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
Function URLDecode(strToDecode)
	strIn = strToDecode
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
	End Function
	
%>