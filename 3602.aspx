<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3602.aspx.vb" Inherits="hPMISWEB.HSM_Stock2" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <title>HSM_Stock2</title>
      <script src="libs/echarts.min.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />
    
    <style>
        .main-container {
            width: 95%; /* 響應式寬度，會隨視窗縮放 */
            max-width: 1200px; /* 最大寬度限制，避免大螢幕看起來太寬 */
            margin: 30px auto; /* 上下留 30px 邊距，左右 auto 達到水平置中 */
            display: flex;
            flex-direction: column; /* 讓內容由上往下垂直排列 */
            align-items: center; /* 讓內容水平置中 */
            gap: 20px; /* 每個區塊之間的間距 */
        }
        .chart-box {
            width: 100%; /* 圖表寬度撐滿容器 */
            height: 380px; 
        }
        .grid-box {
            width: 100%;
            overflow-x: auto; /* 若螢幕太小，表格會出現橫向卷軸，避免跑版 */
            text-align: center; /* 讓內部表格置中 */
        }
        /* 確保 GridView 生成的 table 也是置中的 */
        .grid-box table {
            margin: 0 auto;
        }
    </style>

    <div class="main-container">
        
        <div id="mainChart" class="chart-box"></div>

        <div class="grid-box">
            <asp:GridView ID="gvlimit" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                <RowStyle CssClass="gvrs" />
                <HeaderStyle CssClass="gvhs" />
                <FooterStyle CssClass="gvfs" />
                <PagerStyle CssClass="gvps" />
                <SelectedRowStyle CssClass="gvsrs" />
                <EditRowStyle CssClass="gvers" />
            </asp:GridView>
        </div>

        <div class="grid-box" style="margin-top: -15px;"> <asp:GridView ID="gvStock" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                <RowStyle CssClass="gvrs" ForeColor="Blue" />
                <HeaderStyle CssClass="gvhs" />
                <FooterStyle CssClass="gvfs" />
                <PagerStyle CssClass="gvps" />
                <SelectedRowStyle CssClass="gvsrs" />
                <EditRowStyle CssClass="gvers" />
            </asp:GridView>
        </div>

        <div style="display: none;">
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="
            SELECT 1 as c1, 2 as c2, 3 as c3, 4 as c4, 5 as c5, 6 as c6, 7 as c7, 8 as c8, 9 as c9, 
                   a.*, b.*, c.*, 
                   'D3toD7_' + cast(round((b.d3_orate+c.d4_orate+c.d5_orate+c.d6_orate+c.d7_orate)/5,2) as varchar) as D3_to_D7, 
                   round((b.d3_orate+c.d4_orate+c.d5_orate+c.d6_orate+c.d7_orate)/5,2) as d3_to_d7_orate
            FROM 
                (SELECT top(1)
                    'D1_'+cast(round(cast( d1_orate/10 as float),2) as varchar) as D1, round(cast( d1_orate/10 as float),2) as d1_orate, 
                    'D2_'+cast(round(cast( d2_orate/10 as float),2) as varchar) as D2, round(cast( d2_orate/10 as float),2) as d2_orate,
                    'D1+D2_'+cast(round(cast(((d1_orate/10)+(d2_orate/10))/2 as float),2) as varchar) as D1_D2,
                    round(cast(((d1_orate/10)+(d2_orate/10))/2 as float),2) as d1_d2_orate
                 FROM h_pmis_ys03 order by process_date desc) a
            CROSS JOIN 
                (SELECT top(1) 
                    'D3_'+cast(round(cast( d3_orate/10 as float),2) as varchar) as D3, round(cast( d3_orate/10 as float),2) as d3_orate 
                 FROM h_pmis_di01 order by process_date desc) b
            CROSS JOIN 
                (SELECT top(1) 
                    'D4_'+cast(round(cast( d4_orate/10 as float),2) as varchar) as D4, round(cast( d4_orate/10 as float),2) as d4_orate,
                    'D5_'+cast(round(cast( d5_orate/10 as float),2) as varchar) as D5, round(cast( d5_orate/10 as float),2) as d5_orate,
                    'D6_'+cast(round(cast( d6_orate/10 as float),2) as varchar) as D6, round(cast( d6_orate/10 as float),2) as d6_orate,
                    'D7_'+cast(round(cast( d7_orate/10 as float),2) as varchar) as D7, round(cast( d7_orate/10 as float),2) as d7_orate 
                 FROM h_pmis_pi01 order by process_date desc) c
        "></asp:SqlDataSource>
        
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="
            SELECT 
                a.d1_orate, a.d2_orate, a.d1_d2_orate,
                b.d3_orate,
                c.d4_orate, c.d5_orate, c.d6_orate, c.d7_orate,
                round((b.d3_orate+c.d4_orate+c.d5_orate+c.d6_orate+c.d7_orate)/5,2) as d3_to_d7_orate
            FROM 
                (SELECT top(1)
                    round(cast( d1_orate/10 as float),2) as d1_orate, 
                    round(cast( d2_orate/10 as float),2) as d2_orate,
                    round(cast(((d1_orate/10)+(d2_orate/10))/2 as float),2) as d1_d2_orate
                 FROM h_pmis_ys03 order by process_date desc) a
            CROSS JOIN 
                (SELECT top(1) round(cast( d3_orate/10 as float),2) as d3_orate 
                 FROM h_pmis_di01 order by process_date desc) b
            CROSS JOIN 
                (SELECT top(1) 
                    round(cast( d4_orate/10 as float),2) as d4_orate,
                    round(cast( d5_orate/10 as float),2) as d5_orate,
                    round(cast( d6_orate/10 as float),2) as d6_orate,
                    round(cast( d7_orate/10 as float),2) as d7_orate 
                 FROM h_pmis_pi01 order by process_date desc) c
        "></asp:SqlDataSource>
        </div>

    </div>
    </form>
    
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function() {
            if (typeof echarts === 'undefined') {
               
                return; 
            }

            var rawData = '<%= ChartDataJson %>';
            var chartData = [];
            try {
                chartData = JSON.parse(rawData);
            } catch (e) {
                console.error("資料解析失敗", e);
                return;
            }

            if (chartData && chartData.length > 0) {
                var chartDom = document.getElementById('mainChart');
                var myChart = echarts.init(chartDom);
                
                var option = {
                    title: { 
                        text: '	鋼捲儲區/成品倉庫庫存量',
                        left: 'center', // 讓標題也置中對齊
                        top: 0
                    },
                    tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                    grid: { 
                        left: '3%', right: '4%', bottom: '3%', 
                        top: '15%', // 增加 top 邊距，避免長條圖撞到上面的標題
                        containLabel: true 
                    },
                    xAxis: {
                        type: 'category',
                        data: ['D1', 'D2', 'D1+D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D3~D7'],
                        axisTick: { alignWithLabel: true },
                        axisLabel: { interval: 0 } 
                    },
                    yAxis: { type: 'value', max: 100 },
                    series: [
                        {
                            name: '	鋼捲儲區/成品倉庫庫存量',
                            type: 'bar',
                            barWidth: '40%', // 長條圖稍微調細一點點，畫面會比較精緻
                            label: { show: true, position: 'top' },
                            data: chartData,
                            itemStyle: {
                                color: function(params) {
                                    var val = params.value;
                                    var idx = params.dataIndex;
                                    if (idx <= 2 && val > 80) { return '#ff0000'; }
                                    else if (idx >= 3 && val > 75) { return '#ff0000'; }
                                    return '#5470c6'; 
                                }
                            }
                        }
                    ]
                };

                myChart.setOption(option);

                //讓 ECharts 隨著視窗大小自動縮放
                window.addEventListener('resize', function() {
                    myChart.resize();
                });

            }
        });
    </script>
</body>
</html>