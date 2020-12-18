'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class CustSignIn
    Inherits CWebPage

    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents txtSIEMail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSIPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgSignIn As System.Web.UI.WebControls.Image
    Protected WithEvents imgCreate As System.Web.UI.WebControls.Image
    Protected WithEvents btnSignIn As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnCreate As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtCAFirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCALastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAEMail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCAConfirmPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkSubscribe As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ForgotLink As System.Web.UI.WebControls.HyperLink
    Protected WithEvents ReturnPage As System.Web.UI.WebControls.TextBox
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents P1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            SetPageTitle = m_objMessages.GetXMLMessage("CustSignIn.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)
        Catch ex As Exception
            Session("DetailError") = "Class CustSignIn Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        If (Request.QueryString("SignOut") <> "") Then
            m_objCustomer.UpdateSignIn(False)
            Session("Customer") = Nothing
            Session("XMLShoppingCart") = Nothing
            m_objXMLCart = Nothing
            m_objCustomer = Nothing
            Session("OrderHistory") = Nothing
            Response.Redirect("default.aspx")
        End If

        Try
            If (Request.QueryString("ReturnPage") <> "") Then
                ReturnPage.Text = Request.QueryString("ReturnPage")
            ElseIf (Request.Form("ReturnPage") <> "") Then
                ReturnPage.Text = Request.Form("ReturnPage")
            End If
            If (IsNothing(Session("EMailPasswordMessage")) = False) Then
                Message.Text = Session("EMailPasswordMessage")
                Message.Visible = True
                Session("EMailPasswordMessage") = Nothing
            ElseIf (IsNothing(Session("OrderLoginMessage")) = False) Then
                Message.Text = Session("OrderLoginMessage")
                Message.Visible = True
                Session("OrderLoginMessage") = Nothing
                m_objCustomer.UpdateSignIn(False)
                Session("XMLShoppingCart") = Nothing
                m_objXMLCart = Nothing
                Session("OrderHistory") = Nothing

            Else
                Message.Visible = False
            End If
            imgSignIn.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("SignIn").Attributes("Filename").Value
            imgCreate.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("CreateAccount").Attributes("Filename").Value
            btnSignIn.Attributes.Add("onclick", "return SetValidationSignIn();")
            btnCreate.Attributes.Add("onclick", "return SetValidationNew();")
        Catch ex As Exception
            Session("DetailError") = "Class CustSignIn Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
 End Sub

    Private Sub btnSignIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignIn.Click
        If (txtSIEMail.Text = "" Or txtSIPassword.Text = "") Then
            If (txtSIEMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "SigningIn", "BlankEMailAddress")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "SigningIn", "BlankPassword")
            End If
            ErrorMessage.Visible = True
        Else
            If (m_objCustomer.ValidateCustomer(txtSIEMail.Text, txtSIPassword.Text)) Then
                m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                If (ReturnPage.Text = "" Or ReturnPage.Text = "default.aspx") Then
                    Response.Redirect("CustProfileMain.aspx")
                Else
                    Response.Redirect(ReturnPage.Text)
                End If
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "SigningIn", "IncorrectSignIn")
                ErrorMessage.Visible = True
            End If
        End If
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        If (txtCAFirstName.Text = "" Or txtCALastName.Text = "" Or _
            txtCAEMail.Text = "" Or txtCAPassword.Text = "" Or _
            txtCAConfirmPassword.Text = "") Then
            If (txtCAFirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "BlankFirstName")
            ElseIf (txtCALastName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "BlankLastName")
            ElseIf (txtCAEMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "BlankEMailAddress")
            ElseIf (txtCAPassword.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "BlankPassword")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "BlankConfirmPassword")
            End If
            ErrorMessage.Visible = True
        ElseIf (txtCAPassword.Text <> txtCAConfirmPassword.Text) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", "PasswordConfirmEqual")
            ErrorMessage.Visible = True
        Else
            If (m_objCustomer.AddCustomer(txtCAFirstName.Text, txtCALastName.Text, txtCAEMail.Text, txtCAPassword.Text, chkSubscribe.Checked)) Then
                If (ReturnPage.Text = "" Or ReturnPage.Text = "default.aspx") Then
                    Response.Redirect("CustProfileMain.aspx")
                Else
                    Response.Redirect(ReturnPage.Text)
                End If
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", m_objCustomer.ErrorMessage)
                ErrorMessage.Visible = True
            End If
        End If
    End Sub
End Class
