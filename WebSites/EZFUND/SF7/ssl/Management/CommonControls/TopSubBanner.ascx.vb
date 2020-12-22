Partial Class TopSubBanner1
    Inherits CWebControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sTitle As String
    Public Property Title() As String
        Get
            Return Me.sTitle
        End Get
        Set(ByVal Value As String)
            sTitle = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub lbSignOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSignOut.Click
        Session.Remove("Admin")
        Response.Redirect("default.aspx")
    End Sub
End Class
