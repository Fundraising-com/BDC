
Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Partial  Class BYODetailControl
    Inherits CProductDetailBase

    Protected WithEvents btnAddToCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnAddToSavedCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents CAttributeControl1 As CAttributeControl
    Protected WithEvents trAttributesSpacer As HtmlTableRow
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEMailFriend As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblRCustomPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRCustomPriceOnly As System.Web.UI.WebControls.Label
    Protected WithEvents btnVolumePricing As System.Web.UI.WebControls.LinkButton
    Protected WithEvents trAttributes As HtmlTableRow
    Protected WithEvents imgEMailFriend As System.Web.UI.WebControls.Image
    Protected WithEvents imgAddToCart As System.Web.UI.WebControls.Image
    Protected WithEvents imgAddToSavedCart As System.Web.UI.WebControls.Image
    Protected WithEvents lblSubscriptionPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRecurringPrice As System.Web.UI.WebControls.Label

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

        MyBase.LoadSettings(StoreFrontConfiguration.ProductDetail())
        CAttributeControl1.DisplayType = m_AttributeDisplay
        If Not IsPostBack Then
            If Request.QueryString("ID") = 0 Then
                Product = Session("dProduct")
                Me.trVolumePricing3.Visible = False
                CAttributeControl1.Data_Bind(Product, m_objCustomer) '1521
                Me.CInventoryControl1.Visible = False
                DataBind()
                'Tee 9/11/2007 product configurator
                CAttributeControl1.SetAttributeControlDisplay()
                'end Tee
            Else
                Dim oprodManagement As New CProductManagement
                Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(Request.QueryString("ID"), m_objCustomer.CustomerGroup).Products.Rows(0)
                Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
                oprodManagement = Nothing
                Session("dProduct") = Product
                CAttributeControl1.Data_Bind(Product, m_objCustomer) '1521
                Me.CInventoryControl1.Visible = False
                DataBind()
                'Tee 9/11/2007 product configurator
                CAttributeControl1.SetAttributeControlDisplay()
                'end Tee
            End If
            
        Else
            Product = Session("dProduct")
        End If

        ProductImage.Visible = MyBase.m_bDisplayImage
        If Product.Vendor = "No Vendor" Or Product.Vendor = "" Then
            m_bDisplayVendor = False
        End If
        If Product.CategoryNames = "No Category" Or Product.CategoryNames = "" Then
            Me.m_bDisplayCategory = False
        End If
        If Product.Manufacturer = "No Manufacturer" Or Product.Manufacturer = "" Then
            Me.m_bDisplayManufacturer = False
        End If
        Me.SetStockDisplay(StockInfo)
        If (MyBase.m_bDisplayLabels = False) Then
            lblProductName.Visible = False
            lblProductCode.Visible = False
            lblVendor.Visible = False
            lblManufacturer.Visible = False
            lblCategory.Visible = False
            lblDescription.Visible = False
        End If
        SetDisplay2(True)
        If (CAttributeControl1.Visible = False) Then
            trAttributesSpacer.Visible = False
            trAttributes.Visible = False
        End If
        RelatedProducts(DetailTemplate.Template2, Repeater1, SeperatorLine)
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

End Class
