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

Partial Class ProductImages
    Inherits CWebPage
    Protected WithEvents InvCell1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents InvCell2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents InvCell3 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents InvCell4 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ProductImages As ProductImagesControl
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
            'chcek if user has permission to view this page
            If MyBase.RestrictedPages(Tasks.ProductImages) = True Then
                Response.Redirect("Accessdenied.aspx")
            End If
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Me.lblPDName.Text = Session("ProductName")
        Catch ex As Exception
            Session("DetailError") = "Class ProductImages Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
