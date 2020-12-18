'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders

Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Public Class ShipSummary
    Inherits CWebPage
    Protected WithEvents DynamicCartDisplay1 As UITools.DynamicCartDisplay
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents TotalDisplay1 As UITools.TotalDisplay
    Protected WithEvents btnContinue As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgContinue As System.Web.UI.WebControls.Image
    Protected WithEvents lblNickName As System.Web.UI.WebControls.Label
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents UPSCW As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents cboShipRates As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboShipping As UITools.SelectValControl
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

        Try
            btnContinue.Visible = False
            SetPageTitle = m_objMessages.GetXMLMessage("ShipSummary.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            ErrorMessage.Visible = False
            Dim objOrder As COrder = Session("Order")

            'objOrder.StoreShippingAccess = New Admin.CShipping(dom)
            'objOrder.TaxRateAccess = New CTaxRateAccess(dom.Item("SiteProducts").Item("TaxRates"), dom.Item("SiteProducts").Item("Countries"), dom.Item("SiteProducts").Item("States"), dom.Item("SiteProducts").Item("Locals"))
            objOrder.SetAddressIndex()
            Dim objOrderAddr As COrderAddress
            Dim RestrictCOD As Boolean = True
            'For Each objOrderAddr In objOrder.OrderAddresses
            '    If objOrderAddr.HasShippableProducts Then
            '        ShowCOD = True

            '    End If
            'Next


            TotalDisplay1.OandaISO = Session("ConvertISO")
            TotalDisplay1.OandaRate = CDec(Session("OandaRate"))

            If (CType(Session("Order"), COrder).OrderAddresses.Count = 1) Then
                TotalDisplay1.DisplayMerchandiseTotal = True
                TotalDisplay1.DisplayDiscountTotal = True
                TotalDisplay1.DisplaySubTotal = True
                TotalDisplay1.DisplayLocalTaxTotal = True
                TotalDisplay1.DisplayStateTaxTotal = True
                TotalDisplay1.DisplayCountryTaxTotal = True
                TotalDisplay1.DisplayShippingTotal = True
                TotalDisplay1.DisplaySubHandlingTotal = True
                TotalDisplay1.DisplayShipmentTotal = False
                TotalDisplay1.DisplayOrderTotal = False
            Else
                TotalDisplay1.DisplayCODTotal = False
                TotalDisplay1.DisplayMerchandiseTotal = False
                TotalDisplay1.DisplayDiscountTotal = False
                TotalDisplay1.DisplaySubTotal = False
                TotalDisplay1.DisplayLocalTaxTotal = False
                TotalDisplay1.DisplayStateTaxTotal = False
                TotalDisplay1.DisplayCountryTaxTotal = False
                TotalDisplay1.DisplayShippingTotal = False
                TotalDisplay1.DisplaySubHandlingTotal = True
                TotalDisplay1.DisplayOrderTotal = False
            End If
            Dim tempShipCarrier As String
            Dim item As DataListItem
            Dim shipCarrier As SelectValControl
            Dim shipChoices As DropDownList
            Dim objAddress As COrderAddress
            Dim shipType As String = StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText
            Dim shipChoice As DropDownList
            Dim objShippingChoice As CShipChoice
            Dim lblBackupShip As Label
            Dim lblShippingError As Label
            Dim PremiumShipping As CheckBox
            Dim UPSRow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim UPSCWRow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim UPSRateChange As Label

            btnContinue.Attributes.Add("onclick", "return SetValidation(" & objOrder.OrderAddresses.Count & ");")

            If (Not IsPostBack) Then
                DataList1.DataSource = objOrder.OrderAddresses
                DataList1.DataBind()
                For Each objAddress In objOrder.OrderAddresses
                    If (objAddress.ShippingObject.SelectedShipMethod = "") Then
                        objAddress.ShippingObject.RefreshShippingAmount = True
                        objAddress.ShippingObject.SelectedShipMethod = "NONE"
                        objAddress.CarrierCode = "NONE"
                        objAddress.ShippingObject.ShipCarrierSelectedIndex = 0

                        If objAddress.HasShippableProducts Then
                            RestrictCOD = False
                        End If

                    End If
                Next
            Else
                For Each item In DataList1.Items
                    shipCarrier = CType(item.FindControl("cboShipping"), UITools.SelectValControl)
                    shipChoices = CType(item.FindControl("cboShipChoices"), DropDownList)
                    PremiumShipping = CType(item.FindControl("chkPremiumShipping"), CheckBox)
                    If (IsNothing(PremiumShipping) = False) Then
                        objAddress = CType(objOrder.OrderAddresses.Item(item.ItemIndex), COrderAddress)
                        If PremiumShipping.Checked <> objAddress.ShippingObject.PremiumShipping Then
                            objAddress.ShippingObject.PremiumShipping = PremiumShipping.Checked
                            objAddress.ShippingObject.RefreshShippingAmount = True
                        End If
                        If objAddress.HasShippableProducts Then
                            RestrictCOD = False
                        End If
                    End If
                    'If (IsNothing(shipCarrier) = False) And shipCarrier.SelectedIndex = 0  Then
                    'objAddress = CType(objOrder.OrderAddresses.Item(item.ItemIndex), COrderAddress)
                    'if shipCarrier.SelectedIndex<>objAddress.ShippingObject.ShipCarrierSelectedIndex then
                    '   objAddress.ShippingObject.RefreshShippingAmount = True
                    '   objAddress.CarrierCode="NONE"
                    '   objAddress.ShippingObject.ShipCarrierSelectedIndex=ShipCarrier.selectedIndex
                    'end if
                    'end if

                    If (IsNothing(shipCarrier) = False) Then 'If (IsNothing(shipCarrier) = False) And shipCarrier.SelectedIndex <> 0 Then
                        'shipMethod.SelectedItem.Value
                        'objOrder = CType(Session("Order"), COrder)
                        objAddress = CType(objOrder.OrderAddresses.Item(item.ItemIndex), COrderAddress)
                        objAddress.ShippingObject.ShipCarrierSelectedIndex = shipCarrier.SelectedIndex
                        If objAddress.HasShippableProducts Then
                            RestrictCOD = False
                        End If
                        If ((shipCarrier.SelectedItem.Value <> objAddress.CarrierCode) Or (IsNothing(shipChoices.SelectedItem))) Then
                            objAddress.CarrierCode = shipCarrier.SelectedItem.Value
                            objAddress.ShippingObject.RefreshShippingAmount = True
                        ElseIf (shipCarrier.SelectedItem.Value <> "NONE") Then
                            If (objAddress.ShippingObject.UseBackupShipping = False And objAddress.ShippingObject.FreeShipping = False) Then

                                objShippingChoice = objAddress.ShippingObject.ShippingChoices(CInt(shipChoices.SelectedItem.Value))
                                objAddress.ShippingObject.SelectedShipMethod = objShippingChoice.Carrier
                                objAddress.ShippingObject.ShippingTotal = objShippingChoice.Total
                                objAddress.ShippingObject.BackOrderShippingAmount = objShippingChoice.BackOrderTotal
                                objAddress.ShippingObject.ShippableShippingAmount = objShippingChoice.ShippableTotal
                                objAddress.ShippingObject.ShipChoiceSelectedIndex = CInt(shipChoices.SelectedItem.Value)
                                If shipChoices.SelectedIndex = 0 Then
                                    btnContinue.Visible = False
                                Else
                                    btnContinue.Visible = True
                                End If
                            End If
                        End If
                    End If

                Next
            End If
            TotalDisplay1.RestrictCOD = RestrictCOD
            objOrder.PaymentMethod = CType(TotalDisplay1.FindControl("PaymentMethod"), SelectValControl).SelectedItem.Value
            TotalDisplay1.DataSource = objOrder
            TotalDisplay1.DataBind()


            'this happens after everything is re-totaled
            For Each item In DataList1.Items
                UPSRow = CType(item.FindControl("UPS"), System.Web.UI.HtmlControls.HtmlTableRow)
                UPSRateChange = CType(item.FindControl("UPSRateChange"), Label)

                shipCarrier = CType(item.FindControl("cboShipping"), UITools.SelectValControl)
                shipChoices = CType(item.FindControl("cboShipChoices"), DropDownList)
                PremiumShipping = CType(item.FindControl("chkPremiumShipping"), CheckBox)
                lblBackupShip = CType(item.FindControl("lblBackupShip"), Label)
                lblShippingError = CType(item.FindControl("lblShippingError"), Label)
                PremiumShipping.Visible = False
                If (shipType = 2 And objAddress.ShippingObject.FreeShipping = False And objAddress.HasShippableProducts) Then
                    CType(item.FindControl("CarrierShipping"), HtmlTableRow).Visible = True
                    objAddress = CType(objOrder.OrderAddresses.Item(item.ItemIndex), COrderAddress)
                    ' If (IsPostBack) Then

                    shipCarrier.SelectedIndex = objAddress.ShippingObject.ShipCarrierSelectedIndex()
                    If (shipCarrier.SelectedIndex <> 0) Then

                        If (objAddress.ShippingObject.UseBackupShipping = True) Then
                            objAddress.CarrierCode = "NONE"
                            shipChoices.Visible = False
                            lblBackupShip.Visible = True
                            If objAddress.ShippingObject.ShippingError.Trim <> "" Then
                                lblShippingError.Text = objAddress.ShippingObject.ShippingError
                            End If
                            lblShippingError.Visible = True
                            UPSRow.Visible = False
                            UPSCW.Visible = False
                            btnContinue.Visible = True
                        Else
                            CreateShipChoiceDD(shipChoices, objAddress.ShippingObject.ShippingChoices)
                            shipChoices.SelectedIndex = objAddress.ShippingObject.ShipChoiceSelectedIndex
                            shipChoices.Visible = True
                            lblBackupShip.Visible = False
                            lblShippingError.Visible = False
                            If shipCarrier.SelectedItem.Value = "UPS" Then
                                UPSRow.Visible = True
                                UPSCW.Visible = True
                                If objAddress.ShippingObject.RateAdjusted = True Then
                                    If StoreFrontConfiguration.StoreName.Length > 0 Then
                                        UPSRateChange.Text = UPSRateChange.Text.Replace("the online merchant", StoreFrontConfiguration.StoreName)
                                    End If
                                    UPSRateChange.Visible = True
                                Else
                                    UPSRateChange.Visible = False
                                End If
                            Else
                                UPSRow.Visible = False
                                UPSCW.Visible = False
                                UPSRateChange.Visible = False
                            End If
                        End If
                    Else
                        shipChoices.Visible = False
                        lblBackupShip.Visible = False
                        lblShippingError.Visible = False
                        UPSRow.Visible = False
                        UPSCW.Visible = False
                        Me.btnContinue.Visible = False
                    End If
                    PremiumShipping.Visible = False
                    'Else
                    '    If (objAddress.ShippingObject.SelectedShipMethod <> "" And objAddress.ShippingObject.SelectedShipMethod <> "NONE") Then

                    '        If (objAddress.ShippingObject.UseBackupShipping = True) Then
                    '            shipChoices.Visible = False
                    '            lblBackupShip.Visible = True
                    '        Else
                    '            CreateShipChoiceDD(shipChoices, objAddress.ShippingObject.ShippingChoices)
                    '            shipChoices.SelectedIndex = objAddress.ShippingObject.ShipChoiceSelectedIndex
                    '            shipChoices.Visible = True
                    '            lblBackupShip.Visible = False
                    '        End If

                    '    Else
                    '        shipChoices.Visible = False
                    '        lblBackupShip.Visible = False
                    '    End If
                    'End If
                ElseIf (shipType <> 2 And objAddress.ShippingObject.FreeShipping = False And objAddress.HasShippableProducts) Then
                    btnContinue.Visible = True
                    If StoreFrontConfiguration.AdminShipping.Item("PremieumActive").InnerText = "1" Then
                        Dim PremShipLabel As String
                        Dim premShipAmt As Decimal
                        premShipAmt = CDec("0" & StoreFrontConfiguration.AdminShipping.Item("SpecialAmount").InnerText)
                        PremShipLabel = StoreFrontConfiguration.AdminShipping.Item("PremShipLabel").InnerText
                        PremiumShipping.Visible = True
                        PremiumShipping.Text = Replace(Replace(m_objMessages.GetXMLMessage("ShipSummary.aspx", "PremiumShipping", "ApplyPremiumShipping"), "[PremiumShippingName]", PremShipLabel), "[Amount]", Format(premShipAmt, "C"))
                    Else
                        PremiumShipping.Visible = False
                    End If
                    CType(item.FindControl("CarrierShipping"), HtmlTableRow).Visible = False
                    shipChoices.Visible = False
                    lblBackupShip.Visible = False
                    lblShippingError.Visible = False
                    UPSRow.Visible = False
                    UPSCW.Visible = False

                Else
                    btnContinue.Visible = True
                    CType(item.FindControl("CarrierShipping"), HtmlTableRow).Visible = False
                    shipChoices.Visible = False
                    lblBackupShip.Visible = False
                    lblShippingError.Visible = False
                    PremiumShipping.Visible = False
                    UPSRow.Visible = False
                    UPSCW.Visible = False

                End If
            Next
            imgContinue.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filename").Value
        Catch ex As Exception
            Me.ErrorMessage.Text = ex.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

    Private Function CheckShipMethods() As Boolean
        Dim objOrder As COrder = Session("Order")
        Dim _orderaddress As COrderAddress

        For Each _orderaddress In objOrder.OrderAddresses
            If _orderaddress.HasShippableProducts Then
                If (_orderaddress.CarrierCode = "NONE" And _orderaddress.ShippingObject.FreeShipping = False And _orderaddress.ShippingObject.UseBackupShipping = False) Then
                    Return False
                End If

                If _orderaddress.CarrierCode <> "NONE" And _orderaddress.ShippingObject.FreeShipping = False And _orderaddress.ShippingObject.SelectedShipMethod = "<Please Select a Carrier Method>" And _orderaddress.ShippingObject.UseBackupShipping = False Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Sub DataList1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemCreated

        Dim shipMethod As UITools.SelectValControl = CType(e.Item.FindControl("cboShipping"), UITools.SelectValControl)
        '        Dim objOrder As COrder = Session("Order")
        '        Dim objAddress As COrderAddress

        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then

            If (CType(Session("Order"), COrder).OrderAddresses.Count = 1) Then
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).Visible = False
            Else
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataSource = CType(CType(Session("Order"), COrder).OrderAddresses.Item(e.Item.ItemIndex), COrderAddress)
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).DataBind()
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaISO = Session("ConvertISO")
                CType(e.Item.FindControl("ShipmentTotalDisplay1"), TotalDisplay).OandaRate = CDec(Session("OandaRate"))
            End If

            CType(e.Item.FindControl("DynamicCartDisplay1"), DynamicCartDisplay).OandaISO = Session("ConvertISO")
            CType(e.Item.FindControl("DynamicCartDisplay1"), DynamicCartDisplay).OandaRate = CDec(Session("OandaRate"))

            CType(e.Item.FindControl("DynamicCartDisplay1"), DynamicCartDisplay).DataSource = CType(CType(Session("Order"), COrder).OrderAddresses.Item(e.Item.ItemIndex), COrderAddress).OrderItems
            CType(e.Item.FindControl("DynamicCartDisplay1"), DynamicCartDisplay).DataBind()

        End If
    End Sub

    Private Sub CreateShipChoiceDD(ByRef objShipChoices As DropDownList, ByVal objShipChoicesArrayList As ArrayList)
        Dim i As Integer
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim tempShipChoice As CShipChoice
        Dim tempDisplay As String
        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))
        For Each tempShipChoice In objShipChoicesArrayList
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
        Next
        objShipChoices.DataSource = dt
        objShipChoices.DataValueField = "ID"
        objShipChoices.DataTextField = "Display"
        objShipChoices.DataBind()
    End Sub

    Public Sub cboShipping_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboShipping.SelectedIndexChanged

        Dim ddlShipping As SelectValControl = sender

        ' cboShipping.SelectedIndex = ddlShipping.SelectedIndex

    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        ' Get the ShipMethods and the Special Instructions
        Dim shipType As String = StoreFrontConfiguration.AdminShipping.Item("ShipType").InnerText
        Dim objOrder As COrder = Session("Order")


        If (shipType = 2 And CheckShipMethods() = False) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("ShipSummary.aspx", "Error", "InvalidCarrier")
            ErrorMessage.Visible = True
            Exit Sub
        End If

        '        objOrder.PaymentMethod = CType(TotalDisplay1.FindControl("PaymentMethod"), SelectValControl).SelectedItem.Value

        Dim objItem As DataListItem
        Dim objShip As SelectValControl
        Dim _orderaddress As COrderAddress
        Dim txtSpecialInstruction As TextBox
        Dim txtShipID As TextBox

        For Each objItem In DataList1.Items
            objShip = objItem.FindControl("cboShipping")
            txtShipID = objItem.FindControl("txtShipID")
            txtSpecialInstruction = objItem.FindControl("txtSpecialInstruction")

            For Each _orderaddress In objOrder.OrderAddresses
                If (_orderaddress.Address.ID = txtShipID.Text) Then
                    _orderaddress.Address.Instructions = txtSpecialInstruction.Text
                    _orderaddress.Address.ShipMethod = objShip.SelectedItem.Value
                    Exit For
                End If
            Next
        Next
        Dim strWeb As String = m_objcustomer.GetSessionID

        '#SRS 6.5.3
        If objOrder.BillAddress Is Nothing Then
            Response.Redirect("Billing.aspx?WebID=" & strWeb)
        Else
            Session("Order") = objOrder
            Response.Redirect("Payment.aspx")
        End If

        'Else
        'Me.ErrorMessage.Text = "Please select a shipping carrier"
        'Me.ErrorMessage.Visible = True
        'End If
    End Sub
End Class
