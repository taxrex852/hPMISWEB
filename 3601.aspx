<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3601.aspx.vb" Inherits="hPMISWEB.HSM_Stock1" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>3601 扁鋼胚儲區庫存量</title>
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

        /* ---- Card 樣式 (同 3101) ---- */
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

        /* ---- 廠區底圖 ---- */
        .map-container {
            position: relative;
            width: 100%;
        }
        .map-container img {
            width: 100%;
            display: block;
        }

        /* ---- 儲區 Zone Boxes ---- */
        .zone-box {
            position: absolute;
            border: 3px solid transparent;
            opacity: 0.8;
            transition: all 0.3s ease;
            cursor: pointer;
            box-sizing: border-box;
            z-index: 10;
        }
        .map-label {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            color: #fff;
            font-weight: bold;
            font-size: 14px;
            text-shadow: 1px 1px 2px #000, -1px -1px 2px #000;
            pointer-events: none;
            white-space: nowrap;
        }

        #box-Y1-part1  { background-color: rgba(255, 0, 0, 0.3);   top: 56%; left: 4%;  width: 5%;  height: 16%; }
        #box-Y1-part2  { background-color: rgba(255, 0, 0, 0.3);   top: 65%; left: 9%;  width: 42%; height: 7%;  }
        #box-Y1-part3  { background-color: rgba(255, 0, 0, 0.3);   top: 56%; left: 46%; width: 38%; height: 9%;  }
        #box-Y1-part4  { background-color: rgba(255, 0, 0, 0.3);   top: 65%; left: 77%; width: 7%;  height: 7%;  }
        #box-Y2        { background-color: rgba(0, 255, 0, 0.3);   top: 41%; left: 42%; width: 42%; height: 16%; }
        #box-Y3        { background-color: rgba(255, 255, 0, 0.3); top: 56%; left: 27%; width: 19%; height: 9%;  }
        #box-Y3-part1  { background-color: rgba(255, 255, 0, 0.3); top: 56%; left: 9%;  width: 7%;  height: 9%;  }
        #box-Y4        { background-color: rgba(0, 0, 255, 0.3);   top: 25%; left: 61%; width: 23%; height: 16%; }
        #box-HC保溫坑  { background-color: rgba(0, 0, 0, 0.5);     top: 56%; left: 16%; width: 11%; height: 9%;  z-index: 21; }

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
            padding: 6px 8px !important;
            font-weight: 600;
            border: 1px solid #2c3e50 !important;
            white-space: nowrap;
        }
        .bs5-table td {
            vertical-align: middle;
            text-align: center;
            padding: 5px 8px;
            border: 1px solid #dee2e6;
            white-space: nowrap;
            /* color 不設定，讓 VB RowDataBound 設的 ForeColor 從 <tr> 繼承 */
        }
        /* 奇偶列底色交替（設在 tr，讓 VB inline 顏色可正常穿透顯示） */
        .bs5-table tbody tr:nth-child(odd)  { background-color: #ffffff; }
        .bs5-table tbody tr:nth-child(even) { background-color: #f8f9fc; }
        .bs5-table tbody tr:hover           { background-color: #e9ecef; }

        /* ---- Card 2 三欄內表格：自適應內容寬度，不強迫撐滿欄寬 ---- */
        /* 讓左欄(今日入儲)與右欄(今日消耗)的兩欄表格依內容縮合，不被拉寬 */
        /* 讓中欄(每日增減量)多欄表格保持自然寬並啟用水平捲動 */
        .flex-col-third .bs5-table {
            width: auto !important;      /* 依內容自適應，不強迫 100% */
            min-width: 0 !important;
            max-width: 100% !important;
        }
        /* 表格外層容器：flex 置中，使表格對齊上方圖表中央 */
        .flex-col-third .table-wrap {
            width: 100%;
            overflow-x: auto;           /* 欄位過多時（中欄）水平捲動 */
            display: flex;
            justify-content: center;    /* 表格置中對齊 ECharts */
        }

        /* ---- 中欄「每日增減量」寬度優先策略：以佔比換取更多欄寬，不縮小字型 ---- */
        /* 22吋 1920px：中欄約 840px，9pt 字型 + 原始 padding 的 7~8 日期欄約需 680px，有餘裕 */
        /* 19吋 1366px：中欄約 600px，仍可容納大部分情況，極少數多欄時才出現 scrollbar  */
        .weekly-col {
            flex: 2.5 1 calc(45% - 20px) !important; /* 佔三欄中最大份額 */
            min-width: 300px !important;
            box-sizing: border-box;
        }
        /* 左右欄略縮，讓出寬度給中欄 */
        .rcv-col, .used-col {
            flex: 1 1 calc(24% - 20px) !important;
            min-width: 160px !important;
            box-sizing: border-box;
        }

        /* ---- 響應式斷點：當視窗寬度 ≤ 900px 時，三欄改為垂直堆疊 ---- */
        @media (max-width: 900px) {
            .flex-col-third, .weekly-col, .rcv-col, .used-col {
                flex: 1 1 100% !important;
                min-width: 0 !important;
                max-width: 100% !important;
            }
            .flex-col-left {
                max-width: 100% !important;
            }
            .flex-col-right {
                min-width: 0 !important;
                max-width: 100% !important;
            }
        }
        /* ---- 中型螢幕 (901px ~ 1200px)：三欄改為兩欄 + 一欄佈局 ---- */
        @media (min-width: 901px) and (max-width: 1200px) {
            .flex-col-third, .weekly-col, .rcv-col, .used-col {
                flex: 1 1 calc(50% - 20px) !important;
                min-width: 0 !important;
            }
        }

        /* ---- 卡片內容 flex 佈局 (避開 diagram.css 覆蓋的 .row/.col) ---- */
        .card-body-inner { padding: 15px; display: block; }
        .flex-row-layout {
            display: flex !important;
            flex-direction: row !important;
            flex-wrap: wrap;
            gap: 20px;
            align-items: flex-start;
            width: 100%;
            box-sizing: border-box;
        }
        .flex-col-left  { flex: 1 1 280px; min-width: 260px; max-width: 42%; box-sizing: border-box; }
        .flex-col-right { flex: 2 1 380px; min-width: 340px; max-width: 100%; box-sizing: border-box; overflow-x: auto; }
        /* Card 2 三欄：以百分比 flex-basis 搭配 calc 自動均分，小螢幕時退回 100% */
        .flex-col-third {
            flex: 1 1 calc(33.333% - 20px);
            min-width: 220px;
            max-width: 100%;
            box-sizing: border-box;
            overflow-x: auto;   /* 欄位過多時允許欄內水平捲動 */
        }

        /* ---- 儲量水位燈號 ---- */
        .limit-status-container { display: flex; align-items: center; justify-content: center; gap: 15px; padding: 8px 0; flex-wrap: wrap; }
        .limit-status-title     { font-size: 16px; color: #004b97; font-weight: bold; }
        .limit-status-item      { display: flex; align-items: center; gap: 8px; }
        .limit-status-circle    { width: 20px; height: 20px; border-radius: 50%; border: 2px solid #4a76a8; }
        .limit-status-box       { border: 1px solid #a0a0a0; padding: 4px 10px; background-color: #fff; color: #333; }

        /* ---- Modal ---- */
        .modal-overlay {
            display: none;
            position: fixed; z-index: 9999; left: 0; top: 0; width: 100%; height: 100%;
            background-color: rgba(0,0,0,0.8);
        }
        .modal-content {
            background-color: #fefefe;
            margin: 3% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 95%; max-width: 1700px;
            border-radius: 10px;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            position: relative;
        }
        .close-btn {
            color: #aaa; float: right; font-size: 32px; font-weight: bold; cursor: pointer;
            position: absolute; right: 20px; top: 10px; z-index: 10000;
        }
        .close-btn:hover { color: black; }
        .btn-open-detail {
            background-color: #004b97; color: white; border: none; padding: 8px 15px;
            border-radius: 5px; font-size: 14px; font-weight: bold; cursor: pointer;
            margin-left: 20px; vertical-align: middle;
        }
        .btn-open-detail:hover { background-color: #003366; }
    </style>

    <script src="libs/echarts.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function() {

        // 統一所有圖表的標題樣式
        var commonTitleStyle = { color: '#003399', fontSize: 15, fontWeight: 'bold' };

        /* =========================================================
           圖表一：每週扁鋼胚增減與庫存量 (雙 Y 軸折線圖)
           ========================================================= */
        var chartDom_Weekly = document.getElementById('weeklyChart');
        if (chartDom_Weekly) {
            var myChart_Weekly = echarts.init(chartDom_Weekly);

            // 從 ASP.NET 後端綁定資料
            var xData_Weekly = <%= xAxisData_Weekly %>;
            var diffData = <%= seriesData_Diff %>;
            var stockData = <%= seriesData_Stock %>;

            var option_Weekly = {
                title: { text: '每日增減量與庫存量', left: 'center', textStyle: commonTitleStyle },
                tooltip: { trigger: 'axis' },
                legend: { data: ['增減量合計', '庫存量'], top: '12%' },
                grid: { left: '3%', right: '3%', bottom: '3%', top: '28%', containLabel: true },
                xAxis: { type: 'category', boundaryGap: false, data: xData_Weekly, axisLabel: { interval: 0 } },
                yAxis: [
                    { type: 'value', name: '增減量', position: 'left' },
                    { type: 'value', name: '庫存量 (%)', position: 'right', splitLine: { show: false }, axisLabel: { formatter: function (value) { return value.toFixed(1); } } }
                ],
                series: [
                    { name: '增減量合計', type: 'line', yAxisIndex: 0, data: diffData, itemStyle: { color: '#FF0000' } },
                    { name: '庫存量', type: 'line', yAxisIndex: 1, data: stockData, symbolSize: 8, color: '#000000',
                      itemStyle: {
                          color: function (params) {
                              // 庫存燈號變色邏輯：大於 85 紅燈，大於 70 黃燈，否則綠燈
                              var val = params.value;
                              if (val >= 85) return '#FF0000';
                              else if (val >= 70) return '#FFC000';
                              else return '#92D050';
                          }
                      },
                      lineStyle: { color: '#000000', width: 2 }
                    }
                ]
            };
            myChart_Weekly.setOption(option_Weekly);
        }

        /* =========================================================
           圖表二：今日入儲即時資訊 (單柱狀圖)
           ========================================================= */
        var chartDom_TodayRcv = document.getElementById('todayRcvChart');
        if (chartDom_TodayRcv) {
            var myChart_TodayRcv = echarts.init(chartDom_TodayRcv);
            var xData_TodayRcv = <%= xAxisData_TodayRcv %>;
            var yData_TodayRcv = <%= seriesData_TodayRcv %>;
            var option_TodayRcv = {
                title: { text: '今日入儲即時資訊', left: 'center', textStyle: commonTitleStyle },
                tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                grid: { left: '3%', right: '4%', bottom: '8%', top: '15%', containLabel: true },
                xAxis: { type: 'category', data: xData_TodayRcv, axisLabel: { interval: 0 } },
                yAxis: { type: 'value' },
                series: [{ name: '數量(PCS)', type: 'bar', data: yData_TodayRcv, barWidth: '40%', itemStyle: { color: '#FDB062' }, label: { show: true, position: 'top', color: '#000' } }]
            };
            myChart_TodayRcv.setOption(option_TodayRcv);
        }

        /* =========================================================
           圖表三：今日消耗即時資訊 (單柱狀圖)
           ========================================================= */
        var chartDom_TodayUsed = document.getElementById('todayUsedChart');
        if (chartDom_TodayUsed) {
            var myChart_TodayUsed = echarts.init(chartDom_TodayUsed);
            var xData_TodayUsed = <%= xAxisData_TodayUsed %>;
            var yData_TodayUsed = <%= seriesData_TodayUsed %>;
            var option_TodayUsed = {
                title: { text: '今日消耗即時資訊', left: 'center', textStyle: commonTitleStyle },
                tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                grid: { left: '3%', right: '4%', bottom: '8%', top: '15%', containLabel: true },
                xAxis: { type: 'category', data: xData_TodayUsed, axisLabel: { interval: 0 } },
                yAxis: { type: 'value' },
                series: [{ name: '數量(PCS)', type: 'bar', data: yData_TodayUsed, barWidth: '40%', itemStyle: { color: '#0000FF' }, label: { show: true, position: 'top', color: '#000' } }]
            };
            myChart_TodayUsed.setOption(option_TodayUsed);
        }

        /* =========================================================
           圖表四：扁鋼胚儲區庫存量 (與廠區底圖連動的柱狀圖)
           ========================================================= */
        var chartDom_Storage = document.getElementById('storageChart');
        if (chartDom_Storage) {
            var myChart_Storage = echarts.init(chartDom_Storage);
            var xData_Storage = <%= xAxisData_Storage %>;
            var yData_Storage = <%= seriesData_Storage %>;
            var option_Storage = {
                title: { text: '扁鋼胚儲區庫存量', left: 'center', textStyle: commonTitleStyle },
                tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                grid: { left: '3%', right: '4%', bottom: '3%', top: '15%', containLabel: true },
                xAxis: { type: 'category', data: xData_Storage, axisLabel: { show: false }, axisTick: { show: false } },
                yAxis: { type: 'value', max: function (value) { return Math.ceil(value.max / 10) * 10 + 10; } }, // Y軸最大值自動取 10 的倍數 + 10
                series: [{
                    type: 'bar', data: yData_Storage, barWidth: '40%',
                    itemStyle: {
                        color: function(params) {
                            // 動態長條圖顏色 (紅、橘、綠)
                            var value = params.value;
                            if (value >= 85) return '#FF0000';
                            else if (value >= 70) return '#FFA500';
                            else return '#32CD32';
                        },
                        borderColor: '#333', borderWidth: 1
                    },
                    label: {
                        show: true, position: 'top', backgroundColor: '#FFFFE0', borderColor: '#333', borderWidth: 1, padding: [4, 6], color: '#000',
                        formatter: function(params) {
                            if (params.name === '庫存量') return '庫存量' + params.value + '%';
                            else return params.name + '_' + params.value;
                        }
                    }
                }]
            };
            myChart_Storage.setOption(option_Storage);

            /* --- 初始化：替廠區底圖預先上色並填入數字 --- */
            var zoneIndexMap = {}; // 建立儲區名稱對應陣列 index 的字典，供雙向連動使用
            if (xData_Storage && yData_Storage) {
                xData_Storage.forEach(function(zoneName, i) {
                    zoneIndexMap['zone-' + zoneName] = i; // 紀錄名稱與位置 (例：zone-Y1 -> 0)
                    var val = yData_Storage[i];
                    // 設定全時熱力圖底色 (帶 0.7 透明度)
                    var bg = val >= 85 ? 'rgba(255, 0, 0, 0.7)' : (val >= 70 ? 'rgba(255, 165, 0, 0.7)' : 'rgba(50, 205, 50, 0.7)');
                    var boxes = document.querySelectorAll('.zone-' + zoneName);
                    boxes.forEach(function(bx) {
                        bx.style.backgroundColor = bg;
                        var lbl = bx.querySelector('.map-label');
                        if (lbl) lbl.innerText = val + '%'; // 把數字寫入地圖對應的 label 中
                    });
                });
            }

            /* --- 互動方向 1：滑鼠移入 ECharts 長條圖 → 高亮廠區底圖 --- */
            myChart_Storage.on('mouseover', function (params) {
                var boxes = document.querySelectorAll('.zone-' + params.name);
                boxes.forEach(function(box) {
                    box.style.borderColor = 'red'; // 畫上黑色實線邊框
                    box.style.opacity = '1';       // 透明度設為 1 (完全不透明)
                    box.style.zIndex = '30';       // 提高層級避免被旁邊區塊遮擋
                });
            });

            /* 滑鼠移出 ECharts 長條圖 → 復原廠區底圖 */
            myChart_Storage.on('mouseout', function (params) {
                var boxes = document.querySelectorAll('.zone-' + params.name);
                boxes.forEach(function(box) {
                    // 清空行內樣式，交還給預設 CSS
                    box.style.borderColor = '';
                    box.style.opacity = '';
                    box.style.zIndex = '';
                });
            });

            /* --- 互動方向 2：滑鼠移入廠區底圖 → 觸發 ECharts 長條圖 Tooltip --- */
            document.querySelectorAll('.zone-box').forEach(function(box) {
                box.addEventListener('mouseenter', function() {
                    var classes = this.className.split(' ');
                    var targetClass = null;

                    // 找出該區塊屬於哪一個儲區 (例如 'zone-Y1')
                    for(var i=0; i<classes.length; i++) {
                        if(classes[i].indexOf('zone-') === 0 && classes[i] !== 'zone-box') {
                            targetClass = classes[i];
                            break;
                        }
                    }

                    // 連動底圖：把同屬該區的其他區塊 (例如 Y1-part1 ~ part4) 一併打光
                    if (targetClass) {
                        var relatedBoxes = document.querySelectorAll('.' + targetClass);
                        relatedBoxes.forEach(function(b) {
                            b.style.borderColor = 'red';
                            b.style.opacity = '1';
                            b.style.zIndex = '30';
                        });
                    }

                    // 觸發 ECharts API，顯示對應長條圖的 Tooltip 與高亮效果
                    var idx = zoneIndexMap[targetClass];
                    if(idx !== undefined) {
                        myChart_Storage.dispatchAction({ type: 'showTip', seriesIndex: 0, dataIndex: idx });
                        myChart_Storage.dispatchAction({ type: 'highlight', seriesIndex: 0, dataIndex: idx });
                    }
                });

                // 滑鼠移出廠區底圖 → 取消打光與 ECharts Tooltip
                box.addEventListener('mouseleave', function() {
                    var classes = this.className.split(' ');
                    var targetClass = null;
                    for(var i=0; i<classes.length; i++) {
                        if(classes[i].indexOf('zone-') === 0 && classes[i] !== 'zone-box') {
                            targetClass = classes[i];
                            break;
                        }
                    }
                    if (targetClass) {
                        var relatedBoxes = document.querySelectorAll('.' + targetClass);
                        relatedBoxes.forEach(function(b) {
                            b.style.borderColor = '';
                            b.style.opacity = '';
                            b.style.zIndex = '';
                        });
                    }
                    // 通知 ECharts 隱藏 Tooltip
                    myChart_Storage.dispatchAction({ type: 'hideTip' });
                    myChart_Storage.dispatchAction({ type: 'downplay' });
                });
            });
        }

        // 當瀏覽器視窗大小改變時，重新繪製所有圖表以符合新寬度
        window.addEventListener('resize', function() {
            if (typeof myChart_Weekly !== 'undefined') myChart_Weekly.resize();
            if (typeof myChart_TodayRcv !== 'undefined') myChart_TodayRcv.resize();
            if (typeof myChart_TodayUsed !== 'undefined') myChart_TodayUsed.resize();
            if (typeof myChart_Storage !== 'undefined') myChart_Storage.resize();
        });
    });

    /* =========================================================
       Modal 與 混合比例詳細分佈圖 控制邏輯
       ========================================================= */
    var detailChart = null;
    var isDetailChartInit = false;

    // 開啟彈出視窗
    function openDetailModal() {
        document.getElementById('detailModal').style.display = 'block';
        if (!isDetailChartInit) {
            initDetailChart(); // 首次打開才進行初始化
            isDetailChartInit = true;
        } else {
            // ECharts 在 display:none 的容器中寬度會計算錯誤，所以顯示後必須手動呼叫 resize
            setTimeout(function() { detailChart.resize(); }, 100);
        }
    }

    // 關閉彈出視窗
    function closeDetailModal() {
        document.getElementById('detailModal').style.display = 'none';
    }

    // 初始化詳細分佈圖 (堆疊百分比長條圖)
    function initDetailChart() {
        var dom = document.getElementById('detailDestinationChart');
        detailChart = echarts.init(dom);

        // 將後端組好的原始 PCS 陣列存入物件，供後續 label 讀取真實數字
        var rawData = {
            hr: <%= js_Raw_HotRoll %>,
            rd: <%= js_Raw_Ready %>,
            wt: <%= js_Raw_Wait %>,
            hv: <%= js_Raw_Heavy %>,
            rt: <%= js_Raw_Return %>,
            ts: <%= js_Raw_Test %>,
            em: <%= js_Raw_Empty %>
        };

        var option = {
            tooltip: {
                trigger: 'item',
                formatter: function(p) {
                    // Tooltip 客製化：抓取對應的原始 PCS 資料顯示
                    var keys = ['hr', 'rd', 'wt', 'hv', 'rt', 'ts', 'em'];
                    var val = rawData[keys[p.seriesIndex]][p.dataIndex];
                    if (val == 0) return ''; // 數值為 0 則不顯示
                    return '<b>' + p.name + '</b><br/>' + p.seriesName + ': <b style="color:#003399">' + val + ' PCS</b> (' + p.value + '%)';
                }
            },
            grid: { left: '22%', right: '5%', bottom: '5%', top: '5%', containLabel: false },
            xAxis: { type: 'value', max: 100, show: false }, // X 軸為百分比，最大固定 100，不顯示刻度
            yAxis: {
                type: 'category',
                inverse: true, // 反轉 Y 軸，讓「總計」顯示在最上方
                axisLine: { show: false },
                axisTick: { show: false },
                data: ['總計', '中龍熱軋', '中鴻胚','外售船胚', '中龍備胚','外售內銷', '中鋼胚','待回線', '其他'],
                axisLabel: {
                    formatter: function (value, index) {
                        // 自訂 Y 軸標籤：利用 ECharts Rich Text 排版成表格樣式
                             var codes = ['-', '1','Y','G、K、X','J','W','C、E、F、P', 'B、8', '上述以外'];
                        if (index === 0) return '{total|總計 (儲量水位)}';
                        return '{col1|去向碼} {col2|' + codes[index] + '} {col3|' + value + '}';
                    },
                    rich: {
                        col1: { width: 70, align: 'center', backgroundColor: '#eee', borderColor: '#000', borderWidth: 1, padding: [4, 0] },
                        col2: { width: 100, align: 'center', backgroundColor: '#fff', borderColor: '#000', borderWidth: 1, padding: [4, 0] },
                        col3: { width: 70, align: 'center', fontWeight: 'bold' },
                        total: { width: 240, align: 'right', fontWeight: 'bold', fontSize: 16, color: '#003399', padding: [0, 10, 0, 0] }
                    }
                }
            },
            series: [
                // === 以下為堆疊長條圖的各個區塊定義 ===
                // 加入 emphasis 設定讓滑鼠停靠時凸顯整個系列
                {
                    name: '熱軋軋延', type: 'bar', stack: 'total', barWidth: 32,
                    itemStyle: { color: '#FFFF00', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_HotRoll %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.hr[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            if (p.value < 15) return '{detail|' + val + ' PCS}';
                            return '{title|熱軋軋延}  {detail|' + val + ' PCS}  {desc|(排定軋延)}';
                        },
                        rich: {
                            title: { color: '#000', fontSize: 13 },
                            detail: { color: '#000', fontSize: 14, fontWeight: 'bold' },
                            desc: { color: '#333', fontSize: 12 }
                        }
                    }
                },
                {
                    name: '已可外搬(售)', type: 'bar', stack: 'total',
                    itemStyle: { color: '#92D050', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Ready %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.rd[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            var remark = (p.dataIndex === 1 || p.dataIndex === 5) ? '天數≧4天, 重量<27t' : '天數≧5天, 重量<27t';
                            if (p.dataIndex === 0) remark = '天數達標, 重量<27t';
                            if (p.value < 18) return '{detail|' + val + ' PCS}';
                            return '{title|已可外搬(售)}  {detail|' + val + ' PCS}  {desc|(' + remark + ')}';
                        },
                        rich: {
                            title: { color: '#000', fontSize: 13 },
                            detail: { color: '#000', fontSize: 14, fontWeight: 'bold' },
                            desc: { color: '#333', fontSize: 12 }
                        }
                    }
                },
                {
                    name: '等候外搬(售)', type: 'bar', stack: 'total',
                    itemStyle: { color: '#FFC000', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Wait %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.wt[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            var remark = (p.dataIndex === 1 || p.dataIndex === 5) ? '天數<4天, 重量<27t' : '天數<5天, 重量<27t';
                            if (p.dataIndex === 0) remark = '天數未達標, 重量<27t';
                            if (p.value < 18) return '{detail|' + val + ' PCS}';
                            return '{title|等候外搬(售)}  {detail|' + val + ' PCS}  {desc|(' + remark + ')}';
                        },
                        rich: {
                            title: { color: '#000', fontSize: 13 },
                            detail: { color: '#000', fontSize: 14, fontWeight: 'bold' },
                            desc: { color: '#333', fontSize: 12 }
                        }
                    }
                },
                {
                    name: '不可外搬鋼胚', type: 'bar', stack: 'total',
                    itemStyle: { color: '#FF0000', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Heavy %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.hv[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            if (p.value < 15) return '{detail|' + val + ' PCS}';
                            return '{title|重胚 / 不可外搬}  {detail|' + val + ' PCS}  {desc|(重量≧27t)}';
                        },
                        rich: {
                            title: { color: '#FFFF00', fontSize: 13 },
                            detail: { color: '#FFFF00', fontSize: 14, fontWeight: 'bold' },
                            desc: { color: '#FFFF00', fontSize: 12 }
                        }
                    }
                },
                {
                    name: '待回線鋼胚', type: 'bar', stack: 'total',
                    itemStyle: { color: '#800080', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Return %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.rt[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            if (p.value < 15) return '{detail|' + val + ' PCS}';
                            return '{title|待回線}  {detail|' + val + ' PCS}';
                        },
                        rich: {
                            title: { color: '#FFFF00', fontSize: 13 },
                            detail: { color: '#FFFF00', fontSize: 14, fontWeight: 'bold' }
                        }
                    }
                },
                {
                    name: '試驗/暫留/無主等......', type: 'bar', stack: 'total',
                    itemStyle: { color: '#00BFFF', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Test %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.ts[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            if (p.value < 15) return '{detail|' + val + ' PCS}';
                            return '{title|試驗 / 暫留 / 無主等......}  {detail|' + val + ' PCS}';
                        },
                        rich: {
                            title: { color: '#000', fontSize: 13 },
                            detail: { color: '#000', fontSize: 14, fontWeight: 'bold' }
                        }
                    }
                },
                {
                    name: '剩餘空間', type: 'bar', stack: 'total',
                    itemStyle: { color: '#FFFFFF', borderColor: '#000', borderWidth: 1 },
                    emphasis: { focus: 'series', itemStyle: { borderWidth: 2, borderColor: '#000', shadowBlur: 10, shadowColor: 'rgba(0,0,0,0.5)' } },
                    data: <%= js_Pct_Empty %>,
                    label: {
                        show: true, position: 'inside', overflow: 'hidden',
                        formatter: function(p) {
                            var val = rawData.em[p.dataIndex];
                            if (val == 0 || p.value < 5) return '';
                            if (p.value < 15) return '{detail|' + val + ' PCS}';
                            return '{title|剩餘空間}  {detail|' + val + ' PCS}';
                        },
                        rich: {
                            title: { color: '#000', fontSize: 13 },
                            detail: { color: '#000', fontSize: 14, fontWeight: 'bold' }
                        }
                    }
                }
            ]
        };
        detailChart.setOption(option);

        // --- 新增：綁定滑鼠懸停事件，讓同一種顏色的長條圖同步凸顯 ---
        detailChart.on('mouseover', function (params) {
            detailChart.dispatchAction({
                type: 'highlight',
                seriesIndex: params.seriesIndex
            });
        });

        detailChart.on('mouseout', function (params) {
            detailChart.dispatchAction({
                type: 'downplay',
                seriesIndex: params.seriesIndex
            });
        });
    }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />

    <div class="container-fluid main-content px-4">

        <%-- ============================================================
             Card 1：廠區圖 + 儲區庫存柱狀圖 + 水位燈號 + 詳細資料表
             ============================================================ --%>
        <div class="card-custom mb-4 mt-2">
            <div class="card-header-custom">
                <span class="fs-4">📦 3601 扁鋼胚儲區庫存量</span>
                <span class="badge bg-warning text-dark fs-6 shadow-sm" id="headerUpdateTime"></span>
            </div>
            <div class="card-body-inner">
                <div class="flex-row-layout">

                    <%-- 左欄：廠區底圖 --%>
                    <div class="flex-col-left">
                        <div class="map-container">
                            <img src="images/SYMClayout_V1.png" alt="儲區配置圖" />
                            <div id="box-Y1-part1" class="zone-box zone-Y1"></div>
                            <div id="box-Y1-part2" class="zone-box zone-Y1"><span class="map-label"></span></div>
                            <div id="box-Y1-part3" class="zone-box zone-Y1"><span class="map-label"></span></div>
                            <div id="box-Y1-part4" class="zone-box zone-Y1"></div>
                            <div id="box-Y2" class="zone-box zone-Y2"><span class="map-label"></span></div>
                            <div id="box-Y3" class="zone-box zone-Y3"><span class="map-label"></span></div>
                            <div id="box-Y3-part1" class="zone-box zone-Y3"></div>
                            <div id="box-Y4" class="zone-box zone-Y4"><span class="map-label"></span></div>
                            <div id="box-HC保溫坑" class="zone-box zone-HC保溫坑"><span class="map-label"></span></div>
                        </div>
                    </div>

                    <%-- 右欄：庫存柱狀圖 + 水位燈號 + 詳細數據表 --%>
                    <div class="flex-col-right">
                        <div id="storageChart" style="width: 100%; height: 260px;"></div>

                        <div class="limit-status-container" style="margin: 8px 0;">
                            <div class="limit-status-title">儲量水位
                                <button type="button" class="btn-open-detail" onclick="openDetailModal()">&#128269; 檢視各去向鋼胚明細</button>
                            </div>
                            <div class="limit-status-item">
                                <div class="limit-status-circle" style="background-color: #92D050;"></div>
                                <div class="limit-status-box">&le;3900 PCS(70%)</div>
                            </div>
                            <div class="limit-status-item">
                                <div class="limit-status-circle" style="background-color: #FFC000;"></div>
                                <div class="limit-status-box">3900-4760 PCS(70-85%)</div>
                            </div>
                            <div class="limit-status-item">
                                <div class="limit-status-circle" style="background-color: #FF0000;"></div>
                                <div class="limit-status-box">&ge;4760 PCS(85%)</div>
                            </div>
                        </div>

                        <div style="overflow-x: auto;">
                            <asp:GridView ID="gvStock" runat="server" GridLines="None" CssClass="bs5-table">
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%-- ============================================================
             Card 2：今日入儲 / 每日增減趨勢 / 今日消耗  (三欄並排)
             ============================================================ --%>
        <div class="card-custom mb-5">
            <div class="card-header-custom">
                <span class="fs-4">📊 今日入儲 / 每日增減趨勢 / 今日消耗 即時資訊</span>
            </div>
            <div class="card-body-inner">
                <%-- 三欄並排：使用獨立 flex 容器，確保不同解析度下自動換行 --%>
                <div class="flex-row-layout" style="gap: 15px;">

                    <%-- 左欄：今日入儲（兩欄表格，自適應寬度置中對齊圖表） --%>
                    <div class="flex-col-third rcv-col">
                        <div id="todayRcvChart" style="width: 100%; height: 240px; min-width: 0;"></div>
                        <div class="table-wrap" style="margin-top: 8px;">
                            <asp:GridView ID="GridView4" runat="server" DataSourceID="dsImport" GridLines="None" CssClass="bs5-table">
                            </asp:GridView>
                        </div>
                    </div>

                    <%-- 中欄：每日增減趨勢（給中欄更寬的 flex 比例，小字型縮排，確保 1920px 下免除 scrollbar） --%>
                    <div class="flex-col-third weekly-col">
                        <div id="weeklyChart" style="width: 100%; height: 240px; min-width: 0;"></div>
                        <div class="table-wrap" style="margin-top: 8px; justify-content: flex-start;">
                            <%-- 中欄表格欄位多，justify-content 改 flex-start 讓表格靠左從頭顯示 --%>
                            <asp:GridView ID="GridView5" runat="server" DataSourceID="dsWeekly" GridLines="None" CssClass="bs5-table">
                            </asp:GridView>
                        </div>
                    </div>

                    <%-- 右欄：今日消耗（兩欄表格，自適應寬度置中對齊圖表） --%>
                    <div class="flex-col-third used-col">
                        <div id="todayUsedChart" style="width: 100%; height: 240px; min-width: 0;"></div>
                        <div class="table-wrap" style="margin-top: 8px;">
                            <asp:GridView ID="GridView6" runat="server" DataSourceID="dsExport" GridLines="None" CssClass="bs5-table">
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>

    <%-- ============================================================
         Modal：鋼胚去向詳細分佈圖 (完整保留)
         ============================================================ --%>
    <div id="detailModal" class="modal-overlay">
        <div class="modal-content">
            <span class="close-btn" onclick="closeDetailModal()">&times;</span>
            <h2 style="text-align: center; color: #003399; margin-top: 0; margin-bottom: 10px;">鋼胚去向詳細分佈圖</h2>

            <div style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap; background-color: #f4f7f9; padding: 10px 20px; border: 1px solid #d0d0d0; border-radius: 8px; margin-bottom: 10px; font-size: 14px;">
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #FFFF00; border: 1px solid #000;"></span>
                    <b>熱軋軋延</b>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #92D050; border: 1px solid #000;"></span>
                    <b>已可外搬(售)</b> <span style="color: #555;">(重量&lt;27t 且 天數達標)</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #FFC000; border: 1px solid #000;"></span>
                    <b>等候外搬(售)</b> <span style="color: #555;">(重量&lt;27t 且 天數未達標)</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #FF0000; border: 1px solid #000;"></span>
                    <b>重胚/不可外搬</b> <span style="color: #555;">(重量≧27t)</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #800080; border: 1px solid #000;"></span>
                    <b>待回線</b>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <span style="display: inline-block; width: 16px; height: 16px; background-color: #00BFFF; border: 1px solid #000;"></span>
                    <b>試驗/暫留/無主......</b>
                </div>
                <div style="font-size: 12px; color: #888; width: 100%; text-align: center; margin-top: 5px;">
                    * 備註：外售船胚、中龍備胚之天數達標為 <b>≧4天</b>；外售內銷、中鴻胚、中鋼胚之天數達標為 <b>≧5天</b>。
                </div>
            </div>

            <div id="detailDestinationChart" style="width: 100%; height: 500px;"></div>
        </div>
    </div>

    <%-- SqlDataSources (完整保留，不異動) --%>
    <asp:SqlDataSource ID="dsImport" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_rcvCount_today" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="dsImport_forteechart" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_rcvCount_today_Teechartues" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="dsWeekly_forteechart" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_TotalCount_weekly_Teechartuse" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="GetWeeklyInventoryReport" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="usp_GetWeeklyInventoryReport" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="dsExport" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_UsedCount_today" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="dsExport_forteechart" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_UsedCount_today_Teechartues" SelectCommandType="StoredProcedure" />
    <asp:SqlDataSource ID="dsWeekly" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="GetSlab_TotalCount_weekly" SelectCommandType="StoredProcedure" />

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>"
        SelectCommand="SELECT TOP 1
    'Y1' + '_' + CAST(ROUND(CAST(y1_orate / 10.0 AS float), 2) AS varchar) AS Y1,
    ROUND(CAST(y1_orate / 10.0 AS float), 2) AS y1_orate,
    'Y2' + '_' + CAST(ROUND(CAST(y2_orate / 10.0 AS float), 2) AS varchar) AS Y2,
    ROUND(CAST(y2_orate / 10.0 AS float), 2) AS y2_orate,
    'Y3' + '_' + CAST(ROUND(CAST(y3_orate / 10.0 AS float), 2) AS varchar) AS Y3,
    ROUND(CAST(y3_orate / 10.0 AS float), 2) AS y3_orate,
    'Y4' + '_' + CAST(ROUND(CAST(y4_orate / 10.0 AS float), 2) AS varchar) AS Y4,
    ROUND(CAST(y4_orate / 10.0 AS float), 2) AS y4_orate,
    'HC保溫坑' + '_' + CAST(ROUND(CAST(hc_orate / 10.0 AS float), 2) AS varchar) AS HC,
    ROUND(CAST(hc_orate / 10.0 AS float), 2) AS hc_orate,
    '庫存量' + CAST(ROUND(CAST(total_stock_num AS float) / 5600.0, 2)*100 AS varchar)+'%' AS total_stock_num,
    ROUND(CAST(total_stock_num AS float) / 5600.0, 2)*100 AS total_stock_num_left
    FROM h_pmis_isyh WITH (NOLOCK) ORDER BY process_date DESC" />

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>"
        SelectCommand="SELECT TOP 1
    ROUND(CAST(y1_orate / 10.0 AS float), 2) AS y1_orate,
    ROUND(CAST(y2_orate / 10.0 AS float), 2) AS y2_orate,
    ROUND(CAST(y3_orate / 10.0 AS float), 2) AS y3_orate,
    ROUND(CAST(y4_orate / 10.0 AS float), 2) AS y4_orate,
    ROUND(CAST(hc_orate / 10.0 AS float), 2) AS hc_orate,
    ROUND(CAST(total_stock_num AS float) / 5600.0, 3)*100 AS total_stock_num_left
    FROM h_pmis_isyh WITH (NOLOCK) ORDER BY process_date DESC" />

    </form>

    <script src="libs/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        // 自動填入資料更新時間標籤
        (function () {
            var now = new Date();
            var y = now.getFullYear();
            var m = ('0' + (now.getMonth() + 1)).slice(-2);
            var d = ('0' + now.getDate()).slice(-2);
            var hh = ('0' + now.getHours()).slice(-2);
            var mm = ('0' + now.getMinutes()).slice(-2);
            var el = document.getElementById('headerUpdateTime');
            if (el) el.textContent = '資料更新時間：' + y + '/' + m + '/' + d + ' ' + hh + ':' + mm;
        })();
    </script>
</body>
</html>
