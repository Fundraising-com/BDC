Imports StoreFront.SystemBase
Partial Class ManageUserRoles
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents AddEditUserRoles1 As AddEditUserRoles
    Protected WithEvents Roles1 As Roles
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "User Control Roles Events"
    Sub Roles_EditClick(ByVal sender As Object, ByVal e As EventArgs) Handles Roles1.EditClick
        'AddEditUserRoles1.RoleId = CLng(sender)
        Session("RoleID") = CLng(sender)
        CType(AddEditUserRoles1.FindControl("hdnTitle"), HtmlInputHidden).Value = "Edit Role"
        AddEditUserRoles1.BindData()
        MakeVisible(AddEditUserRoles1)
    End Sub
    Sub Roles_DeleteClick(ByVal sender As Object, ByVal e As EventArgs) Handles Roles1.DeleteClick
        MakeVisible(Roles1)
    End Sub
    Sub Roles1_AddNew(ByVal sender As Object, ByVal e As EventArgs) Handles Roles1.AddNew
        CType(AddEditUserRoles1.FindControl("hdnTitle"), HtmlInputHidden).Value = "Add Role"
        Session("RoleId") = 0
        AddEditUserRoles1.RoleId = 0
        AddEditUserRoles1.ClearForm()
        AddEditUserRoles1.SetTitle()
        MakeVisible(AddEditUserRoles1)
    End Sub
#End Region

#Region "user Control AddEditUserRoles1 Events"
    Sub AddEditUserRoles1_SaveClick(ByVal Sender As Object, ByVal e As EventArgs) Handles AddEditUserRoles1.SaveClick
        MakeVisible(AddEditUserRoles1)
        If CStr(Sender) <> "" Then
            lblErrorMessage.Text = CStr(Sender)
            lblErrorMessage.Visible = True
        End If
    End Sub
    Sub AddEditUserRoles1_CancelClick(ByVal Sender As Object, ByVal e As EventArgs) Handles AddEditUserRoles1.CancelClick
        MakeVisible(Roles1)
    End Sub

#End Region

#Region "Private Methods"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.ManageRoles) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            If Not IsPostBack Then
                Roles1.BindData()
                MakeVisible(Roles1)
            End If
            lblErrorMessage.Text = ""
        Catch ex As Exception
            Session("DetailError") = "Class Customers Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
    Private Sub MakeVisible(ByVal obj As Object)
        Dim _type As Type

        'Set all objects to not visible
        AddEditUserRoles1.Visible = False
        Roles1.Visible = False
        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region
End Class
