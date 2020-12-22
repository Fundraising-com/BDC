Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

Partial Class LanguageCurrency
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents LanguageCurrency As LanguageCurrencyControl
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
        'Put user code to initialize the page here
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.Localization) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        CType(LanguageCurrency.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
    End Sub

End Class
