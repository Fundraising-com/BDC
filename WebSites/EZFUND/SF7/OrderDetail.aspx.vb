'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Text
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class OrderDetail
    Inherits CWebPage

#Region "Class Members"
    Protected BillingAddress As AddressLabel
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents DynamicCartDisplay2 As UITools.DynamicCartDisplay
    Private m_objOrderHistory As BusinessRule.Orders.COrders
    Private m_objOrder As BusinessRule.Orders.COrder
    Private m_objPaymentMethodAccess As CXMLPaymentMethodAccess
    Private m_objShipMethodAccess As CXMLShipMethodAccess
    Private m_OrderId As Long
    'Private m_OrderItem As COrderItem




#End Region

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        SetPageTitle = m_objMessages.GetXMLMessage("OrderDetail.aspx", "PageTitle", "Title")
        SetDesign(PageTable, PageSubTable, PageCell)


        If (m_objCustomer.IsSignedIn = False) Then
            Dim strQueryString As String = "?"
            Dim str As String
            For Each str In Request.QueryString
                If (strQueryString.Length > 1) Then
                    strQueryString = strQueryString & "&" & str & "=" & Request.QueryString(str)
                Else
                    strQueryString = strQueryString & str & "=" & Request.QueryString(str)
                End If
            Next
            strQueryString = HttpUtility.UrlEncode(strQueryString)
            Response.Redirect("CustSignIn.aspx?ReturnPage=OrderDetail.aspx" & strQueryString)
        End If


        If IsNothing(Session("OrderHistory")) = True OrElse Not IsNothing(Request.QueryString("WebID")) Then
            Session("OrderHistory") = New BusinessRule.Orders.COrders
            Session("OrderHistory").LoadOrderHistory(m_objCustomer.GetCustomerID(), False)
        End If


        TotalDisplay1.OandaISO = Session("ConvertISO")
        TotalDisplay1.OandaRate = CDec(Session("OandaRate"))
        m_objOrderHistory = Session("OrderHistory")
        m_OrderId = Request.QueryString("OrderID")
        lblOrderID.Text = ""
        Dim i As Integer

        For i = 0 To m_objOrderHistory.Orders.Count - 1
            If (m_objOrderHistory.Orders(i).UID = Request.QueryString("OrderID")) Then
                m_objOrder = m_objOrderHistory.Orders(i)
                If m_objOrder.OrderAddresses.Count = 0 Then
                    ' only load if not loaded
                    m_objOrder.LoadOrderAddresses()
                End If
                lblOrderID.Text = m_objOrder.OrderNumber
                lblOrderDate.Text = m_objOrder.OrderDate
                Exit For
            End If
        Next

        If lblOrderID.Text.Trim = "" Then
            Session("OrderLoginMessage") = "This order is unavailable to this account.  Please log in again using a different account."
            Dim strQueryString As String = "?"
            Dim str As String
            For Each str In Request.QueryString
                If (strQueryString.Length > 1) Then
                    strQueryString = strQueryString & "&" & str & "=" & Request.QueryString(str)
                Else
                    strQueryString = strQueryString & str & "=" & Request.QueryString(str)
                End If
            Next
            strQueryString = HttpUtility.UrlEncode(strQueryString)
            Response.Redirect("CustSignIn.aspx?ReturnPage=OrderDetail.aspx" & strQueryString)
        End If

        If Not Page.IsPostBack Then
            BindDataSources()
        End If

        Dim con As DataListItem
        For Each con In Datalist2.Items
            CType(con.FindControl("imgTrack"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Track").Attributes("Filepath").Value()
        Next

        InventoryBackOrderConfirm(CType(FindControl("myhiddenfield"), HtmlInputHidden).Value)
    End Sub

#End Region

    Private Sub BindDataSources()
        BillingAddress.AddressSource = m_objOrder.BillAddress
        lblEmail.Text = m_objOrder.BillAddress.EMail
        m_objPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
        lblPaymentMethod.Text = m_objPaymentMethodAccess.GetPaymentMethodName(m_objOrder.PaymentMethod())
        If (m_objOrder.OrderAddresses.Count = 1) Then
            TotalDisplay1.DisplayMerchandiseTotal = True
            TotalDisplay1.DisplayDiscountTotal = True
            TotalDisplay1.DisplaySubTotal = True
            TotalDisplay1.DisplayLocalTaxTotal = True
            TotalDisplay1.DisplayStateTaxTotal = True
            TotalDisplay1.DisplayCountryTaxTotal = True
            TotalDisplay1.DisplayShippingTotal = True
            TotalDisplay1.DisplayHandlingTotal = True
            TotalDisplay1.DisplayOrderTotal = False
        Else
            TotalDisplay1.DisplayMerchandiseTotal = False
            TotalDisplay1.DisplayDiscountTotal = False
            TotalDisplay1.DisplaySubTotal = False
            TotalDisplay1.DisplayLocalTaxTotal = False
            TotalDisplay1.DisplayStateTaxTotal = False
            TotalDisplay1.DisplayCountryTaxTotal = False
            TotalDisplay1.DisplayShippingTotal = False
            TotalDisplay1.DisplayHandlingTotal = True
            TotalDisplay1.DisplayOrderTotal = False
        End If
        Datalist2.DataSource = m_objOrder.OrderAddresses
        Datalist2.DataBind()
        ' begin: JDB - Advanced Coupon Functionality
        Me.rDiscounts.DataSource = Me.m_objOrder.GetOrderDiscounts()
        Me.rDiscounts.DataBind()
        ' end: JDB - Advanced Coupon Functionality
        TotalDisplay1.DataSource = m_objOrder
        TotalDisplay1.DataBind()
    End Sub

    ' begin: JDB - Advanced Coupon Functionality
    Protected DiscountTotal As Decimal = 0
    Protected Function HandleDiscountAmount(ByVal dDiscountAmount As Decimal) As Decimal
        Me.DiscountTotal += dDiscountAmount
        Return dDiscountAmount
    End Function
    ' end: JDB - Advanced Coupon Functionality

    Private Sub OnReorder(ByVal mOrderItemId As Long)
        Dim objAddress As COrderAddress
        Dim myOrderItem As COrderItem
        For Each objAddress In m_objOrder.OrderAddresses
            For Each myOrderItem In objAddress.OrderItems
                If myOrderItem.OrderItemID = mOrderItemId Then
                    ReOrder(myOrderItem)
                    BindDataSources()
                    Dim con2 As DataListItem
                    For Each con2 In Datalist2.Items
                        CType(con2.FindControl("imgTrack"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Track").Attributes("Filepath").Value()
                    Next
                    Exit Sub
                End If
            Next
        Next

    End Sub

#Region "Public ReadOnly Property OrderID() As Long"

    Public ReadOnly Property OrderID() As Long
        Get
            Return m_objOrder.UID()
        End Get
    End Property

#End Region

#Region "Public ReadOnly Property DownloadDate() As Date"

    Public ReadOnly Property DownloadDate() As Date
        Get
            If (m_objOrder.DownloadDate = "") Then
                Return Nothing
            Else
                Return CDate(m_objOrder.DownloadDate())
            End If
        End Get
    End Property

#End Region

#Region "Private Sub Datalist2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Datalist2.ItemCreated"

    Private Sub Datalist2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Datalist2.ItemCreated

    End Sub

#End Region

#Region "Public Sub BtnTrackClick(ByVal source As Object, ByVal e As System.EventArgs)"

    Public Sub BtnTrackClick(ByVal source As Object, ByVal e As System.EventArgs)
        Response.Redirect("OrderTracking.aspx?" & source.CommandArgument)
    End Sub

#End Region

#Region "Private Sub ReOrder()"
    Private Sub ReOrder(ByVal myOrderItem As COrderItem)
        Dim objButton As New LinkButton
        Session("OrderAttributes") = Nothing
        If VerifyAttributes(myOrderItem) Then
            'Tee 8/30/2007 product configurator
            Session("BundledProducts") = myOrderItem.BundledProducts
            'end Tee
            objButton.CommandName = myOrderItem.ProductID
            objButton.CommandArgument = myOrderItem.Quantity
            Me.AddItemToCart(objButton, EventArgs.Empty)
        Else
            Me.ErrorMessage.Text = "Product no longer exist or does not have same attributes"
            Me.ErrorMessage.Visible = True
        End If
    End Sub
#End Region

#Region "Private Function VerifyAttributes() As Boolean"

    Private Function VerifyAttributes(ByVal myOrderItem As COrderItem) As Boolean
        'Tee 8/30/2007 product configurator
        If myOrderItem.ProductType <> ProductType.Normal AndAlso myOrderItem.ProductType <> ProductType.Subscription Then
            For Each item As CProduct In myOrderItem.BundledProducts
                If Not (New Management.CProductManagement).IsActive(item.ProductID) Then
                    Return False
                End If
            Next
        End If
        'end Tee
        Dim cOrderAtts As CAttributesSelected
        Dim oAtt As CAttribute
        Dim oAttDetail As CAttributeDetail
        Dim ar As New ArrayList
        Dim objCurrentAtts As CAttributes
        Dim oCartItem As CCartItem
        If StoreFrontConfiguration.ProductLoading = StoreFrontConfiguration.LoadType.XML Then
            Dim objNode As System.Xml.XmlNode = m_objXMLAccess.GetProduct(myOrderItem.ProductID)
            If (IsNothing(objNode) = True) Then
                Return False
            End If
            objCurrentAtts = New CAttributes(objNode, True)
        Else
            Dim oprodManagement As New Management.CProductManagement
            Dim ds As dsProducts
            ds = oprodManagement.GetProductRow(myOrderItem.ProductID, m_objCustomer.CustomerGroup)
            If ds.Products.Rows.Count > 0 Then
                Dim drProd As dsProducts.ProductsRow = ds.Products.Rows(0)
                oCartItem = New CCartItem(drProd, CLng(myOrderItem.ProductID), , m_objCustomer.CustomerGroup, , myOrderItem.BundledProducts)
                objCurrentAtts = oCartItem.Attributes

                'Begin Mod 7/06 - Junu - sp8-298
                'If oCartItem.isActive = False Then
                '    Return False
                'End If
                'end mod 7/06
                oprodManagement = Nothing
                oCartItem = Nothing
            Else
                Return False
            End If
        End If
        If objCurrentAtts.Count = myOrderItem.Attributes.Count Then
            For Each oAtt In myOrderItem.Attributes
                cOrderAtts = New CAttributesSelected
                If MainAttribute(objCurrentAtts, oAtt) Then
                    cOrderAtts.AttributeId = oAtt.UID
                    For Each oAttDetail In oAtt.AttributeDetails
                        If DetailAttribute(objCurrentAtts, oAtt, oAttDetail) Then
                            cOrderAtts.UID = oAttDetail.UID
                            cOrderAtts.Customor_Custom_Description = oAttDetail.Customor_Custom_Description
                            Exit For
                        Else
                            Return False
                            Exit Function
                        End If
                    Next
                Else
                    Return False
                    Exit Function
                End If
                ar.Add(cOrderAtts)
            Next
        Else
            'product dynamics has  changed
            Return False
        End If
        Session("OrderAttributes") = ar
        Return True
    End Function

#End Region

#Region "Private Function MainAttribute(ByVal objCurrentAtts As CAttributes, ByVal objAtt As CAttribute) As Boolean"

    Private Function MainAttribute(ByVal objCurrentAtts As CAttributes, ByVal objAtt As CAttribute) As Boolean
        Dim oAtt As CAttribute
        For Each oAtt In objCurrentAtts
            If objAtt.Name = oAtt.Name Then
                If objAtt.AttributeType = oAtt.AttributeType Then
                    'same attribute so set uid  same(case attributes have been modified)
                    objAtt.UID = oAtt.UID
                    Return True
                    Exit For
                End If
            End If
        Next
        Return False
    End Function

#End Region

#Region "Private Function DetailAttribute(ByVal objCurrentAtts As CAttributes, ByVal objCurrentAtt As CAttribute, ByVal objAttDetail As CAttributeDetail) As Boolean"

    Private Function DetailAttribute(ByVal objCurrentAtts As CAttributes, ByVal objCurrentAtt As CAttribute, ByVal objAttDetail As CAttributeDetail) As Boolean
        Dim oAttDetail As CAttributeDetail
        Dim oAtt As CAttribute = Nothing
        For Each oAtt In objCurrentAtts
            If oAtt.Name = objCurrentAtt.Name Then
                Exit For
            End If
        Next
        For Each oAttDetail In oAtt.AttributeDetails
            If oAttDetail.Name = objAttDetail.Name Then
                'same attribute so set uid  same(case attributes have been modified)
                objAttDetail.UID = oAttDetail.UID
                Return True
                Exit For
            ElseIf oAtt.AttributeType = tAttributeType.Custom Then
                If objAttDetail.Name = oAtt.Name Then
                    objAttDetail.UID = oAttDetail.UID
                    Return True
                    Exit For
                End If
            End If
        Next
        Return False
    End Function

#End Region

#Region "Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR"

    Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR
        Me.ErrorMessage.Text = sender.ToString
        Me.ErrorMessage.Visible = True
    End Sub

#End Region

#Region "Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded"

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
        Else
            Message.Text = ""
            Message.Visible = False
        End If
    End Sub



#End Region

    Private Sub Datalist2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles Datalist2.ItemCommand
        If TypeOf e.CommandSource Is DynamicCartDisplay AndAlso e.CommandName.ToLower = "reset" Then
            OnReorder(CLng(e.CommandArgument))
        End If
    End Sub

    Private Sub Datalist2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Datalist2.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            If (m_objOrder.OrderAddresses.Count = 1) Then
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).Visible = False
            Else
                If (IsNothing(e.Item.DataItem) = False) Then
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataSource = e.Item.DataItem
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataBind()
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaISO = Session("ConvertISO")
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaRate = CDec(Session("OandaRate"))
                End If
            End If
            If (IsNothing(e.Item.DataItem) = False) Then
                If (IsNothing(m_objShipMethodAccess) = True) Then
                    m_objShipMethodAccess = StoreFrontConfiguration.ShipMethodAccess
                End If
                CType(e.Item.FindControl("ShipMethod"), Label).Text = m_objShipMethodAccess.GetShipMethodName(CType(e.Item.DataItem, COrderAddress).Address.ShipMethod())
            End If
        End If
        If (IsNothing(e.Item.DataItem) = False) Then
            If (Not (IsNothing(e.Item.FindControl("DynamicCartDisplay2")))) Then
                DynamicCartDisplay2 = CType(e.Item.FindControl("DynamicCartDisplay2"), DynamicCartDisplay)
                DynamicCartDisplay2.ReOrderImg = dom.Item("SiteProducts").Item("SiteImages").Item("ReOrder").Attributes("Filepath").Value
            End If
        End If
    End Sub
End Class
