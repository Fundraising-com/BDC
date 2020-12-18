<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System     :   StoreFront 2000						'
'   Author     :   LaGarde, Incorporated					'
'   Notes      :   There are no configurable elements in this file.		'
'										'
'                         COPYRIGHT NOTICE					'
'										'
'   The contents of this file is protected under the United States		'
'   copyright laws as an unpublished work, and is confidential and		'
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in		'
'   whole or in part without the expressed written permission of		'
'   LaGarde, Incorporated is expressely prohibited.				'
'										'
'   (c) Copyright 1999 by LaGarde, Incorporated.  All rights reserved.	'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%>
<%

	SQL = "SELECT * FROM design"
	Set RSDesign = Connection.Execute(SQL)
	
	
	'font stuff ***************************************
	FontColor = Trim(RSDesign("FONT_COLOR"))
	FontSize = Trim(RSDesign("FONT_SIZE"))
	FontType1 = Trim(RSDesign("FONT_TYPE_PRIMARY"))
	FontType2 = Trim(RSDesign("FONT_TYPE_SECONDARY"))
	FontStyle = Trim(RSDesign("FONT_STYLE"))
	
	'table stuff **************************************
	BorderColor = Trim(RSDesign("TABLE_BORDER_COLOR"))
	BorderSize = Trim(RSDesign("TABLE_BORDER_SIZE"))
	CellColor = Trim(RSDesign("TABLE_CELL_COLOR"))
	CellFontColor = Trim(RSDesign("TABLE_CELL_FONT_COLOR"))
	CellFontStyle = Trim(RSDesign("FONT_CELL_STYLE"))
	CellPadding = Trim(RSDesign("TABLE_CELL_PADDING"))
	CellSpacing = Trim(RSDesign("TABLE_CELL_SPACING"))
	TableWidth = Trim(RSDesign("TABLE_WIDTH"))
	TableAlign = Trim(RSDesign("TABLE_ALIGN"))
        If Trim(RSDesign("TABLE_BG_COLOR")) = "#XXXXXX" Then
        TableBG = ""
        Else
	TableBG = Trim(RSDesign("TABLE_BG_COLOR"))
        End If

	'label stuff **************************************
	lblCategory = Trim(RSDesign("lblCATEGORY"))
	lblCategoryActive = Trim(RSDesign("lblCATEGORY_ACTIVE"))
	lblManufacturer = Trim(RSDesign("lblMANUFACTURER"))
	lblManufacturerActive = Trim(RSDesign("lblMANUFACTURER_ACTIVE"))
	lblDescription = Trim(RSDesign("lblDESCRIPTION"))
	lblDescriptionActive = Trim(RSDesign("lblDESCRIPTION_ACTIVE"))
	lblLngDescriptionActive = Trim(RSDesign("lblLONG_DESCRIPTION_ACTIVE"))
	lblLngDescription = Trim(RSDesign("lblLONG_DESCRIPTION"))
	lblProductID = Trim(RSDesign("lblPRODUCT_ID"))
	lblProductIDActive = Trim(RSDesign("lblPRODUCT_ID_ACTIVE")) 
	lblPrice = Trim(RSDesign("lblPRICE"))
	lblPriceActive = Trim(RSDesign("lblPRICE_ACTIVE"))
	lblAttributeA = Trim(RSDesign("lblATTRIBUTE_A"))
	lblAttributeAActive = Trim(RSDesign("lblATTRIBUTE_A_ACTIVE"))
	lblAttributeB = Trim(RSDesign("lblATTRIBUTE_B"))
	lblAttributeBActive = Trim(RSDesign("lblATTRIBUTE_B_ACTIVE"))
	lblAttributeC = Trim(RSDesign("lblATTRIBUTE_C"))
	lblAttributeCActive = Trim(RSDesign("lblATTRIBUTE_C_ACTIVE"))
	lblLink = Trim(RSDesign("lblLINK"))
	lblLinkActive = Trim(RSDesign("lblLINK_ACTIVE"))
	lblImage = Trim(RSDesign("lblIMAGE"))
	lblImageActive = Trim(RSDesign("lblIMAGE_ACTIVE"))
	'finished collecting ******************************
	Set RSDesign = nothing	

lblCOUNT = 0
	If lblProductIDActive = "1" Then lblCount = lblCount + 1
	If lblCategoryActive = "1" Then lblCount = lblCount + 1
	If lblManufacturerActive = "1" Then lblCount = lblCount + 1
	If lblDescriptionActive = "1" Then lblCount = lblCount + 1
	If lblPriceActive = "1" Then lblCount = lblCount + 1
	If lblImageActive = "1" Then lblCount = lblCount + 1
	If lblLinkActive = "1" Then lblCount = lblCount + 1
	If lblAttributeAActive = "1" Then lblCount = lblCount + 1
	If lblAttributeBActive = "1" Then lblCount = lblCount + 1
	If lblAttributeCActive = "1" Then lblCount = lblCount + 1

%>