<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CharacterMatch.aspx.cs" Inherits="WebApplication2.CharacterMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <td style="vertical-align: top">
            <asp:Panel ID="Panel1" runat="server">
                <p>
                    專用系統名稱：<asp:DropDownList ID="DL_System" runat="server" DataSourceID="SqlDataSource_PageLoad_SystemList" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_System_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [SystemID], [SystemName] FROM [SystemList]"></asp:SqlDataSource>
                    <asp:TextBox ID="TB_character_match_serial" runat="server" Visible="False"></asp:TextBox>
                </p>


                <p>
                    角色配對：<asp:DropDownList ID="DL_Character_master" runat="server" DataSourceID="SqlDataSourceCharacter_master" DataTextField="character_master" DataValueField="character_serial" AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceCharacter_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT character_serial,('Y6P2設備編碼：'+y6p2_device_code+'。設備名稱：'+iso_device_name)as character_master FROM Y6P2_Materials.dbo.character_master where system_name=@system_name">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DL_System" Name="system_name" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </p>




                <p>
                    電腦設備儲位： 
                    <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_storage_code_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_serial" AutoPostBack="True" AppendDataBoundItems="True" OnTextChanged="DL_storage_serial_TextChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="DL_area" runat="server" DataSourceID="SqlDataSource_DL_area" DataTextField="area" DataValueField="material_serial" AppendDataBoundItems="True" AutoPostBack="True" Width="100%">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_code" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT storage.storage_code,storage.storage_area 
from  dbo.material_master left join dbo.material_detail 
on material_master.material_code=material_detail.material_code 
inner join 
(select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code)  as storage 
on material_detail.storage_serial=storage.storage_serial 
where  material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_code,storage.storage_area"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_serial" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource_DL_area" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>

                </p>
                <p>
                    目前角色配對：<asp:TextBox ID="TB_character_serial" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                </p>
                <p>
                    目前電腦配對：<asp:TextBox ID="TB_computer_serial" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                </p>
                <p>
                    目前電腦設備儲位：<asp:TextBox ID="TB_area_temp" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
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


        <td style="vertical-align: top">
            <asp:Panel ID="Panel3" runat="server">
                <asp:SqlDataSource ID="SqlDataSource_computer_character_match" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_computer_character_match" ForeColor="#333333" GridLines="None" PageSize="50">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BT_modi" runat="server" Text="修改"
                                    OnClick="BT_modiClick" />

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:BoundField DataField="character_match_serial" HeaderText="角色配對序號" InsertVisible="False" ReadOnly="True" SortExpression="character_match_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="material_serial" HeaderText="物料序號" SortExpression="material_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="character_serial" HeaderText="角色序號" SortExpression="character_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="y6p2_device_code" HeaderText="Y6P2設備編碼" SortExpression="y6p2_device_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="iso_device_name" HeaderText="設備名稱" SortExpression="iso_device_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="system_name" HeaderText="系統名稱" SortExpression="system_name">
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
                        <asp:BoundField DataField="storage_serial" HeaderText="儲位序號" SortExpression="storage_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_code" HeaderText="儲位代碼" InsertVisible="False" ReadOnly="True" SortExpression="storage_code">
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



            </asp:Panel>
        </td>

    </table>
</asp:Content>


