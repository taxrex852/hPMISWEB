Imports System.Data.SqlClient

Partial Public Class _2TNRL_Defect
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3203"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

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

    Private Sub TNRL1_Table()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "班", "班", "班"}
        Dim strColName() As String = {"top(1)", "top(2)", "top(3)", "top(4)", "top(5)", "top(1)", "top(2)", "top(3)"}
        Dim strACCESS As String = Nothing

        Dim calTmp As Double

        Dim shift_num As String = "", shift_sym As String = "", shift_sym_c As String = ""
        Dim shift_date(2) As Date

        '1:M 2:A 3:N
        Select Case Now.Hour
            Case 7 To 14 'M
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_num = "231"
                shift_sym_c = "ANM"
            Case 15 To 22 'A
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_num = "312"
                shift_sym_c = "NMA"
            Case 0 To 6 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
            Case 23 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
        End Select

        strDailyTitle(1) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(1)
        strDailyTitle(2) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(3)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        'layout title
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next
        For i As Integer = 0 To strColName.Length - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
            'For j As Integer = 1 To strDailyTitle.Length - 1
            '    dtDataTable.Rows(i).Item(j) = "N/A"
            'Next
        Next

        Conn.Open()
        'reject
        For shift As Integer = 0 To 2
            strACCESS = String.Format("select top 5 code, sum(weight) from (" & _
                                        "select def_code_1 as code,an_weight as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_2 as code,an_weight as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_3 as code,an_weight as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_4 as code,an_weight as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                      ")top5 where code != '' group by code order by sum(weight) desc", _
                                      shift_date(shift).ToString("yyyyMMdd"), _
                                      shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            'fill data
            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    dtDataTable.Rows(i).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(i).Item(shift + 1) = "N/A"
                Next
            End If

            strACCESS = String.Format("select top 3 code, sum(weight) from (" & _
                                        "select cd_code_1 as code,cd_weight_1 as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_2 as code,cd_weight_2 as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_3 as code,cd_weight_3 as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_4 as code,cd_weight_4 as weight from h_pmis_wh97 where shift_date='{0}' and shift_code='{1}'" & _
                                        ")top3 where code != '' group by code order by sum(weight) desc", _
                                      shift_date(shift).ToString("yyyyMMdd"), _
                                      shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            'fill data
            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    dtDataTable.Rows(i + 5).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 2
                    dtDataTable.Rows(i + 5).Item(shift + 1) = "N/A"
                Next
            End If
        Next

        Conn.Close()

        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        gvDaily.Rows(0).Cells(0).Width = 210
        gvDaily.Rows(0).Cells(1).Width = 215
        gvDaily.Rows(0).Cells(2).Width = 215
        gvDaily.Rows(0).Cells(3).Width = 215

        For i As Integer = 0 To 7
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
        Dim strMonthTitle() As String = {" ", "defect top (1)", "defect top (2)", "defect top (3)", "defect top (4)", "defect top (5)", "defect top (1) ", "defect top (2) ", "defect top (3) "}
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
            'For j As Integer = 0 To strMonthTitle.Length - 2
            '    dtDataTable.Rows(idate).Item(j + 1) = "N/A"
            'Next
        Next

        Conn.Open()

        For idate As Integer = 1 To Date.DaysInMonth(Year([Today]), Month([Today]))
            strACCESS = "select top 5 code, sum(weight) from (" & _
                        "select def_code_1 as code,an_weight as weight " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_2,an_weight " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_3,an_weight " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_4,an_weight " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        ")top5 where code != '' group by code order by sum(weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    dtDataTable.Rows(idate - 1).Item(i + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(idate - 1).Item(i + 1) = "N/A"
                Next
            End If

            strACCESS = "select top 3 code, sum(weight) from (" & _
                        "select cd_code_1 as code,cd_weight_1 as weight " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_2,cd_weight_2 " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_3,cd_weight_3 " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_4,cd_weight_4 " & _
                        "from h_pmis_wh97 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        ")top3 where code != '' group by code order by sum(weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    dtDataTable.Rows(idate - 1).Item(i + 6) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 2
                    dtDataTable.Rows(idate - 1).Item(i + 6) = "N/A"
                Next
            End If
        Next

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        'col width
        gvMonth.Rows(0).Cells(0).Width = 60
        For i As Integer = 1 To 8
            gvMonth.Rows(0).Cells(i).Width = 105
        Next

        lblMonth.Text = Date.Today.ToString("MM")
        strACCESS = "select top 5 code, sum(weight) from (" & _
                    "select def_code_1 as code,an_weight as weight " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_2,an_weight " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_3,an_weight " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_4,an_weight " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    ")top5 where code != '' group by code order by sum(weight) desc"
        dtTmp = execQuery(strACCESS, "", Conn)

        lblR1.Text = "N/A"
        lblR2.Text = "N/A"
        lblR3.Text = "N/A"
        lblR4.Text = "N/A"
        lblR5.Text = "N/A"
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(i).Item(0).ToString.Trim.Length <> 0 Then
                    Select Case i
                        Case 0
                            '單位換算
                            lblR1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算
                            lblR2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算
                            lblR3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                        Case 3
                            '單位換算
                            lblR4.Text = dtTmp.Rows(3).Item(0).ToString + "/" + (Val(dtTmp.Rows(3).Item(1).ToString) / 1000).ToString("0.00")
                        Case 4
                            '單位換算
                            lblR5.Text = dtTmp.Rows(4).Item(0).ToString + "/" + (Val(dtTmp.Rows(4).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

        strACCESS = "select top 3 code, sum(weight) from (" & _
                    "select cd_code_1 as code,cd_weight_1 as weight " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_2,cd_weight_2 " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_3,cd_weight_3 " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_4, cd_weight_4 " & _
                    "from h_pmis_wh97 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    ")top3 where code != '' group by code order by sum(weight) desc"
        dtTmp = execQuery(strACCESS, "", Conn)

        lblC1.Text = "N/A"
        lblC2.Text = "N/A"
        lblC3.Text = "N/A"
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(i).Item(0).ToString.Trim.Length <> 0 Then
                    Select Case i
                        Case 0
                            '單位換算
                            lblC1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算
                            lblC2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算
                            lblC3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

        Conn.Close()

    End Sub

    'Private Sub TeeChartData()

    '    'TeeChart data
    '    Dim strDate As New StringBuilder

    '    Dim strR(4) As StringBuilder
    '    Dim strC(2) As StringBuilder
    '    For i As Integer = 0 To strR.Length - 1
    '        strR(i) = New StringBuilder
    '    Next
    '    For i As Integer = 0 To strC.Length - 1
    '        strC(i) = New StringBuilder
    '    Next

    '    Dim tmpDate As Date
    '    tmpDate = chartDate

    '    Dim dtTmp As DataTable = Nothing
    '    Dim adapter As SqlDataAdapter = Nothing

    '    Conn.Open()
    '    For iyear As Integer = 1 To 13
    '        strACCESS = "select top 5 code, sum(weight) from (" & _
    '                    "select def_code_1 as code,an_weight as weight " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_2,an_weight " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_3,an_weight " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_4,an_weight " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    ")top5 where code != '' group by code order by sum(weight) desc"
    '        dtTmp = execQuery(strACCESS, "", Conn)

    '        If dtTmp IsNot Nothing Then
    '            For i As Integer = 0 To dtTmp.Rows.Count - 1
    '                '單位換算
    '                dtTmp.Rows(i).Item(1) = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
    '                strR(i).Append(CType(dtTmp.Rows(i).Item(1), Double).ToString("0.00") + ",")
    '            Next
    '            For i As Integer = dtTmp.Rows.Count To strR.Length - 1
    '                strR(i).Append("0,")
    '            Next
    '        End If

    '        strDate.Append(tmpDate.ToString("MM") + "月,")
    '        tmpDate = tmpDate.AddMonths(1)
    '    Next

    '    tmpDate = chartDate
    '    For iyear As Integer = 1 To 13
    '        strACCESS = "select top 3 code, sum(weight) from (" & _
    '                    "select cd_code_1 as code,cd_weight_1 as weight " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_2,cd_weight_2 " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_3,cd_weight_3 " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_4, cd_weight_4 " & _
    '                    "from h_pmis_wh97 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    ")top3 where code != '' group by code order by sum(weight) desc"
    '        dtTmp = execQuery(strACCESS, "", Conn)

    '        If dtTmp IsNot Nothing Then
    '            For i As Integer = 0 To dtTmp.Rows.Count - 1
    '                '單位換算
    '                dtTmp.Rows(i).Item(1) = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
    '                strC(i).Append(CType(dtTmp.Rows(i).Item(1), Double).ToString("0.00") + ",")
    '            Next
    '            For i As Integer = dtTmp.Rows.Count To strC.Length - 1
    '                strC(i).Append("0,")
    '            Next
    '        End If

    '        strDate.Append(tmpDate.ToString("MM") + "月,")
    '        tmpDate = tmpDate.AddMonths(1)
    '    Next

    '    Conn.Close()

    '    '傳入前台控制項
    '    hDate.Value = strDate.ToString
    '    hR1.Value = strR(0).ToString
    '    hR2.Value = strR(1).ToString
    '    hR3.Value = strR(2).ToString
    '    hR4.Value = strR(3).ToString
    '    hR5.Value = strR(4).ToString
    '    hC1.Value = strC(0).ToString
    '    hC2.Value = strC(1).ToString
    '    hC3.Value = strC(2).ToString
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
        TNRL1_Table()
        SumTable()
        'TeeChartData()
    End Sub
End Class