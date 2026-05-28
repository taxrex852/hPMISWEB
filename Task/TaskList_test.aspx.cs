using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


namespace WebApplication2
{
    public partial class TaskList_test : System.Web.UI.Page
    {
        String QS_employeeID;
        String QS_IssueID;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. 先確認是否登入
            if (Object.Equals(Session["ID"], null))
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                string userId = Session["ID"].ToString();

                // 定義允許進入此頁面的帳號清單
                string[] allowedUsers = { "045627", "008197", "019126" };

                // 2. 檢查帳號是否在允許清單內
                if (!allowedUsers.Contains(userId))
                {
                    // 若不在清單內，跳出警告並導回首頁
          
                    Response.End(); // 停止繼續執行
                }
            }
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
                   
                    TB_TaskDateEnd.Text = Calendar_TaskDate.TodaysDate.ToShortDateString();
                    QS_employeeID = Request.QueryString["employee_id"];
                    QS_IssueID = Request.QueryString["IssueID"];
                    if (QS_employeeID != null)
                    {//若是ISSUE ID 有值，就在選擇主題清單那邊預設代入該ID
                        SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE employee.employee_id = '" + TextBoxNameQueryTemp.Text + "'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                    };
                    if (QS_IssueID != null)
                    {//若是ISSUE ID 有值，就在選擇主題清單那邊預設代入該ID
                        SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.IssueID = '" + QS_IssueID + "'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                    };
                    SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
                    //SqlConnection Conn = new SqlConnection("Data Source=10.108.137.235;Password=spcmgr;User ID=sa;Initial Catalog=Y6P2_Materials_test");
                    //SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Password=test;User ID=test;Initial Catalog=Y6P2_Materials");
                    Conn.Open();

                    SqlDataAdapter x_maxmin = new SqlDataAdapter("SELECT employee.employee_system_group,IssueGroup.IssueGroupTitle FROM employee left join dbo.IssueGroup on employee.employee_system_group=IssueGroup.IssueGroupID WHERE employee_id = '" + TextBoxName.Text + "' ", Conn);
                    DataSet x_maxminds = new DataSet();
                    x_maxmin.Fill(x_maxminds, "employee");
                    DataTable myTable = x_maxminds.Tables["employee"];
                    string myString1 = "";
                    string myString2 = "";
                    myString1 = myString1 + myTable.Rows[0]["employee_system_group"];
                    myString2 = myString2 + myTable.Rows[0]["IssueGroupTitle"];
                    if (myString1 != "")
                    {
                        DL_IssueGroupID.SelectedValue = myString2;
                    }


                    string dateInput = TB_TaskDateEnd.Text;
                var parsedDate = DateTime.Parse(dateInput);

                TB_TaskDateStart.Text = parsedDate.AddDays(-1).ToShortDateString();
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
         
            if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'   and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";

                };
            }
            else if (DL_TaskPeople.SelectedValue != "ALL" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {


                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";

                };


            }

            else if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'   and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";

                };

            }

            else if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  and  IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName ";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')   and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                };

            }

            else if (DL_TaskPeople.SelectedValue == "ALL" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                };

            }

            else if (DL_TaskPeople.SelectedValue != "ALL" && DL_SystemList.SelectedValue != "ALL" && DL_IssueGroupID.SelectedValue == "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "'   and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                };

            }
            else if (DL_TaskPeople.SelectedValue != "ALL" && DL_SystemList.SelectedValue == "ALL" && DL_IssueGroupID.SelectedValue != "ALL")
            {

                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'   and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%' ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

                };

            }


            else
            {

                
                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "'  and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";
                }
                else
                {
                    SqlDataSource_TaskList.SelectCommand = "SELECT TaskList.IssueID, IssueList.IssueTitle, TaskList.TaskID, TaskList.TaskDate, TaskList.TaskDescription, TaskList.TaskHours, SystemList.SystemName, employee.employee_name, employee.employee_id, IssueList.IssueStatus, IssueGroup.IssueGroupTitle FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 INNER JOIN IssueList ON TaskList.IssueID = IssueList.IssueID INNER JOIN IssueGroup ON IssueList.IssueGroupID = IssueGroup.IssueGroupID WHERE (TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "')  AND  employee.employee_id = '" + DL_TaskPeople.SelectedValue + "' and SystemList.SystemName = '" + DL_SystemList.SelectedValue + "' and IssueGroup.IssueGroupTitle = '" + DL_IssueGroupID.SelectedValue + "'  and TaskList.TaskDescription like '%" + TB_TaskDescription.Text + "%'  ORDER BY TaskList.TaskDate DESC, employee.employee_id, SystemList.SystemName";

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

        protected void GridView_TaskList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            GridView_TaskList.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                switch (e.Row.Cells[4].Text)
                {
                    case "0":

                        e.Row.Cells[4].Text = "新建";

                        break;
                    case "1":

                        e.Row.Cells[4].Text = "執行中";

                        break;
                    case "2":

                     
                        e.Row.Cells[4].Text = "已完成待上線";
                        break;
                    case "3":


                        e.Row.Cells[4].Text = "已完成待結案";
                        break;
                    case "4":

                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[4].Text = "結案";
                        break;
                    case "5":

                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[4].Text = "刪除";
                        break;
                }



            }

        }

        protected void BT_Print_Click(object sender, EventArgs e)
        {
            string dateInputStart = TB_TaskDateStart.Text;
            var parsedDateStart = DateTime.Parse(dateInputStart);
            string dateInputEnd = TB_TaskDateEnd.Text;
            var parsedDateEnd = DateTime.Parse(dateInputEnd);
            double timespan = new TimeSpan(DateTime.Parse(dateInputEnd).Ticks - DateTime.Parse(dateInputStart).Ticks).Days;

            //for (int i = 0; i <= timespan; i++)
            //{
                //string dateInput = TB_TaskDateStart.Text;
                //var parsedDate1 = DateTime.Parse(dateInput).AddDays(+i);
                ReportDocument rd = new ReportDocument();
              
                string reportPath = Server.MapPath("CrystalReport6.rpt");

                SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
                Conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("select " +
                    "case when id > 1 then '' else employee_name end as employee_name," +
                    "a.SystemName, a.TaskDescription " +
                    "from ( " +
                    "SELECT ROW_NUMBER() over(partition by employee.employee_name, TaskDate " +
                    "ORDER BY TaskList.TaskDate DESC, employee.employee_id) ID," +
                    "TaskDate,employee.employee_id,employee.employee_name,SystemList.SystemName," +
                    "IssueList.IssueTitle+'__'+TaskList.TaskDescription as TaskDescription " +
                    "FROM (SELECT t.* FROM tasklist t LEFT JOIN IssueList i ON t.IssueID = i.IssueID  WHERE  (t.employee_id NOT IN ('045627','019126')) OR (t.employee_id IN ('045627', '019126') AND LEN(t.TaskDescription) < 90 AND ( i.IssueID = '34471' OR i.IssueTitle LIKE '%請假%' OR  i.IssueTitle LIKE '%庶務%' OR  i.IssueTitle LIKE '%點檢%')) UNION ALL SELECT * FROM TaskList_ORG WHERE employee_id IN ('045627','019126')) TaskList  inner join " +
                    "IssueList on TaskList.IssueID=IssueList.IssueID and IssueList.IssueID not in (2683,2684) " +
                    "INNER JOIN SystemList ON TaskList.SystemID = SystemList.SystemID " +
                    "INNER JOIN employee ON TaskList.employee_id = employee.employee_id  AND employee.employee_Employed = 1 " +
                    "WHERE TaskList.TaskDate = '" + parsedDateEnd.ToShortDateString() + "' ) a" +
                    " ORDER BY TaskDate DESC, employee_id", Conn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "TaskList_RPT");

                rd.Load(reportPath);

                rd.SetDataSource(ds);

                System.Globalization.TaiwanCalendar tc = new System.Globalization.TaiwanCalendar();
                string twdate = string.Format("{0}年{1}月{2}日", tc.GetYear(parsedDateEnd), tc.GetMonth(parsedDateEnd), tc.GetDayOfMonth(parsedDateEnd));
                rd.SetParameterValue("年月日", twdate.ToString());
                rd.SetParameterValue("星期", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(parsedDateEnd.DayOfWeek));
                //DiskFileDestinationOptions DiskOption = new DiskFileDestinationOptions();
                //PdfRtfWordFormatOptions Pdfoption = new PdfRtfWordFormatOptions();
                //DiskOption.DiskFileName = "C:";
                //ExportOptions crexportoption = rd.ExportOptions;
                //{
                //    crexportoption.ExportDestinationType = ExportDestinationType.DiskFile;
                //    crexportoption.ExportFormatType = ExportFormatType.PortableDocFormat;
                //    crexportoption.ExportDestinationOptions = DiskOption;
                //    crexportoption.ExportFormatOptions = Pdfoption;
                //}
                //rd.Export();
                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=工作日誌.pdf");
              
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, "工作日誌");
                //Response.Flush();

                rd.Close();
                rd.Dispose();
                         
                Conn.Close();
                Conn.Dispose();

            //}
           
        }
    }
}