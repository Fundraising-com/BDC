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

Public MustInherit Class SearchTemplate1
    Inherits CSearchResultBase
    Protected WithEvents lblKeyword As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategoryName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label
    Protected WithEvents lblProducts As System.Web.UI.WebControls.Label
    Protected WithEvents ResultInfo As System.Web.UI.WebControls.Panel
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
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

        Dim objTemplate As DataGridColumn
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
            isLive = True
        End If
        If Me.Visible = False Then Exit Sub
        LoadSettings()
        m_bPageLoad = True
        If (IsPostBack = False) Then
            If (IsNothing(Request.QueryString("Index")) = False) Then
                Session("PageIndex") = Request.QueryString("Index")
            Else
                If (IsNothing(Session("Popup")) = True) Then
                    Session("PageIndex") = 0
                Else
                    Session("Popup") = Nothing
                End If
            End If
        End If
        'ViewState.Item("Template1") = m_nProductsPerRow.ToString()
        mRecordsToGet = m_nProductsPerRow * Me.m_nRows
        lngCatId = Request.QueryString("CategoryID")
        qsobjexist = HaQsParams()
        If IsNumeric(lngCatId) And lngCatId > 0 And qsobjexist = False Then
            Me.CategorySearch()
        Else
            RegularSearch()
        End If
        SetDataGrid()
        SetDisplayInfo()
        BindInitial()
        CheckOanda()
        SetResultsDisplay()
        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        End If
        LabelText(ResultInfo)
    End Sub
#End Region

#Region "Private Sub CategorySearch()"

    Private Sub CategorySearch()
        
        ResultInfo.Visible = False
        objStorage = SetSearch()
        'Sp1 Change
        '   m_objSearchEngine = New CSearchEngine(dom.Item("SiteProducts").Item("ProductList"), dom.Item("SiteProducts").Item("CategoryTree"), objStorage.CategoryID)
        m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)

        m_arResults = m_objSearchEngine.StandardSearch(objStorage, True, Session("PageIndex"))
    End Sub

#End Region

#Region "Private Sub RegularSearch()"

    Private Sub RegularSearch()
        m_strPrevCategory = ""
        objStorage = SetSearch()

        If objStorage.CategoryID <> -1 Then
            If isLive = False Then
                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                m_arResults = m_objSearchEngine.StandardSearch(objStorage)
            Else

                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                m_arResults = m_objSearchEngine.StandardSearch(objStorage, False, Session("PageIndex"))
            End If
        Else
            If isLive = False Then

                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                'Dim sTime As Date = Now
                m_arResults = m_objSearchEngine.StandardSearch(objStorage, False)
                'Dim eTime As Date = Now
                'sElasped = " IN:" & eTime.Subtract(sTime).Seconds & " Seconds  and "
                'sElasped = sElasped & eTime.Subtract(sTime).Milliseconds & " MiliSeconds"
            Else
                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                'Dim sTime As Date = Now
                m_arResults = m_objSearchEngine.StandardSearch(objStorage, False, Session("PageIndex"))
                'm_arResults = m_objSearchEngine.StandardSearch(objStorage, False)
                'Dim eTime As Date = Now
                'sElasped = " IN:" & eTime.Subtract(sTime).Seconds & " Seconds  and "
                'sElasped = sElasped & eTime.Subtract(sTime).Milliseconds & " MiliSeconds"

            End If

        End If
    End Sub


#End Region

#Region "Private Sub SetDataGrid()"

    Private Sub SetDataGrid()
        If isLive = False Then
            Dim i As Long, j As Long
            Dim nCount As Long = m_nProductsPerRow
            Dim arRow As ArrayList
            For j = 0 To m_arResults.Count - 1 Step nCount
                arRow = New ArrayList()
                If (j + nCount > m_arResults.Count) Then
                    nCount = (m_arResults.Count - j)
                End If
                For i = 0 To nCount - 1
                    arRow.Add(m_arResults(j + i))
                Next
                m_arResultItems.Add(arRow)
            Next

            DataGrid1.PageSize = m_nRows
        Else
            DataGrid1.AllowCustomPaging = True
            DataGrid1.PageSize = 1
            Dim dblVirtItemCount As Double = (m_objSearchEngine.ResultsCount / mRecordsToGet)
            If (Round(dblVirtItemCount) < dblVirtItemCount) Then
                dblVirtItemCount = Round(dblVirtItemCount) + 1
            End If
            DataGrid1.VirtualItemCount = dblVirtItemCount
            m_arResultItems = m_arResults
        End If
    End Sub

#End Region

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

#Region "Private Sub BindInitial()"

    Private Sub BindInitial()

        If isLive Then
            DataGrid1.DataSource = arFake
        Else
            DataGrid1.DataSource = m_arResultItems
        End If

        DataGrid1.CurrentPageIndex = Session("PageIndex")
        DataGrid1.DataBind()
    End Sub

#End Region

#Region "Private Sub CheckOanda()"

    Private Sub CheckOanda()
        If (IsNothing(Session("OandaChange")) = False) Then
            Session("OandaChange") = Nothing
            Dim iPage As Long = DataGrid1.CurrentPageIndex
            DataGrid1.CurrentPageIndex = iPage
            If isLive = False Then
                DataGrid1.DataSource = m_arResultItems
                DataGrid1.DataBind()
                DataGrid1.CurrentPageIndex = Session("PageIndex")
            Else
                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                m_arResults = m_objSearchEngine.StandardSearch(objStorage, , DataGrid1.CurrentPageIndex)
                DataGrid1.AllowCustomPaging = True
                DataGrid1.PageSize = m_nRows
                Dim dblVirtItemCount As Double = (m_objSearchEngine.ResultsCount / mRecordsToGet)
                If (Round(dblVirtItemCount) < dblVirtItemCount) Then
                    dblVirtItemCount = Round(dblVirtItemCount) + 1
                End If
                DataGrid1.VirtualItemCount = dblVirtItemCount
            End If

        End If

    End Sub

#End Region

#Region "Private Sub SetResultsDisplay()"

    Private Sub SetResultsDisplay()
        If isLive = False Then

            lblCount.Text = " " & m_objSearchEngine.ResultsCount
            If (m_arResults.Count = 1) Then
                lblProducts.Text = "Product" '& " " & sElasped
            Else
                lblProducts.Text = "Products" '& " " & sElasped
            End If
        Else
            If Not IsPostBack Then
                lblCount.Text = " " & m_objSearchEngine.ResultsCount
                If (m_arResults.Count = 1) Then
                    lblProducts.Text = "Product" '& " " & sElasped
                Else
                    lblProducts.Text = "Products" '& " " & sElasped
                End If

            End If
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
                    CType(dlItem.FindControl("CAttributeControl1"), CAttributeControl).Data_Bind(oResult)
                    If (dlItem.FindControl("CAttributeControl1").Visible = True And m_bdisplayaddtocart = False) Then
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
                    CType(dlItem.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
                End If
                If (IsNothing(dlItem.FindControl("imgAddToSavedCart")) = False) Then
                    CType(dlItem.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
                End If
                If (IsNothing(dlItem.FindControl("imgEMailFriend")) = False) Then
                    CType(dlItem.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value()
                End If
            Next
        End If
    End Sub
#End Region

#Region "Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated"
    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton()

            If (Session("PageIndex") > 0) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (Session("PageIndex") < DataGrid1.PageCount - 1) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton()
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        ElseIf (e.Item.ItemType = ListItemType.AlternatingItem Or _
            e.Item.ItemType = ListItemType.Item) Then
            If (m_bPageLoad = True) Then
                CType(e.Item.FindControl("DataList1"), DataList).RepeatColumns = m_nProductsPerRow 'Integer.Parse(ViewState.Item("Template1"))
            End If
        End If
        If (Not (IsNothing(dom))) Then
            If (Not (IsNothing(e.Item.FindControl("imgAddToCart")))) Then
                CType(e.Item.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
            End If
            If (Not (IsNothing(e.Item.FindControl("imgAddToSavedCart")))) Then
                CType(e.Item.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
            End If
            If (Not (IsNothing(e.Item.FindControl("imgEMailFriend")))) Then
                CType(e.Item.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
            End If
        End If
    End Sub
#End Region

#Region "Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged"
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged

        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
            m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)

            m_arResults = m_objSearchEngine.StandardSearch(objStorage, False, e.NewPageIndex)
            DataGrid1.AllowCustomPaging = True
            DataGrid1.PageSize = 1

            Dim dblVirtItemCount As Double = (m_objSearchEngine.ResultsCount / mRecordsToGet)
            If (Round(dblVirtItemCount) < dblVirtItemCount) Then
                dblVirtItemCount = Round(dblVirtItemCount) + 1
            End If


            DataGrid1.VirtualItemCount = dblVirtItemCount
            m_arResultItems = m_arResults
            lblCount.Text = " " & m_objSearchEngine.ResultsCount
            If (m_arResults.Count = 1) Then
                lblProducts.Text = "Product"
            Else
                lblProducts.Text = "Products"
            End If
        End If
        If isLive Then
            DataGrid1.DataSource = arFake
        Else
            DataGrid1.DataSource = m_arResultItems
        End If
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        Session("PageIndex") = e.NewPageIndex
        DataGrid1.DataBind()
    End Sub
#End Region

#Region "Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand"
    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource

            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("PageIndex") + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("PageIndex") - 1
            End If
            Session("PageIndex") = DataGrid1.CurrentPageIndex

            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, mRecordsToGet)
                m_arResults = m_objSearchEngine.StandardSearch(objStorage, False, DataGrid1.CurrentPageIndex)
                DataGrid1.AllowCustomPaging = True
                DataGrid1.PageSize = 1
                Dim dblVirtItemCount As Double = (m_objSearchEngine.ResultsCount / mRecordsToGet)
                If (Round(dblVirtItemCount) < dblVirtItemCount) Then
                    dblVirtItemCount = Round(dblVirtItemCount) + 1
                End If
                DataGrid1.VirtualItemCount = dblVirtItemCount
                m_arResultItems = m_arResults
                lblCount.Text = " " & m_objSearchEngine.ResultsCount
                If (m_arResults.Count = 1) Then
                    lblProducts.Text = "Product"
                Else
                    lblProducts.Text = "Products"
                End If
            End If
            If isLive Then
                DataGrid1.DataSource = arFake
            Else
                DataGrid1.DataSource = m_arResultItems
            End If
            DataGrid1.DataBind()
        End If
    End Sub
#End Region

#Region "Sub LoadDefaults(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)"
    Protected Sub LoadDefaults(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)
        Dim tempSalePrice As Decimal
        Dim tempRegPrice As Decimal
        Dim tempCustomPrice As Decimal
        Dim tempCartItem As BusinessRule.CCartItem
        If Not (dlItem Is Nothing) Then
            tempCartItem = ar(nItemIndex)

            Dim CustomPrice As String

            If Me.m_bDisplayLabels = False Then
                hidePriceLabels(dlItem)
            Else
                hidePriceLabels(dlItem)
            End If
            tempCustomPrice = GetCustomPrice(dlItem, ar, tempCartItem)
            tempRegPrice = tempCartItem.Price
            tempSalePrice = tempCartItem.SalePrice
            If (IsNothing(dlItem.FindControl("lblRegularPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblRegularPriceDisplay"), Label).Text = PriceDisplay2(tempRegPrice)
            End If
            If (IsNothing(dlItem.FindControl("lblSalePriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblSalePriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempSalePrice)
            End If
            If (IsNothing(dlItem.FindControl("lblCustomPriceDisplay")) = False) Then
                CType(dlItem.FindControl("lblCustomPriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempCustomPrice)
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
        Dim tempPrice As Decimal
        Dim custPrice As Decimal
        Dim savings As Decimal

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
    End Sub
#End Region

#Region "Private Sub HandleInventoryDisplay(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)"

    Private Sub HandleInventoryDisplay(ByVal nItemIndex As Long, ByVal dlItem As DataListItem, ByVal ar As ArrayList)
        Dim objItem As CCategoryStorage = CType(ar(dlItem.ItemIndex), CProduct)
        Dim objInventoryLink As LinkButton = Nothing

        objInventoryLink = CType(dlItem.FindControl("StockInfo"), LinkButton)
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

End Class
