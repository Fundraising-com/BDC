<% 
' ExtractRequestParam.asp
'
' Generic routine to extract a param from a Form or Querystring
' variable.  This code is repeated time and time again!
' This routine assumes the variable sUserFeedbackMessage is 
' being managed by caller.
'
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
' NOTE:  Caller must define the variable sUserFeedbackMessage; 
'        used by ExtractxxxParams() to return error messages
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

Function ExtractOneRequestParam(theLabel, theParamName, theParamValue, theDefaultValue, bIsRequired)
	' This function extracts a param and provides minimal validation.
	' If additional validation is required, caller should wrap this 
	' function and perform the necessary validation on theParamValue.
	If Request.Form.Count > 0 Then theParamValue = Request.Form(theParamName)
	If theParamValue = "" Then theParamValue = Request.QueryString(theParamName)
	If theParamValue = "" Then theParamValue = theDefaultValue

	' NB: Assumption that "" is not allowed as a valid required value.
	If bIsRequired = True And theParamValue = "" Then
		sUserFeedbackMessage = sUserFeedbackMessage & "You must enter " & theLabel & ".<br>"
		ExtractOneRequestParam = False
	Else
		ExtractOneRequestParam = True
	End If

End Function
%>
