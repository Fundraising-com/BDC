Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.Systembase
Public MustInherit Class ShipStatusControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents ddShipped As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddBackOrderShipped As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dlShipStatus As System.Web.UI.WebControls.DataList
    Protected WithEvents BOShipRow As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtBOTrackVisible As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrackVisible As System.Web.UI.WebControls.TextBox
    Protected WithEvents rowSep As System.Web.UI.HtmlControls.HtmlTableRow

    Private m_OrderId As Long
    Private txtOrderID As TextBox
    Private _ShowSeperator As Boolean = False
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
        If IsPostBack Then
            SetItemsVisible()
        End If
    End Sub
    Public Sub Set_Display(ByVal arrShipStat As ArrayList)
        If arrShipStat.Count > 1 Then
            _ShowSeperator = True
        End If
        dlShipStatus.DataSource = arrShipStat
        dlShipStatus.DataBind()

    End Sub

    Public Sub SetBOShipping(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddBackOrderShipped.SelectedIndexChanged
        Dim obj As DropDownList = sender
        Dim objParent As Object = sender
        Dim objDataListItem As New DataListItem(0, ListItemType.Item)
        Dim objItem As DataListItem
        Dim cmdSetBackOrderShipped As LinkButton
        Dim cmdTrack As LinkButton
        Dim imgSetBackOrderShipped As System.Web.UI.WebControls.Image
        Dim imgTrack As System.Web.UI.WebControls.Image
        While (Not objParent.GetType() Is objDataListItem.GetType)
            objParent = objParent.Parent
        End While
        objItem = CType(objParent, DataListItem)
        Dim txtAddID As TextBox
        txtAddID = CType(objItem.FindControl("AddID"), TextBox)
        cmdSetBackOrderShipped = CType(objItem.FindControl("cmdsetBackOrderShipped"), LinkButton)
        cmdTrack = CType(objItem.FindControl("cmdTrack"), LinkButton)
        imgSetBackOrderShipped = CType(objItem.FindControl("imgsetBackOrderShipped"), System.Web.UI.WebControls.Image)
        imgTrack = CType(objItem.FindControl("imgTrack"), System.Web.UI.WebControls.Image)
        txtOrderID = CType(objItem.FindControl("OrderID"), TextBox)
        m_OrderId = txtOrderID.Text

        If cmdTrack.CommandArgument <> "" Then
            cmdTrack.Visible = True
            imgTrack.Visible = True
        Else
            cmdTrack.Visible = False
            imgTrack.Visible = False
        End If
        If obj.SelectedIndex = 0 Then
            cmdSetBackOrderShipped.Visible = False
            imgSetBackOrderShipped.Visible = False
        Else
            cmdSetBackOrderShipped.Visible = True
            imgSetBackOrderShipped.Visible = True
        End If
        UpdateShippingStatus(txtAddID.Text, 1)
    End Sub

    Public Sub SetShipping(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddShipped.SelectedIndexChanged

        Dim objParent As Object = sender
        Dim objDataListItem As New DataListItem(0, ListItemType.Item)
        Dim objItem As DataListItem
        Dim txtAddID As TextBox


        While (Not objParent.GetType() Is objDataListItem.GetType)
            objParent = objParent.Parent
        End While
        objItem = CType(objParent, DataListItem)
        txtAddID = CType(objItem.FindControl("AddID"), TextBox)
        txtOrderID = CType(objItem.FindControl("OrderID"), TextBox)
        m_OrderId = txtOrderID.Text

        UpdateShippingStatus(txtAddID.Text, 0)

    End Sub

    Private Sub UpdateShippingStatus(ByVal lngAddressID As String, ByVal PendingType As Integer)
        'PendingType  0 =normal,1=BO
        Dim objManagement As New CManagement()
        objManagement.UpdateShipPendingStatus(CLng(lngAddressID), PendingType)
        objManagement.UpdateShipPendingOverAllStatus(m_OrderId, HasAnyPending, HasAnyBOPending)

    End Sub

    Private Function HasAnyPending() As Integer
        Dim dlItem As DataListItem

        For Each dlItem In dlShipStatus.Items
            If CType(dlItem.FindControl("ddShipped"), DropDownList).SelectedIndex = 1 Then
                Return 1
                Exit Function
            End If
        Next
        Return 0
    End Function

    Private Function HasAnyBOPending() As Integer
        Dim dlItem As DataListItem

        For Each dlItem In dlShipStatus.Items
            If CType(dlItem.FindControl("ddBackOrderShipped"), DropDownList).Visible Then
                If CType(dlItem.FindControl("ddBackOrderShipped"), DropDownList).SelectedIndex = 1 Then
                    Return 1
                    Exit Function
                End If
            End If
        Next
        Return 0
    End Function



    Private Sub dlShipStatus_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShipStatus.ItemCreated
        If Not IsPostBack Then
            Dim ar As New ArrayList()
            Dim obj As COrderShipmentStatus = e.Item.DataItem
            Dim cmdSetBackOrderShipped As LinkButton
            Dim cmdTrack As LinkButton
            Dim cmdSetHomeShipped As LinkButton
            Dim cmdBOTrack As LinkButton
            Dim imgSetHomeShipped As System.Web.UI.WebControls.Image
            Dim imgTrack As System.Web.UI.WebControls.Image
            Dim imgSetBackOrderShipped As System.Web.UI.WebControls.Image
            Dim imgBOTrack As System.Web.UI.WebControls.Image
            rowSep = e.Item.FindControl("rowSep")
            If IsNothing(rowSep) = False Then
                rowSep.Visible = _ShowSeperator
            End If

            BOShipRow = CType(e.Item.FindControl("BOShipRow"), System.Web.UI.HtmlControls.HtmlTableRow)


            cmdSetHomeShipped = CType(e.Item.FindControl("cmdsetHomeShipped"), LinkButton)
            cmdSetBackOrderShipped = CType(e.Item.FindControl("cmdsetBackOrderShipped"), LinkButton)
            cmdTrack = CType(e.Item.FindControl("cmdTrack"), LinkButton)
            cmdBOTrack = CType(e.Item.FindControl("cmdBOTrack"), LinkButton)
            imgSetBackOrderShipped = CType(e.Item.FindControl("imgsetBackOrderShipped"), System.Web.UI.WebControls.Image)
            imgTrack = CType(e.Item.FindControl("imgTrack"), System.Web.UI.WebControls.Image)
            imgSetHomeShipped = CType(e.Item.FindControl("imgSetHomeShipped"), System.Web.UI.WebControls.Image)
            imgBOTrack = CType(e.Item.FindControl("imgBOTrack"), System.Web.UI.WebControls.Image)

            ddShipped = CType(e.Item.FindControl("ddShipped"), System.Web.UI.WebControls.DropDownList)
            ddBackOrderShipped = CType(e.Item.FindControl("ddBackOrderShipped"), System.Web.UI.WebControls.DropDownList)

            If obj.TrackingNumber = "" Then
                cmdTrack.Visible = False
                imgTrack.Visible = False

            Else
                cmdTrack.Visible = True
                imgTrack.Visible = True
                '    txtTrackVisible.Text = obj.TrackingNumber
            End If
            If obj.BOTrackingNumber = "" Then
                cmdBOTrack.Visible = False
                imgBOTrack.Visible = False

            Else
                cmdBOTrack.Visible = True
                imgBOTrack.Visible = True
                '  txtBOTrackVisible.Text = obj.BOTrackingNumber
            End If
            If obj.HasShipmentPending Then
                cmdSetHomeShipped.Visible = True
                imgSetHomeShipped.Visible = True

                ddShipped.SelectedIndex = 1
            Else
                cmdSetHomeShipped.Visible = False
                imgSetHomeShipped.Visible = False
                ddShipped.SelectedIndex = 0
            End If

            If obj.HasBOShipmentPending Then
                cmdSetBackOrderShipped.Visible = True
                imgSetBackOrderShipped.Visible = True
                ddBackOrderShipped.SelectedIndex = 1
            Else
                cmdSetBackOrderShipped.Visible = False
                imgSetBackOrderShipped.Visible = False
                ddBackOrderShipped.SelectedIndex = 0
            End If
            If obj.BOQuantity > 0 Then
                BOShipRow.Visible = True
            Else
                BOShipRow.Visible = False
            End If
        End If
    End Sub


    Private Sub SetItemsVisible()
        Dim dlItem As DataListItem
        Dim cmdTrack As LinkButton
        Dim cmdBOTrack As LinkButton
        Dim imgTrack As System.Web.UI.WebControls.Image
        Dim imgBOTrack As System.Web.UI.WebControls.Image
        If dlShipStatus.Items.Count > 1 Then
            _ShowSeperator = True
        Else
            _ShowSeperator = False
        End If

        For Each dlItem In dlShipStatus.Items
            txtTrackVisible = CType(dlItem.FindControl("txtTrackVisible"), TextBox)
            txtBOTrackVisible = CType(dlItem.FindControl("txtBOTrackVisible"), TextBox)
            Dim txtBOCount As TextBox = CType(dlItem.FindControl("BOCount"), TextBox)
            cmdTrack = CType(dlItem.FindControl("CmdTrack"), LinkButton)
            cmdBOTrack = CType(dlItem.FindControl("CmdBOTrack"), LinkButton)
            imgTrack = CType(dlItem.FindControl("imgTrack"), System.Web.UI.WebControls.Image)
            imgBOTrack = CType(dlItem.FindControl("imgBOTrack"), System.Web.UI.WebControls.Image)

            BOShipRow = CType(dlItem.FindControl("BOShipRow"), System.Web.UI.HtmlControls.HtmlTableRow)
            rowSep = dlItem.FindControl("rowSep")
            If IsNothing(rowSep) = False Then
                rowSep.Visible = _ShowSeperator
            End If
            If CLng(txtBOCount.Text) > 0 Then
                BOShipRow.Visible = True
            Else
                BOShipRow.Visible = False
            End If

            If txtTrackVisible.Text = "" Then
                cmdTrack.Visible = False
                imgTrack.Visible = False
            Else
                cmdTrack.Visible = True
                imgTrack.Visible = True
            End If
            If txtBOTrackVisible.Text = "" Then
                cmdBOTrack.Visible = False
                imgBOTrack.Visible = False
            Else
                cmdBOTrack.Visible = True
                imgBOTrack.Visible = True
            End If

            If CType(dlItem.FindControl("ddShipped"), DropDownList).SelectedIndex = 1 Then

                CType(dlItem.FindControl("cmdsetHomeShipped"), LinkButton).Visible = True

            Else
                CType(dlItem.FindControl("cmdsetHomeShipped"), LinkButton).Visible = False
            End If
            If CType(dlItem.FindControl("ddBackOrderShipped"), DropDownList).SelectedIndex = 1 Then

                CType(dlItem.FindControl("cmdsetBackOrderShipped"), LinkButton).Visible = True
            Else
                CType(dlItem.FindControl("cmdsetBackOrderShipped"), LinkButton).Visible = False
            End If
        Next

    End Sub

    Public Sub Ship(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = sender
        Dim sAddID As String = obj.CommandArgument
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/ProcessShipment.aspx?Type=" & obj.CommandName & "&Address=" & sAddID)
    End Sub
    Public Sub TrackClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = sender
        'Dim sCustID As String = "&CustID=" & obj.CommandName
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/TrackShipment.aspx?OrderID=" & CType(sender.FindControl("OrderID"), TextBox).Text & "&Type=" & obj.CommandName & "&OrderAddressID=" & obj.CommandArgument)  '?OrderID=" & obj.CommandArgument & sCustID)

    End Sub
End Class
