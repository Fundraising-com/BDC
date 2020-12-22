Imports StoreFront.BusinessRule.Management
Public MustInherit Class ProductDetailsControl

    Inherits System.Web.UI.UserControl
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents DetailLinkType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DetailLink As System.Web.UI.WebControls.TextBox
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ucSmallImage As UploadControl
    Protected WithEvents ucLargeImage As UploadControl
    Protected WithEvents ShortDescription As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents LongDescription As System.Web.UI.HtmlControls.HtmlTextArea
    Protected WithEvents ProdUID As System.Web.UI.HtmlControls.HtmlInputHidden
    Private m_uid As Long
    Private objProdManagement As CProductManagement
    Private lblProdName As Label
    Private m_DetailLinkType As Integer
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
            ucSmallImage.FileType = UploadControl._FileType.Image



            objProdManagement = New CProductManagement(m_uid)
            ProdUID.Value = m_uid
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name
            ShortDescription.Value = objProdManagement.ShortDescription
            LongDescription.Value = objProdManagement.LongDescription
            m_DetailLinkType = objProdManagement.DetailLinkType
            Call loadDetailLinkTypeDD()
            If m_DetailLinkType = 0 Then
                DetailLink.Text = ""
            Else
                DetailLink.Text = objProdManagement.DetailLink
            End If
            ucSmallImage.FileType = UploadControl._FileType.Image
            ucLargeImage.FileType = UploadControl._FileType.Image
            ucSmallImage.LabelDisplay = "Small Image:"
            ucLargeImage.LabelDisplay = "Large Image:"

            ucSmallImage.FileText = objProdManagement.SmallImage
            ucLargeImage.FileText = objProdManagement.LargeImage
        End If

    End Sub

    Private Sub loadDetailLinkTypeDD()
        DetailLinkType.DataSource = objProdManagement.getDetailLinkTypesDT
        DetailLinkType.DataValueField = "ID"
        DetailLinkType.DataTextField = "Display"
        DetailLinkType.DataBind()
        DetailLinkType.SelectedIndex = m_DetailLinkType

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim objError As Label
        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        objError.Visible = False
        If Request.Form("ProductDetails:ShortDescription").Length > 255 Then

            objError.Text = "Short description may not exceed 255 characters."
            objError.Visible = True
            Exit Sub

        End If
        objProdManagement = New CProductManagement(m_uid)
        ProdUID.Value = m_uid
        lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
        lblProdName.Text = objProdManagement.Name
        objProdManagement.ShortDescription = Request.Form("ProductDetails:ShortDescription") 'this returns it as html instead of html encoded
        objProdManagement.LongDescription = Request.Form("ProductDetails:LongDescription") 'this returns it as html instead of html encoded
        m_DetailLinkType = DetailLinkType.SelectedItem.Value
        objProdManagement.DetailLinkType = m_DetailLinkType
        objProdManagement.DetailLink = DetailLink.Text
        ucLargeImage.FileText.Replace("\", "/")
        ucSmallImage.FileText.Replace("\", "/")
        If ucSmallImage.FileText.ToLower.StartsWith("images/") = False Then
            If ucSmallImage.FileText <> "" Then
                ucSmallImage.FileText = "images/" & ucSmallImage.FileText
            End If
        End If

        If ucLargeImage.FileText.ToLower.StartsWith("images/") = False Then
            If ucLargeImage.FileText <> "" Then
                ucLargeImage.FileText = "images/" & ucLargeImage.FileText
            End If
        End If
        objProdManagement.SmallImage = ucSmallImage.FileText
        objProdManagement.LargeImage = ucLargeImage.FileText
        objProdManagement.update()
    End Sub

End Class
