<%
' StateCodeInclude.asp - State code (name) include

' REMOVE THIS!  These were the params we used for CCE/CTP sites.
' Since the name of the control is passed as an argument to these
' routines it may be best to keep the param names out of this file.

' Name of param variable (Form or Querystring)
'Const cStateCodeParam = "StateCode"
' Variable to store the extracted param
'Dim sParamStateCode


Function EmitOneStateOption(sCode, sName, sValue)
	' Push a single option line
	Response.Write "<option value=""" & sCode & """"
	If sCode = sValue Then Response.Write " selected"
	Response.Write ">" & sName
End Function


' ---- Emit a State code dropdown, with optional selected item

Function EmitStateCodeOptionList(sValue)
' The following include file contains a script line for each item
' in the list, such as...
' Note: This file only contains States, no (select one) line.
'
'	Call EmitOneStateOption("AK", "AK", sValue)
%>
<!--#include virtual="common/EmitStateCodeOptionInclude.asp"-->
<%
End Function

Function EmitStateCodeComboBox(theName, theDflt, sAllText)
	Response.Write "<select name=""" & theName & """>"
	If sAllText <> "" Then
		Call EmitOneStateOption("", sAllText, theDflt)	' used for (select one) entry
	End If
	Call EmitStateCodeOptionList(theDflt)
	Response.Write "</select>"
End Function

Function EmitStateCodeComboBoxExt(theName, theDflt, sAllText, sAdditional)
	' This function was added to allow sticking JavaScript events in the select tag
	' w/o breaking EmitStateCodeComboBox() above.
	Response.Write Trim("<select name=""" & theName & """ " & sAdditional) & ">"
	If sAllText <> "" Then
		Call EmitOneStateOption("", sAllText, theDflt)	' used for (select one) entry
	End If
	Call EmitStateCodeOptionList(theDflt)
	Response.Write "</select>"
End Function


' ---- Emit a State name dropdown, with optional selected item

Function EmitStateNameOptionList(sValue)
' The following include file contains a script line for each item
' in the list, such as...
' Note: This file only contains States, no (select one) line.
'
'	Call EmitOneStateOption("AK", "Alaska", sValue)
%>
<!--#include virtual="common/EmitStateNameOptionInclude.asp"-->
<%
End Function

Function EmitStateNameComboBox(theName, theDflt, sAllText)
	Response.Write "<select name=""" & theName & """>"
	If sAllText <> "" Then
		Call EmitOneStateOption("", sAllText, theDflt)	' used for (select one) entry
	End If
	Call EmitStateNameOptionList(theDflt)
	Response.Write "</select>"
End Function

Function EmitStateNameComboBoxExt(theName, theDflt, sAllText, sAdditional)
	' This function was added to allow sticking JavaScript events in the select tag
	' w/o breaking EmitStateNameComboBox() above.
	Response.Write Trim("<select name=""" & theName & """ " & sAdditional) & ">"
	If sAllText <> "" Then
		Call EmitOneStateOption("", sAllText, theDflt)	' used for (select one) entry
	End If
	Call EmitStateNameOptionList(theDflt)
	Response.Write "</select>"
End Function

%>
