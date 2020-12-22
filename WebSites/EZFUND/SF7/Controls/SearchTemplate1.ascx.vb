Imports System.Xml
Imports System.Math
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Enum AlignmentType
    Left = 0
    Right = 1
    Center = 2
    Alternate = 3
End Enum

Partial  Class SearchTemplate1
    Inherits CSearchResultBase
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    ' begin: JDB - Search Filters
    Protected WithEvents SearchFiltersControl1 As SearchFiltersControl
    ' end: JDB - Search Filters
    Private isLive As Boolean = False
    Private arFake(0) As String
    Private sElasped As String
    Private m_arResults As ArrayList
    Private m_arResultItems As ArrayList
    Private mRecordsToGet As Long = 0
    Private m_bPageLoad As Boolean = False

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

#Region "Private Sub Page_Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Visible = False Then Exit Sub

        ' begin: JDB - 2/20/2007 - UrlRewriter Add-On
        ' note: categoryid, categoryname and page should all be defined in the query string only if the request url was rewritten
        Dim iCurrentPageIndex As Integer = 1
        If Me.IsRewrittenURL Then
            iCurrentPageIndex = Convert.ToInt32(Request.QueryString("page")) - 1
            Me.SearchTemplate1PageIndex = iCurrentPageIndex
            Me.mPageIndex = iCurrentPageIndex
        End If
        ' end: JDB - 2/20/2007 - UrlRewriter Add-On

        LoadSettings()
        m_bPageLoad = True
        mRecordsToGet = m_nProductsPerRow * Me.m_nRows
        qsobjexist = HaQsParams()
        'Tee 9/26/2007 bug #312 changed postback condition, rebind instead of cache
        If Not Page.IsPostBack OrElse Request.Form("__EVENTTARGET").ToLower.IndexOf("stockinfo") <> -1 Then
            Session("Popup") = Nothing
            Me.objStorage = SetSearch()
            BindSearchData()
        End If
        'end Tee
        MyBase.PageIndex = Me.SearchTemplate1PageIndex
    End Sub
#End Region

    ' begin: JDB - 2/29/2007 - UrlRewriter Add-On
    Private ReadOnly Property IsRewrittenURL() As Boolean
        Get
            Return ((Not IsNothing(Request.QueryString("categoryid"))) AndAlso Request.QueryString("categoryid").Length > 0 AndAlso (Not IsNothing(Request.QueryString("categoryname"))) AndAlso Request.QueryString("categoryname").Length > 0 AndAlso (Not IsNothing(Request.QueryString("page"))) AndAlso Request.QueryString("page").Length > 0)
        End Get
    End Property
    ' end: JDB - 2/29/2007 - UrlRewriter Add-On

#Region "BindSearchData()"

    Private Sub BindSearchData()
        Dim myCategoriesAreGrouped As Boolean = False
        MyBase.PageIndex = Me.SearchTemplate1PageIndex

        If objStorage.CategoryID > 0 Then
            myCategoriesAreGrouped = True
        End If
        m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
        ' begin: JDB - Search Filters
        Dim oSearchFilters As Hashtable = SearchFiltersControl1.GetSearchFilters
        m_arResultItems = m_objSearchEngine.StandardSearch(objStorage, oSearchFilters, myCategoriesAreGrouped, Me.SearchTemplate1PageIndex)
        Dim oSearchFilterValues As Hashtable = m_objSearchEngine.SearchFilterValues
        Me.SearchFiltersControl1.CreateControls(oSearchFilterValues)
        ' end: JDB - Search Filters
        If m_arResultItems.Count = 0 Then EmptyResults(StoreFrontConfiguration.MessagesAccess().GetXMLMessage("SearchResult.aspx", "Suggestion", "NoResult"))
        DataGrid1.AllowCustomPaging = True
        DataGrid1.PageSize = 1
        Dim dblVirtItemCount As Double = (m_objSearchEngine.ResultsCount / mRecordsToGet)
        If (Round(dblVirtItemCount) < dblVirtItemCount) Then
            dblVirtItemCount = Round(dblVirtItemCount) + 1
        End If
        DataGrid1.VirtualItemCount = dblVirtItemCount
        lblCount.Text = " " & m_objSearchEngine.ResultsCount
        If (m_arResultItems.Count = 1) Then
            lblProducts.Text = "Product"
        Else
            lblProducts.Text = "Products"
        End If
        DataGrid1.DataSource = m_arResultItems

        DataGrid1.CurrentPageIndex = Me.PageIndex
        DataGrid1.DataBind()
        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        End If
        SetDisplayInfo()
        CheckOanda()
        LabelText(ResultInfo)
        'begin: GJV - 6/12/2007 - Attribute Detail Hotfix
        Dim dgi As DataGridItem
        For Each dgi In DataGrid1.Items
            Dim dl As DataList = DirectCast(dgi.FindControl("DataList1"), DataList)
            Dim dli As DataListItem
            For Each dli In dl.Items
                Dim ac As CAttributeControl = DirectCast(dli.FindControl("CAttributeControl1"), CAttributeControl)
                ac.SetAttributeControlDisplay()
            Next dli
        Next dgi
        'end: GJV - 6/12/2007 - Attribute Detail Hotfix
    End Sub

#End Region

    ' begin: JDB - Search Filters
    Private Sub SearchFiltersControl_FilterResults(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchFiltersControl1.FilterResults
        Me.SearchTemplate1PageIndex = 0
        Me.BindSearchData()
    End Sub
    ' end: JDB - Search Filters

#Region "Private Sub SetDisplayInfo()"
    Private Sub SetDisplayInfo()
        If (objStorage.Keyword.Trim() = "") Then
            lblKeyword.Text = "All"
        Else
            lblKeyword.Text = objStorage.Keyword
        End If
        If (objStorage.CategoryID = -1) Then
            Dim strCatLabel As String
            strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
            lblCategoryName.Text = "All " & strCatLabel
        Else
            'Get Category Name
            Dim objCategoryAccess As New CSearchResult(dom.Item("SiteProducts").Item("CategoryTree"))
            lblCategoryName.Text = objCategoryAccess.GetCategoryName(objStorage.CategoryID)
        End If
    End Sub
#End Region

#Region "Private Sub CheckOanda()"

    Private Sub CheckOanda()
        If (IsNothing(Session("OandaChange")) = False) Then
            Session("OandaChange") = Nothing
        End If
    End Sub

#End Region


#Region " Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim objbutton As LinkButton = sender
        'm_lngCurrentID = CLng(objbutton.CommandName)

        Dim objDataListItem As New DataListItem(0, ListItemType.Item)
        Dim objParent As Object = sender
        Dim oTblRow As HtmlTableRow
        While (Not objParent.GetType() Is objDataListItem.GetType)
            objParent = objParent.Parent
        End While
        objDataListItem = CType(objParent, DataListItem)
        oTblRow = objDataListItem.FindControl("trStockStatus")
        If Not IsNothing(oTblRow) Then
            If oTblRow.Visible = False Then
                oTblRow.Visible = True
            Else
                oTblRow.Visible = False
            End If
        End If
    End Sub
#End Region

#Region "Sub AddCart(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub AddCart(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objDataListItem As New DataListItem(0, ListItemType.Item)
        Dim objParent As Object = sender

        While (Not objParent.GetType() Is objDataListItem.GetType)
            objParent = objParent.Parent
        End While

        objDataListItem = objParent
        AddCartClick(sender, e, objDataListItem)
        Me.objStorage = SetSearch()
        BindSearchData()
    End Sub
#End Region

#Region "Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound"
    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Dim i As Long = 0
        If (e.Item.DataSetIndex > -1) Then
            Dim ar As ArrayList
            Dim dlItem As DataListItem
            Dim oResult As CCategoryStorage
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                ar = m_arResultItems((DataGrid1.PageSize * DataGrid1.CurrentPageIndex) + e.Item.ItemIndex)
            Else
                ar = m_arResultItems
            End If

            CType(e.Item.FindControl("DataList1"), DataList).ItemStyle.Width = New Unit((Me.m_nProductsPerRow / 100) * 100 & "%")
            CType(e.Item.FindControl("DataList1"), DataList).DataSource = ar
            CType(e.Item.FindControl("DataList1"), DataList).DataBind()

            For Each dlItem In CType(e.Item.FindControl("DataList1"), DataList).Items
                LoadDefaults(dlItem.ItemIndex, dlItem, ar)

                setlabelvisible(dlItem, MyBase.m_bDisplayLabels)
                LabelText(dlItem)

                oResult = (ar(dlItem.ItemIndex))

                If (IsNothing(dlItem.FindControl("CAttributeControl1")) = False) Then
                    CType(dlItem.FindControl("CAttributeControl1"), CAttributeControl).DisplayType = m_AttributeDisplay '1740
                    CType(dlItem.FindControl("CAttributeControl1"), CAttributeControl).Data_Bind(oResult, m_objCustomer) '1521
                    If (dlItem.FindControl("CAttributeControl1").Visible = True And m_bDisplayAddToCart = False) Then
                        If (IsNothing(dlItem.FindControl("AttributesCell")) = False) Then
                            dlItem.FindControl("AttributesCell").Visible = False
                        End If
                    ElseIf (dlItem.FindControl("CAttributeControl1").Visible = False) Then
                        If (IsNothing(dlItem.FindControl("AttributesCell")) = False) Then
                            dlItem.FindControl("AttributesCell").Visible = False
                        End If
                    End If
                End If
                If (IsNothing(dlItem.FindControl("imgAddToCart")) = False) Then
                    CType(dlItem.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value)
                End If
                If (IsNothing(dlItem.FindControl("imgAddToSavedCart")) = False) Then
                    CType(dlItem.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value)
                End If
                If (IsNothing(dlItem.FindControl("imgEMailFriend")) = False) Then
                    CType(dlItem.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value())
                End If
            Next
        End If
    End Sub
#End Region

#Region "Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated"
    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            ' begin: JDB - 2/20/2007 - UrlRewriter Add-On
            ' note: categoryid, categoryname and page should all be defined in the query string only if the request url was rewritten
            If Me.IsRewrittenURL Then
                Dim sCategoryName As String = Request.QueryString("categoryname")
                Dim iCategoryId As Integer = Convert.ToInt32(Request.QueryString("categoryid"))
                Dim bControlReplaced As Boolean = True
                Dim oLink As HtmlAnchor
                Dim oSpace As Literal
                ' note: find the control for each page and replace it with a link
                Do While bControlReplaced
                    bControlReplaced = False
                    For iControlIndex As Integer = 0 To objCell.Controls.Count - 1
                        Dim oControl As Control = objCell.Controls(iControlIndex)
                        ' note: must reference the type of the control as string instead of a type because the
                        '   actual type is private
                        If oControl.GetType().ToString() = "System.Web.UI.WebControls.DataGridLinkButton" Then
                            bControlReplaced = True

                            Dim sPageDisplay As String = CType(oControl, LinkButton).Text
                            Dim iPageNumber As Integer = Convert.ToInt32(CType(oControl, LinkButton).CommandArgument)
                            objCell.Controls.Remove(oControl)

                            oLink = New HtmlAnchor
                            oLink.HRef = "../" + StoreFrontConfiguration.GetCategoryLink(iCategoryId, sCategoryName, iPageNumber)
                            oLink.InnerText = sPageDisplay
                            objCell.Controls.AddAt(iControlIndex, oLink)
                        End If
                    Next ' iControlIndex
                Loop ' bControlReplaced
                ' note: add previous and next buttons
                If Me.SearchTemplate1PageIndex > 0 Then
                    oLink = New HtmlAnchor
                    oLink.HRef = "../" + StoreFrontConfiguration.GetCategoryLink(iCategoryId, sCategoryName, Me.SearchTemplate1PageIndex)
                    oLink.InnerText = "< Previous"
                    objCell.Controls.AddAt(0, oLink)

                    oSpace = New Literal
                    oSpace.Text = "&nbsp;"
                    objCell.Controls.AddAt(1, oSpace)
                End If
                If Me.SearchTemplate1PageIndex < Me.DataGrid1.PageCount - 1 Then
                    oSpace = New Literal
                    oSpace.Text = "&nbsp;"
                    objCell.Controls.Add(oSpace)

                    oLink = New HtmlAnchor
                    oLink.HRef = "../" + StoreFrontConfiguration.GetCategoryLink(iCategoryId, sCategoryName, Me.SearchTemplate1PageIndex + 2)
                    oLink.InnerText = "Next >"
                    objCell.Controls.Add(oLink)
                End If
            Else
                ' end: JDB - 2/20/2007 - UrlRewriter Add-On
                Dim objSpace As Label
                Dim objButton As New LinkButton

                If (Me.SearchTemplate1PageIndex > 0) Then
                    objSpace = New Label
                    objSpace.Text = "&nbsp;"
                    objCell.Controls.AddAt(0, objSpace)
                    objButton.Text = "< Previous"
                    objCell.Controls.AddAt(0, objButton)
                End If
                If (Me.SearchTemplate1PageIndex < DataGrid1.PageCount - 1) Then
                    objSpace = New Label
                    objSpace.Text = "&nbsp;"
                    objButton = New LinkButton
                    objButton.Text = "Next >"
                    objCell.Controls.Add(objSpace)
                    objCell.Controls.Add(objButton)
                End If
                ' begin: JDB - 2/20/2007 - UrlRewriter Add-On
            End If
            ' end: JDB - 2/20/2007 - UrlRewriter Add-On
        ElseIf (e.Item.ItemType = ListItemType.AlternatingItem Or _
            e.Item.ItemType = ListItemType.Item) Then
            If (m_bPageLoad = True) Then
                CType(e.Item.FindControl("DataList1"), DataList).RepeatColumns = m_nProductsPerRow 'Integer.Parse(ViewState.Item("Template1"))
            End If
        End If
        If (Not (IsNothing(dom))) Then
            If (Not (IsNothing(e.Item.FindControl("imgAddToCart")))) Then
                CType(e.Item.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value)
            End If
            If (Not (IsNothing(e.Item.FindControl("imgAddToSavedCart")))) Then
                CType(e.Item.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value)
            End If
            If (Not (IsNothing(e.Item.FindControl("imgEMailFriend")))) Then
                CType(e.Item.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = MyBase.ResolveUrl(dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value)
            End If
        End If
    End Sub
#End Region

#Region "Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged"
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        SearchTemplate1PageIndex = e.NewPageIndex
        MyBase.PageIndex = SearchTemplate1PageIndex
        Me.BindSearchData()
    End Sub
#End Region

#Region "Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand"
    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource
            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = SearchTemplate1PageIndex + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = SearchTemplate1PageIndex - 1
            End If

            SearchTemplate1PageIndex = DataGrid1.CurrentPageIndex
            Me.BindSearchData()
        End If
    End Sub
#End Region

#Region "Sub LoadDefaults(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)"
    Protected Sub LoadDefaults(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)
        Dim tempSalePrice As Decimal
        Dim tempRegPrice As Decimal
        Dim tempCustomPrice As Decimal
        'Verisign Recurring Billing
        Dim tempSubscriptionPrice As Decimal
        Dim tempRecurringPrice As Decimal
        'Verisign Recurring Billing
        Dim tempCartItem As BusinessRule.CCartItem = Nothing
        If Not (dlItem Is Nothing) Then
            tempCartItem = ar(nItemIndex)

            'Dim CustomPrice As String

            If Me.m_bDisplayLabels = False Then
                hidePriceLabels(dlItem)
            Else
                hidePriceLabels(dlItem)
            End If
            tempCustomPrice = GetCustomPrice(dlItem, ar, tempCartItem)
            tempRegPrice = tempCartItem.Price
            tempSalePrice = tempCartItem.SalePrice

            'Verisign Recurring Billing
            If tempCartItem.ProductType = ProductType.Subscription OrElse _
                tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                tempSubscriptionPrice = tempCartItem.Price
                tempRecurringPrice = tempCartItem.RecurringSubscriptionPrice 'should be recurring amount here
            End If
            If (IsNothing(dlItem.FindControl("lblSubscriptionPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblSubscriptionPriceDisplay"), Label).Text = PriceDisplay2(tempSubscriptionPrice, tempCartItem)
            End If
            If (IsNothing(dlItem.FindControl("lblRecurringPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblRecurringPriceDisplay"), Label).Text = PriceDisplay2(tempRecurringPrice) & " " & SystemBase.ProductHelperModule.GetPaymentPeriodName(tempCartItem.PaymentPeriod)
            End If
            'Verisign Recurring Billing

            If (IsNothing(dlItem.FindControl("lblRegularPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblRegularPriceDisplay"), Label).Text = PriceDisplay2(tempRegPrice, tempCartItem)
            End If
            If (IsNothing(dlItem.FindControl("lblSalePriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblSalePriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice, tempCartItem) & "</S>&nbsp;<BR>" & PriceDisplay2(tempSalePrice)
            End If
            If (IsNothing(dlItem.FindControl("lblCustomPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblCustomPriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice, tempCartItem) & "</S>&nbsp;<BR>" & PriceDisplay2(tempCustomPrice)
            End If
            If tempCartItem.IsOnSale And tempSalePrice < tempRegPrice Then
                If tempCustomPrice < tempSalePrice And tempCustomPrice <> 0 Then
                    If (IsNothing(dlItem.FindControl("trCustomPrice")) = False) Then
                        dlItem.FindControl("trCustomPrice").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("trSalePrice")) = False) Then
                        dlItem.FindControl("trSalePrice").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("trRegularPrice")) = False) Then
                        dlItem.FindControl("trRegularPrice").Visible = False
                    End If
                    'Verisign Recurring Billing
                    If tempCartItem.ProductType = ProductType.Subscription OrElse _
                        tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                        tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                        If (IsNothing(dlItem.FindControl("trSubscriptionPrice")) = False) Then
                            dlItem.FindControl("trSubscriptionPrice").Visible = False
                        End If
                        If (IsNothing(dlItem.FindControl("trRecurringPrice")) = False) Then
                            dlItem.FindControl("trRecurringPrice").Visible = True
                        End If
                    End If
                    'Verisign Recurring Billing
                Else
                    If (IsNothing(dlItem.FindControl("trCustomPrice")) = False) Then
                        dlItem.FindControl("trCustomPrice").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("trSalePrice")) = False) Then
                        dlItem.FindControl("trSalePrice").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("trRegularPrice")) = False) Then
                        dlItem.FindControl("trRegularPrice").Visible = False
                    End If
                    'Verisign Recurring Billing
                    If tempCartItem.ProductType = ProductType.Subscription OrElse _
                        tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                        tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                        If (IsNothing(dlItem.FindControl("trSubscriptionPrice")) = False) Then
                            dlItem.FindControl("trSubscriptionPrice").Visible = False
                        End If
                        If (IsNothing(dlItem.FindControl("trRecurringPrice")) = False) Then
                            dlItem.FindControl("trRecurringPrice").Visible = True
                        End If
                    End If
                    'Verisign Recurring Billing
                End If
            ElseIf tempCustomPrice < tempRegPrice And tempCustomPrice <> 0 Then
                If (IsNothing(dlItem.FindControl("trCustomPrice")) = False) Then
                    dlItem.FindControl("trCustomPrice").Visible = True
                End If
                If (IsNothing(dlItem.FindControl("trSalePrice")) = False) Then
                    dlItem.FindControl("trSalePrice").Visible = False
                End If
                If (IsNothing(dlItem.FindControl("trRegularPrice")) = False) Then
                    dlItem.FindControl("trRegularPrice").Visible = False
                End If
                'Verisign Recurring Billing
                If tempCartItem.ProductType = ProductType.Subscription OrElse _
                    tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                    tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                    If (IsNothing(dlItem.FindControl("trSubscriptionPrice")) = False) Then
                        dlItem.FindControl("trSubscriptionPrice").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("trRecurringPrice")) = False) Then
                        dlItem.FindControl("trRecurringPrice").Visible = True
                    End If
                End If
                'Verisign Recurring Billing
            Else
                If (IsNothing(dlItem.FindControl("trCustomPrice")) = False) Then
                    dlItem.FindControl("trCustomPrice").Visible = False
                End If
                If (IsNothing(dlItem.FindControl("trSalePrice")) = False) Then
                    dlItem.FindControl("trSalePrice").Visible = False
                End If
                If (IsNothing(dlItem.FindControl("trRegularPrice")) = False) Then
                    dlItem.FindControl("trRegularPrice").Visible = True
                End If
                'Verisign Recurring Billing
                If tempCartItem.ProductType = ProductType.Subscription OrElse _
                    tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                    tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                    dlItem.FindControl("trRegularPrice").Visible = False
                    If (IsNothing(dlItem.FindControl("trSubscriptionPrice")) = False) Then
                        dlItem.FindControl("trSubscriptionPrice").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("trRecurringPrice")) = False) Then
                        dlItem.FindControl("trRecurringPrice").Visible = True
                    End If
                End If
                'Verisign Recurring Billing
            End If
        End If
        If (IsNothing(ar(dlItem.ItemIndex)) = False) Then
            HandleInventoryDisplay(nItemIndex, dlItem, ar)

            If ((CType(ar(dlItem.ItemIndex), CProduct).Vendor = "No Vendor") Or CType(ar(dlItem.ItemIndex), CProduct).Vendor = "") And m_bDisplayVendor = True Then
                If (IsNothing(dlItem.FindControl("VendorCell")) = False) Then
                    dlItem.FindControl("VendorCell").Visible = False
                End If
            End If

            If ((CType(ar(dlItem.ItemIndex), CProduct).Manufacturer = "No Manufacturer") Or CType(ar(dlItem.ItemIndex), CProduct).Manufacturer = "") And m_bDisplayManufacturer = True Then
                If (IsNothing(dlItem.FindControl("ManufacturerCell")) = False) Then
                    dlItem.FindControl("ManufacturerCell").Visible = False
                End If
            End If
        End If

        If (m_bDisplayImage = False) Then
            If (IsNothing(dlItem.FindControl("ImageCell")) = False) Then
                dlItem.FindControl("ImageCell").Visible = False
            End If
            If (IsNothing(dlItem.FindControl("ImageSpacer")) = False) Then
                dlItem.FindControl("ImageSpacer").Visible = False
            End If
        Else
            If (IsNothing(tempCartItem) = False) Then
                If (m_bLinkimage And tempCartItem.DetailLink <> "") Then
                    If (IsNothing(dlItem.FindControl("ImageCell")) = False) Then
                        dlItem.FindControl("ImageCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkImage")) = False) Then
                        dlItem.FindControl("lnkImage").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("SmallImage")) = False) Then
                        dlItem.FindControl("SmallImage").Visible = False
                    End If
                Else
                    If (IsNothing(dlItem.FindControl("ImageCell")) = False) Then
                        dlItem.FindControl("ImageCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkImage")) = False) Then
                        dlItem.FindControl("lnkImage").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("SmallImage")) = False) Then
                        dlItem.FindControl("SmallImage").Visible = True
                    End If
                End If
            End If
        End If

        If (m_bDisplayProductCode = False) Then
            If (IsNothing(dlItem.FindControl("ProductCodeCell")) = False) Then
                dlItem.FindControl("ProductCodeCell").Visible = False
            End If
        Else
            If (IsNothing(tempCartItem) = False) Then
                If (m_blinkProductcode And tempCartItem.DetailLink <> "") Then
                    If (IsNothing(dlItem.FindControl("ProductCodeCell")) = False) Then
                        dlItem.FindControl("ProductCodeCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkProductCode")) = False) Then
                        dlItem.FindControl("lnkProductCode").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lblProdCode")) = False) Then
                        dlItem.FindControl("lblProdCode").Visible = False
                    End If
                Else
                    If (IsNothing(dlItem.FindControl("ProductCodeCell")) = False) Then
                        dlItem.FindControl("ProductCodeCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkProductCode")) = False) Then
                        dlItem.FindControl("lnkProductCode").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("lblProdCode")) = False) Then
                        dlItem.FindControl("lblProdCode").Visible = True
                    End If
                End If
            End If
        End If

        If (m_bDisplayProductName = False) Then
            If (IsNothing(dlItem.FindControl("ProductNameCell")) = False) Then
                dlItem.FindControl("ProductNameCell").Visible = False
            End If
        Else
            If (IsNothing(tempCartItem) = False) Then
                If (m_blinkproductname And tempCartItem.DetailLink <> "") Then
                    If (IsNothing(dlItem.FindControl("ProductNameCell")) = False) Then
                        dlItem.FindControl("ProductNameCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkProductName")) = False) Then
                        dlItem.FindControl("lnkProductName").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lblProdName")) = False) Then
                        dlItem.FindControl("lblProdName").Visible = False
                    End If
                Else
                    If (IsNothing(dlItem.FindControl("ProductNameCell")) = False) Then
                        dlItem.FindControl("ProductNameCell").Visible = True
                    End If
                    If (IsNothing(dlItem.FindControl("lnkProductName")) = False) Then
                        dlItem.FindControl("lnkProductName").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("lblProdName")) = False) Then
                        dlItem.FindControl("lblProdName").Visible = True
                    End If
                End If
            End If
        End If

        If (m_bDisplayPriceSalePrice = False) Then
            If (IsNothing(dlItem.FindControl("trRegularPrice")) = False) Then
                dlItem.FindControl("trRegularPrice").Visible = False
            End If
            If (IsNothing(dlItem.FindControl("trSalePrice")) = False) Then
                dlItem.FindControl("trSalePrice").Visible = False
            End If
            If (IsNothing(dlItem.FindControl("trCustomPrice")) = False) Then
                dlItem.FindControl("trCustomPrice").Visible = False
            End If
            'Verisign Recurring Billing
            If tempCartItem.ProductType = ProductType.Subscription OrElse _
                tempCartItem.ProductType = ProductType.BundleSubscription OrElse _
                tempCartItem.ProductType = ProductType.CustomizedSubscription Then
                If (IsNothing(dlItem.FindControl("trSubscriptionPrice")) = False) Then
                    dlItem.FindControl("trSubscriptionPrice").Visible = False
                End If
                If (IsNothing(dlItem.FindControl("trRecurringPrice")) = False) Then
                    dlItem.FindControl("trRecurringPrice").Visible = False
                End If
            End If
            'Verisign Recurring Billing
        End If

        If (m_bdisplayvendor = False) Then
            If (IsNothing(dlItem.FindControl("VendorCell")) = False) Then
                dlItem.FindControl("VendorCell").Visible = False
            End If
        End If

        If (m_bdisplaymanufacturer = False) Then
            If (IsNothing(dlItem.FindControl("ManufacturerCell")) = False) Then
                dlItem.FindControl("ManufacturerCell").Visible = False
            End If
        End If

        If (m_bdisplayaddtocart = False) Then
            If (IsNothing(dlItem.FindControl("BuyNowCell")) = False) Then
                dlItem.FindControl("BuyNowCell").Visible = False
            End If
        End If

        If (m_bDisplayShortDescription = False) Then
            If (IsNothing(dlItem.FindControl("ShortDescriptionCell")) = False) Then
                dlItem.FindControl("ShortDescriptionCell").Visible = False
            End If
        End If

        If (m_bDisplayStockInfo = False) Then
            If (IsNothing(dlItem.FindControl("DisplayStockInfo")) = False) Then
                dlItem.FindControl("DisplayStockInfo").Visible = False
            End If
            If (IsNothing(dlItem.FindControl("trStockStatus")) = False) Then
                dlItem.FindControl("trStockStatus").Visible = False
            End If
        End If

        If (m_bDisplaySavedCartWishList = False) Then
            If (IsNothing(dlItem.FindControl("SavedCartCell")) = False) Then
                dlItem.FindControl("SavedCartCell").Visible = False
            End If
        End If

        If (m_bDisplayEMailFriend = False) Then
            If (IsNothing(dlItem.FindControl("EMailFriendCell")) = False) Then
                dlItem.FindControl("EMailFriendCell").Visible = False
            End If
        End If

        If (m_bDisplayMoreInfo = False) Then
            If (IsNothing(dlItem.FindControl("MoreInfoCell")) = False) Then
                dlItem.FindControl("MoreInfoCell").Visible = False
            End If
        Else
            If (IsNothing(tempCartItem) = False) Then
                If (tempCartItem.DetailLink <> "") Then
                    If (IsNothing(dlItem.FindControl("MoreInfoCell")) = False) Then
                        dlItem.FindControl("MoreInfoCell").Visible = True
                    End If
                Else
                    If (IsNothing(dlItem.FindControl("MoreInfoCell")) = False) Then
                        dlItem.FindControl("MoreInfoCell").Visible = False
                    End If
                End If
            End If
        End If

    End Sub
#End Region

#Region "Private Sub GetCustomPricing(ByVal objGridItem As DataGridItem)"

    Private Function GetCustomPrice(ByVal Id As Long) As Decimal
        Dim custPrice As Decimal
        Dim tempCartItem As New BusinessRule.CCartItem(Me.m_objXMLAccess.GetProduct(Id), 1, , Me.m_objCustomer.CustomerGroup)
        custPrice = tempCartItem.CustomerSpecificPrice
        If tempCartItem.IsOnSale Then
            If custPrice < tempCartItem.SalePrice Then
                Return custPrice
            Else
                Return PriceDisplay2(tempCartItem.SalePrice)
            End If
        ElseIf custPrice < tempCartItem.Price Then
            Return custPrice
        Else
            Return 0.0
        End If
    End Function

    Private Function GetCustomPrice(ByVal dlitem As DataListItem, ByVal ar As ArrayList, ByVal oCartItem As CCartItem) As Decimal
        'Dim tempPrice As Decimal
        Dim custPrice As Decimal
        'Dim savings As Decimal

        custPrice = oCartItem.CustomerSpecificPrice
        If oCartItem.IsOnSale Then
            If custPrice < oCartItem.SalePrice Then
                Return custPrice
            Else
                Return oCartItem.SalePrice
            End If
        ElseIf custPrice < oCartItem.Price Then
            Return custPrice
        End If
        Return 0.0
    End Function

    Public Sub MakePriceLabelsVisible(ByVal dlItem As DataListItem)
        If (IsNothing(dlItem.FindControl("lblRegularPrice")) = False) Then
            CType(dlItem.FindControl("lblRegularPrice"), Label).Visible = True
        End If
        If (IsNothing(dlItem.FindControl("lblSalePrice")) = False) Then
            CType(dlItem.FindControl("lblSalePrice"), Label).Visible = True
        End If
        If (IsNothing(dlItem.FindControl("lblCustomPrice")) = False) Then
            CType(dlItem.FindControl("lblCustomPrice"), Label).Visible = True
        End If
    End Sub

    Public Sub hidePriceLabels(ByVal dlItem As DataListItem)
        If (IsNothing(dlItem.FindControl("lblRegularPrice")) = False) Then
            CType(dlItem.FindControl("lblRegularPrice"), Label).Visible = False
        End If
        If (IsNothing(dlItem.FindControl("lblSalePrice")) = False) Then
            CType(dlItem.FindControl("lblSalePrice"), Label).Visible = False
        End If
        If (IsNothing(dlItem.FindControl("lblCustomPrice")) = False) Then
            CType(dlItem.FindControl("lblCustomPrice"), Label).Visible = False
        End If
        'Verisign Recurring Billing
        If IsNothing(dlItem.FindControl("lblSubscriptionPrice")) = False Then
            CType(dlItem.FindControl("lblSubscriptionPrice"), Label).Visible = False
        End If
        If IsNothing(dlItem.FindControl("lblRecurringPrice")) = False Then
            CType(dlItem.FindControl("lblRecurringPrice"), Label).Visible = False
        End If
        'Verisign Recurring Billing
    End Sub
#End Region

#Region "Private Sub HandleInventoryDisplay(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)"

    Private Sub HandleInventoryDisplay(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)
        Dim objItem As CCategoryStorage = CType(ar(dlItem.ItemIndex), CProduct)
        Dim objInventoryLink As LinkButton = Nothing

        objInventoryLink = CType(dlItem.FindControl("StockInfo"), LinkButton)
        'Tee 10/15/2007 bug 312 fix
        objInventoryLink.Attributes.Add("OnClick", "return DisplayStatus('" & objInventoryLink.ClientID & "');")
        'end Tee
        Try
            If objItem.Inventory.InventoryTracked Then
                If objItem.Inventory.ShowStatus Then
                    m_Stock_Depleted = objItem.Inventory.StockIsDepleted
                    If Me.m_lngCurrentID = objItem.ProductID Then
                        If (IsNothing(dlItem.FindControl("trStockStatus")) = False) Then
                            dlItem.FindControl("trStockStatus").Visible = True
                        End If
                        InvenStatVisible = True
                    Else
                        If (IsNothing(dlItem.FindControl("trStockStatus")) = False) Then
                            dlItem.FindControl("trStockStatus").Visible = False
                        End If
                    End If
                    If (IsNothing(objInventoryLink) = False) Then
                        If (m_stock_depleted) Then
                            objInventoryLink.Text = "Out of Stock!"
                        Else
                            objInventoryLink.Text = "In Stock!"
                        End If
                    End If
                Else
                    If (IsNothing(dlItem.FindControl("trStockStatus")) = False) Then
                        dlItem.FindControl("trStockStatus").Visible = False
                    End If
                    If (IsNothing(dlItem.FindControl("DisplayStockInfo")) = False) Then
                        dlItem.FindControl("DisplayStockInfo").Visible = False
                    End If
                End If
                m_InventoryLinkVisible = True
                objInventoryLink.Visible = True
            Else
                If (IsNothing(dlItem.FindControl("trStockStatus")) = False) Then
                    dlItem.FindControl("trStockStatus").Visible = False
                End If
                If (IsNothing(dlItem.FindControl("DisplayStockInfo")) = False) Then
                    dlItem.FindControl("DisplayStockInfo").Visible = False
                End If
            End If

        Catch objErr As System.Exception
            If (IsNothing(objInventoryLink) = False) Then
                objInventoryLink.Visible = False
            End If
            m_InventoryLinkVisible = True
        End Try
    End Sub
#End Region


#Region "SP8"
    '2828
    Private mPageIndex As Integer
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me.SearchTemplate1PageIndex = savedState(1)
        Me.objStorage = savedState(2)
    End Sub

    Protected Overrides Function SaveViewState() As Object
        Dim myState(3) As Object
        myState(0) = MyBase.SaveViewState
        myState(1) = Me.SearchTemplate1PageIndex
        myState(2) = Me.objStorage
        Return myState
    End Function

    Public Property SearchTemplate1PageIndex() As Integer
        Get
            Return mPageIndex
        End Get
        Set(ByVal Value As Integer)
            mPageIndex = Value
        End Set
    End Property

#End Region



End Class
