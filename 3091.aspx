<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3091.aspx.vb" Inherits="hPMISWEB._3091" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>3091 HSM系統連線資訊</title>

    <!-- Bootstrap CSS（與 3101 等頁面一致） -->
    <link rel="stylesheet" href="libs/bootstrap.min.css" />
    <!-- D3.js v7 -->
    <script src="/libs/d3.min.js"></script>

    <style type="text/css">

        /* =====================================================
           基礎版面（與 3101 等頁面保持一致）
        ===================================================== */
        body {
            background-color: #f8f9fc;
            padding-bottom: 20px;
        }

        .main-content {
            clear: both !important;
            display: block !important;
            position: relative;
            padding-top: 20px;
        }

        /* =====================================================
           Card 設計（與 3101 完全一致）
        ===================================================== */
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

        .card-header-custom .badge-time {
            font-size: 12px;
            font-weight: 400;
            color: #94a3b8;
            background: rgba(255,255,255,0.08);
            border: 1px solid rgba(255,255,255,0.15);
            padding: 4px 12px;
            border-radius: 20px;
            white-space: nowrap;
        }

        /* 拓撲圖卡片內容區 */
        .topology-card-body {
            padding: 0;
            background-color: #f8fafc;
            height: 580px;
            position: relative;
            overflow: hidden;
        }

        /* =====================================================
           圖例說明列
        ===================================================== */
        .legend-bar {
            display: flex;
            align-items: center;
            gap: 24px;
            padding: 10px 20px;
            background: #fff;
            border-top: 1px solid #e3e6f0;
            font-size: 13px;
            color: #475569;
        }
        .legend-item {
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .legend-dot {
            width: 14px;
            height: 14px;
            border-radius: 3px;
            border: 2px solid;
        }
        .legend-dot.online  { border-color: #10b981; background: #f0fdf4; }
        .legend-dot.offline { border-color: #ef4444; background: #fef2f2; }
        .legend-dot.unknown { border-color: #94a3b8; background: #ffffff; }
        .legend-line {
            width: 30px;
            height: 2px;
        }
        .legend-line.normal  { background: #94a3b8; }
        .legend-line.broken  { background: #ef4444; border-top: 2px dashed #ef4444; height: 0; }

        /* =====================================================
           D3 節點與連接線
        ===================================================== */

        /* 連接線 - 預設（尚未取得狀態） */
        .link {
            fill: none;
            stroke: #cbd5e1;
            stroke-width: 2px;
        }
        /* 連接線 - 正常連線（綠色流動虛線，代表資料正常傳輸） */
        .link.online-link {
            stroke: #10b981;
            stroke-width: 2.5px;
            stroke-dasharray: 10, 5;
            animation: flow-green 1.2s linear infinite;
        }
        /* 連接線 - 斷線（紅色流動虛線） */
        .link.offline-link {
            stroke: #ef4444;
            stroke-width: 1.5px;
            stroke-dasharray: 6, 4;
            animation: flow-red 0.8s linear infinite;
        }

        /* 節點矩形外框 */
        .node rect {
            fill: #ffffff;
            stroke: #94a3b8;
            stroke-width: 2px;
            transition: all 0.35s ease;
        }
        /* 節點文字 */
        .node text {
            font-family: "Segoe UI", "Microsoft JhengHei", Arial, sans-serif;
            font-size: 12px;
            font-weight: 700;
            fill: #334155;
            text-anchor: middle;
            dominant-baseline: middle;
            pointer-events: none;
        }
        /* 游標懸停樣式 */
        .node:hover rect {
            filter: brightness(0.95);
            cursor: pointer;
        }

        /* 狀態：正常（綠色外框與發光） */
        .node.online rect {
            stroke: #10b981;
            fill: #f0fdf4;
            filter: drop-shadow(0 0 8px rgba(16, 185, 129, 0.4));
        }
        /* 狀態：異常（紅色呼吸） */
        .node.offline rect {
            stroke: #ef4444;
            fill: #fef2f2;
            animation: pulse-red 0.9s infinite alternate;
        }
        .node.offline text { fill: #b91c1c; }

        /* 動畫定義 */
        /* 正常連線：綠色往下流動（代表資料流向下層節點） */
        @keyframes flow-green {
            from { stroke-dashoffset: 15; }
            to   { stroke-dashoffset: 0;  }
        }
        /* 斷線：紅色往上流動（代表反向警示） */
        @keyframes flow-red {
            from { stroke-dashoffset: 10; }
            to   { stroke-dashoffset: 0;  }
        }
        @keyframes pulse-red {
            0%   { filter: drop-shadow(0 0 2px  rgba(239, 68, 68, 0.5)); }
            100% { filter: drop-shadow(0 0 14px rgba(239, 68, 68, 0.85)); }
        }

        /* =====================================================
           Tooltip
           position: fixed → 以瀏覽器可視區為基準，
           配合 event.clientX / clientY，完全不受任何容器影響
        ===================================================== */
        #topology-tooltip {
            position: fixed;
            padding: 10px 16px;
            font-size: 13px;
            line-height: 1.7;
            font-family: "Segoe UI", "Microsoft JhengHei", Arial, sans-serif;
            background: rgba(15, 23, 42, 0.93);
            color: #fff;
            border-radius: 6px;
            pointer-events: none;
            opacity: 0;
            transition: opacity 0.15s ease;
            z-index: 9999;
            white-space: nowrap;
            box-shadow: 0 4px 20px rgba(0,0,0,0.35);
            border-left: 4px solid #10b981;
        }
        #topology-tooltip.status-offline { border-left-color: #ef4444; }
        #topology-tooltip.status-unknown { border-left-color: #94a3b8; }

    </style>
</head>
<body>

    <!-- Tooltip 放在 body 最外層，避免被任何容器裁切或遮蓋 -->
    <div id="topology-tooltip"></div>

    <form id="form1" runat="server">
        <!-- PageHeader 元件（包含選單列，約 40~50px 高） -->
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <a name="#Home"></a>

        <!-- 主要內容區（與 3101 相同的 container-fluid main-content） -->
        <div class="container-fluid main-content px-4">

            <!-- ================================================
                 拓撲圖 Card（仿 3101 card-custom 設計）
            ================================================ -->
            <div class="card-custom mb-4 mt-2">

                <!-- Card 標題列 -->
                <div class="card-header-custom">
                    <span class="fs-5">
                        🌐 3091 HSM 系統網路監控
                    </span>
                    <span class="badge-time">
                        ⏱ 上次巡檢：<span id="lblTime">同步中...</span>
                    </span>
                </div>

                <!-- 拓撲圖本體 -->
                <div class="topology-card-body" id="topologyChart">
                    <!-- D3.js SVG 會自動注入此 div -->
                </div>

                <!-- 圖例說明列 -->
                <div class="legend-bar">
                    <span style="font-weight:600; color:#2c3e50;">圖例說明：</span>
                    <span class="legend-item">
                        <span class="legend-dot online"></span> 正常 (Online)
                    </span>
                    <span class="legend-item">
                        <span class="legend-dot offline"></span> 異常 (Offline)
                    </span>
                    <span class="legend-item">
                        <span class="legend-dot unknown"></span> 未知
                    </span>
                    <span class="legend-item">
                        <span class="legend-line normal"></span> 正常連線
                    </span>
                    <span class="legend-item">
                        <span class="legend-line broken"></span> 斷線（虛線）
                    </span>
                    <span style="margin-left:auto; font-size:12px; color:#94a3b8;">
                        💡 可使用滾輪縮放 / 拖曳平移
                    </span>
                </div>

            </div><!-- /.card-custom -->

        </div><!-- /.container-fluid -->
    </form>

    <!-- Bootstrap JS（與 3101 一致） -->
    <script src="libs/bootstrap.bundle.min.js"></script>

    <script>
        // =========================================================
        // 1. 定義多層式樹狀結構
        //    HOST → HPMIS → 15 個現場系統節點
        // =========================================================
        const fieldNodes = [
            "CYMC", "FCE", "HRFSPC", "HRS", "MIL",
            "PYMC", "SPC", "SYMC", "TG",
            "TNRL1", "TNRL2", "TNRL3", "TNRL4",
            "DYMC", "HBMMIL"
        ];

        const treeData = {
            id: "HOST",
            name: "HOST",
            children: [{
                id: "HPMIS",
                name: "HPMIS",
                children: fieldNodes.map(name => ({ id: name, name: name }))
            }]
        };

        // =========================================================
        // 2. 設定 D3 畫布尺寸
        //    svgWidth 加大確保 15 個第三階層節點有足夠間距
        // =========================================================
        const svgWidth   = 1700;    // 加大，讓 15 節點充分分散
        const svgHeight  = 520;
        const rectW      = 82;
        const rectH      = 34;
        const PADDING_X  = 60;      // 左右邊界留白，避免節點被截切

        const svg = d3.select("#topologyChart")
            .append("svg")
            .attr("viewBox", `0 0 ${svgWidth} ${svgHeight}`)
            .attr("preserveAspectRatio", "xMidYMid meet")
            .style("width",  "100%")
            .style("height", "100%");

        // 支援滾輪縮放與拖曳
        const g = svg.append("g");
        const zoom = d3.zoom()
            .scaleExtent([0.4, 3])
            .on("zoom", (event) => g.attr("transform", event.transform));
        svg.call(zoom);
        // 預設平移（稍微向下，避免 HOST 節點緊貼頂部）
        svg.call(zoom.transform, d3.zoomIdentity.translate(0, 35).scale(1));

        // =========================================================
        // 3. 建立樹狀佈局（Tree Layout）
        // =========================================================
        const treeLayout = d3.tree().size([svgWidth - PADDING_X * 2, svgHeight - 130]);
        const root = d3.hierarchy(treeData);
        treeLayout(root);

        // 全部節點的 x 座標平移 PADDING_X，讓畫面左右對稱
        root.descendants().forEach(d => { d.x += PADDING_X; });

        // =========================================================
        // 4. 繪製連接線（Links）— 平滑貝茲曲線
        // =========================================================
        g.selectAll(".link")
            .data(root.links())
            .enter().append("path")
            .attr("class", d => `link link-${d.target.data.id}`)
            .attr("d", d3.linkVertical()
                .x(d => d.x)
                .y(d => d.y)
            );

        // =========================================================
        // 5. 繪製節點（Nodes）與 Tooltip 互動
        //    Tooltip 使用 position:fixed + event.clientX/clientY
        //    確保位置精準跟隨滑鼠右方，不受容器 transform 影響
        // =========================================================
        const tooltip = d3.select("#topology-tooltip");

        const node = g.selectAll(".node")
            .data(root.descendants())
            .enter().append("g")
            .attr("class", d => `node node-${d.data.id}`)
            .attr("transform", d => `translate(${d.x},${d.y})`)
            .on("mouseover", function(event, d) {
                let statusText, cssClass;
                if (d.data.status === 'N') {
                    statusText = '✅ 正常 (Online)';
                    cssClass   = 'status-online';
                } else if (d.data.status === 'E') {
                    statusText = '❌ 異常 (Offline)';
                    cssClass   = 'status-offline';
                } else {
                    statusText = '⏳ 未知';
                    cssClass   = 'status-unknown';
                }

                // 組合 tooltip 內容，包含節點名稱、狀態、資料更新時間
                const updatedAt = d.data.lastUpdated || '－';
                tooltip
                    .attr("class", cssClass)
                    .html(
                        `<strong>${d.data.id}</strong><br/>` +
                        `狀態：${statusText}<br/>` +
                        `<span style="font-size:11px;color:#94a3b8;">🕐 更新時間：${updatedAt}</span>`
                    )
                    .style("opacity", 1);

                positionTooltip(event);
            })
            .on("mousemove", function(event) {
                // 持續更新位置，確保 tooltip 緊跟游標
                positionTooltip(event);
            })
            .on("mouseout", function() {
                tooltip.style("opacity", 0);
            });

        // Tooltip 定位：顯示在滑鼠右方 15px，超出視窗則自動反轉
        function positionTooltip(event) {
            const offsetX = 15;
            const offsetY = -8;
            const tipEl   = document.getElementById("topology-tooltip");
            const tipW    = tipEl.offsetWidth;
            const tipH    = tipEl.offsetHeight;
            const vw      = window.innerWidth;
            const vh      = window.innerHeight;

            let left = event.clientX + offsetX;
            let top  = event.clientY + offsetY;

            // 右側超出視窗 → 改顯示在左方
            if (left + tipW > vw - 10) {
                left = event.clientX - tipW - offsetX;
            }
            // 底部超出視窗 → 向上調整
            if (top + tipH > vh - 10) {
                top = vh - tipH - 10;
            }

            tooltip
                .style("left", left + "px")
                .style("top",  top  + "px");
        }

        // 節點矩形外框（HOST 層稍大）
        node.append("rect")
            .attr("width",  d => d.depth === 0 ? 104 : rectW)
            .attr("height", d => d.depth === 0 ? 44  : rectH)
            .attr("x",      d => d.depth === 0 ? -52 : -(rectW / 2))
            .attr("y",      d => d.depth === 0 ? -22 : -(rectH / 2))
            .attr("rx", 6)
            .attr("ry", 6);

        // 節點文字
        node.append("text")
            .text(d => d.data.name);

        // =========================================================
        // 6. 與後端 PING 服務溝通，非同步更新節點狀態
        // =========================================================
        function updateNetworkTopology() {
            fetch('3091.aspx/GetSystemStatus', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            })
                .then(res => res.json())
                .then(data => {
                    const statusDict = data.d;
                    const now = new Date();
                    const pad = n => String(n).padStart(2, '0');
                    document.getElementById('lblTime').innerText =
                        `${pad(now.getMonth()+1)}/${pad(now.getDate())} ` +
                        `${pad(now.getHours())}:${pad(now.getMinutes())}:${pad(now.getSeconds())}`;

                    // 遍歷所有節點，動態切換 CSS Class
                    root.descendants().forEach(d => {
                        const sysId  = d.data.id.toUpperCase();
                        const status = statusDict[sysId];
                        d.data.status = status;   // 存入 data 供 Tooltip 讀取

                        // 記錄本次查詢的時間戳記，供 Tooltip 顯示
                        const timeStr =
                            `${pad(now.getMonth()+1)}/${pad(now.getDate())} ` +
                            `${pad(now.getHours())}:${pad(now.getMinutes())}:${pad(now.getSeconds())}`;
                        if (status) {
                            d.data.lastUpdated = timeStr;   // 有回應才更新時間
                        }

                        if (status) {
                            const nodeEl = d3.select(`.node-${sysId}`);
                            const linkEl = d3.select(`.link-${sysId}`);

                            if (status === 'N') {
                                nodeEl.classed("online",  true ).classed("offline", false);
                                // 正常：加上綠色流動動畫，移除斷線樣式
                                linkEl.classed("online-link", true ).classed("offline-link", false);
                            } else {
                                nodeEl.classed("online",  false).classed("offline", true);
                                // 斷線：加上紅色虛線動畫，移除正常樣式
                                linkEl.classed("online-link", false).classed("offline-link", true);
                            }
                        }
                    });
                })
                .catch(err => console.error("D3.js 拓撲更新失敗:", err));
        }

        // 載入後立即執行一次，之後每 10 秒自動刷新
        document.addEventListener('DOMContentLoaded', () => {
            updateNetworkTopology();
            setInterval(updateNetworkTopology, 10000);
        });
    </script>
</body>
</html>