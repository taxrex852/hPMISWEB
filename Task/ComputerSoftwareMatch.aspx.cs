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
    public partial class ComputerSoftwareMatch : System.Web.UI.Page
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


            TB_material_serial.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_computer_serial.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
        
            DL_software_manufacturer.SelectedValue = "";
            DL_software_name.Items.Clear();
            TB_remark.Text = "";
            TB_Licensed.Text = "";
       
            SqlDataSource_computer_software_match.SelectCommand = "select " +
                 " computer_software_match.computer_software_match_serial," +
                " computer_software_match.computer_serial," +
                " computer_software_match.material_serial," +
                " computer_software_match.software_serial," +
                " computer_detail.product_name," +
                " software.software_manufacturer," +
                " software.software_name," +
                " software.license_number," +
                " computer_software_match.remark," +
                " computer_software_match.time_stamp" +
                " from computer_software_match " +
                "left join software_detail " +
                "on computer_software_match.software_serial = software_detail.software_serial" +
                " left join computer_detail " +
                "on computer_software_match.computer_serial = computer_detail.computer_serial" +
                " left join " +
                "(SELECT software_detail.software_serial,software_master.software_name,software_master.software_manufacturer,dbo.software_detail.license_number" +
                " FROM  dbo.software_master left JOIN " +
                " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code) as software " +
                "on computer_software_match.software_serial = software.software_serial" +
                " where computer_software_match.computer_serial = " + TB_computer_serial.Text + " order by computer_software_match_serial desc";



            BT_Modify.Visible = false;
            BT_Send.Visible = true;


        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.computer_software_match"
           + "(computer_serial"
           + ", software_serial"
           + ", material_serial"  
           + ", remark"      
           + ", time_stamp)"
            + "VALUES"
            +"("+ TB_computer_serial.Text+""
            + ",'" + DL_software_name.SelectedValue + "'"
            + ",'" + TB_material_serial.Text + "'"
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

            SqlDataSource_computer_software_match.SelectCommand = "select " +
                " computer_software_match.computer_software_match_serial," +
                " computer_software_match.computer_serial," +
                " computer_software_match.material_serial," +
                " computer_software_match.software_serial," +
                " computer_detail.product_name," +
                " software.software_manufacturer," +
                " software.software_name," +
                " software.license_number," +
                " computer_software_match.remark," +
                " computer_software_match.time_stamp" +
                " from computer_software_match " +
                "left join software_detail " +
                "on computer_software_match.software_serial = software_detail.software_serial" +
                " left join computer_detail " +
                "on computer_software_match.computer_serial = computer_detail.computer_serial" +
                " left join " +
                "(SELECT software_detail.software_serial,software_master.software_name,software_master.software_manufacturer,dbo.software_detail.license_number" +
                " FROM  dbo.software_master left JOIN " +
                " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code) as software " +
                "on computer_software_match.software_serial = software.software_serial" +
                " where computer_software_match.computer_serial = " + TB_computer_serial.Text + " order by computer_software_match_serial desc";
        }

        protected void BT_modiClick(object sender, System.EventArgs e)
        {
       
            Button btn = (Button)sender;

    
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            TB_material_serial.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_computer_serial.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            DL_software_manufacturer.SelectedValue= gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_computer_software_match_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            SqlDataSource2.SelectCommand = " SELECT  " +
               " dbo.software_detail.software_serial," +
               " dbo.software_master.software_name+' license:'+dbo.software_detail.license_number as software" +
               " FROM dbo.software_master left JOIN" +
               " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code" +
              " where software_serial not in (select software_serial from computer_software_match) and  software_manufacturer = '" + gvr.Cells[6].Text.Replace("&nbsp;", "") + "'" +
               " order by software_serial desc ";
            TB_Licensed.Text = gvr.Cells[6].Text.Replace("&nbsp;", "")+""+ gvr.Cells[7].Text.Replace("&nbsp;", "")+" License:"+ gvr.Cells[8].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");
         

            BT_Modify.Visible = true;
            BT_Send.Visible = false;


           
        }

        protected void BT_delClick(object sender, System.EventArgs e)
        {

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TB_computer_software_match_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            SqlDataSource_Send.DeleteCommand = "Delete from dbo.computer_software_match"

         + " WHERE computer_software_match_serial = '" + TB_computer_software_match_serial.Text + "'";
            LB_Message.Text = "刪除資料完成";

            SqlDataSource_Send.Delete();
            SqlDataSource_computer_software_match.SelectCommand = "select " +
              " computer_software_match.computer_software_match_serial," +
              " computer_software_match.computer_serial," +
              " computer_software_match.material_serial," +
              " computer_software_match.software_serial," +
              " computer_detail.product_name," +
              " software.software_manufacturer," +
              " software.software_name," +
              " software.license_number," +
              " computer_software_match.remark," +
              " computer_software_match.time_stamp" +
              " from computer_software_match " +
              "left join software_detail " +
              "on computer_software_match.software_serial = software_detail.software_serial" +
              " left join computer_detail " +
              "on computer_software_match.computer_serial = computer_detail.computer_serial" +
              " left join " +
              "(SELECT software_detail.software_serial,software_master.software_name,software_master.software_manufacturer,dbo.software_detail.license_number" +
              " FROM  dbo.software_master left JOIN " +
              " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code) as software " +
              "on computer_software_match.software_serial = software.software_serial" +
              " where computer_software_match.computer_serial = " + TB_computer_serial.Text + " order by computer_software_match_serial desc";

            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

            SqlDataSource_Send.UpdateCommand = "UPDATE dbo.computer_software_match"
 
   + " Set software_serial = '" + DL_software_name.SelectedValue + "'"

   + ", remark ='" + TB_remark.Text + "'"

   + ", time_stamp =getdate()"
   + " WHERE computer_software_match_serial = '" + TB_computer_software_match_serial.Text + "'";


            SqlDataSource_Send.Update();
            LB_Message.Text = "修改完成，computer_software_match_serial = " + TB_computer_software_match_serial.Text;
            SqlDataSource_computer_software_match.SelectCommand = "select " +
             " computer_software_match.computer_software_match_serial," +
             " computer_software_match.computer_serial," +
             " computer_software_match.material_serial," +
             " computer_software_match.software_serial," +
             " computer_detail.product_name," +
              " software.software_manufacturer," +
             " software.software_name," +
             " software.license_number," +
             " computer_software_match.remark," +
             " computer_software_match.time_stamp" +
             " from computer_software_match " +
             "left join software_detail " +
             "on computer_software_match.software_serial = software_detail.software_serial" +
             " left join computer_detail " +
             "on computer_software_match.computer_serial = computer_detail.computer_serial" +
             " left join " +
             "(SELECT software_detail.software_serial,software_master.software_name,software_master.software_manufacturer,dbo.software_detail.license_number" +
             " FROM  dbo.software_master left JOIN " +
             " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code) as software " +
             "on computer_software_match.software_serial = software.software_serial" +
             " where computer_software_match.computer_serial = " + TB_computer_serial.Text + " order by computer_software_match_serial desc";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void DL_software_manufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataSource2.SelectCommand = " SELECT  " +
                " dbo.software_detail.software_serial," +
                " dbo.software_master.software_name+' license:'+dbo.software_detail.license_number as software"+                 
                " FROM dbo.software_master left JOIN"+
                " dbo.software_detail ON dbo.software_master.software_code = dbo.software_detail.software_code" +
                " where software_serial not in (select software_serial from computer_software_match) and  software_manufacturer = '" + DL_software_manufacturer.SelectedValue + "'" +
                " order by software_serial desc ";


        }
        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (DL_storage_code.SelectedValue != "")
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.storage_area,storage.storage_id,computer_detail.computer_serial,computer_detail.product_name from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial left join computer_detail on material_detail.material_serial=computer_detail.material_serial where iscomputer ='C' and computer_serial is not null  and material_name like '%" + Sel_material_name.Text + "%' and product_name like '%" + Sel_product_name.Text + "%' and storage.storage_id = '" + DL_storage_serial.SelectedValue + "'  ORDER BY computer_serial DESC";

            }

            else
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.storage_area,storage.storage_id,computer_detail.computer_serial,computer_detail.product_name from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial left join computer_detail on material_detail.material_serial=computer_detail.material_serial where iscomputer ='C' and computer_serial is not null  and material_name like '%" + Sel_material_name.Text + "%' and product_name like '%" + Sel_product_name.Text + "%'   ORDER BY computer_serial DESC";

            }






        }



    }
}