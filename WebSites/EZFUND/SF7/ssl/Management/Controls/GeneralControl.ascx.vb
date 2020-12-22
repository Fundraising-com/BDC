Imports StoreFront.BusinessRule.Management
Partial  Class GeneralControl
    Inherits System.Web.UI.UserControl
    'begin: GJV - 9/7/2007 - Component License
    'end: GJV - 9/7/2007 - Component License
    Private objAdminManagement As New CAdminGeneralManagement
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
        'Put user code to initialize the page here
        If IsPostBack = False Then
            StoreName.Text = objAdminManagement.StoreName
            'AffiliateID.Text = objAdminManagement.AffiliateID
            'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            If objAdminManagement.AllowAnonymous = True Then
                chkAllowAnonymous.Checked = True
            Else
                chkAllowAnonymous.Checked = False
            End If
            If objAdminManagement.AccountCreationAllowed = True Then
                chkCreateAccount.Checked = True
            Else
                chkCreateAccount.Checked = False
            End If
            'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

            'begin: GJV - 9/7/2007 - Component License
            Me.txtComponentLicense.Text = objAdminManagement.ComponentLicense
            'end: GJV - 9/7/2007 - Component License
            ' begin: JDB - Google Analytics
            Me.txtGoogleAnalyticsID.Text = objAdminManagement.GoogleAnalyticsID
            ' end: JDB - Google Analytics
            'begin: GJV - 7.0.2 - Add means to edit the site and ssl url's
            'Me.txtSiteURL.Text = objAdminManagement.SiteURL
            'Me.txtSSLPath.Text = objAdminManagement.SSLPath
            'end: GJV - 7.0.2 - Add means to edit the site and ssl url's
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objAdminManagement.StoreName = StoreName.Text
        'objAdminManagement.AffiliateID = AffiliateID.Text
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If chkAllowAnonymous.Checked Then
            objAdminManagement.AllowAnonymous = True
        Else
            objAdminManagement.AllowAnonymous = False
        End If
        If chkCreateAccount.Checked Then
            objAdminManagement.AccountCreationAllowed = True
        Else
            objAdminManagement.AccountCreationAllowed = False
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

        'begin: GJV - 9/7/2007 - Component License
        objAdminManagement.ComponentLicense = Me.txtComponentLicense.Text
        'end: GJV - 9/7/2007 - Component License
        ' begin: JDB - Google Analytics
        objAdminManagement.GoogleAnalyticsID = Me.txtGoogleAnalyticsID.Text
        ' end: JDB - Google Analytics
        'begin: GJV - 7.0.2 - Add means to edit the site and ssl url's
        'objAdminManagement.SiteURL = Me.txtSiteURL.Text
        'objAdminManagement.SSLPath = Me.txtSSLPath.Text
        'end: GJV - 7.0.2 - Add means to edit the site and ssl url's

        objAdminManagement.update()
    End Sub
End Class
