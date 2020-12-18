Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class PreviewMenuBar
    Inherits System.Web.UI.UserControl

    Protected WithEvents Stuff2 As HtmlTableRow
    Public Enum Display As Integer
        iHorizontal = 0
        iVertical = 1
    End Enum
    Private m_strStyle As String
    Public imgpath As String = ""
    Private m_sCallPage As String
    Private mCurrentRegion As String = "TopSubBanner"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"
#Region "Property CallPage"
    Public Property CallPage() As String
        Get
            Return m_sCallPage
        End Get
        Set(ByVal Value As String)
            m_sCallPage = Value
        End Set
    End Property
#End Region

#Region "Property StyleClass() As String"
    Public Property StyleClass() As String
        Get
            Return m_strStyle
        End Get
        Set(ByVal Value As String)
            m_strStyle = Value
        End Set
    End Property
#End Region

#Region "Property CurrentRegion"
    Public Property CurrentRegion() As String
        Get
            Return mCurrentRegion
        End Get
        Set(ByVal Value As String)
            mCurrentRegion = Value
        End Set
    End Property
#End Region
#End Region

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        LoadNav()
    End Sub
#End Region

#Region "Sub LoadNav()"
    Private Sub LoadNav()
        If IsNothing(m_strStyle) Then
            m_strStyle = ""
        End If
        dlMenu.ItemStyle.CssClass = m_strStyle
        If (CallPage = "top") Then
            dlMenu.RepeatDirection = RepeatDirection.Horizontal
        Else
            dlMenu.RepeatDirection = RepeatDirection.Vertical
            dlMenu.Width = New Unit("100%")
        End If
        Dim m_objItems As New ArrayList
        m_objItems = LoadMenus()
        dlMenu.DataSource = m_objItems
        dlMenu.DataBind()
    End Sub
#End Region

#Region "Private Function LoadMenus() As ArrayList"
    Private Function LoadMenus() As ArrayList
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllMenuBarPreviewByMenuPositionAndOrderPositionAsc(GetRegion, 0)
        Dim arList As New ArrayList
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow
            For Each dr In ds.Tables(0).Rows
                Dim objItem As New CMenuItem
                If Not IsDBNull(dr("MenuText")) Then
                    objItem.DisplayName = dr("MenuText")
                End If
                If Not IsDBNull(dr("MenuImage")) AndAlso dr("MenuImage") <> "" Then
                    objItem.ImageOFF = StoreFrontConfiguration.SSLPath & dr("MenuImage")
                End If
                If Not IsDBNull(dr("MenuOffImage")) AndAlso dr("MenuOffImage") <> "" Then
                    objItem.ImageON = StoreFrontConfiguration.SSLPath & dr("MenuOffImage")
                End If
                If Not IsDBNull(dr("MenuImage")) AndAlso dr("MenuImage") <> "" Then
                    objItem.MenuImage = StoreFrontConfiguration.SSLPath & dr("MenuImage")
                End If
                If Not IsDBNull(dr("MenuPosition")) Then
                    objItem.Level = dr("MenuPosition")
                End If
                arList.Add(objItem)
            Next
            Return arList
        End If
        Return Nothing
    End Function
#End Region

#Region "Private Sub dlMenu_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMenu.ItemCreated"
    Private Sub dlMenu_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMenu.ItemCreated
        Dim objCell As HtmlTableCell

        If (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then
            If (dlMenu.RepeatDirection = RepeatDirection.Horizontal) Then
                CType(e.Item.FindControl("TopBarSpacer"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("BottomBarSpacer"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("Row5"), HtmlTableRow).Visible = False
                CType(e.Item.FindControl("TopBar"), HtmlTableRow).Visible = False
                If (e.Item.ItemIndex > 0) Then
                    CType(e.Item.FindControl("LeftBar"), HtmlTableCell).Visible = False
                    CType(e.Item.FindControl("TopBarCell"), HtmlTableCell).ColSpan = 4
                    CType(e.Item.FindControl("BottomBarCell"), HtmlTableCell).ColSpan = 4
                End If
            End If
            If (dlMenu.RepeatDirection = RepeatDirection.Vertical) Then
                objCell = e.Item.FindControl("Stuff4")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("Stuff5")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("Column11")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column21")
                objCell.Visible = False
                objCell = e.Item.FindControl("LeftBar")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column22")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column13")
                objCell.Visible = False
                objCell = e.Item.FindControl("Column23")
                objCell.Visible = False
                If (e.Item.ItemIndex > 0) Then
                    CType(e.Item.FindControl("TopBar"), HtmlTableRow).Visible = False
                End If
            End If

            If (IsNothing(m_strStyle) = False) Then
                CType(e.Item.FindControl("PageLink"), Label).CssClass = m_strStyle
                objCell = e.Item.FindControl("Stuff")
                objCell.Attributes.Add("class", m_strStyle)
                objCell = e.Item.FindControl("Stuff2")
                objCell.Attributes.Add("class", m_strStyle)
                objCell.NoWrap = False
                objCell = e.Item.FindControl("Stuff3")
                objCell.Attributes.Add("class", m_strStyle)
            End If

            Dim objItem As CMenuItem = e.Item.DataItem
            If (IsNothing(objItem) = False) Then
                If (objItem.DisplayName.ToLower() = "liveperson" And StoreFrontConfiguration.LivePersonID <> "") Then
                    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = True
                    CType(e.Item.FindControl("LivePerson1"), LivePerson).LivePersonAccount = StoreFrontConfiguration.LivePersonID
                    CType(e.Item.FindControl("PageLink"), Label).Visible = False
                    CType(e.Item.FindControl("PageLink2"), Label).Visible = False
                    If Not IsNothing(e.Item.FindControl("CartList1")) Then
                        CType(e.Item.FindControl("CartList1"), CartList).Visible = False
                    End If
                    If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                        CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False
                    End If
                ElseIf (objItem.DisplayName.ToLower() = "storefront affilliate link") Then
                    CType(e.Item.FindControl("PageLink"), Label).Visible = False
                    CType(e.Item.FindControl("PageLink2"), Label).Visible = True
                    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                    If Not IsNothing(e.Item.FindControl("CartList1")) Then
                        CType(e.Item.FindControl("CartList1"), CartList).Visible = False
                    End If
                    If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                        CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False
                    End If
                    imgpath = StoreFrontConfiguration.SSLPath & "images/sfaffiliate.jpg"
                ElseIf (objItem.DisplayName.ToLower() = "shopping cart control") Then
                    CType(e.Item.FindControl("PageLink"), Label).Visible = False
                    CType(e.Item.FindControl("PageLink2"), Label).Visible = False
                    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                    If Not IsNothing(e.Item.FindControl("CartList1")) Then
                        CType(e.Item.FindControl("CartList1"), CartList).Visible = True
                        If objItem.Level = 0 Then
                            CType(e.Item.FindControl("CartList1"), CartList).CssCls = "TopSubBannerText"
                        ElseIf objItem.Level = 1 Then
                            CType(e.Item.FindControl("CartList1"), CartList).CssCls = "LeftColumnText"
                        ElseIf objItem.Level = 2 Then
                            CType(e.Item.FindControl("CartList1"), CartList).CssCls = "RightColumnText"
                        ElseIf objItem.Level = 3 Then
                            CType(e.Item.FindControl("CartList1"), CartList).CssCls = "FooterText"
                        Else
                            CType(e.Item.FindControl("CartList1"), CartList).CssCls = "Content"
                        End If
                    End If
                    If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                        CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False

                    End If
                ElseIf (objItem.DisplayName.ToLower() = "simple search control") Then
                    CType(e.Item.FindControl("PageLink"), Label).Visible = False
                    CType(e.Item.FindControl("PageLink2"), Label).Visible = False
                    CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                    If Not IsNothing(e.Item.FindControl("CartList1")) Then
                        CType(e.Item.FindControl("CartList1"), CartList).Visible = False
                    End If
                    If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                        CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = True
                        CType(CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).FindControl("txtSimpleSearch"), TextBox).Enabled = False
                        CType(CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).FindControl("btnSearch"), LinkButton).Enabled = False
                        If objItem.Level = 0 Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "TopSubBannerText"
                        ElseIf objItem.Level = 1 Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "LeftColumnText"
                        ElseIf objItem.Level = 2 Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "RightColumnText"
                        ElseIf objItem.Level = 3 Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "FooterText"
                        Else
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).CssCls = "Content"
                        End If
                    End If
                Else
                    If objItem.MenuImage = "" Then
                        CType(e.Item.FindControl("PageLink"), Label).Visible = True
                        CType(e.Item.FindControl("PageLink2"), Label).Visible = False
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        If Not IsNothing(e.Item.FindControl("CartList1")) Then
                            CType(e.Item.FindControl("CartList1"), CartList).Visible = False
                        End If
                        If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False
                        End If
                    Else
                        CType(e.Item.FindControl("PageLink"), Label).Visible = False
                        imgpath = objItem.MenuImage
                        CType(e.Item.FindControl("PageLink2"), Label).Visible = True
                        CType(e.Item.FindControl("LivePerson1"), LivePerson).Visible = False
                        If Not IsNothing(e.Item.FindControl("CartList1")) Then
                            CType(e.Item.FindControl("CartList1"), CartList).Visible = False
                        End If
                        If Not IsNothing(e.Item.FindControl("SimpleSearch1")) Then
                            CType(e.Item.FindControl("SimpleSearch1"), SimpleSearch).Visible = False
                        End If
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "Private Function GetRegion() As Long"
    Private Function GetRegion() As Long
        Select Case CurrentRegion
            Case "TopSubBanner"
                Return 0
            Case "LeftColumnNav"
                Return 1
            Case "RightColumnNav"
                Return 2
            Case "FooterNav"
                Return 3
        End Select

    End Function
#End Region
End Class
