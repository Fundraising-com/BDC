Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Imports System

Public MustInherit Class addpricegroups
    Inherits CWebControl
    Protected WithEvents cboPriceOptions As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPercentChange As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSelect As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cboPriceChoices As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnPriceChoice As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents trMemberButton As System.Web.UI.HtmlControls.HtmlTableRow
    Protected objPriceGroup As CPriceGroup
    Protected WithEvents trPercent As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trPriceChoiceDD As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trPriceChoiceButton As System.Web.UI.HtmlControls.HtmlTableRow
    Protected strErrorMessage As String


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
    Event Add As EventHandler
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

        objPriceGroup = New CPriceGroup()
        If Not (IsPostBack) Then
            trMemberButton.Visible = False
            Me.trPriceChoiceButton.Visible = False
            Me.trPriceChoiceDD.Visible = False
            FillDropDowns()
            ClearFields()
        ElseIf (IsNothing(Session("EditClick"))) Then
            'If Me.cboPriceOptions.SelectedIndex = 0 Or Me.cboPriceOptions.SelectedIndex = 1 Then
            '    Me.txtPercentChange.Text = 0
            '    trPercent.Visible = False
            'Else
            '    trPercent.Visible = True
            'End If
            'RaiseEvent Add(Nothing, EventArgs.Empty)
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
        Dim i As Integer
        Me.txtGroupName.Text = priceGroup.Name
        Me.txtPercentChange.Text = priceGroup.Amount * 100

        cboPriceOptions.SelectedIndex = priceGroup.GroupID

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
        'objPriceGroup.ID = CType(Parent.FindControl("txtGroupIDHidden"), HtmlInputHidden).Value
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

        'Fill Pricing Application
        Dim myList2 As New ArrayList()
        myList2.Add("All Products")
        myList2.Add("Selected Products")
        myList2.Add("Category")
        myList2.Add("Manufacturer")
        myList2.Add("Vendor")
        Me.cboPriceChoices.DataSource = myList2
        cboPriceChoices.DataBind()

    End Sub
#End Region

#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtPercentChange.Text = 0
        txtGroupName.Text() = ""
        cboPriceChoices.SelectedIndex = 0
        cboPriceOptions.SelectedIndex = 0
        Me.trPercent.Visible = False
    End Sub
#End Region


#Region "Function Validate() As Boolean"
    Public Function Validate() As Boolean
        If Me.txtGroupName.Text = "" Then
            strErrorMessage = "Please fill in the group name."
            Return False
        End If

        Return True
    End Function
#End Region

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
        RaiseEvent Add(Nothing, EventArgs.Empty)
    End Sub
#End Region

    '-----------------------------------------------------------
    ' Sub btnSelect_Click
    ' Parameters: System.Object, System.EventArgs
    ' Description:
    '   Handles the btnSelect button click
    '   Raises the SelectMembers event
    '-----------------------------------------------------------
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        FillPriceGroup()
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
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Validate() Then
            FillPriceGroup()
            objPriceGroup.AddPriceGroup()
            ClearFields()
            RaiseEvent Save(objPriceGroup, EventArgs.Empty)
        Else
            RaiseEvent Add(Nothing, EventArgs.Empty)
        End If
    End Sub
End Class
