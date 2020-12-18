Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management

Partial Class CustomerDefinedRulesControl
    Inherits CWebControl

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

    Private lblProdName As Label
    Private m_dtBundleRules As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Visible = False OrElse IsNothing(Session("ProductID")) Then
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim prodId As Long = Session("ProductID")
            hdnID.Value = prodId

            Dim objProdManagement As New CProductManagement(prodId)
            MakeCommonVisible(objProdManagement.ProductType, False)
            lblProdName = CType(Me.Parent.FindControl("lblPDName"), Label)
            lblProdName.Text = objProdManagement.Name

            Dim obj As New CCustomizeBundleTemplate
            Dim oBundleDetails As CCustomizeBundleTemplateBase = obj.GetCustomizeBundleTemplate(prodId)
            Dim bDisplayPage As Boolean = True
            Try
                Dim iTotalCombinations As Integer = 1
                For Each oStepDetails As CCustomizeBundleTemplateDetailBase In oBundleDetails.StepDetails
                    iTotalCombinations *= Me.Factorial(oStepDetails.ProductDetails.Count) / (Me.Factorial(oStepDetails.ProductDetails.Count - oStepDetails.SelectableQuantity) * Me.Factorial(oStepDetails.SelectableQuantity))
                Next
                If iTotalCombinations > 3000 Then
                    bDisplayPage = False
                End If
            Catch ex As Exception
                bDisplayPage = False
            End Try

            If bDisplayPage Then
                Me.m_dtBundleRules = obj.GetBundleRules(prodId)
                Dim aoStepCombinations(oBundleDetails.StepDetails.Count - 1) As StepCombinationDetail
                For iStepIndex As Integer = 0 To oBundleDetails.StepDetails.Count - 1
                    Dim oStepDetails As CCustomizeBundleTemplateDetailBase = oBundleDetails.StepDetails(iStepIndex)

                    Dim oStepCombinationDetail As New StepCombinationDetail
                    oStepCombinationDetail.StepName = oStepDetails.StepName

                    If oStepDetails.SelectableQuantity < oStepDetails.ProductDetails.Count Then
                        Me.SetStepCombinations(oStepDetails)
                        oStepCombinationDetail.ProductIDs = Me.m_oStepProductIDs
                        oStepCombinationDetail.ProductNames = Me.m_oStepProductNames
                    Else
                        Dim sProductIDs As String = ""
                        Dim sProductNames As String = ""
                        For Each oProductDetail As CCustomizeBundleProductDetailBase In oStepDetails.ProductDetails
                            If sProductIDs.Length > 0 Then
                                sProductIDs += ","
                            End If
                            If sProductNames.Length > 0 Then
                                sProductNames += ", "
                            End If
                            sProductIDs += oProductDetail.ProductID.ToString
                            sProductNames += oProductDetail.Quantity.ToString + " "
                            If oProductDetail.Quantity > 1 Then
                                sProductNames += oProductDetail.Product.PluralName
                            Else
                                sProductNames += oProductDetail.Product.Name
                            End If
                        Next
                        oStepCombinationDetail.ProductIDs.Add(sProductIDs)
                        oStepCombinationDetail.ProductNames.Add(sProductNames)
                    End If

                    aoStepCombinations(iStepIndex) = oStepCombinationDetail
                Next
                Me.SetCombinations(aoStepCombinations)
                Me.rCombinations.DataSource = Me.m_oProductNames
                Me.rCombinations.DataBind()
            Else
                Me.lblMessage.Text = "The number of combinations for this bundle exceeds the number allowable for this page."
            End If
        End If
    End Sub

    Private m_oStepProductIDs As ArrayList
    Private m_oStepProductNames As ArrayList
    Private Sub SetStepCombinations(ByVal oStepDetails As CCustomizeBundleTemplateDetailBase)
        Me.m_oStepProductIDs = New ArrayList
        Me.m_oStepProductNames = New ArrayList
        Dim iSelectableQuantity As Integer = oStepDetails.SelectableQuantity
        Dim total As Integer = Factorial(oStepDetails.ProductDetails.Count) / (Factorial(oStepDetails.ProductDetails.Count - iSelectableQuantity) * Factorial(iSelectableQuantity))

        Dim counter(iSelectableQuantity) As Integer
        For a As Integer = 1 To iSelectableQuantity
            counter(a) = a
        Next

        Dim current As Integer = iSelectableQuantity
        Dim count As Integer = -1
        Do
            count += 1
            Dim sProductIDs As String = ""
            Dim sProductNames As String = ""
            For a As Integer = 1 To iSelectableQuantity
                If sProductIDs.Length > 0 Then
                    sProductIDs += ","
                End If
                If sProductNames.Length > 0 Then
                    sProductNames += ", "
                End If
                Dim oProduct As CCustomizeBundleProductDetailBase = oStepDetails.ProductDetails(counter(a) - 1)
                sProductIDs += oProduct.ProductID.ToString
                sProductNames += oProduct.Quantity.ToString + " "
                If oProduct.Quantity > 1 Then
                    sProductNames += oProduct.Product.PluralName
                Else
                    sProductNames += oProduct.Product.Name
                End If
            Next
            Me.m_oStepProductIDs.Add(sProductIDs)
            Me.m_oStepProductNames.Add(sProductNames)
            If counter(current) = oStepDetails.ProductDetails.Count Then
                Do
                    current -= 1
                Loop Until counter(current) < (oStepDetails.ProductDetails.Count - (iSelectableQuantity - current))
                counter(current) += 1
                For a As Integer = current + 1 To iSelectableQuantity
                    counter(a) = counter(a - 1) + 1
                Next
                current = iSelectableQuantity
            Else
                counter(current) += 1
            End If
        Loop Until count = total - 1
    End Sub

    Public Function Factorial(ByVal i As Integer) As Integer
        Dim iFactorial As Integer = 1
        For iIndex As Integer = i To 1 Step -1
            iFactorial *= iIndex
        Next
        Return iFactorial
    End Function

    Private m_oProductIDs As New ArrayList
    Private m_oProductNames As New ArrayList
    Private Sub SetCombinations(ByVal aoStepCombinations() As StepCombinationDetail)
        Dim aiRepeatFactor(aoStepCombinations.Length - 1) As Integer
        Dim nTotalCombinationCount As Integer = 1
        Dim aoCombinations() As String


        For iAttributeIndex As Integer = 0 To aoStepCombinations.Length - 1
            nTotalCombinationCount = nTotalCombinationCount * aoStepCombinations(iAttributeIndex).ProductIDs.Count
        Next
        Dim nMultiple As Integer
        For iIndex As Integer = aoStepCombinations.Length - 1 To 0 Step -1
            If iIndex = aoStepCombinations.Length - 1 Then
                nMultiple = aoStepCombinations(iIndex).ProductIDs.Count
                aiRepeatFactor(iIndex) = CInt(nMultiple / aoStepCombinations(iIndex).ProductIDs.Count)
            Else
                nMultiple = nMultiple * aoStepCombinations(iIndex).ProductIDs.Count
                aiRepeatFactor(iIndex) = CInt(nMultiple / aoStepCombinations(iIndex).ProductIDs.Count)
            End If
        Next

        ReDim aoCombinations(nTotalCombinationCount - 1)
        For iCombinationIndex As Integer = 0 To nTotalCombinationCount - 1
            Me.SetCombination(iCombinationIndex, aoStepCombinations, aiRepeatFactor)
            Me.m_oProductIDs.Add(Me.m_sProductIDs)
            Me.m_oProductNames.Add(Me.m_sProductNames)
        Next
    End Sub

    Private m_sProductIDs As String
    Private m_sProductNames As String
    Private Sub SetCombination(ByVal n As Integer, ByVal aoStepCombinations() As StepCombinationDetail, ByVal aiRepeatFactor() As Integer)
        Dim sProductIDs As String = ""
        Dim sProductNames As String = ""
        For iIndex As Integer = 0 To aoStepCombinations.Length - 1
            Dim iRepeatIndex As Integer = CInt(Int(n / aiRepeatFactor(iIndex))) Mod aoStepCombinations(iIndex).ProductIDs.Count
            If sProductIDs.Length > 0 Then
                sProductIDs += ","
            End If
            If sProductNames.Length > 0 Then
                sProductNames += " "
            End If
            sProductIDs += aoStepCombinations(iIndex).ProductIDs(iRepeatIndex)
            sProductNames += "<b>" + aoStepCombinations(iIndex).StepName + "</b>: " + aoStepCombinations(iIndex).ProductNames(iRepeatIndex)
        Next
        Me.m_sProductIDs = sProductIDs
        Me.m_sProductNames = sProductNames
    End Sub

    Private Class StepCombinationDetail
        Public StepName As String = ""
        Public ProductIDs As New ArrayList
        Public ProductNames As New ArrayList
    End Class

    Private Sub rCombinations_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rCombinations.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim sRule As String = Me.m_oProductIDs(e.Item.ItemIndex)

            Dim oHidden As System.Web.UI.HtmlControls.HtmlInputHidden = e.Item.FindControl("hdnProductIDs")
            oHidden.Value = sRule

            Dim bFound As Boolean = False
            For Each oRow As DataRow In Me.m_dtBundleRules.Rows
                If sRule = oRow("BundleRuleDetail") Then
                    bFound = True
                    Exit For
                End If
            Next
            Dim oCheckbox As CheckBox = e.Item.FindControl("chkValid")
            oCheckbox.Checked = (Not bFound)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim prodID As Long = CLng(hdnID.Value)
        Dim obj As New CCustomizeBundleTemplate

        obj.ResetBundleRules(prodID)
        For Each oItem As RepeaterItem In Me.rCombinations.Items
            Dim oCheckbox As CheckBox = oItem.FindControl("chkValid")
            Dim oHidden As System.Web.UI.HtmlControls.HtmlInputHidden = oItem.FindControl("hdnProductIDs")

            If Not oCheckbox.Checked Then
                obj.SaveBundleRule(prodID, oHidden.Value)
            End If
        Next
    End Sub
End Class
