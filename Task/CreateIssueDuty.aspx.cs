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

public partial class CreateIssueDuty : System.Web.UI.Page
    {

    

 


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
               
                    //DL_IssueManager_Duty.SelectedValue = TextBoxName.Text;
                    //DL_IssueManager.SelectedValue = TextBoxName.Text;
                    SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
                    //SqlConnection Conn = new SqlConnection("Data Source=10.108.137.235;Password=spcmgr;User ID=sa;Initial Catalog=Y6P2_Materials_test");
                    //SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Password=test;User ID=test;Initial Catalog=Y6P2_Materials");
                    Conn.Open();

                    SqlDataAdapter x_maxmin = new SqlDataAdapter("SELECT [employee_system_group] FROM [employee] WHERE employee_id = '" + TextBoxName.Text + "' ", Conn);
                    DataSet x_maxminds = new DataSet();
                    x_maxmin.Fill(x_maxminds, "employee");
                    DataTable myTable = x_maxminds.Tables["employee"];
                    string myString1 = "";
                    myString1 = myString1 + myTable.Rows[0]["employee_system_group"];

                    if (myString1 != "")
                    {
                        DL_IssueGroup.SelectedValue = myString1;
                    }

                    Conn.Close();
                    Conn.Dispose();
                    TB_FinishDatePre2.Text = DateTime.Now.AddDays(14).ToShortDateString();
                }


               


                DL_Duty_Fail_LevelList.SelectedValue = "2";
                TB_CreateDate.Text = DateTime.Now.ToShortDateString();

              
            TB_FinishDatePre.Text = DateTime.Now.ToShortDateString();

            DL_StartHour.SelectedValue = DateTime.Now.Hour.ToString();
            DL_StartMinute.SelectedValue = DateTime.Now.Minute.ToString();

            DL_EndHour.SelectedValue = DateTime.Now.Hour.ToString();
            DL_EndMinute.SelectedValue = DateTime.Now.Minute.ToString();

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

            string TB_CreateDateMonLen = TB_CreateDate.Text.Substring(4, 3).Replace("/", "");
          
            string TB_CreateDateDayLen = TB_CreateDate.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
            string TB_FinishDatePreMonLen = TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");
            string TB_FinishDatePreDayLen = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");

          

         

            try 
            {
                SqlDataSource_Send.InsertCommand = "INSERT INTO IssueList " +
                   "(IssueTitle, CreateDate, FinishDatePre, IssueStatus, employee_id, IssueGroupID , IssueDescription, IssueType, DeapartmentID,IsDutyIssue) VALUES( '"
                   + TB_IssueTitle.Text + "','" + TB_CreateDate.Text + "','" + TB_FinishDatePre2.Text + "','0','" + DL_IssueManager.SelectedValue + "','" + DL_IssueGroup.SelectedValue + "','" + TB_IssueDescription.Text + "','"
                    + DL_IssueType.SelectedValue + "','" + DL_IssueDeapartment.SelectedValue + "',1)";

                
                SqlDataSource_Send.Insert();

                String IssueID="0";//預設IssueID為0
                SqlDataSource_Send.SelectCommand = "SELECT IssueID,IssueTitle,CreateDate FROM IssueList WHERE IssueTitle='" + TB_IssueTitle.Text + "' AND CreateDate='" + TB_CreateDate.Text + "' ORDER BY IssueID DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    IssueID = sqlDataReader[0].ToString();

                    SqlDataSource_SendDuty_Fail_LevelMap.InsertCommand += "INSERT INTO dbo.Duty_Fail_LevelMap (IssueID,Duty_Fail_LevelID) VALUES  ('" + IssueID + "', '" + DL_Duty_Fail_LevelList.SelectedValue+ "' )";

                    SqlDataSource_SendDuty_Fail_LevelMap.Insert();

                    SqlDataSource_SendDuty_Fail_StyleMap.InsertCommand += "INSERT INTO dbo.Duty_Fail_StyleMap (IssueID,Duty_Fail_StyleID) VALUES  ('" + IssueID + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "' )";

                    SqlDataSource_SendDuty_Fail_StyleMap.Insert();

                    SqlDataSource_SendAssistantStyleMap.InsertCommand += "INSERT INTO dbo.Duty_AssistantStyleMap (IssueID,Duty_AssistantStyleID) VALUES  ('" + IssueID + "', '" + DL_Duty_AssistantStyleList.SelectedValue + "' )";

                    SqlDataSource_SendAssistantStyleMap.Insert();


                    if (TB_CreateDateMonLen.Length == 1)
                    {

                        string TB_CreateDateMon = "0" + TB_CreateDate.Text.Substring(4, 3).Replace("/", "");

                        if (TB_CreateDateDayLen.Length == 1)
                        {

                            string TB_CreateDateDay = "0" + TB_CreateDate.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                            string DatetimeStart = TB_CreateDate.Text.Substring(0, 4) + TB_CreateDateMon + TB_CreateDateDay + DL_StartHour.SelectedValue + DL_StartMinute.SelectedValue;

                            if (TB_FinishDatePreMonLen.Length == 1) {

                                string TB_FinishDatePreMon = "0"+ TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");

                                if (TB_FinishDatePreDayLen.Length == 1) {

                                    string TB_FinishDatePreDay= "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;

                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2) {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }
                            else if (TB_FinishDatePreMonLen.Length == 2) {

                                string TB_FinishDatePreMon =  TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");


                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }


                        }
                        else if (TB_CreateDateDayLen.Length == 2)
                        {

                            string TB_CreateDateDay = TB_CreateDate.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");

                            string DatetimeStart = TB_CreateDate.Text.Substring(0, 4) + TB_CreateDateMon + TB_CreateDateDay + DL_StartHour.SelectedValue + DL_StartMinute.SelectedValue;

                            if (TB_FinishDatePreMonLen.Length == 1)
                            {

                                string TB_FinishDatePreMon = "0" + TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");

                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;

                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }
                            else if (TB_FinishDatePreMonLen.Length == 2)
                            {

                                string TB_FinishDatePreMon = TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");


                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }

                        }


                    }
                    else if (TB_CreateDateMonLen.Length == 2)
                    {

                        string TB_CreateDateMon = TB_CreateDate.Text.Substring(4, 3).Replace("/", "");

                        if (TB_CreateDateDayLen.Length == 1)
                        {

                            string TB_CreateDateDay = "0" + TB_CreateDate.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                            string DatetimeStart = TB_CreateDate.Text.Substring(0, 4) + TB_CreateDateMon + TB_CreateDateDay + DL_StartHour.SelectedValue + DL_StartMinute.SelectedValue;
                            if (TB_FinishDatePreMonLen.Length == 1)
                            {

                                string TB_FinishDatePreMon = "0" + TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");

                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;

                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }
                            else if (TB_FinishDatePreMonLen.Length == 2)
                            {

                                string TB_FinishDatePreMon = TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");


                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }

                        }
                        else if (TB_CreateDateDayLen.Length == 2)
                        {

                            string TB_CreateDateDay = TB_CreateDate.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");

                            string DatetimeStart = TB_CreateDate.Text.Substring(0, 4) + TB_CreateDateMon + TB_CreateDateDay + DL_StartHour.SelectedValue + DL_StartMinute.SelectedValue;

                            if (TB_FinishDatePreMonLen.Length == 1)
                            {

                                string TB_FinishDatePreMon = "0" + TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");

                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;

                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }
                            else if (TB_FinishDatePreMonLen.Length == 2)
                            {

                                string TB_FinishDatePreMon = TB_FinishDatePre.Text.Substring(4, 3).Replace("/", "");


                                if (TB_FinishDatePreDayLen.Length == 1)
                                {

                                    string TB_FinishDatePreDay = "0" + TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();


                                }

                                else if (TB_FinishDatePreDayLen.Length == 2)
                                {

                                    string TB_FinishDatePreDay = TB_FinishDatePre.Text.Substring(TB_CreateDate.Text.Length - 2).Replace("/", "");
                                    string DatetimeEND = TB_FinishDatePre.Text.Substring(0, 4) + TB_FinishDatePreMon + TB_FinishDatePreDay + DL_EndHour.SelectedValue + DL_EndMinute.SelectedValue;


                                    SqlDataSource_SendIssueDuty.InsertCommand += "INSERT INTO dbo.IssueDuty (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Fail_Level) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_Duty_Fail_LevelList.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDuty.Insert();

                                    SqlDataSource_SendIssueDutyDetail.InsertCommand += "INSERT INTO dbo.IssueDutyDetail (IssueID,Duty_Start,Duty_End,Duty_Work_Time,Duty_Fail_Style,Duty_Person) VALUES  ('" + IssueID + "', '" + DatetimeStart + "','" + DatetimeEND + "', '" + LabelTimeDiff.Text + "', '" + DL_Duty_Fail_StyleList.SelectedValue + "', '" + DL_IssueManager_Duty.SelectedValue + "' )";

                                    SqlDataSource_SendIssueDutyDetail.Insert();

                                }

                            }
                        }
                    }



               





                    for (int i = 0; i < CBL_SystemList.Items.Count; i++)
                    {
                        if (CBL_SystemList.Items[i].Selected == true)
                        {
                            SqlDataSource_SendIssueSystemMap.InsertCommand = "INSERT INTO IssueSystemMap (IssueID, SystemID) VALUES( '"
                                + IssueID + "','" +  CBL_SystemList.Items[i].Value + "')";

                            SqlDataSource_SendIssueSystemMap.Insert();


                        }
                        else
                        {
                            continue;

                        }
                    }




                    for (int i = 0; i < CBL_IssueManager.Items.Count; i++)
                    {
                        if (CBL_IssueManager.Items[i].Selected == true)
                        {
                            SqlDataSource_SendIssueAssistantMap.InsertCommand = "INSERT INTO dbo.IssueAssistantMap (IssueID,employee_id) VALUES  ('" + IssueID + "', '" + CBL_IssueManager.Items[i].Value + "' )";

                            SqlDataSource_SendIssueAssistantMap.Insert();

                        }
                        else
                        {
                            continue;

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