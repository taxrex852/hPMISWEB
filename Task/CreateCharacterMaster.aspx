<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCharacterMaster.aspx.cs" Inherits="WebApplication2.CreateCharacterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td style="vertical-align: top">
                <asp:Panel ID="Panel1" runat="server">

                            <p>
                    專用系統名稱：<asp:DropDownList ID="DL_System" runat="server" DataSourceID="SqlDataSource_PageLoad_SystemList" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DL_System_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT [SystemID], [SystemName] FROM [SystemList]"></asp:SqlDataSource>
                </p>
                    <p>
                        Y6P2設備編碼(EX:HBMMOBTMILHM01)：<asp:TextBox ID="TB_y6p2_device_code" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TB_character_serial" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                    </p>
                
                    <p>
                        ERP設備編碼(EX:5A1XB1P7)：<asp:TextBox ID="TB_iso_decive_code" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        設備名稱(ex:HBM/MIL-DEV01)：<asp:TextBox ID="TB_iso_device_name" runat="server"></asp:TextBox>
                    </p>

                    <p>
                        防毒軟體安裝與否：<asp:DropDownList ID="DL_isantivirus_Install" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0">N</asp:ListItem>
                            <asp:ListItem Value="1">Y</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        防毒軟體未裝原因：<asp:TextBox ID="TB_notinstall_antivirus_reason" runat="server" Width="500"></asp:TextBox>
                    </p>
                    <p>
                        USB是否啟用：<asp:DropDownList ID="DL_isusbport_enable" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0">N</asp:ListItem>
                            <asp:ListItem Value="1">Y</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        USB啟用原因：<asp:TextBox ID="TB_usbport_enable_reason" runat="server" Width="500"></asp:TextBox>
                    </p>
                    <p>
                        IP位置1：<asp:TextBox ID="TB_Ip_address1" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        IP位置2：<asp:TextBox ID="TB_Ip_address2" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        IP位置3：<asp:TextBox ID="TB_Ip_address3" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        IP位置4：<asp:TextBox ID="TB_Ip_address4" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        是否列入DT管理(電腦設備才需要)：<asp:DropDownList ID="DL_ISERP_DTList" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0">N</asp:ListItem>
                            <asp:ListItem Value="1">Y</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                

                    <p>
                        備註：<asp:TextBox ID="TB_remark" runat="server" Height="137px" TextMode="MultiLine" Width="340px"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Button ID="BT_Send" runat="server"  Text="新增" OnClick="BT_Send_Click" />
                        <asp:Button ID="BT_Modify" runat="server" Text="修改" OnClick="BT_Modify_Click"  />
                    </p>
                    <p>
                        <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                        <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand=""></asp:SqlDataSource>
                    </p>

                </asp:Panel>
            </td>









            <td style="vertical-align:top">


                <asp:Panel ID="Panel2" runat="server">
              <%--      <p>
                        物料種類查詢:
                 <asp:TextBox ID="Sel_material_type" runat="server"></asp:TextBox>
                        設備名稱查詢<asp:TextBox ID="Sel_material_name" runat="server"></asp:TextBox>
                        <asp:Button ID="BT_select" runat="server" Text="查詢" OnClick="BT_select_Click" />

                    </p>--%>
                    <asp:SqlDataSource ID="SqlDataSource_character_master" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    <asp:GridView ID="GridView_character_master" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_character_master" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                              <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BT_modi" runat="server" Text="修改"
                                    OnClick="BT_modiClick" />
                           
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                            <asp:BoundField DataField="character_serial" HeaderText="角色序號" InsertVisible="False" SortExpression="character_serial">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="system_name" HeaderText="系統名稱" SortExpression="system_name" />
                            <asp:BoundField DataField="y6p2_device_code" HeaderText="Y6P2設備編碼" SortExpression="y6p2_device_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iso_device_code" HeaderText="ERP設備編碼" SortExpression="iso_device_code">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iso_device_name" HeaderText="設備名稱" SortExpression="iso_device_name">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="isantivirus_install" HeaderText="防毒軟體是否安裝" SortExpression="isantivirus_install">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="notinstall_antivirus_reason" HeaderText="防毒軟體未安裝原因" SortExpression="notinstall_antivirus_reason">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="isusbport_enable" HeaderText="USB是否啟用" SortExpression="isusbport_enable">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usbport_enable_reason" HeaderText="USB啟用原因" SortExpression="usbport_enable_reason">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ip_address1" HeaderText="IP位置1" SortExpression="ip_address1">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ip_address2" HeaderText="IP位置2" SortExpression="ip_address2">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ip_address3" HeaderText="IP位置3" SortExpression="ip_address3">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ip_address4" HeaderText="IP位置4" SortExpression="ip_address4">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="iserp_dtlist" HeaderText="是否列入DT管理" SortExpression="iserp_dtlist">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="remark" HeaderText="備註" SortExpression="remark">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="time_stamp" HeaderText="資料異動日期" SortExpression="time_stamp">
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
