<%	
	option explicit 
	Response.Buffer = True


'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0000.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   Web admin 

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
<!--#include file="../SFLib/incDesign.asp"-->
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<%
	Dim rsAdmin, sSSLPath, sDomainNamePath, sStoreName, sEzeeHelpAdmin, sEzeeHelpAct
	dim sadminSFID,sadminSFActive,iSavecart,iEmail
	
	If Request.Form("Submit.x") <> "" Then
		sStoreName		= Trim(Request.Form("storeName"))
		sEzeeHelpAdmin           = Trim(Request.Form("ezeeHelp"))
		sEzeeHelpAct		= Trim(Request.Form("ezeeHelpActive"))
		sadminSFID = Trim(Request.Form("sSFID"))
		sadminSFActive = Trim(Request.Form("sSFActive"))
		iSaveCart = Trim(Request.Form("sCartActive"))
		iEmail = Trim(Request.Form("sEmailActive"))
		
		If sadminSFActive = "" Then sadminSFActive = 0
		If iSaveCart = "" Then iSaveCart = 0
		If iEmail = "" Then iEmail = 0
		If sEzeeHelpAct = "" Then sEzeeHelpAct = 0
		
		Set rsAdmin = Server.CreateObject("ADODB.RecordSet")			
		rsAdmin.Open "sfAdmin", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
			rsAdmin.Fields("adminStoreName")	    = sStoreName
			rsAdmin.Fields("adminEzeeLogin")        = sEzeeHelpAdmin
			rsAdmin.Fields("adminEzeeActive")        = sEzeeHelpAct
			rsAdmin.fields("adminSFID") = sadminSFID 
			rsAdmin.fields("adminSFActive") = sadminSFActive
			rsAdmin.fields("adminSaveCartActive") = iSaveCart
			rsAdmin.fields("adminEmailActive") = iEmail  

			rsAdmin.Update 
 
		closeObj(rsAdmin)
	Else
		Set rsAdmin = Server.CreateObject("ADODB.RecordSet")		
		rsAdmin.Open "sfAdmin", cnn, adOpenStatic, adLockOptimistic, adCmdTable		
		sStoreName      = Trim(rsAdmin.Fields("adminStoreName"))
		sEzeeHelpAdmin       = Trim(rsAdmin.fields("adminEzeeLogin")) 
		sEzeeHelpAct    = Trim(rsAdmin.Fields("adminEzeeActive"))
		sadminSFID = Trim(rsAdmin.fields("adminSFID"))
		sadminSFActive =Trim(rsAdmin.fields("adminSFActive"))
		iSaveCart = Trim(rsAdmin.fields("adminSaveCartActive"))
		iEmail = Trim(rsAdmin.fields("adminEmailActive"))
		closeObj(rsAdmin)
	End If	
%>
	<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
<tr>
<td>
	<form method="post" action="m0000.asp" onSubmit="this.sSFID.optional=true;return sfCheck(this)" id="form1" name="form1">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
    <tr>
<%	If C_BNRBKGRND = "" Then %>
		<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %></font></b></td>
<%	Else %>
		<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%	End If %>      
    </tr>
    <tr>
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Path Configuration</font></b></td>        
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b></font></td>
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
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Store Domain Name</font></b></td>
        </tr>
        <tr>
        <td width="40%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Store Name:</font></td>
        <td width="60%"><input type="text" value="<%= sStoreName %>" name="storeName" size="40" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <td nowrap width="40%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Affliate Id:</font></td>
        <td nowrap width="60%"><input type="text" value="<%= sadminSFID %>" name="sSFID" size="40" style="<%= C_FORMDESIGN %>"><input type="checkbox" value="1" <%If sadminSFActive = "1" Then Response.Write "checked" %> name="sSFActive"> Acivate</td>
        </tr>
        <tr>
        <td nowrap width="40%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Ezee Help Login<br> (Requires Registration at<br> <a href="http://www.ezeehelp.com">www.ezeehelp.com</a>):</font></td>
        <td nowrap  width="60%" valign ="bottom"><input type="text" value="<%= sEzeeHelpAdmin %>" name="ezeeHelp" size="40" style="<%= C_FORMDESIGN %>"> <input type="checkbox" value="1" <%If sEzeeHelpAct = "1" Then Response.Write "checked" %> name="ezeeHelpActive"> Acivate</td>
        </tr>        
        <tr>
        <td nowrap width="40%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Save Cart:</font></td>
        <td nowrap width="60%"><input type="checkbox" value="1" <%If iSaveCart = "1" Then Response.Write "checked" %> name="sCartActive"></td>
        </tr>
        <tr>
        <td nowrap width="40%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Email to a Friend:</font></td>
        <td nowrap width="60%"><input type="checkbox" value="1" <%If iEmail = "1" Then Response.Write "checked" %> name="sEmailActive"></td>
        </tr>
 
        <tr>
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




