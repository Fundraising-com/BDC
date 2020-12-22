Imports System.Xml
Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.UITools
Imports StoreFront.BusinessRule.Processors
Partial Class CSRPayment
    Inherits CSRWebControl

#Region "Class Events"
    Event RecalculateOrder()

#End Region
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tdSecurityCode As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdIssueNum As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents txtCardType As SelectValControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then
            SetPaymentMethods()
        End If
    End Sub

    Public Sub SetPaymentMethods()
        GetOrder()
        Dim bAdd As Boolean = False
        Dim ar As New ArrayList
        Dim i As Integer
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim oNode As XmlNode
        Dim oSelNode As XmlNode
        'Dim oPrevNode As XmlNode
        Dim j As Long = 0
        Dim Item_Selected As Long = 0
        Dim strSelection As String
        Dim bSwitchSolo As Boolean = False
        Dim bCreditCard As Boolean = False
        If IsNothing(csrorder.PaymentMethod) Then
            strSelection = ""
        Else
            strSelection = csrorder.PaymentMethod
        End If
        dt.Columns.Add(New DataColumn("PayName", GetType(String)))
        dt.Columns.Add(New DataColumn("PayID", GetType(String)))
        oNode = StoreFrontConfiguration.XMLDocument.Item("SiteProducts").Item("PaymentMethods")


        For i = 0 To oNode.ChildNodes.Count - 1
            oSelNode = oNode.ChildNodes.Item(i)
            dr = dt.NewRow()
            If oSelNode.Item("Name").InnerText.ToString.ToLower = "switch" Or oSelNode.Item("Name").InnerText.ToString.ToLower = "solo" Then
                bSwitchSolo = True
            End If
            If oSelNode.Item("Type").InnerText.ToString.ToLower = "cod" Then
                If csrorder.ShippableItemCount > 0 Then
                    dr(0) = oSelNode.Item("Type").InnerText.ToString & " (add " & Format(CDec(StoreFrontConfiguration.AdminStore.Item("CODAmount").InnerText.ToString), "C") & " per address)"
                    dr(1) = oSelNode.Item("ID").InnerText.ToString()
                    bAdd = True
                End If
            ElseIf oSelNode.Item("Type").InnerText.ToString.ToLower <> "paypal" Then
                'do not display paypal
                If oSelNode.Item("Type").InnerText.ToString.ToLower = "credit card" Then
                    'Display the card name, not "Credit Card"
                    dr(0) = oSelNode.Item("Name").InnerText.ToString
                    dr(1) = oSelNode.Item("ID").InnerText.ToString()
                    bCreditCard = True
                    bAdd = True
                Else
                    dr(0) = oSelNode.Item("Type").InnerText.ToString
                    dr(1) = oSelNode.Item("ID").InnerText.ToString()

                    bAdd = True
                End If
                
                End If
            If bAdd = True Then
                If (IsNothing(strSelection) = False) Then
                    If (dr(1).ToString.ToLower = strSelection.ToLower) Then
                        Item_Selected = j
                    End If
                End If
                If bCreditCard = True Then
                    dt.Rows.InsertAt(dr, 0)
                Else
                    dt.Rows.Add(dr)
                End If

                j = j + 1
                bAdd = False
            End If

        Next

        csrorder.PaymentMethod = dt.Rows(Item_Selected).Item("PayID")
        PaymentTypes.DataSource = dt
        PaymentTypes.DataTextField = "PayName"
        PaymentTypes.DataValueField = "PayID"
        PaymentTypes.DataBind()
        PaymentTypes.SelectedIndex = Item_Selected
        If bSwitchSolo = True Then
            Me.pnlswitchSoloElements.Visible = True
        Else
            Me.pnlswitchSoloElements.Visible = False
        End If
        SetVisible()

    End Sub
    Private Sub SetVisible()

        Dim Method As String
        Method = PaymentTypes.SelectedItem.Text.ToLower
        tblECheck.Visible = False
        tblPurchaseOrder.Visible = False
        tblCreditCard.Visible = False
        tblCreditCard.Visible = False
        tblPurchaseOrder.Visible = False
        If Method = "echeck" Then
            tblECheck.Visible = True
        ElseIf Method = "po" Then
            Me.tblPurchaseOrder.Visible = True
        ElseIf Method = "phonefax" Then
            ' Check to see if PO is available
            Dim objPayments As New CXMLPaymentMethodAccess
            tblPurchaseOrder.Visible = objPayments.IsPOAvailable
            Me.tblCreditCard.Visible = True
        ElseIf Method.IndexOf("cod") = 0 Then
            'Show nothing (have to have this here so else can just be for credit cards below
        Else
            'Credit Card
            Me.tblCreditCard.Visible = True
        End If

        If StoreFrontConfiguration.AdminStore.Item("CVVIsActive").InnerText() = "1" Then
            Me.txtSecureCode.Visible = True
            Me.lblCVV.Visible = True
        Else
            Me.txtSecureCode.Visible = False
            Me.lblCVV.Visible = False
        End If

    End Sub

    Private Sub PaymentTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentTypes.SelectedIndexChanged
        GetOrder()
        csrorder.PaymentMethod = PaymentTypes.SelectedItem.Value
        RaiseEvent RecalculateOrder()
        SetVisible()
    End Sub
    Private Function CheckFields() As Boolean
        Dim Method As String
        Dim strCreditCardError As String = ""
        Method = PaymentTypes.SelectedItem.Text.ToLower
        If Method = "echeck" Then
            'check echeck fields
            If txtRoutingNumber.Text.Trim = "" Or Me.txtCheckNumber.Text.Trim = "" Or Me.txtBankName.Text.Trim = "" Or Me.txtAccountNumber.Text.Trim = "" Or txtSSN.Text.Trim = "" Then
                MagicAjax.AjaxCallHelper.WriteAlert("Please check number, bank name, routing number, account number, and SSN for e-check.")
                Return False
            Else
                Return True
            End If
        ElseIf Method = "po" Then
            'check po fields
            If Me.txtPONumber.Text.Trim <> "" Then
                Return True
            Else
                MagicAjax.AjaxCallHelper.WriteAlert("Please enter a PO number.")
                Return False
            End If
        ElseIf Method = "phonefax" Then
            'check either credit card or po
            Dim objPayments As New CXMLPaymentMethodAccess
            If objPayments.IsPOAvailable Then
                If CheckCreditCardFields(strCreditCardError) = True Or Me.txtPONumber.Text.Trim <> "" Then
                    Return True
                Else
                    MagicAjax.AjaxCallHelper.WriteAlert("Please enter a PO number or valid credit card information.")
                    Return False
                End If
            Else
                If CheckCreditCardFields(strCreditCardError) = True Then
                    Return True
                Else
                    MagicAjax.AjaxCallHelper.WriteAlert(strCreditCardError)
                    Return False
                End If
            End If

        ElseIf Method.IndexOf("cod") = 0 Then 'COD at beginning of string
            Return True
        Else
            If CheckCreditCardFields(strCreditCardError) = True Then
                If Method = "switch" Or Method = "solo" Then
                    If CheckSwitchSoloFields() = True Then
                        Return True
                    Else
                        MagicAjax.AjaxCallHelper.WriteAlert("Please enter Switch/Solo start month and start year or issue number.")
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                MagicAjax.AjaxCallHelper.WriteAlert(strCreditCardError)
                Return False
            End If
        End If

    End Function
    Private Function CheckCreditCardFields(ByRef strCreditCardError As String) As Boolean
        Dim CreditCardNumber As String = Me.txtCardNumber.Value
        CreditCardNumber = CreditCardNumber.Replace(" ", "")
        CreditCardNumber = CreditCardNumber.Replace("-", "")
        If IsNumeric(CreditCardNumber) = False Or CreditCardNumber.Length < 15 Or CreditCardNumber.Length > 16 Then
            strCreditCardError = "Please enter a valid credit card number."
            Return False
        End If

        Dim objProcessor As New Processors.CProcessor(csrOrder)
        If (PaymentTypes.SelectedItem.Text.ToLower <> "switch") And (PaymentTypes.SelectedItem.Text.ToLower <> "solo") Then
            If objProcessor.CCOnline And StoreFrontConfiguration.AdminStore.Item("CVVIsActive").InnerText() = "1" Then
                If Me.txtSecureCode.Text = "" Or Me.txtSecureCode.Text.Length > 4 Then
                    strCreditCardError = "Please enter a valid security code."
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Private Function CheckSwitchSoloFields() As Boolean
        Dim IssueNumber As String = Me.txtIssueNum.Text
        If IssueNumber <> "" Or (Me.txtStartMonth.SelectedIndex <> 0 And Me.txtStartYear.SelectedIndex <> 0) Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Function CompleteOrder() As Boolean
        GetOrder()
        If CheckFields() = False Then
            Return False
        End If
        Dim objPayment As New COrderPayment
        Dim bCreditCard As Boolean = False
        Dim Method As String
        Method = PaymentTypes.SelectedItem.Text.ToLower

        Try

            If Method = "phonefax" Then
                If txtPONumber.Text <> "" Then
                    objPayment.Type = 1
                    objPayment.PONumber = txtPONumber.Text
                ElseIf tblCreditCard.Visible Then
                    objPayment.Type = 0
                    objPayment.CardType = Method
                    objPayment.CreditCardNumber = txtCardNumber.Value
                    If txtSecureCode.Text <> "" Then
                        objPayment.SecurityCode = txtSecureCode.Text
                    Else
                        objPayment.SecurityCode = ""
                    End If

                    objPayment.ExpireMonth = txtExpMonth.SelectedItem.Value
                    objPayment.ExpireYear = txtExpYear.SelectedItem.Value
                End If
            ElseIf Method = "po" Then
                objPayment.Type = 1
                objPayment.PONumber = txtPONumber.Text
            ElseIf Method = "echeck" Then
                objPayment.Type = 2
                objPayment.CheckNumber = txtCheckNumber.Text
                objPayment.BankName = txtBankName.Text
                objPayment.RoutingNumber = txtRoutingNumber.Text
                objPayment.AccountNumber = txtAccountNumber.Text
                objPayment.SSNumber = txtSSN.Text
            ElseIf Method.IndexOf("cod") = 0 Then
                'COD-Do nothing
            Else
                'Credit card
                bCreditCard = True
                objPayment.Type = 0
                '#update 1930
                objPayment.CardType = Method
                objPayment.CreditCardNumber = txtCardNumber.Value
                objPayment.SecurityCode = txtSecureCode.Text
                objPayment.ExpireMonth = txtExpMonth.SelectedItem.Value
                objPayment.ExpireYear = txtExpYear.SelectedItem.Value
                objPayment.StartMonth = txtStartMonth.SelectedItem.Value
                objPayment.StartYear = txtStartYear.SelectedItem.Value
                objPayment.IssueNumber = txtIssueNum.Text
            End If
            objPayment.FilePath = Server.MapPath(Me.TemplateSourceDirectory)
            CSROrder.OrderPayment = objPayment
            Dim Processor As New CProcessor(csrOrder)
            csrOrder.SaveOrder(CSRCustomer.GetCustomerID(), Processor.SaveCCNumber)
            '#2407
            If (CSROrder.GrandTotal = 0) Or (CSROrder.TotalBilledAmt <= 0) Then
                Return True
            End If

            If (Processor.CCOnline And bCreditCard = True) Or (Processor.ECheckOnline And Method.ToLower = "echeck") Then
                Try
                    Processor.CallProcessor(CSRCustomer.GetSessionID(), Request.ServerVariables("REMOTE_ADDR"))
                    If Processor.SupportsPayerAuthentication Then Exit Function
                Catch objErr As Exception
                    If Not TypeOf objErr Is Threading.ThreadAbortException Then
                        csrOrder.DeleteOrder(m_objcustomer.GetCustomerID())
                        If Processor.Message = "" Then
                            MagicAjax.AjaxCallHelper.WriteAlert(objErr.Message)
                        Else
                            MagicAjax.AjaxCallHelper.WriteAlert(Processor.Message)
                        End If

                    End If
                    Return False
                End Try
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            MagicAjax.AjaxCallHelper.WriteAlert(ex.Message)
            Return False
        End Try
    End Function
End Class
