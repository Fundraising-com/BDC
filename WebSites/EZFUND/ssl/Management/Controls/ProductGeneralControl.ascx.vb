Imports StoreFront.BusinessRule.Management
Public MustInherit Class ProductGeneralControl
    Inherits System.Web.UI.UserControl

    Protected WithEvents ProductIsActive As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ProdCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProdName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProdNamePlural As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProdPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents ProdCost As System.Web.UI.WebControls.TextBox
    Protected WithEvents Manufacturers As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Vendors As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LocalTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents StateTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CountryTax As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ProdUID As System.Web.UI.HtmlControls.HtmlInputHidden
    Private lblProdName As Label
    Private M_uid As Long
    Private objProdManagement As CProductManagement
    Private trAdd As System.Web.UI.HtmlControls.HtmlTableRow
    Private trEdit As System.Web.UI.HtmlControls.HtmlTableRow
    Private trAdd2 As System.Web.UI.HtmlControls.HtmlTableRow
    Private trEdit2 As System.Web.UI.HtmlControls.HtmlTableRow
    Dim objError As Label

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

        trAdd = CType(Me.Parent.FindControl("Add"), System.Web.UI.HtmlControls.HtmlTableRow)
        trEdit = CType(Me.Parent.FindControl("Edit"), System.Web.UI.HtmlControls.HtmlTableRow)
        trAdd2 = CType(Me.Parent.FindControl("Add2"), System.Web.UI.HtmlControls.HtmlTableRow)
        trEdit2 = CType(Me.Parent.FindControl("Edit2"), System.Web.UI.HtmlControls.HtmlTableRow)
        If IsPostBack = True Then
            M_uid = Session("ProductId")

        Else
            If Request.QueryString("ID") = "" And Session("ProductId") = 0 Then
                M_uid = 0
                trEdit.Visible = False
                trAdd.Visible = True
                trEdit2.Visible = False
                trAdd2.Visible = True
            Else
                trEdit.Visible = True
                trAdd.Visible = False
                trEdit2.Visible = True
                trAdd2.Visible = False

                M_uid = Request.QueryString("ID")
                If M_uid = 0 Then
                    M_uid = Session("ProductId")
                Else
                    Session("ProductId") = M_uid
                End If
            End If


            objProdManagement = New CProductManagement(M_uid)
            ProdUID.Value = M_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            Session("ProductName") = objProdManagement.Name
            ProductIsActive.Checked = objProdManagement.Active
            ProdCode.Text = objProdManagement.Code
            ProdName.Text = objProdManagement.Name
            ProdNamePlural.Text = objProdManagement.PluralName
            ProdPrice.Text = objProdManagement.Price
            ProdCost.Text = objProdManagement.Cost
            Call loadManufacturerDD()
            Call loadVendorDD()
            LocalTax.Checked = objProdManagement.ApplyLocalTax
            StateTax.Checked = objProdManagement.ApplyStateTax
            CountryTax.Checked = objProdManagement.ApplyCountryTax

        End If

    End Sub

    Private Sub loadManufacturerDD()
        Dim dt As DataTable

        Dim x As Integer
        dt = objProdManagement.getManufacturersDT
        Manufacturers.DataSource = dt
        Manufacturers.DataValueField = "ID"
        Manufacturers.DataTextField = "Name"
        Manufacturers.DataBind()
        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objProdManagement.Manufacturer Then
                Manufacturers.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub loadVendorDD()
        Dim dt As DataTable

        Dim x As Integer
        dt = objProdManagement.getVendorsDT
        Vendors.DataSource = dt
        Vendors.DataValueField = "ID"
        Vendors.DataTextField = "Name"
        Vendors.DataBind()
        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objProdManagement.Vendor Then
                Vendors.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        objError.Text = ""
        objError.Visible = False
        Dim bnew As Boolean
        objProdManagement = New CProductManagement(M_uid)

        If M_uid = 0 Then
            If objProdManagement.ProductUID(ProdCode.Text) <> 0 Then
                objError.Text = "Product code already exists."
                objError.Visible = True
                Exit Sub
            End If
        Else
            If objProdManagement.ProductUID(ProdCode.Text) <> 0 And objProdManagement.ProductUID(ProdCode.Text) <> M_uid Then
                objError.Text = "Product code already exists."
                objError.Visible = True
                Exit Sub
            End If
        End If
        lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
        lblProdName.Text = objProdManagement.Name
        objProdManagement.Active = ProductIsActive.Checked
        objProdManagement.Code = ProdCode.Text
        objProdManagement.Name = ProdName.Text
        objProdManagement.PluralName = ProdNamePlural.Text
        objProdManagement.Price = ProdPrice.Text
        objProdManagement.Cost = ProdCost.Text
        objProdManagement.Vendor = Vendors.SelectedItem.Value
        objProdManagement.Manufacturer = Manufacturers.SelectedItem.Value
        objProdManagement.ApplyLocalTax = LocalTax.Checked
        objProdManagement.ApplyStateTax = StateTax.Checked
        objProdManagement.ApplyCountryTax = CountryTax.Checked

        objProdManagement.update()
        If M_uid = 0 Then
            M_uid = objProdManagement.uid
            trEdit.Visible = True
            trAdd.Visible = False
            trEdit2.Visible = True
            trAdd2.Visible = False
            Session("ProductId") = M_uid
            lblProdName.Text = objProdManagement.Name

        End If
    End Sub
End Class
