Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase

Partial  Class freeshipping
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ErrorMessage = CType(Me.Parent.FindControl("ErrorMessage"), Label)

        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
    End Sub

    Public Sub LoadFreeShipping()
        Dim obj As New CStoreDiscounts()
        Dim objDiscount As CDiscount
        For Each objDiscount In obj.GetAllDiscounts.DiscountItems
            If (objDiscount.Name.ToLower = "freeshipping") Then
                txtAmount.Text = objDiscount.MinimumAmount
                chkActive.Checked = objDiscount.IsActive
                If (objDiscount.Expires.ToLower = "never") Then
                    DropDownList1.SelectedIndex = 0
                Else
                    DropDownList1.SelectedIndex = 1
                    txtDate.Text = objDiscount.Expires
                End If
                Exit For
            End If
        Next
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If chkActive.Checked Then
            If (txtAmount.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("freeshipping", "Error", "BlankOrderAmount")
                ErrorMessage.Visible = True
                Exit Sub
            ElseIf (DropDownList1.SelectedIndex = 1) Then
                If (txtDate.Text = "") Then
                    ErrorMessage.Text = m_objMessages.GetXMLMessage("freeshipping", "Error", "BlankDate")
                    ErrorMessage.Visible = True
                    Exit Sub
                End If
            End If
        End If
        Dim obj As New CStoreDiscounts()
        Dim objDiscount As CDiscount

        For Each objDiscount In obj.GetAllDiscounts.DiscountItems
            If (objDiscount.Name = "FreeShipping") Then
                If (chkActive.Checked) Then
                    objDiscount.MinimumAmount = CDec(txtAmount.Text)
                Else
                    objDiscount.MinimumAmount = 0
                End If
                If (DropDownList1.SelectedIndex = 0) Then
                    objDiscount.Expires = "Never"
                Else
                    objDiscount.Expires = txtDate.Text
                End If

                objDiscount.IsActive = chkActive.Checked

                obj.UpdateDiscounts(objDiscount)

                RaiseEvent Save(Nothing, EventArgs.Empty)
                Exit Sub
            End If
        Next

        ' Create one
        objDiscount = New CDiscount()
        objDiscount.Name = "FreeShipping"
        objDiscount.AppliedTo = AppliedDiscounts.All
        objDiscount.AppliedToID = 0
        objDiscount.DiscountType = DiscountTypes.Dollar
        objDiscount.DollarOff = 0
        objDiscount.PercentOff = 0
        objDiscount.Code = ""
        objDiscount.MinAmountType = MinAmountTypes.MerchandiseTotal
        objDiscount.Description = "Free Shipping"

        objDiscount.IsActive = chkActive.Checked
        objDiscount.MinimumAmount = CDec(txtAmount.Text)
        If (DropDownList1.SelectedIndex = 0) Then
            objDiscount.Expires = "Never"
        Else
            objDiscount.Expires = txtDate.Text
        End If

        obj.InsertDiscounts(objDiscount)

        RaiseEvent Save(Nothing, EventArgs.Empty)
    End Sub
End Class
