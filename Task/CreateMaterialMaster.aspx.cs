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
    public partial class CreateMaterialMaster : System.Web.UI.Page
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

                SqlDataSource_Send.InsertCommand = "INSERT INTO material_master " +
                   "(material_type,material_name,material_unit,safety_quantity,material_consumable,material_ERP_no,remark,time_stamp,iscomputer,material_part_number,material_detail_spec) VALUES( '"
                   + TB_material_type.Text + "','" + TB_material_name.Text + "','" + DL_material_unit.SelectedValue + "','" + TB_safety_quantity.Text + "','" + DL_material_consumable.SelectedValue + "','" + TB_material_ERP_no.Text + "','" + TB_remark.Text + "',getdate(),'" + DL_iscomputer.SelectedValue + "','"+TB_material_part_number.Text+ "','" + TB_material_detail_spec.Text + "')";
                SqlDataSource_Send.Insert();

                String material_code = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 material_code FROM material_master ORDER BY material_code DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    material_code = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，material_code = " + material_code;
                    if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text == "")
                    {

                        SqlDataSource_material_master.SelectCommand = "select * from material_master ORDER BY material_code DESC";

                    }
                    else if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text != "")
                    {
                        SqlDataSource_material_master.SelectCommand = "select * from material_master where material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";

                    }
                    else if (DL_sel_material_type.SelectedValue != "" && Sel_material_name.Text == "")
                    {
                        SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC";

                    }
                    else
                    {
                        SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' or material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";


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


            TB_material_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_material_type.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            DL_material_type.SelectedValue= gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_material_name.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            DL_material_unit.SelectedValue = gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_safety_quantity.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            DL_material_consumable.SelectedValue = gvr.Cells[6].Text.Replace("&nbsp;", "");
           
            TB_material_ERP_no.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[8].Text.Replace("&nbsp;", "");
            DL_iscomputer.SelectedValue = gvr.Cells[9].Text.Replace("&nbsp;", "");
     
            TB_material_part_number.Text= gvr.Cells[10].Text.Replace("&nbsp;", "");
            TB_material_detail_spec.Text= gvr.Cells[11].Text.Replace("&nbsp;", "");

            BT_Modify.Visible =  true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {
          
            SqlDataSource_Send.UpdateCommand = "UPDATE material_master SET " +
                 "material_type = '" + TB_material_type.Text + "', material_name = '" + TB_material_name.Text + "', material_unit = '" + DL_material_unit.SelectedValue +
                    "',safety_quantity = '" + TB_safety_quantity.Text + "', material_consumable = '" + DL_material_consumable.SelectedValue + "', material_ERP_no = '" + TB_material_ERP_no.Text +"', remark = '" + TB_remark.Text +
                       "',material_part_number = '" + TB_material_part_number.Text + "', material_detail_spec = '" + TB_material_detail_spec.Text +
                    "', time_stamp=getdate(),iscomputer = '" + DL_iscomputer.SelectedValue + "'  WHERE material_code = '" + TB_material_code.Text + "'";

            SqlDataSource_Send.Update();
            LB_Message.Text = "修改資料完成，material_code = " + TB_material_code.Text;
            if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text == "")
            {

                SqlDataSource_material_master.SelectCommand = "select * from material_master ORDER BY material_code DESC";

            }
            else if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text != "")
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";

            }
            else if (DL_sel_material_type.SelectedValue != "" && Sel_material_name.Text == "")
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC";

            }
            else
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' or material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";


            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text == "")
            {

                SqlDataSource_material_master.SelectCommand = "select * from material_master ORDER BY material_code DESC";

            }
            else if (DL_sel_material_type.SelectedValue == "" && Sel_material_name.Text != "")
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";

            }
            else if (DL_sel_material_type.SelectedValue != "" && Sel_material_name.Text == "")
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC";

            }
            else
            {
                SqlDataSource_material_master.SelectCommand = "select * from material_master where material_type like '%" + DL_sel_material_type.SelectedValue + "%' or material_name like '%" + Sel_material_name.Text + "%' ORDER BY material_code DESC";


            }



           
            

        }

        protected void DL_material_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_material_type.Text = DL_material_type.SelectedValue;
        }
    }
}