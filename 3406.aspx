<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3406.aspx.vb" Inherits="hPMISWEB.HBM_Production" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HBM_Production</title>
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <style type="text/css">
        .auto-fit-table { width: auto !important; }
        .auto-fit-table th, .auto-fit-table td { white-space: nowrap; padding: 8px 15px; }
    </style>
    <script type="text/javascript" src="libs/echarts.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            function syncTableWidths() {
                var dataTable = document.getElementById('<%= gvMonth1.ClientID %>');
                var headerTable = document.getElementById('tblMonthHeader');
                var footerTable = document.getElementById('tblMonthFooter');
                if (dataTable && dataTable.rows.length > 0 && headerTable && footerTable) {
                    var dataCells = dataTable.rows[0].cells;
                    var headerCells = headerTable.rows[0].cells;
                    var footerCells = footerTable.rows[0].cells;
                    for (var i = 0; i < headerCells.length; i++) {
                        if(headerCells[i]) headerCells[i].style.width = 'auto';
                        if(dataCells[i]) dataCells[i].style.width = 'auto';
                        if(footerCells[i]) footerCells[i].style.width = 'auto';
                    }
                    setTimeout(function() {
                        for (var i = 0; i < headerCells.length; i++) {
                            var hw = headerCells[i] ? headerCells[i].offsetWidth : 0;
                            var dw = dataCells[i] ? dataCells[i].offsetWidth : 0;
                            var fw = footerCells[i] ? footerCells[i].offsetWidth : 0;
                            var maxWidth = Math.max(hw, dw, fw);
                            if(headerCells[i]) { headerCells[i].style.width = maxWidth + 'px'; headerCells[i].style.minWidth = maxWidth + 'px'; }
                            if(dataCells[i]) { dataCells[i].style.width = maxWidth + 'px'; dataCells[i].style.minWidth = maxWidth + 'px'; }
                            if(footerCells[i]) { footerCells[i].style.width = maxWidth + 'px'; footerCells[i].style.minWidth = maxWidth + 'px'; }
                        }
                    }, 50);
                }
            }
            window.addEventListener('load', syncTableWidths);
            window.addEventListener('resize', syncTableWidths);

            if (typeof chartData === 'undefined') return;

            var prodChart = echarts.init(document.getElementById('echartProd'));
            prodChart.setOption({
                backgroundColor: '#ffffff',
                title: { text: '型鋼生產分析趨勢 (MT)', left: 'center', textStyle: { fontSize: 16 } },
                tooltip: { trigger: 'axis', axisPointer: { type: 'cross' } },
                legend: { data: ['型鋼 (Hxx)', '窄板 (Fxx)', '矩形胚 (Lxx)'], bottom: 0 },
                grid: { left: '8%', right: '5%', bottom: '15%', containLabel: true },
                xAxis: [{ type: 'category', boundaryGap: false, data: chartData.xAxis }],
                yAxis: [{ type: 'value', name: '重量 (MT)', scale: true }],
                series: [
                    { name: '型鋼 (Hxx)', type: 'line', smooth: true, data: chartData.hxx },
                    { name: '窄板 (Fxx)', type: 'line', smooth: true, data: chartData.fxx },
                    { name: '矩形胚 (Lxx)', type: 'line', smooth: true, data: chartData.lxx }
                ]
            });
            window.addEventListener('resize', function () { prodChart.resize(); });
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
                <div id="echartProd" style="width: 100%; height: 400px;"></div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HBMPMISConnectionString %>" SelectCommand="SELECT 
    DATEADD(m, DATEDIFF(m, 0, boundle_date), 0) AS boundle_date,
    

    CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'H%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_H,
    CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'F%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_F,
    CAST(ROUND(SUM(CASE WHEN Product_Size_Code LIKE 'L%' THEN bound_weight ELSE 0 END) / 1000.0, 2) AS FLOAT) AS prod_L

FROM h_pmis_hbm_info WITH(NOLOCK)
WHERE boundle_date BETWEEN DATEADD(year, -1, GETDATE()) AND GETDATE()

  AND (Product_Size_Code LIKE 'H%' OR Product_Size_Code LIKE 'F%' OR Product_Size_Code LIKE 'L%')
GROUP BY DATEADD(m, DATEDIFF(m, 0, boundle_date), 0)
ORDER BY boundle_date;"></asp:SqlDataSource>
            </div>
            <div style="margin-bottom: 40px;">
                <div style="margin-bottom: 10px;">
                    <strong style="font-size: 16px;">型鋼生產分析履歷</strong><br />
                    <span style="font-size: 13px; color: #555;">資料於每日2300進行更新，並於當月第一天2300將資料重新編排</span>
                </div>
                
                <div style="overflow-x: auto; max-width: 100%; display: inline-block;">
                    <table id="tblMonthHeader" border="0" cellpadding="0" cellspacing="0" class="auto-fit-table" style="border-collapse: collapse;">
                        <tr>
                            <td class="gvhs_data">日期</td>
                            <td class="gvhs_data" style="text-align: center">型鋼 (Hxx)</td>
                            <td class="gvhs_data" style="text-align: center">窄板 (Fxx)</td>
                            <td class="gvhs_data" style="text-align: center">矩形胚 (Lxx)</td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="gvMonth1" runat="server" CellSpacing="1" CssClass="gv auto-fit-table" GridLines="None" ShowHeader="False">
                            <RowStyle CssClass="gvrs" /><SelectedRowStyle CssClass="gvsrs" />
                        </asp:GridView>
                    </asp:Panel>
                    <table id="tblMonthFooter" border="0" cellpadding="0" cellspacing="0" class="auto-fit-table" style="border-collapse: collapse;">
                        <tr>
                            <td class="data"><asp:Label ID="lblMonth1" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>月統計</td>
                            <td class="data"><asp:Label ID="lblHxx" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblFxx" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                            <td class="data"><asp:Label ID="lblLxx" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

         
        </div>
    </form>
</body>
</html>