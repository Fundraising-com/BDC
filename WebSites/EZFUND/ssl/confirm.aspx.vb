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

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.WebRequest

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Imports StoreFront.StoreFront.Email

Public Class Confirm1
    Inherits CWebPage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents btnView As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblOrderNumber As System.Web.UI.WebControls.Label
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents DownloadDisplay1 As UITools.DownloadDisplay
    Protected WithEvents WorldPayTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Form2 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents HeadRow As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PhoneFaxRow As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tblCreditCard As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblPurchaseOrder As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblECheck As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblYear As System.Web.UI.WebControls.Label
    Protected WithEvents lblSecureCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblPONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblCheckNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblBankName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRoutingNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblAccountNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblSSN As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreditCardType As System.Web.UI.WebControls.Label
    Protected WithEvents lblCardNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblMonth As System.Web.UI.WebControls.Label
    Protected WithEvents txtOrderID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected COrderDetailControl1 As COrderDetailControl

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

    Private m_bDeleteOrder As Boolean = False
    Public m_objPFNR As COrder

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Request.QueryString("PayPal") <> "") Then
            If (Request.QueryString("PayPal") <> "1" And _
                Request.QueryString("PayPal") <> "3" And _
                Request.QueryString("PayPal") <> "4") Then
                HandlePayPal()
            End If
        End If
        Try
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            SetPageTitle = m_objMessages.GetXMLMessage("Confirm.aspx", "PageTitle", "Title")

            Session("OrderHistory") = Nothing

            If (IsPostBack = False) Then
                If (Request.QueryString("Barclay") <> "" Or Request.QueryString("barclay") <> "" Or Request.QueryString("oid") <> "") Then
                    WorldPayTable.Visible = False
                    HandleBarclay()
                ElseIf (Request.QueryString("WorldPay") <> "") Then
                    WorldPayTable.Visible = True
                    HandleWorldPay()
                ElseIf (Request.QueryString("PayPal") <> "") Then
                    WorldPayTable.Visible = False
                    HandlePayPal()
                Else
                    WorldPayTable.Visible = False
                End If
            End If

        Catch ex As Exception
            Session("DetailError") = "Class Confirm Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        Dim objOrder As COrder = Session("Order")
        m_objPFNR = objOrder

        If (IsNothing(objOrder) = False) Then
            Dim PayMethodname As String = StoreFrontConfiguration.PaymentMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod())
            If (PayMethodname.ToLower = "phonefax") Then
                Session("PFInstruct") = 1
                btnView.Visible = False
                DownloadDisplay1.Visible = False
                COrderDetailControl1.Visible = True
                'PhoneFaxRow.Visible = True
                fillPhoneFaxFields(objOrder)
            Else
                COrderDetailControl1.Visible = False
            End If
            If (IsPostBack = False) Then
                ProcessOrder(objOrder)
            End If
        Else
            If (IsPostBack = False And m_bDeleteOrder = False) Then
                Response.Redirect(StoreFrontConfiguration.SiteURL)
            End If
        End If
        If (Request.QueryString("WorldPay") <> "") Then
            btnView.ImageUrl = StyleLinkURL() & "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("View").Attributes("Filename").Value
        Else
            btnView.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("View").Attributes("Filename").Value
        End If
    End Sub

    Public Function StyleLinkURL() As String
        Return StoreFrontConfiguration.SSLPath
    End Function

    Public Function ProcessorDisplay() As String
        If (Request.QueryString("WorldPay") <> "") Then
            Return "<WPDISPLAY ITEM=banner>"
        Else
            Return ""
        End If
    End Function

    Public ReadOnly Property OrderID() As Long
        Get
            Return Session("ConfirmOrderID")
        End Get
    End Property

    Public ReadOnly Property DownloadDate() As Date
        Get
            Return StoreFrontConfiguration.ConvertToLocalDate(Session("ConfirmDownloadDate"))
        End Get
    End Property

    Private Sub fillPhoneFaxFields(ByVal order As COrder)
        If order.OrderPayment.PONumber <> "" Then
            Me.lblPONumber.Text = order.OrderPayment.PONumber
            Me.tblPurchaseOrder.Visible = True
        End If
        If order.OrderPayment.CreditCardNumber <> "" Then
            Me.lblCardNumber.Text = order.OrderPayment.CreditCardNumber
            Me.lblCreditCardType.Text = order.OrderPayment.CardType
            Me.lblMonth.Text = order.OrderPayment.ExpireMonth
            Me.lblYear.Text = order.OrderPayment.ExpireYear
            Me.lblSecureCode.Text = order.OrderPayment.SecurityCode
            Me.tblCreditCard.Visible = True
        End If
    End Sub

#Region "Sub ProcessOrder(ByVal objOrder As COrder)"
    Private Sub ProcessOrder(ByVal objOrder As COrder)
        ' Get the Real Order Number

        If (objOrder.UID <> 0) Then
            objOrder.SetOrderID(m_objCustomer.GetCustomerID())
            lblOrderNumber.Text = objOrder.OrderNumber
        Else
            HeadRow.Visible = False
        End If

        Dim objDiscountManager As New CDiscountManager()
        Dim objGiftCertificateManager As New CGiftCertificateManager()

        'Set OneTime Coupons to inactive and add to the UsedCoupon arraylist
        If (IsNothing(objOrder.Coupons) = False) Then
            Dim objCoupon As CDiscount
            For Each objCoupon In objOrder.Coupons
                If (objCoupon.Expires = "OneTime") Then
                    objDiscountManager.SetCouponToIsActive(objCoupon)
                End If
            Next
        End If

        If (IsNothing(objOrder.GiftCertificates) = False) Then
            Dim objGiftCertificate As CGiftCertificate
            For Each objGiftCertificate In objOrder.GiftCertificates
                objGiftCertificateManager.SetGiftCertificateToIsActive(objGiftCertificate)
            Next
        End If

        'Delete from Shopping Cart
        m_objxmlcart.DeleteFromDB(1)

        txtOrderID.Value = objOrder.UID
        Session("ConfirmOrderID") = objOrder.UID
        Session("ConfirmDownloadDate") = objOrder.DownloadDate

        Dim ar As New ArrayList()
        Dim objAddress As COrderAddress
        Dim objItem As COrderItem
       
        ' Send Confirmation Email
        If (Request.QueryString("PayPal") <> "") Then
            If (Request.QueryString("PayPal") = "3") Then
                If (Request.Form("payment_status") = "Completed") Then
                    Dim objConfirmEmail As New CConfirmationEmail()
                    For Each objAddress In objOrder.OrderAddresses
                        For Each objItem In objAddress.OrderItems
                            ar.Add(objItem)
                            'update inventory #1080
                            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
                                If objItem.Inventory.InventoryTracked Then
                                    objItem.Inventory.UpdateInventory(objItem.Attributes, objItem.Quantity)
                                End If
                            End If
                        Next
                    Next
                    Try
                        objConfirmEmail.SendConfirmationEmail(objOrder, m_objCustomer, m_arEMailContent)

                    Catch objError As Exception
                        ErrorMessage.Text = objError.Message
                        ErrorMessage.Visible = True
                    End Try
                End If
            End If
        Else
            Dim objConfirmEmail As New CConfirmationEmail()
            For Each objAddress In objOrder.OrderAddresses
                For Each objItem In objAddress.OrderItems
                    ar.Add(objItem)
                    'update inventory #1080
                    If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
                        If objItem.Inventory.InventoryTracked Then
                            objItem.Inventory.UpdateInventory(objItem.Attributes, objItem.Quantity)
                        End If
                    End If

                Next
            Next
            Try
                objConfirmEmail.SendConfirmationEmail(objOrder, m_objCustomer, m_arEMailContent)

            Catch objError As Exception
                ErrorMessage.Text = objError.Message
                ErrorMessage.Visible = True
            End Try
        End If
        DownloadDisplay1.DataSource = ar
        DownloadDisplay1.DataBind()
        Session("Order") = Nothing
        Session("XMLShoppingCart") = Nothing
        m_objxmlcart = Nothing
        objOrder = Nothing
    End Sub
#End Region

#Region "Sub HandleWorldPay()"
    Private Sub HandleWorldPay()
        Dim objOrder As COrder = Session("Order")

        Dim str As String
        Dim ar As New ArrayList()

        For Each str In Request.Form
            ar.Add(New String(str & "=" & Request.Form(str)))
        Next

        objOrder = New COrder()
        objOrder.UID = Request.Form("cartId")
        objOrder.LoadOrder(m_objCustomer.GetCustomerID())
        objOrder.CustomerID = m_objcustomer.GetCustomerID

        Dim objWorldPay As New BusinessRule.Processors.CWorldPay(objOrder)
        objWorldPay.SetResponse(ar)

        If (Request.Form("transStatus") = "C") Then
            Dim nOrderUID As Long = Request.Form("cartId")
            Dim objCancelOrder As New COrder()

            objCancelOrder.DeleteOrder(nOrderUID, -1)

            DownloadDisplay1.Visible = False
            btnView.Visible = False
            m_bDeleteOrder = True
            HeadRow.InnerHtml = ""
            objOrder = Nothing
        Else
            ' Load Order from DB
            objOrder = New COrder()
            objOrder.UID = Request.Form("cartId")
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            Session("Order") = objOrder
        End If
    End Sub
#End Region

#Region "Sub HandlePayPal()"
    Private Sub HandlePayPal()
        Dim objOrder As COrder = Session("Order")
        Dim ar As New ArrayList()
        Dim str As String

        If (Request.QueryString("PayPal") = "1") Then
            ' PayPal Finish Redirect
            ' Load Order from DB
            objOrder = New COrder()
            objOrder.UID = Request.Form("invoice")
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            Session("Order") = objOrder
        ElseIf (Request.QueryString("PayPal") = "4") Then
            ' PayPal IPN Second Response
            Response.End()
        ElseIf (Request.QueryString("PayPal") = "3") Then
            ' PayPal IPN First Response
            objOrder = New COrder()
            objOrder.UID = Request.Form("invoice")
            m_objCustomer = New CCustomer(Request.Form("custom").ToString(), StoreFrontConfiguration.XMLDocument)
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            objOrder.SetOrderID(m_objCustomer.GetCustomerID())

            Dim objPayPal As New BusinessRule.Processors.CPayPal(objOrder)
            Dim obj As New CWebRequest()
            obj.Type = BusinessRule.WebRequestType.WEBPOST

            If (Request.Form("payment_status") = "Failed" Or Request.Form("payment_status") = "Denied") Then
                Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
                objManagement.CancelOrder()
                objManagement = Nothing
                'ElseIf (Request.Form("payment_status") = "Completed") Then
                '    Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
                '    If (objOrder.PaymentsPending = True) Then
                '        objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, 0)
                '    End If
                '    objManagement = Nothing
            End If

            For Each str In Request.Form
                ar.Add(New String(str & "=" & Request.Form(str)))
                obj.AddNameValuePair(str, Request.Form(str))
            Next
            obj.AddNameValuePair("notify_url", StoreFrontConfiguration.SSLPath & "Confirm.aspx?PayPal=4")
            obj.SendRequest()
            objPayPal.SetResponse(ar)

            If (Request.Form("payment_status") = "Completed") Then
                Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
                'objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, 0)
                objManagement.SetPaymentsPaid(objOrder.OrderNumber)
                objManagement = Nothing
            End If

            Session("Order") = objOrder
        ElseIf (Request.QueryString("PayPal") <> "") Then
            ' PayPal Cancel Request
            Dim strPayPal As String = Request.QueryString("PayPal")
            Dim pair() As String = strPayPal.Split("|")
            Dim nOrderUID As Long = CLng(pair.GetValue(0))
            Dim objCancelOrder As New COrder()

            objCancelOrder.DeleteOrder(nOrderUID, -1)
            m_bDeleteOrder = True
            Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx?WebID=" & pair.GetValue(1))
        End If
    End Sub
#End Region

#Region "Sub HandleBarclay()"
    Private Sub HandleBarclay()
        Dim objOrder As COrder = Session("Order")
        Dim ar As New ArrayList()
        Dim str As String
        Dim strPost() As String
        Dim strPair() As String
        Dim strElem As String
        Dim nOrderID As Long

        If (Request.QueryString("Barclay") = "1") Then
            Dim objBinary() As Byte
            objBinary = Request.BinaryRead(Request.TotalBytes)
            Dim i As Long
            For i = 0 To objBinary.GetUpperBound(0)
                str = str & Chr(CLng(objBinary.GetValue(i)))
            Next
            strPost = Split(str, "&")
            For Each strElem In strPost
                strPair = Split(strElem, "=")
                ar.Add(New String(strPair.GetValue(0) & "=" & strPair.GetValue(1)))
                If (strPair.GetValue(0) = "oid") Then
                    nOrderID = CLng(strPair.GetValue(1))
                End If
            Next

            objOrder = New COrder()
            objOrder.UID = nOrderID
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            Session("Order") = objOrder

            Dim objBarclay As New BusinessRule.Processors.CBarclay(objOrder)
            objBarclay.SetResponse(ar)
        ElseIf (Request.QueryString("oid") <> "") Then
            objOrder = New COrder()
            objOrder.UID = Request.QueryString("oid")
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID
            Session("Order") = objOrder

            Dim objBarclay As New BusinessRule.Processors.CBarclay(objOrder)
            If (objbarclay.GetStatus(objOrder.UID) = False) Then
                Response.Redirect(StoreFrontConfiguration.SSLPath & "Payment.aspx")
            End If
        End If
    End Sub
#End Region

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
        If (IsNothing(Session("ConfirmOrderID")) = True) Then
            Session("ConfirmOrderID") = Request.Form("txtOrderID")
        End If
        Response.Redirect(StoreFrontConfiguration.SiteURL & "OrderDetail.aspx?WebID=" & m_objCustomer.GetSessionID & "&OrderID=" & Session("ConfirmOrderID"))
    End Sub

End Class
