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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule.WebRequest
Imports System.Xml

Partial Class m_Default
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
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
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            ' begin: JDB - ReloadXML availability to all MT Users
            Me.ErrorMessage.Visible = False
            If Request.QueryString("ReloadXML") = 1 Then
                Me.ErrorMessage.Visible = True
                Dim oAdmin As New CAdminGeneralManagement
                Dim objWeb As New CWebRequest
                objWeb.Type = 0
                objWeb.URI = New String(StoreFrontConfiguration.SiteURL & "ReloadXML.aspx?SSL=1")
                objWeb.AddNameValuePair("AdminGuid", oAdmin.AdminGuid)
                objWeb.SendRequest()
                If objWeb.Response.ToLower().IndexOf("internal server error") >= 0 Then
                    Throw New Exception("During call to ReloadXML: " & objWeb.Response)
                End If
                objWeb = Nothing

                Me.ErrorMessage.Text = "Your changes have been successfully applied to your site."
                StoreFrontConfiguration.MerchantMenus.Clear()
                CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).ReloadNav()
            End If
            ' end: JDB - ReloadXML availability to all MT Users
        Catch ex As Exception
            Session("DetailError") = "Class Default Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
