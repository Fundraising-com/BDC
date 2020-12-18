Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml
Imports StoreFront.BusinessRule.Orders
Imports storefront.businessrule.Management

Public MustInherit Class trackorder
    Inherits CWebControl
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderID As System.Web.UI.WebControls.Label
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblItems As System.Web.UI.WebControls.Label
    Protected WithEvents lblWeight As System.Web.UI.WebControls.Label
    Protected WithEvents lblCarrier As System.Web.UI.WebControls.Label
    Protected WithEvents lblMethod As System.Web.UI.WebControls.Label
    Protected WithEvents AddressLabel1 As AddressLabel
    Protected WithEvents trackorder1 As trackorder
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected m_objOrder As BusinessRule.Orders.COrder
    Private _AddressID As Long
    Private _PendingType As PendingType
    Private m_objOrderHistory As BusinessRule.Orders.corders
    Enum PendingType
        Normal = 0
        BackOrder = 1
    End Enum
    Public Property Order() As BusinessRule.Orders.COrder
        Get
            Return m_objOrder
        End Get
        Set(ByVal Value As BusinessRule.Orders.COrder)
            m_objOrder = Value
        End Set
    End Property

    Public Property AddressID() As Long
        Get
            Return _AddressID
        End Get
        Set(ByVal Value As Long)
            _AddressID = Value
        End Set
    End Property

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
        Dim objManagement As New CManagement()
        Dim objTracking As CManagementShipTracking

        m_objOrder = New COrder()
        m_objOrder.UID = Request.QueryString("OrderID").ToString
        m_objOrder.LoadOrder(0)
        'm_objOrder = Session("objOrder")
        If m_objOrder.OrderAddresses.Count = 0 Then
            m_objOrder.LoadOrderAddresses()
        End If
        'm_objOrder = Session("objOrder")
        Dim objOrderAddress As COrderAddress
        _AddressID = Request.QueryString("OrderAddressID")
        Dim ar As New ArrayList()

        'If Request.QueryString("Type") = "0" Then
        '    _PendingType = PendingType.Normal
        'Else
        '    _PendingType = PendingType.BackOrder
        'End If
        If IsNothing(m_objOrder) Then
            Response.Redirect(Request.UrlReferrer.ToString)
        Else
            If Not IsPostBack Then
                Session("CallPage") = Request.UrlReferrer.ToString

                lblDate.Text = m_objOrder.OrderDate
                lblOrderID.Text = m_objOrder.OrderNumber
                For Each objOrderAddress In m_objOrder.OrderAddresses
                    If objOrderAddress.Address.ID = _AddressID Then
                        Exit For
                    End If
                Next


                lblMethod.Text = objOrderAddress.Address.ShipMethod
                lblCarrier.Text = objOrderAddress.Address.ShipCarrierCode
                AddressLabel1.AddressSource = objOrderAddress.Address
                ' Select Case _PendingType
                'Case PendingType.Normal
                lblItems.Text = CLng(objOrderAddress.ItemsQuantity)
                lblWeight.Text = CDec(objOrderAddress.Weight)
                'objTracking = objManagement.Tracking(_AddressID, 0)
                'With objTracking
                '    Me.txtMsg.Text = .TrackingMessage
                '    Me.txtTrackingNumber.Text = .TrackingNumber
                '    Me.txtSent.Text = .SentDate
                'End With

                'Case PendingType.BackOrder
                'lblItems.Text = objOrderAddress.BackOrderQuantity
                'lblWeight.Text = objOrderAddress.BackOrderWeight

                'objTracking = objManagement.Tracking(_AddressID, 1)
                'With objTracking
                '    Me.txtMsg.Text = .TrackingMessage
                '    Me.txtTrackingNumber.Text = .TrackingNumber
                '    Me.txtSent.Text = .SentDate
                'End With
                'End Select
                'If txtSent.Text = "" Then
                '    txtSent.Text = Format(Now, "d")
                'End If
                'Session("objTracking") = objTracking
                Session("objOrder") = m_objOrder
                ' m_objOrder = objOrder
                DataBind()
            End If
        End If
    End Sub

End Class
