Imports System.Security.Cryptography

Module modGeneral
    Public Sub showMsgbox(ByVal strMsg As String, ByVal WebPage As System.Web.UI.Page)
        WebPage.ClientScript.RegisterClientScriptBlock(WebPage.GetType(), Guid.NewGuid().ToString(), "<script language='javascript'>alert('" & strMsg & "');</script>")
    End Sub

    Public Sub showMsgbox(ByVal strMsg As String, ByVal WebPage As System.Web.UI.Page, ByVal UPanel As UpdatePanel)
        ScriptManager.RegisterClientScriptBlock(UPanel, WebPage.GetType(), "click", "alert('" & strMsg.Replace("'", "") & "');", True)
    End Sub

    Public Function getConnStr(ByRef strConnInfo As String) As String
        Dim rootWebConfig As System.Configuration.Configuration = Nothing
        Dim connString As System.Configuration.ConnectionStringSettings = Nothing

        If strConnInfo = "" Then
            rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            If (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count) Then
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings("PMISConnectionString")
                strConnInfo = connString.ConnectionString
            End If
        End If

        getConnStr = strConnInfo
    End Function

    Public Function getHBMConnStr(ByRef strHBMConnInfo As String) As String '1091229 新增HBMPMIS連線字串
        Dim hbmrootWebConfig As System.Configuration.Configuration = Nothing
        Dim hbmconnString As System.Configuration.ConnectionStringSettings = Nothing

        If strHBMConnInfo = "" Then
            hbmrootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            If (0 < hbmrootWebConfig.ConnectionStrings.ConnectionStrings.Count) Then
                hbmconnString = hbmrootWebConfig.ConnectionStrings.ConnectionStrings("HBMPMISConnectionString")
                strHBMConnInfo = hbmconnString.ConnectionString
            End If
        End If

        getHBMConnStr = strHBMConnInfo
    End Function

    Public Sub ScriptFocus(ByVal obj As Object, ByVal WebPage As System.Web.UI.Page)
        Dim sScript As String

        sScript = "setTimeout(""$get('" & obj.ClientID & "').focus(); "", 100);"
        'sScript = "document.getElementById('" + obj.ClientID + "').focus();"
        ScriptManager.RegisterStartupScript(WebPage, WebPage.GetType(), "focus", sScript, True)
    End Sub
    Public Function NullStrToNA(ByVal strData As String) As String
        '空字串，回傳N/A
        If strData <> "" Then
            Return strData
        Else
            Return "N/A"
        End If
    End Function

    '計算字串長度，包含每個中文為2個Byte
    '回傳值為Integer
    Public Function encToByte(ByVal strText As String) As Integer
        Return System.Text.Encoding.Default.GetByteCount(strText)
    End Function

    'MD5 Hash 方法
    '為不可逆的加密方法
    'return 為 加密後的字串值
    Function getMd5Hash(ByVal input As String) As String
        ' Create a new instance of the MD5 object.
        Dim md5Hasher As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function

    'verify Md5 Hash
    '檢查以MD5加密後的字串是否一致
    'return :true 為驗證成功
    '       :false 為驗證失敗
    Function verifyMd5Hash(ByVal input As String, ByVal hash As String) As Boolean
        ' Hash the input.
        Dim hashOfInput As String = getMd5Hash(input)

        ' Create a StringComparer an comare the hashes.
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If

    End Function

    'DES 加密方法
    'sKey 為私鑰必須使用英文字符，區分大小寫，且字符數量必須為8個
    'return 為 加密後的字串值
    Public Function Encrypt(ByVal pToEncrypt As String, ByVal sKey As String) As String
        If pToEncrypt = "" Or sKey.Length <> 8 Then Return ""
        Try
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte

            inputByteArray = Encoding.Default.GetBytes(pToEncrypt)
            '建立加密對象的密鑰和偏移量
            '原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            '使得輸入密碼必須輸入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
            '寫二進制數組到加密流
            '(把內存流中的內容全部寫入)
            Dim ms As New System.IO.MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor, CryptoStreamMode.Write)
            '寫二進制數組到加密流
            '(把內存流中的內容全部寫入)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()

            '建立輸出字符串     
            Dim ret As New StringBuilder()
            Dim b As Byte
            For Each b In ms.ToArray()
                ret.AppendFormat("{0:X2}", b)
            Next

            Return ret.ToString()
        Catch ex As Exception
            '加密過程錯誤
            Return ""
        End Try

    End Function

    'DES 解密方法
    'sKey 為私鑰必須使用英文字符，區分大小寫，且字符數量必須為8個
    'return 為 解密後的字串值
    Public Function Decrypt(ByVal pToDecrypt As String, ByVal sKey As String) As String
        If pToDecrypt = "" Or sKey.Length <> 8 Then Return ""
        Try
            Dim des As New DESCryptoServiceProvider()
            '把字符串放入byte數組
            Dim len As Integer
            len = pToDecrypt.Length / 2 - 1
            Dim inputByteArray(len) As Byte
            Dim x, i As Integer

            For x = 0 To len
                i = Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16)
                inputByteArray(x) = CType(i, Byte)
            Next
            '建立加密對象的密鑰和偏移量，此值重要，不能修改
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
            Dim ms As New System.IO.MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor, CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Encoding.Default.GetString(ms.ToArray)

        Catch ex As Exception
            '解密過程錯誤
            Return ""
        End Try

    End Function

    '14XX & 16XX Utility資料全部顯示為小數一位
    '2008/10/09 homer 問題追蹤彙總表 no.6
    Public Function IsNullData_chkpt(ByVal strData As String) As String
        If IsNumeric(strData) Then
            Return CType(strData, Double).ToString("F1")
        Else
            Return "N/A"
        End If
    End Function

End Module
