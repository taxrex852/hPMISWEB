<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateMaterialMaster.aspx.cs" Inherits="WebApplication2.CreateMaterialMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td style="vertical-align:top">
                <asp:Panel ID="Panel1" runat="server">


                    <p>
                        物料種類：<asp:DropDownList ID="DL_material_type" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceMaterialtype" DataTextField="material_type" DataValueField="material_type" OnSelectedIndexChanged="DL_material_type_SelectedIndexChanged"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceMaterialtype" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select material_type from material_master group by material_type union select ''  order by material_type"></asp:SqlDataSource>              
                        <asp:TextBox ID="TB_material_type" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TB_material_code" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                    </p>
                    <p>
                        設備大項名稱：<asp:TextBox ID="TB_material_name" runat="server" Width="500px"></asp:TextBox>
                    </p>
                    <p>
                        物料單位：
                  <asp:DropDownList ID="DL_material_unit" runat="server" AutoPostBack="True">
                      <asp:ListItem Selected="True"></asp:ListItem>
                      <asp:ListItem>支</asp:ListItem>
                      <asp:ListItem>片</asp:ListItem>
                      <asp:ListItem>台</asp:ListItem>
                      <asp:ListItem>件</asp:ListItem>
                      <asp:ListItem>式</asp:ListItem>
                      <asp:ListItem>個</asp:ListItem>
                      <asp:ListItem>套</asp:ListItem>
                      <asp:ListItem>座</asp:ListItem>
                      <asp:ListItem>隻</asp:ListItem>
                      <asp:ListItem>張</asp:ListItem>
                      <asp:ListItem>條</asp:ListItem>
                      <asp:ListItem>組</asp:ListItem>
                      <asp:ListItem>箱</asp:ListItem>
                      <asp:ListItem>顆</asp:ListItem>
                  </asp:DropDownList>
                    </p>
                    <p>
                        安全庫存量：<asp:TextBox ID="TB_safety_quantity" runat="server" ></asp:TextBox>
                    </p>
                    <p>
                        是否為耗材：<asp:DropDownList ID="DL_material_consumable" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True">N</asp:ListItem>
                            <asp:ListItem>Y</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        是否為電腦：<asp:DropDownList ID="DL_iscomputer" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="C">電腦_C</asp:ListItem>
                            <asp:ListItem Value="D">電腦零組件_D</asp:ListItem>
                            <asp:ListItem Value="E" Selected="True">其他設備_E</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        ERP物料編號(EX:61005904052A)：<asp:TextBox ID="TB_material_ERP_no" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        原廠零件號：<asp:TextBox ID="TB_material_part_number" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        詳細規格：<asp:TextBox ID="TB_material_detail_spec" runat="server" Height="137px" TextMode="MultiLine" Width="340px"></asp:TextBox>
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


                <asp:Panel ID="Panel2" runat="server">
                    <p>
                        物料種類查詢:
                 <asp:DropDownList ID="DL_sel_material_type" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceMaterialtype" DataTextField="material_type" DataValueField="material_type" AppendDataBoundItems="True">
                        </asp:DropDownList>
                        
                        設備名稱查詢<asp:TextBox ID="Sel_material_name" runat="server"></asp:TextBox>
                        <asp:Button ID="BT_select" runat="server" Text="查詢" OnClick="BT_select_Click" />

                    </p>
                    <asp:SqlDataSource ID="SqlDataSource_material_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [material_master] ORDER BY [material_code] DESC"></asp:SqlDataSource>
                    <asp:GridView ID="GridView_material_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="material_code" DataSourceID="SqlDataSource_material_master" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:Button ID="BT_modi" runat="server" Text="修改"
                                        OnClick="BT_modiClick" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="material_code" HeaderText="物料代碼" InsertVisible="False" ReadOnly="True" SortExpression="material_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_type" HeaderText="物料種類" SortExpression="material_type">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_name" HeaderText="設備大項名稱" SortExpression="material_name">
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
                            <asp:BoundField DataField="iscomputer" HeaderText="是否為電腦" SortExpression="iscomputer">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_part_number" HeaderText="原廠零件號" SortExpression="material_part_number">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="material_detail_spec" HeaderText="詳細規格" SortExpression="material_detail_spec">
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



                </asp:Panel>
            </td>
        </tr>
    </table>

</asp:Content>
