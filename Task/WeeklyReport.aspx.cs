using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WeeklyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                    Response.Redirect("Login.aspx", true);

                }
                else { 
                TextBoxName.Text = Session["ID"].ToString();
               // DL_TaskPeople.SelectedValue = TextBoxName.Text;
                TB_TaskDateEnd.Text = Calendar_TaskDate.TodaysDate.ToShortDateString();
                string dateInput = TB_TaskDateEnd.Text;
                var parsedDate = DateTime.Parse(dateInput);

                TB_TaskDateStart.Text = parsedDate.AddDays(-7).ToShortDateString();
                };


                //SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, TaskList.TaskID, the_date, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name,dbo.employee_id FROM TaskList INNER JOIN SystemList ON TaskList.System = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = TaskPeople WHERE the_date = '" + TB_TaskDate.Text + "' AND  TaskPeople = '" + TextBoxName.Text + "' ";
                return;
            }
        }
     

        protected void BT_Cal_Sel_CreateDate_Click(object sender, EventArgs e)
        {
            Calendar_TaskDate.Visible = true;
        }

     

        protected void Calendar_CreateDate_Sel(object sender, EventArgs e)
        {
            TB_TaskDateStart.Text = Calendar_TaskDate.SelectedDate.ToShortDateString();
            Calendar_TaskDate.Visible = false;
        }

        protected void BT_Cal_Sel_TaskDateEnd_Click(object sender, EventArgs e)
        {
            Calendar_TaskDateEnd.Visible = true;
        }
        protected void Calendar_TaskDateEnd_SelectionChanged(object sender, EventArgs e)
        {
            TB_TaskDateEnd.Text = Calendar_TaskDateEnd.SelectedDate.ToShortDateString();
            Calendar_TaskDateEnd.Visible = false;
        }

        protected void BT_Send_Click(object sender, EventArgs e)

        {


            string hiddenValue = hfSelectedValues.Value; // "A001,B002,C003"
            string[] items = hiddenValue.Split(',');
            // 每個加單引號
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = $"'{items[i]}'";
            }
            string TaskPeople = string.Join(",", items); // "'A001','B002','C003'"

            if (hfSelectedValues.Value == "" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )   AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%' and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )   AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%' and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };
            }
            else if (hfSelectedValues.Value != "" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {


                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "') AND  employee.employee_id in (" + TaskPeople + ")  AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };


            }

            else if (hfSelectedValues.Value == "" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )    and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )   and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };

            }

            else if (hfSelectedValues.Value == "" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )   and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )   and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };

            }

            else if (hfSelectedValues.Value == "" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )   and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )   and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };

            }

            else if (hfSelectedValues.Value != "" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')  AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON IL.IssueStatus<> 5 and  IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")   and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };

            }
            else if (hfSelectedValues.Value != "" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')  AND   employee.employee_id in (" + TaskPeople + ")  AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON IL.IssueStatus<> 5 and  IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id AND employee.employee_Employed = 1  ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")    and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")   and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };

            }

                       
            else {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate = '" + TB_TaskDateEnd.Text + "')  AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle  FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate = '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")   and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)  and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT IL.IssueTitle, IL.CreateDate, IssueID_SumTaskHour.SumTaskHours,(SELECT  SUM(TaskHours) AS SumTaskHours_Period FROM dbo.TaskList where  (TaskList.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') AND   employee.employee_id in (" + TaskPeople + ")   AND (IL.IssueType <> 5) and IssueID=IL.IssueID     GROUP BY IssueID) as SumTaskHours_Period, TaskList_1.TaskDate, TaskList_1.TaskDescription, SystemList.SystemName, employee.employee_name, employee.employee_id, IL.IssueDescription, TaskList_1.TaskID, IL.IssueID, IL.IssueStatus,  IssueGroup.IssueGroupTitle FROM IssueID_SumTaskHour INNER JOIN  IssueList AS IL ON  IL.IssueStatus<> 5 and IssueID_SumTaskHour.IssueID = IL.IssueID INNER JOIN IssueGroup ON IL.IssueGroupID = IssueGroup.IssueGroupID RIGHT OUTER JOIN SystemList INNER JOIN TaskList AS TaskList_1 ON SystemList.SystemID = TaskList_1.SystemID INNER JOIN employee ON TaskList_1.employee_id = employee.employee_id  AND employee.employee_Employed = 1 ON IL.IssueID = TaskList_1.IssueID WHERE   (TaskList_1.TaskDate BETWEEN '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "' )  AND   employee.employee_id in (" + TaskPeople + ")  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "' AND (IL.IssueType <> 5)   and TaskList_1.TaskDescription like '%" + TB_TaskDescription.Text + "%'  and  IL.IssueID not in (2683,2684) ORDER BY IL.IssueGroupID, IL.IssueTitle, SystemList.SystemName, IL.IssueStatus, TaskList_1.TaskDate, employee.employee_id";

                };



            };


        }

        protected void BT_Cal_Sel_TaskDate_Prev_Click(object sender, EventArgs e)
        {
            string dateInputStart = TB_TaskDateStart.Text;
            var parsedDateStart = DateTime.Parse(dateInputStart);
            string dateInputEnd = TB_TaskDateEnd.Text;
            var parsedDateEnd = DateTime.Parse(dateInputEnd);

            TB_TaskDateStart.Text = parsedDateStart.AddDays(-1).ToShortDateString();
            TB_TaskDateEnd.Text = parsedDateEnd.AddDays(-1).ToShortDateString();

        }

        protected void BT_Cal_Sel_TaskDate_Next_Click(object sender, EventArgs e)
        {
            string dateInputStart = TB_TaskDateStart.Text;
            var parsedDateStart = DateTime.Parse(dateInputStart);
            string dateInputEnd = TB_TaskDateEnd.Text;
            var parsedDateEnd = DateTime.Parse(dateInputEnd);

            TB_TaskDateEnd.Text = parsedDateEnd.AddDays(+1).ToShortDateString();
            TB_TaskDateStart.Text = parsedDateStart.AddDays(+1).ToShortDateString();
        }

        protected void GridView_WeeklyReport_DataBound1(object sender, EventArgs e)
        {
         
            int i;
            int rowcount = GridView_WeeklyReport.Rows.Count;
            string tempGridRowCell = "";
  
            string tempGridRowCell3 = "";
            string tempGridRowCellCreateIssueTime = "";
            for (i = 0; i < rowcount; i += 1)
            {


            


                if (GridView_WeeklyReport.Rows[i].Cells[1].Text != tempGridRowCellCreateIssueTime)
                {

                    tempGridRowCellCreateIssueTime = GridView_WeeklyReport.Rows[i].Cells[1].Text;
   
                }
                else
                {

                    GridView_WeeklyReport.Rows[i].Cells[1].Text = "";
         
                };

                HyperLink href = (HyperLink)GridView_WeeklyReport.Rows[i].Cells[2].Controls[0];
                string hyperlinktext = href.Text;

                if (href.Text != tempGridRowCell  )
                {
                 
                    tempGridRowCell = href.Text;

                    tempGridRowCell3 = GridView_WeeklyReport.Rows[i].Cells[5].Text;

                }
                else
                {

                    href.Text = "";
                    GridView_WeeklyReport.Rows[i].Cells[3].Text = "";
                    GridView_WeeklyReport.Rows[i].Cells[4].Text = "";
                    GridView_WeeklyReport.Rows[i].Cells[1].Text = "";
                    GridView_WeeklyReport.Rows[i].Cells[6].Text = "";
                    GridView_WeeklyReport.Rows[i].Cells[7].Text = "";
                    if (GridView_WeeklyReport.Rows[i].Cells[5].Text != tempGridRowCell3)
                    {

                        tempGridRowCell3 = GridView_WeeklyReport.Rows[i].Cells[5].Text;
                    }
                    else
                    {

                        GridView_WeeklyReport.Rows[i].Cells[5].Text = "";

                    };
                };


              

            };

        }

        protected void GridView_WeeklyReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_WeeklyReport.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                switch (e.Row.Cells[3].Text)
                {
                    case "0":

                        e.Row.Cells[3].Text = "新建";

                        break;
                    case "1":

                        e.Row.Cells[3].Text = "執行中";

                        break;
                    case "2":

                   
                        e.Row.Cells[3].Text = "已完成待上線";
                        break;
                    case "3":


                        e.Row.Cells[3].Text = "已完成待結案";
                        break;
                    case "4":

                        e.Row.Cells[12].Text = "";
                        e.Row.Cells[3].Text = "結案";
                        break;
                    case "5":

                        e.Row.Cells[12].Text = "";
                        e.Row.Cells[3].Text = "刪除";
                        break;
                }



            }
        }
    }
}