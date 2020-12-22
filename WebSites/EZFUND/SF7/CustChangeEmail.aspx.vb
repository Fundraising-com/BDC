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

Partial Class CustChangeEmail
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
            SetPageTitle = m_objMessages.GetXMLMessage("CustChangeEmail.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            txtEmail.Text = Request.QueryString("Email")
            If (txtEmail.Text.Trim = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustChangeEmail.aspx", "ErrorMessage", "NoEMailAddress")
                ErrorMessage.Visible = True
            ElseIf (m_objCustomer.ValidateCustomerEmail(txtEmail.Text)) Then
                Dim strRecipientFirstName As String = m_objCustomer.GetCustomerFirstName(txtEmail.Text)
                Dim strRecipientLastName As String = m_objCustomer.GetCustomerLastName(txtEmail.Text)
                Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
                'Dim objContent As CXMLEMailContent
                lblCustName.Text = strRecipientFirstName
                lblStoreName.Text = StoreFrontConfiguration.AdminStore.Item("Name").InnerText
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustChangeEmail.aspx", "ErrorMessage", "NoSuchEMailAddressOnFile")
                ErrorMessage.Text = ErrorMessage.Text.Replace("[EmailAddress]", txtEmail.Text)
                ErrorMessage.Text = ErrorMessage.Text.Replace("[StoreName]", StoreFrontConfiguration.StoreName)
                ErrorMessage.Visible = True
            End If
            imgSubmit.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Apply").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class CustChangeEmail Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If (rdoRemove.Checked) Then
            Session("ChangeEmailAction") = "remove"
        ElseIf (rdoNoAction.Checked) Then
            Session("ChangeEmailAction") = "noaction"
        ElseIf (rdoChangeEmail.Checked) Then
            Session("ChangeEmailAction") = "changeemail"
            Session("ChangeEmailActionAddress") = txtUpdateEmail.Text.ToString()
        End If
        If (m_objCustomer.IsSignedIn) Then
            Response.Redirect("CustUnsubscribe.aspx")
        ElseIf (rdoNoAction.Checked) Then
            Response.Redirect("CustUnsubscribe.aspx")
        Else
            Response.Redirect("CustSignIn.aspx?ReturnPage=CustUnsubscribe.aspx")
        End If
    End Sub


End Class
