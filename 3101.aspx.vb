Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' 3101 HSM 熱軋生產指標監控
''' 顯示當班三班（早/中/夜）PA/PY/PO/OR/MR 即時資料，以及本月每日統計
''' 資料來源：h_pmis_coil_info、h_pmis_whqh、h_pmis_si01
''' </summary>
Partial Public Class HSM_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3101"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '讀取 SqlDataSource1 近12個月趨勢資料
            Dim args1 As New DataSourceSelectArguments()
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                '設定資料區間標題
                LabelStartdate.Text = DR1(0)("product_date").ToString()
                LabelEnddate.Text = DR1(count - 1)("product_date").ToString()

                ' 組裝 ECharts 格式資料（近12個月趨勢）
                Dim xAxis As New List(Of String)()
                Dim pa As New List(Of Double)()
                Dim py As New List(Of Double)()
                Dim po As New List(Of Double)()
                Dim opr As New List(Of Double)() '避免與 OR 關鍵字衝突
                Dim mr As New List(Of Double)()

                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & DR1(i)("product_date").ToString() & "'")
                    pa.Add(Convert.ToDouble(DR1(i)("PA")))
                    py.Add(Convert.ToDouble(DR1(i)("PY")))
                    po.Add(Convert.ToDouble(DR1(i)("PO")))
                    opr.Add(Convert.ToDouble(DR1(i)("OR")))
                    mr.Add(Convert.ToDouble(DR1(i)("MR")))
                Next

                '將資料注入 JavaScript 變數供 ECharts 使用
                Dim script As String = "var chartData = {" &
                    "xAxis: [" & String.Join(",", xAxis) & "]," &
                    "pa: [" & String.Join(",", pa) & "]," &
                    "py: [" & String.Join(",", py) & "]," &
                    "po: [" & String.Join(",", po) & "]," &
                    "or: [" & String.Join(",", opr) & "]," &
                    "mr: [" & String.Join(",", mr) & "]" &
                "};"

                '登錄前端啟動腳本
                ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
            End If

            Mainprocess()
        End If
    End Sub

    ''' <summary>
    ''' 建立三班當日生產資料表（班別標題列＋5行指標）
    ''' 班別順序依目前時段輪替：早(M)=07-14h、中(A)=15-22h、夜(N)=23-06h
    ''' </summary>
    Private Sub HSMTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "單位", "班", "班", "班"}
        Dim strColName() As String = {"產量 (PA)", "產率 (PY)", "訂單合格率 (PO)", "作業率 (OR)", "剔退重量 (MR)"}
        Dim strUnitName() As String = {"MT", "%", "%", "%", "MT"}
        Dim strACCESS As String = Nothing

        Dim shift_num As String = "", shift_sym As String = ""
        Dim shift_sym_c As String = ""
        Dim shift_date(2) As Date

        Dim calTmp As Double
        Dim slab_mw(2) As Integer

        '依目前時間判斷班別順序，shift_date(0)=前一班, (1)=更前一班, (2)=最近班
        Select Case Now.Hour
            Case 7 To 14 'M 早班
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym_c = "中夜早"
                shift_sym = "ANM"
                shift_num = "231"
            Case 15 To 22 'A 中班
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym_c = "夜早中"
                shift_sym = "NMA"
                shift_num = "312"
            Case 0 To 6 'N 夜班
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "MAN"
                shift_num = "123"
            Case 23 'N 夜班（23時起算）
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym_c = "早中夜"
                shift_sym = "MAN"
                shift_num = "123"
        End Select

        '設定欄位標題（日期＋班別字符）
        strDailyTitle(2) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym_c(0) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym_c(1) + strDailyTitle(3)
        strDailyTitle(4) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym_c(2) + strDailyTitle(4)

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

        Conn.Open()

        'PA(MT) — 產量：reject_reason=0 的 coil_weight 加總（kg → MT 在後段換算）
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " &
                        "SUM(coil_weight) " &
                        "FROM h_pmis_coil_info " &
                        "WHERE reject_reason = 0 AND shift_code = '" + shift_sym(shift) + "' " &
                        "AND SUBSTRING(CONVERT(char, product_date, 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                    dtDataTable.Rows(0).Item(shift + 2) = "0"
                Else
                    dtDataTable.Rows(0).Item(shift + 2) = dtTmp.Rows(0).Item(0).ToString
                End If
            End If
        Next

        'PY(%) — 產率：PA / slab_weight（投入重量）
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " &
                        "SUM(slab_weight) " &
                        "FROM h_pmis_coil_info " &
                        "WHERE shift_code = '" + shift_sym(shift) + "' " &
                        "AND SUBSTRING(CONVERT(char, product_date, 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                    slab_mw(shift) = "0"
                Else
                    slab_mw(shift) = dtTmp.Rows(0).Item(0).ToString
                End If
                calTmp = dtDataTable.Rows(0).Item(shift + 2)
                If (slab_mw(shift) = 0) Then
                    dtDataTable.Rows(1).Item(shift + 2) = "N/A"
                Else
                    calTmp = (calTmp / slab_mw(shift)) * 100
                    dtDataTable.Rows(1).Item(shift + 2) = calTmp.ToString("0.00")
                End If
            End If
        Next

        'PO(%) — 訂單合格率：夜班時間段需跨日處理
        For shift As Integer = 0 To 2

            If shift_sym(shift) = "N" Then
                strACCESS = "SELECT SUM(coil_wm) from h_pmis_whqh " &
                            "WHERE " &
                            "(final_dis <> '1' AND final_dis <> '2' AND " &
                            "final_dis <> 'H')  AND " &
                            "(product_date+product_time) BETWEEN '" + shift_date(shift).AddDays(-1).ToString("yyyyMMddHHmmss") +
                            "' AND '" + shift_date(shift).AddDays(-1).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") + "'"
            Else
                strACCESS = "SELECT SUM(coil_wm) from h_pmis_whqh " &
                            "WHERE " &
                            "(final_dis <> '1' AND final_dis <> '2' AND " &
                            "final_dis <> 'H')  AND " &
                            "(product_date+product_time) BETWEEN '" + shift_date(shift).ToString("yyyyMMddHHmmss") +
                            "' AND '" + shift_date(shift).AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") + "'"
            End If

            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                    calTmp = "0"
                Else
                    calTmp = dtTmp.Rows(0).Item(0).ToString
                End If

                If (slab_mw(shift) = 0) Then
                    dtDataTable.Rows(2).Item(shift + 2) = "N/A"
                Else
                    calTmp = ((dtDataTable.Rows(0).Item(shift + 2) - calTmp) / slab_mw(shift)) * 100
                    dtDataTable.Rows(2).Item(shift + 2) = calTmp.ToString("0.00")
                End If
            End If
        Next

        'OR(%) — 作業率：(480-延誤時間)/(480-停機時間)*100，line_id=1 為 HSM
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " &
                        "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," &
                        "SUM(shutdown_time) " &
                        "FROM h_pmis_si01 " &
                        "WHERE line_id = 1 AND shift = " + shift_num(shift) + " " &
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

                calTmp = calTmp * 100
                dtDataTable.Rows(3).Item(shift + 2) = calTmp.ToString("0.00")
            End If
        Next

        'MR(MT) — 剔退重量：reject_reason != 0 的 slab_weight
        For shift As Integer = 0 To 2
            strACCESS = "SELECT " &
                        "SUM(slab_weight) " &
                        "FROM h_pmis_coil_info " &
                        "WHERE reject_reason != 0 AND shift_code = '" + shift_sym(shift) + "' " &
                        "AND SUBSTRING(CONVERT(char, product_date, 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
                    dtDataTable.Rows(4).Item(shift + 2) = "0"
                Else
                    dtDataTable.Rows(4).Item(shift + 2) = dtTmp.Rows(0).Item(0).ToString
                End If
            End If
        Next

        Conn.Close()

        '單位換算：kg → MT（除以1000）
        dtDataTable.Rows(0).Item(2) = (Val(dtDataTable.Rows(0).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(3) = (Val(dtDataTable.Rows(0).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(4) = (Val(dtDataTable.Rows(0).Item(4).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(2) = (Val(dtDataTable.Rows(4).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(3) = (Val(dtDataTable.Rows(4).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(4) = (Val(dtDataTable.Rows(4).Item(4).ToString) / 1000).ToString("0.00")

    End Sub

    ''' <summary>
    ''' 建立本月每日生產統計表（PA/PY/PO/OR/MR）
    ''' 並將月合計顯示於標籤控制項
    ''' </summary>
    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "PA/MT", "PY/%", "PO/%", "OR/%", "MR/MT"}
        Dim adapter As SqlDataAdapter = Nothing
        Dim tmpVal As Double = 0, tmpSlabWeight As Double = 0

        Dim dtSlab_mw As DataTable
        Dim calTmp As Double
        Dim sumPA, sumSlabmw, sumCoilwm, sumDelay, sumShutdown, sumMR As Single

        '月報表欄位配置
        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        'layout 每月天數列
        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
        Next

        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(0) = Date.Today.ToString("MM") + "月" + (idate + 1).ToString("d2") + "日"
            For j As Integer = 2 To 3
                dtDataTable.Rows(idate).Item(j) = "0.00"
            Next
            dtDataTable.Rows(idate).Item(1) = "0"
            dtDataTable.Rows(idate).Item(4) = "100.00"
            dtDataTable.Rows(idate).Item(5) = "0"
        Next

        Conn.Close()
        'PA(MT) — 每日產量，依日期分組加總
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 7, 2)," &
                    "SUM(coil_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE reject_reason = 0 " &
                    "And SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        sumPA = 0
        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(1) = Val(dtTmp.Rows(iCount).Item(1)).ToString("0.00")
                    sumPA += dtTmp.Rows(iCount).Item(1)
                End If
            Next
        End If
        lblPA.Text = sumPA.ToString("0.00")

        'PY(%) — 每日產率，分母為 slab_weight（投入量）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 7, 2)," &
                    "SUM(slab_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        sumSlabmw = 0
        dtSlab_mw = dtTmp
        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                    If dtTmp.Rows(iCount).Item(1) = 0 Then
                        calTmp = 0
                    Else
                        calTmp = 100 * dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(1) / dtTmp.Rows(iCount).Item(1)
                    End If
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(2) = calTmp.ToString("0.00")
                    sumSlabmw += dtTmp.Rows(iCount).Item(1)
                End If
            Next
        End If

        If sumSlabmw = 0 Then
            calTmp = 0
        Else
            calTmp = 100 * sumPA / sumSlabmw
        End If
        lblPY.Text = calTmp.ToString("0.00")

        'PO(%) — 訂單合格率：final_dis 定義改為 final_dis，99/10/26 修改
        strACCESS = "SELECT Day(product_date), SUM(coil_wm) from h_pmis_whqh " &
                    "WHERE " &
                    "(final_dis <> '1' AND final_dis <> '2' AND " &
                    "final_dis <> 'H')  AND " &
                    "(Year(product_date) = " + Date.Today.ToString("yyyy") + ") and " &
                    "(Month(product_date) = " + Date.Today.ToString("MM") + ") " &
                    "GROUP BY Day(product_date)"

        dtTmp = execQuery(strACCESS, "", Conn)

        sumCoilwm = 0
        If dtTmp IsNot Nothing Then
            '計算有剔退數量的 PO
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                    calTmp = 0
                    If dtSlab_mw IsNot Nothing Then
                        For i As Integer = 0 To dtSlab_mw.Rows.Count - 1
                            If dtTmp.Rows(iCount).Item(0) = dtSlab_mw.Rows(i).Item(0) Then
                                calTmp = dtSlab_mw.Rows(i).Item(1)
                            End If
                        Next
                    End If

                    If calTmp <> 0 Then
                        calTmp = 100 * (dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(1) - dtTmp.Rows(iCount).Item(1)) / calTmp
                    End If
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(3) = calTmp.ToString("0.00")
                    sumCoilwm += dtTmp.Rows(iCount).Item(1)
                End If
            Next
        End If

        '計算無剔退數量的 PO（以 slab_weight 補算）
        If dtSlab_mw IsNot Nothing Then
            For i As Integer = 0 To Val(Now.ToString("dd")) - 1
                If Val(dtDataTable.Rows(i).Item(3)) = 0 Then
                    For j As Integer = 0 To dtSlab_mw.Rows.Count - 1
                        If Val(dtSlab_mw.Rows(j).Item(0)) - 1 = i Then
                            tmpSlabWeight = Val(dtSlab_mw.Rows(j).Item(1))
                            Exit For
                        End If
                    Next
                    If tmpSlabWeight <> 0 Then
                        dtDataTable.Rows(i).Item(3) = (100 * (Val(dtDataTable.Rows(i).Item(1)) / tmpSlabWeight)).ToString("0.00")
                    End If
                End If
            Next
        End If


        If sumSlabmw = 0 Then
            calTmp = 0
        Else
            calTmp = 100 * (sumPA - sumCoilwm) / sumSlabmw
        End If
        lblPO.Text = calTmp.ToString("0.00")

        'OR(%) — 每日作業率，依班次總延誤與停機時間計算
        strACCESS = "SELECT Day(select_dates)," &
                    "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," &
                    "SUM(shutdown_time) " &
                    "FROM h_pmis_si01 " &
                    "WHERE line_id = 1 AND " &
                    "(Year(select_dates) = " + Date.Today.ToString("yyyy") + ") and " &
                    "(Month(select_dates) = " + Date.Today.ToString("MM") + ") " &
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

        If sumShutdown = 480 * dtDataTable.Rows.Count Then
            calTmp = 0
        Else
            calTmp = 100 * (480 * dtDataTable.Rows.Count - sumDelay) / (480 * dtDataTable.Rows.Count - sumShutdown)
        End If
        lblOR.Text = calTmp.ToString("0.00")

        'MR(MT) — 每日剔退重量
        strACCESS = "SELECT SUBSTRING(CONVERT(char, product_date, 112), 7, 2)," &
                    "SUM(slab_weight) " &
                    "FROM h_pmis_coil_info " &
                    "WHERE reject_reason != 0 AND " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " &
                    "SUBSTRING(CONVERT(char, product_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " " &
                    "GROUP BY SUBSTRING(CONVERT(char, product_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        sumMR = 0
        If dtTmp IsNot Nothing Then
            For iCount As Integer = 0 To dtTmp.Rows.Count - 1
                If dtTmp.Rows(iCount).Item(0) IsNot DBNull.Value Then
                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(5) = Val(dtTmp.Rows(iCount).Item(1)).ToString("0.00")
                    sumMR += dtTmp.Rows(iCount).Item(1)
                End If
            Next
        End If

        lblMR.Text = sumMR.ToString

        Conn.Close()

        '單位換算：PA/MR 皆由 kg 換算為 MT（除以1000）
        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(1) = (Val(dtDataTable.Rows(idate).Item(1).ToString) / 1000).ToString("0.00")
            dtDataTable.Rows(idate).Item(5) = (Val(dtDataTable.Rows(idate).Item(5).ToString) / 1000).ToString("0.00")
        Next
        lblPA.Text = (Val(lblPA.Text) / 1000).ToString("0.00")
        lblMR.Text = (Val(lblMR.Text) / 1000).ToString("0.00")

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

    Private Sub Mainprocess()
        '建立資料庫連線（PMIS 主資料庫）
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        HSMTable()
        SumTable()
    End Sub
End Class
