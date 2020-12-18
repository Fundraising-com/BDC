<%

' PaymentInclude.asp -- payment-type functions for general use, including credit card functions.

' NOTE: this page is adapted from PaymentInclude.asp on the CCE site.
' Some of the functions etc. are not relevant on EZFund.
' Someday, we'll clean this up!

' Cache of Payment Type table
Const ptPtypCde = 1
Const ptPTypTxt = 2
Const ptPtypCcrdFlg = 3
Const ptColCount = 3
Dim sPaymentTypes()		' ptColCount x #rows
Dim nPaymentTypes		' #rows in sPaymentTypes()
nPaymentTypes = 0
Dim sAllPaymentTypes	' string of all payment type codes, space-sep
sAllPaymentTypes = ""

Function PaymentTypeIndex(ByVal sPtypCde)
	' Look up a payment type; return its position in the cache.
	Dim iPaymentType
	If nPaymentTypes = 0 Then Call CachePaymentTypes()
	For iPaymentType = 1 To nPaymentTypes
		If sPaymentTypes(ptPtypCde, iPaymentType) = sPtypCde Then
			PaymentTypeIndex = iPaymentType
			Exit Function
		End if
	Next
	PaymentTypeIndex = 0
End Function

Function PaymentTypeText(ByVal sPtypCde)
	Dim iPaymentType
	iPaymentType = PaymentTypeIndex(sPtypCde)
	If iPaymentType > 0 Then
		PaymentTypeText = sPaymentTypes(ptPTypTxt, iPaymentType)
	Else
		PaymentTypeText = ""
	End If
End Function

Function PaymentTypeIsCreditCard(ByVal sPtypCde)
	Dim iPaymentType
	iPaymentType = PaymentTypeIndex(sPtypCde)
	If iPaymentType > 0 Then
		PaymentTypeIsCreditCard = sPaymentTypes(ptPtypCcrdFlg, iPaymentType)
	Else
		PaymentTypeIsCreditCard = False
	End If
End Function

Function AnyPaymentTypeIsCreditCard(ByVal sPtypCdes)
	Dim sPtyp, iPos
	sPtypCdes = UCase(Trim(sPtypCdes))
	Do While sPTypCdes <> ""
		iPos = InStr(sPtypCdes, " ")
		If iPos > 0 Then
			sPtyp = Left(sPtypCdes, iPos - 1)
			sPtypCdes = Mid(sPtypCdes, iPos + 1)
		Else
			sPtyp = sPtypCdes
			sPtypCdes = ""
		End If
		If PaymentTypeIsCreditCard(sPtyp) Then
			AnyPaymentTypeIsCreditCard = True
			Exit Function
		End If
	Loop
	AnyPaymentTypeIsCreditCard = False
End Function

Function CachePaymentTypes()

	' Populate the cache of payment types from the lookup table

	Dim RS
	Dim SQLStmt
	Dim bOpenCloseEZLeadsDB
	Dim bOK

	bOpenCloseEZLeadsDB = (objEZLeadsDB Is Nothing)

	bOK = True

	If bOK Then
		If bOpenCloseEZLeadsDB Then
			If OpenEZLeadsDB() = False Then
				sUserFeedbackMessage = "Unable to open order database - 1a"
				bOK = False
			End If
		End If
	End If

	' Cache all payment types for easy future use
	If bOK Then
		SQLStmt = "Select * From PTYP_LKUP_TBL Order By PTYP_SEQ_NBR"
		If CreateEZLeadsRS(RS, SQLStmt) Then
			If CheckRS(RS) Then
				sAllPaymentTypes = ""
				ReDim sPaymentTypes(ptColCount, 0)
				nPaymentTypes = 0
				Do While Not RS.EOF
					nPaymentTypes = nPaymentTypes + 1
					ReDim Preserve sPaymentTypes(ptColCount, nPaymentTypes)
					sPaymentTypes(ptPtypCde, nPaymentTypes) = RS("PTYP_CDE")
					sPaymentTypes(ptPtypTxt, nPaymentTypes) = RS("PTYP_TXT")
					sPaymentTypes(ptPtypCcrdFlg, nPaymentTypes) = CBool(RS("PTYP_CCRD_FLG"))
					sAllPaymentTypes = sAllPaymentTypes & " " & RS("PTYP_CDE")
					RS.MoveNext
				Loop
				sAllPaymentTypes = sAllPaymentTypes & " "
			End If
		End If
		If Not RS Is Nothing Then
			On Error Resume Next
			RS.Close
			Set RS = Nothing
		End If
	End If

	If bOpenCloseEZLeadsDB Then
		On Error Resume Next
		Call CloseEZLeadsDB()
	End If

End Function

Function PaymentTypesForCompany(ByVal nCompNtrnIdntNbr, ByVal sPtypFltrCde)
	' Return a space-sep string of payment type codes for the specified company,
	' filtered by the specified filter code (e.g., RETL)
	
	Dim RS
	Dim SQLStmt
	Dim bOpenCloseEZLeadsDB
	Dim bOK

	Dim sPTyps

	bOpenCloseEZLeadsDB = (objEZLeadsDB Is Nothing)

	bOK = True
	sPTyps = ""

	If bOK Then
		If bOpenCloseEZLeadsDB Then
			If OpenEZLeadsDB() = False Then
				sUserFeedbackMessage = "Unable to open database - 1a"
				bOK = False
			End If
		End If
	End If

	' Collect payment types for this company
	If bOK Then
		SQLStmt = "Select COMP_PROF_PTYP_TBL.PTYP_CDE " & _
					" From COMP_PROF_PTYP_TBL Inner Join PTYP_LKUP_TBL " & _
					" On COMP_PROF_PTYP_TBL.PTYP_CDE = PTYP_LKUP_TBL.PTYP_CDE " & _
					" Where COMP_PROF_PTYP_TBL.COMP_NTRN_IDNT_NBR=" & nCompNtrnIdntNbr & " " & _
					" And COMP_PROF_PTYP_TBL.PTYP_FLTR_CDE='" & sPtypFltrCde & "' " & _
					" Order By PTYP_LKUP_TBL.PTYP_SEQ_NBR"
		If CreateEZLeadsRS(RS, SQLStmt) Then
			If CheckRS(RS) Then
				sPTyps = ""
				nPaymentTypes = 0
				Do While Not RS.EOF
					sPTyps = sPTyps & " " & RS("PTYP_CDE")
					RS.MoveNext
				Loop
				sPTyps = sPTyps & " "
			End If
		End If
		If Not RS Is Nothing Then
			On Error Resume Next
			RS.Close
			Set RS = Nothing
		End If
	End If

	If bOpenCloseEZLeadsDB Then
		On Error Resume Next
		Call CloseEZLeadsDB()
	End If
	
	' For now, if company is not listed, assume all payment types
	' WRITE THIS! IS THIS CORRECT?
	If Trim(sPTyps) = "" Then
		If nPaymentTypes = 0 Then Call CachePaymentTypes()
		sPTyps = sAllPaymentTypes
	End If

	PaymentTypesForCompany = sPTyps

End Function

Function PaymentTermsForCompany(Byval nCompNtrnIdntNbr)
	' Return payment terms for the specified company
	
	Dim RS
	Dim SQLStmt
	Dim bOpenCloseEZLeadsDB
	Dim bOK

	bOpenCloseEZLeadsDB = (objEZLeadsDB Is Nothing)

	bOK = True
	PaymentTermsForCompany = ""

	If bOK Then
		If bOpenCloseEZLeadsDB Then
			If OpenEZLeadsDB() = False Then
				sUserFeedbackMessage = "Unable to open database - 1a"
				bOK = False
			End If
		End If
	End If

	' Collect payment types for this company
	If bOK Then
		SQLStmt = "Select TERM_TXT " & _
					" From COMP_PROF_TERM_TBL " & _
					" Where COMP_NTRN_IDNT_NBR=" & nCompNtrnIdntNbr & " " 
		If CreateEZLeadsRS(RS, SQLStmt) Then
			If CheckRS(RS) Then
				PaymentTermsForCompany = RS("TERM_TXT")
			End If
		End If
		If Not RS Is Nothing Then
			On Error Resume Next
			RS.Close
			Set RS = Nothing
		End If
	End If

	If bOpenCloseEZLeadsDB Then
		On Error Resume Next
		Call CloseEZLeadsDB()
	End If
	
End Function

Function EmitPaymentTypeComboBox(theName, theDefault, thePTypOrder)
	' Return a complete SELECT for standard payment types (check, money order, various credit cards).
	' There will be an empty entry at the top, so "none of the below" can be preselected.
	' Pass theDefault = "" or one of the supported payment type codes
	' Pass thePTypOrder = a space-sep list of payment types 
	'			to determine which payment types in what order.
	' Pass thePTypOrder = "" 
	'			to get empty item first, then all payment types in order.
	' If a default is specified, it is selected. 
	' If not, the empty entry at the beginning is selected.

	Dim iPos, sThisPTyp, sDefault, sSelected, sItem, sPTyps

	If nPaymentTypes = 0 Then Call CachePaymentTypes()

	If thePTypOrder = "" Then 
		sPTyps = sAllPaymentTypes
	Else
		sPTyps = thePTypOrder
	End If
	sPTyps = UCase(Trim(sPTyps))
	sDefault = UCase(Trim(theDefault))

	Response.Write vbCrLf & "<select name=""" & theName & """>" & vbCrLf

	' The empty item at the top
	If sDefault = "" Then
		sSelected = " selected"
	Else
		sselected = ""
	End If
	Response.Write "<option value=""""" & sSelected & ">" & vbCrLf ' no text

	' List items for each specified payment type
	Do
		iPos = InStr(sPTyps, " ")
		If iPos > 0 Then
			sThisPTyp = Left(sPTyps, iPos - 1)
			sPTyps = Mid(sPTyps, iPos + 1)
		Else
			sThisPTyp = sPTyps
			sPTyps = ""
		End If
		
		If sThisPTyp <> "" Then
			If sThisPTyp = sDefault Then
				sSelected = " selected"
			Else
				sSelected = ""
			End If
			sItem = "<option value=""" & sThisPTyp & """" & sSelected & ">" & PaymentTypeText(sThisPTyp)
			Response.Write sItem & vbCrLf
		End If
	Loop While sThisPTyp <> ""

	Response.Write "</select>" & vbCrLf
	
End Function

Function EmitPaymentTypeRadioButtons(theName, theDefault, thePTypOrder)
	Call EmitPaymentTypeCheckRadioButtons(theName, theDefault, thePTypOrder, "radio")
End Function

Function EmitPaymentTypeCheckBoxes(theName, theDefault, thePTypOrder)
	Call EmitPaymentTypeCheckRadioButtons(theName, theDefault, thePTypOrder, "checkbox")
End Function

Function EmitPaymentTypeCheckRadioButtons(theName, theDefault, thePTypOrder, sElementType)
	' Return a complete set of 
	'	INPUT TYPE=RADIO buttons (sElementType="radio")
	' or
	'	INPUT TYPE=CHECKBOX boxes (sElementType="checkbox")
	' for standard payment types (check, money order, various credit cards).
	' Pass theDefault = "" or 
	'	(for "radio") one of the supported payment type codes
	'	(for "checkbox") one or more of the supported payment type codes, space-sep.
	' Pass thePTypOrder = a space-sep list of payment types 
	'			to determine which payment types in what order.
	' Pass thePTypOrder = "" 
	'			to get all payment types in order.
	' Pass sElementType = "radio" or "checkbox"
	' If a default is specified, it is selected. 
	' If not, nothing is selected.

	Const cButtonCols = 3

	Dim iPos, sThisPTyp, sDefault, sChecked, sItem, sPTyps, iButton
	Dim sCheckboxDefault, sThisCheckboxPTyp

	If nPaymentTypes = 0 Then Call CachePaymentTypes()

	If thePTypOrder = "" Then 
		sPTyps = sAllPaymentTypes
	Else
		sPTyps = thePTypOrder
	End If
	sPTyps = UCase(Trim(sPTyps))
	sDefault = UCase(Trim(theDefault))

	' List items for each specified payment type
	iButton = 0
	Response.Write "<table border=0 cellspacing=1>"
	Do
		iPos = InStr(sPTyps, " ")
		If iPos > 0 Then
			sThisPTyp = Left(sPTyps, iPos - 1)
			sPTyps = Mid(sPTyps, iPos + 1)
		Else
			sThisPTyp = sPTyps
			sPTyps = ""
		End If
		
		If sThisPTyp <> "" Then
			iButton = iButton + 1
			If iButton Mod cButtonCols = 1 Then Response.Write "<tr>"
			Select Case LCase(sElementType)
				Case "radio"
					' Default is a single value
					If sThisPTyp = sDefault Then
						sChecked = " checked"
					Else
						sChecked = ""
					End If
				Case "checkbox"
					' Default is multiples, comma-sep (actually comma-space-sep)
					sCheckboxDefault = " " & ReplaceAll(sDefault, ",", " ") & " "
					sThisCheckboxPTyp =  " " & Trim(sThisPTyp) & " "
					If InStr(sCheckboxDefault, sThisCheckboxPTyp) > 0 Then
						sChecked = " checked"
					Else
						sChecked = ""
					End If
			End Select
			sItem = "<input type=""" & sElementType & """ name=""" & theName & """ value=""" & sThisPTyp & """" & sChecked & ">" & _
								HTFont(False, dfltFontFace & " size=1") & PaymentTypeText(sThisPTyp) & HTFontEnd()
			Response.Write "<td>" & sItem & "</td>" & vbCrLf
			If iButton Mod cButtonCols = 0 Then Response.Write "</tr>"
		End If
	Loop While sThisPTyp <> ""
	If iButton Mod cButtonCols <> 0 Then Response.Write "</tr>"
	Response.Write "</table>"

End Function

Dim CreditCardNumberErrorMessage	' set if not legal for type; see below

Function CreditCardNumberIsLegalForType(sCCNumber, sCCType)

	' This facilitates debug on the non-secure development site
	If LCase(Request.ServerVariables("SERVER_NAME")) = "rdezf.atchou.com" Then
		If sCCNumber = "1234-1234-1234-1234" Then
			CreditCardNumberIsLegalForType = True
			Exit Function
		End If
	End If

	' Check that sCCNumber is a legal credit card number of type sCCType.
	' Pass
	'	sCCNumber	the credit card number to test (dashes etc. are OK)
	'	sCCType		a payment type code for which the credit card flag is true

	' NOTE: this does NOT "validate" the credit card, i.e., check if it is a
	' number of a valid existing card. It only assures you it's a LEGAL value
	' for a number for that type of card
	Dim iRet, sSep

	CreditCardNumberErrorMessage = ""
	sSep = ""

	iRet = CCValidationCheckCC(sCCNumber, sCCType)

	If CBool(iRet And 1) Then
		CreditCardNumberErrorMessage = CreditCardNumberErrorMessage & sSep & "Wrong credit card type" & sSep
		sSep = ", "
	End If

	If CBool(iRet And 2) Then
		CreditCardNumberErrorMessage = CreditCardNumberErrorMessage & sSep & "Wrong length for credit card number" & sSep
		sSep = ", "
	End If

	If CBool(iRet And 4) Then
		CreditCardNumberErrorMessage = CreditCardNumberErrorMessage & sSep & "Wrong checksum for credit card number" & sSep
		sSep = ", "
	End If

	If CBool(iRet And 8) Then
		CreditCardNumberErrorMessage = CreditCardNumberErrorMessage & sSep & "Unknown credit card type"
		sSep = ", "
	End If

	If iRet <> 0 And CreditCardNumberErrorMessage = "" Then
		CreditCardNumberErrorMessage = CreditCardNumberErrorMessage & sSep & "Unknown error checking credit card"
		sSep = ", "
	End If

	CreditCardNumberIsLegalForType = (iRet = 0)

End Function


''''''''''''''''''''''''''''''
' Credit Card check routine for ASP
' (c) 1998 by Click Online
' You may use these functions only if this header is not removed
' http://www.click-online.de
' info@click-online.de

' MODIFIED 2/2001 (SSB):
' - cleaned up source, declared variables.
' - renamed functions to avoid namespace collisions
' - allowed our abbreviations in the cctype argument

Function CCValidationCheckCC(ccnumber, cctype)
	' PRIVATE USE, do not call from your source!

	'	Checks credit card number For checksum,length and type.

	'	ccnumber =  credit card number (all useless characters are
	'		being removed before check)
	'
	'	cctype:
	'       "V" VISA
	'       "M" Mastercard/Eurocard
	'       "A" American Express
	'       "C" Diners Club / Carte Blanche
	'       "D" Discover
	'       "E" enRoute
	'       "J" JCB

	'	returns:
	'			CCValidationCheckCC = 0 (Bit0)  : card valid
	'			CCValidationCheckCC = 1 (Bit1) : wrong type
	'			CCValidationCheckCC = 2 (Bit2) : wrong length
	'			CCValidationCheckCC = 4 (Bit3) : wrong checksum (MOD10-Test)
	'			CCValidationCheckCC = 8 (Bit4) : cardtype unknown

	Dim ctype, cclength, ccprefix
	Dim prefix, prefixes, prefixvalid
	Dim length, lengths, lengthvalid
	Dim number
	Dim result
	Dim sum, qsum
	Dim x, ch

	ctype = UCase(cctype)
	Select Case ctype
		Case "V", "VISA"
			cclength = "13;16"
			ccprefix = "4"
		Case "M", "MC"
			cclength = "16"
			ccprefix = "51;52;53;54;55"
		Case "A", "AMEX"
			cclength = "15"
			ccprefix = "34;37"
		Case "C"
			cclength = "14"
			ccprefix = "300;301;302;303;304;305;36;38"
		Case "D", "DISC"
			cclength = "16"
			ccprefix = "6011"
		Case "E"
			cclength = "15"
			ccprefix = "2014;2149"
		Case "J"
			cclength = "15;16"
			ccprefix = "3;2131;1800"
		Case Else
			cclength = ""
			ccprefix = ""
	End Select

	prefixes = Split(ccprefix, ";", -1)
	lengths = Split(cclength, ";", -1)

	number = CCValidationTrimToDigits(ccnumber)

	prefixvalid = False
	lengthvalid = False
	For Each prefix In prefixes
	  If InStr(number, prefix) = 1 Then
	    prefixvalid = True
	  End If
	Next  
	For Each length In lengths
	  If CStr(Len(number)) = length Then
	    lengthvalid = True
	  End If
	Next

	result = 0
	If Not prefixvalid Then
	  result = result + 1
	End If  
	If Not lengthvalid Then
	  result = result + 2
	End If  

	qsum = 0
	For x = 1 To Len(number)

	  ch = Mid(number, Len(number) - x + 1,1)
	  'Response.Write ch ' DEBUG WRITE THIS REMOVE THIS

	  If x Mod 2 = 0 Then
	    sum = 2 * CInt(ch)
	    qsum = qsum + (sum Mod 10)
	    If sum > 9 Then 
	      qsum = qsum + 1
	    End If
	  Else
	    qsum = qsum + CInt(ch)
	  End If

	Next

	'response.write qsum  ' DEBUG WRITE THIS REMOVE THIS

	If qsum Mod 10 <> 0 Then
	  result = result + 4
	End If
	If cclength = "" Then
	  result = result + 8
	End If

	CCValidationCheckCC = result

End Function

Function CCValidationTrimToDigits(tstring)
	' PRIVATE USE, do not call from your source!

	' Removes all chars except of 0-9

	Dim s, ts, x, ch

	s = "" 
	ts = tstring
	For x = 1 To Len(ts)
		ch = Mid(ts, x, 1)
		If Asc(ch) >= 48 and Asc(ch) <= 57 Then
			s = s & ch
		End If
	Next

	CCValidationTrimToDigits = s

End Function

' END OF Credit Card check routine for ASP
' (c) 1998 by Click Online
''''''''''''''''''''''''''''''

Function CreditCardExpireDateIsValid(sCCExpires)

	' Check that sCCExpires is a legal card expiration date of form 
		' m/yy or mm/yy or m/yyyy or mm/yyyy
	' or the equivalent with "-" instead of "/"
	' NOTE: we don't test that the date is before today. Seller can see that.

	Dim iPos, sMonth, sYear

	If IsDate(sCCExpires) Then
		' Nonstandard to use full date, but allow it
		CreditCardExpireDateIsValid = True
		Exit Function
	End If

	iPos = Instr(sCCExpires, "/")
	If iPos = 0 Then iPos = Instr(sCCExpires, "-")
	If iPos = 0 Then
		CreditCardExpireDateIsValid = False
		Exit Function
	End If

	sMonth = Left(sCCExpires, iPos - 1)
	sYear = Mid(sCCExpires, iPos + 1)

	CreditCardExpireDateIsValid = IsDate(sMonth & "/1/" & sYear)
	
End Function

%>