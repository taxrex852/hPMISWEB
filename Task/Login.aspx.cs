using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_Login_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource1.Select(new DataSourceSelectArguments());

            if(sqlDataReader.Read())
            {
                LB_Status.Text = "true";
                Session["ID"] = TB_ID.Text;
                Response.Redirect("IssueList.aspx");
              
            }
            else
                LB_Status.Text = "false";
        }


    }
}