Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial  Class CGiftWrapControl
    Inherits CWebControl


#Region " Members"

    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Private m_Price As Decimal
    Private m_ProdId As String
    Private alGW As ArrayList
    Private m_ProdName As String
    Protected WithEvents tblHead As System.Web.UI.HtmlControls.HtmlTable
    Private m_objItem As CCartItem
    Private lngItem As Long
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

#Region "  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim lngCount As Long = 1
        Dim oGiftWrap As CGiftWrap
        Dim arClear As New ArrayList()
        Dim x As Integer
        'Dim oItem As DataListItem
        Dim ingGiftwrapCount As Long


        lngItem = Request.QueryString("Item")
        If Not IsPostBack Then

            dlGW.EnableViewState = False
            For Each m_objItem In m_objxmlcart.GetCartItems
                If lngCount = lngItem Then
                    m_ProdId = m_objItem.ProductCode
                    m_ProdName = m_objItem.Name
                    Session("GiftWraps") = New ArrayList(m_objItem.GiftWraps) ' #1470
                    lblPrice.Text = "Gift Wrap Price:" & FormatCurrency(m_objItem.GiftWrapPrice) & "/each"
                    lblId.Text = m_objItem.ProductCode & " : " & m_objItem.Name
                    'If m_objItem.GiftWrapQty > 0 Then
                    If m_objItem.GiftWrapQty < m_objItem.Quantity Then
                        ingGiftwrapCount = m_objItem.Quantity - m_objItem.GiftWraps.Count

                        For x = 1 To ingGiftwrapCount
                            oGiftWrap = New CGiftWrap()
                            m_objItem.GiftWrapQty = m_objItem.GiftWrapQty + 1
                            m_objItem.GiftWraps.Add(oGiftWrap)
                        Next
                    ElseIf m_objItem.GiftWraps.Count > m_objItem.Quantity Then

                        Dim iHi As Integer = m_objItem.GiftWraps.Count - 1

                        For x = 0 To m_objItem.GiftWraps.Count - 1
                            oGiftWrap = m_objItem.GiftWraps(iHi)
                            m_objItem.GiftWraps.Remove(oGiftWrap)
                            iHi = iHi - 1

                            If m_objItem.GiftWraps.Count = m_objItem.Quantity Then
                                Exit For
                            End If
                        Next
                    End If
                    dlGW.DataSource = m_objItem.GiftWraps
                    dlGW.DataBind()
                    dlGW.EnableViewState = True
                    'Else
                    '  dlGW.Visible = False
                    'End If
                    Exit For
                End If
                lngCount = lngCount + 1
            Next
        End If


        imgContinue.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
        imgCancel.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Cancel").Attributes("Filepath").Value
    End Sub

#End Region

#Region " Private Sub dlGW_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlGW.ItemCreated"


    Private Sub dlGW_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlGW.ItemCreated






    End Sub
#End Region

#Region " Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.eventArgs) Handles cmdCancel.Click"


    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dim objGw As CGiftWrap
        Dim lngCount As Integer = 1
        Dim arRemoved As New ArrayList()
        Dim iCount As Integer = 0
        For Each m_objItem In m_objxmlcart.GetCartItems
            If lngCount = lngItem Then

                m_objItem.GiftWraps = Session("GiftWraps")
                For Each objGw In m_objItem.GiftWraps
                    If objGw.IsChecked Then
                        iCount = iCount + 1
                    End If
                Next
                m_objItem.GiftWrapQty = iCount
                Session("GiftWraps") = Nothing
                Exit For
            End If
            lngCount = lngCount + 1
        Next
        Response.Redirect("ShoppingCart.aspx")
    End Sub
#End Region

#Region "Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdContinue.Click"


    Private Sub cmdContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click


        Dim objGw As CGiftWrap
        Dim oItem As DataListItem
        Dim i As Integer = 0
        Dim lngCount As Long
        Dim chkGiftWrap As CheckBox
        Dim oTo As TextBox
        Dim oFrom As TextBox
        Dim oMessage As TextBox
        If IsPostBack Then
            lngCount = 1
            For Each m_objItem In m_objxmlcart.GetCartItems
                If lngCount = lngItem Then
                    If m_objItem.GiftWraps.Count > 0 Then
                        objGw = m_objItem.GiftWraps.Item(0)
                        m_objItem.GiftWraps.Clear()
                        ' Update the Array with new
                        For Each oItem In dlGW.Items
                            chkGiftWrap = oItem.FindControl("chkGW")
                            If chkGiftWrap.Checked Then
                                oTo = oItem.FindControl("txtTo")
                                oFrom = oItem.FindControl("txtFrom")
                                oMessage = oItem.FindControl("txtMessage")
                                objGw = New CGiftWrap()
                                objGw.MessageTo = oTo.Text
                                objGw.MessageFrom = oFrom.Text
                                objGw.Message = oMessage.Text
                                objGw.IsChecked = True
                                m_objItem.GiftWraps.Add(objGw)
                                i = i + 1
                            End If

                        Next
                        m_objItem.GiftWrapQty = i
                        m_objxmlcart.UpdateItem(m_objItem)
                    End If
                    Exit For
                End If
                lngCount = lngCount + 1
            Next
        End If
        Session("GiftWraps") = Nothing
        Response.Redirect("ShoppingCart.aspx")
    End Sub
#End Region

End Class
