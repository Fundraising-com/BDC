Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class SearchTemplate2
    Inherits CSearchResultBase

    Protected WithEvents lblKeyword As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategoryName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label
    Protected WithEvents lblProducts As System.Web.UI.WebControls.Label
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ResultInfo As System.Web.UI.WebControls.Panel

    Private dlCat As DropDownList
    Private oResult As CCategoryStorage
    Private m_arResults As ArrayList


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

#Region "Private Sub Page_Load()"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim con As DataGridItem
        If Me.Visible = False Then Exit Sub
        LoadSettings()

        DataGrid1.PageSize = m_nRows
        qsobjexist = HaQsParams()
        If (Not IsPostBack) Then
            If (IsNothing(Session("Popup")) = True) Then
                Session("PageIndex") = 0
            Else
                Session("Popup") = Nothing
            End If
        End If
        NormalSearch(SetSearch)
        SetSearchLabels()
        SetResultsDisplay()
        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        End If
        LabelText(ResultInfo)
        For Each con In DataGrid1.Items
            If (IsNothing(con.FindControl("imgAddToCart")) = False) Then
                CType(con.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
            End If
            If (IsNothing(con.FindControl("imgAddToSavedCart")) = False) Then
                CType(con.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
            End If
            If (IsNothing(con.FindControl("imgEMailFriend")) = False) Then
                CType(con.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
            End If
        Next
    End Sub
#End Region
#Region "Private Sub SetResultsDisplay()"

    Private Sub SetResultsDisplay()
        lblCount.Text = " " & m_objSearchEngine.ResultsCount
        If (m_objSearchEngine.ResultsCount = 1) Then
            lblProducts.Text = "Product" '& " " & sElasped
        Else
            lblProducts.Text = "Products" '& " " & sElasped
        End If
    End Sub
#End Region

#Region "Private Sub SetSearchLabels()"

    Private Sub SetSearchLabels()
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

#Region "Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated"
    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Dim objInventoryLink As LinkButton
        Dim CAttributeControl1 As CAttributeControl
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
        ElseIf (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            If (IsNothing(dom) = False) Then
                oResult = e.Item.DataItem
                If (IsNothing(e.Item.FindControl("CAttributeControl1")) = False) Then
                    CType(e.Item.FindControl("CAttributeControl1"), CAttributeControl).Data_Bind(oResult)
                    If (e.Item.FindControl("CAttributeControl1").Visible = False) Then
                        If (IsNothing(e.Item.FindControl("AttributeRow")) = False) Then
                            e.Item.FindControl("AttributeRow").Visible = False
                        End If
                    End If
                    If (e.Item.FindControl("CAttributeControl1").Visible = True And m_bdisplayaddtocart = False) Then
                        If (IsNothing(e.Item.FindControl("AttributeRow")) = False) Then
                            e.Item.FindControl("AttributeRow").Visible = m_bDisplayAddToCart
                        End If
                    End If
                End If
                LoadDisplay(DataGrid1, e.Item)
            End If
        End If
        If (IsNothing(dom) = False) Then
            If (IsNothing(e.Item.FindControl("imgAddToCart")) = False) Then
                CType(e.Item.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
            End If
            If (Not (IsNothing(e.Item.FindControl("imgAddToSavedCart")))) Then
                CType(e.Item.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
            End If
            If (Not (IsNothing(e.Item.FindControl("imgEMailFriend")))) Then
                CType(e.Item.FindControl("imgEMailFriend"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
            End If
            LabelText(e.Item)
        End If
    End Sub
#End Region

#Region "Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged"

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        Dim m_objSearchEngine As CSearchEngine
        objStorage = Session("Search")
        'SP 1 Change
        ' m_objSearchEngine = New CSearchEngine(dom.Item("SiteProducts").Item("ProductList"), dom.Item("SiteProducts").Item("CategoryTree"), objStorage.CategoryID)
        m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
        m_strPrevCategory = ""
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
            DataGrid1.AllowCustomPaging = True
        Else
            DataGrid1.AllowCustomPaging = False
        End If
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        Session("PageIndex") = e.NewPageIndex
        'SP 1 Change
        If objStorage.CategoryID <> -1 And QsObjExist = False Then
            _CatsAreGrouped = True
        End If
        m_arResults = m_objSearchEngine.StandardSearch(objStorage, _CatsAreGrouped, e.NewPageIndex)
        DataGrid1.DataSource = m_arResults
        DataGrid1.VirtualItemCount = m_objSearchEngine.ResultsCount
        DataGrid1.DataBind()
        SetResultsDisplay()
    End Sub

#End Region

#Region "Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand"
    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource
            Dim m_objSearchEngine As CSearchEngine
            objStorage = Session("Search")
            m_strPrevCategory = ""
            m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
                DataGrid1.AllowCustomPaging = True
            Else
                DataGrid1.AllowCustomPaging = False
            End If
            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("PageIndex") + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("PageIndex") - 1
            End If
            Session("PageIndex") = DataGrid1.CurrentPageIndex
            If objStorage.CategoryID <> -1 And QsObjExist = False Then
                _CatsAreGrouped = True
            End If
            
            m_arResults = m_objSearchEngine.StandardSearch(objStorage, _CatsAreGrouped, DataGrid1.CurrentPageIndex)
            DataGrid1.DataSource = m_arResults

            DataGrid1.VirtualItemCount = m_objSearchEngine.ResultsCount
            DataGrid1.DataBind()
            SetResultsDisplay()
        End If
    End Sub

#End Region

#Region "Page_PreRender"
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Dim con As DataGridItem
        For Each con In DataGrid1.Items
            Dim oText As TextBox
            oText = con.FindControl("txtQuantity")
            If Not IsNothing(oText) Then
                Dim oImage As System.Web.UI.WebControls.Image
                Dim sKey As String = oText.ClientID
                sKey = Replace(sKey, "_", ":")
                sKey = Replace(sKey, "::", ":_")
                oImage = CType(con.FindControl("imgAddToCart"), System.Web.UI.WebControls.Image)
                If Not IsNothing(oImage) Then
                    oImage.Attributes.Add("onClick", "return SearchValidation('" & sKey & "');")
                End If
                oImage = CType(con.FindControl("imgAddToSavedCart"), System.Web.UI.WebControls.Image)
                If (Not IsNothing(oImage)) Then
                    oImage.Attributes.Add("onClick", "return SearchValidation('" & sKey & "');")
                End If
            End If
        Next
    End Sub
#End Region

#Region "Private Sub LoadDisplay()"
    Private Sub LoadDisplay(ByVal dg As DataGrid, ByVal dgItem As DataGridItem)
        Dim objItem As CCategoryStorage = dgItem.DataItem
        Dim objDropList As DropDownList
        Dim objDataGridItem As DataGridItem
        Dim ar As ArrayList

        If (IsNothing(objItem) = False) Then
            If (objItem.CategoryName = m_strPrevCategory And dgItem.ItemIndex > 0) Then
                If (IsNothing(dgItem.FindControl("HeaderRow")) = False) Then
                    dgItem.FindControl("HeaderRow").Visible = False
                End If

                If (dgItem.ItemIndex > 0) Then
                    objDataGridItem = dg.Items(dgItem.ItemIndex - 1)
                    If (IsNothing(objDataGridItem.FindControl("FooterRow")) = False) Then
                        objDataGridItem.FindControl("FooterRow").Visible = False
                    End If
                    If (IsNothing(objDataGridItem.FindControl("SpacerRow")) = False) Then
                        objDataGridItem.FindControl("SpacerRow").Visible = False
                    End If
                End If
            Else
                m_strPrevCategory = objItem.CategoryName
            End If

            DisplaySeperators(dg, dgItem)

            Me.m_bDisplayVendorTemp = Me.m_bDisplayVendor
            If ((CType(dgItem.DataItem, CProduct).Vendor = "No Vendor") Or (CType(dgItem.DataItem, CProduct).Vendor = "")) And m_bDisplayVendor = True Then
                m_bDisplayVendorTemp = False
            End If

            If Not (dgItem.FindControl("CAttributeControl1") Is Nothing) Then
                CType(dgItem.FindControl("CAttributeControl1"), CAttributeControl).DisplayType = m_AttributeDisplay
            End If

            If (IsNothing(dgItem.FindControl("HeaderRow")) = False) Then
                If (dgItem.FindControl("HeaderRow").Visible = True) Then
                    objDropList = CType(dgItem.FindControl("NextLevel"), DropDownList)
                    If (IsNothing(objDropList) = False) Then
                        If (m_objSearchEngine.NextLevel(objItem.CategoryID).Count > 1) Then
                            objDropList.DataSource = m_objSearchEngine.NextLevel(objItem.CategoryID)
                            objDropList.DataBind()
                        Else
                            objDropList.Visible = False
                        End If
                    End If
                End If
            End If
            HandleInventoryDisplay(dg, dgItem)
        End If
        DisplayFromDefaults(dgItem)

    End Sub
#End Region

#Region "Sub DisplaySeperators(ByVal dg As DataGrid, ByVal dgItem As DataGridItem)"
    Protected Sub DisplaySeperators(ByVal dg As DataGrid, ByVal dgItem As DataGridItem)
        If (CType(dg.DataSource, ArrayList).Count > dg.PageSize) Then
            If ((dg.PageSize * dg.CurrentPageIndex) + (dgItem.ItemIndex + 1) = CType(dg.DataSource, ArrayList).Count) Then
                ' End of pages
                If (IsNothing(dgItem.FindControl("FooterRow")) = False) Then
                    dgItem.FindControl("FooterRow").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("SpacerRow")) = False) Then
                    dgItem.FindControl("SpacerRow").Visible = False
                End If
            ElseIf (dgItem.ItemIndex + 1 = dg.PageSize) Then
                If (IsNothing(dgItem.FindControl("FooterRow")) = False) Then
                    dgItem.FindControl("FooterRow").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("SpacerRow")) = False) Then
                    dgItem.FindControl("SpacerRow").Visible = False
                End If
            End If
        Else
            If (dgItem.ItemIndex = CType(dg.DataSource, ArrayList).Count - 1) Then
                If (IsNothing(dgItem.FindControl("FooterRow")) = False) Then
                    dgItem.FindControl("FooterRow").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("SpacerRow")) = False) Then
                    dgItem.FindControl("SpacerRow").Visible = False
                End If
            End If
        End If
    End Sub
#End Region

#Region " Private Sub HandleInventoryDisplay(ByVal dg As DataGrid, ByVal dgItem As DataGridItem)"


    Private Sub HandleInventoryDisplay(ByVal dg As DataGrid, ByVal dgItem As DataGridItem)
        Dim objItem As CCategoryStorage = dgItem.DataItem
        Dim objTableRow As HtmlTableRow

        Try
            If objItem.Inventory.InventoryTracked Then
                If objItem.Inventory.ShowStatus Then
                    m_Stock_Depleted = objItem.Inventory.StockIsDepleted
                    If Me.m_lngCurrentID = objItem.ProductID Then
                        If (IsNothing(dgItem.FindControl("trStockStatus")) = False) Then
                            dgItem.FindControl("trStockStatus").Visible = True
                        End If
                        InvenStatVisible = True
                    Else
                        If (IsNothing(dgItem.FindControl("trStockStatus")) = False) Then
                            dgItem.FindControl("trStockStatus").Visible = False
                        End If
                    End If
                Else
                    If (IsNothing(dgItem.FindControl("trStockStatus")) = False) Then
                        dgItem.FindControl("trStockStatus").Visible = False
                    End If
                    If (IsNothing(dgItem.FindControl("DisplayStockInfo")) = False) Then
                        dgItem.FindControl("DisplayStockInfo").Visible = False
                    End If
                End If
                m_InventoryLinkVisible = True
                If (IsNothing(dgItem.FindControl("StockInfo")) = False) Then
                    dgItem.FindControl("StockInfo").Visible = True
                End If
            Else
                If (IsNothing(dgItem.FindControl("trStockStatus")) = False) Then
                    dgItem.FindControl("trStockStatus").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("DisplayStockInfo")) = False) Then
                    dgItem.FindControl("DisplayStockInfo").Visible = False
                End If
            End If

        Catch objErr As System.Exception
            If (IsNothing(dgItem.FindControl("StockInfo")) = False) Then
                dgItem.FindControl("StockInfo").Visible = False
            End If
            m_InventoryLinkVisible = True
        End Try

    End Sub
#End Region

#Region "Sub DisplayFromDefaults(ByVal dgItem As DataGridItem)"
    Protected Sub DisplayFromDefaults(ByVal dgItem As DataGridItem)
        Dim objTableRow As HtmlTableRow
        Dim objTableCell As HtmlTableCell
        Dim objLabel As Label
        Dim objLink As HyperLink
        Dim tempPrice As String
        Dim tempCustomPrice As Decimal
        Dim tempRegPrice As Decimal
        Dim tempSalePrice As Decimal
        Dim tempCartItem As BusinessRule.CCartItem
        If Not (dgItem.DataItem Is Nothing) Then
            tempCartItem = dgItem.DataItem
        End If
        Dim objTextBox As TextBox
        objTextBox = CType(dgItem.FindControl("txtQuantity"), TextBox)
        If (IsNothing(objTextBox) = False) Then
            If (objTextBox.Text = "") Then
                objTextBox.Text = m_nDefaultQty
            End If
            If (m_bDisplayQty = False) Then
                objTextBox.Visible = False
                objTextBox.Text = "" & m_nDefaultQty
            End If
        End If

        If (m_bDisplayAddToCart = False) Then
            If (IsNothing(dgItem.FindControl("AddToCart")) = False) Then
                dgItem.FindControl("AddToCart").Visible = False
            End If
        End If

        If (m_bDisplaySavedCartWishList = False) Then
            If (IsNothing(dgItem.FindControl("AddToSavedCart")) = False) Then
                dgItem.FindControl("AddToSavedCart").Visible = False
            End If
        End If
        If (m_bDisplayEMailFriend = False) Then
            If (IsNothing(dgItem.FindControl("EMailFriend")) = False) Then
                dgItem.FindControl("EMailFriend").Visible = False
            End If
        End If

        If (m_bDisplaySavedcartwishlist = False And m_bdisplayemailfriend = False And m_bdisplayaddtocart = False) Then
            If (IsNothing(dgItem.FindControl("ActionTable")) = False) Then
                dgItem.FindControl("ActionTable").Visible = False
            End If
        End If

        If (m_bDisplayProductCode = False) Then
            If (IsNothing(dgItem.FindControl("lblProdCode")) = False) Then
                dgItem.FindControl("lblProdCode").Visible = False
            End If
            If (IsNothing(dgItem.FindControl("lnkProductCode")) = False) Then
                dgItem.FindControl("lnkProductCode").Visible = False
            End If
            If (IsNothing(dgItem.FindControl("DisplayProductCode")) = False) Then
                CType(dgItem.FindControl("DisplayProductCode"), HtmlTableCell).InnerHtml = "&nbsp;"
            End If
        Else
            If (m_bLinkProductCode) Then
                If (IsNothing(dgItem.FindControl("lblProdCode")) = False) Then
                    dgItem.FindControl("lblProdCode").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("lnkProductCode")) = False) Then
                    dgItem.FindControl("lnkProductCode").Visible = True
                End If
            Else
                If (IsNothing(dgItem.FindControl("lblProdCode")) = False) Then
                    dgItem.FindControl("lblProdCode").Visible = True
                End If
                If (IsNothing(dgItem.FindControl("lnkProductCode")) = False) Then
                    dgItem.FindControl("lnkProductCode").Visible = False
                End If
            End If
        End If
        If (m_bDisplayProductName = False) Then
            If (IsNothing(dgItem.FindControl("DisplayProductName")) = False) Then
                dgItem.FindControl("DisplayProductName").Visible = False
            End If
        Else
            If (m_bLinkProductName) Then
                If (IsNothing(dgItem.FindControl("lblProdName")) = False) Then
                    dgItem.FindControl("lblProdName").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("lnkProductName")) = False) Then
                    dgItem.FindControl("lnkProductName").Visible = True
                End If
            Else
                If (IsNothing(dgItem.FindControl("lblProdName")) = False) Then
                    dgItem.FindControl("lblProdName").Visible = True
                End If
                If (IsNothing(dgItem.FindControl("lnkProductName")) = False) Then
                    dgItem.FindControl("lnkProductName").Visible = False
                End If
            End If
        End If

        If Not (dgItem.DataItem Is Nothing) Then
            tempCartItem = dgItem.DataItem
            If (m_bDisplayPriceSalePrice = False) Then
                If (IsNothing(dgItem.FindControl("tdRegularPrice")) = False) Then
                    dgItem.FindControl("tdRegularPrice").Visible = True
                End If
                If (IsNothing(dgItem.FindControl("lblRegularPriceDisplay")) = False) Then
                    dgItem.FindControl("lblRegularPriceDisplay").Visible = True
                End If
                If (IsNothing(dgItem.FindControl("tdSalePrice")) = False) Then
                    dgItem.FindControl("tdSalePrice").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("tdCustomPrice")) = False) Then
                    dgItem.FindControl("tdCustomPrice").Visible = False
                End If
                If (IsNothing(dgItem.FindControl("lblRegularPriceDisplay")) = False) Then
                    CType(dgItem.FindControl("lblRegularPriceDisplay"), Label).Text = "&nbsp;"
                End If
            Else
                If Me.m_bDisplayLabels = False Then
                    HidePriceLabels(dgItem)
                Else
                    MakePriceLabelsVisible(dgItem)
                End If
                tempCustomPrice = GetCustomPrice(dgItem)
                tempRegPrice = tempCartItem.Price
                tempSalePrice = tempCartItem.SalePrice
                If (IsNothing(dgItem.FindControl("lblRegularPriceDisplay")) = False) Then
                    CType(dgItem.FindControl("lblRegularPriceDisplay"), Label).Text = PriceDisplay2(tempRegPrice)
                End If
                If (IsNothing(dgItem.FindControl("lblSalePriceDisplay")) = False) Then
                    CType(dgItem.FindControl("lblSalePriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & PriceDisplay2(tempSalePrice)
                End If
                If (IsNothing(dgItem.FindControl("lblCustomPriceDisplay")) = False) Then
                    CType(dgItem.FindControl("lblCustomPriceDisplay"), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & PriceDisplay2(tempCustomPrice)
                End If
                If tempCartItem.IsOnSale And tempSalePrice < tempRegPrice Then
                    If tempCustomPrice < tempSalePrice And tempCustomPrice <> 0 Then
                        If (IsNothing(dgItem.FindControl("tdCustomPrice")) = False) Then
                            dgItem.FindControl("tdCustomPrice").Visible = True
                        End If
                        If (IsNothing(dgItem.FindControl("tdSalePrice")) = False) Then
                            dgItem.FindControl("tdSalePrice").Visible = False
                        End If
                        If (IsNothing(dgItem.FindControl("tdRegularPrice")) = False) Then
                            dgItem.FindControl("tdRegularPrice").Visible = False
                        End If
                    Else
                        If (IsNothing(dgItem.FindControl("tdCustomPrice")) = False) Then
                            CType(dgItem.FindControl("tdCustomPrice"), HtmlTableCell).Visible = False
                        End If
                        If (IsNothing(dgItem.FindControl("tdSalePrice")) = False) Then
                            dgItem.FindControl("tdSalePrice").Visible = True
                        End If
                        If (IsNothing(dgItem.FindControl("tdRegularPrice")) = False) Then
                            dgItem.FindControl("tdRegularPrice").Visible = False
                        End If
                    End If
                ElseIf tempCustomPrice < tempRegPrice And tempCustomPrice <> 0 Then
                    If (IsNothing(dgItem.FindControl("tdCustomPrice")) = False) Then
                        dgItem.FindControl("tdCustomPrice").Visible = True
                    End If
                    If (IsNothing(dgItem.FindControl("tdSalePrice")) = False) Then
                        dgItem.FindControl("tdSalePrice").Visible = False
                    End If
                    If (IsNothing(dgItem.FindControl("tdRegularPrice")) = False) Then
                        dgItem.FindControl("tdRegularPrice").Visible = False
                    End If
                Else
                    If (IsNothing(dgItem.FindControl("tdCustomPrice")) = False) Then
                        dgItem.FindControl("tdCustomPrice").Visible = False
                    End If
                    If (IsNothing(dgItem.FindControl("tdSalePrice")) = False) Then
                        CType(dgItem.FindControl("tdSalePrice"), HtmlTableCell).Visible = False
                    End If
                    If (IsNothing(dgItem.FindControl("tdRegularPrice")) = False) Then
                        dgItem.FindControl("tdRegularPrice").Visible = True
                    End If
                End If
            End If
        End If

        If (m_bDisplayStockInfo = False) Then
            If (IsNothing(dgItem.FindControl("DisplayStockInfo")) = False) Then
                dgItem.FindControl("DisplayStockInfo").Visible = False
            End If
        End If

    End Sub
#End Region

#Region "Private Sub GetCustomPricing(ByVal objGridItem As DataGridItem)"

    Private Function GetCustomPrice(ByVal objGridItem As DataGridItem) As Decimal
        Dim tempPrice As Decimal
        Dim custPrice As Decimal
        Dim savings As Decimal
        Dim strPrice As String

        custPrice = objGridItem.DataItem.CustomerSpecificPrice

        If CType(objGridItem.DataItem, CProduct).IsOnSale Then
            If custPrice < CType(objGridItem.DataItem, CProduct).SalePrice Then
                Return custPrice
            Else
                Return CType(objGridItem.DataItem, CProduct).SalePrice
            End If
        ElseIf custPrice < CType(objGridItem.DataItem, CProduct).Price Then
            Return custPrice
        End If
        Return 0.0
    End Function

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

    Public Sub MakePriceLabelsVisible(ByVal dgItem As DataGridItem)
        If (IsNothing(dgItem.FindControl("lblSalePrice")) = False) Then
            dgItem.FindControl("lblSalePrice").Visible = True
        End If
        If (IsNothing(dgItem.FindControl("lblRegularPrice")) = False) Then
            dgItem.FindControl("lblRegularPrice").Visible = True
        End If
        If (IsNothing(dgItem.FindControl("lblCustomPrice")) = False) Then
            dgItem.FindControl("lblCustomPrice").Visible = True
        End If
    End Sub

    Public Sub HidePriceLabels(ByVal dgitem As DataGridItem)
        If (IsNothing(dgitem.FindControl("lblSalePrice")) = False) Then
            dgitem.FindControl("lblSalePrice").Visible = False
        End If
        If (IsNothing(dgitem.FindControl("lblRegularPrice")) = False) Then
            dgitem.FindControl("lblRegularPrice").Visible = False
        End If
        If (IsNothing(dgitem.FindControl("lblCustomPrice")) = False) Then
            dgitem.FindControl("lblCustomPrice").Visible = False
        End If
    End Sub
#End Region

#Region "Public Sub AddCart(ByVal sender As Object, ByVal e As System.eventargs)"
    Public Sub AddCart(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objDataGridItem As DataGridItem
        Dim objParent As Object = sender.Parent

        objDataGridItem = DataGrid1.Items(0)
        While (Not objParent.GetType() Is objDataGridItem.GetType)
            objParent = objParent.Parent
        End While
        objDataGridItem = CType(objParent, DataGridItem)
        If (IsNothing(objDataGridItem.FindControl("txtQuantity")) = False) Then
            Try
                Dim nLong As Long = CLng(CType(objDataGridItem.FindControl("txtQuantity"), TextBox).Text)
            Catch objErr As Exception
                AddErrorMessage(m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty"))
                Exit Sub
            End Try
        End If
        AddCartClick(sender, e, Nothing, DataGrid1)
    End Sub

#End Region

#Region "Public Sub StockInfo_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles StockInfo.DataBinding"
    Public Sub StockInfo_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles StockInfo.DataBinding
        Dim oResult As CCategoryStorage
        Dim oLink As System.Web.UI.WebControls.LinkButton
        Dim pId As String
        Dim sText As String
        oLink = sender
        If oLink.Visible = True Then
            pId = oLink.CommandName
            If m_Stock_Depleted Then
                sText = "Out of Stock!"
            Else
                sText = "In Stock!"
            End If
            oLink.Text = sText
            If InvenStatVisible = True Then
                oLink.CommandName = "-1"
            End If
        End If

    End Sub
#End Region

#Region " Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objbutton As LinkButton = sender
        m_lngCurrentID = CLng(objbutton.CommandName)
    End Sub
#End Region

#Region "Public Sub DrillDown(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub DrillDown(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dlCat As DropDownList = sender

        Dim dgItem As DataGridItem
        If IsNothing(dlCat) = False Then
            If IsNothing(dlCat.SelectedItem) = False Then
                If (dlCat.SelectedItem.Value <> -2) Then
                    If IsNothing(objStorage) Then
                        objStorage = New CSearchStorage()
                        objStorage.Keyword = ""
                    End If
                    objStorage.CategoryID = CLng(dlCat.SelectedItem.Value)
                    _CatsAreGrouped = True
                    Session("Search") = objStorage

                    Dim sItem As String
                    Dim strQuery As String = "?"
                    For Each sItem In Request.QueryString.Keys
                        If (sItem.ToLower = "categoryid") Then
                            strQuery = strQuery & sItem & "=" & objStorage.CategoryID & "&"
                        Else
                            strQuery = strQuery & sItem & "=" & Request.QueryString.Item(sItem) & "&"
                        End If
                    Next
                    If (strQuery.Length > 1) Then
                        strQuery = strQuery.Substring(0, strQuery.Length - 1)
                    End If
                    Response.Redirect("SearchResult.aspx" & strQuery)
                Else
                    For Each dgItem In DataGrid1.Items
                        oResult = dgItem.DataItem
                        LoadDisplay(DataGrid1, dgItem)
                    Next
                End If
            Else
                For Each dgItem In DataGrid1.Items
                    oResult = dgItem.DataItem
                    LoadDisplay(DataGrid1, dgItem)
                Next
            End If
        Else
            For Each dgItem In DataGrid1.Items
                oResult = dgItem.DataItem

                LoadDisplay(DataGrid1, dgItem)
            Next
        End If

    End Sub


#End Region

End Class
