Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Public MustInherit Class addcoupon
    Inherits CWebControl

    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents chkActive As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDiscription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdoDollar As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdoPercent As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ddlApplyTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSelect As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkOneTime As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtApplyToID As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkAllowWithSale As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkAllowMulti As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgSave As System.Web.UI.WebControls.Image
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
    Event CouponSelect As EventHandler

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
        Dim objCoupon As New CDiscount()

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
        Return objCoupon
    End Function
#End Region

#Region "Sub ClearFields"
    Public Sub ClearFields()
        txtDiscription.Text = ""
        txtCode.Text = ""
        txtAmount.Text = ""
        rdoDollar.Checked = True
        ddlApplyTo.SelectedIndex = 0
        DropDownList1.SelectedIndex = 0
        txtDate.Text = ""
        chkOneTime.Checked = False
        chkActive.Checked = False

        txtApplyToID.Text = "0"
        btnSelect.Visible = False
        imgSelect.Visible = False
    End Sub
#End Region

#Region "Sub SaveClick"
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If validate() Then
            Dim objCoupon As CDiscount = FillCoupon()
            Dim obj As New CStoreDiscounts()
            Dim objList As CDiscounts = obj.GetAllDiscounts
            Dim objItem As CDiscount
            For Each objItem In objList.CouponItems
                If (objItem.Code = objCoupon.Code) Then
                    ErrorMessage.Text = "Error: Coupon already exists."
                    ErrorMessage.Visible = True
                    Return
                End If
            Next

            obj.InsertDiscounts(objCoupon)

            txtDiscription.Text = ""
            txtCode.Text = ""
            txtAmount.Text = ""
            rdoDollar.Checked = True
            ddlApplyTo.SelectedIndex = 0
            DropDownList1.SelectedIndex = 0
            txtDate.Text = ""
            chkOneTime.Checked = False
            chkActive.Checked = False

            txtApplyToID.Text = "0"
            btnSelect.Visible = False
            imgSelect.Visible = False

            RaiseEvent Save(Nothing, EventArgs.Empty)
        End If
    End Sub
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
            ErrorMessage.Text = m_objMessages.GetXMLMessage("adddiscount", "Error", "BlankApplyToID")
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

    Public Sub SelectClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        RaiseEvent CouponSelect(ddlApplyTo.SelectedIndex, EventArgs.Empty)
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

