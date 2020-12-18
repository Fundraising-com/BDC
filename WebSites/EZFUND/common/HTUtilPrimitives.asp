<%
'
' HTUtilPrimitives.asp    Common HTML utilities used by ATC Web apps
'
Const AlignLeft =   " align=""left"""
Const AlignCenter = " align=""center"""
Const AlignRight =  " align=""right"""

Const ColorRed =	" color=""red"""
Const ColorGreen =	" color=""green"""
%>	
<script language=VBScript runat=Server>
Function Width(ByVal wval)
	Width = attr("width",wval)
End Function
	
Function Size(ByVal sval)
	Size = attr("size",sval)
End Function

Function attr(ByVal tag, ByVal value)
	attr = " " & LTrim(tag) & "=""" & value & """"
End Function

Function HTWrap(ByVal tag, ByVal attribs, ByVal display)
	HTWrap = "<" & tag & RTrim(" " & LTrim(attribs)) & ">" & display & "</" & tag & ">"
End Function

Function HTAnchor (ByVal link, ByVal attribs, ByVal display)
    HTAnchor = HTWrap("a", attr("name",link) & RTrim(" " & LTrim(attribs)), display)
End Function

Function HTHREF (ByVal link, ByVal attribs, ByVal display)
    HTHREF = HTWrap("a", attr("href",link) & RTrim(" " & LTrim(attribs)), display)
End Function
	
Function HTBold (ByVal display)
    HTBold = HTWrap("b", "", display)
End Function

Function HTItalic (ByVal display)
    HTItalic = HTWrap("i", "", display)
End Function

Function HTCenterText (ByVal display)
    HTCenterText = HTWrap("center", "", display)
End Function

Function HTFontText(ByVal attribs, ByVal theText)
	HTFontText = "<font" & dfltFontFace & dfltFontSize & RTrim(" " & LTrim(attribs)) & ">" & theText & "</font>"
End Function

Function HTTinyFontText(ByVal attribs, ByVal theText)
	HTTinyFontText = "<font" & dfltFontFace & Size(1) & RTrim(" " & LTrim(attribs)) & ">" & theText & "</font>"
End Function

Function HTFont(ByVal useDfltFont, ByVal attribs)
	HTFont = "<font" & InclIf(useDfltFont,dfltFontFace & dfltFontSize,"") & RTrim(" " & LTrim(attribs)) & ">"
End Function

Function HTFontEnd()
	HTFontEnd = "</font>"
End Function
</script>
