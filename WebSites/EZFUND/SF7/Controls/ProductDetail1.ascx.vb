Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Partial  Class ProductDetail1
    Inherits CProductDetailBase

    Protected WithEvents ProductImage As System.Web.UI.WebControls.Panel
    Protected WithEvents CAttributeControl1 As CAttributeControl

    Protected WithEvents trAttributesSpacer As HtmlTableRow
    Protected WithEvents lblRCustomPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRCustomPriceOnly As System.Web.UI.WebControls.Label
    Protected WithEvents lblRProductName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRProductCode As System.Web.UI.WebControls.Label
    ' Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
 	'BEGIN CUSTOM CODE 3/6/04
    Protected WithEvents dlSwatch As System.Web.UI.WebControls.DataList
    Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents CloseUp As System.Web.UI.WebControls.HyperLink
    Protected WithEvents Swatches As CSwatch
    'END CUSTOM CODE 3/6/04
    Protected WithEvents trAttributes As HtmlTableRow
    'SKC Product Bundles
    Protected WithEvents ProductBundleDetail1 As ProductBundleDetail

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
        'Tee 10/23/2007 added
        If IsPostBack Then Exit Sub
        'end Tee
        If Me.Visible = False Then Exit Sub
        Dim obXML As XmlNode
        imgAddToCart.ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value)
        imgAddToSavedCart.ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value)
        imgEMailFriend.ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value)
        MyBase.LoadSettings(StoreFrontConfiguration.ProductDetail())
        'Tee 10/24/2007 added js
        btnVolumePricing.Attributes.Add("OnClick", "return DisplayStatus('" & btnVolumePricing.ClientID & "');")
        'end Tee
        Me.btnVolumePricing.Text = StoreFrontConfiguration.Labels.Item("lblVolumePrice").InnerText()
        CAttributeControl1.DisplayType = m_AttributeDisplay
        If Not IsPostBack Then
            If Request.QueryString("ID") = 0 Then
                Product = Session("dProduct")
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    obXML = m_objXMLAccess.GetProduct(Product.ProductID)
                End If
                Me.trVolumePricing3.Visible = False
                CAttributeControl1.Data_Bind(Product, m_objCustomer) '1521
                Me.CInventoryControl1.Visible = False
                DataBind()
            Else
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    obXML = m_objXMLAccess.GetProduct(Request.QueryString("ID"))
                    Product = New CCartItem(obXML, 1, , Me.m_objCustomer.CustomerGroup)
                Else
                    Dim oprodManagement As New CProductManagement
                    Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(Request.QueryString("ID"), m_objCustomer.CustomerGroup).Products.Rows(0)
                    Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
                    oprodManagement = Nothing
                End If
                Session("dProduct") = Product
                Me.trVolumePricing3.Visible = False
                CAttributeControl1.Data_Bind(Product, m_objCustomer) '1521
                Me.CInventoryControl1.Visible = False
                DataBind()
            End If
            'Tee 7/20/2007 product configurator
            If Product.ProductType = ProductType.Customized OrElse _
                Product.ProductType = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & Product.ProductID & "&Qty=1")
            End If
            'end Tee
            'SKC Product Bundles
            If Product.ProductType = ProductType.Bundle OrElse _
                Product.ProductType = ProductType.BundleSubscription Then
                'Show the ProductBundleDetail control
                ProductBundleDetail1.Visible = True
                'Tee 9/13/2007 product configurator
                ProductBundleDetail1.AttributeDisplayType = CAttributeControl1.DisplayType
                'end Tee
            End If
            'End SKC
            'begin: GJV - 6/12/2007 - Attribute Detail Hotfix
            If Not IsPostBack Then CAttributeControl1.SetAttributeControlDisplay()
            'end: GJV - 6/12/2007 - Attribute Detail Hotfix
        Else
            Product = Session("dProduct")
        End If

        'ELW MOD'
        If Product.isActive = False Then
            Response.Redirect("default.aspx")
        End If
        'End ELW MOD'

        'ProductImage.Visible = MyBase.m_bDisplayImage
        'BEGIN CUSTOM CODE September '04
        Swatches.SwatchAllignment = Product.SwatchAllignment
        Swatches.DescriptionAllignment = Product.DescriptionAllignment
        Swatches.ChangeOnClick = Product.ChangeOnClick
        Swatches.ChangeOnMouseover = Product.ChangeOnMouseover
        Swatches.PerRow = Product.SwatchesPerRow
        Swatches.Swatches = Product.GetDetailSwatches
        If Product.CloseUpImage <> "" Then
            CType(Swatches.FindControl("CloseUp"), HyperLink).Visible = Product.ShowCloseUpLink
            CType(Swatches.FindControl("CloseUp"), HyperLink).Text = Product.CloseUpLinkText

            If Product.LinkBigImage = False Then
                CType(Swatches.FindControl("imgHyperlink"), HyperLink).NavigateUrl = ""
            End If

        Else
            CType(Swatches.FindControl("CloseUp"), HyperLink).Visible = False
            CType(Swatches.FindControl("imgHyperlink"), HyperLink).NavigateUrl = ""
        End If
        Me.Swatches.AttributeControl = Me.CAttributeControl1
        'END CUSTOM CODE September '04
        'Tee 10/9/2007 fix for bug 573
        If BaseImageString.ToLower.StartsWith("http://") OrElse BaseImageString.ToLower.StartsWith("https://") Then
            CType(Swatches.FindControl("imgProductImage"), Web.UI.HtmlControls.HtmlImage).Src = BaseImageString
        Else
            CType(Swatches.FindControl("imgProductImage"), Web.UI.HtmlControls.HtmlImage).Src = MyBase.ResolveUrl("../" & BaseImageString)
        End If
        'end Tee
        'BEGIN CUSTOM CODE Oct '04
        If MyBase.m_bDisplayImage = False Then
            CType(Swatches.FindControl("imgHyperlink"), HyperLink).NavigateUrl = ""
        End If
        'END CUSTOM CODE Oct '04

        If (Product.Vendor = "No Vendor" Or Product.Vendor = "") And m_bDisplayVendor = True Then
            m_bdisplayvendor = False
        End If
        If (Product.Manufacturer = "No Manufacturer" Or Product.Manufacturer = "") And m_bDisplayManufacturer = True Then
            m_bDisplayManufacturer = False
        End If
        If Product.CategoryNames = "No Category" Or Product.CategoryNames = "" Then
            Me.m_bDisplayCategory = False
        End If
        If m_bDisplayVolumePricing Then
            m_bDisplayVolumePricing = (Product.HasVolumePricing)
        End If
        If (MyBase.m_bDisplayLabels = False) Then
            lblProductName.Visible = False
            lblProductCode.Visible = False
            lblVendor.Visible = False
            lblManufacturer.Visible = False
            lblPrice.Visible = False
            lblSalePrice.Visible = False
            ' lblStockInfo.Visible = False
            lblCategory.Visible = False
            lblDescription.Visible = False
            'Verisign Recurring Billing
            lblSubscriptionPrice.Visible = False
            lblRecurringPrice.Visible = False
            'Verisign Recurring Billing
        End If
        SetDisplay1()
        'BEGIN: GJV - 8/22/2007 - OSP merge
        'OSP 
 		 If trAddToCart.Visible = False Then
            CAttributeControl1.Visible = False
        End If
        'END: GJV - 8/22/2007 - OSP merge
        If (CAttributeControl1.Visible = False) Then
            trAttributesSpacer.Visible = False
            trAttributes.Visible = False
        End If
        'RelatedProducts(DetailTemplate.Template1, Repeater1)
        Me.SetStockDisplay(StockInfo)
        If IsPostBack = False Then
            txtQty.Text = MyBase.m_nDefaultQty
            'Tee 9/12/2007 product configurator
            If Request.QueryString("Qty") <> "" Then
                txtQty.Text = Request.QueryString("Qty")
            End If
            'end Tee
        End If
        LabelText(Me)

    End Sub

    Public ReadOnly Property ImageString() As String
        Get
            Return MyBase.BaseImageString()
        End Get
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return MyBase.BaseDescription()
        End Get
    End Property

    'Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
    '    If (IsNothing(e.Item.DataItem) = False) Then
    '        If (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then
    '            SetDisplay(e.Item)
    '            LabelText(e.Item)
    '        End If

    '    End If
    'End Sub

    Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim nLong As Long = CLng(txtQty.Text)
        Catch objErr As Exception
            AddErrorMessage(m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty"))
            Exit Sub
        End Try
        'Tee 9/12/2007 product configurator
        If Not ProductBundleDetail1.AddBundleAttributes() Then Exit Sub
        'end Tee
        AddCartClick(sender, e, txtQty, CAttributeControl1)
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If txtQty.Visible Then
            If Not IsNothing(imgAddToCart) Then
                imgAddToCart.Attributes.Add("onClick", "return DetailValidation('ProductDetail1:txtQty');")
            End If
        End If
    End Sub
End Class
