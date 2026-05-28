<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="WebApplication2.TaskList" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p>

        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一天" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一天" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />
    <asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True" AutoPostBack="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
          <asp:DropDownList ID="DL_SystemList" runat="server" DataSourceID="SqlDataSourceSystemlist" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
          <asp:DropDownList ID="DL_IssueGroupID" runat="server" DataSourceID="SqlDataSourceIIssueGroupID" DataTextField="IssueGroupTitle" DataValueField="IssueGroupTitle" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
        關鍵字查詢：<asp:TextBox ID="TB_TaskDescription" runat="server"></asp:TextBox>
            <asp:SqlDataSource ID="SqlDataSourceIIssueGroupID" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueGroup]"></asp:SqlDataSource>
            <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="TextBoxNameQueryTemp" runat="server" Visible="False"></asp:TextBox>
&nbsp;<asp:Button ID="BT_Send" runat="server" Text="查詢" OnClick="BT_Send_Click" />
        <asp:Button ID="BT_Print" runat="server" Text="列印" OnClick="BT_Print_Click"  />
    
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
         <asp:SqlDataSource ID="SqlDataSourceSystemlist" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [SystemID], [SystemName] FROM [SystemList]"></asp:SqlDataSource>
         <asp:Calendar ID="Calendar_TaskDateEnd"   runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="Calendar_TaskDateEnd_SelectionChanged">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
        </asp:Calendar>

        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where  employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
    &nbsp;</p>
    <p>
    <asp:GridView ID="GridView_TaskList"  runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TaskID" DataSourceID="SqlDataSource_TaskList" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_TaskList_RowDataBound">
        <AlternatingRowStyle BackColor="White"  />
        <Columns>
            <asp:BoundField DataField="IssueTitle" HeaderText="主題名稱" SortExpression="IssueTitle" >
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="employee_name" HeaderText="人員" SortExpression="employee_name" >
            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="SystemName" HeaderText="系統" SortExpression="SystemName" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueGroupTitle" HeaderText="所屬區域" SortExpression="IssueGroupTitle">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueStatus" HeaderText="狀態" SortExpression="IssueStatus">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="TaskDescription" HeaderText="工作內容" SortExpression="TaskDescription" >
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="500px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="IssueID" DataNavigateUrlFormatString="ModifyIssue.aspx?IssueID={0}" HeaderText="主題修改" Text="修改">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="TaskID" DataNavigateUrlFormatString="ModifyTask.aspx?TaskID={0}" HeaderText="工作日誌修改" Text="修改" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="TaskDate" HeaderText="日期" SortExpression="TaskDate" DataFormatString="{0:d}" />
            <asp:BoundField DataField="TaskHours" HeaderText="工時" />
        </Columns>
        <EditRowStyle BackColor="#2461BF"  />
   
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"  />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB"  />
        <SortedAscendingHeaderStyle BackColor="#6D95E1"  />
        <SortedDescendingCellStyle BackColor="#E9EBEF"  />
        <SortedDescendingHeaderStyle BackColor="#4870BE"  />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource_TaskList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between @TaskDate1 and  @TaskDate2) ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_TaskDateStart" Name="TaskDate1" PropertyName="Text" />
            <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="TaskDate2" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</p>
  
          

    </asp:Content>
