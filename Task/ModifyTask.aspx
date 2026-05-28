<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyTask.aspx.cs" Inherits="WebApplication2.ModifyTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
          <td style="vertical-align:top">
    <p>
        修改工作日誌<asp:SqlDataSource ID="SqlDataSource_PageLoad" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT TaskList.TaskID, CONVERT (VARCHAR(20), TaskList.TaskDate, 23) AS TaskDate, TaskList.TaskDescription, TaskList.TaskHours, TaskList.HwDeviceID, TaskList.SwDevID, employee.employee_name, SystemList.SystemName, TaskList.IssueID, IssueList.IssueTitle, employee.employee_id, IssueList.IssueStatus, SystemList.SystemID FROM employee INNER JOIN TaskList ON employee.employee_id = TaskList.employee_id INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID WHERE (TaskList.TaskID = @TaskID)">
            <SelectParameters>
                <asp:QueryStringParameter Name="TaskID" QueryStringField="TaskID" DefaultValue="" />
            </SelectParameters>
        </asp:SqlDataSource>
        </p>
    <p>
       <asp:Label ID="LB_TaskID" runat="server" Visible="False"></asp:Label>
        <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
    </p>
<p>
      <asp:Label ID="LB_IssueID" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="LB_IssueStatus" runat="server" Visible="False"></asp:Label>
        <asp:TextBox ID="TB_IssueID" runat="server"  Visible="False"></asp:TextBox>

    </p>
    <p>
        主題名稱：<asp:Label ID="LB_IssueTitle" runat="server"></asp:Label>
    </p>
    <p>
        工作日期：<asp:Label ID="LB_TaskDate" runat="server" Visible="False"></asp:Label>
        <asp:TextBox ID="TB_TaskDate" runat="server" Width="128px" AutoPostBack="True"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDate" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />

        <asp:Calendar ID="Calendar_TaskDate" OnSelectionChanged="Calendar_CreateDate_Sel"  runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
        執行人員：<asp:Label ID="LB_EmployeeName" runat="server" Visible="False"></asp:Label>
        <asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AutoPostBack="True">
        </asp:DropDownList>

        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
    </p>
    <p>
        
        <asp:Label ID="Label1" runat="server" text="相關系統：" BackColor="#FFFF66"></asp:Label>
        <asp:Label ID="LB_SystemName" runat="server" Visible="False" BackColor="#FFFF66"></asp:Label>
        <asp:DropDownList ID="DL_System" runat="server" DataSourceID="SqlDataSource_PageLoad_SystemList" DataTextField="SystemName" DataValueField="SystemID" AutoPostBack="True" BackColor="#FFFF66">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT IssueSystemMap.IssueID, IssueSystemMap.SystemID, SystemList.SystemName FROM IssueSystemMap INNER JOIN SystemList ON IssueSystemMap.SystemID = SystemList.SystemID WHERE (IssueSystemMap.IssueID = @IssueID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="TB_IssueID" Name="IssueID" PropertyName="Text" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
        </p>
<p>
        工作時數(小時)：<asp:DropDownList ID="DL_TaskHours" runat="server" DataTextField="DeapartmentName" DataValueField="DeapartmentID">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        工作內容：<asp:TextBox ID="TB_TaskDescription" runat="server" Height="500px" TextMode="MultiLine" Width="340px" Font-Size="20px"></asp:TextBox>
    </p>
<p>
        設備編號：<asp:TextBox ID="TB_HwDeviceID" runat="server"></asp:TextBox>
    </p>
<p>
        軟體修改單號：<asp:TextBox ID="TB_SwDevID" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="BT_Send" runat="server" Text="修改" OnClick="BT_Send_Click" />
    &nbsp;
        <asp:Button ID="BT_Del" runat="server" OnClick="BT_Del_Click" Text="刪除" />
    </p>
<p>
        <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueList]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
    </p>
        <asp:SqlDataSource ID="SqlDataSource_InsertSend" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [TaskList_log]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
<p>
        &nbsp;</p>
          </td>
   <%--    <td>
    <asp:Image ID="Image1" runat="server" ImageUrl="~/工作主題 作業流程.png" />    
    </td>--%>
    </table>
</asp:Content>
