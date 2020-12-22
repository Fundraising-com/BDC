Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Public MustInherit Class ProductDetail1
    Inherits CProductDetailBase

    Protected WithEvents btnAddToCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnAddToSavedCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ProductImage As System.Web.UI.WebControls.Panel
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblVendor As System.Web.UI.WebControls.Label
    Protected WithEvents lblManufacturer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents CAttributeControl1 As CAttributeControl
    Protected WithEvents btnVolumePricing As System.Web.UI.WebControls.LinkButton

    Protected WithEvents trAttributesSpacer As HtmlTableRow
    Protected WithEvents btnEMailFriend As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblRCustomPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRCustomPriceOnly As System.Web.UI.WebControls.Label
    Protected WithEvents lblRProductName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRProductCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductName As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductCode As System.Web.UI.WebControls.Label
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblQty As System.Web.UI.WebControls.Label
    ' Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgEMailFriend As System.Web.UI.WebControls.Image
    Protected WithEvents imgAddToCart As System.Web.UI.WebControls.Image
    Protected WithEvents imgAddToSavedCart As System.Web.UI.WebControls.Image
    Protected WithEvents trAttributes As HtmlTableRow

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
        If Me.Visible = False Then Exit Sub
        Dim obXML As XmlNode
        imgAddToCart.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
        imgAddToSavedCart.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
        imgEMailFriend.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
        MyBase.LoadSettings(StoreFrontConfiguration.ProductDetail())
        Me.btnVolumePricing.Text = StoreFrontConfiguration.Labels.Item("lblVolumePrice").InnerText()
        CAttributeControl1.DisplayType = m_AttributeDisplay
        If Not IsPostBack Then
            If Request.QueryString("ID") = 0 Then
                Product = Session("dProduct")
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    obXML = m_objXMLAccess.GetProduct(Product.ProductID)
                End If
                Me.trVolumePricing3.Visible = False
                CAttributeControl1.Data_Bind(Product)
                Me.CInventoryControl1.Visible = False
                DataBind()
            Else
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    obXML = m_objXMLAccess.GetProduct(Request.QueryString("ID"))
                    Product = New CCartItem(obXML, 1, , Me.m_objCustomer.CustomerGroup)
                Else
                    Dim oprodManagement As New CProductManagement()
                    Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(Request.QueryString("ID"), m_objCustomer.CustomerGroup).Products.Rows(0)
                    Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
                    oprodManagement = Nothing
                End If
                Session("dProduct") = Product
                Me.trVolumePricing3.Visible = False
                CAttributeControl1.Data_Bind(Product)
                Me.CInventoryControl1.Visible = False
                DataBind()
            End If
        Else
            Product = Session("dProduct")
        End If
        ProductImage.Visible = MyBase.m_bDisplayImage
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
        End If
        SetDisplay1()
        If (CAttributeControl1.Visible = False) Then
            trAttributesSpacer.Visible = False
            trAttributes.Visible = False
        End If
        RelatedProducts(DetailTemplate.Template1, Repeater1)
        Me.SetStockDisplay(StockInfo)
        If IsPostBack = False Then
            txtQty.Text = MyBase.m_nDefaultQty
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

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        If (IsNothing(e.Item.DataItem) = False) Then
            If (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then
                SetDisplay(e.Item)
                LabelText(e.Item)
            End If

        End If
    End Sub

    Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim nLong As Long = CLng(txtQty.Text)
        Catch objErr As Exception
            AddErrorMessage(m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty"))
            Exit Sub
        End Try
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
