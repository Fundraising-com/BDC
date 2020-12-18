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

Imports StoreFront.BusinessRule
Imports CSR.CSRBusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Public Class CSRWebControl
    Inherits CWebControl
    Private bError As Boolean
    Protected CSRCustomer As CCustomer
    Protected CSROrder As CSROrder
    Public Property IsError() As Boolean
        Get
            Return bError
        End Get
        Set(ByVal Value As Boolean)
            bError = Value
        End Set
    End Property
    Public ReadOnly Property GetItemPriceWithoutAttributes(ByVal Item As CCartItem) As Decimal
        Get
            Dim dPrice As Decimal

            If Item.IsItemEbay Then
                dPrice = Item.EbayPrice
                Return dPrice
            ElseIf (Item.IsOnSale) Then
                dPrice = Item.SalePrice
            Else
                dPrice = Item.Price
            End If
            dPrice = dPrice

            If Item.VolumePrice() < dPrice Then
                dPrice = Item.VolumePrice()
            End If
            If Item.CustomerSpecificPrice() < dPrice Then
                dPrice = Item.CustomerSpecificPrice()
            End If

            Return dPrice

        End Get

    End Property
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub GetOrder()
        CSROrder = Session("CSROrder")
        CSRCustomer = Session("CSRCustomer")

    End Sub
    Public Function checkPhoneNumber(ByVal strPhone As String) As Boolean
        strPhone = strPhone.Trim
        strPhone.Replace("-", "")
        strPhone.Replace("(", "")
        strPhone.Replace(")", "")
        strPhone.Replace("+", "")
        If strPhone.Length < 10 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function GetIndividualItems() As ArrayList
        Dim arList As New ArrayList
        Dim ShipQuantity As Long = 0 'update #2055
        Dim iGiftWrapQty As Long
        Dim j As Long = -1, i As Long = 0, iGiftWrapIndex As Long = 0
        Dim OrderItem As CSROrderItem
        Dim OrderAddress As CSROrderAddress
        Dim NewOrderItem As CSROrderItem
        For Each OrderAddress In CSROrder.OrderAddresses
            For Each OrderItem In OrderAddress.OrderItems
                iGiftWrapIndex = 0
                iGiftWrapQty = OrderItem.GiftWrapQty

                'update #2055
                'update #2380

                If StoreFrontConfiguration.BackOrderBillType = SystemBase.StoreFrontConfiguration.BackOrderBilling.BillOnShipped Then
                    ShipQuantity = OrderItem.Quantity - OrderItem.BackOrderQuantity
                End If
                For i = 0 To OrderItem.Quantity - 1
                    NewOrderItem = OrderItem.GetCopy(OrderItem)
                    NewOrderItem.Quantity = 1
                    If ShipQuantity < 1 Then
                        NewOrderItem.BackOrderQuantity = 1
                    Else
                        NewOrderItem.BackOrderQuantity = 0
                    End If
                    NewOrderItem.GiftWrapQty = 0
                    NewOrderItem.GiftWraps.Clear()
                    If (iGiftWrapQty > 0) Then
                        NewOrderItem.GiftWrapQty = 1
                        If (IsNothing(OrderItem.GiftWraps) = False) Then
                            NewOrderItem.AddGiftWrap(OrderItem.GiftWraps.Item(iGiftWrapIndex))
                        End If
                        iGiftWrapQty = iGiftWrapQty - 1
                        iGiftWrapIndex = iGiftWrapIndex + 1
                    End If

                    arList.Add(NewOrderItem)
                    j = j - 1
                    'update #2055
#If AE = True Then
                    ShipQuantity = ShipQuantity - 1
                    If ShipQuantity < 0 Then
                        ShipQuantity = 0
                    End If
#End If
                Next
            Next
        Next

        Return arList
    End Function

    Public Function GetProduct(ByVal code As String) As CProductManagement
        Try
            'Load Product Object
            Dim oInventory As New CSRInventory
            Dim dr As DataRow = oInventory.GetInventoryBySku(code)
            If dr Is Nothing Then
                Dim pm As New CProductManagement
                Dim lUid As Long
                lUid = pm.ProductUID(code)
                pm = New CProductManagement(lUid)
                Return pm
            Else
                Dim pm As New CProductManagement(CLng(dr("ProductID")))
                Return pm
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CanAdd(ByVal OrderItem As CCartItem, ByVal Order As CSROrder) As Boolean
        Dim bReturn As Boolean = True
        If Not OrderItem.Inventory Is Nothing Then
            Dim qty As Integer = OrderItem.Quantity
            Dim BackOrderQuantity As Integer = 0
            If OrderItem.Inventory.InventoryTracked Then
                Dim Prod As CSROrderItem
                Dim OrderAddress As CSROrderAddress
                'Dim newCartItemAttr As CAttribute
                'Dim newCartItemAttrDetail As CAttributeDetail
                'Dim cartItemAttr As CAttribute
                'Dim cartItemAttrDetail As CAttributeDetail
                For Each OrderAddress In Order.OrderAddresses
                    For Each Prod In OrderAddress.OrderItems
                        '2507
                        If Prod.ProductID = OrderItem.ProductID Then
                            If Prod.Attributes.Count > 0 Then
                                If Prod.Inventory.get_Id(Prod.Attributes) = OrderItem.Inventory.get_Id(OrderItem.Attributes) Then
                                    qty = Prod.Quantity + qty
                                    BackOrderQuantity = BackOrderQuantity + Prod.BackOrderQuantity
                                End If
                            Else
                                qty = Prod.Quantity + qty
                                BackOrderQuantity = BackOrderQuantity + Prod.BackOrderQuantity
                            End If

                            '2507
                        End If
                    Next
                Next
                If OrderItem.Inventory.ItemsAreStocked(OrderItem.Attributes, qty) = False Then
                    'not enough in stock
                    If OrderItem.Inventory.CanBackOrder = True Then
                        Dim lOldQty As Long = OrderItem.Quantity
                        OrderItem.Quantity = qty - BackOrderQuantity
                        'MagicAjax.AjaxCallHelper.WriteAlert(OrderItem.BackOrderQuantity() & " of this product have been put on backorder.")
                        OrderItem.Quantity = lOldQty
                    Else
                        MagicAjax.AjaxCallHelper.WriteAlert("There are only " & OrderItem.Inventory.InventoryCount(OrderItem.Attributes) & " of this product in stock.")
                        bError = True
                        bReturn = False
                    End If


                End If
            End If
        End If
        Return bReturn
    End Function

    Public Function getItem(ByVal code As String, ByVal bAddingToOrder As Boolean, ByVal SearchResultsdatagriditem As DataGridItem, Optional ByRef ItemPrice As Decimal = 0, Optional ByRef bOverride As Boolean = False) As CCartItem
        'Validate Existence
        Dim product As CProductManagement
        Dim item As CCartItem
        'Dim OrderItem As CSROrderItem
        Dim CustGroup As Long

        Dim qty As Long
        Dim QuantityTextBox As TextBox
        Dim NewPriceTextBox As TextBox
        Dim OldPriceHidden As HtmlInputHidden
        If SearchResultsdatagriditem Is Nothing Then
            'From Order Form
            QuantityTextBox = FindControl("NewQuantity")
            NewPriceTextBox = FindControl("NewPrice")
            OldPriceHidden = FindControl("OldPrice")
        Else
            'From Search Page
            QuantityTextBox = SearchResultsdatagriditem.FindControl("NewQuantity")
            NewPriceTextBox = SearchResultsdatagriditem.FindControl("NewPrice")
            OldPriceHidden = SearchResultsdatagriditem.FindControl("OldPrice")
        End If
        If bAddingToOrder = True Then
            If IsNumeric(QuantityTextBox.Text) = False Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please enter a valid quantity.")
                bError = True
                Return Nothing
            End If
            If IsNumeric(NewPriceTextBox.Text) = False Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please enter a valid price.")
                bError = True
                Return Nothing
            End If
            If CInt(QuantityTextBox.Text) <= 0 Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please enter a valid quantity.")
                bError = True
                Return Nothing
            End If
            qty = CInt(QuantityTextBox.Text)
        Else
            qty = 1
        End If


        CustGroup = CSRCustomer.CustomerGroup

        Try
            product = GetProduct(code)

            If product Is Nothing Then
                Return Nothing
            End If
            'Price Groups are not being used since no calculation is taking place.
            Dim dr As dsProducts.ProductsRow = CType(product.GetProductRow(product.uid, 0).Products.Rows(0), dsProducts.ProductsRow)

            Dim oInventory As New CInventory
            'Dim HasAtts As Boolean

            'Dim dr2 As DataRow
            Dim Atts As ArrayList = Nothing
            If bAddingToOrder = False Then
                'if just pulling item from databases, add all attributes to product
            Else
                Atts = GetAttributesFromPage(SearchResultsdatagriditem)
            End If
            If bError = False Then
                item = New CCartItem(dr, qty, Atts, CustGroup)
                If bAddingToOrder = True Then
                    If NewPriceTextBox.Text.Trim <> OldPriceHidden.Value.ToString.Trim Then
                        bOverride = True
                        ItemPrice = CDec(NewPriceTextBox.Text) + item.CustomerSpecificAttributePrice(item.AttributesTotal)
                        If ItemPrice < 0 Then
                            ItemPrice = 0
                        End If
                    Else
                        ItemPrice = item.ItemPrice
                    End If
                End If

                Return item
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Sub AddProductToOrder(ByVal ProdCode As String, Optional ByVal SearchResultsdatagriditem As DataGridItem = Nothing)
        Dim oOrderItem As CCartItem
        Dim ItemPrice As Decimal
        Dim bOverride As Boolean = False
        oOrderItem = getItem(ProdCode, True, SearchResultsdatagriditem, ItemPrice, bOverride)
        'this will set the ItemPrice with ItemPrice or override price, sets price override whichever appropriate
        If oOrderItem Is Nothing Then
            Exit Sub
        End If
        Dim iPackage As Integer = 0

        Dim objOrderAddress As CSROrderAddress


        objOrderAddress = CSROrder.OrderAddresses(iPackage)

        If CanAdd(oOrderItem, CSROrder) = True Then
            objOrderAddress.AddCSRItem(oOrderItem, ItemPrice, bOverride)
            objOrderAddress.SetPackageIndex(0)
            'If objOrderAddress.NoShipCharge = False Then
            '    objOrderAddress.ShippingObject.RefreshShippingAmount = True
            'End If
            If objOrderAddress.CarrierCode = "" Then
                objOrderAddress.CarrierCode = "NONE"
            End If
            Session("CSRNewProduct") = ""
            Session("CSROrder") = CSROrder
            ResetBackOrdersForSpecificItem(oOrderItem)
            CSROrder.SetPackageIndexes()
        Else
            bError = True
        End If
    End Sub
    Public Sub ResetBackOrdersForSpecificItem(ByVal OrderItem As CProduct)


        Dim ItemInventoryQuantity As Long
        If IsNothing(OrderItem.Inventory) = False Then
            If OrderItem.Inventory.InventoryTracked = True Then

                CSROrder = Session("CSROrder")
                ItemInventoryQuantity = OrderItem.Inventory.InventoryCount(OrderItem.Attributes)
                Dim OrderAddress As CSROrderAddress
                Dim Prod As CSROrderItem
                Dim qty As Long
                For Each OrderAddress In CSROrder.OrderAddresses
                    For Each Prod In OrderAddress.OrderItems
                        '2507
                        If Prod.ProductID = OrderItem.ProductID Then
                            If Prod.Attributes.Count > 0 Then
                                If Prod.Inventory.get_Id(Prod.Attributes) = OrderItem.Inventory.get_Id(OrderItem.Attributes) Then
                                    qty = Prod.Quantity
                                    If qty > ItemInventoryQuantity Then
                                        Prod.BackOrderQuantity = qty - ItemInventoryQuantity
                                        ItemInventoryQuantity = 0
                                    Else
                                        Prod.BackOrderQuantity = 0
                                        ItemInventoryQuantity = ItemInventoryQuantity - qty
                                    End If
                                End If
                            Else
                                qty = Prod.Quantity
                                If qty > ItemInventoryQuantity Then
                                    Prod.BackOrderQuantity = qty - ItemInventoryQuantity
                                    ItemInventoryQuantity = 0
                                Else
                                    Prod.BackOrderQuantity = 0
                                    ItemInventoryQuantity = ItemInventoryQuantity - qty
                                End If
                            End If

                            '2507
                        End If
                    Next
                Next
            End If
            Session("CSROrder") = CSROrder
        End If

    End Sub
    Private Function GetAttributesFromPage(ByVal SearchResultsdatagriditem As DataGridItem) As ArrayList

        Dim dlItem As DataListItem
        'Dim oCont As Control
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        Dim objAttributes As DropDownList
        Dim sAttName As String
        Dim AttributeControl As CSRAttributes
        If SearchResultsdatagriditem Is Nothing Then
            'from Order Form Page, just find control on page
            AttributeControl = Me.FindControl("CAttributeControl1")
        Else
            'From Search Page, get Attribute control from SearchResultsdatagriditem
            AttributeControl = SearchResultsdatagriditem.FindControl("CAttributeControl1")
        End If
        If AttributeControl Is Nothing OrElse AttributeControl.Visible = False Then
            Return Nothing
        Else

            Dim OrderAttributes As New ArrayList
            'set attributes 
            DlAttributes = CType(AttributeControl.FindControl("DlAttributes"), DataList)

            For Each dlItem In DlAttributes.Items


                'late bind based on dropdown or radiolist
                objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text

                '


                If objAttributes.SelectedItem Is Nothing Then
                    MagicAjax.AjaxCallHelper.WriteAlert(objAttributes.Items(0).Text & " required")
                    bError = True
                    Return Nothing
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    MagicAjax.AjaxCallHelper.WriteAlert(objAttributes.Items(0).Text & " required")
                    bError = True
                    Return Nothing
                Else
                    Dim oAttStorage As New CAttributesSelected
                    oattStorage.UID = CLng(objAttributes.SelectedItem.Value())
                    sTemp = CType(dlItem.FindControl("AttributeID"), TextBox).Text
                    oattStorage.AttributeId = CLng(sTemp)
                    OrderAttributes.Add(oattStorage)
                End If


            Next

            'set Custom attributes
            DlCustomAttributes = CType(AttributeControl.FindControl("dlCustomAttributes"), DataList)

            For Each dlItem In DlCustomAttributes.Items
                Dim oAttStorage As New CAttributesSelected
                IsRequired = CType(dlItem.FindControl("CustomRequired"), TextBox).Text
                If IsRequired = True Then
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) = "" Then
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        MagicAjax.AjaxCallHelper.WriteAlert(sAttName & " Required")
                        bError = True
                        Return Nothing
                    Else
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        OrderAttributes.Add(oattStorage)
                    End If
                Else
                    'Not required
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) <> "" Then
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        OrderAttributes.Add(oattStorage)
                    End If
                End If
            Next

            Return OrderAttributes

        End If
    End Function



End Class

