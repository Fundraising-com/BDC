Imports StoreFront.UITools
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports CSR.CSRBusinessRule
Imports CSR.CSRSystemBase


Partial  Class addemployee
    Inherits CWebControl
    Protected objUser As CSRUser
    Protected m_bAdded As Boolean
    Protected strErrorMessage As String
    Dim um As New CSRUserManagement
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton
    Public Mode As String
    Public uid As String


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

        'begin: GJV - 7/31/2007 - CSR
        Dim CSRMgmt As New CSRManagement(StoreFrontConfiguration.SiteURL)
        Dim bIsAdvancedCSR As Boolean = CSRMgmt.IsAdvancedCSR

        'note: users will only be able to modify the checkstate of the following checkboxes
        '      if theyve purchased the advanced CSR license
        chkOverridePricing.Enabled = bIsAdvancedCSR
        chkOverrideShippingCharges.Enabled = bIsAdvancedCSR
        chkOverrideTaxes.Enabled = bIsAdvancedCSR
        'end: GJV - 7/31/2007 - CSR

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
#Region "Sub BindMenuOptions(ByVal uid As Long)"
    Public Sub BindMenuOptions(ByVal uid As Long)
        'OffSomewhere on display or Saving.
        Dim user As CSRUser = um.GetUser(uid)
        lblEmployeeHeader.Text = "Edit Customer Service Representative"
        With user
            txtPassword.TextMode = TextBoxMode.SingleLine
            txtConfirmPass.TextMode = TextBoxMode.SingleLine
            txtUserName.Text = .UserName
            txtPassword.Text = "" & .Password
            txtConfirmPass.Text = "" & .Password
            'begin: GJV - 7/31/2007 - CSR
            Dim CSRMgmt As New CSRManagement(StoreFrontConfiguration.SiteURL)
            Dim bIsAdvancedCSR As Boolean = CSRMgmt.IsAdvancedCSR

            chkOverridePricing.Checked = bIsAdvancedCSR And .OverridePricing
            chkOverrideShippingCharges.Checked = bIsAdvancedCSR And .OverrideShippingCharges
            chkOverrideTaxes.Checked = bIsAdvancedCSR And .OverrideTaxes
            'end: GJV - 7/31/2007 - CSR
            'Begin Custom Code
            Textbox_hide.Text = .UserName
            Textbox_hide1.Text = uid
            'End Custom Code

        End With

    End Sub
#End Region
#Region "Sub ClearFields()"
    Public Sub ClearFields()

        Me.txtUserName.Text = ""
        Me.txtPassword.Text = ""
        Me.txtConfirmPass.Text = ""
        'begin: GJV - 7/31/2007 - CSR
        chkOverridePricing.Checked = False
        chkOverrideShippingCharges.Checked = False
        chkOverrideTaxes.Checked = False
        'end: GJV - 7/31/2007 - CSR
        
    End Sub
#End Region
#Region "Sub ValidateInput()"
    Private Sub ValidateInput()
        If txtUserName.Text = "" Then
            strErrorMessage = "Please Enter The Username"
        ElseIf txtPassword.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankPassword")
        ElseIf txtConfirmPass.Text = "" Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankPassword")
        ElseIf Not (txtPassword.Text = txtConfirmPass.Text) Then
            strErrorMessage = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "PasswordsNotMatch")
        Else
            strErrorMessage = ""
        End If
    End Sub
#End Region

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        strErrorMessage = ""
        ValidateInput()
        Dim CSRMan As New CSRManagement(StoreFrontConfiguration.SiteURL)
        If CSRMan.IsAdvancedCSR = False Then

            If um.GetEmployeeCount >= 2 Then
                strErrorMessage = "You may only set up 2 customer service representatives."
            End If
        End If
        If strErrorMessage = "" Then
            objUser = New CSRUser
            Mode = Session(Mode)
            If (Mode = "Edit") Then
                objUser.UID = Textbox_hide1.Text
            End If

            objUser.UserName = txtUserName.Text
            objUser.Password = txtPassword.Text

            'begin: GJV - 7/31/2007 - CSR
            Dim bIsAdvancedCSR As Boolean = CSRMan.IsAdvancedCSR

            objUser.OverridePricing = bIsAdvancedCSR And chkOverridePricing.Checked
            objUser.OverrideShippingCharges = bIsAdvancedCSR And chkOverrideShippingCharges.Checked
            objUser.OverrideTaxes = bIsAdvancedCSR And chkOverrideTaxes.Checked
            'end: GJV - 7/31/2007 - CSR

            If (Mode = "Edit") Then
                If (Textbox_hide.Text = txtUserName.Text) Then
                    um.SaveUser(objUser)
                Else
                    If um.ValidateCustomer(objUser.UserName, objUser.Password) = True Then
                        strErrorMessage = "User Name and Password Already Exists"
                    Else
                        um.SaveUser(objUser)
                    End If
                End If
            Else
                If um.ValidateCustomer(objUser.UserName, objUser.Password) = True Then
                    strErrorMessage = "User Name and Password Already Exists"
                Else
                    um.SaveUser(objUser)
                End If
            End If
            If strErrorMessage = "" Then
                IsAdded = True
                If (Mode = "Edit") Then
                    strErrorMessage = "Changes Have been Saved"
                Else
                    strErrorMessage = "The Customer Service Representative has been Added"
                End If
            Else
                IsAdded = False
            End If
        Else
            IsAdded = False
        End If
        RaiseEvent Add(IsAdded, EventArgs.Empty)
    End Sub

End Class
