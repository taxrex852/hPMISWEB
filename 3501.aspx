<%@ Page Language="vb" AutoEventWireup="false" Codebehind="3501.aspx.vb" Inherits="hPMISWEB.HSM_Process" %>

<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <title>HSM_Process</title>
</head>

<script language="JavaScript" type="text/javascript"></script>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <img src="images\HSM_Process.JPG" style="left: 168px; position: absolute; top: 408px" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server">
                </asp:Timer>
                <table style="border-collapse: collapse; z-index: 101; left: 88px; position: absolute;
                    top: 248px;">
                    <tr>
                        <td class="Lgvhs_data" colspan="2">
                            目前累計產量</td>
                        <td colspan="1" style="width: 40px">
                        </td>
                        <td class="Lgvhs_data" colspan="2">
                            最後完軋鋼捲：<asp:Label ID="lblLast" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="Lgvhs_data" style="width: 80px">
                            班產量</td>
                        <td class="Ldata" style="width: 170px">
                            <asp:Label ID="lblShift" runat="server" ForeColor="Black" Text="N/A"></asp:Label>
                            Ton</td>
                        <td style="width: 40px">
                        </td>
                        <td class="Lgvhs_data" style="width: 80px">
                            厚度</td>
                        <td class="Ldata" style="width: 250px; text-align: left;">
                            <asp:Label ID="lblT" runat="server" ForeColor="Blue" Text="N/A"></asp:Label>
                            mm</td>
                    </tr>
                    <tr>
                        <td class="Lgvhs_data" style="width: 80px">
                            日產量</td>
                        <td class="Ldata" style="width: 170px">
                            <asp:Label ID="lblDay" runat="server" ForeColor="Black" Text="N/A" CssClass="pmisdata"></asp:Label>
                            Ton</td>
                        <td style="width: 40px">
                        </td>
                        <td class="Lgvhs_data" style="width: 80px">
                            寬度</td>
                        <td class="Ldata" style="width: 250px; text-align: left;">
                            <asp:Label ID="lblW" runat="server" ForeColor="Blue" Text="N/A"></asp:Label>
                            mm</td>
                    </tr>
                    <tr>
                        <td class="Lgvhs_data" style="width: 80px">
                            月產量</td>
                        <td class="Ldata" style="width: 170px">
                            <asp:Label ID="lblMonth" runat="server" ForeColor="Black" Text="N/A" CssClass="pmisdata"></asp:Label>
                            Ton</td>
                        <td style="width: 40px">
                        </td>
                        <td class="Lgvhs_data" style="width: 80px">
                            鋼種</td>
                        <td class="Ldata" style="width: 250px; text-align: left;">
                            <asp:Label ID="lblKind" runat="server" ForeColor="Blue" Text="N/A"></asp:Label></td>
                    </tr>
                </table>
                <table style="border-collapse: collapse; left: 88px; position: absolute; top: 168px; width: 365px; height: 39px;">
                    <tr>
                        <td class="Lgvhs_data">
                            資料日期：</td>
                        <td class="Ldata">
                            <asp:Label ID="lblData" runat="server" ForeColor="Blue" Text="N/A"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="Lgvhs_data">
                            更新時間：</td>
                        <td class="Ldata">
                            <asp:Label ID="lblNow" runat="server" ForeColor="Blue" Text="N/A"></asp:Label></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblStatus" runat="server" Font-Names="標楷體" Font-Size="20pt" ForeColor="Blue"
            Style="left: 664px; position: absolute; top: 168px" Text="目前正在生產中！"></asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Names="標楷體" Font-Size="20pt" ForeColor="Black"
            Style="left: 528px; position: absolute; top: 168px" Text="產線狀態："></asp:Label>
    </form>
    
    
</body>
</html>
