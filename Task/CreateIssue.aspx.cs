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
    public partial class CreateIssue : System.Web.UI.Page
    {
      
           
      


       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBoxName.Text = Session["ID"].ToString();
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
          
                 
              
              
                




                TB_CreateDate.Text = DateTime.Now.ToShortDateString();
                    TB_FinishDatePre.Text = DateTime.Now.AddDays(7).ToShortDateString();
              

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

        protected void BT_Send_Click(object sender, EventArgs e)
        {
            try 
            {
                SqlDataSource_Send.InsertCommand = "INSERT INTO IssueList " +
                   "(IssueTitle, CreateDate, FinishDatePre, IssueStatus, employee_id, IssueGroupID , IssueDescription, IssueType, DeapartmentID,IsDutyIssue) VALUES( '"
                   + TB_IssueTitle.Text + "','" + TB_CreateDate.Text + "','" + TB_FinishDatePre.Text + "','0','" + DL_IssueManager.SelectedValue + "','" + DL_IssueGroup.SelectedValue + "','" + TB_IssueDescription.Text + "','"
                    + DL_IssueType.SelectedValue + "','" + DL_IssueDeapartment.SelectedValue + "',0)";
                SqlDataSource_Send.Insert();

                String IssueID="0";//預設IssueID為0
                SqlDataSource_Send.SelectCommand = "SELECT IssueID,IssueTitle,CreateDate FROM IssueList WHERE IssueTitle='" + TB_IssueTitle.Text + "' AND CreateDate='" + TB_CreateDate.Text + "' ORDER BY IssueID DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    IssueID = sqlDataReader[0].ToString();
                    for (int i = 0; i < CBL_SystemList.Items.Count; i++)
                    {
                        if (CBL_SystemList.Items[i].Selected == true)
                        {
                            SqlDataSource_Send.InsertCommand = "INSERT INTO IssueSystemMap (IssueID, SystemID) VALUES( '"
                                + IssueID + "','" +  CBL_SystemList.Items[i].Value + "')";
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