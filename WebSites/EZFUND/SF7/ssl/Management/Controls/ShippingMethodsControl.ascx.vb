Imports StoreFront.BusinessRule.management
Partial  Class ShippingMethodsControl
    Inherits System.Web.UI.UserControl

    Private objShippingManagement As New CShippingManagement()
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
        If (IsPostBack = True) Then
        Else
            CarrierCode.Value = Request.QueryString("CarrierCode")
            Call GetShippingMethods()
            DataBind()
            If Request.QueryString("CarrierCode").ToUpper = "UPS" Then
                UPS.Visible = True
            Else
                UPS.Visible = False
            End If
            End If
    End Sub

    Public Sub GetShippingMethods()
        Methods.DataSource = objShippingManagement.getCarrierMethods(Request.QueryString("CarrierCode"))
        Methods.DataBind()
    End Sub

    Private Sub Methods_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Methods.ItemCommand
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim item As RepeaterItem
        Dim hid As HtmlControls.HtmlInputHidden
        Dim rate As TextBox
        Dim chkActive As CheckBox
        For Each item In Methods.Items
            hid = CType(item.FindControl("MethodCode"), HtmlControls.HtmlInputHidden)
            chkActive = CType(item.FindControl("Activate"), CheckBox)
            rate = CType(item.FindControl("rate"), TextBox)
            objShippingManagement.UpdateShippingMethod(hid.Value, chkActive.Checked, rate.Text)
            'hid.Value = item.datitem("Code")

        Next
    End Sub
End Class
