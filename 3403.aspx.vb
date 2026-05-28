Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Public Class _2TNRL_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3403"
    Private Conn As SqlConnection
    Private Const EXLC_C As Integer = 100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ' 設定 Title
            setTitle(Me, PAGE_ID)

            ' 設定資料區間標題
            LabelStartdate.Text = Date.Today.AddMonths(-11).ToString("yyyy/MM")
            LabelEnddate.Text = Date.Today.ToString("yyyy/MM")

            Mainprocess()
        End If
    End Sub

    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        ' 本月日報表
        TNRL_Table1()
        TNRL_Table2()
        ' ECharts 趨勢圖資料
        BuildChartData()
    End Sub

    ''' <summary>
    ''' 建立 ECharts 趨勢圖 JSON 資料（近 12 個月）
    ''' </summary>
    Private Sub BuildChartData()
        Dim startYYYYMM As String = Date.Today.AddMonths(-11).ToString("yyyyMM")
        Dim endYYYYMM As String = Date.Today.ToString("yyyyMM")

        ' SQL1：厚度與前段製程（12個月）
        Dim sql1 As String =
            "SELECT SUBSTRING(CONVERT(char, product_date, 112), 1, 6) AS yyyymm, " &
            "SUM(CASE WHEN target_width <= 1260 AND target_thickness <= 1500 THEN coil_weight ELSE 0 END)/1000.0 AS ETNG, " &
            "SUM(CASE WHEN target_width >= 1500 AND target_thickness <= 2300 THEN coil_weight ELSE 0 END)/1000.0 AS WTNG, " &
            "SUM(CASE WHEN target_width > 1260 AND target_width < 1500 AND target_thickness >= 1500 AND target_thickness <= 1900 THEN coil_weight ELSE 0 END)/1000.0 AS NTNG, " &
            "SUM(CASE WHEN target_thickness >= 6000 AND target_thickness <= 9900 THEN coil_weight ELSE 0 END)/1000.0 AS NTCG, " &
            "SUM(CASE WHEN target_thickness > 9900 THEN coil_weight ELSE 0 END)/1000.0 AS ETCG, " &
            "SUM(coil_weight)/1000.0 AS PA, " &
            "SUM(CASE WHEN target_width <= 950 THEN coil_weight ELSE 0 END)/1000.0 AS NRWD, " &
            "SUM(CASE WHEN target_width > 950 AND target_width < 1550 THEN coil_weight ELSE 0 END)/1000.0 AS MDWD, " &
            "SUM(CASE WHEN target_width >= 1550 THEN coil_weight ELSE 0 END)/1000.0 AS WIWD " &
            "FROM h_pmis_coil_info " &
            "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 6) BETWEEN '" & startYYYYMM & "' AND '" & endYYYYMM & "' AND reject_reason = '00' " &
            "GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 1, 6) ORDER BY yyyymm"

        ' SQL2：強度與表面製程（12個月）
        Dim sql2 As String =
            "SELECT SUBSTRING(CONVERT(char, product_date, 112), 1, 6) AS yyyymm, " &
            "SUM(CASE WHEN c <= " & EXLC_C & " THEN coil_weight ELSE 0 END)/1000.0 AS EXLC, " &
            "SUM(CASE WHEN tensile_s <= 40 AND c > " & EXLC_C & " THEN coil_weight ELSE 0 END)/1000.0 AS LSCS, " &
            "SUM(CASE WHEN tensile_s > 40 AND tensile_s <= 50 THEN coil_weight ELSE 0 END)/1000.0 AS MSCS, " &
            "SUM(CASE WHEN tensile_s > 50 AND tensile_s <= 60 THEN coil_weight ELSE 0 END)/1000.0 AS HICS, " &
            "SUM(CASE WHEN tensile_s > 60 THEN coil_weight ELSE 0 END)/1000.0 AS VHIS, " &
            "SUM(CASE WHEN steel_gcode like '6%' THEN coil_weight ELSE 0 END)/1000.0 AS SUS, " &
            "SUM(CASE WHEN inspection_code >= '5000' AND inspection_code < '6000' THEN coil_weight ELSE 0 END)/1000.0 AS NRCQ, " &
            "SUM(CASE WHEN inspection_code >= '4000' AND inspection_code < '5000' THEN coil_weight ELSE 0 END)/1000.0 AS HICQ, " &
            "SUM(CASE WHEN inspection_code >= '2000' AND inspection_code < '4000' THEN coil_weight ELSE 0 END)/1000.0 AS VHCQ " &
            "FROM h_pmis_coil_info " &
            "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 6) BETWEEN '" & startYYYYMM & "' AND '" & endYYYYMM & "' AND reject_reason = '00' " &
            "GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 1, 6) ORDER BY yyyymm"

        Conn.Open()
        Dim dt1 As DataTable = execQuery(sql1, "", Conn)
        Dim dt2 As DataTable = execQuery(sql2, "", Conn)
        Conn.Close()

        ' 確保近 12 個月皆有預設值，避免缺月
        Dim dictDim As New Dictionary(Of String, Double())
        Dim dictStr As New Dictionary(Of String, Double())
        Dim xAxis As New List(Of String)()

        For i As Integer = -11 To 0
            Dim m As String = Date.Today.AddMonths(i).ToString("yyyyMM")
            xAxis.Add("'" & Date.Today.AddMonths(i).ToString("yyyy/MM") & "'")
            dictDim(m) = New Double() {0, 0, 0, 0, 0, 0, 0, 0, 0}
            dictStr(m) = New Double() {0, 0, 0, 0, 0, 0, 0, 0, 0}
        Next

        If dt1 IsNot Nothing Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                Dim m As String = dt1.Rows(i)("yyyymm").ToString()
                If dictDim.ContainsKey(m) Then
                    Dim p_etng = If(IsDBNull(dt1.Rows(i)("ETNG")), 0, Convert.ToDouble(dt1.Rows(i)("ETNG")))
                    Dim p_wtng = If(IsDBNull(dt1.Rows(i)("WTNG")), 0, Convert.ToDouble(dt1.Rows(i)("WTNG")))
                    Dim p_ntng = If(IsDBNull(dt1.Rows(i)("NTNG")), 0, Convert.ToDouble(dt1.Rows(i)("NTNG")))
                    Dim p_ntcg = If(IsDBNull(dt1.Rows(i)("NTCG")), 0, Convert.ToDouble(dt1.Rows(i)("NTCG")))
                    Dim p_etcg = If(IsDBNull(dt1.Rows(i)("ETCG")), 0, Convert.ToDouble(dt1.Rows(i)("ETCG")))
                    Dim p_pa   = If(IsDBNull(dt1.Rows(i)("PA")),   0, Convert.ToDouble(dt1.Rows(i)("PA")))
                    Dim p_mdsz = p_pa - p_etng - p_wtng - p_ntng - p_ntcg - p_etcg
                    Dim p_nrwd = If(IsDBNull(dt1.Rows(i)("NRWD")), 0, Convert.ToDouble(dt1.Rows(i)("NRWD")))
                    Dim p_mdwd = If(IsDBNull(dt1.Rows(i)("MDWD")), 0, Convert.ToDouble(dt1.Rows(i)("MDWD")))
                    Dim p_wiwd = If(IsDBNull(dt1.Rows(i)("WIWD")), 0, Convert.ToDouble(dt1.Rows(i)("WIWD")))
                    dictDim(m) = New Double() {p_etng, p_wtng, p_ntng, p_ntcg, p_etcg, p_mdsz, p_nrwd, p_mdwd, p_wiwd}
                End If
            Next
        End If

        If dt2 IsNot Nothing Then
            For i As Integer = 0 To dt2.Rows.Count - 1
                Dim m As String = dt2.Rows(i)("yyyymm").ToString()
                If dictStr.ContainsKey(m) Then
                    Dim p_exlc = If(IsDBNull(dt2.Rows(i)("EXLC")), 0, Convert.ToDouble(dt2.Rows(i)("EXLC")))
                    Dim p_lscs = If(IsDBNull(dt2.Rows(i)("LSCS")), 0, Convert.ToDouble(dt2.Rows(i)("LSCS")))
                    Dim p_mscs = If(IsDBNull(dt2.Rows(i)("MSCS")), 0, Convert.ToDouble(dt2.Rows(i)("MSCS")))
                    Dim p_hics = If(IsDBNull(dt2.Rows(i)("HICS")), 0, Convert.ToDouble(dt2.Rows(i)("HICS")))
                    Dim p_vhis = If(IsDBNull(dt2.Rows(i)("VHIS")), 0, Convert.ToDouble(dt2.Rows(i)("VHIS")))
                    Dim p_sus  = If(IsDBNull(dt2.Rows(i)("SUS")),  0, Convert.ToDouble(dt2.Rows(i)("SUS")))
                    Dim p_nrcq = If(IsDBNull(dt2.Rows(i)("NRCQ")), 0, Convert.ToDouble(dt2.Rows(i)("NRCQ")))
                    Dim p_hicq = If(IsDBNull(dt2.Rows(i)("HICQ")), 0, Convert.ToDouble(dt2.Rows(i)("HICQ")))
                    Dim p_vhcq = If(IsDBNull(dt2.Rows(i)("VHCQ")), 0, Convert.ToDouble(dt2.Rows(i)("VHCQ")))
                    dictStr(m) = New Double() {p_exlc, p_lscs, p_mscs, p_hics, p_vhis, p_sus, p_nrcq, p_hicq, p_vhcq}
                End If
            Next
        End If

        ' 組合 ECharts JSON 字串
        Dim months As New List(Of String)(dictDim.Keys)
        months.Sort()

        Dim etng As New List(Of String)()
        Dim wtng As New List(Of String)()
        Dim ntng As New List(Of String)()
        Dim ntcg As New List(Of String)()
        Dim etcg As New List(Of String)()
        Dim mdsz As New List(Of String)()
        Dim nrwd As New List(Of String)()
        Dim mdwd As New List(Of String)()
        Dim wiwd As New List(Of String)()
        Dim exlc As New List(Of String)()
        Dim lscs As New List(Of String)()
        Dim mscs As New List(Of String)()
        Dim hics As New List(Of String)()
        Dim vhis As New List(Of String)()
        Dim sus  As New List(Of String)()
        Dim nrcq As New List(Of String)()
        Dim hicq As New List(Of String)()
        Dim vhcq As New List(Of String)()

        For Each m As String In months
            Dim d() As Double = dictDim(m)
            Dim s() As Double = dictStr(m)
            etng.Add(d(0).ToString("0.00"))
            wtng.Add(d(1).ToString("0.00"))
            ntng.Add(d(2).ToString("0.00"))
            ntcg.Add(d(3).ToString("0.00"))
            etcg.Add(d(4).ToString("0.00"))
            mdsz.Add(d(5).ToString("0.00"))
            nrwd.Add(d(6).ToString("0.00"))
            mdwd.Add(d(7).ToString("0.00"))
            wiwd.Add(d(8).ToString("0.00"))
            exlc.Add(s(0).ToString("0.00"))
            lscs.Add(s(1).ToString("0.00"))
            mscs.Add(s(2).ToString("0.00"))
            hics.Add(s(3).ToString("0.00"))
            vhis.Add(s(4).ToString("0.00"))
            sus.Add(s(5).ToString("0.00"))
            nrcq.Add(s(6).ToString("0.00"))
            hicq.Add(s(7).ToString("0.00"))
            vhcq.Add(s(8).ToString("0.00"))
        Next

        Dim script As String =
            "var chartData = {" &
            "xAxis: [" & String.Join(",", xAxis) & "]," &
            "etng:[" & String.Join(",", etng) & "]," &
            "wtng:[" & String.Join(",", wtng) & "]," &
            "ntng:[" & String.Join(",", ntng) & "]," &
            "ntcg:[" & String.Join(",", ntcg) & "]," &
            "etcg:[" & String.Join(",", etcg) & "]," &
            "mdsz:[" & String.Join(",", mdsz) & "]," &
            "nrwd:[" & String.Join(",", nrwd) & "]," &
            "mdwd:[" & String.Join(",", mdwd) & "]," &
            "wiwd:[" & String.Join(",", wiwd) & "]," &
            "exlc:[" & String.Join(",", exlc) & "]," &
            "lscs:[" & String.Join(",", lscs) & "]," &
            "mscs:[" & String.Join(",", mscs) & "]," &
            "hics:[" & String.Join(",", hics) & "]," &
            "vhis:[" & String.Join(",", vhis) & "]," &
            "sus:[" & String.Join(",", sus) & "]," &
            "nrcq:[" & String.Join(",", nrcq) & "]," &
            "hicq:[" & String.Join(",", hicq) & "]," &
            "vhcq:[" & String.Join(",", vhcq) & "]" &
            "};"

        ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
    End Sub

    ''' <summary>
    ''' 厚度/前段製程：本月日報表 (gvMonth1 + gvMonth3)
    ''' </summary>
    Private Sub TNRL_Table1()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "ETNG", "WTNG", "NTNG", "NTCG", "ETCG", "MDSZ"}
        Dim strMonthTitle1() As String = {"NRWD", "MDWD", "WIWD"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next
        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next

        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Today), Month(Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") & "月" & (i + 1).ToString("d2") & "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next
        For i As Integer = 0 To daysInMonth - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth1.Text = Date.Today.ToString("MM")

        Conn.Open()

        ' ETNG
        Dim strACCESS As String
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width <= 1260 and avg_thickness <= 1500 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width <= 1260 and coil_thickness <= 1500 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETNG.Text = calTmp.ToString("0.00")

        ' WTNG
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width >= 1500 and avg_thickness <= 2300 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width >= 1500 and coil_thickness <= 2300 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWTNG.Text = calTmp.ToString("0.00")

        ' NTNG
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width > 1260 and avg_width < 1500 and avg_thickness >= 1500 and avg_thickness <= 1900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width > 1260 and coil_width < 1500 and coil_thickness >= 1500 and coil_thickness <= 1900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTNG.Text = calTmp.ToString("0.00")

        ' NTCG
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_thickness >= 6000 and avg_thickness <= 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_thickness >= 6000 and coil_thickness <= 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTCG.Text = calTmp.ToString("0.00")

        ' ETCG
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_thickness > 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_thickness > 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETCG.Text = calTmp.ToString("0.00")

        ' MDSZ = PA - ETNG - WTNG - NTNG - NTCG - ETCG
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing AndAlso dtTmp.Rows.Count > 0 Then
            calTmp = 0
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                Dim dayIdx As Integer = CInt(dtTmp.Rows(i).Item(0)) - 1
                With dtDataTable.Rows(dayIdx)
                    Dim paVal As Double = Val(dtTmp.Rows(i).Item(1)) / 1000
                    Dim mdszVal As Double = paVal - Val(.Item(1)) - Val(.Item(2)) - Val(.Item(3)) - Val(.Item(4)) - Val(.Item(5))
                    .Item(6) = IIf(mdszVal < 0, "0.00", mdszVal.ToString("0.00"))
                    calTmp += Val(.Item(6))
                End With
            Next
        End If
        lblMDSZ.Text = calTmp.ToString("0.00")

        gvMonth1.DataSource = dtDataTable
        gvMonth1.DataBind()
        gvMonth1.HeaderRow.Visible = False

        ' NRWD
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width <= 950 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width <= 950 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRWD.Text = calTmp.ToString("0.00")

        ' MDWD
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width > 950 and avg_width < 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width > 950 and coil_width < 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMDWD.Text = calTmp.ToString("0.00")

        ' WIWD
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh93 " &
            "where shift_date like '{0}%' and avg_width >= 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9b " &
            "where shift_date like '{0}%' and coil_width >= 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWIWD.Text = calTmp.ToString("0.00")

        gvMonth3.DataSource = dtdatatable1
        gvMonth3.DataBind()
        gvMonth3.HeaderRow.Visible = False

        Conn.Close()
    End Sub

    ''' <summary>
    ''' 強度/表面製程：本月日報表 (gvMonth2 + gvMonth4)
    ''' </summary>
    Private Sub TNRL_Table2()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "EXLC", "LSCS", "MSCS", "HICS", "VHIS", "SUS"}
        Dim strMonthTitle1() As String = {"NRCQ", "HICQ", "VHCQ"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next
        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next

        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Today), Month(Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") & "月" & (i + 1).ToString("d2") & "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next
        For i As Integer = 0 To daysInMonth - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth2.Text = Date.Today.ToString("MM")

        Conn.Open()
        Dim strACCESS As String

        ' EXLC（碳含量 <= 100）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon <= " & EXLC_C.ToString() &
            " Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon <= " & EXLC_C.ToString() &
            " Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblEXLC.Text = calTmp.ToString("0.00")

        ' LSCS（tensile <= 40, carbon > 100）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile <= 40 " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile <= 40 " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLSCS.Text = calTmp.ToString("0.00")

        ' MSCS（tensile 40~50）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 40 and wh91.tensile <= 50 " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 40 and wh91.tensile <= 50 " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMSCS.Text = calTmp.ToString("0.00")

        ' HICS（tensile 50~60）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 50 and wh91.tensile <= 60 " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 50 and wh91.tensile <= 60 " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICS.Text = calTmp.ToString("0.00")

        ' VHIS（tensile > 60）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 60 " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.tensile > 60 " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHIS.Text = calTmp.ToString("0.00")

        ' SUS（鋼種代碼 6%）
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.steel_grade_code like '6%' " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.carbon > " & EXLC_C.ToString() & " and wh91.steel_grade_code like '6%' " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(6) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblSUS.Text = calTmp.ToString("0.00")

        gvMonth2.DataSource = dtDataTable
        gvMonth2.DataBind()
        gvMonth2.HeaderRow.Visible = False

        ' NRCQ
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.inspection_code < '6000' and wh91.inspection_code >= '5000' " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.inspection_code < '6000' and wh91.inspection_code >= '5000' " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRCQ.Text = calTmp.ToString("0.00")

        ' HICQ
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.inspection_code < '5000' and wh91.inspection_code >= '4000' " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.inspection_code < '5000' and wh91.inspection_code >= '4000' " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICQ.Text = calTmp.ToString("0.00")

        ' VHCQ
        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh93.shift_date, 7, 2) as product_day, SUM(wh93.g_weight) as product_weight from h_pmis_wh93 as wh93, h_pmis_wh91 as wh91 " &
            "where wh93.shift_date like '{0}%' and SUBSTRING(wh93.product_no,1, 7) = wh91.coil_no " &
            "and wh91.inspection_code < '4000' and wh91.inspection_code >= '2000' " &
            "Group by SUBSTRING(wh93.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh9b.shift_date, 7, 2) as product_day, SUM(wh9b.gross_weight) as product_weight from h_pmis_wh9b as wh9b, h_pmis_wh91 as wh91 " &
            "where wh9b.shift_date like '{0}%' and wh9b.coil_no = wh91.coil_no " &
            "and wh91.inspection_code < '4000' and wh91.inspection_code >= '2000' " &
            "Group by SUBSTRING(wh9b.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHCQ.Text = calTmp.ToString("0.00")

        gvMonth4.DataSource = dtdatatable1
        gvMonth4.DataBind()
        gvMonth4.HeaderRow.Visible = False

        Conn.Close()
    End Sub

End Class