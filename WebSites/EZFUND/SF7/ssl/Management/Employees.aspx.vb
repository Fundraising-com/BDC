Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports CSR.CSRSystemBase
Imports System.Xml
Partial Class Employees
    Inherits CWebPage
    Protected WithEvents AddEmployee1 As addemployee
    Protected WithEvents Employeecontrol1 As employeecontrol
    Protected objuser As CSRUser
    Public mode As String



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

        'begin: GJV - 7/31/2007 - CSR
        'note: only allow those that have purchased CSR to view this page
        Me.CSRLicenseCheck()
        'end: GJV - 7/31/2007 - CSR

        AddEmployee1.Visible = False
        btnAdd.Visible = True
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

        Catch ex As Exception
            Session("DetailError") = "Class ProductImport Error=" & ex.Message
            Response.Redirect(SystemBase.StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub


    Private Sub AddEmployee1_Add(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddEmployee1.Add
        If (sender = True) Then
            Employeecontrol1.Reload()
            Employeecontrol1.Visible = True
            AddEmployee1.Visible = False
            btnAdd.Visible = True
        Else
            AddEmployee1.Visible = True
            btnAdd.Visible = False
        End If
        ErrorMessage.Text = Me.AddEmployee1.Message
        ErrorMessage.Visible = True
    End Sub

    Private Sub Employeecontrol1_Delete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Employeecontrol1.Delete
        ErrorMessage.Text = "User Deleted"
        ErrorMessage.Visible = True
        Response.Redirect(Request.Url.ToString)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Employeecontrol1.Visible = False
        btnAdd.Visible = False
        ErrorMessage.Visible = False
        Session(mode) = "Add"
        AddEmployee1.ClearFields()
        AddEmployee1.Visible = True
    End Sub

    Private Sub Employeecontrol1_Edit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Employeecontrol1.Edit
        Employeecontrol1.Visible = False
        btnAdd.Visible = False
        ErrorMessage.Visible = False
        Session(mode) = "Edit"
        AddEmployee1.uid = sender
        AddEmployee1.BindMenuOptions(sender)
        AddEmployee1.Visible = True
    End Sub

    Private Sub CSRForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CSRForm.Click
        Response.Redirect(StoreFrontConfiguration.SSLPath & "csr/OrderForm.aspx")
    End Sub
End Class
