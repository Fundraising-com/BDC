Imports StoreFront.BusinessRule

Partial Class SearchFiltersControl
    Inherits CWebControl

    Private Const SearchFiltersPerRow As Integer = 3
    Private FilterResultsButton As New LinkButton

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        FilterResultsButton.ID = "btnFilterResults"
        FilterResultsButton.Text = "Filter Results"
        AddHandler FilterResultsButton.Click, AddressOf Me.btnFilterResults_Click
        Dim oImage As New System.Web.UI.WebControls.Image
        oImage.BorderWidth = New Unit(0)
        oImage.ID = "imgFilterResults"
        oImage.AlternateText = "Filter Results"
        FilterResultsButton.Controls.Add(oImage)
        Me.Controls.Add(Me.FilterResultsButton)
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oImage As Control = Me.FindControl("imgFilterResults")
        If Not IsNothing(oImage) Then
            CType(oImage, System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("FilterResults").Attributes("Filepath").Value
        End If
    End Sub

    Public Event FilterResults As EventHandler
    Private Sub btnFilterResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent FilterResults(sender, e)
    End Sub

    Public Function GetSearchFilters() As Hashtable
        Dim oDropDownLists As Hashtable
        If Not IsNothing(Session("DropDownLists")) Then
            oDropDownLists = Session("DropDownLists")
        Else
            oDropDownLists = New Hashtable
        End If
        Dim oSearchFilters As New Hashtable
        For Each sKey As String In oDropDownLists.Keys
            Dim sValue As String = Request.Form(oDropDownLists(sKey))
            If Not IsNothing(sValue) AndAlso sValue.Length > 0 Then
                oSearchFilters.Add(sKey, sValue)
            End If
        Next
        Session.Remove("DropDownLists")
        Return oSearchFilters
    End Function

    Public Sub CreateControls(ByVal oSearchFilterValues As Hashtable)
        Me.Controls.Remove(Me.FilterResultsButton)

        Dim oAttributeManagement As New CAttributeManagement
        Dim iDropDownIndex As Integer = 1

        Dim oContentRow As HtmlTableRow = Nothing
        Dim iSearchFiltersAdded As Integer = 0
        Dim iSearchFilterIndex As Integer = 0
        Dim oDropDownLists As New Hashtable
        For Each sKey As String In oSearchFilterValues.Keys
            Dim asKeyParts As String() = sKey.Split(Chr(29))
            Dim sAttributeName As String = asKeyParts(0)
            Dim sGlobalSelectorName As String = asKeyParts(1)

            If iSearchFiltersAdded = 0 Then
                If iSearchFilterIndex = 0 Then
                    Me.AddHeaderFooterRow()
                    Me.AddSpacerRow()
                End If

                oContentRow = New HtmlTableRow
                ' note: left border cell
                oContentRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, True))
                ' note: left spacer cell
                oContentRow.Cells.Add(Me.GetNewContentTableCell("Content", 1, False))
            End If

            ' note: drop down list
            Dim oCell As New HtmlTableCell
            oCell.Attributes.Add("Class", "Content")
            oCell.Width = Me.GetCellWidth(oSearchFilterValues.Keys.Count)
            Dim oDropDown As New DropDownList
            oDropDown.ID = "ddl" + iDropDownIndex.ToString
            iDropDownIndex += 1
            oDropDown.Items.Add(New ListItem(sGlobalSelectorName, ""))

            For Each sSearchFilterValue As String In oSearchFilterValues(sKey).ToString.Split(Chr(29))
                oDropDown.Items.Add(New ListItem(sSearchFilterValue))
            Next
            Dim sClientId As String = Me.ClientID.Replace("_", ":") + ":" + oDropDown.ClientID
            For Each sRequestKey As String In Request.Form.AllKeys
                If sRequestKey = sClientId Then
                    oDropDown.SelectedValue = Request.Form(sRequestKey)
                    Exit For
                End If
            Next
            oCell.Controls.Add(oDropDown)
            oContentRow.Cells.Add(oCell)

            oDropDownLists.Add(sAttributeName, sClientId)
            iSearchFiltersAdded += 1

            If iSearchFiltersAdded = SearchFiltersPerRow Or iSearchFilterIndex = (oSearchFilterValues.Keys.Count - 1) Then
                If iSearchFilterIndex = (oSearchFilterValues.Keys.Count - 1) Then
                    For iBlankCellIndex As Integer = iSearchFiltersAdded To SearchFiltersPerRow - 1
                        oContentRow.Cells.Add(Me.GetNewContentTableCell("Content", 1, False))
                    Next
                    ' note: add button
                    Dim oButtonCell As New HtmlTableCell
                    oButtonCell.Attributes.Add("Class", "Content")
                    oButtonCell.Width = Me.GetCellWidth(oSearchFilterValues.Keys.Count)

                    oButtonCell.Controls.Add(Me.FilterResultsButton)
                    oContentRow.Cells.Add(oButtonCell)
                Else
                    ' note: add blank cell in button column
                    oContentRow.Cells.Add(Me.GetNewContentTableCell("Content", 1, False))
                End If

                ' note: right spacer cell
                oContentRow.Cells.Add(Me.GetNewContentTableCell("Content", 1, False))
                ' note: right border cell
                oContentRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, True))
                Me.tblSearchFilters.Rows.Add(oContentRow)
                Me.AddSpacerRow()

                If iSearchFilterIndex = (oSearchFilterValues.Keys.Count - 1) Then
                    Me.AddHeaderFooterRow()
                End If

                iSearchFiltersAdded = 0
            End If
            iSearchFilterIndex += 1
        Next
        Session("DropDownLists") = oDropDownLists
    End Sub

    Private Function GetCellWidth(ByVal iRowCount As Integer) As String
        Dim iColumnCount As Integer
        If iRowCount < SearchFiltersPerRow Then
            iColumnCount = iRowCount
        Else
            iColumnCount = SearchFiltersPerRow
        End If
        ' note: add a column for the button
        iColumnCount += 1
        Dim iColumnWidth As Integer = 100 / iColumnCount
        Return iColumnWidth.ToString + "%"
    End Function

    Private Sub AddSpacerRow()
        Dim oHeaderFooterRow As New HtmlTableRow
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, False))
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("Content", SearchFiltersPerRow + 3, False))
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, False))
        Me.tblSearchFilters.Rows.Add(oHeaderFooterRow)
    End Sub

    Private Sub AddHeaderFooterRow()
        Dim oHeaderFooterRow As New HtmlTableRow
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, True))
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", SearchFiltersPerRow + 3, True))
        oHeaderFooterRow.Cells.Add(Me.GetNewContentTableCell("ContentTable", 1, True))
        Me.tblSearchFilters.Rows.Add(oHeaderFooterRow)
    End Sub

    Private Function GetNewContentTableCell(ByVal sStyleName As String, ByVal iColumnSpan As Integer, ByVal bUseImage As Boolean) As HtmlTableCell
        Dim oCell As New HtmlTableCell
        oCell.Attributes.Add("Class", sStyleName)
        oCell.Width = "1px"
        oCell.Style.Add("width", "1px")
        oCell.ColSpan = iColumnSpan
        If bUseImage Then
            Dim oImage As New HtmlImage
            oImage.Height = 1
            oImage.Src = "images/clear.gif"
            oCell.Controls.Add(oImage)
        Else
            oCell.InnerHtml = "&nbsp;"
        End If
        Return oCell
    End Function
End Class
