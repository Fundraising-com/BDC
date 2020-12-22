'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO
Option Explicit On 
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml

Partial Class managepricegroups
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TabControl1 As AdminTabControl
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents StandardSearchControl2 As StandardSearchControl
    Protected WithEvents AddPriceGroupControl As addpricegroups
    Protected WithEvents EditPriceGroupControl As editpricegroup
    Protected WithEvents lnkManagePriceGroups As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkAddPriceGroups As System.Web.UI.WebControls.LinkButton
    Protected objPriceGroup As CPriceGroup
    ' begin: JDB - product configurator bug 74
    Protected Overrides ReadOnly Property PreserveSessionSearch() As Boolean
        Get
            Return True
        End Get
    End Property
    ' end: JDB - product configurator bug 74

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.PriceGroups) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            CType(AddPriceGroupControl.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnSelect"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnPriceChoice"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            If Not IsPostBack Then
                LoadCustomerSearch()
                LoadPriceSearch()
            End If
            MakeVisible(StandardSearchControl2)

            If (IsPostBack = False) And (Request.QueryString("Edit")) <> "" Then
                objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
                EditPriceGroupControl.FillValues(objPriceGroup)
                SaveMe()
                MakeVisible(EditPriceGroupControl)
                Me.btnAdd.Visible = False
                Me.imgAdd.Visible = False
                'ElseIf (IsPostBack = True) And (Session("EditClick") > 0) Then
                'objPriceGroup = New CPriceGroup(CLng(Me.txtGroupIDHidden.Value))
                'EditPriceGroupControl.FillValues(objPriceGroup)
                '    MakeVisible(EditPriceGroupControl)
                '    Me.btnAdd.Visible = False
                '    Me.imgAdd.Visible = False
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ManagePriceGroups Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub ShowEdit(ByVal sender As Object, ByVal e As EventArgs) Handles EditPriceGroupControl.Edit
        If Not (EditPriceGroupControl.ErrorMessage = "") Then
            lblErrorMessage.Text = CStr(sender)
            lblErrorMessage.Visible = True
        End If
        MakeVisible(EditPriceGroupControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    Public Sub ShowAdd(ByVal sender As Object, ByVal e As EventArgs) Handles AddPriceGroupControl.Add
        If Not (AddPriceGroupControl.ErrorMessage = "") Then
            lblErrorMessage.Text = AddPriceGroupControl.ErrorMessage
            lblErrorMessage.Visible = True
        End If
        MakeVisible(Me.AddPriceGroupControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
    Private Sub LoadCustomerSearch()
        'fill customer search control
        Dim objStorage As New CSearchControlStorage
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = True
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = False
        objStorage.Sorting = False
        'objStorage.SortList = arSort
        objStorage.DataSource = m_objCustomer.GetCustomers()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Customers"
        objStorage.DeleteMessage = "Are you sure you want to delete this customer?"

        'Fields to add to the control
        Dim ar As New ArrayList
        ar.Clear()
        ar.Add("LastName")
        ar.Add("FirstName")

        objStorage.ColumnList = ar
        StandardSearchControl1.StorageClass = objStorage
    End Sub
    Private Sub LoadPriceSearch()
        Dim objStorage2 As New CSearchControlStorage
        'fill Price Group search control
        Me.objPriceGroup = New CPriceGroup
        objStorage2.ButtonID = "ID"
        objStorage2.CheckBoxColumn = False
        objStorage2.ContentClass = "Content"
        objStorage2.Paging = True
        objStorage2.ShowButtons = True
        objStorage2.Sorting = False
        'objStorage2.SortList = arSort
        objStorage2.DataSource = Me.objPriceGroup.GetPriceGroups()
        objStorage2.PagerClass = "ContentTableHeader"
        objStorage2.TitleClass = "ContentTableHeader"
        objStorage2.TitleString = "Pricing Groups"
        objStorage2.DeleteMessage = "Are You Sure You Want to Delete This Price Group?"

        'Fields to add to the control
        Dim ar2 As New ArrayList
        ar2.Clear()
        ar2.Add("Name")
        objStorage2.ColumnList = ar2
        StandardSearchControl2.StorageClass = objStorage2
    End Sub

    Private Sub StandardSearchControl2_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl2.DeleteClick
        objPriceGroup = New CPriceGroup
        objPriceGroup.DeletePriceGroup(CLng(sender))
        LoadPriceSearch()
        StandardSearchControl2.ReloadList()
        MakeVisible(StandardSearchControl2)
        'Tee 2/13/2008 bug 1119 fix
        If Session("GroupID") = CLng(sender) Then
            Session.Remove("GroupID")
        End If
        'end Tee
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Private Sub StandardSearchControl2_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl2.EditClick
        Session("EditClick") = "1"
        txtGroupIDHidden.Value = CStr(sender)
        EditPriceGroupControl.FillValues(New CPriceGroup(CLng(sender)))
        lblErrorMessage.Visible = False
        MakeVisible(EditPriceGroupControl)

        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    Public Sub SavePriceGroup(ByVal sender As Object, ByVal e As EventArgs) Handles EditPriceGroupControl.Save
        Me.txtGroupIDHidden.Value = CStr(CType(sender, CPriceGroup).ID)
        Session("GroupID") = txtGroupIDHidden.Value
        Dim arr As New ArrayList
        ' begin: JDB - 7/10/2007 - Customer Price Groups
        ' hack: price groups are saved several times during the editing process.  in order to maintain the new values
        '   in the categories, manufacturers and vendors tables, this save routine needs to run every time, not just
        '   for saving all products.
        'If Session("ApplyTo") = "0" Then
        'Tee 2/14/2008 bug 1018 fix
        'SaveMe()
        'end Tee
        'End If
        ' end: JDB - 7/10/2007 - Customer Price Groups
        Session("NewCustomerID") = Nothing
        Session("NewCustomer") = Nothing
        Session("groupID") = Nothing
        Session("ApplyToID") = Nothing
        Session("ApplyTo") = Nothing
        LoadPriceSearch()
        StandardSearchControl2.ReloadList()
        MakeVisible(StandardSearchControl2)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Public Sub SaveNewPriceGroup(ByVal sender As Object, ByVal e As EventArgs) Handles AddPriceGroupControl.Save
        objPriceGroup = New CPriceGroup
        txtGroupIDHidden.Value = objPriceGroup.GetIDByName(CType(sender, CPriceGroup).Name)
        CType(sender, CPriceGroup).ID = txtGroupIDHidden.Value
        EditPriceGroupControl.FillValues(CType(sender, CPriceGroup))
        LoadPriceSearch()
        StandardSearchControl2.ReloadList()
        MakeVisible(EditPriceGroupControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    Public Sub SelectMembers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddPriceGroupControl.SelectMembers
        txtGroupIDHidden.Value = CStr(CType(sender, CPriceGroup).ID)
        lblErrorMessage.Visible = False
        'MakeVisible(StandardSearchControl1)
        Session("NewCustomer") = Session("ApplyToID")
        MakeVisible(EditPriceGroupControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    Public Sub chkBoxChecked(ByVal sender As Object, ByVal e As EventArgs) Handles StandardSearchControl1.CheckClick
        'StandardSearchControl1.FindControl(CType(StandardSearchControl1.datagrid2.chk & sender, CheckBox)).checked()
        objPriceGroup = New CPriceGroup
        Me.objPriceGroup.AddCustomerToPriceGroup(CLng(sender), txtGroupIDHidden.Value)
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Public Sub MakeVisible(ByVal obj As Object)
        'Dim objData As Object
        Dim _type As Type
        StandardSearchControl1.Visible = False
        StandardSearchControl2.Visible = False
        AddPriceGroupControl.Visible = False
        EditPriceGroupControl.Visible = False
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        MakeVisible(AddPriceGroupControl)
        lblErrorMessage.Visible = False
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    Private Sub SaveMe()
        Dim arr As New ArrayList
        ' begin: JDB - 7/10/2007 - Customer Price Groups
        If Session("ApplyTo") = 1 Then
            ' Selected Products
            Me.objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            Me.EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(Me.EditPriceGroupControl)
            'arr.Add(CLng(Session("ApplyToID")))
            'Tee 14/2/2008 bug 1018 fix
            objPriceGroup.CleanUpCustPriceGroup()
            'end Tee
            If (IsNothing(Session("ArrChecked")) = False) Then
                objPriceGroup.AssignToProducts(Session("ArrChecked"), objPriceGroup.ID)
            End If
        ElseIf Session("ApplyTo") = "2" Then
            ' Categories
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            Me.EditPriceGroupControl.FillValues(objPriceGroup)
            'objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            'Tee 14/2/2008 bug 1018 fix
            objPriceGroup.CleanUpCustPriceGroup()
            'end Tee
            If (IsNothing(Session("ArrChecked")) = False) Then
                Dim nCat As String
                For Each nCat In Session("ArrChecked")
                    objPriceGroup.AssignToCategory(CLng(nCat), CLng(Session("groupID")))
                Next
            End If
            MakeVisible(EditPriceGroupControl)
            arr.Add(CLng(Session("ApplyToID")))
        ElseIf Session("ApplyTo") = "3" Then
            ' Vendors
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(EditPriceGroupControl)
            'objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            'Tee 14/2/2008 bug 1018 fix
            objPriceGroup.CleanUpCustPriceGroup()
            'end Tee
            If (IsNothing(Session("ArrChecked")) = False) Then
                '1344
                Dim nVendor As String
                For Each nVendor In Session("ArrChecked")
                    'objPriceGroup.AssignToManufacturer(objPriceGroup.ID, CLng(nManuf))
                    objPriceGroup.AssignToVendors(objPriceGroup.ID, CLng(nVendor))
                Next
            End If
        ElseIf Session("ApplyTo") = "4" Then
            ' Manufacturers
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(EditPriceGroupControl)
            'objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            '1344
            'Tee 14/2/2008 bug 1018 fix
            objPriceGroup.CleanUpCustPriceGroup()
            'end Tee
            Dim nManufacturer As Long
            If (IsNothing(Session("ArrChecked")) = False) Then
                For Each nManufacturer In Session("ArrChecked")
                    'objPriceGroup.AssignToVendors(objPriceGroup.ID, nVend)
                    objPriceGroup.AssignToManufacturer(objPriceGroup.ID, CLng(nManufacturer))
                Next
            End If
        ElseIf Session("ApplyTo") = "0" Then
            ' All Products
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            objPriceGroup.AssignToAllProducts(CLng(Session("groupID")))
        ElseIf Session("ApplyTo") = "100" Then
            ' Customers
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            Session("NewCustomerID") = Session("ArrChecked")
        End If
        ' end: JDB - 7/10/2007 - Customer Price Groups
        If (IsNothing(Session("NewCustomerID")) = False) Then
            If CType(Session("NewCustomerID"), ArrayList).Count > 0 Then
                Dim tempCustomer As New SystemBase.Customer
                Dim counter As String
                arr = m_objcustomer.GetCustomerByPriceGroup(Session("groupID"))
                For Each counter In arr
                    m_objcustomer.FillCustomer(tempCustomer, CLng(counter))
                    tempCustomer.CustomerGroup = 0
                    m_objcustomer.UpdateCustomer(tempCustomer)
                Next
                For Each counter In Session("NewCustomerID")
                    Me.m_objCustomer.FillCustomer(tempCustomer, CLng(counter))
                    tempCustomer.CustomerGroup = CLng(Session("groupID"))
                    m_objcustomer.UpdateCustomer(tempCustomer)
                Next
            End If
        End If
        Session("NewCustomerID") = Nothing
        Session("NewCustomer") = Nothing
        Session("ApplyToID") = Nothing
    End Sub



End Class
