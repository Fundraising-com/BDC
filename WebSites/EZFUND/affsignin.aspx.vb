'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

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

Public Class affsignin
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Affiliatesignin1 As Affiliatesignin
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
            SetPageTitle = m_objMessages.GetXMLMessage("affsignin.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell)
            m_Affiliate = Session("Affiliate")
            CType(Affiliatesignin1.FindControl("btnSignIn"), LinkButton).Attributes.Add("onclick", "return SetValidationSignIn();")
            CType(Affiliatesignin1.FindControl("btnCreate"), LinkButton).Attributes.Add("onclick", "return SetValidationNew();")
        Catch ex As Exception
            Session("DetailError") = "Class AffSignIn Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        If IsNothing(m_Affiliate) = False Then
            If m_Affiliate.IsSignedIn Then
                Response.Redirect("affiliateaccount.aspx")
            End If
        End If
    End Sub


End Class
