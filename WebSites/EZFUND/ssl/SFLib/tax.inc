<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''	'
'   System     :   StoreFront 2000	Version 4.02.7				'	
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
'   (c) Copyright 1999 by LaGarde, Incorporated.  All rights reserved.		'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''	'
%>
<%	


	
	
	SQL = "SELECT TOTAL AS CTaxTotal FROM orders, product WHERE (orders.PRODUCT_ID = "_
	& "product.PRODUCT_ID and product.CTax = '1' AND ORDERS.ORDER_ID = " & ORDER_ID & ")"
	Set RSCTaxSubTotal = Connection.Execute(SQL)

	
If Not (RSCTaxSubTotal.EOF AND RSCTaxSubTotal.BOF) Then
RSCTaxSubTotal.MoveFirst
CurrentRecord = 0
Do While NOT RSCTaxSubTotal.EOF
	CTaxTotal = CCur(RSCTaxSubTotal("CTaxTotal"))+CTaxTotal
	
RSCTaxSubTotal.MoveNext
CurrentRecord = CurrentRecord = 1
Loop
CTaxTotal = CCur(CTaxTotal)
End If
Set RSCTaxSubTotal = nothing

	SQL = "SELECT TOTAL AS STaxTotal FROM orders, product WHERE (orders.PRODUCT_ID = "_
	& "product.PRODUCT_ID and product.STax = '1' AND ORDERS.ORDER_ID = " & ORDER_ID & ")"
	Set RSSTaxSubTotal = Connection.Execute(SQL)
	
If Not (RSSTaxSubTotal.EOF AND RSSTaxSubTotal.BOF) Then	
RSSTaxSubTotal.MoveFirst
CurrentRecord = 0
Do While NOT RSSTaxSubTotal.EOF

	STaxTotal = CCur(RSSTaxSubTotal("STaxTotal"))+STaxTotal
	
RSSTaxSubTotal.MoveNext
CurrentRecord = CurrentRecord = 1
Loop
STaxTotal = STaxTotal
End If
Set RSSTaxSubTotal = nothing


Function CountryTax()

	If SHIP_COUNTRY <> "" Then
	AppTaxCountry = SHIP_COUNTRY
	Else
	AppTaxCountry = Country
	End If



	SQL = "SELECT CTax FROM locales WHERE COUNTRY_ABB = '" & AppTaxCountry & "' AND " _
		& "ACTIVE = '1' "
	Set CTaxRate = Connection.Execute(SQL)

	
	If Not (CTaxRate.BOF AND CTaxRate.EOF) Then
	If Not IsNull(CTaxRate("CTax")) AND Trim(CTaxRate("CTax")) <> "" Then
	If IsNumeric(CTaxRate("CTax")) Then
	
		CTax = CDbl(CTaxRate("CTax"))

	End If
	End If
	End If
	Set CTaxRate = nothing

	If CTax <> "" Then

	   If Trim(TaxShip) = "1" Then
	         If PrmShip = "1" Then
	            RevSubTotal = CTaxTotal + XValAmt(SpShipping)
	         Else 
		  			
	            RevSubTotal = CTaxTotal + XValAmt(StdShipping)

	         End If
	     CountryTax = (CTax*RevSubTotal)

	Else 

	      CountryTax = (CTax*CTaxTotal)
	   End If

	Else
	   CountryTax = 0
	End If
	CTax = CountryTax

	If CTax > 0 Then
	 CTax = CTax
	Else 
	 CTax = 0
	End If
	

End Function


Function StateTax()

	If SHIP_STATE <> "" Then
	AppTaxState = SHIP_STATE
	Else
	AppTaxState = State
	End If

	SQL = "SELECT STax FROM locales WHERE STATE_ABB = '" & AppTaxState & "' AND "_
		& "ACTIVE = '1' "
	Set STaxRate = Connection.Execute(SQL)

	If Not (STaxRate.EOF AND STaxRate.BOF) Then
	If Not IsNull(STaxRate("STax")) AND Trim(STaxRate("STax")) <> "" Then
	If IsNumeric(STaxRate("STax")) Then

		STax = STaxRate("STax")

	End If
	End If
	End If

	Set STaxRate = nothing

	If STax <> "" Then

	   If TaxShip = "1" Then
	         If PrmShip = "1" Then
	            RevSubTotal = STaxTotal+XValAmt(SpShipping)
	         Else 
	            RevSubTotal = STaxTotal+XValAmt(StdShipping)
	         End If
	      StateTax = (STax*RevSubTotal)
	   Else 
	      StateTax = (STax*STaxTotal)
	   End If
	Else
	   StateTax = 0
	End If
	
	STax = StateTax


	If STax > 0 Then
	 STax = STax
	Else
	 STax = 0
	End If

End Function

%>
