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
    public partial class CreateSoftwareDetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BT_Modify.Visible = false;
                BT_Send.Visible = true;

                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                    Response.Redirect("Login.aspx", true);

                }
                else
                {

                    TextBoxName.Text = Session["ID"].ToString();

                    DL_TaskPeople.SelectedValue = TextBoxName.Text;




                }



                return;
            }


        }

        protected void BT_queryMasterClick(object sender, System.EventArgs e)
        {

            TB_software_serial.Text = "";

            TB_license_number.Text = "";


            TB_license_date_start.Text = "";
            TB_license_date_end.Text = "";
            TB_remark.Text = "";
            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;



            TB_software_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_software_name.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_software_manufacturer.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
        
            SqlDataSource_software_detail.SelectCommand = "SELECT  software_code,software_serial,license_number,software_detail.storage_serial,storage_detail.storage_id,employee_id,license_date_start,license_date_end,software_detail.remark,software_detail.time_stamp  FROM Y6P2_Materials.dbo.software_detail left join storage_detail on software_detail.storage_serial=storage_detail.storage_serial   where software_code = " + TB_software_code.Text + " ORDER BY software_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {


            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO software_detail " +
                   "(software_code,storage_serial,license_number,employee_id,license_date_start,license_date_end,remark,time_stamp) VALUES( '"
                   + TB_software_code.Text + "','" + DL_storage_serial.SelectedValue + "','" + TB_license_number.Text + "','" + DL_TaskPeople.SelectedValue +
                  "', '" + TB_license_date_start.Text + "','" + TB_license_date_end.Text + 
                  "','" + TB_remark.Text + "',getdate())";

                SqlDataSource_Send.Insert();

                String software_serial = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 software_serial FROM software_detail ORDER BY software_serial DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    software_serial = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，software_serial = " + software_serial;
                    SqlDataSource_software_detail.SelectCommand = "SELECT  software_code,software_serial,license_number,software_detail.storage_serial,storage_detail.storage_id,employee_id,license_date_start,license_date_end,software_detail.remark,software_detail.time_stamp  FROM Y6P2_Materials.dbo.software_detail left join storage_detail on software_detail.storage_serial=storage_detail.storage_serial   where software_code = " + TB_software_code.Text + " ORDER BY software_serial DESC";

                }


            }
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

        protected void BT_modiClick(object sender, System.EventArgs e)
        {


            TB_software_serial.Text = "";

            TB_license_number.Text = "";
           
          
            TB_license_date_start.Text = "";
            TB_license_date_end.Text = "";
            TB_remark.Text = "";
          

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            DL_storage_serial.SelectedValue = gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_software_serial.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_license_number.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            DL_TaskPeople.SelectedValue = gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_license_date_start.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            TB_license_date_end.Text = gvr.Cells[8].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");

            BT_Modify.Visible = true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {




                SqlDataSource_Send.UpdateCommand = "UPDATE software_detail SET " +
                " license_number ='" + TB_license_number.Text +
                  "',storage_serial ='" + DL_storage_serial.SelectedValue +
                "',employee_id ='" + DL_TaskPeople.SelectedValue +
                "',license_date_start ='" + TB_license_date_start.Text +
                "',license_date_end ='" + TB_license_date_end.Text +
                "',remark ='" + TB_remark.Text +
                    "',time_stamp = getdate()" +

                     "  WHERE software_serial = '" + TB_software_serial.Text + "'";


                SqlDataSource_Send.Update();
                LB_Message.Text = "修改完成，software_serial = " + TB_software_serial.Text;


            SqlDataSource_software_detail.SelectCommand = "SELECT  software_code,software_serial,license_number,software_detail.storage_serial,storage_detail.storage_id,employee_id,license_date_start,license_date_end,software_detail.remark,software_detail.time_stamp  FROM Y6P2_Materials.dbo.software_detail left join storage_detail on software_detail.storage_serial=storage_detail.storage_serial   where software_code = " + TB_software_code.Text + " ORDER BY software_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

      
    }
        
}