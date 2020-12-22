Imports StoreFront.BusinessRule.management
Imports StoreFront.SystemBase
Public MustInherit Class PaymentMethodsControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents AcceptCC As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CCProcMethod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CCDeleteSchedule As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequireSecurity As System.Web.UI.WebControls.CheckBox
    Protected WithEvents AcceptEcheck As System.Web.UI.WebControls.CheckBox
    Protected WithEvents EcheckProcMethod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents AcceptCOD As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CODAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents AcceptPO As System.Web.UI.WebControls.CheckBox
    Protected WithEvents AcceptMailFax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents MailFaxType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents AcceptPayPal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents tblPaymentMethods As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents AcceptedCCs As System.Web.UI.WebControls.Repeater
    Protected WithEvents NewCCName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Private objError As Label

    Protected WithEvents PayPalID As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkAllowBO As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents bo2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bo3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bo0 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bo1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents bo4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trCCs As System.Web.UI.HtmlControls.HtmlTableRow
    Private objPaymentManagement As CPaymentManagement
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

        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            bo.Visible = False
            bo0.Visible = False
            bo1.Visible = False
            bo2.Visible = False
            bo3.Visible = False
            bo4.Visible = False
        End If


        If IsPostBack = False Then
            objPaymentManagement = New CPaymentManagement()
            AcceptCC.Checked = objPaymentManagement.AcceptCC
            ' If AcceptCC.Checked Then
            trCCs.Visible = AcceptCC.Checked
            'End If
            RequireSecurity.Checked = objPaymentManagement.AdminInfo.RequireSecurityCode
            AcceptEcheck.Checked = objPaymentManagement.AcceptEcheck
            AcceptCOD.Checked = objPaymentManagement.AcceptCOD
            AcceptPO.Checked = objPaymentManagement.AcceptPO
            AcceptPayPal.Checked = objPaymentManagement.AcceptPayPal
            CODAmount.Text = objPaymentManagement.AdminInfo.CODAmount
            PayPalID.Text = objPaymentManagement.PayPalID
            chkAllowBO.Checked = objPaymentManagement.AdminInfo.BackOrderBilling
            AcceptMailFax.Checked = objPaymentManagement.AcceptMailFax

            AcceptedCCs.DataSource = objPaymentManagement.getCreditCardsDT(True)
            AcceptedCCs.DataBind()

            CCProcMethod.DataSource = objPaymentManagement.getProcMethodDT
            CCProcMethod.DataValueField = "ID"
            CCProcMethod.DataTextField = "Display"
            CCProcMethod.DataBind()
            If objPaymentManagement.AdminInfo.CCProcessingMethod = 1 Then
                CCProcMethod.SelectedIndex = 0
            Else
                CCProcMethod.SelectedIndex = 1
            End If

            CCDeleteSchedule.DataSource = objPaymentManagement.getCCDeleteScheduleDT
            CCDeleteSchedule.DataValueField = "ID"
            CCDeleteSchedule.DataTextField = "Display"
            CCDeleteSchedule.DataBind()
            Select Case objPaymentManagement.AdminInfo.CCDeletionSchedule
                Case 0
                    CCDeleteSchedule.SelectedIndex = 0
                Case 1
                    CCDeleteSchedule.SelectedIndex = 1
                Case 7
                    CCDeleteSchedule.SelectedIndex = 2
                Case 30
                    CCDeleteSchedule.SelectedIndex = 3
                Case 90
                    CCDeleteSchedule.SelectedIndex = 4
            End Select

            EcheckProcMethod.DataSource = objPaymentManagement.getProcMethodDT
            EcheckProcMethod.DataValueField = "ID"
            EcheckProcMethod.DataTextField = "Display"
            EcheckProcMethod.DataBind()
            If objPaymentManagement.AdminInfo.EcheckProcessingMethod = 1 Then
                EcheckProcMethod.SelectedIndex = 0
            Else
                EcheckProcMethod.SelectedIndex = 1
            End If

            MailFaxType.DataSource = objPaymentManagement.getMailFaxTypesDT
            MailFaxType.DataValueField = "ID"
            MailFaxType.DataTextField = "Display"
            MailFaxType.DataBind()
            If objPaymentManagement.MailFaxType = "Recorded" Then
                MailFaxType.SelectedIndex = 0
            Else
                MailFaxType.SelectedIndex = 1
            End If
            NewCCName.Text = ""
        End If
        ErrorMessage.Visible = False

    End Sub


#Region "Sub deleteRow(ByVal sender As Object, ByVal e As EventArgs)"
    '-----------------------------------------------------------
    ' Sub deleteRow
    ' Parameters: Object, EventArgs
    ' Return: Nothing
    ' Description:
    '   Raise the deleteRow Event passing the Given value for ButtonID
    '-----------------------------------------------------------
    '-----------------------------------------------------------
    Public Sub deleteRow(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable()
        dt = getDTFromPage()
        dt.Rows(CInt(sender.CommandName)).Delete()
        NewCCName.Text = ""

        AcceptedCCs.DataSource = dt
        AcceptedCCs.DataBind()
        If dt.Rows.Count = 0 Then
            AcceptCC.Checked = False
        End If
    End Sub


#End Region



    Public Function getDTFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim x As Integer
        Dim sName As String
        dt.Columns.Add(New DataColumn("Name", GetType(String)))
        dt.Columns.Add(New DataColumn("IsActive", GetType(Integer)))
        For x = 0 To AcceptedCCs.Items.Count - 1
            sName = CType(AcceptedCCs.Items(x).FindControl("Name"), TextBox).Text
            dr = dt.NewRow()
            dr("Name") = sName
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        If NewCCName.Text.Trim <> "" Then
            Dim dt As New DataTable()
            Dim dr As DataRow
            Dim x As Integer
            Dim pos As Integer
            Dim sNewName As String
            Dim sName As String
            Dim bError As Boolean
            bError = False

            dt = getDTFromPage()
            sNewName = NewCCName.Text

            objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
            objError.Visible = False
            For x = 0 To AcceptedCCs.Items.Count - 1
                sName = CType(AcceptedCCs.Items(x).FindControl("Name"), TextBox).Text
                If (sNewName = sName) Then
                    objError.Text = "Row already exists."
                    objError.Visible = True
                    Exit For
                End If
            Next

            If objError.Visible = False Then
                dr = dt.NewRow
                dr("Name") = sNewName
                dt.Rows.Add(dr)
            End If
            AcceptedCCs.DataSource = dt
            AcceptedCCs.DataBind()
            NewCCName.Text = ""
        Else
            ErrorMessage.Text = "Please enter a credit card number"
            ErrorMessage.Visible = True
        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim dt As New DataTable()
        objPaymentManagement = New CPaymentManagement()
        dt = getDTFromPage()
        'If dt.Rows.Count = 0 Then
        ' 'A() 'cceptCC.Checked = False
        'End If
        objPaymentManagement.AcceptCC = AcceptCC.Checked
       
        objPaymentManagement.AdminInfo.RequireSecurityCode = RequireSecurity.Checked
        objPaymentManagement.AcceptEcheck = AcceptEcheck.Checked
        objPaymentManagement.AcceptCOD = AcceptCOD.Checked
        objPaymentManagement.AcceptPO = AcceptPO.Checked
        objPaymentManagement.AcceptPayPal = AcceptPayPal.Checked
        objPaymentManagement.AdminInfo.CODAmount = CODAmount.Text
        objPaymentManagement.PayPalID = PayPalID.Text
        objPaymentManagement.AdminInfo.CCProcessingMethod = CCProcMethod.SelectedItem.Value
        objPaymentManagement.AdminInfo.CCDeletionSchedule = CCDeleteSchedule.SelectedItem.Value
        objPaymentManagement.AdminInfo.EcheckProcessingMethod = EcheckProcMethod.SelectedItem.Value
        objPaymentManagement.MailFaxType = MailFaxType.SelectedItem.Value
        objPaymentManagement.AdminInfo.BackOrderBilling = chkAllowBO.Checked
        objPaymentManagement.AcceptMailFax = Me.AcceptMailFax.Checked
        objPaymentManagement.update(dt)
        If AcceptCC.Checked Then
            trCCs.Visible = True
            AcceptedCCs.DataSource = objPaymentManagement.getCreditCardsDT(True)
            AcceptedCCs.DataBind()
        End If
        trCCs.Visible = AcceptCC.Checked
        NewCCName.Text = ""
    End Sub
End Class
