Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Partial  Class AdvancedSearch
    Inherits CWebControl
    Protected WithEvents PriceBetween As System.Web.UI.WebControls.Panel

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
        If Me.Visible = False Then Exit Sub
        LoadOptions()
        If (IsPostBack = False) Then
            Dim arList As New ArrayList()
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                Dim objCategoryAccess As CXMLCategoryAccess = StoreFrontConfiguration.CategoryAccess
                arList = objCategoryAccess.GetLevelWChildren(0)
                Dim objCategory As New CXMLCategoryList()
                Dim strCatLabel As String
                strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
                objCategory.ID = -1
                objCategory.Name = "All " & strCatLabel
                arList.Insert(0, objCategory)
            Else
                Dim objCategoryAccess As New CCategories()
                arList = objCategoryAccess.ParantCategories
                Dim objCategory As New CCategory()
                Dim strCatLabel As String
                strCatLabel = StoreFrontConfiguration.Labels.Item("lblCategorys").InnerText()
                objCategory.ID = -1
                objCategory.Name = "All " & strCatLabel
                arList.Insert(0, objCategory)
            End If

            AdvCategory.DataSource = arList
            AdvCategory.DataBind()
            PopulateDD(1, AdvVendor)
            PopulateDD(2, AdvManufacture)
        End If
        imgAdvSearch.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value

        LabelText(trCategory)

    End Sub

    Private Sub PopulateDD(ByVal iType As Integer, ByVal objDD As DropDownList)
        'iType 1 =vendor ;2 = manufactuerer
        Dim obDD As New CGenericDDContainer()
        Dim _oNode As XmlNode = Nothing
        Dim oSelNode As XmlNode
        Dim i As Integer
        Dim ar As New ArrayList()
        Dim strVendLabel As String
        Dim strManLabel As String
        Dim isLive As Boolean = False
        Dim dt As DataTable = Nothing
        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.Live Then
            isLive = True
        End If

        strVendLabel = StoreFrontConfiguration.Labels.Item("lblVendors").InnerText()
        strManLabel = StoreFrontConfiguration.Labels.Item("lblManufacturers").InnerText()
        Dim sLabel As String = ""
        Select Case iType
            Case 1
                sLabel = "All " & strVendLabel
                If isLive Then
                    Dim objVendor As New CVendor()
                    dt = objVendor.ddVendors
                Else

                    _oNode = dom.Item("SiteProducts").Item("Vendors")
                    obDD.ID = "-1"
                    obDD.Name = sLabel
                    ar.Add(obDD)
                End If



            Case 2
                sLabel = "All " & strManLabel
                If isLive Then
                    Dim objMan As New CManufacturer()
                    dt = objMan.ddManufacturers
                Else

                    _oNode = dom.Item("SiteProducts").Item("Manufacturers")
                    obDD.ID = "-1"
                    obDD.Name = sLabel
                    ar.Add(obDD)
                End If

        End Select



        If isLive Then
            objDD.DataValueField = "ID"
            objDD.DataTextField = "Name"
            objDD.DataSource = dt
            objDD.DataBind()
            objDD.Items.Insert(0, sLabel)
        Else
            For i = 0 To _oNode.ChildNodes.Count - 1
                oSelNode = _oNode.ChildNodes.Item(i)
                obDD = New CGenericDDContainer()
                If iType = 1 Then
                    obDD.ID = oSelNode.Item("VendorID").InnerText.ToString
                Else
                    obDD.ID = oSelNode.Item("ManufacturerID").InnerText.ToString
                End If

                obDD.Name = oSelNode.Item("Name").InnerText.ToString

                ar.Add(obDD)
            Next
            objDD.DataValueField = "ID"
            objDD.DataTextField = "Name"
            objDD.DataSource = ar
            objDD.DataBind()

        End If


       

    End Sub

    Private Sub btnAdvSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvSearch.Click
        Dim objStorage As New CSearchStorage()
        'viewstate("PageIndex") = 0
        ''2715 - X
        'Session("PageIndex") = 0

        objStorage.Keyword = AdvKeyword.Text
        objStorage.IsAdvanced = True

        If (SimpleKeywordGroup1.Checked = True) Then

            objStorage.SearchType = SearchType.AnyWords

        ElseIf (SimpleKeywordGroup2.Checked = True) Then

            objStorage.SearchType = SearchType.ExactPhrase

        ElseIf (SimpleKeywordGroup3.Checked = True) Then

            objStorage.SearchType = SearchType.AllWords

        End If

        objStorage.Added_Inventory_ON = dlInventory.SelectedItem.Value
        objStorage.Lower_Price = AdvPriceStart.Text
        objStorage.Upper_Price = AdvPriceEnd.Text

        If IsNumeric(AdvManufacture.SelectedItem.Value) Then
            objStorage.ManufacturerID = AdvManufacture.SelectedItem.Value
        Else
            objStorage.ManufacturerID = 0
        End If
        If IsNumeric(AdvVendor.SelectedItem.Value) Then
            objStorage.VendorID = AdvVendor.SelectedItem.Value
        Else
            objStorage.VendorID = 0
        End If
        If IsNumeric(AdvCategory.SelectedItem.Value) Then
            objStorage.CategoryID = AdvCategory.SelectedItem.Value
        Else
            objStorage.CategoryID = 0
        End If


        objStorage.OnlySale = AdvSaleOnly.Checked
        Session("Search") = objStorage
        Response.Redirect("SearchResult.aspx")
    End Sub

    Private Sub LoadOptions()
        Dim _oNode As XmlNode
        _oNode = dom.Item("SiteProducts").Item("SearchOptions")
        trKeyWord.Visible = CInt("0" & _oNode.Item("Keyword").InnerText)
        trKeyWord2.Visible = CInt("0" & _oNode.Item("Keyword").InnerText)
        Me.trCategory.Visible = CInt("0" & _oNode.Item("Category").InnerText)
        Me.trManufacturer.Visible = CInt("0" & _oNode.Item("Manufacturer").InnerText)
        Me.trVendor.Visible = CInt("0" & _oNode.Item("Vendor").InnerText)
        Me.trAddedBetween.Visible = CInt("0" & _oNode.Item("AddedOn").InnerText)
        Me.trPriceBetween.Visible = CInt("0" & _oNode.Item("PriceBetween").InnerText)
        Me.pnlSaleOnly.Visible = CInt("0" & _oNode.Item("SaleOnly").InnerText)




    End Sub
End Class
