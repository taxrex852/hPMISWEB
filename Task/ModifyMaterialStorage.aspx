<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation = "false" CodeBehind="ModifyMaterialStorage.aspx.cs" Inherits="WebApplication2.ModifyMaterialStorage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>
                <p>
                    物料異動儲位</p>
                <p>
                    序號：<asp:TextBox ID="Textbox1" runat="server"></asp:TextBox>
                    品名：<asp:TextBox ID="Textbox2" runat="server"></asp:TextBox>
                    儲位：<asp:TextBox ID="Textbox3" runat="server"></asp:TextBox>
                    ERP請購、訂購、驗收：<asp:TextBox ID="Textbox4" runat="server"></asp:TextBox>
                    程控系統名稱：<asp:TextBox ID="Textbox5" runat="server"></asp:TextBox>
                    電腦類：<asp:TextBox ID="Textbox6" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
                       <asp:TextBox ID="TextBoxName1" runat="server" Visible="False"></asp:TextBox>
                    <asp:Button ID="BT_Query" runat="server" Text="查詢" OnClick="BT_Query_Click" />
                    <asp:TextBox ID="TB_material_serial" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TB_material_code" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TB_storage_code" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TB_storage_serial" runat="server" Visible="False"></asp:TextBox>


                    <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                </p>

            </td>
        </tr>
        <tr>
            <td>

                <asp:SqlDataSource ID="SqlDataSource_material_modify" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT           
dbo.material_master.material_code,
dbo.material_detail.material_serial,
dbo.material_master.material_type, 
dbo.material_master.material_name, 
dbo.material_master.material_ERP_no, 
dbo.material_detail.material_asset_no, 
dbo.material_detail.pr_no, 
dbo.material_detail.po_no, 
dbo.material_detail.iqc_no, 
dbo.material_detail.device_serial_no, 
material_detail.remark,
dbo.SystemList.SystemID,
dbo.material_detail.system_name,
dbo.material_detail.storage_serial,
storage.storage_code,
storage.storage_area,
storage.storage_id,
dbo.material_detail.employee_id, 
dbo.employee.employee_name,
dbo.computer_detail.computer_serial, 
dbo.computer_detail.product_name, 
dbo.computer_detail.oem_os
from  
dbo.material_master
left join 
dbo.material_detail on material_master.material_code=material_detail.material_code
left join 
dbo.employee on
material_detail.employee_id=dbo.employee.employee_id
left join 
dbo.SystemList on 
material_detail.system_name=SystemList.SystemName
inner join 
(select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, 
 dbo.storage_detail.storage_id
 from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code)
 as storage on
 material_detail.storage_serial=storage.storage_serial
 left join 
 computer_detail on 
 material_detail.material_serial=computer_detail.material_serial"></asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_DL_storage_code" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_code], [storage_area] FROM [storage_master]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_DL_storage_serial" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>



                <asp:Panel ID="Panel3" runat="server">

                    <asp:GridView ID="GridView_material_modify" runat="server" AllowSorting="false" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_material_modify" ForeColor="#333333" GridLines="None" PageSize="30">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="material_code" HeaderText="物料代碼" InsertVisible="False" ReadOnly="True" SortExpression="material_code">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_serial" HeaderText="物料序號" SortExpression="material_serial" InsertVisible="False" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                               <asp:BoundField DataField="storage_area" HeaderText="目前儲位區域" SortExpression="storage_area">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_id" HeaderText="目前儲位編號" InsertVisible="False" ReadOnly="True" SortExpression="storage_id">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="修改儲位區域">

                                <ItemTemplate>

                                    <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_storage_code_SelectedIndexChanged" Visible="False">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改儲位編號">

                                <ItemTemplate>

                                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_serial" AutoPostBack="True" Visible="False"></asp:DropDownList>


                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="employee_name" HeaderText="目前保管人" SortExpression="employee_name" />
                            <asp:TemplateField HeaderText="修改保管人">

                                <ItemTemplate>
                                    <asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="DL_TaskPeople_SelectedIndexChanged">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>

                                </ItemTemplate>

                            </asp:TemplateField>
                                <asp:BoundField DataField="storage_code" HeaderText="目前儲位代碼" SortExpression="storage_code">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_serial" HeaderText="目前儲位序號" SortExpression="storage_serial">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:Button ID="BT_modi" runat="server" Text="修改"
                                        OnClick="BT_modiClick" />
                                    <asp:Button ID="BT_save" runat="server" Text="儲存"
                                        OnClick="BT_saveClick" Visible="False" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="material_type" HeaderText="物料種類" SortExpression="material_type">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_name" HeaderText="設備大項名稱" SortExpression="material_name">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="material_ERP_no" HeaderText="ERP物料編號" SortExpression="material_ERP_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_asset_no" HeaderText="資產編號" SortExpression="material_asset_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pr_no" HeaderText="請購單號" SortExpression="pr_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="po_no" HeaderText="採購單號" SortExpression="po_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iqc_no" HeaderText="驗收單號" SortExpression="iqc_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="device_serial_no" HeaderText="設備SN碼" SortExpression="device_serial_no">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="system_name" HeaderText="系統名稱" SortExpression="system_name">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                         
                            <asp:BoundField DataField="computer_serial" HeaderText="電腦序號" InsertVisible="False" ReadOnly="True" SortExpression="computer_serial">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="product_name" HeaderText="廠牌/型號" SortExpression="product_name" >
                            <itemstyle horizontalalign="Center" verticalalign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oem_os" HeaderText="隨機版作業系統" SortExpression="oem_os">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        
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
