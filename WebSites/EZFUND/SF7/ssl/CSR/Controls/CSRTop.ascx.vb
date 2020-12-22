Imports StoreFront.BusinessRule
Imports CSR.CSRBusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports System.Web.Security

Partial  Class CSRTop
    Inherits CSRWebControl

#Region "Class Events"
    Event RecalculateOrder()
    Event ClearFormNow()
    
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


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblCSRName.Text = Session("CSRUserName")
    End Sub


    Private Sub SignOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SignOut.Click
        SignoutNow()
    End Sub
    Public Sub SignoutNow()
        ResetForm()
        FormsAuthentication.SignOut()
        Response.Redirect("OrderForm.aspx")
    End Sub
    Private Sub ClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearForm.Click
        RaiseEvent ClearFormNow()
        Response.Redirect("OrderForm.aspx")
    End Sub
    Public Sub ResetForm()
        Session("NewCust") = 0
        Session("CSRNewProduct") = Nothing
        Session("CSROrder") = Nothing
        Session("CSRCustomer") = Nothing
        Session("CSRSelectedCustomer") = Nothing
        Session("CSRBillAddress") = "-1"
        Session("CSRShipAddress") = "-1"
        Session("CSRFirstName") = ""
        Session("CSRLastName") = ""
        Session("CSREmail") = ""

        Dim CSRCustomer As CCustomer
        Dim objStoreDiscounts As New CStoreDiscounts
        Dim Order As CSROrder
        Dim objOrderAddress As CSROrderAddress
        Dim ShipAddress As New AddressOrder

        CSRCustomer = New CCustomer(Guid.NewGuid().ToString(), StoreFrontConfiguration.XMLDocument)
CSRCustomer.AddressList.Clear() ' this clears out any Addresses that had customerID's set to '1        
Order = New CSROrder(CSRCustomer, StoreFrontConfiguration.SiteURL)
        objOrderAddress = New CSROrderAddress(ShipAddress)
        Order.AddCSROrderAddress(objOrderAddress)
        objOrderAddress.ShippingObject.RefreshShippingAmount = True
        Order.StoreDiscounts = objStoreDiscounts.GetDiscounts()
        Order.Coupons = m_objxmlcart.AppliedDiscounts

        Session("CSROrder") = Order
        Session("CSRCustomer") = CSRCustomer
        'RaiseEvent RecalculateOrder()

    End Sub
End Class
