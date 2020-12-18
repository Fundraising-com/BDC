Imports System.Web.Security
Imports System.Text
Partial Class LoginPage
    Inherits CWebPage
    'Inherits System.Web.UI.Page

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

        'begin: GJV - 7/31/2007 - CSR
        'note: only allow those that have purchased CSR to view this page
        Me.CSRLicenseCheck()
        'end: GJV - 7/31/2007 - CSR

        imgSignIn.ImageUrl = "images/Submit.jpg"
        SetEnterKeyPostBack(Me.txtPass, Me.btnSignIn)
    End Sub
    '    Private Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "", Optional ByVal condition As String = "")
    '
    '        If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
    '            Dim script As New StringBuilder
    '            script.Append("if (isEnterKey(event)")
    '            If Not IsNothing(condition) AndAlso condition <> String.Empty Then
    '                script.Append(" && " & condition)
    '            End If
    '            script.Append(") ")
    '            script.Append("postBack(event, '" & objButton.UniqueID & "', '" & argument & "')")
    '            objTextControl.Attributes.Add("onKeyDown", script.ToString)
    '        End If
    '
    '    End Sub
    Private Sub btnSignIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignIn.Click
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from Employees where UserName=" & "'" & txtUser.Text & "'", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        ResetForm()
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Password") = txtPass.Text Then
                Session.Item("CSRUserName") = txtUser.Text
                Session.Item("CSRUID") = dt.Rows(0).Item("UID")
                FormsAuthentication.RedirectFromLoginPage(txtUser.Text, False)
            Else
                ErrorMessage.Text = "Invalid Password"
                ErrorMessage.Visible = True
            End If
        Else
            ErrorMessage.Text = "Invalid User"
            ErrorMessage.Visible = True
        End If
    End Sub
    Private Sub ResetForm()
        Session("NewCust") = 0
        Session("CSRNewProduct") = Nothing
        Session("CSROrder") = Nothing
        Session("CSRCustomer") = Nothing
        Session("CSRSelectedCustomer") = Nothing
        Session("CSRBillAddress") = "-1"
        Session("CSRShipAddress") = "-1"
        Session("CSRFirstName") = ""
        Session("CSRLastName") = ""
        Session("CSREmail") = ""

    End Sub
End Class
