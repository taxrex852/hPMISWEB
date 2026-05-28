<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3201.aspx.vb" Inherits="hPMISWEB.HSM_Defect" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>HSM_Defect</title>
    <link rel="stylesheet" href="libs/bootstrap.min.css" />

    <style type="text/css">
        /* --- 全域與背景 --- */
        body { background-color: #f8f9fc; padding-bottom: 20px; }

        .main-content {
            clear: both !important;
            display: block !important;
            position: relative;
            padding-top: 20px;
        }

        /* --- 卡片風格 --- */
        .card-custom {
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.05);
            border: 1px solid #e3e6f0;
            margin-bottom: 25px;
            overflow: hidden;
            display: block !important;
        }
        .card-header-custom {
            background-color: #2c3e50 !important;
            color: #ffffff !important;
            font-weight: bold;
            padding: 12px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        /* --- 圖表區塊 --- */
        .chart-card-body {
            padding: 20px;
            background-color: #ffffff;
            display: block !important;
            min-height: 420px;
            width: 100%;
        }

        /* --- 表格捲軸設定 --- */
        .table-responsive-custom {
            max-height: 450px;
            overflow-y: auto;
            overflow-x: auto;
            margin: 0;
            background-color: #fff;
        }

        .custom-auto-table {
            width: auto;
            table-layout: auto !important;
            white-space: nowrap;
            border-collapse: separate;
            border-spacing: 0;
            margin-bottom: 0;
            margin: 0 auto;
        }

        /* --- 凍結表頭 --- */
        .custom-auto-table thead th {
            position: sticky;
            top: 0;
            z-index: 10;
            background-color: #34495e !important;
            color: white !important;
            text-align: center;
            vertical-align: middle;
            padding: 12px 18px;
            font-weight: 600;
            border-bottom: 2px solid #233140 !important;
        }
        /* --- 強制 GridView 表頭深色白字 --- */
.table-header-dark th, .table-header-dark td {
    background-color: #34495e !important;
    color: #ffffff !important;
    position: sticky;
    top: 0;
    z-index: 10;
    text-align: center;
    vertical-align: middle;
    padding: 12px 18px;
    font-weight: 600;
    border-bottom: 2px solid #233140 !important;
}
        /* --- 凍結表尾 --- */
        .custom-auto-table tfoot td {
            position: sticky;
            bottom: 0;
            z-index: 10;
            background-color: #eaecf4 !important;
            color: #2d3748 !important;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
            padding: 12px 18px;
            border-top: 2px solid #cbd5e1;
        }

        /* --- 資料列 --- */
        .custom-auto-table tbody td {
            vertical-align: middle;
            text-align: center;
            padding: 10px 18px;
            border-bottom: 1px solid #e2e8f0;
            color: #2d3748;
        }

        .custom-auto-table tbody tr:nth-child(even) { background-color: #f8f9fc; }
        .custom-auto-table tbody tr:hover { background-color: #e9ecef; }

        .custom-auto-table tbody td:first-child {
            font-weight: bold;
            color: #2c3e50;
            background-color: #f8f9fa;
        }
    </style>

    <script type="text/javascript" src="libs/echarts.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {

            // ==========================================
            // 1. 月份表格合併 (thead / tfoot 注入)
            // ==========================================
            try {
                var gvMonth = document.getElementById('<%= gvMonth.ClientID %>');
                var tblHeader = document.getElementById('tempMonthHeader');
                var tblFooter = document.getElementById('tempMonthFooter');

                if (gvMonth && tblHeader && tblFooter) {
                    var theadRow = tblHeader.querySelector('tr');
                    if (theadRow) {
                        var thead = document.createElement('thead');
                        thead.appendChild(theadRow);
                        gvMonth.insertBefore(thead, gvMonth.firstChild);
                    }

                    var tfootRow = tblFooter.querySelector('tr');
                    if (tfootRow) {
                        var tfoot = document.createElement('tfoot');
                        tfoot.appendChild(tfootRow);
                        gvMonth.appendChild(tfoot);
                    }
                    gvMonth.className = 'table custom-auto-table';
                }
            } catch (e) {
                console.error("表格合併失敗:", e);
            }

            // ==========================================
            // 2. ECharts 初始化
            // ==========================================
            try {
                if (typeof chartData !== 'undefined') {
                    var defectDom = document.getElementById('echartDefect');
                    var defectChart = echarts.init(defectDom);

                    var optionDefect = {
                        backgroundColor: 'transparent',
                        title: { text: '缺陷月累計趨勢 (MT)', left: 'center', textStyle: { color: '#2c3e50', fontSize: 15, fontWeight: 'bold' } },
                        tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                        legend: { data: ['缺陷 Top 1', '缺陷 Top 2', '缺陷 Top 3', '缺陷 Top 4', '缺陷 Top 5'], bottom: 0, icon: 'circle' },
                        grid: { left: '8%', right: '5%', bottom: '15%', top: '15%', containLabel: true },
                        xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis, axisLabel: { fontWeight: 'bold' } }],
                        yAxis: [{ type: 'value', name: '重量 (MT)', scale: true, splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } } }],
                        series: [
                            { name: '缺陷 Top 1', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.d1 },
                            { name: '缺陷 Top 2', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.d2 },
                            { name: '缺陷 Top 3', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.d3 },
                            { name: '缺陷 Top 4', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.d4 },
                            { name: '缺陷 Top 5', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.d5 }
                        ]
                    };

                    defectChart.setOption(optionDefect);

                    window.addEventListener('resize', function () {
                        defectChart.resize();
                    });
                } else {
                    console.warn("找不到 chartData，圖表無法繪製。");
                }
            } catch (e) {
                console.error("ECharts 繪製失敗:", e);
            }
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <a name="#Home"></a>

        <div class="container-fluid main-content px-4">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="WITH UnpivotData AS (

    SELECT 
        DATEADD(m, DATEDIFF(m, 0, product_date), 0) AS product_month,
        v.def_code,
        coil_wm
    FROM h_pmis_whqh WITH (NOLOCK)
    CROSS APPLY (
        VALUES (no1_code), (no2_code), (no3_code), (no4_code), (no5_code)
    ) v(def_code)
    WHERE product_date BETWEEN DATEADD(year, -1, GETDATE()) AND GETDATE()
      AND v.def_code IS NOT NULL 
      AND v.def_code != ''
),
RankedData AS (

    SELECT 
        product_month,
        def_code,
        CAST(ROUND(SUM(coil_wm) / 1000.0, 2) AS FLOAT) AS total_coil_wm,
        ROW_NUMBER() OVER (
            PARTITION BY product_month 
            ORDER BY CAST(ROUND(SUM(coil_wm) / 1000.0, 2) AS FLOAT) DESC
        ) AS rn
    FROM UnpivotData
    GROUP BY product_month, def_code
)

SELECT 
    product_month AS product_date,
    MAX(CASE WHEN rn = 1 THEN total_coil_wm END) AS def_top1,
    MAX(CASE WHEN rn = 2 THEN total_coil_wm END) AS def_top2,
    MAX(CASE WHEN rn = 3 THEN total_coil_wm END) AS def_top3,
    MAX(CASE WHEN rn = 4 THEN total_coil_wm END) AS def_top4,
    MAX(CASE WHEN rn = 5 THEN total_coil_wm END) AS def_top5
FROM RankedData
WHERE rn <= 5
GROUP BY product_month
ORDER BY product_month;">
            </asp:SqlDataSource>

            <!-- ========== 第一層 Card：缺陷趨勢圖 ========== -->
            <div class="card-custom mb-4 mt-2">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">📊 缺陷月累計趨勢</span>
                    <span class="badge bg-warning text-dark fs-6 shadow-sm">資料區間：<asp:Label ID="LabelStartdate" runat="server"></asp:Label> ~ <asp:Label ID="LabelEnddate" runat="server"></asp:Label></span>
                </div>
                <div class="chart-card-body">
                    <div id="echartDefect" style="width: 100%; height: 400px;"></div>
                </div>
            </div>

            <!-- ========== 第二層 Card：每日缺陷統計 ========== -->
            <div class="card-custom mb-4">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 近期每日缺陷重量累計</span>
                        <%--<span class="badge bg-warning text-dark ms-2 fw-normal" style="font-size: 0.9rem;">每日 0700/1500/2300 更新 / 每日 1500 為前一日完整統計</span>--%>
                    
                    </div>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvDaily" runat="server" CssClass="table custom-auto-table" GridLines="None">

                            <HeaderStyle  Wrap="True" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- ========== 第三層 Card：月份缺陷統計 ========== -->
            <div class="card-custom mb-5">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 近期月份缺陷重量累計</span>
                        <%--<span class="badge bg-warning text-dark ms-2 fw-normal" style="font-size: 0.9rem;">每晚 23:00 更新 / 月初重新統計</span>--%>
                    </div>
                </div>
                <div class="card-body p-0">
                    <div style="display: none;">
                        <table id="tempMonthHeader">
                            <tr>
                                <th>月份</th>
                                <th>缺陷 Top (1)</th>
                                <th>缺陷 Top (2)</th>
                                <th>缺陷 Top (3)</th>
                                <th>缺陷 Top (4)</th>
                                <th>缺陷 Top (5)</th>
                            </tr>
                        </table>
                        <table id="tempMonthFooter">
                            <tr>
                                <td><asp:Label ID="lblMonth" runat="server" Text="N/A"></asp:Label>月統計</td>
                                <td><asp:Label ID="lblDT1" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblDT2" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblDT3" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblDT4" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblDT5" runat="server" Text="N/A"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvMonth" runat="server" CellSpacing="1" GridLines="None" ShowHeader="False">
                            <RowStyle CssClass="gvrs" />
                            <FooterStyle CssClass="gvfs" />
                            <PagerStyle CssClass="gvps" />
                            <SelectedRowStyle CssClass="gvsrs" />
                            <EditRowStyle CssClass="gvers" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script src="libs/bootstrap.bundle.min.js"></script>
</body>
</html>
