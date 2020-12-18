Imports StoreFront.SystemBase

Partial Class Help
    Inherits StoreFront.CWebPage

    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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
        Try
            SetPageTitle = m_objMessages.GetXMLMessage("Help.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)
        Catch ex As Exception
            Session("DetailError") = "Class GiftWrap Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
