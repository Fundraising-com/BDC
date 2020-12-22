Imports StoreFront.SystemBase

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

Partial Class ProductGeneral
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents InvCell4 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ProductGeneralControl1 As ProductGeneralControl

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
        MyBase.RedirectToProductTab(Tasks.ProductGeneral)
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            CType(ProductGeneralControl1.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return Validation();")
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
                InvCell4.InnerHtml = ""
                InvCell4.InnerText = ""
            End If
            DataBind()
        Catch ex As Exception
            Session("DetailError") = "Class ProductGeneral Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overrides ReadOnly Property HelpUrl() As String
        Get

            'objective: write the help url based on the product type

            Dim oProductManagement As BusinessRule.Management.CProductManagement = Nothing
            Dim iProductType As Integer = 0

            If Not IsNothing(Request.QueryString("ProdType")) Then
                iProductType = CInt(Request.QueryString("ProdType"))
            Else
                If Not IsNothing(Request.QueryString("ID")) Then
                    oProductManagement = New BusinessRule.Management.CProductManagement(CInt(Request.QueryString("ID")))
                Else
                    oProductManagement = New BusinessRule.Management.CProductManagement(CInt(Session("ProductId")))
                End If
                iProductType = oProductManagement.ProductType
            End If

            Select Case iProductType
                Case 2 'merchant bundles
                    Return String.Format(HELP_URL_FORMAT, "merchbdl_sbs1")
                Case 4 'customer bundles
                    Return String.Format(HELP_URL_FORMAT, "custbdl_sbs1")
                Case Else 'products
                    Return String.Format(HELP_URL_FORMAT, "products_gen")
            End Select

        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class
