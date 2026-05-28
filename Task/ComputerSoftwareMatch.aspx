<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ComputerSoftwareMatch.aspx.cs" Inherits="WebApplication2.ComputerSoftwareMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <td style="vertical-align:top">
            <asp:Panel ID="Panel1" runat="server">
                <p>
                    新增電腦授權
                </p>
                <p>
                    物料序號：<asp:TextBox ID="TB_material_serial" runat="server" ReadOnly="True"></asp:TextBox>
                      <asp:TextBox ID="TB_computer_software_match_serial" runat="server" Visible="False"></asp:TextBox>

                </p>
                <p>
                    電腦序號：<asp:TextBox ID="TB_computer_serial" runat="server" ReadOnly="True"></asp:TextBox>

                  
                </p>
                  <p>
                    目前電腦授權：<asp:TextBox ID="TB_Licensed" runat="server" ReadOnly="True"></asp:TextBox>

                  
                </p>

            
                
                   <p class="auto-style1">
                    修改電腦授權： 
                    <asp:DropDownList ID="DL_software_manufacturer" runat="server" DataSourceID="SqlDataSource1" DataTextField="software_manufacturer" DataValueField="software_manufacturer" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_software_manufacturer_SelectedIndexChanged" >
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                       <asp:DropDownList ID="DL_software_name" runat="server" DataSourceID="SqlDataSource2" DataTextField="software" DataValueField="software_serial"  AutoPostBack="True" >
                       
                    </asp:DropDownList>
                   
                   
                       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [software_manufacturer] FROM [software_master]  group by [software_manufacturer] "></asp:SqlDataSource>
                   
                       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                   
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
                </p>

            </asp:Panel>
        </td>


        <td style="vertical-align:top">
            <asp:Panel ID="Panel3" runat="server">
                <p>
                        設備大項名稱查詢:
                 <asp:TextBox ID="Sel_material_name" runat="server"></asp:TextBox>
                        廠牌/型號<asp:TextBox ID="Sel_product_name" runat="server"></asp:TextBox>
                        
                         儲位： 
                    <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_id" AutoPostBack="True"></asp:DropDownList>
                        <asp:Button ID="BT_select" runat="server" Text="查詢" OnClick="BT_select_Click" />
                         <asp:SqlDataSource ID="SqlDataSource_DL_storage_code" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_code], [storage_area] FROM [storage_master]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_serial" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_serial], [storage_id] FROM [storage_detail] WHERE ([storage_code] = @storage_code)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DL_storage_code" Name="storage_code" PropertyName="SelectedValue" Type="Int64" />
                        </SelectParameters>
                        </asp:SqlDataSource>
               
                    </p>
                <asp:SqlDataSource ID="SqlDataSource_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select material_master.material_code,material_master.material_name,
material_detail.material_serial,
storage.storage_area,storage.storage_id,computer_detail.computer_serial,computer_detail.product_name

from material_master
left join material_detail on material_master.material_code=material_detail.material_code 
left join 
(select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id
from storage_master 
left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage 
on material_detail.storage_serial = storage.storage_serial
left join computer_detail on material_detail.material_serial=computer_detail.material_serial
where iscomputer ='C' and computer_serial is not null"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_computer_software_match" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_computer_software_match" ForeColor="#333333" GridLines="None" PageSize="50" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BT_modi" runat="server" Text="修改"
                                    OnClick="BT_modiClick" />
                           
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                         <asp:BoundField DataField="computer_software_match_serial" HeaderText="授權流水號" SortExpression="computer_software_match_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="material_serial" HeaderText="物料序號" InsertVisible="False" ReadOnly="True" SortExpression="material_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="computer_serial" HeaderText="電腦序號" InsertVisible="False" ReadOnly="True" SortExpression="computer_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="software_serial" HeaderText="軟體流水號" SortExpression="software_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product_name" HeaderText="廠牌型號" SortExpression="product_name">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="software_manufacturer" HeaderText="軟體製造商" SortExpression="software_manufacturer">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="software_name" HeaderText="軟體名稱" SortExpression="software_name">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="license_number" HeaderText="授權序號" SortExpression="license_number">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="time_stamp" HeaderText="資料異動時間" SortExpression="time_stamp">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BT_del" runat="server" Text="刪除"
                                    OnClick="BT_delClick" />
                           
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                          
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
                <asp:GridView ID="GridView_computer_detail" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_master" ForeColor="#333333" GridLines="None" PageSize="50" >
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
                        <asp:BoundField DataField="material_serial" HeaderText="物料序號" InsertVisible="False" ReadOnly="True" SortExpression="material_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="computer_serial" HeaderText="電腦流水號" InsertVisible="False" ReadOnly="True" SortExpression="computer_serial">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="material_name" HeaderText="設備大項名稱" SortExpression="material_name">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_area" HeaderText="儲位區域" SortExpression="storage_area">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_id" HeaderText="儲位編號" SortExpression="storage_id">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="product_name" HeaderText="廠牌/型號" InsertVisible="False" ReadOnly="True" SortExpression="product_name">
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

    </table>
</asp:Content>


