Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.Management
Imports StoreFront.SystemBase

Public MustInherit Class GiftCertificates
    Inherits CWebControl
    Protected WithEvents btnApply As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgApply As System.Web.UI.WebControls.Image
    Protected WithEvents Spacer1 As HtmlTableRow
    Protected WithEvents Spacer2 As HtmlTableRow
    Protected WithEvents Spacer3 As HtmlTableRow
    Protected WithEvents Label As HtmlTableRow
    Protected WithEvents BarSpacer As HtmlTableRow
    Protected WithEvents Table1 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents GiftCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftApply As System.Web.UI.WebControls.Button
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents GiftsApplied As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents txtGiftCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftTable As System.Web.UI.WebControls.DataList
    Protected WithEvents txtGiftCertificateCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents GiftCertificateTable As System.Web.UI.WebControls.DataList
    Protected WithEvents GiftCertTable As HtmlTableRow

    Event GiftCertificateAdd As EventHandler
    Event GiftCertificateRemove As EventHandler
    Event GiftCertificateError As EventHandler

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
        Dim objOrder As COrder = Session("Order")
        If (IsNothing(GiftCertificateTable) = False And IsNothing(objOrder) = False) Then
            GiftCertificateTable.DataSource = objOrder.GiftCertificates
            GiftCertificateTable.DataBind()
            If (objOrder.GiftCertificates.Count = 0) Then
                GiftCertificateTable.Visible = False
            Else
                GiftCertificateTable.Visible = True
            End If
        End If

        imgApply.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Apply").Attributes("Filepath").Value

    End Sub

    Public Sub ReloadList()
        Dim objOrder As COrder = Session("Order")
        If (IsNothing(GiftCertificateTable) = False And IsNothing(objOrder) = False) Then
            txtGiftCertificateCode.Text = ""
            GiftCertificateTable.DataSource = objOrder.GiftCertificates
            GiftCertificateTable.DataBind()
            If (objOrder.GiftCertificates.Count = 0) Then
                GiftCertificateTable.Visible = False
            Else
                GiftCertificateTable.Visible = True
            End If
            Dim con As DataListItem
            'For Each con In GiftCertificateTable.Items
            '    CType(con.FindControl("imgGiftCertificateRemove"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filepath").Value
            'Next
        End If
    End Sub

    Private Sub ReCalculate()
        Dim objOrder As COrder = Session("Order")
        Dim objGiftCertificate As CGiftCertificate
        Dim dTotal As Decimal = objOrder.OrderTotal

        For Each objGiftCertificate In objOrder.GiftCertificates
            If (objGiftCertificate.DollarOff < dTotal) Then
                objGiftCertificate.Remaining = 0.0
                dTotal = dTotal - objGiftCertificate.DollarOff
            Else
                objGiftCertificate.Remaining = objGiftCertificate.DollarOff - dTotal
                dTotal = 0
            End If
        Next

        ReloadList()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim objOrder As COrder = Session("Order")
        Dim objGiftCertificate As CGiftCertificate
        Dim bFound As Boolean = False
        Dim objStoreGiftCertificates As New CStoreGiftCertificates()
        Dim m_objGiftCertificates As CGiftCertificates = objStoreGiftCertificates.GetGiftCertificates

        For Each objGiftCertificate In objOrder.GiftCertificates
            If (objGiftCertificate.Code = txtGiftCertificateCode.Text) Then
                RaiseEvent GiftCertificateError(m_objMessages.GetXMLMessage("GiftCertificates", "Error", "AlreadyUsed"), EventArgs.Empty)
                Exit Sub
            End If
        Next

        For Each objGiftCertificate In m_objGiftCertificates.GiftCertificates
            If (objGiftCertificate.Code = txtGiftCertificateCode.Text) Then
                bFound = True
                If (m_objGiftCertificates.CanAddGiftCertificate(objGiftCertificate)) Then
                    objOrder.AddGiftCertificate(objGiftCertificate)
                    GiftCertificateTable.DataSource = objOrder.GiftCertificates
                    GiftCertificateTable.DataBind()
                    GiftCertificateTable.Visible = True
                    'Dim con As DataListItem
                    'For Each con In GiftCertificateTable.Items
                    '    CType(con.FindControl("imgGiftCertificateRemove"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filepath").Value
                    'Next
                    ReCalculate()
                    RaiseEvent GiftCertificateAdd(Nothing, EventArgs.Empty)
                Else
                    ' Error Message
                    ReCalculate()
                    RaiseEvent GiftCertificateError(m_objMessages.GetXMLMessage("GiftCertificates", "Error", m_objGiftCertificates.AddGiftCertificateError), EventArgs.Empty)
                End If
                Exit For
            End If
        Next
        txtGiftCertificateCode.Text = ""
        If (bFound = False) Then
            ReCalculate()
            RaiseEvent GiftCertificateError(m_objMessages.GetXMLMessage("GiftCertificates", "Error", "NotFound"), EventArgs.Empty)
        End If
        'ReCalculate()
    End Sub

    Public Sub RemoveGiftCertificate(ByVal sender As Object, ByVal e As EventArgs)
        Dim objOrder As COrder = Session("Order")
        Dim objButton As LinkButton = CType(sender, System.Web.UI.WebControls.LinkButton)
        Dim objGiftCertificate As CGiftCertificate
        For Each objGiftCertificate In objOrder.GiftCertificates
            If (objGiftCertificate.ID = CLng(objButton.CommandArgument)) Then
                objOrder.DeleteGiftCertificate(objGiftCertificate)
                Exit For
            End If
        Next
        txtGiftCertificateCode.Text = ""
        ReCalculate()
        RaiseEvent GiftCertificateRemove(Nothing, EventArgs.Empty)
        'ReCalculate()
    End Sub

    Private Sub GiftCertificateTable_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles GiftCertificateTable.ItemCreated
        CType(e.Item.FindControl("imgGiftCertificateRemove"), System.Web.UI.WebControls.Image).ImageUrl = StoreFrontConfiguration.XMLDocument.Item("SiteProducts").Item("SiteImages").Item("Remove").Attributes("Filepath").Value
    End Sub
End Class
