<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="disk_report.aspx.vb" Inherits="hPMISWEB.zabbix_report" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register assembly="TeeChart" namespace="Steema.TeeChart.Web" tagprefix="tchart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>disk_report</title>
      <style type="text/css">
     
      
          .bg_skyorange {
              width: 8%;
          }
          
      </style>
</head>

  

<body runat="server">
    <form id="form1" runat="server">
        
       <hPMISWEB:PageHeader ID="ph" runat="server" />
     
     
            <table style="border-width: 3px;border-style:groove; border-collapse: collapse; border-color:black; width: 100%" 
    border="1" id="TblSummary" runat="server" align="left" >
           
                      
                      
         
          
            
            </table>
  
  <tr>
      </tr>
          <tr>
      </tr>
          <tr>
      </tr>
     
        <br />
        <br />
        <br />
      
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
    <Columns>

        <%-- C 槽區塊 --%>
    <%--    <asp:BoundField DataField="C_Used_Percentage" HeaderText="C:使用%" ReadOnly="True" SortExpression="C_Used_Percentage">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>--%>

        <%-- D 槽區塊 --%>
    <%--    <asp:BoundField DataField="D_Used_Percentage" HeaderText="D:使用%" ReadOnly="True" SortExpression="D_Used_Percentage">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>--%>

        <%-- E 槽區塊 --%>
     <%--   <asp:BoundField DataField="E_Used_Percentage" HeaderText="E:使用%" ReadOnly="True" SortExpression="E_Used_Percentage">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>--%>

        <%-- F 槽區塊 --%>
      <%--  <asp:BoundField DataField="F_Used_Percentage" HeaderText="F:使用%" ReadOnly="True" SortExpression="F_Used_Percentage">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>--%>

        <%-- G 槽區塊 --%>
     <%--   <asp:BoundField DataField="G_Used_Percentage" HeaderText="G:使用%" ReadOnly="True" SortExpression="G_Used_Percentage">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>--%>

        <%-- 更新時間 --%>
            <asp:BoundField DataField="IssueGroupTitle" HeaderText="小組" SortExpression="IssueGroupTitle">
            <ItemStyle Width="50px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="SystemName" HeaderText="系統名稱" SortExpression="SystemName">
            <ItemStyle Width="150px" HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:TemplateField HeaderText="更新時間" SortExpression="LastCheckTime">
            <ItemTemplate>
                <asp:Label ID="lblLastCheck" runat="server" Text='<%# Eval("LastCheckTime", "{0:MM-dd HH:mm}") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="C_Total_GB" HeaderText="C:總容" SortExpression="C_Total_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="C_Used_GB" HeaderText="C:使用" SortExpression="C_Used_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="C:剩餘" SortExpression="C_Free_space">
            <ItemTemplate><asp:Label ID="lblCFreeGB" runat="server" Text='<%# Eval("C_Free_space") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Bold="True" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="C:剩餘%" SortExpression="C_Free_Percentage">
            <ItemTemplate><asp:Label ID="lblCFreePercent" runat="server" Text='<%# Eval("C_Free_Percentage") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Italic="True" />
        </asp:TemplateField>

        <asp:BoundField DataField="D_Total_GB" HeaderText="D:總容" SortExpression="D_Total_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="D_Used_GB" HeaderText="D:使用" SortExpression="D_Used_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="D:剩餘" SortExpression="D_Free_space">
            <ItemTemplate><asp:Label ID="lblDFreeGB" runat="server" Text='<%# Eval("D_Free_space") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Bold="True" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="D:剩餘%" SortExpression="D_Free_Percentage">
            <ItemTemplate><asp:Label ID="lblDFreePercent" runat="server" Text='<%# Eval("D_Free_Percentage") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Italic="True" />
        </asp:TemplateField>

        <asp:BoundField DataField="E_Total_GB" HeaderText="E:總容" SortExpression="E_Total_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="E_Used_GB" HeaderText="E:使用" SortExpression="E_Used_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="E:剩餘" SortExpression="E_Free_space">
            <ItemTemplate><asp:Label ID="lblEFreeGB" runat="server" Text='<%# Eval("E_Free_space") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Bold="True" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="E:剩餘%" SortExpression="E_Free_Percentage">
            <ItemTemplate><asp:Label ID="lblEFreePercent" runat="server" Text='<%# Eval("E_Free_Percentage") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Italic="True" />
        </asp:TemplateField>

        <asp:BoundField DataField="F_Total_GB" HeaderText="F:總容" SortExpression="F_Total_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="F_Used_GB" HeaderText="F:使用" SortExpression="F_Used_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="F:剩餘" SortExpression="F_Free_space">
            <ItemTemplate><asp:Label ID="lblFFreeGB" runat="server" Text='<%# Eval("F_Free_space") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Bold="True" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="F:剩餘%" SortExpression="F_Free_Percentage">
            <ItemTemplate><asp:Label ID="lblFFreePercent" runat="server" Text='<%# Eval("F_Free_Percentage") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Italic="True" />
        </asp:TemplateField>

        <asp:BoundField DataField="G_Total_GB" HeaderText="G:總容" SortExpression="G_Total_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="G_Used_GB" HeaderText="G:使用" SortExpression="G_Used_GB">
            <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="G:剩餘" SortExpression="G_Free_space">
            <ItemTemplate><asp:Label ID="lblGFreeGB" runat="server" Text='<%# Eval("G_Free_space") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Bold="True" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G:剩餘%" SortExpression="G_Free_Percentage">
            <ItemTemplate><asp:Label ID="lblGFreePercent" runat="server" Text='<%# Eval("G_Free_Percentage") %>'></asp:Label></ItemTemplate>
            <ItemStyle Width="80px" HorizontalAlign="Right" Font-Italic="True" />
        </asp:TemplateField>

    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="sp_GetZabbixDiskReport" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
  
  
     
    </form>
        
            
  
      
    </body>
</html>
