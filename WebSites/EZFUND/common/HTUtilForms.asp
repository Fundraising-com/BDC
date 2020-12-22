<script language=VBScript runat=Server>
'
' HTUtilForms.asp    Common HTML Form routines used by ATC Web apps
'
Function HTFormPost (ByVal aAction)
    HTFormPost = "<form method=post" & attr("action", aAction) & ">"
End Function

Function HTFormGet (ByVal aAction)
    HTFormPost = "<form method=get" & attr("action", aAction) & ">"
End Function

Function HTReset (ByVal aAttrs, ByVal aValue)
    HTReset = "<input type=reset" & attr("value", aValue) & aAttrs & ">"
End Function

Function HTSubmit (ByVal aAttrs, ByVal aValue)
    HTSubmit = "<input type=submit" & attr("value", aValue) & aAttrs & ">"
End Function

Function HTInputText (ByVal aName, ByVal aValue, ByVal aSize, ByVal aMaxLength, ByVal bBreakAfter)
    Dim br
    If bBreakAfter = true Then br = "<br>" Else br = "" 
    HTInputText = "<input type=text" & attr("name", aName) & attr("value", aValue) & _
		attr("size", aSize) & attr("maxlength", aMaxLength) & ">" & br
End Function

Function HTHidden (ByVal aName, ByVal aValue)
    HTHidden = "<input type=hidden" & attr("name", aName) & attr("value", aValue) & ">"
End Function

Function HTSelect (ByVal aName, ByVal aSize)
    Dim siz
    If nvn(aSize) = 0 Then siz = "" Else siz = attr("size",aSize)
    HTSelect = "<select" & attr("name", aName) & attr("id",aName) & siz & ">"
End Function

Function HTSelectEnd ()
    HTSelectEnd = "</select>"
End Function

Function HTOption (ByVal aValue, ByVal sText, ByVal bSelected)
    Dim selec, valu
    If bSelected Then selec = " selected" Else selec = ""
    If nvl(aValue) = "" Then valu = "" else valu = attr("value",aValue)
    HTOption = "<option" & valu & selec & ">" & sText
End Function

Function HTRadio (ByVal aName, ByVal aValue, ByVal sText, ByVal bChecked)
    Dim s
    s = "<input type=radio" & attr("name",aName) & attr("value",aValue)
    If bChecked = true Then s = s & " checked"
    s = s & ">" & sText
    HTRadio = s
End Function

Function HTCheckbox (ByVal aName, ByVal aValue, ByVal sText, ByVal bChecked)
    Dim s
    s = "<input type=checkbox" & attr("name",aName) & attr("value",aValue)
    If bChecked = true Then s = s & " checked"
    s = s & ">" & sText
    HTCheckbox = s
End Function

Function HTTextArea (ByVal aName, ByVal aRows, ByVal aCols, ByVal sText)
    HTTextArea = "<textarea" & attr("name",aName) & attr("rows",aRows) & attr("cols",aCols) & ">" & nvl(sText) & "</textarea>"
End Function

Function HTFormEnd ()
    HTFormEnd = "</form>"
End Function
</script>