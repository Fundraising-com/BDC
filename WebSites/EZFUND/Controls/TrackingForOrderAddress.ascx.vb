Public MustInherit Class TrackingForOrderAddress
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

    Private m_objTracking As BusinessRule.Orders.COrderTracking
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Private m_sAddressID As String
    Private m_objOrderHistory As BusinessRule.Orders.COrders
    Private m_objOrder As BusinessRule.Orders.COrder
    Private m_bUsesUPS As Boolean
    'Private m_sOrderID As String

    Public Property UsesUPS() As Boolean
        Get
            Return m_bUsesUPS
        End Get

        Set(ByVal Value As Boolean)
            m_bUsesUPS = Value
        End Set
    End Property

    Public Property Order() As BusinessRule.Orders.COrder
        Get
            Return m_objOrder
        End Get

        Set(ByVal Value As BusinessRule.Orders.COrder)
            m_objOrder = Value
        End Set
    End Property


    Public Property AddressID() As String
        Get
            Return m_sAddressID
        End Get

        Set(ByVal Value As String)
            m_sAddressID = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim objOrder As BusinessRule.Orders.COrder
        If Not IsPostBack Then
            'm_objOrderHistory = Session("OrderHistory")

            'For Each objOrder In m_objOrderHistory.Orders
            '    If (objOrder.UID = m_sOrderID) Then
            '        m_objOrder = objOrder
            '        '   lblOrderID.Text = m_objOrder.OrderNumber
            '        '  lblOrderDate.Text = m_objOrder.OrderDate
            '        Exit For
            '    End If
            'Next
            m_objTracking = New BusinessRule.Orders.COrderTracking(m_objOrder)
            m_objTracking.OrderAddressID = m_sAddressID
            m_objTracking.getTracking()
            m_bUsesUPS = m_objTracking.UsesUPS
            DataList1.DataSource = m_objTracking.TrackingInfo
            DataList1.DataBind()
        End If
    End Sub

End Class
