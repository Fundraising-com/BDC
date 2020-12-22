Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Partial  Class CSwatch
    Inherits CWebControl

    'BEGIN CUSTOM CODE 3/6/04
    Protected WithEvents ProductImage As System.Web.UI.WebControls.Panel
    Protected WithEvents dlSwatch As System.Web.UI.WebControls.DataList
    Protected WithEvents Table4 As System.Web.UI.HtmlControls.HtmlTable
    Protected TopSW As SwatchDataList
    Protected BottomSW As SwatchDataList
    Protected LeftSW As SwatchDataList
    Protected RightSW As SwatchDataList

    Private m_SwatchAllignment As Integer
    Private m_Swatches As ArrayList
    Private m_ChangeOnMouseover As Boolean
    Private m_ChangeOnClick As Boolean
    Private m_PerRow As Integer
    Private m_IsCloseUp As Boolean = False
    Private m_DescriptionAllignment As Integer
    'END CUSTOM CODE 3/6/04

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

    Public WriteOnly Property DescriptionAllignment() As Integer
        Set(ByVal Value As Integer)
            m_DescriptionAllignment = Value
        End Set
    End Property
    Public WriteOnly Property SwatchAllignment() As Integer
        Set(ByVal Value As Integer)
            m_SwatchAllignment = Value
        End Set
    End Property
    Public WriteOnly Property IsCloseUp() As Boolean
        Set(ByVal Value As Boolean)
            m_IsCloseUp = Value
        End Set
    End Property
    Public WriteOnly Property ChangeOnClick() As Boolean
        Set(ByVal Value As Boolean)
            m_ChangeOnClick = Value
        End Set
    End Property
    Public WriteOnly Property ChangeOnMouseover() As Boolean
        Set(ByVal Value As Boolean)
            m_ChangeOnMouseover = Value
        End Set
    End Property
    Public WriteOnly Property PerRow() As Integer
        Set(ByVal Value As Integer)
            m_PerRow = Value
        End Set
    End Property
    Public WriteOnly Property Swatches() As ArrayList
        Set(ByVal Value As ArrayList)
            m_Swatches = Value
        End Set
    End Property
    Private m_oAttributeControl As CAttributeControl
    Public WriteOnly Property AttributeControl() As CAttributeControl
        Set(ByVal Value As CAttributeControl)
            Me.m_oAttributeControl = Value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Tee 10/23/2007 added
        If IsPostBack Then Exit Sub
        'end Tee
        Dim dlSwatch As DataList = Nothing
        Select Case m_SwatchAllignment
            Case 0 'left
                dlSwatch = CType(LeftSW.FindControl("dlSwatch"), DataList)
                LeftSW.DescriptionAllignment = m_DescriptionAllignment
                dlSwatch.DataSource = m_Swatches
                dlSwatch.DataBind()
                TopSW.Visible = False
                RightSW.Visible = False
                BottomSW.Visible = False
            Case 1 'right
                dlSwatch = CType(RightSW.FindControl("dlSwatch"), DataList)
                RightSW.DescriptionAllignment = m_DescriptionAllignment
                dlSwatch.DataSource = m_Swatches
                dlSwatch.DataBind()
                LeftSW.Visible = False
                TopSW.Visible = False
                BottomSW.Visible = False
            Case 2 'top
                dlSwatch = CType(TopSW.FindControl("dlSwatch"), DataList)
                TopSW.DescriptionAllignment = m_DescriptionAllignment
                dlSwatch.DataSource = m_Swatches
                dlSwatch.DataBind()
                LeftSW.Visible = False
                RightSW.Visible = False
                BottomSW.Visible = False
            Case 3 'bottom
                dlSwatch = CType(BottomSW.FindControl("dlSwatch"), DataList)
                BottomSW.DescriptionAllignment = m_DescriptionAllignment
                dlSwatch.DataSource = m_Swatches
                dlSwatch.DataBind()
                LeftSW.Visible = False
                RightSW.Visible = False
                TopSW.Visible = False
        End Select
        'Tee 10/1/2007 2.0 transition
        If IsNothing(dlSwatch) Then Exit Sub
        'end Tee
        dlSwatch.RepeatColumns = m_PerRow
        Dim txtFunctionCall As String = ""
        Dim dlItem As DataListItem

        Dim sSwatchJavascriptArray As String = ""
        Dim sSwatchJavascriptItems As String = ""
        If Not Me.m_IsCloseUp Then
            sSwatchJavascriptArray = "<script language=""Javascript"">var aoSwatch= new Array({0});</script>"
        End If

        For Each dlItem In dlSwatch.Items
            txtFunctionCall = "Javascript:chgImg('imgProductImage','thumb" & dlItem.ItemIndex & "'," & m_Swatches(dlItem.ItemIndex).SwatchID & ");"
            Dim oSwatch As Swatch = CType(Me.m_Swatches(dlItem.ItemIndex), Swatch)
            If (Not Me.m_IsCloseUp) AndAlso oSwatch.AttributeNames.Length > 0 Then
                txtFunctionCall += "javascript:SetAttributeValues(ao" + Me.m_oAttributeControl.ClientID + ", '" + oSwatch.AttributeNames + "','" + oSwatch.Name + "');"

                If sSwatchJavascriptItems.Length > 0 Then
                    sSwatchJavascriptItems += ","
                End If
                sSwatchJavascriptItems += String.Format("new Swatch('thumb{0}', '{1}', '{2}', '{3}')", dlItem.ItemIndex, oSwatch.SwatchID, oSwatch.Name, oSwatch.AttributeNames)
            End If
            If m_ChangeOnClick = False Then
                CType(dlItem.FindControl("LinkSwatch"), HyperLink).Attributes.Add("onmouseover", txtFunctionCall)
                CType(dlItem.FindControl("LinkSwatchText"), HyperLink).Attributes.Add("onmouseover", txtFunctionCall)
                CType(dlItem.FindControl("LinkSwatch"), HyperLink).NavigateUrl = ""
                CType(dlItem.FindControl("LinkSwatchText"), HyperLink).NavigateUrl = ""
            Else
                CType(dlItem.FindControl("LinkSwatch"), HyperLink).NavigateUrl = txtFunctionCall
                CType(dlItem.FindControl("LinkSwatchText"), HyperLink).NavigateUrl = txtFunctionCall
            End If
        Next

        If (Not Me.m_IsCloseUp) AndAlso sSwatchJavascriptArray.Length > 0 AndAlso sSwatchJavascriptItems.Length > 0 Then
            Me.Parent.Page.ClientScript.RegisterStartupScript(Me.Parent.GetType, "aoSwatch", String.Format(sSwatchJavascriptArray, sSwatchJavascriptItems))
        End If

        If m_IsCloseUp = True Then
            Me.CloseUp.Visible = False
            Me.imgHyperlink.NavigateUrl = ""
        End If

    End Sub

    'BEGIN CUSTOM CODE Oct '04
    Private Sub DLSwatch_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlSwatch.ItemCreated

    End Sub
    'END CUSTOM CODE Oct '04

End Class
