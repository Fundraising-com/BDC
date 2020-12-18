Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class RightColumnNav
    Inherits CWebControl
    Implements IMenuAccess

    Protected WithEvents CMenuBar1 As CMenubar
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CMenuBar1.StyleClass = "RightColumnText" 'update #2319
        ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
        CMenuBar1.DisabledStyleClass = "RightColumnTextDisabled" 'update #2319
        ' end: JDB - 8/2/2007 - StoreFront Module Licensing
    End Sub

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
End Class
