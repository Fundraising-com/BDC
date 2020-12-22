Imports System.Text
Imports storefront.BusinessRule.Orders
Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial  Class CSRConfirmControl
    Inherits CSRWebControl

    Protected BillingAddress As CSRAddressLabel

    Private m_objPaymentMethodAccess As CXMLPaymentMethodAccess
    Private m_objShipMethodAccess As CXMLShipMethodAccess
    Private m_objOrder As COrder

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
        If (Me.Visible = False) Then
            Exit Sub
        End If
        Dim OrderID As String = Request.QueryString("OrderID")
        Dim CustomerID As String = Request.QueryString("CustomerID")
        If (IsNothing(OrderID) = False) Then
            If (OrderID <> "0") Then
                m_objOrder = New COrder
                m_objOrder.UID = OrderID
                m_objOrder.LoadOrder(CustomerID)
                m_objOrder.CustomerID = CustomerID
            Else
                m_objOrder = CType(Me.Page, Confirm1).m_objPFNR
            End If

            TotalDisplay1.OandaISO = Session("ConvertISO")
            TotalDisplay1.OandaRate = CDec(Session("OandaRate"))
            OrderNumber.Text = m_objOrder.OrderNumber
            BillingAddress.AddressSource = m_objOrder.BillAddress
            lblEmail.Text = m_objOrder.BillAddress.EMail
            m_objPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
            lblPaymentMethod.Text = m_objPaymentMethodAccess.GetPaymentMethodName(m_objOrder.PaymentMethod())

            If (m_objOrder.OrderAddresses.Count = 1) Then
                TotalDisplay1.DisplayMerchandiseTotal = True
                TotalDisplay1.DisplayDiscountTotal = True
                TotalDisplay1.DisplaySubTotal = True
                TotalDisplay1.DisplayLocalTaxTotal = True
                TotalDisplay1.DisplayStateTaxTotal = True
                TotalDisplay1.DisplayCountryTaxTotal = True
                TotalDisplay1.DisplayShippingTotal = True
                TotalDisplay1.DisplayHandlingTotal = True
                TotalDisplay1.DisplayOrderTotal = False
                TotalDisplay1.DisplaySubHandlingTotal = False
                TotalDisplay1.DisplayShipmentTotal = False
            Else
                TotalDisplay1.DisplayMerchandiseTotal = False
                TotalDisplay1.DisplayDiscountTotal = False
                TotalDisplay1.DisplaySubTotal = False
                TotalDisplay1.DisplayLocalTaxTotal = False
                TotalDisplay1.DisplayStateTaxTotal = False
                TotalDisplay1.DisplayCountryTaxTotal = False
                TotalDisplay1.DisplayShippingTotal = False
                TotalDisplay1.DisplayHandlingTotal = False
                TotalDisplay1.DisplayCODTotal = False
                TotalDisplay1.DisplayShipmentTotal = True
                TotalDisplay1.DisplaySubHandlingTotal = True
                TotalDisplay1.DisplayOrderTotal = False
            End If

            Datalist2.DataSource = m_objOrder.OrderAddresses
            Datalist2.DataBind()
            TotalDisplay1.DataSource = m_objOrder
            TotalDisplay1.DataBind()
        End If
    End Sub

    Private Sub Datalist2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Datalist2.ItemCreated
        If (IsPostBack = False) Then
            If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
                ' TODO If Order has been FullFilled then
                ' Show DownloadFiles

                If (m_objOrder.OrderAddresses.Count = 1) Then
                    CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).Visible = False
                Else
                    If (IsNothing(e.Item.DataItem) = False) Then
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataSource = e.Item.DataItem
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataBind()
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaISO = Session("ConvertISO")
                        CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaRate = CDec(Session("OandaRate"))
                    End If
                End If
                If (IsNothing(e.Item.DataItem) = False) Then
                    If (IsNothing(m_objShipMethodAccess) = True) Then
                        m_objShipMethodAccess = StoreFrontConfiguration.ShipMethodAccess
                    End If
                    CType(e.Item.FindControl("ShipMethod"), Label).Text = m_objShipMethodAccess.GetShipMethodName(CType(e.Item.DataItem, cOrderAddress).Address.ShipMethod())
                End If
            End If
        End If
    End Sub
End Class
