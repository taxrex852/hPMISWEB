<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3092.aspx.vb" Inherits="hPMISWEB.HBMsys" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style30 {
            left: 323px;
            width: 254px;
            position: absolute;
            top: 549px;
            z-index: 109;
            height: 6px;
        }
        .auto-style31 {
            left: 281px;
            position: absolute;
            top: 760px;
            z-index: 121;
        }
        .auto-style32 {
            left: 371px;
            position: absolute;
            top: 760px;
            z-index: 122;
        }
        .auto-style33 {
            left: 459px;
            position: absolute;
            top: 760px;
            z-index: 123;
        }
        .auto-style34 {
            z-index: 125;
            left: 541px;
            position: absolute;
            top: 784px;
        }
        .auto-style35 {
            left: 439px;
            position: absolute;
            top: 489px;
            z-index: 126;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                &nbsp;
                <asp:Label ID="lblHBMFCE_t" runat="server" Style="z-index: 125; left: 272px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
                <asp:Label ID="lblHBMMIL_t" runat="server" Style="z-index: 125; left: 360px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblHBMSPC_t" runat="server" Style="z-index: 125; left: 448px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
<%--                <asp:Label ID="lblSqc_t" runat="server" Style="z-index: 125; left: 472px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>--%>
                <asp:Label ID="lblHBMCARAT_t" runat="server" Text="N/A" CssClass="auto-style34"></asp:Label>

                &nbsp;
                <asp:Image ID="imgHBMFCE" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 280px; position: absolute; top: 600px; z-index: 104;" Width="70px" />
                <asp:Image ID="imgHBMMIL" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 368px; position: absolute; top: 600px; z-index: 105;" Width="70px" />   
                <asp:Image ID="imgHBMSPC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 456px; position: absolute; top: 600px; z-index: 106;" Width="70px" />
<%--                <asp:Image ID="imgSQC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 464px; position: absolute; top: 600px; z-index: 107;" Width="70px" />--%>
                <asp:Image ID="imgHBMCARAT" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 544px; position: absolute; top: 600px; z-index: 108;" Width="70px" />
                <asp:Image ID="imgHBMPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 411px; position: absolute; top: 360px; z-index: 114;" />
                <%--<asp:Image ID="imgsPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 681px; position: absolute; top: 361px; z-index: 115;" />--%>
                <asp:Image ID="imgHOST" runat="server" Height="140px" ImageUrl="~/images/pc_host_normal.jpg"
                    Style="left: 409px; position: absolute; top: 157px; z-index: 116;" />         &nbsp;
                <asp:Label ID="lblHBMMIL" runat="server" Font-Bold="True" Text="HBMMIL" ForeColor="Blue" CssClass="auto-style32"></asp:Label>     
                <asp:Label ID="lblHBMSPC" runat="server" Font-Bold="True" Text="HBMSPC" ForeColor="Blue" CssClass="auto-style33"></asp:Label>                 
                <%--<asp:Label ID="lblSQC" runat="server" Font-Bold="True" Style="left: 472px;
                    position: absolute; top: 760px; z-index: 124;" Text="SQC" ForeColor="Blue"></asp:Label>--%>
                <asp:Label ID="lblHBMCARAT" runat="server" Font-Bold="True" Style="left: 544px;
                    position: absolute; top: 760px; z-index: 120;" Text="HBMCARAT" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblHBMFCE" runat="server" Font-Bold="True" Text="HBMFCE"  ForeColor="Blue" CssClass="auto-style31"></asp:Label>   
                <asp:Label ID="lblHPMIS" runat="server" Font-Bold="True" Text="HBMPMIS" ForeColor="Blue" CssClass="auto-style35"></asp:Label>
                <%--<asp:Label ID="lblsPMIS" runat="server" Font-Bold="True" Style="left: 722px; 
                    position: absolute; top: 489px; z-index: 127;" Text="sPMIS" ForeColor="Blue"></asp:Label>--%>                   
                <asp:Label ID="lblHOST" runat="server" Font-Bold="True" Style="left: 453px; 
                    position: absolute; top: 298px; z-index: 129;" Text="HOST" ForeColor="Blue"></asp:Label>    
           

                    
            </ContentTemplate>
        </asp:UpdatePanel>
        <img src="images/status.jpg" style="left: 820px; position: absolute; top: 140px;
            z-index: 114;" height="90" width="90" />
        <table style="left: 760px; position: absolute; top: 160px; z-index: 115;">
            <tr>
                <td>
                    Online:</td>
            </tr>
            <tr>
                <td>
                    <br/>
                </td>
            </tr>
            <tr>
                <td>
                    Offline:</td>
            </tr>
        </table>
        <img src="images/line_vertical.jpg" style="left: 496px; position: absolute; top: 552px;
            height: 50px; z-index: 117;" width="3" />
        &nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;
        <img src="images/line_vertical.jpg" style="left: 320px; position: absolute; top: 552px;
            height: 50px; z-index: 103;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 408px; position: absolute; top: 552px;
            height: 50px; z-index: 104;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 576px; position: absolute; top: 550px;
            height: 50px; z-index: 105;" width="3" />&nbsp;&nbsp;&nbsp;
        <img src="images/line_horizontal.jpg" class="auto-style30" />
        <img src="images/line_vertical.jpg" style="left: 470px; width: 3px; position: absolute;
            top: 315px; height: 41px; z-index: 110;" id="IMG1" />
<%--        <img src="images/line_horizontal.jpg" style="left: 536px; width: 144px; position: absolute;
            top: 464px; z-index: 111;" height="3" />--%>
        &nbsp;<img src="images/line_vertical.jpg" style="left: 473px; position: absolute; top: 512px;
            height: 40px; z-index: 113;" width="3" />&nbsp; &nbsp;&nbsp; &nbsp;
        </form>
</body>
</html>