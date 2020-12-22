<%@ Language=VBScript %>
<%	option explicit 
	Response.Buffer = True
		
	'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0023.asp
	
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
<!-- #include file='../SFLib/db.conn.open.asp' -->
<!--#include file="../SfLib/incDesign.asp"-->
<!-- #include file='../SFLib/adovbs.inc' -->
<!--#include file="../SfLib/incGeneral.asp"-->
<!-- #include file="incAdmin.asp" -->
<%
Dim sAction, rsAff, sSQL, sAff, sFilter, rsAffEdit, rsAffRemove

sAction = Request.QueryString("action")

If Request.Form("editAffSubmit.x") <> "" Then
	Set rsAffEdit = Server.CreateObject("ADODB.Recordset")
	rsAffEdit.Open "sfAffiliates", cnn, adOpenStatic, adLockOptimistic, adCmdTable
	sFilter = "affName = '" & trim(Request.Form("txtName")) & "'"
	rsAffEdit.Filter = sFilter
	
	rsAffEdit.Fields("affName") = trim(Request.Form("txtName"))
	rsAffEdit.Fields("affCompany") = trim(Request.Form("txtCompany"))
	rsAffEdit.Fields("affAddress1") = trim(Request.Form("txtAdd1"))
	rsAffEdit.Fields("affAddress2") = trim(Request.Form("txtAdd2"))
	rsAffEdit.Fields("affCity") = trim(Request.Form("txtCity"))
	rsAffEdit.Fields("affState") = trim(Request.Form("txtState"))
	rsAffEdit.Fields("affZip") = trim(Request.Form("txtZip"))
	rsAffEdit.Fields("affCountry") = trim(Request.Form("txtCountry"))
	rsAffEdit.Fields("affPhone") = trim(Request.Form("txtPhone"))
	rsAffEdit.Fields("affFAX") = trim(Request.Form("txtFax"))
	rsAffEdit.Fields("affEmail") = trim(Request.Form("txtEmail"))
	rsAffEdit.Fields("affHttpAddr") = trim(Request.Form("txtWeb"))
	rsAffEdit.Fields("affPassword") = trim(Request.Form("Password"))
	rsAffEdit.Update 
	
	closeObj(rsAffEdit)
End If

%>
<html>
<head>
	<title>SF Menu Page</title>
</head>

<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
<tr>
<td>
<table width="100%" border="0" cellspacing="1" cellpadding="3">
<tr>
<%If C_BNRBKGRND = "" Then%>
	<td colspan="4" align="center" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %></font></b></td>
<%Else%>
	<td colspan="4" align="center" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%End If%>
</tr>
<tr>
<td colspan="4" align="center" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Affiliate Partners Administration</font></b></td>
</tr>
<tr>
<td align="center" background="<%= C_BKGRND3 %>" bgcolor="<%= C_BGCOLOR3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" SIZE="<%= C_FONTSIZE3 %>"><b>Instructions:</b> ...</font></td>
</tr>
<tr>
<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1  %>">Affiliate Partners</font></b></td>
</tr>
<tr>
<td bgcolor="<%= C_BGCOLOR4 %>">
	<table border="0" cellpadding="0" cellspacing="5" width="100%">
	<%
	Set rsAff = Server.CreateObject("ADODB.Recordset")
	If sAction = "Edit" Then
		rsAff.Open "sfAffiliates", cnn, adOpenStatic, adLockOptimistic, adCmdTable
		sFilter = "affName = '" & Request.QueryString("partner") & "'"
		rsAff.Filter = sFilter
	%>
		<form method="post" action="m0023.asp" name="frmAffEdit&quot;">
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Affiliate ID:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" readonly style="<%= C_FORMDESIGN %>" size="15" name="txtName" value="<%= rsAff.Fields("affName")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Company:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="20" name="txtCompany" value="<%= rsAff.Fields("affCompany")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Address Line 1:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="25" name="txtAdd1" value="<%= rsAff.Fields("affAddress1")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Address Line 2:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="25" name="txtAdd2" value="<%= rsAff.Fields("affAddress2")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">City:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="15" name="txtCity" value="<%= rsAff.Fields("affCity")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">State:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="15" name="txtState" value="<%= rsAff.Fields("affState")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Zip/Postal Code:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="10" name="txtZip" value="<%= rsAff.Fields("affZip")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Country:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="20" name="txtCountry" value="<%= rsAff.Fields("affCountry")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Phone Number:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="15" name="txtPhone" value="<%= rsAff.Fields("affPhone")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Fax Number:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="15" name="txtFax" value="<%= rsAff.Fields("affFax")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Email:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="20" name="txtEmail" value="<%= rsAff.Fields("affEmail")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Web Site:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="25" name="txtWeb" value="<%= rsAff.Fields("affHttpAddr")%>"></td>
	    </tr>
	    <tr>
	    <td align="left" width="20%" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Password:</font></td>
	    <td align="left" width="80%" nowrap><input type="text" style="<%= C_FORMDESIGN %>" size="15" name="Password" value="<%= rsAff.Fields("affPassword") %>"></td>
	    </tr>
	    <tr>
	    <td align="center" width="100%" colspan="2"><input type="image" src="images/submit.gif" border="0" name="editAffSubmit" WIDTH="108" HEIGHT="21"></td>
	    </tr>
	    </form>
	<%
	ElseIf sAction = "Remove" Then
		rsAff.Open "sfAffiliates", cnn, adOpenStatic, adLockOptimistic, adCmdTable
		sFilter = "affName = '" & Request.QueryString("partner") & "'"
		rsAff.Filter = sFilter
		
		rsAff.Delete 
		
		sSQL = "SELECT orderID, orderTradingPartner FROM sfOrders WHERE orderTradingPartner = '" & Request.QueryString("partner") & "'"
		Set rsAffRemove = Server.CreateObject("ADODB.Recordset")
		rsAffRemove.Open sSQL, cnn, adOpenStatic, adLockOptimistic, adCmdText
		Do Until rsAffRemove.EOF 
			rsAffRemove.Fields("orderTradingPartner") = ""
			rsAffRemove.Update
			rsAffRemove.MoveNext 
		Loop
		closeObj(rsAffRemove)
		Response.Redirect "m0023.asp"
	%>
	<%
	Else
		sSQL = "SELECT affName FROM sfAffiliates"
		rsAff.Open sSQL, cnn, adOpenForwardOnly, adLockReadOnly, adCmdText
	%>
		<tr>
		<td width="80%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><b>Affiliate Partner</b></font></td>
		<td width="10%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><b>Edit</b></font></td>
		<td width="10%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><b>Remove</b></font></td>
		</tr>
		<%
		Do Until rsAff.EOF
			sAff = rsAff.Fields("affName")
		%>
			<tr>
			<td width="80%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><%=sAff%></font></td>
			<td width="10%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><a href="m0023.asp?action=Edit&amp;partner=<%=trim(sAff)%>">Edit</a></font></td>
			<td width="10%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><a href="m0023.asp?action=Remove&amp;partner=<%=trim(sAff)%>">Remove</a></font></td>
			</tr>
		<%	rsAff.MoveNext 
		Loop
		%>
	
	<%
	End If
	closeObj(rsAff)
	closeObj(cnn)
	%>
	</table>
</td>
</tr>
<tr>
<td colspan="4" background="<%= C_BKGRND7 %>" bgcolor="<%= C_BGCOLOR7 %>"><p align="center"><b><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></font></b></td>
</tr>
</table>
</td>
</tr>
</table>
</body>
</html>
