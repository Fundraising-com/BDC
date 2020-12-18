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

Public Class AttTemplate
    Inherits CWebPage

#Region "Class Members"

	Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents attTemplates1 As attTemplates
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected objStorage As CSearchControlStorage
    Protected WithEvents lblLeft As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Private _AttManager As CAttributeManagement
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdDone As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtTemplateName As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
    Protected WithEvents imgDone As System.Web.UI.WebControls.Image

    Private Enum Mode
        list = 0
        add = 1
        edit = 2
        isnew = 3
    End Enum
    Private _Mode As Mode = Mode.list

#End Region     

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

        Try
            btnAdd.Attributes.Add("onclick", "return SetValidationAddTemplate();")
            Dim attdetailctrl1 As attdetailctrl
            Dim attMainctrl As attMainctrl
            CType(attTemplates1.FindControl("btnAdd"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            attdetailctrl1 = CType(attTemplates1.FindControl("Attdetailctrl1"), attdetailctrl)
            attMainctrl = CType(attTemplates1.FindControl("AttMainctrl1"), attMainctrl)
            CType(attdetailctrl1.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidationAttDetail();")
            CType(attdetailctrl1.FindControl("cmdAddOption"), LinkButton).Attributes.Add("onclick", "return SetValidationAttDetail();")
            CType(attMainctrl.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidationAttMain();")


            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("customers.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            Dim i As Integer = Request.QueryString("Mode")
            Me.ErrorMessage.Visible = False
            Session("WorkMode") = AttributeWorkMode.Template


            If Not IsPostBack Then
                cmdDone.Visible = False
                imgDone.Visible = False
                attTemplates1.Visible = False
                LoadTemplates()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region   

#Region "Public Sub LoadTemplates()"

    Public Sub LoadTemplates()
        Try
            Dim _xmlpath As String
            Dim sTemplate As String = Request.QueryString("Template")
            _AttManager = New CAttributeManagement()
            _xmlpath = Server.MapPath("")
            _xmlpath = _xmlpath & "\SFTemplates\AttributeTemplates.xml"
            _xmlpath = Replace(_xmlpath, "\\", "\")
            _AttManager.TemplateXML.Load(_xmlpath)
            _AttManager.TemplatePath = _xmlpath
            _AttManager.WorkMode = AttributeWorkMode.Template
            objStorage = New CSearchControlStorage()
            Dim arSort As New ArrayList()

            '   arSort.Add("Template Name")


            objStorage.ButtonID = "ID"
            objStorage.CheckBoxColumn = False
            objStorage.ContentClass = "Content"
            objStorage.Paging = False

            objStorage.ShowButtons = True
            ' objStorage.Sorting = True
            'objStorage.SortList = arSort
            objStorage.DataSource = _AttManager.TemplateChoices

            objStorage.PagerClass = "ContentTableHeader"
            objStorage.TitleClass = "ContentTableHeader"
            objStorage.TitleString = "Templates"
            objStorage.DeleteMessage = "Are You Sure You Want to Delete This Template?"

            'Fields to add to the control
            Dim ar As New ArrayList()
            ar.Add("Name")
            '   ar.Add("ID")
            cmdDone.Visible = False
            imgDone.Visible = False
            objStorage.ColumnList = ar


            StandardSearchControl1.StorageClass = objStorage
            Session("AttributeManager") = _AttManager

        Catch err As SystemException

        End Try
    End Sub

#End Region

#Region "Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick"

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        If sender.ToString.ToLower <> "none" Then
            StandardSearchControl1.Visible = False
            attTemplates1.LoadFromTemplate(sender)
            btnAdd.Visible = False
            cmdDone.Visible = True
            imgAdd.Visible = False
            imgDone.Visible = True
            txtTemplateName.Visible = False
            attTemplates1.Visible = True
        Else
            StandardSearchControl1.ReloadList()
        End If

    End Sub


#End Region

#Region "Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick"

    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        Dim _xmlpath As String
        _AttManager = Session("AttributeManager")

        _xmlpath = Server.MapPath("")

        _xmlpath = _xmlpath & "\SFTemplates\" & sender
        _xmlpath = Replace(_xmlpath, "\\", "\")
        _AttManager.DeleteTemplate(_xmlpath, sender)
        LoadTemplates()
        StandardSearchControl1.ReloadList()
    End Sub

#End Region

#Region "Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles btnAdd.Click"

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtTemplateName.Text <> "" Then
            _AttManager = Session("AttributeManager")
            _AttManager.SaveNewTemplate(txtTemplateName.Text, False)
            LoadTemplates()
            StandardSearchControl1.ReloadList()
            txtTemplateName.Text = ""
        Else
            StandardSearchControl1.ReloadList()
        End If
    End Sub

#End Region

#Region "Private Sub cmdDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdDone.Click"

    Private Sub cmdDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDone.Click
        cmdDone.Visible = False
        attTemplates1.Visible = False
        StandardSearchControl1.Visible = True
        btnAdd.Visible = True
        imgAdd.Visible = True
        imgDone.Visible = False
        txtTemplateName.Visible = True
        LoadTemplates()
        StandardSearchControl1.ReloadList()

    End Sub


#End Region

#Region "Private Sub attTemplates1_ShowMessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowMessage"

    Private Sub attTemplates1_ShowMessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowMessage
        ErrorMessage.Visible = True
        ErrorMessage.Text = sender.ToString
    End Sub

#End Region

#Region "Private Sub attTemplates1_TemplateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.TemplateError"

    Private Sub attTemplates1_TemplateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.TemplateError
        ErrorMessage.Visible = True
        ErrorMessage.Text = sender.ToString
    End Sub

#End Region

#Region "Private Sub attTemplates1_ShowChoices(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowChoices"

    Private Sub attTemplates1_ShowChoices(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowChoices
        Try
            cmdDone.Visible = sender
            imgDone.Visible = sender
        Catch

        End Try
    End Sub


#End Region

End Class
