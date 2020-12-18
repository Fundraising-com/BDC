<%
' Modification for ASP by Dave Lambert, dlambert@infoponic.com
' Modified for StoreFront by LaGarde Inc.
' Created from ccapi_error.h from API 3.8

Const Fail = 0
Const Succeed = 1

' Created from ccapi_client.h from API 3.8

'Request types possible for OrderField_Chargetype
Const Chargetype_Auth = 0
Const Chargetype_Sale = 0
Const Chargetype_Preauth = 1
Const Chargetype_Postauth = 2
Const Chargetype_Credit = 3
Const Chargetype_Error = 0

'Result types possible for OrderField_Result
Const Result_Live = 0 
Const Result_Good = 1
Const Result_Duplicate = 2
Const Result_Decline = 3

'ESD types for ItemField_Esdtype
Const Esdtype_None = 0
Const Esdtype_Softgood = 1
Const Esdtype_Key = 2

' OrderField_t
Const OrderField_Oid = 0   
Const OrderField_Userid = 1
Const OrderField_Bcompany = 2
Const OrderField_Bcountry = 3
Const OrderField_Bname = 4   
Const OrderField_Baddr1 = 5  
Const OrderField_Baddr2 = 6
Const OrderField_Bcity = 7
Const OrderField_Bstate = 8
Const OrderField_Bzip = 9
Const OrderField_Sname = 10
Const OrderField_Saddr1 = 11
Const OrderField_Saddr2 = 12
Const OrderField_Scity = 13
Const OrderField_Sstate = 14
Const OrderField_Szip = 15
Const OrderField_Scountry = 16
Const OrderField_Phone = 17
Const OrderField_Fax = 18
Const OrderField_Refer = 19
Const OrderField_Shiptype = 20
Const OrderField_Shipping = 21
Const OrderField_Tax = 22
Const OrderField_Subtotal = 23 
Const OrderField_Vattax = 24
Const OrderField_Comments = 25
Const OrderField_MaxItems = 26
Const OrderField_Email = 27
Const OrderField_Cardnumber = 28 
Const OrderField_Expmonth = 29
Const OrderField_Expyear = 30
Const OrderField_Chargetype = 31
Const OrderField_Chargetotal = 32
Const OrderField_Referencenumber = 33 
Const OrderField_Result = 34
Const OrderField_Addrnum = 35
Const OrderField_Ip = 36

' Responses 
Const OrderField_R_Time = 37
Const OrderField_R_Ref = 38
Const OrderField_R_Approved = 39
Const OrderField_R_Code = 40
Const OrderField_R_Ordernum = 41
Const OrderField_R_Error = 42
Const OrderField_R_FraudCode = 43

' ReqField_t
Const ReqField_Configfile = 0
Const ReqField_Keyfile = 1
Const ReqField_Appname = 2
Const ReqField_Host = 3
Const ReqField_Port = 4

' ItemField_t
Const ItemField_Itemid = 0   
Const ItemField_Description = 1
Const ItemField_Price = 2
Const ItemField_Quantity = 3
Const ItemField_Softfile = 4
Const ItemField_Esdtype = 5
Const ItemField_Serial = 6
Const ItemField_MaxOptions = 7

' ShippingField_t
Const ShippingField_Country = 0
Const ShippingField_State = 1
Const ShippingField_Total = 2
Const ShippingField_Items = 3
Const ShippingField_Weight = 4
Const ShippingField_Carrier = 5
' Responses 
Const ShippingField_R_Total = 6

' TaxField_t
Const TaxField_State = 0
Const TaxField_Zip = 1
Const TaxField_Total = 2
' Responses 
Const TaxField_R_Total = 3

' OptionField_t
Const OptionField_Option = 0
Const OptionField_Choice = 1

' Opaque contexts
Dim OrderCtx
Dim ItemCtx
Dim OptionCtx
Dim ReqCtx
Dim ApiDriver

Dim total


'------------------------------------------------------------
' Functions
'------------------------------------------------------------
Function ProcessCard()

Set ApiDriver = Server.CreateObject("ComApi_3_8.ComApi")

OrderCtx =		ApiDriver.csi_order_alloc()
ReqCtx =		ApiDriver.csi_req_alloc()

'Get Path to PEM file.''''''''''''''''''''''''''''''
Dim PemPath
PemPath = Server.MapPath(".")
PemPath = PemPath & "\SFLib\sf.pem"
PemPath = Replace(PemPath, "\", "\\")
''''''''''''''''''''''''''''''''''''''''''''''''''''

Flag = ApiDriver.csi_req_set(ReqCtx, ReqField_Configfile, CStr(LOGIN))
Flag = ApiDriver.csi_req_set(ReqCtx, ReqField_Keyfile, CStr(PemPath))
Flag = ApiDriver.csi_req_set(ReqCtx, ReqField_Host, CStr(PaymentServer))
Flag = ApiDriver.csi_req_set(ReqCtx, ReqField_Port, 1139)

Flag = ApiDriver.csi_order_setrequest(OrderCtx, ReqCtx)
if ApiDriver.bStat <> Succeed Then
	ProcRespone = "fail"
	ProcErrMessage = "Error: " & ApiDriver.csi_util_errorstr(ApiDriver.csi_order_error(OrderCtx))
	Set ApiDriver = nothing
	Exit Function
End If

'Get expiration date of credit card''''
ccExp_Date = Replace(CARD_EXP,"/","")
ccExp_Date = Replace(ccExp_Date,"-","")
ccExp_Date = Replace(ccExp_Date," ","")
'''''''''''''''''''''''''''''''''''''''

Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Userid, CStr(ORDER_ID))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bname, CStr(CARD_NAME))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bcompany, CStr(COMPANY))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Baddr1, CStr(ADDRESS_2))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bcity, CStr(CITY))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bstate, CStr(STATE))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bzip, CStr(ZIP))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Bcountry, CStr(COUNTRY))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Sname, CStr(SHIP_NAME))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Saddr1, CStr(SHIP_ADDRESS_1))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Scity, CStr(SHIP_CITY))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Sstate, CStr(SHIP_STATE))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Szip, CStr(SHIP_ZIP))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Scountry, CStr(SHIP_COUNTRY))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Phone, CStr(PHONE))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Fax, CStr(FAX))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Comments, CStr(SHIP_MESSAGE))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Cardnumber, CStr(CARD_NO))

'Set Flag for Authorization Only or Charge
If MerchantType = "authonly" Then
	Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_ChargeType, Chargetype_Preauth)
ElseIf MerchantType = "authcapture" Then 
	Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_ChargeType, Chargetype_Sale)
End If	

Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Expmonth, CStr(Mid(ccExp_Date, 1, 2)))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Expyear, CStr(Mid(ccExp_Date, 3, 2)))
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Email, CStr(E_MAIL))

'This flag must be edited for testing purposes.  To test it cab set to Result_Good or Result_Decline
Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Result, Result_Live)

Flag = ApiDriver.csi_order_set(OrderCtx, OrderField_Chargetotal, CDbl(GrandTotal))

Flag = ApiDriver.csi_order_process(OrderCtx)

if ApiDriver.bStat <> Succeed Then
	ProcResponse = "fail"
	ProcErrMessage = "Error: " & ApiDriver.csi_util_errorstr(ApiDriver.csi_order_error(OrderCtx))
	Set ApiDriver = nothing
	Exit Function
Else
	ProcMessage = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Time)
	ProcActionCode = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Ref)
	ProcResponseCode = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Approved)
	ProcAuth = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Code)
	ProcErrMsg = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Error)
	ProcCustNumber = ApiDriver.csi_order_get(OrderCtx, OrderField_R_Ordernum)
End If

If ProcResponseCode = "APPROVED" Then
	ProcResponse = "approve"
	ProcAuthCode = Mid(ProcAuth, 1, 6)
	ProcRefCode = Mid(ProcAuth, 7, 10)
	ProcAvsCode = Mid(ProcAuth, 17, 3)
Else
	ProcResponse = "fail"
End If

Flag = ApiDriver.csi_order_drop(OrderCtx)
Flag = ApiDriver.csi_req_drop(ReqCtx)

Set ApiDriver = nothing

End Function
	
%>


