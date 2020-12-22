<%	
	Option Explicit 
	Response.Buffer = True 
	'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0011.asp
	
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
<html>

<head>
<title>SF Menu Page</title>
</head>
<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">

<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
<tr>
<td>
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
    <tr>
<%	If C_BNRBKGRND = "" Then %>
		<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %></font></b></td>
<%	Else %>
		<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%	End If %>     
    </tr>
    <tr>
    <td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">List All Products</font></b></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>Use this product list for a reference to what is currently in your web’s inventory.  Products that are marked as Enabled are currently available to customers through the StoreFront search engine.  Click Edit to begin modifying a product item.</font></td>
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>">
			<p><br>
            <table border="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="90%" align="center">
            <tr>
            <td width="15%" align="center" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Product ID</font></b></td>
            <td width="40%" align="center" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Product Name</font></b></td>
            <td width="15%" align="center" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Enable</font></b></td>
            <td width="15%" align="center" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Price</font></b></td>
            <td width="15%" align="center" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Edit</font></b></td>
            </tr>
<%	
	Dim objRS
	Dim iCount 
	Dim sColor
	
	Set objRS = getProductList()
	Do While Not objRS.EOF 
		If iCount = 0 Then
			sColor = C_ALTBGCOLOR1
			iCount = 1
		Else
			sColor = C_ALTBGCOLOR2
			iCount = 0
		End If			
	
%>
            <tr>
            <td width="15%" align="center" bgcolor="<%= sColor %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><%= objRS("prodID") %></font></td>
            <td width="40%" align="center" bgcolor="<%= sColor %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><%= objRS("prodName") %></font></td>
            <td width="15%" align="center" bgcolor="<%= sColor %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><%= getColumnBValue("prodEnabledIsActive",objRS) %></font></td>
            <td width="15%" align="center" bgcolor="<%= sColor %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><% If objRS("prodPrice") <> "" Then Response.Write(FormatCurrency(objRS("prodPrice"),2)) %></font></td>
            <td width="15%" align="center" bgcolor="<%= sColor %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><a href=m0010i.asp?ProdID=<%= objRS("prodID") %>>Edit</a></font></td>
            </tr>
<%	
	objRS.MoveNext
	Loop
	closeObj(objRS)
%>            

			</table>
			<br>
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

