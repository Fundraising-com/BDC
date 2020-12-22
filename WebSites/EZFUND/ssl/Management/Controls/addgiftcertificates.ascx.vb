Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Public MustInherit Class addgiftcertificates
    Inherits CWebControl
    Protected WithEvents txtGiftCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton

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

    Event Save As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'btnSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSave").Attributes("Filepath").Value
        If (IsPostBack = False) Then
            txtGiftCode.Text = ""
            txtAmount.Text = ""
            txtDate.Text = ""
            DropDownList1.SelectedIndex = 0
        End If
    End Sub

    Public Sub ClearFields()
        txtGiftCode.Text = ""
        txtAmount.Text = ""
        txtDate.Text = ""
        DropDownList1.SelectedIndex = 0
    End Sub

    Public Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (txtAmount.Text = "" Or txtGiftCode.Text = "") Then
            If (txtAmount.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("addgiftcertificates", "Error", "BlankAmount")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("addgiftcertificates", "Error", "BlankCode")
            End If
            ErrorMessage.Visible = True
            Exit Sub
        ElseIf (DropDownList1.SelectedIndex > 0) Then
            If (txtDate.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("addgiftcertificates", "Error", "BlankDate")
                ErrorMessage.Visible = True
                Exit Sub
            End If
        End If

        Dim m_objGift As New CGiftCertificate()
        m_objGift.DollarOff = CDec(txtAmount.Text)
        m_objGift.Code = txtGiftCode.Text

        If (DropDownList1.SelectedIndex = 0) Then
            m_objGift.Expires = "Never"
        Else
            m_objGift.Expires = txtDate.Text
        End If

        Dim obj As New CStoreGiftCertificates()
        Dim objCerts As CGiftCertificates = obj.GetAllGiftCertificates()
        Dim objgift As CGiftCertificate
        For Each objgift In objCerts.GiftCertificates
            If objgift.Code = m_objGift.Code Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("addgiftcertificates.ascx", "Error", "Duplicate")
                ErrorMessage.Visible = True
                obj = Nothing
                objCerts = Nothing
                objgift = Nothing
                Exit Sub
            End If
        Next
        obj.InsertGiftCertificate(m_objGift)
        objCerts = Nothing
        objgift = Nothing
        RaiseEvent Save(Nothing, EventArgs.Empty)
    End Sub


End Class
