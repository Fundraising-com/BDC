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
Option Explicit On 
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

Partial Class managevendors
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents AddVendorControl As AddVendorControl
    Protected WithEvents EditVendorControl As EditVendorControl
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
        If MyBase.RestrictedPages(Tasks.Vendors) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(AddVendorControl.FindControl("cmdAdd"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(EditVendorControl.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True


            If Not IsPostBack() Then LoadSearch()

            MakeVisible(StandardSearchControl1)
        Catch ex As Exception
            Session("DetailError") = "Class ManageVendors Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Sub LoadSearch()"
    Private Sub LoadSearch()
        Dim objStorage As CSearchControlStorage
        Dim objVendor As New CVendor
        'fill customer search control

        Dim arVend As New ArrayList
        Dim tempVend As Address

        objStorage = New CSearchControlStorage
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = False
        'objStorage.SortList = arSort

        objVendor = New CVendor
        For Each tempVend In objVendor.GetAllVendors
            If Not (tempVend.Company.ToLower = "no vendor" Or tempVend.Company = "") Then
                arVend.Add(tempVend)
            End If
        Next
        objStorage.DataSource = arVend
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Vendors"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Vendor?"

        'Fields to add to the control
        Dim ar As New ArrayList
        ar.Clear()
        ar.Add("Company")
        objStorage.ColumnList = ar
        StandardSearchControl1.StorageClass = objStorage
    End Sub
#End Region

#Region "Sub AddVendor()"
    Public Sub AddVendor(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddVendorControl.Save
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick"
    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        Dim objVendor As New CVendor
        objVendor.DeleteVendor(CLng(sender))
        LoadSearch()
        StandardSearchControl1.ReloadList()
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick"
    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        Dim objVendor As New CVendor
        'txtGroupIDHidden.Value = CStr(sender)
        'lblErrorMessage.Visible = False
        objVendor.GetAddressByID(CLng(sender))
        EditVendorControl.FillForm(objVendor)
        MakeVisible(EditVendorControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
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
        AddVendorControl.Visible = False
        EditVendorControl.Visible = False

        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region

#Region "Sub UpdateVendor(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditVendorControl.Save"
    Public Sub UpdateVendor(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditVendorControl.Save
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub EditVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles EditVendorControl.editVisible"
    Public Sub EditVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles EditVendorControl.editVisible
        MakeVisible(EditVendorControl)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
#End Region

#Region "Sub AddVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles AddVendorControl.addVisible"
    Public Sub AddVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles AddVendorControl.addVisible

        Me.LoadSearch()
        Me.StandardSearchControl1.ReloadList()
        Me.MakeVisible(Me.StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddVendorControl.ClearFields()
        MakeVisible(AddVendorControl)
        'lblErrorMessage.Visible = False
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
        'Me.MakeVisible(Me.StandardSearchControl1)

    End Sub

End Class
