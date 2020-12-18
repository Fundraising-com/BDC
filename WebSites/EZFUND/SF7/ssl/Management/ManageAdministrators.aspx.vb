Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Partial Class ManageAdministrators
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents AddEditAdmin1 As AddEditAdmin
    Protected WithEvents AdminLists1 As AdminLists

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
        'Put user code to initialize the page here
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.ManageAdministrators) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            lblErrorMessage.Text = ""
            If Not IsPostBack Then
                AdminLists1.BindData()
                MakeVisible(AdminLists1)
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ManageAdministrators Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
    Private Sub MakeVisible(ByVal obj As Object)
        Dim _type As Type

        'Set all objects to not visible
        AdminLists1.Visible = False
        AddEditAdmin1.Visible = False
        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#Region "ADDEditAdmin1 Events"
    Sub AddEditAdmin1_Saveclick(ByVal sender As Object, ByVal e As EventArgs) Handles AddEditAdmin1.SaveClick
        If CStr(sender) <> "" Then
            lblErrorMessage.Visible = True
            lblErrorMessage.Text = CStr(sender)
        End If
        MakeVisible(AddEditAdmin1)
    End Sub
    Sub AddeditAdmin1_Errormsg(ByVal str As String) Handles AddEditAdmin1.errorMsg
        lblErrorMessage.Visible = True
        lblErrorMessage.Text = str
        MakeVisible(AddEditAdmin1)
    End Sub
    Sub AddeditAdmin1_CancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles AddEditAdmin1.CancelClick
        MakeVisible(AdminLists1)
        AdminLists1.BindData()
    End Sub
#End Region

#Region "AdminLists1 events"
    Sub AdminLists1_Editclick(ByVal sender As Object, ByVal e As EventArgs) Handles AdminLists1.EditClick
        CType(AddEditAdmin1.FindControl("hdnAdminId"), HtmlInputHidden).Value = CLng(sender)
        CType(AddEditAdmin1.FindControl("hdnTitle"), HtmlInputHidden).Value = "Edit User"
        AddEditAdmin1.BindData()
        MakeVisible(AddEditAdmin1)
    End Sub
    Sub AdminLists1_Deleteclick(ByVal sender As Object, ByVal e As EventArgs) Handles AdminLists1.DeleteClick
        MakeVisible(AdminLists1)
    End Sub
    Sub AdminLists1_AddNew(ByVal sender As Object, ByVal e As EventArgs) Handles AdminLists1.AddNew
        CType(AddEditAdmin1.FindControl("hdnAdminId"), HtmlInputHidden).Value = 0
        CType(AddEditAdmin1.FindControl("hdnTitle"), HtmlInputHidden).Value = "Add User"
        AddEditAdmin1.BindData()
        MakeVisible(AddEditAdmin1)
    End Sub
#End Region
End Class
