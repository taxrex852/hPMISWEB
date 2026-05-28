Imports System.Data.SqlClient
Imports System.Collections.Generic

''' <summary>
''' 4TNRL 產品尺寸/強度分類生產統計頁面 (PAGE_ID=3407)
''' 資料來源：h_pmis_wh73 (g_weight)、h_pmis_wh76 (gross_weight)、h_pmis_wh71 (JOIN條件)
''' 尺寸分類（MDSZ = PA - 其餘各類之和）：
'''   ETNG：avg_width ≤ 1260 且 avg_thickness ≤ 1500（窄幅薄板）
'''   WTNG：avg_width ≥ 1500 且 avg_thickness ≤ 2300（寬幅薄板）
'''   NTNG：avg_width 1260~1500 且 avg_thickness 1500~1900（中幅中板）
'''   NTCG：avg_thickness 6000~9900（中厚板）
'''   ETCG：avg_thickness > 9900（極厚板）
'''   MDSZ：其餘（PA 扣除上列各類）
''' 寬度分類：NRWD(width<950)、MDWD(950~1550)、WIWD(≥1550)
''' 強度/品質分類（以 h_pmis_wh71 JOIN）：
'''   EXLC：carbon ≤ EXLC_C（100）；LSCS：tensile≤40；MSCS：tensile 40~50
'''   HICS：tensile 50~60；VHIS：tensile>60；SUS：steel_grade_code LIKE '6%'
'''   NRCQ：inspection_code 5000~5999；HICQ：4000~4999；VHCQ：2000~3999
''' 連線：getConnStr（HPMIS 資料庫）
''' </summary>
Partial Public Class _4TNRL_Production
    Inherits System.Web.UI.Page
    Private Const PAGE_ID = "3407"
    Private Conn As SqlConnection
    ''' <summary>EXLC 分類碳含量門檻值（carbon ≤ 100 → EXLC 低碳鋼）</summary>
    Private Const EXLC_C As Integer = 100

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ' 設定 Title
            setTitle(Me, PAGE_ID)

            ' 設定資料區間標題
            LabelStartdate.Text = Date.Today.AddMonths(-11).ToString("yyyy/MM")
            LabelEnddate.Text = Date.Today.ToString("yyyy/MM")

            Mainprocess()
        End If
    End Sub

    ''' <summary>
    ''' 主流程：建立 HPMIS 連線，依序執行兩張日報表再執行趨勢圖
    ''' </summary>
    Private Sub Mainprocess()
        Conn = New SqlConnection(getConnStr(Application("ConnStr")))
        ' 本月日報表
        TNRL_Table1()
        TNRL_Table2()
        ' ECharts 趨勢圖資料（原始資料來源：h_pmis_wh73 + h_pmis_wh76）
        BuildChartData()
    End Sub

    ''' <summary>
    ''' 建立 ECharts 趨勢圖 JSON 資料（近 12 個月，19 個分類）
    ''' 資料來源：h_pmis_wh73 + h_pmis_wh76 FULL OUTER JOIN（原始 SqlDataSource1 邏輯）
    ''' 所有分類皆以近一年資料（DATEADD(year,-1,getdate()) ~ getdate()）查詢
    ''' 單位：g → MT（SQL 內已除以 1000）
    ''' </summary>
    Private Sub BuildChartData()
        ' 原始 SelectCommand SQL（從 SqlDataSource1 還原，資料來源為 h_pmis_wh73 / h_pmis_wh76 / h_pmis_wh71）
        ' 共 19 個分類：ETNG/WTNG/NTNG/NTCG/ETCG/MDSZ(衍生) + NRWD/MDWD/WIWD + EXLC/LSCS/MSCS/HICS/VHIS/SUS/NRCQ/HICQ/VHCQ
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

        ' 組合 ECharts JSON 字串
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

        ' 同時更新資料區間標題（以實際資料為準）
        If dt.Rows.Count > 0 Then
            LabelStartdate.Text = Convert.ToDateTime(dt.Rows(0)("process_date")).ToString("yyyy/MM")
            LabelEnddate.Text   = Convert.ToDateTime(dt.Rows(dt.Rows.Count - 1)("process_date")).ToString("yyyy/MM")
        End If

        '填入各月各分類資料
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

        '注入至前端 JS 供 ECharts 使用
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

    ''' <summary>
    ''' 尺寸分類：本月日報表 (gvMonth1 + gvMonth3)
    ''' gvMonth1：ETNG/WTNG/NTNG/NTCG/ETCG/MDSZ（厚度/前段製程分類）
    ''' gvMonth3：NRWD/MDWD/WIWD（寬度分類）
    ''' 資料來源：h_pmis_wh73 + h_pmis_wh76 FULL OUTER JOIN，依 shift_date 篩選本月
    ''' MDSZ = PA - ETNG - WTNG - NTNG - NTCG - ETCG（衍生計算，不足時填 0.00）
    ''' 本月累計：lblETNG/lblWTNG/lblNTNG/lblNTCG/lblETCG/lblMDSZ/lblNRWD/lblMDWD/lblWIWD
    ''' </summary>
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

        '建立本月每日列，預設各欄 0.00
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

        ' ETNG：窄幅薄板（avg_width ≤ 1260 且 avg_thickness ≤ 1500）
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
            '單位換算：g → MT
            tmpValue = Val(dtTmp.Rows(i).Item(1)) / 1000
            dtDataTable.Rows(dtTmp.Rows(i).Item(0) - 1).Item(1) = tmpValue.ToString("0.00")
            calTmp += tmpValue
        Next
        lblETNG.Text = calTmp.ToString("0.00")

        ' WTNG：寬幅薄板（avg_width ≥ 1500 且 avg_thickness ≤ 2300）
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

        ' NTNG：中幅中板（avg_width 1260~1500 且 avg_thickness 1500~1900）
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

        ' NTCG：中厚板（avg_thickness 6000~9900）
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

        ' ETCG：極厚板（avg_thickness > 9900）
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

        ' MDSZ = PA - ETNG - WTNG - NTNG - NTCG - ETCG（衍生）
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
                    '衍生計算：MDSZ = PA - 已知各類之和，負值填 0.00
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

        ' NRWD：窄幅（avg_width < 950）
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

        ' MDWD：中幅（avg_width 950~1550）
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

        ' WIWD：寬幅（avg_width ≥ 1550）
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

    ''' <summary>
    ''' 強度/品質分類：本月日報表 (gvMonth2 + gvMonth4)
    ''' gvMonth2：EXLC/LSCS/MSCS/HICS/VHIS/SUS（依 h_pmis_wh71.carbon/tensile/steel_grade_code JOIN）
    ''' gvMonth4：NRCQ/HICQ/VHCQ（依 h_pmis_wh71.inspection_code JOIN）
    ''' EXLC_C = 100：碳含量門檻值，≤100 為低碳鋼（EXLC）
    ''' 本月累計：lblEXLC/lblLSCS/lblMSCS/lblHICS/lblVHIS/lblSUS/lblNRCQ/lblHICQ/lblVHCQ
    ''' </summary>
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

        ' EXLC（碳含量 ≤ EXLC_C = 100，低碳鋼）
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

        ' LSCS（tensile ≤ 40, carbon > EXLC_C，低強度鋼）
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

        ' MSCS（tensile 40~50，中強度鋼）
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

        ' HICS（tensile 50~60，高強度鋼）
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

        ' VHIS（tensile > 60，超高強度鋼）
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

        ' SUS（steel_grade_code LIKE '6%'，不鏽鋼類）
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

        ' NRCQ（inspection_code 5000~5999，一般檢驗）
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

        ' HICQ（inspection_code 4000~4999，高等級檢驗）
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

        ' VHCQ（inspection_code 2000~3999，超高等級檢驗）
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
