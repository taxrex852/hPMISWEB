<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="onlinehelp.aspx.vb" Inherits="hPMISWEB.onlinehelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblPageID" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gvOnlinehelp" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
             <RowStyle CssClass="helpdata" />
             <AlternatingRowStyle CssClass="helpdata2" />
             <HeaderStyle CssClass="gvhs" />
             <FooterStyle CssClass="gvfs" />
             <PagerStyle CssClass="gvps" />
             <SelectedRowStyle CssClass="gvsrs" />
             <EditRowStyle CssClass="gvers" />
        </asp:GridView>
    </form>
</body>
</html>
