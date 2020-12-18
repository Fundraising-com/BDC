<%
'***** ACQUIRE SHIPPING VARIABLES **************************
If (CUST_ID <> "" AND ORDER_ID = "") Then
SHIP_ID = CUST_ID
Else
SHIP_ID = ORDER_ID
End If


	SQL = "SELECT (orders.TOTAL) AS ShipTotal, (orders.SHIP) as " _
	& "TtlShip, (orders.WEIGHT) AS TtlWt FROM orders INNER JOIN product ON orders.PRODUCT_ID = product.PRODUCT_ID WHERE " _
	& "(product.ShipSwitch = '1' AND "_
	& " ORDER_ID = " & SHIP_ID & ") "

	Set RSShipSubTotal = Connection.Execute(SQL)

If Not (RSShipSubTotal.EOF and RSShipSubTotal.BOF) Then
RSShipSubTotal.MoveFirst
CurrentRecord = 0
Do While NOT RSShipSubTotal.EOF

	ShipTotal = CCur(RSShipSubTotal("ShipTotal"))+ShipTotal
	WEIGHT = CDbl(RSShipSubTotal("TtlWt"))+WEIGHT
	TtlShip = CCur(RSShipSubTotal("TtlShip"))+TtlShip

RSShipSubTotal.MoveNext
CurrentRecord = CurrentRecord = 1
Loop


End If
set RSShipSubTotal = nothing

Function Shipping()
	ShipMeth = Trim(Replace(Request("ShipMeth"),"+", " "))
	ShipType = Trim(ShipType)
	If (ShipType = "2" And (ShipMeth <> "Standard Shipping" AND ShipMeth <> "Premium Shipping")) Then
	SQL = "SELECT CODE, RATES from shipping WHERE METHODS = '" & ShipMeth & "'"
	set RSShipMeth = Connection.Execute (SQL)
	ShipCode = RSShipMeth("CODE")
	ShipRate = RSShipMeth("RATES")
	set RSShipMeth = nothing



	If SHIP_COUNTRY <> "" Then
	dCountry = SHIP_COUNTRY
	Else
	dCountry = Country
	End If
	
	
	If SHIP_ZIP <> "" Then
	dZip = SHIP_ZIP
	Else
	dZip = ZIP
	End If


	If WEIGHT > 150 Then
	 Pkg = FormatNumber(WEIGHT/150,2)
	 TtlWt = 150
	Else
	 TtlWt = WEIGHT
	 Pkg = 1
	End If

			Set ups = Server.CreateObject("sfServer.UPS")
			ups.setDebug FALSE
			ups.setAction "3"
			ups.setProduct ShipCode
			ups.setContainer "00"
			ups.setWeight TtlWt
			ups.setOriginCountry oCountry
			ups.setOriginPostal oZip
			ups.setDestCountry dCountry
			ups.setDestPostal dZip
			ups.setResidential "1"
			ups.setRateChart "Regular Daily Pickup"
			ups.queryUPS()
			Shipping = ups.getTotalCharge()

			If (Shipping <> "FAIL" AND Shipping <> "ERROR" AND Not IsNull(Shipping) AND Shipping <> "TIMEOUT") Then
			Shipping = Shipping*ShipRate
			Shipping = FormatCurrency(Shipping)
			If WEIGHT > 150 THEN
			Shipping = Shipping*Pkg
			End If
	
			If CCur(ShipMin) => CCur(Shipping) then
			Shipping = ShipMin
			Else
			Shipping = Shipping
			End If
			
			ElseIf Shipping = "FAIL" OR Shipping = "ERROR" OR IsNull(Shipping) OR Shipping = "TIMEOUT" then
			ShipType = SecShipType
			End If
			Set ups = nothing
		
End If

If ShipType = "1" Then

		SQL = "SELECT * from valueshipping"
		Set RSShip = Connection.Execute (SQL)

		ShippingA = RSShip("SHIPPING_A")
		ShipAAmt = RSShip("SHIPA_AMOUNT")
		ShippingB = RSShip("SHIPPING_B")
		ShipABAmt = RSShip("SHIPAB_AMOUNT")
		ShippingC = RSShip("SHIPPING_C")
		ShipBCAmt = RSShip("SHIPBC_AMOUNT")
		ShippingD = RSShip("SHIPPING_D")
		ShipCDAmt = RSShip("SHIPCD_AMOUNT")
		ShippingE = RSShip("SHIPPING_E")
		ShipDEAmt = RSShip("SHIPDE_AMOUNT")
		ShippingF = RSShip("SHIPPING_F")
		ShipEFAmt = RSShip("SHIPEF_AMOUNT")
		ShipF_UpAmt = RSShip("SHIPF_UP_AMOUNT")
		SpShipAmt = RSShip("SPECIAL_SHIP_AMOUNT")
		SpShipAAmt = ShipAAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipBAmt = ShipABAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipCAmt = ShipBCAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipDAmt = ShipCDAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipEAmt = ShipDEAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipFAmt = ShipEFAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
		SpShipF_UpAmt = ShipF_upAmt+(RSShip("SPECIAL_SHIP_AMOUNT"))
Set RSShip = nothing

			If ShipTotal <= CCur(ShippingA) Then
				Shipping = ShipAAmt
			ElseIf (ShipTotal > CCur(ShippingA) AND ShipTotal <= CCur(ShippingB)) Then
				Shipping = ShipABAmt
			ElseIf (ShipTotal > CCur(ShippingB) AND ShipTotal <= CCur(ShippingC)) Then
				Shipping = ShipBCAmt
			ElseIf (ShipTotal > CCur(ShippingC) AND ShipTotal <= CCur(ShippingD)) Then
				Shipping = ShipCDAmt
			ElseIf (ShipTotal > CCur(ShippingD) AND ShipTotal <= CCur(ShippingE)) Then
				Shipping = ShipDEAmt
			ElseIf (ShipTotal > CCur(ShippingE) AND ShipTotal <= CCur(ShippingF)) Then
				Shipping = ShipEFAmt
			ElseIf ShipTotal > CCur(ShippingF) Then
				Shipping = ShipF_UpAmt
			End If
			
	If CCur(ShipMin) => CCur(Shipping) then
	  Shipping = ShipMin
	Else
	  Shipping = Shipping
	End If
	
End If

If ShipType = "3" Then

	If CCur(ShipMin) => CCur(TtlShip) Then
	  Shipping = ShipMin
	Else
	 Shipping = TtlShip
	End If
End If

End Function

Function PrmShipping()

	If ShipType = "1" then

		SQL = "SELECT * from valueshipping"
		Set RSShip = Connection.Execute (SQL)

		ShippingA = RSShip("SHIPPING_A")
		ShipAAmt = RSShip("SHIPA_AMOUNT")
		ShippingB = RSShip("SHIPPING_B")
		ShipABAmt = RSShip("SHIPAB_AMOUNT")
		ShippingC = RSShip("SHIPPING_C")
		ShipBCAmt = RSShip("SHIPBC_AMOUNT")
		ShippingD = RSShip("SHIPPING_D")
		ShipCDAmt = RSShip("SHIPCD_AMOUNT")
		ShippingE = RSShip("SHIPPING_E")
		ShipDEAmt = RSShip("SHIPDE_AMOUNT")
		ShippingF = RSShip("SHIPPING_F")
		ShipEFAmt = RSShip("SHIPEF_AMOUNT")
		ShipF_UpAmt = RSShip("SHIPF_UP_AMOUNT")
		SpShipAmt = RSShip("SPECIAL_SHIP_AMOUNT")
		SpShipAAmt = CCur(ShipAAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipBAmt = CCur(ShipABAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipCAmt = CCur(ShipBCAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipDAmt = CCur(ShipCDAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipEAmt = CCur(ShipDEAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipFAmt = CCur(ShipEFAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
		SpShipF_UpAmt = CCur(ShipF_upAmt)+(CCur(RSShip("SPECIAL_SHIP_AMOUNT")))
Set RSShip = nothing


If SpShipAmt > "" Then

			If ShipTotal <= CCur(ShippingA) Then
				SpShipping = SpShipAAmt
			ElseIf (ShipTotal > CCur(ShippingA) AND ShipTotal <= CCur(ShippingB)) Then
				SpShipping = SpShipBAmt
			ElseIf (ShipTotal > CCur(ShippingB) AND ShipTotal <= CCur(ShippingC)) Then
				SpShipping = SpShipCAmt
			ElseIf (ShipTotal > CCur(ShippingC) AND ShipTotal <= CCur(ShippingD)) Then
				SpShipping = SpShipDAmt
			ElseIf (ShipTotal > CCur(ShippingD) AND ShipTotal <= CCur(ShippingE)) Then
				SpShipping = SpShipEAmt
			ElseIf (ShipTotal > CCur(ShippingE) AND ShipTotal <= CCur(ShippingF)) Then
				SpShipping = SpShipEAmt
			ElseIf ShipTotal > CCur(ShippingF) Then
				SpShipping = SpShipF_UpAmt
			End If


	If SpShipAmt <> 0 Then
	If CCur(ShipMin) > CCur(SpShipping) then
	  PrmShipping = ShipMin
	Else
	  PrmShipping = SpShipping
	End If
	Else		
	PrmShipping = "0"
	End If
	End If

	ElseIf ShipType = "2" then



	ElseIf ShipType = "3" then
	
		SQL = "SELECT SPECIAL_SHIP_AMOUNT from valueshipping"
		set RSShip = Connection.Execute (SQL)
		SpShipAmt = RSShip("SPECIAL_SHIP_AMOUNT")
		set RSShip = nothing

		If SpShipAmt <> 0 Then
		PrmShipping = CCur(SpShipAmt)+CCur(TtlShip)
		Else
		PrmShipping = "0"
		End If


	End If
	
End Function

%>
