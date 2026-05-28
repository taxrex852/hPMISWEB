Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' 4TNRL 缺陷 Top5/Top3 監控頁面 (PAGE_ID=3207)
''' 資料來源：h_pmis_wh73
''' 缺陷欄位：def_code_1~4（an_weight）→ Top5 剔退，cd_code_1~4（cd_weight_1~4）→ Top3 其他缺陷
''' 班次代碼：A=中班、N=夜班、M=早班（ANM/NMA/MAN 順序）
''' 單位換算：an_weight / cd_weight (kg) → MT（/ 1000）
''' 連線：getConnStr（HPMIS 資料庫）
''' </summary>
Partial Public Class _4TNRL_Defect
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3207"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)

            '取得 SqlDataSource1 資料（12個月 ECharts 趨勢用）
            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                '設定圖表起訖月份標籤
                LabelStartdate.Text = Format(CDate(DR1(0)("product_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("product_date").ToString()), "yyyy/MM")

                '組裝 ECharts JSON 資料（def_top1~def_top5 趨勢）
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

    ''' <summary>
    ''' 三班缺陷日報表（gvDaily）
    ''' 分兩區塊：
    '''   上半段（row 0~4）：剔退 Top5，查詢 def_code_1~4/an_weight，依班次+日期
    '''   下半段（row 5~7）：其他缺陷 Top3，查詢 cd_code_1~4/cd_weight_1~4，依班次+日期
    ''' 單位換算：an_weight/cd_weight (kg) → MT（/ 1000）
    ''' 顯示格式：缺陷代碼/重量(MT)，不足筆數補 N/A
    ''' </summary>
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
        '依目前時間判斷班次顯示順序
        Select Case Now.Hour
            Case 7 To 14 'M 早班時段 → 顯示順序：中夜早 (ANM)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_num = "231"
                shift_sym_c = "ANM"
            Case 15 To 22 'A 中班時段 → 顯示順序：夜早中 (NMA)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_num = "312"
                shift_sym_c = "NMA"
            Case 0 To 6 'N 夜班時段 → 顯示順序：早中夜 (MAN)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
            Case 23 'N 夜班起始小時 → 顯示順序：早中夜 (MAN)
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_num = "123"
                shift_sym_c = "MAN"
        End Select

        '設定表頭日期班別
        strDailyTitle(1) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(1)
        strDailyTitle(2) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(3)

        strDailyTitle(0) = "目前日期 : " + Date.Today.Date.ToString("MM月dd日")
        'layout title
        For i As Integer = 0 To strDailyTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strDailyTitle(i)))
        Next
        '建立 Top5 + Top3 共 8 列
        For i As Integer = 0 To strColName.Length - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dtDataTable.Rows(i).Item(0) = strColName(i)
            'For j As Integer = 1 To strDailyTitle.Length - 1
            '    dtDataTable.Rows(i).Item(j) = "N/A"
            'Next
        Next

        Conn.Open()
        '逐班查詢兩類缺陷資料
        'reject
        For shift As Integer = 0 To 2
            '剔退 Top5：查詢 def_code_1~4（an_weight 剔退重量）依班次合計
            strACCESS = String.Format("select top 5 code, sum(weight) from (" & _
                                        "select def_code_1 as code,an_weight as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_2 as code,an_weight as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_3 as code,an_weight as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select def_code_4 as code,an_weight as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                      ")top5 where code != '' group by code order by sum(weight) desc", _
                                      shift_date(shift).ToString("yyyyMMdd"), _
                                      shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            'fill data
            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算：an_weight (kg) → MT（/ 1000）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    '格式：缺陷代碼/重量(MT)
                    dtDataTable.Rows(i).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                '不足 5 筆補 N/A
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(i).Item(shift + 1) = "N/A"
                Next
            End If

            '其他缺陷 Top3：查詢 cd_code_1~4（cd_weight_1~4 非剔退缺陷重量）
            strACCESS = String.Format("select top 3 code, sum(weight) from (" & _
                                        "select cd_code_1 as code,cd_weight_1 as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_2 as code,cd_weight_2 as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_3 as code,cd_weight_3 as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        " union " & _
                                        "select cd_code_4 as code,cd_weight_4 as weight from h_pmis_wh73 where shift_date='{0}' and shift_code='{1}'" & _
                                        ")top3 where code != '' group by code order by sum(weight) desc", _
                                      shift_date(shift).ToString("yyyyMMdd"), _
                                      shift_sym_c(shift))

            dtTmp = execQuery(strACCESS, "", Conn)

            'fill data
            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算：cd_weight (kg) → MT（/ 1000）
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000
                    '填入第 5~7 列（row 5~7 = Top3 其他缺陷）
                    dtDataTable.Rows(i + 5).Item(shift + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                '不足 3 筆補 N/A
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

        '最新班欄位套用強調樣式
        For i As Integer = 0 To 7
            gvDaily.Rows(i).Cells(3).CssClass = "irondata0"
        Next

    End Sub

    ''' <summary>
    ''' 本月每日缺陷月報表（gvMonth）+ 本月累計
    ''' 逐日查詢兩類缺陷（剔退 Top5 + 其他缺陷 Top3）
    ''' 本月累計剔退 Top5：lblR1~lblR5
    ''' 本月累計其他缺陷 Top3：lblC1~lblC3
    ''' 單位換算：kg → MT（/ 1000）
    ''' </summary>
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

        '建立月報表欄位（剔退 Top5 + 其他缺陷 Top3，共 9 欄）
        'Month produce record
        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        '依本月天數建立每日列
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

        '逐日查詢兩類缺陷
        For idate As Integer = 1 To Date.DaysInMonth(Year([Today]), Month([Today]))
            '剔退 Top5（def_code_1~4/an_weight）
            strACCESS = "select top 5 code, sum(weight) from (" & _
                        "select def_code_1 as code,an_weight as weight " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_2,an_weight " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_3,an_weight " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select def_code_4,an_weight " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        ")top5 where code != '' group by code order by sum(weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算：kg → MT
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    dtDataTable.Rows(idate - 1).Item(i + 1) = dtTmp.Rows(i).Item(0) + "/" + calTmp.ToString("0.00")
                Next
                For i As Integer = dtTmp.Rows.Count To 4
                    dtDataTable.Rows(idate - 1).Item(i + 1) = "N/A"
                Next
            End If

            '其他缺陷 Top3（cd_code_1~4/cd_weight_1~4）
            strACCESS = "select top 3 code, sum(weight) from (" & _
                        "select cd_code_1 as code,cd_weight_1 as weight " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_2,cd_weight_2 " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_3,cd_weight_3 " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        " union " & _
                        "select cd_code_4,cd_weight_4 " & _
                        "from h_pmis_wh73 where shift_date = '" + Date.Today.ToString("yyyyMM") + idate.ToString("d2") + "'" + _
                        ")top3 where code != '' group by code order by sum(weight) desc"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then
                For i As Integer = 0 To dtTmp.Rows.Count - 1
                    '單位換算：kg → MT
                    calTmp = Val(dtTmp.Rows(i).Item(1).ToString) / 1000

                    '填入第 6~8 欄（欄位索引 6~8 = 其他缺陷 Top3）
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

        '本月累計剔退 Top5（lblR1~5）
        lblMonth.Text = Date.Today.ToString("MM")
        strACCESS = "select top 5 code, sum(weight) from (" & _
                    "select def_code_1 as code,an_weight as weight " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_2,an_weight " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_3,an_weight " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select def_code_4,an_weight " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
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
                            '單位換算：kg → MT
                            lblR1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算：kg → MT
                            lblR2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算：kg → MT
                            lblR3.Text = dtTmp.Rows(2).Item(0).ToString + "/" + (Val(dtTmp.Rows(2).Item(1).ToString) / 1000).ToString("0.00")
                        Case 3
                            '單位換算：kg → MT
                            lblR4.Text = dtTmp.Rows(3).Item(0).ToString + "/" + (Val(dtTmp.Rows(3).Item(1).ToString) / 1000).ToString("0.00")
                        Case 4
                            '單位換算：kg → MT
                            lblR5.Text = dtTmp.Rows(4).Item(0).ToString + "/" + (Val(dtTmp.Rows(4).Item(1).ToString) / 1000).ToString("0.00")
                    End Select
                End If
            Next
        End If

        '本月累計其他缺陷 Top3（lblC1~3）
        strACCESS = "select top 3 code, sum(weight) from (" & _
                    "select cd_code_1 as code,cd_weight_1 as weight " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_2,cd_weight_2 " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_3,cd_weight_3 " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    " union " & _
                    "select cd_code_4, cd_weight_4 " & _
                    "from h_pmis_wh73 where (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and (SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
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
                            '單位換算：kg → MT
                            lblC1.Text = dtTmp.Rows(0).Item(0).ToString + "/" + (Val(dtTmp.Rows(0).Item(1).ToString) / 1000).ToString("0.00")
                        Case 1
                            '單位換算：kg → MT
                            lblC2.Text = dtTmp.Rows(1).Item(0).ToString + "/" + (Val(dtTmp.Rows(1).Item(1).ToString) / 1000).ToString("0.00")
                        Case 2
                            '單位換算：kg → MT
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
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_2,an_weight " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_3,an_weight " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select def_code_4,an_weight " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
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
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_2,cd_weight_2 " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_3,cd_weight_3 " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
    '                    " union " & _
    '                    "select cd_code_4, cd_weight_4 " & _
    '                    "from h_pmis_wh73 where SUBSTRING(shift_date, 1, 4) = '" + tmpDate.ToString("yyyy") + "' and SUBSTRING(shift_date, 5, 2) = '" + tmpDate.ToString("MM") + "'" & _
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

    ''' <summary>
    ''' 主流程：建立 HPMIS 資料庫連線，依序執行三班日報表與月報表
    ''' </summary>
    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        TNRL1_Table()
        SumTable()
        'TeeChartData()
    End Sub
End Class
