Imports System.Data.SqlClient

Module modSQL

    Public Function getData(ByVal strSQL As String, ByRef dt As DataTable, ByRef conn As SqlConnection, ByRef strErr As String) As Boolean
        Dim scmd As SqlCommand = Nothing
        Dim sadapter As SqlDataAdapter = Nothing
        Dim bResult As Boolean = False

        Try
            '資料庫連線檢查
            If conn IsNot Nothing Then
                If conn.State <> ConnectionState.Open Then
                    strErr = "SQL連線未開啟"
                Else
                    scmd = New SqlCommand(strSQL, conn)
                    sadapter = New SqlDataAdapter(scmd)
                    sadapter.Fill(dt)

                    bResult = True
                End If
            Else
                strErr = "請先建立SQL連線"
            End If
        Catch ex As Exception
            strErr = ex.Message
        End Try

        Return bResult
    End Function

    Public Function execReader(ByVal strSQL As String, ByRef errMsg As String, ByVal conn As SqlConnection) As SqlDataReader
        Dim reader As SqlDataReader = Nothing
        Dim scmd As SqlCommand = Nothing
        Try
            '建立連線
            If Not conn Is Nothing Then
                If conn.State <> ConnectionState.Open Then
                    errMsg = "SQL連線未開啟"
                    Return Nothing
                End If
            Else
                errMsg = "請先建立SQL連線"
                Return Nothing
            End If

            '產生SQL Cmmand
            scmd = New SqlCommand(strSQL, conn)
            '執行SQL Command
            reader = scmd.ExecuteReader
            Return reader
        Catch sErr As SqlException
            errMsg = sErr.Message
            Return Nothing
        End Try
    End Function

    Public Function execQuery(ByVal strSQL As String, ByRef strErr As String, ByVal connPMIS As SqlConnection) As DataTable
        Dim command As SqlCommand = Nothing
        Dim adapter As SqlDataAdapter = Nothing
        Dim table As DataTable = Nothing

        Try
            command = New SqlCommand(strSQL, connPMIS)
            adapter = New SqlDataAdapter()
            adapter.SelectCommand = command

            table = New DataTable
            table.Locale = System.Globalization.CultureInfo.InvariantCulture
            adapter.Fill(table)
        Catch ex As Exception
            'logMsg(strAppName, "execQuery: " + ex.Message, LogLevel.lvl_process_log)
            strErr = ex.Message
            Return Nothing
        End Try

        Return table
    End Function

    Public Function execSQL(ByVal strSQL As String, ByRef errMsg As String, ByVal conn As SqlConnection, ByVal transaction As SqlClient.SqlTransaction) As Integer
        Dim iCount As Integer = 0
        Dim tmpConn As SqlConnection = Nothing
        Dim scmd As SqlCommand = Nothing

        Try
            '建立連線
            tmpConn = conn
            If tmpConn.State <> ConnectionState.Open Then tmpConn.Open()
            scmd = New SqlCommand(strSQL, tmpConn, transaction)

            '執行SQL Command
            iCount = scmd.ExecuteNonQuery()
            Return iCount
        Catch sErr As SqlException
            errMsg = sErr.Message
            Return -1
        End Try
    End Function

    Public Function execSQL(ByVal strSQL As String, ByRef errMsg As String, ByVal connString As String) As Integer
        Dim iCount As Integer = 0
        Dim tmpConn As SqlConnection = Nothing
        Dim scmd As SqlCommand = Nothing

        Try
            '建立連線
            tmpConn = New SqlConnection(connString)
            tmpConn.Open()
            scmd = New SqlCommand(strSQL, tmpConn)

            '執行SQL Command
            iCount = scmd.ExecuteNonQuery()
            tmpConn.Close()
            Return iCount
        Catch sErr As SqlException
            errMsg = sErr.Message
            If Not tmpConn Is Nothing Then
                If tmpConn.State = ConnectionState.Open Then tmpConn.Close()
            End If
            Return -1
        End Try
    End Function

    '權限驗證
    ' =================================================
    ' 1. 根據pid檢查是否需要權限驗證，若否則return true。
    ' 2. 若需要驗證，則根據gid找出對應群組是否有pid的授權，
    '    若有則return true，反之return false。
    ' =================================================
    ' mCode： 1:讀取驗證  2:寫入驗證
    ' =================================================
    ' Return： 0:正常  -1:未登入  -2:無權限  -99:其他錯誤
    ' =================================================
    Public Function chkAuth(ByVal gid As String, ByVal pid As String, ByVal mCode As Integer, ByRef strConn As String, ByVal strErr As String) As Integer
        Dim strSQL As String = ""
        Dim sreader As SqlDataReader = Nothing
        Dim conn As SqlConnection = Nothing
        Dim r_check As Boolean = False, w_check As Boolean = False, bTmp As Boolean = False
        Dim bResult As Integer = -99

        Try
            conn = New SqlConnection(strConn)
            conn.Open()
            strSQL = "SELECT read_check, write_check FROM sys_page WHERE pid='" & pid.Trim & "'"
            sreader = execReader(strSQL, strErr, conn)
            If Not sreader Is Nothing Then
                If sreader.HasRows Then
                    sreader.Read()
                    r_check = IIf(CType(sreader.GetValue(0), String).Trim = "Y", True, False)
                    w_check = IIf(CType(sreader.GetValue(1), String).Trim = "Y", True, False)
                    sreader.Close()

                    If mCode = 1 Then
                        bTmp = r_check
                    ElseIf mCode = 2 Then
                        bTmp = w_check
                    End If

                    '權限驗證
                    '若驗證為false，則return true
                    If Not bTmp Then
                        bResult = 0
                    Else
                        '若gid=""，則為未登入
                        If gid = "" Then
                            bResult = -1
                        Else
                            '若讀取驗證為true，則檢查權限群組
                            strSQL = "SELECT read_auth, write_auth FROM sys_auth WHERE gid='" & gid.Trim & "' AND pid='" & pid.Trim & "'"
                            sreader = execReader(strSQL, strErr, conn)
                            If Not sreader Is Nothing Then
                                If sreader.HasRows Then
                                    sreader.Read()
                                    r_check = IIf(CType(sreader.GetValue(0), String).Trim = "Y", True, False)
                                    w_check = IIf(CType(sreader.GetValue(1), String).Trim = "Y", True, False)
                                    sreader.Close()

                                    If mCode = 1 Then
                                        bTmp = r_check
                                    ElseIf mCode = 2 Then
                                        bTmp = w_check
                                    End If

                                    bResult = IIf(bTmp, 0, -2)
                                Else
                                    '無資料
                                    'bResult = False
                                    strErr = "無群組權限相關資料[sys_auth]。"
                                    sreader.Close()
                                End If
                            Else
                                '資料取得失敗
                                'bResult = False
                                strErr = "取得群組權限相關資料失敗[sys_auth]"
                            End If
                        End If
                    End If
                Else
                    '無資料
                    'bResult = False
                    strErr = "無網頁相關資料[sys_page]。"
                    sreader.Close()
                End If
            Else
                '資料取得失敗
                'bResult = False
                strErr = "取得網頁相關資料失敗[sys_page]"
            End If
            conn.Close()
        Catch ex As SqlException
            'bResult = False
            strErr = ex.Message
        End Try

        Return bResult
    End Function

    Public Function chkPageAuth(ByVal gid As String, ByVal pid As String, ByVal iMode As Integer, ByVal strConn As String, ByRef strErr As String, ByVal WebPage As System.Web.UI.Page) As Integer
        Dim iResult As Integer = 0

        iResult = chkAuth(gid, pid, iMode, strConn, strErr)
        If iMode = 1 Then
            If iResult = 0 Then
                'showMsgbox("read OK", WebPage)
            ElseIf iResult = -1 Then
                'showMsgbox("尚未登入，將返回登入頁。", WebPage)
                WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>alert('未登入');document.location.href='Default.aspx';</script>")
            ElseIf iResult = -2 Then
                'showMsgbox("無讀取權限，將返回上一頁。", WebPage)
                'WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>history.back();</script>")
                'WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>window.open('login.aspx','','height=240,width=320,status=no,toolbar=no,menubar=no,location=no');</script>")
                WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>alert('無讀取權限。');document.location.href='login.aspx?lastpage=" + pid + "&imode=1';</script>")
            Else
                showMsgbox("權限驗證發生錯誤，錯誤訊息：" & strErr, WebPage)
            End If
        ElseIf iMode = 2 Then
            If iResult = 0 Then
                'showMsgbox("write OK", WebPage)
            ElseIf iResult = -1 Then
                WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>alert('未登入');document.location.href='Default.aspx';</script>")
                'WebPage.Response.Redirect("Default.aspx")
            ElseIf iResult = -2 Then
                'showMsgbox("無寫入權限，將返回上一頁。", WebPage)
                'WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>history.back();</script>")
                WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>alert('無寫入權限。');document.location.href='login.aspx?lastpage=" + pid + "&imode=2';</script>")
            Else
                showMsgbox("權限驗證發生錯誤，錯誤訊息：" & strErr, WebPage)
            End If
        End If

        Return iResult
    End Function

    Public Function chkPageAuth(ByVal gid As String, ByVal pid As String, ByVal iMode As Integer, ByVal strConn As String, ByRef strErr As String, ByVal WebPage As UpdatePanel) As Integer
        Dim iResult As Integer = 0

        iResult = chkAuth(gid, pid, iMode, strConn, strErr)
        If iMode = 1 Then
            If iResult = 0 Then
                'showMsgbox("read OK", WebPage)
            ElseIf iResult = -1 Then
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('未登入');document.location.href='Default.aspx';", True)
            ElseIf iResult = -2 Then
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('無讀取權限。');document.location.href='login.aspx?lastpage=" + pid + "&imode=1';", True)
            Else
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('權限驗證發生錯誤，錯誤訊息：" + strErr + "');document.location.href='Default.aspx';", True)
            End If
        ElseIf iMode = 2 Then
            If iResult = 0 Then
                'showMsgbox("write OK", WebPage)
            ElseIf iResult = -1 Then
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('未登入');document.location.href='Default.aspx';", True)
            ElseIf iResult = -2 Then
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('無寫入權限。');document.location.href='login.aspx?lastpage=" + pid + "&imode=2';", True)
            Else
                ScriptManager.RegisterClientScriptBlock(WebPage, WebPage.GetType(), Guid.NewGuid().ToString(), "alert('權限驗證發生錯誤，錯誤訊息：" + strErr + "');document.location.href='Default.aspx';", True)
            End If
        End If

        Return iResult
    End Function


    Public Sub getPageUser(ByVal WebPage As System.Web.UI.Page)
        '檢查 session ,uid

        If WebPage.Session("uid") Is Nothing Then
            If WebPage.Request("uid") Is Nothing Then
                CreateUserSession("everybody", WebPage)
            Else
                'If WebPage.Session("uid") Is Nothing Then
                Dim strEncryptUID As String
                Dim strDecryptUID As String
                Dim arrData() As String

                'WebPage.Session.RemoveAll()

                'strEncryptUID ex:uid=[kent];datetime=[2008/07/23 09:00:00]
                strEncryptUID = WebPage.Request("uid")
                strDecryptUID = Decrypt(strEncryptUID, SYS_PRIVATE_KEY)

                arrData = Split(strDecryptUID, "];[")

                If arrData.Length = 2 Then
                    arrData(0) = Mid(arrData(0), 2)
                    arrData(1) = Left(arrData(1), arrData(1).Length - 1)

                    If DateDiff(DateInterval.Second, Convert.ToDateTime(arrData(1)), Now) < SYS_TR_TIME_OUT Then
                        CreateUserSession(arrData(0), WebPage)
                    End If

                End If
            End If

        End If

    End Sub

    Private Sub CreateUserSession(ByVal strUID As String, ByVal WebPage As System.Web.UI.Page)
        '建立user session
        Dim conn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing
        Dim strErr As String = "", strSQL As String = ""

        Try
            conn = New SqlConnection(getConnStr(WebPage.Application("ConnStr")))
            conn.Open()

            strSQL = "SELECT * FROM sys_member WHERE uid='" & strUID & "'"
            reader = execReader(strSQL, strErr, conn)
            If Not reader Is Nothing Then
                If reader.HasRows Then

                    reader.Read()
                    '建立session
                    WebPage.Session("uid") = reader.GetString(0)
                    WebPage.Session("gid") = reader.GetString(5)
                    WebPage.Session("name") = reader.GetString(2)

                    reader.Close()
                Else
                    WebPage.Session("uid") = ""
                    WebPage.Session("gid") = ""
                    WebPage.Session("name") = ""

                End If
            End If
            conn.Close()

        Catch ex As SqlException
            showMsgbox(ex.Message, WebPage)
        End Try
    End Sub

    Public Sub setTitle(ByVal WebPage As System.Web.UI.Page, ByVal PageID As String)
        '顯示Header Title 與 Document Title
        'Header Image 變換
        Dim conn As SqlConnection = Nothing
        Dim sreader As SqlDataReader = Nothing
        Dim strSQL As String = "", strErr As String = ""
        Dim strTitle As String = PageID

        Dim hPMIS_Image As String = "../images/ipmis-header.jpg"

        Try
            conn = New SqlConnection(getConnStr(WebPage.Application("ConnStr")))
            conn.Open()

            strSQL = "SELECT page_name FROM sys_page WHERE pid = '" + PageID + "' "
            sreader = execReader(strSQL, strErr, conn)

            If Not sreader Is Nothing Then
                If sreader.HasRows Then
                    sreader.Read()
                    strTitle = sreader.Item("page_name")
                End If
            End If
            conn.Close()

            CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("lblShowTitle_header"), Label).Text = strTitle
            CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("lblShowTitle_ID"), Label).Text = PageID

            WebPage.Title = PageID + " " + strTitle

            CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = hPMIS_Image

            'If PageID.StartsWith("1") Then
            '    CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = iPMIS_Image
            'ElseIf PageID.StartsWith("2") Then
            '    'CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = sPMIS_Image
            '    CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = iPMIS_Image
            'ElseIf PageID.StartsWith("m") Then
            '    CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = iPMIS_Image
            'ElseIf PageID.StartsWith("0") Then
            '    CType(CType(WebPage.FindControl("ph"), PageHeader).FindControl("imgHeaderImage"), Image).ImageUrl = iPMIS_Image
            'End If

        Catch ex As Exception
            showMsgbox(ex.Message, WebPage)
        End Try

    End Sub

    Public Structure LimitData
        Dim name As String
        Dim high As String
        Dim low As String
    End Structure

    Enum LimitTable
        bf = 1
        sp = 2
        coke = 3
    End Enum

    Public Function read_limit_table(ByVal eLimitTable As LimitTable, ByVal id As Byte, ByVal connPMIS As SqlConnection, ByVal WebPage As System.Web.UI.Page) As Boolean
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""
        Dim strTable As String = ""
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable

        Try
            Select Case eLimitTable
                Case LimitTable.bf
                    strTable = "sys_bf_limit"
                    strSQL2 = "select * from sys_bf_limit where bfid = " + id.ToString
                Case LimitTable.sp
                    strTable = "sys_sp_limit"
                    strSQL2 = "select * from sys_sp_limit where spid = " + id.ToString
                Case LimitTable.coke
                    strTable = "sys_cok_limit"
                    strSQL2 = "select * from sys_cok_limit where ckid = " + id.ToString
            End Select


            strSQL1 = "select syscolumns.name as name " & _
                        "from syscolumns " & _
                        "left outer join systypes on syscolumns.xtype = systypes.xtype left outer join sysobjects on sysobjects.id = syscolumns.id " & _
                        "where sysobjects.name='" + strTable + "' " & _
                        "order by syscolumns.id "


            dt1 = execQuery(strSQL1, "", connPMIS)

            dt2 = execQuery(strSQL2, "", connPMIS)

            Dim LimitData_Array((dt1.Rows.Count - 1) / 2 - 1) As LimitData

            For i As Integer = 0 To LimitData_Array.Length - 1
                LimitData_Array(i).name = Left(dt1.Rows(i * 2 + 1).Item(0).ToString, dt1.Rows(i * 2 + 1).Item(0).ToString.IndexOf("_")).ToUpper
                LimitData_Array(i).high = dt2.Rows(0).Item(i * 2 + 1).ToString
                LimitData_Array(i).low = dt2.Rows(0).Item(i * 2 + 2).ToString
            Next


            WebPage.Application("LimitData" + Mid(strTable, 5, strTable.LastIndexOf("_") - 4) + id.ToString) = LimitData_Array

            'MsgBox(Application())

            Return True

        Catch ex As Exception
            Return False
        End Try


    End Function


    Public Function rtnlimit_data(ByVal idxName As Byte, ByVal value As String, ByVal limit As LimitTable, ByVal id As Byte, ByVal objLable As Label, ByVal connPMIS As SqlConnection) As String

        If value = "" Then

            Return ""

        End If

        Dim limit_array() As LimitData
        Dim WebPage As System.Web.UI.Page = objLable.Page

        Dim strSystem As String = ""
        Select Case limit
            Case LimitTable.bf
                ReDim limit_array(32)
                strSystem = "bf"

            Case LimitTable.sp
                ReDim limit_array(3)
                strSystem = "sp"

            Case LimitTable.coke
                ReDim limit_array(1)
                strSystem = "cok"

        End Select

        If WebPage.Application("LimitData" + strSystem + id.ToString) Is Nothing Then
            read_limit_table(limit, id, connPMIS, WebPage)
        End If

        'MsgBox(CType(objLable.Page.Application("LimitDatabf1")(0), LimitData).name)

        limit_array = WebPage.Application("LimitData" + strSystem + id.ToString)

        If limit_array(idxName).high <> "" And Val(limit_array(idxName).high) < Val(value) Then
            objLable.ForeColor = Drawing.Color.Red
        ElseIf limit_array(idxName).low <> "" And Val(limit_array(idxName).low) > Val(value) Then
            objLable.ForeColor = Drawing.Color.Red
        End If

        Return value
    End Function

    Public Function GetSQLConnection(ByVal strConnectionInfo As String) As SqlConnection
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(strConnectionInfo)
            conn.Open()
        Catch ex As Exception
            CloseSQLConnection(conn)
        End Try
        Return conn
    End Function

    Public Sub CloseSQLConnection(ByVal conn As SqlConnection)

        Try
            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetDataToStringArray(ByVal strConnectionInfo As String, ByVal g_strSQL As String, ByVal intCount As Integer, Optional ByVal redimArray As Boolean = True, Optional ByVal startIdx As Integer = 0) As String()
        Dim dt As DataTable = Nothing
        Dim strErr As String = ""
        Dim i As Int16 = 0
        Dim g_sConn As System.Data.SqlClient.SqlConnection = Nothing
        Dim tmpDataArray As ArrayList = Nothing
        Dim finalDataArray As String()

        g_sConn = GetSQLConnection(strConnectionInfo)
        If g_sConn IsNot Nothing Then
            dt = execQuery(g_strSQL, strErr, g_sConn)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    tmpDataArray = New ArrayList
                    For Each obj As Object In dt.Rows(0).ItemArray
                        tmpDataArray.Add(obj.ToString)
                    Next
                    If tmpDataArray.Count > intCount Then tmpDataArray = Nothing
                End If
                dt.Dispose()
            End If
        End If
        CloseSQLConnection(g_sConn)

        If tmpDataArray Is Nothing Then
            ReDim finalDataArray(intCount)
        Else
            finalDataArray = tmpDataArray.ToArray(GetType(String))
        End If

        Return finalDataArray
    End Function

End Module
