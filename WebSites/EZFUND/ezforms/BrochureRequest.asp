<%@ LANGUAGE="VBScript" %>
<% Option Explicit %>
<!--#include virtual="common/EZPageTop.asp"-->
<!--#include virtual="common/ExtractRequestParam.asp"-->
<!--#include virtual="common/EZCommonDBUtils.asp"-->
<!--#include virtual="common/EZLeadsDBUtils.asp"-->
<!--#include virtual="common/EZMainDBUtils.asp"-->
<!--#include virtual="common/HTUtilForms.asp"-->
<!--#include virtual="common/HTUtilTables.asp"-->
<!--#include virtual="common/StateCodeInclude.asp"-->
<!--#include virtual="ezforms/EZFormCommonInclude.asp"-->
<!--#include virtual="ezforms/EZFormRequestInclude.asp"-->
<%	
' ----------------------------------------------------------------------------------------------------
' 04/23/04 - This Brochure FORM is officially retired!  Redirect user to our NEW ALL PRODUCT FORM!
Response.Redirect "/ezforms/ProductInfoRequest.asp"
Response.End
' ----------------------------------------------------------------------------------------------------
%>
<%
' BROWSER FIX:  Correct MSIE 4.01 (pre SP2) bug warning that "Page has Expired".
Response.CacheControl = "Public" 
Response.Expires = 0
%>
<html>
<head>
<title>EZFund.com - Request for brochure information and free catalog</title>
<!--#include virtual="ezforms/EZFormJSInclude.asp"-->
</head>
<body bgcolor="#FFFFFF">
<p align="left"><img border="0" src="/images/EZ_new_logo.gif" width="300" height="56"></p>
<font face="Verdana,Arial,Helvetica,Sans" size=4><b>Request for brochure information and free catalog</b></font><p>
<%
	' ---------- Start of Main ----------
	' Define the path and name of our script
	Const cOurEZFormASP = "/ezforms/BrochureRequest.asp"
	
	Call ExtractRequestParams(cBrochureRequest)
	
	If sUserFeedbackMessage <> "" Then
		Response.Write "<font color=red>" & sUserFeedbackMessage & "</font><p>"
		ActionVerb = ""		' don't allow a SAVE operation, just display the FORM
	End If

	Select Case ActionVerb

		Case cSaveAction:
			' Process the FORM request!
			Call ProcessFormRequest(cBrochureRequest)

		Case Else:	
			' Display the request FORM
			Call EmitRequestForm(cBrochureRequest)

	End Select		
%>
<!--#include virtual="ezforms/EZFormFooter.asp"-->
</body>
</html>
