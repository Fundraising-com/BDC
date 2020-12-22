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

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class SavedCart
    Inherits CWishListSavedCartBase
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
        Dim arSaveditems As New ArrayList
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        DynamicCartDisplay1.NotPostBack = False

        If (m_objcustomer.IsSignedIn = False) Then
            Response.Redirect("CustSignIn.aspx?ReturnPage=SavedCart.aspx")
            'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            'ElseIf Not IsNothing(Session("anonymous")) Then
            '    ' anonymous user should not be able to access my acct page
            '    ' redirect to sign in page
            '    Response.Redirect("CustSignIn.aspx?SignOut=2&ReturnPage=SavedCart.aspx")
            'End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

        End If
        Message.Text = String.Empty
        Try
            If (IsNothing(Session("AddSavedItem")) = False) Then
                Dim objCart As CGenericCart = m_objcustomer.SavedCart
                objCart.AddCartItem(Session("AddSavedItem"))
                m_objcustomer.UpdateSavedCart(objCart)
                Session("AddSavedItem") = Nothing
                Message.Text = Message.Text & m_objMessages.GetXMLMessage("SavedCart.aspx", "Message", "Add")
            End If

            SetPageTitle = m_objMessages.GetXMLMessage("SavedCart.aspx", "PageTitle", "Title")
            lblHeading.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "SubHeading", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)
            DynamicCartDisplay1.NegativeMessage = m_objMessages.GetXMLMessage("SavedCart.aspx", "Error", "NegativeQty")
            'Begin Mod 7/06 - Junu
            arSaveditems = m_objcustomer.SavedCart.CartItems
            If (arSaveditems.Count = 0) Then
                If (m_objcustomer.WishListSavedCartMessage <> "") Then
                    Message.Text = m_objcustomer.WishListSavedCartMessage & "<br>"
                Else
                    Message.Text = ""
                End If
                Message.Text = Message.Text & m_objMessages.GetXMLMessage("SavedCart.aspx", "NoItems", "NoItems")
                Table3.Visible = False
            Else
                DynamicCartDisplay1.DataSource = arSaveditems  'm_objcustomer.SavedCart.CartItems()
                DynamicCartDisplay1.DataBind()
                If (Message.Text <> "") Then
                    Message.Text = Message.Text & "<br>"
                End If
                Message.Text = Message.Text & m_objcustomer.WishListSavedCartMessage
            End If
            'end mod 7/06

            If (IsNothing(Session("EMailWishListMessage")) = False) Then
                Message.Text = Session("EMailWishListMessage")
                Session("EMailWishListMessage") = Nothing
            Else
                Message.Visible = False
            End If

            If (Message.Text <> "") Then
                Message.Visible = True
            Else
                Message.Visible = False
            End If
            imgUpdate.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("UpdateQuantity").Attributes("Filepath").Value
            imgEmailList.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value

            DynamicCartDisplay1.RemoveImg = dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filepath").Value
            DynamicCartDisplay1.ReOrderImg = dom.Item("SiteProducts").Item("SiteImages").Item("ReOrder").Attributes("Filepath").Value
            DynamicCartDisplay1.SavedCartImg = dom.Item("SiteProducts").Item("SiteImages").Item("SaveCart").Attributes("Filepath").Value
            DynamicCartDisplay1.GiftWrapImg = dom.Item("SiteProducts").Item("SiteImages").Item("GiftWrap").Attributes("Filepath").Value
            DynamicCartDisplay1.BuyNowImg = dom.Item("SiteProducts").Item("SiteImages").Item("BuyNow").Attributes("Filepath").Value
            btnUpdate.Attributes.Add("onclick", "return SetValidationUpdateQty(" & m_objcustomer.SavedCart.CartItems.Count & ");")
            InventoryBackOrderConfirm(CType(FindControl("myhiddenfield"), HtmlInputHidden).Value)
        Catch ex As Exception
            If TypeOf ex Is Threading.ThreadAbortException Then Exit Sub
            Session("DetailError") = "Class SavedCart Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objCart As CGenericCart = m_objcustomer.SavedCart
        Dim objItem As CGenericCartItem
        Dim txtQty As TextBox
        Dim lngItem As Long = 1

        For Each objItem In objCart.CartItems
            txtQty = DynamicCartDisplay1.FindControl("Qty" & lngItem)
            If (CLng(txtQty.Text) > 0) Then
                objItem.Quantity = CLng(txtQty.Text)
                objCart.UpdateCartItem(objItem)
            ElseIf (CLng(txtQty.Text) = 0) Then
                objCart.DeleteCartItem(objItem)
            Else
                objCart.UpdateCartItem(objItem)
            End If
            lngItem = lngItem + 1
        Next

        If (DynamicCartDisplay1.ErrorMessage <> "") Then
            ErrorMessage.Text = DynamicCartDisplay1.ErrorMessage
            ErrorMessage.Visible = True
        End If

        m_objcustomer.UpdateSavedCart(objCart)

        If (m_objcustomer.SavedCart.CartItems.Count = 0) Then
            Message.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "NoItems", "NoItems")
            Message.Visible = True
            Table3.Visible = False
        Else
            DynamicCartDisplay1.DataSource = m_objcustomer.SavedCart.CartItems()
            DynamicCartDisplay1.DataBind()
            Message.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "Message", "Update")
            Message.Visible = True
        End If
    End Sub

    Private Sub DynamicCartDisplay1_RemoveBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynamicCartDisplay1.RemoveBtnClick
        Dim objCart As CGenericCart = m_objcustomer.SavedCart
        Dim objItem As CGenericCartItem
        Dim lngItem As Long = 1
        For Each objItem In objCart.CartItems
            If (lngItem = sender) Then
                objCart.DeleteCartItem(objItem)
                Exit For
            End If
            lngItem = lngItem + 1
        Next

        m_objcustomer.UpdateSavedCart(objCart)

        If (m_objcustomer.SavedCart.CartItems.Count = 0) Then
            Table3.Visible = False
            Message.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "NoItems", "NoItems")
            Message.Visible = True
        Else
            DynamicCartDisplay1.NotPostBack = True
            DynamicCartDisplay1.DataSource = m_objcustomer.SavedCart.CartItems()
            DynamicCartDisplay1.DataBind()
            Message.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "Message", "Remove")
            Message.Visible = True
        End If
    End Sub

    Private Sub DynamicCartDisplay1_BuyNowBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynamicCartDisplay1.BuyNowBtnClick
        Dim objCart As CGenericCart = m_objcustomer.SavedCart
        Dim objItem As CGenericCartItem
        Dim lngItem As Long = 1
        Dim objButton As New LinkButton()
        'Session("OrderAttributes") = Nothing

        For Each objItem In objCart.CartItems
            objCart.UpdateCartItem(objItem)
            If (lngItem = sender) Then
                ' Add to Shopping Cart
                If (objItem.Quantity > 0) Then
                    objButton.CommandName = objItem.ProductID
                    objButton.CommandArgument = objItem.Quantity
                    If objItem.Attributes.Count > 0 Then
                        Dim m_OrderAttributes As New ArrayList()
                        Dim objAtt As CAttribute
                        For Each objAtt In objItem.Attributes
                            Dim oAttStorage As New CAttributesSelected()
                            Dim objDetail As CAttributeDetail
                            objDetail = objAtt.AttributeDetails.Item(0)
                            oAttStorage.AttributeId = objAtt.UID
                            oAttStorage.UID = objDetail.UID
                            oAttStorage.Customor_Custom_Description = objDetail.Customor_Custom_Description
                            m_OrderAttributes.Add(oAttStorage)
                        Next
                        Session("OrderAttributes") = m_OrderAttributes
                    Else
                        '2416
                        Session("OrderAttributes") = New ArrayList
                    End If
                    'Dim oItem As New CCartItem(objItem, objItem.Quantity)
                    'm_objxmlcart.AddItem(oItem)
                    objCart.DeleteCartItem(objItem)
                End If
                'Exit For
            End If
            lngItem = lngItem + 1
        Next

        m_objcustomer.UpdateSavedCart(objCart)

        AddItemToCart(objButton, e)

        If (m_objcustomer.SavedCart.CartItems.Count = 0) Then
            Table3.Visible = False
            Message.Text = Message.Text & m_objMessages.GetXMLMessage("SavedCart.aspx", "NoItems", "NoItems")
            Message.Visible = True
        Else
            DynamicCartDisplay1.NotPostBack = True
            DynamicCartDisplay1.DataSource = m_objcustomer.SavedCart.CartItems()
            DynamicCartDisplay1.DataBind()
            'Message.Text = m_objMessages.GetXMLMessage("SavedCart.aspx", "Message", "BuyNow")
            'Message.Visible = True
        End If
    End Sub

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
        Else
            Message.Text = ""
            Message.Visible = False
        End If
    End Sub


End Class
