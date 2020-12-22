Option Explicit On 
Imports System.Xml
Imports CSR.CSRBusinessRule
Imports CSR.CSRSystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class CSRSearchControl
    Inherits CSRWebControl

    Private oResult As CCategoryStorage
    Protected WithEvents lblHeaderRowCategoryname As System.Web.UI.WebControls.Label
    Private SearchStorage As CSearchStorage
    Private arResults As ArrayList
    Private m_objSearchEngine As CSearchEngine
    Private CatsAreGrouped As Boolean
    Private RowCount As Integer = 10

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

#Region "Private Sub Page_Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim con As DataGridItem
        GetOrder()
        If Me.Visible = False Then Exit Sub
        dgSearch.PageSize = RowCount
        dgSearch.AllowCustomPaging = True
        '2715 - A
        If IsNothing(ViewState("PageIndex")) = True Then
            ViewState("PageIndex") = Session("PageIndex")
        End If
        'End 
        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then
            Dim arList As New ArrayList

            Dim objCategoryAccess As New CCategories
            arList = objCategoryAccess.ParantCategories
            Dim objCategory As New CCategory
            Dim strCatLabel As String
            strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
            objCategory.ID = -1
            objCategory.Name = "All " & strCatLabel
            arList.Insert(0, objCategory)

            SimpleCategory.DataSource = arList
            SimpleCategory.DataBind()

            SearchStorage = SetSearch()
            If SearchStorage Is Nothing = False Then
                NormalSearch(SearchStorage)
                SetSearchInfo()
            End If
        End If
    End Sub
#End Region

#Region "Private Sub SetSearchInfo()"

    Private Sub SetSearchInfo()
        If (SearchStorage.Keyword.Trim() = "") Then
            lblKeyword.Text = "All"
        Else
            lblKeyword.Text = SearchStorage.Keyword
        End If
        If (SearchStorage.CategoryID = -1) Then
            'Dim strCatLabel As String
            lblCategoryName.Text = "All Categories"
        Else
            'Get Category Name'
            'SP1 Change
            Dim objCategoryAccess As New CSearchResult
            lblCategoryName.Text = objCategoryAccess.GetCategoryName(SearchStorage.CategoryID)
        End If
        If (dgSearch.PageCount = 1) Then
            dgSearch.PagerStyle.Visible = False
        End If
        If (IsNothing(arResults) = False) Then
            lblCount.Text = m_objSearchEngine.ResultsCount & " "
            If (arResults.Count = 1) Then
                lblProducts.Text = "Product"
            Else
                lblProducts.Text = "Products"
            End If
        End If
    End Sub

#End Region


#Region "Public Sub AddCart(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub AddCart(ByVal sender As Object, ByVal e As System.EventArgs)
        Call AddProductToOrder(sender.CommandName.ToString, sender.bindingcontainer)
        If IsError = False Then
            MagicAjax.AjaxCallHelper.Write("ClosePage();")
        End If
    End Sub
#End Region

#Region "Private Sub DGSearch_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGSearch.PageIndexChanged"
    Private Sub DGSearch_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSearch.PageIndexChanged
        'SP 1 Change
        ' m_objSearchEngine = New CSearchEngine(dom.Item("SiteProducts").Item("ProductList"), dom.Item("SiteProducts").Item("CategoryTree"), objStorage.CategoryID)
        SearchStorage = Session("CSRSearch")
        m_objSearchEngine = New CSearchEngine(CSRCustomer.CustomerGroup, RowCount)
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
            dgSearch.AllowCustomPaging = True
        Else
            dgSearch.AllowCustomPaging = False
        End If
        dgSearch.CurrentPageIndex = e.NewPageIndex
        viewstate("PageIndex") = e.NewPageIndex
        '2715 - A 
        Session("PageIndex") = e.NewPageIndex
        'End
        If SearchStorage.CategoryID <> -1 Then
            CatsAreGrouped = True
        End If
        Dim arResult As ArrayList
        arResult = m_objSearchEngine.StandardSearch(SearchStorage, New Hashtable, CatsAreGrouped, e.NewPageIndex)
        dgSearch.DataSource = arResult
        dgSearch.VirtualItemCount = m_objSearchEngine.ResultsCount
        dgSearch.DataBind()
    End Sub
#End Region
    'Protected Function GetCategoryName() As String
    '    Return "Sample return category name"
    'End Function


#Region "Private Sub DGSearch_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGSearch.ItemCreated"
    Private Sub DGSearch_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSearch.ItemCreated
        'Dim objInventoryLink As LinkButton
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton

            If (viewstate("PageIndex") > 0) Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (viewstate("PageIndex") < dgSearch.PageCount - 1) Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        ElseIf (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            If (IsNothing(dom) = False) Then
                oResult = e.Item.DataItem
                Dim CSRMan As New CSRManagement(StoreFrontConfiguration.SiteURL)
                Dim NewPrice As TextBox
                Dim OldPrice As HtmlInputHidden
                NewPrice = CType(e.Item.FindControl("NewPrice"), TextBox)
                OldPrice = CType(e.Item.FindControl("OldPrice"), HtmlInputHidden)
                NewPrice.Text = Format(CDec(GetItemPriceWithoutAttributes(oResult)), "n2")
                OldPrice.Value = Format(CDec(GetItemPriceWithoutAttributes(oResult)), "n2")
                If CSRMan.IsAdvancedCSR = False Then
                    NewPrice.Enabled = False
                Else
                    NewPrice.Enabled = True
                End If
                If (IsNothing(e.Item.FindControl("CAttributeControl1")) = False) Then
                    CType(e.Item.FindControl("CAttributeControl1"), CSRAttributes).Data_Bind(oResult, CSRCustomer) '1521
                    If (e.Item.FindControl("CAttributeControl1").Visible = False) Then
                        If (IsNothing(e.Item.FindControl("AttributeRow")) = False) Then
                            e.Item.FindControl("AttributeRow").Visible = False
                        End If
                    End If

                End If

            End If
        End If

    End Sub
#End Region

#Region "Private Sub DGSearch_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGSearch.ItemCommand"
    Private Sub DGSearch_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSearch.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            If Not (CType(e.CommandSource, LinkButton).ID = "btnVolumePricing") Then
                Dim objButton As LinkButton = e.CommandSource
                SearchStorage = Session("CSRSearch")
                m_objSearchEngine = New CSearchEngine(CSRCustomer.CustomerGroup, RowCount)
                dgSearch.AllowCustomPaging = True
                If (objButton.Text.IndexOf("Next") <> -1) Then
                    dgSearch.CurrentPageIndex = viewstate("PageIndex") + 1
                ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                    dgSearch.CurrentPageIndex = viewstate("PageIndex") - 1
                End If
                viewstate("PageIndex") = dgSearch.CurrentPageIndex
                '2715 - A 
                Session("PageIndex") = dgSearch.CurrentPageIndex
                'End
                If SearchStorage.CategoryID <> -1 Then
                    CatsAreGrouped = True
                End If
                dgSearch.DataSource = m_objSearchEngine.StandardSearch(SearchStorage, New Hashtable, CatsAreGrouped, dgSearch.CurrentPageIndex)
                dgSearch.VirtualItemCount = m_objSearchEngine.ResultsCount
                dgSearch.DataBind()
            End If
        End If
    End Sub
#End Region


    Public Function SetSearch() As CSearchStorage
        SearchStorage = New CSearchStorage
        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then
            SearchStorage.Keyword = Request.QueryString("Keyword")
                SimpleKeyword.Text = SearchStorage.Keyword
                SearchStorage.CategoryID = -1

        Else
            SearchStorage.Keyword = SimpleKeyword.Text.Trim
            If Me.SimpleCategory.SelectedItem.Value <> -1 Then
                SearchStorage.CategoryID = SimpleCategory.SelectedItem.Value
            End If
        End If

        SearchStorage.IsAdvanced = False
        
        If (TestInput(SearchStorage.Keyword) <> "") Then
            SearchStorage.Keyword = TestInput(SearchStorage.Keyword)
        Else
            SearchStorage.Keyword = SearchStorage.Keyword
        End If

        If (SearchStorage.Keyword <> "" Or SearchStorage.CategoryID <> -1) Then
            SearchStorage.SearchType = SearchType.AnyWords
            Return SearchStorage
        Else
            If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = True Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please Enter Search Criteria")
            End If
            Return Nothing
            End If
    End Function
    Protected Sub NormalSearch(ByVal obj As CSearchStorage)
        'Category
        SearchStorage = obj
        'sp1 change
        ' m_objSearchEngine = New CSearchEngine(dom.Item("SiteProducts").Item("ProductList"), dom.Item("SiteProducts").Item("CategoryTree"), objStorage.CategoryID)
        If SearchStorage.CategoryID <> -1 Then
            CatsAreGrouped = True
        End If
        m_objSearchEngine = New CSearchEngine(CSRCustomer.CustomerGroup, RowCount)
        arResults = m_objSearchEngine.StandardSearch(SearchStorage, New Hashtable, CatsAreGrouped, viewstate("PageIndex"))
        If arResults.Count = 0 Then
            CType(Me.Parent.FindControl("ErrorMessage"), Label).Text = "No Products Found"
        End If

            Session("CSRSearch") = SearchStorage
            If (IsNothing(Me.FindControl("dgsearch")) = False) And arResults.Count > 0 Then
            Dim dg1 As DataGrid = Me.FindControl("DGSearch")
                dg1.AllowCustomPaging = True
                dg1.VirtualItemCount = m_objSearchEngine.ResultsCount
                dg1.CurrentPageIndex = viewstate("PageIndex")
                dg1.DataSource = arResults
                dg1.DataBind()
                
            End If
    End Sub
    Private Function TestInput(ByVal strValue As String) As String
        Dim bReSubmit As Boolean = False
        Dim nIndex As Integer = 0

        While (True)
            nIndex = strValue.IndexOf("'", nIndex)

            If (nIndex > -1) Then
                If (nIndex = strValue.Length - 1) Then
                    bReSubmit = True
                    Exit While
                End If

                If (strValue.Chars(nIndex + 1) <> "'") Then
                    bReSubmit = True
                    Exit While
                Else
                    nIndex = nIndex + 2
                End If
            Else
                Exit While
            End If
        End While

        If (bReSubmit) Then
            Return strValue.Replace("'", "")
        Else
            Return ""
        End If
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SearchStorage = SetSearch()
        If SearchStorage Is Nothing = False Then
            NormalSearch(SearchStorage)
            SetSearchInfo()
        End If
    End Sub

 
    Private Sub dgSearch_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSearch.ItemDataBound

    End Sub

    'begin: GJV - 7/31/2007 - CSR
    Public Function AllowOverridePricing() As Boolean

        Dim CSRMgmt As New CSRManagement(StoreFrontConfiguration.SiteURL)
        Dim CSRUserMgmt As New CSRUserManagement
        Dim CSRUser As CSRUser = CSRUserMgmt.GetUser(Session.Item("CSRUID"))

        Return CSRMgmt.IsAdvancedCSR And CSRUser.OverridePricing

    End Function
    'end: GJV - 7/31/2007 - CSR

End Class

