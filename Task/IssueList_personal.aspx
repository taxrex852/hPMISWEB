<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IssueList_personal.aspx.cs" Inherits="WebApplication2.IssueList_personal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <tr>
       <asp:CheckBox ID="CheckBox0" Text="0" runat="server" />新建
        <asp:CheckBox ID="CheckBox1" Text="1" runat="server" />執行中
        <asp:CheckBox ID="CheckBox2" Text="2" runat="server" />已完成待上線
        <asp:CheckBox ID="CheckBox3" Text="3" runat="server" />已完成待結案
        <asp:CheckBox ID="CheckBox4" Text="4" runat="server" />結案
        <asp:CheckBox ID="CheckBox5" Text="5" runat="server" />刪除
        <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
        <br>
        
        主題負責人：<asp:DropDownList ID="DL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee]  where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>          
        
    所屬區域：<asp:DropDownList ID="DL_IssueGroup" runat="server" DataSourceID="SqlDataSourceIssueGroup" DataTextField="IssueGroupTitle" DataValueField="IssueGroupID" AppendDataBoundItems="True">
        <asp:ListItem Selected="True">ALL</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceIssueGroup" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [IssueGroupID], [IssueGroupTitle] FROM [IssueGroup] ORDER BY [IssueGroupID]"></asp:SqlDataSource>
       
         主題日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
&nbsp;<asp:Calendar ID="Calendar_TaskDate" OnSelectionChanged="Calendar_CreateDate_Sel"  runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
        <asp:Button ID="Query" runat="server" Text="查詢" Height="22px" OnClick="Query_Click" Width="54px" />
        </tr>
    
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IssueID" DataSourceID="SqlDataSource_GV_IssueList" ForeColor="#333333" GridLines="None"  PageSize="20" OnRowDataBound="GridView1_RowDataBound" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
           <asp:TemplateField HeaderText="序號">
          <ItemTemplate>
             <%#GridView1.PageIndex * GridView1.PageSize + GridView1.Rows.Count + 1%>
           </ItemTemplate>
           <HeaderStyle Wrap="False"  />
            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
  </asp:TemplateField>
            <asp:BoundField DataField="IssueTitle" HeaderText="主題名稱" SortExpression="IssueTitle" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="SumTaskHours" HeaderText="主題工時" SortExpression="SumTaskHours" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="SumTaskHours_Personal" HeaderText="個人工時" SortExpression="SumTaskHours_Personal">
            </asp:BoundField>
            <asp:BoundField DataField="IssueStatus" HeaderText="狀態" SortExpression="IssueStatus" />
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
    <asp:SqlDataSource ID="SqlDataSource_GV_IssueList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT dbo.IssueList.IssueID, dbo.IssueList.IssueTitle, dbo.TaskList.employee_id, dbo.IssueID_SumTaskHour.SumTaskHours, SUM(dbo.TaskList.TaskHours) AS SumTaskHours_Personal,dbo.IssueList.IssueStatus FROM dbo.TaskList INNER JOIN dbo.IssueList ON dbo.TaskList.IssueID = dbo.IssueList.IssueID INNER JOIN dbo.IssueID_SumTaskHour ON dbo.TaskList.IssueID = dbo.IssueID_SumTaskHour.IssueID  WHERE (IssueList.IssueStatus = 0) OR (IssueList.IssueStatus = 1) and  dbo.TaskList.employee_id = @id   GROUP BY  dbo.IssueList.IssueID, dbo.IssueList.IssueTitle, dbo.TaskList.employee_id, dbo.IssueList.IssueStatus,dbo.IssueID_SumTaskHour.SumTaskHours, dbo.IssueList.IssueGroupID ORDER BY IssueID asc">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxName" Name="id" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
         </tr>
</asp:Content>
