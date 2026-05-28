<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Working.aspx.vb" Inherits="hPMISWEB._Working" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
</head>
<body onload="alert('CO訊號預計建立日期：101.12.25');">
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timerworking" runat="server" OnTick="Timerworking_Tick"></asp:Timer>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<%--                <asp:Label ID="lblSqc_t" runat="server" Style="z-index: 125; left: 472px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>--%>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<%--                <asp:Image ID="imgSQC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 464px; position: absolute; top: 600px; z-index: 107;" Width="70px" />--%>
                &nbsp; &nbsp; &nbsp;&nbsp;<%--<asp:Image ID="imgsPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 681px; position: absolute; top: 361px; z-index: 115;" />--%>
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<%--<asp:Label ID="lblSQC" runat="server" Font-Bold="True" Style="left: 472px;
                    position: absolute; top: 760px; z-index: 124;" Text="SQC" ForeColor="Blue"></asp:Label>--%>
                &nbsp; &nbsp; &nbsp;<%--<asp:Label ID="lblsPMIS" runat="server" Font-Bold="True" Style="left: 722px; 
                    position: absolute; top: 489px; z-index: 127;" Text="sPMIS" ForeColor="Blue"></asp:Label>--%>&nbsp;

            </ContentTemplate>
        </asp:UpdatePanel>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; 
        <%--        <img src="images/line_horizontal.jpg" style="left: 536px; width: 144px; position: absolute;
            top: 464px; z-index: 111;" height="3" />--%>
        &nbsp; &nbsp; 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Working.jpg" />
    </form>
</body>
</html>
