Imports System
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Public MustInherit Class addcategory
    Inherits CWebControl
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtLevelHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected objCategory As CXMLCategoryList
    Protected WithEvents txtParentIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
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

#Region "Events"
    Event Save As EventHandler
    Event Cancel As EventHandler
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objCategory = New CXMLCategoryList()

    End Sub
#End Region

    Public Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        FillCategory()
        objCategoryAccess = New CCategories()
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
        objCategoryAccess.AddCategory(objCategory)
        RaiseEvent Save(objCategory, EventArgs.Empty)
    End Sub

#Region "Sub FillFields(ByVal category As CXMLCategoryList)"
    Public Sub FillFields(ByVal category As CXMLCategoryList)
        Me.txtLevelHidden.Value = CLng(category.Level) + 1
        Me.txtParentIDHidden.Value = category.ID
        Me.txtName.Text = ""
    End Sub
#End Region

    Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        CType(Parent.FindControl("lblErrorMessage"), Label).Text = ""
        CType(Parent.FindControl("lblErrorMessage"), Label).Visible = False
        RaiseEvent Cancel(objCategory, EventArgs.Empty)
    End Sub


#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtName.Text = ""
        txtLevelHidden.Value = 0
        txtParentIDHidden.Value = 0
    End Sub
#End Region

#Region "Sub FillCategory()"
    Public Sub FillCategory()
        objCategory.Level = CLng(txtLevelHidden.Value)
        objCategory.Name = txtName.Text
        objCategory.ParentID = txtParentIDHidden.Value
    End Sub
#End Region

End Class
