<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3106.aspx.vb" Inherits="hPMISWEB.HBM_Produce" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>HBM_Produce</title>
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
        
        /* 統一深色表頭設定 */
        .card-header-custom { 
            background-color: #2c3e50 !important; 
            color: #ffffff !important; 
            font-weight: bold; 
            padding: 12px 20px; 
            display: flex; 
            justify-content: space-between; 
            align-items: center; 
        }

        /* --- 防塌陷圖表區塊 (這段是解決圖表消失的關鍵) --- */
        .chart-card-body {
            padding: 20px;
            background-color: #ffffff;
            display: block !important;
            min-height: 420px; /* 強制最低高度 */
            width: 100%;
        }
        .chart-flex-wrapper {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            width: 100%;
            height: 100%;
        }
        .chart-box {
            flex: 1 1 45%;
            min-width: 300px;
            height: 380px;
        }

        /* --- 表格自適應與置中設定 --- */
        .table-wrapper-center {
            display: flex;
            justify-content: center; /* 讓表格在 Card 中置中 */
            width: 100%;
            padding: 15px 0;
        }

        .table-responsive-custom {
            max-height: 500px; 
            overflow-y: auto;
            overflow-x: auto;
            width: fit-content; 
            max-width: 100%;    
            box-shadow: 0 0 10px rgba(0,0,0,0.05); 
        }

        .custom-auto-table {
            width: auto !important; 
            table-layout: auto !important; 
            white-space: nowrap; 
            border-collapse: separate; 
            border-spacing: 0;
            margin-bottom: 0;
            font-size: 1.1rem; /* 字體放大 */
        }

        /* --- 凍結表頭與表尾 --- */
        .custom-auto-table thead th, .custom-auto-table th {
            position: sticky;
            top: 0;
            z-index: 10;
            background-color: #34495e !important; 
            color: white !important; 
            text-align: center;
            vertical-align: middle;
            padding: 15px 25px; 
            font-weight: 600;
            border-bottom: 2px solid #233140 !important;
        }

        .custom-auto-table tfoot td {
            position: sticky;
            bottom: 0;
            z-index: 10;
            background-color: #eaecf4 !important; 
            color: #2d3748 !important;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
            padding: 15px 25px;
            border-top: 2px solid #cbd5e1;
        }

        /* 資料列樣式 */
        .custom-auto-table tbody td {
            vertical-align: middle;
            text-align: center;
            padding: 12px 25px; 
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
            // 1. 表格合併腳本
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
                
                var gvDaily = document.getElementById('<%= gvDaily.ClientID %>');
                if (gvDaily) {
                    gvDaily.className = 'table custom-auto-table';
                }

            } catch (e) {
                console.error("表格設定失敗:", e);
            }

            // ==========================================
            // 2. ECharts 初始化 (容錯設計)
            // ==========================================
            try {
                if (typeof chartData !== 'undefined') {
                    var percentChart = echarts.init(document.getElementById('echartPercent'));
                    var optionPercent = {
                        backgroundColor: 'transparent',
                        title: { text: '📈 型鋼生產效益趨勢 (百分比)', left: 'center', textStyle: { color: '#2c3e50', fontSize: 16, fontWeight: 'bold' } },
                        tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                        legend: { bottom: 0, icon: 'circle', data: ['產率 (%)', '訂單合格率 (%)', '作業率 (%)'] },
                        grid: { left: '10%', right: '5%', bottom: '15%', top: '15%', containLabel: true },
                        xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis, axisLabel: { fontWeight: 'bold' } }],
                        yAxis: [{ 
                            type: 'value', 
                            name: '百分比 (%)', 
                            axisLabel: { formatter: '{value} %' }, 
                            scale: true,
                            splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } }
                        }],
                        series: [
                            { name: '產率 (%)', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, itemStyle: { color: '#2ecc71' }, data: chartData.py },
                            { name: '訂單合格率 (%)', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, itemStyle: { color: '#3498db' }, data: chartData.po },
                            { name: '作業率 (%)', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, itemStyle: { color: '#9b59b6' }, data: chartData.or }
                        ]
                    };
                    percentChart.setOption(optionPercent);

                    var weightChart = echarts.init(document.getElementById('echartWeight'));
                    var optionWeight = {
                        backgroundColor: 'transparent',
                        title: { text: '⚖️ 型鋼生產重量趨勢 (MT)', left: 'center', textStyle: { color: '#2c3e50', fontSize: 16, fontWeight: 'bold' } },
                        tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                        legend: { bottom: 0, icon: 'circle', data: ['產量 (MT)', '剔退重量 (MT)'] },
                        grid: { left: '12%', right: '5%', bottom: '15%', top: '15%', containLabel: true },
                        xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis, axisLabel: { fontWeight: 'bold' } }],
                        yAxis: [{ 
                            type: 'value', 
                            name: '重量 (MT)', 
                            scale: true,
                            splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } } 
                        }],
                        series: [
                            { name: '產量 (MT)', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, itemStyle: { color: '#f39c12' }, data: chartData.pa },
                            { name: '剔退重量 (MT)', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, itemStyle: { color: '#e74c3c' }, data: chartData.mr }
                        ]
                    };
                    weightChart.setOption(optionWeight);

                    window.addEventListener('resize', function () {
                        percentChart.resize();
                        weightChart.resize();
                    });
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
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HBMPMISConnectionString %>" SelectCommand="WITH MonthlyMP01 AS (
                SELECT 
                    DATEADD(month, DATEDIFF(month, 0, process_date), 0) AS prod_month,
                    SUM(Measured_BLBB_weight) / 1000.0 AS Slab_wgt_PDI,
                    SUM(CASE WHEN Rolling_flag = 1 THEN Measured_BLBB_weight ELSE 0 END) / 1000.0 AS Slab_wgt,
                    SUM(CASE WHEN Rolling_flag IN (2, 3) THEN Measured_BLBB_weight ELSE 0 END) / 1000.0 AS MR_wgt
                FROM h_pmis_mp01
                WHERE process_date BETWEEN DATEADD(year, -1, GETDATE()) AND GETDATE()
                GROUP BY DATEADD(month, DATEDIFF(month, 0, process_date), 0)
            )
            SELECT 
                CAST(m.prod_month AS date) AS process_date,
                ISNULL(ROUND(m.Slab_wgt_PDI, 2), 0) AS PA,
                ISNULL(ROUND((m.Slab_wgt * 100.0) / NULLIF(m.Slab_wgt_PDI, 0), 2), 0) AS PY,
                ISNULL(ROUND(((m.Slab_wgt - m.MR_wgt) * 100.0) / NULLIF(m.Slab_wgt_PDI, 0), 2), 0) AS PO,
                100 AS [OR],
                ISNULL(ROUND(m.MR_wgt, 2), 0) AS MR
            FROM MonthlyMP01 m
            ORDER BY m.prod_month"></asp:SqlDataSource>

            <div class="card-custom mb-4 mt-2">
                <div class="card-header-custom">
                    <span class="fs-4 text-white">📊 型鋼生產效益與重量趨勢</span>
                    <span class="badge bg-warning text-dark fs-6 shadow-sm">資料區間：<asp:Label ID="LabelStartdate" runat="server"></asp:Label> ~ <asp:Label ID="LabelEnddate" runat="server"></asp:Label></span>
                </div>
                
                <div class="chart-card-body">
                    <div class="chart-flex-wrapper">
                        <div class="chart-box">
                            <div id="echartPercent" style="width: 100%; height: 100%;"></div>
                        </div>
                        <div class="chart-box">
                            <div id="echartWeight" style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-custom mb-4">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4 text-white">📋 型鋼每日生產履歷</span>
                        <span class="badge bg-warning text-dark ms-2 fw-normal" style="font-size: 0.9rem;">每日 07:00 / 15:00 / 23:00 進行資料更新</span>
                    </div>
                </div>
                
                <div class="card-body p-0">
                    <div class="table-wrapper-center">
                        <div class="table-responsive-custom">
                            <asp:GridView ID="gvDaily" runat="server" GridLines="None" UseAccessibleHeader="true" ShowHeaderWhenEmpty="true">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-custom mb-5">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4 text-white">📋 型鋼當月生產履歷</span>
                        <span class="badge bg-warning text-dark ms-2 fw-normal" style="font-size: 0.9rem;">資料於每日 23:00 更新，月初重新編排</span>
                    </div>
                </div>
                
                <div class="card-body p-0">
                    <div style="display: none;">
                        <table id="tempMonthHeader">
                            <tr>
                                <th>月份</th>
                                <th>產量/MT</th>
                                <th>產率/%</th>
                                <th>訂單合格率/%</th>
                                <th>作業率/%</th>
                                <th>剔退重量/MT</th>
                            </tr>
                        </table>
                        <table id="tempMonthFooter">
                            <tr>
                                <td><asp:Label ID="lblMonth" runat="server" Text="N/A"></asp:Label>月統計</td>
                                <td><asp:Label ID="lblPA" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblPY" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblPO" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblOR" runat="server" Text="N/A"></asp:Label></td>
                                <td><asp:Label ID="lblMR" runat="server" Text="N/A"></asp:Label></td>
                            </tr>
                        </table>
                    </div>

                    <div class="table-wrapper-center">
                        <div class="table-responsive-custom">
                            <asp:GridView ID="gvMonth" runat="server" GridLines="None" ShowHeader="False">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script src="libs/bootstrap.bundle.min.js"></script>
</body>
</html>