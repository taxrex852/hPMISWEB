<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Home.aspx.vb" Inherits="hPMISWEB.Home" %>
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
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 208px; position: absolute; top: 768px; height: 200px; z-index: 100; display: none; visibility: hidden; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
             <colgroup>
                    <col span="7" width="100" />
                </colgroup>
            <tr>
                <td></td>
                <td align="center" class="homedata" >
                    熱軋</td>
                <td align="center" class="homedata2" >
                    精整#1<span style="color: white; background-color: #507cd1"></span></td>
                <td align="center" class="homedata3" >
                    精整#2</td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="border-collapse: collapse" class="gvhs_data">
                    生產履歷</td>
                <td class="homedata"><asp:Button ID="btnHsm_Produce" runat="server" Text="3101" /></td>
                <td class="homedata2"><asp:Button ID="btn1tnrl_Produce" runat="server" Text="3102" /></td>
                <td class="homedata3"><asp:Button ID="btn2tnrl_Produce" runat="server" Text="3103" /></td>
                <td class="data"><asp:Button ID="btn3tnrl_Produce" runat="server" Text="#3 TNRL" /></td>
                <td class="data"><asp:Button ID="btnSteel_Produce" runat="server" Text="型鋼" /></td>
                <td class="data">
                    <asp:Button ID="btnPol_Produce" runat="server" Text="POL" /></td>
            </tr>
            <tr>
                <td class="gvhs_data">
                    缺陷統計</td>
                <td class="homedata"><asp:Button ID="btnHsm_Defect" runat="server" Text="3201" /></td>
                <td class="homedata2"><asp:Button ID="btn1tnrl_Defect" runat="server" Text="3202" /></td>
                <td class="homedata3"><asp:Button ID="btn2tnrl_Defect" runat="server" Text="3203" /></td>
                <td class="data"><asp:Button ID="btn3tnrl_Defect" runat="server" Text="#3 TNRL" /></td>
                <td class="data"><asp:Button ID="btnSteel_Defect" runat="server" Text="型鋼" /></td>
                <td class="data">
                    <asp:Button ID="btnPol_Defect" runat="server" Text="POL" /></td>
            </tr>
            <tr>
                <td class="gvhs_data">
                    延誤資料</td>
                <td class="homedata"><asp:Button ID="btnHsm_Delay" runat="server" Text="3301" /></td>
                <td class="homedata2"><asp:Button ID="btn1tnrl_Delay" runat="server" Text="3302" /></td>
                <td class="homedata3"><asp:Button ID="btn2tnrl_Delay" runat="server" Text="3303" /></td>
                <td class="data"><asp:Button ID="btn3tnrl_Delay" runat="server" Text="#3 TNRL" /></td>
                <td class="data"><asp:Button ID="btnSteel_Delay" runat="server" Text="型鋼" /></td>
                <td class="data">
                    <asp:Button ID="btnPol_Delay" runat="server" Text="POL" /></td>
            </tr>
            <tr>
                <td class="gvhs_data">
                    生產分析</td>
                <td class="homedata"><asp:Button ID="btnHsm_Production" runat="server" Text="3401" /></td>
                <td class="homedata2"><asp:Button ID="btn1tnrl_Production" runat="server" Text="3402" /></td>
                <td class="homedata3"><asp:Button ID="btn2tnrl_Production" runat="server" Text="3403" /></td>
                <td class="data"><asp:Button ID="btn3tnrl_Production" runat="server" Text="#3 TNRL" /></td>
                <td class="data"><asp:Button ID="btnSteel_Production" runat="server" Text="型鋼" /></td>
                <td class="data">
                    <asp:Button ID="btnPol_Production" runat="server" Text="POL" /></td>
            </tr>
             <tr>
                <td class="gvhs_data">
                    生產現況</td>
                <td class="homedata">
                    <asp:Button ID="btnHsm_Process" runat="server" Text="3501" /></td>
                <td class="homedata2">
                    <asp:Button ID="btn1tnrl_Process" runat="server" Text="3502" /></td>
                <td class="homedata3">
                    <asp:Button ID="btn2tnrl_Process" runat="server" Text="3503" /></td>
                <td class="data">
                    <asp:Button ID="btn3tnrl_Process" runat="server" Text="#3 TNRL" /></td>
                <td class="data">
                    <asp:Button ID="btnSteel_Process" runat="server" Text="型鋼" /></td>
                <td class="data">
                    <asp:Button ID="btnPol_Process" runat="server" Text="POL" /></td>
            </tr>
        </table>
        <table style="z-index: 101; left: 198px; position: absolute; top: 157px">
            <tr>
                <td style="vertical-align: top">
                    1、熱軋：</td>
                <td style="vertical-align: top; width: 296px;">
                    <asp:LinkButton ID="lbtn3101" runat="server">3101 HSM每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3201" runat="server">3201 HSM每日缺陷統計</asp:LinkButton><br />
        <%--            <asp:LinkButton ID="lbtn3301" runat="server">3301 HSM每日延誤資料</asp:LinkButton><br />--%>
                    <asp:LinkButton ID="lbtn3401" runat="server">3401 HSM生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3501" runat="server">3501 HSM生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px">
                </td>
                <td style="vertical-align: top; width: 168px;">
                    6、型鋼：</td>
                <td style="vertical-align: top; width: 273px;">
                    <asp:LinkButton ID="lbtn3106" runat="server">3106 HBM每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3206" runat="server">3206 HBM每日缺陷統計</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3406" runat="server">3406 HBM生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3506" runat="server">3506 HBM生產現況</asp:LinkButton></td>
                <td rowspan="5" style="vertical-align: top">
                    
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
                <td style="height: 20px; width: 296px;">
                </td>
                <td style="width: 20px; height: 20px">
                </td>
                <td style="height: 20px; width: 168px;">
                </td>
                <td style="height: 20px; width: 273px;">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 97px;">
                    2、精整#1：</td>
                <td style="vertical-align: top; height: 97px; width: 296px;">
                    <asp:LinkButton ID="lbtn3102" runat="server">3102 #1TNRL每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3202" runat="server">3202 #1TNRL每日缺陷統計</asp:LinkButton><br />
             <%--       <asp:LinkButton ID="lbtn3302" runat="server">3302 #1TNRL每日延誤資料</asp:LinkButton><br />--%>
                    <asp:LinkButton ID="lbtn3402" runat="server">3402 #1TNRL生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3502" runat="server">3502 #1TNRL生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 97px;">
                </td>
                <td style="vertical-align: top; width: 168px; height: 97px;">
                    7、儲區資訊：</td>
                <td style="vertical-align: top; height: 97px; width: 273px;">
                    <asp:LinkButton ID="lbtn3601" runat="server">3601 扁鋼胚儲區庫存量</asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lbtn3602" runat="server">3602 鋼捲儲區/成品倉庫庫存量</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 296px;">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 168px;">
                </td>
                <td style="vertical-align: top; height: 20px; width: 273px;">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 78px;">
                    3、精整#2：</td>
                <td style="vertical-align: top; height: 78px; width: 296px;">
                    <asp:LinkButton ID="lbtn3103" runat="server">3103 #2TNRL每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3203" runat="server">3203 #2TNRL每日缺陷統計</asp:LinkButton><br />
        <%--            <asp:LinkButton ID="lbtn3303" runat="server">3303 #2TNRL每日延誤資料</asp:LinkButton><br />--%>
                    <asp:LinkButton ID="lbtn3403" runat="server">3403 #2TNRL生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3503" runat="server">3503 #2TNRL生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 78px;">
                </td>
                <td style="vertical-align: top; height: 78px; width: 168px;">
                    8、CO偵測：</td>
                <td style="vertical-align: top; height: 78px; width: 273px;">
                    <asp:LinkButton ID="lbtn3701" runat="server">3701 W4全場固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3702" runat="server">3702 熱軋加熱爐固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3703" runat="server">3703 型鋼加熱爐固定式CO偵測圖</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton2" runat="server">110B 全場固定式CO偵測圖</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 296px;">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 168px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 273px;">
                </td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                    4、精整#3：</td>
                <td style="vertical-align: top; height: 20px; width: 296px;">
                    <asp:LinkButton ID="lbtn3104" runat="server">3104 #3TNRL每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3204" runat="server">3204 #3TNRL每日缺陷統計</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3404" runat="server">3404 #3TNRL生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3504" runat="server">3504 #3TNRL生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 168px; height: 20px">
                    9、外部網站：</td>
                <td style="vertical-align: top; height: 20px; width: 273px;">
                    <asp:LinkButton ID="lbtnout1" runat="server">中龍生產部門戰情中心</asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lbtnout2" runat="server">W7每日生產量追蹤表</asp:LinkButton></td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 296px;">
                </td>
                <td style="vertical-align: top; width: 20px; height: 20px">
                </td>
                <td style="vertical-align: top; width: 168px; height: 20px">
                </td>
                <td style="vertical-align: top; height: 20px; width: 273px;">
                </td>
                <td rowspan="1" style="vertical-align: top; height: 20px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 103px">
                    5、精整#4：</td>
                <td style="vertical-align: top; height: 103px; width: 296px;">
                    <asp:LinkButton ID="lbtn3107" runat="server">3107 #4TNRL每日生產履歷</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3207" runat="server">3207 #4TNRL每日缺陷統計</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3407" runat="server">3407 #4TNRL生產分析</asp:LinkButton><br />
                    <asp:LinkButton ID="lbtn3507" runat="server">3507 #4TNRL生產現況</asp:LinkButton></td>
                <td style="vertical-align: top; width: 20px; height: 103px">
                </td>
                <td style="vertical-align: top; height: 103px; width: 168px;">
                    10、Offline Packing作業：</td>
                <td style="vertical-align: top; height: 103px; width: 273px;">
                    <asp:LinkButton ID="lbtn3105" runat="server">3105 Offline Packing作業每日生產履歷</asp:LinkButton></td>
                <td rowspan="1" style="vertical-align: top; height: 103px;">
                </td>
            </tr>
        </table>
    </form>
    
</body>
</html>
