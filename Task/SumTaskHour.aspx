<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SumTaskHour.aspx.cs" Inherits="WebApplication2.SumTaskHour" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
       
    <p>
       
        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一天" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一天" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />
<asp:Button ID="BT_Send" runat="server" Text="查詢" OnClick="BT_Send_Click"/>
    
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
            <asp:BoundField DataField="SumSystemNameTaskHour" HeaderText="系統工時統計" SortExpression="SumSystemNameTaskHour" ReadOnly="True" >
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

             <asp:SqlDataSource ID="SqlDataSourceSumSystemNameTaskHour" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT          dbo.SystemList.SystemName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL 
                            THEN 0 ELSE SUM(dbo.TaskList.TaskHours) END)  AS SumSystemNameTaskHour
FROM              dbo.SystemList left OUTER JOIN
                            dbo.TaskList ON dbo.SystemList.SystemID = dbo.TaskList.SystemID
where TaskList.taskdate between @taskdatestart and  @taskdateend
GROUP BY   dbo.SystemList.SystemName">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                     <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                 </SelectParameters>
               </asp:SqlDataSource>
  </td valign="top">

                 <td valign="top">
    

              <asp:GridView ID="GridView_SumIssuetypetitle" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceSumIssuetypetitle" ForeColor="#333333" GridLines="None"  >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="issuetypetitle" HeaderText="系統類型" SortExpression="issuetypetitle" >
            </asp:BoundField>
            <asp:BoundField DataField="SumIssuetypetitle" HeaderText="系統類型工時統計" SortExpression="SumIssuetypetitle" ReadOnly="True" >
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
                       <asp:SqlDataSource ID="SqlDataSourceSumIssuetypetitle" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="select issuetypetitle, SUM(taskhours) as SumIssuetypetitle from tasklist
left join issuelist on tasklist.issueid = issuelist.issueid
left join issuetype on issuelist.issuetype = issuetype.issuetypeid
where TaskList.taskdate between @taskdatestart and  @taskdateend
group by issuetypetitle">
                           <SelectParameters>
                               <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                               <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                           </SelectParameters>
                      </asp:SqlDataSource>


     
        </td valign="top">
                
          
       <td valign="top">

      

             <asp:GridView ID="GridView_SumDeapartmentIDHour" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSourceSumDeapartmentIDHour" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="DeapartmentName" HeaderText="部門名稱" SortExpression="DeapartmentName" >
            </asp:BoundField>
            <asp:BoundField DataField="SumDeapartmentIDHour" HeaderText="部門名稱工時統計" SortExpression="SumDeapartmentIDHour" ReadOnly="True" >
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
                                 <asp:SqlDataSource ID="SqlDataSourceSumDeapartmentIDHour" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT          dbo.DeapartmentList.DeapartmentName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL 
                            THEN 0 ELSE SUM(dbo.TaskList.TaskHours ) END) AS SumDeapartmentIDHour
FROM              dbo.IssueList INNER JOIN
                            dbo.TaskList ON dbo.IssueList.IssueID = dbo.TaskList.IssueID right outer JOIN
                            dbo.DeapartmentList ON dbo.IssueList.DeapartmentID = dbo.DeapartmentList.DeapartmentID
where TaskList.taskdate between @taskdatestart and  @taskdateend
GROUP BY   dbo.DeapartmentList.DeapartmentName" >
                                     <SelectParameters>
                                         <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                                         <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                                     </SelectParameters>
                                </asp:SqlDataSource>



                  </td valign="top">   

          <td valign="top">
    

              <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"  >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                     <asp:BoundField DataField="SystemName" HeaderText="系統名稱" SortExpression="SystemName" >
            </asp:BoundField>
            <asp:BoundField DataField="IssueTypeTitle" HeaderText="主題類別" SortExpression="IssueTypeTitle" >
            </asp:BoundField>
            <asp:BoundField DataField="SumIssuetypetitle" HeaderText="系統主題類別工時統計" ReadOnly="True" SortExpression="SumIssuetypetitle" />
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
                       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT          dbo.SystemList.SystemName, dbo.IssueType.IssueTypeTitle, SUM(dbo.TaskList.TaskHours) 
                            AS SumIssuetypetitle
FROM              dbo.TaskList INNER JOIN
                            dbo.SystemList ON dbo.TaskList.SystemID = dbo.SystemList.SystemID LEFT OUTER JOIN
                            dbo.IssueList ON dbo.TaskList.IssueID = dbo.IssueList.IssueID LEFT OUTER JOIN
                            dbo.IssueType ON dbo.IssueList.IssueType = dbo.IssueType.IssueTypeID
where TaskList.taskdate between @taskdatestart and  @taskdateend
GROUP BY   dbo.IssueType.IssueTypeTitle, dbo.SystemList.SystemName">
                           <SelectParameters>
                               <asp:ControlParameter ControlID="TB_TaskDateStart" Name="taskdatestart" PropertyName="Text" />
                               <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="taskdateend" PropertyName="Text" />
                           </SelectParameters>
                      </asp:SqlDataSource>


     
        </td valign="top">
              
                             
     
                               
                   </table>                 
                        
     

</asp:Content>


