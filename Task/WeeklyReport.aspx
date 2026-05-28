<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeeklyReport.aspx.cs" Inherits="WebApplication2.WeeklyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .dropdown-container {
            position: relative;
            display: inline-block;
            width: 300px;
        }

        .dropdown-display {
            width: 100%;
            padding: 6px;
            border: 1px solid #ccc;
            cursor: pointer;
            background-color: #fff;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: white;
            border: 1px solid #ccc;
            width: 100%;
            max-height: none;
            overflow-y: auto;
            z-index: 999;
        }

        .dropdown-container.active .dropdown-content {
            display: block;
        }
    </style>

   
    

        工作日期區間：<asp:TextBox ID="TB_TaskDateStart" runat="server" Width="128px"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_TaskDateStart" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        ～<asp:TextBox ID="TB_TaskDateEnd" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_TaskDateEnd"  runat="server" Text="選擇日期" OnClick="BT_Cal_Sel_TaskDateEnd_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Prev" runat="server" Text="前一天" OnClick="BT_Cal_Sel_TaskDate_Prev_Click" />
        <asp:Button ID="BT_Cal_Sel_TaskDate_Next"  runat="server" Text="後一天" OnClick="BT_Cal_Sel_TaskDate_Next_Click" />
         
        <div class="dropdown-container">
                        <!-- 四個條件按鈕 -->
    <button type="button" class="btn-group" data-group="3">YMC</button>
    <button type="button" class="btn-group" data-group="1">HBM_TNRL</button>
    <button type="button" class="btn-group" data-group="2">SPC</button>
    <button type="button" class="btn-group" data-group="0">HSM</button>
            
    <span id="btnClearSelection" style="cursor:pointer; margin-left:5px; color:red;">&times;</span>
      <asp:TextBox ID="txtSelected" runat="server"
    CssClass="dropdown-display"
    ReadOnly="true"
    Text="請選擇..."
    ClientIDMode="Static" />

   
<asp:HiddenField ID="hfSelectedValues" runat="server"  ClientIDMode="Static"/>
        <div class="dropdown-content">
<asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
        </asp:CheckBoxList>
                   </div>
        </div>

   <%--<asp:DropDownList ID="DL_TaskPeople" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>--%>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DL_SystemList" runat="server" DataSourceID="SqlDataSourceSystemlist" DataTextField="SystemName" DataValueField="SystemName" AppendDataBoundItems="True">
        <asp:ListItem>ALL</asp:ListItem>
              
        </asp:DropDownList>
            <asp:DropDownList ID="DL_IssueGroupID" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSourceIIssueGroupID" DataTextField="IssueGroupTitle" DataValueField="IssueGroupTitle">
                <asp:ListItem>ALL</asp:ListItem>
        </asp:DropDownList>
         關鍵字查詢：<asp:TextBox ID="TB_TaskDescription" runat="server"></asp:TextBox>
            <asp:SqlDataSource ID="SqlDataSourceSystemlist" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [SystemID], [SystemName] FROM [SystemList]"></asp:SqlDataSource>
            
            <asp:SqlDataSource ID="SqlDataSourceIIssueGroupID" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueGroup]"></asp:SqlDataSource>
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

        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where  employee_Employed = 1 ORDER BY [employee_id]"></asp:SqlDataSource>
    &nbsp;</p>
<p>
    <asp:GridView ID="GridView_WeeklyReport" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource_SumTaskHourList" ForeColor="#333333" GridLines="None" OnDataBound="GridView_WeeklyReport_DataBound1" OnRowDataBound="GridView_WeeklyReport_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
              <asp:TemplateField HeaderText="序號">
          <ItemTemplate>
             <%#GridView_WeeklyReport.PageIndex * GridView_WeeklyReport.PageSize + GridView_WeeklyReport.Rows.Count + 1%>
           </ItemTemplate>
           <HeaderStyle Wrap="False" HorizontalAlign="Left"  />
            <ItemStyle  HorizontalAlign="Left" VerticalAlign="Middle" />
  </asp:TemplateField>
            <asp:BoundField DataField="CreateDate" HeaderText="主題建立時間" SortExpression="CreateDate" DataFormatString="{0:d}" >
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
            </asp:BoundField>
              <asp:HyperLinkField DataNavigateUrlFields="IssueID" DataNavigateUrlFormatString="TaskList.aspx?IssueID={0}" DataTextField="IssueTitle" HeaderText="主題" NavigateUrl="~/TaskList.aspx"  ItemStyle-Wrap="True" ItemStyle-Width="250px" />
              <asp:BoundField DataField="IssueStatus" HeaderText="狀態" SortExpression="IssueStatus" >
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
              </asp:BoundField>
              <asp:BoundField DataField="IssueDescription" HeaderText="主題描述" SortExpression="IssueDescription" >
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
              </asp:BoundField>
            <asp:BoundField DataField="SystemName" HeaderText="系統" SortExpression="SystemName" >
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
              </asp:BoundField>
              <asp:BoundField DataField="IssueGroupTitle" HeaderText="所屬區域" SortExpression="IssueGroupTitle">
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
              </asp:BoundField>
              <asp:BoundField DataField="SumTaskHours_Period" HeaderText="主題合計工時" SortExpression="SumTaskHours_Period">
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
              </asp:BoundField>
            <asp:BoundField DataField="TaskDescription" HeaderText="工作日誌" SortExpression="TaskDescription" >
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="500px" />
              </asp:BoundField>
            <asp:BoundField DataField="TaskDate" HeaderText="工作日誌填寫時間" SortExpression="TaskDate" DataFormatString="{0:d}" >
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
              <ItemStyle Width="170px" HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:BoundField>
            <asp:BoundField DataField="employee_name" HeaderText="負責人" SortExpression="employee_name" >
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="IssueID" DataNavigateUrlFormatString="ModifyIssue.aspx?IssueID={0}" HeaderText="主題修改" NavigateUrl="~/ModifyIssue.aspx" Text="編輯" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="TaskID" DataNavigateUrlFormatString="ModifyTask.aspx?TaskID={0}" HeaderText="工作日誌修改" NavigateUrl="~/ModifyTask.aspx" Text="編輯" >
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
              </asp:HyperLinkField>
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
    <asp:SqlDataSource ID="SqlDataSource_SumTaskHourList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours, (SELECT SUM(TaskHours) AS SumTaskHours_Period FROM TaskList WHERE (TaskDate BETWEEN @TaskDateStart AND @TaskDateEnd) AND (IL.IssueType &lt;&gt; 5) AND (IssueID = IL.IssueID) GROUP BY IssueID) AS SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus, IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN IssueList AS IL ON IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE (TaskList_1.TaskDate BETWEEN @TaskDateStart1 AND @TaskDateEnd1) AND (IL.IssueType &lt;&gt; 5) and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id">
        <SelectParameters>
                  <asp:ControlParameter ControlID="TB_TaskDateStart" Name="TaskDateStart" PropertyName="Text" />
                  <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="TaskDateEnd" PropertyName="Text" />
                  <asp:ControlParameter ControlID="TB_TaskDateStart" Name="TaskDateStart1" PropertyName="Text" />
                  <asp:ControlParameter ControlID="TB_TaskDateEnd" Name="TaskDateEnd1" PropertyName="Text" />
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

<script src="/Scripts/jquery-3.6.0.min.js"></script>
   <script>
$(document).ready(function () {
    // ---- 模擬下拉顯示 ----
    $('.dropdown-display').on('click', function () {
        $(this).closest('.dropdown-container').toggleClass('active');
    });

    // ---- 勾選 CheckBox 更新 TextBox + HiddenField ----
    $('.dropdown-content input[type=checkbox]').on('change', function () {
        updateSelection();
    });

    // ---- 小叉叉清除 ----
    $('#btnClearSelection').on('click', function (e) {
        e.stopPropagation();
        var container = $(this).closest('.dropdown-container');
        container.find('input[type=checkbox]').prop('checked', false);
        container.find('.dropdown-display').val('請選擇...');
        $('#hfSelectedValues').val('');
    });

    // ---- 四個按鈕，依 group 勾選 ----
    // 預先準備 group 對應的 employee_id
    // 這裡用範例資料
    var groupData = {
        "3": ["013140","013257","045411","053233"],   // YMC
        "1": ["028721","030715","043128","045841","045627","053233"],       // HBM_TNRL
        "2": ["045627","048644","048652"],       // SPC
        "0": ["019126","019134","028739","029926","052715"]            // HSM
    };

    $('.btn-group').on('click', function (e) {
        e.stopPropagation();
        var group = $(this).data('group').toString();
        var selectedIds = groupData[group] || [];

        var container = $(this).closest('.dropdown-container');

        container.find('input[type=checkbox]').each(function () {
            if (selectedIds.includes($(this).val())) {
                $(this).prop('checked', true);
            } else {
                $(this).prop('checked', false);
            }
        });

        updateSelection();
    });

    // ---- 更新函數 ----
    function updateSelection() {
        var container = $('.dropdown-container');
        var selectedValues = [];
        var selectedTexts = [];

        container.find('input[type=checkbox]:checked').each(function () {
            selectedValues.push($(this).val());
            selectedTexts.push($(this).parent().text().trim());
        });

        container.find('.dropdown-display').val(selectedTexts.join(', ') || '請選擇...');
        $('#hfSelectedValues').val(selectedValues.join(','));
    }

    // ---- PostBack 後恢復 TextBox 顯示 ----
    var hfVal = $('#hfSelectedValues').val();
    if (hfVal) {
        var selectedTexts = [];
        $('.dropdown-content input[type=checkbox]').each(function () {
            if (hfVal.split(',').includes($(this).val())) {
                $(this).prop('checked', true);
                selectedTexts.push($(this).parent().text().trim());
            }
        });
        $('.dropdown-display').val(selectedTexts.join(', '));
    }
});
</script>

</asp:Content>
