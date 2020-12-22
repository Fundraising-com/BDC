Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class Footer
    Inherits CWebControl
    Implements IMenuAccess

    Protected WithEvents CMenuBar1 As CMenubar
    Private m_objMenuElement As XmlElement
    Protected WithEvents StoreFrontLink As System.Web.UI.WebControls.Label
    'BEGIN: GJV - 8/17/2007 - OSP merge
    'NOTE: removed per the requirements
    '2811
    'Protected WithEvents CustomHtml As System.Web.UI.WebControls.Label
    'END: GJV - 8/17/2007 - OSP merge

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
        CMenuBar1.StyleClass = "FooterText" 'update #2319
        ' begin: JDB - 8/2/2007 - StoreFront Module Licensing
        CMenuBar1.DisabledStyleClass = "FooterTextDisabled" 'update #2319
        ' end: JDB - 8/2/2007 - StoreFront Module Licensing
        '2729
        If Not IsNothing(StoreFrontLink) Then
            Select Case StoreFrontConfiguration.StoreFrontLinkValue
                Case 1
                    StoreFrontLink.Text = StoreFrontConfiguration.StoreName & " powered by StoreFront <a href='http://www.storefront.net/default.asp?REFERER=" & StoreFrontConfiguration.StoreFrontAffiliateID & "'" & " title ='Shopping cart software by StoreFront' class='FooterText'>shopping cart software</a>"
                Case 2
                    StoreFrontLink.Text = StoreFrontConfiguration.StoreName & " <a href='http://www.storefront.net/default.asp?REFERER=" & StoreFrontConfiguration.StoreFrontAffiliateID & "'" & " title ='Shopping cart by StoreFront' class='FooterText'>shopping cart</a> powered by StoreFront"
                Case 3
                    StoreFrontLink.Text = StoreFrontConfiguration.StoreName & " powered by StoreFront <a href='http://www.storefront.net/default.asp?REFERER=" & StoreFrontConfiguration.StoreFrontAffiliateID & "'" & " title ='Ecommerce solutions from StoreFront' class='FooterText'>ecommerce solutions</a>"
                Case 4
                    StoreFrontLink.Text = StoreFrontConfiguration.StoreName & " <a href='http://www.storefront.net/default.asp?REFERER=" & StoreFrontConfiguration.StoreFrontAffiliateID & "'" & " title ='Ecommerce software by StoreFront' class='FooterText'>ecommerce software </a> powered by StoreFront "
                Case Else
                    StoreFrontLink.Text = StoreFrontConfiguration.StoreName & " powered by StoreFront <a href='http://www.storefront.net/default.asp?REFERER=" & StoreFrontConfiguration.StoreFrontAffiliateID & "'" & " title ='Shopping cart software by StoreFront' class='FooterText'>shopping cart software</a>"
            End Select
        End If
        Me.imgLagardeLogo.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("LagardeLogo").Attributes("Filepath").Value
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
