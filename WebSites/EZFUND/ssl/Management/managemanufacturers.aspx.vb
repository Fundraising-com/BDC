'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Public Class managemanufacturers
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents EditManufacturerControl1 As editmanufacturer
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents AddManufacturerControl1 As addmanufacturer
    'Protected WithEvents AdminTabControl1 As AdminTabControl
    Protected objStorage As CSearchControlStorage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
    Protected objManufacturer As CManufacturer
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

        Try
            CType(AddManufacturerControl1.FindControl("cmdAdd"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(EditManufacturerControl1.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")
            'Put user code to initialize the page here
            objStorage = New CSearchControlStorage()

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            LoadSearch()
            MakeVisible(StandardSearchControl1)
        Catch ex As Exception
            Session("DetailError") = "Class ManageManufacturers Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Sub LoadSearch()"
    Public Sub LoadSearch()
        Dim arMan As New ArrayList()
        Dim tempMan As Address
        'fill customer search control
        objManufacturer = New CManufacturer()
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = False
        'objStorage.SortList = arSort
        'arMan = objManufacturer.GetAllManufacturers()
        For Each tempMan In objManufacturer.GetAllManufacturers()
            If Not (tempMan.Company = "No Manufacturer" Or tempMan.Company = "") Then
                arMan.Add(tempMan)
            End If
        Next
        objStorage.DataSource = arMan
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Manufacturers"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Manufacturer?"

        'Fields to add to the control
        Dim ar As New ArrayList()
        ar.Clear()
        ar.Add("Company")
        objStorage.ColumnList = ar
        StandardSearchControl1.StorageClass = objStorage
    End Sub
#End Region


#Region "Sub EditCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditManufacturerControl1.Cancel"
    Public Sub EditCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditManufacturerControl1.Cancel
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub EditSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditManufacturerControl1.Save"
    Public Sub EditSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles EditManufacturerControl1.Save
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub AddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddManufacturerControl1.Save"
    Public Sub AddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddManufacturerControl1.Save
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Public Sub AddCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddManufacturerControl1.Cancel"
    Public Sub AddCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddManufacturerControl1.Cancel
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick"
    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        'objVendor.DeleteVendor(CLng(sender))
        'objManufacturer.DeleteManufacturer(CLng(sender))
        objManufacturer.DeleteManufacturer(CLng(sender))
        LoadSearch()
        StandardSearchControl1.ReloadList()
        MakeVisible(StandardSearchControl1)
    End Sub
#End Region

#Region "Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick"
    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        'lblErrorMessage.Visible = False
        objManufacturer.GetManufacturerByAddressID(CLng(sender))
        EditManufacturerControl1.FillForm(objManufacturer)
        MakeVisible(EditManufacturerControl1)
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
        AddManufacturerControl1.Visible = False
        EditManufacturerControl1.Visible = False

        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region

#Region "Sub AddVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles AddManufacturerControl1.AddVisible"
    Public Sub AddVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles AddManufacturerControl1.AddVisible
        'MakeVisible(AddManufacturerControl1)
        'Me.btnAdd.Visible = False
        Me.LoadSearch()
        Me.StandardSearchControl1.ReloadList()
        Me.MakeVisible(Me.StandardSearchControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True

    End Sub
#End Region

#Region "Sub EditVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles EditManufacturerControl1.editVisible"
    Public Sub EditVisibleEvent(ByVal sender As Object, ByVal e As EventArgs) Handles EditManufacturerControl1.editVisible
        MakeVisible(EditManufacturerControl1)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddManufacturerControl1.ClearFields()
        MakeVisible(AddManufacturerControl1)
        'lblErrorMessage.Visible = False
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
End Class
