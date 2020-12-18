Imports StoreFront.BusinessRule.Management
Public MustInherit Class ProductCategoriesControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton

    Protected WithEvents ProdUID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ShortDescription As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents LongDescription As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents Categories As System.Web.UI.WebControls.Repeater
    Private objProdManagement As CProductManagement
    Private m_uid As Long
    Private lblProdName As Label
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
        'Put user code to initialize the page here
        If IsPostBack = True Then
            m_uid = ProdUID.Value

        Else
            m_uid = Request.QueryString("ID")
            If m_uid = 0 Then
                m_uid = Session("ProductId")
            Else
                Session("ProductId") = m_uid
            End If

            objProdManagement = New CProductManagement(m_uid)
            ProdUID.Value = m_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            Call bindCategoryList()
        End If

    End Sub

    Private Sub bindCategoryList()
        Dim x As Integer
        Dim CatList As ArrayList
        Dim ActiveCatsDT As DataTable
        Dim dr As DataRow
        Dim DataListItem As RepeaterItem
        Dim ActiveChkBox As CheckBox
        ActiveCatsDT = objProdManagement.GetAllProductCategoryRecords
        CatList = objProdManagement.GetAllCategories
        Categories.DataSource = CatList
        Categories.DataBind()
        For x = 0 To CatList.Count - 1
            For Each dr In ActiveCatsDT.Rows
                If CatList(x).ID = dr.Item("CategoryID") Then
                    DataListItem = Categories.Items(x)
                    ActiveChkBox = CType(DataListItem.FindControl("Active"), CheckBox)
                    ActiveChkBox.Checked = True
                End If
            Next
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim _item As RepeaterItem
        Dim Active As Boolean
        Dim dt As New DataTable()
        Dim dr As DataRow
        objProdManagement = New CProductManagement(m_uid)
        dt.Columns.Add(New DataColumn("ProductID", GetType(String)))
        dt.Columns.Add(New DataColumn("CategoryID", GetType(String)))

        For Each _item In Categories.Items
            Active = CType(_item.FindControl("Active"), CheckBox).Checked
            If Active = True Then
                dr = dt.NewRow()
                dr(0) = m_uid
                dr(1) = CType(_item.FindControl("CatUID"), HtmlControls.HtmlInputHidden).Value
                dt.Rows.Add(dr)
            End If
        Next
        objProdManagement.updateProductCategories(dt)
    End Sub
End Class
