Imports System
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Partial Class Roles
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
        
    End Sub
    Private Sub dlRoles_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dlRoles.ItemCreated
        If e.Item.ItemType = ListItemType.Pager Then
            Dim objcell As TableCell = e.Item.Controls(0)
            objcell.ColumnSpan = 3
        End If
    End Sub
    Private Sub dlroles_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dlRoles.PageIndexChanged
        dlRoles.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub dlroles_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dlRoles.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btn As LinkButton = e.Item.FindControl("Delete")
            btn.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this role?');")
        End If
    End Sub
    Sub deleteRole(ByVal sender As Object, ByVal e As EventArgs)
        Dim roleId As Long = dlRoles.DataKeys(CType(sender, LinkButton).CommandName)
        Dim objRole As New CAdminUser
        objRole.DeleteRole(roleId)
        BindData()
        RaiseEvent DeleteClick(roleId, e)
    End Sub
    Sub Editrole(ByVal sender As Object, ByVal e As EventArgs)
        Dim roleId As Long = dlRoles.DataKeys(CType(sender, LinkButton).CommandName)
        RaiseEvent EditClick(roleId, e)
    End Sub
    Sub BindData()
        Dim objRole As New CAdminUser
        Dim ar As ArrayList = objRole.GetAllRoles

        dlRoles.DataSource = ar
        dlRoles.DataBind()
        If dlRoles.PageCount <= 1 Then
            dlRoles.PagerStyle.Visible = False
        Else
            dlRoles.PagerStyle.Visible = True
        End If
    End Sub

    Private Sub cmdAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        RaiseEvent AddNew(sender, e)
    End Sub
End Class
