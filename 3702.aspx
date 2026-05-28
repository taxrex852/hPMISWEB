<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="3702.aspx.vb" Inherits="hPMISWEB._HSM3702" %>
<%@ Register TagPrefix="hPMISWEB" TagName="PageHeader" Src="~/include/header.ascx" %>
<%@ Register assembly="TeeChart" namespace="Steema.TeeChart.Web" tagprefix="tchart" %>
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

</script>

    <link href="/css/diagram.css" media="all" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            font-size: small;
        }
        .auto-style2 {
            width: 603px;
            height: 676px;
            vertical-align: top;
            font-size: small;
            font-family: 微軟正黑體;
        }
        .auto-style3 {
            font-family: 微軟正黑體;
        }
        .auto-style4 {
            font-size: small;
            font-family: 微軟正黑體;
        }
        .auto-style5 {
            font-family: 微軟正黑體;
            font-weight: normal;
        }
        .auto-style6 {
            font-weight: normal;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <hPMISWEB:PageHeader ID="ph" runat ="server" />
        <br />
        <br />
        
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
            <ContentTemplate>
                <span class="auto-style4">&nbsp;</span><asp:Timer ID="Timer1" runat="server" Interval="10000">
                </asp:Timer>
               
                 <div id="D_1GIA2001" class ="auto-style4" style="position:absolute; left: 368px; width: 40px; top: 560px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_1GIA2001','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2001','','hide')">
                <asp:Label ID="P_1GIA2001" runat="server" Text="1" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_1GIA2001" class="auto-style4"
    style="position:absolute; left:368px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>1GIA2001
     <asp:Label ID="V_1GIA2001" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                
          
           <div id="D_1GIA2104" class ="auto-style4" style="position:absolute; left: 464px; width: 40px; top: 480px; height: 40px;"
                      onmouseover="MM_showHideLayers('S_1GIA2104','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2104','','hide')">
                        <asp:Label ID="P_1GIA2104" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="6" Width="16px" BackColor="Yellow"></asp:Label></div>
                              <div id="S_1GIA2104" class="auto-style4"
    style="position:absolute; left:488px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;z-index: 1;"><p>1GIA2104
     <asp:Label ID="V_1GIA2104" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_1GIA2103" class ="auto-style4" style="position:absolute; left: 368px; width: 40px; top: 480px; height: 40px;"
                      onmouseover="MM_showHideLayers('S_1GIA2103','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2103','','hide')">
                        <asp:Label ID="P_1GIA2103" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="5" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_1GIA2103" class="auto-style4"
    style="position:absolute; left:392px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>1GIA2103
     <asp:Label ID="V_1GIA2103" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
          
          
                    <div id="D_2GIA2101" class ="auto-style4" style="position:absolute; left: 240px; width: 40px; top: 536px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2101','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2101','','hide')">
                        <asp:Label ID="P_2GIA2101" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                            text-align: center" Text="13" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_2GIA2101" class="auto-style4"
    style="position:absolute; left:144px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2101
     <asp:Label ID="V_2GIA2101" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_2GIA2108" class ="auto-style4" style="position:absolute; left: 336px; width: 40px; top: 384px; height: 40px;"
                      onmouseover="MM_showHideLayers('S_2GIA2108','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2108','','hide')">
                        <asp:Label ID="P_2GIA2108" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="20" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_2GIA2108" class="auto-style4"
    style="position:absolute; left:272px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2108
     <asp:Label ID="V_2GIA2108" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_2GIA2107" class ="auto-style4" style="position:absolute; left: 240px; width: 40px; top: 384px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2107','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2107','','hide')">
                        <asp:Label ID="P_2GIA2107" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="19" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_2GIA2107" class="auto-style4"
    style="position:absolute; left:144px; top:352px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;z-index: 1;"><p>2GIA2107
     <asp:Label ID="V_2GIA2107" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_2GIA2105" class ="auto-style4" style="position:absolute; left: 240px; width: 40px; top: 432px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2105','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2105','','hide')">
                        <asp:Label ID="P_2GIA2105" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="17" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_2GIA2105" class="auto-style4"
    style="position:absolute; left:144px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2105
     <asp:Label ID="V_2GIA2105" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
            <div id="D_3GIA2001" class ="auto-style4" style="position:absolute; left: 104px; width: 40px; top: 560px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2001','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2001','','hide')">
                <asp:Label ID="P_3GIA2001" runat="server" Text="21" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2001" class="auto-style4"
    style="position:absolute; left:8px; top:560px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2001
     <asp:Label ID="V_3GIA2001" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
  
    <div id="D_3GIA2002" class ="auto-style4" style="position:absolute; left: 128px; width: 40px; top: 552px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2002','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2002','','hide')">
                <asp:Label ID="P_3GIA2002" runat="server" Text="22" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2002" class="auto-style4"
    style="position:absolute; left:128px; top:576px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2002
     <asp:Label ID="V_3GIA2002" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
           <div id="D_3GIA2101" class ="auto-style4" style="position:absolute; left: 104px; width: 40px; top: 536px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2101','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2101','','hide')">
                <asp:Label ID="P_3GIA2101" runat="server" Text="23" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2101" class="auto-style4"
    style="position:absolute; left:104px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2101
     <asp:Label ID="V_3GIA2101" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
          <div id="D_3GIA2102" class ="auto-style4" style="position:absolute; left: 208px; width: 40px; top: 536px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2102','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2102','','hide')">
                <asp:Label ID="P_3GIA2102" runat="server" Text="24" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2102" class="auto-style4"
    style="position:absolute; left:144px; top:560px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2102
     <asp:Label ID="V_3GIA2102" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
            <div id="D_3GIA2103" class ="auto-style4" style="position:absolute; left: 104px; width: 40px; top: 480px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2103','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2103','','hide')">
                <asp:Label ID="P_3GIA2103" runat="server" Text="25" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2103" class="auto-style4"
    style="position:absolute; left:8px; top:480px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2103
     <asp:Label ID="V_3GIA2103" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
           <div id="D_3GIA2104" class ="auto-style4" style="position:absolute; left: 208px; width: 40px; top: 480px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2104','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2104','','hide')">
                <asp:Label ID="P_3GIA2104" runat="server" Text="26" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2104" class="auto-style4"
    style="position:absolute; left:112px; top:480px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2104
     <asp:Label ID="V_3GIA2104" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
          <div id="D_3GIA2105" class ="auto-style4" style="position:absolute; left: 104px; width: 40px; top: 432px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2105','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2105','','hide')">
                <asp:Label ID="P_3GIA2105" runat="server" Text="27" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2105" class="auto-style4"
    style="position:absolute; left:8px; top:432px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2105
     <asp:Label ID="V_3GIA2105" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
          <div id="D_3GIA2106" class ="auto-style4" style="position:absolute; left: 208px; width: 40px; top: 432px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2106','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2106','','hide')">
                <asp:Label ID="P_3GIA2106" runat="server" Text="28" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2106" class="auto-style4"
    style="position:absolute; left:112px; top:432px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2106
     <asp:Label ID="V_3GIA2106" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
           <div id="D_3GIA2107" class ="auto-style4" style="position:absolute; left: 104px; width: 40px; top: 384px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2107','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2107','','hide')">
                <asp:Label ID="P_3GIA2107" runat="server" Text="29" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2107" class="auto-style4"
    style="position:absolute; left:8px; top:384px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2107
     <asp:Label ID="V_3GIA2107" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
              <div id="D_3GIA2108" class ="auto-style4" style="position:absolute; left: 208px; width: 40px; top: 384px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2108','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2108','','hide')">
                <asp:Label ID="P_3GIA2108" runat="server" Text="30" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2108" class="auto-style4"
    style="position:absolute; left:112px; top:384px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2108
     <asp:Label ID="V_3GIA2108" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_2GIA2002" class ="auto-style4" style="position:absolute; left: 272px; width: 40px; top: 552px; height: 40px;"
                    onmouseover="MM_showHideLayers('S_2GIA2002','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2002','','hide')">
                        <asp:Label ID="P_2GIA2002" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="12" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_2GIA2002" class="auto-style4"
    style="position:absolute; left:272px; top:584px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2002
     <asp:Label ID="V_2GIA2002" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_2GIA2001" class ="auto-style4" style="position:absolute; left: 240px; width: 40px; top: 560px; height: 40px;"
                    onmouseover="MM_showHideLayers('S_2GIA2001','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2001','','hide')">
                        <asp:Label ID="P_2GIA2001" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="11" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_2GIA2001" class="auto-style4"
    style="position:absolute; left:240px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2001
     <asp:Label ID="V_2GIA2001" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                             <div id="D_1GIA2002" class ="auto-style4" style="position:absolute; left: 392px; width: 40px; top: 552px; height: 40px;"
                              onmouseover="MM_showHideLayers('S_1GIA2002','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2002','','hide')">
                    <asp:Label ID="P_1GIA2002" runat="server" Height="16px" Style="border-right: thin solid;
                        border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                         text-align: center" Text="2" Width="16px" BackColor="Yellow"></asp:Label></div>
                        <div id="S_1GIA2002" class="auto-style4"
    style="position:absolute; left:392px; top:584px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>1GIA2002
     <asp:Label ID="V_1GIA2002" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
           <div id="D_1GIA2102" class ="auto-style4" style="position:absolute; left: 464px; width: 40px; top: 536px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_1GIA2102','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2102','','hide')">
                        <asp:Label ID="P_1GIA2102" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="4" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_1GIA2102" class="auto-style4"
    style="position:absolute; left:488px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;"><p>1GIA2102
     <asp:Label ID="V_1GIA2102" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                        <div id="D_1GIA2101" class ="auto-style4" style="position:absolute; left: 368px; width: 40px; top: 536px; height: 40px;"
                         onmouseover="MM_showHideLayers('S_1GIA2101','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2101','','hide')">
                            <asp:Label ID="P_1GIA2101" runat="server" Height="16px" Style="border-right: thin solid;
                                border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                                 text-align: center" Text="3" Width="16px" BackColor="Yellow"></asp:Label></div>
                           <div id="S_1GIA2101" class="auto-style4"
    style="position:absolute; left:392px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>1GIA2101
     <asp:Label ID="V_1GIA2101" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                        
                   
                   
          
                    <div id="D_2GIA2103" class ="auto-style4" style="position:absolute; left: 240px; width: 40px; top: 480px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2103','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2103','','hide')">
                        <asp:Label ID="P_2GIA2103" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="15" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_2GIA2103" class="auto-style4"
    style="position:absolute; left:144px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2103
     <asp:Label ID="V_2GIA2103" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
                    <div id="D_1GIA2106" class ="auto-style4" style="position:absolute; left: 464px; width: 40px; top: 432px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_1GIA2106','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2106','','hide')">
                        <asp:Label ID="P_1GIA2106" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="8" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_1GIA2106" class="auto-style4"
    style="position:absolute; left:488px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;"><p>1GIA2106
     <asp:Label ID="V_1GIA2106" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                            
                    <div id="D_1GIA2105" class ="auto-style4" style="position:absolute; left: 368px; width: 40px; top: 432px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_1GIA2105','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2105','','hide')">
                        <asp:Label ID="P_1GIA2105" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="7" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_1GIA2105" class="auto-style4"
    style="position:absolute; left:392px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;" onclick="return S_1GIA2105_onclick()"><p>1GIA2105
     <asp:Label ID="V_1GIA2105" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                            
                    
                    <div id="D_1GIA2108" class ="auto-style4" style="position:absolute; left: 464px; width: 40px; top: 384px; height: 40px;"
                    onmouseover="MM_showHideLayers('S_1GIA2108','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2108','','hide')">
                        <asp:Label ID="P_1GIA2108" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="10" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_1GIA2108" class="auto-style4"
    style="position:absolute; left:488px; top:352px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; z-index: 1;"><p>1GIA2108
     <asp:Label ID="V_1GIA2108" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                            
                    <div id="D_2GIA2104" class ="auto-style4" style="position:absolute; left: 336px; width: 40px; top: 480px; height: 40px;"
                    onmouseover="MM_showHideLayers('S_2GIA2104','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2104','','hide')"> 
                        <asp:Label ID="P_2GIA2104" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="16" Width="16px" BackColor="Yellow"></asp:Label></div>
                             <div id="S_2GIA2104" class="auto-style4"
    style="position:absolute; left:248px; top:448px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2104
     <asp:Label ID="V_2GIA2104" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                            
                   
                            
                    <div id="D_2GIA2106" class ="auto-style4" style="position:absolute; left: 336px; width: 40px; top: 432px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2106','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2106','','hide')">
                        <asp:Label ID="P_2GIA2106" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="18" Width="16px" BackColor="Yellow"></asp:Label></div>
                              <div id="S_2GIA2106" class="auto-style4"
    style="position:absolute; left:248px; top:400px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2106
     <asp:Label ID="V_2GIA2106" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
          
           <div id="D_2GIA2102" class ="auto-style4" style="position:absolute; left: 336px; width: 40px; top: 536px; height: 40px;"
                     onmouseover="MM_showHideLayers('S_2GIA2102','','show')" 
           onmouseout="MM_showHideLayers('S_2GIA2102','','hide')">
                        <asp:Label ID="P_2GIA2102" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="14" Width="16px" BackColor="Yellow"></asp:Label></div>
                               <div id="S_2GIA2102" class="auto-style4"
    style="position:absolute; left:248px; top:504px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>2GIA2102
     <asp:Label ID="V_2GIA2102" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
                     
              <div id="D_1GIA2107" class ="auto-style4" style="position:absolute; left: 368px; width: 40px; top: 384px; height: 40px;"
                 onmouseover="MM_showHideLayers('S_1GIA2107','','show')" 
           onmouseout="MM_showHideLayers('S_1GIA2107','','hide')">
                        <asp:Label ID="P_1GIA2107" runat="server" Height="16px" Style="border-right: thin solid;
                            border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid;
                             text-align: center" Text="9" Width="16px" BackColor="Yellow"></asp:Label></div>
                            <div id="S_1GIA2107" class="auto-style4"
    style="position:absolute; left:368px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left;z-index: 1;"><p>1GIA2107
     <asp:Label ID="V_1GIA2107" runat="server" ForeColor="Blue" 
          Style="text-align: center" Text="N/A"></asp:Label>ppm</p></div>
         
          <div id="D_3GIA2003" class ="auto-style4" style="position:absolute; left: 168px; width: 40px; top: 728px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2003','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2003','','hide')">
                <asp:Label ID="P_3GIA2003" runat="server" Text="31" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2003" class="auto-style4"
    style="position:absolute; left:72px; top:728px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2003
     <asp:Label ID="V_3GIA2003" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
           <div id="D_3GIA2004" class ="auto-style4" style="position:absolute; left: 168px; width: 40px; top: 352px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2004','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2004','','hide')">
                <asp:Label ID="P_3GIA2004" runat="server" Text="32" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2004" class="auto-style4"
    style="position:absolute; left:72px; top:320px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps;z-index: 1;"><p>3GIA2004
     <asp:Label ID="V_3GIA2004" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
         
                  <div id="D_3GIA2005" class ="auto-style4" style="position:absolute; left: 520px; width: 40px; top: 592px; height: 40px;" 
                 onmouseover="MM_showHideLayers('S_3GIA2005','','show')" 
           onmouseout="MM_showHideLayers('S_3GIA2005','','hide')">
                <asp:Label ID="P_3GIA2005" runat="server" Text="33" 
                style="border-right: thin solid; border-top: thin solid; vertical-align: middle; border-left: thin solid; border-bottom: thin solid; 
               text-align: center" Height="16px" Width="16px" BackColor="Yellow"></asp:Label></div>
                <div id="S_3GIA2005" class="auto-style4"
    style="position:absolute; left:424px; top:592px; width:85px; height:50px; 
    background-color: #ffff00; border-right: black thin solid; border-top: black thin solid; visibility: 
    hidden; border-left: black thin solid; border-bottom: black thin solid; 
    vertical-align: top; text-align: left; font-variant: small-caps; z-index: 1;"><p>3GIA2005
     <asp:Label ID="V_3GIA2005" runat="server" ForeColor="Blue" Style="text-align: center"
         Text="N/A"></asp:Label>ppm</p></div>
 
        <table style="width: 968px">
            <tr>
                <td style="width: 10px; height: 676px;">
                </td>
                <td class="auto-style2">
<img src="images/FCE.JPG" usemap="#Map" style="width: 560px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; vertical-align: top;" id="IMG1" onclick="return IMG1_onclick()" /><br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;<table style="width: 472px">
                    <%--    <tr>
                            <td style="width: 193px; height: 136px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/North.JPG" /></td>
                            <td style="width: 280px; height: 136px">
                  
                       
        <input id="wind_direction_E" runat="server" enableviewstate="true" name="wind_direction_E" type="hidden" />
                                <tchart:WebChart ID="WebChart2" runat="server" AutoPostback="False" Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnQtAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QPLkxlZ2VuZC5WaXNpYmxlDS5IZWFkZXIuTGluZXMQLkFzcGVjdC5Sb3RhdGlvbhUuQXNwZWN0LlJvdGF0aW9uRmxvYXQRLkFzcGVjdC5FbGV2YXRpb24WLkFzcGVjdC5FbGV2YXRpb25GbG9hdBIuQXNwZWN0Lk9ydGhvZ29uYWwZLkFzcGVjdC5Db2xvclBhbGV0dGVJbmRleBMuQXNwZWN0LlBlcnNwZWN0aXZlDi5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchYuU2VyaWVzLjAuQ2lyY2xlTGFiZWxzHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5Sb3RhdGlvbkFuZ2xlJC5TZXJpZXMuMC5GcmFtZS5GcmFtZUVsZW1lbnRQZXJjZW50cxcuU2VyaWVzLjAuRnJhbWUuQ2lyY2xlZCwuU2VyaWVzLjAuRnJhbWUuT3V0ZXJCYW5kLkdyYWRpZW50LlVzZU1pZGRsZRwuU2VyaWVzLjAuVW5pcXVlQ3VzdG9tUmFkaXVzFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGS5BeGVzLkxlZnQuTGFiZWxzLlZpc2libGUYLkF4ZXMuVG9wLkxhYmVscy5WaXNpYmxlGi5BeGVzLlJpZ2h0LkxhYmVscy5WaXNpYmxlGy5BeGVzLkJvdHRvbS5MYWJlbHMuVmlzaWJsZQ8uQXhlcy5BdXRvbWF0aWMABAAABgAAAAAAAAAAAQQAAAQEAAcAAAAHAAEEBwABAAQBAAAAAAQEAAAAAAABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAAQEIBggGAQgIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAgGAQEBBgglU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAABAQEBAQIAAAAABfz///8ZU3RlZW1hLlRlZUNoYXJ0LlRoZW1lVHlwZQEAAAAHdmFsdWVfXwAIAgAAAAAAAAAAAAkFAAAAaAEAAAAAAAAAgHZAOwEAAAAAAAAAsHNAAAAAAAAAAAAAAAYGAAAAH1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuV2luZFJvc2UF+f///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABfj///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH3////+f///woAAAAAAAAAAAAAAABaAAAACQoAAAABAAEJCwAAAAIAAAAGDAAAABNXaW5kX2RpcmVjdGlvbl9wb3MyBfP///8lU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgEAAAAHdmFsdWVfXwAIAgAAAAEAAAAJDgAAAAIAAAAGDwAAAA9XaW5kX1NwZWVkX3BvczIAAfD////5////CgAA//8AAAAAAAACAAYRAAAACumiqOWQkeWcljEAAAAAAAAAAAAAABRAAAAAAAAAIEAF7v///yRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQBAAAAB3ZhbHVlX18ACAIAAAAAAAAABe3///8VU3lzdGVtLkRyYXdpbmcuUG9pbnRGAgAAAAF4AXkAAAsLAwAAAAAAAAAAAAAAAAAAAAERBQAAAAEAAAAGFAAAAAAPCgAAAAMAAAAGAAAAAAAAOUAAAAAAAABOQAAAAAAAAC5ADwsAAAACAAAABgAAAAAAAAAAAAAAAAAQdUAPDgAAAAIAAAAGAAAAAAAAAAAAAAAAABB1QAs=" DataSourceID="SqlDataSource1" GetChartFile="GetChart.aspx" Height="325px" LastFileName="" TempChart="Session" Width="429px" />
                                <br />
                                <asp:Label ID="Label23" runat="server" BackColor="#FFC080" Style="border-right: 1px solid;
                                    border-top: 1px solid; font-weight: bold; border-left: 1px solid; border-bottom: 1px solid"
                                    Text="熱軋主電氣室南棟"></asp:Label><br />
                                <asp:Label ID="Wind_S_E_L" runat="server" BackColor="Yellow" Style="border-right: 1px solid;
                                    border-top: 1px solid; font-weight: bold; border-left: 1px solid; border-bottom: 1px solid"
                                    Text="風速："></asp:Label>&nbsp;
                                <asp:Label ID="Val_W_E_S" runat="server" ForeColor="Lime"
                                        Style="font-weight: bold" Text="N/A"></asp:Label>
                                <asp:Label ID="Label25" runat="server" Style="font-weight: bold" Text="m/s"></asp:Label></td>
                            <td style="width: 1526px; height: 136px;"><br />
                                <br />
                                </td>
                        </tr>--%>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                <%--<td style="width: 293px; height: 536px" align="left">
                </td>--%>
               
                <td style="height: 676px; vertical-align: top; width: 340px;">
                    <table style="width: 344px">
                        <tr>
                            <td style="width: 14px">
                            </td>
                            <td style="width: 281px">
     
                                <tchart:WebChart ID="WebChart1" runat="server" AutoPostback="False" Config="AAEAAAD/////AQAAAAAAAAAMAgAAAFFUZWVDaGFydCwgVmVyc2lvbj00LjEuMjAxOC41MDQyLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTljODEyNjI3NmM3N2JkYjcMAwAAAFFTeXN0ZW0uRHJhd2luZywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWIwM2Y1ZjdmMTFkNTBhM2EFAQAAABVTdGVlbWEuVGVlQ2hhcnQuQ2hhcnQtAAAADC5DYW5jZWxNb3VzZQ0uQ3VycmVudFRoZW1lEC5DdXN0b21DaGFydFJlY3QPLkxlZ2VuZC5WaXNpYmxlDS5IZWFkZXIuTGluZXMQLkFzcGVjdC5Sb3RhdGlvbhUuQXNwZWN0LlJvdGF0aW9uRmxvYXQRLkFzcGVjdC5FbGV2YXRpb24WLkFzcGVjdC5FbGV2YXRpb25GbG9hdBIuQXNwZWN0Lk9ydGhvZ29uYWwZLkFzcGVjdC5Db2xvclBhbGV0dGVJbmRleBMuQXNwZWN0LlBlcnNwZWN0aXZlDi5Bc3BlY3QuVmlldzNECFNlcmllcy4wFS5TZXJpZXMuMC5CcnVzaC5Db2xvchYuU2VyaWVzLjAuQ2lyY2xlTGFiZWxzHC5TZXJpZXMuMC5Qb2ludGVyLlNpemVEb3VibGUbLlNlcmllcy4wLlBvaW50ZXIuU2l6ZVVuaXRzHS5TZXJpZXMuMC5Qb2ludGVyLkJydXNoLkNvbG9yFy5TZXJpZXMuMC5Sb3RhdGlvbkFuZ2xlJC5TZXJpZXMuMC5GcmFtZS5GcmFtZUVsZW1lbnRQZXJjZW50cxcuU2VyaWVzLjAuRnJhbWUuQ2lyY2xlZCwuU2VyaWVzLjAuRnJhbWUuT3V0ZXJCYW5kLkdyYWRpZW50LlVzZU1pZGRsZRwuU2VyaWVzLjAuVW5pcXVlQ3VzdG9tUmFkaXVzFy5TZXJpZXMuMC5YVmFsdWVzLlZhbHVlFy5TZXJpZXMuMC5YVmFsdWVzLkNvdW50HC5TZXJpZXMuMC5YVmFsdWVzLkRhdGFNZW1iZXIXLlNlcmllcy4wLlhWYWx1ZXMuT3JkZXIXLlNlcmllcy4wLllWYWx1ZXMuVmFsdWUXLlNlcmllcy4wLllWYWx1ZXMuQ291bnQcLlNlcmllcy4wLllWYWx1ZXMuRGF0YU1lbWJlchMuU2VyaWVzLjAuQ29sb3JFYWNoDy5TZXJpZXMuMC5Db2xvcg8uU2VyaWVzLjAuVGl0bGUdLlNlcmllcy4wLlVzZUV4dGVuZGVkTnVtUmFuZ2UhLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuTWFyZ2luKC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLlBvaW50ZXJIZWlnaHQnLlNlcmllcy4wLk1hcmtzLlRhaWxQYXJhbXMuUG9pbnRlcldpZHRoIC5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkFsaWduKS5TZXJpZXMuMC5NYXJrcy5UYWlsUGFyYW1zLkN1c3RvbVBvaW50UG9zGS5BeGVzLkxlZnQuTGFiZWxzLlZpc2libGUYLkF4ZXMuVG9wLkxhYmVscy5WaXNpYmxlGi5BeGVzLlJpZ2h0LkxhYmVscy5WaXNpYmxlGy5BeGVzLkJvdHRvbS5MYWJlbHMuVmlzaWJsZQ8uQXhlcy5BdXRvbWF0aWMABAAABgAAAAAAAAAAAQQAAAQEAAcAAAAHAAEEBwABAAQBAAAAAAQEAAAAAAABGVN0ZWVtYS5UZWVDaGFydC5UaGVtZVR5cGUCAAAAAQEIBggGAQgIARRTeXN0ZW0uRHJhd2luZy5Db2xvcgMAAAABBidTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlBvaW50ZXJTaXplVW5pdHMCAAAAFFN5c3RlbS5EcmF3aW5nLkNvbG9yAwAAAAgGAQEBBgglU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgIAAAAGCAEUU3lzdGVtLkRyYXdpbmcuQ29sb3IDAAAAAQsGBiRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQCAAAAFVN5c3RlbS5EcmF3aW5nLlBvaW50RgMAAAABAQEBAQIAAAAABfz///8ZU3RlZW1hLlRlZUNoYXJ0LlRoZW1lVHlwZQEAAAAHdmFsdWVfXwAIAgAAAAAAAAAAAAkFAAAAaAEAAAAAAAAAgHZAOwEAAAAAAAAAsHNAAAAAAAAAAAAAAAYGAAAAH1N0ZWVtYS5UZWVDaGFydC5TdHlsZXMuV2luZFJvc2UF+f///xRTeXN0ZW0uRHJhd2luZy5Db2xvcgQAAAAEbmFtZQV2YWx1ZQprbm93bkNvbG9yBXN0YXRlAQAAAAkHBwMAAAAKAAD//wAAAAAAAAIAAQAAAAAAAAAABfj///8nU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5Qb2ludGVyU2l6ZVVuaXRzAQAAAAd2YWx1ZV9fAAgCAAAAAAAAAAH3////+f///woAAAAAAAAAAAAAAABaAAAACQoAAAABAAEJCwAAAAIAAAAGDAAAABNXaW5kX2RpcmVjdGlvbl9wb3MxBfP///8lU3RlZW1hLlRlZUNoYXJ0LlN0eWxlcy5WYWx1ZUxpc3RPcmRlcgEAAAAHdmFsdWVfXwAIAgAAAAEAAAAJDgAAAAIAAAAGDwAAAA9XaW5kX1NwZWVkX3BvczEAAfD////5////CgAA//8AAAAAAAACAAYRAAAACumiqOWQkeWcljEAAAAAAAAAAAAAABRAAAAAAAAAIEAF7v///yRTdGVlbWEuVGVlQ2hhcnQuU3R5bGVzLlRhaWxBbGlnbm1lbnQBAAAAB3ZhbHVlX18ACAIAAAAAAAAABe3///8VU3lzdGVtLkRyYXdpbmcuUG9pbnRGAgAAAAF4AXkAAAsLAwAAAAAAAAAAAAAAAAAAAAERBQAAAAEAAAAGFAAAAAAPCgAAAAMAAAAGAAAAAAAAOUAAAAAAAABOQAAAAAAAAC5ADwsAAAACAAAABgAAAAAAAAAAAAAAAACAZEAPDgAAAAIAAAAGAAAAAAAAAAAAAAAAAIBkQAs=" GetChartFile="GetChart.aspx" Height="300px" LastFileName="" TempChart="Session" Width="300px" DataSourceID="SqlDataSource1" CssClass="auto-style4" />
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PMISConnectionString %>" SelectCommand="select * from (
(SELECT TOP(1) Wind_direction as Wind_Speed_pos1,Wind_direction as Wind_direction_pos1
                                FROM Wind 
                                Where Position=1
                               ORDER BY sys_process_date DESC) as a 
cross join 
(SELECT TOP(1) Wind_direction as Wind_Speed_pos2,Wind_direction as Wind_direction_pos2
                                FROM Wind 
                                Where Position=2
                               ORDER BY sys_process_date DESC) as b)
Union
( select 0 as Wind_Speed_pos1,0 as Wind_direction_pos1,0 as Wind_Speed_pos2,0 as Wind_direction_pos2)
order by Wind_Speed_pos1"></asp:SqlDataSource>
                                <input id="wind_direction_W" runat="server" enableviewstate="true" name="wind_direction_W" type="hidden" class="auto-style4" />&nbsp;<span class="auto-style1"><span class="auto-style3"><br />
                                <asp:Label ID="Label20" runat="server" BackColor="#FFC080" Style="border: 1px solid black;" Text="熱軋大樓"></asp:Label>
                                <br />
                                <asp:Label ID="Wind_S_W_L" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px;" Text="風速："></asp:Label>
                                &nbsp;
                                <asp:Label ID="Val_W_W_S" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                <asp:Label ID="Label22" runat="server" ForeColor="Black" Text="m/s"></asp:Label>
                                <br />
                                <br />
                                </span></span>
                            </td>
                            <td style="width: 73px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 14px">
                            </td>
                            <td style="font-weight: bold; width: 281px; background-color: #ffff33" class="auto-style1">
                                <span class="auto-style6"><span class="auto-style3">加熱爐固定式CO偵測最大值：</span><asp:Label ID="Total_CO" runat="server" CssClass="auto-style3" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span><span class="auto-style5">ppm</span></td>
                            <td style="width: 73px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 14px">
                            </td>
                            <td style="width: 281px">
                            </td>
                            <td style="width: 73px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 368px; text-align: left;">
                        <tr>
                            <td style="width: 30px;text-align: center" class="auto-style4">
                                <asp:Label ID="IT_01" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="1" Width="24px" ForeColor="ControlText"></asp:Label></td>
                            <td style="width: 130px; height: 20px; text-align: left" class="auto-style3">
                                <asp:Label ID="L1" runat="server" Style="color: #ff9933; text-align: left;" Text="1G1A2001" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V1" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 20px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label5" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_02" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="2" Width="24px" ForeColor="Black"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L2" runat="server" Style="color: #ff9933" Text="1G1A2002" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V2" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label6" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                        <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_03" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="3" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L3" runat="server" Style="color: #ff9933" Text="1G1A2101" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V3" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label14" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                        <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_04" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="4" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L4" runat="server" Style="color: #ff9933" Text="1G1A2102" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V4" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label19" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                        <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_05" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="5" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L5" runat="server" Style="color: #ff9933" Text="1G1A2103" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V5" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label24" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_06" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="6" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L6" runat="server" Style="color: #ff9933" Text="1G1A2104" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V6" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label29" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_07" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="7" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L7" runat="server" Style="color: #ff9933" Text="1G1A2105" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V7" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label34" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_08" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="8" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L8" runat="server" Style="color: #ff9933" Text="1G1A2106" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V8" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label39" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_09" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="9" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L9" runat="server" Style="color: #ff9933" Text="1G1A2107" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V9" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label54" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_10" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="10" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L10" runat="server" Style="color: #ff9933" Text="1G1A2108" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V10" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label59" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_11" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="11" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L11" runat="server" Style="color: #ff9933" Text="2G1A2001" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V11" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label64" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_12" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="12" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L12" runat="server" Style="color: #ff9933" Text="2G1A2002" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V12" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label69" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_13" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="13" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L13" runat="server" Style="color: #ff9933" Text="2G1A2101" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V13" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label74" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_14" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="14" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L14" runat="server" Style="color: #ff9933" Text="2G1A2102" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V14" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label79" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_15" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="15" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L15" runat="server" Style="color: #ff9933" Text="2G1A2103" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V15" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label84" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_16" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="16" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L16" runat="server" Style="color: #ff9933" Text="2G1A2104" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V16" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label89" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_17" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="17" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L17" runat="server" Style="color: #ff9933" Text="2G1A2105" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V17" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label94" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_18" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="18" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L18" runat="server" Style="color: #ff9933" Text="2G1A2106" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V18" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label99" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_19" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="19" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L19" runat="server" Style="color: #ff9933" Text="2G1A2107" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V19" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label104" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                         <tr>
                           <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_20" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="20" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L20" runat="server" Style="color: #ff9933" Text="2G1A2108" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V20" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<span class="auto-style3"><asp:Label ID="Label109" runat="server" CssClass="auto-style1" Text="ppm"></asp:Label>
                                <span class="auto-style1">&nbsp; </span></span>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 30px;text-align: center" class="auto-style4">
                                <asp:Label ID="IT_21" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="21" Width="24px"></asp:Label></td>
                            <td style="width: 130px; height: 23px; text-align: left" class="auto-style3">
                                <asp:Label ID="L21" runat="server" Style="color: #ff9933" Text="3G1A2001" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V21" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 23px">
                                &nbsp;<asp:Label ID="Label2" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_22" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="22" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L22" runat="server" Style="color: #ff9933" Text="3G1A2002" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V22" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label4" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px;text-align: center" class="auto-style4">
                                <asp:Label ID="IT_23" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="23" Width="24px"></asp:Label></td>
                            <td style="width: 130px; height: 23px; text-align: left" class="auto-style3">
                                <asp:Label ID="L23" runat="server" Style="color: #ff9933" Text="3G1A2101" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V23" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 23px">
                                &nbsp;<asp:Label ID="Label7" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_24" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="24" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L24" runat="server" Style="color: #ff9933" Text="3G1A2102" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V24" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label8" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_25" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="25" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L25" runat="server" Style="color: #ff9933" Text="3G1A2103" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V25" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label9" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_26" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="26" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L26" runat="server" Style="color: #ff9933" Text="3G1A2104" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V26" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label10" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_27" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="27" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L27" runat="server" Style="color: #ff9933" Text="3G1A2105" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V27" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label11" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_28" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="28" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L28" runat="server" Style="color: #ff9933" Text="3G1A2106" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V28" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label12" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_29" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="29" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L29" runat="server" Style="color: #ff9933" Text="3G1A2107" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V29" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label13" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px;text-align: center" class="auto-style4">
                                <asp:Label ID="IT_30" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="30" Width="24px"></asp:Label></td>
                            <td style="width: 130px; height: 21px; text-align: left" class="auto-style3">
                                <asp:Label ID="L30" runat="server" Style="color: #ff9933" Text="3G1A2108" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V30" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 21px">
                                &nbsp;<asp:Label ID="Label15" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px;text-align: center" class="auto-style4">
                                <asp:Label ID="IT_31" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="31" Width="24px"></asp:Label></td>
                            <td style="width: 130px; height: 22px; text-align: left" class="auto-style3">
                                <asp:Label ID="L31" runat="server" Style="color: #ff9933" Text="3G1A2003" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V31" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 22px">
                                &nbsp;<asp:Label ID="Label16" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_32" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="32" Width="24px"></asp:Label></td>
                            <td style="width: 130px; text-align: left" class="auto-style3">
                                <asp:Label ID="L21L32" runat="server" Style="color: #ff9933" Text="3G1A2004" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V32" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px">
                                &nbsp;<asp:Label ID="Label17" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 30px; text-align: center" class="auto-style4">
                                <asp:Label ID="IT_33" runat="server" BackColor="Yellow" Style="border-style: solid; border-color: inherit; border-width: 1px; text-align: center" Text="33" Width="24px"></asp:Label></td>
                            <td style="width: 130px; height: 21px; text-align: left" class="auto-style3">
                                <asp:Label ID="L33" runat="server" Style="color: #ff9933" Text="3G1A2005" CssClass="auto-style1"></asp:Label>
                                <span class="auto-style1">&nbsp;
                                <asp:Label ID="V33" runat="server" ForeColor="Lime" Text="N/A"></asp:Label>
                                </span></td>
                            <td style="width: 160px; height: 21px">
                                &nbsp;<asp:Label ID="Label18" runat="server" Text="ppm" CssClass="auto-style4"></asp:Label></td>
                        </tr>
                        
                        
                    </table>
                    <span class="auto-style3">
                    <br class="auto-style1" />
                    <span class="auto-style1">&nbsp; &nbsp;&nbsp; </span>
                     <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Black" CssClass="auto-style13">CO偵測值說明：</asp:Label><br class="auto-style13" />
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="blue" CssClass="auto-style13">[ 藍色：< 35 ppm | </asp:Label>
                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Small"
                                    ForeColor="red" CssClass="auto-style13">紅色：>= 35 ppm ]</asp:Label><br class="auto-style13" />
                    <span class="auto-style1">
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp;</span><asp:Label ID="Label49" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0" Text="資料更新時間："></asp:Label>
                    <span class="auto-style1">&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp; &nbsp; &nbsp;</span><asp:Label ID="Fn1" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="Black">#1 FCE：</asp:Label>
                    <asp:Label ID="Last_time_1" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                    <br class="auto-style1" />
                    <span class="auto-style1">&nbsp; &nbsp;&nbsp; </span>
                    <asp:Label ID="Fn2" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="Black">#2 FCE：</asp:Label>
                    <asp:Label ID="Last_time_2" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                    <br class="auto-style1" />
                    <span class="auto-style1">&nbsp; &nbsp;&nbsp; </span>
                    <asp:Label ID="Label3" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="Black">#3 FCE：</asp:Label>
                    <asp:Label ID="Last_time_3" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                    <br class="auto-style1" />
                    <span class="auto-style1">&nbsp; &nbsp;&nbsp; </span>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="Black">風速計(W)：</asp:Label>
                    <asp:Label ID="Last_time_4" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                    <br class="auto-style1" />
                    <span class="auto-style1">&nbsp; &nbsp;&nbsp; </span>
                <%--    <asp:Label ID="Label21" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="Black">風速計(E)：</asp:Label>
                    <asp:Label ID="Last_time_5" runat="server" CssClass="auto-style1" Font-Bold="True" Font-Size="Small" ForeColor="#0000C0"></asp:Label>
                    <span class="auto-style1">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br /> &nbsp; &nbsp; &nbsp;--%>
                    <br />
                    &nbsp; &nbsp; &nbsp;
                    <br />
                    &nbsp; &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    </span></span>
                    </td>

            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 603px">
                </td>
                <td style="width: 340px">
                </td>

            </tr>
            <caption class="auto-style4">
                &nbsp;
            </caption>
        </table>
        
          </ContentTemplate>
        </asp:UpdatePanel>
     </div>   
    </form>
</body>
</html>
