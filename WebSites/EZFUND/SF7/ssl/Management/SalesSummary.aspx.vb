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
Imports StoreFront.BusinessRule.Management
Imports System.Xml

Partial Class SalesSummary
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PeriodSalesControl1 As PeriodSalesControl
    Protected WithEvents SalesStatsControl1 As SalesStatsControl
    Protected WithEvents GoodsControl1 As GoodsControl
    Protected WithEvents ProductSalesStats1 As ProductSalesStats
    'ProductSales1
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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.SalesReports) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

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

                    PeriodSalesControl1.SetDisplay(objReport.SalesPeriodSummary)
                    SalesStatsControl1.SetDisplay(objReport.SalesStats)
                    GoodsControl1.SetDisplay(objReport.SalesStats.GoodsStats)
                    ProductSalesStats1.SetDisplay(objReport.SalesStats)
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class SalesSummary Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
