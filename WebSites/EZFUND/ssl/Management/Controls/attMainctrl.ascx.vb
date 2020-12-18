Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml

Public MustInherit Class attMainctrl
    Inherits System.Web.UI.UserControl

#Region "Class Members"

	Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtAttName As System.Web.UI.WebControls.TextBox
    Protected WithEvents attType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkRequired As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents requiredRow As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents TypeRow As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents PriceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents WeightType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtWeight As System.Web.UI.WebControls.TextBox
    Protected WithEvents CustomDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents lblAttName As System.Web.UI.WebControls.Label
    Private _AttManager As CAttributeManagement
    Protected WithEvents InvPrompt As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents UploadControl1 As UploadControl
    Event AddDone As EventHandler
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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        UploadControl1.FileType = UploadControl._FileType.DownLoad
        ErrorMessage.Visible = False
        TypeRow.Visible = False
        If (StoreFrontConfiguration.XMLDocument.DocumentElement.Item("Admin").Item("StoreFront").Attributes("Type").Value = "SE") Then
            UploadControl1.Visible = False
        End If
    End Sub

#End Region

#Region "Public WriteOnly Property Manager() As CAttributeManagement"

    Public WriteOnly Property Manager() As CAttributeManagement
        Set(ByVal Value As CAttributeManagement)
            _AttManager = Value
            Session("AttributeManager") = _AttManager
        End Set
    End Property
#End Region

#Region "Public Sub LoadMe()"

    Public Sub LoadMe()

        Dim i As Integer
        Try
            CustomDetails.Visible = False
            lblAttName.Text = "Name:"

            If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                If _AttManager.ProductInventory.InventoryTracked Then
                    'Me.cmdSave.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Inventory Stock will be overwritten. Do you want to continue?" & "');")
                    Me.InvPrompt.Attributes.Add("value", "true")
                End If

            End If

            attType.ClearSelection()
            For i = 0 To attType.Items.Count - 1
                If attType.Items(i).Value = _AttManager.Attribute.AttributeType Then
                    attType.Items(i).Selected = True
                    Exit For
                End If
            Next

            Me.txtAttName.Text = _AttManager.Attribute.Name

            If _AttManager.Attribute.AttributeType = SystemBase.tAttributeType.Normal Then
                requiredRow.Visible = False
            Else
                '#update 1907
                _AttManager.AttributeDetail = _AttManager.Attribute.AttributeDetails.Item(0)
                Me.txtPrice.Text = _AttManager.AttributeDetail.Price
                Me.txtWeight.Text = _AttManager.AttributeDetail.Weight
                requiredRow.Visible = True
                LoadCustomDetail()

            End If
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

#Region "Public Sub addnew(Optional ByVal IsCustom As Boolean = False, Optional ByVal strName As String = "")"

    Public Sub addnew(Optional ByVal IsCustom As Boolean = False, Optional ByVal strName As String = "")
        Try
            Me.chkRequired.Checked = True
            CustomDetails.Visible = False
            lblAttName.Text = "Name:"
            txtAttName.Text = ""
            requiredRow.Visible = False
            If IsCustom Then
                CustomDetails.Visible = True
                requiredRow.Visible = True
                lblAttName.Text = "Label:"
                UploadControl1.LabelDisplay = "File DownLoad: "
                ClearCustomObjects()
                attType.SelectedIndex = 1
                txtAttName.Text = strName
            End If
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub


#End Region

#Region "Private Sub attType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles attType.SelectedIndexChanged"

    Private Sub attType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles attType.SelectedIndexChanged
        _AttManager = Session("AttributeManager")
        Try
            If attType.SelectedItem.Value = 0 Then
                ' #1436 MS Start
                Dim obj As CAttribute
                obj = New CAttribute()
                obj.Name = txtAttName.Text
                obj.AttributeType = SystemBase.tAttributeType.Normal
                obj.Required = True
                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Template Then
                    obj.UID = _AttManager.NewAttributeID
                Else
                    obj.UID = 0 - (_AttManager.Attributes.Count + 1)
                End If
                _AttManager.Attributes.Add(obj)
                _AttManager.Attribute = obj

                _AttManager.Save()
                LoadMe()
                '_AttManager.Attribute.AttributeType = SystemBase.tAttributeType.Normal
                ' #1426 MS End
                requiredRow.Visible = False
            Else
                If _AttManager.CurrentEditMode = CAttributeManagement.EditMode.edit Then
                    _AttManager.Attribute.AttributeType = SystemBase.tAttributeType.Custom
                    ClearCustomObjects()
                    LoadCustomDetail()
                Else
                    CustomDetails.Visible = True
                    lblAttName.Text = "Label:"
                    UploadControl1.LabelDisplay = "File DownLoad: "
                    ClearCustomObjects()
                End If
                requiredRow.Visible = True
            End If
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

#Region "Private Sub MakeNewAttribute()"
    '##SUMMARY   Makes a New Attribute
    Private Sub MakeNewAttribute()
        Try
            Dim obj As CAttribute
            If _AttManager.CurrentEditMode <> CAttributeManagement.EditMode.edit Then
                obj = New CAttribute()
                _AttManager.Attributes.Add(obj)
                _AttManager.Attribute = obj
                _AttManager.Attribute.UID = _AttManager.NewAttributeID
            End If

            _AttManager.Attribute.Name = Me.txtAttName.Text
            _AttManager.Attribute.AttributeType = Me.attType.SelectedItem.Value
            _AttManager.Attribute.Required = Me.chkRequired.Checked

            If _AttManager.Attribute.AttributeType = SystemBase.tAttributeType.Custom Then
                Dim objDetail As New CAttributeDetail()
              
                    _AttManager.Attribute.AttributeDetails.Clear()
                  

                _AttManager.Attribute.AttributeDetails.Add(objDetail)
                _AttManager.AttributeDetail = _AttManager.Attribute.AttributeDetails.Item(0)
                If _AttManager.WorkMode = AttributeWorkMode.Template Then
                    objDetail.UID = _AttManager.NewDetailID
                Else
                    objDetail.UID = 0 - (_AttManager.NewDetailID + 1)
                End If
                _AttManager.AttributeDetail.AttributeID = _AttManager.Attribute.UID
                _AttManager.AttributeDetail.FilePath = GetFileName(UploadControl1.FileText)
                _AttManager.AttributeDetail.Name = _AttManager.Attribute.Name
                _AttManager.AttributeDetail.Order = 1
                _AttManager.AttributeDetail.Price = CDec("0" & Me.txtPrice.Text)
                _AttManager.AttributeDetail.PriceChange = Me.PriceType.SelectedItem.Value
                _AttManager.AttributeDetail.Weight = CDec("0" & Me.txtWeight.Text)
                _AttManager.AttributeDetail.WeightChange = Me.WeightType.SelectedItem.Value
            End If

        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

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

#Region "Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles CmdCancel.Click"

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        RaiseEvent AddDone("", EventArgs.Empty)
    End Sub


#End Region

#Region "Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdSave.Click"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            _AttManager = Session("AttributeManager")
            MakeNewAttribute()
            _AttManager.Save()
            txtAttName.Text = ""
            attType.ClearSelection()
            Session("AttributeManager") = _AttManager
            RaiseEvent AddDone("Changes Saved", EventArgs.Empty)
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub


#End Region

#Region "Private Sub LoadCustomDetail()"

    Private Sub LoadCustomDetail()
        Dim i As Integer
        Try
            CustomDetails.Visible = True
            lblAttName.Text = "Label:"
            UploadControl1.LabelDisplay = "File DownLoad: "
            If IsNothing(_AttManager.AttributeDetail) Then
                If _AttManager.Attribute.AttributeDetails.Count > 0 Then
                    _AttManager.AttributeDetail = _AttManager.Attribute.AttributeDetails.Item(0)
                Else
                    Dim obj As New CAttributeDetail()
                    obj.Name = _AttManager.Attribute.Name
                    obj.AttributeID = _AttManager.Attribute.UID
                    _AttManager.Attribute.AttributeDetails.Add(obj)
                    _AttManager.AttributeDetail = _AttManager.Attribute.AttributeDetails.Item(0)
                End If
            End If
            Me.chkRequired.Checked = _AttManager.Attribute.Required
            UploadControl1.FileText = _AttManager.AttributeDetail.FilePath
            Me.txtPrice.Text = _AttManager.AttributeDetail.Price
            Me.txtWeight.Text = _AttManager.AttributeDetail.Weight
            WeightType.ClearSelection()
            For i = 0 To WeightType.Items.Count - 1
                If WeightType.Items(i).Value = _AttManager.AttributeDetail.WeightChange Then
                    WeightType.Items(i).Selected = True
                    Exit For
                End If
            Next
            PriceType.ClearSelection()
            For i = 0 To PriceType.Items.Count - 1
                If PriceType.Items(i).Value = _AttManager.AttributeDetail.PriceChange Then
                    PriceType.Items(i).Selected = True
                    Exit For
                End If
            Next
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub


#End Region

#Region "Private Sub ClearCustomObjects()"
    '##SUMMARY  Clears all interface values
    Private Sub ClearCustomObjects()
        Try
            UploadControl1.FileText = ""
            txtWeight.Text = ""
            txtPrice.Text = ""
            WeightType.ClearSelection()
            PriceType.ClearSelection()
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

End Class
