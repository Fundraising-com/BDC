Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports System.Xml

Public MustInherit Class FulFillmentControl
    Inherits CWebControl
    Protected WithEvents ddDateRange As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddPayments As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddShipments As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdGetOrders As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtFree As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdLocateOrder As System.Web.UI.WebControls.LinkButton

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
        Message.Visible = False
        If IsPostBack Then
            If ddDateRange.SelectedItem.Value = 5 Then

                txtFrom.Enabled = True
                txtTo.Enabled = True
            Else

                txtFrom.Enabled = False
                txtTo.Enabled = False
            End If
        End If
        'cmdGetOrders.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSubmit").Attributes("Filepath").Value()
        'cmdLocateOrder.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSubmit").Attributes("Filepath").Value()
    End Sub

    Private Function SomeThingSelected() As Boolean


        If ddPayments.SelectedItem.Value <> 0 Then
            Return True
            Exit Function
        ElseIf ddShipments.SelectedItem.Value <> 0 Then

            Return True
            Exit Function
        End If

        Return False


    End Function

    Private Sub cmdGetOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetOrders.Click
        If SomeThingSelected() Then
            Dim sqString As String = "&PaymentStatus=" & ddPayments.SelectedItem.Value & "&ShipStaus=" & ddShipments.SelectedItem.Value
            If ddDateRange.SelectedItem.Value = 5 Then
                If IsDate(txtFrom.Text) And IsDate(txtTo.Text) Then
                    If CDate(txtFrom.Text) <= CDate(txtTo.Text) Then
                        Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/Orderresults.aspx" & "?DateType=" & ddDateRange.SelectedItem.Value & "&From=" & txtFrom.Text & "&To=" & txtTo.Text & sqString)
                    Else
                        'display msg
                        Message.Visible = True
                        Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "From>To")

                    End If
                Else
                    Message.Visible = True
                    Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "InvalidDate")
                End If
            Else
                If ddDateRange.SelectedItem.Value <> -1 Then
                    Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/Orderresults.aspx" & "?DateType=" & ddDateRange.SelectedItem.Value & sqString)
                Else
                    Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "NoCriteria")
                    Message.Visible = True
                End If
            End If
        Else
            'show msg
            Message.Text = m_objMessages.GetXMLMessage("StoreReports.aspx", , "NoCriteria")
            Message.Visible = True
        End If
    End Sub

    Private Sub cmdLocateOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLocateOrder.Click
        If txtFree.Text = "" Then
            Message.Visible = True
            Message.Text = "Please Enter Valid Criteria"
        Else
            Dim objSearch As New sfSearchContainer()
            If txtFree.Text <> "" Then
                If IsNumeric(txtFree.Text) Then
                    objSearch.OrderNumber = txtFree.Text
                Else
                    Dim ar() As String
                    ar = Split(Trim(txtFree.Text), " ")
                    If ar.Length > 1 Then
                        objSearch.FirstName = ar(0)
                        objSearch.LastName = ar(1)
                    Else
                        objSearch.FirstName = txtFree.Text
                        objSearch.LastName = txtFree.Text
                    End If

                End If

            End If


            Session("Search") = objSearch
            Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/Orderresults.aspx")
        End If
    End Sub

End Class
