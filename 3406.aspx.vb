Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Public Class HBM_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3406"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            setTitle(Me, PAGE_ID)

            Dim args1 As New DataSourceSelectArguments
            Dim DR1 As DataView = CType(SqlDataSource1.Select(args1), DataView)

            If DR1 IsNot Nothing AndAlso DR1.Count > 0 Then
                Dim count As Integer = DR1.Count
                LabelStartdate.Text = Format(CDate(DR1(0)("boundle_date").ToString()), "yyyy/MM")
                LabelEnddate.Text = Format(CDate(DR1(count - 1)("boundle_date").ToString()), "yyyy/MM")

                ' --- ECharts 格式資料 (尺寸趨勢) ---
                Dim xAxis As New List(Of String)()
                Dim hxx As New List(Of Double)()
                Dim fxx As New List(Of Double)()
                Dim lxx As New List(Of Double)()

                For i As Integer = 0 To count - 1
                    xAxis.Add("'" & Convert.ToDateTime(DR1(i)("boundle_date")).ToString("yyyy/MM") & "'")
                    hxx.Add(If(IsDBNull(DR1(i)("prod_H")), 0, Convert.ToDouble(DR1(i)("prod_H"))))
                    fxx.Add(If(IsDBNull(DR1(i)("prod_F")), 0, Convert.ToDouble(DR1(i)("prod_F"))))
                    lxx.Add(If(IsDBNull(DR1(i)("prod_L")), 0, Convert.ToDouble(DR1(i)("prod_L"))))
                Next

                Dim script As String = "var chartData = {" &
                    "xAxis: [" & String.Join(",", xAxis) & "]," &
                    "hxx: [" & String.Join(",", hxx) & "]," &
                    "fxx: [" & String.Join(",", fxx) & "]," &
                    "lxx: [" & String.Join(",", lxx) & "]" &
                "};"

                ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
            End If

            Mainprocess()
        End If
    End Sub


    Private Sub HBM_Table1()
        Dim dtDataTable As New DataTable

        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "Hxx", "Fxx", "Lxx"}
        Dim strACCESS As String = Nothing
        Dim tmpValue As Double = 0

        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next

        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") + "月" + (i + 1).ToString("d2") + "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth1.Text = Date.Today.ToString("MM")

        Conn.Open()

        'Hxx
        strACCESS = "SELECT " & _
                    "SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " & _
                    "FROM h_pmis_hbm_info " & _
                    "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " & _
                    "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " & _
                    "Product_Size_Code like 'H%' " & _
                    "group by SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)


        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += Val(dtTmp.Rows(i).Item(1)) / 1000
        Next
        lblHxx.Text = calTmp.ToString("0.00")

        strACCESS = "SELECT " & _
                   "SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " & _
                   "FROM h_pmis_hbm_info " & _
                   "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " & _
                   "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " & _
                   "Product_Size_Code like 'F%' " & _
                   "group by SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2)"
        dtTmp = execQuery(strACCESS, "", Conn)

        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblFxx.Text = calTmp.ToString("0.00")

        'Lxx
        strACCESS = "SELECT " & _
                  "SUBSTRING(CONVERT(char, boundle_date, 112), 7, 2), SUM(bound_weight) " & _
                  "FROM h_pmis_hbm_info " & _
                  "WHERE SUBSTRING(CONVERT(char, boundle_date, 112), 1, 4) = " + Date.Today.ToString("yyyy") + " and " & _
                  "SUBSTRING(CONVERT(char, boundle_date, 112), 5, 2) = " + Date.Today.ToString("MM") + " and " & _
                  "Product_Size_Code like 'L%' " & _
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

        gvMonth1.Rows(0).Cells(0).Width = 100

        For i As Integer = 1 To 3
            gvMonth1.Rows(0).Cells(i).Width = 80
        Next

        Conn.Close()
    End Sub



    Private Sub Mainprocess()
        Conn = New SqlConnection(getHBMConnStr(Application("HBMConnStr"))) '1091229 新增HBMPMIS連線字串
        HBM_Table1()
        'TeeChartData1()

    End Sub


End Class