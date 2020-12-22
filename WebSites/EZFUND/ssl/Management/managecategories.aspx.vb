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

Public Class managecategories
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents AddCategoryControl1 As addcategory
    Protected WithEvents EditCategoryControl1 As editcategory
    Protected WithEvents CategoryControl1 As categorycontrol
    Protected WithEvents AdminTabControl1 As AdminTabControl
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
    Protected objCategory As CXMLCategoryList

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
            CType(AddCategoryControl1.FindControl("cmdAdd"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(EditCategoryControl1.FindControl("cmdAdd"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")
            'Put user code to initialize the page here
            Dim ar As New ArrayList()
            objCategory = New CXMLCategoryList()

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            ' MakeVisible(AddCategoryControl1)
            If Not (IsPostBack) Then
                MakeVisible(CategoryControl1)
                Me.btnAdd.Visible = True
                Me.imgAdd.Visible = True
            End If

            'ar.Add("Manage Categories")
            'ar.Add("Add Category")

            'AdminTabControl1.BorderClass = "ContentTable"
            'AdminTabControl1.TabItemClass = "Content"
            'AdminTabControl1.TabStringArray = ar

        Catch ex As Exception
            Session("DetailError") = "Class ManageCategories Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Sub EditSave(ByVal sender As Object, ByVal e As EventArgs) Handles EditCategoryControl1.Save"
    Public Sub EditSave(ByVal sender As Object, ByVal e As EventArgs) Handles EditCategoryControl1.Save
        CategoryControl1.Reload()
        MakeVisible(CategoryControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.TabClick"
    'Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
    '    If (sender = "0") Then
    '        'Manage Manufacturers
    '        MakeVisible(CategoryControl1)
    '        'lblErrorMessage.Visible = False
    '    ElseIf (sender = "1") Then
    '        ' Add Manacturers
    '        Me.AddCategoryControl1.ClearFields()
    '        MakeVisible(AddCategoryControl1)
    '        'lblErrorMessage.Visible = False
    '    End If
    'End Sub
#End Region

#Region "Sub EditCancel(ByVal sender As Object, ByVal e As EventArgs) Handles EditCategoryControl1.Cancel"
    Public Sub EditCancel(ByVal sender As Object, ByVal e As EventArgs) Handles EditCategoryControl1.Cancel
        MakeVisible(CategoryControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub AddSave(ByVal sender As Object, ByVal e As EventArgs) Handles AddCategoryControl1.Save"
    Public Sub AddSave(ByVal sender As Object, ByVal e As EventArgs) Handles AddCategoryControl1.Save
        CategoryControl1.Reload()
        MakeVisible(CategoryControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub AddCancel(ByVal sender As Object, ByVal e As EventArgs) Handles AddCategoryControl1.Cancel"
    Public Sub AddCancel(ByVal sender As Object, ByVal e As EventArgs) Handles AddCategoryControl1.Cancel
        MakeVisible(CategoryControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub
#End Region

#Region "Sub CategoryControlEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Edit"
    Public Sub CategoryControlEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Edit
        objCategory = CType(sender, CXMLCategoryList)
        EditCategoryControl1.FillFields(objCategory)
        MakeVisible(EditCategoryControl1)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
#End Region

#Region "Sub CategoryControlAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Add"
    Public Sub CategoryControlAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Add

        AddCategoryControl1.FillFields(CType(sender, CXMLCategoryList))
        MakeVisible(AddCategoryControl1)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
#End Region

#Region "Sub CategoryControlDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Delete"
    Public Sub CategoryControlDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryControl1.Delete
        CategoryControl1.Reload()
        MakeVisible(CategoryControl1)
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
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
        CategoryControl1.Visible = False
        AddCategoryControl1.Visible = False
        EditCategoryControl1.Visible = False

        'Set the object that is passed to the function to visible
        _type = obj.GetType
        _type.InvokeMember("Visible", Reflection.BindingFlags.SetProperty, Nothing, obj, New Object() {True}, Nothing)
    End Sub
#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Me.AddCategoryControl1.ClearFields()
        MakeVisible(AddCategoryControl1)
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub


End Class
