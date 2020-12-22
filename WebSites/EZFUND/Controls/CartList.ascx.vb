Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.Math

Public MustInherit Class CartList
    Inherits CWebControl
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblItem As System.Web.UI.WebControls.Label
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label

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
        ' LoadCart()
    End Sub

    Public Sub LoadCart()
        If (IsNothing(m_objxmlcart) = False) Then
            lblCount.Text = m_objxmlcart.ItemCount
            If (IsNothing(lblItem) = False) Then
                If lblCount.Text = "1" Then
                    lblItem.Text = "Item"
                Else
                    lblItem.Text = "Items"
                End If
            End If
            lblTotal.Text = PriceDisplay2(m_objXMLCart.SubTotal())
        Else
            lblCount.Text = Format(0, "c")
            If (IsNothing(lblItem) = False) Then
                lblItem.Text = "Items"
            End If
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        LoadCart()
    End Sub
End Class
