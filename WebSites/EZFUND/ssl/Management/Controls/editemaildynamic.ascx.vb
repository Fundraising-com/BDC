
Imports StoreFront.SystemBase

Public MustInherit Class editemaildynamic
    Inherits CWebControl
    Protected strTextBody As String
    Protected strSubject As String

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
        If Page.IsPostBack Then
        End If
    End Sub

    Public Property TextBody() As String
        Get
            Return strTextBody
        End Get
        Set(ByVal Value As String)
            strTextBody = Value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return strSubject
        End Get
        Set(ByVal Value As String)
            strSubject = Value
        End Set
    End Property
End Class
