Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports StoreFront.Integration

Partial Class wsunsentorders
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
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        Catch ex As Exception
            Session("DetailError") = "Class WSMangagement Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub DataBindPage()
        Dim report As New CSFReports
        rptMain.DataSource = report.GetBadIntegrationOrders().Tables(0)
        rptMain.DataBind()
    End Sub
    Public Sub lbtnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim orderHanlder As New NewOrderHandler
            orderHanlder.NewOrderGenerated(String.Empty, CType(sender, LinkButton).CommandArgument)
            DataBindPage()
        Catch ex As Exception
            Session("DetailError") = "Class WSMangagement Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Shared Sub SendAllOrders()
        Dim report As New CSFReports
        Dim orderHanlder As New NewOrderHandler
        For Each dRow As Data.DataRow In report.GetBadIntegrationOrders().Tables(0).Rows
            orderHanlder.NewOrderGenerated(String.Empty, dRow("OrderNumber"))
        Next
    End Sub
    Private Sub lbtnSendAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSendAll.Click
        Try
            SendAllOrders()
        Catch ex As Exception
            Session("DetailError") = "Class WSMangagement Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        DataBindPage()
    End Sub
End Class
