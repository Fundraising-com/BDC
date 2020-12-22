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
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

Partial Class CustomerDefinedRules
    Inherits CWebPage

    Protected WithEvents hdnID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents CustomerDefinedRulesControl1 As CustomerDefinedRulesControl

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
        If RestrictedPages(Tasks.CustomerDefinedRules) Then
            Response.Redirect("AccessDenied.aspx")
        End If
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        Me.ErrorMessage.Visible = False
    End Sub

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overrides ReadOnly Property HelpUrl() As String
        Get

            'objective: write the help url based on the product type

            'note: this is always be customer bundles
            Return String.Format(HELP_URL_FORMAT, "custbdl_sbs7")

        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class
