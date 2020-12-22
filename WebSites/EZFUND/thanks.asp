
<%@ Language=VBScript %>
<%	option explicit 
%>
<!--#include file="SFLib/db.conn.open.asp"-->
<!--#include file="SFLib/ADOVBS.inc"-->
<!--#include file="SFLib/incGeneral.asp"-->
<!--#include file="sfLib/incDesign.asp"-->
<%
'@BEGINVERSIONINFO

'@APPVERSION: 50.4014.0.2

'@FILENAME: thanks.asp
 


'@DESCRIPTION: Popup Confirmation Page

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO

Dim sHas, sProdName, iQuantity, sProdUnit, sProdMessage, sResponseMessage
'sSearchPath = Request.ServerVariables("HTTP_REFERER")

sProdName = Request.QueryString("sProdName")
iQuantity = Request.QueryString("iQuantity")
sProdUnit = Request.QueryString("sProdUnit")
sProdMessage = Request.QueryString("sProdMessage")
sResponseMessage = Request.QueryString("sResponseMessage")

If Request.Cookies("sfThanks")("PreviousAction") <> "" Then
	Response.Cookies("sfThanks").Expires = Now()
End If

' For Safety
'If sSearchPath = "" Or instr(1,sSearchPath,"login.asp",1) Then
'	sSearchPath = "search.asp"
'End If	

closeObj(cnn)	
%>

<html>
<head>
<SCRIPT language="javascript">
function linkCorrect() {
	if (window.document.links.length > 1) {
		for (i=0;i<window.document.links.length;i++) {
			if (window.document.links[i].href != "javascript:window.close()") {
				temp = window.document.links[i].href
				window.document.links[i].href = "javascript:openParent('" + temp + "')"
			}
		}
	}
}
function openParent(sHref) {
	window.opener.location = sHref;
	window.close();
}
</SCRIPT>
<link rel="stylesheet" href="sfCSS.css" type="text/css">
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title><%= C_STORENAME %>-Order Item Confirmation</title>


<!--Header Begin -->
<link rel="stylesheet" href="sfCSS.css" type="text/css">
</head>

<body  link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>" onLoad="javascript:linkCorrect()">

<table border="0" cellpadding="1" cellspacing="0" class="tdbackgrnd" width="100%" align="center">
  <tr>
    <td>
      <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
          <td align="middle" class="tdTopBanner"><%= C_STORENAME %></td>
    
        </tr>
<!--Header End -->
        <tr>
          <td class="tdContent2">
            <%if iQuantity=0 then%>
            <hr noshade color="#000000" size="1" width="90%">
            <p align="center"><font class="Content_Large"><b>We're Sorry!</b></font><br>
            <b><%= sResponseMessage %></b>
            <br>
            <%else%>
            <hr noshade color="#000000" size="1" width="90%">
            <p align="center"><font class="Content_Large"><b>Thank You!</b></font><br>
            
            <b><%= iQuantity %> &nbsp; <%= sProdUnit %>
            <%= sProdName %>&nbsp;<%= sResponseMessage %></b>
            <p align="center">
            <b><%= sProdMessage %></b><br>
            
            <%end if%>
            <br>
	        <b><a href="javascript:window.close()">Close</a>
            <hr noshade color="#000000" size="1" width="90%">
            </b>
	        </td>        
            </tr>
<!--Footer Begin -->
            <tr>
              <td class="tdFooter"></td>
                </tr>
                </table>
              </td>
            </tr>
            </table>
            </body>
          </html>
<!--Footer End -->



