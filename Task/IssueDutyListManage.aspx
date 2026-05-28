<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IssueDutyListManage.aspx.cs" Inherits="WebApplication2.IssueDutyListManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p>

        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>
        <asp:TextBox ID="TB_TaskDateStartTemp" runat="server" Width="128px" Visible="False"></asp:TextBox>
        <asp:TextBox ID="TB_TaskDateEndTemp" runat="server" Width="128px" Visible="False"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一月" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一月" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />
        <asp:Button ID="BT_Issuestate"  runat="server" Text="未結案" OnClick="BT_Issuestate_Click"  />
    <asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True" AutoPostBack="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
          <asp:DropDownList ID="DL_SystemList" runat="server" DataSourceID="SqlDataSourceSystemlist" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSourceIIssueGroupID" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueGroup]"></asp:SqlDataSource>
            <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="TextBoxNameQueryTemp" runat="server" Visible="False"></asp:TextBox>
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
    <asp:GridView ID="GridView_TaskList"  runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_TaskList" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White"  />
        <Columns>
            <asp:BoundField DataField="SystemName" HeaderText="系統名稱" SortExpression="SystemName" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Duty_Start" HeaderText="開始時間" SortExpression="Duty_Start" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" ReadOnly="True" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Duty_End" HeaderText="結束時間" SortExpression="Duty_End" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" ReadOnly="True">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Duty_Work_Time" HeaderText="故障時間" SortExpression="Duty_Work_Time">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Duty_Fail_Level" HeaderText="故障等級" SortExpression="Duty_Fail_Level" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Duty_Fail_Style" HeaderText="故障種類" SortExpression="Duty_Fail_Style" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="employee_name" HeaderText="值勤人員" SortExpression="employee_name">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueAssistantName" HeaderText="協助處理者" SortExpression="IssueAssistantName">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueTitle" HeaderText="異況描述" SortExpression="IssueTitle">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueDescription" HeaderText="處理方式及結果" SortExpression="IssueDescription">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="TaskDescription" HeaderText="原因分析與對策" SortExpression="TaskDescription">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="IssueStatus" HeaderText="狀態" ReadOnly="True" SortExpression="IssueStatus">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
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
    <asp:SqlDataSource ID="SqlDataSource_TaskList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [vw_issue_DutyManager]
where [Duty_Start] between @taskdate1 and @taskdate2

 ORDER BY [Duty_Start] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_TaskDateStartTemp" Name="taskdate1" PropertyName="Text" />
            <asp:ControlParameter ControlID="TB_TaskDateEndTemp" Name="taskdate2" PropertyName="Text" />
           
        </SelectParameters>
    </asp:SqlDataSource>
</p>
     

</asp:Content>
