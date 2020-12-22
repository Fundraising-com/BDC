Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports csr.CSRBusinessRule
Partial Class AddEditUserRoles
    Inherits CWebControl

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
    Event SaveClick As EventHandler
    Event CancelClick As EventHandler
    Private _roleId As Long
    Property RoleId() As Long
        Get
            If Not IsNothing("RoleId") Then
                _roleId = Session("roleId")
            End If
            Return _roleId
        End Get
        Set(ByVal Value As Long)
            _roleId = Value
        End Set
    End Property
    Private _Title As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal Value As String)
            _Title = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        SetTitle()
        cmdSave.Attributes.Add("onclick", "return SetValidation();")
        If Not IsPostBack Then
            BindManagementCheckBox()
            If Not CSRManagement.HasCSRLicense() Then
                Me.chkManageCSROptions.Enabled = False
            End If
            If Not StoreFrontConfiguration.AreMerchantBundlesActive Then
                Me.chkBundleComponents.Enabled = False
            End If
            If Not StoreFrontConfiguration.AreCustomerBundlesActive Then
                Me.chkCustomerDefinedRules.Enabled = False
            End If
            If Not StoreFrontConfiguration.AreWebServicesActive Then
                Me.chkWebServices.Enabled = False
            End If
        End If
    End Sub
    Sub BindData()
        Dim objAdminMana As New CAdminUser
        _roleId = Session("roleId")
        Dim objRole As UserRoleBase = objAdminMana.GetRolesByRolesID(_roleId)

        For Each objTasks As RoleTasks In objRole.Tasks
            SetCheckBoxes(objTasks.TaskID)
        Next
        txtname.Text = objRole.RoleName
        SetTitle()
        
    End Sub
    Sub BindManagementCheckBox()
        Dim listitm As ListItem
        listitm = New ListItem("Sales Reports", Tasks.SalesReports)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Orders", Tasks.Orders)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Affiliates", Tasks.Affiliates)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Customers", Tasks.Customers)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Price Groups", Tasks.PriceGroups)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Gift Certificates", Tasks.GiftCertificates)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Manage Roles", Tasks.ManageRoles)
        chkManagement.Items.Add(listitm)
        listitm = New ListItem("Manage Administrators", Tasks.ManageAdministrators)
        chkManagement.Items.Add(listitm)

        'bind to marketing and prpomo
        listitm = New ListItem("Storewide Discounts", Tasks.StorewideDiscounts)
        chkMarkPromo.Items.Add(listitm)
        listitm = New ListItem("Coupons", Tasks.Coupons)
        chkMarkPromo.Items.Add(listitm)
        listitm = New ListItem("Promotional Mail", Tasks.PromotionalMail)
        chkMarkPromo.Items.Add(listitm)
        listitm = New ListItem("Search Engines", Tasks.SearchEngines)
        chkMarkPromo.Items.Add(listitm)
        listitm = New ListItem("Market Places", Tasks.Marketplaces)
        chkMarkPromo.Items.Add(listitm)

        'bind to Storedesign
        'listitm = New ListItem("Site Setup", Tasks.SiteSetup)
        'chkDesign.Items.Add(listitm)
        listitm = New ListItem("Layout Templates", Tasks.LayoutTemplates)
        chkDesign.Items.Add(listitm)
        listitm = New ListItem("Themes", Tasks.themes)
        chkDesign.Items.Add(listitm)
        listitm = New ListItem("Custom Pages", Tasks.CustomPages)
        chkDesign.Items.Add(listitm)
    End Sub
    Sub SetTitle()
        lblHeading.Text = hdnTitle.Value
    End Sub
    Sub ClearForm()
        txtname.Text = ""
        UnCheckAll()
    End Sub
  
    Sub SetCheckBoxes(ByVal taskID As Integer)
        If taskID = Tasks.SalesReports OrElse taskID = Tasks.Orders OrElse taskID = Tasks.Affiliates OrElse taskID = Tasks.Customers OrElse taskID = Tasks.PriceGroups OrElse taskID = Tasks.GiftCertificates OrElse taskID = Tasks.ManageRoles OrElse taskID = Tasks.ManageAdministrators Then
            Try
                chkManagement.Items.FindByValue(taskID).Selected = True
            Catch
            End Try
        ElseIf taskID = Tasks.StorewideDiscounts OrElse taskID = Tasks.Coupons OrElse taskID = Tasks.PromotionalMail OrElse taskID = Tasks.SearchEngines OrElse taskID = Tasks.Marketplaces Then
            Try
                chkMarkPromo.Items.FindByValue(taskID).Selected = True
            Catch ex As Exception
            End Try
        ElseIf taskID = Tasks.LayoutTemplates OrElse taskID = Tasks.themes OrElse taskID = Tasks.CustomPages Then
            Try
                chkDesign.Items.FindByValue(taskID).Selected = True
            Catch
            End Try
        Else
            Select Case taskID
                Case Tasks.ManageCSROptions
                    Me.chkManageCSROptions.Checked = True
                Case Tasks.ImportProducts
                    chkImportProd.Checked = True
                Case Tasks.ProductDelete
                    Me.chkProdDelete.Checked = True
                Case Tasks.ProductAddnew
                    chkProdAddNew.Checked = True
                Case Tasks.ProductGeneral
                    chkProdGeneral.Checked = True
                Case Tasks.ProductDetails
                    chkProdDetails.Checked = True
                Case Tasks.ProductCategories
                    chkProdCategories.Checked = True
                Case Tasks.ProductAttributes
                    chkProdAttributes.Checked = True
                Case Tasks.ProductImages
                    Me.chkProdImages.Checked = True
                Case Tasks.Fulfillment
                    chkFulfillment.Checked = True
                Case Tasks.Inventory
                    chkInventory.Checked = True
                Case Tasks.Discounts
                    chkDiscounts.Checked = True
                Case Tasks.Marketing
                    chkMarketing.Checked = True
                Case Tasks.BundleComponents
                    Me.chkBundleComponents.Checked = True
                Case Tasks.CustomerDefinedRules
                    Me.chkCustomerDefinedRules.Checked = True
                Case Tasks.Attributes
                    chkAttributes.Checked = True
                Case Tasks.categories
                    chkCategories.Checked = True
                Case Tasks.Manufacturers
                    chkManufacturers.Checked = True
                Case Tasks.Vendors
                    chkVendors.Checked = True
                Case Tasks.SearchResultFilters
                    chkSearchFilters.Checked = True
                Case Tasks.storeSettingGeneral
                    chkGeneral.Checked = True
                Case Tasks.OnlineChat
                    chkOnlineChat.Checked = True
                Case Tasks.EMail
                    chkEmail.Checked = True
                Case Tasks.Shipping
                    chkShipping.Checked = True
                    'Case Tasks.ShippingHandling
                    '    chkShipping.Checked = True
                    '    chkshippinghandling.Checked = True
                    'Case Tasks.CarrierBasedShipping
                    '    chkShipping.Checked = True
                    '    chkCarrierBasedShipping.Checked = True
                    'Case Tasks.ValueBasedShipping
                    '    chkShipping.Checked = True
                    '    chkValueBasedShipping.Checked = True
                Case Tasks.PaymentMethods
                    chkPaymentMethods.Checked = True
                Case Tasks.OnlineProcessing
                    chkOnlineProcessing.Checked = True
                Case Tasks.Encryption
                    ChkEncryption.Checked = True
                Case Tasks.PayPal
                    chkPayPal.Checked = True
                Case Tasks.Localization
                    chkLocalization.Checked = True
                Case Tasks.Tax
                    chkTax.Checked = True
                Case Tasks.WebServices
                    chkWebServices.Checked = True
                Case Tasks.MappedURLs
                    Me.chkMappedURLs.Checked = True
            End Select
        End If

    End Sub
    Sub UnCheckAll()
        chkManagement.SelectedIndex = -1
        chkMarkPromo.SelectedIndex = -1
        chkDesign.SelectedIndex = -1
        Me.chkManageCSROptions.Checked = False
        chkImportProd.Checked = False
        Me.chkProdDelete.Checked = False
        chkProdAddNew.Checked = False
        chkProdGeneral.Checked = False
        chkProdDetails.Checked = False
        chkProdCategories.Checked = False
        chkProdAttributes.Checked = False
        Me.chkProdImages.Checked = False
        'chkshippinghandling.Checked = False
        chkFulfillment.Checked = False
        'chkCarrierBasedShipping.Checked = False
        'chkValueBasedShipping.Checked = False
        chkInventory.Checked = False
        chkDiscounts.Checked = False
        chkMarketing.Checked = False
        Me.chkBundleComponents.Checked = False
        Me.chkCustomerDefinedRules.Checked = False
        chkAttributes.Checked = False
        chkCategories.Checked = False
        chkManufacturers.Checked = False
        chkVendors.Checked = False
        chkSearchFilters.Checked = False
        chkGeneral.Checked = False
        chkOnlineChat.Checked = False
        chkEmail.Checked = False
        chkShipping.Checked = False
        chkPaymentMethods.Checked = False
        chkOnlineProcessing.Checked = False
        ChkEncryption.Checked = False
        chkPayPal.Checked = False
        chkLocalization.Checked = False
        chkTax.Checked = False
        chkWebServices.Checked = False
        Me.chkMappedURLs.checked = False
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim err As String = ""
        Try
            Dim objAdmin As New CAdminUser
            _roleId = Session("roleId")
            objAdmin.UserRole.UID = _roleId
            objAdmin.UserRole.RoleName = txtname.Text
            objAdmin.UserRole.IsSuper = False
            objAdmin.UserRole.Tasks = GetTasks()
            _roleId = objAdmin.UpdateRole()
            Session("RoleId") = _roleId
            lblHeading.Text = hdnTitle.Value
        Catch ex As Exception
            err = ex.Message
        End Try
        BindData()
        If _roleId > 0 Then
            err &= " Role saved successfully."
        Else
            err &= " Role could not be saved."
        End If
        RaiseEvent SaveClick(err, e)
    End Sub
    Function GetTasks() As ArrayList
        Dim objtasks As RoleTasks
        Dim itm As ListItem
        Dim ar As New ArrayList
        'store management
        For Each itm In chkManagement.Items
            If itm.Selected Then
                objtasks = New RoleTasks
                objtasks.TaskID = itm.Value
                objtasks.TaskName = itm.Text
                objtasks.RoleId = _roleId
                ar.Add(objtasks)
            End If
        Next
        If Me.chkManageCSROptions.Checked AndAlso CSRManagement.HasCSRLicense Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ManageCSROptions
            objtasks.TaskName = Me.chkManageCSROptions.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        ' marketing and promo
        For Each itm In chkMarkPromo.Items
            If itm.Selected Then
                objtasks = New RoleTasks
                objtasks.TaskID = itm.Value
                objtasks.TaskName = itm.Text
                objtasks.RoleId = _roleId
                ar.Add(objtasks)
            End If
        Next
        'store design
        For Each itm In chkDesign.Items
            If itm.Selected Then
                objtasks = New RoleTasks
                objtasks.TaskID = itm.Value
                objtasks.TaskName = itm.Text
                objtasks.RoleId = _roleId
                ar.Add(objtasks)
            End If
        Next
        'store inventory
        If chkImportProd.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ImportProducts
            objtasks.TaskName = chkImportProd.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If

        'products
        If Me.chkProdDelete.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductDelete
            objtasks.TaskName = Me.chkProdDelete.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkProdAddNew.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductAddnew
            objtasks.TaskName = chkProdAddNew.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkProdGeneral.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductGeneral
            objtasks.TaskName = chkProdGeneral.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkProdDetails.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductDetails
            objtasks.TaskName = chkProdDetails.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkProdCategories.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductCategories
            objtasks.TaskName = chkProdCategories.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkProdAttributes.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductAttributes
            objtasks.TaskName = chkProdAttributes.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If Me.chkProdImages.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.ProductImages
            objtasks.TaskName = Me.chkProdImages.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkFulfillment.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Fulfillment
            objtasks.TaskName = chkFulfillment.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkInventory.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Inventory
            objtasks.TaskName = chkInventory.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkDiscounts.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Discounts
            objtasks.TaskName = chkDiscounts.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkMarketing.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Marketing
            objtasks.TaskName = chkMarketing.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If Me.chkBundleComponents.Checked AndAlso StoreFrontConfiguration.AreMerchantBundlesActive Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.BundleComponents
            objtasks.TaskName = Me.chkBundleComponents.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If Me.chkCustomerDefinedRules.Checked AndAlso StoreFrontConfiguration.AreCustomerBundlesActive Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.CustomerDefinedRules
            objtasks.TaskName = Me.chkCustomerDefinedRules.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        'end products
        If chkAttributes.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Attributes
            objtasks.TaskName = chkAttributes.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkCategories.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.categories
            objtasks.TaskName = chkCategories.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkManufacturers.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Manufacturers
            objtasks.TaskName = chkManufacturers.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkVendors.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Vendors
            objtasks.TaskName = chkVendors.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkSearchFilters.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.SearchResultFilters
            objtasks.TaskName = chkSearchFilters.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        'end store inventory
        'store setting
        If chkGeneral.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.storeSettingGeneral
            objtasks.TaskName = chkGeneral.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkOnlineChat.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.OnlineChat
            objtasks.TaskName = chkOnlineChat.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkEmail.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.EMail
            objtasks.TaskName = chkEmail.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        ''shipping
        'If chkshippinghandling.Checked Then
        '    objtasks = New RoleTasks
        '    objtasks.TaskID = Tasks.ShippingHandling
        '    objtasks.TaskName = chkshippinghandling.Text
        '    objtasks.RoleId = _roleId
        '    ar.Add(objtasks)
        '    chkShipping.Checked = True
        'End If
        'If chkCarrierBasedShipping.Checked Then
        '    objtasks = New RoleTasks
        '    objtasks.TaskID = Tasks.CarrierBasedShipping
        '    objtasks.TaskName = chkCarrierBasedShipping.Text
        '    objtasks.RoleId = _roleId
        '    ar.Add(objtasks)
        '    chkShipping.Checked = True
        'End If
        'If chkValueBasedShipping.Checked Then
        '    objtasks = New RoleTasks
        '    objtasks.TaskID = Tasks.ValueBasedShipping
        '    objtasks.TaskName = chkValueBasedShipping.Text
        '    objtasks.RoleId = _roleId
        '    ar.Add(objtasks)
        '    chkShipping.Checked = True
        'End If
        If chkShipping.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Shipping
            objtasks.TaskName = chkShipping.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        'payments
        If chkPaymentMethods.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.PaymentMethods
            objtasks.TaskName = chkPaymentMethods.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkOnlineProcessing.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.OnlineProcessing
            objtasks.TaskName = chkOnlineProcessing.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If ChkEncryption.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Encryption
            objtasks.TaskName = ChkEncryption.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkPayPal.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.PayPal
            objtasks.TaskName = chkPayPal.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        'store setting
        If chkLocalization.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Localization
            objtasks.TaskName = chkLocalization.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkTax.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.Tax
            objtasks.TaskName = chkTax.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        If chkWebServices.Checked AndAlso StoreFrontConfiguration.AreWebServicesActive Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.WebServices
            objtasks.TaskName = chkWebServices.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        For Each itm In Me.chkDesign.Items
            If itm.Selected Then
                objtasks = New RoleTasks
                objtasks.TaskID = itm.Value
                objtasks.TaskName = itm.Text
                objtasks.RoleId = _roleId
                ar.Add(objtasks)
            End If
        Next
        If Me.chkMappedURLs.Checked Then
            objtasks = New RoleTasks
            objtasks.TaskID = Tasks.MappedURLs
            objtasks.TaskName = Me.chkMappedURLs.Text
            objtasks.RoleId = _roleId
            ar.Add(objtasks)
        End If
        Return ar
    End Function
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        BindData()
        RaiseEvent CancelClick(sender, e)
    End Sub
End Class
