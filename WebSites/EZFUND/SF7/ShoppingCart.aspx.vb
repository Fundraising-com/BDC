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
Imports StoreFront.SystemBase

Partial Class ShoppingCart
    Inherits CWebPage
    Protected WithEvents CCartControl1 As CCartControl
    Protected WithEvents SalesDiscount1 As SalesDiscount
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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
            SetPageTitle = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)

            TotalDisplay1.OandaISO = Session("ConvertISO")
            TotalDisplay1.OandaRate = CDec(Session("OandaRate"))
            'sp7
            btnPayPal.Attributes.Add("OnClick", "return chkPayPal();")
            'Check Admin Settings for PayPalExpress
            Dim objAdminInfo As New CAdminGeneralManagement
            If objAdminInfo.AcceptPayPalExpress = True Then
                pnlPayPal.Visible = True
            Else
                pnlPayPal.Visible = False
            End If

            If (IsNothing(Session("EmailedAFriend")) = False) Then
                Message.Text = Session("EmailedAFriend")
                Message.Visible = True
                Session("EmailedAFriend") = Nothing
            Else
                Message.Text = ""
                Message.Visible = False
            End If

            If (m_objXMLCart.Count = 0) Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "NoItems", "NoItems")
                ErrorMessage.Visible = True
                Table3.Visible = False
                'sp7
                pnlPayPal.Visible = False
            ElseIf (IsNothing(Request.QueryString("PayPalError")) = False) Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "Error", "PayPalError")
                ErrorMessage.Visible = True
            ElseIf (IsNothing(Request.QueryString("ProfileError")) = False) Then
                ErrorMessage.Text = Session("ProfileError")
                ErrorMessage.Visible = True
                Session.Remove("ProfileError")
            Else
                ErrorMessage.Visible = False
            End If

            If Not Session("OrderSubmitErrorMessage") Is Nothing Then
                OrderErrorMessage.Text = Session("OrderSubmitErrorMessage").ToString()
                OrderErrorMessage.Visible = True
                Session("OrderSubmitErrorMessage") = Nothing
            End If

            InventoryBackOrderConfirm(CType(FindControl("myhiddenfield"), HtmlInputHidden).Value)

            'If Not Page.IsPostBack Then
            CCartControl1.CartDataArray = m_objxmlcart.GetCartItems()
            Dim arrItems As ArrayList
            Dim objCartItem As CCartItem
            Dim cnt As Integer = 0

            arrItems = m_objxmlcart.GetCartItems()
            Dim dynCartControl As UITools.DynamicCartDisplay
            dynCartControl = CType(CCartControl1.FindControl("DynaCart"), UITools.DynamicCartDisplay)
            dynCartControl.DisplayMultiShipCheck = False
            For Each objCartItem In arrItems
                If objCartItem.IsShipable Then
                    cnt = cnt + objCartItem.Quantity
                    If cnt > 1 Then
                        'only show if more than one item is shippable
                        dynCartControl.DisplayMultiShipCheck = True
                        Exit For
                    End If
                End If
            Next
            TotalDisplay1.DataSource = m_objxmlcart
            TotalDisplay1.DataBind()
            'End If

            'Check Coupons
            If (m_objxmlcart.AppliedDiscounts.Count > 0) Then
                CheckCoupons()
            End If

            If (IsNothing(Session("ItemAdded")) = False) Then
                SetMessage(Message)
            Else
                Message.Text = ""
                Message.Visible = False
            End If
            imgUpdateQty.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("UpdateQuantity").Attributes("Filepath").Value
            Me.imgContinueShopping.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("ContinueShopping").Attributes("Filepath").Value
            imgCheckout.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CheckOut").Attributes("Filepath").Value
            btnUpdateQty.Attributes.Add("onclick", "return SetValidationUpdateQty(" & m_objxmlcart.GetCartItems.Count & ");")
            CType(SalesDiscount1.FindControl("btnApply"), LinkButton).Attributes.Add("onclick", "return SetValidationCoupon();")

        Catch ex As Exception
            Session("DetailError") = "Class ShoppingCart Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
    Public Sub ContentTableVisible(ByVal bVisible As Boolean)
        Table3.Visible = bVisible
    End Sub

    Private Sub btnUpdateQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateQty.Click
        CCartControl1.UpdateQuantity()

        If (m_objXMLCart.Count = 0) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "NoItems", "NoItems")
            ErrorMessage.Visible = True
            Table3.Visible = False
            'sp7
            pnlPayPal.Visible = False
            Exit Sub
        Else
            ErrorMessage.Visible = False
        End If

        If (CCartControl1.DynaCart.ErrorMessage <> "") Then
            ErrorMessage.Text = CCartControl1.DynaCart.ErrorMessage
            ErrorMessage.Visible = True
        End If

        ' Check Coupons
        If (m_objxmlcart.AppliedDiscounts.Count > 0) Then
            CheckCoupons()
        End If

        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()

        UpdateCartList()
        Dim arrItems As ArrayList
        Dim objCartItem As CCartItem
        Dim cnt As Integer = 0
        Dim dynCartControl As UITools.DynamicCartDisplay
        dynCartControl = CType(CCartControl1.FindControl("DynaCart"), UITools.DynamicCartDisplay)
        dynCartControl.DisplayMultiShipCheck = False
        arrItems = m_objxmlcart.GetCartItems()
        For Each objCartItem In arrItems
            If objCartItem.IsShipable Then
                cnt = cnt + objCartItem.Quantity
                If cnt > 1 Then
                    'only show if more than one item is shippable
                    dynCartControl.DisplayMultiShipCheck = True
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub btnCheckout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckout.Click
        Dim strWebID As String = m_objcustomer.GetSessionID()

        ' Write the Shopping cart to the DB
        m_objcustomer.UpdateOanda(Session("OandaRate"), Session("ConvertISO"))
        m_objXMLCart.SaveToDB()
        Dim strReferrer As String
        strReferrer = CStr(m_objcustomer.Referer)

        Session("XMLShoppingCart") = Nothing
        'Tee 11/15/2007 removed unnecessary session variable
        Session.Remove("ItemAdded")
        'end Tee
        m_objxmlcart = Nothing
        m_objCustomer = Nothing
        Session("Customer") = Nothing

        Dim objCheck As CheckBox
        objCheck = CCartControl1.DynaCart.FindControl("MultiShip")

        Response.Expires = 0
        Response.Buffer = True
        Response.Clear()

        ' begin: JDB - Google Analytics
        If StoreFrontConfiguration.GoogleAnalyticsID.Length <= 0 Then
            If (IsNothing(objCheck) = False) Then
                If strReferrer = "0" Then
                    If (objCheck.Checked = False) Then
                        Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID)
                        'Response.Redirect("Admin/Shipping.aspx")
                    Else
                        Response.Redirect(StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID)
                        'Response.Redirect("Admin/MultiShip.aspx")
                    End If
                Else
                    If (objCheck.Checked = False) Then
                        Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                        'Response.Redirect("Admin/Shipping.aspx")
                    Else
                        Response.Redirect(StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                        'Response.Redirect("Admin/MultiShip.aspx")
                    End If
                End If

            Else
                If strReferrer = "0" Then
                    Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID)
                Else
                    Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                End If

            End If
        Else
            If (IsNothing(objCheck) = False) Then
                If strReferrer = "0" Then
                    If (objCheck.Checked = False) Then
                        '*** Old Code
                        'Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID)
                        '*** New Code
                        Session("myRedir") = StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID
                        Response.Redirect("shoppingcartredir.aspx")
                        '*** End New Code
                        'Response.Redirect("Admin/Shipping.aspx")
                    Else
                        '*** Old Code
                        'Response.Redirect(StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID)
                        '*** New Code
                        Session("myRedir") = StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID
                        Response.Redirect("shoppingcartredir.aspx")
                        '*** End New Code
                        'Response.Redirect("Admin/MultiShip.aspx")
                    End If
                Else
                    If (objCheck.Checked = False) Then
                        '*** Old Code
                        '   Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                        '*** New Code
                        Session("myRedir") = StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer
                        Response.Redirect("shoppingcartredir.aspx")
                        '*** End New Code
                        'Response.Redirect("Admin/Shipping.aspx")
                    Else
                        '*** Old Code
                        '   Response.Redirect(StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                        '*** New Code
                        Session("myRedir") = StoreFrontConfiguration.SSLPath & "MultiShip.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer
                        Response.Redirect("shoppingcartredir.aspx")
                        '*** End New Code
                        'Response.Redirect("Admin/MultiShip.aspx")
                    End If
                End If

            Else
                If strReferrer = "0" Then
                    '*** Old Code
                    '  Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID)
                    '*** New Code
                    Session("myRedir") = StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID
                    Response.Redirect("shoppingcartredir.aspx")
                    '*** End New Code
                Else
                    '*** Old Code
                    '  Response.Redirect(StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer)
                    '*** New Code
                    Session("myRedir") = StoreFrontConfiguration.SSLPath & "Shipping.aspx?WebID=" & strWebID & "&Affiliate=" & strReferrer
                    Response.Redirect("shoppingcartredir.aspx")
                    '*** End New Code
                End If

            End If
        End If
        ' end: JDB - Google Analytics
    End Sub


    Private Sub CCartControl1_RemovedCartItem(ByVal sender As Object, ByVal e As System.EventArgs) Handles CCartControl1.RemovedCartItem
        If (m_objXMLCart.Count = 0) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "NoItems", "NoItems")
            ErrorMessage.Visible = True
            Table3.Visible = False
            'sp7
            pnlPayPal.Visible = False
        Else
            ErrorMessage.Visible = False
        End If
        ' Check Coupons
        If (m_objxmlcart.AppliedDiscounts.Count > 0) Then
            CheckCoupons()
        End If

        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()

        UpdateCartList()
    End Sub

    Private Sub SalesDiscount1_CouponAdd(ByVal sender As Object, ByVal e As System.EventArgs) Handles SalesDiscount1.CouponAdd
        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()
        UpdateCartList()
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub

    Private Sub SalesDiscount1_CouponError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SalesDiscount1.CouponError
        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()
        UpdateCartList()
        ErrorMessage.Text = sender
        ErrorMessage.Visible = True
    End Sub

    Protected Sub CheckCoupons()
        Dim objCoupon As CDiscount
        Dim ar As New ArrayList

        Dim objStoreDiscounts As New CStoreDiscounts
        Dim m_objDiscounts As CDiscounts = objStoreDiscounts.GetDiscounts()

        For Each objCoupon In m_objxmlcart.AppliedDiscounts
            If (m_objDiscounts.CheckCoupon(objCoupon, m_objxmlcart.GetCartItems) = False) Then
                ' Remove Coupon
                ar.Add(objCoupon)
                ErrorMessage.Text = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "Coupons", "Remove")
                ErrorMessage.Visible = True
            End If
        Next
        For Each objCoupon In ar
            m_objxmlcart.DeleteCoupon(objCoupon)
        Next

        SalesDiscount1.ReloadList()
        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()
        UpdateCartList()
    End Sub

    Private Sub SalesDiscount1_CouponRemove(ByVal sender As Object, ByVal e As System.EventArgs) Handles SalesDiscount1.CouponRemove
        SalesDiscount1.ReloadList()
        TotalDisplay1.DataSource = m_objxmlcart
        TotalDisplay1.DataBind()
        UpdateCartList()
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
        Else
            Message.Text = ""
            Message.Visible = False
        End If
    End Sub
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>Call the SetExpressCheckout
    ' </summary>
    ' <remarks>this would cause the request to be transferred to the HandlePayPal 
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	05/10/2005
    '		Last Revised on :
    ' </history>
    ' -----------------------------------------------------------------------------
    Private Sub btnPayPal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayPal.Click
        Try
            If m_objXMLCart.OrderTotal <= 0 Then
                ErrorMessage.Visible = True
                ErrorMessage.Text = "PayPal does not accept Order Total less than 0. <br> Please click the Store Checkout button"
                Exit Sub
            Else
                Dim strWebID As String = m_objcustomer.GetSessionID()

                ' Write the Shopping cart to the DB
                m_objcustomer.UpdateOanda(Session("OandaRate"), Session("ConvertISO"))
                m_objXMLCart.SaveToDB()
                Dim strReferrer As String
                strReferrer = CStr(m_objcustomer.Referer)

                Session("XMLShoppingCart") = Nothing
                'Tee 11/15/2007 removed unnecessary session variable
                Session.Remove("ItemAdded")
                'end Tee
                m_objxmlcart = Nothing
                m_objCustomer = Nothing
                Session("Customer") = Nothing

                Dim objCheck As CheckBox
                objCheck = CCartControl1.DynaCart.FindControl("MultiShip")

                Response.Expires = 0
                Response.Buffer = True
                Response.Clear()
                'Response.Redirect(StoreFrontConfiguration.SSLPath & "HandlePayPalExpress.aspx?From=SC", True)
                If strReferrer = "0" Then
                    Response.Redirect(StoreFrontConfiguration.SSLPath & "HandlePayPalExpress.aspx?From=SC&WebID=" & strWebID)
                Else
                    Response.Redirect(StoreFrontConfiguration.SSLPath & "HandlePayPalExpress.aspx?From=SC&WebID=" & strWebID & "&Affiliate=" & strReferrer)
                End If
            End If
        Catch ex As Exception When Not TypeOf ex Is Threading.ThreadAbortException
            Session("DetailError") = "Class ShoppingCart Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

    End Sub
    'End SP7
End Class
