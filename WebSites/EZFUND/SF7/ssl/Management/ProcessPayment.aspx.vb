'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management

Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Partial Class ProcessPayment
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents AddressLabel1 As AddressLabel
    Private objOrder As COrder
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
        
        Try
            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            Dim m_objPaymentMethodAccess As CXMLPaymentMethodAccess = StoreFrontConfiguration.PaymentMethodAccess
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Me.ErrorMessage.Visible = False
            'CType(Me.TopSubBanner1.FindControl("CMenuBar1"), CMenubar).IsAdminArea = True
            objOrder = New COrder(CType(Session("objOrder"), COrder).OrderNumber)
            AddressLabel1.AddressSource = objOrder.BillAddress
            lblEmail.Text = objOrder.BillAddress.EMail
            lblOrderID.Text = objOrder.OrderNumber
            lblDate.Text = objOrder.OrderDate
            lblPaymethod.Text = m_objPaymentMethodAccess.GetPaymentMethodName(objOrder.PaymentMethod())
            If Not IsPostBack Then
                Session("CallPage") = Request.UrlReferrer.ToString
            End If
            If (IsNothing(objOrder.OrderPayment) = False) Then
                Me.lblAccountNumber.Text = GetDecryptedValue(objOrder.OrderPayment, objOrder.OrderPayment.AccountNumber)
                Me.lblBankName.Text = objOrder.OrderPayment.BankName
                Me.lblCCNumber.Text = GetCreditCardNumber(objOrder.OrderPayment)
                Me.lblCCType.Text = objOrder.OrderPayment.CardType
                Me.lblExpireDate.Text = objOrder.OrderPayment.ExpireMonth & "/" & objOrder.OrderPayment.ExpireYear
                Me.lblPONumber.Text = objOrder.OrderPayment.PONumber
                Me.lblRoutingNumber.Text = GetDecryptedValue(objOrder.OrderPayment, objOrder.OrderPayment.RoutingNumber)
            End If

            If lblPaymethod.Text = "eCheck" Then
                printEcheck.Visible = True
                tblCreditCard.Visible = False
                tblECheck.Visible = True
                tblPurchaseOrder.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "credit card" Then
                tblCreditCard.Visible = True
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "po" Then
                tblCreditCard.Visible = False
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = True
            End If
            If lblPaymethod.Text.ToLower = "phonefax" Then
                If Me.lblPONumber.Text.Trim <> "" Then
                    tblCreditCard.Visible = False
                    tblPurchaseOrder.Visible = True
                Else
                    tblCreditCard.Visible = True
                    tblPurchaseOrder.Visible = False
                End If
                tblECheck.Visible = False
            End If
            If lblPaymethod.Text.ToLower = "cod" Then
                tblCreditCard.Visible = False
                tblECheck.Visible = False
                tblPurchaseOrder.Visible = False
            End If
        Catch ex As Exception
            Session("DetailError") = "Class ProcessPayment Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub
    Private Function GetDecryptedValue(ByVal mOrderPayment As COrderPayment, ByVal Value As String) As String
        Try
            If Not StoreFrontConfiguration.ConvertedFrom3DES Then
                Dim objKey(7) As Byte
                Dim objIV(7) As Byte
                Dim sReturn As String = String.Empty
                If (Not Value Is Nothing) AndAlso Value.Trim.Length > 0 Then
                    sReturn = Value
                    'sp8 - # 2821
                    If Not lblPaymethod.Text.Equals("eCheck") Then
                        decryptIt(objKey, objIV, sReturn)
                    End If
                    Return sReturn
                End If
                    Return String.Empty
                End If
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Error Decrypting value ." & ex.Message
            Return Value
        End Try


        Try
            If Not Session(FulFillmentControl.ENCRYPTIONKEY) Is Nothing AndAlso _
              Not mOrderPayment.isDirty AndAlso _
              Value.Trim.Length > 0 Then
                Return (New StoreFrontSecurity.StoreFrontRSACrypto).Decrypt(Session(FulFillmentControl.ENCRYPTIONKEY).ToString, Value)
            End If
            Return String.Empty
        Catch ex As Exception
            ErrorMessage.Visible = True
            ErrorMessage.Text = "Error Decrypting value ." & ex.Message
            Return String.Empty
        End Try
    End Function
#Region "Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(),byRef strValue as string)"
    Private Sub decryptIt(ByRef objKey As Byte(), ByRef objIV As Byte(), ByRef strValue As String)
        Dim st() As String
        Dim i As Long, j As Long, x As Long, k As Long, y As Long
        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(Nothing, i)
        Next
        For i = 0 To objIV.GetUpperBound(0)
            objIV.SetValue(Nothing, i)
        Next

        st = Split(strValue)

        For i = 0 To objKey.GetUpperBound(0)
            objKey.SetValue(CByte(CInt(st.GetValue(i))), i)
            j = i
        Next
        j = j + 1
        For i = j To j + objIV.GetUpperBound(0)
            objIV.SetValue(CByte(CInt(st.GetValue(i))), x)
            k = i
            x = x + 1
        Next
        k = k + 1
        Dim arEncrypt(st.GetUpperBound(0) - k) As Byte
        For i = k To st.GetUpperBound(0)
            arEncrypt.SetValue(CByte(CInt(st.GetValue(i))), y)
            y = y + 1
        Next

        Dim obj As New StoreFrontSecurity.CStoreFrontCrypto2(arEncrypt)
        obj.Type = StoreFrontSecurity.CryptoType.Decrypt
        strValue = obj.GetData(objKey, objIV)
        obj = Nothing
    End Sub
#End Region
    Private Sub printEcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles printEcheck.Click
        Response.Redirect("PrintEcheck.aspx")
    End Sub

    Private Sub cmdProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcess.Click
        'todo RE Process Back Order Amount ??
        Dim objManagement As New CManagement
        objOrder = Session("objOrder")
        Dim pendingType As Integer
        If Request.QueryString("Type") <> "" Then
            pendingType = Request.QueryString("Type")
            objManagement.UpdateShipPaymentStatus(objOrder.OrderNumber, pendingType)
            Response.Redirect(Session("CallPage"))
            Exit Sub
        End If
        Me.ErrorMessage.Text = "Error Processing Payment"
        Me.ErrorMessage.Visible = True

    End Sub
    Private Function GetCreditCardNumber(ByVal mOrderPayment As COrderPayment) As String
        Dim sReturn As String
        sReturn = Me.GetDecryptedValue(mOrderPayment, mOrderPayment.CreditCardNumber)
        If sReturn.Length <= 0 Then
            Return "************" & mOrderPayment.LastFourDigits
        Else
            Return sReturn
        End If
    End Function
End Class
