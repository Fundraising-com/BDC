Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

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

Imports StoreFront.BusinessRule.Orders

Partial Class PrintEcheck
    Inherits System.Web.UI.Page

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
            'Put user code to initialize the page here
            Dim objOrder As COrder = Session("objOrder")
            Dim BillName As String
            BillName = objOrder.BillAddress.FirstName & " " & objOrder.BillAddress.MI & " " & objOrder.BillAddress.LastName
            BillName = BillName.Replace("  ", " ")
            name.Text = BillName
            SignedBy.Text = BillName
            Address.Text = objOrder.BillAddress.Address1
            If objOrder.BillAddress.Address2.Length > 0 Then
                Address.Text = Address.Text & "<br>" & objOrder.BillAddress.Address2
            End If
            CityStateZip.Text = objOrder.BillAddress.City & " " & objOrder.BillAddress.State & ", " & objOrder.BillAddress.Zip
            Country.Text = objOrder.BillAddress.Country
            CheckNumber.Text = objOrder.OrderPayment.CheckNumber 'sp8 # 2821 removed call to decryptit
            BankName.Text = objOrder.OrderPayment.BankName
            'sp8 # 2821 removed call to decryptit
            RoutingNumber.Text = "A" & objOrder.OrderPayment.RoutingNumber & "A  " & objOrder.OrderPayment.AccountNumber & "B" & objOrder.OrderPayment.CheckNumber     'to do, fix this field
            Total.Text = Format(objOrder.OrderTotal, "C")
            OrderDate.Text = objOrder.OrderDate
        Catch ex As Exception
            Session("DetailError") = "Class PrintECheck Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
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
    Private Function GetDecryptedValue(ByVal mOrderPayment As COrderPayment, ByVal Value As String) As String
        Try
            If Not StoreFrontConfiguration.ConvertedFrom3DES Then
                Dim objKey(7) As Byte
                Dim objIV(7) As Byte
                Dim sReturn As String = String.Empty
                If (Not Value Is Nothing) AndAlso Value.Trim.Length > 0 Then
                    sReturn = Value
                    decryptIt(objKey, objIV, sReturn)
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
End Class
