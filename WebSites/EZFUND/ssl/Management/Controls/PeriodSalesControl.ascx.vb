Imports StoreFront.Systembase
Imports StoreFront.BusinessRule

Public MustInherit Class PeriodSalesControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents lblMerchandise As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalDisCounts As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocalTax As System.Web.UI.WebControls.Label
    Protected WithEvents lblStateTax As System.Web.UI.WebControls.Label
    Protected WithEvents lblCountryTax As System.Web.UI.WebControls.Label
    Protected WithEvents lblShipFees As System.Web.UI.WebControls.Label
    Protected WithEvents lblGiftCert As System.Web.UI.WebControls.Label
    Protected WithEvents lblHandling As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderTotals As System.Web.UI.WebControls.Label

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
