Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
' -----------------------------------------------------------------------------
' SP7 - PayPal/VeriSign Integration
' -----------------------------------------------------------------------------
' <summary>
' </summary>
' <remarks>This class handles the  information
' </remarks>
' <history>AB Code 
'		Created On:	05/03/2005
'		Last Revised on :
' </history>
' -----------------------------------------------------------------------------

Partial Class PayPal
    Inherits CWebPage


    'BEGINVERSIONINFO

    'APPVERSION: 7.0.0

    'STARTCOPYRIGHT
    'The contents of this file are protected under the United States
    'copyright laws and is confidential and proprietary to
    'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
    'expressed written permission of LaGarde, Incorporated is expressly prohibited.
    '
    '(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
    'ENDCOPYRIGHT

    'ENDVERSIONINFO

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub



    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objPaymentManagement As New CPaymentManagement


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.PayPal) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Put user code to initialize the page here

            cmdSave.Attributes.Add("onclick", "return ValidateSave();")

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            If Page.IsPostBack = False Then
                Me.GetPayPalInfo()
            End If
            ErrorMessage.Visible = False
        Catch ex As Exception
            Session("DetailError") = "Class PayPal Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Private Sub GetPayPalInfo()"
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>Gets the PayPal information from the db
    ' </summary>
    ' <remarks>
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	05/03/2005
    '		Last Revised on :06/01/2005
    ' </history>
    ' -----------------------------------------------------------------------------
    Private Sub GetPayPalInfo()
        Dim dr As DataRow
        dr = objPaymentManagement.GetPaymentProcessor("PayPal").Tables(0).Rows(0)
        Me.PPMerchantID.Text = dr("MerchantID").ToString
        Me.PPMerchantPWD.Text = dr("UserID").ToString
        Me.PPPrivateKeyPWD.Text = dr("Pass").ToString
        txtthirdpartycertfilepath.Text = dr("Pass").ToString
        txtPayPalEmailID.Text = dr("MerchantID").ToString

        If dr("AuthMode") = 0 Then
            Me.PayPalAuthOnly.Checked = False
            Me.PayPalSale.Checked = True
        Else
            PayPalAuthOnly.Checked = True
            PayPalSale.Checked = False
        End If
        ''need to change
        If objPaymentManagement.AcceptPayPal = True Then
            chkBoxPayPalAsPayMethod.Checked = True
        Else
            chkBoxPayPalAsPayMethod.Checked = False
        End If
        If objPaymentManagement.AdminInfo.AcceptPayPalExpress Then
            chkBoxExpress.Checked = True
        Else
            chkBoxExpress.Checked = False
        End If
        If objPaymentManagement.AdminInfo.PayPalFirstPartyCertificate Then
            firstParty.Checked = True
            pnlFirstParty.Visible = True
            thirdParty.Checked = False
            pnlThirdParty.Visible = False

        Else
            pnlThirdParty.Visible = True
            thirdParty.Checked = True

            pnlFirstParty.Visible = False
            firstParty.Checked = False
        End If
    End Sub
    'End SP7
#End Region

#Region "Private Sub SavePayPalInfo()"
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>Updates the PayPal info in the db
    ' </summary>
    ' <remarks>
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	05/03/05
    '		Last Revised on :
    ' </history>
    ' -----------------------------------------------------------------------------
    Private Sub SavePayPalInfo()

        Dim authmode As Integer
        Dim errorMessage As String = "PayPal Express Checkout requires that you accept PayPal as a Payment Method. Your selection could not be saved.  Please check 'Accept PayPal as a Payment Method' and try again."


        If chkBoxExpress.Checked And Not Me.chkBoxPayPalAsPayMethod.Checked Then
            Throw New ApplicationException(errorMessage)
        End If

        If Me.PayPalAuthOnly.Checked = True Then
            authmode = 1
        Else
            authmode = 0
        End If



        If chkBoxExpress.Checked Then
            objPaymentManagement.AdminInfo.AcceptPayPalExpress = True
        Else
            objPaymentManagement.AdminInfo.AcceptPayPalExpress = False
        End If
        If chkBoxPayPalAsPayMethod.Checked Then
            objPaymentManagement.AcceptPayPal = True
        Else
            objPaymentManagement.AcceptPayPal = False
        End If
        If thirdParty.Checked = True Then
            objPaymentManagement.AdminInfo.PayPalFirstPartyCertificate = False
            objPaymentManagement.UpdatePayPalInfo(txtPayPalEmailID.Text, "", txtthirdpartycertfilepath.Text, authmode)
        Else
            objPaymentManagement.AdminInfo.PayPalFirstPartyCertificate = True
            objPaymentManagement.UpdatePayPalInfo(Me.PPMerchantID.Text, Me.PPMerchantPWD.Text, Me.PPPrivateKeyPWD.Text, authmode)
        End If

    End Sub

    'End SP7
#End Region

#Region "Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click"
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>Handles the Click event of Save Button
    ' </summary>
    ' <remarks>Calls the savepayPalInfo function
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	05/03/2005
    '		Last Revised on :
    ' </history>
    ' -----------------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Me.SavePayPalInfo()
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "PayPal information could not be saved due to <br>" & ex.Message
        End Try
    End Sub
    'End SP7

#Region "Certfication type selection changed evenst"
    ' -----------------------------------------------------------------------------
    ' SP7 - PayPal/VeriSign Integration
    ' -----------------------------------------------------------------------------
    ' <summary>Public Sub PayPalCertificateTypeChanged()
    ' </summary>
    ' <remarks>This would change the visiblity of the panels pnlThirdParty and pnlFirstParty depending
    ' upon which radio button option was checked.
    ' </remarks>
    ' <history>AB Code 
    '		Created On:	06/01/2005
    '		Last Revised on :
    ' </history>
    ' -----------------------------------------------------------------------------
    Public Sub PayPalCertificateTypeChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim myRadioButton As RadioButton
        myRadioButton = CType(sender, RadioButton)
        If myRadioButton.ID.ToLower.Equals("thirdparty") AndAlso myRadioButton.Checked = True Then
            thirdParty.Checked = True
            pnlThirdParty.Visible = True

            pnlFirstParty.Visible = False
            firstParty.Checked = False
        Else
            pnlFirstParty.Visible = True
            firstParty.Checked = True

            thirdParty.Checked = False
            pnlThirdParty.Visible = False
        End If
    End Sub

    'End SP7
#End Region

#End Region




End Class

'End SP7