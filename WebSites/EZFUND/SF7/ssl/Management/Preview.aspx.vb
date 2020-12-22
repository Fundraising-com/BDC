Imports StoreFront.BusinessRule

Partial Class Preview
    Inherits System.Web.UI.Page

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
    Protected WithEvents TopBanner1 As PreviewTopBanner
    Protected WithEvents TopSubBanner1 As PreviewTopSubBanner
    Protected WithEvents LeftColumnNav1 As PreviewLeftColumnNav
    Protected WithEvents RightColumnNav1 As PreviewRightColumnNav
    Protected WithEvents Footer1 As PreviewFooterNav

    Private m_strStyleheetPath As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            Dim iDesignId As Integer = CInt(Request.QueryString("id"))
            Dim oDesignManager As New DesignManager

            Dim dsDesigns As DataSet = oDesignManager.GetAllDesigns()
            Dim drDesign As DataRow

            For Each drDesign In dsDesigns.Tables(0).Rows
                If CInt(drDesign("uid")) = iDesignId Then

                    m_strStyleheetPath = drDesign("Path")

                    Exit For
                End If
            Next drDesign

            FillPreviewTables(iDesignId, m_strStyleheetPath)

        End If

        'Put user code to initialize the page here
        If Not IsNothing(Request.QueryString("flag")) AndAlso Request.QueryString("flag").ToLower = "true" Then
            PreviewStyle.InnerText = (New CSSBuilder).getPreviewCss(True)
        Else
            PreviewStyle.InnerText = (New CSSBuilder).getPreviewCss
        End If
        PreviewStyle.InnerText = PreviewStyle.InnerText.Replace("<Style>", "")
        PreviewStyle.InnerText = PreviewStyle.InnerText.Replace("</Style>", "")
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreview
        If ds.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow
            For Each dr In ds.Tables(0).Rows
                Select Case dr("Name").ToString
                    Case "TopBanner"
                        If dr("Visible") <> 1 Then
                            Me.TopBanner1.Visible = False
                        End If
                    Case "TopSubBanner"
                        If dr("Visible") <> 1 Then
                            Me.TopSubBanner1.Visible = False
                        End If
                    Case "LeftColumn"
                        If dr("Visible") <> 1 Then
                            Me.LeftColumnNav1.Visible = False
                            Me.LeftColumnCell.Visible = False
                        End If
                    Case "RightColumn"
                        If dr("Visible") <> 1 Then
                            Me.RightColumnNav1.Visible = False
                            Me.RightColumnCell.Visible = False
                        End If
                    Case "Footer"
                        If dr("Visible") <> 1 Then
                            Me.Footer1.Visible = False
                        End If
                End Select
            Next
        End If
        SetDesign()
    End Sub

    Public ReadOnly Property StylesheetPath() As String
        Get
            Return MyBase.ResolveUrl(m_strStyleheetPath)
        End Get
    End Property

    Private Sub FillPreviewTables(ByVal iDesignId As Integer, ByVal strThemePath As String)

        Dim oDesignManager As New DesignManager
        Dim ds As DataSet

        oDesignManager.DeleteAllImagesPreview()
        ds = oDesignManager.GetAllImagesByDesignId(iDesignId)
        For Each dr As DataRow In ds.Tables(0).Rows
            If Not IsDBNull(dr("Filename")) AndAlso Not dr("Filename").ToString.Trim.Equals(String.Empty) Then
                dr("Filename") = String.Format("{0}images/{1}", strThemePath, dr("Filename"))
            End If
        Next dr
        oDesignManager.UpdateImagesPreviewByDataset(ds)

        oDesignManager.DeleteAllLayoutPreview()
        ds = oDesignManager.GetAllLayoutByDesignId(iDesignId)
        For Each dr As DataRow In ds.Tables(0).Rows
            If Not IsDBNull(dr("ImageURL")) AndAlso Not dr("ImageURL").ToString.Trim.Equals(String.Empty) Then
                dr("ImageURL") = String.Format("{0}images/{1}", strThemePath, dr("ImageURL"))
            End If
        Next dr
        oDesignManager.UpdateLayoutPreviewByDataSet(ds)

        oDesignManager.DeleteAllMenuBarPreview()
        ds = oDesignManager.GetAllMenuBarByDesignId(iDesignId)
        oDesignManager.UpdateMenubarPreviewByDataSet(ds)

    End Sub

#Region "Sub SetDesign()"
    Protected Sub SetDesign()
        Dim myDesignManager As New DesignManager
        Dim ds As DataSet = myDesignManager.GetAllLayoutPreviewByAreaName("BodyTable")
        Dim TempCell As HtmlTableCell
        'General Settings
        If ds.Tables(0).Rows.Count > 0 Then
            If (IsNothing(PageSubTable) = False) Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("CellPadding")) Then
                    PageSubTable.CellPadding = ds.Tables(0).Rows(0)("CellPadding")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("CellSpacing")) Then
                    PageSubTable.CellSpacing = ds.Tables(0).Rows(0)("CellSpacing")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                    PageSubTable.Width = ds.Tables(0).Rows(0)("TableWidth")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("BorderColor")) Then
                    PageSubTable.BorderColor = ds.Tables(0).Rows(0)("BorderColor")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("BorderSize")) Then
                    PageSubTable.Border = ds.Tables(0).Rows(0)("BorderSize")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("HorizontalAlignment")) Then
                    PageSubTable.Align = ds.Tables(0).Rows(0)("HorizontalAlignment")
                End If
            End If
            If (IsNothing(PageCell) = False) Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("HorizontalAlignment")) Then
                    PageCell.Align = ds.Tables(0).Rows(0)("HorizontalAlignment")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("VerticalAlignment")) Then
                    PageCell.VAlign = ds.Tables(0).Rows(0)("VerticalAlignment")
                End If
            End If
            Dim tempLWidth As String = ""
            Dim tempRWidth As String = ""
            'TopBanner
            If IsNothing(PageSubTable) = False Then
                If (IsNothing(PageSubTable.FindControl("TopBannerCell")) = False) Then
                    ds = myDesignManager.GetAllLayoutPreviewByAreaName("TopBanner")
                    If ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Rows(0)("Visible") = False Then
                        CType(PageSubTable.FindControl("TopBannerCell"), HtmlTableCell).Visible = False
                    End If
                    'TopSubBanner
                    ds = myDesignManager.GetAllLayoutPreviewByAreaName("TopSubBanner")
                    If ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Rows(0)("Visible") = False Then
                        CType(PageSubTable.FindControl("TopSubBannerCell"), HtmlTableCell).Visible = False
                    End If
                    'LeftColumn
                    ds = myDesignManager.GetAllLayoutPreviewByAreaName("LeftColumn")
                    If ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Rows(0)("Visible") = False Then
                        If (IsNothing(PageSubTable.FindControl("LeftColumnCell")) = False) Then
                            CType(PageSubTable.FindControl("LeftColumnCell"), HtmlTableCell).Visible = False
                        End If
                        tempLWidth = "0%"
                    Else
                        TempCell = CType(PageSubTable.FindControl("LeftColumnCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) AndAlso Not IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                            TempCell.Style.Add("width", ds.Tables(0).Rows(0)("TableWidth"))
                            TempCell.Width = ds.Tables(0).Rows(0)("TableWidth")
                            If (IsNothing(TempCell.FindControl("LeftColumnNav1")) = False) Then
                                If (IsNothing(TempCell.FindControl("LeftColumnNav1").FindControl("Table1")) = False) Then
                                    CType(TempCell.FindControl("LeftColumnNav1").FindControl("Table1"), HtmlTable).Style.Add("width", ds.Tables(0).Rows(0)("TableWidth"))
                                    CType(TempCell.FindControl("LeftColumnNav1").FindControl("Table1"), HtmlTable).Width = ds.Tables(0).Rows(0)("TableWidth")
                                End If
                            End If
                        End If
                        If IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                            tempLWidth = "0%"
                        Else
                            tempLWidth = ds.Tables(0).Rows(0)("TableWidth")
                        End If
                        TempCell = Nothing
                    End If
                    'RightColumn
                    ds = myDesignManager.GetAllLayoutPreviewByAreaName("RightColumn")
                    If ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Rows(0)("Visible") = False Then
                        CType(PageSubTable.FindControl("RightColumnCell"), HtmlTableCell).Visible = IsNothing(PageSubTable.FindControl("RightColumnCell"))
                        tempRWidth = "0%"
                    Else
                        TempCell = CType(PageSubTable.FindControl("RightColumnCell"), HtmlTableCell)
                        If (IsNothing(TempCell) = False) AndAlso Not IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                            TempCell.Style.Add("width", ds.Tables(0).Rows(0)("TableWidth"))
                            TempCell.Width = ds.Tables(0).Rows(0)("TableWidth")
                            If (IsNothing(TempCell.FindControl("RightColumnNav1")) = False) Then
                                If (IsNothing(TempCell.FindControl("RightColumnNav1").FindControl("Table1")) = False) Then
                                    CType(TempCell.FindControl("RightColumnNav1").FindControl("Table1"), HtmlTable).Style.Add("width", ds.Tables(0).Rows(0)("TableWidth"))
                                    CType(TempCell.FindControl("RightColumnNav1").FindControl("Table1"), HtmlTable).Width = ds.Tables(0).Rows(0)("TableWidth")
                                End If
                            End If
                        End If
                        If IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                            tempRWidth = "0%"
                        Else
                            tempRWidth = ds.Tables(0).Rows(0)("TableWidth")
                        End If
                        TempCell = Nothing
                    End If
                End If
                'Footer
                ds = myDesignManager.GetAllLayoutPreviewByAreaName("Footer")
                If ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Rows(0)("Visible") = False Then
                    CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell).Visible = False
                Else
                    TempCell = CType(PageSubTable.FindControl("FooterCell"), HtmlTableCell)
                    If (IsNothing(TempCell) = False) AndAlso Not IsDBNull(ds.Tables(0).Rows(0)("TableWidth")) Then
                        TempCell.Width = ds.Tables(0).Rows(0)("TableWidth")
                        TempCell.Style.Add("width", ds.Tables(0).Rows(0)("TableWidth"))
                    End If
                    TempCell = Nothing
                End If
                'Content
                If Not ((tempLWidth Is Nothing) Or (tempRWidth Is Nothing)) Then
                    Dim tempWidth As String
                    If tempLWidth.EndsWith("%") And tempRWidth.EndsWith("%") Then
                        tempWidth = CStr(100 - CDbl(tempLWidth.Replace("%", "")) - CDbl(tempRWidth.Replace("%", ""))) & "%"
                        If (IsNothing(FindControl("ContentCell")) = False) Then CType(FindControl("ContentCell"), HtmlTableCell).Width = tempWidth
                        If (IsNothing(FindControl("tdContent")) = False) Then CType(FindControl("tdContent"), HtmlTableCell).Width = tempWidth
                    ElseIf Not PageSubTable.Width.EndsWith("%") Then
                        If Not tempLWidth.EndsWith("%") AndAlso Not tempRWidth.EndsWith("%") Then
                            tempWidth = CStr(CDbl(PageSubTable.Width) - CDbl(tempLWidth) - CDbl(tempRWidth))
                            If (IsNothing(FindControl("ContentCell")) = False) Then CType(FindControl("ContentCell"), HtmlTableCell).Width = tempWidth
                            If (IsNothing(FindControl("tdContent")) = False) Then CType(FindControl("tdContent"), HtmlTableCell).Width = tempWidth
                        End If
                    End If
                End If
            End If
        End If
    End Sub
#End Region
End Class
