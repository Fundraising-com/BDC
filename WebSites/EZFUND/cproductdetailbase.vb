'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Xml

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Public Enum ImageSize
    Small = 0
    Large = 1
End Enum

Public Enum DetailTemplate
    Template1 = 1
    Template2 = 2
End Enum

Public Class CProductDetailBase
    Inherits CWebControl

#Region "Events"
    Event AddToCart As EventHandler
    Event AddToSavedCart As EventHandler
    Event EMailFriend As EventHandler
    Event AttributeRequiredError As EventHandler
    Event AddError As EventHandler
    Event ISDetailCall As EventHandler
#End Region

#Region "Class Members"
    Protected WithEvents trProductName As HtmlTableRow
    Protected WithEvents trProductCode As HtmlTableRow
    Protected WithEvents trVendor As HtmlTableRow
    Protected WithEvents trManufacturer As HtmlTableRow
    Protected WithEvents trPrice As HtmlTableRow
    Protected WithEvents trQty As HtmlTableRow
    Protected WithEvents trSalePrice As HtmlTableRow
    Protected WithEvents trStockInfo As HtmlTableRow
    Protected WithEvents trVolumePricing As HtmlTableRow
    Protected WithEvents trVolumePricing3 As HtmlTableRow
    Protected WithEvents trEMailFriend As HtmlTableRow
    Protected WithEvents trSavedCart As HtmlTableRow
    Protected WithEvents trCategory As HtmlTableRow
    Protected WithEvents trDescription As HtmlTableRow
    Protected WithEvents trCustomPrice As HtmlTableRow
    Protected WithEvents trVendorManuSpacer As HtmlTableRow
    Protected WithEvents trCategorySpacer As HtmlTableRow
    Protected WithEvents trDescriptionSpacer As HtmlTableRow
    Protected WithEvents trPriceSpacer As HtmlTableRow
    Protected WithEvents trStockVolumeSpacer As HtmlTableRow
    Protected WithEvents trEMailFriendSpacer As HtmlTableRow
    Protected WithEvents ImageCell As HtmlTableCell
    Protected WithEvents CustomPriceCell As HtmlTableCell
    Protected WithEvents lblYourPrice As HtmlTableCell
    Protected WithEvents lblSalePriceDisplay As HtmlTableCell
    Protected WithEvents lblSavings As HtmlTableCell
    Protected WithEvents trSavings As HtmlTableRow
    Protected WithEvents RecommendedYourPriceCell As HtmlTableCell
    Protected WithEvents RecommendedSalePriceCell As HtmlTableCell
    Protected WithEvents RecommendedCustomPriceCell As HtmlTableCell
    Protected m_AttributeDisplay As Integer = 0
    Private m_objProduct As CProduct
    Protected WithEvents CInventoryControl1 As CInventoryControl
    Protected m_bDisplayImage As Boolean
    Protected m_bDisplayProductCode As Boolean
    Protected m_bDisplayProductName As Boolean
    Protected m_bDisplayCategory As Boolean
    Protected m_bDisplayPriceSalePrice As Boolean
    Protected m_bDisplayShortDescription As Boolean
    Protected m_bDisplayLongDescription As Boolean
    Protected m_bDisplayVendor As Boolean
    Protected m_bDisplayManufacturer As Boolean
    Protected m_bDisplayVolumePricing As Boolean
    Protected m_bDisplayStockInfo As Boolean
    Protected m_bDisplaySavedCartWishList As Boolean
    Protected m_bDisplayEMailFriend As Boolean
    Protected m_bDisplayLabels As Boolean
    Protected m_bDisplayQty As Boolean
    Protected m_bDisplayRecommendedProducts As Boolean
    Protected m_bDisplayRecommendedImage As Boolean
    Protected m_bDisplayRecommendedName As Boolean
    Protected m_bDisplayRecommendedCode As Boolean
    Protected m_bDisplayRecommendedShortDescription As Boolean
    Protected m_bDisplayRecommendedPrice As Boolean
    Protected m_bLinkImage As Boolean
    Protected m_bLinkProductCode As Boolean
    Protected m_bLinkProductName As Boolean
    Protected m_nDefaultQty As Long
    Protected m_strRecommendedTitle As String
    Protected m_nImageSize As Long
    Protected m_bDisplaySavings As Boolean
    Private m_OrderAttributes As ArrayList
    Private m_bIsSalePrice As Boolean
    Private m_bIsCustomPrice As Boolean

#End Region

#Region "Sub LoadSettings(ByVal objDetail As XmlNode)"
    Public Sub LoadSettings(ByVal objDetail As XmlNode)
        m_bDisplayImage = IIf(objDetail.Attributes("DisplayImage").Value = "1", True, False)
        m_bDisplayProductCode = IIf(objDetail.Attributes("DisplayProductCode").Value = "1", True, False)
        m_bDisplayProductName = IIf(objDetail.Attributes("DisplayProductName").Value = "1", True, False)
        m_bDisplayCategory = IIf(objDetail.Attributes("DisplayCategory").Value = "1", True, False)
        m_bDisplayPriceSalePrice = IIf(objDetail.Attributes("DisplayPriceSalePrice").Value = "1", True, False)
        m_bDisplayShortDescription = IIf(objDetail.Attributes("DisplayShortDescription").Value = "1", True, False)
        m_bDisplayLongDescription = IIf(objDetail.Attributes("DisplayLongDescription").Value = "1", True, False)
        m_bDisplayVendor = IIf(objDetail.Attributes("DisplayVendor").Value = "1", True, False)
        m_bDisplayManufacturer = IIf(objDetail.Attributes("DisplayManufacturer").Value = "1", True, False)
        m_bDisplayVolumePricing = IIf(objDetail.Attributes("DisplayVolumePricing").Value = "1", True, False)
        m_bDisplayStockInfo = IIf(objDetail.Attributes("DisplayStockInfo").Value = "1", True, False)
        m_bDisplaySavedCartWishList = IIf(objDetail.Attributes("DisplaySavedCartWishList").Value = "1", True, False)
        m_bDisplayEMailFriend = IIf(objDetail.Attributes("DisplayEMailFriend").Value = "1", True, False)
        m_bDisplayLabels = IIf(objDetail.Attributes("DisplayLabels").Value = "1", True, False)
        m_bDisplayQty = IIf(objDetail.Attributes("DisplayQty").Value = "1", True, False)
        m_bDisplaySavings = IIf(objDetail.Attributes("DisplaySavings").Value = "1", True, False)
        m_bDisplayRecommendedProducts = IIf(objDetail.Attributes("DisplayRecommendedProducts").Value = "1", True, False)
        m_bDisplayRecommendedImage = IIf(objDetail.Attributes("DisplayRecommendedImage").Value = "1", True, False)
        m_bDisplayRecommendedName = IIf(objDetail.Attributes("DisplayRecommendedName").Value = "1", True, False)
        m_bDisplayRecommendedCode = IIf(objDetail.Attributes("DisplayRecommendedCode").Value = "1", True, False)
        m_bDisplayRecommendedShortDescription = IIf(objDetail.Attributes("DisplayRecommendedShortDescription").Value = "1", True, False)
        m_bDisplayRecommendedPrice = IIf(objDetail.Attributes("DisplayRecommendedPrice").Value = "1", True, False)
        m_strRecommendedTitle = objDetail.Attributes("RecommendedTitle").Value

        m_bLinkImage = IIf(objDetail.Attributes("LinkImage").Value = "1", True, False)
        m_bLinkProductCode = IIf(objDetail.Attributes("LinkProductCode").Value = "1", True, False)
        m_bLinkProductName = IIf(objDetail.Attributes("LinkProductName").Value = "1", True, False)
        m_AttributeDisplay = CInt("0" & objDetail.Attributes("AttributeDisplay").Value)

        m_nDefaultQty = CLng(objDetail.Attributes("DefaultQty").Value)

        m_nImageSize = CLng(objDetail.Attributes("ImageSize").Value)

    End Sub
#End Region

#Region "Sub SetDisplay1()"
    Public Sub SetDisplay1()
        Dim CustomPrice As String
        lblYourPrice.Visible = False
        lblSalePriceDisplay.Visible = False
        trProductName.Visible = m_bDisplayProductName
        trProductCode.Visible = m_bDisplayProductCode
        trVendor.Visible = m_bDisplayVendor
        trManufacturer.Visible = m_bDisplayManufacturer
        trPrice.Visible = m_bDisplayPriceSalePrice

        trSavings.Visible = False

        trSalePrice.Visible = False
        If m_bDisplayLabels = True Then
            If Request.QueryString("ID") = 0 Then
                CustomPrice = Me.SetCustomPriceValue(Product.ProductID)
            Else
                CustomPrice = Me.SetCustomPriceValue(Request.QueryString("ID"))
            End If

        Else
            If Request.QueryString("ID") = 0 Then
                CustomPrice = SetCustomPriceValue2(Product.ProductID)
            Else
                CustomPrice = SetCustomPriceValue2(Request.QueryString("ID"))
            End If

        End If

        If CustomPrice <> "" Then
            Dim Temp As String
            If Request.QueryString("ID") = 0 Then
                Temp = SetSavingsValue(Product.ProductID)
            Else
                Temp = SetSavingsValue(Request.QueryString("ID"))
            End If

            trPrice.Visible = False
            CustomPriceCell.InnerHtml = CustomPrice
            Me.trCustomPrice.Visible = True
            If m_bDisplaySavings = True Then
                trSavings.Visible = True
                lblSavings.InnerHtml = "(Savings: " & Temp & ")"
            End If
        Else
            Me.trCustomPrice.Visible = False
        End If

        trStockInfo.Visible = m_bDisplayStockInfo

        trVolumePricing.Visible = m_bDisplayVolumePricing
        trEMailFriend.Visible = m_bDisplayEMailFriend
        trSavedCart.Visible = m_bDisplaySavedCartWishList
        trCategory.Visible = m_bDisplayCategory
        trQty.Visible = m_bDisplayQty

        If (m_bDisplayShortDescription Or m_bDisplayLongDescription) Then
            trDescription.Visible = True
        Else
            trDescription.Visible = False
        End If

        If (m_bDisplayVendor Or m_bDisplayManufacturer) Then
            trVendorManuSpacer.Visible = True
        Else
            trVendorManuSpacer.Visible = False
        End If

        trPriceSpacer.Visible = trSalePrice.Visible

        If (m_bDisplayStockInfo Or m_bDisplayVolumePricing) Then
            trStockVolumeSpacer.Visible = True
        Else
            trStockVolumeSpacer.Visible = False
        End If

        If (m_bDisplayEMailFriend) Then
            trEMailFriendSpacer.Visible = True
        Else
            trEMailFriendSpacer.Visible = False
        End If
    End Sub
#End Region

#Region "Sub SetDisplay2()"
    Public Sub SetDisplay2()
        Dim CustomPrice As String
        trProductName.Visible = m_bDisplayProductName
        trProductCode.Visible = m_bDisplayProductCode
        trVendor.Visible = m_bDisplayVendor
        trManufacturer.Visible = m_bDisplayManufacturer
        trPrice.Visible = m_bDisplayPriceSalePrice
        lblSalePriceDisplay.Visible = False
        lblYourPrice.Visible = False
        'trSalePrice.Visible = m_bDisplayPriceSalePrice
        'If (Product.IsOnSale = False) Then
        '    trSalePrice.Visible = False
        'End If
        trSalePrice.Visible = False
        If m_bDisplayLabels = True Then
            CustomPrice = SetCustomPriceValue(Request.QueryString("ID"))
        Else
            CustomPrice = SetCustomPriceValue2(Request.QueryString("ID"))
        End If

        trQty.Visible = m_bDisplayQty

        If CustomPrice <> "" Then
            Dim Temp As String
            CustomPriceCell.InnerHtml = CustomPrice
            trCustomPrice.Visible = True
            trPrice.Visible = False
            If m_bDisplaySavings = True Then
                Temp = SetSavingsValue(Request.QueryString("ID"))
                trSavings.Visible = True
                lblSavings.InnerHtml = "(Savings: " & Temp & ")"
            End If
        End If

        trStockInfo.Visible = m_bDisplayStockInfo
        trVolumePricing.Visible = m_bDisplayVolumePricing
        trEMailFriend.Visible = m_bDisplayEMailFriend
        trSavedCart.Visible = m_bDisplaySavedCartWishList
        trCategory.Visible = m_bDisplayCategory

        If (m_bDisplayShortDescription Or m_bDisplayLongDescription) Then
            trDescription.Visible = True
        Else
            trDescription.Visible = False
            trDescriptionSpacer.Visible = False
        End If

        If (m_bDisplayVendor Or m_bDisplayManufacturer) Then
            trVendorManuSpacer.Visible = True
        Else
            trVendorManuSpacer.Visible = False
        End If

        If (m_bDisplayCategory) Then
            trCategorySpacer.Visible = True
        Else
            trCategorySpacer.Visible = False
        End If

        trPriceSpacer.Visible = trSalePrice.Visible

        If (m_bDisplayStockInfo Or m_bDisplayVolumePricing) Then
            trStockVolumeSpacer.Visible = True
        Else
            trStockVolumeSpacer.Visible = False
        End If

    End Sub
#End Region

#Region "Sub SetDisplay(ByVal objItem As RepeaterItem)"
    Public Sub SetDisplay(ByVal objItem As RepeaterItem)
        If (m_bDisplayLabels = False) Then
            setVisible(False, objItem.FindControl("lblProductName2"))
            setVisible(False, objItem.FindControl("lblProductCode2"))
            setVisible(False, objItem.FindControl("lblDescription2"))
            setVisible(False, objItem.FindControl("lblPrice2"))
        End If

        If (m_bDisplayRecommendedShortDescription = False) Then
            setVisible(False, CType(objItem.FindControl("trRShortDescription"), HtmlTableRow))
            setVisible(False, CType(objItem.FindControl("trRShortDescriptonSpacer"), HtmlTableRow))
        End If

        If (m_bDisplayRecommendedImage = False) Then
            setVisible(False, CType(objItem.FindControl("tdRImageCell"), HtmlTableCell))
        Else
            If (m_bLinkImage) Then
                If (CType(objItem.DataItem, CProduct).DetailLink = "") Then
                    setVisible(False, CType(objItem.FindControl("lnkImage"), HyperLink))
                Else
                    setVisible(False, CType(objItem.FindControl("SmallImage"), Panel))
                End If
            Else
                setVisible(False, CType(objItem.FindControl("lnkImage"), HyperLink))
            End If
        End If

        If (m_bDisplayRecommendedName = False) Then
            setVisible(False, CType(objItem.FindControl("trRProductName"), HtmlTableRow))
        Else
            If (m_bLinkProductName) Then
                If (CType(objItem.DataItem, CProduct).DetailLink = "") Then
                    setVisible(False, CType(objItem.FindControl("lnkProductName"), HyperLink))
                Else
                    setVisible(False, CType(objItem.FindControl("lblRProductName"), Label))
                End If
            Else
                setVisible(False, CType(objItem.FindControl("lnkProductName"), HyperLink))

            End If
        End If

        If (m_bDisplayRecommendedCode = False) Then
            setVisible(False, CType(objItem.FindControl("trRProductCode"), HtmlTableRow))
        Else
            If (m_bLinkProductCode) Then
                If (CType(objItem.DataItem, CProduct).DetailLink = "") Then
                    setVisible(False, CType(objItem.FindControl("lnkRProductCode"), HyperLink))
                Else
                    setVisible(False, CType(objItem.FindControl("lblRProductCode"), Label))
                End If
            Else
                setVisible(False, CType(objItem.FindControl("lnkProductCode"), HyperLink))
            End If
        End If
        'If Not (CType(objItem.FindControl("btnVolumePricing"), LinkButton) Is Nothing) Then
        '    CType(objItem.FindControl("btnVolumePricing"), LinkButton).Text = "Volume Pricing"
        'End If
        If (m_bDisplayRecommendedPrice = False) Then
            setVisible(False, CType(objItem.FindControl("trRPriceSpacer"), HtmlTableRow))
            setVisible(False, CType(objItem.FindControl("trRPrice"), HtmlTableRow))
            setVisible(False, CType(objItem.FindControl("trRSalePrice"), HtmlTableRow))
        Else
            Dim tempPrice As Decimal
            tempPrice = GetCustomPrice2(CType(objItem.DataItem, CCartItem))
            'tempPrice = GetCustomPrice2(CType(objItem.DataItem, CProduct).ProductID)
            If tempPrice = 0 Then
                setVisible(False, CType(objItem.FindControl("trRSalePrice"), HtmlTableRow))
            Else

                If Not (objItem.FindControl("lblRCustomPrice") Is Nothing) Then
                    objItem.FindControl("trRSalePrice").Visible = True
                    objItem.FindControl("RecommendedSalePriceCell").Visible = False
                    objItem.FindControl("RecommendedYourPriceCell").Visible = False
                    objItem.FindControl("RecommendedCustomPriceCell").Visible = False
                    If Me.m_bDisplayLabels Then
                        If Me.m_bIsCustomPrice Then
                            objItem.FindControl("RecommendedYourPriceCell").Visible = True
                        ElseIf Me.m_bIsSalePrice Then
                            objItem.FindControl("RecommendedSalePriceCell").Visible = True
                        End If
                    Else
                        objItem.FindControl("RecommendedCustomPriceCell").Visible = True

                    End If
                    If objItem.FindControl("trRSalePrice").Visible Then
                        objItem.FindControl("trRPrice").Visible = False
                    End If
                    CType(objItem.FindControl("lblRCustomPrice"), Label).Text = "<S>" & PriceDisplay2(CType(objItem.DataItem, CCartItem).Price) & "</S>" & "&nbsp;" & PriceDisplay2(tempPrice)
                    CType(objItem.FindControl("lblRSalePrice"), Label).Text = "<S>" & PriceDisplay2(CType(objItem.DataItem, CCartItem).Price) & "</S>" & "&nbsp;" & PriceDisplay2(tempPrice)
                    CType(objItem.FindControl("lblRCustomPriceOnly"), Label).Text = "<S>" & PriceDisplay2(CType(objItem.DataItem, CCartItem).Price) & "</S>" & "&nbsp;" & PriceDisplay2(tempPrice)
                End If

            End If
        End If
    End Sub
#End Region

    Private Sub setVisible(ByVal bSeen As Boolean, ByVal obj As Object)
        If Not IsNothing(obj) Then
            obj.visible = bSeen
        End If
    End Sub

#Region "Class Properties"
    Public Property Product() As CCartItem
        Get
            Return m_objProduct
        End Get
        Set(ByVal Value As CCartItem)
            m_objProduct = Value
        End Set
    End Property

    Public ReadOnly Property BaseImageString() As String
        Get
            If (IsNothing(Product) = True) Then
                Return ""
            End If
            If (m_nImageSize = ImageSize.Small) Then
                Return Product.SmallImage
            ElseIf (m_nImageSize = ImageSize.Large) Then
                Return Product.LargeImage
            End If
        End Get
    End Property

    Public ReadOnly Property BaseDescription() As String
        Get
            If (IsNothing(Product) = True) Then
                Return ""
            End If
            If (m_bDisplayShortDescription) Then
                Return Product.ShortDescription
            Else
                Return Product.Description
            End If
        End Get
    End Property

    Public ReadOnly Property RecommendedTitle() As String
        Get
            Return m_strRecommendedTitle
        End Get
    End Property
#End Region

#Region "Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)"
    Public Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objbutton As LinkButton
        objbutton = CType(sender, LinkButton)
        If (objbutton.ID = "btnVolumePricing") Then
            If trVolumePricing3.Visible = True Then
                trVolumePricing3.Visible = False
            Else
                trVolumePricing3.Visible = True
            End If
        End If
    End Sub
#End Region

#Region "Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)"
    Public Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)
        Dim objButton As LinkButton

        objButton = CType(sender, LinkButton)
        objButton.CommandArgument = txtQty.Text

        'get attributes selected
        Set_Item_Attributes(oAttControl)

        If (objButton.ID = "btnAddToCart") Then
            RaiseEvent ISDetailCall(True, EventArgs.Empty)
            RaiseEvent AddToCart(objButton, e)
        ElseIf (objButton.ID = "btnAddToSavedCart") Then
            RaiseEvent AddToSavedCart(objButton, e)
        ElseIf (objButton.ID = "btnEMailFriend") Then
            RaiseEvent EMailFriend(objButton, e)
        ElseIf (objButton.ID = "btnVolumePricing") Then
            If Me.trVolumePricing3.Visible = True Then
                Me.trVolumePricing3.Visible = False
            Else
                Me.trVolumePricing3.Visible = True
            End If
        End If
    End Sub
#End Region

#Region "Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl)"


    Private Sub Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl)

        Dim dlItem As DataListItem
        Dim oCont As Control
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        Dim objAttributes As Object
        Dim sAttName As String
        Session("OrderAttributes") = Nothing

        If oAttributeControl Is Nothing Then
            Exit Sub 'exit 
        Else
            m_OrderAttributes = New ArrayList()
            'set attributes 
            DlAttributes = CType(oAttributeControl.FindControl("DlAttributes"), DataList)

            For Each dlItem In DlAttributes.Items


                'late bind based on dropdown or radiolist
                If oAttributeControl.DisplayType = CAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If

                '    IsRequired = CType(dlItem.FindControl("Required"), TextBox).Text
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text

                '


                If objAttributes.SelectedItem Is Nothing Then
                    ' required raise error

                    RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                    Exit Sub
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    ' required raise error

                    RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                    Exit Sub
                Else
                    Dim oAttStorage As New CAttributesSelected()
                    oattStorage.UID = CLng(objAttributes.SelectedItem.Value())
                    sTemp = CType(dlItem.FindControl("AttributeID"), TextBox).Text
                    oattStorage.AttributeId = CLng(sTemp)
                    m_OrderAttributes.Add(oattStorage)
                End If


            Next

            'set Custom attributes
            DlCustomAttributes = CType(oAttributeControl.FindControl("dlCustomAttributes"), DataList)

            For Each dlItem In DlCustomAttributes.Items
                Dim oAttStorage As New CAttributesSelected()
                IsRequired = CType(dlItem.FindControl("CustomRequired"), TextBox).Text
                If IsRequired = True Then
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) = "" Then
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                        Exit Sub
                    Else
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                Else
                    'Not required
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) <> "" Then
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                End If
            Next

            Session("OrderAttributes") = m_OrderAttributes

        End If
    End Sub

#End Region

#Region "Custom Pricing"



#Region "GetCustomPrice(ByVal Id As Long) As String"
    Private Function GetSavings(ByVal Id As Long) As Decimal
        Dim custPrice As Decimal
        Dim savings As Decimal
        '   Dim tempCartItem As New BusinessRule.CCartItem(Me.m_objXMLAccess.GetProduct(Id), 1, , Me.m_objCustomer.CustomerGroup)

        custPrice = Product.CustomerSpecificPrice
        If Product.IsOnSale Then
            If custPrice < Product.SalePrice Then
                m_bIsCustomPrice = True
                Return Product.Price - custPrice
            Else
                m_bIsSalePrice = True
                Return Product.Price - Product.SalePrice
            End If
        ElseIf custPrice < Product.Price Then
            m_bIsCustomPrice = True
            Return Product.Price - custPrice
        Else
            Return 0
        End If
    End Function

    Private Function GetSavings(ByVal objProduct As CCartItem) As Decimal
        Dim custPrice As Decimal
        Dim savings As Decimal
        '   Dim tempCartItem As New BusinessRule.CCartItem(Me.m_objXMLAccess.GetProduct(Id), 1, , Me.m_objCustomer.CustomerGroup)

        custPrice = objProduct.CustomerSpecificPrice
        If objProduct.IsOnSale Then
            If custPrice < objProduct.SalePrice Then
                m_bIsCustomPrice = True
                Return objProduct.Price - custPrice
            Else
                m_bIsSalePrice = True
                Return objProduct.Price - objProduct.SalePrice
            End If
        ElseIf custPrice < objProduct.Price Then
            m_bIsCustomPrice = True
            Return objProduct.Price - custPrice
        Else
            Return 0
        End If
    End Function

    Private Function GetCustomPrice2(ByVal objProduct As CCartItem) As Decimal
        Dim custPrice As Decimal
        '  Dim tempCartItem As New BusinessRule.CCartItem(Me.m_objXMLAccess.GetProduct(Id), 1, , Me.m_objCustomer.CustomerGroup)
        m_bIsSalePrice = False
        m_bIsCustomPrice = False
        custPrice = objProduct.CustomerSpecificPrice
        If objProduct.IsOnSale Then
            If custPrice < objProduct.SalePrice Then
                m_bIsCustomPrice = True
                Return custPrice
            Else
                m_bIsSalePrice = True
                Return objProduct.SalePrice 'PriceDisplay2(tempCartItem.SalePrice)
            End If
        ElseIf custPrice < objProduct.Price Then
            m_bIsCustomPrice = True
            Return custPrice
        Else
            Return 0.0
        End If
    End Function

    Private Function GetCustomPrice2(ByVal Id As Long) As Decimal
        Dim custPrice As Decimal
        '  Dim tempCartItem As New BusinessRule.CCartItem(Me.m_objXMLAccess.GetProduct(Id), 1, , Me.m_objCustomer.CustomerGroup)
        m_bIsSalePrice = False
        m_bIsCustomPrice = False
        custPrice = Product.CustomerSpecificPrice
        If Product.IsOnSale Then
            If custPrice < Product.SalePrice Then
                m_bIsCustomPrice = True
                Return custPrice
            Else
                m_bIsSalePrice = True
                Return Product.SalePrice 'PriceDisplay2(tempCartItem.SalePrice)
            End If
        ElseIf custPrice < Product.Price Then
            m_bIsCustomPrice = True
            Return custPrice
        Else
            Return 0.0
        End If
    End Function
#End Region

#Region "Function SetCustomPriceValue2(ByVal Id As Long) As String"
    Private Function SetCustomPriceValue2(ByVal Id As Long) As String
        Dim CustPrice As Decimal
        CustPrice = GetCustomPrice2(Id)
        m_bIsSalePrice = False
        m_bIsCustomPrice = False
        If m_objProduct.IsOnSale Then
            If CustPrice >= m_objProduct.SalePrice Then
                m_bIsSalePrice = True
                Return " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(m_objProduct.SalePrice)
            Else
                m_bIsCustomPrice = True
                Return " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(CustPrice)
            End If
        ElseIf CustPrice < m_objProduct.Price And CustPrice <> 0 Then
            m_bIsCustomPrice = True
            Return " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(CustPrice)
        End If
        Return ""
    End Function
#End Region

#Region "Function SetCustomPriceValue(ByVal Id As Long) As String"
    Private Function SetCustomPriceValue(ByVal Id As Long) As String
        Dim CustPrice As Decimal
        CustPrice = GetCustomPrice2(Id)
        If m_objProduct.IsOnSale Then
            If CustPrice >= m_objProduct.SalePrice Then
                Return StoreFrontConfiguration.Labels.Item("lblSalePrice").InnerText().Trim() & " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(m_objProduct.SalePrice)
            Else
                Return lblYourPrice.InnerHtml & " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(CustPrice)
            End If
        ElseIf CustPrice < m_objProduct.Price And CustPrice <> 0 Then
            Return lblYourPrice.InnerHtml & " <s>" & PriceDisplay2(m_objProduct.Price) & "</s>&nbsp;" & PriceDisplay2(CustPrice)
        End If
        Return ""
    End Function
#End Region

#Region "Function SetSavingsValue(ByVal Id As Long) As String"
    Private Function SetSavingsValue(ByVal Id As Long) As String
        Dim savings As Decimal
        savings = GetSavings(Id)
        Return PriceDisplay2(savings)
    End Function
#End Region
#End Region

#Region "Public Sub SetStockDisplay(ByVal StockInfo As System.Web.UI.WebControls.LinkButton)"

    Public Sub SetStockDisplay(ByVal StockInfo As System.Web.UI.WebControls.LinkButton)
        Try
            If Product.Inventory.InventoryTracked = True Then
                If Product.Inventory.ShowStatus = True Then
                    StockInfo.Visible = True
                    If Product.Inventory.StockIsDepleted Then
                        StockInfo.Text = "Out of Stock!"
                    Else
                        StockInfo.Text = "In Stock!"
                    End If
                Else
                    StockInfo.Visible = False
                    StockInfo.Visible = False
                End If
            Else
                trStockInfo.Visible = False
                StockInfo.Visible = False
            End If
        Catch
            trStockInfo.Visible = False
            StockInfo.Visible = False
        End Try
    End Sub

#End Region

#Region "Private Function arRelatedProd() As ArrayList"

    Private Function arRelatedProd() As ArrayList
        Dim ar As New ArrayList()
        Dim objProduct As CProduct
        Dim nID As Long
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
            For Each nID In Product.RelatedProducts
                Try
                    objProduct = New CProduct(m_objXMLaccess.GetProduct(nID))
                    ar.Add(New CCartItem(objProduct, 1, Nothing, m_objCustomer.CustomerGroup))
                Catch objErr As Exception
                    ' Related Product not active
                End Try
            Next
        Else
            Dim oprodManagement As New Management.CProductManagement()
            Dim ds As dsProducts
            ds = oprodManagement.RelatedItemsDisplay(Product.RelatedProducts, m_objCustomer.CustomerGroup)
            Dim dr As dsProducts.ProductsRow
            For Each dr In ds.Products
                ar.Add(New CCartItem(dr, 1, Nothing, m_objCustomer.CustomerGroup))
                'ar.Add(New CProduct(dr, 1))
            Next
            ds = Nothing
            oprodManagement = Nothing
        End If
        Return ar
        ar.Clear()
        ar = Nothing
    End Function

#End Region

#Region "Public Sub RelatedProducts(ByVal DtTemplate As DetailTemplate, ByVal Repeater1 As Repeater, Optional ByVal SeperatorLine As System.Web.UI.HtmlControls.HtmlTable = Nothing)"

    Public Sub RelatedProducts(ByVal DtTemplate As DetailTemplate, ByVal Repeater1 As Repeater, Optional ByVal SeperatorLine As System.Web.UI.HtmlControls.HtmlTable = Nothing)
        If (Product.RelatedProducts.Count > 0 And m_bDisplayRecommendedProducts) Then
            Dim objProduct As CProduct
            Dim ar As ArrayList
            ar = arRelatedProd()
            If (ar.Count = 0) Then
                If DtTemplate = DetailTemplate.Template2 Then
                    SeperatorLine.Visible = False
                End If
                Repeater1.Visible = False
            Else
                Repeater1.DataSource = ar
                Repeater1.DataBind()
            End If
            ar.Clear()
            ar = Nothing
        Else
            If DtTemplate = DetailTemplate.Template2 Then
                SeperatorLine.Visible = False
            End If
            Repeater1.Visible = False
        End If
    End Sub

#End Region

#Region " Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objbutton As LinkButton = sender
        Dim bSeen As Boolean = False
        If IsNothing(Product) Then
            Product = Session("dProduct")
        End If
        bSeen = CBool("0" & CStr(ViewState.Item("IV")))
        If bSeen = True Then
            CInventoryControl1.Visible = False
            ViewState.Item("IV") = "0"
        Else
            CInventoryControl1.Visible = True
            CInventoryControl1.ProductID = Product.ProductID
            ViewState.Item("IV") = "1"
        End If
    End Sub

#End Region

#Region "Public Sub AddErrorMessage(ByVal strMessage As String)"

    Public Sub AddErrorMessage(ByVal strMessage As String)
        RaiseEvent AddError(strMessage, EventArgs.Empty)


    End Sub

#End Region


End Class
