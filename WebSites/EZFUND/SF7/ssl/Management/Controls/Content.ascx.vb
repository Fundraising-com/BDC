Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class Content
    Inherits System.Web.UI.UserControl

    Private m_DesignArea As String
    Protected WithEvents NavObjects1 As NavObjects

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
    Protected WithEvents rbBanner As System.Web.UI.WebControls.RadioButton

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
        If CurrentArea = "LeftColumn" OrElse CurrentArea = "RightColumn" OrElse CurrentArea = "Footer" Then
            Me.NavObjects1.CurrentArea = Me.m_DesignArea
            Me.NavObjects1.ShowSelf()
        End If
    End Sub

    Public Sub SaveSettings()
        '------------------------------------------------------------------------
        'Summary
        '------------------------------------------------------------------------
        'Save settings from controls back into Layout Preview table.
        '------------------------------------------------------------------------
        '------------------------------------------------------------------------
        Me.NavObjects1.CurrentArea = Me.m_DesignArea
            Me.NavObjects1.Save()
    End Sub

End Class
