<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.4
'   Date        :   7.14.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Invoice Printing Routines
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
<title>Customer Invoice</title>
</head>
<body bgcolor=ffffff>
<% 
Dim DSN_Name
DSN_Name = Session("DSN_Name")
set Connection = Server.CreateObject("ADODB.Connection")

Connection.Open DSN_Name

'*************************************************************************************************
sql2 = "Select TOP 1 CUSTOMER_ID from CUSTOMER where CUSTOMER_ID < " & Request("OrderID") & " " _
& " and GRAND_TOTAL <> '0' Order by  CUSTOMER_ID DESC "
Set RSback = Connection.Execute(sql2)
If NOT(RSback.BOF OR RSback.EOF) Then
%>
<form action="printinvoice.asp?OrderID=<%= RSback("CUSTOMER_ID") %>"
 method="Post" id=form1 name=form1>
<input type="submit" value="Previous Order" name=submit1></form>
<%
End If
Set RSback = Nothing

sql1 = "Select TOP 1 CUSTOMER_ID from CUSTOMER where CUSTOMER_ID > " & Request("OrderID") & " " _
& " and GRAND_TOTAL <> '0' Order by  CUSTOMER_ID ASC "

Set RSnext = Connection.Execute(sql1)
If NOT(RSnext.BOF OR RSnext.EOF) Then
%>
<form action="printinvoice.asp?OrderID=<%= RSnext("CUSTOMER_ID") %>" method="Post" id=form2 name=form2>
<input type="submit" value="Next Order" name=submit1></form>
<%
End If
Set RSnext = Nothing
'*************************************************************************************************

SQL = "SELECT LCID FROM admin"
set RSAdmin = Connection.Execute (SQL)
session.LCID = RSAdmin("LCID")
Set RSAdmin = nothing

	'Report 3 is the Customer Invoice Report.
	SQLStmt = "SELECT * FROM Customer WHERE CUSTOMER_ID = "
	SQLStmt = SQLStmt & "" & Request("OrderID") & " "
	Set RSCustDetail = Connection.Execute(SQLStmt)
	SQLStmt = "SELECT orders.AttributeA, orders.AttributeB, orders.AttributeC, " _
	& "orders.Product_ID, orders.Description, orders.Price, orders.Quantity, " _
	& "orders.Total FROM orders WHERE ORDER_ID = " & Request("OrderID") & " "

	'Response.Write (SQLStmt)
	Set RSOrderDetail = Connection.Execute(SQLStmt)
	
	GrandTotal = Ccur(RSCustDetail("GRAND_TOTAL"))
	SubTotal = Ccur(RSCustDetail("SUB_TOTAL"))
	Tax = ccur(RSCustDetail("TAX"))
	ShipAmt = ccur(RSCustDetail("SHIPPING_AMT"))
	HandlingCOD = FormatCurrency(GrandTotal-(SubTotal+Tax+ShipAmt))

	
%>
<!--#include file="invoice_head.htm"-->
<!--#include file="../SFLib/processor.inc"-->
<table align=center bgColor=ffffff border=0 cellPadding=1 cellSpacing=1 width="100%">
    <tr>
      <td bgcolor=000000 colspan=2><font face=arial color=ffffff><small><b>Sold To</b></small></font></td>
    </tr>
    <tr>
      <td><font face=arial><small><%= RSCustDetail("NAME") %></small></font></td>
      <td><font face=arial><small>Order Date: <%= FormatDateTime(RSCustDetail("ORDER_DATE"),vbShortDate) %></small></font></td>
    </tr>
    <tr>
      <td><font face=arial><small><%= RSCustDetail("COMPANY") %></small></font></td>
      <td><font face=arial><small>Order No: <%=RSCustDetail("Customer_ID") %></small></font></td>
    </tr>
    <tr>
      <td><font face=arial><small><%= RSCustDetail("ADDRESS_2") %></small></font></td>
      <td><font face=arial><small>Payment Method: <%= Trim(RSCustDetail("PAYMENT_METHOD")) %></small></font></td>
    </tr>
      <td colspan=2><font face=arial><small><%= RSCustDetail("ADDRESS_1") %></small></font></td>
    
    <tr>
      <td colspan=2><font face=arial><small><%= RSCustDetail("CITY")&", "&RSCustDetail("STATE")&"&nbsp;&nbsp;&nbsp;"&RSCustDetail("ZIP")&"&nbsp;&nbsp;&nbsp;&nbsp;"&RSCustDetail("COUNTRY")%></small></font></td>
    </tr>
	<%	
		Dim ShipMsg	
		ShipMsg = RSCustDetail("SHIP_MESSAGE")
	    If ShipMsg <> "" Then
	%>	
    <tr>
      <td bgcolor=000000 colspan=2><font face=arial color=ffffff><small><b>Special Instructions:</b></small></font></td>
    </tr>
    <tr>
      <td colspan="2"><font face=arial><small><%= ShipMsg %></small></font></td>
    </tr>
  <% End If %>
        
  <% If (RSCustDetail("SHIP_STATE")) <> "" OR (RSCustDetail("SHIP_COUNTRY")) <> "" Then %>
    <tr>
      <td bgcolor=000000 colspan=2><font face=arial color=ffffff><small><b>Ship To</b></small></font></td>
    </tr>
    <tr>
      <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_NAME") %></small></font></td>
      </tr>
    <tr>
      <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_COMPANY") %></small></font></td>
    </tr>
    <tr>
      <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_ADDRESS_2") %></small></font></td>
      <tr>
        <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_ADDRESS_1") %></small></font></td>
      </tr>
      <tr>
        <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_CITY") %></small></font></td>
      </tr>
      <tr>
        <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_STATE") %></small></font></td>
      </tr>
      <tr>
        <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_ZIP") %></small></font></td>
      </tr>
      <tr>
        <td colspan=2><font face=arial><small><%= RSCustDetail("SHIP_COUNTRY") %></small></font></td>
      </tr>
      <% End If %>
      <% If Trim(RSCustDetail("PAYMENT_METHOD")) = "Credit Card" Then %>
      <tr>
        <td bgcolor=000000 colspan=2><font face=arial color=ffffff><small><b>Credit Card Information</b></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Card Type:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("CARD_TYPE") %></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Card Number:</small></font></td>
        <% If Instr(RSCustDetail("CARD_NO"),"N") Then %>
        <td align=left><font face=arial><small><%= DecodeCC(Trim(RSCustDetail("CARD_NO")), Trim(RSCustDetail("CUSTOMER_ID"))) %></small></font></td>
		<% Else %>
		<td align=left><font face=arial><small><%= RSCustDetail("CARD_NO") %></small></font></td>
		<% End If %>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Card Name:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("NAME") %></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Expiration Date:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("CARD_EXP") %></small></font></td>
      </tr>
      <% ElseIf Trim(RSCustDetail("PAYMENT_METHOD")) = "Electronic Check" Then %>
      <tr>
        <td bgcolor=000000 colspan=2><font face=arial color=ffffff><small><b>E-Check Information</b></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Customer Name:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("NAME") %></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Bank Name:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("BANK_NAME") %></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Routing Number:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("ROUTING_NO") %></small></font></td>
      </tr>
      <tr>
        <td align=left><font face=arial><small>Account Number:</small></font></td>
        <td align=left><font face=arial><small><%= RSCustDetail("CHK_ACCT_NO") %></small></font></td>
      </tr>
      <% ElseIf Trim(RSCustDetail("PAYMENT_METHOD")) = "Purchase Order" Then%>
        <tr>
			<td bgcolor=000000 colspan=4><font face=arial color=ffffff><small><b>Purchase Order Information</b></small></font></td>
        </tr>
		<tr>
			<td align=left><font face=arial><small>Purchase Order Number:</small></font></td>
			<td align=left><font face=arial><small><%= RSCustDetail("PURCH_ORDER_NO")%></small></font></td>
		</tr>
      <% End If %>
	  <tr>
      </table>
      <table align=center bgColor=ffffff border=0 cellPadding=1 cellSpacing=1 width="100%">
        <tr>
          <td bgcolor=000000 align=center><font face=arial color=ffffff><small><b>Product Code</b></small></font></td>
          <td bgcolor=000000 align=center><font face=arial color=ffffff><small><b>Description</b></small></font></td>
          <td bgcolor=000000 align=center><font face=arial color=ffffff><small><b>Unit Price</b></small></font></td>
          <td bgcolor=000000 align=center><font face=arial color=ffffff><small><b>Quantity</b></small></font></td>
          <td bgcolor=000000 align=center><font face=arial color=ffffff><small><b>Total</b></small></font></td>
        </tr>
        <% 
RSOrderDetail.MoveFirst
CurrentRecord = 0
Do While NOT RSOrderDetail.EOF
AttA = RSOrderDetail("AttributeA")
AttB = RSOrderDetail("AttributeB")
AttC = RSOrderDetail("AttributeC")
If AttA <> "" Then ATTResponse = AttA End If
If AttB <> "" Then ATTResponse = AttResponse&", "&AttB End If
If AttC <> "" Then ATTResponse = AttResponse&", "&AttC End If
ATTResponse = ATTResponse&" "
%>
        <tr>
          <td align=left><font face=arial><small><%= RSOrderDetail("PRODUCT_ID") %></b></td>
            <td align=left><font face=arial><small><%= ATTResponse&RSOrderDetail("DESCRIPTION") %></b></td>
              <td align=right><font face=arial><small><%= FormatCurrency(RSOrderDetail("PRICE")) %>&nbsp;</b></td>
                <td align=right><font face=arial><small><%= RSOrderDetail("QUANTITY") %>&nbsp;</b></td>
                  <td align=right><font face=arial><small><%= FormatCurrency(RSOrderDetail("TOTAL")) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
                  </tr>
                  <%
AttResponse = ""
RSOrderDetail.MoveNext
CurrentRecord = CurrentRecord = 1
Loop
%>
                  <tr>
	                <td colspan=5 size=1><hr></td>
                  </tr>
                  <tr>
                    <td colspan=4 align=right><font face=arial><small>Sub Total:</small></font></td>
                    <td align=right><font face=arial><small><%= FormatCurrency(RSCustDetail("SUB_TOTAL")) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</small></font></td>
                  </tr>
                  <tr>
                    <td colspan=4 align=right><font face=arial><small>Tax:</small></font></td>
                    <td align=right><font face=arial><small><%= FormatCurrency(RSCustDetail("TAX")) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</small></font></td>
                  </tr>
                  <tr>
                    <td colspan=4 align=right><font face=arial><small><%=RSCustDetail("SHIPPING_METHOD") %></small></font></td>
                    <td align=right><font face=arial><small><%= FormatCurrency(RSCustDetail("SHIPPING_AMT")) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</small></font></td>
                  </tr>
                  <% If HandlingCOD > FormatCurrency(0) Then %>
                  <tr>
                    <td colspan=4 align=right><font face=arial><small>Handling/COD Charges:</small></font></td>
                    <td align=right><font face=arial><small><%= FormatCurrency(HandlingCOD) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</small></font></td>
                  </tr>
                  <% End If %>    
                  <tr>
                    <td colspan=4 align=right><font face=arial><small>Total</small></font></td>
                    <td align=right><font face=arial><small><%= FormatCurrency(RSCustDetail("GRAND_TOTAL")) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</small></font></td>
                  </tr>
                  <tr>
                    <td colspan=5 align=right>
                      <br>
		              <script Language="Javascript"> 
		 var NS = (navigator.appName == "Netscape");
		 var VERSION = parseInt(navigator.appVersion);
		 if (VERSION > 3) {
		 document.write('<form><input type=button value="Print this Page" name="Print" onClick="printthis()"></form>'); 
		 }
		              </script></td>
                  </tr>
                </table>
                <%
     Connection.Close
     Set Connection = Nothing 
  %>
              </body>
            </html>

