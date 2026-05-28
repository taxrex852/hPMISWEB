using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class ModifyIssue : System.Web.UI.Page
    {
        protected override void OnPreRenderComplete(EventArgs e)//要在prerrender時期才能處理預設項目，不然checkbox, DropDownList的資料庫資料尚未載入
        {

            // base.OnPreRenderComplete(e);
            SqlDataReader sqlDataReader2 = (SqlDataReader)SqlDataSource_PageLoad_IssueAssistantMap.Select(new DataSourceSelectArguments());
            while (sqlDataReader2.Read())//如果查詢的到，就取出IssueID
            {
                CBL_IssueManager.Items[Int16.Parse(sqlDataReader2["row_id"].ToString())].Selected = true;
            }
            // base.OnPreRenderComplete(e);
            SqlDataReader sqlDataReader = sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_SystemList.Select(new DataSourceSelectArguments());
            while (sqlDataReader.Read())//如果查詢的到，就取出IssueID
            {
                CBL_SystemList.Items[Int16.Parse(sqlDataReader["SystemID"].ToString()) - 1].Selected = true;
            }
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

                    try
                {
                    SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad.Select(new DataSourceSelectArguments());
                    if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                    {
                        LB_ID.Text = sqlDataReader["IssueID"].ToString();
                        TB_IssueDescription.Text = sqlDataReader["IssueDescription"].ToString();
                        TB_IssueTitle.Text = sqlDataReader["IssueTitle"].ToString();
                        TB_CreateDate.Text = sqlDataReader["CreateDate"].ToString();
                        DL_IssueType.SelectedValue = sqlDataReader["IssueType"].ToString();
                        DL_IssueManager.SelectedValue = sqlDataReader["employee_id"].ToString();
                        DL_IssueDeapartment.SelectedValue = sqlDataReader["DeapartmentID"].ToString();
                        TB_FinishDatePre.Text = sqlDataReader["FinishDatePre"].ToString();
                        TB_FinishDateAct.Text = sqlDataReader["FinishDateAct"].ToString();
                        DL_IssueStatus.SelectedValue = sqlDataReader["IssueStatus"].ToString();
                        DL_IssueGroup.SelectedValue = sqlDataReader["IssueGroupID"].ToString();


                    }
               
                }
                catch (Exception ex)
                {
                    LB_Message.Text = "載入資料過程發生錯誤。 " + ex.Message;
                    }


                }



                return;
            }

        }


        protected void BT_Cal_Sel_CreateDate_Click(object sender, EventArgs e)
        {
            Calendar_CreateDate.Visible = true;
        }

        protected void BT_Cal_Sel_FinishDatePre_Click(object sender, EventArgs e)
        {
            Calendar_FinishDatePre.Visible = true;
        }

        protected void BT_Cal_Sel_FinishDateAct_Click(object sender, EventArgs e)
        {
            Calendar_FinishDateAct.Visible = true;
        }

        protected void Calendar_CreateDate_Sel(object sender, EventArgs e)
        {
            TB_CreateDate.Text = Calendar_CreateDate.SelectedDate.ToShortDateString();
            Calendar_CreateDate.Visible = false;
        }

        protected void Calendar_FinishDatePre_Sel(object sender, EventArgs e)
        {
      
            TB_FinishDatePre.Text = Calendar_FinishDatePre.SelectedDate.ToShortDateString();
            Calendar_FinishDatePre.Visible = false;
        }

        protected void Calendar_FinishDateAct_Sel(object sender, EventArgs e)
        {
            TB_FinishDateAct.Text = Calendar_FinishDateAct.SelectedDate.ToShortDateString();
            Calendar_FinishDateAct.Visible = false;
        }

        protected void BT_Send_Click(object sender, EventArgs e)
        {
            try
            {
                String FinishDateAct = "NULL";
                if (TB_FinishDateAct.Text != "")//處理實際結案時間NULL問題
                    FinishDateAct = "'" + TB_FinishDateAct.Text + "'";

              

                if (DL_IssueStatus.SelectedValue == "3")
                {

                    SqlDataSource_sendlog.InsertCommand = "insert into Y6P2_Materials.dbo.IssueList_log " +
                    "         (Modify_Person, IssueID, Org_IssueTitle, Org_CreateDate, Org_FinishDatePre, Org_FinishDateAct, Org_IssueStatus, Org_employee_id, Org_IssueGroupID, Org_IssueDescription, Org_IssueType, Org_DeapartmentID, Org_IsDutyIssue, ModifyTime) " +
                    "      SELECT '" + TextBoxName.Text + "',IssueID, IssueTitle, CreateDate, FinishDatePre, FinishDateAct, IssueStatus, employee_id, IssueGroupID, IssueDescription, IssueType, DeapartmentID, IsDutyIssue,getdate() " +
                    "                 FROM Y6P2_Materials.dbo.IssueList WHERE IssueID = '" + LB_ID.Text + "'";
                    SqlDataSource_sendlog.Insert();



                    SqlDataSource_Send.UpdateCommand = "UPDATE IssueList SET " +
                 "IssueTitle = '" + TB_IssueTitle.Text + "', CreateDate = '" + TB_CreateDate.Text + "', FinishDatePre = '" + TB_FinishDatePre.Text +
                 "', FinishDateAct = CONVERT(varchar(20), GETDATE(), 120), IssueStatus = '" + DL_IssueStatus.SelectedValue + "', employee_id = '" + DL_IssueManager.SelectedValue + "',IssueGroupID = '" + DL_IssueGroup.SelectedValue +
                 "', IssueDescription = '" + TB_IssueDescription.Text + "', IssueType = '" + DL_IssueType.SelectedValue + "', DeapartmentID = '" + DL_IssueDeapartment.SelectedValue +
                 "' WHERE IssueID = '" + LB_ID.Text + "'";
                    SqlDataSource_Send.Update();


                }
                else {

                    SqlDataSource_sendlog.InsertCommand = "insert into Y6P2_Materials.dbo.IssueList_log " +
                "         (Modify_Person, IssueID, Org_IssueTitle, Org_CreateDate, Org_FinishDatePre, Org_FinishDateAct, Org_IssueStatus, Org_employee_id, Org_IssueGroupID, Org_IssueDescription, Org_IssueType, Org_DeapartmentID, Org_IsDutyIssue, ModifyTime) " +
                "      SELECT '" + TextBoxName.Text + "',IssueID, IssueTitle, CreateDate, FinishDatePre, FinishDateAct, IssueStatus, employee_id, IssueGroupID, IssueDescription, IssueType, DeapartmentID, IsDutyIssue,getdate() " +
                "                  FROM Y6P2_Materials.dbo.IssueList WHERE IssueID = '" + LB_ID.Text + "'";
                    SqlDataSource_sendlog.Insert();



                    SqlDataSource_Send.UpdateCommand = "UPDATE IssueList SET " +
                  "IssueTitle = '" + TB_IssueTitle.Text + "', CreateDate = '" + TB_CreateDate.Text + "', FinishDatePre = '" + TB_FinishDatePre.Text +
                  "', IssueStatus = '" + DL_IssueStatus.SelectedValue + "', employee_id = '" + DL_IssueManager.SelectedValue + "',IssueGroupID = '" + DL_IssueGroup.SelectedValue +
                  "', IssueDescription = '" + TB_IssueDescription.Text + "', IssueType = '" + DL_IssueType.SelectedValue + "', DeapartmentID = '" + DL_IssueDeapartment.SelectedValue +
                  "' WHERE IssueID = '" + LB_ID.Text + "'";
                    SqlDataSource_Send.Update();

                }



                               

                String IssueID = "0";//預設IssueID為0
                SqlDataSource_Send.SelectCommand = "SELECT IssueID,IssueTitle,CreateDate FROM IssueList WHERE IssueTitle='" + TB_IssueTitle.Text + "' AND CreateDate='" + TB_CreateDate.Text + "' and (IssueStatus = 0 OR IssueStatus = 1)ORDER BY IssueID DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    IssueID = sqlDataReader[0].ToString();
                    SqlDataSource_Send.DeleteCommand = "delete  from IssueSystemMap Where IssueID='" + IssueID + "'";
                    SqlDataSource_Send.Delete();
                    SqlDataSource_Send.DeleteCommand = "delete  from IssueListAssistantMap Where IssueID='" + IssueID + "'";
                    SqlDataSource_Send.Delete();
                    for (int i = 0; i < CBL_SystemList.Items.Count; i++)
                    {
                        if (CBL_SystemList.Items[i].Selected == true)
                        {
                            SqlDataSource_Send.InsertCommand = "INSERT INTO IssueSystemMap (IssueID, SystemID) VALUES( '"
                                + IssueID + "','" + CBL_SystemList.Items[i].Value + "')";
                            SqlDataSource_Send.Insert();

                        }
                    }

                    for (int i = 0; i < CBL_IssueManager.Items.Count; i++)
                    {
                        if (CBL_IssueManager.Items[i].Selected == true)
                        {
                            SqlDataSource_SendIssueAssistantMap.InsertCommand = "INSERT INTO dbo.IssueListAssistantMap (IssueID,employee_id) VALUES  ('" + IssueID + "', '" + CBL_IssueManager.Items[i].Value + "' )";

                            SqlDataSource_SendIssueAssistantMap.Insert();

                        }
                    }
                }
                LB_Message.Text = "修改資料完成，IssueID = " + IssueID;
            }
            catch (Exception ex)
            {
                LB_Message.Text = "修改資料過程發生錯誤\n" + ex.Message;
            }



        }

    }
}