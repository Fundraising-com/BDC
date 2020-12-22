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

Public Class CustProfileMain
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnSignOut As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgSignOut As System.Web.UI.WebControls.Image
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents P1 As System.Web.UI.HtmlControls.HtmlGenericControl
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
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("CustProfileMain.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell)
            imgSignOut.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("SignOut").Attributes("Filename").Value
        Catch ex As Exception
            Session("DetailError") = "Class CustProfileMain Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        If (m_objCustomer.IsSignedIn() = False) Then
            Response.Redirect("CustSignIn.aspx?ReturnPage=CustProfileMain.aspx")
        End If
       
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Session("Customer") = Nothing
    '    m_objCustomer = Nothing
    '    Session("OrderHistory") = Nothing
    '    Response.Redirect("default.aspx")
    'End Sub

    Private Sub btnSignOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignOut.Click
        If (IsNothing(m_objCustomer) = False) Then
            m_objCustomer.UpdateSignIn(False)
        End If
        Session("Customer") = Nothing
        m_objCustomer = Nothing
        Session("XMLShoppingCart") = Nothing
        m_objXMLCart = Nothing
        Session("OrderHistory") = Nothing
        Response.Redirect("default.aspx")
    End Sub
End Class
