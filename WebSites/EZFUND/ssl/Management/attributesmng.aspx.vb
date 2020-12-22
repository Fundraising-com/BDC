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

Public Class attributesmng
    Inherits CWebPage

#Region "Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable"

	  Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TabControl As AdminTabControl
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Attributechoice1 As attributechoice
    Protected WithEvents lblPDName As System.Web.UI.WebControls.Label
    Protected WithEvents InvCell1 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents InvCell2 As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents attTemplates1 As attTemplates

    Private Enum Mode
        list = 0
        add = 1
        edit = 2
        ISNew = 3
    End Enum
    Private _Mode As Mode = Mode.list
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Table001 As System.Web.UI.HtmlControls.HtmlTable
    Private _AttManager As CAttributeManagement

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

            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
                InvCell1.InnerHtml = ""
                InvCell1.InnerText = ""
                InvCell2.InnerHtml = ""
                InvCell2.InnerText = ""
            End If

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not IsPostBack Then
                LoadForm()
            Else
                _AttManager = Session("AttributeManager")
                If IsNothing(_AttManager) Then
                    LoadForm()
                ElseIf _AttManager.ProductID = 0 Then
                    LoadForm()
                End If
            End If
            ErrorMessage.Visible = False
            ErrorMessage.Text = ""
            Me.lblPDName.Text = Session("ProductName")
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = ex.Message
        End Try
    End Sub


#End Region

#Region "Private Sub Attributechoice1_ApplyTemplate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attributechoice1.ApplyTemplate"

    Private Sub Attributechoice1_ApplyTemplate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attributechoice1.ApplyTemplate
        _AttManager = Session("AttributeManager")
        If _AttManager.CanEdit = False Then
            _AttManager.CanEdit = True
            _AttManager.Attributes = _AttManager.TemplateAttributes
            _AttManager.Save()
            LoadForm()
        End If

    End Sub


#End Region

#Region "Private Sub Attributechoice1_TemplateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attributechoice1.TemplateChanged"

    Private Sub Attributechoice1_TemplateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attributechoice1.TemplateChanged
        _AttManager = Session("AttributeManager")
        Dim obj As DropDownList = sender
        Dim sXmlPath As String

        If obj.SelectedItem.Value.Trim.ToLower <> "none" Then
            If obj.SelectedItem.Text.Trim <> "" Then
                _AttManager.CanEdit = False
                sXmlPath = Server.MapPath("")
                sXmlPath = sXmlPath & "\SFTemplates\" & obj.SelectedItem.Value
                sXmlPath = Replace(sXmlPath, "\\", "\")
                _AttManager.TemplateName = obj.SelectedItem.Text
                _AttManager.XMLPath = sXmlPath
                attTemplates1.LoadFromTemplate(obj.SelectedItem.Value)
            Else
                _AttManager.CanEdit = True
                LoadForm()
            End If
        Else
            _AttManager.CanEdit = True
            LoadForm()
        End If

    End Sub


#End Region

#Region "Public Sub LoadForm()"

    Public Sub LoadForm()
        Dim i As Integer = Request.QueryString("Mode")
        Dim lngProdId As Long = Request.QueryString("ProductID")
        Session("WorkMode") = AttributeWorkMode.Live
        Session("AttributeManager") = Nothing
        Session("NewDetails") = Nothing  ' #1407 MS 
        _AttManager = Nothing
        If lngProdId = 0 Then
            lngProdId = Session("ProductID")

        End If
        Dim _xmlpath As String
        Dim sTemplate As String = Request.QueryString("Template")
        _AttManager = New CAttributeManagement()
        _xmlpath = Server.MapPath("")
        _xmlpath = _xmlpath & "\SFTemplates\AttributeTemplates.xml"
        _xmlpath = Replace(_xmlpath, "\\", "\")
        _AttManager.CanEdit = True
        Session("AttributeManager") = _AttManager
        _AttManager.WorkMode = AttributeWorkMode.Live
        _AttManager.TemplatePath = _xmlpath
        _AttManager.TemplateXML.Load(_xmlpath)
        Attributechoice1.LoadChoices()
        _AttManager.ProductID = lngProdId

        Session("AttributeManager") = _AttManager
        attTemplates1.LoadLive()
    End Sub

#End Region

#Region "Private Sub attTemplates1_ShowMessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowMessage"

    Private Sub attTemplates1_ShowMessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowMessage
        ErrorMessage.Visible = True
        ErrorMessage.Text = sender.ToString
    End Sub


#End Region

#Region "Private Sub attTemplates1_ShowChoices(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowChoices"

    Private Sub attTemplates1_ShowChoices(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.ShowChoices
        Attributechoice1.Visible = sender
    End Sub

#End Region

#Region "Private Sub attTemplates1_TemplateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.TemplateError"

    Private Sub attTemplates1_TemplateError(ByVal sender As Object, ByVal e As System.EventArgs) Handles attTemplates1.TemplateError
        ErrorMessage.Visible = True
        ErrorMessage.Text = sender.ToString
    End Sub

#End Region

End Class
