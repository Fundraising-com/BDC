<%	
	option explicit 
	Response.Buffer = True
	'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0015.asp
	
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
<%
Dim bDisplayingChanges, sMfgName, sMfgDescription, sMfgImageURL, aGetMfg,_
	sMfgPageURL,sActiveOnOffString,iRecordFound, sError, sDescription, sImageURL,sPageURL, sForceEdit, sMfgType, bForceEdit, iMfgID, rsMfg, sLocalSQL
	
	If Request.Form("bEditedMfg.x") <> "" Then
		
		sMfgName	= Trim(Request.Form("mfgName"))
		iRecordFound	= findMfgRecord(sMfgName) 
		sMfgType		= Trim(Request.Form("MfgType"))		
		bForceEdit		= Trim(Request.Form("ForceEdit"))		
		If bForceEdit = "" Then
			bForceEdit = 0
		End If	
		
		If sMfgType = "NewMfg" And  iRecordFound > 1 And bForceEdit <> 1 Then
			sError = "The manufacturer '" & sMfgName & "' already exists. </font><br><font size='2' face='verdana'>Are you sure you want to overwrite it? <br>Press Submit again to overwrite."			
			' Still give them what they filled out
			sDescription	= Trim(Request.Form("mfgDescription"))
			bForceEdit		= 1				

			If iIsActive = "" Then iIsActive = 0	
		ElseIf sMfgType = "NewMfg" And  iRecordFound > 1 And bForceEdit = 1 Then
			sDescription	= Trim(Request.Form("mfgDescription"))
			bForceEdit		= 1				
											
			sLocalSQL = "SELECT mfgName, mfgNotes FROM sfManufacturers WHERE mfgID =" &  iRecordFound
							
			Set rsMfg = Server.CreateObject("ADODB.RecordSet")
			rsMfg.Open sLocalSQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText
					
			If Not rsMfg.EOF Then
				rsMfg.Fields("mfgName")		   =	sMfgName
				rsMfg.Fields("mfgNotes")       =	sDescription
				rsMfg.Update
			End If	
			closeobj(rsMfg)	
			bDisplayingChanges = 1	
		ElseIf sMfgType = "OldMfg" And  iRecordFound > 1 Then
			sDescription	= Trim(Request.Form("mfgDescription"))
			bForceEdit		= 1			

			sLocalSQL = "SELECT mfgName, mfgNotes FROM sfManufacturers WHERE mfgID =" &  iRecordFound
							
			Set rsMfg = Server.CreateObject("ADODB.RecordSet")
			rsMfg.Open sLocalSQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText
					
			If Not rsMfg.EOF Then
				rsMfg.Fields("mfgName")		   =	sMfgName
				rsMfg.Fields("mfgNotes")       =	sDescription
				rsMfg.Update
			End If	
			closeobj(rsMfg)		
			bDisplayingChanges = 1				
		Else	
			sDescription	= Trim(Request.Form("mfgDescription"))
			
			Set rsMfg = Server.CreateObject("ADODB.RecordSet")
			rsMfg.Open "sfManufacturers", cnn, adOpenDynamic, adLockOptimistic, adCmdTable
			
			rsMfg.AddNew		
			rsMfg.Fields("mfgName")		   =	sMfgName
			rsMfg.Fields("mfgNotes")       =	sDescription
			rsMfg.Update
				
			closeobj(rsMfg)		
			bDisplayingChanges = 1		
		End If		
		
	ElseIf Trim(Request.QueryString("mfgID")) <> "" Then	
		iMfgID	= Trim(Request.QueryString("mfgID"))
		sMfgType		= "OldMfg"
		Set rsMfg = Server.CreateObject("ADODB.Recordset")
		sLocalSQL = "SELECT mfgName, mfgNotes FROM sfManufacturers WHERE mfgID = " & iMfgID
		rsMfg.open sLocalSQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText 	
		
		sMfgName	    = trim(rsMfg.Fields("mfgName"))
		sDescription	= trim(rsMfg.Fields("mfgNotes"))
	
	Else 
		sMfgType = "NewMfg"	
	End If
%>

<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<form method="post" id="editMfgForm" name="editMfgForm">
	<input type="hidden" name="referringForm" value="editMfg">
	
<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">

<tr>
	<td>
		<table width="100%" border="0" cellspacing="1" cellpadding="3">
			<tr>
<%	If C_BNRBKGRND = "" Then %>
				<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>">
					<%= C_STORENAME %></font></b>
				</td>
<%	Else %>
				<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>">
					<img src="<%= C_BNRBKGRND %>" border="0">
				</td>				
<%	End If %> 
			</tr>
			<tr>
				<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>">
					<b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">
						<% If sMfgType = "OldMfg" Then %>
						Edit Manufacturer: <%=sMfgName%>
						<% Else %>
						New Manufacturer
						<% End If %>
					</font></b>
				</td>    
			</tr>
			<tr>
				<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>">
					<b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">
						Instructions:</B> Enter the information to create a new manufacturer below, then click the <B>Submit</b> button to create the manufacturer.  You will then be able to create product items listed under this manufacturer, and customers will be able to search your inventory using this manufacturer in their criteria.
					</b></font>
				</td>
			</tr>
			<tr>
				<td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
					<table border="0" width="100%" cellpadding="4" cellspacing="0">
			<% If bDisplayingChanges = 1 Or sError <> "" Then %>
						<tr>
							<td width="100%" colspan="2" align="center" height="90" valign="center">
								<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
								<tr>
									<td width="100%">
									<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
										<tr>
										<td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
										<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>">
											<% If sError <> "" Then 
												Response.Write	sError	
											   Else 
											%>
												Database Updated
											</font>
											<br>
											<font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>">
											<a href="menu.asp">Return to Menu</a>
											<% End If %>
											</font>
										</b>
										</td>
										</tr>
									</table>
									</td>
									</tr>	
								</table>
							</td>
						</tr>
			<% End If %>
						<tr>
							<td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>">
								<b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">
										<% If sMfgType = "OldMfg" Then %>
										Edit Manufacturer: <%=sMfgName%>
										<% Else %>
										New Manufacturer
										<% End If %>
								</font></b>
							</td>
						</tr>


						<tr>
							<td width="50%" align="right" valign="top">
								<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
									Manufacturer Name:
								</font>
							</td>
							<td width="50%">
								<input type="text" name="mfgName" value="<%= sMfgName %>" size="30" style="<%= C_FORMDESIGN %>">
								
							</td>
						</tr>
						<tr>
							<td width="50%" align="right" valign="top">
								<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
									Manufacturer Description:
								</font>
							</td>
							<td width="50%">
								<textarea name="mfgDescription" cols="30" rows="4" style="<%= C_FORMDESIGN %>"><%= sDescription %></textarea>
							</td>
						</tr>
						<tr>
							<td width="100%" align="center" valign="top" colspan="2">
								<input type="image" name="bEditedMfg" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21">
								<input type="hidden" name="MfgType" value="<%= sMfgType %>">
								<input type="hidden" name="ForceEdit" value="<%= bForceEdit %>">	
							</td>
						</tr>
						<tr>
							<td width="100%" align="center" valign="top" colspan="2">
								<b><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><p align="center">
									<a href="m0015.asp">
										Add Another Manufacturer
									</a>
									|
									<a href="m0016.asp">
										Edit/Delete Another Manufacturer
									</a>
								</font></b>
							</td>
						</tr>
					
					</table>
				</td>
			</tr>
			<tr>
				<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>">
					<font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>">
						<p align="center">
							<b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b>
						</p>
					</font>
				</td>
			</tr>
		</table>
	</td>
</tr>

</table>
</form>
</body>
</html>


