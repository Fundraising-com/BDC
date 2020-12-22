Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml


Public MustInherit Class attdetailctrl
    Inherits System.Web.UI.UserControl

#Region "Class Members"

	 Protected WithEvents lblDetailName As System.Web.UI.WebControls.Label
    Protected WithEvents txtDetailName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddDetailOrder As System.Web.UI.WebControls.DropDownList
    Protected WithEvents PriceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPrice As System.Web.UI.WebControls.TextBox
    Protected WithEvents WeightType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdAddOption As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgAddOption As System.Web.UI.WebControls.Image
    Protected WithEvents CmdCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtWeight As System.Web.UI.WebControls.TextBox
    Protected WithEvents UploadControl1 As UploadControl
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Private _AttManager As CAttributeManagement
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Private arNEWDetails As ArrayList
    Event AddDone As EventHandler

    Private Enum JavaMessage
        Confirm = 1
        ApplyChanges = 2
        OverWriteInventory = 3
        Custom = 4
    End Enum


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
        Me.ErrorMessage.Visible = False
        UploadControl1.FileType = UploadControl._FileType.DownLoad
        UploadControl1.LabelDisplay = "File DownLoad: "
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
        If IsNothing(_AttManager) Then
            Exit Sub
        Else
            Try
                Dim i As Integer
                txtDetailName.Text = _AttManager.AttributeDetail.Name
                UploadControl1.LabelDisplay = "File DownLoad: "
                UploadControl1.FileText = _AttManager.AttributeDetail.FilePath
                Me.txtPrice.Text = _AttManager.AttributeDetail.Price
                Me.txtWeight.Text = _AttManager.AttributeDetail.Weight
                WeightType.ClearSelection()
                LoadOrderCount()
                For i = 0 To WeightType.Items.Count - 1
                    If WeightType.Items(i).Value = _AttManager.AttributeDetail.WeightChange Then
                        WeightType.Items(i).Selected = True
                        Exit For
                    End If
                Next
                ddDetailOrder.ClearSelection()
                For i = 0 To ddDetailOrder.Items.Count - 1
                    If ddDetailOrder.Items(i).Value = _AttManager.AttributeDetail.Order Then
                        ddDetailOrder.Items(i).Selected = True
                        Exit For
                    End If
                Next
                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                    If _AttManager.ProductInventory.InventoryTracked Then
                        AddJavaMSG(cmdSave, JavaMessage.Custom, "Inventory Stock will be overwritten. Are You Sure You Want to Save Changes?")
                    End If
                End If

                PriceType.ClearSelection()
                For i = 0 To PriceType.Items.Count - 1
                    If PriceType.Items(i).Value = _AttManager.AttributeDetail.PriceChange Then
                        PriceType.Items(i).Selected = True
                        Exit For
                    End If
                Next
                cmdAddOption.Visible = False
                imgAddOption.Visible = False
            Catch err As SystemException
                Me.ErrorMessage.Text = err.Message
                Me.ErrorMessage.Visible = True
            End Try

        End If

    End Sub

#End Region

#Region "Private Sub LoadOrderCount()"
    '##SUMMARY  Loads Order Display for the attribute
    '##SUMMARY   and adds one to count if needed
    Private Sub LoadOrderCount()
        Try
            ddDetailOrder.DataSource = _AttManager.DetailOrderList
            ddDetailOrder.DataBind()

        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

#Region "Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdSave.Click"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        arNEWDetails = Session("NewDetails")
        Dim objNewDetail As CAttributeDetail
        Try
            _AttManager = Session("AttributeManager")
            Select Case _AttManager.CurrentEditMode
                Case CAttributeManagement.EditMode.add
                    If MakeNewDetail() Then
                        If IsNothing(arNEWDetails) = False Then
                            For Each objNewDetail In arNEWDetails
                                ' _AttManager.Attribute.AttributeDetails.Add(objNewDetail)
                                _AttManager.Attribute.AttributeDetails.Insert(objNewDetail.Order - 1, objNewDetail)
                            Next
                        End If
                        _AttManager.CurrentEditMode = CAttributeManagement.EditMode.edit
                        _AttManager.NewDetailCount = 0
                        Session("NewDetails") = Nothing
                        _AttManager.Save()
                        Session("AttributeManager") = _AttManager
                        ClearObjects()
                        RaiseEvent AddDone("Changes Saved", EventArgs.Empty)
                    End If
                Case CAttributeManagement.EditMode.edit
                    If MakeNewDetail() Then
                        _AttManager.Attribute.AttributeDetails.Remove(_AttManager.AttributeDetail)
                        _AttManager.Attribute.AttributeDetails.Insert(_AttManager.AttributeDetail.Order - 1, _AttManager.AttributeDetail)
                        _AttManager.Save()
                        Session("AttributeManager") = _AttManager
                        Session("NewDetails") = Nothing
                        ClearObjects()
                        RaiseEvent AddDone("Changes Saved", EventArgs.Empty)
                    End If
            End Select
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try

    End Sub


#End Region

#Region "Public Sub AddNew()"

    Public Sub AddNew()
        Try
            cmdAddOption.Visible = True
            imgAddOption.Visible = True
            ClearObjects()
            LoadOrderCount()
            ddDetailOrder.SelectedIndex = ddDetailOrder.Items.Count - 1
            If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                If _AttManager.ProductInventory.InventoryTracked Then
                    AddJavaMSG(cmdSave, JavaMessage.Custom, "Inventory Stock will be overwritten. Are You Sure You Want to Save Changes?")
                End If
            End If
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

#Region "Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles CmdCancel.Click"

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        Try
            arNEWDetails = Nothing
            Session("NewDetails") = Nothing
            RaiseEvent AddDone("", e)
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub


#End Region

#Region "Private Sub cmdAddOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles cmdAddOption.Click"

    Private Sub cmdAddOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddOption.Click
        Try
            _AttManager = Session("AttributeManager")
            If IsNothing(_AttManager) = True Then
                RaiseEvent AddDone("", e)
                Exit Sub
            End If
            If MakeNewDetail() Then
                ClearObjects()
                '    'RE-
                If _AttManager.Attribute.AttributeDetails.Count = 0 Then
                    Dim newCount As String = (arNEWDetails.Count + 1).ToString
                    ddDetailOrder.Items.Add(newCount)

                    ddDetailOrder.SelectedIndex = ddDetailOrder.Items.Count - 1
                Else
                    LoadOrderCount()
                    ddDetailOrder.SelectedIndex = ddDetailOrder.Items.Count - 1
                End If
                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                    If _AttManager.ProductInventory.InventoryTracked Then
                        AddJavaMSG(cmdSave, JavaMessage.Custom, "Inventory Stock will be overwritten. Are You Sure You Want to Save Changes?")
                    End If
                End If
            End If
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub


#End Region

#Region "Private Sub ClearObjects()"
    '##SUMMARY  Clears all interface values
    Private Sub ClearObjects()
        ''atribute
        Try
            txtDetailName.Text = ""
            UploadControl1.FileText = ""
            txtWeight.Text = ""
            ddDetailOrder.Items.Clear()
            WeightType.ClearSelection()
            PriceType.ClearSelection()
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Sub

#End Region

#Region "Private Sub MakeNewDetail()"
    '##SUMMARY   Makes a New attribute Detail
    '##SUMMARY   object
    Private Function MakeNewDetail() As Boolean
        Dim obj As CAttributeDetail
        Try
            If Detail_OKTOADD() Then
                If txtDetailName.Text.Trim.Length <> 0 Then
                    If _AttManager.CurrentEditMode = CAttributeManagement.EditMode.edit Then
                        obj = _AttManager.AttributeDetail
                    Else
                        obj = New CAttributeDetail()
                    End If
                    arNEWDetails = Session("NewDetails")
                    If IsNothing(arNEWDetails) Then
                        arNEWDetails = New ArrayList()
                    End If
                    If _AttManager.WorkMode = AttributeWorkMode.Template Then
                        obj.UID = _AttManager.NewDetailID
                    Else
                        obj.UID = 0 - (_AttManager.NewDetailID + 1)
                    End If

                    obj.AttributeID = _AttManager.Attribute.UID
                    obj.FilePath = GetFileName(UploadControl1.FileText)
                    obj.Name = Me.txtDetailName.Text
                    obj.Order = Me.ddDetailOrder.SelectedItem.Text
                    obj.Price = CDec("0" & Me.txtPrice.Text)
                    obj.PriceChange = Me.PriceType.SelectedItem.Value
                    obj.Weight = CDec("0" & Me.txtWeight.Text)
                    obj.WeightChange = Me.WeightType.SelectedItem.Value
                    arNEWDetails.Add(obj)
                    _AttManager.NewDetailCount = arNEWDetails.Count + 1
                    Session("NewDetails") = arNEWDetails
                End If
                Return True
                Exit Function
            End If
            Return False
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Function

#End Region

#Region "Private Function Detail_OKTOADD() As Boolean"
    '##SUMMARY   Verifies that requierd detail information 
    '##SUMMARY   exist
    Private Function Detail_OKTOADD() As Boolean
        Try
            Dim objAtt As CAttributeDetail
            If txtDetailName.Text.Length = 0 Then
                If IsNothing(arNEWDetails) = False Then
                    If arNEWDetails.Count = 0 Then
                        Me.ErrorMessage.Text = "Please enter a name!"
                        Me.ErrorMessage.Visible = True
                        Return False
                        Exit Function
                    End If
                Else
                    Return False
                    Exit Function
                End If
            End If
            If PriceType.SelectedItem.Value <> 0 Then
                If Not IsNumeric(Me.txtPrice.Text) Then
                    Me.ErrorMessage.Text = "Please enter a  Price!"
                    Me.ErrorMessage.Visible = True
                    Return False
                    Exit Function
                End If
            End If
            If WeightType.SelectedItem.Value <> 0 Then
                If Not IsNumeric(Me.txtWeight.Text) Then
                    Me.ErrorMessage.Text = "Please enter a  Weight!"
                    Me.ErrorMessage.Visible = True
                    Return False
                    Exit Function
                End If
            End If
            If UploadControl1.FileText.Length > 255 Then
                Me.ErrorMessage.Text = "File download location length must be less then 255 characters!"
                Me.ErrorMessage.Visible = True
                Return False
                Exit Function
            End If

            arNEWDetails = Session("NewDetails")
            If IsNothing(arNEWDetails) = False Then
                If arNEWDetails.Count <> 0 Then
                    For Each objAtt In arNEWDetails
                        If objAtt.Name.ToLower = txtDetailName.Text.ToLower Then
                            Me.ErrorMessage.Text = "Attribute Detail " & txtDetailName.Text & " already exist!"
                            Me.ErrorMessage.Visible = True
                            Return False
                            Exit Function
                        End If
                    Next
                End If
            End If
            For Each objAtt In _AttManager.Attribute.AttributeDetails
                If objAtt.Name.ToLower = txtDetailName.Text.ToLower Then
                    If IsNothing(_AttManager.AttributeDetail) = False Then
                        If _AttManager.AttributeDetail.Name <> objAtt.Name Then
                            Me.ErrorMessage.Text = "Attribute Detail " & txtDetailName.Text & " already exist!"
                            Me.ErrorMessage.Visible = True
                            Return False
                            Exit Function
                        End If
                    Else
                        Me.ErrorMessage.Text = "Attribute Detail " & txtDetailName.Text & " already exist!"
                        Me.ErrorMessage.Visible = True
                        Return False
                    End If
                End If
            Next

            Return True
        Catch err As SystemException
            Me.ErrorMessage.Text = err.Message
            Me.ErrorMessage.Visible = True
        End Try
    End Function
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

#Region "Private Sub AddJavaMSG(ByVal objButton As LinkButton, ByVal MsgType As JavaMessage)"
    '##SUMMARY  Adds Java message to button onClick event 
    Private Sub AddJavaMSG(ByVal objButton As LinkButton, ByVal MsgType As JavaMessage, Optional ByVal sMsg As String = "")
        If IsNothing(objButton) = False Then
            Select Case MsgType

                Case JavaMessage.ApplyChanges
                    objButton.Attributes.Add("onclick", "javascript:alert('Must Apply Template before Editing');return false;")

                Case JavaMessage.Confirm
                    objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Delete This Item?" & "');")
                Case JavaMessage.OverWriteInventory
                    objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Inventory Stock will be overwritten. Are You Sure You Want to Delete This Item?" & "');")
                Case JavaMessage.Custom
                    objButton.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & sMsg & "');")
            End Select
        End If
    End Sub

#End Region

End Class
