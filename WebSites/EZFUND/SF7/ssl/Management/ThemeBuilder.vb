Imports StoreFront.BusinessRule
Imports System.io

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Creates a new theme/design and populates the tables with default values.
'Most of the code is straight from the client tools code.
'------------------------------------------------------------------------
'------------------------------------------------------------------------

Public Class ThemeBuilder


    Dim dsImages As New DataSet
    Dim dsMenuBar As New DataSet
    Dim dsLayout As New DataSet

#Region "Add Theme"

    Public Sub AddNewTheme(ByVal ThemeName As String)
        AddDesign(ThemeName)
        Dim DesignID As Long = Me.GetDesignID(ThemeName)
        'Populate the ds's
        Me.FilldsMenuBar(DesignID)

        Me.FilldsImages(DesignID)
        Me.FilldsLayout(DesignID)

        'Copy Defaults
        Me.SetDefaultDesignSettings(DesignID)

        'Commit Changes
        Dim myDesignManager As New DesignManager
        myDesignManager.UpdateImages(Me.dsImages)
        myDesignManager.UpdateLayout(Me.dsLayout)
        myDesignManager.UpdateMenubar(Me.dsMenuBar)

    End Sub

    Public Sub InstallNewTheme(ByVal designds As DataSet, ByVal imagesds As DataSet, ByVal layoutds As DataSet)
        If designds.Tables(0).Rows.Count > 0 Then
            Dim designdr As DataRow = designds.Tables(0).Rows(0)
            AddDesign(designdr.Item("Name"), designdr.Item("Image"), designdr.Item("categories"))
            Dim DesignID As Long = Me.GetDesignID(designdr.Item("Name"))

            'Populate the ds's
            Me.FilldsMenuBar(DesignID)

            Me.FilldsImages(DesignID)
            Me.FilldsLayout(DesignID)

            'Copy Defaults
            Me.SetThemeSettings(DesignID, imagesds, layoutds)

            'Commit Changes
            Dim myDesignManager As New DesignManager
            myDesignManager.UpdateImages(Me.dsImages)
            myDesignManager.UpdateLayout(Me.dsLayout)
            myDesignManager.UpdateMenuBar(Me.dsMenuBar)
        End If
    End Sub

    Public Sub FilldsLayout(ByVal designId As Long)
        Dim myDesignManager As New DesignManager
        Me.dsLayout = myDesignManager.GetAllLayoutByDesignId(designId)
    End Sub

    Public Sub FilldsImages(ByVal designId As Long)
        Dim myDesignManager As New DesignManager
        Me.dsImages = myDesignManager.GetAllImagesByDesignId(designId)
    End Sub

    Public Sub FilldsMenuBar(ByVal designId As Long)
        Dim myDesignManager As New DesignManager
        Me.dsMenuBar = myDesignManager.GetAllMenuBarByDesignId(designId)
    End Sub

    Public Sub AddDesign(ByVal ThemeName As String, Optional ByVal image As String = "custom.jpg", Optional ByVal category As String = "Custom")
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetDesignByName(ThemeName)
        Dim dr As DataRow
        If ds.Tables(0).Rows.Count > 0 Then
            dr = ds.Tables(0).Rows(0)
        Else
            dr = ds.Tables(0).NewRow
            ds.Tables(0).Rows.Add(dr)
        End If
        dr.Item("Name") = ThemeName
        If IsDBNull(dr.Item("IsActive")) Then
            dr.Item("IsActive") = 0
        End If
        dr.Item("Image") = image
        dr.Item("Categories") = category
        myDesignManager.UpdateDesigns(ds)
    End Sub
    Public Function GetDesignID(ByVal ThemeName As String) As Long
        Dim ds As DataSet = (New DesignManager).GetDesignByName(ThemeName)
        Return ds.Tables(0).Rows(0).Item("uid")
    End Function
    Private Sub AddMenuBarRow(ByVal nDesignID As Long, ByVal strMenuText As String, ByVal strLink As String, ByVal nMenuPosition As Long, ByVal nLinkVisibility As Long, ByVal nOrderPosition As Long)
        Dim dr As DataRow
        dr = dsMenuBar.Tables(0).NewRow

        dr("DesignID") = nDesignID
        dr("PageName") = "Default.aspx"
        dr("MenuText") = strMenuText
        dr("MenuImage") = ""
        dr("MenuOffImage") = ""
        dr("Link") = strLink
        dr("MenuPosition") = nMenuPosition
        dr("LinkVisibility") = nLinkVisibility
        dr("OrderPosition") = nOrderPosition

        dsMenuBar.Tables(0).Rows.Add(dr)
    End Sub

    Private Sub AddImageRow(ByVal nDesignID As Long, ByVal strName As String, ByVal strFile As String)
        Dim dr As DataRow

        dr = dsImages.Tables(0).NewRow

        dr("DesignID") = nDesignID
        dr("Name") = strName
        dr("Filename") = strFile

        dsImages.Tables(0).Rows.Add(dr)
    End Sub

    Private Sub AddLayoutRow(ByVal nDesignID As Long, ByVal strName As Object, ByVal strTableWidth As Object, ByVal strCellPadding As Object, ByVal strCellSpacing As Object, ByVal strBorderSize As Object, ByVal strBorderColor As Object, ByVal strHAlignment As Object, ByVal strVAlignment As Object, ByVal nTopMargin As Object, ByVal nRightMargin As Object, ByVal nBottomMargin As Object, ByVal nLeftMargin As Object, ByVal strBGImageURL As Object, ByVal strBGColor As Object, ByVal strFontFace As Object, ByVal nFontSize As Object, ByVal strFontColor As Object, ByVal strFontStyle As Object, ByVal strImageURL As Object, ByVal nDisplayStyle As Object, ByVal nVisible As Object)
        Dim dr As DataRow
        dr = dsLayout.Tables(0).NewRow

        dr("DesignID") = nDesignID
        dr("Name") = strName
        dr("TableWidth") = strTableWidth
        dr("CellPadding") = strCellPadding
        dr("CellSpacing") = strCellSpacing
        dr("BorderSize") = strBorderSize
        dr("BorderColor") = strBorderColor
        dr("HorizontalAlignment") = strHAlignment
        dr("VerticalAlignment") = strVAlignment
        dr("TopMargin") = nTopMargin
        dr("RightMargin") = nRightMargin
        dr("BottomMargin") = nBottomMargin
        dr("LeftMargin") = nLeftMargin
        dr("BackgroundImageURL") = strBGImageURL
        dr("BackgroundColor") = strBGColor
        dr("FontFace") = strFontFace
        dr("FontSize") = nFontSize
        dr("FontColor") = strFontColor
        dr("FontStyle") = strFontStyle
        dr("ImageURL") = strImageURL
        dr("DisplayStyle") = nDisplayStyle
        dr("Visible") = nVisible
        dsLayout.Tables(0).Rows.Add(dr)
    End Sub

    Private Sub SetDefaultDesignSettings(ByVal nDesignID As Long)
        AddDefaultMenuBar(nDesignID)

        AddImageRow(nDesignID, "CreateAccount", "themes/10001/continue.jpg")
        AddImageRow(nDesignID, "ReOrder", "themes/10001/reorder.jpg")
        AddImageRow(nDesignID, "ViewAndPrintReceipt", "themes/10001/viewandprintreceipt.jpg")
        AddImageRow(nDesignID, "CompleteOrder", "themes/10001/complete_order.jpg")
        AddImageRow(nDesignID, "AddAddress", "themes/10001/add_addresses.jpg")
        AddImageRow(nDesignID, "ManageAddresses", "themes/10001/manage_addresses.jpg")
        AddImageRow(nDesignID, "GiftWrap", "themes/10001/gift_wrap.jpg")
        AddImageRow(nDesignID, "SaveCart", "themes/10001/save_cart.jpg")
        AddImageRow(nDesignID, "Close", "themes/10001/close.jpg")
        AddImageRow(nDesignID, "CheckOut", "themes/10001/checkout.jpg")
        AddImageRow(nDesignID, "UpdateQuantity", "themes/10001/update_quantity.jpg")
        AddImageRow(nDesignID, "Apply", "themes/10001/apply.jpg")
        AddImageRow(nDesignID, "AddToSavedCart", "themes/10001/addtosavedcart.jpg")
        AddImageRow(nDesignID, "EmailFriend", "themes/10001/email_friend.jpg")
        AddImageRow(nDesignID, "AddToOrder", "themes/10001/add_to_cart.jpg")
        AddImageRow(nDesignID, "BuyNow", "themes/10001/buy_now.jpg")
        AddImageRow(nDesignID, "Clear", "themes/10001/clear.jpg")
        AddImageRow(nDesignID, "SignOut", "themes/10001/sign_out.jpg")
        AddImageRow(nDesignID, "SignIn", "themes/10001/sign_in.jpg")
        AddImageRow(nDesignID, "Send", "themes/10001/send.jpg")
        AddImageRow(nDesignID, "Track", "themes/10001/track.jpg")
        AddImageRow(nDesignID, "View", "themes/10001/view.jpg")
        AddImageRow(nDesignID, "Continue", "themes/10001/continue.jpg")
        AddImageRow(nDesignID, "Remove", "themes/10001/remove.jpg")
        AddImageRow(nDesignID, "Cancel", "themes/10001/cancel.jpg")
        AddImageRow(nDesignID, "Edit", "themes/10001/edit.jpg")
        AddImageRow(nDesignID, "Delete", "themes/10001/delete.jpg")
        AddImageRow(nDesignID, "Add", "themes/10001/add.jpg")
        AddImageRow(nDesignID, "Save", "themes/10001/save.jpg")
        AddImageRow(nDesignID, "Search", "themes/10001/go.gif")

        Dim oNull As Object = System.DBNull.Value

        AddLayoutRow(nDesignID, "ContentTableHeader", oNull, oNull, oNull, oNull, "#8F8F8F", "Left", oNull, oNull, oNull, oNull, oNull, oNull, "#8F8F8F", "Verdana", 10, "#FFFFFF", "Bold", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "Headings", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, oNull, "#FFFFFF", "Verdana", 10, "#CF0906", "Bold", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "Messages", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, oNull, oNull, "Verdana", 8, "#000000", "Normal", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "Content", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, oNull, "#FFFFFF", "Verdana", 8, "#000000", "Normal", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "Instruction", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, oNull, "#FFFFFF", "Verdana", 8, "#000000", "Normal", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "RightColumn", "100", oNull, oNull, oNull, oNull, "Left", "Top", oNull, oNull, oNull, oNull, oNull, "#E7E7E7", "Verdana", 8, "#000000", "Normal", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "Footer", oNull, oNull, oNull, oNull, oNull, "Center", "Middle", oNull, oNull, oNull, oNull, oNull, "Black", "Verdana", 8, "#FFFFFF", "Bold", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "ErrorMessages", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, oNull, oNull, "Verdana", 8, "Red", "Bold", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "TopSubBanner", oNull, oNull, oNull, oNull, oNull, "Center", oNull, oNull, oNull, oNull, oNull, "themes/10001/topsub_back.jpg", oNull, "Verdana", 8, "#FFFFFF", "Bold", oNull, 2, 1)
        AddLayoutRow(nDesignID, "TopBanner", oNull, oNull, oNull, oNull, oNull, "Left", oNull, oNull, oNull, oNull, oNull, "themes/10001/top_back.jpg", "#FFFFFF", "Verdana", 20, "#000000", "Bold", "themes/10001/store_logo.gif", 1, 1)
        AddLayoutRow(nDesignID, "LeftColumn", "100", oNull, oNull, oNull, oNull, "Left", "Top", oNull, oNull, oNull, oNull, oNull, "#E7E7E7", "Verdana", 8, "#000000", "Normal", oNull, oNull, 1)
        AddLayoutRow(nDesignID, "BodyTable", "766", "2", "0", oNull, "#FFFFFF", "Center", oNull, 15, 0, 0, 0, oNull, "#787777", oNull, oNull, oNull, oNull, oNull, oNull, 1)
    End Sub

    Private Sub SetThemeSettings(ByVal nDesignID As Long, ByVal imagesds As DataSet, ByVal layoutds As DataSet)
        'fill the default menubar
        If dsMenuBar.Tables(0).Rows.Count = 0 Then
            AddDefaultMenuBar(nDesignID)
        End If
        Dim imagesdr As DataRow
        Dim layoutdr As DataRow

        'fill the images table
        If dsImages.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To imagesds.Tables(0).Rows.Count - 1
                dsImages.Tables(0).Rows(i)("DesignID") = nDesignID
                dsImages.Tables(0).Rows(i)("Name") = imagesds.Tables(0).Rows(i)("Name")
                dsImages.Tables(0).Rows(i)("Filename") = imagesds.Tables(0).Rows(i)("Filename")
            Next i
        Else
            For Each imagesdr In imagesds.Tables(0).Rows
                AddImageRow(nDesignID, imagesdr("Name"), imagesdr("Filename"))
            Next
        End If

        'fillthe layout table
        If dsLayout.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To layoutds.Tables(0).Rows.Count - 1

                dsLayout.Tables(0).Rows(i)("DesignID") = nDesignID
                dsLayout.Tables(0).Rows(i)("Name") = layoutds.Tables(0).Rows(i)("Name")
                dsLayout.Tables(0).Rows(i)("TableWidth") = layoutds.Tables(0).Rows(i)("TableWidth")
                dsLayout.Tables(0).Rows(i)("CellPadding") = layoutds.Tables(0).Rows(i)("CellPadding")
                dsLayout.Tables(0).Rows(i)("CellSpacing") = layoutds.Tables(0).Rows(i)("CellSpacing")
                dsLayout.Tables(0).Rows(i)("BorderSize") = layoutds.Tables(0).Rows(i)("BorderSize")
                dsLayout.Tables(0).Rows(i)("BorderColor") = layoutds.Tables(0).Rows(i)("BorderColor")
                dsLayout.Tables(0).Rows(i)("HorizontalAlignment") = layoutds.Tables(0).Rows(i)("HorizontalAlignment")
                dsLayout.Tables(0).Rows(i)("VerticalAlignment") = layoutds.Tables(0).Rows(i)("VerticalAlignment")
                dsLayout.Tables(0).Rows(i)("TopMargin") = layoutds.Tables(0).Rows(i)("TopMargin")
                dsLayout.Tables(0).Rows(i)("RightMargin") = layoutds.Tables(0).Rows(i)("RightMargin")
                dsLayout.Tables(0).Rows(i)("BottomMargin") = layoutds.Tables(0).Rows(i)("BottomMargin")
                dsLayout.Tables(0).Rows(i)("LeftMargin") = layoutds.Tables(0).Rows(i)("LeftMargin")
                dsLayout.Tables(0).Rows(i)("BackgroundImageURL") = layoutds.Tables(0).Rows(i)("BackgroundImageURL")
                dsLayout.Tables(0).Rows(i)("BackgroundColor") = layoutds.Tables(0).Rows(i)("BackgroundColor")
                dsLayout.Tables(0).Rows(i)("FontFace") = layoutds.Tables(0).Rows(i)("FontFace")
                dsLayout.Tables(0).Rows(i)("FontSize") = layoutds.Tables(0).Rows(i)("FontSize")
                dsLayout.Tables(0).Rows(i)("FontColor") = layoutds.Tables(0).Rows(i)("FontColor")
                dsLayout.Tables(0).Rows(i)("FontStyle") = layoutds.Tables(0).Rows(i)("FontStyle")
                dsLayout.Tables(0).Rows(i)("ImageURL") = layoutds.Tables(0).Rows(i)("ImageURL")
                dsLayout.Tables(0).Rows(i)("DisplayStyle") = layoutds.Tables(0).Rows(i)("DisplayStyle")
                dsLayout.Tables(0).Rows(i)("Visible") = layoutds.Tables(0).Rows(i)("Visible")
            Next i
        Else
            For Each layoutdr In layoutds.Tables(0).Rows
                AddLayoutRow(nDesignID, layoutdr("Name"), layoutdr("TableWidth"), layoutdr("CellPadding"), layoutdr("CellSpacing"), layoutdr("BorderSize"), layoutdr("BorderColor"), layoutdr("HorizontalAlignment"), layoutdr("VerticalAlignment"), layoutdr("TopMargin"), layoutdr("RightMargin"), layoutdr("BottomMargin"), layoutdr("LeftMargin"), layoutdr("BackgroundImageURL"), layoutdr("BackgroundColor"), layoutdr("FontFace"), layoutdr("FontSize"), layoutdr("FontColor"), layoutdr("FontStyle"), layoutdr("ImageURL"), layoutdr("DisplayStyle"), layoutdr("Visible"))
            Next
        End If

    End Sub

    Private Sub AddDefaultMenuBar(ByVal nDesignID As Long)
        AddMenuBarRow(nDesignID, "Home", "default.aspx", 0, 0, 0)
        AddMenuBarRow(nDesignID, "Search", "search.aspx", 0, 0, 1)
        AddMenuBarRow(nDesignID, "Checkout", "shoppingcart.aspx", 0, 0, 3)
        AddMenuBarRow(nDesignID, "Sign In", "custsignin.aspx?returnpage=default.aspx", 0, 2, 6)
        AddMenuBarRow(nDesignID, "Wish List", "savedcart.aspx", 0, 1, 4)
        AddMenuBarRow(nDesignID, "Home", "default.aspx", 1, 0, 0)
        AddMenuBarRow(nDesignID, "Search", "search.aspx", 1, 0, 1)
        AddMenuBarRow(nDesignID, "Checkout", "shoppingcart.aspx", 1, 0, 3)
        AddMenuBarRow(nDesignID, "Sign In", "custsignin.aspx?returnpage=default.aspx", 1, 2, 6)
        AddMenuBarRow(nDesignID, "Wish List", "savedcart.aspx", 1, 1, 4)
        AddMenuBarRow(nDesignID, "Home", "default.aspx", 2, 0, 0)
        AddMenuBarRow(nDesignID, "Search", "search.aspx", 2, 0, 1)
        AddMenuBarRow(nDesignID, "Checkout", "shoppingcart.aspx", 2, 0, 3)
        AddMenuBarRow(nDesignID, "Sign In", "custsignin.aspx?returnpage=default.aspx", 2, 2, 6)
        AddMenuBarRow(nDesignID, "Wish List", "savedcart.aspx", 2, 1, 4)
        AddMenuBarRow(nDesignID, "My Account", "CustProfileMain.aspx", 0, 1, 7)
        AddMenuBarRow(nDesignID, "My Account", "CustProfileMain.aspx", 1, 1, 7)
        AddMenuBarRow(nDesignID, "My Account", "CustProfileMain.aspx", 2, 1, 7)
        AddMenuBarRow(nDesignID, "categories", "cat-{0}-{1}.aspx", 0, 0, 2)
        AddMenuBarRow(nDesignID, "categories", "cat-{0}-{1}.aspx", 1, 0, 2)
        AddMenuBarRow(nDesignID, "categories", "cat-{0}-{1}.aspx", 2, 0, 2)
        AddMenuBarRow(nDesignID, "Home", "default.aspx", 3, 0, 0)
        AddMenuBarRow(nDesignID, "Search", "search.aspx", 3, 0, 1)
        AddMenuBarRow(nDesignID, "Checkout", "shoppingcart.aspx", 3, 0, 3)
        AddMenuBarRow(nDesignID, "Sign In", "custsignin.aspx?returnpage=default.aspx", 3, 2, 6)
        AddMenuBarRow(nDesignID, "Wish List", "savedcart.aspx", 3, 1, 4)
        AddMenuBarRow(nDesignID, "My Account", "CustProfileMain.aspx", 3, 1, 7)
        AddMenuBarRow(nDesignID, "Sign Out", "custsignin.aspx?SignOut=1", 0, 1, 8)
        AddMenuBarRow(nDesignID, "Sign Out", "custsignin.aspx?SignOut=1", 1, 1, 8)
        AddMenuBarRow(nDesignID, "Sign Out", "custsignin.aspx?SignOut=1", 2, 1, 8)
        AddMenuBarRow(nDesignID, "Sign Out", "custsignin.aspx?SignOut=1", 3, 1, 8)
        AddMenuBarRow(nDesignID, "LivePerson", "ddd", 0, 1, 9)
        AddMenuBarRow(nDesignID, "LivePerson", "ddd", 1, 1, 9)
        AddMenuBarRow(nDesignID, "LivePerson", "ddd", 2, 1, 9)
        AddMenuBarRow(nDesignID, "LivePerson", "ddd", 3, 1, 9)
        AddMenuBarRow(nDesignID, "Affiliates", "affiliateaccount.aspx", 0, 0, 10)
        AddMenuBarRow(nDesignID, "Affiliates", "affiliateaccount.aspx", 1, 0, 10)
        AddMenuBarRow(nDesignID, "Affiliates", "affiliateaccount.aspx", 2, 0, 10)
        AddMenuBarRow(nDesignID, "Simple Search Control", "ddd", 2, 0, 11)
        AddMenuBarRow(nDesignID, "Shopping Cart Control", "ddd", 2, 0, 12)
    End Sub

#End Region
End Class
