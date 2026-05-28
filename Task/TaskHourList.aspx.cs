using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class TaskHourList : System.Web.UI.Page
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
                DL_TaskPeople.SelectedValue = TextBoxName.Text;
                TB_TaskDateEnd.Text = Calendar_TaskDate.TodaysDate.ToShortDateString();
                string dateInput = TB_TaskDateEnd.Text;
                var parsedDate = DateTime.Parse(dateInput);

                TB_TaskDateStart.Text = parsedDate.AddDays(-7).ToShortDateString();
                };


                //SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, TaskList.TaskID, the_date, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name,dbo.employee_id FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = TaskPeople WHERE the_date = '" + TB_TaskDate.Text + "' AND  TaskPeople = '" + TextBoxName.Text + "' ";
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
            if (DL_TaskPeople.SelectedValue == "ALL")
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT DISTINCT CONVERT(Varchar(10), dbo.time_dimension.the_date, 111) AS the_date, dbo.time_dimension.the_workhour,dbo.employee.employee_id, dbo.employee.employee_name,(CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily,(CASE when dbo.time_dimension.the_workhour = 0 then 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 else dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM dbo.employee CROSS JOIN dbo.time_dimension LEFT OUTER JOIN dbo.Empolyee_SumTaskHour_Daily ON  dbo.time_dimension.the_date = dbo.Empolyee_SumTaskHour_Daily.TaskDate AND  dbo.employee.employee_id = dbo.Empolyee_SumTaskHour_Daily.employee_id  AND employee.employee_Employed = 1  WHERE time_dimension.the_date =  '" + TB_TaskDateEnd.Text + "'  order by the_date, employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT DISTINCT CONVERT(Varchar(10), dbo.time_dimension.the_date, 111) AS the_date, dbo.time_dimension.the_workhour,dbo.employee.employee_id, dbo.employee.employee_name,(CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily,(CASE when dbo.time_dimension.the_workhour = 0 then 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 else dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM dbo.employee CROSS JOIN dbo.time_dimension LEFT OUTER JOIN dbo.Empolyee_SumTaskHour_Daily ON  dbo.time_dimension.the_date = dbo.Empolyee_SumTaskHour_Daily.TaskDate AND  dbo.employee.employee_id = dbo.Empolyee_SumTaskHour_Daily.employee_id  AND employee.employee_Employed = 1 WHERE time_dimension.the_date between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'order by the_date, employee_id";

                };
            }
            else {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT DISTINCT CONVERT(Varchar(10), dbo.time_dimension.the_date, 111) AS the_date, dbo.time_dimension.the_workhour,dbo.employee.employee_id, dbo.employee.employee_name,(CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily,(CASE when dbo.time_dimension.the_workhour = 0 then 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 else dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM dbo.employee CROSS JOIN dbo.time_dimension LEFT OUTER JOIN dbo.Empolyee_SumTaskHour_Daily ON  dbo.time_dimension.the_date = dbo.Empolyee_SumTaskHour_Daily.TaskDate AND  dbo.employee.employee_id = dbo.Empolyee_SumTaskHour_Daily.employee_id  AND employee.employee_Employed = 1 WHERE time_dimension.the_date =  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "' order by the_date, employee_id";
                }
                else
                {
                    SqlDataSource_SumTaskHourList.SelectCommand = "SELECT DISTINCT CONVERT(Varchar(10), dbo.time_dimension.the_date, 111) AS the_date, dbo.time_dimension.the_workhour,dbo.employee.employee_id, dbo.employee.employee_name,(CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily,(CASE when dbo.time_dimension.the_workhour = 0 then 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 else dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM dbo.employee CROSS JOIN dbo.time_dimension LEFT OUTER JOIN dbo.Empolyee_SumTaskHour_Daily ON  dbo.time_dimension.the_date = dbo.Empolyee_SumTaskHour_Daily.TaskDate AND  dbo.employee.employee_id = dbo.Empolyee_SumTaskHour_Daily.employee_id  AND employee.employee_Employed = 1 WHERE time_dimension.the_date between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "' order by the_date, employee_id";

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

      
    }
}