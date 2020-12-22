Imports storefront.systembase
Imports StoreFront.BusinessRule.Management
Partial  Class ProductDiscountsControl
    Inherits CWebControl

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
    Private objProdManagement As CProductManagement
    Private lblProdName As Label
    'Protected WithEvents DiscountType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Download As System.Web.UI.WebControls.CheckBox
    Protected WithEvents FileName As System.Web.UI.WebControls.TextBox
    Protected WithEvents MultipleDownload As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Expires As System.Web.UI.WebControls.DropDownList
    Private M_uid As Long

#Region "Properties"
    Public ReadOnly Property Price() As String
        Get
            If objProdManagement.ProductType = ProductType.Customized OrElse _
            objProdManagement.ProductType = ProductType.CustomizedSubscription Then
                Return objProdManagement.PriceRange
            End If
            Return Format(objProdManagement.Price, "c")
        End Get
    End Property

    Public ReadOnly Property Cost() As String
        Get
            If objProdManagement.ProductType = ProductType.Customized OrElse _
            objProdManagement.ProductType = ProductType.CustomizedSubscription Then
                Return objProdManagement.CostRange
            End If
            Return Format(objProdManagement.Cost, "c")
        End Get
    End Property
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Tee 7/16/2007 added script
        ActivateSale.Attributes.Add("OnClick", "ValidateExclusive(this);")
        cbClearance.Attributes.Add("OnClick", "ValidateExclusive(this);")
        'end Tee
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            VolumeDiscounts.Visible = False
        End If

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
            'Tee 8/7/2007 product configurator
            MakeCommonVisible(objProdManagement.ProductType, False)
            'end Tee
            ProdUID.Value = M_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            'Tee 7/16/2007 set values
            If objProdManagement.SaleIsActive Then
                If objProdManagement.SaleType = 0 Then
                    ActivateSale.Checked = True
                    SalePrice.Text = objProdManagement.SalePrice
                    cbClearance.Checked = False
                    tbClearance.Text = ""
                ElseIf objProdManagement.SaleType = 1 Then
                    cbClearance.Checked = True
                    tbClearance.Text = objProdManagement.SalePrice
                    ActivateSale.Checked = False
                    SalePrice.Text = ""
                End If
            End If
            hidListPrice.Value = objProdManagement.Price
            hidCost.Value = objProdManagement.Cost
            'end Tee
            'Call loadDiscountTypesDD()
            If (VolumeDiscounts.Visible) Then
                loadVolumeDiscounts()
            End If
            DataBind()
        End If
    End Sub

    Private Sub loadDiscountTypesDD(ByVal DiscountType As DropDownList)
        Dim dt As DataTable

        'Dim x As Integer
        dt = objProdManagement.getDiscountTypesDT
        DiscountType.DataValueField = "ID"
        DiscountType.DataTextField = "Display"



        If objProdManagement.DiscountType = 0 Then
            DiscountType.SelectedIndex = 1
            Dim dt2 As New DataTable
            Dim dr As DataRow
            dt2.Columns.Add(New DataColumn("ID", GetType(String)))
            dt2.Columns.Add(New DataColumn("Display", GetType(String)))

            dr = dt2.NewRow()
            dr(0) = dt.Rows(1)(0)
            dr(1) = dt.Rows(1)(1)
            dt2.Rows.Add(dr)

            dr = dt2.NewRow()
            dr(0) = dt.Rows(0)(0)
            dr(1) = dt.Rows(0)(1)
            dt2.Rows.Add(dr)

            DiscountType.DataSource = dt2
        Else
            'DiscountType.SelectedIndex = 0
            DiscountType.DataSource = dt
        End If
        DiscountType.DataBind()
    End Sub

    Private Sub loadVolumeDiscounts()
        Dim dt As DataTable
        Dim dtNew As New DataTable
        Dim x As Integer
        Dim dr As DataRow
        Dim dr2 As DataRow
        dt = objProdManagement.getVolumeDiscountsDT

        dtNew.Columns.Add(New DataColumn("BreakLevel", GetType(String)))
        dtNew.Columns.Add(New DataColumn("Amount", GetType(String)))

        For Each dr In dt.Rows
            dr2 = dtNew.NewRow
            dr2(0) = dr.Item("BreakLevel")
            If dr.Item("DollarOrPercent").ToString = "1" Then
                dr2(1) = Convert.ToDecimal(dr.Item("Amount")) * 100
            Else
                dr2(1) = Convert.ToDecimal(dr.Item("Amount"))
            End If
            dtNew.Rows.Add(dr2)
        Next


        x = dtNew.Rows.Count
        Do While x < 7
            x = x + 1
            dr2 = dtNew.NewRow
            dr2(0) = ""
            dr2(1) = ""
            dtNew.Rows.Add(dr2)
        Loop

        VolumeDiscounts.DataSource = dtNew
        VolumeDiscounts.DataBind()

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim _item As RepeaterItem
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim breaklevel As String
        objProdManagement = New CProductManagement(M_uid)
        'Tee 7/16/2007 set value
        objProdManagement.SaleIsActive = ActivateSale.Checked OrElse cbClearance.Checked
        If ActivateSale.Checked Then
            objProdManagement.SalePrice = SalePrice.Text
            objProdManagement.SaleType = 0
        ElseIf cbClearance.Checked Then
            objProdManagement.SalePrice = tbClearance.Text
            objProdManagement.SaleType = 1
        End If
        'end Tee
        If (VolumeDiscounts.Visible) Then
            objProdManagement.DiscountType = CType(VolumeDiscounts.Controls(0).FindControl("DiscountType"), DropDownList).SelectedItem.Value
            objProdManagement.update()
            dt.Columns.Add(New DataColumn("BreakLevel", GetType(String)))
            dt.Columns.Add(New DataColumn("Amount", GetType(String)))
            For Each _item In VolumeDiscounts.Items
                breaklevel = CType(_item.FindControl("BreakLevel"), TextBox).Text
                If breaklevel.Length > 0 And objProdManagement.DiscountType = 1 Then
                    dr = dt.NewRow
                    dr(0) = breaklevel
                    dr(1) = Convert.ToDecimal(CType(_item.FindControl("Amount"), TextBox).Text) / 100
                    dt.Rows.Add(dr)
                ElseIf breaklevel.Length > 0 And objProdManagement.DiscountType = 0 Then
                    dr = dt.NewRow
                    dr(0) = breaklevel
                    dr(1) = Convert.ToDecimal(CType(_item.FindControl("Amount"), TextBox).Text)
                    dt.Rows.Add(dr)
                End If
            Next
            objProdManagement.updateVolumePricing(dt)
        Else
            objProdManagement.update()
        End If
    End Sub

    Private Sub VolumeDiscounts_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles VolumeDiscounts.ItemCreated
        If (e.Item.ItemType = ListItemType.Header) Then
            If (IsNothing(objProdManagement) = False) Then
                loadDiscountTypesDD(e.Item.FindControl("DiscountType"))
            End If
        End If
    End Sub
End Class
