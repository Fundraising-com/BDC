Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Imports StoreFront.BusinessRule.Management
Imports CSR.CSRBusinessRule
Imports CSR.CSRSystemBase
Partial  Class employeecontrol
    Inherits CWebControl
#Region "Class Members"
    Protected WithEvents lblAttName As System.Web.UI.WebControls.Label
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected objCategory As CXMLCategoryList
    Protected objCategoryAccess As CCategories
    Protected ar As ArrayList
    Dim um As New cSRusermanagement
#End Region

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

#Region "Events"
    Event Add As EventHandler
    Event Edit As EventHandler
    Event Delete As EventHandler
#End Region

#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        DataGrid1.AllowPaging = True
        DataGrid1.PagerStyle.CssClass = "Content"

        DataGrid1.GridLines = GridLines.Horizontal
        ar = New ArrayList
        objCategory = New CXMLCategoryList
        objCategoryAccess = New CCategories
        Reload()

        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        Else
            DataGrid1.PagerStyle.Visible = True
        End If

    End Sub
#End Region

#Region "Sub Reload()"
    Public Sub Reload()
        ar.Clear()
        DataGrid1.DataSource = um.GetUsers
        '2417
        If DataGrid1.CurrentPageIndex <> 0 AndAlso ar.Count / DataGrid1.CurrentPageIndex <= DataGrid1.PageSize Then
            DataGrid1.CurrentPageIndex -= 1
        End If
        '2417
        DataGrid1.DataBind()
    End Sub
#End Region

#Region "Sub cmdEdit_click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub cmdEdit_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim user As CSRUser = um.GetUser(CLng(CType(sender, LinkButton).CommandArgument))
        RaiseEvent Edit(user.UID, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub cmdDelete_click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Public Sub cmdDelete_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim uid As String
        uid = CType(sender, LinkButton).CommandArgument
        um.DeleteUser(uid)
        RaiseEvent Delete(sender, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged"
    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.DataSource = um.GetUsers
        DataGrid1.DataBind()
    End Sub
#End Region

End Class
