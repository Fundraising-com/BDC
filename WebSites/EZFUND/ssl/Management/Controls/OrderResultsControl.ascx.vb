Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.Systembase
Public MustInherit Class OrderResultsControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents HistoryTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Private ItemRemoved As Integer = -1
    Event OrderCanceled As EventHandler
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
    End Sub

    Public Sub SetDisplay(ByVal arrSummary As ArrayList)
        If arrSummary.Count < 11 Then
            DataGrid1.AllowPaging = False
        Else
            DataGrid1.AllowPaging = True
            DataGrid1.PageSize = 10
        End If
        Session("arrSummary") = arrSummary

        DataGrid1.DataSource = arrSummary
        DataGrid1.DataBind()
    End Sub

    Public Sub Cancel(ByVal source As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = source
        Dim objManagment As New CManagement(CLng(obj.CommandArgument))
        objManagment.CancelOrder()
        ReloadME()
        RaiseEvent OrderCanceled(obj.CommandArgument, e)
    End Sub

    Public Sub GetStatus(ByVal source As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = source
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/OrderStatus.aspx?OrderId=" & obj.CommandArgument)
    End Sub

    Private Sub DataList1_ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        Dim objButton As LinkButton
        objButton = e.Item.FindControl("cmdCancelOrder")
        If IsNothing(objButton) = False Then
            objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Cancel This Order?" & "');")
        End If
    End Sub

    Private Sub ReloadME()
        Dim iDateRange As Integer = Request.QueryString("DateType")
        Dim sTo As String = Request.QueryString("To")
        Dim sFrom As String = Request.QueryString("From")
        If iDateRange = 0 Then
            Dim objSearch As sfSearchContainer = Session("Search")
            If IsNothing(objSearch) = True Then
                Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/OrderFulfillment.aspx")
            Else
                Dim objManagment As New CManagement(objSearch)
                SetDisplay(objManagment.OrderSummary)
            End If
        Else
            Dim objDate As New CSearchDate(iDateRange, sFrom, sTo)
            Dim iPayment As Integer = Request.QueryString("PaymentStatus")
            Dim iShipping As Integer = Request.QueryString("ShipStaus")
            Dim objManagment As New CManagement(objDate, iShipping, iPayment)
            SetDisplay(objManagment.OrderSummary)
        End If
    End Sub



    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex

        DataGrid1.DataSource = Session("arrSummary")
        DataGrid1.DataBind()
    End Sub
End Class
