Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Public Class Offline_packing_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3105"
    Private Conn As SqlConnection
    Private strACCESS As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ' 設定標題
            setTitle(Me, PAGE_ID)
            
            ' 讀取近一年趨勢資料 (ECharts)
            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = SqlDataSource1.Select(args1)
            Dim count As Integer = DR1.Count
            
            If count > 0 Then
                LabelStartdate.Text = Format(CDate(DR1(0)(0).ToString), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)(0).ToString), "yyyy/MM")
                
                ' 準備 ECharts 所需的 xAxis 與 pa 資料列
                Dim xAxis As New List(Of String)()
                Dim pa As New List(Of Double)()
                
                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & Format(CDate(DR1(i)("process_date").ToString()), "yyyy/MM") & "'")
                    pa.Add(Convert.ToDouble(DR1(i)("PA")))
                Next
                
                ' 注入 ECharts JSON 資料至前端 JS
                Dim script As String = "var chartData = {" & _
                    "xAxis: [" & String.Join(",", xAxis) & "]," & _
                    "pa: [" & String.Join(",", pa) & "]" & _
                "};"
                
                ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
            End If
            
            Mainprocess()
        End If
    End Sub

    ''' <summary>
    ''' 離線包裝 三班日報表 (gvDaily)
    ''' </summary>
    Private Sub Offline_packing_Table()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        
        Dim strDailyTitle() As String = {" ", "單位", "班", "班", "班"}
        Dim strColName() As String = {"產量 (PA)"}
        Dim strUnitName() As String = {"MT"}
        
        Dim shift_sym As String = ""
        Dim shift_sym_c As String = ""
        Dim shift_date(2) As Date
        Dim tmpPA(2) As Double

        ' 依目前時間判斷三班排列順序
        Select Case Now.Hour
            Case 7 To 14 'M班 (早班)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_sym_c = "ANM"
            Case 15 To 22 'A班 (中班)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_sym_c = "NMA"
            Case 0 To 6 'N班 (夜班)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
            Case 23 'N班 (夜班)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
        End Select

        ' 組合表頭（含日期+班別）
        strDailyTitle(2) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(3)
        strDailyTitle(4) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(4)
        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next

        dr = dtDataTable.NewRow
        dtDataTable.Rows.Add(dr)
        dtDataTable.Rows(0).Item(0) = strColName(0)
        dtDataTable.Rows(0).Item(1) = strUnitName(0)

        Conn.Open()
        
        ' 查詢三班產量 (Weight)
        For shift As Integer = 0 To 2
            strACCESS = "SELECT SUM(Weight) FROM h_pmis_ys01 " & _
                        "WHERE (Move_Kind ='G' OR Move_Kind ='H' OR Move_Kind ='I' OR Move_Kind ='J') " & _
                        "AND ShiftCode = '" + shift_sym_c(shift) + "' " & _
                        "AND SUBSTRING(CONVERT(char, process_date, 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows.Count > 0 AndAlso Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
                    dtDataTable.Rows(0).Item(shift + 2) = dtTmp.Rows(0).Item(0).ToString
                Else
                    dtDataTable.Rows(0).Item(shift + 2) = "0"
                End If
                dtTmp.Dispose()
            Else
                dtDataTable.Rows(0).Item(shift + 2) = "N/A"
            End If
            tmpPA(shift) = IIf(dtDataTable.Rows(0).Item(shift + 2).ToString = "N/A", 0, dtDataTable.Rows(0).Item(shift + 2))
        Next
        
        Conn.Close()

        ' 單位換算：kg ➔ MT（除以 1000）
        dtDataTable.Rows(0).Item(2) = (Val(dtDataTable.Rows(0).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(3) = (Val(dtDataTable.Rows(0).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(4) = (Val(dtDataTable.Rows(0).Item(4).ToString) / 1000).ToString("0.00")

        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        
        ' 設定欄寬
        gvDaily.Rows(0).Cells(0).Width = 200
        gvDaily.Rows(0).Cells(1).Width = 60
        gvDaily.Rows(0).Cells(2).Width = 180
        gvDaily.Rows(0).Cells(3).Width = 180
        gvDaily.Rows(0).Cells(4).Width = 180

        ' 最右欄（目前班別）套用高亮醒目樣式
        gvDaily.Rows(0).Cells(4).CssClass = "irondata0"
    End Sub

    ''' <summary>
    ''' 本月每日 KPI 累計報表 (gvMonth)
    ''' </summary>
    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "PA/MT"}
        Dim sumPA As Integer = 0

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        ' 建立本月每日預設列
        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Date.Today), Month(Date.Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow()
            dtDataTable.Rows.Add(dr)
        Next

        For idate As Integer = 0 To daysInMonth - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
            dtDataTable.Rows(idate).Item(1) = "0"
        Next

        Conn.Open()

        ' 查詢本月每日產量累計
        strACCESS = "SELECT SUBSTRING(CONVERT(char, process_date, 112), 7, 2), SUM(Weight) " & _
                    "FROM h_pmis_ys01 " & _
                    "WHERE (Move_Kind ='G' OR Move_Kind ='H' OR Move_Kind ='I' OR Move_Kind ='J') " & _
                    "And SUBSTRING(CONVERT(char, process_date, 112), 1, 4) = '" + Date.Today.ToString("yyyy") + "' " & _
                    "And SUBSTRING(CONVERT(char, process_date, 112), 5, 2) = '" + Date.Today.ToString("MM") + "' " & _
                    "GROUP BY SUBSTRING(CONVERT(char, process_date, 112), 7, 2)"
        
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If Not IsDBNull(dtTmp.Rows(iCount).Item(0)) AndAlso Not IsDBNull(dtTmp.Rows(iCount).Item(1)) Then
                    Dim dayIndex As Integer = Convert.ToInt32(dtTmp.Rows(iCount).Item(0)) - 1
                    If dayIndex >= 0 AndAlso dayIndex < daysInMonth Then
                        dtDataTable.Rows(dayIndex).Item(1) = dtTmp.Rows(iCount).Item(1)
                        sumPA += Convert.ToInt32(dtTmp.Rows(iCount).Item(1))
                    End If
                End If
            Next
            dtTmp.Dispose()
        End If
        
        Conn.Close()

        lblPA.Text = sumPA.ToString

        ' 單位換算：kg ➔ MT（除以 1000）
        For idate As Integer = 0 To daysInMonth - 1
            dtDataTable.Rows(idate).Item(1) = (Val(dtDataTable.Rows(idate).Item(1).ToString) / 1000).ToString("0.00")
        Next
        lblPA.Text = (Val(lblPA.Text) / 1000).ToString("0.00")

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        ' 設定欄寬
        gvMonth.Rows(0).Cells(0).Width = 200
        gvMonth.Rows(0).Cells(1).Width = 120

        lblMonth.Text = Date.Today.ToString("MM")
    End Sub

    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        Offline_packing_Table()
        SumTable()
    End Sub
End Class