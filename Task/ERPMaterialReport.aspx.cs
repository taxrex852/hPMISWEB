using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


namespace WebApplication2
{
    public partial class ERPMaterialReport : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Object.Equals(Session["ID"], null))
                {//判斷在Session["AdminName"]是否存在值
                    Response.Redirect("Login.aspx", true);

                }
                else {

                  
                };


           
                return;
            }
        }

       

        protected void BT_Print_Click(object sender, EventArgs e)
        {
          
                ReportDocument rd = new ReportDocument();
              
                string reportPath = Server.MapPath("CrystalReport2.rpt");

                SqlConnection Conn = new SqlConnection("Data Source=10.108.20.4;Password=Y6P2!@#;User ID=sa;Initial Catalog=Y6P2_Materials");
                Conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter(" select Mat_Master.material_name,Mat_Master.safety_quantity,Mat_Master.material_unit,Mat_Master.material_ERP_no,Sto_Info.storage_id, " +
                              " count(Mat_Master.material_name) As material_counter " +
                              " From [dbo].[material_master]  As Mat_Master  " +
                              " Left Join [dbo].[material_detail]  As Mat_Detail　On Mat_Detail.material_code = Mat_Master.material_code " +
                              " Left Join [dbo].[storage_detail]  As Sto_Info 　　On Mat_Detail.storage_serial  = Sto_info.storage_serial  " +
                              " where Mat_Detail.material_serial <> '' and  Mat_Master.material_ERP_no <> '' and Sto_info.storage_code = '2' " +
                              " Group by Mat_Master.material_name, Mat_Master.safety_quantity, Mat_Master.material_unit,Mat_Master.material_ERP_no, Sto_Info.storage_id " +
                              " order by Sto_Info.storage_id ", Conn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "ERP_Report_DataSet");

                rd.Load(reportPath);

                rd.SetDataSource(ds);


            rd.SetParameterValue("盤點報表", "Y6P2 ERP盤點報表");
            rd.SetParameterValue("排序方式", "(依儲位排序)");



            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, "ERP物料盤點報表");
               
                rd.Close();
                rd.Dispose();
                         
                Conn.Close();
                Conn.Dispose();

            //}
           
        }
    }
}