<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ERPMaterialReport.aspx.cs" Inherits="WebApplication2.ERPMaterialReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p>

&nbsp;<asp:Button ID="BT_Print" runat="server" Text="列印" OnClick="BT_Print_Click"  />
    
    &nbsp;</p>
    <p>
    <asp:GridView ID="GridView_ERPMaterialReport"  runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_ERPMaterialReport" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White"  />
        <Columns>
            <asp:BoundField DataField="material_name" HeaderText="物料名稱" SortExpression="material_name" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="storage_id" HeaderText="儲位" SortExpression="storage_id" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="material_counter" HeaderText="儲位數量" SortExpression="material_counter" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="safety_quantity" HeaderText="安全數量" SortExpression="safety_quantity">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="material_unit" HeaderText="計量單位" SortExpression="material_unit">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="material_ERP_no" HeaderText="ERP固定資產號碼" SortExpression="material_ERP_no" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#2461BF"  />
   
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"  />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB"  />
        <SortedAscendingHeaderStyle BackColor="#6D95E1"  />
        <SortedDescendingCellStyle BackColor="#E9EBEF"  />
        <SortedDescendingHeaderStyle BackColor="#4870BE"  />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource_ERPMaterialReport" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select Mat_Master.material_name,Mat_Master.safety_quantity,Mat_Master.material_unit,Mat_Master.material_ERP_no,Sto_Info.storage_id, 
                               count(Mat_Master.material_name) As material_counter 
                               From [dbo].[material_master]  As Mat_Master  
                               Left Join [dbo].[material_detail]  As Mat_Detail　On Mat_Detail.material_code = Mat_Master.material_code 
                               Left Join [dbo].[storage_detail]  As Sto_Info 　　On Mat_Detail.storage_serial  = Sto_info.storage_serial 
                               where Mat_Detail.material_serial &lt;&gt; '' and  Mat_Master.material_ERP_no &lt;&gt; '' and Sto_info.storage_code = '2' 
                               Group by Mat_Master.material_name, Mat_Master.safety_quantity, Mat_Master.material_unit,Mat_Master.material_ERP_no, Sto_Info.storage_id 
                               order by Sto_Info.storage_id ">
    </asp:SqlDataSource>
</p>
  
          

    </asp:Content>
