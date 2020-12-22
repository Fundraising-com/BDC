Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class adddiscount
    Inherits CWebControl
    Protected WithEvents chkActive As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDiscription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdoDollar As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdoPercent As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ddlApplyTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSelect As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents txtApplyToID As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents minAmt As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgSelect As System.Web.UI.WebControls.Image
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
    Event DiscountSelect As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Me.Visible = False) Then
            Exit Sub
        End If

        ErrorMessage = CType(Me.Parent.FindControl("ErrorMessage"), Label)

        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub


#Region "Function FillCoupon() As CDiscount"
    Private Function FillCoupon() As CDiscount
        Dim objDiscount As New CDiscount()

        objDiscount.Name = "Discount"
        objDiscount.Description = txtDiscription.Text

        If Me.rdoDollar.Checked Then
            objDiscount.DiscountType = SystemBase.DiscountTypes.Dollar
            objDiscount.DollarOff = 0 & txtAmount.Text
            objDiscount.PercentOff = 0
        Else
            objDiscount.DiscountType = SystemBase.DiscountTypes.Percent
            objDiscount.PercentOff = 0 & txtAmount.Text
            objDiscount.DollarOff = 0
        End If

        If DropDownList1.SelectedIndex = 0 Then
            objDiscount.Expires = "Never"
        Else
            objDiscount.Expires = txtDate.Text
        End If

        If chkActive.Checked = True Then
            objDiscount.IsActive = True
        Else
            objDiscount.IsActive = False
        End If

        If (ddlApplyTo.SelectedIndex = 0) Then
            objDiscount.AppliedTo = SystemBase.AppliedDiscounts.All
            objDiscount.AppliedToID = 0
        Else
            objDiscount.AppliedToID = CLng(txtApplyToID.Text)
            objDiscount.AppliedTo = ddlApplyTo.SelectedItem.Value
        End If
        objDiscount.MinAmountType = MinAmountTypes.MerchandiseTotal
        objDiscount.MinimumAmount = CDec(minAmt.Text)
        Return objDiscount
    End Function
#End Region

#Region "Sub ClearFields"
    Public Sub ClearFields()
        txtDiscription.Text = ""
        txtAmount.Text = ""
        rdoDollar.Checked = True
        ddlApplyTo.SelectedIndex = 0
        DropDownList1.SelectedIndex = 0
        txtDate.Text = ""
        chkActive.Checked = False
    End Sub
#End Region

#Region "Validate"
    Public Function validate() As Boolean
        If txtDiscription.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editdiscount", "Error", "BlankDiscription")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtAmount.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editdiscount", "Error", "BlankAmount")
            ErrorMessage.Visible = True
            Return False
        ElseIf (DropDownList1.SelectedIndex > 0) Then
            If (txtDate.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("editdiscount", "Error", "BlankDate")
                ErrorMessage.Visible = True
                Return False
            End If
        ElseIf (txtApplyToID.Text = "0" And ddlApplyTo.SelectedIndex > 0) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("adddiscount", "Error", "BlankApplyToID")
            ErrorMessage.Visible = True
            Return False
        End If

        Return True
    End Function
#End Region

#Region "Property ApplyToID"
    Public Property ApplyToID() As String
        Get
            Return txtApplyToID.Text
        End Get
        Set(ByVal Value As String)
            txtApplyToID.Text = Value
        End Set
    End Property
#End Region

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If validate() Then
            Dim objDiscount As CDiscount = FillCoupon()
            Dim obj As New CStoreDiscounts()

            obj.InsertDiscounts(objDiscount)

            RaiseEvent Save(Nothing, EventArgs.Empty)
        End If
    End Sub

    Public Sub SelectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        RaiseEvent DiscountSelect(ddlApplyTo.SelectedIndex, EventArgs.Empty)
    End Sub

    Private Sub SetErrorMessage(ByVal strCondition As String)
        ErrorMessage.Text = m_objMessages.GetXMLMessage("adddiscount", "Error", strCondition)
        ErrorMessage.Visible = True
    End Sub

    Private Sub ddlApplyTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlApplyTo.SelectedIndexChanged
        Dim ddl As DropDownList = sender

        If (ddl.SelectedIndex > 0) Then
            btnSelect.Visible = True
            imgSelect.Visible = True
        Else
            txtApplyToID.Text = "0"
            btnSelect.Visible = False
            imgSelect.Visible = False
        End If
    End Sub
End Class
