Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management '1521
Imports StoreFront.SystemBase
Imports System.Xml

#Region "Public MustInherit Class CSRAttributes"


Partial  Class CSRAttributes
    Inherits CSRWebControl

#Region " Members"


    Event NewProductImage As EventHandler
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents AttributeName As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddImageHolder As System.Web.UI.WebControls.DropDownList
    'AttributeName
    Enum t_DisplayType
        DropDown = 0
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
        GetOrder()
        Dim oItem As DataListItem
        '  SetDisplay()
        Dim ddl As DropDownList


            For Each oItem In DlAttributes.Items
            ddl = CType(oItem.FindControl("AttributeName"), DropDownList)
                ddl.Visible = True

            Next

    End Sub
#End Region

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


#Region "Public Sub Data_Bind(ByVal oProduct As CProduct)"


    Public Sub Data_Bind(ByVal oProduct As CProduct, ByVal mCustomer As CCustomer)

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
            DlAttributes.DataBind()
            dlCustomAttributes.DataSource = arCustom
            dlCustomAttributes.DataBind()
        Else
            Me.Visible = False
        End If
    End Sub
#End Region

#Region "Public Sub Data_Bind(ByVal oStorage As CCategoryStorage)"


    Public Sub Data_Bind(ByVal oStorage As CCategoryStorage, ByVal mCustomer As CCustomer)

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
            DlAttributes.DataBind()
            dlCustomAttributes.DataSource = arCustom
            dlCustomAttributes.DataBind()
        Else
            Me.Visible = False
        End If
    End Sub
#End Region
    'Begin Custom Code 1/06
    Private m_SelectedAttributes As ArrayList
    Public Property SelectedAttributes() As ArrayList
        Get
            Return m_SelectedAttributes
        End Get
        Set(ByVal Value As ArrayList)
            m_SelectedAttributes = Value
        End Set
    End Property
    'End Custom Code 1/06
#Region "Public ReadOnly Property OrderAttributes() As ArrayList"


    Public ReadOnly Property OrderAttributes() As ArrayList

        Get
            Return m_arOrderAttributes
        End Get

    End Property
#End Region

#Region "Private Sub DlAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DlAttributes.ItemCreated"


    Private Sub DlAttributes_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DlAttributes.ItemCreated

        Dim oAttribute As CAttribute

        'Dim oTxt As TextBox

        oAttribute = (e.Item.DataItem)
        'populate DropDown

        If IsNothing(oAttribute) = False Then
            If oAttribute.AttributeType = tAttributeType.Normal Then
                'Begin Custom Code 1/06
                Dim DLAtt As DropDownList
                DLAtt = CType(e.Item.FindControl("AttributeName"), DropDownList)
                DLAtt.Visible = True
                CType(e.Item.FindControl("attName1"), Label).Visible = False
                DLAtt.DataSource = oAttribute.AttributeDetails
                DLAtt.DataBind()
                'End Custom Code 1/06


            End If

        End If
    End Sub
    Private Sub Page_Prerender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Dim dlItem As DataListItem
        Dim i As Integer = 0
        If IsNothing(DlAttributes.DataSource) = True Then
            Exit Sub
        End If
        For Each dlItem In Me.DlAttributes.Items

            Dim oAttribute As CAttribute

            oAttribute = DlAttributes.DataSource(i)
            If IsNothing(oAttribute) = False Then
                If IsNothing(oAttribute) = False Then
                    If oAttribute.AttributeType = tAttributeType.Normal Then
                        'Begin Custom Code 1/06
                        Dim DLAtt As DropDownList
                        DLAtt = CType(dlItem.FindControl("AttributeName"), DropDownList)
                        If IsNothing(SelectedAttributes) = False Then
                            Dim oAtt As CAttributesSelected
                            For Each oAtt In Me.SelectedAttributes
                                If oAtt.AttributeId = oAttribute.UID Then
                                    Dim oAttDet As CAttributeDetail
                                    Dim x As Integer = 0
                                    For Each oAttDet In oAttribute.AttributeDetails
                                        If oAtt.UID = oAttDet.UID Then
                                            DLAtt.SelectedIndex = x
                                            Exit For
                                        End If
                                        x = x + 1
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If
            End If
            i = i + 1
        Next
    End Sub
    'End Custom Code 1/06
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




    '1521
#Region "Function CustomerSpecificAttributePrice(ByVal AttributesTotal as Decimal, ByVal productRow As DataRow, ByVal mCustomer As CCustomer) As Decimal"
    Function CustomerSpecificAttributePrice(ByVal AttributesTotal As Decimal, ByVal productRow As DataRow, ByVal mCustomer As CCustomer) As Decimal
        Dim m_CustomerSpecificAttributePrice As Decimal
        Dim drCustPricing As DataRow
        'Dim drAttributes As DataRow
        'Dim attributePricing As String
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
    'Begin Custom Code 1/06
    Function CustomerSpecificAttributePrice(ByVal AttributesTotal As Decimal, ByVal productRow As DataRow, ByVal mCustomer As Customer) As Decimal
        'This is a copy of the above one where you can just Pass In a Customer instead of CCustomer
        Dim m_CustomerSpecificAttributePrice As Decimal
        Dim drCustPricing As DataRow
        'Dim drAttributes As DataRow
        'Dim attributePricing As String
        For Each drCustPricing In productRow.GetChildRows("ProductsCustomerSpecificPricing")
            If mCustomer.CustomerGroup = drCustPricing("GroupID") Then
                m_CustomerSpecificAttributePrice = FormatNumber(CalculateCSAttributePrice(drCustPricing, AttributesTotal.ToString), 2)
                Return m_CustomerSpecificAttributePrice
            End If
        Next
        m_CustomerSpecificAttributePrice = AttributesTotal
        Return m_CustomerSpecificAttributePrice
    End Function
    'End Custom Code 1/06
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



End Class
#End Region
