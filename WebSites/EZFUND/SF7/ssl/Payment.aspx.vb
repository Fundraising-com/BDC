'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Processors
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase.AppException
Imports StoreFront.Integration
Imports System.Xml
Imports System.Collections.Specialized

Partial Class CreditCard
    Inherits CWebPage
    Protected WithEvents AddressLabel1 As AddressLabel
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents GiftCertificates1 As GiftCertificates
    'sp7

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private WithEvents objProcessor As CProcessor
    Private strMethod As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Tee 4/25/2007 check ensure customer still in valid session
        If (m_objCustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            Response.Redirect("CustSignInCheckout.aspx" & IIf(Request.QueryString("Affiliate") = "", "", "&Affiliate=" & Request.QueryString("Affiliate")))
        End If
        'end Tee
        Try
            pnlRegular.Visible = True
            pnlPayerAuth.Visible = False

            If Not Request.QueryString("PayFlowLink") Is Nothing Then
                HandlePayFlowProLink(Request.Form)
            End If
            Dim objOrder As COrder = Session("Order")
            SetPageTitle = m_objMessages.GetXMLMessage("Payment.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            AddressLabel1.AddressSource = objOrder.BillAddress


            objProcessor = New CProcessor(objOrder)
            objOrder.CustomerID = m_objcustomer.GetCustomerID()


            CType(GiftCertificates1.FindControl("btnApply"), LinkButton).Attributes.Add("onclick", "return SetValidationGiftCert();")


            TotalDisplay1.OandaISO = Session("ConvertISO")
            TotalDisplay1.OandaRate = CDec(Session("OandaRate"))

            Dim objMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
            strMethod = objMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod)

            SetDisplayObjects(objOrder)

            TotalDisplay1.DataSource = objOrder
            TotalDisplay1.DataBind()
            imgEdit.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Edit").Attributes("Filepath").Value
            imgCompleteOrder.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CompleteOrder").Attributes("Filepath").Value
            'sp7
            imgPayPalOrder.ImageUrl = imgCompleteOrder.ImageUrl

            'check for post backs from payer auth results.
            ' check for payer authentication and set the values
            If Not Page.Request.Form.Item("CcpaClientId") Is Nothing Then
                ' return from clear commerce payer authentication
                Me.HandlePayerAuthenticationResponse(objProcessor.Name, Request.Form)
            End If

            If Not Request.Form("hdPayerAuthId") Is Nothing AndAlso Request.Form("hdPayerAuthId") = MyBase.m_objCustomer.GetSessionID Then
                Dim myPayerAuthFields As New NameValueCollection
                'return from cardinal payer auth
                myPayerAuthFields.Add("MD", Request.Form("hdMD"))
                myPayerAuthFields.Add("PaRes", Request.Form("hdPaRes"))
                'Cardinal hot fix
                '   Me.HandlePayerAuthenticationResponse(objProcessor.Name, myPayerAuthFields)
                If Request.Form("hdPaRes") <> "" Then
                    Me.HandlePayerAuthenticationResponse(objProcessor.Name, myPayerAuthFields)
                Else
                    ErrorMessage.Visible = True
                    ErrorMessage.Text = "Your Credit Card transaction was not able to be authenticated. Please provide another form of payment."
                    Exit Sub
                End If
                'Cardinal hot fix ends
            End If
        Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
            Session("DetailError") = "Class Payment Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Function GetBackOrderQuantities() As ArrayList
        Dim bkoQtys As New ArrayList
        Dim cartItems As ArrayList = m_objXMLCart.GetCartItems()
        For Each cartItem As CCartItem In cartItems
            bkoQtys.Add(cartItem.BackOrderQuantity)
        Next
        Return bkoQtys
    End Function

    Private Function CheckQuantities(ByVal backOrderQuantities As ArrayList) As Boolean

        Dim redirect As Boolean = False
        Dim removeList As New ArrayList
        Dim cartItems As ArrayList = m_objXMLCart.GetCartItems()
        Dim i As Integer = 0
        For Each cartItem As CCartItem In cartItems
            If cartitem.Inventory.InventoryTracked Then
                Dim quantity As Long = cartItem.Inventory.GetQuantity(cartitem.Attributes)
                If quantity <= 0 AndAlso Not cartItem.Inventory.CanBackOrder Then
                    removeList.Add(cartItem)
                    redirect = True
                Else
                    If cartItem.Quantity > quantity Then
                        If Not cartItem.Inventory.CanBackOrder Then
                            'not enough can not BKO
                            redirect = True
                            cartItem.Quantity = quantity
                        Else
                            If cartItem.BackOrderQuantity <> backOrderQuantities(i) Then
                                'BKO amount has changed
                                redirect = True
                            End If
                        End If
                    End If
                End If
            End If
            i += 1
        Next

        If redirect Then
            For Each cartItem As CCartItem In removeList
                m_objXMLCart.DeleteItem(cartitem, cartitem.Quantity)
            Next
            Session("OrderSubmitErrorMessage") = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "OrderError", "OrderError")
            m_objxmlcart.SaveToDB()
            Dim config As New StoreFrontConfiguration
            Response.Redirect(StoreFrontConfiguration.SiteURL & "ShoppingCart.aspx?WebID=" & m_objCustomer.GetSessionID)
            Return False
        End If
        Session("OrderSubmitErrorMessage") = Nothing
        Return True

    End Function

    Private Function UpdateInventoryLevels() As Boolean
        Try
            Dim invChecker As New InventoryRequester
            Dim oprodmanagement As New CProductManagement
            Dim cartItems As ArrayList = m_objXMLCart.GetCartItems()
            Dim outbound() As OutboundInventory
            ReDim outbound(cartItems.Count - 1)
            Dim i As Integer = 0
            For Each cartItem As CCartItem In cartItems
                Dim code As String = oprodmanagement.GetSkuOrCode(cartItem.ProductID, cartItem.Attributes)
                outbound(i) = New OutboundInventory
                outbound(i).ItemCode = code
                outbound(i).Id = i
                i += 1
            Next

            Dim inventory() As InboundInventory = invChecker.CheckOrderInventory(outbound)
            If inventory Is Nothing Then
                Return True
            End If

            For Each invItem As InboundInventory In inventory
                oprodmanagement.UpdateQuantity(CType(cartItems(invItem.Id), CCartItem).ProductID, CType(cartItems(invItem.Id), CCartItem).Attributes, invItem.Available)
            Next
            Return True
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = m_objMessages.GetXMLMessage("Payment.aspx", "InventoryCheck", "InventoryCheck")
            Return False
        End Try

    End Function

    Private Function isSWitchSoloActive() As Boolean
        Dim node As XmlNode
        For Each node In dom.Item("SiteProducts").Item("CreditCards")
            If node.Item("Name").InnerText.ToLower = "switch" Or node.Item("Name").InnerText.ToLower = "solo" Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub SetDisplayObjects(ByVal objOrder As COrder)
        tblCreditCard.Visible = False
        tblPurchaseOrder.Visible = False
        tblECheck.Visible = False

        'sp7
        btnPayPalCompleteOrder.Visible = False
        imgPayPalOrder.Visible = False
        'end sp7

        If objProcessor.CCOnline And StoreFrontConfiguration.AdminStore.Item("CVVIsActive").InnerText() = "1" Then
            Me.txtSecureCode.Visible = True
            tdSecurityCode.Visible = True
        Else
            txtSecureCode.Visible = False
            tdSecurityCode.InnerHtml = "&nbsp;"
        End If
        If (objProcessor.DisplayTables) Then
            If (strMethod.ToLower = "credit card") Then
                btnCompleteOrder.Attributes.Add("onclick", "return SetValidationComplete();")
                tblCreditCard.Visible = True
                tblPurchaseOrder.Visible = False
                tblECheck.Visible = False
                If isSWitchSoloActive() Then
                    pnlswitchSoloElements.Visible = True
                    txtCardNumber.MaxLength = 19
                End If
                'Cardinal hot fix
                If objProcessor.PayerAuthEnabled Then
                    payerAuthPanel.Visible = True
                End If
            ElseIf (strMethod.ToLower = "echeck") Then
                btnCompleteOrder.Attributes.Add("onclick", "return SetValidationComplete();")
                tblCreditCard.Visible = False
                tblPurchaseOrder.Visible = False
                tblECheck.Visible = True
            ElseIf (strMethod.ToLower = "po") Then
                btnCompleteOrder.Attributes.Add("onclick", "return SetValidationComplete();")
                tblCreditCard.Visible = False
                tblPurchaseOrder.Visible = True
                tblECheck.Visible = False
            ElseIf (strMethod.ToLower = "phonefax") Then
                ' Check to see if PO is available
                Dim objPayments As New CXMLPaymentMethodAccess
                tblCreditCard.Visible = True
                tblPurchaseOrder.Visible = objPayments.IsPOAvailable
                tblECheck.Visible = False
                objPayments = Nothing
            End If
        ElseIf (objProcessor.IsOffSite) Then
            'Checking if the processor is VeriSignPayFLow Link
            If objProcessor.Name = "VeriSignPayFlowLink" Then
                'eCheck
                If strMethod.ToLower = "echeck" Then
                    If objProcessor.ECheckOnline = True Then
                        btnCompleteOrder.Visible = False
                        imgCompleteOrder.Visible = False
                    Else
                        tblCreditCard.Visible = False
                        tblECheck.Visible = True
                        btnCompleteOrder.Visible = True
                        imgCompleteOrder.Visible = True
                    End If
                ElseIf strMethod.ToLower = "credit card" Then 'CC
                    If objProcessor.CCOnline = True Then
                        btnCompleteOrder.Visible = False
                        imgCompleteOrder.Visible = False
                    Else
                        tblCreditCard.Visible = True
                        tblECheck.Visible = False
                        btnCompleteOrder.Visible = True
                        imgCompleteOrder.Visible = True
                    End If
                ElseIf strMethod.ToLower = "paypal" Then
                    btnCompleteOrder.Visible = False
                    imgCompleteOrder.Visible = False
                    tblCreditCard.Visible = False
                    tblECheck.Visible = False
                End If
            Else
                'update #1894
                If (strMethod.ToLower = "paypal") Then
                    btnCompleteOrder.Visible = False
                    imgCompleteOrder.Visible = False
                    'sp7
                    btnPayPalCompleteOrder.Visible = True
                    imgPayPalOrder.Visible = True
                    btnEdit.Visible = False
                ElseIf (objProcessor.CCOnline = False) Then
                    tblCreditCard.Visible = True
                    btnCompleteOrder.Visible = True
                    imgCompleteOrder.Visible = True
                Else
                    btnCompleteOrder.Visible = False
                    imgCompleteOrder.Visible = False
                End If
            End If
        End If
        If txtCardType.Items.Count = 0 Then
            tblCreditCard.Visible = False
        End If

        'Verisign Recurring Billing
        Dim hasSubscription As Boolean = HasSubscriptionProducts(objOrder)
        'Verisign Recurring Billing

        If (objOrder.GrandTotal = 0) Then
            If (objProcessor.IsOffSite) Then
                btnCompleteOrder.Visible = True
                imgCompleteOrder.Visible = True
            End If
            'Verisign Recurring Billing
            ' if the recurring billing is used with verisign and the grand total is 0, we need to display the
            'credit card panel. But if the recurring billing is not activated does the merchant not need the
            'credit card information so that he can set up the profile manually?
            If objProcessor.Name.ToLower() = "verisign" Then
                'Dim objOrderAddress As COrderAddress
                'Dim objOrderItem As COrderItem
                Dim objproc As New CProcessorBase
                Dim recurringActive As Boolean = objproc.GetRecurringInformationByName("VeriSign")
                If hasSubscription AndAlso recurringActive Then
                    Exit Sub
                End If
            End If
            tblCreditCard.Visible = False
            tblPurchaseOrder.Visible = False
            tblECheck.Visible = False
            'Verisign Recurring Billing
        End If
        'Verisign Recurring Billing
        If hasSubscription Then
            pnlConsent.Visible = True
            If strMethod.ToLower.Equals("paypal") Then
                btnPayPalCompleteOrder.Visible = False
            End If
            btnCompleteOrder.Visible = False
        Else
            pnlConsent.Visible = False
            If strMethod.ToLower.Equals("paypal") Then
                btnCompleteOrder.Visible = False
                btnPayPalCompleteOrder.Visible = True
            Else
                btnCompleteOrder.Visible = True
                btnPayPalCompleteOrder.Visible = False
            End If
        End If
        'Verisign Recurring Billing
    End Sub

    'Verisign Recurring Billing
#Region "Public Function HasSubscriptionProducts(ByVal objOrder As COrder) As Boolean"
    Public Function HasSubscriptionProducts(ByVal objOrder As COrder) As Boolean
        Dim objOrderAddress As COrderAddress
        Dim objOrderItem As COrderItem
        For Each objOrderAddress In objOrder.OrderAddresses
            For Each objOrderItem In objOrderAddress.OrderItems
                If objOrderItem.ProductType = ProductType.Subscription OrElse _
                objOrderItem.ProductType = ProductType.BundleSubscription OrElse _
                objOrderItem.ProductType = ProductType.CustomizedSubscription Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
#End Region
    'Verisign Recurring Billing

#Region "Function ProcessorCode() As String"
    Public Function ProcessorCode() As String
        Dim objOrder As COrder = Session("Order")
        If (objOrder.GrandTotal = 0) Then
            Return ""
        End If
        'Special Check for VeriSignPayFlowLink
        If objProcessor.Name = "VeriSignPayFlowLink" And strMethod <> "PayPal" Then
            If strMethod.ToLower = "echeck" Then
                If objProcessor.ECheckOnline = True Then
                    Return objProcessor.ProcessorCode(m_objCustomer)
                Else
                    Return ""
                End If
            ElseIf strMethod.ToLower = "credit card" Then
                If objProcessor.CCOnline = True Then
                    Return objProcessor.ProcessorCode(m_objCustomer)
                Else
                    Return ""
                End If
            End If
        End If
        'End VeriSignPayFlowLink
        '#update #1894
        If (objProcessor.CCOnline = False And strMethod <> "PayPal") Then
            Return ""
        End If
        If (objProcessor.Name.ToLower = "worldpay") Then
            Session("XMLShoppingCart") = Nothing
            'Tee 11/15/2007 removed unnecessary session variable
            Session.Remove("ItemAdded")
            'end Tee
            m_objxmlcart = Nothing
        End If
        Return objProcessor.ProcessorCode(m_objcustomer)
    End Function
#End Region

#Region "Sub GiftCertificates1_GiftCertificateAdd(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateAdd"
    Private Sub GiftCertificates1_GiftCertificateAdd(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateAdd
        Dim objOrder As COrder = Session("Order")
        TotalDisplay1.DataSource = objOrder
        TotalDisplay1.DataBind()
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False

        objProcessor = New CProcessor(objOrder)

        SetDisplayObjects(objOrder)

    End Sub
#End Region

#Region "Sub GiftCertificates1_GiftCertificateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateError"
    Private Sub GiftCertificates1_GiftCertificateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateError
        ErrorMessage.Text = sender
        ErrorMessage.Visible = True
    End Sub
#End Region

#Region "Sub GiftCertificates1_GiftCertificateRemove(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateRemove"
    Private Sub GiftCertificates1_GiftCertificateRemove(ByVal sender As Object, ByVal e As System.EventArgs) Handles GiftCertificates1.GiftCertificateRemove
        Dim objOrder As COrder = Session("Order")

        GiftCertificates1.ReloadList()
        TotalDisplay1.DataSource = objOrder
        TotalDisplay1.DataBind()
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False

        objProcessor = New CProcessor(objOrder)

        SetDisplayObjects(objOrder)
    End Sub
#End Region

    Private Sub btnCompleteOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompleteOrder.Click
        'Tee 4/25/2007 check ensure customer still in valid session
        If (m_objCustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            Response.Redirect("CustSignInCheckout.aspx" & IIf(Request.QueryString("Affiliate") = "", "", "&Affiliate=" & Request.QueryString("Affiliate")))
        End If
        'end Tee

        Dim bkoQtys As ArrayList = GetBackOrderQuantities()
        If Not UpdateInventoryLevels() Then
            Exit Sub
        End If

        If Not CheckQuantities(bkoQtys) Then
            Exit Sub
        End If

        Dim objPayment As New COrderPayment
        Dim objOrder As COrder = Session("Order")

        Try
            pnlRegular.Visible = True

            If (tblCreditCard.Visible) And (tblPurchaseOrder.Visible = False) Then
                objPayment.Type = 0
                '#update 1930
                If (strMethod.ToLower = "phonefax") Then
                    objPayment.CardType = txtCardType.SelectedItem.Text
                    objPayment.ExpireMonth = txtExpMonth.SelectedItem.Value
                    objPayment.CreditCardNumber = txtCardNumber.Value
                    objPayment.ExpireYear = txtExpYear.SelectedItem.Value
                    objPayment.SecurityCode = txtSecureCode.Text
                Else
                    If txtCardType.SelectedItem.Text <> "" Then
                        objPayment.CardType = txtCardType.SelectedItem.Text
                    Else
                        Me.ErrorMessage.Text = "Please select a card type"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If
                    If txtCardNumber.Value <> "" Then
                        objPayment.CreditCardNumber = txtCardNumber.Value
                    Else
                        Me.ErrorMessage.Text = "Please enter a card number"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If
                    If txtSecureCode.Visible Then
                        '2425
                        If (txtCardType.SelectedItem.Text.ToLower <> "switch") And (txtCardType.SelectedItem.Text.ToLower <> "solo") Then
                            If txtSecureCode.Text <> "" Then
                                objPayment.SecurityCode = txtSecureCode.Text
                            Else
                                Me.ErrorMessage.Text = "Please enter a security code"
                                Me.ErrorMessage.Visible = True
                                Exit Sub
                            End If
                        End If
                    End If
                    If txtExpMonth.SelectedItem.Value <> "" Then
                        objPayment.ExpireMonth = txtExpMonth.SelectedItem.Value
                    Else
                        Me.ErrorMessage.Text = "Please select a month"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If

                    If txtExpYear.SelectedItem.Value <> "" Then
                        objPayment.ExpireYear = txtExpYear.SelectedItem.Value
                    Else
                        Me.ErrorMessage.Text = "Please select a year"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If

                    objPayment.StartMonth = txtStartMonth.SelectedItem.Value
                    objPayment.StartYear = txtStartYear.SelectedItem.Value
                    objPayment.IssueNumber = txtIssueNum.Text

                End If
            End If
            If (tblPurchaseOrder.Visible) Then
                '#853
                objPayment.Type = 1
                If txtPONumber.Text <> "" Then
                    objPayment.PONumber = txtPONumber.Text
                ElseIf tblCreditCard.Visible Then
                    objPayment.Type = 0
                    If txtCardType.SelectedItem.Text <> "" Then
                        objPayment.CardType = txtCardType.SelectedItem.Text
                    Else
                        objPayment.CardType = ""
                    End If
                    If txtCardNumber.Value <> "" Then
                        objPayment.CreditCardNumber = txtCardNumber.Value
                    Else
                        objPayment.CreditCardNumber = ""
                    End If
                    If txtSecureCode.Visible Then
                        If txtSecureCode.Text <> "" Then
                            objPayment.SecurityCode = txtSecureCode.Text
                        Else
                            objPayment.SecurityCode = ""
                        End If
                    End If
                    If txtExpMonth.SelectedItem.Value <> "" Then
                        objPayment.ExpireMonth = txtExpMonth.SelectedItem.Value
                    Else
                        objPayment.ExpireMonth = ""
                    End If
                    If txtExpYear.SelectedItem.Value <> "" Then
                        objPayment.ExpireYear = txtExpYear.SelectedItem.Value
                    Else
                        objPayment.ExpireYear = ""
                    End If
                Else
                    objPayment.PONumber = ""
                End If
            End If
            If (tblECheck.Visible) Then
                objPayment.Type = 2
                If txtCheckNumber.Text <> "" Then
                    objPayment.CheckNumber = txtCheckNumber.Text
                Else
                    Me.ErrorMessage.Text = "Please enter a check number"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                End If
                If txtBankName.Text <> "" Then
                    objPayment.BankName = txtBankName.Text
                Else
                    Me.ErrorMessage.Text = "Please enter a bank name"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                End If
                If txtRoutingNumber.Text <> "" Then
                    objPayment.RoutingNumber = txtRoutingNumber.Text
                Else
                    Me.ErrorMessage.Text = "Please enter a routing number"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                End If
                If txtAccountNumber.Text <> "" Then
                    objPayment.AccountNumber = txtAccountNumber.Text
                Else
                    Me.ErrorMessage.Text = "Please enter an account number"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                End If
                If txtSSN.Text <> "" Then
                    objPayment.SSNumber = txtSSN.Text
                Else
                    Me.ErrorMessage.Text = "Please enter a social security number (SSN)"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                End If
            End If
            objPayment.FilePath = Server.MapPath(Me.TemplateSourceDirectory)
            objOrder.OrderPayment = objPayment
            If objProcessor.RecordPayment Then
                objOrder.SaveOrder(m_objcustomer.GetCustomerID(), objProcessor.SaveCCNumber)
                'update #2160
                If (IsNothing(Session("Referer")) = False) Then
                    m_objcustomer.Referer = objOrder.RetReferer
                    Session("Referer") = m_objcustomer.Referer
                End If
            End If
            '#2407
            If (objOrder.GrandTotal = 0) Or (objOrder.TotalBilledAmt <= 0) Then
                Response.Redirect("Confirm.aspx")
            End If

            If (objProcessor.CCOnline And strMethod.ToLower = "credit card") Or (objProcessor.ECheckOnline And strMethod.ToLower = "echeck") Then
                Try
                    objProcessor.CallProcessor(m_objCustomer.GetSessionID(), Request.ServerVariables("REMOTE_ADDR"))
                    If objProcessor.SupportsPayerAuthentication Then Exit Sub
                Catch objErr As Exception
                    If Not TypeOf objErr Is Threading.ThreadAbortException Then
                        objOrder.DeleteOrder(m_objcustomer.GetCustomerID())
                        If objProcessor.Message = "" Then
                            ErrorMessage.Text = objErr.Message
                        Else
                            ErrorMessage.Text = objProcessor.Message
                        End If
                        ErrorMessage.Visible = True
                    End If
                    Exit Sub
                End Try
                Response.Redirect("Confirm.aspx")
            Else
                Response.Redirect("Confirm.aspx")
            End If
        Catch ex As Exception
            Me.ErrorMessage.Text = ex.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Response.Redirect("Billing.aspx?Edit=True")
    End Sub
    Private Sub objProcessor_RequestPayerAuthentication(ByVal RequestHTML As String) Handles objProcessor.RequestPayerAuthentication
        If objProcessor.Name.ToLower = "clear commerce" Then
            Response.Write(RequestHTML)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            Return
        End If
        pnlRegular.Visible = False
        pnlPayerAuth.Visible = True
        ClientScript.RegisterHiddenField("hdPayerAuthId", String.Empty)
        ClientScript.RegisterHiddenField("hdMD", String.Empty)
        ClientScript.RegisterHiddenField("hdPaRes", String.Empty)
        Session("PayerAuthHtml") = RequestHTML
    End Sub
    Private Sub HandlePayerAuthenticationResponse(ByVal Name As String, ByVal PayerAuthFormFields As NameValueCollection)
        Try
            Dim objFields As NameValueCollection = Request.Form
            Dim objOrder As COrder = Session("Order")
            Dim objProc As CProcessorBase = (New PayementProcessorFactory(objOrder)).CreateProcessor(Name, PayerAuthFormFields)
            objProc.SendRequest()
            Response.Redirect("Confirm.aspx", True)
        Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
            Me.ErrorMessage.Text = ex.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub
    Private Sub HandlePayFlowProLink(ByVal myFields As NameValueCollection)
        Try
            Dim myFormFields As NameValueCollection = Request.Form
            'AB Code - SP6
            Dim strSessionID As String = MyBase.m_objCustomer.GetStrSessionID(CLng(myFormFields("CUSTID")))
            ' Dim myCustomer As New CCustomer(myFormFields("CUSTID"), Nothing)
            Dim myCustomer As New CCustomer(strSessionID, Nothing)
            Dim myorder As COrder
            Dim myOrderUid As Long = CLng(myFormFields("USER1"))
            myorder = New COrder
            myorder.UID = myOrderUid
            myorder.LoadOrder(myCustomer.GetCustomerID())
            myorder.CustomerID = myCustomer.GetCustomerID
            Dim myProcessor As New CVeriSignPayFlowLink(myorder, myCustomer.GetSessionID)
            myProcessor.SetResponse(myFormFields)
            Session("Order") = myorder
            MyBase.m_objCustomer = myCustomer
            Response.Redirect("Confirm.aspx", True)
        Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
            Session("Order") = Nothing
            MyBase.m_objCustomer = Nothing
            Me.ErrorMessage.Text = ex.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>btnPayPalCompleteOrder.Click event
    ' </summary>
    ' <remarks>Handles the click event for PayPal specific orders
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	05/15/2005
    '		Last Revised on :
    ' </history>
    ' -----------------------------------------------------------------------------

    Private Sub btnPayPalCompleteOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayPalCompleteOrder.Click
        Server.Transfer("HandlePayPalExpress.aspx?WebID=" & m_objCustomer.GetSessionID & "&From=Payment", True)
    End Sub
    'End SP7
    Private Sub chkConsent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkConsent.CheckedChanged
        Dim objOrder As COrder = Session("Order")
        Dim objMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
        strMethod = objMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod)
        If chkConsent.Checked Then
            If strMethod.ToLower.Equals("paypal") Then
                btnPayPalCompleteOrder.Visible = True
                btnCompleteOrder.Visible = False
            Else
                btnCompleteOrder.Visible = True
                btnPayPalCompleteOrder.Visible = False
            End If
        Else
            btnPayPalCompleteOrder.Visible = False
            btnCompleteOrder.Visible = False
        End If
    End Sub


End Class
