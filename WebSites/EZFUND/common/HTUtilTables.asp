<script language=VBScript runat=Server>
'
' HTUtilTables.asp    Common HTML Table routines used by ATC Web apps
'
Public Function HTTable(ByVal sParams)
	If sParams <> "" Then
		HTTable = "<table" & RTrim(" " & LTrim(sParams)) & ">"
	Else
		HTTable = "<table>"
	End If
End Function

Public Function HTTableEnd()
	HTTableEnd = "</table>" & vbCrLf
End Function

Public Function HTTableRow(ByVal sParams)
		HTTableRow = "<tr" & RTrim(" " & LTrim(sParams)) & ">"
End Function

Public Function HTTableRowEnd()
		HTTableRowEnd = "</tr>"
End Function

Public Function HTTableCell(ByVal sParams, ByVal sData)
		HTTableCell = "<td" & RTrim(" " & LTrim(sParams)) & ">" & sData & "</td>"
End Function
</script>