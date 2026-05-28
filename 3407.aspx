<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3407.aspx.vb" Inherits="hPMISWEB._4TNRL_Production" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register Assembly="TeeChart" Namespace="Steema.TeeChart.Web" TagPrefix="tchart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>4TNRL_Production</title>
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="left: 25px; position: absolute;
            top: 150px; border-collapse: collapse">
            <tr>
                <td>
                    <strong>精整#4生產分析</strong></td>
            </tr>
            <tr>
                <td style="height: 45px">
                    <br />
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; position: absolute;
                        top: 27px; border-collapse: collapse">
                        <colgroup>
                            <col width="115" />
                            <col span="10" width="80" />
                        </colgroup>
                        <tr>
                            <td class="gvhs_data" style="text-align: center; width: 100px;">
                                尺寸</td>
                            <td class="gvhs_data" style="text-align: center">
                                極薄板<br />
                                (ETNG)</td>
                            <td class="gvhs_data" style="text-align: center">
                                寬薄板<br />
                                (WTNG)</td>
                            <td class="gvhs_data" style="text-align: center">
                                薄板<br />
                                (NTNG)</td>
                            <td class="gvhs_data" style="text-align: center">
                                寬厚板<br />
                                (NTCG)</td>
                            <td class="gvhs_data" style="text-align: center">
                                極厚板<br />
                                (ETCG)</td>
                            <td class="gvhs_data" style="text-align: center">
                                中厚板<br />
                                (MDSZ)</td>
                            <td style="text-align: center">
                                <strong><span style="color: white; background-color: #507cd1"></span></strong>
                            </td>
                            <td class="gvhs_data" style="text-align: center">
                                窄板<br />
                                (NRWD)</td>
                            <td class="gvhs_data" style="text-align: center">
                                中寬板<br />
                                (MDWD)</td>
                            <td class="gvhs_data" style="text-align: center">
                                寬板<br />
                                (WIWD)</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" Height="190px" ScrollBars="Vertical">
                        <table style="border-collapse: collapse" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                        <td style="vertical-align: top">
                        <asp:GridView ID="gvMonth1" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                            <FooterStyle CssClass="gvfs" />
                            <RowStyle CssClass="gvrs2" />
                            <EditRowStyle CssClass="gvers" />
                            <SelectedRowStyle CssClass="gvsrs" />
                            <PagerStyle CssClass="gvps" />
                            <HeaderStyle CssClass="gvhs" />
                            <EmptyDataRowStyle CssClass="gvemrs" />
                        </asp:GridView>
                        </td>
                        <td style="width: 80px; height: 151px;">
                        </td>
                        <td style="vertical-align: top">
                        <asp:GridView ID="gvMonth3" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <FooterStyle CssClass="gvfs" />
                                <RowStyle CssClass="gvrs2" />
                                <EditRowStyle CssClass="gvers" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <PagerStyle CssClass="gvps" />
                                <HeaderStyle CssClass="gvhs" />
                                <EmptyDataRowStyle CssClass="gvemrs" />
                        </asp:GridView>
                        </td>
                        </tr>
                        </table>                     
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; position: absolute;
                        top: 250px; border-collapse: collapse">
                        <colgroup>
                            <col width="115" />
                            <col span="10" width="80" />
                        </colgroup>
                        <tr>
                            <td class="data">
                                <asp:Label ID="lblMonth1" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>月統計
                            </td>
                            <td class="data">
                                <asp:Label ID="lblETNG" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblWTNG" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblNTNG" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblNTCG" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblETCG" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblMDSZ" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td></td>
                            <td class="data">
                                <asp:Label ID="lblNRWD" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblMDWD" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblWIWD" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="left: 25px; position: absolute;
            top: 430px; border-collapse: collapse">
            <tr>
                <td>
                    <strong></strong>
                </td>
            </tr>
            <tr>
                <td style="height: 35px;">
                    <br />
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; position: absolute;
                        top: 0px; border-collapse: collapse">
                        <colgroup>
                            <col width="115" />
                            <col span="10" width="80" />
                        </colgroup>
                        <tr>
                            <td class="gvhs_data" style="text-align: center; width: 100px;">
                                強度與品質</td>
                            <td class="gvhs_data" style="text-align: center">
                                極低碳鋼(EXLC)</td>
                            <td class="gvhs_data" style="text-align: center">
                                低強度鋼(LSCS)</td>
                            <td class="gvhs_data" style="text-align: center">
                                中強度鋼(MSCS)</td>
                            <td class="gvhs_data" style="text-align: center">
                                高強度鋼(HICS)</td>
                            <td class="gvhs_data" style="text-align: center">
                                超高強度鋼(VHIS)</td>
                            <td class="gvhs_data" style="text-align: center">
                                不鏽鋼(SUS)</td>
                            <td style="text-align: center"></td>
                            <td class="gvhs_data" style="text-align: center">
                                一般品級(NRCQ)</td>
                            <td class="gvhs_data" style="text-align: center">
                                高品級(HICQ)</td>
                            <td class="gvhs_data" style="text-align: center">
                                超高品級(VHCQ)</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" BackColor="Transparent" Height="200px" ScrollBars="Vertical">
                        <table style="border-collapse: collapse" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                        <td style="vertical-align: top">
                            <asp:GridView ID="gvMonth2" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <FooterStyle CssClass="gvfs" />
                                <RowStyle CssClass="gvrs2" />
                                <EditRowStyle CssClass="gvers" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <PagerStyle CssClass="gvps" />
                                <HeaderStyle CssClass="gvhs" />
                            </asp:GridView>
                        </td>
                        <td style="width: 80px">
                        </td>
                        <td style="vertical-align: top">
                            <asp:GridView ID="gvMonth4" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <FooterStyle CssClass="gvfs" />
                                <RowStyle CssClass="gvrs2" />
                                <EditRowStyle CssClass="gvers" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <PagerStyle CssClass="gvps" />
                                <HeaderStyle CssClass="gvhs" />
                            </asp:GridView>
                        </td>
                        </tr>
                        </table>
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; position: absolute;
                        top: 240px; border-collapse: collapse">
                        <colgroup>
                            <col width="115" />
                            <col span="10" width="80" />
                        </colgroup>
                        <tr>
                            <td class="data">
                                <asp:Label ID="lblMonth2" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>月統計
                            </td>
                            <td class="data">
                                <asp:Label ID="lblEXLC" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblLSCS" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblMSCS" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblHICS" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblVHIS" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblSUS" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblNRCQ" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblHICQ" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                            <td class="data">
                                <asp:Label ID="lblVHCQ" runat="server" CssClass="pmisdata" Text="N/A"></asp:Label>
                            </td>
                        </tr>
                      </table>
                   </table>
                  <table  border="0" cellpadding="0" cellspacing="0" style="left: 0px; position: absolute;
                        top: 700px; border-collapse: collapse" >
                   
                        <tr>
                          
                            <td>
                                   資料區間<asp:Label ID="LabelStartdate" runat="server"></asp:Label>
                        ~<asp:Label ID="LabelEnddate" runat="server"></asp:Label>
                                  <tchart:WebChart ID="WebChart1" runat="server" UseLock="False" TempChart="Session"  Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnSlAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoFC5TZXJpZXMuMC5EYXRhU291cmNlDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4xFS5TZXJpZXMuMS5CcnVzaC5Db2xvchkuU2VyaWVzLjEuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4xLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4xLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4xLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4xLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4xLllWYWx1ZXMuQ291bnQcLlNlcmllcy4xLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjEuQ29sb3JFYWNoFC5TZXJpZXMuMS5EYXRhU291cmNlDy5TZXJpZXMuMS5Db2xvcg8uU2VyaWVzLjEuVGl0bGUdLlNlcmllcy4xLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4yFS5TZXJpZXMuMi5CcnVzaC5Db2xvchkuU2VyaWVzLjIuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMi5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4yLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMi5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMi5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMi5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMi5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMi5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4yLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4yLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4yLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4yLllWYWx1ZXMuQ291bnQcLlNlcmllcy4yLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjIuQ29sb3JFYWNoFC5TZXJpZXMuMi5EYXRhU291cmNlDy5TZXJpZXMuMi5Db2xvcg8uU2VyaWVzLjIuVGl0bGUdLlNlcmllcy4yLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4zFS5TZXJpZXMuMy5CcnVzaC5Db2xvchkuU2VyaWVzLjMuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMy5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4zLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMy5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMy5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMy5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMy5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4zLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4zLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4zLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4zLllWYWx1ZXMuQ291bnQcLlNlcmllcy4zLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjMuQ29sb3JFYWNoFC5TZXJpZXMuMy5EYXRhU291cmNlDy5TZXJpZXMuMy5Db2xvcg8uU2VyaWVzLjMuVGl0bGUdLlNlcmllcy4zLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4zLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4zLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy40FS5TZXJpZXMuNC5CcnVzaC5Db2xvchkuU2VyaWVzLjQuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuNC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy40LlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuNC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuNC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuNC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuNC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuNC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy40LlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy40LlhWYWx1ZXMuT3JkZXIXLlNlcmllcy40LllWYWx1ZXMuVmFsdWUXLlNlcmllcy40LllWYWx1ZXMuQ291bnQcLlNlcmllcy40LllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjQuQ29sb3JFYWNoFC5TZXJpZXMuNC5EYXRhU291cmNlDy5TZXJpZXMuNC5Db2xvcg8uU2VyaWVzLjQuVGl0bGUdLlNlcmllcy40LlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy40Lk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy40Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy41FS5TZXJpZXMuNS5CcnVzaC5Db2xvchkuU2VyaWVzLjUuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuNS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy41LlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuNS5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuNS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuNS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuNS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuNS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy41LlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy41LlhWYWx1ZXMuT3JkZXIXLlNlcmllcy41LllWYWx1ZXMuVmFsdWUXLlNlcmllcy41LllWYWx1ZXMuQ291bnQcLlNlcmllcy41LllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjUuQ29sb3JFYWNoFC5TZXJpZXMuNS5EYXRhU291cmNlDy5TZXJpZXMuNS5Db2xvcg8uU2VyaWVzLjUuVGl0bGUdLlNlcmllcy41LlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGC5BeGVzLkxlZnQuVGl0bGUuQ2FwdGlvbhYuQXhlcy5MZWZ0LlRpdGxlLkxpbmVzIy5BeGVzLkJvdHRvbS5MYWJlbHMuUm91bmRGaXJzdExhYmVsIi5BeGVzLkJvdHRvbS5MYWJlbHMuRGF0ZVRpbWVGb3JtYXQfLkF4ZXMuQm90dG9tLkxhYmVscy5WYWx1ZUZvcm1hdBYuQXhlcy5Cb3R0b20uSW5jcmVtZW50Gi5BeGVzLkJvdHRvbS5UaXRsZS5DYXB0aW9uGC5BeGVzLkJvdHRvbS5UaXRsZS5MaW5lcw8uQXhlcy5BdXRvbWF0aWMABAAAAAYAAAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQEBwABAAQHAAEAAQQBAAAAAAQEAQQAAAQEBwABAAQHAAEAAQQBAAAAAAQEAQQAAAQEBAcAAQAEBwABAAEEAQAAAAAEBAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBgABAQABBgABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAAQgICAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAGCAElU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAAQYBAgAAAAAF/P///xlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAAHAAAAAgAAAAkFAAAAAAAAAAAGBgAAABtTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLkxpbmUF+f///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABfj///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH3////+f///woAAP//AAAAAAAAAgAB9v////n///8KAACZ/wAAAAAAAAIACQsAAAAFAAAABgwAAAAMcHJvY2Vzc19kYXRlAQXz////JVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXIBAAAAB3ZhbHVlX18ACAIAAAABAAAACQ4AAAAFAAAABg8AAAAERVRORwAGEAAAAA5TcWxEYXRhU291cmNlMQHv////+f///woAAP//AAAAAAAAAgAGEgAAAARFVE5HAAAAAAAAAAAAAAAgQAAAAAAAACBABe3///8kU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAXs////FVN5c3RlbS5EcmF3aW5nLlBvaW50RgIAAAABeAF5AAALCwMAAAAAAAAAAAAAAAkGAAAAAer////5////CgCAAP8AAAAAAAACAAEAAAAAAAAAAAHp////+P///wAAAAAB6P////n///8KAE0A/wAAAAAAAAIACRkAAAAFAAAACQwAAAABAeX////z////AQAAAAkcAAAABQAAAAYdAAAABFdUTkcACRAAAAAB4f////n///8KAIAA/wAAAAAAAAIABiAAAAAEV1RORwAAAAAAAAAAAAAAFEAAAAAAAAAgQAHf////7f///wAAAAAB3v///+z///8AAAAAAAAAAAkGAAAAAdz////5////CgBAwP8AAAAAAAACAAEAAAAAAAAAAAHb////+P///wAAAAAB2v////n///8KAEDA/wAAAAAAAAIAAdn////5////CgCZmf8AAAAAAAACAAkoAAAABQAAAAkMAAAAAQHW////8////wEAAAAJKwAAAAUAAAAGLAAAAAROVE5HAAkQAAAAAdL////5////CgBAwP8AAAAAAAACAAYvAAAABE5UTkcAAAAAAAAAAAAAABRAAAAAAAAAIEAB0P///+3///8AAAAAAc/////s////AAAAAAAAAAAJBgAAAAHN////+f///wr/AAD/AAAAAAAAAgABAAAAAAAAAAABzP////j///8AAAAAAcv////5////CpkAAP8AAAAAAAACAAk2AAAABQAAAAkMAAAAAQHI////8////wEAAAAJOQAAAAUAAAAGOgAAAAROVENHAAkQAAAAAcT////5////Cv8AAP8AAAAAAAACAAY9AAAABE5UQ0cAAAAAAAAAAAAAABRAAAAAAAAAIEABwv///+3///8AAAAAAcH////s////AAAAAAAAAAAJBgAAAAG/////+f///wr//wD/AAAAAAAAAgABAAAAAAAAAAABvv////j///8AAAAAAb3////5////Cv//AP8AAAAAAAACAAG8////+f///wqZmZn/AAAAAAAAAgAJRQAAAAUAAAAJDAAAAAEBuf////P///8BAAAACUgAAAAFAAAABkkAAAAERVRDRwAJEAAAAAG1////+f///wr//wD/AAAAAAAAAgAGTAAAAARFVENHAAAAAAAAAAAAAAAUQAAAAAAAACBAAbP////t////AAAAAAGy////7P///wAAAAAAAAAACQYAAAABsP////n///8KAAAA/wAAAAAAAAIAAQAAAAAAAAAAAa/////4////AAAAAAGu////+f///woAAAD/AAAAAAAAAgABrf////n///8KmZmZ/wAAAAAAAAIACVQAAAAFAAAACQwAAAABAar////z////AQAAAAlXAAAABQAAAAZYAAAABE1EU1oACRAAAAABpv////n///8KAAAA/wAAAAAAAAIABlsAAAAETURTWgAAAAAAAAAAAAAAIEAAAAAAAAAgQAGk////7f///wAAAAABo////+z///8AAAAAAAAAAAZeAAAABumHjemHjwlfAAAAAAZgAAAAA01NTQZhAAAAAyMjIwAAAAAAAD5ABmIAAAAG5pmC6ZaTCWMAAAABEQUAAAABAAAABmQAAAAJVGhpY2tuZXNzDwsAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPDgAAAAUAAAAGSOF6FC6msEBI4XoUDoXdQJqZmZkhyeFAAAAAAFCA5ECPwvUoRFbgQA8ZAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADxwAAAAFAAAABoXrUbgei5FAzczMzIyvzkC4HoXrEY7IQFK4HoUL0cpAuB6F61HgyUAPKAAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA8rAAAABQAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADzYAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPOQAAAAUAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA9FAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVAD0gAAAAFAAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPVAAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA9XAAAABQAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEV8AAAABAAAABmUAAAAG6YeN6YePEWMAAAABAAAABmYAAAAG5pmC6ZaTCw==" CssClass="auto-style1"  GetChartFile="GetChart.aspx" Height="300px" LastFileName="" Width="900px" AutoPostback="False" EnableTheming="False"  ViewStateMode="Disabled" DataSourceID="SqlDataSource1"  />
                            </td>
                                <td>
                                <tchart:WebChart ID="WebChart2" runat="server" UseLock="False" TempChart="Session"  Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnRbAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoFC5TZXJpZXMuMC5EYXRhU291cmNlDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4xFS5TZXJpZXMuMS5CcnVzaC5Db2xvchkuU2VyaWVzLjEuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4xLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4xLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4xLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4xLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4xLllWYWx1ZXMuQ291bnQcLlNlcmllcy4xLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjEuQ29sb3JFYWNoFC5TZXJpZXMuMS5EYXRhU291cmNlDy5TZXJpZXMuMS5Db2xvcg8uU2VyaWVzLjEuVGl0bGUdLlNlcmllcy4xLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4yFS5TZXJpZXMuMi5CcnVzaC5Db2xvchkuU2VyaWVzLjIuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMi5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4yLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMi5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMi5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMi5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMi5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMi5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4yLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4yLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4yLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4yLllWYWx1ZXMuQ291bnQcLlNlcmllcy4yLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjIuQ29sb3JFYWNoFC5TZXJpZXMuMi5EYXRhU291cmNlDy5TZXJpZXMuMi5Db2xvcg8uU2VyaWVzLjIuVGl0bGUdLlNlcmllcy4yLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGC5BeGVzLkxlZnQuVGl0bGUuQ2FwdGlvbhYuQXhlcy5MZWZ0LlRpdGxlLkxpbmVzIy5BeGVzLkJvdHRvbS5MYWJlbHMuUm91bmRGaXJzdExhYmVsIi5BeGVzLkJvdHRvbS5MYWJlbHMuRGF0ZVRpbWVGb3JtYXQfLkF4ZXMuQm90dG9tLkxhYmVscy5WYWx1ZUZvcm1hdBYuQXhlcy5Cb3R0b20uSW5jcmVtZW50Gi5BeGVzLkJvdHRvbS5UaXRsZS5DYXB0aW9uGC5BeGVzLkJvdHRvbS5UaXRsZS5MaW5lcw8uQXhlcy5BdXRvbWF0aWMABAAAAAYAAAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQEBwABAAQHAAEAAQQBAAAAAAQEAQYAAQEAAQYAARlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAgAAAAEICAgBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAGCAElU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAABBgECAAAAAAX8////GVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAAcAAAACAAAACQUAAAAAAAAAAAYGAAAAG1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuTGluZQX5////FFN5c3RlbS5EcmF3aW5nLkNvbG9yBAAAAARuYW1lBXZhbHVlCmtub3duQ29sb3IFc3RhdGUBAAAACQcHAwAAAAoAAP//AAAAAAAAAgABAAAAAAAAAAAF+P///ydTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAff////5////CgAA//8AAAAAAAACAAH2////+f///woAAJn/AAAAAAAAAgAJCwAAAAUAAAAGDAAAAAxwcm9jZXNzX2RhdGUBBfP///8lU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgEAAAAHdmFsdWVfXwAIAgAAAAEAAAAJDgAAAAUAAAAGDwAAAAROUldEAAYQAAAADlNxbERhdGFTb3VyY2UxAe/////5////CgAA//8AAAAAAAACAAYSAAAABE5SV0QAAAAAAAAAAAAAACBAAAAAAAAAIEAF7f///yRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQBAAAAB3ZhbHVlX18ACAIAAAAAAAAABez///8VU3lzdGVtLkRyYXdpbmcuUG9pbnRGAgAAAAF4AXkAAAsLAwAAAAAAAAAAAAAACQYAAAAB6v////n///8KAIAA/wAAAAAAAAIAAQAAAAAAAAAAAen////4////AAAAAAHo////+f///woATQD/AAAAAAAAAgAJGQAAAAUAAAAJDAAAAAEB5f////P///8BAAAACRwAAAAFAAAABh0AAAAETURXRAAJEAAAAAHh////+f///woAgAD/AAAAAAAAAgAGIAAAAARNRFdEAAAAAAAAAAAAAAAUQAAAAAAAACBAAd/////t////AAAAAAHe////7P///wAAAAAAAAAACQYAAAAB3P////n///8KAEDA/wAAAAAAAAIAAQAAAAAAAAAAAdv////4////AAAAAAHa////+f///woAQMD/AAAAAAAAAgAB2f////n///8KAJmZ/wAAAAAAAAIACSgAAAAFAAAACQwAAAABAdb////z////AQAAAAkrAAAABQAAAAYsAAAABFdJV0QACRAAAAAB0v////n///8KAEDA/wAAAAAAAAIABi8AAAAEV0lXRAAAAAAAAAAAAAAAFEAAAAAAAAAgQAHQ////7f///wAAAAABz////+z///8AAAAAAAAAAAYyAAAABumHjemHjwkzAAAAAAY0AAAAA01NTQY1AAAAAyMjIwAAAAAAAD5ABjYAAAAG5pmC6ZaTCTcAAAABEQUAAAABAAAABjgAAAAFV2lkdGgPCwAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA8OAAAABQAAAAa4HoXrUS6CQM3MzMwsXsRAXI/C9YgIwkAK16NwfUPLQHE9CteDjcBADxkAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPHAAAAAUAAAAGXI/C9WiZsUB7FK5H4eTlQFyPwvWYQulAw/UoXAcz50C4HoXrGVnmQA8oAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADysAAAAFAAAABh+F61G4mIJAj8L1KNwtoUDNzMzMTBaiQDMzMzMzhp9ApHA9CtdXkUARMwAAAAEAAAAGOQAAAAbph43ph48RNwAAAAEAAAAGOgAAAAbmmYLplpML" CssClass="auto-style1"  GetChartFile="GetChart.aspx" Height="300px" LastFileName="" Width="900px" AutoPostback="False" EnableTheming="False"  ViewStateMode="Disabled" DataSourceID="SqlDataSource1"  />
                 
                                </td>
                        </tr>
                        <tr>
                            <td>
                                       <tchart:WebChart ID="WebChart3" runat="server" UseLock="False" TempChart="Session"  Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnSlAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoFC5TZXJpZXMuMC5EYXRhU291cmNlDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4xFS5TZXJpZXMuMS5CcnVzaC5Db2xvchkuU2VyaWVzLjEuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4xLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4xLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4xLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4xLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4xLllWYWx1ZXMuQ291bnQcLlNlcmllcy4xLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjEuQ29sb3JFYWNoFC5TZXJpZXMuMS5EYXRhU291cmNlDy5TZXJpZXMuMS5Db2xvcg8uU2VyaWVzLjEuVGl0bGUdLlNlcmllcy4xLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4yFS5TZXJpZXMuMi5CcnVzaC5Db2xvchkuU2VyaWVzLjIuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMi5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4yLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMi5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMi5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMi5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMi5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMi5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4yLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4yLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4yLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4yLllWYWx1ZXMuQ291bnQcLlNlcmllcy4yLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjIuQ29sb3JFYWNoFC5TZXJpZXMuMi5EYXRhU291cmNlDy5TZXJpZXMuMi5Db2xvcg8uU2VyaWVzLjIuVGl0bGUdLlNlcmllcy4yLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4zFS5TZXJpZXMuMy5CcnVzaC5Db2xvchkuU2VyaWVzLjMuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMy5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4zLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMy5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMy5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMy5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMy5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4zLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4zLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4zLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4zLllWYWx1ZXMuQ291bnQcLlNlcmllcy4zLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjMuQ29sb3JFYWNoFC5TZXJpZXMuMy5EYXRhU291cmNlDy5TZXJpZXMuMy5Db2xvcg8uU2VyaWVzLjMuVGl0bGUdLlNlcmllcy4zLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4zLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4zLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMy5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy40FS5TZXJpZXMuNC5CcnVzaC5Db2xvchkuU2VyaWVzLjQuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuNC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy40LlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuNC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuNC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuNC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuNC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuNC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy40LlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy40LlhWYWx1ZXMuT3JkZXIXLlNlcmllcy40LllWYWx1ZXMuVmFsdWUXLlNlcmllcy40LllWYWx1ZXMuQ291bnQcLlNlcmllcy40LllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjQuQ29sb3JFYWNoFC5TZXJpZXMuNC5EYXRhU291cmNlDy5TZXJpZXMuNC5Db2xvcg8uU2VyaWVzLjQuVGl0bGUdLlNlcmllcy40LlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy40Lk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy40Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuNC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy41FS5TZXJpZXMuNS5CcnVzaC5Db2xvchkuU2VyaWVzLjUuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuNS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy41LlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuNS5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuNS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuNS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuNS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuNS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy41LlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy41LlhWYWx1ZXMuT3JkZXIXLlNlcmllcy41LllWYWx1ZXMuVmFsdWUXLlNlcmllcy41LllWYWx1ZXMuQ291bnQcLlNlcmllcy41LllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjUuQ29sb3JFYWNoFC5TZXJpZXMuNS5EYXRhU291cmNlDy5TZXJpZXMuNS5Db2xvcg8uU2VyaWVzLjUuVGl0bGUdLlNlcmllcy41LlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy41Lk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuNS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGC5BeGVzLkxlZnQuVGl0bGUuQ2FwdGlvbhYuQXhlcy5MZWZ0LlRpdGxlLkxpbmVzIy5BeGVzLkJvdHRvbS5MYWJlbHMuUm91bmRGaXJzdExhYmVsIi5BeGVzLkJvdHRvbS5MYWJlbHMuRGF0ZVRpbWVGb3JtYXQfLkF4ZXMuQm90dG9tLkxhYmVscy5WYWx1ZUZvcm1hdBYuQXhlcy5Cb3R0b20uSW5jcmVtZW50Gi5BeGVzLkJvdHRvbS5UaXRsZS5DYXB0aW9uGC5BeGVzLkJvdHRvbS5UaXRsZS5MaW5lcw8uQXhlcy5BdXRvbWF0aWMABAAAAAYAAAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQEBwABAAQHAAEAAQQBAAAAAAQEAQQAAAQEBwABAAQHAAEAAQQBAAAAAAQEAQQAAAQEBAcAAQAEBwABAAEEAQAAAAAEBAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBgABAQABBgABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAAQgICAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAGCAElU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQYnU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAgAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAABggBJVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXICAAAABggBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAELBgYkU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AgAAABVTeXN0ZW0uRHJhd2luZy5Qb2ludEYDAAAAAQYBAgAAAAAF/P///xlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAAHAAAAAgAAAAkFAAAAAAAAAAAGBgAAABtTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLkxpbmUF+f///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABfj///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH3////+f///woAAP//AAAAAAAAAgAB9v////n///8KAACZ/wAAAAAAAAIACQsAAAAFAAAABgwAAAAMcHJvY2Vzc19kYXRlAQXz////JVN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVmFsdWVMaXN0T3JkZXIBAAAAB3ZhbHVlX18ACAIAAAABAAAACQ4AAAAFAAAABg8AAAAERVhMQwAGEAAAAA5TcWxEYXRhU291cmNlMQHv////+f///woAAP//AAAAAAAAAgAGEgAAAARFWExDAAAAAAAAAAAAAAAgQAAAAAAAACBABe3///8kU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5UYWlsQWxpZ25tZW50AQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAXs////FVN5c3RlbS5EcmF3aW5nLlBvaW50RgIAAAABeAF5AAALCwMAAAAAAAAAAAAAAAkGAAAAAer////5////CgCAAP8AAAAAAAACAAEAAAAAAAAAAAHp////+P///wAAAAAB6P////n///8KAE0A/wAAAAAAAAIACRkAAAAFAAAACQwAAAABAeX////z////AQAAAAkcAAAABQAAAAYdAAAABExTQ1MACRAAAAAB4f////n///8KAIAA/wAAAAAAAAIABiAAAAAETFNDUwAAAAAAAAAAAAAAFEAAAAAAAAAgQAHf////7f///wAAAAAB3v///+z///8AAAAAAAAAAAkGAAAAAdz////5////CgBAwP8AAAAAAAACAAEAAAAAAAAAAAHb////+P///wAAAAAB2v////n///8KAEDA/wAAAAAAAAIAAdn////5////CgCZmf8AAAAAAAACAAkoAAAABQAAAAkMAAAAAQHW////8////wEAAAAJKwAAAAUAAAAGLAAAAARNU0NTAAkQAAAAAdL////5////CgBAwP8AAAAAAAACAAYvAAAABE1TQ1MAAAAAAAAAAAAAABRAAAAAAAAAIEAB0P///+3///8AAAAAAc/////s////AAAAAAAAAAAJBgAAAAHN////+f///wr/AAD/AAAAAAAAAgABAAAAAAAAAAABzP////j///8AAAAAAcv////5////CpkAAP8AAAAAAAACAAk2AAAABQAAAAkMAAAAAQHI////8////wEAAAAJOQAAAAUAAAAGOgAAAARISUNTAAkQAAAAAcT////5////Cv8AAP8AAAAAAAACAAY9AAAABEhJQ1MAAAAAAAAAAAAAABRAAAAAAAAAIEABwv///+3///8AAAAAAcH////s////AAAAAAAAAAAJBgAAAAG/////+f///wr//wD/AAAAAAAAAgABAAAAAAAAAAABvv////j///8AAAAAAb3////5////Cv//AP8AAAAAAAACAAG8////+f///wqZmZn/AAAAAAAAAgAJRQAAAAUAAAAJDAAAAAEBuf////P///8BAAAACUgAAAAFAAAABkkAAAAEVkhJUwAJEAAAAAG1////+f///wr//wD/AAAAAAAAAgAGTAAAAARWSElTAAAAAAAAAAAAAAAUQAAAAAAAACBAAbP////t////AAAAAAGy////7P///wAAAAAAAAAACQYAAAABsP////n///8KAAAA/wAAAAAAAAIAAQAAAAAAAAAAAa/////4////AAAAAAGu////+f///woAAAD/AAAAAAAAAgABrf////n///8KmZmZ/wAAAAAAAAIACVQAAAAFAAAACQwAAAABAar////z////AQAAAAlXAAAABQAAAAZYAAAAA1NVUwAJEAAAAAGm////+f///woAAAD/AAAAAAAAAgAGWwAAAANTVVMAAAAAAAAAAAAAACBAAAAAAAAAIEABpP///+3///8AAAAAAaP////s////AAAAAAAAAAAGXgAAAAbph43ph48JXwAAAAAGYAAAAANNTU0GYQAAAAMjIyMAAAAAAAA+QAZiAAAABuaZgumWkwljAAAAAREFAAAAAQAAAAZkAAAACFN0cmVuZ3RoDwsAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPDgAAAAUAAAAGSOF6FK53WkBcj8L16He3QDMzMzNzA8BAAAAAAEBcvkDhehSuR3a7QA8ZAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADxwAAAAFAAAABpqZmZmZSX5ASOF6FO5oyEBmZmZmRhHWQIXrUbgeN9ZAMzMzM/Mpz0APKAAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA8rAAAABQAAAAYK16NwPU54QOF6FK5HRsJAMzMzM3MjyUA9CtejUDrQQB+F61FYcc1ADzYAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPOQAAAAUAAAAG16NwPQrhhkC4HoXr8VXaQGZmZmZ2bNFArkfhepT4zUD2KFyPopnOQA9FAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVAD0gAAAAFAAAABgAAAAAAAAAA7FG4HgVaokA9CtejcNiYQI/C9ShcgYRAuB6F61G4mUAPVAAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA9XAAAABQAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEV8AAAABAAAABmUAAAAG6YeN6YePEWMAAAABAAAABmYAAAAG5pmC6ZaTCw==" CssClass="auto-style1"  GetChartFile="GetChart.aspx" Height="300px" LastFileName="" Width="900px" AutoPostback="False" EnableTheming="False"  ViewStateMode="Disabled" DataSourceID="SqlDataSource1"  />
                             </td>
                                <td>
                                <tchart:WebChart ID="WebChart4" runat="server" UseLock="False" TempChart="Session"  Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnRbAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QVLkxlZ2VuZC5UZXh0U3ltYm9sR2FwEy5MZWdlbmQuVmVydFNwYWNpbmcNLkhlYWRlci5MaW5lcxkuQXNwZWN0LkNvbG9yUGFsZXR0ZUluZGV4Di5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchkuU2VyaWVzLjAuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4wLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoFC5TZXJpZXMuMC5EYXRhU291cmNlDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4xFS5TZXJpZXMuMS5CcnVzaC5Db2xvchkuU2VyaWVzLjEuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMS5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4xLlBvaW50ZXIuU2l6ZVVuaXRzFy5TZXJpZXMuMS5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMS5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMS5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMS5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4xLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4xLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4xLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4xLllWYWx1ZXMuQ291bnQcLlNlcmllcy4xLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjEuQ29sb3JFYWNoFC5TZXJpZXMuMS5EYXRhU291cmNlDy5TZXJpZXMuMS5Db2xvcg8uU2VyaWVzLjEuVGl0bGUdLlNlcmllcy4xLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4xLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMS5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zCFNlcmllcy4yFS5TZXJpZXMuMi5CcnVzaC5Db2xvchkuU2VyaWVzLjIuUG9pbnRlci5WaXNpYmxlHC5TZXJpZXMuMi5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4yLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMi5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMi5MaW5lUGVuLkNvbG9yFy5TZXJpZXMuMi5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMi5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMi5YVmFsdWVzLkRhdGFNZW1iZXIaLlNlcmllcy4yLlhWYWx1ZXMuRGF0ZVRpbWUXLlNlcmllcy4yLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4yLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4yLllWYWx1ZXMuQ291bnQcLlNlcmllcy4yLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjIuQ29sb3JFYWNoFC5TZXJpZXMuMi5EYXRhU291cmNlDy5TZXJpZXMuMi5Db2xvcg8uU2VyaWVzLjIuVGl0bGUdLlNlcmllcy4yLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4yLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMi5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGC5BeGVzLkxlZnQuVGl0bGUuQ2FwdGlvbhYuQXhlcy5MZWZ0LlRpdGxlLkxpbmVzIy5BeGVzLkJvdHRvbS5MYWJlbHMuUm91bmRGaXJzdExhYmVsIi5BeGVzLkJvdHRvbS5MYWJlbHMuRGF0ZVRpbWVGb3JtYXQfLkF4ZXMuQm90dG9tLkxhYmVscy5WYWx1ZUZvcm1hdBYuQXhlcy5Cb3R0b20uSW5jcmVtZW50Gi5BeGVzLkJvdHRvbS5UaXRsZS5DYXB0aW9uGC5BeGVzLkJvdHRvbS5UaXRsZS5MaW5lcw8uQXhlcy5BdXRvbWF0aWMABAAAAAYAAAEEAAAEBAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQHAAEABAcAAQABBAEAAAAABAQBBAAABAQEBwABAAQHAAEAAQQBAAAAAAQEAQYAAQEAAQYAARlTdGVlbWEuVGVlQ2hhcnQuVGhlbWVUeXBlAgAAAAEICAgBFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAEGJ1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuUG9pbnRlclNpemVVbml0cwIAAAAUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAYIASVTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlZhbHVlTGlzdE9yZGVyAgAAAAYIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABCwYGJFN0ZWVtYS5UZWVDaGFydC5TdHlsZXMuVGFpbEFsaWdubWVudAIAAAAVU3lzdGVtLkRyYXdpbmcuUG9pbnRGAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAABRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAAGCAElU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAABBgECAAAAAAX8////GVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAAcAAAACAAAACQUAAAAAAAAAAAYGAAAAG1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuTGluZQX5////FFN5c3RlbS5EcmF3aW5nLkNvbG9yBAAAAARuYW1lBXZhbHVlCmtub3duQ29sb3IFc3RhdGUBAAAACQcHAwAAAAoAAP//AAAAAAAAAgABAAAAAAAAAAAF+P///ydTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMBAAAAB3ZhbHVlX18ACAIAAAAAAAAAAff////5////CgAA//8AAAAAAAACAAH2////+f///woAAJn/AAAAAAAAAgAJCwAAAAUAAAAGDAAAAAxwcm9jZXNzX2RhdGUBBfP///8lU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgEAAAAHdmFsdWVfXwAIAgAAAAEAAAAJDgAAAAUAAAAGDwAAAAROUkNRAAYQAAAADlNxbERhdGFTb3VyY2UxAe/////5////CgAA//8AAAAAAAACAAYSAAAABE5SQ1EAAAAAAAAAAAAAACBAAAAAAAAAIEAF7f///yRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQBAAAAB3ZhbHVlX18ACAIAAAAAAAAABez///8VU3lzdGVtLkRyYXdpbmcuUG9pbnRGAgAAAAF4AXkAAAsLAwAAAAAAAAAAAAAACQYAAAAB6v////n///8KAIAA/wAAAAAAAAIAAQAAAAAAAAAAAen////4////AAAAAAHo////+f///woATQD/AAAAAAAAAgAJGQAAAAUAAAAJDAAAAAEB5f////P///8BAAAACRwAAAAFAAAABh0AAAAESElDUQAJEAAAAAHh////+f///woAgAD/AAAAAAAAAgAGIAAAAARISUNRAAAAAAAAAAAAAAAUQAAAAAAAACBAAd/////t////AAAAAAHe////7P///wAAAAAAAAAACQYAAAAB3P////n///8KAEDA/wAAAAAAAAIAAQAAAAAAAAAAAdv////4////AAAAAAHa////+f///woAQMD/AAAAAAAAAgAB2f////n///8KAJmZ/wAAAAAAAAIACSgAAAAFAAAACQwAAAABAdb////z////AQAAAAkrAAAABQAAAAYsAAAABFZIQ1EACRAAAAAB0v////n///8KAEDA/wAAAAAAAAIABi8AAAAEVkhDUQAAAAAAAAAAAAAAFEAAAAAAAAAgQAHQ////7f///wAAAAABz////+z///8AAAAAAAAAAAYyAAAABumHjemHjwkzAAAAAAY0AAAAA01NTQY1AAAAAyMjIwAAAAAAAD5ABjYAAAAG5pmC6ZaTCTcAAAABEQUAAAABAAAABjgAAAAEQ29kZQ8LAAAABQAAAAYAAAAAQMLlQAAAAAAgxuVAAAAAAKDJ5UAAAAAAgM3lQAAAAABA0eVADw4AAAAFAAAABuxRuB6Ff3ZAexSuRxF420CF61G4bsrXQD0K16MQkdNApHA9CsdS3kAPGQAAAAUAAAAGAAAAAEDC5UAAAAAAIMblQAAAAACgyeVAAAAAAIDN5UAAAAAAQNHlQA8cAAAABQAAAAaF61G4Hh6VQKRwPQq3XtxAj8L1KCTq4kB7FK5H4QvlQOF6FK7HwNdADygAAAAFAAAABgAAAABAwuVAAAAAACDG5UAAAAAAoMnlQAAAAACAzeVAAAAAAEDR5UAPKwAAAAUAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABEzAAAAAQAAAAY5AAAABumHjemHjxE3AAAAAQAAAAY6AAAABuaZgumWkws=" CssClass="auto-style1"  GetChartFile="GetChart.aspx" Height="300px" LastFileName="" Width="900px" AutoPostback="False" EnableTheming="False"  ViewStateMode="Disabled" DataSourceID="SqlDataSource1"  />
             
                                </td>
                            </tr>
                    </table>                                     
          
          <%--  <tr>
                <td>
                    <asp:Button ID="btnUp" runat="server" OnClick="btnUp_Click" Text="前一月" />
                    <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="後一月" />
                    <input id="Radio1" onclick="create_data(1)" type="radio" />thickness&nbsp;
                    <input id="Radio2" onclick="create_data(2)" type="radio" />width&nbsp;
                    <input id="Radio3" onclick="create_data(3)" type="radio" />strength&nbsp;
                    <input id="Radio4" onclick="create_data(4)" type="radio" />code</td>
            </tr>--%>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="select
ETNG.process_date,
isnull(ETNG.total_prod,0) as ETNG,
isnull(WTNG.total_prod,0) as WTNG,
isnull(NTNG.total_prod,0) as NTNG,
isnull(NTCG.total_prod,0) as NTCG,
isnull(ETCG.total_prod,0) as ETCG,
round(isnull(PA.total_prod-ETNG.total_prod-WTNG.total_prod-NTNG.total_prod-NTCG.total_prod-ETCG.total_prod,0),2) as MDSZ,
isnull(NRWD.total_prod,0) as NRWD,
isnull(MDWD.total_prod,0) as MDWD,
isnull(WIWD.total_prod,0) as WIWD,
isnull(EXLC.total_prod,0) as EXLC,
isnull(LSCS.total_prod,0) as LSCS,
isnull(MSCS.total_prod,0) as MSCS,
isnull(HICS.total_prod,0) as HICS,
isnull(VHIS.total_prod,0) as VHIS,
isnull(SUS.total_prod,0) as SUS,
isnull(NRCQ.total_prod,0) as NRCQ,
isnull(HICQ.total_prod,0) as HICQ,
isnull(VHCQ.total_prod,0) as VHCQ
from(
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &lt;= 1260 and avg_thickness &lt;= 1500 
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &lt;= 1260 and coil_thickness &lt;= 1500 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date ) 
as ETNG 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &gt;= 1500 and avg_thickness &lt;= 2300 
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &gt;= 1500 and coil_thickness &lt;= 2300 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date) 
as WTNG on ETNG.process_date=WTNG.process_date
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &gt; 1260 and avg_width &lt; 1500 
and avg_thickness &gt;= 1500 and avg_thickness &lt;= 1900
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &gt; 1260 and coil_width &lt; 1500 
and coil_thickness &gt;= 1500 and coil_thickness &lt;= 1900
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date )
as NTNG on ETNG.process_date=NTNG.process_date 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_thickness &gt;= 6000 and avg_thickness &lt;= 9900 
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_thickness &gt;= 6000 and coil_thickness &lt;= 9900
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date)
as NTCG on ETNG.process_date=NTCG.process_date 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_thickness &gt; 9900
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_thickness &gt; 9900 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date)
as ETCG on ETNG.process_date=ETCG.process_date 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date )
as PA on ETNG.process_date=PA.process_date
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &lt;= 950 
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &lt;= 950 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date )
as NRWD on ETNG.process_date=NRWD.process_date 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &gt;= 950 and avg_width &lt;1550
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &gt;= 950 and coil_width &lt; 1550 
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date )
as MDWD on ETNG.process_date=MDWD.process_date 
left join (
select 
dateadd(m, datediff(m,0,A.process_date),0) as process_date, 
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and avg_width &gt;= 1550
group by  dateadd(m, datediff(m,0,process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,process_date),0) as process_date,
cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock)
where process_date between DATEADD(year,-1,getdate()) and getdate() 
and coil_width &gt;= 1550
group by  dateadd(m, datediff(m,0,process_date),0) ) as B ON A.process_date = B.process_date and A.process_date = B.process_date)
as WIWD on ETNG.process_date=WIWD.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &lt;= 100 
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &lt;= 100
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
)
as EXLC on ETNG.process_date=EXLC.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 40
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 40
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as LSCS on ETNG.process_date=LSCS.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 50 and wh71.tensile &gt; 40 
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 50 and wh71.tensile &gt; 40 
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as MSCS on ETNG.process_date=MSCS.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 60 and wh71.tensile &gt; 50 
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &lt;= 60 and wh71.tensile &gt; 50 
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as HICS on ETNG.process_date=HICS.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &gt; 60
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &gt; 100 and wh71.tensile &gt; 60
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as VHIS on ETNG.process_date=VHIS.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.carbon &gt; 100 and wh71.steel_grade_code like '6%'
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.carbon &gt; 100 and wh71.steel_grade_code like '6%'
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as SUS on ETNG.process_date=SUS.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.inspection_code &lt; '6000' and wh71.inspection_code &gt;= '5000'
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.inspection_code &lt; '6000' and wh71.inspection_code &gt;= '5000'
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
)
as NRCQ on ETNG.process_date=NRCQ.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.inspection_code &lt; '5000' and wh71.inspection_code &gt;= '4000'
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.inspection_code &lt; '5000' and wh71.inspection_code &gt;= '4000'
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
 )
as HICQ on ETNG.process_date=HICQ.process_date 
left join (
select dateadd(m, datediff(m,0,A.process_date),0) as process_date,
round(ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0),2) as total_prod 
from (
select dateadd(m, datediff(m,0,wh73.process_date),0) as process_date,
cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73  as wh73 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() 
and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no and wh71.inspection_code &lt; '4000' and wh71.inspection_code &gt;= '2000' 
group by  dateadd(m, datediff(m,0,wh73.process_date),0)) as A 
FULL OUTER JOIN (
select dateadd(m, datediff(m,0,wh76.process_date),0) as process_date, 
cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76  as wh76 with(nolock), h_pmis_wh71  as wh71 with(nolock)
where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() 
and wh76.coil_no = wh71.coil_no and wh71.inspection_code &lt; '4000' and wh71.inspection_code &gt;= '2000' 
group by  dateadd(m, datediff(m,0,wh76.process_date),0)) as B ON A.process_date = B.process_date and A.process_date = B.process_date
)
as VHCQ on ETNG.process_date=VHCQ.process_date 
order by ETNG.process_date  ">
                        </asp:SqlDataSource>
        
     
    
    </div>
   
    </form>
</body>
</html>


