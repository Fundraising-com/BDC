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
Imports System.Xml

Public Class affiliatelist
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Private objStorage As New CSearchControlStorage()
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Private objAffiliates As New CAffilateManagment()
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
            LoadSearch() 'load the table
        Catch ex As Exception
            Session("DetailError") = "Class AffiliateList Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub LoadSearch()

        Dim arSort As New ArrayList()


        arSort.Add("Affiliate Name")
        arSort.Add("Affiliate ID")


        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = True
        'objStorage.SortList = arSort
        objStorage.DataSource = objAffiliates.GetAllAffiliates

        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Affiliates"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Affiliate?"

        'Fields to add to the control
        Dim ar As New ArrayList()
        ar.Add("Name")
        ar.Add("ID")

        objStorage.ColumnList = ar

        StandardSearchControl1.StorageClass = objStorage
    End Sub


    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        '  m_objCustomer.DeleteCustomer(CType(sender, Long))
        objAffiliates.DeleteAffiliate(CLng(sender))

        objStorage.DataSource = objAffiliates.GetAllAffiliates
        LoadSearch()
        StandardSearchControl1.ReloadList()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Affiliate Deleted"
    End Sub

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/editaffiliate.aspx?Affiliate=" & sender)
    End Sub



End Class
