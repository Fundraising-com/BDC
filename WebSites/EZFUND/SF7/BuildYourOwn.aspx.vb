Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management


Partial Class BuildYourOwn
    Inherits CWebPage

    Protected WithEvents BuildYourOwnControl1 As BuildYourOwnControl
    Protected WithEvents BuildDessert As BuildYourOwnControl
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents stepDetails As BuildYourOwnControl

    Private ProductID As Long
    Private qty As Long = 0
    Private nextDYOM As Integer = 0
    Private DYOMList As New ArrayList
    Dim m_oProduct As CProduct

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        SetPageTitle = "Build Your Own Bundle"
        SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
        imgAddToCart.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("AddToOrder").Attributes("Filepath").Value
        If Not IsNothing(Request.QueryString("ID")) Then
            'Tee 8/27/2007 product configurator
            btnContinue.Attributes.Add("OnClick", "return ValidateSelection()")
            'end Tee
            ProductID = Request.QueryString("ID")
            qty = CLng(Request.QueryString("Qty").ToString)

            Dim objMeal As CCustomizeBundleTemplateBase
            Dim objProd As CProductManagement
            Dim objMealEx As New CCustomizeBundleTemplate
            objProd = New CProductManagement(ProductID)
            m_oProduct = New CProduct(objProd.GetProductRow(ProductID, 0).Products.Rows(0), 0)
            objMeal = objMealEx.GetCustomizeBundleTemplate(ProductID, True)
            objMeal.StepDetails.Sort()
            Dim jscript As New System.Text.StringBuilder
            jscript.Append("<script language='javascript' id='stepCount'>")
            jscript.Append("var numberOfSteps = " & objMeal.StepDetails.Count & ";")
            jscript.Append("</script>")
            ClientScript.RegisterClientScriptBlock(Me.GetType, "stepCountScript", jscript.ToString())

            If Not IsPostBack Then
                listOfSteps.DataSource = objMeal.StepDetails
                listOfSteps.DataBind()
                ' begin: JDB - Customer Bundle Rules
                Me.bRequeryJavascriptArray = True
                ' end: JDB - Customer Bundle Rules
            End If
        Else
            Exit Sub
        End If
    End Sub

    ' begin: JDB - Customer Bundle Rules
    Protected Sub RegisterJavascriptArray()
        ' note: session should not be used here but viewstate is not working correctly
        Dim sScript As String
        If Me.bRequeryJavascriptArray Then
            For iDataItemIndex As Integer = 0 To Me.aoJavascriptDataItem.Length - 1
                Me.AddToJavascriptArray(Me.aoJavascriptDataItem(iDataItemIndex))
            Next
            sScript = Me.GetAttributeValidationJavascript()
            Session(Me.ClientID + "AttributeValidationJavascript") = sScript
        Else
            sScript = Session(Me.ClientID + "AttributeValidationJavascript")
        End If
        ClientScript.RegisterStartupScript(Me.GetType, "CustomerBundleRules", sScript)
    End Sub

    Private aoJavascriptDataItem() As JavascriptDataItem = {}
    Private Sub AddToJavascriptArray(ByRef oJavascriptDataItem As JavascriptDataItem)
        'new BundleOption(
        '	"1",
        '	"cup",
        '	document.getElementById("listOfSteps__ctl0_stepDetails_productList__ctl0_checkProduct")
        '	)
        '),
        Dim asArguments(4) As String
        asArguments(0) = oJavascriptDataItem.Product.ProductID
        asArguments(1) = oJavascriptDataItem.Product.Name
        asArguments(2) = "document.getElementById(""" + oJavascriptDataItem.Checkbox.ClientID + """)"
        asArguments(3) = oJavascriptDataItem.SelectableQuantity
        asArguments(4) = oJavascriptDataItem.ProductDetailCount
        Me.sJavascriptArray += String.Format("new BundleOption(""{0}"", ""{1}"", {2}, {3}, {4}),", asArguments)
    End Sub

    Private iProductId As Integer
    Private sJavascriptArray As String = ""
    Private bRequeryJavascriptArray As Boolean = False
    Private Function GetAttributeValidationJavascript() As String
        If Not IsNothing(Request.QueryString("ID")) Then
            Dim iProductID As Integer = Request.QueryString("ID")
            If Me.aoJavascriptDataItem.Length > 0 Then
                Dim sInvalidMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "Combination", "Invalid")
                Dim sOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "StockStatus", "OutOfStock")
                Dim sInvalidOrOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "LastAttributeValue", "InvalidOrOutOfStock")
                Dim sMessageVariables As String = String.Format("var sInvalid = '{0}'; var sOutOfStock = '{1}'; var sInvalidOrOutOfStock = '{2}';", sInvalidMessage, sOutOfStockMessage, sInvalidOrOutOfStockMessage)

                Dim sUnavailable As String = ""
                Dim sOutOfStock As String = ""
                Dim obj As New CCustomizeBundleTemplate
                Dim dtBundleRules As DataTable = obj.GetBundleRules(iProductID)
                For Each oRow As DataRow In dtBundleRules.Rows
                    If sUnavailable.Length > 0 Then
                        sUnavailable += ","
                    End If
                    sUnavailable += """" + oRow("BundleRuleDetail") + """"
                Next
                Dim iTotalCombinations As Integer = 1
                Dim objMealEx As New CCustomizeBundleTemplate
                Dim objMeal As CCustomizeBundleTemplateBase = objMealEx.GetCustomizeBundleTemplate(ProductID, True)
                For Each oStep As CCustomizeBundleTemplateDetailBase In objMeal.StepDetails
                    iTotalCombinations *= (Me.Factorial(oStep.ProductDetails.Count) / (Me.Factorial(oStep.ProductDetails.Count - oStep.SelectableQuantity) * Me.Factorial(oStep.SelectableQuantity)))
                    For Each oProduct As CCustomizeBundleProductDetailBase In oStep.ProductDetails
                        Dim oInventoryManagement As Inventory_Management = New Inventory_Management(oProduct.ProductID)
                        If oInventoryManagement.Inventory.InventoryTracked AndAlso (Not oInventoryManagement.Inventory.CanBackOrder) Then
                            If oInventoryManagement.Inventory.StockIsDepleted Then
                                If sOutOfStock.Length > 0 Then
                                    sOutOfStock += ","
                                End If
                                sOutOfStock += """" + oProduct.ProductID.ToString + """"
                            End If
                        End If
                    Next
                Next

                Dim sScript As String = _
                    "<script language=""Javascript"">" + _
                    sMessageVariables + _
                    "var iTotalCombinations = " + iTotalCombinations.ToString + ";" + _
                    "var aoCustomerBundleRules = new Array(" + _
                    Me.sJavascriptArray + _
                    "new Array(" + sUnavailable + ")," + _
                    "new Array(" + sOutOfStock + ")" + _
                    ");CheckBundleRuleValues(aoCustomerBundleRules,true);</script>"
                Return sScript
            End If
        End If
        Return ""
    End Function

    Public Function Factorial(ByVal i As Integer) As Integer
        Dim iFactorial As Integer = 1
        For iIndex As Integer = i To 1 Step -1
            iFactorial *= iIndex
        Next
        Return iFactorial
    End Function

    Private Sub listOfSteps_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listOfSteps.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oStepDetail As BuildYourOwnControl = e.Item.FindControl("stepDetails")
            AddHandler oStepDetail.ProductAdded, AddressOf Me.StepDetail_ProductAdded
        End If
    End Sub

    Private Sub StepDetail_ProductAdded(ByVal sender As Object, ByVal iItemIndex As Integer, ByVal oProduct As CProduct, ByVal oCheckbox As CheckBox, ByVal iSelectableQuantity As Integer, ByVal iProductDetailCount As Integer)
        Dim oJavascriptDataItem As New JavascriptDataItem
        oJavascriptDataItem.Product = oProduct
        oJavascriptDataItem.Checkbox = oCheckbox
        oJavascriptDataItem.SelectableQuantity = iSelectableQuantity
        oJavascriptDataItem.ProductDetailCount = iProductDetailCount
        ReDim Preserve Me.aoJavascriptDataItem(Me.aoJavascriptDataItem.Length)
        Me.aoJavascriptDataItem(Me.aoJavascriptDataItem.Length - 1) = oJavascriptDataItem
    End Sub
    ' end: JDB - Customer Bundle Rules

    Private Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim objMeal As CCustomizeBundleTemplateBase
        Dim objProd As New CProductManagement(ProductID)
        Dim objMealEx As New CCustomizeBundleTemplate
        Dim detailList As DataList
        Dim dItem As DataListItem
        Dim lItem As DataListItem
        'Begin custom code Sep 06
        Dim prodManage As New CProductManagement
        Dim drProd As dsProducts.ProductsRow
        Dim product As CProduct
        Dim bundledProducts As New SystemBase.CProducts
        'Tee 8/22/2007 product configurator
        Dim tempProds As New ArrayList
        ErrorMessage.Text = ""
        ErrorMessage.Visible = False
        'end Tee
        objMeal = objMealEx.GetCustomizeBundleTemplate(ProductID)
        'Get Products for all the steps
        For Each dItem In listOfSteps.Items
            tempProds.Clear() 'Tee 8/22/2007 product configurator
            stepDetails = CType(dItem.FindControl("stepDetails"), BuildYourOwnControl)
            detailList = CType(stepDetails.FindControl("productList"), DataList)
            For Each lItem In detailList.Items
                If lItem.ItemType = ListItemType.Item OrElse lItem.ItemType = ListItemType.AlternatingItem Then
                    If CType(lItem.FindControl("checkProduct"), CheckBox).Checked = True Then
                        'Tee 8/21/2007 product configurator
                        'check required attributes selection
                        Dim arrAtts As ArrayList = Set_Item_Attributes(CType(lItem.FindControl("CAttributeControl1"), CAttributeControl))
                        If IsNothing(arrAtts) Then
                            Exit Sub
                        End If
                        drProd = prodManage.GetProductRow(CLng(CType(lItem.FindControl("ProdID"), TextBox).Text), m_objCustomer.CustomerGroup).Products.Rows(0)
                        product = New CProduct(drProd, 1, arrAtts)
                        tempProds.Add(product)
                        'end Tee
                    End If
                End If
            Next
            Dim MealStep As CCustomizeBundleTemplateDetailBase

            For Each MealStep In objMeal.StepDetails
                'For j = 0 To objMeal.StepDetails.Count - 1
                If MealStep.StepID = CLng(CType(dItem.FindControl("stepID"), TextBox).Text) Then
                    If MealStep.SelectableQuantity <> tempProds.Count Then
                        ErrorMessage.Text = "Please select " & MealStep.SelectableQuantity & " item(s) in Step " & MealStep.DisplayOrder
                        ErrorMessage.Visible = True
                        Exit Sub
                    Else
                        Dim ProdDetail As CCustomizeBundleProductDetailBase
                        'Tee 8/22/2007 product configurator
                        For Each ProdDetail In MealStep.ProductDetails
                            For Each _prod As CProduct In tempProds
                                If ProdDetail.ProductID = _prod.ProductID Then
                                    _prod.BundledQuantity = ProdDetail.Quantity
                                    bundledProducts.Add(_prod)
                                End If
                            Next
                        Next
                        'end Tee
                    End If
                End If
            Next
        Next
        Session("BundledProducts") = bundledProducts
        Dim objButton As New LinkButton
        objButton.CommandArgument = CLng(Request.QueryString("Qty").ToString)
        objButton.CommandName = ProductID
        AddItemToCart(objButton, e)
    End Sub

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
        Else
            Message.Text = ""
            Message.Visible = False
        End If
    End Sub

    'Tee 8/21/2007 product configurator
#Region "Private Function Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl) As ArrayList"
    Private Function Set_Item_Attributes(ByVal oAttributeControl As CAttributeControl) As ArrayList
        Dim dlItem As DataListItem
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        Dim objAttributes As Object
        Dim sAttName As String

        If oAttributeControl Is Nothing Then
            Return Nothing
        Else
            m_OrderAttributes = New ArrayList
            'set attributes 
            DlAttributes = CType(oAttributeControl.FindControl("DlAttributes"), DataList)
            For Each dlItem In DlAttributes.Items
                'late bind based on dropdown or radiolist
                If oAttributeControl.DisplayType = CAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If
                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text

                If objAttributes.SelectedItem Is Nothing Then
                    'raise required error
                    ErrorMessage.Text = sAttName & " required"
                    ErrorMessage.Visible = True
                    Return Nothing
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    'raise required error
                    ErrorMessage.Text = sAttName & " required"
                    ErrorMessage.Visible = True
                    Return Nothing
                Else
                    Dim oAttStorage As New CAttributesSelected
                    oattStorage.UID = CLng(objAttributes.SelectedItem.Value())
                    sTemp = CType(dlItem.FindControl("AttributeID"), TextBox).Text
                    oattStorage.AttributeId = CLng(sTemp)
                    m_OrderAttributes.Add(oattStorage)
                End If
            Next
            'set Custom attributes
            DlCustomAttributes = CType(oAttributeControl.FindControl("dlCustomAttributes"), DataList)
            For Each dlItem In DlCustomAttributes.Items
                Dim oAttStorage As New CAttributesSelected
                IsRequired = CType(dlItem.FindControl("CustomRequired"), TextBox).Text
                If IsRequired = True Then
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) = "" Then
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        'raise required error
                        ErrorMessage.Text = sAttName & " required"
                        ErrorMessage.Visible = True
                        Return Nothing
                    Else
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                Else
                    'Not required
                    If Trim(CType(dlItem.FindControl("txtCustom"), TextBox).Text.ToString) <> "" Then
                        sTemp = CType(dlItem.FindControl("CustomDetailID"), TextBox).Text
                        oattStorage.UID = CLng(sTemp)
                        sTemp = CType(dlItem.FindControl("CustomAttributeID"), TextBox).Text()
                        oattStorage.AttributeId = CLng(sTemp)
                        oattStorage.Customor_Custom_Description = CType(dlItem.FindControl("txtCustom"), TextBox).Text()
                        m_OrderAttributes.Add(oattStorage)
                    End If
                End If
            Next
            Return m_OrderAttributes
        End If
    End Function
#End Region

#Region "Private Function IsItemStocked(ByVal mealProdDetail As CCustomizeBundleProductDetailBase) As Boolean"
    Private Function IsItemStocked(ByVal mealProdDetail As CCustomizeBundleProductDetailBase) As Boolean
        Dim inventory As Inventory_Management
        inventory = New Inventory_Management(mealProdDetail.ProductID)
        If inventory.Inventory.InventoryTracked Then
            If inventory.Inventory.ItemsAreStocked(mealProdDetail.Product.Attributes, CLng(Request.QueryString("Qty")) * mealProdDetail.Quantity) Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function
#End Region

#Region "Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR"
    Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR
        Try
            ErrorMessage.Visible = True
            ErrorMessage.Text = sender.ToString
        Catch err As System.Exception
            ErrorMessage.Visible = False
        End Try
    End Sub
#End Region
    'end Tee

    ' begin: JDB - Customer Bundle Rules
    Private Class JavascriptDataItem
        Public Product As CProduct
        Public Checkbox As Checkbox
        Public SelectableQuantity As Integer
        Public ProductDetailCount As Integer
    End Class
    ' end: JDB - Customer Bundle Rules

    Public Sub WriteProductName()
        If Not m_oProduct Is Nothing Then
            Response.Write(m_oProduct.ProductCode + " - ")
            Response.Write(m_oProduct.Name + " - ")
            Response.Write(m_oProduct.CategoryNames.Replace("<br>", " - "))
            Response.Write(" - ")
            MyBase.WriteTitle()
        Else
            MyBase.WriteTitle()
        End If
    End Sub
End Class
