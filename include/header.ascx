<%@ Control Language="vb" AutoEventWireup="false" Codebehind="header.ascx.vb" Inherits="hPMISWEB.PageHeader" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<%--<asp:Timer ID="Timer_header" runat="server" Interval="1000">
</asp:Timer>--%>
<asp:Image ID="imgHeaderImage" runat="server" ImageUrl ="../images/ipmis-header.jpg" />
<link rel="stylesheet" type="text/css" href="/css/diagram.css" media="all"  />
<a href="Home.aspx" style="position: absolute; top: 15px; left: 10px; z-index: 200; height:80px; width:335px; background-color: Transparent;">
</a>

<!--Block offline messages -->
<script type="text/javascript" language="javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args)
    {   
        if (args.get_error() != undefined)
        {   
            args.set_errorHandled(true); 
            $get('ph_lblShowNow_header').style.color = 'red';
            $get('ph_lblShowNow_header').innerHTML = 'OFFLINE';
        }
    }
</script>

<table style="position: absolute; top: 25px; left: 10px; z-index: 150">
<colgroup>
<col width="250" />
<col width="100" />
<col width="355" />
<col width="250" />
</colgroup>
<tr>
<td></td>
<td></td>
<td>
<asp:Menu ID="Menu1" runat="server" BackColor="#9DC8FD" DynamicHorizontalOffset="2"
Font-Names="Verdana" Font-Size="10pt" ForeColor="Blue" Orientation="Horizontal"
StaticSubMenuIndent="10px" Style="vertical-align: middle; text-align: left;" >
<StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
<DynamicMenuStyle BackColor="#9DC8FD" />
<StaticSelectedStyle BackColor="#9DC8FD" />
<DynamicSelectedStyle BackColor="#9DC8FD" />
<DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" BackColor="#9DC8FD" />
</asp:Menu>
</td>
<td align="left" style="width: 250px">
<asp:Panel ID="Panel_gopage" runat="server" DefaultButton="btnGoto_Page" Width="250px">
<asp:TextBox ID="txtPageID" runat="server" ForeColor="Blue" MaxLength="4" Width="64px"></asp:TextBox>
<asp:Button ID="btnGoto_Page" runat="server" Text="捷徑" />
<asp:Button ID="btnHelp" runat="server" Text="Help" Visible="False" /></asp:Panel>
</td>
</tr>
<tr>
<td><br /><br /></td>
<td></td>
<td></td>
<td style="width: 250px"></td>
</tr>
<tr>
<td align="left">
&nbsp;&nbsp;&nbsp;<asp:Label ID="lblShowTitle_ID" runat="server" Font-Bold="True" Font-Size="14pt"
ForeColor="Green"></asp:Label>
</td>
<td align="left" colspan="2">
<asp:Label ID="lblShowTitle_header" runat="server" Font-Bold="True" Font-Size="14pt"
ForeColor="Green"></asp:Label>
</td>
<td style="width: 250px"></td>
</tr>
</table>
<table style="position: absolute; top: 95px; left: 745px;">
<tr>
<td>
<span style="color: #FF8C00; font-weight: bold; font-size: 10pt">資料更新時間：</span></td>
</tr>
</table>
<table style="position: absolute; top: 93px; left: 822px;">
<tr>
<td>
<asp:UpdatePanel ID="UpdatePanel_header" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:Label ID="lblShowNow_header" runat="server" Style="z-index: 10; color: #FF8C00;
text-align: center; font-weight: bold; font-size: 10pt" Width="160"></asp:Label>
<asp:Timer ID="TimerALL" runat="server" Interval="60000">
                </asp:Timer>
</ContentTemplate>
   
</asp:UpdatePanel>
</td>
</tr>
</table>
