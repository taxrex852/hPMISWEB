<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3206.aspx.vb" Inherits="hPMISWEB.HBM_Defect" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HBM_Defect</title>
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <style type="text/css">
        .auto-fit-table { width: auto !important; }
        .auto-fit-table th, .auto-fit-table td { white-space: nowrap; padding: 8px 15px; }
    </style>
    <script type="text/javascript" src="libs/echarts.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            function syncTableWidths() {
                var dataTable = document.getElementById('<%= gvMonth.ClientID %>');
                var headerTable = document.getElementById('tblMonthHeader');
                var footerTable = document.getElementById('tblMonthFooter');
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
            window.addEventListener('load', syncTableWidths);
            window.addEventListener('resize', syncTableWidths);

            if (typeof chartData === 'undefined') return;

            var defectChart = echarts.init(document.getElementById('echartDefect'));
            defectChart.setOption({
                backgroundColor: '#ffffff',
                title: { text: '型鋼缺陷統計趨勢 (MT)', left: 'center', textStyle: { fontSize: 16 } },
                tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                legend: { data: ['缺陷 Top 1', '缺陷 Top 2', '缺陷 Top 3', '缺陷 Top 4', '缺陷 Top 5'], bottom: 0 },
                grid: { left: '8%', right: '5%', bottom: '15%', containLabel: true },
                xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis }],
                yAxis: [{ type: 'value', name: '重量 (MT)', scale: true }],
                series: [
                    { name: '缺陷 Top 1', type: 'line', smooth: true, data: chartData.d1 },
                    { name: '缺陷 Top 2', type: 'line', smooth: true, data: chartData.d2 },
                    { name: '缺陷 Top 3', type: 'line', smooth: true, data: chartData.d3 },
                    { name: '缺陷 Top 4', type: 'line', smooth: true, data: chartData.d4 },
                    { name: '缺陷 Top 5', type: 'line', smooth: true, data: chartData.d5 }
                ]
            });
            window.addEventListener('resize', function () { defectChart.resize(); });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        <div style="max-width: 1200px; width: 95%; margin: 20px auto; font-family: sans-serif;">
               <div>
                <div style="margin-bottom: 15px; font-weight: bold; color: #333;">
                    資料區間：<asp:Label ID="LabelStartdate" runat="server"></asp:Label> ~ <asp:Label ID="LabelEnddate" runat="server"></asp:Label>
                </div>
                <div id="echartDefect" style="width: 100%; height: 400px;"></div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HBMPMISConnectionString %>" SelectCommand="WITH MonthlyDefect AS (

    SELECT 
        DATEADD(m, DATEDIFF(m, 0, boundle_date), 0) AS boundle_month,
        Defect_Code_of_This_Bundle AS def_code,
        bound_weight
    FROM h_pmis_hbm_info WITH (NOLOCK)
    WHERE boundle_date BETWEEN DATEADD(year, -1, GETDATE()) AND GETDATE()
      AND Defect_Code_of_This_Bundle &lt;&gt; ''
      AND Defect_Code_of_This_Bundle IS NOT NULL
),
RankedData AS (

    SELECT 
        boundle_month,
        def_code,
        CAST(ROUND(SUM(bound_weight) / 1000.0, 2) AS FLOAT) AS total_bound_weight,
        ROW_NUMBER() OVER (
            PARTITION BY boundle_month 
            ORDER BY CAST(ROUND(SUM(bound_weight) / 1000.0, 2) AS FLOAT) DESC
        ) AS rn
    FROM MonthlyDefect
    GROUP BY boundle_month, def_code
)

SELECT 
    boundle_month AS boundle_date,
    MAX(CASE WHEN rn = 1 THEN total_bound_weight END) AS def_top1,
    MAX(CASE WHEN rn = 2 THEN total_bound_weight END) AS def_top2,
    MAX(CASE WHEN rn = 3 THEN total_bound_weight END) AS def_top3,
    MAX(CASE WHEN rn = 4 THEN total_bound_weight END) AS def_top4,
    MAX(CASE WHEN rn = 5 THEN total_bound_weight END) AS def_top5
FROM RankedData
WHERE rn &lt;= 5
GROUP BY boundle_month
ORDER BY boundle_month;"></asp:SqlDataSource>
            </div>
            <div style="margin-bottom: 40px;">
                <div style="margin-bottom: 10px;">
                    <strong style="font-size: 16px;">型鋼每日缺陷統計</strong><br />
                    <span style="font-size: 13px; color: #555;">每日0700/1500/2300進行資料更新，並於每日1500將資料重新編排</span>
                </div>
                <div style="overflow-x: auto;">
                    <asp:GridView ID="gvDaily" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None">
                        <RowStyle CssClass="gvrs" /><HeaderStyle CssClass="gvhs" /><SelectedRowStyle CssClass="gvsrs" />
                    </asp:GridView>
                </div>
            </div>

            <div style="margin-bottom: 40px;">
                <div style="margin-bottom: 10px;">
                    <strong style="font-size: 16px;">型鋼當月缺陷統計</strong><br />
                    <span style="font-size: 13px; color: #555;">資料於每日2300進行更新，並於當月第一天2300將資料進行重整</span>
                </div>
                <div style="overflow-x: auto; max-width: 100%; display: inline-block;">
                    <table id="tblMonthHeader" border="0" cellpadding="0" cellspacing="0" class="auto-fit-table" style="border-collapse: collapse;">
                        <tr>
                            <td class="gvhs_data"></td>
                            <td class="gvhs_data" style="text-align: center">缺陷 top (1)</td>
                            <td class="gvhs_data" style="text-align: center">缺陷 top (2)</td>
                            <td class="gvhs_data" style="text-align: center">缺陷 top (3)</td>
                            <td class="gvhs_data" style="text-align: center">缺陷 top (4)</td>
                            <td class="gvhs_data" style="text-align: center">缺陷 top (5)</td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="gvMonth" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                            <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                        </asp:GridView>
                    </asp:Panel>
                    <table id="tblMonthFooter" border="0" cellpadding="0" cellspacing="0" class="auto-fit-table" style="border-collapse: collapse;">
                        <tr>
                            <td class="data"><asp:Label ID="lblMonth" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>月統計</td>
                            <td class="data"><asp:Label ID="lblDT1" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblDT2" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblDT3" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblDT4" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblDT5" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

         
        </div>
    </form>
</body>
</html>