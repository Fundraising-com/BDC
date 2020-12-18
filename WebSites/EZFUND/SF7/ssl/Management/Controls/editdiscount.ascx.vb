Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Partial  Class editdiscount
    Inherits CWebControl
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

    Public Property EditDiscount() As CDiscount
        Get
            Return FillCoupon()
        End Get
        Set(ByVal Value As CDiscount)
            Dim objDiscount As CDiscount = Value

            txtID.Text = objDiscount.ID
            chkActive.Checked = objDiscount.IsActive
            txtDiscription.Text = objDiscount.Description
            minAmt.Text = objDiscount.MinimumAmount.ToString
            If (objDiscount.DiscountType = DiscountTypes.Dollar) Then
                txtAmount.Text = objDiscount.DollarOff
                rdoDollar.Checked = True
            Else
                txtAmount.Text = objDiscount.PercentOff
                rdoPercent.Checked = True
            End If

            If (objDiscount.AppliedTo = AppliedDiscounts.All) Then
                ddlApplyTo.SelectedIndex = 0
            ElseIf (objDiscount.AppliedTo = AppliedDiscounts.Category) Then
                ddlApplyTo.SelectedIndex = 1
            ElseIf (objDiscount.AppliedTo = AppliedDiscounts.Manufacturer) Then
                ddlApplyTo.SelectedIndex = 2
            ElseIf (objDiscount.AppliedTo = AppliedDiscounts.Vendor) Then
                ddlApplyTo.SelectedIndex = 3
            End If

            If (objDiscount.AppliedTo <> SystemBase.AppliedDiscounts.All) Then
                btnSelect.Visible = True
                imgSelect.Visible = True
            End If
            '1205 '2412
            '   Dropdownlist2.SelectedItem.Value = Convert.ToUInt32(objDiscount.ApplyOnce).ToString
            Dropdownlist2.SelectedIndex = Convert.ToInt32(objDiscount.ApplyOnce)
            '1205 '2412
            txtApplyToID.Text = objDiscount.AppliedToID

            If (objDiscount.Expires = "Never") Then
                DropDownList1.SelectedIndex = 0
                txtDate.Text = ""
            Else
                DropDownList1.SelectedIndex = 1
                txtDate.Text = objDiscount.Expires
            End If

        End Set
    End Property

    'Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    If validate() Then
    '        Dim objDiscount As CDiscount = FillCoupon()
    '        Dim obj As New CStoreDiscounts()

    '        obj.UpdateDiscounts(objDiscount)
    '        RaiseEvent Save(Nothing, EventArgs.Empty)
    '    End If
    'End Sub

    'Private Sub SelectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
    '    RaiseEvent DiscountSelect(ddlApplyTo.SelectedIndex, EventArgs.Empty)
    'End Sub

    Private Sub SetErrorMessage(ByVal strCondition As String)
        ErrorMessage.Text = m_objMessages.GetXMLMessage("editdiscount", "Error", strCondition)
        ErrorMessage.Visible = True
    End Sub

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

#Region "Function FillCoupon() As CDiscount"
    Private Function FillCoupon() As CDiscount
        Dim objDiscount As New CDiscount()

        objDiscount.ID = txtID.Text
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
        '1205 '2412
        'objDiscount.ApplyOnce = CBool(Dropdownlist2.SelectedItem.Value)
        objDiscount.ApplyOnce = CBool(Dropdownlist2.SelectedIndex)
        '1205 '2412
        Return objDiscount
    End Function
#End Region

#Region "Validate"
    Public Function validate() As Boolean
        If txtDiscription.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankDiscription")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtAmount.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankAmount")
            ErrorMessage.Visible = True
            Return False
        ElseIf (DropDownList1.SelectedIndex > 0) Then
            If (txtDate.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankDate")
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

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        RaiseEvent DiscountSelect(ddlApplyTo.SelectedIndex, EventArgs.Empty)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If validate() Then
            Dim objDiscount As CDiscount = FillCoupon()
            Dim obj As New CStoreDiscounts()

            obj.UpdateDiscounts(objDiscount)
            RaiseEvent Save(Nothing, EventArgs.Empty)
        End If
    End Sub
End Class
