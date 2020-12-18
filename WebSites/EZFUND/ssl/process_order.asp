<%@ Language=VBScript %>
<%	option explicit 
	Response.Buffer = True

%>

<!--#include file="SFLib/db.conn.open.asp"-->
<!--#include file="SFLib/incProcOrder.asp"-->
<!--#include file="error_trap.asp"-->
<!--#include file="SFLib/incDesign.asp"-->
<!--#include file="SFLib/incGeneral.asp"-->
<!--#include file="SFLib/ADOVBS.inc"-->
<!--#include file="SFLib/incAE.asp"-->

<%
    Const vDebug = 0
	'@BEGINVERSIONINFO

	'@APPVERSION: 50.4014.0.2

	'@FILENAME: process_order.asp
		 

	
	'@DESCRIPTION: Gathers Information For order.

	'@STARTCOPYRIGHT
	'The contents of this file is protected under the United States
	'copyright laws  and is confidential and proprietary to
	'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
	'expressed written permission of LaGarde, Incorporated is expressly prohibited.
	'
	'(c) Copyright 2000,2001 by LaGarde, Incorporated.  All rights reserved.
	'@ENDCOPYRIGHT

	'@ENDVERSIONINFO
dim tmpOrderQty
If Application("AppName") = "StoreFrontAE" Then 'SFAE
	tmpOrderQty=GetTotalOrderQTY
else
	tmpOrderQty=GetTotalOrderQTYSE
end if
if isnumeric(tmpOrderQty)=false or tmpOrderQty <= 0 then
'If Session("SessionID") = "" Then 'SFUPDATE
	Session.Abandon
	Response.Redirect(C_HomePath & "search.asp")		
End If
If Application("AppName") = "StoreFrontAE" Then 'SFAE
	Confirm_CheckCartAndRedirect 
End IF

Dim sSql,sEmail,iOrderID,rsMyOrders, sPassword, iAuthenticate, bLoggedIn, sCondition, sPaymentMethod, sPaymentList
Dim sProdID,iQuantity,iShip,sTotalPrice,strProdID,intQuantity,rstAdmin,sTotalCost,blnFree, iFreeShip
' initially false
bLoggedIn = false
iShip = 0
sSQL = "SELECT * FROM sfTmpOrderDetails WHERE odrdttmpShipping >= 1 AND odrdttmpSessionID = " & Session("SessionID")
Set rsMyOrders = Server.CreateObject("ADODB.RecordSet")
Set rstAdmin = Server.CreateObject("ADODB.RecordSet")
rsMyOrders.Open sSQL, cnn, adOpenForwardOnly, adLockReadOnly, adCmdText
rstAdmin.Open "sfAdmin", cnn, adOpenForwardOnly, adLockReadOnly, adCmdTable


If Request.QueryString("Persist") = 1 then '#245
   sTotalPrice= Session("persistTotalPrice")
else
  sTotalPrice =Request.Form("TotalPrice")
end if  

Session("persistTotalPrice") = sTotalPrice
If NOT rsMyOrders.EOF = True AND NOT rsMyOrders.EOF = true Then
rsMyOrders.MoveFirst 
iShip = 0
Do While NOT rsMyOrders.EOF
 iShip = iShip + rsMyOrders.Fields("odrdttmpShipping")
  rsMyOrders.MoveNext 
loop
End If

blnFree =False
if iShip <> 0 then
iFreeShip = rstAdmin.Fields("adminFreeShippingIsActive") 
  if (cdbl(sTotalPrice) > cdbl(rstAdmin.Fields("adminFreeShippingAmount")) AND iFreeShip = "1")  then
    blnFree =True
  end if
elseif iShip = 0 then
   blnFree =True
     else
  blnFree =False
     end if  
rsMyOrders.Close 
rstAdmin.Close 
 set rstAdmin =nothing
 set rsMyOrders = nothing    

'-------------------------------------------------------
' See if session is repeating, if so, give new id to use
'-------------------------------------------------------
If Session("SessionID") = Request.Cookies("EndSession") Then	
	bLoggedIn = false
End If	

'-------------------------------------------------------
' Check if custID exists 
'-------------------------------------------------------
iCustID = Session("custID")
If iCustID <> "" Then
	 Dim bCustIdExists
	   	bCustIdExists = CheckCustomerExists(iCustID)
    	If bCustIdExists = false Then
    		Response.Cookies("sfCustomer")("custID") = ""
	   		Response.Cookies("sfCustomer").Expires = NOW()
	   	Else
			Response.Cookies("sfCustomer")("custID") = iCustID
			Response.Cookies("sfCustomer").Expires = Date() + 730
		End If
End If	
	
If Request.Cookies(Session("GeneratedKey") & "sfOrder")("SessionID") = Session("SessionID") AND Request.Cookies(Session("GeneratedKey") & "sfOrder")("SessionID") <> ""  AND bCustIdExists Then
	bLoggedIn = true
End If

' Get Payment List
	sPaymentList = getPaymentMethods()

'-------------------------------------------------------
' If login button is depressed
'-------------------------------------------------------
If Trim(Request.Form("btnLogin.x")) <> "" Then
	
	sEmail			= Trim(Request.Form("Email"))
	sPassword		= Trim(Request.Form("Passwd"))
	
	' Authenticate
	iAuthenticate	= customerAuth(sEmail,sPassword,"loose")
		
	If iAuthenticate > 0 Then
		If Request.Cookies("sfCustomer")("custID") <> "" AND iAuthenticate <> Request.Cookies("sfCustomer")("custID")  Then
			Dim bSvdCartCust
			bSvdCartCust = CheckSavedCartCustomer(Request.Cookies("sfCustomer")("custID"))
			'Response.write "Saved Cart Cust?" & Request.Cookies("sfCustomer")("custID") & "False?" & bSvdCartCust 
			If bSvdCartCust Then
				' Delete SvdCartCustomer Row
				Call DeleteCustRow(Request.Cookies("sfCustomer")("custID"))
				' See if saved cart has any remaining saved
				Call setUpdateSavedCartCustID(iAuthenticate,Request.Cookies("sfCustomer")("custID"))
			End If
		End If	
		Response.Cookies(Session("GeneratedKey") & "sfOrder")("SessionID") = Session("SessionID")
		Response.Cookies(Session("GeneratedKey") & "sfOrder").Expires = Date() + 1
		Response.Cookies("sfCustomer")("custID") = iAuthenticate
		Response.Cookies("sfCustomer").Expires = Date() + 730
		Session("custID") = iAuthenticate
		bLoggedIn = true
		iCustID = iAuthenticate
	Else 	
		If customerAuth(sEmail,sPassword,"loosest") > 0 Then
			sCondition = "EmailMatch"   
			Response.Cookies("sfCustomer").Expires = Now()
		Else 
			sCondition = "WrongCombination"
			Response.Cookies("sfCustomer").Expires = Now()		
		End If			
	End If			
End If		
%>

<html>
<head>
	<script language="javascript">
function clearShipping(form)
 {
for (var i=0; i < form.length; i++) 
   {
	 e = form.elements[i];
	 if (e.name.indexOf("Ship") == 0) 
	 {
			e.value = "";
	 }
   } 
}
function validate_Me(frm)
{
 var bmain_is_good = sfCheck(frm);
 if(bmain_is_good == true)
   {
    var bshipping_is_good = sfCheckPlus(frm);
    return bshipping_is_good;
   }
     
 else
  {
  return false;
  } 
}
</script>
<SCRIPT language="javascript" src="SFLib/sfCheckErrors.js"></SCRIPT>


<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title><%= C_STORENAME %>-SF Information Gathering Page for Check Out/Second Step in Checkout</title>


<!--Header Begin -->
<link rel="stylesheet" href="sfCSS.css" type="text/css">
</head>


<body  bgproperties="fixed"  link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<table border="0" cellpadding="1" cellspacing="0" class="tdbackgrnd" width="<%= C_WIDTH %>" align="center">
  <tr>
    <td>
      <table width="100%" border="0" cellspacing="1" cellpadding="2">
        <tr>
          <td align="middle"  class="tdTopBanner"><%If C_BNRBKGRND = "" Then%><%= C_STORENAME %><%Else%><img src="<%= C_BNRBKGRND %>" border="0"><%End If%></td>
        </tr>
<!--Header End -->
        <tr>
          <td align="center" class="tdMiddleTopBanner">Customer Information</td>
        </tr>
        <tr>
          <td class="tdBottomTopBanner" align="left">
This is the final step in completing your order. You are now connected using a secure (SSL) connection and all information is transmitted in an encrypted form. 
You should see a small key (Netscape) or lock (IE) indicating that your browser is communicating securely with our web store.
          </td>    
        </tr>	
        <tr>
          <td width="100%" align="center" class="tdContent2"  valign="center"><b>Step 1: Customer Information</b> | Step 2: Payment Information | Step 3: Complete Order</td>
        </tr>
        <tr valign="center">
          <td width="100%" class="tdContent2" align="center" valign="center"> 	
            <% If Not bLoggedIn Then %>   

			   <br>
		       <form action="process_order.asp" method="post" name="frmEmail"> 
<input type="hidden" name="FreeShip" value="<%= blnFree %>">
		        <input type="hidden" name="FreeShip" value="<%= blnFree %>">
		        <table border="0" width="100%" cellpadding="5" cellspacing="1">
		          <tr>
		            <td width="50%" align="center" class="tdContent2" valign="center">
					  <table border="0" class="tdBottomTopBanner2" width="100%" cellpadding="3" cellspacing="1">
						<tr>
						  <td width="100%" align="center" class="tdBottomTopBanner">Returning Customer Login</td>		        
						</tr>
						<tr>
						  <td width="100%" align="center" valign="center" class="tdContent2">
					        <table border="0" width="100%" cellpadding="2">
							  <tr>
							    <td width="15%" align="right"><b> E-Mail:</b></td>
							    <td width="85%"><input type="text" size="35" name="Email"  title="E-Mail Address" style="<%= C_FORMDESIGN %>"></td>
							  </tr>
							  <tr>
							    <td width="15%" align="right"><b>Password:</b></td>
							    <td width="85%"><input type="password" size="35" name="Passwd" title="Password" style="<%= C_FORMDESIGN %>"></td>
							  </tr>
							  <tr>
							    <td width="100%" align="middle" colspan="2">
							      <input Type="image" src="<%= C_BTN16 %>" name="btnLogin" border="0">
							    </td>
							  </tr>
					        </table>					    
						  </td>
						</tr>
						<tr>
						  <td width="100%" align="center" class="tdContent2"><a href="password.asp?status=fpwd">Forgot your password?</a> <br> <a href="password.asp?status=change">Change Login/Password</a></td>		        
						</tr>
					  </table>
				    </td>
				    <td width="50%" class="tdContent2">
				      <center>
				      <font class="Error"><b>
				      <% If sCondition = "EmailMatch" or sCondition = "WrongCombination" Then %>	
				      <font color="red">Login Failed</font>
				      <% Else %>
						Login Directions
				      <% End If %>
				      </b></font>	
				      <hr width="90%" noshade size="1">
				      </center>
					  <% If sCondition = "EmailMatch" Then %>	
						Your combination was wrong, but an e-mail match was found. Please login with the correct password or if you wish to open a new account, you must choose a new password.
					  <% ElseIf sCondition = "WrongCombination" Then %> 		
						Your e-mail and password combination is incorrect. Try again.
					  <% Else %>	
						Please use your e-mail address and password to log in and retrieve your customer information.
					  <% End If %>					
		            </td>
		          </tr>
		        </table>	
		        <input type="hidden" name="TotalPrice" value="<%= sTotalPrice %>">
		        <input type="hidden" name="FreeShip" value="<%= blnFree %>">
		        <input type="hidden" name="bShip" value="<%= iShip %>">
		        </form>		
	        <% End If %>		
	        <!--#include file="orderform.asp"-->	  

           <!--Footer begin-->
                <!--#include file="footer.txt"-->
              </table>
            </td>
          </tr>
        </table>
       </body>

    </html>
<!--Footer End-->












