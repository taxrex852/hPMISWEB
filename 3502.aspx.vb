Imports System.Data.SqlClient

Partial Public Class _1TNRL_Process
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3502"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date
    Private Const DeviceDownThreshold As Integer = 10

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)
            Timer1.Enabled = False
            Timer1.Interval = 60000
            createtable()
            Timer1.Enabled = True
        End If
    End Sub

    ' 判斷是否超過DeviceDownThreshold分鐘未收到WH93
    ' 102.09.17 修改擷取條件和戰情中心相同
    Private Function CheckDeviceStatus(ByVal Conn As SqlConnection) As Boolean
        Dim dtTmp As DataTable = Nothing
        Dim strSQL As String = ""
        Dim bDeviceStatus As Boolean = False
        Dim shift_date As Date

        'strSQL = "SELECT TOP 1 process_date FROM h_pmis_wh93 ORDER BY process_date DESC"
        'dtTmp = execQuery(strSQL, "", Conn)
        'If dtTmp IsNot Nothing Then
        '    If dtTmp.Rows.Count > 0 Then
        '        If CType(dtTmp.Rows(0).Item(0), Date).AddMinutes(DeviceDownThreshold) >= Now Then
        '            bDeviceStatus = True
        '        End If
        '    End If
        'End If

        'Return bDeviceStatus
        Select Case Now.Hour
            Case 7 To 14 'M
                shift_date = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
            Case 15 To 22 'A
                shift_date = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
            Case 0 To 6 'N
                shift_date = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 23:00:00")
            Case 23 'N
                shift_date = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
        End Select

        strSQL = "SELECT SUM(an_weight) " & _
                    "FROM h_pmis_wh93" & _
                    " WHERE product_date+product_time BETWEEN '" + shift_date.ToString("yyyyMMddHHmmss") + _
                    "' AND '" + shift_date.AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") + "'"
        dtTmp = execQuery(strSQL, "", Conn)

        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
                    If Val(dtTmp.Rows(0).Item(0).ToString) > 0 Then
                        bDeviceStatus = True
                    End If
                End If

            End If
            dtTmp.Dispose()
        Else
            dtTmp = Nothing
            strSQL = "SELECT SUM(an_weight) " & _
                    "FROM h_pmis_wh93" & _
                    " WHERE product_date+product_time BETWEEN '" + shift_date.ToString("yyyyMMddHHmmss") + _
                    "' AND '" + shift_date.AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") + "'"
            dtTmp = execQuery(strSQL, "", Conn)

            If dtTmp IsNot Nothing Then
                If dtTmp.Rows.Count > 0 Then
                    If Not IsDBNull(dtTmp.Rows(0).Item(0)) Then
                        If Val(dtTmp.Rows(0).Item(0).ToString) > 0 Then
                            bDeviceStatus = True
                        End If
                    End If

                End If
                dtTmp.Dispose()
            End If
        End If
        Return bDeviceStatus
    End Function

    Private Sub createtable()
        Dim dtTmp As DataTable = Nothing
        Dim shift_date As Date
        Dim shift_sym As String = ""

        Select Case Now.Hour
            Case 7 To 14 'M
                shift_date = Convert.ToDateTime(Date.Today.Date + " 07:00:00")
                shift_sym = "M"
            Case 15 To 22 'A
                shift_date = Convert.ToDateTime(Date.Today.Date + " 15:00:00")
                shift_sym = "A"
            Case 0 To 6 'N
                shift_date = Convert.ToDateTime(Date.Today.Date.AddDays(-1) + " 23:00:00")
                shift_sym = "N"
            Case 23 'N
                shift_date = Convert.ToDateTime(Date.Today.Date + " 23:00:00")
                shift_sym = "N"
        End Select

        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        Conn.Open()

        'shift
        'strACCESS = "SELECT SUM(an_weight/1000) " & _
        '            "FROM h_pmis_wh93 " & _
        '            "WHERE product_date+product_time BETWEEN '" + shift_date.ToString("yyyyMMddHHmmss") + _
        '            "' AND '" + shift_date.AddHours(7).AddMinutes(59).AddSeconds(59).ToString("yyyyMMddHHmmss") + "'"
        strACCESS = String.Format("select ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod, ISNULL(A.product_day, B.product_day) as ProductDay from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                        "SUM(g_weight) as product_weight from h_pmis_wh93 " & _
                        "where shift_date='{0}' and shift_code='{1}' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight " & _
                        "from h_pmis_wh9b where shift_date='{0}' and shift_code='{1}' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                    "ON A.product_day = B.product_day " & _
                    "ORDER BY ProductDay", _
                    shift_date.ToString("yyyyMMdd"), _
                    shift_sym)

        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                    lblShift.Text = (Val(dtTmp.Rows(0).Item(0)) / 1000).ToString("0.00")
                Else
                    lblShift.Text = "N/A"
                End If
            Else
                lblShift.Text = "N/A"
            End If
        Else
            lblShift.Text = "N/A"
        End If

        'day
        'strACCESS = "SELECT SUM(an_weight/1000) " & _
        '            "FROM h_pmis_wh93 " & _
        '            "WHERE (product_date = " + Date.Today.ToString("yyyyMMdd") + ")"
        strACCESS = String.Format("select ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod, ISNULL(A.product_day, B.product_day) as ProductDay from " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, " & _
                        "SUM(g_weight) as product_weight from h_pmis_wh93 " & _
                        "where shift_date='{0}' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight " & _
                        "from h_pmis_wh9b where shift_date='{1}' " & _
                        "Group by SUBSTRING(shift_date, 7, 2)) as B " & _
                    "ON A.product_day = B.product_day " & _
                    "ORDER BY ProductDay", _
                    Now.ToString("yyyyMMdd"), _
                    Now.ToString("yyyyMMdd"))
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                    lblDay.Text = (Val(dtTmp.Rows(0).Item(0)) / 1000).ToString("0.00")
                Else
                    lblDay.Text = "N/A"
                End If
            Else
                lblDay.Text = "N/A"
            End If
        Else
            lblDay.Text = "N/A"
        End If

        'month
        'strACCESS = "SELECT SUM(an_weight/1000) " & _
        '            "FROM h_pmis_wh93 " & _
        '            "WHERE (Year(product_date) = " + Date.Today.ToString("yyyy") + ") and " & _
        '            "(Month(product_date) = " + Date.Today.ToString("MM") + ")"
        strACCESS = String.Format("select ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod, ISNULL(A.product_month, B.product_month) as ProductMonth from " & _
                        "(select SUBSTRING(shift_date, 5, 2) as product_month, " & _
                        "SUM(g_weight) as product_weight from h_pmis_wh93 " & _
                        "where shift_date like '{0}%' " & _
                        "Group by SUBSTRING(shift_date, 5, 2)) as A " & _
                    "FULL OUTER JOIN " & _
                        "(select SUBSTRING(shift_date, 5, 2) as product_month, SUM(gross_weight) as product_weight " & _
                        "from h_pmis_wh9b where shift_date like '{1}%' " & _
                        "Group by SUBSTRING(shift_date, 5, 2)) as B " & _
                    "ON A.product_month = B.product_month " & _
                    "ORDER BY ProductMonth", _
                    Now.ToString("yyyyMM"), _
                    Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                    lblMonth.Text = (Val(dtTmp.Rows(0).Item(0)) / 1000).ToString("0.00")
                Else
                    lblMonth.Text = "N/A"
                End If
            Else
                lblMonth.Text = "N/A"
            End If
        Else
            lblMonth.Text = "N/A"
        End If

        strACCESS = "SELECT top 1 " & _
                    "product_date, product_time, material_no, thickness_m/100, width_m " & _
                    "FROM h_pmis_wh93 " & _
                    "WHERE (SUBSTRING(shift_date, 1, 4) = '" + Date.Today.ToString("yyyy") + "') and " & _
                    "(SUBSTRING(shift_date, 5, 2) = '" + Date.Today.ToString("MM") + "') " & _
                    "ORDER BY product_date+product_time DESC "
        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count <> 0 Then
                lblData.Text = dtTmp.Rows(0).Item(0).ToString().Substring(0, 4) & "/" & _
                               dtTmp.Rows(0).Item(0).ToString().Substring(4, 2) & "/" & dtTmp.Rows(0).Item(0).ToString().Substring(6, 2)
                lblData.Text += " " & dtTmp.Rows(0).Item(1).ToString().Substring(0, 2) & ":" & _
                                dtTmp.Rows(0).Item(1).ToString().Substring(2, 2) & ":" & dtTmp.Rows(0).Item(1).ToString().Substring(4, 2)
                lblT.Text = Val(dtTmp.Rows(0).Item(3)).ToString("0.00")
                lblW.Text = dtTmp.Rows(0).Item(4).ToString
                lblLast.Text = dtTmp.Rows(0).Item(2).ToString

                strACCESS = "SELECT top 1 " & _
                            "specification " & _
                            "FROM h_pmis_wh91 " & _
                            "WHERE coil_no = '" + dtTmp.Rows(0).Item(2).ToString.Trim + "'"
                dtTmp = execQuery(strACCESS, "", Conn)
                If dtTmp.Rows.Count <> 0 Then
                    lblKind.Text = dtTmp.Rows(0).Item(0).ToString
                Else
                    lblKind.Text = "N/A"
                End If
            Else
                lblData.Text = "N/A"
                lblT.Text = "N/A"
                lblW.Text = "N/A"
                lblKind.Text = "N/A"
                lblLast.Text = "N/A"
            End If
        End If

        lblNow.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        '100.08.03林弘男要求修正
        If CheckDeviceStatus(Conn) = True Then
            lblStatus.Text = "目前正在生產中！"
            lblStatus.ForeColor = Drawing.Color.Blue
        Else
            lblStatus.Text = "目前產線暫停中！"
            lblStatus.ForeColor = Drawing.Color.Firebrick
        End If
        'lblStatus.Visible = CheckDeviceStatus(Conn)
        Conn.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        createtable()
    End Sub

    Private Sub Link3503_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link3503.Click
        Response.Redirect("http://w4mes.dsc.com.tw/3503.aspx")
    End Sub

    Private Sub Link3504_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Link3504.Click
        Response.Redirect("http://w4mes.dsc.com.tw/3504.aspx")
    End Sub

    Protected Sub Link3507_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Link3507.Click
        Response.Redirect("http://w4mes.dsc.com.tw/3507.aspx")
    End Sub
End Class