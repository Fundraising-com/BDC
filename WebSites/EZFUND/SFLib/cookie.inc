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
%>
<%
	If not RSPrevOrd.EOF or RSPrevOrd.BOF Then
On Error Resume Next
	P_CUST_NAME = RSPrevOrd("CARD_NAME")
	P_COMPANY = RSPrevOrd("COMPANY")
	P_ADDRESS_1 = RSPrevOrd("ADDRESS_1")
	P_ADDRESS_2 = RSPrevOrd("ADDRESS_2")
	P_CITY = RSPrevOrd("CITY")
	P_STATE = RSPrevOrd("STATE")
	P_COUNTRY = RSPrevOrd("COUNTRY")
	P_ZIP = RSPrevOrd("ZIP")
	P_PHONE = RSPrevOrd("PHONE")
	P_FAX = RSPrevOrd("FAX")
	P_EMAIL = RSPrevOrd("E_MAIL")
	P_SHIP_NAME = RSPrevOrd("SHIP_NAME")
	P_SHIP_COMPANY = RSPrevOrd("SHIP_COMPANY")
	P_SHIP_ADDRESS_1 = RSPrevOrd("SHIP_ADDRESS_1")
	P_SHIP_ADDRESS_2 = RSPrevOrd("SHIP_ADDRESS_2")
	P_SHIP_CITY = RSPrevOrd("SHIP_CITY")
	P_SHIP_STATE = RSPrevOrd("SHIP_STATE")
	P_SHIP_COUNTRY = RSPrevOrd("SHIP_COUNTRY")
	P_SHIP_ZIP = RSPrevOrd("SHIP_ZIP")
	P_SHIP_TELEPHONE = RSPrevOrd("SHIP_TELEPHONE")
	Set RSPrevOrd = nothing
	End If
	
%>