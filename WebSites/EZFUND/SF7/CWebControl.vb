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

Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Xml

Imports System.Math

Imports System.Text
Imports System.Net
Imports System.IO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.UITools
Imports StoreFront.StoreFront.FrameworkExceptions
Imports StoreFront.BusinessRule.Management

Public Class CWebControl
    Inherits UserControl

    Protected WithEvents SimpleSearch1 As SimpleSearch
    Protected m_objCustomer As CCustomer
    Protected m_objXMLCart As CCart
    Protected m_objXMLAccess As CXMLProductAccess
    Protected m_arEMailContent As ArrayList
    'Protected m_XMLMessages As CXMLMessages
    Protected m_objInstructions As CXMLInstructions
    Protected m_objMessages As CXMLMessages
    Protected m_objCartList As CartList
    Protected dom As XmlDocument
    Protected m_Affiliate As CAffiliate

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dom = StoreFrontConfiguration.XMLDocument
        'm_XMLMessages = StoreFrontConfiguration.XMLMessages
        m_objInstructions = StoreFrontConfiguration.InstructionsAccess

        m_objXMLCart = Session("XMLShoppingCart")
        m_objCustomer = Session("Customer")
        m_objXMLAccess = StoreFrontConfiguration.ProductAccess
        m_arEMailContent = Session("EMailContent")
        m_objMessages = StoreFrontConfiguration.MessagesAccess
        m_Affiliate = Session("Affiliate")

    End Sub

    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Trace.IsEnabled = True
        Dim objError As New CStoreFrontWebError(Err)
        objError.TrackInfo(Me.GetType().ToString(), "Page_Error")
    End Sub

    Public Property CartListControl() As CartList
        Get
            Return m_objCartList
        End Get
        Set(ByVal Value As CartList)
            m_objCartList = Value
        End Set
    End Property

#Region "Function CurrentWebPage"
    Public Function CurrentWebPage() As String
        Dim sTemp As String
        sTemp = Request.Url.ToString
        If InStr(sTemp, "?") > 0 Then
            Return sTemp.Substring(0, InStr(sTemp, "?") - 1)
        Else
            Return sTemp
        End If

    End Function
#End Region

#Region "UpdateXMLQuantity"
    Public Overridable Sub UpdateXMLQuantity(ByVal Cart As DynamicCartDisplay, ByVal Total As Label)
        'Dim i As Integer
        'Dim objItem As CCartItem 'CXMLShoppingCartItem
        Dim obj As CCartItem
        Dim objText As TextBox
        Dim objLabel As Label = Nothing
        Dim ar As New ArrayList
        Dim lngID As Long = 1
        Dim objTemp As CCartItem
        Dim arOverInventory As New ArrayList
        Dim objBizBase As New CBusinessBase

        Cart.DataSource = m_objXMLCart.GetCartItems
        Cart.DataBind()

        For Each obj In m_objXMLCart.GetCartItems
            objText = CType(Cart.FindControl("Qty" & lngID), TextBox)
            If (CLng(objText.Text) = 0) Then
                ar.Add(obj)
            ElseIf (CLng(objText.Text) > 0) Then

                If obj.Inventory.InventoryTracked Then
                    If obj.Inventory.ItemsAreStocked(obj.Attributes, CLng(objText.Text)) Then
                        obj.Quantity = CLng(objText.Text)
                        objBizBase.UpdateQty(obj, Cart, objLabel, objText, lngID)
                        m_objXMLCart.UpdateItem(obj, lngID)
                    Else
                        objTemp = New CCartItem(obj, CLng(objText.Text), obj.Attributes, obj.CustomerGroup)
                        ' only raise an event if quantity is being increased
                        If CLng(objText.Text) > obj.Quantity Then
                            objTemp.Quantity = CLng(objText.Text) - obj.Quantity
                            arOverInventory.Add(objTemp)
                        ElseIf CLng(objText.Text) < obj.Quantity Then
                            obj.Quantity = CLng(objText.Text)
                            objBizBase.UpdateQty(obj, Cart, objLabel, objText, lngID)
                            m_objXMLCart.UpdateItem(obj, lngID)
                        End If
                    End If
                Else
                    obj.Quantity = CLng(objText.Text)
                    objBizBase.UpdateQty(obj, Cart, objLabel, objText, lngID)
                    m_objXMLCart.UpdateItem(obj, lngID)
                End If
            End If
            lngID = lngID + 1
        Next


        lngID = 1
        For Each obj In ar
            m_objXMLCart.DeleteItem(obj, lngID)
            lngID = lngID + 1
        Next

        If arOverInventory.Count > 0 Then
            m_objXMLCart.UpdateItems(arOverInventory)
        End If

        If (m_objXMLCart.GetCartItems.Count = 0) Then
            Cart.Visible = False
        ElseIf (ar.Count > 0) Then
            Cart.NotPostBack = True
            Cart.DataSource = m_objXMLCart.GetCartItems
            Cart.DataBind()
        End If

    End Sub
#End Region

    Public Function PriceDisplay(ByVal dPrice As Decimal) As String
        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return "<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>"
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)"
            End If
        Else
            Return Format(dPrice, "c")
        End If
    End Function

    Public Function PriceDisplay2(ByVal dPrice As Decimal, Optional ByVal prod As CCartItem = Nothing) As String
        'Tee 8/7/2007 product configurator
        If Not IsNothing(prod) Then
            If (prod.ProductType = ProductType.Customized OrElse _
            prod.ProductType = ProductType.CustomizedSubscription) AndAlso prod.ProductID > 0 Then
                Return (New CProductManagement(prod.ProductID)).PriceRange
            End If
        End If
        'end Tee
        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return "<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>"
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)"
            End If
        Else
            Return Format(dPrice, "c")
        End If

    End Function

    Public Function PriceDisplay3(ByVal dPrice As Decimal) As String

        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return Format(dPrice, "c")
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " " & Session("ConvertISO") & ")"
            End If
        Else
            Return Format(dPrice, "c")
        End If

    End Function

    Private Sub SimpleSearch1_SimpleSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SimpleSearch1.SimpleSearch_Click
        Response.Redirect(sender)
        Response.End()
    End Sub

    Protected Sub LabelText(ByVal objContainer As Object)
        Dim objLabel As Label
        Dim arLabels As New ArrayList
        Dim strID As String

        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblPrice"))
        arLabels.Add(New String("lblVolumePrice"))
        arLabels.Add(New String("lblStockInfo"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))
        'Verisign Recurring Billing
        arLabels.Add(New String("lblSubscriptionPrice"))
        arLabels.Add(New String("lblRecurringPrice"))
        'Verisign Recurring Billing

        For Each strID In arLabels
            objLabel = objContainer.FindControl(strID)
            If (IsNothing(objLabel) = False) Then
                If strID <> "lblMoreInfo" Then
                    If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                        objLabel.Text = ""
                    Else
                        objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                    End If
                Else
                    objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText()
                End If
            End If
            objLabel = objContainer.FindControl(strID & "2")
            If (IsNothing(objLabel) = False) Then
                If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                    objLabel.Text = ""
                Else
                    objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                End If
            End If
        Next
    End Sub

    Protected Sub SetLabelVisible(ByVal objContainer As Object, ByVal bVisible As Boolean)
        Dim objLabel As Label
        Dim arLabels As New ArrayList
        Dim strID As String

        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblPrice"))
        arLabels.Add(New String("lblVolumePrice"))
        arLabels.Add(New String("lblStockInfo"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        'arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))
        'Verisign Recurring Billing
        arLabels.Add(New String("lblSubscriptionPrice"))
        arLabels.Add(New String("lblRecurringPrice"))
        'Verisign Recurring Billing

        For Each strID In arLabels
            objLabel = objContainer.FindControl(strID)
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
            objLabel = objContainer.FindControl(strID & "2")
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
        Next
    End Sub

    'Verisign Recurring Billing
#Region "Public Sub GetPaymentPeriodName(ByVal period As PaymentPeriod)"
    Public Function GetPaymentPeriodName(ByVal period As PaymentPeriod) As String
        Return ProductHelperModule.GetPaymentPeriodName(period)
    End Function
#End Region
    'Verisign Recurring Billing
#Region "Protected Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "")"
    Protected Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "", Optional ByVal condition As String = "")
        If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
            Dim script As New StringBuilder
            script.Append("if (isEnterKey(event)")
            If Not IsNothing(condition) AndAlso condition <> String.Empty Then
                script.Append(" && " & condition)
            End If
            script.Append(") ")
            script.Append("postBack(event, '" & objButton.UniqueID & "', '" & argument & "')")
            objTextControl.Attributes.Add("onKeyDown", script.ToString)
        End If
    End Sub
#End Region

    'Tee 8/7/2007 product configurator
#Region "Public Sub MakeCommonVisible(ByVal prodType As ProductType, ByVal addExists As Boolean)"
    Public Sub MakeCommonVisible(ByVal prodType As ProductType, ByVal addExists As Boolean)
        If prodType = ProductType.Normal OrElse prodType = ProductType.Subscription Then
            CType(Me.Parent.FindControl("tcEditInventory"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditAttributes"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditFulfillment"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditMerchantBundleComponents"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustBundleComponents"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustDefinedRules"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditInventorySP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditAttributesSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditImageControl"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditImageControlSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditFulfillmentSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditMerchantSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustBundleSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustDefinedSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditMarketing"), HtmlTableCell).ColSpan = 7
            If Not addExists Then
                Exit Sub
            End If
            CType(Me.Parent.FindControl("tcAddInventory"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddAttributes"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddFulfillment"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddBundleComponents"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddCustDefinedRules"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddInventorySP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddAttributesSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddFulfillmentSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddBundleSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddCustDefinedSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddMarketing"), HtmlTableCell).ColSpan = 7
        ElseIf prodType = ProductType.Bundle OrElse prodType = ProductType.BundleSubscription Then
            CType(Me.Parent.FindControl("tcEditInventory"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditAttributes"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditFulfillment"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditMerchantBundleComponents"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditmerchantBundleComponents"), HtmlTableCell).ColSpan = 13
            CType(Me.Parent.FindControl("tcEditCustBundleComponents"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustDefinedRules"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditInventorySP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditAttributesSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditImageControl"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditImageControlSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditFulfillmentSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditMerchantSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditCustBundleSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustDefinedSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditAttributes"), HtmlTableCell).Visible = False
            If Not addExists Then
                Exit Sub
            End If
            CType(Me.Parent.FindControl("tcAddInventory"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddAttributes"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddFulfillment"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddBundleComponents"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddBundleComponents"), HtmlTableCell).ColSpan = 13
            CType(Me.Parent.FindControl("tcAddCustDefinedRules"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddInventorySP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddAttributesSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddImageControl"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddImageControlSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddFulfillmentSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddBundleSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddCustDefinedSP"), HtmlTableCell).Visible = False
        ElseIf prodType = ProductType.Customized OrElse prodType = ProductType.CustomizedSubscription Then
            CType(Me.Parent.FindControl("tcEditInventory"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditAttributes"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditFulfillment"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditMerchantBundleComponents"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustBundleComponents"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditCustBundleComponents"), HtmlTableCell).ColSpan = 11
            CType(Me.Parent.FindControl("tcEditCustDefinedRules"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditInventorySP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditAttributesSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditImageControl"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditImageControlSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditFulfillmentSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditMerchantSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcEditCustBundleSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcEditCustDefinedSP"), HtmlTableCell).Visible = True
            If Not addExists Then
                Exit Sub
            End If
            CType(Me.Parent.FindControl("tcAddInventory"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddAttributes"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddFulfillment"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddBundleComponents"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddBundleComponents"), HtmlTableCell).ColSpan = 11
            CType(Me.Parent.FindControl("tcAddCustDefinedRules"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddInventorySP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddAttributesSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddImageControl"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddImageControlSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddFulfillmentSP"), HtmlTableCell).Visible = False
            CType(Me.Parent.FindControl("tcAddBundleSP"), HtmlTableCell).Visible = True
            CType(Me.Parent.FindControl("tcAddCustDefinedSP"), HtmlTableCell).Visible = True
        End If
    End Sub
#End Region
    'end Tee

    Function RestrictedPages(ByVal DisplayName As String) As Boolean
        If Not IsNothing(Session("Admin")) Then
            Dim taskNo As Int16 = 0
            'left col nav link
            Select Case DisplayName.ToLower
                Case "sales reports"
                    taskNo = Tasks.SalesReports
                Case "orders"
                    taskNo = Tasks.Orders
                Case "affiliates"
                    taskNo = Tasks.Affiliates
                Case "customers"
                    taskNo = Tasks.Customers
                Case "price groups"
                    taskNo = Tasks.PriceGroups
                Case "gift certificates"
                    taskNo = Tasks.GiftCertificates
                Case "manage csr options"
                    taskNo = Tasks.ManageCSROptions
                Case "manage roles"
                    taskNo = Tasks.ManageRoles
                Case "manage administrators"
                    taskNo = Tasks.ManageAdministrators
                Case "storewide discounts"
                    taskNo = Tasks.StorewideDiscounts
                Case "coupons"
                    taskNo = Tasks.Coupons
                Case "promotional mail"
                    taskNo = Tasks.PromotionalMail
                Case "search engines"
                    taskNo = Tasks.SearchEngines
                Case "marketplaces"
                    taskNo = Tasks.Marketplaces
                Case "import products"
                    taskNo = Tasks.ImportProducts
                Case "attributes"
                    taskNo = Tasks.Attributes
                Case "categories"
                    taskNo = Tasks.categories

                Case "manufacturers"
                    taskNo = Tasks.Manufacturers
                Case "vendors"
                    taskNo = Tasks.Vendors
                Case "general"
                    taskNo = Tasks.storeSettingGeneral
                Case "online chat"
                    taskNo = Tasks.OnlineChat
                Case "e-mail"
                    taskNo = Tasks.EMail
                Case "shipping"
                    taskNo = Tasks.Shipping
                Case "payments"
                    taskNo = Tasks.Payments
                Case "localization"
                    taskNo = Tasks.Localization
                Case "tax"
                    taskNo = Tasks.Tax
                    'manage products links
                Case "details"
                    taskNo = Tasks.ProductDetails
                Case "general"
                    taskNo = Tasks.ProductGeneral
                Case "productcategories"
                    taskNo = Tasks.ProductCategories
                Case "productattributes"
                    taskNo = Tasks.ProductAttributes
                Case "productimages"
                    taskNo = Tasks.ProductImages
                Case "productfulfillment"
                    taskNo = Tasks.Fulfillment
                Case "storefront connector"
                    taskNo = Tasks.WebServices

                Case "inventory"
                    taskNo = Tasks.Inventory
                Case "discounts"
                    taskNo = Tasks.Discounts
                Case "marketing"
                    taskNo = Tasks.Marketing
                    'site design
                Case "site setup"
                    taskNo = Tasks.SiteSetup
                Case "layout templates"
                    taskNo = Tasks.LayoutTemplates
                Case "themes"
                    taskNo = Tasks.themes
                Case "search result filters"
                    taskNo = Tasks.SearchResultFilters
                Case "custom pages"
                    taskNo = Tasks.CustomPages
                Case "mapped urls"
                    taskNo = Tasks.MappedURLs
            End Select
            If DisplayName.ToLower = "products" OrElse DisplayName.ToLower = "merchant bundles" OrElse DisplayName.ToLower = "customer defined bundles" Then
                Return Me.RestrictedProductPages()
            ElseIf DisplayName.ToLower = "payments" Then
                Return Me.RestrictedPaymentPages()
            Else
                Return Me.RestrictedPages(taskNo)
            End If
        End If
        Return True
    End Function

    Function RestrictedPages(ByVal task As Integer) As Boolean
        If Not IsNothing(Session("Admin")) Then
            Dim objUserBase As New AdminUserBase
            objUserBase = Session("Admin")
            ' begin: JDB - 8/7/2007 - MT Admin User Management
            ' note: build in admin account
            If objUserBase.IsSuperUser OrElse objUserBase.Role.IsSuper Then
                Return False
            Else
                For Each objTasks As RoleTasks In objUserBase.Role.Tasks
                    If objTasks.TaskID = task Then
                        Return False
                    End If
                Next
            End If
            ' end: JDB - 8/7/2007 - MT Admin User Management
        End If
        Return True
    End Function

    Function RestrictedProductPages() As Boolean
        If Not Me.RestrictedPages(Tasks.ProductAddnew) _
        OrElse Not Me.RestrictedPages(Tasks.ProductGeneral) _
        OrElse Not Me.RestrictedPages(Tasks.ProductDetails) _
        OrElse Not Me.RestrictedPages(Tasks.ProductCategories) _
        OrElse Not Me.RestrictedPages(Tasks.ProductAttributes) _
        OrElse Not Me.RestrictedPages(Tasks.ProductImages) _
        OrElse Not Me.RestrictedPages(Tasks.Fulfillment) _
        OrElse Not Me.RestrictedPages(Tasks.Inventory) _
        OrElse Not Me.RestrictedPages(Tasks.Discounts) _
        OrElse Not Me.RestrictedPages(Tasks.Marketing) _
        OrElse Not Me.RestrictedPages(Tasks.BundleComponents) _
        OrElse Not Me.RestrictedPages(Tasks.CustomerDefinedRules) Then
            Return False
        Else
            Return True
        End If
    End Function

    Function RestrictedPaymentPages() As Boolean
        If Not Me.RestrictedPages(Tasks.PaymentMethods) _
        OrElse Not Me.RestrictedPages(Tasks.OnlineProcessing) _
        OrElse Not Me.RestrictedPages(Tasks.Encryption) _
        OrElse Not Me.RestrictedPages(Tasks.PayPal) Then
            Return False
        Else
            Return True
        End If
    End Function

End Class

