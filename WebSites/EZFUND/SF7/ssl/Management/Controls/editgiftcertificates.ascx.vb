Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Partial  Class editgiftcertificates
    Inherits CWebControl

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

    Private m_objGift As CGiftCertificate

    Event Save As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'btnSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSave").Attributes("Filepath").Value
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub

    Public Property EditGiftCertificateID() As CGiftCertificate
        Get
            Return m_objGift
        End Get
        Set(ByVal Value As CGiftCertificate)
            m_objGift = Value

            txtGiftCode.Text = m_objGift.Code
            txtAmount.Text = m_objGift.DollarOff

            If (m_objGift.Expires = "Never") Then
                DropDownList1.SelectedIndex = 0
                txtDate.Text = ""
            Else
                DropDownList1.SelectedIndex = 1
                txtDate.Text = m_objGift.Expires
            End If
            Session("Gift") = Value
        End Set
    End Property

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        m_objGift = Session("Gift")
        m_objGift.DollarOff = CDec(txtAmount.Text)
        m_objGift.Code = txtGiftCode.Text

        If (DropDownList1.SelectedIndex = 0) Then
            m_objGift.Expires = "Never"
        Else
            m_objGift.Expires = txtDate.Text
        End If
        'm_objGift.Remaining = m_objGift.DollarOff

        Dim obj As New CStoreGiftCertificates()
        Dim objCerts As CGiftCertificates = obj.GetAllGiftCertificates()
        Dim objgift As CGiftCertificate
        For Each objgift In objCerts.GiftCertificates
            If (objgift.Code = m_objGift.Code) And Not (objgift.ID = m_objGift.ID) Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("addgiftcertificates.ascx", "Error", "Duplicate")
                ErrorMessage.Visible = True
                obj = Nothing
                objCerts = Nothing
                objgift = Nothing
                Exit Sub
            End If
        Next
        obj.UpdateGiftCertificate(m_objGift)
        objCerts = Nothing
        objgift = Nothing

        Session("Gift") = Nothing
        RaiseEvent Save(Nothing, EventArgs.Empty)
    End Sub

End Class
