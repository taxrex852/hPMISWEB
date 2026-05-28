using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{

 
    public partial class ModifyIssueDuty : System.Web.UI.Page
    {


        protected override void OnPreRenderComplete(EventArgs e)//要在prerrender時期才能處理預設項目，不然checkbox, DropDownList的資料庫資料尚未載入
        {
            // base.OnPreRenderComplete(e);
            SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_IssueAssistantMap.Select(new DataSourceSelectArguments());
            while (sqlDataReader.Read())//如果查詢的到，就取出IssueID
            {
                CBL_IssueManager.Items[Int16.Parse(sqlDataReader["row_id"].ToString())].Selected = true;
            }

            SqlDataReader sqlDataReader2 = (SqlDataReader)SqlDataSource_PageLoad_SystemList.Select(new DataSourceSelectArguments());
            while (sqlDataReader2.Read())//如果查詢的到，就取出IssueID
            {
                CBL_SystemList.Items[Int16.Parse(sqlDataReader2["SystemID"].ToString()) - 1].Selected = true;
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
                       
                            TB_FinishDatePre2.Text= sqlDataReader["FinishDatePre"].ToString();
                            DL_IssueStatus.SelectedValue = sqlDataReader["IssueStatus"].ToString();
                            DL_IssueGroup.SelectedValue = sqlDataReader["IssueGroupID"].ToString();


                        }

                        sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_Duty_AssistantStyleMap.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                        {

                            DL_Duty_AssistantStyleList.SelectedValue = sqlDataReader["Duty_AssistantStyleID"].ToString();

                        }
                        sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_Duty_Fail_LevelMap.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                        {
                            DL_Duty_Fail_LevelList.SelectedValue = sqlDataReader["Duty_Fail_LevelID"].ToString();

                        }
                        sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_Duty_Fail_StyleMap.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                        {

                            DL_Duty_Fail_StyleList.SelectedValue = sqlDataReader["Duty_Fail_StyleID"].ToString();

                        }

                        sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_IssueDutyDetail.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                        {

                            DL_IssueManager_Duty.SelectedValue = sqlDataReader["Duty_Person"].ToString();

                        }
                     

                        sqlDataReader = (SqlDataReader)SqlDataSource_PageLoad_IssueDuty.Select(new DataSourceSelectArguments());
                        if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                        {
                           
                            DL_StartHour.SelectedValue = sqlDataReader["Duty_Start"].ToString().Substring(8, 2);

                            DL_StartMinute.SelectedValue = sqlDataReader["Duty_Start"].ToString().Substring(10, 2);

                            TB_FinishDatePre.Text = sqlDataReader["Duty_End"].ToString().Substring(0, 4) + "-" + sqlDataReader["Duty_End"].ToString().Substring(4, 2) + "-" + sqlDataReader["Duty_End"].ToString().Substring(6, 2);

                            DL_EndHour.SelectedValue = sqlDataReader["Duty_End"].ToString().Substring(8, 2);

                            DL_EndMinute.SelectedValue = sqlDataReader["Duty_End"].ToString().Substring(10, 2);

                            LabelTimeDiff.Text = sqlDataReader["Duty_Work_Time"].ToString();

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
        protected void BT_Cal_Sel_TB_FinishDatePre2_Click(object sender, EventArgs e)
        {
            Calendar_FinishDatePre2.Visible = true;


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
        protected void Calendar_FinishDatePre2_Sel(object sender, EventArgs e)
        {
            TB_FinishDatePre2.Text = Calendar_FinishDatePre2.SelectedDate.ToShortDateString();
            Calendar_FinishDatePre2.Visible = false;

        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {
            DateTime StartDate;
            DateTime EndDate;
            int StartHour = int.Parse(DL_StartHour.SelectedValue);
            int StartMinute = int.Parse(DL_StartMinute.SelectedValue);
            int EndHour = int.Parse(DL_EndHour.SelectedValue);
            int EndMinute = int.Parse(DL_EndMinute.SelectedValue);
       
           
            if (StartHour >= 0 && StartHour <= 11 ) {

            StartDate = DateTime.Parse(TB_CreateDate.Text + "上午" + DL_StartHour.SelectedValue + ":" + DL_StartMinute.SelectedValue);

                if (EndHour >= 0 && EndHour <= 11)
                {

                    EndDate = DateTime.Parse(TB_FinishDatePre.Text + "上午" + DL_EndHour.SelectedValue + ":" + DL_EndMinute.SelectedValue);

                    TimeSpan time = EndDate - StartDate;

                    LabelTimeDiff.Text = time.TotalMinutes.ToString();

                }
                else if (EndHour >= 12 && EndHour <= 23)
                {


                    EndDate = DateTime.Parse(TB_FinishDatePre.Text + "下午" + DL_EndHour.SelectedValue + ":" + DL_EndMinute.SelectedValue);

                    TimeSpan time = EndDate - StartDate;


                    LabelTimeDiff.Text = time.TotalMinutes.ToString();
                }

            }
            else if (StartHour >= 12 && StartHour <= 23) {


            StartDate = DateTime.Parse(TB_CreateDate.Text + "下午" + DL_StartHour.SelectedValue + ":" + DL_StartMinute.SelectedValue);

                if (EndHour >= 0 && EndHour <= 11)
                {

                    EndDate = DateTime.Parse(TB_FinishDatePre.Text + "上午" + DL_EndHour.SelectedValue + ":" + DL_EndMinute.SelectedValue);

                    TimeSpan time = EndDate - StartDate;


                    LabelTimeDiff.Text = time.TotalMinutes.ToString();

                }
                else if (EndHour >= 12 && EndHour <= 23)
                {


                    EndDate = DateTime.Parse(TB_FinishDatePre.Text + "下午" + DL_EndHour.SelectedValue + ":" + DL_EndMinute.SelectedValue);

                    TimeSpan time = EndDate - StartDate;


                    LabelTimeDiff.Text = time.TotalMinutes.ToString();
                }
               

            }


           
            string DatetimeStart = TB_CreateDate.Text.Substring(0,10).Replace("-", "")  + DL_StartHour.SelectedValue + DL_StartMinute.SelectedValue;
            string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 10).Replace("-", "") + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;
            try 
            {
                SqlDataSource_Send.UpdateCommand =  "UPDATE IssueList SET " +
                    "IssueTitle = '" + TB_IssueTitle.Text + "', CreateDate = '" + TB_CreateDate.Text + "', FinishDatePre = '" + TB_FinishDatePre2.Text +
                    "', FinishDateAct = CONVERT(varchar(20), GETDATE(), 120), IssueStatus = '" + DL_IssueStatus.SelectedValue + "', employee_id = '" + DL_IssueManager.SelectedValue + "',IssueGroupID = '" + DL_IssueGroup.SelectedValue +
                    "', IssueDescription = '" + TB_IssueDescription.Text + "', IssueType = '" + DL_IssueType.SelectedValue + "', DeapartmentID = '" + DL_IssueDeapartment.SelectedValue +
                    "' WHERE IssueID = '" + LB_ID.Text + "'";


                SqlDataSource_Send.Update();

                String IssueID="0";//預設IssueID為0
                SqlDataSource_Send.SelectCommand = "SELECT IssueID,IssueTitle,CreateDate FROM IssueList WHERE IssueTitle='" + TB_IssueTitle.Text + "' AND CreateDate='" + TB_CreateDate.Text + "' ORDER BY IssueID DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());



                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    IssueID = sqlDataReader[0].ToString();


                    SqlDataSource_SendDuty_Fail_LevelMap.DeleteCommand = "delete  from Duty_Fail_LevelMap Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendDuty_Fail_LevelMap.Delete();

                    SqlDataSource_SendDuty_Fail_StyleMap.DeleteCommand = "delete  from Duty_Fail_StyleMap Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendDuty_Fail_StyleMap.Delete();

                    SqlDataSource_SendAssistantStyleMap.DeleteCommand = "delete  from Duty_AssistantStyleMap Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendAssistantStyleMap.Delete();

                    SqlDataSource_SendIssueDuty.DeleteCommand = "delete  from IssueDuty Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendIssueDuty.Delete();

                    SqlDataSource_SendIssueDutyDetail.DeleteCommand = "delete  from IssueDutyDetail Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendIssueDutyDetail.Delete();

                    SqlDataSource_SendIssueSystemMap.DeleteCommand = "delete  from IssueSystemMap Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendIssueSystemMap.Delete();

                    SqlDataSource_SendIssueAssistantMap.DeleteCommand = "delete  from IssueAssistantMap Where IssueID='" + IssueID + "'";

                    SqlDataSource_SendIssueAssistantMap.Delete();



                    SqlDataSource_SendDuty_Fail_LevelMap.InsertCommand += "INSERT INTO dbo.Duty_Fail_LevelMap (IssueID,Duty_Fail_LevelID) VALUES  ('" + IssueID + "', '" + DL_Duty_Fail_LevelList.SelectedValue+ "' )";

                    SqlDataSource_SendDuty_Fail_LevelMap.Insert();

                    SqlDataSource_SendDuty_Fail_StyleMap.InsertCommand += "INSERT INTO dbo.Duty_Fail_StyleMap (IssueID,Duty_Fail_StyleID) VALUES  ('" + IssueID + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "' )";

                    SqlDataSource_SendDuty_Fail_StyleMap.Insert();

                    SqlDataSource_SendAssistantStyleMap.InsertCommand += "INSERT INTO dbo.Duty_AssistantStyleMap (IssueID,Duty_AssistantStyleID) VALUES  ('" + IssueID + "', '" + DL_Duty_AssistantStyleList.SelectedValue + "' )";

                    SqlDataSource_SendAssistantStyleMap.Insert();

                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                    SqlDataSource_SendIssueDuty.Insert();

                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                    SqlDataSource_SendIssueDutyDetail.Insert();


               



                    for (int i = 0; i < CBL_SystemList.Items.Count; i++)
                    {
                        if (CBL_SystemList.Items[i].Selected == true)
                        {
                            SqlDataSource_SendIssueSystemMap.InsertCommand = "INSERT INTO IssueSystemMap (IssueID, SystemID) VALUES( '"
                                + IssueID + "','" +  CBL_SystemList.Items[i].Value + "')";

                             SqlDataSource_SendIssueSystemMap.Insert();
                          

                        }
                    }
                    

                    for (int i = 0; i < CBL_IssueManager.Items.Count; i++)
                    {
                        if (CBL_IssueManager.Items[i].Selected == true)
                        {
                            SqlDataSource_SendIssueAssistantMap.InsertCommand = "INSERT INTO dbo.IssueAssistantMap (IssueID,employee_id) VALUES  ('" + IssueID + "', '" + CBL_IssueManager.Items[i].Value + "' )";

                            SqlDataSource_SendIssueAssistantMap.Insert();

                        }
                    }
                   
                }
                LB_Message.Text = "新增資料完成，IssueID = " + IssueID;
            }
            catch(Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message  ;
            }



        }
    }
}