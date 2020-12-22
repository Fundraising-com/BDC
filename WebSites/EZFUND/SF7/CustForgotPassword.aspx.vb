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
Imports StoreFront.StoreFront.Email

Partial Class CustForgotPassword
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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
            btnSubmit.Attributes.Add("onclick", "return SetValidation();")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("CustForgotPassword.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)

            If (Request.QueryString("ReturnPage") <> "") Then
                ReturnPage.Text = Request.QueryString("ReturnPage")
            ElseIf (Request.Form("ReturnPage") <> "") Then
                ReturnPage.Text = Request.Form("ReturnPage")
            End If

            imgSubmit.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class CustForgotPassword Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If (txtEMail.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("CustForgotPassword.aspx", "EMailPassword", "BlankEMailAddress")
            ErrorMessage.Visible = True
        Else
            If (m_objCustomer.ValidateCustomerEmail(txtEMail.Text)) Then
                'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                If m_objcustomer.Password <> "" Then
                    'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
                    Dim objForgotEmail As New CCustForgotPasswordEMail

                    Try
                        objForgotEmail.SendForgotPassword(m_aremailcontent, m_objcustomer, txtEMail)
                    Catch objError As Exception
                        ErrorMessage.Text = objError.Message
                        ErrorMessage.Visible = True
                    End Try

                    Dim strRecipientFirstName As String = m_objCustomer.GetCustomerFirstName(txtEMail.Text)
                    Dim strRecipientLastName As String = m_objCustomer.GetCustomerLastName(txtEMail.Text)
                    Dim txtConfirmMessage As String = m_objMessages.GetXMLMessage("CustForgotPassword.aspx", "PasswordSent", "Sent")

                    txtConfirmMessage = txtConfirmMessage.Replace("[RecipientEmailAddress]", txtEMail.Text)
                    txtConfirmMessage = txtConfirmMessage.Replace("[RecipientFirstName]", strRecipientFirstName)
                    txtConfirmMessage = txtConfirmMessage.Replace("[RecipientLastName]", strRecipientLastName)
                    Session("EMailPasswordMessage") = txtConfirmMessage
                    If (ReturnPage.Text = "" Or ReturnPage.Text = "default.aspx") Then
                        Response.Redirect("CustSignIn.aspx")
                    Else
                        Response.Redirect(ReturnPage.Text)
                    End If
                    'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
                Else
                    ErrorMessage.Text = m_objMessages.GetXMLMessage("CustForgotPassword.aspx", "EMailPassword", "EMailNotFound")
                    ErrorMessage.Visible = True
                End If
                'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustForgotPassword.aspx", "EMailPassword", "EMailNotFound")
                ErrorMessage.Visible = True
            End If
        End If
    End Sub
End Class
