Imports System.Data.SqlClient
Imports hPMISWEB.modGeneral
Imports hPMISWEB.modSQL


Partial Public Class PageHeader
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            loadMenu()

            btnHelp.OnClientClick = "javascript:window.open('onlinehelp.aspx?pid=" + lblShowTitle_ID.Text + "','','width=550,,height=350,toolbar=no,location=no,directorybuttons=no,scrollbars=yes')"
            lblShowNow_header.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
            'Me.txtPageID.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) {document.getElementById('" + Me.btnGoto_Page.ClientID + "').focus();}")
        End If
    End Sub



    ' PosArray              位置字串陣列
    ' nodeName  `           節點名稱
    ' url                   網址
    ' node                  父節點
    Private Sub setNode(ByVal PosArray As String(), ByVal nodeName As String, ByVal url As String, Optional ByVal node As MenuItem = Nothing, Optional ByVal lvl As Integer = 0)
        Dim nodeTmp As MenuItem = node
        Dim idx As Integer = Integer.Parse(PosArray(lvl))

        If nodeTmp Is Nothing Then
            If PosArray.Length = 1 Then
                Menu1.Items.Add(New MenuItem(nodeName, lvl, "", url))
                Exit Sub
            Else
                If Menu1.Items(idx) IsNot Nothing Then
                    nodeTmp = Menu1.Items(idx)
                    'Else
                    '    Exit Sub
                End If
            End If
        Else
            If idx + 1 > nodeTmp.ChildItems.Count Then
                nodeTmp.ChildItems.Add(New MenuItem(nodeName, lvl, "", url))
                Exit Sub
            Else
                If nodeTmp.ChildItems(idx) IsNot Nothing Then
                    nodeTmp = nodeTmp.ChildItems(idx)
                    'Else
                    '    nodeTmp.ChildItems.Add(New MenuItem(nodeName, url))
                    '    Exit Sub
                End If
            End If
        End If

        lvl += 1
        If lvl <= PosArray.Length - 1 Then
            setNode(PosArray, nodeName, url, nodeTmp, lvl)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub loadMenu()
        Dim strSQL As String = "SELECT * FROM sys_page ORDER BY Node_Route "
        Dim strErr As String = ""
        Dim sreader As SqlDataReader = Nothing
        Dim conn As SqlConnection = Nothing
        Dim tmpItem As MenuItem = Nothing
        Dim dt As DataTable = Nothing
        Dim tmpArray As String() = Nothing
        Dim tmpNodeName As String = ""
        Dim tmpUrl As String = ""

        Menu1.Items.Clear()
        Try
            conn = New SqlConnection(getConnStr(Application("ConnStr")))
            conn.Open()

            dt = New DataTable
            If getData(strSQL, dt, conn, strErr) Then
                For Each dr As DataRow In dt.Rows
                    If dr(8).ToString.Trim <> "#" Then
                        'Node_Route
                        tmpArray = dr(8).ToString.Split(",")
                        'node name
                        If Not dr(0).ToString.StartsWith("0") Then
                            tmpNodeName = dr(0).ToString + dr(1).ToString
                        Else
                            tmpNodeName = dr(1)
                        End If
                        'url
                        If dr(0).ToString.StartsWith("1") Then
                            tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("iPmisUrl") + dr(2).ToString
                        ElseIf dr(0).ToString.StartsWith("2") Then
                            tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("sPmisUrl") + dr(2).ToString
                        ElseIf dr(0).ToString.StartsWith("3") Then
                            tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl") + dr(2).ToString
                        ElseIf dr(0).ToString.StartsWith("0") And dr(2).ToString.Trim <> "#" Then
                            If dr(0).ToString = "0002" Or dr(0).ToString.Substring(0, 3) = "002" Then
                                tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("sPmisUrl") + dr(2).ToString
                            ElseIf dr(0).ToString = "0004" Or dr(0).ToString.Substring(0, 3) = "004" Then
                                tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl") + dr(2).ToString
                                '1020918 added by Jimmy Chang 處理型鋼聯結錯誤的問題
                            ElseIf dr(0).ToString = "0005" Or dr(0).ToString.Substring(0, 3) = "005" Then
                                tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl") + dr(2).ToString
                            ElseIf dr(0).ToString = "0036" Then
                                tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("sPmisUrl") + dr(2).ToString
                            Else
                                tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("iPmisUrl") + dr(2).ToString
                            End If
                        End If
                        setNode(tmpArray, tmpNodeName, tmpUrl)

                        'tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl") + dr(2).ToString
                        'setNode(tmpArray, tmpNodeName, tmpUrl)
                    End If
                Next
            Else
                '取得page資料時發生錯誤
            End If

            conn.Close()

            'lblUser.Text = Session("uid")
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub setNode(ByVal PosArray As String(), ByVal nodeName As String, ByVal url As String, Optional ByVal lvl As Integer = 0, Optional ByVal node As MenuItem = Nothing)
    '    Dim lvlTmp As Integer = lvl
    '    Dim nodeTmp As MenuItem = node
    '    Dim idx As Integer = Integer.Parse(PosArray(lvl))

    '    If nodeTmp Is Nothing Then
    '        If Menu1.Items(idx) IsNot Nothing Then
    '            nodeTmp = Menu1.Items(idx)
    '        Else
    '            Exit Sub
    '        End If
    '    Else
    '        If nodeTmp.ChildItems(idx) IsNot Nothing Then
    '            nodeTmp = nodeTmp.ChildItems(idx)
    '        Else
    '            nodeTmp.ChildItems.Add(New MenuItem(nodeName, url))
    '            Exit Sub
    '        End If
    '    End If

    '    lvl += 1
    '    If PosArray.Length <= lvl Then
    '        setNode(PosArray, nodeName, url, lvlTmp, nodeTmp)
    '    Else
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub loadMenu()
    '    Dim strSQL As String = "SELECT * FROM sys_page ORDER BY Node_Route "
    '    Dim strErr As String = ""
    '    Dim sreader As SqlDataReader = Nothing
    '    Dim conn As SqlConnection = Nothing
    '    Dim tmpItem As MenuItem = Nothing
    '    Dim dt As DataTable = Nothing
    '    Dim tmpArray As String() = Nothing
    '    Dim tmpNodeName As String = ""
    '    Dim tmpUrl As String = ""

    '    Menu1.Items.Clear()
    '    Try
    '        conn = New SqlConnection(getConnStr(Application("ConnStr")))
    '        conn.Open()

    '        dt = New DataTable
    '        If getData(strSQL, dt, conn, strErr) Then
    '            For Each dr As DataRow In dt.Rows
    '                If dr(8).ToString.Trim <> "#" Then
    '                    'Node_Route
    '                    tmpArray = dr(8).ToString.Split(",")
    '                    'node name
    '                    If Not dr(0).ToString.StartsWith("0") Then
    '                        tmpNodeName = dr(0).ToString + dr(1).ToString
    '                    Else
    '                        tmpNodeName = dr(1)
    '                    End If
    '                    'url
    '                    If dr(0).ToString.StartsWith("1") Then
    '                        tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("iPmisUrl") + dr(2).ToString
    '                    ElseIf dr(0).ToString.StartsWith("2") Then
    '                        tmpUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("sPmisUrl") + dr(2).ToString
    '                    End If

    '                    setNode(tmpArray, tmpNodeName, tmpUrl)
    '                End If
    '            Next
    '        Else
    '            '取得page資料時發生錯誤
    '        End If

    '        conn.Close()

    '        lblUser.Text = Session("uid")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles Menu1.MenuItemClick
        Dim strUrl As String
        If e.Item.Depth > 0 Then '第一層不執行加密及轉址
            'Dim aa As String
            'aa = CType(Me.Page.FindControl("PAGE_ID"), String)
            'aa = Str(Me.Page.FindControl("PAGE_ID"))
            'Me.Page.Form.FindControl("PAGE_ID")

            strUrl = addEncryptUID(e.Item.Value)

            Response.Redirect(strUrl)

        Else
            If e.Item.Value = "Logout" Then
                Session.Clear()
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Protected Sub btnGoto_Page_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoto_Page.Click
        Dim strSQL As String = "", strErr As String = ""
        Dim sreader As SqlDataReader = Nothing
        Dim conn As New SqlConnection(getConnStr(Application("ConnStr")))
        Dim strUrl As String

        If txtPageID.Text.Length <> 4 Then
            Exit Sub
        End If
        Try
            strSQL = "SELECT * FROM sys_page WHERE pid = '" + txtPageID.Text + "' "
            conn.Open()
            sreader = execReader(strSQL, strErr, conn)
            If Not sreader Is Nothing Then
                If sreader.HasRows Then
                    While sreader.Read()
                        strUrl = addEncryptUID(sreader.Item("url"))

                        If strUrl.StartsWith("1") Then
                            strUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("iPmisUrl") + strUrl
                        ElseIf strUrl.StartsWith("2") Then
                            strUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("sPmisUrl") + strUrl
                        ElseIf strUrl.StartsWith("3") Then
                            strUrl = System.Web.Configuration.WebConfigurationManager.AppSettings("hPmisUrl") + strUrl
                        End If

                        Response.Redirect(strUrl)

                    End While
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function addEncryptUID(ByVal strUrl As String) As String
        Dim strEncryptUID As String
        If Session("uid") IsNot Nothing Then
            '加密 ex:uid=[kent];datetime=[2008/07/23 09:00:00]
            strEncryptUID = Encrypt("[" + Session("uid") + "];[" + Now.ToString("yyyy/MM/dd HH:mm:ss") + "]", SYS_PRIVATE_KEY)

            If strUrl.IndexOf("?") > 0 Then
                strUrl += "&uid=" + strEncryptUID
            Else
                strUrl += "?uid=" + strEncryptUID
            End If
        End If

        Return strUrl

    End Function

    Protected Sub TimerALL_Tick(sender As Object, e As EventArgs) Handles TimerALL.Tick
        lblShowNow_header.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
    End Sub
End Class