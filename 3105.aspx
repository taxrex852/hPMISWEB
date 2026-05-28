<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3105.aspx.vb" Inherits="hPMISWEB.Offline_packing_Produce" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register Assembly="TeeChart" Namespace="Steema.TeeChart.Web" TagPrefix="tchart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Offline_packing_Produce</title>
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
            <a name="#Home"></a>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 150px;">
                <tr>
                    <td>
                        <strong>Offline Packing每日生產履歷</strong></td>
                </tr>
                <tr>
                    <td>
                        每日0700/1500/2300進行資料更新，並於每日1500將資料重新編排
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 185px;">
                <tr>
                    <td>
                        <asp:GridView ID="gvDaily" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                            <RowStyle CssClass="gvrs" />
                            <HeaderStyle CssClass="gvhs" />
                            <FooterStyle CssClass="gvfs" />
                            <PagerStyle CssClass="gvps" />
                            <SelectedRowStyle CssClass="gvsrs" />
                            <EditRowStyle CssClass="gvers" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
             <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 320px;">
                <tr>
                    <td>
                        <strong>Offline Packing當月生產履歷</strong></td>
                </tr>
                <tr>
                    <td style="height: 21px">
                        資料於每日2300進行更新，並於當月第一天2300將資料重新編排
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 104px; position: absolute; top: 376px; width: 848px;" id="TABLE1" onclick="return TABLE1_onclick()">
                <tr>
                    <td style="width: 380px; height: 221px; background-image: url(images/OFFLINEPACKING.JPG); background-repeat: no-repeat;">
                        <asp:Panel ID="Panel1" runat="server" BackColor="gainsboro" Height="200px" Width="342px"
                            ScrollBars="Vertical">
                            <asp:GridView ID="gvMonth" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <RowStyle CssClass="gvrs" />
                                <HeaderStyle CssClass="gvhs" />
                                <FooterStyle CssClass="gvfs" />
                                <PagerStyle CssClass="gvps" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <EditRowStyle CssClass="gvers" />
                            </asp:GridView>
                        </asp:Panel>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 380px">
                        <br />
                      <br />
                        資料區間<asp:Label ID="LabelStartdate" runat="server"></asp:Label>
                        ~<asp:Label ID="LabelEnddate" runat="server"></asp:Label>
                    
                    </td>
                </tr>
                <tr>
                    <td id="teech" class="teech" style="width: 380px">
                        <a name="#Chart"></a>
                  
                    <tchart:WebChart ID="WebChart1" runat="server" AutoPostback="False" GetChartFile="GetChart.aspx" Height="400px" LastFileName="" TempChart="Session" UseLock="False" Width="900px" Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnQrAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QRLkxlZ2VuZC5UZXh0U3R5bGUVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGC5BeGVzLkxlZnQuVGl0bGUuQ2FwdGlvbhYuQXhlcy5MZWZ0LlRpdGxlLkxpbmVzGS5BeGVzLlJpZ2h0LlRpdGxlLkNhcHRpb24XLkF4ZXMuUmlnaHQuVGl0bGUuTGluZXMjLkF4ZXMuQm90dG9tLkxhYmVscy5Sb3VuZEZpcnN0TGFiZWwiLkF4ZXMuQm90dG9tLkxhYmVscy5EYXRlVGltZUZvcm1hdB8uQXhlcy5Cb3R0b20uTGFiZWxzLlZhbHVlRm9ybWF0Fi5BeGVzLkJvdHRvbS5JbmNyZW1lbnQaLkF4ZXMuQm90dG9tLlRpdGxlLkNhcHRpb24YLkF4ZXMuQm90dG9tLlRpdGxlLkxpbmVzDy5BeGVzLkF1dG9tYXRpYwAEAAQAAAYAAAEEAAAEBAcAAQAEBwABAAQBAAAAAAQEAQYBBgABAQABBgABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAASBTdGVlbWEuVGVlQ2hhcnQuTGVnZW5kVGV4dFN0eWxlcwIAAAAICAgBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAAQYBAgAAAAAF/P///xlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAAF+////yBTdGVlbWEuVGVlQ2hhcnQuTGVnZW5kVGV4dFN0eWxlcwEAAAAHdmFsdWVfXwAIAgAAAAYAAAAHAAAAAgAAAAkGAAAAAAAAAAAGBwAAABtTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLkxpbmUF+P///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABff///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH2////+P///woAAJn/AAAAAAAAAgAJCwAAAAUAAAAGDAAAAAxwcm9jZXNzX2RhdGUBBfP///8lU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgEAAAAHdmFsdWVfXwAIAgAAAAEAAAAJDgAAAAUAAAAGDwAAAAJQQQAB8P////j///8KAAD//wAAAAAAAAIABhEAAAAG55Si6YePAAAAAAAAAAAAAAAUQAAAAAAAACBABe7///8kU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAXt////FVN5c3RlbS5EcmF3aW5nLlBvaW50RgIAAAABeAF5AAALCwMAAAAAAAAAAAAAAAYUAAAABumHjemHjwkVAAAABhYAAAAJ55m+5YiG5q+UCRcAAAAABhgAAAADTU1NBhkAAAADIyMjAAAAAAAAPkAGGgAAAAbmmYLplpMJGwAAAAERBgAAAAEAAAAGHAAAACxUTlJMIE9mZmxpbmUgUGFja2luZ+S9nOalreavj+aXpeeUn+eUouWxpeattw8LAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADw4AAAAFAAAABpqZmZmZK49APQrXozAawkApXI/CFcHIQAAAAAAgvMhAcT0K16PFkEARFQAAAAEAAAAGHQAAAAbph43ph48RFwAAAAEAAAAGHgAAAAnnmb7liIbmr5QRGwAAAAEAAAAGHwAAAAbmmYLplpML" DataSourceID="SqlDataSource1" />
                    </td>
                </tr>
                <tr>
                    <td>
                     <%--   <asp:Button ID="btnUp" runat="server" OnClick="btnUp_Click" Text="前一月" />
                        <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="後一月" />--%>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="select dateadd(m, datediff(m,0,process_date),0) as process_date  ,cast(round(SUM(Weight)/1000,2) as float) as PA 
from
h_pmis_ys01
where  process_date between DATEADD(year,-1,getdate()) and getdate() group by   dateadd(m, datediff(m,0,process_date),0)
order by process_date"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 104px; position: absolute; top: 360px;">
                <colgroup>
                    <col width="200" />
                    <col span="5" width="120" />
                </colgroup>
                <tr>
                    <td class="gvhs_data" style="height: 18px">
                    </td>
                    <td class="gvhs_data" style="text-align: center; height: 18px; width: 119px;">
                        產量/MT
                    </td>
                   <%-- <td class="gvhs_data" style="text-align: center">
                        產率/%
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        訂單合格率/%
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        作業率/%
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        剔退重量/MT
                    </td>--%>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 104px; position: absolute; top: 580px;">
                <colgroup>
                    <col width="200" />
                    <col span="5" width="120" />
                </colgroup>
                <tr>
                    <td class="data" style="height: 23px; width: 200px;">
                        <asp:Label ID="lblMonth" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>月統計
                    </td>
                    <td class="data" style="height: 23px">
                        <asp:Label ID="lblPA" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                </tr>
            </table>

    </form>
    
</body>
</html>
