Imports StoreFront.BusinessRule

Public MustInherit Class editmanufacturer
    Inherits CWebControl
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected objManufacturer As CManufacturer
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
    Event Cancel As EventHandler
    Event editVisible As EventHandler
#Region "Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'txtName.Text = ""
        objManufacturer = New CManufacturer()
        ErrorMessage.Visible = False
        Message.Visible = false
    End Sub
#End Region

#Region "Sub FillForm(ByVal manuf As CManufacturer)"
    Public Sub FillForm(ByVal manuf As CManufacturer)
        txtIDHidden.Value = manuf.ManufacturerAddress.ID
        txtName.Text = manuf.ManufacturerAddress.Company
    End Sub
#End Region

#Region "Sub FillManufacturer()"
    Public Sub FillManufacturer()
        objManufacturer.ManufacturerAddress.Company = txtName.Text
        objManufacturer.ManufacturerAddress.ID = txtIDHidden.Value
    End Sub
#End Region

    Public Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        FillManufacturer()
        If Validate() Then
            Try
                objManufacturer.UpdateManufacturerName(objManufacturer.ManufacturerAddress)
                Message.Text = m_objmessages.GetXMLMessage("editmanufacturer.aspx", "edit", "Success")
                Message.Visible = True
                'RaiseEvent Save(objManufacturer, EventArgs.Empty)
            Catch
                ErrorMessage.Text = m_objmessages.GetXMLMessage("editmanufacturer.aspx", "edit", "UpdateError")
                ErrorMessage.Visible = True
            End Try
        End If
        RaiseEvent editVisible(sender, EventArgs.Empty)
    End Sub

    Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        RaiseEvent Cancel(txtIDHidden, EventArgs.Empty)
    End Sub

    Public Function Validate() As Boolean
        If Me.txtName.Text = "" Then
            ErrorMessage.Text = m_objmessages.GetXMLMessage("editmanufacturer.aspx", "edit", "BlankName")
            ErrorMessage.Visible = True
            Return False
        Else
            Return True
        End If
    End Function

End Class
