Partial Class AnonymousLogin
    Inherits CWebControl

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
        Try
            imgContinue.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
            SetEnterKeyPostBack(txtAnonEMail, btnContinue, , "SetValidationAnonymousSignIn()")
            btnContinue.Attributes.Add("onclick", "return SetValidationAnonymousSignIn();")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Session("anonymous") = 1
        m_objcustomer.IsAnonymous = True
        Session("Customer") = m_objCustomer
        M_objcustomer.AddCustomer("", "", txtAnonEMail.Text, "", False)
        m_objXMLCart.CustomerGroup = 0
        Response.Redirect("Shipping.aspx?WebID=" & m_objcustomer.GetSessionID)
    End Sub
End Class
