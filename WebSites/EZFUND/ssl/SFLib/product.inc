<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System     :   StoreFront 2000										'
'   Author     :   LaGarde, Incorporated								'
'   Notes      :   There are no configurable elements in this file.		'
'																		'
'                         COPYRIGHT NOTICE								'
'																		'
'   The contents of this file is protected under the United States		'
'   copyright laws as an unpublished work, and is confidential and		'
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in		'
'   whole or in part without the expressed written permission of		'
'   LaGarde, Incorporated is expressely prohibited.						'
'																		'
'   (c) Copyright 1999 by LaGarde, Incorporated.  All rights reserved.	'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%><%	

Function GetProductInfo(ProdID, Action)

	Dim DSN_Name
	DSN_Name = Session("DSN_Name")
	set Connection = Server.CreateObject("ADODB.Connection")
	Connection.Open DSN_Name

	session.LCID = Session("LCID")
	
	SQL = "SELECT * FROM product WHERE PRODUCT_ID = '" & ProdID &"' "
	RSProdInfo = Connection.Execute(SQL)
	
	If Action = "0" Then
	GetProductInfo = RSProdInfo("LONG_DESCRIPTION")
	ElseIf Action = "1" Then 
	GetProductInfo = RSProdInfo("DESCRIPTION")
	ElseIf Action = "2" Then
	GetProductInfo = RSProdInfo("PRICE")
	ElseIf Action = "3" Then
	GetProductInfo = RSProdInfo("Category")
	ElseIf Action = "4" Then 
	GetProductInfo = RSProdInfo("MFG")
	ElseIf Action = "5" Then
	GetProductInfo = RSProdInfo("IMAGE_PATH")
	ElseIf Action = "6" Then
	GetProductInfo = RSProdInfo("AttributeA")
	ElseIf Action = "7" Then
	GetProductInfo = RSProdInfo("AttributeB")
	ElseIf Action = "8" Then 
	GetProductInfo = RSProdInfo("AttributeC")	
	End If
Set RSProdInfo = nothing
Connection.Close
Set Connection = nothing

End Function

%>

