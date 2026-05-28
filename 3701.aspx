<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3701.aspx.vb" Inherits="hPMISWEB._3701" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%--<%@Import Namespace="System.Data" %>>
<%@Import Namespace="System.Data.SqlClient" %>>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>熱軋全場固定式CO偵測圖</title>
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
    <style type="text/css">
        .auto-style4 {
            height: 65px;
            width: 3px;
        }
        .auto-style5 {
            height: 348px;
            width: 3px;
        }
        .auto-style6 {
            height: 65px;
            width: 434px;
        }
        .auto-style7 {
            height: 348px;
            width: 434px;
        }
        .auto-style8 {
            height: 536px;
            width: 550px;
        }
        .auto-style9 {
            width: 550px;
        }
        .auto-style10 {
            height: 65px;
        }
        .auto-style12 {
            height: 52px;
            width: 408px;
        }
        .auto-style13 {
            font-size: small;
        }
        .auto-style14 {
            font-size: small;
           
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td style="width: 48px; height: 536px">
                </td>
                <td style="width: 677px; height: 536px">
                    &nbsp;
                   
                    <img src="images/Y6P2.jpg" usemap="#Map" style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; width: 672px; height: 512px;" id="IMG1" onclick="return IMG1_onclick()" /></td>
         <map id="Map" name="Map" onmouseover="return Map_onmouseover()" onmouseout="return Map_onmouseout()">
          <area shape="rect" coords="190,380,248,452" alt="加熱爐固定式CO偵測圖" href="3702.aspx"  
       onmouseover="MM_showHideLayers('CO_FCE','','show')" 
           onmouseout="MM_showHideLayers('CO_FCE','','hide')" />
               <area shape="rect" coords="120,250,190,323" alt="型鋼加熱爐固定式CO偵測圖" href="3703.aspx"  
       onmouseover="MM_showHideLayers('CO_FCE_HBM','','show')" 
           onmouseout="MM_showHideLayers('CO_FCE_HBM','','hide')" />
                <td align="left" class="auto-style8">
                    &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="TimerALL" runat="server" Interval="10000">
                </asp:Timer>
                 <div id="CO_FCE" class="auto-style13"
    style="position:absolute; left:328px; top:560px; width:96px; height:88px; z-index:1; background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: hidden; border-left: black thin solid; border-bottom: black thin solid;">
  <p>
   熱軋加熱爐區域CO偵測最大值<br />
     <asp:Label ID="fceppm" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p>
</div>
                <div id="CO_FCE_HBM" class="auto-style14"
     style="position:absolute; left:255px; top:440px; width:96px; height:88px; z-index:1; background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: hidden; border-left: black thin solid; border-bottom: black thin solid;">
  <p>
   型鋼加熱爐區域CO偵測最大值<br />
     <asp:Label ID="hbmfceppm" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p>
</div>
            <table>
                        <tr>
                            <td style="height: 58px">
                            </td>
                            <td style="height: 58px; width: 3px;">
                            </td>
                            <td style="border-style: inset; background-color: gainsboro; text-align: left;" class="auto-style15">
                                <asp:TextBox ID="TextBox1" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="98%" CssClass="auto-style13">熱軋加熱爐區域</asp:TextBox><br class="auto-style13" />
                                <asp:TextBox ID="TextBox2" runat="server" BackColor="#FF8000" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">CO偵測最大值</asp:TextBox><asp:TextBox ID="TextBox3" runat="server" BackColor="#FF8000" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">環境狀況</asp:TextBox><asp:TextBox ID="TextBox4" runat="server" AutoCompleteType="Cellular" BackColor="#FF8000"
                                    Font-Bold="True" Font-Names="微軟正黑體" ReadOnly="True" Style="text-align: center"
                                    Width="32%" CssClass="auto-style13">通訊狀況</asp:TextBox>
                                <asp:TextBox ID="TextBox5" runat="server" BackColor="Lime" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">0 ppm</asp:TextBox><asp:TextBox ID="TextBox6" runat="server" BackColor="Lime" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">安全</asp:TextBox><asp:TextBox ID="TextBox7" runat="server" AutoCompleteType="Cellular" BackColor="Lime"
                                    Font-Bold="True" Font-Names="微軟正黑體" ReadOnly="True" Style="text-align: center"
                                    Width="32%" ForeColor="Black" CssClass="auto-style13">正常</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 58px">
                            </td>
                            <td style="height: 58px; width: 3px;">
                            </td>
                            <td style="border-style: inset; background-color: gainsboro; text-align: left;" class="auto-style15">
                                <asp:TextBox ID="TextBox8" runat="server" BackColor="Yellow" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="98%" CssClass="auto-style13">型鋼加熱爐區域</asp:TextBox><br class="auto-style13" />
                                <asp:TextBox ID="TextBox9" runat="server" BackColor="#FF8000" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">CO偵測最大值</asp:TextBox><asp:TextBox ID="TextBox10" runat="server" BackColor="#FF8000" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">環境狀況</asp:TextBox><asp:TextBox ID="TextBox11" runat="server" AutoCompleteType="Cellular" BackColor="#FF8000"
                                    Font-Bold="True" Font-Names="微軟正黑體" ReadOnly="True" Style="text-align: center"
                                    Width="32%" CssClass="auto-style13">通訊狀況</asp:TextBox>
                                <asp:TextBox ID="TextBox12" runat="server" BackColor="Lime" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">0 ppm</asp:TextBox><asp:TextBox ID="TextBox13" runat="server" BackColor="Lime" Font-Bold="True" Font-Names="微軟正黑體"
                                    ReadOnly="True" Style="text-align: center" Width="31%" CssClass="auto-style13">安全</asp:TextBox><asp:TextBox ID="TextBox14" runat="server" AutoCompleteType="Cellular" BackColor="Lime"
                                    Font-Bold="True" Font-Names="微軟正黑體" ReadOnly="True" Style="text-align: center"
                                    Width="32%" ForeColor="Black" CssClass="auto-style13">正常</asp:TextBox></td>
                        </tr>
                        <tr>
                           <%-- <td class="auto-style10"></td>
                            <td class="auto-style4"></td>--%>
                            <%--<td class="auto-style6">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" Text="熱軋資料更新時間：" CssClass="auto-style13"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" CssClass="auto-style13"></asp:Label>
                                <br class="auto-style13" />
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" Text="型鋼資料更新時間：" CssClass="auto-style13"></asp:Label>
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" CssClass="auto-style13"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="height: 348px">
                            </td>
                            <td class="auto-style5">
                            </td>
                            <td style="vertical-align: top; text-align: left;" class="auto-style7">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Black" CssClass="auto-style13">CO偵測值說明：</asp:Label><br class="auto-style13" />
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="blue" CssClass="auto-style13">[ 藍色：< 35 ppm | </asp:Label>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="red" CssClass="auto-style13">紅色：>= 35 ppm ]</asp:Label><br class="auto-style13" />
                            
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/North.JPG" Width="176px" />
                                </span></td>
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 48px">
                </td>
                <td style="width: 677px">
                </td>
                <td class="auto-style9">
                </td>
            </tr>
            <tr>
                <td style="width: 48px">
                </td>
                <td style="width: 677px">
                </td>
                <td class="auto-style9">
                </td>
            </tr>
        </table>
    <br />
     
        <%--<img src="images/Y6P2.JPG" border="0" usemap="#Map" alt="" style="z-index: -100; left: 56px; position: absolute; top: 139px; right: 273px; width: 635px; height: 772px;" />--%>
      
        <!--onmouseover="MM_showHideLayers('PH1-COP','','show')" -->
        <!--onmouseout="MM_showHideLayers('PH1-COP','','hide')" />-->
    </form>
</body>
</html>
