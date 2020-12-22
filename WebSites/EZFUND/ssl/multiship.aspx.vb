'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

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
Imports StoreFront.BusinessRule.Orders

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Public Class MultiShip
    Inherits CWebPage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents btnManage As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgManage As System.Web.UI.WebControls.Image
    Protected WithEvents imgContinue As System.Web.UI.WebControls.Image
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnContinue As System.Web.UI.WebControls.LinkButton
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents dlAttributes As System.Web.UI.WebControls.DataList
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

        Dim objMultiShip As New CMultiShip()
        Dim objItemList As ArrayList
        Dim objOrder As COrder
        Dim objDataGridItem As DataGridItem
        Dim objGiftWrap As CGiftWrap
        Dim objDropDown As DropDownList
        Dim objAddressLabel As AddressLabel
        Dim objGiftWrapTable As HtmlTable
        Dim objMessageTo As Label
        Dim objMessageFrom As Label
        Dim objMessage As Label

        SetPageTitle = m_objMessages.GetXMLMessage("MultiShip.aspx", "PageTitle", "Title")
        SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)

        If (m_objcustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            If (Request.QueryString("Affiliate") <> "") Then
                Response.Redirect("CustSignInCheckout.aspx?ReturnPage=MultiShip.aspx&WebID=" & Request.QueryString("WebID") & "&Affiliate=" & Request.QueryString("Affiliate"))
            Else
                Response.Redirect("CustSignInCheckout.aspx?ReturnPage=MultiShip.aspx&WebID=" & Request.QueryString("WebID"))
            End If
        End If

        Try
            If (Not IsPostBack) Then
                'Dim objGiftCertificates As New CStoreGiftCertificates()
                m_objXMLCart.CustomerGroup = m_objcustomer.CustomerGroup
                m_objXMLCart.LoadFromDB() ', objGiftCertificates.GetGiftCertificates())
            End If
            CheckCart()
            ' Set OrderItem to OrderAddress on the Change Event of the DropDown List
            objItemList = objMultiShip.GetMultiShipItems(m_objxmlcart)

            If Not IsPostBack And Request.QueryString("Return") = "" Then
                ' Start the InMemory Order
                Session("Order") = objMultiShip.StartMultiShipOrder(m_objcustomer)

                Repeater1.DataSource = objItemList
                Repeater1.DataBind()
            ElseIf (IsPostBack = False And Repeater1.Items.Count = 0) Then
                Repeater1.DataSource = objItemList
                Repeater1.DataBind()
            End If

            objOrder = Session("Order")

            If (IsPostBack) Then
                objMultiShip.ClearAddresses(objOrder, m_objcustomer)
                If (objOrder.OrderAddresses.Count = 0) Then
                    Session("Order") = objMultiShip.StartMultiShipOrder(m_objcustomer)
                    objOrder = Session("Order")
                End If
            End If

            SetItems(objMultiShip)

            Session("Order") = objOrder
            imgManage.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("ManageAddresses").Attributes("Filename").Value
            imgContinue.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filename").Value
            Dim con As RepeaterItem
            For Each con In Repeater1.Items
                CType(con.FindControl("imgNewManage"), System.Web.UI.WebControls.Image).ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("AddAddress").Attributes("Filename").Value
            Next
            btnContinue.Attributes.Add("onclick", "return SetValidation(" & Repeater1.Items.Count & ");")
        Catch ex As Exception
            Session("DetailError") = "Class MultiShip Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Function CheckAddress() As Boolean
        Dim objRepeaterItem As RepeaterItem
        Dim objDropDown As DropDownList
        For Each objRepeaterItem In Repeater1.Items
            objDropDown = CType(objRepeaterItem.FindControl("DropDownlist2"), DropDownList)
            If (objDropDown.SelectedItem.Value = -1) Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub SetItems(Optional ByVal objMultiShip As CMultiShip = Nothing)
        Dim objOrder As COrder
        Dim objRepeaterItem As RepeaterItem
        Dim objGiftWrap As CGiftWrap
        Dim objDropDown As DropDownList
        Dim objAddressLabel As AddressLabel
        Dim objGiftWrapTable As HtmlTable
        Dim objMessageTo As Label
        Dim objMessageFrom As Label
        Dim objMessage As Label
        Dim i As Long

        If (IsNothing(objMultiShip)) Then
            objMultiShip = New CMultiShip()
            objMultiShip.GetMultiShipItems(m_objxmlcart)
        End If

        objOrder = Session("Order")

        For Each objRepeaterItem In Repeater1.Items
            objDropDown = CType(objRepeaterItem.FindControl("DropDownlist2"), DropDownList)
            objAddressLabel = CType(objRepeaterItem.FindControl("Addresslabel2"), AddressLabel)
            objGiftWrapTable = CType(objRepeaterItem.FindControl("GiftWrapTable"), HtmlTable)
            objMessageTo = CType(objRepeaterItem.FindControl("MessageTo"), Label)
            objMessageFrom = CType(objRepeaterItem.FindControl("MessageFrom"), Label)
            objMessage = CType(objRepeaterItem.FindControl("Message"), Label)
            dlAttributes = objRepeaterItem.FindControl("dlAttributes")
           
            If (IsNothing(objDropDown.SelectedItem) = False) Then
                If (objDropDown.SelectedItem.Value <> -1) Then
                    If (objDropDown.Items(0).Value = "-1") Then
                        objDropDown.Items.RemoveAt(0)
                    End If
                    objAddressLabel.AddressSource = objMultiShip.GetAddress(objDropDown.SelectedItem.Value, m_objcustomer)
                    objOrder.AddOrderAddress(objMultiShip.SetItemToAddress(objRepeaterItem.ItemIndex, objDropDown.SelectedItem.Value, m_objcustomer))
                ElseIf (objOrder.OrderAddresses.Count > 1) Then
                    i = objMultiShip.GetAddressID(objRepeaterItem.ItemIndex, objOrder)
                    If (i <> -1) Then
                        SetFromOrder(objDropDown, i)
                        objAddressLabel.AddressSource = objMultiShip.GetAddress(i, m_objcustomer)
                    End If
                End If
            End If

            dlAttributes.DataSource = objMultiShip.Attributes(objRepeaterItem.ItemIndex)
            dlAttributes.DataBind()

            If (objMultiShip.DisplayGiftWrapTable(objRepeaterItem.ItemIndex)) Then
                objGiftWrapTable.Visible = True

                objGiftWrap = objMultiShip.GetGiftWrap(objRepeaterItem.ItemIndex)

                objMessageTo.Text = objGiftWrap.MessageTo
                objMessageFrom.Text = objGiftWrap.MessageFrom
                objMessage.Text = objGiftWrap.Message
            Else
                objGiftWrapTable.Visible = False
            End If

        Next
    End Sub

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

    Private Sub Repeater1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand
        If (CType(e.CommandSource, WebControl).ID = "btnNewManage") Then
            Response.Redirect("CustAddressBook.aspx?ReturnPage=MultiShip.aspx")
        End If
    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim objDropDown As DropDownList
        Dim objbtnManage As LinkButton
        Dim objImgManage As System.Web.UI.WebControls.Image
        Dim objAddress As Address
        Dim objAddresses As ArrayList
        Dim bAdd As Boolean = True

        objDropDown = CType(e.Item.FindControl("DropDownlist2"), DropDownList)
        objbtnManage = CType(e.Item.FindControl("btnNewManage"), LinkButton)
        objImgManage = CType(e.Item.FindControl("imgNewManage"), System.Web.UI.WebControls.Image)

        If (IsNothing(objDropDown) = False) Then
            objAddresses = m_objcustomer.AddressList

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
                    objAddress = New Address()
                    objAddress.NickName = "Select An Address"
                    objAddress.ID = -1
                    objAddresses.Insert(0, objAddress)
                End If

                objDropDown.DataSource = objAddresses
                objDropDown.DataBind()
            End If
        End If
    End Sub

    Private Sub btnManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManage.Click
        Response.Redirect("CustAddressBook.aspx?ReturnPage=MultiShip.aspx")
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim objOrder As New COrder(m_objCustomer)
        objOrder = Session("Order")
        If (objOrder.OrderAddresses.Count > 1) Then
            objOrder.OrderAddresses.RemoveAt(0)
            'objOrder.XMLProductAccess = m_objXMLAccess
            Dim objDiscounts As New CStoreDiscounts()
            objOrder.StoreDiscounts = objDiscounts.GetDiscounts()

            objOrder.Coupons = m_objxmlcart.AppliedDiscounts
            'objOrder.GiftCertificates = m_objxmlcart.AppliedGiftCertificates

            If (CheckAddress() = False) Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("MultiShip.aspx", "Continue", "NotAllShipped")
                ErrorMessage.Visible = True
            Else
                Response.Redirect("ShipSummary.aspx")
            End If
        Else
            ErrorMessage.Text = m_objMessages.GetXMLMessage("MultiShip.aspx", "Continue", "NotAllShipped")
            ErrorMessage.Visible = True
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
