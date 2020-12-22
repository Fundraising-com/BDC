Imports StoreFront.Systembase
Imports StoreFront.BusinessRule

Partial  Class ProductSalesStats
    Inherits System.Web.UI.UserControl

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
    End Sub
    Public Sub SetDisplay(ByVal objStats As CReportSalesStats)
        DlBest.DataSource = objStats.BestSelling
        DlBest.DataBind()

        dlWorst.DataSource = objStats.WorstSelling
        dlWorst.DataBind()


    End Sub
End Class
