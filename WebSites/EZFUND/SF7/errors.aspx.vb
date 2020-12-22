Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml

Partial Class Errors
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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
        Try
            SetPageTitle = m_objMessages.GetXMLMessage("default.aspx", "PageTitle", "Title")

            SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            If IsNothing(Session("DetailError")) = False Then
                ErrMsg.Text = Session("DetailError")
                Session("DetailError") = Nothing
            End If
        Catch err As SystemException
            ErrMsg.Text = err.Message
        End Try
        
    End Sub
End Class
