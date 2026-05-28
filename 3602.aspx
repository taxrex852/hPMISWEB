<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3602.aspx.vb" Inherits="hPMISWEB.HSM_Stock2" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>HSM_Stock2</title>
    <link rel="stylesheet" href="libs/bootstrap.min.css" />

    <style type="text/css">
        body { background-color: #f8f9fc; padding-bottom: 20px; }

        .main-content {
            clear: both !important;
            display: block !important;
            position: relative;
            padding-top: 20px;
        }

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
            min-height: 380px;
            width: 100%;
        }

        .gv {
            width: auto !important;
            white-space: nowrap;
            border-collapse: collapse;
        }

        tr.gvrs td {
            padding: 10px 18px;
            text-align: center;
            vertical-align: middle;
            border-bottom: 1px solid #e2e8f0;
            color: #2d3748;
            white-space: nowrap;
            background-color: #ffffff;
        }

        tr.gvhs td {
            background-color: #34495e !important;
            color: white !important;
            font-weight: 600;
            padding: 10px 18px;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
        }

        .gv tbody tr:nth-child(even) td { background-color: #f8f9fc !important; }
        .gv tbody tr:hover td { background-color: #e9ecef !important; }

        .gv tbody tr td:first-child {
            font-weight: bold;
            color: #2c3e50;
            background-color: #f8f9fa !important;
        }

        .table-scroll {
            overflow-x: auto;
            border: 1px solid #e2e8f0;
            border-radius: 4px;
        }
    </style>

    <script type="text/javascript" src="libs/echarts.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />

        <div class="container-fluid main-content px-4">

            <!-- Card 1: 滿儲率圖表 -->
            <div class="card-custom mb-4 mt-2">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">&#128202; 成品廠/製品庫各儲區滿儲率</span>
                    <span class="badge bg-warning text-dark fs-6 shadow-sm">資料時間：<asp:Label ID="lblDataTime" runat="server"></asp:Label></span>
                </div>
                <div class="chart-card-body">
                    <div id="mainChart" style="width: 100%; height: 360px;"></div>
                </div>
            </div>

            <!-- Card 2: 滿儲率門檻 -->
            <div class="card-custom mb-4">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">&#9888; 滿儲率門檻設定 (%)</span>
                </div>
                <div class="card-body p-3">
                    <div style="text-align: center;">
                        <div class="table-scroll" style="display: inline-block; text-align: left;">
                            <asp:GridView ID="gvlimit" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <RowStyle CssClass="gvrs" />
                                <HeaderStyle CssClass="gvhs" />
                                <FooterStyle CssClass="gvfs" />
                                <PagerStyle CssClass="gvps" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <EditRowStyle CssClass="gvers" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Card 3: 庫存明細 -->
            <div class="card-custom mb-5">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">&#128203; 庫存明細</span>
                </div>
                <div class="card-body p-3">
                    <div style="text-align: center;">
                        <div class="table-scroll" style="display: inline-block; text-align: left;">
                            <asp:GridView ID="gvStock" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <RowStyle CssClass="gvrs" ForeColor="Blue" />
                                <HeaderStyle CssClass="gvhs" />
                                <FooterStyle CssClass="gvfs" />
                                <PagerStyle CssClass="gvps" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <EditRowStyle CssClass="gvers" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var rawData = '<%= ChartDataJson %>';
            var limitData = <%= LimitDataJson %>;
            var chartData = [];
            try { chartData = JSON.parse(rawData); } catch (e) { return; }
            if (!chartData || chartData.length === 0) return;

            var categories = ['D1', 'D2', 'D1+D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D3˜D7'];
            var chartDom = document.getElementById('mainChart');
            var myChart = echarts.init(chartDom);

            var option = {
                backgroundColor: 'transparent',
                title: {
                    text: '成品廠/製品庫各儲區滿儲率 (%)',
                    left: 'center',
                    top: 0,
                    textStyle: { color: '#2c3e50', fontSize: 15, fontWeight: 'bold' }
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: { type: 'shadow' },
                    formatter: function (params) {
                        var idx = params[0].dataIndex;
                        var val = params[0].value;
                        var limit = limitData ? limitData[idx] : '-';
                        return categories[idx] + '<br/>滿儲率：<b>' + val + '%</b><br/>門檻：' + limit + '%';
                    }
                },
                legend: {
                    data: ['滿儲率', '門檻'],
                    bottom: 0,
                    icon: 'circle'
                },
                grid: { left: '3%', right: '4%', bottom: '10%', top: '15%', containLabel: true },
                xAxis: {
                    type: 'category',
                    data: categories,
                    axisTick: { alignWithLabel: true },
                    axisLabel: { interval: 0, fontWeight: 'bold' }
                },
                yAxis: {
                    type: 'value',
                    max: 100,
                    name: '%',
                    splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } }
                },
                series: [
                    {
                        name: '滿儲率',
                        type: 'bar',
                        barWidth: '45%',
                        label: { show: true, position: 'top', formatter: '{c}%', fontWeight: 'bold' },
                        data: chartData,
                        itemStyle: {
                            color: function (params) {
                                var val = params.value;
                                var limit = limitData ? limitData[params.dataIndex] : 80;
                                return (val >= limit) ? '#e74c3c' : '#3498db';
                            }
                        }
                    },
                    {
                        name: '門檻',
                        type: 'line',
                        symbol: 'none',
                        lineStyle: { type: 'dashed', color: '#e67e22', width: 2 },
                        itemStyle: { color: '#e67e22' },
                        data: limitData
                    }
                ]
            };

            myChart.setOption(option);
            window.addEventListener('resize', function () { myChart.resize(); });
        });
    </script>
    <script src="libs/bootstrap.bundle.min.js"></script>
</body>
</html>
