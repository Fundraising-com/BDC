<script language=VBScript runat=Server>
'
' EmailUtils.asp	Common E-mail routines used by EZ Web apps
'
Function IsEmailAddress(theEmailAddress)
	' Simple check for EmailAddress format
	' REPAIRED 6/13/2003 SSB - by chance, it worked right before, but only by chance.
	Dim bValidEmailChars
	If Instr(theEmailAddress,"@") <= 0 Then
		bValidEmailChars = False
	ElseIf Instr(theEmailAddress,".") <= 0 Then
		bValidEmailChars = False
	ElseIf Instr(theEmailAddress," ") > 0 Then
		bValidEmailChars = False
	Else
		bValidEmailChars = True
	End If
	IsEmailAddress = bValidEmailChars
End Function

Function SendMail(MailTo, MailFrom, MailCC, MailBcc, theSubject, theBody)
	Dim objMail
	' Create mail component and send the request
	On Error Resume Next
	Err.Clear
	Set objMail = CreateObject("CDONTS.NewMail")

	' MSKB (Q201352) - PRB: Mail format TEXT limits line length to 74 characters!	
	objMail.BodyFormat = 1		' Body format Text
	objMail.MailFormat = 0		' Mail format MIME

	objMail.To = MailTo
	objMail.From = MailFrom
	objMail.Cc = MailCC
	objMail.Bcc = MailBcc
	objMail.Subject = theSubject
	objMail.Body = theBody
	objMail.Send
	Set objMail = Nothing	
	SendMail = (Err = 0)
End Function

Function SendHTMLMail(MailTo, MailFrom, MailCC, MailBcc, theSubject, theBody, theAttachFile)
	Dim objMail
	' Create mail component and send the request (as HTML email)
	On Error Resume Next
	Err.Clear
	Set objMail = CreateObject("CDONTS.NewMail")

	' MSKB (Q201352) - PRB: Mail format TEXT limits line length to 74 characters!	
	objMail.BodyFormat = 0		' Body format HTML
	objMail.MailFormat = 0		' Mail format MIME

	objMail.To = MailTo
	objMail.From = MailFrom
	objMail.Cc = MailCC
	objMail.Bcc = MailBcc
	objMail.Subject = theSubject
	objMail.Body = theBody
	If theAttachFile <> "" Then objMail.AttachFile(theAttachFile)
	objMail.Send
	Set objMail = Nothing	
	SendHTMLMail = (Err = 0)
End Function
</script>