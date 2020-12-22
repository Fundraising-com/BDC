'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.Xml

Partial Class InventoryMessage

    Inherits CWebPage
    Private _Message As String

    Event AddItem As EventHandler

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
            If Not IsPostBack Then
                Dim oItem As CCartItem
                Dim sReturn As String = " window.close();"
                Dim sPage As String = Request.QueryString("sPage")
                If (Not (Session("arIventoryInfo") Is Nothing)) And sPage.ToLower.StartsWith("shoppingcart.aspx") Then
                    Dim ar As New ArrayList()
                    ar = Session("arIventoryInfo")
                    oItem = CType(ar(0), CCartItem)
                    If oItem.Inventory.CanBackOrder Then
                        'ask user if they want to back order 
                        cmdNo.Attributes.Add("onclick", "javascript:opener.location.href='" & StoreFrontConfiguration.SiteURL & "shoppingcart.aspx?BOAction=Cancel';" & sReturn)
                        _Message = oItem.Name & " is Currently Out of Stock, Would You Like to Back Order?"
                        CType(tblInventory.FindControl("CanBackOrder"), HtmlTableRow).Visible = True
                        CType(tblInventory.FindControl("TrClose"), HtmlTableRow).Visible = False
                        cmdBackOrder.Attributes.Add("onclick", "javascript:window.opener.location.href='" & StoreFrontConfiguration.SiteURL & "shoppingcart.aspx?BOAction=BackOrder';" & sReturn)
                        Session("oItem") = oItem
                    Else
                        'tell them item is not currently available
                        cmdClose.Attributes.Add("onclick", "javascript:opener.location.href='" & StoreFrontConfiguration.SiteURL & "shoppingcart.aspx?BOAction=Cancel';" & sReturn)

                        CType(tblInventory.FindControl("CanBackOrder"), HtmlTableRow).Visible = False
                        CType(tblInventory.FindControl("TrClose"), HtmlTableRow).Visible = True
                        _Message = oItem.Name & " is Out of Stock!"
                    End If
                    Me.DataBind()
                Else

                    'get the cart item
                    oItem = Session("IventoryInfo")
                    'Tee 8/22/2007 product configurator
                    Dim bundleItem As CCartItem = Session("BundleItem")
                    'end Tee
                    If IsNothing(oItem) Then
                        'We are done 
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                & "  language='JavaScript'>window.close();</script>")
                        Exit Sub
                    Else
                        'Tee 8/22/2007 product configurator
                        If oItem.Inventory.CanBackOrder OrElse (Not oItem.ProductType = ProductType.Normal _
                        AndAlso Not oItem.ProductType = ProductType.Subscription AndAlso Not _
                        IsNothing(bundleItem) AndAlso bundleItem.Inventory.CanBackOrder) Then
                            If Not oItem.Inventory.CanBackOrder Then
                                _Message = bundleItem.Name & " in " & oItem.Name & " is currently Out of Stock, Would You Like to Back Order?"
                            Else
                                _Message = "Item is Currently Out of Stock, Would You Like to Back Order?"
                            End If
                            'end Tee
                            'ask user if they want to back order 
                            cmdNo.Attributes.Add("onclick", "javascript:opener.history.go(-1);window.close();return false;")
                            CType(tblInventory.FindControl("CanBackOrder"), HtmlTableRow).Visible = True
                            CType(tblInventory.FindControl("TrClose"), HtmlTableRow).Visible = False
                            'refresh the parent so that it can handle the item add
                            If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText <> CStr(AddProductStyle.PopUp)) Then
                                ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                                          & "  language='JavaScript'>opener.history.go(-1);</script>")
                                Session("HistoryMoved") = 1
                                If (IsNothing(viewstate("PageIndex")) = False) Then

                                    cmdBackOrder.Attributes.Add("onclick", "javascript:window.opener.location.href='" & sPage & "?Add=1&Index=" & viewstate("PageIndex") & "&alertStat=1';" & sReturn)
                                Else
                                    cmdBackOrder.Attributes.Add("onclick", "javascript:window.opener.location.href='" & sPage & "?Add=1';" & sReturn)
                                End If
                            End If
                        Else
                            'tell them item is not currently available
                            cmdClose.Attributes.Add("onclick", "javascript:opener.history.go(-1);window.close();return false;")
                            Session("IventoryInfo") = Nothing
                            CType(tblInventory.FindControl("CanBackOrder"), HtmlTableRow).Visible = False
                            CType(tblInventory.FindControl("TrClose"), HtmlTableRow).Visible = True
                            'Tee 8/22/2007 product configurator
                            If oItem.ProductType = ProductType.Normal OrElse oItem.ProductType = ProductType.Subscription Then
                                _Message = "Item is Out of Stock!"
                            Else
                                _Message = CType(Session("BundleItem"), CCartItem).Name & " in " & oItem.Name & " is Out of Stock!"
                            End If
                            Session.Remove("BundleItem")
                            'end Tee
                        End If
                    End If
                    'bind the page
                    Me.DataBind()
                End If

                imgNo.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Cancel").Attributes("Filepath").Value
                imgBackOrder.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
                imgClose.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Close").Attributes("Filepath").Value


            End If
        Catch ex As Exception
            Session("DetailError") = "Class InventoryMessage Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal Value As String)
            _Message = Value
        End Set
    End Property



    Private Sub cmdBackOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackOrder.Click
        Me.boConfirm.Visible = False
        Me.Confirmation.Visible = True
        Dim objItem As CCartItem = Session("IventoryInfo")
        Dim sPage As String = Request.QueryString("sPage")
        Try
            If Not (Session("arIventoryInfo") Is Nothing) Then

                Dim ar As ArrayList = Session("arIventoryInfo")
                objItem = CType(ar(0), CCartItem)
                Me.AddCartItem(objItem)
                ar.RemoveAt(0)
                If ar.Count > 0 Then
                    'RegisterClientScriptBlock("PopScript", "<script" _
                    '                              & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
                    Session("arIventoryInfo") = ar
                    Session("oItem") = Nothing
                Else
                    Session("arIventoryInfo") = Nothing
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                                & "  language='JavaScript'>window.opener.location.href='" & sPage & "';window.close();return false;</script>")
                End If
            Else


                If (IsNothing(objItem) = True) Then
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                & "  language='JavaScript'>window.close();</script>")
                    Exit Sub
                End If
                Me.AddItemToCart("userNotified", EventArgs.Empty, objItem)

                If (IsNothing(viewstate("PageIndex")) = False) Then

                    ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                & "  language='JavaScript'>window.opener.location.href='" & sPage & "?Index=" & ViewState("PageIndex") & "';</script>")
                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "myScript", "<script" _
                                & "  language='JavaScript'>window.opener.location.href='" & sPage & "';</script>")
                End If


                Dim strMessage As String = m_objMessages.GetXMLMessage("AddProduct", "AddToCart", "Add")
                Dim objNode As XmlNode = StoreFrontConfiguration.AddProductStyle()

                If (objNode.Attributes("DisplayQuantity").Value = "1") Then
                    strMessage = strMessage.Replace("[Quantity]", objItem.Quantity)
                Else
                    strMessage = strMessage.Replace("[Quantity] ", "")
                End If
                If (objNode.Attributes("DisplayProductName").Value = "1") Then
                    If (objItem.Quantity > 1) Then
                        strMessage = strMessage.Replace("[ProductName]", objItem.PluralName)
                        strMessage = strMessage.Replace("[has]", "have")
                    Else
                        strMessage = strMessage.Replace("[ProductName]", objItem.Name)
                        strMessage = strMessage.Replace("[has]", "has")
                    End If
                Else
                    strMessage = strMessage.Replace("[has] ", "")
                    strMessage = strMessage.Replace("[ProductName] ", "")
                End If

                If (objNode.Attributes("DisplayUpSellMessage").Value = "1") Then
                    strMessage = strMessage.Replace("[UpSellMessage]", objItem.UpSellMessage)
                Else
                    strMessage = strMessage.Replace("[UpSellMessage] ", "")
                End If

                lblDisplay.Text = strMessage
                Session("ItemAdded") = Nothing

                imgCheckout.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CheckOut").Attributes("Filepath").Value
                Image1.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Close").Attributes("Filepath").Value
                btnCheckout.Attributes.Add("onclick", "CheckoutFromPopUp();")
                btnClose.Attributes.Add("onclick", "window.close();")
            End If
        Catch ex As Exception
            Session("DetailError") = "Class AddProductPopUp Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try



    End Sub

    Private Sub cmdNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        If Not (Session("arIventoryInfo") Is Nothing) Then
            Dim ar As ArrayList
            ar = Session("arIventoryInfo")
            ar.RemoveAt(0)
            Session("oItem") = Nothing
            If ar.Count = 0 Then
                Session("arIventoryInfo") = Nothing
            Else
                Session("oItem") = ar(0)
            End If
        End If
    End Sub
End Class
