'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class storecoupons
    Inherits CWebPage

    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents StandardSearchControl21 As StandardSearchControl2
    Protected WithEvents AdminTabControl1 As AdminTabControl
    Protected WithEvents Editcoupon1 As editcoupon
    Protected WithEvents Addcoupon1 As addcoupon
    Protected objDiscount As CStoreDiscounts
    ' begin: JDB - product configurator bug 74
    Protected Overrides ReadOnly Property PreserveSessionSearch() As Boolean
        Get
            Return True
        End Get
    End Property
    ' end: JDB - product configurator bug 74

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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.Coupons) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Addcoupon1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(Editcoupon1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            objDiscount = New CStoreDiscounts

            If Not IsPostBack Then
                Me.chkMultipleCoupons.Checked = objDiscount.GetAllowMultiple
                Me.chkSaleItems.Checked = objDiscount.GetAllowWithDiscount
            End If


            LoadTabs()

        Catch ex As Exception
            Session("DetailError") = "Class StoreCoupons Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Sub AdminTabControl1_TabClick"
    Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
        If (sender = "0") Then
            LoadSearch()
            StandardSearchControl1.ReloadList()
            MakeVisible(StandardSearchControl1)
        ElseIf (sender = "1") Then
            MakeVisible(Addcoupon1)
            Addcoupon1.ClearFields()
        ElseIf (sender = "2") Then
            MakeVisible(tblGeneralInfo)
        End If
    End Sub
#End Region

    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        ' Delete giftcertificate with uid = sender
        Dim obj As New CStoreDiscounts
        obj.DeleteDiscounts(sender)
        LoadSearch()
        StandardSearchControl1.ReloadList()
    End Sub

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        Dim obj As New CStoreDiscounts

        Dim objCerts As CDiscounts = obj.GetAllDiscounts
        Dim objDiscount As CDiscount
        Dim arDis As New ArrayList
        arDis = objCerts.CouponItems

        For Each objDiscount In arDis
            If (objDiscount.ID = sender) Then
                Editcoupon1.EditCoupon = objDiscount
                Exit For
            End If
        Next
        MakeVisible(Editcoupon1)
    End Sub

#Region "Sub Editcoupon1_Save"
    Private Sub Editcoupon1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editcoupon1.Save
        AdminTabControl1.SelectedTabIndex = 0
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub
#End Region

#Region "Sub Addcoupon1_Save"
    Private Sub Addcoupon1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Addcoupon1.Save
        AdminTabControl1.SelectedTabIndex = 0
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub
#End Region

#Region "Public Sub btnSaveGeneral_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveGeneral.Click"

    Public Sub btnSaveGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGeneral.Click
        objDiscount.SetAdminAllows(chkMultipleCoupons.Checked, chkSaleItems.Checked)
    End Sub

#End Region

#Region "Sub MakeVisible(ByVal obj As Object)"
    '--------------------------------------------------
    'Sub MakeVisible
    'Parameters Object
    'Return Nothing
    'Description
    '    Makes the object that is passed in visible
    '--------------------------------------------------
    Public Sub MakeVisible(ByVal obj As Object)
        Dim _type As Type

        'Set all objects to not visible
        StandardSearchControl1.Visible = False
        StandardSearchControl21.Visible = False
        Addcoupon1.Visible = False
        Editcoupon1.Visible = False
        tblGeneralInfo.Visible = False

        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region

#Region "Sub Addcoupon1_CouponSelect"
    Private Sub Addcoupon1_CouponSelect(ByVal sender As Object, ByVal e As System.EventArgs) Handles Addcoupon1.CouponSelect
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        Session("ArrChecked") = New ArrayList

        If (CLng("0" & Addcoupon1.ApplyToID) > 0) Then
            Session("ArrChecked").Add(CLng(Addcoupon1.ApplyToID))
        End If

        If (sender = 1) Then
            LoadProducts()
        ElseIf (sender = 2) Then
            LoadCategory()
        ElseIf (sender = 3) Then
            LoadManufacturer()
        ElseIf (sender = 4) Then
            LoadVendor()
        End If

        Addcoupon1.Visible = False
        AdminTabControl1.Visible = False
        StandardSearchControl1.Visible = False
        StandardSearchControl21.ReloadList()
        StandardSearchControl21.Visible = True
        btnBackSelect.Visible = True
        btnSaveSelect.Visible = True
        imgBackSelect.Visible = True
        imgSaveSelect.Visible = True
    End Sub
#End Region

#Region "Sub Editcoupon1_CouponSelect"
    Private Sub Editcoupon1_CouponSelect(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editcoupon1.CouponSelect
        'Set Checked Items in EditCoupon
        Session("ArrChecked") = New ArrayList
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        If (CLng("0" & Editcoupon1.ApplyToID) > 0) Then
            Session("ArrChecked").Add(CLng(Editcoupon1.ApplyToID))
        End If

        If (sender = 1) Then
            LoadProducts()
        ElseIf (sender = 2) Then
            LoadCategory()
        ElseIf (sender = 3) Then
            LoadManufacturer()
        ElseIf (sender = 4) Then
            LoadVendor()
        End If

        Editcoupon1.Visible = False
        AdminTabControl1.Visible = False
        StandardSearchControl1.Visible = False
        StandardSearchControl21.ReloadList()
        StandardSearchControl21.Visible = True
        btnEditBackSelect.Visible = True
        btnEditSaveSelect.Visible = True
        imgEditBackSelect.Visible = True
        imgEditSaveSelect.Visible = True
    End Sub
#End Region

#Region "Load Routines"
    Private Sub LoadTabs()
        Dim ar As ArrayList
        ' Set the Tab Elements
        ar = New ArrayList

        ar.Add("Manage Coupons")
        ar.Add("Add Coupons")
        ar.Add("General")

        AdminTabControl1.BorderClass = "ContentTable"
        AdminTabControl1.TabItemClass = "Content"
        AdminTabControl1.TabStringArray = ar
    End Sub

    Private Sub LoadSearch()
        Dim obj As New CStoreDiscounts
        Dim objStorage As New CSearchControlStorage
        Dim objCerts As CDiscounts = obj.GetAllDiscounts
        Dim objDiscount As CDiscount
        Dim arDis As New ArrayList

        For Each objDiscount In objCerts.CouponItems
            If (objDiscount.Name <> "FreeShipping" And objDiscount.Name <> "Discount") Then
                arDis.Add(objDiscount)
            End If
        Next

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = True
        objStorage.DataSource = arDis
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Coupons"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Coupon?"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Code")
        ar.Add("Description")
        ar.Add("IsActiveString")

        objStorage.ColumnList = ar

        Dim objSort As CSortStorage
        ar = New ArrayList

        objSort = New CSortStorage
        objSort.ColumnName = "Code"
        objSort.DisplayName = "Coupon Code"
        ar.Add(objSort)

        objSort = New CSortStorage
        objSort.ColumnName = "Description"
        objSort.DisplayName = "Description"
        ar.Add(objSort)

        objStorage.SortList = ar

        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub LoadProducts()
        Dim objStorage As New CSearchControlStorage
        Dim objProducts As New CStoreProducts

        ' Set the display properties
        objStorage.SelectOne = True
        objStorage.ButtonID = "ProductID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objProducts.GetAllProducts()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Products"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Name")
        ar.Add("ProductCode")

        objStorage.ColumnList = ar

        StandardSearchControl21.CategoryNodes = False
        StandardSearchControl21.StorageClass = objStorage
    End Sub

    Private Sub LoadCategory()
        Dim objStorage As New CSearchControlStorage
        Dim objCategories As New CCategories

        ' Set the display properties
        objStorage.SelectOne = True
        objStorage.NoSort = True
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objCategories.GetAllCategories
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Category"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Name")

        objStorage.ColumnList = ar

        StandardSearchControl21.CategoryNodes = True
        StandardSearchControl21.StorageClass = objStorage
    End Sub

    Private Sub LoadVendor()
        Dim objStorage As New CSearchControlStorage
        Dim objVen As New CVendor

        ' Set the display properties
        objStorage.SelectOne = True
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objVen.GetAllVendorIDsAndNames
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Vendor"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Name")

        objStorage.ColumnList = ar

        StandardSearchControl21.CategoryNodes = False
        StandardSearchControl21.StorageClass = objStorage
    End Sub

    Private Sub LoadManufacturer()
        Dim objStorage As New CSearchControlStorage
        Dim objMan As New CManufacturer

        ' Set the display properties
        objStorage.SelectOne = True
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        objStorage.DataSource = objMan.GetAllManufacturerIDsAndNames
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Select Manufacturer"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Name")

        objStorage.ColumnList = ar

        StandardSearchControl21.CategoryNodes = False
        StandardSearchControl21.StorageClass = objStorage
    End Sub
#End Region

#Region "Sub btnBackSelect_Click"
    Private Sub btnBackSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackSelect.Click
        AdminTabControl1.Visible = True
        Addcoupon1.Visible = True
        StandardSearchControl21.Visible = False
        btnBackSelect.Visible = False
        btnSaveSelect.Visible = False
        btnEditBackSelect.Visible = False
        btnEditSaveSelect.Visible = False
        imgBackSelect.Visible = False
        imgSaveSelect.Visible = False
        imgEditBackSelect.Visible = False
        imgEditSaveSelect.Visible = False
    End Sub
#End Region

#Region "Sub btnSaveSelect_Click"
    Private Sub btnSaveSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSelect.Click
        If (IsNothing(Session("ArrChecked")) = False) Then
            If (Session("ArrChecked").Count > 0) Then
                Addcoupon1.ApplyToID = "" & CType(Session("ArrChecked"), ArrayList).Item(0)
            Else
                Addcoupon1.ApplyToID = "0"
            End If
        Else
            Addcoupon1.ApplyToID = "0"
        End If
        AdminTabControl1.Visible = True
        Addcoupon1.Visible = True
        StandardSearchControl21.Visible = False
        btnBackSelect.Visible = False
        btnSaveSelect.Visible = False
        btnEditBackSelect.Visible = False
        btnEditSaveSelect.Visible = False
        imgBackSelect.Visible = False
        imgSaveSelect.Visible = False
        imgEditBackSelect.Visible = False
        imgEditSaveSelect.Visible = False
    End Sub
#End Region

#Region "Sub btnEditBackSelect_Click"
    Private Sub btnEditBackSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditBackSelect.Click
        AdminTabControl1.Visible = True
        Editcoupon1.Visible = True
        StandardSearchControl21.Visible = False
        btnBackSelect.Visible = False
        btnSaveSelect.Visible = False
        btnEditBackSelect.Visible = False
        btnEditSaveSelect.Visible = False
        imgBackSelect.Visible = False
        imgSaveSelect.Visible = False
        imgEditBackSelect.Visible = False
        imgEditSaveSelect.Visible = False
    End Sub
#End Region

#Region "Sub btnEditSaveSelect_Click"
    Private Sub btnEditSaveSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSaveSelect.Click
        If (IsNothing(Session("ArrChecked")) = False) Then
            If (Session("ArrChecked").Count > 0) Then
                Editcoupon1.ApplyToID = "" & CType(Session("ArrChecked"), ArrayList).Item(0)
            Else
                Editcoupon1.ApplyToID = "0"
            End If
        Else
            Editcoupon1.ApplyToID = "0"
        End If
        AdminTabControl1.Visible = True
        Editcoupon1.Visible = True
        StandardSearchControl21.Visible = False
        btnBackSelect.Visible = False
        btnSaveSelect.Visible = False
        btnEditBackSelect.Visible = False
        btnEditSaveSelect.Visible = False
        imgBackSelect.Visible = False
        imgSaveSelect.Visible = False
        imgEditBackSelect.Visible = False
        imgEditSaveSelect.Visible = False
    End Sub
#End Region
End Class
