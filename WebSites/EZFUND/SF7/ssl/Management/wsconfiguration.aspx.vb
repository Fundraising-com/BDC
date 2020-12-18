Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class wsconfiguration
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable

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
		'begin: GJV - 9/7/2007 - Component License
        If Not StoreFrontConfiguration.AreWebServicesActive Then
            Response.Redirect(String.Format("{0}Management/PurchaseStoreFrontConnector.aspx", StoreFrontConfiguration.SSLPath))
        End If
        'end: GJV-  9/7/2007 - OSP merge
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        Catch ex As Exception
            Session("DetailError") = "Class WSConfiguration Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
