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

Public Class inventorymng
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents InventoryLevelCtrl1 As InventoryLevelCtrl
    Protected WithEvents InventoryOptCtrl1 As InventoryOptCtrl
    Protected WithEvents lblPDName As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Form2 As System.Web.UI.HtmlControls.HtmlForm
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
            CType(InventoryOptCtrl1.FindControl("cmdApplyNotify"), LinkButton).Attributes.Add("onclick", "return SetValidationLowFlag();")
            CType(InventoryOptCtrl1.FindControl("cmdApplyInventory"), LinkButton).Attributes.Add("onclick", "return SetValidationQty();")
            CType(InventoryLevelCtrl1.FindControl("cmdSave"), LinkButton).Attributes.Add("onclick", "return SetValidationSave();")
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("customers.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            Me.lblPDName.Text = Session("ProductName")
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            Me.ErrorMessage.Visible = False
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not IsPostBack Then
                loadForm()
            Else
            End If
        Catch ex As Exception
            Session("DetailError") = "Class InventoryMng Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub loadForm()
        InventoryLevelCtrl1.Visible = False
    End Sub

    Private Sub InventoryOptCtrl1_ShowInventory(ByVal sender As Object, ByVal e As System.EventArgs) Handles InventoryOptCtrl1.ShowInventory
        Dim obj As Inventory_Management = sender
        If Not IsNothing(obj) Then
            InventoryLevelCtrl1.SetME(obj)
        End If



    End Sub

    Private Sub InventoryOptCtrl1_SetDefaultLevel(ByVal sender As Object, ByVal e As System.EventArgs) Handles InventoryOptCtrl1.SetDefaultLevel
        InventoryLevelCtrl1.SetDefaultLevel(CLng(sender))
    End Sub

    Private Sub InventoryOptCtrl1_SetDefaultLowFlag(ByVal sender As Object, ByVal e As System.EventArgs) Handles InventoryOptCtrl1.SetDefaultLowFlag
        InventoryLevelCtrl1.SetLowFlag(CLng(sender))
    End Sub

    Private Sub InventoryOptCtrl1_InventoryError(ByVal sender As Object, ByVal e As System.EventArgs) Handles InventoryOptCtrl1.InventoryError
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = sender.ToString
    End Sub
End Class
