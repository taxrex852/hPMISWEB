using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class IssueList : System.Web.UI.Page
    {
        String QS_IssueID;
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
                    CheckBox3.Checked = true;
                    TextBoxName.Text = Session["ID"].ToString();
                   
                    DL_IssueManager.SelectedValue = TextBoxName.Text;
                    QS_IssueID = Request.QueryString["IssueID"];
                    if (QS_IssueID != null) //若是ISSUE ID 有值，就在選擇主題清單那邊預設代入該ID
                        DL_IssueManager.SelectedValue = QS_IssueID;
                    if (TextBoxName.Text == "045627")
                    {
                        SqlDataSource_GV_IssueList.SelectCommand = " (select * from VW_Issue_DutyList WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3))  " +
                     " UNION (select * from VW_Issue_List WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3) " +
                     "  and  employee_id  = '" + TextBoxName.Text + "' or exists (select 1 from IssueListAssistantMap as iam where iam.issueid=VW_Issue_List.issueid  and iam.employee_id  =  '" + TextBoxName.Text + "' )) " +

                     " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";

                    }
                    else if (TextBoxName.Text == "008197") {

                        SqlDataSource_GV_IssueList.SelectCommand = " (select * from VW_Issue_DutyList WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3))  " +
                    " UNION (select * from VW_Issue_List WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3) )" +
                   " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";




                    }
                    else {
                        SqlDataSource_GV_IssueList.SelectCommand = " (select * from VW_Issue_DutyList WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3))  " +
                            " UNION (select * from VW_Issue_List WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3) " +
                            "  and IssueGroupID in (select employee_system_group from employee where employee_id  = '" + TextBoxName.Text + "')) " +
                            " UNION(select * from VW_Issue_List WHERE (IssueStatus = 0 OR IssueStatus = 1 OR IssueStatus = 2 OR IssueStatus = 3) " +
                            " and employee_id =  '" + TextBoxName.Text + "' or exists (select 1 from IssueListAssistantMap as iam where iam.issueid=VW_Issue_List.issueid  and iam.employee_id  =  '" + TextBoxName.Text + "' ) and IssueGroupID not in (select employee_system_group from employee where employee_id  = '" + TextBoxName.Text + "') ) " +
                            " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";

                    }

                }

                TB_TaskDateEnd.Text = Calendar_TaskDate.TodaysDate.ToString("yyyy-MM-dd");
                string dateInput = TB_TaskDateEnd.Text;
                var parsedDate = DateTime.Parse(dateInput);

                TB_TaskDateStart.Text = "2000-01-01";

                return;
            }
        
       
        }

        protected void BT_Cal_Sel_CreateDate_Click(object sender, EventArgs e)
        {
            Calendar_TaskDate.Visible = true;
        }



        protected void Calendar_CreateDate_Sel(object sender, EventArgs e)
        {
            TB_TaskDateStart.Text = Calendar_TaskDate.SelectedDate.ToString("yyyy-MM-dd");
            Calendar_TaskDate.Visible = false;
        }

        protected void BT_Cal_Sel_TaskDateEnd_Click(object sender, EventArgs e)
        {
            Calendar_TaskDateEnd.Visible = true;
        }
        protected void Calendar_TaskDateEnd_SelectionChanged(object sender, EventArgs e)
        {
            TB_TaskDateEnd.Text = Calendar_TaskDateEnd.SelectedDate.ToString("yyyy-MM-dd");
            Calendar_TaskDateEnd.Visible = false;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DateTime FinishDatePre = Convert.ToDateTime(e.Row.Cells[3].Text);
                DateTime now = DateTime.Now;
                double timespan = new TimeSpan(now.Ticks - FinishDatePre.Ticks).Days;
                string issueStatus = e.Row.Cells[5].Text;

                if (timespan >= 1)
                {
                    e.Row.Cells[3].Font.Bold = true;
                }
                ;
                if (timespan >= 1 & timespan <= 15 & issueStatus == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(238, 211, 214);
                }
                else if (timespan >= 16 & timespan <= 30 & issueStatus == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(221, 154, 194);
                    if (e.Row.BackColor == System.Drawing.Color.FromArgb(221, 154, 194))
                    {
                        HyperLink link = e.Row.Cells[1].Controls[0] as HyperLink;

                        link.ForeColor = System.Drawing.Color.White;
                    }

                }
                else if (timespan >= 31 & timespan <= 45 & issueStatus == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(180, 134, 171);
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.BackColor == System.Drawing.Color.FromArgb(180, 134, 171))
                    {
                        HyperLink link = e.Row.Cells[1].Controls[0] as HyperLink;

                        link.ForeColor = System.Drawing.Color.White;
                    }
                }
                else if (timespan >= 46 & issueStatus == "1")
                {


                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.BackColor == System.Drawing.Color.Red) {
                        HyperLink link = e.Row.Cells[1].Controls[0] as HyperLink;

                        link.ForeColor = System.Drawing.Color.White;
                     }

                }
                else if (timespan <= 0 & issueStatus == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.White;

                }
                else if (issueStatus == "2" || issueStatus == "3")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(203, 255, 173);
                };

               



                switch (e.Row.Cells[14].Text)
                {
                    case "0":
                        e.Row.Cells[11].Text = "";
                     

                        break;
                    case "1":

                        e.Row.Cells[10].Text = "";
                 
                        break;
                
                }

                switch (e.Row.Cells[5].Text)
                {
                    case "0":
                     
                        e.Row.Cells[5].Text = "新建";

                        break;
                    case "1":
                    
                        e.Row.Cells[5].Text = "執行中";

                        break;
                    case "2":

                      
                        e.Row.Cells[5].Text = "已完成待上線";
                         break;
                    case "3":


                        e.Row.Cells[5].Text = "已完成待結案";
                        break;
                    case "4":

                        e.Row.Cells[12].Text = "";
                        e.Row.Cells[5].Text = "結案";
                        break;
                    case "5":

                        e.Row.Cells[12].Text = "";
                        e.Row.Cells[5].Text = "刪除";
                        break;
                }

          

            }

            e.Row.Cells[14].Visible = false;

        }

        protected void Query_Click(object sender, EventArgs e)
        {
            int i;
            int CountFalse = 0;
            int CountTrue = 0;
    


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
                                  SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";                    
                                break;

                            case 1:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else {

                                    SQLCMD += " or IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;

                            case 2:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;

                            case 3:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueStatus = '" + Tempcheckbox.Text + "'";

                                };
                                break;
                            case 4:
                                if (CountFalse > 0 & CountTrue == 1)
                                {
                                    SQLCMD += " IssueStatus = '" + Tempcheckbox.Text + "'";
                                }
                                else
                                {

                                    SQLCMD += " or IssueStatus = '" + Tempcheckbox.Text + "'";

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
                else {
                    CountFalse += 1;

                };




            }
            if (DL_IssueManager.SelectedValue == "ALL" & DL_IssueGroup.SelectedValue == "ALL")
            {
                SqlDataSource_GV_IssueList.SelectCommand = " select * from " +
                 " (select * from VW_Issue_DutyList WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + "and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_DutyList.IssueTitle like '%" + TB_IssueTitle.Text + "%' )isDList  " +
                 " UNION (select * from VW_Issue_List WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + " and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_List.IssueTitle like '%" + TB_IssueTitle.Text + "%')" +

             " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";

            }
            else if (DL_IssueManager.SelectedValue == "ALL")
            {
                SQLCMDForDL += " and IssueGroupID = '" + DL_IssueGroup.SelectedValue + "'";

                SqlDataSource_GV_IssueList.SelectCommand = " select * from " +
              " (select * from VW_Issue_DutyList WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + "and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_DutyList.IssueTitle like '%" + TB_IssueTitle.Text + "%' )isDList  " +
              " UNION (select * from VW_Issue_List WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + " and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_List.IssueTitle like '%" + TB_IssueTitle.Text + "%')" +

          " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";

            }
            else if (DL_IssueGroup.SelectedValue == "ALL")
            {

                SQLCMDForDL += " and employee_id = '" + DL_IssueManager.SelectedValue + "'  ";

                SqlDataSource_GV_IssueList.SelectCommand = " select * from " +
                    " (select * from VW_Issue_DutyList WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + "and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_DutyList.IssueTitle like '%" + TB_IssueTitle.Text + "%' )isDList  " +
                    " UNION (select * from VW_Issue_List WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + " or exists (select 1 from IssueListAssistantMap as iam where iam.issueid=VW_Issue_List.issueid  and iam.employee_id  =  '" + DL_IssueManager.SelectedValue + "' )  and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_List.IssueTitle like '%" + TB_IssueTitle.Text + "%')" +

                " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";

            }
            else {

                SQLCMDForDL += " and employee_id = '" + DL_IssueManager.SelectedValue + "'  and IssueGroupID = '" + DL_IssueGroup.SelectedValue + "' ";


                SqlDataSource_GV_IssueList.SelectCommand = " select * from " +
                    " (select * from VW_Issue_DutyList WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + " and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_DutyList.IssueTitle like '%" + TB_IssueTitle.Text + "%' )isDList  " +
                    " UNION (select * from VW_Issue_List WHERE " + "(" + SQLCMD + ")" + SQLCMDForDL + " and (CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  or FinishDateAct between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "') and VW_Issue_List.IssueTitle like '%" + TB_IssueTitle.Text + "%')" +

                " ORDER BY IsDutyIssue desc, IssueGroupID,IssueTitle, CreateDate, employee_id";


            };
        

        }
        


    }


}