<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskHourList.aspx.cs" Inherits="WebApplication2.TaskHourList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>

        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一天" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一天" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />
    <asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
            <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
&nbsp;<asp:Button ID="BT_Send" runat="server" Text="查詢" OnClick="BT_Send_Click" />
    
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

        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee]  where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
    &nbsp;</p>
<p>
    <asp:GridView ID="GridView_TaskList" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_SumTaskHourList" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="the_date" HeaderText="日期" SortExpression="the_date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="employee_name" HeaderText="人員" SortExpression="employee_name" >
            </asp:BoundField>
            <asp:BoundField DataField="Remaining_Hours" HeaderText="應填工時" SortExpression="Remaining_Hours" ReadOnly="True" />
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
    <asp:SqlDataSource ID="SqlDataSource_SumTaskHourList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT DISTINCT CONVERT (Varchar(10), time_dimension.the_date, 111) AS the_date, time_dimension.the_workhour, employee.employee_id, employee.employee_name, (CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily, (CASE WHEN dbo.time_dimension.the_workhour = 0 THEN 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 ELSE dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM employee CROSS JOIN time_dimension LEFT OUTER JOIN Empolyee_SumTaskHour_Daily ON time_dimension.the_date = Empolyee_SumTaskHour_Daily.TaskDate AND employee.employee_id = Empolyee_SumTaskHour_Daily.employee_id WHERE (time_dimension.the_date = @the_date) AND (employee.employee_Employed = 1) ORDER BY the_date, employee.employee_id">
        <SelectParameters>
                  <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="the_date" PropertyName="Text" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
<p>
    &nbsp;</p>
</asp:Content>
