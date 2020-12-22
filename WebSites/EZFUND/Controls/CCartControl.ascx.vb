Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class CCartControl
    Inherits CShoppingCartControlBase

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

    Protected Shadows WithEvents DynaCart As UITools.DynamicCartDisplay
    Protected Shadows WithEvents Total As System.Web.UI.WebControls.Label

    Event RemovedCartItem As EventHandler

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DynaCart.NotPostBack = False
        MyBase.DynaCart = DynaCart
        MyBase.Total = Total


        DynaCart.BackOrderMSG = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "BackOrder", "CanBackOrder")
        DynaCart.NoBackOrderMSG = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "BackOrder", "CannotBackOrder")

        DynaCart.OandaISO = Session("ConvertISO")
        DynaCart.OandaRate = CDec(Session("OandaRate"))
        DynaCart.BackOrderMSG = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "BackOrder", "CanBackOrder")
        DynaCart.NoBackOrderMSG = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "BackOrder", "CannotBackOrder")

        Dim objAdmin As New Admin.CShipping(StoreFrontConfiguration.AdminShipping)

        DynaCart.MultiShipMessage = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "MultiShip", "CheckLabel")
        If objAdmin.AdditionalAddressHandling <> 0 Then
            If (DynaCart.MultiShipMessage.IndexOf("[AdditionalHandling]") <> -1) Then
                DynaCart.MultiShipMessage = DynaCart.MultiShipMessage.Replace("[AdditionalHandling]", Format(objAdmin.AdditionalAddressHandling, "c"))
            End If
        Else
            If InStr(DynaCart.MultiShipMessage, "(") > 0 Then
                DynaCart.MultiShipMessage = Left(DynaCart.MultiShipMessage, InStr(DynaCart.MultiShipMessage, "(") - 1)
            End If

        End If
        DynaCart.NegativeMessage = m_objMessages.GetXMLMessage("ShoppingCart.aspx", "Error", "NegativeQty")
        DynaCart.TooLargeMessage = m_objMessages.GetXMLMessage("Search.aspx", "Error", "LargeQty")
       
        If (m_objxmlcart.GetCartItems.Count = 0) Then
            DynaCart.Visible = False
        Else
            DynaCart.Visible = True
            m_objxmlcart.CustomerGroup = m_objCustomer.CustomerGroup
            DynaCart.DataSource = m_objxmlcart.GetCartItems
            DynaCart.DataBind()
        End If
        DynaCart.RemoveImg = dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filename").Value
        DynaCart.SavedCartImg = dom.Item("SiteProducts").Item("SiteImages").Item("SaveCart").Attributes("Filename").Value
        DynaCart.ReOrderImg = dom.Item("SiteProducts").Item("SiteImages").Item("ReOrder").Attributes("Filename").Value
        DynaCart.GiftWrapImg = dom.Item("SiteProducts").Item("SiteImages").Item("GiftWrap").Attributes("Filename").Value

    End Sub

    Public WriteOnly Property CartData() As DataView
        Set(ByVal Value As DataView)
            If (Value.Count = 0) Then
                DynaCart.Visible = False
            Else
                DynaCart.Visible = True
                DynaCart.DataSource = Value
                DynaCart.DataBind()
            End If
        End Set
    End Property

    Public WriteOnly Property CartDataArray() As ArrayList
        Set(ByVal Value As ArrayList)
            If (Value.Count = 0) Then
                DynaCart.Visible = False
            Else
                DynaCart.Visible = True
                DynaCart.DataSource = Value
                DynaCart.DataBind()
            End If
        End Set
    End Property

    Private Sub DynaCart_RemoveBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynaCart.RemoveBtnClick
        Dim objItem As CCartItem
        Dim ingCount As Long = 1
        For Each objItem In m_objxmlcart.GetCartItems()
            If (ingCount = CLng(sender)) Then
                m_objxmlcart.DeleteItem(objItem, ingCount)
                Exit For
            End If
            ingCount = ingCount + 1
        Next

        If (m_objxmlcart.GetCartItems.Count = 0) Then
            DynaCart.Visible = False
        Else
            DynaCart.Visible = True
            DynaCart.NotPostBack = True
            DynaCart.DataSource = m_objxmlcart.GetCartItems()
            DynaCart.DataBind()
        End If

        RaiseEvent RemovedCartItem(sender, e)
    End Sub

    Private Sub DynaCart_GiftWrapBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynaCart.GiftWrapBtnClick
        Response.Redirect("GiftWrap.aspx?Item=" & sender)
    End Sub

    Private Sub DynaCart_SavedCartBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynaCart.SavedCartBtnClick
        '  Dim objProductAccess As New CXMLProductAccess(dom)
        Dim ingCount As Long = 1
        Dim objSelectItem As CCartItem
        Dim txtQty As TextBox
        For Each objSelectItem In m_objxmlcart.GetCartItems()
            If (ingCount = CLng(sender)) Then
                Dim objItem As CGenericCartItem

                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    objItem = New CGenericCartItem(objSelectItem, m_objCustomer.CustomerGroup)

                Else
                    Dim oprodManagement As New Management.CProductManagement()
                    Dim drProd As DataRow = oprodManagement.GetProductRow(objSelectItem.ProductID, Me.m_objCustomer.CustomerGroup).Products.Rows(0)
                    objItem = New CGenericCartItem(drProd, 1, objSelectItem.Attributes, Me.m_objCustomer.CustomerGroup, True)
                    oprodManagement = Nothing
                End If

                txtQty = DynaCart.FindControl("Qty" & sender)
                objItem.Quantity = txtQty.Text
                m_objxmlcart.DeleteItem(objSelectItem, ingCount)
                Session("AddSavedItem") = objItem
                Response.Redirect("SavedCart.aspx")
                Exit For
            End If
            ingCount = ingCount + 1
        Next

    End Sub

    Private Sub DynaCart_BuyNowBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynaCart.BuyNowBtnClick

    End Sub

    Private Sub DynaCart_ReOrderBtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DynaCart.ReOrderBtnClick

    End Sub
End Class
