Imports System.Data.SqlClient
Imports System.Collections.Generic













Partial Public Class _2TNRL_Produce
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3103"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            setTitle(Me, PAGE_ID)
            Dim args1 As New DataSourceSelectArguments

            Dim DR1 As DataView = SqlDataSource1.Select(args1)
            Dim count As Integer = DR1.Count

            LabelStartdate.Text = Format(CDate(DR1(0)(0).ToString), "yyyy/MM")
            LabelEnddate.Text = Format(CDate(DR1(count - 1)(0).ToString), "yyyy/MM")


            Dim xAxis As New List(Of String)()
            Dim pa As New List(Of Double)()
            Dim py As New List(Of Double)()
            Dim po As New List(Of Double)()
            Dim opr As New List(Of Double)()
            Dim mr As New List(Of Double)()


            For i As Integer = 0 To count - 1
                xAxis.Add("'" & Format(CDate(DR1(i)("process_date").ToString()), "yyyy/MM") & "'")
                pa.Add(Convert.ToDouble(DR1(i)("PA")))
                py.Add(Convert.ToDouble(DR1(i)("PY")))
                po.Add(Convert.ToDouble(DR1(i)("PO")))
                opr.Add(Convert.ToDouble(DR1(i)("OR")))
                mr.Add(Convert.ToDouble(DR1(i)("MR")))
            Next


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

    End Sub










    Private Sub TNRL2_Table()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing, dtTmp2 As DataTable = Nothing
        Dim bPO_pa_Ready As Boolean = True, bPO_coil_Ready As Boolean = True

        Dim dr As DataRow
        Dim strDailyTitle() As String = {" ", "單位", "班", "班", "班"}
        Dim strColName() As String = {"產量 (PA)", "產率 (PY)", "訂單合格率 (PO)", "作業率 (OR)", "剔退重量 (MR)"}
        Dim strUnitName() As String = {"MT", "%", "%", "%", "MT"}
        Dim strACCESS As String = "", strACCESS2 As String = ""


        Dim shift_num As String = "", shift_sym As String = "", shift_sym_c As String = ""

        Dim shift_date(2) As Date

        Dim calTmp As Double
        Dim slab_mw(2) As Integer


        Dim tmpPA(2) As Double


        Select Case Now.Hour
            Case 7 To 14
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "中夜早"
                shift_sym_c = "ANM"
                shift_num = "231"
            Case 15 To 22
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "夜早中"
                shift_sym_c = "NMA"
                shift_num = "312"
            Case 0 To 6
                shift_date(0) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
                shift_num = "123"
            Case 23
                shift_date(0) = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_date(1) = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_date(2) = Convert.ToDateTime(Date.Today.Date.AddDays(1) + " 23:00:00")
                shift_sym = "早中夜"
                shift_sym_c = "MAN"
                shift_num = "123"
        End Select


        strDailyTitle(2) = shift_date(0).ToString("yyyy.MM.dd") + " " + shift_sym(0) + strDailyTitle(2)
        strDailyTitle(3) = shift_date(1).ToString("yyyy.MM.dd") + " " + shift_sym(1) + strDailyTitle(3)
        strDailyTitle(4) = shift_date(2).ToString("yyyy.MM.dd") + " " + shift_sym(2) + strDailyTitle(4)

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

                            If dtDataTable.Rows(0).Item(shift + 2).ToString.Trim <> "N/A" Then

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







        For shift As Integer = 0 To 2
            bPO_pa_Ready = True
            bPO_coil_Ready = True


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















        Next






        For shift As Integer = 0 To 2
            strACCESS = "SELECT " & _
                        "SUM(acci_delay_time+roll_delay_time+shutdown_time+others_delay_time)," & _
                        "SUM(shutdown_time) " & _
                        "FROM h_pmis_si01 " & _
                        "WHERE line_id = 3 AND shift = " + shift_num(shift) + " " & _
                        "AND select_dates = '" + shift_date(shift).ToString("yyyyMMdd") + "'"
            dtTmp = execQuery(strACCESS, "", Conn)

            If dtTmp IsNot Nothing Then

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




        For shift As Integer = 0 To 2
            dtDataTable.Rows(4).Item(shift + 2) = "N/A"
        Next




        dtDataTable.Rows(0).Item(2) = (Val(dtDataTable.Rows(0).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(3) = (Val(dtDataTable.Rows(0).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(0).Item(4) = (Val(dtDataTable.Rows(0).Item(4).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(2) = (Val(dtDataTable.Rows(4).Item(2).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(3) = (Val(dtDataTable.Rows(4).Item(3).ToString) / 1000).ToString("0.00")
        dtDataTable.Rows(4).Item(4) = (Val(dtDataTable.Rows(4).Item(4).ToString) / 1000).ToString("0.00")


        gvDaily.DataSource = dtDataTable
        gvDaily.DataBind()
        gvDaily.Rows(0).Cells(0).Width = 200
        gvDaily.Rows(0).Cells(1).Width = 60
        gvDaily.Rows(0).Cells(2).Width = 180
        gvDaily.Rows(0).Cells(3).Width = 180
        gvDaily.Rows(0).Cells(4).Width = 180


        For i As Integer = 0 To 4
            gvDaily.Rows(i).Cells(4).CssClass = "irondata0"
        Next
    End Sub






    Private Sub SumTable()
        Dim dtDataTable As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dtMonthCoilWeight As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {" ", "PA/MT", "PY/%", "PO/%", "OR/%", "MR/MT"}
        Dim adapter As SqlDataAdapter = Nothing

        Dim strSQL_A As String = ""
        Dim strSQL_B As String = ""


        Dim calTmp As Double

        Dim sumPA, sumDelay, sumShutdown As Integer



        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next


        For i As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
        Next


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

        lblPA.Text = sumPA.ToString








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

                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(2) = Decimal.Round(dtTmp.Rows(iCount).Item(1), 2)
                End If
            Next
        End If



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

                    dtDataTable.Rows(dtTmp.Rows(iCount).Item(0) - 1).Item(3) = Decimal.Round(dtTmp.Rows(iCount).Item(1), 2)
                End If
            Next
        End If



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

        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                lblPO.Text = Val(dtTmp.Rows(0).Item(0)).ToString("0.00")
            Else
                lblPO.Text = "0.00"
            End If
        End If





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


        If sumShutdown = 480 * dtDataTable.Rows.Count Then
            calTmp = 0
        Else
            calTmp = 100 * (480 * dtDataTable.Rows.Count - sumDelay) / (480 * dtDataTable.Rows.Count - sumShutdown)
        End If
        lblOR.Text = calTmp.ToString("0.00")




        lblMR.Text = "0"





        For idate As Integer = 0 To Date.DaysInMonth(Year([Today]), Month([Today])) - 1
            dtDataTable.Rows(idate).Item(1) = (Val(dtDataTable.Rows(idate).Item(1).ToString) / 1000).ToString("0.00")
            dtDataTable.Rows(idate).Item(5) = (Val(dtDataTable.Rows(idate).Item(5).ToString) / 1000).ToString("0.00")
        Next
        lblPA.Text = (Val(lblPA.Text) / 1000).ToString("0.00")
        lblMR.Text = (Val(lblMR.Text) / 1000).ToString("0.00")


        gvMonth.DataSource = dtDataTable
        gvMonth.DataBind()
        gvMonth.HeaderRow.Visible = False


        gvMonth.Rows(0).Cells(0).Width = 200
        For i As Integer = 1 To 5
            gvMonth.Rows(0).Cells(i).Width = 120
        Next

        lblMonth.Text = Date.Today.ToString("MM")
    End Sub















































































































































































































































    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        TNRL2_Table()
        SumTable()

    End Sub
End Class