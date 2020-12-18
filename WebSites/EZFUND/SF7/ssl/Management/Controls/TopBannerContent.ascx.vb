Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class TopBannerContent
    Inherits System.Web.UI.UserControl

    Private m_DesignArea As String

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
        If CurrentArea = "TopBanner" Then
            Dim ds As DataSet = (New DesignManager).GetAllLayoutPreviewByAreaName(CurrentArea)
            Dim dt As DataTable = ds.Tables(0)

            If dt.Rows.Count > 0 AndAlso IsDBNull(dt.Rows(0).Item("DisplayStyle")) = False Then
                If dt.Rows(0).Item("DisplayStyle") = DisplayStyle.None Then
                    Me.chkDisplayDynamicContent.Checked = False
                Else
                    Me.chkDisplayDynamicContent.Checked = True
                End If

                If dt.Rows(0).Item("DisplayStyle") = DisplayStyle.Image Then
                    Me.rbBannerImage.Checked = True
                ElseIf dt.Rows(0).Item("DisplayStyle") = DisplayStyle.PageName Then
                    Me.rbStoreName.Checked = True
                End If
                If Not IsDBNull(dt.Rows(0).Item("ImageURL")) Then
                    Me.txtBannerImage.Text = dt.Rows(0).Item("ImageURL")
                End If
            End If
        End If
    End Sub

    Public Sub SaveSettings()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Save settings from controls back into Layout Preview table.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        ' begin: JDB - Require image if displaying banner image
        If Me.rbBannerImage.Checked AndAlso Me.txtBannerImage.Text.Length <= 0 Then
            Throw New Exception("Please select a Banner Image.")
        Else
            Dim CurrentArea As String = TranslateAreaName(Me.m_DesignArea)
            Dim myDesignManager As New DesignManager
            Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName(CurrentArea)
            Dim dt As DataTable = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                If Me.chkDisplayDynamicContent.Checked Then
                    If Me.rbBannerImage.Checked Then
                        dt.Rows(0).Item("DisplayStyle") = DisplayStyle.Image
                    ElseIf Me.rbStoreName.Checked Then
                        dt.Rows(0).Item("DisplayStyle") = DisplayStyle.PageName
                    End If
                Else
                    dt.Rows(0).Item("DisplayStyle") = DisplayStyle.None
                End If
                dt.Rows(0).Item("ImageURL") = Me.txtBannerImage.Text
                'Commit Changes to temp table
                myDesignManager.UpdateLayoutPreview(ds)
            End If
        End If
        ' end: JDB - Require image if displaying banner image
    End Sub

    Public Sub UploadImage(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ucUploadImage.Visible = True
        ucUploadImage.PanelVisible()
    End Sub

    Private Sub ucUploadImage_ImageUpload(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucUploadImage.ImageUpload
        If CType(sender, String) <> String.Empty Then
            Me.txtBannerImage.Text = CType(sender, String)
            SaveSettings()
        End If
    End Sub

End Class
