Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class LeftColumnNav
    Inherits CWebControl
    Implements IMenuAccess

    Protected WithEvents CMenuBar1 As CMenubar

'jgh
    Protected WithEvents CMenuBar2 As CMenubar
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
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
            Return CMenuBar1.Visible
        End Get
        Set(ByVal Value As Boolean)
            CMenuBar1.Visible = Value
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
        CMenuBar1.ReloadNav()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CMenuBar1.StyleClass = "LeftColumn"

'jgh
'test if CMenubar2 exists, if it does set style

If Not IsNothing(Me.FindControl("Cmenubar2"))
	CMenuBar2.StyleClass = "LeftColumn"
End If


'test if custommenu is present, if yes set visibility

If Not IsNothing(Me.FindControl("custommenu"))

        If (CurrentWebPage.ToLower.IndexOf("/management/") <> -1) Then

            Me.FindControl("custommenu").Visible = False
            Me.FindControl("merchanttools").Visible = True
	else

            Me.FindControl("custommenu").Visible = True
            Me.FindControl("merchanttools").Visible = False
        End If

'/jgh

End If


    End Sub
End Class
