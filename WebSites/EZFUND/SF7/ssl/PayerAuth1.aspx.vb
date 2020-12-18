Partial Class PayerAuth1
    Inherits System.Web.UI.Page

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
        'Put user code to initialize the page here
        If Not Session("PayerAuthHtml") Is Nothing Then
            Response.Write(Session("PayerAuthHtml"))
            Session.Remove("PayerAuthHtml")
            Return
        End If

        Dim MD As String = Request.Form("MD")
        Dim paRes As String = Request.Form("PaRes")
        Dim sId As String = Request.QueryString("Cardinal")
        Dim myScript1 As String = "<script language='javascript'>"
        myScript1 = myScript1 & " SubmitParent('" & sId & "','" & MD & "','" & paRes & "');"
        myScript1 = myScript1 & "</script>"
        ClientScript.RegisterStartupScript(Me.GetType, "payeauthscript", myScript1)
    End Sub
End Class
