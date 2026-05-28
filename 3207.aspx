<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3207.aspx.vb" Inherits="hPMISWEB._4TNRL_Defect" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register Assembly="TeeChart" Namespace="Steema.TeeChart.Web" TagPrefix="tchart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>4TNRL_Defect</title>
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
</head>
<script language="JavaScript" type="text/javascript"></script>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
            <a name="#Home"></a>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 30px; position: absolute; top: 150px;">
                <tr>
                    <td>
                        <strong>精整#4每日缺陷統計</strong></td>
                </tr>
                <tr>
                    <td>
                        每日0700/1500/2300進行資料更新，並於每日1500將資料重新編排
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 31px; position: absolute; top: 208px; width:60px">
                <tr>
                    <td class="gvhs_data" height="91px" style="text-align: center">
                        剔除</td>
                </tr>
                <tr>
                    <td class="gvhs_data" height="54px" style="text-align: center">
                        切除</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 90px; position: absolute; top: 169px;">
                <caption align="right">&nbsp;</caption>
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
                left: 30px; position: absolute; top: 370px;">
                <tr>
                    <td>
                        <strong>精整#4當月缺陷統計</strong></td>
                </tr>
                <tr>
                    <td>
                        資料於每日2300進行更新，並於當月第一天2300將資料進行重整
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 30px; position: absolute; top: 445px;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BackColor="gainsboro" Height="200px" ScrollBars="Vertical">
                            <asp:GridView ID="gvMonth" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <RowStyle CssClass="gvrs" />
                                <HeaderStyle CssClass="gvhs" />
                                <FooterStyle CssClass="gvfs" />
                                <PagerStyle CssClass="gvps" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <EditRowStyle CssClass="gvers" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                      
                         資料區間<asp:Label ID="LabelStartdate" runat="server"></asp:Label>
                        ~<asp:Label ID="LabelEnddate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    <table>
                    <tr>
                    <td id="teech" class="teech">
                    <a name="#Chart"></a>
                      <tchart:WebChart ID="WebChart1" runat="server" UseLock="False" TempChart="Session"  Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnTqAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoFC5TZXJpZXMuMC5EYXRhU291cmNlDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UVLlNlcmllcy4wLk1hcmtzLlN0eWxlIS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLk1hcmdpbiguU2VyaWVzLjAuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVySGVpZ2h0Jy5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJXaWR0aCAuU2VyaWVzLjAuTWFya3MuVGFpbFBhcmFtcy5BbGlnbikuU2VyaWVzLjAuTWFya3MuVGFpbFBhcmFtcy5DdXN0b21Qb2ludFBvcwhTZXJpZXMuMRUuU2VyaWVzLjEuQnJ1c2guQ29sb3IZLlNlcmllcy4xLlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjEuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuMS5Qb2ludGVyLlNpemVVbml0cxcuU2VyaWVzLjEuTGluZVBlbi5Db2xvchcuU2VyaWVzLjEuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjEuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjEuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuMS5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuMS5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuMS5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuMS5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuMS5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy4xLkNvbG9yRWFjaBQuU2VyaWVzLjEuRGF0YVNvdXJjZQ8uU2VyaWVzLjEuQ29sb3IPLlNlcmllcy4xLlRpdGxlHS5TZXJpZXMuMS5Vc2VFeHRlbmRlZE51bVJhbmdlIS5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLk1hcmdpbiguU2VyaWVzLjEuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVySGVpZ2h0Jy5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJXaWR0aCAuU2VyaWVzLjEuTWFya3MuVGFpbFBhcmFtcy5BbGlnbikuU2VyaWVzLjEuTWFya3MuVGFpbFBhcmFtcy5DdXN0b21Qb2ludFBvcwhTZXJpZXMuMhUuU2VyaWVzLjIuQnJ1c2guQ29sb3IZLlNlcmllcy4yLlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjIuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuMi5Qb2ludGVyLlNpemVVbml0cx0uU2VyaWVzLjIuUG9pbnRlci5CcnVzaC5Db2xvchcuU2VyaWVzLjIuTGluZVBlbi5Db2xvchcuU2VyaWVzLjIuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjIuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjIuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuMi5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuMi5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuMi5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuMi5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuMi5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy4yLkNvbG9yRWFjaBQuU2VyaWVzLjIuRGF0YVNvdXJjZQ8uU2VyaWVzLjIuQ29sb3IPLlNlcmllcy4yLlRpdGxlHS5TZXJpZXMuMi5Vc2VFeHRlbmRlZE51bVJhbmdlIS5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLk1hcmdpbiguU2VyaWVzLjIuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVySGVpZ2h0Jy5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJXaWR0aCAuU2VyaWVzLjIuTWFya3MuVGFpbFBhcmFtcy5BbGlnbikuU2VyaWVzLjIuTWFya3MuVGFpbFBhcmFtcy5DdXN0b21Qb2ludFBvcwhTZXJpZXMuMxUuU2VyaWVzLjMuQnJ1c2guQ29sb3IZLlNlcmllcy4zLlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjMuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuMy5Qb2ludGVyLlNpemVVbml0cxcuU2VyaWVzLjMuTGluZVBlbi5Db2xvchcuU2VyaWVzLjMuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjMuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjMuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuMy5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuMy5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuMy5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuMy5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuMy5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy4zLkNvbG9yRWFjaBQuU2VyaWVzLjMuRGF0YVNvdXJjZQ8uU2VyaWVzLjMuQ29sb3IPLlNlcmllcy4zLlRpdGxlHS5TZXJpZXMuMy5Vc2VFeHRlbmRlZE51bVJhbmdlIS5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLk1hcmdpbiguU2VyaWVzLjMuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVySGVpZ2h0Jy5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJXaWR0aCAuU2VyaWVzLjMuTWFya3MuVGFpbFBhcmFtcy5BbGlnbikuU2VyaWVzLjMuTWFya3MuVGFpbFBhcmFtcy5DdXN0b21Qb2ludFBvcwhTZXJpZXMuNBUuU2VyaWVzLjQuQnJ1c2guQ29sb3IZLlNlcmllcy40LlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjQuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuNC5Qb2ludGVyLlNpemVVbml0cx0uU2VyaWVzLjQuUG9pbnRlci5CcnVzaC5Db2xvchcuU2VyaWVzLjQuTGluZVBlbi5Db2xvchcuU2VyaWVzLjQuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjQuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjQuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuNC5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuNC5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuNC5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuNC5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuNC5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy40LkNvbG9yRWFjaBQuU2VyaWVzLjQuRGF0YVNvdXJjZQ8uU2VyaWVzLjQuQ29sb3IPLlNlcmllcy40LlRpdGxlHS5TZXJpZXMuNC5Vc2VFeHRlbmRlZE51bVJhbmdlIS5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLk1hcmdpbiguU2VyaWVzLjQuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVySGVpZ2h0Jy5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJXaWR0aCAuU2VyaWVzLjQuTWFya3MuVGFpbFBhcmFtcy5BbGlnbikuU2VyaWVzLjQuTWFya3MuVGFpbFBhcmFtcy5DdXN0b21Qb2ludFBvcwhTZXJpZXMuNRUuU2VyaWVzLjUuQnJ1c2guQ29sb3IZLlNlcmllcy41LlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjUuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuNS5Qb2ludGVyLlNpemVVbml0cx0uU2VyaWVzLjUuUG9pbnRlci5CcnVzaC5Db2xvchcuU2VyaWVzLjUuTGluZVBlbi5Db2xvchcuU2VyaWVzLjUuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjUuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjUuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuNS5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuNS5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuNS5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuNS5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuNS5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy41LkNvbG9yRWFjaBQuU2VyaWVzLjUuRGF0YVNvdXJjZQ8uU2VyaWVzLjUuQ29sb3IPLlNlcmllcy41LlRpdGxlHS5TZXJpZXMuNS5Vc2VFeHRlbmRlZE51bVJhbmdlFS5TZXJpZXMuNS5NYXJrcy5TdHlsZSEuU2VyaWVzLjUuTWFya3MuVGFpbFBhcmFtcy5NYXJnaW4oLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlckhlaWdodCcuU2VyaWVzLjUuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVyV2lkdGggLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuQWxpZ24pLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuQ3VzdG9tUG9pbnRQb3MgLlNlcmllcy41Lk1hcmtzLkZvbnQuQnJ1c2guQ29sb3IZLlNlcmllcy41Lk1hcmtzLlBlbi5Db2xvcghTZXJpZXMuNhUuU2VyaWVzLjYuQnJ1c2guQ29sb3IZLlNlcmllcy42LlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjYuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuNi5Qb2ludGVyLlNpemVVbml0cx0uU2VyaWVzLjYuUG9pbnRlci5CcnVzaC5Db2xvchcuU2VyaWVzLjYuTGluZVBlbi5Db2xvchcuU2VyaWVzLjYuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjYuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjYuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuNi5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuNi5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuNi5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuNi5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuNi5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy42LkNvbG9yRWFjaBQuU2VyaWVzLjYuRGF0YVNvdXJjZQ8uU2VyaWVzLjYuQ29sb3IPLlNlcmllcy42LlRpdGxlHS5TZXJpZXMuNi5Vc2VFeHRlbmRlZE51bVJhbmdlFS5TZXJpZXMuNi5NYXJrcy5TdHlsZSEuU2VyaWVzLjYuTWFya3MuVGFpbFBhcmFtcy5NYXJnaW4oLlNlcmllcy42Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlckhlaWdodCcuU2VyaWVzLjYuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVyV2lkdGggLlNlcmllcy42Lk1hcmtzLlRhaWxQYXJhbXMuQWxpZ24pLlNlcmllcy42Lk1hcmtzLlRhaWxQYXJhbXMuQ3VzdG9tUG9pbnRQb3MgLlNlcmllcy42Lk1hcmtzLkZvbnQuQnJ1c2guQ29sb3IZLlNlcmllcy42Lk1hcmtzLlBlbi5Db2xvcghTZXJpZXMuNxUuU2VyaWVzLjcuQnJ1c2guQ29sb3IZLlNlcmllcy43LlBvaW50ZXIuVmlzaWJsZRwuU2VyaWVzLjcuUG9pbnRlci5TaXplRG91YmxlGy5TZXJpZXMuNy5Qb2ludGVyLlNpemVVbml0cx0uU2VyaWVzLjcuUG9pbnRlci5CcnVzaC5Db2xvchcuU2VyaWVzLjcuTGluZVBlbi5Db2xvchcuU2VyaWVzLjcuWFZhbHVlcy5WYWx1ZRcuU2VyaWVzLjcuWFZhbHVlcy5Db3VudBwuU2VyaWVzLjcuWFZhbHVlcy5EYXRhTWVtYmVyGi5TZXJpZXMuNy5YVmFsdWVzLkRhdGVUaW1lFy5TZXJpZXMuNy5YVmFsdWVzLk9yZGVyFy5TZXJpZXMuNy5ZVmFsdWVzLlZhbHVlFy5TZXJpZXMuNy5ZVmFsdWVzLkNvdW50HC5TZXJpZXMuNy5ZVmFsdWVzLkRhdGFNZW1iZXITLlNlcmllcy43LkNvbG9yRWFjaBQuU2VyaWVzLjcuRGF0YVNvdXJjZQ8uU2VyaWVzLjcuQ29sb3IPLlNlcmllcy43LlRpdGxlHS5TZXJpZXMuNy5Vc2VFeHRlbmRlZE51bVJhbmdlFS5TZXJpZXMuNy5NYXJrcy5TdHlsZSEuU2VyaWVzLjcuTWFya3MuVGFpbFBhcmFtcy5NYXJnaW4oLlNlcmllcy43Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlckhlaWdodCcuU2VyaWVzLjcuTWFya3MuVGFpbFBhcmFtcy5Qb2ludGVyV2lkdGggLlNlcmllcy43Lk1hcmtzLlRhaWxQYXJhbXMuQWxpZ24pLlNlcmllcy43Lk1hcmtzLlRhaWxQYXJhbXMuQ3VzdG9tUG9pbnRQb3MgLlNlcmllcy43Lk1hcmtzLkZvbnQuQnJ1c2guQ29sb3IZLlNlcmllcy43Lk1hcmtzLlBlbi5Db2xvcgdUb29scy4wGi5Ub29scy4wLkxlZ2VuZC5EcmF3QmVoaW5kGy5Ub29scy4wLkxlZ2VuZC5MZWdlbmRTdHlsZR4uVG9vbHMuMC5MZWdlbmQuQ3VzdG9tUG9zaXRpb24ULlRvb2xzLjAuTGVnZW5kLkxlZnQTLlRvb2xzLjAuTGVnZW5kLlRvcBUuVG9vbHMuMC5MZWdlbmQuUmlnaHQWLlRvb2xzLjAuTGVnZW5kLkJvdHRvbRguQXhlcy5MZWZ0LlRpdGxlLkNhcHRpb24WLkF4ZXMuTGVmdC5UaXRsZS5MaW5lcxkuQXhlcy5SaWdodC5UaXRsZS5WaXNpYmxlIy5BeGVzLkJvdHRvbS5MYWJlbHMuUm91bmRGaXJzdExhYmVsIi5BeGVzLkJvdHRvbS5MYWJlbHMuRGF0ZVRpbWVGb3JtYXQfLkF4ZXMuQm90dG9tLkxhYmVscy5WYWx1ZUZvcm1hdBYuQXhlcy5Cb3R0b20uSW5jcmVtZW50Gi5BeGVzLkJvdHRvbS5UaXRsZS5DYXB0aW9uGC5BeGVzLkJvdHRvbS5UaXRsZS5MaW5lcw8uQXhlcy5BdXRvbWF0aWMABAAAAAYAAAEEAAAEBAQHAAEABAcAAQABBAEABAAAAAQEAQQAAAQEBwABAAQHAAEAAQQBAAAAAAQEAQQAAAQEBAcAAQAEBwABAAEEAQAAAAAEBAEEAAAEBAcAAQAEBwABAAEEAQAAAAAEBAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQEBwABAAQHAAEAAQQBAAQAAAAEBAQEAQQAAAQEBAcAAQAEBwABAAEEAQAEAAAABAQEBAEEAAAEBAQHAAEABAcAAQABBAEABAAAAAQEBAQBAAQAAAAAAAEGAAABAQABBgABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAAQgICAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEiU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwIAAAALBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAGCAElU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEiU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwIAAAALBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEiU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwIAAAALBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEiU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5NYXJrc1N0eWxlcwIAAAALBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABHFN0ZWVtYS5UZWVDaGFydC5MZWdlbmRTdHlsZXMCAAAAAQgICAgBAQYBAgAAAAAF/P///xlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAAHAAAAAgAAAAkFAAAAAAAAAAAGBgAAABtTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLkxpbmUF+f///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABfj///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH3////+f///woAAP//AAAAAAAAAgAB9v////n///8KAACZ/wAAAAAAAAIACQsAAAAFAAAABgwAAAAMcHJvY2Vzc19kYXRlAQXz////JVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXIBAAAAB3ZhbHVlX18ACAIAAAABAAAACQ4AAAAFAAAABg8AAAAIZGVmX3RvcDEABhAAAAAOU3FsRGF0YVNvdXJjZTEB7/////n///8KAAD//wAAAAAAAAIABhIAAAAV5YmU6YCA57y66Zm356K8VG9wKDEpAAXt////IlN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuTWFya3NTdHlsZXMBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAAAAAAAAAAAAACBAAAAAAAAAIEAF7P///yRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQBAAAAB3ZhbHVlX18ACAIAAAAAAAAABev///8VU3lzdGVtLkRyYXdpbmcuUG9pbnRGAgAAAAF4AXkAAAsLAwAAAAAAAAAAAAAACQYAAAAB6f////n///8KAIAA/wAAAAAAAAIAAQAAAAAAAAAAAej////4////AAAAAAHn////+f///woATQD/AAAAAAAAAgAJGgAAAAUAAAAJDAAAAAEB5P////P///8BAAAACR0AAAAFAAAABh4AAAAIZGVmX3RvcDIACRAAAAAB4P////n///8KAIAA/wAAAAAAAAIABiEAAAAV5YmU6YCA57y66Zm356K8VG9wKDIpAAAAAAAAAAAAAAAUQAAAAAAAACBAAd7////s////AAAAAAHd////6////wAAAAAAAAAACQYAAAAB2/////n///8KAEDA/wAAAAAAAAIAAQAAAAAAAAAAAdr////4////AAAAAAHZ////+f///woAQMD/AAAAAAAAAgAB2P////n///8KAJmZ/wAAAAAAAAIACSkAAAAFAAAACQwAAAABAdX////z////AQAAAAksAAAABQAAAAYtAAAACGRlZl90b3AzAAkQAAAAAdH////5////CgBAwP8AAAAAAAACAAYwAAAAFeWJlOmAgOe8uumZt+eivFRvcCgzKQAAAAAAAAAAAAAAFEAAAAAAAAAgQAHP////7P///wAAAAABzv///+v///8AAAAAAAAAAAkGAAAAAcz////5////Cv8AAP8AAAAAAAACAAEAAAAAAAAAAAHL////+P///wAAAAAByv////n///8KmQAA/wAAAAAAAAIACTcAAAAFAAAACQwAAAABAcf////z////AQAAAAk6AAAABQAAAAY7AAAACGRlZl90b3A0AAkQAAAAAcP////5////Cv8AAP8AAAAAAAACAAY+AAAAFeWJlOmAgOe8uumZt+eivFRvcCg0KQAAAAAAAAAAAAAAFEAAAAAAAAAgQAHB////7P///wAAAAABwP///+v///8AAAAAAAAAAAkGAAAAAb7////5////Cv//AP8AAAAAAAACAAEAAAAAAAAAAAG9////+P///wAAAAABvP////n///8K//8A/wAAAAAAAAIAAbv////5////CpmZmf8AAAAAAAACAAlGAAAABQAAAAkMAAAAAQG4////8////wEAAAAJSQAAAAUAAAAGSgAAAAhkZWZfdG9wNQAJEAAAAAG0////+f///wr//wD/AAAAAAAAAgAGTQAAABXliZTpgIDnvLrpmbfnorxUb3AoNSkAAAAAAAAAAAAAABRAAAAAAAAAIEABsv///+z///8AAAAAAbH////r////AAAAAAAAAAAJBgAAAAGv////+f///woAAAD/AAAAAAAAAgABAAAAAAAAAAABrv////j///8AAAAAAa3////5////CgAAAP8AAAAAAAACAAGs////+f///woAAJn/AAAAAAAAAgAJVQAAAAUAAAAJDAAAAAEBqf////P///8BAAAACVgAAAAFAAAABlkAAAAMY3V0X2RlZl90b3AxAAkQAAAAAaX////5////CgAAAP8AAAAAAAACAAZcAAAAE+WIh+mZpOe8uumZt+eivFRvcDEAAaP////t////AAAAAAAAAAAAAAAAAAAgQAAAAAAAACBAAaL////s////AAAAAAGh////6////wAAAAAAAAAAAaD////5////CgAAAAAAAAAAAAAAAAGf////+f///woAAAAAAAAAAAAAAAAJBgAAAAGd////+f///woAAAD/AAAAAAAAAgABAAAAAAAAAAABnP////j///8AAAAAAZv////5////CgAAAP8AAAAAAAACAAGa////+f///woAAJn/AAAAAAAAAgAJZwAAAAUAAAAJDAAAAAEBl/////P///8BAAAACWoAAAAFAAAABmsAAAAMY3V0X2RlZl90b3AyAAkQAAAAAZP////5////CgAAAP8AAAAAAAACAAZuAAAAE+WIh+mZpOe8uumZt+eivFRvcDIAAZH////t////AAAAAAAAAAAAAAAAAAAgQAAAAAAAACBAAZD////s////AAAAAAGP////6////wAAAAAAAAAAAY7////5////CgAAAAAAAAAAAAAAAAGN////+f///woAAAAAAAAAAAAAAAAJBgAAAAGL////+f///woAAAD/AAAAAAAAAgABAAAAAAAAAAABiv////j///8AAAAAAYn////5////CgAAAP8AAAAAAAACAAGI////+f///woAAJn/AAAAAAAAAgAJeQAAAAUAAAAJDAAAAAEBhf////P///8BAAAACXwAAAAFAAAABn0AAAAMY3V0X2RlZl90b3AzAAkQAAAAAYH////5////CgAAAP8AAAAAAAACAAaAAAAAE+WIh+mZpOe8uumZt+eivFRvcDMAAX/////t////AAAAAAAAAAAAAAAAAAAgQAAAAAAAACBAAX7////s////AAAAAAF9////6////wAAAAAAAAAAAXz////5////CgAAAAAAAAAAAAAAAAF7////+f///woAAAAAAAAAAAAAAAAGhgAAACFTdGVlbWEuVGVlQ2hhcnQuVG9vbHMuRXh0cmFMZWdlbmQABXn///8cU3RlZW1hLlRlZUNoYXJ0LkxlZ2VuZFN0eWxlcwEAAAAHdmFsdWVfXwAIAgAAAAIAAAABAAAAAAAAAAAAAAAAAAAAAAaIAAAABumHjemHjwmJAAAAAAAGigAAAANNTU0GiwAAAAMjIyMAAAAAAAA+QAaMAAAABuaZgumWkwmNAAAAAREFAAAAAQAAAAaOAAAAGueyvuaVtCMz55W25pyI57y66Zm357Wx6KiIDwsAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPDgAAAAUAAAAG7FG4HoUfeUAUrkfhepu5QEjhehRu4LFA16NwPQrnsUC4HoXrUZmnQA8aAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADx0AAAAFAAAABh+F61G4dnhAUrgehSscsUCkcD0KFxywQBSuR+H6YqdAuB6F61HaoEAPKQAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA8sAAAABQAAAAbNzMzMzORnQBSuR+F65qFAH4XrUbjEpkCuR+F6FLKlQHsUrkfhjIlADzcAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPOgAAAAUAAAAGw/UoXI96Z0CamZmZmZ+hQEjhehQuTaNAj8L1KFz7nUD2KFyPwgOHQA9GAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVAD0kAAAAFAAAABoXrUbgepWFAXI/C9SjwhECamZmZme6eQBSuR+F6TZxApHA9Cte/hkAPVQAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA9YAAAABQAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD2cAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPagAAAAUAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA95AAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVAD3wAAAAFAAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARiQAAAAEAAAAGjwAAAAbph43ph48RjQAAAAEAAAAGkAAAAAbmmYLplpML" CssClass="auto-style1"  GetChartFile="GetChart.aspx" Height="400px" LastFileName="" Width="900px" AutoPostback="False" EnableTheming="False" style="height: 344px; width: 862px" ViewStateMode="Disabled" DataSourceID="SqlDataSource1"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="select
a.process_date,
a.an_weight as def_top1,
b.an_weight as def_top2,
c.an_weight as def_top3,
d.an_weight as def_top4,
e.an_weight as def_top5,
isnull(f.cut_def_top1,0) as cut_def_top1,
isnull(f.cut_def_top2,0) as cut_def_top2,
isnull(f.cut_def_top3,0) as cut_def_top3

from 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(an_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(an_weight)/1000,2) as float) as an_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_1 as def_code,an_weight as an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_2 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_3 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_4 ,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 1) as a 
inner join 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(an_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(an_weight)/1000,2) as float) as an_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_1 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_2 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_3 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_4 ,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 2) as b on a.process_date=b.process_date
inner join 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(an_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(an_weight)/1000,2) as float) as an_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_1 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_2 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_3 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_4 ,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 3) as c on a.process_date=c.process_date
inner join
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(an_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(an_weight)/1000,2) as float) as an_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_1 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_2 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_3 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_4 ,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 4) as d on a.process_date=d.process_date
inner join 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(an_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(an_weight)/1000,2) as float) as an_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_1 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_2 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_3 as def_code,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
def_code_4 ,an_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and def_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 5) as e on a.process_date = e.process_date
left join (
select
a.process_date,
a.cd_weight as cut_def_top1,
b.cd_weight as cut_def_top2,
c.cd_weight as cut_def_top3
from 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(cd_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(cd_weight)/1000,2) as float) as cd_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_1 as def_code,cd_weight_1 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_2 as def_code,cd_weight_2 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_3 as def_code,cd_weight_3 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_4 ,cd_weight_4 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_4 != ''
) as a 
group by process_date,def_code) as a 
where id = 1) as a 
inner join 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(cd_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(cd_weight)/1000,2) as float) as cd_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_1 as def_code,cd_weight_1 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_2 as def_code,cd_weight_2 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_3 as def_code,cd_weight_3 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_4 ,cd_weight_4 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_4 != ''

) as a 
group by process_date,def_code) as a 
where id = 2) as b on a.process_date=b.process_date
inner join 
(select a.* from(
select ROW_NUMBER() Over (Partition By dateadd(m, datediff(m,0,process_date),0) order by dateadd(m, datediff(m,0,process_date),0),cast(round(SUM(cd_weight)/1000,2) as float) desc) As ID,a.process_date,a.def_code,cast(round(SUM(cd_weight)/1000,2) as float) as cd_weight
from(
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_1 as def_code,cd_weight_1 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_1 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_2 as def_code,cd_weight_2 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_2 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_3 as def_code,cd_weight_3 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_3 != ''

union
select 
distinct 
dateadd(m, datediff(m,0,process_date),0) as process_date,
cd_code_4 ,cd_weight_4 as cd_weight from h_pmis_wh73
where process_date between DATEADD(year,-1,getdate()) and getdate() and cd_code_4 != ''

) as a 
group by process_date,def_code) as a 
where id = 3) as c on a.process_date=c.process_date) as f on a.process_date=f.process_date
order by process_date"></asp:SqlDataSource>
                    </td>
                </tr>
                    </table>    
                    </td>
                </tr>
              <%--  <tr>
                   <%-- <td>
                        <asp:Button ID="btnUp" runat="server" OnClick="btnUp_Click" Text="前一月" />
                        <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="後一月" />
                        <input id="Radio1" onclick="create_chart(1)" type="radio" />Reject&nbsp;
                        <input id="Radio2" onclick="create_chart(2)" type="radio" />Cut</td>
                </tr>--%>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 30px; position: absolute; top: 390px;">
                <colgroup>
                    <col width="60" />
                    <col span="8" width="108" />
                </colgroup>
                <caption align="right">&nbsp;</caption>
                <tr>
                    <td>
                    </td>
                    <td class="gvhs_data" colspan="5" style="text-align: center">
                        剔除</td>
                    <td class="gvhs_data" colspan="3" style="text-align: center">
                        切除&nbsp;</td>
                </tr>
                <tr>
                    <td class="gvhs_data">
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (1)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (2)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (3)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (4)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (5)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (1)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (2)
                    </td>
                    <td class="gvhs_data" style="text-align: center">
                        缺陷 top (3)
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 30px; position: absolute; top: 650px;">
                <colgroup>
                    <col width="60" />
                    <col span="8" width="108" />
                </colgroup>
                <tr>
                    <td class="data">
                        <asp:Label ID="lblMonth" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>月統計
                    </td>
                    <td class="data">
                        <asp:Label ID="lblR1" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblR2" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblR3" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblR4" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblR5" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblC1" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblC2" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblC3" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                    </td>
                </tr>
            </table>
        
   
        </form>
    
</body>
</html>
