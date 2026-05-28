<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3091.aspx.vb" Inherits="hPMISWEB._3091" %>
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
        .auto-style1 {
            left: 712px;
            position: absolute;
            top: 899px;
            z-index: 110;
        }
        .auto-style4 {
            left: 829px;
            position: absolute;
            top: 850px;
            height: 50px;
            z-index: 119;
        }
        .auto-style11 {
            z-index: 125;
            left: 794px;
            position: absolute;
            top: 1104px;
            right: 422px;
        }
        .auto-style14 {
            left: 748px;
            position: absolute;
            top: 847px;
            height: 50px;
            z-index: 119;
        }
        .auto-style15 {
            z-index: 122;
            left: 714px;
            position: absolute;
            top: 1060px;
        }
        .auto-style16 {
            z-index: 124;
            left: 872px;
            position: absolute;
            top: 1059px;
        }
        .auto-style21 {
            left: 750px;
            width: 252px;
            position: absolute;
            top: 848px;
            z-index: 109;
            height: 3px;
        }
        .auto-style25 {
            z-index: 122;
            left: 787px;
            position: absolute;
            top: 1060px;
        }
        .auto-style26 {
            left: 791px;
            position: absolute;
            top: 900px;
            z-index: 110;
        }
        .auto-style27 {
            left: 872px;
            position: absolute;
            top: 900px;
            z-index: 112;
        }
        .auto-style29 {
            z-index: 125;
            left: 714px;
            position: absolute;
            top: 1085px;
            right: 500px;
        }
        .auto-style30 {
            left: 908px;
            position: absolute;
            top: 849px;
            height: 50px;
            z-index: 119;
        }
        .auto-style31 {
            z-index: 125;
            left: 871px;
            position: absolute;
            top: 1086px;
            right: 345px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                &nbsp;
                <asp:Label ID="lblCymc_t" runat="server" Style="z-index: 125; left: 8px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblFce_t" runat="server" Style="z-index: 125; left: 88px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
                <asp:Label ID="lblHrfspc_t" runat="server" Style="z-index: 125; left: 184px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblTg_t" runat="server" Style="z-index: 125; left: 272px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
                <asp:Label ID="lblMil_t" runat="server" Style="z-index: 125; left: 360px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblSPC_t" runat="server" Style="z-index: 125; left: 448px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
<%--                <asp:Label ID="lblSqc_t" runat="server" Style="z-index: 125; left: 472px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>--%>
                <asp:Label ID="lblSymc_t" runat="server" Style="z-index: 125; left: 536px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblHrs_t" runat="server" Style="z-index: 125; left: 632px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
                <asp:Label ID="lblTnrl1_t" runat="server" Style="z-index: 125; left: 712px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
                <asp:Label ID="lblTnrl2_t" runat="server" Style="z-index: 125; left: 800px; position: absolute;
                    top: 808px" Text="N/A"></asp:Label>
                <asp:Label ID="lblPymc_t" runat="server" Style="z-index: 125; left: 888px; position: absolute;
                    top: 784px" Text="N/A"></asp:Label>
           
                <asp:Label ID="lblIpmis_t" runat="server" Style="z-index: 125; left: 144px; position: absolute;
                    top: 512px" Text="N/A"></asp:Label>

              
                <asp:Image ID="imgFCE" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 104px; position: absolute; top: 600px; z-index: 102;" Width="70px" />
                <asp:Image ID="imgCYMC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg" Style="left: 16px; position: absolute; top: 600px; z-index: 101;" Width="70px" />
                <asp:Image ID="imgHRFSPC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 192px; position: absolute; top: 600px; z-index: 103;" Width="70px" />
                <asp:Image ID="imgTG" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 280px; position: absolute; top: 600px; z-index: 104;" Width="70px" />
                <asp:Image ID="imgMIL" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 368px; position: absolute; top: 600px; z-index: 105;" Width="70px" />   
                <asp:Image ID="imgSPC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 456px; position: absolute; top: 600px; z-index: 106;" Width="70px" />
<%--                <asp:Image ID="imgSQC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 464px; position: absolute; top: 600px; z-index: 107;" Width="70px" />--%>
                <asp:Image ID="imgSYMC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 544px; position: absolute; top: 600px; z-index: 108;" Width="70px" />
                <asp:Image ID="imgHRS" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 632px; position: absolute; top: 600px; z-index: 109;" Width="70px" />
                <asp:Image ID="imgTNRL1" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 720px; position: absolute; top: 600px; z-index: 110;" Width="70px" />   
                <asp:Image ID="imgTNRL2" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 808px; position: absolute; top: 600px; z-index: 111;" Width="70px" />
                <asp:Image ID="imgPYMC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg"
                    Style="left: 896px; position: absolute; top: 600px; z-index: 112;" Width="70px" />   
                <asp:Image ID="imgDYMC" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg" Width="70px" CssClass="auto-style27" />
                <asp:Image ID="imgiPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 141px; position: absolute; top: 361px; z-index: 113;" />    
                <asp:Image ID="imghPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 411px; position: absolute; top: 360px; z-index: 114;" />
                <%--<asp:Image ID="imgsPMIS" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 681px; position: absolute; top: 361px; z-index: 115;" />--%>
                <asp:Image ID="imgHOST" runat="server" Height="140px" ImageUrl="~/images/pc_host_normal.jpg"
                    Style="left: 409px; position: absolute; top: 157px; z-index: 116;" />         &nbsp;
                <asp:Label ID="lblCYMC" runat="server" Font-Bold="True" Style="left: 16px;
                    position: absolute; top: 760px; z-index: 118;" Text="CYMC" ForeColor="Blue"></asp:Label>    
                <asp:Label ID="lblFCE" runat="server" Font-Bold="True" Style="left: 112px;
                    position: absolute; top: 760px; z-index: 119;" Text="FCE" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblHRFSPC" runat="server" Font-Bold="True" Style="left: 192px;
                    position: absolute; top: 760px; z-index: 120;" Text="HRFSPC" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblHRS" runat="server" Font-Bold="True" Style="left: 640px; 
                    position: absolute; top: 760px; z-index: 121;" Text="HRS"  ForeColor="Blue"></asp:Label>   
                <asp:Label ID="lblMIL" runat="server" Font-Bold="True" Style="left: 376px;
                    position: absolute; top: 760px; z-index: 122;" Text="MIL" ForeColor="Blue"></asp:Label>     
                <asp:Label ID="lblSPC" runat="server" Font-Bold="True" Style="left: 464px;
                    position: absolute; top: 760px; z-index: 123;" Text="SPC" ForeColor="Blue"></asp:Label>                 
                <%--<asp:Label ID="lblSQC" runat="server" Font-Bold="True" Style="left: 472px;
                    position: absolute; top: 760px; z-index: 124;" Text="SQC" ForeColor="Blue"></asp:Label>--%>
                <asp:Label ID="lblSYMC" runat="server" Font-Bold="True" Style="left: 544px;
                    position: absolute; top: 760px; z-index: 120;" Text="SYMC" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblTG" runat="server" Font-Bold="True" Style="left: 288px; 
                    position: absolute; top: 760px; z-index: 121;" Text="TG"  ForeColor="Blue"></asp:Label>   
                <asp:Label ID="lblTNRL1" runat="server" Font-Bold="True" Style="left: 720px;
                    position: absolute; top: 760px; z-index: 122;" Text="TNRL1" ForeColor="Blue"></asp:Label>     
                <asp:Label ID="lblTNRL2" runat="server" Font-Bold="True" Style="left: 808px;
                    position: absolute; top: 760px; z-index: 123;" Text="TNRL2" ForeColor="Blue"></asp:Label>                 
                <asp:Label ID="lblPYMC" runat="server" Font-Bold="True" Style="left: 896px;
                    position: absolute; top: 760px; z-index: 124;" Text="PYMC" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblDYMC" runat="server" Font-Bold="True" ForeColor="Blue" Text="DYMC" CssClass="auto-style16"></asp:Label>
                <asp:Label ID="lblHPMIS" runat="server" Font-Bold="True" Style="left: 454px;
                    position: absolute; top: 489px; z-index: 126;" Text="hPMIS" ForeColor="Blue"></asp:Label>
                <%--<asp:Label ID="lblsPMIS" runat="server" Font-Bold="True" Style="left: 722px; 
                    position: absolute; top: 489px; z-index: 127;" Text="sPMIS" ForeColor="Blue"></asp:Label>--%>                   
                <asp:Label ID="lblIPMIS" runat="server" Font-Bold="True" Style="left: 176px;
                    position: absolute; top: 488px; z-index: 128;" Text="iPMIS" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblHOST" runat="server" Font-Bold="True" Style="left: 453px; 
                    position: absolute; top: 298px; z-index: 129;" Text="HOST" ForeColor="Blue"></asp:Label>    
                <asp:Image ID="imgTNRL3" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg" Width="70px" CssClass="auto-style26" />  
              <asp:Label ID="lblTNRL3" runat="server" Font-Bold="True" ForeColor="Blue" Text="TNRL3" CssClass="auto-style25"></asp:Label>
                <asp:Label ID="lblTNRL3_t" runat="server" Text="N/A" CssClass="auto-style11"></asp:Label>
                <asp:Label ID="lblDymc_t" runat="server" Text="N/A" CssClass="auto-style31"></asp:Label>
                 <asp:Image ID="imgTNRL4" runat="server" BackColor="White" ImageUrl="~/images/pc_svr_normal.jpg" Width="70px" CssClass="auto-style1" />
                <asp:Label ID="lblTNRL4" runat="server" Font-Bold="True" ForeColor="Blue" Text="TNRL4" CssClass="auto-style15"></asp:Label>
                <asp:Label ID="lblTNRL4_t" runat="server" Text="N/A" CssClass="auto-style29"></asp:Label>

                <asp:Image ID="imgUDC" runat="server" Height="125px" ImageUrl="~/images/pc_pmis_normal.jpg"
                    Style="left: 680px; position: absolute; top: 360px; z-index: 113;" />
                <asp:Label ID="lblUDC" runat="server" Font-Bold="True" ForeColor="Blue" Style="z-index: 128;
                    left: 720px; position: absolute; top: 488px" Text="UDC"></asp:Label>
                <asp:Label ID="lblUDC_t" runat="server" Style="z-index: 125; left: 696px; position: absolute;
                    top: 512px" Text="N/A"></asp:Label>

                    
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
        &nbsp;
        <img src="images/line_vertical.jpg" style="left: 760px; position: absolute; top: 552px;
            height: 50px; z-index: 119;" width="3" />
        &nbsp;
        <img src="images/line_vertical.jpg" style="left: 51px; position: absolute; top: 550px;
            height: 50px; z-index: 100;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 144px; position: absolute; top: 552px;
            height: 50px; z-index: 101;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 232px; position: absolute; top: 552px;
            height: 50px; z-index: 116;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 320px; position: absolute; top: 552px;
            height: 50px; z-index: 103;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 408px; position: absolute; top: 552px;
            height: 50px; z-index: 104;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 576px; position: absolute; top: 550px;
            height: 50px; z-index: 105;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 672px; position: absolute; top: 552px;
            height: 50px; z-index: 106;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 848px; position: absolute; top: 552px;
            height: 50px; z-index: 107;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 936px; position: absolute; top: 552px;
            height: 50px; z-index: 108;" width="3" />
        <img src="images/line_horizontal.jpg" style="left: 53px; width: 950px; position: absolute;
            top: 549px; z-index: 109; height: 3px;" />
        <img src="images/line_vertical.jpg" style="left: 470px; width: 3px; position: absolute;
            top: 315px; height: 41px; z-index: 110;" id="IMG1" />
<%--        <img src="images/line_horizontal.jpg" style="left: 536px; width: 144px; position: absolute;
            top: 464px; z-index: 111;" height="3" />--%>
        <img src="images/line_horizontal.jpg" style="left: 265px; width: 144px; position: absolute;
            top: 464px; z-index: 112;" height="3" />
        <img src="images/line_vertical.jpg" style="left: 473px; position: absolute; top: 512px;
            height: 40px; z-index: 113;" width="3" />
        <img src="images/line_vertical.jpg" style="left: 1000px; position: absolute; top: 552px;
            height: 300px; z-index: 108;" width="3" />
        <img src="images/line_horizontal.jpg" class="auto-style21" />&nbsp;
        <img src="images/line_vertical.jpg" width="3" class="auto-style4" />
           <img src="images/line_vertical.jpg" width="3" class="auto-style30" />
        <img src="images/line_vertical.jpg" width="3" class="auto-style14" />&nbsp;
    </form>
</body>
</html>
