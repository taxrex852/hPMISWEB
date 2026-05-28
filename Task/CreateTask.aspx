<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="WebApplication2.CreateTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
   <table>
     <td style="vertical-align:top">
         <asp:Panel ID="Panel1" runat="server" >
             <p>
                 填寫工作日誌</p>
             <p>
                 主題編號與名稱：<asp:DropDownList ID="DL_IssueSel" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource_IssueSel" DataTextField="IssueTitle" DataValueField="IssueID" OnSelectedIndexChanged="DL_IssueSel_SelectedIndexChanged">
                 </asp:DropDownList>
                 <asp:TextBox ID="TB_FileterTasklist" runat="server" Width="128px"></asp:TextBox>
                 <asp:Button ID="BT_fileter" runat="server" Text="主題篩選" OnClick="BT_fileter_Click" />
                 <asp:SqlDataSource ID="SqlDataSource_IssueSel" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" OnSelected="SqlDataSource_IssueSel_Selected" SelectCommand="(select * from VW_Issue_List where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2' or IssueStatus = '3')) 
UNION (select * from VW_Issue_DutyList where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2' or IssueStatus = '3') )
  
ORDER BY IsDutyIssue desc,IssueGroupID, IssueTitle,CreateDate, employee_id "></asp:SqlDataSource>
                 <asp:SqlDataSource ID="SqlDataSource_PageLoad" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT IssueStatus FROM IssueList WHERE (IssueID = @IssueID)">
                     <SelectParameters>
                         <asp:QueryStringParameter Name="IssueID" QueryStringField="IssueID" Type="Int64" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             </p>
             <p>
                 工作日期：<asp:TextBox ID="TB_TaskDate" runat="server" Width="128px"></asp:TextBox>
                 <asp:Button ID="BT_Cal_Sel_TaskDate" runat="server" OnClick="BT_Cal_Sel_CreateDate_Click" Text="選擇日期" />
                 <asp:Calendar ID="Calendar_TaskDate" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="Calendar_CreateDate_Sel" Visible="False" Width="200px">
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
                 執行人員：<asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
                 </asp:DropDownList>
                 <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
                 <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
             </p>
             <p>
                 <asp:Label ID="Label1" runat="server" Text="相關系統：" BackColor="#FFFF66"></asp:Label><asp:DropDownList ID="DL_System" runat="server" DataSourceID="SqlDataSource_PageLoad_SystemList" DataTextField="SystemName" DataValueField="SystemID" BackColor="#FFFF66">
                 </asp:DropDownList>
                 <asp:SqlDataSource ID="SqlDataSource_PageLoad_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT IssueSystemMap.IssueID, IssueSystemMap.SystemID, SystemList.SystemName FROM IssueSystemMap INNER JOIN SystemList ON IssueSystemMap.SystemID = SystemList.SystemID WHERE (IssueSystemMap.IssueID = @IssueID)">
                     <SelectParameters>
                         <asp:ControlParameter ControlID="DL_IssueSel" Name="IssueID" PropertyName="SelectedValue" Type="Int64" />
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
                 <asp:Button ID="BT_Send" runat="server" OnClick="BT_Send_Click" Text="送出" />
             </p>
             <p>
                 <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
                 <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" DataSourceMode="DataReader" SelectCommand="SELECT * FROM [IssueList]"></asp:SqlDataSource>
                 <asp:TextBox ID="TBTempissuestatus" runat="server" Visible="False"></asp:TextBox>
             </p>
       
      </asp:Panel>
    </td>
        <td style="vertical-align:top">
               <asp:Panel ID="Panel2" runat="server">
    <asp:SqlDataSource ID="SqlDataSource_TaskList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT TaskList.IssueID,
 IssueList.IssueTitle,
 TaskList.TaskID, 
TaskList.TaskDate, 
TaskList.TaskDescription, 
TaskList.TaskHours, 
SystemList.SystemName, 
employee.employee_name 
FROM TaskList 
INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID 
INNER JOIN employee ON TaskList.employee_id = employee.employee_id 
INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID
 WHERE (TaskList.IssueID = @IssueID) ORDER BY TaskList.TaskDate DESC, employee.employee_id,  IssueList.IssueTitle DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="DL_IssueSel" Name="IssueID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
             <asp:GridView ID="GridView_TaskList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TaskID" DataSourceID="SqlDataSource_TaskList" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IssueTitle" HeaderText="主題名稱" SortExpression="IssueTitle" />
            <asp:BoundField DataField="employee_name" HeaderText="人員" SortExpression="employee_name" />
            <asp:BoundField DataField="SystemName" HeaderText="系統" SortExpression="SystemName" />
            <asp:BoundField DataField="TaskDescription" HeaderText="工作內容" SortExpression="TaskDescription" />
            <asp:HyperLinkField DataNavigateUrlFields="TaskID" DataNavigateUrlFormatString="ModifyTask.aspx?TaskID={0}" HeaderText="工作日誌修改" Text="修改" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="TaskDate" HeaderText="日期" SortExpression="TaskDate" DataFormatString="{0:d}" />
            <asp:BoundField DataField="TaskHours" HeaderText="工時" SortExpression="TaskHours" />
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
       </table>
</asp:Content>
