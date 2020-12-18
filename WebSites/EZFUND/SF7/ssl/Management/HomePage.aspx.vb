Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

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

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Allows viewing and editing of settings for the stores detail pages.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class HomePage
    Inherits CWebPage
    Protected WithEvents Tr2 As System.Web.UI.HtmlControls.HtmlTableRow

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
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        If Not IsPostBack Then
            Me.rdoTemplate1.Checked = (StoreFrontConfiguration.HomePageDetail.Attributes("uid").Value = 1)
            Me.rdoTemplate2.Checked = (StoreFrontConfiguration.HomePageDetail.Attributes("uid").Value = 2)
            Me.rdoTemplate3.Checked = (StoreFrontConfiguration.HomePageDetail.Attributes("uid").Value = 3)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click

        If Me.rdoTemplate1.Checked Then
            HomePageDetail.SetActiveTemplate(1)
        ElseIf Me.rdoTemplate2.Checked Then
            HomePageDetail.SetActiveTemplate(2)
        ElseIf Me.rdoTemplate3.Checked Then
            HomePageDetail.SetActiveTemplate(3)
        End If

    End Sub

End Class
