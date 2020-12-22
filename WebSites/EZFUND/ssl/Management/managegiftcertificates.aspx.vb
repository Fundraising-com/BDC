
Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class managegiftcertificates
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents StandardSearchControl1 As StandardSearchControl
    Protected WithEvents Editgiftcertificates1 As editgiftcertificates
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents P1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Addgiftcertificates1 As addgiftcertificates
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
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

        Try
            CType(Addgiftcertificates1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationAdd();")
            CType(Editgiftcertificates1.FindControl("btnSave"), LinkButton).Attributes.Add("onclick", "return SetValidationEdit();")
            CType(Me.LeftColumnNav1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            LoadSearch()
            ' Set the Tab Elements
            'Dim ar As New ArrayList()
            'ar = New ArrayList()
            'ar.Add("Manage Gift Certificates")
            'ar.Add("Add Gift Certificates")
            'AdminTabControl1.BorderClass = "ContentTable"
            'AdminTabControl1.TabItemClass = "Content"
            'AdminTabControl1.TabStringArray = ar
        Catch ex As Exception
            Session("DetailError") = "Class ManageGiftCertificates Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        'If Not Page.IsPostBack Then
        '    Me.StandardSearchControl1.Visible = True
        '    Me.Editgiftcertificates1.Visible = False
        '    Me.Addgiftcertificates1.Visible = False
        'End If


    End Sub

    Private Sub LoadSearch()
        Dim obj As New CStoreGiftCertificates()
        Dim objStorage As New CSearchControlStorage()
        Dim objCerts As CGiftCertificates = obj.GetAllGiftCertificates()

        ' Set the display properties
        objStorage.ButtonID = "ID"
        objStorage.CheckBoxColumn = False
        objStorage.ContentClass = "Content"
        objStorage.Paging = True
        objStorage.ShowButtons = True
        objStorage.Sorting = False
        objStorage.DataSource = objCerts.GiftCertificates
        objStorage.PagerClass = "ContentTableHeader"
        objStorage.TitleClass = "ContentTableHeader"
        objStorage.TitleString = "Gift Certificates"
        objStorage.DeleteMessage = "Are You Sure You Want to Delete This Gift Certificate?"

        ' Add the Columns
        Dim ar As New ArrayList()
        ar.Add("Code")
        ar.Add("DollarOff")

        objStorage.ColumnList = ar

        StandardSearchControl1.StorageClass = objStorage

    End Sub

    Private Sub StandardSearchControl1_DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.DeleteClick
        ' Delete giftcertificate with uid = sender
        Dim obj As New CStoreGiftCertificates()
        obj.DeleteGiftCertificate(sender)
        LoadSearch()
        StandardSearchControl1.ReloadList()
    End Sub

    Private Sub StandardSearchControl1_EditClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StandardSearchControl1.EditClick
        ' Show Edit Control
        StandardSearchControl1.Visible = False
        Addgiftcertificates1.Visible = False

        Dim obj As New CStoreGiftCertificates()
        Dim gift As CGiftCertificate
        Dim objCerts As CGiftCertificates = obj.GetAllGiftCertificates()

        For Each gift In objCerts.GiftCertificates
            If (gift.ID = sender) Then
                Editgiftcertificates1.EditGiftCertificateID = gift
                Exit For
            End If
        Next

        Editgiftcertificates1.Visible = True
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub

    'Private Sub AdminTabControl1_TabClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdminTabControl1.TabClick
    '    If (sender = "0") Then
    '        'Manage Gift Certificiates
    '        StandardSearchControl1.Visible = True
    '        Addgiftcertificates1.Visible = False
    '        Editgiftcertificates1.Visible = False
    '    ElseIf (sender = "1") Then
    '        ' Add Gift Certificates
    '        StandardSearchControl1.Visible = False
    '        Addgiftcertificates1.Visible = True
    '        Editgiftcertificates1.Visible = False
    '    End If
    'End Sub

    Private Sub Editgiftcertificates1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Editgiftcertificates1.Save
        'Manage Gift Certificiates
        StandardSearchControl1.Visible = True
        Addgiftcertificates1.Visible = False
        Editgiftcertificates1.Visible = False
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
        LoadSearch()
        StandardSearchControl1.ReloadList()
    End Sub

    Private Sub Addgiftcertificates1_Save(ByVal sender As Object, ByVal e As System.EventArgs) Handles Addgiftcertificates1.Save
        'Manage Gift Certificiates
        StandardSearchControl1.Visible = True
        Addgiftcertificates1.Visible = False
        Editgiftcertificates1.Visible = False
        Me.btnAdd.Visible = True
        Me.imgAdd.Visible = True
        LoadSearch()
        StandardSearchControl1.ReloadList()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Me.StandardSearchControl1.Visible = False
        Me.Addgiftcertificates1.Visible = True
        Me.Addgiftcertificates1.ClearFields()
        Me.Editgiftcertificates1.Visible = False
        Me.btnAdd.Visible = False
        Me.imgAdd.Visible = False
    End Sub


End Class
