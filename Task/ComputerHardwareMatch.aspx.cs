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
    public partial class ComputerHardwareMatch : System.Web.UI.Page
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


          
            TB_computer_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
        
            DL_hardware1.SelectedValue = "";
            DL_hardware2.Items.Clear();
            TB_remark.Text = "";
            TB_hardwareMatch.Text = "";
       
            SqlDataSource_computer_hardware_match.SelectCommand =" select "+
               " computer_hardware_match.computer_hardware_match_serial," +
               " computer_hardware_match.computer_serial," +
               " computer_hardware_match.material_serial," +
               " material.material_code," +
                "  material.material_type," +
               "  material.material_name," +
               " computer_detail.product_name," +
               " computer_hardware_match.remark," +
               "  computer_hardware_match.time_stamp" +
               "  from computer_hardware_match" +
               "  left join computer_detail" +
               " on computer_hardware_match.computer_serial = computer_detail.computer_serial" +
               "  left join" +
               " (SELECT dbo.material_detail.material_serial, dbo.material_master.material_code, dbo.material_master.material_type," +
               "             dbo.material_master.material_name, dbo.material_master.iscomputer" +
               "         FROM              dbo.material_master inner JOIN" +
               "              dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
             " where iscomputer = 'D') as material" +
               " on computer_hardware_match.material_serial = material.material_serial" +
                "  where computer_hardware_match.computer_serial = " + TB_computer_serial.Text + "  order by computer_hardware_match_serial desc";



            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.computer_hardware_match"
           + "(computer_serial"
      
           + ", material_serial"  
           + ", remark"      
           + ", time_stamp)"
            + "VALUES"
            +"("+ TB_computer_serial.Text+""
            + ",'" + DL_hardware2.SelectedValue + "'"
            + ",'" + TB_remark.Text + "'"
            +",getdate() )";

                SqlDataSource_Send.Insert();

         
                    LB_Message.Text = "新增資料完成";
               

            
            } 
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

            SqlDataSource_computer_hardware_match.SelectCommand = " select " +
              " computer_hardware_match.computer_hardware_match_serial," +
              " computer_hardware_match.computer_serial," +
              " computer_hardware_match.material_serial," +
              " material.material_code," +
               "  material.material_type," +
              "  material.material_name," +
              " computer_detail.product_name," +
              " computer_hardware_match.remark," +
              "  computer_hardware_match.time_stamp" +
              "  from computer_hardware_match" +
              "  left join computer_detail" +
              " on computer_hardware_match.computer_serial = computer_detail.computer_serial" +
              "  left join" +
              " (SELECT dbo.material_detail.material_serial, dbo.material_master.material_code, dbo.material_master.material_type," +
              "             dbo.material_master.material_name, dbo.material_master.iscomputer" +
              "         FROM              dbo.material_master inner JOIN" +
              "              dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
            " where iscomputer = 'D') as material" +
              " on computer_hardware_match.material_serial = material.material_serial" +
               "  where computer_hardware_match.computer_serial = " + TB_computer_serial.Text + "  order by computer_hardware_match_serial desc";
        }

        protected void BT_modiClick(object sender, System.EventArgs e)
        {
       
            Button btn = (Button)sender;

    
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

          
            TB_computer_serial.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            DL_hardware1.SelectedValue= gvr.Cells[5].Text.Replace("&nbsp;", "");
            TB_computer_hardware_match_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            SqlDataSource2.SelectCommand = " SELECT     dbo.material_detail.material_serial," +
                      "  '物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'，'+dbo.material_master.material_name as material_name" +
                      " FROM    dbo.material_master inner JOIN" +
                      " dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
                      " where material_serial not in (select material_serial from computer_hardware_match)  and iscomputer = 'D'" +
                      " and material_master.material_type   = '" + DL_hardware1.SelectedValue + "' ";

            TB_hardwareMatch.Text = "material serial:"+gvr.Cells[2].Text.Replace("&nbsp;", "")+" "+ gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
         

            BT_Modify.Visible = true;
            BT_Send.Visible = false;


           
        }

        protected void BT_delClick(object sender, System.EventArgs e)
        {

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TB_computer_hardware_match_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            SqlDataSource_Send.DeleteCommand = "Delete from dbo.computer_hardware_match"

         + " WHERE computer_hardware_match_serial = '" + TB_computer_hardware_match_serial.Text + "'";
            LB_Message.Text = "刪除資料完成";

            SqlDataSource_Send.Delete();
            SqlDataSource_computer_hardware_match.SelectCommand = " select " +
               " computer_hardware_match.computer_hardware_match_serial," +
               " computer_hardware_match.computer_serial," +
               " computer_hardware_match.material_serial," +
               " material.material_code," +
               "  material.material_type," +
               "  material.material_name," +
               " computer_detail.product_name," +
               " computer_hardware_match.remark," +
               "  computer_hardware_match.time_stamp" +
               "  from computer_hardware_match" +
               "  left join computer_detail" +
               " on computer_hardware_match.computer_serial = computer_detail.computer_serial" +
               "  left join" +
               " (SELECT dbo.material_detail.material_serial, dbo.material_master.material_code, dbo.material_master.material_type," +
               "             dbo.material_master.material_name, dbo.material_master.iscomputer" +
               "         FROM              dbo.material_master inner JOIN" +
               "              dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
             " where iscomputer = 'D') as material" +
               " on computer_hardware_match.material_serial = material.material_serial" +
                "  where computer_hardware_match.computer_serial = " + TB_computer_serial.Text + "  order by computer_hardware_match_serial desc";

            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

            SqlDataSource_Send.UpdateCommand = "UPDATE dbo.computer_hardware_match"
 
   + " Set material_serial = '" + DL_hardware2.SelectedValue + "'"

   + ", remark ='" + TB_remark.Text + "'"

   + ", time_stamp =getdate()"
   + " WHERE computer_hardware_match_serial = '" + TB_computer_hardware_match_serial.Text + "'";


            SqlDataSource_Send.Update();
            LB_Message.Text = "修改完成，computer_hardware_match_serial = " + TB_computer_hardware_match_serial.Text;
            SqlDataSource_computer_hardware_match.SelectCommand = " select " +
               " computer_hardware_match.computer_hardware_match_serial," +
               " computer_hardware_match.computer_serial," +
               " computer_hardware_match.material_serial," +
               " material.material_code," +
                "  material.material_type," +
               "  material.material_name," +
               " computer_detail.product_name," +
               " computer_hardware_match.remark," +
               "  computer_hardware_match.time_stamp" +
               "  from computer_hardware_match" +
               "  left join computer_detail" +
               " on computer_hardware_match.computer_serial = computer_detail.computer_serial" +
               "  left join" +
               " (SELECT dbo.material_detail.material_serial, dbo.material_master.material_code, dbo.material_master.material_type," +
               "             dbo.material_master.material_name, dbo.material_master.iscomputer" +
               "         FROM              dbo.material_master inner JOIN" +
               "              dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
             " where iscomputer = 'D') as material" +
               " on computer_hardware_match.material_serial = material.material_serial" +
                "  where computer_hardware_match.computer_serial = " + TB_computer_serial.Text + "  order by computer_hardware_match_serial desc";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void DL_hardware1_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataSource2.SelectCommand = " SELECT     dbo.material_detail.material_serial,"+
                                "  '物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'，'+dbo.material_master.material_name as material_name" +
                            " FROM    dbo.material_master inner JOIN" +
                            " dbo.material_detail ON dbo.material_master.material_code = dbo.material_detail.material_code" +
                            " where material_serial not in (select material_serial from computer_hardware_match)  and iscomputer = 'D'" +
                            " and material_master.material_type  = '" + DL_hardware1.SelectedValue + "' ";


        }



        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (DL_storage_code.SelectedValue!="")
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.storage_area,storage.storage_id,computer_detail.computer_serial,computer_detail.product_name from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial left join computer_detail on material_detail.material_serial=computer_detail.material_serial where iscomputer ='C' and computer_serial is not null and material_name like '%" + Sel_material_name.Text + "%' and product_name like '%" + Sel_product_name.Text + "%' and storage.storage_id = '" + DL_storage_serial.SelectedValue + "'  ORDER BY computer_serial DESC";

            }
           
            else
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.storage_area,storage.storage_id,computer_detail.computer_serial,computer_detail.product_name from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial left join computer_detail on material_detail.material_serial=computer_detail.material_serial where iscomputer ='C' and computer_serial is not null and material_name like '%" + Sel_material_name.Text + "%' and product_name like '%" + Sel_product_name.Text + "%'   ORDER BY computer_serial DESC";

            }






        }
    }
}