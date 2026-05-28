<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3301.aspx.vb" Inherits="hPMISWEB.HSM_Delay" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css\diagram.css" />
    <title>HSM_Delay</title>
</head>
<script language="JavaScript" type="text/javascript"></script>
<script language="vbscript" type="text/vbscript">
    Dim i
    Dim color(0)
    color(0) = RGB(255, 51, 102)
    
    Dim LineColor(4)
    LineColor(0) = RGB(255, 0, 0)
    LineColor(1) = RGB(0, 255, 0)
    LineColor(2) = RGB(0, 0, 255)
    LineColor(3) = RGB(255, 165, 0)
    LineColor(4) = RGB(139, 69, 19)
    
    Sub window_onload()
        initChart()
        create_chart()
        create_data(1)
        check_active()
        window.location.hash = form1.hAnc.value
    End Sub

    Sub initChart()
        form1.TChart1.Header.Text.clear
        form1.TChart1.Header.Text.Add form1.hStartDate.value + "~" + form1.hEndDate.value 
        form1.TChart1.Aspect.View3D = False
        form1.TChart1.Panel.BevelOuter = 2
        form1.TChart1.Panel.BevelWidth = 2
        form1.TChart1.Legend.LegendStyle = 1 
        form1.TChart1.Legend.CheckBoxes = 1    
        form1.TChart1.Scroll.Enable = 0        
        'Tools Setting
        '十字游標
        idxCursor = form1.TChart1.Tools.Add(tcCursor)
        form1.TChart1.Tools.Items(idxCursor).asTeeCursor.FollowMouse = True
        
        '顯示資料數值
        idxMarksTip = form1.TChart1.Tools.Add(8)
        form1.TChart1.Tools.Items(idxMarksTip).asMarksTip.MouseAction = mtmMove
        form1.TChart1.Tools.Items(idxMarksTip).asMarksTip.Style = smsValue 
        
        dim tmp
        tmp = teech.currentStyle.backgroundColor
        tmp = "&H" & mid(tmp,6,2) & mid(tmp,4,2) & mid(tmp,2,2)
        form1.TChart1.Panel.Color = tmp
            
    End Sub
    
    Sub create_chart()
                
        for i = 0 to 4 
            form1.TChart1.AddSeries(0)
            form1.TChart1.Series(i).asLine.Pointer.Visible = True
            form1.TChart1.Series(i).asLine.Pointer.Style = 1
            form1.TChart1.Series(i).asLine.Pointer.HorizontalSize = 2
            form1.TChart1.Series(i).asLine.Pointer.VerticalSize = 2
            form1.TChart1.Series(i).Color = LineColor(i)
            form1.TChart1.Series(i).asLine.LinePen.Width = 3
        next
        
        form1.TChart1.Series(0).Title = "DA"
        form1.TChart1.Series(1).Title = "DR"
        form1.TChart1.Series(2).Title = "DS"
        form1.TChart1.Series(3).Title = "DO"
        form1.TChart1.Series(4).Title = "Total"
   End Sub
   
   Sub create_data(Mode)
        Dim arrDA, arrDR, arrDS, arrDO, arrTotal
        arrDate = split(form1.hDate.value, ",")
        
        form1.TChart1.Series(0).Clear
        form1.TChart1.Series(1).Clear
        form1.TChart1.Series(2).Clear
        form1.TChart1.Series(3).Clear
        form1.TChart1.Series(4).Clear
        
        if Mode = 1 then
            form1.TChart1.Axis.Left.Title.Font.Name="@新細明體"
            form1.TChart1.Axis.Left.Title.Angle=270
            form1.TChart1.Axis.Left.Title.Caption = "次數 (次)"
             
            arrDA = split(form1.hDA1.value, ",")
            arrDR = split(form1.hDR1.value, ",")
            arrDS = split(form1.hDS1.value, ",")
            arrDO = split(form1.hDO1.value, ",")
            arrTotal = split(form1.hTotal1.value, ",")
            
            form1.radio1.checked = true
            form1.radio2.checked = false
             
        else
            form1.TChart1.Axis.Left.Title.Font.Name="@新細明體"
            form1.TChart1.Axis.Left.Title.Angle=270
            form1.TChart1.Axis.Left.Title.Caption = "時間 (分)"
             
            arrDA = split(form1.hDA2.value, ",")
            arrDR = split(form1.hDR2.value, ",")
            arrDS = split(form1.hDS2.value, ",")
            arrDO = split(form1.hDO2.value, ",")
            arrTotal = split(form1.hTotal2.value, ",")
            
            form1.radio2.checked = true
            form1.radio1.checked = false
            
        end if

        for i = 0 to ubound(arrDA) -1
             form1.TChart1.Series(0).AddXY i+1, CDbl(arrDA(i)), arrDate(i), color(0)
             form1.TChart1.Series(1).AddXY i+1, CDbl(arrDR(i)), arrDate(i), color(0) 
             form1.TChart1.Series(2).AddXY i+1, CDbl(arrDS(i)), arrDate(i), color(0) 
             form1.TChart1.Series(3).AddXY i+1, CDbl(arrDO(i)), arrDate(i), color(0) 
             form1.TChart1.Series(4).AddXY i+1, CDbl(arrTotal(i)), arrDate(i), color(0)                            
        next
    End Sub
    
    Sub check_active()
        form1.TChart1.Series(0).Active = form1.DAAct.value
        form1.TChart1.Series(1).Active = form1.DRAct.value
        form1.TChart1.Series(2).Active = form1.DSAct.value
        form1.TChart1.Series(3).Active = form1.DOAct.value
        form1.TChart1.Series(4).Active = form1.TotalAct.value
    End Sub
    
    Sub TChart1_OnDblClick()
        form1.TChart1.ShowEditor
        form1.TChart1.StopMouse
    End Sub
    
    Sub TChart1_OnClick()
        form1.DAAct.value = form1.TChart1.Series(0).Active
        form1.DRAct.value = form1.TChart1.Series(1).Active
        form1.DSAct.value = form1.TChart1.Series(2).Active
        form1.DOAct.value = form1.TChart1.Series(3).Active
        form1.TotalAct.value = form1.TChart1.Series(4).Active
    End Sub
    
</script>

<body>
    <form id="form1" runat="server">
        <hPMISWEB:PageHeader ID="ph" runat="server" />
            <a name="#Home"></a>
            <div>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 150px;">
                <tr>
                    <td>
                        <strong>熱軋每日生產延誤資料</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        每日0700/1500/2300進行資料更新，並於每日1500將資料重新編排
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 185px;">
                <tr>
                    <td>
                        <asp:GridView ID="gvDaily" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                            <RowStyle CssClass="gvrs" />
                            <HeaderStyle CssClass="gvhs" />
                            <FooterStyle CssClass="gvfs" />
                            <PagerStyle CssClass="gvps" />
                            <SelectedRowStyle CssClass="gvsrs" />
                            <EditRowStyle CssClass="gvers" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 320px;">
                <tr>
                    <td>
                        <strong>熱軋當月生產延誤資料</strong></td>
                </tr>
                <tr>
                    <td>
                        資料於每日2300進行更新，並於當月第一天2300將資料重新編排
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 375px;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BackColor="gainsboro" Height="200px" Width="836px"
                            ScrollBars="Vertical">
                            <asp:GridView ID="gvMonth" runat="server" CellSpacing="1" CssClass="gv" GridLines="None">
                                <RowStyle CssClass="gvrs" />
                                <HeaderStyle CssClass="gvhs" />
                                <FooterStyle CssClass="gvfs" />
                                <PagerStyle CssClass="gvps" />
                                <SelectedRowStyle CssClass="gvsrs" />
                                <EditRowStyle CssClass="gvers" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td id="teech" class="teech">
                        <a name="#Chart"></a>
                        <object id="TChart1" classid="clsid:BDEB0088-66F9-4A55-ABD2-0BF8DEEC1196" width="836px" height="400"
                                    codebase="Teechart8.cab#version=8,0,0,3">
                        </object>  
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUp" runat="server" OnClick="btnUp_Click" Text="前一月" />
                        <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" Text="後一月" />
                        <input id="Radio1" type="radio" onclick="create_data(1)" />次數&nbsp;
                        <input id="Radio2" type="radio" onclick="create_data(2)" />分</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 355px;">
                <colgroup>
                    <col width="200" />
                    <col span="5" width="120" />
                </colgroup>
                <tr>
                    <td class="gvhs_data" style="width: 200px">
                        &nbsp;
                    </td>
                    <td class="gvhs_data" style="width: 120px; text-align: center;">
                        設備延誤 (DA)
                    </td>
                    <td class="gvhs_data" style="width: 120px; text-align: center">
                        換輥延誤 (DR)
                    </td>
                    <td class="gvhs_data" style="width: 120px; text-align: center">
                        計畫性延誤 (DS)
                    </td>
                    <td class="gvhs_data" style="width: 120px; text-align: center">
                        其他延誤 (DO)
                    </td>
                    <td class="gvhs_data" style="width: 120px; text-align: center">
                        總計</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                left: 100px; position: absolute; top: 570px;">
                <colgroup>
                    <col width="200" />
                    <col span="5" width="120" />
                </colgroup>
                <tr>
                    <td class="data">
                        <asp:Label ID="lblMonth" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>月統計
                    </td>
                    <td class="data">
                        <asp:Label ID="lblDA" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblDR" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblDS" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblDO" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                    <td class="data">
                        <asp:Label ID="lblTotal" runat="server" Text="N/A" CssClass="pmisdata"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <input id="hDA1" runat="server" enableviewstate="true" name="hDA1" type="hidden" />
        <input id="hDR1" runat="server" enableviewstate="true" name="hDR1" type="hidden" />
        <input id="hDS1" runat="server" enableviewstate="true" name="hDS1" type="hidden" />
        <input id="hDO1" runat="server" enableviewstate="true" name="hDO1" type="hidden" />
        <input id="hTotal1" runat="server" enableviewstate="true" name="hTotal1" type="hidden" />
        <input id="hDA2" runat="server" enableviewstate="true" name="hDA2" type="hidden" />
        <input id="hDR2" runat="server" enableviewstate="true" name="hDR2" type="hidden" />
        <input id="hDS2" runat="server" enableviewstate="true" name="hDS2" type="hidden" />
        <input id="hDO2" runat="server" enableviewstate="true" name="hDO2" type="hidden" />
        <input id="hTotal2" runat="server" enableviewstate="true" name="hTotal2" type="hidden" />
        <input id="hDate" runat="server" enableviewstate="true" name="hDate" type="hidden" />
        <input id="hStartDate" runat="server" enableviewstate="true" name="hStartDate" type="hidden" />
        <input id="hEndDate" runat="server" enableviewstate="true" name="hEndDate" type="hidden" />
        <input id="hAnc" runat="server" enableviewstate="true" name="hAnc" type="hidden" />
        <input id="DAAct" runat="server" enableviewstate="true" name="DAAct" type="hidden" value="1" />
        <input id="DRAct" runat="server" enableviewstate="true" name="DRAct" type="hidden" value="1" />
        <input id="DSAct" runat="server" enableviewstate="true" name="DSAct" type="hidden" value="1" />
        <input id="DOAct" runat="server" enableviewstate="true" name="DOAct" type="hidden" value="1" />
        <input id="TotalAct" runat="server" enableviewstate="true" name="TotalAct" type="hidden" value="1" />
    </form>
</body>
</html>
