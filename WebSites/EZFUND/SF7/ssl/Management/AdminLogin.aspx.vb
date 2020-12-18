Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports System
Imports StoreFrontSecurity
Partial Class AdminLogin
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtGroupIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        SetEnterKeyPostBack(Me.txtUserName, Me.cmdSubmit, , "SetValidation()")
        SetEnterKeyPostBack(Me.txtPassword, Me.cmdSubmit, , "SetValidation()")
        cmdSubmit.Attributes.Add("onclick", "return SetValidation();")
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Dim objAdmin As New CAdminUser
        Dim password As String = txtPassword.Text
        Dim objAdminBase As AdminUserBase = objAdmin.CheckAdmin(txtUserName.Text)

        If Not IsNothing(objAdminBase) Then
            If objAdminBase.NumFailedAttempt >= 5 Then
                Session("Admin") = Nothing
                ErrorMessage.Visible = True
                ErrorMessage.Text = "This account has been locked.  You must contact your StoreFront administrator to unlock your account."
            Else

                If Not password.Equals(GetDecryptedValue(objAdminBase.Password)) Then 'validated false

                    objAdminBase.NumFailedAttempt += 1

                    If objAdminBase.NumFailedAttempt = 5 Then
                        objAdminBase.IsLocked = True
                        objAdmin.UpdateAdmin(objAdminBase)
                        Session("Admin") = Nothing
                        ErrorMessage.Visible = True
                        ErrorMessage.Text = "This account has been locked.  You must contact your StoreFront administrator to unlock your account."
                    Else
                        objAdmin.UpdateAdmin(objAdminBase)
                        Session("Admin") = Nothing
                        ErrorMessage.Visible = True
                        ErrorMessage.Text = "Invalid User Name/Password"
                    End If

                    objAdminBase = Nothing

                Else
                    Session("Admin") = objAdminBase
                    objAdminBase.NumFailedAttempt = 0
                    objAdmin.UpdateAdmin(objAdminBase)
                    Response.Redirect("default.aspx")
                End If
            End If
        Else
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Invalid User Name/Password"
            Session("Admin") = Nothing
        End If


    End Sub
    Private Function GetDecryptedValue(ByVal Value As String) As String
        Try
            'If Not StoreFrontConfiguration.ConvertedFrom3DES Then
            Dim objKey(7) As Byte
            Dim objIV(7) As Byte
            Dim sReturn As String = String.Empty
            If (Not Value Is Nothing) AndAlso Value.Trim.Length > 0 Then
                sReturn = Value
                decryptIt(objKey, objIV, sReturn)
                Return sReturn
            End If
            Return String.Empty
            'End If
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Error Decrypting value ." & ex.Message
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
    Private Function Encrypt(ByVal pass As String) As String
        Dim mCrypto As New StoreFrontSecurity.StoreFrontRSACrypto
        Dim mEncryptedpass As String = String.Empty
         Dim objCrypt As New CStoreFrontCrypto2(pass)
        objCrypt.Type = CryptoType.Encrypt
        mEncryptedpass = objCrypt.GetData
        Return mEncryptedpass
    End Function
#End Region
End Class
