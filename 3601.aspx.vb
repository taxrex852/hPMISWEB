Imports System.Data.SqlClient

Partial Public Class HSM_Stock1
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3601"
    Private Conn As SqlConnection
    Private Cmd As SqlCommand
    Dim reader As SqlDataReader = Nothing
    Private strACCESS As String
    Private chartDate As Date
    Public xAxisData_Weekly As String = "[]"
    Public seriesData_In As String = "[]"
    Public seriesData_Out As String = "[]"
    Public seriesData_Diff As String = "[]"
    Public xAxisData_TodayRcv As String = "[]"
    Public seriesData_TodayRcv As String = "[]"
    Public xAxisData_TodayUsed As String = "[]"
    Public seriesData_TodayUsed As String = "[]"
    Public xAxisData_Storage As String = "[]"
    Public seriesData_Storage As String = "[]"
    Public seriesData_Stock As String
    Public js_Raw_HotRoll As String, js_Raw_Ready As String, js_Raw_Wait As String
    Public js_Raw_Heavy As String, js_Raw_Return As String, js_Raw_Test As String, js_Raw_Empty As String

    Public js_Pct_HotRoll As String, js_Pct_Ready As String, js_Pct_Wait As String
    Public js_Pct_Heavy As String, js_Pct_Return As String, js_Pct_Test As String, js_Pct_Empty As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Page.IsPostBack = False Then

            BindEChartsData()
            BindEChartsData_TodayRcv()
            BindEChartsData_TodayUsed()
            BindEChartsData_Storage()
            BindEChartsData_slab_Nextcode()
            Dim adapter As SqlDataAdapter = Nothing
            Dim dtDataTable As New DataTable
            Dim dtLimit As New DataTable
            Dim dr As DataRow
            Dim strTitle() As String = {"儲區代號", "Y1", "Y2", "Y3", "Y4", "H/C(保溫坑)", "總量"}
            Dim strColName() As String = {"設計數量(堆)", "庫存數量(塊)", "標準容量(MT)", "庫存重量(MT)", "剩餘容量(MT)"}
            Dim dtTmp As DataTable = Nothing
            Dim totalTmp As Integer
            Dim calTmp As Double

            Dim strlimit As New StringBuilder
            Dim strTC As New StringBuilder


            '設定Title
            setTitle(Me, PAGE_ID)
            Conn = New SqlConnection(getConnStr(Application("ConnStr")))
            Conn.Open()

            'limit 
            strACCESS = "SELECT stock_limit FROM sys_stock_limit WHERE stock = 'symc' ORDER BY stock_level asc"
            dtTmp = execQuery(strACCESS, "", Conn)

            'layout
            For i As Integer = 0 To 5
                dtLimit.Columns.Add(New DataColumn())
            Next
            dr = dtLimit.NewRow
            dtLimit.Rows.Add(dr)
            dtLimit.Rows(0).Item(0) = "滿儲率門檻(%)"

            'hc,y1,y2,y3,y4,y5 -> y1,y2,y3,y4,y5,hc
            For i As Integer = 1 To dtTmp.Rows.Count - 1
                calTmp = dtTmp.Rows(i).Item(0) / 10
                strlimit.Append(calTmp.ToString("F1") + ",")
                dtLimit.Rows(0).Item(i) = calTmp.ToString("F1")
            Next
            calTmp = dtTmp.Rows(0).Item(0) / 10
            strlimit.Append(calTmp.ToString("F1"))
            dtLimit.Rows(0).Item(5) = calTmp.ToString("F1")
            'undefined
            strlimit.Append(",0,0,")


            'table
            strACCESS = "SELECT top(1) " &
                            "y1_design_weight,y2_design_weight,y3_design_weight,y4_design_weight,hc_design_weight," &
                            "y1_stock_num,y2_stock_num,y3_stock_num,y4_stock_num,hc_stock_num," &
                            "y1_standard,y2_standard,y3_standard,y4_standard,hc_standard," &
                            "y1_stock_weight,y2_stock_weight,y3_stock_weight,y4_stock_weight,hc_stock_weight," &
                            "y1_standard-y1_stock_weight,y2_standard-y2_stock_weight,y3_standard-y3_stock_weight,y4_standard-y4_stock_weight,hc_standard-hc_stock_weight," &
                            "y1_orate,y2_orate,y3_orate,y4_orate,hc_orate, process_date " &
                            "FROM h_pmis_isyh " &
                            "ORDER BY process_date DESC"
            dtTmp = execQuery(strACCESS, "", Conn)

            'layout
            For i As Integer = 0 To strTitle.Length - 1
                dtDataTable.Columns.Add(New DataColumn(strTitle(i)))
            Next
            For i As Integer = 0 To strColName.Length - 1
                dr = dtDataTable.NewRow
                dtDataTable.Rows.Add(dr)
                dtDataTable.Rows(i).Item(0) = strColName(i)
            Next

            ' i : 0 - 設計數量， 1 - 庫存數量，2 - 庫存重量，3 - 標準容量，4- 剩餘容量
            ' j : 0 - Y1, 1 - Y2, 2 - Y3, 3 - Y4, 4 - HC
            For i As Integer = 0 To 4
                totalTmp = 0
                For j As Integer = 0 To 4
                    If dtTmp.Rows.Count = 0 Then
                        dtDataTable.Rows(i).Item(j + 1) = "0"
                    Else
                        dtDataTable.Rows(i).Item(j + 1) = dtTmp.Rows(0).Item(i * 5 + j).ToString
                        totalTmp += dtTmp.Rows(0).Item(i * 5 + j)
                    End If
                Next
                dtDataTable.Rows(i).Item(6) = totalTmp.ToString
            Next


            gvStock.DataSource = dtDataTable
            gvStock.DataBind()
            gvStock.Rows(0).Cells(0).Width = 143
            gvStock.Rows(0).Cells(6).Width = 80
            For i As Integer = 1 To 6
                gvStock.Rows(0).Cells(i).Width = 103
            Next

            For Each row As GridViewRow In gvStock.Rows
                row.Cells(0).ForeColor = System.Drawing.Color.Black
                row.Cells(6).ForeColor = System.Drawing.Color.Black
            Next



            gvStock.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Black

            For i As Integer = 1 To 6
                gvStock.Rows(4).Cells(i).ForeColor = System.Drawing.Color.Black
            Next

            'teechart data
            For i As Integer = 0 To 4
                If dtTmp.Rows.Count = 0 Then
                    strTC.Append("0,")
                Else
                    calTmp = dtTmp.Rows(0).Item(25 + i) / 10
                    strTC.Append(calTmp.ToString("F1") + ",")
                End If
            Next

            'undefined
            strTC.Append("0,0,")
            'hTC.Value = strTC.ToString

            Conn.Close()
        End If
    End Sub

    Protected Sub GridView4_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView4.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)

            If rowView IsNot Nothing Then


                Dim item As String = e.Row.Cells(0).Text.Replace("&nbsp;", "").Trim()

                If item = "合計" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDB062")
                    e.Row.ForeColor = System.Drawing.Color.Black
                    e.Row.Font.Bold = True
                End If

            End If
        End If
    End Sub

    Protected Sub GridView6_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView6.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)

            If rowView IsNot Nothing Then

                ' 抓取第一格的文字並去除空白字元
                Dim item As String = e.Row.Cells(0).Text.Replace("&nbsp;", "").Trim()

                If item = "合計" Then
                    ' 1. 背景改為藍色 (對應你的 TeeChart)
                    e.Row.BackColor = System.Drawing.Color.Blue ' 或使用 ColorTranslator.FromHtml("#0000FF")

                    ' 2. 字體改為純白 (最高對比，最清晰)
                    e.Row.ForeColor = System.Drawing.Color.White

                    ' 3. 字體加粗
                    e.Row.Font.Bold = True
                End If

            End If
        End If
    End Sub

    Protected Sub GridView5_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView5.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' 抓取第 1 格 (類別) 與第 2 格 (項目) 的文字，並過濾掉 HTML 空白字元
            Dim category As String = e.Row.Cells(0).Text.Replace("&nbsp;", "").Trim()

            Dim item As String = ""
            ' 確保這列至少有兩格以上才讀取，避免出錯
            If e.Row.Cells.Count > 1 Then
                item = e.Row.Cells(1).Text.Replace("&nbsp;", "").Trim()
            End If


            ' 1. 判斷【入儲 合計】 (對應橘底黑字)
            If category = "入儲" AndAlso item = "合計" Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDB062") ' 橘色
                e.Row.ForeColor = System.Drawing.Color.Black ' 淺底配黑字
                e.Row.Font.Bold = True

                ' 2. 判斷【消耗 合計】 (對應藍底白字)
            ElseIf category = "消耗" AndAlso item = "合計" Then
                e.Row.BackColor = System.Drawing.Color.Blue ' 藍色
                e.Row.ForeColor = System.Drawing.Color.White ' 深底配白字
                e.Row.Font.Bold = True

                ' 3. 判斷【增減量】 (對應紅底白字)
            ElseIf category = "增減量" Then
                e.Row.BackColor = System.Drawing.Color.Red ' 紅色
                e.Row.ForeColor = System.Drawing.Color.White ' 深底配白字
                e.Row.Font.Bold = True
            End If

        End If
    End Sub

    Private Sub BindEChartsData()
        ' 從 SqlDataSource (dsWeekly_forteechart) 取得 DataView
        Dim dv As DataView = CType(GetWeeklyInventoryReport.Select(DataSourceSelectArguments.Empty), DataView)

        If dv IsNot Nothing AndAlso dv.Count > 0 Then
            ' 使用 StringBuilder 來組合 JSON 格式的陣列字串
            Dim strX As New System.Text.StringBuilder("[")
            Dim strDiff As New System.Text.StringBuilder("[")
            Dim strStock As New System.Text.StringBuilder("[") ' 新增：庫存量

            For i As Integer = 0 To dv.Count - 1
                Dim row As DataRowView = dv(i)

                ' --- 1. 處理 X 軸 (LogTime) 轉換為民國年 ---
                Dim dateStr As String = ""
                If Not IsDBNull(row("LogTime")) Then
                    Dim dt As DateTime = Convert.ToDateTime(row("LogTime"))
                    ' 將西元年減去 1911 變成民國年，並組合 MM.dd
                    dateStr = (dt.Year - 1911).ToString() & "." & dt.ToString("MM.dd")
                End If
                strX.Append("'" & dateStr & "'")

                ' --- 2. 處理 Y 軸數值 (對應新資料集的欄位名稱) ---
                strDiff.Append(If(IsDBNull(row("增減量合計")), "0", row("增減量合計").ToString()))

                ' 庫存量如果是小數點，直接轉字串傳給前端 ECharts 處理即可
                strStock.Append(If(IsDBNull(row("庫存量")), "0", row("庫存量").ToString()))

                ' --- 3. 加上逗號區隔 (最後一筆不加) ---
                If i < dv.Count - 1 Then
                    strX.Append(",")
                    strDiff.Append(",")
                    strStock.Append(",")
                End If
            Next

            strX.Append("]")
            strDiff.Append("]")
            strStock.Append("]")

            ' 將組裝好的字串指派給全域變數
            xAxisData_Weekly = strX.ToString()
            seriesData_Diff = strDiff.ToString()
            seriesData_Stock = strStock.ToString() ' 指派給新的變數
        End If
    End Sub

    Private Sub BindEChartsData_TodayRcv()

        Dim dv As DataView = CType(dsImport.Select(DataSourceSelectArguments.Empty), DataView)

        If dv IsNot Nothing AndAlso dv.Count > 0 Then
            Dim strX As New System.Text.StringBuilder("[")
            Dim strY As New System.Text.StringBuilder("[")

            For i As Integer = 0 To dv.Count - 1
                Dim row As DataRowView = dv(i)

                ' 1. 處理 X 軸 (今日入儲即時資訊)
                Dim category As String = If(IsDBNull(row(0)), "", row(0).ToString())
                strX.Append("'" & category & "'")
                ' 2. 處理 Y 軸 (數量(PCS))
                Dim qty As String = If(IsDBNull(row(1)), "0", row(1).ToString())
                strY.Append(qty)
                ' 3. 加上逗號區隔 (最後一筆不加)
                If i < dv.Count - 1 Then
                    strX.Append(",")
                    strY.Append(",")
                End If
            Next

            strX.Append("]")
            strY.Append("]")

            ' 賦值給全域變數
            xAxisData_TodayRcv = strX.ToString()
            seriesData_TodayRcv = strY.ToString()
        End If
    End Sub
    Private Sub BindEChartsData_TodayUsed()

        Dim dv As DataView = CType(dsExport.Select(DataSourceSelectArguments.Empty), DataView)

        If dv IsNot Nothing AndAlso dv.Count > 0 Then
            Dim strX As New System.Text.StringBuilder("[")
            Dim strY As New System.Text.StringBuilder("[")

            For i As Integer = 0 To dv.Count - 1
                Dim row As DataRowView = dv(i)

                ' 1. 處理 X 軸 (索引 0：今日消耗即時資訊)
                Dim category As String = If(IsDBNull(row(0)), "", row(0).ToString())
                strX.Append("'" & category & "'")

                ' 2. 處理 Y 軸 (索引 1：數量(PCS))
                Dim qty As String = If(IsDBNull(row(1)), "0", row(1).ToString())
                strY.Append(qty)

                ' 3. 加上逗號區隔
                If i < dv.Count - 1 Then
                    strX.Append(",")
                    strY.Append(",")
                End If
            Next

            strX.Append("]")
            strY.Append("]")

            ' 賦值給全域變數
            xAxisData_TodayUsed = strX.ToString()
            seriesData_TodayUsed = strY.ToString()
        End If
    End Sub

    Private Sub BindEChartsData_Storage()

        Dim dv As DataView = CType(SqlDataSource2.Select(DataSourceSelectArguments.Empty), DataView)


        If dv IsNot Nothing AndAlso dv.Count > 0 Then
            Dim row As DataRowView = dv(0)

            ' X軸直接寫死對應的類別名稱
            xAxisData_Storage = "['Y1', 'Y2', 'Y3', 'Y4', 'HC保溫坑', '庫存量']"

            ' Y軸數值 (對應 SQL 撈出來的 orate 與 left 欄位)
            Dim y1 As String = If(IsDBNull(row("y1_orate")), "0", row("y1_orate").ToString())
            Dim y2 As String = If(IsDBNull(row("y2_orate")), "0", row("y2_orate").ToString())
            Dim y3 As String = If(IsDBNull(row("y3_orate")), "0", row("y3_orate").ToString())
            Dim y4 As String = If(IsDBNull(row("y4_orate")), "0", row("y4_orate").ToString())
            Dim hc As String = If(IsDBNull(row("hc_orate")), "0", row("hc_orate").ToString())
            Dim total As String = If(IsDBNull(row("total_stock_num_left")), "0", row("total_stock_num_left").ToString())

            ' 組合成陣列字串
            seriesData_Storage = $"[{y1}, {y2}, {y3}, {y4}, {hc}, {total}]"
        End If
    End Sub


    Private Sub BindEChartsData_slab_Nextcode()
        ' 1. 準備原始數值的 List (分行宣告，最安全不易報錯)
        Dim rHR As New List(Of String)()
        Dim rRD As New List(Of String)()
        Dim rWT As New List(Of String)()
        Dim rHV As New List(Of String)()
        Dim rRT As New List(Of String)()
        Dim rTS As New List(Of String)()
        Dim rEM1 As New List(Of String)()

        ' 2. 準備混合百分比的 List
        Dim pHR As New List(Of String)()
        Dim pRD As New List(Of String)()
        Dim pWT As New List(Of String)()
        Dim pHV As New List(Of String)()
        Dim pRT As New List(Of String)()
        Dim pTS As New List(Of String)()
        Dim pEM As New List(Of String)()

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PMISConnectionString").ConnectionString)
            ' 讀取 SQL 中處理好的「混合比例尺」View
            Dim cmd As New SqlCommand("SELECT * FROM vw_Slab_Nextcode_Detail ORDER BY SortOrder", conn)
            conn.Open()
            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    ' 讀取原始 PCS 數值
                    rHR.Add(reader("HotRoll").ToString())
                    rRD.Add(reader("Ready").ToString())
                    rWT.Add(reader("Wait").ToString())
                    rHV.Add(reader("Heavy").ToString())
                    rRT.Add(reader("Return").ToString())
                    rTS.Add(reader("Test").ToString())
                    rEM1.Add(reader("Empty_Raw").ToString())

                    ' 讀取 SQL 計算好的混合百分比 (Pct)
                    pHR.Add(reader("HotRoll_MixedPct").ToString())
                    pRD.Add(reader("Ready_MixedPct").ToString())
                    pWT.Add(reader("Wait_MixedPct").ToString())
                    pHV.Add(reader("Heavy_MixedPct").ToString())
                    pRT.Add(reader("Return_MixedPct").ToString())
                    pTS.Add(reader("Test_MixedPct").ToString())
                    pEM.Add(reader("Empty_MixedPct").ToString())
                End While
            End Using
        End Using

        ' 3. 將 List 轉換成 JavaScript 陣列格式字串 
        js_Raw_HotRoll = "[" & String.Join(",", rHR) & "]"
        js_Raw_Ready = "[" & String.Join(",", rRD) & "]"
        js_Raw_Wait = "[" & String.Join(",", rWT) & "]"
        js_Raw_Heavy = "[" & String.Join(",", rHV) & "]"
        js_Raw_Return = "[" & String.Join(",", rRT) & "]"
        js_Raw_Test = "[" & String.Join(",", rTS) & "]"
        js_Raw_Empty = "[" & String.Join(",", rEM1) & "]"

        js_Pct_HotRoll = "[" & String.Join(",", pHR) & "]"
        js_Pct_Ready = "[" & String.Join(",", pRD) & "]"
        js_Pct_Wait = "[" & String.Join(",", pWT) & "]"
        js_Pct_Heavy = "[" & String.Join(",", pHV) & "]"
        js_Pct_Return = "[" & String.Join(",", pRT) & "]"
        js_Pct_Test = "[" & String.Join(",", pTS) & "]"
        js_Pct_Empty = "[" & String.Join(",", pEM) & "]"
    End Sub
End Class

