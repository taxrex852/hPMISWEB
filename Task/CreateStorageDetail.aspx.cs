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
    public partial class CreateStorageDetail : System.Web.UI.Page
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

                return;
            }


        }
        protected void BT_queryMasterClick(object sender, System.EventArgs e)
        {
        
            Button btn = (Button)sender;

   
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            TB_storage_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_storage_area.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_storage_size.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_storage_name.Text = "";
            TB_storage_id.Text = "";
            TB_remark.Text = "";
    
   
            SqlDataSource_storage_detail.SelectCommand = "select * from storage_detail  where storage_code = " + TB_storage_code.Text + " ORDER BY storage_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO storage_detail " +
                   "(storage_code, storage_name, storage_id,remark, time_stamp) VALUES( '"
                   + TB_storage_code.Text + "','" + TB_storage_name.Text + "','" + TB_storage_id.Text + "','" + TB_remark.Text + "',getdate())";
                SqlDataSource_Send.Insert();

                String storage_serial = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 storage_serial FROM storage_detail ORDER BY storage_serial DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    storage_serial = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，storage_serial = " + storage_serial;
                    SqlDataSource_storage_detail.SelectCommand = "select * from storage_detail  where storage_code = " + TB_storage_code.Text + " ORDER BY storage_serial DESC";

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
       
            Button btn = (Button)sender;

    
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            TB_storage_serial.Text= gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_storage_name.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_storage_id.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");


            BT_Modify.Visible = true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

            SqlDataSource_Send.UpdateCommand = "UPDATE storage_detail SET " +
                 "storage_name = '" + TB_storage_name.Text + "', storage_id = '" + TB_storage_id.Text + "', remark = '" + TB_remark.Text +
                 "', time_stamp=getdate() WHERE storage_serial = '" + TB_storage_serial.Text + "'";

            SqlDataSource_Send.Update();
            LB_Message.Text = "修改完成，storage_serial = " + TB_storage_serial.Text;
            SqlDataSource_storage_detail.SelectCommand = "select * from storage_detail  where storage_code = " + TB_storage_code.Text + " ORDER BY storage_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }
     
    }
}