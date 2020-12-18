<%
'
' StringPrimitives.asp    Primitives for dealing with strings
'
Const LJUST = 1		' Left justify
Const RJUST = 2		' Right justify
%>
<script language=VBScript runat=Server>

' ---- Non-null functions

Function nvl (ByVal s)
	On Error Resume Next
    If IsNull(s) Then nvl = "" Else nvl = s
End Function

Function nvs(ByVal s)
	On Error Resume Next
	nvs = "" & s
	If Err Then nvs = "???"
End Function

Function nvn(ByVal n)
	On Error Resume Next
	If IsNumeric(n) Then
		nvn = CDbl(n)
	ElseIf IsNumeric("" & n) Then	' NB: required for DECIMAL data types to work properly
		nvn = CDbl(n)
	Else
		nvn = CDbl(0)
	End If
	If Err Then nvn = CDbl(0)
End Function

Function nvd(ByVal d)
	On Error Resume Next
	If IsDate(d) Then
		nvd = CDate(d)
	Else
		nvd = CDate(0)
	End If
	If Err Then nvd = CDate(0)
End Function

' ---- SQL fragment functions - the choice was between this module and CommonDBUtils

' NOTE: these now require SQS(), below in MISC functions.

Function svs(ByVal s)
	' SQL value of string
	On Error Resume Next
	s = Trim("" & s)
	If s <> "" Then
		svs = SQS(s)
	Else
		svs = "Null"
	End If
	If Err.number <> 0 Then
		svs = SQS(Err.description)	' intentionally cause likely db error
	End If
End Function

Function svn(ByVal n)
	' SQL value of number
	On Error Resume Next
	If IsNumeric(n) Then
		svn = CStr(n)
	Else
		svn = "Null"	' intentionally cause likely db error
	End If
	If Err.number <> 0 Then
		svn = "'" & Err.description & "'"	' intentionally cause likely db error
	End If
End Function

Function svd(ByVal d)
	' SQL value of date
	' NOTE! This is SQL Server specific!
	On Error Resume Next
	If IsDate(d) Then
		svd = "'" & Day(d) & "-" & MonthName(Month(d), True) & "-" & Year(d) & "'"
	Else
		svd = "Null"	' intentionally cause likely db error
	End If
	If Err.number <> 0 Then
		svd = SQS(Err.description)	' intentionally cause likely db error
	End If
End Function

' ---- MISC FUNCTIONS

Function qs (strng)
    qs = Chr(34) & strng & Chr(34)
End Function

Function lj (strng, lenth)
    lj = Left(strng & Space(lenth), lenth)
End Function

Function ljchar (strng, lenth, ch)
    ljchar = Left(strng & String(lenth, ch), lenth)
End Function

Function rj (strng, lenth)
    rj = Right(Space(lenth) & RTrim(strng), lenth)
End Function

Function rjchar (strng, lenth, ch)
    rjchar = Right(String(lenth, ch) & RTrim(strng), lenth)
End Function

' ------- rj a string with '&nbsp;' as left pad - each counts as 1 space
' ------- WARNING:   LEN(rjnb(x)) WILL NOT PRODUCE THE PROPER LENGTH!
function rjnb(strng, lnth)
	dim i, l, t, tnb
	t = rj(strng,lnth)
	tnb = ""
	for i = 1 to lnth
		if mid(t,i, 1) = " " then
			tnb = tnb & "&nbsp;"
		else
			tnb = tnb & mid(t,i,1)
		end if
	next
	rjnb = tnb
end function

Function sz (strng, lenth, whichend)
    If Len(strng) >= lenth Then
        sz = Left(strng, lenth)
    ElseIf whichend And RJUST Then      ' right justify
        sz = Space(lenth - Len(nvl(strng))) & strng
    Else                            	' left justify
        sz = strng & Space(lenth - Len(nvl(strng)))
    End If
End Function

Function DQS(ByVal sString)
    ' Double-Quoted String:
    ' Return the passed string, surrounded by double quotes.
    ' Duplicate any enclosed double-quotes, VB-fashion.
    Dim iChar, nChars
    Dim sChar
    Dim sResult

    sResult = ""
    nChars = Len(sString)
    For iChar = 1 To nChars
        sChar = Mid(sString, iChar, 1)
        If sChar = """" Then
            sResult = sResult & """"""
        Else
            sResult = sResult & sChar
        End If
    Next
    
    DQS = """" & sResult & """"
End Function

Function SQS(ByVal sString)
    ' Single-Quoted String:
    ' Return the passed string, surrounded by single quotes.
    ' Duplicate any enclosed single-quotes.
    Dim iChar, nChars
    Dim sChar
    Dim sResult

    sResult = ""
    nChars = Len(sString)
    For iChar = 1 To nChars
        sChar = Mid(sString, iChar, 1)
        If sChar = "'" Then
            sResult = sResult & "''"
        Else
            sResult = sResult & sChar
        End If
    Next
    
    SQS = "'" & sResult & "'"
End Function

Function ReplaceAll(sReplaceIn, sReplaceThis, sReplaceWith)
    ' In sReplaceIn, replace every occurrence of
    ' sReplaceThis with sReplaceWith.
    ' Replacement is one pass, left to right.
    ' NOTE: args are effectively ByVal.
    Dim iPos
    Dim sResult, sRemainder
    
    If sReplaceThis = "" Then
        ReplaceAll = sReplaceIn
        Exit Function
    End If

    If sReplaceThis = sReplaceWith Then
        ReplaceAll = sReplaceIn
        Exit Function
    End If

    sResult = ""
    sRemainder = sReplaceIn
    Do
        iPos = InStr(sRemainder, sReplaceThis)
        If iPos > 0 Then
            sResult = sResult & Left(sRemainder, iPos - 1) & sReplaceWith
            sRemainder = Mid(sRemainder, iPos + Len(sReplaceThis))
        Else
            sResult = sResult & sRemainder
            sRemainder = ""
        End If
    Loop Until sRemainder = ""
    
    ReplaceAll = sResult
End Function

Function TrimTrailingCrLf(theText)
	Dim iPos
	
	If nvl(theText) = "" Then TrimTrailingCrLf = "": Exit Function
	iPos = Len(theText)
	Do While (iPos > 0)
		If (Asc(Mid(theText,iPos,1)) > 32) Then Exit Do
		iPos = iPos - 1
	Loop
	If iPos < 1 Then
		TrimTrailingCrLf = ""
	Else	
		TrimTrailingCrLf = Mid(theText,1,iPos)
	End If	
End Function

Function GetNextItemFromString(GetFromString, NextItemPos, Delimiter)
    
    ' Get next delimited item from GetFromString.
    ' Caller must set NextItemPos to 1 before the first call.
    ' NextItemPos is advanced past the returned item.
    ' GetFromString itself is not destroyed (it and Delimiter are
    '   passed ByRef only for performance reasons).
    ' It's OK if there is no Delimiter found after the last item;
    '   that item is returned anyway.
    ' Returns "" when there are no more items to return.
    
    ' NOTE: you may nest calls as long as you maintain separate
    ' positions and delimiters; e.g., get a vbCrLf-separated line,
    ' then get tab-separated items on that line.
    
    ' NOTE: for big strings, InStr() with a start position
    ' is MUCH faster than chopping up the string.
    ' If we were to chop up GetFromString as we go,
    ' it could take several SECONDS to perform this function
    ' for a really large string.

    Dim iPos
    Dim sItem

    iPos = InStr(NextItemPos, GetFromString, Delimiter)
    If iPos > 0 Then
        sItem = Mid(GetFromString, NextItemPos, iPos - NextItemPos)
        NextItemPos = iPos + Len(Delimiter)
    Else
        sItem = Mid(GetFromString, NextItemPos)
        NextItemPos = Len(GetFromString) + 1
    End If

    GetNextItemFromString = sItem

End Function

Function StripHTMLTags(ByVal sString)
    ' Strip HTML tags from string:
    ' Return the passed string, minus HTML tags (identified by "<" and ">").
    Dim iChar, nChars
    Dim sChar
    Dim sResult
    Dim bTagStartFound, sCurrentTagString

    sResult = ""
    bTagStartFound = False
    sCurrentTagString = ""
    nChars = Len(sString)
    For iChar = 1 To nChars
        sChar = Mid(sString, iChar, 1)
        If bTagStartFound = True Then
			sCurrentTagString = sCurrentTagString & sChar
			If sChar = ">" Then 
				bTagStartFound = False
				If UCase(sCurrentTagString) = "<BR>" Then sResult = sResult & " "
				If UCase(sCurrentTagString) = "<P>" Then sResult = sResult & " "
				sCurrentTagString = ""
			End If	
        Else
			If sChar = "<" Then
				bTagStartFound = True
				sCurrentTagString = sCurrentTagString & sChar
			Else	
				sResult = sResult & sChar
			End If	
        End If
    Next
    If bTagStartFound = True Then
		sResult = sResult & sCurrentTagString
    End If
    
    StripHTMLTags = sResult
End Function

</script>