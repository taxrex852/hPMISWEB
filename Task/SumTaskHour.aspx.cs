using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class SumTaskHour : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (!IsPostBack)
            {
                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                    Response.Redirect("Login.aspx", true);

                }
                else {
                    DateTime startMonth = dt.AddDays(1 - dt.Day);
                    DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);
                    TB_TaskDateStart.Text = startMonth.ToShortDateString();
                    TB_TaskDateEnd.Text = endMonth.ToShortDateString();
                };


              
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

        protected void BT_Send_Click(object sender, EventArgs e)
        {


                if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
                {
                SqlDataSourceSumSystemNameTaskHour.SelectCommand = "SELECT   dbo.SystemList.SystemName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL THEN 0 ELSE SUM(dbo.TaskList.TaskHours) END)  AS SumSystemNameTaskHour FROM  dbo.SystemList left OUTER JOIN dbo.TaskList ON dbo.SystemList.SystemID = dbo.TaskList.SystemID  WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'    GROUP BY    dbo.SystemList.SystemName";
                SqlDataSourceSumIssuetypetitle.SelectCommand = "SELECT issuetypetitle, SUM(taskhours) as SumIssuetypetitle from tasklist left join issuelist on tasklist.issueid = issuelist.issueid left join issuetype on issuelist.issuetype = issuetype.issuetypeid WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  group by issuetypetitle";
                SqlDataSourceSumDeapartmentIDHour.SelectCommand = "SELECT   dbo.DeapartmentList.DeapartmentName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL THEN 0 ELSE SUM(dbo.TaskList.TaskHours ) END) AS SumDeapartmentIDHour FROM     dbo.IssueList INNER JOIN dbo.TaskList ON dbo.IssueList.IssueID = dbo.TaskList.IssueID right outer JOIN dbo.DeapartmentList ON dbo.IssueList.DeapartmentID = dbo.DeapartmentList.DeapartmentID  WHERE TaskList.TaskDate =  '" + TB_TaskDateEnd.Text + "'  GROUP BY   dbo.DeapartmentList.DeapartmentName";

            }
                else
                {
                SqlDataSourceSumSystemNameTaskHour.SelectCommand = "SELECT   dbo.SystemList.SystemName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL THEN 0 ELSE SUM(dbo.TaskList.TaskHours) END)  AS SumSystemNameTaskHour FROM  dbo.SystemList left OUTER JOIN dbo.TaskList ON dbo.SystemList.SystemID = dbo.TaskList.SystemID WHERE TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'   GROUP BY    dbo.SystemList.SystemName";
                SqlDataSourceSumIssuetypetitle.SelectCommand = "SELECT issuetypetitle, SUM(taskhours) as SumIssuetypetitle from tasklist left join issuelist on tasklist.issueid = issuelist.issueid left join issuetype on issuelist.issuetype = issuetype.issuetypeid WHERE TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'   group by issuetypetitle";
                SqlDataSourceSumDeapartmentIDHour.SelectCommand = "SELECT   dbo.DeapartmentList.DeapartmentName,  (CASE WHEN SUM(dbo.TaskList.TaskHours) IS NULL THEN 0 ELSE SUM(dbo.TaskList.TaskHours ) END) AS SumDeapartmentIDHour FROM     dbo.IssueList INNER JOIN dbo.TaskList ON dbo.IssueList.IssueID = dbo.TaskList.IssueID right outer JOIN dbo.DeapartmentList ON dbo.IssueList.DeapartmentID = dbo.DeapartmentList.DeapartmentID WHERE TaskList.TaskDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  GROUP BY   dbo.DeapartmentList.DeapartmentName";
            };

            

        }
    }
}