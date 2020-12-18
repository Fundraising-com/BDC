'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Xml
Imports StoreFront.BusinessRule

Imports StoreFront.SystemBase


Public Class CMultiBotSP2
    Inherits CWebPage

#Region "Class Members"

    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ProductImage As System.Web.UI.WebControls.Panel
    Protected WithEvents lblProductName As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblVendor As System.Web.UI.WebControls.Label
    Protected WithEvents lblManufacturer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalePrice As System.Web.UI.WebControls.Label
    Protected WithEvents btnVolumePricing As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblStockInfo As System.Web.UI.WebControls.Label
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAddToCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnAddToSavedCart As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnEMailFriend As System.Web.UI.WebControls.LinkButton
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents CAttributeControl1 As CAttributeControl
    Protected WithEvents CInventoryControl1 As CInventoryControl
    Protected WithEvents Volumepricing1 As VolumePricing
    Protected WithEvents pnlStock As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPrice As System.Web.UI.WebControls.Panel
    Protected WithEvents tblVolumePriceing As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblSalePriceDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegularPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegularPriceDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustomPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblcustomPriceDisplay As System.Web.UI.WebControls.Label
    '
    Protected WithEvents trVolumePricing3 As HtmlTableRow
    Protected WithEvents AttRow As HtmlTableCell
    Protected WithEvents trVolumePricing As HtmlTableRow
    Protected WithEvents DisplayStockInfo As HtmlTableRow
    Protected WithEvents StockDisplay As HtmlTableRow
    Protected WithEvents StockInfo As System.Web.UI.WebControls.LinkButton
    Private m_lngId As Long
    'DisplayStockInfo
    'Event AttributeRequiredError As EventHandler
    Dim m_objProduct As CCartItem
    Protected WithEvents txtId As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShrtDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lnkProductCode As System.Web.UI.WebControls.HyperLink
    Protected WithEvents tblProdBot As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents pnlBot As System.Web.UI.WebControls.Panel
    Protected WithEvents tdContent As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Form2 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents BotTable As System.Web.UI.HtmlControls.HtmlTable

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

#Region " Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment, True)
        Try
            Me.ErrorMessage.Visible = False
            Dim obj As New Panel
            Dim oItem As Control
            Dim objLinkButton As New LinkButton
            Dim oItemRow As System.Web.UI.HtmlControls.HtmlTableRow
            Dim oItemCol As System.Web.UI.HtmlControls.HtmlTableCell
            Dim oItem2 As Object
            Dim lngProdId As Long
            'Dim strName As String
            Me.ErrorMessage.Visible = False

            For Each oItemRow In BotTable.Rows
                For Each oItemCol In oItemRow.Cells
                    For Each oItem In oItemCol.Controls
                        If TypeOf oItem Is Panel Then
                            pnlBot = CType(oItem, Panel)
                            lngProdId = Replace(pnlBot.ID, "_", "")
                            SetProduct(lngProdId)
                            If Not Product.ISActive Then Exit Sub

                            For Each oItem2 In pnlBot.Controls
                                If TypeOf oItem2 Is LinkButton Then
                                    objLinkButton = CType(oItem2, LinkButton)
                                    If objLinkButton.ID = "btnAddToSavedCart" & pnlBot.ID Or _
                                        objLinkButton.ID = "btnAddToCart" & pnlBot.ID Then
                                        objLinkButton.Attributes.Add("onclick", "return SetValidationProductBot('" & pnlBot.ID & ":','" & pnlBot.ID & "');")
                                    End If
                                End If
                            Next

                            If Not IsPostBack Then pnlBot.DataBind()

                            LabelText(pnlBot, "_" & lngProdId)
                            SetLabelVisible(pnlBot, "_" & lngProdId)
                            CAttributeControl1 = pnlBot.FindControl("CAttributeControl1_" & lngProdId)
                            StockInfo = pnlBot.FindControl("StockInfo_" & lngProdId)
                            tblVolumePriceing = pnlBot.FindControl("tblVolumePriceing_" & lngProdId)

                            If Not IsNothing(StockInfo) Then
                                StockInfo.Visible = Me.Product.Inventory.InventoryTracked
                            Else
                                CInventoryControl1 = pnlBot.FindControl("CInventoryControl1_" & lngProdId)
                                If Not IsNothing(CInventoryControl1) Then
                                    Dim bivSeen As Boolean = False
                                    bivSeen = CBool("0" & CStr(ViewState.Item("IV" & lngProdId)))
                                    CInventoryControl1.Visible = bivSeen
                                End If
                            End If

                            If Not IsNothing(tblVolumePriceing) Then
                                tblVolumePriceing.Visible = Me.Product.HasVolumePricing
                                Volumepricing1 = tblVolumePriceing.FindControl("Volumepricing1_" & lngProdId)
                                If Not IsNothing(Volumepricing1) Then
                                    Dim bvpSeen As Boolean = False
                                    bvpSeen = CBool("0" & CStr(ViewState.Item("VP" & lngProdId)))
                                    Volumepricing1.Visible = bvpSeen
                                End If
                            End If
                        End If

                    Next
                Next
            Next
            If Not FindControl("myhiddenfield") Is Nothing Then InventoryBackOrderConfirm(CType(FindControl("myhiddenfield"), HtmlInputHidden).Value)
        Catch err As SystemException
            'may be No Bot on page
        End Try
    End Sub
#End Region

#Region "Public ReadOnly Property AddCartImage() As String"

    Public ReadOnly Property AddCartImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value

            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region "Public ReadOnly Property AddToSavedCartImage() As String"

    Public ReadOnly Property AddToSavedCartImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("AddToSavedCart").Attributes("Filepath").Value
            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region "Public ReadOnly Property EmailImage() As String"

    Public ReadOnly Property EmailImage() As String
        Get
            Dim sReturn As String
            sReturn = dom.Item("SiteProducts").Item("SiteImages").Item("EmailFriend").Attributes("Filepath").Value
            sReturn = Replace(sReturn, "../", "")
            Return sReturn
        End Get
    End Property

#End Region

#Region " Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.EventArgs)"

    Public Sub AddCart(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim objButton As LinkButton
            Dim ar() As String
            Dim sID As String
            objButton = CType(sender, LinkButton)
            ar = Split(CStr(objButton.ID), "_")
            sID = ar(1)
            pnlBot = Me.FindControl("_" & sID)

            txtQty = pnlBot.FindControl("txtQty_" & sID)
            '#1112 DP
            If InStr(objButton.ID.ToLower, "btnemailfriend") > 0 Then
                txtQty.Text = "1"
            End If

            Try
                Dim nLong As Long = CLng(txtQty.Text)
            Catch err As SystemException
                Me.ErrorMessage.Text = m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty")
                Exit Sub
            End Try
            CAttributeControl1 = pnlBot.FindControl("CAttributeControl1_" & sID)
        Catch objErr As Exception
            Me.ErrorMessage.Text = objErr.Message
            'obj.AddErrorMessage(m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty"))
            Exit Sub
        End Try
        AddCartClick(sender, e, txtQty, CAttributeControl1)
    End Sub

#End Region

#Region " Public Property Product() As CProduct"


    Public Property Product() As CCartItem
        Get
            Return m_objProduct
        End Get
        Set(ByVal Value As CCartItem)
            m_objProduct = Value
        End Set
    End Property

#End Region

#Region " Public WriteOnly Property ProdId() As Long"

    Public WriteOnly Property ProdId() As Long
        Set(ByVal Value As Long)
            m_lngId = Value
        End Set
    End Property

#End Region

#Region "Public ReadOnly Property DetailLink() As String"

    Public ReadOnly Property DetailLink() As String
        Get
            If (Product.DetailLink.StartsWith(".") = True) Then
                Dim slink As String
                slink = Replace(Product.DetailLink, "../", "")
                slink = Replace(slink, "./", "")
                If (slink.StartsWith("/") = True) Then
                    slink = Replace(Product.DetailLink, "/", "")
                End If
                Return StoreFrontConfiguration.SiteURL & slink
            Else
                Return Product.DetailLink
            End If
        End Get
    End Property

#End Region

#Region " Public Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)"


    Public Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objbutton As LinkButton
        objbutton = CType(sender, LinkButton)
        Dim ar() As String
        Dim sID As String
        ar = Split(CStr(objbutton.ID), "_")
        sID = ar(1)
        If ar(0).ToString.ToLower = "btnvolumepricing" Then
            Dim bSeen As Boolean = False
            bSeen = CBool("0" & CStr(ViewState.Item("VP" & sID)))
            pnlBot = Me.FindControl("_" & sID)
            Volumepricing1 = pnlBot.FindControl("Volumepricing1_" & sID)
            If bSeen = True Then
                ViewState.Item("VP" & sID) = "0"
                Volumepricing1.Visible = False
            Else
                ViewState.Item("VP" & sID) = "1"
                Volumepricing1.Visible = True
            End If
        End If


    End Sub

#End Region

#Region "Private Sub Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl)"


    Private Sub Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl)

        Dim dlItem As DataListItem
        'Dim oCont As Control
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        Dim objAttributes As Object
        Dim sAttName As String
        Session("OrderAttributes") = Nothing

        If oAttributeControl Is Nothing Then
            Exit Sub 'exit 
        Else
            m_OrderAttributes = New ArrayList
            'set attributes 
            DlAttributes = CType(oAttributeControl.FindControl("DlAttributes"), DataList)

            For Each dlItem In DlAttributes.Items
                'late bind based on dropdown or radiolist
                If oAttributeControl.DisplayType = CAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If
                sAttName = ""
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text
                '
                If objAttributes.SelectedItem Is Nothing Then
                    m_bAttribute_Error = True
                    Me.ErrorMessage.Text = sAttName & " Required"
                    Me.ErrorMessage.Visible = True
                    Exit Sub
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    ' required raise error
                    Me.ErrorMessage.Text = sAttName & " Required "
                    Me.ErrorMessage.Visible = True
                    m_bAttribute_Error = True
                    Exit Sub
                Else
                    Dim oAttStorage As New CAttributesSelected
                    oattStorage.UID = CLng(objAttributes.SelectedItem.Value())
                    sTemp = CType(dlItem.FindControl("AttributeID"), TextBox).Text
                    oattStorage.AttributeId = CLng(sTemp)
                    m_OrderAttributes.Add(oattStorage)
                End If
            Next
            'set Custom attributes
            DlCustomAttributes = CType(oAttributeControl.FindControl("dlCustomAttributes"), DataList)
            For Each dlItem In DlCustomAttributes.Items
                Dim oAttStorage As New CAttributesSelected
                IsRequired = CType(dlItem.FindControl("CustomRequired"), TextBox).Text
                If IsRequired = True Then
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) = "" Then
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        Me.ErrorMessage.Text = sAttName & " Required "
                        Me.ErrorMessage.Visible = True
                        m_bAttribute_Error = True
                        Exit Sub
                    Else
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                Else
                    'Not required
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) <> "" Then
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                End If
            Next
            Session("OrderAttributes") = m_OrderAttributes
        End If
    End Sub
#End Region

#Region "Public Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)"


    Public Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal txtQty As TextBox, ByVal oAttControl As CAttributeControl)
        Dim objButton As LinkButton
        Dim ar() As String
        Dim sID As String
        Dim sOrigID As String
        objButton = CType(sender, LinkButton)
        ar = Split(CStr(objButton.ID), "_")
        sOrigID = ar(0)
        sID = ar(1)
        If Not IsNumeric(txtQty.Text) Then
            objButton.CommandArgument = 1
        Else
            objButton.CommandArgument = txtQty.Text
        End If

        'get attributes selected
        Set_Item_Attributes(oAttControl)
        Dim arPage() As String
        arPage = Split(Me.CurrentWebPage, "/")

        Me.sPage = arPage(arPage.Length - 1)



        If (sOrigID = "btnAddToCart") Then
            Me.AddItemToCart(objButton, e)
        ElseIf (sOrigID = "btnAddToSavedCart") Then
            Me.AddItemToSavedCart(objButton, e)
        ElseIf (sOrigID = "btnEMailFriend") Then
            EMailAFriend(objButton, e)
        ElseIf (sOrigID = "btnVolumePricing") Then
            If Volumepricing1.Visible = True Then
                Volumepricing1.Visible = False
            Else
                Volumepricing1.Visible = True
            End If
        End If
    End Sub
#End Region

#Region " Public Sub SetProduct(ByVal lngProdId As Long)"

    Public Sub SetProduct(ByVal lngProdId As Long)
        Dim obXML As XmlNode
        Try
            CAttributeControl1 = pnlBot.FindControl("CAttributeControl1_" & lngProdId)
            pnlStock = pnlBot.FindControl("pnlStock_" & lngProdId)
            tblVolumePriceing = pnlBot.FindControl("tblVolumePriceing_" & lngProdId)
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                obXML = m_objXMLAccess.GetProduct(lngProdId)
                Product = New CCartItem(obXML, 1, , Me.m_objCustomer.CustomerGroup)
            Else
                Dim oprodManagement As New Management.CProductManagement
                Dim drProd As dsProducts.ProductsRow = oprodManagement.GetProductRow(lngProdId, m_objCustomer.CustomerGroup).Products.Rows(0)
                Product = New CCartItem(drProd, 1, , m_objCustomer.CustomerGroup)
                oprodManagement = Nothing
            End If
            If Product.ISActive Then
                If Not IsPostBack Then
                    CAttributeControl1.Data_Bind(Me.Product, m_objCustomer) '1521
                End If

                If IsNothing(pnlStock) = False Then
                    If Product.Inventory.InventoryTracked = True Then
                        If Product.Inventory.ShowStatus = True Then
                            pnlStock.Visible = True
                            CInventoryControl1 = pnlBot.FindControl("CInventoryControl1_" & lngProdId)
                            CInventoryControl1.Visible = False
                        Else
                            pnlStock.Visible = False
                        End If
                    End If
                End If
            End If
        Catch err As SystemException
            'may be No Bot on page
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try

    End Sub
#End Region

#Region " Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub StockButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objbutton As LinkButton = sender
        Dim ar() As String
        Dim sID As String

        ar = Split(CStr(objbutton.ID), "_")
        sID = ar(1)
        pnlBot = Me.FindControl("_" & sID)
        CInventoryControl1 = pnlBot.FindControl("CInventoryControl1_" & sID)
        Dim bSeen As Boolean = False
        bSeen = CBool("0" & CStr(ViewState.Item("IV" & sID)))
        If bSeen = True Then
            CInventoryControl1.Visible = False
            ViewState.Item("IV" & sID) = "0"
        Else
            CInventoryControl1.Visible = True
            CInventoryControl1.ProductID = CLng(objbutton.CommandName)
            ViewState.Item("IV" & sID) = "1"
        End If
    End Sub

#End Region

#Region "Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded"

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        Try
            If (IsNothing(Session("ItemAdded")) = False) Then
                SetMessage(Message)
            Else
                Message.Text = ""
                Message.Visible = False
            End If
        Catch err As SystemException
        End Try
    End Sub

#End Region

#Region "Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR"

    Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR
        Try
            ErrorMessage.Visible = True
            ErrorMessage.Text = sender.ToString
        Catch err As System.Exception
        End Try
    End Sub

#End Region


#Region "Public Sub tblPrice_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub tblPrice_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tblPrice As System.Web.UI.WebControls.Table
        tblPrice = sender
        Dim ar() As String
        Dim sID As String
        Dim sOrigID As String

        ar = Split(CStr(tblPrice.ID), "_")
        sOrigID = ar(0)
        sID = ar(1)

        If Not IsNothing(Me.Product) Then
            Dim tempSalePrice As Decimal
            Dim tempRegPrice As Decimal
            Dim tempCustomPrice As Decimal
            tempCustomPrice = GetCustomPrice()
            tempRegPrice = Product.Price
            tempSalePrice = Product.SalePrice
            CType(tblPrice.FindControl("lblRegularPriceDisplay_" & sID), Label).Text = PriceDisplay2(tempRegPrice)
            CType(tblPrice.FindControl("lblSalePriceDisplay_" & sID), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempSalePrice)
            CType(tblPrice.FindControl("lblCustomPriceDisplay_" & sID), Label).Text = "<S>" & PriceDisplay2(tempRegPrice) & "</S>&nbsp;<BR>" & PriceDisplay2(tempCustomPrice)
            If Product.IsOnSale And tempSalePrice < tempRegPrice Then
                If tempCustomPrice < tempSalePrice And tempCustomPrice <> 0 Then
                    CType(tblPrice.FindControl("trCustomPrice_" & sID), TableRow).Visible = True
                    CType(tblPrice.FindControl("trSalePrice_" & sID), TableRow).Visible = False
                    CType(tblPrice.FindControl("trRegularPrice_" & sID), TableRow).Visible = False
                Else
                    CType(tblPrice.FindControl("trCustomPrice_" & sID), TableRow).Visible = False
                    CType(tblPrice.FindControl("trSalePrice_" & sID), TableRow).Visible = True
                    CType(tblPrice.FindControl("trRegularPrice_" & sID), TableRow).Visible = False
                End If
            ElseIf tempCustomPrice < tempRegPrice And tempCustomPrice <> 0 Then
                CType(tblPrice.FindControl("trCustomPrice_" & sID), TableRow).Visible = True
                CType(tblPrice.FindControl("trSalePrice_" & sID), TableRow).Visible = False
                CType(tblPrice.FindControl("trRegularPrice_" & sID), TableRow).Visible = False
            Else
                CType(tblPrice.FindControl("trCustomPrice_" & sID), TableRow).Visible = False
                CType(tblPrice.FindControl("trSalePrice_" & sID), TableRow).Visible = False
                CType(tblPrice.FindControl("trRegularPrice_" & sID), TableRow).Visible = True
            End If
        End If

    End Sub


#End Region

#Region "Private Function GetCustomPrice() As Decimal"

    Private Function GetCustomPrice() As Decimal
        'Dim tempPrice As Decimal
        Dim custPrice As Decimal
        'Dim savings As Decimal
        custPrice = Product.CustomerSpecificPrice
        If Product.IsOnSale Then
            If custPrice < Product.SalePrice Then
                Return custPrice
            Else
                Return Product.SalePrice
            End If
        ElseIf custPrice < Product.Price Then
            Return custPrice
        End If
        Return 0.0
    End Function

#End Region

#Region "Public WriteOnly Property BotTitle() As String"

    Public WriteOnly Property BotTitle() As String
        Set(ByVal Value As String)
            Try
                If Value.ToString <> "" Then
                    Me.TopSubBanner1.PageName = Value
                End If
            Catch err As SystemException

            End Try

        End Set
    End Property

#End Region

#Region "Protected Sub LabelText(ByVal objContainer As Panel, ByVal sId As String)"

    Protected Sub LabelText(ByVal objContainer As Panel, ByVal sId As String)
        Dim objLabel As Label
        Dim arLabels As New ArrayList
        Dim strID As String
        Dim sHolder As String
        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblRegularPrice"))
        arLabels.Add(New String("lblVolumePrice"))
        arLabels.Add(New String("lblStockInfo"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))

        For Each strID In arLabels
            Try
                strID = strID
                sHolder = strID
                objLabel = objContainer.FindControl(strID & sId)
                If (IsNothing(objLabel) = False) Then
                    If strID <> "lblMoreInfo" & sId Then
                        If strID.ToLower = "lblregularprice" Then
                            strID = "lblPrice"
                        End If
                        If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                            objLabel.Text = ""
                        Else
                            objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                        End If
                    Else
                        objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText()
                    End If
                End If
                strID = sHolder

                objLabel = objContainer.FindControl(strID & "2" & sId)
                If (IsNothing(objLabel) = False) Then
                    If (StoreFrontConfiguration.Labels.Item(strID).InnerText().Trim.Length = 0) Then
                        objLabel.Text = ""
                    Else
                        objLabel.Text = StoreFrontConfiguration.Labels.Item(strID).InnerText() & ":&nbsp;"
                    End If
                End If
            Catch err As SystemException

            End Try

        Next
    End Sub


#End Region

#Region "Protected Sub SetLabelVisible(ByVal objContainer As Panel, ByVal sId As String)"

    Protected Sub SetLabelVisible(ByVal objContainer As Panel, ByVal sId As String)
        Dim objLabel As Label
        Dim arLabels As New ArrayList
        Dim strID As String
        Dim objNode As XmlNode
        Dim bVisible As Boolean = True
        Try
        Catch err As SystemException

        End Try
        objNode = dom.Item("SiteProducts").Item("SearchResult")
        bVisible = IIf(objNode.Attributes("DisplayLabels").Value = "1", True, False)

        arLabels.Add(New String("lblProductCode"))
        arLabels.Add(New String("lblDescription"))
        arLabels.Add(New String("lblPrice"))
        arLabels.Add(New String("lblCategory"))
        arLabels.Add(New String("lblCategorys"))
        arLabels.Add(New String("lblManufacturer"))
        arLabels.Add(New String("lblManufacturers"))
        arLabels.Add(New String("lblVendor"))
        arLabels.Add(New String("lblVendors"))
        arLabels.Add(New String("lblProductName"))
        arLabels.Add(New String("lblMoreInfo"))
        arLabels.Add(New String("lblSalePrice"))
        arLabels.Add(New String("lblRegularPrice"))
        arLabels.Add(New String("lblCustomPrice"))

        For Each strID In arLabels
            objLabel = objContainer.FindControl(strID & sId)
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
            objLabel = objContainer.FindControl(strID & "2" & sId)
            If (IsNothing(objLabel) = False) Then
                objLabel.Visible = bVisible
            End If
        Next

    End Sub


#End Region
End Class
