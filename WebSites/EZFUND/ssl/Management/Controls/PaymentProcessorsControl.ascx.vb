Imports StoreFront.BusinessRule.management
Public MustInherit Class PaymentProcessorsControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents Gateway As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CSMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents CSPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents avsA As System.Web.UI.WebControls.CheckBox
    Protected WithEvents FirePay As System.Web.UI.WebControls.Panel
    Protected WithEvents CyberSource As System.Web.UI.WebControls.Panel
    Protected WithEvents FPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents LPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents LinkPoint As System.Web.UI.WebControls.Panel
    Protected WithEvents PGMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents PsiGate As System.Web.UI.WebControls.Panel
    Protected WithEvents SPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents SecurePay As System.Web.UI.WebControls.Panel
    Protected WithEvents VSUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents VSPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents VeriSign As System.Web.UI.WebControls.Panel
    Protected WithEvents BAMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents BAUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents BAPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents BAAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents BAAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents BankOfAmerica As System.Web.UI.WebControls.Panel
    Protected WithEvents IGLogin As System.Web.UI.WebControls.TextBox
    Protected WithEvents IONGate As System.Web.UI.WebControls.Panel
    Protected WithEvents PPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents PayPal As System.Web.UI.WebControls.Panel
    Protected WithEvents WPMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents WorldPay As System.Web.UI.WebControls.Panel
    Protected WithEvents BCUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents BCPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents BCAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents BCAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Barclay As System.Web.UI.WebControls.Panel
    Protected WithEvents CSAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents CSAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents LPAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents LPAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents PGAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents PGAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents VSAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents VSAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents QCUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents QCPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents QCAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents QCAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents QuickCommerce As System.Web.UI.WebControls.Panel
    Protected WithEvents ANUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ANPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents ANAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ANAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents AuthorizeNet As System.Web.UI.WebControls.Panel
    Protected WithEvents PPUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents PPPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents PPAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents PPAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents PlanetPayment As System.Web.UI.WebControls.Panel
    Protected WithEvents SSUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents SSPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents SSAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents SSAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents SecureSource As System.Web.UI.WebControls.Panel
    Protected WithEvents ucLinkPointFile As UploadControl
    Protected WithEvents ucFirePayFile As UploadControl
    Protected WithEvents ucCyberSourceFile As UploadControl
    Protected WithEvents ucPsiGateFile As UploadControl
    Protected WithEvents Paradata As System.Web.UI.WebControls.Panel
    Protected WithEvents PDPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents PDAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents PDAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents SFPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents SFAuthOnly As System.Web.UI.WebControls.RadioButton
    Protected WithEvents SFAuthCapture As System.Web.UI.WebControls.RadioButton
    Protected WithEvents StoreFrontGateway As System.Web.UI.WebControls.Panel
    Protected WithEvents imgSFGateway1 As System.Web.UI.WebControls.Image
    Protected WithEvents imgParaData As System.Web.UI.WebControls.Image
    Protected WithEvents SFImage As System.Web.UI.WebControls.Image
    Protected WithEvents FPUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents WPTestMode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents WPLiveMode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents LPTestMode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents LPLiveMode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents VTestMode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents VSMerchantID As System.Web.UI.WebControls.TextBox
    Protected WithEvents VSVendorName As System.Web.UI.WebControls.TextBox
    Protected WithEvents VLiveMode As System.Web.UI.WebControls.RadioButton

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
        Dim objfile As IO.File
        strPath = Server.MapPath("")

        If Not (objfile.Exists(strPath & "\images\sfpaygateway.jpg")) Then
            Me.SFImage.ImageUrl = "../images/clear.gif"
        Else
            SFImage.ImageUrl = "../images/sfpaygateway.jpg"
        End If
        If IsPostBack = False Then

            Call getGateWayDD()
            Call loadFromDB()
        Else
            m_TransMethodName = Gateway.SelectedItem.Text
        End If
        ucLinkPointFile.FileType = UploadControl._FileType.Pem
        ucFirePayFile.FileType = UploadControl._FileType.Pem
        ucPsiGateFile.FileType = UploadControl._FileType.Pem
        ucCyberSourceFile.FileType = UploadControl._FileType.Pem
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
        StoreFrontGateway.Visible = False
        Dim dr As DataRow
        Select Case m_TransMethodName.ToLower
            Case "storefront payment gateway"
                dr = objPaymentManagement.GetPaymentProcessor("StoreFront Payment Gateway").Tables(0).Rows(0)
                StoreFrontGateway.Visible = True
                If dr("AuthMode") = 0 Then
                    SFAuthCapture.Checked = True
                    SFAuthOnly.Checked = False
                Else
                    SFAuthCapture.Checked = False
                    SFAuthOnly.Checked = True
                End If
                Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")

                If Not (objFile.Exists(strPath & "\images\sfpaygateway.jpg")) Then
                    imgSFGateway1.ImageUrl = "../images/clear.gif"
                Else
                    imgSFGateway1.ImageUrl = "../images/sfpaygateway.jpg"
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
                Dim objFile As IO.File
                Dim strPath As String
                strPath = Server.MapPath("")
                If Not (objFile.Exists(strpath & "/images/paradata.jpg")) Then
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
                dr = objPaymentManagement.GetPaymentProcessor("PayPal").Tables(0).Rows(0)
                PPMerchantID.Text = dr("MerchantID").ToString
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
            Case "firepay"
                FirePay.Visible = True
                dr = objPaymentManagement.GetPaymentProcessor("FirePay").Tables(0).Rows(0)
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

        End Select
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
        Select Case m_TransMethodName.ToLower
            Case "storefront payment gateway"
               
                If SFAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("StoreFront Payment Gateway", "", "", SFPassword.Text, authMode)
                
            Case "paradata"
                If PDAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("Paradata", "", "", Me.PDPassword.Text, authMode)
            Case "bankofamerica"
                If BAAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("BankOfAmerica", BAMerchantID.Text, BAUserName.Text, BAPassword.Text, authMode)
            Case "authorizenet"
                If ANAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("AuthorizeNet", ANUserName.Text, "", ANPassword.Text, authMode)
            Case "securesource"
                If SSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("SecureSource", SSUserName.Text, "", SSPassword.Text, authMode)

            Case "planetpayment"
                If PPAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("PlanetPayment", PPUserName.Text, "", PPPassword.Text, authMode)
            Case "quickcommerce"
                If QCAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("QuickCommerce", Me.QCUserName.Text, "", QCPassword.Text, authMode)
            Case "iongate"
                objPaymentManagement.UpdatePaymentProcessor("IONGate", "", IGLogin.Text, "", 0)
            Case "paypal"
                objPaymentManagement.UpdatePaymentProcessor("PayPal", PPMerchantID.Text, "", "", 0)

            Case "worldpay"
                objPaymentManagement.UpdatePaymentProcessor("WorldPay", WPMerchantID.Text, "", "", authMode)
                If WPTestMode.Checked Then
                    testMode = 1
                Else
                    testMode = 0
                End If
                objPaymentManagement.UpdatePaymentTestMode("WorldPay", testMode)
            Case "barclay"
                If BCAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("Barclay", "", BCUserName.Text, BCPassword.Text, authMode)


            Case "cybersource"
                If CSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("CyberSource", CSMerchantID.Text, "", ucCyberSourceFile.FileText, authMode)

            Case "firepay"
                objPaymentManagement.UpdatePaymentProcessor("FirePay", FPMerchantID.Text, FPUserID.Text, ucFirePayFile.FileText, 0)
            Case "linkpoint"
                If LPAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("LinkPoint", LPMerchantID.Text, "", ucLinkPointFile.FileText, authMode)
                If LPTestMode.Checked Then
                    testMode = 1
                Else
                    testMode = 0
                End If
                objPaymentManagement.UpdatePaymentTestMode("LinkPoint", testMode)

            Case "psigate"
                If PGAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                objPaymentManagement.UpdatePaymentProcessor("PsiGate", PGMerchantID.Text, "", ucPsiGateFile.FileText, authMode)

            Case "securepay"
                objPaymentManagement.UpdatePaymentProcessor("SecurePay", SPMerchantID.Text, "", "", 0)
            Case "verisign"
                If VSAuthCapture.Checked Then
                    authMode = 0
                Else
                    authMode = 1
                End If
                ' VeriSign uses both a Vendor and User field - they are combined into UserID for storage
                VSUserName.Text = VSUserName.Text & "|" & VSVendorName.Text

                objPaymentManagement.UpdatePaymentProcessor("VeriSign", VSMerchantID.Text, VSUserName.Text, VSPassword.Text, authMode)

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
                objPaymentManagement.UpdatePaymentTestMode("VeriSign", testMode)

        End Select
    End Sub

End Class
