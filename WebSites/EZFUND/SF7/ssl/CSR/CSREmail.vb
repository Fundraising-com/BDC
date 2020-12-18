'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Web.Mail
Imports StoreFront.SystemBase

Imports System
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.Management

Namespace CSR

#Region "Class CSREmail"
    Public Class CSREmail
        Inherits Email.CEmail

        Public Sub SendPassword(ByRef m_objCustomer As CCustomer)
            Dim strPassword As String = m_objCustomer.CustPassWord
            Dim strRecipientFirstName As String = m_objCustomer.GetCustomerFirstName(m_objCustomer.Email)
            Dim strRecipientLastName As String = m_objCustomer.GetCustomerLastName(m_objCustomer.Email)
            Dim objAdmin As New Admin.CEmail(StoreFrontConfiguration.AdminEmail)
            Dim objEmail As New CEMailContent
            'Dim objContent As CXMLEMailContent

            Dim txtBody As String = ""
            Dim txtSubject As String = "New Account"
            txtBody = "Dear " & strRecipientFirstName & " " & strRecipientLastName & ","
            txtBody = txtBody & vbCrLf
            txtBody = txtBody & "A new account has been created for you at " & StoreFrontConfiguration.AdminStore.Item("Name").InnerText
            txtBody = txtBody & vbCrLf
            txtBody = txtBody & "Your password is: " & strPassword
            
            If (m_objCustomer.Email <> "" And objAdmin.EmailPrimary <> "") Then
                MyBase.To = m_objCustomer.Email
                MyBase.From = objAdmin.EmailPrimary
                MyBase.MailMethod = objAdmin.EmailMethod
                MyBase.MailServer = objAdmin.EmailServer
                MyBase.Subject = txtSubject
                MyBase.Body = txtBody
                Try
                    MyBase.SendEMail()
                Catch objErr As Exception
                    Throw New Exception(objErr.Message)
                End Try
            End If

        End Sub
    End Class
#End Region

End Namespace


