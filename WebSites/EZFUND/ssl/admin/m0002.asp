<%	
	option explicit 
	Response.Buffer = True
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0002.asp
	
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
	
'--------------------------------------------------------------------
' Function : getCountryList
' This returns the country list in HTML format for dropdown box.
' This list will not return Catagories which have no products associated with them.
' Recommend using incStateCtry unless generating the list
'--------------------------------------------------------------------	
Function getCountryList(sCountryAbbr)	
	Dim rsCountryList, sCtryList, iCounter, sLocalSQL
	
	sLocalSQL = "SELECT loclctryAbbreviation, loclctryName FROM sfLocalesCountry ORDER BY loclctryName"
	
	Set rsCountryList = Server.CreateObject("ADODB.RecordSet")
	rsCountryList.Open sLocalSQL, cnn, adOpenKeyset, adLockOptimistic, adCmdText
	
	sCtryList = ""
	For iCounter = 1 to rsCountryList.RecordCount
		If sCountryAbbr <> "" AND sCountryAbbr = Trim(rsCountryList.Fields("loclctryAbbreviation")) Then
			sCtryList = sCtryList & "<option selected value=""" & Trim(rsCountryList.Fields("loclctryAbbreviation"))& """>" & Trim(rsCountryList.Fields("loclctryName")) & "</option>"
		Else
			sCtryList = sCtryList & "<option value=""" & Trim(rsCountryList.Fields("loclctryAbbreviation"))& """>" & Trim(rsCountryList.Fields("loclctryName")) & "</option>"
		End If
		rsCountryList.MoveNext
	Next		
	
	getCountryList = sCtryList
	closeObj(rsCountryList)
End Function
%>
<script language="javascript">
function availableShipping() {
	if ((shippingForm.R1[0].checked)||(shippingForm.R1[2].checked)) {
		if (shippingForm.R1[0].checked) {
			for (i=3; i<=21; i++) {
				shippingForm.elements[i].disabled = true;
			}
			//shippingForm.valueBasedInput.value = "";
			for (i=0;i<shippingForm.length;i++) {
				if (shippingForm.elements[i].name.indexOf("T") == 0) {
					shippingForm.elements[i].disabled = false;
				}
			}
			
		}
		if (shippingForm.R1[2].checked) {
			for (i=3; i<=24; i++) {
				shippingForm.elements[i].disabled = true;
			}
			//shippingForm.valueBasedInput.disabled = true;
			for (i=0;i<shippingForm.length;i++) {
				if (shippingForm.elements[i].name.indexOf("T") == 0) {
					shippingForm.elements[i].disabled = true;
				}
			}
		}
	}
	else {
		for (i=3; i<=24; i++) {
				shippingForm.elements[i].disabled = false;
			}
		if (shippingForm.shiptype2[1].checked) {
		    //shippingForm.valueBasedInput.show = false;
			for (i=0;i<shippingForm.length;i++) {
				if (shippingForm.elements[i].name.indexOf("T") == 0) {
					shippingForm.elements[i].disabled = true;
				}
			}
		}
		else if (shippingForm.shiptype2[0].checked) {
			//shippingForm.valueBasedInput.show = true;
			for (i=0;i<shippingForm.length;i++) {
				if (shippingForm.elements[i].name.indexOf("T") == 0) {
					shippingForm.elements[i].disabled = false;
				}
			}		
		}
	}
}
function premiumShip() {
	if (shippingForm.iPremiumIsActive[1].checked)	 {
		shippingForm.PremiumShipCharge.disabled = true;
	}
	else {
		shippingForm.PremiumShipCharge.disabled = false;
	}
}	
function handling() {
	if (shippingForm.ApplyHandling[1].checked) {
		shippingForm.HandlingType[0].disabled = true;
		shippingForm.HandlingType[1].disabled = true;
		shippingForm.HandlingCharge.disabled = true;
	}
	else {
		shippingForm.HandlingType[0].disabled = false;
		shippingForm.HandlingType[1].disabled = false;
		shippingForm.HandlingCharge.disabled = false;
	}
}
</script>
<html>

<head>
<title>SF Menu Page</title>
</head>

<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<!--#include file="../SFLib/ADOVBS.inc"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="incAdmin.asp"-->
<script language="javascript" src="../../SFLib/sfCheckErrors.js"></script>

<%
	' Variable Declarations
	Dim objRS, iCounter, rsAdmin, aAdmin, sOriginZip, sOriginCountry, iShipType, iShipType2, iPrmShipIsActive
	Dim iHandling, iShipTaxIsActive, iHandlingType, sHandling, sShipMin, sSpcShipAmt, rsShipMethod, rsValueShip, iValueShipNumbers
	Dim sNewTotal, sNewShip, sSubmitAction, sShipID, iShippingMethod, tmpShipID, iUpperBound, iShipTax, iPremShip, sPremShipCharge, sMinShipCharge
	Dim iHandlingActive, iPurchaseVal, iShipVal,sUspsPassword,sUspsUserName
	
	Set objRS = getList(1,"sfShipping", "shipMethod") 
	Set rsAdmin = Server.CreateObject("ADODB.RecordSet")
	
	If Request.Form("deleteValShipImg.x") <> "" Then
		Call setDeleteValShip(Trim(Request.Form("deleteValShip")),0) 
	End If	
	
	If Request.Form("valueBasedInput.x") <> "" Then
	   If Trim(Request.Form("TNew")) <> "" And Trim(Request.Form("TVNew"))<> "" Then	
			sNewTotal = Trim(Request.Form("TNew"))
			sNewShip = Trim(Request.Form("TVNew"))
			call setNewValShip(sNewTotal,sNewShip)		   
	   End If
	End If
	
	If Request.Form("Submit.x") <> "" Then
			' Collect Shipping Method
			iShippingMethod = Trim(Request.Form("R1"))
		
			If iShippingMethod = "2" Then
				' Collect UPS material			
				tmpShipID = "ShipID1"
				iUpperBound = Request.Form("shipCount")
				
				For i = 1 to iUpperBound
					sShipID = sShipID & "," & Trim(Request.Form(tmpShipID))
					tmpShipID = "ShipID" & (i+1)
				Next
							
				If len(sShipID) > 0 Then
					Call setUpdateShipping(sShipID)
				End If	
			End If
		
			' Collect backup ship type
			iShipType2 = Trim(Request.Form("shiptype2"))
					
			' Collect Geographical Info
			sOriginCountry	= Trim(Request.Form("OriginCountry"))
			sOriginZip		= Trim(Request.Form("OriginZip"))
					
			' Collect Handling Info
			If Trim(Request.form("ApplyHandling")) = 0 Then
			   iHandlingActive	= 0
			   sHandling		= ""
			   iHandlingType	= ""
			Else		   	
			   iHandlingActive	= 1
			   iHandlingType	= Trim(Request.Form("HandlingType"))
			   sHandling		= Trim(Request.Form("HandlingCharge"))		 
			End If   	
	
			If Trim(Request.Form("R1")) = 1 Or Trim(Request.Form("shiptype2") = 1) Then
				' Collect and update val-amount
				' First clear out table
				Call setDeleteValShip("",1) 
				
				' Repopulate the table with new values
				iCounter = Request.Form("valuecounter")
				For i = 1 to iCounter
					iPurchaseVal	= Trim(Request.Form("T" & i))
					iShipVal		= Trim(Request.Form("TV" & i))
					Call setUpdateNewVal(iPurchaseVal,iShipVal)
				Next
			End If
	
			' Collect Shipping Charges Info
			iShipTax		= Trim(Request.Form("ShipTax"))
			iPremShip		= Trim(Request.Form("iPremiumIsActive"))			
			sPremShipCharge = Trim(Request.Form("PremiumShipCharge"))
			sMinShipCharge	= Trim(Request.Form("Minimum"))
			sUspsPassword = Trim(Request.Form("UspsPass"))
		   sUspsUserName = Trim(Request.Form("UspsUser"))
		
			' Update database
			Call updateAdminShip(iShippingMethod,iShipType2,sOriginCountry,sOriginZip,iShipTax,iHandlingActive,sHandling,iHandlingType,iPremShip,sPremShipCharge,sMinShipCharge,sUspsPassword,sUspsUserName)
		
	End If
	
		rsAdmin.Open "SELECT adminOriginZip,adminOriginCountry,adminShipType,adminShipType2,adminPrmShipIsActive,adminHandlingIsActive,adminTaxShipIsActive,adminHandlingType,"_
				   & "adminHandling,adminShipMin,adminSpcShipAmt, adminHandlingType,adminUsPsPassword,adminUsPsUserName FROM sfAdmin",cnn,adOpenDynamic,adLockOptimistic,adCmdText
		
		aAdmin = rsAdmin.GetRows
		sOriginZip = aAdmin(0,0)
		sOriginCountry = aAdmin(1,0)
		iShipType = aAdmin(2,0)
		iShipType2 = aAdmin(3,0)		
		iPrmShipIsActive = aAdmin(4,0)
		iHandling = aAdmin(5,0)
		iShipTaxIsActive = aAdmin(6,0)
		iHandlingType = aAdmin(7,0)
		sHandling = aAdmin(8,0)
		sShipMin = aAdmin(9,0)
		sSpcShipAmt = aAdmin(10,0)
		iHandlingType = aAdmin(11,0)
		sUspspassword = aAdmin(12,0)
		sUspsUserName = aAdmin(13,0)

		' Object cleanup
		closeobj(rsAdmin)
		
		Set rsValueShip = Server.CreateObject("ADODB.RecordSet")
			rsValueShip.Open "SELECT valShpPurTotal, valShpAmt FROM sfValueShipping",cnn,adOpenKeyset,adLockOptimistic,adCmdText		
			iValueShipNumbers = rsValueShip.RecordCount
		
		sSubmitAction = "shippingForm.TNew.optional = true;shippingForm.TVNew.optional = true;shippingForm.Minimum.optional = true;"
%>
	<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>" onLoad="availableShipping();handling();premiumShip()">
	<form method="post" name="shippingForm" onSubmit="<%= sSubmitAction %>">
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
			<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Shipping Configuration</font></b></td>        
		</tr>
		<tr>
			<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>The settings below will allow you to control how shipping costs are calculated for orders placed in your StoreFront web.  Choose from one of the three shipping methods (Value Based, Product Based, or Carrier Based) using the radio buttons under the Shipping Methods section.  All of the configuration options that are not required for the selected shipping method will then be grayed out, so simply configure your shipping using the remaining choices.  If you need additional help, please review the StoreFront help files for step-by-step instructions on configuring StoreFront&#146;s shipping methods.</font></td>
		</tr>
		<tr>
			<td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
			<table border="0" width="100%" cellpadding="4" cellspacing="0">		
			<%  If Request.Form("Submit.x") <> "" Then %>
			<tr>
			<td width="100%" colspan="4" align="center" height="90" valign="center">
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
				<td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Shipping Method</font></b></td>
			</tr>
			
			<tr>
				<td width="50%" colspan="2" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">&nbsp;<input type="radio" name="R1" value="1" <% If iShipType = 1 Then %>checked<% End If %> onClick="availableShipping()">Value Based Shipping</font></td>
				<td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Use this shipping method if you want the shipping charge based on a customers total order amount.</font></td>
			</tr>
			
		<tr>
				<td width="50%" colspan="2" valign="top">	
				<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">&nbsp;<input type="radio" <% If iShipType = 2 Then %> checked <% End If %> name="R1" value="2" onClick="availableShipping()">UPS Based Shipping</font></td>
				<td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Use this shipping method if you want the shipping charge to be based on UPS rates.</font></td>
			</tr>
			<tr>
			<td width="50%" colspan="2" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">&nbsp;<input type="radio" <% If iShipType = 3 Then %>checked<% End If %> name="R1" value="3" onClick="availableShipping()">Product Based Shipping</font></td>
			<td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Use this shipping method if you want to set the shipping charge for each product.</font></td>
			</tr>
			
			<tr>
			<td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Supported Shipping Methods</font></b></td>
			</tr>
			<tr>
			<td width="50%" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
			<%	
			Dim iRecordCount,i
			
			Set rsShipMethod = Server.CreateObject("ADODB.RecordSet")		
			rsShipMethod.Open "SELECT shipID,shipMethod,shipIsActive FROM sfShipping",cnn,adOpenKeyset,adLockOptimistic,adCmdText
			
			iRecordCount = rsShipMethod.RecordCount
			
			For i = 0 to iRecordCount / 2 
				If rsShipMethod.Fields("shipIsActive") = 1 Then%>
				<input type="checkbox" name="ShipID<%= i+1 %>" value="<%= rsShipMethod.Fields("shipID") %>" checked><%= rsShipMethod.Fields("shipMethod") %><br>
			<%	
				Else
			%>
				<input type="checkbox" name="ShipID<%= i+1 %>" value="<%= rsShipMethod.Fields("shipID") %>"><%= rsShipMethod.Fields("shipMethod") %><br>
			<%	
				End If 	
				rsShipMethod.MoveNext
			Next 
			%>
		        </td>
		        <td width="50%" colspan="2" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
			<%	
			Do While NOT rsShipMethod.EOF 
				If rsShipMethod.Fields("shipIsActive") = 1 Then%>
				<input type="checkbox" name="ShipID<%= i %>" value="<%= rsShipMethod.Fields("shipID")%>" checked><%= rsShipMethod.Fields("shipMethod") %><br>
			<%	
				Else
			%>
				<input type="checkbox" name="ShipID<%= i %>" value="<%= rsShipMethod.Fields("shipID")%>"><%= rsShipMethod.Fields("shipMethod") %><br>
			<%	
				End If 	
				i = i + 1
				rsShipMethod.MoveNext
			Loop 
			
			' object cleanup
			closeobj(rsShipMethod)	
			%>
				</td>
		        </tr>        
		        <tr>
		        <td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Carrier Based Shipping Options</font></b></td>
		        </tr>
		        <tr>
		        <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Country Products are shipped from:</font></td>
		        <td width="50%" colspan="2"><select name="OriginCountry" title="Country Products are Shipped From" style="<%= C_FORMDESIGN %>">
		        <% Response.Write getCountryList(sOriginCountry)%></select></td>
		        </tr>
		        <tr>
		        <td width="50%" align="right" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Zip Code Products are shipped from:</font></td>
		        <td width="50%" colspan="2"><input type="text" name="OriginZip" title="Zip Code" style="<%= C_FORMDESIGN %>" size="10" value="<%= sOriginZip %>"></td>
		        </tr>
		        <tr>
		        <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Backup Shipping Method:</font></td>
		        <td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" value="1" name="shiptype2" <% If iShipType2 = 1 Then%>checked<% End If%> onClick="availableShipping()">Value Based Shipping<br><input type="radio" value="3" name="shiptype2" <% If iShipType2 = 3 Then%>checked<% End If%> onClick="availableShipping()">Product Based Shipping</font></td>
		        </tr>
		        <tr>
        		 <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">USPS User Name:</font></td>
       		 <td width="50%" colspan="2"><input type="text" value="<%= suspsUsername %>" name="uspsUser" size="40" style="<%= C_FORMDESIGN %>"></td>
       		 </tr>
       		 <tr>
       		 <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">USPS Password:</font></td>
       		 <td width="50%" colspan="2"><input type="text" value="<%= suspsPassword %>" name="uspsPass" size="40" style="<%= C_FORMDESIGN %>"></td>
       		 </tr>
       		 <tr>
		        <tr>
		        <td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Value Based Shipping Setup</font></b></td>
		        </tr>
		        <tr>
		        <td width="50%" align="center" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><b>For Order Totals up To</b></font></td>
		        <td width="50%" align="center" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><b>Shipping Charge</b></font></td>
		        </tr> 
		        <% 		        
					For iCounter = 1 to iValueShipNumbers - 1
		        %> 
					<tr>
					<td width="20%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
					<td width="20%"><input name="T<%=iCounter%>" value="<%= rsValueShip.Fields("valShpPurTotal") %>" style="<%= C_FORMDESIGN %>" size="10"></td>
					<td width="25%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
					<td width="35%"><input name="TV<%=iCounter%>" value="<%= rsValueShip.Fields("valShpAmt") %>" style="<%= C_FORMDESIGN %>" size="10">
					</td>					
					</tr>
		        <%				
					rsValueShip.MoveNext					
					Next
		        %>
		        <% If iValueShipNumbers > 0 Then %>
		        <tr>
					<td width="20%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
					<td width="20%"><input name="T<%=iCounter%>" value="<%= rsValueShip.Fields("valShpPurTotal") %>" style="<%= C_FORMDESIGN %>" size="10"></td>
					<td width="25%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
					<td width="35%" valign="bottom"><input name="TV<%=iCounter%>" value="<%= rsValueShip.Fields("valShpAmt") %>" style="<%= C_FORMDESIGN %>" size="10">
									<input type="image" src="images/delete.gif" alt="Delete this row" name="deleteValShipImg" value="<%= rsValueShip.Fields("valShpAmt") %>" WIDTH="82" HEIGHT="21">
									<input type="hidden" name="deleteValShip" value="<%= rsValueShip.Fields("valShpPurTotal") %>">
					</td>					
				</tr>
				<% End If %>
		        <tr>
		        <td width="20%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
		        <td width="20%"><input name="TNew" style="<%= C_FORMDESIGN %>" size="10"></td>
		        <td width="25%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Amount:</font></td>
		        <td width="35%" height="30" valign="top"><input name="TVNew" style="<%= C_FORMDESIGN %>" size="10"> <input type="image" src="images/add.gif" name="valueBasedInput" WIDTH="82" HEIGHT="21"></td>
		        </tr>

		        <tr>
		        <td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Handling Charges</font></b></td>
		        </tr>

		        <tr>
		        <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Apply Handling Charge:</font></td>
		        <td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" name="ApplyHandling" title="Apply Handling Charge" value="1" <% If iHandling = 1 Then %>checked<% End If %> onClick="handling()">Yes <input name="ApplyHandling" type="radio" title="Apply Handling Charge" value="0" <% If iHandling = 0 Then %>checked<% End If %> onClick="handling()">No</font></td>
		        </tr>

		        <tr>
		        <td width="50%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Apply Handling to:</font></td>
		        <td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" name="HandlingType" value="0" <% If iHandlingType = 0 Then%>checked<% End If %>>Shipped Orders Only<br><input type="radio" name="HandlingType" value="1" <% If iHandlingType = 1 Then%>checked<% End If %>>All Orders</font></td>
		        </tr>

		        <tr>
		        <td width="50%" align="right" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Handling Charge:</font></td>
		        <td width="50%" colspan="2"><input type="text" name="HandlingCharge" title="Handling Charge" style="<%= C_FORMDESIGN %>" size="10" value="<%= sHandling%>"></td>
		        </tr>
		        
		        <tr>
		        <td width="100%" colspan="4" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Shipping Charges</font></b></td>
		        </tr>
		        
			    <tr>
				<td width="100%" align="right" valign="top" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Apply Tax to Shipping:</td>
				<td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" value="1" <% If iShipTaxIsActive = 1 Then%>checked<%End If%> name="ShipTax">Yes <input type="radio" name="ShipTax" <% If iShipTaxIsActive = 0 Then%>checked<%End If%> value="0">No</font></td>
				</tr>		        
				
		        <tr>
		        <td width="50%" align="right" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Premium Shipping: </font></td>
		        <td width="50%" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" <% If iPrmShipIsActive = 1 Then %>checked<% End If%> name="iPremiumIsActive" title="Premium Shipping Active" size="1" value="1" onClick="premiumShip()">Yes
											<input type="radio" <% If iPrmShipIsActive = 0 Then %>checked<% End If%> name="iPremiumIsActive" title="Premium Shipping Not Active" size="10" value="0" onClick="premiumShip()">No</font>
		        </td>
		        </tr>
				
				<tr>
		        <td width="50%" align="right" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Premium Shipping Charge: </font></td>
		        <td width="50%" colspan="2"><input type="text" name="PremiumShipCharge" title="Premium Shipping Charge" style="<%= C_FORMDESIGN %>" size="10" value="<%= sSpcShipAmt %>"></td>
		        </tr>        

		        
		        <tr>
		        <td width="50%" align="right" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Minimum Shipping Charge:</font></td>
		        <td width="50%" colspan="2"><input type="text" name="Minimum" title="Minimum Shipping Charge" style="<%= C_FORMDESIGN %>" size="10" value="<%= sShipMin %>"></td>
		        </tr>
		        
		        <tr>
				<input type="hidden" name="valuecounter" value="<%= iCounter %>">
		        <input type="hidden" name="shipCount" value="<%= iRecordCount %>">
		        <td width="100%" align="center" colspan="4"><input type="image" name="submit" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21"></td>
		        </tr>
		        </table>
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
	</td>
	</tr>
</table>
</body>
</html>






