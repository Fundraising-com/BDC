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

Imports System.Threading
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Marketplaces

Partial Class marketplaces
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    'Protected objMarketPlaceManager As CMarketPlaceManager
    Protected objDealTime As CDealTime

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

#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.Marketplaces) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            If Not (IsPostBack) Then
                'objMarketPlaceManager = New CMarketPlaceManager()
                'txtUserName.Text = objMarketPlaceManager.User
                'txtPassword.Text = objMarketPlaceManager.Password
            End If
            'cmdAdd.Attributes.Add("onclick", "return SetValidation();")
        Catch ex As Exception
            Session("DetailError") = "Class Marketplaces Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
#End Region

#Region "Sub cmdCreateFile_Click(ByVal sender As Object, ByVal e As EventArgs)"
    Public Sub cmdCreateFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateFile.Click
        Dim objThread As New DealTimeWork()
        objThread.threadImport.Name = "StoreFront 6 ReloadXML"
        objThread.threadImport.Start()

        'objDealTime = New CDealTime((StoreFrontConfiguration.SiteURL))
        'If (objDealTime.CreateFile()) Then
        '    Me.lblMessage.Text = "DealTime File Updated."
        'Else
        '    Me.lblMessage.Text = "Unable to create DealTime File.  Please try again."
        'End If
        'lblMessage.Visible = True
    End Sub
#End Region

    'Public Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    objMarketPlaceManager = New CMarketPlaceManager()
    '    objMarketPlaceManager.User = txtUserName.Text
    '    objMarketPlaceManager.Password = txtPassword.Text
    '    Try
    '        objMarketPlaceManager.UpdateInfo()
    '    Catch
    '        lblMessage.Text = "Unable to save changes"
    '        lblMessage.Visible = True
    '    End Try
    '    If lblMessage.Text = "" Then
    '        lblMessage.Text = "Changes saved"
    '        lblMessage.Visible = True
    '    End If
    'End Sub


    Class DealTimeWork
        Private m_strPath As String
        Private objthreadImport As Thread
        Protected objDealTime As CDealTime

        Public Sub New()
            objthreadImport = New Thread(AddressOf Me.DoWork)
        End Sub

        Public Property threadImport() As Thread
            Get
                Return objthreadImport
            End Get
            Set(ByVal Value As Thread)
                objthreadImport = Value
            End Set
        End Property

        Sub DoWork()
            With New CDealTime(StoreFrontConfiguration.SiteURL)
                .CreateFile()
            End With
        End Sub
    End Class

End Class
