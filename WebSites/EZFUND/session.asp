<%@ LANGUAGE="VBScript" %>
<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.0
'   Date        :   1.6.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Session Display Routine
'   Notes       :   There are no configurable elements in this file.
'
'                         COPYRIGHT NOTICE
'
'   The contents of this file is protected under the United States
'   copyright laws as an unpublished work, and is confidential and
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in 
'   whole or in part without the expressed written permission of 
'   LaGarde, Incorporated is expressely prohibited.
'
'   (c) Copyright 1998 by LaGarde, Incorporated.  All rights reserved.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<title>Session ID</title>
<meta name="ProgId" content="FrontPage.Editor.Document">
</head>
<body>
<div align="center"><center>
  <table border="0" cellpadding="3" cellspacing="0" width="90%">
    <tr>
      <td width="50%"><font face=arial>Your Session ID Is: </font></td>
      <td width="50%">
        <table border="1" cellpadding="3" cellspacing="0" width="100" height="24" align="center">
		  <tr>
            <td width="100%" height="16" align="center"><font face=arial><% Response.Write Session.SessionID %></font></td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
  </center></div>
</body>
</html>
