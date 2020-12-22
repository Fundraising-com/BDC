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

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.WebRequest

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Imports StoreFront.StoreFront.Email

Partial Class Confirm1
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents HeadRow As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PhoneFaxRow As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tblECheck As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblCheckNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblBankName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRoutingNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblAccountNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblSSN As System.Web.UI.WebControls.Label
    Protected COrderDetailControl1 As COrderDetailControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        ' begin: JDB - Google Analytics
        If StoreFrontConfiguration.GoogleAnalyticsID.Length > 0 Then
            '*** New Code ©copyright Stokes Web Development www.stokesweb.com
            CType(FindControl("BodyTag"), System.Web.UI.HtmlControls.HtmlGenericControl).Attributes.Add("onLoad", "javascript:__utmSetTrans()")
            '*** End New Code
        End If
        ' end: JDB - Google Analytics
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
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If Not IsNothing(Session("anonymous")) And StoreFrontConfiguration.AccountCreationAllowed Then
            If Not IsNothing(m_objcustomer.CustPassWord) Then
                If m_objcustomer.CustPassWord.Trim = "" Then
                    trSave.Visible = True
                    lblemail.Text = m_objcustomer.Email
                Else
                    trSave.Visible = False
                End If
            Else
                lblemail.Text = m_objcustomer.Email
                trSave.Visible = True
            End If
        Else
            trSave.Visible = False
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

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
        'note: GJV - removed because both conditions result in the same imageurl
        'If (Request.QueryString("WorldPay") <> "") Then
        '    btnView.ImageUrl = StyleLinkURL() & "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("View").Attributes("Filepath").Value
        'Else
        btnView.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("ViewAndPrintReceipt").Attributes("Filepath").Value
        'End If
        ' begin: JDB - Google Analytics
        If StoreFrontConfiguration.GoogleAnalyticsID.Length > 0 Then
            '*** New Code ©copyright Stokes Web Development www.stokesweb.com
            CType(FindControl("myGoogle"), Literal).Text = doGoogle()
            '*** End New Code
        End If
        ' end: JDB - Google Analytics
    End Sub

    ' begin: JDB - Google Analytics
    '*** New Code ©copyright Stokes Web Development www.stokesweb.com
    Public Function doGoogle() As String
        Try
            Dim myOutput As String
            myOutput = "<form style=""display:none;"" name=""utmform"">" & vbCrLf
            myOutput += "<textarea id=""utmtrans"">UTM:T|"
            myOutput += m_objPFNR.OrderNumber & "|"
            Try
                myOutput += m_objPFNR.RetReferer & "|"
            Catch
                myOutput += "n/a|"
            End Try
            myOutput += m_objPFNR.GrandTotal & "|"
            myOutput += (m_objPFNR.LocalTax + m_objPFNR.StateTax + m_objPFNR.CountryTax) & "|"
            myOutput += (m_objPFNR.ShippingTotal + m_objPFNR.HandlingTotal) & "|"
            myOutput += m_objPFNR.BillAddress.City & "|"
            myOutput += m_objPFNR.BillAddress.State & "|"
            myOutput += m_objPFNR.BillAddress.Country & vbCrLf

            Dim myOrderAddress As COrderAddress
            Dim myItem As COrderItem
            For Each myOrderAddress In m_objPFNR.OrderAddresses
                For Each myItem In myOrderAddress.OrderItems
                    myOutput += "UTM:I|"
                    myOutput += m_objPFNR.OrderNumber & "|"
                    myOutput += myItem.ProductCode & "|"
                    myOutput += myItem.Name & "|"
                    myOutput += myItem.CategoryParent & "|"
                    myOutput += myItem.ItemPrice & "|"
                    myOutput += myItem.Quantity & vbCrLf
                Next
            Next
            myOutput += "</textarea></form>"

            Return myOutput
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    '*** End New Code
    ' end: JDB - Google Analytics

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
            'objOrder.SetOrderID(m_objCustomer.GetCustomerID())
            objOrder.SetOrderID(MyBase.m_objCustomer)
            lblOrderNumber.Text = objOrder.OrderNumber
        Else
            HeadRow.Visible = False
        End If

        Dim objDiscountManager As New CDiscountManager
        Dim objGiftCertificateManager As New CGiftCertificateManager

        'Set OneTime Coupons to inactive and add to the UsedCoupon arraylist
        If (IsNothing(objOrder.Coupons) = False) Then
            Dim objCoupon As CDiscount
            For Each objCoupon In objOrder.Coupons
                If (objCoupon.Expires = "OneTime") Then
                    objDiscountManager.SetCouponToIsActive(objCoupon)
                End If
            Next
        End If

        'Delete from Shopping Cart
        m_objxmlcart.DeleteFromDB(1)

        txtOrderID.Value = objOrder.UID
        Session("ConfirmOrderID") = objOrder.UID
        Session("ConfirmDownloadDate") = objOrder.DownloadDate

        Dim ar As New ArrayList
        Dim objAddress As COrderAddress
        Dim objItem As COrderItem

        ' Send Confirmation Email
        If (Request.QueryString("PayPal") <> "") Then
            If (Request.QueryString("PayPal") = "3") Then
                If (Request.Form("payment_status") = "Completed") Then
                    Dim objConfirmEmail As New CConfirmationEmail
                    For Each objAddress In objOrder.OrderAddresses
                        For Each objItem In objAddress.OrderItems
                            ar.Add(objItem)
                            'update inventory #1080
                            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
                                If objItem.Inventory.InventoryTracked Then
                                    objItem.Inventory.UpdateInventory(objItem.Attributes, objItem.Quantity)
                                    'Tee 8/16/2007 product configurator
                                ElseIf Not objItem.Inventory.InventoryTracked AndAlso _
                                objItem.ProductType <> ProductType.Normal AndAlso _
                                objItem.ProductType <> ProductType.Subscription Then
                                    For Each _item As COrderItem In objItem.BundledProducts
                                        If _item.Inventory.InventoryTracked AndAlso Not _item.IsItemEbay Then
                                            _item.Inventory.UpdateInventory(_item.Attributes, _item.BundledQuantity * objItem.Quantity)
                                        End If
                                    Next
                                    'end Tee
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
            Dim objConfirmEmail As New CConfirmationEmail
            For Each objAddress In objOrder.OrderAddresses
                For Each objItem In objAddress.OrderItems
                    ar.Add(objItem)
                    'update inventory #1080
                    If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
                        If objItem.Inventory.InventoryTracked And Not objItem.IsItemEbay Then
                            objItem.Inventory.UpdateInventory(objItem.Attributes, objItem.Quantity)
                            'Tee 8/16/2007 product configurator
                        ElseIf Not objItem.Inventory.InventoryTracked AndAlso _
                        objItem.ProductType <> ProductType.Normal AndAlso _
                        objItem.ProductType <> ProductType.Subscription Then
                            For Each _item As COrderItem In objItem.BundledProducts
                                If _item.Inventory.InventoryTracked AndAlso Not _item.IsItemEbay Then
                                    _item.Inventory.UpdateInventory(_item.Attributes, _item.BundledQuantity * objItem.Quantity)
                                End If
                            Next
                            'end Tee
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
        If ar.Count > 0 Then
            DownloadDisplay1.Visible = True
        Else
            DownloadDisplay1.Visible = False
        End If
        Session("AnonOrder") = objOrder 'ELW MOD'
        Session("Order") = Nothing
        Session("XMLShoppingCart") = Nothing
        'Tee 11/15/2007 removed unnecessary session variable
        Session.Remove("ItemAdded")
        'end Tee
        m_objxmlcart = Nothing
        objOrder = Nothing
    End Sub
#End Region

#Region "Sub HandleWorldPay()"
    Private Sub HandleWorldPay()
        Dim objOrder As COrder = Session("Order")

        Dim str As String
        Dim ar As New ArrayList

        For Each str In Request.Form
            ar.Add(New String(str & "=" & Request.Form(str)))
        Next

        objOrder = New COrder
        objOrder.UID = Request.Form("cartId")
        objOrder.LoadOrder(m_objCustomer.GetCustomerID())
        objOrder.CustomerID = m_objcustomer.GetCustomerID

        Dim objWorldPay As New BusinessRule.Processors.CWorldPay(objOrder)
        objWorldPay.SetResponse(ar)

        If (Request.Form("transStatus") = "C") Then
            Dim nOrderUID As Long = Request.Form("cartId")
            Dim objCancelOrder As New COrder
            objCancelOrder.UID = nOrderUID
            objCancelOrder.DeleteOrder(-1)

            DownloadDisplay1.Visible = False
            btnView.Visible = False
            m_bDeleteOrder = True
            HeadRow.InnerHtml = ""
            objOrder = Nothing
        Else
            ' Load Order from DB
            objOrder = New COrder
            objOrder.UID = Request.Form("cartId")
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            Session("Order") = objOrder
        End If
    End Sub
#End Region

    '#Region "Sub HandlePayPal()"
    '    Private Sub HandlePayPal()
    '        Dim objOrder As COrder = Session("Order")
    '        Dim ar As New ArrayList()
    '        Dim str As String

    '        If (Request.QueryString("PayPal") = "1") Then
    '            ' PayPal Finish Redirect
    '            ' Load Order from DB
    '            objOrder = New COrder()
    '            objOrder.UID = Request.Form("invoice")
    '            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
    '            objOrder.CustomerID = m_objcustomer.GetCustomerID

    '            Session("Order") = objOrder
    '        ElseIf (Request.QueryString("PayPal") = "4") Then
    '            ' PayPal IPN Second Response
    '            Response.End()
    '        ElseIf (Request.QueryString("PayPal") = "3") Then
    '            ' PayPal IPN First Response
    '            objOrder = New COrder()
    '            objOrder.UID = Request.Form("invoice")
    '            m_objCustomer = New CCustomer(Request.Form("custom").ToString(), StoreFrontConfiguration.XMLDocument)
    '            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
    '            objOrder.CustomerID = m_objcustomer.GetCustomerID

    '            objOrder.SetOrderID(MyBase.m_objCustomer)

    '            Dim objPayPal As New BusinessRule.Processors.CPayPal(objOrder)
    '            Dim obj As New CWebRequest()
    '            obj.Type = BusinessRule.WebRequestType.WEBPOST

    '            If (Request.Form("payment_status") = "Failed" Or Request.Form("payment_status") = "Denied") Then
    '                Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
    '                objManagement.CancelOrder()
    '                objManagement = Nothing
    '                'ElseIf (Request.Form("payment_status") = "Completed") Then
    '                '    Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
    '                '    If (objOrder.PaymentsPending = True) Then
    '                '        objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, 0)
    '                '    End If
    '                '    objManagement = Nothing
    '            End If

    '            For Each str In Request.Form
    '                ar.Add(New String(str & "=" & Request.Form(str)))
    '                obj.AddNameValuePair(str, Request.Form(str))
    '            Next
    '            obj.AddNameValuePair("notify_url", StoreFrontConfiguration.SSLPath & "Confirm.aspx?PayPal=4")
    '            obj.SendRequest()
    '            objPayPal.SetResponse(ar)

    '            If (Request.Form("payment_status") = "Completed") Then
    '                Dim objManagement As Management.CManagement = New Management.CManagement(objOrder.OrderNumber)
    '                'objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, 0)
    '                objManagement.SetPaymentsPaid(objOrder.OrderNumber)
    '                objManagement = Nothing
    '            End If

    '            Session("Order") = objOrder
    '        ElseIf (Request.QueryString("PayPal") <> "") Then
    '            ' PayPal Cancel Request
    '            Dim strPayPal As String = Request.QueryString("PayPal")
    '            Dim pair() As String = strPayPal.Split("|")
    '            Dim nOrderUID As Long = CLng(pair.GetValue(0))
    '            Dim objCancelOrder As New COrder()
    '            objCancelOrder.UID = nOrderUID
    '            objCancelOrder.DeleteOrder(-1)
    '            m_bDeleteOrder = True
    '            Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx?WebID=" & pair.GetValue(1))
    '        End If
    '    End Sub
    '#End Region

#Region "Sub HandleBarclay()"
    Private Sub HandleBarclay()
        Dim objOrder As COrder = Session("Order")
        Dim ar As New ArrayList
        Dim str As String = ""
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

            objOrder = New COrder
            objOrder.UID = nOrderID
            objOrder.LoadOrder(m_objCustomer.GetCustomerID())
            objOrder.CustomerID = m_objcustomer.GetCustomerID

            Session("Order") = objOrder

            Dim objBarclay As New BusinessRule.Processors.CBarclay(objOrder)
            objBarclay.SetResponse(ar)
        ElseIf (Request.QueryString("oid") <> "") Then
            objOrder = New COrder
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

    'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        DownloadDisplay1.Visible = False
        Dim err As String = CheckInfo()
        If err <> "" Then
            DownloadDisplay1.Visible = False
            ErrorMessage.Text = err
            ErrorMessage.Visible = True
        Else
            DownloadDisplay1.Visible = False
            Try
                Dim ocust As New Customer
                If m_objcustomer.ValidAccount(m_objcustomer.Email, txtPWD.Text) = False Then
                    ocust.ID = m_objcustomer.GetCustomerID
                    ocust.FirstName = txtFname.Text
                    ocust.LastName = txtLname.Text
                    ocust.Email = m_objcustomer.Email
                    ocust.PassWord = txtPWD.Text
                    ocust.Subscribed = chk.Checked
                    ocust.CustomerGroup = 0
                    m_objcustomer.UpdateCustomer(ocust)

                    Session("anonymous") = Nothing
                    trSave.Visible = False
                    ErrorMessage.Text = "New account created successfully"
                    ErrorMessage.Visible = True
                Else
                    'ELW MOD'
                    Dim objOrder As COrder = Session("AnonOrder")
                    Dim UID As Long = m_objcustomer.GetCustomerUID(m_objcustomer.Email, txtPWD.Text)

                    objOrder.CustomerID = UID

                    objOrder.UpdateOrderCustomerID(objOrder)

                    ErrorMessage.Text = "New account created successfully"
                    ErrorMessage.Visible = True

                    Session("AnonOrder") = Nothing
                    objOrder = Nothing
                    'End ELW MOD'
                End If

            Catch ex As Exception
                ErrorMessage.Text = ex.Message
                ErrorMessage.Visible = True
            End Try
        End If
    End Sub
    Function CheckInfo() As String
        Dim err As String = ""
        If txtFname.Text = "" Then
            err &= "Please enter first name<br>"
        End If
        If txtLname.Text = "" Then
            err &= "Please enter last name<br>"
        End If

        If txtPWD.Text = "" Or txtCPWD.Text = "" Then
            err &= "Please enter password and confirm password<br>"
        Else
            If Not txtPWD.Text.Equals(txtCPWD.Text) Then
                err &= "Both password and confirm password must be same."
            End If
        End If
        Return err
    End Function
    'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

End Class
