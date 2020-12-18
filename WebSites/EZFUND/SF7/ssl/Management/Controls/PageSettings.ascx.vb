'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'This control alows you to edit general(BodyTable) design settings.  It is
'Identical in most ways to the Design settings control.  It is only required
'because of the different information collected for this area.  
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class PageSettings
    Inherits System.Web.UI.UserControl

    Private m_DesignArea As String = "General Settings"

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
    'Protected WithEvents BackgroundColor As Omnipotent.OPColorPicker
    'Protected WithEvents BorderColor As Omnipotent.OPColorPicker
    Protected WithEvents ucUploadImage As SFExpressUploadControl
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
        'Put user code to initialize the page here
        If Not Session.Item("DesignArea") Is Nothing Then
            m_DesignArea = Session.Item("DesignArea")
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

    'Private Sub BorderColor_ColorPicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BorderColor.ColorPicked

    '    '------------------------------------------------------------------------
    '    'Summary
    '    '------------------------------------------------------------------------
    '    'Set the text property to a value for the color object that is currently
    '    'selected in the color picker control.
    '    '------------------------------------------------------------------------
    '    '------------------------------------------------------------------------
    '    Me.txtBorderColor.Text = System.Drawing.ColorTranslator.ToHtml(Me.BorderColor.Color)

    'End Sub

    Public Sub SaveSettings()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Save settings from controls back into Layout Preview table.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim CurrentArea As String = TranslateAreaName(Me.m_DesignArea)

        '***ONLY CONTINUE IF EDITING GENERAL SETTINGS***
        If CurrentArea <> "BodyTable" Then
            Exit Sub
        End If

        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from LayoutPreview where name='" & CurrentArea & "'", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim cb As New OleDb.OleDbCommandBuilder(da)

            If IsNumeric(Me.txtTopMargin.Text) Then
                dr.Item("TopMargin") = Me.txtTopMargin.Text
            End If
            If IsNumeric(Me.txtLeftMargin.Text) Then
                dr.Item("LeftMargin") = Me.txtLeftMargin.Text
            End If
            If IsNumeric(Me.txtBottomMargin.Text) Then
                dr.Item("BottomMargin") = Me.txtBottomMargin.Text
            End If
            If IsNumeric(Me.txtRightMargin.Text) Then
                dr.Item("RightMargin") = Me.txtRightMargin.Text
            End If
            dr.Item("BackGroundColor") = Me.txtBackgroundColor.Text
            dr.Item("HorizontalAlignment") = Me.ddlAlignment.SelectedItem.Text
            dr.Item("BackgroundImageURL") = Me.txtBackgroundImage.Text


            If IsNumeric(Me.txtBorderSize.Text) Then
                dr.Item("BorderSize") = Me.txtBorderSize.Text
            End If

            If IsNumeric(Me.txtCellPadding.Text) Then
                dr.Item("CellPadding") = Me.txtCellPadding.Text
            End If

            If IsNumeric(Me.txtCellSpacing.Text) Then
                dr.Item("CellSpacing") = Me.txtCellSpacing.Text
            End If


            dr.Item("BorderColor") = Me.txtBorderColor.Text

            dr.Item("TableWidth") = Me.txtWidth.Text


            'Commit Changes to temp table
            da.Update(dt)
        End If
    End Sub

    Public Function TranslateAreaName(ByVal AreaName As String) As String
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Change the selected area string to the actual name as it appears in the
        'layout table.  Return the value.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
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

        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from LayoutPreview where name='" & CurrentArea & "'", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)

            If Not IsDBNull(dr.Item("BackGroundColor")) Then
                Me.txtBackgroundColor.Text = dr.Item("BackGroundColor")
                'Me.BackgroundColor.Color = System.Drawing.ColorTranslator.FromHtml(dr.Item("BackGroundColor"))
            End If

            If Not IsDBNull(dr.Item("HorizontalAlignment")) Then
                Me.ddlAlignment.SelectedIndex = Me.ddlAlignment.Items.IndexOf(Me.ddlAlignment.Items.FindByText(CType(dr.Item("HorizontalAlignment"), String).ToLower))
            End If

            If Not IsDBNull(dr.Item("BackgroundImageURL")) Then
                Me.txtBackgroundImage.Text = dr.Item("BackgroundImageURL")
            End If

            If Not IsDBNull(dr.Item("BottomMargin")) Then
                Me.txtBottomMargin.Text = dr.Item("BottomMargin")
            End If

            If Not IsDBNull(dr.Item("TopMargin")) Then
                Me.txtTopMargin.Text = dr.Item("TopMargin")
            End If

            If Not IsDBNull(dr.Item("LeftMargin")) Then
                Me.txtLeftMargin.Text = dr.Item("LeftMargin")
            End If

            If Not IsDBNull(dr.Item("RightMargin")) Then
                Me.txtRightMargin.Text = dr.Item("RightMargin")
            End If


            If Not IsDBNull(dr.Item("BorderColor")) Then
                Me.txtBorderColor.Text = dr.Item("BorderColor")
                'Me.BorderColor.Color = System.Drawing.ColorTranslator.FromHtml(dr.Item("BorderColor"))
            End If

            If Not IsDBNull(dr.Item("TableWidth")) Then
                Me.txtWidth.Text = dr.Item("TableWidth")
            End If

            If Not IsDBNull(dr.Item("CellPadding")) Then
                Me.txtCellPadding.Text = dr.Item("CellPadding")
            End If
            If Not IsDBNull(dr.Item("CellSpacing")) Then
                Me.txtCellSpacing.Text = dr.Item("CellSpacing")
            End If
            If Not IsDBNull(dr.Item("BorderSize")) Then
                Me.txtBorderSize.Text = dr.Item("BorderSize")
            End If

            Me.ThemeName.Text = "<b>" & Me.GetThemeName(Me.GetDesignID) & "</b>"
        End If
    End Sub



    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Add("DesignArea", Me.m_DesignArea)
    End Sub

    Public Function GetThemeName(ByVal uid As Long) As String
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Get the theme name from the design table that matches the UID passed.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from designs where uid=" & uid, SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Return dt.Rows(0).Item("Name")
    End Function

    Public Function GetDesignID() As Long
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Utility function used to get the ID of the design currently in the preview
        'table.  (UID in Designs table)
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Dim dt As New DataTable
        Dim da As New OleDb.OleDbDataAdapter("Select * from LayoutPreview", SystemBase.StoreFrontConfiguration.ConnectionString)
        da.Fill(dt)
        Dim dr As DataRow = dt.Rows(0)
        Return dr.Item("DesignID")

    End Function

    Public Sub UploadImage(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.txtBackgroundImage.Visible = False
        Me.btnBrowse.Visible = False
        ucUploadImage.Visible = True
        ucUploadImage.PanelVisible()
    End Sub

    Private Sub ucUploadImage_ImageUpload(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucUploadImage.ImageUpload
        Me.txtBackgroundImage.Visible = True
        Me.btnBrowse.Visible = True
        If CType(sender, String) <> String.Empty Then
            Me.txtBackgroundImage.Text = CType(sender, String)
        End If

    End Sub
End Class
