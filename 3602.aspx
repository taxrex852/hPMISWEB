<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3602.aspx.vb" Inherits="hPMISWEB.HSM_Stock2" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>3602 各儲區/熱軋廠庫存滿儲率</title>
    <link rel="stylesheet" href="libs/bootstrap.min.css" />

    <style type="text/css">
        body {
            font-family: "Microsoft JhengHei", Arial, sans-serif;
            background-color: #f8f9fc;
            padding-bottom: 20px;
        }

        .main-content {
            clear: both !important;
            display: block !important;
            position: relative;
            padding-top: 20px;
        }

        /* ---- Card 樣式 (同 3601) ---- */
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

        /* ---- Bootstrap 5 風格資料表格 ---- */
        .bs5-table {
            width: 100%;
            table-layout: auto;
            border-collapse: collapse;
            margin: 0 auto;
            font-size: 9pt;
        }
        .bs5-table th {
            background-color: #34495e !important;
            color: #ffffff !important;
            text-align: center !important;
            vertical-align: middle !important;
            padding: 8px 10px !important;
            font-weight: 600;
            border: 1px solid #2c3e50 !important;
            white-space: nowrap;
        }
        .bs5-table td {
            vertical-align: middle;
            text-align: center;
            padding: 7px 10px;
            border: 1px solid #dee2e6;
            white-space: nowrap;
        }
        .bs5-table tbody tr:nth-child(odd)  { background-color: #ffffff; }
        .bs5-table tbody tr:nth-child(even) { background-color: #f8f9fc; }
        .bs5-table tbody tr:hover           { background-color: #e9ecef; }

        .card-body-inner { padding: 15px; display: block; }

        .section-label {
            font-size: 13px;
            font-weight: bold;
            color: #2c3e50;
            margin: 14px 0 8px 0;
            padding-left: 6px;
            border-left: 4px solid #2c3e50;
        }

        /* ---- 滿儲率燈號面板 ---- */
        .limit-panel {
            display: flex;
            align-items: center;
            justify-content: center;
            background: #f4f7f9;
            border: 1px solid #d0d0d0;
            border-radius: 8px;
            padding: 12px 16px;
            margin: 4px 0 12px 0;
            flex-wrap: wrap;
            gap: 10px;
        }
        .limit-panel-title {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            font-size: 14px;
            font-weight: bold;
            color: #004b97;
            padding-right: 14px;
            border-right: 2px solid #c8cdd5;
            min-width: 60px;
            text-align: center;
            line-height: 1.5;
            align-self: stretch;
        }
        .limit-zone-group {
            display: flex;
            gap: 8px;
            align-items: center;
            flex-wrap: wrap;
        }
        .limit-zone-divider {
            width: 2px;
            min-height: 80px;
            background: #c8cdd5;
            margin: 0 4px;
            align-self: stretch;
        }
        .limit-zone-item {
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 5px;
            padding: 8px 10px;
            border-radius: 8px;
            background: #fff;
            border: 1px solid #e0e4ea;
            min-width: 72px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.06);
            transition: box-shadow 0.2s;
        }
        .limit-zone-item:hover { box-shadow: 0 3px 8px rgba(0,0,0,0.15); }
        .limit-zone-name {
            font-size: 13px;
            font-weight: bold;
            color: #2c3e50;
        }
        .limit-zone-circle {
            width: 54px;
            height: 54px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #fff;
            font-size: 12px;
            font-weight: bold;
            border: 2px solid rgba(0,0,0,0.12);
            text-shadow: 0 1px 2px rgba(0,0,0,0.35);
            letter-spacing: -0.5px;
        }
        .limit-zone-threshold {
            font-size: 10px;
            color: #7a8395;
        }
        .zone-ok   { background: radial-gradient(circle at 35% 35%, #aee571, #5cb85c); }
        .zone-err  {
            background: radial-gradient(circle at 35% 35%, #ff6b6b, #d9534f);
            /* Red breathing light effect */
            animation: pulse-red 1.6s ease-in-out infinite;
        }

        /* ---- Red breathing animation: scale + red glow ---- */
        @keyframes pulse-red {
            0%   {
                transform: scale(1);
                box-shadow:
                    0 0  6px 1px rgba(217, 83, 79, 0.55),
                    0 0 12px 3px rgba(217, 83, 79, 0.30);
            }
            50%  {
                transform: scale(1.10);
                box-shadow:
                    0 0 14px 5px  rgba(217, 83, 79, 0.90),
                    0 0 28px 10px rgba(217, 83, 79, 0.55),
                    0 0 42px 16px rgba(255, 80, 60, 0.25);
            }
            100% {
                transform: scale(1);
                box-shadow:
                    0 0  6px 1px rgba(217, 83, 79, 0.55),
                    0 0 12px 3px rgba(217, 83, 79, 0.30);
            }
        }

        /* Legend red dot also breathes */
        .limit-legend-dot.dot-err {
            animation: pulse-red-dot 1.6s ease-in-out infinite;
        }
        @keyframes pulse-red-dot {
            0%   { box-shadow: 0 0 2px 1px rgba(217, 83, 79, 0.40); }
            50%  { box-shadow: 0 0 6px 3px rgba(217, 83, 79, 0.85); }
            100% { box-shadow: 0 0 2px 1px rgba(217, 83, 79, 0.40); }
        }

        /* ---- Light legend ---- */
        .limit-legend {
            display: flex;
            flex-direction: column;
            justify-content: center;
            gap: 6px;
            padding-left: 10px;
            border-left: 2px solid #c8cdd5;
            align-self: stretch;
            font-size: 12px;
            color: #555;
        }
        .limit-legend-item {
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .limit-legend-dot {
            width: 14px;
            height: 14px;
            border-radius: 50%;
            flex-shrink: 0;
            border: 1px solid rgba(0,0,0,0.15);
        }
    </style>

    <script src="libs/echarts.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function() {
        if (typeof echarts === 'undefined') { return; }

        var rawData = '<%= ChartDataJson %>';
        var chartData = [];
        try {
            chartData = JSON.parse(rawData);
        } catch (e) {
            console.error("資料解析錯誤", e);
            return;
        }

        /* =========================================================
           柱狀圖：各儲區/熱軋廠庫存滿儲率
           ========================================================= */
        if (chartData && chartData.length > 0) {
            var chartDom = document.getElementById('mainChart');
            var myChart = echarts.init(chartDom);

            var commonTitleStyle = { color: '#003399', fontSize: 15, fontWeight: 'bold' };

            var option = {
                title: {
                    text: '各儲區/熱軋廠庫存滿儲率',
                    left: 'center',
                    top: 0,
                    textStyle: commonTitleStyle
                },
                tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                grid: { left: '3%', right: '4%', bottom: '3%', top: '15%', containLabel: true },
                xAxis: {
                    type: 'category',
                    data: ['D1', 'D2', 'D1+D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D3~D7'],
                    axisTick: { alignWithLabel: true },
                    axisLabel: { interval: 0 }
                },
                yAxis: { type: 'value', max: 100 },
                series: [{
                    name: '各儲區/熱軋廠庫存滿儲率',
                    type: 'bar',
                    barWidth: '40%',
                    label: { show: true, position: 'top', color: '#000' },
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
                }]
            };

            myChart.setOption(option);
            window.addEventListener('resize', function() { myChart.resize(); });

            /* =========================================================
               燈號面板：從隱藏的 gvlimit 讀取門檻，搭配 chartData 顯示
               ========================================================= */
            var zones      = ['D1', 'D2', 'D1+D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D3~D7'];
            // 預設門檻 (萬一讀取失敗時的後備值)
            var thresholds = [80, 80, 80, 75, 75, 75, 75, 75, 75];

            // 從後端 gvlimit 表格讀取實際門檻值 (cells[0]=標籤, cells[1..9]=門檻)
            var limitTable = document.getElementById('<%= gvlimit.ClientID %>');
            if (limitTable) {
                var allTd = limitTable.getElementsByTagName('td');
                for (var t = 0; t < 9; t++) {
                    var cv = parseFloat(allTd[t + 1] ? allTd[t + 1].innerText : '');
                    if (!isNaN(cv)) thresholds[t] = cv;
                }
            }

            var group1 = document.getElementById('zoneGroup1');
            var group2 = document.getElementById('zoneGroup2');

            zones.forEach(function(zone, i) {
                var val   = parseFloat(chartData[i]);
                var limit = thresholds[i];
                var isOver = (i <= 2) ? (val > 80) : (val > 75); // 依後端變色邏輯
                var colorClass = isOver ? 'zone-err' : 'zone-ok';

                var item = document.createElement('div');
                item.className = 'limit-zone-item';
                item.innerHTML =
                    '<div class="limit-zone-name">' + zone + '</div>' +
                    '<div class="limit-zone-circle ' + colorClass + '">' + val + '%</div>' +
                    '<div class="limit-zone-threshold">門檻 &le;' + limit + '%</div>';

                if (i < 3) { group1.appendChild(item); }
                else       { group2.appendChild(item); }
            });
        }
    });
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />

    <div class="container-fluid main-content px-4">

        <%-- ============================================================
             Card：各儲區/熱軋廠庫存滿儲率
             ============================================================ --%>
        <div class="card-custom mb-4 mt-2">
            <div class="card-header-custom">
                <span class="fs-4">📊 3602 各儲區/熱軋廠庫存滿儲率</span>
                <span class="badge bg-warning text-dark fs-6 shadow-sm" id="headerUpdateTime"></span>
            </div>
            <div class="card-body-inner">

                <%-- 滿儲率柱狀圖 --%>
                <div id="mainChart" style="width: 100%; height: 380px;"></div>

                <%-- 滿儲率燈號面板 (JS 動態填入) --%>
                <div class="section-label">滿儲率現況</div>
                <div class="limit-panel">
                    <div class="limit-panel-title">各儲區<br />滿儲率<br />現況</div>

                    <%-- 左群組：D1 / D2 / D1+D2（門檻 80%）--%>
                    <div class="limit-zone-group" id="zoneGroup1"></div>

                    <div class="limit-zone-divider"></div>

                    <%-- 右群組：D3 ~ D3~D7（門檻 75%）--%>
                    <div class="limit-zone-group" id="zoneGroup2"></div>

                    <div class="limit-legend">
                        <div class="limit-legend-item">
                            <div class="limit-legend-dot" style="background:#5cb85c;"></div>
                            <span>正常（低於門檻）</span>
                        </div>
                        <div class="limit-legend-item">
                            <div class="limit-legend-dot dot-err" style="background:#d9534f;"></div>
                            <span>超標（高於門檻）</span>
                        </div>
                    </div>
                </div>

                <%-- gvlimit 隱藏保留供 JS 讀取門檻值 --%>
                <div style="display: none;">
                    <asp:GridView ID="gvlimit" runat="server" GridLines="None" CssClass="bs5-table">
                    </asp:GridView>
                </div>

                <%-- 庫存數量/重量明細表 --%>
                <div class="section-label">各儲區庫存明細</div>
                <div style="overflow-x: auto;">
                    <asp:GridView ID="gvStock" runat="server" GridLines="None" CssClass="bs5-table">
                    </asp:GridView>
                </div>

            </div>
        </div>

    </div>

    <%-- SqlDataSources (完整保留，不異動) --%>
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

    </form>

    <script src="libs/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        (function () {
            var now = new Date();
            var y  = now.getFullYear();
            var m  = ('0' + (now.getMonth() + 1)).slice(-2);
            var d  = ('0' + now.getDate()).slice(-2);
            var hh = ('0' + now.getHours()).slice(-2);
            var mm = ('0' + now.getMinutes()).slice(-2);
            var el = document.getElementById('headerUpdateTime');
            if (el) el.textContent = '資料更新時間：' + y + '/' + m + '/' + d + ' ' + hh + ':' + mm;
        })();
    </script>
</body>
</html>
