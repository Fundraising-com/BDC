Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Public MustInherit Class editcategory
    Inherits CWebControl
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
    Protected objCategory As CXMLCategoryList
    Protected objCategoryAccess As CCategories
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
    Event Save As EventHandler
    Event Cancel As EventHandler
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objCategory = New CXMLCategoryList()
        objCategoryAccess = New CCategories()
    End Sub

#Region "Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdAdd.Click"
    Public Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        FillCategory()
        Dim ar As ArrayList
        ar = objCategoryAccess.GetAllCategories()
        Dim cat As CXMLCategoryList
        For Each cat In ar
            If cat.Name.ToLower = objCategory.Name.ToLower And cat.ParentID = 0 And objCategory.ParentID = 0 Then
                CType(Parent.FindControl("lblErrorMessage"), Label).Text = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("addcategory", "Error", "Duplicate")
                CType(Parent.FindControl("lblErrorMessage"), Label).Visible = True
                Exit Sub
            End If
        Next
        CType(Parent.FindControl("lblErrorMessage"), Label).Text = ""
        CType(Parent.FindControl("lblErrorMessage"), Label).Visible = False
        objCategoryAccess.UpdateCategory(objCategory)
        RaiseEvent Save(objCategory, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdCancel.Click"
    Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        CType(Parent.FindControl("lblErrorMessage"), Label).Text = ""
        CType(Parent.FindControl("lblErrorMessage"), Label).Visible = False
        RaiseEvent Cancel(CType(sender, LinkButton).CommandArgument, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub FillFields(ByVal category As CXMLCategoryList)"
    Public Sub FillFields(ByVal category As CXMLCategoryList)
        txtName.Text = category.Name
        txtIDHidden.Value = category.ID
    End Sub
#End Region

#Region "Sub FillCategory()"
    Public Sub FillCategory()
        objCategory.Name = txtName.Text
        objCategory.ID = txtIDHidden.Value

    End Sub
#End Region
End Class
