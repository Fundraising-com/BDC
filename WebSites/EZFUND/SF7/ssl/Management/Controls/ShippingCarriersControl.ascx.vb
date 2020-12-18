Imports StoreFront.BusinessRule.management
Partial  Class ShippingCarriersControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents FedExPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents tblImport As System.Web.UI.HtmlControls.HtmlTable
    Private objMessage As Label
    Private objShipping As New CShippingManagement()
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

        objMessage = CType(Me.Parent.FindControl("Message"), Label)
        objMessage.Text = ""
        objMessage.Visible = False
        If (IsPostBack = True) Then
        Else
            Call GetBackupTypes()
            Call GetStates()
            Call setControls()
        End If
    End Sub


    Public Sub setControls()
        Dim x As Integer
        Dim dr As DataRow

        For x = 0 To OriginState.Items.Count - 1
            If OriginState.Items(x).Value = objShipping.AdminShipping.OriginState Then
                OriginState.SelectedIndex = x
                Exit For
            End If
        Next
        If (objShipping.AdminShipping.BackupShippingType = 3) Then
            BackupShipping.SelectedIndex = 0
        Else
            BackupShipping.SelectedIndex = 1
        End If
        OriginZip.Text = objShipping.AdminShipping.OriginZip
        OriginCity.Text = objShipping.AdminShipping.OriginCity
        UPSPanel.Visible = False
        USPSPanel.Visible = False
        FEPanel.Visible = False
        CPPanel.Visible = False
        LTLPanel.Visible = False
        For Each dr In objShipping.getCarriers.Tables(0).Rows
            Select Case dr.Item("Code").ToString
                Case "UPS"
                    UPSPanel.Visible = True
                    If dr.Item("UserName").ToString.Trim = "" Or dr.Item("Pass").ToString.Trim = "" Or dr.Item("AccessCode").ToString.Trim = "" Then
                        UPSRegistered.Visible = False
                        UPSNotRegistered.Visible = True
                    Else
                        UPSRegistered.Visible = True
                        UPSNotRegistered.Visible = False
                    End If
                    If dr.Item("Active").ToString = "1" Then
                        UPSActive.Checked = True
                    Else
                        UPSActive.Checked = False
                    End If
                Case "USPS"
                    USPSPanel.Visible = True
                    If dr.Item("Active").ToString = "1" Then
                        USPSActive.Checked = True
                    Else
                        USPSActive.Checked = False
                    End If
                    USPSUserName.Text = dr.Item("UserName").ToString
                    '#2844
                    'USPSPassword.Text = dr.Item("Pass").ToString
                Case "FEDEX"
                    FEPanel.Visible = True
                    If dr.Item("Active").ToString = "1" Then
                        FEActive.Checked = True
                    Else
                        FEActive.Checked = False
                    End If
                    FedExUserName.Text = dr.Item("UserName").ToString

                Case "CP"
                    CPPanel.Visible = True
                    If dr.Item("Active").ToString = "1" Then
                        CPActive.Checked = True
                    Else
                        CPActive.Checked = False
                    End If
                    CPUserName.Text = dr.Item("UserName").ToString
                Case "LTL"
                    LTLPanel.Visible = True
                    If dr.Item("Active").ToString = "1" Then
                        LTLActive.Checked = True
                    Else
                        LTLActive.Checked = False
                    End If
                    LTLUserName.Text = dr.Item("UserName").ToString
                    LTLPassword.Text = dr.Item("Pass").ToString
            End Select
        Next
    End Sub

    Public Sub GetBackupTypes()
        BackupShipping.DataSource = objShipping.getBackupShipDT
        BackupShipping.DataValueField = "ID"
        BackupShipping.DataTextField = "Display"
        BackupShipping.DataBind()
    End Sub

    Public Sub GetStates()
        OriginState.DataSource = objShipping.getStatesDT
        OriginState.DataValueField = "Abbreviation"
        OriginState.DataTextField = "Name"
        OriginState.DataBind()
    End Sub

    Private Sub UPSLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UPSLink.Click
        Response.Redirect("upsregistration.aspx")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ds As dataset
        Dim dr As datarow
        ds = objShipping.getCarriers
        For Each dr In ds.Tables(0).Rows
            Select Case dr.Item("Code").ToString
                Case "UPS"
                    If UPSActive.Checked = True Then
                        dr.Item("Active") = "1"
                    Else
                        dr.Item("Active") = "0"
                    End If
                Case "USPS"
                    If USPSActive.Checked = True Then
                        dr.Item("Active") = "1"
                    Else
                        dr.Item("Active") = "0"
                    End If
                    If USPSUserName.Text.Trim <> "" Then
                        dr.Item("UserName") = USPSUserName.Text
                    Else
                        dr.Item("UserName") = Nothing
                    End If
                    '#2844
                    'If USPSPassword.Text.Trim <> "" Then
                    '    dr.Item("Pass") = USPSPassword.Text
                    'Else
                    dr.Item("Pass") = String.Empty
                    'End If

                Case "FEDEX"
                    If FEActive.Checked = True Then
                        dr.Item("Active") = "1"
                    Else
                        dr.Item("Active") = "0"
                    End If

                    If FedExUserName.Text.Trim <> "" Then
                        If FedExUserName.Text <> dr.Item("UserName").ToString Then
                            dr.Item("UserName") = FedExUserName.Text
                            dr.Item("AccessCode") = Nothing
                        End If
                    Else
                        dr.Item("UserName") = Nothing
                        dr.Item("AccessCode") = Nothing
                    End If


                Case "CP"
                    If CPActive.Checked = True Then
                        dr.Item("Active") = "1"
                    Else
                        dr.Item("Active") = "0"
                    End If

                    If CPUserName.Text.Trim <> "" Then
                        dr.Item("UserName") = CPUserName.Text
                    Else
                        dr.Item("UserName") = Nothing
                    End If

                Case "LTL"
                    If LTLActive.Checked = True Then
                        dr.Item("Active") = "1"
                    Else
                        dr.Item("Active") = "0"
                    End If

                    If LTLUserName.Text.Trim <> "" Then
                        dr.Item("UserName") = LTLUserName.Text
                    Else
                        dr.Item("UserName") = Nothing
                    End If

                    If LTLPassword.Text.Trim <> "" Then
                        dr.Item("Pass") = LTLPassword.Text
                    Else
                        dr.Item("Pass") = Nothing
                    End If
            End Select
        Next

        objShipping.AdminShipping.BackupShippingType = BackupShipping.SelectedItem.Value
        objShipping.AdminShipping.OriginState = OriginState.SelectedItem.Value
        objShipping.AdminShipping.OriginCity = OriginCity.Text
        objShipping.AdminShipping.OriginZip = OriginZip.Text

        objShipping.update(ds)
        objMessage.Text = "Your changes have been saved"
        objMessage.Visible = True
    End Sub
End Class
