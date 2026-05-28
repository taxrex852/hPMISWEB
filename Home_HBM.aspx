<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Home_HBM.aspx.vb" Inherits="hPMISWEB.Home_HBM" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Home</title>
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <table style="z-index: 101; left: 198px; position: absolute; top: 157px">
            <tr>
                <td style="vertical-align: top">
                    1、型鋼：</td>
                <td style="vertical-align: top">
                    <asp:LinkButton ID="lbtn3106" runat="server">3106 HBM每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3206" runat="server">3206 HBM每日缺陷統計</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3406" runat="server">3406 HBM生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3506" runat="server">3506 HBM生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px">
                </td>
                <td style="vertical-align: top; width: 89px;">
                    </td>
                <td style="vertical-align: top">
                    </td>
                <td rowspan="5" style="vertical-align: top">
                    </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td style="height: 20px">
                </td>
                <td style="width: 20px; height: 20px">
                </td>
                <td style="height: 20px; width: 89px;">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 97px;">
                    2、CO偵測：</td>
                <td style="vertical-align: top; height: 97px;">
                    <asp:LinkButton ID="lbtn3701" runat="server">3701 W4全場固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3702" runat="server">3702 熱軋加熱爐固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton1" runat="server">3703 型鋼加熱爐固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton2" runat="server">110B 全場固定式CO偵測圖</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 97px;">
                </td>
                <td style="vertical-align: top; width: 89px; height: 97px;">
                    </td>
                <td style="vertical-align: top; height: 97px;">
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 89px;">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 78px;">
                    3、外部網站：</td>
                <td style="vertical-align: top; height: 78px;">
                    <asp:LinkButton ID="lbtnout1" runat="server">中龍生產部門戰情中心</asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lbtnout2" runat="server">W7每日生產量追蹤表</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 78px;">
                </td>
                <td style="vertical-align: top; height: 78px; width: 89px;">
                    </td>
                <td style="vertical-align: top; height: 78px;">
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 89px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                    4、熱軋網站：</td>
                <td style="vertical-align: top; height: 20px">
                    <asp:LinkButton ID="LinkHSM" runat="server">熱軋MES首頁</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 89px; height: 20px">
                    </td>
                <td style="vertical-align: top; height: 20px">
                    &nbsp;<br />
                    </td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 89px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 56px">
                    </td>
                <td style="vertical-align: top; height: 56px">
                    </td>
                <td style="vertical-align: top; width: 20px; height: 56px">
                </td>
                <td style="vertical-align: top; height: 56px; width: 89px;">
                    </td>
                <td style="vertical-align: top; height: 56px">
                    </td>
                <td rowspan="1" style="vertical-align: top">
                </td>
            </tr>
        </table>
    </form>
    
</body>
</html>
