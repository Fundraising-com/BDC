' begin: JDB - 4/2/2007 - UrlRewriter Add-On
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

Public Class MappedURLsControl
    Inherits System.Web.UI.UserControl

    Private Const PageSize As Integer = 10

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgMappedURLs As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Me.BindDataGrid()
        End If
    End Sub

    Private _categories As ArrayList
    Private ReadOnly Property Categories() As ArrayList
        Get
            If IsNothing(Me._categories) Then
                Dim categoryAccess As New CCategories
                Me._categories = categoryAccess.GetAllCategories()
            End If
            Return Me._categories
        End Get
    End Property

    Private Function FindCategoryName(ByVal categoryID As Integer) As String
        Dim categoryName As String = String.Empty
        For Each category As CXMLCategoryList In Me.Categories
            If category.ID = categoryID Then
                categoryName = category.Name
                Exit For
            End If
        Next
        Return categoryName
    End Function

    Private _products As ArrayList
    Private ReadOnly Property Products() As ArrayList
        Get
            If IsNothing(Me._products) Then
                Dim productAccess As New CStoreProducts
                'Tee 2/13/2008 bug 1131 fix
                Me._products = productAccess.GetActiveProducts
                'end Tee
            End If
            Return Me._products
        End Get
    End Property

    Private Function FindProductName(ByVal productID As Integer) As String
        Dim productName As String = String.Empty
        For Each product As CProduct In Me.Products
            If product.ProductID = productID Then
                productName = product.Name
                Exit For
            End If
        Next
        Return productName
    End Function

    Protected ReadOnly Property VirtualDirectory() As String
        Get
            Dim siteURL As New Uri(StoreFrontConfiguration.SiteURL)
            Return siteURL.AbsolutePath
        End Get
    End Property

    Protected Const FileExt As String = ".aspx"

    Protected Function GetURLForEdit(ByVal url As Object) As String
        Return url.ToString().Replace(Me.VirtualDirectory, "").Replace(FileExt, "")
    End Function

    Protected Function GetMapsToForDisplay(ByVal url As String) As String
        Dim mapsToURL As New MapsTo(url)
        Select Case mapsToURL.Type
            Case MapsToType.Product
                Dim productName As String = Me.FindProductName(mapsToURL.Detail)
                Return "Product Detail (" & productName & ")"
            Case MapsToType.Category
                Dim categoryName As String = Me.FindCategoryName(mapsToURL.Detail)
                Return "Category Search (" & categoryName & ")"
            Case MapsToType.Custom
                Return "Custom Search (" & mapsToURL.Detail & ")"
            Case Else
                Return ""
        End Select
    End Function

    Protected Function FormatCurrency(ByVal NumberToFormat As Decimal) As String
        Return NumberToFormat.ToString("C")
    End Function

    Protected Function FormatNumber(ByVal NumberToFormat As Decimal) As String
        Return NumberToFormat.ToString("F2")
    End Function

    Private Sub BindDataGrid(Optional ByVal oDataSet As DataSet = Nothing)
        If IsNothing(oDataSet) Then
            oDataSet = Me.GetMappedURLs()
        End If
        If oDataSet.Tables(0).Rows.Count > PageSize Then
            Me.dgMappedURLs.PageSize = PageSize
            Me.dgMappedURLs.AllowPaging = True
        Else
            Me.dgMappedURLs.AllowPaging = False
        End If
        Me.dgMappedURLs.DataSource = oDataSet
        Me.dgMappedURLs.DataBind()
    End Sub

    Private m_oCMappedURLs As CMappedURLs
    Private ReadOnly Property MappedURLsBusiness() As CMappedURLs
        Get
            If IsNothing(Me.m_oCMappedURLs) Then
                Me.m_oCMappedURLs = New CMappedURLs
            End If
            Return Me.m_oCMappedURLs
        End Get
    End Property

    Private Function GetMappedURLs() As DataSet
        Return Me.MappedURLsBusiness.GetMappedURLs()
    End Function

    Private Sub dgMappedURLs_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMappedURLs.ItemCommand
        If e.CommandName = "Delete" Or e.CommandName = "Edit" Then
            If e.CommandName = "Delete" Then
                Me.MappedURLsBusiness.Delete(Me.dgMappedURLs.DataKeys(e.Item.ItemIndex))
                Me.btnAdd.Visible = True
            Else
                Me.dgMappedURLs.EditItemIndex = e.Item.ItemIndex
                Me.btnAdd.Visible = False
            End If
            Me.BindDataGrid()
        End If
        If e.CommandName = "Cancel" Or (e.CommandName = "Save" AndAlso Page.IsValid) Then
            If e.CommandName = "Save" Then
                Dim sBaseURL As String = CType(e.Item.FindControl("txtBaseURL"), TextBox).Text.Trim()

                Dim sRewrittenURL As String = String.Empty
                If CType(e.Item.FindControl("rbProduct"), RadioButton).Checked Then
                    Dim ddlProduct As DropDownList = e.Item.FindControl("ddlProduct")
                    sRewrittenURL = Me.VirtualDirectory & "detail.aspx?id=" & ddlProduct.SelectedValue
                ElseIf CType(e.Item.FindControl("rbCategory"), RadioButton).Checked Then
                    Dim ddlCategory As DropDownList = e.Item.FindControl("ddlCategory")
                    sRewrittenURL = Me.VirtualDirectory & "searchresult.aspx?categoryid=" & ddlCategory.SelectedValue
                ElseIf CType(e.Item.FindControl("rbCustom"), RadioButton).Checked Then
                    Dim txtCustom As TextBox = e.Item.FindControl("txtCustom")
                    sRewrittenURL = Me.VirtualDirectory & "searchresult.aspx?keywords=" & txtCustom.Text
                End If

                Me.MappedURLsBusiness.Save(Me.dgMappedURLs.DataKeys(e.Item.ItemIndex), Me.VirtualDirectory & sBaseURL & FileExt, sRewrittenURL)
            Else
                ' note: if adding a row is cancelled, set the page index back to the page it was before adding the row
                Me.dgMappedURLs.CurrentPageIndex = Me.ViewState("PageIndex")
            End If
            Me.dgMappedURLs.EditItemIndex = -1
            Me.btnAdd.Visible = True
            Me.BindDataGrid()
        End If
    End Sub

    Private Sub dgMappedURLs_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMappedURLs.ItemDataBound
        If Me.dgMappedURLs.EditItemIndex <> e.Item.ItemIndex Then
            ' note: add a confirmation pop-up to the Delete button when not in edit mode
            Dim btnCheckRequestDelete As LinkButton = CType(e.Item.FindControl("btnDelete"), LinkButton)
            If Not IsNothing(btnCheckRequestDelete) Then
                btnCheckRequestDelete.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Delete This Mapped URL?" & "');")
            End If
        Else
            If Not IsNothing(e.Item.DataItem) Then
                Dim rewrittenURL As String
                If Not IsDBNull(e.Item.DataItem("RewrittenURL")) Then
                    rewrittenURL = e.Item.DataItem("RewrittenURL")
                Else
                    rewrittenURL = String.Empty
                End If
                Dim mapsToURL As New MapsTo(rewrittenURL)
                Dim ddlProduct As DropDownList = CType(e.Item.FindControl("ddlProduct"), DropDownList)
                If Not IsNothing(ddlProduct) Then
                    ddlProduct.DataSource = Me.Products
                    ddlProduct.DataValueField = "ProductID"
                    ddlProduct.DataTextField = "Name"
                    ddlProduct.DataBind()
                End If
                Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
                If Not IsNothing(ddlCategory) Then
                    ddlCategory.DataSource = Me.Categories
                    ddlCategory.DataValueField = "ID"
                    ddlCategory.DataTextField = "Name"
                    ddlCategory.DataBind()
                End If
                Select Case mapsToURL.Type
                    Case MapsToType.Product
                        CType(e.Item.FindControl("rbProduct"), RadioButton).Checked = True
                        ddlProduct.SelectedValue = mapsToURL.Detail
                    Case MapsToType.Category
                        CType(e.Item.FindControl("rbCategory"), RadioButton).Checked = True
                        ddlCategory.SelectedValue = mapsToURL.Detail
                    Case MapsToType.Custom
                        CType(e.Item.FindControl("rbCustom"), RadioButton).Checked = True
                        CType(e.Item.FindControl("txtCustom"), TextBox).Text = mapsToURL.Detail
                End Select
            End If
        End If
    End Sub

    Private Sub dgMappedURLs_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgMappedURLs.PageIndexChanged
        Me.dgMappedURLs.CurrentPageIndex = e.NewPageIndex
        Me.BindDataGrid()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim dsMappedURLs As DataSet = Me.GetMappedURLs()
        dsMappedURLs.Tables(0).Rows.Add(dsMappedURLs.Tables(0).NewRow)
        dsMappedURLs.Tables(0).Rows(dsMappedURLs.Tables(0).Rows.Count - 1)("uid") = 0

        ' note: add one to the page count if we are adding a row that will cause the page count increase
        Dim iPageCount As Integer
        iPageCount = Me.dgMappedURLs.PageCount
        If dsMappedURLs.Tables(0).Rows.Count Mod PageSize = 1 Then
            iPageCount += 1
        End If

        ' note: if there is more than one page, subtract the items from previous pages.  otherwise, do not subtract anything
        Dim iItemsOnPreviousPages As Integer
        If dsMappedURLs.Tables(0).Rows.Count < PageSize Then
            iItemsOnPreviousPages = 0
        Else
            iItemsOnPreviousPages = (iPageCount - 1) * PageSize
        End If

        Me.dgMappedURLs.EditItemIndex = dsMappedURLs.Tables(0).Rows.Count - 1 - iItemsOnPreviousPages
        ' note: store the current page index in case adding the row is cancelled
        Me.ViewState("PageIndex") = Me.dgMappedURLs.CurrentPageIndex
        Me.dgMappedURLs.CurrentPageIndex = iPageCount - 1

        Me.BindDataGrid(dsMappedURLs)
        Me.btnAdd.Visible = False
    End Sub

    Protected Enum MapsToType
        Unknown = 0
        Product
        Category
        Custom
    End Enum

    Protected Class MapsTo
        Public Type As MapsToType = MapsToType.Unknown
        Public Detail As String = String.Empty

        Public Sub New(ByVal url As String)
            Dim regEx As New System.Text.RegularExpressions.Regex("(.*)detail\.aspx\?id=(\d+)")
            Dim match As System.Text.RegularExpressions.Match = regEx.Match(url.ToLower())
            If match.Success Then
                Me.Type = MapsToType.Product
                Me.Detail = regEx.Replace(url, "$2")
            Else
                regEx = New System.Text.RegularExpressions.Regex("(.*)searchresult\.aspx\?categoryid=(\d+)")
                match = regEx.Match(url.ToLower())
                If match.Success Then
                    Me.Type = MapsToType.Category
                    Me.Detail = regEx.Replace(url, "$2")
                Else
                    regEx = New System.Text.RegularExpressions.Regex("(.*)searchresult\.aspx\?keywords=(.*)")
                    match = regEx.Match(url.ToLower())
                    If match.Success Then
                        Me.Type = MapsToType.Custom
                        Me.Detail = regEx.Replace(url, "$2")
                    End If
                End If
            End If
        End Sub
    End Class
End Class
' end: JDB - 4/2/2007 - UrlRewriter Add-On