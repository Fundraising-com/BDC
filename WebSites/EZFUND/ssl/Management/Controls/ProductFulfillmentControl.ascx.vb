Imports storefront.systembase
Imports StoreFront.BusinessRule.Management
Public MustInherit Class ProductFulfillmentControl
    Inherits System.Web.UI.UserControl

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
    Protected WithEvents ProdUID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Row1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row5 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row6 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row7 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Row8 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ShipProduct As System.Web.UI.WebControls.CheckBox
    Protected WithEvents DropShip As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Height As System.Web.UI.WebControls.TextBox
    Protected WithEvents Width As System.Web.UI.WebControls.TextBox
    Protected WithEvents Length As System.Web.UI.WebControls.TextBox
    Protected WithEvents Weight As System.Web.UI.WebControls.TextBox
    Protected WithEvents ShipPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftWrapPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftWrap As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Download As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Expires As System.Web.UI.WebControls.DropDownList
    Protected WithEvents MultipleDownload As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents UploadControl1 As UploadControl

    Private objProdManagement As CProductManagement
    
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            Row1.Visible = False
            Row2.Visible = False
            Row3.Visible = False
            Row4.Visible = False
            Row5.Visible = False
            Row6.Visible = False
            Row7.Visible = False
            Row8.Visible = False
        Else
            UploadControl1.FileType = UploadControl._FileType.DownLoad
            UploadControl1.LabelDisplay = "Download: "
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
            ProdUID.Value = M_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            ShipProduct.Checked = objProdManagement.ShipProduct
            GiftWrap.Checked = objProdManagement.GiftWrap
            GiftWrapPrice.Text = objProdManagement.GiftWrapPrice

            If (Row1.Visible) Then
                Download.Checked = objProdManagement.Downloadable
                MultipleDownload.Checked = objProdManagement.AllowMultipleDownloads
                DropShip.Checked = objProdManagement.ShipProductFromVendor
                UploadControl1.FileText = objProdManagement.FileName
                Call loadExpiresDD()
            End If

            Weight.Text = objProdManagement.Weight
            Height.Text = objProdManagement.Height
            Length.Text = objProdManagement.Length
            Width.Text = objProdManagement.Width
            ShipPrice.Text = objProdManagement.ShipPrice



        End If
    End Sub

    Private Sub loadExpiresDD()
        Dim dt As DataTable

        Dim x As Integer
        dt = objProdManagement.getDownloadExpiresDT
        Expires.DataSource = dt
        Expires.DataValueField = "ID"
        Expires.DataTextField = "Display"
        Expires.DataBind()

        For x = 0 To dt.Rows.Count - 1
            If dt.Rows(x).Item("ID") = objProdManagement.DownloadExpires Then
                Expires.SelectedIndex = x
                Exit For
            End If
        Next
    End Sub

#Region "Private Function GetFileName(ByVal strPath As String) As String"

    Private Function GetFileName(ByVal strPath As String) As String
        Dim ar() As String
        strPath = Trim(strPath)
        strPath = Replace(strPath, "/", "\")

        If Right(strPath, 1) = "\" Then
            strPath = Left(strPath, Len(strPath) - 1)
        End If
        ar = Split(strPath, "\")
        If ar(UBound(ar)) <> "" Then
            Return ar(UBound(ar))
        Else
            Return ""
        End If
    End Function

#End Region

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objProdManagement = New CProductManagement(M_uid)


        lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
        lblProdName.Text = objProdManagement.Name
        objProdManagement.ShipProduct = ShipProduct.Checked
        objProdManagement.GiftWrap = GiftWrap.Checked
        objProdManagement.GiftWrapPrice = GiftWrapPrice.Text
        objProdManagement.Weight = Weight.Text
        objProdManagement.Height = Height.Text
        objProdManagement.Length = Length.Text
        objProdManagement.Width = Width.Text
        objProdManagement.ShipPrice = ShipPrice.Text

        If (Row1.Visible) Then
            objProdManagement.Downloadable = Download.Checked
            objProdManagement.AllowMultipleDownloads = MultipleDownload.Checked
            objProdManagement.ShipProductFromVendor = DropShip.Checked
            objProdManagement.DownloadExpires = Expires.SelectedItem.Value
            objProdManagement.FileName = GetFileName(UploadControl1.FileText)
        End If

        objProdManagement.update()
    End Sub
End Class
