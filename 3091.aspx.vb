Imports System.Web.Services
Imports System.Net.NetworkInformation
Imports System.Threading.Tasks
Imports System.Collections.Generic

Partial Public Class _3091
    Inherits System.Web.UI.Page
    Public Const PAGE_ID = "3091"

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
    ''' HSM 系統 PING 狀態查詢 WebMethod
    ''' 回傳格式：Dictionary(Key=系統名稱, Value="狀態|IP位置")
    ''' 例如：{"HOST" -> "N|10.109.2.2", "HPMIS" -> "E|10.108.20.21"}
    ''' </summary>
    <WebMethod()>
    Public Shared Function GetSystemStatus() As Dictionary(Of String, String)
        ' HSM 系統 IP 對應表
        Dim hostIpMap As New Dictionary(Of String, String)() From {
            {"HOST", "10.109.2.2"},
            {"HPMIS", "10.108.20.21"},
            {"CYMC", "10.108.10.21"},
            {"APFC", "10.108.9.21"},
            {"SQC", "10.108.13.21"},
            {"HSMCARAT", "10.108.21.21"},
            {"W4MES", "10.108.20.4"},
            {"FCE", "10.108.6.21"},
            {"HRFSPC", "10.108.16.21"},
            {"HRS", "10.108.8.21"},
            {"MIL", "10.108.7.21"},
            {"PYMC", "10.108.19.21"},
            {"HSMSPC", "10.108.15.21"},
            {"SYMC", "10.108.5.21"},
            {"TG", "10.108.14.21"},
            {"TNRL1", "10.108.11.21"},
            {"TNRL2", "10.108.12.21"},
            {"TNRL3", "10.108.17.21"},
            {"TNRL4", "10.108.22.21"},
            {"DYMC", "10.108.19.26"}
        }

        Dim statusDict As New Dictionary(Of String, String)()
        Dim lockObj As New Object()

        ' 並行 PING，逾時 1000ms
        Parallel.ForEach(hostIpMap, Sub(kvp)
                                        Dim pcName As String = kvp.Key
                                        Dim ipAddress As String = kvp.Value
                                        Dim statusCode As String = "E"  ' 預設：斷線

                                        If Not String.IsNullOrEmpty(ipAddress) Then
                                            Try
                                                Using p As New Ping()
                                                    Dim reply As PingReply = p.Send(ipAddress, 1000)
                                                    If reply IsNot Nothing AndAlso reply.Status = IPStatus.Success Then
                                                        statusCode = "N"  ' 正常
                                                    End If
                                                End Using
                                            Catch
                                                statusCode = "E"
                                            End Try
                                        End If

                                        ' 回傳格式：「狀態碼|IP位置」，前端以 | 分隔解析
                                        SyncLock lockObj
                                            statusDict(pcName) = $"{statusCode}|{ipAddress}"
                                        End SyncLock
                                    End Sub)

        Return statusDict
    End Function
End Class