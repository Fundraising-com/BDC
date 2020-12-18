Imports storefront.systembase
Imports StoreFront.BusinessRule

Partial  Class addvendorcontrol
    Inherits CWebControl
    Protected WithEvents Row1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row5 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row6 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row7 As System.Web.UI.HtmlControls.HtmlTableRow

    Protected strMessage As String
    Protected objVendor As CVendor

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
    Event addVisible As EventHandler

#Region "Properties"
   
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            Row1.Visible = False
            Row2.Visible = False
            Row3.Visible = False
            Row4.Visible = False
            Row5.Visible = False
            Row6.Visible = False
            Row7.Visible = False
        End If
        'Put user code to initialize the page here
        objVendor = New CVendor()
        Message.Visible = False
        ErrorMessage.Visible = False

    End Sub

#Region "Sub FillVendor()"
    Public Sub FillVendor()
        objVendor.VendorAddress.City = txtCity.Text
        objVendor.VendorAddress.State = selState.SelectedItem.Value
        objVendor.VendorAddress.Country = selCountry.SelectedItem.Value
        objVendor.VendorAddress.Company = txtVendorName.Text
        objVendor.VendorAddress.EMail = txtEMail.Text
        objVendor.VendorAddress.Zip = txtZip.Text
    End Sub
#End Region

#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtVendorName.Text = ""
        txtEMail.Text = ""
        txtCity.Text = ""
        selState.SelectedIndex = 0
        selCountry.SelectedIndex = 0
        txtZip.Text = ""
    End Sub
#End Region

#Region "Sub ValidateFields()"
    Public Function ValidateFields() As Boolean
        If txtVendorName.Text = "" Then
            ErrorMessage.Text = m_objmessages.GetXMLMessage("addvendor.aspx", "add", "BlankName")
            ErrorMessage.Visible = True
            Return False
        Else
            If (Row1.Visible = True) Then
                If txtCity.Text = "" Then
                    ErrorMessage.Text = m_objmessages.GetXMLMessage("addvendor.aspx", "add", "BlankCity")
                    ErrorMessage.Visible = True
                    Return False
                ElseIf txtEMail.Text = "" Then
                    ErrorMessage.Text = m_objmessages.GetXMLMessage("addvendor.aspx", "add", "BlankEmail")
                    ErrorMessage.Visible = True
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End If
    End Function
#End Region

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        If ValidateFields() Then
            FillVendor()
            Try
                objVendor.AddVendor()
                Message.Text = m_objmessages.GetXMLMessage("addvendor.aspx", "add", "Success")
                Message.Visible = True
            Catch
                ErrorMessage.Text = m_objmessages.GetXMLMessage("addvendor.aspx", "add", "UpdateError")
                ErrorMessage.Visible = True
            End Try
        End If
        RaiseEvent Save(objVendor, e)
        RaiseEvent addVisible(objVendor, e)

    End Sub

End Class
