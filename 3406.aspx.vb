Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' HBM（高爐）產品尺寸分類生產統計頁面 (PAGE_ID=3406)
''' 資料來源：h_pmis_hbm_info（bound_weight 單位：g，輸出換算 MT = g/1000）
''' 分類方式：依 Product_Size_Code 前綴 — H%=Hxx、F%=Fxx、L%=Lxx
''' 圖表：近12個月各分類產量趨勢（ECharts）
''' 表格：本月每日分類產量（gvMonth1）+ 本月累計標籤（lblHxx/lblFxx/lblLxx）
''' 連線：getHBMConnStr（HBMPMIS 資料庫）
''' </summary>
Partial Public Class HBM_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3406"
    Private Conn As SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)

            ' 設定資料區間標題（近 12 個月）
            LabelStartdate.Text = Date.Today.AddMonths(-11).ToString("yyyy/MM")
            LabelEnddate.Text = Date.Today.ToString("yyyy/MM")

            Mainprocess()
        End If
    End Sub

    ''' <summary>
    ''' 主流程：建立 HBM 連線，依序執行圖表與表格
    ''' </summary>
    Private Sub Mainprocess()
        Conn = New SqlConnection(getHBMConnStr(Application("HBMConnStr")))
        BuildChartData()
        HBM_Table1()
    End Sub

    ''' <summary>
    ''' 建立 ECharts 趨勢圖 JSON 資料（近 12 個月）
    ''' 依 Product_Size_Code 前綴分三類：H%=Hxx、F%=Fxx、L%=Lxx
    ''' 各月產量加總，單位換算：g → MT（除以 1000）
    ''' 使用 WITH(NOLOCK) 避免查詢鎖定
    ''' 若某月無資料，預設填 0.00
    ''' </summary>
    Private Sub BuildChartData()
        Dim startYYYYMM As String = Date.Today.AddMonths(-11).ToString("yyyyMM")
        Dim endYYYYMM As String = Date.Today.ToString("yyyyMM")

        '依 Product_Size_Code 前綴分三類 (H/F/L)，月加總 bound_weight
        Dim sql As String =
            "SELECT DATEADD(m, DATEDIFF(m, 0, boundle_date), 0) AS boundle_date, " &
            "CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'H%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_H, " &
            "CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'F%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_F, " &
            "CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'L%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_L " &
            "FROM h_pmis_hbm_info WITH(NOLOCK) " &
            "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 6) BETWEEN '" & startYYYYMM & "' AND '" & endYYYYMM & "' " &
            "AND (Product_Size_Code LIKE 'H%' OR Product_Size_Code LIKE 'F%' OR Product_Size_Code LIKE 'L%') " &
            "GROUP BY DATEADD(m, DATEDIFF(m, 0, boundle_date), 0) " &
            "ORDER BY boundle_date"

        Conn.Open()
        Dim dt As DataTable = execQuery(sql, "", Conn)
        Conn.Close()

        ' 確保近 12 個月皆有預設值，避免缺月導致圖表斷點
        Dim dictH As New Dictionary(Of String, Double)
        Dim dictF As New Dictionary(Of String, Double)
        Dim dictL As New Dictionary(Of String, Double)
        Dim xAxis As New List(Of String)()

        For i As Integer = -11 To 0
            Dim m As String = Date.Today.AddMonths(i).ToString("yyyyMM")
            xAxis.Add("'" & Date.Today.AddMonths(i).ToString("yyyy/MM") & "'")
            dictH(m) = 0
            dictF(m) = 0
            dictL(m) = 0
        Next

        '將查詢結果填入對應月份字典
        If dt IsNot Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim m As String = Convert.ToDateTime(dt.Rows(i)("boundle_date")).ToString("yyyyMM")
                If dictH.ContainsKey(m) Then
                    dictH(m) = If(IsDBNull(dt.Rows(i)("prod_H")), 0, Convert.ToDouble(dt.Rows(i)("prod_H")))
                    dictF(m) = If(IsDBNull(dt.Rows(i)("prod_F")), 0, Convert.ToDouble(dt.Rows(i)("prod_F")))
                    dictL(m) = If(IsDBNull(dt.Rows(i)("prod_L")), 0, Convert.ToDouble(dt.Rows(i)("prod_L")))
                End If
            Next
        End If

        ' 組合 ECharts JSON 字串，注入至前端 JS
        Dim months As New List(Of String)(dictH.Keys)
        months.Sort()

        Dim hxx As New List(Of String)()
        Dim fxx As New List(Of String)()
        Dim lxx As New List(Of String)()

        For Each m As String In months
            hxx.Add(dictH(m).ToString("0.00"))
            fxx.Add(dictF(m).ToString("0.00"))
            lxx.Add(dictL(m).ToString("0.00"))
        Next

        Dim script As String =
            "var chartData = {" &
            "xAxis: [" & String.Join(",", xAxis) & "]," &
            "hxx:[" & String.Join(",", hxx) & "]," &
            "fxx:[" & String.Join(",", fxx) & "]," &
            "lxx:[" & String.Join(",", lxx) & "]" &
            "};"

        ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
    End Sub

    ''' <summary>
    ''' HBM 本月每日分類產量報表 (gvMonth1)
    ''' 欄位：日期 | Hxx | Fxx | Lxx（各依 Product_Size_Code LIKE 'H%'/'F%'/'L%' 篩選）
    ''' 單位換算：bound_weight (g) → MT（除以 1000）
    ''' 本月累計顯示於 lblHxx / lblFxx / lblLxx
    ''' </summary>
    Private Sub HBM_Table1()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "Hxx", "Fxx", "Lxx"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        '建立報表欄位
        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        '依本月天數建立每日列，預設各欄位為 "0.00"
        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Today), Month(Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") & "月" & (i + 1).ToString("d2") & "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth1.Text = Date.Today.ToString("MM")

        Conn.Open()
        Dim strACCESS As String

        ' Hxx：查詢本月每日 H 類產品產量
        strACCESS =
            "SELECT SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " &
            "FROM h_pmis_hbm_info " &
            "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " & Date.Today.ToString("yyyy") & " and " &
            "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " & Date.Today.ToString("MM") & " and " &
            "Product_Size_Code like 'H%' " &
            "group by SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            '單位換算：g → MT
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHxx.Text = calTmp.ToString("0.00") '本月累計 Hxx

        ' Fxx：查詢本月每日 F 類產品產量
        strACCESS =
            "SELECT SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " &
            "FROM h_pmis_hbm_info " &
            "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " & Date.Today.ToString("yyyy") & " and " &
            "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " & Date.Today.ToString("MM") & " and " &
            "Product_Size_Code like 'F%' " &
            "group by SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            '單位換算：g → MT
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblFxx.Text = calTmp.ToString("0.00") '本月累計 Fxx

        ' Lxx：查詢本月每日 L 類產品產量
        strACCESS =
            "SELECT SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " &
            "FROM h_pmis_hbm_info " &
            "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " & Date.Today.ToString("yyyy") & " and " &
            "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " & Date.Today.ToString("MM") & " and " &
            "Product_Size_Code like 'L%' " &
            "group by SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            '單位換算：g → MT
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLxx.Text = calTmp.ToString("0.00") '本月累計 Lxx

        gvMonth1.DataSource = dtDataTable
        gvMonth1.DataBind()
        gvMonth1.HeaderRow.Visible = False

        Conn.Close()
    End Sub

End Class
