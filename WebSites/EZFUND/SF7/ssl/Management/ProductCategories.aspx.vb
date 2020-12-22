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

Partial Class ProductCategories
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents InvCell1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents InvCell2 As System.Web.UI.HtmlControls.HtmlTableCell

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
        If MyBase.RestrictedPages(Tasks.ProductCategories) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
                InvCell1.InnerHtml = ""
                InvCell1.InnerText = ""
                InvCell2.InnerHtml = ""
                InvCell2.InnerText = ""
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ProductCategories Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    'begin: GJV - 7.0.2 issue 850 - correct product / bundle help links
    Public Overrides ReadOnly Property HelpUrl() As String
        Get

            'objective: write the help url based on the product type

            Dim oProductManagement As BusinessRule.Management.CProductManagement = Nothing
            Dim iProductType As Integer = 0

            If Not IsNothing(Request.QueryString("ID")) Then
                oProductManagement = New BusinessRule.Management.CProductManagement(CInt(Request.QueryString("ID")))
            Else
                oProductManagement = New BusinessRule.Management.CProductManagement(CInt(Session("ProductId")))
            End If
            iProductType = oProductManagement.ProductType

            Select Case iProductType
                Case 2 'merchant bundles
                    Return String.Format(HELP_URL_FORMAT, "merchbdl_sbs3")
                Case 4 'customer bundles
                    Return String.Format(HELP_URL_FORMAT, "custbdl_sbs3")
                Case Else 'products
                    Return String.Format(HELP_URL_FORMAT, "products_choosecat")
            End Select

        End Get
    End Property
    'end: GJV - 7.0.2 issue 850 - correct product / bundle help links

End Class
