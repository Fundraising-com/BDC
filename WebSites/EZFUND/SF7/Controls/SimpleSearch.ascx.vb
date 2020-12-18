Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class SimpleSearch
    Inherits CWebControl
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
        'begin: GJV - 9/7/2007 - OSP
        imgSearch.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Search").Attributes("Filepath").Value
        'end: GJV - 9/7/2007 - OSP 

		'BEGIN: GJV - 8/23/2007 - OSP merge
		'NOTE: this code was by the OSP implementation in the original source
        'If Me.Parent.NamingContainer.ToString.ToLower.IndexOf("rightcolumnnav") >= 0 Then
        '    mCss = "RightColumnText"
        'ElseIf Me.Parent.NamingContainer.ToString.ToLower.IndexOf("leftcolumnnav") >= 0 Then
        '    mCss = "LeftColumnText"
        'Else
        '    mCss = "Content"
        'End If
        'END: GJV - 8/23/2007 - OSP merge
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim i As Integer
        Dim oAdmin As New Admin.CStore(StoreFrontConfiguration.AdminStore)
        Dim sLink As String
        sLink = oAdmin.DomainName & "SearchResult.aspx?KeyWords=" & txtSimpleSearch.Text
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
