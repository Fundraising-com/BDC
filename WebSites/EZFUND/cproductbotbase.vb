'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Xml
Imports StoreFront.BusinessRule

Imports StoreFront.SystemBase


Public Class CProductBotBase
    Inherits CWebPage

#Region "Class Members"

	 Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ProductImage As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProductName As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblVendor As System.Web.UI.WebControls.Label
    Protected WithEvents lblManufacturer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents btnVolumePricing As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblStockInfo As System.Web.UI.WebControls.Label
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAddToCart As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnAddToSavedCart As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnEMailFriend As System.Web.UI.WebControls.ImageButton
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents CAttributeControl1 As CAttributeControl
    Protected WithEvents CInventoryControl1 As CInventoryControl
    Protected WithEvents Volumepricing1 As VolumePricing
    Protected WithEvents pnlStock As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPrice As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlVolumePriceing As System.Web.UI.WebControls.Panel
    Protected WithEvents lblSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalePriceDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegularPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegularPriceDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustomPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblcustomPriceDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents trVolumePricing3 As HtmlTableRow
    Protected WithEvents AttRow As HtmlTableCell
    Protected WithEvents trVolumePricing As HtmlTableRow
    Protected WithEvents DisplayStockInfo As HtmlTableRow
    Protected WithEvents StockDisplay As HtmlTableRow
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Private m_lngId As Long
    Dim m_objProduct As CCartItem
    Protected WithEvents txtId As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShrtDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lnkProductCode As System.Web.UI.WebControls.HyperLink
    Protected WithEvents tblProdBot As System.Web.UI.HtmlControls.HtmlTable

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

#Region " Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment, True)
        Try
            Me.ErrorMessage.Visible = False
            If Page.IsPostBack Then
            Else
                If Not IsNothing(StockInfo) Then
                    StockInfo.Visible = False
                End If
                SetProduct(CLng(Me.txtId.Text))
                CAttributeControl1.Data_Bind(Me.Product)
                If IsNothing(CInventoryControl1) = False Then
                    CInventoryControl1.Visible = False
                End If
                If IsNothing(Volumepricing1) = False Then
                    Volumepricing1.Visible = False
                End If

            End If
            If IsNothing(btnAddToCart) = False Then
                If btnAddToCart.ImageUrl = "" Then
                    btnAddToCart.ImageUrl = Me.AddCartImage
                End If
            End If

            If IsNothing(btnAddToSavedCart) = False Then
                If btnAddToSavedCart.ImageUrl = "" Then
                    btnAddToSavedCart.ImageUrl = Me.AddToSavedCartImage
                End If

            End If

            If IsNothing(btnEMailFriend) = False Then
                If btnEMailFriend.ImageUrl = "" Then
                    btnEMailFriend.ImageUrl = EmailImage
                End If
            End If
        Catch err As SystemException
            'may be No Bot on page
        End Try
    End Sub
#End Region

#Region "Public ReadOnly Property AddCartImage() As String"

	Public ReadOnly Property AddCartImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value

            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region "Public ReadOnly Property AddToSavedCartImage() As String"

	  Public ReadOnly Property AddToSavedCartImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region "Public ReadOnly Property EmailImage() As String"

	  Public ReadOnly Property EmailImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region " Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)"

    Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If IsNumeric(txtQty.Text) = False Then
                txtQty.Text = 1
            End If
            Dim nLong As Long = CLng(txtQty.Text)
        Catch objErr As Exception
            ' obj.AddErrorMessage(m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty"))
            Exit Sub
        End Try
        AddCartClick(sender, e, txtQty, CAttributeControl1)
    End Sub

#End Region

#Region " Public Property Product() As CProduct"


    Public Property Product() As CCartItem
        Get
            Return m_objProduct
        End Get
        Set(ByVal Value As CCartItem)
            m_objProduct = Value
        End Set
    End Property

#End Region

#Region " Public WriteOnly Property ProdId() As Long"

    Public WriteOnly Property ProdId() As Long
        Set(ByVal Value As Long)
            m_lngId = Value
        End Set
    End Property

#End Region

#Region "Public ReadOnly Property DetailLink() As String"

    Public ReadOnly Property DetailLink() As String
        Get
            If (Product.DetailLink.StartsWith(".") = True) Then
                Dim slink As String
                slink = Replace(Product.DetailLink, "../", "")
                slink = Replace(slink, "./", "")
                If (slink.StartsWith("/") = True) Then
                    slink = Replace(Product.DetailLink, "/", "")
                End If
                Return StoreFrontConfiguration.SiteURL & slink
            Else
                Return Product.DetailLink
            End If
        End Get
    End Property


#End Region

#Region " Public Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)"


    Public Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objbutton As LinkButton
        objbutton = CType(sender, LinkButton)
        If Volumepricing1.Visible = True Then
            Volumepricing1.Visible = False
        Else
            '   Volumepricing1.
            Volumepricing1.Visible = True
        End If

    End Sub

#End Region

#Region "Private Sub Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl)"


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
                If oAttributeControl.DisplayType = oAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If
                sAttName = ""
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text
                '
                If objAttributes.SelectedItem Is Nothing Then
                    m_bAttribute_Error = True
                    Me.ErrorMessage.Text = sAttName & " Required"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    ' required raise error
                    Me.ErrorMessage.Text = sAttName & " Required "
                    Me.ErrorMessage.Visible = True
                    m_bAttribute_Error = True
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
                        Me.ErrorMessage.Text = sAttName & " Required "
                        Me.ErrorMessage.Visible = True
                        m_bAttribute_Error = True
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

#Region "Public Sub AddCartClick(ByVal sender As Object, ByVal e As ImageClickEventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)"


    Public Sub AddCartClick(ByVal sender As Object, ByVal e As ImageClickEventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)
        Dim objButton As New LinkButton()
        Dim objIButton As ImageButton

        objIButton = CType(sender, ImageButton)
        If IsNumeric(txtQty.Text) = False Then
            txtQty.Text = 1
        End If
        objIButton.CommandArgument = txtQty.Text
        objButton.CommandArgument = txtQty.Text
        objButton.ID = objIButton.ID
        objButton.CommandName = objIButton.CommandName
        objButton.CommandArgument = objIButton.CommandArgument
        'get attributes selected
        Set_Item_Attributes(oAttControl)
        Dim arPage() As String
        arPage = Split(Me.CurrentWebPage, "/")

        Me.sPage = arPage(arPage.Length - 1)
        If (objButton.ID = "btnAddToCart") Then
            Me.AddItemToCart(objButton, e)
        ElseIf (objButton.ID = "btnAddToSavedCart") Then
            Me.AddItemToSavedCart(objButton, e)
        ElseIf (objButton.ID = "btnEMailFriend") Then
            EMailAFriend(objButton, e)
        ElseIf (objButton.ID = "btnVolumePricing") Then
            If Volumepricing1.Visible = True Then
                Volumepricing1.Visible = False
            Else
                Volumepricing1.Visible = True
            End If
        End If
    End Sub
#End Region

#Region " Public Sub SetProduct(ByVal lngProdId As Long)"

    Public Sub SetProduct(ByVal lngProdId As Long)
        Dim obXML As XmlNode
        Try


            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                obXML = m_objXMLAccess.GetProduct(lngProdId)
                Product = New CCartItem(obXML, 1, , Me.m_objCustomer.CustomerGroup)
            Else
                Dim oprodManagement As New Management.CProductManagement()
                Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(lngProdId, m_objCustomer.CustomerGroup).Products.Rows(0)
                Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
                oprodManagement = Nothing
            End If

            If Not IsNothing(StockInfo) Then

                If Product.Inventory.InventoryTracked = True Then
                    If Product.Inventory.ShowStatus = True Then
                        StockInfo.Visible = True
                    Else
                        StockInfo.Visible = False
                    End If

                End If
            End If

            If IsNothing(pnlVolumePriceing) = False Then


                If Product.HasVolumePricing Then
                    pnlVolumePriceing.Visible = True
                Else
                    pnlVolumePriceing.Visible = False
                End If
            End If
            DataBind()
        Catch err As SystemException
            'may be No Bot on page
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try

    End Sub
#End Region

#Region " Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objbutton As LinkButton = sender
        If CInventoryControl1.Visible = True Then
            CInventoryControl1.Visible = False
        Else
            CInventoryControl1.Visible = True
            CInventoryControl1.ProductID = CLng(objbutton.CommandName)
        End If
    End Sub

#End Region

#Region "Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded"

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        Try
            If (IsNothing(Session("ItemAdded")) = False) Then
                SetMessage(Message)
            Else
                Message.Text = ""
                Message.Visible = False
            End If
        Catch err As SystemException
        End Try
    End Sub

#End Region

#Region "Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR"

    Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR
        Try
            ErrorMessage.Visible = True
            ErrorMessage.Text = sender.ToString
        Catch err As System.Exception
        End Try
    End Sub

#End Region

#Region "Private Sub pnlPrice_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlPrice.DataBinding"

    Private Sub pnlPrice_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlPrice.DataBinding
        pnlPrice = sender
        If Not IsNothing(Me.Product) Then
            Dim tempSalePrice As Decimal
            Dim tempRegPrice As Decimal
            Dim tempCustomPrice As Decimal
            tempCustomPrice = GetCustomPrice()
            tempRegPrice = Product.Price
            tempSalePrice = Product.SalePrice
            CType(pnlPrice.FindControl("lblRegularPriceDisplay"), Label).Text = PriceDisplay2(tempRegPrice)
            CType(pnlPrice.FindControl("lblSalePriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempSalePrice)
            CType(pnlPrice.FindControl("lblCustomPriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempCustomPrice)
            If Product.IsOnSale And tempSalePrice < tempRegPrice Then
                If tempCustomPrice < tempSalePrice And tempCustomPrice <> 0 Then
                    CType(pnlPrice.FindControl("trCustomPrice"), HtmlTableRow).Visible = True
                    CType(pnlPrice.FindControl("trSalePrice"), HtmlTableRow).Visible = False
                    CType(pnlPrice.FindControl("trRegularPrice"), HtmlTableRow).Visible = False
                Else
                    CType(pnlPrice.FindControl("trCustomPrice"), HtmlTableRow).Visible = False
                    CType(pnlPrice.FindControl("trSalePrice"), HtmlTableRow).Visible = True
                    CType(pnlPrice.FindControl("trRegularPrice"), HtmlTableRow).Visible = False
                End If
            ElseIf tempCustomPrice < tempRegPrice And tempCustomPrice <> 0 Then
                CType(pnlPrice.FindControl("trCustomPrice"), HtmlTableRow).Visible = True
                CType(pnlPrice.FindControl("trSalePrice"), HtmlTableRow).Visible = False
                CType(pnlPrice.FindControl("trRegularPrice"), HtmlTableRow).Visible = False
            Else
                CType(pnlPrice.FindControl("trCustomPrice"), HtmlTableRow).Visible = False
                CType(pnlPrice.FindControl("trSalePrice"), HtmlTableRow).Visible = False
                CType(pnlPrice.FindControl("trRegularPrice"), HtmlTableRow).Visible = True
            End If
        End If

    End Sub


#End Region

#Region "Private Function GetCustomPrice() As Decimal"

    Private Function GetCustomPrice() As Decimal
        Dim tempPrice As Decimal
        Dim custPrice As Decimal
        Dim savings As Decimal
        custPrice = Product.CustomerSpecificPrice
        If Product.IsOnSale Then
            If custPrice < Product.SalePrice Then
                Return custPrice
            Else
                Return Product.SalePrice
            End If
        ElseIf custPrice < Product.Price Then
            Return custPrice
        End If
        Return 0.0
    End Function

#End Region

End Class
