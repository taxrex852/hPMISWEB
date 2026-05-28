using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication20
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            // 檢查有沒有登入
            if (Object.Equals(Session["ID"], null))
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                string userId = Session["ID"].ToString();
                LB_SessionID.Text = userId;

                // 1. 定義允許看到選單的帳號清單
                string[] allowedUsers = { "045627", "008197", "019126" };

                // 2. 如果目前帳號「不包含」在允許清單內，就隱藏選單
                if (!allowedUsers.Contains(userId))
                {
                    Menu_Test.Visible = false;
                }
            }
            LB_SessionID.Text = Session["ID"].ToString();
            SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
            //SqlConnection Conn = new SqlConnection("Data Source=10.108.137.235;Password=spcmgr;User ID=sa;Initial Catalog=Y6P2_Materials_test");
            //SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Password=test;User ID=test;Initial Catalog=Y6P2_Materials");
            Conn.Open();

            SqlDataAdapter x_maxmin = new SqlDataAdapter("SELECT DISTINCT CONVERT(Varchar(10), dbo.time_dimension.the_date, 111) AS the_date, dbo.time_dimension.the_workhour,dbo.employee.employee_id, dbo.employee.employee_name,(CASE WHEN dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 0 ELSE dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS SumTaskHours_daily,(CASE when dbo.time_dimension.the_workhour = 0 then 0 WHEN dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour IS NULL THEN 8 else dbo.time_dimension.the_workhour - dbo.Empolyee_SumTaskHour_Daily.SumTaskHour END) AS Remaining_Hours FROM dbo.employee CROSS JOIN dbo.time_dimension LEFT OUTER JOIN dbo.Empolyee_SumTaskHour_Daily ON  dbo.time_dimension.the_date = dbo.Empolyee_SumTaskHour_Daily.TaskDate AND  dbo.employee.employee_id = dbo.Empolyee_SumTaskHour_Daily.employee_id   where dbo.time_dimension.the_date = convert(Varchar(10), getdate(), 111)  and dbo.employee.employee_id = '" + LB_SessionID.Text + "'  order by the_date, employee_id", Conn);
            DataSet x_maxminds = new DataSet();
            x_maxmin.Fill(x_maxminds, "Empolyee_Workhour_Subtract_TaskHour");
            DataTable myTable = x_maxminds.Tables["Empolyee_Workhour_Subtract_TaskHour"];
            string myString1 = "";
            myString1 = myString1 + myTable.Rows[0]["Remaining_Hours"];

            LB_SubTask.Text = myString1;
            Conn.Close();
            Conn.Dispose();
            SqlConnection.ClearAllPools();
        }
        protected void BT_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}