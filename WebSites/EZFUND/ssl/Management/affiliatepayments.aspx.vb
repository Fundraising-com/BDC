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
Imports StoreFront.BusinessRule

Public Class affiliatepayments
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddaffliates As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkPending As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cmdSubmit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
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
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("customers.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not IsPostBack Then
                LoadAffiliates()
            End If
            'cmdSubmit.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSubmit").Attributes("Filename").Value
        Catch ex As Exception
            Session("DetailError") = "Class AffiliatePayments Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        If Me.ddaffliates.SelectedItem.Value <> 0 Then
            Dim ar As New ArrayList()
            Dim obj As New CGenericDDContainer()
            obj.ID = ddaffliates.SelectedItem.Value
            obj.Name = ddaffliates.SelectedItem.Text
            ar.Add(obj)
            Session("AffList") = ar
        End If
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/affiliatepaymentlist.aspx")
    End Sub

    Private Sub LoadAffiliates()
        Dim oAff As New CAffilateManagment()
        Dim obj As New CGenericDDContainer()
        obj.ID = 0
        obj.Name = "All Affliates"

        Dim ar As ArrayList = oAff.GetAffiliateList(chkPending.Checked)

        ar.Insert(0, obj)
        ddaffliates.DataSource = ar
        ddaffliates.DataBind()
        Session("AffList") = ar

    End Sub

    Private Sub chkPending_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPending.CheckedChanged
        LoadAffiliates()
    End Sub
End Class
