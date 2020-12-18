Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management

Public MustInherit Class StandardSearchLive
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
    Protected WithEvents rpAlpha As System.Web.UI.WebControls.Repeater
    Private m_DeleteMessage
    Private m_objStorage As CProdManageSearch
    Private m_objManagement As CProductManagement
    Private m_Dt As DataTable

#Region "Class Events"
    Event EditClick As EventHandler
    Event DeleteClick As EventHandler
    Event SortClick As EventHandler
    Event AlphaClick As EventHandler
    Event CheckClick As EventHandler
    Event EmptyResults As EventHandler
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

#Region "Public Property DeleteMessage() As String"

	 Public Property DeleteMessage() As String
        Get
            Return CStr(ViewState("DeleteMSG"))
        End Get
        Set(ByVal Value As String)
            ViewState("DeleteMSG") = Value
        End Set
    End Property

#End Region

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtKeyword.Attributes.Add("onkeydown", "enterKeyPressLive(event);")
        If (IsPostBack = False) Then
            BuildAlphabet()
            BindProds()
        End If

    End Sub

#End Region


#Region "Public Sub AlphaSearch(ByVal sender As Object, ByVal e As System.EventArgs)"

	   Public Sub AlphaSearch(ByVal sender As Object, ByVal e As System.EventArgs)
        m_objStorage = New CProdManageSearch()
        Dim linkBtn As LinkButton = CType(sender, LinkButton)
        If linkBtn.CommandArgument.Length = 1 Then
            m_objStorage.ALPHA = linkBtn.CommandArgument
        End If
        DataGrid2.CurrentPageIndex = 0
        BindProds()
    End Sub

#End Region

#Region "Sub BuildAlphabet()"
    Private Sub BuildAlphabet()
        ' Dim strList As String = "#|,A|,B|,C|,D|,E|,F|,G|,H|,I|,J|,K|,L|,M|,N|,O|,P|,Q|,R|,S|,T|,U|,V|,W|,X|,Y|,Z|,All"
        Dim strList As String = "#,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,ALL"
        Dim sAlpha As String
        Dim i As Integer
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim drLive As DataRow
        dt.Columns.Add(New DataColumn("sAlpha", GetType(String)))
        For i = 0 To strList.Split(",").Length - 1
            sAlpha = strList.Split(",")(i)
            dr = dt.NewRow()
            dr("sAlpha") = sAlpha
            dt.Rows.Add(dr)
        Next
        rpAlpha.DataSource = dt
        rpAlpha.DataBind()
    End Sub
#End Region

#Region "Edit Item"
    '-----------------------------------------------------------
    ' Sub EditItem
    ' Parameters: Object, EventArgs
    ' Return: Nothing
    ' Description:
    '   Raise the EditClick Event passing the Given value for ButtonID
    '-----------------------------------------------------------
    Public Sub EditItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim olinkBt As LinkButton = CType(sender, LinkButton)
        RaiseEvent EditClick(olinkBt.CommandArgument, EventArgs.Empty)
    End Sub
#End Region

#Region "Delete Item"
    '-----------------------------------------------------------
    ' Sub DeleteItem
    ' Parameters: Object, EventArgs
    ' Return: Nothing
    ' Description:
    '   Raise the DeleteClick Event passing the Given value for ButtonID
    '-----------------------------------------------------------
    Public Sub DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim olinkBt As LinkButton = CType(sender, LinkButton)
        RaiseEvent DeleteClick(olinkBt.CommandArgument, EventArgs.Empty)
    End Sub
#End Region

#Region "Sub DataGrid2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemCreated"
    '-----------------------------------------------------------
    ' DataGrid2_ItemCreated
    ' Parameters: Object, System.Web.UI.WebControls.DataGridItemEventArgs
    ' Return: Nothing
    ' Description:
    '   Toggles visible elements and dynamicly creates the columns
    '-----------------------------------------------------------
    Private Sub DataGrid2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemCreated
        Dim objButton As LinkButton
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            objButton = New LinkButton()

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
        Else
            objButton = e.Item.FindControl("btnDelete")
            If IsNothing(objButton) = False Then
                objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & Me.DeleteMessage & "');")
            End If
        End If
    End Sub
#End Region

#Region "Sub ReloadList()"
    Public Sub ReloadList()
        BindProds()
    End Sub
#End Region

#Region "Private Sub BindProds(Optional ByVal PageIndex As Long = 0)"

    Private Sub BindProds(Optional ByVal PageIndex As Long = 0)
        If IsNothing(m_objStorage) Then
            m_objStorage = Session("m_objStorage")
            If IsNothing(m_objStorage) Then
                m_objStorage = New CProdManageSearch()
            End If
        End If
        Session("m_objStorage") = m_objStorage
        m_objManagement = New CProductManagement()
        m_Dt = m_objManagement.ManagementSearch(m_objStorage, PageIndex)
        DataGrid2.AllowCustomPaging = True
        DataGrid2.VirtualItemCount = m_objManagement.ResultsCount
        If m_objManagement.ResultsCount = 0 Then
            RaiseEvent EmptyResults(m_objStorage, EventArgs.Empty)
        End If
        DataGrid2.DataSource = m_Dt
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
    End Sub

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
            BindProds(DataGrid2.CurrentPageIndex)
        End If
    End Sub
#End Region

#Region "Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged"
    Private Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        DataGrid2.CurrentPageIndex = e.NewPageIndex
        BindProds(e.NewPageIndex)
    End Sub
#End Region

#Region "Public Sub GoSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click"

    Public Sub GoSearch(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If txtKeyword.Text.Trim.Length > 0 Then

                m_objStorage = New CProdManageSearch()

                If IsNumeric(txtKeyword.Text.Trim) Then
                    m_objStorage.UID = txtKeyword.Text.Trim
                Else
                    Dim sSearch As String
                    sSearch = Replace(txtKeyword.Text.Trim, "'", "''")
                    sSearch = Replace(sSearch, "\", " ")
                    m_objStorage.ProductName = sSearch
                End If
                DataGrid2.CurrentPageIndex = 0
                BindProds()
            End If
        Catch err As SystemException

        End Try
       
    End Sub


#End Region

End Class
