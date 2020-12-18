<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SFLib/ADOVBS.inc"-->
<!--#include file="../SFLib/incDesign.asp"-->
<html>

<head>
<title>SF Menu Page</title>
</head>

<%
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0010i.asp
	
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
Dim prodID, prodCategoryId, prodManufacturerId, prodVendorId, prodName, prodDescription, prodMessage
Dim prodImageSmallPath, prodImageLargePath, prodLink, prodPrice, prodWeight, prodShip, Output, choice0, choice1, choice2, choice
Dim prodShipIsAct, prodCountryTaxIsAct, prodStateTaxIsAct, prodEnabledIsAct, addAttribute, attNumber
Dim addAttType, attNumDetail, rsProdInput, rsProdAttGet, counter, tempVar, tempVarName, tempVarPrice
Dim tempVarSet, counter2, tempVar3, SQL, tempPrice, txtEditProdID, prodSalePrice, prodSaleIsAct, sTitle

' Editing attributes
txtEditProdID = Request.QueryString("ProdID")

If txtEditProdID <> "" AND Request.Form("deleteproduct.x") = "" AND Request.Form("prodSubmit.x") = "" AND Request.Form("AddAttribute.x") = "" AND Request.Form("deleteAttributes.x") = "" AND Request.Form("addAttType") = "" Then
	
	sTitle=" Edit Product: " & txtEditProdID
	
	SQL = "SELECT * FROM sfProducts WHERE prodID = '" & txtEditProdID & "'"
	Set rsGetProductInfo = Server.CreateObject("ADODB.RecordSet")
	rsGetProductInfo.Open SQL, cnn, adOpenStatic, adLockOptimistic, adCmdText
	SQL = "SELECT * FROM sfAttributes INNER JOIN sfProducts ON sfAttributes.attrProdId = sfProducts.prodID WHERE prodID = '" & txtEditProdID & "'"
	Set rsGetAttributeTitle = Server.CreateObject("ADODB.RecordSet")
	rsGetAttributeTitle.Open SQL, cnn, adOpenStatic, adLockOptimistic, adCmdText
	
	SQL = "SELECT * FROM sfAttributeDetail INNER JOIN (sfAttributes INNER JOIN sfProducts ON sfAttributes.attrProdId = sfProducts.prodID) " _
		  & "ON sfAttributeDetail.attrdtAttributeID = sfAttributes.attrID WHERE prodID = '" & txtEditProdID & "'"
	Set rsGetAttributeDeatils = Server.CreateObject("ADODB.RecordSet")
	rsGetAttributeDeatils.Open SQL, cnn, adOpenStatic, adLockOptimistic, adCmdText

	prodID =              Trim(rsGetProductInfo.Fields("prodID"))
	prodCategoryId =      Trim(rsGetProductInfo.Fields("prodCategoryId"))
	prodManufacturerId =  Trim(rsGetProductInfo.Fields("prodManufacturerId"))
	prodVendorId =        Trim(rsGetProductInfo.Fields("prodVendorId"))
	prodName =            Trim(rsGetProductInfo.Fields("prodName"))
	prodNamePlural =      Trim(rsGetProductInfo.Fields("prodNamePlural"))
	prodShortDescription =Trim(rsGetProductInfo.Fields("prodShortDescription"))
	prodDescription =     Trim(rsGetProductInfo.Fields("prodDescription"))
	prodMessage =         Trim(rsGetProductInfo.Fields("prodMessage"))
	prodImageSmallPath =  Trim(rsGetProductInfo.Fields("prodImageSmallPath"))
	prodImageLargePath =  Trim(rsGetProductInfo.Fields("prodImageLargePath"))
	prodLink =            Trim(rsGetProductInfo.Fields("prodLink"))
	prodPrice =           Trim(rsGetProductInfo.Fields("prodPrice"))
	prodWeight =          Trim(rsGetProductInfo.Fields("prodWeight"))
	prodSalePrice =       Trim(rsGetProductInfo.Fields("prodSalePrice"))
	prodSaleIsAct =       Trim(rsGetProductInfo.Fields("prodSaleIsActive"))
	prodShip =            Trim(rsGetProductInfo.Fields("prodShip"))
	prodShipIsAct =       Trim(rsGetProductInfo.Fields("prodShipIsActive"))
	prodCountryTaxIsAct = Trim(rsGetProductInfo.Fields("prodCountryTaxIsActive"))
	prodStateTaxIsAct =   Trim(rsGetProductInfo.Fields("prodStateTaxIsActive"))
	prodEnabledIsAct =    Trim(rsGetProductInfo.Fields("prodEnabledIsActive"))
	attNumber =           Trim(rsGetProductInfo.Fields("prodAttrNum"))
	
Else
	
	'Request all inputs		
	prodID =              Trim(Request.Form("prodID"))
	prodCategoryId =      Trim(Request.Form("prodCategoryId"))
	prodManufacturerId =  Trim(Request.Form("prodManufacturerId"))
	prodVendorId =        Trim(Request.Form("prodVendorId"))
	prodName =            Trim(Request.Form("prodName"))
	prodNamePlural =      Trim(Request.Form("prodNamePlural"))
	prodDescription =     Trim(Request.Form("prodDescription"))
	prodShortDescription =Trim(Request.Form("prodShortDescription"))
	prodMessage =         Trim(Request.Form("prodMessage"))
	prodImageSmallPath =  Trim(Request.Form("prodImageSmallPath"))
	prodImageLargePath =  Trim(Request.Form("prodImageLargePath"))
	prodLink =            Trim(Request.Form("prodLink"))
	prodPrice =           Trim(Request.Form("prodPrice"))
	prodWeight =          Trim(Request.Form("prodWeight"))
	prodSalePrice =       Trim(Request.Form("prodSalePrice"))
	prodSaleIsAct =       Trim(Request.Form("prodSaleIsAct"))
	prodShip =            Trim(Request.Form("prodShip"))
	prodShipIsAct =       Trim(Request.Form("prodShipIsAct"))
	prodCountryTaxIsAct = Trim(Request.Form("prodCountryTaxIsAct"))
	prodStateTaxIsAct =   Trim(Request.Form("prodStateTaxIsAct"))
	prodEnabledIsAct =    Trim(Request.Form("prodEnabledIsAct"))
	addAttribute =        Trim(Request.Form("addAttribute"))
	If Request.Form("deleteAttributes.x") <> "" Then	
		attNumber = 0
		Set rsDeleteAttribute = Server.CreateObject("ADODB.RecordSet")
		sSQL = "DELETE FROM sfAttributes WHERE attrProdId = '" & prodID & "'"
		rsDeleteAttribute = cnn.Execute(sSQL)
		closeObj(rsDeleteAttribute)
	Else
		attNumber = Trim(Request.Form("attNumber"))
	End If
	addAttType =          Trim(Request.Form("addAttType"))
	attNumDetail = 	      Trim(Request.Form("attNumDetail"))
End If 

If Request.Form("deleteproduct.x") <> "" Then	
	SQL = "DELETE FROM sfProducts WHERE prodID = '" & prodID & "'"
	cnn.Execute(SQL)
%>	
	<html>
	<head>
	<title>Delete Products&gt;</title>
	</head>
	<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
	<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
    <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center" bgcolor="<%= C_BGCOLOR3 %>">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b>Product <%= prodID %> has been Deleted
				</font>
				<br><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>"><a href="mnu.asp">Return to Menu</b></a>
				</font></b>
				</td></tr>
				</table>
			</td></tr>	
			</table>
        </td>
    </tr>
    </table>
    </body>
    </html>
<%
Response.End 
End If 

If Request.Form("prodSubmit.x") <> "" Then	
	'correct null data
	If prodShipIsAct="" Then
		prodShipIsAct="0"
	End If
	
	If prodCountryTaxIsAct="" Then
		prodCountryTaxIsAct="0"
	End If

	If prodStateTaxIsAct="" Then
		prodStateTaxIsAct="0"
	End If

	If prodEnabledIsAct="" Then
		prodEnabledIsAct="0"
	End If	
	
	If prodSaleIsAct="" Then
		prodSaleIsAct="0"
	End If	
	
	If attNumber = "" Then attNumber = 0
	
	'Input information into the database
	Set rsProdInput = Server.CreateObject("ADODB.RecordSet")
	SQL = "SELECT * FROM sfProducts WHERE prodID = '" & prodID & "'"
	rsProdInput.Open SQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText
	
	'Input base product information	
	rsProdInput.Fields("prodCategoryId")         = prodCategoryId
	rsProdInput.Fields("prodManufacturerId")     = prodManufacturerId
	rsProdInput.Fields("prodVendorId")           = prodVendorId
	rsProdInput.Fields("prodName")               = prodName
	rsProdInput.Fields("prodNamePlural")         = prodNamePlural
	rsProdInput.Fields("prodDescription")        = prodDescription
	rsProdInput.Fields("prodShortDescription")   = prodShortDescription
	rsProdInput.Fields("prodMessage")            = prodMessage
	rsProdInput.Fields("prodImageSmallPath")     = prodImageSmallPath
	rsProdInput.Fields("prodImageLargePath")     = prodImageLargePath
	rsProdInput.Fields("prodLink")               = prodLink
	rsProdInput.Fields("prodPrice")              = prodPrice
	rsProdInput.Fields("prodWeight")             = prodWeight
	rsProdInput.Fields("prodSalePrice")          = prodSalePrice
	rsProdInput.Fields("prodSaleIsActive")       = prodSaleIsAct
	rsProdInput.Fields("prodShip")               = prodShip
	rsProdInput.Fields("prodShipIsActive")       = prodShipIsAct
	rsProdInput.Fields("prodCountryTaxIsActive") = prodCountryTaxIsAct
	rsProdInput.Fields("prodStateTaxIsActive")   = prodStateTaxIsAct
	rsProdInput.Fields("prodEnabledIsActive")    = prodEnabledIsAct
	rsProdInput.Fields("prodAttrNum")            = attNumber
	rsProdInput.Fields("prodDateAdded")          = Now()
	rsProdInput.Update
	rsProdInput.Close

	SQL = "SELECT * FROM sfAttributes INNER JOIN sfProducts ON sfAttributes.attrProdId = sfProducts.prodID WHERE prodID = '" & prodID & "'"
	Set rsGetAttributeTitle = Server.CreateObject("ADODB.RecordSet")
	rsGetAttributeTitle.Open SQL, cnn, adOpenStatic, adLockOptimistic, adCmdText
	
	SQL = "SELECT * FROM sfAttributeDetail INNER JOIN (sfAttributes INNER JOIN sfProducts ON sfAttributes.attrProdId = sfProducts.prodID) " _
		  & "ON sfAttributeDetail.attrdtAttributeID = sfAttributes.attrID WHERE prodID = '" & prodID & "'"
	Set rsGetAttributeDeatils = Server.CreateObject("ADODB.RecordSet")
	rsGetAttributeDeatils.Open SQL, cnn, adOpenStatic, adLockOptimistic, adCmdText
	
	Do While Not rsGetAttributeDeatils.EOF
		rsGetAttributeDeatils.Delete
		rsGetAttributeDeatils.MoveNext
	Loop
	Do While Not rsGetAttributeTitle.EOF
		rsGetAttributeTitle.Delete 
		rsGetAttributeTitle.MoveNext
	Loop
	
	rsProdInput.Open "sfAttributes", cnn, adOpenDynamic, adLockOptimistic, adCmdTable 
	
	'Input Attribute Headers
	If Err.number = 0 Then
		counter = 1
		Do While Request.Form("T" & counter) <> ""
			tempVar = "T" & counter
			rsProdInput.AddNew 
			rsProdInput.Fields("attrProdId") = prodID
			rsProdInput.Fields("attrName") = Request.Form(tempVar)
			rsProdInput.Update
			counter = counter + 1
		Loop
	
		rsProdInput.Close 
			
		SQL = "SELECT attrID, attrName FROM sfAttributes WHERE attrProdId = '" & prodID & "'"
		Set rsProdAttGet = Server.CreateObject("ADODB.RecordSet")
		rsProdAttGet.Open SQL, cnn, adOpenForwardOnly, adLockOptimistic, adCmdText
	
		rsProdInput.Open "sfAttributeDetail", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
	
		'Input Attribute Details
		counter = 1
		Do While Not rsProdAttGet.EOF 
			tempVar = "T" & counter
			tempVarName = "N" & counter
			tempVarPrice = "P" & counter
			tempVarSet = "S" & counter
			If Request.Form(tempVar) = rsProdAttGet.Fields("attrName") Then
				counter2 = 1
				Do While Request.Form(tempVarName & counter2) <> ""
					rsProdInput.AddNew 
					rsProdInput.Fields("attrdtAttributeId") = rsProdAttGet.Fields("attrID")
					rsProdInput.Fields("attrdtName") = Request.Form(tempVarName & counter2)
					tempPrice = Request.Form(tempVarPrice & counter2)
					If tempPrice = "" Then
						rsProdInput.Fields("attrdtPrice") = "0"
					Else
						rsProdInput.Fields("attrdtPrice") = tempPrice
					End If 
					rsProdInput.Fields("attrdtType") = Request.Form(tempVarSet & counter2)
					rsProdInput.Update 
					counter2 = counter2 + 1
				Loop
				rsProdAttGet.MoveNext
				counter = 1
			Else
				counter = counter + 1
			End If
		Loop	
	
		rsProdInput.Close
		rsProdAttGet.Close 
	
		Set rsProdInput = nothing
		Set rsProdAttGet = nothing
	End If 
	
End If 
%>	
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<form method="post" name="formAddProduct">
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
    <td align="center" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Add Product to Store</font></b></td>
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>...</font></td>
    </tr>
<%
If Err.number <> 0 Then
	If Err.number = 3705 Then
%>
    <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center" bgcolor="<%= C_BGCOLOR3 %>">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#FF0000" size="<%= C_FONTSIZE5+2 %>"><b>Database Error
				</font></b>
				<br>
				<b><font face="<%= C_FONTFACE5 %>" color="#FF0000" size="<%= C_FONTSIZE5 %>">Error Number= <%= Err.number%><br>Error Description= The Product ID already Exists, Please Enter a Different ID
				</td></tr>
				</table>
			</td></tr>	
			</table>
        </td>
    </tr>
	<%Else%>	
    <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center" bgcolor="<%= C_BGCOLOR3 %>">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#FF0000" size="<%= C_FONTSIZE5+2 %>"><b>Database Error
				</font></b>
				<br>
				<b><font face="<%= C_FONTFACE5 %>" color="#FF0000" size="<%= C_FONTSIZE5 %>">Error Number= <%= Err.number%><br>Error Description= <%= Err.description %>
				</td></tr>
				</table>
			</td></tr>	
			</table>
        </td>
    </tr>
<%
	End If
End If
If Request.Form("prodSubmit.x") <> "" And Err.number = 0 Then
%>
    <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center" bgcolor="<%= C_BGCOLOR3 %>">
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
<%End If%>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product</font></b></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product ID:</font></td>
        <td width="50%"><input type="text" name="prodID" value="<%= prodID%>" size="20" style="<%= C_FORMDESIGN %>" readonly></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Name:</font></td>
        <td width="50%"><input type="text" name="prodName" value="<%= prodName%>" size="20" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Name Plural:</font></td>
        <td width="50%"><input type="text" name="prodNamePlural" value="<%= prodNamePlural%>" size="20" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Category:</font></td>
        <td width="50%"><select size="1" name="prodCategoryId" style="<%= C_FORMDESIGN %>"><%= getCategoryList(prodCategoryId)%></select></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product
          Manufacturer:</font></td>
        <td width="50%"><select size="1" name="prodManufacturerId" style="<%= C_FORMDESIGN %>">
            <%= getManufacturersList(prodManufacturerId)%>
          </select>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product
          Vendor:</font></td>
        <td width="50%"><select size="1" name="prodVendorId" style="<%= C_FORMDESIGN %>">
            <%= getVendorList(prodVendorId)%>
          </select>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Price:</font></td>
        <td width="50%"><font face="Verdana" size="2"><input type="text" name="prodPrice" value="<%= prodPrice%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right" valign="top" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Short Description :<br><i><font size="-1">used on Search.asp</font></i></font></td>
        <td width="50%"><font face="Verdana" size="2"><textarea rows="2" name="prodShortDescription" cols="30" style="<%= C_FORMDESIGN %>"><%= prodShortDescription %></textarea>
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Long Description : <br><i><font size="-1">used on Detail.asp</font></i></font></td>
        <td width="50%"><font face="Verdana" size="2"><textarea rows="4" name="prodDescription" cols="30" style="<%= C_FORMDESIGN %>"><%= prodDescription %></textarea>
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Confirmation Message:</font></td>
        <td width="50%"><font face="Verdana" size="2"><textarea name="prodMessage" cols="30" rows="4" style="<%= C_FORMDESIGN %>"><%= prodMessage%></textarea>
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Link:</font></td>
        <td width="50%"><font face="Verdana" size="2"><input type="text" name="prodLink" value="<%= prodLink%>" size="30" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Weight:</font></td>
        <td width="50%"><font face="Verdana" size="2"><input type="text" name="prodWeight" value="<%= prodWeight%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Product:</font></td>
        <%If prodEnabledIsAct = "" Then'checks the correct option%>
		<td width="50%"><input type="checkbox" name="prodEnabledIsAct" value="1"></td>
		<%Else%>
		<td width="50%"><input type="checkbox" name="prodEnabledIsAct" value="1" checked></td>
		<%End If%>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Attributes</font></b></td>
        </tr>
        <tr>
        <td width="100%" colspan="2">
        <table width="100%" border="0">
               
        <% If attNumber > 0 Then %>
        <tr>
        <td colspan="2" align="center">
        <input type="image" name="deleteAttributes" src="images/delattr.gif" WIDTH="108" HEIGHT="21">
        </td>
        </tr>
        <% End If %>
        
		<% If txtEditProdID <> "" AND Request.Form("deleteproduct.x") = "" AND Request.Form("prodSubmit.x") = "" AND Request.Form("AddAttribute.x") = "" AND Request.Form("deleteAttributes.x") = "" AND Request.Form("addAttType") = "" Then
			counter = 1
        	Do While Not rsGetAttributeTitle.EOF  'Loop which gathers Attribute Titles already added
					tempVar = "T" & counter
					tempVarName = "N" & counter
					tempVarPrice = "P" & counter
					tempVarSet = "S" & counter
		%>
		<tr><td colspan="2"><hr></td></tr><tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Attribute Title: </td><td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="T<%= counter %>" value="<%= rsGetAttributeTitle.Fields("attrName") %>" size="15" style="<%= C_FORMDESIGN %>"></td></tr> 
		<%			
					counter2 = 1
					If Not (rsGetAttributeDeatils.EOF And rsGetAttributeDeatils.BOF) Then rsGetAttributeDeatils.MoveFirst 
					'rsGetAttributeDeatils.MoveFirst 
					Do While Not rsGetAttributeDeatils.EOF 'Loop which gathers Attribute Options
						If rsGetAttributeDeatils.Fields("attrdtAttributeId") = rsGetAttributeTitle.Fields("attrID") Then
		%>
		<tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Option <%= counter2 %>: </td><td><input type="text" name="<%= tempVarName & counter2 %>" value="<%= rsGetAttributeDeatils.Fields("attrdtName") %>" size="10" style="<%= C_FORMDESIGN %>">
		<% 
							choice = rsGetAttributeDeatils.Fields("attrdtType")
							choice0 = ""
							choice1 = ""
							choice2 = ""
							If choice = "0" Then
								choice0 = "checked"
							ElseIf choice = "1" Then
								choice1 = "checked"
							ElseIf choice = "2" Then
								choice2 = "checked"
							End If
		%>
							<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">	
							<input type="radio" <%=choice0%> name="<%= tempVarSet & counter2 %>" value="0">Same Price
							<input type="radio" <%=choice1%> name="<%= tempVarSet & counter2 %>" value="1">Increase
							<input type="radio" <%=choice2%> name="<%= tempVarSet & counter2 %>" value="2">Decrease by: 
		<%
						
							If choice = "0" OR rsGetAttributeDeatils.Fields("attrdtPrice") = "" Then
								tempVar3 = "0"
							Else
								tempVar3 = rsGetAttributeDeatils.Fields("attrdtPrice")
							End If 
		%>
						<input type="text" name="<%= tempVarPrice & counter2 %>" value="<%= tempVar3 %>" style="<%= C_FORMDESIGN %>" size="3"></td></tr>
		<%
							counter2 = counter2 + 1
						End If
						rsGetAttributeDeatils.MoveNext
					Loop
					
					counter = counter + 1
					rsGetAttributeTitle.MoveNext
				Loop 'The Loops above only gather information already added
				Response.Write "<input type=hidden name=attNumber value=" & attNumber & ">"
			End If				
		'Gathers Attribute information and displayes in correct HTML format
		addAttribute = Request.Form("AddAttribute.x")
		If addAttribute <> "" or addAttType = "Options" or Request.Form("prodSubmit.x") <> "" Then
			If attNumber = "" and Request.Form("prodSubmit.x") <> "" Then 'First Instance of Attribute being added
		%>
		<tr><td colspan="2"><hr></td></tr><tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Attribute Title: </td><td><input type="text" name="T1" size="15"></td></tr>
		<tr><td align="center" colspan="2"><input type="submit" name="addAttType" value="Options"></td></tr><input type="hidden" name="attNumber" value="1">
		<%		
			Else
				counter = 1
				Do While Request.Form("T" & counter) <> "" 'Loop which gathers Attribute Titles already added
					tempVar = "T" & counter
					tempVarName = "N" & counter
					tempVarPrice = "P" & counter
					tempVarSet = "S" & counter
		%>
		<tr><td colspan="2"><hr></td></tr><tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Attribute Title: </td><td><input type="text" name="T<%= counter %>" value="<%= Request.Form(tempVar) %>" size="15" style="<%= C_FORMDESIGN %>"></td></tr> 
		<%			
					counter2 = 1
					Do While Trim(Request.Form(tempVarName & counter2)) <> "" 'Loop which gathers Attribute Options
		%>
		<tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Option <%= counter2 %>: </td><td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="<%= tempVarName & counter2 %>" value="<%= Request.Form(tempVarName & counter2) %>" size="10" style="<%= C_FORMDESIGN %>">
		<% 
						choice = Trim(Request.Form(tempVarSet & counter2))
						choice0 = ""
						choice1 = ""
						choice2 = ""
						If choice = "0" Then
							choice0 = "checked"
						ElseIf choice = "1" Then
							choice1 = "checked"
						ElseIf choice = "2" Then
							choice2 = "checked"
						End If
		%>
							<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
							<input type="radio" <%=choice0%> name="<%= tempVarSet & counter2 %>" value="0">Same Price
							<input type="radio" <%=choice1%> name="<%= tempVarSet & counter2 %>" value="1">Increase
							<input type="radio" <%=choice2%> name="<%= tempVarSet & counter2 %>" value="2">Decrease by: 
		<%
						
						If choice = "0" OR Trim(Request.Form(tempVarPrice & counter2)) = "" Then
							tempVar3 = "0"
						Else
							tempVar3 = Trim(Request.Form(tempVarPrice & counter2))
						End If 
		%>
						<input type="text" name="<%= tempVarPrice & counter2 %>" value="<%= tempVar3 %>" size="3" style="<%= C_FORMDESIGN %>"></td></tr>
		<%
						counter2 = counter2 + 1
					Loop
					counter = counter + 1
				Loop 'The Loops above only gather information already added
				If addAttType = "Options" Then 'If Statement addeds correct input field for new Option or Attribute Title
		%>
					<tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Option <%= counter2 %>: </td><td><input type="text" name="<%= tempVarName & counter2 %>" size="10" style="<%= C_FORMDESIGN %>">
					<input type="radio" checked name="<%= tempVarSet & counter2 %>" value="0">Same Price
					<input type="radio" name="<%= tempVarSet & counter2 %>" value="1">Increase
					<input type="radio" name="<%= tempVarSet & counter2 %>" value="2">Decrease by: 
					<input type="text" name="<%= tempVarPrice & counter2 %>" size="3" style="<%= C_FORMDESIGN %>"></td></tr>
					<tr><td align="center" colspan="2"><input type="submit" name="addAttType" value="Options"></td></tr>
					<input type="hidden" name="attNumber" value="<%= counter-1 %>">
		<%
				ElseIf addAttribute <> "" Then
		%>
					<tr><td colspan="2"><hr></td></tr><tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Attribute Title: </td><td><input type="text" name="T<%=  counter %>" size="15" style="<%= C_FORMDESIGN %>"></tr></td><tr><td align="center" colspan="2">
					<input type="submit" name="addAttType" value="Options"></td></tr><input type="hidden" name="attNumber" value="<%= counter-1 %>">
		<%
				End If	
			End If
		End If
		%>
		</table>
		</td>
		</tr>
        <tr>
        <td width="100%" colspan="2" align="center"><input type="image" name="AddAttribute" border="0" src="images/addattr.gif" width="108" height="21"></td>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Images</font></b></td>
        </tr>
        <tr>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Small Image<br>
          <input type="text" name="prodImageSmallPath" value="<%= prodImageSmallPath%>" size="30" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Large Image<br>
          <input type="text" name="prodImageLargePath" value="<%= prodImageLargePath%>" size="30" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Sale Options</font></b></td>
        </tr>
        <tr>
        <%If prodSaleIsAct = "1" Then 'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodSaleIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodSaleIsAct" value="1">
		<%End If%>
		<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Sale&nbsp;</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Sale Price<br>
          <input type="text" name="prodSalePrice" value="<%= prodSalePrice%>" size="20" style="<%= C_FORMDESIGN %>">
          </font></td>
        </tr>
        <tr>
       <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Shipping Options</font></b></td>
        </tr>
        <tr>
        <%If prodShipIsAct = "1" Then 'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodShipIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodShipIsAct" value="1">
		<%End If%>
		<font face="Verdana" size="2">Activate Shipping&nbsp;</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Shipping Cost<br>
          <input type="text" name="prodShip" value="<%= prodShip%>" size="20" style="<%= C_FORMDESIGN %>">
          </font></td>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Tax Options</font></b></td>
        </tr>
        <tr>
        <%If prodStateTaxIsAct = "1" Then'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodStateTaxIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodStateTaxIsAct" value="1">
		<%End If%>
		<font face="Verdana" size="2">Activate State Tax</font></td>
        <%If prodCountryTaxIsAct = "1" Then'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodCountryTaxIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodCountryTaxIsAct" value="1">
		<%End If%> 
		<font face="Verdana" size="2">Activate Country Tax</font></td>
        </tr>
        <tr>
		<td align="center" width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>"><input type="image" border="0" src="images/submit.gif" name="prodSubmit" WIDTH="108" HEIGHT="21"> <input type="image" border="0" name="deleteproduct" src="images/delprod.gif" WIDTH="108" HEIGHT="21"></td>
		</tr>
        </table><br>
    </td>
    </tr>
	    <tr>
		<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="m0010.asp">Edit Another Product</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
        </tr>
</table>
</td>
</tr>
</table>
</form>
</body>
<%
closeObj(rsGetAttributeDeatils)
closeObj(rsGetAttributeTitle)
%>
</html>




