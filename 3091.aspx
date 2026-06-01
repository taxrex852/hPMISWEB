<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3091.aspx.vb" Inherits="hPMISWEB._3091" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>3091 工業級網路拓撲監控</title>
    <!-- D3.js v7 -->
    <script src="/libs/d3.min.js"></script>
    <style>
        body {
            background: #f1f5f9;
            margin: 0;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        .header {
            background: linear-gradient(135deg, #1e293b 0%, #334155 100%);
            color: #fff;
            padding: 16px 28px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            box-shadow: 0 2px 8px rgba(0,0,0,0.18);
        }
        .header h1 {
            margin: 0;
            font-size: 20px;
            font-weight: 700;
            letter-spacing: 1px;
        }
        .last-update {
            font-size: 13px;
            color: #94a3b8;
        }

        /* 拓撲圖容器 */
        #topologyChart {
            width: 100%;
            height: calc(100vh - 100px);
            background: #f8fafc;
            /* 重要：position 需設為 relative，使 tooltip absolute 定位正確 */
            position: relative;
            overflow: hidden;
        }

        /* 連接線 - 正常狀態 */
        .link {
            fill: none;
            stroke: #94a3b8;
            stroke-width: 2px;
        }
        /* 連接線 - 斷線狀態（虛線紅色流動） */
        .link.offline-link {
            stroke: #ef4444;
            stroke-width: 1.5px;
            stroke-dasharray: 6, 4;
            animation: flow 1s linear infinite;
        }

        /* 節點樣式 */
        .node rect {
            fill: #ffffff;
            stroke: #94a3b8;
            stroke-width: 2px;
            transition: all 0.4s ease;
        }
        .node text {
            font-family: "Segoe UI", Arial, sans-serif;
            font-size: 13px;
            font-weight: 700;
            fill: #334155;
            text-anchor: middle;
            dominant-baseline: middle;
            pointer-events: none;
        }

        /* 節點狀態：正常（綠色外框與發光） */
        .node.online rect {
            stroke: #10b981;
            fill: #f0fdf4;
            filter: drop-shadow(0 0 8px rgba(16, 185, 129, 0.4));
        }

        /* 節點狀態：異常（紅色外框、紅字與呼吸吸附效果） */
        .node.offline rect {
            stroke: #ef4444;
            fill: #fef2f2;
            animation: pulse-red 0.8s infinite alternate;
        }
        .node.offline text {
            fill: #b91c1c;
        }

        /* 動畫定義 */
        @keyframes flow {
            from { stroke-dashoffset: 10; }
            to   { stroke-dashoffset: 0; }
        }
        @keyframes pulse-red {
            0%   { filter: drop-shadow(0 0 2px rgba(239, 68, 68, 0.5)); }
            100% { filter: drop-shadow(0 0 12px rgba(239, 68, 68, 0.8)); }
        }

        /* =====================================================
           Tooltip 樣式
           position: fixed → 以瀏覽器可視區為基準，
           配合 event.clientX / event.clientY 計算位置，
           完全不受父容器 transform / scroll 影響。
        ===================================================== */
        #tooltip {
            position: fixed;
            padding: 10px 14px;
            font-size: 13px;
            line-height: 1.6;
            background: rgba(15, 23, 42, 0.92);
            color: #fff;
            border-radius: 6px;
            pointer-events: none;   /* 滑鼠穿透，不影響操作 */
            opacity: 0;
            transition: opacity 0.15s ease;
            z-index: 9999;
            white-space: nowrap;
            box-shadow: 0 4px 16px rgba(0,0,0,0.3);
        }
    </style>
</head>
<body>
    <!-- Tooltip 放在 body 最外層，避免被任何容器裁切 -->
    <div id="tooltip"></div>

    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />

        <div class="header">
            <h1>3091 / 工業級網路拓撲監控 (D3.js 樹狀架構)</h1>
            <div class="last-update">
                上次巡檢時間：<span id="lblTime">同步中...</span>
            </div>
        </div>

        <div id="topologyChart"></div>
    </form>

    <script>
        // =========================================================
        // 1. 定義多層式樹狀結構（完全符合 HOST -> HPMIS -> 15個現場設備）
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
            children: [
                {
                    id: "HPMIS",
                    name: "HPMIS",
                    children: fieldNodes.map(name => ({ id: name, name: name }))
                }
            ]
        };

        // =========================================================
        // 2. 設定 D3 畫布與響應式 viewBox
        //    加大 svgWidth 讓第三階層有足夠空間分散
        // =========================================================
        const svgWidth  = 1600;   // 加大寬度，讓 15 個節點有空間分散
        const svgHeight = 550;
        const rectW = 82;
        const rectH = 34;
        const PADDING_X = 80;    // 左右邊界留白，避免節點被截切

        const chartDiv = document.getElementById("topologyChart");

        const svg = d3.select("#topologyChart")
            .append("svg")
            .attr("viewBox", `0 0 ${svgWidth} ${svgHeight}`)
            .attr("preserveAspectRatio", "xMidYMid meet")
            .style("width", "100%")
            .style("height", "100%");

        // 支援滑鼠滾輪縮放與拖曳
        const g = svg.append("g");
        const zoom = d3.zoom()
            .scaleExtent([0.4, 3])
            .on("zoom", (event) => g.attr("transform", event.transform));
        svg.call(zoom);
        // 預設縮放位置微調（稍微向下平移，留出標題空間）
        svg.call(zoom.transform, d3.zoomIdentity.translate(0, 30).scale(1));

        // =========================================================
        // 3. 建立樹狀佈局（Tree Layout）
        //    留出 PADDING_X 讓最左/最右節點不被截切
        // =========================================================
        const treeLayout = d3.tree().size([svgWidth - PADDING_X * 2, svgHeight - 140]);
        const root = d3.hierarchy(treeData);
        treeLayout(root);

        // 將所有節點的 x 平移 PADDING_X，使左右對稱置中
        root.descendants().forEach(d => { d.x += PADDING_X; });

        // =========================================================
        // 4. 繪製連接線（Links），使用平滑貝茲曲線
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
        // 5. 繪製節點（Nodes）
        //    Tooltip 改用 event.clientX / clientY（相對視窗），
        //    搭配 position: fixed，確保不受容器 scroll/transform 影響
        // =========================================================
        const tooltip = d3.select("#tooltip");

        const node = g.selectAll(".node")
            .data(root.descendants())
            .enter().append("g")
            .attr("class", d => `node node-${d.data.id}`)
            .attr("transform", d => `translate(${d.x},${d.y})`)
            .on("mouseover", function(event, d) {
                // 根據狀態碼組合顯示文字
                let statusText;
                if (d.data.status === 'N') {
                    statusText = '✅ 正常 (Online)';
                } else if (d.data.status === 'E') {
                    statusText = '❌ 異常 (Offline)';
                } else {
                    statusText = '⏳ 未知';
                }

                tooltip
                    .html(`<strong>${d.data.id}</strong><br/>狀態：${statusText}`)
                    .style("opacity", 1);

                // 使用 clientX/clientY（相對於視窗），
                // tooltip 使用 position:fixed，位置完全精準
                positionTooltip(event);
            })
            .on("mousemove", function(event) {
                // 滑鼠移動時持續更新位置，確保 tooltip 跟隨游標
                positionTooltip(event);
            })
            .on("mouseout", function() {
                tooltip.style("opacity", 0);
            });

        // Tooltip 定位函式：顯示在滑鼠右方 15px
        function positionTooltip(event) {
            const offsetX = 15;  // 滑鼠右方偏移
            const offsetY = -10; // 微微向上對齊游標中心

            // 取得 tooltip 的實際寬高，避免超出視窗右側
            const tipNode = document.getElementById("tooltip");
            const tipW = tipNode.offsetWidth;
            const tipH = tipNode.offsetHeight;
            const vw = window.innerWidth;
            const vh = window.innerHeight;

            let left = event.clientX + offsetX;
            let top  = event.clientY + offsetY;

            // 如果右側放不下，改顯示在滑鼠左方
            if (left + tipW > vw - 10) {
                left = event.clientX - tipW - offsetX;
            }
            // 如果底部放不下，向上調整
            if (top + tipH > vh - 10) {
                top = vh - tipH - 10;
            }

            tooltip
                .style("left", left + "px")
                .style("top",  top  + "px");
        }

        // 節點矩形外框
        node.append("rect")
            .attr("width",  d => d.depth === 0 ? 100 : rectW)
            .attr("height", d => d.depth === 0 ? 44  : rectH)
            .attr("x",      d => d.depth === 0 ? -50 : -(rectW / 2))
            .attr("y",      d => d.depth === 0 ? -22 : -(rectH / 2))
            .attr("rx", 6)
            .attr("ry", 6);

        // 節點文字
        node.append("text")
            .text(d => d.data.name);

        // =========================================================
        // 6. 與後端 PING 溝通，非同步更新節點狀態
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
                    const timeStr = `${pad(now.getMonth() + 1)}/${pad(now.getDate())} ${pad(now.getHours())}:${pad(now.getMinutes())}:${pad(now.getSeconds())}`;
                    document.getElementById('lblTime').innerText = timeStr;

                    // 遍歷所有節點，直接使用 D3 切換 CSS Class
                    root.descendants().forEach(d => {
                        const sysId  = d.data.id.toUpperCase();
                        const status = statusDict[sysId];
                        d.data.status = status; // 存入 data 供 Tooltip 使用

                        if (status) {
                            const nodeEl = d3.select(`.node-${sysId}`);
                            const linkEl = d3.select(`.link-${sysId}`);

                            if (status === 'N') {
                                nodeEl.classed("online",  true ).classed("offline", false);
                                linkEl.classed("offline-link", false);
                            } else {
                                nodeEl.classed("online",  false).classed("offline", true);
                                // 如果該節點斷線，連接線也會變為警示虛線
                                linkEl.classed("offline-link", true);
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