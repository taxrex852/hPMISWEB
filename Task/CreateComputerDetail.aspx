<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateComputerDetail.aspx.cs" Inherits="WebApplication2.CreateComputerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <td style="vertical-align: top">
            <asp:Panel ID="Panel1" runat="server">
                <p>
                    填寫電腦詳細資料
                </p>
                <p>
                    物料序號：<asp:TextBox ID="TB_material_serial" runat="server" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="TB_computer_serial" runat="server" Visible="False"></asp:TextBox>
                </p>
                <%--<p>
                    電腦廠牌型號：<asp:TextBox ID="TB_product_name" runat="server" ></asp:TextBox>

                
                </p>--%>

                <p>
                    CPU規格：<asp:TextBox ID="TB_cpu_spec" runat="server"></asp:TextBox>
                </p>
                <p>
                    記憶體規格：<asp:TextBox ID="TB_ram_size" runat="server"></asp:TextBox>
                </p>

                <p>
                    HDD大小：<asp:TextBox ID="TB_hdd_size" runat="server"></asp:TextBox>
                </p>
                <p>
                    DVD規格：<asp:TextBox ID="TB_dvd_type" runat="server"></asp:TextBox>
                </p>
                <p>
                    隨機版作業系統：<asp:DropDownList ID="DL_oem_os" runat="server">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>Windows XP</asp:ListItem>
                        <asp:ListItem>Windows Vista</asp:ListItem>
                        <asp:ListItem>Windows 7</asp:ListItem>
                        <asp:ListItem>Windows 10</asp:ListItem>
                        <asp:ListItem>Windows 11</asp:ListItem>
                        <asp:ListItem>Windows Server 2003</asp:ListItem>
                        <asp:ListItem>Windows Server 2003 R2</asp:ListItem>
                        <asp:ListItem>Windows Server 2008</asp:ListItem>
                        <asp:ListItem>Windows Server 2008 R2</asp:ListItem>
                        <asp:ListItem>Windows Server 2012</asp:ListItem>
                        <asp:ListItem>Windows Server 2012 DC</asp:ListItem>
                        <asp:ListItem>Windows Server 2016</asp:ListItem>
                        <asp:ListItem>Windows Server 2019</asp:ListItem>
                        <asp:ListItem>RHEL 5.2</asp:ListItem>
                        <asp:ListItem>RHEL 5.3</asp:ListItem>
                    </asp:DropDownList>
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
                <p>
                        設備大項名稱查詢:
                 <asp:TextBox ID="Sel_material_name" runat="server"></asp:TextBox>
                                             
                         儲位： 
                     <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_storage_code_SelectedIndexChanged1">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_id" AutoPostBack="True" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
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
storage.*

from material_master
left join material_detail on material_master.material_code=material_detail.material_code 
left join 
(select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id
from storage_master 
left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage 
on material_detail.storage_serial = storage.storage_serial
where iscomputer ='C'"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_computer_detail" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_computer_detail" ForeColor="#333333" GridLines="None" PageSize="50">
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
                        <asp:BoundField DataField="computer_serial" HeaderText="電腦序號" InsertVisible="False" ReadOnly="True" SortExpression="computer_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                   <%--     <asp:BoundField DataField="product_name" HeaderText="廠牌型號" SortExpression="product_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="cpu_spec" HeaderText="CPU規格" SortExpression="cpu_spec">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ram_size" HeaderText="記憶體規格" SortExpression="ram_size">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hdd_size" HeaderText="HDD大小" SortExpression="hdd_size">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dvd_type" HeaderText="DVD規格" SortExpression="dvd_type">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oem_os" HeaderText="隨機版作業系統" SortExpression="oem_os">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="time_stamp" HeaderText="資料異動時間" SortExpression="time_stamp">
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
                <asp:GridView ID="GridView_computer_detail" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_master" ForeColor="#333333" GridLines="None" PageSize="50">
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
                        <asp:BoundField DataField="material_name" HeaderText="設備大項名稱" SortExpression="material_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_code" HeaderText="儲位代碼" InsertVisible="False" ReadOnly="True" SortExpression="storage_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_serial" HeaderText="儲位序號" InsertVisible="False" ReadOnly="True" SortExpression="storage_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_area" HeaderText="儲位區域" SortExpression="storage_area">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_name" HeaderText="儲位名稱" SortExpression="storage_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="storage_id" HeaderText="儲位編號" SortExpression="storage_id">
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
