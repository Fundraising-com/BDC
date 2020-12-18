Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management '1521
Imports StoreFront.SystemBase
Imports System.Xml

#Region "Public MustInherit Class CAttributeControl"


Partial  Class CAttributeControl
    Inherits CWebControl

#Region " Members"


    Event NewProductImage As EventHandler
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents AttributeName As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddImageHolder As System.Web.UI.WebControls.DropDownList
    Protected WithEvents AttributeName2 As System.Web.UI.WebControls.RadioButtonList
    'AttributeName
    Enum t_DisplayType
        DropDown = 0
        Radio = 1
    End Enum
    Private m_arOrderAttributes As ArrayList
    Private m_DisplayType As t_DisplayType = t_DisplayType.DropDown
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
        ' end: JDB - Dynamic Image Suite
        'begin: GJV - 6/12/2007 - Attribute Detail Hotfix
        'note: this is now handled by the SetAttributeControlDisplay method

        'Dim oItem As DataListItem
        'Dim Rdo As RadioButtonList
        'Dim ddl As DropDownList

        'If DisplayType = t_DisplayType.DropDown Then
        '    For Each oItem In DlAttributes.Items
        '        Rdo = CType(oItem.FindControl("AttributeName2"), RadioButtonList)
        '        ddl = CType(oItem.FindControl("AttributeName"), DropDownList)
        '        Rdo.Visible = False
        '        ddl.Visible = True
        '    Next
        'Else
        '    For Each oItem In DlAttributes.Items
        '        Rdo = CType(oItem.FindControl("AttributeName2"), RadioButtonList)
        '        ddl = CType(oItem.FindControl("AttributeName"), DropDownList)
        '        Rdo.Visible = True
        '        ddl.Visible = False
        '        CType(oItem.FindControl("attName1"), Label).Visible = True
        '    Next
        'End If

        'end: GJV - 6/12/2007 - Attribute Detail Hotfix
    End Sub
#End Region

    ' begin: JDB - Dynamic Image Suite
    Protected Sub RegisterJavascriptArray()
        ' note: session should not be used here but viewstate is not working and due to the messed up event
        '   execution order in the search functionality, there is no other option
        Dim sScript As String
        If Me.bRequeryJavascriptArray Then
            ' note: begin option 2
            For iAttributeIndex As Integer = 0 To Me.aoAttributes.Length - 1
                Me.AddToJavascriptArray(Me.aoAttributes(iAttributeIndex), Me.aoListControls(iAttributeIndex))
            Next
            ' note: end option 2
            sScript = Me.GetAttributeValidationJavascript()
            Session(Me.ClientID + "AttributeValidationJavascript") = sScript
        Else
            sScript = Session(Me.ClientID + "AttributeValidationJavascript")
        End If
        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.Parent.GetType, Me.ClientID, sScript)
        'Me.Parent.Page.RegisterStartupScript(Me.ClientID, ViewState("AttributeValidationJavascript"))
    End Sub

    ' note: begin option 2
    Private aoAttributes() As CAttribute = {}
    Private aoListControls() As ListControl = {}
    ' note: end option 2
    Private Sub AddToJavascriptArray(ByRef oAttribute As CAttribute, ByRef oListControl As ListControl)
        'new Attribute(
        '		"Color",
        '		document.getElementById("SearchTemplate12_DataGrid1__ctl4_CAttributeControl1_DlAttributes__ctl0_AttributeName"),
        '		new Array(
        '			new Option("Red", "1"),
        '			new Option("Blue", "2"),
        '			new Option("Orange", "3"),
        '			new Option("Black", "4")
        '			)
        '		)

        ' note: begin option 1
        ' note: not using option 1 because the multiple iterations of event execution are no longer
        '   occurring on the seach but are still occurring on the product detail
        'Dim sOptionArray As String = ""
        'Dim sRadioControls As String = ""
        'Dim iOptionIndex As Integer = 0
        'For Each oAttributeDetail As CAttributeDetail In oAttribute.AttributeDetails
        '    If sOptionArray.Length > 0 Then
        '        sOptionArray += ","
        '        sRadioControls += ","
        '    End If
        '    sOptionArray += String.Format("new Option(""{0}"", ""{1}"")", oAttributeDetail.Name, oAttributeDetail.UID)
        '    sRadioControls += String.Format("document.getElementById(""{{" + Me.iAttributeCount.ToString + "}}_{0}"")", iOptionIndex)
        '    iOptionIndex += 1
        'Next
        'Dim asArguments(4) As String
        'asArguments(0) = oAttribute.Name
        'If Me.DisplayType = t_DisplayType.DropDown Then
        '    asArguments(1) = "new Array(document.getElementById(""{" + Me.iAttributeCount.ToString + "}""))"
        'Else
        '    asArguments(1) = "new Array(" + sRadioControls + ")"
        'End If
        'asArguments(2) = Me.DisplayType
        'asArguments(3) = sOptionArray
        'Me.sJavascriptArray += String.Format("new Attribute(""{0}"", {1}, {2}, new Array({3})),", asArguments)
        'ReDim Preserve Me.aoAttributeControl(Me.iAttributeCount)
        'Me.aoAttributeControl(Me.iAttributeCount) = oListControl
        'Me.iAttributeCount += 1
        ' note: end option 1
        ' note: begin option 2
        Dim sOptionArray As String = ""
        Dim sOptionSuffixArray As String = ""
        Dim sRadioControls As String = ""
        Dim iOptionIndex As Integer = 0
        Dim bAddDelimiter As Boolean = False
        For Each oAttributeDetail As CAttributeDetail In oAttribute.AttributeDetails
            If bAddDelimiter Then
                sOptionArray += ","
                sOptionSuffixArray += ","
                sRadioControls += ","
            Else
                bAddDelimiter = True
            End If
            sOptionArray += String.Format("new Option(""{0}"", ""{1}"")", oAttributeDetail.Name, oAttributeDetail.UID)
            sOptionSuffixArray += String.Format("""{0}""", oAttributeDetail.Name_Price_Info_Suffix())
            sRadioControls += String.Format("document.getElementById(""" + oListControl.ClientID + "_{0}"")", iOptionIndex)
            iOptionIndex += 1
        Next
        Dim asArguments(5) As String
        asArguments(0) = oAttribute.Name
        If Me.DisplayType = t_DisplayType.DropDown Then
            asArguments(1) = "new Array(document.getElementById(""" + oListControl.ClientID + """))"
        Else
            asArguments(1) = "new Array(" + sRadioControls + ")"
        End If
        asArguments(2) = Me.DisplayType
        asArguments(3) = sOptionArray
        asArguments(4) = sOptionSuffixArray
        Me.sJavascriptArray += String.Format("new Attribute(""{0}"", {1}, {2}, new Array({3}), new Array({4})),", asArguments)
        ' note: end option 2
    End Sub
    ' end: JDB - Dynamic Image Suite

#Region "Public WriteOnly Property ProdAttributes() As ArrayList"
    Public WriteOnly Property ProdAttributes() As ArrayList
        Set(ByVal Value As ArrayList)
            Dim objAtt As CAttribute
            Dim arNormal As New ArrayList
            Dim arCustom As New ArrayList
            If Value.Count > 0 Then
                For Each objAtt In Value
                    If objAtt.AttributeType = tAttributeType.Custom Then
                        If objAtt.AttributeDetails.Count > 0 Then
                            arCustom.Add(objAtt.AttributeDetails.Item(0))
                        End If
                    Else
                        If objAtt.AttributeDetails.Count > 0 Then
                            arNormal.Add(objAtt)
                        End If
                    End If
                Next
                DlAttributes.DataSource = arNormal
                DlAttributes.DataBind()
                dlCustomAttributes.DataSource = arCustom
                dlCustomAttributes.DataBind()
                ' begin: JDB - Dynamic Image Suite
                Me.bRequeryJavascriptArray = True
                ' end: JDB - Dynamic Image Suite
            Else
                Me.Visible = False
            End If
        End Set
    End Property
#End Region

#Region "Public Sub CheckImage(ByVal sender As Object, ByVal e As System.EventArgs) Handles AttributeName.SelectedIndexChanged"
    Public Sub CheckImage(ByVal sender As Object, ByVal e As System.EventArgs) Handles AttributeName.SelectedIndexChanged
        '   Update_Image(sender)

        'Dim ddlAtt As DropDownList = sender
        'Dim oAttDetails As New CAttributeDetail(dom, ddlAtt.SelectedItem.Value)

        'If oAttDetails.SmallImage <> "" Then
        '    'raise event
        '    RaiseEvent NewProductImage(oAttDetails.SmallImage, EventArgs.Empty)
        ' End If

    End Sub
#End Region

#Region "Public Property DisplayType() As t_DisplayType"
    Public Property DisplayType() As t_DisplayType
        Get
            Return m_DisplayType
        End Get
        Set(ByVal Value As t_DisplayType)
            m_DisplayType = Value
        End Set
    End Property
#End Region

#Region "Public Sub Data_Bind(ByVal oProduct As CProduct, ByVal mCustomer As CCustomer, Optional ByVal bDataBind As Boolean = True)"
    Public Sub Data_Bind(ByVal oProduct As CProduct, ByVal mCustomer As CCustomer, Optional ByVal bDataBind As Boolean = True)
        Dim objAtt As CAttribute
        Dim arNormal As New ArrayList
        Dim arCustom As New ArrayList
        Dim objdetail As CAttributeDetail
        If oProduct.Attributes.Count > 0 Then
            '1521
            Dim oprodManagement As New CProductManagement
            Dim drProd As DataRow = oprodManagement.GetProductRow(oProduct.ProductID, mCustomer.CustomerGroup).Products.Rows(0)
            For Each objAtt In oProduct.Attributes
                If objAtt.AttributeType = tAttributeType.Custom Then
                    If objAtt.AttributeDetails.Count > 0 Then
                        '1521
                        For Each objdetail In objAtt.AttributeDetails
                            objdetail.CustomerSpecificAttributePrice = CustomerSpecificAttributePrice(objdetail.Price, drProd, mCustomer)
                        Next
                        arCustom.Add(objAtt.AttributeDetails.Item(0))
                    End If
                Else
                    If objAtt.AttributeDetails.Count > 0 Then
                        '1521
                        For Each objdetail In objAtt.AttributeDetails
                            objdetail.CustomerSpecificAttributePrice = CustomerSpecificAttributePrice(objdetail.Price, drProd, mCustomer)
                        Next
                        arNormal.Add(objAtt)
                    End If
                End If
            Next
            DlAttributes.DataSource = arNormal
            'Tee 10/8/2007 fix for bug 607, 1.1 to 2.0 transition
            If bDataBind Then
                DlAttributes.DataBind()
            End If
            'end Tee
            dlCustomAttributes.DataSource = arCustom
            dlCustomAttributes.DataBind()
            ' begin: JDB - Dynamic Image Suite
            'ViewState("AttributeValidationJavascript") = Me.GetAttributeValidationJavascript()
            Me.bRequeryJavascriptArray = True
            Me.iProductId = oProduct.ProductID
            ' end: JDB - Dynamic Image Suite
        Else
            Me.Visible = False
        End If
    End Sub
#End Region

#Region "Public Sub Data_Bind(ByVal oStorage As CCategoryStorage, ByVal mCustomer As CCustomer, Optional ByVal bDataBind As Boolean = True)"
    Public Sub Data_Bind(ByVal oStorage As CCategoryStorage, ByVal mCustomer As CCustomer, Optional ByVal bDataBind As Boolean = True)
        Dim objAtt As CAttribute
        Dim arNormal As New ArrayList
        Dim arCustom As New ArrayList
        Dim objdetail As CAttributeDetail
        If oStorage.Attributes.Count > 0 Then
            '1521
            Dim oprodManagement As New CProductManagement
            Dim drProd As DataRow = oprodManagement.GetProductRow(oStorage.ProductID, mCustomer.CustomerGroup).Products.Rows(0)

            For Each objAtt In oStorage.Attributes

                If objAtt.AttributeType = tAttributeType.Custom Then
                    If objAtt.AttributeDetails.Count > 0 Then
                        '1521
                        For Each objdetail In objAtt.AttributeDetails
                            objdetail.CustomerSpecificAttributePrice = CustomerSpecificAttributePrice(objdetail.Price, drProd, mCustomer)
                        Next
                        arCustom.Add(objAtt.AttributeDetails.Item(0))
                    End If
                Else
                    If objAtt.AttributeDetails.Count > 0 Then
                        '1521
                        For Each objdetail In objAtt.AttributeDetails
                            objdetail.CustomerSpecificAttributePrice = CustomerSpecificAttributePrice(objdetail.Price, drProd, mCustomer)
                        Next
                        arNormal.Add(objAtt)
                    End If
                End If
            Next
            DlAttributes.DataSource = arNormal
            'Tee 10/8/2007 fix for bug 607, 1.1 to 2.0 transition
            If bDataBind Then
                DlAttributes.DataBind()
            End If
            'end Tee
            dlCustomAttributes.DataSource = arCustom
            dlCustomAttributes.DataBind()
            ' begin: JDB - Dynamic Image Suite
            'ViewState("AttributeValidationJavascript") = Me.GetAttributeValidationJavascript()
            Me.bRequeryJavascriptArray = True
            Me.iProductId = oStorage.ProductID
            ' end: JDB - Dynamic Image Suite
        Else
            Me.Visible = False
        End If
    End Sub
#End Region

    'begin: GJV - 6/12/2007 - Attribute Detail Hotfix
    Public Sub SetAttributeControlDisplay()
        Dim dli As DataListItem
        For Each dli In DlAttributes.Items

            Dim lbl As Label = DirectCast(dli.FindControl("attName1"), Label)

            Dim lc As ListControl = Nothing
            If DisplayType = t_DisplayType.DropDown Then
                lc = CType(dli.FindControl("AttributeName"), ListControl)
            ElseIf DisplayType = t_DisplayType.Radio Then
                lc = CType(dli.FindControl("AttributeName2"), ListControl)
            End If

            If lc.Items.Count = 1 Then

                lc.SelectedIndex = 0
                lc.Visible = False

                lbl.Text = String.Format("{0}: {1}", lbl.Text, lc.Items(0).Text)
                lbl.Visible = True

            Else

                lc.Visible = True

                lbl.Text = String.Format("{0}:", lbl.Text)
                lbl.Visible = True

            End If

        Next dli

    End Sub
    'end: GJV - 6/12/2007 - Attribute Detail Hotfix

#Region "Public ReadOnly Property OrderAttributes() As ArrayList"


    Public ReadOnly Property OrderAttributes() As ArrayList

        Get
            Return m_arOrderAttributes
        End Get

    End Property
#End Region

#Region "Private Sub DlAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DlAttributes.ItemCreated"
    '    Private Sub DlAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DlAttributes.ItemCreated
    Private Sub DlAttributes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DlAttributes.ItemDataBound
        Dim oAttribute As CAttribute
        oAttribute = (e.Item.DataItem)

        'begin: - GJV - 6/12/2007 - Attribute Detail Hotfix

        'populate DropDown
        'If IsNothing(oAttribute) = False Then
        '    If DisplayType = t_DisplayType.DropDown Then
        '        If oAttribute.AttributeType = tAttributeType.Normal Then
        '            CType(e.Item.FindControl("AttributeName2"), RadioButtonList).Visible = False
        '            CType(e.Item.FindControl("AttributeName"), DropDownList).Visible = True
        '            CType(e.Item.FindControl("attName1"), Label).Visible = False
        '            CType(e.Item.FindControl("AttributeName"), DropDownList).DataSource = oAttribute.AttributeDetails
        '            CType(e.Item.FindControl("AttributeName"), DropDownList).DataBind()
        '        End If
        '    ElseIf DisplayType = t_DisplayType.Radio Then
        '        If oAttribute.AttributeType = tAttributeType.Normal Then
        '            Dim rdList As RadioButtonList
        '            Dim oAttdetail As CAttributeDetail
        '            For Each oAttdetail In oAttribute.AttributeDetails
        '                If oAttdetail.UID = -1 Then
        '                    oAttribute.AttributeDetails.Remove(oAttdetail)
        '                    Exit For
        '                End If
        '            Next
        '            CType(e.Item.FindControl("attName1"), Label).Visible = True
        '            CType(e.Item.FindControl("AttributeName"), DropDownList).Visible = False
        '            CType(e.Item.FindControl("AttributeName2"), RadioButtonList).Visible = True
        '            rdList = CType(e.Item.FindControl("AttributeName2"), RadioButtonList)
        '            rdList.DataSource = oAttribute.AttributeDetails   ' CreateDataSource()
        '            rdList.ClearSelection()
        '            rdList.SelectedIndex = 0
        '            rdList.DataBind()
        '        End If
        '    End If
        'End If

        If Not oAttribute Is Nothing Then

            Dim lc As ListControl = Nothing

            If DisplayType = t_DisplayType.DropDown Then
                lc = CType(e.Item.FindControl("AttributeName"), ListControl)
            ElseIf DisplayType = t_DisplayType.Radio Then
                lc = CType(e.Item.FindControl("AttributeName2"), ListControl)
            End If

            'note: remove the attribute name from the datasource
            Dim oAttdetail As CAttributeDetail
            For Each oAttdetail In oAttribute.AttributeDetails
                If oAttdetail.UID = -1 Then
                    oAttribute.AttributeDetails.Remove(oAttdetail)
                    Exit For
                End If
            Next

            'note: bind the datasource to the control
            lc.DataSource = oAttribute.AttributeDetails
            lc.DataBind()

            ' begin: JDB - Dynamic Image Suite
            ' note: for some reason, ItemCreated is being called twice for every dataitem.  in order to work around this,
            '   the number of iterations of the first dataitem being processed is being tracked and the code only executed
            '   the second time.
            If Me.DisplayType = t_DisplayType.DropDown Then
                lc.Attributes.Add("onchange", "javascript:CheckAttributeValues(ao" + Me.ClientID + ",false);")
            Else
                lc.Attributes.Add("onclick", "javascript:CheckAttributeValues(ao" + Me.ClientID + ",false);")
            End If
            ' note: begin option 1
            ' note: not using option 1 because the multiple iterations of event execution are no longer
            '   occurring on the seach but are still occurring on the product detail
            'If e.Item.ItemIndex = 0 Then
            '    Me.iIterationsOfItemCreated += 1
            'End If
            'If Me.iIterationsOfItemCreated = 2 Then
            '    Me.AddToJavascriptArray(oAttribute, lc)
            'End If
            ' note: end option 1
            ' note: begin option 2
            If Me.aoAttributes.Length < (e.Item.ItemIndex + 1) Then
                ReDim Preserve Me.aoAttributes(e.Item.ItemIndex)
            End If
            Me.aoAttributes(e.Item.ItemIndex) = oAttribute
            If Me.aoListControls.Length < (e.Item.ItemIndex + 1) Then
                ReDim Preserve Me.aoListControls(e.Item.ItemIndex)
            End If
            Me.aoListControls(e.Item.ItemIndex) = lc
            ' note: end option 2
            ' end: JDB - Dynamic Image Suite
        End If
        'end: GJV - 6/12/2007 - Attribute Detail Hotfix
    End Sub
#End Region

#Region " Function CreateDataSource() As ICollection"
    Function CreateDataSource() As ICollection
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("IntegerValue", GetType(Int32)))
        dt.Columns.Add(New DataColumn("StringValue", GetType(String)))
        dt.Columns.Add(New DataColumn("DateTimeValue", GetType(DateTime)))
        dt.Columns.Add(New DataColumn("BoolValue", GetType(Boolean)))
        dt.Columns.Add(New DataColumn("CurrencyValue", GetType(Decimal)))

        Dim i As Integer
        For i = 0 To 8
            dr = dt.NewRow()
            dr(0) = i
            dr(1) = "Item " + i.ToString()
            dr(2) = DateTime.Now
            If (i Mod 2) <> 0 Then
                dr(3) = True
            Else
                dr(3) = False
            End If
            dr(4) = 1.23 * (i + 1)

            dt.Rows.Add(dr)
        Next i

        Dim dv As New DataView(dt)
        Return dv
    End Function
#End Region

#Region " Private Sub SetDisplay()"
    Private Sub SetDisplay()
        Dim objNode As XmlNode
        objNode = dom.Item("SiteProducts").Item("SearchResult")
        If CInt("0" & objNode.Attributes("AttributeDisplay").Value) = 0 Then
            DisplayType = t_DisplayType.DropDown
        Else
            DisplayType = t_DisplayType.Radio
        End If
    End Sub
#End Region

    '1521
#Region "Function CustomerSpecificAttributePrice(ByVal AttributesTotal as Decimal, ByVal productRow As DataRow, ByVal mCustomer As CCustomer) As Decimal"
    Function CustomerSpecificAttributePrice(ByVal AttributesTotal As Decimal, ByVal productRow As DataRow, ByVal mCustomer As CCustomer) As Decimal
        Dim m_CustomerSpecificAttributePrice As Decimal
        Dim drCustPricing As DataRow
        For Each drCustPricing In productRow.GetChildRows("ProductsCustomerSpecificPricing")
            If mCustomer.CustomerGroup = drCustPricing("GroupID") Then
                m_CustomerSpecificAttributePrice = FormatNumber(CalculateCSAttributePrice(drCustPricing, AttributesTotal.ToString), 2)
                Return m_CustomerSpecificAttributePrice
            End If
        Next
        m_CustomerSpecificAttributePrice = AttributesTotal
        Return m_CustomerSpecificAttributePrice
    End Function
#End Region

    '1521
#Region "Private Function CalculateCSAttributePrice(ByVal dr As DataRow, ByVal sPrice As String, ByVal sCost As String) As Decimal"
    '##SUMMARY calulates Customer specific Attribute price
    Private Function CalculateCSAttributePrice(ByVal dr As DataRow, ByVal sAttributePrice As String) As Decimal
        Dim csAttributePrice As Decimal
        Select Case CInt(dr("GroupTypeID"))
            Case 2
                csAttributePrice = CDec(sAttributePrice) - (CDec(sAttributePrice) * CDec(dr("Amount")))
            Case Else
                csAttributePrice = CDec(sAttributePrice)
        End Select
        Return csAttributePrice
    End Function
#End Region

    'Try ViewState
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me.DisplayType = savedState(1)
    End Sub
    Protected Overrides Function SaveViewState() As Object
        Dim myState(2) As Object
        myState(0) = MyBase.SaveViewState
        myState(1) = Me.DisplayType
        Return myState
    End Function

    ' begin: JDB - Dynamic Image Suite
    Private iProductId As Integer
    ' note: begin option 1
    ' note: not using begin option 1 because the multiple iterations of event execution are no longer
    '   occurring on the seach but are still occurring on the product detail
    'Private iIterationsOfItemCreated As Integer = 0
    'Private aoAttributeControl() As Control = {}
    'Private iAttributeCount As Integer = 0
    ' note: end option 1
    Private sJavascriptArray As String = ""
    Private bRequeryJavascriptArray As Boolean = False
    Private Function GetAttributeValidationJavascript() As String
        ' note: begin option 1
        ' note: not using option 1 because the multiple iterations of event execution are no longer
        '   occurring on the seach but are still occurring on the product detail
        'If Me.iAttributeCount Then
        '   Dim sInvalidMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "Combination", "Invalid")
        '   Dim sOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "StockStatus", "OutOfStock")
        '   Dim sInvalidOrOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "LastAttributeValue", "InvalidOrOutOfStock")
        '   Dim sMessageVariables As String = String.Format("var sInvalid = '{0}'; var sOutOfStock = '{1}'; var sInvalidOrOutOfStock = '{2}", sInvalidMessage, sOutOfStockMessage, sInvalidOrOutOfStockMessage)
        '
        '    Dim asAttributeClientIDs(Me.iAttributeCount - 1) As String
        '    For iAttributeIndex As Integer = 0 To Me.iAttributeCount - 1
        '        Dim oListControl As ListControl = CType(Me.aoAttributeControl(iAttributeIndex), Control)
        '        asAttributeClientIDs(iAttributeIndex) = oListControl.ClientID
        '        'oListControl.Attributes.Add("onchange", "javascript:CheckAttributeValues(ao" + Me.ClientID + ",false);")
        '    Next
        '    Dim sUnavailable As String = ""
        '    Dim sOutOfStock As String = ""
        '    Dim oInventoryManagement = New Inventory_Management(Me.iProductId)
        '    For Each oInventoryItem As CInventoryItem In oInventoryManagement.InventoryItems()
        '        If Not oInventoryManagement.Inventory.CanBackOrder Then
        '            If oInventoryItem.Quantity <= 0 Then
        '                If sOutOfStock.Length > 0 Then
        '                    sOutOfStock += ","
        '                End If
        '                sOutOfStock += """" + oInventoryItem.AttributeDetailID + """"
        '            End If
        '        End If
        '        If Not oInventoryItem.Valid Then
        '            If sUnavailable.Length > 0 Then
        '                sUnavailable += ","
        '            End If
        '            sUnavailable += """" + oInventoryItem.AttributeDetailID + """"
        '        End If
        '    Next
        '    Dim sScript As String = _
        '        "<script language=""Javascript"">" + _
        '        sMessageVariables + _
        '        "var ao" + Me.ClientID + " = new Array(" + _
        '        String.Format(Me.sJavascriptArray, asAttributeClientIDs) + _
        '        "new Array(" + sUnavailable + ")," + _
        '        "new Array(" + sOutOfStock + ")" + _
        '        ");CheckAttributeValues(ao" + Me.ClientID + ",true);</script>"
        '    Return sScript
        ' note: end option 1
        ' note: begin option 2
        If Me.aoAttributes.Length > 0 AndAlso Me.aoListControls.Length > 0 Then
            Dim sInvalidMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "Combination", "Invalid")
            Dim sOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "StockStatus", "OutOfStock")
            Dim sInvalidOrOutOfStockMessage As String = StoreFrontConfiguration.MessagesAccess.GetXMLMessage("AttributeMessage", "LastAttributeValue", "InvalidOrOutOfStock")
            Dim sMessageVariables As String = String.Format("var sInvalid = '{0}'; var sOutOfStock = '{1}'; var sInvalidOrOutOfStock = '{2}';", sInvalidMessage, sOutOfStockMessage, sInvalidOrOutOfStockMessage)

            Dim sUnavailable As String = ""
            Dim sOutOfStock As String = ""
            Dim oInventoryManagement As Inventory_Management = New Inventory_Management(Me.iProductId)
            For Each oInventoryItem As CInventoryItem In oInventoryManagement.InventoryItems()
                If oInventoryManagement.Inventory.InventoryTracked AndAlso (Not oInventoryManagement.Inventory.CanBackOrder) Then
                    If oInventoryItem.Quantity <= 0 Then
                        If sOutOfStock.Length > 0 Then
                            sOutOfStock += ","
                        End If
                        sOutOfStock += """" + oInventoryItem.AttributeDetailID + """"
                    End If
                End If
                If Not oInventoryItem.Valid Then
                    If sUnavailable.Length > 0 Then
                        sUnavailable += ","
                    End If
                    sUnavailable += """" + oInventoryItem.AttributeDetailID + """"
                End If
            Next
            Dim sScript As String = _
                "<script language=""Javascript"">" + _
                sMessageVariables + _
                "var ao" + Me.ClientID + " = new Array(" + _
                Me.sJavascriptArray + _
                "new Array(" + sUnavailable + ")," + _
                "new Array(" + sOutOfStock + ")" + _
                ");CheckAttributeValues(ao" + Me.ClientID + ",true);</script>"
            Return sScript
        End If
        Return ""
        ' note: end option 2
    End Function
    ' end: JDB - Dynamic Image Suite

End Class
#End Region
