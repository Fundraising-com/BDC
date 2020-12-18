Imports StoreFront.SystemBase
'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Public Class ProductFulfillment
    Inherits CWebPage
    Protected WithEvents lblPDName As System.Web.UI.WebControls.Label
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents InvCell1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Table001 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents InvCell2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ProductFulfillment As ProductFulfillmentControl
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
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            CType(ProductFulfillment.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidation();")
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
                InvCell1.InnerHtml = ""
                InvCell1.InnerText = ""
                InvCell2.InnerHtml = ""
                InvCell2.InnerText = ""
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ProductFulfillment Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
