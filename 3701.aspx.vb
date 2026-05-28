Imports System.Data.SqlClient


Partial Public Class _3701
    Inherits System.Web.UI.Page
    Public Const PAGE_ID = "3701"
    Private g_sConn As SqlConnection
    Private g_sCmd As SqlCommand
    Private g_strSQL As String = Nothing
    Private g_strSQL2 As String = Nothing
    Private g_strSQL_time As String = Nothing
    Private g_aDataSet() As String
    Private g_aDataSet2() As String
    Private g_aDataSetHBM() As String
    Private g_aDataSet_T() As String
    Dim ConStatus As Integer
    Dim d_sumT1 As Decimal = 0
    Dim d_sumt2 As Decimal = 0
    Dim d_sumHBM As Decimal = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '取得user的uid, gid, name
            getPageUser(Me)

            Dim strErr As String = ""
            ''檢查權限
            'If chkPageAuth(Session("gid"), PAGE_ID, 1, getConnStr(Application("ConnStr")), strErr, Me) <> 0 Then Exit Sub

            '設定更新時間
            TimerALL.Enabled = False
            TimerALL.Interval = 30000 '30秒

            '主程式
            '#1 FCE + #2 FCE
            MainProcess()
            '#3 FCE 
            MainProcess_3()
            'HBM
            MainProcess_HBM()

            '啟動更新Timer1
            TimerALL.Enabled = True

            'MsgBox("CO訊號預計建立日期：100.12.25", MsgBoxStyle.OkOnly, "訊息")
        End If
    End Sub

    Private Sub MainProcess()

        Dim dtTmp As DataTable = Nothing
        Dim d_temp As Decimal = 0.01

        g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                               "FROM dbo.h_pmis_fi06 " &
                               "ORDER BY sys_process_date DESC "
        GetData_CO_T(1, True)

        '更新最後一次收到各程控的資料時間
        Read_Last_DataTime(g_aDataSet_T(0))


        d_sumT1 = 0

        g_strSQL = "SELECT max_co FROM vw_3702_maxco "
        GetData_CO(0, True)
        d_sumT1 = CDec(g_aDataSet(0))

        ShowData()

    End Sub

    Private Sub MainProcess_3()


        g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                               "FROM dbo.h_pmis_fi06_3 " &
                               "ORDER BY sys_process_date DESC "
        GetData_CO_T(1, True)

        '更新最後一次收到各程控的資料時間
        Read_Last_DataTime(g_aDataSet_T(0))

    End Sub

    Private Sub MainProcess_HBM()


        g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                               "FROM dbo.h_pmis_fp06 " &
                               "ORDER BY sys_process_date DESC "
        GetData_CO_THBM(1, True)

        '更新最後一次收到各程控的資料時間
        Read_Last_DataTimeHBM(g_aDataSet_T(0))

        d_sumHBM = 0

        g_strSQL2 = "SELECT max_co FROM vw_3703_maxco "
        GetData_CO_HBM(0, True)
        d_sumHBM = CDec(g_aDataSetHBM(0))

        ShowDataHBM()

    End Sub

    Private Sub Read_Last_DataTime(ByVal Processtime As DateTime)
        ' Label1.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
        If Now.AddMinutes(-15) > Processtime Then
            TextBox7.Text = "中斷"
            TextBox7.BackColor = Drawing.Color.Red
            TextBox6.Text = "N/A"
            TextBox6.BackColor = Drawing.Color.Red
            TextBox5.Text = "N/A"
            TextBox5.BackColor = Drawing.Color.Red
            ConStatus = 0
            'If Processtime.ToString("yyyy") = "1911" Then
            '    Label1.Text = "N/A"
            'Else
            '    Label1.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
            'End If
        Else
            TextBox7.Text = "正常"
            TextBox7.BackColor = Drawing.Color.Lime
            'TextBox6.BackColor = Drawing.Color.Lime
            'TextBox5.BackColor = Drawing.Color.Lime
            ConStatus = 1
        End If

    End Sub

    Private Sub Read_Last_DataTimeHBM(ByVal Processtime As DateTime)
        'Label12.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
        If Now.AddMinutes(-15) > Processtime Then
            TextBox14.Text = "中斷"
            TextBox14.BackColor = Drawing.Color.Red
            TextBox13.Text = "N/A"
            TextBox13.BackColor = Drawing.Color.Red
            TextBox12.Text = "N/A"
            TextBox12.BackColor = Drawing.Color.Red
            ConStatus = 0
            'If Processtime.ToString("yyyy") = "1911" Then
            '    Label12.Text = "N/A"
            'Else
            '    Label12.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
            'End If
        Else
            TextBox14.Text = "正常"
            TextBox14.BackColor = Drawing.Color.Lime
            'TextBox13.BackColor = Drawing.Color.Lime
            'TextBox12.BackColor = Drawing.Color.Lime
            ConStatus = 1
        End If

    End Sub


    Private Sub ShowData()
        Dim d_ppm As Decimal = 0

        If ConStatus = 1 Then
            'If Fn = 1 Then
            '    d_sumT1 = d_FCE
            'Else
            '    d_sumt2 = d_FCE
            'End If

            d_ppm = d_sumT1
            TextBox5.Text = d_ppm.ToString & " ppm"
            'ppm = Convert.ToInt32(FCE)
            fceppm.Text = d_ppm.ToString

            If d_ppm >= 35 Then
                TextBox6.Text = "危險"
                TextBox6.BackColor = Drawing.Color.Red
                TextBox5.BackColor = Drawing.Color.Red
            ElseIf d_ppm < 35 And d_ppm >= 0 Then
                TextBox6.Text = "安全"
                TextBox6.BackColor = Drawing.Color.Lime
                TextBox5.BackColor = Drawing.Color.Lime
            Else
                TextBox6.Text = "N/A"
                TextBox6.BackColor = Drawing.Color.Red

            End If
        Else
            TextBox5.Text = "N/A"
            TextBox5.BackColor = Drawing.Color.Red
            fceppm.Text = "N/A"
        End If

    End Sub

    Private Sub ShowDataHBM()
        Dim d_ppm As Decimal = 0

        If ConStatus = 1 Then
            'If Fn = 1 Then
            '    d_sumT1 = d_FCE
            'Else
            '    d_sumt2 = d_FCE
            'End If

            d_ppm = d_sumHBM
            TextBox12.Text = d_ppm.ToString & " ppm"
            'ppm = Convert.ToInt32(FCE)
            hbmfceppm.Text = d_ppm.ToString

            If d_ppm >= 35 Then
                TextBox13.Text = "危險"
                TextBox13.BackColor = Drawing.Color.Red
                TextBox12.BackColor = Drawing.Color.Red
            ElseIf d_ppm < 35 And d_ppm >= 0 Then
                TextBox13.Text = "安全"
                TextBox13.BackColor = Drawing.Color.Lime
                TextBox12.BackColor = Drawing.Color.Lime
            Else
                TextBox13.Text = "N/A"
                TextBox13.BackColor = Drawing.Color.Red

            End If
        Else
            TextBox12.Text = "N/A"
            TextBox12.BackColor = Drawing.Color.Red
            hbmfceppm.Text = "N/A"
        End If

    End Sub



    Private Sub GetData_CO(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=localhost;Initial Catalog=TEST2;User ID=sa;Password=1234"

        If redimArray Then : ReDim g_aDataSet(intCount) : End If

        Try
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            'g_sConn = New SqlConnection(strDbCon)
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



    Private Sub GetData_CO_T(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=.;Initial Catalog=TEST2;User ID=sa;Password=1234"

        If redimArray Then : ReDim g_aDataSet_T(intCount) : End If

        Try

            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            g_sCmd = New SqlCommand(g_strSQL_time, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()
            If rd.HasRows() Then
                rd.Read()
                g_aDataSet_T(0) = rd.Item(0)
            Else
                g_aDataSet_T(0) = #1/1/1911#
            End If
            g_sConn.Close()

        Catch sErr As SqlException
            'MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try
    End Sub

    Private Sub GetData_CO_THBM(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=.;Initial Catalog=TEST2;User ID=sa;Password=1234"

        If redimArray Then : ReDim g_aDataSet_T(intCount) : End If

        Try

            g_sConn = New SqlConnection(getHBMConnStr(Application("HBMConnStr")))
            g_sCmd = New SqlCommand(g_strSQL_time, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()
            If rd.HasRows() Then
                rd.Read()
                g_aDataSet_T(0) = rd.Item(0)
            Else
                g_aDataSet_T(0) = #1/1/1911#
            End If
            g_sConn.Close()

        Catch sErr As SqlException
            'MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try
    End Sub

    Private Sub GetData_CO_HBM(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=localhost;Initial Catalog=TEST2;User ID=sa;Password=1234"

        If redimArray Then : ReDim g_aDataSetHBM(intCount) : End If

        Try
            g_sConn = New SqlConnection(getHBMConnStr(Application("HBMConnStr")))
            'g_sConn = New SqlConnection(strDbCon)
            g_sCmd = New SqlCommand(g_strSQL2, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows Then

                While rd.Read

                    For i As Integer = 0 To rd.FieldCount - 1 Step 1
                        g_aDataSetHBM(i + startIdx) = rd.Item(i).ToString
                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            'MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Private Sub TimerALL_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerALL.Tick
        '主程式
        MainProcess()
        MainProcess_3()
        MainProcess_HBM()
        'ShowData(arrDeName(0), Sum.ToString)
    End Sub
End Class