'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System
Imports System.Text
Imports System.Math
Imports System.Web
Imports System.Web.UI
Imports System.Xml
Imports System.Web.Caching
Imports System.Globalization
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.StoreFront.FrameworkExceptions
Imports StoreFront.SystemBase
Imports System.Collections.Specialized
Imports System.Runtime.Serialization.Formatters.Binary


Public Enum AddProductStyle
    GoCartMessage = 0
    ShowMessage = 1
    PopUp = 2
End Enum

Public Class CWebPage
    Inherits Page

    Protected WithEvents RightColumnNav1 As RightColumnNav
    Protected WithEvents TopBanner1 As TopBanner
    Protected WithEvents TopSubBanner1 As TopSubBanner
    Protected WithEvents LeftColumnNav1 As LeftColumnNav
    Protected WithEvents Footer1 As Footer
    Protected WithEvents SearchTemplate11 As SearchTemplate1
    Protected WithEvents SearchTemplate12 As SearchTemplate2
    Protected WithEvents SearchTemplate13 As SearchTemplate3
    Protected WithEvents ProductDetail11 As ProductDetail1
    Protected WithEvents ProductDetail21 As ProductDetail2

    Private arSQL() As String = {"select", "drop", ";", "--", "insert", "delete", "xp_"}

#Region "Class Members"
    Protected m_objCartList As CartList
    Protected dom As XmlDocument
    Protected WithEvents m_objXMLCart As CCart
    Protected m_dep As CacheDependency
    Protected m_objCustomer As CCustomer
    Protected m_Affiliate As CAffiliate
    Protected m_arEMailContent As New ArrayList
    Protected m_objXMLAccess As CXMLProductAccess
    Protected m_objMessages As CXMLMessages
    Protected m_OrderAttributes As ArrayList
    Protected m_bAttribute_Error As Boolean = False
    Protected slinkContent As String = ""
    Protected IsFromDetail As Boolean = False
    Protected objAdmin As Admin.CStore
    Event ProductAdded As EventHandler
    Event USER_ERROR As EventHandler
    Event EmptySearch As EventHandler
    Protected sPage As String = "SearchResult.aspx"
#End Region

#Region "WriteOnly Property SetPageTitle() As String"
    Public WriteOnly Property SetPageTitle() As String
        Set(ByVal Value As String)
            If (IsNothing(TopSubBanner1) = False) Then
                TopSubBanner1.PageName = Value
            End If
        End Set
    End Property
#End Region

#Region "Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error"
    Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Error
        Trace.IsEnabled = True
        Dim objError As New CStoreFrontWebError(Err)
        objError.TrackInfo(Me.GetType().ToString(), "Page_Error")
    End Sub
#End Region


#Region "Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        MakeSafe()

        If ((CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) <> -1) And (Request.QueryString("WebID") <> "")) Then
            Session("Customer") = Nothing
            Session("XMLShoppingCart") = Nothing
        End If
        dom = StoreFrontConfiguration.XMLDocument()
        objAdmin = New Admin.CStore(StoreFrontConfiguration.AdminStore)
        If (Session.LCID <> objAdmin.LCID) Then
            Session.LCID = objAdmin.LCID
        End If
        Try
            If IsNothing(RightColumnNav1) = False Then
                SetDefaultButton(Page, RightColumnNav1.FindControl("SimpleSearch1").FindControl("txtSimpleSearch"), RightColumnNav1.FindControl("SimpleSearch1").FindControl("btnSearch"))
            End If
        Catch
            'do nothing
        End Try

        StoreFrontConfiguration.Culture = CultureInfo.CurrentCulture
        If (CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) <> -1) Then
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
        End If
        If (IsNothing(Session("Customer"))) Then
            If (Request.QueryString("WebID") = "") Then
                If (Request.Form("WebID") = "") Then
                    ' New Session
                    If (Request.Form("M_WebID") = "") Then
                        m_objCustomer = New CCustomer(Guid.NewGuid().ToString(), dom)
                    Else
                        m_objCustomer = New CCustomer(Request.Form("M_WebID"), dom)
                    End If
                Else
                    m_objCustomer = New CCustomer(Request.Form("WebID"), dom)
                End If
            Else
                m_objCustomer = New CCustomer(Request.QueryString("WebID"), dom)
            End If
            Session("WebID") = m_objCustomer.GetSessionID()
            Session("Customer") = m_objCustomer
            SetAffiliate()
            If (IsNothing(Session("Affiliate"))) Then
                m_Affiliate = Session("Affiliate")
            End If
            Session("ConvertISO") = m_objCustomer.OandaISO
            Session("OandaRate") = m_objCustomer.OandaRate
            If (m_objCustomer.OandaRate = "") Then
                Session("OandaRate") = Nothing
            End If
        Else
            If (Request.QueryString("WebID") <> "") Then
                m_objCustomer = New CCustomer(Request.QueryString("WebID"), dom)
                Session("WebID") = m_objCustomer.GetSessionID()
                Session("Customer") = m_objCustomer
                SetAffiliate()
                If (IsNothing(Session("Affiliate"))) Then
                    m_Affiliate = Session("Affiliate")
                End If
                Session("ConvertISO") = m_objCustomer.OandaISO
                Session("OandaRate") = m_objCustomer.OandaRate
                If (m_objCustomer.OandaRate = "") Then
                    Session("OandaRate") = Nothing
                End If
                m_objCustomer.Referer = Session("Referer")
                m_objCustomer.HttpReferer = Session("HttpReferer")
            Else
                m_objCustomer = Session("Customer")
                m_objCustomer = New CCustomer(m_objCustomer.GetSessionID(), dom)
                'update #2160
                If Session("Referer") <> 0 Then
                    m_objCustomer.Referer = Session("Referer")
                End If
                m_objCustomer.HttpReferer = Session("HttpReferer")
            End If
        End If

        m_objMessages = StoreFrontConfiguration.MessagesAccess
        Session("Messages") = m_objMessages

        m_objXMLAccess = StoreFrontConfiguration.ProductAccess 'New CXMLProductAccess()

        If (IsNothing(Session("XMLShoppingCart"))) Then
            Dim objStoreDiscounts As New CStoreDiscounts
            Session("XMLShoppingCart") = New CCart(m_objCustomer.GetSessionID(), m_objXMLAccess, objStoreDiscounts.GetDiscounts(), m_objCustomer.CustomerGroup())

            ' If no session of shopping cart
            ' try to load from DB
            m_objXMLCart = Session("XMLShoppingCart")
            m_objXMLCart.LoadFromDB() ', objGiftCertificates.GetGiftCertificates())
            If (m_objXMLCart.OandaISO <> "") Then
                Session("OandaRate") = m_objXMLCart.OandaRate
                Session("ConvertISO") = m_objXMLCart.OandaISO
            End If
        Else
            m_objXMLCart = Session("XMLShoppingCart")
        End If

        ' Toggle the Nav Objects

        Dim objMenuAccess As New CXMLMenuAccess
        If (IsNothing(TopSubBanner1) = False) Then
            If (objAdmin.TopMenuBarNav) Then
                TopSubBanner1.NavVisible = True
                TopSubBanner1.MenuNode = objMenuAccess.TopMenuBarNav() 'CurrentWebPage)
            Else
                TopSubBanner1.NavVisible = False
            End If
        End If
        If (IsNothing(Footer1) = False) Then
            If (objAdmin.FooterNav) Then
                Footer1.NavVisible = True
                Footer1.MenuNode = objMenuAccess.FooterNav() 'CurrentWebPage)
            Else
                Footer1.NavVisible = False
            End If
        End If
        If (IsNothing(RightColumnNav1) = False) Then
            If (objAdmin.RightNav) Then
                RightColumnNav1.NavVisible = True
                RightColumnNav1.MenuNode = objMenuAccess.RightNav() 'CurrentWebPage)
            Else
                RightColumnNav1.NavVisible = False
            End If
        End If
        If (IsNothing(LeftColumnNav1) = False) Then
            If (objAdmin.LeftNav) Then
                LeftColumnNav1.NavVisible = True
                LeftColumnNav1.MenuNode = objMenuAccess.LeftNav() 'CurrentWebPage)
            Else
                LeftColumnNav1.NavVisible = False
            End If
        End If

        If (objAdmin.ActivateOanda) Then
            Session("OandaID") = objAdmin.OandaID
            Session("ISOCurrency") = objAdmin.ISOCurrency
        Else
            Session("OandaID") = Nothing
        End If

        '''''''''''''''BackOrder"

        'IF Item is bieng backordered
        If Request.QueryString("add") = 1 Then

            Dim oCartItem As CCartItem = Session("IventoryInfo")
            If IsNothing(oCartItem) = False Then
                AddItemToCart(Nothing, EventArgs.Empty, oCartItem)
                Session("IventoryInfo") = Nothing
                Session("CanAdd") = Nothing
            End If
        ElseIf Request.QueryString("BOAction") = "Cancel" And (Not IsPostBack) Then
            If IsNothing(Session("arIventoryInfo")) = False Then
                Dim ar As ArrayList
                ar = Session("arIventoryInfo")
                ar.RemoveAt(0)
                If ar.Count > 0 Then
                    Session("arIventoryInfo") = ar
                Else
                    Session("arIventoryInfo") = Nothing
                End If
            End If

        ElseIf (Request.QueryString("BOAction") = "BackOrder") And (Not IsPostBack) Then
            If IsNothing(Session("arIventoryInfo")) = False Then
                Dim ar As ArrayList
                Dim oCartItem As CCartItem
                ar = Session("arIventoryInfo")
                oCartItem = ar(0)
                AddCartItem(ocartitem)
                ar.RemoveAt(0)
                If ar.Count > 0 Then
                    Session("arIventoryInfo") = ar
                Else
                    Session("arIventoryInfo") = Nothing
                End If
            End If

        End If
        If IsNothing(Session("arIventoryInfo")) = False Then

        End If
        ''''''''''End BackOrder"


    End Sub
#End Region

#Region "Sub SetDesign(ByRef PageTable As HtmlTable, ByRef PageSubTable As HtmlTable, ByRef PageCell As HtmlTableCell, Optional ByVal ErrorMessage As HtmlGenericControl = Nothing, Optional ByVal Message As HtmlGenericControl = Nothing)"
    Protected Sub SetDesign(ByRef PageTable As HtmlTable, ByRef PageSubTable As HtmlTable, ByRef PageCell As HtmlTableCell, Optional ByVal ErrorMessage As HtmlGenericControl = Nothing, Optional ByVal Message As HtmlGenericControl = Nothing, Optional ByVal IsProdBot As Boolean = False)
        Dim objDesign As New CDesign(StoreFrontConfiguration.SiteDesign.Item("BodyTable"))
        Dim TempCell As HtmlTableCell
        If (IsNothing(PageSubTable) = False) Then
            PageSubTable.CellPadding = objDesign.CellPadding
            PageSubTable.CellSpacing = objDesign.CellSpacing
            PageSubTable.Width = objDesign.TableWidth
            PageSubTable.BorderColor = objDesign.BorderColor
            PageSubTable.Border = objDesign.BorderSize
            PageSubTable.Align = objDesign.HorizontalAlignment
        End If
        If (IsNothing(PageCell) = False) Then
            PageCell.Align = objDesign.HorizontalAlignment
            PageCell.VAlign = objDesign.VerticalAlignment
        End If
        Dim tempLWidth As String
        Dim tempRWidth As String


        If (IsNothing(ErrorMessage) = False) Then
            objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("ErrorMessages"))
            ErrorMessage.Attributes.Add("Align", objDesign.HorizontalAlignment)
        End If

        If (IsNothing(Message) = False) Then
            objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("Messages"))
            Message.Attributes.Add("Align", objDesign.HorizontalAlignment)
        End If
        If IsNothing(PageSubTable) = False Then
            If (IsNothing(PageSubTable.FindControl("TopBannerCell")) = False) Then
                objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("TopBanner"))
                If (objDesign.Visible = False) Then
                    TempCell = CType(PageSubTable.FindControl("TopBannerCell"), HtmlTableCell)
                    If (IsNothing(TempCell) = False) Then
                        TempCell.Visible = False
                    End If
                    TempCell = Nothing
                End If
                objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("TopSubBanner"))
                If (objDesign.Visible = False) Then
                    TempCell = CType(PageSubTable.FindControl("TopSubBannerCell"), HtmlTableCell)
                    If (IsNothing(TempCell) = False) Then
                        TempCell.Visible = False
                    End If
                    TempCell = Nothing
                End If

                objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("LeftColumn"))

                If (objDesign.Visible = False) Then
                    If (IsNothing(PageSubTable.FindControl("LeftColumnCell")) = False) Then
                        CType(PageSubTable.FindControl("LeftColumnCell"), HtmlTableCell).Visible = False
                    End If
                    If IsProdBot = False And Not IsPostBack() Then
                        TempCell = CType(PageSubTable.FindControl("TopBannerCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then
                            TempCell.ColSpan = TempCell.ColSpan - 1
                        End If
                        TempCell = Nothing
                        TempCell = CType(PageSubTable.FindControl("TopSubBannerCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then
                            TempCell.ColSpan = TempCell.ColSpan - 1
                        End If
                        TempCell = Nothing
                        TempCell = CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then
                            TempCell.ColSpan = TempCell.ColSpan - 1
                        End If
                        TempCell = Nothing
                    End If
                    tempLWidth = "0%"
                Else
                    TempCell = CType(PageSubTable.FindControl("LeftColumnCell"), HtmlTableCell)
                    If (IsNothing(TempCell) = False) Then
                        TempCell.Style.Add("width", objDesign.TableWidth)
                        TempCell.Width = objDesign.TableWidth
                        If (IsNothing(TempCell.FindControl("LeftColumnNav1")) = False) Then
                            If (IsNothing(TempCell.FindControl("LeftColumnNav1").FindControl("Table1")) = False) Then
                                CType(TempCell.FindControl("LeftColumnNav1").FindControl("Table1"), HtmlTable).Style.Add("width", objDesign.TableWidth)
                                CType(TempCell.FindControl("LeftColumnNav1").FindControl("Table1"), HtmlTable).Width = objDesign.TableWidth
                            End If
                        End If
                    End If
                    tempLWidth = objDesign.TableWidth
                    TempCell = Nothing
                End If

                objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("RightColumn"))
                If (objDesign.Visible = False) Then
                    CType(PageSubTable.FindControl("RightColumnCell"), HtmlTableCell).Visible = IsNothing(PageSubTable.FindControl("RightColumnCell"))
                    If IsProdBot = False And Not IsPostBack() Then
                        TempCell = CType(PageSubTable.FindControl("TopBannerCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then TempCell.ColSpan = TempCell.ColSpan - 1
                        TempCell = Nothing
                        TempCell = CType(PageSubTable.FindControl("TopSubBannerCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then TempCell.ColSpan = TempCell.ColSpan - 1
                        TempCell = Nothing
                        TempCell = CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) Then TempCell.ColSpan = TempCell.ColSpan - 1
                    End If
                    TempCell = Nothing
                End If
                tempRWidth = "0%"
            Else
                TempCell = CType(PageSubTable.FindControl("RightColumnCell"), HtmlTableCell)
                If (IsNothing(TempCell) = False) Then
                    TempCell.Style.Add("width", objDesign.TableWidth)
                    TempCell.Width = objDesign.TableWidth
                    If (IsNothing(TempCell.FindControl("RightColumnNav1")) = False) Then
                        If (IsNothing(TempCell.FindControl("RightColumnNav1").FindControl("Table1")) = False) Then
                            CType(TempCell.FindControl("RightColumnNav1").FindControl("Table1"), HtmlTable).Style.Add("width", objDesign.TableWidth)
                            CType(TempCell.FindControl("RightColumnNav1").FindControl("Table1"), HtmlTable).Width = objDesign.TableWidth
                        End If
                    End If
                End If
                tempRWidth = objDesign.TableWidth
                TempCell = Nothing
            End If
        End If


        objDesign = New CDesign(StoreFrontConfiguration.SiteDesign.Item("Footer"))
        If (objDesign.Visible = False) Then
            TempCell = CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell)
            TempCell.Visible = IsNothing(TempCell)
            TempCell = Nothing
        Else
            TempCell = CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell)
            If (IsNothing(TempCell) = False) Then
                TempCell.Width = objDesign.TableWidth
                TempCell.Style.Add("width", objDesign.TableWidth)
            End If
            TempCell = Nothing
        End If


        If Not ((tempLWidth Is Nothing) Or (tempRWidth Is Nothing)) Then
            Dim tempWidth As String
            If tempLWidth.EndsWith("%") And tempRWidth.EndsWith("%") Then tempWidth = CStr(100 - CDbl(tempLWidth.Replace("%", "")) - CDbl(tempRWidth.Replace("%", ""))) & "%"
            If (IsNothing(FindControl("ContentCell")) = False) Then CType(FindControl("ContentCell"), HtmlTableCell).Width = tempWidth
            If (IsNothing(FindControl("tdContent")) = False) Then CType(FindControl("tdContent"), HtmlTableCell).Width = tempWidth
        End If
    End Sub
#End Region

#Region "Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload"
    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session("XMLShoppingCart") = m_objXMLCart
        Session("Customer") = m_objCustomer








    End Sub
#End Region

#Region "Sub RefreshLeftNav"
    Public Sub RefreshLeftNav()
        If (IsNothing(LeftColumnNav1) = False) Then
            LeftColumnNav1.RefreshNav()
        End If
    End Sub
#End Region

#Region "Function CurrentWebPage"
    Public Function CurrentWebPage() As String
        Dim sTemp As String
        sTemp = Request.Url.ToString
        If InStr(sTemp, "?") > 0 Then
            Return sTemp.Substring(0, InStr(sTemp, "?") - 1)
        Else
            Return sTemp
        End If

    End Function
#End Region

#Region "Sub AddItemToCart(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal objCart As CCartItem = Nothing)"

    Public Sub AddItemToCart(ByVal sender As Object, ByVal e As EventArgs, Optional ByVal objCart As CCartItem = Nothing)

        Dim objItemAdded As CCartItem
        Dim oCartItem As CCartItem
        Try


            If m_bAttribute_Error = False Then

                If IsNothing(objCart) Then
                    'normal non-BackOrdered Item
                    Dim objButton As LinkButton = sender

                    If (CLng(objButton.CommandArgument) > 0) Then
                        m_OrderAttributes = Session("OrderAttributes")
                        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                            oCartItem = New CCartItem(m_objXMLAccess.GetProduct(objButton.CommandName), CLng(objButton.CommandArgument), m_OrderAttributes, m_objCustomer.CustomerGroup)

                        Else
                            Dim oprodManagement As New CProductManagement
                            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(objButton.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
                            oCartItem = New CCartItem(drProd, CLng(objButton.CommandArgument), m_OrderAttributes, m_objCustomer.CustomerGroup)
                            oprodManagement = Nothing
                        End If
                        If (IsNothing(oCartItem.Inventory) = False) Then
                            Dim qty As Integer = oCartItem.Quantity
                            If oCartItem.Inventory.InventoryTracked Then
                                Dim objProd As CCartItem

                                For Each objProd In m_objXMLCart.GetCartItems()
                                    If objProd.ProductID = oCartItem.ProductID Then
                                        qty = objProd.Quantity + qty
                                    End If
                                Next

                                If oCartItem.Inventory.ItemsAreStocked(oCartItem.Attributes, qty) Then
                                    'in stock so its a normal order
                                    m_objXMLCart.AddItem(oCartItem)

                                Else
                                    'pop then inventory message page and let it do the logic
                                    'to allow the user to backorder or not
                                    Dim obj As HtmlGenericControl
                                    If IsFromDetail Then
                                        sPage = "Detail.aspx"
                                    End If
                                    RegisterClientScriptBlock("PopScript", "<script" _
                                                           & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
                                    Session("IventoryInfo") = oCartItem
                                    Exit Sub
                                End If
                            Else
                                'normal ORder 
                                m_objXMLCart.AddItem(oCartItem)
                            End If
                        Else
                            m_objXMLCart.AddItem(oCartItem)
                        End If
                        objItemAdded = oCartItem
                    End If
                Else
                    'we are here because the user selected to backorder Item(s)
                    m_objXMLCart.AddItem(objCart, False)
                    objItemAdded = objCart
                    Session("IventoryInfo") = Nothing
                End If

                'clear the Session Ordered Attributes
                Session("OrderAttributes") = Nothing
                Session("ItemAdded") = objItemAdded 'm_objXMLCart.GetCartItems().Item(m_objXMLCart.GetCartItems.Count() - 1)
                If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
                    'Dim obj As HtmlGenericControl
                    'obj = FindControl("BodyTag")
                    'If (IsNothing(obj) = False) Then
                    '    obj.Attributes.Add("OnLoad", "javascript: popUpWindow();")
                    'End If
                    If IsNothing(sender) = True Then
                        RegisterClientScriptBlock("PopScript", "<script" _
                          & "  language='JavaScript'>javascript: popUpWindow();</script>")
                    Else
                        Try
                            If sender.ToString <> "userNotified" Then
                                RegisterClientScriptBlock("PopScript", "<script" _
                                                                        & "  language='JavaScript'>javascript: popUpWindow();</script>")
                            End If
                        Catch
                            RegisterClientScriptBlock("PopScript", "<script" _
                                             & "  language='JavaScript'>javascript: popUpWindow();</script>")
                        End Try
                    End If



                ElseIf (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.GoCartMessage)) Then
                    Response.Redirect("ShoppingCart.aspx")
                Else
                    RaiseEvent ProductAdded(sender, e)
                End If
                If (IsNothing(CartListControl()) = False) Then
                    CartListControl().LoadCart()
                    Exit Sub
                End If
                If (IsNothing(RightColumnNav1) = False) Then
                    If (IsNothing(RightColumnNav1.CartListControl) = False) Then
                        RightColumnNav1.CartListControl.LoadCart()
                        Exit Sub
                    End If
                End If
                If (IsNothing(LeftColumnNav1) = False) Then
                    If (IsNothing(LeftColumnNav1.CartListControl) = False) Then
                        LeftColumnNav1.CartListControl.LoadCart()
                        Exit Sub
                        Exit Sub
                    End If
                End If

            End If
        Catch err As SystemException
            RaiseEvent USER_ERROR(err.Message, EventArgs.Empty)

        End Try

    End Sub

#End Region

#Region "Public Sub AddCartItem(ByVal objitem As CCartItem)"
    Public Sub AddCartItem(ByVal objitem As CCartItem)
        m_objXMLCart.AddItem(objitem, False)
    End Sub
#End Region

#Region "Sub UpdateCartList()"
    Public Sub UpdateCartList()
        If (IsNothing(CartListControl()) = False) Then
            CartListControl().LoadCart()
        End If
        If (IsNothing(RightColumnNav1) = False) Then
            If (IsNothing(RightColumnNav1.CartListControl) = False) Then
                RightColumnNav1.CartListControl.LoadCart()
            End If
        End If
        If (IsNothing(LeftColumnNav1) = False) Then
            If (IsNothing(LeftColumnNav1.CartListControl) = False) Then
                LeftColumnNav1.CartListControl.LoadCart()
            End If
        End If
    End Sub
#End Region

#Region "Property CartListControl() As CartList"
    Protected Property CartListControl() As CartList
        Get
            Return m_objCartList
        End Get
        Set(ByVal Value As CartList)
            m_objCartList = Value
        End Set
    End Property
#End Region

#Region "Sub AddItemToSavedCart(ByVal sender As Object, ByVal e As EventArgs)"
    Public Sub AddItemToSavedCart(ByVal sender As Object, ByVal e As EventArgs)
        Dim objButton As LinkButton = sender
        'Dim objProductAccess As CXMLProductAccess
        Dim objItem As CGenericCartItem
        If m_bAttribute_Error = False Then
            m_OrderAttributes = Session("OrderAttributes")
            If (CLng(objButton.CommandArgument) > 0) Then
                'objProductAccess = New CXMLProductAccess(dom)

                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    objItem = New CGenericCartItem(m_objXMLAccess.GetProduct(objButton.CommandName), m_OrderAttributes, CLng(m_objCustomer.CustomerGroup))

                Else
                    ' Dim oprodManagement As New CProductManagement()
                    'Dim drProd As DataRow = oprodManagement.GetProductRow(CLng(objButton.CommandName), 0).Products.Rows(0)
                    '  objItem = New CGenericCartItem(drProd, 1, m_OrderAttributes, Me.m_objCustomer.CustomerGroup, True)
                    ' oprodManagement = Nothing

                    Dim oprodManagement As New CProductManagement
                    Dim drProd As DataRow = oprodManagement.GetProductRow(CLng(objButton.CommandName), Me.m_objCustomer.CustomerGroup).Products.Rows(0)
                    objItem = New CGenericCartItem(drProd, 1, m_OrderAttributes, Me.m_objCustomer.CustomerGroup)
                    oprodManagement = Nothing


                End If

                objItem.Quantity = objButton.CommandArgument

                Session("AddSavedItem") = objItem
                Response.Redirect("SavedCart.aspx")
            End If
        End If

    End Sub
#End Region

#Region "Sub EMailAFriend(ByVal sender As Object, ByVal e As EventArgs)"
    Public Sub EMailAFriend(ByVal sender As Object, ByVal e As EventArgs)
        Dim objButton As LinkButton = sender
        Response.Redirect("EMailAFriend.aspx?ID=" & objButton.CommandName)
    End Sub
#End Region

#Region "SearchTemplate Events"

#Region "SearchTemplate11"

    Private Sub SearchTemplate11_AddToCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate11.AddToCart
        AddItemToCart(sender, e)
    End Sub

    Private Sub SearchTemplate11_AddToSavedCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate11.AddToSavedCart
        AddItemToSavedCart(sender, e)
    End Sub

    Private Sub SearchTemplate11_EMailFriend(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate11.EMailFriend
        EMailAFriend(sender, e)
    End Sub

    Private Sub SearchTemplate11_AttributeRequiredError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate11.AttributeRequiredError
        m_bAttribute_Error = True
        Raise_User_Error(sender & " Required")
    End Sub

#End Region

#Region "SearchTemplate12"

    Private Sub SearchTemplate12_AddToCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.AddToCart
        AddItemToCart(sender, e)
    End Sub

    Private Sub SearchTemplate12_AddToSavedCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.AddToSavedCart
        AddItemToSavedCart(sender, e)
    End Sub

    Private Sub SearchTemplate12_EMailFriend(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.EMailFriend
        EMailAFriend(sender, e)
    End Sub

    Private Sub SearchTemplate12_AttributeRequiredError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.AttributeRequiredError
        m_bAttribute_Error = True
        Raise_User_Error(sender & " Required")
    End Sub

    Private Sub SearchTemplate12_EmptyResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.EmptyResult
        ShowNoResults(sender)
    End Sub

    Private Sub SearchTemplate12_AddError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate12.AddError
        Raise_User_Error(sender)
    End Sub

#End Region

#Region "SearchTemplate13"

    Private Sub SearchTemplate13_AddToCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.AddToCart
        AddItemToCart(sender, e)
    End Sub

    Private Sub SearchTemplate13_AddToSavedCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.AddToSavedCart
        AddItemToSavedCart(sender, e)
    End Sub

    Private Sub SearchTemplate13_EMailFriend(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.EMailFriend
        EMailAFriend(sender, e)
    End Sub

    Private Sub SearchTemplate13_AttributeRequiredError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.AttributeRequiredError
        m_bAttribute_Error = True
        Raise_User_Error(sender & " Required")
    End Sub

    Private Sub SearchTemplate13_EmptyResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.EmptyResult
        ShowNoResults(sender)
    End Sub

    Private Sub SearchTemplate13_AddError(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate13.AddError
        Raise_User_Error(sender)
    End Sub
#End Region

#End Region

#Region "LeftColumnNav1 Events"
    Private Sub LeftColumnNav1_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LeftColumnNav1.Search_Click
        Session("SimpleSearch") = sender
    End Sub
#End Region

#Region "ProductDetail Events"

#Region "ProductDetail11"

    Private Sub ProductDetail11_AddToCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.AddToCart
        AddItemToCart(sender, e)
    End Sub

    Private Sub ProductDetail11_ISDetailCall(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.ISDetailCall
        IsFromDetail = sender
    End Sub

    Private Sub ProductDetail11_AddToSavedCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.AddToSavedCart
        AddItemToSavedCart(sender, e)
    End Sub

    Private Sub ProductDetail11_EMailFriend(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.EMailFriend
        EMailAFriend(sender, e)
    End Sub

    Private Sub ProductDetail11_AttributeRequiredError(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.AttributeRequiredError
        m_bAttribute_Error = True
        Raise_User_Error(sender & " Required")
    End Sub

    Private Sub ProductDetail11_AddError(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail11.AddError
        Raise_User_Error(sender)
    End Sub
#End Region

#Region "ProductDetail21"

    Private Sub ProductDetail21_AddToCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.AddToCart
        AddItemToCart(sender, e)
    End Sub

    Private Sub ProductDetail21_ISDetailCall(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.ISDetailCall
        IsFromDetail = sender
    End Sub

    Private Sub ProductDetail21_AddToSavedCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.AddToSavedCart
        AddItemToSavedCart(sender, e)
    End Sub

    Private Sub ProductDetail21_EMailFriend(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.EMailFriend
        EMailAFriend(sender, e)
    End Sub

    Private Sub ProductDetail21_AttributeRequiredError(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.AttributeRequiredError
        m_bAttribute_Error = True
        Raise_User_Error(sender & " Required")
    End Sub

    Private Sub ProductDetail21_AddError(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProductDetail21.AddError
        Raise_User_Error(sender)
    End Sub
#End Region

#End Region

#Region "Sub SetMessage(ByRef Message As Label)"
    Protected Sub SetMessage(ByRef Message As Label)
        Dim objItem As CCartItem = Session("ItemAdded")

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
        Session("strMessage") = strMessage
        Message.Text = strMessage
        Message.Visible = True

        Session("ItemAdded") = Nothing
    End Sub
#End Region

#Region "Sub Raise_User_Error(ByVal sErrorMessage As String)"

    Private Sub Raise_User_Error(ByVal sErrorMessage As String)
        RaiseEvent USER_ERROR(sErrorMessage, EventArgs.Empty)
    End Sub

#End Region

#Region "Sub PageHeader()"
    Public Sub PageHeader()
        ' Oanda Script
        If (StoreFrontConfiguration.OandaID <> "") Then
            Response.Write("<script language=javascript src=""Oanda.js""></script>" & Chr(13))
        End If

        ' Live Person Script
        If (StoreFrontConfiguration.LivePersonID <> "") Then
            Dim strScript As New StringBuilder
            Dim strList As New StringBuilder
            If (IsNothing(Session("XMLShoppingCart"))) Then
                Dim objStoreDiscounts As New CStoreDiscounts
                'Session("XMLShoppingCart") = New CCart(m_objCustomer.GetSessionID(), m_objXMLAccess, objStoreDiscounts.GetDiscounts(), m_objCustomer.CustomerGroup())

                ' If no session of shopping cart
                ' try to load from DB
                'm_objXMLCart = Session("XMLShoppingCart")
                'Dim objGiftCertificates As New CStoreGiftCertificates()
                'm_objXMLCart.LoadFromDB() ', objGiftCertificates.GetGiftCertificates())
                'If (m_objXMLCart.OandaISO <> "") Then
                'Session("OandaRate") = m_objXMLCart.OandaRate
                'Session("ConvertISO") = m_objXMLCart.OandaISO
                'End If
            Else
                'm_objXMLCart = Session("XMLShoppingCart")
            End If

            strScript.Append("<script>")
            strScript.Append(Chr(13))
            strScript.Append("var lpPosY=100;")
            strScript.Append(Chr(13))
            strScript.Append("var lpPosX=100;")
            strScript.Append(Chr(13))
            strScript.Append("</script>")
            strScript.Append(Chr(13))

            Dim objBody As HtmlGenericControl = FindControl("BodyTag")
            If (IsNothing(objBody) = False) Then
                Dim objMonitor As New Web.UI.LiteralControl
                Dim strMonitor As New StringBuilder

                strMonitor.Append("<!-- BEGIN LivePerson Monitor. -->")
                strMonitor.Append(Chr(13))
                If (CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) <> -1) Then
                    strMonitor.Append("<script language=""javascript"" src='https://server.iad.liveperson.net/hc/" & StoreFrontConfiguration.LivePersonID & "/x.js?cmd=file&file=chatScript3&site=" & StoreFrontConfiguration.LivePersonID & "&imageUrl=http://server.iad.liveperson.net/visitor/storefront'>")
                Else
                    strMonitor.Append("<script language=""javascript"" src='http://server.iad.liveperson.net/hc/" & StoreFrontConfiguration.LivePersonID & "/x.js?cmd=file&file=chatScript3&site=" & StoreFrontConfiguration.LivePersonID & "&imageUrl=https://server.iad.liveperson.net/visitor/storefront'>")
                End If
                strMonitor.Append(Chr(13))
                strMonitor.Append("</script>")
                strMonitor.Append(Chr(13))
                strMonitor.Append("<!-- END LivePerson Monitor. -->")
                strMonitor.Append(Chr(13))

                objMonitor.Text = strMonitor.ToString()

                objBody.Controls.Add(objMonitor)
            End If

            Response.Write(strScript)
        End If

        ' Meta Tags
        'Response.Write("<meta name=""" & StoreFrontConfiguration.MetaDescription & """ content=""" & StoreFrontConfiguration.MetaTag & """>" & Chr(13))
        Response.Write("<meta name=""keywords"" content=""" & StoreFrontConfiguration.MetaKeywords & """>" & Chr(13))
        Response.Write("<meta name=""description"" content=""" & StoreFrontConfiguration.MetaDescription & """>" & Chr(13))
    End Sub
#End Region

#Region "Sub ShowNoResults(ByVal strMsg As String)"
    Private Sub ShowNoResults(ByVal strMsg As String)
        RaiseEvent EmptySearch(strMsg, EventArgs.Empty)

    End Sub
#End Region

#Region "Sub PriceDisplay(ByVal dPrice As Decimal)"
    Public Sub PriceDisplay(ByVal dPrice As Decimal)
        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Response.Write("<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>")
            Else
                Response.Write(Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)")
            End If
        Else
            Response.Write(Format(dPrice, "c"))
        End If
    End Sub
#End Region

#Region "Function PriceDisplay2(ByVal dPrice As Decimal) As String"
    Public Function PriceDisplay2(ByVal dPrice As Decimal) As String
        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return "<script>document.write('" & Format(dPrice, "c") & " ' + OandaConvert());</script>"
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " <script>document.write(OandaConvert('" & Session("ConvertISO") & "'));</script>)"
            End If
        Else
            Return Format(dPrice, "c")
        End If
    End Function
#End Region

#Region "Function PriceDisplay3(ByVal dPrice As Decimal) As String"
    Public Function PriceDisplay3(ByVal dPrice As Decimal) As String

        If (StoreFrontConfiguration.OandaID <> "") Then
            If (IsNothing(Session("OandaRate"))) Then
                Return Format(dPrice, "c")
            Else
                Return Format(dPrice, "c") & " (" & Format(Round(CDec(Session("OandaRate")) * dPrice, 2), "F") & " " & Session("ConvertISO") & ")"
            End If
        Else
            Return Format(dPrice, "c")
        End If

    End Function
#End Region

#Region " Private Sub SetAffiliate "

    Private Sub SetAffiliate()
        If Me.m_objCustomer.IsSignedIn = False Or Me.m_objCustomer.Referer = 0 Then
            Dim iAffiliateID As Long = Request.QueryString("Affiliate")
            Try
                If iAffiliateID = 0 Then
                    m_objCustomer.Referer = Session("Referer")
                    m_objCustomer.HttpReferer = Session("HttpReferer")
                    If m_objCustomer.Referer = 0 Then
                        iAffiliateID = CLng(Response.Cookies("Referer").Value)
                        m_objCustomer.Referer = iAffiliateID
                        m_objCustomer.HttpReferer = Response.Cookies("HttpReferer").Value
                    End If
                Else
                    m_objCustomer.Referer = iAffiliateID
                    'Request.ServerVariables.Item(48)
                    Try
                        If m_objCustomer.HttpReferer.ToString = "" Then
                            m_objCustomer.HttpReferer = Request.ServerVariables.Item("HTTP_REFERER").ToString
                        End If
                        Response.Cookies("HttpReferer").Value = m_objCustomer.HttpReferer.ToString
                        Response.Cookies("HttpReferer").Expires.AddYears(1)
                        Response.Cookies("Referer").Value = CStr(m_objCustomer.Referer)
                        Response.Cookies("Referer").Expires.AddYears(1)
                    Catch
                        'error do nothing........
                    End Try
                End If
                Session("Referer") = m_objCustomer.Referer
                Session("HttpReferer") = m_objCustomer.HttpReferer
            Catch e As SystemException
                'error do nothing........
            End Try
        End If
    End Sub

#End Region

#Region "Function GetProduct(ByVal ProdID As Long) As CProduct"

    '##SUMMARY   
    '##SUMMARY   

    Function GetProduct(ByVal ProdID As Long) As CProduct
        Dim obj As CProduct
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
            obj = New CProduct(m_objXMLAccess.GetProduct(ProdID))
        Else
            Dim oprodManagement As New CProductManagement
            Dim drProd As DataRow = oprodManagement.GetProductRow(ProdID, m_objCustomer.CustomerGroup).Products.Rows(0)
            obj = New CProduct(drProd, 1, , )
            oprodManagement = Nothing
        End If
        Return obj
    End Function

#End Region

#Region "Sub WriteTitle()"

    Public Sub WriteTitle()
        Response.Write(StoreFrontConfiguration.StoreName)
    End Sub

#End Region

#Region "Public Sub CheckCart()"

    Public Sub CheckCart()
        If Me.m_objXMLCart.Count < 1 Then
            Throw New Exception(m_objMessages.GetXMLMessage("ShoppingCart.aspx", "NoItems", "NoItems"))
        End If
    End Sub

#End Region

#Region "Public Sub SetDefaultButton(ByRef Page As System.Web.UI.Page, ByRef objTextControl As TextBox, ByRef objButton As LinkButton)"

    Public Sub SetDefaultButton(ByRef Page As System.Web.UI.Page, ByRef objTextControl As TextBox, ByRef objButton As LinkButton)
        If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
            Dim sb As New System.Text.StringBuilder("")
            sb.Append("<SCRIPT language=""javascript"">" & vbCrLf)
            sb.Append("function DefaultKeyPress(btn){" & vbCrLf)
            sb.Append(" if (document.all){" & vbCrLf)
            sb.Append("   if (event.keyCode == 13)" & vbCrLf)
            sb.Append("   { " & vbCrLf)
            sb.Append("     event.returnValue=false;" & vbCrLf)
            sb.Append("     event.cancel = true;" & vbCrLf)
            sb.Append("     btn.click();" & vbCrLf)
            sb.Append("   } " & vbCrLf)
            sb.Append(" } " & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("</SCRIPT>" & vbCrLf)
            objTextControl.Attributes.Add("onkeydown", "DefaultKeyPress(document.all." & objButton.ClientID & ")")
            Page.RegisterStartupScript("ForceDefaultToScript", sb.ToString)
        End If

    End Sub


#End Region

#Region "Private Sub m_objXMLCart_BoPrompt(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPrompt"

    Private Sub m_objXMLCart_BoPrompt(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPrompt
        'this event was raised from m_objXMLCart letting us know user already had this product 
        'in his cart and that quantity plus the quantity trying to add was greater than the items in stock for that product
        'pop then inventory message page and let it do the logic
        'to allow the user to backorder or not
        Dim obj As HtmlGenericControl
        If IsFromDetail Then
            sPage = "Detail.aspx"
        End If

        RegisterClientScriptBlock("PopScript", "<script" _
                               & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
        Session("IventoryInfo") = sender 'sender is the new cartItem
    End Sub

#End Region

#Region "Private Sub m_objXMLCart_BoPromptCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPromptCart"
    Private Sub m_objXMLCart_BoPromptCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPromptCart
        'this event was raised from m_objXMLCart letting us know user already had this product 
        'in his cart and that quantity plus the quantity trying to add was greater than the items in stock for that product
        'pop then inventory message page and let it do the logic
        'to allow the user to backorder or not
        Dim obj As HtmlGenericControl

        sPage = "ShoppingCart.aspx"
        RegisterClientScriptBlock("PopScript", "<script" _
                               & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
        Session("arIventoryInfo") = sender 'sender is an arraylist
    End Sub
#End Region

    Private Function TestInput(ByVal strValue As String) As String
        Dim bReSubmit As Boolean = False
        Dim nIndex As Integer = 0

        While (True)
            nIndex = strValue.IndexOf("'", nIndex)

            If (nIndex > -1) Then
                If (nIndex = strValue.Length - 1) Then
                    bReSubmit = True
                    Exit While
                End If

                If (strValue.Chars(nIndex + 1) <> "'") Then
                    bReSubmit = True
                    Exit While
                Else
                    nIndex = nIndex + 2
                End If
            Else
                Exit While
            End If
        End While

        If (bReSubmit) Then
            Return strValue.Replace("'", "")
        Else
            Return ""
        End If
    End Function

    Private Sub MakeSafe()
        If (CurrentWebPage.ToLower.IndexOf(StoreFrontConfiguration.SSLPath.ToLower & "management") <> -1) Then
            Exit Sub
        End If
        Dim nvcForm As New NameValueCollection
        Dim nvcQString As New NameValueCollection
        Dim i As Integer
        Dim sSQL As String
        Dim sTempString As String
        Dim bRepost As Boolean = False

        ' Do Test for WebID      
        If (Request.QueryString("WebID") <> "") Then
            If (Request.QueryString("WebID").IndexOf("'") <> -1) Then
                Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
            Else
                For Each sSQL In arSQL
                    If (Request.QueryString("WebID").IndexOf(sSQL) <> -1) Then
                        Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
                    End If
                Next
            End If
        ElseIf (Request.Form("WebID") <> "") Then
            If (Request.Form("WebID").IndexOf("'") <> -1) Then
                Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
            Else
                For Each sSQL In arSQL
                    If (Request.Form("WebID").IndexOf(sSQL) <> -1) Then
                        Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
                    End If
                Next
            End If
        ElseIf (Request.Form("M_WebID") <> "") Then
            If (Request.Form("M_WebID").IndexOf("'") <> -1) Then
                Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
            Else
                For Each sSQL In arSQL
                    If (Request.Form("M_WebID").IndexOf(sSQL) <> -1) Then
                        Response.Redirect(StoreFrontConfiguration.SiteURL & "default.aspx")
                    End If
                Next
            End If
        End If

        'Loop through the Form Collection
        If (Request.Form.Count > 0) Then
            For i = 0 To Request.Form.Count - 1
                If (Request.Form.Keys(i) = "__VIEWSTATE" Or Request.Form.Keys(i) = "__EVENTARGUMENT" Or Request.Form.Keys(i) = "__EVENTTARGET") Then
                    nvcForm.Add(Request.Form.Keys(i), Request.Form.Item(i))
                Else
                    sTempString = TestInput(Request.Form.Item(i))
                    If sTempString <> "" Then
                        nvcForm.Add(Request.Form.Keys(i), sTempString)
                        bRepost = True
                    Else
                        nvcForm.Add(Request.Form.Keys(i), Request.Form.Item(i))
                    End If
                End If
            Next
        End If

        If bRepost Then
            RecursiveTest(nvcForm, Me)
        End If
    End Sub

    Private Sub RecursiveTest(ByVal nvcForm As NameValueCollection, ByVal objControl As Control)
        If (objControl.Controls.Count > 0) Then
            Dim objCtrl As Control
            For Each objCtrl In objControl.Controls
                RecursiveTest(nvcForm, objCtrl)
            Next
        End If

        Dim o As New TextBox

        If (objControl.GetType Is o.GetType) Then
            Try
                CType(objControl, TextBox).Text = nvcForm.Item(objControl.UniqueID).ToString()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
