<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateMaterialDetail.aspx.cs" Inherits="WebApplication2.CreateMaterialDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <td width="30%" style="vertical-align:top">
            <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" >
               
                <p>
                    填寫物料詳細資料
                </p>
                <p>
                   
                     物料代碼：<asp:Label ID="TB_material_code" runat="server" ></asp:Label>
                    <asp:TextBox ID="TB_material_serial" runat="server" Visible="False"></asp:TextBox>
                </p>
                <p>
                    物料總類：<asp:Label ID="TB_material_type" runat="server"  ></asp:Label>

                </p>
                <p>
                    設備大項名稱：<asp:Label ID="TB_material_name" runat="server"   ></asp:Label>
                </p>
                <p>
                    物料單位：<asp:Label ID="TB_material_unit" runat="server"   ></asp:Label>
                </p>
                <p>
                    安全庫存量：<asp:Label ID="TB_safety_quantity" runat="server"   ></asp:Label>
                </p>
                <p>
                    是否為耗材：<asp:Label ID="TB_material_consumable" runat="server" ></asp:Label>
                </p>
          <p>
                    ERP物料編號：<asp:Label ID="TB_material_ERP_no" runat="server"   ></asp:Label>
                </p>
                 </asp:Panel>
                  <asp:Panel ID="Panel4" runat="server">  
                   <p>
                    設備序號SN碼：<asp:TextBox ID="TB_device_serial_no" runat="server"></asp:TextBox>
                </p>
                <p>
                    固定資產編號：<asp:TextBox ID="TB_material_asset_no" runat="server"></asp:TextBox>
                </p>
                    <p>
                    固定資產名稱：<asp:TextBox ID="TB_material_asset_name" runat="server"></asp:TextBox>
                </p>
                <p>
                    儲位： 
                    <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_storage_code_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_serial" AutoPostBack="True"></asp:DropDownList>
                    <asp:TextBox ID="DL_storage_serial_temp" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="DL_storage_code_temp" runat="server" Visible="False"></asp:TextBox>
                </p>
                <p>
                    保管人：<asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
                    <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
                </p>
                  <p>
                    物料取得時間：<asp:TextBox ID="TB_material_getdate" runat="server" ></asp:TextBox>
                </p>
                <p>
                    保固起算日期：<asp:TextBox ID="TB_warranty_date_start" runat="server" TextMode="Date"></asp:TextBox>
                   
                </p>
                <p>
                    保固截止日期：<asp:TextBox ID="TB_warranty_date_end" runat="server" TextMode="Date"></asp:TextBox>
                </p>
                     <p>
                    專用系統名稱：<asp:DropDownList ID="DL_System" runat="server" DataSourceID="SqlDataSource_PageLoad_SystemList" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [SystemID], [SystemName] FROM [SystemList]"></asp:SqlDataSource>
                </p>
              
                <p>
                    成本中心：<asp:TextBox ID="TB_cost_center" runat="server"></asp:TextBox>
                </p>
                <p>
                    請購單號：<asp:TextBox ID="TB_pr_no" runat="server"></asp:TextBox>
                </p>
                <p>
                    採購單號：<asp:TextBox ID="TB_po_no" runat="server"></asp:TextBox>
                </p>
                <p>
                    驗收單號：<asp:TextBox ID="TB_iqc_no" runat="server"></asp:TextBox>
                </p>
            
             
                <p>
                    設備照片：<asp:TextBox ID="TB_device_image" runat="server"></asp:TextBox>
                </p>
           

                <p>
                    備註：<asp:TextBox ID="TB_remark" runat="server" Height="137px" TextMode="MultiLine" Width="340px"></asp:TextBox>
                </p>
                <p>
                    <asp:Button ID="BT_Send" runat="server" OnClick="BT_Send_Click" Text="新增" />
                    <asp:Button ID="BT_Modify" runat="server" Text="修改" OnClick="BT_Modify_Click" />
                </p>
                <p>
                    <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                    <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_code" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_code], [storage_area] FROM [storage_master]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_serial" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_iscomputer" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>
                </p>
             
            </asp:Panel>
        </td>

        <td style="vertical-align:top">
            <asp:Panel ID="Panel2" runat="server">
                <p>
                        物料種類查詢:
                 <asp:DropDownList ID="DL_sel_material_type" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceMaterialtype" DataTextField="material_type" DataValueField="material_type" AppendDataBoundItems="True">
                        </asp:DropDownList>
                        
                        設備名稱查詢<asp:TextBox ID="Sel_material_name" runat="server"></asp:TextBox>
                      儲位： 
                    <asp:DropDownList ID="Sel_Storage_code" runat="server" DataSourceID="SqlDataSource1" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Sel_Storage_code_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="Sel_Storage_serial" runat="server" DataSourceID="SqlDataSource2" DataTextField="storage_id" DataValueField="storage_serial" AutoPostBack="True" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="BT_select" runat="server" Text="查詢" OnClick="BT_select_Click" />
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_code], [storage_area] FROM [storage_master]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_serial], [storage_id] FROM [storage_detail]  WHERE ([storage_code] = @storage_code)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Sel_Storage_code" Name="storage_code" PropertyName="SelectedValue" Type="Int64" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                       
                    <asp:SqlDataSource ID="SqlDataSourceMaterialtype" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select material_type from material_master group by material_type union select ''  order by material_type"></asp:SqlDataSource>              
                    </p>
                <asp:SqlDataSource ID="SqlDataSource_material_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [material_master] ORDER BY [material_code] DESC"></asp:SqlDataSource>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:SqlDataSource ID="SqlDataSource_material_detail" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    <asp:GridView ID="GridView_storage_detail" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="material_serial" DataSourceID="SqlDataSource_material_detail" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:Button ID="BT_modi" runat="server" Text="修改"
                                        OnClick="BT_modiClick" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="material_serial" HeaderText="物料序號" InsertVisible="False" ReadOnly="True" SortExpression="material_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_code" HeaderText="物料代碼" SortExpression="material_code">
                            <itemstyle horizontalalign="Center" verticalalign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_asset_no" HeaderText="資產編號" SortExpression="material_asset_no">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_id" HeaderText="儲位編號" SortExpression="storage_id">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_name" HeaderText="儲位名稱" SortExpression="storage_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="employee_id" HeaderText="保管人" SortExpression="employee_id">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="warranty_date_start" DataFormatString="{0:yyyy-MM-dd}" HeaderText="設備保固起算日期" SortExpression="warranty_date_start">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="warranty_date_end" DataFormatString="{0:yyyy-MM-dd}" HeaderText="設備保固截止日期" SortExpression="warranty_date_end">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cost_center" HeaderText="成本中心" SortExpression="cost_center">
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
                            <asp:BoundField DataField="material_asset_name" HeaderText="ERP固定資產名稱" SortExpression="material_asset_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="device_serial_no" HeaderText="設備序號SN碼" SortExpression="device_serial_no">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="device_image" HeaderText="設備照片" SortExpression="device_image">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="system_name" HeaderText="系統名稱" SortExpression="system_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_getdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="物料取得時間" SortExpression="material_getdate">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_modify_getdate" HeaderText="最後異儲時間" SortExpression="storage_modify_getdate">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="time_stamp" HeaderText="資料異動時間" SortExpression="time_stamp">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_code" HeaderText="儲位代碼" SortExpression="storage_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="storage_serial" HeaderText="儲位序號" SortExpression="storage_serial">
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
                    <asp:GridView ID="GridView_storage_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="material_code" DataSourceID="SqlDataSource_material_master" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="BT_queryMaster" runat="server" OnClick="BT_queryMasterClick" Text="帶入基本資料" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="material_code" HeaderText="物料代碼" InsertVisible="False" ReadOnly="True" SortExpression="material_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_type" HeaderText="物料種類" SortExpression="material_type">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_name" HeaderText="物料大項名稱" SortExpression="material_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_unit" HeaderText="物料單位" SortExpression="material_unit">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="safety_quantity" HeaderText="安全庫存量" SortExpression="safety_quantity">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_consumable" HeaderText="是否為耗材" SortExpression="material_consumable">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_ERP_no" HeaderText="ERP物料編號" SortExpression="material_ERP_no">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="time_stamp" HeaderText="資料異動時間" SortExpression="time_stamp">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iscomputer" HeaderText="是否為電腦" SortExpression="iscomputer">
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



            </asp:Panel>
        </td>

    </table>
</asp:Content>
