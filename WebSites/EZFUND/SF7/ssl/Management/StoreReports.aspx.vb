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
Imports System.Xml

Partial Class StoreReports
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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.SalesReports) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            cmdSubmit.Attributes.Add("onclick", "return SetValidation();")
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            If IsPostBack Then
                If ddDateRange.SelectedItem.Value = 5 Then
                    txtFrom.Enabled = True
                    txtTo.Enabled = True
                Else
                    Message.Visible = False
                    txtFrom.Enabled = False
                    txtTo.Enabled = False
                End If
            End If
            'cmdSubmit.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSubmit").Attributes("Filename").Value
        Catch ex As Exception
            Session("DetailError") = "Class StoreReports Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        If ddDateRange.SelectedItem.Value = 5 Then

            If IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then
                If CDate(txtFrom.Text) <= CDate(txtTo.Text) Then

                    Response.Redirect(ddReportType.SelectedItem.Value.ToString & "?DateType=" & ddDateRange.SelectedItem.Value & "&From=" & txtFrom.Text & "&To=" & txtTo.Text)

                Else
                    'display msg
                    Message.Visible = True
                    Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "From>To")

                End If
            Else
                Message.Visible = True
                Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "InvalidDate")
            End If
        Else
            If ddDateRange.SelectedItem.Value <> -1 Then
                Response.Redirect(ddReportType.SelectedItem.Value.ToString & "?DateType=" & ddDateRange.SelectedItem.Value)
            Else
                Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "NoCriteria")
            End If
        End If
    End Sub

End Class
