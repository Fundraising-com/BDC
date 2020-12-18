Imports StoreFront.BusinessRule.Management
Imports StoreFront.Systembase
Imports system.text

Public MustInherit Class OnlineChatControl
    Inherits System.Web.UI.UserControl

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
    Protected WithEvents LivePersonID As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tblTrial As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Private objAdminManagement As New CAdminGeneralManagement()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        lblErrorMessage = Parent.FindControl("ErrorMessage")
        lblErrorMessage.Text = ""
        lblErrorMessage.Visible = False
        If IsPostBack = False Then
            LivePersonID.Text = objAdminManagement.LivePersonID
        End If
        If (LivePersonID.Text.Trim.Length > 0) Then
            tblTrial.Visible = False
        Else
            tblTrial.Visible = True
        End If
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            objAdminManagement.LivePersonID = LivePersonID.Text
            objAdminManagement.update()


    End Sub

End Class
