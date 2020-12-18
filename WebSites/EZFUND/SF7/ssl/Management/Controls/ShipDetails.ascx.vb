
Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.management
Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Partial  Class ShipDetails
    Inherits System.Web.UI.UserControl
    Protected WithEvents AddressLabel1 As AddressLabel
    Private _AddressID As Long
    Private _PendingType As PendingType
    Enum PendingType
        Normal = 0
        BackOrder = 1
    End Enum
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

    'TODO Missing Images?
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objManagement As New CManagement()
        Dim objTracking As CManagementShipTracking = Nothing
        Dim objOrder As COrder = Session("objOrder")
        Dim objOrderAddress As COrderAddress = Nothing
        _AddressID = Request.QueryString("Address")
        Dim ar As New ArrayList()
        If Request.QueryString("Type") = "0" Then
            _PendingType = PendingType.Normal
        Else
            _PendingType = PendingType.BackOrder
        End If
        If IsNothing(objOrder) Then
            Response.Redirect(Request.UrlReferrer.ToString)
        Else
            If Not IsPostBack Then
                Session("CallPage") = Request.UrlReferrer.ToString

                lblDate.Text = objOrder.OrderDate
                lblOrderID.Text = objOrder.OrderNumber
                For Each objOrderAddress In objOrder.OrderAddresses
                    If objOrderAddress.Address.ID = _AddressID Then
                        Exit For
                    End If
                Next


                lblMethod.Text = objOrderAddress.Address.ShipMethod
                lblCarrier.Text = objOrderAddress.Address.ShipCarrierCode
                AddressLabel1.AddressSource = objOrderAddress.Address
                lblEmail.Text = objOrderAddress.Address.EMail
                Select Case _PendingType
                    Case PendingType.Normal
                        Dim lQty As Long = objOrderAddress.ItemsQuantity - objOrderAddress.BackOrderQuantity
                        If lQty < 0 Then
                            lQty = 0
                        End If
                        lblItems.Text = CStr(lQty)
                        lblWeight.Text = objOrderAddress.Weight
                        objTracking = objManagement.Tracking(_AddressID, 0)
                        With objTracking
                            Me.txtMsg.Text = .TrackingMessage
                            Me.txtTrackingNumber.Text = .TrackingNumber
                            Me.txtSent.Text = .SentDate
                        End With

                    Case PendingType.BackOrder
                        lblItems.Text = objOrderAddress.BackOrderQuantity
                        lblWeight.Text = objOrderAddress.BackOrderWeight

                        objTracking = objManagement.Tracking(_AddressID, 1)
                        With objTracking
                            Me.txtMsg.Text = .TrackingMessage
                            Me.txtTrackingNumber.Text = .TrackingNumber
                            Me.txtSent.Text = .SentDate
                        End With
                End Select
                If txtSent.Text = "" Then
                    txtSent.Text = Format(Now, "d")
                End If

                Session("objTracking") = objTracking
                Session("objOrder") = objOrder
            End If
        End If
    End Sub

    Private Function ValidateForm() As Boolean


        If Trim(txtTrackingNumber.Text) = "" Then
            Me.ErrorMessage.Text = "No TrackingNumber"
            Me.ErrorMessage.Visible = True
            Return False
            Exit Function
        ElseIf IsDate(txtSent.Text) = False Then
            Me.ErrorMessage.Text = "Date Invalid"
            Me.ErrorMessage.Visible = True
            Return False
        End If

        Me.ErrorMessage.Visible = False

        Return True
    End Function

    Private Sub cmdPackingSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPackingSlip.Click
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/PackingSlip.aspx?Address=" & _AddressID & "&Type=" & _PendingType)
    End Sub

    Private Sub cmdShip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShip.Click
        If ValidateForm() Then
            Dim objManagement As New CManagement()
            Dim obj As CManagementShipTracking = Session("objTracking")
            '   Dim objOrder As COrder = Session("objOrder")
            If IsNothing(obj) Then

            End If

            obj.TrackingNumber = Me.txtTrackingNumber.Text
            obj.SentDate = Me.txtSent.Text
            obj.TrackingMessage = Me.txtMsg.Text

            obj.ID = objManagement.SetTracking(obj)
            Session("objTracking") = obj
            Response.Redirect(Session("CallPage"))
        End If
    End Sub
End Class
