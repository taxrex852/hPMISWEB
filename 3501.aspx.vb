Imports System.Data.SqlClient

Partial Public Class HSM_Process
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3501"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date
    ' 停機判斷門檻（單位：分鐘）
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

    ' 判斷是否超過DeviceDownThreshold分鐘未收到WHQA
    Private Function CheckDeviceStatus(ByVal Conn As SqlConnection) As Boolean
        Dim dtTmp As DataTable = Nothing
        Dim strSQL As String = ""
        Dim bDeviceStatus As Boolean = False

        strSQL = "SELECT TOP 1 process_date FROM h_pmis_whqa ORDER BY process_date DESC"
        dtTmp = execQuery(strSQL, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If CType(dtTmp.Rows(0).Item(0), Date).AddMinutes(DeviceDownThreshold) >= Now Then
                    bDeviceStatus = True
                End If
            End If
        End If

        Return bDeviceStatus
    End Function

    Private Sub createtable()
        Dim dtTmp As DataTable = Nothing
        Dim shift_code As String

        '依現在時間決定班別
        Select Case Now.Hour
            Case 7 To 14
                shift_code = "M"
            Case 15 To 22
                shift_code = "A"
            Case Else
                shift_code = "N"
        End Select

        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        Conn.Open()

        'shift production
        'strACCESS = "SELECT " & _
        '            "SUM(ccweight) " & _
        '            "FROM h_pmis_mi02 " & _
        '            "WHERE dccdate = " + Date.Today.ToString("yyyyMMdd") + " AND shift_code = '" + shift_code + "'"
        'dtTmp = execQuery(strACCESS, "", Conn)

        'If dtTmp IsNot Nothing Then
        '    If dtTmp.Rows(0).Item(0) Is DBNull.Value Then
        '        strACCESS = "SELECT " & _
        '                    "SUM(coil_weight) " & _
        '                    "FROM h_pmis_whqa " & _
        '                    "WHERE datep = " + Date.Today.ToString("yyyyMMdd") + " AND shift_code = '" + shift_code + "'"
        '        dtTmp = execQuery(strACCESS, "", Conn)

        '        If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
        '            lblShift.Text = dtTmp.Rows(0).Item(0).ToString("0.00")
        '        Else
        '            lblShift.Text = "N/A"
        '        End If
        '    Else
        '        '單位換算
        '        lblShift.Text = (Val(dtTmp.Rows(0).Item(0).ToString) / 1000).ToString("0.00") '班產量
        '    End If
        'End If

        lblShift.Text = "N/A"
        strACCESS = "SELECT SUM(coil_weight) FROM h_pmis_coil_info where " & _
                    "reject_reason = 0 AND  SUBSTRING(CONVERT(char, product_date, 112), 1, 8)='" + Now.ToString("yyyyMMdd") + "' AND shift_code='" + shift_code + "'"
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing Then
            If dtTmp.Rows.Count > 0 Then
                If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                    lblShift.Text = (Val(dtTmp.Rows(0).Item(0)) / 1000).ToString("0.00")
                Else
                    lblShift.Text = "N/A"
                End If
            End If
            dtTmp.Dispose()
        End If

        'day production
        'strACCESS = "SELECT " & _
        '            "SUM(ccweight) " & _
        '            "FROM h_pmis_mi02 " & _
        '            "WHERE dccdate = " + Date.Today.ToString("yyyyMMdd")
        strACCESS = "SELECT " & _
                    "SUM(coil_weight) " & _
                    "FROM h_pmis_coil_info " & _
                    "WHERE reject_reason = 0 AND SUBSTRING(CONVERT(char, product_date, 112), 1, 8) = '" + Date.Today.ToString("yyyyMMdd") + "'"
        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                '單位換算
                lblDay.Text = (Val(dtTmp.Rows(0).Item(0).ToString) / 1000).ToString("0.00") '日產量
            Else
                lblDay.Text = "N/A"
            End If
        End If

        'month production
        'strACCESS = "SELECT " & _
        '            "SUM(ccweight) " & _
        '            "FROM h_pmis_mi02 " & _
        '            "WHERE (Year(dccdate) = " + Date.Today.ToString("yyyy") + ") and " & _
        '            "(Month(dccdate) = " + Date.Today.ToString("MM") + ") "
        strACCESS = "SELECT " & _
                    "SUM(coil_weight) " & _
                    "FROM h_pmis_coil_info " & _
                    "WHERE reject_reason = 0 AND SUBSTRING(CONVERT(char, product_date, 112), 1, 6) ='" + Now.ToString("yyyyMM") + "'"
        dtTmp = execQuery(strACCESS, "", Conn)

        If dtTmp IsNot Nothing Then
            If dtTmp.Rows(0).Item(0) IsNot DBNull.Value Then
                '單位換算
                lblMonth.Text = (Val(dtTmp.Rows(0).Item(0).ToString) / 1000).ToString("0.00") '月產量
            Else
                lblMonth.Text = "N/A"
            End If
        End If

        strACCESS = "SELECT top 1 " & _
                    "dccdate, dcctime, coil_id, ftargett/1000, ftargetw, steal_spec " & _
                    "FROM h_pmis_mi02 " & _
                    "WHERE (Year(dccdate) = " + Date.Today.ToString("yyyy") + ") and " & _
                    "(Month(dccdate) = " + Date.Today.ToString("MM") + ") " & _
                    "ORDER BY dccdate+dcctime DESC "
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

                lblKind.Text = dtTmp.Rows(0).Item(5).ToString

                'strACCESS = "SELECT top 1 " & _
                '            "steel_spec " & _
                '            "FROM h_pmis_whq1 " & _
                '            "WHERE coil_id = '" + dtTmp.Rows(0).Item(2).ToString.Trim + "'"
                'dtTmp = execQuery(strACCESS, "", Conn)
                'If dtTmp.Rows.Count <> 0 Then
                '    lblKind.Text = dtTmp.Rows(0).Item(0).ToString
                'Else
                '    lblKind.Text = "N/A"
                'End If
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
End Class