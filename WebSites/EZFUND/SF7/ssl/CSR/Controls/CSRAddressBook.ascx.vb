Imports System.ComponentModel
Imports StoreFront.SystemBase

Partial  Class CSRAddressBook
    Inherits CSRWebControl

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

    Event AddressEdit As EventHandler
    Event AddressDelete As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetOrder()
        If (CSRCustomer.AddressList.Count = 0) Then
            DataGrid1.Visible = False
        Else
            DataGrid1.Visible = True
        End If

        Dim _List As New ArrayList
        Dim _addr As Address

        For Each _addr In CSRCustomer.AddressList
            If (_addr.ID <> -1) Then
                _List.Add(_addr)
            End If
        Next

        DataGrid1.DataSource = _List
        DataGrid1.DataBind()

        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        Else
            DataGrid1.PagerStyle.Visible = True
        End If
        Dim con As DataGridItem
        For Each con In DataGrid1.Items
            CType(con.FindControl("imgDelete"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Delete").Attributes("Filepath").Value
            CType(con.FindControl("imgEdit"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Edit").Attributes("Filepath").Value
        Next
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.DataSource = CSRCustomer.AddressList
        DataGrid1.DataBind()
    End Sub

    Public Sub EditClick(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AddressEdit(sender, e)
    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objButton As LinkButton = sender
        Dim objAddress As New Address
        objAddress.ID = objButton.CommandArgument

        CSRCustomer.DeleteAddress(objAddress)

        RaiseEvent AddressDelete(sender, e)

        ReLoadAddresses()
    End Sub

    Public Sub ReLoadAddresses()
        GetOrder()
        CSRCustomer.ReLoadAddresses()
        DataGrid1.CurrentPageIndex = 0
        DataGrid1.DataSource = CSRCustomer.AddressList
        DataGrid1.DataBind()
        DataGrid1.Visible = True
        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        Else
            DataGrid1.PagerStyle.Visible = True
        End If
    End Sub

    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Dim objLabel As CSRAddressLabel

        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            objLabel = e.Item.FindControl("AddressLabel1")
            objLabel.AddressSource = e.Item.DataItem
        End If
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource

            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex - 1
            End If
            DataGrid1.DataSource = CSRCustomer.AddressList
            DataGrid1.DataBind()
        End If
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton

            If (DataGrid1.CurrentPageIndex > 0) Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (DataGrid1.CurrentPageIndex < DataGrid1.PageCount - 1) Then
                objSpace = New Label
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        End If
        If (Not (IsNothing(dom))) Then
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                CType(e.Item.FindControl("imgDelete"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Delete").Attributes("Filepath").Value
                CType(e.Item.FindControl("imgEdit"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Edit").Attributes("Filepath").Value
            End If
        End If
    End Sub


End Class
