Imports StoreFront
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports CSR.CSRBusinessRule

Public MustInherit Class CMenubar1
    Inherits CWebControl

	'BEGIN: GJV - 8/22/2007 - OSP merge
    Protected WithEvents Stuff2 As HtmlTableRow
    'END: GJV - 8/22/2007 - OSP merge
    Public Enum Display As Integer
        iHorizontal = 0
        iVertical = 1
    End Enum
    Private m_isAdmin As Boolean = False

    Protected WithEvents dlMenu As System.Web.UI.WebControls.DataList
    Private m_strStyle As String
    ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
    Private m_strDisabledStyle As String = ""
    ' end: JDB - 8/2/2007 - StoreFront Module Licensing
    Public imgpath As String = ""

#Region "Class members"
    Private m_objItems As ArrayList
    Private m_DisplayType As Integer
    Private m_sCallPage As String
    Private m_sPageClicked As String
#End Region

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

#Region " Sub Page_Load"
    '-----------------------------------------------------------
    ' Sub Page_Load
    ' Parameters 
    ' 
    ' Description:
    ' 
    '-----------------------------------------------------------
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        LoadNav()
    End Sub
#End Region

#Region "Sub LoadNav()"
    Private Sub LoadNav()
        Dim obusiness As New CBusinessBase
        If IsNothing(m_strStyle) Then
            m_strStyle = ""
        End If
        dlMenu.ItemStyle.CssClass = m_strStyle
        If IsAdminArea Then

            If (CallPage = "top") Then
                dlMenu.Visible = False
            Else
                Me.Visible = True
                dlMenu.Visible = True
                dlMenu.RepeatDirection = RepeatDirection.Vertical
                dlMenu.Width = New Unit("100%")


                m_objItems = obusiness.LoadMerchantMenus()


                Dim objItem As CMenuItem

                '# Quickbooks
                Dim strPath As String = Server.MapPath(".")
                If IO.Directory.Exists(strPath & "\quickbooks") Then
                    objItem = New CMenuItem
                    objItem.Link = StoreFrontConfiguration.SSLPath & "management/quickbooks/general.aspx"
                    objItem.DisplayName = "QuickBooks"
                    m_objItems.Add(objItem)
                End If


                '# Apply
                objItem = New CMenuItem
                objItem.DisplayName = "<b>Apply Change(s)</b>"
                m_objItems.Add(objItem)

                objItem = New CMenuItem
                ' begin: JDB - ReloadXML availability to all MT Users
                'objItem.Link = StoreFrontConfiguration.SiteURL & "ReloadXML.aspx?SSL=1"
                objItem.Link = StoreFrontConfiguration.SSLPath & "management/default.aspx?ReloadXML=1"
                ' end: JDB - ReloadXML availability to all MT Users
                objItem.DisplayName = "Apply To Site"
                m_objItems.Add(objItem)

                dlMenu.DataSource = m_objItems
                dlMenu.DataBind()


            End If

        Else
            If (CallPage = "top") Then
                dlMenu.RepeatDirection = RepeatDirection.Horizontal
            Else
                dlMenu.RepeatDirection = RepeatDirection.Vertical
                dlMenu.Width = New Unit("100%")
            End If

            Dim objMenuElement As IMenuAccess = Nothing

            Try
                objMenuElement = CType(Parent, IMenuAccess)
            Catch err As Exception
                Dim objParent As Object = Parent

                While True
                    Try
                        objMenuElement = CType(objParent.Parent, IMenuAccess)
                        Exit While
                    Catch objerr As Exception
                        objParent = objParent.Parent
                    End Try
                End While
            End Try

            If (IsNothing(objMenuElement.MenuNode) = False) Then
                m_objItems = New ArrayList
                'SKC 7.0 Anonymous Fix
                m_objItems = obusiness.LoadMenus(objMenuElement.MenuNode, CallPage, CurrentWebPage, m_objcustomer.IsSignedIn, m_objcustomer.GetSessionID, m_objCustomer.IsAnonymous)
                'End SKC
            End If

            dlMenu.DataSource = m_objItems
            dlMenu.DataBind()
        End If

    End Sub
#End Region

#Region "Property StyleClass() As String"
    Public Property StyleClass() As String
        Get
            Return m_strStyle
        End Get
        Set(ByVal Value As String)
            m_strStyle = Value
        End Set
    End Property
#End Region

    ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
#Region "Property DisabledStyleClass() As String"
    Public Property DisabledStyleClass() As String
        Get
            Return Me.m_strDisabledStyle
        End Get
        Set(ByVal Value As String)
            Me.m_strDisabledStyle = Value
        End Set
    End Property
#End Region
    ' end: JDB - 8/2/2007 - StoreFront Module Licensing

#Region "Sub ReloadNav()"
    Public Sub ReloadNav()
        LoadNav()
    End Sub
#End Region

#Region "Property DisplayType"
    Public Property DisplayType() As Integer
        Get
            Return m_DisplayType
        End Get
        Set(ByVal Value As Integer)
            m_DisplayType = Value

        End Set
    End Property
#End Region

#Region "Property CallPage"
    Public Property CallPage() As String
        Get
            Return m_sCallPage
        End Get
        Set(ByVal Value As String)
            m_sCallPage = Value
        End Set
    End Property
#End Region

#Region "Property CallPage"
    Public Property IsAdminArea() As Boolean
        Get
            Return m_isAdmin
        End Get
        Set(ByVal Value As Boolean)
            m_isAdmin = Value
        End Set
    End Property
#End Region

#Region "Property PageClicked"
    Public Property PageClicked() As String
        Get
            Return m_sPageClicked
        End Get
        Set(ByVal Value As String)
            m_sPageClicked = Value
        End Set
    End Property
#End Region

#Region "dlMenu_ItemCreated"
    '-----------------------------------------------------------
    ' Sub Handle_signIn
    ' Parameters 
    ' 
    ' Description:
    ' here we toggle the category control on or off depending on the link clicked

    '-----------------------------------------------------------

    Private Sub dlMenu_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMenu.ItemCreated
        Dim objCell As HtmlTableCell

        If (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then
            If (dlMenu.RepeatDirection = RepeatDirection.Horizontal) Then
                CType(e.Item.FindControl("TopBarSpacer"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("BottomBarSpacer"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("BottomBar"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("TopBar"), HtmlTableRow).Visible = False
                If (e.Item.ItemIndex > 0) Then
                    CType(e.Item.FindControl("Column21"), HtmlTableCell).Visible = False
                    CType(e.Item.FindControl("TopBarCell"), HtmlTableCell).ColSpan = 4
                    CType(e.Item.FindControl("BottomBarCell"), HtmlTableCell).ColSpan = 4
                End If
            ElseIf (dlMenu.RepeatDirection = RepeatDirection.Vertical) Then
                objCell = e.Item.FindControl("TopBarTD")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("BottomBarTD")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("Column11")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column12")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column21")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column22")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column13")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column23")
                objCell.Visible = False
                If (e.Item.ItemIndex > 0) Then
                    CType(e.Item.FindControl("TopBar"), HtmlTableRow).Visible = False
                End If
            End If

            If (IsNothing(m_strStyle) = False) Then
                CType(e.Item.FindControl("PageLink"), HyperLink).CssClass = m_strStyle

                objCell = e.Item.FindControl("LeftSpacerTD")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("ContentTD")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("RightSpacerTD")
                'BEGIN: GJV - 8/22/2007 - OSP merge
                'SFExpress
                If IsAdminArea Then
                    objCell.NoWrap = True
                Else
                    objCell.NoWrap = False
                End If
                'END: GJV - 8/22/2007 - OSP merge
                objCell.Attributes.Add("class", m_strStyle)
            End If

            Dim objItem As CMenuItem = e.Item.DataItem
            If (IsNothing(objItem) = False) Then
				'BEGIN: GJV - 8/29/2007 - OSP merge
				If Not IsNothing(e.Item.FindControl("CartList1")) Then
				    CType(e.Item.FindControl("CartList1"), CartList).Visible = False
				End If
				If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
				    CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False
                End If
                If Not IsNothing(e.Item.FindControl("Navigator1")) Then
                    CType(e.Item.FindControl("Navigator1"), Control).Visible = False
                End If
                'END: GJV - 8/29/2007 - OSP merge
                ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
                Select Case objItem.DisplayName.ToLower()
                    Case "liveperson"
                        If StoreFrontConfiguration.LivePersonID <> "" Then
                            CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = True
                            CType(e.Item.FindControl("LivePerson1"), LivePerson).LivePersonAccount = StoreFrontConfiguration.LivePersonID
                            CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                            CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                        End If
                    Case "storefront affilliate link"
                        CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                        CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = True
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        'update # 1834
                        imgpath = StoreFrontConfiguration.SSLPath & "images/sfaffiliate.jpg"
                        'BEGIN: GJV - 8/29/2007 - OSP merge
                    Case "shopping cart control"
                        CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                        CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        If Not IsNothing(e.Item.FindControl("CartList1")) Then
                            CType(e.Item.FindControl("CartList1"), CartList).Visible = True
                            If objItem.Level = 0 Then
                                CType(e.Item.FindControl("CartList1"), CartList).CssCls = "TopSubBannerText"
                            ElseIf objItem.Level = 1 Then
                                CType(e.Item.FindControl("CartList1"), CartList).CssCls = "LeftColumnText"
                            ElseIf objItem.Level = 2 Then
                                CType(e.Item.FindControl("CartList1"), CartList).CssCls = "RightColumnText"
                            ElseIf objItem.Level = 3 Then
                                CType(e.Item.FindControl("CartList1"), CartList).CssCls = "FooterText"
                            Else
                                CType(e.Item.FindControl("CartList1"), CartList).CssCls = "Content"
                            End If
                        End If
                    Case "simple search control"
                        CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                        CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = True
                            If objItem.Level = 0 Then
                                CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "TopSubBannerText"
                            ElseIf objItem.Level = 1 Then
                                CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "LeftColumnText"
                            ElseIf objItem.Level = 2 Then
                                CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "RightColumnText"
                            ElseIf objItem.Level = 3 Then
                                CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "FooterText"
                            Else
                                CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "Content"
                            End If
                        End If
                    Case "all categories"
                        CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                        CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        If Not IsNothing(e.Item.FindControl("Navigator1")) Then
                            CType(e.Item.FindControl("Navigator1"), Control).Visible = True
                        End If
                        'END: GJV - 8/29/2007 - OSP merge
                    Case Else
                        'Logic to disable link that user don't have permission to view
                        If Request.Url.PathAndQuery.ToLower.IndexOf("/management/") <> -1 Then
                            If Not GeneralAccess(objItem.DisplayName) Then
                                If RestrictedPages(objItem.DisplayName) Then
                                    objCell = e.Item.FindControl("ContentTD")
                                    objCell.Attributes.Remove("class")
                                    objCell.Attributes.Add("class", Me.m_strDisabledStyle)
                                    CType(e.Item.FindControl("PageLink"), HyperLink).Enabled = False
                                    CType(e.Item.FindControl("PageLink"), HyperLink).CssClass = Me.m_strDisabledStyle
                                ElseIf (objItem.DisplayName.ToLower() = "manage csr options" AndAlso Not CSRManagement.HasCSRLicense()) OrElse _
                                    (objItem.DisplayName.ToLower.Equals("storefront connector") AndAlso Not StoreFrontConfiguration.AreWebServicesActive) OrElse _
                                    (objItem.DisplayName.ToLower.Equals("customer defined bundles") AndAlso Not StoreFrontConfiguration.AreCustomerBundlesActive) OrElse _
                                    (objItem.DisplayName.ToLower.Equals("merchant bundles") AndAlso Not StoreFrontConfiguration.AreMerchantBundlesActive) Then
                                    ' note: if user has permission to view but does not have a license,
                                    '   the link needs to be clickable
                                    objCell = e.Item.FindControl("ContentTD")
                                    objCell.Attributes.Remove("class")
                                    objCell.Attributes.Add("class", Me.m_strDisabledStyle)
                                    CType(e.Item.FindControl("PageLink"), HyperLink).CssClass = Me.m_strDisabledStyle
                                End If
                            End If
                        Else
                            CType(e.Item.FindControl("PageLink"), HyperLink).Enabled = True
                            CType(e.Item.FindControl("PageLink"), HyperLink).Visible = True
                        End If
                        CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                End Select
                'If (objItem.DisplayName.ToLower() = "liveperson" And StoreFrontConfiguration.LivePersonID <> "") Then
                '    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = True
                '    CType(e.Item.FindControl("LivePerson1"), LivePerson).LivePersonAccount = StoreFrontConfiguration.LivePersonID
                '    CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                '    CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                'ElseIf (objItem.DisplayName.ToLower() = "storefront affilliate link") Then
                '    CType(e.Item.FindControl("PageLink"), HyperLink).Visible = False
                '    CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = True
                '    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                '    'update # 1834
                '    imgpath = StoreFrontConfiguration.SSLPath & "images/sfaffiliate.jpg"
                'Else
                '    CType(e.Item.FindControl("PageLink"), HyperLink).Visible = True
                '    CType(e.Item.FindControl("PageLink2"), HyperLink).Visible = False
                '    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                'End If
                ' end: JDB - 8/2/2007 - StoreFront Module Licensing
            End If
        End If
    End Sub
    Function GeneralAccess(ByVal DisplayName As String) As Boolean
        Select Case DisplayName.ToLower.Replace("<b>", "").Replace("</b>", "")
            Case "store management"
                Return True
            Case "marketing & promotions"
                Return True
            Case "store inventory"
                Return True
            Case "store settings"
                Return True
            Case "store design"
                Return True
            Case "apply change(s)"
                Return True
            Case "apply to site"
                Return True
            Case Else
                Return False
        End Select
    End Function
#End Region

End Class

'm_objItems = New ArrayList()

'Dim objItem As New CMenuItem()

'objItem.DisplayName = "<B>Store Management</B>"

'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/StoreReports.aspx"
'objItem.DisplayName = "Sales Reports"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/orderfulfillment.aspx"
'objItem.DisplayName = "Orders"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/affiliatepayments.aspx"
'objItem.DisplayName = "Affiliates"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/customers.aspx"
'objItem.DisplayName = "Customers"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/managepricegroups.aspx"
'objItem.DisplayName = "Price Groups"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/managegiftcertificates.aspx"
'objItem.DisplayName = "Gift Certificates"
'm_objItems.Add(objItem)

''# Marketing and Promotions
'objItem = New CMenuItem()
'objItem.DisplayName = "<B>Marketing & Promotions</B>"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/storediscounts.aspx"
'objItem.DisplayName = "Storewide Discounts"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/storecoupons.aspx"
'objItem.DisplayName = "Coupons"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/PromotionalMail.aspx"
'objItem.DisplayName = "Promotional Mail"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/SearchEngineSubmission.aspx"
'objItem.DisplayName = "Search Engines"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/marketplaces.aspx"
'objItem.DisplayName = "Marketplaces"
'm_objItems.Add(objItem)

''# Store Inventory
'objItem = New CMenuItem()
'objItem.DisplayName = "<b>Store Inventory</b>"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/productimport.aspx"
'objItem.DisplayName = "Import Products"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/manageproducts.aspx"
'objItem.DisplayName = "Products"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/attributestemplate.aspx"
'objItem.DisplayName = "Attributes"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/managecategories.aspx"
'objItem.DisplayName = "Categories"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/managemanufacturers.aspx"
'objItem.DisplayName = "Manufacturers"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/managevendors.aspx"
'objItem.DisplayName = "Vendors"
'm_objItems.Add(objItem)

''# Store Settings
'objItem = New CMenuItem()
'objItem.DisplayName = "<b>Store Settings</b>"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/general.aspx"
'objItem.DisplayName = "General"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/onlinechat.aspx"
'objItem.DisplayName = "Online Chat"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/ManageEmail.aspx"
'objItem.DisplayName = "E-Mail"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/shippinghandling.aspx"
'objItem.DisplayName = "Shipping"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/paymentmethods.aspx"
'objItem.DisplayName = "Payments"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/geography.aspx"
'objItem.DisplayName = "Localization"
'm_objItems.Add(objItem)

'objItem = New CMenuItem()
'objItem.Link = StoreFrontConfiguration.SSLPath & "management/tax.aspx"
'objItem.DisplayName = "Tax"
'm_objItems.Add(objItem)