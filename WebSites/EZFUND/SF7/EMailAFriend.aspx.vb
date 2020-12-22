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
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports StoreFront.StoreFront.Email

Partial Class EMailAFriend
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
            btnSend.Attributes.Add("onclick", "return SetValidation();")
            SetPageTitle = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            txtProdID.Text = Request.QueryString("ID")
            Try
                Dim index As Int16 = Request.UrlReferrer.AbsoluteUri.IndexOf("EMailAFriend.aspx")
                If (index < 0) Then '(Not Request.UrlReferrer.AbsoluteUri.EndsWith("/EMailAFriend.aspx?ID=") & txtProdID.Text) Then '(StoreFrontConfiguration.AdminStore.Item("SiteURL").InnerText.ToString & "/EMailAFriend.aspx")) Then
                    txtReferrer.Text = Request.UrlReferrer.AbsoluteUri
                End If
            Catch
                txtReferrer.Text = "default.aspx"
            End Try
            imgSend.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Send").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class EMailAFriend Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If (txtSendTo.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "ErrorMessage", "NoRecipientName")
            ErrorMessage.Visible = True
        ElseIf (txtSendToEmail.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "ErrorMessage", "NoRecipientEmail")
            ErrorMessage.Visible = True
        ElseIf (txtSenderName.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "ErrorMessage", "NoSenderName")
            ErrorMessage.Visible = True
        ElseIf (txtSenderEmail.Text = "") Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "ErrorMessage", "NoSenderEmail")
            ErrorMessage.Visible = True
        Else
            Dim objProduct As CProduct
            Dim objSendEmail As New CEmailAFriend()
            '            Dim objAccess As New CXMLProductAccess(dom)
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                objProduct = New CProduct(m_objXMLAccess.GetProduct(txtProdID.Text))
            Else
                Dim oprodManagement As New CProductManagement()
                Dim drProd As DataRow = oprodManagement.GetProductRow(CLng(txtProdID.Text), 0).Products.Rows(0)
                objProduct = New CProduct(drProd, 1)
                oprodManagement = Nothing
            End If


            Try
                objSendEmail.SendEmailAFriend(objProduct, txtSendTo, txtSendToEmail, txtSenderName, txtSenderEmail, txtMessage, m_arEMailContent, chkEmailCopyToSender)
            Catch objError As Exception
                ErrorMessage.Text = objError.Message
                ErrorMessage.Visible = True
            End Try

            Dim strSentMessage As String = m_objMessages.GetXMLMessage("EMailAFriend.aspx", "EmailAFriend", "Sent")
            strSentMessage = strSentMessage.Replace("[RecipientEmailAddress]", txtSendToEmail.Text)
            strSentMessage = strSentMessage.Replace("[ProductName]", objProduct.Name)
            Session("EmailedAFriend") = strSentMessage
            Response.Redirect(txtReferrer.Text.ToString)
        End If
    End Sub

End Class
