<%	
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
' EZSecurePageInclude.asp
'
' NOTE: INSERT AT TOP OF PAGE REQUIRING SECURE CONNECTION.
'
' This inline code checks to make sure the request for this
' page is coming through a secure socket (SSL).  If not we
' redirect back to ourselves with the (https:) prefix!
' NOTE! THIS CODE ONLY RUNS ON THE PRODUCTION SERVER!
' (WE DON'T HAVE A CERTIFICATE ON THE DEVELOPMENT SERVER.)
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
Dim SecurePage
If LCase(Request.ServerVariables("SERVER_NAME")) = "rdezf.atchou.com" Then
	SecurePage = False
Else
	If UCase(Request.ServerVariables("HTTPS")) <> "ON" Then
		Response.Redirect "https://" & Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("URL")
		Response.End
	End If
	SecurePage = True
End If

' ----- SUPPORT FUNCTIONS -----
Function EmitSecurePageHeader(theHeader)
	' Display the page header with a secure transaction image
	Response.Write "<table border=0 cellpadding=0 cellspacing=0 width=""560"" bgcolor=""#FFFFFF"">"
	Response.Write "<tr>"
	Response.Write "<td align=""left""><font face=""Verdana,Arial,Helvetica,Sans"" size=4><b>" & theHeader & "</b></font></td>"
	If SecurePage = True Then
		Response.Write "<td align=""right"" valign=""top""><img src=""/images/secure.jpg""></td>"
	Else
		Response.Write "<td align=""right"" valign=""top""><font face=""Verdana,Arial,Helvetica,Sans"" size=1 color=red><b>WARNING:<br>NON-SECURE page!</b></font></td>"
	End If	
	Response.Write "</tr>"
	Response.Write "</table>"
End Function

Function EmitSecureCertificateLogo()
	' Display the logo of our Secure certificate provider
	If SecurePage = True Then
		Response.Write "<br><img src=""/images/thawte.gif""><br>"
	End If	
End Function
%>