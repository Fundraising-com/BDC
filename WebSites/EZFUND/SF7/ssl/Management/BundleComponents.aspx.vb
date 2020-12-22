'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2007 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO
Imports StoreFront.SystemBase

Partial Class BundleComponents
    Inherits CWebPage

    Protected WithEvents ProductBundleControl1 As ProductBundleControl
    Protected WithEvents BundleTemplateControl1 As BundleTemplateControl
    'Tee 8/29/2007 product configurator
    'end Tee

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If RestrictedPages(Tasks.BundleComponents) Then
            Response.Redirect("AccessDenied.aspx")
        End If
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        Me.ErrorMessage.Visible = False
        If Request.QueryString("ProdType") <> "" Then
            If Request.QueryString("ProdType") = ProductType.Bundle Then
                ProductBundleControl1.Visible = True
            ElseIf Request.QueryString("ProdType") = ProductType.Customized Then
                'Tee 8/29/2007 product configurator
                BodyTag.Attributes.Add("OnBeforeUnload", "return chkModified();")
                'end Tee
                BundleTemplateControl1.Visible = True
            End If
        End If
    End Sub

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overrides ReadOnly Property HelpUrl() As String
        Get

            'objective: write the help url based on the product type

            Dim iProductType As Integer = CInt(Request.QueryString("ProdType"))

            Select Case iProductType
                Case 2 'merchant bundles
                    Return String.Format(HELP_URL_FORMAT, "merchbdl_sbs6")
                Case 4 'customer bundles
                    Return String.Format(HELP_URL_FORMAT, "custbdl_sbs6")
                Case Else
                    Return "#"
            End Select

        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class
