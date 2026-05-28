Imports System.Data.SqlClient

Partial Public Class _2TNRL_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3403"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date
    Dim adapter1 As SqlDataAdapter = Nothing
    Private Const EXLC_C As Integer = 100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim count1 As Integer = WebChart1.Chart.Series.Count
        For i As Integer = 0 To count1 - 1
            WebChart1.Chart.Series(i).CheckDataSource()
            WebChart1.Chart.Series(i).RefreshSeries()
        Next
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)
            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = SqlDataSource1.Select(args1)
            Dim count As Integer = DR1.Count
            LabelStartdate.Text = Format(CDate(DR1(0)(0).ToString), "yyyy/MM")
            LabelEnddate.Text = Format(CDate(DR1(count - 1)(0).ToString), "yyyy/MM")
            Mainprocess()
        End If

    End Sub

    Private Sub TNRL2_Table1()
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
        'ETNG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width <= 1260 and avg_thickness <= 1500 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width <= 1260 and coil_thickness <= 1500 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))


        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETNG.Text = calTmp.ToString("0.00")

        'WTNG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width >= 1500 and avg_thickness <= 2300 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width >= 1500 and coil_thickness <= 2300 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWTNG.Text = calTmp.ToString("0.00")

        'NTNG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width > 1260 and avg_width < 1500 " & _
                        "and avg_thickness >= 1500 and avg_thickness <= 1900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width > 1260 and coil_width < 1500 " & _
                        "and coil_thickness >= 1500 and coil_thickness <= 1900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTNG.Text = calTmp.ToString("0.00")

        'NTCG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_thickness >= 6000 and avg_thickness <= 9900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_thickness >= 6000 and coil_thickness <= 9900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTCG.Text = calTmp.ToString("0.00")

        'ETCG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_thickness > 9900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_thickness > 9900 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETCG.Text = calTmp.ToString("0.00")

        'MDSZ
        ' PA - ETNG - WTNG - NTNG - NTCG - ETCG
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                calTmp = 0
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    With dtDataTable.Rows(Val(dtTmp.Rows(i).Item(0)) - 1)
                        .Item(6) = (Val((Val(dtTmp.Rows(i).Item(1)) / 1000).ToString("0.00")) - Val(.Item(5)) - Val(.Item(4)) - Val(.Item(3)) - Val(.Item(2)) - Val(.Item(1))).ToString("0.00")
                        .Item(6) = IIf(Val(.Item(6)) < 0, "0.00", .Item(6))
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

        'NRWD
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width <= 950 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width <= 950 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRWD.Text = calTmp.ToString("0.00")

        'MDWD
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width > 950 and avg_width < 1550 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width > 950 and coil_width < 1550 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMDWD.Text = calTmp.ToString("0.00")

        'WIWD
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh97 " & _
                        "where shift_date like '{0}%' " & _
                        "and avg_width >= 1550 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
                        "where shift_date like '{0}%' " & _
                        "and coil_width >= 1550 " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day", _
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

        For i As Integer = 0 To 2
            gvMonth3.Rows(0).Cells(i).Width = 80
        Next

        Conn.Close()
    End Sub

    Private Sub TNRL2_Table2()
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
        'EXLC
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon <=" + EXLC_C.ToString + _
                                        " Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon <=" + EXLC_C.ToString + _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblEXLC.Text = calTmp.ToString("0.00")

        'LSCS
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + " and wh95.tensile <= 40 " & _
                                        " Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + " and wh95.tensile <= 40 " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLSCS.Text = calTmp.ToString("0.00")

        'MSCS
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 40 and wh95.tensile <= 50 " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 40 and wh95.tensile <= 50 " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMSCS.Text = calTmp.ToString("0.00")

        'HICS
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 50 and wh95.tensile <= 60 " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 50 and wh95.tensile <= 60 " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICS.Text = calTmp.ToString("0.00")

        'VHIS
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 60 " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.tensile > 60 " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHIS.Text = calTmp.ToString("0.00")

        'SUS
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.steel_grade_code like '6%' " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.carbon > " + EXLC_C.ToString + _
                                        " and wh95.steel_grade_code like '6%' " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
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

        gvMonth2.Rows(0).Cells(0).Width = 100

        For i As Integer = 1 To 6
            gvMonth2.Rows(0).Cells(i).Width = 80
        Next

        'NRCQ
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.inspection_code < '6000' and wh95.inspection_code >= '5000' " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.inspection_code < '6000' and wh95.inspection_code >= '5000' " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRCQ.Text = calTmp.ToString("0.00")

        'HICQ
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.inspection_code < '5000' and wh95.inspection_code >= '4000' " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.inspection_code < '5000' and wh95.inspection_code >= '4000' " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
                        Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICQ.Text = calTmp.ToString("0.00")

        'VHCQ
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                        "(select SUBSTRING(wh97.shift_date, 7, 2) as product_day, SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 " & _
                                        "where wh97.shift_date like '{0}%' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
                                        "and wh95.inspection_code < '4000' and wh95.inspection_code >= '2000' " & _
                                        "Group by SUBSTRING(wh97.shift_date, 7, 2)) as A " & _
                                  "FULL OUTER JOIN " & _
                                        "(select SUBSTRING(wh9c.shift_date, 7, 2) as product_day, SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 " & _
                                        "where wh9c.shift_date like '{0}%' and wh9c.coil_no = wh95.coil_no " & _
                                        "and wh95.inspection_code < '4000' and wh95.inspection_code >= '2000' " & _
                                        "Group by SUBSTRING(wh9c.shift_date, 7, 2)) as B " & _
                                  "ON A.product_day = B.product_day", _
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

        For i As Integer = 0 To 2
            gvMonth4.Rows(0).Cells(i).Width = 80
        Next

        Conn.Close()
    End Sub

    'Private Sub TeeChartData1()
    '    Dim strDate As New StringBuilder
    '    Dim strETNG As New StringBuilder
    '    Dim strWTNG As New StringBuilder
    '    Dim strNTNG As New StringBuilder
    '    Dim strNTCG As New StringBuilder
    '    Dim strETCG As New StringBuilder
    '    Dim strMDSZ As New StringBuilder

    '    Dim strNRWD As New StringBuilder
    '    Dim strMDWD As New StringBuilder
    '    Dim strWIWD As New StringBuilder

    '    Dim dtTmp As DataTable = Nothing
    '    Dim tmpDate As Date
    '    Dim dtOverall As New DataTable
    '    Dim strTitle() As String = {"Year", "Month", "ETNG", "WTNG", "NTNG", "NTCG", "ETCG", "MDSZ", "NRWD", "MDWD", "WTWD"}
    '    Dim dr As DataRow
    '    Dim rowNum As Integer

    '    For i As Integer = 0 To strTitle.Length - 1
    '        dtOverall.Columns.Add(New DataColumn())
    '    Next

    '    'layout
    '    For i As Integer = 0 To 12
    '        dr = dtOverall.NewRow
    '        dtOverall.Rows.Add(dr)
    '    Next

    '    tmpDate = chartDate
    '    For i As Integer = 0 To 12
    '        dtOverall.Rows(i).Item(0) = tmpDate.AddMonths(i).Year.ToString
    '        dtOverall.Rows(i).Item(1) = tmpDate.AddMonths(i).Month.ToString
    '        For j As Integer = 2 To strTitle.Length - 1
    '            dtOverall.Rows(i).Item(j) = "0"
    '        Next
    '    Next

    '    Conn.Open()
    '    'ETNG
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width <= 1260 and avg_thickness <= 1500 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width <= 1260 and coil_thickness <= 1500 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(2) = (Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")
    '    Next

    '    'WTNG
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width >= 1500 and avg_thickness <= 2300 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width >= 1500 and coil_thickness <= 2300 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(3) = (Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")
    '    Next

    '    'NTNG
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width > 1260 and avg_width < 1500 " & _
    '                                "and avg_thickness >= 1500 and avg_thickness <= 1900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width > 1260 and coil_width < 1500 " & _
    '                                "and coil_thickness >= 1500 and coil_thickness <= 1900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(4) = (Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")
    '    Next

    '    'NTCG
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_thickness >= 6000 and avg_thickness <= 9900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_thickness >= 6000 and coil_thickness <= 9900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(5) = (Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")
    '    Next

    '    'ETCG
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_thickness > 9900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_thickness > 9900 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(6) = (Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")
    '    Next

    '    ' MDSZ
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        With dtOverall.Rows(rowNum)
    '            .Item(7) = ((Val(Val(dtTmp.Rows(i).Item(2)) / 1000).ToString("0.00")) - Val(.Item(6)) - Val(.Item(5)) - Val(.Item(4)) - Val(.Item(3)) - Val(.Item(2))).ToString("0.00")
    '        End With
    '    Next

    '    'NRWD
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width <= 950 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width <= 950 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(8) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'MDWD
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width >= 950 and avg_width <1550 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width >= 950 and coil_width < 1550 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(9) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'WIWD
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_wh97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and avg_width >= 1550 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_wh9c " & _
    '                                "where shift_date between '{0}' and '{1}' " & _
    '                                "and coil_width >= 1550 " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(10) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    Conn.Close()

    '    '傳入前台控制項

    '    For i As Integer = 0 To dtOverall.Rows.Count - 1
    '        strDate.Append(tmpDate.AddMonths(i).ToString("MM") + "月,")
    '        strETNG.Append(dtOverall.Rows(i).Item(2).ToString + ",")
    '        strWTNG.Append(dtOverall.Rows(i).Item(3).ToString + ",")
    '        strNTNG.Append(dtOverall.Rows(i).Item(4).ToString + ",")
    '        strNTCG.Append(dtOverall.Rows(i).Item(5).ToString + ",")
    '        strETCG.Append(dtOverall.Rows(i).Item(6).ToString + ",")
    '        strMDSZ.Append(dtOverall.Rows(i).Item(7).ToString + ",")
    '        strNRWD.Append(dtOverall.Rows(i).Item(8).ToString + ",")
    '        strMDWD.Append(dtOverall.Rows(i).Item(9).ToString + ",")
    '        strWIWD.Append(dtOverall.Rows(i).Item(10).ToString + ",")
    '    Next

    '    hDate.Value = strDate.ToString
    '    hETNG.Value = strETNG.ToString
    '    hWTNG.Value = strWTNG.ToString
    '    hNTNG.Value = strNTNG.ToString
    '    hNTCG.Value = strNTCG.ToString
    '    hETCG.Value = strETCG.ToString
    '    hMDSZ.Value = strMDSZ.ToString
    '    hNRWD.Value = strNRWD.ToString
    '    hMDWD.Value = strMDWD.ToString
    '    hWIWD.Value = strWIWD.ToString

    'End Sub

    'Private Sub TeeChartData2()
    '    Dim strEXLC As New StringBuilder
    '    Dim strLSCS As New StringBuilder
    '    Dim strMSCS As New StringBuilder
    '    Dim strHICS As New StringBuilder
    '    Dim strVHIS As New StringBuilder
    '    Dim strSUS As New StringBuilder

    '    Dim strNRCQ As New StringBuilder
    '    Dim strHICQ As New StringBuilder
    '    Dim strVHCQ As New StringBuilder

    '    Dim dtTmp As DataTable = Nothing
    '    Dim tmpDate As Date
    '    Dim dtOverall As New DataTable
    '    Dim strTitle() As String = {"Year", "Month", "EXLC", "LSCS", "MSCS", "HICS", "VHIS", "SUS", "NRCQ", "HICQ", "VHCQ"}
    '    Dim dr As DataRow
    '    Dim rowNum As Integer

    '    For i As Integer = 0 To strTitle.Length - 1
    '        dtOverall.Columns.Add(New DataColumn())
    '    Next

    '    'layout
    '    For i As Integer = 0 To 12
    '        dr = dtOverall.NewRow
    '        dtOverall.Rows.Add(dr)
    '    Next

    '    tmpDate = chartDate
    '    For i As Integer = 0 To 12
    '        dtOverall.Rows(i).Item(0) = tmpDate.AddMonths(i).Year.ToString
    '        dtOverall.Rows(i).Item(1) = tmpDate.AddMonths(i).Month.ToString
    '        For j As Integer = 2 To strTitle.Length - 1
    '            dtOverall.Rows(i).Item(j) = "0"
    '        Next
    '    Next

    '    Conn.Open()
    '    'EXLC
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                                "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                                "and wh95.carbon <= " + EXLC_C.ToString & _
    '                                " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                                "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                                "and wh95.carbon <= " + EXLC_C.ToString & _
    '                                " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(2) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'LSCS
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                                "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                                "and wh95.carbon > " + EXLC_C.ToString & " and wh95.tensile <= 40 " & _
    '                                " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                                "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                                "and wh95.carbon > " + EXLC_C.ToString & " and wh95.tensile <= 40 " & _
    '                                " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(3) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'MSCS
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile <= 50 and wh95.tensile > 40 " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile <= 50 and wh95.tensile > 40 " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(4) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'HICS
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile <= 60 and wh95.tensile > 50 " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile <= 60 and wh95.tensile > 50 " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(5) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'VHIS
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile > 60 " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.tensile > 60 " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(6) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'SUS
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.steel_grade_code like '6%' " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.carbon > " + EXLC_C.ToString + " " & _
    '                        "and wh95.steel_grade_code like '6%' " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(7) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'NRCQ
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '6000' and wh95.inspection_code >= '5000' " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '6000' and wh95.inspection_code >= '5000' " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(8) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'HICQ
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '5000' and wh95.inspection_code >= '4000' " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '5000' and wh95.inspection_code >= '4000' " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(9) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    'VHCQ
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                    "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                        "(select SUBSTRING(wh97.shift_date, 1, 4) as PYear, SUBSTRING(wh97.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh97.g_weight) as product_weight from h_pmis_wh97 as wh97, h_pmis_wh95 as wh95 where " & _
    '                        "wh97.shift_date between '{0}' and '{1}' and SUBSTRING(wh97.product_no,1, 7) = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '4000' and wh95.inspection_code >= '2000' " & _
    '                        " Group by SUBSTRING(wh97.shift_date, 1, 4), SUBSTRING(wh97.shift_date, 5, 2)) as A " & _
    '                    "FULL OUTER JOIN " & _
    '                        "(select SUBSTRING(wh9c.shift_date, 1, 4) as PYear, SUBSTRING(wh9c.shift_date, 5, 2) as PMonth, " & _
    '                        "SUM(wh9c.gross_weight) as product_weight from h_pmis_wh9c as wh9c, h_pmis_wh95 as wh95 where " & _
    '                        "shift_date between '{0}' and '{1}' and wh9c.coil_no = wh95.coil_no " & _
    '                        "and wh95.inspection_code < '4000' and wh95.inspection_code >= '2000' " & _
    '                        " Group by SUBSTRING(wh9c.shift_date, 1, 4), SUBSTRING(wh9c.shift_date, 5, 2)) as B " & _
    '                        "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    For i As Integer = 0 To dtTmp.Rows.Count - 1
    '        rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '        dtOverall.Rows(rowNum).Item(10) = Val(dtTmp.Rows(i).Item(2)) / 1000
    '    Next

    '    Conn.Close()

    '    '傳入前台控制項

    '    For i As Integer = 0 To dtOverall.Rows.Count - 1
    '        strEXLC.Append(dtOverall.Rows(i).Item(2).ToString + ",")
    '        strLSCS.Append(dtOverall.Rows(i).Item(3).ToString + ",")
    '        strMSCS.Append(dtOverall.Rows(i).Item(4).ToString + ",")
    '        strHICS.Append(dtOverall.Rows(i).Item(5).ToString + ",")
    '        strVHIS.Append(dtOverall.Rows(i).Item(6).ToString + ",")
    '        strSUS.Append(dtOverall.Rows(i).Item(7).ToString + ",")
    '        strNRCQ.Append(dtOverall.Rows(i).Item(8).ToString + ",")
    '        strHICQ.Append(dtOverall.Rows(i).Item(9).ToString + ",")
    '        strVHCQ.Append(dtOverall.Rows(i).Item(10).ToString + ",")
    '    Next

    '    hEXLC.Value = strEXLC.ToString
    '    hLSCS.Value = strLSCS.ToString
    '    hMSCS.Value = strMSCS.ToString
    '    hHICS.Value = strHICS.ToString
    '    hVHIS.Value = strVHIS.ToString
    '    hSUS.Value = strSUS.ToString
    '    hNRCQ.Value = strNRCQ.ToString
    '    hHICQ.Value = strHICQ.ToString
    '    hVHCQ.Value = strVHCQ.ToString

    'End Sub

    'Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    chartDate = hStartDate.Value.ToString
    '    chartDate = chartDate.AddMonths(-1)
    '    hStartDate.Value = chartDate.ToString("yyyy/MM")
    '    hEndDate.Value = chartDate.AddMonths(12).ToString("yyyy/MM")
    '    Mainprocess()
    '    hAnc.Value = "#Chart"
    'End Sub

    'Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    chartDate = hStartDate.Value.ToString
    '    chartDate = chartDate.AddMonths(1)
    '    hStartDate.Value = chartDate.ToString("yyyy/MM")
    '    hEndDate.Value = chartDate.AddMonths(12).ToString("yyyy/MM")
    '    Mainprocess()
    '    hAnc.Value = "#Chart"
    'End Sub

    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        TNRL2_Table1()
        TNRL2_Table2()
        'TeeChartData1()
        'TeeChartData2()
    End Sub
End Class