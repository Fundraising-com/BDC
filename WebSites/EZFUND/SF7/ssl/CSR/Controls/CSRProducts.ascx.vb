Imports StoreFront.BusinessRule
Imports CSR.CSRBusinessRule
Imports CSR.CSRSystemBase
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports System.Text
Partial  Class CSRProducts
    Inherits CSRWebControl
    Protected WithEvents Cart As System.Web.UI.WebControls.DataList
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents CAttributeControl1 As CSRAttributes


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

#Region "Class Events"
    Event RecalculateOrder()
    Event ResetShipping()
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NoProdMessage.Visible = False
        GetOrder()
        SetEnterKeyPostBack(Me.NewSKU, Me.DisplayProduct)
    End Sub
    'Private Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "", Optional ByVal condition As String = "")

    '    If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
    '        Dim script As New StringBuilder
    '        script.Append("if (isEnterKey(event)")
    '        If Not IsNothing(condition) AndAlso condition <> String.Empty Then
    '            script.Append(" && " & condition)
    '        End If
    '        script.Append(") ")
    '        script.Append("postBack(event, '" & objButton.UniqueID & "', '" & argument & "')")
    '        objTextControl.Attributes.Add("onKeyDown", script.ToString)
    '    End If

    'End Sub


    Private Sub Page_Prerender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Dim dl As DataList
        Dim dlItem1 As DataListItem

        Dim bShowPackages As Boolean = False
        'Dim dlItem2 As DataListItem
        If Me.packages.Items.Count > 0 Then
            For Each dlItem1 In Me.packages.Items
                dl = dlItem1.FindControl("Cart")
                If dl.Items.Count <= 0 Then
                    dl.Visible = False
                Else
                    dl.Visible = True
                    bShowPackages = True
                End If

            Next

        End If
        Me.packages.Visible = bShowPackages
    End Sub
    Private Sub DisplayProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayProduct.Click
        Session("CSRNewProduct") = Me.NewSKU.Text


        DisplayProductOnPage(Me.NewSKU.Text)

        'begin: GJV - 7/30/2007 - CSR
        If NoProdMessage.Visible Then MagicAjax.AjaxCallHelper.WriteLine("JavaScript:Search();")
        'end: GJV - 7/30/2007 - CSR

    End Sub


    Public Sub DisplayProductOnPage(ByVal sku As String)
        If sku.Trim = "" Then
            Exit Sub
        End If
        Dim Item As CCartItem
        Item = getItem(sku, False, Nothing, Nothing)
        If Item Is Nothing = False AndAlso Item.ISActive = True Then
            SetNewProdVisibility(True)
            Me.NewProdName.Text = Item.ProductCode & "-" & Item.Name
            Me.NewQuantity.Text = "1"
            Me.NewPrice.Visible = True
            Me.NewPrice.Text = Format(CDec(GetItemPriceWithoutAttributes(Item)), "n2")

            'begin: GJV - 7/31/2007 - CSR
            Dim CSRMan As New CSRManagement(StoreFrontConfiguration.SiteURL)
            'If CSRMan.IsAdvancedCSR = False Then

            '    Me.NewPrice.Enabled = False
            'Else
            '    Me.NewPrice.Enabled = True
            'End If
            Dim CSRUserMgmt As New CSRUserManagement
            Dim CSRUser As CSRUser = CSRUserMgmt.GetUser(Session.Item("CSRUID"))

            Me.NewPrice.Enabled = CSRMan.IsAdvancedCSR And CSRUser.OverridePricing
            'end: GJV - 7/31/2007 - CSR


            Me.OldPrice.Value = Me.NewPrice.Text
            'populate selected attribs if they entered SKU instead of Prod ID
            Dim drAttribs As DataRow
            Dim oInventory As New CSRInventory
            drAttribs = oInventory.GetInventoryBySku(Me.NewSKU.Text)
            Dim SelectedAtts As New ArrayList
            If drAttribs Is Nothing = False Then
                If drAttribs("AttributeDetailID").ToString <> "" Then
                    Dim arAttDetailID As String() = Split(drAttribs("AttributeDetailID"), ",")
                    Dim x As Integer
                    Dim Attribute As New Management.CStoreProducts
                    Dim ProductAttributes As ArrayList = Attribute.GetProductAttributes(Item.ProductID)

                    For x = 0 To (arAttDetailID.Length - 1)
                        Dim oAttStorage As New CAttributesSelected
                        oAttStorage.UID = arAttDetailID(x)
                        Dim sTemp As String = ""
                        sTemp = CType(ProductAttributes(x), CAttribute).UID
                        oAttStorage.AttributeId = CLng(sTemp)
                        SelectedAtts.Add(oAttStorage)
                    Next
                End If
            End If
            CAttributeControl1.SelectedAttributes = SelectedAtts
            CAttributeControl1.Data_Bind(Item, CSRCustomer)
            Me.NewSKU.Text = ""
            hdnProdID.Value = Item.ProductCode
        Else
            NoProdMessage.Visible = True
            Me.SetNewProdVisibility(False)
        End If
    End Sub

    Private Sub AddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddProduct.Click
        Call AddProductToOrder(Me.hdnProdID.Value)
        If IsError = False Then

            RaiseEvent RecalculateOrder()
            SetNewProdVisibility(False)
        End If
    End Sub
    Public Sub SetNewProdVisibility(ByVal IsVisible As Boolean)
        Me.NewProdName.Visible = IsVisible
        Me.NewQuantity.Visible = IsVisible
        Me.NewPrice.Visible = IsVisible
        Me.CAttributeControl1.Visible = IsVisible
        Me.AddProduct.Visible = IsVisible
        Me.lblNewOptions.Visible = IsVisible
        Me.lblNewPrice.Visible = IsVisible
        Me.lblNewQty.Visible = IsVisible

    End Sub

    Public Sub deleteRow(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        'this gets the index of the first level (package) data list
        i = sender.commandname   'CInt("0" & sender.parent.parent.parent.findcontrol("hdnOrderAddressIndex").value.ToString)
        Dim OrderItem As CSROrderItem

        OrderItem = CSROrder.OrderAddresses(i).orderitems(CInt(sender.CommandArgument))
        DeleteItem(i, CInt(sender.CommandArgument))

        Session("CSRNewProduct") = ""
        Session("CSROrder") = CSROrder

        ResetBackOrdersForSpecificItem(OrderItem)
        RaiseEvent RecalculateOrder()
    End Sub

    Public Sub RemoveBackorder(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        'this gets the index of the first level (package) data list
        i = sender.commandname   'CInt("0" & sender.parent.parent.parent.findcontrol("hdnOrderAddressIndex").value.ToString)
        Dim OrderItem As CSROrderItem
        OrderItem = CSROrder.OrderAddresses(i).orderitems(CInt(sender.CommandArgument))
        If OrderItem.Quantity <= OrderItem.BackOrderQuantity Then
            DeleteItem(i, CInt(sender.CommandArgument))
        Else
            OrderItem.Quantity = OrderItem.Quantity - OrderItem.BackOrderQuantity
            OrderItem.BackOrderQuantity = 0
        End If
        Session("CSRNewProduct") = ""
        Session("CSROrder") = CSROrder

        RaiseEvent RecalculateOrder()
        ResetBackOrdersForSpecificItem(OrderItem)
    End Sub
    Private Sub DeleteItem(ByVal OrderAddressIndex As Integer, ByVal OrderItemIndex As Integer)
        CSROrder.OrderAddresses(OrderAddressIndex).RemoveOrderItem(OrderItemIndex)
        If CType(CSROrder.OrderAddresses(OrderAddressIndex), CSROrderAddress).OrderItems.Count = 0 And CSROrder.OrderAddresses.Count > 1 Then
            CSROrder.OrderAddresses.RemoveAt(OrderAddressIndex)
            If CSROrder.OrderAddresses.Count = 1 Then
                Session("CSRShipAddress") = CType(CSROrder.OrderAddresses(0), CSROrderAddress).Address.ID
                RaiseEvent ResetShipping()
            End If
            CSROrder.SetPackageIndexes()
        End If

    End Sub
    Public Sub bindCart()
        GetOrder()
        Me.packages.DataSource = CSROrder.OrderAddresses
        packages.DataBind()

        If Session("CSRNewProduct") Is Nothing = False Then
            DisplayProductOnPage(Session("CSRNewProduct"))
        End If
    End Sub
    Public Sub packages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles packages.ItemDataBound
        Dim dlCart As DataList
        If IsNothing(e.Item.DataItem) = False Then
            dlCart = e.Item.FindControl("Cart")
            dlCart.DataSource = CType(e.Item.DataItem, CSROrderAddress).OrderItems
            dlCart.DataBind()

        End If
    End Sub
    Public Sub Cart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles Cart.ItemDataBound
        Dim lblBackOrder As Label
        Dim GiftWrap As HyperLink
        If IsNothing(e.Item.DataItem) = False Then
            lblBackOrder = e.Item.FindControl("BackOrder")
            GiftWrap = e.Item.FindControl("cmdGiftWrap")
            If CType(e.Item.DataItem, CSROrderItem).BackOrderQuantity > 0 Then
                lblBackOrder.Visible = True
            Else
                lblBackOrder.Visible = False
            End If
            If CType(e.Item.DataItem, CSROrderItem).IsGiftWrapable = False Then
                GiftWrap.Visible = False
            Else
                GiftWrap.Visible = True
            End If
        End If
    End Sub

End Class
