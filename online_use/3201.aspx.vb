Imports System.Data.SqlClient
Imports System.Collections.Generic






Partial Public Class HSM_Defect
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3201"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            setTitle(Me, PAGE_ID)


            Dim args1 As New DataSourceSelectArguments()
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count

                LabelStartdate.Text = Format(CDate(DR1(0)("product_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("product_date").ToString()), "yyyy/MM")


                Dim xAxis As New List(Of String)()
                Dim d1 As New List(Of Double)()
                Dim d2 As New List(Of Double)()
                Dim d3 As New List(Of Double)()
                Dim d4 As New List(Of Double)()
                Dim d5 As New List(Of Double)()

                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & Convert.ToDateTime(DR1(i)("product_date")).ToString("yyyy/MM") & "'")

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
            Case 7 To 14
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym_c = "中夜早"
                shift_sym = "ANM"
                shift_num = "231"
            Case 15 To 22
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym_c = "夜早中"
                shift_sym = "NMA"
                shift_num = "312"
            Case 0 To 6
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "MAN"
                shift_num = "123"
            Case 23
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "MAN"
                shift_num = "123"
        End Select


        strDailyTitle(1) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym_c(0) + strDailyTitle(1)
        strDailyTitle(2) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym_c(1) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym_c(2) + strDailyTitle(3)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")

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
            If shift_sym(shift) = "N" Then
                strACCESS = "select top 5 code, sum(weight) from (" &
                            "select no1_code as code,coil_wm as weight  " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no2_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no3_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no4_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no5_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            ")top5 where code != '' group by code order by sum(weight) desc"
            Else
                strACCESS = "select top 5 code, sum(weight) from (" &
                            "select no1_code as code,coil_wm as weight  " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no2_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no3_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no4_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            " union " &
                            "select no5_code,coil_wm " &
                            "from h_pmis_whqh where product_date+product_time between " + shift_date(shift).ToString("yyyyMMddHHmmss") + " and " + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") +
                            ")top5 where code != '' group by code order by sum(weight) desc"
            End If

            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1

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


        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next


        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
        Next
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
        Next

        Conn.Open()


        For idate As Integer = 1 To Date.DaysInMonth(Year([Today]), Month([Today]))
            strACCESS = "select top 5 code, sum(weight) from (" &
                        "select no1_code as code,coil_wm as weight " &
                        "from h_pmis_whqh where product_date = " + Date.Today.ToString("yyyyMM") + idate.ToString("d2") +
                        " union " &
                        "select no2_code,coil_wm " &
                        "from h_pmis_whqh where product_date = " + Date.Today.ToString("yyyyMM") + idate.ToString("d2") +
                        " union " &
                        "select no3_code,coil_wm " &
                        "from h_pmis_whqh where product_date = " + Date.Today.ToString("yyyyMM") + idate.ToString("d2") +
                        " union " &
                        "select no4_code,coil_wm " &
                        "from h_pmis_whqh where product_date = " + Date.Today.ToString("yyyyMM") + idate.ToString("d2") +
                        " union " &
                        "select no5_code,coil_wm " &
                        "from h_pmis_whqh where product_date = " + Date.Today.ToString("yyyyMM") + idate.ToString("d2") +
                        ")top5 where code != '' group by code order by sum(weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1

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


        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next

        lblMonth.Text = Date.Today.ToString("MM")

        strACCESS = "select top 5 code, sum(weight) from (" &
                    "select no1_code as code,coil_wm as weight " &
                    "from h_pmis_whqh where (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and (Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    " union " &
                    "select no2_code,coil_wm " &
                    "from h_pmis_whqh where (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and (Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    " union " &
                    "select no3_code,coil_wm " &
                    "from h_pmis_whqh where (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and (Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    " union " &
                    "select no4_code,coil_wm " &
                    "from h_pmis_whqh where (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and (Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    " union " &
                    "select no5_code,coil_wm " &
                    "from h_pmis_whqh where (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and (Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    ")top5 where code != '' group by code order by sum(weight) desc"
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

                            lblDT1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1

                            lblDT2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2

                            lblDT3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                        Case 3

                            lblDT4.Text = dtTmp.Rows(3).Item(0).ToString + "/" + (Val(dtTmp.Rows(3).Item(1).ToString) / 1000).ToString("0.00")
                        Case 4

                            lblDT5.Text = dtTmp.Rows(4).Item(0).ToString + "/" + (Val(dtTmp.Rows(4).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

    End Sub

    Private Sub Mainprocess()

        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        HSMTable()
        SumTable()
    End Sub
End Class