using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2
{
    public partial class IssueDutyListManage : System.Web.UI.Page
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
                    TextBoxNameQueryTemp.Text = Session["ID"].ToString();
                    // DL_TaskPeople.SelectedValue = TextBoxName.Text;
                  
                  TB_TaskDateStart.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToShortDateString();
                    
                  TB_TaskDateEnd.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToShortDateString();

                    TB_TaskDateStartTemp.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToShortDateString()+" 00:00:00";

                    TB_TaskDateEndTemp.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59";

                };


                //SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name,dbo.employee.employee_id FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id WHERE TaskList.TaskDate = '" + TB_TaskDate.Text + "' AND  employee.employee_id = '" + TextBoxName.Text + "' ";
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
         
            if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue == "ALL" )
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start =  '" + TB_TaskDateStart.Text + " 00:00:00'    ORDER BY Duty_Start desc ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start  between  '" + TB_TaskDateStart.Text + " 00:00:00'  AND  '" + TB_TaskDateEnd.Text + " 23:59:59'    ORDER BY Duty_Start desc ";

                };
            }
            else if (DL_TaskPeople.SelectedValue != "ALL" && DL_SystemList.SelectedValue == "ALL" )
            {


                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start =  '" + TB_TaskDateStart.Text + " 00:00:00'  AND  Duty_Person = '" + DL_TaskPeople.SelectedValue + "'   ORDER BY Duty_Start desc ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start between  '" + TB_TaskDateStart.Text + " 00:00:00' AND  '" + TB_TaskDateEnd.Text + " 23:59:59'   AND  Duty_Person = '" + DL_TaskPeople.SelectedValue + "'   ORDER BY Duty_Start desc";

                };


            }

            else if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue != "ALL" )
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start =  '" + TB_TaskDateStart.Text + " 00:00:00'  and SystemName = '" + DL_SystemList.SelectedValue + "' ORDER BY Duty_Start desc ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start between  '" + TB_TaskDateStart.Text + " 00:00:00' AND  '" + TB_TaskDateEnd.Text + " 23:59:59'   and SystemName = '" + DL_SystemList.SelectedValue + "' ORDER BY Duty_Start desc  ";

                };

            }

         
            else
            {

                
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start =   '" + TB_TaskDateEnd.Text + " 00:00:00'  AND  Duty_Person = '" + DL_TaskPeople.SelectedValue + "'  and SystemName = '" + DL_SystemList.SelectedValue + "' ORDER BY Duty_Start desc ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE Duty_Start between  '" + TB_TaskDateStart.Text + " 00:00:00' AND  '" + TB_TaskDateEnd.Text + " 23:59:59'  AND  Duty_Person = '" + DL_TaskPeople.SelectedValue + "'  and SystemName = '" + DL_SystemList.SelectedValue + "' ORDER BY Duty_Start desc";

                };



            };


        }

        protected void BT_Cal_Sel_TaskDate_Prev_Click(object sender, EventArgs e)
        {
           
            TB_TaskDateStart.Text = DateTime.Parse(TB_TaskDateStart.Text).AddDays(1 - DateTime.Parse(TB_TaskDateStart.Text).Day).AddMonths(-1).ToShortDateString() ;
            TB_TaskDateEnd.Text = DateTime.Parse(TB_TaskDateStart.Text).AddDays(1 - DateTime.Parse(TB_TaskDateStart.Text).Day).AddMonths(1).AddDays(-1).ToShortDateString() ;

        }

        protected void BT_Cal_Sel_TaskDate_Next_Click(object sender, EventArgs e)
        {
            
            TB_TaskDateStart.Text = DateTime.Parse(TB_TaskDateStart.Text).AddDays(1 - DateTime.Parse(TB_TaskDateStart.Text).Day).AddMonths(1).ToShortDateString() ;
            TB_TaskDateEnd.Text = DateTime.Parse(TB_TaskDateStart.Text).AddDays(1 - DateTime.Parse(TB_TaskDateStart.Text).Day).AddMonths(1).AddDays(-1).ToShortDateString() ;
        }

    

        protected void BT_Issuestate_Click(object sender, EventArgs e)
        {

            SqlDataSource_TaskList.SelectCommand = "SELECT * from vw_issue_DutyManager WHERE IssueStatus ='未結案' ORDER BY Duty_Start desc";

        }
    }
}