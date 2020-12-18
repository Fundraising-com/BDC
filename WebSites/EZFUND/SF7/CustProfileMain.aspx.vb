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

Partial Class CustProfileMain
    Inherits CWebPage
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
            imgSignOut.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignOut").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class CustProfileMain Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If (m_objCustomer.IsSignedIn() = False) Then
            ' regular user sign in
            Response.Redirect("CustSignIn.aspx?ReturnPage=CustProfileMain.aspx")
            'ElseIf Not IsNothing(Session("anonymous")) Then
            '    ' anonymous user should not be able to access my acct page
            '    ' redirect to sign in page
            '    Response.Redirect("CustSignIn.aspx?SignOut=2&ReturnPage=CustProfileMain.aspx")
            '    Exit Sub
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
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
        'Tee 11/15/2007 removed unnecessary session variable
        Session.Remove("ItemAdded")
        'end Tee
    End Sub
End Class
