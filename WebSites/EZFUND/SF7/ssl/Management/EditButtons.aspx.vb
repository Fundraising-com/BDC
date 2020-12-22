Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.IO

Partial Class EditButtons
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    'Protected WithEvents editor1 As Telerik.WebControls.RadEditor
    Protected WithEvents Tr2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ucUploadImage As SFExpressUploadControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
        If Not Page.IsPostBack Then
            BindDataList()
        End If
    End Sub

    Private Sub BindDataList()
        Dim buttonManager As New DesignManager
        Dim buttonsDataTable As DataTable = AppendThemeImageDirectory(buttonManager.GetAllButtons)
        dlButtonSet.DataSource = buttonsDataTable
        dlButtonSet.DataKeyField = "uid"
        dlButtonSet.DataBind()

    End Sub

    Private Function AppendThemeImageDirectory(ByVal dt As DataTable) As DataTable

        Dim odesignManager As New DesignManager
        Dim strActiveThemePath As String = odesignManager.GetActiveThemePath()

        Dim drLagardeLogo As DataRow = Nothing

        For Each dr As DataRow In dt.Rows
            dr("Filename") = String.Format("{0}images/{1}", strActiveThemePath, dr("Filename"))
            ' while we're here lets remove the 'LagardeLogo' from showing by 
            ' removing it from the datatable, store the row here it will actually
            ' be removed below
            If dr("Name").ToString.ToUpper.Equals("LAGARDELOGO") Then
                drLagardeLogo = dr
            End If
        Next dr

        If Not drLagardeLogo Is Nothing Then
            dt.Rows.Remove(drLagardeLogo)
        End If

        Return dt

    End Function

    Public Sub SaveButton(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim dlItem As DataListItem = CType(sender, ImageButton).Parent
        CType(dlItem.FindControl("ucUploadImage"), SFExpressUploadControl).Visible = True

        Dim strImageUrl As String = CType(dlItem.FindControl("LabelImage"), WebControls.Image).ImageUrl()
        Dim oFileInfo As New FileInfo(Me.MapPath(strImageUrl))
        Dim strUploadPath As String = strImageUrl.Replace(oFileInfo.Name, String.Empty)
        CType(dlItem.FindControl("ucUploadImage"), SFExpressUploadControl).UploadPath = strUploadPath

        CType(dlItem.FindControl("ucUploadImage"), SFExpressUploadControl).PanelVisible()
    End Sub

    Private Sub dlButtonSet_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlButtonSet.ItemCommand
        If e.CommandSource.GetType Is GetType(LinkButton) AndAlso CType(e.CommandSource, LinkButton).ID = "uploadBtn" Then
            If CType(e.CommandSource, LinkButton).CommandName <> String.Empty Then
                Dim myDesignManager As New DesignManager
                Dim dl As DataList = CType(source, DataList)
                Dim itemUid As Long = CLng(dl.DataKeys(e.Item.ItemIndex))
                myDesignManager.UpdateButtonImages(itemUid, CType(e.CommandSource, LinkButton).CommandName)
                BindDataList()
            End If
        End If
    End Sub

    Private Sub dlButtonSet_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlButtonSet.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) AndAlso Not IsNothing(e.Item.FindControl("ButtonLabel")) Then
            CType(e.Item.FindControl("ButtonLabel"), Label).Text = SetLabel(CType(e.Item.FindControl("ButtonLabel"), Label).Text)
        End If
    End Sub

    Private Function SetLabel(ByVal labelText As String) As String
        If labelText = "Search" Then
            Return "Go"
        ElseIf labelText = "AddToOrder" Then
            Return "Add To Cart"
        ElseIf labelText = "EmailFriend" Then
            Return "E-mail A Friend"
        ElseIf labelText = "SignIn" Then
            Return "Sign In"
        ElseIf labelText = "CreateAccount" Then
            Return "Create Account"
        ElseIf labelText = "UpdateQuantity" Then
            Return "Update Quantity"
        ElseIf labelText = "Apply" Then
            Return "Apply"
        ElseIf labelText = "CheckOut" Then
            Return "CheckOut"
        ElseIf labelText = "Continue" Then
            Return "Continue"
        ElseIf labelText = "Add" Then
            Return "Add"
        ElseIf labelText = "Clear" Then
            Return "Clear"
        ElseIf labelText = "Delete" Then
            Return "Delete"
        ElseIf labelText = "Edit" Then
            Return "Edit"
        ElseIf labelText = "Cancel" Then
            Return "Cancel"
        ElseIf labelText = "Remove" Then
            Return "Remove"
        ElseIf labelText = "Save" Then
            Return "Save"
        ElseIf labelText = "View" Then
            Return "View"
        ElseIf labelText = "Track" Then
            Return "Track"
        ElseIf labelText = "Send" Then
            Return "Send"
        ElseIf labelText = "SignOut" Then
            Return "Sign Out"
        ElseIf labelText = "BuyNow" Then
            Return "Buy Now"
        ElseIf labelText = "Close" Then
            Return "Close"
        ElseIf labelText = "SaveCart" Then
            Return "Save Cart"
        ElseIf labelText = "GiftWrap" Then
            Return "Gift Wrap"
        ElseIf labelText = "ManageAddresses" Then
            Return "Manage Addresses"
        ElseIf labelText = "AddAddress" Then
            Return "Add Address"
        ElseIf labelText = "CompleteOrder" Then
            Return "Complete Order"
        ElseIf labelText = "ViewAndPrintReceipt" Then
            Return "View/Print Receipt"
        ElseIf labelText = "ReOrder" Then
            Return "Re-order"
        ElseIf labelText = "AddToSavedCart" Then
            Return "Add To Saved Cart"
        ElseIf labelText = "FilterResults" Then
            Return "Filter Results"
        Else
            Return labelText
        End If
    End Function
End Class
