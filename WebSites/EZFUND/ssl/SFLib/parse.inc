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
	
Function MakePairs
	Set ObjResult = CreateObject("Scripting.Dictionary")

	strQuery = StrIn
	intSep = Instr(strQuery, "&")
	Do While intSep
	strKey = ""
	strValue = ""
	strNVPair = Left(strQuery, intSep - 1)
	strQuery = Mid(strQuery, intSep + 1)
	intEqu = Instr(strNVPair, "=")
	If intEqu > 1 Then strKey = URLDecode(Left(strNVPair, intEqu-1))
	If intEqu < Len(strNVPair) Then strValue = URLDecode(Mid(strNVPair, intEqu+1))
	objResult.Add strKey, strValue
	intSep = instr(strQuery, "&")
	Loop
	strKey = ""
	strValue = ""
	intEqu = Instr(strQuery, "=")
	If intEqu > 1 Then strKey = URLDecode(Left(strQuery, intEqu-1))
	If intEqu < Len(strQuery) Then strValue = URLDecode(Mid(strQuery, intEqu+1))
	objResult.Add strKey, strValue
	
	strKeysArray = objResult.Keys
	strItemsArray = objResult.Items
	For intLoop = 0 To objResult.Count - 1
	strThisKey = strKeyArray(intLoop)
	strThisItem = strItemsArray(intLoop)
	Response.Write strThisKey & " = " & strthisItem& "<BR>"
End Function

AuthResp = ObjResult.Items()


	
%>