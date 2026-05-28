Imports System.Net
Imports System.Net.NetworkInformation


Partial Public Class HBMsys
    Inherits Page

    Public Const PAGE_ID = "3092"
    Private Const pcHOST_bad = "~/images/pc_host_bad.jpg"
    Private Const pcHOST_normal = "~/images/pc_host_normal.jpg"
    Private Const pcPMIS_bad = "~/images/pc_pmis_bad.jpg"
    Private Const pcPMIS_normal = "~/images/pc_pmis_normal.jpg"
    Private Const pcSVR_bad = "~/images/pc_svr_bad.jpg"
    Private Const pcSVR_normal = "~/images/pc_svr_normal.jpg"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            '設定Title
            setTitle(Me, PAGE_ID)

            '取得user的uid, gid, name
            getPageUser(Me)

            '主程式
            MainProcess()




        End If
    End Sub

    Private Sub MainProcess()
        Dim arrPcName() As String = {"HBMFCE", "HBMSPC", "HBMCARAT", "HBMMIL", "HOST", "HBMPMIS"}

        For i As Integer = 0 To arrPcName.Length - 1

            ShowData(arrPcName(i))
        Next


    End Sub

    Private Sub ShowData(ByVal strPcName As String)

        Dim pingo As Ping = Nothing
        Dim reply As PingReply = Nothing
        Dim ipA As IPAddress = Nothing






        Select Case strPcName.ToUpper.Trim

            Case "HOST"
                pingo = New Ping
                ipA = IPAddress.Parse("10.109.2.1")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHOST.ImageUrl = pcHOST_normal

                Else
                    imgHOST.ImageUrl = pcHOST_bad
                End If
            Case "HBMPMIS"
                pingo = New Ping
                ipA = IPAddress.Parse("10.108.20.11")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHBMPMIS.ImageUrl = pcPMIS_normal
                Else
                    imgHBMPMIS.ImageUrl = pcPMIS_bad
                End If
            Case "HBMMIL"
                pingo = New Ping
                ipA = IPAddress.Parse("10.108.37.21")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHBMMIL.ImageUrl = pcSVR_normal
                    lblHBMMIL_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHBMMIL.ImageUrl = pcSVR_bad
                End If
            Case "HBMCARAT"
                pingo = New Ping
                ipA = IPAddress.Parse("10.108.37.11")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHBMCARAT.ImageUrl = pcSVR_normal
                    lblHBMCARAT_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHBMCARAT.ImageUrl = pcSVR_bad
                End If
            Case "HBMSPC"
                pingo = New Ping
                ipA = IPAddress.Parse("10.108.37.232")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHBMSPC.ImageUrl = pcSVR_normal
                    lblHBMSPC_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHBMSPC.ImageUrl = pcSVR_bad
                End If
            Case "HBMFCE"
                pingo = New Ping
                ipA = IPAddress.Parse("10.108.38.21")
                reply = pingo.Send(ipA, 100)
                If reply.Status = IPStatus.Success Then
                    imgHBMFCE.ImageUrl = pcSVR_normal
                    lblHBMFCE_t.Text = Date.Now.ToString("MM/dd hh:mm:ss")
                Else
                    imgHBMFCE.ImageUrl = pcSVR_bad
                End If

        End Select
    End Sub


End Class