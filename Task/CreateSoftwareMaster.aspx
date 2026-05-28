<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateSoftwareMaster.aspx.cs" Inherits="WebApplication2.CreateSoftwareMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td style="vertical-align:top">
                <asp:Panel ID="Panel1" runat="server">


                    <p>
                        軟體名稱：<asp:TextBox ID="TB_software_name" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TB_software_code" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                    </p>
                    <p>
                        軟體製造商：<asp:TextBox ID="TB_software_manufacturer" runat="server"></asp:TextBox>
           
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
                        軟體名稱查詢:
                 <asp:TextBox ID="Sel_software_name" runat="server"></asp:TextBox>
                        軟體製造商查詢<asp:TextBox ID="Sel_software_manufacturer" runat="server"></asp:TextBox>
                        <asp:Button ID="BT_select" runat="server" Text="查詢" OnClick="BT_select_Click" />

                    </p>
                    <asp:SqlDataSource ID="SqlDataSource_software_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [software_master] ORDER BY [software_code] DESC"></asp:SqlDataSource>
                    <asp:GridView ID="GridView_software_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="software_code" DataSourceID="SqlDataSource_software_master" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                              <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:Button ID="BT_modi" runat="server" Text="修改"
                                        OnClick="BT_modiClick" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="software_code" HeaderText="軟體代碼" InsertVisible="False" ReadOnly="True" SortExpression="software_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="software_name" HeaderText="軟體名稱" SortExpression="software_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="software_manufacturer" HeaderText="軟體製造商" SortExpression="software_manufacturer">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
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
