'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml

Public Class managepricegroups
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TabControl1 As AdminTabControl
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents StandardSearchControl2 As StandardSearchControl
    Protected WithEvents AddPriceGroupControl As addpricegroups
    Protected WithEvents EditPriceGroupControl As editpricegroup
    Protected txtGroupIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected objStorage2 As CSearchControlStorage
    Protected objStorage As CSearchControlStorage
    Protected WithEvents lnkManagePriceGroups As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkAddPriceGroups As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
    Protected objPriceGroup As CPriceGroup

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Put user code to initialize the page here
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            objStorage = New CSearchControlStorage()
            objStorage2 = New CSearchControlStorage()

            'Dim tabArray As New ArrayList()
            'tabArray.Clear()
            'tabArray.Add("Manage Pricing Groups")
            'tabArray.Add("Add Pricing Groups")

            'TabControl1.BorderClass = "ContentTable"
            'TabControl1.TabItemClass = "Content"
            'TabControl1.TabStringArray = tabArray
            CType(AddPriceGroupControl.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnSelect"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditPriceGroupControl.FindControl("btnPriceChoice"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            LoadSearch()
            MakeVisible(StandardSearchControl2)

            If (IsPostBack = False) And (Request.QueryString("Edit")) <> "" Then
                objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
                EditPriceGroupControl.FillValues(objPriceGroup)
                SaveMe()
                MakeVisible(EditPriceGroupControl)
                Me.btnAdd.Visible = False
                Me.imgAdd.Visible = False
            ElseIf (IsPostBack = True) And (Session("EditClick") > 0) Then
                'objPriceGroup = New CPriceGroup(CLng(Me.txtGroupIDHidden.Value))
                'EditPriceGroupControl.FillValues(objPriceGroup)
                MakeVisible(EditPriceGroupControl)
                Me.btnAdd.Visible = False
                Me.imgAdd.Visible = False
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

    Private Sub LoadSearch()
        'fill customer search control

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
        Dim ar As New ArrayList()
        ar.Clear()
        ar.Add("LastName")
        ar.Add("FirstName")

        objStorage.ColumnList = ar
        StandardSearchControl1.StorageClass = objStorage

        'fill Price Group search control
        Me.objPriceGroup = New CPriceGroup()
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
        Dim ar2 As New ArrayList()
        ar2.Clear()
        ar2.Add("Name")
        objStorage2.ColumnList = ar2
        StandardSearchControl2.StorageClass = objStorage2
    End Sub

    'Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabClick
    '    If (sender = "0") Then
    '        'Manage Customers
    '        LoadSearch()
    '        StandardSearchControl2.ReloadList()
    '        MakeVisible(StandardSearchControl2)
    '        lblErrorMessage.Visible = False
    '    ElseIf (sender = "1") Then
    '        ' Add Customers
    '        MakeVisible(AddPriceGroupControl)
    '        lblErrorMessage.Visible = False
    '    End If
    'End Sub

    Private Sub StandardSearchControl2_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl2.DeleteClick
        objPriceGroup.DeletePriceGroup(CLng(sender))
        LoadSearch()
        StandardSearchControl2.ReloadList()
        MakeVisible(StandardSearchControl2)

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
        Dim arr As New ArrayList()
        If Session("ApplyTo") = "0" Then
            SaveMe()
        End If
        'If Session("ApplyTo") = 1 Then
        '    Me.objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    Me.EditPriceGroupControl.FillValues(objPriceGroup)
        '    MakeVisible(Me.EditPriceGroupControl)
        '    'arr.Add(CLng(Session("ApplyToID")))
        '    If (IsNothing(Session("ArrChecked")) = False) Then
        '        objPriceGroup.AssignToProducts(Session("ArrChecked"), objPriceGroup.ID)
        '    End If
        'ElseIf Session("ApplyTo") = "2" Then
        '    objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    Me.EditPriceGroupControl.FillValues(objPriceGroup)
        '    objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
        '    Dim nCat As String
        '    For Each nCat In Session("ArrChecked")
        '        objPriceGroup.AssignToCategory(CLng(nCat), CLng(Session("groupID")))
        '    Next
        '    MakeVisible(EditPriceGroupControl)
        '    arr.Add(CLng(Session("ApplyToID")))
        'ElseIf Session("ApplyTo") = "3" Then
        '    objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    EditPriceGroupControl.FillValues(objPriceGroup)
        '    MakeVisible(EditPriceGroupControl)
        '    objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
        '    Dim nManuf As String
        '    For Each nManuf In Session("ArrChecked")
        '        objPriceGroup.AssignToManufacturer(objPriceGroup.ID, CLng(nManuf))
        '    Next
        'ElseIf Session("ApplyTo") = "4" Then
        '    objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    EditPriceGroupControl.FillValues(objPriceGroup)
        '    MakeVisible(EditPriceGroupControl)
        '    objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
        '    Dim nVend As Long
        '    For Each nVend In Session("ArrChecked")
        '        objPriceGroup.AssignToVendors(objPriceGroup.ID, nVend)
        '    Next
        'ElseIf Session("ApplyTo") = "0" Then
        '    objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    objPriceGroup.AssignToAllProducts(CLng(Session("groupID")))
        'ElseIf Session("ApplyTo") = "100" Then
        '    objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
        '    Session("NewCustomerID") = Session("ArrChecked")
        'End If
        'If (IsNothing(Session("NewCustomerID")) = False) Then
        '    If CType(Session("NewCustomerID"), ArrayList).Count > 0 Then
        '        Dim tempCustomer As New SystemBase.Customer()
        '        Dim counter As String
        '        arr = m_objcustomer.GetCustomerByPriceGroup(Session("groupID"))
        '        For Each counter In arr
        '            m_objcustomer.FillCustomer(tempCustomer, CLng(counter))
        '            tempCustomer.CustomerGroup = 0
        '            m_objcustomer.UpdateCustomer(tempCustomer)
        '        Next
        '        For Each counter In Session("NewCustomerID")
        '            Me.m_objCustomer.FillCustomer(tempCustomer, CLng(counter))
        '            tempCustomer.CustomerGroup = CLng(Session("groupID"))
        '            m_objcustomer.UpdateCustomer(tempCustomer)
        '        Next
        '    End If
        'End If
        Session("NewCustomerID") = Nothing
        Session("NewCustomer") = Nothing
        Session("groupID") = Nothing
        Session("ApplyToID") = Nothing
        Session("ApplyTo") = Nothing
        LoadSearch()
        StandardSearchControl2.ReloadList()
        MakeVisible(StandardSearchControl2)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Public Sub SaveNewPriceGroup(ByVal sender As Object, ByVal e As EventArgs) Handles AddPriceGroupControl.Save
        txtGroupIDHidden.Value = objPriceGroup.GetIDByName(CType(sender, CPriceGroup).Name)
        CType(sender, CPriceGroup).ID = txtGroupIDHidden.Value
        EditPriceGroupControl.FillValues(CType(sender, CPriceGroup))
        LoadSearch()
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
        Me.objPriceGroup.AddCustomerToPriceGroup(CLng(sender), txtGroupIDHidden.Value)
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Public Sub MakeVisible(ByVal obj As Object)
        Dim objData As Object
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
        Dim arr As New ArrayList()
        If Session("ApplyTo") = 1 Then
            Me.objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            Me.EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(Me.EditPriceGroupControl)
            'arr.Add(CLng(Session("ApplyToID")))
            If (IsNothing(Session("ArrChecked")) = False) Then
                objPriceGroup.AssignToProducts(Session("ArrChecked"), objPriceGroup.ID)
            End If
        ElseIf Session("ApplyTo") = "2" Then
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            Me.EditPriceGroupControl.FillValues(objPriceGroup)
            objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            If (IsNothing(Session("ArrChecked")) = False) Then
                Dim nCat As String
                For Each nCat In Session("ArrChecked")
                    objPriceGroup.AssignToCategory(CLng(nCat), CLng(Session("groupID")))
                Next
            End If
            MakeVisible(EditPriceGroupControl)
            arr.Add(CLng(Session("ApplyToID")))
        ElseIf Session("ApplyTo") = "3" Then
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(EditPriceGroupControl)
            objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            If (IsNothing(Session("ArrChecked")) = False) Then
                Dim nManuf As String
                For Each nManuf In Session("ArrChecked")
                    objPriceGroup.AssignToManufacturer(objPriceGroup.ID, CLng(nManuf))
                Next
            End If
        ElseIf Session("ApplyTo") = "4" Then
            objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
            EditPriceGroupControl.FillValues(objPriceGroup)
            MakeVisible(EditPriceGroupControl)
            objPriceGroup.RemoveProductsFromGroup(CLng(Session("groupID")))
            Dim nVend As Long
            If (IsNothing(Session("ArrChecked")) = False) Then
                For Each nVend In Session("ArrChecked")
                    objPriceGroup.AssignToVendors(objPriceGroup.ID, nVend)
                Next
            End If
            ElseIf Session("ApplyTo") = "0" Then
                objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
                objPriceGroup.AssignToAllProducts(CLng(Session("groupID")))
            ElseIf Session("ApplyTo") = "100" Then
                objPriceGroup = New CPriceGroup(CLng(Session("groupID")))
                Session("NewCustomerID") = Session("ArrChecked")
            End If
            If (IsNothing(Session("NewCustomerID")) = False) Then
                If CType(Session("NewCustomerID"), ArrayList).Count > 0 Then
                    Dim tempCustomer As New SystemBase.Customer()
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
        'Session("GroupID") = Nothing
            Session("ApplyToID") = Nothing
        'Session("ApplyTo") = Nothing


    End Sub

   
    
End Class
