'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports CSR.CSRBusinessRule
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.IO

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class CSRMapShipmentsControl
    Inherits CSRWebControl
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents dlAttributes As System.Web.UI.WebControls.DataList
    Protected WithEvents btnManage As System.Web.UI.WebControls.LinkButton

    Private MapShipmentsOrder As CSROrder
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetOrder()


        MapShipmentsOrder = Session("CSRMapShipmentsOrder")


        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then
            SetSavedAddresses()

            Dim al As ArrayList
            al = GetIndividualItems()

            Items.DataSource = al
            Items.DataBind()

        End If
        SetItems()

    End Sub
    Private Sub SetSavedAddresses()
        Dim al As ArrayList
        al = CSRCustomer.AddressList.Clone
        If al Is Nothing = False AndAlso al.Count > 0 Then
            If al(0).ID = "-1" Then
                al.RemoveAt(0)
            End If

            SavedAddresses.DataSource = al
            SavedAddresses.DataBind()

            Dim x As Integer = 0


            Dim DLItem As DataListItem
            For Each DLItem In SavedAddresses.Items
                CType(DLItem.FindControl("Csraddresslabel2"), CSRAddressLabel).AddressSource = al(x)
                x = x + 1
            Next
        End If
    End Sub
    Private Function CheckAddress() As Boolean
        Dim objRepeaterItem As RepeaterItem
        Dim objDropDown As DropDownList
        For Each objRepeaterItem In Items.Items
            objDropDown = CType(objRepeaterItem.FindControl("DropDownlist2"), DropDownList)
            If objDropDown.Visible = False OrElse (objDropDown.SelectedItem.Value = -1) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub SetItems()

        Dim objRepeaterItem As RepeaterItem
        Dim objGiftWrap As CGiftWrap
        Dim objDropDown As DropDownList
        Dim objAddressLabel As CSRAddressLabel
        Dim objGiftWrapTable As HtmlTable
        Dim objMessageTo As Label
        Dim objMessageFrom As Label
        Dim objMessage As Label
        Dim i As Long = -1
        Dim x As Integer = 0

        'objMultiShip = New CMultiShip
        Dim OrderItems As ArrayList
        OrderItems = GetIndividualItems()
        'objMultiShip.OrderItems = al
        MapShipmentsOrder.OrderAddresses.Clear()
        Dim SelectedAddresses As New ArrayList
        For Each objRepeaterItem In Items.Items
            objDropDown = CType(objRepeaterItem.FindControl("DropDownlist2"), DropDownList)
            objAddressLabel = CType(objRepeaterItem.FindControl("Addresslabel2"), CSRAddressLabel)
            objGiftWrapTable = CType(objRepeaterItem.FindControl("GiftWrapTable"), HtmlTable)
            objMessageTo = CType(objRepeaterItem.FindControl("MessageTo"), Label)
            objMessageFrom = CType(objRepeaterItem.FindControl("MessageFrom"), Label)
            objMessage = CType(objRepeaterItem.FindControl("Message"), Label)
            dlAttributes = objRepeaterItem.FindControl("dlAttributes")

            If (IsNothing(objDropDown.SelectedItem) = False) Then
                SelectedAddresses.Add(objDropDown.SelectedItem.Value)
                If (objDropDown.SelectedItem.Value <> -1) Then
                    If (objDropDown.Items(0).Value = "-1") Then
                        objDropDown.Items.RemoveAt(0)
                    End If
                    Dim Address As Address
                    Address = GetAddress(objDropDown.SelectedItem.Value, CSRCustomer)
                    objAddressLabel.AddressSource = Address
                    Dim OrderItem As CSROrderItem
                    OrderItem = CType(OrderItems(x), CSROrderItem)
                    OrderItem.AddressID = Address.ID

                    MapShipmentsOrder.AddCSROrderAddress(SetCSRItemToAddress(OrderItems, objRepeaterItem.ItemIndex, objDropDown.SelectedItem.Value, CSRCustomer))
                Else
                    Dim SelectedAddresses2 As ArrayList = Session("CSRSelectedAddresses")
                    If IsNothing(SelectedAddresses2) Then
                        Dim OrderItem As CSROrderItem
                        OrderItem = CType(OrderItems(x), CSROrderItem)
                        i = OrderItem.AddressID
                    Else
                        Dim DDItem As ListItem
                        i = -1
                        For Each DDItem In objDropDown.Items
                            If SelectedAddresses2 Is Nothing = False AndAlso SelectedAddresses2.Count > 0 Then
                                If DDItem.Value = SelectedAddresses2(x) Then
                                    i = SelectedAddresses2(x)  'this makes sure that the selected Address wasn't deleted when they went to address book
                                End If
                            End If
                        Next

                    End If
                    If (i <> -1) Then
                        If (objDropDown.Items(0).Value = "-1") Then
                            objDropDown.Items.RemoveAt(0)
                        End If

                        SetFromOrder(objDropDown, i)
                        objAddressLabel.AddressSource = GetAddress(i, CSRCustomer)
                    End If
                End If
            End If

            dlAttributes.DataSource = CType(OrderItems(objRepeaterItem.ItemIndex), CSROrderItem).Attributes
            dlAttributes.DataBind()

            If (DisplayGiftWrapTable(OrderItems, objRepeaterItem.ItemIndex)) Then
                objGiftWrapTable.Visible = True

                objGiftWrap = CType(OrderItems(objRepeaterItem.ItemIndex), CSROrderItem).GiftWraps(0)

                objMessageTo.Text = objGiftWrap.MessageTo
                objMessageFrom.Text = objGiftWrap.MessageFrom
                objMessage.Text = objGiftWrap.Message
            Else
                objGiftWrapTable.Visible = False
            End If
            x = x + 1
        Next
        Session("CSRSelectedAddresses") = SelectedAddresses
    End Sub
    Public Function DisplayGiftWrapTable(ByVal OrderItems As ArrayList, ByVal nItemIndex As Long) As Boolean
        Dim objOrderItem As CSROrderItem = OrderItems.Item(nItemIndex)

        If (objOrderItem.GiftWrapQty = 0) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetAddress(ByVal nID As Long, ByVal objCustomer As CCustomer) As Address
        Dim objAddress As Address
        For Each objAddress In objCustomer.AddressList
            If (objAddress.ID = nID) Then
                Return objAddress
            End If
        Next
        Return Nothing
    End Function
    Public Function SetCSRItemToAddress(ByVal alItems As ArrayList, ByVal nItemIndex As Long, ByVal nAddressID As Long, ByVal objCustomer As CCustomer) As CSROrderAddress
        Dim objAddress As Address
        Dim objOrderAddress As CSROrderAddress
        Dim objOrderItem As CSROrderItem = alItems.Item(nItemIndex)

        For Each objAddress In objCustomer.AddressList
            If (objAddress.ID = nAddressID) Then
                objOrderAddress = New CSROrderAddress(objAddress)
                objOrderAddress.AddCSRItem(objOrderItem)
                Return objOrderAddress
            End If
        Next
        Return Nothing
    End Function
#Region "Sub SetFromOrder(ByRef objDropDown As DropDownList, ByVal nIndex As Long)"
    Private Sub SetFromOrder(ByRef objDropDown As DropDownList, ByVal nIndex As Long)
        Dim objDropDownItem As ListItem

        For Each objDropDownItem In objDropDown.Items
            If (objDropDownItem.Value = nIndex) Then
                objDropDownItem.Selected = True
            Else
                objDropDownItem.Selected = False
            End If
        Next
    End Sub
#End Region


    Private Sub Items_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Items.ItemDataBound
        Dim objDropDown As DropDownList
        Dim objbtnManage As HyperLink
        Dim objImgManage As System.Web.UI.WebControls.Image
        Dim objAddress As Address
        Dim objAddresses As ArrayList
        Dim bAdd As Boolean = True

        objDropDown = CType(e.Item.FindControl("DropDownlist2"), DropDownList)
        objbtnManage = CType(e.Item.FindControl("btnNewManage"), HyperLink)
        objImgManage = CType(e.Item.FindControl("imgNewManage"), System.Web.UI.WebControls.Image)

        If (IsNothing(objDropDown) = False) Then
            objAddresses = CSRCustomer.AddressList.Clone

            If (objAddresses.Count = 0) Then
                objbtnManage.Visible = True
                objImgManage.Visible = True
                objDropDown.Visible = False
            Else
                objbtnManage.Visible = False
                objImgManage.Visible = False
                objDropDown.Visible = True
                For Each objAddress In objAddresses
                    If (objAddress.ID = -1) Then
                        bAdd = False
                        Exit For
                    End If
                Next
                If (bAdd) Then
                    objAddress = New Address
                    objAddress.NickName = "Select An Address"
                    objAddress.ID = -1
                    objAddresses.Insert(0, objAddress)
                End If

                objDropDown.DataSource = objAddresses
                objDropDown.DataBind()
            End If
        End If
    End Sub


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        If (CheckAddress() = False) Then
            MagicAjax.AjaxCallHelper.WriteAlert("Please select an address for every item.")
        Else

            SetItems()
            Session("CSRSelectedAddresses") = Nothing

            Session("CSRMapShipmentsOrder") = Nothing
            If MapShipmentsOrder.OrderAddresses.Count = 1 Then
                Session("CSRShipAddress") = MapShipmentsOrder.OrderAddresses(0).Address.ID
                Dim OrderItem As CSROrderItem
                For Each OrderItem In MapShipmentsOrder.OrderAddresses(0).orderitems
                    OrderItem.AddressID = -1
                Next
            End If
            Dim objDiscounts As New CStoreDiscounts
            MapShipmentsOrder.StoreDiscounts = objDiscounts.GetDiscounts()
            MapShipmentsOrder.Coupons = m_objxmlcart.AppliedDiscounts
            MapShipmentsOrder.SetPackageIndexes()
            Session("csrOrder") = MapShipmentsOrder
            MagicAjax.AjaxCallHelper.Write("ClosePage();")
        End If

    End Sub


    Private Sub dlAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAttributes.ItemCreated
        Dim objAtt As CAttribute = e.Item.DataItem
        Dim oAttDetail As CAttributeDetail
        Dim oLbl As Label
        Try
            If IsNothing(objAtt) = False Then
                oAttDetail = objAtt.AttributeDetails.Item(0)
                If IsNothing(oAttDetail) = False Then
                    oLbl = e.Item.FindControl("AttDetail")
                    If IsNothing(oLbl) = False Then
                        If objAtt.AttributeType = tAttributeType.Custom Then
                            oLbl.Text = ": " & oAttDetail.Customor_Custom_Description
                        Else
                            oLbl.Text = ": " & oAttDetail.Name
                        End If
                    End If
                End If
            End If

        Catch err As SystemException
            ErrorMessage.Text = err.Message
            ErrorMessage.Visible = True
        End Try
    End Sub
End Class
