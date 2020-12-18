Imports StoreFront.BusinessRule.Management
Public MustInherit Class ProductMarketingControl
    Inherits System.Web.UI.UserControl
    Protected WithEvents DealTime As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ProdUID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents UpsellMessage As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents Keywords As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents cmdRelatedProducts As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable

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
    Private lblProdName As Label
    Private M_uid As Long
    Private objProdManagement As CProductManagement

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsPostBack = True Then
            M_uid = ProdUID.Value

        Else
            M_uid = Request.QueryString("ID")
            If M_uid = 0 Then
                M_uid = Session("ProductId")
            Else
                Session("ProductId") = M_uid
            End If

            objProdManagement = New CProductManagement(M_uid)
            ProdUID.Value = M_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            If Request.QueryString("loadStored") = 1 Then
                Dim StoredMarketplaceValues As ArrayList
                StoredMarketplaceValues = Session("StoredMarketplaceValues")
                DealTime.Checked = StoredMarketplaceValues(0)
                'MarketPlaceManager.Checked = StoredMarketplaceValues(1)
                'update #2169
                UpsellMessage.Value = StoredMarketplaceValues(1)
                Keywords.Value = StoredMarketplaceValues(2)
                Session("StoredMarketplaceValues") = Nothing
                Session("ApplyTo") = Nothing
                Session("ReturnPage") = Nothing
                If (IsNothing(Session("ArrChecked")) = False) Then
                    objProdManagement.updateRelatedProducts(Session("ArrChecked"))
                End If
                Session("ArrChecked") = Nothing
            Else

                DealTime.Checked = objProdManagement.DealtimeIsActive
                'MarketPlaceManager.Checked = objProdManagement.MarketplaceManagerIsActive
                UpsellMessage.Value = objProdManagement.UpsellMessage
                Keywords.Value = objProdManagement.Keywords
            End If

            End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objProdManagement = New CProductManagement(M_uid)

        lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
        lblProdName.Text = objProdManagement.Name
        objProdManagement.DealtimeIsActive = DealTime.Checked
        'objProdManagement.MarketplaceManagerIsActive = MarketPlaceManager.Checked
        objProdManagement.UpsellMessage = Request.Form("ProductMarketing:UpsellMessage")
        objProdManagement.Keywords = Keywords.Value
        objProdManagement.update()
    End Sub

    Private Sub cmdRelatedProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRelatedProducts.Click
        objProdManagement = New CProductManagement(M_uid)
        Dim StoredMarketplaceValues As New ArrayList()
        Dim dt As DataTable
        Dim dr As DataRow
        Dim RelatedItemsAL As New ArrayList()
        Dim RelatedID As Long
        dt = objProdManagement.getRelatedProducts()
        For Each dr In dt.Rows
            RelatedID = CLng(dr.Item("RelatedID"))
            RelatedItemsAL.Add(RelatedID)
        Next

        StoredMarketplaceValues.Add(DealTime.Checked)
        'StoredMarketplaceValues.Add(MarketPlaceManager.Checked)
        StoredMarketplaceValues.Add(UpsellMessage.Value)
        StoredMarketplaceValues.Add(Keywords.Value)
        Session("StoredMarketplaceValues") = StoredMarketplaceValues
        Session("ApplyTo") = "5"
        Session("ReturnPage") = "productmarketing.aspx?loadStored=1"
        Session("ArrChecked") = RelatedItemsAL
        Session("ProdID") = M_uid
        Response.Redirect("multiselect.aspx")
    End Sub
End Class
