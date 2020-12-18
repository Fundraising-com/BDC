'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class CustSignInCheckout
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents anonymouslogin1 As AnonymousLogin
    Private Const JS_SetValidationSignIn As String = "SetValidationSignIn()"
    Private Const JS_SetValidationNew As String = "SetValidationNew()"

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
            SetPageTitle = m_objMessages.GetXMLMessage("CustSignInCheckout.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)
            SetEnterKeyPostBack(txtSIEMail, btnSignIn, , JS_SetValidationSignIn)
            SetEnterKeyPostBack(txtSIPassword, btnSignIn, , JS_SetValidationSignIn)
            SetEnterKeyPostBack(txtCAFirstName, btnContinue, , JS_SetValidationNew)
            SetEnterKeyPostBack(txtCALastName, btnContinue, , JS_SetValidationNew)
            SetEnterKeyPostBack(txtCAEMail, btnContinue, , JS_SetValidationNew)
            SetEnterKeyPostBack(txtCAPassword, btnContinue, , JS_SetValidationNew)
            SetEnterKeyPostBack(txtCAConfirmPassword, btnContinue, , JS_SetValidationNew)
            btnSignIn.Attributes.Add("onclick", "return SetValidationSignIn();")
            btnContinue.Attributes.Add("onclick", "return SetValidationNew();")
            
            If (Request.QueryString("ReturnPage") <> "") Then
                ReturnPage.Text = Request.QueryString("ReturnPage")
                'sp7
                'If (IsNothing(Request.QueryString("From")) = False) Then
                '    ReturnPage.Text = ReturnPage.Text & "?WebID=" & Session("WebID") & "&From=" & Request.QueryString("From")
                'End If
            ElseIf (Request.Form("ReturnPage") <> "") Then
                ReturnPage.Text = Request.Form("ReturnPage")
                'Tee 4/26/2007 display session expired message
            Else
                ErrorMessage.Text = "The secure session has expired due to navigating back with browser " _
                & "back button.<BR>There are no items currently in your cart.<BR>Please sign in again."
                ErrorMessage.Visible = True
                ReturnPage.Text = StoreFrontConfiguration.SiteURL
                'end Tee
            End If
            If (IsNothing(Session("EMailPasswordMessage")) = False) Then
                Message.Text = Session("EMailPasswordMessage")
                Message.Visible = True
                Session("EMailPasswordMessage") = Nothing
            Else
                Message.Visible = False
            End If
            'sp7
            If IsPostBack = False AndAlso IsNothing(Session("PayPalEmail")) = False Then
                txtCAEMail.Text = Session("PayPalEmail")
                pnlPayPalNew.Visible = True
                pnlPayPalOld.Visible = True
            Else
                pnlPayPalNew.Visible = False
                pnlPayPalOld.Visible = False
            End If

            imgContinue.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
            imgSignIn.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignIn").Attributes("Filepath").Value
           'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            
            If StoreFrontConfiguration.AllowAnonymous Then
                anonymouslogin1.Visible = True
            Else
                anonymouslogin1.Visible = False
            End If
            'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
        Catch ex As Exception
            Session("DetailError") = "Class CustSignInCheckout Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSignIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignIn.Click
        If (txtSIEMail.Text = "" Or txtSIPassword.Text = "") Then
            If (txtSIEMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignInCheckout.aspx", "SigningIn", "BlankEMailAddress")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignInCheckout.aspx", "SigningIn", "BlankPassword")
            End If
            ErrorMessage.Visible = True
        Else
            If (m_objCustomer.ValidateCustomer(txtSIEMail.Text, txtSIPassword.Text)) Then
                Session("Anonymous") = Nothing
                m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                'sp7
                If IsNothing(Session("PayPalEmail")) = False And chkPayPalOld.Checked Then
                    m_objCustomer.AddPayPalUser(Session("PayPalEmail"))
                    Session("PayPalEmail") = Nothing
                End If

                'sp7
                If ReturnPage.Text.ToLower = "handlepaypalexpress.aspx" Then
                    Response.Redirect("HandlePayPalExpress.aspx?WebID=" & m_objcustomer.GetSessionID & "&From=CS")
                ElseIf (ReturnPage.Text <> "") Then
                    Response.Redirect(ReturnPage.Text)
                Else
                    Response.Redirect("Shipping.aspx")
                End If
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignInCheckout.aspx", "SigningIn", "IncorrectSignIn")
                ErrorMessage.Visible = True
            End If
        End If
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
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
            'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            ' if regular customer is creating new regular acct then no extra logic needed
            ' if anonymous customer creating new acct....
            ' Check if anonymous tied up to temp acct - blank password - then update customer info to this acct
            ' if it returns multiple email address for this acct validate email and pwd before creating new acct.
            ' if anonymous tied up to regular acct - password not blank - then validate email address and create new acct.

            Dim tempAcct As Boolean = False
            If Not IsNothing(Session("anonymous")) Then ' flag for anonymous login
                'm_objcustomer.IsAnonymous = True
                tempAcct = True
            End If
            'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
            If (m_objCustomer.AddCustomer(txtCAFirstName.Text, txtCALastName.Text, txtCAEMail.Text, txtCAPassword.Text, chkSubscribe.Checked)) Then
                Session("anonymous") = Nothing
                m_objCustomer.IsAnonymous = False

                'sp7
                If IsNothing(Session("PayPalEmail")) = False And chkPayPalNew.Checked Then
                    m_objCustomer.AddPayPalUser(Session("PayPalEmail"))
                    Session("PayPalEmail") = Nothing
                End If
                'sp7
                If ReturnPage.Text.ToLower = "handlepaypalexpress.aspx" Then
                    Response.Redirect("HandlePayPalExpress.aspx?WebID=" & m_objcustomer.GetSessionID & "&From=CS")
                ElseIf (ReturnPage.Text <> "") Then
                    Response.Redirect(ReturnPage.Text)
                Else
                    Response.Redirect("Shipping.aspx")
                End If
            Else
                'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                'if something goes wrong during acct creation anonymous customer still must be anonymous
                'If Not IsNothing(Session("anonymous")) Then
                '    m_objcustomer.IsAnonymous = True
                'End If
                'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", m_objCustomer.ErrorMessage)
                ErrorMessage.Visible = True
            End If
        End If
    End Sub
End Class
