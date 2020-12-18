
Partial  Class SwatchDataList
    Inherits CWebControl
    Private m_DescriptionAllignment As Integer = 0
    Public WriteOnly Property DescriptionAllignment() As Integer
        Set(ByVal Value As Integer)
            m_DescriptionAllignment = Value
        End Set
    End Property
    Public ReadOnly Property DescAllign() As String
        Get
            If m_DescriptionAllignment = 0 Then
                Return "Left"
            ElseIf m_DescriptionAllignment = 1 Then
                Return "Center"
            ElseIf m_DescriptionAllignment = 2 Then
                Return "Right"
            End If
            Return ""
        End Get
    End Property
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
    End Sub
    Private Sub DLSwatch_Databind(ByVal sender As Object, ByVal e As DataListItemEventArgs) Handles dlSwatch.ItemCreated
        Dim td As Web.UI.HtmlControls.HtmlTableCell
        
        td = CType(e.Item.FindControl("tdSwatch"), Web.UI.HtmlControls.HtmlTableCell)
        td.Align = DescAllign
    End Sub
End Class
