<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3703.aspx.vb" Inherits="hPMISWEB._3703" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv ="refresh" content ="60"/>
    <title>加熱爐固定式CO偵測圖</title>
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    
<script language="javascript" type="text/javascript">
// <!CDATA[

function IMG1_onclick() {

}
<!--
function MM_findObj(n, d) { //v4.01
 var p,i,x; if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
 d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
 if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
 for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
 if(!x && d.getElementById) x=d.getElementById(n); return x;
}
function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}
//-->


// ]]>
</script>

<script language="vbscript" type="text/vbscript">
   
         Sub TChart1_OnDblClick()
            TChart2_OnDblClick()
            form1.TChart1.ShowEditor
            form1.TChart1.StopMouse
            form1.TChart2.ShowEditor
            form1.TChart2.StopMouse
        End Sub

         Sub window_onload()
            
           showChart()
            
         End Sub     
          
         Sub showChart()
           Dim arrData
             Dim arrData1
             form1.TChart1.Aspect.View3D=False '關閉3D圖型
             form1.TChart1.Legend.Visible = False '關閉圖例
             form1.TChart1.Axis.Visible = False '關閉 xy 軸
             form1.TChart1.Header.Visible = 0'關閉 Title
             
                         
             form1.TChart1.AddSeries(22) '增加圖型 WindRose代碼為22
             form1.TChart1.Series(0).asWindRose.Circled = True '固定為正圓型
             form1.TChart1.Series(0).asWindRose.Pointer.Visible = 1 '開關: 資料點
             form1.TChart1.Series(0).asWindRose.AngleIncrement = 45 '角度增量:每45度為一個角度
             form1.TChart1.Series(0).asWindRose.RadiusIncrement = 1 '半徑增量
             form1.TChart1.Series(0).asWindRose.RotationAngle = 21.5 '方位角度 90度為 N 向上
             form1.TChart1.Series(0).asWindRose.CircleLabels.Font.Size = 10 '方位字体大小   
             
             form1.TChart2.Aspect.View3D=False '關閉3D圖型
             form1.TChart2.Legend.Visible = False '關閉圖例
             form1.TChart2.Axis.Visible = False '關閉 xy 軸
             form1.TChart2.Header.Visible = 0'關閉 Title
             
                         
             form1.TChart2.AddSeries(22) '增加圖型 WindRose代碼為22
             form1.TChart2.Series(0).asWindRose.Circled = True '固定為正圓型
             form1.TChart2.Series(0).asWindRose.Pointer.Visible = 1 '開關: 資料點
             form1.TChart2.Series(0).asWindRose.AngleIncrement = 45 '角度增量:每45度為一個角度
             form1.TChart2.Series(0).asWindRose.RadiusIncrement = 1 '半徑增量
             form1.TChart2.Series(0).asWindRose.RotationAngle = 21.5 '方位角度 90度為 N 向上
             form1.TChart2.Series(0).asWindRose.CircleLabels.Font.Size = 10 '方位字体大小      
                       
             
             form1.TChart1.Series(0).asWindRose.Pen.Width = 3 '線寬
             form1.TChart2.Series(0).asWindRose.Pen.Width = 3 '線寬

             arrData = split(form1.wind_direction_W.value, "")
            
             '加入資料 
             form1.TChart1.Series(0).AddXY 0, 0, "T1", &H0000FF
             form1.TChart1.Series(0).AddXY arrData(0), 1, "T2", &H0000FF
             
              arrData1 = split(form1.wind_direction_E.value, "")
            
             '加入資料 
             form1.TChart2.Series(0).AddXY 0, 0, "T3", &H0000FF
             form1.TChart2.Series(0).AddXY arrData1(0), 1, "T4", &H0000FF
         End sub
         
                
    </script>
    <%-- <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />--%>
    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style4 {
            height: 20px;
            width: 229px;
            font-size: small;
        }
        .auto-style5 {
            width: 229px;
            font-size: small;
        }
        .auto-style6 {
            height: 23px;
            width: 229px;
            font-size: small;
        }
        .auto-style7 {
            height: 21px;
            width: 229px;
            font-size: small;
        }
        .auto-style8 {
            height: 22px;
            width: 229px;
            font-size: small;
        }
        .auto-style9 {
            height: 548px;
            width: 576px;
            font-size: small;
        }
        .auto-style10 {
            width: 576px;
        }
        .auto-style12 {
            position: absolute;
            left: 510px;
            width: 40px;
            top: 755px;
            height: 40px;
            font-family: 微軟正黑體;
            font-size: small;
        }
        .auto-style13 {
            right: 946px;
            font-family: 微軟正黑體;
            font-size: small;
        }
        .auto-style14 {
            position: absolute;
            left: 208px;
            width: 40px;
            top: 384px;
            height: 40px;
            font-family: 微軟正黑體;
            font-size: small;
        }
        .auto-style16 {
            position: absolute;
            left: 150px;
            width: 40px;
            top: 600px;
            height: 40px;
            font-family: 微軟正黑體;
            font-size: small;
        }
        .auto-style18 {
            width: 10px;
            height: 548px;
        }
        .auto-style19 {
            width: 340px;
            height: 548px;
        }
        .auto-style20 {
            font-family: 微軟正黑體;
        }
        .auto-style21 {
            font-family: 微軟正黑體;
            font-size: small;
        }
        .auto-style22 {
            font-size: small;
        }
    </style>
</head>
<%--<body onload="alert('CO訊號預計建立日期：101.12.25');">--%>
<body>
    <form id="form1" runat="server">
                       
        <input id="wind_direction_E" runat="server" enableviewstate="true" name="wind_direction_E" type="hidden" class="auto-style21" />
    <hPMISWEB:PageHeader ID="ph" runat ="server" />
        <span class="auto-style21">
        <br />
        <br />
        
        </span>
        
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
            <ContentTemplate>
                &nbsp;<asp:Timer ID="Timer1" runat="server" Interval="10000">
                </asp:Timer>
                <div id="D_AT-2110" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2001','','hide')" onmouseover="MM_showHideLayers('S_1GIA2001','','show')" style="position:absolute; left: 700px; width: 40px; top: 332px; height: 40px;">
                    <asp:Label ID="P_1GIA2001" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="1" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2001" class="auto-style21" style="position:absolute; left:368px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        1GIA2001
                        <asp:Label ID="V_1GIA2001" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2115" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2104','','hide')" onmouseover="MM_showHideLayers('S_1GIA2104','','show')" style="position:absolute; left: 480px; width: 40px; top: 677px; height: 40px;">
                    <asp:Label ID="P_1GIA2104" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="6" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2104" class="auto-style21" style="position:absolute; left:488px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;z-index: 1;">
                    <p>
                        1GIA2104
                        <asp:Label ID="V_1GIA2104" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2114" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2103','','hide')" onmouseover="MM_showHideLayers('S_1GIA2103','','show')" style="position:absolute; left: 475px; width: 40px; top: 618px; height: 40px;">
                    <asp:Label ID="P_1GIA2103" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="5" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2103" class="auto-style21" style="position:absolute; left:392px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        1GIA2103
                        <asp:Label ID="V_1GIA2103" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2122" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2101','','hide')" onmouseover="MM_showHideLayers('S_2GIA2101','','show')" style="position:absolute; left: 445px; width: 40px; top: 375px; height: 40px;">
                    <asp:Label ID="P_2GIA2101" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                            text-align: center" Text="13" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2101" class="auto-style21" style="position:absolute; left:144px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2101
                        <asp:Label ID="V_2GIA2101" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2129" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2108','','hide')" onmouseover="MM_showHideLayers('S_2GIA2108','','show')" style="position:absolute; left: 380px; width: 40px; top: 352px; height: 40px;">
                    <asp:Label ID="P_2GIA2108" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="20" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2108" class="auto-style21" style="position:absolute; left:272px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2108
                        <asp:Label ID="V_2GIA2108" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2128" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2107','','hide')" onmouseover="MM_showHideLayers('S_2GIA2107','','show')" style="position:absolute; left: 466px; width: 40px; top: 375px; height: 40px;">
                    <asp:Label ID="P_2GIA2107" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="19" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2107" class="auto-style21" style="position:absolute; left:144px; top:352px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;z-index: 1;">
                    <p>
                        2GIA2107
                        <asp:Label ID="V_2GIA2107" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2126" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2105','','hide')" onmouseover="MM_showHideLayers('S_2GIA2105','','show')" style="position:absolute; left: 75px; width: 40px; top: 460px; height: 40px;">
                    <asp:Label ID="P_2GIA2105" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="17" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2105" class="auto-style21" style="position:absolute; left:144px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2105
                        <asp:Label ID="V_2GIA2105" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2130" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2001','','hide')" onmouseover="MM_showHideLayers('S_3GIA2001','','show')" style="position:absolute; left: 274px; width: 40px; top: 600px; height: 40px;">
                    <asp:Label ID="P_3GIA2001" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="21" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2001" class="auto-style21" style="position:absolute; left:8px; top:560px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2001
                        <asp:Label ID="V_3GIA2001" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2131" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2002','','hide')" onmouseover="MM_showHideLayers('S_3GIA2002','','show')" style="position:absolute; left: 438px; width: 40px; top: 600px; height: 40px;">
                    <asp:Label ID="P_3GIA2002" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="22" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2002" class="auto-style21" style="position:absolute; left:128px; top:576px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2002
                        <asp:Label ID="V_3GIA2002" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2132" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2101','','hide')" onmouseover="MM_showHideLayers('S_3GIA2101','','show')" style="position:absolute; left: 384px; width: 40px; top: 470px; height: 40px;">
                    <asp:Label ID="P_3GIA2101" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="23" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2101" class="auto-style21" style="position:absolute; left:104px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2101
                        <asp:Label ID="V_3GIA2101" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2133" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2102','','hide')" onmouseover="MM_showHideLayers('S_3GIA2102','','show')" style="position:absolute; left: 332px; width: 40px; top: 390px; height: 40px;">
                    <asp:Label ID="P_3GIA2102" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="24" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2102" class="auto-style21" style="position:absolute; left:144px; top:560px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2102
                        <asp:Label ID="V_3GIA2102" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2134" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2103','','hide')" onmouseover="MM_showHideLayers('S_3GIA2103','','show')" style="position:absolute; left: 352px; width: 40px; top: 390px; height: 40px;">
                    <asp:Label ID="P_3GIA2103" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="25" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2103" class="auto-style21" style="position:absolute; left:8px; top:480px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2103
                        <asp:Label ID="V_3GIA2103" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2135" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2104','','hide')" onmouseover="MM_showHideLayers('S_3GIA2104','','show')" style="position:absolute; left: 445px; width: 40px; top: 398px; height: 40px;">
                    <asp:Label ID="P_3GIA2104" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="26" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2104" class="auto-style21" style="position:absolute; left:112px; top:480px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2104
                        <asp:Label ID="V_3GIA2104" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2136" class="auto-style13" onmouseout="MM_showHideLayers('S_3GIA2105','','hide')" onmouseover="MM_showHideLayers('S_3GIA2105','','show')" style="position:absolute; left: 445px; width: 40px; top: 420px; height: 40px;">
                    <asp:Label ID="P_3GIA2105" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="27" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2105" class="auto-style21" style="position:absolute; left:8px; top:432px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2105
                        <asp:Label ID="V_3GIA2105" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2137" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2106','','hide')" onmouseover="MM_showHideLayers('S_3GIA2106','','show')" style="position:absolute; left: 467px; width: 40px; top: 420px; height: 40px;">
                    <asp:Label ID="P_3GIA2106" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="28" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2106" class="auto-style21" style="position:absolute; left:112px; top:432px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2106
                        <asp:Label ID="V_3GIA2106" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2138" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2107','','hide')" onmouseover="MM_showHideLayers('S_3GIA2107','','show')" style="position:absolute; left: 332px; width: 40px; top: 570px; height: 40px;">
                    <asp:Label ID="P_3GIA2107" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="29" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2107" class="auto-style21" style="position:absolute; left:8px; top:384px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2107
                        <asp:Label ID="V_3GIA2107" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2139" class="auto-style14" onmouseout="MM_showHideLayers('S_3GIA2108','','hide')" onmouseover="MM_showHideLayers('S_3GIA2108','','show')" style="position:absolute; left: 352px; width: 40px; top: 570px; height: 40px;">
                    <asp:Label ID="P_3GIA2108" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="30" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2108" class="auto-style21" style="position:absolute; left:112px; top:384px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2108
                        <asp:Label ID="V_3GIA2108" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2121" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2002','','hide')" onmouseover="MM_showHideLayers('S_2GIA2002','','show')" style="position:absolute; left: 437px; width: 40px; top: 575px; height: 40px;">
                    <asp:Label ID="P_2GIA2002" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="12" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2002" class="auto-style21" style="position:absolute; left:272px; top:584px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2002
                        <asp:Label ID="V_2GIA2002" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2120" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2001','','hide')" onmouseover="MM_showHideLayers('S_2GIA2001','','show')" style="position:absolute; left: 467px; width: 40px; top: 396px; height: 40px;">
                    <asp:Label ID="P_2GIA2001" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="11" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2001" class="auto-style21" style="position:absolute; left:240px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2001
                        <asp:Label ID="V_2GIA2001" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2111" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2002','','hide')" onmouseover="MM_showHideLayers('S_1GIA2002','','show')" style="position:absolute; left: 665px; width: 40px; top: 332px; height: 40px;">
                    <asp:Label ID="P_1GIA2002" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                        border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                         text-align: center" Text="2" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2002" class="auto-style21" style="position:absolute; left:392px; top:584px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        1GIA2002
                        <asp:Label ID="V_1GIA2002" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2113" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2102','','hide')" onmouseover="MM_showHideLayers('S_1GIA2102','','show')" style="position:absolute; left: 458px; width: 40px; top: 677px; height: 40px;">
                    <asp:Label ID="P_1GIA2102" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="4" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2102" class="auto-style21" style="position:absolute; left:488px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;">
                    <p>
                        1GIA2102
                        <asp:Label ID="V_1GIA2102" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2112" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2101','','hide')" onmouseover="MM_showHideLayers('S_1GIA2101','','show')" style="position:absolute; left: 500px; width: 40px; top: 618px; height: 40px;">
                    <asp:Label ID="P_1GIA2101" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                                border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                                 text-align: center" Text="3" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2101" class="auto-style21" style="position:absolute; left:392px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        1GIA2101
                        <asp:Label ID="V_1GIA2101" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2124" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2103','','hide')" onmouseover="MM_showHideLayers('S_2GIA2103','','show')" style="position:absolute; left: 510px; width: 40px; top: 600px; height: 40px;">
                    <asp:Label ID="P_2GIA2103" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="15" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2103" class="auto-style21" style="position:absolute; left:144px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2103
                        <asp:Label ID="V_2GIA2103" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2117" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2106','','hide')" onmouseover="MM_showHideLayers('S_1GIA2106','','show')" style="position:absolute; left: 485px; width: 40px; top: 655px; height: 40px;">
                    <asp:Label ID="P_1GIA2106" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="8" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2106" class="auto-style21" style="position:absolute; left:488px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;">
                    <p>
                        1GIA2106
                        <asp:Label ID="V_1GIA2106" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2116" class="auto-style12" onmouseout="MM_showHideLayers('S_1GIA2105','','hide')" onmouseover="MM_showHideLayers('S_1GIA2105','','show')" style="position: absolute; left: 505px; width: 40px; top: 655px; height: 40px;">
                    <asp:Label ID="P_1GIA2105" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="7" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2105" class="auto-style21" onclick="return S_1GIA2105_onclick()" style="position:absolute; left:392px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;">
                    <p>
                        1GIA2105
                        <asp:Label ID="V_1GIA2105" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2119" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2108','','hide')" onmouseover="MM_showHideLayers('S_1GIA2108','','show')" style="position:absolute; left: 475px; width: 40px; top: 555px; height: 40px;">
                    <asp:Label ID="P_1GIA2108" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="10" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2108" class="auto-style21" style="position:absolute; left:488px; top:352px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;">
                    <p>
                        1GIA2108
                        <asp:Label ID="V_1GIA2108" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2125" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2104','','hide')" onmouseover="MM_showHideLayers('S_2GIA2104','','show')" style="position:absolute; left: 340px; width: 40px; top: 620px; height: 40px;">
                    <asp:Label ID="P_2GIA2104" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="16" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2104" class="auto-style21" style="position:absolute; left:248px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2104
                        <asp:Label ID="V_2GIA2104" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2127" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2106','','hide')" onmouseover="MM_showHideLayers('S_2GIA2106','','show')" style="position:absolute; left: 460px; width: 40px; top: 575px; height: 40px;">
                    <asp:Label ID="P_2GIA2106" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="18" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2106" class="auto-style21" style="position:absolute; left:248px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2106
                        <asp:Label ID="V_2GIA2106" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2123" class="auto-style21" onmouseout="MM_showHideLayers('S_2GIA2102','','hide')" onmouseover="MM_showHideLayers('S_2GIA2102','','show')" style="position:absolute; left: 500px; width: 40px; top: 396px; height: 40px;">
                    <asp:Label ID="P_2GIA2102" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="14" Width="16px"></asp:Label>
                </div>
                <div id="S_2GIA2102" class="auto-style21" style="position:absolute; left:248px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        2GIA2102
                        <asp:Label ID="V_2GIA2102" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2118" class="auto-style21" onmouseout="MM_showHideLayers('S_1GIA2107','','hide')" onmouseover="MM_showHideLayers('S_1GIA2107','','show')" style="position:absolute; left: 415px; width: 40px; top: 565px; height: 40px;">
                    <asp:Label ID="P_1GIA2107" runat="server" BackColor="Yellow" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="9" Width="16px"></asp:Label>
                </div>
                <div id="S_1GIA2107" class="auto-style21" style="position:absolute; left:368px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;">
                    <p>
                        1GIA2107
                        <asp:Label ID="V_1GIA2107" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2140" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2003','','hide')" onmouseover="MM_showHideLayers('S_3GIA2003','','show')" style="position:absolute; left: 455px; width: 40px; top: 555px; height: 40px;">
                    <asp:Label ID="P_3GIA2003" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="31" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2003" class="auto-style21" style="position:absolute; left:72px; top:728px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2003
                        <asp:Label ID="V_3GIA2003" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2141" class="auto-style21" onmouseout="MM_showHideLayers('S_3GIA2004','','hide')" onmouseover="MM_showHideLayers('S_3GIA2004','','show')" style="position:absolute; left: 455px; width: 40px; top: 535px; height: 40px;">
                    <asp:Label ID="P_3GIA2004" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="32" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2004" class="auto-style21" style="position:absolute; left:72px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;">
                    <p>
                        3GIA2004
                        <asp:Label ID="V_3GIA2004" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <div id="D_AT-2142" class="auto-style16" onmouseout="MM_showHideLayers('S_3GIA2005','','hide')" onmouseover="MM_showHideLayers('S_3GIA2005','','show')" style="position:absolute; left: 455px; width: 40px; top: 515px; height: 40px;">
                    <asp:Label ID="P_3GIA2005" runat="server" BackColor="Yellow" Height="16px" style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Text="33" Width="16px"></asp:Label>
                </div>
                <div id="S_3GIA2005" class="auto-style21" style="position:absolute; left:424px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps; z-index: 1;">
                    <p>
                        3GIA2005
                        <asp:Label ID="V_3GIA2005" runat="server" ForeColor="Blue" Style="text-align: center" Text="N/A"></asp:Label>
                        ppm</p>
                </div>
                <table style="width: 968px">
                    <tr>
                        <td class="auto-style18"></td>
                        <td class="auto-style9" style="vertical-align: top;"><span class="auto-style20">
                            <img src="images/HBMFCE_3.jpg" usemap="#Map" style="border-style: none; vertical-align: top;" id="IMG1" onclick="return IMG1_onclick()" />
                            <br />
                            </span><span class="auto-style20">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span></td>
                        <%--<td style="width: 293px; height: 536px" align="left">
                </td>--%>
                        <td class="auto-style19" style="vertical-align: top; ">
                            <table style="width: 344px">
                                <tr>
                                    <td style="width: 14px"></td>
                                    <td style="width: 281px">
                                        <input id="wind_direction_W" runat="server" enableviewstate="true" name="wind_direction_W" type="hidden" class="auto-style21" />
                                        &nbsp;<span class="auto-style20"><span class="auto-style22"><br />
                                        <br />
                                        </span></span><span class="auto-style20"><span class="auto-style22">&nbsp;
                                        <br />
                                        <br />
                                        </span></span></td>
                                    <td style="width: 73px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 14px"></td>
                                    <td class="auto-style21" style="width: 281px; background-color: #ffff33">加熱爐固定式CO偵測最大值：<asp:Label ID="Total_CO" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        ppm</td>

                                    <td style="width: 73px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 14px"></td>
                                    <td style="width: 281px"></td>
                                    <td style="width: 73px"></td>
                                </tr>
                            </table>
                            <table style="text-align: left;">
                                <tr>
                                    <td class="auto-style21" style="width: 30px;text-align: center">
                                        <asp:Label ID="IT_01" runat="server" BackColor="Yellow" ForeColor="ControlText" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="1" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style4" style="text-align: left">
                                        <asp:Label ID="L1" runat="server" CssClass="auto-style20" Style="color: #ff9933; text-align: left;" Text="AT-2110"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V1" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 20px">&nbsp;<span class="auto-style22"><asp:Label ID="Label5" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_02" runat="server" BackColor="Yellow" ForeColor="Black" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="2" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L2" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2111"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V2" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label6" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_03" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="3" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L3" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2112"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V3" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label14" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_04" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="4" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L4" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2113"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V4" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label19" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_05" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="5" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L5" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2114"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V5" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label24" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_06" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="6" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L6" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2115"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V6" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label29" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_07" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="7" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L7" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2116"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V7" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label34" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_08" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="8" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L8" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2117"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V8" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label39" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_09" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="9" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L9" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2118"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V9" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label54" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_10" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="10" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L10" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2119"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V10" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label59" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_11" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="11" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L11" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2120"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V11" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label64" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_12" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="12" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L12" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2121"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V12" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label69" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_13" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="13" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L13" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2122"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V13" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label74" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_14" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="14" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L14" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2123"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V14" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label79" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_15" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="15" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L15" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2124"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V15" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label84" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_16" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="16" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L16" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2125"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V16" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label89" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_17" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="17" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L17" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2126"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V17" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label94" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_18" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="18" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L18" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2127"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V18" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label99" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_19" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="19" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L19" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2128"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V19" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label104" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_20" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="20" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L20" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2129"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V20" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<span class="auto-style22"><asp:Label ID="Label109" runat="server" CssClass="auto-style20" Text="ppm"></asp:Label>
                                        </span><span class="auto-style20"><span class="auto-style22">&nbsp; </span></span></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px;text-align: center">
                                        <asp:Label ID="IT_21" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="21" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style6" style="text-align: left">
                                        <asp:Label ID="L21" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2130"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V21" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 23px">&nbsp;<asp:Label ID="Label2" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_22" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="22" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L22" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2131"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V22" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label4" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px;text-align: center">
                                        <asp:Label ID="IT_23" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="23" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style6" style="text-align: left">
                                        <asp:Label ID="L23" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2132"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V23" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 23px">&nbsp;<asp:Label ID="Label7" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_24" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="24" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L24" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2133"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V24" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label8" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_25" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="25" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L25" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2134"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V25" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label9" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_26" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="26" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L26" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2135"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V26" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label10" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_27" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="27" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L27" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2136"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V27" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label11" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_28" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="28" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L28" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2137"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V28" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label12" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_29" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="29" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L29" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2138"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V29" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label13" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px;text-align: center">
                                        <asp:Label ID="IT_30" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="30" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style7" style="text-align: left">
                                        <asp:Label ID="L30" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2139"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V30" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 21px">&nbsp;<asp:Label ID="Label15" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px;text-align: center">
                                        <asp:Label ID="IT_31" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="31" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style8" style="text-align: left">
                                        <asp:Label ID="L31" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2140"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V31" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 22px">&nbsp;<asp:Label ID="Label16" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_32" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="32" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style5" style="text-align: left">
                                        <asp:Label ID="L21L32" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2141"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V32" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px">&nbsp;<asp:Label ID="Label17" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="width: 30px; text-align: center">
                                        <asp:Label ID="IT_33" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="33" Width="24px"></asp:Label>
                                    </td>
                                    <td class="auto-style7" style="text-align: left">
                                        <asp:Label ID="L33" runat="server" CssClass="auto-style20" Style="color: #ff9933" Text="AT-2142"></asp:Label>
                                        <span class="auto-style20">&nbsp;
                                        <asp:Label ID="V33" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                        </span></td>
                                    <td style="width: 160px; height: 21px">&nbsp;<asp:Label ID="Label18" runat="server" CssClass="auto-style21" Text="ppm"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <span class="auto-style22">
                            <br class="auto-style20" />
                            </span><span class="auto-style20"><span class="auto-style22">&nbsp; &nbsp;&nbsp; </span>
                              <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Black" CssClass="auto-style13">CO偵測值說明：</asp:Label><br class="auto-style13" />
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="blue" CssClass="auto-style13">[ 藍色：< 35 ppm | </asp:Label>
                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="red" CssClass="auto-style13">紅色：>= 35 ppm ]</asp:Label><br class="auto-style13" />
                            <span class="auto-style22">
                            <br />
                            <br />
                            </span><span class="auto-style22">&nbsp; &nbsp; &nbsp;</span><asp:Label ID="Label49" runat="server" CssClass="auto-style22" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" Text="資料更新時間："></asp:Label>
                            <span class="auto-style22">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp; &nbsp; &nbsp;</span><asp:Label ID="Fn1" runat="server" CssClass="auto-style22" Font-Bold="True" Font-Size="Small" ForeColor="Black">HBM FCE：</asp:Label>
                            <asp:Label ID="Last_time_1" runat="server" CssClass="auto-style22" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                            <br class="auto-style22" />
                            <span class="auto-style22">&nbsp; &nbsp;&nbsp;
                            <br />
                            &nbsp; &nbsp;&nbsp;
                            <br />
                            &nbsp; &nbsp;&nbsp;
                            <br />
                            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br /> &nbsp; &nbsp; &nbsp;
                            <br />
                            &nbsp; &nbsp; &nbsp;
                            <br />
                            &nbsp; &nbsp; &nbsp;
                            <br />
                            <br />
                            <br />
                            </span></span></td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td class="auto-style10"></td>
                        <td style="width: 340px"></td>
                    </tr>
                    <caption class="auto-style21">
                        &nbsp;
                    </caption>
                </table>
        
          </ContentTemplate>
        </asp:UpdatePanel>
     </div>   
    </form>
</body>
</html>
