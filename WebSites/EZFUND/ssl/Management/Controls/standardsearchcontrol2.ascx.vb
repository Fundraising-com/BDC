Imports StoreFront.SystemBase
Public MustInherit Class standardsearchcontrol2
    Inherits CWebControl

    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblSortBy As System.Web.UI.WebControls.Label
    Protected WithEvents SortCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents searchTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblSearch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKeyword As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGo As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DataGrid2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tblSearch As System.Web.UI.HtmlControls.HtmlTable

    Private m_objStorage As CSearchControlStorage

#Region "Class Events"
    Event SortClick As EventHandler
    Event AlphaClick As EventHandler
    Event CheckClick As EventHandler
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Me.Visible = False) Then
            Exit Sub
        End If
        m_objStorage = StorageClass()
        DataGrid2.AllowPaging = m_objStorage.Paging
        DataGrid2.PagerStyle.CssClass = m_objStorage.PagerClass
        Dim obj As Object
        Dim cnt As Integer
        cnt = 0
        For Each obj In m_objStorage.DataSource
            cnt = cnt + 1
            Exit For
        Next
        If (IsPostBack = False) Then
            If cnt > 0 Then
                DataGrid2.DataSource = DoSearching()
            End If
            DataGrid2.DataBind()
        Else
            'If (m_objStorage.SelectOne) Then
            Session("SearchStorage2") = m_objStorage
            DataGrid2.DataSource = DoSearching()
            DataGrid2.DataBind()
            ' End If
        End If
        BuildAlphabet()

        If (DataGrid2.PageCount = 1) Then
            DataGrid2.PagerStyle.Visible = False
            'tblSearch.Visible = False
            SortCell.InnerHtml = "&nbsp;"
            SortCell.InnerText = ""
        Else
            DataGrid2.PagerStyle.Visible = True
            tblSearch.Visible = True
        End If
        txtKeyword.Attributes.Add("onkeydown", "enterKeyPress(event);")
    End Sub

    Public WriteOnly Property CategoryNodes() As Boolean
        Set(ByVal Value As Boolean)
            Session("CategoryNodes") = Value
        End Set
    End Property

#Region "Function DoSearching() As Object"
    Private Function DoSearching() As Object
        Dim _type As Type
        Dim obj As Object
        Dim objData As Object
        Dim str As String
        Dim ar As New ArrayList()

        Select Case m_objStorage.SearchingType
            Case CSearchControlStorage.SearchType.Keyword
                If (m_objStorage.SearchValue = "") Then
                    Return SortBy(0, m_objStorage.DataSource)
                End If
                ' Sort by Column 0
                For Each obj In m_objStorage.DataSource
                    _type = obj.GetType()
                    For Each str In m_objStorage.ColumnList
                        objData = _type.InvokeMember(str, Reflection.BindingFlags.GetProperty, Nothing, obj, Nothing, Nothing)
                        If (objData.ToString().ToLower.IndexOf(m_objStorage.SearchValue.ToLower) <> -1) Then
                            ar.Add(obj)
                            Exit For
                        End If
                    Next
                Next

                Return SortBy(0, ar)
            Case CSearchControlStorage.SearchType.Alpha
                ' Clicked the All
                If (m_objStorage.SearchValue = "All") Then
                    ' Sort by Column 0
                    Return SortBy(0, m_objStorage.DataSource)
                End If

                If (m_objStorage.SearchValue = "#") Then
                    str = m_objStorage.ColumnList.Item(0)
                    For Each obj In m_objStorage.DataSource
                        _type = obj.GetType()
                        objData = _type.InvokeMember(str, Reflection.BindingFlags.GetProperty, Nothing, obj, Nothing, Nothing)

                        If (objData.ToString().Substring(0, 1) = "0" Or _
                            objData.ToString().Substring(0, 1) = "1" Or _
                            objData.ToString().Substring(0, 1) = "2" Or _
                            objData.ToString().Substring(0, 1) = "3" Or _
                            objData.ToString().Substring(0, 1) = "4" Or _
                            objData.ToString().Substring(0, 1) = "5" Or _
                            objData.ToString().Substring(0, 1) = "6" Or _
                            objData.ToString().Substring(0, 1) = "7" Or _
                            objData.ToString().Substring(0, 1) = "8" Or _
                            objData.ToString().Substring(0, 1) = "9") Then
                            ar.Add(obj)
                        End If
                    Next

                    Return SortBy(0, ar)
                End If

                str = m_objStorage.ColumnList.Item(0)

                ' Alpha Character Used
                Dim strValue As String
                Dim bAddNode As Boolean = False
                For Each obj In m_objStorage.DataSource
                    _type = obj.GetType()
                    For Each str In m_objStorage.ColumnList
                        objData = _type.InvokeMember(str, Reflection.BindingFlags.GetProperty, Nothing, obj, Nothing, Nothing)
                        strValue = CType(objData, String)
                        If (strValue.ToLower.IndexOf(m_objStorage.SearchValue.ToLower) = 0) Then
                            ar.Add(obj)
                            bAddNode = True
                            Exit For
                        Else
                            If (IsNothing(Session("CategoryNodes")) = False) Then
                                If (Session("CategoryNodes") = True) Then
                                    If (bAddNode = True And strValue.StartsWith("...") = True) Then
                                        ar.Add(obj)
                                        Exit For
                                    Else
                                        bAddNode = False
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next

                Return SortBy(0, ar)
            Case CSearchControlStorage.SearchType.Sort
                Dim nIndex As Long = 0
                Dim nCounter As Long = 0
                Dim sortStorage As CSortStorage = m_objStorage.SearchValue

                For Each str In m_objStorage.ColumnList
                    If (sortStorage.ColumnName = str) Then
                        nIndex = nCounter
                        Exit For
                    End If
                    nCounter = nCounter + 1
                Next

                If (IsNothing(Session("LastSearch2")) = True) Then
                    Return SortBy(nIndex, m_objStorage.DataSource)
                Else
                    Return SortBy(nIndex, Session("LastSearch2"))
                End If
        End Select
    End Function
#End Region

#Region "Function SortBy(ByVal nIndex As Long, ByVal objDataSource As Object)"
    Private Function SortBy(ByVal nIndex As Long, ByVal objDataSource As Object)

        If (m_objStorage.NoSort) Then
            Return objDataSource
        End If

        Dim strColumn As String = m_objStorage.ColumnList.Item(nIndex)
        Dim obj As Object
        Dim ar As New ArrayList()

        For Each obj In objDataSource
            ar.Add(obj)
        Next

        If (ar.Count > 0) Then
            quicksort(ar, strColumn, 0, ar.Count - 1)
        End If

        Return ar
    End Function
#End Region

#Region "Sub quicksort(ByRef array As ArrayList, ByVal strColumn As String, ByVal left As Long, ByVal right As Long)"
    Private Sub quicksort(ByRef array As ArrayList, ByVal strColumn As String, ByVal left As Long, ByVal right As Long)
        Dim _type As Type = array.Item(0).GetType
        Dim i As Long
        Dim j As Long
        Dim x As Object
        Dim objData As Object

        i = left
        j = right
        x = array.Item((left + right) / 2)

        Do While (i <= j)
            objData = _type.InvokeMember(strColumn, Reflection.BindingFlags.GetProperty, Nothing, x, Nothing, Nothing)

            Do While (i < right And _type.InvokeMember(strColumn, Reflection.BindingFlags.GetProperty, Nothing, array.Item(i), Nothing, Nothing) < objData)
                i = i + 1
            Loop

            Do While (j > left And _type.InvokeMember(strColumn, Reflection.BindingFlags.GetProperty, Nothing, array.Item(j), Nothing, Nothing) > objData)
                j = j - 1
            Loop

            If (i <= j) Then
                swap(array, i, j)
                i = i + 1
                j = j - 1
            End If
        Loop

        If (left < j) Then
            quicksort(array, strColumn, left, j)
        End If
        If (i < right) Then
            quicksort(array, strColumn, i, right)
        End If
    End Sub
#End Region

#Region "Sub swap(ByRef array As ArrayList, ByVal i As Long, ByVal j As Long)"
    Private Sub swap(ByRef array As ArrayList, ByVal i As Long, ByVal j As Long)
        Dim temp As Object
        temp = array.Item(i)
        array.Item(i) = array.Item(j)
        array.Item(j) = temp
    End Sub
#End Region

#Region "Sub BuildAlphabet()"
    Private Sub BuildAlphabet()
        Dim strList As String = "#,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"
        Dim _type As Type
        Dim obj As Object
        Dim objData As Object
        Dim str As String
        Dim strValue As String
        Dim _space As Label
        Dim nCounter As Long = 0
        Dim _td As HtmlTableCell = FindControl("AlphaCell")

        Dim _link As LinkButton
        Dim _lbl As Label

        Dim objCol As New Collection()

        For Each str In strList.Split(",")
            objCol.Add("0", str.ToUpper())
        Next

        str = m_objStorage.ColumnList.Item(0)

        For Each obj In m_objStorage.DataSource
            _type = obj.GetType()
            objData = _type.InvokeMember(str, Reflection.BindingFlags.GetProperty, Nothing, obj, Nothing, Nothing)
            If objData Is Nothing Then
                objData = "_"
            ElseIf (objData = "") Then
                objData = "_"
            End If
            strValue = CType(objData, String)
            If (strValue.Substring(0, 1) = "0" Or _
                strValue.Substring(0, 1) = "1" Or _
                strValue.Substring(0, 1) = "2" Or _
                strValue.Substring(0, 1) = "3" Or _
                strValue.Substring(0, 1) = "4" Or _
                strValue.Substring(0, 1) = "5" Or _
                strValue.Substring(0, 1) = "6" Or _
                strValue.Substring(0, 1) = "7" Or _
                strValue.Substring(0, 1) = "8" Or _
                strValue.Substring(0, 1) = "9") Then
                objCol.Remove("#")
                objCol.Add("1", "#")
            Else
                Try
                    objCol.Remove(strValue.Substring(0, 1).ToUpper())
                    objCol.Add("1", strValue.Substring(0, 1).ToUpper())
                Catch objerr As Exception
                End Try
            End If
        Next

        For Each str In strList.Split(",")
            If (objCol.Item(str) = "0") Then
                _lbl = New Label()
                _lbl.Text = str
                _td.Controls.Add(_lbl)
            Else
                _link = New LinkButton()
                _link.Text = str
                _link.ID = "lnkAlpha" & nCounter
                _link.CommandName = str
                _td.Controls.Add(_link)
            End If

            _space = New Label()
            _space.Text = "&nbsp;|&nbsp;"

            _td.Controls.Add(_space)

            nCounter = nCounter + 1
        Next

        ' Add the all to the end
        _link = New LinkButton()
        _link.Text = "All"
        _link.ID = "lnkAlphaAll"
        _link.CommandName = "All"
        _td.Controls.Add(_link)
    End Sub
#End Region

#Region "Sub GoSearch(ByVal sender As Object, ByVal e As System.EventArgs) handles btnGo.Click"
    Public Sub GoSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        m_objStorage = StorageClass()
        m_objStorage.SearchValue = txtKeyword.Text
        m_objStorage.SearchingType = CSearchControlStorage.SearchType.Keyword
        Session("SearchStorage2") = m_objStorage
        DataGrid2.DataSource = DoSearching()
        DataGrid2.DataBind()
    End Sub
#End Region

#Region "Property StorageClass() As CSearchControlStorage"
    '-----------------------------------------------------------
    ' Property StorageClass
    ' Parameters: None
    ' Return: CSearchControlStorage
    ' Description:
    '   Property for the storage class which will set visible properties
    '-----------------------------------------------------------
    Public Property StorageClass() As CSearchControlStorage
        Get
            If (IsNothing(m_objStorage) = True) Then
                m_objStorage = Session("SearchStorage2")
            End If
            Return m_objStorage
        End Get
        Set(ByVal Value As CSearchControlStorage)
            m_objStorage = Value

            lblTitle.Text = m_objStorage.TitleString
            lblTitle.CssClass = m_objStorage.TitleClass

            If (m_objStorage.Sorting = False) Then
                lblSortBy.Visible = False
            Else
                Dim str As CSortStorage
                Dim _link As LinkButton
                Dim _space As Label
                Dim nCounter As Long = 0
                Dim ctrl As System.Web.UI.Control
                Dim bLinksExist As Boolean = False
                For Each ctrl In SortCell.Controls
                    If Left(ctrl.ID, 7) = "lnkSort" Then
                        bLinksExist = True
                    End If
                Next
                If Not bLinksExist Then
                    For Each str In m_objStorage.SortList
                        _link = New LinkButton()
                        _link.CausesValidation = True
                        _link.Text = str.DisplayName
                        _link.ID = "lnkSort" & nCounter
                        _link.CommandName = nCounter

                        If (IsNothing(SortCell) = False) Then
                            SortCell.Controls.Add(_link)
                        End If

                        _space = New Label()
                        _space.Text = "&nbsp;|&nbsp;"

                        If (nCounter < m_objStorage.SortList.Count - 1) Then
                            If (IsNothing(SortCell) = False) Then
                                SortCell.Controls.Add(_space)
                            End If
                        End If

                        nCounter = nCounter + 1
                    Next
                    SortCell.Attributes.Add("Class", m_objStorage.TitleClass)
                End If
            End If

            Session("SearchStorage2") = m_objStorage
        End Set
    End Property
#End Region

    Public Sub CheckChange(ByVal sender As Object, ByVal e As EventArgs)
        Dim objItem As DataGridItem
        Dim _type As Type
        Dim objData As Object
        Dim objControl As Object

        If (m_objStorage.SelectOne) Then
            Session("ArrChecked") = New ArrayList()

            Dim bcheck As Boolean = CType(sender, CheckBox).Checked

            For Each objItem In DataGrid2.Items
                objData = objItem.FindControl("Table1").FindControl("chkCell")
                For Each objControl In objData.Controls
                    If (IsNothing(objControl.ID) = False) Then
                        If (objControl.ID <> "") Then
                            If (objControl.ID.IndexOf("chk") = 0) Then
                                CType(objControl, CheckBox).Checked = False
                            End If
                        End If
                    End If
                Next
            Next
            Dim str2 As String = sender.ID

            If (bcheck) Then
                str2 = str2.Substring(3)
                Session("ArrChecked").Add(CLng(str2))
                CType(sender, CheckBox).Checked = True
            End If
        Else
            If CType(sender, CheckBox).Checked = True Then
                Session("ArrChecked").add(CLng((sender.ID).substring(3)))
            Else
                Session("ArrChecked").Remove(CLng((sender.ID).substring(3)))
            End If
        End If

        Dim str As String = sender.ID

        str = str.Substring(3)

        RaiseEvent CheckClick(str, EventArgs.Empty)

        'CType(sender, CheckBox).Checked = True
    End Sub

#Region "Sub DataGrid2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemCreated"
    '-----------------------------------------------------------
    ' DataGrid2_ItemCreated
    ' Parameters: Object, System.Web.UI.WebControls.DataGridItemEventArgs
    ' Return: Nothing
    ' Description:
    '   Toggles visible elements and dynamicly creates the columns
    '-----------------------------------------------------------
    Private Sub DataGrid2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton()

            If (DataGrid2.CurrentPageIndex > 0) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (DataGrid2.CurrentPageIndex < DataGrid2.PageCount - 1) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton()
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        ElseIf (e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item) Then
            If (IsNothing(e.Item.DataItem) = False) Then
                m_objStorage = StorageClass()

                Dim str As String
                Dim _tr As HtmlTableRow = e.Item.FindControl("trInner")
                Dim _td As HtmlTableCell
                Dim _type As Type = e.Item.DataItem.GetType()
                Dim i As Long = 0
                Dim nWidth As Decimal = (100 / m_objStorage.ColumnList.Count)
                Dim objData As Object

                objData = _type.InvokeMember(m_objStorage.ButtonID, Reflection.BindingFlags.GetProperty, Nothing, e.Item.DataItem, Nothing, Nothing)
                CType(e.Item.FindControl("txtID"), TextBox).Text = objData
                If Session("ArrChecked").contains(CLng(objData)) Then
                    CType(e.Item.FindControl("chk"), CheckBox).Checked = True
                End If

                CType(e.Item.FindControl("chk"), CheckBox).ID = "chk" & objData

                For Each str In m_objStorage.ColumnList
                    _td = New HtmlTableCell()
                    _td.Attributes.Add("Class", m_objStorage.ContentClass)
                    _td.Align = "left"
                    _td.Width = nWidth & "%"

                    objData = _type.InvokeMember(str, Reflection.BindingFlags.GetProperty, Nothing, e.Item.DataItem, Nothing, Nothing)
                    If (objData.GetType() Is GetType(Decimal)) Then
                        _td.InnerText = Format(objData, "C")
                    Else
                        _td.InnerText = objData
                    End If

                    _tr.Cells.Insert(i, _td)
                    i = i + 1
                Next
            End If
        End If
    End Sub
#End Region

#Region "Sub ReloadList()"
    Public Sub ReloadList()
        DataGrid2.DataSource = DoSearching()
        DataGrid2.DataBind()
        BuildAlphabet()
        If (DataGrid2.PageCount = 1) Then
            DataGrid2.PagerStyle.Visible = False
            'tblSearch.Visible = False
            SortCell.InnerHtml = "&nbsp;"
            SortCell.InnerText = ""
        Else
            DataGrid2.PagerStyle.Visible = True
            tblSearch.Visible = True
        End If
    End Sub
#End Region

#Region "Overrides Function OnBubbleEvent(ByVal source As Object, ByVal args As System.EventArgs) As Boolean"
    Protected Overrides Function OnBubbleEvent(ByVal source As Object, ByVal args As System.EventArgs) As Boolean
        m_objStorage = StorageClass()
        If (source.GetType Is GetType(LinkButton)) Then
            Dim _link As LinkButton = source
            If (_link.ID.IndexOf("lnkSort") <> -1) Then
                m_objStorage.SearchingType = CSearchControlStorage.SearchType.Sort
                m_objStorage.SearchValue = m_objStorage.SortList.Item(CLng(source.CommandName))
                Session("SearchStorage2") = m_objStorage
                RaiseEvent SortClick(source.CommandName, EventArgs.Empty)
            ElseIf (_link.ID.IndexOf("lnkAlpha") <> -1) Then
                m_objStorage.SearchingType = CSearchControlStorage.SearchType.Alpha
                m_objStorage.SearchValue = _link.CommandName
                Session("SearchStorage2") = m_objStorage
                RaiseEvent AlphaClick(source.CommandName, EventArgs.Empty)
            End If
            DataGrid2.CurrentPageIndex = 0
            DataGrid2.DataSource = DoSearching()
            DataGrid2.DataBind()

            If (DataGrid2.PageCount = 1) Then
                DataGrid2.PagerStyle.Visible = False
                'tblSearch.Visible = False
                SortCell.InnerHtml = "&nbsp;"
                SortCell.InnerText = ""
            Else
                DataGrid2.PagerStyle.Visible = True
                tblSearch.Visible = True
            End If
            Session("LastSearch2") = DataGrid2.DataSource
        End If
    End Function
#End Region

#Region "Sub DataGrid2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.ItemCommand"
    Private Sub DataGrid2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource

            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid2.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid2.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex - 1
            End If
            StorageClass = Session("SearchStorage2")
            DataGrid2.DataSource = DoSearching()
            DataGrid2.DataBind()
        End If
    End Sub
#End Region

#Region "Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged"
    Private Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        DataGrid2.CurrentPageIndex = e.NewPageIndex
        DataGrid2.DataSource = DoSearching()
        DataGrid2.DataBind()
    End Sub
#End Region
End Class
