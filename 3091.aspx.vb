Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.NetworkInformation

Partial Public Class _3091
    Inherits System.Web.UI.Page
    Public Const PAGE_ID = "3091"
    Private g_sConn As SqlConnection
    Private g_sCmd As SqlCommand
    Private g_strSQL As String = Nothing
    Private g_aDataSet() As String

    Private Const pcHOST_bad = "~/images/pc_host_bad.jpg"
    Private Const pcHOST_normal = "~/images/pc_host_normal.jpg"
    Private Const pcPMIS_bad = "~/images/pc_pmis_bad.jpg"
    Private Const pcPMIS_normal = "~/images/pc_pmis_normal.jpg"
    Private Const pcSVR_bad = "~/images/pc_svr_bad.jpg"
    Private Const pcSVR_normal = "~/images/pc_svr_normal.jpg"

    Private Sub GetData(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        If redimArray Then : ReDim g_aDataSet(intCount) : End If

        Try
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))

            g_sCmd = New SqlCommand(g_strSQL, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows Then
                While rd.Read
                    For i As Integer = 0 To rd.FieldCount - 1 Step 1
                        g_aDataSet(i + startIdx) = rd.Item(i).ToString
                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            'MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '取得user的uid, gid, name
            getPageUser(Me)

            Dim strErr As String = ""
            '檢查權限
            If chkPageAuth(Session("gid"), PAGE_ID, 1, getConnStr(Application("ConnStr")), strErr, Me) <> 0 Then Exit Sub

            '設定更新時間
            Timer1.Enabled = False
            Timer1.Interval = 60000 '60秒

            '主程式
            MainProcess()

            '啟動更新Timer1
            Timer1.Enabled = True


        End If
    End Sub

    Private Sub MainProcess()
        Dim arrPcName() As String = {"CYMC", "FCE", "HRFSPC", "HRS", "MIL", "PYMC", "SPC", "SYMC", "TG", "TNRL1", "TNRL2", "TNRL3", "TNRL4", "DYMC", "HBMMIL", "IPMIS", "UDC", "sPMIS", "HOST"}

        For i As Integer = 0 To arrPcName.Length - 1
            g_strSQL = "SELECT status " &
                        "FROM sys_pc_conn_sts " &
                        "WHERE name = '" + arrPcName(i) + "' "

            GetData(1, True)
            ShowData(arrPcName(i), g_aDataSet(0).ToUpper)

        Next

        '更新最後一次收到各程控的資料時間
        'Read_Last_DataTime()
    End Sub

    Private Sub ShowData(ByVal strPcName As String, ByVal bStatus As Char)
        'status => E: offline , N: online


        Select Case strPcName.ToUpper.Trim
            Case "CYMC"
                If bStatus = "N" Then
                    imgCYMC.ImageUrl = pcSVR_normal
                    lblCymc_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgCYMC.ImageUrl = pcSVR_bad
                End If
            Case "FCE"
                If bStatus = "N" Then
                    imgFCE.ImageUrl = pcSVR_normal
                    lblFce_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgFCE.ImageUrl = pcSVR_bad
                End If
            Case "HRFSPC"
                If bStatus = "N" Then
                    imgHRFSPC.ImageUrl = pcSVR_normal
                    lblHrfspc_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHRFSPC.ImageUrl = pcSVR_bad
                End If
            Case "HRS"
                If bStatus = "N" Then
                    imgHRS.ImageUrl = pcSVR_normal
                    lblHrs_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHRS.ImageUrl = pcSVR_bad
                End If
            Case "MIL"
                If bStatus = "N" Then
                    imgMIL.ImageUrl = pcSVR_normal
                    lblMil_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgMIL.ImageUrl = pcSVR_bad
                End If
            Case "PYMC"
                If bStatus = "N" Then
                    imgPYMC.ImageUrl = pcSVR_normal
                    lblPymc_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgPYMC.ImageUrl = pcSVR_bad
                End If
            Case "SPC"
                If bStatus = "N" Then
                    imgSPC.ImageUrl = pcSVR_normal
                    lblSPC_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgSPC.ImageUrl = pcSVR_bad
                End If
                'Case "SQC"
                '    If bStatus = "N" Then
                '        imgSQC.ImageUrl = pcSVR_normal
                '    Else
                '        imgSQC.ImageUrl = pcSVR_bad
                '    End If
            Case "SYMC"
                If bStatus = "N" Then
                    imgSYMC.ImageUrl = pcSVR_normal
                    lblSymc_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgSYMC.ImageUrl = pcSVR_bad
                End If
            Case "TG"
                If bStatus = "N" Then
                    imgTG.ImageUrl = pcSVR_normal
                    lblTg_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTG.ImageUrl = pcSVR_bad
                End If
            Case "TNRL1"
                If bStatus = "N" Then
                    imgTNRL1.ImageUrl = pcSVR_normal
                    lblTnrl1_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTNRL1.ImageUrl = pcSVR_bad
                End If
            Case "TNRL2"
                If bStatus = "N" Then
                    imgTNRL2.ImageUrl = pcSVR_normal
                    lblTnrl2_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTNRL2.ImageUrl = pcSVR_bad
                End If
            Case "IPMIS"
                If bStatus = "N" Then
                    imgiPMIS.ImageUrl = pcPMIS_normal
                    lblIpmis_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgiPMIS.ImageUrl = pcPMIS_bad
                End If
            Case "UDC"
                If bStatus = "N" Then
                    imgUDC.ImageUrl = pcPMIS_normal
                    lblUDC_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgUDC.ImageUrl = pcPMIS_bad
                End If
                'Case "SPMIS"
                '    If bStatus = "N" Then
                '        imgsPMIS.ImageUrl = pcPMIS_normal
                '    Else
                '        imgsPMIS.ImageUrl = pcPMIS_bad
                '    End If
            Case "HOST"
                If bStatus = "N" Then
                    imgHOST.ImageUrl = pcHOST_normal

                Else
                    imgHOST.ImageUrl = pcHOST_bad
                End If
            'Case "HBMMIL"
            '    If bStatus = "N" Then
            '        imgHBMMIL.ImageUrl = pcSVR_normal
            '    Else
            '        imgHBMMIL.ImageUrl = pcSVR_bad
            '    End If
            Case "TNRL3"
                If bStatus = "N" Then
                    imgTNRL3.ImageUrl = pcSVR_normal
                    lblTNRL3_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTNRL3.ImageUrl = pcSVR_bad
                End If
            Case "TNRL4"
                If bStatus = "N" Then
                    imgTNRL4.ImageUrl = pcSVR_normal
                    lblTNRL4_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTNRL4.ImageUrl = pcSVR_bad
                End If
            Case "HRS"
                If bStatus = "N" Then
                    imgTNRL3.ImageUrl = pcSVR_normal
                    lblTNRL3_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgTNRL3.ImageUrl = pcSVR_bad
                End If
            Case "DYMC"
                If bStatus = "N" Then
                    imgDYMC.ImageUrl = pcSVR_normal
                    lblDymc_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                        imgDYMC.ImageUrl = pcSVR_bad
                    End If
            End Select
    End Sub

    Private Sub Read_Last_DataTime()
        Dim strSQL As String = Nothing

        Try
            'CYMC (YS03)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_ys03 " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblCymc_t.Text = DateFormat(g_aDataSet(0), lblCymc_t)

            'FCE (FI05)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_fi05 " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblFce_t.Text = DateFormat(g_aDataSet(0), lblFce_t)

            'HRFSPC (WHC1 = HOST => HRFSPC)


            'HRS (WR01: once a month) 
            strSQL = "SELECT TOP(1) timestamp FROM di_pdo_hrs WHERE queueid='435200WR01' " &
                     "AND timestamp > '" + WinTimeToUnixtime(Now.AddDays(-(FIND_DATA_DAYS + 30))).ToString + "' " &
                     "ORDER BY timestamp DESC"
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            If g_aDataSet(0) IsNot Nothing Then
                lblHrs_t.Text = DateFormat(UnixtimeToWinTime(Val(g_aDataSet(0))).ToString, lblHrs_t, False)
            Else
                lblHrs_t.Text = ""
            End If


            'MIL (MI01)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_mi01 " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblMil_t.Text = DateFormat(g_aDataSet(0), lblMil_t)

            'PYMC (PH01)->(PI01)
            '99.10.20
            'strSQL = "SELECT TOP(1) timestamp FROM di_pdo_pymc WHERE queueid='4D5200PH01' " & _
            '         "AND timestamp > '" + WinTimeToUnixtime(Now.AddDays(-FIND_DATA_DAYS)).ToString + "' " & _
            '         "ORDER BY timestamp DESC"
            'g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            'If g_aDataSet(0) IsNot Nothing Then
            '    lblPymc_t.Text = DateFormat(UnixtimeToWinTime(Val(g_aDataSet(0))).ToString, lblPymc_t, False)
            'Else
            '    lblPymc_t.Text = ""
            'End If
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_pi01 " &
                      "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblPymc_t.Text = DateFormat(g_aDataSet(0), lblPymc_t)

            'SPC (WHQH)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_whqh " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblSPC_t.Text = DateFormat(g_aDataSet(0), lblSPC_t)

            'SQC

            'SYMC (ISYH)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_isyh " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblSymc_t.Text = DateFormat(g_aDataSet(0), lblSymc_t)

            'TG (TQ61)
            strSQL = "SELECT TOP(1) timestamp FROM di_pdo_tg WHERE queueid='4C5200TQ61' " &
                     "AND timestamp > '" + WinTimeToUnixtime(Now.AddDays(-FIND_DATA_DAYS)).ToString + "' " &
                     "ORDER BY timestamp DESC"
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            If g_aDataSet(0) IsNot Nothing Then
                lblTg_t.Text = DateFormat(UnixtimeToWinTime(Val(g_aDataSet(0))).ToString, lblTg_t, False)
            Else
                lblTg_t.Text = ""
            End If

            'TNRL1 (TI11)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_ti11 " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblTnrl1_t.Text = DateFormat(g_aDataSet(0), lblTnrl1_t)

            'TNRL2 (TI21)
            strSQL = "SELECT TOP(1) sys_process_date FROM h_pmis_ti21 " &
                     "WHERE CONVERT(VARCHAR,sys_process_date,112) > '" + Now.AddDays(-FIND_DATA_DAYS).ToString("yyyyMMdd") + "' " &
                     "ORDER BY sys_process_date DESC "
            g_aDataSet = GetDataToStringArray(getConnStr(Application("ConnStr")), strSQL, 1)
            lblTnrl2_t.Text = DateFormat(g_aDataSet(0), lblTnrl2_t)

            'IPMIS
        Catch ex As Exception
            showMsgbox("[Read_Last_DataTime] Error :" + ex.Message, Me, Me.UpdatePanel1)
        End Try

    End Sub

    Private Function DateFormat(ByVal strDate As String, ByVal objLabel As Label, Optional ByVal bTimeout As Boolean = True) As String
        'date string 轉換為 日期格式
        Dim strReturn As String = ""
        objLabel.ForeColor = Drawing.Color.Black

        Try
            If Not String.IsNullOrEmpty(strDate) Then
                If IsDate(strDate) Then
                    Dim dDate As DateTime = Convert.ToDateTime(strDate)

                    strReturn = dDate.ToString("MM/dd HH:mm")
                    If dDate < Now.AddHours(-1) And bTimeout = True Then '時間超過1小時以上
                        objLabel.ForeColor = Drawing.Color.Red
                    Else
                        objLabel.ForeColor = Drawing.Color.Black
                    End If

                End If
            End If

        Catch ex As Exception
            showMsgbox("[DateFormat] Error :" + strDate, Me, Me.UpdatePanel1)
        End Try

        Return strReturn
    End Function

    Protected Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '主程式
        MainProcess()
    End Sub

    Function UnixtimeToWinTime(ByVal unixtime As Long) As DateTime
        Dim dWinTime As DateTime = DateTime.Parse("1970/01/01 00:00:00")
        dWinTime = dWinTime.AddMilliseconds(unixtime / 1000)
        Return dWinTime
    End Function

    Function WinTimeToUnixtime(ByVal dWinTime As DateTime) As Long
        Dim diff As TimeSpan = dWinTime.Subtract(New DateTime(1970, 1, 1, 0, 0, 0))
        Dim unixtime As Long = CType(diff.TotalMilliseconds, Long) * 1000
        Return unixtime
    End Function

End Class