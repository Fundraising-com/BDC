Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Partial  Class PackingSlipProducts
    Inherits System.Web.UI.UserControl
    Protected WithEvents Qty As System.Web.UI.WebControls.Label
    Protected WithEvents dlProduct As System.Web.UI.WebControls.DataList
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Enum tOrderType
        Normal = 0
        BackOrder = 1
    End Enum
    Dim _OrderType As tOrderType
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
    Public WriteOnly Property Order(ByVal OrderType As tOrderType) As COrderAddress
        Set(ByVal Value As COrderAddress)

            PackingSlipDisplay1.OrderType = OrderType

            PackingSlipDisplay1.DataSource = Value.OrderItems

            PackingSlipDisplay1.DataBind()



        End Set
    End Property

    
End Class
