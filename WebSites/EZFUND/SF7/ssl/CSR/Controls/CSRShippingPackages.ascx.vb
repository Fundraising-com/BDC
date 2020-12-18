
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports CSR.CSRBusinessRule
Imports CSR.CSRSystemBase
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports System.Xml
Partial  Class CSRShippingPackages
    Inherits CSRWebControl
    
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

#Region "Class Events"
    Event RecalculateOrder()
    Event SetShippingAddress()

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GetOrder()
        Dim Datalistitem As Datalistitem
        Dim i As Integer = 0
        Dim bRecalculate As Boolean = False
        If packages.Items.Count = CSROrder.OrderAddresses.Count Then
            For Each Datalistitem In packages.Items
                'have to call this here because if you select a shipping carrier,
                'then checkboxes no longer call their postback function,
                'not sure if this is an AJAX issue
                If ChargeTax(i) = True Then
                    bRecalculate = True
                End If
                If ChargeShipping(i) = True Then
                    bRecalculate = True
                End If
                i = i + 1
            Next
        End If
        If bRecalculate = True Then
            RaiseEvent RecalculateOrder()
        End If

    End Sub

    Private Sub Page_Prerender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        
    End Sub
    
    Public Sub SetSpecialInstructions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        i = sender.commandargument
        Dim SpecialInstructions As TextBox
        SpecialInstructions = packages.Items(i).FindControl("SpecialInstructions")
        If SpecialInstructions.Visible = True Then
            SpecialInstructions.Visible = False
            CType(CSROrder.OrderAddresses(i), CSROrderAddress).ShowSpecialInstructions = False
        Else
            SpecialInstructions.Visible = True
            CType(CSROrder.OrderAddresses(i), CSROrderAddress).ShowSpecialInstructions = True
        End If
    End Sub
    Public Sub SaveSpecialInstructions()
        Dim i As Integer = 0

        Dim package As DataListItem
        If packages.Items.Count = CSROrder.OrderAddresses.Count Then
            For Each package In packages.Items
                CType(CSROrder.OrderAddresses(i), CSROrderAddress).Address.Instructions = CType(package.FindControl("SpecialInstructions"), TextBox).Text

                i = i + 1
            Next
        End If

    End Sub
    'Public Sub SetChargeShipping(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    Dim ParentItem As DataListItem = sender.parent
    '    i = CLng(CType(ParentItem.FindControl("lblIndex"), Label).Text) - 1 'this was displaying with +1 so It could say Package 1 instead of package 0

    '    ChargeShipping(i)
    'End Sub
    Private Function ChargeTax(ByVal i As Long) As Boolean
        Dim NoTax As CheckBox
        NoTax = packages.Items(i).FindControl("chkNoTax")
        Dim OrderAddress As CSROrderAddress
        OrderAddress = CSROrder.OrderAddresses(i)
        If NoTax.Checked <> OrderAddress.NoTaxCharge Then
            OrderAddress.NoTaxCharge = NoTax.Checked
            Return True
        End If
        Return False
    End Function
    Private Function ChargeShipping(ByVal i As Long) As Boolean
        Dim NoShip As CheckBox
        NoShip = packages.Items(i).FindControl("chkNoShipping")
        Dim OrderAddress As CSROrderAddress
        OrderAddress = CSROrder.OrderAddresses(i)
        If NoShip.Checked <> OrderAddress.NoShipCharge Then
            OrderAddress.NoShipCharge = NoShip.Checked
            If OrderAddress.NoShipCharge = False Then
                If OrderAddress.ShippingObject.ShippingChoices Is Nothing = False Then
                    SetShippingChoice(OrderAddress, OrderAddress.ShippingObject.ShipChoiceSelectedIndex)
                Else
                    OrderAddress.ShippingObject.RefreshShippingAmount = True
                End If
            End If
            Return True
        End If
        Return False
    End Function
    'Public Sub SetTax(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    Dim ParentItem As DataListItem = sender.parent
    '    i = CLng(CType(ParentItem.FindControl("lblIndex"), Label).Text) - 1 'this was displaying with +1 so It could say Package 1 instead of package 0

    '    ChargeTax(i)
    'End Sub
    Public Sub SetPremiumShipping(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim PremiumShipping As CheckBox
        Dim OrderAddress As CSROrderAddress
        Dim ParentItem As DataListItem = sender.parent
        i = CLng(CType(ParentItem.FindControl("lblIndex"), Label).Text) - 1 'this was displaying with +1 so It could say Package 1 instead of package 0


        PremiumShipping = packages.Items(i).FindControl("PremiumShipping")

        If (IsNothing(PremiumShipping) = False) Then
            OrderAddress = CType(CSROrder.OrderAddresses(i), CSROrderAddress)
            If PremiumShipping.Checked <> OrderAddress.ShippingObject.PremiumShipping Then
                OrderAddress.ShippingObject.PremiumShipping = PremiumShipping.Checked
                OrderAddress.ShippingObject.RefreshShippingAmount = True
            End If
        End If
        BindPackages()
    End Sub
    Public Sub RecalculateShipping(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer

        Dim OrderAddress As CSROrderAddress
        i = sender.commandargument
        OrderAddress = CType(CSROrder.OrderAddresses(i), CSROrderAddress)
        If CSROrder.OrderAddresses.Count = 1 Then
            RaiseEvent SetShippingAddress()
        End If
        If OrderAddress.NoShipCharge = True Or OrderAddress.ShippableItemCount = 0 Or StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText <> 2 Then
            'do nothing
        Else
            If OrderAddress.OrderItems.Count > 0 Then
                RecalculateShippingForAddress(OrderAddress)
            End If
        End If
        RaiseEvent RecalculateOrder()
    End Sub
    Private Sub SetShippingChoice(ByRef OrderAddress As CSROrderAddress, ByVal i As Long)
        Dim objShippingChoice As CShipChoice
        objShippingChoice = OrderAddress.ShippingObject.ShippingChoices(i + 1)
        OrderAddress.ShippingObject.SelectedShipMethod = objShippingChoice.Carrier
        OrderAddress.ShippingObject.ShippingTotal = objShippingChoice.Total
        OrderAddress.ShippingObject.BackOrderShippingAmount = objShippingChoice.BackOrderTotal
        OrderAddress.ShippingObject.ShippableShippingAmount = objShippingChoice.ShippableTotal
        OrderAddress.ShippingObject.ShipChoiceSelectedIndex = i
    End Sub
    Private Sub RecalculateShippingForAddress(ByVal OrderAddress As CSROrderAddress)
        Dim bExit As Boolean = False
        Dim i As Integer = 0  'Start here to skip the NONE carrier

        If OrderAddress.NoShipCharge = True Or OrderAddress.ShippableItemCount = 0 Or StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText <> 2 Then
            Exit Sub 'this is if it is not carrier shipping
        End If

        Do Until bExit = True
            Dim Code As String
            Code = GetShipCarrier(i)
            If Code <> "" Then
                OrderAddress.ShippingObject.ShipCarrierSelectedIndex = i
                OrderAddress.CarrierCode = Code
                OrderAddress.ShippingObject.RefreshShippingAmount = True
                Dim tot As Long
                tot = OrderAddress.ShippingObject.ShippingTotal 'initializes so I can check if they are using backup
                If OrderAddress.ShippingObject.UseBackupShipping = True Then
                    'keep trying to default to next on list
                    bExit = False
                Else
                    bExit = True
                End If
            Else
                'if there are no more Options to default to
                bExit = True
            End If
            i = i + 1
        Loop
        If OrderAddress.ShippingObject.UseBackupShipping = False Then
            SetShippingChoice(OrderAddress, 0)
        End If

    End Sub
    Public Sub RecalculateAllShipping()
        GetOrder()
        Dim OrderAddress As CSROrderAddress
        Dim bBind As Boolean = False
        For Each OrderAddress In CSROrder.OrderAddresses
            If OrderAddress.OrderItems.Count > 0 Then
                RecalculateShippingForAddress(OrderAddress)
                bBind = True
            End If
        Next
        If bBind = True Then
            BindPackages()
        End If
    End Sub
    Private Sub setDisplays()
        Dim Package As DataListItem
        Dim OrderAddress As CSROrderAddress
        Dim shipType As String = StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText

        Dim x As Integer = 0

        For Each Package In packages.Items
            Dim PremiumShipping As CheckBox = Package.FindControl("PremiumShipping")
            Dim Recalculate As LinkButton = Package.FindControl("Recalculate")
            Dim lblDisplayShippingMethod As Label = Package.FindControl("lblDisplayShippingMethod")
            Dim lblShippingCarrier As Label = Package.FindControl("lblShippingCarrier")
            Dim lblShippingMethod As Label = Package.FindControl("lblShippingMethod")
            Dim ShippingCarrier As DropDownList = Package.FindControl("ShippingCarrier")
            Dim ShippingMethod As DropDownList = Package.FindControl("ShippingMethod")
            Dim NoShipping As CheckBox = Package.FindControl("chkNoShipping")
            Dim NoTax As CheckBox = Package.FindControl("chkNoTax")
            Dim lblError As Label = Package.FindControl("BackupError")
            OrderAddress = CSROrder.OrderAddresses(x)
            NoShipping.Checked = OrderAddress.NoShipCharge
            NoTax.Checked = OrderAddress.NoTaxCharge
            PremiumShipping.Checked = OrderAddress.ShippingObject.PremiumShipping
            If OrderAddress.NoShipCharge = True Or OrderAddress.ShippableItemCount = 0 Then
                'OrderAddress.ShippingObject.RefreshShippingAmount = True
                '    Recalculate.Visible = False
                PremiumShipping.Visible = False
                lblShippingCarrier.Visible = False
                lblShippingMethod.Visible = False
                ShippingCarrier.Visible = False
                ShippingMethod.Visible = False
                lblDisplayShippingMethod.Visible = False
                lblError.Visible = False
                'ElseIf OrderAddress.OrderItems.Count = 0 Then

                '    Recalculate.Visible = True
                '    PremiumShipping.Visible = False
                '    lblShippingCarrier.Visible = False
                '    lblShippingMethod.Visible = False
                '    ShippingCarrier.Visible = False
                '    ShippingMethod.Visible = False
                '    lblDisplayShippingMethod.Visible = False
                '    lblError.Visible = False
            ElseIf shipType = 2 And OrderAddress.ShippingObject.UseBackupShipping = True And OrderAddress.ShippableItemCount > 0 Then
                'Carrier With Backup Shipping

                If OrderAddress.ShippingObject.ShippingError.Trim <> "" Then
                    lblError.Visible = True
                    lblError.Text = OrderAddress.ShippingObject.ShippingError
                Else
                    lblError.Visible = False
                End If
                PremiumShipping.Visible = False
                lblShippingCarrier.Visible = False
                lblShippingMethod.Visible = False
                ShippingCarrier.Visible = False
                ShippingMethod.Visible = False
                'Recalculate.Visible = True
                lblDisplayShippingMethod.Visible = True

            ElseIf shipType = 2 Then
                'carrier
                lblError.Visible = False
                PremiumShipping.Visible = False
                lblDisplayShippingMethod.Visible = False



                If OrderAddress.ShippingObject.ShippingChoices Is Nothing Then
                    lblShippingCarrier.Visible = False
                    lblShippingMethod.Visible = False
                    ShippingCarrier.Visible = False
                    ShippingMethod.Visible = False
                    'Recalculate.Visible = True
                Else
                    BindShippingCarriers(ShippingCarrier, OrderAddress.ShippingObject.ShipCarrierSelectedIndex)
                    BindShippingMethods(ShippingMethod, OrderAddress.ShippingObject.ShippingChoices, OrderAddress.ShippingObject.ShipChoiceSelectedIndex)
                    'Recalculate.Visible = True
                    lblShippingCarrier.Visible = True
                    lblShippingMethod.Visible = True
                    ShippingCarrier.Visible = True
                    ShippingMethod.Visible = True
                End If


            Else
                OrderAddress.ShippingObject.RefreshShippingAmount = True
                'Product or value based
                lblError.Visible = False
                If StoreFrontConfiguration.AdminShipping.Item("PremieumActive").InnerText = "1" Then
                    Dim PremShipLabel As String
                    Dim premShipAmt As Decimal
                    premShipAmt = CDec("0" & StoreFrontConfiguration.AdminShipping.Item("SpecialAmount").InnerText)
                    PremShipLabel = StoreFrontConfiguration.AdminShipping.Item("PremShipLabel").InnerText
                    PremiumShipping.Visible = True
                    PremiumShipping.Text = Replace(Replace(StoreFrontConfiguration.MessagesAccess.GetXMLMessage("ShipSummary.aspx", "PremiumShipping", "ApplyPremiumShipping"), "[PremiumShippingName]", PremShipLabel), "[Amount]", Format(premShipAmt, "C"))
                Else
                    PremiumShipping.Visible = False
                End If
                lblShippingCarrier.Visible = False
                lblShippingMethod.Visible = False
                ShippingCarrier.Visible = False
                ShippingMethod.Visible = False
                'Recalculate.Visible = False
                lblDisplayShippingMethod.Visible = True
            End If
            Dim CSRMan As New CSRManagement(StoreFrontConfiguration.SiteURL)
            If CSRMan.IsAdvancedCSR = False Then

                NoShipping.Visible = False
                NoTax.Visible = False
            End If

            x = x + 1
        Next

    End Sub
    Private Sub BindShippingCarriers(ByVal ShippingCarrier As DropDownList, ByVal SelectedItem As Long)
        Dim ar As New ArrayList
        Dim i As Integer
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim oNode As XmlNode
        Dim oSelNode As XmlNode

        dt.Columns.Add(New DataColumn("ShipName", GetType(String)))
        dt.Columns.Add(New DataColumn("ShipCode", GetType(String)))
        oNode = StoreFrontConfiguration.XMLDocument().Item("SiteProducts").Item("ShipMethods")


        For i = 1 To oNode.ChildNodes.Count - 1
            oSelNode = oNode.ChildNodes.Item(i)
            dr = dt.NewRow()
            dr(0) = oSelNode.Item("Method").InnerText.ToString
            dr(1) = oSelNode.Item("Code").InnerText.ToString
            dt.Rows.Add(dr)
        Next

        Dim dv As New DataView(dt)
        ShippingCarrier.DataSource = dt
        ShippingCarrier.SelectedIndex = SelectedItem
        ShippingCarrier.DataTextField = "ShipName"
        ShippingCarrier.DataValueField = "ShipCode"
        ShippingCarrier.DataBind()
    End Sub
    Private Function GetShipCarrier(ByVal i As Integer) As String
        Dim onode As XmlNode
        onode = StoreFrontConfiguration.XMLDocument().Item("SiteProducts").Item("ShipMethods")
        Try
            Return onode.ChildNodes(i + 1).Item("Code").InnerText.ToString 'add one to skip No Carrier
        Catch
            Return ""
        End Try
    End Function
    Private Sub BindShippingMethods(ByRef objShipChoices As DropDownList, ByVal objShipChoicesArrayList As ArrayList, ByVal SelectedIndex As Long)
        Dim i As Integer = 0
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim tempShipChoice As CShipChoice
        Dim tempDisplay As String
        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))
        For Each tempShipChoice In objShipChoicesArrayList
            If i > 0 Then
                dr = dt.NewRow()
                dr(0) = tempShipChoice.ID
                tempDisplay = tempShipChoice.Carrier
                If (tempShipChoice.TimeInTransit <> "") Then
                    If (tempShipChoice.TimeInTransit = "1") Then
                        tempDisplay = tempDisplay & " (" & tempShipChoice.TimeInTransit & " Day)"
                    Else
                        tempDisplay = tempDisplay & " (" & tempShipChoice.TimeInTransit & " Days)"
                    End If
                End If
                If (tempShipChoice.Total > 0) Then
                    tempDisplay = tempDisplay & " - " & PriceDisplay3(tempShipChoice.Total) & " "
                End If

                dr(1) = tempDisplay
                dt.Rows.Add(dr)
            End If
            i = i + 1
        Next
        objShipChoices.DataSource = dt
        objShipChoices.DataValueField = "ID"
        objShipChoices.DataTextField = "Display"
        objShipChoices.SelectedIndex = SelectedIndex
        objShipChoices.DataBind()
    End Sub
    Public Sub packages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles packages.ItemDataBound
        Dim ShipMethod As Label
        ShipMethod = e.Item.FindControl("lblDisplayShippingMethod")
        If ShipMethod Is Nothing = False Then


            Dim strLabel As String
            Dim OrderAddress As CSROrderAddress
            Dim Price As Decimal
            OrderAddress = e.Item.DataItem
            Price = CDec("0" & OrderAddress.ShippingTotal.ToString)
            Dim strSelShipMethod As String
            strSelShipMethod = OrderAddress.ShippingObject.SelectedShipMethod
            If strSelShipMethod Is Nothing = False Then
                If strSelShipMethod.IndexOf("-") > -1 Then
                    strSelShipMethod = strSelShipMethod.Substring(0, strSelShipMethod.IndexOf("-")) 'Remove Premium shipping label
                End If
                strLabel = PriceDisplay2(Price) & " - " & strSelShipMethod
                ShipMethod.Text = strLabel
            End If
            CType(e.Item.FindControl("SpecialInstructions"), TextBox).Text = OrderAddress.Address.Instructions
            CType(e.Item.FindControl("SpecialInstructions"), TextBox).Visible = OrderAddress.ShowSpecialInstructions

        End If
    End Sub

    Public Sub ChangeCarrier(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim ParentItem As DataListItem = sender.parent
        i = CLng(CType(ParentItem.FindControl("lblIndex"), Label).Text) - 1 'this was displaying with +1 so It could say Package 1 instead of package 0

        Dim OrderAddress As CSROrderAddress

        OrderAddress = CType(CSROrder.OrderAddresses(i), CSROrderAddress)
        OrderAddress.ShippingObject.ShipCarrierSelectedIndex = sender.selectedindex
        OrderAddress.CarrierCode = sender.selecteditem.value
        OrderAddress.ShippingObject.RefreshShippingAmount = True
        Dim tot As Long
        tot = OrderAddress.ShippingObject.ShippingTotal 'initialize total and shipping choices
        If OrderAddress.ShippingObject.UseBackupShipping = False Then
            SetShippingChoice(OrderAddress, 0)
        End If
        RaiseEvent RecalculateOrder()
    End Sub
    Public Sub ChangeMethod(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        'Dim objShippingChoice As CShipChoice
        Dim OrderAddress As CSROrderAddress
        Dim ParentItem As DataListItem = sender.parent
        i = CLng(CType(ParentItem.FindControl("lblIndex"), Label).Text) - 1 'this was displaying with +1 so It could say Package 1 instead of package 0


        OrderAddress = CType(CSROrder.OrderAddresses(i), CSROrderAddress)
        SetShippingChoice(OrderAddress, CLng(sender.selectedIndex))
        RaiseEvent RecalculateOrder()
    End Sub
    Public Sub BindPackages()
        GetOrder()
        SaveSpecialInstructions()
        'Dim objAddress As CSROrderAddress
        Dim RestrictCOD As Boolean = True

        packages.DataSource = CSROrder.OrderAddresses
        packages.DataBind()
        setDisplays()

    End Sub
    Public Function CheckShipping() As Boolean
        Dim Package As DataListItem
        Dim OrderAddress As CSROrderAddress
        Dim bNeedsAttention As Boolean = False
        SaveSpecialInstructions()
        Dim shipType As String = StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText
        If shipType <> 2 Then
            Return True
        Else

            Dim x As Integer = 0

            For Each Package In packages.Items
                Dim ShippingCarrier As DropDownList = Package.FindControl("ShippingCarrier")
                Dim ShippingMethod As DropDownList = Package.FindControl("ShippingMethod")
                OrderAddress = CSROrder.OrderAddresses(x)
                If OrderAddress.NoShipCharge = False Then
                    If OrderAddress.OrderItems.Count > 0 Then
                        If OrderAddress.ShippingObject.UseBackupShipping = False Then
                            If ShippingCarrier.Visible = False OrElse ShippingMethod.Visible = False Then
                                bNeedsAttention = True
                            End If
                        End If
                    End If
                End If

                x = x + 1
            Next
            If bNeedsAttention = True Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please make shipping selection(s).")
                Return False
            End If
            Return True
        End If
    End Function

    'begin: GJV - 7/31/2007 - CSR
    Public Function AllowNoShipping() As Boolean

        Dim CSRMgmt As New CSRManagement(StoreFrontConfiguration.SiteURL)
        Dim CSRUserMgmt As New CSRUserManagement

        Dim CSRUser As CSRUser = CSRUserMgmt.GetUser(Session.Item("CSRUID"))

        Return CSRMgmt.IsAdvancedCSR And CSRUser.OverrideShippingCharges

    End Function

    Public Function AllowNoTaxes() As Boolean

        Dim CSRMgmt As New CSRManagement(StoreFrontConfiguration.SiteURL)
        Dim CSRUserMgmt As New CSRUserManagement

        Dim CSRUser As CSRUser = CSRUserMgmt.GetUser(Session.Item("CSRUID"))

        Return CSRMgmt.IsAdvancedCSR And CSRUser.OverrideTaxes

    End Function
    'end: GJV - 7/31/2007 - CSRs

End Class
