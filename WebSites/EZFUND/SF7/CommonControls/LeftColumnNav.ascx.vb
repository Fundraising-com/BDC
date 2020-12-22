Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class LeftColumnNav
    Inherits CWebControl
    Implements IMenuAccess

    Protected WithEvents CMenuBar1 As CMenubar

'jgh
    Protected WithEvents CMenuBar2 As CMenubar
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    'BEGIN: GJV - 8/17/2007 - OSP merge
    'NOTE: removed per the requirements
    'Protected WithEvents CustomHtml As System.Web.UI.WebControls.Label
    'END: GJV - 8/17/2007 - OSP merge
    '/jgh

    Event Search_Click As EventHandler

    Private m_objMenuElement As XmlElement

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

    Public Property NavVisible() As Boolean
        Get
            If Not IsNothing(CMenuBar1) Then
                Return CMenuBar1.Visible
            Else
                Return False
            End If
        End Get
        Set(ByVal Value As Boolean)
            If Not IsNothing(CMenuBar1) Then
                CMenuBar1.Visible = Value
            End If
        End Set
    End Property

    Public Property MenuNode() As XmlElement Implements IMenuAccess.MenuNode
        Get
            Return m_objMenuElement
        End Get
        Set(ByVal Value As XmlElement)
            m_objMenuElement = Value
        End Set
    End Property

    Public Sub RefreshNav()
        If Not IsNothing(CMenuBar1) Then
            CMenuBar1.ReloadNav()
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(CMenuBar1) Then
            CMenuBar1.StyleClass = "LeftColumnText" 'update #2319
            ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
            CMenuBar1.DisabledStyleClass = "LeftColumnTextDisabled" 'update #2319
            ' end: JDB - 8/2/2007 - StoreFront Module Licensing
        End If
        'jgh
        'test if CMenubar2 exists, if it does set style
        If Not IsNothing(Me.FindControl("Cmenubar2")) Then
            CMenuBar2.StyleClass = "LeftColumnText"
            ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
            CMenuBar2.DisabledStyleClass = "LeftColumnTextDisabled" 'update #2319
            ' end: JDB - 8/2/2007 - StoreFront Module Licensing
        End If

        'test if custommenu is present, if yes set visibility
        If Not IsNothing(Me.FindControl("custommenu")) Then
            If (CurrentWebPage.ToLower.IndexOf("/management/") <> -1) Then
                If Not IsNothing(Me.FindControl("custommenu")) Then
                    Me.FindControl("custommenu").Visible = False
                End If
                If Not IsNothing(Me.FindControl("merchanttools")) Then
                    Me.FindControl("merchanttools").Visible = True
                End If
            Else
                If Not IsNothing(Me.FindControl("custommenu")) Then
                    Me.FindControl("custommenu").Visible = True
                End If
                If Not IsNothing(Me.FindControl("merchanttools")) Then
                    Me.FindControl("merchanttools").Visible = False
                End If
            End If
            '/jgh
        End If
        'BEGIN: GJV - 8/17/2007 - OSP merge
        'NOTE: removed per the requirements
        'StoreFrontExpress
        'If Not IsNothing(CustomHtml) AndAlso StoreFrontConfiguration.LeftColumnCustomHtml <> String.Empty Then
        '    If CType(Me.FindControl("CMenuBar1"), CMenubar).IsAdminArea Then
        '        CustomHtml.Visible = False
        '    Else
        '        CustomHtml.Visible = True
        '        If (CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) < 0) Then
        '            CustomHtml.Text = StoreFrontConfiguration.LeftColumnCustomHtml
        '        Else
        '            CustomHtml.Text = StoreFrontConfiguration.LeftColumnCustomHtml.Replace(StoreFrontConfiguration.SiteURL, StoreFrontConfiguration.SSLPath)
        '        End If
        '    End If
        'End If
        'END: GJV - 8/17/2007 - OSP merge
    End Sub
End Class
