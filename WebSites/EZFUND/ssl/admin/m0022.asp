<%	
	option explicit 
	Response.Buffer = True
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0022.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   web Admin tool

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO		
%>
<script language="javascript" src="../../SFLib/sfCheckErrors.js"></script>
<html>

<head>
<title>SF Menu Page</title>
</head>

<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SfLib/incDesign.asp"-->
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<%
	Dim rsAdmin, sGDiscount, sGDiscountAmt, sSDiscount, sSDiscountAmt, iGchecked, iSchecked
	
	If Request.Form("Submit.x") <> "" Then
		If Request.Form("chkGDiscount") = "" Then
			sGDiscount = "0"
		Else
			sGDiscount = Request.Form("chkGDiscount")
		End If 
	
		If Request.Form("chkSDiscount") = "" Then
			sSDiscount = "0"
		Else
			sSDiscount = Request.Form("chkSDiscount")
		End If 
	
		Set rsAdmin = Server.CreateObject("ADODB.RecordSet")			
		rsAdmin.Open "sfAdmin", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
			rsAdmin.Fields("adminGlobalSaleIsActive")	= sGDiscount
			rsAdmin.Fields("adminGlobalSaleAmt")		= Request.Form("txtGDiscount")
			rsAdmin.Fields("adminFreeShippingIsActive")	= sSDiscount
			rsAdmin.Fields("adminFreeShippingAmount")	= Request.Form("txtSDiscount")
		rsAdmin.Update 
 		closeObj(rsAdmin)
	End If
	Set rsAdmin = Server.CreateObject("ADODB.RecordSet")		
	rsAdmin.Open "sfAdmin", cnn, adOpenStatic, adLockOptimistic, adCmdTable		
		sGDiscount	  = Trim(rsAdmin.Fields("adminGlobalSaleIsActive"))
		sGDiscountAmt = Trim(rsAdmin.Fields("adminGlobalSaleAmt"))	
		sSDiscount	  = Trim(rsAdmin.Fields("adminFreeShippingIsActive")) 
		sSDiscountAmt = Trim(rsAdmin.Fields("adminFreeShippingAmount"))
	closeObj(rsAdmin)
	
	If sGDiscount = "1" Then iGchecked = "checked"
	If sSDiscount = "1" Then iSchecked = "checked"
%>
	<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
<tr>
<td>
	<form method="post" action="m0022.asp" onSubmit="this.txtGDiscount.number=true;this.txtSDiscount.number=true;return sfCheck(this)" id="form1" name="form1">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
    <tr>
<%	If C_BNRBKGRND = "" Then %>
		<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %></font></b></td>
<%	Else %>
		<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%	End If %>      
    </tr>
    <tr>
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Sales and Discounts</font></b></td>        
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>You can use this page to create a global discount that will apply to all orders in your web, or to set a order total where shipping becomes free.  Enter the decimal amount of the global discount and the order total where shipping becomes free, then check the box to the right of each option to activate it.  Click <B>Submit</b> to put the global discount or free shipping option into effect.</font></td>
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">        
        <% If Request.Form("Submit.x") <> "" Then %>
        <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b>Database Updated
				</font>
				<br><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>"><a href="menu.asp">Return to Menu</b></a>
				</font></b>
				</td></tr>
				</table>
			</td></tr>	
			</table>
        </td>
        </tr>
		<% End If %>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Sales and Discounts</font></b></td>
        </tr>
        <tr>
        <td width="40%" align="right" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Global Discount (decimal form)</font></td>
        <td width="60%" nowrap><input type="text" value="<%= sGDiscountAmt %>" name="txtGDiscount" title="Global Sale Amount" size="10" style="<%= C_FORMDESIGN %>"><input type="checkbox" <%= iGchecked %> value="1" name="chkGDiscount"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Global Sale</font></td>
        </tr>
		<tr>
        <td width="40%" align="right" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Free Shipping For Purchases Over</font></td>
        <td width="60%" nowrap><input type="text" value="<%= sSDiscountAmt %>" name="txtSDiscount" title="Free Shipping Amount" size="10" style="<%= C_FORMDESIGN %>"><input type="checkbox" <%= iSchecked %> value="1" name="chkSDiscount"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Free Shipping</font></td>
        </tr>
        <td width="100%" align="center" valign="top" colspan="2"><input type="image" name="Submit" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21"></td>
        </tr>     
        </table>
		</form>
	</td>
	</tr>
    <tr>
	<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
    </tr>
</table>
</td>
</tr>
</table>
</body>
</html>
