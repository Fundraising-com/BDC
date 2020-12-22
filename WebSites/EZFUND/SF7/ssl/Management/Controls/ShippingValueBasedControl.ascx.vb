Imports StoreFront.BusinessRule.management
Partial  Class ShippingValueBasedControl
    Inherits System.Web.UI.UserControl

    Private objError As Label
    Private objShippingManagement As New CShippingManagement()
    Private objMessage As Label
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
        'Put user code to initialize the page here
        objMessage = CType(Me.Parent.FindControl("Message"), Label)
        objMessage.Text = ""
        objMessage.Visible = False
        ErrorMessage.Visible = False
        If (IsPostBack = True) Then
        Else
            Dim objDS As DataSet = objShippingManagement.getValueBasedShippingDS
            ValueShipping.DataSource = objDS.Tables(0).DefaultView
            ValueShipping.DataBind()
        End If
    End Sub


#Region "Sub deleteRow(ByVal sender As Object, ByVal e As EventArgs)"
    '-----------------------------------------------------------
    ' Sub deleteRow
    ' Parameters: Object, EventArgs
    ' Return: Nothing
    ' Description:
    '   Raise the deleteRow Event passing the Given value for ButtonID
    '-----------------------------------------------------------
    '-----------------------------------------------------------
    Public Sub deleteRow(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable()
        dt = getDTFromPage()
        dt.Rows(CInt(sender.CommandName)).Delete()
        NewMaxTotal.Text = ""
        NewCharge.Text = ""
        ValueShipping.DataSource = dt.DefaultView
        ValueShipping.DataBind()
    End Sub


#End Region



    Public Function getDTFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim x As Integer
        Dim sCharge As String
        Dim sMaxTotal As String
        Dim sMinTotal As String
        dt.Columns.Add(New DataColumn("MinTotal", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("MaxTotal", System.Type.GetType("System.Decimal")))
        dt.Columns.Add(New DataColumn("Amount", System.Type.GetType("System.Decimal")))

        For x = 0 To ValueShipping.Items.Count - 1
            If x = 0 Then
                sMinTotal = 0
            Else
                sMinTotal = CType(ValueShipping.Items(x - 1).FindControl("MaxTotal"), TextBox).Text
            End If
            sMaxTotal = CType(ValueShipping.Items(x).FindControl("MaxTotal"), TextBox).Text
            sCharge = CType(ValueShipping.Items(x).FindControl("Charge"), TextBox).Text
            dr = dt.NewRow()
            dr("MinTotal") = CDec("0" & sMinTotal)
            dr("MaxTotal") = CDec("0" & sMaxTotal)
            dr("Amount") = CDec("0" & sCharge)
            dt.Rows.Add(dr)
        Next
        dt.DefaultView.Sort = "MaxTotal"
        Return dt
    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        If NewMaxTotal.Text <> "" And NewCharge.Text <> "" Then
            Dim dt As New DataTable()
            Dim dr As DataRow
            Dim x As Integer
            Dim pos As Integer
            Dim sNewMaxTotal As String
            Dim sNewMinTotal As String
            Dim sMaxTotal As String
            Dim sNewCharge As String

            Dim bError As Boolean
            bError = False

            dt = getDTFromPage()

            sNewMaxTotal = NewMaxTotal.Text
            sNewCharge = NewCharge.Text
            pos = ValueShipping.Items.Count
            sNewMinTotal = "0"
            objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
            objError.Visible = False
            For x = 0 To ValueShipping.Items.Count - 1
                sMaxTotal = CType(ValueShipping.Items(x).FindControl("MaxTotal"), TextBox).Text
                If (CDec(sNewMaxTotal) = CDec(sMaxTotal)) Then

                    objError.Text = "Row already exists."
                    objError.Visible = True
                    Exit For
                End If
                If (CDec(sNewMaxTotal) < CDec(sMaxTotal)) Then
                    pos = x
                    If x = 0 Then
                        sNewMinTotal = 0
                    Else
                        sNewMinTotal = CType(ValueShipping.Items(x - 1).FindControl("MaxTotal"), TextBox).Text
                    End If
                    Exit For
                End If
            Next
            If objError.Visible = False Then
                dr = dt.NewRow
                dr("MinTotal") = CDec(sNewMinTotal)
                dr("MaxTotal") = CDec(sNewMaxTotal)
                dr("Amount") = CDec(sNewCharge)

                dt.Rows.InsertAt(dr, pos)
            End If
            dt.DefaultView.Sort = "MaxTotal"
            ValueShipping.DataSource = dt.DefaultView
            ValueShipping.DataBind()
            NewMaxTotal.Text = ""
            NewCharge.Text = ""
        Else
            ErrorMessage.Text = "Please enter an order amount and a shipping charge"
            ErrorMessage.Visible = True
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        objShippingManagement.updateValueBasedShipping(getDTFromPage)
        NewMaxTotal.Text = ""
        NewCharge.Text = ""
        objMessage.Text = "Your changes have been saved"
        objMessage.Visible = True
    End Sub
End Class
