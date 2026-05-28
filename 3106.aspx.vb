Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' 3106 HBM 高爐煉鐵生產指標監控
''' 顯示當班三班（早/中/夜）PA/PY/PO/OR/MR 即時資料，以及本月每日統計
''' 資料來源：h_pmis_mp01（高爐板坯資料，HBMPMIS 資料庫）
''' HBM 使用班別代碼 K=早班 / Q=中班 / 8=夜班（與 HSM 不同）
''' </summary>
Partial Public Class HBM_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3106"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '讀取 SqlDataSource1 近12個月趨勢資料
            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                '設定資料區間標題
                LabelStartdate.Text = Format(CDate(DR1(0)("process_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("process_date").ToString()), "yyyy/MM")

                '組裝 ECharts 格式資料（近12個月趨勢）
                Dim xAxis As New List(Of String)()
                Dim pa As New List(Of Double)()
                Dim py As New List(Of Double)()
                Dim po As New List(Of Double)()
                Dim opr As New List(Of Double)()
                Dim mr As New List(Of Double)()

                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & Convert.ToDateTime(DR1(i)("process_date")).ToString("yyyy/MM") & "'")
                    pa.Add(If(IsDBNull(DR1(i)("PA")), 0, Convert.ToDouble(DR1(i)("PA"))))
                    py.Add(If(IsDBNull(DR1(i)("PY")), 0, Convert.ToDouble(DR1(i)("PY"))))
                    po.Add(If(IsDBNull(DR1(i)("PO")), 0, Convert.ToDouble(DR1(i)("PO"))))
                    opr.Add(If(IsDBNull(DR1(i)("OR")), 0, Convert.ToDouble(DR1(i)("OR"))))
                    mr.Add(If(IsDBNull(DR1(i)("MR")), 0, Convert.ToDouble(DR1(i)("MR"))))
                Next

                '注入 ECharts JavaScript 資料變數
                Dim script As String = "var chartData = {" &
                    "xAxis: [" & String.Join(",", xAxis) & "]," &
                    "pa: [" & String.Join(",", pa) & "]," &
                    "py: [" & String.Join(",", py) & "]," &
                    "po: [" & String.Join(",", po) & "]," &
                    "or: [" & String.Join(",", opr) & "]," &
                    "mr: [" & String.Join(",", mr) & "]" &
                "};"

                ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
            End If

            Mainprocess()
        End If
    End Sub

    ''' <summary>
    ''' 建立 HBM 三班當日生產資料表（PA/PY/PO/OR/MR）
    ''' 查詢 h_pmis_mp01（Measured_BLBB_weight 板坯重量）
    ''' PA = SUM(Measured_BLBB_weight)（全部板坯，含退料）
    ''' PY = slab_wgt(flag=1) / slab_wgt_pdi * 100
    ''' PO = (slab_wgt(flag=1) - mr_wgt(flag=2,3)) / slab_wgt_pdi * 100
    ''' OR = 固定 100.00（HBM 無延誤資料）
    ''' MR = SUM(flag in 2,3) / 1000（kg→MT）
    ''' </summary>
    Private Sub HBMTable()
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

        '用於儲存各班統計數值（單位：kg，顯示時除以1000）
        Dim slab_wgt_pdi(2) As Double  '投入PA（全部板坯）
        Dim slab_wgt(2) As Double      '合格板坯（flag = 1）
        Dim mr_wgt(2) As Double        '剔退板坯（flag in 2,3）

        '依目前時間判斷班別順序（K=早/Q=中/8=夜，班別代碼為 HBM 特有）
        Select Case Now.Hour
            Case 7 To 14 '早班（7-14h 對應昨日中班→昨日夜班→今日早班）
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym_c = "中夜早" : shift_sym = "KQ8" : shift_num = "231"
            Case 15 To 22 '中班（15-22h 對應昨日夜班→今日早班→今日中班）
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym_c = "夜早中" : shift_sym = "Q8K" : shift_num = "312"
            Case 0 To 6 '夜班（0-6h）
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym_c = "早中夜" : shift_sym = "8KQ" : shift_num = "123"
            Case 23 '夜班（23時起算）
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym_c = "早中夜" : shift_sym = "8KQ" : shift_num = "123"
        End Select

        '設定欄位標題（日期＋班別字符）
        strDailyTitle(2) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym_c(0) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym_c(1) + strDailyTitle(3)
        strDailyTitle(4) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym_c(2) + strDailyTitle(4)
        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")

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

        For shift As Integer = 0 To 2
            '1. Slab_wgt_PDI — 投入PA（全部板坯，DATEADD(hour,1) 是因 HBM 時間記錄偏移1小時）
            strACCESS = "SELECT SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE Shift = '" + shift_sym(shift) + "' AND SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)
            slab_wgt_pdi(shift) = If(dtTmp IsNot Nothing AndAlso dtTmp.Rows.Count > 0 AndAlso Not IsDBNull(dtTmp.Rows(0).Item(0)), Convert.ToDouble(dtTmp.Rows(0).Item(0)), 0)
            '單位換算：kg → MT
            dtDataTable.Rows(0).Item(shift + 2) = (slab_wgt_pdi(shift) / 1000.0).ToString("0.00")

            '2. Slab_wgt — 合格板坯（Rolling_flag = 1）
            strACCESS = "SELECT SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE Shift = '" + shift_sym(shift) + "' AND Rolling_flag = 1 AND SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)
            slab_wgt(shift) = If(dtTmp IsNot Nothing AndAlso dtTmp.Rows.Count > 0 AndAlso Not IsDBNull(dtTmp.Rows(0).Item(0)), Convert.ToDouble(dtTmp.Rows(0).Item(0)), 0)

            '3. MR_wgt — 剔退板坯（Rolling_flag IN 2, 3）
            strACCESS = "SELECT SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE Shift = '" + shift_sym(shift) + "' AND Rolling_flag IN (2, 3) AND SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 8) = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)
            mr_wgt(shift) = If(dtTmp IsNot Nothing AndAlso dtTmp.Rows.Count > 0 AndAlso Not IsDBNull(dtTmp.Rows(0).Item(0)), Convert.ToDouble(dtTmp.Rows(0).Item(0)), 0)
            dtDataTable.Rows(4).Item(shift + 2) = (mr_wgt(shift) / 1000.0).ToString("0.00")

            '4. 計算 PY/PO（以 slab_wgt_pdi 為分母）
            If slab_wgt_pdi(shift) = 0 Then
                dtDataTable.Rows(1).Item(shift + 2) = "N/A"
                dtDataTable.Rows(2).Item(shift + 2) = "N/A"
            Else
                Dim py_val As Double = (slab_wgt(shift) / slab_wgt_pdi(shift)) * 100
                Dim po_val As Double = ((slab_wgt(shift) - mr_wgt(shift)) / slab_wgt_pdi(shift)) * 100
                dtDataTable.Rows(1).Item(shift + 2) = py_val.ToString("0.00")
                dtDataTable.Rows(2).Item(shift + 2) = po_val.ToString("0.00")
            End If
            'OR 固定 100.00（高爐無作業率追蹤）
            dtDataTable.Rows(3).Item(shift + 2) = "100.00"
        Next

        Conn.Close()

        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        gvDaily.Rows(0).Cells(0).Width = 200
        gvDaily.Rows(0).Cells(1).Width = 60
        gvDaily.Rows(0).Cells(2).Width = 180
        gvDaily.Rows(0).Cells(3).Width = 180
        gvDaily.Rows(0).Cells(4).Width = 180
        '最後欄位套用樣式
        For i As Integer = 0 To 4
            gvDaily.Rows(i).Cells(4).CssClass = "irondata0"
        Next
    End Sub

    ''' <summary>
    ''' 建立本月每日生產統計表（PA/PY/PO/OR/MR），逐日查詢 h_pmis_mp01
    ''' </summary>
    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "PA/MT", "PY/%", "PO/%", "OR/%", "MR/MT"}

        Dim sumSlabPDI As Double = 0, sumSlab1 As Double = 0, sumMR As Double = 0
        Dim daysInMonth As Integer = Date.DaysInMonth(Year([Today]), Month([Today]))

        '每月各天的統計陣列（kg，顯示時除以1000）
        Dim arrSlabPDI(daysInMonth) As Double
        Dim arrSlab1(daysInMonth) As Double
        Dim arrMR(daysInMonth) As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        '初始化每月天數列（預設 0.00）
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = Date.Today.ToString("MM") + "月" + (i + 1).ToString("d2") + "日"
            dtDataTable.Rows(i).Item(1) = "0.00"
            dtDataTable.Rows(i).Item(2) = "0.00"
            dtDataTable.Rows(i).Item(3) = "0.00"
            dtDataTable.Rows(i).Item(4) = "100.00"
            dtDataTable.Rows(i).Item(5) = "0.00"
        Next

        Conn.Open()

        '1. 每日投入板坯重量（Slab_wgt_PDI → 等於 PA）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2), SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 6) = '" + Date.Today.ToString("yyyyMM") + "' GROUP BY SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If Not IsDBNull(dtTmp.Rows(i)(0)) Then arrSlabPDI(Val(dtTmp.Rows(i)(0).ToString())) = Convert.ToDouble(dtTmp.Rows(i)(1))
            Next
        End If

        '2. 每日合格板坯（flag = 1）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2), SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE Rolling_flag = 1 And SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 6) = '" + Date.Today.ToString("yyyyMM") + "' GROUP BY SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If Not IsDBNull(dtTmp.Rows(i)(0)) Then arrSlab1(Val(dtTmp.Rows(i)(0).ToString())) = Convert.ToDouble(dtTmp.Rows(i)(1))
            Next
        End If

        '3. 每日剔退板坯（flag in 2, 3）
        strACCESS = "SELECT SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2), SUM(Measured_BLBB_weight) FROM h_pmis_mp01 WHERE Rolling_flag IN (2, 3) And SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 1, 6) = '" + Date.Today.ToString("yyyyMM") + "' GROUP BY SUBSTRING(CONVERT(char, DATEADD(hour, 1, process_date), 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                If Not IsDBNull(dtTmp.Rows(i)(0)) Then arrMR(Val(dtTmp.Rows(i)(0).ToString())) = Convert.ToDouble(dtTmp.Rows(i)(1))
            Next
        End If

        Conn.Close()

        '逐日填入統計值，並計算 PY/PO（OR 固定 100）
        For idate As Integer = 1 To daysInMonth
            Dim pdi_mt As Double = arrSlabPDI(idate) / 1000.0
            Dim slab1_mt As Double = arrSlab1(idate) / 1000.0
            Dim mr_mt As Double = arrMR(idate) / 1000.0

            dtDataTable.Rows(idate - 1).Item(1) = pdi_mt.ToString("0.00")
            dtDataTable.Rows(idate - 1).Item(5) = mr_mt.ToString("0.00")

            If pdi_mt = 0 Then
                dtDataTable.Rows(idate - 1).Item(2) = "0.00"
                dtDataTable.Rows(idate - 1).Item(3) = "0.00"
            Else
                dtDataTable.Rows(idate - 1).Item(2) = ((slab1_mt / pdi_mt) * 100).ToString("0.00")
                dtDataTable.Rows(idate - 1).Item(3) = (((slab1_mt - mr_mt) / pdi_mt) * 100).ToString("0.00")
            End If

            sumSlabPDI += arrSlabPDI(idate)
            sumSlab1 += arrSlab1(idate)
            sumMR += arrMR(idate)
        Next

        '月合計標籤顯示
        lblPA.Text = (sumSlabPDI / 1000.0).ToString("0.00")
        lblMR.Text = (sumMR / 1000.0).ToString("0.00")

        If sumSlabPDI = 0 Then
            lblPY.Text = "0.00"
            lblPO.Text = "0.00"
        Else
            lblPY.Text = ((sumSlab1 / sumSlabPDI) * 100).ToString("0.00")
            lblPO.Text = (((sumSlab1 - sumMR) / sumSlabPDI) * 100).ToString("0.00")
        End If
        lblOR.Text = "100.00"
        lblMonth.Text = Date.Today.ToString("MM")

        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False

        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next
    End Sub

    Private Sub Mainprocess()
        '建立 HBMPMIS 資料庫連線（高爐製程獨立資料庫）
        Conn = New SqlConnection(getHBMConnStr(Application("HBMConnStr")))
        HBMTable()
        SumTable()
    End Sub
End Class
