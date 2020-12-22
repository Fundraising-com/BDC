Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.Xml


Partial  Class VolumePricing
    Inherits CWebControl

    Protected WithEvents Table1 As System.Web.UI.WebControls.Table
    Private m_Id As Long
    Private m_arIDs As New ArrayList()

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

#Region "Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVolumePricing.Text = String.Format("{0} Information", StoreFrontConfiguration.Labels.Item("lblVolumePrice").InnerText())
        If Request.QueryString("ID") > 0 Then
            Dim lProdID As Long
            lProdID = Request.QueryString("ID")
            LoadVolumePricing(lProdID)
        End If
    End Sub
#End Region

#Region "ProdID() As String"
    Public WriteOnly Property ProdID() As String
        Set(ByVal Value As String)
            Dim lngProdID As Long = CLng("0" & Value)
            If Me.Visible = False Then Exit Property
            LoadVolumePricing(lngProdID)
        End Set
    End Property
#End Region

#Region "DataSource"
    Public WriteOnly Property DataSource() As DataTable
        Set(ByVal Value As DataTable)
            DataGrid1.DataSource = dtVolumePrices(Value)
            DataGrid1.DataBind()
        End Set
    End Property
#End Region

#Region "LoadVolumePricing"
    Private Sub LoadVolumePricing(ByVal lngProdID As Long)
        Dim dt As DataTable = Nothing
            If lngProdID <> 0 Then
            Dim objVolumeManager As New CVolumePricingManager
                If IsNothing(Me.m_objXMLAccess) Then
                    Me.m_objXMLAccess = StoreFrontConfiguration.ProductAccess
                End If
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                dt = objVolumeManager.GetVolumePrices(lngProdID, m_objXMLAccess)
                Else
                dt = dtVolumePrices(objVolumeManager.GetVolumePricesByID(lngProdID, True).Tables(0))
                    'Dim objProdMan As New Management.CProductManagement()
                    'Dim dsprod As DataRow = objProdMan.GetProductRow(lngProdID, m_objcustomer.CustomerGroup).Tables(0).Rows(0)
                    'objProd = New CProduct(dsprod, StoreFrontConfiguration.ProductLoading, Nothing, False)

                End If
            If dt.Rows.Count = 0 Then
                    lblVolumePricing.Text = "&nbsp;Volume Pricing not available" ' for " & Request.QueryString("ProdID")
                End If
            Else
                lblVolumePricing.Text = "&nbsp;No Product Specified"
            End If
        DataGrid1.DataSource = dt
            DataGrid1.DataBind()
    End Sub
#End Region

#Region "data_bind(ByVal lProdID As Long)"
    Public Sub data_bind(ByVal lProdID As Long)
        lblVolumePricing.Text = "&nbsp; Volume Pricing Information"
        LoadVolumePricing(lProdID)
    End Sub
#End Region

    '##SUMMARY Returns as DataTable of VolumePrices on the specified Product from the in-memory XmlDocument.
#Region "dtVolumePrices(ByVal dtLive As DataTable) As DataTable"
    Public Function dtVolumePrices(ByVal dtLive As DataTable) As DataTable

        Dim dt As New DataTable
        Dim dr As DataRow
        'Dim objNode2 As XmlNode
        Dim drLive As DataRow

        dt.Columns.Add(New DataColumn("BreakLevel", GetType(Integer)))
        dt.Columns.Add(New DataColumn("DollarOrPercent", GetType(Integer)))
        dt.Columns.Add(New DataColumn("VPrice", GetType(String)))
        dt.Columns.Add(New DataColumn("Amount", GetType(String)))

        For Each drLive In dtLive.Rows
            Dim vPrice As Decimal = CalculateVolumePrice(drLive, drLive("ProductPrice"))
            If vPrice >= 0 Then
                dr = dt.NewRow()
                dr(0) = CInt("0" & drLive("BreakLevel").ToString)
                dr(1) = CInt("0" & drLive("DollarOrPercent").ToString)
                dr(2) = Format(vPrice, "C")
                If dr(1) = 0 Then
                    dr(3) = Format(CDec("0" & drLive("Amount").ToString), "C")
                Else
                    dr(3) = CStr(CDec("0" & drLive("Amount").ToString) * 100) & "%"
                End If

                dt.Rows.Add(dr)
            End If
        Next

        Return dt

    End Function
#End Region

#Region "CalculateVolumePrice(ByVal dr As DataRow, ByVal price As String) As Decimal)"
    '##SUMMARY Gets the VolumePrice for the Product Row
    Private Function CalculateVolumePrice(ByVal dr As DataRow, ByVal sPrice As String) As Decimal
        Dim vPrice As Decimal
        If dr("DollarOrPercent") = 0 Then
            vPrice = CDec(sPrice) - CDec(dr("Amount"))
        Else
            vPrice = CDec(sPrice) - (CDec(sPrice) * CDec(dr("Amount")))
        End If
        If vPrice < 0 Then
            vPrice = 0
        End If
        Return vPrice
    End Function
#End Region

End Class
