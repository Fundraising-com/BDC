Partial Class LayoutGuide
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lnkContent As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public SelectedArea As String = ""
    Public Event SelectedAreaChanged(ByVal SelectedArea As String)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            SetAreaDesignSettings(lnkTopBanner)
            If lnkTopBanner.Enabled Then
                SetAreaDesignSettings(lnkTopBanner)
                RaiseEvent SelectedAreaChanged(lnkTopBanner.Text)
            ElseIf lnkTopSubBanner.Enabled Then
                SetAreaDesignSettings(lnkTopSubBanner)
                RaiseEvent SelectedAreaChanged(lnkTopSubBanner.Text)
            ElseIf lnkLeftColumn.Enabled Then
                SetAreaDesignSettings(lnkLeftColumn)
                RaiseEvent SelectedAreaChanged(lnkLeftColumn.Text)
            ElseIf lnkRightColumn.Enabled Then
                SetAreaDesignSettings(lnkRightColumn)
                RaiseEvent SelectedAreaChanged(lnkRightColumn.Text)
            ElseIf lnkBottomBar.Enabled Then
                SetAreaDesignSettings(lnkBottomBar)
                RaiseEvent SelectedAreaChanged(lnkBottomBar.Text)
            Else
                'nothing can be edited
                SetAreaDesignSettings(New LinkButton)
            End If
        End If
    End Sub

    Private Sub AreaLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkTopBanner.Click, lnkBottomBar.Click, lnkContent.Click, lnkLeftColumn.Click, lnkRightColumn.Click, lnkTopSubBanner.Click
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Raise the SelectedAreaChanged Event and pass the text of the button that
        'was clicked.  This is used for navigation in editing design areas.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------

        Dim CurrentButton As LinkButton = sender

        If Me.SelectedArea <> CurrentButton.Text Then
            ' begin: JDB - Require image if displaying banner image
            If tdTopBanner.BgColor = selectedColor Then
                PreviouslySelectedAreaControl = Me.lnkTopBanner
            ElseIf tdTopSubBanner.BgColor = selectedColor Then
                PreviouslySelectedAreaControl = Me.lnkTopSubBanner
            ElseIf tdLeftColumn.BgColor = selectedColor Then
                PreviouslySelectedAreaControl = Me.lnkLeftColumn
            ElseIf tdRightColumn.BgColor = selectedColor Then
                PreviouslySelectedAreaControl = Me.lnkRightColumn
            ElseIf tdBottomBar.BgColor = selectedColor Then
                PreviouslySelectedAreaControl = Me.lnkBottomBar
            End If
            ' end: JDB - Require image if displaying banner image
            SetAreaDesignSettings(CurrentButton)
            RaiseEvent SelectedAreaChanged(CurrentButton.Text)

        End If

    End Sub

    ' begin: JDB - Require image if displaying banner image
    Private PreviouslySelectedAreaControl As LinkButton

    Public Sub CancelSelectedAreaChange()
        Me.SetAreaDesignSettings(Me.PreviouslySelectedAreaControl)
    End Sub
    ' end: JDB - Require image if displaying banner image

#Region "Members"
    Private mBottomBarEditable As Boolean = True
    Private mRightColumnEditable As Boolean = True
    Private mLeftColumnEditable As Boolean = True
    Private mTopSubBannerEditable As Boolean = True
    Private mTopBannerEditable As Boolean = True
#End Region

#Region "Properties"
    Public Property BottomBarEditable() As Boolean
        Get
            Return mBottomBarEditable
        End Get
        Set(ByVal Value As Boolean)
            mBottomBarEditable = Value
        End Set
    End Property
    Public Property RightColumnEditable() As Boolean
        Get
            Return mRightColumnEditable
        End Get
        Set(ByVal Value As Boolean)
            mRightColumnEditable = Value
        End Set
    End Property
    Public Property LeftColumnEditable() As Boolean
        Get
            Return mLeftColumnEditable
        End Get
        Set(ByVal Value As Boolean)
            mLeftColumnEditable = Value
        End Set
    End Property
    Public Property TopBannerEditable() As Boolean
        Get
            Return mTopBannerEditable
        End Get
        Set(ByVal Value As Boolean)
            mTopBannerEditable = Value
        End Set
    End Property
    Public Property TopSubBannerEditable() As Boolean
        Get
            Return mTopSubBannerEditable
        End Get
        Set(ByVal Value As Boolean)
            mTopSubBannerEditable = Value
        End Set
    End Property
#End Region

    ' begin: JDB - Require image if displaying banner image
    Private Const editableColor As String = "#FFFFFF"
    Private Const notEditableColor As String = "#FFDDDD"
    Private Const selectedColor As String = "#D6DEDE"
    ' end: JDB - Require image if displaying banner image

    Private Sub SetAreaDesignSettings(ByVal currentButton As LinkButton)
        ' begin: JDB - Require image if displaying banner image
        'Dim editableColor As String = "#FFFFFF"
        'Dim notEditableColor As String = "#FFDDDD"
        'Dim selectedColor As String = "#D6DEDE"
        ' end: JDB - Require image if displaying banner image

        tdGeneral.BgColor = editableColor
        tdContent.BgColor = editableColor

        If TopBannerEditable Then
            tdTopBanner.BgColor = editableColor
            lnkTopBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(editableColor)
        Else
            tdTopBanner.BgColor = notEditableColor
            lnkTopBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(notEditableColor)
            lnkTopBanner.Enabled = False
        End If

        If TopSubBannerEditable Then
            tdTopSubBanner.BgColor = editableColor
            lnkTopSubBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(editableColor)
        Else
            tdTopSubBanner.BgColor = notEditableColor
            lnkTopSubBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(notEditableColor)
            lnkTopSubBanner.Enabled = False
        End If

        If LeftColumnEditable Then
            tdLeftColumn.BgColor = editableColor
            lnkLeftColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(editableColor)
        Else
            tdLeftColumn.BgColor = notEditableColor
            lnkLeftColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(notEditableColor)
            lnkLeftColumn.Enabled = False
        End If

        If RightColumnEditable Then
            tdRightColumn.BgColor = editableColor
            lnkRightColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(editableColor)
        Else
            tdRightColumn.BgColor = notEditableColor
            lnkRightColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(notEditableColor)
            lnkRightColumn.Enabled = False
        End If

        If BottomBarEditable Then
            tdBottomBar.BgColor = editableColor
            lnkBottomBar.BackColor = System.Drawing.ColorTranslator.FromHtml(editableColor)
        Else
            tdBottomBar.BgColor = notEditableColor
            lnkBottomBar.BackColor = System.Drawing.ColorTranslator.FromHtml(notEditableColor)
            lnkBottomBar.Enabled = False
        End If

        If currentButton.ID = lnkTopBanner.ID Then
            tdTopBanner.BgColor = selectedColor
            lnkTopBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(selectedColor)
        ElseIf currentButton.ID = lnkTopSubBanner.ID Then
            tdTopSubBanner.BgColor = selectedColor
            lnkTopSubBanner.BackColor = System.Drawing.ColorTranslator.FromHtml(selectedColor)
        ElseIf currentButton.ID = lnkLeftColumn.ID Then
            tdLeftColumn.BgColor = selectedColor
            lnkLeftColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(selectedColor)
        ElseIf currentButton.ID = lnkRightColumn.ID Then
            tdRightColumn.BgColor = selectedColor
            lnkRightColumn.BackColor = System.Drawing.ColorTranslator.FromHtml(selectedColor)
        ElseIf currentButton.ID = lnkBottomBar.ID Then
            tdBottomBar.BgColor = selectedColor
            lnkBottomBar.BackColor = System.Drawing.ColorTranslator.FromHtml(selectedColor)
        End If

    End Sub
End Class
