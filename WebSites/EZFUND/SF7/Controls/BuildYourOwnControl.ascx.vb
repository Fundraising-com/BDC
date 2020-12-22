Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class BuildYourOwnControl
    Inherits CWebControl

    Private mProductDetails As ArrayList
    Private mStepID As Integer
    Private mSelectable As Integer

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

    Public Property ProductDetails() As ArrayList
        Get
            Return mProductDetails
        End Get
        Set(ByVal Value As ArrayList)
            mProductDetails = Value
        End Set
    End Property

    Public Property StepID() As Integer
        Get
            Return mStepID
        End Get
        Set(ByVal Value As Integer)
            mStepID = Value
        End Set
    End Property

    Public Property Selectable() As Integer
        Get
            Return mSelectable
        End Get
        Set(ByVal Value As Integer)
            mSelectable = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsNothing(ProductDetails) Then
            productList = CType(Me.FindControl("productList"), DataList)
            productList.DataSource = ProductDetails
            productList.DataBind()
        End If
    End Sub

    ' begin: JDB - Customer Defined Rules
    Public Delegate Sub ProductAddedHandler(ByVal sender As Object, ByVal iItemIndex As Integer, ByVal oProduct As CProduct, ByVal oCheckbox As CheckBox, ByVal iSelectableQuantity As Integer, ByVal iProductDetailCount As Integer)
    Public Event ProductAdded As ProductAddedHandler
    ' end: JDB - Customer Defined Rules

    Private Sub productList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles productList.ItemDataBound
        Dim checkID As String
        If Not IsNothing(e.Item) Then
            'Tee 8/21/2007 product configurator
            Dim objAtt As CAttributeControl = CType(e.Item.FindControl("CAttributeControl1"), CAttributeControl)
            objAtt.DisplayType = CInt("0" & StoreFrontConfiguration.ProductDetail.Attributes("AttributeDisplay").Value)
            objAtt.Data_Bind(CType(e.Item.DataItem, CCustomizeBundleProductDetailBase).Product, m_objCustomer)
            objAtt.SetAttributeControlDisplay()
            'moved stock check to attributes level
            'If Not IsItemStocked(e.Item.DataItem) Then
            '    CType(e.Item.FindControl("checkProduct"), CheckBox).Enabled = False
            '    CType(e.Item.FindControl("StockMessage"), Label).Visible = True
            'End If
            'end Tee
            checkID = CType(e.Item.FindControl("checkProduct"), CheckBox).UniqueID
            ' begin: JDB - Customer Defined Rules
            'CType(e.Item.FindControl("checkProduct"), CheckBox).Attributes.Add("onclick", "Javascript:limitSelection(" & StepID & "," & Selectable & "," & ProductDetails.Count & ",'" & checkID & "')")
            Dim oCheckbox As CheckBox = e.Item.FindControl("checkProduct")
            Dim sOnChangeJavascript As String = _
                "Javascript:limitSelection(" & StepID & "," & Selectable & "," & ProductDetails.Count & ",'" & checkID & "');" & _
                "CheckBundleRuleValues(aoCustomerBundleRules,false);"
            oCheckbox.Attributes.Add("onclick", sOnChangeJavascript)

            RaiseEvent ProductAdded(Me, e.Item.ItemIndex, CType(e.Item.DataItem, CCustomizeBundleProductDetailBase).Product, oCheckbox, Me.Selectable, Me.ProductDetails.Count)
            ' end: JDB - Customer Defined Rules
        End If
    End Sub

    'Private Function IsItemStocked(ByVal mealProdDetail As CCustomizeBundleProductDetailBase) As Boolean
    '    Dim inventory As Inventory_Management
    '    inventory = New Inventory_Management(mealProdDetail.ProductID)
    '    If inventory.Inventory.InventoryTracked Then
    '        If inventory.Inventory.ItemsAreStocked(mealProdDetail.Product.Attributes, CLng(Request.QueryString("Qty")) * mealProdDetail.Quantity) Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        Return True
    '    End If
    'End Function
End Class
