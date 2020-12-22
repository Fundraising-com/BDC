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

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule

Imports StoreFront.SystemBase
Partial Class ManageProducts
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
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
		'begin: GJV - 9/7/2007 - Component License
        If (Request.QueryString("ProdType") = 2 OrElse Request.QueryString("ProdType") = 4) AndAlso _
            Not StoreFrontConfiguration.AreMerchantBundlesActive Then
            Response.Redirect(String.Format("{0}Management/PurchaseBundles.aspx", StoreFrontConfiguration.SSLPath))
        End If
		'end: GJV - 9/7/2007 - Component License
        'chcek if user has permission to view this page
        If MyBase.RestrictedProductPages() = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Me.lblErrorMessage.Visible = False
            If (IsPostBack = False) Then
                LoadProducts()
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ManageProducts Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        btnSelectAll.Attributes.Add("onclick", "return CheckAll('activate',1);")
        btnDeselectAll.Attributes.Add("onclick", "return CheckAll('activate',0);")

        If MyBase.RestrictedPages(Tasks.ProductGeneral) Then
            Me.btnSave.Visible = False
        End If
        If MyBase.RestrictedPages(Tasks.ProductAddnew) OrElse MyBase.RestrictedPages(Tasks.ProductGeneral) Then
            Me.btnAdd.Visible = False
        End If
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

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click, StandardSearchLive1.addnew
        Session("ProductID") = Nothing
        'Tee 7/19/2007 product configurator
        If Request.QueryString("ProdType") <> "" Then
            Response.Redirect("ProductGeneral.aspx?ProdType=" & Request.QueryString("ProdType"))
        End If
        'end Tee
        Response.Redirect("productgeneral.aspx")
    End Sub

#End Region

#Region "Private Sub StandardSearchLive1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.DeleteClick"

	   Private Sub StandardSearchLive1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchLive1.DeleteClick
        Dim objProductManagement As New CProductManagement(CLng(sender))
        Dim result As Long = objProductManagement.DeleteProduct
        If result = 1 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "PopUp", "<Script language='JavaScript'>alert('This product is " _
            & "currently part of a Customer Defined Bundle, please\n remove this product from the Customer " _
            & "Defined Bundle before deleting\n this product.');</Script>")
            Exit Sub
        ElseIf result = 2 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "PopUp", "<Script language='JavaScript'>alert('This product is " _
            & "currently part of a Merchant Bundle, please\n remove this product from the Merchant " _
            & "Bundle before deleting\n this product.');</Script>")
            Exit Sub
        End If
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, StandardSearchLive1.saveAll
        StandardSearchLive1.ActivateProducts()
    End Sub

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overrides ReadOnly Property HelpUrl() As String
        Get

            'objective: write the help url based on the product type

            Dim iProductType As Integer = CInt(Request.QueryString("ProdType"))

            Select Case iProductType
                Case 2 'merchant bundles
                    Return String.Format(HELP_URL_FORMAT, "merchbdl_ov1")
                Case 4 'customer bundles
                    Return String.Format(HELP_URL_FORMAT, "custbdl_ov1")
                Case Else 'products
                    Return String.Format(HELP_URL_FORMAT, "products_ov1")
            End Select

        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class
