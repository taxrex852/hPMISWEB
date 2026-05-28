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
    public partial class CharacterMatch : System.Web.UI.Page
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
                if (DL_Character_master.SelectedValue !="" && DL_storage_code.SelectedValue !=""&& DL_storage_serial.SelectedValue!=""&& DL_area.SelectedValue!="") {
                    SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.character_match"
               + "(material_serial"

               + ", character_serial"
               + ", remark"
               + ", time_stamp)"
                + "VALUES"
                + "(" + DL_area.SelectedValue + ""

                + ",'" + DL_Character_master.SelectedValue + "'"
                + ",'" + TB_remark.Text + "'"
                + ",getdate() )";

                    SqlDataSource_Send.Insert();


                    LB_Message.Text = "新增資料完成";

                }
                else {

                    LB_Message.Text = "新增資資料失敗，角色配對或電腦配對不能為空白";

                }

            } 
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }
            BT_Modify.Visible = false;
            BT_Send.Visible = true;

            SqlDataSource_computer_character_match.SelectCommand = "SELECT character_match_serial,character_match.material_serial,character_match.character_serial,character_master.y6p2_device_code,character_master.iso_device_name,character_master.system_name,material.material_name,material.storage_area,material.storage_id,material.storage_serial,material.storage_code,character_match.remark,character_match.time_stamp FROM  dbo.character_match left join character_master on character_match.character_serial=character_master.character_serial left join (select material.material_serial,material.storage_serial,storage.storage_code,material.material_name,storage.storage_area,storage.storage_id from (select material_detail.material_serial,material_detail.storage_serial,material_master.material_name from material_master left join material_detail on material_master.material_code=material_detail.material_code) as material left join (select dbo.storage_master.storage_code,dbo.storage_detail.storage_serial,dbo.storage_master.storage_area,dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material.storage_serial = storage.storage_serial) as material on character_match.material_serial=material.material_serial  where system_name = '" + DL_System.SelectedValue + "'";
            GridView1.DataBind();
            DL_area.Items.Clear();
            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                   "dbo.material_master.material_code," +
                   "dbo.material_detail.material_serial," +
                   "dbo.material_detail.storage_serial," +
                   "storage.storage_code," +
                   "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                   "dbo.material_detail.system_name " +
                   "from  dbo.material_master" +
                   " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                   " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                   " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                   "inner join" +
                   " (select dbo.storage_master.storage_code," +
                   " dbo.storage_detail.storage_serial, " +
                   "dbo.storage_master.storage_area, " +
                   " dbo.storage_detail.storage_id " +
                   " from storage_master " +
                   "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                   " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";


        }

        protected void BT_modiClick(object sender, System.EventArgs e)
        {
       
            Button btn = (Button)sender;

    
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TB_character_match_serial.Text= gvr.Cells[1].Text.Replace("&nbsp;", "");
            DL_System.SelectedValue= gvr.Cells[6].Text.Replace("&nbsp;", "");
     
            TB_computer_serial.Text = "設備大項名稱：" + gvr.Cells[7].Text.Replace("&nbsp;", "") + "。使用系統：" + gvr.Cells[6].Text.Replace("&nbsp;", "") + "。儲位：" + gvr.Cells[8].Text.Replace("&nbsp;", "") + "/" + gvr.Cells[9].Text.Replace("&nbsp;", "");
            TB_character_serial.Text = "Y6P2設備編碼：" + gvr.Cells[4].Text.Replace("&nbsp;", "") + "。角色設備名稱：" + gvr.Cells[5].Text.Replace("&nbsp;", "") ;

            TB_area_temp.Text = "儲位：" + gvr.Cells[8].Text.Replace("&nbsp;", "") + "/" + gvr.Cells[9].Text.Replace("&nbsp;", "") + "。設備大項名稱：" + gvr.Cells[7].Text.Replace("&nbsp;", "");
            DL_storage_code.Items.Clear();
         
            DL_storage_serial.Items.Clear();
            DL_storage_serial.Items.Add(new ListItem("", ""));
            DL_storage_serial.SelectedValue = "";
            DL_storage_code.Items.Add(new ListItem("", ""));
            DL_storage_code.SelectedValue = "";


            SqlDataSource_DL_storage_code.SelectCommand = "SELECT " +
                "storage.storage_code," +
                "storage.storage_area" +
                " from  dbo.material_master " +
                "left join " +
                "dbo.material_detail " +
                "on material_master.material_code=material_detail.material_code " +
                "inner join" +
                " (select dbo.storage_master.storage_code," +
                " dbo.storage_detail.storage_serial, " +
                "dbo.storage_master.storage_area," +
                " dbo.storage_detail.storage_id " +
                "from storage_master " +
                "left join storage_detail on storage_master.storage_code=storage_detail.storage_code)  " +
                "as storage on material_detail.storage_serial=storage.storage_serial " +
                "where  material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_code,storage.storage_area";

            SqlDataSource_DL_storage_serial.SelectCommand = " SELECT  " +
                  "storage.storage_serial," +
                  "storage.storage_id " +
                  "from dbo.material_master " +
                  "left join dbo.material_detail " +
                  "on material_master.material_code=material_detail.material_code " +
                  "inner join " +
                  "(select dbo.storage_master.storage_code," +
                  " dbo.storage_detail.storage_serial, " +
                  "dbo.storage_master.storage_area," +
                  "  dbo.storage_detail.storage_id " +
                  "from storage_master " +
                  "left join storage_detail " +
                  "on storage_master.storage_code=storage_detail.storage_code) as storage " +
                  "on material_detail.storage_serial=storage.storage_serial" +
                  " where   storage.storage_code = '" + DL_storage_code.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_serial,storage.storage_id";


            DL_area.Items.Clear();
            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                   "dbo.material_master.material_code," +
                   "dbo.material_detail.material_serial," +
                   "dbo.material_detail.storage_serial," +
                   "storage.storage_code," +
                   "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                   "dbo.material_detail.system_name " +
                   "from  dbo.material_master" +
                   " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                   " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                   " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                   "inner join" +
                   " (select dbo.storage_master.storage_code," +
                   " dbo.storage_detail.storage_serial, " +
                   "dbo.storage_master.storage_area, " +
                   " dbo.storage_detail.storage_id " +
                   " from storage_master " +
                   "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                   " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";





         
            TB_remark.Text = gvr.Cells[12].Text.Replace("&nbsp;", "");
         

            BT_Modify.Visible = true;
            BT_Send.Visible = false;


           
        }

        protected void BT_delClick(object sender, System.EventArgs e)
        {

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
          
            TB_character_match_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            SqlDataSource_Send.DeleteCommand = "Delete from dbo.character_match"

         + " WHERE character_match_serial = '" + TB_character_match_serial.Text + "'";
            LB_Message.Text = "刪除資料完成";

            SqlDataSource_Send.Delete();
            SqlDataSource_computer_character_match.SelectCommand = "SELECT character_match_serial,character_match.material_serial,character_match.character_serial,character_master.y6p2_device_code,character_master.iso_device_name,character_master.system_name,material.material_name,material.storage_area,material.storage_id,material.storage_serial,material.storage_code,character_match.remark,character_match.time_stamp FROM  dbo.character_match left join character_master on character_match.character_serial=character_master.character_serial left join (select material.material_serial,material.storage_serial,storage.storage_code,material.material_name,storage.storage_area,storage.storage_id from (select material_detail.material_serial,material_detail.storage_serial,material_master.material_name from material_master left join material_detail on material_master.material_code=material_detail.material_code) as material left join (select dbo.storage_master.storage_code,dbo.storage_detail.storage_serial,dbo.storage_master.storage_area,dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material.storage_serial = storage.storage_serial) as material on character_match.material_serial=material.material_serial  where system_name = '" + DL_System.SelectedValue + "'";
            GridView1.DataBind();
            DL_area.Items.Clear();
            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                   "dbo.material_master.material_code," +
                   "dbo.material_detail.material_serial," +
                   "dbo.material_detail.storage_serial," +
                   "storage.storage_code," +
                   "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                   "dbo.material_detail.system_name " +
                   "from  dbo.material_master" +
                   " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                   " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                   " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                   "inner join" +
                   " (select dbo.storage_master.storage_code," +
                   " dbo.storage_detail.storage_serial, " +
                   "dbo.storage_master.storage_area, " +
                   " dbo.storage_detail.storage_id " +
                   " from storage_master " +
                   "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                   " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";





            BT_Modify.Visible = false;
            BT_Send.Visible = true;

        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

            SqlDataSource_Send.UpdateCommand = "UPDATE dbo.character_match"

   + " Set material_serial = '" + DL_area.SelectedValue + "'"

   + ", character_serial ='" + DL_Character_master.SelectedValue + "'"
   + ", remark ='" + TB_remark.Text + "'"

   + ", time_stamp =getdate()"
   + " WHERE character_match_serial = '" + TB_character_match_serial.Text + "'";


            SqlDataSource_Send.Update();
            LB_Message.Text = "修改完成，character_match_serial = " + TB_character_match_serial.Text;
            SqlDataSource_computer_character_match.SelectCommand = "SELECT character_match_serial,character_match.material_serial,character_match.character_serial,character_master.y6p2_device_code,character_master.iso_device_name,character_master.system_name,material.material_name,material.storage_area,material.storage_id,material.storage_serial,material.storage_code,character_match.remark,character_match.time_stamp FROM  dbo.character_match left join character_master on character_match.character_serial=character_master.character_serial left join (select material.material_serial,material.storage_serial,storage.storage_code,material.material_name,storage.storage_area,storage.storage_id from (select material_detail.material_serial,material_detail.storage_serial,material_master.material_name from material_master left join material_detail on material_master.material_code=material_detail.material_code) as material left join (select dbo.storage_master.storage_code,dbo.storage_detail.storage_serial,dbo.storage_master.storage_area,dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material.storage_serial = storage.storage_serial) as material on character_match.material_serial=material.material_serial  where system_name = '" + DL_System.SelectedValue + "'";
            GridView1.DataBind();
            DL_area.Items.Clear();
            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                   "dbo.material_master.material_code," +
                   "dbo.material_detail.material_serial," +
                   "dbo.material_detail.storage_serial," +
                   "storage.storage_code," +
                   "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                   "dbo.material_detail.system_name " +
                   "from  dbo.material_master" +
                   " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                   " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                   " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                   "inner join" +
                   " (select dbo.storage_master.storage_code," +
                   " dbo.storage_detail.storage_serial, " +
                   "dbo.storage_master.storage_area, " +
                   " dbo.storage_detail.storage_id " +
                   " from storage_master " +
                   "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                   " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";


            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void DL_storage_code_SelectedIndexChanged(object sender, EventArgs e)
        {

            DL_area.Items.Clear();
            DL_storage_serial.Items.Clear();
            DL_storage_serial.Items.Add(new ListItem("", ""));
            DL_storage_serial.SelectedValue = "";
            SqlDataSource_DL_storage_serial.SelectCommand = " SELECT  " +
                "storage.storage_serial," +
                "storage.storage_id " +
                "from dbo.material_master " +
                "left join dbo.material_detail " +
                "on material_master.material_code=material_detail.material_code " +
                "inner join " +
                "(select dbo.storage_master.storage_code," +
                " dbo.storage_detail.storage_serial, " +
                "dbo.storage_master.storage_area," +
                "  dbo.storage_detail.storage_id " +
                "from storage_master " +
                "left join storage_detail " +
                "on storage_master.storage_code=storage_detail.storage_code) as storage " +
                "on material_detail.storage_serial=storage.storage_serial" +
                " where   storage.storage_code = '" + DL_storage_code.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_serial,storage.storage_id";



        }


        protected void DL_storage_serial_TextChanged(object sender, EventArgs e)
        {
            DL_area.Items.Clear();

            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                "dbo.material_master.material_code," +
                "dbo.material_detail.material_serial," +
                "dbo.material_detail.storage_serial," +
                "storage.storage_code," +
                "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                "dbo.material_detail.system_name " +
                "from  dbo.material_master" +
                " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                "inner join" +
                " (select dbo.storage_master.storage_code," +
                " dbo.storage_detail.storage_serial, " +
                "dbo.storage_master.storage_area, " +
                " dbo.storage_detail.storage_id " +
                " from storage_master " +
                "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";

        }

        protected void DL_System_SelectedIndexChanged(object sender, EventArgs e)
        {
            DL_storage_code.Items.Clear();
            DL_storage_code.Items.Add(new ListItem("", ""));
            DL_storage_code.SelectedValue = "";

            DL_area.Items.Clear();

            DL_storage_serial.Items.Clear();
            DL_storage_serial.Items.Add(new ListItem("", ""));
            DL_storage_serial.SelectedValue = "";

            DL_Character_master.Items.Clear();
            DL_Character_master.Items.Add(new ListItem("", ""));
            DL_Character_master.SelectedValue = "";

            SqlDataSource_DL_storage_code.SelectCommand = "SELECT " +
                "storage.storage_code," +
                "storage.storage_area" +
                " from  dbo.material_master " +
                "left join " +
                "dbo.material_detail " +
                "on material_master.material_code=material_detail.material_code " +
                "inner join" +
                " (select dbo.storage_master.storage_code," +
                " dbo.storage_detail.storage_serial, " +
                "dbo.storage_master.storage_area," +
                " dbo.storage_detail.storage_id " +
                "from storage_master " +
                "left join storage_detail on storage_master.storage_code=storage_detail.storage_code)  " +
                "as storage on material_detail.storage_serial=storage.storage_serial " +
                "where  material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_code,storage.storage_area";

            SqlDataSource_DL_storage_serial.SelectCommand = " SELECT  " +
                  "storage.storage_serial," +
                  "storage.storage_id " +
                  "from dbo.material_master " +
                  "left join dbo.material_detail " +
                  "on material_master.material_code=material_detail.material_code " +
                  "inner join " +
                  "(select dbo.storage_master.storage_code," +
                  " dbo.storage_detail.storage_serial, " +
                  "dbo.storage_master.storage_area," +
                  "  dbo.storage_detail.storage_id " +
                  "from storage_master " +
                  "left join storage_detail " +
                  "on storage_master.storage_code=storage_detail.storage_code) as storage " +
                  "on material_detail.storage_serial=storage.storage_serial" +
                  " where   storage.storage_code = '" + DL_storage_code.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match) group by storage.storage_serial,storage.storage_id";



            SqlDataSource_DL_area.SelectCommand = " SELECT " +
                   "dbo.material_master.material_code," +
                   "dbo.material_detail.material_serial," +
                   "dbo.material_detail.storage_serial," +
                   "storage.storage_code," +
                   "'物料序號：'+cast(dbo.material_detail.material_serial as varchar)+'。儲位：'+storage.storage_area+'/'+storage.storage_id+'。設備名稱：'+dbo.material_master.material_name +'。設備SN碼：'+dbo.material_detail.device_serial_no as area," +
                   "dbo.material_detail.system_name " +
                   "from  dbo.material_master" +
                   " left join dbo.material_detail on material_master.material_code=material_detail.material_code" +
                   " left join  dbo.employee on material_detail.employee_id=dbo.employee.employee_id" +
                   " left join dbo.SystemList on material_detail.system_name=SystemList.SystemName " +
                   "inner join" +
                   " (select dbo.storage_master.storage_code," +
                   " dbo.storage_detail.storage_serial, " +
                   "dbo.storage_master.storage_area, " +
                   " dbo.storage_detail.storage_id " +
                   " from storage_master " +
                   "left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material_detail.storage_serial=storage.storage_serial" +
                   " where  storage.storage_serial = '" + DL_storage_serial.SelectedValue + "' and material_detail.material_serial not in (select material_serial from character_match)";
            TB_character_serial.Text = "";
                TB_computer_serial.Text = "";
            TB_area_temp.Text = "";

            SqlDataSource_computer_character_match.SelectCommand = "SELECT character_match_serial,character_match.material_serial,character_match.character_serial,character_master.y6p2_device_code,character_master.iso_device_name,character_master.system_name,material.material_name,material.storage_area,material.storage_id,material.storage_serial,material.storage_code,character_match.remark,character_match.time_stamp FROM  dbo.character_match left join character_master on character_match.character_serial=character_master.character_serial left join (select material.material_serial,material.storage_serial,storage.storage_code,material.material_name,storage.storage_area,storage.storage_id from (select material_detail.material_serial,material_detail.storage_serial,material_master.material_name from material_master left join material_detail on material_master.material_code=material_detail.material_code) as material left join (select dbo.storage_master.storage_code,dbo.storage_detail.storage_serial,dbo.storage_master.storage_area,dbo.storage_detail.storage_id from storage_master left join storage_detail on storage_master.storage_code=storage_detail.storage_code) as storage on material.storage_serial = storage.storage_serial) as material on character_match.material_serial=material.material_serial  where system_name = '" + DL_System.SelectedValue+"'";

        }
    }
}