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
Partial Class affiliatelinks
    Inherits CWebPage
    Protected WithEvents link As System.Web.UI.WebControls.Label
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

        Try
            SetPageTitle = m_objMessages.GetXMLMessage("affiliateaccount.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell)
            m_Affiliate = Session("Affiliate")
            If IsNothing(Me.m_Affiliate) Then
                Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
            ElseIf m_Affiliate.IsSignedIn = False Then
                Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
            ElseIf m_Affiliate.Address.FirstName.Length = 0 Then
                Response.Redirect("affiliateregister.aspx")
            Else
                lbllink.Text = m_Affiliate.Link
            End If
        Catch ex As Exception
            Session("DetailError") = "Class AffiliateLinks Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
