Imports StoreFront.BusinessRule.Management
Public MustInherit Class GeneralControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents StoreName As System.Web.UI.WebControls.TextBox
    Protected WithEvents AffiliateID As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Private objAdminManagement As New CAdminGeneralManagement()
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
            AffiliateID.Text = objAdminManagement.AffiliateID
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objAdminManagement.StoreName = StoreName.Text
        objAdminManagement.AffiliateID = AffiliateID.Text
        objAdminManagement.update()
    End Sub
End Class
