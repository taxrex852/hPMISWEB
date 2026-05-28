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
    public partial class CreateTask : System.Web.UI.Page
    {
        String QS_IssueID;
        protected override void OnPreRenderComplete(EventArgs e)//要在prerrender時期才能處理預設項目，不然checkbox, DropDownList的資料庫資料尚未載入
        {
            if (QS_IssueID != null)
            {
                SqlDataReader sqlDataReader = sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad.Select(new DataSourceSelectArguments());
                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    TBTempissuestatus.Text = sqlDataReader["IssueStatus"].ToString();

                };
            };
         
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
     
            if (!IsPostBack)
            {
             

                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                   Response.Redirect("Login.aspx", true);

                             }
                else
                {
                
                        TextBoxName.Text = Session["ID"].ToString();
                    TB_TaskDate.Text = Calendar_TaskDate.TodaysDate.ToShortDateString();
                    DL_TaskPeople.SelectedValue = TextBoxName.Text;
                    QS_IssueID = Request.QueryString["IssueID"];
       
                
                    if (QS_IssueID != null)
                    {//若是ISSUE ID 有值，就在選擇主題清單那邊預設代入該ID
                        DL_IssueSel.SelectedValue = QS_IssueID;
                    
                    };
                }
               


                return;
            }
          

        }

        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {
                SqlDataSource_Send.InsertCommand = "INSERT INTO TaskList " +
                   "(IssueID, TaskDate, employee_id, SystemID, TaskDescription, TaskHours, HwDeviceID, SwDevID) VALUES( '"
                   + DL_IssueSel.SelectedValue + "','" + TB_TaskDate.Text + "','" + DL_TaskPeople.SelectedValue + "','" + DL_System.SelectedValue + "','" + TB_TaskDescription.Text + "','"
                    + DL_TaskHours.SelectedValue + "','" + TB_HwDeviceID.Text + "','" + TB_SwDevID.Text + "')";
                SqlDataSource_Send.Insert();

                String TaskID = "0";//預設IssueID為0
                SqlDataSource_Send.SelectCommand = "SELECT TaskID FROM TaskList WHERE IssueID='" + DL_IssueSel.SelectedValue +
                    "' AND TaskDate='" + TB_TaskDate.Text + "' AND employee_id='" + DL_TaskPeople.SelectedValue +
                    "' AND SystemID='" + DL_System.SelectedValue + "' AND TaskDescription='" + TB_TaskDescription.Text +
                    "' AND TaskHours='" + DL_TaskHours.SelectedValue + "' ORDER BY TaskID DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    TaskID = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，TaskID = " + TaskID;
                }

                if (TBTempissuestatus.Text == "0")
                {

                    SqlDataSource_Send.UpdateCommand = "UPDATE IssueList SET IssueStatus = 1  WHERE IssueID = '" + DL_IssueSel.SelectedValue + "'";
                    SqlDataSource_Send.Update();

                }

            }
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }
            // SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name FROM TaskList INNER JOIN SystemList ON TaskList.System = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID WHERE TaskList.TaskDate = '" + TB_TaskDate.Text + "' and TaskList.IssueID = '" + DL_IssueSel.SelectedValue + "' order by  TaskList.TaskDate desc ";
            SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID WHERE  TaskList.IssueID = '" + DL_IssueSel.SelectedValue + "' order by  TaskList.TaskDate desc ";
        }

        protected void BT_Cal_Sel_CreateDate_Click(object sender, EventArgs e)
        {
            Calendar_TaskDate.Visible = true;
        }

        protected void Calendar_CreateDate_Sel(object sender, EventArgs e)
        {
            TB_TaskDate.Text = Calendar_TaskDate.SelectedDate.ToShortDateString();
            Calendar_TaskDate.Visible = false;
        }


        protected void SqlDataSource_IssueSel_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            e.ExceptionHandled = true;  //--表示我們自己處理例外狀況（exception）！            
            if (e.Exception != null)
                Response.Redirect("CreateTask.aspx", true);

        }

        protected void DL_IssueSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            QS_IssueID = null;


        }

        protected void BT_fileter_Click(object sender, EventArgs e)
        {
            if (TB_FileterTasklist.Text!="") {
                SqlDataSource_IssueSel.SelectCommand = "(select * from VW_Issue_List" +
                    " where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%') " +
                    "UNION " +
                    "(select * from VW_Issue_DutyList" +
                    " where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%') " +
                    "ORDER BY IsDutyIssue desc,IssueGroupID, IssueTitle,CreateDate, employee_id ";
                SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID WHERE  TaskList.IssueID = (select top 1 a.issueID from ((select * from VW_Issue_List where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%' ) UNION (select * from VW_Issue_DutyList where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%' ) ) as a   ORDER BY IsDutyIssue desc,IssueGroupID, IssueTitle,CreateDate, employee_id) order by  TaskList.TaskDate desc ";
                SqlDataSource_PageLoad_SystemList.SelectCommand = " SELECT IssueSystemMap.IssueID, IssueSystemMap.SystemID, SystemList.SystemName FROM IssueSystemMap INNER JOIN SystemList ON IssueSystemMap.SystemID = SystemList.SystemID WHERE(IssueSystemMap.IssueID = (select top 1 a.issueID from((select * from VW_Issue_List where (IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%') UNION(select * from VW_Issue_DutyList where (IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') and IssueTitle like '%" + TB_FileterTasklist.Text + "%' ) ) as a   ORDER BY IsDutyIssue desc, IssueGroupID, IssueTitle, CreateDate, employee_id))";
            }
            else {
                SqlDataSource_IssueSel.SelectCommand = "(select * from VW_Issue_List" +
                    " where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') ) " +
                    "UNION " +
                    "(select * from VW_Issue_DutyList" +
                    " where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') ) " +
                    "ORDER BY IsDutyIssue desc,IssueGroupID, IssueTitle,CreateDate, employee_id ";
                SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name FROM TaskList INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID WHERE  TaskList.IssueID = (select top 1 a.issueID from ((select * from VW_Issue_List where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') ) UNION (select * from VW_Issue_DutyList where ( IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') ) ) as a   ORDER BY IsDutyIssue desc,IssueGroupID, IssueTitle,CreateDate, employee_id) order by  TaskList.TaskDate desc ";
                SqlDataSource_PageLoad_SystemList.SelectCommand = " SELECT IssueSystemMap.IssueID, IssueSystemMap.SystemID, SystemList.SystemName FROM IssueSystemMap INNER JOIN SystemList ON IssueSystemMap.SystemID = SystemList.SystemID WHERE(IssueSystemMap.IssueID = (select top 1 a.issueID from((select * from VW_Issue_List where (IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2') ) UNION(select * from VW_Issue_DutyList where (IssueStatus = '0' or IssueStatus = '1' or IssueStatus = '2')  ) ) as a   ORDER BY IsDutyIssue desc, IssueGroupID, IssueTitle, CreateDate, employee_id))";


            }
        }
    }
}