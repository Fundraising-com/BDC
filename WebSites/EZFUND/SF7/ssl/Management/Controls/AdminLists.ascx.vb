
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Partial Class AdminLists
    Inherits CWebControl
    Event EditClick As EventHandler
    Event DeleteClick As EventHandler
    Event AddNew As EventHandler

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



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'If Not IsPostBack Then
        '    DataGrid1.CurrentPageIndex = 0
        'End If
    End Sub
    Sub BindData()
        Dim objUser As New CAdminUser
        Dim ar As ArrayList
        ar = objUser.GetAllAdmins()
        DataGrid1.DataSource = ar
        DataGrid1.DataBind()
        If DataGrid1.PageCount <= 1 Then
            DataGrid1.PagerStyle.Visible = False
        End If
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If e.Item.ItemType = ListItemType.Pager Then
            Dim objcell As TableCell = e.Item.Controls(0)
            objcell.ColumnSpan = 4
        ElseIf e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btn As LinkButton = e.Item.FindControl("Delete")
            btn.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this administrator?');")
        End If
    End Sub
    Private Sub datagrid1_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Sub EditAdmin(ByVal sender As Object, ByVal e As EventArgs)
        Dim uid As Long = CLng(DataGrid1.DataKeys(CType(sender, LinkButton).CommandName))
        RaiseEvent EditClick(uid, e)
        BindData()
    End Sub
    Sub DeleteAdmin(ByVal Sender As Object, ByVal e As EventArgs)
        Dim uid As Long = CLng(DataGrid1.DataKeys(CType(Sender, LinkButton).CommandName))
        Dim objAdmin As New CAdminUser
        objAdmin.deleteAdmin(uid)
        RaiseEvent DeleteClick(Sender, e)
        BindData()
    End Sub

    Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        RaiseEvent AddNew(sender, e)
    End Sub

  
End Class
