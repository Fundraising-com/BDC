Partial  Class TrackingForOrder
    Inherits CWebControl


    Private m_objOrderHistory As BusinessRule.Orders.COrders
    Private m_objOrder As BusinessRule.Orders.COrder
    'Private m_sOrderID As String
    Private m_sAddressID As String


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
        'Dim objOrder As BusinessRule.Orders.COrder
        If Not IsPostBack Then
            'm_objOrderHistory = Session("OrderHistory")

            'For Each objOrder In m_objOrderHistory.Orders
            'If (objOrder.UID = m_sOrderID) Then
            'm_objOrder = objOrder
            ''   lblOrderID.Text = m_objOrder.OrderNumber
            ' ' lblOrderDate.Text = m_objOrder.OrderDate
            'Exit For
            'End If
            'Next
            Bind()
            Dim TrackingForOrderAddress As TrackingForOrderAddress
            Dim item As DataListItem
            UPS.visible = False
            For Each item In DataList1.Items
                TrackingForOrderAddress = CType(item.FindControl("TrackingForOrderAddress1"), TrackingForOrderAddress)
                If TrackingForOrderAddress.UsesUPS = True Then
                    UPS.visible = True
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Bind()
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim tempAddress As BusinessRule.Orders.COrderAddress
        'Dim tempDisplay As String
        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("NickName", GetType(String)))
        'm_objOrder.LoadOrderAddresses()
        For Each tempAddress In m_objOrder.OrderAddresses
            If (m_sAddressID = "" Or tempAddress.Address.ID.ToString = m_sAddressID) Then
                dr = dt.NewRow()
                dr(0) = tempAddress.Address.ID
                dr(1) = tempAddress.Address.NickName
                dt.Rows.Add(dr)
            End If
        Next
        DataList1.DataSource = dt
        DataList1.DataBind()
    End Sub

    Public Sub writeUPS()
        Dim TrackingForOrderAddress As TrackingForOrderAddress
        Dim item As DataListItem
        UPS.visible = False
        For Each item In DataList1.Items
            TrackingForOrderAddress = CType(item.FindControl("TrackingForOrderAddress1"), TrackingForOrderAddress)
            If TrackingForOrderAddress.UsesUPS = True Then
                UPS.Visible = True
                'Response.Write("<table width='100%' border=0>")
                'Response.Write("<tr>")
                'Response.Write("<td class='Content' width='1' align=left valign=top><img src='images/LOGO_S.gif'></td>")
                'Response.Write("<td class='Content' align=left valign=top>NOTICE: UPS tracking systems and the information they contain are the private property of UPS and may be used solely to track shipments tendered by, to or for you to UPS for delivery and for no other purpose. Any other use of UPS tracking systems and information is strictly prohibited.</td>")
                'Response.Write("</tr>")
                'Response.Write("</table>")
                Exit For
            End If
        Next

    End Sub


End Class
