Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Partial Class ProductBundleControl
    Inherits CWebControl


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
   
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property SingleBundlePrice(ByVal itemPrice As Decimal, ByVal qty As Long) As String
        Get
            If CDec(hidPrice.Value) = 0 OrElse CDec(hidTotalPrice.Value) = 0 OrElse itemPrice = 0 Then
                Return Format(0, "c")
            End If
            hidTempCount.Value = CLng(hidTempCount.Value) + 1
            If hidBundleCount.Value = hidTempCount.Value Then
                Return Format(CDec(hidPrice.Value) - CDec(hidTempPrice.Value), "c") & " (" & qty & " x " & Format((CDec(hidPrice.Value) - CDec(hidTempPrice.Value)) / qty, "c") & ")"
            End If
            Dim singleItemPrice As Decimal = Math.Round((itemPrice * CDec(hidPrice.Value)) / CDec(hidTotalPrice.Value), 2)
            Dim tempTotalPrice As Decimal = singleItemPrice * qty
            hidTempPrice.Value += singleItemPrice * qty
            Return Format(tempTotalPrice, "c") & " (" & qty & " x " & Format(singleItemPrice, "c") & ")"
        End Get
    End Property

#End Region

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Me.Visible = False OrElse IsNothing(Session("ProductID")) Then
            Exit Sub
        Else
            'Tee 7/20/2007 product configurator
            btnSaveBundle.Attributes.Add("OnClick", "return Validation();")
            Dim prodID As Long = Session("ProductID")
            hidProdId.Value = prodID
            Dim objProdManagement As New CProductManagement(prodID)
            CType(Me.Parent.FindControl("lblPDName"), Label).Text = objProdManagement.Name
            hidPrice.Value = objProdManagement.Price
            MakeCommonVisible(objProdManagement.ProductType, False)
            'end Tee
            Dim objProd As New CProductBundle
            'Dim arrResult As ArrayList
            If Not IsNothing(Session("ArrChecked")) Then
                Dim bundleProd As ArrayList = Session("ArrChecked")
                Session("ArrChecked") = Nothing
                BindData(objProd.GetBundleProducts(prodID, bundleProd))
            ElseIf Not IsPostBack Then
                BindData(objProd.GetBundleProducts(prodID))
            End If
            'end Tee
        End If
    End Sub
#End Region

#Region "Private Sub selectProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectProducts.Click"
    Private Sub selectProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectProducts.Click
        Dim prodID As Long = CLng(hidProdId.Value)
        Dim ProdIDList As New ArrayList
        Dim objProd As New CProductBundle
        ProdIDList = objProd.GetBundleProductIDList(prodID)

        Session("ApplyTo") = "6"
        Session("ReturnPage") = "BundleComponents.aspx?ProdType=2"
        Session("ArrChecked") = ProdIDList
        Session("ProdID") = prodID
        Response.Redirect("multiselect.aspx")
    End Sub
#End Region

    'Tee 8/1/2007 product configurator
#Region "Private Sub btnSaveBundle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBundle.Click"
    Private Sub btnSaveBundle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBundle.Click
        Dim _arrBundles As New ArrayList
        Dim _prodBundle As CProductBundleBase
        Dim objProdBundle As New CProductBundle
        Dim objProd As New CProductBundle
        For Each dli As DataListItem In bundleProductList.Items
            _prodBundle = New CProductBundleBase
            _prodBundle.DisplayOrder = CLng(CType(dli.FindControl("txtDisplayOrder"), TextBox).Text)
            _prodBundle.Quantity = CLng(CType(dli.FindControl("quantity1"), TextBox).Text)
            _prodBundle.BundleID = CLng(CType(dli.FindControl("hidBundleId"), HtmlInputHidden).Value)
            _arrBundles.Add(_prodBundle)
        Next
        objProdBundle.AssignProductsToBundle(CLng(hidProdId.Value), _arrBundles)
        hidPrice.Value = (New CProductManagement(CLng(hidProdId.Value))).Price
        hidTempCount.Value = "0"
        hidTempPrice.Value = "0"
        BindData(objProd.GetBundleProducts(CLng(hidProdId.Value)))
    End Sub
#End Region

#Region "Private Function GetTotalPrice(ByVal arr As ArrayList) As Decimal"
    Private Function GetTotalPrice(ByVal arr As ArrayList) As Decimal
        Dim _totalPrice As Decimal = 0
        For Each _prodBundle As CProductBundle In arr
            _totalPrice += (_prodBundle.Product.Price * _prodBundle.Quantity)
        Next
        Return _totalPrice
    End Function
#End Region

#Region "Private Sub BindData(ByVal arrResult As ArrayList)"
    Private Sub BindData(ByVal arrResult As ArrayList)
        hidBundleCount.Value = arrResult.Count
        hidTotalPrice.Value = GetTotalPrice(arrResult)
        bundleProductList.DataSource = arrResult
        bundleProductList.DataBind()
    End Sub
#End Region
    'end Tee
End Class
