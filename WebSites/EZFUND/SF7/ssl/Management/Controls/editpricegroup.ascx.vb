Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Imports System


Partial  Class editpricegroup
    Inherits System.Web.UI.UserControl
    Protected objPriceGroup As CPriceGroup
    Protected strErrorMessage As String
    Dim objCust As CCustomer
    Dim objProd As CStoreProducts
    Dim objManuf As CManufacturer
    Dim objVend As CVendor

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
    Event Edit As EventHandler
    Event Save As EventHandler
    Event SelectMembers As EventHandler

    Public Property ErrorMessage() As String
        Get
            Return strErrorMessage
        End Get
        Set(ByVal Value As String)
            strErrorMessage = Value
        End Set
    End Property

#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not (IsPostBack) Then
            FillDropDowns()
            If Not (IsNothing(Session("GroupID"))) Then
                objPriceGroup = New CPriceGroup(CLng(Session("GroupID")))
                FillValues(objPriceGroup)
            Else
                objPriceGroup = New CPriceGroup
            End If

            'trPercent.Visible = False
        Else
            ' begin: JDB - 7/10/2007 - Customer Price Groups
            'objPriceGroup = New CPriceGroup
            If Not (IsNothing(Session("GroupID"))) Then
                objPriceGroup = New CPriceGroup(CLng(Session("GroupID")))
            Else
                objPriceGroup = New CPriceGroup
            End If
            ' end: JDB - 7/10/2007 - Customer Price Groups
            If Me.cboPriceOptions.SelectedIndex = 0 Or Me.cboPriceOptions.SelectedIndex = 1 Then
                Me.txtPercentChange.Text = 0
                trPercent.Visible = False
            Else
                trPercent.Visible = True
            End If
        End If


    End Sub
#End Region

#Region "Sub FillValues(ByVal priceGroup As CPriceGroup)"
    '-----------------------------------------------------------
    ' Sub FillValues
    ' Parameters: CPriceGroup
    ' Description:
    '   Fills the AddPriceGroupForm
    '-----------------------------------------------------------

    Public Sub FillValues(ByVal priceGroup As CPriceGroup)
        'Dim i As Integer
        Me.txtGroupName.Text = priceGroup.Name
        Me.txtPercentChange.Text = priceGroup.Amount * 100
        FillDropDowns()
        cboPriceOptions.ClearSelection()
        cboPriceOptions.SelectedIndex = CInt(priceGroup.GroupID)
        If priceGroup.GroupID = 0 Or priceGroup.GroupID = 1 Then
            Me.trPercent.Visible = False
            txtPercentChange.Text = 0
        Else
            Me.trPercent.Visible = True
        End If
        ' begin: JDB - 7/10/2007 - Customer Price Groups
        'If Not (IsNothing(Session("ApplyTo"))) Then
        '    If (Session("ApplyTo") <> "100") Then
        '        Me.cboPriceChoices.SelectedIndex = Session("ApplyTo")
        '    Else
        '        cboPriceChoices.SelectedIndex = 1
        '    End If
        'Else
        '    cboPriceChoices.SelectedIndex = 1
        'End If

        ' note: options for cboPriceChoices drop down list by ordinal reference are as follows:
        '   0 - All Products
        '   1 - Selected Products
        '   2 - Category
        '   3 - Vendor
        '   4 - Manufacturer

        ' note: set the GroupID variable here so that the selected applicable items are prepopulated properly on
        '   the multiselect page.
        Session("GroupID") = priceGroup.ID

        Dim iSelectedIndex As Integer = 1
        If priceGroup.AllProducts Then
            iSelectedIndex = 0
        ElseIf priceGroup.Categories.Length > 0 Then
            iSelectedIndex = 2
        ElseIf priceGroup.Vendors.Length > 0 Then
            iSelectedIndex = 3
        ElseIf priceGroup.Manufacturers.Length > 0 Then
            iSelectedIndex = 4
        End If
        'Tee 2/11/2008 bug 1018 fix
        If IsNothing(Session("ApplyTo")) OrElse Session("ApplyTo") = "100" Then
            Me.cboPriceChoices.SelectedIndex = iSelectedIndex
            'Tee 2/13/2008 bug 1118 fix
            If cboPriceChoices.SelectedValue.ToLower = "all products" Then
                btnPriceChoice.Visible = False
            Else
                btnPriceChoice.Visible = True
            End If
            'end Tee
        End If
        'end Tee
        ' end: JDB - 7/10/2007 - Customer Price Groups

        CType(Me.Parent.FindControl("txtGroupIDHidden"), HtmlInputHidden).Value = priceGroup.ID
    End Sub
#End Region

#Region "Sub FillPriceGroup()"
    '-----------------------------------------------------------
    ' Sub FillPriceGroup
    ' Parameters: None
    ' Description:
    '   Fills the CPriceGroupObject
    '-----------------------------------------------------------
    Public Sub FillPriceGroup()
        objPriceGroup.Amount = CDec(Me.txtPercentChange.Text) / 100
        objPriceGroup.GroupID = Me.cboPriceOptions.SelectedIndex
        objPriceGroup.Name = txtGroupName.Text
        objPriceGroup.ID = CType(Parent.FindControl("txtGroupIDHidden"), HtmlInputHidden).Value
    End Sub
#End Region

#Region "Sub FillDropDowns()"
    '-----------------------------------------------------------
    ' Sub FillDropDowns
    ' Parameters: None
    ' Description:
    '   Fills the dropdown lists on the AddPriceGroupControl
    '-----------------------------------------------------------
    Private Sub FillDropDowns()
        Dim myList As New ArrayList()
        'Fill Price Options
        myList.Add("Regular Price")
        myList.Add("Cost")
        myList.Add("Discount from Regular Price")
        myList.Add("Markup from Cost")
        cboPriceOptions.DataSource = myList
        cboPriceOptions.DataBind()
        cboPriceOptions.SelectedIndex = 0
        'Fill Pricing Application
        Dim myList2 As New ArrayList()
        myList2.Add("All Products")
        myList2.Add("Selected Products")
        myList2.Add("Category")
        myList2.Add("Vendor")
        myList2.Add("Manufacturer")
        Me.cboPriceChoices.DataSource = myList2
        cboPriceChoices.DataBind()
        If (IsNothing(Session("ApplyTo")) = False AndAlso CLng(Session("ApplyTo")) <> 6 AndAlso _
        CLng(Session("ApplyTo")) <> 8 AndAlso CLng(Session("ApplyTo")) <> 9) Then
            If Session("ApplyTo") <> "100" Then
                cboPriceChoices.SelectedIndex = CLng(Session("ApplyTo"))
            End If
        Else
            cboPriceChoices.SelectedIndex = 0
        End If
        'Tee 2/13/2008 bug 1118 fix
        If cboPriceChoices.SelectedValue.ToLower = "all products" Then
            btnPriceChoice.Visible = False
        Else
            btnPriceChoice.Visible = True
        End If
        'end Tee
    End Sub
#End Region

    '-----------------------------------------------------------
    ' Sub btnSelect_Click
    ' Parameters: System.Object, System.EventArgs
    ' Description:
    '   Handles the btnSelect button click
    '   Raises the SelectMembers event
    '-----------------------------------------------------------
    Public Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        FillPriceGroup()
        objCust = New CCustomer(StoreFrontConfiguration.XMLDocument)
        Session("GroupID") = Me.objPriceGroup.ID
        Session("ApplyTo") = "100"
        Session("ArrChecked") = objCust.GetCustomerByPriceGroup(objPriceGroup.ID)
        Session("ReturnPage") = "managepricegroups.aspx?Edit=1"
        Response.Redirect("multiselect.aspx")
        RaiseEvent SelectMembers(objPriceGroup, EventArgs.Empty)
    End Sub

    '-----------------------------------------------------------
    ' Sub btnSave_Click
    ' Parameters: System.Object, System.EventArgs
    ' Description:
    '   Handles the btnSave button click
    '   Adds new price group to database
    '   Raises the Save event
    '-----------------------------------------------------------
    Public Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validate() Then
            If Session("NewCustomer") = "1" Then
                Session("NewCustomerID") = Session("ApplyToID")
                Session("NewCustomer") = Nothing
            End If
            If IsNothing(Session("ApplyTo")) Then
                Session("ApplyTo") = cboPriceChoices.SelectedIndex
            End If
            FillPriceGroup()
            'Tee 2/14/2008 bug 1018 fix
            objPriceGroup.UpdatePriceGroup(False)
            'end Tee
            Session("EditClick") = Nothing
            RaiseEvent Save(objPriceGroup, EventArgs.Empty)
        Else
            RaiseEvent Edit(strErrorMessage, EventArgs.Empty)
        End If
    End Sub


#Region "Sub PriceOption_Change(ByVal sender As Object, ByVal e As EventArgs) Handles cboPriceOptions.SelectedIndexChanged"
    Public Sub PriceOption_Change(ByVal sender As Object, ByVal e As EventArgs) Handles cboPriceOptions.SelectedIndexChanged
        FillPriceGroup()
        If objPriceGroup.GroupID = 0 Or objPriceGroup.GroupID = 1 Then
            Me.trPercent.Visible = False
            Me.txtPercentChange.Text = 0
        Else
            trPercent.Visible = True
            txtPercentChange.Text = 0
        End If
        FillValues(objPriceGroup)
        RaiseEvent Edit(Nothing, EventArgs.Empty)
    End Sub
#End Region

#Region "Validate()"
    Public Function Validate() As Boolean
        If Me.txtGroupName.Text = "" Then
            strErrorMessage = "Please fill in a group name."
            Return False
        End If

        Return True
    End Function
#End Region

    Public Sub PriceChoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPriceChoice.Click
        FillPriceGroup()
        Dim arr As New ArrayList()
        Dim arr2 As New ArrayList()
        If Session("NewCustomer") = "1" Then
            Session("NewCustomerID") = Session("ArrChecked")
            Session("NewCustomer") = Nothing
        End If
        Session("ApplyTo") = cboPriceChoices.SelectedIndex
        Session("GroupID") = Me.objPriceGroup.ID
        Session("ReturnPage") = "managepricegroups.aspx?Edit=1"
        If (Me.cboPriceChoices.SelectedIndex = 0) Then
            RaiseEvent Edit(Nothing, EventArgs.Empty)
            Exit Sub
        ElseIf (Me.cboPriceChoices.SelectedIndex = 1) Then
            objProd = New CStoreProducts()
            arr.Clear()
            arr = objProd.GetProductsByPriceGroup(objPriceGroup.ID)
            Dim prod As CProduct
            arr2.Clear()
            For Each prod In arr
                arr2.Add(CLng(prod.ProductID))
            Next
            Session("ArrChecked") = arr2
        ElseIf (Me.cboPriceChoices.SelectedIndex = 2) Then
            'categories
            arr.Clear()
            ' begin: JDB - 7/10/2007 - Customer Price Groups
            'arr.Add(0)
            For Each iValue As Long In objPriceGroup.Categories
                arr.Add(iValue)
            Next
            ' end: JDB - 7/10/2007 - Customer Price Groups
            Session("ArrChecked") = arr
        ElseIf (Me.cboPriceChoices.SelectedIndex = 3) Then
            arr.Clear()
            ' begin: JDB - 7/10/2007 - Customer Price Groups
            'arr.Add(0)
            For Each iValue As Long In objPriceGroup.Vendors
                arr.Add(iValue)
            Next
            ' end: JDB - 7/10/2007 - Customer Price Groups
            Session("ArrChecked") = arr

        ElseIf (Me.cboPriceChoices.SelectedIndex = 4) Then
            arr.Clear()
            ' begin: JDB - 7/10/2007 - Customer Price Groups
            'arr.Add(0)
            For Each iValue As Long In objPriceGroup.Manufacturers
                arr.Add(iValue)
            Next
            ' end: JDB - 7/10/2007 - Customer Price Groups
            Session("ArrChecked") = arr

        End If
        Response.Redirect("multiselect.aspx")
    End Sub

    'Tee 2/13/2008 bug 1118 fix
#Region "Private Sub cboPriceChoices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPriceChoices.SelectedIndexChanged"
    Private Sub cboPriceChoices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPriceChoices.SelectedIndexChanged
        If cboPriceChoices.SelectedValue.ToLower = "all products" Then
            btnPriceChoice.Visible = False
        Else
            btnPriceChoice.Visible = True
        End If
        RaiseEvent Edit("", EventArgs.Empty)
    End Sub
#End Region
    'end Tee
End Class
