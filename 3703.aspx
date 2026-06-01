<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3703.aspx.vb" Inherits="hPMISWEB._3703" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>加熱爐固定式CO偵測圖</title>
    <style type="text/css">
        :root {
            --bg-light: #ffffff;
            --card-bg: #f8fafc;
            --border-color: #cbd5e1;
            --text-main: #334155;
            --text-muted: #64748b;
            --danger-red: #ef4444;
            --warn-orange: #f59e0b;
            --safe-blue: #2563eb;
        }
        body {
            background-color: var(--bg-light);
            color: var(--text-main);
            font-family: "Helvetica Neue", Helvetica, "Microsoft JhengHei", sans-serif;
            margin: 0;
            padding: 20px;
        }
        .page-center-wrapper {
            display: flex;
            justify-content: center;
            align-items: flex-start;
            width: 100%;
            margin-top: 20px;
        }
        .dashboard-container {
            display: flex;
            flex-direction: row;
            gap: 20px;
            width: 100%;
            max-width: 1600px;
            align-items: flex-start;
            justify-content: center;
        }

        /* ===== Map ===== */
        .map-wrapper {
            position: relative;
            width: 853px;
            flex-shrink: 0;
            background: #fff;
            border-radius: 12px;
            padding: 8px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
            border: 1px solid var(--border-color);
            overflow: visible;
        }
        .map-wrapper img.factory-img {
            width: 837px;
            height: 632px;
            display: block;
            border-radius: 6px;
        }
        .sensor-hotspot {
            position: absolute;
            cursor: pointer;
            z-index: 5;
        }
        .sensor-hotspot:hover {
            z-index: 1000;
        }
        .sensor-dot {
            display: inline-block;
            width: 22px;
            height: 22px;
            line-height: 22px;
            text-align: center;
            font-size: 8px;
            font-weight: bold;
            color: #fff;
            border: 1px solid rgba(0,0,0,0.3);
            border-radius: 3px;
            box-sizing: border-box;
        }

        /* ===== 現代化 Tooltip ===== */
        .sensor-tooltip {
            position: absolute;
            top: 50%;
            left: 130%;
            transform: translateY(-50%) translateX(-8px);
            background: rgba(15, 23, 42, 0.97);
            border: 1px solid rgba(255, 255, 255, 0.12);
            border-radius: 8px;
            padding: 10px 14px;
            width: 200px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.35);
            opacity: 0;
            visibility: hidden;
            transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1);
            z-index: 9999;
            pointer-events: none;
            color: #e2e8f0;
        }
        .sensor-hotspot:hover .sensor-tooltip {
            opacity: 1;
            visibility: visible;
            transform: translateY(-50%) translateX(0);
        }
        .sensor-hotspot.tip-left .sensor-tooltip {
            left: auto;
            right: 130%;
            transform: translateY(-50%) translateX(8px);
        }
        .sensor-hotspot.tip-left:hover .sensor-tooltip {
            transform: translateY(-50%) translateX(0);
        }
        .st-title {
            font-size: 0.88rem;
            font-weight: bold;
            color: #ffffff;
            border-bottom: 1px solid rgba(255, 255, 255, 0.15);
            padding-bottom: 6px;
            margin-bottom: 8px;
            letter-spacing: 0.3px;
            font-family: monospace;
        }
        .st-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 0.78rem;
            margin-bottom: 5px;
        }
        .st-row:last-child { margin-bottom: 0; }
        .st-lbl { color: #94a3b8; }
        .st-val { color: #38bdf8; font-weight: bold; }
        .st-status { font-weight: bold; }

        /* ===== Right section ===== */
        .right-section {
            display: flex;
            flex-direction: row;
            gap: 16px;
            align-items: flex-start;
            flex: 1;
            min-width: 0;
        }
        .main-data-col {
            display: flex;
            flex-direction: column;
            gap: 12px;
            width: 320px;
            flex-shrink: 0;
        }
        .info-col {
            display: flex;
            flex-direction: column;
            gap: 12px;
            width: 210px;
            flex-shrink: 0;
        }

        /* ===== Cards ===== */
        .data-card, .info-card {
            background: var(--card-bg);
            border-radius: 12px;
            border: 1px solid var(--border-color);
            padding: 14px 16px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.04);
        }
        .card-header {
            font-size: 0.95rem;
            font-weight: bold;
            border-bottom: 2px solid var(--border-color);
            padding-bottom: 8px;
            margin-bottom: 10px;
            color: var(--text-main);
        }
        .co-max-value {
            text-align: center;
            font-size: 2rem;
            font-weight: bold;
            padding: 8px 0;
            line-height: 1.1;
        }
        .co-max-unit {
            font-size: 0.9rem;
            font-weight: normal;
            color: var(--text-muted);
        }

        /* ===== Sensor table ===== */
        .sensor-tbl { width: 100%; border-collapse: collapse; }
        .sensor-tbl td { padding: 2px 3px; font-size: 0.75rem; vertical-align: middle; }
        .s-badge {
            display: inline-block;
            width: 20px; height: 20px; line-height: 20px;
            text-align: center; font-size: 8px; font-weight: bold;
            color: #fff;
            border: 1px solid rgba(0,0,0,0.2); border-radius: 3px;
        }
        .s-name { color: #f97316; font-family: monospace; font-size: 0.72rem; white-space: nowrap; }
        .s-val { font-weight: bold; min-width: 32px; }

        /* ===== Legend ===== */
        .legend-row { display: flex; align-items: center; gap: 8px; margin-bottom: 5px; font-size: 0.8rem; color: var(--text-muted); }
        .bdg { padding: 2px 7px; border-radius: 3px; font-weight: bold; font-size: 0.7rem; white-space: nowrap; }
        .bdg-b { background: rgba(37,99,235,0.1); color: #2563eb; border: 1px solid rgba(37,99,235,0.3); }
        .bdg-r { background: rgba(239,68,68,0.1); color: #dc2626; border: 1px solid rgba(239,68,68,0.3); }

        /* ===== Timestamp ===== */
        .time-row { font-size: 0.76rem; margin-bottom: 4px; display: flex; flex-direction: column; }
        .time-lbl { color: var(--text-muted); }
        .time-val { color: #2563eb; font-size: 0.73rem; }

        /* ===== Breathing light ===== */
        @keyframes pulse-danger {
            0%   { box-shadow: 0 0 0 0 rgba(239,68,68,0.8); }
            70%  { box-shadow: 0 0 0 10px rgba(239,68,68,0); }
            100% { box-shadow: 0 0 0 0 rgba(239,68,68,0); }
        }
        .pulse-danger { animation: pulse-danger 2s ease-in-out infinite; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <input id="wind_direction_E" runat="server" enableviewstate="true" name="wind_direction_E" type="hidden" />
    <hPMISWEB:PageHeader ID="ph" runat="server" />
    <div class="page-center-wrapper">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="10000"></asp:Timer>
                <input id="wind_direction_W" runat="server" enableviewstate="true" name="wind_direction_W" type="hidden" />

                <div class="dashboard-container">

                    <!-- ===== 廠區地圖 ===== -->
                    <div class="map-wrapper">
                        <img src="images/HBMFCE_3.jpg" class="factory-img" alt="HBM加熱爐廠區圖" />

                        <!-- #1 HBM FCE 感測器 1-10 (AT-2110~AT-2119) -->
                        <div class="sensor-hotspot tip-left" style="left:685px;top:163px;">
                            <asp:Label ID="P_1GIA2001" runat="server" CssClass="sensor-dot" Text="1" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2110</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2001" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot tip-left" style="left:650px;top:164px;">
                            <asp:Label ID="P_1GIA2002" runat="server" CssClass="sensor-dot" Text="2" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2111</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2002" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:487px;top:445px;">
                            <asp:Label ID="P_1GIA2101" runat="server" CssClass="sensor-dot" Text="3" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2112</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2101" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:443px;top:501px;">
                            <asp:Label ID="P_1GIA2102" runat="server" CssClass="sensor-dot" Text="4" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2113</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2102" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:456px;top:446px;">
                            <asp:Label ID="P_1GIA2103" runat="server" CssClass="sensor-dot" Text="5" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2114</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2103" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:466px;top:503px;">
                            <asp:Label ID="P_1GIA2104" runat="server" CssClass="sensor-dot" Text="6" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2115</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2104" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:495px;top:482px;">
                            <asp:Label ID="P_1GIA2105" runat="server" CssClass="sensor-dot" Text="7" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2116</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2105" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:470px;top:487px;">
                            <asp:Label ID="P_1GIA2106" runat="server" CssClass="sensor-dot" Text="8" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2117</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2106" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:402px;top:389px;">
                            <asp:Label ID="P_1GIA2107" runat="server" CssClass="sensor-dot" Text="9" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2118</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2107" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:461px;top:388px;">
                            <asp:Label ID="P_1GIA2108" runat="server" CssClass="sensor-dot" Text="10" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2119</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_1GIA2108" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>

                        <!-- #2 HBM FCE 感測器 11-20 (AT-2120~AT-2129) -->
                        <div class="sensor-hotspot" style="left:450px;top:220px;">
                            <asp:Label ID="P_2GIA2001" runat="server" CssClass="sensor-dot" Text="11" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2120</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2001" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:417px;top:401px;">
                            <asp:Label ID="P_2GIA2002" runat="server" CssClass="sensor-dot" Text="12" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2121</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2002" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:427px;top:203px;">
                            <asp:Label ID="P_2GIA2101" runat="server" CssClass="sensor-dot" Text="13" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2122</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2101" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:480px;top:223px;">
                            <asp:Label ID="P_2GIA2102" runat="server" CssClass="sensor-dot" Text="14" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2123</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2102" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:496px;top:427px;">
                            <asp:Label ID="P_2GIA2103" runat="server" CssClass="sensor-dot" Text="15" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2124</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2103" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:325px;top:449px;">
                            <asp:Label ID="P_2GIA2104" runat="server" CssClass="sensor-dot" Text="16" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2125</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2104" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:50px;top:289px;">
                            <asp:Label ID="P_2GIA2105" runat="server" CssClass="sensor-dot" Text="17" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2126</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2105" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:443px;top:404px;">
                            <asp:Label ID="P_2GIA2106" runat="server" CssClass="sensor-dot" Text="18" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2127</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2106" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:448px;top:204px;">
                            <asp:Label ID="P_2GIA2107" runat="server" CssClass="sensor-dot" Text="19" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2128</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2107" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:363px;top:181px;">
                            <asp:Label ID="P_2GIA2108" runat="server" CssClass="sensor-dot" Text="20" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2129</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_2GIA2108" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>

                        <!-- #3 HBM FCE 感測器 21-33 (AT-2130~AT-2142) -->
                        <div class="sensor-hotspot" style="left:257px;top:427px;">
                            <asp:Label ID="P_3GIA2001" runat="server" CssClass="sensor-dot" Text="21" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2130</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2001" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:422px;top:422px;">
                            <asp:Label ID="P_3GIA2002" runat="server" CssClass="sensor-dot" Text="22" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2131</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2002" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:363px;top:301px;">
                            <asp:Label ID="P_3GIA2101" runat="server" CssClass="sensor-dot" Text="23" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2132</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2101" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:313px;top:219px;">
                            <asp:Label ID="P_3GIA2102" runat="server" CssClass="sensor-dot" Text="24" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2133</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2102" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:341px;top:217px;">
                            <asp:Label ID="P_3GIA2103" runat="server" CssClass="sensor-dot" Text="25" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2134</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2103" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:430px;top:220px;">
                            <asp:Label ID="P_3GIA2104" runat="server" CssClass="sensor-dot" Text="26" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2135</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2104" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:418px;top:240px;">
                            <asp:Label ID="P_3GIA2105" runat="server" CssClass="sensor-dot" Text="27" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2136</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2105" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:444px;top:240px;">
                            <asp:Label ID="P_3GIA2106" runat="server" CssClass="sensor-dot" Text="28" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2137</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2106" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:315px;top:396px;">
                            <asp:Label ID="P_3GIA2107" runat="server" CssClass="sensor-dot" Text="29" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2138</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2107" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:333px;top:397px;">
                            <asp:Label ID="P_3GIA2108" runat="server" CssClass="sensor-dot" Text="30" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2139</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2108" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:435px;top:390px;">
                            <asp:Label ID="P_3GIA2003" runat="server" CssClass="sensor-dot" Text="31" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2140</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2003" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:420px;top:365px;">
                            <asp:Label ID="P_3GIA2004" runat="server" CssClass="sensor-dot" Text="32" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2141</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2004" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                        <div class="sensor-hotspot" style="left:446px;top:365px;">
                            <asp:Label ID="P_3GIA2005" runat="server" CssClass="sensor-dot" Text="33" />
                            <div class="sensor-tooltip">
                                <div class="st-title">AT-2142</div>
                                <div class="st-row"><span class="st-lbl">CO 濃度</span><span class="st-val"><asp:Label ID="V_3GIA2005" runat="server" Text="N/A" /> ppm</span></div>
                                <div class="st-row"><span class="st-lbl">所屬爐組</span><span class="st-val">HBM FCE</span></div>
                                <div class="st-row"><span class="st-lbl">狀態</span><span class="st-status">--</span></div>
                            </div>
                        </div>
                    </div>

                    <!-- ===== 右側資料欄 ===== -->
                    <div class="right-section">
                        <div class="main-data-col">

                            <!-- CO 偵測最大值 -->
                            <div class="data-card">
                                <div class="card-header">CO 偵測最大值</div>
                                <div class="co-max-value">
                                    <asp:Label ID="Total_CO" runat="server" Text="N/A" />
                                    <span class="co-max-unit">&#160;ppm</span>
                                </div>
                            </div>

                            <!-- HBM FCE 感測器 (AT-2110 ~ AT-2142) -->
                            <div class="data-card">
                                <div class="card-header">HBM FCE 感測器</div>
                                <table class="sensor-tbl">
                                    <tr>
                                        <td><asp:Label ID="IT_01" runat="server" CssClass="s-badge" Text="1" /></td>
                                        <td><asp:Label ID="L1" runat="server" CssClass="s-name" Text="AT-2110" /></td>
                                        <td class="s-val"><asp:Label ID="V1" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td style="width:8px;"></td>
                                        <td><asp:Label ID="IT_02" runat="server" CssClass="s-badge" Text="2" /></td>
                                        <td><asp:Label ID="L2" runat="server" CssClass="s-name" Text="AT-2111" /></td>
                                        <td class="s-val"><asp:Label ID="V2" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_03" runat="server" CssClass="s-badge" Text="3" /></td>
                                        <td><asp:Label ID="L3" runat="server" CssClass="s-name" Text="AT-2112" /></td>
                                        <td class="s-val"><asp:Label ID="V3" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_04" runat="server" CssClass="s-badge" Text="4" /></td>
                                        <td><asp:Label ID="L4" runat="server" CssClass="s-name" Text="AT-2113" /></td>
                                        <td class="s-val"><asp:Label ID="V4" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_05" runat="server" CssClass="s-badge" Text="5" /></td>
                                        <td><asp:Label ID="L5" runat="server" CssClass="s-name" Text="AT-2114" /></td>
                                        <td class="s-val"><asp:Label ID="V5" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_06" runat="server" CssClass="s-badge" Text="6" /></td>
                                        <td><asp:Label ID="L6" runat="server" CssClass="s-name" Text="AT-2115" /></td>
                                        <td class="s-val"><asp:Label ID="V6" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_07" runat="server" CssClass="s-badge" Text="7" /></td>
                                        <td><asp:Label ID="L7" runat="server" CssClass="s-name" Text="AT-2116" /></td>
                                        <td class="s-val"><asp:Label ID="V7" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_08" runat="server" CssClass="s-badge" Text="8" /></td>
                                        <td><asp:Label ID="L8" runat="server" CssClass="s-name" Text="AT-2117" /></td>
                                        <td class="s-val"><asp:Label ID="V8" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_09" runat="server" CssClass="s-badge" Text="9" /></td>
                                        <td><asp:Label ID="L9" runat="server" CssClass="s-name" Text="AT-2118" /></td>
                                        <td class="s-val"><asp:Label ID="V9" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_10" runat="server" CssClass="s-badge" Text="10" /></td>
                                        <td><asp:Label ID="L10" runat="server" CssClass="s-name" Text="AT-2119" /></td>
                                        <td class="s-val"><asp:Label ID="V10" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr><td colspan="9"><hr style="border:none;border-top:1px solid #e2e8f0;margin:3px 0;" /></td></tr>
                                    <tr>
                                        <td><asp:Label ID="IT_11" runat="server" CssClass="s-badge" Text="11" /></td>
                                        <td><asp:Label ID="L11" runat="server" CssClass="s-name" Text="AT-2120" /></td>
                                        <td class="s-val"><asp:Label ID="V11" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td style="width:8px;"></td>
                                        <td><asp:Label ID="IT_12" runat="server" CssClass="s-badge" Text="12" /></td>
                                        <td><asp:Label ID="L12" runat="server" CssClass="s-name" Text="AT-2121" /></td>
                                        <td class="s-val"><asp:Label ID="V12" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_13" runat="server" CssClass="s-badge" Text="13" /></td>
                                        <td><asp:Label ID="L13" runat="server" CssClass="s-name" Text="AT-2122" /></td>
                                        <td class="s-val"><asp:Label ID="V13" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_14" runat="server" CssClass="s-badge" Text="14" /></td>
                                        <td><asp:Label ID="L14" runat="server" CssClass="s-name" Text="AT-2123" /></td>
                                        <td class="s-val"><asp:Label ID="V14" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_15" runat="server" CssClass="s-badge" Text="15" /></td>
                                        <td><asp:Label ID="L15" runat="server" CssClass="s-name" Text="AT-2124" /></td>
                                        <td class="s-val"><asp:Label ID="V15" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_16" runat="server" CssClass="s-badge" Text="16" /></td>
                                        <td><asp:Label ID="L16" runat="server" CssClass="s-name" Text="AT-2125" /></td>
                                        <td class="s-val"><asp:Label ID="V16" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_17" runat="server" CssClass="s-badge" Text="17" /></td>
                                        <td><asp:Label ID="L17" runat="server" CssClass="s-name" Text="AT-2126" /></td>
                                        <td class="s-val"><asp:Label ID="V17" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_18" runat="server" CssClass="s-badge" Text="18" /></td>
                                        <td><asp:Label ID="L18" runat="server" CssClass="s-name" Text="AT-2127" /></td>
                                        <td class="s-val"><asp:Label ID="V18" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_19" runat="server" CssClass="s-badge" Text="19" /></td>
                                        <td><asp:Label ID="L19" runat="server" CssClass="s-name" Text="AT-2128" /></td>
                                        <td class="s-val"><asp:Label ID="V19" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_20" runat="server" CssClass="s-badge" Text="20" /></td>
                                        <td><asp:Label ID="L20" runat="server" CssClass="s-name" Text="AT-2129" /></td>
                                        <td class="s-val"><asp:Label ID="V20" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr><td colspan="9"><hr style="border:none;border-top:1px solid #e2e8f0;margin:3px 0;" /></td></tr>
                                    <tr>
                                        <td><asp:Label ID="IT_21" runat="server" CssClass="s-badge" Text="21" /></td>
                                        <td><asp:Label ID="L21" runat="server" CssClass="s-name" Text="AT-2130" /></td>
                                        <td class="s-val"><asp:Label ID="V21" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td style="width:8px;"></td>
                                        <td><asp:Label ID="IT_22" runat="server" CssClass="s-badge" Text="22" /></td>
                                        <td><asp:Label ID="L22" runat="server" CssClass="s-name" Text="AT-2131" /></td>
                                        <td class="s-val"><asp:Label ID="V22" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_23" runat="server" CssClass="s-badge" Text="23" /></td>
                                        <td><asp:Label ID="L23" runat="server" CssClass="s-name" Text="AT-2132" /></td>
                                        <td class="s-val"><asp:Label ID="V23" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_24" runat="server" CssClass="s-badge" Text="24" /></td>
                                        <td><asp:Label ID="L24" runat="server" CssClass="s-name" Text="AT-2133" /></td>
                                        <td class="s-val"><asp:Label ID="V24" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_25" runat="server" CssClass="s-badge" Text="25" /></td>
                                        <td><asp:Label ID="L25" runat="server" CssClass="s-name" Text="AT-2134" /></td>
                                        <td class="s-val"><asp:Label ID="V25" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_26" runat="server" CssClass="s-badge" Text="26" /></td>
                                        <td><asp:Label ID="L26" runat="server" CssClass="s-name" Text="AT-2135" /></td>
                                        <td class="s-val"><asp:Label ID="V26" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_27" runat="server" CssClass="s-badge" Text="27" /></td>
                                        <td><asp:Label ID="L27" runat="server" CssClass="s-name" Text="AT-2136" /></td>
                                        <td class="s-val"><asp:Label ID="V27" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_28" runat="server" CssClass="s-badge" Text="28" /></td>
                                        <td><asp:Label ID="L28" runat="server" CssClass="s-name" Text="AT-2137" /></td>
                                        <td class="s-val"><asp:Label ID="V28" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_29" runat="server" CssClass="s-badge" Text="29" /></td>
                                        <td><asp:Label ID="L29" runat="server" CssClass="s-name" Text="AT-2138" /></td>
                                        <td class="s-val"><asp:Label ID="V29" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_30" runat="server" CssClass="s-badge" Text="30" /></td>
                                        <td><asp:Label ID="L30" runat="server" CssClass="s-name" Text="AT-2139" /></td>
                                        <td class="s-val"><asp:Label ID="V30" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_31" runat="server" CssClass="s-badge" Text="31" /></td>
                                        <td><asp:Label ID="L31" runat="server" CssClass="s-name" Text="AT-2140" /></td>
                                        <td class="s-val"><asp:Label ID="V31" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td>
                                        <td><asp:Label ID="IT_32" runat="server" CssClass="s-badge" Text="32" /></td>
                                        <td><asp:Label ID="L21L32" runat="server" CssClass="s-name" Text="AT-2141" /></td>
                                        <td class="s-val"><asp:Label ID="V32" runat="server" Text="N/A" /></td><td>ppm</td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="IT_33" runat="server" CssClass="s-badge" Text="33" /></td>
                                        <td><asp:Label ID="L33" runat="server" CssClass="s-name" Text="AT-2142" /></td>
                                        <td class="s-val"><asp:Label ID="V33" runat="server" Text="N/A" /></td><td>ppm</td>
                                        <td></td><td colspan="4"></td>
                                    </tr>
                                </table>
                            </div>

                        </div><!-- end main-data-col -->

                        <!-- ===== Info Column ===== -->
                        <div class="info-col">

                            <div class="info-card">
                                <div class="card-header"><asp:Label ID="Label49" runat="server" Text="資料更新時間" /></div>
                                <div class="time-row">
                                    <span class="time-lbl"><asp:Label ID="Fn1" runat="server" Text="HBM FCE CO 偵測：" /></span>
                                    <asp:Label ID="Last_time_1" runat="server" CssClass="time-val" />
                                </div>
                            </div>

                            <div class="info-card">
                                <div class="legend-row"><span class="bdg bdg-b">&lt; 35 ppm</span><asp:Label ID="Label3" runat="server" Text="安全狀態" /></div>
                                <div class="legend-row"><span class="bdg bdg-r">&ge; 35 ppm</span><asp:Label ID="Label20" runat="server" Text="危險警報" /></div>
                            </div>

                        </div><!-- end info-col -->

                    </div><!-- end right-section -->

                </div><!-- end dashboard-container -->

                <!-- 隱藏的相容性 Label（保留 designer.vb 相依） -->
                <div style="display:none;">
                    <asp:Label ID="Label1" runat="server" Text="CO偵測值說明：" />
                    <asp:Label ID="Label5" runat="server" Text="ppm" />
                    <asp:Label ID="Label6" runat="server" Text="ppm" />
                    <asp:Label ID="Label14" runat="server" Text="ppm" />
                    <asp:Label ID="Label19" runat="server" Text="ppm" />
                    <asp:Label ID="Label24" runat="server" Text="ppm" />
                    <asp:Label ID="Label29" runat="server" Text="ppm" />
                    <asp:Label ID="Label34" runat="server" Text="ppm" />
                    <asp:Label ID="Label39" runat="server" Text="ppm" />
                    <asp:Label ID="Label54" runat="server" Text="ppm" />
                    <asp:Label ID="Label59" runat="server" Text="ppm" />
                    <asp:Label ID="Label64" runat="server" Text="ppm" />
                    <asp:Label ID="Label69" runat="server" Text="ppm" />
                    <asp:Label ID="Label74" runat="server" Text="ppm" />
                    <asp:Label ID="Label79" runat="server" Text="ppm" />
                    <asp:Label ID="Label84" runat="server" Text="ppm" />
                    <asp:Label ID="Label89" runat="server" Text="ppm" />
                    <asp:Label ID="Label94" runat="server" Text="ppm" />
                    <asp:Label ID="Label99" runat="server" Text="ppm" />
                    <asp:Label ID="Label104" runat="server" Text="ppm" />
                    <asp:Label ID="Label109" runat="server" Text="ppm" />
                    <asp:Label ID="Label2" runat="server" Text="ppm" />
                    <asp:Label ID="Label4" runat="server" Text="ppm" />
                    <asp:Label ID="Label7" runat="server" Text="ppm" />
                    <asp:Label ID="Label8" runat="server" Text="ppm" />
                    <asp:Label ID="Label9" runat="server" Text="ppm" />
                    <asp:Label ID="Label10" runat="server" Text="ppm" />
                    <asp:Label ID="Label11" runat="server" Text="ppm" />
                    <asp:Label ID="Label12" runat="server" Text="ppm" />
                    <asp:Label ID="Label13" runat="server" Text="ppm" />
                    <asp:Label ID="Label15" runat="server" Text="ppm" />
                    <asp:Label ID="Label16" runat="server" Text="ppm" />
                    <asp:Label ID="Label17" runat="server" Text="ppm" />
                    <asp:Label ID="Label18" runat="server" Text="ppm" />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function applyBreathingEffect() {
            var targets = document.querySelectorAll('.sensor-dot, .s-badge');
            for (var i = 0; i < targets.length; i++) {
                var el = targets[i];
                var bg = window.getComputedStyle(el).backgroundColor;
                el.classList.remove('pulse-danger');
                if (bg === 'rgb(255, 0, 0)') {
                    el.classList.add('pulse-danger');
                }
            }
        }

        function updateTooltipStatus() {
            var hotspots = document.querySelectorAll('.sensor-hotspot');
            for (var i = 0; i < hotspots.length; i++) {
                var hs = hotspots[i];
                var dot = hs.querySelector('.sensor-dot');
                var statusEl = hs.querySelector('.st-status');
                if (!dot || !statusEl) continue;
                var bg = window.getComputedStyle(dot).backgroundColor;
                if (bg === 'rgb(255, 0, 0)') {
                    statusEl.textContent = '危險警報';
                    statusEl.style.color = '#ef4444';
                } else {
                    statusEl.textContent = '安全狀態';
                    statusEl.style.color = '#000000';
                }
            }
        }

        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
                applyBreathingEffect();
                updateTooltipStatus();
            });
        }

        window.addEventListener('load', function() {
            applyBreathingEffect();
            updateTooltipStatus();
        });
    </script>
</form>
</body>
</html>
