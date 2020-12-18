Imports StoreFront.BusinessRule
Imports StoreFront.Integration

Partial Class WSConfCtrl
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            lblMessage.Text = String.Empty
            LoadValues()
        End If
    End Sub
    Private Sub LoadValues()

        Dim intConfig As New Configuration

        chkAddedCart.Checked = intConfig.CheckInvOnAddToCart
        chkPlaceOrder.Checked = intConfig.CheckInvOnOrderPlaced
        chkSubmitOrder.Checked = intConfig.SubmitOrderToWebService
        txtInvWS.Text = intConfig.ProductCheckWebServiceUrl
        txtOrderWS.Text = intConfig.NewOrderWebServiceUrl
        txtEmail.Text = intConfig.ErrorEmail

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        chkAddedCart.Checked = False
        chkPlaceOrder.Checked = False
        chkSubmitOrder.Checked = False
        txtInvWS.Text = String.Empty
        txtOrderWS.Text = String.Empty
        txtEmail.Text = String.Empty
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If chkAddedCart.Checked Or chkPlaceOrder.Checked Then
            If txtInvWS.Text = String.Empty Then
                lblMessage.Text = "You must provide the Inventory Check web service Url to activate it."
                Exit Sub
            End If
        End If

        If chkSubmitOrder.Checked Then
            If txtOrderWS.Text = String.Empty Then
                lblMessage.Text = "You must provide the Submit Order web service Url to activate it."
                Exit Sub
            End If
        End If
        Try

            Dim intconfig As New Configuration

            intConfig.CheckInvOnAddToCart = chkAddedCart.Checked
            intConfig.CheckInvOnOrderPlaced = chkPlaceOrder.Checked
            intConfig.SubmitOrderToWebService = chkSubmitOrder.Checked
            intConfig.ProductCheckWebServiceUrl = txtInvWS.Text
            intConfig.NewOrderWebServiceUrl = txtOrderWS.Text
            intConfig.ErrorEmail = txtEmail.Text

            intconfig.Save()

            lblMessage.Text = "The data was successfully saved."
        Catch ex As Exception
            lblMessage.Text = "There was a problem saving the data."
        End Try

    End Sub
End Class

Public Class WSDatatable
    Inherits DataTable

    Public Sub New()
        Me.Columns.Add(New DataColumn("Check_ItemToCart", GetType(Boolean)))
        Me.Columns.Add(New DataColumn("Check_BeforeOrder", GetType(Boolean)))
        Me.Columns.Add(New DataColumn("InvWebServiceUrl", GetType(String)))
        Me.Columns.Add(New DataColumn("SubmitOrder", GetType(Boolean)))
        Me.Columns.Add(New DataColumn("OrderWebServiceUrl", GetType(String)))
        Me.Columns.Add(New DataColumn("ErrorNotificationEmail", GetType(String)))
    End Sub
End Class