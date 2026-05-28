using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class ModifyTask : System.Web.UI.Page
    {
        protected override void OnPreRenderComplete(EventArgs e)//要在prerrender時期才能處理預設項目，不然checkbox, DropDownList的資料庫資料尚未載入
        {
            // base.OnPreRenderComplete(e);
          
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
                    try
                    {
                        SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出TaskID
                        {

                            LB_TaskID.Text = sqlDataReader["TaskID"].ToString();
                            LB_IssueID.Text = sqlDataReader["IssueID"].ToString();
                            TB_IssueID.Text = sqlDataReader["IssueID"].ToString();
                            LB_IssueTitle.Text = sqlDataReader["IssueTitle"].ToString();
                            TB_TaskDate.Text = sqlDataReader["TaskDate"].ToString();
                            TB_TaskDescription.Text = sqlDataReader["TaskDescription"].ToString();
                            DL_TaskHours.SelectedValue = sqlDataReader["TaskHours"].ToString();
                            TB_HwDeviceID.Text = sqlDataReader["HwDeviceID"].ToString();
                            TB_SwDevID.Text = sqlDataReader["SwDevID"].ToString();
                            DL_TaskPeople.SelectedValue = sqlDataReader["employee_id"].ToString();
                            DL_System.SelectedValue = sqlDataReader["SystemID"].ToString();
                            LB_IssueStatus.Text = sqlDataReader["IssueStatus"].ToString();
                            TextBoxName.Text = Session["ID"].ToString();
                        }
                        if (LB_IssueStatus.Text == "3" || LB_IssueStatus.Text == "4")
                        {
                            BT_Send.Visible = false;

                        }

                        else
                        {
                            BT_Send.Visible = true;
                        };
                    }
                    catch (Exception ex)
                    {
                        LB_Message.Text = "載入資料過程發生錯誤。 " + ex.Message;
                    }


                }



                return;
            }
         
        }

        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {


                SqlDataSource_InsertSend.InsertCommand = "insert into Y6P2_Materials.dbo.TaskList_log " +
                "         (Modify_Person,TaskID,IssueID,Org_TaskDate,TaskDate,Org_employee_id,employee_id,Org_SystemID,SystemID,Org_TaskDescription,TaskDescription,Org_TaskHours,TaskHours,Org_HwDeviceID,HwDeviceID,Org_SwDevID,SwDevID,Delete_Flag,ModifyTime) " +
                "      SELECT '" + TextBoxName.Text + "',TaskID,IssueID,TaskDate,'" + TB_TaskDate.Text + "',employee_id, '" + DL_TaskPeople.SelectedValue + "',SystemID,'" + DL_System.SelectedValue + 
                "',TaskDescription,'" + TB_TaskDescription.Text + "',TaskHours,'" + DL_TaskHours.Text + "',HwDeviceID,'" + TB_HwDeviceID.Text + "',SwDevID,'" + TB_SwDevID.Text + "',0,getdate() " +
                "                 FROM Y6P2_Materials.dbo.TaskList WHERE TaskID =  '" + LB_TaskID.Text + "'";
                SqlDataSource_InsertSend.Insert();


                SqlDataSource_Send.UpdateCommand = "UPDATE TaskList SET " +
                      "employee_id = '" + DL_TaskPeople.SelectedValue + "', SystemID = '" + DL_System.SelectedValue + "', TaskDate = '" + TB_TaskDate.Text +
                    "',TaskHours = '" + DL_TaskHours.Text + "', TaskDescription = '" + TB_TaskDescription.Text + "', HwDeviceID = '" + TB_HwDeviceID.Text +
                  
                    "', SwDevID = '" + TB_SwDevID.Text +  "' WHERE TaskID = '" + LB_TaskID.Text + "'";
                SqlDataSource_Send.Update();
                LB_Message.Text = "修改資料完成，TaskID = '" + LB_TaskID.Text + "'";

                SqlDataSource_Send.UpdateCommand = "UPDATE TaskList_log SET " +
                      "employee_id = '" + DL_TaskPeople.SelectedValue + "', SystemID = '" + DL_System.SelectedValue + "', TaskDate = '" + TB_TaskDate.Text +
                    "',TaskHours = '" + DL_TaskHours.Text + "', TaskDescription = '" + TB_TaskDescription.Text + "', HwDeviceID = '" + TB_HwDeviceID.Text +

                    "', SwDevID = '" + TB_SwDevID.Text + "' WHERE TaskID = '" + LB_TaskID.Text + "' and convert(varchar,ModifyTime,120) = convert (varchar,getdate(),120)";
                SqlDataSource_Send.Update();
                LB_Message.Text = "修改資料完成，TaskID = '" + LB_TaskID.Text + "'";


            }
            catch (Exception ex)
            {
                LB_Message.Text = "修改資料過程發生錯誤\n" + ex.Message;
            }

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

        protected void BT_Del_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataSource_InsertSend.InsertCommand = "insert into Y6P2_Materials.dbo.TaskList_log " +
                               "         (Modify_Person,TaskID,IssueID,Org_TaskDate,TaskDate,Org_employee_id,employee_id,Org_SystemID,SystemID,Org_TaskDescription,TaskDescription,Org_TaskHours,TaskHours,Org_HwDeviceID,HwDeviceID,Org_SwDevID,SwDevID,Delete_Flag,ModifyTime) " +
                               "      SELECT '" + TextBoxName.Text + "',TaskID,IssueID,TaskDate,'" + TB_TaskDate.Text + "',employee_id, '" + DL_TaskPeople.SelectedValue + "',SystemID,'" + DL_System.SelectedValue +
                               "',TaskDescription,'" + TB_TaskDescription.Text + "',TaskHours,'" + DL_TaskHours.Text + "',HwDeviceID,'" + TB_HwDeviceID.Text + "',SwDevID,'" + TB_SwDevID.Text + "',1,getdate() " +
                               "                 FROM Y6P2_Materials.dbo.TaskList WHERE TaskID =  '" + LB_TaskID.Text + "'";
                SqlDataSource_InsertSend.Insert();

                SqlDataSource_Send.DeleteCommand = "Delete From TaskList  WHERE TaskID = '" + LB_TaskID.Text + "'";
                SqlDataSource_Send.Delete();
                LB_Message.Text = "刪除資料完成，TaskID = '" + LB_TaskID.Text + "'";
                


            }
            catch (Exception ex)
            {
                LB_Message.Text = "刪除資料過程發生錯誤\n" + ex.Message;
            }

        }
    }
}