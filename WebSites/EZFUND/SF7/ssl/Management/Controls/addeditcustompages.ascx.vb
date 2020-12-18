Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class Addcustompages
    Inherits System.Web.UI.UserControl
    Private mDataSource As CCustomPage
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtenddate As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblname As System.Web.UI.WebControls.Label
    Protected WithEvents lbltitle As System.Web.UI.WebControls.Label
    Protected WithEvents txthiddenname As System.Web.UI.WebControls.TextBox
    Protected WithEvents txthiddentitle As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (Not Page.IsPostBack) Then
            Me.DataBind()
            UltimateEditor1.EditorHtml = Me.DataSource.Content
        End If
    End Sub

    Protected ReadOnly Property VirtualDirectory() As String
        Get
            Dim siteURL As New Uri(StoreFrontConfiguration.SiteURL)
            Return siteURL.AbsolutePath
        End Get
    End Property

    Protected ReadOnly Property DataSource() As CCustomPage
        Get
            If (Not (mDataSource Is Nothing)) Then
                Return mDataSource
            End If
            If (Request.QueryString.Item("Edit") <> Nothing) Then
                mDataSource = New CCustomPage(Integer.Parse(Request.QueryString.Item("Id")))
            ElseIf (Not (ViewState("Id") Is Nothing)) Then
                mDataSource = New CCustomPage(Integer.Parse(ViewState("Id")))
            Else
                mDataSource = New CCustomPage
            End If
            Return mDataSource
        End Get
    End Property

    Protected Function GetPagename(ByVal pagename As String) As String

        If (pagename.EndsWith(".aspx") = True) Then
            Return pagename.Replace(".aspx", "")
        Else
            Return pagename
        End If
    End Function

    Protected Sub lnkSave_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not ValidateControls() Then Return
        If (txtName.Text.EndsWith(".aspx") = False) Then
            Me.DataSource.PageName = txtName.Text & ".aspx"
        Else
            Me.DataSource.PageName = txtName.Text
        End If

        Me.DataSource.Content = UltimateEditor1.EditorHtml
        Me.DataSource.PageTitle = txttitle.Text

        Me.DataSource.Save()
        Dim designmanager As New DesignManager

        Dim menuds As DataSet
        If (txthiddenname.Text <> DataSource.PageName Or txthiddentitle.Text <> DataSource.PageTitle Or (txthiddenname.Text <> DataSource.PageName And txthiddentitle.Text <> DataSource.PageTitle)) Then
            menuds = designmanager.GetAllMenuBarByMenuTextandLink(txthiddentitle.Text, txthiddenname.Text)
            Dim dt As DataTable = menuds.Tables(0)
            Dim dr As DataRow
            For Each dr In dt.Rows
                dr.Item("MenuText") = DataSource.PageTitle
                dr.Item("Link") = DataSource.PageName
            Next
            designmanager.UpdateMenuBar(menuds)
        End If
        ViewState("Id") = Me.DataSource.Id
        RaiseEvent Message("Custom Page Saved", Nothing)
        Me.DataBind()
    End Sub

    Event Message As EventHandler

    Private Function ValidateControls() As Boolean
        Dim pagename As String

        If (txtName.Text.EndsWith(".aspx") = False) Then
            pagename = txtName.Text & ".aspx"
        Else
            pagename = txtName.Text
        End If

        If (txtName.Text.Length <= 0) Then
            RaiseEvent Message("Name is required", Nothing)
            Return False
        End If
        If (txttitle.Text.Length <= 0) Then
            RaiseEvent Message("Title is required", Nothing)
            Return False
        End If
        'If (CCustomPage.IsCustompageExist(pagename) And Not Me.DataSource.Id > 0) Then
        If CCustomPage.IsCustompageExist(pagename) AndAlso Not Me.DataSource.PageName.ToLower.Equals(pagename.ToLower) Then
            RaiseEvent Message("Page Name already exists", Nothing)
            Return False
        End If
        Return True
    End Function

End Class
