Imports StoreFront.BusinessRule.management
Partial  Class ShippingHandlingControl
    Inherits System.Web.UI.UserControl

    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents AllowMultipleShipTo As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Textbox2 As System.Web.UI.WebControls.TextBox
    Private objMessage As Label
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

    Private objShippingManagement As CShippingManagement

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objMessage = CType(Me.Parent.FindControl("Message"), Label)
        objMessage.Text = ""
        objMessage.Visible = False
        objShippingManagement = New CShippingManagement()
        If (IsPostBack = False) Then

            Call GetShippingTypes()
            Call GetHandlingTypes()
            Call SetControls()
        End If

    End Sub

    Public Sub SetControls()

        If (objShippingManagement.AdminShipping.ShippingType = 1) Then
            ShippingType.SelectedIndex = 2
        ElseIf (objShippingManagement.AdminShipping.ShippingType = 2) Then
            ShippingType.SelectedIndex = 0
        ElseIf (objShippingManagement.AdminShipping.ShippingType = 3) Then
            ShippingType.SelectedIndex = 1
        End If

        If (objShippingManagement.AdminShipping.ApplyHandlingToAllOrders = 1) Then
            ApplyHandlingTo.SelectedIndex = 0
        Else
            ApplyHandlingTo.SelectedIndex = 1
        End If

        If (objShippingManagement.AdminShipping.ApplyTaxToShipping = 1) Then
            ApplyTax.Checked = True
        Else
            ApplyTax.Checked = False
        End If

        If (objShippingManagement.AdminShipping.ApplyHandling = 1) Then
            ApplyHandling.Checked = True
        Else
            ApplyHandling.Checked = False
        End If

        If (objShippingManagement.AdminShipping.ApplyPremium = 1) Then
            ActivatePremium.Checked = True
        Else
            ActivatePremium.Checked = False
        End If

        If (objShippingManagement.AdminShipping.ActivateMultiShip) Then
            chkActivateMultiShip.Checked = True
        Else
            chkActivateMultiShip.Checked = False
        End If

        ShippingMinimum.Text = objShippingManagement.AdminShipping.ShippingMin
        HandlingCharge.Text = objShippingManagement.AdminShipping.HandlingCharge
        MultipleShipToCharge.Text = objShippingManagement.AdminShipping.MultipleShipToCharge
        PremShipName.Text = objShippingManagement.AdminShipping.PremiumName
        PremShipCharge.Text = objShippingManagement.AdminShipping.PremiumCharge

    End Sub

    Public Sub GetShippingTypes()
        ShippingType.DataSource = objShippingManagement.getShipTypeDT
        ShippingType.DataValueField = "ID"
        ShippingType.DataTextField = "Display"
        ShippingType.DataBind()
    End Sub

    Public Sub GetHandlingTypes()
        ApplyHandlingTo.DataSource = objShippingManagement.getApplyHandlingToDT
        ApplyHandlingTo.DataValueField = "ID"
        ApplyHandlingTo.DataTextField = "Display"
        ApplyHandlingTo.DataBind()
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If (ShippingType.SelectedIndex = 2) Then
            objShippingManagement.AdminShipping.ShippingType = 1
        ElseIf (ShippingType.SelectedIndex = 0) Then
            objShippingManagement.AdminShipping.ShippingType = 2
        ElseIf (ShippingType.SelectedIndex = 1) Then
            objShippingManagement.AdminShipping.ShippingType = 3
        End If

        If (ApplyHandlingTo.SelectedIndex = 0) Then
            objShippingManagement.AdminShipping.ApplyHandlingToAllOrders = 1
        Else
            objShippingManagement.AdminShipping.ApplyHandlingToAllOrders = 0
        End If

        If (ApplyTax.Checked = True) Then
            objShippingManagement.AdminShipping.ApplyTaxToShipping = 1
        Else
            objShippingManagement.AdminShipping.ApplyTaxToShipping = 0
        End If

        If (ApplyHandling.Checked = True) Then
            objShippingManagement.AdminShipping.ApplyHandling = 1
        Else
            objShippingManagement.AdminShipping.ApplyHandling = 0
        End If

        If (ActivatePremium.Checked = True) Then
            objShippingManagement.AdminShipping.ApplyPremium = 1
        Else
            objShippingManagement.AdminShipping.ApplyPremium = 0
        End If

        If (chkActivateMultiShip.Checked = True) Then
            objShippingManagement.AdminShipping.ActivateMultiShip = True
        Else
            objShippingManagement.AdminShipping.ActivateMultiShip = False
        End If

        objShippingManagement.AdminShipping.ShippingMin = ShippingMinimum.Text
        objShippingManagement.AdminShipping.HandlingCharge = HandlingCharge.Text
        objShippingManagement.AdminShipping.MultipleShipToCharge = MultipleShipToCharge.Text
        objShippingManagement.AdminShipping.PremiumName = PremShipName.Text
        objShippingManagement.AdminShipping.PremiumCharge = PremShipCharge.Text
        objShippingManagement.update()
        objMessage.Text = "Your changes have been saved"
        objMessage.Visible = True
    End Sub
End Class
