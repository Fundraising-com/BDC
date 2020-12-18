Imports StoreFront.Systembase
Imports StoreFront.BusinessRule

Partial  Class PeriodSalesControl
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
    Public Sub SetDisplay(ByVal objPeriodSales As CReportPeriodSales)
        lblMerchandise.Text = Format(objPeriodSales.MerchandiseTotals, "c")
        lblTotalDisCounts.Text = "-" & Format(objPeriodSales.DiscountsAppliedTotals, "c")
        lblLocalTax.Text = Format(objPeriodSales.LocalTaxTotals, "c")
        lblStateTax.Text = Format(objPeriodSales.StateTaxTotals, "c")
        lblCountryTax.Text = Format(objPeriodSales.CountryTaxTotals, "c")
        lblOrderTotals.Text = Format(objPeriodSales.OrderTotals, "c")
        lblShipFees.Text = Format(objPeriodSales.ShippingFeeTotals, "c")
        lblHandling.Text = Format(objPeriodSales.HandlingTotals, "c")
        lblGiftCert.Text = "-" & Format(objPeriodSales.GiftCertificateTotals, "c")
    End Sub
End Class
