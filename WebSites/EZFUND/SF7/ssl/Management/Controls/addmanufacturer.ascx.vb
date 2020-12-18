Imports StoreFront.BusinessRule

Partial  Class addmanufacturer
    Inherits CWebControl
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
        'Tee 7/13/2007 clear chkbox
        cbActivate.Checked = False
        'end Tee
    End Sub
#End Region

#Region "Sub FillManufacturer()"
    Public Sub FillManufacturer()
        objManufacturer.ManufacturerAddress.Company = Me.txtName.Text
        'Tee 7/13/2007 transfer active value
        objManufacturer.Active = cbActivate.Checked
        'end Tee
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
