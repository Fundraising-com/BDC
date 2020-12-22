'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Net.Mail
Imports StoreFront.SystemBase

Imports System
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.Management

Namespace Email

#Region "Class CEMailBase"
    Public Class CEMailBase

#Region "Class Members"
        Protected m_strError As String

        Protected m_strSubject As String
        Protected m_strBody As String
        Protected m_strTo As String
        Protected m_strFrom As String
        Protected m_strCC As String
        Protected m_strMailMethod As String
        Protected m_strMailServer As String
        Protected m_strMailFormat As String = "TEXT"
        Protected m_objMessages As CXMLMessages
#End Region

#Region "Class Properties"
#Region "Property From() As String"
        Public Property From() As String
            Get
                Return m_strFrom
            End Get
            Set(ByVal Value As String)
                m_strFrom = Value.Trim
            End Set
        End Property
#End Region

#Region "Property [To]() As String"
        Public Property [To]() As String
            Get
                Return m_strTo
            End Get
            Set(ByVal Value As String)
                m_strTo = Value.Trim
            End Set
        End Property
#End Region

#Region "Property CC() As String"
        Public Property CC() As String
            Get
                Return m_strCC
            End Get
            Set(ByVal Value As String)
                m_strCC = Value.Trim
            End Set
        End Property
#End Region

#Region "Property Body() As String"
        Public Property Body() As String
            Get
                Return m_strBody
            End Get
            Set(ByVal Value As String)
                Value = Value.Trim
                If m_strMailFormat = "HTML" Then
                    m_strBody = Value
                Else
                    m_strBody = Value.Replace("&vbcr;", vbCrLf)
                End If
            End Set
        End Property
#End Region

#Region "Property Subject() As String"
        Public Property Subject() As String
            Get
                Return m_strSubject
            End Get
            Set(ByVal Value As String)
                Dim x As Integer
                Value = Value.Trim
                m_strSubject = ""
                For x = 0 To Value.Length - 1
                    m_strSubject = m_strSubject & Chr(Asc(Value.Substring(x, 1)))
                Next

            End Set
        End Property
#End Region

#Region "Property MailMethod() As String"
        Public Property MailMethod() As String
            Get
                Return m_strMailMethod
            End Get
            Set(ByVal Value As String)
                m_strMailMethod = Value.Trim
            End Set
        End Property
#End Region

#Region "Property MailServer() As String"
        Public Property MailServer() As String
            Get
                Return m_strMailServer
            End Get
            Set(ByVal Value As String)
                m_strMailServer = Value.Trim
            End Set
        End Property
#End Region

#Region "Property MailFormat() As String"
        Public Property MailFormat() As String
            Get
                Return m_strMailFormat
            End Get
            Set(ByVal Value As String)
                If (Value.Trim <> "") Then
                    m_strMailFormat = Value.ToUpper.Trim
                End If
            End Set
        End Property
#End Region

#Region "ReadOnly Property ErrorMessage() As String"
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return m_strError
            End Get
        End Property
#End Region
#End Region

#Region "Overridable Sub SendEMail()"
        Public Overridable Sub SendEMail()
        End Sub
#End Region

#Region "Protected Sub SetErrorMessage(ByVal strMessage As String)"
        Protected Sub SetErrorMessage(ByVal strMessage As String)
            If (m_strError <> "") Then
                m_strError = m_strError & "<br>" & strMessage
            Else
                m_strError = strMessage
            End If
        End Sub
#End Region

#Region "Function CheckError() As Boolean"
        Protected Function CheckError() As Boolean
            m_strError = ""
            m_objMessages = StoreFrontConfiguration.MessagesAccess
            If ([To]() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankTo"))
            End If
            If (From() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankFrom"))
            End If
            If (Subject() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankSubject"))
            End If
            If (Body() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankBody"))
            End If
            If (MailMethod() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankMethod"))
            End If
            If (MailServer() = "") Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "BlankServer"))
            End If
            If ((MailFormat() <> "HTML") And (MailFormat() <> "TEXT")) Then
                SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "InvalidFormat"))
            End If

            If (ErrorMessage <> "") Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

        '--- Custom Case Insensitive Replace Function CReplace
        '---	VB.NET Loop Version
        Public Function CReplace(ByVal strExpression As String, ByVal strSearch As String, ByVal strReplace As String) As String
            Dim strReturn As String
            Dim lngPosition As Long
            Dim strTemp As String


            strReturn = ""
            strSearch = strSearch.ToUpper()
            strTemp = strExpression.ToUpper()
            lngPosition = strTemp.IndexOf(strSearch)
            Do While lngPosition >= 0
                strReturn = strReturn + strExpression.SubString(0, lngPosition) + strReplace
                strExpression = strExpression.SubString(lngPosition + strSearch.Length)
                strTemp = strTemp.SubString(lngPosition + strSearch.Length)
                lngPosition = strTemp.IndexOf(strSearch)
            Loop
            strReturn = strReturn + strExpression

            CReplace = strReturn
        End Function

    End Class
#End Region

#Region "Class CEmail"
    Public Class CEmail
        Inherits CEMailBase

#Region "Overridable Sub SendEmail()"
        Public Overrides Sub SendEmail()
            If (CheckError()) Then
                Throw New Exception(m_strError)
            End If

            Try
                Select Case MailMethod()
                    Case "CDONTS Mail"
                        CDONTS()
                    Case "CDOSYS Mail"
                        CDONTS()
                    Case "CDONTS/CDOSYS Mail"
                        CDONTS()
                    Case "ASP Mail"
                        ASPMail()
                    Case "ASP QMail"
                        ASPQMail()
                    Case "SimpleMail 3"
                        SimpleMail3()
                    Case "J Mail"
                        JMail()
                    Case "ASP Email"
                        ASPEmail()
                    Case "AB Mail"
                        ABMail()
                    Case "OCX Mail"
                        OCXMail()
                    Case Else
                        SetErrorMessage(m_objMessages.GetXMLMessage("CEMailBase", "Error", "InvalidMethod"))
                        Throw New Exception(m_strError)
                End Select
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub CDONTS()"
        Private Sub CDONTS()
            Dim mailer As New MailMessage(m_strFrom.Trim, m_strTo.Trim)
 
            mailer.Subject = m_strSubject
            mailer.Body = m_strBody
            If m_strCC <> "" Then
                mailer.CC.Add(m_strCC.Trim)
            End If

            If m_strMailFormat.ToLower = "html" Then
                mailer.IsBodyHtml = True
            Else
                mailer.IsBodyHtml = False
            End If
            mailer.BodyEncoding = Text.Encoding.UTF8
            Dim client As New SmtpClient(m_strMailServer)

            Try
                '  SmtpMail.Send(mailer.From, mailer.To, mailer.Subject, mailer.Body)
                If mailer.From.Address <> "" And mailer.To.Count > 0 Then
                    client.Send(mailer)
                End If
            Catch objErr As Exception
                '  Throw New Exception(objErr.Message)
                m_strError = objErr.Message
            End Try
        End Sub
#End Region

#Region "Sub ASPMail()"
        Private Sub ASPMail()
            Dim mailer As Object
            Dim ret As Boolean

            Try
                mailer = CreateObject("SMTPsvg.Mailer")
                mailer.QMessage = False
                mailer.RemoteHost = m_strMailServer
                mailer.AddRecipient(m_strTo, m_strTo)
                If m_strCC <> "" Then
                    mailer.AddRecipient(m_strCC, m_strCC)
                End If
                If m_strMailFormat = "HTML" Then
                    mailer.ContentType = "text/html"
                Else
                    mailer.ContentType = "text/plain"
                End If

                mailer.FromAddress = m_strFrom
                mailer.FromName = m_strFrom
                mailer.Subject = m_strSubject
                mailer.BodyText = m_strBody
                ret = mailer.SendMail()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub ASPQMail()"
        Private Sub ASPQMail()
            Dim mailer As Object
            Dim ret As Boolean

            Try
                mailer = CreateObject("SMTPsvg.Mailer")
                mailer.QMessage = True
                mailer.RemoteHost = m_strMailServer
                mailer.AddRecipient(m_strTo, m_strTo)
                If m_strCC <> "" Then
                    mailer.AddRecipient(m_strCC, m_strCC)
                End If
                If m_strMailFormat = "HTML" Then
                    mailer.ContentType = "text/html"
                Else
                    mailer.ContentType = "text/plain"
                End If
                mailer.FromAddress = m_strFrom
                mailer.FromName = m_strFrom
                mailer.Subject = m_strSubject
                mailer.BodyText = m_strBody
                ret = mailer.SendMail()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub SimpleMail3()"
        Private Sub SimpleMail3()
            Dim mailer As Object

            Try
                mailer = CreateObject("ADISCON.SimpleMail.1")         ' create mailer!

                mailer.MailServer = m_strMailServer
                mailer.Sender = m_strFrom
                mailer.Recipient = m_strTo
                If m_strCC <> "" Then
                    mailer.CCRecipient = m_strCC
                End If
                If m_strMailFormat = "HTML" Then
                    mailer.MessageHTMLText = m_strBody
                Else
                    mailer.MessageText = m_strBody
                End If
                mailer.Subject = m_strSubject

                mailer.Send()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub OCXMail()"
        Private Sub OCXMail()
            Dim ret As String
            Dim mailer As Object

            Try
                mailer = CreateObject("ASPMail.ASPMailCtrl.1")

                Dim sCharSet As String
                sCharSet = "utf-8"

                If m_strMailFormat = "HTML" Then
                    mailer.XHeader("Content-Type", "text/html; charset=" & Chr(34) & sCharSet & Chr(34))
                Else
                    mailer.XHeader("Content-Type", "text/plain; charset=" & Chr(34) & sCharSet & Chr(34))
                End If

                'this next section is for CC

                If m_strCC <> "" Then
                    ret = mailer.SendX(m_strMailServer, m_strFrom, m_strFrom, "", "", m_strTo, m_strCC, "", "", m_strSubject, m_strBody)
                Else
                    ret = mailer.SendMail(m_strMailServer, m_strTo, m_strFrom, m_strSubject, m_strBody)
                End If

            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub JMail()"
        Private Sub JMail()
            Dim mailer As Object

            Try
                mailer = CreateObject("JMail.SMTPMail")

                mailer.ServerAddress = m_strMailServer
                mailer.Sender = m_strFrom
                mailer.SenderName = m_strFrom
                mailer.AddRecipient(m_strTo)
                If m_strCC <> "" Then
                    mailer.AddRecipientCC(m_strCC)
                End If
                If m_strMailFormat = "HTML" Then
                    mailer.ContentType = "text/html"
                Else
                    mailer.ContentType = "text/plain"
                End If
                mailer.Subject = m_strSubject
                mailer.Body = m_strBody
                mailer.Execute()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub ASPEmail()"
        Private Sub ASPEmail()
            Dim Mailer As Object

            Try
                Mailer = CreateObject("Persits.MailSender")
                Mailer.Host = m_strMailServer
                Mailer.From = m_strFrom
                Mailer.FromName = m_strFrom
                Mailer.AddReplyTo(m_strFrom)
                Mailer.AddAddress(m_strTo)
                If m_strCC <> "" Then
                    Mailer.AddAddress(m_strCC)
                End If
                If m_strMailFormat = "HTML" Then
                    Mailer.IsHTML = True
                Else
                    Mailer.IsHTML = False
                End If
                Mailer.Subject = m_strSubject
                Mailer.Body = m_strBody

                Mailer.Send()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region

#Region "Sub ABMail()"
        Private Sub ABMail()
            Dim mailer As Object

            Try
                mailer = CreateObject("ABMailer.Mailman")

                '-- Set the Mail Properties
                mailer.Clear()
                mailer.MailSubject = m_strSubject
                mailer.FromAddress = m_strFrom
                mailer.FromName = m_strFrom
                mailer.ReplyTo = m_strFrom
                mailer.SendTo = m_strTo
                mailer.ServerAddr = m_strMailServer
                If m_strCC <> "" Then
                    mailer.SendCc = m_strCC
                End If
                If m_strMailFormat = "HTML" Then
                    mailer.MessageType = 1
                Else
                    mailer.MessageType = 0
                End If
                '-- Set Message Body
                mailer.MailMessage = m_strBody

                '-- Fire off the email message
                mailer.SendMail()
            Catch objErr As Exception
                Throw New Exception(objErr.Message)
            End Try
        End Sub
#End Region
    End Class
#End Region

#Region "Class CConfirmationEmail"
    Public Class CConfirmationEmail
        Inherits Email.CEmail

#Region "Class Members"

	 Private objContent As CXMLEMailContent
        Private objBillingContent As CXMLEMailContent
        Private objShippingContent As CXMLEMailContent
        Private objProductsContent As CXMLEMailContent
        Private objOrderTotalContent As CXMLEMailContent
        Private objEmail As New CEMailContent()
        Private m_objOrder As COrder
        Private m_objCustomer As CCustomer
        Private strProductsContent As String = ""
        Private arEmpty As ArrayList  'dim for backwards compat
        Private objAdmin As Admin.CEmail

#End Region        

#Region "Public Sub SendConfirmationEmail(ByVal m_objOrder As COrder, ByVal m_objCustomer As CCustomer, ByVal m_arEMailContent As ArrayList)"
        Public Sub SendConfirmationEmail(ByVal objOrder As COrder, ByVal objCustomer As CCustomer, ByVal m_arEMailContent As ArrayList, Optional ByVal sendToCustomer As Boolean = True, Optional ByVal sendToVendor As Boolean = True, Optional ByVal sendToMerchant As Boolean = True)
            m_objOrder = objOrder
            m_objCustomer = objCustomer
            objAdmin = New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim strBillingContent As String = ""
            Dim strShippingContent As String = ""
            Dim strOrderTotalContent As String = ""
            strBillingContent = Me.GetBillingContent
            objProductsContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Products)
            If (objProductsContent.IsActive = 1) Then
                strProductsContent = objProductsContent.Body
            End If
            strShippingContent = GetShippingContent()
            strOrderTotalContent = GetOrderTotalContent()
            If sendToCustomer Then SendShopperEmail(strOrderTotalContent, strBillingContent, strShippingContent)
            'update #2344
            If Not (New CXMLPaymentMethodAccess().GetPaymentMethodName(objOrder.PaymentMethod).ToLower = "phonefax") AndAlso sendToVendor Then
                SendVendorEmail(strOrderTotalContent, strBillingContent)
            End If
            If sendToMerchant Then SendMerchantEmail(strBillingContent, strShippingContent)
        End Sub
#End Region

#Region "Private Function GetBillingContent() As String"

	  Private Function GetBillingContent() As String
            objBillingContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Billing_Info)
            Dim strBillingContent As String = ""
            Dim objMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess

            If (objBillingContent.IsActive = 1) Then
                strBillingContent = objBillingContent.Body
                strBillingContent = strBillingContent.Replace("[BillingName]", m_objOrder.BillAddress.Name)
                strBillingContent = strBillingContent.Replace("[BillingCompany]", m_objOrder.BillAddress.Company)
                strBillingContent = strBillingContent.Replace("[BillingAddress1]", m_objOrder.BillAddress.Address1)
                strBillingContent = strBillingContent.Replace("[BillingAddress2]", m_objOrder.BillAddress.Address2)
                strBillingContent = strBillingContent.Replace("[BillingCity]", m_objOrder.BillAddress.City)
                strBillingContent = strBillingContent.Replace("[BillingState]", m_objOrder.BillAddress.State)
                strBillingContent = strBillingContent.Replace("[BillingZip]", m_objOrder.BillAddress.Zip)
                strBillingContent = strBillingContent.Replace("[BillingCountry]", m_objOrder.BillAddress.Country)
                If (Trim(m_objOrder.BillAddress.Fax) <> "") Then
                    strBillingContent = strBillingContent.Replace("[BillingFax]", m_objOrder.BillAddress.Fax)
                Else
                    strBillingContent = strBillingContent.Replace("[BillingFax]", "None")
                End If
                strBillingContent = strBillingContent.Replace("[BillingPhone]", m_objOrder.BillAddress.Phone)
                strBillingContent = strBillingContent.Replace("[BillingEMail]", m_objOrder.BillAddress.EMail)
                If objMethodAccess.GetPaymentMethodName(m_objOrder.PaymentMethod).ToLower = "po" Then
                    strBillingContent = strBillingContent.Replace("[BillingPaymentMethod]", objMethodAccess.GetPaymentMethodName(m_objOrder.PaymentMethod) & " " & m_objOrder.OrderPayment.PONumber)
                End If
                strBillingContent = strBillingContent.Replace("[BillingPaymentMethod]", objMethodAccess.GetPaymentMethodName(m_objOrder.PaymentMethod))
            End If
            Return strBillingContent

        End Function

#End Region       

#Region "Private Function GetShippingContent() As String"

	     Private Function GetShippingContent() As String
            Dim strShippingContent As String = ""
            Dim objInventoryMail As New LowStockNotice()
            objShippingContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Shipping_Info)
            If (objShippingContent.IsActive = 1) Then
                Dim objAddress As COrderAddress
                Dim strAddress As String = ""

                ' Associate order items with shipping address
                For Each objAddress In m_objOrder.OrderAddresses
                    strAddress = strAddress & vbCrLf & objShippingContent.Body
                    strAddress = strAddress.Replace("[ShippingName]", objAddress.Address.Name)
                    strAddress = strAddress.Replace("[ShippingCompany]", objAddress.Address.Company)
                    strAddress = strAddress.Replace("[ShippingAddress1]", objAddress.Address.Address1)
                    strAddress = strAddress.Replace("[ShippingAddress2]", objAddress.Address.Address2)
                    strAddress = strAddress.Replace("[ShippingCity]", objAddress.Address.City)
                    strAddress = strAddress.Replace("[ShippingState]", objAddress.Address.State)
                    strAddress = strAddress.Replace("[ShippingZip]", objAddress.Address.Zip)
                    strAddress = strAddress.Replace("[ShippingCountry]", objAddress.Address.Country)
                    If (Trim(objAddress.Address.Fax) <> "") Then
                        strAddress = strAddress.Replace("[ShippingFax]", objAddress.Address.Fax)
                    Else
                        strAddress = strAddress.Replace("[ShippingFax]", "None")
                    End If
                    strAddress = strAddress.Replace("[ShippingPhone]", objAddress.Address.Phone)
                    strAddress = strAddress.Replace("[ShippingFax]", objAddress.Address.Fax)
                    strAddress = strAddress.Replace("[ShippingEMail]", objAddress.Address.EMail)
                    If objAddress.CarrierCode <> "NONE" Then
                        strAddress = strAddress.Replace("[ShippingMethod]", objAddress.Address.ShipMethod)
                    Else
                        If objAddress.ShippingObject.FreeShipping = True Then
                            strAddress = strAddress.Replace("[ShippingMethod]", "Free Shipping")
                        ElseIf objAddress.ShippingObject.PremiumShipping = True Then
                            strAddress = strAddress.Replace("[ShippingMethod]", StoreFrontConfiguration.AdminShipping.Item("PremShipLabel").InnerText)
                        Else
                            strAddress = strAddress.Replace("[ShippingMethod]", "Standard")
                        End If
                    End If
                    ' #1314  SV
                    strAddress = strAddress.Replace("[ShippingInstructions]", objAddress.Address.Instructions)
                    ' #1314  SV
                    Dim objOrderItem As COrderItem
                    Dim strOrderIteration As String = ""
                    Dim i As Integer = 0 ' to count off products
                    For Each objOrderItem In objAddress.OrderItems
                        Dim strOrderItems As String = strProductsContent
                        Dim strOrderItemAttr As String = ""

                        i = i + 1
                        strOrderItems = i & ". " & strOrderItems
                        strOrderItems = strOrderItems.Replace("[ProductID]", objOrderItem.ProductCode)
                        strOrderItems = strOrderItems.Replace("[ProductName]", objOrderItem.Name)
                        strOrderItems = strOrderItems.Replace("[ProductQuantity]", objOrderItem.Quantity)

                        ' check for sale price
                        If (objOrderItem.IsOnSale) Then
                            strOrderItems = strOrderItems.Replace("[ProductPrice]", Format(objOrderItem.ItemPrice, "c"))
                            strOrderItems = strOrderItems.Replace("[ProductSalePrice]", Format(objOrderItem.SalePrice, "c"))
                        Else
                            strOrderItems = strOrderItems.Replace("[ProductPrice]", Format(objOrderItem.ItemPrice, "c"))
                            strOrderItems = strOrderItems.Replace("[ProductSalePrice]", "No Sale Price")
                        End If

                        ' Iterate through attributes if any
                        If (objOrderItem.Attributes.Count > 0) Then
                            Dim objAttr As CAttribute
                            Dim objAttrDetail As CAttributeDetail
                            For Each objAttr In objOrderItem.Attributes
                                strOrderItemAttr = strOrderItemAttr & objAttr.Name & " - "
                                For Each objAttrDetail In objAttr.AttributeDetails
                                    'update #1031 & #2614
                                    If objAttr.AttributeType = tAttributeType.Normal Then
                                        strOrderItemAttr = strOrderItemAttr & objAttrDetail.Name
                                    Else
                                        strOrderItemAttr = strOrderItemAttr & objAttrDetail.Customor_Custom_Description
                                    End If
                                    'update #1031 & #2614
                                Next
                                strOrderItemAttr = strOrderItemAttr & Chr(9)
                            Next
                        End If
                        strOrderItems = strOrderItems.Replace("[ProductAttributes]", strOrderItemAttr)
                        'Verisign Recurring Billing
                        If objOrderItem.ProductType = ProductType.Subscription OrElse _
                            objOrderItem.ProductType = ProductType.BundleSubscription OrElse _
                            objOrderItem.ProductType = ProductType.CustomizedSubscription Then
                            'strOrderItems = strOrderItems & " (Subscription: " & Format(objOrderItem.RecurringSubscriptionPrice, "c") & " " & SystemBase.GetPaymentPeriodName(objOrderItem.PaymentPeriod) & ")"
                            strOrderItems = strOrderItems.Replace("[RecurringAmount]", Format(objOrderItem.RecurringSubscriptionPrice, "c"))
                            strOrderItems = strOrderItems.Replace("[Payperiod]", SystemBase.GetPaymentPeriodName(objOrderItem.PaymentPeriod))
                            strOrderItems = strOrderItems.Replace("[Term]", objOrderItem.Term.ToString())
                        Else
                            strOrderItems = strOrderItems.Replace("[RecurringAmount]", "")
                            strOrderItems = strOrderItems.Replace("[Payperiod]", "")
                            strOrderItems = strOrderItems.Replace("[Term]", "")
                        End If
                        'Verisign Recurring Billing
                        If objShippingContent.Format.ToLower = "text" Then
                            strOrderItems = strOrderItems & vbCrLf
                        Else
                            strOrderItems = strOrderItems & "<BR>"
                        End If
                        strOrderIteration = strOrderIteration & vbCrLf & strOrderItems & vbCrLf
                        Try
                            If (IsNothing(objOrderItem.Inventory)) = False Then
                                If (objOrderItem.Inventory.HasLowStock) Then
                                    objInventoryMail.SendLowStockNotice(StoreFrontConfiguration.AdminEmailClass.EmailPrimary, arEmpty, objOrderItem)
                                End If
                            End If
                        Catch Ex As Exception

                        End Try


                    Next
                    'djp  12-17-02 All products showing for each address issue
                    strAddress = strAddress.Replace("[ShippingProducts]", strOrderIteration & vbCrLf)
                    strShippingContent = strShippingContent & strAddress 'strAddress.Replace("[ShippingProducts]", vbCrLf & strOrderIteration & vbCrLf)
                    strAddress = ""
                    'strShippingContent = strShippingContent & vbCrLf & strOrderIteration & vbCrLf & "Product(s) will be shipped to:" & vbCrLf & strAddress & vbCrLf
                    '    strShippingContent = strShippingContent & vbCrLf & strOrderIteration & vbCrLf
                Next
                ' strShippingContent = strAddress.Replace("[ShippingProducts]", strShippingContent)
            End If

            Return strShippingContent
        End Function


#End Region

#Region "Private Function GetShippingContentVendor(ByVal strProducts As String, ByVal objAddress As COrderAddress) As String"

        Private Function GetShippingContentVendor(ByVal strProducts As String, ByVal objAddress As COrderAddress) As String
            Dim strShippingContent As String = ""
            Dim objInventoryMail As New LowStockNotice()
            'objShippingContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Shipping_Info)

            Dim strAddress As String = ""

            ' Associate order items with shipping address

            strAddress = strAddress & vbCrLf & objShippingContent.Body
            strAddress = strAddress.Replace("[ShippingName]", objAddress.Address.Name)
            strAddress = strAddress.Replace("[ShippingCompany]", objAddress.Address.Company)
            strAddress = strAddress.Replace("[ShippingAddress1]", objAddress.Address.Address1)
            strAddress = strAddress.Replace("[ShippingAddress2]", objAddress.Address.Address2)
            strAddress = strAddress.Replace("[ShippingCity]", objAddress.Address.City)
            strAddress = strAddress.Replace("[ShippingState]", objAddress.Address.State)
            strAddress = strAddress.Replace("[ShippingZip]", objAddress.Address.Zip)
            strAddress = strAddress.Replace("[ShippingCountry]", objAddress.Address.Country)
            If (Trim(objAddress.Address.Fax) <> "") Then
                strAddress = strAddress.Replace("[ShippingFax]", objAddress.Address.Fax)
            Else
                strAddress = strAddress.Replace("[ShippingFax]", "None")
            End If
            strAddress = strAddress.Replace("[ShippingPhone]", objAddress.Address.Phone)
            strAddress = strAddress.Replace("[ShippingFax]", objAddress.Address.Fax)
            strAddress = strAddress.Replace("[ShippingEMail]", objAddress.Address.EMail)
            If objAddress.CarrierCode <> "NONE" Then
                strAddress = strAddress.Replace("[ShippingMethod]", objAddress.Address.ShipMethod)
            Else
                If objAddress.ShippingObject.FreeShipping = True Then
                    strAddress = strAddress.Replace("[ShippingMethod]", "Free Shipping")
                ElseIf objAddress.ShippingObject.PremiumShipping = True Then
                    strAddress = strAddress.Replace("[ShippingMethod]", StoreFrontConfiguration.AdminShipping.Item("PremShipLabel").InnerText)
                Else
                    strAddress = strAddress.Replace("[ShippingMethod]", "Standard")
                End If

            End If
            ' #1314  SV
            strAddress = strAddress.Replace("[ShippingInstructions]", objAddress.Address.Instructions)
            ' #1314  SV

            strAddress = strAddress.Replace("[ShippingProducts]", strProducts)
            Return strAddress
        End Function


#End Region

#Region "Private Function GetOrderTotalContent() As String"

        Private Function GetOrderTotalContent() As String
            Dim strOrderTotalContent As String = ""
            objOrderTotalContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Order_Total)

            If (objOrderTotalContent.IsActive = 1) Then
                strOrderTotalContent = objOrderTotalContent.Body
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderMerchandiseTotal]", Format(m_objOrder.MerchandiseTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderDiscounts]", Format(m_objOrder.DiscountTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderSubtotal]", Format(m_objOrder.SubTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderLocalTax]", Format(m_objOrder.LocalTax, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderStateProvinceTax]", Format(m_objOrder.StateTax, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderCountryTax]", Format(m_objOrder.CountryTax, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderShipping]", Format(m_objOrder.ShippingTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderHandling]", Format(m_objOrder.HandlingTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderTotal]", Format(m_objOrder.OrderTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderGiftCertificate]", Format(m_objOrder.GiftCertificateTotal, "c"))
                strOrderTotalContent = strOrderTotalContent.Replace("[OrderGrandTotal]", Format(m_objOrder.GrandTotal, "c"))
            End If
            Return strOrderTotalContent
        End Function


#End Region

#Region "Private Sub SendShopperEmail(ByVal strOrderTotalContent As String, ByVal strBillingContent As String, ByVal strShippingContent As String)"

        Private Sub SendShopperEmail(ByVal strOrderTotalContent As String, ByVal strBillingContent As String, ByVal strShippingContent As String)
            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Shopper)

            If (objContent.IsActive = 1) Then

                ' Retrieve body and subject
                Dim strShopperBody As String = objContent.Body
                Dim strShopperSubject As String = objContent.Subject

                ' Replace tags in subject line
                strShopperSubject = strShopperSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strShopperSubject = strShopperSubject.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                strShopperSubject = strShopperSubject.Replace("[CustomerFirstName]", m_objCustomer.CustFirstName)
                strShopperSubject = strShopperSubject.Replace("[CustomerLastName]", m_objCustomer.CustLastName)
                strShopperSubject = strShopperSubject.Replace("[OrderID]", m_objOrder.OrderNumber)
                strShopperSubject = strShopperSubject.Replace("[OrderTotal]", strOrderTotalContent)

                ' Replace tags in body
                strShopperBody = strShopperBody.Replace("[CustomerFirstName]", m_objCustomer.CustFirstName)
                strShopperBody = strShopperBody.Replace("[CustomerLastName]", m_objCustomer.CustLastName)
                strShopperBody = strShopperBody.Replace("[OrderTotal]", strOrderTotalContent)
                strShopperBody = strShopperBody.Replace("[OrderID]", m_objOrder.OrderNumber)
                strShopperBody = strShopperBody.Replace("[BillingInfo]", strBillingContent)
                strShopperBody = strShopperBody.Replace("[ShippingInfo]", strShippingContent)
                strShopperBody = strShopperBody.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)

                ' Handle HTML Links
                If objContent.Format = "HTML" Or objContent.Format = "Html" Then

                    ' Weird ace error fix -- it places the full path of management into
                    If strShopperBody.IndexOf(StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/[StoreURL]") > 0 Then
                        strShopperBody = strShopperBody.Replace(StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    ElseIf strShopperBody.IndexOf(StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/[StoreURL]") > 0 Then
                        strShopperBody = strShopperBody.Replace(StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    ElseIf strShopperBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strShopperBody.IndexOf("<a href=[StoreURL]") > 0 Or strShopperBody.IndexOf("<A href=[StoreURL]") > 0 Or strShopperBody.IndexOf("<A href=" & Chr(34) & "[StoreURL]") > 0 Then
                        strShopperBody = strShopperBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    ElseIf strShopperBody.IndexOf("<a href=" & Chr(34) & "http://[storeurl]") > 0 Or strShopperBody.IndexOf("<a href=" & Chr(34) & "http://[StoreURL]") > 0 Then
                        strShopperBody = strShopperBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.Split("://").GetValue(2))
                    Else
                        strShopperBody = CReplace(strShopperBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Order Details Link

                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        If strShopperBody.IndexOf("<a href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strShopperBody.ToLower.IndexOf("<a href=" & Chr(34) & "[orderdetailslink]") > 0 Or strShopperBody.ToLower.IndexOf("<a href=[orderdetailslink]") > 0 Or strShopperBody.IndexOf("<A href=[OrderDetailsLink]") > 0 Or strShopperBody.IndexOf("<A href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strShopperBody.IndexOf("<a href=" & Chr(34) & "[orderdetailslink]") > 0 Then
                            strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                        Else
                            strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID & "</a>")
                        End If
                    Else
                        If strShopperBody.ToLower.IndexOf("<a href=" & Chr(34) & "[orderdetailslink]") Or strShopperBody.IndexOf("<a href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strShopperBody.ToLower.IndexOf("<a href=[orderdetailslink]") > 0 Or strShopperBody.IndexOf("<A href=[OrderDetailsLink]") > 0 Or strShopperBody.IndexOf("<A href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strShopperBody.IndexOf("<a href=" & Chr(34) & "[orderdetailslink]") > 0 Then
                            strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                        Else
                            strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID & "</a>")
                        End If
                    End If

                    strShopperBody = "<html><body>" & strShopperBody & "</body></html>"

                Else
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                    Else
                        strShopperBody = CReplace(strShopperBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                    End If
                End If

                strShopperBody = strShopperBody.Replace("[ProductsShippingInfo]", strShippingContent)

                If (objAdmin.EmailPrimary <> "" And m_objOrder.BillAddress.EMail <> "") Then
                    MyBase.To = m_objOrder.BillAddress.EMail
                    MyBase.From = objAdmin.EmailPrimary
                    MyBase.MailMethod = objAdmin.EmailMethod
                    MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = objContent.Format
                    MyBase.Subject = strShopperSubject
                    MyBase.Body = strShopperBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            End If
        End Sub


#End Region

#Region "Private Sub SendVendorEmail(ByVal strOrderTotalContent As String, ByVal strBillingContent As String)"

        Private Sub SendVendorEmail(ByVal strOrderTotalContent As String, ByVal strBillingContent As String)

            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Vendor)
            Dim strVendorShippingContent As String = ""
            If (objContent.IsActive = 1) Then
                ' Take the first object, check its vendor id, place item's vendor id in accounted for list, check
                ' rest of order for similar vendor id. 
                Dim objSortAddress As COrderAddress
                For Each objSortAddress In m_objOrder.OrderAddresses

                    Dim i As Integer
                    Dim j As Integer
                    Dim arrVendorIDArrayList As New ArrayList '= objSortAddress.OrderItems
                    For i = 0 To objSortAddress.OrderItems.Count - 1
                        'update #2292
                        If (objSortAddress.OrderItems(i).DropShip) = True Then
                            arrVendorIDArrayList.Add(objSortAddress.OrderItems(i))
                        End If
                    Next
                    Dim objVend As COrderItem
                    Dim arRemove As New ArrayList
                    'Dim objCurrentItem As COrderItem
                    Dim nCurrVend As COrderItem
                    Dim strOrderItems As String
                    Dim strOrderItemAttr As String = ""
                    While arrVendorIDArrayList.Count > 0
                        objVend = arrVendorIDArrayList(0)

                        For i = 0 To arrVendorIDArrayList.Count - 1
                            nCurrVend = arrVendorIDArrayList(i)
                            'Group the products by vendor
                            'If objVend.VendorID = nCurrVend.VendorID Then '2215
                            If (objVend.SavedVendorName() = nCurrVend.SavedVendorName()) And (objVend.VendorEmail = nCurrVend.VendorEmail) Then


                                'Arraylist to keep track of which items are already added
                                arRemove.Add(i)
                                'Add product to email here
                                strOrderItems = strProductsContent
                                'update #2005
                                strOrderItems = strOrderItems.Replace("[ProductID]", nCurrVend.ProductCode)
                                strOrderItems = strOrderItems.Replace("[ProductName]", nCurrVend.Name)
                                strOrderItems = strOrderItems.Replace("[ProductQuantity]", nCurrVend.Quantity)

                                ' check for sale price
                                If (nCurrVend.IsOnSale) Then
                                    strOrderItems = strOrderItems.Replace("[ProductPrice]", Format(nCurrVend.SalePrice, "c"))
                                    strOrderItems = strOrderItems.Replace("[ProductSalePrice]", Format(nCurrVend.SalePrice, "c"))
                                Else
                                    strOrderItems = strOrderItems.Replace("[ProductPrice]", Format(nCurrVend.Price, "c"))
                                    strOrderItems = strOrderItems.Replace("[ProductSalePrice]", "No Sale Price")
                                End If

                                ' Iterate through attributes if any
                                If (nCurrVend.Attributes.Count > 0) Then
                                    Dim objAttr As CAttribute
                                    Dim objAttrDetail As CAttributeDetail
                                    For Each objAttr In nCurrVend.Attributes
                                        strOrderItemAttr = strOrderItemAttr & objAttr.Name & " - "
                                        For Each objAttrDetail In objAttr.AttributeDetails
                                            strOrderItemAttr = strOrderItemAttr & objAttrDetail.Name & " " & objAttrDetail.Customor_Custom_Description
                                        Next
                                        strOrderItemAttr = strOrderItemAttr & Chr(9)
                                    Next
                                End If
                                strOrderItems = strOrderItems.Replace("[ProductAttributes]", strOrderItemAttr)
                                strOrderItemAttr = ""
                                If objContent.Format.ToLower = "text" Then
                                    strVendorShippingContent = strVendorShippingContent & vbCrLf & strOrderItems & vbCrLf
                                Else
                                    strVendorShippingContent = strVendorShippingContent & "<BR>" & strOrderItems & "<BR>"
                                End If
                            End If
                        Next

                        ' start mailing
                        Dim strVendorBody As String = objContent.Body
                        Dim strVendorSubject As String = objContent.Subject

                        ' Replace tags in subject line
                        strVendorSubject = strVendorSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                        strVendorSubject = strVendorSubject.Replace("[CustomerFirstName]", m_objCustomer.CustFirstName)
                        strVendorSubject = strVendorSubject.Replace("[CustomerLastName]", m_objCustomer.CustLastName)
                        strVendorSubject = strVendorSubject.Replace("[OrderID]", m_objOrder.OrderNumber)

                        ' Replace tags in body
                        strVendorBody = CReplace(strVendorBody, "[CustomerFirstName]", m_objCustomer.CustFirstName)
                        strVendorBody = CReplace(strVendorBody, "[CustomerLastName]", m_objCustomer.CustLastName)
                        strVendorBody = CReplace(strVendorBody, "[BillingInfo]", strBillingContent)
                        strVendorBody = CReplace(strVendorBody, "[OrderID]", m_objOrder.OrderNumber)
                        strVendorBody = CReplace(strVendorBody, "[ProductsShippingInfo]", GetShippingContentVendor(strVendorShippingContent, objSortAddress))
                        strVendorBody = CReplace(strVendorBody, "[OrderTotal]", strOrderTotalContent)
                        strVendorBody = CReplace(strVendorBody, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)

                        If objContent.Format = "HTML" Or objContent.Format = "Html" Then
                            ' For Store URL -- handles if it is placed inside a link or outside.
                            If strVendorBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strVendorBody.IndexOf("<a href=[StoreURL]") > 0 Then
                                strVendorBody = CReplace(strVendorBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                            Else
                                strVendorBody = CReplace(strVendorBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                            End If

                            ' Order Details Link
                            If strVendorBody.IndexOf("<a href=" & Chr(34) & "[OrderDetailsLink]") Or strVendorBody.IndexOf("<a href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strVendorBody.IndexOf("<a href=[OrderDetailsLink]") > 0 Or strVendorBody.IndexOf("<A href=[OrderDetailsLink]") > 0 Or strVendorBody.IndexOf("<A href=" & Chr(34) & "[OrderDetailsLink]") > 0 Or strVendorBody.IndexOf("<a href=" & Chr(34) & "[orderdetailslink]") > 0 Then
                                If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                    strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                                Else
                                    strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                                End If

                            Else ' not within hyperlink
                                If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                    strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID & "</a>")
                                Else
                                    strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID & "</a>")
                                End If
                            End If

                            strVendorBody = "<html><body>" & strVendorBody & "</body></html>"
                        Else
                            strVendorBody = strVendorBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                            If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                            Else
                                strVendorBody = CReplace(strVendorBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/OrderDetail.aspx?OrderID=" & m_objOrder.UID)
                            End If
                        End If

                        If (objVend.VendorEmail <> "" And objAdmin.EmailPrimary <> "") Then
                            MyBase.To = objVend.VendorEmail
                            MyBase.From = objAdmin.EmailPrimary
                            MyBase.MailMethod = objAdmin.EmailMethod
                            MyBase.MailServer = objAdmin.EmailServer
                            MyBase.MailFormat = objContent.Format
                            MyBase.Subject = strVendorSubject
                            MyBase.Body = strVendorBody
                            Try
                                MyBase.SendEMail()
                            Catch objErr As Exception
                                Throw New Exception(objErr.Message)
                            End Try
                        End If


                        strOrderItems = ""
                        strVendorShippingContent = ""
                        strVendorBody = ""
                        arRemove.Sort()
                        arRemove.Reverse()
                        For Each j In arRemove
                            'Remove all products associated with vendor from arraylist
                            arrVendorIDArrayList.RemoveAt(j)
                        Next
                        arRemove.Clear()
                    End While


                Next
            End If
        End Sub


#End Region

#Region "Private Sub SendMerchantEmail(ByVal strBillingContent As String, ByVal strShippingContent As String)"

        Private Sub SendMerchantEmail(ByVal strBillingContent As String, ByVal strShippingContent As String)
            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Confirm_EMail_Merchant)
            If (objContent.Type = XMLEMailContentTypes.Confirm_EMail_Merchant And objContent.IsActive = 1) Then

                ' Retrieve body and subject
                Dim strMerchantBody As String = objContent.Body
                Dim strMerchantSubject As String = objContent.Subject

                ' Replace tags in subject line
                strMerchantSubject = strMerchantSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strMerchantSubject = strMerchantSubject.Replace("[CustomerFirstName]", m_objCustomer.CustFirstName)
                strMerchantSubject = strMerchantSubject.Replace("[CustomerLastName]", m_objCustomer.CustLastName)
                strMerchantSubject = strMerchantSubject.Replace("[BillingInfo]", strBillingContent)
                strMerchantSubject = strMerchantSubject.Replace("[ProductsShippingInfo]", strShippingContent)
                strMerchantSubject = strMerchantSubject.Replace("[OrderID]", m_objOrder.OrderNumber)
                strMerchantSubject = strMerchantSubject.Replace("[OrderTotal]", GetOrderTotalContent())
                strMerchantSubject = strMerchantSubject.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)

                ' Replace tags in body
                strMerchantBody = strMerchantBody.Replace("[CustomerFirstName]", m_objCustomer.CustFirstName)
                strMerchantBody = strMerchantBody.Replace("[CustomerLastName]", m_objCustomer.CustLastName)
                strMerchantBody = strMerchantBody.Replace("[BillingInfo]", strBillingContent)
                strMerchantBody = strMerchantBody.Replace("[ProductsShippingInfo]", strShippingContent)
                strMerchantBody = strMerchantBody.Replace("[OrderID]", m_objOrder.OrderNumber)
                strMerchantBody = strMerchantBody.Replace("[OrderTotal]", GetOrderTotalContent())
                strMerchantBody = strMerchantBody.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strMerchantBody = strMerchantBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)

                If objContent.Format = "HTML" Or objContent.Format = "Html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If strMerchantBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strMerchantBody.IndexOf("<a href=[StoreURL]") > 0 Then
                        strMerchantBody = CReplace(strMerchantBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        strMerchantBody = CReplace(strMerchantBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Order Details Link
                    If strMerchantBody.ToLower.IndexOf("<a href=[orderdetailslink]".ToLower) > 0 Or strMerchantBody.ToLower.IndexOf("<a href=".ToLower & Chr(34) & "[orderdetailslink]".ToLower) > 0 Then
                        If (StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText.EndsWith("/")) Then
                            strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber)
                        Else
                            strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber)
                        End If
                    Else ' not within hyperlink
                        If (StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText.EndsWith("/")) Then
                            strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber & "</a>")
                        Else
                            strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber & "</a>")
                        End If
                    End If
                    strMerchantBody = "<html><body>" & strMerchantBody & "</body></html>"
                Else
                    strMerchantBody = strMerchantBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber)
                    Else
                        strMerchantBody = CReplace(strMerchantBody, "[OrderDetailsLink]", StoreFrontConfiguration.AdminStore.Item("SSLPath").InnerText & "/Management/orddetails.aspx?OrderID=" & m_objOrder.OrderNumber)
                    End If
                End If

                If (objAdmin.EmailPrimary <> "" And objAdmin.EmailPrimary <> "") Then
                    MyBase.To = objAdmin.EmailPrimary
                    MyBase.From = objAdmin.EmailPrimary
                    If (objAdmin.EmailSecondary <> "") Then
                        MyBase.CC = objAdmin.EmailSecondary
                    End If
                    MyBase.MailMethod = objAdmin.EmailMethod
                    MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = objContent.Format
                    MyBase.Subject = strMerchantSubject
                    MyBase.Body = strMerchantBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            End If
        End Sub


#End Region

    End Class
#End Region

#Region "Class CPromoMail"
    Public Class CPromoMail
        Inherits Email.CEmail

#Region "Public Sub SendPromoMail(ByVal txtSubject As TextBox, ByVal txtBody As String, ByVal strFormat As String, ByRef arrCustomersList As ArrayList)"
        'This was an overload of the function SendPromoMail--The old function was left intact to maintain backwards compatibility
        Public Sub SendPromoMail(ByVal txtSubject As TextBox, ByVal txtBody As String, ByVal strFormat As String, ByRef arrCustomersList As ArrayList)
            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim strSubject As String
            Dim strBody As String
            Dim objContainer As PromoMailAddrProdID
            Dim strProdList As String
            'Dim lngProdId As Long
            Dim i As Integer
            For Each objContainer In arrCustomersList
                strProdList = ""
                'Dim strTemp As String
                If (IsNothing(objContainer.Products) = False) Then
                    If objContainer.Products.Count > 0 Then
                        For i = 0 To objContainer.Products.Count - 1
                            If strProdList = "" Then
                                strProdList = objContainer.Products(i)
                            ElseIf i = objContainer.Products.Count - 1 Then
                                If objContainer.Products.Count = 2 Then
                                    strProdList = strProdList & " and " & objContainer.Products(i)
                                Else
                                    strProdList = strProdList & ", and " & objContainer.Products(i)
                                End If
                            Else
                                strProdList = strProdList & ", " & objContainer.Products(i)
                            End If
                        Next
                    End If
                End If
                strSubject = txtSubject.Text.ToString
                strBody = txtBody
                strSubject = CReplace(strSubject, "[CustomerFirstName]", objContainer.CustFirstName)
                strSubject = CReplace(strSubject, "[CustomerLastName]", objContainer.CustLastName)
                strSubject = CReplace(strSubject, "[OrderedProducts]", strProdList)
                strSubject = CReplace(strSubject, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strBody = CReplace(strBody, "[CustomerFirstName]", objContainer.CustFirstName)
                strBody = CReplace(strBody, "[CustomerEMail]", objContainer.CustEmail)
                strBody = CReplace(strBody, "[CustomerLastName]", objContainer.CustLastName)
                strBody = CReplace(strBody, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strBody = CReplace(strBody, "[OrderedProducts]", strProdList)

                If strFormat = "HTML" Or strFormat = "Html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If strBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strBody.IndexOf("<a href=[StoreURL]") > 0 Then
                        strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        strBody = CReplace(strBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Unsubscribe Link
                    If strBody.IndexOf("<a href=" & Chr(34) & "[UnsubscribeURL]") > 0 Or strBody.IndexOf("<a href=[UnsubscribeURL]") > 0 Then
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                        Else
                            strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                        End If
                    Else ' not within hyperlink
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[UnsubscribeURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & "</a>")
                        Else
                            strBody = CReplace(strBody, "[UnsubscribeURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & "</a>")
                        End If
                    End If

                    strBody = "<html><body>" & strBody & "</body></html>"
                Else
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.IndexOf("/") < 0) Then
                        strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                    Else
                        strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                    End If
                    strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                End If

                If (objContainer.CustEmail <> "" And objAdmin.EmailPrimary <> "") Then
                    MyBase.To = objContainer.CustEmail
                    MyBase.From = objAdmin.EmailPrimary
                    MyBase.MailMethod = objAdmin.EmailMethod
                    Dim myAdmin As New CAdminGeneralManagement
                    MyBase.MailServer = myAdmin.PromoMailServer
                    'MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = strFormat
                    MyBase.Subject = strSubject
                    MyBase.Body = strBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            Next
        End Sub
#End Region

#Region "Public Sub SendPromoMail(ByVal txtSubject As TextBox, ByVal txtBody As String, ByVal strFormat As String, ByRef arrCustomersList As ArrayList, ByRef arProdList As ArrayList)"
        Public Sub SendPromoMail(ByVal txtSubject As TextBox, ByVal txtBody As String, ByVal strFormat As String, ByRef arrCustomersList As ArrayList, ByRef arProdList As ArrayList)

            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim strSubject As String
            Dim strBody As String
            Dim objContainer As PromoMailAddrProdID
            Dim strProdList As String = ""
            'Dim lngProdId As Long
            Dim i As Integer
            Dim count As Integer = arProdList.Count
            If count > 1 Then
                For i = 0 To count - 2
                    strProdList = arProdList(i) & ", " & strProdList
                Next
                strProdList = strProdList & " " & arProdList.Item(count - 1)
            ElseIf count = 1 Then
                strProdList = arProdList(0)
            Else
                ' no products
                strProdList = ""
            End If

            For Each objContainer In arrCustomersList
                strSubject = txtSubject.Text.ToString
                strBody = txtBody
                strSubject = CReplace(strSubject, "[CustomerFirstName]", objContainer.CustFirstName)
                strSubject = CReplace(strSubject, "[CustomerLastName]", objContainer.CustLastName)
                strSubject = CReplace(strSubject, "[OrderedProducts]", strProdList)
                strSubject = CReplace(strSubject, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strBody = CReplace(strBody, "[CustomerFirstName]", objContainer.CustFirstName)
                strBody = CReplace(strBody, "[CustomerEMail]", objContainer.CustEmail)
                strBody = CReplace(strBody, "[CustomerLastName]", objContainer.CustLastName)
                strBody = CReplace(strBody, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strBody = CReplace(strBody, "[OrderedProducts]", strProdList)

                If strFormat = "HTML" Or strFormat = "Html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If strBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strBody.IndexOf("<a href=[StoreURL]") > 0 Then
                        strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        strBody = CReplace(strBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Unsubscribe Link
                    If strBody.IndexOf("<a href=" & Chr(34) & "[UnsubscribeURL]") > 0 Or strBody.IndexOf("<a href=[UnsubscribeURL]") > 0 Then
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                        Else
                            strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                        End If
                    Else ' not within hyperlink
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[UnsubscribeURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & "</a>")
                        Else
                            strBody = CReplace(strBody, "[UnsubscribeURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail & "</a>")
                        End If
                    End If

                    strBody = "<html><body>" & strBody & "</body></html>"
                Else
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.IndexOf("/") < 0) Then
                        strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                    Else
                        strBody = CReplace(strBody, "[UnsubscribeURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "CustChangeEmail.aspx?EMail=" & objContainer.CustEmail)
                    End If
                    strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                End If

                If (objContainer.CustEmail <> "" And objAdmin.EmailPrimary <> "") Then
                    MyBase.To = objContainer.CustEmail
                    MyBase.From = objAdmin.EmailPrimary
                    MyBase.MailMethod = objAdmin.EmailMethod
                    '#2854
                    'MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailServer = (New CAdminGeneralManagement).PromoMailServer
                    MyBase.MailFormat = strFormat
                    MyBase.Subject = strSubject
                    MyBase.Body = strBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            Next
        End Sub
#End Region

    End Class
#End Region

#Region "Class CCustForgotPasswordEMail"
    Public Class CCustForgotPasswordEMail
        Inherits Email.CEmail

        Public Sub SendForgotPassword(ByRef m_arEMailContent As ArrayList, ByRef m_objCustomer As CCustomer, ByVal txtEMail As TextBox)
            Dim strPassword As String = m_objCustomer.Password
            Dim strRecipientFirstName As String = m_objCustomer.GetCustomerFirstName(txtEMail.Text)
            Dim strRecipientLastName As String = m_objCustomer.GetCustomerLastName(txtEMail.Text)
            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim objEmail As New CEMailContent()
            Dim objContent As CXMLEMailContent

            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Forgot_EMail)
            If (objContent.IsActive = 1) Then

                ' Retrieve body and subject
                Dim txtBody As String = objContent.Body
                Dim txtSubject As String = objContent.Subject

                ' Replace tags in subject line
                txtSubject = CReplace(txtSubject, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                txtSubject = CReplace(txtSubject, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                txtSubject = CReplace(txtSubject, "[RecipientFirstName]", strRecipientFirstName)
                txtSubject = CReplace(txtSubject, "[RecipientLastName]", strRecipientLastName)

                ' Replace tags in body
                txtBody = CReplace(txtBody, "[Password]", strPassword)
                txtBody = CReplace(txtBody, "[RecipientFirstName]", strRecipientFirstName)
                txtBody = CReplace(txtBody, "[RecipientLastName]", strRecipientLastName)
                txtBody = CReplace(txtBody, "[RecipientEmailAddress]", txtEMail.Text)
                txtBody = CReplace(txtBody, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)

                If objContent.Format = "HTML" Or objContent.Format = "Html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If txtBody.IndexOf("<A href=" & Chr(34) & "[StoreURL]") > 0 Or txtBody.IndexOf("<a href=[StoreURL]") > 0 Or txtBody.IndexOf("<A href=[StoreURL]") > 0 Then
                        txtBody = CReplace(txtBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        txtBody = CReplace(txtBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    txtBody = "<html><body>" & txtBody & "</body></html>"
                Else
                    txtBody = CReplace(txtBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                End If

                If (txtEMail.Text <> "" And objAdmin.EmailPrimary <> "") Then
                    MyBase.To = txtEMail.Text
                    MyBase.From = objAdmin.EmailPrimary
                    MyBase.MailMethod = objAdmin.EmailMethod
                    MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = objContent.Format
                    MyBase.Subject = txtSubject
                    MyBase.Body = txtBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            End If

        End Sub
    End Class
#End Region

#Region "Class CEmailAFriend"
    Public Class CEmailAFriend
        Inherits Email.CEmail

        'Public Sub SendEmailAFriend(ByRef m_objAccess As CXMLProductAccess, ByVal m_txtUID As TextBox, ByVal m_txtRecipientName As TextBox, ByVal m_txtRecipientEmail As TextBox, ByVal m_txtSenderName As TextBox, ByVal m_txtSenderEmail As TextBox, ByVal m_objMessage As TextBox, ByVal m_arEMailContent As ArrayList, ByVal m_chkSendToSelf As CheckBox)
        Public Sub SendEmailAFriend(ByRef m_objProduct As CProduct, ByVal m_txtRecipientName As TextBox, ByVal m_txtRecipientEmail As TextBox, ByVal m_txtSenderName As TextBox, ByVal m_txtSenderEmail As TextBox, ByVal m_objMessage As TextBox, ByVal m_arEMailContent As ArrayList, ByVal m_chkSendToSelf As CheckBox)
            'Dim objProduct As New CProduct(m_objAccess.GetProduct(m_txtUID.Text))
            Dim objContent As CXMLEMailContent
            Dim objEmail As New CEMailContent()
            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.EMail_A_Friend)
            If (objContent.IsActive = 1) Then

                ' Retrieve email configuration 
                Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
                Dim strBody As String = objContent.Body
                Dim strSubject As String = objContent.Subject

                ' Retrieve info to replace tags with
                Dim strProductLink As String = m_objProduct.DetailLink
                Dim strProductName As String = m_objProduct.Name
                Dim strProductDesc As String = m_objProduct.ShortDescription
                Dim strProductSmallImg As String = m_objProduct.SmallImage
                Dim strRecipientName As String = m_txtRecipientName.Text
                Dim strRecipientEmail As String = m_txtRecipientEmail.Text
                Dim strSenderName As String = m_txtSenderName.Text
                Dim strSenderEmail As String = m_txtSenderEmail.Text

                ' Replace tags in subject line
                strSubject = strSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                strSubject = strSubject.Replace("[SenderName]", strSenderName)
                strSubject = strSubject.Replace("[RecipientName]", strRecipientName)
                strSubject = strSubject.Replace("[ProductName]", strProductName)

                ' get rid of relative links  
                strProductLink = strProductLink.Replace("../", "")


                ' Replace tags in body

                strBody = CReplace(strBody, "[ProductName]", strProductName)
                strBody = CReplace(strBody, "[ProductDescription]", strProductDesc)
                strBody = CReplace(strBody, "[RecipientName]", strRecipientName)
                strBody = CReplace(strBody, "[SenderName]", strSenderName)
                strBody = CReplace(strBody, "[PersonalMessage]", m_objMessage.Text)
                strBody = CReplace(strBody, "[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)

                ' Handle HTML Links
                If objContent.Format = "HTML" Or objContent.Format = "Html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If strBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or strBody.IndexOf("<a href=[StoreURL]") > 0 Or strBody.IndexOf("<A href=[StoreURL]") > 0 Or strBody.IndexOf("<A href=" & Chr(34) & "[StoreURL]") > 0 Then
                        strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        strBody = CReplace(strBody, "[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Unsubscribe Link
                    If strBody.ToLower.IndexOf("<a href=" & Chr(34) & "[productlink]" & Chr(34)) > 0 Or strBody.IndexOf("<a href=[productlink]") > 0 Then
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink)
                        Else
                            strBody = CReplace(strBody, "[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductLink)
                        End If
                    Else ' not within hyperlink
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            strBody = CReplace(strBody, "[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink & "</a>")
                        Else
                            strBody = CReplace(strBody, "[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductLink & "</a>")
                        End If
                    End If


                    ' image
                    If strProductSmallImg.ToLower = "images/clear.gif" Then
                        strBody = CReplace(strBody, "[ProductImage]", "")
                        'Tee 2/11/2008 bug 1115 fix
                    ElseIf strProductSmallImg.ToLower.StartsWith("http://") OrElse strProductSmallImg.ToLower.StartsWith("https://") Then
                        strBody = CReplace(strBody, "[ProductImage]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink & Chr(34) & "><img src=" & Chr(34) & strProductSmallImg & Chr(34) & "></a>")
                        'end Tee
                    ElseIf (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        strBody = CReplace(strBody, "[ProductImage]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink & Chr(34) & "><img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductSmallImg & Chr(34) & "></a>")
                    Else
                        strBody = CReplace(strBody, "[ProductImage]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink & Chr(34) & "><img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductSmallImg & Chr(34) & "></a>")
                    End If

                    strBody = "<html><body>" & strBody & "</body></html>"
                Else
                    strBody = CReplace(strBody, "[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        strBody = CReplace(strBody, "[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductLink)
                        'Tee 2/11/2008 bug 1115 fix
                        If strProductSmallImg.ToLower.StartsWith("http://") OrElse strProductSmallImg.ToLower.StartsWith("https://") Then
                            strBody = CReplace(strBody, "[ProductImage]", strProductSmallImg)
                        Else
                            strBody = CReplace(strBody, "[ProductImage]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strProductSmallImg)
                        End If
                        'end Tee
                    Else
                        strBody = CReplace(strBody, "[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductLink)
                        'strBody = CReplace(strBody, "[ProductImage]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strProductSmallImg)
                        strBody = CReplace(strBody, "[ProductImage]", "")
                    End If
                End If

                ' Prepare send
                If (strRecipientEmail <> "" And strSenderEmail <> "") Then
                    MyBase.To = strRecipientEmail
                    MyBase.From = strSenderEmail
                    MyBase.MailMethod = objAdmin.EmailMethod
                    MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = objContent.Format
                    MyBase.Subject = strSubject
                    MyBase.Body = strBody

                    ' Send 
                    Try
                        MyBase.SendEmail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try

                    ' See if sender wants an email sent as well
                    If (m_chkSendToSelf.Checked) Then
                        MyBase.To = strSenderEmail
                        MyBase.From = strSenderEmail
                        MyBase.MailMethod = objAdmin.EmailMethod
                        MyBase.MailServer = objAdmin.EmailServer
                        MyBase.MailFormat = objContent.Format
                        MyBase.Subject = strSubject
                        MyBase.Body = strBody
                        Try
                            MyBase.SendEmail()
                        Catch objErr As Exception
                            Throw New Exception(objErr.Message)
                        End Try
                    End If
                End If

            End If

        End Sub

    End Class
#End Region

#Region "Public Class CEMailWishList"
    Public Class CEMailWishList
        Inherits Email.CEmail

        Public Sub SendEMailWishList(ByRef m_objCustomer As CCustomer, ByRef m_txtRecipientEmail As TextBox, ByRef m_txtRecipientName As TextBox, ByRef m_txtSenderEmail As TextBox, ByRef m_txtSenderName As TextBox, ByRef m_txtMessage As TextBox, ByRef m_arEMailContent As ArrayList)
            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim objContent As CXMLEMailContent
            Dim objWishListContent As CXMLEMailContent
            Dim objCart As CGenericCart = m_objCustomer.SavedCart
            Dim arrCartItems As ArrayList = objCart.CartItems()
            Dim strDetailLink As String = ""
            Dim objEmail As New CEMailContent()

            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.EMail_Wish_List)

            If (objContent.IsActive = 1) Then

                ' Retrieve body and subject
                Dim txtBody As String = objContent.Body
                Dim txtSubject As String = objContent.Subject

                ' Replace tags in subject line
                txtSubject = txtSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                txtSubject = txtSubject.Replace("[RecipientName]", m_txtRecipientName.Text)
                txtSubject = txtSubject.Replace("[SenderName]", m_txtSenderName.Text)

                ' Replace tags in body
                txtBody = txtBody.Replace("[SenderName]", m_txtSenderName.Text)
                txtBody = txtBody.Replace("[RecipientName]", m_txtRecipientName.Text)
                txtBody = txtBody.Replace("[RecipientEmailAddress]", m_txtRecipientEmail.Text)
                txtBody = txtBody.Replace("[PersonalMessage]", m_txtMessage.Text)
                txtBody = txtBody.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                If objContent.Format = "HTML" Or objContent.Format = "Html" Then
                    If txtBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or txtBody.IndexOf("<a href=[StoreURL]") > 0 Then
                        txtBody = txtBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        txtBody = txtBody.Replace("[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If
                Else
                    txtBody = txtBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                End If

                ' Get WishList format
                objWishListContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Wish_List_Components)

                If (objWishListContent.IsActive = 1) Then

                    Dim txtList As String = objWishListContent.Body
                    Dim txtWishList As String = ""

                    ' Format WishList
                    Dim objItem As CGenericCartItem
                    If (arrCartItems.Count > 0) Then
                        For Each objItem In arrCartItems
                            Dim objList As String = txtList
                            Dim txtAttr As String = ""
                            If (objItem.Attributes.Count > 0) Then
                                Dim objAttr As CAttribute
                                Dim objAttrDetail As CAttributeDetail
                                For Each objAttr In objItem.Attributes
                                    txtAttr = txtAttr & objAttr.Name & "-"
                                    For Each objAttrDetail In objAttr.AttributeDetails
                                        txtAttr = txtAttr & objAttrDetail.Name_Price_Info
                                    Next
                                    txtAttr = txtAttr & Chr(9)
                                Next
                            End If

                            ' replace tags in wishlist format    
                            objList = objList.Replace("[ProductID]", objItem.ProductID)
                            objList = objList.Replace("[ProductName]", objItem.Name)
                            objList = objList.Replace("[ProductDescription]", objItem.ShortDescription)
                            objList = objList.Replace("[ProductAttributes]", txtAttr)
                            objList = objList.Replace("[Price]", Format(objItem.Price, "c"))
                            If (objItem.Price.ToString <> "0") Then
                                objList = objList.Replace("[SalePrice]", Format(objItem.SalePrice, "c"))
                            Else
                                objList = objList.Replace("[SalePrice]", "No Sale Price")
                            End If

                            ' Handle HTML Links
                            If objContent.Format.ToLower = "html" Then
                                ' For Store URL -- handles if it is placed inside a link or outside.
                                If objList.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or objList.IndexOf("<a href=[StoreURL]") > 0 Then
                                    objList = objList.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                                Else
                                    objList = objList.Replace("[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                                End If
                                strDetailLink = objItem.DetailLink.Replace("../", "")
                                ' Unsubscribe Link
                                If objList.IndexOf("<a href=" & Chr(34) & "[ProductLink]") > 0 Or objList.IndexOf("<a href=[ProductLink]") > 0 Then
                                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                        objList = objList.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strDetailLink)
                                    Else
                                        objList = objList.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strDetailLink)
                                    End If
                                Else ' not within hyperlink
                                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                        objList = objList.Replace("[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strDetailLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strDetailLink & "</a>")
                                    Else
                                        objList = objList.Replace("[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strDetailLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strDetailLink & "</a>")
                                    End If
                                End If

                                ' image
                                If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                    objList = objList.Replace("[ProductImage]", "<img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & objItem.SmallImage & Chr(34) & ">")
                                Else
                                    objList = objList.Replace("[ProductImage]", "<img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & objItem.SmallImage & Chr(34) & ">")
                                End If
                            Else
                                objList = objList.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                                If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                                    objList = objList.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & strDetailLink)
                                    objList = objList.Replace("[ProductImage]", "")
                                Else
                                    objList = objList.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & strDetailLink)
                                    objList = objList.Replace("[ProductImage]", "")
                                End If
                            End If

                            txtWishList = txtWishList & objList & Chr(13)
                        Next
                        txtBody = txtBody.Replace("[WishList]", txtWishList)
                        txtBody = "<html><body>" & txtBody & "</body></html>"

                        If (m_txtRecipientEmail.Text <> "" And objAdmin.EmailPrimary <> "") Then
                            MyBase.To = m_txtRecipientEmail.Text
                            MyBase.From = m_txtSenderEmail.Text
                            MyBase.MailMethod = objAdmin.EmailMethod
                            MyBase.MailServer = objAdmin.EmailServer
                            MyBase.MailFormat = objContent.Format
                            MyBase.Subject = txtSubject
                            MyBase.Body = txtBody
                            Try
                                MyBase.SendEMail()
                            Catch objErr As Exception
                                Throw New Exception(objErr.Message)
                                Exit Sub
                            End Try
                        End If
                    End If
                End If
            End If


        End Sub
    End Class
#End Region

#Region "Class LowStockNotice"
    Public Class LowStockNotice
        Inherits Email.CEmail

        Public Sub SendLowStockNotice(ByVal strEMail As String, ByRef m_arEMailContent As ArrayList, ByRef objProduct As COrderItem)

            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim objContent As CXMLEMailContent
            Dim objEmail As New CEMailContent()

            objContent = objEmail.GetCXMLEMailContent(EMailContentTypes.Low_Stock_Notice)
            MyBase.CC = objAdmin.EmailSecondary

            If (objContent.Type = XMLEMailContentTypes.Low_Stock_Notice And objContent.IsActive = 1) Then
                ' Retrieve body and subject
                Dim txtBody As String = objContent.Body
                Dim txtSubject As String = objContent.Subject
                Dim txtDetailLink As String = objProduct.DetailLink
                ' Replace tags in subject line
                txtSubject = txtSubject.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                txtSubject = txtSubject.Replace("[ProductName]", objProduct.Name)
                txtSubject = txtSubject.Replace("[ProductInventoryCount]", objProduct.Inventory.InventoryCount(objProduct.Attributes))

                ' Replace tags in body
                txtBody = txtBody.Replace("[ProductName]", objProduct.Name)
                txtBody = txtBody.Replace("[ProductInventoryCount]", objProduct.Inventory.InventoryCount(objProduct.Attributes))
                txtBody = txtBody.Replace("[ProductID]", objProduct.ProductID)
                txtBody = txtBody.Replace("[ProductCode]", objProduct.ProductCode)
                txtBody = txtBody.Replace("[ManufacturerName]", objProduct.Manufacturer)
                txtBody = txtBody.Replace("[VendorName]", objProduct.Vendor)
                txtBody = txtBody.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                'update #2348
                If IsNothing(txtDetailLink) Then
                    Dim dsprod As New CProductManagement(objProduct.ProductID)
                    txtDetailLink = dsprod.DetailLink
                    dsprod = Nothing
                End If
                txtDetailLink = txtDetailLink.Replace("../", "")
                ' Handle HTML Links
                If objContent.Format.ToLower = "html" Then
                    ' For Store URL -- handles if it is placed inside a link or outside.
                    If txtBody.IndexOf("<a href=" & Chr(34) & "[StoreURL]") > 0 Or txtBody.IndexOf("<a href=[StoreURL]") > 0 Then
                        txtBody = txtBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    Else
                        txtBody = txtBody.Replace("[StoreURL]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("Name").InnerText & "</a>")
                    End If

                    ' Unsubscribe Link
                    If txtBody.IndexOf("<a href=" & Chr(34) & "[ProductLink]") > 0 Or txtBody.IndexOf("<a href=[ProductLink]") > 0 Then
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            txtBody = txtBody.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & txtDetailLink)
                        Else
                            txtBody = txtBody.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & txtDetailLink)
                        End If
                    Else ' not within hyperlink
                        If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                            txtBody = txtBody.Replace("[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & txtDetailLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & txtDetailLink & "</a>")
                        Else
                            txtBody = txtBody.Replace("[ProductLink]", "<a href=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & txtDetailLink & Chr(34) & ">" & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & txtDetailLink & "</a>")
                        End If
                    End If

                    ' image
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        txtBody = txtBody.Replace("[ProductImage]", "<img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & objProduct.SmallImage & Chr(34) & ">")
                    Else
                        txtBody = txtBody.Replace("[ProductImage]", "<img src=" & Chr(34) & StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & objProduct.SmallImage & Chr(34) & ">")
                    End If
                    txtBody = "<html><body>" & txtBody & "</body></html>"
                Else
                    txtBody = txtBody.Replace("[StoreURL]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText)
                    If (StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.EndsWith("/")) Then
                        txtBody = txtBody.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & txtDetailLink)
                        txtBody = txtBody.Replace("[ProductImage]", "")
                    Else
                        txtBody = txtBody.Replace("[ProductLink]", StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText & "/" & txtDetailLink)
                        txtBody = txtBody.Replace("[ProductImage]", "")
                    End If
                End If

                If (strEMail <> "" And objAdmin.EmailPrimary <> "") Then
                    MyBase.To = strEMail
                    MyBase.From = objAdmin.EmailPrimary
                    MyBase.MailMethod = objAdmin.EmailMethod
                    MyBase.MailServer = objAdmin.EmailServer
                    MyBase.MailFormat = objContent.Format
                    MyBase.Subject = txtSubject
                    MyBase.Body = txtBody
                    Try
                        MyBase.SendEMail()
                    Catch objErr As Exception
                        Throw New Exception(objErr.Message)
                    End Try
                End If
            End If
        End Sub
    End Class
#End Region

End Namespace


