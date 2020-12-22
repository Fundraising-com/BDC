Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Public Class upsregistration
    Inherits CWebPage
    Protected WithEvents Name As System.Web.UI.WebControls.TextBox
    Protected WithEvents Title As System.Web.UI.WebControls.TextBox
    Protected WithEvents Company As System.Web.UI.WebControls.TextBox
    Protected WithEvents Address As System.Web.UI.WebControls.TextBox
    Protected WithEvents City As System.Web.UI.WebControls.TextBox
    Protected WithEvents State As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Country As System.Web.UI.WebControls.DropDownList
    Protected WithEvents PostalCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Phone As System.Web.UI.WebControls.TextBox
    Protected WithEvents WebSiteURL As System.Web.UI.WebControls.TextBox
    Protected WithEvents Email As System.Web.UI.WebControls.TextBox
    Protected WithEvents UPSAccountNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents Yes As System.Web.UI.WebControls.RadioButton
    Protected WithEvents No As System.Web.UI.WebControls.RadioButton
    Protected WithEvents cmdSubmit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tblImport As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Agree As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Disagree As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents pnlAgreement As System.Web.UI.WebControls.Panel
    Protected WithEvents Agreement As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlSuccess As System.Web.UI.WebControls.Panel
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Private objShippingManagement As New CShippingManagement()

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
        'Put user code to initialize the page here
        Try
            cmdSubmit.Attributes.Add("onclick", "return SetValidation();")
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            'Put user code to initialize the page here

            If IsPostBack = False Then
                objShippingManagement.getUPSLicenseAgreement()
                If objShippingManagement.UPSError.Length > 0 Then
                    ErrorMessage.Text = objShippingManagement.UPSError
                    ErrorMessage.Visible = True
                    pnlAgreement.Visible = False
                    pnlSuccess.Visible = False
                Else
                    Agreement.Text = objShippingManagement.UPSLicense
                    Session("UPSLicesnseText") = objShippingManagement.UPSLicense
                    GetStates()
                    GetCountries()
                    pnlAgreement.Visible = True
                    pnlSuccess.Visible = False
                End If
            End If
        Catch ex As Exception
            Session("DetailError") = "Class UPSRegistration Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub GetCountries()
        Country.DataSource = objShippingManagement.getUPSCountriesDT
        Country.DataValueField = "Abbreviation"
        Country.DataTextField = "Name"
        Country.DataBind()
    End Sub

    Public Sub GetStates()
        State.DataSource = objShippingManagement.getStatesDT
        State.DataValueField = "Abbreviation"
        State.DataTextField = "Name"
        State.DataBind()
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        If Yes.Checked = False And No.Checked = False Then
            ErrorMessage.Text = "Please select whether or not you wish to be contacted by a UPS representative."
            ErrorMessage.Visible = True
            pnlAgreement.Visible = True
            pnlSuccess.Visible = False
        ElseIf Agree.Checked = False Then
            ErrorMessage.Text = "You may not continue if you do not agree to the license agreement."
            ErrorMessage.Visible = True
            pnlAgreement.Visible = True
            pnlSuccess.Visible = False
        Else
            Dim response As Boolean
            objShippingManagement.UPSLicense = Session("UPSLicesnseText")
            response = objShippingManagement.CreateUPSAccount(Company.Text, Name.Text, Title.Text, Email.Text, Phone.Text, WebSiteURL.Text, Yes.Checked, Address.Text, City.Text, State.SelectedItem.Value, Country.SelectedItem.Value, PostalCode.Text, UPSAccountNumber.Text)
            If response = True Then
                ErrorMessage.Visible = False
                pnlAgreement.Visible = False
                pnlSuccess.Visible = True
                Session("UPSLicesnseText") = Nothing
            Else
                ErrorMessage.Text = objShippingManagement.UPSError
                ErrorMessage.Visible = True
                pnlAgreement.Visible = True
                pnlSuccess.Visible = False
            End If
        End If
    End Sub
End Class
