Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Allows viewing and editing of menubar items for the theme being edited.
'This control is used inside the DesignEditor control for regions that
'have a cMenubar control.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Partial Class NavObjects
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtLabel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLinkUrl As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlLink As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public CurrentArea As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsNothing(ddlLinkType) AndAlso Not IsNothing(ddlLinkType.SelectedValue) Then
            If ddlLinkType.SelectedValue = "StoreFront Logo" Then
                cmdAdd.CommandName = "StoreFront Affilliate Link"
            Else
                cmdAdd.CommandName = ddlLinkType.SelectedValue
            End If
        End If

    End Sub

    Public Sub Bind()
        Dim ds As DataSet = (New DesignManager).GetAllMenuBarPreviewByMenuPosition(Me.GetArea)
        Me.dlNavObjects.DataSource = ds.Tables(0)
        Me.dlNavObjects.DataKeyField = "uid"
        Me.dlNavObjects.DataBind()
        Me.FillDropDown(Me.ddlLinkType)
    End Sub

    Public Function GetArea() As Long
        Select Case Me.CurrentArea
            Case "Top Sub Banner"
                Return 0
            Case "Left Column"
                Return 1
            Case "Right Column"
                Return 2
            Case "Bottom Bar"
                Return 3
            Case Else
                Return -1

        End Select
    End Function

    Public Sub ShowSelf()

        If Me.GetArea <> -1 Then
            Me.Visible = True
            Me.Bind()
        Else
            Me.Visible = False
        End If
    End Sub

    Public Sub Save()
        Me.CurrentArea = Session.Item("DesignArea")
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPosition(Me.GetArea)
        Dim dt As DataTable = ds.Tables(0)
        Me.dlNavObjects.DataSource = dt
        Dim item As DataListItem
        Dim dr As DataRow
        For Each item In Me.dlNavObjects.Items
            For Each dr In dt.Rows
                If Me.dlNavObjects.DataKeys(item.ItemIndex) = dr.Item("uid") Then
                    Dim sMenuText As String = CType(item.FindControl("ddlLink"), DropDownList).SelectedItem.Text
                    If sMenuText = "Top Level Categories" Then
                        sMenuText = "Categories"
                    End If
                    dr.Item("MenuText") = sMenuText
                    dr.Item("MenuImage") = CType(item.FindControl("txtImage"), TextBox).Text
                    dr.Item("Link") = GetLink(CType(item.FindControl("ddlLink"), DropDownList).SelectedItem)
                    dr.Item("LinkVisibility") = GetLinkVisibility(CType(item.FindControl("txtMenuText"), TextBox).Text)
                End If
            Next
        Next
        myDesignManager.UpdateMenubarPreview(ds)
    End Sub

    Private Sub dlNavObjects_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlNavObjects.DeleteCommand
        Me.CurrentArea = Session.Item("DesignArea")
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPosition(Me.GetArea)
        Dim dt As DataTable = ds.Tables(0)
        Dim item As DataListItem = e.Item
        Dim dr As DataRow
        For Each dr In dt.Rows
            If Me.dlNavObjects.DataKeys(item.ItemIndex) = dr.Item("uid") Then
                dr.Delete()
            End If
        Next
        myDesignManager.UpdateMenubarPreview(ds)
        Me.Bind()
    End Sub



    Public Sub FillDropDown(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        'ddl.Items.Add("Home")
        'ddl.Items.Add("About Us")
        'ddl.Items.Add("Contact Us")
        'ddl.Items.Add("FAQ")
        'ddl.Items.Add("Privacy Policy")
        ddl.Items.Add(New ListItem("Home", "default.aspx"))
        Dim custompages As New ArrayList
        custompages = CCustomPage.GetCustomPagesList(0, 0)
        Dim cpage As CCustomPage
        For Each cpage In custompages
            ddl.Items.Add(New ListItem(cpage.PageTitle, cpage.PageName))
        Next
        'BEGIN: GJV - 8/29/2007 - OSP merge
        If GetArea() = 1 Or GetArea() = 2 Then      ' right and left column
            ddl.Items.Add("All Categories")
        End If
        ddl.Items.Add(New ListItem("Top Level Categories", "Categories"))
        'END: GJV - 8/29/2007 - OSP merge
        ddl.Items.Add("Search")
        ddl.Items.Add("Advanced Search")
        ddl.Items.Add("Wish List")
        ddl.Items.Add("My Account")
        ddl.Items.Add("Checkout")
        ddl.Items.Add("Affiliates")
        ddl.Items.Add("LivePerson")
        ddl.Items.Add("Sign In")
        ddl.Items.Add("Sign Out")
        ddl.Items.Add("Simple Search Control")
        ddl.Items.Add("Shopping Cart Control")
        'Custom Web Pages
        'Dim myDesignManager As New DesignManager
        'Dim customWebPageNames As ArrayList = myDesignManager.GetAllCustomWebPageNames
        'Dim customWebPageName As String
        'For Each customWebPageName In customWebPageNames
        '    If customWebPageName.ToLower <> "home" Then
        '        ddl.Items.Add(customWebPageName)
        '    End If
        'Next
    End Sub

    Private Sub dlNavObjects_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlNavObjects.ItemDataBound
        Dim myddl As DropDownList
        myddl = e.Item.FindControl("ddlLink")
        If Not IsNothing(myddl) Then
            Me.FillDropDown(myddl)
            Me.BindSelected(e.Item)
            'set properties for other items
            Select Case CType(e.Item.FindControl("txtMenuText"), TextBox).Text
                'BEGIN: GJV - 8/29/2007 - OSP merge
                Case "Top Level Categories", "All Categories"
                    'END: GJV - 8/29/2007 - OSP merge
                    CType(e.Item.FindControl("txtImage"), TextBox).Enabled = False
                    CType(e.Item.FindControl("btnBrowse"), ImageButton).Visible = False
                Case "StoreFront Affilliate Link"
                    CType(e.Item.FindControl("txtImage"), TextBox).Enabled = False
                    CType(e.Item.FindControl("btnBrowse"), ImageButton).Visible = False
                Case "Simple Search Control"
                    CType(e.Item.FindControl("txtImage"), TextBox).Enabled = False
                    CType(e.Item.FindControl("btnBrowse"), ImageButton).Visible = False
                Case "Shopping Cart Control"
                    CType(e.Item.FindControl("txtImage"), TextBox).Enabled = False
                    CType(e.Item.FindControl("btnBrowse"), ImageButton).Visible = False
                Case Else
                    CType(e.Item.FindControl("txtImage"), TextBox).Enabled = True
                    CType(e.Item.FindControl("btnBrowse"), ImageButton).Visible = True
            End Select
        End If
    End Sub

    Public Sub BindSelected(ByVal item As DataListItem)
        Dim myddl As DropDownList
        myddl = item.FindControl("ddlLink")
        Dim txtMenuText As TextBox
        txtMenuText = item.FindControl("txtMenuText")
        If txtMenuText.Text = "StoreFront Affilliate Link" Then
            'myddl.SelectedValue = "StoreFront Logo"
            item.Visible = False
        Else
            'myddl.SelectedValue = txtMenuText.Text
            Dim sMenuText As String = txtMenuText.Text
            If sMenuText = "Categories" Then
                sMenuText = "Top Level Categories"
            End If
            For Each li As ListItem In myddl.Items
                li.Selected = li.Text.ToUpper.Equals(sMenuText.ToUpper)
                If li.Selected = True Then
                    Exit For
                End If
            Next li
        End If
    End Sub

    Public Sub UploadImage(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim dlItem As DataListItem = CType(sender, ImageButton).Parent
        CType(dlItem.FindControl("ucUploadImage"), SFExpressUploadControl).Visible = True
        CType(dlItem.FindControl("ucUploadImage"), SFExpressUploadControl).PanelVisible()
        CType(dlItem.FindControl("txtImage"), TextBox).Visible = False
        CType(dlItem.FindControl("btnBrowse"), ImageButton).Visible = False
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdAdd.Click
        Me.CurrentArea = Session.Item("DesignArea")
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPositionOrderDesc(Me.GetArea)
        Dim dt As DataTable = ds.Tables(0)

        Dim NewOrderPosition As Long
        If dt.Rows.Count > 0 Then
            NewOrderPosition = dt.Rows(0).Item("OrderPosition") + 1
        Else
            NewOrderPosition = 0
        End If
        Dim dr As DataRow = dt.NewRow
        dr.Item("MenuText") = Me.ddlLinkType.SelectedItem.Text
        dr.Item("OrderPosition") = NewOrderPosition
        dr.Item("MenuPosition") = Me.GetArea
        dr.Item("MenuImage") = Me.txtImageName.Text
        dr.Item("PageName") = "Default.aspx"
        dr.Item("DesignID") = Me.GetDesignID
        dr.Item("LinkVisibility") = GetLinkVisibility(Me.ddlLinkType.SelectedValue)
        dr.Item("Link") = GetLink(Me.ddlLinkType.SelectedItem)
        dt.Rows.Add(dr)
        myDesignManager.UpdateMenubarPreview(ds)
        Me.Bind()
    End Sub

    Public Function GetLinkVisibility(ByVal Link As String) As Long
        Select Case Link
            Case "Sign In"
                Return 2
            Case "Wish List"
                Return 1
            Case "My Account"
                Return 1
            Case "LivePerson"
                Return 1
            Case "Sign Out"
                Return 1
            Case Else
                Return 0
        End Select
    End Function

    Public Function GetLink(ByVal selectedItem As ListItem) As String
        Select Case selectedItem.Text
            Case "Search"
                Return "search.aspx"
            Case "Checkout"
                Return "shoppingcart.aspx"
            Case "Sign In"
                Return "custsignin.aspx?returnpage=default.aspx"
            Case "Wish List"
                Return "savedcart.aspx"
            Case "My Account"
                Return "CustProfileMain.aspx"
            Case "Sign Out"
                Return "custsignin.aspx?SignOut=1"
            Case "LivePerson"
                Return "ddd"
            Case "Affiliates"
                Return "affiliateaccount.aspx"
            Case "StoreFront Logo"
                Return "http://www.storefront.net"
            Case "Advanced Search"
                Return "search.aspx?Advanced=1"
                'BEGIN: GJV - 8/29/2007 - OSP merge
            Case "Top Level Categories"
                Return "cat-{0}-{1}.aspx"
            Case "All Categories"
                Return "???"
                'END: GJV - 8/29/2007 - OSP merge
            Case "Simple Search Control"
                Return "ddd"
            Case "Shopping Cart Control"
                Return "ddd"
                'Case Else
                '    Dim myCustomWebpage As New CustomWebPage
                '    myCustomWebpage = (New DesignManager).GetByPageName(selectedValue)
                '    Return myCustomWebpage.Path
            Case Else
                Return selectedItem.Value
        End Select
    End Function

    Public Function GetDesignID() As Long
        Dim ds As DataSet = (New DesignManager).GetAllLayoutPreview
        Return ds.Tables(0).Rows(0).Item("DesignID")
    End Function


    Public Sub ddlLinkChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim item As DataListItem = CType(sender, DropDownList).Parent
        If CType(sender, DropDownList).SelectedValue = "StoreFront Logo" Then
            CType(item.FindControl("txtMenuText"), TextBox).Text = "StoreFront Affilliate Link"
        Else
            CType(item.FindControl("txtMenuText"), TextBox).Text = CType(sender, DropDownList).SelectedValue
        End If
        Me.Save()
        Dim myddl As DropDownList
        myddl = item.FindControl("ddlLink")
        '    'Now turn off things people shouldn't change
        Select Case myddl.SelectedValue
            'BEGIN: GJV - 8/29/2007 - OSP merge
        Case "Top Level Categories", "All Categories"
                'END: GJV - 8/29/2007 - OSP merge
                Me.txtImageName.Enabled = False
                CType(item.FindControl("txtImage"), TextBox).Enabled = False
                CType(item.FindControl("btnBrowse"), ImageButton).Visible = False
            Case "StoreFront Logo"
                Me.txtImageName.Enabled = False
                CType(item.FindControl("txtImage"), TextBox).Enabled = False
                CType(item.FindControl("btnBrowse"), ImageButton).Visible = False
            Case "Simple Search Control"
                Me.txtImageName.Enabled = False
                CType(item.FindControl("txtImage"), TextBox).Enabled = False
                CType(item.FindControl("btnBrowse"), ImageButton).Visible = False
            Case "Shopping Cart Control"
                Me.txtImageName.Enabled = False
                CType(item.FindControl("txtImage"), TextBox).Enabled = False
                CType(item.FindControl("btnBrowse"), ImageButton).Visible = False
            Case Else
                Me.txtImageName.Enabled = True
                CType(item.FindControl("txtImage"), TextBox).Enabled = True
                CType(item.FindControl("btnBrowse"), ImageButton).Visible = True
        End Select


    End Sub

    Private Sub dlNavObjects_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlNavObjects.ItemCommand
        Me.CurrentArea = Session.Item("DesignArea")
        If e.CommandName = "MoveUp" Then
            Me.MoveUp(e.Item)
        End If
        If e.CommandName = "MoveDown" Then
            Me.MoveDown(e.Item)
        End If
        If e.CommandSource.GetType Is GetType(LinkButton) AndAlso (CType(e.CommandSource, LinkButton).ID = "uploadBtn" OrElse CType(e.CommandSource, LinkButton).ID = "CmdCancel") Then
            Dim dataListItem As dataListItem = CType(source, DataList).Items(e.Item.ItemIndex)
            CType(dataListItem.FindControl("txtImage"), TextBox).Visible = True
            CType(dataListItem.FindControl("btnBrowse"), ImageButton).Visible = True
        End If
        If e.CommandSource.GetType Is GetType(LinkButton) AndAlso CType(e.CommandSource, LinkButton).ID = "uploadBtn" Then
            If CType(e.CommandSource, LinkButton).CommandName <> String.Empty Then
                Dim myDesignManager As New DesignManager
                Dim dl As DataList = CType(source, DataList)
                Dim itemUid As Long = CLng(dl.DataKeys(e.Item.ItemIndex))
                Dim dsMenuBar As DataSet = myDesignManager.GetAllMenuBarPreviewByUid(itemUid)
                If dsMenuBar.Tables(0).Rows.Count > 0 Then
                    dsMenuBar.Tables(0).Rows(0)("MenuImage") = "Images/" & CType(e.CommandSource, LinkButton).CommandName
                End If
                myDesignManager.UpdateMenubarPreview(dsMenuBar)
                Bind()
            End If
        End If
    End Sub

    Public Sub MoveUp(ByVal item As DataListItem)
        Dim CurrentPosition As Long
        Dim NewPosition As Long
        CurrentPosition = Me.GetPosition(Me.dlNavObjects.DataKeys(item.ItemIndex))
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPositionAndOrderPositionDesc(Me.GetArea, CurrentPosition)
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count < 2 Then
            Exit Sub
        Else
            NewPosition = dt.Rows(1).Item("OrderPosition")
            dt.Rows(1).Item("OrderPosition") = CurrentPosition
            dt.Rows(0).Item("OrderPosition") = NewPosition
            myDesignManager.UpdateMenubarPreview(ds)
            Me.Save()
            Me.Bind()
        End If
    End Sub

    Public Sub MoveDown(ByVal item As DataListItem)
        Dim CurrentPosition As Long
        Dim NewPosition As Long
        CurrentPosition = Me.GetPosition(Me.dlNavObjects.DataKeys(item.ItemIndex))
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPositionAndOrderPositionAsc(Me.GetArea, CurrentPosition)
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count < 2 Then
            Exit Sub
        Else
            NewPosition = dt.Rows(1).Item("OrderPosition")
            dt.Rows(1).Item("OrderPosition") = CurrentPosition
            dt.Rows(0).Item("OrderPosition") = NewPosition
            myDesignManager.UpdateMenubarPreview(ds)
            Me.Save()
            Me.Bind()
        End If
    End Sub

    Public Function GetPosition(ByVal uid As Long) As Long
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByUid(uid)
        Return ds.Tables(0).Rows(0).Item("OrderPosition")
    End Function
End Class
