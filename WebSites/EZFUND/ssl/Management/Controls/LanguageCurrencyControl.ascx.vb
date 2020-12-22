Imports StoreFront.BusinessRule.Management
Public MustInherit Class LanguageCurrencyControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents Countries As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LCID As System.Web.UI.WebControls.DropDownList
    Protected WithEvents OANDAID As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents OandaActive As System.Web.UI.WebControls.CheckBox
    Private objLocalization As New CLocalization()
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (IsPostBack = False) Then
            OANDAID.Text = objLocalization.AdminInfo.OANDAID
            OandaActive.Checked = objLocalization.AdminInfo.OandaActive
            Call getCountryDD()
            Call getLCIDDD()
        End If
    End Sub

    Private Sub getCountryDD()
        Dim x As Integer
        Dim dt As DataTable

        dt = objLocalization.getAllCountriesDT
        Countries.DataSource = dt
        Countries.DataValueField = "ID"
        Countries.DataTextField = "Display"
        Countries.DataBind()

        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objLocalization.AdminInfo.OriginCountry Then
                Countries.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub getLCIDDD()
        Dim x As Integer
        Dim dt As DataTable
        dt = objLocalization.getLCIDDT()
        LCID.DataSource = dt
        LCID.DataValueField = "ID"
        LCID.DataTextField = "Display"
        LCID.DataBind()

        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objLocalization.AdminInfo.LCID Then
                LCID.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objLocalization.AdminInfo.OandaActive = OandaActive.Checked
        objLocalization.AdminInfo.OANDAID = OANDAID.Text
        objLocalization.AdminInfo.LCID = LCID.SelectedItem.Value
        objLocalization.AdminInfo.OriginCountry = Countries.SelectedItem.Value
        objLocalization.update()
    End Sub
End Class
