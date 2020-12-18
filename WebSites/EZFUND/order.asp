<%@ Language=VBScript %>
<%	option explicit 
   Response.Buffer=True       
   Const vDebug = 0
%>
<!--#include file="SFLib/db.conn.open.asp"-->
<!--#include file="SFLib/adovbs.inc"-->
<!--#include file="SFLib/incOrder.asp"-->
<!--#include file="error_trap.asp"-->
<!--#include file="sfLib/incDesign.asp"-->
<!--#include file="SFLib/incGeneral.asp"-->
<!--#include file="SFLIB/incAE.asp"-->

<%
'@BEGINVERSIONINFO


'@APPVERSION: 50.4014.0.6

'@FILENAME: order.asp
 



'@DESCRIPTION: Cart Page

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
'Modified 12/4/01 
'Storefront Ref#'s: 241 djp
If Session("SessionID") = "" Then 'SFUPDATE
		' redirect to neworder screen
		Session.Abandon
		'Response.Redirect(C_HomePath & "search.asp")
		Response.Redirect("search.asp")								
End If
Order_InitializeDiscounts


If iConverion = 1 Then Response.Write "<script language=""JavaScript"" src=""http://www.oanda.com/cgi-bin/fxcommerce/fxcommerce.js?user=" & sUserName & """></script>"
Dim CurrencyISO,rstAdmin,bActiveCart
set rstadmin = server.CreateObject ("ADODB.Recordset")
rstAdmin.Open "Select adminSaveCartActive from sfAdmin",cnn,adOpenStatic ,adLockReadOnly ,adCMDText
bActiveCart = rstAdmin("adminSaveCartActive")
rstAdmin.Close 
set rstAdmin = nothing
'Response.Write bactiveCart
'Response.End 
 
CurrencyISO = getCurrencyISO(Session.LCID )
%>


<html>
<head>
<SCRIPT language="javascript" src="SFLib/incae.js"></SCRIPT>
<%
Order_ShowInventoryMessage 'SFAE
%>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title><%= C_STORENAME %>-SF Shopping Cart Page/Begin Checkout Page</title>



<!--Header Begin -->
<link rel="stylesheet" href="sfCSS.css" type="text/css">

<SCRIPT LANGUAGE=javascript id="recalc">
<!--
function reCalc()
 {
 //#400 
 //#531 
  var doCalc = document.frmQty.recalc.value;
  if(doCalc != "no")
    {
     document.frmQty.recalc.value ="1";
    }
 }   
//-->
</SCRIPT>




</head>
<body  bgproperties="fixed"  link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<table border="0" cellpadding="1" cellspacing="0" class="tdbackgrnd" width="<%= C_WIDTH %>" align="center">
  <tr>
    <td>
      <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
          <td align="middle"  class="tdTopBanner"><%If C_BNRBKGRND = "" Then%><%= C_STORENAME %><%Else%><img src="<%= C_BNRBKGRND %>" border="0"><%End If%></td>
        </tr>
<!--Header End -->  
        <tr>
          <td align="center" class="tdMiddleTopBanner">Order
            Summary</td>
        </tr>
        <tr>
          <td class="tdBottomTopBanner">Please review your order as shown below. To modify the quantity of any item ordered,
            input the desired quantity and select the <b>Recalculate Order</b>
            button below. To delete an item click on <b>DELETE</b>.<%if bactiveCart = 1 then
          %> To save
      an item to return and purchase at a later time click on <b>Wish List</b>.
      If you want to add new items return to the correct page and select additional items to be added to your order. When you have
        completed your order, select the Check Out button below to connect to our secure directory and complete the order process.
        <%
        end if
        
        %>
        </tr>
       
         <tr>
          <td class="tdContent2">
         
	                <form method="POST" name="frmQty" id="frmQty" action="order.asp" onSubmit="reCalc();" >
            <table border="0" width="100%" cellspacing="0" cellpadding="4">
              <tr>
                <td width="40%" class="tdContentBar">product</td>
                <td width="15%" align="center" class="tdContentBar">unit price</td>
                <td width="15%" align="center" class="tdContentBar">qty</td>
                <td width="15%" align="center" class="tdContentBar">price</td>
                <td width="15%" align="center" class="tdContentBar">action</td>
                </tr>
              <%
'@BEGINCODE
Dim sSql, rsAllOrders, sProdID, aProduct, aProdAttr, sProdName, sProdPrice, iProdAttrNum, iCounter 
Dim sAttrUnitPrice, sUnitPrice, iQuantity, iNewQuantity, sProductSubtotal, dProductSubtotal, sTotalPrice, iOrderID, aProdAttrID,  sProductPrice
Dim iProductCounter, sBgColor, sFontFace, sFontColor, iFontSize
Dim bHasProducts, sBtnAction, sSaveCart, sDelete, iSvdCartID,iCustID, sRecalculate, iSaveFind, iDeleteFind
Dim sErrorDescription, sSearchPath, sBkGrnd, sReferer
Dim dUnitPrice

	' Determine action and OrderID
	For iCounter = 1 to Request.Form("iProductCounter")
		sSaveCart = Request.Form("SaveToCart" & iCounter & ".x")
			If sSaveCart <> "" Then  
				iSaveFind = iCounter
				sBtnAction = "SaveToCart"				
				Exit For
			End If	
		
		sDelete	= Request.Form("DeleteFromOrder" & iCounter & ".x")		
			If sDelete <> "" Then
				iDeleteFind = iCounter
				sBtnAction = "DeleteFromCart"				
				Exit For
			End If	
	Next 
	
	' Check to see if custID exists in customer table
	  iCustID = Request.Cookies("sfCustomer")("custID")
	  If iCustID <> "" Then
	   	Dim bCustIdExists
	    	bCustIdExists = CheckCustomerExists(iCustID)
	    	If bCustIdExists = false Then
	    		Response.Cookies("sfCustomer")("custID") = ""
	    		Response.Cookies("sfCustomer").Expires = NOW()
	    	End If			    		    	
	    End If	

	' Determine if it is recalculate action
	sRecalculate  = Request.Form("recalc") 
	If sRecalculate = "1" AND lcase(sBtnAction) <> "savetocart" Then '#530
		sBtnAction = "Recalculate"	
	End If	 	

	' Get referer
	sReferer = Session("HttpReferer")

	' Recalculate subtotal
	If sBtnAction = "Recalculate" Then
    
	
	
	
			Dim iTmpOrderID, iOldQuantity 		
			For iCounter = 1 To Request.Form("iProductCounter") 			
				iNewQuantity = Request.Form("FormQuantity" & iCounter)
				iOldQuantity = Request.Form("iQuantity" & iCounter)
				iTmpOrderID = Request.Form("iOrderID" & iCounter)
			   if not isnumeric(iNewQuantity) or trim(iNewQuantity) ="" then
	               iNewQuantity = iOldQuantity
	           end if  
				Order_Update_GiftWrapsBackOrder 'SFAE b2
				
				If iNewQuantity <> "" Then 				
					If iNewQuantity = 0 Then
						' Delete if 0
						Call setDeleteOrder("odrdttmp",iTmpOrderID)
						DeleteTmpOrderDetailsAE iTmpOrderID 'SFAE 
						
					ElseIf iNewQuantity <> iOldQuantity Then						
						' Update Quantity For Product
						Call setReplaceQuantity("odrdttmp",iNewQuantity,iTmpOrderID)					
					End If
				Else
					' Delete if Null Value
					Call setDeleteOrder("odrdttmp",iTmpOrderID)
					DeleteTmpOrderDetailsAE iTmpOrderID 'SFAE 
				End If	
			Next	
			Reset_Shipping '# 425
			Order_AdjustCart 'SFAE b2
			
	' Save to Cart
	ElseIf sBtnAction = "SaveToCart" Then	
			sProdID = Request.Form("sProdID" & iSaveFind)
			iOrderID = Request.Form("iOrderID" & iSaveFind)
			iQuantity = Request.Form("iQuantity" & iSaveFind)	  
			iProdAttrNum = Request.Form("iProdAttrNum" & iSaveFind)
			iCustID = Request.Cookies("sfCustomer")("custID")
	  		iNewQuantity = Request.Form("FormQuantity" & iSaveFind)
	  		
	  		' In the case that one types in a new quantity and hits save 
	  		If iNewQuantity <> iQuantity And iNewQuantity <> "" And iNewQuantity <> 0 Then
	  			iQuantity = iNewQuantity
	  		End If	
	  		
	  		If iProdAttrNum > 0 Then
			   Redim aProdAttr(iProdAttrNum)
			   aProdAttr = getProdAttr("odrattrtmp",iOrderID,iProdAttrNum)  				   
			End If		
	
		' Check if cookies are set		
		If Request.Cookies("sfCustomer")("custID") = "" OR Request.Cookies("sfOrder")("SessionID") <> Session("SessionID") Then
			' Write to cookie identifying place
			  Call getSavedTable(aProdAttr,sProdID,iNewQuantity,0,sReferer)
			  Response.Cookies("sfThanks")("PreviousAction") = "FromShopCart"		
			  Response.Cookies("sfThanks")("DeleteTmpOrderID") = iOrderID
			  Response.Cookies("sfThanks").Expires = Date() + 1					  	  			 
			  Response.Redirect("login.asp")
		End If	
	  		  
		If iProdAttrNum > 0 Then
		   Redim aProdAttr(iProdAttrNum)
		   aProdAttr = getProdAttr("odrattrtmp",iOrderID,iProdAttrNum)  			   
		End If		
	  		  	  
		iSvdCartID = getOrderID("odrdtsvd","odrattrsvd", sProdID,aProdAttr,cInt(iProdAttrNum))					

		If iSvdCartID <> "" Then
				' New Row in SavedCartDetails
				If iSvdCartID < 0 Then								
						' Write as new row
		  				Call getSavedTable(aProdAttr,sProdID,iQuantity,iCustID,Session("HttpReferer"))
					
				' Existing cart
				Else						
					' Update Quantity
					Call setUpdateQuantity("odrdtsvd",iQuantity,iSvdCartID)
					' End iSvdCartID exists If					
				End If		
		Else
				'sErrorDescription =  "Number of attributes not equal to the product specs or database writing error."			
				'Response.Redirect("error.asp?strPageName=order.asp&strErrorDescription="&sErrorDescription)
		' End iSvdCartID Null If
		End If	
		
		' delete from sfTmpOrderDetails
		Call setDeleteOrder("odrdttmp",iOrderID)
	
	ElseIf sBtnAction = "DeleteFromCart" Then
			' Remove from cart
			iOrderID = Request.Form("iOrderID" & iDeleteFind)	
			Call setDeleteOrder("odrdttmp",iOrderID)
			DeleteTmpOrderDetailsAE iOrderID 'SFAE
			Call Reset_Shipping 
	End If

	' Product counter initialize
	iProductCounter = 0	
	sTotalPrice = 0 
	
	'-----------------------------------------------------------------
	' Collect all orders associated with Session ::: Begin
	'-----------------------------------------------------------------
		' Get a RecordSet of all orders
		sSql = "SELECT * FROM sfTmpOrderDetails WHERE odrdttmpSessionID = " & Session("SessionID")
		If vDebug = 1 Then Response.Write "<br> " & sSql
		Set rsAllOrders = cnn.execute(sSql)
		

		' Check for no orders
		If (rsAllOrders.BOF And rsAllOrders.EOF) Then
			bHasProducts = False	
			%>
	            <tr>
	              <td colspan="5" width="40%" class="tdAltFont1"><font class='TopBanner_Large'>
	                <p style="margin-top:25pt">
	                <center><b>No Items in Order</b>
	                <br>
	                Please press continue to begin searching for items.
	                </center> 
	                </font>
								</td>			
	            </tr>					
			      <%
		Else
			bHasProducts = True
			
			Do While NOT rsAllOrders.EOF
			' Get the ProdIDs
			iOrderID = rsAllOrders.Fields("odrdttmpID")
			sProdID = rsAllOrders.Fields("odrdttmpProductID")
			iQuantity = rsAllOrders.Fields("odrdttmpQuantity")
	    
	    	' Get an array of 3 values from getProduct()
		    '++ On Error Resume Next
			ReDim aProduct(3)
				aProduct = getProduct(sProdID)		
	  			sProdName = aProduct(0)
	  			sProdPrice = aProduct(1)
	  			iProdAttrNum = aProduct(2)	  			
	  			
			' ++ Call CheckForError()
				
				'Order_SetProdPrice 'SFAE		
				
				' If not an array, then the product does not exist 
				If NOT IsArray(aProduct) Then
					Response.Write "<br>Product Does Not Exist"
					' ++ Needs to MoveNext to iterate through the rest of the order			
				Else
						If NOT IsNumeric(iProdAttrNum)Then 
							iProdAttrNum = 0
						End If	
						
						' Get Associated Attribute IDs in an array
						If iProdAttrNum <> "" Then							
							ReDim aProdAttrID(iProdAttrNum)
							aProdAttrID = getProdAttr("odrattrtmp",iOrderID,iProdAttrNum)	
						End If
								
			
						' Response Write all Output
						If vDebug = 1 And IsArray(aProdAttrID) Then 
							Response.Write "<p>Product = " & sProdID & "<br>ProdName = " & sProdName & "<br>ProdPrice = " & sProdPrice & "<br>ProdAttrNum = " & iProdAttrNum
						
							For iCounter = 0 To iProdAttrNum -1 
								Response.Write "<br>Attribute :" & aProdAttrID(iCounter)
							Next			
					
						End If	 
				
						iProductCounter = iProductCounter + 1
			dim fontclass
						' Do alternating colors and fonts	
						If (iProductCounter mod 2) = 1 Then 
							fontclass="tdAltFont1"
						Else 	
							fontclass="tdAltFont2"
						End If	
		
				%>
	              <tr>
	                <td width="40%" valign="top" class="<%=fontClass%>" nowrap background=""><b><%= sProdName %></b><br>
	                  <%
						sAttrUnitPrice = 0
						
						' Iterate Through Attributes
						If iProdAttrNum > 0 And IsArray(aProdAttrID) Then
							Dim sAttrSubtotal, aAttrDetails, sAttrName, sAttrPrice, iAttrType
							For iCounter = 0 To iProdAttrNum - 1 
								aAttrDetails = getAttrDetails(aProdAttrID(iCounter))												
								sAttrName = aAttrDetails(0)
								sAttrPrice = aAttrDetails(1)
								iAttrType = aAttrDetails(2)
							
								' Calculate Subtotal
								sAttrUnitPrice =  getAttrUnitPrice(sAttrUnitPrice,sAttrPrice,iAttrType)								
					%>										                
	                &nbsp;&nbsp;<%=sAttrName%>
	                  <br>                									
					  <%		
							' ProdAttr Loop
							Next
						Elseif iProdAttrNum > 0 And NOT IsArray(aProdAttrID) Then 
							Response.Write "<br>Error: No Attributes found for " & iOrderID
							Response.Write "<br> Deleting from Saved Orders. Sorry for the inconvenience."
													
							Call setDeleteOrder("odrdttmp",iOrderID)
							DeleteTmpOrderDetailsAE iOrderID 'SFAE
							
							If vDebug = 1 Then Response.Write "<p><font color=""red""> Deleted: " & iOrderID & "</font>"						
						' End Product Attribute If
						End If	
						
						dUnitPrice = cdbl(cDbl(sAttrUnitPrice) + cDbl(sProdPrice))
						Order_SetProdPrice 'SFAE
						
						' Set Unit Price for Product
						If iConverion = 1 Then
							sUnitPrice = "<script> document.write(""" & FormatCurrency(dUnitPrice) & " = ("" + OANDAconvert(" & dUnitPrice & ", " & chr(34) & CurrencyISO & chr(34) & ") + "")"");</script>" 
						Else
							sUnitPrice = FormatCurrency(dUnitPrice)
						End If
						dProductSubtotal = iQuantity * (dUnitPrice)
						If iConverion = 1 Then
							sProductSubtotal = "<script>document.write(""" & FormatCurrency(dProductSubtotal) & " = ("" + OANDAconvert(" & dProductSubtotal & ", " & chr(34) & CurrencyISO & chr(34) & ") + "")"");</script>"  
									Else
							sProductSubtotal = FormatCurrency(dProductSubtotal)
						End If
						'sTotalPrice = sTotalPrice + cDbl(dProductSubtotal) 'SFUPDATE
					%>
	                </td>
     						  <td width="15%" align="center" class='<%=fontClass%>' valign="top" nowrap background=""><%= sUnitPrice %></td>
									<td width="15%" align="center" class='<%=fontClass%>' valign="top" nowrap background=""><input type="text" style="<%= C_FORMDESIGN %>" size="2" name="FormQuantity<%= iProductCounter%>" value="<%= iQuantity %>"></td>
									<td width="15%" align="center" class='<%=fontClass%>' valign="top" nowrap background=""><%= sProductSubtotal %></td>
									<td width="15%" align="center" class='<%=fontClass%>' valign="top" background="">
									<input type="hidden" name="sProdID<%= iProductCounter%>" value="<%=sProdID%>">
									<input type="hidden" name="iOrderID<%= iProductCounter%>" value="<%=iOrderID%>">
									<input type="hidden" name="iQuantity<%= iProductCounter%>" value="<%=iQuantity%>">
									<input type="hidden" name="iProdAttrNum<%= iProductCounter%>" value="<%=iProdAttrNum%>">
									<input type="image" src="<%= C_BTN06 %>" border="0" onmousedown="recalc.value='no';" name="DeleteFromOrder<%= iProductCounter%>"><br>
					    <%
					    if bActiveCart = 1 then
					    %><input type="image" border="0" src="<%= C_BTN07 %>" name="SaveToCart<%= iProductCounter%>">
					    <%
					    end if
					    %>
								  </td>	  
	              </tr> 
					<%OVC_ShowGiftWrapValue 1   'SFAE%>
					<%OVC_ShowBackOrderMessage 1'SFAE %>        
              <%
				' End IsArray If
				End If
				
				sTotalPrice = cdbl(sTotalPrice) + cDbl(dProductSubtotal) 'SFUPDATE
				
			' Move to next RecordSet
			rsAllOrders.MoveNext		
		' loop through recordset	
		
		Loop
	
	'@ENDCODE

	'-----------------------------------------------------------
	' END PRODUCT DETAIL OUTPUT --------------------------------
	'-----------------------------------------------------------
				
				
	'Order_FixTable 'SFAE b2 %>          
			          <tr>
	                  <td width="40%"></td>
	                  <td width="15%" align="center" valign="top"></td>
	                  <td nowrap colspan="2" width="30%" align="right" valign="top"><font class="Error"><%= getShippingSaleText(0) %></font>
	                  <br><br><font class="Error"><%= getGlobalSaleText %></font></td>
	                  <td width="15%" align="center" valign="top">
	                    <input type="hidden" name="iProductCounter" value="<%= iProductCounter%>">
	           	        <input type="hidden" name="recalc" value="">
	           	        
	           	            <input type="image" value="" src="<%= C_BTN14 %>" border="0" id="Recalculate" name="Recalculate" onclick="javascript:recalc.value='';" > 
	                    <p style="line-height:8pt;margin-top:1pt;">Recalculate Order
		                </td>
	               </tr>
	           </table>	            
	                
	          </form>       
	              <%Order_ShowCouponLink'SFAE%>
	              <%OVC_SaveSubTotalWOD 'SFAE%>
	              
	              <%If Application("AppName") = "StoreFront" Then 'SFUPDATE
						sTotalPrice = getGlobalSalePrice(cdbl(sTotalPrice))
					End If
							
					If Application("AppName") = "StoreFrontAE" Then 'SFAE
						sTotalPrice = ApplyALLDiscounts(cdbl(sTotalPrice),"Total") '.3008
					End If 
					
	'----------------------------------------------------------- 
	' SUBTOTAL OUTPUT
	'-----------------------------------------------------------
	%>			  
	              <table border="0" width="100%" cellspacing="0" cellpadding="2">
	              <%OVC_ShowOrderDiscounts'SFAE%>
 									<tr>
	                  <td width="75%" align="right"><b>Sub Total:</b></td>
	                    <td nowrap align="left"  width="25%" height="20"><b>
	                      <%                       
	                     
	                      If iConverion = 1 Then 
						        dim stemp
							  	stemp = "<script> document.write(""" & FormatCurrency(sTotalPrice) & " = ("" + OANDAconvert(" & sTotalPrice & ", " & chr(34) & CurrencyISO & chr(34) & ") + "")"");</script>" 
	                      		dblTotal = FormatCurrency(sTotalPrice)
	                      		Response.Write stemp
				           Else				  
				 				If sTotalPrice < 0 Then 
									Response.Write "Error: Your SubTotal Was a Negative Amount" 
									Response.End 
								Else
								dim dblTotal
								dblTotal = FormatCurrency(sTotalPrice)
								Response.write FormatCurrency(sTotalPrice)
								End If 
					    End If
	            %></b>&nbsp;&nbsp;&nbsp;</td>
                        <td width="5%" align="center"></td>
                      </tr>
	                  <%
	 ' End rsAllOrders If
	End If
	%>               
                      <tr>
	                    <td width="100%" colspan="5" align="center">
	                      <hr noshade color="#000000" size="1" width="90%">
	                    </td>
                      </tr>
                      <tr>
                        <td width="100%" colspan="5" align="center">
                          <form action="<%= C_SecurePath %>" method="post">
                           	<input type="hidden" name="REFERER" value="<%= Trim(Request.Cookies("sfHTTP_REFERER")("REFERER"))%>">
		                    		<input type="hidden" name="HTTP_REFERER" value="<%= Trim(Request.Cookies("sfHTTP_REFERER")("HTTP_REFERER"))%>">
		                    		<input type="hidden" name="REMOTE_ADDRESS" value="<%=	Trim(Request.Cookies("sfHTTP_REFERER")("REMOTE_ADDRESS"))%>">
                           	<input type="hidden" name="CustID" value="<%= Trim(Request.Cookies("sfCustomer")("custID")) %>">
                           	<input type="hidden" name="SessionID" value="<%= Session("SessionID") %>">
                           	<input type="hidden" name="LoggedSessionID" value="<%= Trim(Request.Cookies("sfOrder")("SessionID"))%>">
                           	<input type="hidden" name="TotalPrice" value="<%= dblTotal%>">
                            <% If iSaveCartActive = 1 Then 
																if Application("AppName")="StoreFrontAE" then%>
                            <a href="savecart.asp"><img src="<%= C_BTN08 %>" alt="View Wish List" border="0"></a>
																<% else %>
                            <a href="savecart.asp"><img src="<%= C_BTN08 %>" alt="View Saved Cart" border="0"></a>
																<% End If %>
                            <% End If %>
                            
                            <%
          						' See if there is any path to return to 
								If Request.Cookies("sfSearch")("SearchPath") <> "" Then
									sSearchPath = Request.Cookies("sfSearch")("SearchPath")
									If InStr(LCase(sSearchPath), "login.asp") <> 0 Then
										sSearchPath = "search.asp"
									End If 
								Else
									sSearchPath = "search.asp"
								End If        
            					%>
            
                            <%If bHasProducts Then %>                      
                            <input type="image" src="<%= C_BTN05 %>" border="0" name="checkout">    
                          </form>
                          <form action="<%= sSearchPath %>" Method="post" id="form1" name="form1">
			                <input type="image" src="<%= C_BTN04 %>" alt="Continue Shopping" border="0" name="continue_search" value="Continue Search">
			              </form>            
                          <font class="Content_Small">Please Note: <b>CHECKOUT</b> will go to our secure server to finish your transaction.</font>
            
                        <% Else %>                                   
                        <a href="<%= sSearchPath%>"><img src="<%= C_BTN04 %>" border="0" name="continue_search" alt="Continue Search" ></a>
                          <% End If %>
												</td>
                    </tr>
                    </table>
<!--Footer begin-->
                    <!--#include file="footer.txt"-->
                    </table>
                   </td>
                  </tr>
		   </table>
        </body>
      </html>
<!--Footer End-->
      <%
closeObj(rsAllOrders)
closeObj(cnn)
%>
