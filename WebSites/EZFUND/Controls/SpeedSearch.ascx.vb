Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public MustInherit Class SpeedSearch
    Inherits CSearchResultBase
    Protected WithEvents lblKeyword As System.Web.UI.WebControls.Label
    Protected WithEvents lblCount As System.Web.UI.WebControls.Label
    Protected WithEvents lblProducts As System.Web.UI.WebControls.Label
    Protected WithEvents ResultInfo As System.Web.UI.WebControls.Panel
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Private dt As DataTable
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategoryName As System.Web.UI.WebControls.Label
    Private sElasped As String
    Dim dr As DataRowView
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
        If Me.Visible = False Then Exit Sub
        MyBase.LoadSettings()
        If Not IsPostBack Then

            DataGrid1.PageSize = m_nRows
            DataGrid1.AllowCustomPaging = True
            m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
            objStorage = SetSearch()
            Dim sTime As Date = Now
            dt = m_objSearchEngine.SpeedSearch(objStorage)
            DataGrid1.VirtualItemCount = m_objSearchEngine.ResultsCount
            Dim eTime As Date = Now
            sElasped = " IN:" & eTime.Subtract(sTime).Seconds & " Seconds  and "
            sElasped = sElasped & eTime.Subtract(sTime).Milliseconds & " MiliSeconds"
            If dt.Rows.Count = 0 Then
                'EmptySearchMsg
            Else
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
            End If
            SetSearchInfo()
            objStorage = Nothing
            dt = Nothing
            m_objSearchEngine = Nothing
        End If

       

    End Sub


    Public ReadOnly Property DetailLinkDisplay() As String
        Get
            Dim sReturn As String = ""
            If Not IsNothing(dr) Then
                If dr("ImageSmallPath").ToString.Trim.Length > 0 Then
                    sReturn = "<img border=0 src='" & dr("ImageSmallPath") & "'>"
                End If
            End If
            Return sReturn
        End Get
    End Property

    Public ReadOnly Property ProductPrice() As String
        Get
            Dim sReturn As String = ""
            If Not IsNothing(dr) Then
                sReturn = Me.PriceDisplay2(CDec("0" & dr("Price").ToString))
            End If
            Return sReturn
        End Get
    End Property

    Public ReadOnly Property DetailLink() As String
        Get
            Dim sReturn As String = ""
            If Not IsNothing(dr) Then
                sReturn = StoreFrontConfiguration.SiteURL & "Detail.aspx?ID=" & dr("uid").ToString
            End If
            Return sReturn
        End Get
    End Property



#Region "Private Sub SetSearchInfo()"

    Private Sub SetSearchInfo()
        If (objStorage.Keyword.Trim() = "") Then
            lblKeyword.Text = "All"
        Else
            lblKeyword.Text = objStorage.Keyword
        End If
        If (objStorage.CategoryID = -1) Or (objStorage.CategoryID = -0) Then
            Dim strCatLabel As String
            strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
            lblCategoryName.Text = "All " & strCatLabel
        Else
            'Get Category Name'
            'SP1 Change
            Dim objCategoryAccess As New CSearchResult()
            lblCategoryName.Text = objCategoryAccess.GetCategoryName(objStorage.CategoryID)
        End If
        If (DataGrid1.PageCount = 1) Then
            DataGrid1.PagerStyle.Visible = False
        End If
        If (IsNothing(dt) = False) Then
            lblCount.Text = " " & m_objSearchEngine.ResultsCount
            If (dt.Rows.Count = 1) Then
                lblProducts.Text = "Product" & " " & sElasped
            Else
                lblProducts.Text = "Products" & " " & sElasped
            End If
        End If
    End Sub

#End Region

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If Not IsNothing(e.Item.DataItem) Then
            dr = CType(e.Item.DataItem, DataRowView)
        End If
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton()

            If (DataGrid1.CurrentPageIndex > 0) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (DataGrid1.CurrentPageIndex < DataGrid1.PageCount - 1) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton()
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        End If

    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged

        objStorage = Session("Search")
        m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)

        DataGrid1.AllowCustomPaging = True

        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.DataSource = m_objSearchEngine.SpeedSearch(objStorage, e.NewPageIndex)
        DataGrid1.VirtualItemCount = m_objSearchEngine.ResultsCount
        DataGrid1.DataBind()
        SetSearchInfo()
        objStorage = Nothing
        dt = Nothing
        m_objSearchEngine = Nothing
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then

            Dim objButton As LinkButton = e.CommandSource
            objStorage = Session("Search")

            m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
            DataGrid1.AllowCustomPaging = True
            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex - 1
            End If
            DataGrid1.DataSource = m_objSearchEngine.SpeedSearch(objStorage, DataGrid1.CurrentPageIndex)
            DataGrid1.VirtualItemCount = m_objSearchEngine.ResultsCount
            DataGrid1.DataBind()
            SetSearchInfo()
            objStorage = Nothing
            dt = Nothing
            m_objSearchEngine = Nothing
        End If
    End Sub
End Class
