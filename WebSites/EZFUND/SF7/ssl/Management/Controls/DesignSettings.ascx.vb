Imports StoreFront.BusinessRule

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'This control alows you to edit design settings.  It is used for all areas
'except the page settings or BodyTable area. It contains a control for editing
'the menubar content that is shown only for areas that have a menubar.  The
'MenuBar save method is called whenever this control saves its own content.
'The DesignArea property determines which area is being displayed and edited.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class DesignSettings
    Inherits System.Web.UI.UserControl

    Private m_DesignArea As String = "General Settings"
    'Protected WithEvents BackgroundColor As Omnipotent.OPColorPicker
    'Protected WithEvents ddlFontList As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    'Protected WithEvents btnBrowse As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents FontColor As Omnipotent.OPColorPicker

    Public Property DesignArea() As String
        Get
            Return m_DesignArea
        End Get
        Set(ByVal Value As String)

            If Me.Visible = True Then
                SaveSettings()
            End If

            m_DesignArea = Value
        End Set
    End Property

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblBackGroundColor As System.Web.UI.WebControls.Label
    'Protected WithEvents txtBackgroundColor As System.Web.UI.WebControls.TextBox
    'Protected WithEvents lblBackGroundImage As System.Web.UI.WebControls.Label
    'Protected WithEvents txtBackgroundImage As System.Web.UI.WebControls.TextBox
    'Protected WithEvents lblAlignment As System.Web.UI.WebControls.Label
    'Protected WithEvents ddlAlignment As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents lblFont As System.Web.UI.WebControls.Label
    'Protected WithEvents lblSize As System.Web.UI.WebControls.Label
    'Protected WithEvents lblColor As System.Web.UI.WebControls.Label
    'Protected WithEvents lblStyle As System.Web.UI.WebControls.Label
    'Protected WithEvents ddlStyle As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents txtSize As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtFontColor As System.Web.UI.WebControls.TextBox
    'Protected WithEvents pnlWidth As System.Web.UI.WebControls.Panel
    'Protected WithEvents txtWidth As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ucUploadImage As SFExpressUploadControl
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set visiblility of the checkbox to show and hide design sections
        'It should only be visible for areas that can be hidden
        If Not Session.Item("DesignArea") Is Nothing Then
            m_DesignArea = Session.Item("DesignArea")
            Dim CurrentArea As String = TranslateAreaName(Me.m_DesignArea)
            If Me.CanHide(CurrentArea) = True Then
                Me.chkVisible.Visible = True
            End If
        End If
    End Sub

    'Private Sub BackgroundColor_ColorPicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackgroundColor.ColorPicked
    '    '------------------------------------------------------------------------
    '    'Summary
    '    '------------------------------------------------------------------------
    '    'Set the text property to a value for the color object that is currently
    '    'selected in the color picker control.
    '    '------------------------------------------------------------------------
    '    '------------------------------------------------------------------------
    '    Me.txtBackgroundColor.Text = System.Drawing.ColorTranslator.ToHtml(Me.BackgroundColor.Color)
    'End Sub

    'Private Sub FontColor_ColorPicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles FontColor.ColorPicked
    '    '------------------------------------------------------------------------
    '    'Summary
    '    '------------------------------------------------------------------------
    '    'Set the text property to a value for the color object that is currently
    '    'selected in the color picker control.
    '    '------------------------------------------------------------------------
    '    '------------------------------------------------------------------------
    '    Me.txtFontColor.Text = System.Drawing.ColorTranslator.ToHtml(Me.FontColor.Color)

    'End Sub

    Public Sub SaveSettings()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Save settings from controls back into Layout Preview table.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------

        Dim CurrentArea As String = TranslateAreaName(Me.m_DesignArea)

        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName(CurrentArea)
        Dim dt As DataTable = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            'dr.Item("BackGroundColor") = Me.txtBackgroundColor.Text
            'dr.Item("HorizontalAlignment") = Me.ddlAlignment.SelectedItem.Text
            'dr.Item("FontFace") = Me.ddlFontList.SelectedValue
            'If IsNumeric(Me.txtSize.Text) Then
            '    dr.Item("FontSize") = Me.txtSize.Text
            'End If
            'dr.Item("BackgroundImageURL") = Me.txtBackgroundImage.Text
            'dr.Item("FontColor") = Me.txtFontColor.Text
            'dr.Item("FontStyle") = Me.ddlStyle.SelectedItem.Text
            If Me.CanHide(CurrentArea) = True Then
                Me.chkVisible.Visible = True
                dr.Item("Visible") = Me.chkVisible.Checked
            End If
            'If pnlWidth.Visible = True Then
            '    dr.Item("TableWidth") = Me.txtWidth.Text
            'End If
            'Commit Changes to temp table
            myDesignManager.UpdateLayoutPreview(ds)
        End If
    End Sub

    Public Function TranslateAreaName(ByVal AreaName As String) As String
        Select Case AreaName
            Case "General Settings"
                Return "BodyTable"
            Case "Top Banner"
                Return "TopBanner"
            Case "Top Sub Banner"
                Return "TopSubBanner"
            Case "Instructions"
                Return "Instruction"
            Case "Messages"
                Return "Messages"
            Case "Errors"
                Return "ErrorMessages"
            Case "Headings"
                Return "Headings"
            Case "Left Column"
                Return "LeftColumn"
            Case "Content"
                Return "Content"
            Case "Right Column"
                Return "RightColumn"
            Case "Tables"
                Return "ContentTableHeader"
            Case "Bottom Bar"
                Return "Footer"
            Case Else
                Return ""
        End Select
    End Function

    Public Sub BindFields()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Bind control values on the form to values in the database
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim CurrentArea As String = TranslateAreaName(Me.m_DesignArea)
        'If CurrentArea = "LeftColumn" OrElse CurrentArea = "RightColumn" Then
        '    pnlWidth.Visible = True
        'Else
        '    pnlWidth.Visible = False
        'End If
        Select Case CurrentArea
            Case "LeftColumn"
            Case "RightColumn"
            Case "TopBanner"
            Case "TopSubBanner"
            Case "Footer"
                Me.chkVisible.Visible = True
            Case Else
                Me.chkVisible.Visible = False
        End Select
        'BindFontList()
        Dim ds As DataSet = (New DesignManager).GetAllLayoutPreviewByAreaName(CurrentArea)
        Dim dt As DataTable = ds.Tables(0)
        'Dim flag As Boolean

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)

            'If Not IsDBNull(dr.Item("BackGroundColor")) Then
            '    Me.txtBackgroundColor.Text = dr.Item("BackGroundColor")
            '    'Me.BackgroundColor.Color = System.Drawing.ColorTranslator.FromHtml(dr.Item("BackGroundColor"))
            'Else
            '    Me.txtBackgroundColor.Text = String.Empty
            'End If

            'If Not IsDBNull(dr.Item("TableWidth")) AndAlso pnlWidth.Visible = True Then
            '    Me.txtWidth.Text = dr.Item("TableWidth")
            'ElseIf pnlWidth.Visible = True Then
            '    Me.txtWidth.Text = String.Empty
            'End If
            'If Not IsDBNull(dr.Item("HorizontalAlignment")) Then
            '    Me.ddlAlignment.SelectedIndex = Me.ddlAlignment.Items.IndexOf(Me.ddlAlignment.Items.FindByText(dr.Item("HorizontalAlignment")))
            'End If
            'If Not IsDBNull(dr.Item("BackgroundImageURL")) Then
            '    Me.txtBackgroundImage.Text = dr.Item("BackgroundImageURL")
            'Else
            '    Me.txtBackgroundImage.Text = String.Empty
            'End If

            'If Not IsDBNull(dr.Item("FontFace")) AndAlso dr.Item("FontFace") <> String.Empty Then
            '    Dim ddlFoltListItem As System.Web.UI.WebControls.ListItem
            '    For Each ddlFoltListItem In ddlFontList.Items
            '        If dr.Item("FontFace") = ddlFoltListItem.Text Then
            '            Me.ddlFontList.SelectedValue = dr.Item("FontFace")
            '            flag = True
            '        End If
            '    Next
            '    If flag = False Then
            '        CType(Page.FindControl("ErrorMessage"), Label).Visible = True
            '        CType(Page.FindControl("ErrorMessage"), Label).Text = "The Font " & dr.Item("FontFace") & " you have selected is not WebSafe Font. Please select one from the availbale list of WebSafe fonts."
            '    End If
            'End If

            'If Not IsDBNull(dr.Item("FontSize")) Then
            '    Me.txtSize.Text = dr.Item("FontSize")
            'Else
            '    Me.txtSize.Text = String.Empty
            'End If

            'If Not IsDBNull(dr.Item("FontColor")) Then
            '    Me.txtFontColor.Text = dr.Item("FontColor")
            '    'Me.FontColor.Color = System.Drawing.ColorTranslator.FromHtml(dr.Item("FontColor"))
            'Else
            '    Me.txtFontColor.Text = String.Empty
            'End If

            'If Not IsDBNull(dr.Item("FontStyle")) Then
            '    Me.ddlStyle.SelectedIndex = Me.ddlStyle.Items.IndexOf(Me.ddlStyle.Items.FindByText(dr.Item("FontStyle")))
            'End If

            If Me.CanHide(CurrentArea) = True Then
                Me.chkVisible.Visible = True
            End If

            If Not IsDBNull(dr.Item("Visible")) Then
                Me.chkVisible.Checked = dr.Item("Visible")
            End If
        End If
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Add("DesignArea", Me.m_DesignArea)
    End Sub

    Public Sub UploadImage(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'ucUploadImage.Visible = True
        'ucUploadImage.PanelVisible()
        'Me.txtBackgroundImage.Visible = False
        'Me.btnBrowse.Visible = False
    End Sub

    'Private Sub ucUploadImage_ImageUpload(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucUploadImage.ImageUpload
    '    Me.txtBackgroundImage.Visible = True
    '    Me.btnBrowse.Visible = True
    '    If CType(sender, String) <> String.Empty Then
    '        Me.txtBackgroundImage.Text = CType(sender, String)
    '    End If
    'End Sub

    Private Function CanHide(ByVal CurrentArea As String) As Boolean
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Return a boolean describing whether or not this area can have its visibility
        'Controlled.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Select Case CurrentArea

            Case "TopBanner"
                Return True
            Case "TopSubBanner"
                Return True

            Case "LeftColumn"
                Return True

            Case "RightColumn"
                Return True

            Case "Footer"
                Return True

            Case Else
                Return False

        End Select
    End Function

    'Private Sub BindFontList()
    '    Me.ddlFontList.Items.Clear()
    '    Me.ddlFontList.Items.Add("Arial")
    '    Me.ddlFontList.Items.Add("Arial Black")
    '    Me.ddlFontList.Items.Add("Courier New")
    '    Me.ddlFontList.Items.Add("Comic Sans")
    '    Me.ddlFontList.Items.Add("Georgia")
    '    Me.ddlFontList.Items.Add("Impact")
    '    Me.ddlFontList.Items.Add("Times New Roman")
    '    Me.ddlFontList.Items.Add("Trebuchet")
    '    Me.ddlFontList.Items.Add("Verdana")
    'End Sub


End Class
