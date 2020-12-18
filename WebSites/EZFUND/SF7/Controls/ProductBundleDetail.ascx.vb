Imports StoreFront.BusinessRule
'Tee 9/12/2007 product configurator
Imports StoreFront.SystemBase
'end Tee

Partial Class ProductBundleDetail
    Inherits CProductDetailBase

    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents CAttributeControl1 As CAttributeControl
    'Tee 9/13/2007 product configurator
    Private m_iAttributeDisplayType As Integer = 0
    'end Tee

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

    'Tee 9/13/2007 product configurator
#Region "Properties"
    Public Property AttributeDisplayType() As Integer
        Get
            Return m_iAttributeDisplayType
        End Get
        Set(ByVal Value As Integer)
            m_iAttributeDisplayType = Value
        End Set
    End Property
#End Region
    'end Tee

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Tee 9/12/2007 product configurator
        If Page.IsPostBack Then
            Exit Sub
        End If
        'end Tee
        If Me.Visible = False Then Exit Sub
        If Not IsNothing(Session("dProduct")) Then
            Product = Session("dProduct")
        Else
            Exit Sub
        End If

        Dim dt As New BundleDataTable
        Dim row As DataRow

        For Each cItem As CCartItem In Product.BundledProducts
            row = dt.NewRow
            row.Item("name") = cItem.Name
            row.Item("bundledquantity") = cItem.BundledQuantity
            row.Item("shortdescription") = cItem.ShortDescription
            row.Item("productID") = cItem.ProductID
            If cItem.Inventory.InventoryTracked = True Then
                If cItem.Inventory.ShowStatus = True Then
                    If cItem.Inventory.StockIsDepleted Then
                        row.Item("stock") = "Out of Stock!"
                    Else
                        row.Item("stock") = "In Stock!"
                    End If
                End If
            End If
            row.Item("detaillink") = cItem.DetailLink
            row.Item("smallimage") = cItem.SmallImage
            dt.Rows.Add(row)
        Next

        rptBundle.DataSource = dt
        rptBundle.DataBind()

        'Tee 9/11/2007 product configurator
        Dim ac As CAttributeControl
        For Each item As RepeaterItem In rptBundle.Items
            ac = DirectCast(item.FindControl("CAttributeControl1"), CAttributeControl)
            ac.SetAttributeControlDisplay()
        Next
        'end Tee
    End Sub

    Private Sub rptBundle_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptBundle.ItemCreated
        Product = Session("dProduct")
        Dim cust As New CCustomer

        Dim att As CAttributeControl = e.Item.FindControl("CAttributeControl1")

        For Each cItem As CCartItem In Product.BundledProducts
            'Tee 9/12/2007 product configurator
            If IsNothing(e.Item.DataItem) Then
                Exit For
            End If
            'end Tee
            If cItem.ProductID = e.Item.DataItem("ProductID") Then
                att.DisplayType = AttributeDisplayType
                'Tee 10/8/2007 fix for bug 607, 1.1 to 2.0 transition
                att.Data_Bind(cItem, cust, False)
                'end Tee
            End If
        Next
    End Sub

    'Tee 9/12/2007 product configurator
#Region "Public Sub AddBundleAttributes()"
    Public Function AddBundleAttributes() As Boolean
        Dim drProd As dsProducts.ProductsRow
        Dim prodManage As New Management.CProductManagement
        Dim bundleProducts As New CProducts
        Dim prod As CProduct
        For Each item As RepeaterItem In rptBundle.Items
            If item.ItemType = ListItemType.AlternatingItem OrElse item.ItemType = ListItemType.Item Then
                Dim arrAtts As ArrayList = Set_Item_Attributes(CType(item.FindControl("CAttributeControl1"), CAttributeControl))
                If IsNothing(arrAtts) Then
                    Session.Remove("BundledProducts")
                    Return False
                End If
                drProd = prodManage.GetProductRow(CLng(CType(item.FindControl("hidProdID"), HtmlInputHidden).Value), m_objCustomer.CustomerGroup).Products.Rows(0)
                prod = New CProduct(drProd, 1, arrAtts)
                prod.BundledQuantity = CLng(CType(item.FindControl("hidQty"), HtmlInputHidden).Value)
                bundleProducts.Add(prod)
            End If
        Next
        Session("BundledProducts") = bundleProducts
        Return True
    End Function
#End Region

#Region "Private Function Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl) As ArrayList"
    Private Function Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl) As ArrayList
        Dim dlItem As DataListItem
        'Dim oCont As Control
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        Dim objAttributes As Object
        Dim sAttName As String
        Dim m_OrderAttributes As ArrayList
        Dim ErrorMessage As Label = CType(Parent.Page.FindControl("ErrorMessage"), Label)

        If oAttributeControl Is Nothing Then
            Return Nothing
        Else
            m_OrderAttributes = New ArrayList
            'set attributes 
            DlAttributes = CType(oAttributeControl.FindControl("DlAttributes"), DataList)
            For Each dlItem In DlAttributes.Items
                'late bind based on dropdown or radiolist
                If oAttributeControl.DisplayType = CAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text

                If objAttributes.SelectedItem Is Nothing Then
                    'raise required error
                    ErrorMessage.Text = sAttName & " required"
                    ErrorMessage.Visible = True
                    Return Nothing
                    ' begin: JDB - Dynamic Image Suite
                    ' note: added to determine of the attribute has been set as unavailable or out-of-stock
                    '   and not backorderable
                ElseIf objAttributes.Visible AndAlso Request.Form(objAttributes.UniqueID) = "" Then
                    ErrorMessage.Text = sAttName & " required"
                    ErrorMessage.Visible = True
                    Return Nothing
                    ' end: JDB - Dynamic Image Suite
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    'raise required error
                    ErrorMessage.Text = sAttName & " required"
                    ErrorMessage.Visible = True
                    Return Nothing
                Else
                    Dim oAttStorage As New CAttributesSelected
                    oattStorage.UID = CLng(objAttributes.SelectedItem.Value())
                    sTemp = CType(dlItem.FindControl("AttributeID"), TextBox).Text
                    oattStorage.AttributeId = CLng(sTemp)
                    m_OrderAttributes.Add(oattStorage)
                End If
            Next
            'set Custom attributes
            DlCustomAttributes = CType(oAttributeControl.FindControl("dlCustomAttributes"), DataList)
            For Each dlItem In DlCustomAttributes.Items
                Dim oAttStorage As New CAttributesSelected
                IsRequired = CType(dlItem.FindControl("CustomRequired"), TextBox).Text
                If IsRequired = True Then
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) = "" Then
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        'raise required error
                        ErrorMessage.Text = sAttName & " required"
                        ErrorMessage.Visible = True
                        Return Nothing
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
            Return m_OrderAttributes
        End If
    End Function
#End Region
    'end Tee

End Class

<Serializable()> _
    Public Class BundleDataTable
    Inherits DataTable

    Public Sub New()
        Me.Columns.Add(New DataColumn("name", GetType(String)))
        Me.Columns.Add(New DataColumn("bundledquantity", GetType(Integer)))
        Me.Columns.Add(New DataColumn("shortdescription", GetType(String)))
        Me.Columns.Add(New DataColumn("productID", GetType(Integer)))
        Me.Columns.Add(New DataColumn("stock", GetType(String)))
        Me.Columns.Add(New DataColumn("detaillink", GetType(String)))
        Me.Columns.Add(New DataColumn("smallimage", GetType(String)))
    End Sub
End Class
