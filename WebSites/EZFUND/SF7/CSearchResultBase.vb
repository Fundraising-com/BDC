'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class CSearchResultBase
    Inherits CWebControl

#Region "Class Events"
    Event AddToCart As EventHandler
    Event AddToSavedCart As EventHandler
    Event AttributeRequiredError As EventHandler
    Event EMailFriend As EventHandler
    Event VolumePricing As EventHandler
    Event EmptyResult As EventHandler
    Event AddError As EventHandler
#End Region

#Region "Class Members"
    Protected m_strPrevCategory As String
    Protected m_objSearchEngine As CSearchEngine
    Protected m_nDefaultQty As Long = 1
    Protected m_nProductsPerRow As Long = 4
    Protected m_nRows As Long = 2
    Protected m_nAlignment As AlignmentType = AlignmentType.Center

    Protected m_bDisplayQty As Boolean = True
    Protected m_bDisplayImage As Boolean = True
    Protected m_bDisplayProductCode As Boolean = True
    Protected m_bDisplayProductName As Boolean = True
    Protected m_bDisplayPriceSalePrice As Boolean = False
    Protected m_bDisplayShortDescription As Boolean = False
    Protected m_bDisplayVendor As Boolean = False
    Protected m_bDisplayManufacturer As Boolean = False
    Protected m_bDisplayVolumePricing As Boolean = False
    Protected m_bDisplayStockInfo As Boolean = False
    Protected m_bDisplayAddToCart As Boolean = True
    Protected m_bDisplaySavedCartWishList As Boolean = True
    Protected m_bDisplayEMailFriend As Boolean = True
    Protected m_bDisplayMoreInfo As Boolean = False
    Protected m_bDisplayLabels As Boolean = False
    Protected m_OrderAttributes As ArrayList
    Protected m_bLinkImage As Boolean = True
    Protected m_bLinkProductCode As Boolean = True
    Protected m_bLinkProductName As Boolean = True
    Protected m_bDisplayVendorTemp As Boolean = True
    Protected m_bDisplayManufacturerTemp As Boolean = True
    Protected m_bDisplayVolumePriceGrid As Integer = -1
    Protected m_bDisplayVolumePricingTemp As Boolean
    Protected m_AttributeDisplay As Integer = 0
    Protected m_Stock_Depleted As Boolean = False
    Protected m_lngCurrentID As Long = -1
    Protected InvenStatVisible As Boolean = False
    Protected m_InventoryLinkVisible As Boolean = False
    Protected m_CatsAreGrouped As Boolean = False
    Protected lngCatId As Long
    Protected QsObjExist As Boolean = False
    Protected objStorage As CSearchStorage
    Protected arResults As ArrayList
    '2820
    Private mPageIndex As Integer
#End Region

    'Tee 8/24/2007 product configurator
#Region "Properties"
    Public ReadOnly Property BundleInfo(ByVal prod As CProducts) As String
        Get
            If prod.Count > 0 Then
                Dim info As String = ""
                For Each item As CProduct In prod
                    info = info & "<font style='padding-left:6px;'>" & item.ProductCode & " - " & item.Name & "</font><BR>"
                Next
                Return info
            End If
            Return ""
        End Get
    End Property
#End Region
    'end Tee


#Region "Protected Sub LoadSettings()"
    Protected Sub LoadSettings()
        Dim objNode As XmlNode
        objNode = dom.Item("SiteProducts").Item("SearchResult")

        m_bDisplayQty = IIf(objNode.Attributes("DisplayQty").Value = "1", True, False)
        m_bDisplayImage = IIf(objNode.Attributes("DisplayImage").Value = "1", True, False)
        m_bDisplayProductCode = IIf(objNode.Attributes("DisplayProductCode").Value = "1", True, False)
        m_bDisplayProductName = IIf(objNode.Attributes("DisplayProductName").Value = "1", True, False)
        m_bDisplayPriceSalePrice = IIf(objNode.Attributes("DisplayPriceSalePrice").Value = "1", True, False)
        m_bDisplayShortDescription = IIf(objNode.Attributes("DisplayShortDescription").Value = "1", True, False)
        m_bDisplayVendor = IIf(objNode.Attributes("DisplayVendor").Value = "1", True, False)
        m_bDisplayManufacturer = IIf(objNode.Attributes("DisplayManufacturer").Value = "1", True, False)
        m_bDisplayVolumePricing = IIf(objNode.Attributes("DisplayVolumePricing").Value = "1", True, False)
        m_bDisplayStockInfo = IIf(objNode.Attributes("DisplayStockInfo").Value = "1", True, False)
        m_bDisplayAddToCart = IIf(objNode.Attributes("DisplayAddToCart").Value = "1", True, False)
        m_bDisplaySavedCartWishList = IIf(objNode.Attributes("DisplaySavedCartWishList").Value = "1", True, False)
        m_bDisplayEMailFriend = IIf(objNode.Attributes("DisplayEMailFriend").Value = "1", True, False)
        m_bDisplayMoreInfo = IIf(objNode.Attributes("DisplayMoreInfo").Value = "1", True, False)
        m_bDisplayLabels = IIf(objNode.Attributes("DisplayLabels").Value = "1", True, False)
        m_bLinkImage = IIf(objNode.Attributes("LinkImage").Value = "1", True, False)
        m_bLinkProductCode = IIf(objNode.Attributes("LinkProductCode").Value = "1", True, False)
        m_bLinkProductName = IIf(objNode.Attributes("LinkProductName").Value = "1", True, False)
        m_nProductsPerRow = CLng(objNode.Attributes("ProductsPerRow").Value)
        m_AttributeDisplay = CInt("0" & objNode.Attributes("AttributeDisplay").Value)
        m_nRows = CLng(objNode.Attributes("Rows").Value)
        m_nAlignment = CLng(objNode.Attributes("Alignment").Value)
        m_nDefaultQty = CLng(objNode.Attributes("DefaultQty").Value)
    End Sub
#End Region

#Region "Protected Sub NormalSearch()"

    Protected Function NormalSearch(ByVal obj As CSearchStorage, ByVal oSearchFilters As Hashtable) As Hashtable
        'Category
        objStorage = obj
        m_strPrevCategory = ""
        If objStorage.CategoryID <> -1 And QsObjExist = False Then
            m_CatsAreGrouped = True
        End If
        m_objSearchEngine = New CSearchEngine(Me.m_objCustomer.CustomerGroup, m_nRows)
        arResults = m_objSearchEngine.StandardSearch(objStorage, oSearchFilters, m_CatsAreGrouped, Me.PageIndex)
        If arResults.Count = 0 Then EmptyResults(StoreFrontConfiguration.MessagesAccess().GetXMLMessage("SearchResult.aspx", "Suggestion", "NoResult"))


        'If (IsNothing(Me.FindControl("DataGrid1")) = False) And arResults.Count > 0 Then
        If (IsNothing(Me.FindControl("DataGrid1")) = False) Then
            Dim dg1 As DataGrid = Me.FindControl("DataGrid1")
            dg1.AllowCustomPaging = True
            dg1.VirtualItemCount = m_objSearchEngine.ResultsCount
            dg1.CurrentPageIndex = Me.PageIndex
            dg1.DataSource = arResults
            dg1.DataBind()
            'put the category name too here
        End If
        Dim lb1 As Label = Me.FindControl("lblHeaderRowCategoryname")

        If arResults.Count > 0 Then
            lb1.Text = CType(arResults.Item(0), CCategoryStorage).CategoryName
        Else
            lb1.Visible = False
        End If
        Return m_objSearchEngine.SearchFilterValues
    End Function

#End Region

    Public Sub EmptyResults(ByVal strMessage As String)
        RaiseEvent EmptyResult(strMessage, EventArgs.Empty)
    End Sub

#Region "Protected Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal objDataList As DataListItem = Nothing, Optional ByVal objDataGrid As DataGrid = Nothing)"
    Protected Sub AddCartClick(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal objDataList As DataListItem = Nothing, Optional ByVal objDataGrid As DataGrid = Nothing)
        Dim objButton As LinkButton
        Dim objDataGridItem As DataGridItem = Nothing
        Dim objParent As Object
        
        objButton = CType(sender, LinkButton)
        objParent = objButton.Parent

        If (IsNothing(objDataList) = False) Then

            Set_Item_Attributes(objDataGridItem, objDataList)
            objButton.CommandArgument = m_nDefaultQty
        ElseIf (IsNothing(objDataGrid) = False) Then
            objDataGridItem = objDataGrid.Items(0)
            While (Not objParent.GetType() Is objDataGridItem.GetType)
                objParent = objParent.Parent
            End While
            objDataGridItem = CType(objParent, DataGridItem)
            If (IsNothing(objDataGridItem.FindControl("txtQuantity")) = False) Then
                objButton.CommandArgument = CType(objDataGridItem.FindControl("txtQuantity"), TextBox).Text

            Else
                objButton.CommandArgument = m_nDefaultQty
            End If
            Set_Item_Attributes(objDataGridItem)
        End If

        If (objButton.ID = "btnAddToCart") Then
            RaiseEvent AddToCart(objButton, e)
        ElseIf (objButton.ID = "btnAddToSavedCart") Then
            RaiseEvent AddToSavedCart(objButton, e)
        ElseIf (objButton.ID = "btnEMailFriend") Then
            RaiseEvent EMailFriend(objButton, e)
        End If

    End Sub
#End Region

#Region "Public Sub AddErrorMessage(ByVal strMessage As String)"
    Public Sub AddErrorMessage(ByVal strMessage As String)
        RaiseEvent AddError(strMessage, EventArgs.Empty)
    End Sub
#End Region

#Region "Private Sub Set_Item_Attributes(ByVal objGridItem As DataGridItem,optional dlItem as )"


    Protected Sub Set_Item_Attributes(ByVal objGridItem As DataGridItem, Optional ByVal objDatalist As DataListItem = Nothing)

        Dim dlItem As DataListItem
        'Dim dlItem2 As DataListItem
        'Dim oCont As Control
        Dim sTemp As String
        Dim IsRequired As Boolean
        Dim oAttributeControl As CAttributeControl
        Dim DlAttributes As DataList
        Dim DlCustomAttributes As DataList
        'Dim oAttDetail As CAttributeDetail
        Session.Remove("OrderAttributes")

        If objDatalist Is Nothing Then
            oAttributeControl = CType(objGridItem.FindControl("CAttributeControl1"), CAttributeControl)
        Else
            oAttributeControl = CType(objDatalist.FindControl("CAttributeControl1"), CAttributeControl)
        End If

        If oAttributeControl Is Nothing Then
            Exit Sub 'exit 
        Else
            m_OrderAttributes = New ArrayList
            'set attributes 
            DlAttributes = CType(oAttributeControl.FindControl("DlAttributes"), DataList)
            Dim objAttributes As ListControl
            Dim sAttName As String
            For Each dlItem In DlAttributes.Items


                'late bind based on dropdown or radiolist
                If oAttributeControl.DisplayType = CAttributeControl.t_DisplayType.DropDown Then
                    objAttributes = CType(dlItem.FindControl("AttributeName"), DropDownList)
                Else
                    objAttributes = CType(dlItem.FindControl("AttributeName2"), RadioButtonList)
                End If

                sAttName = CType(dlItem.FindControl("ErrorName"), TextBox).Text

                If objAttributes.SelectedItem Is Nothing Then
                    ' required raise error

                    RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                    Exit Sub
                    ' begin: JDB - Dynamic Image Suite
                    ' note: added to determine of the attribute has been set as unavailable or out-of-stock
                    '   and not backorderable
                ElseIf objAttributes.Visible AndAlso Request.Form(objAttributes.UniqueID) = "" Then
                    RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                    Exit Sub
                    ' end: JDB - Dynamic Image Suite
                ElseIf CLng(objAttributes.SelectedItem.Value()) = -1 Then
                    ' required raise error

                    RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                    Exit Sub
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
                        ' required raise error
                        sAttName = (CType(dlItem.FindControl("attName"), TextBox).Text)   'attName
                        RaiseEvent AttributeRequiredError(sAttName, EventArgs.Empty)
                        Exit Sub
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

            Session("OrderAttributes") = m_OrderAttributes

        End If
    End Sub
#End Region

    Public Function HaQsParams() As Boolean
        'If Request.QueryString("Manufacturer") <> "" Or Request.QueryString("Vendor") <> "" Or Request.QueryString("KeyWords") <> "" Then
        '    Return True
        'Else
        '    Return False

        'End If


        Return Not (Request.QueryString("Manufacturer") Is Nothing And Request.QueryString("Vendor") Is Nothing And Request.QueryString("KeyWords") Is Nothing)
        '    Return False
        'Else
        '    Return True

        'End If



    End Function
    Public Function SetSearch() As CSearchStorage

        If HaQsParams() Or Request.QueryString("CategoryID") <> "" Then
            lngCatId = Request.QueryString("CategoryID")
            objStorage = New CSearchStorage
            objStorage.Keyword = ""
            objStorage.IsAdvanced = False
            If IsNumeric(lngCatId) And lngCatId <> 0 Then
                If (IsNothing(Me.FindControl("ResultInfo")) = False) Then
                    Me.FindControl("ResultInfo").Visible = False
                End If
                objStorage.CategoryID = lngCatId
            End If
            'Manufacturer
            If Request.QueryString("Manufacturer") <> "" Then
                objStorage.IsAdvanced = True
                objStorage.ManufacturerID = Request.QueryString("Manufacturer")
            End If
            'Vendor
            If Request.QueryString("Vendor") <> "" Then
                objStorage.IsAdvanced = True
                objStorage.VendorID = Request.QueryString("Vendor")
            End If
            'KeyWords
            If Request.QueryString("KeyWords") <> "" Then
                If (TestInput(Request.QueryString("KeyWords")) <> "") Then
                    objStorage.Keyword = TestInput(Request.QueryString("KeyWords"))
                Else
                    objStorage.Keyword = Request.QueryString("KeyWords")
                End If
                If Request.QueryString("All") = "True" Then
                    objStorage.SearchType = SearchType.AllWords
                ElseIf Request.QueryString("Exact") = "True" Then
                    objStorage.SearchType = SearchType.ExactPhrase
                Else
                    objStorage.SearchType = SearchType.AnyWords
                End If
            End If
            Return objStorage
        End If

        If Not Session("Search") Is Nothing Then
            Return Session("Search")
        End If
        Return New CSearchStorage
    End Function

    Private Function TestInput(ByVal strValue As String) As String
        Dim bReSubmit As Boolean = False
        Dim nIndex As Integer = 0

        While (True)
            nIndex = strValue.IndexOf("'", nIndex)

            If (nIndex > -1) Then
                If (nIndex = strValue.Length - 1) Then
                    bReSubmit = True
                    Exit While
                End If

                If (strValue.Chars(nIndex + 1) <> "'") Then
                    bReSubmit = True
                    Exit While
                Else
                    nIndex = nIndex + 2
                End If
            Else
                Exit While
            End If
        End While

        If (bReSubmit) Then
            Return strValue.Replace("'", "")
        Else
            Return ""
        End If
    End Function

    'SP8
    Public Property PageIndex() As Integer
        Get
            Return mPageIndex
        End Get
        Set(ByVal Value As Integer)
            mPageIndex = Value
        End Set
    End Property

End Class
