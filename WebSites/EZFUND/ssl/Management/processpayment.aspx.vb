'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management

Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Public Class ProcessPayment
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents lblPaymethod As System.Web.UI.WebControls.Label
    Protected WithEvents cmdProcess As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblOrderID As System.Web.UI.WebControls.Label
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents printEcheck As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents AddressLabel1 As AddressLabel
    Protected WithEvents lblCCNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblCCType As System.Web.UI.WebControls.Label
    Protected WithEvents lblExpireDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblBankName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRoutingNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblAccountNumber As System.Web.UI.WebControls.Label
    Protected WithEvents tblCreditCard As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPurchaseOrder As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblECheck As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Private objOrder As COrder
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            Dim m_objPaymentMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            Me.ErrorMessage.Visible = False
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            objOrder = New COrder(CType(Session("objOrder"), COrder).OrderNumber)
            AddressLabel1.AddressSource = objOrder.BillAddress
            lblEmail.Text = objOrder.BillAddress.EMail
            lblOrderID.Text = objOrder.OrderNumber
            lblDate.Text = objOrder.OrderDate
            lblPaymethod.Text = m_objPaymentMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod())
            If Not IsPostBack Then
                Session("CallPage") = Request.UrlReferrer.ToString
            End If

            If (IsNothing(objOrder.OrderPayment) = False) Then
                Me.lblAccountNumber.Text = objOrder.OrderPayment.AccountNumber
                Me.lblBankName.Text = objOrder.OrderPayment.BankName
                Me.lblCCNumber.Text = objOrder.OrderPayment.CreditCardNumber
                Me.lblCCType.Text = objOrder.OrderPayment.CardType
                Me.lblExpireDate.Text = objOrder.OrderPayment.ExpireMonth & "/" & objOrder.OrderPayment.ExpireYear
                Me.lblPONumber.Text = objOrder.OrderPayment.PONumber
                Me.lblRoutingNumber.Text = objOrder.OrderPayment.RoutingNumber
            End If

            If lblPaymethod.Text = "eCheck" Then
                printEcheck.Visible = True
                tblCreditCard.Visible = False
                tblECheck.Visible = True
                tblPurchaseOrder.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "credit card" Then
                tblCreditCard.Visible = True
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "po" Then
                tblCreditCard.Visible = False
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = True
            End If
            If lblPaymethod.Text.ToLower = "phonefax" Then
                If Me.lblPONumber.Text.Trim <> "" Then
                    tblCreditCard.Visible = False
                    tblPurchaseOrder.Visible = True
                Else
                    tblCreditCard.Visible = True
                    tblPurchaseOrder.Visible = False
                End If
                tblECheck.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "cod" Then
                tblCreditCard.Visible = False
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = False
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ProcessPayment Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub printEcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printEcheck.Click
        Response.Redirect("PrintEcheck.aspx")
    End Sub

    Private Sub cmdProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcess.Click
        'todo RE Process Back Order Amount ??
        Dim objManagement As New CManagement()
        objOrder = Session("objOrder")
        Dim pendingType As Integer
        If Request.QueryString("Type") <> "" Then
            pendingType = Request.QueryString("Type")
            objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, pendingType)
            Response.Redirect(Session("CallPage"))
            Exit Sub
        End If
        Me.ErrorMessage.Text = "Error Processing Payment"
        Me.ErrorMessage.Visible = True

    End Sub
End Class
