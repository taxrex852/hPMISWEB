Imports System.Data.SqlClient

Partial Public Class HSM_Stock2
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3602"
    Private Cmd As SqlCommand
    Dim reader As SqlDataReader = Nothing
    Private strACCESS As String
    Private chartDate As Date

    Private ReadOnly strTitle() As String = {"儲區代號", "D1", "D2", "D1+D2", "D3", "D4", "D5", "D6", "D7", "D3+..+D7", "總量"}
    Private ReadOnly strColName() As String = {"設計數量(Coil)", "庫存數量(Coil)", "剩餘數量(Coil)", "標準容量(MT)", "庫存重量(MT)", "剩餘容量(MT)"}
    Private dtLimit As DataTable = Nothing, dtDataTable As DataTable = Nothing

    ' 將資料拋轉給前端 ECharts
    Public ChartDataJson As String = "[]"

    Private Sub ShowData()
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim Conn As SqlConnection = Nothing
        Dim strAccess As String = ""
        Dim strlimit As New StringBuilder, strTC As New StringBuilder
        Dim calTmp As Double = 0
        Dim totalTmp As Integer = 0
        Dim totalD37 As Integer = 0

        ' create sql connection
        Conn = GetSQLConnection(getConnStr(Application("ConnStr")))
        If Conn IsNot Nothing Then
            'limit setting
            strAccess = "SELECT stock_limit FROM sys_stock_limit WHERE stock = 'cymc' ORDER BY stock_level asc"
            dtTmp = execQuery(strAccess, "", Conn)
            If dtTmp IsNot Nothing Then
                ' 滿儲率layout
                dtLimit = New DataTable
                For i As Integer = 0 To 9
                    dtLimit.Columns.Add(New DataColumn())
                Next
                dr = dtLimit.NewRow
                dtLimit.Rows.Add(dr)
                dtLimit.Rows(0).Item(0) = "滿儲率門檻(%)"


                'd1,d2
                For i As Integer = 0 To 1
                    calTmp = dtTmp.Rows(i).Item(0) / 10
                    strlimit.Append(calTmp.ToString("F1") + ",")
                    dtLimit.Rows(0).Item(i + 1) = calTmp.ToString("F1")
                Next
                'd1+d2
                calTmp = 80.0
                strlimit.Append(calTmp.ToString("F1") + ",")
                dtLimit.Rows(0).Item(3) = calTmp.ToString("F1")


                'd3,d4,d5,d6,d7
                For i As Integer = 2 To dtTmp.Rows.Count - 1
                    calTmp = dtTmp.Rows(i).Item(0) / 10
                    strlimit.Append(calTmp.ToString("F1") + ",")
                    dtLimit.Rows(0).Item(i + 2) = calTmp.ToString("F1")
                Next
                'd3+~+d7

                calTmp = 75.0
                strlimit.Append(calTmp.ToString("F1") + ",")
                dtLimit.Rows(0).Item(9) = calTmp.ToString("F1")


                dtTmp.Dispose()
            End If

            'd1,d2
            ' stock table
            strAccess = "SELECT top(1) " & _
                        "d1_stock_num, d2_stock_num,total_stock_num," & _
                        "d1_num,d2_num,total_num," & _
                        "d1_stock_num-d1_num,d2_stock_num-d2_num,total_stock_num-total_num," & _
                        "d1_stock_weight, d2_stock_weight,total_stock_weight," & _
                        "d1_weight, d2_weight,total_weight," & _
                        "d1_stock_weight-d1_weight, d2_stock_weight-d2_weight,total_stock_weight-total_weight," & _
                        "d1_orate/10, d2_orate/10, process_date " & _
                        "FROM h_pmis_ys03 " & _
                        "ORDER BY process_date DESC"
            dtTmp = execQuery(strAccess, "", Conn)
            If dtTmp IsNot Nothing Then
                ' 庫存資料layout
                dtDataTable = New DataTable
                For i As Integer = 0 To strTitle.Length - 1
                    dtDataTable.Columns.Add(New DataColumn(strTitle(i)))
                Next
                For i As Integer = 0 To strColName.Length - 1
                    dr = dtDataTable.NewRow
                    dtDataTable.Rows.Add(dr)
                    dtDataTable.Rows(i).Item(0) = strColName(i)
                Next


                For i As Integer = 0 To 5
                    For j As Integer = 0 To 2
                        If dtTmp.Rows.Count = 0 Then
                            dtDataTable.Rows(i).Item(j + 1) = "0"
                        Else

                            dtDataTable.Rows(i).Item(j + 1) = dtTmp.Rows(0).Item(i * 3 + j).ToString
                        End If
                    Next
                Next




                'teechart data (d1, d2 滿儲率)
                For i As Integer = 0 To 1
                    If dtTmp.Rows.Count = 0 Then
                        strTC.Append("0,")
                    Else
                        calTmp = dtTmp.Rows(0).Item(18 + i)
                        strTC.Append(calTmp.ToString("F1") + ",")
                    End If
                Next

                dtTmp.Dispose()
            Else

            End If

            'teechart data
            'd1+d2
            calTmp = (CType(dtDataTable.Rows(1).Item(3), Integer) / CType(dtDataTable.Rows(0).Item(3), Integer)) * 100
            strTC.Append(calTmp.ToString("F1") + ",")
            'd3
            ' stock table
            strAccess = "SELECT top(1) " & _
                        "d3_stock_num," & _
                        "d3_num," & _
                        "d3_stock_num-d3_num," & _
                        "d3_stock_weight," & _
                        "d3_weight," & _
                        "d3_stock_weight-d3_weight," & _
                        "d3_orate/10, process_date " & _
                        "FROM h_pmis_di01 " & _
                        "ORDER BY process_date DESC"
            dtTmp = execQuery(strAccess, "", Conn)
            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To 5

                    If dtTmp.Rows.Count = 0 Then
                        dtDataTable.Rows(i).Item(4) = "0"
                    Else
                        If CType(dtTmp.Rows(0).Item(i), Integer) < 0 Then
                            dtDataTable.Rows(i).Item(4) = "0"
                        Else
                            dtDataTable.Rows(i).Item(4) = dtTmp.Rows(0).Item(i).ToString
                        End If

                    End If
                Next



                'teechart data (d3 滿儲率)
                'For i As Integer = 0 To 1
                If dtTmp.Rows.Count = 0 Then
                    strTC.Append("0,")
                Else
                    calTmp = dtTmp.Rows(0).Item(6)
                    strTC.Append(calTmp.ToString("F1") + ",")
                End If
                'Next

                dtTmp.Dispose()
            Else
                'lblDYMCRefreshTime.Text = "N/A"
            End If

            strAccess = "SELECT top(1) " & _
                                      "d4_stock_num, d5_stock_num,d6_stock_num,d7_stock_num," & _
                                      "d4_num,d5_num,d6_num,d7_num," & _
                                      "d4_stock_num-d4_num, d5_stock_num-d5_num,d6_stock_num-d6_num,d7_stock_num-d7_num," & _
                                      "d4_stock_weight, d5_stock_weight, d6_stock_weight,d7_stock_weight," & _
                                      "d4_weight, d5_weight,d6_weight,d7_weight," & _
                                      "d4_stock_weight-d4_weight, d5_stock_weight-d5_weight, d6_stock_weight-d6_weight,d7_stock_weight-d7_weight," & _
                                      "d4_orate/10, d5_orate/10,d6_orate/10,d7_orate/10, process_date " & _
                                      "FROM h_pmis_pi01 " & _
                                      "ORDER BY process_date DESC"
                dtTmp = execQuery(strAccess, "", Conn)
                If dtTmp IsNot Nothing Then
                    For i As Integer = 0 To 5
                    totalTmp = CType(dtDataTable.Rows(i).Item(1), Integer) + CType(dtDataTable.Rows(i).Item(2), Integer) + CType(dtDataTable.Rows(i).Item(4), Integer)
                        For j As Integer = 0 To 3
                            If dtTmp.Rows.Count = 0 Then
                                dtDataTable.Rows(i).Item(j + 5) = "0"
                            Else

                            Dim SWBigerW As String
                                If CType(dtTmp.Rows(0).Item(i * 4 + j), Integer) < 0 Then
                                    SWBigerW = "0"
                                Else
                                    SWBigerW = dtTmp.Rows(0).Item(i * 4 + j).ToString
                                End If

                                dtDataTable.Rows(i).Item(j + 5) = SWBigerW
                                'end
                                totalTmp += Val(dtDataTable.Rows(i).Item(j + 5).ToString)
                            End If
                        Next
                        dtDataTable.Rows(i).Item(10) = totalTmp
                    Next




                'sum d3+..+d7
                For i As Integer = 0 To 5
                        totalD37 = CType(dtDataTable.Rows(i).Item(10), Integer) - CType(dtDataTable.Rows(i).Item(3), Integer)
                        'totalD37 = dtDataTable.Rows(i).Item(11) - dtDataTable.Rows(i).Item(3)
                        dtDataTable.Rows(i).Item(9) = totalD37
                    Next

                    'teechart data d3~d7
                    For i As Integer = 0 To 3
                        If dtTmp.Rows.Count = 0 Then
                            strTC.Append("0,")
                        Else
                            calTmp = Val(dtTmp.Rows(0).Item(24 + i).ToString)
                            strTC.Append(calTmp.ToString("F1") + ",")
                        End If
                    Next
                    'd7
                    'strTC.Append("0,")
                    'd3+..+d7
                    calTmp = (CType(dtDataTable.Rows(1).Item(9), Integer) / CType(dtDataTable.Rows(0).Item(9), Integer)) * 100
                    strTC.Append(calTmp.ToString("F1") + ",")
                'teechart data值


                dtTmp.Dispose()
                Else

            End If

                CloseSQLConnection(Conn)
            End If


            ' Data Binding
            gvlimit.DataSource = dtLimit
            gvlimit.DataBind()
            gvlimit.HeaderRow.Visible = False
            gvlimit.Rows(0).Cells(0).Width = 143
            For i As Integer = 1 To 9
                gvlimit.Rows(0).Cells(i).Width = 80
            Next


        gvStock.DataSource = dtDataTable
            gvStock.DataBind()
            gvStock.Rows(0).Cells(0).Width = 143
            gvStock.Rows(0).Cells(10).Width = 80
            For i As Integer = 1 To 9
                gvStock.Rows(0).Cells(i).Width = 80
            Next


        For Each row As GridViewRow In gvStock.Rows
                row.Cells(0).ForeColor = Drawing.Color.Black
                row.Cells(3).ForeColor = Drawing.Color.Black
                row.Cells(9).ForeColor = Drawing.Color.Black
                row.Cells(10).ForeColor = Drawing.Color.Black
            Next

            For i As Integer = 1 To 10
                gvStock.Rows(2).Cells(i).ForeColor = Drawing.Color.Black
                gvStock.Rows(5).Cells(i).ForeColor = Drawing.Color.Black
            Next

            For i As Integer = 0 To 5
                For y As Integer = 1 To 10

                    If dtDataTable.Rows(i).Item(y).ToString = "0" Then
                        If i = 2 Or i = 5 Then
                            If CType(dtDataTable.Rows(i - 1).Item(y), Integer) = CType(dtDataTable.Rows(i - 2).Item(y), Integer) Then
                                gvStock.Rows(i).Cells(y).ForeColor = Drawing.Color.Black
                            Else
                                gvStock.Rows(i).Cells(y).ForeColor = Drawing.Color.Red
                            End If
                        Else
                            gvStock.Rows(i).Cells(y).ForeColor = Drawing.Color.Red
                        End If
                    End If


                Next
            Next



    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)
            ShowData()

            ' 修改圖表資料綁定邏輯：將資料轉為 JS 可以讀懂的陣列格式
            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = SqlDataSource2.Select(args1)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim valList As New List(Of String)
                ' SqlDataSource2 回傳剛好 9 個欄位 (i 從 0 到 8)
                For i As Integer = 0 To 8
                    ' 為了避免空值錯誤，加上基礎防護
                    Dim cellValue As String = DR1(0)(i).ToString()
                    If String.IsNullOrEmpty(cellValue) Then cellValue = "0"

                    valList.Add(cellValue)
                Next

                ' 組裝成 [x, x, x, ...] 的 JSON 字串，前端即可直接調用
                ChartDataJson = "[" & String.Join(",", valList.ToArray()) & "]"
            End If


        End If
    End Sub
End Class