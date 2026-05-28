<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3403.aspx.vb" Inherits="hPMISWEB._2TNRL_Production" ContentType="text/html" ResponseEncoding="UTF-8" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>#2TNRL 生產進度</title>
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
            min-height: 420px;
            width: 100%;
        }

        td.gvhs_data {
            background-color: #34495e !important;
            color: white !important;
            font-weight: 600;
            padding: 12px 18px;
            text-align: center;
            vertical-align: middle;
            border-bottom: 2px solid #233140 !important;
            white-space: nowrap;
        }

        td.data {
            background-color: #eaecf4 !important;
            color: #2d3748 !important;
            font-weight: bold;
            padding: 12px 18px;
            text-align: center;
            vertical-align: middle;
            border-top: 2px solid #cbd5e1;
            white-space: nowrap;
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

        .gv tbody tr:nth-child(even) td { background-color: #f8f9fc !important; }
        .gv tbody tr:hover td { background-color: #e9ecef !important; }

        .gv tbody tr td:first-child {
            font-weight: bold;
            color: #2c3e50;
            background-color: #f8f9fa !important;
        }

        .auto-fit-table {
            width: auto !important;
            border-collapse: collapse;
        }

        .auto-fit-table td { white-space: nowrap; }

        .pmisdata { font-weight: bold; }

        /* 合併捲軸容器：header/footer sticky，資料列共用一個 scrollbar */
        .combined-tbl-scroll {
            max-height: 340px;
            overflow-y: auto;
            overflow-x: auto;
            border: 1px solid #e2e8f0;
            border-radius: 4px;
        }

        .ctbl-hdr-row {
            position: sticky;
            top: 0;
            z-index: 10;
            display: flex;
            gap: 20px;
            background-color: #34495e;
        }

        .ctbl-data-row {
            display: flex;
            gap: 20px;
            background-color: #ffffff;
        }

        .ctbl-ftr-row {
            position: sticky;
            bottom: 0;
            z-index: 10;
            display: flex;
            gap: 20px;
            background-color: #eaecf4;
        }
    </style>

    <script type="text/javascript" src="libs/echarts.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {

            // ==========================================
            // A. 表格欄位自動寬度同步
            // ==========================================
            function syncTables(dataId, headId, footId) {
                var dataTable = document.getElementById(dataId);
                var headerTable = document.getElementById(headId);
                var footerTable = document.getElementById(footId);

                if (dataTable && dataTable.rows.length > 0 && headerTable && footerTable) {
                    var dataCells = dataTable.rows[0].cells;
                    var headerCells = headerTable.rows[0].cells;
                    var footerCells = footerTable.rows[0].cells;

                    for (var i = 0; i < headerCells.length; i++) {
                        if (headerCells[i]) headerCells[i].style.width = 'auto';
                        if (dataCells[i]) dataCells[i].style.width = 'auto';
                        if (footerCells[i]) footerCells[i].style.width = 'auto';
                    }

                    setTimeout(function () {
                        for (var i = 0; i < headerCells.length; i++) {
                            var hw = headerCells[i] ? headerCells[i].offsetWidth : 0;
                            var dw = dataCells[i] ? dataCells[i].offsetWidth : 0;
                            var fw = footerCells[i] ? footerCells[i].offsetWidth : 0;
                            var maxWidth = Math.max(hw, dw, fw);
                            if (headerCells[i]) { headerCells[i].style.width = maxWidth + 'px'; headerCells[i].style.minWidth = maxWidth + 'px'; }
                            if (dataCells[i]) { dataCells[i].style.width = maxWidth + 'px'; dataCells[i].style.minWidth = maxWidth + 'px'; }
                            if (footerCells[i]) { footerCells[i].style.width = maxWidth + 'px'; footerCells[i].style.minWidth = maxWidth + 'px'; }
                        }
                    }, 50);
                }
            }

            function syncAllTables() {
                syncTables('<%= gvMonth1.ClientID %>', 'tblHeader1', 'tblFooter1');
                syncTables('<%= gvMonth3.ClientID %>', 'tblHeader3', 'tblFooter3');
                syncTables('<%= gvMonth2.ClientID %>', 'tblHeader2', 'tblFooter2');
                syncTables('<%= gvMonth4.ClientID %>', 'tblHeader4', 'tblFooter4');
            }

            window.addEventListener('load', syncAllTables);
            window.addEventListener('resize', syncAllTables);

            // ==========================================
            // B. ECharts 圖表繪製
            // ==========================================
            if (typeof chartData === 'undefined') return;

            var dimDom = document.getElementById('echartDim');
            var dimChart = echarts.init(dimDom);
            var optionDim = {
                backgroundColor: 'transparent',
                title: { text: '#2TNRL 厚度與前段製程生產趨勢 (MT)', left: 'center', textStyle: { color: '#2c3e50', fontSize: 15, fontWeight: 'bold' } },
                tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                legend: { data: ['ETNG', 'WTNG', 'NTNG', 'NTCG', 'ETCG', 'MDSZ', 'NRWD', 'MDWD', 'WIWD'], bottom: 0, icon: 'circle' },
                grid: { left: '8%', right: '5%', bottom: '20%', top: '15%', containLabel: true },
                xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis, axisLabel: { fontWeight: 'bold' } }],
                yAxis: [{ type: 'value', name: '產量 (MT)', scale: true, splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } } }],
                series: [
                    { name: 'ETNG', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.etng },
                    { name: 'WTNG', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.wtng },
                    { name: 'NTNG', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.ntng },
                    { name: 'NTCG', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.ntcg },
                    { name: 'ETCG', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.etcg },
                    { name: 'MDSZ', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.mdsz },
                    { name: 'NRWD', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.nrwd },
                    { name: 'MDWD', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.mdwd },
                    { name: 'WIWD', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.wiwd }
                ]
            };
            dimChart.setOption(optionDim);

            var strDom = document.getElementById('echartStrength');
            var strChart = echarts.init(strDom);
            var optionStr = {
                backgroundColor: 'transparent',
                title: { text: '#2TNRL 強度與表面製程生產趨勢 (MT)', left: 'center', textStyle: { color: '#2c3e50', fontSize: 15, fontWeight: 'bold' } },
                tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                legend: { data: ['EXLC', 'LSCS', 'MSCS', 'HICS', 'VHIS', 'SUS', 'NRCQ', 'HICQ', 'VHCQ'], bottom: 0, icon: 'circle' },
                grid: { left: '8%', right: '5%', bottom: '20%', top: '15%', containLabel: true },
                xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis, axisLabel: { fontWeight: 'bold' } }],
                yAxis: [{ type: 'value', name: '產量 (MT)', scale: true, splitLine: { lineStyle: { type: 'dashed', color: '#eaeaea' } } }],
                series: [
                    { name: 'EXLC', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.exlc },
                    { name: 'LSCS', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.lscs },
                    { name: 'MSCS', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.mscs },
                    { name: 'HICS', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.hics },
                    { name: 'VHIS', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.vhis },
                    { name: 'SUS',  type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.sus  },
                    { name: 'NRCQ', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.nrcq },
                    { name: 'HICQ', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.hicq },
                    { name: 'VHCQ', type: 'line', smooth: true, symbol: 'circle', symbolSize: 6, lineStyle: { width: 3 }, data: chartData.vhcq }
                ]
            };
            strChart.setOption(optionStr);

            window.addEventListener('resize', function () {
                dimChart.resize();
                strChart.resize();
            });
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <a name="#Home"></a>

        <div class="container-fluid main-content px-4">

            <!-- ========== 第一層 Card：#2TNRL 生產趨勢圖 ========== -->
            <div class="card-custom mb-4 mt-2">
                <div class="card-header-custom">
                    <span class="fs-4" style="color: white !important;">📊 #2TNRL 生產趨勢</span>
                    <span class="badge bg-warning text-dark fs-6 shadow-sm">資料區間：<asp:Label ID="LabelStartdate" runat="server"></asp:Label> ~ <asp:Label ID="LabelEnddate" runat="server"></asp:Label></span>
                </div>
                <div class="chart-card-body">
                    <div style="display: flex; justify-content: space-between; flex-wrap: wrap; gap: 20px; width: 100%;">
                        <div id="echartDim" style="flex: 1 1 48%; min-width: 500px; height: 350px;"></div>
                        <div id="echartStrength" style="flex: 1 1 48%; min-width: 500px; height: 350px;"></div>
                    </div>
                </div>
            </div>

            <!-- ========== 第二層 Card：廠區厚度與前段製程進度 ========== -->
            <div class="card-custom mb-4">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 #2TNRL 厚度與前段製程進度</span>
                    </div>
                </div>
                <div class="card-body p-3">
                    <!-- 置中容器 -->
                    <div style="text-align: center;">
                        <div style="display: inline-block; text-align: left;">
                            <!-- 合併捲軸：header sticky-top、兩個 GridView 共用一個 scrollbar、footer sticky-bottom -->
                            <div class="combined-tbl-scroll">
                                <div class="ctbl-hdr-row">
                                    <table id="tblHeader1" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="gvhs_data">月份</td>
                                            <td class="gvhs_data">東窄薄料<br />(ETNG)</td>
                                            <td class="gvhs_data">西寬薄料<br />(WTNG)</td>
                                            <td class="gvhs_data">中寬料<br />(NTNG)</td>
                                            <td class="gvhs_data">北薄中厚<br />(NTCG)</td>
                                            <td class="gvhs_data">東中厚料<br />(ETCG)</td>
                                            <td class="gvhs_data">中尺寸<br />(MDSZ)</td>
                                        </tr>
                                    </table>
                                    <table id="tblHeader3" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="gvhs_data">窄寬<br />(NRWD)</td>
                                            <td class="gvhs_data">中等寬<br />(MDWD)</td>
                                            <td class="gvhs_data">寬寬<br />(WIWD)</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="ctbl-data-row">
                                    <asp:GridView ID="gvMonth1" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                                        <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvMonth3" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                                        <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                                    </asp:GridView>
                                </div>
                                <div class="ctbl-ftr-row">
                                    <table id="tblFooter1" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="data"><asp:Label ID="lblMonth1" runat="server" CssClass="pmisdata"></asp:Label>月統計</td>
                                            <td class="data"><asp:Label ID="lblETNG" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblWTNG" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblNTNG" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblNTCG" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblETCG" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblMDSZ" runat="server" CssClass="pmisdata"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <table id="tblFooter3" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="data"><asp:Label ID="lblNRWD" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblMDWD" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblWIWD" runat="server" CssClass="pmisdata"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- ========== 第三層 Card：廠區強度與表面製程進度 ========== -->
            <div class="card-custom mb-5">
                <div class="card-header-custom">
                    <div>
                        <span class="fs-4" style="color: white !important;">📋 #2TNRL 強度與表面製程進度</span>
                    </div>
                </div>
                <div class="card-body p-3">
                    <div style="text-align: center;">
                        <div style="display: inline-block; text-align: left;">
                            <div class="combined-tbl-scroll">
                                <div class="ctbl-hdr-row">
                                    <table id="tblHeader2" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="gvhs_data">月份</td>
                                            <td class="gvhs_data">EXLC</td>
                                            <td class="gvhs_data">LSCS</td>
                                            <td class="gvhs_data">MSCS</td>
                                            <td class="gvhs_data">HICS</td>
                                            <td class="gvhs_data">VHIS</td>
                                            <td class="gvhs_data">SUS</td>
                                        </tr>
                                    </table>
                                    <table id="tblHeader4" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="gvhs_data">NRCQ</td>
                                            <td class="gvhs_data">HICQ</td>
                                            <td class="gvhs_data">VHCQ</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="ctbl-data-row">
                                    <asp:GridView ID="gvMonth2" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                                        <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvMonth4" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                                        <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                                    </asp:GridView>
                                </div>
                                <div class="ctbl-ftr-row">
                                    <table id="tblFooter2" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="data"><asp:Label ID="lblMonth2" runat="server" CssClass="pmisdata"></asp:Label>月統計</td>
                                            <td class="data"><asp:Label ID="lblEXLC" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblLSCS" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblMSCS" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblHICS" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblVHIS" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblSUS"  runat="server" CssClass="pmisdata"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <table id="tblFooter4" class="auto-fit-table" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="data"><asp:Label ID="lblNRCQ" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblHICQ" runat="server" CssClass="pmisdata"></asp:Label></td>
                                            <td class="data"><asp:Label ID="lblVHCQ" runat="server" CssClass="pmisdata"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script src="libs/bootstrap.bundle.min.js"></script>
</body>
</html>
