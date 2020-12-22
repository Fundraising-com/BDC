<%
	Option Explicit 
	Response.Buffer = True	
	'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0013.asp
	
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
<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<!--#include file="incAdmin.asp"-->
<%
	Dim sCategories, sMessage, categoryID
	sCategories = getCategoriesList()
	
	If Request.Form("bFindCategory.x") <> "" Then
		categoryID = Trim(Request.Form("categoryID"))
		Response.Redirect("m0012.asp?categoryID=" & categoryID)
	ElseIf Request.Form("bDeleteCat.x") <> ""  Then
		Call setDeleteCategory(Trim(Request.Form("categoryID")))
		sMessage = "Category Deleted" 		
	End If
%>

<html>
<head>
<title>SF Menu Page</title>
</head>


<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">

<form method="post" action="m0013.asp">
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
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Edit/Delete Categories</font></b></td>        
    </tr>
    <tr>
	<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>Select the name of the category you would like to edit or delete in the Category Name field, then click the Submit button to begin editing the category information, or click the Delete button to delete the category.</font></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
   					<% If sMessage <> "" Then %>
					<tr>
					<td width="100%" colspan="2" align="center" height="90" valign="center">
					<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
					<tr><td width="100%">
						<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
						<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
						<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b><%= sMessage %>
						</font>
						<br><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>"><a href="mnu.asp">Return to Menu</b></a>
						</font></b>
					</td></tr>
					</table>
					</td></tr>	
					</table>
					</td>
					</tr>
					<% End If %>
        <tr>
		<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Search Categories</font></b></td>        
        </tr>
        <tr>
        <td width="50%" align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Category Name:</font></td>
        <td width="50%"><select name="categoryID" style="<%= C_FORMDESIGN %>" size="1"><%= sCategories %></select></td>
        </tr>
        <tr>
        <td width="100%" align="center" valign="top" colspan="2"><input type="image" name="bFindCategory" src="images/edit.gif" WIDTH="82" HEIGHT="21"> <input type="image" name="bDeleteCat" border="0" src="images/delete.gif" WIDTH="82" HEIGHT="21" border="0"></td>
        </tr>
        </table>
    </td>
    </tr>
        <tr>
		<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
        </tr>
</table>
</td>
</tr>
</table>
</form>
</body>
</html>

