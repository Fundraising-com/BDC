'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO
Option Explicit On 
Imports System
Imports System.Text
Imports System.text.RegularExpressions
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
Imports StoreFront.Integration

Imports CSR.CSRBusinessRule


Public Enum AddProductStyle
    GoCartMessage = 0
    ShowMessage = 1
    PopUp = 2
End Enum

Public Class CWebPage
    Inherits Page

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Const HELP_URL_FORMAT As String = "http://support.storefront.net/mtdocs70/{0}.asp"
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

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
    Protected WithEvents Instruction1 As Instruction
    Private arSQL() As String = {"select", "drop", ";", "--", "insert", "delete", "xp_"}
    Private AdminGeneralAccessPages As String = "changepassword.aspx adminlogin.aspx"
    Private AnonRestrictedwebPages As String = "savedcart.aspx custprofilemain.aspx custedit.aspx custaddressbook.aspx orderhistory.aspx"
    Private Const LASTWEBID As String = "LastWebId"
    ' begin: JDB - product configurator bug 74
    Protected Overridable ReadOnly Property PreserveSessionSearch() As Boolean
        Get
            Return False
        End Get
    End Property
    ' end: JDB - product configurator bug 74

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
    Protected mOverrideLogIn As Boolean = False
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

    Protected VirtualPath As String = String.Empty

    Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
        Dim virtualURL As String = HttpContext.Current.Items("VirtualURL")
        If Not IsNothing(virtualURL) Then
            Dim virtual As New Uri(StoreFrontConfiguration.SiteURL)
            Me.VirtualPath = virtual.LocalPath
            writer = New PostBackHtmlTextWriter(writer, virtualURL)
        End If
        MyBase.Render(writer)
    End Sub

#Region "Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Tee 8/23/2007 product configurator bug 74
        If Not Me.PreserveSessionSearch And CurrentWebPage.IndexOf("/management/") > 0 Then
            Session("SearchStorage") = Nothing
            Session("SearchStorage2") = Nothing
        End If
        'end Tee
        Try
            MakeSafe()
        Catch Ex As Exception
        End Try
        ValidateSession()
        dom = StoreFrontConfiguration.XMLDocument()
        objAdmin = New Admin.CStore(StoreFrontConfiguration.AdminStore)
        If (Session.LCID <> objAdmin.LCID) Then
            Session.LCID = objAdmin.LCID
        End If

        'BEGIN: GJV - 10/1/2007 - SF7 Merchant Tools Design
        'Try
        '    If IsNothing(RightColumnNav1) = False Then
        '        SetDefaultButton(Page, RightColumnNav1.FindControl("SimpleSearch1").FindControl("txtSimpleSearch"), RightColumnNav1.FindControl("SimpleSearch1").FindControl("btnSearch"))
        '    End If
        'Catch
        '    'do nothing
        'End Try        
		Try
			If IsNothing(LeftColumnNav1) = False Then
				SetDefaultButton(Page, LeftColumnNav1.FindControl("SimpleSearch1").FindControl("txtSimpleSearch"), LeftColumnNav1.FindControl("SimpleSearch1").FindControl("btnSearch"))
			End If
        Catch
            'do nothing
        End Try
		Try
			If IsNothing(TopSubBanner1) = False Then
				SetDefaultButton(Page, TopSubBanner1.FindControl("SimpleSearch1").FindControl("txtSimpleSearch"), TopSubBanner1.FindControl("SimpleSearch1").FindControl("btnSearch"))
			End If
        Catch
            'do nothing
        End Try
		Try
			If IsNothing(Footer1) = False Then
				SetDefaultButton(Page, Footer1.FindControl("SimpleSearch1").FindControl("txtSimpleSearch"), Footer1.FindControl("SimpleSearch1").FindControl("btnSearch"))
			End If
        Catch
            'do nothing
        End Try
        'END: GJV - 10/1/2007 - SF7 Merchant Tools Design

        StoreFrontConfiguration.Culture = CultureInfo.CurrentCulture
        If (CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) <> -1) Then
            If CurrentWebPage.IndexOf("Encryption.aspx") <> -1 Then
                Response.Cache.SetCacheability(HttpCacheability.Private)
            Else
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
            End If
        End If

        SetSession()
        'Admin user management - Junu 7/07
        CheckAdminLoggedIn()
        'end Admin user management - Junu 7/07
        'Anonymous checkout -junu 7/07
        AnonymousRestrictedPages()
        'end Anonymous checkout
        m_objMessages = StoreFrontConfiguration.MessagesAccess
        Session("Messages") = m_objMessages

        m_objXMLAccess = StoreFrontConfiguration.ProductAccess 'New CXMLProductAccess()

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
            Session.Remove("OandaID")
        End If

        '''''BackOrder"

        'IF Item is bieng backordered
        If Request.QueryString("add") = 1 Then
            Dim oCartItem As CCartItem = Session("IventoryInfo")
            If IsNothing(oCartItem) = False Then
                AddItemToCart(Nothing, EventArgs.Empty, oCartItem)
                Session.Remove("IventoryInfo")
                'Tee 8/22/2007 product configurator
                'Session.Remove("IventoryInfo")
                'end Tee
                Session.Remove("CanAdd")
            End If
        ElseIf Request.QueryString("BOAction") = "Cancel" And (Not IsPostBack) Then
            If IsNothing(Session("arIventoryInfo")) = False Then
                Dim ar As ArrayList
                ar = Session("arIventoryInfo")
                ar.RemoveAt(0)
                If ar.Count > 0 Then
                    Session("arIventoryInfo") = ar
                Else
                    Session.Remove("arIventoryInfo")
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
                    Session.Remove("arIventoryInfo")
                End If
            End If

        End If
        If IsNothing(Session("arIventoryInfo")) = False Then
        End If
        ''''End BackOrder"
		' begin: JDB - Google Analytics
        If StoreFrontConfiguration.GoogleAnalyticsID.Length > 0 Then
            '*** New Code ©copyright Stokes Web Development www.stokesweb.com
            '*** Code adds tracking script for Google Analytics
            Dim myOutput As String = ""
            Try
                If (CurrentWebPage.ToLower.IndexOf("https://") <> -1) Then
                    myOutput += "<script src=""https://ssl.google-analytics.com/urchin.js"" type=""text/javascript"">" & vbCrLf
                Else
                    myOutput += "<script src=""http://www.google-analytics.com/urchin.js"" type=""text/javascript"">" & vbCrLf
                End If
                myOutput += "</script>" & vbCrLf
                myOutput += "<script type=""text/javascript"">" & vbCrLf
                myOutput += String.Format("_uacct = ""{0}"";", StoreFrontConfiguration.GoogleAnalyticsID) & vbCrLf
                myOutput += "_udn = ""none"";" & vbCrLf
                myOutput += "_ulink = 1;" & vbCrLf
                myOutput += "urchinTracker();" & vbCrLf
                myOutput += "</script>" & vbCrLf
                ClientScript.RegisterStartupScript(Me.GetType, "swgooglecode", myOutput)
            Catch
            End Try
            '*** End New Code
        End If
        ' end: JDB - Google Analytics
    End Sub
#End Region
    Sub CheckAdminLoggedIn()
        'Begin mod Admin User management - 7/07
        'check if user logged or user not accessing general access pages then redirect to login page
        'else no need to redirect
        Dim adminUrl As String = StoreFrontConfiguration.SSLPath
        If adminUrl.Substring(adminUrl.Length - 1, 1) <> "/" Then
            adminUrl &= adminUrl.ToLower & "/"
        End If
        Dim generalAccessPage As Boolean = IIf(Request.Url.PathAndQuery.ToLower.IndexOf("adminlogin.aspx") <> -1, True, False)
        If IsNothing(Session("Admin")) Then
            ' begin: JDB - 8/7/2007 - MT Admin User Management
            ' note: build in admin account
            If Request.Url.PathAndQuery.ToLower.IndexOf("/management/") <> -1 And generalAccessPage = False And Request.Url.PathAndQuery.ToLower.IndexOf("/management/authentication/") = -1 And Not mOverrideLogIn Then 'redirect to 
                Response.Redirect("AdminLogin.aspx?returnpage=" & Request.Url.PathAndQuery)
            End If
            ' end: JDB - 8/7/2007 - MT Admin User Management
            ' begin: JDB - 8/7/2007 - MT Admin User Management
            ' note: Removing the functionality that logs the user out when they leave the management folder.
            '   This had to be removed as it will not work in a shared SSL environment and the behavior needs
            '   to be constent.
            'Else
            '    If Request.Url.PathAndQuery.ToLower.IndexOf("/management/") = -1 Then  'end Session - user out of management
            '        Session("Admin") = Nothing
            '    End If
            ' end: JDB - 8/7/2007 - MT Admin User Management
        End If
        'End mod Admin User management
    End Sub
    Sub AnonymousRestrictedPages()
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        'check if following pages need to login and if user logged in as anonymous, redirect to the login page
        If IsNothing(Session("anonymous")) = False Then
            Dim query As String = ""
            Dim path As String = ""
            Dim webid As String = ""
            If Not IsNothing(Request.QueryString("webid")) Then
                If Request.QueryString("webid") <> "" Then
                    webid = Request.QueryString("webid")
                Else
                    webid = Session("webid")
                End If
            Else
                webid = Session("webid")
            End If
            Dim curPageName As String = Request.Url.PathAndQuery.Substring(Request.Path.LastIndexOf("/") + 1)
            If curPageName.IndexOf("?") <> -1 Then
                path = curPageName.Substring(0, curPageName.IndexOf("?"))
                query = curPageName.Substring(curPageName.IndexOf("?"))
            Else
                path = curPageName
            End If
            If AnonRestrictedwebPages.IndexOf(path.ToLower) <> -1 And path.Trim <> "" Then  'login required redirect to custsignin page
                Response.Redirect("custsignin.aspx?webID=" & webid & "&signout=2&returnpage=" & path & query)
            End If
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
    End Sub
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
        Dim tempLWidth As String = ""
        Dim tempRWidth As String = ""


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
            'BEGIN: GJV - 8/22/2007 - OSP merge
                If tempLWidth.EndsWith("%") And tempRWidth.EndsWith("%") Then
                    tempWidth = CStr(100 - CDbl(tempLWidth.Replace("%", "")) - CDbl(tempRWidth.Replace("%", ""))) & "%"
                    If (IsNothing(FindControl("ContentCell")) = False) Then CType(FindControl("ContentCell"), HtmlTableCell).Width = tempWidth
                    If (IsNothing(FindControl("tdContent")) = False) Then CType(FindControl("tdContent"), HtmlTableCell).Width = tempWidth
                ElseIf Not PageSubTable.Width.EndsWith("%") Then
                    If Not tempLWidth.EndsWith("%") AndAlso Not tempRWidth.EndsWith("%") Then
                        tempWidth = CStr(CDbl(PageSubTable.Width) - CDbl(tempLWidth) - CDbl(tempRWidth))
            'END: GJV - 8/23/2007 - OSP merge
            If (IsNothing(FindControl("ContentCell")) = False) Then CType(FindControl("ContentCell"), HtmlTableCell).Width = tempWidth
            If (IsNothing(FindControl("tdContent")) = False) Then CType(FindControl("tdContent"), HtmlTableCell).Width = tempWidth
        End If
        'BEGIN: GJV - 8/23/2007 - OSP merge
                End If
            End If
        'END: GJV - 8/23/2007 - OSP merge
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

        Dim objItemAdded As New CCartItem
        Dim oCartItem As CCartItem
        Try

            If (Not m_bAttribute_Error) Then
                'Tee 9/11/2007 product configurator, added condition check
                If Not TypeOf sender Is String Then
                    '9/7/07 Update item via inventory check
                    Try
                        Dim invChecker As New InventoryRequester
                        Dim objButton As LinkButton = sender
                        Dim oprodManagement As New CProductManagement
                        m_OrderAttributes = Session("OrderAttributes")

                        Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(objButton.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
                        oCartItem = New CCartItem(drProd, CLng(objButton.CommandArgument), m_OrderAttributes, m_objCustomer.CustomerGroup, , Session("BundledProducts"))
                        Dim code As String = oprodManagement.GetSkuOrCode(CLng(objButton.CommandName), oCartItem.Attributes)
                        If code <> String.Empty Then
                            Dim quantity As Integer
                            quantity = oCartItem.Quantity
                            If invChecker.CheckCartInventory(code, quantity) Then
                                oprodManagement.UpdateQuantity(CLng(objButton.CommandName), oCartItem.Attributes, quantity)
                            End If
                        End If
                    Catch ex As Exception
                        RaiseEvent USER_ERROR(m_objMessages.GetXMLMessage("AddToCart", "InventoryCheck", "InventoryCheck"), EventArgs.Empty)
                        Exit Sub
                    End Try
                End If
                'end Tee

                If IsNothing(objCart) Then
                    'normal non-BackOrdered Item
                    Dim objButton As LinkButton = sender
                    If (CLng(objButton.CommandArgument) > 0) Then
                        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                            oCartItem = New CCartItem(m_objXMLAccess.GetProduct(objButton.CommandName), CLng(objButton.CommandArgument), m_OrderAttributes, m_objCustomer.CustomerGroup)
                        Else
                            Dim oprodManagement As New CProductManagement
                            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(objButton.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
                            oCartItem = New CCartItem(drProd, CLng(objButton.CommandArgument), m_OrderAttributes, m_objCustomer.CustomerGroup, , Session("BundledProducts"))
                            oprodManagement = Nothing
                        End If

                        If Not oCartItem.Inventory Is Nothing Then
                            Dim qty As Integer = oCartItem.Quantity
                            If oCartItem.Inventory.InventoryTracked Then
                                Dim objProd As CCartItem
                                For Each objProd In m_objXMLCart.GetCartItems()
                                    '2507
                                    If objProd.ProductID = oCartItem.ProductID Then
                                        If objProd.Attributes.Count > 0 Then
                                            If objProd.Inventory.get_Id(objProd.Attributes) = oCartItem.Inventory.get_Id(oCartItem.Attributes) Then
                                                qty = objProd.Quantity + qty
                                            End If
                                        Else
                                            qty = objProd.Quantity + qty
                                        End If
                                        '2507
                                    End If
                                Next

                                If oCartItem.Inventory.ItemsAreStocked(oCartItem.Attributes, qty) Then
                                    'in stock so its a normal order
                                    m_objXMLCart.AddItem(oCartItem)
                                Else
                                    If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
                                        'pop then inventory message page and let it do the logic
                                        'to allow the user to backorder or not
                                        If IsFromDetail Then sPage = "Detail.aspx"

                                        ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                                                              & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
                                        Session("IventoryInfo") = oCartItem
                                        Exit Sub
                                    Else
                                        ' add the javascript popup
                                        Dim alist As New ArrayList
                                        alist.Add(oCartItem)
                                        ClientScript.RegisterStartupScript(Me.GetType, "myscript", CreateInventoryConfirmScript(alist))
                                        Session("IventoryInfo") = oCartItem
                                        Exit Sub
                                    End If
                                End If
                                'Else 'normal ORder
                                'Tee 8/16/2007 product configurator
                            ElseIf Not oCartItem.Inventory.InventoryTracked AndAlso _
                                oCartItem.ProductType <> ProductType.Normal AndAlso _
                                oCartItem.ProductType <> ProductType.Subscription Then
                                For Each _item As CCartItem In oCartItem.BundledProducts
                                    If _item.Inventory.InventoryTracked Then
                                        If Not _item.Inventory.ItemsAreStocked(_item.Attributes, _item.Quantity) Then
                                            If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
                                                'pop then inventory message page and let it do the logic
                                                'to allow the user to backorder or not
                                                ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                                                & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=Detail.aspx');</script>")
                                                Session("IventoryInfo") = oCartItem
                                                Session("BundleItem") = _item
                                                Exit Sub
                                            Else
                                                ' add the javascript popup
                                                Dim alist As New ArrayList
                                                alist.Add(_item)
                                                ClientScript.RegisterStartupScript(Me.GetType, "myscript", CreateInventoryConfirmScript(alist))
                                                Session("IventoryInfo") = oCartItem
                                                Session("BundleItem") = _item
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Next
                                m_objXMLCart.AddItem(oCartItem)
                                'end Tee
                            Else
                                m_objXMLCart.AddItem(oCartItem)
                            End If
                            objItemAdded = oCartItem
                        End If
                    End If
                Else
                    'we are here because the user selected to backorder Item(s)
                    m_objXMLCart.AddItem(objCart, False)
                    objItemAdded = objCart
                    Session.Remove("IventoryInfo")
                    'Tee 8/22/2007 product configurator
                    Session.Remove("BundleItem")
                    'end Tee
                End If

                'clear the Session Ordered Attributes
                Session.Remove("OrderAttributes")
                Session("ItemAdded") = objItemAdded 'm_objXMLCart.GetCartItems().Item(m_objXMLCart.GetCartItems.Count() - 1)
                If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
                    If IsNothing(sender) = True Then
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                          & "  language='JavaScript'>javascript: popUpWindow();</script>")
                    Else
                        Try
                            If sender.ToString <> "userNotified" Then
                                ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                                                                        & "  language='JavaScript'>javascript: popUpWindow();</script>")
                            End If
                        Catch
                            ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
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

                If (Not IsNothing(RightColumnNav1)) AndAlso Not IsNothing(RightColumnNav1.CartListControl) Then
                    RightColumnNav1.CartListControl.LoadCart()
                    Exit Sub
                End If

                If (Not IsNothing(LeftColumnNav1) AndAlso (Not IsNothing(LeftColumnNav1.CartListControl))) Then
                    LeftColumnNav1.CartListControl.LoadCart()
                    Exit Sub
                End If
            End If
        Catch err As SystemException
            'RaiseEvent USER_ERROR(err.Message, EventArgs.Empty)

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
        'Tee 7/23/2007 product configurator
        If (CLng(sender.CommandArgument) > 0) Then
            Dim oprodManagement As New CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(sender.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
            If drProd("ProductType") = ProductType.Customized OrElse drProd("ProductType") = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            ElseIf drProd("ProductType") = ProductType.Bundle OrElse drProd("ProductType") = ProductType.BundleSubscription Then
                Response.Redirect("Detail.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            Else
                AddItemToCart(sender, e)
            End If
        End If
        'end Tee
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
        'Tee 7/23/2007 product configurator
        If (CLng(sender.CommandArgument) > 0) Then
            Dim oprodManagement As New CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(sender.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
            If drProd("ProductType") = ProductType.Customized OrElse drProd("ProductType") = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            ElseIf drProd("ProductType") = ProductType.Bundle OrElse drProd("ProductType") = ProductType.BundleSubscription Then
                Response.Redirect("Detail.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            Else
                AddItemToCart(sender, e)
            End If
        End If
        'end Tee
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
        'Tee 7/23/2007 product configurator
        If (CLng(sender.CommandArgument) > 0) Then
            Dim oprodManagement As New CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(sender.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
            If drProd("ProductType") = ProductType.Customized OrElse drProd("ProductType") = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            ElseIf drProd("ProductType") = ProductType.Bundle OrElse drProd("ProductType") = ProductType.BundleSubscription Then
                Response.Redirect("Detail.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            Else
                AddItemToCart(sender, e)
            End If
        End If
        'end Tee
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
        'Tee 7/23/2007 product configurator
        If (CLng(sender.CommandArgument) > 0) Then
            Dim oprodManagement As New CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(sender.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
            If drProd("ProductType") = ProductType.Customized OrElse drProd("ProductType") = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            Else
                AddItemToCart(sender, e)
            End If
        End If
        'end Tee
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
        'Tee 7/23/2007 product configurator
        If (CLng(sender.CommandArgument) > 0) Then
            Dim oprodManagement As New CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(sender.CommandName, m_objCustomer.CustomerGroup).Products.Rows(0)
            If drProd("ProductType") = ProductType.Customized OrElse drProd("ProductType") = ProductType.CustomizedSubscription Then
                Response.Redirect("BuildYourOwn.aspx?ID=" & sender.CommandName & "&Qty=" & sender.CommandArgument)
            Else
                AddItemToCart(sender, e)
            End If
        End If
        'end Tee
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

        Session.Remove("ItemAdded")
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
        'Response.Write("<meta name=""keywords"" content=""" & StoreFrontConfiguration.MetaKeywords & """>" & Chr(13))
        'Response.Write("<meta name=""description"" content=""" & StoreFrontConfiguration.MetaDescription & """>" & Chr(13))
        Dim myProduct As CCartItem = Nothing
        Dim myControl As CProductDetailBase

        ' Find which ProductDetail template is used
        myControl = Page.FindControl("ProductDetail11")
        If Not myControl Is Nothing Then
            If myControl.Visible = True Then
                ' ProductDetail1 template is used
                myProduct = myControl.Product
            End If
        End If
        myControl = Page.FindControl("ProductDetail21")
        If Not myControl Is Nothing Then
            If myControl.Visible = True Then
                ' ProductDetail2 template is used
                myProduct = myControl.Product
            End If
        End If

        ' Now proceed
        If Not myProduct Is Nothing Then
            Dim metaKeywords As String
            Dim metaDescription As String
            Dim objProdManagement As CProductManagement

            objProdManagement = New CProductManagement(myProduct.ProductID)

            metaKeywords = objProdManagement.Keywords()

            metaDescription = Regex.Replace(myProduct.Description, "<[^>]*>", "")
            metaDescription = Server.HtmlEncode(metaDescription.Replace(ControlChars.CrLf, ""))

            ' Write product keywords to header
            Response.Write("<meta name=""keywords"" content=""" & metaKeywords & """>" & Chr(13))

            ' Now write product info into the title
            Response.Write("<meta name=""description"" content=""" & metaDescription & """>" & Chr(13))
        Else
            ' Product info was not found => default SF behavior
            Response.Write("<meta name=""description"" content=""" & StoreFrontConfiguration.MetaDescription & """>" & Chr(13))
            Response.Write("<meta name=""keywords"" content=""" & StoreFrontConfiguration.MetaKeywords & """>" & Chr(13))
        End If
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
            ClientScript.RegisterStartupScript(Me.GetType, "ForceDefaultToScript", sb.ToString)
        End If

    End Sub


#End Region

#Region "Private Sub m_objXMLCart_BoPrompt(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPrompt"

    Private Sub m_objXMLCart_BoPrompt(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPrompt
        'this event was raised from m_objXMLCart letting us know user already had this product 
        'in his cart and that quantity plus the quantity trying to add was greater than the items in stock for that product
        'pop then inventory message page and let it do the logic
        'to allow the user to backorder or not
        If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
            'Dim obj As HtmlGenericControl
            If IsFromDetail Then
                sPage = "Detail.aspx"
            End If
            ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                                  & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
        Else
            Dim alist As New ArrayList
            alist.Add(CType(sender, CCartItem))
            ClientScript.RegisterClientScriptBlock(Me.GetType, "myscript", CreateInventoryConfirmScript(alist))
        End If
        Session("IventoryInfo") = sender 'sender is the new cartItem
    End Sub

#End Region

#Region "Private Sub m_objXMLCart_BoPromptCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPromptCart"
    Private Sub m_objXMLCart_BoPromptCart(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_objXMLCart.BoPromptCart
        'this event was raised from m_objXMLCart letting us know user already had this product 
        'in his cart and that quantity plus the quantity trying to add was greater than the items in stock for that product
        'pop then inventory message page and let it do the logic
        'to allow the user to backorder or not
        If (StoreFrontConfiguration.AdminStore.Item("AddProductStyle").InnerText = CStr(AddProductStyle.PopUp)) Then
            sPage = "ShoppingCart.aspx"
            ClientScript.RegisterClientScriptBlock(Me.GetType, "PopScript", "<script" _
                                  & "  language='JavaScript'> popUpInventory('InventoryMessage.aspx?sPage=" & sPage & "');</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "myscript", CreateInventoryConfirmScript(CType(sender, ArrayList)))
        End If
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
            '2641
            Dim admin As New CAdminGeneralManagement
            If admin.AdminGuid = "" Then
                admin.UpDateAdminGuid()
            End If
            Exit Sub
        End If
        '2729
        If StoreFrontConfiguration.StoreFrontLinkValue = -1 Then
            Dim admin As New CAdminGeneralManagement
            StoreFrontConfiguration.StoreFrontLinkValue = admin.UpDateStoreFrontLinkValue()
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
    Private Sub SearchTemplate11_EmptyResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchTemplate11.EmptyResult, SearchTemplate12.EmptyResult, SearchTemplate13.EmptyResult
        Instruction1.Visible = True
        Instruction1.Set_LabelText(sender.ToString)
    End Sub
    Private Function CreateInventoryConfirmScript(ByVal aList As ArrayList) As String
        Dim strMessage As String
        Dim strScript As String
        Dim oItem As CCartItem
        Try
            oItem = CType(aList.Item(0), CCartItem)
            Dim bOrder As Long = GetBackOrderQuantity(oItem)
            If oItem.Inventory.CanBackOrder Then
                strMessage = "We are sorry, our current stock levels are less than the quantity requested. Do you wish to backorder the remainder? " & bOrder & " item" & IIf(bOrder > 1, "s", "") & " will be backordered."
                strScript = " <script language='JavaScript'>  var agree = confirm('" & strMessage & "'); if (agree){ document.forms[0].myhiddenfield.value = '1'; document.forms[0].submit();  } </script>"
                If Request.FilePath.EndsWith("shoppingcart.aspx") Then
                    strScript = " <script language='JavaScript'>  var agree = confirm('" & strMessage & "'); if (agree){ document.forms[0].myhiddenfield.value = '1'; document.forms[0].submit();} else { location.href = " & Chr(34) & StoreFrontConfiguration.SiteURL & "Shoppingcart.aspx?BoAction=Cancel" & Chr(34) & "} </script> "
                End If
                Return strScript
            Else
                strMessage = "The selected item: " & oItem.Name & " is out of stock for the quantity that you requested!\nPlease lower the quantity and try again."
                strScript = " <script language='JavaScript'> alert('" & strMessage & "'); </script>"
                Return strScript
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function GetBackOrderQuantity(ByVal oCartItem As CCartItem) As Long
        Dim _item As CCartItem
        ' find the item from the cart which is being ordered
        ' calculate the backorder quantity if it was to be actually ordered

        For Each _item In m_objXMLCart.GetCartItems
            If _item.ProductID = oCartItem.ProductID Then
                Dim objTempItem As New CCartItem(_item, _item.Quantity + oCartItem.Quantity, _item.Attributes, _item.CustomerGroup)
                Return objTempItem.BackOrderQuantity
            End If
        Next
        Return oCartItem.BackOrderQuantity
    End Function
    Protected Sub InventoryBackOrderConfirm(ByVal strVal As String)
        Try
            Select Case strVal
                Case "1"
                    Dim objItem As CCartItem = Session("IventoryInfo")
                    If Not (Session("arIventoryInfo") Is Nothing) Then
                        Dim ar As ArrayList = Session("arIventoryInfo")
                        objItem = CType(ar(0), CCartItem)
                        Me.AddCartItem(objItem)
                        ar.RemoveAt(0)
                        If ar.Count > 0 Then
                            Session("arIventoryInfo") = ar
                            Session("oItem") = Nothing
                        Else
                            Session("arIventoryInfo") = Nothing
                        End If
                        Exit Sub
                    End If
                    CType(FindControl("myhiddenfield"), HtmlInputHidden).Value = "null"
                    AddItemToCart("userNotified", EventArgs.Empty, objItem)
                Case "2"
                    If IsNothing(Session("arIventoryInfo")) = False Then
                        Dim ar As ArrayList
                        ar = Session("arIventoryInfo")
                        ar.RemoveAt(0)
                        If ar.Count > 0 Then
                            Session("arIventoryInfo") = ar
                        Else
                            Session.Remove("arIventoryInfo")
                        End If
                    End If
            End Select
        Catch ex As Exception
            If TypeOf ex Is Threading.ThreadAbortException Then Exit Sub
            Session("DetailError") = "Class AddProductPopUp Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub ValidateSession()
        If Page.IsPostBack Then Return
        'if no webid then dont care, the session customer is valid 
        If Request.QueryString("WebID") = String.Empty Then
            Session.Remove(LASTWEBID)
            Return
        End If
        ' If CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) = -1 Then Return

        ' if there is no customer object in the session, then dont care
        If Session("Customer") Is Nothing Then Return
        'this means that this is a refresh button, and the customer in the session is valid
        If Request.QueryString("WebID") = Session(LASTWEBID) Then Return
        Session.Remove("Customer")
        Session.Remove("XMLShoppingCart")
        'Tee 11/15/2007 removed unnecessary session variable
        Session.Remove("ItemAdded")
        'end Tee
    End Sub

    Private Sub SetSession()
        SetCurrentCustomer()
        SetCurrentShoppingCart()
        SetCurrentAffiliate()
    End Sub
    Private Sub SetCurrentCustomer()
        If Session("Customer") Is Nothing Then
            Dim myWebId As String = IIf(Request.QueryString("WebID") = String.Empty, Guid.NewGuid.ToString, Request.QueryString("WebID"))
            m_objCustomer = New CCustomer(myWebId, StoreFrontConfiguration.XMLDocument)
            Session(LASTWEBID) = Request.QueryString("WebID")
            Session("Customer") = m_objCustomer
            If m_objCustomer.IsAnonymous Then
                Session("Anonymous") = 1
            Else
                Session("Anonymous") = Nothing
            End If
        Else
            m_objCustomer = Session("Customer")
        End If

        Session("WebID") = m_objCustomer.GetSessionID()
    End Sub
    Private Sub SetCurrentShoppingCart()
        If Session("XMLShoppingCart") Is Nothing Then
            m_objXMLCart = New CCart(m_objCustomer.GetSessionID(), m_objXMLAccess, (New CStoreDiscounts).GetDiscounts, m_objCustomer.CustomerGroup())
            m_objXMLCart.LoadFromDB()
            Session("XMLShoppingCart") = m_objXMLCart
        Else
            m_objXMLCart = Session("XMLShoppingCart")
        End If
        If (m_objXMLCart.OandaISO <> "") Then
            Session("OandaRate") = m_objXMLCart.OandaRate
            Session("ConvertISO") = m_objXMLCart.OandaISO
        Else
            If IsNothing(Session("OandaRate")) Then Session.Remove("OandaRate")
            If IsNothing(Session("ConvertISO")) Then Session.Remove("ConvertISO")
        End If
    End Sub
    Private Sub SetCurrentAffiliate()
        SetAffiliate()
        If (IsNothing(Session("Affiliate"))) Then
            m_Affiliate = Session("Affiliate")
        End If
        m_objCustomer.Referer = Session("Referer")
    End Sub
    'Begin User management
    'check if user have permission to view pages
    Function RestrictedPages(ByVal task As Integer) As Boolean
        If Not IsNothing(Session("Admin")) Then
            Dim objUserBase As New AdminUserBase
            objUserBase = Session("Admin")
            ' begin: JDB - 8/7/2007 - MT Admin User Management
            ' note: build in admin account
            If objUserBase.IsSuperUser OrElse objUserBase.Role.IsSuper Then
                Return False
            Else
                For Each objTasks As RoleTasks In objUserBase.Role.Tasks
                    If objTasks.TaskID = task Then
                        Return False
                    End If
                Next
            End If
            ' end: JDB - 8/7/2007 - MT Admin User Management
        End If
        Return True
    End Function

    Function RestrictedProductPages() As Boolean
        If Not Me.RestrictedPages(Tasks.ProductAddnew) _
        OrElse Not Me.RestrictedPages(Tasks.ProductGeneral) _
        OrElse Not Me.RestrictedPages(Tasks.ProductDetails) _
        OrElse Not Me.RestrictedPages(Tasks.ProductCategories) _
        OrElse Not Me.RestrictedPages(Tasks.ProductAttributes) _
        OrElse Not Me.RestrictedPages(Tasks.ProductImages) _
        OrElse Not Me.RestrictedPages(Tasks.Fulfillment) _
        OrElse Not Me.RestrictedPages(Tasks.Inventory) _
        OrElse Not Me.RestrictedPages(Tasks.Discounts) _
        OrElse Not Me.RestrictedPages(Tasks.Marketing) _
        OrElse Not Me.RestrictedPages(Tasks.BundleComponents) _
        OrElse Not Me.RestrictedPages(Tasks.CustomerDefinedRules) Then
            Return False
        Else
            Return True
        End If
    End Function

    Function RestrictedPaymentPages() As Boolean
        If Not Me.RestrictedPages(Tasks.PaymentMethods) _
        OrElse Not Me.RestrictedPages(Tasks.OnlineProcessing) _
        OrElse Not Me.RestrictedPages(Tasks.Encryption) _
        OrElse Not Me.RestrictedPages(Tasks.PayPal) Then
            Return False
        Else
            Return True
        End If
    End Function

    Sub RedirectToProductTab(ByVal iTask As Integer)
        If Me.RestrictedPages(iTask) Then
            ' TODO: cannot redirect to Attributes, Inventory or Fulfillment tabs if it is a Merchant or Customer Bundle
            If Not Me.RestrictedPages(Tasks.ProductGeneral) Then
                Response.Redirect("ProductGeneral.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.ProductDetails) Then
                Response.Redirect("ProductDetails.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.ProductCategories) Then
                Response.Redirect("ProductCategories.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.ProductAttributes) Then
                Response.Redirect("attributesmng.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.ProductImages) Then
                Response.Redirect("ProductImages.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.Fulfillment) Then
                Response.Redirect("ProductFulfillment.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.Inventory) Then
                Response.Redirect("inventorymng.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.Discounts) Then
                Response.Redirect("ProductDiscounts.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.Marketing) Then
                Response.Redirect("ProductMarketing.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.BundleComponents) OrElse Not Me.RestrictedPages(Tasks.CustomerDefinedRules) Then
                Dim oProductType As ProductType
                Dim iProductId As Integer = Session("ProductId")
                If iProductId > 0 Then
                    Dim oProductManagement As CProductManagement = New CProductManagement(iProductId)
                    oProductType = oProductManagement.ProductType
                ElseIf Request.QueryString("ProdType") <> "" Then
                    oProductType = Request.QueryString("ProdType")
                End If
                If oProductType > 0 Then
                    If Not Me.RestrictedPages(Tasks.BundleComponents) AndAlso (oProductType = ProductType.Bundle OrElse oProductType = ProductType.BundleSubscription) Then
                        Response.Redirect("BundleComponents.aspx?ProdType=2")
                    ElseIf oProductType = ProductType.Customized OrElse oProductType = ProductType.CustomizedSubscription Then
                        If Not Me.RestrictedPages(Tasks.BundleComponents) Then
                            Response.Redirect("BundleComponents.aspx?ProdType=4")
                        Else
                            Response.Redirect("CustomerDefinedRules.aspx")
                        End If
                    Else
                        Response.Redirect("Accessdenied.aspx")
                    End If
                Else
                    Response.Redirect("Accessdenied.aspx")
                End If
            Else
                Response.Redirect("Accessdenied.aspx")
            End If
        End If
    End Sub

    Sub RedirectToPaymentTab(ByVal iTask As Integer)
        If Me.RestrictedPages(iTask) Then
            If Not Me.RestrictedPages(Tasks.PaymentMethods) Then
                Response.Redirect("PaymentMethods.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.OnlineProcessing) Then
                Response.Redirect("PaymentProcessors.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.Encryption) Then
                Response.Redirect("Encryption.aspx")
            ElseIf Not Me.RestrictedPages(Tasks.PayPal) Then
                Response.Redirect("PayPal.aspx")
            Else
                Response.Redirect("Accessdenied.aspx")
            End If
        End If
    End Sub

    'end user management
#Region "Protected Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "")"
    Protected Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "", Optional ByVal condition As String = "")
        If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
            Dim script As New StringBuilder
            script.Append("if (isEnterKey(event)")
            If Not IsNothing(condition) AndAlso condition <> String.Empty Then
                script.Append(" && " & condition)
            End If
            script.Append(") ")
            script.Append("postBack(event, '" & objButton.UniqueID & "', '" & argument & "')")
            objTextControl.Attributes.Add("onKeyDown", script.ToString)
        End If
    End Sub
#End Region

    'begin: GJV - 7/31/2007 - CSR
    Public Sub CSRLicenseCheck()

        If Not CSRManagement.HasCSRLicense Then
            Response.Redirect(String.Format("{0}Management/PurchaseCSR.aspx", StoreFrontConfiguration.SSLPath))
        End If

    End Sub
    'end: GJV - 7/31/2007 - CSR

    'begin: GJV - 9/7/2007 - OSP merge
    Public ReadOnly Property ThemesPath() As String
        Get
            Return MyBase.ResolveUrl(StoreFrontConfiguration.ThemesPath)
        End Get
    End Property
    'end: GJV - 9/7/2007 - OSP merge

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overridable ReadOnly Property HelpUrl() As String
        Get
            Throw New NotImplementedException("Help URL has not been implemented")
        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class

Public Class PostBackHtmlTextWriter
    Inherits HtmlTextWriter

    'Private _isForm As Boolean = False
    Private _virtualURL As String = ""

    Public Sub New(ByVal writer As HtmlTextWriter, ByVal virtualURL As String)
        MyBase.New(writer)
        Me._virtualURL = virtualURL
    End Sub

    'Public Overloads Overrides Sub RenderBeginTag(ByVal tagName As String)
    '    Me._isForm = (String.Compare(tagName, "form") = 0)
    '    If tagName.ToLower = "form" Then
    '        Dim i As Integer = 0
    '    End If
    '    MyBase.RenderBeginTag(tagName)
    'End Sub

    'Public Overloads Overrides Sub RenderBeginTag(ByVal tagKey As HtmlTextWriterTag)
    '    Me._isForm = (tagKey = HtmlTextWriterTag.Form)
    '    If tagKey = HtmlTextWriterTag.Form Then
    '        Dim i As Integer = 0
    '    End If
    '    MyBase.RenderBeginTag(tagKey)
    'End Sub

    Public Overloads Overrides Sub WriteAttribute(ByVal name As String, ByVal value As String, ByVal encode As Boolean)
        'If String.Compare(name, "action") = 0 AndAlso Me._virtualURL.EndsWith(value) Then
        If String.Compare(name, "action") = 0 Then
            value = Me._virtualURL
        End If
        MyBase.WriteAttribute(name, value, encode)
    End Sub
End Class