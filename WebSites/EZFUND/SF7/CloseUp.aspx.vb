Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.systembase
Partial Class CloseUp
    Inherits CWebPage
    Protected WithEvents dlSwatch As System.Web.UI.WebControls.DataList
    Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
    Protected imgProductImage As System.Web.UI.HtmlControls.HtmlImage
    Private Product As CCartItem
    Private imgPath As String = ""
    Protected WithEvents Swatches As CSwatch
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
        'Put user code to initialize the page here
        Product = Session("dProduct")
        If IsNothing(Product) = False Then
            Dim cProdMan As New Management.CProductManagement(Product.ProductID)
            Dim drProd As dsProducts.ProductsRow = cProdMan.GetProductRow(Product.ProductID, Product.CustomerGroup).Products.Rows(0)
            Product = New CCartItem(drProd, 1, , Product.CustomerGroup)


            Dim tmpStrUID As String = ""
            tmpStrUID = Request.QueryString("uid").ToString
            imgPath = cProdMan.CloseUpImage
            Dim al1 As ArrayList
            al1 = Product.GetCloseUpSwatches
            If tmpStrUID <> "" Then
                Dim tmpSwatch As Swatch
                For Each tmpSwatch In al1
                    If tmpSwatch.SwatchID = CLng(tmpStrUID) Then
                        imgPath = tmpSwatch.CloseUpImage
                    End If
                Next
            End If


            'BEGIN CUSTOM CODE September '04
            Swatches.SwatchAllignment = Product.SwatchAllignment
            Swatches.ChangeOnClick = Product.ChangeOnClick
            Swatches.ChangeOnMouseover = Product.ChangeOnMouseover
            Swatches.PerRow = Product.SwatchesPerRow
            Swatches.DescriptionAllignment = Product.DescriptionAllignment
            Swatches.IsCloseUp = True
            Swatches.Swatches = al1
            CType(Swatches.FindControl("CloseUp"), HyperLink).Visible = False
            CType(Swatches.FindControl("imgHyperlink"), HyperLink).NavigateUrl = ""
        End If

        'Tee 10/9/2007 fix for bug 573
        If imgPath.ToLower.StartsWith("http://") OrElse imgPath.ToLower.StartsWith("https://") Then
            CType(Swatches.FindControl("imgProductImage"), Web.UI.HtmlControls.HtmlImage).Src = imgPath
        Else
            CType(Swatches.FindControl("imgProductImage"), Web.UI.HtmlControls.HtmlImage).Src = "../" & imgPath
        End If
        'end Tee

    End Sub
    Public ReadOnly Property ImageString() As String
        Get
            Return imgPath

        End Get
    End Property

End Class
