Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class SimpleSearch
    Inherits CWebControl
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSearch As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgSearch As System.Web.UI.WebControls.Image
    Protected WithEvents txtSimpleSearch As System.Web.UI.WebControls.TextBox
    Private mCss As String = "RightColumn"
    Event SimpleSearch_Click As EventHandler

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
        imgSearch.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Search").Attributes("Filepath").Value

        If Me.Parent.NamingContainer.ToString.ToLower.IndexOf("rightcolumnnav") >= 0 Then
            mCss = "RightColumn"
        ElseIf Me.Parent.NamingContainer.ToString.ToLower.IndexOf("leftcolumnnav") >= 0 Then
            mCss = "LeftColumn"
        Else
            mCss = "Content"

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim i As Integer
        Dim oAdmin As New Admin.CStore(StoreFrontConfiguration.AdminStore)
        Dim sLink As String
        Session("PageIndex") = 0
        Session("Search")=Nothing
        sLink = oAdmin.DomainName & "SearchResult.aspx?KeyWords=" & txtSimpleSearch.Text
        ' RaiseEvent SimpleSearch_Click(sLink, System.EventArgs.Empty)
        Response.Redirect(sLink)
    End Sub

    Public Property CssCls() As String
        Get
            Return mCss
        End Get
        Set(ByVal Value As String)
            mCss = Value
        End Set
    End Property
End Class
