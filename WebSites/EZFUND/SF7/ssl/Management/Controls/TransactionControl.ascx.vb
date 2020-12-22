Imports StoreFront.BusinessRule.Management

Partial  Class TransactionControl
    Inherits System.Web.UI.UserControl

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
    Public Sub SetDisplay(ByVal arrTransactions As ArrayList)
        DataList1.DataSource = arrTransactions
        DataList1.DataBind()
    End Sub

    'Verisign Recurring Billing
    '##SUMMARY - Populate the 

    Private Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound
        Dim listItem As DataListItem
        Dim tranResponse As SystemBase.CProcessorResponse
        listItem = e.Item
        If Not IsNothing(listItem) Then
            tranResponse = e.Item.DataItem
            Dim report As New CSFReports
            CType(listItem.FindControl("rptRecurring"), Repeater).DataSource = report.GetRecurringBillingResponse(tranResponse.OrderID)
            CType(listItem.FindControl("rptRecurring"), Repeater).DataBind()
        End If
    End Sub
    'Verisign Recurring Billing
End Class
