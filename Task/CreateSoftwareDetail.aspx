<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateSoftwareDetail.aspx.cs" Inherits="WebApplication2.CreateSoftwareDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <td width="30%" style="vertical-align:top">
            <asp:Panel ID="Panel1" runat="server">
                <p>
                    填寫軟體詳細資料
                </p>
                <p>
                    軟體代碼：<asp:TextBox ID="TB_software_code" runat="server" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="TB_software_serial" runat="server" Visible="False"></asp:TextBox>
                </p>
                <p>
                    軟體名稱：<asp:TextBox ID="TB_software_name" runat="server" ReadOnly="True"></asp:TextBox>

                </p>
                <p>
                    軟體製造商：<asp:TextBox ID="TB_software_manufacturer" runat="server" ReadOnly="True"></asp:TextBox>
                </p>
                <p>
                    授權序號：<asp:TextBox ID="TB_license_number" runat="server"></asp:TextBox>
                </p>
                  <p>
                    儲位： 
                    <asp:DropDownList ID="DL_storage_code" runat="server" DataSourceID="SqlDataSource_DL_storage_code" DataTextField="storage_area" DataValueField="storage_code" AppendDataBoundItems="True" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DL_storage_serial" runat="server" DataSourceID="SqlDataSource_DL_storage_serial" DataTextField="storage_id" DataValueField="storage_serial" AutoPostBack="True"></asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSource_DL_storage_code" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_code], [storage_area] FROM [storage_master] where storage_area= 'SOFTWARE'"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource_DL_storage_serial" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [storage_serial], [storage_id] FROM [storage_detail]  where storage_code=4 order by storage_serial "></asp:SqlDataSource>
                    
                </p>
                <p>
                    保管人：<asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
                    <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
                </p>
                <p>
                    軟體授權起算日期：<asp:TextBox ID="TB_license_date_start" runat="server" TextMode="Date"></asp:TextBox>
                </p>
                <p>
                    軟體授權截止日期：<asp:TextBox ID="TB_license_date_end" runat="server" TextMode="Date"></asp:TextBox>
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
                <asp:SqlDataSource ID="SqlDataSource_software_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [software_master] ORDER BY [software_code] DESC"></asp:SqlDataSource>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:SqlDataSource ID="SqlDataSource_software_detail" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    <asp:GridView ID="GridView_software_detail" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="software_serial" DataSourceID="SqlDataSource_software_detail" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                               <asp:TemplateField>

                                <ItemTemplate>
                                    <asp:Button ID="BT_modi" runat="server" Text="修改"
                                        OnClick="BT_modiClick" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:BoundField DataField="software_code" HeaderText="軟體代碼" SortExpression="software_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="software_serial" HeaderText="軟體流水號" InsertVisible="False" ReadOnly="True" SortExpression="software_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="license_number" HeaderText="授權序號" SortExpression="license_number">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                               <asp:BoundField DataField="storage_serial" HeaderText="儲位序號" SortExpression="storage_serial">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                               </asp:BoundField>
                               <asp:BoundField DataField="storage_id" HeaderText="儲位編號" SortExpression="storage_id">
                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                               </asp:BoundField>
                            <asp:BoundField DataField="employee_id" HeaderText="保管人" SortExpression="employee_id">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="license_date_start" DataFormatString="{0:yyyy-MM-dd}" HeaderText="軟體授權起算日期" SortExpression="license_date_start">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="license_date_end" DataFormatString="{0:yyyy-MM-dd}" HeaderText="軟體授權截止日期" SortExpression="license_date_end">
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
                    <asp:GridView ID="GridView_software_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="software_code" DataSourceID="SqlDataSource_software_master" ForeColor="#333333" GridLines="None" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="BT_queryMaster" runat="server" OnClick="BT_queryMasterClick" Text="帶入基本資料" />
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



            </asp:Panel>
        </td>

    </table>
</asp:Content>
