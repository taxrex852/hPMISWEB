<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3701.aspx.vb" Inherits="hPMISWEB._3701" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>熱軋全場固定式CO偵測圖</title>
    <style type="text/css">
        /* ==========================================================================
           🎨 現代化戰情室全球樣式變數 (白底清爽質感風格)
           ========================================================================== */
        :root {
            --bg-light: #ffffff;
            --card-bg: #f8fafc;
            --border-color: #cbd5e1;
            --text-main: #334155;
            --text-muted: #64748b;
            --safe-blue: #2563eb;    /* 🛠️ 配合安全狀態改為標準藍色 */
            --danger-red: #ef4444;   /* 危險警報標準紅色 */
            --warn-orange: #f59e0b;
        }

        body {
            background-color: var(--bg-light);
            color: var(--text-main);
            font-family: "Helvetica Neue", Helvetica, "Microsoft JhengHei", sans-serif;
            margin: 0;
            padding: 20px;
        }

        /* 讓最外層容器負責整個頁面的水平置中 */
        .page-center-wrapper {
            display: flex;
            justify-content: center; /* 水平置中 */
            align-items: flex-start;
            width: 100%;
            margin-top: 20px;
        }

        /* 戰情室內層雙欄彈性佈局 */
        .dashboard-container {
            display: flex;
            flex-direction: row; /* 橫向並排 */
            gap: 30px; /* 左右邊欄與主圖的間距 */
            width: 100%;
            max-width: 1500px; /* 限制最大寬度 */
            justify-content: center; /* 內容在中間靠攏 */
            align-items: flex-start;
        }

        /* ==========================================================================
           🖼️ 左側：廠區地圖與動態覆蓋圖層
           ========================================================================== */
        .map-wrapper {
            position: relative;
            flex: 1; /* 自動分配空間 */
            min-width: 600px; /* 限制最小寬度 */
            max-width: 900px; /* 限制最大寬度 */
            background-color: #ffffff;
            border-radius: 12px;
            padding: 12px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
            border: 1px solid var(--border-color);
        }

        .base-factory-png {
            width: 100%;
            height: auto;
            display: block;
            border-radius: 8px;
        }

        /* 數位互動熱點 (Hotspot) 基底 */
        .interactive-hotspot {
            position: absolute;
            cursor: pointer;
            z-index: 5;
        }

        /* 粗框設計的核心覆蓋圖層 (完全不加 background-color) */
        .hotspot-overlay {
            width: 100%;
            height: 100%;
            border-radius: 6px;
            border: 3px solid #94a3b8; /* 🛠️ 預設改為實線粗框 */
            background-color: transparent !important; /* ❌ 捨去色塊，完全透明 */
            transition: all 0.3s ease;
            box-sizing: border-box; /* 確保粗框不會撐大原範圍 */
        }

        /* 滑鼠移入熱點時的微微放大特效 */
        .interactive-hotspot:hover .hotspot-overlay {
            transform: scale(1.02);
        }

        /* ==========================================================================
           💬 現代化懸浮卡片 (Tooltip) -> 移至右側，且確保不被擋住
           ========================================================================== */
        .modern-tooltip {
            position: absolute;
            top: 50%;
            left: 108%; /* 從右側彈出 */
            transform: translateY(-50%) translateX(-10px);
            background-color: rgba(30, 41, 59, 0.98); /* 保持深色以利清晰對比 */
            border: 1px solid rgba(255, 255, 255, 0.15);
            border-radius: 8px;
            padding: 12px;
            width: 220px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.3);
            opacity: 0;
            visibility: hidden;
            transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1);
            z-index: 999; /* 🛠️ 提高層級，確保絕對在最上層 */
            pointer-events: none;
            color: #ffffff;
        }

        .modern-tooltip h3 {
            margin: 0 0 10px 0;
            font-size: 1rem;
            color: #ffffff;
            border-bottom: 1px solid rgba(255, 255, 255, 0.2);
            padding-bottom: 6px;
        }

        .tooltip-row {
            display: flex;
            justify-content: space-between;
            font-size: 0.85rem;
            margin-bottom: 6px;
            color: #e2e8f0;
        }

        .tooltip-row .highlight {
            color: #38bdf8;
            font-weight: bold;
        }

        /* 滑鼠移入熱點時，右側 Tooltip 滑順浮現 */
        .interactive-hotspot:hover .modern-tooltip {
            opacity: 1;
            visibility: visible;
            transform: translateY(-50%) translateX(0); /* 動態推回原位 */
        }

        /* ==========================================================================
           📊 右側：數據監控看板欄
           ========================================================================== */
        .side-panel {
            width: 360px;
            display: flex;
            flex-direction: column;
            gap: 16px;
            flex-shrink: 0;
        }

        .data-card, .info-card {
            background-color: var(--card-bg);
            border-radius: 12px;
            border: 1px solid var(--border-color);
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.04);
        }

        .card-header {
            font-size: 1.1rem;
            font-weight: bold;
            border-bottom: 2px solid var(--border-color);
            padding-bottom: 10px;
            margin-bottom: 16px;
            color: var(--text-main);
            letter-spacing: 0.5px;
        }

        .metric-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 12px;
        }

        .metric-row:last-child {
            margin-bottom: 0;
        }

        .metric-row .label {
            color: var(--text-muted);
            font-size: 0.95rem;
        }

        .asp-display-box {
            background: transparent !important;
            border: none !important;
            color: inherit !important;
            text-align: right;
            font-weight: bold;
            font-size: 1rem;
            width: 140px;
            cursor: default;
            font-family: inherit;
        }

        .legend-title {
            font-size: 0.95rem;
            font-weight: bold;
            margin-bottom: 10px;
            color: var(--text-main);
        }

        .legend-item {
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 0.85rem;
            margin-bottom: 6px;
            color: var(--text-muted);
        }

        .badge {
            padding: 3px 8px;
            border-radius: 4px;
            font-weight: bold;
            font-size: 0.75rem;
        }
        .badge.blue { background-color: rgba(59, 130, 246, 0.1); color: #2563eb; border: 1px solid rgba(59, 130, 246, 0.3); }
        .badge.red { background-color: rgba(239, 68, 68, 0.1); color: #dc2626; border: 1px solid rgba(239, 68, 68, 0.3); }

        .compass-box {
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 16px;
            background-color: #ffffff;
            border-radius: 12px;
            margin-top: 10px;
            border: 1px solid var(--border-color);
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.04);
        }
        .compass-box img {
            width: 130px;
            height: auto;
        }

        /* ==========================================================================
           🚨 狀態自動驅動引擎 (修改：安全時用粗藍框、危險或中斷時用粗紅框)
           ========================================================================== */

        /* 🔴 情況一：環境危險 或 通訊中斷 -> 顯示實線「粗紅框」+ 呼吸發光效果 */
        [data-env="危險"], [data-status="中斷"] {
            border-color: rgba(239, 68, 68, 0.5) !important;
        }
        [data-env="危險"] .hotspot-overlay, [data-status="中斷"] .hotspot-overlay {
            border: 4px solid var(--danger-red) !important; /* 🛠️ 4px 實線粗紅框 */
            background-color: transparent !important;
            animation: pulse-danger-border 2s infinite; /* 觸發紅色呼吸發光 */
        }
        [data-env="危險"] .asp-display-box, [data-status="中斷"] .asp-display-box {
            color: #dc2626 !important;
        }

        /* 🔵 情況二：環境安全 且 通訊正常 -> 顯示實線「粗藍框」 */
        [data-env="安全"][data-status="正常"], [data-status="正常"] {
            border-color: rgba(37, 99, 235, 0.3);
        }
        [data-env="安全"][data-status="正常"] .hotspot-overlay, [data-status="正常"] .hotspot-overlay {
            border: 4px solid var(--safe-blue) !important; /* 🛠️ 4px 實線粗藍框 */
            background-color: transparent !important;
        }
        [data-env="安全"][data-status="正常"] .asp-display-box, [data-status="正常"] .asp-display-box {
            color: #2563eb !important;
        }

        /* 粗紅框專用的外發光呼吸動畫 */
        @keyframes pulse-danger-border {
            0% { box-shadow: 0 0 0 0 rgba(239, 68, 68, 0.5); }
            70% { box-shadow: 0 0 0 14px rgba(239, 68, 68, 0); }
            100% { box-shadow: 0 0 0 0 rgba(239, 68, 68, 0); }
        }

        /* ==========================================================================
           🖱️ Card Hover → 地圖熱點凸顯
           ========================================================================== */
        .interactive-hotspot.card-highlighted .hotspot-overlay {
            transform: scale(1.06);
            box-shadow: 0 0 0 5px rgba(37, 99, 235, 0.35), 0 0 24px rgba(37, 99, 235, 0.25);
        }
        [data-env="危險"].card-highlighted .hotspot-overlay,
        [data-status="中斷"].card-highlighted .hotspot-overlay {
            transform: scale(1.06);
            box-shadow: 0 0 0 5px rgba(239, 68, 68, 0.45), 0 0 24px rgba(239, 68, 68, 0.3) !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        
        <div class="page-center-wrapper">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Timer ID="TimerALL" runat="server" Interval="30000"></asp:Timer>

                    <div class="dashboard-container">
                        
                        <div class="map-wrapper">
                            <img src="images/Y6P2.jpg" id="IMG1" class="base-factory-png" alt="W4廠區圖" />
                            
                            <div class="interactive-hotspot"
                                  style="top: 72.2%; left: 28.2%; width: 10.2%; height: 14.2%;"
                                 data-zone="fce-hot" data-status='<%= TextBox7.Text %>' data-env='<%= TextBox6.Text %>'>
                                <div class="hotspot-overlay"></div>
                                <div class="modern-tooltip">
                                    <h3>熱軋加熱爐區域</h3>
                                    <div class="tooltip-row">CO 最大值：<span class="highlight"><asp:Label ID="fceppm" runat="server" /> ppm</span></div>
                                    <div class="tooltip-row">環境狀況：<span><%= TextBox6.Text %></span></div>
                                    <div class="tooltip-row">通訊狀況：<span><%= TextBox7.Text %></span></div>
                                </div>
                            </div>

                            <div class="interactive-hotspot"
                                     style="top: 42.8%; left: 17.8%; width: 10.5%; height: 12.3%;"
                                 data-zone="fce-beam" data-status='<%= TextBox14.Text %>' data-env='<%= TextBox13.Text %>'>
                                <div class="hotspot-overlay"></div>
                                <div class="modern-tooltip">
                                    <h3>型鋼加熱爐區域</h3>
                                    <div class="tooltip-row">CO 最大值：<span class="highlight"><asp:Label ID="hbmfceppm" runat="server" /> ppm</span></div>
                                    <div class="tooltip-row">環境狀況：<span><%= TextBox13.Text %></span></div>
                                    <div class="tooltip-row">通訊狀況：<span><%= TextBox14.Text %></span></div>
                                </div>
                            </div>
                        </div>

                        <div class="side-panel">
                            
                            <div class="info-card">
                                <div class="legend-title">CO 偵測值狀態規範</div>
                                <div class="legend-item"><span class="badge blue">&lt; 35 ppm</span> 安全狀態 (藍框)</div>
                                <div class="legend-item"><span class="badge red">&ge; 35 ppm</span> 危險警報狀態 (紅框)</div>
                            </div>

                            <div class="data-card" data-zone="fce-hot" data-status='<%= TextBox7.Text %>' data-env='<%= TextBox6.Text %>'>
                                <div class="card-header">熱軋加熱爐區域</div>
                                <div class="metric-row">
                                    <span class="label">CO 偵測最大值</span>
                                    <asp:TextBox ID="TextBox5" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                                <div class="metric-row">
                                    <span class="label">環境狀況</span>
                                    <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                                <div class="metric-row">
                                    <span class="label">通訊狀況</span>
                                    <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                            </div>

                            <div class="data-card" data-zone="fce-beam" data-status='<%= TextBox14.Text %>' data-env='<%= TextBox13.Text %>'>
                                <div class="card-header">型鋼加熱爐區域</div>
                                <div class="metric-row">
                                    <span class="label">CO 偵測最大值</span>
                                    <asp:TextBox ID="TextBox12" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                                <div class="metric-row">
                                    <span class="label">環境狀況</span>
                                    <asp:TextBox ID="TextBox13" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                                <div class="metric-row">
                                    <span class="label">通訊狀況</span>
                                    <asp:TextBox ID="TextBox14" runat="server" ReadOnly="True" CssClass="asp-display-box" />
                                </div>
                            </div>

                            <div class="compass-box">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/North.JPG" AlternateText="指北針方位" />
                            </div>
                        </div>

                    </div> <div style="display: none;">
                        <asp:TextBox ID="TextBox1" runat="server" />
                        <asp:TextBox ID="TextBox2" runat="server" />
                        <asp:TextBox ID="TextBox3" runat="server" />
                        <asp:TextBox ID="TextBox4" runat="server" />
                        <asp:TextBox ID="TextBox8" runat="server" />
                        <asp:TextBox ID="TextBox9" runat="server" />
                        <asp:TextBox ID="TextBox10" runat="server" />
                        <asp:TextBox ID="TextBox11" runat="server" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>

    <script type="text/javascript">
        (function () {
            function wireCardHighlight() {
                var cards = document.querySelectorAll('.data-card[data-zone]');
                for (var i = 0; i < cards.length; i++) {
                    (function (card) {
                        var zone = card.getAttribute('data-zone');
                        var hs = document.querySelector('.interactive-hotspot[data-zone="' + zone + '"]');
                        if (!hs) return;
                        card.addEventListener('mouseenter', function () { hs.classList.add('card-highlighted'); });
                        card.addEventListener('mouseleave', function () { hs.classList.remove('card-highlighted'); });
                    })(cards[i]);
                }
            }
            if (typeof Sys !== 'undefined' && Sys.WebForms) {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(wireCardHighlight);
            }
            window.addEventListener('load', wireCardHighlight);
        })();
    </script>
</body>
</html>