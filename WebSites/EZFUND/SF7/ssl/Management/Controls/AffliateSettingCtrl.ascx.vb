Imports StoreFront.Systembase
Imports StoreFront.BusinessRule


Partial  Class AffliateSettingCtrl
    Inherits CWebControl

#Region "Class Members"

    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Private _AffiliateManager As New CAffilateManagment()

#End Region    

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

	 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            loadMe()
        End If
        Me.ErrorMessage.Visible = False

        Session("Manager") = _AffiliateManager
        'cmdSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Save").Attributes("Filepath").Value
    End Sub

#End Region    

#Region "Private Sub loadMe()"
    '##SUMMARY  LOADS Interface
    '##SUMMARY   

    Private Sub loadMe()
        Dim i As Integer
        With Me
            .txtFlatFee.Text = _AffiliateManager.Settings.FlatFee
            .txtMinimum.Text = _AffiliateManager.Settings.MinumimPayOut
            .txtPercent.Text = _AffiliateManager.Settings.PayOut
            .txtTerms.Text = _AffiliateManager.Settings.Terms
            For i = 0 To .ddPayOutType.Items.Count - 1
                If .ddPayOutType.Items(i).Value = _AffiliateManager.Settings.PayOutRule Then
                    .ddPayOutType.SelectedIndex = i
                End If
            Next
        End With
    End Sub


#End Region

#Region "Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdSave.Click"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        _AffiliateManager = Session("Manager")
        'Dim i As Integer
        If IsNothing(_AffiliateManager) = False Then
            With Me
                _AffiliateManager.Settings.FlatFee = FormatNumber(.txtFlatFee.Text, 2)
                _AffiliateManager.Settings.MinumimPayOut = FormatNumber(.txtMinimum.Text, 2)
                _AffiliateManager.Settings.PayOut = CDec(.txtPercent.Text)
                'update #1558
                'If .txtTerms.Text.Length < 256 Then
                _AffiliateManager.Settings.Terms = .txtTerms.Text
                'Else
                '_AffiliateManager.Settings.Terms = Left(.txtTerms.Text, 255)

                'End If

                _AffiliateManager.Settings.PayOutRule = .ddPayOutType.SelectedItem.Value
                _AffiliateManager.UpdateSettings()
                Me.ErrorMessage.Visible = True
                Me.ErrorMessage.Text = "Settings Saved"
            End With
        End If
    End Sub

#End Region

End Class
