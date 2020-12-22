Imports StoreFront.BusinessRule.Management
Partial  Class ProductDetailsControl
    Inherits CWebControl

    Protected WithEvents ucSmallImage As UploadControl
    Protected WithEvents ucLargeImage As UploadControl
    'BEGIN CUSTOM CODE Sept '04
    Protected WithEvents ucCloseUpImage As UploadControl
    'END CUSTOM CODE Sept '04
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
            ucSmallImage.FileType = UploadControl.m_FileType.Image
            'BEGIN CUSTOM CODE Sept '04
            ucCloseUpImage.FileType = UploadControl.m_FileType.Image
            'END CUSTOM CODE Sept '04

            objProdManagement = New CProductManagement(m_uid)
            'Tee 8/7/2007 product configurator
            MakeCommonVisible(objProdManagement.ProductType, False)
            'end Tee
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
            ucSmallImage.FileType = UploadControl.m_FileType.Image
            ucLargeImage.FileType = UploadControl.m_FileType.Image
            ucSmallImage.LabelDisplay = "Small Image:"
            ucLargeImage.LabelDisplay = "Large Image:"
            'BEGIN CUSTOM CODE Sept '04
            ucCloseUpImage.FileType = UploadControl.m_FileType.Image
            ucCloseUpImage.LabelDisplay = "Close Up Image:"
            ucCloseUpImage.FileText = objProdManagement.CloseUpImage
            'END CUSTOM CODE Sept '04

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
        If ucLargeImage.FileText.ToLower.IndexOf("http://") = -1 Then ' if FQDN is not entered
            ' TODO: check the next line, I don't think it works
            ucLargeImage.FileText.Replace("\", "/")
            If ucLargeImage.FileText.ToLower.StartsWith("images/") = False Then
                If ucLargeImage.FileText <> "" Then
                    ucLargeImage.FileText = "images/" & ucLargeImage.FileText
                End If
            End If
        End If
        If ucSmallImage.FileText.ToLower.IndexOf("http://") = -1 Then
            ' TODO: check the next line, I don't think it works
            ucSmallImage.FileText.Replace("\", "/")
            If ucSmallImage.FileText.ToLower.StartsWith("images/") = False Then
                If ucSmallImage.FileText <> "" Then
                    ucSmallImage.FileText = "images/" & ucSmallImage.FileText
                End If
            End If
        End If
        objProdManagement.SmallImage = ucSmallImage.FileText
        objProdManagement.LargeImage = ucLargeImage.FileText
        'BEGIN CUSTOM CODE Sept '04
        If ucCloseUpImage.FileText.ToLower.IndexOf("http://") = -1 Then
            ' TODO: make this work like the 2 above
            ucCloseUpImage.FileText = ucCloseUpImage.FileText.Replace("\", "/")
            If ucCloseUpImage.FileText.ToLower.StartsWith("images/") = False Then
                If ucCloseUpImage.FileText <> "" Then
                    ucCloseUpImage.FileText = "images/" & ucCloseUpImage.FileText
                End If
            End If
        End If
        objProdManagement.CloseUpImage = ucCloseUpImage.FileText
        'END CUSTOM CODE Sept '04

        objProdManagement.update()
    End Sub

End Class
