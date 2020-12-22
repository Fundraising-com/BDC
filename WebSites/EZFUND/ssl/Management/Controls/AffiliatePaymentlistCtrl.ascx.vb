Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Public MustInherit Class AffiliatePaymentlistCtrl
    Inherits System.Web.UI.UserControl

#Region "Class Members"

	  Protected WithEvents lblId As System.Web.UI.WebControls.Label
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPayment As System.Web.UI.WebControls.Label
    Protected WithEvents txtPayment As System.Web.UI.WebControls.Label
    Protected WithEvents ddAffiliates As System.Web.UI.WebControls.DataList
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ddPayments As System.Web.UI.WebControls.DataList

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

	 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ErrorMessage.Visible = False
        If Not IsPostBack Then
            loadme()
        End If
    End Sub

#End Region    

#Region "Private Sub loadme()"
    '##SUMMARY  Loads Affiliates Dropdown
    Private Sub loadme()
        Dim ar As ArrayList
        Dim objAff As New CAffilateManagment()
        ar = Session("AffList")
        If IsNothing(ar) Then
        Else
            ddAffiliates.DataSource = objAff.LoadAffiliates(ar)
            ddAffiliates.DataBind()
        End If
    End Sub

#End Region    

#Region "Public Sub Edit(ByVal source As Object, ByVal e As System.EventArgs)"

    Public Sub Edit(ByVal source As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = source
        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/editaffiliate.aspx?Affiliate=" & obj.CommandArgument)
    End Sub

#End Region

#Region "Public Sub Void(ByVal source As Object, ByVal e As System.EventArgs)"

    Public Sub Void(ByVal source As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = source
        Dim objAff As New CAffilateManagment()
        objAff.VoidPayment(obj.CommandArgument)
        loadme()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Payment is Void"
    End Sub

#End Region

#Region "Public Sub Pay(ByVal source As Object, ByVal e As System.EventArgs)"

    Public Sub Pay(ByVal source As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = source
        Dim objAff As New CAffilateManagment()
        objAff.MakePayment(obj.CommandArgument)
        loadme()
        Me.ErrorMessage.Visible = True
        Me.ErrorMessage.Text = "Payment Made"
    End Sub


#End Region

#Region "Private Sub ddAffiliates_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ddAffiliates.ItemCreated"

    Private Sub ddAffiliates_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ddAffiliates.ItemCreated
        Dim obj As CAffiliate = e.Item.DataItem
        If Not IsNothing(obj) Then
            CType(e.Item.FindControl("AddressLabel1"), AddressLabel).AddressSource = obj.Address
            ddPayments = CType(e.Item.FindControl("ddPayments"), DataList)
            ddPayments.DataSource = obj.PaymentHistory
            ddPayments.DataBind()
        End If
    End Sub

#End Region

#Region "Private Sub ddPayments_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ddPayments.ItemCreated"

    Private Sub ddPayments_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ddPayments.ItemCreated
        Dim objButton As LinkButton
        objButton = e.Item.FindControl("cmdVoid")
        If IsNothing(objButton) = False Then
            objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Void This Payment?" & "');")
        End If
    End Sub

#End Region

End Class
