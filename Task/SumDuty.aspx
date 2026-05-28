<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SumDuty.aspx.cs" Inherits="WebApplication2.SumDuty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
       
    <p>
       
        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一天" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一天" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />  
<asp:Button ID="BT_Send" runat="server" Text="查詢" OnClick="BT_Send_Click"/>
        工作日期日數：<asp:Label ID="Totaldays" runat="server" Text=""></asp:Label>
        工作日期總小時：<asp:Label ID="TotalWorkhours" runat="server" Text=""></asp:Label>
    
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

   </p>
    <table>

    <td valign="top">
      
             <asp:GridView ID="GridView_SumSystemNameTaskHour" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceSumSystemNameTaskHour" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="SystemName" HeaderText="系統名稱" SortExpression="SystemName" >
            </asp:BoundField>
            <asp:BoundField DataField="System_failCount" HeaderText="事故次數" SortExpression="System_failCount" ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="System_failTimeSum" HeaderText="事故時數" ReadOnly="True" SortExpression="System_failTimeSum" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <PagerSettings Position="Top" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                
    </asp:GridView>

             <asp:SqlDataSource ID="SqlDataSourceSumSystemNameTaskHour" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select a.SystemName,count(SystemName) as System_failCount,sum(Duty_Work_Time) as System_failTimeSum from (
SELECT  IssueList.IssueID,SystemName,
 round(cast(Duty_Work_Time as float)/60,2) as Duty_Work_Time
   FROM Y6P2_Materials.dbo.IssueList
  inner join IssueDutyDetail 
  on IssueList.IssueID=IssueDutyDetail.IssueID
  inner join IssueSystemMap 
  on IssueList.IssueID=IssueSystemMap.IssueID
  inner join SystemList
  on IssueSystemMap.SystemID=SystemList.SystemID
  where IsDutyIssue = 1  and  IssueList.CreateDate between @taskdatestart and  @taskdateend and IssueStatus &lt;&gt; 5) a
  group by SystemName">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                     <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                 </SelectParameters>
               </asp:SqlDataSource>
  </td valign="top">
          <td valign="top">     

             <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_Duty_shift" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:BoundField DataField="Duty_Person" HeaderText="職工編號" SortExpression="Duty_Person" >
                     <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="employee_name" HeaderText="值勤人員" SortExpression="employee_name" >
                     <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Duty_End" HeaderText="調班日期" ReadOnly="True" SortExpression="Duty_End" DataFormatString="{0:d}" >
                     <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Duty_shift" HeaderText="新班別" ReadOnly="True" SortExpression="Duty_shift" >
                     <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="the_workhour" HeaderText="平日/假日" ReadOnly="True" SortExpression="the_workhour" >
                     <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                 </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                   <PagerSettings Position="Top" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                
    </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource_Duty_shift" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select distinct Duty_Person,employee_name,cast(Duty_End as date) as Duty_End ,case when convert(varchar,cast(Duty_End as datetime),108) between '07:30:00' and '16:30:00' and d.the_workhour =0   then  '04：0730~1630 假日待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =0   then  '05：1630~0730 假日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =8   then  '06：1630~0730 平日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '23:00:00' and '23:59:59' or convert(varchar,cast(Duty_End as datetime),108) between '00:00:00' and '07:00:00'   then  '07：1630~0730 夜間待命-2300~0700接獲電話處理事故' end as Duty_shift,case when d.the_workhour = 0 then '假日' else '平日' end as the_workhour from (select  SystemList.SystemName,a.IssueID,Duty_End,Duty_Person,employee_name from (SELECT a.IssueID,substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,Duty_Person ,b.employee_name FROM Y6P2_Materials.dbo.IssueDutyDetail a  inner join employee b on a.Duty_Person=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus &lt;&gt;5 union all SELECT a.IssueID, substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,a.employee_id,b.employee_name FROM dbo.IssueAssistantMap a  inner join employee b on a.employee_id=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus &lt;&gt;5 inner join IssueDutyDetail d on a.IssueID=d.IssueID   ) a inner join IssueSystemMap  on a.IssueID=IssueSystemMap.IssueID  inner join SystemList  on IssueSystemMap.SystemID=SystemList.SystemID) a inner join time_dimension d on cast(a.Duty_End as date)=cast(the_date as date) 
  where cast(Duty_End as date) between @taskdatestart and  @taskdateend 
  order by  Duty_End desc">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                     <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                 </SelectParameters>
               </asp:SqlDataSource>

                </td valign="top">
            
                               
                   </table>                 
                        
     

</asp:Content>


