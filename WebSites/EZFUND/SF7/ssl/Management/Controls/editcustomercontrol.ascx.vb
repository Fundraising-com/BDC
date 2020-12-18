Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Partial  Class editcustomercontrol
    Inherits CWebControl
    Protected m_bSuccess As Boolean
    Protected strErrorMessage As String
    Protected WithEvents Row1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row2 As System.Web.UI.HtmlControls.HtmlTableRow

    Protected objCustomer As Customer
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            Row1.Visible = False
            Row2.Visible = False
        End If
    End Sub

#Region "Properties"
    Public Property Success() As Boolean
        Get
            Return m_bSuccess
        End Get
        Set(ByVal Value As Boolean)
            m_bSuccess = Value
        End Set
    End Property
    Public Property Message() As String
        Get
            Return strErrorMessage
        End Get
        Set(ByVal Value As String)
            strErrorMessage = Value
        End Set
    End Property
#End Region

    Public Sub fillValues(ByVal cust As Customer)
        txtCAFirstName.Text = cust.FirstName
        txtCALastName.Text = cust.LastName
        txtCAEMail.Text = cust.Email
        chkSubscribe.Checked = cust.Subscribed

        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "AE") Then
            Dim item As ListItem
            Dim i As Integer = 0
            For Each item In txtCustomerGroups.Items
                If item.Value = cust.CustomerGroup Then
                    txtCustomerGroups.SelectedIndex = i
                End If
                i = i + 1
            Next
        End If
        Me.txtCAPassword.Text = cust.PassWord
        Me.txtCAConfirmPassword.Text = cust.PassWord
        hdnID.Value = cust.ID
    End Sub

    Public Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Success = False
        ValidateInput()
        If strErrorMessage = "" Then
            objCustomer = New Customer()
            If (Row1.Visible) Then
                objCustomer.CustomerGroup = txtCustomerGroups.SelectedItem.Value
            Else
                objCustomer.CustomerGroup = 0
            End If
            objCustomer.FirstName = Me.txtCAFirstName.Text
            objCustomer.LastName = txtCALastName.Text
            objCustomer.Email = txtCAEMail.Text
            If txtCAPassword.Text.Trim <> "" Then
                objCustomer.PassWord = txtCAPassword.Text
            End If

            objCustomer.Subscribed = chkSubscribe.Checked
            objCustomer.ID = hdnID.Value
            Try
                m_objcustomer.UpdateCustomer(objCustomer)
                strErrorMessage = Me.m_objMessages.GetXMLMessage("editcustomer.aspx", "EditCustomer", "Success")
                Success = True

            Catch err As Exception
                strErrorMessage = err.Message

            End Try
            RaiseEvent Edit(strErrorMessage, EventArgs.Empty)
        End If
    End Sub

    Private Sub ValidateInput()
        If txtCAFirstName.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankFirstName")
        ElseIf txtCALastName.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankLastName")
        ElseIf txtCAEMail.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankEMailAddress")
        ElseIf Not (txtCAPassword.Text = txtCAConfirmPassword.Text) Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "PasswordsNotMatch")
        Else
            strErrorMessage = ""
        End If

    End Sub

    Public Sub ClearFields()
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "AE") Then
            txtCustomerGroups.SelectedIndex = 0
            If txtCustomerGroups.Items.Count < 1 Then
                txtCustomerGroups.Visible = False
            End If
        End If
        txtCAFirstName.Text = ""
        txtCALastName.Text = ""
        txtCAEMail.Text = ""
        txtCAPassword.Text = ""
        chkSubscribe.Checked = False
    End Sub

End Class
