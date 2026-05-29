<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3702.aspx.vb" Inherits="hPMISWEB._HSM3702" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>加熱爐固定式CO偵測圖</title>
    <script src="libs/echarts.min.js" type="text/javascript"></script>
    <style type="text/css">
        /* ==========================================================================
           Modern dashboard style (3701 style) - white clean design
           ========================================================================== */
        :root {
            --bg-light: #ffffff;
            --card-bg: #f8fafc;
            --border-color: #cbd5e1;
            --text-main: #334155;
            --text-muted: #64748b;
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
            gap: 24px;
            width: 100%;
            max-width: 1060px;
            align-items: flex-start;
        }
        .map-wrapper {
            position: relative;
            width: 576px;
            flex-shrink: 0;
            background: #fff;
            border-radius: 12px;
            padding: 8px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
            border: 1px solid var(--border-color);
        }
        .map-wrapper img.factory-img {
            width: 560px;
            height: auto;
            display: block;
            border-radius: 6px;
        }
        .sensor-hotspot {
            position: absolute;
            cursor: pointer;
            z-index: 5;
        }
        .sensor-dot {
            display: inline-block;
            width: 20px;
            height: 20px;
            line-height: 20px;
            text-align: center;
            font-size: 8px;
            font-weight: bold;
            border: 1px solid #555;
            border-radius: 2px;
            box-sizing: border-box;
        }
        .sensor-tooltip {
            position: absolute;
            bottom: 130%;
            left: 50%;
            transform: translateX(-50%);
            background: rgba(15,23,42,0.95);
            color: #e2e8f0;
            font-size: 0.73rem;
            padding: 4px 10px;
            border-radius: 5px;
            white-space: nowrap;
            opacity: 0;
            visibility: hidden;
            transition: all 0.2s ease;
            z-index: 200;
            pointer-events: none;
            box-shadow: 0 4px 12px rgba(0,0,0,0.3);
        }
        .sensor-hotspot:hover .sensor-tooltip {
            opacity: 1;
            visibility: visible;
        }
        .side-panel {
            flex: 1;
            min-width: 320px;
            max-width: 400px;
            display: flex;
            flex-direction: column;
            gap: 12px;
        }
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
        .metric-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 6px;
        }
        .metric-row:last-child { margin-bottom: 0; }
        .lbl { color: var(--text-muted); font-size: 0.85rem; }
        .sensor-tbl { width: 100%; border-collapse: collapse; }
        .sensor-tbl td { padding: 1px 3px; font-size: 0.76rem; vertical-align: middle; }
        .s-badge {
            display: inline-block;
            width: 18px; height: 18px; line-height: 18px;
            text-align: center; font-size: 8px; font-weight: bold;
            border: 1px solid #888; border-radius: 2px;
        }
        .s-name { color: #f97316; font-family: monospace; }
        .s-val { font-weight: bold; }
        .legend-row { display: flex; align-items: center; gap: 8px; margin-bottom: 4px; font-size: 0.8rem; color: var(--text-muted); }
        .bdg { padding: 2px 6px; border-radius: 3px; font-weight: bold; font-size: 0.7rem; }
        .bdg-b { background: rgba(37,99,235,0.1); color: #2563eb; border: 1px solid rgba(37,99,235,0.3); }
        .bdg-y { background: rgba(217,119,6,0.1); color: #d97706; border: 1px solid rgba(217,119,6,0.3); }
        .bdg-r { background: rgba(239,68,68,0.1); color: #dc2626; border: 1px solid rgba(239,68,68,0.3); }
        #wind-echart { width: 210px; height: 210px; margin: 4px auto 0; display: block; }
        .time-row { font-size: 0.76rem; margin-bottom: 3px; display: flex; justify-content: space-between; }
        .time-lbl { color: var(--text-muted); }
        .time-val { color: #2563eb; font-size: 0.74rem; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />
    <div class="page-center-wrapper">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="60000"></asp:Timer>
                <input id="wind_direction_W" runat="server" enableviewstate="true" name="wind_direction_W" type="hidden" />

                <div class="dashboard-container">

                    <!-- Left: factory map with 33 sensor hotspots -->
                    <div class="map-wrapper">
                        <img src="images/FCE.JPG" class="factory-img" alt="加熱爐廠區圖" />

                        <!-- #1FCE sensors 1-10 -->
                        <div class="sensor-hotspot" style="left:358px;top:420px;">
                            <asp:Label ID="P_1GIA2001" runat="server" CssClass="sensor-dot" Text="1" />
                            <div class="sensor-tooltip">1GIA2001：<asp:Label ID="V_1GIA2001" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:382px;top:412px;">
                            <asp:Label ID="P_1GIA2002" runat="server" CssClass="sensor-dot" Text="2" />
                            <div class="sensor-tooltip">1GIA2002：<asp:Label ID="V_1GIA2002" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:358px;top:396px;">
                            <asp:Label ID="P_1GIA2101" runat="server" CssClass="sensor-dot" Text="3" />
                            <div class="sensor-tooltip">1GIA2101：<asp:Label ID="V_1GIA2101" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:454px;top:396px;">
                            <asp:Label ID="P_1GIA2102" runat="server" CssClass="sensor-dot" Text="4" />
                            <div class="sensor-tooltip">1GIA2102：<asp:Label ID="V_1GIA2102" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:358px;top:340px;">
                            <asp:Label ID="P_1GIA2103" runat="server" CssClass="sensor-dot" Text="5" />
                            <div class="sensor-tooltip">1GIA2103：<asp:Label ID="V_1GIA2103" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:454px;top:340px;">
                            <asp:Label ID="P_1GIA2104" runat="server" CssClass="sensor-dot" Text="6" />
                            <div class="sensor-tooltip">1GIA2104：<asp:Label ID="V_1GIA2104" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:358px;top:292px;">
                            <asp:Label ID="P_1GIA2105" runat="server" CssClass="sensor-dot" Text="7" />
                            <div class="sensor-tooltip">1GIA2105：<asp:Label ID="V_1GIA2105" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:454px;top:292px;">
                            <asp:Label ID="P_1GIA2106" runat="server" CssClass="sensor-dot" Text="8" />
                            <div class="sensor-tooltip">1GIA2106：<asp:Label ID="V_1GIA2106" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:358px;top:244px;">
                            <asp:Label ID="P_1GIA2107" runat="server" CssClass="sensor-dot" Text="9" />
                            <div class="sensor-tooltip">1GIA2107：<asp:Label ID="V_1GIA2107" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:454px;top:244px;">
                            <asp:Label ID="P_1GIA2108" runat="server" CssClass="sensor-dot" Text="10" />
                            <div class="sensor-tooltip">1GIA2108：<asp:Label ID="V_1GIA2108" runat="server" Text="N/A" /> ppm</div>
                        </div>

                        <!-- #2FCE sensors 11-20 -->
                        <div class="sensor-hotspot" style="left:230px;top:420px;">
                            <asp:Label ID="P_2GIA2001" runat="server" CssClass="sensor-dot" Text="11" />
                            <div class="sensor-tooltip">2GIA2001：<asp:Label ID="V_2GIA2001" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:262px;top:412px;">
                            <asp:Label ID="P_2GIA2002" runat="server" CssClass="sensor-dot" Text="12" />
                            <div class="sensor-tooltip">2GIA2002：<asp:Label ID="V_2GIA2002" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:230px;top:396px;">
                            <asp:Label ID="P_2GIA2101" runat="server" CssClass="sensor-dot" Text="13" />
                            <div class="sensor-tooltip">2GIA2101：<asp:Label ID="V_2GIA2101" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:326px;top:396px;">
                            <asp:Label ID="P_2GIA2102" runat="server" CssClass="sensor-dot" Text="14" />
                            <div class="sensor-tooltip">2GIA2102：<asp:Label ID="V_2GIA2102" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:230px;top:340px;">
                            <asp:Label ID="P_2GIA2103" runat="server" CssClass="sensor-dot" Text="15" />
                            <div class="sensor-tooltip">2GIA2103：<asp:Label ID="V_2GIA2103" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:326px;top:340px;">
                            <asp:Label ID="P_2GIA2104" runat="server" CssClass="sensor-dot" Text="16" />
                            <div class="sensor-tooltip">2GIA2104：<asp:Label ID="V_2GIA2104" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:230px;top:292px;">
                            <asp:Label ID="P_2GIA2105" runat="server" CssClass="sensor-dot" Text="17" />
                            <div class="sensor-tooltip">2GIA2105：<asp:Label ID="V_2GIA2105" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:326px;top:292px;">
                            <asp:Label ID="P_2GIA2106" runat="server" CssClass="sensor-dot" Text="18" />
                            <div class="sensor-tooltip">2GIA2106：<asp:Label ID="V_2GIA2106" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:230px;top:244px;">
                            <asp:Label ID="P_2GIA2107" runat="server" CssClass="sensor-dot" Text="19" />
                            <div class="sensor-tooltip">2GIA2107：<asp:Label ID="V_2GIA2107" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:326px;top:244px;">
                            <asp:Label ID="P_2GIA2108" runat="server" CssClass="sensor-dot" Text="20" />
                            <div class="sensor-tooltip">2GIA2108：<asp:Label ID="V_2GIA2108" runat="server" Text="N/A" /> ppm</div>
                        </div>

                        <!-- #3FCE sensors 21-33 -->
                        <div class="sensor-hotspot" style="left:94px;top:420px;">
                            <asp:Label ID="P_3GIA2001" runat="server" CssClass="sensor-dot" Text="21" />
                            <div class="sensor-tooltip">3GIA2001：<asp:Label ID="V_3GIA2001" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:118px;top:412px;">
                            <asp:Label ID="P_3GIA2002" runat="server" CssClass="sensor-dot" Text="22" />
                            <div class="sensor-tooltip">3GIA2002：<asp:Label ID="V_3GIA2002" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:94px;top:396px;">
                            <asp:Label ID="P_3GIA2101" runat="server" CssClass="sensor-dot" Text="23" />
                            <div class="sensor-tooltip">3GIA2101：<asp:Label ID="V_3GIA2101" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:198px;top:396px;">
                            <asp:Label ID="P_3GIA2102" runat="server" CssClass="sensor-dot" Text="24" />
                            <div class="sensor-tooltip">3GIA2102：<asp:Label ID="V_3GIA2102" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:94px;top:340px;">
                            <asp:Label ID="P_3GIA2103" runat="server" CssClass="sensor-dot" Text="25" />
                            <div class="sensor-tooltip">3GIA2103：<asp:Label ID="V_3GIA2103" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:198px;top:340px;">
                            <asp:Label ID="P_3GIA2104" runat="server" CssClass="sensor-dot" Text="26" />
                            <div class="sensor-tooltip">3GIA2104：<asp:Label ID="V_3GIA2104" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:94px;top:292px;">
                            <asp:Label ID="P_3GIA2105" runat="server" CssClass="sensor-dot" Text="27" />
                            <div class="sensor-tooltip">3GIA2105：<asp:Label ID="V_3GIA2105" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:198px;top:292px;">
                            <asp:Label ID="P_3GIA2106" runat="server" CssClass="sensor-dot" Text="28" />
                            <div class="sensor-tooltip">3GIA2106：<asp:Label ID="V_3GIA2106" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:94px;top:244px;">
                            <asp:Label ID="P_3GIA2107" runat="server" CssClass="sensor-dot" Text="29" />
                            <div class="sensor-tooltip">3GIA2107：<asp:Label ID="V_3GIA2107" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:198px;top:244px;">
                            <asp:Label ID="P_3GIA2108" runat="server" CssClass="sensor-dot" Text="30" />
                            <div class="sensor-tooltip">3GIA2108：<asp:Label ID="V_3GIA2108" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:158px;top:588px;">
                            <asp:Label ID="P_3GIA2003" runat="server" CssClass="sensor-dot" Text="31" />
                            <div class="sensor-tooltip">3GIA2003：<asp:Label ID="V_3GIA2003" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:158px;top:212px;">
                            <asp:Label ID="P_3GIA2004" runat="server" CssClass="sensor-dot" Text="32" />
                            <div class="sensor-tooltip">3GIA2004：<asp:Label ID="V_3GIA2004" runat="server" Text="N/A" /> ppm</div>
                        </div>
                        <div class="sensor-hotspot" style="left:510px;top:452px;">
                            <asp:Label ID="P_3GIA2005" runat="server" CssClass="sensor-dot" Text="33" />
                            <div class="sensor-tooltip">3GIA2005：<asp:Label ID="V_3GIA2005" runat="server" Text="N/A" /> ppm</div>
                        </div>
                    </div>

                    <!-- Right: side panel -->
                    <div class="side-panel">

                        <!-- CO max value card -->
                        <div class="data-card">
                            <div class="card-header">CO 偵測最大值</div>
                            <div style="text-align:center; font-size:1.8rem; font-weight:bold; padding:8px 0;">
                                <asp:Label ID="Total_CO" runat="server" Text="N/A" />
                                <span style="font-size:0.9rem; font-weight:normal; color:var(--text-muted);">&#160;ppm</span>
                            </div>
                        </div>

                        <!-- Wind gauge card (ECharts compass) -->
                        <div class="data-card">
                            <div class="card-header">風速計 &#8212; <asp:Label ID="Label20" runat="server" Text="熱軋大樓" /></div>
                            <div id="wind-echart"></div>
                            <div class="metric-row" style="margin-top:4px;">
                                <asp:Label ID="Wind_S_W_L" runat="server" CssClass="lbl" Text="風速：" />
                                <span style="font-weight:bold;">
                                    <asp:Label ID="Val_W_W_S" runat="server" Text="N/A" />&#160;<asp:Label ID="Label22" runat="server" Text="m/s" />
                                </span>
                            </div>
                        </div>

                        <!-- #1FCE sensor list -->
                        <div class="data-card">
                            <div class="card-header">#1 加熱爐 (FCE) 感測器</div>
                            <table class="sensor-tbl">
                                <tr>
                                    <td><asp:Label ID="IT_01" runat="server" CssClass="s-badge" Text="1" /></td>
                                    <td><asp:Label ID="L1" runat="server" CssClass="s-name" Text="1G1A2001" /></td>
                                    <td class="s-val"><asp:Label ID="V1" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td style="width:6px;"></td>
                                    <td><asp:Label ID="IT_02" runat="server" CssClass="s-badge" Text="2" /></td>
                                    <td><asp:Label ID="L2" runat="server" CssClass="s-name" Text="1G1A2002" /></td>
                                    <td class="s-val"><asp:Label ID="V2" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_03" runat="server" CssClass="s-badge" Text="3" /></td>
                                    <td><asp:Label ID="L3" runat="server" CssClass="s-name" Text="1G1A2101" /></td>
                                    <td class="s-val"><asp:Label ID="V3" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_04" runat="server" CssClass="s-badge" Text="4" /></td>
                                    <td><asp:Label ID="L4" runat="server" CssClass="s-name" Text="1G1A2102" /></td>
                                    <td class="s-val"><asp:Label ID="V4" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_05" runat="server" CssClass="s-badge" Text="5" /></td>
                                    <td><asp:Label ID="L5" runat="server" CssClass="s-name" Text="1G1A2103" /></td>
                                    <td class="s-val"><asp:Label ID="V5" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_06" runat="server" CssClass="s-badge" Text="6" /></td>
                                    <td><asp:Label ID="L6" runat="server" CssClass="s-name" Text="1G1A2104" /></td>
                                    <td class="s-val"><asp:Label ID="V6" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_07" runat="server" CssClass="s-badge" Text="7" /></td>
                                    <td><asp:Label ID="L7" runat="server" CssClass="s-name" Text="1G1A2105" /></td>
                                    <td class="s-val"><asp:Label ID="V7" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_08" runat="server" CssClass="s-badge" Text="8" /></td>
                                    <td><asp:Label ID="L8" runat="server" CssClass="s-name" Text="1G1A2106" /></td>
                                    <td class="s-val"><asp:Label ID="V8" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_09" runat="server" CssClass="s-badge" Text="9" /></td>
                                    <td><asp:Label ID="L9" runat="server" CssClass="s-name" Text="1G1A2107" /></td>
                                    <td class="s-val"><asp:Label ID="V9" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_10" runat="server" CssClass="s-badge" Text="10" /></td>
                                    <td><asp:Label ID="L10" runat="server" CssClass="s-name" Text="1G1A2108" /></td>
                                    <td class="s-val"><asp:Label ID="V10" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                            </table>
                        </div>

                        <!-- #2FCE sensor list -->
                        <div class="data-card">
                            <div class="card-header">#2 加熱爐 (FCE) 感測器</div>
                            <table class="sensor-tbl">
                                <tr>
                                    <td><asp:Label ID="IT_11" runat="server" CssClass="s-badge" Text="11" /></td>
                                    <td><asp:Label ID="L11" runat="server" CssClass="s-name" Text="2G1A2001" /></td>
                                    <td class="s-val"><asp:Label ID="V11" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td style="width:6px;"></td>
                                    <td><asp:Label ID="IT_12" runat="server" CssClass="s-badge" Text="12" /></td>
                                    <td><asp:Label ID="L12" runat="server" CssClass="s-name" Text="2G1A2002" /></td>
                                    <td class="s-val"><asp:Label ID="V12" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_13" runat="server" CssClass="s-badge" Text="13" /></td>
                                    <td><asp:Label ID="L13" runat="server" CssClass="s-name" Text="2G1A2101" /></td>
                                    <td class="s-val"><asp:Label ID="V13" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_14" runat="server" CssClass="s-badge" Text="14" /></td>
                                    <td><asp:Label ID="L14" runat="server" CssClass="s-name" Text="2G1A2102" /></td>
                                    <td class="s-val"><asp:Label ID="V14" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_15" runat="server" CssClass="s-badge" Text="15" /></td>
                                    <td><asp:Label ID="L15" runat="server" CssClass="s-name" Text="2G1A2103" /></td>
                                    <td class="s-val"><asp:Label ID="V15" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_16" runat="server" CssClass="s-badge" Text="16" /></td>
                                    <td><asp:Label ID="L16" runat="server" CssClass="s-name" Text="2G1A2104" /></td>
                                    <td class="s-val"><asp:Label ID="V16" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_17" runat="server" CssClass="s-badge" Text="17" /></td>
                                    <td><asp:Label ID="L17" runat="server" CssClass="s-name" Text="2G1A2105" /></td>
                                    <td class="s-val"><asp:Label ID="V17" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_18" runat="server" CssClass="s-badge" Text="18" /></td>
                                    <td><asp:Label ID="L18" runat="server" CssClass="s-name" Text="2G1A2106" /></td>
                                    <td class="s-val"><asp:Label ID="V18" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_19" runat="server" CssClass="s-badge" Text="19" /></td>
                                    <td><asp:Label ID="L19" runat="server" CssClass="s-name" Text="2G1A2107" /></td>
                                    <td class="s-val"><asp:Label ID="V19" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_20" runat="server" CssClass="s-badge" Text="20" /></td>
                                    <td><asp:Label ID="L20" runat="server" CssClass="s-name" Text="2G1A2108" /></td>
                                    <td class="s-val"><asp:Label ID="V20" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                            </table>
                        </div>

                        <!-- #3FCE sensor list -->
                        <div class="data-card">
                            <div class="card-header">#3 加熱爐 (FCE) 感測器</div>
                            <table class="sensor-tbl">
                                <tr>
                                    <td><asp:Label ID="IT_21" runat="server" CssClass="s-badge" Text="21" /></td>
                                    <td><asp:Label ID="L21" runat="server" CssClass="s-name" Text="3G1A2001" /></td>
                                    <td class="s-val"><asp:Label ID="V21" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td style="width:6px;"></td>
                                    <td><asp:Label ID="IT_22" runat="server" CssClass="s-badge" Text="22" /></td>
                                    <td><asp:Label ID="L22" runat="server" CssClass="s-name" Text="3G1A2002" /></td>
                                    <td class="s-val"><asp:Label ID="V22" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_23" runat="server" CssClass="s-badge" Text="23" /></td>
                                    <td><asp:Label ID="L23" runat="server" CssClass="s-name" Text="3G1A2101" /></td>
                                    <td class="s-val"><asp:Label ID="V23" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_24" runat="server" CssClass="s-badge" Text="24" /></td>
                                    <td><asp:Label ID="L24" runat="server" CssClass="s-name" Text="3G1A2102" /></td>
                                    <td class="s-val"><asp:Label ID="V24" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_25" runat="server" CssClass="s-badge" Text="25" /></td>
                                    <td><asp:Label ID="L25" runat="server" CssClass="s-name" Text="3G1A2103" /></td>
                                    <td class="s-val"><asp:Label ID="V25" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_26" runat="server" CssClass="s-badge" Text="26" /></td>
                                    <td><asp:Label ID="L26" runat="server" CssClass="s-name" Text="3G1A2104" /></td>
                                    <td class="s-val"><asp:Label ID="V26" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_27" runat="server" CssClass="s-badge" Text="27" /></td>
                                    <td><asp:Label ID="L27" runat="server" CssClass="s-name" Text="3G1A2105" /></td>
                                    <td class="s-val"><asp:Label ID="V27" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_28" runat="server" CssClass="s-badge" Text="28" /></td>
                                    <td><asp:Label ID="L28" runat="server" CssClass="s-name" Text="3G1A2106" /></td>
                                    <td class="s-val"><asp:Label ID="V28" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_29" runat="server" CssClass="s-badge" Text="29" /></td>
                                    <td><asp:Label ID="L29" runat="server" CssClass="s-name" Text="3G1A2107" /></td>
                                    <td class="s-val"><asp:Label ID="V29" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_30" runat="server" CssClass="s-badge" Text="30" /></td>
                                    <td><asp:Label ID="L30" runat="server" CssClass="s-name" Text="3G1A2108" /></td>
                                    <td class="s-val"><asp:Label ID="V30" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_31" runat="server" CssClass="s-badge" Text="31" /></td>
                                    <td><asp:Label ID="L31" runat="server" CssClass="s-name" Text="3G1A2003" /></td>
                                    <td class="s-val"><asp:Label ID="V31" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td>
                                    <td><asp:Label ID="IT_32" runat="server" CssClass="s-badge" Text="32" /></td>
                                    <td><asp:Label ID="L21L32" runat="server" CssClass="s-name" Text="3G1A2004" /></td>
                                    <td class="s-val"><asp:Label ID="V32" runat="server" Text="N/A" /></td><td>ppm</td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="IT_33" runat="server" CssClass="s-badge" Text="33" /></td>
                                    <td><asp:Label ID="L33" runat="server" CssClass="s-name" Text="3G1A2005" /></td>
                                    <td class="s-val"><asp:Label ID="V33" runat="server" Text="N/A" /></td><td>ppm</td>
                                    <td></td><td colspan="4"></td>
                                </tr>
                            </table>
                        </div>

                        <!-- Data update timestamps -->
                        <div class="info-card">
                            <div class="card-header"><asp:Label ID="Label49" runat="server" Text="資料更新時間" /></div>
                            <div class="time-row">
                                <span class="time-lbl"><asp:Label ID="Fn1" runat="server" Text="#1 FCE：" /></span>
                                <asp:Label ID="Last_time_1" runat="server" CssClass="time-val" />
                            </div>
                            <div class="time-row">
                                <span class="time-lbl"><asp:Label ID="Fn2" runat="server" Text="#2 FCE：" /></span>
                                <asp:Label ID="Last_time_2" runat="server" CssClass="time-val" />
                            </div>
                            <div class="time-row">
                                <span class="time-lbl"><asp:Label ID="Label3" runat="server" Text="#3 FCE：" /></span>
                                <asp:Label ID="Last_time_3" runat="server" CssClass="time-val" />
                            </div>
                            <div class="time-row">
                                <span class="time-lbl"><asp:Label ID="Label1" runat="server" Text="風速計(W)：" /></span>
                                <asp:Label ID="Last_time_4" runat="server" CssClass="time-val" />
                            </div>
                        </div>

                        <!-- CO status legend -->
                        <div class="info-card">
                            <div class="legend-row"><span class="bdg bdg-b">&lt; 35 ppm</span><asp:Label ID="Label25" runat="server" Text="安全狀態" /></div>
                            <div class="legend-row"><span class="bdg bdg-y">35 ~ 75 ppm</span><asp:Label ID="Label26" runat="server" Text="注意狀態" /></div>
                            <div class="legend-row"><span class="bdg bdg-r">&gt; 75 ppm</span><asp:Label ID="Label23" runat="server" Text="危險警報" /></div>
                        </div>

                        <!-- Hidden ppm unit labels kept for designer.vb compatibility -->
                        <div style="display:none;">
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

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- ECharts wind compass gauge (outside UpdatePanel to persist across partial refresh) -->
    <script type="text/javascript">
        var windChart = null;

        function getWindDir() {
            var hf = document.getElementById('<%= wind_direction_W.ClientID %>');
            if (!hf || hf.value === '') return 0;
            var raw = parseFloat(hf.value);
            if (isNaN(raw)) return 0;
            // VB stores Wind_direction * (-1), reverse to get actual bearing
            var dir = -raw;
            dir = ((dir % 360) + 360) % 360;
            return dir;
        }

        function initWindChart() {
            var container = document.getElementById('wind-echart');
            if (!container || typeof echarts === 'undefined') return;
            var existing = echarts.getInstanceByDom(container);
            if (existing) { existing.dispose(); }
            windChart = echarts.init(container);
            windChart.setOption({
                backgroundColor: 'transparent',
                series: [{
                    type: 'gauge',
                    radius: '88%',
                    startAngle: 90,
                    endAngle: -270,
                    clockwise: true,
                    min: 0,
                    max: 360,
                    splitNumber: 8,
                    axisLine: { lineStyle: { width: 3, color: [[1, '#cbd5e1']] } },
                    splitLine: { length: 14, lineStyle: { color: '#94a3b8', width: 2 } },
                    axisTick: { length: 7, lineStyle: { color: '#94a3b8' } },
                    axisLabel: {
                        color: '#334155',
                        fontSize: 11,
                        formatter: function(v) {
                            var m = {0:'N',45:'NE',90:'E',135:'SE',180:'S',225:'SW',270:'W',315:'NW'};
                            return m[v] !== undefined ? m[v] : '';
                        }
                    },
                    pointer: { length: '68%', width: 5, itemStyle: { color: '#ef4444' } },
                    anchor: {
                        show: true, size: 10,
                        itemStyle: { color: '#2563eb', borderColor: '#1d4ed8', borderWidth: 2 }
                    },
                    detail: { show: false },
                    title: { show: false },
                    data: [{ value: getWindDir() }]
                }]
            });
        }

        // Re-init after UpdatePanel partial refresh (DOM is replaced)
        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
                initWindChart();
            });
        }

        window.addEventListener('load', function() { initWindChart(); });
    </script>
</form>
</body>
</html>

