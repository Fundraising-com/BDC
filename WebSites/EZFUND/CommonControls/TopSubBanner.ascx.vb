Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class TopSubBanner
    Inherits CWebControl
    Implements IMenuAccess

    Protected WithEvents Label As System.Web.UI.WebControls.Label
    Protected WithEvents PageImage As System.Web.UI.WebControls.Image
    Protected WithEvents CMenuBar1 As CMenubar

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

    Private m_strPageName As String
    Private m_objMenuElement As XmlElement

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If StoreFrontConfiguration.TopSubBannerDisplayStyle = DisplayStyle.Image Then
            PageImage.ImageUrl = "../images/" & StoreFrontConfiguration.TopSubBannerImage
            PageImage.Visible = True
            CMenuBar1.Visible = False
            Label.visible = False
        ElseIf StoreFrontConfiguration.TopSubBannerDisplayStyle = DisplayStyle.NavLinks Then
            PageImage.visible = False
            Label.visible = False
            CMenuBar1.Visible = True
            CMenuBar1.StyleClass = "TopSubBanner"
        Else
            Label.Visible = True
            PageImage.Visible = False
            CMenuBar1.Visible = False
        End If
    End Sub

    Public Property NavVisible() As Boolean
        Get
            Return CMenuBar1.Visible
        End Get
        Set(ByVal Value As Boolean)
            CMenuBar1.Visible = Value
            Label.Visible = Not Value
        End Set
    End Property

    Public Property PageName() As String
        Get
            Return m_strPageName
        End Get
        Set(ByVal Value As String)
            m_strPageName = Value
            Label.Text = m_strPageName
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
