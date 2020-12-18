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

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule

Imports StoreFront.SystemBase
Public Class ManageProducts
    Inherits CWebPage
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents txtGroupIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents StandardSearchLive1 As StandardSearchLive

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

	   Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            Me.lblErrorMessage.Visible = False
            If (IsPostBack = False) Then
                LoadProducts()
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ManageProducts Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#End Region

#Region "Private Sub LoadProducts()"

	   Private Sub LoadProducts()
        Dim objStorage As New CSearchControlStorage()
        Dim objProducts As New CStoreProducts()
        StandardSearchLive1.DeleteMessage = "Are you sure you want to delete this product?"
    End Sub

#End Region

#Region "Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click"

	  Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("ProductID") = Nothing
        Response.Redirect("productgeneral.aspx")
    End Sub

#End Region

#Region "Private Sub StandardSearchLive1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.DeleteClick"

	   Private Sub StandardSearchLive1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.DeleteClick
        Dim objProductManagement As New CProductManagement(CLng(sender))
        objProductManagement.DeleteProduct()
        LoadProducts()
        StandardSearchLive1.ReloadList()
    End Sub

#End Region

#Region "Private Sub StandardSearchLive1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.EditClick"

	 Private Sub StandardSearchLive1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.EditClick
        Session("ProductID") = CLng(sender)
        Response.Redirect("productgeneral.aspx")
    End Sub

#End Region

#Region "Private Sub StandardSearchLive1_EmptyResults(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.EmptyResults"

	 Private Sub StandardSearchLive1_EmptyResults(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.EmptyResults
        Me.lblErrorMessage.Text = "No Products Found!"
        Me.lblErrorMessage.Visible = True
    End Sub

#End Region

End Class
