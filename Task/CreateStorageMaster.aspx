<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateStorageMaster.aspx.cs" Inherits="WebApplication2.CreateStorageMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
   <table>
       <tr>
     <td style="vertical-align:top">
         <asp:Panel ID="Panel1" runat="server" >
             <p>
                 填寫儲位基本資料</p>
                    
             <p>
                 儲位區域：<asp:TextBox ID="TB_storage_area" runat="server"></asp:TextBox>
                 <asp:TextBox ID="TB_storage_code" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
             </p>
             <p>
                 儲位大小：<asp:TextBox ID="TB_storage_size" runat="server"></asp:TextBox>
             </p>
              <p>
                 備註：<asp:TextBox ID="TB_remark" runat="server" Height="137px" TextMode="MultiLine" Width="340px"></asp:TextBox>
             </p>
             <p>
                 <asp:Button ID="BT_Send" runat="server" OnClick="BT_Send_Click" Text="新增" />
                 <asp:Button ID="BT_Modify" runat="server"  Text="修改" OnClick="BT_Modify_Click" />
             </p>
             <p>
                 <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                 <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>
             </p>
       
      </asp:Panel>
    </td>
        <td style="vertical-align:top">
               <p>
                 儲位區域查詢:
                 <asp:TextBox ID="Sel_storage_area" runat="server"></asp:TextBox>
                 備註查詢<asp:TextBox ID="Sel_remark" runat="server"></asp:TextBox>
                                  <asp:Button ID="BT_select" runat="server"  Text="查詢" OnClick="BT_select_Click" />
                 
             </p>
               <asp:Panel ID="Panel2" runat="server">
    <asp:SqlDataSource ID="SqlDataSource_storage_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [storage_code], [storage_area], [storage_size], [remark], [time_stamp] FROM [storage_master] order by [storage_code] desc">
    </asp:SqlDataSource>
             <asp:GridView ID="GridView_storage_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="storage_code" DataSourceID="SqlDataSource_storage_master" ForeColor="#333333" GridLines="None" PageSize="50">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                <asp:TemplateField>

                 <ItemTemplate>
      <asp:Button ID="BT_modi" runat="server"  Text="修改" 
                    OnClick="BT_modiClick" />
    </ItemTemplate>
                      
            </asp:TemplateField>
            <asp:BoundField DataField="storage_code" HeaderText="儲位代碼" InsertVisible="False" ReadOnly="True" SortExpression="storage_code" />
            <asp:BoundField DataField="storage_area" HeaderText="儲位區域" SortExpression="storage_area" />
            <asp:BoundField DataField="storage_size" HeaderText="儲位大小" SortExpression="storage_size" />
            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark" />
            <asp:BoundField DataField="time_stamp" HeaderText="資料異動時間" SortExpression="time_stamp" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>


   
   </asp:Panel>
             </td>
           </tr>
       </table>
</asp:Content>
