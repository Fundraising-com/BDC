<%

'*******************************************************************
'** AuthorizeNet Send Sub-Routine **********************************

sub AuthNetProc

	If MerchantType = "authonly" Then MType = "AO" End If
	If MerchantType = "authcapture" Then MType = "PA" End If
		
	If PAYMENT_METHOD = "ECHECK" Then
	CARD_TYPE = "ACH"
	End If
	
	ShipArray1 = Server.URLEncode(SHIP_ADDRESS_1&"@"&SHIP_ADDRESS_2&"@"&SHIP_ZIP&"@"&SHIP_TELEPHONE)
	ShipArray2 = Server.URLEncode(ShipMeth&"@"&PrmShip)
	
	'//***********FORMAT VARIABLES FOR AUTHORIZENET ********************
	DSN_NAME = Server.URLEncode(DSN_NAME)
	CARD_NAME = Server.URLEncode(CARD_NAME)
	ORDER_ID = Server.URLEncode(ORDER_ID)
	SHIP_MESSAGE = Server.URLEncode(SHIP_MESSAGE)
	
	'//************CUSTOMER VARIABLES *****************
	COMPANY = Server.URLEncode(COMPANY)
	ADDRESS_1 = Server.URLEncode(ADDRESS_1)
	ADDRESS_2 = Server.URLEncode(ADDRESS_2)
	CITY = Server.URLEncode(CITY)
	STATE = Server.URLEncode(STATE)
	COUNTRY = Server.URLEncode(COUNTRY)
	ZIP = Server.URLEncode(ZIP)
	PHONE = Server.URLEncode(PHONE)
	FAX = Server.URLEncode(FAX)
	E_MAIL = Server.URLEncode(E_MAIL)	
	'//***** PAYMENT VARIABLES *********************
	PAYMENT_METHOD = Server.URLEncode(PaymentMethod)
	CARD_TYPE = Server.URLEncode(CARD_TYPE)
	CARD_NO = Server.URLEncode(CARD_NO)
	CARD_EXP = Server.URLEncode(CARD_EXP)
	BANK_NAME = Server.URLEncode(BANK_NAME)
	ROUTING_NO = Server.URLEncode(ROUTING_NO)
	CHK_ACCT_NO = Server.URLEncode(CHK_ACCT_NO)
	PURCH_ORDER_NO = Server.URLEncode(PURCH_ORDER_NO)
	CHK_NO = Server.URLEncode(CHK_NO)	
	'//******** SHIPPING VARIABLES *******************
	SHIP_NAME = Server.URLEncode(SHIP_NAME)
	SHIP_COMPANY = Server.URLEncode(SHIP_COMPANY)
	SHIP_ADDRESS_1 = Server.URLEncode(SHIP_ADDRESS_1)
	SHIP_ADDRESS_2 = Server.URLEncode(SHIP_ADDRESS_2)
	SHIP_CITY = Server.URLEncode(SHIP_CITY)
	SHIP_STATE = Server.URLEncode(SHIP_STATE)
	SHIP_COUNTRY = Server.URLEncode(SHIP_COUNTRY)
	SHIP_ZIP = Server.URLEncode(SHIP_ZIP)
	SHIP_TELEPHONE = Server.URLEncode(SHIP_TELEPHONE)
	Login = Server.URLEncode(LOGIN)
	ShipMeth = Server.URLEncode(ShipMeth)
	PrmShip = Server.URLEncode(PrmShip)
	CustID = ORDER_ID
	
'//**************** END FORMAT VARIABLES ********************

	PaymentString = "LOGIN="&Login&"&USER1="&DSN_Name&"&USER2="&Address_1&"&USER3="&Company&"&USER4="&SHIP_NAME&"&USER5="&SHIP_COMPANY&"&USER6="&ShipArray1&"&USER7="&ShipArray2&"&USER8="&SHIP_CITY&"&USER9="&SHIP_STATE&"&USER10="&SHIP_COUNTRY&"&CUSTID="&CustID&"&Invoice="&ORDER_ID&"&DESCRIPTION="&SHIP_MESSAGE&"&TYPE="&MType&"&AMOUNT="&GrandTotal&"&ACCTNO="&CHK_ACCT_NO&"&ABACODE="&BANK_CODE&"&BANKNAME="&BANK_NAME&"&METHOD="&CARD_TYPE&"&CARDNUM="&CARD_NO&"&EXPDATE="&CARD_EXP&"&NAME="&CARD_NAME&"&ADDRESS="&ADDRESS_2&"&CITY="&CITY&"&STATE="&STATE&"&ZIP="&ZIP&"&COUNTRY="&COUNTRY&"&PHONE="&PHONE&"&FAX="&FAX&"&EMAIL="&E_MAIL&"&ECHODATA=TRUE&DISABLERECEIPT=TRUE"

	SndPayment = PaymentServer&"?"&PaymentString
	Response.Redirect SndPayment
	
end sub

'*******************************************************************
'** AuthorizeNet Receipt Sub-Routine *******************************

sub AuthNetResp(AuthResp)

	strIn = AuthResp
	strOut = ""
	intPos = Instr(strIn, "+")
	Do While intPos
	strLeft = ""
	strRight = ""
	If intPos > 1 Then strLeft = Left(strIn, intPos - 1)
	If intPos < Len(strIn) Then strRight = Mid(strIn, intPos + 1)
	strIn = strLeft & " " & strRight
	intPos = instr(strIn, "+")
	intLoop = intLoop + 1
	
	Loop
	intPos = Instr(strIn, "%")
	Do While intPos
	If intPos > 1 Then strOut = strOut & Left(strIn, intPos - 1)
	strOut = strOut & Chr(Cint("&H" & Mid(strIn, intPos + 1, 2)))
	If intPos > (Len(strIn) - 3) Then
	strIn = ""
	Else
	strIn = Mid(strIn, intPos + 3)
	End If
	intPos = Instr(strIn, "%")
	Loop
	URLDecode = strOut & strIn
	
'**************************
Set ObjResult = CreateObject("Scripting.Dictionary")

	strQuery = URLDecode
	intSep = Instr(strQuery, "&")
	Do While intSep
	strKey = ""
	strValue = ""
	strNVPair = Left(strQuery, intSep - 1)
	strQuery = Mid(strQuery, intSep + 1)
	intEqu = Instr(strNVPair, "=")
	If intEqu > 1 Then 
	strKey = Left(strNVPair, intEqu-1)
	End If
	If intEqu < Len(strNVPair) Then
	strValue = Mid(strNVPair, intEqu+1)
	End If
	objResult.Add strKey, strValue
	intSep = instr(strQuery, "&")
	Loop
	strKey = ""
	strValue = ""
	intEqu = Instr(strQuery, "=")
	If intEqu > 1 Then 
	strKey = Left(strQuery, intEqu-1)
	End if
	If intEqu < Len(strQuery) Then 
	strValue = Mid(strQuery, intEqu+1)
	End If
	objResult.Add strKey, strValue
	
	strKeysArray = objResult.Keys
	strItemsArray = objResult.Items
	For intLoop = 0 To objResult.Count - 1
	strThisKey = strKeysArray(intLoop)
	strThisItem = strItemsArray(intLoop)
	Next
	
'AuthResp = ObjResult.Items()
'//********* GLOBAL VARIABLES ***************************
	CARD_NAME = objResult.Item("NAME")
	ORDER_ID = objResult.Item("INVOICE")
	SHIP_MESSAGE = Replace(objResult.Item("Description"),"'","''")
	DSN_NAME = objResult.Item("USER1")

'//************CUSTOMER VARIABLES *****************
	COMPANY = objResult.Item("USER3")
	ADDRESS_1 = objResult.Item("ADDRESS")
	ADDRESS_2 = objResult.Item("USER2")
	CITY = objResult.Item("CITY")
	STATE = objResult.Item("STATE")
	COUNTRY = objResult.Item("COUNTRY")
	ZIP = objResult.Item("ZIP")
	PHONE = objResult.Item("PHONE")
	FAX = objResult.Item("FAX")
	CUSTOMER = objResult.Item("EMAIL")
	E_MAIL = objResult.Item("EMAIL")

	ProcCustNumber = objResult.Item("CustomerTransactionNumber")
	ProcMerchNumber = objResult.Item("TRANSID")
	ProcAvsCode = objResult.Item("AVSDATA")
	ProcMessage = objResult.Item("AuxMsg")
	ProcRefCode = objResult.Item("RESPONSECODE")
	Proc = objResult.Item("RetrievalCode")
	ProcAuthCode = objResult.Item("AUTHCODE")
	ProcErrMsg = objResult.Item("DECLINEREASON")
	ProcErrLoc = objResult.Item("ErrorLocation")
	Status = objResult.Item("Status")
	
'//***** PAYMENT VARIABLES *********************
	If objResult.Item("METHOD") = "ACH" Then
	PAYMENT_METHOD = "ECHECK"
	Else
	PAYMENT_METHOD = "CREDIT"
	End If	
	CARD_TYPE = "AuthorizeNet Transaction"
	CARD_NO = ""
	CARD_EXP = ""
	BANK_NAME = ""
	ROUTING_NO = ""
	CHK_ACCT_NO = ""
	PURCH_ORDER_NO = ""
	CHK_NO = ""
	

	SHIP_NAME = objResult.Item("USER4")
	SHIP_COMPANY = objResult.Item("USER5")
	ShipArray1 = objResult.Item("USER6")
	ShipArray1 = Split(ShipArray1,"@")
	SHIP_ADDRESS_2 = Replace(ShipArray1(0),"@","")
	SHIP_ADDRESS_1 = Replace(ShipArray1(1),"@","")
	SHIP_ZIP = Replace(ShipArray1(2),"@","")
	SHIP_TELEPHONE = Replace(ShipArray1(3),"@","")
	
	SHIP_CITY = objResult.Item("USER8")
	SHIP_STATE = objResult.Item("USER9")
	SHIP_COUNTRY = objResult.Item("USER10")
		
	ShipArray2 = Request("USER7")
	ShipArray2 = Replace(ShipArray2,"%40","@")
	ShipArray2 = Split(ShipArray2,"@")
	
	ShipMeth = Replace(ShipArray2(0),"@","")
	PrmShip = Replace(ShipArray2(1),"@","")
	
end sub


'*******************************************************************
'** AuthorizeNet 3.0 Send Sub-Routine ******************************

sub AuthNet30Proc

	If MerchantType = "authonly" Then
	MType = "AUTH_ONLY"
	End If
	If MerchantType = "authcapture" Then
	MType = "AUTH_CAPTURE"
	End If
		
	If PAYMENT_METHOD = "ECHECK" Then
	PAYMENT_METHOD = "ECHECK"
	Else
	PAYMENT_METHOD = "CC"
	End If
	

	'//***********FORMAT VARIABLES FOR AUTHORIZENET ********************
	DSN_NAME = Server.URLEncode(DSN_NAME)
	CARD_NAME = Server.URLEncode(CARD_NAME)
	ORDER_ID = Server.URLEncode(ORDER_ID)
	SHIP_MESSAGE = Server.URLEncode(SHIP_MESSAGE)
	
	'//************CUSTOMER VARIABLES *****************
	COMPANY = Server.URLEncode(COMPANY)
	ADDRESS_1 = Server.URLEncode(ADDRESS_1)
	ADDRESS_2 = Server.URLEncode(ADDRESS_2)
	CITY = Server.URLEncode(CITY)
	STATE = Server.URLEncode(STATE)
	COUNTRY = Server.URLEncode(COUNTRY)
	ZIP = Server.URLEncode(ZIP)
	PHONE = Server.URLEncode(PHONE)
	FAX = Server.URLEncode(FAX)
	E_MAIL = Server.URLEncode(E_MAIL)	
	'//***** PAYMENT VARIABLES *********************
	PAYMENT_METHOD = Server.URLEncode(PAYMENT_METHOD)
	MTYPE = Server.URLEncode(MType)
	CARD_TYPE = Server.URLEncode(CARD_TYPE)
	CARD_NO = Server.URLEncode(CARD_NO)
	CARD_EXP = Server.URLEncode(CARD_EXP)
	BANK_NAME = Server.URLEncode(BANK_NAME)
	ROUTING_NO = Server.URLEncode(ROUTING_NO)
	CHK_ACCT_NO = Server.URLEncode(CHK_ACCT_NO)
	PURCH_ORDER_NO = Server.URLEncode(PURCH_ORDER_NO)
	CHK_NO = Server.URLEncode(CHK_NO)	
	'//******** SHIPPING VARIABLES *******************
	SHIP_NAME = Server.URLEncode(SHIP_NAME)
	SHIP_COMPANY = Server.URLEncode(SHIP_COMPANY)
	SHIP_ADDRESS_1 = Server.URLEncode(SHIP_ADDRESS_1)
	SHIP_ADDRESS_2 = Server.URLEncode(SHIP_ADDRESS_2)
	SHIP_CITY = Server.URLEncode(SHIP_CITY)
	SHIP_STATE = Server.URLEncode(SHIP_STATE)
	SHIP_COUNTRY = Server.URLEncode(SHIP_COUNTRY)
	SHIP_ZIP = Server.URLEncode(SHIP_ZIP)
	SHIP_TELEPHONE = Server.URLEncode(SHIP_TELEPHONE)
	Login = Server.URLEncode(LOGIN)
	ShipMeth = Server.URLEncode(ShipMethName)
	PrmShip = Server.URLEncode(PrmShip)
	CustID = ORDER_ID

      If Request.ServerVariables("HTTPS") = "off" Then
	rawURL = "http://"&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("URL")
      Else
	rawURL = "https://"&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("URL")
      End If
	newURL = Replace(rawURL,"SFLib/processor.inc","confirm.asp")
	URL = Server.URLEncode(newURL)

		
'//**************** END FORMAT VARIABLES ********************

	PaymentString = "x_Login="&Login&"&x_Amount="&GrandTotal&"&x_Card_Num="&CARD_NO&"&x_Exp_Date="&CARD_EXP&"&x_ADC_Relay_Response=TRUE&x_Version=3.0&x_Method="&PAYMENT_METHOD&"&x_Type="&MTYPE&"&x_Bank_Name="&BANK_NAME&"&x_Bank_ABA_Code="&ROUTING_NO&"&x_Bank_Acct_Num="&CHK_ACCT_NO&"&x_PO_Num="&PURCH_ORDER_NO&"&x_Cust_ID="&CustID&"&x_Company="&COMPANY&"&x_Last_Name="&CARD_NAME&"&x_Address="&ADDRESS_2&"&Address1="&ADDRESS_1&"&x_City="&CITY&"&x_State="&STATE&"&x_Zip="&ZIP&"&x_Country="&COUNTRY&"&x_Phone="&PHONE&"&x_Email="&E_MAIL&"&x_Ship_To_Last_Name="&SHIP_NAME&"&x_Ship_To_Company="&SHIP_COMPANY&"&x_Ship_To_Address="&SHIP_ADDRESS_2&"&ShipAddress="&SHIP_ADDRESS_1&"&x_Ship_To_City="&SHIP_CITY&"&x_Ship_To_State="&SHIP_STATE&"&x_Ship_To_Zip="&SHIP_ZIP&"&x_Ship_To_Country="&SHIP_COUNTRY&"&DSN="&DSN_NAME&"&ShipMessage="&SHIP_MESSAGE&"&FaxNo="&FAX&"&ChkNo="&CHK_NO&"&ShipMeth="&ShipMeth&"&ShipMethName="&ShipMeth&"&PrmShip="&PrmShip&"&ShipPhone="&SHIP_TELEPHONE&"&CardType="&CARD_TYPE

	SndPayment = PaymentServer&"?"&PaymentString
	Response.Redirect SndPayment

end sub


'*******************************************************************
'** AuthorizeNet 3.0 Receipt Sub-Routine ***************************

sub AuthNet30Resp

'//********* GLOBAL VARIABLES ***************************
	CARD_NAME = Request("x_last_name")
	ORDER_ID = Request("x_cust_id")
	SHIP_MESSAGE = Replace(Request("ShipMessage"),"'","''")
	DSN_Name = Request("DSN")

'//************CUSTOMER VARIABLES *****************
	COMPANY = Request("x_company")
	ADDRESS_1 = Request("Address1")
	ADDRESS_2 = Request("x_address")
	CITY = Request("x_city")
	STATE = Request("x_state")
	COUNTRY = Request("x_country")
	ZIP = Request("x_zip")
	PHONE = Request("x_phone")
	FAX = Request("FaxNo")
	CUSTOMER = Request("x_email")
	E_MAIL = Request("x_email")
	
'//***** PAYMENT VARIABLES *********************
	If Request("x_method") = "ECHECK" Then
	PAYMENT_METHOD = "ECHECK"
	Else
	PAYMENT_METHOD = "CREDIT"
	End If	
	CARD_TYPE = Request("CardType")
	CARD_NO = Request("x_card_num")
	CARD_EXP = Request("x_exp_date")
	BANK_NAME = Request("x_bank_name")
	ROUTING_NO = Request("x_bank_aba_code")
	CHK_ACCT_NO = Request("x_bank_acct_num")
	PURCH_ORDER_NO = Request("x_po_num")
	CHK_NO = Request("ChkNo")
	
	SHIP_NAME = Request("x_ship_to_last_name")
	SHIP_COMPANY = Request("x_ship_to_company")
	SHIP_ADDRESS_2 = Request("x_ship_To_Address")
	SHIP_ADDRESS_1 = Request("ShipAddress")
	SHIP_ZIP = Request("x_ship_to_zip")
	SHIP_TELEPHONE = Request("ShipPhone")
	SHIP_CITY = Request("x_ship_to_city")
	SHIP_STATE = Request("x_ship_to_state")
	SHIP_COUNTRY = Request("x_ship_to_country")
		
	ShipMeth = Request("ShipMeth")
	ShipMethName = Request("ShipMethName")
	PrmShip = Request("PrmShip")
	
	If Request("x_response_code") = "1" Then
		ProcResponse = "success"
	ElseIf Request("x_response_code") = "2" or Request("x_response_code") = "3" Then
		ProcResponse = "fail"
	End If
	ProcCustNumber = Request("x_trans_id")
	ProcAuthCode =Request("x_auth_code")
	ProcMessage = ""
	ProcActionCode = Request("x_reason_code")
	ProcMessage = Request("x_response_subcode")
	ProcErrMsg = Request("x_response_reason_text")
	ProcAvsCode = Request("x_avs_code")
	
	Select Case ProcAvsCode
		Case "A"
		  AVSMsg = "Address (Street) matches, ZIP does not. (Code A)"
		Case "E"
		  AVSMsg = "AVS error. (Code E)"
		Case "N"
		  AVSMsg = "No Match on Address (Street) or ZIP. (Code N)"
		Case "P"
		  AVSMsg = "AVS not applicable for this transaction. (Code P)"
		Case "R"
		  AVSMsg = "Retry System unavailable or timed out. (Code R)"
		Case "S"
		  AVSMsg = "Service not supported by issuer. (Code S)"
		Case "U"
		  AVSMsg = "Address information is unavailable. (Code U)"
		Case "W"
		  AVSMsg = "9 digit ZIP matches, Address (Street) does not. (Code W)"
		Case "X"
		  AVSMsg = "Exact AVS Match. (Code X)"
		Case "Y"
		  AVSMsg = "Address (Street) and 5 digit ZIP match. (Code Y)"
		Case "Z"
		  AVSMsg = "5 digit ZIP matches, Address (Street) does not. (Code Z)"
		Case Else
		  AVSMsg = "Unknown AVS Code."
	End Select
	
	ProcAvsZip = ""

	
	
end sub


'*******************************************************************
'** PaymentNet Sub-Routine *****************************************

sub PaymentNetProc

	Grand_Total = FormatCurrency(GrandTotal,2)
	ccCard_No = CARD_NO
	ccExp_Date = Replace(CARD_EXP,"/","")
	ccExp_Date = Replace(ccExp_Date,"-","")
	ccExp_Date = Replace(ccExp_Date," ","")
	ccName = CARD_NAME
	ccAddress = Request("ADDRESS_2")&","&Request("ADDRESS_1")
	ccCity = CITY
	ccState = STATE
	ccZip = ZIP
	ccCountry = COUNTRY
	ParseLogin = Split(Login,"@")
	UserID = ParseLogin(0)
	Password = ParseLogin(1)
	Amt = GrandTotal
	Amt = ValAmt(Amt)
	OrderID = Request.QueryString("ORDER_ID")
	MerchantType = UCase( MerchantType )
		If MerchantType = "AUTHCAPTURE" Then
		TrxType = "S"
		ElseIf MerchantType = "AUTHONLY" Then
		TrxType = "A"
		End If

	Set obj = Server.CreateObject("PFProSSControl.PFProSSControl.1")
	obj.HostAddress = PaymentServer
	obj.HostPort = 443 'Live
	obj.TimeOut = 30
	obj.DebugMode = 1
	obj.ParmList = "TRXTYPE="&TrxType&"&TENDER=C&USER="&UserID&"&PWD="&Password&"&ACCT="&ccCard_No&"&EXPDATE="&ccExp_Date&"&AMT="&Amt&"&ZIP="&ccZip
	obj.PNInit()
	obj.ProcessTransaction()
	
	strIn = obj.Response 
	strOut = ""
	intPos = Instr(strIn, "+")
	Do While intPos
	strLeft = ""
	strRight = ""
	If intPos > 1 Then strLeft = Left(strIn, intPos - 1)
	If intPos < Len(strIn) Then strRight = Mid(strIn, intPos + 1)
	strIn = strLeft & " " & strRight
	intPos = instr(strIn, "+")
	intLoop = intLoop + 1
	
	Loop
	intPos = Instr(strIn, "%")
	Do While intPos
	If intPos > 1 Then strOut = strOut & Left(strIn, intPos - 1)
	strOut = strOut & Chr(Cint("&H" & Mid(strIn, intPos + 1, 2)))
	If intPos > (Len(strIn) - 3) Then
	strIn = ""
	Else
	strIn = Mid(strIn, intPos + 3)
	End If
	intPos = Instr(strIn, "%")
	Loop
	URLDecode = strOut & strIn

Set ObjResult = CreateObject("Scripting.Dictionary")

	strQuery = URLDecode
	intSep = Instr(strQuery, "&")
	Do While intSep
	strKey = ""
	strValue = ""
	strNVPair = Left(strQuery, intSep - 1)
	strQuery = Mid(strQuery, intSep + 1)
	intEqu = Instr(strNVPair, "=")
	If intEqu > 1 Then 
	strKey = Left(strNVPair, intEqu-1)
	End If
	If intEqu < Len(strNVPair) Then
	strValue = Mid(strNVPair, intEqu+1)
	End If
	objResult.Add strKey, strValue
	intSep = instr(strQuery, "&")
	Loop
	strKey = ""
	strValue = ""
	intEqu = Instr(strQuery, "=")
	If intEqu > 1 Then 
	strKey = Left(strQuery, intEqu-1)
	End if
	If intEqu < Len(strQuery) Then 
	strValue = Mid(strQuery, intEqu+1)
	End If
	objResult.Add strKey, strValue
	
	strKeysArray = objResult.Keys
	strItemsArray = objResult.Items
	For intLoop = 0 To objResult.Count - 1
	strThisKey = strKeysArray(intLoop)
	strThisItem = strItemsArray(intLoop)
	Next
	ProcResponseNum = ObjResult.Item("RESULT")
		
		If ProcResponseNum = "0" OR ProcResponse = "112" Then
		ProcResponse = "approved"
		Else
		ProcResponse = "fail"
		If ProcResponseNum = "12" OR ProcResponse = "50" Then
		ProcErrMsg = "Transaction Declined"
		ElseIf ProcResponseNum = "13" Then
		ProcErrMsg = "Card Holder -- Call Issuing Bank"
		ElseIf ProcResponseNum = "23" Then
		ProcErrMsg = "Invalid Account Number"
		ElseIf ProcResponseNum = "24" Then
		ProcErrMsg = "Invalid Expiration Date"
		Else
		ProcErrMsg = "General Processing Error -- Please try the transaction again or notify the merchant"
		End If
	
		End If
	ProcCustNumber = ObjResult.Item("PNREF")
	ProcAuthCode = ObjResult.Item("AUTHCODE")
	ProcActionCode = ObjResult.Item("RESPCODE")
	'ProcMessage = ObjResult.Item("RESPMSG")
	'ProcErrMsg = ObjResult.Item("ERRMSG")
	ProcAvsCode = ObjResult.Item("AVSADDR")
	ProcAvsZip = ObjResult.Item("AVSZIP")

	Select Case ProcAvsCode
		Case "Y"
		  AVSMsg = "Yes. Address and zip match. (Code Y)"
		Case "N"
		  AVSMsg = "No. Address and zip do not match. (Code N)"
		Case "X"
		  AVSMsg = "Address/zip not supplied or service not supported. (Code X)"
		Case Else
		  AVSMsg = "Unknown AVS Code."
	End Select

	obj.PNCleanup 
	Set obj = Nothing

end sub


'*******************************************************************
'** PCAuthorize Sub-Routine ****************************************
sub PCAuthProc

	Set AuthCtl = Server.CreateObject("AuthCtl.AuthCtlCtrl.1")
	AuthCtl.Hub=1
	AuthCtl.Protocol="SOCKETS"
	AuthCtl.ApplName="PCAuthorize"
	AuthCtl.HostName=PaymentServer
	AuthCtl.Port=54321
	AuthCtl.Account = ccCard_No
	AuthCtl.Amount = GrandTotal
	AuthCtl.ExpDate = ccExp_Date
	AuthCtl.Invoice = ORDER_ID
	AuthCtl.CustomerID = ORDER_ID
	AuthCtl.AVSAddress = ccAddress
	AuthCtl.AVSZip = ccZip
	AuthCtl.Authorize

	ProcResponse = AuthCtl.RespStatus
	ProcMessage = AuthCtl.RespText
	ProcRefCode = AuthCtl.RespRefNum
	ProcAuthCode = AuthCtl.RespAuthCode
	ProcErrMsg = AuthCtl.ErrMsg
	ProcAvsCode = AuthCtl.RespAVSCode

	Select Case ProcAvsCode
		Case "X"
		  AVSMsg = "Exact match including 9 digit zip. (Code X)"
		Case "Y"
		  AVSMsg = "Exact match including 5 digit zip. (Code Y)"
		Case "A"
		  AVSMsg = "Address matches, zip does not. (Code A)"
		Case "W"
		  AVSMsg = "Nine digit zip code matches, address does not. (Code W)"
		Case "Z"
		  AVSMsg = "Five digit zip code matches, address does not. (Code Z)"
		Case "N"
		  AVSMsg = "Nothing matches. (Code N)"
		Case "U"
		  AVSMsg = "No data from card issuer. (Code U)"
		Case "R"
		  AVSMsg = "Retry. (Code R)"
		Case "S"
		  AVSMsg = "AVS not supported for this merchant. (Code S)"
		Case "E"
		  AVSMsg = "Error. AVS not supported for this industry. (Code E)"
		Case Else
		  AVSMsg = "Unknown AVS Code."
	End Select
	
	If ProcResponse = "PD" OR ProcResponse = "AA" Then 
	ProcResponse = "approve"
	Else
	ProcResponse = "fail"
	End If

end sub

'*******************************************************************
'****************** Processing Network Sub-Routine *****************

sub ProcessingNetProc

	Dim approval
	Dim session
	Dim result
	Dim merchantID
	Dim orderID 
		
	merchantID = Split(LOGIN, ",")
	orderID = Request.QueryString("ORDER_ID")
	ccExp_Date = Replace(CARD_EXP,"/","")
	ccExp_Date = Replace(ccExp_Date," ","")
	ccExp_Date = Replace(ccExp_Date,"-","")
	
	Set msg = Server.CreateObject("msg2ax.msg2")
	msg.sessionCreate session

	msg.sessionSetVendor session, merchantID(0)
	msg.sessionSetKey session, merchantID(1)
	msg.sessionSetHost session, PaymentServer
	msg.sessionSetPort session, 80
	If MerchantType = "authonly" Then
		msg.sessionSetOption session, "auth", 1 
	End If	
	
	msg.sessionSetOption session, "card", CARD_NO
	msg.sessionSetOption session, "expr", ccExp_Date
	msg.sessionSetOption session, "price", GrandTotal
	msg.sessionSetOption session, "orderid", orderID
	msg.sessionSetOption session, "zip", ZIP
	result = msg.sessionTransaction (session, approval)
	msg.sessionDestroy session
		
	If Left(approval, 1) = "Y" Then
	ProcResponse = "approved"
	ProcAuthCode = Mid(approval, 2, 6)
	ProcAvsCode = Mid(approval, 8, 1)
	Else
	ProcResponse = "fail"
	ProcErrMsg = "There was no reason returned for the transaction failure. Check card number, card expiration date and available credit for the account"	
	End If

	Select Case ProcAvsCode
		Case "A"
		  AVSMsg = "Street address matches, Zip is wrong. (Code A)"
		Case "E"
		  AVSMsg = "Address verification not permitted. (Code E)"
		Case "N"
		  AVSMsg = "Street address and Zip are both wrong. (Code N)"
		Case "R"
		  AVSMsg = "Address verification system is busy (at processor). (Code R)"
		Case "U"
		  AVSMsg = "Address verification is unavailable (foreign card?). (Code U)"
		Case "W"
		  AVSMsg = "Street address and 9 digit Zip code both match. (Code W)"
		Case "X"
		  AVSMsg = "Nine digit Zip code matches, street address is wrong. (Code X)"
		Case "Y"
		  AVSMsg = "Street address and 5-digit zip both match. (Code Y)"
		Case "Z"
		  AVSMsg = "5-digit Zip matches, street address is wrong. (Code Z)"
		Case Else
		  AVSMsg = "Unknown AVS Code."
	End Select

End Sub

Sub CyberCash()

'Purpose: Process Payment
'Author: gjohnson(c)1999,2000 CyberCash, Inc.
'Requirement: CYCHMCK.DLL 2.0 or higher

Set Config = Server.CreateObject("CyberCashMCK.MessageBlock")
Set Input  = Server.CreateObject("CyberCashMCK.MessageBlock")
Set Output = Server.CreateObject("CyberCashMCK.MessageBlock")

strMessage="m"&MerchantType
CCID = Login
MERCHANT_KEY = PaymentServer

'Provide the config parameters required for CyberCash Transaction processing
Config.Add "CYBERCASH_ID", CCID
Config.Add "MERCHANT_KEY", MERCHANT_KEY
Config.Add "CCPS_HOST", "http://cr.cybercash.com/cgi-bin/cr21api.cgi/" & strMessage

'Provide the input parameters required for the CyberCash message (see developers guide)

Input.Add "card-number",  CARD_NO
Input.Add "card-exp",     CARD_EXP
Input.Add "card-name",    CARD_NAME
Input.Add "card-Address", Request("ADDRESS_2")&","&Request("ADDRESS_1")
Input.Add "card-city",    CITY
Input.Add "card-state",   STATE
Input.Add "card-zip",     ZIP
Input.Add "card-country", COUNTRY
Input.Add "order-id",     ORDER_ID
Input.Add "amount",       "usd " & REPLACE(AMOUNT,",","")

Set SocketObject = Server.CreateObject("CyberCashMCK.socket.1")
Set Output = SocketObject.SendMessageBlock(Config, Input)

	ProcResponse = Output.Item("MStatus") 'Approved/declined
	
	If ProcResponse = "success" Then
	 ProcResponse = "approved"
	Else
	 ProcResponse = "fail"
	End If
	
	ProcMessage = Replace(Output.Item("aux-msg"),"'","''") 'Detailed Info
	ProcCustNumber = Replace(Output.Item("cust-txn"),"'","''") 'Trans Number?
	ProcAddlData = Replace(Output.Item("addnl-response-data"),"'","''") 'Number?
	ProcRefCode = Replace(Output.Item("ref-code"),"'","''")
	ProcAuthCode = Replace(Output.Item("auth-code"),"'","''")
	ProcMerchNumber = Replace(Output.Item("merch-txn"),"'","''") 'Merch Trans Number
	ProcActionCode = Replace(Output.Item("action-code"),"'","''") '???
	ProcErrMsg = Replace(Output.Item("MErrMsg"),"'","''") 'Detailed info on failure
	ProcErrLoc = Replace(Output.Item("MErrLoc"),"'","''") 'Location error occured
	ProcErrCode = Replace(Output.Item("MErrCode"),"'","''") 'CyberCash error codes
	ProcAvsCode = Replace(Output.Item("avs-code"),"'","''") 'CyberCash AVS code

	Select Case ProcAvsCode
		Case "A"
		  AVSMsg = "Address matches, ZIP does not. (Code A)"
		Case "E"
		  AVSMsg = "Ineligible transaction. (Code E)"
		Case "N"
		  AVSMsg = "Neither address nor ZIP matches. (Code N)"
		Case "R"
		  AVSMsg = "Retry (system unavailable or timed out). (Code R)"
		Case "S"
		  AVSMsg = "Card type not supported. (Code S)"
		Case "U"
		  AVSMsg = "Address information unavailable. (Code U)"
		Case "W"
		  AVSMsg = "9 digit zip match, address does not. (Code W)"
		Case "X"
		  AVSMsg = "Exact match (9 digit zip and address). (Code X)"
		Case "Y"
		  AVSMsg = "Address and 5 digit zip match. (Code Y)"
		Case "Z"
		  AVSMsg = "5 digit zip matches, address does not. (Code Z)"
		Case Else
		  AVSMsg = "Unknown AVS Code."
	End Select


End Sub
'*******************************************************************
'****************** Internet Cash (SpendCash) Sub-Routine *****************

Sub InternetCash()

Dim bRet, configPath, iCash

Set iCash = Server.CreateObject("SpendCash.ICash")

configPath = Server.MapPath(".")
iCash.TemplateDirectory = configPath & "\icash"
configPath = configPath & "\icash\icash_mt.cnf"

Call iCash.LoadConfig(configPath)

iCash.Message = Request("message")

bRet = iCash.Charge()

if Err.number <> 0 OR Not bRet Then
	ProcResponse = "fail"
	ProcErrMsg = iCash.TransactionResponse.ReturnDescription 
	ProcErrCode = iCash.TransactionResponse.ReturnCode 
	ProcRefCode = iCash.TransactionResponse.PayAuthorizationNumber 
	ProcAuthCode = iCash.TransactionResponse.PAN
Else
	ProcResponse = "approved"
	ProcRefCode = iCash.TransactionResponse.PayAuthorizationNumber 
	ProcAuthCode = iCash.TransactionResponse.PAN
	ProcActionCode = iCash.TransactionResponse.ReturnCode  
	ProcMessage = iCash.TransactionResponse.ReturnDescription
End if

Set iCash = Nothing

End Sub


'*******************************************************************
'** CS-VPOS Send Sub-Routine ***************************************
'** Charge Solutions, LLC and APPROVE.net **************************

sub CSVPOSProc

	If MerchantType = "normal_auth" Then csAction = "capture" End If
	If MerchantType = "authonly" Then csAction = "auth" End If
	If MerchantType = "authcapture" Then csAction = "capture" End If
	If MerchantType = "Credit" Then csAction = "credit" End If
		
	'//***********FORMAT VARIABLES FOR CS-VPOS ********************
	csCard_Name= Server.URLEncode(CARD_NAME)
	
	'//************CUSTOMER VARIABLES *****************
	csTrans = FormatNumber(GrandTotal,2,0,0,0)
	csCard_No = Replace(CARD_NO,"/","")
	csCard_No = Replace(csCard_No,"-","")
	csCard_No = Replace(csCard_No," ","")
	csExp_Date = Replace(CARD_EXP,"/","")
	csExp_Date = Replace(csExp_Date,"-","")
	csExp_Date = Replace(csExp_Date," ","")
	ExpLen = Len(csExp_Date)	
	if (ExpLen = 6) then
		csExp_Month = Mid(csExp_Date,1,2)
		csExp_Year = Mid(csExp_Date,5,2)
	else
		csExp_Month = Mid(csExp_Date,1,2)
		csExp_Year = Mid(csExp_Date,3,2)
	end If
	csCompany = Server.URLEncode(COMPANY)
	csNames = Split(CARD_NAME," ")
	csFirstname = csNames(0)
	lastname = Ubound(csNames)
	csLastname = csNames(lastname)
	csAddress = ADDRESS_2&" "&ADDRESS_1
	csAddress = Server.URLEncode(csAddress)
	csCity = Server.URLEncode(CITY)
	csState = Server.URLEncode(STATE)
	if (COUNTRY = "US") then 
		csCountry = "United States Of America" 
	else 
		csCountry = COUNTRY
	end If
	csCountry = Server.URLEncode(csCountry)
	csZip = Server.URLEncode(ZIP)
	csPhone = Server.URLEncode(PHONE)
	csFax = Server.URLEncode(FAX)
	csEmail = Server.URLEncode(E_MAIL)	
	csPassthru1 = DSN_Name
	csPassthru1 = Server.URLEncode(csPassthru1)
	csPassthru2 = ORDER_ID
	csPassthru2 = Server.URLEncode(CSPassthru2)
	csUser2 = Server.URLEncode(SHIP_MESSAGE)
	If ShipType = "2" Then 
		ShipMeth = ShipMethName 
	End If
	csPassthru3 = Server.URLEncode(ShipMeth)
	csPassthru4 = Server.URLEncode(ShipType)
	csPassthru5 = Server.URLEncode(PrmShip)

	'//***** PAYMENT VARIABLES *********************
	PAYMENT_METHOD = Server.URLEncode(PaymentMethod)
	'//******** SHIPPING VARIABLES *******************
	if (SHIP_NAME <> "") then
		csShip_Name = SHIP_NAME
	end If
	if (SHIP_COMPANY <> "") then
		csShip_Name = csShip_Name&"/"&SHIP_COMPANY
	end If
	if (csShip_Name = "") then
		csShip_Name = CARD_NAME
	end If
	csShip_Name = Server.URLEncode(csShip_Name)
	if (SHIP_ADDRESS_1 <> "" or SHIP_ADDRESS_2 <> "") then
		csShip_Address = SHIP_ADDRESS_2&" "&SHIP_ADDRESS_1
	else 
		csShip_Address = ADDRESS_2&" "&ADDRESS_1
	end If
	csShip_Address = Server.URLEncode(csShip_Address)
	if (SHIP_CITY <> "") then
		csShip_City = SHIP_CITY
	else
		csShip_City = CITY
	end If
	csShip_City = Server.URLEncode(csShip_City)
	if (SHIP_STATE <> "") then
		csShip_State = SHIP_STATE
	else
		csShip_State = STATE
	end If
	csShip_State = Server.URLEncode(csShip_State)
	if (SHIP_ZIP <> "") then
		csShip_Zip = SHIP_ZIP
	else
		csShip_Zip = ZIP
	end If
	csShip_Zip = Server.URLEncode(csShip_Zip)
	if (SHIP_COUNTRY <> "" and SHIP_COUNTRY = "US") then 
		csShip_Country = "United States Of America" 
	else 
		csShip_Country = SHIP_COUNTRY
	end If
	csShip_Country = Server.URLEncode(csShip_Country)
	if (SHIP_PHONE <> "") then 
		csShip_Phone = SHIP_PHONE
	else 
		csShip_Phone = PHONE
	end If
	csShip_Phone = Server.URLEncode(csShip_Phone)
	Merchant = Server.URLEncode(LOGIN)
	nextURL = "http://"&Request.ServerVariables("HTTP_HOST")&Request.ServerVariables("URL")
	csNext_URL = Replace(nextURL,"SFLib/processor.inc","confirm.asp")
	URL = Server.URLEncode(csNext_URL)
	csRemoteAddr = Server.URLEncode(Request.ServerVariables("REMOTE_ADDR"))
'//**************** END FORMAT VARIABLES ********************

	PaymentString = "mode=virtual&trans_action="&csAction&"&trans_amount="&csTrans&"&card_number="&csCard_No&"&exp_mon="&csExp_Month&"&exp_year="&csExp_Year&"&merchant_account="&Merchant
	PaymentString = PaymentString&"&card_holder="&csCard_Name&"&city="&csCity&"&state="&csState&"&address1="&csAddress&"&customer_ip="&csRemoteAddr&"&customer_email=NO"
	PaymentString = PaymentString&"&email="&csEmail&"&zip="&csZip&"&country="&csCountry&"&firstname="&csFirstname&"&lastname="&csLastname
	paymentString = PaymentString&"&next_url="&csNext_URL&"&passthru1="&csPassthru1&"&passthru2="&csPassthru2&"&passthru3="&csPassthru3&"&passthru4="&csPassthru4&"&passthru5="&csPassthru5&"&user2="&csUser2&"&SHIPPING=YES&PHONE=YES&ship_name="&csShip_Name&"&ship_address="&csShip_Address&"&ship_city="&csShip_City&"&ship_state="&csShip_State&"&ship_zip="&csShip_Zip&"&ship_country="&csShip_Country&"&ship_phone="&csShip_Phone&"&home_phone="&csPhone&"&fax_phone="&csFax

	SndPayment = PaymentServer&"?"&PaymentString
	Response.Redirect(SndPayment)

end sub


'*******************************************************************
'** CS-VPOS Send Sub-Routine ***************************************
'** Charge Solutions, LLC and APPROVE.net **************************

sub CSVPOSResp(AuthResp)

	strIn = AuthResp
	strOut = ""
	intPos = Instr(strIn, "+")
	Do While intPos
	strLeft = ""
	strRight = ""
	If intPos > 1 Then strLeft = Left(strIn, intPos - 1)
	If intPos < Len(strIn) Then strRight = Mid(strIn, intPos + 1)
	strIn = strLeft & " " & strRight
	intPos = instr(strIn, "+")
	intLoop = intLoop + 1
	
	Loop
	intPos = Instr(strIn, "%")
	Do While intPos
	If intPos > 1 Then strOut = strOut & Left(strIn, intPos - 1)
	strOut = strOut & Chr(Cint("&H" & Mid(strIn, intPos + 1, 2)))
	If intPos > (Len(strIn) - 3) Then
	strIn = ""
	Else
	strIn = Mid(strIn, intPos + 3)
	End If
	intPos = Instr(strIn, "%")
	Loop
	URLDecode = strOut & strIn
	'**************************
Set ObjResult = CreateObject("Scripting.Dictionary")

	strQuery = URLDecode
	intSep = Instr(strQuery, "&")
	Do While intSep
	strKey = ""
	strValue = ""
	strNVPair = Left(strQuery, intSep - 1)
	strQuery = Mid(strQuery, intSep + 1)
	intEqu = Instr(strNVPair, "=")
	If intEqu > 1 Then 
	strKey = Left(strNVPair, intEqu-1)
	End If
	If intEqu < Len(strNVPair) Then
	strValue = Mid(strNVPair, intEqu+1)
	End If
	objResult.Add strKey, strValue
	intSep = instr(strQuery, "&")
	Loop
	strKey = ""
	strValue = ""
	intEqu = Instr(strQuery, "=")
	If intEqu > 1 Then 
	strKey = Left(strQuery, intEqu-1)
	End if
	If intEqu < Len(strQuery) Then 
	strValue = Mid(strQuery, intEqu+1)
	End If
	objResult.Add strKey, strValue
	
	strKeysArray = objResult.Keys
	strItemsArray = objResult.Items
	For intLoop = 0 To objResult.Count - 1
	strThisKey = strKeysArray(intLoop)
	strThisItem = strItemsArray(intLoop)
	Next
	
'AuthResp = ObjResult.Items()
'//********* GLOBAL VARIABLES ***************************

	CARD_NO = objResult.Item("card_number")
	CARD_NAME = objResult.Item("card_holder")
	SHIP_MESSAGE = Replace(objResult.Item("user2"),"'","''")
	DSN_NAME = objResult.Item("passthru1")
	ORDER_ID = objResult.Item("passthru2")
	ShipMeth = objResult.Item("passthru3")
	ShipType = objResult.Item("passthru4")
	PrmShip = objResult.Item("passthru5")
	Answer = objResult.Item("trans_approved")
	if (Answer <> "APPROVED") then
		ProcResponse = "fail"
	else
		ProcResponse = "approve"
	end If
	if (ProcResponse <> "approve") then
		ProcMessage = objResult.Item("trans_response")
		ProcErrMsg = objResult.Item("trans_response")
	else
		ProcAuthCode = objResult.Item("trans_response")
		ProcCustNumber = objResult.Item("receipt_number")
		ProcRefCode = objResult.Item("receipt_number")
		ProcMerchNumber = objResult.Item("receipt_number")
		if (ProcAuthCode = "APPROVED") then
			ProceActionCode = "capture"
		end If
		if (ProcAuthCode = "CREDIT") then
			ProceActionCode = "credit"
		end If
		if (ProcAuthCode = "AUTH ONLY") then
			ProceActionCode = "pre-auth"
		end If
		ProcAvsCode = objResult.Item("avs_result")
		ProcAvsZip = objResult.Item("avs_zip_result")
	end If


'//************CUSTOMER VARIABLES *****************
	COMPANY = objResult.Item("ship_name")
	ADDRESS_2 = objResult.Item("address1")
	CITY = objResult.Item("city")
	STATE = objResult.Item("state")
	COUNTRY = objResult.Item("country")
	If InStr(COUNTRY,"United States") Then
	COUNTRY = "US"
	End If
	ZIP = objResult.Item("zip")
	PHONE = objResult.Item("home_phone")
	FAX = objResult.Item("fax_phone")
	CUSTOMER = objResult.Item("email")
	E_MAIL = objResult.Item("email")
	
'//***** PAYMENT VARIABLES *********************
	PAYMENT_METHOD = "CREDIT"
	CARD_TYPE = objResult.Item("cardtype")
	CARD_NO = objResult.Item("card_number")
	EXP_MON = objResult.Item("exp_mon")
	EXP_YEAR = objResult.Item("exp_year")
	CARD_EXP = EXP_MON & "/" & EXP_YEAR
	SHIP_NAME = objResult.Item("ship_name")
	SHIP_COMPANY = objResult.Item("ship_name")
	SHIP_ADDRESS_2 = objResult.Item("ship_address")
	SHIP_ZIP = objResult.Item("ship_zip")
	SHIP_TELEPHONE = objResult.Item("ship_phone")
	SHIP_CITY = objResult.Item("ship_city")
	SHIP_STATE = objResult.Item("ship_state")
	SHIP_COUNTRY = objResult.Item("ship_country")

end sub

'// Credit Card Number Encode/Decode Routines

Function EncodeCC(newCC, newOrderID)

	Dim cc
	Dim order_id

	Dim lHalf
	Dim rHalf

	Dim hexlHalf
	Dim hexrHalf

	Dim result
	Dim length

	cc = Replace(newCC, "/", "")
	cc = Replace(cc, "-", "")
	cc = Replace(cc, ",", "")
	cc = Replace(cc, " ", "")	

	order_id = newOrderID

	length = Len(Trim(cc))

	lHalf = Mid(cc, 1, Int((length / 2)))
	rHalf = Mid(cc, Int((length / 2)) + 1)

	lHalf = "1" & lHalf
	rHalf = "1" & rHalf

	lHalf = (lHalf + CDbl(Mid(order_id, 1, 4)))
	rHalf = (rHalf - CDbl(Mid(order_id, 1, 4)))

	hexlHalf = Hex(CStr(lHalf))
	hexrHalf = Hex(CStr(rHalf))

	EncodeCC = hexrHalf & "N" & hexlHalf

End Function



Function DecodeCC(newCodedCC, newOrderID)

	Dim hexCC 
	Dim order_id 

	Dim lHalf 
	Dim rHalf 

	Dim hexlHalf 
	Dim hexrHalf 

	Dim rlength 

	hexCC = newCodedCC
	order_id = newOrderID

	hexrHalf = Mid(hexCC, 1, InStr(1, hexCC, "N", vbBinaryCompare))
	hexlHalf = Mid(hexCC, InStr(1, hexCC, "N", vbBinaryCompare))


	hexrHalf = Mid(hexrHalf, 1, Len(hexrHalf) - 1)
	hexlHalf = Mid(hexlHalf, 2, Len(hexlHalf) - 1)

	lHalf = HexToDec(hexlHalf)
	rHalf = HexToDec(hexrHalf)

	lHalf = (lHalf - CDbl(Mid(order_id, 1, 4)))
	rHalf = (rHalf + CDbl(Mid(order_id, 1, 4)))


	lHalf = Mid(lHalf, 2)
	rHalf = Mid(rHalf, 2)

	DecodeCC = CStr(lHalf) & CStr(rHalf)

End Function



Function HexToDec(sHex)

	Dim power 
	Dim values(25)
	Dim currValue 
	Dim length 
	Dim sum 

	length = 0

	For power = 1 To Len(Trim(sHex))
    
    	Select Case Mid(sHex, Len(Trim(sHex)) - length, 1)
    
    		Case "A"
    		currValue = "10"
    
    		Case "B"
    		currValue = "11"
    
    		Case "C"
    		currValue = "12"
    
    		Case "D"
    		currValue = "13"
    
    		Case "E"
    		currValue = "14"
    
    		Case "F"
    		currValue = "15"
    
    		Case Else
    		currValue = Mid(sHex, Len(Trim(sHex)) - length, 1)
    
    	End Select
    
    	values(power) = ((16 ^ length) * currValue)
    	length = length + 1
    	sum = sum + values(power)
    
	Next

	HexToDec = sum

End Function

%>