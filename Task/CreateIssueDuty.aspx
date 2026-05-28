<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateIssueDuty.aspx.cs" Inherits="WebApplication2.CreateIssueDuty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <td style="vertical-align:top">
    <p>
        新增主題</p>
    <p>
        主題名稱：<asp:TextBox ID="TB_IssueTitle" runat="server"  Width="500px"></asp:TextBox>
    </p>
    <p>
        開始日期：<asp:TextBox ID="TB_CreateDate" runat="server" Width="128px"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_CreateDate" OnClick="BT_Cal_Sel_CreateDate_Click" runat="server" Text="選擇日期" />
        &nbsp;&nbsp;
        時：<asp:DropDownList ID="DL_StartHour" runat="server">
               <asp:ListItem>00</asp:ListItem>
               <asp:ListItem>01</asp:ListItem>
               <asp:ListItem>02</asp:ListItem>
               <asp:ListItem>03</asp:ListItem>
               <asp:ListItem>04</asp:ListItem>
               <asp:ListItem>05</asp:ListItem>
               <asp:ListItem>06</asp:ListItem>
               <asp:ListItem>07</asp:ListItem>
               <asp:ListItem>08</asp:ListItem>
               <asp:ListItem>09</asp:ListItem>
               <asp:ListItem>10</asp:ListItem>
               <asp:ListItem>11</asp:ListItem>
               <asp:ListItem>12</asp:ListItem>
               <asp:ListItem>13</asp:ListItem>
               <asp:ListItem>14</asp:ListItem>
               <asp:ListItem>15</asp:ListItem>
               <asp:ListItem>16</asp:ListItem>
               <asp:ListItem>17</asp:ListItem>
               <asp:ListItem>18</asp:ListItem>
               <asp:ListItem>19</asp:ListItem>
               <asp:ListItem>20</asp:ListItem>
               <asp:ListItem>21</asp:ListItem>
               <asp:ListItem>22</asp:ListItem>
               <asp:ListItem>23</asp:ListItem>
           
        </asp:DropDownList>
        分：<asp:DropDownList ID="DL_StartMinute" runat="server">
            <asp:ListItem>00</asp:ListItem>
            <asp:ListItem>01</asp:ListItem>
            <asp:ListItem>02</asp:ListItem>
            <asp:ListItem>03</asp:ListItem>
            <asp:ListItem>04</asp:ListItem>
            <asp:ListItem>05</asp:ListItem>
            <asp:ListItem>06</asp:ListItem>
            <asp:ListItem>07</asp:ListItem>
            <asp:ListItem>08</asp:ListItem>
            <asp:ListItem>09</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
            <asp:ListItem>17</asp:ListItem>
            <asp:ListItem>18</asp:ListItem>
            <asp:ListItem>19</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>21</asp:ListItem>
            <asp:ListItem>22</asp:ListItem>
            <asp:ListItem>23</asp:ListItem>
            <asp:ListItem>24</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>26</asp:ListItem>
            <asp:ListItem>27</asp:ListItem>
            <asp:ListItem>28</asp:ListItem>
            <asp:ListItem>29</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>31</asp:ListItem>
            <asp:ListItem>32</asp:ListItem>
            <asp:ListItem>33</asp:ListItem>
            <asp:ListItem>34</asp:ListItem>
            <asp:ListItem>35</asp:ListItem>
            <asp:ListItem>36</asp:ListItem>
            <asp:ListItem>37</asp:ListItem>
            <asp:ListItem>38</asp:ListItem>
            <asp:ListItem>39</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>41</asp:ListItem>
            <asp:ListItem>42</asp:ListItem>
            <asp:ListItem>43</asp:ListItem>
            <asp:ListItem>44</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
            <asp:ListItem>46</asp:ListItem>
            <asp:ListItem>47</asp:ListItem>
            <asp:ListItem>48</asp:ListItem>
            <asp:ListItem>49</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem>51</asp:ListItem>
            <asp:ListItem>52</asp:ListItem>
            <asp:ListItem>53</asp:ListItem>
            <asp:ListItem>54</asp:ListItem>
            <asp:ListItem>55</asp:ListItem>
            <asp:ListItem>56</asp:ListItem>
            <asp:ListItem>57</asp:ListItem>
            <asp:ListItem>58</asp:ListItem>
            <asp:ListItem>59</asp:ListItem>
        </asp:DropDownList>
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
        結束日期：<asp:TextBox ID="TB_FinishDatePre" runat="server"></asp:TextBox>
        <asp:Button ID="BT_Cal_Sel_FinishDatePre" OnClick="BT_Cal_Sel_FinishDatePre_Click" runat="server" Text="選擇日期" />
          &nbsp;&nbsp;時：<asp:DropDownList ID="DL_EndHour" runat="server">
               <asp:ListItem>00</asp:ListItem>
               <asp:ListItem>01</asp:ListItem>
               <asp:ListItem>02</asp:ListItem>
               <asp:ListItem>03</asp:ListItem>
               <asp:ListItem>04</asp:ListItem>
               <asp:ListItem>05</asp:ListItem>
               <asp:ListItem>06</asp:ListItem>
               <asp:ListItem>07</asp:ListItem>
               <asp:ListItem>08</asp:ListItem>
               <asp:ListItem>09</asp:ListItem>
               <asp:ListItem>10</asp:ListItem>
               <asp:ListItem>11</asp:ListItem>
               <asp:ListItem>12</asp:ListItem>
               <asp:ListItem>13</asp:ListItem>
               <asp:ListItem>14</asp:ListItem>
               <asp:ListItem>15</asp:ListItem>
               <asp:ListItem>16</asp:ListItem>
               <asp:ListItem>17</asp:ListItem>
               <asp:ListItem>18</asp:ListItem>
               <asp:ListItem>19</asp:ListItem>
               <asp:ListItem>20</asp:ListItem>
               <asp:ListItem>21</asp:ListItem>
               <asp:ListItem>22</asp:ListItem>
               <asp:ListItem>23</asp:ListItem>
           
        </asp:DropDownList>

        分：<asp:DropDownList ID="DL_EndMinute" runat="server">
            <asp:ListItem>00</asp:ListItem>
            <asp:ListItem>01</asp:ListItem>
            <asp:ListItem>02</asp:ListItem>
            <asp:ListItem>03</asp:ListItem>
            <asp:ListItem>04</asp:ListItem>
            <asp:ListItem>05</asp:ListItem>
            <asp:ListItem>06</asp:ListItem>
            <asp:ListItem>07</asp:ListItem>
            <asp:ListItem>08</asp:ListItem>
            <asp:ListItem>09</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
            <asp:ListItem>17</asp:ListItem>
            <asp:ListItem>18</asp:ListItem>
            <asp:ListItem>19</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>21</asp:ListItem>
            <asp:ListItem>22</asp:ListItem>
            <asp:ListItem>23</asp:ListItem>
            <asp:ListItem>24</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>26</asp:ListItem>
            <asp:ListItem>27</asp:ListItem>
            <asp:ListItem>28</asp:ListItem>
            <asp:ListItem>29</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>31</asp:ListItem>
            <asp:ListItem>32</asp:ListItem>
            <asp:ListItem>33</asp:ListItem>
            <asp:ListItem>34</asp:ListItem>
            <asp:ListItem>35</asp:ListItem>
            <asp:ListItem>36</asp:ListItem>
            <asp:ListItem>37</asp:ListItem>
            <asp:ListItem>38</asp:ListItem>
            <asp:ListItem>39</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>41</asp:ListItem>
            <asp:ListItem>42</asp:ListItem>
            <asp:ListItem>43</asp:ListItem>
            <asp:ListItem>44</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
            <asp:ListItem>46</asp:ListItem>
            <asp:ListItem>47</asp:ListItem>
            <asp:ListItem>48</asp:ListItem>
            <asp:ListItem>49</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem>51</asp:ListItem>
            <asp:ListItem>52</asp:ListItem>
            <asp:ListItem>53</asp:ListItem>
            <asp:ListItem>54</asp:ListItem>
            <asp:ListItem>55</asp:ListItem>
            <asp:ListItem>56</asp:ListItem>
            <asp:ListItem>57</asp:ListItem>
            <asp:ListItem>58</asp:ListItem>
            <asp:ListItem>59</asp:ListItem>
        </asp:DropDownList>
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
    預計結案日期：<asp:TextBox ID="TB_FinishDatePre2" runat="server" AutoPostBack="True"></asp:TextBox>

        <asp:Button ID="Button1" OnClick="BT_Cal_Sel_TB_FinishDatePre2_Click" runat="server" Text="選擇日期" />

        <asp:Calendar ID="Calendar_FinishDatePre2" OnSelectionChanged="Calendar_FinishDatePre2_Sel"  runat="server" Visible="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
        故障時間：<asp:Label ID="LabelTimeDiff" runat="server" Text="Label"></asp:Label>分鐘
    </p>
    <p>
        值勤人員：<asp:DropDownList ID="DL_IssueManager_Duty" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_Employee_List" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19  ORDER BY [employee_id]"></asp:SqlDataSource>
                 <asp:TextBox ID="TextBoxName" runat="server" Visible="False"></asp:TextBox>
    </p>

              <p>
        協助處理者：
         <asp:CheckBoxList ID="CBL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id" RepeatColumns="5">
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [employee_id], [employee_name] FROM [employee] where employee_Employed = 1 and row_id &lt; 19 ORDER BY [employee_id]"></asp:SqlDataSource>
    </p>
    <p>
        系統名稱：</p>
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
        主題負責人員：<asp:DropDownList ID="DL_IssueManager" runat="server" DataSourceID="SqlDataSource_Employee_List" DataTextField="employee_name" DataValueField="employee_id">
        </asp:DropDownList>
      
    </p>

              <p>
        故障種類：<asp:DropDownList ID="DL_Duty_Fail_StyleList" runat="server" DataSourceID="SqlDataSource_DL_Duty_Fail_StyleList" DataTextField="Duty_Fail_Style" DataValueField="Duty_Fail_StyleID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_DL_Duty_Fail_StyleList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [Duty_Fail_StyleID], [Duty_Fail_Style] FROM [Duty_Fail_StyleList]"></asp:SqlDataSource>
    </p>
              <p>
        故障等級：<asp:DropDownList ID="DL_Duty_Fail_LevelList" runat="server" DataSourceID="SqlDataSource_DL_Duty_Fail_LevelList" DataTextField="Duty_Fail_Level" DataValueField="Duty_Fail_LevelID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_DL_Duty_Fail_LevelList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [Duty_Fail_LevelID], [Duty_Fail_Level] FROM [Duty_Fail_LevelList]"></asp:SqlDataSource>
    </p>
              <p>
        協助方式：<asp:DropDownList ID="DL_Duty_AssistantStyleList" runat="server" DataSourceID="SqlDataSource_DL_Duty_AssistantStyleList" DataTextField="Duty_AssistantStyle" DataValueField="Duty_AssistantStyleID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource_DL_Duty_AssistantStyleList" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [Duty_AssistantStyleID], [Duty_AssistantStyle] FROM [Duty_AssistantStyleList]"></asp:SqlDataSource>
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
        <asp:SqlDataSource ID="SqlDataSource_SendDuty_Fail_LevelMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [Duty_Fail_LevelMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendDuty_Fail_StyleMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [Duty_Fail_StyleMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendAssistantStyleMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [Duty_AssistantStyleMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueDuty" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueDuty]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueDutyDetail" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueDutyDetail]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueAssistantMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT * FROM [IssueAssistantMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource_SendIssueSystemMap" runat="server" ConnectionString="<%$ ConnectionStrings:Y6P2_MaterialsConnectionString %>" SelectCommand="SELECT [IssueID], [SystemID] FROM [IssueSystemMap]" DataSourceMode="DataReader">
        </asp:SqlDataSource>
    </p>
<%--<p>
        &nbsp;</p>--%>
            </td>
 <%--   <td>
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Y6P2系統分類.png" />    
    </td>--%>
    </table>


</asp:Content>
