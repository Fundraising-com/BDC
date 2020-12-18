Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase


Partial  Class CInventoryControl
    Inherits System.Web.UI.UserControl

#Region "members"
    Protected WithEvents Status As System.Web.UI.WebControls.Panel
    Protected WithEvents InventoryInfo As System.Web.UI.WebControls.LinkButton
    ' Private _ProdId As Long = 0
    Protected WithEvents cmdInventoryInfo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents pID As System.Web.UI.WebControls.TextBox
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


        ' begin: JDB - Dynamic Image Suite
        Dim lngId As Long = 0
        lngId = Request.QueryString("ProdID")

        If Request.QueryString("ProdID") <> "" Then
            Me.Data_Bind(lngId)
        End If
        ' end: JDB - Dynamic Image Suite

        If (IsNothing(lblStockInfo) = False) Then
            lblStockInfo.Text = StoreFrontConfiguration.Labels.Item("lblStockInfo").InnerText()
        End If
    End Sub
#End Region

    ' begin: JDB - Dynamic Image Suite
    Private Sub Data_Bind(ByVal lProductId As Long)
        Dim objInventory As New CInventory
        Dim oInventoryItems As ArrayList = objInventory.Inventory_Items(lProductId)
        For iInventoryItemIndex As Integer = oInventoryItems.Count - 1 To 0 Step -1
            If Not oInventoryItems(iInventoryItemIndex).Valid Then
                oInventoryItems.RemoveAt(iInventoryItemIndex)
            End If
        Next
        dlInventory.DataSource = oInventoryItems
        dlInventory.DataBind()
    End Sub
    ' end: JDB - Dynamic Image Suite

#Region "Public WriteOnly Property ProductID() As Long"
    Public WriteOnly Property ProductID() As Long
        Set(ByVal Value As Long)
            Dim obj As New CInventory

            ' begin: JDB - Dynamic Image Suite
            Me.Data_Bind(Value)
            ' end: JDB - Dynamic Image Suite

        End Set
    End Property
#End Region

#Region "Public WriteOnly Property Product() As CCategoryStorage"
    Public WriteOnly Property Product() As CCategoryStorage
        Set(ByVal Value As CCategoryStorage)
            _obj = Value
            ' LoadME()
            ' Status.Visible = False
            ' begin: JDB - Dynamic Image Suite
            Me.Data_Bind(_obj.ProductID)
            ' end: JDB - Dynamic Image Suite
        End Set
    End Property
#End Region

End Class
