<%@ LANGUAGE="VBScript" %>
<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.0
'   Date        :   1.6.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Order Complete Management Tool
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
<%

	DSN_Name = Session("DSN_Name")
	
	Set Connection = Server.CreateObject("ADODB.Connection")

	Connection.Open DSN_Name

	SQLStmt = "SELECT DOMAIN_NAME FROM Admin"

	Set RSAdmin = Connection.Execute(SQLStmt)


SndPage = RSAdmin("DOMAIN_NAME")
Set RSAdmin = nothing
%><html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta http-equiv="refresh" content="3; url=<%= SndPage %>">

<title>Order Complete</title>
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
</head>

<body bgColor=white>

<p>&nbsp;</p>
<div align="center"><center>

  <table border="0" cellpadding="10" cellspacing="10" width="80%">
    <tr>
      <td><p align="center"><IMG alt="WB01516_.gif (447 bytes)" height =68 src="../images/WB01516_.gif" width=69 > &nbsp;&nbsp;&nbsp; </p>
      </td>
      <td><p align="center">This order has already been completed.&nbsp; One moment while we prepare a new shopping session ....&nbsp;&nbsp;&nbsp;
    Thank You!</p>
      </td>
    </tr>
  </table>
  </center></div>
<% 
Connection.Close
Set Connection = nothing
Session.Abandon
%>
</body>
</html>
