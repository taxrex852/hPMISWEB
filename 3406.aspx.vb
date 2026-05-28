Imports System.Data.SqlClient
Imports System.Collections.Generic

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

    Private Sub Mainprocess()
        Conn = New SqlConnection(getHBMConnStr(Application("HBMConnStr")))
        BuildChartData()
        HBM_Table1()
    End Sub

    ''' <summary>
    ''' 建立 ECharts 趨勢圖 JSON 資料（近 12 個月）
    ''' </summary>
    Private Sub BuildChartData()
        Dim startYYYYMM As String = Date.Today.AddMonths(-11).ToString("yyyyMM")
        Dim endYYYYMM As String = Date.Today.ToString("yyyyMM")

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

        ' 確保近 12 個月皆有預設值，避免缺月
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

        ' 組合 ECharts JSON 字串
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
    ''' HBM 本月日報表 (gvMonth1)
    ''' </summary>
    Private Sub HBM_Table1()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "Hxx", "Fxx", "Lxx"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

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

        ' Hxx
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
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHxx.Text = calTmp.ToString("0.00")

        ' Fxx
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
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblFxx.Text = calTmp.ToString("0.00")

        ' Lxx
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
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLxx.Text = calTmp.ToString("0.00")

        gvMonth1.DataSource = dtDataTable
        gvMonth1.DataBind()
        gvMonth1.HeaderRow.Visible = False

        Conn.Close()
    End Sub

End Class