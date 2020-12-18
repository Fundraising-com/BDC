Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports System
Imports StoreFrontSecurity

Partial Class AddEditAdmin
    Inherits CWebControl
    Event SaveClick As EventHandler
    Event CancelClick As EventHandler
    Event errorMsg(ByVal str As String)
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
    Private m_uid As Long
    Private _title As String
    Public Property Title() As string
        Get
            Return _title
        End Get
        Set(ByVal Value As string)
            _title = Value
        End Set
    End Property
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsNothing(hdnAdminId) Then
            m_uid = CLng(hdnAdminId.Value)
        Else
            m_uid = 0
        End If
        'lblTitle.Text = _title
        cmdSave.Attributes.Add("onclick", "return SetValidation();")
        If Not IsPostBack Then
            BindDDl()
        End If
        
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim objUser As New AdminUserBase
        Dim err As String = ""
        err = CheckData() 'one more time required field check
        If err.Length > 0 Then
            CType(Parent.FindControl("lblErrorMessage"), Label).Text = err
            CType(Parent.FindControl("lblErrorMessage"), Label).Visible = True
            Exit Sub
        End If
        lblTitle.Text = hdnTitle.Value
        objUser.Uid = hdnAdminId.Value
        objUser.FirstName = txtFName.Text
        objUser.LastName = txtLName.Text
        objUser.UserName = txtUsername.Text
        'ELW MOD'
        objUser.IsLocked = chkLocked.Checked
        If objUser.IsLocked Then
            objUser.NumFailedAttempt = 5
        Else
            objUser.NumFailedAttempt = 0
        End If
        'if passowrd is already encrypted no need to encrypt again
        If txtPassword.Text.Length > 10 Then
            objUser.Password = txtPassword.Text 'already encrypted
        Else
            objUser.Password = Encrypt(txtPassword.Text)
        End If

        objUser.RoleId = ddlRoles.SelectedValue
        Dim objAdmin As New CAdminUser
        m_uid = objAdmin.UpdateAdmin(objUser)
        If m_uid = 0 Then
            err = "Admin with same username already exists."
        ElseIf m_uid < 0 Then
            err = "Database error: Admin could not be saved successfully."
        Else
            hdnAdminId.Value = m_uid
            err = "Admin saved successfully."
        End If
        hdnTitle.Value = lblTitle.Text
        RaiseEvent SaveClick(err, e)
        BindData()
    End Sub
    Function CheckData() As String
        Dim err As String = ""
        If txtFName.Text.Trim = "" Then
            err &= "Please enter First Name."
        End If
        If txtLName.Text.Trim = "" Then
            err &= "Please enter Last Name."
        End If
        If txtUsername.Text.Trim = "" Then
            err &= "Please enter User Name."
        End If
        If txtPassword.Text.Trim = "" Then
            txtPassword.Attributes.Add("value", "")
            err &= "Please enter Password."
        End If
        Return err
    End Function
    Sub BindData()
        Dim objAdmin As New CAdminUser
        Dim objUser As AdminUserBase = Nothing
        'lblTitle.Text = _title
        lblTitle.Text = hdnTitle.Value
        Try
            objUser = objAdmin.GetAdminInfo(CLng(hdnAdminId.Value))
        Catch ex As Exception
            RaiseEvent errorMsg(ex.Message)
        End Try
        If Not IsNothing(objUser) Then
            txtFName.Text = objUser.FirstName
            txtLName.Text = objUser.LastName
            txtUsername.Text = objUser.UserName
            If objUser.IsSuperUser Then
                txtPassword.ReadOnly = True
            Else
                txtPassword.ReadOnly = False
            End If
            txtPassword.Attributes.Add("value", Me.GetDecryptedValue(objUser.Password))
            chkLocked.Checked = objUser.IsLocked 'ELW MOD'
            chkLocked.Enabled = CType(Session("Admin"), AdminUserBase).IsBuiltInAdmin 'ELW MOD'
            ddlRoles.SelectedIndex = -1
            Try
                ddlRoles.Items.FindByValue(objUser.RoleId).Selected = True
            Catch ex As Exception
            End Try
            hdnAdminId.Value = objUser.Uid
        Else
            ClearForm()
        End If
    End Sub
    Sub ClearForm()
        txtFName.Text = ""
        txtLName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        ddlRoles.SelectedIndex = 0
        hdnAdminId.Value = 0
        chkLocked.Checked = False 'ELW MOD'
    End Sub
    Sub BindDDl()
        Dim objRole As New CAdminUser
        Dim ar As New ArrayList
        ar = objRole.GetAllRoles()

        Me.ddlRoles.Items.Clear()
        ddlRoles.Items.Add(New ListItem("Select Role", ""))
        For Each oRole As UserRoleBase In ar
            ddlRoles.Items.Add(New ListItem(oRole.RoleName, oRole.UID))
        Next
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        BindData()
        RaiseEvent CancelClick(sender, e)
    End Sub
    Private Function Encrypt(ByVal pass As String) As String
        Dim mCrypto As New StoreFrontSecurity.StoreFrontRSACrypto
        Dim mEncryptedpass As String = String.Empty
        Dim objCrypt As New CStoreFrontCrypto2(pass)
        objCrypt.Type = CryptoType.Encrypt
        mEncryptedpass = objCrypt.GetData
        Return mEncryptedpass
    End Function
    Private Function GetDecryptedValue(ByVal Value As String) As String
        Try
            Dim objKey(7) As Byte
            Dim objIV(7) As Byte
            Dim sReturn As String = String.Empty
            If (Not Value Is Nothing) AndAlso Value.Trim.Length > 0 Then
                sReturn = Value
                decryptIt(objKey, objIV, sReturn)
                Return sReturn
            End If
            Return String.Empty
        Catch ex As Exception
            RaiseEvent errorMsg("Error decrypting password " & Value)
            Return Value
        End Try
    End Function
#Region "Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(),byRef strValue as string)"
    Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(), ByRef strValue As String)
        Dim st() As String
        Dim i As Long, j As Long, x As Long, k As Long, y As Long
        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(Nothing, i)
        Next
        For i = 0 To objIV.GetUpperBound(0)
            objIV.SetValue(Nothing, i)
        Next

        st = Split(strValue)

        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(CByte(CInt(st.GetValue(i))), i)
            j = i
        Next
        j = j + 1
        For i = j To j + objIV.GetUpperBound(0)
            objIV.SetValue(CByte(CInt(st.GetValue(i))), x)
            k = i
            x = x + 1
        Next
        k = k + 1
        Dim arEncrypt(st.GetUpperBound(0) - k) As Byte
        For i = k To st.GetUpperBound(0)
            arEncrypt.SetValue(CByte(CInt(st.GetValue(i))), y)
            y = y + 1
        Next

        Dim obj As New StoreFrontSecurity.CStoreFrontCrypto2(arEncrypt)
        obj.Type = StoreFrontSecurity.CryptoType.Decrypt
        strValue = obj.GetData(objKey, objIV)
        obj = Nothing
    End Sub
#End Region
End Class
