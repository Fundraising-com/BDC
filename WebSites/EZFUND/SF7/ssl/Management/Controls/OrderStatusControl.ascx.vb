'Imports RMSBusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.Systembase
Imports StoreFront.BusinessRule.Orders


Partial  Class OrderStatusControl
    Inherits System.Web.UI.UserControl

    Protected WithEvents ddSetShipped As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddSetBackOrderShipped As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ShipStatusControl1 As ShipStatusControl
    'Start Code for RMS
    'end Code for RMS
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
        If Not IsPostBack Then
            SetDisplay()

        End If
    End Sub

    Private Sub SetDisplay()
        Dim objManagement As New CManagement(Request.QueryString("OrderId"))
        Dim objOrder As New COrder(CLng(Request.QueryString("OrderId")))


        With objOrder
            '  setBackOrder.CommandName = .CustID
            ' setPrimary.CommandName = .CustID
            ' setBackOrder.CommandArgument = .OrderID
            ' setPrimary.CommandArgument = .OrderID
            If .PaymentsPending Then
                ddPrimaryPayments.SelectedIndex = 1

                cmdSetPrimary.Visible = True
                imgSetPrimary.Visible = True
            Else
                cmdSetPrimary.Visible = False
                imgSetPrimary.Visible = False
                ddPrimaryPayments.SelectedIndex = 0

            End If
            lblOrderID.Text = Request.QueryString("OrderId")
            lblOrderDate.Text = .OrderDate
            If .BOPaymentsPending Then
                ddBackOrderPayments.SelectedIndex = 1

                cmdsetBackOrder.Visible = True
                imgsetBackOrder.Visible = True
            Else
                cmdsetBackOrder.Visible = False
                imgsetBackOrder.Visible = False
                ddBackOrderPayments.SelectedIndex = 0
            End If

            If .BackOrderTotalAmt = 0 Then
                BOPayment.Visible = False
            End If

            lblTotalPaid.Text = Format(.TotalBilledAmt, "c")
            lblRemaining.Text = Format(.BackOrderTotalAmt, "c")

            'Start Code for RMS
            'Dim rmsOrder As New Order
            'Dim RMSStatus As Long = rmsOrder.GetRMSStatusByOrderNumber(CLng(Request.QueryString("OrderId")))
            'Select Case RMSStatus
            '    Case -1
            '        lblRMSStatus.Text = "Error Processing Order"
            '        cmdResetRMS.Visible = True
            '    Case -2
            '        lblRMSStatus.Text = "Invalid Information"
            '        cmdResetRMS.Visible = True
            '    Case -3
            '        lblRMSStatus.Text = "Incomplete Information"
            '        cmdResetRMS.Visible = True
            '    Case 1
            '        lblRMSStatus.Text = "Successfully Processed"
            '        cmdResetRMS.Visible = True
            '    Case 0
            '        lblRMSStatus.Text = "Order not yet Processed"
            '        cmdResetRMS.Visible = False
            'End Select
            'end Code for RMS
            ShipStatusControl1.Set_Display(objManagement.OrderShipStatus(.UID))

        End With

        Session("objOrder") = objOrder
    End Sub




    Public Sub setPayStatus(ByVal PendingType As Integer)
        Dim objManagement As New CManagement()
        Dim lOrderNumber As Long = Request.QueryString("OrderID")

        objManagement.UpdateShipPaymentStatus(lOrderNumber, PendingType)

    End Sub

    Public Sub Process(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = sender
        'Dim sCustID As String = "&CustID=" & obj.CommandName
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/ProcessPayment.aspx?Type=" & obj.CommandName) '?OrderID=" & obj.CommandArgument & sCustID)

    End Sub

    'Start Code for RMS
    '#Region "ResetRMSStatus"
    '    Public Sub ResetRMS(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Dim objOrder As New Order
    '        objOrder.UpdateRMSStatus(CLng(Request.QueryString("OrderId")), 0)
    '        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/OrderStatus.aspx?OrderId=" & CLng(Request.QueryString("OrderId")))
    '    End Sub
    '#End Region
    'End Code for RMS

    Private Sub ddPrimaryPayments_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddPrimaryPayments.SelectedIndexChanged
        Dim dd As DropDownList = sender
        If dd.SelectedItem.Value = 2 Then
            cmdSetPrimary.Visible = False
            imgSetPrimary.Visible = False
        Else
            cmdSetPrimary.Visible = True
            imgSetPrimary.Visible = True
        End If
        setPayStatus(0)
    End Sub

    Private Sub ddBackOrderPayments_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddBackOrderPayments.SelectedIndexChanged
        Dim dd As DropDownList = sender
        If dd.SelectedItem.Value = 2 Then
            cmdsetBackOrder.Visible = False
            imgsetBackOrder.Visible = False
        Else
            cmdsetBackOrder.Visible = True
            imgsetBackOrder.Visible = True
        End If
        setPayStatus(1)
    End Sub

    
End Class
