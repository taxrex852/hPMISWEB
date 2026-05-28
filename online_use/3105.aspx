<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3105.aspx.vb" Inherits="hPMISWEB.Offline_packing_Produce" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>TNRL Offline Packing</title>
    <link rel="stylesheet" href="libs/bootstrap.min.css" />
    <style type="text/css">
        body { background-color: #f8f9fc; padding-bottom: 20px; }
        .main-content { clear: both !important; display: block !important; position: relative; padding-top: 20px; }
        
        /* 卡片與風格 */
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
        .chart-card-body { 
            padding: 20px; 
            background-color: #ffffff; 
            display: block !important; 
            min-height: 420px; 
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
            flex: 1 1 100%; 
            min-width: 300px; 
            height: 380px; 
        }
        
        /* 表格與滾動 */
        .table-wrapper-center { 
            display: flex; 
            justify-content: center; 
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
            font-size: 1.1rem; 
        }
        
        /* 表頭凍結 */
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
        
        /* 表尾凍結 */
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
        .custom-auto-table tbody td { 
            vertical-align: middle; 
            text-align: center; 
            padding: 12px 25px; 
            border-bottom: 1px solid #e2e8f0; 
            color: #2d3748; 
        }
        .custom-auto-table tbody tr:nth-child(even) { background-color: #f8f9fc; }
        .custom-auto-table tbody tr:hover { background-color: #e9ecef; }
        
        /* 首欄加粗醒目 */
        .custom-auto-table tbody td:first-child { 
            font-weight: bold; 
            color: #2c3e50; 
            background-color: #f8f9fa; 
        }
    </style>
    <script type="text/javascript" src="libs/echarts.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            // 合併 gvMonth 與預留表頭表尾
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
                console.error("表格合併失敗:", e);
            }

            // 初始化 ECharts
            try {
                if (typeof chartData !== 'undefined') {
                    var weightChart = echarts.init(document.getElementById('echartWeight'));
                    weightChart.setOption({
                        backgroundColor: 'transparent',
                        title: { 
                            text: '⚖️ 離線包裝 生產重量趨勢 (MT)', 
                            left: 'center', 
                            textStyle: { color: '#2c3e50', fontSize: 16, fontWeight: 'bold' } 
                        },
                        tooltip: { 
                            trigger: 'axis', 
                            axisPointer: { type: 'cross' } 
                        },
                        legend: { 
                            bottom: 0, 
                            icon: 'circle', 
                            data: ['產量 (MT)'] 
                        },
                        grid: { left: '12%', right: '5%', bottom: '15%', top: '15%', containLabel: true },
                        xAxis: [{ 
                            type: 'category', 
                            boundaryGap: false, 
                            data: chartData.xAxis, 
                            axisLabel: { fontWeight: 'bold' } 
                        }],
                        yAxis: [{ 
                            type: 'value', 
                            name: '重量 (MT)', 
                            scale: true, 
                            splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } } 
                        }],
                        series: [
                            { 
                                name: '產量 (MT)', 
                                type: 'line', 
                                smooth: true, 
                                symbol: 'circle', 
                                symbolSize: 6, 
                                lineStyle: { width: 3 }, 
                                itemStyle: { color: '#f39c12' }, 
                                data: chartData.pa 
                            }
                        ]
                    });
                    
                    window.addEventListener('resize', function () {
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
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>"
                SelectCommand="select dateadd(m, datediff(m,0,process_date),0) as process_date, cast(round(SUM(Weight)/1000,2) as float) as PA 
from h_pmis_ys01
where process_date between DATEADD(year,-1,getdate()) and getdate() 
group by dateadd(m, datediff(m,0,process_date),0)
order by process_date"></asp:SqlDataSource>

            <!-- Card 1：生產重量趨勢圖 -->
            <div class="card-custom mb-4 mt-2">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">📊 離線包裝 生產重量趨勢</span>
                    <span class="badge bg-warning text-dark fs-6 shadow-sm">資料區間：<asp:Label ID="LabelStartdate" runat="server"></asp:Label> ~ <asp:Label ID="LabelEnddate" runat="server"></asp:Label></span>
                </div>
                <div class="chart-card-body">
                    <div class="chart-flex-wrapper">
                        <div class="chart-box">
                            <div id="echartWeight" style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Card 2：每日生產履歷 -->
            <div class="card-custom mb-4">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 離線包裝 每日生產履歷</span>
                    </div>
                </div>
                <div class="card-body p-0">
                    <div class="table-wrapper-center">
                        <div class="table-responsive-custom">
                            <asp:GridView ID="gvDaily" runat="server" GridLines="None" UseAccessibleHeader="true" ShowHeaderWhenEmpty="true"></asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Card 3：當月生產履歷 -->
            <div class="card-custom mb-5">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 離線包裝 當月生產履歷</span>
                    </div>
                </div>
                <div class="card-body p-0">
                    <!-- 隱藏表格，用於為 GridView 提供表頭與表尾結構以進行 JavaScript 合併 -->
                    <div style="display: none;">
                        <table id="tempMonthHeader">
                            <tr>
                                <th>日期</th>
                                <th>產量/MT</th>
                            </tr>
                        </table>
                        <table id="tempMonthFooter">
                            <tr>
                                <td><asp:Label ID="lblMonth" runat="server" Text="N/A"></asp:Label>月統計</td>
                                <td><asp:Label ID="lblPA" runat="server" Text="N/A"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <!-- 使用寫死 inline styles 強制 Flexbox 圖片與表格平行並列對齊，並一起置中 -->
                    <div style="display: flex !important; flex-direction: row !important; flex-wrap: nowrap !important; justify-content: center !important; align-items: center !important; gap: 24px !important; width: 100%; overflow-x: auto; padding: 15px;">
                        <!-- 左邊：表格區塊 (帶有 Panel 滾動條) -->
                        <div style="border: 1px solid #e3e6f0; border-radius: 4px; overflow: hidden; flex-shrink: 0; margin: 10px;">
                            <asp:Panel ID="Panel1" runat="server" Height="300px" Width="342px" ScrollBars="Vertical">
                                <asp:GridView ID="gvMonth" runat="server" GridLines="None" ShowHeader="False"></asp:GridView>
                            </asp:Panel>
                        </div>
                        <!-- 右邊：圖片區塊 -->
                        <div style="flex-shrink: 0; text-align: center; margin: 10px;">
                            <img src="images/OFFLINEPACKING.JPG" alt="Offline Packing" class="img-fluid rounded shadow-sm" style="max-height: 300px; display: block;" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script src="libs/bootstrap.bundle.min.js"></script>
</body>
</html>
