Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

Partial Class BundleTemplateControl
    Inherits CWebControl

    Protected WithEvents cmdSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ProductList As System.Web.UI.WebControls.DataList
    Protected WithEvents selectProducts As System.Web.UI.WebControls.LinkButton
    Event TemplateSaved As EventHandler
    Event DisplayError As EventHandler

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property GetPrice(ByVal prodId As Long, ByVal prodType As ProductType, ByVal prodPrice As Decimal) As String
        Get
            Return Format(IIf(prodType = ProductType.Bundle OrElse prodType = ProductType.BundleSubscription, (New CProductManagement(prodId)).Price, prodPrice), "c")
        End Get
    End Property

#End Region

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Me.Visible = False OrElse IsNothing(Session("ProductID")) Then
            Exit Sub
        End If
        Dim prodId As Long = Session("ProductID")
        hdnID.Value = prodId
        'Tee 8/7/2007 product configurator
        Dim objProdManagement As New CProductManagement(prodId)
        MakeCommonVisible(objProdManagement.ProductType, False)
        CType(Me.Parent.FindControl("lblPDName"), Label).Text = objProdManagement.Name
        btnSaveAll.Attributes.Add("OnClick", "return ValidateAllSteps()")
        'end Tee
        Dim objTemplateBase As New CCustomizeBundleTemplateBase
        Dim stepID As Integer = Session("StepID")
        Dim obj As New CCustomizeBundleTemplate
        If Not IsNothing(Session("ArrChecked")) AndAlso stepID <> 0 Then
            Dim ProdList As New ArrayList
            ProdList = Session("ArrChecked")
            Session("ArrChecked") = Nothing
            SetModifiedFlag(stepID, ProdList, obj.GetCustomizeBundleTemplate(prodId))
            BindData(obj.GetCustomizeBundleTemplate(prodId, ProdList, stepID))
        ElseIf Not IsPostBack Then
            'Load all the steps for the template
            BindData(obj.GetCustomizeBundleTemplate(prodId))
        End If
    End Sub
#End Region

#Region "Private Sub BindData(ByVal objTemplateBase As CCustomizeBundleTemplateBase)"
    Private Sub BindData(ByVal objTemplateBase As CCustomizeBundleTemplateBase)
        stepList.DataSource = objTemplateBase.StepDetails
        stepList.DataBind()
    End Sub
#End Region

#Region "Public Sub AddNewStep(ByVal sender As System.Object, ByVal e as System.EventArgs)"
    Public Sub AddNewStep(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim prodID As Long = CLng(hdnID.Value)
        Dim item As DataListItem
        item = sender.Parent
        Dim obj As New CCustomizeBundleTemplate
        Dim objDetail As New CCustomizeBundleTemplateDetailBase
        Try
            objDetail.StepID = 0
            objDetail.StepName = CType(item.FindControl("txtStepName"), TextBox).Text().Trim
            objDetail.SelectableQuantity = 0
            If Not IsNumeric(CType(item.FindControl("txtDisplayOrd"), TextBox).Text().Trim) Then
                RaiseEvent DisplayError("Enter a numeric value for the Display Order", System.EventArgs.Empty)
                Exit Sub
            End If
            objDetail.DisplayOrder = CType(item.FindControl("txtDisplayOrd"), TextBox).Text().Trim
            obj.AddNewStep(prodID, objDetail)
            Response.Redirect("BundleComponents.aspx?ProdType=4")
        Catch ex As Exception
            RaiseEvent DisplayError("Could not add New Step: " & ex.Message, System.EventArgs.Empty)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Public Sub DeleteStep(ByVal sender As System.Object, ByVal e As System.EventArgs)"
    Public Sub DeleteStep(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim prodID As Integer = CInt(hdnID.Value)
        Dim obj As New CCustomizeBundleTemplate
        Try
            obj.DeleteCustomizeBundleTemplateDetail(CInt(sender.CommandName))
            Response.Redirect("BundleComponents.aspx?ProdType=4")
        Catch ex As Exception
            CType(Me.Parent.FindControl("ErrorMessage"), Label).Text = "Error Deleting this step: " & ex.Message
            CType(Me.Parent.FindControl("ErrorMessage"), Label).Visible = True
        End Try
    End Sub
#End Region

#Region "Private Sub selectProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectProducts.Click"
    Public Sub SelectProductsClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectProducts.Click
        Dim ProdIDList As New ArrayList
        Dim obj As New CCustomizeBundleTemplate
        ProdIDList = obj.GetProductIDList(CInt(sender.CommandName))
        Session("ProdID") = 0
        Session("ApplyTo") = "9"
        Session("ReturnPage") = "BundleComponents.aspx?ProdType=4"
        Session("ArrChecked") = ProdIDList
        Session("stepID") = CInt(sender.CommandName)
        Session("ManagingProducts") = True
        Response.Redirect("multiselect.aspx")
    End Sub
#End Region

#Region "Private Sub stepList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles stepList.ItemCommand"
    Private Sub stepList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles stepList.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objBtn As LinkButton = e.CommandSource
            If objBtn.ID.IndexOf("btnSaveStep") = -1 Then
                Exit Sub
            End If
            Dim obj As New CCustomizeBundleTemplate
            Dim bundleDetail As New CCustomizeBundleTemplateDetailBase
            Dim bundleProd As CCustomizeBundleProductDetailBase
            bundleDetail.StepID = CInt(objBtn.CommandName)
            bundleDetail.StepName = CType(e.Item.FindControl("txtStep"), TextBox).Text
            bundleDetail.SelectableQuantity = CInt(CType(e.Item.FindControl("txtSelectable"), TextBox).Text)
            bundleDetail.DisplayOrder = CInt(CType(e.Item.FindControl("txtDisplayOrder"), TextBox).Text)
            Dim dl As DataList = CType(e.Item.FindControl("ProductList"), DataList)
            For Each item As DataListItem In dl.Items
                bundleProd = New CCustomizeBundleProductDetailBase
                bundleProd.Quantity = CInt(CType(item.FindControl("quantity"), TextBox).Text)
                bundleProd.DisplayOrder = CInt(CType(item.FindControl("tbDisplayOrder"), TextBox).Text)
                bundleProd.ProductID = CInt(CType(item.FindControl("hidProdId"), HtmlInputHidden).Value)
                bundleDetail.ProductDetails.Add(bundleProd)
            Next
            obj.UpdateCustomizeBundleStep(bundleDetail)
            BindData(obj.GetCustomizeBundleTemplate(CLng(hdnID.Value)))
            hidModified.Value = ""
        End If
    End Sub
#End Region

#Region "Private Sub stepList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles stepList.ItemDataBound"
    Private Sub stepList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles stepList.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim hidprodCount As HtmlInputHidden = CType(e.Item.FindControl("hidProdCount"), HtmlInputHidden)
            Dim txtSelectable As TextBox = CType(e.Item.FindControl("txtSelectable"), TextBox)
            Dim txtDisplayOrd As TextBox = CType(e.Item.FindControl("txtDisplayOrder"), TextBox)
            Dim txtStep As TextBox = CType(e.Item.FindControl("txtStep"), TextBox)
            CType(e.Item.FindControl("btnSaveStep"), LinkButton).Attributes.Add("OnClick", _
            "return ValidateStepDetails('" & hidprodCount.UniqueID & "','" & txtSelectable.UniqueID _
            & "', '" & txtDisplayOrd.UniqueID & "', '" & txtStep.UniqueID & "', '" _
            & CType(e.Item.FindControl("btnSaveStep"), LinkButton).CommandName & "');")
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim txtDisplayOrdId As String = CType(e.Item.FindControl("txtDisplayOrd"), TextBox).UniqueID
            Dim txtStepNameId As String = CType(e.Item.FindControl("txtStepName"), TextBox).UniqueID
            CType(e.Item.FindControl("btnAddNewStep"), LinkButton).Attributes.Add("OnClick", _
            "return ValidateNewStep('" & txtDisplayOrdId & "', '" & txtStepNameId & "');")
        End If
    End Sub
#End Region

#Region "Private Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAll.Click"
    Private Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAll.Click
        Dim obj As New CCustomizeBundleTemplate
        Dim bundleDetail As CCustomizeBundleTemplateDetailBase
        For Each item As DataListItem In stepList.Items
            Dim bundleProd As CCustomizeBundleProductDetailBase
            bundleDetail = New CCustomizeBundleTemplateDetailBase
            bundleDetail.StepID = CInt(CType(item.FindControl("btnSaveStep"), LinkButton).CommandName)
            bundleDetail.StepName = CType(item.FindControl("txtStep"), TextBox).Text
            bundleDetail.SelectableQuantity = CInt(CType(item.FindControl("txtSelectable"), TextBox).Text)
            bundleDetail.DisplayOrder = CInt(CType(item.FindControl("txtDisplayOrder"), TextBox).Text)
            Dim dl As DataList = CType(item.FindControl("ProductList"), DataList)
            For Each _item As DataListItem In dl.Items
                bundleProd = New CCustomizeBundleProductDetailBase
                bundleProd.Quantity = CInt(CType(_item.FindControl("quantity"), TextBox).Text)
                bundleProd.DisplayOrder = CInt(CType(_item.FindControl("tbDisplayOrder"), TextBox).Text)
                bundleProd.ProductID = CInt(CType(_item.FindControl("hidProdId"), HtmlInputHidden).Value)
                bundleDetail.ProductDetails.Add(bundleProd)
            Next
            obj.UpdateCustomizeBundleStep(bundleDetail)
        Next
        BindData(obj.GetCustomizeBundleTemplate(CLng(hdnID.Value)))
    End Sub
#End Region

#Region "Private Sub SetModifiedFlag(ByVal stepId As Integer, ByVal currList As ArrayList, ByVal template As CCustomizeBundleTemplateBase)"
    Private Sub SetModifiedFlag(ByVal stepId As Integer, ByVal currList As ArrayList, ByVal template As CCustomizeBundleTemplateBase)
        For Each item As CCustomizeBundleTemplateDetailBase In template.StepDetails
            If item.StepID = stepId Then
                Dim initList As New ArrayList
                For Each _item As CCustomizeBundleProductDetailBase In item.ProductDetails
                    initList.Add(_item.ProductID)
                Next
                initList.Sort()
                currList.Sort()
                hidModified.Value = ""
                If currList.Count <> initList.Count Then
                    hidModified.Value = stepId & "::" & item.StepName
                Else
                    For Each i As Long In currList
                        If Not initList.Contains(i) Then
                            hidModified.Value = stepId & "::" & item.StepName
                            Exit For
                        End If
                    Next
                End If
            End If
        Next
    End Sub
#End Region

End Class
