Imports System.Text
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class LivePerson
    Inherits CWebControl

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

    Private m_strAccount As String = ""
    Protected WithEvents livepersonlink As System.Web.UI.HtmlControls.HtmlAnchor
    Protected WithEvents hcIcon As System.Web.UI.HtmlControls.HtmlImage
    Private m_strProductList As New StringBuilder()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim obj As HtmlAnchor
        obj = FindControl("livepersonlink")
        Dim objimg As HtmlImage
        objimg = FindControl("hcIcon")
        Dim str As String
        If (IsNothing(obj) = False) Then
            obj.HRef = obj.HRef.Replace("[LivePersonAccount]", LivePersonAccount)
            str = obj.Attributes.Item("onClick")
            str = str.Replace("[LivePersonAccount]", LivePersonAccount)
            obj.Attributes.Item("onClick") = str
        End If
        If (IsNothing(objimg) = False) Then
            objimg.Src = objimg.Src.Replace("[LivePersonAccount]", LivePersonAccount)
        End If
        If (CurrentWebPage.IndexOf(StoreFrontConfiguration.SSLPath) <> -1) Then
            If (IsNothing(objimg) = False) Then
                objimg.Src = objimg.Src.Replace("http:", "https:")
            End If
            If (IsNothing(obj) = False) Then
                obj.HRef = obj.HRef.Replace("http:", "https:")
            End If
        End If
    End Sub

    Public Property LivePersonAccount() As String
        Get
            Return StoreFrontConfiguration.LivePersonID
        End Get
        Set(ByVal Value As String)
            m_strAccount = Value
        End Set
    End Property
End Class
