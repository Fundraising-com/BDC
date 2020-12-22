Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Public MustInherit Class SalesDiscount
    Inherits CWebControl
    Protected WithEvents txtPromotionCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents DataList1 As System.Web.UI.WebControls.DataList
    Protected WithEvents lblSpecialOffer As System.Web.UI.WebControls.Label
    Protected WithEvents btnApply As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgApply As System.Web.UI.WebControls.Image
    Protected WithEvents Spacer1 As HtmlTableRow
    Protected WithEvents Spacer2 As HtmlTableRow
    Protected WithEvents Spacer3 As HtmlTableRow
    Protected WithEvents Label As HtmlTableRow
    Protected WithEvents BarSpacer As HtmlTableRow
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents CouponList As HtmlTableRow

    Event CouponAdd As EventHandler
    Event CouponRemove As EventHandler
    Event CouponError As EventHandler

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
        ' TODO Get Special Offers Message of products
        lblSpecialOffer.Text = ""
        lblSpecialOffer.Visible = False
        If (m_objxmlcart.AppliedDiscounts.Count = 0) Then

            Spacer1.Visible = False
            Spacer2.Visible = False
            Spacer3.Visible = False
            Label.Visible = False
            BarSpacer.Visible = False
            CouponList.Visible = False
        Else
            Spacer1.Visible = True
            Spacer2.Visible = True
            Spacer3.Visible = True
            Label.Visible = True
            BarSpacer.Visible = True
            CouponList.Visible = True
        End If

        DataList1.DataSource = m_objXMLCart.AppliedDiscounts
        DataList1.DataBind()
        imgApply.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Apply").Attributes("Filepath").Value

        Dim con As DataListItem
        For Each con In DataList1.Items
            CType(con.FindControl("imgCouponRemove"), System.Web.UI.WebControls.Image).ImageUrl = "../images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filename").Value
        Next

    End Sub

    Public Sub ReloadList()
        If (IsNothing(DataList1) = False And IsNothing(m_objxmlcart) = False) Then
            txtPromotionCode.Text = ""
            DataList1.DataSource = m_objXMLCart.AppliedDiscounts
            DataList1.DataBind()

            If (m_objxmlcart.AppliedDiscounts.Count = 0) Then
                Spacer1.Visible = False
                Spacer2.Visible = False
                Spacer3.Visible = False
                Label.Visible = False
                BarSpacer.Visible = False
                CouponList.Visible = False
            Else
                Spacer1.Visible = True
                Spacer2.Visible = True
                Spacer3.Visible = True
                Label.Visible = True
                BarSpacer.Visible = True
                CouponList.Visible = True
            End If
        End If
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim objCoupon As CDiscount
        Dim bFound As Boolean = False
        Dim objStoreDiscounts As New CStoreDiscounts()
        Dim m_objDiscounts As CDiscounts = objStoreDiscounts.GetDiscounts

        For Each objCoupon In m_objxmlcart.AppliedDiscounts
            If (objCoupon.Code = txtPromotionCode.Text) Then
                RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", "AlreadyUsed"), EventArgs.Empty)
                Exit Sub
            End If
        Next

        For Each objCoupon In m_objDiscounts.CouponItems
            If (objCoupon.Code = txtPromotionCode.Text) Then
                bFound = True
                If (m_objDiscounts.CanAddCoupon(objCoupon, m_objXMLcart.GetCartItems, m_objxmlCart.AppliedDiscounts)) Then
                    m_objXMLCart.AddCoupon(objCoupon)
                    DataList1.DataSource = m_objXMLCart.AppliedDiscounts
                    DataList1.DataBind()
                    Spacer1.Visible = True
                    Spacer2.Visible = True
                    Spacer3.Visible = True
                    Label.Visible = True
                    BarSpacer.Visible = True
                    CouponList.Visible = True
                    RaiseEvent CouponAdd(Nothing, EventArgs.Empty)
                Else
                    ' Error Message
                    RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", m_objDiscounts.AddCouponError), EventArgs.Empty)
                End If
                Exit For
            End If
        Next
        txtPromotionCode.Text = ""
        If (bFound = False) Then
            RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", "NotFound"), EventArgs.Empty)
        End If
    End Sub
    'Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim objCoupon As CDiscount
    '    Dim bFound As Boolean = False
    '    Dim objStoreDiscounts As New CStoreDiscounts()
    '    Dim m_objDiscounts As CDiscounts = objStoreDiscounts.GetDiscounts

    '    For Each objCoupon In m_objxmlcart.AppliedDiscounts
    '        If (objCoupon.Code = txtPromotionCode.Text) Then
    '            RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", "AlreadyUsed"), EventArgs.Empty)
    '            Exit Sub
    '        End If
    '    Next

    '    For Each objCoupon In m_objDiscounts.CouponItems
    '        If (objCoupon.Code = txtPromotionCode.Text) Then
    '            bFound = True
    '            If (m_objDiscounts.CanAddCoupon(objCoupon, m_objXMLcart.GetCartItems, m_objxmlCart.AppliedDiscounts)) Then
    '                m_objXMLCart.AddCoupon(objCoupon)
    '                DataList1.DataSource = m_objXMLCart.AppliedDiscounts
    '                DataList1.DataBind()
    '                Spacer1.Visible = True
    '                Spacer2.Visible = True
    '                Spacer3.Visible = True
    '                Label.Visible = True
    '                BarSpacer.Visible = True
    '                CouponList.Visible = True
    '                RaiseEvent CouponAdd(Nothing, EventArgs.Empty)
    '            Else
    '                ' Error Message
    '                RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", m_objDiscounts.AddCouponError), EventArgs.Empty)
    '            End If
    '            Exit For
    '        End If
    '    Next
    '    txtPromotionCode.Text = ""
    '    If (bFound = False) Then
    '        RaiseEvent CouponError(m_objMessages.GetXMLMessage("SalesDiscount", "Error", "NotFound"), EventArgs.Empty)
    '    End If
    'End Sub

    Public Sub RemoveCoupon(ByVal sender As Object, ByVal e As EventArgs)
        Dim objButton As LinkButton = CType(sender, LinkButton)
        Dim objCoupon As CDiscount
        For Each objCoupon In m_objXMLCart.AppliedDiscounts
            If (objCoupon.ID = CLng(objButton.CommandArgument)) Then
                m_objXMLCart.DeleteCoupon(objCoupon)
                Exit For
            End If
        Next
        txtPromotionCode.Text = ""
        RaiseEvent CouponRemove(Nothing, EventArgs.Empty)
    End Sub

    Private Sub DataList1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemCreated
        Dim objButton As System.Web.UI.WebControls.Image
        objButton = e.Item.FindControl("imgCouponRemove")
        If (IsNothing(objButton) = False) And (IsNothing(dom) = False) Then
            objButton.ImageUrl = "../images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filename").Value
        End If
    End Sub
End Class
