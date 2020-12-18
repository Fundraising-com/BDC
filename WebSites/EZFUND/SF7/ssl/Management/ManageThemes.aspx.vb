Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

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
'Allows user to browse, create, and apply themes.  Also allows user to
'browse and install remote themes.  Provides ability to begin editing any
'local theme.  This entails populating the Preview tables with the information
'for the selected design and transferring the user to the themeedit page.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class ManageThemes
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Tr1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lnkCheckCustom As System.Web.UI.WebControls.ImageButton
    Private arFake(0) As String

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
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Me.ErrorMessage.Visible = False
            If Not IsPostBack Then
                Me.BindCurrentTheme()
                Me.BindCategoryList()
            End If
            Me.BindThemeList()
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

    End Sub

    Public Sub BindThemeList()
        Try
            Me.DataGrid1.AllowPaging = True
            Me.DataGrid1.AllowCustomPaging = True
            If Not IsNothing(Session("ThemePageIndex")) Then
                DataGrid1.CurrentPageIndex = Session("ThemePageIndex")
            End If
            Dim ds As DataSet
            If ddlCats.SelectedValue = "All Themes" Then
                ds = (New DesignManager).GetAllNonActiveDesigns
            Else
                ds = (New DesignManager).GetAllNonActiveDesigns(ddlCats.SelectedValue)
            End If
            Me.DataGrid1.VirtualItemCount = ds.Tables(0).Rows.Count
            If Me.DataGrid1.CurrentPageIndex >= (Me.DataGrid1.VirtualItemCount / Me.DataGrid1.PageSize) Then
                Me.DataGrid1.CurrentPageIndex = 0
            End If
            Me.DataGrid1.DataSource = arFake
            Me.DataGrid1.DataBind()
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub BindCategoryList()
        Try
            Dim myDesignManager As New DesignManager
            Dim ds As DataSet
            ds = myDesignManager.GetDistinctDesignCategories()
            ddlCats.Items.Clear()
            ddlCats.Items.Add("All Themes")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                For Each dr In ds.Tables(0).Rows
                    If Not IsDBNull(dr.Item("Categories")) Then
                        ddlCats.Items.Add(dr.Item("Categories"))
                    End If
                Next
            End If
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub BindCurrentTheme()
        Try
            Dim ds As DataSet = (New DesignManager).GetAllActiveDesigns()
            Dim dt As DataTable = ds.Tables(0)
            Me.lblThemeName.Text = dt.Rows(0).Item("Name")
            If Not IsDBNull(dt.Rows(0).Item("Thumbnail")) Then
                Me.imgCurrentTheme.ImageUrl = dt.Rows(0).Item("Thumbnail")
            End If
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
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

    Public Sub FillPreviewTablesByDataSet(ByVal ds As DataSet)
        Try
            Dim myDesignManager As New DesignManager
            myDesignManager.DeleteAllLayoutPreview()
            myDesignManager.UpdateLayoutPreviewByDataSet(ds)
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub CurrentThemeEditButton_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CurrentThemeEditButton.Click
        Session.Remove("DesignArea")
        Response.Redirect("ThemeEdit.aspx")
    End Sub

    Public Function GetActiveDesign() As Long
        Dim ds As DataSet = (New DesignManager).GetAllActiveDesigns()
        Return ds.Tables(0).Rows(0).Item("uid")
    End Function

    Public Sub SetActiveDesign(ByVal DesignID As Long)
        Try
            Me.FillPreviewTables(DesignID)
            'Dim CSS As New CSSBuilder
            ''Writes to Css in the root of the site
            'Dim cssContents As String = CSS.getCss(False)

            'Dim arrayBuffer(cssContents.Length) As Byte
            'arrayBuffer = (New System.Text.UTF8Encoding).GetBytes(cssContents)
            'Dim s As String = System.Convert.ToBase64String(arrayBuffer)
            'Dim objClient As New System.Net.WebClient
            'Dim responseArray As Byte()
            'Dim objWebRequest As New System.Collections.Specialized.NameValueCollection
            'objWebRequest.Add("Bytes", 4)
            'objWebRequest.Add("FileBytes", s)
            'objWebRequest.Add("FileName", "Styles.css")
            'Dim adminManagement As New CAdminGeneralManagement
            'objWebRequest.Add("AdminGuid", adminManagement.AdminGuid)
            'responseArray = objClient.UploadValues(StoreFrontConfiguration.SiteURL & "SFExpressUpload.aspx", "POST", objWebRequest)

            ''Sends the Css Contents to the root of the site
            'Dim managementPath As String = Server.MapPath("")
            'Dim sslpath As String = managementPath.Substring(0, managementPath.LastIndexOf("\"))
            'CSS.WriteCss(sslpath & "\Styles.css")

            'Set The Design to be active
            Dim myDesignManager As New DesignManager
            myDesignManager.SetActiveDesign(DesignID)
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub imgCurrentTheme_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCurrentTheme.Click
        Response.Redirect("ThemeEdit.aspx")
    End Sub

    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ds As DataSet
            If ddlCats.SelectedValue = "All Themes" Then
                ds = (New DesignManager).GetNonActiveDesigns((DataGrid1.CurrentPageIndex * DataGrid1.PageSize), DataGrid1.PageSize)
            Else
                ds = (New DesignManager).GetNonActiveDesigns((DataGrid1.CurrentPageIndex * DataGrid1.PageSize), DataGrid1.PageSize, ddlCats.SelectedValue)
            End If
            CType(e.Item.FindControl("DataList1"), DataList).DataSource = ds.Tables(0)
            CType(e.Item.FindControl("DataList1"), DataList).DataKeyField = "uid"
            CType(e.Item.FindControl("DataList1"), DataList).DataBind()
            Dim dlItem As DataListItem
            For Each dlItem In CType(e.Item.FindControl("DataList1"), DataList).Items
                Dim ThemeName As String = CType(dlItem.FindControl("lblTheme"), Label).Text
                CType(dlItem.FindControl("cmdApply"), ImageButton).Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Would you like to apply the " & ThemeName & " to your store now? This will overwrite your existing design immediately." & "');")
                CType(dlItem.FindControl("InstalledButtons"), Panel).Visible = True
                'CType(dlItem.FindControl("imgThumbNail"), ImageButton).ImageUrl = StoreFrontConfiguration.SSLPath & "Images/" & CType(dlItem.FindControl("imgThumbNail"), ImageButton).ImageUrl()
            Next
        End If
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If e.Item.ItemType = ListItemType.Pager AndAlso (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource
            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("ThemePageIndex") + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = Session("ThemePageIndex") - 1
            End If
            Session("ThemePageIndex") = DataGrid1.CurrentPageIndex
            Me.BindThemeList()
        End If
    End Sub

    Public Sub EditTheme(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itemSelected As DataListItem = Nothing
        Dim themedataList As DataList = Nothing
        Try
            itemSelected = CType(sender, ImageButton).Parent.Parent
            themedataList = itemSelected.Parent
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SSLPath & "errors.aspx")
        End Try
        Response.Redirect("ThemeEdit.aspx?id=" & themedataList.DataKeys(itemSelected.ItemIndex))
    End Sub

    Public Sub ApplyTheme(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim itemSelected As DataListItem = CType(sender, ImageButton).Parent.Parent
            Dim themedataList As DataList = itemSelected.Parent
            Dim iThemeID As Integer = themedataList.DataKeys(itemSelected.ItemIndex)
            Me.SetActiveDesign(iThemeID)
            Me.BindCurrentTheme()
            Me.BindThemeList()

            Dim oDesignManager As New DesignManager
            StoreFrontConfiguration.ThemesPath = oDesignManager.GetActiveThemePath()

            Me.ErrorMessage.Visible = True
            Me.ErrorMessage.Text = "Your settings are changed."
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub PreviewTheme(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim itemSelected As DataListItem = CType(sender, ImageButton).Parent
            Dim themedataList As DataList = itemSelected.Parent
            Dim flag As Boolean = False
            Dim iDesignId As Integer = themedataList.DataKeys(itemSelected.ItemIndex)
            'Me.FillPreviewTables(iDesignId)
            'Dim CSS As New CSSBuilder
            'CSS.WriteCss(Server.MapPath("") & "\PreviewStyles.css", flag)
            ClientScript.RegisterStartupScript(Me.GetType, "PopUpPreview", "<script language='JavaScript'> popUpPreview('Preview.aspx?id=" & iDesignId & "&flag=" & flag & "');</script>")
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub InstallTheme(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'BEGIN: GJV - 8/24/2007 - OSP merge
        'NOTE: removed per the requirements
        'Try
        '    Dim itemSelected As DataListItem = CType(sender, ImageButton).Parent.Parent
        '    Dim themedataList As DataList = itemSelected.Parent
        '    Dim themes As New net.storefront.info.Themes
        '    Dim designds As DataSet = themes.GetThemeDesign(themedataList.DataKeys(itemSelected.ItemIndex))
        '    Dim imagesds As DataSet = themes.GetThemeImages(themedataList.DataKeys(itemSelected.ItemIndex))
        '    Dim layoutds As DataSet = themes.GetThemeLayout(themedataList.DataKeys(itemSelected.ItemIndex))
        '    Dim tBuilder As New ThemeBuilder
        '    tBuilder.InstallNewTheme(designds, imagesds, layoutds)
        '    Dim adminId As New CAdminGeneralManagement
        '    themes.InstallImages(themedataList.DataKeys(itemSelected.ItemIndex), StoreFrontConfiguration.SiteURL, adminId.AdminGuid)
        '    Me.ErrorMessage.Visible = True
        '    Me.ErrorMessage.Text = "Please Note: It may take a few minutes to install all the images needed for the selected theme"
        'Catch ex As Exception
        '    Session("DetailError") = "Class General Error=" & ex.Message
        '    Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        'End Try
        'END: GJV - 8/24/2007 - OSP merge
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        Session("ThemePageIndex") = DataGrid1.CurrentPageIndex
        Me.BindThemeList()
        Me.BindCategoryList()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton

            If Session("ThemePageIndex") > 0 Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If Session("ThemePageIndex") < DataGrid1.PageCount - 1 Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        End If
    End Sub

    Private Sub ddlCats_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCats.SelectedIndexChanged
        Session("ThemePageIndex") = 0
        BindThemeList()
    End Sub
    Protected Sub lnkCheckCustom_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim manager As New DesignManager
        Dim sAddedThemes As String = manager.AddCustomThemes()
        Me.ErrorMessage.Visible = True
        If sAddedThemes.Length > 0 Then
            Me.ErrorMessage.Text = "The following themes have been installed:<br>" & sAddedThemes.Replace(vbCrLf, "<br>")
        Else
            Me.ErrorMessage.Text = "No new themes were found to install."
        End If
        Me.BindCategoryList()
        Me.BindThemeList()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Session.Remove("DesignArea")
        Response.Redirect("ThemeEdit.aspx")
    End Sub
End Class
