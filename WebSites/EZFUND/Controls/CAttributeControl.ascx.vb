Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.Xml

#Region "Public MustInherit Class CAttributeControl"


Public MustInherit Class CAttributeControl
    Inherits CWebControl

#Region " Members"


    Event NewProductImage As EventHandler
    Protected WithEvents DlAttributes As System.Web.UI.WebControls.DataList
    Protected WithEvents dlCustomAttributes As System.Web.UI.WebControls.DataList
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
        Dim oItem As DataListItem
        '  SetDisplay()
        Dim Rdo As RadioButtonList
        Dim ddl As DropDownList

        If DisplayType = t_DisplayType.DropDown Then

            For Each oItem In DlAttributes.Items
                Rdo = CType(oItem.FindControl("AttributeName2"), RadioButtonList)
                ddl = CType(oItem.FindControl("AttributeName"), DropDownList)
                Rdo.Visible = False
                ddl.Visible = True

            Next
        Else
            For Each oItem In DlAttributes.Items
                Rdo = CType(oItem.FindControl("AttributeName2"), RadioButtonList)
                ddl = CType(oItem.FindControl("AttributeName"), DropDownList)
                Rdo.Visible = True
                ddl.Visible = False
                CType(oItem.FindControl("attName1"), Label).Visible = True

            Next

        End If

    End Sub
#End Region

#Region "Public WriteOnly Property ProdAttributes() As ArrayList"

    Public WriteOnly Property ProdAttributes() As ArrayList
        Set(ByVal Value As ArrayList)


            Dim objAtt As CAttribute
            Dim arNormal As New ArrayList()
            Dim arCustom As New ArrayList()
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

#Region "Public Sub Data_Bind(ByVal oProduct As CProduct)"


    Public Sub Data_Bind(ByVal oProduct As CProduct)

        Dim objAtt As CAttribute
        Dim arNormal As New ArrayList()
        Dim arCustom As New ArrayList()

        If oProduct.Attributes.Count > 0 Then
            For Each objAtt In oProduct.Attributes

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
    End Sub
#End Region

#Region "Public Sub Data_Bind(ByVal oStorage As CCategoryStorage)"


    Public Sub Data_Bind(ByVal oStorage As CCategoryStorage)

        Dim objAtt As CAttribute
        Dim arNormal As New ArrayList()
        Dim arCustom As New ArrayList()
        If oStorage.Attributes.Count > 0 Then
            For Each objAtt In oStorage.Attributes

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
    End Sub
#End Region

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

        Dim oTxt As TextBox

        oAttribute = (e.Item.DataItem)
        'populate DropDown

        If IsNothing(oAttribute) = False Then
            If DisplayType = t_DisplayType.DropDown Then
                If oAttribute.AttributeType = tAttributeType.Normal Then
                    CType(e.Item.FindControl("AttributeName2"), RadioButtonList).Visible = False
                    CType(e.Item.FindControl("AttributeName"), DropDownList).Visible = True
                    CType(e.Item.FindControl("attName1"), Label).Visible = False
                    CType(e.Item.FindControl("AttributeName"), DropDownList).DataSource = oAttribute.AttributeDetails
                    CType(e.Item.FindControl("AttributeName"), DropDownList).DataBind()
                End If
            ElseIf DisplayType = t_DisplayType.Radio Then
                If oAttribute.AttributeType = tAttributeType.Normal Then
                    Dim rdList As RadioButtonList
                    Dim oAttdetail As CAttributeDetail
                    For Each oAttdetail In oAttribute.AttributeDetails
                        If oAttdetail.UID = -1 Then
                            oAttribute.AttributeDetails.Remove(oAttdetail)
                            Exit For
                        End If
                    Next
                    CType(e.Item.FindControl("attName1"), Label).Visible = True
                    CType(e.Item.FindControl("AttributeName"), DropDownList).Visible = False
                    CType(e.Item.FindControl("AttributeName2"), RadioButtonList).Visible = True
                    rdList = CType(e.Item.FindControl("AttributeName2"), RadioButtonList)
                    rdList.DataSource = oAttribute.AttributeDetails   ' CreateDataSource()
                    rdList.ClearSelection()
                    rdList.SelectedIndex = 0
                    rdList.DataBind()
                    
                    '    rdList.Items(0).Selected = True
                End If
            End If

        End If
    End Sub
#End Region

#Region " Function CreateDataSource() As ICollection"


    Function CreateDataSource() As ICollection

        Dim dt As New DataTable()
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


End Class
#End Region
