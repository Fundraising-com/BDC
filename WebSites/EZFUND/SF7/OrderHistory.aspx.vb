'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Orders
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class OrderHistory
    Inherits CWebPage

    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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

    Private m_objOrderHistory As COrders

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (m_objCustomer.IsSignedIn = False) Then
            Response.Redirect("CustSignIn.aspx")
            'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
            'ElseIf Not IsNothing(Session("anonymous")) Then
            '    ' anonymous user should not be able to access my acct page
            '    ' redirect to sign in page
            '    Response.Redirect("CustSignIn.aspx?SignOut=2")
            '    'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
        End If
        Try

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("OrderHistory.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, Nothing, MessageAlignment)

            Session("OrderHistory") = New BusinessRule.Orders.COrders
            Session("OrderHistory").LoadOrderHistory(m_objCustomer.GetCustomerID(), False)
            m_objOrderHistory = Session("OrderHistory")

            If (m_objOrderHistory.Orders.Count = 0) Then
                Message.Text = "Sorry! No Orders In History."
                Message.Visible = True
                DataGrid1.Visible = False
            Else
                If (IsPostBack = False) Then
                    DataGrid1.DataSource = m_objOrderHistory.Orders
                    DataGrid1.DataBind()
                End If
                Message.Visible = False
            End If

            If (DataGrid1.PageCount = 1) Then
                DataGrid1.PagerStyle.Visible = False
            End If
            Dim con As DataGridItem
            For Each con In DataGrid1.Items
                CType(con.FindControl("imgView"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("View").Attributes("Filepath").Value()
                CType(con.FindControl("imgTrack"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Track").Attributes("Filepath").Value()
            Next
        Catch ex As Exception
            Session("DetailError") = "Class OrderHistory Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Public Sub ViewDetailClick(ByVal source As Object, ByVal e As System.EventArgs)
        Response.Redirect("OrderDetail.aspx?OrderID=" & source.CommandArgument)
    End Sub

    Public Sub TrackShipmentClick(ByVal source As Object, ByVal e As System.EventArgs)
        Response.Redirect("OrderTracking.aspx?OrderID=" & source.CommandArgument)
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        m_objOrderHistory = Session("OrderHistory")
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DataGrid1.DataSource = m_objOrderHistory.Orders
        DataGrid1.DataBind()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If (e.Item.ItemType = ListItemType.Pager) Then
            Dim objCell As TableCell = e.Item.Controls(0)
            Dim objSpace As Label
            Dim objButton As New LinkButton()

            If (DataGrid1.CurrentPageIndex > 0) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objCell.Controls.AddAt(0, objSpace)
                objButton.Text = "< Previous"
                objCell.Controls.AddAt(0, objButton)
            End If
            If (DataGrid1.CurrentPageIndex < DataGrid1.PageCount - 1) Then
                objSpace = New Label()
                objSpace.Text = "&nbsp;"
                objButton = New LinkButton()
                objButton.Text = "Next >"
                objCell.Controls.Add(objSpace)
                objCell.Controls.Add(objButton)
            End If
        ElseIf (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            If (e.Item.ItemIndex > 0) Then
                CType(e.Item.FindControl("HeaderRow"), HtmlTableRow).Visible = False
            End If
        End If
        If Not IsPostBack Then

        End If

    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        If (e.CommandSource.GetType Is GetType(LinkButton)) Then
            Dim objButton As LinkButton = e.CommandSource
            m_objOrderHistory = Session("OrderHistory")

            If (objButton.Text.IndexOf("Next") <> -1) Then
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex + 1
            ElseIf (objButton.Text.IndexOf("Previous") <> -1) Then
                DataGrid1.CurrentPageIndex = CType(source, DataGrid).CurrentPageIndex - 1
            End If
            DataGrid1.DataSource = m_objOrderHistory.Orders
            DataGrid1.DataBind()
        End If
    End Sub

    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If (Not (IsNothing(e.Item.FindControl("imgView")))) Then
            CType(e.Item.FindControl("imgView"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("View").Attributes("Filepath").Value()
        End If
        If (Not (IsNothing(e.Item.FindControl("imgTrack")))) Then
            CType(e.Item.FindControl("imgTrack"), System.Web.UI.WebControls.Image).ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Track").Attributes("Filepath").Value()
        End If
    End Sub


End Class
