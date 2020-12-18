Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Public MustInherit Class PackingSlipAddress
    Inherits System.Web.UI.UserControl
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents AddressLabel1 As AddressLabel
    Protected WithEvents lblNickName As System.Web.UI.WebControls.Label
    Protected WithEvents AddressLabel2 As AddressLabel

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
    Public WriteOnly Property BillAddress() As Address
        Set(ByVal Value As Address)
            AddressLabel1.AddressSource = Value

        End Set
    End Property

    Public WriteOnly Property ShipAddress() As Address
        Set(ByVal Value As Address)
            lblNickName.Text = Value.NickName
            AddressLabel2.AddressSource = Value
        End Set
    End Property
End Class
