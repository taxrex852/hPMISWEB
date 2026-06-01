Imports System.Web.Services
Imports System.Net.NetworkInformation
Imports System.Threading.Tasks
Imports System.Collections.Generic

Partial Public Class HBMsys
    Inherits System.Web.UI.Page
    Public Const PAGE_ID = "3092"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' 標準初始化與權限檢查
            setTitle(Me, PAGE_ID)
            getPageUser(Me)
            Dim strErr As String = ""
            If chkPageAuth(Session("gid"), PAGE_ID, 1, HttpContext.Current.Application("ConnStr").ToString(), strErr, Me) <> 0 Then Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' HBM 系統 PING 狀態查詢 WebMethod
    ''' 拓撲結構：HOST → HBMPMIS → HBMFCE / HBMMIL / HBMSPC / HBMCARAT
    ''' </summary>
    <WebMethod()>
    Public Shared Function GetSystemStatus() As Dictionary(Of String, String)
        ' HBM 系統 IP 對應表（來源：原 3092.aspx.vb ShowData 函式）
        Dim hostIpMap As New Dictionary(Of String, String)() From {
            {"HOST",     "10.109.2.1"},
            {"HBMPMIS",  "10.108.20.11"},
            {"HBMFCE",   "10.108.38.21"},
            {"HBMMIL",   "10.108.37.21"},
            {"HBMSPC",   "10.108.37.232"},
            {"HBMCARAT", "10.108.37.11"}
        }

        Dim statusDict As New Dictionary(Of String, String)()
        Dim lockObj As New Object()

        ' 並行 PING，逾時 1000ms
        Parallel.ForEach(hostIpMap, Sub(kvp)
                                        Dim pcName As String = kvp.Key
                                        Dim ipAddress As String = kvp.Value
                                        Dim status As String = "E"  ' 預設：斷線

                                        If Not String.IsNullOrEmpty(ipAddress) Then
                                            Try
                                                Using p As New Ping()
                                                    Dim reply As PingReply = p.Send(ipAddress, 1000)
                                                    If reply IsNot Nothing AndAlso reply.Status = IPStatus.Success Then
                                                        status = "N"  ' 正常
                                                    End If
                                                End Using
                                            Catch
                                                status = "E"
                                            End Try
                                        End If

                                        SyncLock lockObj
                                            statusDict(pcName) = status
                                        End SyncLock
                                    End Sub)

        Return statusDict
    End Function
End Class