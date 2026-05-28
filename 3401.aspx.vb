Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' 3401 TPM HSM 生產品種分類統計
''' 依厚度/寬度分類（ETNG/WTNG/NTNG/NTCG/ETCG/MDSZ）以及
''' 依強度/品質分類（EXLC/LSCS/MSCS/HICS/VHIS/SUS/NRCQ/HICQ/VHCQ）
''' 顯示本月每日統計與近12個月趨勢圖
''' 資料來源：h_pmis_coil_info
''' EXLC_C = 100（低碳鋼界定碳含量上限）
''' </summary>
Partial Public Class TPM_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3401"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private Const EXLC_C As Integer = 100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '設定趨勢圖資料區間標題（近12個月）
            LabelStartdate.Text = Date.Today.AddMonths(-11).ToString("yyyy/MM")
            LabelEnddate.Text = Date.Today.ToString("yyyy/MM")

            Mainprocess()
        End If
    End Sub

    Private Sub Mainprocess()
        '建立資料庫連線（PMIS 主資料庫）
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        '產生本月日報表（尺寸分類 + 強度分類）
        HSM_Table1()
        HSM_Table2()
        '產生 ECharts 趨勢圖 JSON
        BuildChartData()
    End Sub

    ''' <summary>
    ''' 建立 ECharts 趨勢圖 JSON 資料（近12個月）
    ''' sql1：尺寸與寬度分類，一次查詢涵蓋 ETNG/WTNG/NTNG/NTCG/ETCG/PA/NRWD/MDWD/WIWD
    ''' sql2：強度與品質分類，一次查詢涵蓋 EXLC/LSCS/MSCS/HICS/VHIS/SUS/NRCQ/HICQ/VHCQ
    ''' </summary>
    Private Sub BuildChartData()
        Dim startYYYYMM As String = Date.Today.AddMonths(-11).ToString("yyyyMM")
        Dim endYYYYMM As String = Date.Today.ToString("yyyyMM")

        'SQL 1: 尺寸與寬度 12個月趨勢（整合單一高效率查詢）
        Dim sql1 As String = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 1, 6) AS yyyymm, " &
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

        'SQL 2: 強度與品質 12個月趨勢（整合單一高效率查詢）
        Dim sql2 As String = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 1, 6) AS yyyymm, " &
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

        '確保連續12個月皆有預設值，避免缺月造成圖表斷點
        Dim dictDim As New Dictionary(Of String, Double())
        Dim dictStr As New Dictionary(Of String, Double())
        Dim xAxis As New List(Of String)()

        For i As Integer = -11 To 0
            Dim m As String = Date.Today.AddMonths(i).ToString("yyyyMM")
            xAxis.Add("'" & Date.Today.AddMonths(i).ToString("yyyy/MM") & "'")
            dictDim(m) = New Double() {0, 0, 0, 0, 0, 0, 0, 0, 0}
            dictStr(m) = New Double() {0, 0, 0, 0, 0, 0, 0, 0, 0}
        Next

        '填入尺寸分類資料（ETNG/WTNG/NTNG/NTCG/ETCG/MDSZ/NRWD/MDWD/WIWD）
        If dt1 IsNot Nothing Then
            For i As Integer = 0 To dt1.Rows.Count - 1
                Dim m As String = dt1.Rows(i)("yyyymm").ToString()
                If dictDim.ContainsKey(m) Then
                    Dim p_etng = If(IsDBNull(dt1.Rows(i)("ETNG")), 0, Convert.ToDouble(dt1.Rows(i)("ETNG")))
                    Dim p_wtng = If(IsDBNull(dt1.Rows(i)("WTNG")), 0, Convert.ToDouble(dt1.Rows(i)("WTNG")))
                    Dim p_ntng = If(IsDBNull(dt1.Rows(i)("NTNG")), 0, Convert.ToDouble(dt1.Rows(i)("NTNG")))
                    Dim p_ntcg = If(IsDBNull(dt1.Rows(i)("NTCG")), 0, Convert.ToDouble(dt1.Rows(i)("NTCG")))
                    Dim p_etcg = If(IsDBNull(dt1.Rows(i)("ETCG")), 0, Convert.ToDouble(dt1.Rows(i)("ETCG")))
                    Dim p_pa = If(IsDBNull(dt1.Rows(i)("PA")), 0, Convert.ToDouble(dt1.Rows(i)("PA")))
                    'MDSZ = PA - 其他各尺寸分類之總和
                    Dim p_mdsz = p_pa - p_etng - p_wtng - p_ntng - p_ntcg - p_etcg

                    Dim p_nrwd = If(IsDBNull(dt1.Rows(i)("NRWD")), 0, Convert.ToDouble(dt1.Rows(i)("NRWD")))
                    Dim p_mdwd = If(IsDBNull(dt1.Rows(i)("MDWD")), 0, Convert.ToDouble(dt1.Rows(i)("MDWD")))
                    Dim p_wiwd = If(IsDBNull(dt1.Rows(i)("WIWD")), 0, Convert.ToDouble(dt1.Rows(i)("WIWD")))
                    dictDim(m) = New Double() {p_etng, p_wtng, p_ntng, p_ntcg, p_etcg, p_mdsz, p_nrwd, p_mdwd, p_wiwd}
                End If
            Next
        End If

        '填入強度/品質分類資料（EXLC/LSCS/MSCS/HICS/VHIS/SUS/NRCQ/HICQ/VHCQ）
        If dt2 IsNot Nothing Then
            For i As Integer = 0 To dt2.Rows.Count - 1
                Dim m As String = dt2.Rows(i)("yyyymm").ToString()
                If dictStr.ContainsKey(m) Then
                    dictStr(m) = New Double() {
                        If(IsDBNull(dt2.Rows(i)("EXLC")), 0, Convert.ToDouble(dt2.Rows(i)("EXLC"))),
                        If(IsDBNull(dt2.Rows(i)("LSCS")), 0, Convert.ToDouble(dt2.Rows(i)("LSCS"))),
                        If(IsDBNull(dt2.Rows(i)("MSCS")), 0, Convert.ToDouble(dt2.Rows(i)("MSCS"))),
                        If(IsDBNull(dt2.Rows(i)("HICS")), 0, Convert.ToDouble(dt2.Rows(i)("HICS"))),
                        If(IsDBNull(dt2.Rows(i)("VHIS")), 0, Convert.ToDouble(dt2.Rows(i)("VHIS"))),
                        If(IsDBNull(dt2.Rows(i)("SUS")), 0, Convert.ToDouble(dt2.Rows(i)("SUS"))),
                        If(IsDBNull(dt2.Rows(i)("NRCQ")), 0, Convert.ToDouble(dt2.Rows(i)("NRCQ"))),
                        If(IsDBNull(dt2.Rows(i)("HICQ")), 0, Convert.ToDouble(dt2.Rows(i)("HICQ"))),
                        If(IsDBNull(dt2.Rows(i)("VHCQ")), 0, Convert.ToDouble(dt2.Rows(i)("VHCQ")))
                    }
                End If
            Next
        End If

        '轉換成 ECharts JSON 格式各系列資料
        Dim etng As New List(Of Double)(), wtng As New List(Of Double)(), ntng As New List(Of Double)()
        Dim ntcg As New List(Of Double)(), etcg As New List(Of Double)(), mdsz As New List(Of Double)()
        Dim nrwd As New List(Of Double)(), mdwd As New List(Of Double)(), wiwd As New List(Of Double)()

        Dim exlc As New List(Of Double)(), lscs As New List(Of Double)(), mscs As New List(Of Double)()
        Dim hics As New List(Of Double)(), vhis As New List(Of Double)(), sus As New List(Of Double)()
        Dim nrcq As New List(Of Double)(), hicq As New List(Of Double)(), vhcq As New List(Of Double)()

        For Each key As String In dictDim.Keys
            etng.Add(dictDim(key)(0)) : wtng.Add(dictDim(key)(1)) : ntng.Add(dictDim(key)(2))
            ntcg.Add(dictDim(key)(3)) : etcg.Add(dictDim(key)(4)) : mdsz.Add(dictDim(key)(5))
            nrwd.Add(dictDim(key)(6)) : mdwd.Add(dictDim(key)(7)) : wiwd.Add(dictDim(key)(8))

            exlc.Add(dictStr(key)(0)) : lscs.Add(dictStr(key)(1)) : mscs.Add(dictStr(key)(2))
            hics.Add(dictStr(key)(3)) : vhis.Add(dictStr(key)(4)) : sus.Add(dictStr(key)(5))
            nrcq.Add(dictStr(key)(6)) : hicq.Add(dictStr(key)(7)) : vhcq.Add(dictStr(key)(8))
        Next

        '組合最終 ECharts JSON 字串並注入前端
        Dim script As String = "var chartData = {" &
            "xAxis: [" & String.Join(",", xAxis) & "]," &
            "etng: [" & String.Join(",", etng) & "], wtng: [" & String.Join(",", wtng) & "], ntng: [" & String.Join(",", ntng) & "]," &
            "ntcg: [" & String.Join(",", ntcg) & "], etcg: [" & String.Join(",", etcg) & "], mdsz: [" & String.Join(",", mdsz) & "]," &
            "nrwd: [" & String.Join(",", nrwd) & "], mdwd: [" & String.Join(",", mdwd) & "], wiwd: [" & String.Join(",", wiwd) & "]," &
            "exlc: [" & String.Join(",", exlc) & "], lscs: [" & String.Join(",", lscs) & "], mscs: [" & String.Join(",", mscs) & "]," &
            "hics: [" & String.Join(",", hics) & "], vhis: [" & String.Join(",", vhis) & "], sus: [" & String.Join(",", sus) & "]," &
            "nrcq: [" & String.Join(",", nrcq) & "], hicq: [" & String.Join(",", hicq) & "], vhcq: [" & String.Join(",", vhcq) & "]" &
        "};"

        ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
    End Sub

    ''' <summary>
    ''' 本月尺寸/寬度分類日報表（gvMonth1=厚度, gvMonth3=寬度）
    ''' ETNG: 薄厚(W≤1260,T≤1500) / WTNG: 寬厚(W≥1500,T≤2300)
    ''' NTNG: 中寬中厚(1260&lt;W&lt;1500, 1500≤T≤1900) / NTCG: 中厚捲(T 6000~9900)
    ''' ETCG: 極厚捲(T&gt;9900) / MDSZ: 其他尺寸（PA 扣除以上各類）
    ''' NRWD: (W≤950) / MDWD: 中寬(950&lt;W&lt;1550) / WIWD: 寬(W≥1550)
    ''' </summary>
    Private Sub HSM_Table1()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "ETNG", "WTNG", "NTNG", "NTCG", "ETCG", "MDSZ"}
        Dim strMonthTitle1() As String = {"NRWD", "MDWD", "WIWD"}
        Dim strACCESS As String = Nothing
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next

        '初始化每月天數列（預設值 0.00）
        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") + "月" + (i + 1).ToString("d2") + "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next

        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth1.Text = Date.Today.ToString("MM")

        Conn.Open()

        'ETNG — 薄厚（W≤1260, T≤1500）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = 0 and " &
                    "target_width <= 1260 and " &
                    "target_thickness <= 1500 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += Val(dtTmp.Rows(i).Item(1)) / 1000
        Next
        lblETNG.Text = calTmp.ToString("0.00")

        'WTNG — 寬厚（W≥1500, T≤2300）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_width >= 1500 and " &
                    "target_thickness <= 2300 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWTNG.Text = calTmp.ToString("0.00")

        'NTNG — 中寬中厚（1260<W<1500, 1500≤T≤1900）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_width > 1260 and " &
                    "target_width < 1500 and " &
                    "target_thickness >= 1500 and " &
                    "target_thickness <= 1900 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTNG.Text = calTmp.ToString("0.00")

        'NTCG — 中厚捲（T 6000~9900）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_thickness >= 6000 and " &
                    "target_thickness <= 9900 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTCG.Text = calTmp.ToString("0.00")

        'ETCG — 極厚捲（T>9900）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_thickness > 9900 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETCG.Text = calTmp.ToString("0.00")

        'MDSZ — 其他尺寸（PA 總量扣除 ETNG/WTNG/NTNG/NTCG/ETCG）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 7, 2) as day, SUM(coil_weight) as PA " &
                    "FROM h_pmis_coil_info WHERE " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 1, 6) = '" + Now.ToString("yyyyMM") + "' and reject_reason=0 GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                calTmp = 0
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    With dtDataTable.Rows(Val(dtTmp.Rows(i).Item(0)) - 1)
                        .Item(6) = (Math.Round((Val(dtTmp.Rows(i).Item(1)) / 1000), 2, MidpointRounding.AwayFromZero) - Val(.Item(4)) - Val(.Item(3)) - Val(.Item(2)) - Val(.Item(1))).ToString("0.00")
                        calTmp += Val(.Item(6))
                    End With
                Next
            End If
            dtTmp.Dispose()
        End If
        lblMDSZ.Text = calTmp.ToString("0.00")

        gvMonth1.DataSource = dtDataTable
        gvMonth1.DataBind()
        gvMonth1.HeaderRow.Visible = False

        gvMonth1.Rows(0).Cells(0).Width = 100
        For i As Integer = 1 To 6
            gvMonth1.Rows(0).Cells(i).Width = 80
        Next

        'NRWD — （W≤950）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_width <= 950 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRWD.Text = calTmp.ToString("0.00")

        'MDWD — 中寬（950<W<1550）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_width > 950 and " &
                    "target_width < 1550 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMDWD.Text = calTmp.ToString("0.00")

        'WIWD — 寬（W≥1550）
        strACCESS = "SELECT " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 7, 2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "target_width >= 1550 " &
                    "group by SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
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

        For i As Integer = 0 To 2
            gvMonth3.Rows(0).Cells(i).Width = 80
        Next

        Conn.Close()
    End Sub

    ''' <summary>
    ''' 本月強度/品質分類日報表（gvMonth2=強度, gvMonth4=品質）
    ''' EXLC: 低碳鋼(C≤100) / LSCS: 低強度(tensile≤40, C>100)
    ''' MSCS: 中強度(40&lt;t≤50) / HICS: 高強度(50&lt;t≤60) / VHIS: 超高強度(t>60)
    ''' SUS: 不鏽鋼(steel_gcode like '6%')
    ''' NRCQ: 非輻射檢查(5000≤inspection&lt;6000)
    ''' HICQ: 高檢查(4000≤inspection&lt;5000) / VHCQ: 超高檢查(2000≤inspection&lt;4000)
    ''' </summary>
    Private Sub HSM_Table2()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"strength and quality", "EXLC", "LSCS", "MSCS", "HICS", "VHIS", "SUS"}
        Dim strMonthTitle1() As String = {"NRCQ", "HICQ", "VHCQ"}
        Dim strACCESS As String = Nothing
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next

        '初始化每月天數列
        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") + "月" + (i + 1).ToString("d2") + "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next

        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth2.Text = Date.Today.ToString("MM")

        Conn.Open()
        'EXLC — 低碳鋼（C ≤ EXLC_C = 100）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) " &
                    "FROM h_pmis_coil_info WHERE " &
                    "SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Now.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Now.ToString("MM") + " and " &
                    "reject_reason = '00' and c <= " + EXLC_C.ToString + " GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblEXLC.Text = calTmp.ToString("0.00")

        'LSCS — 低強度鋼（tensile≤40, C>EXLC_C）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "tensile_s <= 40 and c > " + EXLC_C.ToString + " GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLSCS.Text = calTmp.ToString("0.00")

        'MSCS — 中強度鋼（40<tensile≤50）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "tensile_s > 40 and " &
                    "tensile_s <= 50 " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMSCS.Text = calTmp.ToString("0.00")

        'HICS — 高強度鋼（50<tensile≤60）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "tensile_s > 50 and " &
                    "tensile_s <= 60 " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICS.Text = calTmp.ToString("0.00")

        'VHIS — 超高強度鋼（tensile>60）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "tensile_s > 60 " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHIS.Text = calTmp.ToString("0.00")

        'SUS — 不鏽鋼（steel_gcode like '6%'）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "steel_gcode like '6%' " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
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

        gvMonth2.Rows(0).Cells(0).Width = 100
        For i As Integer = 1 To 6
            gvMonth2.Rows(0).Cells(i).Width = 80
        Next

        'NRCQ — 非輻射檢查（5000≤inspection<6000）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "inspection_code < '6000' and " &
                    "inspection_code >= '5000' " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRCQ.Text = calTmp.ToString("0.00")

        'HICQ — 高檢查（4000≤inspection<5000）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                     "inspection_code < '5000' and " &
                    "inspection_code >= '4000' " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICQ.Text = calTmp.ToString("0.00")

        'VHCQ — 超高檢查（2000≤inspection<4000）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112),7,2), SUM(coil_weight) FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112),1,4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112),5,2) = " + Date.Today.ToString("MM") + " and " &
                    "reject_reason = '00' and " &
                    "inspection_code < '4000' and " &
                    "inspection_code >= '2000' " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112),7,2)"
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

        For i As Integer = 0 To 2
            gvMonth4.Rows(0).Cells(i).Width = 80
        Next

        Conn.Close()
    End Sub
End Class
