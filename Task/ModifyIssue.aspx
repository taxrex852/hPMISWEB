<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyIssue.aspx.cs" Inherits="WebApplication2.ModifyIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <td style="vertical-align: top">
            <p>
                編修主題內容<asp:Label ID="LB_TEST" runat="server"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource_PageLoad" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT IssueList.IssueID, IssueList.IssueTitle, CONVERT (VARCHAR(20), IssueList.CreateDate, 23) AS CreateDate, CONVERT (VARCHAR(20), IssueList.FinishDatePre, 23) AS FinishDatePre, CONVERT (VARCHAR(20), IssueList.FinishDateAct, 23) AS FinishDateAct, IssueList.IssueStatus, employee.employee_name, employee.employee_id, IssueList.IssueDescription, IssueList.IssueType, DeapartmentList.DeapartmentName, DeapartmentList.DeapartmentID, IssueList.IssueGroupID FROM IssueList INNER JOIN employee ON IssueList.employee_id = employee.employee_id INNER JOIN DeapartmentList ON IssueList.DeapartmentID = DeapartmentList.DeapartmentID WHERE (IssueList.IssueID = @IssueID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="IssueID" QueryStringField="IssueID" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT IssueID, SystemID FROM IssueSystemMap WHERE (IssueID = @IssueID)">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="IssueID" QueryStringField="IssueID" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource_PageLoad_IssueAssistantMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="  SELECT IssueListAssistantMap.IssueID, 
  IssueListAssistantMap.employee_id, employee.row_id
  FROM IssueListAssistantMap INNER JOIN employee ON IssueListAssistantMap.employee_id = employee.employee_id WHERE (IssueListAssistantMap.IssueID = @IssueID)">
                <SelectParameters>
                    <asp:QueryStringParameter Name="IssueID" QueryStringField="IssueID" Type="Int64" />
                </SelectParameters>
            </asp:SqlDataSource>
            </p>
            <p>
                <asp:Label ID="LB_ID" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
            </p>
            <p>
                主題名稱：<asp:TextBox ID="TB_IssueTitle" runat="server" Width="500px"></asp:TextBox>
            </p>
            <p>
                新建日期：<asp:TextBox ID="TB_CreateDate" runat="server" Width="128px"></asp:TextBox>

                <asp:Button ID="BT_Cal_Sel_CreateDate" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" Visible="true" />

                <asp:Calendar ID="Calendar_CreateDate" OnSelectionChanged="Calendar_CreateDate_Sel" runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </p>
            <p>
                預計結案日期：<asp:TextBox ID="TB_FinishDatePre" runat="server"></asp:TextBox>

                <asp:Button ID="BT_Cal_Sel_FinishDatePre" OnClick="BT_Cal_Sel_FinishDatePre_Click" runat="server" Text="選擇日期" />

                <asp:Calendar ID="Calendar_FinishDatePre" OnSelectionChanged="Calendar_FinishDatePre_Sel" runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </p>
            <p>
                實際結案日期：<asp:TextBox ID="TB_FinishDateAct" runat="server" Enabled="False"></asp:TextBox>

                <asp:Button ID="BT_Cal_Sel_FinishDateAct" OnClick="BT_Cal_Sel_FinishDateAct_Click" runat="server" Text="選擇日期" Visible="false" />

                <asp:Calendar ID="Calendar_FinishDateAct" OnSelectionChanged="Calendar_FinishDateAct_Sel" runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </p>
            <p>
                主題狀態：<asp:DropDownList ID="DL_IssueStatus" runat="server">
                    <asp:ListItem Value="0">0.新建</asp:ListItem>
                    <asp:ListItem Value="1">1.執行中</asp:ListItem>
                    <asp:ListItem Value="2">2.已完成待上線</asp:ListItem>
                    <asp:ListItem Value="3">3.已完成待結案</asp:ListItem>
                    <asp:ListItem Value="4">4.結案</asp:ListItem>
                    <asp:ListItem Value="5">5.刪除</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                主題負責人：<asp:DropDownList ID="DL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19  ORDER BY [employee_id]"></asp:SqlDataSource>
            </p>
              <p>
        主題協作者：
         <asp:CheckBoxList ID="CBL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" RepeatColumns="5">
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19  ORDER BY [employee_id]"></asp:SqlDataSource>
    </p>
            <p>
                相關系統：
            </p>
            <p>
                <asp:CheckBoxList ID="CBL_SystemList" runat="server" DataSourceID="SqlDataSource_SystemList" DataTextField="SystemName" DataValueField="SystemID" RepeatColumns="5">
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [SystemList] ORDER BY [SystemID]"></asp:SqlDataSource>
            </p>


            <p>
                所屬區域：<asp:DropDownList ID="DL_IssueGroup" runat="server" DataSourceID="SqlDataSourceIssueGroup" DataTextField="IssueGroupTitle" DataValueField="IssueGroupID" AppendDataBoundItems="True">
                </asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSourceIssueGroup" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [IssueGroupID], [IssueGroupTitle] FROM [IssueGroup] ORDER BY [IssueGroupID]"></asp:SqlDataSource>

            </p>

            <p>
                主題部門：<asp:DropDownList ID="DL_IssueDeapartment" runat="server" DataSourceID="SqlDataSource_DepartmentList" DataTextField="DeapartmentName" DataValueField="DeapartmentID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource_DepartmentList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [DeapartmentList] ORDER BY [DeapartmentName]"></asp:SqlDataSource>
            </p>
            <p>
                主題描述：<asp:TextBox ID="TB_IssueDescription" runat="server" Height="137px" TextMode="MultiLine" Width="340px"></asp:TextBox>
            </p>
            <p>
                主題類別：<asp:DropDownList ID="DL_IssueType" runat="server" DataSourceID="SqlDataSourceIssueType" DataTextField="IssueTypeTitle" DataValueField="IssueTypeID">
                </asp:DropDownList>
            </p>
            <asp:SqlDataSource ID="SqlDataSourceIssueType" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [IssueTypeID], [IssueTypeTitle] FROM [IssueType] ORDER BY [IssueTypeID]"></asp:SqlDataSource>
            <p>
                <asp:Button ID="BT_Send" runat="server" Text="修改" OnClick="BT_Send_Click" />
            </p>
            <p>
                <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueList]" DataSourceMode="DataReader"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_sendlog" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueList_log]" DataSourceMode="DataReader"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueAssistantMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueListAssistantMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
            </p>
            <p>
                &nbsp;
            </p>
        </td>
<%--        <td>
          <asp:Image ID="Image1" runat="server" ImageUrl="~/工作主題 作業流程.png" />
        </td>--%>
    </table>
</asp:Content>
