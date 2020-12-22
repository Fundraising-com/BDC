Imports StoreFront.Systembase
Imports StoreFront.BusinessRule

Partial  Class SalesStatsControl
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
    Public Sub SetDisplay(ByVal objPeriodSales As CReportSalesStats)
        lblTotalDays.Text = objPeriodSales.TotalDays
        lblTotalOrders.Text = objPeriodSales.TotalOrders
        lblAveDailyUnits.Text = objPeriodSales.DailyUnitsAverage
        lblAveDailyOrders.Text = objPeriodSales.DailyOrdersAverage
        lblAveDailySales.Text = Format(objPeriodSales.DailySalesAverage, "c")
        lblAvePerOrder.Text = Format(objPeriodSales.PerOrderAverage, "c")
        lblAvePriceperUnit.Text = Format(objPeriodSales.UnitPriceAverage, "c")
    End Sub
End Class
