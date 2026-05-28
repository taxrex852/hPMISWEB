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
    public partial class CreateStorageMaster : System.Web.UI.Page
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

        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO storage_master " +
                   "(storage_area, storage_size, remark, time_stamp) VALUES( '"
                   + TB_storage_area.Text + "','" + TB_storage_size.Text + "','" + TB_remark.Text + "',getdate())";
                SqlDataSource_Send.Insert();

                String storage_code =null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 storage_code FROM storage_master ORDER BY storage_code DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    storage_code = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，storage_code = " + storage_code;
                    if (Sel_storage_area.Text == "" && Sel_remark.Text == "")
                    {

                        SqlDataSource_storage_master.SelectCommand = "select * from storage_master ORDER BY storage_code DESC";

                    }
                    else if (Sel_storage_area.Text == "" && Sel_remark.Text != "")
                    {
                        SqlDataSource_storage_master.SelectCommand = "select * from storage_master where remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";

                    }
                    else if (Sel_storage_area.Text != "" && Sel_remark.Text == "")
                    {
                        SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' ORDER BY storage_code DESC";

                    }
                    else
                    {
                        SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' or remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";


                    }
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


            TB_storage_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_storage_area.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_storage_size.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");


            BT_Modify.Visible =  true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {
          
            SqlDataSource_Send.UpdateCommand = "UPDATE storage_master SET " +
                 "storage_area = '" + TB_storage_area.Text + "', storage_size = '" + TB_storage_size.Text + "', remark = '" + TB_remark.Text +
                 "', time_stamp=getdate() WHERE storage_code = '" + TB_storage_code.Text + "'";

            SqlDataSource_Send.Update();
            LB_Message.Text = "修改資料完成，storage_code = " + TB_storage_code.Text;
            if (Sel_storage_area.Text == "" && Sel_remark.Text == "")
            {

                SqlDataSource_storage_master.SelectCommand = "select * from storage_master ORDER BY storage_code DESC";

            }
            else if (Sel_storage_area.Text == "" && Sel_remark.Text != "")
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";

            }
            else if (Sel_storage_area.Text != "" && Sel_remark.Text == "")
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' ORDER BY storage_code DESC";

            }
            else
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' or remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";


            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }
        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (Sel_storage_area.Text == "" && Sel_remark.Text == "")
            {

                SqlDataSource_storage_master.SelectCommand = "select * from storage_master ORDER BY storage_code DESC";

            }
            else if (Sel_storage_area.Text == "" && Sel_remark.Text != "")
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";

            }
            else if (Sel_storage_area.Text != "" && Sel_remark.Text == "")
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' ORDER BY storage_code DESC";

            }
            else
            {
                SqlDataSource_storage_master.SelectCommand = "select * from storage_master where storage_area like '%" + Sel_storage_area.Text + "%' or remark like '%" + Sel_remark.Text + "%' ORDER BY storage_code DESC";


            }

        }
    }
}