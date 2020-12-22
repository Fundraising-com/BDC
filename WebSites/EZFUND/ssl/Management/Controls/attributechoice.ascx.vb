Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml
Public MustInherit Class attributechoice
    Inherits System.Web.UI.UserControl

#Region "Class Members"

    Protected WithEvents DDTemplates As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdApply As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Private _AttManager As CAttributeManagement

#End Region   

#Region "Class Events"

    Event AddNew As EventHandler
    Event ApplyTemplate As EventHandler
    Event TemplateChanged As EventHandler

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

#Region "Public Sub LoadChoices()"
    '##SUMMARY   Loads available attribute templates to the drop down
    Public Sub LoadChoices()
        Dim i As Integer
        If IsNothing(_AttManager) Then
            _AttManager = Session("AttributeManager")
        End If
        Dim obj As New CGenericDDContainer()
        obj.ID = 0
        obj.Name = ""
        DDTemplates.ClearSelection()
        Dim ar As ArrayList = _AttManager.TemplateChoices
        ar.Insert(0, obj)
        DDTemplates.DataSource = ar
        DDTemplates.DataBind()
        For i = 0 To DDTemplates.Items.Count - 1
            If DDTemplates.Items(i).Text = _AttManager.TemplateName Then
                DDTemplates.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

#End Region   

#Region "Private Sub DDTemplates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDTemplates.SelectedIndexChanged"

	Private Sub DDTemplates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDTemplates.SelectedIndexChanged
        Dim obj As DropDownList = sender

        If DDTemplates.SelectedItem.Text <> "" Then
            Me.cmdApply.Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Are You Sure You Want to Apply This Template Item?" & "');")
        Else
            Me.cmdApply.Attributes.Remove("onclick")
        End If

        RaiseEvent TemplateChanged(sender, e)


     
    End Sub

#End Region     

   

#Region "Private Sub cmdApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdApply.Click"

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        RaiseEvent ApplyTemplate(DDTemplates.SelectedItem.Value, e)
    End Sub

#End Region

End Class
