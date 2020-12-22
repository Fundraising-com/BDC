<%
	Option Explicit
	Response.Buffer=True       
    Const vDebug = 0
    '--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0009.asp
	
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
%>
<SCRIPT language="javascript" src="../../SFLib/sfCheckErrors.js"></SCRIPT>
<SCRIPT language="javascript">
	var bAddProduct
	bAddProduct = false
	function newCategory() {
		if (formAddProduct.prodCategoryId.value == "New") {
			formAddProduct.prodCategoryNewId.disabled = false
		}
		else {
			formAddProduct.prodCategoryNewId.disabled = true
			formAddProduct.prodCategoryNewId.value = "Select New Category"
		}	
	}
	function newMFG() {
		if (formAddProduct.prodManufacturerId.value == "New") {
			formAddProduct.prodManufacturerNewId.disabled = false
		}
		else {
			formAddProduct.prodManufacturerNewId.disabled = true
			formAddProduct.prodManufacturerNewId.value = "Select New Manufacturer"
		}	
	}
	function newVendor() {
		if (formAddProduct.prodVendorId.value == "New") {
			formAddProduct.prodVendorNewId.disabled = false
		}
		else {
			formAddProduct.prodVendorNewId.disabled = true
			formAddProduct.prodVendorNewId.value = "Select New Vendor"
		}	
	}
	function addproductCheck(form) {
		if (bAddProduct) {
			if (sfCheck(form)) {
				return true;
			}
			else {
				bAddProduct = false;
				return false;
			}
		}
		else {
			return true;
		}
	}
	function addProductClicked() {
		bAddProduct = true
	}
</SCRIPT>

<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SFLib/ADOVBS.inc"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<html>

<head>
<title>SF Menu Page</title>
</head>

<%
dim prodID, prodCategoryId, prodManufacturerId, prodVendorId, prodName, prodDescription, prodMessage
dim prodImageSmallPath, prodImageLargePath, prodLink, prodPrice, prodWeight, prodShip, Output, choice0, choice1, choice2, choice
dim prodShipIsAct, prodCountryTaxIsAct, prodStateTaxIsAct, prodEnabledIsAct, addAttribute, attNumber
dim addAttType, attNumDetail, rsProdInput, rsProdAttGet, counter, tempVar, tempVarName, tempVarPrice
dim tempVarSet, counter2, tempVar3, SQL, tempPrice, prodSalePrice, prodSaleIsAct, prodShortDescription, prodNamePlural, selected
dim Cvalue, Mvalue, Vvalue, rsNewCat, rsNewMFG, rsNewVen, iBookMark
'The page is devided into two area, confirmation area, and gathering input area

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
attNumber =           Trim(Request.Form("attNumber"))
addAttType =          Trim(Request.Form("addAttType"))
attNumDetail = 	      Trim(Request.Form("attNumDetail"))

	'correct null data
	If prodShipIsAct="" Then
		prodShipIsAct= 0
	End If
	
	If prodCountryTaxIsAct="" Then
		prodCountryTaxIsAct= 0
	End If

	If prodStateTaxIsAct="" Then
		prodStateTaxIsAct= 0
	End If

	If prodEnabledIsAct="" Then
		prodEnabledIsAct= 0 
	End If	
	
	If prodSaleIsAct="" Then
		prodSaleIsAct= 0
	End If	

'Confirmation and database insert area
If Request.Form("addproduct.x") <> "" Then
	If prodShip = "" Then prodShip = 0
	If prodSalePrice = "" Then prodSalePrice = 0
	If attNumber = "" Then attNumber = 0	
	If prodCategoryId = "New" Then
		Set rsNewCat = Server.CreateObject("ADODB.RecordSet")
		rsNewCat.CursorLocation = adUseClient
		rsNewCat.Open "sfCategories Order By catID", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
		rsNewCat.AddNew 
		rsNewCat.Fields("catName") = Trim(Request.Form("prodCategoryNewId"))
		rsNewCat.Update 
		
		iBookMark = rsNewCat.AbsolutePosition 
		rsNewCat.Requery 
		rsNewCat.AbsolutePosition = iBookMark
		
		prodCategoryId = rsNewCat.Fields("catID")		
		rsNewCat.Close 
		Set rsNewCat = nothing
	End If
	
	If prodManufacturerId = "New" Then
		Set rsNewMFG = Server.CreateObject("ADODB.RecordSet")
		rsNewMFG.CursorLocation = adUseClient
		rsNewMFG.Open "sfManufacturers Order By mfgID", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
		rsNewMFG.AddNew 
		rsNewMFG.Fields("mfgName") = Trim(Request.Form("prodManufacturerNewId"))
		rsNewMFG.Update 
		
		iBookMark = rsNewMFG.AbsolutePosition 
		rsNewMFG.Requery 
		rsNewMFG.AbsolutePosition = iBookMark
		
		prodManufacturerId = rsNewMFG.Fields("mfgID")
		rsNewMFG.Close 
		Set rsNewMFG = nothing
	End If
	
	If prodVendorId = "New" Then
		Set rsNewVen = Server.CreateObject("ADODB.RecordSet")
		rsNewVen.CursorLocation = adUseClient
		rsNewVen.Open "sfVendors Order By vendID", cnn, adOpenKeyset, adLockOptimistic, adCmdTable
		rsNewVen.AddNew 
		rsNewVen.Fields("vendName") = Trim(Request.Form("prodVendorNewId"))
		rsNewVen.Update 
		
		iBookMark = rsNewVen.AbsolutePosition 
		rsNewVen.Requery 
		rsNewVen.AbsolutePosition = iBookMark
		
		prodVendorId = rsNewVen.Fields("vendID")
		rsNewVen.Close 
		Set rsNewVen = nothing
	End If
	
	'Input information into the database
	Set rsProdInput = Server.CreateObject("ADODB.RecordSet")
	rsProdInput.Open "sfProducts", cnn, adOpenDynamic, adLockOptimistic, adCmdTable
	
	On Error Resume Next

	rsProdInput.AddNew
	rsProdInput.Fields("prodID") = prodID
	rsProdInput.Fields("prodCategoryId") = prodCategoryId
	rsProdInput.Fields("prodManufacturerId") = prodManufacturerId
	rsProdInput.Fields("prodVendorId") = prodVendorId
	rsProdInput.Fields("prodName") = prodName
	rsProdInput.Fields("prodNamePlural") = prodNamePlural
	rsProdInput.Fields("prodDescription") = prodDescription
	rsProdInput.Fields("prodShortDescription") = prodShortDescription
	rsProdInput.Fields("prodMessage") = prodMessage
	rsProdInput.Fields("prodImageSmallPath") = prodImageSmallPath
	rsProdInput.Fields("prodImageLargePath") = prodImageLargePath
	rsProdInput.Fields("prodLink") = prodLink
	rsProdInput.Fields("prodPrice") = prodPrice
	rsProdInput.Fields("prodWeight") = prodWeight
	rsProdInput.Fields("prodSalePrice") = prodSalePrice
	rsProdInput.Fields("prodSaleIsActive") = prodSaleIsAct
	rsProdInput.Fields("prodShip") = prodShip
	rsProdInput.Fields("prodShipIsActive") = prodShipIsAct
	rsProdInput.Fields("prodCountryTaxIsActive") = prodCountryTaxIsAct
	rsProdInput.Fields("prodStateTaxIsActive") = prodStateTaxIsAct
	rsProdInput.Fields("prodEnabledIsActive") = prodEnabledIsAct
	rsProdInput.Fields("prodAttrNum") = attNumber
	rsProdInput.Fields("prodDateAdded") = Now()
	rsProdInput.Update
	rsProdInput.Close
			
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
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>" onLoad="javascript:newCategory();newMFG();newVendor();">
<form method="post" name="formAddProduct" onSubmit="javascript:this.prodShip.number=true;this.prodSalePrice.number=true;this.prodWeight.number=true;this.prodPrice.number=true;this.prodDescription.optional=true;this.prodImageLargePath.optional=true;this.prodImageSmallPath.optional=true;this.prodLink.optional=true;this.prodSalePrice.optional=true;return addproductCheck(this)">
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
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>Use this interface to add products to your inventory for sale in your web.  You must specify a Product ID, Price, and Name for each product item, as well as a Category, Manufacturer, and Vendor.  You can find step-by-step instructions on using attributes and creating products in the StoreFront help files.</font></td>
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
If Request.Form("addproduct.x") <> "" And Err.number = 0 Then
%>
    <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center" bgcolor="<%= C_BGCOLOR3 %>">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b>Database Updated
				</font>
				<br><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>"><a href="menu.asp">Return to Menu</b></a>| <a href="m0009.asp">Add Another Product</a>
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
        <td width="50%"><input type="text" name="prodID" title="Product ID" value="<%= prodID %>" size="20" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Name:</font></td>
        <td width="50%"><input type="text" name="prodName" title="Product Name" value="<%= prodName %>" size="20" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Name Plural:</font></td>
        <td width="50%"><input type="text" name="prodNamePlural" title="Product Name Plural" value="<%= prodNamePlural %>" size="20" style="<%= C_FORMDESIGN %>"></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Category:</font></td>
        <td width="50%"><select size="1" name="prodCategoryId" style="<%= C_FORMDESIGN %>" onChange="javascript:newCategory()">
        <%
        If prodCategoryId <> "New" Then
			Response.write getCategoryList(prodCategoryId)
			selected = ""
		Else
			Response.write getCategoryList(0)
			selected = "selected"
			Cvalue= Request.Form("prodCategoryNewId")
		End If 
        %><option <%= selected %> value="New">New Category</select><input type="text" name="prodCategoryNewId" value="<%= Cvalue %>" style="<%= C_FORMDESIGN %>" size=18></input></td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product
          Manufacturer:</font></td>
        <td width="50%"><select size="1" name="prodManufacturerId" style="<%= C_FORMDESIGN %>" onChange="javascript:newMFG()">
        <%
        If prodManufacturerId <> "New" Then
            Response.write getManufacturersList(prodManufacturerId)
            selected = ""
        Else
			Response.Write getManufacturersList(0)
			selected = "selected"
			Mvalue = Request.Form("prodManufacturerNewId")
		End If
        %><option <%= selected %> value="New">New Manufacturer</select><input type="text" name="prodManufacturerNewId" value="<%= Mvalue %>" style="<%= C_FORMDESIGN %>" size=18></input></td>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product
          Vendor:</font></td>
        <td width="50%"><select size="1" name="prodVendorId" style="<%= C_FORMDESIGN %>" onChange="javascript:newVendor()">
        <%
        If prodVendorId <> "New" Then
            Response.write getVendorList(prodVendorId)
            selected = ""
        Else
			Response.Write getVendorList(0)
			selected = "selected"
			Vvalue = Request.Form("prodVendorNewId")
		End If
        %><option <%= selected %> value="New">New Vendor</select><input type="text" name="prodVendorNewId" value="<%= Vvalue %>" style="<%= C_FORMDESIGN %>" size=18></input></td>
          </select>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Price:</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="prodPrice" title="Product Price" value="<%= prodPrice%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right" valign="top" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Short Description :<br><font size="-1">(used on search.asp)</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><textarea rows="2" name="prodShortDescription" title="Product Short Description" cols="30" style="<%= C_FORMDESIGN %>"><%= prodShortDescription %></textarea>
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Long Description :<br><font size="-1">(used on detail.asp)</font></font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><textarea rows="4" name="prodDescription" cols="30" style="<%= C_FORMDESIGN %>"><%= prodDescription %></textarea>
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Confirmation Message:</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="prodMessage" title="Product Message" value="<%= prodMessage%>" size="30" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Link:</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="prodLink" value="<%= prodLink%>" size="30" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Product Weight:</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="text" name="prodWeight" title="Product Weight" value="<%= prodWeight%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="50%" align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Product:</font></td>
        <%If Request.Form("prodEnabledIsAct") = "" Then %>
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
        <%
		'Gathers Attribute information and displayes in correct HTML format
		addAttribute = Request.Form("AddAttribute.x")
		If addAttribute <> "" or addAttType = "Options" or Request.Form("actionType") = "Add Product" Then
			If attNumber = "" and Request.Form("actionType") <> "Add Product" Then 'First Instance of Attribute being added
		%>
		<tr><td colspan="2"><hr></td></tr><tr><td align="right" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 + 1 %>">Attribute Title: </td><td><input type="text" name="T1" size="15" style="<%= C_FORMDESIGN %>"></td></tr>
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
		<tr><td colspan="2"><hr></td></tr><tr><td align="right" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 + 1 %>">Attribute Title: </td><td><input type="text" name="T<%= counter %>" value="<%= Request.Form(tempVar) %>" size="15" style="<%= C_FORMDESIGN %>"></td></tr> 
		<%			
					counter2 = 1
					Do While Request.Form(tempVarName & counter2) <> "" 'Loop which gathers Attribute Options
		%>
		<tr><td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Option <%= counter2 %>: </td><td nowrap><input type="text" name="<%= tempVarName & counter2 %>" value="<%= Request.Form(tempVarName & counter2) %>" size="10" style="<%= C_FORMDESIGN %>">
		<% 
						choice = Request.Form(tempVarSet & counter2)
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
						
						If choice = "0" OR Request.Form(tempVarPrice & counter2) = "" Then
							tempVar3 = "0"
						Else
							tempVar3 = Request.Form(tempVarPrice & counter2)
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
					<tr><td align="right" nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"> Option <%= counter2 %>: </td>
					<td nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
					<input type="text" name="<%= tempVarName & counter2 %>" size="10" style="<%= C_FORMDESIGN %>">
					<input type="radio" checked name="<%= tempVarSet & counter2 %>" value="0">Same Price
					<input type="radio" name="<%= tempVarSet & counter2 %>" value="1">Increase
					<input type="radio" name="<%= tempVarSet & counter2 %>" value="2">Decrease by: 
					<input type="text" name="<%= tempVarPrice & counter2 %>" size="3" style="<%= C_FORMDESIGN %>"></td></tr>
					<tr><td align="center" colspan="2"><input type="submit" name="addAttType" value="Options"></td></tr>
					<input type="hidden" name="attNumber" value="<%= counter-1 %>">
		<%
				ElseIf addAttribute <> "" Then
		%>
					<tr><td colspan="2"><hr></td></tr>
					<tr><td align="right" noWrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Attribute Title: </td>
					<td><input type="text" name="T<%=  counter %>" size="15" style="<%= C_FORMDESIGN %>"></tr></td>
					<tr><td align="center" colspan="2">
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
        <td width="50%"><font face="Verdana" size="2">Product Small Image<br>
          <input type="text" name="prodImageSmallPath" value="<%= prodImageSmallPath%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        <td width="50%"><font face="Verdana" size="2">Product Large Image<br>
          <input type="text" name="prodImageLargePath" value="<%= prodImageLargePath%>" size="20" style="<%= C_FORMDESIGN %>">
        </font>
        </td>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Sale Options</font></b></td>
        </tr>
        <tr>
        <%If Request.Form("prodSaleIsAct") = "1" Then 'checks the correct option%>
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
        <%If Request.Form("prodShipIsAct") = "1" Then 'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodShipIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodShipIsAct" value="1">
		<%End If%>
		<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Shipping&nbsp;</font></td>
        <td width="50%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Shipping Cost<br>
          <input type="text" name="prodShip" value="<%= prodShip%>" size="20" style="<%= C_FORMDESIGN %>">
          </font></td>
        </tr>
        <tr>
        <td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Product Tax Options</font></b></td>
        </tr>
        <tr>
        <%If Request.Form("prodStateTaxIsAct") = "1" Then'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodStateTaxIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodStateTaxIsAct" value="1">
		<%End If%>
		<font face="Verdana" size="2">Activate State Tax</font></td>
        <%If Request.Form("prodCountryTaxIsAct") = "1" Then'checks the correct option%>
			<td width="50%"><input type="checkbox" name="prodCountryTaxIsAct" value="1" checked>
		<%Else%>
			<td width="50%"><input type="checkbox" name="prodCountryTaxIsAct" value="1">
		<%End If%> 
		<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Activate Country Tax</font></td>
        </tr>
        <tr>
		<td align="center" width="100%" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>"><input type="image" name="addproduct" src="images/addproduct.gif" border="0" WIDTH="108" HEIGHT="21" onClick="javascript:addProductClicked()"></td>
		</tr>
        </table><br>
    </td>
    </tr>
	    <tr>
		<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="m0009.asp">Add Another Product</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
        </tr>
</table>
</td>
</tr>
</table>
</form>
</body>

</html>

