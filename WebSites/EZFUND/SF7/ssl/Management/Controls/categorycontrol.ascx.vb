Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Partial  Class categorycontrol
    Inherits CWebControl
#Region "Class Members"
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected objCategory As CXMLCategoryList
    Protected objCategoryAccess As CCategories
    Protected ar As ArrayList
#End Region

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

#Region "Events"
    Event Add As EventHandler
    Event Edit As EventHandler
    Event Delete As EventHandler
#End Region

#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        DataGrid1.AllowPaging = True
        DataGrid1.PagerStyle.CssClass = "Content"

        DataGrid1.GridLines = GridLines.Horizontal
        ar = New ArrayList()
        objCategory = New CXMLCategoryList()
        objCategoryAccess = New CCategories()
        Reload()

        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        Else
            DataGrid1.PagerStyle.Visible = True
        End If

    End Sub
#End Region

#Region "Sub Reload()"
    Public Sub Reload()
        ar.Clear()
        ar = objCategoryAccess.GetAllCategories
        DataGrid1.DataSource = ar
        '2417
        If DataGrid1.CurrentPageIndex <> 0 AndAlso ar.Count / DataGrid1.CurrentPageIndex <= DataGrid1.PageSize Then
            DataGrid1.CurrentPageIndex -= 1
        End If
        '2417
        DataGrid1.DataBind()
    End Sub
#End Region

#Region "Sub cmdAdd_click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub cmdAdd_click(ByVal sender As Object, ByVal e As System.EventArgs)
        objCategory = objCategoryAccess.GetCategory(CLng(CType(sender, LinkButton).CommandArgument))
        RaiseEvent Add(objCategory, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub cmdEdit_click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub cmdEdit_click(ByVal sender As Object, ByVal e As System.EventArgs)
        objCategory = objCategoryAccess.GetCategory(CLng(CType(sender, LinkButton).CommandArgument))
        RaiseEvent Edit(objCategory, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub cmdDelete_click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub cmdDelete_click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim catID As String
        Dim str As String
        ar.Clear()
        catID = CType(sender, LinkButton).CommandArgument
        objCategoryAccess.GetChildCategories(CLng(catID), ar)

        ar.Add(catID)
        For Each str In ar
            objCategoryAccess.SetToNoCategory(CLng(str))
        Next
        objCategoryAccess.DeleteCategoryByID(CLng(catID))
        RaiseEvent Delete(sender, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged"
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.DataSource = objCategoryAccess.GetAllCategories
        DataGrid1.DataBind()
    End Sub
#End Region



    Public Function BuildDeleteString(ByVal CatID As String) As String
        Dim strDelete As String
        Dim ar As New ArrayList()
        Dim ar2 As New ArrayList()
        Dim ar3 As New ArrayList()
        Dim nTemp As Long
        Dim nProdID As Long
        objCategoryAccess.GetChildCategories(CLng(CatID), ar)
        For Each nTemp In ar
            ar2 = objCategoryAccess.GetAllProductsByCategory(CLng(nTemp))
            If Not (IsNothing(ar2)) Then
                For Each nProdID In ar2
                    If Not (ar3.Contains(nProdID)) Then
                        ar3.Add(nProdID)
                    End If
                Next
            End If
        Next

        If ar.Count > 1 Then
            strDelete = "There are " & ar.Count & " subcategories that will be deleted."
        ElseIf ar.Count = 1 Then
            strDelete = "There is one subcategory that will be deleted."
        Else
            strDelete = "Are you sure you want to delete this category?"
        End If
        If Not (IsNothing(ar3)) Then
            If ar3.Count > 1 Then
                strDelete = strDelete & "\nThere are also " & ar3.Count & " products that will be put into no category."
            ElseIf ar3.Count = 1 Then
                strDelete = strDelete & "\nThere is also 1 product that will be assigned to no category."
            End If
        End If
        strDelete = strDelete & "\nContinue?"
        Return strDelete
    End Function

    Public Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If Not (IsNothing(e.Item.DataItem)) Then
            CType(e.Item.FindControl("cmdDelete"), LinkButton).Attributes.Add("onclick", "javascript:return ConfirmCancel('" & BuildDeleteString(CType(e.Item.DataItem, CXMLCategoryList).ID) & "');")
        End If

    End Sub
End Class
