Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports System.Xml


Public MustInherit Class VolumePricing
    Inherits CWebControl

    Protected WithEvents lblVolumePricing As System.Web.UI.WebControls.Label
    Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Table1 As System.Web.UI.WebControls.Table
    Private m_Id As Long
    Private m_arIDs = New ArrayList()

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
        lblVolumePricing.Text = "Volume Price Information"
        If Request.QueryString("ID") > 0 Then
            Dim ds As DataTable
            Dim objVolumeManager As New CVolumePricingManager()
            Dim dgItem As DataGridItem
            Dim tempPrice As Decimal
            Dim lProdID As Long
            lProdID = Request.QueryString("ID")

            If lProdID <> 0 Then
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    If IsNothing(Me.m_objXMLAccess) Then
                        Me.m_objXMLAccess = StoreFrontConfiguration.ProductAccess
                    End If
                    ds = objVolumeManager.GetVolumePrices(lProdID, m_objXMLAccess)
                Else
                    ds = dtVolumePrices(objVolumeManager.GetVolumePricesByID(lProdID, True).Tables(0))
                End If
                '      ds = objVolumeManager.GetVolumePrices(lProdID, m_objXMLAccess)
                If ds.Rows.Count = 0 Then
                    lblVolumePricing.Text = "&nbsp;Volume Pricing not available" ' for " & Request.QueryString("ProdID")
                End If
            Else
                lblVolumePricing.Text = "&nbsp;No Product Specified"
            End If
            DataGrid1.DataSource = ds

            DataGrid1.DataBind()
        End If
    End Sub

    Public WriteOnly Property ProdID() As String
        Set(ByVal Value As String)
            Dim lngProdID As Long = CLng("0" & Value)
            Dim ds As DataTable
            Dim dr As DataRow
            Dim objProd As CProduct
            If Me.Visible = False Then Exit Property
            If lngProdID <> 0 Then
                Dim objVolumeManager As New CVolumePricingManager()
                If IsNothing(Me.m_objXMLAccess) Then
                    Me.m_objXMLAccess = StoreFrontConfiguration.ProductAccess
                End If
                If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                    ds = objVolumeManager.GetVolumePrices(lngProdID, m_objXMLAccess)
                Else
                    ds = dtVolumePrices(objVolumeManager.GetVolumePricesByID(lngProdID, True).Tables(0))
                    'Dim objProdMan As New Management.CProductManagement()
                    'Dim dsprod As DataRow = objProdMan.GetProductRow(lngProdID, m_objcustomer.CustomerGroup).Tables(0).Rows(0)
                    'objProd = New CProduct(dsprod, StoreFrontConfiguration.ProductLoading, Nothing, False)

                End If
                If ds.Rows.Count = 0 Then
                    lblVolumePricing.Text = "&nbsp;Volume Pricing not available" ' for " & Request.QueryString("ProdID")
                Else
                    For Each dr In ds.Rows

                    Next
                End If
            Else
                lblVolumePricing.Text = "&nbsp;No Product Specified"
            End If
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
        End Set
    End Property


    Public Sub data_bind(ByVal lProdID As Long, ByVal objXMLAccess As CXMLProductAccess)
        Dim ds As DataTable
        Dim dgItem As DataGridItem
        Dim tempPrice As Decimal
        lblVolumePricing.Text = "&nbsp; Volume Pricing Information"
        If lProdID <> 0 Then
            Dim objVolumeManager As New CVolumePricingManager()
            If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
                If IsNothing(Me.m_objXMLAccess) Then
                    Me.m_objXMLAccess = StoreFrontConfiguration.ProductAccess
                End If
                ds = objVolumeManager.GetVolumePrices(lProdID, m_objXMLAccess)
            Else
                ds = dtVolumePrices(objVolumeManager.GetVolumePricesByID(lProdID, True).Tables(0))
            End If
            If ds.Rows.Count = 0 Then
                lblVolumePricing.Text = "&nbsp;Volume Pricing not available" ' for " & Request.QueryString("ProdID")
            End If
        Else
            lblVolumePricing.Text = "&nbsp;No Product Specified"
        End If

        DataGrid1.DataSource = ds

        DataGrid1.DataBind()
    End Sub

#Region "Public Function GetVolumePrices() As DataTable"
    '##SUMMARY Returns as DataTable of VolumePrices on the specified Product from the in-memory XmlDocument.
    Public Function dtVolumePrices(ByVal dtLive As DataTable) As DataTable

        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim objNode2 As XmlNode
        Dim drLive As DataRow

        dt.Columns.Add(New DataColumn("BreakLevel", GetType(Integer)))
        dt.Columns.Add(New DataColumn("DollarOrPercent", GetType(Integer)))
        dt.Columns.Add(New DataColumn("VPrice", GetType(String)))
        dt.Columns.Add(New DataColumn("Amount", GetType(String)))



        For Each drLive In dtLive.Rows
            If CalculateVolumePrice(drLive, drLive("ProductPrice")) >= 0 Then
                dr = dt.NewRow()
                dr(0) = CInt("0" & drLive("BreakLevel").ToString)
                dr(1) = CInt("0" & drLive("DollarOrPercent").ToString)
                dr(2) = Format(CalculateVolumePrice(drLive, drLive("ProductPrice")), "C")
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
            vprice = 0
        End If
        Return vPrice
    End Function
#End Region

End Class
