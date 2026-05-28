<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateIssue.aspx.cs" Inherits="WebApplication2.CreateIssue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <td style="vertical-align:top">
    <p>
        新增主題</p>
    <p>
        主題名稱：<asp:TextBox ID="TB_IssueTitle" runat="server"  Width="500px"></asp:TextBox>
    </p>
    <p>
        新建日期：<asp:TextBox ID="TB_CreateDate" runat="server" Width="128px" AutoPostBack="True"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_CreateDate" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />

        <asp:Calendar ID="Calendar_CreateDate" OnSelectionChanged="Calendar_CreateDate_Sel"  runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
    預計結案日期：<asp:TextBox ID="TB_FinishDatePre" runat="server" AutoPostBack="True"></asp:TextBox>

        <asp:Button ID="BT_Cal_Sel_FinishDatePre" OnClick="BT_Cal_Sel_FinishDatePre_Click" runat="server" Text="選擇日期" />

        <asp:Calendar ID="Calendar_FinishDatePre" OnSelectionChanged="Calendar_FinishDatePre_Sel"  runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
        主題負責人：<asp:DropDownList ID="DL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
        </asp:DropDownList><asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19  ORDER BY [employee_id]"></asp:SqlDataSource>
    </p>
            <p>
        主題協作者：
         <asp:CheckBoxList ID="CBL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" RepeatColumns="5">
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19  ORDER BY [employee_id]"></asp:SqlDataSource>
    </p>
    <p>
        相關系統：</p>
    <p>
        <asp:CheckBoxList ID="CBL_SystemList" runat="server" DataSourceID="SqlDataSource_SystemList" DataTextField="SystemName" DataValueField="SystemID" RepeatColumns="5">
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource_SystemList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [SystemList] ORDER BY [SystemID]"></asp:SqlDataSource>
    </p>

    
    <p>
         所屬區域：<asp:DropDownList ID="DL_IssueGroup" runat="server" DataSourceID="SqlDataSourceIssueGroup" DataTextField="IssueGroupTitle" DataValueField="IssueGroupID">
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
        <asp:SqlDataSource ID="SqlDataSourceIssueType" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [IssueTypeID], [IssueTypeTitle] FROM [IssueType] ORDER BY [IssueTypeID]"></asp:SqlDataSource>
    </p>
    <p>
        <asp:Button ID="BT_Send" runat="server" Text="送出" OnClick="BT_Send_Click" />
    </p>
<p>
        <asp:Label ID="LB_Message" runat="server" ForeColor="Red"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource_Send" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueList]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueAssistantMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueListAssistantMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
    </p>
            </td>
<%--    <td>
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Y6P2系統分類.png" />    
    </td>--%>
    </table>


</asp:Content>
