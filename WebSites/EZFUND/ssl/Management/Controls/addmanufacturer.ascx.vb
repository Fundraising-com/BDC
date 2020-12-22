Imports StoreFront.BusinessRule

Public MustInherit Class addmanufacturer
    Inherits CWebControl
    Protected WithEvents lblCustomerHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtIDHidden As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
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
    Event Cancel As EventHandler
    Event Save As EventHandler
    Event AddVisible As EventHandler
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objManufacturer = New CManufacturer()
        ErrorMessage.Visible = False
        Message.Visible = False
    End Sub

#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtName.Text = ""
        Me.txtIDHidden.Value = ""
    End Sub
#End Region

#Region "Sub FillManufacturer()"
    Public Sub FillManufacturer()
        objManufacturer.ManufacturerAddress.Company = Me.txtName.Text
    End Sub
#End Region

#Region "Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdAdd.Click"
    Public Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If Validate() Then
            FillManufacturer()
            objManufacturer.AddManufacturer()
            'RaiseEvent Save(sender, EventArgs.Empty)
            Message.Text = m_objmessages.GetXMLMessage("addmanufacturer.aspx", "add", "Success")
            Message.Visible = True
        End If
        RaiseEvent AddVisible(sender, EventArgs.Empty)



    End Sub
#End Region

#Region "Function Validate() As Boolean"
    Public Function Validate() As Boolean
        If Me.txtName.Text = "" Then
            ErrorMessage.Text = m_objmessages.GetXMLMessage("editmanufacturer.aspx", "edit", "BlankName")
            ErrorMessage.Visible = True
        Else
            Return True
        End If
    End Function
#End Region

#Region "Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdCancel.Click"

    Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        RaiseEvent Cancel(sender, EventArgs.Empty)
    End Sub


#End Region

End Class
