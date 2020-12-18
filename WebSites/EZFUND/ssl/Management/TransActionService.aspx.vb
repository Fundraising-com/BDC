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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports System.Xml

Public Class TransActionService
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents DateInfo As System.Web.UI.WebControls.Label
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents TransactionControl1 As TransactionControl

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
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not IsPostBack Then
                Dim iDateRange As Integer = Request.QueryString("DateType")
                Dim sTo As String = Request.QueryString("To")
                Dim sFrom As String = Request.QueryString("From")
                If iDateRange = 0 Then
                    Exit Sub
                Else
                    Dim objDate As New CSearchDate(iDateRange, sFrom, sTo)
                    DateInfo.Text = objDate.DateDisplay
                    Dim objReport As New CSFReports(objDate)
                    If objReport.TransActionResponse.Count > 0 Then
                        TransactionControl1.SetDisplay(objReport.TransActionResponse)
                    Else
                        Me.ErrorMessage.Visible = True
                        Me.ErrorMessage.Text = "No sales were found matching the specified criteria."
                    End If
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class TransactionService Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
