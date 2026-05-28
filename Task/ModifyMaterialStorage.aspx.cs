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
    public partial class ModifyMaterialStorage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            
                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                    Response.Redirect("Login.aspx", true);
                    
                }
                else
                {

                    TextBoxName.Text = Session["ID"].ToString();
                    TextBoxName1.Text = Session["ID"].ToString();





                }



                return;
            }


        }
      
        protected void BT_modiClick(object sender, EventArgs e)
        {


            Button btn = (Button)sender;
           

            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
               TB_material_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            TB_material_code.Text = gvr.Cells[0].Text.Replace("&nbsp;", "");

            DropDownList DL_storage_serial = (DropDownList)gvr.FindControl("DL_storage_serial");
            DL_storage_serial.Visible = true;
            SqlDataSource_DL_storage_serial.SelectCommand = "SELECT storage_id, storage_serial FROM storage_detail where storage_code = '" + gvr.Cells[8].Text.Replace("&nbsp;", "") + "' ";
            DL_storage_serial.SelectedValue= gvr.Cells[9].Text.Replace("&nbsp;", "");
      
            TB_storage_serial.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");

            DropDownList DL_storage_code = (DropDownList)gvr.FindControl("DL_storage_code");
            DL_storage_code.Visible = true;
            DL_storage_code.SelectedValue = gvr.Cells[8].Text.Replace("&nbsp;", "");

            DropDownList DL_TaskPeople = (DropDownList)gvr.FindControl("DL_TaskPeople");
            DL_TaskPeople.Visible = true;
            DL_TaskPeople.SelectedValue = TextBoxName.Text;

            Button BT_modi = (Button)gvr.FindControl("BT_modi");
            BT_modi.Visible = false;
            Button BT_save = (Button)gvr.FindControl("BT_save");
            BT_save.Visible = true;




        }
        protected void BT_saveClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
          
            DropDownList DL_storage_serial = (DropDownList)gvr.FindControl("DL_storage_serial");

            DropDownList DL_storage_code = (DropDownList)gvr.FindControl("DL_storage_code");

            DropDownList DL_TaskPeople = (DropDownList)gvr.FindControl("DL_TaskPeople");

            if (DL_storage_serial.SelectedValue == TB_storage_serial.Text)
            {
                SqlDataSource_Send.UpdateCommand = "UPDATE material_detail SET " +

                "storage_serial ='" + TB_storage_serial.Text +
                "',employee_id ='" + TextBoxName.Text +




                    "',time_stamp = getdate()" +
                     "  WHERE material_serial = '" + TB_material_serial.Text + "'";


                SqlDataSource_Send.Update();

            }
            else
            {
                SqlDataSource_Send.UpdateCommand = "UPDATE material_detail SET " +

               "storage_serial ='" + DL_storage_serial.SelectedValue +
               "',employee_id ='" + TextBoxName.Text +



               "',storage_modify_getdate = getdate()" +
               ",time_stamp = getdate()" +
                    "  WHERE material_serial = '" + TB_material_serial.Text + "'";


                SqlDataSource_Send.Update();


            }


          



            if (Textbox6.Text == "")
            {
                SqlDataSource_material_modify.SelectCommand = "SELECT   "
    + "dbo.material_master.material_code,"
    + "dbo.material_detail.material_serial,"
    + "dbo.material_master.material_type, "
    + "dbo.material_master.material_name, "
   
    + "dbo.material_master.material_ERP_no, "
    + "dbo.material_detail.material_asset_no, "
    + "dbo.material_detail.pr_no, "
    + "dbo.material_detail.po_no, "
    + "dbo.material_detail.iqc_no, "
    + "dbo.material_detail.device_serial_no, "
    + "material_detail.remark,"
    + "dbo.SystemList.SystemID,"
    + "dbo.material_detail.system_name,"
    + "dbo.material_detail.storage_serial,"
    + "storage.storage_code,"
    + "storage.storage_area,"
    + "storage.storage_id,"
    + "dbo.material_detail.employee_id, "
    + "dbo.employee.employee_name,"
    + "dbo.computer_detail.computer_serial, "
    + "dbo.computer_detail.product_name, "
    + "dbo.computer_detail.oem_os"
    + " from"
    + " dbo.material_master"
    + " left join"
    + " dbo.material_detail on material_master.material_code = material_detail.material_code"
    + " left join"
    + " dbo.employee on"
    + " material_detail.employee_id = dbo.employee.employee_id"
    + " left join"
    + " dbo.SystemList on"
    + " material_detail.system_name = SystemList.SystemName"
    + " inner join"
    + " (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area,"
    + " dbo.storage_detail.storage_id"
    + " from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code)"
    + " as storage on"
    + " material_detail.storage_serial = storage.storage_serial"
    + " left join"
    + " computer_detail on"
    + " material_detail.material_serial = computer_detail.material_serial"
    + " where"
    + "  (material_master.material_code like '%" + Textbox1.Text + "%'"

    + "  or material_ERP_no like '%" + Textbox1.Text + "%'"
    + "  or material_asset_no like '%" + Textbox1.Text + "%'"
    + "  or device_serial_no like '%" + Textbox1.Text + "%'"
    + "  or product_name like '%" + Textbox1.Text + "%')"
    + "  and"
    + "  (material_type like '%" + Textbox2.Text + "%'"
    + "  or material_name like '%" + Textbox2.Text + "%'"
    + "  or material_detail.remark like '%" + Textbox2.Text + "%')"
    + "  and"
    + "  (material_detail.storage_serial like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_area like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_id like  '%" + Textbox3.Text + "%')"
    + "  and"
    + "  (pr_no like '%" + Textbox4.Text + "%'"
    + "  or po_no like '%" + Textbox4.Text + "%'"
    + "  or iqc_no like '%" + Textbox4.Text + "%')"
    + "  and"
    + "  (system_name like '%" + Textbox5.Text + "%')";
            }
            else
            {
                SqlDataSource_material_modify.SelectCommand = "SELECT   "
   + "dbo.material_master.material_code,"
   + "dbo.material_detail.material_serial,"
   + "dbo.material_master.material_type, "
   + "dbo.material_master.material_name, "

   + "dbo.material_master.material_ERP_no, "
   + "dbo.material_detail.material_asset_no, "
   + "dbo.material_detail.pr_no, "
   + "dbo.material_detail.po_no, "
   + "dbo.material_detail.iqc_no, "
   + "dbo.material_detail.device_serial_no, "
   + "material_detail.remark,"
   + "dbo.SystemList.SystemID,"
   + "dbo.material_detail.system_name,"
   + "dbo.material_detail.storage_serial,"
   + "storage.storage_code,"
   + "storage.storage_area,"
   + "storage.storage_id,"
   + "dbo.material_detail.employee_id, "
   + "dbo.employee.employee_name,"
   + "dbo.computer_detail.computer_serial, "
   + "dbo.computer_detail.product_name, "
   + "dbo.computer_detail.oem_os"
   + " from"
   + " dbo.material_master"
   + " left join"
   + " dbo.material_detail on material_master.material_code = material_detail.material_code"
   + " left join"
   + " dbo.employee on"
   + " material_detail.employee_id = dbo.employee.employee_id"
   + " left join"
   + " dbo.SystemList on"
   + " material_detail.system_name = SystemList.SystemName"
   + " inner join"
   + " (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area,"
   + " dbo.storage_detail.storage_id"
   + " from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code)"
   + " as storage on"
   + " material_detail.storage_serial = storage.storage_serial"
   + " left join"
   + " computer_detail on"
   + " material_detail.material_serial = computer_detail.material_serial"
   + " where"
    + "  (material_master.material_code like '%" + Textbox1.Text + "%'"

    + "  or material_ERP_no like '%" + Textbox1.Text + "%'"
    + "  or material_asset_no like '%" + Textbox1.Text + "%'"
    + "  or device_serial_no like '%" + Textbox1.Text + "%'"
    + "  or product_name like '%" + Textbox1.Text + "%')"
    + "  and"
    + "  (material_type like '%" + Textbox2.Text + "%'"
    + "  or material_name like '%" + Textbox2.Text + "%'"
    + "  or material_detail.remark like '%" + Textbox2.Text + "%')"
    + "  and"
    + "  (material_detail.storage_serial like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_area like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_id like  '%" + Textbox3.Text + "%')"
    + "  and"
    + "  (pr_no like '%" + Textbox4.Text + "%'"
    + "  or po_no like '%" + Textbox4.Text + "%'"
    + "  or iqc_no like '%" + Textbox4.Text + "%')"
    + "  and"
    + "  (system_name like '%" + Textbox5.Text + "%')"
    + "  and"
   + "  (computer_serial like '%" + Textbox6.Text + "%'"
   + "  or product_name like '%" + Textbox6.Text + "%'"
   + "  or oem_os like '%" + Textbox6.Text + "%')";


            }




;





            LB_Message.Text = "修改完成，material_serial = " + TB_material_serial.Text;

            SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.TaskList" +
              "(IssueID," +
              "TaskDate," +
              "employee_id," +
              "SystemID," +
              "TaskDescription," +
              "TaskHours)" +
              "  VALUES (2683," +
              "convert(varchar(10),getdate(),121)," +
              " '" + TextBoxName1.Text + "'," +
              "27," +
              "'Y6P2資產異儲作業：進行異儲作業,物料代碼:" + gvr.Cells[0].Text.Replace("&nbsp;", "") + "，" +
              "物料序號:" + gvr.Cells[1].Text.Replace("&nbsp;", "") + "，" +
              "設備大項名稱:" + gvr.Cells[12].Text.Replace("&nbsp;", "") + "，" +
              "移動儲位區域至:" + DL_storage_code.SelectedItem.Text + "，" +
              "移動儲位編號至:" + DL_storage_serial.SelectedItem.Text + "，保管人為:" + DL_TaskPeople.SelectedValue + "(" + DL_TaskPeople.SelectedItem.Text + ")'" +
              ",0)";
            SqlDataSource_Send.Insert();

            SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.ModifyMaterialStorage_log" +
             "(material_code," +
             "material_serial," +
             "org_storage_serial," +
             "modify_storage_serial," +
             "org_storage_code," +
             "modify_storage_code," +
             "org_employee_name," +
             "modify_employee_name," +
             "modifytime)" +
             "  VALUES (" + gvr.Cells[0].Text.Replace("&nbsp;", "") + "," +
             "" + gvr.Cells[1].Text.Replace("&nbsp;", "") + "," +
             " " + gvr.Cells[9].Text.Replace("&nbsp;", "") + "," +
             "" + DL_storage_serial.SelectedValue + "," +
              "" + gvr.Cells[8].Text.Replace("&nbsp;", "") + "," +
               "" + DL_storage_code.SelectedValue + "," +
               "'" + gvr.Cells[6].Text.Replace("&nbsp;", "") + "'," +
               "'" + DL_TaskPeople.SelectedItem.Text + "'," +
             "getdate())";
            SqlDataSource_Send.Insert();

            Button BT_modi = (Button)gvr.FindControl("BT_modi");
            BT_modi.Visible = true;
            Button BT_save = (Button)gvr.FindControl("BT_save");
            BT_save.Visible = false;


   

            
            DL_storage_code.Visible = false;

          
            DL_TaskPeople.Visible = false;


            DL_storage_serial.Visible = false;

        }
            protected void DL_storage_code_SelectedIndexChanged(object sender, EventArgs e)

        {
            var ddl = sender as DropDownList;
            GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
            TB_storage_code.Text = ddl.SelectedValue;
           SqlDataSource_DL_storage_serial.SelectCommand = "SELECT storage_id, storage_serial FROM storage_detail where storage_code = '"+TB_storage_code.Text+ "' ";
            DropDownList DL_storage_serial = (DropDownList)gvr.FindControl("DL_storage_serial");
            DL_storage_serial.Visible = true;
           

        }
        
        protected void DL_TaskPeople_SelectedIndexChanged(object sender, EventArgs e)

        {
            var ddl = sender as DropDownList;
            TextBoxName.Text = ddl.SelectedValue;
        }

        protected void BT_Query_Click(object sender, EventArgs e)
        {
            if (Textbox6.Text == "")
            {
                SqlDataSource_material_modify.SelectCommand = "SELECT   "
    + "dbo.material_master.material_code,"
    + "dbo.material_detail.material_serial,"
    + "dbo.material_master.material_type, "
    + "dbo.material_master.material_name, "
 
    + "dbo.material_master.material_ERP_no, "
    + "dbo.material_detail.material_asset_no, "
    + "dbo.material_detail.pr_no, "
    + "dbo.material_detail.po_no, "
    + "dbo.material_detail.iqc_no, "
    + "dbo.material_detail.device_serial_no, "
    + "material_detail.remark,"
    + "dbo.SystemList.SystemID,"
    + "dbo.material_detail.system_name,"
    + "dbo.material_detail.storage_serial,"
    + "storage.storage_code,"
    + "storage.storage_area,"
    + "storage.storage_id,"
    + "dbo.material_detail.employee_id, "
    + "dbo.employee.employee_name,"
    + "dbo.computer_detail.computer_serial, "
    + "dbo.computer_detail.product_name, "
    + "dbo.computer_detail.oem_os"
    + " from"
    + " dbo.material_master"
    + " left join"
    + " dbo.material_detail on material_master.material_code = material_detail.material_code"
    + " left join"
    + " dbo.employee on"
    + " material_detail.employee_id = dbo.employee.employee_id"
    + " left join"
    + " dbo.SystemList on"
    + " material_detail.system_name = SystemList.SystemName"
    + " inner join"
    + " (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area,"
    + " dbo.storage_detail.storage_id"
    + " from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code)"
    + " as storage on"
    + " material_detail.storage_serial = storage.storage_serial"
    + " left join"
    + " computer_detail on"
    + " material_detail.material_serial = computer_detail.material_serial"
    + " where"
    + "  (material_master.material_code like '%" + Textbox1.Text + "%'"
 
    + "  or material_ERP_no like '%" + Textbox1.Text + "%'"
    + "  or material_asset_no like '%" + Textbox1.Text + "%'"
    + "  or device_serial_no like '%" + Textbox1.Text + "%'"
    + "  or product_name like '%" + Textbox1.Text + "%')"
    + "  and"
    + "  (material_type like '%" + Textbox2.Text + "%'"
    + "  or material_name like '%" + Textbox2.Text + "%'"
    + "  or material_detail.remark like '%" + Textbox2.Text + "%')"
    + "  and"
    + "  (material_detail.storage_serial like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_area like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_id like  '%" + Textbox3.Text + "%')"
    + "  and"
    + "  (pr_no like '%" + Textbox4.Text + "%'"
    + "  or po_no like '%" + Textbox4.Text + "%'"
    + "  or iqc_no like '%" + Textbox4.Text + "%')"
    + "  and"
    + "  (system_name like '%" + Textbox5.Text + "%')";
            }
            else
            {
                SqlDataSource_material_modify.SelectCommand = "SELECT   "
   + "dbo.material_master.material_code,"
   + "dbo.material_detail.material_serial,"
   + "dbo.material_master.material_type, "
   + "dbo.material_master.material_name, "

   + "dbo.material_master.material_ERP_no, "
   + "dbo.material_detail.material_asset_no, "
   + "dbo.material_detail.pr_no, "
   + "dbo.material_detail.po_no, "
   + "dbo.material_detail.iqc_no, "
   + "dbo.material_detail.device_serial_no, "
   + "material_detail.remark,"
   + "dbo.SystemList.SystemID,"
   + "dbo.material_detail.system_name,"
   + "dbo.material_detail.storage_serial,"
   + "storage.storage_code,"
   + "storage.storage_area,"
   + "storage.storage_id,"
   + "dbo.material_detail.employee_id, "
   + "dbo.employee.employee_name,"
   + "dbo.computer_detail.computer_serial, "
   + "dbo.computer_detail.product_name, "
   + "dbo.computer_detail.oem_os"
   + " from"
   + " dbo.material_master"
   + " left join"
   + " dbo.material_detail on material_master.material_code = material_detail.material_code"
   + " left join"
   + " dbo.employee on"
   + " material_detail.employee_id = dbo.employee.employee_id"
   + " left join"
   + " dbo.SystemList on"
   + " material_detail.system_name = SystemList.SystemName"
   + " inner join"
   + " (select dbo.storage_master.storage_code, dbo.storage_detail.storage_serial, dbo.storage_master.storage_area,"
   + " dbo.storage_detail.storage_id"
   + " from storage_master left join storage_detail on storage_master.storage_code= storage_detail.storage_code)"
   + " as storage on"
   + " material_detail.storage_serial = storage.storage_serial"
   + " left join"
   + " computer_detail on"
   + " material_detail.material_serial = computer_detail.material_serial"
   + " where"
    + "  (material_master.material_code like '%" + Textbox1.Text + "%'"
 
    + "  or material_ERP_no like '%" + Textbox1.Text + "%'"
    + "  or material_asset_no like '%" + Textbox1.Text + "%'"
    + "  or device_serial_no like '%" + Textbox1.Text + "%'"
    + "  or product_name like '%" + Textbox1.Text + "%')"
    + "  and"
    + "  (material_type like '%" + Textbox2.Text + "%'"
    + "  or material_name like '%" + Textbox2.Text + "%'"
    + "  or material_detail.remark like '%" + Textbox2.Text + "%')"
    + "  and"
    + "  (material_detail.storage_serial like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_area like '%" + Textbox3.Text + "%'"
    + "   or storage.storage_id like  '%" + Textbox3.Text + "%')"
    + "  and"
    + "  (pr_no like '%" + Textbox4.Text + "%'"
    + "  or po_no like '%" + Textbox4.Text + "%'"
    + "  or iqc_no like '%" + Textbox4.Text + "%')"
    + "  and"
    + "  (system_name like '%" + Textbox5.Text + "%')"
    + "  and"
   + "  (computer_serial like '%" + Textbox6.Text + "%'"
   + "  or product_name like '%" + Textbox6.Text + "%'"
   + "  or oem_os like '%" + Textbox6.Text + "%')";


            }


        }

       
    }
}