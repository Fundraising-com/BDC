'BEGINVERSIONINFO

'APPVERSION: 7.0.0

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

Partial Class affiliateaccount
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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

        m_Affiliate = Session("Affiliate")
        If IsNothing(Me.m_Affiliate) Then
            Response.Redirect(StoreFrontConfiguration.SiteURL & "affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
            Exit Sub
        ElseIf m_Affiliate.IsSignedIn = False Then
            Response.Redirect(StoreFrontConfiguration.SiteURL & "affsignin.aspx?ReturnPage=affiliateaccount.aspx")
            Exit Sub
        End If

        Try
            SetPageTitle = m_objMessages.GetXMLMessage("affiliateaccount.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            imgSignOut.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignOut").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class AffiliateAccount Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSignOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignOut.Click
        Session("Affiliate") = Nothing
        ' m_Affiliate = Nothing
        Response.Redirect("default.aspx")
    End Sub
End Class
