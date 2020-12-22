Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Partial  Class Affiliatesignin
    Inherits CWebControl
#Region " Members"

#End Region

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        imgCreate.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CreateAccount").Attributes("Filepath").Value
        imgSignIn.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignIn").Attributes("Filepath").Value
        ReturnPage.Text = Request.QueryString("ReturnPage")
    End Sub
#End Region

#Region "Private Sub btnSignIn_Click(ByVal sender As System.Object, ByVal e As system.eventargs) Handles btnSignIn.Click"

    Private Sub btnSignIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignIn.Click

        m_Affiliate = Session("Affiliate")
        If IsNothing(m_Affiliate) Then
            m_Affiliate = New CAffiliate()
        End If
        If (txtSIEMail.Text = "" Or txtSIPassword.Text = "") Then
            If (txtSIEMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "SigningIn", "BlankEMailAddress")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "SigningIn", "BlankPassword")
            End If
            ErrorMessage.Visible = True
        Else
            If (m_Affiliate.SignIN(txtSIEMail.Text, txtSIPassword.Text)) Then
                Session("Affiliate") = m_Affiliate
                If (ReturnPage.Text = "" Or ReturnPage.Text = "default.aspx") Then
                    Response.Redirect("affiliateaccount.aspx")
                Else
                    Response.Redirect(ReturnPage.Text)
                End If
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "SigningIn", "IncorrectSignIn")
                ErrorMessage.Visible = True
            End If
        End If
        imgSignIn.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignIn").Attributes("Filepath").Value
        imgCreate.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CreateAccount").Attributes("Filepath").Value
    End Sub
#End Region

#Region "Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles btnCreate.Click"

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'If (txtCAFirstName.Text = "" Or txtCALastName.Text = "" Or _
        If txtCAEMail.Text = "" Or txtCAPassword.Text = "" Or _
         txtCAConfirmPassword.Text = "" Then

            If (txtCAEMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankEMailAddress")
            ElseIf (txtCAPassword.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankPassword")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankConfirmPassword")
            End If
            ErrorMessage.Visible = True
        ElseIf (txtCAPassword.Text <> txtCAConfirmPassword.Text) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "PasswordConfirmEqual")
            ErrorMessage.Visible = True
        Else

            m_Affiliate = New CAffiliate()
            If (m_Affiliate.AddAffiliate(txtCAEMail.Text, txtCAPassword.Text)) Then
                Session("Affiliate") = m_Affiliate

                Response.Redirect(StoreFrontConfiguration.SiteURL & "affiliateregister.aspx")

            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", m_Affiliate.ErrorMessage)
                If ErrorMessage.Text.Trim = "" Then
                    ErrorMessage.Text = m_Affiliate.ErrorMessage
                End If
                ErrorMessage.Visible = True
            End If
            Session("Affiliate") = m_Affiliate
        End If
    End Sub

#End Region

End Class

