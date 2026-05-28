<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sys_que_status.aspx.vb" Inherits="hPMISWEB.sys_que_status" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Queue 狀態監控</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table-wrapper {
            max-width: 1400px;
            margin: 0 auto;
        }
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
        
        <div class="container table-wrapper mt-4 mb-5">
            <div class="table-responsive shadow-sm bg-white rounded d-flex justify-content-center">
                
                <asp:Repeater ID="rptQueueStatus" runat="server" OnItemDataBound="rptQueueStatus_ItemDataBound">
                    
                    <HeaderTemplate>
                        <table class="table table-bordered table-striped table-hover table-sm text-center align-middle mb-0 text-nowrap w-auto mx-auto">
                            <thead class="table-dark">
                                <tr>
                                    <th style="font-size: 1.1rem;">系統資料表名稱</th>
                                    <th style="font-size: 1.1rem;">資料表狀態為"N"數量</th>
                                    <th style="font-size: 1.1rem;">最後一筆Queue</th>
                                    <th style="font-size: 1.1rem;">距離上筆拋送時間(分鐘)</th>
                                    <th style="font-size: 1.1rem;">狀態為"N"距離目前時間(分鐘)</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    
                    <ItemTemplate>
                        <tr id="trRow" runat="server">
                            <td class="fw-bold text-start fs-5"><%# Eval("QueueName") %></td>
                            <td class="fs-5"><%# Eval("sys_status") %></td>
                            <td class="fs-5"><%# Eval("LatestProcesstime") %></td>
                            <td class="fs-5"><%# Eval("Datetimediff") %></td>
                            <td class="fs-5">
                                <asp:Literal ID="litStatusN" runat="server" Text='<%# Eval("DatetimediffForStatusN") %>'></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                    
                </asp:Repeater>
                
            </div>
        </div>
    </form>
    
  <script src="js/bootstrap.bundle.min.js"></script>
</body>
</html>