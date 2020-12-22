Imports StoreFront.BusinessRule
Imports System.Web.Security
Imports StoreFront.Integration

Partial Class WSMangCtrl
    Inherits System.Web.UI.UserControl

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            lblMessage.Text = String.Empty
            LoadValues()
        End If
    End Sub

    Private Sub LoadValues()

        Dim intConfig As New Configuration
        txtUserName.Text = intConfig.OutboundUsername
        txtPassword.Text = intConfig.OutboundPassword

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try
            Dim intConfig As New Configuration
            intConfig.OutboundUsername = txtUserName.Text
            intConfig.OutboundPassword = txtPassword.Text
            intConfig.Save()
            lblMessage.Text = "Credentials were successfully saved."
        Catch ex As Exception
            lblMessage.Text = "There was a problem saving the credentials."
        End Try
    End Sub
End Class
