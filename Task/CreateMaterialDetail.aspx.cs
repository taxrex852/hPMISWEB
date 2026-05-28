using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.IO;                  

namespace WebApplication2
{
    public partial class CreateMaterialDetail : System.Web.UI.Page
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

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;



            TB_material_code.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_material_type.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_material_name.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_material_unit.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_safety_quantity.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            TB_material_consumable.Text = gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_material_ERP_no.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");


            TB_material_serial.Text = "";
       
            TB_material_asset_no.Text = "";
            DL_storage_code.SelectedValue = "";
            DL_storage_serial.Items.Clear();
            DL_storage_serial_temp.Text = "";
            DL_TaskPeople.SelectedValue = TextBoxName.Text;
            TB_warranty_date_start.Text = "";
            TB_warranty_date_end.Text = "";
            TB_cost_center.Text = "";
            TB_pr_no.Text = "";
            TB_po_no.Text = "";
            TB_iqc_no.Text = "";
            TB_material_asset_name.Text = "";
            TB_device_serial_no.Text = "";
            TB_device_image.Text = "";
            DL_System.SelectedValue = "";
            TB_remark.Text = "";
            TB_material_getdate.Text = "";



            if (Sel_Storage_code.SelectedValue != "" && Sel_Storage_serial.SelectedValue!="")
            {

                SqlDataSource_material_detail.SelectCommand = "SELECT material_serial,material_code,material_asset_no,material_detail.storage_serial,storage_detail.storage_code,storage_detail.storage_id,storage_detail.storage_name,employee_id,convert(varchar, warranty_date_start, 23) as warranty_date_start,convert(varchar, warranty_date_end, 23) as warranty_date_end,cost_center,pr_no,po_no,iqc_no,material_asset_name,device_serial_no,device_image,system_name,material_detail.remark,convert(varchar, material_getdate, 23) as material_getdate,storage_modify_getdate,material_detail.time_stamp  FROM  material_detail left join  storage_detail on material_detail.storage_serial = storage_detail.storage_serial  where material_code = " + TB_material_code.Text + " and material_detail.storage_serial= '" + Sel_Storage_serial.SelectedValue + "' ORDER BY material_serial DESC";
            }

            else
            {
                SqlDataSource_material_detail.SelectCommand = "SELECT material_serial,material_code,material_asset_no,material_detail.storage_serial,storage_detail.storage_code,storage_detail.storage_id,storage_detail.storage_name,employee_id,convert(varchar, warranty_date_start, 23) as warranty_date_start,convert(varchar, warranty_date_end, 23) as warranty_date_end,cost_center,pr_no,po_no,iqc_no,material_asset_name,device_serial_no,device_image,system_name,material_detail.remark,convert(varchar, material_getdate, 23) as material_getdate,storage_modify_getdate,material_detail.time_stamp  FROM  material_detail left join  storage_detail on material_detail.storage_serial = storage_detail.storage_serial  where material_code = " + TB_material_code.Text + " ORDER BY material_serial DESC";
            }



            
            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }
        protected void BT_Send_Click(object sender, EventArgs e)
        {


            try
            {

                SqlDataSource_Send.InsertCommand = "INSERT INTO material_detail " +
                   "(material_code,material_asset_no,storage_serial,employee_id,warranty_date_start,warranty_date_end,cost_center,pr_no,po_no,iqc_no,material_asset_name,device_serial_no,device_image,system_name,remark,material_getdate,storage_modify_getdate,time_stamp) VALUES( '"
                   + TB_material_code.Text + "','" + TB_material_asset_no.Text + "','" + DL_storage_serial.SelectedValue + "','" + DL_TaskPeople.SelectedValue +
                  "', '" + TB_warranty_date_start.Text + "','" + TB_warranty_date_end.Text + "','" + TB_cost_center.Text + "','" + TB_pr_no.Text +
                  "', '" + TB_po_no.Text + "','" + TB_iqc_no.Text + "','" + TB_material_asset_name.Text + "','" + TB_device_serial_no.Text +
                  "','" + TB_device_image.Text + "','" + DL_System.Text + "','" + TB_remark.Text + "','" + TB_material_getdate.Text +
                  "', getdate(),getdate())";
                SqlDataSource_Send.Insert();

                String material_serial = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 material_serial FROM material_detail ORDER BY material_serial DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    material_serial = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，material_serial = " + material_serial;
                    SqlDataSource_material_detail.SelectCommand = "SELECT material_serial,material_code,material_asset_no,material_detail.storage_serial,storage_detail.storage_code,storage_detail.storage_id,storage_detail.storage_name,employee_id,convert(varchar, warranty_date_start, 23) as warranty_date_start,convert(varchar, warranty_date_end, 23) as warranty_date_end,cost_center,pr_no,po_no,iqc_no,material_asset_name,device_serial_no,device_image,system_name,material_detail.remark,convert(varchar, material_getdate, 23) as material_getdate,storage_modify_getdate,material_detail.time_stamp  FROM  material_detail left join  storage_detail on material_detail.storage_serial = storage_detail.storage_serial  where material_code = " + TB_material_code.Text + " ORDER BY material_serial DESC";

                }
                String material_string = "";
                if (TB_device_serial_no.Text != "")
                {

                    material_string += "，設備序號SN碼:" + TB_device_serial_no.Text + "";

                }
                if (TB_material_asset_no.Text != "")
                {

                    material_string += "，固定資產編號:" + TB_material_asset_no.Text + "";

                }
                if (TB_material_asset_name.Text != "")
                {

                    material_string += "，固定資產名稱:" + TB_material_asset_name.Text + "";

                }
                if (TB_material_getdate.Text != "")
                {
                    material_string += "，物料取得時間:" + TB_material_getdate.Text + "";

                }
                if (TB_warranty_date_start.Text != "")
                {

                    material_string += "，保固起算日期:" + TB_warranty_date_start.Text + "";

                }
                if (TB_warranty_date_end.Text != "")
                {

                    material_string += "，保固截止日期:" + TB_warranty_date_end.Text + "";

                }
                if (DL_System.SelectedValue != "")
                {

                    material_string += "，專用系統名稱:" + DL_System.SelectedValue + "";

                }
                if (TB_cost_center.Text != "")
                {

                    material_string += "，成本中心:" + TB_cost_center.Text + "";

                }
                if (TB_pr_no.Text != "")
                {

                    material_string += "，請購單號:" + TB_pr_no.Text + "";

                }
                if (TB_po_no.Text != "")
                {

                    material_string += "，採購單號:" + TB_po_no.Text + "";

                }
                if (TB_iqc_no.Text != "")
                {

                    material_string += "，驗收單號:" + TB_iqc_no.Text + "";

                }
                if (TB_device_image.Text != "")
                {

                    material_string += "，設備照片:" + TB_device_image.Text + "";

                }
                if (TB_remark.Text != "")
                {

                    material_string += "，備註:" + TB_remark.Text + "";

                }



                SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.TaskList" +
               "(IssueID," +
               "TaskDate," +
               "employee_id," +
               "SystemID," +
               "TaskDescription," +
               "TaskHours)" +
               "  VALUES (2684," +
               "convert(varchar(10),getdate(),121)," +
               " '" + TextBoxName.Text + "'," +
               "27," +
               "'Y6P2物料異動作業：進行物料詳細資料新增，物料代碼:" + TB_material_code.Text + "，" +
               "物料序號:" + material_serial + "，" +
               "設備大項名稱:" + TB_material_name.Text + "，" +
               "儲位區域:" + DL_storage_code.SelectedItem.Text + "，" +
               "儲位編號:" + DL_storage_serial.SelectedItem.Text + "，保管人為:" + DL_TaskPeople.SelectedValue + "(" + DL_TaskPeople.SelectedItem.Text + ")" + material_string + "'" +
               ",0)";
                SqlDataSource_Send.Insert();


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

           
            TB_material_serial.Text = "";
         
            TB_material_asset_no.Text = "";
            DL_storage_code.SelectedValue = "";
            DL_storage_serial.Items.Clear();

            DL_TaskPeople.SelectedValue = TextBoxName.Text;
            TB_warranty_date_start.Text = "";
            TB_warranty_date_end.Text = "";
            TB_cost_center.Text = "";
            TB_pr_no.Text = "";
            TB_po_no.Text = "";
            TB_iqc_no.Text = "";
            TB_material_asset_name.Text = "";
            TB_device_serial_no.Text = "";
            TB_device_image.Text = "";
            DL_System.SelectedValue = "";
            TB_remark.Text = "";
            TB_material_getdate.Text = "";

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;


            TB_material_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
          
            TB_material_asset_no.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
            DL_storage_code.SelectedValue = gvr.Cells[21].Text.Replace("&nbsp;", "");
            DL_storage_code_temp.Text = gvr.Cells[21].Text.Replace("&nbsp;", "");


            SqlDataSource_DL_storage_serial.SelectCommand = " SELECT storage_id, storage_serial FROM storage_detail WHERE storage_code = '" + DL_storage_code_temp.Text + "'";


            if (gvr.Cells[22].Text != "0")
            {
                DL_storage_serial.SelectedValue = gvr.Cells[22].Text;
            }





            DL_TaskPeople.SelectedValue = gvr.Cells[6].Text.Replace("&nbsp;", "");

            TB_warranty_date_start.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            TB_warranty_date_end.Text = gvr.Cells[8].Text.Replace("&nbsp;", "");

            TB_cost_center.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");
            TB_pr_no.Text = gvr.Cells[10].Text.Replace("&nbsp;", "");
            TB_po_no.Text = gvr.Cells[11].Text.Replace("&nbsp;", "");
            TB_iqc_no.Text = gvr.Cells[12].Text.Replace("&nbsp;", "");
            TB_material_asset_name.Text = gvr.Cells[13].Text.Replace("&nbsp;", "");
            TB_device_serial_no.Text = gvr.Cells[14].Text.Replace("&nbsp;", "");
            TB_device_image.Text = gvr.Cells[15].Text.Replace("&nbsp;", "");
            DL_System.SelectedValue = gvr.Cells[16].Text.Replace("&nbsp;", "");
            TB_remark.Text = gvr.Cells[17].Text.Replace("&nbsp;", "");

            TB_material_getdate.Text = gvr.Cells[18].Text.Replace("&nbsp;", "");
           



            BT_Modify.Visible = true;
            BT_Send.Visible = false;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {



            if (DL_storage_serial.SelectedValue == DL_storage_serial_temp.Text)
            {

                SqlDataSource_Send.UpdateCommand = "UPDATE material_detail SET " +
                "material_asset_no ='" + TB_material_asset_no.Text +   
                "',storage_serial ='" + DL_storage_serial_temp.Text +
                "',employee_id ='" + DL_TaskPeople.SelectedValue +
                "',warranty_date_start ='" + TB_warranty_date_start.Text +
                "',warranty_date_end ='" + TB_warranty_date_end.Text +
                "',cost_center ='" + TB_cost_center.Text +
                "',pr_no ='" + TB_pr_no.Text +
                "',po_no ='" + TB_po_no.Text +
                "',iqc_no ='" + TB_iqc_no.Text +
                "',material_asset_name ='" + TB_material_asset_name.Text +
                "',device_serial_no ='" + TB_device_serial_no.Text +
                "',device_image ='" + TB_device_image.Text +
                "',system_name ='" + DL_System.SelectedValue +
                "',remark ='" + TB_remark.Text +
                "',material_getdate ='" + TB_material_getdate.Text +
                    "',time_stamp = getdate()" +

                     "  WHERE material_serial = '" + TB_material_serial.Text + "'";


                SqlDataSource_Send.Update();
                LB_Message.Text = "修改完成，material_serial = " + TB_material_serial.Text;
            }
            else
            {
                SqlDataSource_Send.UpdateCommand = "UPDATE material_detail SET " +
               "material_asset_no ='" + TB_material_asset_no.Text +
                "',storage_serial ='" + DL_storage_serial.SelectedValue +
               "',employee_id ='" + DL_TaskPeople.SelectedValue +
               "',warranty_date_start ='" + TB_warranty_date_start.Text +
               "',warranty_date_end ='" + TB_warranty_date_end.Text +
               "',cost_center ='" + TB_cost_center.Text +
               "',pr_no ='" + TB_pr_no.Text +
               "',po_no ='" + TB_po_no.Text +
               "',iqc_no ='" + TB_iqc_no.Text +
               "',material_asset_name ='" + TB_material_asset_name.Text +
               "',device_serial_no ='" + TB_device_serial_no.Text +
               "',device_image ='" + TB_device_image.Text +
               "',system_name ='" + DL_System.SelectedValue +
               "',remark ='" + TB_remark.Text +
               "',material_getdate ='" + TB_material_getdate.Text +
               "',storage_modify_getdate = getdate()" +
               ",time_stamp = getdate()" +

                    "  WHERE material_serial = '" + TB_material_serial.Text + "'";


                SqlDataSource_Send.Update();
                LB_Message.Text = "修改完成，material_serial = " + TB_material_serial.Text;

            }

            String material_string = "";
            if (TB_device_serial_no.Text != "")
            {

                material_string += "，設備序號SN碼:" + TB_device_serial_no.Text + "";

            }
            if (TB_material_asset_no.Text != "")
            {

                material_string += "，固定資產編號:" + TB_material_asset_no.Text + "";

            }
            if (TB_material_asset_name.Text != "")
            {

                material_string += "，固定資產名稱:" + TB_material_asset_name.Text + "";

            }
            if (TB_material_getdate.Text != "")
            {
                material_string += "，物料取得時間:" + TB_material_getdate.Text + "";

            }
            if (TB_warranty_date_start.Text != "")
            {

                material_string += "，保固起算日期:" + TB_warranty_date_start.Text + "";

            }
            if (TB_warranty_date_end.Text != "")
            {

                material_string += "，保固截止日期:" + TB_warranty_date_end.Text + "";

            }
            if (DL_System.SelectedValue != "")
            {

                material_string += "，專用系統名稱:" + DL_System.SelectedValue + "";

            }
            if (TB_cost_center.Text != "")
            {

                material_string += "，成本中心:" + TB_cost_center.Text + "";

            }
            if (TB_pr_no.Text != "")
            {

                material_string += "，請購單號:" + TB_pr_no.Text + "";

            }
            if (TB_po_no.Text != "")
            {

                material_string += "，採購單號:" + TB_po_no.Text + "";

            }
            if (TB_iqc_no.Text != "")
            {

                material_string += "，驗收單號:" + TB_iqc_no.Text + "";

            }
            if (TB_device_image.Text != "")
            {

                material_string += "，設備照片:" + TB_device_image.Text + "";

            }
            if (TB_remark.Text != "")
            {

                material_string += "，備註:" + TB_remark.Text + "";

            }


            SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.TaskList" +
           "(IssueID," +
           "TaskDate," +
           "employee_id," +
           "SystemID," +
           "TaskDescription," +
           "TaskHours)" +
           "  VALUES (2684," +
           "convert(varchar(10),getdate(),121)," +
           " '" + TextBoxName.Text + "'," +
           "27," +
           "'Y6P2物料異動作業：進行物料詳細資料修改，物料代碼:" + TB_material_code.Text + "，" +
           "物料序號:" + TB_material_serial.Text + "，" +
            "設備大項名稱:" + TB_material_name.Text + "，" +
               "儲位區域:" + DL_storage_code.SelectedItem.Text + "，" +
               "儲位編號:" + DL_storage_serial.SelectedItem.Text + "，保管人為:" + DL_TaskPeople.SelectedValue + "(" + DL_TaskPeople.SelectedItem.Text + ")" + material_string + "'" +
               ",0)";
            SqlDataSource_Send.Insert();

            SqlDataSource_material_detail.SelectCommand = "SELECT material_serial,material_code,material_asset_no,material_detail.storage_serial,storage_detail.storage_code,storage_detail.storage_id,storage_detail.storage_name,employee_id,convert(varchar, warranty_date_start, 23) as warranty_date_start,convert(varchar, warranty_date_end, 23) as warranty_date_end,cost_center,pr_no,po_no,iqc_no,material_asset_name,device_serial_no,device_image,system_name,material_detail.remark,convert(varchar, material_getdate, 23) as material_getdate,storage_modify_getdate,material_detail.time_stamp  FROM  material_detail left join  storage_detail on material_detail.storage_serial = storage_detail.storage_serial  where material_code = " + TB_material_code.Text + " ORDER BY material_serial DESC";
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

        protected void DL_storage_code_SelectedIndexChanged(object sender, EventArgs e)
        {


            SqlDataSource_DL_storage_serial.SelectCommand = " SELECT storage_id, storage_serial FROM storage_detail WHERE storage_code = '" + DL_storage_code.SelectedValue + "'";



        }

        protected void BT_select_Click(object sender, EventArgs e)
        {


            if (Sel_Storage_code.SelectedValue != "" && Sel_Storage_serial.SelectedValue != "")
            {

                SqlDataSource_material_master.SelectCommand = "SELECT * FROM material_master where material_code in (select material_code from material_detail where storage_serial='" + Sel_Storage_serial.SelectedValue + "') and material_name like '%" + Sel_material_name.Text + "%' and material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC";
                SqlDataSource_material_detail.SelectCommand = "";
                GridView_storage_detail.DataBind();
            }
            else if (Sel_Storage_code.SelectedValue != "" && Sel_Storage_serial.SelectedValue == "")
            {
                SqlDataSource_material_master.SelectCommand = "SELECT * FROM material_master a where EXISTS (select * from (select storage_code,material_detail.storage_serial,material_code from material_detail left join (select storage_serial,storage_master.storage_code from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial where storage.storage_code ='" + Sel_Storage_code.SelectedValue + "') b where  a.material_code=b.material_code) and material_name like '%" + Sel_material_name.Text + "%' and material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC ";
                SqlDataSource_material_detail.SelectCommand = "";
                GridView_storage_detail.DataBind();



            }
            else
            {

                SqlDataSource_material_master.SelectCommand = "SELECT * FROM material_master where  material_name like '%" + Sel_material_name.Text + "%' and material_type like '%" + DL_sel_material_type.SelectedValue + "%' ORDER BY material_code DESC";
                SqlDataSource_material_detail.SelectCommand = "";
                GridView_storage_detail.DataBind();
            }

        }

        protected void Sel_Storage_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sel_Storage_serial.Items.Clear();
            Sel_Storage_serial.Items.Add(new ListItem("", ""));
            Sel_Storage_serial.SelectedValue = "";
        }


    }

}


