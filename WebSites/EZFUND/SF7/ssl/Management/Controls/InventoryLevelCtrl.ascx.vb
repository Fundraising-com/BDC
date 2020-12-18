Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Partial  Class InventoryLevelCtrl
    Inherits System.Web.UI.UserControl
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblItems As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtQtyLowFlag As System.Web.UI.WebControls.TextBox

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

    Public Sub SetME(ByVal obj As Inventory_Management)
        ' begin: JDB - Always display inventory information
        'If obj.Inventory.InventoryTracked Then
        Me.Visible = True
        dlInventory.DataSource = obj.InventoryItems
        dlInventory.DataBind()
        'Else
        '    Me.Visible = False
        'End If
        ' end: JDB - Always display inventory information
    End Sub

    Public Sub SetLowFlag(ByVal lngQty As Long)
        Dim oInventory As Inventory_Management = Session("Inventory")
        If Not IsNothing(oInventory) Then
            Dim ds As New DataSet
            Dim obj As CInventoryItem
            Dim oItem As DataListItem
            Dim ar As New ArrayList
            For Each oItem In dlInventory.Items
                obj = New CInventoryItem
                obj.uid = CType(oItem.FindControl("txtuid"), TextBox).Text
                CType(oItem.FindControl("txtQtyLowFlag"), TextBox).Text = lngQty
                obj.Quantity = CType(oItem.FindControl("txtQty"), TextBox).Text
                obj.Quantity_Low_Flag = lngQty
                ar.Add(obj)
            Next
            oInventory.UpDateInventory(ar)
        End If


    End Sub


    Public Sub SetDefaultLevel(ByVal lngQty As Long)
        Dim oInventory As Inventory_Management = Session("Inventory")
        If Not IsNothing(oInventory) Then
            Dim ds As New DataSet
            Dim obj As CInventoryItem
            Dim oItem As DataListItem
            Dim ar As New ArrayList
            For Each oItem In dlInventory.Items
                obj = New CInventoryItem
                CType(oItem.FindControl("txtQty"), TextBox).Text = lngQty
                obj.uid = CType(oItem.FindControl("txtuid"), TextBox).Text
                obj.Quantity = lngQty
                obj.Quantity_Low_Flag = CType(oItem.FindControl("txtQtyLowFlag"), TextBox).Text
                ar.Add(obj)
            Next
            oInventory.UpDateInventory(ar)
        End If


    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim oInventory As Inventory_Management = Session("Inventory")
        If Not IsNothing(oInventory) Then
            Dim ds As New DataSet
            Dim obj As CInventoryItem
            Dim oItem As DataListItem
            Dim ar As New ArrayList
            For Each oItem In dlInventory.Items
                obj = New CInventoryItem
                obj.uid = CType(oItem.FindControl("txtuid"), TextBox).Text
                obj.Quantity = CType(oItem.FindControl("txtQty"), TextBox).Text
                obj.Quantity_Low_Flag = CType(oItem.FindControl("txtQtyLowFlag"), TextBox).Text
                'sp7
                obj.Sku = CType(oItem.FindControl("txtSku"), TextBox).Text
                ' begin: JDB - Dynamic Image Suite
                obj.Valid = CType(oItem.FindControl("chkValid"), CheckBox).Checked
                ' end: JDB - Dynamic Image Suite
                ar.Add(obj)
            Next
            oInventory.UpDateInventory(ar)
        End If
    End Sub
End Class
