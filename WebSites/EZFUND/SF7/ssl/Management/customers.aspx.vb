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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml

Partial Class customers
    Inherits CWebPage

#Region "Class Members"
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents trAddCustomer As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trSearchControl As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    'Protected WithEvents TabControl As AdminTabControl
    Protected WithEvents AddCustomerControl As addcustomer
    Protected WithEvents EditCustomerControl1 As editcustomercontrol
    Protected objCustomer As Customer
    Protected objStorage As CSearchControlStorage
#End Region
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
        If MyBase.RestrictedPages(Tasks.Customers) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            '#1204
            If Not IsPostBack Then LoadSearch() 'load the table
            CType(AddCustomerControl.FindControl("cmdAdd"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            CType(EditCustomerControl1.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
        Catch ex As Exception
            Session("DetailError") = "Class Customers Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub LoadSearch()
        Dim arSort As New ArrayList

        objStorage = New CSearchControlStorage
        arSort.Add("LastName")
        arSort.Add("FirstName")
        arSort.Add("Email")

        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = False
        objStorage.DataSource = m_objCustomer.GetCustomers()
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Customers"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Customer?"

        'Fields to add to the control
        Dim ar As New ArrayList
        ar.Add("LastName")
        ar.Add("FirstName")
        ar.Add("Email")
        objStorage.ColumnList = ar

        StandardSearchControl1.StorageClass = objStorage
    End Sub

    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        m_objCustomer.DeleteCustomer(CType(sender, Long))
        'objStorage.DataSource = m_objCustomer.GetCustomers()
        LoadSearch()
        StandardSearchControl1.ReloadList()

        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
    End Sub

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        objCustomer = New Customer
        Me.m_objCustomer.FillCustomer(objCustomer, CType(sender, Long))
        StandardSearchControl1.Visible = False
        AddCustomerControl.Visible = False
        EditCustomerControl1.fillValues(objCustomer)
        EditCustomerControl1.Visible = True
        lblErrorMessage.Visible = False

        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    'Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.TabClick
    '    If (sender = "0") Then
    '        'Manage Customers
    '        StandardSearchControl1.Visible = True
    '        AddCustomerControl.Visible = False
    '        EditCustomerControl1.Visible = False
    '        lblErrorMessage.Visible = False
    '    ElseIf (sender = "1") Then
    '        ' Add Customers
    '        StandardSearchControl1.Visible = False
    '        AddCustomerControl.ClearFields()
    '        AddCustomerControl.Visible = True
    '        EditCustomerControl1.Visible = False
    '        lblErrorMessage.Visible = False
    '    End If
    'End Sub

    Private Sub Add_CustomersSave(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddCustomerControl.Add
        Dim str As String = ""
        lblErrorMessage.Text = Me.AddCustomerControl.Message
        lblErrorMessage.Visible = True
        If CType(sender, Boolean) Then
            StandardSearchControl1.Visible = True
            AddCustomerControl.Visible = False
            LoadSearch()
            StandardSearchControl1.ReloadList()
            Me.btnAdd.Visible = True
            Me.imgAdd.Visible = True
        Else
            StandardSearchControl1.Visible = False
            AddCustomerControl.Visible = True
            EditCustomerControl1.Visible = False
        End If
    End Sub

    Private Sub Edit_CustomersSave(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCustomerControl1.Edit
        lblErrorMessage.Text = EditCustomerControl1.Message
        lblErrorMessage.Visible = True
        If EditCustomerControl1.Success = True Then
            AddCustomerControl.Visible = False
            EditCustomerControl1.Visible = False
            LoadSearch()
            StandardSearchControl1.ReloadList()
            StandardSearchControl1.Visible = True
            Me.btnAdd.Visible = True
            Me.imgAdd.Visible = True
        Else
            StandardSearchControl1.Visible = False
            AddCustomerControl.Visible = False
            EditCustomerControl1.Visible = True
        End If
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Me.StandardSearchControl1.Visible = False
        Me.AddCustomerControl.ClearFields()
        Me.AddCustomerControl.Visible = True
        Me.EditCustomerControl1.Visible = False
        Me.lblErrorMessage.Visible = False
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub
End Class
