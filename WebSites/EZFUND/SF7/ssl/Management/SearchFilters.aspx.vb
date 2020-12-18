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

Partial Class SearchFilters
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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If MyBase.RestrictedPages(Tasks.SearchResultFilters) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            If Not IsPostBack Then
                Dim oAttributeManagement As New CAttributeManagement
                Me.rpSearchFilter.DataSource = oAttributeManagement.GetSearchFilters()
                Me.rpSearchFilter.DataBind()
            End If
        Catch ex As Exception
            Session("DetailError") = "Class SearchFilters Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#End Region

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim oAttributeManagement As New CAttributeManagement
        For Each oItem As RepeaterItem In Me.rpSearchFilter.Items
            If oItem.ItemType = ListItemType.Item Or oItem.ItemType = ListItemType.AlternatingItem Then
                Dim sAttributeName As String = CType(oItem.FindControl("lblAttributeName"), Label).Text
                Dim bFilter As String = CType(oItem.FindControl("chkFilter"), CheckBox).Checked
                Dim sGlobalSelectorName As String = CType(oItem.FindControl("txtGlobalSelectorName"), TextBox).Text

                oAttributeManagement.SaveSearchFilter(sAttributeName, bFilter, sGlobalSelectorName)
            End If
        Next
    End Sub
End Class
