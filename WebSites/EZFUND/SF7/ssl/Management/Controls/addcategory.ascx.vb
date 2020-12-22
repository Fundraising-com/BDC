Imports System
Imports StoreFront.BusinessRule
Imports StoreFront.Systembase
Partial  Class addcategory
    Inherits CWebControl
    Protected objCategory As CXMLCategoryList
    Protected objCategoryAccess As CCategories
    Protected WithEvents catUpload As UploadControl

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

#Region "Events"
    Event Save As EventHandler
    Event Cancel As EventHandler
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objCategory = New CXMLCategoryList()

        catUpload.FileType = UploadControl.m_FileType.Image
        catUpload.LabelDisplay = String.Empty

    End Sub
#End Region

    Public Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        FillCategory()
        objCategoryAccess = New CCategories()
        Dim ar As ArrayList
        ar = objCategoryAccess.GetAllCategories()
        Dim cat As CXMLCategoryList
        For Each cat In ar
            If cat.Name.ToLower = objCategory.Name.ToLower And cat.ParentID = 0 And objCategory.ParentID = 0 Then
                CType(Parent.FindControl("lblErrorMessage"), Label).Text = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("addcategory", "Error", "Duplicate")
                CType(Parent.FindControl("lblErrorMessage"), Label).Visible = True
                Exit Sub
            End If
        Next
        CType(Parent.FindControl("lblErrorMessage"), Label).Text = ""
        CType(Parent.FindControl("lblErrorMessage"), Label).Visible = False
        objCategoryAccess.AddCategory(objCategory)
        RaiseEvent Save(objCategory, EventArgs.Empty)
    End Sub

#Region "Sub FillFields(ByVal category As CXMLCategoryList)"
    Public Sub FillFields(ByVal category As CXMLCategoryList)
        Me.txtLevelHidden.Value = CLng(category.Level) + 1
        Me.txtParentIDHidden.Value = category.ID
        Me.txtName.Text = ""
        'SKC 7.0
        Me.txtCatDesc.Text = String.Empty
        Me.chkFeatured.Checked = False
        Me.catUpload.FileText = String.Empty
        'End SKC
    End Sub
#End Region

    Public Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        CType(Parent.FindControl("lblErrorMessage"), Label).Text = ""
        CType(Parent.FindControl("lblErrorMessage"), Label).Visible = False
        RaiseEvent Cancel(objCategory, EventArgs.Empty)
    End Sub


#Region "Sub ClearFields()"
    Public Sub ClearFields()
        txtName.Text = ""
        txtLevelHidden.Value = 0
        txtParentIDHidden.Value = 0
        'SKC 7.0
        chkFeatured.Checked = False
        txtCatDesc.Text = String.Empty
        catUpload.FileText = String.Empty
        'End SKC
    End Sub
#End Region

#Region "Sub FillCategory()"
    Public Sub FillCategory()
        objCategory.Level = CLng(txtLevelHidden.Value)
        objCategory.Name = txtName.Text
        objCategory.ParentID = txtParentIDHidden.Value
        'SKC 7.0
        objCategory.Featured = chkFeatured.Checked
        objCategory.Description = txtCatDesc.Text

        If catUpload.FileText.ToLower.IndexOf("http://") = -1 Then
            catUpload.FileText.Replace("\", "/")
            If catUpload.FileText.ToLower.StartsWith("images/") = False Then
                If catUpload.FileText <> "" Then
                    catUpload.FileText = "images/" & catUpload.FileText
                End If
            End If
        End If
        objCategory.Image = catUpload.FileText
        'End SKC
    End Sub
#End Region

End Class
