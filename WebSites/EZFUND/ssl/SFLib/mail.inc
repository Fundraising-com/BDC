<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System     :   StoreFront 2000										'
'   Author     :   LaGarde, Incorporated								'
'   Notes      :   There are no configurable elements in this file.		'
'																		'
'                         COPYRIGHT NOTICE								'
'															'
'   The contents of this file is protected under the United States		'
'   copyright laws as an unpublished work, and is confidential and		'
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in		'
'   whole or in part without the expressed written permission of		'
'   LaGarde, Incorporated is expressely prohibited.				'
'																		'
'   (c) Copyright 1999 by LaGarde, Incorporated.  All rights reserved.	'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%>
<% 

Function DoMail

If MailMethod = "Simple Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("SimpleMail.smtp.1")
	Mailer.OpenConnection MAILSERVER
	Mailer.SendMail PRIMARY, CUSTOMER, SUBJECT, BODY1
	Mailer.CloseConnection
	Set Mailer = nothing
	End If
	If SendMail_S = "SecondaryMail" Then
	Set Mailer = Server.CreateObject("SimpleMail.smtp.1")
	Mailer.OpenConnection MAILSERVER
	Mailer.SendMail SECONDARY, CUSTOMER, SUBJECT, BODY1
	Mailer.CloseConnection
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("SimpleMail.smtp.1")
	Mailer.OpenConnection MAILSERVER	
	Mailer.SendMail CUSTOMER, PRIMARY, SUBJECT, BODY2
	Mailer.CloseConnection
	Set Mailer = nothing
	End If
	
ElseIf MailMethod = "SimpleMail 2.0" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("SimpleMail.smtp")
	Mailer.OpenConnection MAILSERVER
	Mailer.SendMail PRIMARY, CUSTOMER, SECONDARY, SUBJECT, BODY1
	Mailer.CloseConnection
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("SimpleMail.smtp")
	Mailer.OpenConnection MAILSERVER	
	Mailer.SendMail CUSTOMER, PRIMARY, SECONDARY, SUBJECT, BODY2
	Mailer.CloseConnection
	Set Mailer = nothing
	End If

ElseIf MailMethod = "ASP Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("SMTPsvg.Mailer")
	Mailer.QMessage = TRUE
	Mailer.RemoteHost = MAILSERVER
	Mailer.AddRecipient PRIMARY, PRIMARY
	Mailer.AddRecipient SECONDARY, SECONDARY
	Mailer.FromAddress = CUSTOMER
	Mailer.FromName = CARD_NAME
	Mailer.Subject = SUBJECT
	Mailer.BodyText = BODY1
	Mailer.SendMail
	Set Mailer = Nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("SMTPsvg.Mailer")
	Mailer.QMessage = TRUE
	Mailer.RemoteHost = MAILSERVER
	Mailer.AddRecipient CUSTOMER, CUSTOMER
	Mailer.FromAddress = PRIMARY
	Mailer.FromName = PRIMARY
	Mailer.Subject = SUBJECT
	Mailer.BodyText = BODY2
	Mailer.SendMail
	Set Mailer = Nothing
	End If

ElseIf MailMethod = "CDONTS Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("CDONTS.NewMail")
	Mailer.MailFormat = 0
	Mailer.To = PRIMARY
	Mailer.Bcc = SECONDARY
	Mailer.From = CUSTOMER
	Mailer.Subject = SUBJECT
	Mailer.Body = BODY1
	Mailer.Send
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("CDONTS.NewMail")
	Mailer.MailFormat = 0
	Mailer.To = CUSTOMER
	Mailer.From = PRIMARY
	Mailer.Subject = SUBJECT
	Mailer.Body = BODY2
	Mailer.Send
	Set Mailer = nothing
	End If

ElseIf MailMethod= "AB Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("ABMailer.Mailman")
	Mailer.Clear
	Mailer.SendTo = PRIMARY
	Mailer.ReplyTo = CUSTOMER
	Mailer.MailSubject = SUBJECT
	Mailer.SendCc = SECONDARY
	Mailer.MailDate = ""
	Mailer.ServerAddr = MAILSERVER
	Mailer.MailMessage = BODY1
	Mailer.SendMail
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("ABMailer.Mailman")
	Mailer.Clear
	Mailer.SendTo = CUSTOMER
	Mailer.ReplyTo = PRIMARY
	Mailer.MailSubject = SUBJECT
	Mailer.MailDate = ""
	Mailer.ServerAddr = MAILSERVER
	Mailer.MailMessage = BODY2
	Mailer.SendMail
	Set Mailer = nothing
	End If

ElseIf MailMethod = "Bamboo Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("Bamboo.SMTP")
	Mailer.Server = MAILSERVER
	Mailer.RCPT = PRIMARY
	Mailer.From = CUSTOMER
	Mailer.FromName = CUSTOMER
	Mailer.Subject = SUBJECT
	Mailer.Message = BODY1
	Mailer.Send
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("Bamboo.SMTP")
	Mailer.Server = MAILSERVER
	Mailer.RCPT = CUSTOMER
	Mailer.From = PRIMARY
	Mailer.FromName = PRIMARY
	Mailer.Subject = SUBJECT
	Mailer.Message = BODY2
	Mailer.Send
	Set Mailer = nothing
	End If

ElseIf MailMethod = "J Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("JMail.SMTPMail")
	Mailer.ServerAddress = MAILSERVER
	Mailer.Sender = CUSTOMER
	Mailer.SenderName = CARD_NAME
	Mailer.AddRecipientEx PRIMARY, PRIMARY
	Mailer.AddRecipientEx SECONDARY, SECONDARY
	Mailer.Subject = SUBJECT
	Mailer.Body = BODY1
	Mailer.Execute
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("JMail.SMTPMail")
	Mailer.ServerAddress = MAILSERVER
	Mailer.Sender = PRIMARY
	Mailer.SenderName = PRIMARY
	Mailer.AddRecipientEx CUSTOMER, CUSTOMER
	Mailer.Subject = SUBJECT
	Mailer.Body = BODY2
	Mailer.Execute
	Set Mailer = nothing
	End If
		
ElseIf MailMethod = "OCX Mail" Then
	If SendMail_M = "PrimaryMail" Then
	Set Mailer = Server.CreateObject("ASPMail.ASPMailCtrl.1")
	Mailer.SendMail MAILSERVER, PRIMARY, CUSTOMER, SUBJECT, BODY1
	Mailer.SendMail MAILSERVER, SECONDARY, CUSTOMER, SUBJECT, BODY1
	Set Mailer = nothing
	End If
	If SendMail_C = "CustomerMail" Then
	Set Mailer = Server.CreateObject("ASPMail.ASPMailCtrl.1")
	Mailer.SendMail MAILSERVER, CUSTOMER, PRIMARY, SUBJECT, BODY2
	Set Mailer = nothing
	End If
ElseIf MailMethod = "No Mail" Then
End If
End Function
%>