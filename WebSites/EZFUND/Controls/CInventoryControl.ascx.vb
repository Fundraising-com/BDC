Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase


Public MustInherit Class CInventoryControl
    Inherits System.Web.UI.UserControl

#Region "members"
    Protected WithEvents dlInventory As System.Web.UI.WebControls.DataList
    Protected WithEvents Status As System.Web.UI.WebControls.Panel
    Protected WithEvents InventoryInfo As System.Web.UI.WebControls.LinkButton
    ' Private _ProdId As Long = 0
    Protected WithEvents cmdInventoryInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents pID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStockInfo As System.Web.UI.WebControls.Label
    Private _obj As CCategoryStorage

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


        Dim objInventory As New CInventory()
        Dim lngId As Long = 0
        lngId = Request.QueryString("ProdID")

        If Request.QueryString("ProdID") <> "" Then
            dlInventory.DataSource = objInventory.Inventory_Items(lngId)
            dlInventory.DataBind()
        End If

        If (IsNothing(lblStockInfo) = False) Then
            lblStockInfo.Text = StoreFrontConfiguration.Labels.Item("lblStockInfo").InnerText()
        End If
    End Sub
#End Region

#Region "Public WriteOnly Property ProductID() As Long"
        Public WriteOnly Property ProductID() As Long
        Set(ByVal Value As Long)
            Dim obj As New CInventory()

            dlInventory.DataSource = obj.Inventory_Items(Value)
            dlInventory.DataBind()

        End Set
    End Property
#End Region

#Region "Public WriteOnly Property Product() As CCategoryStorage"
    Public WriteOnly Property Product() As CCategoryStorage
        Set(ByVal Value As CCategoryStorage)
            _obj = Value
            ' LoadME()
            ' Status.Visible = False
            dlInventory.DataSource = _obj.Inventory.Inventory_Items(_obj.ProductID)
            dlInventory.DataBind()
        End Set
    End Property
#End Region

End Class
