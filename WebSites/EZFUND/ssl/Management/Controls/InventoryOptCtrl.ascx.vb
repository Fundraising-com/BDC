Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Public MustInherit Class InventoryOptCtrl
    Inherits System.Web.UI.UserControl
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents chkTrack As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkAllowBO As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkNotify As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkStatus As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtLowFlag As System.Web.UI.WebControls.TextBox

    Private oInventory As Inventory_Management
    Protected WithEvents txtDefaultQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdApplyInventory As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdApplyNotify As System.Web.UI.WebControls.LinkButton
    Private _iProdID As Long
    Event ShowInventory As EventHandler
    Event SetDefaultLevel As EventHandler
    Event SetDefaultLowFlag As EventHandler
    Event InventoryError As EventHandler
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
        _iProdID = Request.QueryString("ProductID")
        If _iProdID = 0 Then
            _iProdID = Session("ProductID")

        End If

        If Not IsPostBack Then
            If _iProdID <> 0 Then
                oInventory = New Inventory_Management(_iProdID)
                LoadForm()
            End If



        End If
    End Sub
    Private Sub LoadForm()
        Try
            chkTrack.Checked = oInventory.Inventory.InventoryTracked
            txtDefaultQty.Text = oInventory.Inventory.DefaultQuantity
            chkAllowBO.Checked = oInventory.Inventory.CanBackOrder
            chkStatus.Checked = oInventory.Inventory.ShowStatus
            chkNotify.Checked = oInventory.Inventory.NotifyWhenLow
            txtLowFlag.Text = oInventory.Inventory.LowFlag
            SetInventoryDisplay()
            Session("Inventory") = oInventory
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, EventArgs.Empty)

        End Try
    End Sub

    Private Sub cmdApplyInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyInventory.Click
        Try
            oInventory = Session("Inventory")
            If Not IsNothing(oInventory) Then
                If IsNumeric(txtDefaultQty.Text) Then
                    oInventory.Inventory.DefaultQuantity = txtDefaultQty.Text
                    oInventory.SetDefaultQty()
                    RaiseEvent SetDefaultLevel(txtDefaultQty.Text, e)
                End If
            End If
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, e)

        End Try
    End Sub

    Private Sub cmdApplyNotify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyNotify.Click
        Try
            oInventory = Session("Inventory")
            If Not IsNothing(oInventory) Then
                If IsNumeric(txtLowFlag.Text) Then
                    oInventory.Inventory.NotifyWhenLow = chkNotify.Checked
                    oInventory.Inventory.LowFlag = txtLowFlag.Text
                    oInventory.SetNotifyInfo()
                    RaiseEvent SetDefaultLowFlag(txtLowFlag.Text, e)
                End If
            End If
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, e)

        End Try
    End Sub


    Private Sub chkAllowBO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllowBO.CheckedChanged
        Try
            oInventory = Session("Inventory")
            If Not IsNothing(oInventory) Then
                oInventory.Inventory.CanBackOrder = chkAllowBO.Checked
                oInventory.SetBackOrder()
            End If
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, e)

        End Try
    End Sub
    Private Sub chkStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        Try
            oInventory = Session("Inventory")
            If Not IsNothing(oInventory) Then
                oInventory.Inventory.ShowStatus = chkStatus.Checked
                oInventory.SetShowStatus()
            End If
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, e)

        End Try
    End Sub
    Private Sub chkTrack_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTrack.CheckedChanged
        Try
            oInventory = Session("Inventory")
            If Not IsNothing(oInventory) Then
                oInventory.Inventory.InventoryTracked = chkTrack.Checked
                oInventory.Write_Inventory()
                SetInventoryDisplay()
            End If
        Catch err As SystemException
            RaiseEvent InventoryError(err.Message, e)

        End Try


    End Sub

    Public Sub SetInventoryDisplay()
        RaiseEvent ShowInventory(oInventory, System.EventArgs.Empty)

    End Sub


End Class
