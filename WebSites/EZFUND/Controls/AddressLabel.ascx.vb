Imports StoreFront.SystemBase

Public MustInherit Class AddressLabel
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
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
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
            DataList1.DataSource = _dataSource
            DataList1.DataBind()
        End Set
    End Property

    Private Sub DataList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataList1.SelectedIndexChanged

    End Sub
End Class
