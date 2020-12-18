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

Partial Class affiliateregister
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    'Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Private _New As Boolean = True

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

        cmdSave.Attributes.Add("onclick", "return SetValidation();")

        m_Affiliate = Session("Affiliate")
        ErrorMessage.Visible = False
        If IsNothing(Me.m_Affiliate) Then
            Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
        ElseIf m_Affiliate.IsSignedIn = False Then
            Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
        End If
        Try
            SetPageTitle = m_objMessages.GetXMLMessage("affiliateaccount.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell)
            If Not IsPostBack Then
                PopulateMe()
            End If
            If IsNothing(m_Affiliate.Address) Then
                _New = True
                ShowME.Visible = False

            ElseIf m_Affiliate.Address.FirstName = "" Then
                _New = True
                ShowME.Visible = False

            Else
                _New = False
                ShowME.Visible = True
                EMail.Text = m_Affiliate.Email
            End If

            imgSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Save").Attributes("Filepath").Value
        Catch ex As Exception
            Session("DetailError") = "Class AffiliateRegister Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public ReadOnly Property ISNEW() As Boolean
        Get
            Return _New
        End Get
    End Property

    Private Function CheckAddressFields() As Boolean
        If (FirstName.Text = "" Or _
            Address1.Text = "" Or City.Text = "" Or _
            Phone.Text = "") Then
            ErrorMessage.Text = ""
            If (FirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("AffRegister.aspx", "EditAff", "BlankFirstName")
            End If
            If (Address1.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("AffRegister.aspx", "EditAff", "BlankAddress1")
            End If
            If (City.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("AffRegister.aspx", "EditAff", "BlankCity")
            End If
            'If (Zip.Text = "") Then
            '    ErrorMessage.Text = m_objMessages.GetXMLMessage("AffRegister.aspx", "EditAff", "BlankZip")
            'End If
            If (Phone.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("AffRegister.aspx", "EditAff", "BlankPhone")
            End If


            ErrorMessage.Visible = True

            Return False
        Else
            SaveMe()
            Return True
        End If
    End Function

    Private Function CheckAffiliateInput() As Boolean

        If EMail.Text = "" Or password.Text = "" Or _
       Me.WebSite.Text = "" Or Confirmpassword.Text = "" Then

            If (EMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankEMailAddress")
            ElseIf (password.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankPassword")
            ElseIf (WebSite.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankSite")

            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankConfirmPassword")
            End If
            ErrorMessage.Visible = True
            Return False
            Exit Function
        ElseIf (password.Text <> Confirmpassword.Text) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "PasswordConfirmEqual")
            ErrorMessage.Visible = True
            Return False
            Exit Function
        End If
        SaveMe()
        Return True
    End Function

    Private Sub PopulateMe()
        Dim i As Integer
        FirstName.Text = m_Affiliate.Address.FirstName
        MI.Text = m_Affiliate.Address.MI
        LastName.Text = m_Affiliate.Address.LastName
        Company.Text = m_Affiliate.Address.Company
        Address1.Text = m_Affiliate.Address.Address1
        Address2.Text = m_Affiliate.Address.Address2
        City.Text = m_Affiliate.Address.City
        For i = 0 To State.Items.Count - 1
            If State.Items(i).Value = m_Affiliate.Address.State Then
                State.SelectedIndex = i
            End If
        Next
        Zip.Text = m_Affiliate.Address.Zip
        For i = 0 To Country.Items.Count - 1
            If Country.Items(i).Value = m_Affiliate.Address.Country Then
                Country.SelectedIndex = i
            End If
        Next
        Phone.Text = m_Affiliate.Address.Phone
        Fax.Text = m_Affiliate.Address.Fax
        EMail.Text = m_Affiliate.Email
        Me.password.Text = m_Affiliate.Password

        Me.WebSite.Text = m_Affiliate.HomePage

    End Sub
    Private Sub SaveMe()
        'Dim i As Integer
        m_Affiliate.Address.FirstName = FirstName.Text
        m_Affiliate.Address.MI = MI.Text
        m_Affiliate.Address.LastName = LastName.Text
        m_Affiliate.Address.Company = Company.Text
        m_Affiliate.Address.Address1 = Address1.Text
        m_Affiliate.Address.Address2 = Address2.Text
        m_Affiliate.Address.City = City.Text
        m_Affiliate.Address.State = State.SelectedItem.Value
        m_Affiliate.Address.Zip = Zip.Text

        m_Affiliate.Address.Country = Country.SelectedItem.Value
        m_Affiliate.Address.Phone = Phone.Text
        m_Affiliate.Address.Fax = Fax.Text
        m_Affiliate.Address.EMail = EMail.Text
        m_Affiliate.Email = EMail.Text
        m_Affiliate.Password = Me.password.Text

        m_Affiliate.HomePage = Me.WebSite.Text

    End Sub

    Private Overloads Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckAddressFields() Then
            If Me.ISNEW = True Then

                m_Affiliate.UpdateAffiliate()
                ShowME.Visible = True
            ElseIf CheckAffiliateInput() Then

                m_Affiliate.UpdateAffiliate()
                ShowME.Visible = True
                If m_Affiliate.NewAffiliate = True Then
                    m_Affiliate.NewAffiliate = False
                    Response.Redirect(StoreFrontConfiguration.SiteURL & "affiliatecommissions.aspx")
                    Exit Sub
                End If
            End If
        End If
    End Sub
End Class