Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Public Class RelatedProductControl
    Inherits CProductDetailBase

    Private prodId As Long = 0
    Private m_lngItemsPerRow As Long = 0

#Region "Properties"

#Region "Public ReadOnly Property TotalPageCount() As String"
    Public ReadOnly Property TotalPageCount() As String
        Get
            If IsNothing(Product) Then Return ""
            If Product.RelatedProducts.Count Mod m_lngItemsPerRow > 0 Then
                Return CDec(Product.RelatedProducts.Count / m_lngItemsPerRow + 1).ToString.Substring(0, 1)
            Else
                Return (Product.RelatedProducts.Count / m_lngItemsPerRow).ToString
            End If
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property CurrentPage() As String"
    Public ReadOnly Property CurrentPage() As String
        Get
            If IsNothing(Product) Then Return ""
            Return CDec(CInt("0" & hdnStart.Value) / m_lngItemsPerRow + 1).ToString.Substring(0, 1)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property DisplayShortDescription(ByVal shortDesc As String) As String"
    Public ReadOnly Property DisplayShortDescription(ByVal shortDesc As String) As String
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("DisplayRecommendedShortDescription").Value = "1", shortDesc, "")
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property DisplayPrice() As Boolean"
    Public ReadOnly Property DisplayPrice() As Boolean
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("DisplayRecommendedPrice").Value = "1", True, False)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property DisplayProductCode() As Boolean"
    Public ReadOnly Property DisplayProductCode() As Boolean
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("DisplayRecommendedCode").Value = "1", True, False)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property DisplayProductName() As Boolean"
    Public ReadOnly Property DisplayProductName() As Boolean
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("DisplayRecommendedName").Value = "1", True, False)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property DisplayProductName() As Boolean"
    Public ReadOnly Property DisplayProductImage() As Boolean
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("DisplayRecommendedImage").Value = "1", True, False)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property LinkCode(ByVal url As String) As String"
    Public ReadOnly Property LinkCode(ByVal url As String) As String
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("LinkProductCode").Value = "1", url, "")
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property LinkName(ByVal url As String) As String"
    Public ReadOnly Property LinkName(ByVal url As String) As String
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("LinkProductName").Value = "1", url, "")
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property LinkImage(ByVal url As String) As String"
    Public ReadOnly Property LinkImage(ByVal url As String) As String
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("LinkImage").Value = "1", url, "")
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property WidthPercent() As Integer"
    Public ReadOnly Property WidthPercent() As Integer
        Get
            Return IIf(StoreFrontConfiguration.ProductDetail.Attributes("RecommendedItemsPerRow").Value = "3", 33, 20)
        End Get
    End Property
#End Region

#Region "Public ReadOnly Property FirstPageMaxItemIndex() As Integer"
    Public ReadOnly Property FirstPageMaxItemIndex() As Integer
        Get
            Return CInt(StoreFrontConfiguration.ProductDetail.Attributes("RecommendedItemsPerRow").Value) - 1
        End Get
    End Property
#End Region

#End Region

#Region "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Visible Then Exit Sub
        prodId = CLng("0" & Request.QueryString("ID"))
        If IsNothing(Session("dProduct")) Then Exit Sub
        m_lngItemsPerRow = CInt("0" & StoreFrontConfiguration.ProductDetail.Attributes("RecommendedItemsPerRow").Value)
        If CType(Session("dProduct"), CCartItem).ProductID <> prodId AndAlso prodId > 0 Then
            Dim oprodManagement As New Management.CProductManagement
            Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(prodId, m_objCustomer.CustomerGroup).Products.Rows(0)
            Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
            Session("dProduct") = Product
            oprodManagement = Nothing
        Else
            Product = CType(Session("dProduct"), CCartItem)
        End If
        If Not IsPostBack Then
            dlRelatedProd.RepeatColumns = m_lngItemsPerRow
            hdnEnd.Value = FirstPageMaxItemIndex
            RelatedProducts(dlRelatedProd, CLng(hdnStart.Value), CLng(hdnEnd.Value))
            If Not dlRelatedProd.Visible Then
                Visible = False
            Else
                Visible = True
            End If
        End If
        LinkDisplay()
        SetPaging()
    End Sub
#End Region

#Region "Functions"

#Region "Private Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click"
    Private Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        If prodId <= 0 Then Exit Sub
        If IsNothing(Session("dProduct")) Then Exit Sub
        RelatedProducts(dlRelatedProd, CLng(hdnStart.Value) - m_lngItemsPerRow, CLng(hdnEnd.Value) - m_lngItemsPerRow)
        hdnStart.Value = CLng(hdnStart.Value) - m_lngItemsPerRow
        hdnEnd.Value = CLng(hdnEnd.Value) - m_lngItemsPerRow
        LinkDisplay()
    End Sub
#End Region

#Region "Private Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click"
    Private Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        If prodId <= 0 Then Exit Sub
        If IsNothing(Session("dProduct")) Then Exit Sub
        RelatedProducts(dlRelatedProd, CLng(hdnEnd.Value) + 1, CLng(hdnEnd.Value) + m_lngItemsPerRow)
        hdnStart.Value = CLng(hdnEnd.Value) + 1
        hdnEnd.Value = CLng(hdnEnd.Value) + m_lngItemsPerRow
        LinkDisplay()
    End Sub
#End Region

#Region "Private Sub lnkFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFirst.Click"
    Private Sub lnkFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFirst.Click
        If prodId <= 0 Then Exit Sub
        If IsNothing(Session("dProduct")) Then Exit Sub
        RelatedProducts(dlRelatedProd, 0, m_lngItemsPerRow - 1)
        hdnStart.Value = 0
        hdnEnd.Value = m_lngItemsPerRow - 1
        LinkDisplay()
    End Sub
#End Region

#Region "Private Sub lnkLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLast.Click"
    Private Sub lnkLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLast.Click
        If prodId <= 0 Then Exit Sub
        If IsNothing(Session("dProduct")) OrElse IsNothing(Product) Then Exit Sub
        Dim start As Integer = 0
        For counter As Integer = 0 To Product.RelatedProducts.Count + m_lngItemsPerRow Step m_lngItemsPerRow
            If counter >= Product.RelatedProducts.Count Then
                hdnStart.Value = start
                hdnEnd.Value = counter - 1
                Exit For
            End If
            start = counter
        Next
        RelatedProducts(dlRelatedProd, CLng(hdnStart.Value), CLng(hdnEnd.Value))
        LinkDisplay()
    End Sub
#End Region

#Region "Private Sub LinkDisplay()"
    Private Sub LinkDisplay()
        If CLng(hdnStart.Value) <= 0 Then
            lnkPrev.Enabled = False
            lnkFirst.Enabled = False
        Else
            lnkPrev.Enabled = True
            lnkFirst.Enabled = True
        End If
        If CLng(IIf(hdnEnd.Value = "", FirstPageMaxItemIndex, hdnEnd.Value)) >= Product.RelatedProducts.Count - 1 Then
            lnkNext.Enabled = False
            lnkLast.Enabled = False
        Else
            lnkNext.Enabled = True
            lnkLast.Enabled = True
        End If
        For Each ctrl As Control In divPaging.Controls
            If ctrl.ID.IndexOf("lnkPage") >= 0 Then
                If CType(ctrl, LinkButton).Text = CurrentPage Then
                    CType(ctrl, LinkButton).Enabled = False
                Else
                    CType(ctrl, LinkButton).Enabled = True
                End If
            End If
        Next
    End Sub
#End Region

#Region "Public Function NameDisplay(ByVal name As String) As String"
    Public Function NameDisplay(ByVal name As String) As String
        If name.Length > 18 AndAlso m_lngItemsPerRow = 5 Then
            Return name.Substring(0, 18) & ".."
        Else
            Return name
        End If
    End Function
#End Region

#Region "Private Sub SetPaging()"
    Private Sub SetPaging()
        Dim lnk As LinkButton
        Dim li As Literal
        For counter As Integer = 1 To CInt(TotalPageCount)
            lnk = New LinkButton
            li = New Literal
            li.ID = "spacer_" & counter
            li.Text = " "
            lnk.ID = "lnkPage_" & counter
            lnk.CssClass = "Content"
            lnk.Style.Item("text-decoration") = "none"
            lnk.Style.Item("border") = "1px solid white"
            lnk.Style.Item("padding") = "0px 5px"
            lnk.Style.Item("color") = "White"
            lnk.Attributes.Add("onmouseover", "this.style.border='1px outset white'")
            lnk.Attributes.Add("onmouseout", "this.style.border='1px solid white'")
            lnk.Attributes.Add("onmousedown", "this.style.border='1px inset white'")
            lnk.Attributes.Add("onmouseup", "this.style.border='1px outset white'")
            lnk.CommandArgument = counter
            AddHandler lnk.Command, AddressOf lnk_click
            lnk.Text = counter
            If CurrentPage = counter.ToString Then
                lnk.Enabled = False
            Else
                lnk.Enabled = True
            End If
            divPaging.Controls.Add(lnk)
            divPaging.Controls.Add(li)
        Next
    End Sub
#End Region

#Region "Private Sub lnk_click(ByVal sender As Object, ByVal e As CommandEventArgs)"
    Private Sub lnk_click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If prodId <= 0 Then Exit Sub
        If IsNothing(Session("dProduct")) Then Exit Sub
        Dim btn As LinkButton = CType(sender, LinkButton)
        RelatedProducts(dlRelatedProd, CLng(btn.CommandArgument) * m_lngItemsPerRow - m_lngItemsPerRow, CLng(btn.CommandArgument) * m_lngItemsPerRow - 1)
        hdnStart.Value = CLng(btn.CommandArgument) * m_lngItemsPerRow - m_lngItemsPerRow
        hdnEnd.Value = CLng(btn.CommandArgument) * m_lngItemsPerRow - 1
        LinkDisplay()
    End Sub
#End Region

#End Region

#Region "Private Sub dlRelatedProd_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlRelatedProd.ItemDataBound"
    Private Sub dlRelatedProd_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlRelatedProd.ItemDataBound
        If Not IsNothing(e.Item) AndAlso (e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item) Then
            If CType(sender, DataList).DataSource.count <> 5 AndAlso CType(sender, DataList).Items.Count = CType(sender, DataList).DataSource.count - 1 Then
                CType(e.Item.FindControl("rptCell"), HtmlTableCell).Width = (100 - (CType(sender, DataList).Items.Count * WidthPercent)).ToString & "%"
            End If
        End If
    End Sub
#End Region
    
End Class