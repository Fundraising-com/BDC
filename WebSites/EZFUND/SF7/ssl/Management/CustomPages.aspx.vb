Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Partial Class CustomPages
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents hlnkAddNew As System.Web.UI.WebControls.HyperLink
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If MyBase.RestrictedPages(Tasks.CustomPages) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        Catch ex As Exception
            Session("DetailError") = "Class Default Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        If (Not Page.IsPostBack) Then
            DataBindCustomPages(0)

            If (dg1.PageCount = 1) Then
                dg1.PagerStyle.Visible = False
            Else
                dg1.PagerStyle.Visible = True
            End If
        End If
    End Sub
    Private Sub DataBindCustomPages(ByVal PageIndex As Integer)
        dg1.VirtualItemCount = CCustomPage.GetCustompagesCount()
        dg1.CurrentPageIndex = PageIndex
        dg1.DataSource = CCustomPage.GetCustomPagesList(PageIndex, dg1.PageSize)
        dg1.DataBind()
    End Sub
    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim id As Integer = CType(sender, LinkButton).CommandArgument
        Dim item As New CCustomPage(id)
        Dim myDesignManager As New DesignManager
        If (myDesignManager.IsCutomPageLinkExists(item.PageName) = False) Then
            item.Delete()
            Me.DataBindCustomPages(0)
        Else
            lblErrorMessage.Visible = True
            lblErrorMessage.Text = " This custom page is used by the themes. Please remove the custom page link from existing themes and try again."
        End If
    End Sub

    Private Sub dg1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg1.PageIndexChanged
        DataBindCustomPages(e.NewPageIndex)
    End Sub

    Private Sub dg1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dg1.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim l As LinkButton = e.Item.FindControl("lnkDelete")
            l.Attributes.Add("onclick", "javascript:return ConfirmDelete();")
        End If
    End Sub
End Class