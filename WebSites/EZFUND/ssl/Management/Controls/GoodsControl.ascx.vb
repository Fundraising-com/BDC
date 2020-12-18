Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Public MustInherit Class GoodsControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents lblCost As System.Web.UI.WebControls.Label
    Protected WithEvents lblProfit As System.Web.UI.WebControls.Label

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
    Public Sub SetDisplay(ByVal objGoodsStat As CGoodsStat)
        lblCost.Text = Format(objGoodsStat.MerchandiseCost, "c")
        lblProfit.Text = Format(objGoodsStat.Profit, "c")
    End Sub
End Class
