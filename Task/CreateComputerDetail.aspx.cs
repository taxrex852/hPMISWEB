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
    public partial class CreateComputerDetail : System.Web.UI.Page
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
            //TB_product_name.Text ="";
            TB_cpu_spec.Text = "";
            TB_ram_size.Text = "";
            TB_hdd_size.Text = "";
            TB_dvd_type.Text = "";
            DL_oem_os.SelectedValue = "";
            TB_remark.Text = "";


            SqlDataSource_computer_detail.SelectCommand = "select * from computer_detail where material_serial = " + TB_material_serial.Text + " ORDER BY computer_serial DESC";

            SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
            //SqlConnection Conn = new SqlConnection("Data Source=10.108.137.235;Password=spcmgr;User ID=sa;Initial Catalog=Y6P2_Materials_test");
            //SqlConnection Conn = new SqlConnection("Data Source=127.0.0.1;Password=test;User ID=test;Initial Catalog=Y6P2_Materials");
            Conn.Open();

            SqlDataAdapter x_maxmin = new SqlDataAdapter("SELECT count(*) as countComputer_serial from computer_detail where material_serial = '" + TB_material_serial.Text + "'  ", Conn);
            DataSet x_maxminds = new DataSet();
            x_maxmin.Fill(x_maxminds, "CountComputerSerial");
            DataTable myTable = x_maxminds.Tables["CountComputerSerial"];
            string myString1 = "";
            myString1 = myString1 + myTable.Rows[0]["countComputer_serial"];

            if (myString1=="0")
            {
                BT_Modify.Visible = false;
                BT_Send.Visible = true;

            }
            else {

                BT_Modify.Visible = true;
                BT_Send.Visible = false;

            }

           
        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {

            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.computer_detail"
           +"(material_serial"
           //+ ", product_name"
           + ", cpu_spec"
           + ", ram_size"
           + ", hdd_size"
           + ", dvd_type"
           + ", remark"
           + ", oem_os"
           + ", time_stamp)"
            + "VALUES"
            +"("+TB_material_serial.Text+""
            //+ ",'" + TB_product_name.Text + "'"
            + ",'" + TB_cpu_spec.Text + "'"
            + ",'" + TB_ram_size.Text + "'"
            + ",'" + TB_hdd_size.Text + "'"
            + ",'" + TB_dvd_type.Text + "'"
            + ",'" + TB_remark.Text + "'"
            + ",'" + DL_oem_os.SelectedValue + "' "
            +",getdate() )";








                SqlDataSource_Send.Insert();

                String computer_serial = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 computer_serial FROM computer_detail ORDER BY computer_serial DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    computer_serial = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，computer_serial = " + computer_serial;
                    SqlDataSource_computer_detail.SelectCommand = "select * from computer_detail where material_serial = " + TB_material_serial.Text + " ORDER BY computer_serial DESC";

                }

            
            } 
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }
            BT_Modify.Visible = false;
            BT_Send.Visible = false;

        }

        protected void BT_modiClick(object sender, System.EventArgs e)
        {
       
            Button btn = (Button)sender;

    
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            //TB_product_name.Text = gvr.Cells[3].Text.Replace("&nbsp;", ""); 
            TB_computer_serial.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_cpu_spec.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_ram_size.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_hdd_size.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            TB_dvd_type.Text = gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            DL_oem_os.SelectedValue = gvr.Cells[8].Text.Replace("&nbsp;", "");
            

            BT_Modify.Visible = true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

            SqlDataSource_Send.UpdateCommand = "UPDATE dbo.computer_detail"
   + " SET material_serial = '" + TB_material_serial.Text + "'"
   //+ ", product_name ='" + TB_product_name.Text + "'"
   + ", cpu_spec ='" + TB_cpu_spec.Text + "'"
   + ", ram_size ='" + TB_ram_size.Text + "'"
   + ", hdd_size ='" + TB_hdd_size.Text + "'"
   + ", dvd_type ='" + TB_dvd_type.Text + "'"
   + ", remark ='" + TB_remark.Text + "'"
   + ", oem_os ='"+ DL_oem_os.SelectedValue + "'"
   + ", time_stamp =getdate()"
   + " WHERE computer_serial = '" + TB_computer_serial.Text + "'";


            SqlDataSource_Send.Update();
            LB_Message.Text = "修改完成，computer_serial = " + TB_computer_serial.Text;
            SqlDataSource_computer_detail.SelectCommand = "select * from computer_detail where material_serial = " + TB_material_serial.Text + " ORDER BY computer_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = false;

        }

        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (DL_storage_code.SelectedValue != "" && DL_storage_serial.SelectedValue != "")
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.* from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial where iscomputer ='C'   and material_name like '%" + Sel_material_name.Text + "%'  and storage.storage_id = '" + DL_storage_serial.SelectedValue + "'  ";
                SqlDataSource_computer_detail.SelectCommand = "";
                GridView1.DataBind();
            }
            else if (DL_storage_code.SelectedValue != "" && DL_storage_serial.SelectedValue == "")
            {
                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.* from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial where iscomputer ='C'   and material_name like '%" + Sel_material_name.Text + "%'  and storage.storage_code = '" + DL_storage_code.SelectedValue + "'  ";
                SqlDataSource_computer_detail.SelectCommand = "";
                GridView1.DataBind();

                            }
            else
            {

                SqlDataSource_master.SelectCommand = "select material_master.material_code,material_master.material_name,material_detail.material_serial,storage.* from material_master left join material_detail on material_master.material_code=material_detail.material_code left join (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area, dbo.storage_detail.storage_name, dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code) as storage on material_detail.storage_serial = storage.storage_serial where iscomputer ='C'   and material_name like '%" + Sel_material_name.Text + "%'   ";
                SqlDataSource_computer_detail.SelectCommand = "";
                GridView1.DataBind();
            }






        }
  
        protected void DL_storage_code_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DL_storage_serial.Items.Clear();
            DL_storage_serial.Items.Add(new ListItem("", ""));
            DL_storage_serial.SelectedValue = "";
        }
    }
}