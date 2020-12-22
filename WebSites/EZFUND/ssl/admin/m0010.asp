<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<%
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0010.asp
	
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

Dim sProdID, sProdList, objRS
If Request.Form("btnDeleteProdID.x") <> "" Then
	sProdID =	Trim(Request.Form("txtEditProdID"))
	If sProdID = "" Then
		sProdID = Trim(Request.Form("sltEditProdID"))
	End If
   Call setDeleteProd(sProdID)
ElseIf Request.Form("btnInputProdID.x") <> "" Then
	sProdID =	Trim(Request.Form("txtEditProdID"))
	If sProdID = "" Then
		sProdID = Trim(Request.Form("sltEditProdID"))
	End If
	Response.Redirect("m0010i.asp?ProdID=" & sProdID)
End If	   

	Set objRS = getProductList()
	Do While Not objRS.EOF 		
		sProdList = sProdList & "<option value=""" & objRS("prodID") & """>" &  objRs("prodName") & "</option>"
		objRS.MoveNext
	Loop
	closeobj(objRS)	

%>
<html>
<head>
<title>SF Menu Page</title>
</head>
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">

<form method="post" name="frmInputProdID" action="m0010.asp">
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
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Edit/Delete Products</font></b></td>        
    </tr>
    <tr>
	<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>Select a product from the drop-down box, or enter an item's product ID.  Click the <B>Edit</B> button to begin modifying the product record, or click the <B>Delete</B> button to delete a product record from the database.  Additional instructions on using this function can be found in the StoreFront help files.</font></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <tr>
		<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Search Products</font></b></td>        
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product ID:</font></td>
        <td width="50%"><input name="txtEditProdID" style="<%= C_FORMDESIGN %>" size="25"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product :</font></td>
        <td width="50%"><select name="sltEditProdID" style="<%= C_FORMDESIGN %>" size="1"><option></option><%= sProdList %></select></td>
        </tr>
        <tr>
        <td width="100%" align="center" valign="top" colspan="2"><input Type="image" border="0" name="btnInputProdID" src="images/edit.gif" WIDTH="82" HEIGHT="21"> <input Type="image" border="0" name="btnDeleteProdID" src="images/delete.gif" WIDTH="82" HEIGHT="21"></td>
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

