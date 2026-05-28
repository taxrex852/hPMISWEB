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
    public partial class CreateCharacterMaster : System.Web.UI.Page
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

                SqlDataSource_Send.InsertCommand = "INSERT INTO dbo.character_master" +
                    " (y6p2_device_code," +
                    "system_name," +
                    "iso_device_code," +
                    "iso_device_name," +
                    "isantivirus_install," +
                    "notinstall_antivirus_reason," +
                    "isusbport_enable," +
                    "usbport_enable_reason," +
                    "ip_address1," +
                    "ip_address2," +
                    "ip_address3," +
                    "ip_address4," +
                    "iserp_dtlist," +
           
                    "remark,time_stamp) " +
                     "VALUES( '"
                     + TB_y6p2_device_code.Text + "','" + DL_System.SelectedValue + "','" + TB_iso_decive_code.Text + "','" + TB_iso_device_name.Text + "'," +
                     "'" + DL_isantivirus_Install.SelectedValue + "','" + TB_notinstall_antivirus_reason.Text + "','" + DL_isusbport_enable.SelectedValue +
                    "', '" + TB_usbport_enable_reason.Text + "','" + TB_Ip_address1.Text + "','" + TB_Ip_address2.Text + "','" + TB_Ip_address3.Text +
                    "', '" + TB_Ip_address4.Text + "','" + DL_ISERP_DTList.SelectedValue + "'" +
                    ",'" + TB_remark.Text + "', getdate())";  
                SqlDataSource_Send.Insert();

                String character_serial = null;
                SqlDataSource_Send.SelectCommand = "SELECT top 1 character_serial FROM character_master ORDER BY character_serial DESC";
                SqlDataReader sqlDataReader = (SqlDataReader)SqlDataSource_Send.Select(new DataSourceSelectArguments());

                if (sqlDataReader.Read())//如果查詢的到，就取出IssueID
                {
                    character_serial = sqlDataReader[0].ToString();
                    LB_Message.Text = "新增資料完成，character_serial = " + character_serial;
                    SqlDataSource_character_master.SelectCommand = "SELECT *  FROM dbo.character_master where system_name = '"+DL_System.SelectedValue+"' order by character_master.character_serial desc";
                    GridView_character_master.DataBind();
                }


            }
            catch (Exception ex)
            {
                LB_Message.Text = "新增資料過程發生錯誤\n" + ex.Message;
            }

            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

        protected void BT_Modify_Click(object sender, EventArgs e)
        {

          

                SqlDataSource_Send.UpdateCommand = "UPDATE dbo.character_master SET" +
                    " y6p2_device_code ='" + TB_y6p2_device_code.Text + "'" +
                    ",system_name ='" + DL_System.SelectedValue + "'" +
                    ",iso_device_code ='" + TB_iso_decive_code.Text + "'" +
                    ",iso_device_name ='" + TB_iso_device_name.Text + "'" +
                    ",isantivirus_install ='" + DL_isantivirus_Install.SelectedValue + "'" +
                    ",notinstall_antivirus_reason ='" + TB_notinstall_antivirus_reason.Text + "'" +
                    ",isusbport_enable ='" + DL_isusbport_enable.SelectedValue + "'" +
                    ",usbport_enable_reason ='" + TB_usbport_enable_reason.Text + "'" +
                    ",ip_address1 ='" + TB_Ip_address1.Text + "'" +
                    ",ip_address2 ='" + TB_Ip_address2.Text + "'" +
                    ",ip_address3 ='" + TB_Ip_address3.Text + "'" +
                    ",ip_address4 ='" + TB_Ip_address4.Text + "'" +
                    ",iserp_dtlist ='" + DL_ISERP_DTList.SelectedValue + "'" +
  
                    ",remark ='" + TB_remark.Text + "'" +
                    ",time_stamp =getdate()" +
                    "WHERE character_serial = '" + TB_character_serial.Text + "' ";

                SqlDataSource_Send.Update();
                LB_Message.Text = "修改完成，Y6P2設備編碼 = " + TB_y6p2_device_code.Text;

            SqlDataSource_character_master.SelectCommand = "SELECT *  FROM dbo.character_master where system_name = '" + DL_System.SelectedValue + "' order by character_master.character_serial desc";
            GridView_character_master.DataBind();

            BT_Modify.Visible = false;
            BT_Send.Visible = true;
        }

      
        protected void BT_modiClick(object sender, System.EventArgs e)
        {

            TB_y6p2_device_code.Text = "";
            TB_character_serial.Text = "";
            TB_iso_decive_code.Text = "";
            TB_iso_device_name.Text = "";
            DL_System.SelectedValue = "";
            DL_isantivirus_Install.SelectedValue = "";
            DL_isusbport_enable.SelectedValue = "";
            DL_ISERP_DTList.SelectedValue = "";
            DL_isantivirus_Install.SelectedValue = "";
         
            TB_notinstall_antivirus_reason.Text = "";
            TB_usbport_enable_reason.Text = "";
            TB_Ip_address1.Text = "";
            TB_Ip_address2.Text = "";
            TB_Ip_address3.Text = "";
            TB_Ip_address4.Text = "";
            TB_remark.Text = "";
         

            Button btn = (Button)sender;


            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            TB_character_serial.Text = gvr.Cells[1].Text.Replace("&nbsp;", "");
            DL_System.SelectedValue = gvr.Cells[2].Text.Replace("&nbsp;", "");
            TB_y6p2_device_code.Text =  gvr.Cells[3].Text.Replace("&nbsp;", "");
            TB_iso_decive_code.Text =  gvr.Cells[4].Text.Replace("&nbsp;", "");
            TB_iso_device_name.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
            DL_isantivirus_Install.SelectedValue = gvr.Cells[6].Text.Replace("&nbsp;", "");
            TB_notinstall_antivirus_reason.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
            DL_isusbport_enable.SelectedValue = gvr.Cells[8].Text.Replace("&nbsp;", "");
            TB_usbport_enable_reason.Text = gvr.Cells[9].Text.Replace("&nbsp;", "");

            TB_Ip_address1.Text = gvr.Cells[10].Text.Replace("&nbsp;", "");
            TB_Ip_address2.Text = gvr.Cells[11].Text.Replace("&nbsp;", "");
            TB_Ip_address3.Text = gvr.Cells[12].Text.Replace("&nbsp;", "");
            TB_Ip_address4.Text = gvr.Cells[13].Text.Replace("&nbsp;", "");
            DL_ISERP_DTList.SelectedValue = gvr.Cells[14].Text.Replace("&nbsp;", "");
        


            TB_remark.Text =  gvr.Cells[15].Text.Replace("&nbsp;", "");





            BT_Modify.Visible = true;
            BT_Send.Visible = false;

        }

        protected void DL_System_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB_y6p2_device_code.Text = "";
            TB_character_serial.Text = "";
            TB_iso_decive_code.Text = "";
            TB_iso_device_name.Text = "";
         
            DL_isantivirus_Install.SelectedValue = "";
            DL_isusbport_enable.SelectedValue = "";
            DL_ISERP_DTList.SelectedValue = "";
            DL_isantivirus_Install.SelectedValue = "";

            TB_notinstall_antivirus_reason.Text = "";
            TB_usbport_enable_reason.Text = "";
            TB_Ip_address1.Text = "";
            TB_Ip_address2.Text = "";
            TB_Ip_address3.Text = "";
            TB_Ip_address4.Text = "";
            TB_remark.Text = "";
            SqlDataSource_character_master.SelectCommand = "SELECT *  FROM dbo.character_master where system_name = '" + DL_System.SelectedValue + "' order by character_master.character_serial desc";

        }
    }
}