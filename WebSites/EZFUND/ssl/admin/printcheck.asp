<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.0
'   Date        :   1.6.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Check Printing Routines
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
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Customer E-Check</title>
</head>
<body bgcolor=ffffff>
<%
Dim DSN_Name
DSN_Name = Session("DSN_Name")
set Connection = Server.CreateObject("ADODB.Connection")
Connection.Open DSN_Name
SQL = "SELECT LCID FROM admin"
set RSAdmin = Connection.Execute (SQL)
Session("LCID") = Trim(RSAdmin("LCID"))
session.LCID = Session("LCID")
Set RSAdmin = nothing

	SQLStmt ="SELECT * FROM customer WHERE CUSTOMER_ID = " & Request("OrderID") & " "
	Set RSChck = Connection.Execute(SQLStmt)
%>	
  <table width="90%" border="3" cellpadding="0" cellspacing="5" align="left">
    <tr>
      <td>
        <table width="90%" border="0" align=center valign=center>
                <tr>
                  <td><font face=arial><small><b><%= RSChck("NAME") %></b></small></font></td>
                  <td align=right><font face=arial><small><b><%= RSChck("CHK_NO") %></b></small></font></td>
                </tr>
                <tr>
                  <td colspan=2><font face=arial><small><b><%= RSChck("Address_2") %></b></small></font></td>
                </tr>
                  <% If RSChck("Address_1") > "" Then %>
                  <tr>
                    <td colspan=2><font face=arial><small><b><%= RSChck("Address_1") %></b></small></font></td>
                </tr>
                <% End If %>
                <tr>
                  <td colspan=2><font face=arial><small><b><%= RSChck("City")&","&"&nbsp;"&RSChck("State")&"&nbsp;&nbsp;"&RSChck("Zip") %></b></small></font></td>
              </tr>
              <tr>
                <td colspan=2>&nbsp;</td>
              </tr>
              <tr>
                <td colspan=2 nowrap><font face=arial><small><b>Pay To:</b></small></font><hr size=1></td>
            </tr>
            <tr>
              <td colspan=2>&nbsp;</td>
            </tr>
            <tr>
              <td colspan=2 nowrap align=right><font face=arial><b>Amount: &nbsp;&nbsp;<%= FormatCurrency(RSChck("GRAND_TOTAL")) %></b></font><hr size=1></td>
              </tr>
              <tr>
                <td colspan=2><font face=arial><big><b><%= RSChck("BANK_NAME") %></b></big></font></td>
              </tr>
              <tr>
                <td colspan=2>&nbsp;</td>
              </tr>
              <tr>
                <td nowrap><font face=arial><small><b>Memo:</b></small></font><hr size=1></td>
				<td nowrap align=left><font face=arial><small>Payment Authorized by Account Holder <br>
Indemnification Agreement Provided by <br>
Depositor. <br><b>Electronically Signed:</b></small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=RSChck("NAME")%></font><hr size=1></td>
              </tr>
              <tr>
                <td colspan=2 nowrap>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <font face="MICR 013 BT" size="5"><b>A<%= Trim(RSChck("ROUTING_NO")) %>A<%= Trim(RSChck("CHK_ACCT_NO")) %>B</font>
                  </td>
                </tr>
              </table>
        </td>
      </tr>
    </table>
    <%
       Connection.Close
       Set Connection = Nothing
    %>
  </center>
</body>
</html>
