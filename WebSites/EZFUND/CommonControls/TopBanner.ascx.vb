Imports StoreFront.SystemBase

Public MustInherit Class TopBanner
    Inherits CWebControl

    Protected WithEvents StoreName As System.Web.UI.WebControls.Label
    Protected WithEvents StoreImage As System.Web.UI.WebControls.Image
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

    Private m_strSiteName As String = ""
    Private m_strSiteAdditional As String = ""
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If StoreFrontConfiguration.DisplayStoreName = True Then
            If (m_strSiteName = "") Then
                m_strSiteName = StoreFrontConfiguration.StoreName
                StoreName.Text = m_strSiteName & SiteAdditionalText
                StoreImage.Visible = False
                StoreName.Visible = True
            End If
        Else
            'update #1834
            StoreImage.ImageUrl = StoreFrontConfiguration.SSLPath & "images/" & StoreFrontConfiguration.StoreNameImage
            StoreImage.Visible = True
            StoreName.Visible = False
        End If
    End Sub

    Public Property SiteName() As String
        Get
            Return m_strSiteName
        End Get
        Set(ByVal Value As String)
            m_strSiteName = Value
            StoreName.Text = m_strSiteName & SiteAdditionalText
            StoreImage.Visible = False
            StoreName.Visible = True
        End Set
    End Property

    Public Property SiteAdditionalText() As String
        Get
            Return m_strSiteAdditional
        End Get
        Set(ByVal Value As String)
            m_strSiteAdditional = Value

        End Set
    End Property

    Public Property StoreImagePath() As String
        Get
            Return StoreImage.ImageUrl
        End Get
        Set(ByVal Value As String)
            StoreImage.ImageUrl = Value
            StoreImage.Visible = True
            StoreName.Visible = False
        End Set
    End Property
End Class
