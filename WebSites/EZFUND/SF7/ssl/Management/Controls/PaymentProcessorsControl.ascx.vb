Imports StoreFront.BusinessRule.management
Partial  Class PaymentProcessorsControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents CSPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents avsA As System.Web.UI.WebControls.CheckBox
    Protected WithEvents PPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents ucLinkPointFile As UploadControl
    Protected WithEvents ucFirePayFile As UploadControl
    Protected WithEvents ucCyberSourceFile As UploadControl
    Protected WithEvents ucPsiGateFile As UploadControl
    Protected WithEvents SFImage As System.Web.UI.WebControls.Image



    'PayFuse Integration
    Protected WithEvents PFAuth As System.Web.UI.WebControls.RadioButton

    'StoreFront Payments Gateway
    'Protected WithEvents chkSFPayerAuthentication As System.Web.UI.WebControls.CheckBox

    Private Const CCOMMERCEPAYERAUTH As String = "1"
    Protected WithEvents txtPayFlowLinkURL As System.Web.UI.WebControls.TextBox
    'Verisign Recurring Billing
    'Verisign Recurring Billing
    Private Const CARDINALPAYERAUTH As String = "2"

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

    Private objPaymentManagement As New CPaymentManagement()
    Private m_TransMethodName As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim strPath As String
        'Dim objfile As IO.File
        strPath = Server.MapPath("")

        'If Not (objfile.Exists(strPath & "\images\sfpaygateway.jpg")) Then
        '    Me.SFImage.ImageUrl = "../images/clear.gif"
        'Else
        '    SFImage.ImageUrl = "../images/sfpaygateway.jpg"
        'End If
        If IsPostBack = False Then

            Call getGateWayDD()
            Call loadFromDB()
        Else
            m_TransMethodName = Gateway.SelectedItem.Text
        End If
        ucLinkPointFile.FileType = UploadControl.m_FileType.Pem
        ucFirePayFile.FileType = UploadControl.m_FileType.Pem
        ucPsiGateFile.FileType = UploadControl.m_FileType.Pem
        ucCyberSourceFile.FileType = UploadControl.m_FileType.Pem

        If Page.IsPostBack Then
            Me.SetPayerAuthDisplay(Gateway.SelectedItem.Text)
        End If
        'Verisign Recurring Billing
        If m_TransMethodName.ToLower() = "verisign" Then
            lblActivateRecurring.Visible = True
            chkActivateRecurring.Visible = True
        Else
            lblActivateRecurring.Visible = False
            chkActivateRecurring.Visible = False
        End If
        'Verisign Recurring Billing
    End Sub

    Private Sub loadFromDB()

        BankOfAmerica.Visible = False
        IONGate.Visible = False
        PayPal.Visible = False
        WorldPay.Visible = False
        Barclay.Visible = False
        CyberSource.Visible = False
        FirePay.Visible = False
        LinkPoint.Visible = False
        PsiGate.Visible = False
        SecurePay.Visible = False
        VeriSign.Visible = False
        QuickCommerce.Visible = False
        AuthorizeNet.Visible = False
        SecureSource.Visible = False
        PlanetPayment.Visible = False
        Paradata.Visible = False
        Orbital.Visible = False
        ParadataSFGateway.Visible = False
        CCommerce.Visible = False
        PayFuse.Visible = False
        StoreFrontPayment.Visible = False
        VeriSignPayFlowLink.Visible = False
        Dim dr As DataRow = Nothing
        Select Case m_TransMethodName.ToLower
            Case "paradata - sf payments"
                dr = objPaymentManagement.GetPaymentProcessor("Paradata - SF Payments").Tables(0).Rows(0)
                ParadataSFGateway.Visible = True
                If dr("AuthMode") = 0 Then
                    SFAuthCapture.Checked = True
                    SFAuthOnly.Checked = False
                Else
                    SFAuthCapture.Checked = False
                    SFAuthOnly.Checked = True
                End If
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")

                If Not (IO.File.Exists(strPath & "\images\paradata.jpg")) Then
                    imgSFGateway1.ImageUrl = "../images/clear.gif"
                Else
                    imgSFGateway1.ImageUrl = "../images/paradata.jpg"
                End If
                SFPassword.Text = dr("Pass").ToString
            Case "paradata"
                dr = objPaymentManagement.GetPaymentProcessor("Paradata").Tables(0).Rows(0)
                Paradata.Visible = True
                Me.PDPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    PDAuthCapture.Checked = True
                    PDAuthOnly.Checked = False
                Else
                    PDAuthCapture.Checked = False
                    PDAuthOnly.Checked = True
                End If
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (IO.File.Exists(strPath & "/images/paradata.jpg")) Then
                    imgParaData.ImageUrl = "../images/clear.gif"
                Else
                    imgParaData.ImageUrl = "../images/paradata.jpg"
                End If
            Case "authorizenet"
                dr = objPaymentManagement.GetPaymentProcessor("AuthorizeNet").Tables(0).Rows(0)
                AuthorizeNet.Visible = True
                Me.ANUserName.Text = dr("MerchantID").ToString
                ANPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    ANAuthCapture.Checked = True
                    ANAuthOnly.Checked = False
                Else
                    ANAuthCapture.Checked = False
                    ANAuthOnly.Checked = True
                End If
            Case "securesource"
                dr = objPaymentManagement.GetPaymentProcessor("SecureSource").Tables(0).Rows(0)
                SecureSource.Visible = True
                SSUserName.Text = dr("MerchantID").ToString
                SSPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    SSAuthCapture.Checked = True
                    SSAuthOnly.Checked = False
                Else
                    SSAuthCapture.Checked = False
                    SSAuthOnly.Checked = True
                End If
            Case "planetpayment"
                dr = objPaymentManagement.GetPaymentProcessor("PlanetPayment").Tables(0).Rows(0)
                PlanetPayment.Visible = True
                Me.PPUserName.Text = dr("MerchantID").ToString
                PPPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    PPAuthCapture.Checked = True
                    PPAuthOnly.Checked = False
                Else
                    PPAuthCapture.Checked = False
                    PPAuthOnly.Checked = True
                End If
            Case "quickcommerce"
                dr = objPaymentManagement.GetPaymentProcessor("QuickCommerce").Tables(0).Rows(0)
                QuickCommerce.Visible = True
                Me.QCUserName.Text = dr("MerchantID").ToString
                QCPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    QCAuthCapture.Checked = True
                    QCAuthOnly.Checked = False
                Else
                    QCAuthCapture.Checked = False
                    QCAuthOnly.Checked = True
                End If
            Case "bankofamerica"
                dr = objPaymentManagement.GetPaymentProcessor("BankOfAmerica").Tables(0).Rows(0)
                BankOfAmerica.Visible = True
                BAMerchantID.Text = dr("MerchantID").ToString
                BAUserName.Text = dr("UserID").ToString
                BAPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    BAAuthCapture.Checked = True
                    BAAuthOnly.Checked = False
                Else
                    BAAuthCapture.Checked = False
                    BAAuthOnly.Checked = True
                End If
            Case "iongate"
                IONGate.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("IONGate").Tables(0).Rows(0)
                IGLogin.Text = dr("UserID").ToString
            Case "paypal"
                PayPal.Visible = True
                'SP7
                'dr = objPaymentManagement.GetPaymentProcessor("PayPal").Tables(0).Rows(0)
                'PPMerchantID.Text = dr("MerchantID").ToString
            Case "worldpay"
                WorldPay.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("WorldPay").Tables(0).Rows(0)
                WPMerchantID.Text = dr("MerchantID").ToString
                If dr("TestMode").ToString = "0" Then
                    WPTestMode.Checked = False
                    WPLiveMode.Checked = True
                Else
                    WPTestMode.Checked = True
                    WPLiveMode.Checked = False
                End If
            Case "barclay"
                Barclay.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("Barclay").Tables(0).Rows(0)
                BCUserName.Text = dr("UserID").ToString
                BCPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    BCAuthCapture.Checked = True
                    BCAuthOnly.Checked = False
                Else
                    BCAuthCapture.Checked = False
                    BCAuthOnly.Checked = True
                End If
            Case "cybersource"
                CyberSource.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("CyberSource").Tables(0).Rows(0)
                CSMerchantID.Text = dr("MerchantID").ToString

                ucCyberSourceFile.FileText = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    CSAuthCapture.Checked = True
                    CSAuthOnly.Checked = False
                Else
                    CSAuthCapture.Checked = False
                    CSAuthOnly.Checked = True
                End If
            Case "terra payments"
                FirePay.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("terra payments").Tables(0).Rows(0)
                FPMerchantID.Text = dr("MerchantID").ToString
                FPUserID.Text = dr("UserID").ToString
                ucFirePayFile.FileText = dr("Pass").ToString
            Case "linkpoint"
                LinkPoint.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("LinkPoint").Tables(0).Rows(0)
                LPMerchantID.Text = dr("MerchantID").ToString
                ucLinkPointFile.FileText = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    LPAuthCapture.Checked = True
                    LPAuthOnly.Checked = False
                Else
                    LPAuthCapture.Checked = False
                    LPAuthOnly.Checked = True
                End If
                If dr("TestMode").ToString = "0" Then
                    LPTestMode.Checked = False
                    LPLiveMode.Checked = True
                Else
                    LPTestMode.Checked = True
                    LPLiveMode.Checked = False
                End If
            Case "psigate"
                PsiGate.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("PSIGate").Tables(0).Rows(0)
                PGMerchantID.Text = dr("MerchantID").ToString
                ucPsiGateFile.FileText = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    PGAuthCapture.Checked = True
                    PGAuthOnly.Checked = False
                Else
                    PGAuthCapture.Checked = False
                    PGAuthOnly.Checked = True
                End If
            Case "securepay"
                SecurePay.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("SecurePay").Tables(0).Rows(0)
                SPMerchantID.Text = dr("MerchantID").ToString

            Case "verisign"
                VeriSign.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("VeriSign").Tables(0).Rows(0)
                VSMerchantID.Text = dr("MerchantID").ToString
                ' Split apart UserID to UserID and VendorID
                Dim str() As String = dr("UserID").ToString.Split("|")
                VSUserName.Text = str(0)
                If str.GetUpperBound(0) > 0 Then
                    VSVendorName.Text = str(1)
                End If
                VSPassword.Text = dr("Pass").ToString
                If dr("AuthMode") = 0 Then
                    VSAuthCapture.Checked = True
                    VSAuthOnly.Checked = False
                Else
                    VSAuthCapture.Checked = False
                    VSAuthOnly.Checked = True
                End If
                If dr("TestMode").ToString = "0" Then
                    VTestMode.Checked = False
                    VLiveMode.Checked = True
                Else
                    VTestMode.Checked = True
                    VLiveMode.Checked = False
                End If
                'Verisign Recurring Billing
                lblActivateRecurring.Visible = True
                If dr("RecurringBillingIsActive") = 0 Then
                    chkActivateRecurring.Checked = False
                Else
                    chkActivateRecurring.Checked = True
                End If
                'Verisign Recurring Billing
            Case "orbital"
                dr = objPaymentManagement.GetPaymentProcessor("Orbital").Tables(0).Rows(0)
                Orbital.Visible = True
                Me.ORMerchantID.Text = dr("MerchantId").ToString
                If dr("AuthMode") = 0 Then
                    ORAuthCapture.Checked = True
                    ORAuthOnly.Checked = False
                Else
                    ORAuthCapture.Checked = False
                    ORAuthOnly.Checked = True
                End If

                OrbLive.Checked = (dr("TestMode") = 0)
                OrbTest.Checked = (dr("TestMode") = 1)
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (IO.File.Exists(strPath & "/images/glo_log_orb.gif")) Then
                    imgOrbital.ImageUrl = "../images/clear.gif"
                Else
                    imgOrbital.ImageUrl = "../images/glo_log_orb.gif"
                End If
            Case "clear commerce"
                dr = objPaymentManagement.GetPaymentProcessor("Clear Commerce").Tables(0).Rows(0)
                CCommerce.Visible = True
                Me.txtCCUserId.Text = dr("UserID").ToString
                Me.txtCCClientID.Text = dr("MerchantID").ToString
                Me.txtCCPassword.Text = dr("Pass").ToString
                Me.CCAuthOnly.Checked = dr("AuthMode") = 1
                Me.CCAuthCap.Checked = dr("AuthMode") = 0
                CCLive.Checked = (dr("TestMode") = 0)
                CCTest.Checked = (dr("TestMode") = 1)
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (IO.File.Exists(strPath & "/images/cclogo80_120.gif")) Then
                    imgCCommerce.ImageUrl = "../images/clear.gif"
                Else
                    imgCCommerce.ImageUrl = "../images/cclogo80_120.gif"
                End If

            Case "payfuse"
                dr = objPaymentManagement.GetPaymentProcessor("PayFuse").Tables(0).Rows(0)
                PayFuse.Visible = True
                Me.txtPFUserId.Text = dr("UserID").ToString
                Me.txtPFClientId.Text = dr("MerchantID").ToString
                Me.txtPFPassword.Text = dr("Pass").ToString
                '2733
                Me.PFAuthOnly.Checked = dr("AuthMode") = 1
                Me.PFAuthCap.Checked = dr("AuthMode") = 0

                PFLive.Checked = (dr("TestMode") = 0)
                PFTest.Checked = (dr("TestMode") = 1)
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (IO.File.Exists(strPath & "/images/cclogo80_120.gif")) Then
                    imgPFuse.ImageUrl = "../images/clear.gif"
                Else
                    imgPFuse.ImageUrl = "../images/payfuse.jpg"
                End If
                'StoreFront Payment ClearCommerce Integration
            Case "verisignpayflowlink"
                dr = objPaymentManagement.GetPaymentProcessor("VeriSignPayFlowLink").Tables(0).Rows(0)
                Me.txtPayFlowLinkMerchantId.Text = dr("MerchantId").ToString
                Me.txtpayflowlinkPartnerId.Text = dr("UserId").ToString
                Me.txtPayFlowLinkPassword.Text = dr("Pass").ToString
                Me.rbPayFlowLinkAuthCapt.Checked = dr("AuthMode") = 0
                Me.rbPayFlowLinkAuthorize.Checked = dr("AuthMode") = 1
                Me.VeriSignPayFlowLink.Visible = True

            Case "storefront payments gateway"
                dr = objPaymentManagement.GetPaymentProcessor("StoreFront Payments Gateway").Tables(0).Rows(0)
                StoreFrontPayment.Visible = True
                Me.txtSFUserId.Text = dr("UserID").ToString
                Me.txtSFClientId.Text = dr("MerchantID").ToString
                Me.txtSFPassword.Text = dr("Pass").ToString

                If dr("AuthMode") = 0 Then
                    Me.SFAuth.Checked = True
                    Me.SFAuthCap.Checked = False
                Else
                    Me.SFAuth.Checked = False
                    Me.SFAuthCap.Checked = True
                End If
                SFLive.Checked = (dr("TestMode") = 0)
                SFTest.Checked = (dr("TestMode") = 1)
                'Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (IO.File.Exists(strPath & "../images/sfpaygateway.jpg")) Then
                    imgCCommerce.ImageUrl = "../images/clear.gif"
                Else
                    imgCCommerce.ImageUrl = "../images/sfpaygateway.jpg"
                End If
        End Select
        LoadPayerAuthValuesFromDB(m_TransMethodName.ToLower, dr)
        Me.SetPayerAuthDisplay(m_TransMethodName.ToLower)
    End Sub

    Private Sub getGateWayDD()
        Dim dt As DataTable
        Dim x As Integer
        dt = objPaymentManagement.getPaymentProcessorsDT
        Gateway.DataSource = dt
        Gateway.DataValueField = "ID"
        Gateway.DataTextField = "Display"
        Gateway.DataBind()
        For x = 0 To dt.Rows.Count - 1
            If (dt.Rows(x).Item("ID") = objPaymentManagement.AdminInfo.TransactionMethod) Then
                Gateway.SelectedIndex = x
                m_TransMethodName = dt.Rows(x).Item("Display").ToString
                Exit For
            End If
        Next

    End Sub

    Private Sub Gateway_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Gateway.SelectedIndexChanged
        Call loadFromDB()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim authMode As Integer
        Dim testMode As Integer
        Dim myProcessorName As String = String.Empty
        Select Case m_TransMethodName.ToLower
            Case "paradata - sf payments"
                If SFAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "paradata - sf payments"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, "", "", SFPassword.Text, authMode)

            Case "paradata"
                If PDAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "Paradata"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, "", "", Me.PDPassword.Text, authMode)
            Case "bankofamerica"
                If BAAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "BankOfAmerica"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, BAMerchantID.Text, BAUserName.Text, BAPassword.Text, authMode)
            Case "authorizenet"
                If ANAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "AuthorizeNet"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, ANUserName.Text, "", ANPassword.Text, authMode)
            Case "securesource"
                If SSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "SecureSource"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, SSUserName.Text, "", SSPassword.Text, authMode)

            Case "planetpayment"
                If PPAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "PlanetPayment"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, PPUserName.Text, "", PPPassword.Text, authMode)
            Case "quickcommerce"
                If QCAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "QuickCommerce"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, Me.QCUserName.Text, "", QCPassword.Text, authMode)
            Case "iongate"
                myProcessorName = "IONGate"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, "", IGLogin.Text, "", 0)
            Case "paypal"
                myProcessorName = "PayPal"
                'SP7
                objPaymentManagement.setPayPalAsProcessor()
                'objPaymentManagement.UpdatePaymentProcessor(myProcessorName, PPMerchantID.Text, "", "", 0)
            Case "worldpay"
                myProcessorName = "WorldPay"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, WPMerchantID.Text, "", "", authMode)
                If WPTestMode.Checked Then
                    testMode = 1
                Else
                    testMode = 0
                End If
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, testMode)
            Case "barclay"
                If BCAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "Barclay"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, "", BCUserName.Text, BCPassword.Text, authMode)
            Case "cybersource"
                If CSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "CyberSource"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, CSMerchantID.Text, "", ucCyberSourceFile.FileText, authMode)
            Case "terra payments"
                myProcessorName = "terra payments"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, FPMerchantID.Text, FPUserID.Text, ucFirePayFile.FileText, 0)
            Case "linkpoint"

                If LPAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "LinkPoint"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, LPMerchantID.Text, "", ucLinkPointFile.FileText, authMode)
                If LPTestMode.Checked Then
                    testMode = 1
                Else
                    testMode = 0
                End If
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, testMode)
            Case "psigate"
                If PGAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                myProcessorName = "PsiGate"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, PGMerchantID.Text, "", ucPsiGateFile.FileText, authMode)
            Case "securepay"
                myProcessorName = "SecurePay"
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, SPMerchantID.Text, "", "", 0)
            Case "verisign"
                myProcessorName = "VeriSign"
                If VSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                ' VeriSign uses both a Vendor and User field - they are combined into UserID for storage
                VSUserName.Text = VSUserName.Text & "|" & VSVendorName.Text

                'Verisign Recurring Billing
                Dim activateRecurring As Boolean = False
                If chkActivateRecurring.Checked Then
                    activateRecurring = True
                End If
                'Verisign Recurring Billing
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, VSMerchantID.Text, VSUserName.Text, VSPassword.Text, authMode, activateRecurring)

                ' Split apart UserID to UserID and VendorID
                Dim str() As String = VSUserName.Text.Split("|")
                VSUserName.Text = str(0)
                If str.GetUpperBound(0) > 0 Then
                    VSVendorName.Text = str(1)
                End If
                If VTestMode.Checked Then
                    testMode = 1
                Else
                    testMode = 0
                End If
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, testMode)
            Case "orbital"
                myProcessorName = "Orbital"
                authMode = IIf(ORAuthOnly.Checked, 1, 0)
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, IIf(OrbTest.Checked, 1, 0))
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, ORMerchantID.Text, "", "", authMode)
            Case "clear commerce"
                myProcessorName = "clear commerce"
                authMode = IIf(CCAuthOnly.Checked, 1, 0)
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, IIf(Me.CCTest.Checked, 1, 0))
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, txtCCClientID.Text, txtCCUserId.Text, txtCCPassword.Text, authMode)
            Case "payfuse"
                myProcessorName = "PayFuse"
                authMode = IIf(PFAuthOnly.Checked, 1, 0)
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, IIf(Me.PFTest.Checked, 1, 0))
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, txtPFClientId.Text, txtPFUserId.Text, txtPFPassword.Text, authMode)
            Case "storefront payments gateway"
                myProcessorName = "storefront payments gateway"
                authMode = IIf(SFAuth.Checked, 1, 0)
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, IIf(Me.SFTest.Checked, 1, 0))
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, txtSFClientId.Text, txtSFUserId.Text, txtSFPassword.Text, authMode)
            Case "verisignpayflowlink"
                myProcessorName = "verisignpayflowlink"
                authMode = IIf(Me.rbPayFlowLinkAuthorize.Checked, 1, 0)
                objPaymentManagement.UpdatePaymentTestMode(myProcessorName, 0)
                objPaymentManagement.UpdatePaymentProcessor(myProcessorName, Me.txtPayFlowLinkMerchantId.Text, Me.txtpayflowlinkPartnerId.Text, Me.txtPayFlowLinkPassword.Text, authMode)
                ' begin: jdb - ECheck Limitation
                If objPaymentManagement.AcceptEcheck Then
                    objPaymentManagement.UpdateECheck(False)
                    RaiseEvent DisplayError("E-Checks have been de-selected as a Payment Method as they are not supported with this Gateway Provider.", System.EventArgs.Empty)
                End If
                ' end: jdb - ECheck Limitation
        End Select
        SavePayerAuthentication(myProcessorName, objPaymentManagement)
    End Sub

    ' begin: jdb - ECheck Limitation
    Event DisplayError As EventHandler
    ' end: jdb - ECheck Limitation

    Private Sub SavePayerAuthentication(ByVal ProcessorName As String, ByVal mManagement As CPaymentManagement)
        If Not Me.pnlPayerAuth.Visible Then Return

        If Not Me.chkEnableCardinalAuth.Checked Then
            mManagement.UpdatePaymentPayerAuthentication(ProcessorName, String.Empty, False, String.Empty, String.Empty, String.Empty)
            Return
        End If

        If Me.ddPayerAuthProviders.SelectedItem.Value = PaymentProcessorsControl.CCOMMERCEPAYERAUTH Then
            mManagement.UpdatePaymentPayerAuthentication(ProcessorName, Me.lstCCServices.SelectedItem.Text, True, Me.txtPayerauthenticationURL.Text, Me.txtPayerAuthClientId.Text, String.Empty)
        ElseIf Me.ddPayerAuthProviders.SelectedItem.Value = PaymentProcessorsControl.CARDINALPAYERAUTH Then
            mManagement.UpdatePaymentPayerAuthentication(ProcessorName, String.Empty, True, Me.txtCentinelMapsUrl.Text, Me.txtCardinalMerchantId.Text, Me.txtCardinalProcessorId.Text)
        End If
    End Sub

    Private Function PayerAuthenticationEnabled(ByVal ProcessorName As String) As Boolean
        Select Case ProcessorName.ToLower
            Case "clear commerce", "verisign", "authorizenet", "cybersource", "storefront payments gateway"
                Return True
            Case Else
                Return False
        End Select
    End Function
    Private Function PayerAuthenticationProviders(ByVal ProcessorName As String) As ListItemCollection
        Dim myList As New ListItemCollection
        Dim myItem As UI.WebControls.ListItem
        Select Case ProcessorName.ToLower
            Case "clear commerce", "storefront payments gateway"
                myItem = New WebControls.ListItem
                myItem.Text = "Clear Commerce"
                myItem.Value = PaymentProcessorsControl.CCOMMERCEPAYERAUTH
                myItem.Selected = True
                myList.Add(myItem)
            Case "verisign", "cybersource", "authorizenet"
                myItem = New WebControls.ListItem
                myItem.Text = "Centinel Cardinal"
                myItem.Value = PaymentProcessorsControl.CARDINALPAYERAUTH
                myItem.Selected = True
                myList.Add(myItem)
        End Select
        Return myList
    End Function
    Private Sub SetPayerAuthDisplay(ByVal ProcessorName As String)
        Me.pnlPayerAuth.Visible = Me.PayerAuthenticationEnabled(ProcessorName)
        If Not Me.pnlPayerAuth.Visible Then Return
        Dim myITem As WebControls.ListItem
        Me.ddPayerAuthProviders.Items.Clear()
        For Each myITem In Me.PayerAuthenticationProviders(ProcessorName)
            ddPayerAuthProviders.Items.Add(myITem)
        Next
        Me.pnlPayerAuthMain.Visible = Me.chkEnableCardinalAuth.Checked
        Me.SetPayerAuthProviderDisplay(Me.ddPayerAuthProviders.SelectedItem.Value)
    End Sub
    Private Sub SetPayerAuthProviderDisplay(ByVal ProviderValue As String)
        Select Case ProviderValue
            Case PaymentProcessorsControl.CCOMMERCEPAYERAUTH
                Me.pnlCardinalPayerAuth.Visible = False
                Me.pnlCCommercePayerAuth.Visible = True
            Case PaymentProcessorsControl.CARDINALPAYERAUTH
                Me.pnlCardinalPayerAuth.Visible = True
                Me.pnlCCommercePayerAuth.Visible = False
        End Select
    End Sub

    Private Sub LoadPayerAuthValuesFromDB(ByVal ProcessorName As String, ByVal dr As DataRow)
        If Not Me.PayerAuthenticationEnabled(ProcessorName) Then Return

        Me.chkEnableCardinalAuth.Checked = (Not TypeOf dr("PayerAuthSel") Is DBNull) AndAlso (dr("PayerAuthSel") = 1)

        Dim myCollection As ListItemCollection = Me.PayerAuthenticationProviders(ProcessorName)

        Dim myItem As WebControls.ListItem = Nothing

        For Each myItem In myCollection
            If myItem.Selected Then Exit For
        Next

        If myItem Is Nothing Then Return

        If myItem.Value = PaymentProcessorsControl.CCOMMERCEPAYERAUTH Then
            Me.txtPayerAuthClientId.Text = dr("ccpaclientid").ToString & ""
            Me.txtPayerauthenticationURL.Text = dr("PayerAuthURL").ToString & ""
            For Each myItem In Me.lstCCServices.Items
                If myItem.Text = dr("service").ToString & "" Then
                    myItem.Selected = True
                Else
                    myItem.Selected = False
                End If
            Next
        ElseIf myItem.Value = PaymentProcessorsControl.CARDINALPAYERAUTH Then
            Me.txtCardinalMerchantId.Text = dr("ccpaclientid").ToString & ""
            Me.txtCardinalProcessorId.Text = dr("processorid").ToString & ""
            Me.txtCentinelMapsUrl.Text = dr("PayerAuthURL").ToString & ""
        End If
    End Sub
End Class
