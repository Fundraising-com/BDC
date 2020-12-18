
Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml
Public MustInherit Class attTemplates
    Inherits System.Web.UI.UserControl

#Region "Class Members"

    Protected WithEvents lblAttName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAttDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrice As System.Web.UI.WebControls.Label
    Protected WithEvents lblWeight As System.Web.UI.WebControls.Label
    Protected WithEvents lblFile As System.Web.UI.WebControls.Label
    Protected WithEvents DLAttributes As System.Web.UI.WebControls.DataList
    Protected WithEvents dlAttributeDetail As System.Web.UI.WebControls.DataList
    Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlAddNew As System.Web.UI.WebControls.Panel
    Protected WithEvents DDTemplates As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlTemplate As System.Web.UI.WebControls.Panel
    Protected WithEvents Cell1 As System.Web.UI.HtmlControls.HtmlTableCell
    Private _AttManager As CAttributeManagement
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Attdetailctrl1 As attdetailctrl
    Protected WithEvents AttMainctrl1 As attMainctrl
    Public ShowTemplates As Boolean = False
    Private _TemplateCount As Integer
    Protected WithEvents txtTemplateName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAttName As System.Web.UI.WebControls.TextBox
    Protected WithEvents NewAtt As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ddType As System.Web.UI.WebControls.DropDownList
    Private _CurTemplate As Integer = 0

#End Region

#Region "Class Enums"

    Private Enum JavaMessage
        Confirm = 1
        ApplyChanges = 2
        OverWriteInventory = 3
        Custom = 4
    End Enum

#End Region

#Region "Class Events"
    Event ModifyMode As EventHandler
    Event ShowMessage As EventHandler
    Event TemplateError As EventHandler
    Event ShowChoices As EventHandler
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
        If Not IsPostBack Then
            Attdetailctrl1.Visible = False
            AttMainctrl1.Visible = False
        End If

    End Sub

#End Region

#Region "Public Sub DeleteAttribute(ByVal sender As Object, ByVal e As System.EventArgs)"
    '##SUMMARY   Deletes the attribute selected 
    Public Sub DeleteAttribute(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = sender


        _AttManager = Session("AttributeManager")


        Try
            _AttManager = Session("AttributeManager")
            If IsNothing(_AttManager) = True Then
                ' SetChoices()
                Exit Sub
            End If
            If obj.CommandName = "0" Then
                _AttManager.Attribute_TO_Edit = CAttributeManagement.AttributeMode.Main
                _AttManager.CurrentMainID = CLng(obj.CommandArgument)
                If Not IsNothing(_AttManager.Attribute) Then
                    _AttManager.Attributes.Remove(_AttManager.Attribute)
                End If
            Else
                _AttManager.Attribute_TO_Edit = CAttributeManagement.AttributeMode.Detail
                _AttManager.CurrentDetailID = CLng(obj.CommandArgument)
                If Not IsNothing(_AttManager.AttributeDetail) Then
                    _AttManager.Attribute.AttributeDetails.Remove(_AttManager.AttributeDetail)

                End If
            End If
            _AttManager.DeleteItem()
            DLAttributes.DataSource = _AttManager.Attributes
            DLAttributes.DataBind()
            ReDrill()
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub

#End Region

#Region "Public Sub AddDetail(ByVal sender As Object, ByVal e As System.EventArgs)"

    Public Sub AddDetail(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            _AttManager = Session("AttributeManager")
            Dim obj As LinkButton = sender
            _AttManager.Attribute_TO_Edit = CAttributeManagement.AttributeMode.Main
            _AttManager.CurrentEditMode = _AttManager.EditMode.add
            _AttManager.CurrentMainID = CLng(obj.CommandArgument)
            If _AttManager.Attribute.AttributeType = SystemBase.tAttributeType.Custom Then
                RaiseEvent TemplateError("Not Allowed On Custom Attributes", EventArgs.Empty)
                Exit Sub
            End If
            Me.Table1.Visible = False
            _AttManager.NewDetailCount = 0
            Session("AttributeManager") = _AttManager
            Attdetailctrl1.Manager = _AttManager
            Attdetailctrl1.AddNew()
            Me.Attdetailctrl1.Visible = True
            NewAtt.Visible = False
            RaiseEvent ShowChoices(False, e)
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub


#End Region

#Region "Public Sub EditAttribute(ByVal sender As Object, ByVal e As System.EventArgs)"

    '##SUMMARY   raises an event to set visible the attributeadd control  
    '##SUMMARY   and hide this control

    Public Sub EditAttribute(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As LinkButton = sender
        Try
            _AttManager = Session("AttributeManager")

            _AttManager.Attribute_TO_Edit = CInt(obj.CommandName)
            _AttManager.CurrentEditMode = CAttributeManagement.EditMode.edit
            If obj.CommandName = "0" Then
                _AttManager.CurrentMainID = CLng(obj.CommandArgument)
                AttMainctrl1.Manager = _AttManager
                AttMainctrl1.LoadMe()
                AttMainctrl1.Visible = True
                NewAtt.Visible = False
                Me.Table1.Visible = False
            Else
                _AttManager.CurrentDetailID = CLng(obj.CommandArgument)
                Me.Table1.Visible = False
                _AttManager.NewDetailCount = 0
                Session("AttributeManager") = _AttManager
                Attdetailctrl1.Manager = _AttManager
                Attdetailctrl1.LoadMe()
                Me.Attdetailctrl1.Visible = True
                NewAtt.Visible = False
            End If
            RaiseEvent ShowChoices(False, e)
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)
        End Try
    End Sub

#End Region

#Region "Public Sub LoadFromTemplate(ByVal sTemplate As String)"

    Public Sub LoadFromTemplate(ByVal sTemplate As String)
        Try
            If IsNothing(_AttManager) = True Then
                _AttManager = Session("AttributeManager")
                If IsNothing(_AttManager) = True Then
                    _AttManager = New CAttributeManagement()
                End If
            End If
            AttMainctrl1.Visible = False
            Attdetailctrl1.Visible = False
            Me.Table1.Visible = True

            If sTemplate.ToLower <> "none" Then
                If _AttManager.WorkMode = AttributeWorkMode.Template Then
                    Dim _xmlpath As String
                    '                    AddJavaMSG(cmdDeleteTemplate, JavaMessage.Custom, "Are you sure you want to delete this template?")
                    _xmlpath = Server.MapPath("")
                    _xmlpath = _xmlpath & "\SFTemplates\" & sTemplate
                    _xmlpath = Replace(_xmlpath, "\\", "\")
                    _AttManager.XMLPath = _xmlpath

                    DLAttributes.DataSource = _AttManager.Attributes
                    DLAttributes.DataBind()
                Else
                    'display Live and Template Atts
                    Dim objAtt As CAttribute
                    Dim objTemplateAtt As CAttribute
                    Dim CanAdd As Boolean = True

                    For Each objAtt In _AttManager.Attributes
                        CanAdd = True
                        For Each objTemplateAtt In _AttManager.TemplateAttributes
                            If objTemplateAtt.Name = objAtt.Name Then
                                CanAdd = False
                            End If
                        Next
                        If CanAdd Then
                            _AttManager.TemplateAttributes.Add(objAtt)
                        End If
                    Next
                    DLAttributes.DataSource = _AttManager.TemplateAttributes
                    DLAttributes.DataBind()
                    ReDrill()
                End If
            End If

            Session("AttributeManager") = _AttManager
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try

    End Sub


#End Region

#Region "Public Sub LoadLive()"

    Public Sub LoadLive()
        Try
            If IsNothing(_AttManager) = True Then
                _AttManager = Session("AttributeManager")
                If IsNothing(_AttManager) = True Then
                    Exit Sub
                End If
            End If
            AttMainctrl1.Visible = False
            Attdetailctrl1.Visible = False
            Me.Table1.Visible = True
            'btnAdd.Attributes.Remove("onclick")

            DLAttributes.DataSource = _AttManager.Attributes
            DLAttributes.DataBind()
            ReDrill()
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub

#End Region

#Region "Private Sub DLAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DLAttributes.ItemCreated"
    Private Sub DLAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DLAttributes.ItemCreated
        Try
            Dim obj As CAttribute = e.Item.DataItem

            If Not IsNothing(obj) Then
                If _AttManager.CanEdit = True Then
                    If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then

                        If _AttManager.ProductInventory.InventoryTracked Then
                            AddJavaMSG(e.Item.FindControl("cmdDelete"), JavaMessage.OverWriteInventory)
                            AddJavaMSG(btnAdd, JavaMessage.Custom, "Inventory Stock will be overwritten. Are You Sure You Want to Add Attribute?")

                        Else
                            AddJavaMSG(e.Item.FindControl("cmdDelete"), JavaMessage.Confirm)
                        End If
                    Else
                        AddJavaMSG(e.Item.FindControl("cmdDelete"), JavaMessage.Confirm)
                    End If
                Else
                    AddJavaMSG(e.Item.FindControl("cmdDelete"), JavaMessage.ApplyChanges)
                    AddJavaMSG(e.Item.FindControl("cmdEdit"), JavaMessage.ApplyChanges)
                    AddJavaMSG(e.Item.FindControl("cmdAddDetail"), JavaMessage.ApplyChanges)
                    AddJavaMSG(btnAdd, JavaMessage.ApplyChanges)
                End If

                If obj.AttributeType = SystemBase.tAttributeType.Custom Then
                    Exit Sub
                End If
                dlAttributeDetail = e.Item.FindControl("dlAttributeDetail")
                _TemplateCount = obj.AttributeDetails.Count
                dlAttributeDetail.DataSource = obj.AttributeDetails
                dlAttributeDetail.DataBind()
            Else

            End If
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub

#End Region

#Region "Private Sub dlAttributeDetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAttributeDetail.ItemCreated"

    Private Sub dlAttributeDetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAttributeDetail.ItemCreated
        Try
            If _AttManager.CanEdit = True Then
                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                    If _AttManager.ProductInventory.InventoryTracked Then
                        AddJavaMSG(e.Item.FindControl("cmdDeleteDetail"), JavaMessage.OverWriteInventory)
                    Else
                        AddJavaMSG(e.Item.FindControl("cmdDeleteDetail"), JavaMessage.Confirm)
                    End If

                Else
                    AddJavaMSG(e.Item.FindControl("cmdDeleteDetail"), JavaMessage.Confirm)
                End If

            Else
                AddJavaMSG(e.Item.FindControl("cmdDeleteDetail"), JavaMessage.ApplyChanges)
                AddJavaMSG(e.Item.FindControl("cmdEditDetail"), JavaMessage.ApplyChanges)
            End If

        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub

#End Region

#Region "Public Sub HideDetails(ByVal sender As Object, ByVal e As System.EventArgs)"
    '##SUMMARY toggles attribute details visible/not visible and adds
    '##SUMMARY   the appropiate java msg if needed
    Public Sub HideDetails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objDataListItem As New DataListItem(0, ListItemType.Item)
        Dim objParent As Object = sender
        Dim objButton As LinkButton = sender

        Try
            _AttManager = Session("AttributeManager")

            While (Not objParent.GetType() Is objDataListItem.GetType)
                objParent = objParent.Parent
            End While

            objDataListItem = objParent

            pnlDetails = objDataListItem.FindControl("pnlDetails")
            If IsNothing(pnlDetails) = False Then
                If pnlDetails.Visible = True Then
                    pnlDetails.Visible = False
                    Session("expandedItem") = Nothing


                Else
                    pnlDetails.Visible = True
                    Session("expandedItem") = objButton.CommandArgument.ToString
                    Dim cmdDeleteMain As LinkButton
                    Dim cmdEditMain As LinkButton

                    Dim dlDetails As DataList
                    Dim dlDetailItem As DataListItem

                    cmdDeleteMain = objDataListItem.FindControl("cmdDelete")
                    If IsNothing(cmdDeleteMain) = False Then
                        If _AttManager.CanEdit = True Then
                            For Each dlDetailItem In Me.DLAttributes.Items
                                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                                    If _AttManager.ProductInventory.InventoryTracked Then
                                        AddJavaMSG(dlDetailItem.FindControl("cmdDelete"), JavaMessage.OverWriteInventory)
                                        AddJavaMSG(btnAdd, JavaMessage.Custom, "Inventory Stock will be overwritten. Are You Sure You Want to Add Attribute?")
                                    Else
                                        AddJavaMSG(dlDetailItem.FindControl("cmdDelete"), JavaMessage.Confirm)
                                    End If
                                Else
                                    AddJavaMSG(dlDetailItem.FindControl("cmdDelete"), JavaMessage.Confirm)
                                End If
                                Dim objLabel As Label = dlDetailItem.FindControl("lblAttName")
                                'Dim oText As TextBox = dlDetailItem.FindControl("atttype")
                                'If oText.Text = "Custom" Then
                                '    objLabel.Text = "Customer Defined Attribute"
                                'End If

                            Next
                            dlDetails = objDataListItem.FindControl("dlAttributeDetail")
                            _TemplateCount = dlDetails.Items.Count
                            For Each dlDetailItem In dlDetails.Items

                                If _AttManager.WorkMode = SystemBase.AttributeWorkMode.Live Then
                                    If _AttManager.ProductInventory.InventoryTracked Then
                                        AddJavaMSG(dlDetailItem.FindControl("cmdDeleteDetail"), JavaMessage.OverWriteInventory)
                                    Else
                                        AddJavaMSG(dlDetailItem.FindControl("cmdDeleteDetail"), JavaMessage.Confirm)
                                    End If
                                Else
                                    AddJavaMSG(dlDetailItem.FindControl("cmdDeleteDetail"), JavaMessage.Confirm)
                                End If

                            Next
                        Else
                            For Each dlDetailItem In Me.DLAttributes.Items
                                AddJavaMSG(dlDetailItem.FindControl("cmdEdit"), JavaMessage.ApplyChanges)
                                AddJavaMSG(dlDetailItem.FindControl("cmdDelete"), JavaMessage.ApplyChanges)
                                AddJavaMSG(dlDetailItem.FindControl("cmdAddDetail"), JavaMessage.ApplyChanges)
                                'cmdAddDetail
                            Next
                            dlDetails = objDataListItem.FindControl("dlAttributeDetail")
                            _TemplateCount = dlDetails.Items.Count
                            AddJavaMSG(btnAdd, JavaMessage.ApplyChanges)
                            For Each dlDetailItem In dlDetails.Items
                                AddJavaMSG(dlDetailItem.FindControl("cmdDeleteDetail"), JavaMessage.ApplyChanges)
                                AddJavaMSG(dlDetailItem.FindControl("cmdEditDetail"), JavaMessage.ApplyChanges)

                            Next
                        End If
                    End If
                End If
            End If

        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub

#End Region

#Region "Private Sub ReDrill()"

	Private Sub ReDrill()

        If IsNothing(Session("expandedItem")) = False Then
            Dim sName As String = Session("expandedItem")
            Dim objItem As DataListItem
            For Each objItem In DLAttributes.Items
                Dim objButton As LinkButton
                objButton = objItem.FindControl("cmdDrill")
                If IsNothing(objButton) = False Then
                    If objButton.CommandArgument = sName Then
                        Dim pnl As Panel
                        pnl = objItem.FindControl("pnlDetails")
                        If IsNothing(pnl) = False Then
                            pnl.Visible = True
                            Exit For
                        End If
                    End If
                End If
            Next
        End If

    End Sub

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

#Region "Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) handles btnAdd.Click"

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If txtAttName.Text.Trim.Length <> 0 Then
                Dim obj As CAttribute
                _AttManager = Session("AttributeManager")
                For Each obj In _AttManager.Attributes
                    If obj.Name.ToLower = txtAttName.Text.Trim.ToLower Then
                        RaiseEvent TemplateError("Attribute " & obj.Name & " already exist!", EventArgs.Empty)
                        Exit Sub
                    End If
                Next
                _AttManager.Attribute_TO_Edit = CAttributeManagement.AttributeMode.Main
                _AttManager.CurrentEditMode = _AttManager.EditMode.add
                If ddType.SelectedItem.Value.ToString = "0" Then
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
                    txtAttName.Text = ""

                    DLAttributes.DataSource = _AttManager.Attributes
                    DLAttributes.DataBind()
                    ReDrill()
                Else
                    NewAtt.Visible = False
                    Me.Table1.Visible = False
                    ddType.ClearSelection()
                    AttMainctrl1.Manager = _AttManager
                    AttMainctrl1.addnew(True, txtAttName.Text)
                    txtAttName.Text = ""
                    Me.AttMainctrl1.Visible = True
                    RaiseEvent ShowChoices(False, e)
                End If
            End If

        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub


#End Region

#Region "Private Sub Attdetailctrl1_AddDone(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attdetailctrl1.AddDone"

    Private Sub Attdetailctrl1_AddDone(ByVal sender As Object, ByVal e As System.EventArgs) Handles Attdetailctrl1.AddDone
        Try
            _AttManager = Session("AttributeManager")
            Attdetailctrl1.Visible = False
            Me.Table1.Visible = True
            NewAtt.Visible = True
            DLAttributes.DataSource = _AttManager.Attributes
            DLAttributes.DataBind()
            ReDrill()
            If sender.ToString.Trim <> "" Then
                RaiseEvent ShowMessage(sender, e)
            End If
            RaiseEvent ShowChoices(True, e)
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub


#End Region

#Region "Private Sub AttMainctrl1_AddDone(ByVal sender As Object, ByVal e As System.EventArgs) Handles AttMainctrl1.AddDone"

    Private Sub AttMainctrl1_AddDone(ByVal sender As Object, ByVal e As System.EventArgs) Handles AttMainctrl1.AddDone
        Try
            _AttManager = Session("AttributeManager")
            Me.Table1.Visible = True
            NewAtt.Visible = True
            AttMainctrl1.Visible = False
            DLAttributes.DataSource = _AttManager.Attributes
            DLAttributes.DataBind()
            ReDrill()
            If sender.ToString.Trim <> "" Then
                RaiseEvent ShowMessage(sender, e)
            End If
            RaiseEvent ShowChoices(True, e)
        Catch err As SystemException
            RaiseEvent TemplateError(err.Message, EventArgs.Empty)

        End Try
    End Sub


#End Region

End Class
