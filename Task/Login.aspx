<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Y6P2主題工作系統<br />
        <br />
        帳號：<asp:TextBox ID="TB_ID" runat="server"></asp:TextBox>
        <br />
        <br />
        密碼：<asp:TextBox ID="TB_PW" runat="server" TextMode="Password" ></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BT_Login" runat="server" OnClick="BT_Login_Click" Text="登入" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [employee] WHERE (([employee_id] = @employee_id) AND ([employee_password] = @employee_password))">
            <SelectParameters>
                <asp:ControlParameter ControlID="TB_ID" Name="employee_id" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="TB_PW" Name="employee_password" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Label ID="LB_Status" runat="server"></asp:Label>
    </form>
</body>
</html>
