Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

Partial  Class ProductGeneralControl
    Inherits CWebControl

    Private lblProdName As Label
    Private M_uid As Long
    Private objProdManagement As CProductManagement
    Private trAdd As System.Web.UI.HtmlControls.HtmlTableRow
    Private trEdit As System.Web.UI.HtmlControls.HtmlTableRow
    Private trAdd2 As System.Web.UI.HtmlControls.HtmlTableRow
    Private trEdit2 As System.Web.UI.HtmlControls.HtmlTableRow
    Dim objError As Label

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

    'Tee 8/9/2007 product configurator
#Region "Properties"
    Public ReadOnly Property GetHeader() As String
        Get
            If trMerchantPrice.Visible Then
                Return "General Merchant Bundle Information"
            ElseIf trCustomPrice.Visible Then
                Return "General Customer Defined Bundle Information"
            ElseIf trNormalPrice.Visible Then
                Return "General Product Information"
            End If
            Return ""
        End Get
    End Property
#End Region
    'end Tee

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        trAdd = CType(Me.Parent.FindControl("Add"), System.Web.UI.HtmlControls.HtmlTableRow)
        trEdit = CType(Me.Parent.FindControl("Edit"), System.Web.UI.HtmlControls.HtmlTableRow)
        trAdd2 = CType(Me.Parent.FindControl("Add2"), System.Web.UI.HtmlControls.HtmlTableRow)
        trEdit2 = CType(Me.Parent.FindControl("Edit2"), System.Web.UI.HtmlControls.HtmlTableRow)
        If IsPostBack = True Then
            M_uid = Session("ProductId")
            'Tee 7/20/2007 product configurator
            ProdUID.Value = M_uid
            'end Tee
        Else
            If Request.QueryString("ID") = "" And Session("ProductId") = 0 Then
                M_uid = 0
                trEdit.Visible = False
                trAdd.Visible = True
                trEdit2.Visible = False
                trAdd2.Visible = True
            Else
                trEdit.Visible = True
                trAdd.Visible = False
                trEdit2.Visible = True
                trAdd2.Visible = False

                M_uid = Request.QueryString("ID")
                If M_uid = 0 Then
                    M_uid = Session("ProductId")
                Else
                    Session("ProductId") = M_uid
                End If
            End If

            objProdManagement = New CProductManagement(M_uid)
            ProdUID.Value = M_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            Session("ProductName") = objProdManagement.Name
            ProductIsActive.Checked = objProdManagement.Active
            ProdCode.Text = objProdManagement.Code
            ProdName.Text = objProdManagement.Name
            ProdNamePlural.Text = objProdManagement.PluralName
            ProdPrice.Text = objProdManagement.Price
            ProdCost.Text = objProdManagement.Cost
            LocalTax.Checked = objProdManagement.ApplyLocalTax
            StateTax.Checked = objProdManagement.ApplyStateTax
            CountryTax.Checked = objProdManagement.ApplyCountryTax
            'Verisign Recurring Billing
            If objProdManagement.ProductType = SystemBase.ProductType.Subscription OrElse _
                objProdManagement.ProductType = ProductType.BundleSubscription OrElse _
                objProdManagement.ProductType = ProductType.CustomizedSubscription Then
                SubscriptionProduct.Checked = True
                subscriptionTable.Visible = True
                txtSubscriptionPrice.Text = objProdManagement.RecurringSubscriptionPrice
                txtBillingDelay.Text = objProdManagement.BillingDelay
                txtSubscriptionTerm.Text = objProdManagement.Term
                PaymentPeriod.SelectedIndex = objProdManagement.PaymentPeriod + 1
            Else
                subscriptionTable.Visible = False
            End If
            'Verisign Recurring Billing
            'Tee 7/19/2007 product configurator
            hidMinPrice.Value = Math.Round(objProdManagement.LowestPrice, 2)
            Dim type As Long = Request.QueryString("ProdType")
            If objProdManagement.ProductType = ProductType.Bundle OrElse _
                objProdManagement.ProductType = ProductType.BundleSubscription OrElse _
                type = ProductType.Bundle OrElse type = ProductType.BundleSubscription Then
                MakeVisible(trMerchantPrice)
            ElseIf objProdManagement.ProductType = ProductType.Customized OrElse _
            objProdManagement.ProductType = ProductType.CustomizedSubscription OrElse _
            type = ProductType.Customized OrElse type = ProductType.CustomizedSubscription Then
                MakeVisible(trCustomPrice)
            Else
                MakeVisible(trNormalPrice)
            End If
            If trPriceSetting.Visible AndAlso M_uid > 0 Then
                If trMerchantPrice.Visible Then
                    lblMerchantPrice.Text = Format(objProdManagement.Price, "c")
                    lblMerchantCost.Text = Format(objProdManagement.Cost, "c")
                End If
                If trCustomPrice.Visible Then
                    lblCustomPrice.Text = objProdManagement.PriceRange
                    lblCustomCost.Text = objProdManagement.CostRange
                End If
                If objProdManagement.ComputePrice Then
                    rbPrice.Checked = False
                    rbAutoPrice.Checked = True
                    txtPrice.Text = ""
                    txtAmount.Text = objProdManagement.PriceChangedAmount
                Else
                    rbPrice.Checked = True
                    rbAutoPrice.Checked = False
                    txtPrice.Text = objProdManagement.Price
                    txtAmount.Text = ""
                End If
                If objProdManagement.PriceUp Then
                    rbPlus.Checked = True
                    rbMinus.Checked = False
                Else
                    rbPlus.Checked = False
                    rbMinus.Checked = True
                End If
                If objProdManagement.PriceChangedType = 0 Then
                    rbDollar.Checked = True
                    rbPercent.Checked = False
                ElseIf objProdManagement.PriceChangedType = 1 Then
                    rbDollar.Checked = False
                    rbPercent.Checked = True
                End If
            End If
            DataBind()
            Call loadManufacturerDD()
            Call loadVendorDD()
            'end Tee
        End If

    End Sub

    Private Sub loadManufacturerDD()
        Dim dt As DataTable

        Dim x As Integer
        dt = objProdManagement.getManufacturersDT
        Manufacturers.DataSource = dt
        Manufacturers.DataValueField = "ID"
        Manufacturers.DataTextField = "Name"
        Manufacturers.DataBind()
        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objProdManagement.Manufacturer Then
                Manufacturers.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub loadVendorDD()
        Dim dt As DataTable

        Dim x As Integer
        dt = objProdManagement.getVendorsDT
        Vendors.DataSource = dt
        Vendors.DataValueField = "ID"
        Vendors.DataTextField = "Name"
        Vendors.DataBind()
        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objProdManagement.Vendor Then
                Vendors.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        objError.Text = ""
        objError.Visible = False
        'Dim bnew As Boolean
        objProdManagement = New CProductManagement(M_uid)

        If M_uid = 0 Then
            If objProdManagement.ProductUID(ProdCode.Text) <> 0 Then
                objError.Text = "Product code already exists."
                objError.Visible = True
                Exit Sub
            End If
        Else
            If objProdManagement.ProductUID(ProdCode.Text) <> 0 And objProdManagement.ProductUID(ProdCode.Text) <> M_uid Then
                objError.Text = "Product code already exists."
                objError.Visible = True
                Exit Sub
            End If
        End If
        'Tee 8/27/2007 product configurator change request 003 bug 155
        'If Not ProductIsActive.Checked AndAlso objProdManagement.uid > 0 AndAlso _
        '(objProdManagement.ProductType = ProductType.Normal OrElse objProdManagement.ProductType = ProductType.Subscription) Then
        '    Dim bundleName As String = objProdManagement.IsPartOfBundle(objProdManagement.uid)
        '    If bundleName <> "" Then
        '        objError.Text = "This product is currently part of '" & bundleName & "' Bundle.<BR>" _
        '        & "Please remove it from the bundle before deactivating this product."
        '        objError.Visible = True
        '        ProductIsActive.Checked = True
        '        Exit Sub
        '    End If
        'End If
        'end Tee
        lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
        lblProdName.Text = objProdManagement.Name
        objProdManagement.Active = ProductIsActive.Checked
        objProdManagement.Code = ProdCode.Text
        objProdManagement.Name = ProdName.Text
        objProdManagement.PluralName = ProdNamePlural.Text
        objProdManagement.Cost = ProdCost.Text
        objProdManagement.Vendor = Vendors.SelectedItem.Value
        objProdManagement.Manufacturer = Manufacturers.SelectedItem.Value
        objProdManagement.ApplyLocalTax = LocalTax.Checked
        objProdManagement.ApplyStateTax = StateTax.Checked
        objProdManagement.ApplyCountryTax = CountryTax.Checked
        'Tee 7/19/2007 product configurator
        Dim prodType As Long = objProdManagement.ProductType
        If Not IsNothing(Request.QueryString("ProdType")) Then
            prodType = Request.QueryString("ProdType")
        End If
        'end Tee
        'Verisign Recurring Billing
        If SubscriptionProduct.Checked = True Then
            'Tee 7/19/2007 product configurator
            If prodType = ProductType.Bundle Then
                objProdManagement.ProductType = ProductType.BundleSubscription
            ElseIf prodType = ProductType.Customized Then
                objProdManagement.ProductType = ProductType.CustomizedSubscription
            ElseIf prodType = ProductType.Normal Then
                objProdManagement.ProductType = SystemBase.ProductType.Subscription
            Else
                objProdManagement.ProductType = prodType
            End If
            'end Tee
            objProdManagement.RecurringSubscriptionPrice = CDec("0" & txtSubscriptionPrice.Text.Trim())
            objProdManagement.BillingDelay = CInt("0" & txtBillingDelay.Text.Trim)
            objProdManagement.Term = CInt("0" & txtSubscriptionTerm.Text.Trim())
            If PaymentPeriod.SelectedItem.Value = -1 Then
                objError.Text = "Please select a payment period for this subscription"
                objError.Visible = True
                Exit Sub
            Else
                objProdManagement.PaymentPeriod = PaymentPeriod.SelectedItem.Value()
            End If
        Else
            'Tee 7/19/2007 product configurator
            If prodType = ProductType.BundleSubscription Then
                objProdManagement.ProductType = ProductType.Bundle
            ElseIf prodType = ProductType.CustomizedSubscription Then
                objProdManagement.ProductType = ProductType.Customized
            ElseIf prodType = ProductType.Subscription Then
                objProdManagement.ProductType = SystemBase.ProductType.Normal
            Else
                objProdManagement.ProductType = prodType
            End If
            'end Tee
            objProdManagement.RecurringSubscriptionPrice = 0
            objProdManagement.BillingDelay = 0
            objProdManagement.Term = 0
            objProdManagement.PaymentPeriod = SystemBase.PaymentPeriod.None
        End If
        If trPriceSetting.Visible Then
            objProdManagement.Cost = 0
            If rbPrice.Checked Then
                objProdManagement.Price = CDec("0" & txtPrice.Text)
                txtAmount.Text = ""
            Else
                objProdManagement.Price = 0
                txtPrice.Text = ""
            End If
        Else
            objProdManagement.Price = ProdPrice.Text
        End If
        objProdManagement.ComputePrice = IIf(rbPrice.Checked, False, True)
        objProdManagement.PriceUp = IIf(rbPlus.Checked, True, False)
        objProdManagement.PriceChangedAmount = IIf(rbPrice.Checked, 0, CDec("0" & txtAmount.Text))
        objProdManagement.PriceChangedType = IIf(rbDollar.Checked, 0, 1)
        If objProdManagement.ProductType = ProductType.Bundle OrElse objProdManagement.ProductType = ProductType.BundleSubscription Then
            lblMerchantPrice.Text = Format(objProdManagement.Price, "c")
            lblMerchantCost.Text = Format(objProdManagement.Cost, "c")
        ElseIf (objProdManagement.ProductType = ProductType.Customized OrElse _
        objProdManagement.ProductType = ProductType.CustomizedSubscription) Then
            lblCustomPrice.Text = objProdManagement.PriceRange
            lblCustomCost.Text = objProdManagement.CostRange
        End If
        'Verisign Recurring Billing
        objProdManagement.update()
        If M_uid = 0 Then
            M_uid = objProdManagement.uid
            trEdit.Visible = True
            trAdd.Visible = False
            trEdit2.Visible = True
            trAdd2.Visible = False
            Session("ProductId") = M_uid
            lblProdName.Text = objProdManagement.Name
            'Tee 7/20/2007 product configurator
            ProdUID.Value = M_uid
            'end Tee
            ' begin: JDB - Always display inventory information
            Dim oInventory As New Inventory_Management(M_uid)
            oInventory.Write_Inventory()
            ' end: JDB - Always display inventory information
        End If
    End Sub

    Private Sub SubscriptionProduct_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubscriptionProduct.CheckedChanged
        If SubscriptionProduct.Checked = True Then
            'Tee 8/27/2007 product configurator change request 003 bug 154
            objProdManagement = New CProductManagement(CInt(ProdUID.Value))
            If objProdManagement.ProductType = ProductType.Normal OrElse _
            objProdManagement.ProductType = ProductType.Subscription Then
                Dim result As String = objProdManagement.IsPartOfBundle(CLng(ProdUID.Value))
                If result <> "" Then
                    objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
                    objError.Text = "This product is currently part of '" & result & "' Bundle.<BR>" _
                    & "Please remove it from the bundle before making this a subscription product."
                    objError.Visible = True
                    SubscriptionProduct.Checked = False
                    Exit Sub
                End If
            End If
            'end Tee
            subscriptionTable.Visible = True
        Else
            subscriptionTable.Visible = False
        End If
    End Sub

    'Tee 7/31/2007 product configurator
#Region "Private Sub MakeVisible(ByVal trVisible As HtmlControls.HtmlTableRow)"
    Private Sub MakeVisible(ByVal trVisible As HtmlControls.HtmlTableRow)
        trVisible.Visible = True
        If trVisible.ID = "trNormalPrice" Then
            MakeCommonVisible(ProductType.Normal, True)
        ElseIf trVisible.ID = "trMerchantPrice" Then
            trPriceSetting.Visible = True
            MakeCommonVisible(ProductType.Bundle, True)
        ElseIf trVisible.ID = "trCustomPrice" Then
            trPriceSetting.Visible = True
            MakeCommonVisible(ProductType.Customized, True)
        End If
    End Sub
#End Region
    'end Tee

End Class
