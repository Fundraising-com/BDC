Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Partial  Class editcoupon
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
    Event CouponSelect As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Me.Visible = False) Then
            Exit Sub
        End If

        ErrorMessage = CType(Me.Parent.FindControl("ErrorMessage"), Label)

        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub

#Region "Property EditCoupon() As CDiscount"
    Public Property EditCoupon() As CDiscount
        Get
            Return FillCoupon()
        End Get
        Set(ByVal Value As CDiscount)
            Dim objCoupon As CDiscount = Value

            txtID.Text = objCoupon.ID
            chkActive.Checked = objCoupon.IsActive
            txtDiscription.Text = objCoupon.Description
            txtCode.Text = objCoupon.Code
            minAmt.Text = objCoupon.MinimumAmount.ToString
            If (objCoupon.DiscountType = DiscountTypes.Dollar) Then
                txtAmount.Text = objCoupon.DollarOff
                rdoDollar.Checked = True
            Else
                txtAmount.Text = objCoupon.PercentOff
                rdoPercent.Checked = True
            End If

            If (objCoupon.AppliedTo = AppliedDiscounts.All) Then
                ddlApplyTo.SelectedIndex = 0
            ElseIf (objCoupon.AppliedTo = AppliedDiscounts.Product) Then
                ddlApplyTo.SelectedIndex = 1
            ElseIf (objCoupon.AppliedTo = AppliedDiscounts.Category) Then
                ddlApplyTo.SelectedIndex = 2
            ElseIf (objCoupon.AppliedTo = AppliedDiscounts.Manufacturer) Then
                ddlApplyTo.SelectedIndex = 3
            ElseIf (objCoupon.AppliedTo = AppliedDiscounts.Vendor) Then
                ddlApplyTo.SelectedIndex = 4
            End If

            If (objCoupon.AppliedTo <> SystemBase.AppliedDiscounts.All) Then
                btnSelect.Visible = True
                imgSelect.Visible = True
            End If
            ' ddlApplyOnce.SelectedItem.Value = Convert.ToUInt32(objCoupon.ApplyOnce).ToString
            'AB Code
            ddlApplyOnce.SelectedIndex = Convert.ToInt32(objCoupon.ApplyOnce)
            'End AB Code
            txtApplyToID.Text = objCoupon.AppliedToID

            If (objCoupon.Expires.ToLower = "never" Or objCoupon.Expires.ToLower = "onetime") Then
                DropDownList1.SelectedIndex = 0
                txtDate.Text = ""
                If (objCoupon.Expires.ToLower = "onetime") Then
                    chkOneTime.Checked = True
                Else
                    chkOneTime.Checked = False
                End If
            Else
                DropDownList1.SelectedIndex = 1
                txtDate.Text = objCoupon.Expires
            End If
        End Set
    End Property
#End Region

#Region "Sub SaveClick"
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If validate() Then
            Dim objCoupon As CDiscount = FillCoupon()
            Dim obj As New CStoreDiscounts()

            Dim objList As CDiscounts = obj.GetAllDiscounts
            Dim objItem As CDiscount
            For Each objItem In objList.CouponItems
                If (objItem.ID <> objCoupon.ID And _
                    objItem.Code = objCoupon.Code) Then
                    ErrorMessage.Text = "Error: Coupon already exists."
                    ErrorMessage.Visible = True
                    Return
                End If
            Next

            obj.UpdateDiscounts(objCoupon)
            RaiseEvent Save(Nothing, EventArgs.Empty)
        End If
    End Sub
#End Region

#Region "Sub SelectClick"
    Public Sub SelectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        RaiseEvent CouponSelect(ddlApplyTo.SelectedIndex, EventArgs.Empty)
    End Sub
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

#Region "Function FillCoupon() As CDiscount"
    Private Function FillCoupon() As CDiscount
        Dim objCoupon As New CDiscount()

        objCoupon.ID = txtID.Text
        objCoupon.Name = "Coupon"
        objCoupon.Code = txtCode.Text
        objCoupon.Description = txtDiscription.Text

        If Me.rdoDollar.Checked Then
            objCoupon.DiscountType = SystemBase.DiscountTypes.Dollar
            objCoupon.DollarOff = 0 & txtAmount.Text
            objCoupon.PercentOff = 0
        Else
            objCoupon.DiscountType = SystemBase.DiscountTypes.Percent
            objCoupon.PercentOff = 0 & txtAmount.Text
            objCoupon.DollarOff = 0
        End If

        If (chkOneTime.Checked) Then
            objCoupon.Expires = "OneTime"
        ElseIf DropDownList1.SelectedIndex = 0 Then
            objCoupon.Expires = "Never"
        Else
            objCoupon.Expires = txtDate.Text
        End If

        If chkActive.Checked = True Then
            objCoupon.IsActive = True
        Else
            objCoupon.IsActive = False
        End If

        If (ddlApplyTo.SelectedIndex = 0) Then
            objCoupon.AppliedTo = SystemBase.AppliedDiscounts.All
            objCoupon.AppliedToID = 0
        Else
            objCoupon.AppliedToID = CLng(txtApplyToID.Text)
            objCoupon.AppliedTo = ddlApplyTo.SelectedItem.Value
        End If
        objCoupon.MinAmountType = MinAmountTypes.MerchandiseTotal
        objCoupon.MinimumAmount = CDec(minAmt.Text)
        '1205 '2412
        'objCoupon.ApplyOnce = CBool(ddlApplyOnce.SelectedItem.Value)
        objCoupon.ApplyOnce = CBool(ddlApplyOnce.SelectedIndex)
        '1205 '2412
        Return objCoupon
    End Function
#End Region

#Region "Validate"
    Public Function validate() As Boolean
        If txtDiscription.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankDiscription")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtCode.Text = "" Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankCode")
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
            '## Issue 953 Start
        ElseIf txtApplyToID.Text = "0" And ddlApplyTo.SelectedItem.Value = 1 Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankProductID")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtApplyToID.Text = 0 And ddlApplyTo.SelectedItem.Value = 2 Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankCategoryID")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtApplyToID.Text = "0" And ddlApplyTo.SelectedItem.Value = 3 Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankVendorID")
            ErrorMessage.Visible = True
            Return False
        ElseIf txtApplyToID.Text = "0" And ddlApplyTo.SelectedItem.Value = 4 Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankManufacturerID")
            ErrorMessage.Visible = True
            Return False
            '## Issue 953 End
        ElseIf (txtApplyToID.Text = "0" And ddlApplyTo.SelectedIndex > 0) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", "BlankApplyToID")
            ErrorMessage.Visible = True
            Return False
        Else
            If (Me.DropDownList1.SelectedIndex > 0) Then
                Try
                    If CDate(txtDate.Text) < Now() Then
                        ErrorMessage.Text = m_objMessages.GetXMLMessage("addcoupon", "Error", "EarlyDate")
                        ErrorMessage.Visible = True
                        Return False
                    End If
                Catch ex As Exception
                    ErrorMessage.Text = m_objMessages.GetXMLMessage("addcoupon", "Error", "InvalidDate")
                    ErrorMessage.Visible = True
                    Return False
                End Try
            End If
        End If
        Return True
    End Function
#End Region

#Region "Sub ddlApplyTo_SelectedIndexChanged"
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
#End Region

End Class


'Private Sub SetErrorMessage(ByVal strCondition As String)
'    ErrorMessage.Text = m_objMessages.GetXMLMessage("editcoupon", "Error", strCondition)
'    ErrorMessage.Visible = True
'End Sub