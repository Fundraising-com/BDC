Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

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

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Allows viewing and editing of store settings such as search options that
'are not a part of the search or product detail templates.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class CatalogGeneral
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable

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
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        If Not IsPostBack Then
            Me.Bind()
        End If
    End Sub
    Public Sub Bind()
        Me.BindConfirmationMethod()
        Me.BindSearchEngine()

    End Sub

    Public Sub BindConfirmationMethod()
        Me.rblConfirmationType.SelectedValue = (New Management.CAdminGeneralManagement).AddProductStyle
        Me.txtConfirmation.Text = (New DesignManager).GetMessage("AddProduct", "AddToCart", "Add")
    End Sub

    Public Sub BindSearchEngine()
        Dim ds As DataSet
        Dim dt As New DataTable
        ds = (New DesignManager).GetAllSearchOptions
        dt = ds.Tables(0)
        Dim item As ListItem
        For Each item In Me.cklAdvancedSearch.Items
            item.Selected = CBool(dt.Rows(0).Item(item.Value))
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        Me.SaveConfirmationMethod()
        Me.SaveSearchEngine()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Your changes have been saved"
    End Sub

    Public Sub SaveConfirmationMethod()
        Dim admin As New Management.CAdminGeneralManagement
        admin.UpdateAddProductstyle(CInt(Me.rblConfirmationType.SelectedValue))
        Dim myMessage As New DesignManager
        myMessage.UpdateMessage("AddProduct", "AddToCart", "Add", Me.txtConfirmation.Text)
    End Sub

    Public Sub SaveSearchEngine()
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllSearchOptions
        Dim dt As DataTable = ds.Tables(0)
        Dim item As ListItem
        For Each item In Me.cklAdvancedSearch.Items
            dt.Rows(0).Item(item.Value) = item.Selected
        Next
        myDesignManager.UpdateAdvancedOptions(ds)

    End Sub

End Class
