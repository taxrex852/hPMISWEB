Imports System.Data.SqlClient

Partial Public Class HSM_Delay
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3301"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)
            chartDate = Date.Today.AddMonths(-12)
            hStartDate.Value = chartDate.ToString("yyyy/MM")
            hEndDate.Value = Date.Today.ToString("yyyy/MM")
            Mainprocess()
        End If
        hAnc.Value = ""
    End Sub

    Private Sub HSMTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "班", "班", "班"}
        Dim strColName() As String = {"設備延誤 (DA)", "換輥延誤 (DR)", "計畫性延誤 (DS)", "其他延誤 (DO)", "總計"}
        Dim strACCESS As String = Nothing

        Dim timeTmp, minTmp As Integer

        Dim shift_num As String = "", shift_sym As String = ""
        Dim shift_date(2) As Date

        '1:M 2:A 3:N
        'Select Case Now.Hour
        '    Case 7 To 14 'M
        '        shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
        '        shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 23:00:00")
        '        shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
        '        shift_sym = "中夜早"
        '        shift_num = "231"
        '    Case 15 To 22 'A
        '        shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 23:00:00")
        '        shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
        '        shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
        '        shift_sym = "夜早中"
        '        shift_num = "312"
        '    Case 0 To 6 'N
        '        shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
        '        shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
        '        shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 23:00:00")
        '        shift_sym = "早中夜"
        '        shift_num = "123"
        '    Case 23 'N
        '        shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
        '        shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
        '        shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
        '        shift_sym = "早中夜"
        '        shift_num = "123"
        'End Select
        Select Case Now.Hour
            Case 7 To 14 'M
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_num = "231"
            Case 15 To 22 'A
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_num = "312"
            Case 0 To 6 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
            Case 23 'N
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
        End Select

        strDailyTitle(1) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(1)
        strDailyTitle(2) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(3)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        'layout title
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next
        For i As Integer = 0 To 4
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
        Next

        Conn.Open()
        'fill data (DA,DR,DS,DO)
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " & _
                        "SUM(acci_delay_no), SUM(acci_delay_time)," & _
                        "SUM(roll_delay_no), SUM(roll_delay_time)," & _
                        "SUM(shutdown_time_no), SUM(shutdown_time)," & _
                        "SUM(others_delay_no), SUM(others_delay_time) " & _
                        "FROM h_pmis_si01 " & _
                        "WHERE line_id = 1 AND shift = " + shift_num(shift) + " " & _
                        "AND select_dates ='" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            timeTmp = 0
            minTmp = 0
            For i As Integer = 0 To 3
                If dtTmp IsNot Nothing Then
                    If dtTmp.Rows(0).Item(i * 2) Is DBNull.Value Then
                        dtDataTable.Rows(i).Item(shift + 1) = "0次/0分"
                    Else
                        dtDataTable.Rows(i).Item(shift + 1) = dtTmp.Rows(0).Item(i * 2).ToString + "次/" + dtTmp.Rows(0).Item(i * 2 + 1).ToString + "分"
                        timeTmp += dtTmp.Rows(0).Item(i * 2)
                        minTmp += dtTmp.Rows(0).Item(i * 2 + 1)
                    End If
                Else
                    dtDataTable.Rows(i).Item(shift + 1) = "0次/0分"
                End If

            Next
            'sum
            dtDataTable.Rows(4).Item(shift + 1) = timeTmp.ToString + "次/" + minTmp.ToString + "分"
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
        Dim dtdatatable1 As New DataTable
        Dim dtdatatable2 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "DA", "DR", "Shutdown-time", "DO", "Total"}
        Dim adapter As SqlDataAdapter = Nothing

        Dim timeTmp, minTmp, iday As Integer
        Dim totaltimeTmp, totalminTmp As Integer

        'Month delay collection
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
            For j As Integer = 0 To 4
                dtDataTable.Rows(idate).Item(j + 1) = "0次/0分"
            Next
        Next

        Conn.Open()

        strACCESS = "SELECT Day(select_dates)," & _
                    "SUM(acci_delay_no), SUM(acci_delay_time)," & _
                    "SUM(roll_delay_no), SUM(roll_delay_time)," & _
                    "SUM(shutdown_time_no), SUM(shutdown_time)," & _
                    "SUM(others_delay_no), SUM(others_delay_time) " & _
                    "FROM h_pmis_si01 " & _
                    "WHERE line_id = 1 " & _
                    "And (Year(select_dates) = " + Date.Today.ToString("yyyy") + ") and " & _
                    "(Month(select_dates) = " + Date.Today.ToString("MM") + ") " & _
                    "GROUP BY Day(select_dates)"
        dtTmp = execQuery(strACCESS, "", Conn)

        For iCount As Integer = 0 To dtTmp.Rows.Count - 1
            If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                timeTmp = 0
                minTmp = 0
                iday = dtTmp.Rows(iCount).Item(0) - 1
                For j As Integer = 0 To 3
                    dtDataTable.Rows(iday).Item(j + 1) = dtTmp.Rows(iCount).Item(j * 2 + 1).ToString + "次/" + dtTmp.Rows(iCount).Item((j + 1) * 2).ToString + "分"
                    timeTmp += dtTmp.Rows(iCount).Item(j * 2 + 1)
                    minTmp += dtTmp.Rows(iCount).Item((j + 1) * 2)
                Next
                'sum
                dtDataTable.Rows(iday).Item(5) = timeTmp.ToString + "次/" + minTmp.ToString + "分"
            End If
        Next

        Conn.Close()

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        'col width
        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next

        'Sum Month
        lblMonth.Text = Date.Today.ToString("MM")
        totaltimeTmp = 0
        totalminTmp = 0
        For j As Integer = 0 To 3
            timeTmp = 0
            minTmp = 0
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                timeTmp += dtTmp.Rows(iCount).Item(j * 2 + 1)
                minTmp += dtTmp.Rows(iCount).Item((j + 1) * 2)
            Next
            Select Case j
                Case 0
                    lblDA.Text = timeTmp.ToString + "次/" + minTmp.ToString + "分"
                Case 1
                    lblDR.Text = timeTmp.ToString + "次/" + minTmp.ToString + "分"
                Case 2
                    lblDS.Text = timeTmp.ToString + "次/" + minTmp.ToString + "分"
                Case 3
                    lblDO.Text = timeTmp.ToString + "次/" + minTmp.ToString + "分"
            End Select
            totaltimeTmp += timeTmp
            totalminTmp += minTmp
        Next
        lblTotal.Text = totaltimeTmp.ToString + "次/" + totalminTmp.ToString + "分"

    End Sub

    Private Sub TeeChartData()
        'TeeChart data
        Dim strDate As New StringBuilder
        Dim strDA1 As New StringBuilder
        Dim strDR1 As New StringBuilder
        Dim strDS1 As New StringBuilder
        Dim strDO1 As New StringBuilder
        Dim strTotal1 As New StringBuilder
        Dim strDA2 As New StringBuilder
        Dim strDR2 As New StringBuilder
        Dim strDS2 As New StringBuilder
        Dim strDO2 As New StringBuilder
        Dim strTotal2 As New StringBuilder

        Dim dtTmp As DataTable = Nothing
        Dim timeTmp, minTmp As Integer
        Dim tmpDate As Date

        tmpDate = chartDate

        Conn.Open()
        For iyear As Integer = 1 To 13
            strACCESS = "SELECT " & _
                        "SUM(acci_delay_no), SUM(acci_delay_time)," & _
                        "SUM(roll_delay_no), SUM(roll_delay_time)," & _
                        "SUM(shutdown_time_no), SUM(shutdown_time)," & _
                        "SUM(others_delay_no), SUM(others_delay_time) " & _
                        "FROM h_pmis_si01 " & _
                        "WHERE line_id = 1 " & _
                        "And (Year(select_dates) = " + tmpDate.ToString("yyyy") + ") and " & _
                        "(Month(select_dates) = " + tmpDate.ToString("MM") + ")"
            dtTmp = execQuery(strACCESS, "", Conn)

            timeTmp = 0
            minTmp = 0
            For i As Integer = 0 To 3
                If dtTmp.Rows(0).Item(i * 2) IsNot DBNull.Value Then
                    timeTmp += dtTmp.Rows(0).Item(i * 2)
                    minTmp += dtTmp.Rows(0).Item(i * 2 + 1)
                End If
            Next
            strTotal1.Append(timeTmp.ToString + ",")
            strTotal2.Append(minTmp.ToString + ",")

            If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                strDA1.Append("0,")
                strDA2.Append("0,")
            Else
                strDA1.Append(dtTmp.Rows(0).Item(0).ToString + ",")
                strDA2.Append(dtTmp.Rows(0).Item(1).ToString + ",")
            End If


            If dtTmp.Rows(0).Item(2) Is DBNull.Value Then
                strDR1.Append("0,")
                strDR2.Append("0,")
            Else
                strDR1.Append(dtTmp.Rows(0).Item(2).ToString + ",")
                strDR2.Append(dtTmp.Rows(0).Item(3).ToString + ",")
            End If


            If dtTmp.Rows(0).Item(4) Is DBNull.Value Then
                strDS1.Append("0,")
                strDS2.Append("0,")
            Else
                strDS1.Append(dtTmp.Rows(0).Item(4).ToString + ",")
                strDS2.Append(dtTmp.Rows(0).Item(5).ToString + ",")
            End If

            If dtTmp.Rows(0).Item(6) Is DBNull.Value Then
                strDO1.Append("0,")
                strDO2.Append("0,")
            Else
                strDO1.Append(dtTmp.Rows(0).Item(6).ToString + ",")
                strDO2.Append(dtTmp.Rows(0).Item(7).ToString + ",")
            End If

            strDate.Append(tmpDate.ToString("MM") + "月,")
            tmpDate = tmpDate.AddMonths(1)
        Next

        Conn.Close()

        '傳入前台控制項
        hDate.Value = strDate.ToString
        hDA1.Value = strDA1.ToString
        hDR1.Value = strDR1.ToString
        hDS1.Value = strDS1.ToString
        hDO1.Value = strDO1.ToString
        hTotal1.Value = strTotal1.ToString

        hDA2.Value = strDA2.ToString
        hDR2.Value = strDR2.ToString
        hDS2.Value = strDS2.ToString
        hDO2.Value = strDO2.ToString
        hTotal2.Value = strTotal2.ToString

    End Sub

    Protected Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        chartDate = hStartDate.Value.ToString
        chartDate = chartDate.AddMonths(-1)
        hStartDate.Value = chartDate.ToString("yyyy/MM")
        hEndDate.Value = chartDate.AddMonths(12).ToString("yyyy/MM")
        Mainprocess()
        hAnc.Value = "#Chart"
    End Sub

    Protected Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        chartDate = hStartDate.Value.ToString
        chartDate = chartDate.AddMonths(1)
        hStartDate.Value = chartDate.ToString("yyyy/MM")
        hEndDate.Value = chartDate.AddMonths(12).ToString("yyyy/MM")
        Mainprocess()
        hAnc.Value = "#Chart"
    End Sub

    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        HSMTable()
        SumTable()
        TeeChartData()
    End Sub
End Class