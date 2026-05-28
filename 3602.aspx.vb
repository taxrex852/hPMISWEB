Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Public Class HSM_Stock2
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3602"
    Private dtLimit As DataTable = Nothing
    Private dtDataTable As DataTable = Nothing

    Private ReadOnly strTitle() As String = {"儲區代號", "D1", "D2", "D1+D2", "D3", "D4", "D5", "D6", "D7", "D3+..+D7", "總量"}
    Private ReadOnly strColName() As String = {"設計數量(Coil)", "庫存數量(Coil)", "剩餘數量(Coil)", "標準容量(MT)", "庫存重量(MT)", "剩餘容量(MT)"}

    Public ChartDataJson As String = "[]"
    Public LimitDataJson As String = "[80,80,80,75,75,75,75,75,75]"

    Private Sub ShowData()
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim Conn As SqlConnection = Nothing
        Dim strAccess As String = ""
        Dim calTmp As Double = 0
        Dim totalTmp As Integer = 0
        Dim totalD37 As Integer = 0
        Dim chartValues As New List(Of Double)
        Dim limitValues As New List(Of Double)

        Conn = GetSQLConnection(getConnStr(Application("ConnStr")))
        If Conn Is Nothing Then Return

        ' ── 滿儲率門檻 ──────────────────────────────────
        strAccess = "SELECT stock_limit FROM sys_stock_limit WHERE stock = 'cymc' ORDER BY stock_level asc"
        dtTmp = execQuery(strAccess, "", Conn)
        If dtTmp IsNot Nothing Then
            dtLimit = New DataTable
            For i As Integer = 0 To 9
                dtLimit.Columns.Add(New DataColumn())
            Next
            dr = dtLimit.NewRow
            dtLimit.Rows.Add(dr)
            dtLimit.Rows(0).Item(0) = "滿儲率門檻(%)"

            ' D1、D2
            For i As Integer = 0 To 1
                calTmp = dtTmp.Rows(i).Item(0) / 10
                dtLimit.Rows(0).Item(i + 1) = calTmp.ToString("F1")
                limitValues.Add(calTmp)
            Next
            ' D1+D2（固定 80%）
            calTmp = 80.0
            dtLimit.Rows(0).Item(3) = calTmp.ToString("F1")
            limitValues.Add(calTmp)
            ' D3~D7
            For i As Integer = 2 To dtTmp.Rows.Count - 1
                calTmp = dtTmp.Rows(i).Item(0) / 10
                dtLimit.Rows(0).Item(i + 2) = calTmp.ToString("F1")
                limitValues.Add(calTmp)
            Next
            ' D3~D7 總體（固定 75%）
            calTmp = 75.0
            dtLimit.Rows(0).Item(9) = calTmp.ToString("F1")
            limitValues.Add(calTmp)

            dtTmp.Dispose()
        End If

        ' ── 初始化庫存資料表（所有數值欄預設 "0"，避免 DBNull 轉型錯誤）───
        dtDataTable = New DataTable
        For i As Integer = 0 To strTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strTitle(i)))
        Next
        For i As Integer = 0 To strColName.Length - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
            For j As Integer = 1 To strTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0"
            Next
        Next

        ' ── D1、D2 資料（h_pmis_ys03）─────────────────
        strAccess = "SELECT top(1) " &
                    "d1_stock_num, d2_stock_num, total_stock_num," &
                    "d1_num, d2_num, total_num," &
                    "d1_stock_num-d1_num, d2_stock_num-d2_num, total_stock_num-total_num," &
                    "d1_stock_weight, d2_stock_weight, total_stock_weight," &
                    "d1_weight, d2_weight, total_weight," &
                    "d1_stock_weight-d1_weight, d2_stock_weight-d2_weight, total_stock_weight-total_weight," &
                    "round(cast(d1_orate/10 as float),1), round(cast(d2_orate/10 as float),1), process_date " &
                    "FROM h_pmis_ys03 ORDER BY process_date DESC"
        dtTmp = execQuery(strAccess, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To 5
                For j As Integer = 0 To 2
                    dtDataTable.Rows(i).Item(j + 1) = If(dtTmp.Rows.Count = 0, "0", dtTmp.Rows(0).Item(i * 3 + j).ToString)
                Next
            Next
            ' D1、D2 滿儲率
            For i As Integer = 0 To 1
                chartValues.Add(If(dtTmp.Rows.Count = 0, 0, Math.Round(CDbl(dtTmp.Rows(0).Item(18 + i)), 1)))
            Next
            If dtTmp.Rows.Count > 0 AndAlso Not IsDBNull(dtTmp.Rows(0).Item(20)) Then
                lblDataTime.Text = dtTmp.Rows(0).Item(20).ToString
            End If
            dtTmp.Dispose()
        Else
            chartValues.Add(0) : chartValues.Add(0)
        End If

        ' D1+D2 滿儲率（計算，用 Val() 安全轉換避免 DBNull 問題）
        Dim d12Design As Integer = CInt(Val(dtDataTable.Rows(0).Item(3).ToString()))
        Dim d12Stock As Integer = CInt(Val(dtDataTable.Rows(1).Item(3).ToString()))
        chartValues.Add(Math.Round(If(d12Design > 0, (d12Stock / CDbl(d12Design)) * 100, 0), 1))

        ' ── D3 資料（h_pmis_di01）─────────────────────
        strAccess = "SELECT top(1) " &
                    "d3_stock_num, d3_num, d3_stock_num-d3_num," &
                    "d3_stock_weight, d3_weight, d3_stock_weight-d3_weight," &
                    "round(cast(d3_orate/10 as float),1), process_date " &
                    "FROM h_pmis_di01 ORDER BY process_date DESC"
        dtTmp = execQuery(strAccess, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To 5
                If dtTmp.Rows.Count = 0 Then
                    dtDataTable.Rows(i).Item(4) = "0"
                Else
                    Dim rawVal As Object = dtTmp.Rows(0).Item(i)
                    Dim v As Integer = If(IsDBNull(rawVal), 0, CInt(rawVal))
                    dtDataTable.Rows(i).Item(4) = If(v < 0, "0", v.ToString)
                End If
            Next
            chartValues.Add(If(dtTmp.Rows.Count = 0, 0, Math.Round(CDbl(dtTmp.Rows(0).Item(6)), 1)))
            dtTmp.Dispose()
        Else
            chartValues.Add(0)
        End If

        ' ── D4~D7 資料（h_pmis_pi01）──────────────────
        strAccess = "SELECT top(1) " &
                    "d4_stock_num, d5_stock_num, d6_stock_num, d7_stock_num," &
                    "d4_num, d5_num, d6_num, d7_num," &
                    "d4_stock_num-d4_num, d5_stock_num-d5_num, d6_stock_num-d6_num, d7_stock_num-d7_num," &
                    "d4_stock_weight, d5_stock_weight, d6_stock_weight, d7_stock_weight," &
                    "d4_weight, d5_weight, d6_weight, d7_weight," &
                    "d4_stock_weight-d4_weight, d5_stock_weight-d5_weight, d6_stock_weight-d6_weight, d7_stock_weight-d7_weight," &
                    "round(cast(d4_orate/10 as float),1), round(cast(d5_orate/10 as float),1)," &
                    "round(cast(d6_orate/10 as float),1), round(cast(d7_orate/10 as float),1), process_date " &
                    "FROM h_pmis_pi01 ORDER BY process_date DESC"
        dtTmp = execQuery(strAccess, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To 5
                totalTmp = CInt(Val(dtDataTable.Rows(i).Item(1).ToString())) +
                           CInt(Val(dtDataTable.Rows(i).Item(2).ToString())) +
                           CInt(Val(dtDataTable.Rows(i).Item(4).ToString()))
                For j As Integer = 0 To 3
                    If dtTmp.Rows.Count = 0 Then
                        dtDataTable.Rows(i).Item(j + 5) = "0"
                    Else
                        Dim rawVal As Object = dtTmp.Rows(0).Item(i * 4 + j)
                        Dim v As Integer = If(IsDBNull(rawVal), 0, CInt(rawVal))
                        Dim sv As String = If(v < 0, "0", v.ToString)
                        dtDataTable.Rows(i).Item(j + 5) = sv
                        totalTmp += CInt(Val(sv))
                    End If
                Next
                dtDataTable.Rows(i).Item(10) = totalTmp
            Next
            ' D3~D7 合計
            For i As Integer = 0 To 5
                totalD37 = CInt(Val(dtDataTable.Rows(i).Item(10).ToString())) - CInt(Val(dtDataTable.Rows(i).Item(3).ToString()))
                dtDataTable.Rows(i).Item(9) = totalD37
            Next
            ' D4~D7 滿儲率
            For i As Integer = 0 To 3
                chartValues.Add(If(dtTmp.Rows.Count = 0, 0, Math.Round(CDbl(dtTmp.Rows(0).Item(24 + i)), 1)))
            Next
            ' D3~D7 總體滿儲率（計算）
            Dim d37Design As Integer = CInt(Val(dtDataTable.Rows(0).Item(9).ToString()))
            Dim d37Stock As Integer = CInt(Val(dtDataTable.Rows(1).Item(9).ToString()))
            chartValues.Add(Math.Round(If(d37Design > 0, (d37Stock / CDbl(d37Design)) * 100, 0), 1))
            dtTmp.Dispose()
        Else
            For i As Integer = 0 To 4
                chartValues.Add(0)
            Next
        End If

        CloseSQLConnection(Conn)

        ' ── 組裝 JSON ──────────────────────────────────
        ChartDataJson = "[" & String.Join(",", chartValues.ToArray()) & "]"
        If limitValues.Count = 9 Then
            LimitDataJson = "[" & String.Join(",", limitValues.ToArray()) & "]"
        End If

        ' ── 繫結 gvlimit ───────────────────────────────
        If dtLimit IsNot Nothing Then
            gvlimit.DataSource = dtLimit
            gvlimit.DataBind()
            gvlimit.HeaderRow.Visible = False
            gvlimit.Rows(0).Cells(0).Width = 143
            For i As Integer = 1 To 9
                gvlimit.Rows(0).Cells(i).Width = 80
            Next
        End If

        ' ── 繫結 gvStock ───────────────────────────────
        If dtDataTable IsNot Nothing Then
            gvStock.DataSource = dtDataTable
            gvStock.DataBind()
            gvStock.Rows(0).Cells(0).Width = 143
            gvStock.Rows(0).Cells(10).Width = 80
            For i As Integer = 1 To 9
                gvStock.Rows(0).Cells(i).Width = 80
            Next

            ' 標籤欄、小計欄、D1+D2欄、D3~D7欄 使用黑色字
            For Each row As GridViewRow In gvStock.Rows
                row.Cells(0).ForeColor = Drawing.Color.Black
                row.Cells(3).ForeColor = Drawing.Color.Black
                row.Cells(9).ForeColor = Drawing.Color.Black
                row.Cells(10).ForeColor = Drawing.Color.Black
            Next
            ' 差異列（剩餘數量、剩餘容量）使用黑色字
            For i As Integer = 1 To 10
                gvStock.Rows(2).Cells(i).ForeColor = Drawing.Color.Black
                gvStock.Rows(5).Cells(i).ForeColor = Drawing.Color.Black
            Next
            ' 為零的儲格加紅色警示
            For i As Integer = 0 To 5
                For y As Integer = 1 To 10
                    If dtDataTable.Rows(i).Item(y).ToString = "0" Then
                        If i = 2 OrElse i = 5 Then
                            If CInt(Val(dtDataTable.Rows(i - 1).Item(y).ToString())) = CInt(Val(dtDataTable.Rows(i - 2).Item(y).ToString())) Then
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
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)
            ShowData()
        End If
    End Sub
End Class
