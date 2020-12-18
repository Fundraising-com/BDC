<%	
	option explicit 
	Response.Buffer = True
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0001.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   web based admin tool 

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
	' Variable Declarations
	Dim objRS, dMailMethod, sMailServerAddress, sPrimaryEmail, sSecondaryEmail, sMailSubject, sMailMessage, cSubscription
	Dim rsAdmin
	
	Set objRS = getList(1,"sfSelectValues", "slctvalMailMethod") 
	Set rsAdmin = Server.CreateObject("ADODB.RecordSet")
	rsAdmin.Open "sfAdmin", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
If Request.Form("btnSubmit.x") <> "" Then
		sPrimaryEmail       = Request.Form("sPrimaryEmail")
		sSecondaryEmail     = Request.Form("sSecondaryEmail")
		sMailSubject        = Request.Form("sMailSubject")
		sMailMessage        = Request.Form("sMailMessage")
		cSubscription       = Request.Form("cSubscription")
		
		If cSubscription = "" Then
			cSubscription = 0
		Else
			cSubscription = 1
		End If
		
		rsAdmin.Fields("adminPrimaryEmail")          = sPrimaryEmail
		rsAdmin.Fields("adminSecondaryEmail")        = sSecondaryEmail
		rsAdmin.Fields("adminEmailSubject")          = sMailSubject
		rsAdmin.Fields("adminEmailMessage")          = sMailMessage
		rsAdmin.Fields("adminSubscribeMailIsActive") = cSubscription
		rsAdmin.Update 
End If  
		sPrimaryEmail      = rsAdmin.Fields("adminPrimaryEmail")
		sSecondaryEmail    = rsAdmin.Fields("adminSecondaryEmail")
		sMailSubject       = rsAdmin.Fields("adminEmailSubject")
		cSubscription      = rsAdmin.Fields("adminSubscribeMailIsActive")
		sMailMessage       = rsAdmin.Fields("adminEmailMessage")
		'''''''''''''''''''''''''''''''''''''''''''''''''''''' 
%>
<form method="post" action="m0001.asp" onSubmit="this.sMailServerAddress.option=true;this.sSecondaryEmail.optional=true;return sfCheck(this)">
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
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Mail Configuration</font></b></td>        
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Configure
      the subject line, opening paragraph for order confirmation emails to be
      sent to customers making purchases from your store.&nbsp; Set the email
      address to which merchant sale notification should be sent.</font></td>
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <%  If Request.Form("btnSubmit.x") <> "" Then %>
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
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Email Settings</font></b></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Primary Email Address:</font></td>
        <td width="50%"><input name="sPrimaryEmail" title="Primary Email" value="<%= sPrimaryEmail %>" style="<%= C_FORMDESIGN %>" size="20"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Secondary Email Address:</font></td>
		<td width="50%"><input name="sSecondaryEmail" title="Secondary Email" value="<%= sSecondaryEmail %>" style="<%= C_FORMDESIGN %>" size="20"></td>        
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Email Subject:</font></td>
        <td width="50%"><input name="sMailSubject" title="Mail Subject" value="<%= sMailSubject%>" style="<%= C_FORMDESIGN %>" size="30" value="StoreFront Maddog Order"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Mail Message:</font></td>
        <td width="50%"><textarea name="sMailMessage" title="Mail Message" cols="30" rows="4" style="<%= C_FORMDESIGN %>"><%= sMailMessage %></textarea></td>
        </tr>
 		<tr>                                  
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Display Mail Subscribtion Checkbox on Order Form:</font></td>
        <% 
			If cSubscription = 0 Then
				Response.Write "<td width=""50%""><input name=""cSubscription"" type=""checkbox"" value=""ON""></td>"
			Else
				Response.Write "<td width=""50%""><input CHECKED name=""cSubscription"" type=""checkbox"" value=""ON""></td>"
			End If
        %>		
        </tr>
        <tr>
        <td width="100%" align="center" valign="top" colspan="2"><input type="image" name="btnSubmit" alt="Submit" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21"></td>
        </tr>        
        </table>
        <br>
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
<% 
	closeObj(objRS)
	closeObj(rsAdmin)
%>

</body>

</html>

