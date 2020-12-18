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
Imports CSR.CSRBusinessRule
Imports StoreFront.BusinessRule.WebRequest
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException
Imports StoreFront.StoreFront.Email
Imports System.Web.Security

Partial Class OrderForm
    Inherits CWebPage
    'Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Top1 As CSRTop
    Protected WithEvents ShippingPackages1 As CSRShippingPackages
    Protected WithEvents Products1 As CSRProducts
    Protected WithEvents Customers1 As CSRCustomer
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Discounts1 As CSRDiscounts
    Protected WithEvents Payment1 As CSRPayment
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
        '  Try
        '       SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)

        SetPageTitle = "Order Entry Form"
        Dim CSRCustomer As CCustomer
        CSRCustomer = Session("CSRCustomer")
        If CSRCustomer Is Nothing Then

            CSRCustomer = New CCustomer(Guid.NewGuid().ToString(), StoreFrontConfiguration.XMLDocument)
CSRCustomer.AddressList.Clear() 'clears out any addresses that were added cause there customerid is -1
            Session("CSRCustomer") = CSRCustomer
        End If
        Dim Order As CSROrder
        Dim objOrderAddress As CSROrderAddress
        If Session("CSROrder") Is Nothing Then
            Order = New CSROrder(CSRCustomer, StoreFrontConfiguration.SiteURL)
            Dim objStoreDiscounts As New CStoreDiscounts
            Dim ShipAddress As New AddressOrder
            objOrderAddress = New CSROrderAddress(ShipAddress)
            objOrderAddress.ShippingObject.RefreshShippingAmount = True
            Order.AddCSROrderAddress(objOrderAddress)
            Order.StoreDiscounts = objStoreDiscounts.GetDiscounts()
            Order.Coupons = m_objxmlcart.AppliedDiscounts
            Session("CSROrder") = Order
        End If
        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then


            If FillShipping.Value = "1" Then
                'Customers1.SetShippingInfo()
                FillShipping.Value = "0"
                ShippingPackages1.RecalculateAllShipping()
            Else
                Customers1.DoSearch()
            End If
            UpdatePage()
        End If

        ' Catch ex As Exception
        ' Session("DetailError") = "Class OrderForm Error=" & ex.Message
        ' Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        '  End Try

    End Sub
    Public Sub Top_Recalculate() Handles Top1.RecalculateOrder
        Products1.bindCart()
        Customers1.DisplayCustomer()
        ShippingPackages1.BindPackages()
        Discounts1.ReCalculate()
        Payment1.SetPaymentMethods()
    End Sub
    Public Sub Top_clearForm() Handles Top1.ClearFormNow
        ClearForm()
    End Sub
    Public Sub Products_Recalculate() Handles Products1.RecalculateOrder
        Products1.bindCart()
        Customers1.SetCustomer()
        Discounts1.ReCalculate()
        Payment1.SetPaymentMethods()
        Customers1.SetAddressListsVisiblility()

    End Sub
    Public Sub Customers_Recalculate() Handles Customers1.RecalculateOrder
        Products1.bindCart()
        Customers1.SetCustomer()
        ShippingPackages1.BindPackages()
        Discounts1.ReCalculate()

    End Sub
    Public Sub Products1_ResetShipping() Handles Products1.ResetShipping
        Customers1.RefillShipping()
    End Sub
    Public Sub ShippingPackages_SetShipAddress() Handles ShippingPackages1.SetShippingAddress
        Customers1.SetShipAddress()
    End Sub
    Public Sub ShippingPackages_Recalculate() Handles ShippingPackages1.RecalculateOrder
        ShippingPackages1.BindPackages()
        Discounts1.ReCalculate()
    End Sub

    Public Sub Payment_Recalculate() Handles Payment1.RecalculateOrder
        Discounts1.ReCalculate()
    End Sub

    Public Sub UpdatePage()
        Products1.bindCart()
        Customers1.SetCustomer()
        ShippingPackages1.BindPackages()
        Discounts1.ReCalculate()
        Payment1.SetPaymentMethods()
        Customers1.SetAddressListsVisiblility()

    End Sub

    Private Sub CompleteOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompleteOrder.Click
        Dim Order As CSROrder
        Order = Session("CSROrder")
        'this next line runs all code to check inputs and save order
        If Order.ItemCount > 0 Then
            If Customers1.CompleteOrder = True AndAlso ShippingPackages1.CheckShipping = True AndAlso Payment1.CompleteOrder = True Then

                Order = Session("CSROrder") 'make sure it is updated, probably don't need this
                ProcessOrder(Order)
                Customers1.SendConfirmationEmail()
                Dim OrderID As String = Order.UID
                Dim CustomerID As String = Order.CustomerID
                Order.InsertCSRReference(Order.UID, Session.Item("CSRUID"))
                ClearForm()
                Response.Redirect("CSRConfirm.aspx?OrderID=" & OrderID & "&CustomerID=" & CustomerID)
            End If
        Else
            MagicAjax.AjaxCallHelper.WriteAlert("There are no products in the order.")
        End If
    End Sub
    Private Sub ProcessOrder(ByVal objOrder As CSROrder)
        ' Get the Real Order Number
        Try
            If (objOrder.UID <> 0) Then
                objOrder.SetOrderID(MyBase.m_objCustomer)
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
            'update inventory
            Dim ar As New ArrayList
            Dim objAddress As CSROrderAddress
            Dim objItem As CSROrderItem
            For Each objAddress In objOrder.OrderAddresses
                For Each objItem In objAddress.OrderItems
                    ar.Add(objItem)
                    'update inventory #1080
                    If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value.ToLower = "ae") Then
                        If objItem.Inventory.InventoryTracked And Not objItem.IsItemEbay Then
                            objItem.Inventory.UpdateInventory(objItem.Attributes, objItem.Quantity)
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            MagicAjax.AjaxCallHelper.WriteAlert(ex.Message)
        End Try
    End Sub
    Private Sub ClearForm()
        Top1.ResetForm()
        Products1.SetNewProdVisibility(False)
    End Sub
End Class
