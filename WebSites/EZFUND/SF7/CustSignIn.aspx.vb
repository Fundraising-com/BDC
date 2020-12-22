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

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class CustSignIn
    Inherits CWebPage

    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl

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

        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If (Request.QueryString("SignOut") <> "") Then
            If Request.QueryString("SignOut") = 2 Then 'anonymous sign out
                m_objCustomer.UpdateSignIn(False)
                Session("customer") = Nothing
                Dim webid As String = m_objcustomer.GetSessionID
                m_objCustomer = Nothing
                m_objCustomer = New CCustomer(webid, StoreFrontConfiguration.XMLDocument)
                'm_objcustomer.IsSignedIn = True
                Session("customer") = m_objcustomer
                Session("OrderHistory") = Nothing
            ElseIf Request.QueryString("SignOut") = 1 Then 'regular sign out 
                m_objCustomer.UpdateSignIn(False)
                Session("Customer") = Nothing
                Session("XMLShoppingCart") = Nothing
                'Tee 11/15/2007 removed unnecessary session variable
                Session.Remove("ItemAdded")
                'end Tee
                m_objXMLCart = Nothing
                m_objCustomer = Nothing
                Session("OrderHistory") = Nothing
                Session("anonymous") = Nothing
                Response.Redirect("default.aspx")
            End If
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

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
                'Tee 11/15/2007 removed unnecessary session variable
                Session.Remove("ItemAdded")
                'end Tee
                m_objXMLCart = Nothing
                Session("OrderHistory") = Nothing

            Else
                Message.Visible = False
            End If
            imgSignIn.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("SignIn").Attributes("Filepath").Value
            imgCreate.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("CreateAccount").Attributes("Filepath").Value
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
                'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                Session("Anonymous") = Nothing
                'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

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
            'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            ' if regular customer is creating new regular acct then no extra logic needed
            ' if anonymous customer creating new acct....
            ' Check if anonymous tied up to temp acct - blank password - then update customer info to this acct
            ' if it returns multiple email address for this acct validate email and pwd before creating new acct.
            ' if anonymous tied up to regular acct - password not blank - then validate email address and create new acct.

            Dim tempAcct As Boolean = False
            If Not IsNothing(Session("anonymous")) Then ' flag for anonymous login
                m_objcustomer.IsAnonymous = True
                tempAcct = True
            End If
            'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
            If (m_objCustomer.AddCustomer(txtCAFirstName.Text, txtCALastName.Text, txtCAEMail.Text, txtCAPassword.Text, chkSubscribe.Checked, tempAcct)) Then
                'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                'acct creation successful; remove session("anonymous") and set anonymous flag to false
                'this customer can be treated as regular customer now
                Session("anonymous") = Nothing
                m_objCustomer.IsAnonymous = False
                'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

                If (ReturnPage.Text = "" Or ReturnPage.Text = "default.aspx") Then
                    Response.Redirect("CustProfileMain.aspx")
                Else
                    Response.Redirect(ReturnPage.Text)
                End If
            Else
                'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                'if something goes wrong during acct creation anonymous customer still must be anonymous
                If Not IsNothing(Session("anonymous")) Then
                    m_objcustomer.IsAnonymous = True
                End If
                'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustSignIn.aspx", "CreateAccount", m_objCustomer.ErrorMessage)
                ErrorMessage.Visible = True
            End If
        End If
    End Sub
End Class
