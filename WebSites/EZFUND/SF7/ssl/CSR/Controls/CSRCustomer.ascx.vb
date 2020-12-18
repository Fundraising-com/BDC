Imports StoreFront.BusinessRule
Imports CSR.CSRBusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports StoreFront.UITools
Imports System.text
Imports CSR.CSRSystemBase
Partial  Class CSRCustomer
    Inherits CSRWebControl
    'Protected WithEvents MoveRight As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents IgnoreShipChange As TextBox
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

#Region "Class Events"
    Event RecalculateOrder()

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetOrder()
        SetAddressListsVisiblility()
        
        SetEnterKeyPostBack(Me.FirstName, Me.Search)
        SetEnterKeyPostBack(Me.LastName, Me.Search)
        SetEnterKeyPostBack(Me.Email, Me.Search)
    End Sub
    Public Sub SetAddressListsVisiblility()
        If (CSRCustomer.AddressList.Count = 0) Then
            ShipAddressList.Visible = False
            BillAddresslist.Visible = False
        Else
            If csrorder.OrderAddresses.Count > 1 Then
                ShipAddressList.Visible = False
            Else
                ShipAddressList.Visible = True

            End If

            BillAddresslist.Visible = True
        End If
    End Sub
    'Private Sub SetEnterKeyPostBack(ByRef objTextControl As TextBox, ByRef objButton As LinkButton, Optional ByVal argument As String = "", Optional ByVal condition As String = "")

    '    If IsNothing(objTextControl) = False And IsNothing(objButton) = False Then
    '        Dim script As New StringBuilder
    '        script.Append("if (isEnterKey(event)")
    '        If Not IsNothing(condition) AndAlso condition <> String.Empty Then
    '            script.Append(" && " & condition)
    '        End If
    '        script.Append(") ")
    '        script.Append("postBack(event, '" & objButton.UniqueID & "', '" & argument & "')")
    '        objTextControl.Attributes.Add("onKeyDown", script.ToString)
    '    End If

    'End Sub
    Private Sub Page_Prerender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

    End Sub
    Private Sub setPreselectedAddresses()
        Dim x As Integer = 0
        Dim ddListItem As ListItem

        If ShipAddressList.Visible = True Then
            If IsNothing(Session("CSRShipAddress")) = False Then
                For Each ddListItem In ShipAddressList.Items
                    If ddListItem.Value = Session("CSRShipAddress") Then
                        ShipAddressList.SelectedIndex = x
                    End If
                    x = x + 1
                Next

            End If

        End If
        x = 0
        If BillAddresslist.Visible = True Then
            If IsNothing(Session("CSRBillAddress")) = False Then
                For Each ddListItem In BillAddresslist.Items
                    If ddListItem.Value = Session("CSRBillAddress") Then
                        BillAddresslist.SelectedIndex = x
                    End If
                    x = x + 1
                Next
            End If
        End If


    End Sub
    Private Sub clearInputs()
        Me.FirstName.Text = ""
        Me.LastName.Text = ""
        Me.Email.Text = ""

    End Sub
    Public Sub DoSearch()
        GetOrder()
        Dim arList As New ArrayList
        Dim strFirstName As String = Session("CSRFirstName")
        Dim strLastName As String = Session("CSRLastName")
        Dim strEmail As String = Session("CSREmail")
        If IsNothing(strFirstName) Then
            strFirstName = ""
        End If
        If IsNothing(strLastName) Then
            strLastName = ""
        End If
        If IsNothing(strEmail) Then
            strEmail = ""
        End If
        Dim oCustomers As New CSRCustomerHelper
        arList = oCustomers.CustomerSearch(TestInput(strFirstName), TestInput(strLastName), TestInput(strEmail))
        Dim NewCust As New CSRCustomerBase
        NewCust.FirstName = "<New Customer>"
        NewCust.ID = -1
        arList.Insert(0, NewCust)
        Customers.DataTextField = "DisplayName"
        Customers.DataValueField = "ID"
        Customers.DataSource = arList
        Customers.DataBind()
        If IsNothing(Session("CSRSelectedCustomer")) = False AndAlso Session("CSRSelectedCustomer").ToString <> "" Then
            Customers.SelectedIndex = Session("CSRSelectedCustomer")
            FillAddresses()
        Else

            SetCustomer(Customers.SelectedItem.Value)
            Session("CSRBillAddress") = -1
            Session("CSRShipAddress") = -1
            FillAddresses()
            SetAddresses()
        End If
        'clearInputs()
    End Sub
    Public Sub SetCustomer(ByVal CustID As Long)
        Dim Cust As New CSRCustomerHelper

        If CustID <= 0 Then
            CSRCustomer = New CCustomer(Guid.NewGuid().ToString(), StoreFrontConfiguration.XMLDocument)
CSRCustomer.AddressList.Clear() ' this clears out any Addresses that had customerID's set to '1
            'Dim Addr As Address

            CSRCustomer.AddressList().Clear()
            BillEmail.Text = ""
        Else

            Cust.SetCustomer(CustID, CSRCustomer)
        End If
    End Sub
    Private Function TestInput(ByVal strValue As String) As String

        Return strValue.Replace("'", "")

    End Function
    Private Sub Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click
        Session("CSRFirstName") = Me.FirstName.Text
        Session("CSRLastName") = Me.LastName.Text
        Session("CSREmail") = Me.Email.Text
        Session("CSRSelectedCustomer") = Nothing
        DoSearch()
    End Sub
    Public Sub SetCustomer()
        GetOrder()
        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall Then
            'store addresses
            SetBillAddress()
            If CSROrder.OrderAddresses.Count = 1 Then
                SetShipAddress()
            End If
            SetPage()
        Else
            'display addresses if hit refresh
            DisplayCustomer()
        End If



    End Sub

    Public Sub DisplayCustomer()
        SetAddresses()
        'DoSearch()
        FillAddresses()
        SetPage()
    End Sub
    Private Sub SetPage()
        'FillAddresses(CSRCustomer.GetCustomerID)
        Dim cnt As Long = CSROrder.ShippableItemCount

        If cnt > 1 And StoreFrontConfiguration.AdminStore.Item("AllowMultiShip").InnerText = "1" Then

            Me.MapShipments.Visible = True
            Me.ShippingHeaderTable.Visible = True
            MapShipments.Text = "Map Shipments<br>" & PriceDisplay2(StoreFrontConfiguration.AdminShipping.Item("AdditionalAddressHandling").InnerText) & " per additional address"
            If CSROrder.OrderAddresses.Count > 1 Then
                Me.ShipAddressList.Visible = False
                chkMultiShip.Visible = True
                chkMultiShip.Text = "Multiple Addresses"
                Me.chkMultiShip.Checked = True
                Me.MoveRight.Visible = False
                Me.ShippingTable.Visible = False
            Else
                Me.ShippingTable.Visible = True
                Me.MoveRight.Visible = True
                Me.chkMultiShip.Visible = False
            End If

        ElseIf cnt > 0 Then
            chkMultiShip.Visible = False
            Me.MapShipments.Visible = False
            Me.MoveRight.Visible = True
            Me.ShippingHeaderTable.Visible = True
            Me.ShippingTable.Visible = True
        Else
            Me.MoveRight.Visible = False
            Me.ShippingHeaderTable.Visible = False
            Me.ShippingTable.Visible = False
            Me.MapShipments.Visible = False
        End If
    End Sub
    Private Sub Customers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Customers.SelectedIndexChanged


        SetCustomer(Customers.SelectedItem.Value)

        Session("CSRSelectedCustomer") = Customers.SelectedIndex
        CombineAddresses()

        CSROrder.SetCustomerGroup(CSRCustomer.CustomerGroup)
        CSROrder.BillAddress = Nothing
        CType(CSROrder.OrderAddresses(0), CSROrderAddress).SetOrderAddress(New AddressOrder)
        Session("CSRBillAddress") = "-1"
        Session("CSRShipAddress") = "-1"

        If CSRCustomer.AddressList.Count > 0 Then
            Dim AddressOrder As New SystemBase.AddressOrder(CSRCustomer.AddressList(0))
            CSROrder.BillAddress = AddressOrder
            Session("CSRBillAddress") = AddressOrder.ID
        End If
        Call FillAddresses()
        SetAddresses()
        RaiseEvent RecalculateOrder()
    End Sub
    Public Sub FillAddresses()
        Dim Addresses As ArrayList
        Dim objAddress As Address
        Addresses = CSRCustomer.AddressList().Clone

        If (Addresses.Count = 0) Then
            ShipAddressList.Visible = False
            BillAddresslist.Visible = False
        Else
            ShipAddressList.Visible = True
            BillAddresslist.Visible = True

            objAddress = New Address
            objAddress.NickName = "New Address"
            objAddress.ID = -1

            Addresses.Insert(0, objAddress)

            BillAddresslist.DataTextField = "NickName"
            BillAddresslist.DataValueField = "ID"
            BillAddresslist.DataSource = Addresses
            BillAddresslist.DataBind()
            Addresses = CSRCustomer.AddressList().Clone
            Addresses.Insert(0, objAddress)
            ShipAddressList.DataTextField = "NickName"
            ShipAddressList.DataValueField = "ID"
            ShipAddressList.DataSource = Addresses
            ShipAddressList.DataBind()
            setPreselectedAddresses()
        End If

    End Sub

    Private Sub BillAddresslist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillAddresslist.SelectedIndexChanged
        Session("CSRBillAddress") = BillAddresslist.SelectedItem.Value
        Dim AddressOrder As SystemBase.AddressOrder

        If BillAddresslist.SelectedIndex > 0 Then
            AddressOrder = New SystemBase.AddressOrder(CSRCustomer.AddressList(BillAddresslist.SelectedIndex - 1))
            CSROrder.BillAddress = AddressOrder
        Else
            CSROrder.BillAddress = New AddressOrder
        End If

        SetBillingInfo()
    End Sub
    Private Sub SetAddresses()
        SetShippingInfo()
        SetBillingInfo()
    End Sub

    Private Sub SetBillingInfo()
        'Dim Addresses As ArrayList
        Dim objAddress As Address
        Dim i As Integer
        'Addresses = CSRCustomer.AddressList()
        objAddress = CSROrder.BillAddress
        ' Filling in Address Block
        If IsNothing(Session("CSRSelectedCustomer")) = False AndAlso Session("CSRSelectedCustomer") > 0 Then
            trBillEmail.Visible = False
        Else
            trBillEmail.Visible = True
        End If
        If IsNothing(objAddress) = False Then

            'For Each objAddress In Addresses
            'If (objAddress.ID = Session("CSRBillAddress")) Then
            BillNickName.Text = objAddress.NickName
            BillFirstName.Text = objAddress.FirstName
            BillMI.Text = objAddress.MI
            BillLastName.Text = objAddress.LastName
            BillCompany.Text = objAddress.Company
            BillAddress1.Text = objAddress.Address1
            BillAddress2.Text = objAddress.Address2
            BillCity.Text = objAddress.City
            Dim State As String
            State = objAddress.State
            If State = "" Then
                State = "N/A"
            End If
            For i = 0 To BillState.Items.Count - 1
                If BillState.Items(i).Value = State Then
                    BillState.SelectedIndex = i
                End If
            Next
            BillZip.Text = objAddress.Zip
            For i = 0 To BillCountry.Items.Count - 1
                If BillCountry.Items(i).Value = objAddress.Country Then
                    BillCountry.SelectedIndex = i
                End If
            Next
            BillPhone.Text = objAddress.Phone
            BillFax.Text = objAddress.Fax

            'Exit For
            'End If
            '   Next
        Else
            ' Blank all
            BillNickName.Text = ""
            BillFirstName.Text = ""
            BillMI.Text = ""
            BillLastName.Text = ""
            BillCompany.Text = ""
            BillAddress1.Text = ""
            BillAddress2.Text = ""
            BillCity.Text = ""
            BillZip.Text = ""
            BillPhone.Text = ""
            BillFax.Text = ""
            BillCountry.SelectedIndex = 0
            BillState.SelectedIndex = 0
            For i = 0 To BillState.Items.Count - 1
                If BillState.Items(i).Value = "N/A" Then
                    BillState.SelectedIndex = i
                End If
            Next

            For i = 0 To BillCountry.Items.Count - 1
                If BillCountry.Items(i).Value = "US" Then
                    BillCountry.SelectedIndex = i
                End If
            Next
        End If
    End Sub
    Private Sub ShipAddressList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShipAddressList.SelectedIndexChanged

        Session("CSRShipAddress") = ShipAddressList.SelectedItem.Value
        Dim AddressOrder As SystemBase.AddressOrder
        If ShipAddressList.SelectedIndex > 0 Then
            AddressOrder = New SystemBase.AddressOrder(CSRCustomer.AddressList(ShipAddressList.SelectedIndex - 1))
            CType(CSROrder.OrderAddresses(0), CSROrderAddress).SetOrderAddress(AddressOrder)
        Else
            CType(CSROrder.OrderAddresses(0), CSROrderAddress).SetOrderAddress(New AddressOrder)
        End If
        'If IgnoreShipChange.Text <> "1" Then
        SetShippingInfo()
        'Else
        '   IgnoreShipChange.Text = 0
        'End If

    End Sub
    Public Sub SetShippingInfo()
        'Dim Addresses As ArrayList
        Dim objAddress As Address

        'Addresses = CSRCustomer.AddressList()

        ' Filling in Address Block
        objAddress = CType(CSROrder.OrderAddresses(0), CSROrderAddress).Address
        If IsNothing(objAddress) = False Then
            FillShippingFields(objAddress)
        Else
            ' Blank all
            ShipNickName.Text = ""
            ShipFirstName.Text = ""
            ShipMI.Text = ""
            ShipLastName.Text = ""
            ShipCompany.Text = ""
            ShipAddress1.Text = ""
            ShipAddress2.Text = ""
            ShipCity.Text = ""
            ShipZip.Text = ""
            ShipPhone.Text = ""
            ShipFax.Text = ""
            ShipCountry.SelectedIndex = 0
            ShipState.SelectedIndex = 0
            Dim i As Integer
            For i = 0 To ShipState.Items.Count - 1
                If ShipState.Items(i).Value = "N/A" Then
                    ShipState.SelectedIndex = i
                End If
            Next

            For i = 0 To ShipCountry.Items.Count - 1
                If ShipCountry.Items(i).Value = "US" Then
                    ShipCountry.SelectedIndex = i
                End If
            Next
        End If

    End Sub
    Private Sub FillShippingFields(ByVal objAddress As Address)
        ShipNickName.Text = objAddress.NickName
        ShipFirstName.Text = objAddress.FirstName
        ShipMI.Text = objAddress.MI
        ShipLastName.Text = objAddress.LastName
        ShipCompany.Text = objAddress.Company
        ShipAddress1.Text = objAddress.Address1
        ShipAddress2.Text = objAddress.Address2
        ShipCity.Text = objAddress.City
        Dim i As Integer
        Dim State As String
        State = objAddress.State
        If State = "" Then
            State = "N/A"
        End If
        For i = 0 To ShipState.Items.Count - 1
            If ShipState.Items(i).Value = State Then
                ShipState.SelectedIndex = i
            End If
        Next
        ShipZip.Text = objAddress.Zip
        For i = 0 To ShipCountry.Items.Count - 1
            If ShipCountry.Items(i).Value = objAddress.Country Then
                ShipCountry.SelectedIndex = i
            End If
        Next
        ShipPhone.Text = objAddress.Phone
        ShipFax.Text = objAddress.Fax

    End Sub
    Private Sub chkMultiShip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMultiShip.CheckedChanged
        'Set Back To 1 Order Address

        CombineAddresses()
        Session("CSRShipAddress") = CType(CSROrder.OrderAddresses(0), CSROrderAddress).Address.ID

        chkMultiShip.Visible = False
        RefillShipping()

        RaiseEvent RecalculateOrder()
    End Sub
    Public Sub RefillShipping()
        Me.FillAddresses()
        Me.SetShippingInfo()
    End Sub
    Private Sub CombineAddresses()

        Dim OrderItems As New ArrayList
        Dim OrderAddress As CSROrderAddress
        Dim OrderItem As CSROrderItem
        Dim Cnt As Integer

        Cnt = CSROrder.OrderAddresses.Count
        If Cnt > 1 Then

            For Each OrderAddress In CSROrder.OrderAddresses
                For Each OrderItem In OrderAddress.OrderItems
                    OrderItems.Add(OrderItem)
                Next
            Next
            Dim x As Integer
            For x = 1 To Cnt - 1
                CSROrder.OrderAddresses.RemoveAt(x)
            Next

            CType(CSROrder.OrderAddresses(0), CSROrderAddress).OrderItems.Clear()
            For Each OrderItem In OrderItems
                OrderItem.AddressID = -1
                CType(CSROrder.OrderAddresses(0), CSROrderAddress).AddCSRItem(OrderItem)
            Next

        End If

        CSROrder.SetPackageIndexes()

    End Sub

    Private Sub MoveRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveRight.Click
        SetBillAddress()
        CType(CSROrder.OrderAddresses(0), CSROrderAddress).SetOrderAddress(CSROrder.BillAddress)

        Session("CSRShipAddress") = CSROrder.OrderAddresses(0).Address.ID
        'setPreselectedAddresses()
        FillShippingFields(CSROrder.OrderAddresses(0).Address)
        FillAddresses()
        MagicAjax.AjaxCallHelper.Write("MoveRight();")
    End Sub
    Private Sub SetBillAddress()


        Dim objAddress As New Address
        If IsNothing(BillAddresslist) = False AndAlso IsNothing(BillAddresslist.SelectedItem) = False Then
            objAddress.ID = BillAddresslist.SelectedItem.Value
        Else
            objAddress.ID = -1
        End If
        objAddress.NickName = BillNickName.Text
        objAddress.FirstName = BillFirstName.Text
        objAddress.MI = BillMI.Text
        objAddress.LastName = BillLastName.Text
        objAddress.Company = BillCompany.Text
        objAddress.Address1 = BillAddress1.Text
        objAddress.Address2 = BillAddress2.Text
        objAddress.City = BillCity.Text
        objAddress.State = BillState.SelectedItem.Value
        objAddress.Zip = BillZip.Text
        objAddress.Country = BillCountry.SelectedItem.Value
        objAddress.Phone = BillPhone.Text
        objAddress.Fax = BillFax.Text
        If CSRCustomer.GetCustomerID > 0 Then
            objAddress.EMail = CSRCustomer.Email
        Else
            objAddress.EMail = BillEmail.Text
        End If
        Dim AddressOrder As SystemBase.AddressOrder
        AddressOrder = New SystemBase.AddressOrder(objAddress)
        CSROrder.BillAddress = AddressOrder

    End Sub

    Public Sub SetShipAddress()
        Dim objAddress As New Address
        If IsNothing(ShipAddressList) = False AndAlso IsNothing(ShipAddressList.SelectedItem) = False Then
            objAddress.ID = ShipAddressList.SelectedItem.Value
        Else
            objAddress.ID = -1
        End If

        objAddress.NickName = ShipNickName.Text
        objAddress.FirstName = ShipFirstName.Text
        objAddress.MI = ShipMI.Text
        objAddress.LastName = ShipLastName.Text
        objAddress.Company = ShipCompany.Text
        objAddress.Address1 = ShipAddress1.Text
        objAddress.Address2 = ShipAddress2.Text
        objAddress.City = ShipCity.Text
        objAddress.State = ShipState.SelectedItem.Value
        objAddress.Zip = ShipZip.Text
        objAddress.Country = ShipCountry.SelectedItem.Value
        objAddress.Phone = ShipPhone.Text
        objAddress.Fax = ShipFax.Text
        CType(CSROrder.OrderAddresses(0), CSROrderAddress).SetOrderAddress(New AddressOrder(objAddress))
    End Sub

    Public Function CompleteOrder() As Boolean
        'Update Addresses in DB
        If CheckFields() = True Then
            SetCustomer() 'sets Addresses to info from page
            If Customers.SelectedItem.Value = -1 Then
                If AddNewCustomer() = False Then
                    Return False
                End If
            End If

            AddBillingAddress()
            AddShippingAddress()
            Return True
        Else
            Return False
        End If

    End Function
    Private Function CheckFields() As Boolean
        Dim bReturn As Boolean = False
        Dim Errors As New ArrayList
        Dim ErrorMessage As String = ""
        If (BillNickName.Text = "") Then
            Errors.Add("Billing nickname is required.")
        End If
        If (BillFirstName.Text = "") Then
            Errors.Add("Billing first name is required.")
        End If
        If (BillLastName.Text = "") Then
            Errors.Add("Billing last name is required.")
        End If
        If (BillAddress1.Text = "") Then
            Errors.Add("Billing address is required.")
        End If
        If (BillCity.Text = "") Then
            Errors.Add("Billing city is required.")
        End If
        If (BillPhone.Text = "") Then
            Errors.Add("Billing phone number is required.")
        ElseIf checkPhoneNumber(BillPhone.Text) = False Then
            Errors.Add("Billing phone number is invalid.")
        End If
        If (BillState.SelectedItem.Value = "-1") Then
            Errors.Add("Billing state is required.")
        End If
        If (BillCountry.SelectedItem.Value = "-1") Then
            Errors.Add("Billing country is required.")
        End If
        If BillZip.Text = "" And (BillCountry.SelectedItem.Value = "US" Or BillCountry.SelectedItem.Value = "CA") Then
            Errors.Add("Billing zip code is required.")
        End If

        If BillFax.Text <> "" And checkPhoneNumber(BillFax.Text) = False Then
            Errors.Add("Billing fax number is invalid.")
        End If
        If Me.ShippingTable.Visible = True Then
            If (ShipNickName.Text = "") Then
                Errors.Add("Shipping nickname is required.")
            End If
            If (ShipFirstName.Text = "") Then
                Errors.Add("Shipping first name is required.")
            End If
            If (ShipLastName.Text = "") Then
                Errors.Add("Shipping last name is required.")
            End If
            If (ShipAddress1.Text = "") Then
                Errors.Add("Shipping address is required.")
            End If
            If (ShipCity.Text = "") Then
                Errors.Add("Shipping city is required.")
            End If
            If (ShipPhone.Text = "") Then
                Errors.Add("Shipping phone number is required.")
            ElseIf checkPhoneNumber(ShipPhone.Text) = False Then
                Errors.Add("Shipping phone number is invalid.")
            End If
            If (ShipState.SelectedItem.Value = "-1") Then
                Errors.Add("Shipping state is required.")
            End If
            If (ShipCountry.SelectedItem.Value = "-1") Then
                Errors.Add("Shipping country is required.")
            End If
            If ShipZip.Text = "" And (ShipCountry.SelectedItem.Value = "US" Or ShipCountry.SelectedItem.Value = "CA") Then
                Errors.Add("Shipping zip code is required.")
            End If

            If ShipFax.Text <> "" And checkPhoneNumber(ShipFax.Text) = False Then
                Errors.Add("Shipping fax number is invalid.")
            End If
        End If
        If Errors.Count > 0 Then
            Dim str As String
            ErrorMessage = "Please correct the following:"
            For Each str In Errors
                ErrorMessage = ErrorMessage & vbCrLf & str
            Next
            MagicAjax.AjaxCallHelper.WriteAlert(ErrorMessage)
            bReturn = False
        Else
            bReturn = True
        End If

        Return bReturn
    End Function
    Private Sub MapShipments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapShipments.Click
        Session("CSRSelectedAddresses") = Nothing
        SetBillAddress()
        If Customers.SelectedItem.Value = -1 Then
            If AddNewCustomer() = True Then
                'AddBillingAddress()
                Session("CSRFirstName") = ""
                Session("CSRLastName") = ""
                Session("CSREmail") = ""
                'DoSearch()
                Dim arList As New ArrayList
                Dim NewCust As New CSRCustomerBase
                NewCust.FirstName = BillFirstName.Text
                NewCust.LastName = BillLastName.Text
                NewCust.Email = BillEmail.Text
                NewCust.ID = CSRCustomer.GetCustomerID
                arList.Insert(0, NewCust)
                NewCust = New CSRCustomerBase
                NewCust.ID = -1
                NewCust.FirstName = "<New Customer>"
                arList.Insert(0, NewCust)

                Customers.DataTextField = "DisplayName"
                Customers.DataValueField = "ID"
                Customers.DataSource = arList
                Customers.DataBind()
                Customers.SelectedIndex = 1
                Session("CSRMapShipmentsOrder") = CSROrder
                Session("CSRSelectedCustomer") = 1
                MagicAjax.AjaxCallHelper.Write("CallPage(""CSRMapShipments.aspx"");")
            End If
        Else
            Session("CSRMapShipmentsOrder") = CSROrder
            MagicAjax.AjaxCallHelper.Write("CallPage(""CSRMapShipments.aspx"");")
        End If
        'Remove search Session Variables
    End Sub
    Private Function AddNewCustomer() As Boolean
        Dim strPass As String
        strPass = Now.Ticks & Now.Hour & Now.Minute & Now.Second

        Dim Fields As String = ""
        If BillFirstName.Text.Trim = "" Then
            Fields = Fields & vbCrLf & "Billing First Name"
        End If
        If BillLastName.Text.Trim = "" Then
            Fields = Fields & vbCrLf & "Billing Last Name"
        End If
        If CheckEmail(BillEmail.Text.Trim) = False Then
            Fields = Fields & vbCrLf & "Billing E-Mail"
        End If
        If Fields <> "" Then
            MagicAjax.AjaxCallHelper.WriteAlert("Please enter the following information: " & Fields)
            Return False
        End If
        If (CSRCustomer.AddCustomer(BillFirstName.Text.Trim, BillLastName.Text.Trim, BillEmail.Text.Trim, strPass, False)) Then
            Session("NewCust") = CSRCustomer.GetCustomerID
            Return True
        End If

    End Function
    Private Sub AddBillingAddress()
        Try
            CSRCustomer.AddAddress(CSROrder.BillAddress)
        Catch objErr As Exception
            CSRCustomer.UpdateAddress(CSROrder.BillAddress)
        End Try

    End Sub
    Private Sub AddShippingAddress()
        Try
            CSRCustomer.AddAddress(CSROrder.OrderAddresses(0).address)
        Catch objErr As Exception
            CSRCustomer.UpdateAddress(CSROrder.OrderAddresses(0).address)
        End Try

    End Sub
    Private Function CheckEmail(ByVal strEmail As String) As Boolean
        Dim SubStr As String
        If strEmail <> "" And strEmail.IndexOf("@") <> -1 Then
            SubStr = strEmail.Substring(strEmail.IndexOf("@"))
            If strEmail.IndexOf(".") <> -1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Sub SendConfirmationEmail()
        GetOrder()

        ' Send Confirmation Email
        Dim objConfirmEmail As New StoreFront.Email.CConfirmationEmail
        Dim objCSREmail As New CSR.CSREmail
        Try
            If Me.SendEmail.Checked = True Then
                If CSRCustomer.GetCustomerID = Session("NewCust") Then
                    objCSREmail.SendPassword(CSRCustomer)
                End If
                objConfirmEmail.SendConfirmationEmail(CSROrder, CSRCustomer, m_arEMailContent, True)
            Else
                objConfirmEmail.SendConfirmationEmail(CSROrder, CSRCustomer, m_arEMailContent, False)
            End If
        Catch objError As Exception
            MagicAjax.AjaxCallHelper.WriteAlert(objError.Message)

        End Try




    End Sub
End Class
