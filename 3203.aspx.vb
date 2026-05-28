Imports System.Data.SqlClient
Imports System.Collections.Generic

' ============================================================
' 頁面：3203 — 2TNRL 缺陷分析
' 功能：顯示 2TNRL 產線各班次及本月缺陷 Top 排行
'        - 剔退缺陷 Top 5（依 an_weight 降冪排序）
'        - 其他缺陷 Top 3（依 cd_weight 降冪排序）
' 資料來源：h_pmis_wh97（全小寫）
'   剔退：def_code_1~4（缺陷碼）/ an_weight（剔退重量，g）
'   其他：cd_code_1~4（缺陷碼）/ cd_weight_1~4（缺陷重量，g）
' 班制：ANM 循環（A=中班15-22, N=夜班0-6/23, M=早班7-14）
' ============================================================
Partial Public Class _2TNRL_Defect
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3203"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)

            Dim args1 As New DataSourceSelectArguments
            ' SqlDataSource1 提供近 13 個月的缺陷月統計（def_top1~5）
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                ' 顯示資料區間（起迄月份）
                LabelStartdate.Text = Format(CDate(DR1(0)("product_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("product_date").ToString()), "yyyy/MM")

                ' 準備 ECharts 趨勢圖資料（各月 Top1~5 剔退缺陷重量）
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

                ' 組合 JavaScript 物件字串，注入前端 ECharts 圖表
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

    ' ============================================================
    ' TNRL1_Table — 當日三班次缺陷表格（gvDaily）
    ' 表格共 8 行：
    '   Row 0~4：剔退缺陷 Top 1~5（code/MT）
    '   Row 5~7：其他缺陷 Top 1~3（code/MT）
    ' 班次判斷與 3103 相同（ANM/NMA/MAN 循環）
    ' ============================================================
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
        ' 依目前小時決定班次順序
        Select Case Now.Hour
            Case 7 To 14 'M班（早班）值班 → 顯示順序：A(中)→N(夜)→M(早)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_num = "231"
                shift_sym_c = "ANM"
            Case 15 To 22 'A班（中班）值班 → 顯示順序：N(夜)→M(早)→A(中)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_num = "312"
                shift_sym_c = "NMA"
            Case 0 To 6 'N班（夜班）值班 → 顯示順序：M(早)→A(中)→N(夜)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
            Case 23 'N班（夜班起始）→ 同 00-06 邏輯
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
        End Select

        ' 設定欄位標題（含日期與班別）
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
        ' ── 剔退缺陷 Top 5（各班次）──────────────────────────
        ' 以 UNION 合併 def_code_1~4，依 an_weight 總和降冪取 Top 5
        ' 顯示格式：缺陷碼/MT（單位換算：g ÷ 1000）
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
                    '單位換算（g → MT）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    dtDataTable.Rows(i).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                ' 不足 5 筆時補 N/A
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(i).Item(shift + 1) = "N/A"
                Next
            End If

            ' ── 其他缺陷 Top 3（各班次）──────────────────────
            ' 以 UNION 合併 cd_code_1~4 / cd_weight_1~4，依重量降冪取 Top 3
            ' 填入 Row 5~7（剔退 Top5 之後）
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
                    '單位換算（g → MT）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    ' 其他缺陷從 Row 5 開始（Row 0~4 為剔退 Top 5）
                    dtDataTable.Rows(i + 5).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                ' 不足 3 筆時補 N/A
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

        ' 最後一班（當班）欄位套用特殊樣式
        For i As Integer = 0 To 7
            gvDaily.Rows(i).Cells(3).CssClass = "irondata0"
        Next

    End Sub

    ' ============================================================
    ' SumTable — 本月逐日缺陷統計（gvMonth）+ 月累計標籤
    ' 逐日迴圈：每日執行 Top5 剔退 + Top3 其他兩個查詢
    ' 月累計：
    '   lblR1~lblR5：剔退缺陷 Top 1~5（月累計，code/MT）
    '   lblC1~lblC3：其他缺陷 Top 1~3（月累計，code/MT）
    ' ============================================================
    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtDataTable1 As New DataTable
        Dim dtDataTable2 As New DataTable
        Dim dtDataTable3 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        ' 月表欄位：日期 + Top5剔退 + Top3其他（共 9 欄）
        Dim strMonthTitle() As String = {" ", "defect top (1)", "defect top (2)", "defect top (3)", "defect top (4)", "defect top (5)", "defect top (1) ", "defect top (2) ", "defect top (3) "}
        Dim adapter As SqlDataAdapter = Nothing

        Dim calTmp As Double

        ' 建立本月逐日空白行
        'Month produce record
        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        'layout
        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
        Next
        ' 初始化日期標籤
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
            'For j As Integer = 0 To strMonthTitle.Length - 2
            '    dtDataTable.Rows(idate).Item(j + 1) = "N/A"
            'Next
        Next

        Conn.Open()

        ' 逐日查詢本月每天的缺陷排行
        For idate As Integer = 1 To Date.DaysInMonth(Year([Today]), Month([Today]))
            ' ── 每日剔退缺陷 Top 5 ─────────────────────────
            ' 合併 def_code_1~4，依 an_weight 降冪取 Top 5
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
                    '單位換算（g → MT）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    dtDataTable.Rows(idate - 1).Item(i + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                ' 不足 5 筆時補 N/A（欄位 i+1，i=dtTmp.Rows.Count~4）
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(idate - 1).Item(i + 1) = "N/A"
                Next
            End If

            ' ── 每日其他缺陷 Top 3 ─────────────────────────
            ' 合併 cd_code_1~4 / cd_weight_1~4，依重量降冪取 Top 3
            ' 填入欄位 6~8（欄位 1~5 為剔退 Top5）
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
                    '單位換算（g → MT）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    ' 其他缺陷從第 6 欄（Index=6）開始填入
                    dtDataTable.Rows(idate - 1).Item(i + 6) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                ' 不足 3 筆時補 N/A
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

        ' ── 月累計剔退缺陷 Top 5（lblR1~lblR5）───────────────
        ' 查詢本月所有日期的 def_code_1~4 合併，依 an_weight 降冪取 Top 5
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

        ' 初始化月累計標籤
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
                            '單位換算（g → MT）
                            lblR1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算（g → MT）
                            lblR2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算（g → MT）
                            lblR3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                        Case 3
                            '單位換算（g → MT）
                            lblR4.Text = dtTmp.Rows(3).Item(0).ToString + "/" + (Val(dtTmp.Rows(3).Item(1).ToString) / 1000).ToString("0.00")
                        Case 4
                            '單位換算（g → MT）
                            lblR5.Text = dtTmp.Rows(4).Item(0).ToString + "/" + (Val(dtTmp.Rows(4).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

        ' ── 月累計其他缺陷 Top 3（lblC1~lblC3）───────────────
        ' 查詢本月所有日期的 cd_code_1~4 合併，依 cd_weight 降冪取 Top 3
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

        ' 初始化月累計標籤
        lblC1.Text = "N/A"
        lblC2.Text = "N/A"
        lblC3.Text = "N/A"
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(i).Item(0).ToString.Trim.Length <> 0 Then
                    Select Case i
                        Case 0
                            '單位換算（g → MT）
                            lblC1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算（g → MT）
                            lblC2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算（g → MT）
                            lblC3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

        Conn.Close()

    End Sub

    ' ============================================================
    ' TeeChartData（已停用，改以 ECharts 取代）
    ' 原用途：提供 TeeChart 控制項的月趨勢圖資料
    ' 保留此區塊供歷史參考
    ' ============================================================
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

    ' ============================================================
    ' Mainprocess — 主流程進入點
    ' 建立連線後依序呼叫班次缺陷表格和月統計表格
    ' ============================================================
    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        TNRL1_Table()
        SumTable()
        'TeeChartData()
    End Sub
End Class
