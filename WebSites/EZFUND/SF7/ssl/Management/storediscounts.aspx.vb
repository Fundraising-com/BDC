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

Partial Class storediscounts
    Inherits CWebPage

    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents StandardSearchControl21 As StandardSearchControl2
    Protected WithEvents AdminTabControl1 As AdminTabControl
    Protected WithEvents Editdiscount1 As editdiscount
    Protected WithEvents Adddiscount1 As adddiscount

    Protected WithEvents Freeshipping1 As freeshipping
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
        If MyBase.RestrictedPages(Tasks.StorewideDiscounts) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Adddiscount1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(Editdiscount1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")
            CType(Freeshipping1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationFreeShipping();")

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            LoadTabs()

        Catch ex As Exception
            Session("DetailError") = "Class StoreDiscounts Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

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
        Adddiscount1.Visible = False
        Editdiscount1.Visible = False
        Freeshipping1.Visible = False

        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region

    Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
        If (sender = "0") Then
            LoadSearch()
            StandardSearchControl1.ReloadList()
            MakeVisible(StandardSearchControl1)
        ElseIf (sender = "1") Then
            MakeVisible(Adddiscount1)
            Adddiscount1.ClearFields()
        ElseIf (sender = "2") Then
            Freeshipping1.LoadFreeShipping()
            MakeVisible(Freeshipping1)
        End If
    End Sub

#Region "Load Routines"
    Private Sub LoadTabs()
        ' Set the Tab Elements
        Dim ar As New ArrayList
        ar = New ArrayList
        ar.Add("Manage Store Discounts")
        ar.Add("Add Store Discounts")
        ar.Add("Free Shipping")

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
        arDis = objCerts.DiscountItems

        For Each objDiscount In arDis
            If (objDiscount.Name = "FreeShipping") Then
                arDis.Remove(objDiscount)
                Exit For
            End If
        Next

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = False
        objStorage.DataSource = arDis
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Store Discounts"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Store Discount?"

        ' Add the Columns
        Dim ar As New ArrayList
        ar.Add("Description")
        ar.Add("IsActiveString")

        objStorage.ColumnList = ar

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

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        Dim obj As New CStoreDiscounts

        Dim objCerts As CDiscounts = obj.GetAllDiscounts
        Dim objDiscount As CDiscount
        Dim arDis As New ArrayList
        arDis = objCerts.DiscountItems

        For Each objDiscount In arDis
            If (objDiscount.ID = sender) Then
                Editdiscount1.EditDiscount = objDiscount
                Exit For
            End If
        Next
        MakeVisible(Editdiscount1)
    End Sub

    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        Dim obj As New CStoreDiscounts
        obj.DeleteDiscounts(sender)
        LoadSearch()
        StandardSearchControl1.ReloadList()
    End Sub

    Private Sub Editdiscount1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editdiscount1.Save
        AdminTabControl1.SelectedTabIndex = 0
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub

    Private Sub Adddiscount1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Adddiscount1.Save
        AdminTabControl1.SelectedTabIndex = 0
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub

    Private Sub Freeshipping1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Freeshipping1.Save
        AdminTabControl1.SelectedTabIndex = 0
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub

    Private Sub Adddiscount1_DiscountSelect(ByVal sender As Object, ByVal e As System.EventArgs) Handles Adddiscount1.DiscountSelect
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        Session("ArrChecked") = New ArrayList

        If (CLng("0" & Adddiscount1.ApplyToID) > 0) Then
            Session("ArrChecked").Add(CLng(Adddiscount1.ApplyToID))
        End If

        If (sender = 1) Then
            LoadCategory()
        ElseIf (sender = 2) Then
            LoadManufacturer()
        ElseIf (sender = 3) Then
            LoadVendor()
        End If

        Adddiscount1.Visible = False
        AdminTabControl1.Visible = False
        StandardSearchControl1.Visible = False
        StandardSearchControl21.ReloadList()
        StandardSearchControl21.Visible = True
        btnBackSelect.Visible = True
        btnSaveSelect.Visible = True
        imgBackSelect.Visible = True
        imgSaveSelect.Visible = True
    End Sub

    Private Sub Editdiscount1_DiscountSelect(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editdiscount1.DiscountSelect
        'Set Checked Items in EditCoupon
        Session("ArrChecked") = New ArrayList
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        If (CLng("0" & Editdiscount1.ApplyToID) > 0) Then
            Session("ArrChecked").Add(CLng(Editdiscount1.ApplyToID))
        End If

        If (sender = 1) Then
            LoadCategory()
        ElseIf (sender = 2) Then
            LoadManufacturer()
        ElseIf (sender = 3) Then
            LoadVendor()
        End If

        Editdiscount1.Visible = False
        AdminTabControl1.Visible = False
        StandardSearchControl1.Visible = False
        StandardSearchControl21.ReloadList()
        StandardSearchControl21.Visible = True
        btnEditBackSelect.Visible = True
        btnEditSaveSelect.Visible = True
        imgEditBackSelect.Visible = True
        imgEditSaveSelect.Visible = True
    End Sub

    Private Sub btnEditSaveSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSaveSelect.Click
        If (IsNothing(Session("ArrChecked")) = False) Then
            If (Session("ArrChecked").Count > 0) Then
                Editdiscount1.ApplyToID = "" & CType(Session("ArrChecked"), ArrayList).Item(0)
            Else
                Editdiscount1.ApplyToID = "0"
            End If
        Else
            Editdiscount1.ApplyToID = "0"
        End If
        AdminTabControl1.Visible = True
        Editdiscount1.Visible = True
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

    Private Sub btnEditBackSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditBackSelect.Click
        AdminTabControl1.Visible = True
        Editdiscount1.Visible = True
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

    Private Sub btnSaveSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSelect.Click
        If (IsNothing(Session("ArrChecked")) = False) Then
            If (Session("ArrChecked").Count > 0) Then
                Adddiscount1.ApplyToID = "" & CType(Session("ArrChecked"), ArrayList).Item(0)
            Else
                Adddiscount1.ApplyToID = "0"
            End If
        Else
            Adddiscount1.ApplyToID = "0"
        End If
        AdminTabControl1.Visible = True
        Adddiscount1.Visible = True
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

    Private Sub btnBackSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackSelect.Click
        AdminTabControl1.Visible = True
        Adddiscount1.Visible = True
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
End Class
