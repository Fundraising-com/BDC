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

Public Class ManageEmail
    Inherits CWebPage
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtEMailAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSecondaryEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents AdminTabControl1 As AdminTabControl

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

    Dim strUC As String


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            btnSave.Attributes.Add("onclick", "return SetValidation();")
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True

            Dim ar As New ArrayList()
            ar.Add("Settings")
            ar.Add("EMail A Friend")
            ar.Add("Forgot Password")
            ar.Add("Confirm")
            If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value <> "SE") Then
                ar.Add("WishList")
                ar.Add("Low Stock Notice")
            End If


            AdminTabControl1.BorderClass = "ContentTable"
            AdminTabControl1.TabItemClass = "Content"
            AdminTabControl1.TabStringArray = ar

            If Not Page.IsPostBack Then
                Dim objAdmin As New Management.CAdminGeneralManagement()
                txtEMailAddress.Text = objAdmin.PrimaryEmail
                txtSecondaryEmail.Text = objAdmin.SecondaryEmail
            End If
        Catch ex As Exception
            Me.ErrorMessage.Text = ex.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#Region "Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.TabClick"
    Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
        If (sender = "0") Then
            'Do nothing
        ElseIf (sender = "1") Then 'EMail a Friend
            Response.Redirect("EditEMail.aspx?Type=0")
        ElseIf (sender = "2") Then ' Forgot Password
            Response.Redirect("EditEMail.aspx?Type=1")
        ElseIf (sender = "4") Then ' Wish List
            Response.Redirect("EditEMail.aspx?Type=2")
        ElseIf (sender = "3") Then ' Confirm
            Response.Redirect("EditEMail.aspx?Type=4")
        ElseIf (sender = "5") Then ' Low Stock
            Response.Redirect("EditEMail.aspx?Type=11")
        End If
    End Sub
#End Region

    Public Sub SaveClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objAccess As New Management.CAdminGeneralManagement()
        objAccess.PrimaryEmail = txtEMailAddress.Text
        objAccess.SecondaryEmail = txtSecondaryEmail.Text
        Try
            objAccess.update()
            Message.Text = "Your email settings have been updated"
            Message.Visible = True
        Catch objError As Exception
            ErrorMessage.Text = objError.Message
            ErrorMessage.Visible = True
        End Try
    End Sub

End Class
