using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class IssueList_personal : System.Web.UI.Page
    {
        //String QS_IssueID;
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
                    CheckBox0.Checked = true;
                    CheckBox1.Checked = true;
                    CheckBox2.Checked = true;
                    TextBoxName.Text = Session["ID"].ToString();

                    DL_IssueManager.SelectedValue = TextBoxName.Text;
                    ////QS_IssueID = Request.QueryString["IssueID"];
                    ////if (QS_IssueID != null) //若是ISSUE ID 有值，就在選擇主題清單那邊預設代入該ID
                    ////    DL_IssueManager.SelectedValue = QS_IssueID;
                    TB_TaskDateEnd.Text = Calendar_TaskDate.TodaysDate.ToShortDateString();
                    string dateInput = TB_TaskDateEnd.Text;
                    var parsedDate = DateTime.Parse(dateInput);

                
                    TB_TaskDateStart.Text = DateTime.Now.AddDays(-30).ToShortDateString();
                    TB_TaskDateEnd.Text = DateTime.Now.ToShortDateString();
                }

                return;
            }


        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          




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

                     
                        e.Row.Cells[4].Text = "結案";
                        break;
                    case "5":
                    
                       
                        e.Row.Cells[4].Text = "刪除";
                        break;
                }

              
            
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
        protected void Query_Click(object sender, EventArgs e)
        {
            int i;
            int CountFalse = 0;
            int CountTrue = 0;
            string DeFaultSQLCMD = "SELECT dbo.IssueList.IssueID, dbo.IssueList.IssueTitle, dbo.TaskList.employee_id, dbo.IssueID_SumTaskHour.SumTaskHours, SUM(dbo.TaskList.TaskHours) AS SumTaskHours_Personal,dbo.IssueList.IssueStatus FROM dbo.TaskList INNER JOIN dbo.IssueList ON dbo.TaskList.IssueID = dbo.IssueList.IssueID INNER JOIN dbo.IssueID_SumTaskHour ON dbo.TaskList.IssueID = dbo.IssueID_SumTaskHour.IssueID  WHERE dbo.TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  and";
        
            

            string SQLCMD = "";
            string SQLCMDForDL = "";
            for (i = 0; i <= 5; i += 1) //Checkbox多選判斷
            {

                CheckBox Tempcheckbox = (CheckBox)Master.FindControl("MainContent").FindControl("CheckBox" + i.ToString());

                if (Tempcheckbox.Checked == true)
                {
                    CountTrue += 1;
                    {
                        switch (i)
                        {
                            case 0:
                                SQLCMD += " IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";
                                break;

                            case 1:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;

                            case 2:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;

                            case 3:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;
                            case 4:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueList.IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;
                            case 5:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;
                        }
                    }
                }
                else
                {
                    CountFalse += 1;

                };




            }


            if (DL_IssueManager.SelectedValue == "ALL" & DL_IssueGroup.SelectedValue == "ALL")
            {

            }
            else if (DL_IssueManager.SelectedValue == "ALL")
            {
                SQLCMDForDL += " and IssueList.IssueGroupID = '" + DL_IssueGroup.SelectedValue + "'";
            }
            else if (DL_IssueGroup.SelectedValue == "ALL")
            {

                SQLCMDForDL += " and TaskList.employee_id = '" + DL_IssueManager.SelectedValue + "'";
            }
            else
            {

                SQLCMDForDL += " and TaskList.employee_id = '" + DL_IssueManager.SelectedValue + "' and IssueList.IssueGroupID = '" + DL_IssueGroup.SelectedValue + "' ";
            }; 


            SqlDataSource_GV_IssueList.SelectCommand = DeFaultSQLCMD + "(" + SQLCMD + ")" + SQLCMDForDL + "  GROUP BY  dbo.IssueList.IssueID, dbo.IssueList.IssueTitle, dbo.TaskList.employee_id, dbo.IssueList.IssueStatus,dbo.IssueID_SumTaskHour.SumTaskHours, dbo.IssueList.IssueGroupID ORDER BY dbo.IssueList.IssueID asc ";

        }

    }


}

    