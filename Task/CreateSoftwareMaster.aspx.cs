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
    public partial class CreateSoftwareMaster : System.Web.UI.Page
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

                SqlDataSource_Send.InsertCommand = "INSERT INTO software_master " +
                   "(software_name,software_manufacturer,remark,time_stamp) VALUES( '"
                   + TB_software_name.Text + "','" + TB_software_manufacturer.Text + "','" + TB_remark.Text + "',getdate())";
                SqlDataSource_Send.Insert();

                String software_code = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 software_code FROM software_master ORDER BY software_code DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    software_code = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，software_code = " + software_code;
                    if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text == "")
                    {

                        SqlDataSource_software_master.SelectCommand = "select * from software_master ORDER BY software_code DESC";

                    }
                    else if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text != "")
                    {
                        SqlDataSource_software_master.SelectCommand = "select * from software_master where software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";

                    }
                    else if (Sel_software_name.Text != "" && Sel_software_manufacturer.Text == "")
                    {
                        SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' ORDER BY software_code DESC";

                    }
                    else
                    {
                        SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' or software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";


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


            TB_software_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_software_name.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_software_manufacturer.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
        
            BT_Modify.Visible =  true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {
          
            SqlDataSource_Send.UpdateCommand = "UPDATE software_master SET " +
                 "software_name = '" + TB_software_name.Text + "', software_manufacturer = '" + TB_software_manufacturer.Text + "', remark = '" + TB_remark.Text + "'"
        
                  +  ", time_stamp=getdate() WHERE software_code = '" + TB_software_code.Text + "'";

            SqlDataSource_Send.Update();
            LB_Message.Text = "修改資料完成，software_code = " + TB_software_code.Text;
            if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text == "")
            {

                SqlDataSource_software_master.SelectCommand = "select * from software_master ORDER BY software_code DESC";

            }
            else if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text != "")
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";

            }
            else if (Sel_software_name.Text != "" && Sel_software_manufacturer.Text == "")
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' ORDER BY software_code DESC";

            }
            else
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' or software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";


            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text == "")
            {

                SqlDataSource_software_master.SelectCommand = "select * from software_master ORDER BY software_code DESC";

            }
            else if (Sel_software_name.Text == "" && Sel_software_manufacturer.Text != "")
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";

            }
            else if (Sel_software_name.Text != "" && Sel_software_manufacturer.Text == "")
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' ORDER BY software_code DESC";

            }
            else
            {
                SqlDataSource_software_master.SelectCommand = "select * from software_master where software_name like '%" + Sel_software_name.Text + "%' or software_manufacturer like '%" + Sel_software_manufacturer.Text + "%' ORDER BY software_code DESC";


            }



           
            

        }
    }
}