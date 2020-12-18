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

Public Class CustUnsubscribe
    Inherits CWebPage
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents P1 As System.Web.UI.HtmlControls.HtmlGenericControl

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
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("CustUnsubscribe.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
            If (IsNothing(Session("ChangeEmailAction")) = False) Then
                Dim txtChangeEmailAction As String = Session("ChangeEmailAction")
                If (txtChangeEmailAction = "remove") Then
                    Dim oCust As New Customer()
                    oCust.ID = m_objCustomer.GetCustomerID
                    oCust.Email = m_objCustomer.Email
                    oCust.FirstName = m_objCustomer.CustFirstName
                    oCust.LastName = m_objCustomer.CustLastName
                    oCust.CustomerGroup = m_objCustomer.CustomerGroup
                    oCust.PassWord = m_objCustomer.CustPassWord
                    oCust.Subscribed = False
                    Try
                        m_objCustomer.UpdateCustomer(oCust)
                        Dim strMessage As String = m_objMessages.GetXMLMessage("CustUnsubscribe.aspx", "ChangeEmailAction", "Remove")
                        strMessage = strMessage.Replace("[EmailAddress]", m_objCustomer.Email)
                        strMessage = strMessage.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                        Message.Text = strMessage
                        Message.Visible = True
                    Catch objErr As Exception
                        ErrorMessage.Visible = True
                        ErrorMessage.Text = objErr.Message
                    End Try
                ElseIf (txtChangeEmailAction = "noaction") Then
                    Dim strMessage As String = m_objMessages.GetXMLMessage("CustUnsubscribe.aspx", "ChangeEmailAction", "NoAction")
                    strMessage = strMessage.Replace("[EmailAddress]", Session("ChangeEmailActionAddress"))
                    strMessage = strMessage.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                    Message.Text = strMessage
                    Message.Visible = True
                ElseIf (txtChangeEmailAction = "changeemail") Then
                    Dim oCust As New Customer()
                    oCust.Email = Session("ChangeEmailActionAddress")
                    oCust.ID = m_objCustomer.GetCustomerID
                    oCust.Email = Session("ChangeEmailActionAddress")
                    oCust.FirstName = m_objCustomer.CustFirstName
                    oCust.LastName = m_objCustomer.CustLastName
                    oCust.CustomerGroup = m_objCustomer.CustomerGroup
                    oCust.PassWord = m_objCustomer.CustPassWord
                    oCust.Subscribed = True
                    Try
                        m_objCustomer.UpdateCustomer(oCust)
                        Dim strMessage As String = m_objMessages.GetXMLMessage("CustUnsubscribe.aspx", "ChangeEmailAction", "ChangeEmail")
                        strMessage = strMessage.Replace("[EmailAddress]", Session("ChangeEmailActionAddress"))
                        strMessage = strMessage.Replace("[StoreName]", StoreFrontConfiguration.AdminStore.Item("Name").InnerText)
                        Message.Text = strMessage
                        Message.Visible = True
                    Catch objErr As Exception
                        ErrorMessage.Visible = True
                        ErrorMessage.Text = objErr.Message
                    End Try
                    Session("ChangeEmailActionAddress") = Nothing
                End If
                Session("ChangeEmailAction") = Nothing
            Else
                Message.Visible = False
            End If
        Catch ex As exception
            Session("DetailError") = "Class CustUnsubscribe Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
