Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management

Partial  Class AddressLabel
    Inherits CWebControl

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

    Private m_strWidth As String
    Private _dataSource As New ArrayList()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Public Property AddressWidth() As String
        Get
            Return m_strWidth
        End Get
        Set(ByVal Value As String)
            m_strWidth = Value
        End Set
    End Property

    Public ReadOnly Property AddressID() As Long
        Get
            Return CType(_dataSource.Item(0), Address).ID
        End Get
    End Property

    Public WriteOnly Property AddressSource() As Address
        Set(ByVal Value As Address)
            _dataSource.Add(Value)
            'update #242
            Dim Addr As Address = _dataSource.Item(0)
            Dim local As New CLocalization
            Dim CName As String = local.getCountryName(Addr.Country)
            Addr.CountryName = CName
            DataList1.DataSource = _dataSource
            DataList1.DataBind()
        End Set
    End Property

    Private Sub DataList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataList1.SelectedIndexChanged

    End Sub
End Class
