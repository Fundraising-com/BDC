Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

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

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Contains the controls that allow viewing and changing areas in the design
'and layout.  This class is responsible for sending messages the editing 
'controls when the user has changed areas.  It also coordinates saving on
'navigation as well as handling the updates and deletion of themes themselves.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class ThemeEdit
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents layoutguide1 As LayoutGuide
    Protected WithEvents cmdApply As System.Web.UI.WebControls.ImageButton

    Protected WithEvents DesignSettings1 As DesignSettings

    Protected WithEvents PageSettings1 As PageSettings
    Protected WithEvents TopBannerContent1 As TopBannerContent
    Protected WithEvents TopSubBannerContent1 As TopSubBannerContent
    Protected WithEvents Content1 As Content
    'Protected WithEvents CustomHtml1 As CustomHtml
    Protected WithEvents lblContent As System.Web.UI.WebControls.Label
    Protected WithEvents lblNavigationalObjects As System.Web.UI.WebControls.Label


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

    Private m_iDesignId As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Me.ErrorMessage.Visible = False
            Me.cmdDelete.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Delete This Theme?" & "');")
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        If Not IsNothing(Request.QueryString("id")) AndAlso Request.QueryString("id") <> String.Empty Then
            m_iDesignId = CLng(Request.QueryString("id"))
        Else
            m_iDesignId = Me.GetActiveDesign
        End If
        If Not IsPostBack Then
            Me.FillPreviewTables(m_iDesignId)
            'By default we should be looking at the Page settings.
            'Go ahead and bind this section on first page load.
            Me.PageSettings1.BindFields()
            'Me.lblSelectedRegion.Text = Me.PageSettings1.DesignArea
            If Me.PageSettings1.GetDesignID = Me.GetActiveDesign Then
                Me.cmdDelete.Visible = False
            End If
        End If

        Dim DesignID As Long = Me.PageSettings1.GetDesignID
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select [Name] from Layout where DesignID=" & DesignID & " AND Editable = 0", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        For Each dRow As DataRow In dt.Rows
            If dRow("Name").ToString.ToLower() = "topbanner" Then
                Me.layoutguide1.TopBannerEditable = False
            End If
            If dRow("Name").ToString.ToLower() = "topsubbanner" Then
                Me.layoutguide1.TopSubBannerEditable = False
            End If
            If dRow("Name").ToString.ToLower() = "rightcolumn" Then
                Me.layoutguide1.RightColumnEditable = False
            End If
            If dRow("Name").ToString.ToLower() = "leftcolumn" Then
                Me.layoutguide1.LeftColumnEditable = False
            End If
            If dRow("Name").ToString.ToLower() = "footer" Then
                Me.layoutguide1.BottomBarEditable = False
            End If
        Next
        da.Dispose()

    End Sub


    Private Sub layoutguide1_SelectedAreaChanged(ByVal SelectedArea As String) Handles layoutguide1.SelectedAreaChanged
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Handles the SelectedAreaChanged Event from the layout guide. This indicates
        'the user wishes to change the area they are editing.  Save current settings.
        'and bind the new ones.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        ' begin: JDB - Require image if displaying banner image
        ' note: attempt to save settings prior to setting design area for top and top sub to check for valid option selection
        Try
            If Me.TopBannerContent1.Visible Then
                Me.TopBannerContent1.SaveSettings()
            End If
            If Me.TopSubBannerContent1.Visible Then
                Me.TopSubBannerContent1.SaveSettings()
            End If

            Me.lblSelectedRegion.Text = SelectedArea

            Me.DesignSettings1.DesignArea = SelectedArea

            Me.PageSettings1.DesignArea = SelectedArea
            Me.TopSubBannerContent1.DesignArea = SelectedArea
            Me.TopBannerContent1.DesignArea = SelectedArea
            Me.Content1.DesignArea = SelectedArea
            'Me.CustomHtml1.DesignArea = SelectedArea
            If SelectedArea <> "General Settings" Then
                Me.cmdDelete.Visible = False
            End If

            If SelectedArea = "General Settings" Then
                Me.DesignSettings1.Visible = False
                Me.PageSettings1.Visible = True
            Else
                Me.DesignSettings1.Visible = True
                Me.PageSettings1.Visible = False
            End If
            If Me.PageSettings1.Visible = False Then
                Me.lblEditTheme.Visible = True
                Dim themeName As String = Me.PageSettings1.GetThemeName(Me.PageSettings1.GetDesignID())
                lblEditTheme.Text = "Edit Theme: " & themeName
            Else
                Me.lblEditTheme.Text = "Edit Theme"
            End If
            If SelectedArea = "Top Sub Banner" Then
                Me.TopSubBannerContent1.Visible = True
                Me.TopBannerContent1.Visible = False
                Me.Content1.Visible = False
                Me.Content1.Visible = False
                'Me.CustomHtml1.Visible = True
            ElseIf SelectedArea = "Top Banner" Then
                Me.TopSubBannerContent1.Visible = False
                Me.TopBannerContent1.Visible = True
                Me.Content1.Visible = False
                'Me.CustomHtml1.Visible = True
            ElseIf SelectedArea = "Right Column" OrElse SelectedArea = "Left Column" OrElse SelectedArea = "Bottom Bar" Then
                Me.TopSubBannerContent1.Visible = False
                Me.TopBannerContent1.Visible = False
                Me.Content1.Visible = True
                'Me.CustomHtml1.Visible = True
            Else
                Me.TopSubBannerContent1.Visible = False
                Me.TopBannerContent1.Visible = False
                Me.Content1.Visible = False
                'Me.CustomHtml1.Visible = False
            End If
            If Me.DesignSettings1.Visible = True Then
                Me.DesignSettings1.BindFields()
            Else
                Me.PageSettings1.BindFields()
            End If
            If Me.TopSubBannerContent1.Visible = True Then
                Me.TopSubBannerContent1.BindFields()
                'Me.CustomHtml1.BindFields()
            ElseIf Me.TopBannerContent1.Visible = True Then
                Me.TopBannerContent1.BindFields()
                'Me.CustomHtml1.BindFields()
            ElseIf Me.Content1.Visible = True Then
                Me.Content1.BindFields()
                'Me.CustomHtml1.BindFields()
            End If

        Catch oException As Exception
            Me.layoutguide1.CancelSelectedAreaChange()
            ErrorMessage.Visible = True
            ErrorMessage.Text = oException.Message
        End Try
        ' end: JDB - Require image if displaying banner image
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Save settings to temp tables then copy changes to original tables
        '(Layout, MenuBar, Images).  Also new write styles.css to the root
        'of the site.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Try

            Me.DesignSettings1.SaveSettings()
            Me.PageSettings1.SaveSettings()
            If Me.TopSubBannerContent1.Visible = True Then
                Me.TopSubBannerContent1.SaveSettings()
                'Me.CustomHtml1.SaveSettings()
            ElseIf Me.TopBannerContent1.Visible = True Then
                Me.TopBannerContent1.SaveSettings()
                'Me.CustomHtml1.SaveSettings()
            ElseIf Me.Content1.Visible = True Then
                Me.Content1.SaveSettings()
                'Me.CustomHtml1.SaveSettings()
            End If
            Me.SaveTheme()
            Dim DesignID As Long = Me.PageSettings1.GetDesignID
            Dim activeDesignId As Long = (New DesignManager).GetAllActiveDesigns().Tables(0).Rows(0).Item("uid")
            'Write Css File only for the active theme
            If (DesignID = activeDesignId) Then

                Dim CSS As New CSSBuilder
                'Writes to Css in the root of the site
                Dim cssContents As String = CSS.getCss(False)

                Dim arrayBuffer(cssContents.Length) As Byte
                arrayBuffer = (New System.Text.UTF8Encoding).GetBytes(cssContents)
                Dim s As String = System.Convert.ToBase64String(arrayBuffer)
                Dim objClient As New System.Net.WebClient
                Dim responseArray As Byte()
                Dim objWebRequest As New System.Collections.Specialized.NameValueCollection
                objWebRequest.Add("Bytes", 4)
                objWebRequest.Add("FileBytes", s)
                objWebRequest.Add("FileName", "Styles.css")
                Dim adminManagement As New Management.CAdminGeneralManagement
                objWebRequest.Add("AdminGuid", adminManagement.AdminGuid)
                responseArray = objClient.UploadValues(StoreFrontConfiguration.SiteURL & "SFExpressUpload.aspx", "POST", objWebRequest)

                'Sends the Css Contents to the root of the site
                Dim managementPath As String = Server.MapPath("")
                Dim sslpath As String = managementPath.Substring(0, managementPath.LastIndexOf("\"))
                CSS.WriteCss(sslpath & "\Styles.css")
            End If
            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = "Your changes have been saved"
        Catch ex As Exception
            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = ex.Message
        End Try
    End Sub

    Public Sub SaveTheme()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Copy LayoutPreview table back to Layout table,  Also call a similar routine
        'to save changes to menubar tables.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim DesignID As Long = Me.PageSettings1.GetDesignID
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from Layout where DesignID=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim dtPreview As New DataTable
        Dim daPreview As New OleDb.OleDbDataAdapter("Select * from LayoutPreview", SystemBase.StoreFrontConfiguration.ConnectionString)
        daPreview.Fill(dtPreview)
        Dim cb As New OleDb.OleDbCommandBuilder(da)
        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next
        For Each dr In dtPreview.Rows
            Dim dr2 As DataRow = dt.NewRow
            dr2.ItemArray = dr.ItemArray
            dt.Rows.Add(dr2)
        Next
        da.Update(dt)
        Me.SaveMenuBar()
    End Sub

    Public Sub SaveMenuBar()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Copy MenuBarPreview table back to MenuBar table. 
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim DesignID As Long = Me.PageSettings1.GetDesignID
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from MenuBar where DesignID=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)

        Dim dtPreview As New DataTable
        Dim daPreview As New OleDb.OleDbDataAdapter("Select * from MenuBarPreview", SystemBase.StoreFrontConfiguration.ConnectionString)
        daPreview.Fill(dtPreview)
        Dim cb As New OleDb.OleDbCommandBuilder(da)

        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next

        For Each dr In dtPreview.Rows
            Dim dr2 As DataRow = dt.NewRow
            dr2.ItemArray = dr.ItemArray
            dt.Rows.Add(dr2)
        Next


        da.Update(dt)
    End Sub

    Public Sub FillPreviewTables(ByVal DesignID As Long)
        Try
            Dim myDesignManager As New DesignManager
            myDesignManager.DeleteAllLayoutPreview()
            myDesignManager.UpdateLayoutPreviewByDesignId(DesignID)
            FillMenuPreviewTable(DesignID)
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub FillMenuPreviewTable(ByVal DesignID As Long)
        Try
            Dim myDesignManager As New DesignManager
            myDesignManager.DeleteAllMenuBarPreview()
            myDesignManager.UpdateMenubarPreviewByDesignId(DesignID)
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Delete Theme Functions"

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdDelete.Click
        If Me.PageSettings1.GetDesignID = Me.GetActiveDesign Then
            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = "You cannot delete the active design"
            Exit Sub
        Else
            'delete from tables
            Dim DesignID As Long = Me.PageSettings1.GetDesignID
            Me.DeleteDesign(DesignID)
            Me.DeleteImages(DesignID)
            Me.DeleteLayout(DesignID)
            Me.DeleteMenuBar(DesignID)
            Response.Redirect("ManageThemes.aspx")
        End If
    End Sub

    Public Function GetActiveDesign() As Long
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from designs where isActive=1", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Return dt.Rows(0).Item("uid")
    End Function

    Public Sub DeleteDesign(ByVal DesignID As Long)
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from Designs where uid=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim cb As New OleDb.OleDbCommandBuilder(da)
        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next
        da.Update(dt)
    End Sub

    Public Sub DeleteLayout(ByVal DesignID As Long)
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from Layout where DesignID=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim cb As New OleDb.OleDbCommandBuilder(da)
        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next
        da.Update(dt)
    End Sub

    Public Sub DeleteImages(ByVal DesignID As Long)
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from Images where DesignID=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim cb As New OleDb.OleDbCommandBuilder(da)
        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next
        da.Update(dt)
    End Sub

    Public Sub DeleteMenuBar(ByVal DesignID As Long)
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from MenuBar where DesignID=" & DesignID, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim cb As New OleDb.OleDbCommandBuilder(da)
        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.Delete()
        Next
        da.Update(dt)
    End Sub

#End Region

    Private Sub cmdPreview_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdPreview.Click
        Try
            Me.DesignSettings1.SaveSettings()
            Me.PageSettings1.SaveSettings()
            If Me.TopSubBannerContent1.Visible = True Then
                Me.TopSubBannerContent1.SaveSettings()
            ElseIf Me.TopBannerContent1.Visible = True Then
                Me.TopBannerContent1.SaveSettings()
            ElseIf Me.Content1.Visible = True Then
                Me.Content1.SaveSettings()
            End If
            'If Me.CustomHtml1.Visible = True Then
            '    Me.CustomHtml1.SaveSettings()
            'End If
            'Dim CSS As New CSSBuilder
            'CSS.WriteCss(Server.MapPath("") & "\PreviewStyles.css")
            ClientScript.RegisterStartupScript(Me.GetType, "PopUpPreview", "<script language='JavaScript'> popUpPreview('Preview.aspx?id=" & m_iDesignId & "');</script>")
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = ex.Message
        End Try
    End Sub
End Class
