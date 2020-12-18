Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.orders
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase
Imports StoreFront.UITools
Imports CSR.CSRBusinessRule
Partial  Class CSRDiscounts
    Inherits CSRWebControl
    Protected WithEvents btnApply As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Linkbutton1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Linkbutton2 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Table3 As System.Web.UI.HtmlControls.HtmlTable
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

#Region "Class Events"
    Event RecalculateOrder()

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetOrder()
        ReCalculate()
    End Sub
    


    Public Sub RemoveCoupon(ByVal sender As Object, ByVal e As EventArgs)
        Dim objButton As LinkButton = CType(sender, LinkButton)
        Dim objCoupon As CDiscount
        Dim x As Integer = 0
        For Each objCoupon In csrorder.Coupons
            If (objCoupon.ID = CLng(objButton.CommandArgument)) Then
                csrorder.Coupons.RemoveAt(x)
                Exit For
            End If
            x = x + 1
        Next
        txtPromotionCode.Text = ""
        ReCalculate()
    End Sub
    

    Public Sub ReCalculate()
        GetOrder()
        Dim objGiftCertificate As CGiftCertificate
        Dim dTotal As Decimal = CSROrder.OrderTotal

        For Each objGiftCertificate In CSROrder.GiftCertificates
            If (objGiftCertificate.DollarOff < dTotal) Then
                objGiftCertificate.AmountUsed = objGiftCertificate.DollarOff
                dTotal = dTotal - objGiftCertificate.DollarOff
            Else
                objGiftCertificate.AmountUsed = dTotal
                dTotal = 0
            End If
        Next
        Coupons.DataSource = csrorder.Coupons
        Coupons.DataBind()
        Coupons.Visible = True
        If (CSROrder.Coupons.Count = 0) Then
            Coupons.Visible = False
        Else
            Coupons.Visible = True
        End If


        GiftCertificates.DataSource = CSROrder.GiftCertificates
        GiftCertificates.DataBind()
        If (CSROrder.GiftCertificates.Count = 0) Then
            GiftCertificates.Visible = False
        Else
            GiftCertificates.Visible = True
        End If
        TotalDisplay1.OandaISO = Session("ConvertISO")
        TotalDisplay1.OandaRate = CDec(Session("OandaRate"))
        csrOrder.SetAddressIndex()
        TotalDisplay1.DataSource = CSROrder
        TotalDisplay1.DataBind()
        If csrorder.ItemCount = 0 Then
            TotalDisplay1.Visible = False
        Else
            TotalDisplay1.Visible = True
        End If
        Dim CSRMan As New CSRManagement(StoreFrontConfiguration.SiteURL)
        If CSRMan.IsAdvancedCSR = False Then
            OtherDiscount.Visible = False
        End If
    End Sub

    Public Sub RemoveGiftCertificate(ByVal sender As Object, ByVal e As EventArgs)
        Dim objButton As LinkButton = CType(sender, System.Web.UI.WebControls.LinkButton)
        Dim objGiftCertificate As CGiftCertificate
        For Each objGiftCertificate In csrOrder.GiftCertificates
            If (objGiftCertificate.ID = CLng(objButton.CommandArgument)) Then
                csrOrder.DeleteGiftCertificate(objGiftCertificate)
                Exit For
            End If
        Next
        GiftCertificateCode.Text = ""
        ReCalculate()
    End Sub

    Private Sub ApplyCoupon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyCoupon.Click
        Dim objCoupon As CDiscount
        Dim bFound As Boolean = False
        Dim objStoreDiscounts As New CStoreDiscounts
        Dim m_objDiscounts As CDiscounts = objStoreDiscounts.GetDiscounts

        For Each objCoupon In m_objxmlcart.AppliedDiscounts
            If (objCoupon.Code = txtPromotionCode.Text) Then
                MagicAjax.AjaxCallHelper.WriteAlert("Coupon is already used on this order.")
                Exit Sub
            End If
        Next

        For Each objCoupon In m_objDiscounts.CouponItems
            If (objCoupon.Code = txtPromotionCode.Text) Then
                bFound = True
                If (m_objDiscounts.CanAddCoupon(objCoupon, GetIndividualItems, csrorder.Coupons)) Then
                    csrorder.Coupons.Add(objCoupon)
                    Coupons.DataSource = csrorder.Coupons
                    Coupons.DataBind()
                    Coupons.Visible = True
                    ReCalculate()
                Else
                    ' Error Message
                    MagicAjax.AjaxCallHelper.WriteAlert("Can not add coupon.")
                End If
                Exit For
            End If
        Next
        txtPromotionCode.Text = ""
        If (bFound = False) Then
            MagicAjax.AjaxCallHelper.WriteAlert("Coupon not found.")
        End If

    End Sub

    Private Sub ApplyGiftCertificate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyGiftCertificate.Click
        Dim objGiftCertificate As CGiftCertificate
        Dim bFound As Boolean = False
        Dim objStoreGiftCertificates As New CStoreGiftCertificates
        Dim m_objGiftCertificates As CGiftCertificates = objStoreGiftCertificates.GetGiftCertificates

        For Each objGiftCertificate In CSROrder.GiftCertificates
            If (objGiftCertificate.Code = GiftCertificateCode.Text) Then
                MagicAjax.AjaxCallHelper.WriteAlert("Gift certificate has already been added to this order.")
                Exit Sub
            End If
        Next

        For Each objGiftCertificate In m_objGiftCertificates.GiftCertificates
            If (objGiftCertificate.Code = GiftCertificateCode.Text) Then
                bFound = True
                If (m_objGiftCertificates.CanAddGiftCertificate(objGiftCertificate)) Then
                    CSROrder.AddGiftCertificate(objGiftCertificate)
                    GiftCertificates.DataSource = csrOrder.GiftCertificates
                    GiftCertificates.DataBind()
                    GiftCertificates.Visible = True

                    ReCalculate()

                Else
                    ' Error Message
                    ReCalculate()
                    MagicAjax.AjaxCallHelper.WriteAlert("Can not add gift certificate.")
                End If
                Exit For
            End If
        Next
        GiftCertificateCode.Text = ""
        If (bFound = False) Then
            MagicAjax.AjaxCallHelper.WriteAlert("Gift certificate not found.")
        End If
        ReCalculate()

    End Sub

    Private Sub ApplyOtherDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyOtherDiscount.Click
        Dim GiftCertificate As CGiftCertificate
        Dim objStoreDiscounts As New CStoreDiscounts
        Dim m_objDiscounts As CDiscounts = objStoreDiscounts.GetDiscounts

        If Me.OtherDiscountAmount.Text = "" OrElse IsNumeric(Me.OtherDiscountAmount.Text) = False OrElse Me.OtherDiscountName.Text = "" Then
            MagicAjax.AjaxCallHelper.WriteAlert("Please enter a discount name and valid discount amount.")
            Exit Sub
        End If
        Dim lDiscountAmt As Long = OtherDiscountAmount.Text
        If lDiscountAmt > CSROrder.GrandTotal Then
            lDiscountAmt = CSROrder.GrandTotal
        End If

        GiftCertificate = New CGiftCertificate
        Dim GiftCode As String = OtherDiscountName.Text & Now.Ticks
        GiftCertificate.Code = GiftCode
        'objCoupon.Description = OtherDiscountName.Text
        GiftCertificate.DollarOff = CDec(OtherDiscountAmount.Text)
        GiftCertificate.Expires = Now.AddDays(1)
        GiftCertificate.ID = 0
        Dim oGift As New CStoreGiftCertificates
        oGift.InsertGiftCertificate(GiftCertificate)
        For Each GiftCertificate In oGift.GetAllGiftCertificates.GiftCertificates
            If GiftCertificate.Code = GiftCode Then
                GiftCertificate.Code = OtherDiscountName.Text
                CSROrder.AddGiftCertificate(GiftCertificate)
            End If
        Next

        GiftCertificates.DataSource = CSROrder.GiftCertificates
        GiftCertificates.DataBind()
        GiftCertificates.Visible = True
        ReCalculate()


        Me.OtherDiscountAmount.Text = ""
        Me.OtherDiscountName.Text = ""

    End Sub
    
End Class
