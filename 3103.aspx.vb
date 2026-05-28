Imports System.Data.SqlClient
Imports System.Collections.Generic

' ============================================================
' 頁面：3103 — 2TNRL 生產績效
' 功能：顯示 2TNRL 產線各班次及本月累計生產指標
'        PA / PY / PO / OR / MR
' 資料來源：
'   h_pmis_WH97  — 2TNRL 軋延完工紀錄（g_weight：完工重量，g）
'   h_pmis_WH9C  — 2TNRL 捲取完工紀錄（gross_weight：毛重，g）
'   h_pmis_wh95  — 母捲資料（coil_weight：鋼捲重量）
'   h_pmis_si01  — 作業停機紀錄（line_id=3 對應 2TNRL）
' 班制：ANM 循環（A=中班15-22, N=夜班0-6/23, M=早班7-14）
' 注意：MR（剔退重量）= N/A，2TNRL 無剔退追蹤資料
' ============================================================
Partial Public Class _2TNRL_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3103"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)
            Dim args1 As New DataSourceSelectArguments
            ' SqlDataSource1 提供近 13 個月的月統計資料（用於 ECharts 趨勢圖）
            Dim DR1 As DataView = SqlDataSource1.Select(args1)
            Dim count As Integer = DR1.Count
            ' 顯示資料區間（起迄月份）
            LabelStartdate.Text = Format(CDate(DR1(0)(0).ToString), "yyyy/MM")
            LabelEnddate.Text = Format(CDate(DR1(count - 1)(0).ToString), "yyyy/MM")

            ' 準備 ECharts 趨勢圖資料陣列
            Dim xAxis As New List(Of String)()
            Dim pa As New List(Of Double)()
            Dim py As New List(Of Double)()
            Dim po As New List(Of Double)()
            Dim opr As New List(Of Double)()
            Dim mr As New List(Of Double)()

            ' 逐月填入各 KPI 數值
            For i As Integer = 0 To count - 1
                xAxis.Add("'" & Format(CDate(DR1(i)("process_date").ToString()), "yyyy/MM") & "'")
                pa.Add(Convert.ToDouble(DR1(i)("PA")))
                py.Add(Convert.ToDouble(DR1(i)("PY")))
                po.Add(Convert.ToDouble(DR1(i)("PO")))
                opr.Add(Convert.ToDouble(DR1(i)("OR")))
                mr.Add(Convert.ToDouble(DR1(i)("MR")))
            Next

            ' 組合 JavaScript 物件字串，注入前端 ECharts 圖表
            Dim script As String = "var chartData = {" &
                "xAxis: [" & String.Join(",", xAxis) & "]," &
                "pa: [" & String.Join(",", pa) & "]," &
                "py: [" & String.Join(",", py) & "]," &
                "po: [" & String.Join(",", po) & "]," &
                "or: [" & String.Join(",", opr) & "]," &
                "mr: [" & String.Join(",", mr) & "]" &
            "};"

            ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)

            Mainprocess()
        End If
        'hAnc.Value = ""
    End Sub

    ' ============================================================
    ' TNRL2_Table — 當日三班次生產績效表格（gvDaily）
    ' 顯示各班最近一次班次的 PA/PY/PO/OR/MR
    ' 班次判斷邏輯（依目前時間）：
    '   07-14 (早班時間) → 顯示順序：中班(A)、夜班(N)、早班(M)
    '   15-22 (中班時間) → 顯示順序：夜班(N)、早班(M)、中班(A)
    '   00-06 (夜班時間) → 顯示順序：早班(M)、中班(A)、夜班(N)
    '   23    (夜班時間) → 同 00-06
    ' ============================================================
    Private Sub TNRL2_Table()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing, dtTmp2 As DataTable = Nothing
        Dim bPO_pa_Ready As Boolean = True, bPO_coil_Ready As Boolean = True

        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "單位", "班", "班", "班"}
        Dim strColName() As String = {"產量 (PA)", "產率 (PY)", "訂單合格率 (PO)", "作業率 (OR)", "剔退重量 (MR)"}
        Dim strUnitName() As String = {"MT", "%", "%", "%", "MT"}
        Dim strACCESS As String = "", strACCESS2 As String = ""

        ' shift_num：對應 h_pmis_si01.shift 欄位（1=早M, 2=中A, 3=夜N）
        Dim shift_num As String = "", shift_sym As String = "", shift_sym_c As String = ""
        ' shift_date(0/1/2)：三個班次的 shift_date（yyyyMMdd 格式）
        Dim shift_date(2) As Date

        Dim calTmp As Double
        Dim slab_mw(2) As Integer

        ' tmpPA(i)：各班次 PA（g 單位），用於 PY 計算暫存
        Dim tmpPA(2) As Double

        ' 依目前小時決定班次順序與對應日期
        Select Case Now.Hour
            Case 7 To 14 'M班（早班）當前值班 → 最近完成的三班依序：A(中)→N(夜)→M(早)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_sym_c = "ANM"
                shift_num = "231"
            Case 15 To 22 'A班（中班）當前值班 → 最近完成的三班依序：N(夜)→M(早)→A(中)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_sym_c = "NMA"
                shift_num = "312"
            Case 0 To 6 'N班（夜班）當前值班 → 最近完成的三班依序：M(早)→A(中)→N(夜)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
                shift_num = "123"
            Case 23 'N班（夜班起始）→ 同 00-06 邏輯
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
                shift_num = "123"
        End Select

        ' 設定欄位標題（含日期與班別）
        strDailyTitle(2) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(3)
        strDailyTitle(4) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(4)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        'layout title
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next
        For i As Integer = 0 To 4
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
            dtDataTable.Rows(i).Item(1) = strUnitName(i)
        Next

        'dtTmp = New DataTable
        Conn.Open()
        ' ── PA（產量）─────────────────────────────────────────
        ' PA = Σ(WH97.g_weight) + Σ(WH9C.gross_weight)，單位 g
        ' 以 FULL OUTER JOIN 合併兩表確保不漏任何班次資料
        'PA=Σ(WH97的第17項：Gross weight)
        For shift As Integer = 0 To 2
            strACCESS = String.Format("select ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod, ISNULL(A.product_day, B.product_day) as ProductDay from " & _
                                            "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                            "SUM(g_weight) as product_weight from h_pmis_WH97 " & _
                                            "where shift_date='{0}' and shift_code='{1}' " & _
                                            "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                                        "FULL OUTER JOIN " & _
                                            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight " & _
                                            "from h_pmis_WH9C " & _
                                            "where shift_date='{0}' and shift_code='{1}' " & _
                                            "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                        "ON A.product_day = B.product_day " & _
                                        "ORDER BY ProductDay", _
                                        shift_date(shift).ToString("yyyyMMdd"), _
                                        shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows.Count > 0 Then
                    If Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
                        dtDataTable.Rows(0).Item(shift + 2) = dtTmp.Rows(0).Item(0).ToString
                    Else
                        dtDataTable.Rows(0).Item(shift + 2) = "0"
                    End If
                Else
                    dtDataTable.Rows(0).Item(shift + 2) = "0"
                End If
            Else
                dtDataTable.Rows(0).Item(shift + 2) = "N/A"
            End If
            dtTmp.Dispose()
            tmpPA(shift) = IIf(dtDataTable.Rows(0).Item(shift + 2).ToString = "N/A", 0, dtDataTable.Rows(0).Item(shift + 2))
        Next

        ' ── PY（產率）─────────────────────────────────────────
        ' PY = PA / Coil measured weight × 100
        ' Coil measured weight：wh95.coil_weight（透過 material_no 前 7 碼比對 wh95.coil_no）
        ' 分母同樣需 FULL OUTER JOIN WH97+WH9C 的 coil_weight 加總
        'PY=PA/Coil measured weight
        'Coil measured weight=Σ(wh95的第33項：Coil weight)
        For shift As Integer = 0 To 2
            strACCESS = String.Format("select (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight, ISNULL(A.ProductDay, B.ProductDay) as ProductDay from " & _
                                            "(select SUBSTRING(WH97.shift_date, 7, 2) as ProductDay, SUM(wh95.coil_weight) as material_weight from " & _
                                            "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                                            "where shift_date='{0}' and shift_code='{1}') as WH97, " & _
                                            "h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                                            "group by SUBSTRING(WH97.shift_date, 7, 2)) as A " & _
                                      "FULL OUTER JOIN " & _
                                            "(select SUBSTRING(shift_date, 7, 2) as ProductDay, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                                            "where shift_date='{0}' and shift_code='{1}' " & _
                                            "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                      "ON A.ProductDay = B.ProductDay", _
                                      shift_date(shift).ToString("yyyyMMdd"), _
                                      shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows.Count > 0 Then
                    If Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
                        If Val(dtTmp.Rows(0).Item(0)) <> 0 Then
                            'dtDataTable.Rows(1).Item(shift + 2) = Decimal.Round(dtTmp.Rows(0).Item(0), 2)
                            If dtDataTable.Rows(0).Item(shift + 2).ToString.Trim <> "N/A" Then
                                ' PY = (PA / Coil measured weight) × 100
                                dtDataTable.Rows(1).Item(shift + 2) = ((Val(dtDataTable.Rows(0).Item(shift + 2)) / Val(dtTmp.Rows(0).Item(0))) * 100).ToString("0.00")
                            Else
                                dtDataTable.Rows(1).Item(shift + 2) = "N/A"
                            End If
                        Else
                            dtDataTable.Rows(1).Item(shift + 2) = "N/A"
                        End If
                    Else
                        dtDataTable.Rows(1).Item(shift + 2) = "0.00"
                    End If
                Else
                    dtDataTable.Rows(1).Item(shift + 2) = "0.00"
                End If
            Else
                dtDataTable.Rows(1).Item(shift + 2) = "N/A"
            End If
            dtTmp.Dispose()
        Next

        ' ── PO（訂單合格率）───────────────────────────────────
        ' PO = 合格 PA / Coil measured weight × 100
        ' 合格條件：disposition in ('1','2','H')（正常品與特採）
        ' 分子：WH97(disposition='1'/'2'/'H') + WH9C（全部）的重量 FULL OUTER JOIN
        'PO=[PA-Σ(disposition=3,4,5,6]/ Coil measured weight
        'Coil measured weight=Σ(wh95的第33項：Coil weight)
        For shift As Integer = 0 To 2
            bPO_pa_Ready = True
            bPO_coil_Ready = True

            ' 合格產量（分子）
            strACCESS = String.Format("select ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod, ISNULL(A.product_day, B.product_day) as ProductDay from " & _
                                "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                "SUM(g_weight) as product_weight from h_pmis_WH97 " & _
                                "where shift_date='{0}' and shift_code='{1}' " & _
                                "and disposition in ('1', '2', 'H') " & _
                                "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                            "FULL OUTER JOIN " & _
                                "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight " & _
                                "from h_pmis_WH9C " & _
                                "where shift_date='{0}' and shift_code='{1}' " & _
                                "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                            "ON A.product_day = B.product_day " & _
                            "ORDER BY ProductDay", _
                            shift_date(shift).ToString("yyyyMMdd"), _
                            shift_sym_c(shift))
            ' 投入鋼捲重量（分母）
            strACCESS2 = String.Format("select (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight, ISNULL(A.ProductDay, B.ProductDay) as ProductDay from " & _
                                "(select SUBSTRING(WH97.shift_date, 7, 2) as ProductDay, SUM(wh95.coil_weight) as material_weight from " & _
                                "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                                "where shift_date='{0}' and shift_code='{1}') as WH97, " & _
                                "h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                                "group by SUBSTRING(WH97.shift_date, 7, 2)) as A " & _
                          "FULL OUTER JOIN " & _
                                "(select SUBSTRING(shift_date, 7, 2) as ProductDay, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                                "where shift_date='{0}' and shift_code='{1}' " & _
                                "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                          "ON A.ProductDay = B.ProductDay", _
                          shift_date(shift).ToString("yyyyMMdd"), _
                          shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)
            dtTmp2 = execQuery(strACCESS2, "", Conn)

            If dtTmp Is Nothing Then
                bPO_pa_Ready = False
            Else
                If dtTmp.Rows.Count <= 0 Then
                    bPO_pa_Ready = False
                End If
            End If

            If dtTmp2 Is Nothing Then
                bPO_coil_Ready = False
            Else
                If dtTmp2.Rows.Count <= 0 Then
                    bPO_coil_Ready = False
                End If
            End If

            If bPO_coil_Ready Then
                If bPO_pa_Ready Then
                    If Not IsDBNull(dtTmp2.Rows(0).Item(0)) Then
                        If Val(dtTmp2.Rows(0).Item(0)) <> 0 Then
                            ' PO = (合格PA / Coil measured weight) × 100
                            dtDataTable.Rows(2).Item(shift + 2) = Val(((IIf(IsDBNull(dtTmp.Rows(0).Item(0)), 0, Val(dtTmp.Rows(0).Item(0))) / Val(dtTmp2.Rows(0).Item(0))) * 100)).ToString("0.00")
                        Else
                            dtDataTable.Rows(2).Item(shift + 2) = "N/A"
                        End If
                    Else
                        dtDataTable.Rows(2).Item(shift + 2) = "N/A"
                    End If
                Else
                    dtDataTable.Rows(2).Item(shift + 2) = "0.00"
                End If
            Else
                dtDataTable.Rows(2).Item(shift + 2) = "N/A"
            End If

            If dtTmp IsNot Nothing Then dtTmp.Dispose()
            If dtTmp2 IsNot Nothing Then dtTmp2.Dispose()

            'If dtTmp IsNot Nothing Then
            '    If dtTmp.Rows.Count > 0 Then
            '        If Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
            '            dtDataTable.Rows(2).Item(shift + 2) = Decimal.Round(dtTmp.Rows(0).Item(0), 2)
            '        Else
            '            dtDataTable.Rows(2).Item(shift + 2) = "0"
            '        End If
            '    Else
            '        dtDataTable.Rows(2).Item(shift + 2) = "0"
            '    End If
            'Else
            '    dtDataTable.Rows(2).Item(shift + 2) = "N/A"
            'End If

        Next

        ' ── OR（作業率）──────────────────────────────────────
        ' OR = (480 - 總延誤時間) / (480 - shutdown時間) × 100
        ' 資料來源：h_pmis_si01，line_id=3 為 2TNRL 產線
        ' 480 分鐘 = 一個班次的標準作業時間
        'OR-----------------------------------------------------------
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " & _
                        "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," & _
                        "SUM(shutdown_time) " & _
                        "FROM h_pmis_si01 " & _
                        "WHERE line_id = 3 AND shift = " + shift_num(shift) + " " & _
                        "AND select_dates = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                'OR
                If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                    calTmp = 480
                Else
                    calTmp = 480 - dtTmp.Rows(0).Item(0)
                End If

                If dtTmp.Rows(0).Item(1) Is DBNull.Value Then
                    calTmp = calTmp / 480
                Else
                    calTmp = calTmp / (480 - dtTmp.Rows(0).Item(1))
                End If
            End If

            calTmp = calTmp * 100
            dtDataTable.Rows(3).Item(shift + 2) = calTmp.ToString("0.00")
        Next

        Conn.Close()

        ' ── MR（剔退重量）────────────────────────────────────
        ' 2TNRL 無剔退追蹤資料，固定顯示 N/A
        'MR-----------------------------------------------------------
        For shift As Integer = 0 To 2
            dtDataTable.Rows(4).Item(shift + 2) = "N/A"
        Next

        ' ── 單位換算（g → MT）────────────────────────────────
        ' PA 原始單位為 g，除以 1000 轉為 MT（公噸）顯示
        '------
        dtDataTable.Rows(0).Item(2) = (Val(dtDataTable.Rows(0).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(3) = (Val(dtDataTable.Rows(0).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(4) = (Val(dtDataTable.Rows(0).Item(4).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(2) = (Val(dtDataTable.Rows(4).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(3) = (Val(dtDataTable.Rows(4).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(4) = (Val(dtDataTable.Rows(4).Item(4).ToString) / 1000).ToString("0.00")
        '------

        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        gvDaily.Rows(0).Cells(0).Width = 200
        gvDaily.Rows(0).Cells(1).Width = 60
        gvDaily.Rows(0).Cells(2).Width = 180
        gvDaily.Rows(0).Cells(3).Width = 180
        gvDaily.Rows(0).Cells(4).Width = 180

        ' 最後一班（當班）欄位套用特殊樣式
        For i As Integer = 0 To 4
            gvDaily.Rows(i).Cells(4).CssClass = "irondata0"
        Next
    End Sub

    ' ============================================================
    ' SumTable — 本月逐日及月累計統計（gvMonth）
    ' 顯示本月每日 PA/PY/PO/OR/MR 及月統計數值
    ' lblPA / lblPY / lblPO / lblOR / lblMR：月累計標籤
    ' ============================================================
    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dtMonthCoilWeight As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "PA/MT", "PY/%", "PO/%", "OR/%", "MR/MT"}
        Dim adapter As SqlDataAdapter = Nothing

        Dim strSQL_A As String = ""
        Dim strSQL_B As String = ""

        'Dim dtSlab_mw As DataTable
        Dim calTmp As Double
        'Dim sumSlabmw, sumCoilwm, sumMR As Integer
        Dim sumPA, sumDelay, sumShutdown As Integer

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

        ' 初始化各日預設值
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
            For j As Integer = 2 To 4
                dtDataTable.Rows(idate).Item(j) = "0.00"
            Next
            dtDataTable.Rows(idate).Item(1) = "0"
            dtDataTable.Rows(idate).Item(4) = "100.00"
            dtDataTable.Rows(idate).Item(5) = "0"
        Next

        Conn.Open()

        ' ── PA（月逐日）─────────────────────────────────────
        ' PA = Σ(WH97.g_weight) + Σ(WH9C.gross_weight)，依日期分組
        'PA-----------------------------------------------------------
        'PA=Σ(WH97的第17項：Gross weight)
        strACCESS = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, " & _
                                "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                                "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                "ON A.product_day = B.product_day", _
                            Now.ToString("yyyyMM"))

        dtTmp = execQuery(strACCESS, "", Conn)
        sumPA = 0
        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If (dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value) And (dtTmp.Rows(iCount).Item(1) IsNot DBNull.Value) Then
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(1) = dtTmp.Rows(iCount).Item(1)
                    sumPA += dtTmp.Rows(iCount).Item(1)
                End If
            Next
        End If
        'PA月統計
        lblPA.Text = sumPA.ToString


        ' ── PY（月逐日 + 月累計）────────────────────────────
        ' 每日 PY = 每日 PA / 每日 Coil measured weight × 100
        ' 月累計 PY = 月累計 PA / 月累計 Coil measured weight × 100
        'PY-----------------------------------------------------------
        'PY=PA/Coil measured weight
        'SQL = 每日Coil measured weight(分母)
        strSQL_A = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, " & _
                                "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                                "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                "ON A.product_day = B.product_day", _
                            Now.ToString("yyyyMM"))

        strSQL_B = String.Format("select ISNULL(A.ProductDay, B.ProductDay) as ProductDay, (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
                                    "(select SUBSTRING(WH97.shift_date, 7, 2) as ProductDay, SUM(wh95.coil_weight) as material_weight from " & _
                                    "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                                    "where shift_date like '{0}%') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                                    "group by SUBSTRING(WH97.shift_date, 7, 2)) as A " & _
                                 "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as ProductDay, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                                    "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                 "ON A.ProductDay = B.ProductDay", _
                                 Now.ToString("yyyyMM"))

        ' 外層 SQL 用 FULL OUTER JOIN 計算每日 PY
        strACCESS = String.Format("select ISNULL(PA.ProductDay, Coil_sum.ProductDay) as PYDay, " & _
                                "(CASE ISNULL(Coil_sum.material_weight, 0) when 0 then '0.00' " & _
                                "else (ISNULL(PA.total_prod,0) / ISNULL(Coil_sum.material_weight, 0)) * 100 end) as PY " & _
                                "from ({0}) as PA FULL OUTER JOIN ({1}) as Coil_sum ON PA.ProductDay = Coil_sum.ProductDay", _
                            strSQL_A, _
                            strSQL_B)

        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If (dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value) And (dtTmp.Rows(iCount).Item(1) IsNot DBNull.Value) Then
                    '每日PY=每日PA/每日Coil measured weight
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(2) = Decimal.Round(dtTmp.Rows(iCount).Item(1), 2)
                End If
            Next
        End If

        'PY月統計
        'SQL = 本月Coil measured weight(分母)
        strACCESS = String.Format("select (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
                            "(select SUBSTRING(WH97.shift_date, 5, 2) as ProductMonth, SUM(wh95.coil_weight) as material_weight from " & _
                            "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                            "where shift_date like '{0}%') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                            "group by SUBSTRING(WH97.shift_date, 5, 2)) as A " & _
                         "FULL OUTER JOIN " & _
                            "(select SUBSTRING(shift_date, 5, 2) as ProductMonth, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 5, 2)) as B " & _
                         "ON A.ProductMonth = B.ProductMonth", _
                         Now.ToString("yyyyMM"))

        dtMonthCoilWeight = execQuery(strACCESS, "", Conn)
        'PY月統計=本月PA/本月Coil measured weight
        If dtMonthCoilWeight IsNot Nothing Then
            If dtMonthCoilWeight.Rows.Count > 0 Then
                If dtMonthCoilWeight.Rows(0).Item(0) IsNot DBNull.Value Then
                    lblPY.Text = Decimal.Round(IIf(dtMonthCoilWeight.Rows(0).Item(0) = 0, 0, sumPA / dtMonthCoilWeight.Rows(0).Item(0)) * 100, 2)
                Else
                    lblPY.Text = "0.00"
                End If
            Else
                lblPY.Text = "0.00"
            End If
        Else
            lblPY.Text = "0.00"
        End If

        ' ── PO（月逐日 + 月累計）────────────────────────────
        ' 每日 PO = 每日合格 PA / 每日 Coil measured weight × 100
        ' 月累計 PO = 月累計合格 PA / 月累計 Coil measured weight × 100
        'PO-----------------------------------------------------------
        'PO=[PA-Σ(disposition=3,4,5,6]/ Coil measured weight
        'SQL = 每日[PA-Σ(disposition=3,4,5,6](分子)
        strSQL_A = String.Format("select ISNULL(A.product_day, B.product_day) as ProductDay, " & _
                                "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
                                    "disposition in ('1', '2', 'H') and " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                                "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                                    "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                "ON A.product_day = B.product_day", _
                            Now.ToString("yyyyMM"))

        strSQL_B = String.Format("select ISNULL(A.ProductDay, B.ProductDay) as ProductDay, (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
                                    "(select SUBSTRING(WH97.shift_date, 7, 2) as ProductDay, SUM(wh95.coil_weight) as material_weight from " & _
                                    "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                                    "where shift_date like '{0}%') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                                    "group by SUBSTRING(WH97.shift_date, 7, 2)) as A " & _
                                 "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 7, 2) as ProductDay, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                                    "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                                 "ON A.ProductDay = B.ProductDay", _
                                 Now.ToString("yyyyMM"))

        strACCESS = String.Format("select ISNULL(PA.ProductDay, Coil_sum.ProductDay) as PYDay, " & _
                                "(CASE ISNULL(Coil_sum.material_weight, 0) when 0 then 'N/A' " & _
                                "else (ISNULL(PA.total_prod,0) / ISNULL(Coil_sum.material_weight, 0)) * 100 end) as PY " & _
                                "from ({0}) as PA FULL OUTER JOIN ({1}) as Coil_sum ON PA.ProductDay = Coil_sum.ProductDay", _
                            strSQL_A, _
                            strSQL_B)


        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If (dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value) And (dtTmp.Rows(iCount).Item(1) IsNot DBNull.Value) Then
                    '每日PO=每日[PA-Σ(disposition=3,4,5,6]/ 每日Coil measured weight
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(3) = Decimal.Round(dtTmp.Rows(iCount).Item(1), 2)
                End If
            Next
        End If

        'PO月統計
        'SQL = 本月[PA-Σ(disposition=3,4,5,6)(分子)
        strSQL_A = String.Format("select ISNULL(A.product_month, B.product_month) as Product_month, " & _
                                "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
                                    "(select SUBSTRING(shift_date, 5, 2) as product_month, " & _
                                    "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
                                    "disposition in ('1', '2', 'H') and " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 5, 2)) as A " & _
                                "FULL OUTER JOIN " & _
                                    "(select SUBSTRING(shift_date, 5, 2) as product_month, " & _
                                    "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
                                    "shift_date like '{0}%' Group by SUBSTRING(shift_date, 5, 2)) as B " & _
                                "ON A.product_month = B.product_month", _
                            Now.ToString("yyyyMM"))

        strSQL_B = String.Format("select ISNULL(A.ProductMonth, B.ProductMonth) as Product_month, (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
                            "(select SUBSTRING(WH97.shift_date, 5, 2) as ProductMonth, SUM(wh95.coil_weight) as material_weight from " & _
                            "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
                            "where shift_date like '{0}%') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
                            "group by SUBSTRING(WH97.shift_date, 5, 2)) as A " & _
                         "FULL OUTER JOIN " & _
                            "(select SUBSTRING(shift_date, 5, 2) as ProductMonth, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
                            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 5, 2)) as B " & _
                         "ON A.ProductMonth = B.ProductMonth", _
                         Now.ToString("yyyyMM"))

        strACCESS = String.Format("select " & _
                                "(CASE ISNULL(Coil_sum.material_weight, 0) when 0 then 'N/A' " & _
                                "else (ISNULL(PA.total_prod,0) / ISNULL(Coil_sum.material_weight, 0)) * 100 end) as PY, " & _
                                "ISNULL(PA.Product_month, Coil_sum.Product_month) as PY_month " & _
                                "from ({0}) as PA FULL OUTER JOIN ({1}) as Coil_sum ON PA.Product_month = Coil_sum.Product_month", _
                            strSQL_A, _
                            strSQL_B)

        dtTmp = execQuery(strACCESS, "", Conn)
        'PO月統計=本月[PA-Σ(disposition=3,4,5,6]/ 本月Coil measured weight
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                lblPO.Text = Val(dtTmp.Rows(0).Item(0)).ToString("0.00")
            Else
                lblPO.Text = "0.00"
            End If
        End If

        ' ── OR（月逐日 + 月累計）────────────────────────────
        ' 每日 OR = (480×3 - 各班延誤時間合計) / (480×3 - shutdown時間合計) × 100
        ' line_id=3 對應 2TNRL 產線
        'OR-----------------------------------------------------------
        strACCESS = "SELECT Day(select_dates)," & _
                    "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," & _
                    "SUM(shutdown_time) " & _
                    "FROM h_pmis_si01 " & _
                    "WHERE line_id = 3 AND " & _
                    "(Year(select_dates) = " + Date.Today.ToString("yyyy") + ") and " & _
                    "(Month(select_dates) = " + Date.Today.ToString("MM") + ") " & _
                    "GROUP BY Day(select_dates)"
        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                    If dtTmp.Rows(iCount).Item(2) = 480 * 3 Then
                        calTmp = 0
                    Else
                        calTmp = 100 * (480 * 3 - dtTmp.Rows(iCount).Item(1)) / (480 * 3 - dtTmp.Rows(iCount).Item(2))
                    End If
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(4) = calTmp.ToString("0.00")
                    sumDelay = dtTmp.Rows(iCount).Item(1)
                    sumShutdown = dtTmp.Rows(iCount).Item(2)
                End If
            Next
        End If

        Conn.Close()

        ' OR 月累計
        If sumShutdown = 480 * dtDataTable.Rows.Count Then
            calTmp = 0
        Else
            calTmp = 100 * (480 * dtDataTable.Rows.Count - sumDelay) / (480 * dtDataTable.Rows.Count - sumShutdown)
        End If
        lblOR.Text = calTmp.ToString("0.00")

        ' ── MR（月累計）─────────────────────────────────────
        ' 2TNRL 無剔退資料，月統計固定為 0（換算後顯示 "0.00" MT）
        'MR-----------------------------------------------------------
        lblMR.Text = "0"


        ' ── 單位換算（g → MT）────────────────────────────────
        ' 月表中 PA（欄位1）與 MR（欄位5）原單位為 g，除以 1000 轉為 MT
        '-----
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(1) = (Val(dtDataTable.Rows(idate).Item(1).ToString) / 1000).ToString("0.00")
            dtDataTable.Rows(idate).Item(5) = (Val(dtDataTable.Rows(idate).Item(5).ToString) / 1000).ToString("0.00")
        Next
        lblPA.Text = (Val(lblPA.Text) / 1000).ToString("0.00")
        lblMR.Text = (Val(lblMR.Text) / 1000).ToString("0.00")
        '-----

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        'col width
        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next

        lblMonth.Text = Date.Today.ToString("MM")
    End Sub

    ' ============================================================
    ' TeeChartData（已停用，改以 ECharts 取代）
    ' 原用途：提供 TeeChart 控制項的月趨勢圖資料
    ' 保留此區塊供歷史參考
    ' ============================================================
    'Private Sub TeeChartData()
    '    Dim dtTmp As DataTable = Nothing
    '    Dim tmpDate As Date

    '    Dim dtOverall As New DataTable
    '    Dim strTitle() As String = {"Year", "Month", "PA", "PY", "slab_mw", "PO", "OR", "MR"}
    '    Dim dr As DataRow
    '    Dim calTmp As Double
    '    Dim rowNum As Integer

    '    Dim strSQL_A As String = "", strSQL_B As String = ""

    '    'TeeChart data
    '    Dim strDate As New StringBuilder
    '    Dim strPA As New StringBuilder
    '    Dim strPY As New StringBuilder
    '    Dim strPO As New StringBuilder
    '    Dim strOR As New StringBuilder
    '    Dim strMR As New StringBuilder

    '    For i As Integer = 0 To strTitle.Length - 1
    '        dtOverall.Columns.Add(New DataColumn(strTitle(i)))
    '    Next

    '    'layout
    '    For i As Integer = 0 To 12
    '        dr = dtOverall.NewRow
    '        dtOverall.Rows.Add(dr)
    '    Next

    '    tmpDate = chartDate
    '    For i As Integer = 0 To 12
    '        dtOverall.Rows(i).Item(0) = tmpDate.AddMonths(i).Year
    '        dtOverall.Rows(i).Item(1) = tmpDate.AddMonths(i).Month
    '        For j As Integer = 2 To 5
    '            dtOverall.Rows(i).Item(j) = "0"
    '        Next
    '        dtOverall.Rows(i).Item(6) = "100.00"
    '        dtOverall.Rows(i).Item(7) = "0"
    '    Next

    '    Conn.Open()
    '    'PA-----------------------------------------------------------
    '    'PA=Σ(WH97的第17項：Gross weight)
    '    strACCESS = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
    '                                "shift_date between '{0}' and '{1}' Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    If dtTmp IsNot Nothing Then
    '        For i As Integer = 0 To dtTmp.Rows.Count - 1
    '            rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '            dtOverall.Rows(rowNum).Item(2) = dtTmp.Rows(i).Item(2)
    '        Next
    '    End If

    '    'PY-----------------------------------------------------------
    '    'PY=PA/Coil measured weight
    '    'Coil measured weight=Σ(wh95的第33項：Coil weight)

    '    strSQL_A = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
    '                                "shift_date between '{0}' and '{1}' Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    strSQL_B = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
    '                                "(select SUBSTRING(WH97.shift_date, 1, 4) as PYear, SUBSTRING(WH97.shift_date, 5, 2) as PMonth, SUM(wh95.coil_weight) as material_weight from " & _
    '                                "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
    '                                "where shift_date between '{0}' and '{1}') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
    '                                "group by SUBSTRING(WH97.shift_date, 1, 4), SUBSTRING(WH97.shift_date, 5, 2)) as A " & _
    '                             "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
    '                                "where shift_date between '{0}' and '{1}' Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                             "ON A.PYear = B.PYear and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    strACCESS = String.Format("select ISNULL(PA.PYear, Coil_sum.PYear) as PYear, ISNULL(PA.PMonth, Coil_sum.PMonth) as PMonth, " & _
    '                            "(CASE ISNULL(Coil_sum.material_weight, 0) when 0 then 'N/A' " & _
    '                            "else (ISNULL(PA.total_prod,0) / ISNULL(Coil_sum.material_weight, 0)) * 100 end) as PY " & _
    '                            "from ({0}) as PA FULL OUTER JOIN ({1}) as Coil_sum ON PA.PYear = Coil_sum.PYear and PA.PMonth = Coil_sum.PMonth", _
    '                        strSQL_A, _
    '                        strSQL_B)

    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    If dtTmp IsNot Nothing Then
    '        For i As Integer = 0 To dtTmp.Rows.Count - 1
    '            rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '            dtOverall.Rows(rowNum).Item(3) = dtTmp.Rows(i).Item(2)
    '        Next
    '    End If

    '    'PO-----------------------------------------------------------
    '    'PO=[PA-Σ(disposition=3,4,5,6]/ Coil measured weight
    '    strSQL_A = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, " & _
    '                            "ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(g_weight) as product_weight from h_pmis_WH97 where " & _
    '                                "shift_date between '{0}' and '{1}' " & _
    '                                "and disposition in ('1', '2', 'H') " & _
    '                                "Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as A " & _
    '                            "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, " & _
    '                                "SUM(gross_weight) as product_weight from h_pmis_WH9C where " & _
    '                                "shift_date between '{0}' and '{1}' Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                                "ON A.PMonth = B.PMonth and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    strSQL_B = String.Format("select ISNULL(A.PYear, B.PYear) as PYear, ISNULL(A.PMonth, B.PMonth) as PMonth, (ISNULL(A.material_weight, 0) + ISNULL(B.material_weight, 0)) as material_weight from " & _
    '                                "(select SUBSTRING(WH97.shift_date, 1, 4) as PYear, SUBSTRING(WH97.shift_date, 5, 2) as PMonth, SUM(wh95.coil_weight) as material_weight from " & _
    '                                "(select distinct SUBSTRING(material_no, 1, 7) as mno, shift_date from h_pmis_WH97 " & _
    '                                "where shift_date between '{0}' and '{1}') as WH97, h_pmis_wh95 as wh95 where WH97.mno = wh95.coil_no " & _
    '                                "group by SUBSTRING(WH97.shift_date, 1, 4), SUBSTRING(WH97.shift_date, 5, 2)) as A " & _
    '                             "FULL OUTER JOIN " & _
    '                                "(select SUBSTRING(shift_date, 1, 4) as PYear, SUBSTRING(shift_date, 5, 2) as PMonth, SUM(gross_weight) as material_weight from h_pmis_WH9C " & _
    '                                "where shift_date between '{0}' and '{1}' Group by SUBSTRING(shift_date, 1, 4), SUBSTRING(shift_date, 5, 2)) as B " & _
    '                             "ON A.PYear = B.PYear and A.PMonth = B.PMonth", _
    '                        tmpDate.ToString("yyyyMM") + "01", _
    '                        tmpDate.AddMonths(12).ToString("yyyyMM") + Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))).ToString)

    '    strACCESS = String.Format("select ISNULL(PA.PYear, Coil_sum.PYear) as PYear, ISNULL(PA.PMonth, Coil_sum.PMonth) as PMonth, " & _
    '                            "(CASE ISNULL(Coil_sum.material_weight, 0) when 0 then 'N/A' " & _
    '                            "else (ISNULL(PA.total_prod,0) / ISNULL(Coil_sum.material_weight, 0)) * 100 end) as PY " & _
    '                            "from ({0}) as PA FULL OUTER JOIN ({1}) as Coil_sum ON PA.PYear = Coil_sum.PYear and PA.PMonth = Coil_sum.PMonth", _
    '                        strSQL_A, _
    '                        strSQL_B)

    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    If dtTmp IsNot Nothing Then
    '        For i As Integer = 0 To dtTmp.Rows.Count - 1
    '            rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '            dtOverall.Rows(rowNum).Item(5) = dtTmp.Rows(i).Item(2)
    '        Next
    '    End If

    '    'OR-----------------------------------------------------------
    '    strACCESS = "SELECT year(select_dates), month(select_dates)," & _
    '                "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," & _
    '                "SUM(shutdown_time) " & _
    '                "FROM h_pmis_si01 " & _
    '                "WHERE line_id = 3 AND " & _
    '                "select_dates BETWEEN '" + tmpDate.ToString("yyyyMM") + "01' " & _
    '                 "AND '" + tmpDate.AddMonths(12).ToString("yyyyMM") & _
    '                Date.DaysInMonth(Year(tmpDate.AddMonths(12)), Month(tmpDate.AddMonths(12))) & _
    '                "' GROUP BY year(select_dates), month(select_dates)"
    '    dtTmp = execQuery(strACCESS, "", Conn)

    '    If dtTmp IsNot Nothing Then
    '        For i As Integer = 0 To dtTmp.Rows.Count - 1
    '            rowNum = (dtTmp.Rows(i).Item(0) - dtOverall.Rows(0).Item(0)) * 12 + dtTmp.Rows(i).Item(1) - dtOverall.Rows(0).Item(1)
    '            calTmp = Date.DaysInMonth(dtTmp.Rows(i).Item(0), dtTmp.Rows(i).Item(1))
    '            If 480 - dtTmp.Rows(i).Item(3) = 0 Then
    '                calTmp = 0
    '            Else
    '                calTmp = 100 * ((480 * calTmp) - dtTmp.Rows(i).Item(2)) / ((480 * calTmp) - dtTmp.Rows(i).Item(3))
    '            End If
    '            dtOverall.Rows(rowNum).Item(6) = calTmp.ToString("0.00")
    '        Next
    '    End If

    '    Conn.Close()

    '    '單位換算
    '    '--------
    '    For i As Integer = 0 To dtOverall.Rows.Count - 1
    '        dtOverall.Rows(i).Item(2) = Val(dtOverall.Rows(i).Item(2).ToString) / 1000
    '        dtOverall.Rows(i).Item(7) = Val(dtOverall.Rows(i).Item(7).ToString) / 1000
    '    Next
    '    '--------

    '    '傳入前台控制項
    '    For i As Integer = 0 To dtOverall.Rows.Count - 1
    '        strDate.Append(tmpDate.AddMonths(i).ToString("MM") + "月,")
    '        strPA.Append(dtOverall.Rows(i).Item(2).ToString + ",")
    '        strPY.Append(dtOverall.Rows(i).Item(3).ToString + ",")
    '        strPO.Append(dtOverall.Rows(i).Item(5).ToString + ",")
    '        strOR.Append(dtOverall.Rows(i).Item(6).ToString + ",")
    '        strMR.Append(dtOverall.Rows(i).Item(7).ToString + ",")
    '    Next

    '    hDate.Value = strDate.ToString
    '    hPA.Value = strPA.ToString
    '    hPY.Value = strPY.ToString
    '    hPO.Value = strPO.ToString
    '    hOR.Value = strOR.ToString
    '    hMR.Value = strMR.ToString
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
    ' 建立連線後依序呼叫班次表格和月統計表格
    ' ============================================================
    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        TNRL2_Table()
        SumTable()
        'TeeChartData()
    End Sub
End Class
