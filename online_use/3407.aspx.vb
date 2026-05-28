Imports System.Data.SqlClient
Imports System.Collections.Generic


















Partial Public Class _4TNRL_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3407"
    Private Conn As SqlConnection

    Private Const EXLC_C As Integer = 100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            setTitle(Me, PAGE_ID)


            LabelStartdate.Text = Date.Today.AddMonths(-11).ToString("yyyy/MM")
            LabelEnddate.Text = Date.Today.ToString("yyyy/MM")

            Mainprocess()
        End If
    End Sub




    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))

        TNRL_Table1()
        TNRL_Table2()

        BuildChartData()
    End Sub







    Private Sub BuildChartData()


        Dim sql As String =
            "select " &
            "ETNG.process_date, " &
            "isnull(ETNG.total_prod,0) as ETNG, " &
            "isnull(WTNG.total_prod,0) as WTNG, " &
            "isnull(NTNG.total_prod,0) as NTNG, " &
            "isnull(NTCG.total_prod,0) as NTCG, " &
            "isnull(ETCG.total_prod,0) as ETCG, " &
            "round(isnull(PA.total_prod-ETNG.total_prod-WTNG.total_prod-NTNG.total_prod-NTCG.total_prod-ETCG.total_prod,0),2) as MDSZ, " &
            "isnull(NRWD.total_prod,0) as NRWD, " &
            "isnull(MDWD.total_prod,0) as MDWD, " &
            "isnull(WIWD.total_prod,0) as WIWD, " &
            "isnull(EXLC.total_prod,0) as EXLC, " &
            "isnull(LSCS.total_prod,0) as LSCS, " &
            "isnull(MSCS.total_prod,0) as MSCS, " &
            "isnull(HICS.total_prod,0) as HICS, " &
            "isnull(VHIS.total_prod,0) as VHIS, " &
            "isnull(SUS.total_prod,0) as SUS, " &
            "isnull(NRCQ.total_prod,0) as NRCQ, " &
            "isnull(HICQ.total_prod,0) as HICQ, " &
            "isnull(VHCQ.total_prod,0) as VHCQ " &
            "from( " &
            "select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width <= 1260 and avg_thickness <= 1500 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width <= 1260 and coil_thickness <= 1500 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as ETNG " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width >= 1500 and avg_thickness <= 2300 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width >= 1500 and coil_thickness <= 2300 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as WTNG on ETNG.process_date=WTNG.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width > 1260 and avg_width < 1500 and avg_thickness >= 1500 and avg_thickness <= 1900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width > 1260 and coil_width < 1500 and coil_thickness >= 1500 and coil_thickness <= 1900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as NTNG on ETNG.process_date=NTNG.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_thickness >= 6000 and avg_thickness <= 9900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_thickness >= 6000 and coil_thickness <= 9900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as NTCG on ETNG.process_date=NTCG.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_thickness > 9900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_thickness > 9900 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as ETCG on ETNG.process_date=ETCG.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as PA on ETNG.process_date=PA.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width < 950 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width < 950 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as NRWD on ETNG.process_date=NRWD.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width >= 950 and avg_width < 1550 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width >= 950 and coil_width < 1550 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as MDWD on ETNG.process_date=MDWD.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and avg_width >= 1550 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,process_date),0) as process_date, " &
            "cast(round(SUM(gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 with(nolock) " &
            "where process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and coil_width >= 1550 " &
            "group by dateadd(m,datediff(m,0,process_date),0)) as B ON A.process_date=B.process_date) as WIWD on ETNG.process_date=WIWD.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon <= " & EXLC_C & " " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon <= " & EXLC_C & " " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as EXLC on ETNG.process_date=EXLC.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 40 " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 40 " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as LSCS on ETNG.process_date=LSCS.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 50 and wh71.tensile > 40 " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 50 and wh71.tensile > 40 " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as MSCS on ETNG.process_date=MSCS.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 60 and wh71.tensile > 50 " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile <= 60 and wh71.tensile > 50 " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as HICS on ETNG.process_date=HICS.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile > 60 " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.tensile > 60 " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as VHIS on ETNG.process_date=VHIS.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.steel_grade_code like '6%' " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.carbon > " & EXLC_C & " and wh71.steel_grade_code like '6%' " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as SUS on ETNG.process_date=SUS.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.inspection_code < '6000' and wh71.inspection_code >= '5000' " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.inspection_code < '6000' and wh71.inspection_code >= '5000' " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as NRCQ on ETNG.process_date=NRCQ.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.inspection_code < '5000' and wh71.inspection_code >= '4000' " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.inspection_code < '5000' and wh71.inspection_code >= '4000' " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as HICQ on ETNG.process_date=HICQ.process_date " &
            "left join (select dateadd(m,datediff(m,0,A.process_date),0) as process_date, " &
            "round(ISNULL(A.product_weight,0)+ISNULL(B.product_weight,0),2) as total_prod " &
            "from (select dateadd(m,datediff(m,0,wh73.process_date),0) as process_date, " &
            "cast(round(SUM(g_weight)/1000,2) as float) as product_weight from h_pmis_wh73 as wh73 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh73.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and SUBSTRING(wh73.product_no,1,7)=wh71.coil_no and wh71.inspection_code < '4000' and wh71.inspection_code >= '2000' " &
            "group by dateadd(m,datediff(m,0,wh73.process_date),0)) as A " &
            "FULL OUTER JOIN (select dateadd(m,datediff(m,0,wh76.process_date),0) as process_date, " &
            "cast(round(SUM(wh76.gross_weight)/1000,2) as float) as product_weight from h_pmis_wh76 as wh76 with(nolock),h_pmis_wh71 as wh71 with(nolock) " &
            "where wh76.process_date between DATEADD(year,-1,getdate()) and getdate() " &
            "and wh76.coil_no=wh71.coil_no and wh71.inspection_code < '4000' and wh71.inspection_code >= '2000' " &
            "group by dateadd(m,datediff(m,0,wh76.process_date),0)) as B ON A.process_date=B.process_date) as VHCQ on ETNG.process_date=VHCQ.process_date " &
            "order by ETNG.process_date"

        Conn.Open()
        Dim dt As DataTable = execQuery(sql, "", Conn)
        Conn.Close()

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return


        Dim xAxis As New List(Of String)()
        Dim etng As New List(Of String)()
        Dim wtng As New List(Of String)()
        Dim ntng As New List(Of String)()
        Dim ntcg As New List(Of String)()
        Dim etcg As New List(Of String)()
        Dim mdsz As New List(Of String)()
        Dim nrwd As New List(Of String)()
        Dim mdwd As New List(Of String)()
        Dim wiwd As New List(Of String)()
        Dim exlc As New List(Of String)()
        Dim lscs As New List(Of String)()
        Dim mscs As New List(Of String)()
        Dim hics As New List(Of String)()
        Dim vhis As New List(Of String)()
        Dim sus  As New List(Of String)()
        Dim nrcq As New List(Of String)()
        Dim hicq As New List(Of String)()
        Dim vhcq As New List(Of String)()


        If dt.Rows.Count > 0 Then
            LabelStartdate.Text = Convert.ToDateTime(dt.Rows(0)("process_date")).ToString("yyyy/MM")
            LabelEnddate.Text   = Convert.ToDateTime(dt.Rows(dt.Rows.Count - 1)("process_date")).ToString("yyyy/MM")
        End If


        For i As Integer = 0 To dt.Rows.Count - 1
            xAxis.Add("'" & Convert.ToDateTime(dt.Rows(i)("process_date")).ToString("yyyy/MM") & "'")
            etng.Add(If(IsDBNull(dt.Rows(i)("ETNG")), "0", Convert.ToDouble(dt.Rows(i)("ETNG")).ToString("0.00")))
            wtng.Add(If(IsDBNull(dt.Rows(i)("WTNG")), "0", Convert.ToDouble(dt.Rows(i)("WTNG")).ToString("0.00")))
            ntng.Add(If(IsDBNull(dt.Rows(i)("NTNG")), "0", Convert.ToDouble(dt.Rows(i)("NTNG")).ToString("0.00")))
            ntcg.Add(If(IsDBNull(dt.Rows(i)("NTCG")), "0", Convert.ToDouble(dt.Rows(i)("NTCG")).ToString("0.00")))
            etcg.Add(If(IsDBNull(dt.Rows(i)("ETCG")), "0", Convert.ToDouble(dt.Rows(i)("ETCG")).ToString("0.00")))
            mdsz.Add(If(IsDBNull(dt.Rows(i)("MDSZ")), "0", Convert.ToDouble(dt.Rows(i)("MDSZ")).ToString("0.00")))
            nrwd.Add(If(IsDBNull(dt.Rows(i)("NRWD")), "0", Convert.ToDouble(dt.Rows(i)("NRWD")).ToString("0.00")))
            mdwd.Add(If(IsDBNull(dt.Rows(i)("MDWD")), "0", Convert.ToDouble(dt.Rows(i)("MDWD")).ToString("0.00")))
            wiwd.Add(If(IsDBNull(dt.Rows(i)("WIWD")), "0", Convert.ToDouble(dt.Rows(i)("WIWD")).ToString("0.00")))
            exlc.Add(If(IsDBNull(dt.Rows(i)("EXLC")), "0", Convert.ToDouble(dt.Rows(i)("EXLC")).ToString("0.00")))
            lscs.Add(If(IsDBNull(dt.Rows(i)("LSCS")), "0", Convert.ToDouble(dt.Rows(i)("LSCS")).ToString("0.00")))
            mscs.Add(If(IsDBNull(dt.Rows(i)("MSCS")), "0", Convert.ToDouble(dt.Rows(i)("MSCS")).ToString("0.00")))
            hics.Add(If(IsDBNull(dt.Rows(i)("HICS")), "0", Convert.ToDouble(dt.Rows(i)("HICS")).ToString("0.00")))
            vhis.Add(If(IsDBNull(dt.Rows(i)("VHIS")), "0", Convert.ToDouble(dt.Rows(i)("VHIS")).ToString("0.00")))
            sus.Add( If(IsDBNull(dt.Rows(i)("SUS")),  "0", Convert.ToDouble(dt.Rows(i)("SUS")).ToString("0.00")))
            nrcq.Add(If(IsDBNull(dt.Rows(i)("NRCQ")), "0", Convert.ToDouble(dt.Rows(i)("NRCQ")).ToString("0.00")))
            hicq.Add(If(IsDBNull(dt.Rows(i)("HICQ")), "0", Convert.ToDouble(dt.Rows(i)("HICQ")).ToString("0.00")))
            vhcq.Add(If(IsDBNull(dt.Rows(i)("VHCQ")), "0", Convert.ToDouble(dt.Rows(i)("VHCQ")).ToString("0.00")))
        Next


        Dim script As String =
            "var chartData = {" &
            "xAxis:[" & String.Join(",", xAxis) & "]," &
            "etng:[" & String.Join(",", etng) & "]," &
            "wtng:[" & String.Join(",", wtng) & "]," &
            "ntng:[" & String.Join(",", ntng) & "]," &
            "ntcg:[" & String.Join(",", ntcg) & "]," &
            "etcg:[" & String.Join(",", etcg) & "]," &
            "mdsz:[" & String.Join(",", mdsz) & "]," &
            "nrwd:[" & String.Join(",", nrwd) & "]," &
            "mdwd:[" & String.Join(",", mdwd) & "]," &
            "wiwd:[" & String.Join(",", wiwd) & "]," &
            "exlc:[" & String.Join(",", exlc) & "]," &
            "lscs:[" & String.Join(",", lscs) & "]," &
            "mscs:[" & String.Join(",", mscs) & "]," &
            "hics:[" & String.Join(",", hics) & "]," &
            "vhis:[" & String.Join(",", vhis) & "]," &
            "sus:["  & String.Join(",", sus)  & "]," &
            "nrcq:[" & String.Join(",", nrcq) & "]," &
            "hicq:[" & String.Join(",", hicq) & "]," &
            "vhcq:[" & String.Join(",", vhcq) & "]" &
            "};"

        ClientScript.RegisterStartupScript(Me.GetType(), "EChartsData", script, True)
    End Sub









    Private Sub TNRL_Table1()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "ETNG", "WTNG", "NTNG", "NTCG", "ETCG", "MDSZ"}
        Dim strMonthTitle1() As String = {"NRWD", "MDWD", "WIWD"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next
        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next


        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Today), Month(Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") & "月" & (i + 1).ToString("d2") & "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next
        For i As Integer = 0 To daysInMonth - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth1.Text = Date.Today.ToString("MM")
        Conn.Open()
        Dim strACCESS As String


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width <= 1260 and avg_thickness <= 1500 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width <= 1260 and coil_thickness <= 1500 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1

            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETNG.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width >= 1500 and avg_thickness <= 2300 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width >= 1500 and coil_thickness <= 2300 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWTNG.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width > 1260 and avg_width < 1500 and avg_thickness >= 1500 and avg_thickness <= 1900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width > 1260 and coil_width < 1500 and coil_thickness >= 1500 and coil_thickness <= 1900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTNG.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_thickness >= 6000 and avg_thickness <= 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_thickness >= 6000 and coil_thickness <= 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNTCG.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_thickness > 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_thickness > 9900 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETCG.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        If dtTmp IsNot Nothing AndAlso dtTmp.Rows.Count > 0 Then
            calTmp = 0
            For i As Integer = 0 To dtTmp.Rows.Count - 1
                Dim dayIdx As Integer = CInt(dtTmp.Rows(i).Item(0)) - 1
                With dtDataTable.Rows(dayIdx)
                    Dim paVal As Double = Val(dtTmp.Rows(i).Item(1)) / 1000

                    Dim mdszVal As Double = paVal - Val(.Item(1)) - Val(.Item(2)) - Val(.Item(3)) - Val(.Item(4)) - Val(.Item(5))
                    .Item(6) = IIf(mdszVal < 0, "0.00", mdszVal.ToString("0.00"))
                    calTmp += Val(.Item(6))
                End With
            Next
        End If
        lblMDSZ.Text = calTmp.ToString("0.00")

        gvMonth1.DataSource = dtDataTable
        gvMonth1.DataBind()
        gvMonth1.HeaderRow.Visible = False


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width <= 950 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width <= 950 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRWD.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width > 950 and avg_width < 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width > 950 and coil_width < 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMDWD.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(g_weight) as product_weight from h_pmis_wh73 " &
            "where shift_date like '{0}%' and avg_width >= 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(shift_date, 7, 2) as product_day, SUM(gross_weight) as product_weight from h_pmis_wh76 " &
            "where shift_date like '{0}%' and coil_width >= 1550 " &
            "Group by SUBSTRING(shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblWIWD.Text = calTmp.ToString("0.00")

        gvMonth3.DataSource = dtdatatable1
        gvMonth3.DataBind()
        gvMonth3.HeaderRow.Visible = False
        Conn.Close()
    End Sub








    Private Sub TNRL_Table2()
        Dim dtDataTable As New DataTable
        Dim dtdatatable1 As New DataTable
        Dim dtTmp As DataTable = Nothing
        Dim dr As DataRow
        Dim strMonthTitle() As String = {"dimension", "EXLC", "LSCS", "MSCS", "HICS", "VHIS", "SUS"}
        Dim strMonthTitle1() As String = {"NRCQ", "HICQ", "VHCQ"}
        Dim tmpValue As Double = 0
        Dim calTmp As Double

        For i As Integer = 0 To strMonthTitle.Length - 1
            dtDataTable.Columns.Add(New DataColumn(strMonthTitle(i)))
        Next
        For i As Integer = 0 To strMonthTitle1.Length - 1
            dtdatatable1.Columns.Add(New DataColumn(strMonthTitle1(i)))
        Next

        Dim daysInMonth As Integer = Date.DaysInMonth(Year(Today), Month(Today))
        For i As Integer = 0 To daysInMonth - 1
            dr = dtDataTable.NewRow
            dtDataTable.Rows.Add(dr)
            dr(0) = Date.Today.ToString("MM") & "月" & (i + 1).ToString("d2") & "日"
            For j As Integer = 1 To strMonthTitle.Length - 1
                dtDataTable.Rows(i).Item(j) = "0.00"
            Next
        Next
        For i As Integer = 0 To daysInMonth - 1
            dr = dtdatatable1.NewRow
            dtdatatable1.Rows.Add(dr)
            For j As Integer = 0 To strMonthTitle1.Length - 1
                dtdatatable1.Rows(i).Item(j) = "0.00"
            Next
        Next

        lblMonth2.Text = Date.Today.ToString("MM")
        Conn.Open()
        Dim strACCESS As String


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon <= " & EXLC_C.ToString() &
            " Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon <= " & EXLC_C.ToString() &
            " Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblEXLC.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile <= 40 " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile <= 40 " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblLSCS.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 40 and wh71.tensile <= 50 " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 40 and wh71.tensile <= 50 " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(3) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblMSCS.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 50 and wh71.tensile <= 60 " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 50 and wh71.tensile <= 60 " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(4) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICS.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 60 " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.tensile > 60 " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(5) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHIS.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.steel_grade_code like '6%' " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.carbon > " & EXLC_C.ToString() & " and wh71.steel_grade_code like '6%' " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(6) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblSUS.Text = calTmp.ToString("0.00")

        gvMonth2.DataSource = dtDataTable
        gvMonth2.DataBind()
        gvMonth2.HeaderRow.Visible = False


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.inspection_code < '6000' and wh71.inspection_code >= '5000' " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.inspection_code < '6000' and wh71.inspection_code >= '5000' " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(0) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblNRCQ.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.inspection_code < '5000' and wh71.inspection_code >= '4000' " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.inspection_code < '5000' and wh71.inspection_code >= '4000' " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblHICQ.Text = calTmp.ToString("0.00")


        strACCESS = String.Format(
            "select ISNULL(A.product_day, B.product_day) as ProductDay, ISNULL(A.product_weight, 0) + ISNULL(B.product_weight, 0) as total_prod from " &
            "(select SUBSTRING(wh73.shift_date, 7, 2) as product_day, SUM(wh73.g_weight) as product_weight from h_pmis_wh73 as wh73, h_pmis_wh71 as wh71 " &
            "where wh73.shift_date like '{0}%' and SUBSTRING(wh73.product_no,1, 7) = wh71.coil_no " &
            "and wh71.inspection_code < '4000' and wh71.inspection_code >= '2000' " &
            "Group by SUBSTRING(wh73.shift_date, 7, 2)) as A " &
            "FULL OUTER JOIN " &
            "(select SUBSTRING(wh76.shift_date, 7, 2) as product_day, SUM(wh76.gross_weight) as product_weight from h_pmis_wh76 as wh76, h_pmis_wh71 as wh71 " &
            "where wh76.shift_date like '{0}%' and wh76.coil_no = wh71.coil_no " &
            "and wh71.inspection_code < '4000' and wh71.inspection_code >= '2000' " &
            "Group by SUBSTRING(wh76.shift_date, 7, 2)) as B ON A.product_day = B.product_day",
            Now.ToString("yyyyMM"))
        dtTmp = execQuery(strACCESS, "", Conn)
        calTmp = 0
        For i As Integer = 0 To dtTmp.Rows.Count - 1
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtdatatable1.Rows(dtTmp.Rows(i).Item(0) - 1).Item(2) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblVHCQ.Text = calTmp.ToString("0.00")

        gvMonth4.DataSource = dtdatatable1
        gvMonth4.DataBind()
        gvMonth4.HeaderRow.Visible = False
        Conn.Close()
    End Sub

End Class