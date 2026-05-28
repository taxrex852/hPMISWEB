Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Public Class HBM_Defect
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3206"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)

            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                LabelStartdate.Text = Format(CDate(DR1(0)("boundle_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("boundle_date").ToString()), "yyyy/MM")

                ' --- ECharts 格式資料 (Top 1 ~ Top 5) ---
                Dim xAxis As New List(Of String)()
                Dim d1 As New List(Of Double)()
                Dim d2 As New List(Of Double)()
                Dim d3 As New List(Of Double)()
                Dim d4 As New List(Of Double)()
                Dim d5 As New List(Of Double)()

                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & Convert.ToDateTime(DR1(i)("boundle_date")).ToString("yyyy/MM") & "'")
                    d1.Add(If(IsDBNull(DR1(i)("def_top1")), 0, Convert.ToDouble(DR1(i)("def_top1"))))
                    d2.Add(If(IsDBNull(DR1(i)("def_top2")), 0, Convert.ToDouble(DR1(i)("def_top2"))))
                    d3.Add(If(IsDBNull(DR1(i)("def_top3")), 0, Convert.ToDouble(DR1(i)("def_top3"))))
                    d4.Add(If(IsDBNull(DR1(i)("def_top4")), 0, Convert.ToDouble(DR1(i)("def_top4"))))
                    d5.Add(If(IsDBNull(DR1(i)("def_top5")), 0, Convert.ToDouble(DR1(i)("def_top5"))))
                Next

                Dim script As String = "var chartData = {" &
                    "xAxis: [" & String.Join(",", xAxis) & "]," &
                    "d1: [" & String.Join(",", d1) & "]," &
                    "d2: [" & String.Join(",", d2) & "]," &
                    "d3: [" & String.Join(",", d3) & "]," &
                    "d4: [" & String.Join(",", d4) & "]," &
                    "d5: [" & String.Join(",", d5) & "]" &
                "};"

                ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
            End If

            Mainprocess()
        End If
    End Sub


    Private Sub HSMTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "班", "班", "班"}
        Dim strColName() As String = {"缺陷 top(1)", "缺陷 top(2)", "缺陷 top(3)", "缺陷 top(4)", "缺陷 top(5)"}
        Dim strACCESS As String = Nothing
        Dim shift_sym_c As String = ""

        Dim calTmp As Double

        Dim shift_num As String = "", shift_sym As String = ""
        Dim shift_date(2) As Date



        Select Case Now.Hour
            Case 7 To 14 'M
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym_c = "中夜早"
                shift_sym = "KQ8"
                shift_num = "231"
            Case 15 To 22 'A
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                'shift_sym = "夜早中"
                shift_sym_c = "夜早中"
                shift_sym = "Q8K"
                shift_num = "312"
            Case 0 To 6 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "8KQ"
                shift_num = "123"
            Case 23 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "8KQ"
                shift_num = "123"
        End Select

        strDailyTitle(1) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym_c(0) + strDailyTitle(1)
        strDailyTitle(2) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym_c(1) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym_c(2) + strDailyTitle(3)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        'layout title
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next
        For i As Integer = 0 To strColName.Length - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
        Next

        Conn.Open()

        For shift As Integer = 0 To 2
            strACCESS = "select top 5 Defect_Code_of_This_Bundle, sum(bound_weight) from " &
                        "h_pmis_hbm_info where Defect_Code_of_This_Bundle <> '000' and Defect_Code_of_This_Bundle <> '' and " &
                        "boundle_shift_No = '" + shift_sym(shift) + "' " &
                        "AND SUBSTRING(CONVERT(char, boundle_date, 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'" &
                        "  group by Defect_Code_of_This_Bundle order by sum(bound_weight) desc"

            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    'calTmp = dtTmp.Rows(i).Item(1)
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    dtDataTable.Rows(i).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(i).Item(shift + 1) = "N/A"
                Next
            End If
        Next

        Conn.Close()

        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        gvDaily.Rows(0).Cells(0).Width = 200
        gvDaily.Rows(0).Cells(1).Width = 200
        gvDaily.Rows(0).Cells(2).Width = 200
        gvDaily.Rows(0).Cells(3).Width = 200

        For i As Integer = 0 To 4
            gvDaily.Rows(i).Cells(3).CssClass = "irondata0"
        Next

    End Sub

    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtDataTable1 As New DataTable
        Dim dtDataTable2 As New DataTable
        Dim dtDataTable3 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "defect top (1)", "defect top (2)", "defect top (3)", "defect top (4)", "defect top (5)"}
        Dim adapter As SqlDataAdapter = Nothing

        Dim calTmp As Double

        'Month produce record
        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        'layout
        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
        Next
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
        Next

        Conn.Open()

        For idate As Integer = 1 To Date.DaysInMonth(Year([Today]), Month([Today]))
            strACCESS = "select top 5  Defect_Code_of_This_Bundle, sum(bound_weight) " &
                        "from h_pmis_hbm_info where Defect_Code_of_This_Bundle <> '000' and Defect_Code_of_This_Bundle <> '' and " &
                        "SUBSTRING(CONVERT(char, boundle_date, 112), 1, 8) = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" &
                        " group by Defect_Code_of_This_Bundle order by sum(bound_weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    'calTmp = dtTmp.Rows(i).Item(1)
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    dtDataTable.Rows(idate - 1).Item(i + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(idate - 1).Item(i + 1) = "N/A"
                Next
            End If
        Next

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        'col width
        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next

        lblMonth.Text = Date.Today.ToString("MM")
        strACCESS = "select top 5 Defect_Code_of_This_Bundle, sum(bound_weight) " &
                 "from h_pmis_hbm_info where Defect_Code_of_This_Bundle <> '000' and Defect_Code_of_This_Bundle <> '' and " &
                 "(Year(boundle_date) = " + Date.Today.ToString("yyyy") + ") and (Month(boundle_date) = " + Date.Today.ToString("MM") + ") " &
                 "group by Defect_Code_of_This_Bundle order by sum(bound_weight) desc"
        dtTmp = execQuery(strACCESS, "", Conn)

        Conn.Close()

        lblDT1.Text = "N/A"
        lblDT2.Text = "N/A"
        lblDT3.Text = "N/A"
        lblDT4.Text = "N/A"
        lblDT5.Text = "N/A"

        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(i).Item(0).ToString.Trim.Length <> 0 Then
                    Select Case i
                        Case 0
                            '單位換算
                            lblDT1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算
                            lblDT2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算
                            lblDT3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                        Case 3
                            '單位換算
                            lblDT4.Text = dtTmp.Rows(3).Item(0).ToString + "/" + (Val(dtTmp.Rows(3).Item(1).ToString) / 1000).ToString("0.00")
                        Case 4
                            '單位換算
                            lblDT5.Text = dtTmp.Rows(4).Item(0).ToString + "/" + (Val(dtTmp.Rows(4).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

    End Sub



    Private Sub Mainprocess()
        Conn = New SqlConnection(getHBMConnStr(Application("HBMConnStr"))) '1091229 新增HBMPMIS連線字串
        HSMTable()
        SumTable()
        'TeeChartData()
    End Sub
End Class