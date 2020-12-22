'BEGINVERSIONINFO

'APPVERSION: 7.0.2

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

Public Class RenderCustomPage
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ltPageContent As System.Web.UI.WebControls.Literal
    'BEGIN: GJV - 8/23/2007 - OSP merge
    'OSP
    Protected WithEvents ContentCell As System.Web.UI.HtmlControls.HtmlTableCell

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
            Dim sPageName As String = Request.QueryString("PageName")
            If (Not IsNothing(sPageName)) AndAlso sPageName.Length > 0 Then
                Dim oCustomPage As New CCustomPage(sPageName)
                SetPageTitle = oCustomPage.PageTitle
                SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
                Me.CustomPageTitle = oCustomPage.PageTitle
                Me.ltPageContent.Text = oCustomPage.Content
            End If
        Catch ex As Exception
            Session("DetailError") = "Class RenderCustomPage Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private CustomPageTitle As String = String.Empty
    Protected Sub WriteCustomPageTitle()
        Response.Write(CustomPageTitle)
    End Sub
End Class
