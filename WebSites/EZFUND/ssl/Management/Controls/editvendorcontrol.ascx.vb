Imports storefront.systembase
Imports StoreFront.BusinessRule

Public MustInherit Class editvendorcontrol
    Inherits CWebControl
    Protected WithEvents Row1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row5 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row6 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row7 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtVendorName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents selState As UITools.SelectValControl
    Protected WithEvents selCountry As UITools.SelectValControl
    Protected WithEvents txtEMail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtZip As System.Web.UI.WebControls.TextBox
    Protected objVendor As CVendor
    Protected WithEvents txtAddressIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected strMessage As String
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
    Event editVisible As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            Row1.Visible = False
            Row2.Visible = False
            Row3.Visible = False
            Row4.Visible = False
            Row5.Visible = False
            Row6.Visible = False
            Row7.Visible = False
        End If
        objVendor = New CVendor()
    End Sub

#Region "Sub FillForm(ByVal vend As CVendor)"
    Public Sub FillForm(ByVal vend As CVendor)
        Me.txtAddressIDHidden.Value = vend.VendorAddress.ID
        txtVendorName.Text = vend.VendorAddress.Company
        txtCity.Text = vend.VendorAddress.City
        txtEMail.Text = vend.VendorAddress.EMail
        txtZip.Text = vend.VendorAddress.Zip
        Dim item As ListItem
        Dim i As Integer = 0
        For Each item In selCountry.Items
            If item.Value = vend.VendorAddress.Country Then
                selCountry.SelectedIndex = i
            End If
            i = i + 1
        Next
        i = 0
        For Each item In selState.Items
            If item.Value = vend.VendorAddress.State Then
                selState.SelectedIndex = i
            End If
            i = i + 1
        Next
    End Sub
#End Region

#Region "FillVendor()"
    Public Sub FillVendor()
        objVendor.VendorAddress.City = txtCity.Text
        objVendor.VendorAddress.State = selState.SelectedItem.Value
        objVendor.VendorAddress.Country = selCountry.SelectedItem.Value
        objVendor.VendorAddress.Company = txtVendorName.Text
        objVendor.VendorAddress.EMail = txtEMail.Text
        objVendor.VendorAddress.Zip = txtZip.Text
        objVendor.VendorAddress.ID = txtAddressIDHidden.Value
    End Sub
#End Region

#Region "Sub ValidateFields()"
    Public Function ValidateFields() As Boolean
        If txtVendorName.Text = "" Then
            ErrorMessage.Text = m_objmessages.GetXMLMessage("editvendor.aspx", "edit", "BlankName")
            ErrorMessage.Visible = True
            Return False
        Else
            If (Row1.Visible = True) Then
                If txtCity.Text = "" Then
                    ErrorMessage.Text = m_objmessages.GetXMLMessage("editvendor.aspx", "add", "BlankCity")
                    ErrorMessage.Visible = True
                    Return False
                ElseIf txtEMail.Text = "" Then
                    ErrorMessage.Text = m_objmessages.GetXMLMessage("editvendor.aspx", "add", "BlankEmail")
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

    Public Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If ValidateFields() Then
            FillVendor()
            Try
                objVendor.UpdateVendor()
                Message.Text = m_objmessages.GetXMLMessage("editvendor.aspx", "edit", "Success")
                Message.Visible = True
            Catch
                ErrorMessage.Text = m_objmessages.GetXMLMessage("editvendor.aspx", "edit", "UpdateError")
                ErrorMessage.Visible = True
            End Try
        End If
        'RaiseEvent Save(objVendor, EventArgs.Empty)
        RaiseEvent editVisible(objVendor, EventArgs.Empty)
    End Sub

End Class
