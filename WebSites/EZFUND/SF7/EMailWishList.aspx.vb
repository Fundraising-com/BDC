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

Partial Class EMailWishList
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl


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
            btnSend.Attributes.Add("onclick", "return SetValidation();")
            SetPageTitle = m_objMessages.GetXMLMessage("EmailWishList.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            imgSend.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Send").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class EMailWishList Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If (txtSenderName.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoSenderName")
            ErrorMessage.Visible = True
        ElseIf (txtSenderEmail.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoSenderEmail")
            ErrorMessage.Visible = True
        ElseIf (txtRecipientName.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoRecipientName")
            ErrorMessage.Visible = True
        ElseIf (txtRecipientEmail.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoRecipientEmail")
            ErrorMessage.Visible = True
        Else

            Dim objSendEMailWishList As New CEMailWishList()
            Try
                objSendEMailWishList.SendEMailWishList(m_objCustomer, txtRecipientEmail, txtRecipientName, txtSenderEmail, txtSenderName, txtMessage, m_arEMailContent)
            Catch objError As Exception
                ErrorMessage.Text = objError.Message
                ErrorMessage.Visible = True
            End Try

            Dim txtConfirmMessage As String = m_objMessages.GetXMLMessage("EMailWishList.aspx", "WishlistSent", "Sent")
            txtConfirmMessage = txtConfirmMessage.Replace("[RecipientEmailAddress]", txtRecipientEmail.Text)
            txtConfirmMessage = txtConfirmMessage.Replace("[RecipientName]", txtRecipientName.Text)
            Session("EMailWishListMessage") = txtConfirmMessage
            Response.Redirect("SavedCart.aspx")
        End If
    End Sub
    'Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (txtSenderName.Text = "") Then
    '        ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoSenderName")
    '        ErrorMessage.Visible = True
    '    ElseIf (txtSenderEmail.Text = "") Then
    '        ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoSenderEmail")
    '        ErrorMessage.Visible = True
    '    ElseIf (txtRecipientName.Text = "") Then
    '        ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoRecipientName")
    '        ErrorMessage.Visible = True
    '    ElseIf (txtRecipientEmail.Text = "") Then
    '        ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailWishList.aspx", "ErrorMessage", "NoRecipientEmail")
    '        ErrorMessage.Visible = True
    '    Else

    '        Dim objSendEMailWishList As New CEMailWishList()
    '        Try
    '            objSendEMailWishList.SendEMailWishList(m_objCustomer, txtRecipientEmail, txtRecipientName, txtSenderEmail, txtSenderName, txtMessage, m_arEMailContent)
    '        Catch objError As Exception
    '            ErrorMessage.Text = objError.Message
    '            ErrorMessage.Visible = True
    '        End Try

    '        Dim txtConfirmMessage As String = m_objMessages.GetXMLMessage("EMailWishList.aspx", "WishlistSent", "Sent")
    '        txtConfirmMessage = txtConfirmMessage.Replace("[RecipientEmailAddress]", txtRecipientEmail.Text)
    '        txtConfirmMessage = txtConfirmMessage.Replace("[RecipientName]", txtRecipientName.Text)
    '        Session("EMailWishListMessage") = txtConfirmMessage
    '        Response.Redirect("SavedCart.aspx")
    '    End If
    'End Sub

End Class
