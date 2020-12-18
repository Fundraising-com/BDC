'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

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

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Public Class CreditCard
    Inherits CWebPage
    Protected WithEvents btnCompleteOrder As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnEdit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgCompleteOrder As System.Web.UI.WebControls.Image
    Protected WithEvents imgEdit As System.Web.UI.WebControls.Image
    Protected WithEvents txtCardNumber As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents txtCardType As UITools.SelectValControl
    Protected WithEvents txtMonth As UITools.SelectValControl
    Protected WithEvents txtYear As UITools.SelectValControl
    Protected WithEvents TotalDisplay1 As UITools.TotalDisplay
    Protected WithEvents AddressLabel1 As AddressLabel
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCheckNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRoutingNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAccountNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblCreditCard As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPurchaseOrder As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblECheck As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtBankName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtSecureCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSSN As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftCertificates1 As GiftCertificates
    Protected WithEvents tdSecurityCode As System.Web.UI.HtmlControls.HtmlTableCell

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


    Private objProcessor As CProcessor
    Private strMethod As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
             CType(GiftCertificates1.FindControl("btnApply"), LinkButton).Attributes.Add("onclick", "return SetValidationGiftCert();")
            Dim objOrder As COrder = Session("Order")
            objProcessor = New CProcessor(objOrder)
            objOrder.CustomerID = m_objcustomer.GetCustomerID()

            SetPageTitle = m_objMessages.GetXMLMessage("Payment.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)

            AddressLabel1.AddressSource = objOrder.BillAddress

            TotalDisplay1.OandaISO = Session("ConvertISO")
            TotalDisplay1.OandaRate = CDec(Session("OandaRate"))

            Dim objMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
            strMethod = objMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod)

            SetDisplayObjects(objOrder)

            TotalDisplay1.DataSource = objOrder
            TotalDisplay1.DataBind()
            imgEdit.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Edit").Attributes("Filename").Value
            imgCompleteOrder.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("CompleteOrder").Attributes("Filename").Value
        Catch ex As Exception
            Session("DetailError") = "Class Payment Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub SetDisplayObjects(ByVal objOrder As COrder)
        tblCreditCard.Visible = False
        tblPurchaseOrder.Visible = False
        tblECheck.Visible = False

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
                Dim objPayments As New CXMLPaymentMethodAccess()
                tblCreditCard.Visible = True
                tblPurchaseOrder.Visible = objPayments.IsPOAvailable
                tblECheck.Visible = False
                objPayments = Nothing
            End If
        ElseIf (objProcessor.IsOffSite) Then
            'update #1894
            If (strMethod.ToLower = "paypal") Then
                btnCompleteOrder.Visible = False
                imgCompleteOrder.Visible = False
            ElseIf (objProcessor.CCOnline = False) Then
                tblCreditCard.Visible = True
                btnCompleteOrder.Visible = True
                imgCompleteOrder.Visible = True
            Else
                btnCompleteOrder.Visible = False
                imgCompleteOrder.Visible = False
            End If
        End If
        If txtCardType.Items.Count = 0 Then
            tblCreditCard.Visible = False
        End If

        If (objOrder.GrandTotal = 0) Then
            If (objProcessor.IsOffSite) Then
                btnCompleteOrder.Visible = True
                imgCompleteOrder.Visible = True
            End If
            tblCreditCard.Visible = False
            tblPurchaseOrder.Visible = False
            tblECheck.Visible = False
        End If
    End Sub

#Region "Function ProcessorCode() As String"
    Public Function ProcessorCode() As String
        Dim objOrder As COrder = Session("Order")
        If (objOrder.GrandTotal = 0) Then
            Return ""
        End If
        '#update #1894
        If (objProcessor.CCOnline = False And strMethod <> "PayPal") Then
            Return ""
        End If
        If (objProcessor.Name.ToLower = "worldpay") Then
            Session("XMLShoppingCart") = Nothing
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
        Dim objPayment As New COrderPayment()
        Dim objOrder As COrder = Session("Order")

        Try

            If (tblCreditCard.Visible) And (tblPurchaseOrder.Visible = False) Then
                objPayment.Type = 0
                '#update 1930
                If (strMethod.ToLower = "phonefax") Then
                    objPayment.CardType = txtCardType.SelectedItem.Text
                    objPayment.ExpireMonth = txtMonth.SelectedItem.Value
                    objPayment.CreditCardNumber = txtCardNumber.Value
                    objPayment.ExpireYear = txtYear.SelectedItem.Value
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
                        If txtSecureCode.Text <> "" Then
                            objPayment.SecurityCode = txtSecureCode.Text
                        Else
                            Me.ErrorMessage.Text = "Please enter a security code"
                            Me.ErrorMessage.Visible = True
                            Exit Sub
                        End If
                    End If
                    If txtMonth.SelectedItem.Value <> "" Then
                        objPayment.ExpireMonth = txtMonth.SelectedItem.Value
                    Else
                        Me.ErrorMessage.Text = "Please select a month"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If
                    If txtYear.SelectedItem.Value <> "" Then
                        objPayment.ExpireYear = txtYear.SelectedItem.Value
                    Else
                        Me.ErrorMessage.Text = "Please select a year"
                        Me.ErrorMessage.Visible = True
                        Exit Sub
                    End If
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
                        ' Me.ErrorMessage.Text = "Please select a card type"
                        '  Me.ErrorMessage.Visible = True
                        'Exit Sub
                    End If
                    If txtCardNumber.Value <> "" Then
                        objPayment.CreditCardNumber = txtCardNumber.Value
                    Else
                        objPayment.CreditCardNumber = "" 'txtCardNumber.Value
                        ' Me.ErrorMessage.Text = "Please enter a card number"
                        ' Me.ErrorMessage.Visible = True
                        ' Exit Sub
                    End If
                    If txtSecureCode.Visible Then
                        If txtSecureCode.Text <> "" Then
                            objPayment.SecurityCode = txtSecureCode.Text
                        Else
                            objPayment.SecurityCode = ""
                            ' Me.ErrorMessage.Text = "Please enter a security code"
                            ' Me.ErrorMessage.Visible = True
                            ' Exit Sub
                        End If
                    End If
                    If txtMonth.SelectedItem.Value <> "" Then
                        objPayment.ExpireMonth = txtMonth.SelectedItem.Value
                    Else
                        objPayment.ExpireMonth = ""
                        '  Me.ErrorMessage.Text = "Please select a month"
                        '  Me.ErrorMessage.Visible = True
                        '  Exit Sub
                    End If
                    If txtYear.SelectedItem.Value <> "" Then
                        objPayment.ExpireYear = txtYear.SelectedItem.Value
                    Else
                        objPayment.ExpireYear = ""
                        '                        Me.ErrorMessage.Text = "Please select a year"
                        '                       Me.ErrorMessage.Visible = True
                        '                      Exit Sub
                    End If

                    'end #853
                Else
                    objPayment.PONumber = ""

                    '    Me.ErrorMessage.Text = "Please enter a purchase order number"
                    ' Me.ErrorMessage.Visible = True
                    ' Exit Sub
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
                objOrder.SaveOrder(m_objcustomer.GetCustomerID())
                'update #2160
                If (IsNothing(Session("Referer")) = False) Then
                    m_objcustomer.Referer = objOrder.RetReferer
                    Session("Referer") = m_objcustomer.Referer
                End If
            End If

            If (objOrder.GrandTotal = 0) Then
                Response.Redirect("Confirm.aspx")
            End If

            If (objProcessor.CCOnline And strMethod.ToLower = "credit card") Or (objProcessor.ECheckOnline And strMethod.ToLower = "echeck") Then
                Try
                    objProcessor.CallProcessor(Request.ServerVariables("REMOTE_ADDR"))
                Catch objErr As Exception
                    objOrder.DeleteOrder(objOrder.UID, m_objcustomer.GetCustomerID())
                    If objProcessor.Message = "" Then
                        ErrorMessage.Text = objErr.Message
                    Else
                        ErrorMessage.Text = objProcessor.Message
                    End If
                    ErrorMessage.Visible = True
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
End Class
