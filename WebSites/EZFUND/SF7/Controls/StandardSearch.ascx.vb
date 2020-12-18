Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class StandardSearch
    Inherits CWebControl


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

        If (IsPostBack = False) Then
             Dim arList As New ArrayList()
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                Dim objCategoryAccess As CXMLCategoryAccess = StoreFrontConfiguration.CategoryAccess
                arList = objCategoryAccess.GetLevelWChildren(0)
                Dim objCategory As New CXMLCategoryList()
                Dim strCatLabel As String
                strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
                objCategory.ID = -1
                objCategory.Name = "All " & strCatLabel
                arList.Insert(0, objCategory)
            Else
                Dim objCategoryAccess As New CCategories()
                arList = objCategoryAccess.ParantCategories
                Dim objCategory As New CCategory()
                Dim strCatLabel As String
                strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
                objCategory.ID = -1
                objCategory.Name = "All " & strCatLabel
                arList.Insert(0, objCategory)
            End If

            SimpleCategory.DataSource = arList
            SimpleCategory.DataBind()
        End If
        imgSearch.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Search").Attributes("Filepath").Value

    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objStorage As New CSearchStorage()
        objStorage.Keyword = SimpleKeyword.Text
        objStorage.CategoryID = SimpleCategory.SelectedItem.Value
        If (SimpleKeywordGroup1.Checked = True) Then
            objStorage.SearchType = SearchType.AnyWords
        ElseIf (SimpleKeywordGroup2.Checked = True) Then
            objStorage.SearchType = SearchType.ExactPhrase
        ElseIf (SimpleKeywordGroup3.Checked = True) Then
            objStorage.SearchType = SearchType.AllWords
        End If

        Session("Search") = objStorage
        Response.Redirect("SearchResult.aspx")
    End Sub

End Class
