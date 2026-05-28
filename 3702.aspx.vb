Imports System.Data.SqlClient

Partial Public Class _HSM3702

    Inherits System.Web.UI.Page
    Public Const PAGE_ID = "3702"
    Private g_sConn As SqlConnection
    Private g_sCmd As SqlCommand
    Private g_strSQL As String = Nothing
    Private g_strSQL_maxco As String = Nothing
    Private g_strSQL_time As String = Nothing
    Private g_aDataSet_maxco() As String
    Private g_aDataSet() As String
    Private g_aDataSet2() As Date
    Private g_aDataSetWind() As String
    Dim CoValue_max As Double
    Dim CoValue(33) As Integer
    Dim ConS(35) As Integer
    Dim ProcessT(5) As DateTime
    'Dim Sum As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim count1 As Integer = WebChart1.Chart.Series.Count
        For i As Integer = 0 To count1 - 1
            WebChart1.Chart.Series(i).CheckDataSource()
            WebChart1.Chart.Series(i).RefreshSeries()
        Next
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '取得user的uid, gid, name
            getPageUser(Me)

            Dim strErr As String = ""
            '檢查權限
            'If chkPageAuth(Session("gid"), PAGE_ID, 1, getConnStr(Application("ConnStr")), strErr, Me) <> 0 Then Exit Sub

            '設定更新時間
            Timer1.Enabled = False
            Timer1.Interval = 60000 '60秒

            '主程式
            MainProcess()
            MainProcess_3()
            ProcessWind()


            '啟動更新Timer1
            Timer1.Enabled = False



            'MsgBox("CO訊號預計建立日期：100.12.25", MsgBoxStyle.OkOnly, "訊息")

        End If
    End Sub

    Private Sub MainProcess()
        'Dim arrDeName() As String = {"1GIA2001", "1GIA2002", "1GIA2101", "1GIA2102", "1GIA2103", "1GIA2104", "1GIA2105", "1GIA2106", "1GIA2107", "1GIA2108" _
        '                           , "2GIA2001", "2GIA2002", "2GIA2101", "2GIA2102", "2GIA2103", "2GIA2104", "2GIA2105", "2GIA2106", "2GIA2107", "2GIA2108"}
        ProcessT(0) = Now
        For i As Integer = 1 To 2
            g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                                                 "FROM h_pmis_fi06 " &
                                                 "Where fn= " & i &
                                                 " ORDER BY sys_process_date DESC "
            GetData_Time(1, True)
            ProcessT(i) = g_aDataSet2(0)
            Read_Last_DataTime(i, ProcessT(i))
        Next


        '更新最後一次收到各程控的資料時間
        For i As Integer = 1 To 2
            g_strSQL = "SELECT TOP(1) main_pipe_cog,drain_pit_cog,preh_s_co,preh_n_co,h1_s_co,h1_n_co,h2_s_co,h2_n_co,soak_s_co,soak_n_co " &
                                "FROM h_pmis_fi06 " &
                                "Where fn= " & i &
                                "ORDER BY sys_process_date DESC "
            GetData_CO(9, True)
            For j As Integer = 0 To UBound(g_aDataSet)
                If ConS(j + 1 + (i - 1) * 10) = 0 Then
                    CoValue(j + 1 + (i - 1) * 10) = 0
                    CoValue(0) = CoValue(0) + 0
                Else
                    CoValue(j + 1 + (i - 1) * 10) = g_aDataSet(j)
                    CoValue(0) = CoValue(0) + g_aDataSet(j)
                End If
                'CoValue(j + 1 + (i - 1) * 10) = g_aDataSet(j)
                'CoValue(0) = CoValue(0) + g_aDataSet(j)
            Next
            'ShowData(i, CoValue)
        Next
        'Sum = Sum + Convert.ToInt32(g_aDataSet(0))

    End Sub

    Private Sub MainProcess_3()
        'Dim arrDeName() As String = {"1GIA2001", "1GIA2002", "1GIA2101", "1GIA2102", "1GIA2103", "1GIA2104", "1GIA2105", "1GIA2106", "1GIA2107", "1GIA2108" _
        '                           , "2GIA2001", "2GIA2002", "2GIA2101", "2GIA2102", "2GIA2103", "2GIA2104", "2GIA2105", "2GIA2106", "2GIA2107", "2GIA2108"}
        ProcessT(0) = Now
        g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                                             "FROM h_pmis_fi06_3 " &
                                             "Where fn=3  " &
                                             "ORDER BY sys_process_date DESC "
        GetData_Time(1, True)
        ProcessT(3) = g_aDataSet2(0)
        Read_Last_DataTime(3, ProcessT(3))

        '更新最後一次收到各程控的資料時間

        g_strSQL = "SELECT TOP(1) main_pipe_cog,drain_pit_cog,preh_s_co,preh_n_co,h1_s_co,h1_n_co,h2_s_co,h2_n_co,soak_s_co,soak_n_co,chg_seal_pot_co,dis_seal_pot_co,calorie_meter_co " &
                            "FROM h_pmis_fi06_3 " &
                            "Where fn=3" &
                            "ORDER BY sys_process_date DESC "
        g_strSQL_maxco = "SELECT max_co FROM vw_3702_maxco "

        GetData_CO(12, True)
        GetData_maxCO(1, True)

        For j As Integer = 0 To UBound(g_aDataSet)
            If ConS(j + 21) = 0 Then
                CoValue(j + 21) = 0
                CoValue(0) = CoValue(0) + 0
            Else
                CoValue(j + 21) = g_aDataSet(j)
                CoValue(0) = CoValue(0) + g_aDataSet(j)
            End If
            'CoValue(j + 21) = g_aDataSet(j)
            'CoValue(0) = CoValue(0) + g_aDataSet(j)
        Next

        'Sum = Sum + Convert.ToInt32(g_aDataSet(0))
        ShowData(1, CoValue)
        ShowData(2, CoValue)
        ShowData(3, CoValue)

        CoValue_max = g_aDataSet_maxco(0)

        Total_CO.Text = CoValue_max.ToString() & " "
        Dim d_ppm_maxco As Double = 0
        d_ppm_maxco = CoValue_max

        If d_ppm_maxco > 75 Then
            Total_CO.ForeColor = Drawing.Color.Red
        ElseIf 75 >= d_ppm_maxco And d_ppm_maxco >= 35 Then
            Total_CO.ForeColor = Drawing.Color.Brown
        ElseIf 35 > d_ppm_maxco And d_ppm_maxco >= 0 Then
            Total_CO.ForeColor = Drawing.Color.Blue
        Else
            Total_CO.ForeColor = Drawing.Color.Red
            Total_CO.Text = "N/A"
        End If

    End Sub

    Private Sub ProcessWind()
        'wind_direction_W.Value = "45"
        'wind_direction_E.Value = "45"
        'Val_W_W_S.Text = "100"
        'Val_W_E_S.Text = "100"

        ProcessT(0) = Now
        For i As Integer = 1 To 2
            g_strSQL_time = "SELECT TOP(1) sys_process_date " &
                                                 "FROM Wind " &
                                                 "Where Position= " & i &
                                                 " ORDER BY sys_process_date DESC "
            GetData_Time(1, True)
            ProcessT(i + 3) = g_aDataSet2(0)
            Read_Last_DataTime(i + 3, ProcessT(i + 3))
        Next

        For i As Integer = 1 To 2
            g_strSQL = "SELECT TOP(1) Wind_Speed,Wind_direction " &
                                "FROM Wind " &
                                "Where Position= " & i &
                                "ORDER BY sys_process_date DESC "
            GetData_W(1, True)
            If i = 1 Then

                If ConS(34) = 0 Then
                    Val_W_W_S.Text = "N/A"
                    wind_direction_W.Value = "0"
                    Val_W_W_S.ForeColor = Drawing.Color.Red
                Else
                    Val_W_W_S.Text = g_aDataSetWind(0)
                    wind_direction_W.Value = g_aDataSetWind(1) * (-1)
                    Val_W_W_S.ForeColor = Drawing.Color.Blue
                End If
                'Else
                '    If ConS(35) = 0 Then
                '        Val_W_E_S.Text = "N/A"
                '        wind_direction_E.Value = "0"
                '        Val_W_E_S.ForeColor = Drawing.Color.Red
                '    Else
                '        Val_W_E_S.Text = g_aDataSetWind(0)
                '        wind_direction_E.Value = g_aDataSetWind(1) * (-1)
                '        Val_W_E_S.ForeColor = Drawing.Color.Blue
                '    End If

            End If


        Next





    End Sub


    Private Sub Read_Last_DataTime(ByVal I As Integer, ByVal Processtime As DateTime)
        Select Case I
            Case 1
                Last_time_1.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")

                If Now.AddMinutes(-10) > Processtime Then
                    IT_01.BackColor = Drawing.Color.Red
                    V1.ForeColor = Drawing.Color.Red
                    P_1GIA2001.BackColor = Drawing.Color.Red
                    ConS(1) = 0
                Else
                    IT_01.BackColor = Drawing.Color.Lime
                    V1.ForeColor = Drawing.Color.Lime
                    P_1GIA2001.BackColor = Drawing.Color.Lime
                    ConS(1) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_02.BackColor = Drawing.Color.Red
                    V2.ForeColor = Drawing.Color.Red

                    P_1GIA2002.BackColor = Drawing.Color.Red
                    ConS(2) = 0
                Else
                    IT_02.BackColor = Drawing.Color.Lime
                    V2.ForeColor = Drawing.Color.Lime
                    P_1GIA2002.BackColor = Drawing.Color.Lime
                    ConS(2) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_03.BackColor = Drawing.Color.Red
                    V3.ForeColor = Drawing.Color.Red
                    P_1GIA2101.BackColor = Drawing.Color.Red
                    ConS(3) = 0
                Else
                    IT_03.BackColor = Drawing.Color.Lime
                    V3.ForeColor = Drawing.Color.Lime
                    P_1GIA2101.BackColor = Drawing.Color.Lime
                    ConS(3) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_04.BackColor = Drawing.Color.Red
                    V4.ForeColor = Drawing.Color.Red
                    P_1GIA2102.BackColor = Drawing.Color.Red
                    ConS(4) = 0
                Else
                    IT_04.BackColor = Drawing.Color.Lime
                    V4.ForeColor = Drawing.Color.Lime
                    P_1GIA2102.BackColor = Drawing.Color.Lime
                    ConS(4) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_05.BackColor = Drawing.Color.Red
                    V5.ForeColor = Drawing.Color.Red
                    P_1GIA2103.BackColor = Drawing.Color.Red
                    ConS(5) = 0
                Else
                    IT_05.BackColor = Drawing.Color.Lime
                    V5.ForeColor = Drawing.Color.Lime
                    P_1GIA2103.BackColor = Drawing.Color.Lime
                    ConS(5) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_06.BackColor = Drawing.Color.Red
                    V6.ForeColor = Drawing.Color.Red
                    P_1GIA2104.BackColor = Drawing.Color.Red
                    ConS(6) = 0
                Else
                    IT_06.BackColor = Drawing.Color.Lime
                    V6.ForeColor = Drawing.Color.Lime
                    P_1GIA2104.BackColor = Drawing.Color.Lime
                    ConS(6) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_07.BackColor = Drawing.Color.Red
                    V7.ForeColor = Drawing.Color.Red
                    P_1GIA2105.BackColor = Drawing.Color.Red
                    ConS(7) = 0
                Else
                    IT_07.BackColor = Drawing.Color.Lime
                    V7.ForeColor = Drawing.Color.Lime
                    P_1GIA2105.BackColor = Drawing.Color.Lime
                    ConS(7) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_08.BackColor = Drawing.Color.Red
                    V8.ForeColor = Drawing.Color.Red
                    P_1GIA2106.BackColor = Drawing.Color.Red
                    ConS(8) = 0
                Else
                    IT_08.BackColor = Drawing.Color.Lime
                    V8.ForeColor = Drawing.Color.Lime
                    P_1GIA2106.BackColor = Drawing.Color.Lime
                    ConS(8) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_09.BackColor = Drawing.Color.Red
                    V9.ForeColor = Drawing.Color.Red
                    P_1GIA2107.BackColor = Drawing.Color.Red
                    ConS(9) = 0
                Else
                    IT_09.BackColor = Drawing.Color.Lime
                    V9.ForeColor = Drawing.Color.Lime
                    P_1GIA2107.BackColor = Drawing.Color.Lime
                    ConS(9) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_10.BackColor = Drawing.Color.Red
                    V10.ForeColor = Drawing.Color.Red
                    P_1GIA2108.BackColor = Drawing.Color.Red
                    ConS(10) = 0
                Else
                    IT_10.BackColor = Drawing.Color.Lime
                    V10.ForeColor = Drawing.Color.Lime
                    P_1GIA2108.BackColor = Drawing.Color.Lime
                    ConS(10) = 1
                End If



            Case 2
                Last_time_2.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")

                If Now.AddMinutes(-10) > Processtime Then
                    IT_11.BackColor = Drawing.Color.Red
                    V11.ForeColor = Drawing.Color.Red
                    P_2GIA2001.BackColor = Drawing.Color.Red
                    ConS(11) = 0
                Else
                    IT_11.BackColor = Drawing.Color.Lime
                    V11.ForeColor = Drawing.Color.Lime
                    P_2GIA2001.BackColor = Drawing.Color.Lime
                    ConS(11) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_12.BackColor = Drawing.Color.Red
                    V12.ForeColor = Drawing.Color.Red
                    P_2GIA2002.BackColor = Drawing.Color.Red
                    ConS(12) = 0
                Else
                    IT_12.BackColor = Drawing.Color.Lime
                    V12.ForeColor = Drawing.Color.Lime
                    P_2GIA2002.BackColor = Drawing.Color.Lime
                    ConS(12) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_13.BackColor = Drawing.Color.Red
                    V13.ForeColor = Drawing.Color.Red
                    P_2GIA2101.BackColor = Drawing.Color.Red
                    ConS(13) = 0
                Else
                    IT_13.BackColor = Drawing.Color.Lime
                    V13.ForeColor = Drawing.Color.Lime
                    P_2GIA2101.BackColor = Drawing.Color.Lime
                    ConS(13) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_14.BackColor = Drawing.Color.Red
                    V14.ForeColor = Drawing.Color.Red
                    P_2GIA2102.BackColor = Drawing.Color.Red
                    ConS(14) = 0
                Else
                    IT_14.BackColor = Drawing.Color.Lime
                    V14.ForeColor = Drawing.Color.Lime
                    P_2GIA2102.BackColor = Drawing.Color.Lime
                    ConS(14) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_15.BackColor = Drawing.Color.Red
                    V15.ForeColor = Drawing.Color.Red
                    P_2GIA2103.BackColor = Drawing.Color.Red
                    ConS(15) = 0
                Else
                    IT_15.BackColor = Drawing.Color.Lime
                    V15.ForeColor = Drawing.Color.Lime
                    P_2GIA2103.BackColor = Drawing.Color.Lime
                    ConS(15) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_16.BackColor = Drawing.Color.Red
                    V16.ForeColor = Drawing.Color.Red
                    P_2GIA2104.BackColor = Drawing.Color.Red
                    ConS(16) = 0
                Else
                    IT_16.BackColor = Drawing.Color.Lime
                    V16.ForeColor = Drawing.Color.Lime
                    P_2GIA2104.BackColor = Drawing.Color.Lime
                    ConS(16) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_17.BackColor = Drawing.Color.Red
                    V17.ForeColor = Drawing.Color.Red
                    P_2GIA2105.BackColor = Drawing.Color.Red
                    ConS(17) = 0
                Else
                    IT_17.BackColor = Drawing.Color.Lime
                    V17.ForeColor = Drawing.Color.Lime
                    P_2GIA2105.BackColor = Drawing.Color.Lime
                    ConS(17) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_18.BackColor = Drawing.Color.Red
                    V18.ForeColor = Drawing.Color.Red
                    P_2GIA2106.BackColor = Drawing.Color.Red
                    ConS(18) = 0
                Else
                    IT_18.BackColor = Drawing.Color.Lime
                    V18.ForeColor = Drawing.Color.Lime
                    P_2GIA2106.BackColor = Drawing.Color.Lime
                    ConS(18) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_19.BackColor = Drawing.Color.Red
                    V19.ForeColor = Drawing.Color.Red
                    P_2GIA2107.BackColor = Drawing.Color.Red
                    ConS(19) = 0
                Else
                    IT_19.BackColor = Drawing.Color.Lime
                    V19.ForeColor = Drawing.Color.Lime
                    P_2GIA2107.BackColor = Drawing.Color.Lime
                    ConS(19) = 1
                End If


                If Now.AddMinutes(-10) > Processtime Then
                    IT_20.BackColor = Drawing.Color.Red
                    V20.ForeColor = Drawing.Color.Red
                    P_2GIA2108.BackColor = Drawing.Color.Red
                    ConS(20) = 0
                Else
                    IT_20.BackColor = Drawing.Color.Lime
                    V20.ForeColor = Drawing.Color.Lime
                    P_2GIA2108.BackColor = Drawing.Color.Lime
                    ConS(20) = 1
                End If



            Case 3
                Last_time_3.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")

                If Now.AddMinutes(-10) > Processtime Then
                    IT_21.BackColor = Drawing.Color.Red
                    V21.ForeColor = Drawing.Color.Red
                    P_3GIA2001.BackColor = Drawing.Color.Red
                    ConS(21) = 0
                Else
                    IT_21.BackColor = Drawing.Color.Lime
                    V21.ForeColor = Drawing.Color.Lime
                    P_3GIA2001.BackColor = Drawing.Color.Lime
                    ConS(21) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_22.BackColor = Drawing.Color.Red
                    V22.ForeColor = Drawing.Color.Red
                    P_3GIA2002.BackColor = Drawing.Color.Red
                    ConS(22) = 0
                Else
                    IT_22.BackColor = Drawing.Color.Lime
                    V22.ForeColor = Drawing.Color.Lime
                    P_3GIA2002.BackColor = Drawing.Color.Lime
                    ConS(22) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_23.BackColor = Drawing.Color.Red
                    V23.ForeColor = Drawing.Color.Red
                    P_3GIA2101.BackColor = Drawing.Color.Red
                    ConS(23) = 0
                Else
                    IT_23.BackColor = Drawing.Color.Lime
                    V23.ForeColor = Drawing.Color.Lime
                    P_3GIA2101.BackColor = Drawing.Color.Lime
                    ConS(23) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_24.BackColor = Drawing.Color.Red
                    V24.ForeColor = Drawing.Color.Red
                    P_3GIA2102.BackColor = Drawing.Color.Red
                    ConS(24) = 0
                Else
                    IT_24.BackColor = Drawing.Color.Lime
                    V24.ForeColor = Drawing.Color.Lime
                    P_3GIA2102.BackColor = Drawing.Color.Lime
                    ConS(24) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_25.BackColor = Drawing.Color.Red
                    V25.ForeColor = Drawing.Color.Red
                    P_3GIA2103.BackColor = Drawing.Color.Red
                    ConS(25) = 0
                Else
                    IT_25.BackColor = Drawing.Color.Lime
                    V25.ForeColor = Drawing.Color.Lime
                    P_3GIA2103.BackColor = Drawing.Color.Lime
                    ConS(25) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_26.BackColor = Drawing.Color.Red
                    V26.ForeColor = Drawing.Color.Red
                    P_3GIA2104.BackColor = Drawing.Color.Red
                    ConS(26) = 0
                Else
                    IT_26.BackColor = Drawing.Color.Lime
                    V26.ForeColor = Drawing.Color.Lime
                    P_3GIA2104.BackColor = Drawing.Color.Lime
                    ConS(26) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_27.BackColor = Drawing.Color.Red
                    V27.ForeColor = Drawing.Color.Red
                    P_3GIA2105.BackColor = Drawing.Color.Red
                    ConS(27) = 0
                Else
                    IT_27.BackColor = Drawing.Color.Lime
                    V27.ForeColor = Drawing.Color.Lime
                    P_3GIA2105.BackColor = Drawing.Color.Lime
                    ConS(27) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_28.BackColor = Drawing.Color.Red
                    V28.ForeColor = Drawing.Color.Red
                    P_3GIA2106.BackColor = Drawing.Color.Red
                    ConS(28) = 0
                Else
                    IT_28.BackColor = Drawing.Color.Lime
                    V28.ForeColor = Drawing.Color.Lime
                    P_3GIA2106.BackColor = Drawing.Color.Lime
                    ConS(28) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_29.BackColor = Drawing.Color.Red
                    V29.ForeColor = Drawing.Color.Red
                    P_3GIA2107.BackColor = Drawing.Color.Red
                    ConS(29) = 0
                Else
                    IT_29.BackColor = Drawing.Color.Lime
                    V29.ForeColor = Drawing.Color.Lime
                    P_3GIA2107.BackColor = Drawing.Color.Lime
                    ConS(29) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_30.BackColor = Drawing.Color.Red
                    V30.ForeColor = Drawing.Color.Red
                    P_3GIA2108.BackColor = Drawing.Color.Red
                    ConS(30) = 0
                Else
                    IT_30.BackColor = Drawing.Color.Lime
                    V30.ForeColor = Drawing.Color.Lime
                    P_3GIA2108.BackColor = Drawing.Color.Lime
                    ConS(30) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_31.BackColor = Drawing.Color.Red
                    V31.ForeColor = Drawing.Color.Red
                    P_3GIA2003.BackColor = Drawing.Color.Red
                    ConS(31) = 0
                Else
                    IT_31.BackColor = Drawing.Color.Lime
                    V31.ForeColor = Drawing.Color.Lime
                    P_3GIA2003.BackColor = Drawing.Color.Lime
                    ConS(31) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_32.BackColor = Drawing.Color.Red
                    V32.ForeColor = Drawing.Color.Red
                    P_3GIA2004.BackColor = Drawing.Color.Red
                    ConS(32) = 0
                Else
                    IT_32.BackColor = Drawing.Color.Lime
                    V32.ForeColor = Drawing.Color.Lime
                    P_3GIA2004.BackColor = Drawing.Color.Lime
                    ConS(32) = 1
                End If

                If Now.AddMinutes(-10) > Processtime Then
                    IT_33.BackColor = Drawing.Color.Red
                    V33.ForeColor = Drawing.Color.Red
                    P_3GIA2005.BackColor = Drawing.Color.Red
                    ConS(33) = 0
                Else
                    IT_33.BackColor = Drawing.Color.Lime
                    V33.ForeColor = Drawing.Color.Lime
                    P_3GIA2005.BackColor = Drawing.Color.Lime
                    ConS(33) = 1
                End If
            Case 4
                Last_time_4.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
                If Now.AddMinutes(-10) > Processtime Then
                    Wind_S_W_L.BackColor = Drawing.Color.Red
                    Val_W_W_S.ForeColor = Drawing.Color.Red
                    ConS(34) = 0
                Else
                    Wind_S_W_L.BackColor = Drawing.Color.Lime
                    Val_W_W_S.ForeColor = Drawing.Color.Lime
                    ConS(34) = 1
                End If
                'Case 5
                '    Last_time_5.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
                '    If Now.AddMinutes(-10) > Processtime Then
                '        Wind_S_E_L.BackColor = Drawing.Color.Red
                '        Val_W_E_S.ForeColor = Drawing.Color.Red
                '        ConS(35) = 0
                '    Else
                '        Wind_S_E_L.BackColor = Drawing.Color.Lime
                '        Val_W_E_S.ForeColor = Drawing.Color.Lime
                '        ConS(35) = 1
                '    End If
        End Select

        If Now.AddMinutes(-10) > Processtime Then
            'Total_CO.Text = "N/A"
            Total_CO.ForeColor = Drawing.Color.Red
            ConS(0) = 0
            'If Processtime.ToString("yyyy") = "1911" Then
            '    Label1.Text = "N/A"
            'Else
            '    Label1.Text = Processtime.ToString("yyyy/MM/dd HH:mm:ss")
            'End If
        Else
            Total_CO.ForeColor = Drawing.Color.Blue
            ConS(0) = 1
        End If

    End Sub

    Private Sub ShowData(ByVal I As Integer, ByVal value() As Integer)

        'Dim ppm As Integer = 0
        Dim d_ppm As Decimal = 0
        Dim d_temp As Decimal = 0.01




        If I = 1 Then
            d_ppm = 0
            If ConS(1) = 1 Then
                d_ppm = value(1) * d_temp
                V1.Text = d_ppm
                V_1GIA2001.Text = d_ppm
                If d_ppm > 75 Then
                    IT_01.BackColor = Drawing.Color.Red
                    V1.ForeColor = Drawing.Color.Red
                    P_1GIA2001.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_01.BackColor = Drawing.Color.Yellow
                    V1.ForeColor = Drawing.Color.Brown
                    P_1GIA2001.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_01.BackColor = Drawing.Color.Lime
                    V1.ForeColor = Drawing.Color.Blue
                    P_1GIA2001.BackColor = Drawing.Color.Lime

                Else
                    IT_01.BackColor = Drawing.Color.Red
                    V1.ForeColor = Drawing.Color.Red

                    P_1GIA2001.BackColor = Drawing.Color.Red
                    V1.Text = "N/A"

                    V_1GIA2001.Text = "N/A"
                End If
            Else
                IT_01.BackColor = Drawing.Color.Red
                V1.ForeColor = Drawing.Color.Red

                P_1GIA2001.BackColor = Drawing.Color.Red
                V1.Text = "N/A"

                V_1GIA2001.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(2) = 1 Then
                d_ppm = value(2) * d_temp
                V2.Text = d_ppm
                V_1GIA2002.Text = d_ppm
                If d_ppm > 75 Then
                    IT_02.BackColor = Drawing.Color.Red
                    V2.ForeColor = Drawing.Color.Red

                    P_1GIA2002.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_02.BackColor = Drawing.Color.Yellow
                    V2.ForeColor = Drawing.Color.Brown

                    P_1GIA2002.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_02.BackColor = Drawing.Color.Lime
                    V2.ForeColor = Drawing.Color.Blue

                    P_1GIA2002.BackColor = Drawing.Color.Lime

                Else
                    IT_02.BackColor = Drawing.Color.Red
                    V2.ForeColor = Drawing.Color.Red

                    P_1GIA2002.BackColor = Drawing.Color.Red
                    V2.Text = "N/A"

                    V_1GIA2002.Text = "N/A"
                End If
            Else
                IT_02.BackColor = Drawing.Color.Red
                V2.ForeColor = Drawing.Color.Red

                P_1GIA2002.BackColor = Drawing.Color.Red
                V2.Text = "N/A"

                V_1GIA2002.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(3) = 1 Then
                d_ppm = value(3) * d_temp
                V3.Text = d_ppm
                V_1GIA2101.Text = d_ppm
                If d_ppm > 75 Then
                    IT_03.BackColor = Drawing.Color.Red
                    V3.ForeColor = Drawing.Color.Red

                    P_1GIA2101.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_03.BackColor = Drawing.Color.Yellow
                    V3.ForeColor = Drawing.Color.Brown

                    P_1GIA2101.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_03.BackColor = Drawing.Color.Lime
                    V3.ForeColor = Drawing.Color.Blue

                    P_1GIA2101.BackColor = Drawing.Color.Lime

                Else
                    IT_03.BackColor = Drawing.Color.Red
                    V3.ForeColor = Drawing.Color.Red

                    P_1GIA2101.BackColor = Drawing.Color.Red
                    V3.Text = "N/A"

                    V_1GIA2101.Text = "N/A"
                End If
            Else
                IT_03.BackColor = Drawing.Color.Red
                V3.ForeColor = Drawing.Color.Red

                P_1GIA2101.BackColor = Drawing.Color.Red
                V3.Text = "N/A"

                V_1GIA2101.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(4) = 1 Then
                d_ppm = value(4) * d_temp
                V4.Text = d_ppm
                V_1GIA2102.Text = d_ppm
                If d_ppm > 75 Then
                    IT_04.BackColor = Drawing.Color.Red
                    V4.ForeColor = Drawing.Color.Red

                    P_1GIA2102.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_04.BackColor = Drawing.Color.Yellow
                    V4.ForeColor = Drawing.Color.Brown

                    P_1GIA2102.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_04.BackColor = Drawing.Color.Lime
                    V4.ForeColor = Drawing.Color.Blue

                    P_1GIA2102.BackColor = Drawing.Color.Lime

                Else
                    IT_04.BackColor = Drawing.Color.Red
                    V4.ForeColor = Drawing.Color.Red

                    P_1GIA2102.BackColor = Drawing.Color.Red
                    V4.Text = "N/A"

                    V_1GIA2102.Text = "N/A"
                End If
            Else
                IT_04.BackColor = Drawing.Color.Red
                V4.ForeColor = Drawing.Color.Red

                P_1GIA2102.BackColor = Drawing.Color.Red
                V4.Text = "N/A"

                V_1GIA2102.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(5) = 1 Then
                d_ppm = value(5) * d_temp
                V5.Text = d_ppm
                V_1GIA2103.Text = d_ppm
                If d_ppm > 75 Then
                    IT_05.BackColor = Drawing.Color.Red
                    V5.ForeColor = Drawing.Color.Red

                    P_1GIA2103.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_05.BackColor = Drawing.Color.Yellow
                    V5.ForeColor = Drawing.Color.Brown

                    P_1GIA2103.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_05.BackColor = Drawing.Color.Lime
                    V5.ForeColor = Drawing.Color.Blue

                    P_1GIA2103.BackColor = Drawing.Color.Lime

                Else
                    IT_05.BackColor = Drawing.Color.Red
                    V5.ForeColor = Drawing.Color.Red

                    P_1GIA2103.BackColor = Drawing.Color.Red
                    V5.Text = "N/A"

                    V_1GIA2103.Text = "N/A"
                End If
            Else
                IT_05.BackColor = Drawing.Color.Red
                V5.ForeColor = Drawing.Color.Red

                P_1GIA2103.BackColor = Drawing.Color.Red
                V5.Text = "N/A"

                V_1GIA2103.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(6) = 1 Then
                d_ppm = value(6) * d_temp
                V6.Text = d_ppm
                V_1GIA2104.Text = d_ppm
                If d_ppm > 75 Then
                    IT_06.BackColor = Drawing.Color.Red
                    V6.ForeColor = Drawing.Color.Red

                    P_1GIA2104.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_06.BackColor = Drawing.Color.Yellow
                    V6.ForeColor = Drawing.Color.Brown

                    P_1GIA2104.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_06.BackColor = Drawing.Color.Lime
                    V6.ForeColor = Drawing.Color.Blue

                    P_1GIA2104.BackColor = Drawing.Color.Lime

                Else
                    IT_06.BackColor = Drawing.Color.Red
                    V6.ForeColor = Drawing.Color.Red

                    P_1GIA2104.BackColor = Drawing.Color.Red
                    V6.Text = "N/A"

                    V_1GIA2104.Text = "N/A"
                End If
            Else
                IT_06.BackColor = Drawing.Color.Red
                V6.ForeColor = Drawing.Color.Red

                P_1GIA2104.BackColor = Drawing.Color.Red
                V6.Text = "N/A"

                V_1GIA2104.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(7) = 1 Then
                d_ppm = value(7) * d_temp
                V7.Text = d_ppm
                V_1GIA2105.Text = d_ppm
                If d_ppm > 75 Then
                    IT_07.BackColor = Drawing.Color.Red
                    V7.ForeColor = Drawing.Color.Red

                    P_1GIA2105.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_07.BackColor = Drawing.Color.Yellow
                    V7.ForeColor = Drawing.Color.Brown

                    P_1GIA2105.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_07.BackColor = Drawing.Color.Lime
                    V7.ForeColor = Drawing.Color.Blue

                    P_1GIA2105.BackColor = Drawing.Color.Lime

                Else
                    IT_07.BackColor = Drawing.Color.Red
                    V7.ForeColor = Drawing.Color.Red

                    P_1GIA2105.BackColor = Drawing.Color.Red
                    V7.Text = "N/A"

                    V_1GIA2105.Text = "N/A"
                End If
            Else
                IT_07.BackColor = Drawing.Color.Red
                V7.ForeColor = Drawing.Color.Red

                P_1GIA2105.BackColor = Drawing.Color.Red
                V7.Text = "N/A"

                V_1GIA2105.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(8) = 1 Then
                d_ppm = value(8) * d_temp
                V8.Text = d_ppm
                V_1GIA2106.Text = d_ppm
                If d_ppm > 75 Then
                    IT_08.BackColor = Drawing.Color.Red
                    V8.ForeColor = Drawing.Color.Red

                    P_1GIA2106.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_08.BackColor = Drawing.Color.Yellow
                    V8.ForeColor = Drawing.Color.Brown

                    P_1GIA2106.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_08.BackColor = Drawing.Color.Lime
                    V8.ForeColor = Drawing.Color.Blue

                    P_1GIA2106.BackColor = Drawing.Color.Lime

                Else
                    IT_08.BackColor = Drawing.Color.Red
                    V8.ForeColor = Drawing.Color.Red

                    P_1GIA2106.BackColor = Drawing.Color.Red
                    V8.Text = "N/A"

                    V_1GIA2106.Text = "N/A"
                End If
            Else
                IT_08.BackColor = Drawing.Color.Red
                V8.ForeColor = Drawing.Color.Red

                P_1GIA2106.BackColor = Drawing.Color.Red
                V8.Text = "N/A"

                V_1GIA2106.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(9) = 1 Then
                d_ppm = value(9) * d_temp
                V9.Text = d_ppm
                V_1GIA2107.Text = d_ppm
                If d_ppm > 75 Then
                    IT_09.BackColor = Drawing.Color.Red
                    V9.ForeColor = Drawing.Color.Red

                    P_1GIA2107.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_09.BackColor = Drawing.Color.Yellow
                    V9.ForeColor = Drawing.Color.Brown

                    P_1GIA2107.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_09.BackColor = Drawing.Color.Lime
                    V9.ForeColor = Drawing.Color.Blue

                    P_1GIA2107.BackColor = Drawing.Color.Lime

                Else
                    IT_09.BackColor = Drawing.Color.Red
                    V9.ForeColor = Drawing.Color.Red

                    P_1GIA2107.BackColor = Drawing.Color.Red
                    V9.Text = "N/A"

                    V_1GIA2107.Text = "N/A"
                End If
            Else
                IT_09.BackColor = Drawing.Color.Red
                V9.ForeColor = Drawing.Color.Red

                P_1GIA2107.BackColor = Drawing.Color.Red
                V9.Text = "N/A"

                V_1GIA2107.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(10) = 1 Then
                d_ppm = value(10) * d_temp
                V10.Text = d_ppm
                V_1GIA2108.Text = d_ppm
                If d_ppm > 75 Then
                    IT_10.BackColor = Drawing.Color.Red
                    V10.ForeColor = Drawing.Color.Red

                    P_1GIA2108.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_10.BackColor = Drawing.Color.Yellow
                    V10.ForeColor = Drawing.Color.Brown

                    P_1GIA2108.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_10.BackColor = Drawing.Color.Lime
                    V10.ForeColor = Drawing.Color.Blue

                    P_1GIA2108.BackColor = Drawing.Color.Lime

                Else
                    IT_10.BackColor = Drawing.Color.Red
                    V10.ForeColor = Drawing.Color.Red

                    P_1GIA2108.BackColor = Drawing.Color.Red
                    V10.Text = "N/A"

                    V_1GIA2108.Text = "N/A"
                End If
            Else
                IT_10.BackColor = Drawing.Color.Red
                V10.ForeColor = Drawing.Color.Red

                P_1GIA2108.BackColor = Drawing.Color.Red
                V10.Text = "N/A"

                V_1GIA2108.Text = "N/A"
            End If
        ElseIf I = 2 Then
            d_ppm = 0
            If ConS(11) = 1 Then
                d_ppm = value(11) * d_temp
                V11.Text = d_ppm
                V_2GIA2001.Text = d_ppm
                If d_ppm > 75 Then
                    IT_11.BackColor = Drawing.Color.Red
                    V11.ForeColor = Drawing.Color.Red

                    P_2GIA2001.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_11.BackColor = Drawing.Color.Yellow
                    V11.ForeColor = Drawing.Color.Brown

                    P_2GIA2001.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_11.BackColor = Drawing.Color.Lime
                    V11.ForeColor = Drawing.Color.Blue

                    P_2GIA2001.BackColor = Drawing.Color.Lime

                Else
                    IT_11.BackColor = Drawing.Color.Red
                    V11.ForeColor = Drawing.Color.Red

                    P_2GIA2001.BackColor = Drawing.Color.Red
                    V11.Text = "N/A"

                    V_2GIA2001.Text = "N/A"
                End If
            Else
                IT_11.BackColor = Drawing.Color.Red
                V11.ForeColor = Drawing.Color.Red

                P_2GIA2001.BackColor = Drawing.Color.Red
                V11.Text = "N/A"

                V_2GIA2001.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(12) = 1 Then
                d_ppm = value(12) * d_temp
                V12.Text = d_ppm
                V_2GIA2002.Text = d_ppm
                If d_ppm > 75 Then
                    IT_12.BackColor = Drawing.Color.Red
                    V12.ForeColor = Drawing.Color.Red

                    P_2GIA2002.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_12.BackColor = Drawing.Color.Yellow
                    V12.ForeColor = Drawing.Color.Brown

                    P_2GIA2002.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_12.BackColor = Drawing.Color.Lime
                    V12.ForeColor = Drawing.Color.Blue

                    P_2GIA2002.BackColor = Drawing.Color.Lime

                Else
                    IT_12.BackColor = Drawing.Color.Red
                    V12.ForeColor = Drawing.Color.Red

                    P_2GIA2002.BackColor = Drawing.Color.Red
                    V12.Text = "N/A"

                    V_2GIA2002.Text = "N/A"
                End If
            Else
                IT_12.BackColor = Drawing.Color.Red
                V12.ForeColor = Drawing.Color.Red

                P_2GIA2002.BackColor = Drawing.Color.Red
                V12.Text = "N/A"

                V_2GIA2002.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(13) = 1 Then
                d_ppm = value(13) * d_temp
                V13.Text = d_ppm
                V_2GIA2101.Text = d_ppm
                If d_ppm > 75 Then
                    IT_13.BackColor = Drawing.Color.Red
                    V13.ForeColor = Drawing.Color.Red

                    P_2GIA2101.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_13.BackColor = Drawing.Color.Yellow
                    V13.ForeColor = Drawing.Color.Brown

                    P_2GIA2101.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_13.BackColor = Drawing.Color.Lime
                    V13.ForeColor = Drawing.Color.Blue

                    P_2GIA2101.BackColor = Drawing.Color.Lime

                Else
                    IT_13.BackColor = Drawing.Color.Red
                    V13.ForeColor = Drawing.Color.Red

                    P_2GIA2101.BackColor = Drawing.Color.Red
                    V13.Text = "N/A"

                    V_2GIA2101.Text = "N/A"
                End If
            Else
                IT_13.BackColor = Drawing.Color.Red
                V13.ForeColor = Drawing.Color.Red

                P_2GIA2101.BackColor = Drawing.Color.Red
                V13.Text = "N/A"

                V_2GIA2101.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(14) = 1 Then
                d_ppm = value(14) * d_temp
                V14.Text = d_ppm
                V_2GIA2102.Text = d_ppm
                If d_ppm > 75 Then
                    IT_14.BackColor = Drawing.Color.Red
                    V14.ForeColor = Drawing.Color.Red

                    P_2GIA2102.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_14.BackColor = Drawing.Color.Yellow
                    V14.ForeColor = Drawing.Color.Brown

                    P_2GIA2102.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_14.BackColor = Drawing.Color.Lime
                    V14.ForeColor = Drawing.Color.Blue

                    P_2GIA2102.BackColor = Drawing.Color.Lime

                Else
                    IT_14.BackColor = Drawing.Color.Red
                    V14.ForeColor = Drawing.Color.Red

                    P_2GIA2102.BackColor = Drawing.Color.Red
                    V14.Text = "N/A"

                    V_2GIA2102.Text = "N/A"
                End If
            Else
                IT_14.BackColor = Drawing.Color.Red
                V14.ForeColor = Drawing.Color.Red

                P_2GIA2102.BackColor = Drawing.Color.Red
                V14.Text = "N/A"

                V_2GIA2102.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(15) = 1 Then
                d_ppm = value(15) * d_temp
                V15.Text = d_ppm
                V_2GIA2103.Text = d_ppm
                If d_ppm > 75 Then
                    IT_15.BackColor = Drawing.Color.Red
                    V15.ForeColor = Drawing.Color.Red

                    P_2GIA2103.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_15.BackColor = Drawing.Color.Yellow
                    V15.ForeColor = Drawing.Color.Brown

                    P_2GIA2103.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_15.BackColor = Drawing.Color.Lime
                    V15.ForeColor = Drawing.Color.Blue

                    P_2GIA2103.BackColor = Drawing.Color.Lime

                Else
                    IT_15.BackColor = Drawing.Color.Red
                    V15.ForeColor = Drawing.Color.Red

                    P_2GIA2103.BackColor = Drawing.Color.Red
                    V15.Text = "N/A"

                    V_2GIA2103.Text = "N/A"
                End If
            Else
                IT_15.BackColor = Drawing.Color.Red
                V15.ForeColor = Drawing.Color.Red

                P_2GIA2103.BackColor = Drawing.Color.Red
                V15.Text = "N/A"

                V_2GIA2103.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(16) = 1 Then
                d_ppm = value(16) * d_temp
                V16.Text = d_ppm
                V_2GIA2104.Text = d_ppm
                If d_ppm > 75 Then
                    IT_16.BackColor = Drawing.Color.Red
                    V16.ForeColor = Drawing.Color.Red

                    P_2GIA2104.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_16.BackColor = Drawing.Color.Yellow
                    V16.ForeColor = Drawing.Color.Brown

                    P_2GIA2104.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_16.BackColor = Drawing.Color.Lime
                    V16.ForeColor = Drawing.Color.Blue

                    P_2GIA2104.BackColor = Drawing.Color.Lime

                Else
                    IT_16.BackColor = Drawing.Color.Red
                    V16.ForeColor = Drawing.Color.Red

                    P_2GIA2104.BackColor = Drawing.Color.Red
                    V16.Text = "N/A"

                    V_2GIA2104.Text = "N/A"
                End If
            Else
                IT_16.BackColor = Drawing.Color.Red
                V16.ForeColor = Drawing.Color.Red

                P_2GIA2104.BackColor = Drawing.Color.Red
                V16.Text = "N/A"

                V_2GIA2104.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(17) = 1 Then
                d_ppm = value(17) * d_temp
                V17.Text = d_ppm
                V_2GIA2105.Text = d_ppm
                If d_ppm > 75 Then
                    IT_17.BackColor = Drawing.Color.Red
                    V17.ForeColor = Drawing.Color.Red

                    P_2GIA2105.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_17.BackColor = Drawing.Color.Yellow
                    V17.ForeColor = Drawing.Color.Brown

                    P_2GIA2105.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_17.BackColor = Drawing.Color.Lime
                    V17.ForeColor = Drawing.Color.Blue

                    P_2GIA2105.BackColor = Drawing.Color.Lime

                Else
                    IT_17.BackColor = Drawing.Color.Red
                    V17.ForeColor = Drawing.Color.Red

                    P_2GIA2105.BackColor = Drawing.Color.Red
                    V17.Text = "N/A"

                    V_2GIA2105.Text = "N/A"
                End If
            Else
                IT_17.BackColor = Drawing.Color.Red
                V17.ForeColor = Drawing.Color.Red

                P_2GIA2105.BackColor = Drawing.Color.Red
                V17.Text = "N/A"

                V_2GIA2105.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(18) = 1 Then
                d_ppm = value(18) * d_temp
                V18.Text = d_ppm
                V_2GIA2106.Text = d_ppm
                If d_ppm > 75 Then
                    IT_18.BackColor = Drawing.Color.Red
                    V18.ForeColor = Drawing.Color.Red

                    P_2GIA2106.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_18.BackColor = Drawing.Color.Yellow
                    V18.ForeColor = Drawing.Color.Brown

                    P_2GIA2106.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_18.BackColor = Drawing.Color.Lime
                    V18.ForeColor = Drawing.Color.Blue

                    P_2GIA2106.BackColor = Drawing.Color.Lime

                Else
                    IT_18.BackColor = Drawing.Color.Red
                    V18.ForeColor = Drawing.Color.Red

                    P_2GIA2106.BackColor = Drawing.Color.Red
                    V18.Text = "N/A"

                    V_2GIA2106.Text = "N/A"
                End If
            Else
                IT_18.BackColor = Drawing.Color.Red
                V18.ForeColor = Drawing.Color.Red

                P_2GIA2106.BackColor = Drawing.Color.Red
                V18.Text = "N/A"

                V_2GIA2106.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(19) = 1 Then
                d_ppm = value(19) * d_temp
                V19.Text = d_ppm
                V_2GIA2107.Text = d_ppm
                If d_ppm > 75 Then
                    IT_19.BackColor = Drawing.Color.Red
                    V19.ForeColor = Drawing.Color.Red

                    P_2GIA2107.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_19.BackColor = Drawing.Color.Yellow
                    V19.ForeColor = Drawing.Color.Brown

                    P_2GIA2107.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_19.BackColor = Drawing.Color.Lime
                    V19.ForeColor = Drawing.Color.Blue

                    P_2GIA2107.BackColor = Drawing.Color.Lime

                Else
                    IT_19.BackColor = Drawing.Color.Red
                    V19.ForeColor = Drawing.Color.Red

                    P_2GIA2107.BackColor = Drawing.Color.Red
                    V19.Text = "N/A"

                    V_2GIA2107.Text = "N/A"
                End If
            Else
                IT_19.BackColor = Drawing.Color.Red
                V19.ForeColor = Drawing.Color.Red

                P_2GIA2107.BackColor = Drawing.Color.Red
                V19.Text = "N/A"

                V_2GIA2107.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(20) = 1 Then
                d_ppm = value(20) * d_temp
                V20.Text = d_ppm
                V_2GIA2108.Text = d_ppm
                If d_ppm > 75 Then
                    IT_20.BackColor = Drawing.Color.Red
                    V20.ForeColor = Drawing.Color.Red

                    P_2GIA2108.BackColor = Drawing.Color.Red

                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_20.BackColor = Drawing.Color.Yellow
                    V20.ForeColor = Drawing.Color.Brown

                    P_2GIA2108.BackColor = Drawing.Color.Yellow

                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_20.BackColor = Drawing.Color.Lime
                    V20.ForeColor = Drawing.Color.Blue

                    P_2GIA2108.BackColor = Drawing.Color.Lime

                Else
                    IT_20.BackColor = Drawing.Color.Red
                    V20.ForeColor = Drawing.Color.Red

                    P_2GIA2108.BackColor = Drawing.Color.Red
                    V20.Text = "N/A"

                    V_2GIA2108.Text = "N/A"
                End If
            Else
                IT_20.BackColor = Drawing.Color.Red
                V20.ForeColor = Drawing.Color.Red

                P_2GIA2108.BackColor = Drawing.Color.Red
                V20.Text = "N/A"

                V_2GIA2108.Text = "N/A"
            End If
        Else
            d_ppm = 0
            If ConS(21) = 1 Then
                d_ppm = value(21) * d_temp
                V21.Text = d_ppm
                V_3GIA2001.Text = d_ppm
                If d_ppm > 75 Then
                    IT_21.BackColor = Drawing.Color.Red
                    V21.ForeColor = Drawing.Color.Red
                    P_3GIA2001.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_21.BackColor = Drawing.Color.Yellow
                    V21.ForeColor = Drawing.Color.Brown
                    P_3GIA2001.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_21.BackColor = Drawing.Color.Lime
                    V21.ForeColor = Drawing.Color.Blue
                    P_3GIA2001.BackColor = Drawing.Color.Lime
                Else
                    IT_21.BackColor = Drawing.Color.Red
                    V21.ForeColor = Drawing.Color.Red

                    P_3GIA2001.BackColor = Drawing.Color.Red
                    V21.Text = "N/A"
                    V_3GIA2001.Text = "N/A"
                End If
            Else
                IT_21.BackColor = Drawing.Color.Red
                V21.ForeColor = Drawing.Color.Red
                P_3GIA2001.BackColor = Drawing.Color.Red
                V21.Text = "N/A"
                V_3GIA2001.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(22) = 1 Then
                d_ppm = value(22) * d_temp
                V22.Text = d_ppm
                V_3GIA2002.Text = d_ppm
                If d_ppm > 75 Then
                    IT_22.BackColor = Drawing.Color.Red
                    V22.ForeColor = Drawing.Color.Red
                    P_3GIA2002.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_22.BackColor = Drawing.Color.Yellow
                    V22.ForeColor = Drawing.Color.Brown
                    P_3GIA2002.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_22.BackColor = Drawing.Color.Lime
                    V22.ForeColor = Drawing.Color.Blue
                    P_3GIA2002.BackColor = Drawing.Color.Lime
                Else
                    IT_22.BackColor = Drawing.Color.Red
                    V22.ForeColor = Drawing.Color.Red
                    P_3GIA2002.BackColor = Drawing.Color.Red
                    V22.Text = "N/A"
                    V_3GIA2002.Text = "N/A"
                End If
            Else
                IT_22.BackColor = Drawing.Color.Red
                V22.ForeColor = Drawing.Color.Red
                P_3GIA2002.BackColor = Drawing.Color.Red
                V22.Text = "N/A"
                V_3GIA2002.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(23) = 1 Then
                d_ppm = value(23) * d_temp
                V23.Text = d_ppm
                V_3GIA2101.Text = d_ppm
                If d_ppm > 75 Then
                    IT_23.BackColor = Drawing.Color.Red
                    V23.ForeColor = Drawing.Color.Red
                    P_3GIA2101.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_23.BackColor = Drawing.Color.Yellow
                    V23.ForeColor = Drawing.Color.Brown
                    P_3GIA2101.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_23.BackColor = Drawing.Color.Lime
                    V23.ForeColor = Drawing.Color.Blue
                    P_3GIA2101.BackColor = Drawing.Color.Lime
                Else
                    IT_23.BackColor = Drawing.Color.Red
                    V23.ForeColor = Drawing.Color.Red
                    P_3GIA2101.BackColor = Drawing.Color.Red
                    V23.Text = "N/A"
                    V_3GIA2101.Text = "N/A"
                End If
            Else
                IT_23.BackColor = Drawing.Color.Red
                V23.ForeColor = Drawing.Color.Red
                P_3GIA2101.BackColor = Drawing.Color.Red
                V23.Text = "N/A"
                V_3GIA2101.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(24) = 1 Then
                d_ppm = value(24) * d_temp
                V24.Text = d_ppm
                V_3GIA2102.Text = d_ppm
                If d_ppm > 75 Then
                    IT_24.BackColor = Drawing.Color.Red
                    V24.ForeColor = Drawing.Color.Red
                    P_3GIA2102.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_24.BackColor = Drawing.Color.Yellow
                    V24.ForeColor = Drawing.Color.Brown
                    P_3GIA2102.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_24.BackColor = Drawing.Color.Lime
                    V24.ForeColor = Drawing.Color.Blue
                    P_3GIA2102.BackColor = Drawing.Color.Lime
                Else
                    IT_24.BackColor = Drawing.Color.Red
                    V24.ForeColor = Drawing.Color.Red
                    P_3GIA2102.BackColor = Drawing.Color.Red
                    V24.Text = "N/A"
                    V_3GIA2102.Text = "N/A"
                End If
            Else
                IT_24.BackColor = Drawing.Color.Red
                V24.ForeColor = Drawing.Color.Red
                P_3GIA2102.BackColor = Drawing.Color.Red
                V24.Text = "N/A"
                V_3GIA2102.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(25) = 1 Then
                d_ppm = value(25) * d_temp
                V25.Text = d_ppm
                V_3GIA2103.Text = d_ppm
                If d_ppm > 75 Then
                    IT_25.BackColor = Drawing.Color.Red
                    V25.ForeColor = Drawing.Color.Red
                    P_3GIA2103.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_25.BackColor = Drawing.Color.Yellow
                    V25.ForeColor = Drawing.Color.Brown
                    P_3GIA2103.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_25.BackColor = Drawing.Color.Lime
                    V25.ForeColor = Drawing.Color.Blue
                    P_3GIA2103.BackColor = Drawing.Color.Lime
                Else
                    IT_25.BackColor = Drawing.Color.Red
                    V25.ForeColor = Drawing.Color.Red
                    P_3GIA2103.BackColor = Drawing.Color.Red
                    V25.Text = "N/A"
                    V_3GIA2103.Text = "N/A"
                End If
            Else
                IT_25.BackColor = Drawing.Color.Red
                V25.ForeColor = Drawing.Color.Red
                P_3GIA2103.BackColor = Drawing.Color.Red
                V25.Text = "N/A"
                V_3GIA2103.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(26) = 1 Then
                d_ppm = value(26) * d_temp
                V26.Text = d_ppm
                V_3GIA2104.Text = d_ppm
                If d_ppm > 75 Then
                    IT_26.BackColor = Drawing.Color.Red
                    V26.ForeColor = Drawing.Color.Red
                    P_3GIA2104.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_26.BackColor = Drawing.Color.Yellow
                    V26.ForeColor = Drawing.Color.Brown
                    P_3GIA2104.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_26.BackColor = Drawing.Color.Lime
                    V26.ForeColor = Drawing.Color.Blue
                    P_3GIA2104.BackColor = Drawing.Color.Lime
                Else
                    IT_26.BackColor = Drawing.Color.Red
                    V26.ForeColor = Drawing.Color.Red
                    P_3GIA2104.BackColor = Drawing.Color.Red
                    V26.Text = "N/A"
                    V_3GIA2104.Text = "N/A"
                End If
            Else
                IT_26.BackColor = Drawing.Color.Red
                V26.ForeColor = Drawing.Color.Red
                P_3GIA2104.BackColor = Drawing.Color.Red
                V26.Text = "N/A"
                V_3GIA2104.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(27) = 1 Then
                d_ppm = value(27) * d_temp
                V27.Text = d_ppm
                V_3GIA2105.Text = d_ppm
                If d_ppm > 75 Then
                    IT_27.BackColor = Drawing.Color.Red
                    V27.ForeColor = Drawing.Color.Red
                    P_3GIA2105.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_27.BackColor = Drawing.Color.Yellow
                    V27.ForeColor = Drawing.Color.Brown
                    P_3GIA2105.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_27.BackColor = Drawing.Color.Lime
                    V27.ForeColor = Drawing.Color.Blue
                    P_3GIA2105.BackColor = Drawing.Color.Lime
                Else
                    IT_27.BackColor = Drawing.Color.Red
                    V27.ForeColor = Drawing.Color.Red
                    P_3GIA2105.BackColor = Drawing.Color.Red
                    V27.Text = "N/A"
                    V_3GIA2105.Text = "N/A"
                End If
            Else
                IT_27.BackColor = Drawing.Color.Red
                V27.ForeColor = Drawing.Color.Red
                P_3GIA2105.BackColor = Drawing.Color.Red
                V27.Text = "N/A"
                V_3GIA2105.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(28) = 1 Then
                d_ppm = value(28) * d_temp
                V28.Text = d_ppm
                V_3GIA2106.Text = d_ppm
                If d_ppm > 75 Then
                    IT_28.BackColor = Drawing.Color.Red
                    V28.ForeColor = Drawing.Color.Red
                    P_3GIA2106.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_28.BackColor = Drawing.Color.Yellow
                    V28.ForeColor = Drawing.Color.Brown
                    P_3GIA2106.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_28.BackColor = Drawing.Color.Lime
                    V28.ForeColor = Drawing.Color.Blue
                    P_3GIA2106.BackColor = Drawing.Color.Lime
                Else
                    IT_28.BackColor = Drawing.Color.Red
                    V28.ForeColor = Drawing.Color.Red
                    P_3GIA2106.BackColor = Drawing.Color.Red
                    V28.Text = "N/A"
                    V_3GIA2106.Text = "N/A"
                End If
            Else
                IT_28.BackColor = Drawing.Color.Red
                V28.ForeColor = Drawing.Color.Red
                P_3GIA2106.BackColor = Drawing.Color.Red
                V28.Text = "N/A"
                V_3GIA2106.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(29) = 1 Then
                d_ppm = value(29) * d_temp
                V29.Text = d_ppm
                V_3GIA2107.Text = d_ppm
                If d_ppm > 75 Then
                    IT_29.BackColor = Drawing.Color.Red
                    V29.ForeColor = Drawing.Color.Red
                    P_3GIA2107.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_29.BackColor = Drawing.Color.Yellow
                    V29.ForeColor = Drawing.Color.Brown
                    P_3GIA2107.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_29.BackColor = Drawing.Color.Lime
                    V29.ForeColor = Drawing.Color.Blue
                    P_3GIA2107.BackColor = Drawing.Color.Lime
                Else
                    IT_29.BackColor = Drawing.Color.Red
                    V29.ForeColor = Drawing.Color.Red
                    P_3GIA2107.BackColor = Drawing.Color.Red
                    V29.Text = "N/A"
                    V_3GIA2107.Text = "N/A"
                End If
            Else
                IT_29.BackColor = Drawing.Color.Red
                V29.ForeColor = Drawing.Color.Red
                P_3GIA2107.BackColor = Drawing.Color.Red
                V29.Text = "N/A"
                V_3GIA2107.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(30) = 1 Then
                d_ppm = value(30) * d_temp
                V30.Text = d_ppm
                V_3GIA2108.Text = d_ppm
                If d_ppm > 75 Then
                    IT_30.BackColor = Drawing.Color.Red
                    V30.ForeColor = Drawing.Color.Red
                    P_3GIA2108.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_30.BackColor = Drawing.Color.Yellow
                    V30.ForeColor = Drawing.Color.Brown
                    P_3GIA2108.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_30.BackColor = Drawing.Color.Lime
                    V30.ForeColor = Drawing.Color.Blue
                    P_3GIA2108.BackColor = Drawing.Color.Lime
                Else
                    IT_30.BackColor = Drawing.Color.Red
                    V30.ForeColor = Drawing.Color.Red
                    P_3GIA2108.BackColor = Drawing.Color.Red
                    V30.Text = "N/A"
                    V_3GIA2108.Text = "N/A"
                End If
            Else
                IT_30.BackColor = Drawing.Color.Red
                V30.ForeColor = Drawing.Color.Red
                P_3GIA2108.BackColor = Drawing.Color.Red
                V30.Text = "N/A"
                V_3GIA2108.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(31) = 1 Then
                d_ppm = value(31) * d_temp
                V31.Text = d_ppm
                V_3GIA2003.Text = d_ppm
                If d_ppm > 75 Then
                    IT_31.BackColor = Drawing.Color.Red
                    V31.ForeColor = Drawing.Color.Red
                    P_3GIA2003.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_31.BackColor = Drawing.Color.Yellow
                    V31.ForeColor = Drawing.Color.Brown
                    P_3GIA2003.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_31.BackColor = Drawing.Color.Lime
                    V31.ForeColor = Drawing.Color.Blue
                    P_3GIA2003.BackColor = Drawing.Color.Lime
                Else
                    IT_31.BackColor = Drawing.Color.Red
                    V31.ForeColor = Drawing.Color.Red
                    P_3GIA2003.BackColor = Drawing.Color.Red
                    V31.Text = "N/A"
                    V_3GIA2003.Text = "N/A"
                End If
            Else
                IT_31.BackColor = Drawing.Color.Red
                V31.ForeColor = Drawing.Color.Red
                P_3GIA2003.BackColor = Drawing.Color.Red
                V31.Text = "N/A"
                V_3GIA2003.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(32) = 1 Then
                d_ppm = value(32) * d_temp
                V32.Text = d_ppm
                V_3GIA2004.Text = d_ppm
                If d_ppm > 75 Then
                    IT_32.BackColor = Drawing.Color.Red
                    V32.ForeColor = Drawing.Color.Red
                    P_3GIA2004.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_32.BackColor = Drawing.Color.Yellow
                    V32.ForeColor = Drawing.Color.Brown
                    P_3GIA2004.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_32.BackColor = Drawing.Color.Lime
                    V32.ForeColor = Drawing.Color.Blue
                    P_3GIA2004.BackColor = Drawing.Color.Lime
                Else
                    IT_32.BackColor = Drawing.Color.Red
                    V32.ForeColor = Drawing.Color.Red
                    P_3GIA2004.BackColor = Drawing.Color.Red
                    V32.Text = "N/A"
                    V_3GIA2004.Text = "N/A"
                End If
            Else
                IT_32.BackColor = Drawing.Color.Red
                V32.ForeColor = Drawing.Color.Red
                P_3GIA2004.BackColor = Drawing.Color.Red
                V32.Text = "N/A"
                V_3GIA2004.Text = "N/A"
            End If

            d_ppm = 0
            If ConS(33) = 1 Then
                d_ppm = value(33) * d_temp
                V33.Text = d_ppm
                V_3GIA2005.Text = d_ppm
                If d_ppm > 75 Then
                    IT_33.BackColor = Drawing.Color.Red
                    V33.ForeColor = Drawing.Color.Red
                    P_3GIA2005.BackColor = Drawing.Color.Red
                ElseIf 75 >= d_ppm And d_ppm >= 35 Then
                    IT_33.BackColor = Drawing.Color.Yellow
                    V33.ForeColor = Drawing.Color.Brown
                    P_3GIA2005.BackColor = Drawing.Color.Yellow
                ElseIf 35 > d_ppm And d_ppm >= 0 Then
                    IT_33.BackColor = Drawing.Color.Lime
                    V33.ForeColor = Drawing.Color.Blue
                    P_3GIA2005.BackColor = Drawing.Color.Lime
                Else
                    IT_33.BackColor = Drawing.Color.Red
                    V33.ForeColor = Drawing.Color.Red
                    P_3GIA2005.BackColor = Drawing.Color.Red
                    V33.Text = "N/A"
                    V_3GIA2005.Text = "N/A"
                End If
            Else
                IT_33.BackColor = Drawing.Color.Red
                V33.ForeColor = Drawing.Color.Red
                P_3GIA2005.BackColor = Drawing.Color.Red
                V33.Text = "N/A"
                V_3GIA2005.Text = "N/A"
            End If

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
                        If CInt(rd.Item(i).ToString) < 0 Then
                            g_aDataSet(i + startIdx) = 0
                        Else
                            g_aDataSet(i + startIdx) = rd.Item(i).ToString
                        End If

                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Private Sub GetData_maxCO(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=localhost;Initial Catalog=TEST2;User ID=sa;Password=1234"
        If redimArray Then : ReDim g_aDataSet_maxco(intCount) : End If

        Try
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            'g_sConn = New SqlConnection(strDbCon)
            g_sCmd = New SqlCommand(g_strSQL_maxco, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows Then
                While rd.Read
                    For i As Integer = 0 To rd.FieldCount - 1 Step 1
                        If CInt(rd.Item(i).ToString) < 0 Then
                            g_aDataSet_maxco(i + startIdx) = 0
                        Else
                            g_aDataSet_maxco(i + startIdx) = rd.Item(i).ToString
                        End If

                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Private Sub GetData_W(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=localhost;Initial Catalog=TEST2;User ID=sa;Password=1234"
        If redimArray Then : ReDim g_aDataSetWind(intCount) : End If

        Try
            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            'g_sConn = New SqlConnection(strDbCon)
            g_sCmd = New SqlCommand(g_strSQL, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows Then
                While rd.Read
                    For i As Integer = 0 To rd.FieldCount - 1 Step 1
                        g_aDataSetWind(i + startIdx) = rd.Item(i).ToString
                    Next
                End While
            End If

            g_sConn.Close()

        Catch sErr As SqlException
            MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try

    End Sub

    Private Sub GetData_Time(ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0)
        Dim rd As SqlDataReader = Nothing
        'Dim strDbCon As String
        'strDbCon = "Data Source=.;Initial Catalog=TEST2;User ID=sa;Password=1234"

        If redimArray Then : ReDim g_aDataSet2(intCount) : End If

        Try

            g_sConn = New SqlConnection(getConnStr(Application("ConnStr")))
            g_sCmd = New SqlCommand(g_strSQL_time, g_sConn)
            g_sConn.Open()
            rd = g_sCmd.ExecuteReader()

            If rd.HasRows() Then
                rd.Read()
                g_aDataSet2(0) = rd.Item(0)
            Else
                g_aDataSet2(0) = #1/1/1911#
            End If
            g_sConn.Close()

        Catch sErr As SqlException
            MsgBox(sErr.Message)
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert(""" + sErr.Message + """);", True)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '主程式
        MainProcess()
        MainProcess_3()
        ProcessWind()
        'wind_direction_W.Value = "45"
        'wind_direction_E.Value = "45"


    End Sub

End Class
