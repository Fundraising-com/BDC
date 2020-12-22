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
Imports StoreFront.BusinessRule
Imports System.Xml
Imports System.Runtime.Serialization.Formatters.Binary

Partial Class DefaultPage
    Inherits CWebPage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents EcomLink As System.Web.UI.WebControls.HyperLink
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents HomePageTemplate11 As HomePageTemplate1
    Protected WithEvents HomePageTemplate21 As HomePageTemplate2
    Protected WithEvents HomePageTemplate31 As HomePageTemplate3
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
            SetPageTitle = m_objMessages.GetXMLMessage("default.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            If IsNothing(EcomLink) = False Then
                EcomLink.NavigateUrl = StoreFrontConfiguration.SSLPath & "Management"
            End If


            If Not IsPostBack Then
                If (Not IsNothing(Me.HomePageTemplate11)) AndAlso (Not IsNothing(Me.HomePageTemplate21)) And (Not IsNothing(Me.HomePageTemplate31)) Then
                    Me.HomePageTemplate11.Visible = False
                    Me.HomePageTemplate21.Visible = False
                    Me.HomePageTemplate31.Visible = False
                    Select Case StoreFrontConfiguration.HomePageDetail.Attributes("uid").Value
                        Case 1
                            Me.HomePageTemplate11.Visible = True
                        Case 2
                            Me.HomePageTemplate21.Visible = True
                        Case 3
                            Me.HomePageTemplate31.Visible = True
                    End Select
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class Default Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
End Class
