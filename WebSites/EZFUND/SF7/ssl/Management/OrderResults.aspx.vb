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
Imports System.Xml

Partial Class OrderResults
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents OrderResultsControl1 As OrderResultsControl
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
        If MyBase.RestrictedPages(Tasks.Orders) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            Me.ErrorMessage.Visible = False
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            If Not IsPostBack Then

                Dim iDateRange As Integer = Request.QueryString("DateType")
                Dim sTo As String = Request.QueryString("To")
                Dim sFrom As String = Request.QueryString("From")
                If iDateRange = 0 Then
                    DateInfo.Visible = False
                    Status.Visible = False
                    Dim objSearch As sfSearchContainer = Session("Search")

                    If IsNothing(objSearch) = True Then
                        'msg??
                    Else
                        Dim objManagment As New CManagement(objSearch)
                        If objManagment.OrderSummary.Count > 0 Then
                            OrderResultsControl1.SetDisplay(objManagment.OrderSummary)
                        Else
                            Me.ErrorMessage.Visible = True
                            Me.ErrorMessage.Text = "No Results Found"
                            Me.trOrderSummary.Visible = False
                        End If
                    End If
                Else
                    Dim objDate As New CSearchDate(iDateRange, sFrom, sTo)
                    Dim iPayment As Integer = Request.QueryString("PaymentStatus")
                    Dim iShipping As Integer = Request.QueryString("ShipStaus")
                    Dim sStatus As String = ""
                    If iPayment <> 0 Then
                        If iPayment = 1 Then
                            sStatus = "Status: Payment Pending"
                        ElseIf iPayment = 2 Then
                            sStatus = "Status: Payment Collected"
                        End If
                    End If

                    If iShipping <> 0 Then
                        If sStatus = "" Then
                            sStatus = "Status:"
                        Else
                            sStatus = sStatus & " - "
                        End If

                        If iShipping = 1 Then
                            sStatus = sStatus & " Shipment Pending"
                        ElseIf iShipping = 2 Then
                            sStatus = sStatus & "Shipment Collected"
                        End If
                    End If
                    Status.Text = sStatus

                    DateInfo.Text = objDate.DateDisplay
                    Dim objManagment As New CManagement(objDate, iShipping, iPayment)

                    OrderResultsControl1.SetDisplay(objManagment.OrderSummary)

                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class OrderResults Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub OrderResultsControl1_OrderCanceled(ByVal sender As Object, ByVal e As System.EventArgs) Handles OrderResultsControl1.OrderCanceled
        Me.ErrorMessage.Text = "Order " & sender & " has been canceled"
        Me.ErrorMessage.Visible = True
    End Sub
End Class
