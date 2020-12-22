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
Partial Class CustEdit
    Inherits CWebPage
    Protected WithEvents ReturnPage As System.Web.UI.WebControls.TextBox
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
            cmdUpdate.Attributes.Add("onclick", "return SetValidation();")
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("CustEdit.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)
        Catch ex As Exception
            Session("DetailError") = "Class CustEdit Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If m_objCustomer.IsSignedIn = False Then
            Response.Redirect("CustSignIn.aspx?ReturnPage=CustEdit.aspx")
            Exit Sub
        Else
            If Not IsNothing(Session("anonymous")) Then
                Response.Redirect("CustSignIn.aspx?signout=2")
                Exit Sub
            End If
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
        Try
            If Not IsPostBack Then
                'load values
                If (m_objCustomer.CustFirstName.ToLower() <> "new customer") Then
                    txtCAFirstName.Text = m_objCustomer.CustFirstName
                    txtCALastName.Text = m_objCustomer.CustLastName
                    txtCAEMail.Text = m_objCustomer.Email
                    chkSubscribe.Checked = m_objCustomer.CustISSubscribed
                End If
            End If
            imgSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Save").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class CustEdit Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        'check input
        If Validate_Input() = True Then
            Try
                If (Update_Profile() = False) Then
                    Exit Sub
                End If
                Message.Text = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "Success")
                RefreshLeftNav()
            Catch objErr As Exception
                Message.Text = objErr.Message
            End Try
        End If
    End Sub
    'Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'check input
    '    If Validate_Input() = True Then
    '        Try
    '            Update_Profile()
    '            Message.Text = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "Success")
    '            RefreshLeftNav()
    '        Catch objErr As Exception
    '            Message.Text = objErr.Message
    '        End Try
    '    End If
    'End Sub

#Region "Function Update_Profile"
    '-----------------------------------------------------------
    ' Function Update_Profile
    'returns Boolean
    ' Parameters 
    ' 
    ' Description:
    ' Passes the Customer Object to be used to update the DB 
    'returns True if Successful
    '-----------------------------------------------------------
    Private Function Update_Profile() As Boolean
        Dim oCust As New Customer()
        oCust.ID = m_objCustomer.GetCustomerID
        oCust.Email = txtCAEMail.Text
        oCust.FirstName = txtCAFirstName.Text
        oCust.LastName = txtCALastName.Text
        oCust.Subscribed = chkSubscribe.Checked
        ' #1454  SV
        oCust.CustomerGroup = m_objcustomer.CustomerGroup
        ' #1454  SV
        If txtOldPassWord.Text <> "" Then
            oCust.PassWord = txtCAPassword.Text
        Else
            oCust.PassWord = m_objCustomer.CustPassWord
        End If
        If (oCust.Email <> m_objcustomer.Email) Then
            If (m_objcustomer.DuplicateEmail(oCust.Email) = True) Then
                ErrorMessage.Visible = True
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "DuplicateEMail")
                Return False
            End If
        End If
        Try
            m_objCustomer.UpdateCustomer(oCust)
            Message.Visible = True
            Message.Text = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "Success")
            Return True
        Catch objErr As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = objErr.Message
            Return False
        End Try
    End Function
#End Region

#Region "Function Validate_Input"
    '-----------------------------------------------------------
    ' Function Validate_Input
    'returns Boolean
    ' Parameters 
    ' Description:
    ' Checks user input and returns True if input is ok
    '-----------------------------------------------------------
    Private Function Validate_Input() As Boolean
        Dim sErr As String = ""
        If txtCAFirstName.Text = "" Then
            sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankFirstName")
        ElseIf txtCALastName.Text = "" Then
            sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankLastName")
        ElseIf txtCAEMail.Text = "" Then
            sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankEMailAddress")
        ElseIf Trim(txtOldPassWord.Text) <> "" Then
            If Trim(txtOldPassWord.Text) <> Trim(m_objCustomer.CustPassWord) Then
                sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "OldPasswordNotMatch")
            ElseIf txtCAPassword.Text = "" Then
                sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "BlankPassword")
            ElseIf txtCAPassword.Text <> txtCAConfirmPassword.Text Then
                sErr = m_objMessages.GetXMLMessage("CustEdit.aspx", "EditProfile", "PasswordsNotMatch")
            End If
        End If
        If sErr = "" Then
            Return True
        Else
            ErrorMessage.Visible = True
            ErrorMessage.Text = sErr
            Return False
        End If
    End Function
#End Region

End Class
