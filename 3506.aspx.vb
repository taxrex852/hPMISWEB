Imports System.Data.SqlClient

Partial Public Class HBM_Process
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3506"
    Private Conn As SqlConnection
    Private strACCESS As String
    Private chartDate As Date
    ' 停機判斷門檻（單位：分鐘）
    Private Const DeviceDownThreshold As Integer = 60

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

    ' 判斷是否超過DeviceDownThreshold分鐘未收到mp01
    'Private Function CheckDeviceStatus(ByVal Conn As SqlConnection) As Boolean
    '    Dim dtTmp As DataTable = Nothing
    '    Dim strSQL As String = ""
    '    Dim bDeviceStatus As Boolean = False

    '    strSQL = "SELECT TOP 1 process_date FROM h_pmis_mp01 ORDER BY process_date DESC"
    '    dtTmp = execQuery(strSQL, "", Conn)
    '    If dtTmp IsNot Nothing Then
    '        If dtTmp.Rows.Count > 0 Then
    '            If CType(dtTmp.Rows(0).Item(0), Date).AddMinutes(DeviceDownThreshold) >= Now Then
    '                bDeviceStatus = True
    '            End If
    '        End If
    '    End If

    '    Return bDeviceStatus
    'End Function

    Private Sub createtable()
        Dim dtTmp As DataTable = Nothing
        Dim shift_code As String

        '依現在時間決定班別
        Select Case Now.Hour
            Case 7 To 14
                shift_code = "8"
            Case 15 To 22
                shift_code = "K"
            Case Else
                shift_code = "Q"
        End Select




        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        Conn.Open()

        Dim HBMConn As SqlConnection = New SqlConnection("Data Source=10.108.20.11;Initial Catalog=HBMPMIS;User ID=sa;Password=Y6P2!@#")
        HBMConn.Open()
        Dim da1 As SqlDataAdapter = New SqlDataAdapter("Select round(Sum(SumWeight)/1000,0)  From vw_ForW4mesweb_3506 Where Reject_Code = '0' And SUBSTRING(CONVERT(char, sys_process_date, 112), 1, 8)='" + Now.ToString("yyyyMMdd") + "' AND Bundle_of_Shift_No = '" + shift_code + "' ", HBMConn)
        Dim ds1 As DataSet = New DataSet()
        da1.Fill(ds1, "vw_ForW4mesweb_3506")



        ' strACCESS = "Select Sum(SumWeight)  From vw_ForW4mesweb_3506 Where Reject_Code = '0' And SUBSTRING(CONVERT(char, sys_process_date, 112), 1, 8)='" + Now.ToString("yyyyMMdd") + "' AND Bundle_of_Shift_No = '" + shift_code + "' "

        If ds1.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() = "" Then

            lblShift.Text = "產線定修"

        Else
            lblShift.Text = ds1.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() '班產量1090407修正型鋼產量算方式(實際長度*鋼胚單重)
        End If





        '-----1090407修正型鋼產量算方式(實際長度*鋼胚單重)
        ' strACCESS = "Select Sum(SumWeight)  From vw_ForW4mesweb_3506 Where  Reject_Code = '0' AND Bundle_of_Shift_No = 'K'   And sys_process_date between SUBSTRING(Convert(Char, GETDATE(), 121), 1, 10) + ' 00:00:00' and SUBSTRING(Convert(Char, GETDATE(), 121), 1, 10) + ' 23:00:00' "

        Dim da2 As SqlDataAdapter = New SqlDataAdapter("Select round(Sum(SumWeight)/1000,0)  From vw_ForW4mesweb_3506 Where  Reject_Code = '0'   And sys_process_date between CONVERT(datetime, dateadd(HH,-1, CONVERT(datetime,  SUBSTRING(Convert(Char, GETDATE(), 121), 1, 10) + ' 00:00:00'  ))) and SUBSTRING(Convert(Char, GETDATE(), 121), 1, 10) + ' 23:00:00.000' ", HBMConn)
        Dim ds2 As DataSet = New DataSet()
        da2.Fill(ds2, "vw_ForW4mesweb_3506")


        If ds2.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() = "" Then
            lblDay.Text = "產線定修"

        Else
            lblDay.Text = ds2.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() '日產量1090407修正型鋼產量算方式(實際長度*鋼胚單重)
        End If



        'strACCESS = "Select Sum(SumWeight) From vw_ForW4mesweb_3506 Where  Reject_Code = '0'  And sys_process_date > SUBSTRING(Convert(Char, GETDATE(), 121), 1, 7) + '-01 00:00:00'   "

        Dim da3 As SqlDataAdapter = New SqlDataAdapter("Select round(Sum(SumWeight)/1000,0) From vw_ForW4mesweb_3506 Where  Reject_Code = '0'  And sys_process_date > CONVERT(datetime, dateadd(HH,-1, CONVERT(datetime,  SUBSTRING(CONVERT(char,GETDATE(), 121),1,7) + '-01 00:00:00' )))", HBMConn)
        Dim ds3 As DataSet = New DataSet()
        da3.Fill(ds3, "vw_ForW4mesweb_3506")

        If ds3.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() = "" Then
            lblMonth.Text = "產線定修"

        Else
            lblMonth.Text = ds3.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() '月產量1090407修正型鋼產量算方式(實際長度*鋼胚單重)
        End If


        Dim da4 As SqlDataAdapter = New SqlDataAdapter("SELECT top 1 boundle_date, HBM_No, bound_weight, Product_Size_Code, SGC FROM h_pmis_hbm_info where Reject_Code = '0' ORDER BY boundle_date DESC ", HBMConn)
        Dim ds4 As DataSet = New DataSet()
        da4.Fill(ds4, "h_pmis_hbm_info")




        'strACCESS = "SELECT top 1 boundle_date, Lot_No, Bundle_No, bound_weight, Product_Size_Code, SGC FROM h_pmis_hbm_info where Reject_Code = '0' ORDER BY boundle_date DESC "
        'dtTmp = execQuery(strACCESS, "", Conn)

        'If dtTmp IsNot Nothing Then
        '    If dtTmp.Rows.Count <> 0 Then
        '        Dim temp_time As Date
        '        temp_time = "#" & dtTmp.Rows(0).Item(0).ToString() & "#"
        '        lblData.Text = Format(temp_time, "yyyy/MM/dd HH:mm:ss")

        '        lblW.Text = dtTmp.Rows(0).Item(3).ToString
        '        lblLast.Text = dtTmp.Rows(0).Item(1).ToString & "-" & dtTmp.Rows(0).Item(2).ToString
        '        lblKind.Text = dtTmp.Rows(0).Item(5).ToString
        '        lblP.Text = dtTmp.Rows(0).Item(4).ToString

        '    Else
        '        lblData.Text = "N/A"
        '        lblP.Text = "N/A"
        '        lblW.Text = "N/A"
        '        lblKind.Text = "N/A"
        '        lblLast.Text = "N/A"
        '    End If
        'End If
        lblData.Text = ds4.Tables("h_pmis_hbm_info").Rows(0)(0).ToString()
        lblW.Text = ds4.Tables("h_pmis_hbm_info").Rows(0)(2).ToString()
        lblLast.Text = ds4.Tables("h_pmis_hbm_info").Rows(0)(1).ToString()
        lblKind.Text = ds4.Tables("h_pmis_hbm_info").Rows(0)(4).ToString()
        lblP.Text = ds4.Tables("h_pmis_hbm_info").Rows(0)(3).ToString()

        If ds1.Tables("vw_ForW4mesweb_3506").Rows(0)(0).ToString() = "" Then
            lblStatus.Text = "目前產線暫停中！"
            lblStatus.ForeColor = Drawing.Color.Firebrick
            'lblData.Text = "N/A"
            'lblP.Text = "N/A"
            'lblW.Text = "N/A"
            'lblKind.Text = "N/A"
            'lblLast.Text = "N/A"

        Else
            lblStatus.Text = "目前正在生產中！"
            lblStatus.ForeColor = Drawing.Color.Blue



        End If


        lblNow.Text = Now.ToString
        '100.08.03林弘男要求修正
        'If CheckDeviceStatus(Conn) = True Then
        '    lblStatus.Text = "目前正在生產中！"
        '    lblStatus.ForeColor = Drawing.Color.Blue
        'Else
        '    lblStatus.Text = "目前產線暫停中！"
        '    lblStatus.ForeColor = Drawing.Color.Firebrick
        'End If
        'lblStatus.Visible = CheckDeviceStatus(Conn)
        Conn.Close()
        HBMConn.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        createtable()
    End Sub
End Class