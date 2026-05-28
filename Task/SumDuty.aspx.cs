using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class SumDuty : System.Web.UI.Page
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
                    int days = (int)(endMonth - startMonth).TotalDays+1;
                    int Workhours = days * 24;

                    Totaldays.Text = days.ToString();
                    TotalWorkhours.Text = Workhours.ToString();
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
            int days = (int)(parsedDateEnd.AddDays(+1) - parsedDateStart.AddDays(+1)).TotalDays + 1;
            int Workhours = days * 24;

            Totaldays.Text = days.ToString();
            TotalWorkhours.Text = Workhours.ToString();

        }

        protected void BT_Cal_Sel_TaskDate_Next_Click(object sender, EventArgs e)
        {
            string dateInputStart = TB_TaskDateStart.Text;
            var parsedDateStart = DateTime.Parse(dateInputStart);
            string dateInputEnd = TB_TaskDateEnd.Text;
            var parsedDateEnd = DateTime.Parse(dateInputEnd);

            TB_TaskDateEnd.Text = parsedDateEnd.AddDays(+1).ToShortDateString();
            TB_TaskDateStart.Text = parsedDateStart.AddDays(+1).ToShortDateString();
            int days = (int)(parsedDateEnd.AddDays(+1) - parsedDateStart.AddDays(+1)).TotalDays + 1;
            int Workhours = days * 24;

            Totaldays.Text = days.ToString();
            TotalWorkhours.Text = Workhours.ToString();
        }

        protected void BT_Send_Click(object sender, EventArgs e)
        {
            string dateInputStart = TB_TaskDateStart.Text;
            var parsedDateStart = DateTime.Parse(dateInputStart);
            string dateInputEnd = TB_TaskDateEnd.Text;
            var parsedDateEnd = DateTime.Parse(dateInputEnd);

            int days = (int)(parsedDateEnd - parsedDateStart).TotalDays + 1;
            int Workhours = days * 24;

            Totaldays.Text = days.ToString();
            TotalWorkhours.Text = Workhours.ToString();

            if (TB_TaskDateStart.Text == TB_TaskDateEnd.Text)
            {
                SqlDataSourceSumSystemNameTaskHour.SelectCommand = "select a.SystemName,count(SystemName) as System_failCount,sum(Duty_Work_Time) as System_failTimeSum from( SELECT  IssueList.IssueID, SystemName,  round(cast(Duty_Work_Time as float) / 60, 2) as Duty_Work_Time  FROM Y6P2_Materials.dbo.IssueList  inner join IssueDutyDetail  on IssueList.IssueID = IssueDutyDetail.IssueID  inner join IssueSystemMap  on IssueList.IssueID = IssueSystemMap.IssueID   inner join SystemList on IssueSystemMap.SystemID = SystemList.SystemID  where IsDutyIssue = 1  and   dbo.IssueList.CreateDate = '" + TB_TaskDateEnd.Text + "' and IssueStatus <> 5 ) a  GROUP BY   a.SystemName ";
                SqlDataSource_Duty_shift.SelectCommand = "select distinct Duty_Person,employee_name,cast(Duty_End as date) as Duty_End,case when convert(varchar,cast(Duty_End as datetime),108) between '07:30:00' and '16:30:00' and d.the_workhour =0   then  '04：0730~1630 假日待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =0   then  '05：1630~0730 假日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =8   then  '06：1630~0730 平日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '23:00:00' and '23:59:59' or convert(varchar,cast(Duty_End as datetime),108) between '00:00:00' and '07:00:00'   then  '07：1630~0730 夜間待命-2300~0700接獲電話處理事故' end as Duty_shift,case when d.the_workhour = 0 then '假日' else '平日' end as the_workhour from (select  SystemList.SystemName,a.IssueID,Duty_End,Duty_Person,employee_name from (SELECT a.IssueID,substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,Duty_Person ,b.employee_name FROM Y6P2_Materials.dbo.IssueDutyDetail a  inner join employee b on a.Duty_Person=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus <>5 union all SELECT a.IssueID, substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,a.employee_id,b.employee_name FROM dbo.IssueAssistantMap a  inner join employee b on a.employee_id=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus <>5 inner join IssueDutyDetail d on a.IssueID=d.IssueID   ) a inner join IssueSystemMap  on a.IssueID=IssueSystemMap.IssueID  inner join SystemList  on IssueSystemMap.SystemID=SystemList.SystemID) a inner join time_dimension d on cast(a.Duty_End as date)=cast(the_date as date)    where cast(Duty_End as date) = '" + TB_TaskDateEnd.Text + "'   order by  Duty_End desc";
            }

            else
            {
                SqlDataSourceSumSystemNameTaskHour.SelectCommand = "select a.SystemName,count(SystemName) as System_failCount,sum(Duty_Work_Time) as System_failTimeSum from( SELECT  IssueList.IssueID, SystemName,  round(cast(Duty_Work_Time as float) / 60, 2) as Duty_Work_Time  FROM Y6P2_Materials.dbo.IssueList  inner join IssueDutyDetail  on IssueList.IssueID = IssueDutyDetail.IssueID  inner join IssueSystemMap  on IssueList.IssueID = IssueSystemMap.IssueID   inner join SystemList on IssueSystemMap.SystemID = SystemList.SystemID  where IsDutyIssue = 1  and  dbo.IssueList.CreateDate between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'  and IssueStatus <> 5  ) a  GROUP BY   a.SystemName ";
                SqlDataSource_Duty_shift.SelectCommand = "select distinct Duty_Person,employee_name,cast(Duty_End as date) as Duty_End,case when convert(varchar,cast(Duty_End as datetime),108) between '07:31:00' and '16:30:00' and d.the_workhour =0   then  '04：0730~1630 假日待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =0   then  '05：1630~0730 假日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '16:31:00' and '22:59:00' and d.the_workhour =8   then  '06：1630~0730 平日夜間待命' when convert(varchar,cast(Duty_End as datetime),108) between '23:00:00' and '23:59:59' or convert(varchar,cast(Duty_End as datetime),108) between '00:00:00' and '07:30:00'   then  '07：1630~0730 夜間待命-2300~0700接獲電話處理事故' end as Duty_shift,case when d.the_workhour = 0 then '假日' else '平日' end as the_workhour from (select  SystemList.SystemName,a.IssueID,Duty_End,Duty_Person,employee_name from (SELECT a.IssueID,substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,Duty_Person ,b.employee_name FROM Y6P2_Materials.dbo.IssueDutyDetail a  inner join employee b on a.Duty_Person=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus <>5 union all SELECT a.IssueID, substring(Duty_End,1,4)+'-'+ substring(Duty_End,5,2)+'-' + substring(Duty_End,7,2) + ' '+ substring(Duty_End,9,2)+':' +substring(Duty_End,11,2)+':00' as Duty_End,a.employee_id,b.employee_name FROM dbo.IssueAssistantMap a  inner join employee b on a.employee_id=b.employee_id inner join IssueList c on a.IssueID=c.IssueID and c.IssueStatus <>5 inner join IssueDutyDetail d on a.IssueID=d.IssueID   ) a inner join IssueSystemMap  on a.IssueID=IssueSystemMap.IssueID  inner join SystemList  on IssueSystemMap.SystemID=SystemList.SystemID) a inner join time_dimension d on cast(a.Duty_End as date)=cast(the_date as date)   where cast(Duty_End as date) between  '" + TB_TaskDateStart.Text + "' AND  '" + TB_TaskDateEnd.Text + "'   order by  Duty_End desc";
            };

            

        }
    }
}