Imports StoreFront.UITools
Imports StoreFront.SystemBase


Public MustInherit Class addcustomer
    Inherits CWebControl
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtCAFirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCALastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAEMail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAConfirmPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkSubscribe As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtCustomerGroups As UITools.SelectValControl
    Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
    Protected objCustomer As Customer
    Protected m_bAdded As Boolean
    Protected WithEvents lblPriceGroups As System.Web.UI.WebControls.Label
    Protected WithEvents trCustomerGroups As System.Web.UI.HtmlControls.HtmlTableRow
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

#Region "Class Events"
    Event Add As EventHandler
#End Region

#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            txtCustomerGroups.Visible = False
            lblPriceGroups.Visible = False
            trCustomerGroups.Visible = False
        Else
            If (txtCustomerGroups.Items.Count <= 0) Then
                txtCustomerGroups.Visible = False
                lblPriceGroups.Visible = False
                trCustomerGroups.Visible = False
            End If
        End If

    End Sub
#End Region

#Region "Properties"
    Public Property Message() As String
        Get
            Return strErrorMessage
        End Get
        Set(ByVal Value As String)
            strErrorMessage = Value
        End Set
    End Property

    Public Property IsAdded() As Boolean
        Get
            Return m_bAdded
        End Get
        Set(ByVal Value As Boolean)
            m_bAdded = Value
        End Set
    End Property
#End Region



#Region "Sub ValidateInput()"
    Private Sub ValidateInput()
        If txtCAFirstName.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankFirstName")

        ElseIf txtCALastName.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankLastName")

        ElseIf txtCAEMail.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankEMailAddress")

        ElseIf txtCAPassword.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankPassword")

        ElseIf txtCAConfirmPassword.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankPassword")

        ElseIf Not (txtCAPassword.Text = txtCAConfirmPassword.Text) Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "PasswordsNotMatch")

        Else
            strErrorMessage = ""
        End If
    End Sub
#End Region

#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtCustomerGroups.SelectedIndex = 0
        txtCAFirstName.Text = ""
        txtCALastName.Text = ""
        txtCAEMail.Text = ""
        txtCAPassword.Text = ""
        chkSubscribe.Checked = False
    End Sub
#End Region

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        strErrorMessage = ""
        ValidateInput()
        If strErrorMessage = "" Then
            objCustomer = New Customer()
            If (txtCustomerGroups.Items.Count > 0) Then
                objCustomer.CustomerGroup = txtCustomerGroups.SelectedItem.Value
            Else
                objCustomer.CustomerGroup = 0
            End If

            objCustomer.FirstName = Me.txtCAFirstName.Text
            objCustomer.LastName = txtCALastName.Text
            objCustomer.Email = txtCAEMail.Text
            objCustomer.PassWord = txtCAPassword.Text
            objCustomer.Subscribed = chkSubscribe.Checked
            Try
                If Not (m_objcustomer.AddCustomer(objCustomer)) Then
                    strErrorMessage = Me.m_objMessages.GetXMLMessage("addcustomer.aspx", "Error", "DuplicateCustomer")
                End If
            Catch err As Exception
                strErrorMessage = err.Message
            End Try
            If strErrorMessage = "" Then
                IsAdded = True
                strErrorMessage = Me.m_objMessages.GetXMLMessage("addcustomer.aspx", "AddCustomer", "Success")
            Else
                IsAdded = False
            End If
        Else
            IsAdded = False
        End If
        RaiseEvent Add(IsAdded, EventArgs.Empty)
    End Sub
End Class
