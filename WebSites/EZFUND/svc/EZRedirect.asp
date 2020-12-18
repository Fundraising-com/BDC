<%@ Language=VBScript %>
<% Option Explicit %>
<%
	' Invoke this page as follows:
	'	EZRedirect.asp?URL=http://MyActualTargetPage.asp?arg1=value1&arg2=value2...
	' -or-
	'	EZRedirect.asp  w/FORM field URL=http://MyActualTargetPage.asp?arg1=value1&arg2=value2...
	' EZRedirect will then log the target page, then redirect to it.
	
	Dim sURL

	sURL = Request.QueryString ' unparsed
	If sURL = "" Then sURL = Request.Form("URL")	' QueryString empty? check FORM field for URL!
	If UCase(Left(sURL, 4)) = "URL=" Then
		sURL = Mid(sURL, 5)
	End If

	Response.Redirect sURL
%>
