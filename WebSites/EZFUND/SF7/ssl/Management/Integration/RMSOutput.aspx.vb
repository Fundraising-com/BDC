Imports System
Imports System.Text
Imports System.Data.OleDb
Imports System.Data
Imports System.Xml
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.XMLBuilder
Imports StoreFront.BusinessRule.WebRequest
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException
Imports System.IO
Imports RMSBusinessRule

Partial Class RMSOutput
    Inherits CWebPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        mOverrideLogin = True
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If Request.HttpMethod = "GET" Then
                CreateRMSOutput()
            End If
        Catch ex As Exception
            Session("DetailError") = "RMSOutput Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

#Region "Private Sub CreateRMSOutput() "
    '#Summary - This would create the XML output for the RMS EDI Client
    Private Sub CreateRMSOutput()
        Dim objResponseDoc As New XmlDocument
        Dim objOrder As New Order
        Dim responseStream As New MemoryStream
        Dim objXMLBuilder As New XmlTextWriter(responseStream, Nothing)
        objXMLBuilder.Formatting = Formatting.Indented
        objXMLBuilder.Indentation = 3
        objXMLBuilder.WriteStartDocument()
        Dim dsOrder As DataSet = objOrder.GetRMSOrder()
        Dim drOrderRow As DataRow

        objXMLBuilder.WriteStartElement("WebOrders")
        objXMLBuilder.WriteAttributeString(" NumberOfOrders ", CStr(dsOrder.Tables(0).Rows.Count()))
        For Each drOrderRow In dsOrder.Tables(0).Rows

            'begin: GJV - issue #1176 - update the rms status to 1
            objOrder.UpdateRMSStatus(drOrderRow.Item("OrderNumber"), 1)
            'end: GJV - issue #1176

            Dim OrderID As Long = drOrderRow.Item("uid")
            Dim CustomerID As Long = drOrderRow.Item("CustomerID")
            Dim OrderNumber As Long = drOrderRow.Item("OrderNumber")
            Dim ord As New COrder(OrderNumber)

            objXMLBuilder.WriteStartElement("WebOrder")
            objXMLBuilder.WriteStartElement("Order")
            objXMLBuilder.WriteAttributeString("id", CStr(OrderNumber))
            objXMLBuilder.WriteAttributeString("currency", CStr(StoreFrontConfiguration.ISOCurrency))

            'Get the Shipping Information
            Dim ordShippingAddress As COrderAddress = ord.OrderAddresses(0)
            objXMLBuilder.WriteStartElement("AddressInfo")
            objXMLBuilder.WriteAttributeString("type", "ship")
            objXMLBuilder.WriteStartElement("Name")

            WriteElement(objXMLBuilder, "First", MakeXmlSafe(CStr(ordShippingAddress.Address.FirstName)))
            WriteElement(objXMLBuilder, "Last", MakeXmlSafe(CStr(ordShippingAddress.Address.LastName)))
            WriteElement(objXMLBuilder, "Full", MakeXmlSafe(CStr(ordShippingAddress.Address.FirstName)) & " " & MakeXmlSafe(CStr(ordShippingAddress.Address.LastName)))
            objXMLBuilder.WriteEndElement()

            WriteElement(objXMLBuilder, "Address1", MakeXmlSafe(CStr(ordShippingAddress.Address.Address1)))
            WriteElement(objXMLBuilder, "Address2", MakeXmlSafe(CStr(ordShippingAddress.Address.Address2)))
            WriteElement(objXMLBuilder, "City", MakeXmlSafe(CStr(ordShippingAddress.Address.City)))
            WriteElement(objXMLBuilder, "State", MakeXmlSafe(CStr(ordShippingAddress.Address.State)))
            WriteElement(objXMLBuilder, "Country", MakeXmlSafe(CStr(ordShippingAddress.Address.Country)))
            WriteElement(objXMLBuilder, "Zip", MakeXmlSafe(CStr(ordShippingAddress.Address.Zip)))
            WriteElement(objXMLBuilder, "Phone", MakeXmlSafe(CStr(ordShippingAddress.Address.Phone)))
            WriteElement(objXMLBuilder, "Email", MakeXmlSafe(CStr(ordShippingAddress.Address.EMail)))

            objXMLBuilder.WriteStartElement("Custom")
            objXMLBuilder.WriteAttributeString("name", "Company")
            objXMLBuilder.WriteString(ordShippingAddress.Address.Company)
            objXMLBuilder.WriteEndElement()
            objXMLBuilder.WriteEndElement()

            'Get Billing Address
            objXMLBuilder.WriteStartElement("AddressInfo")
            objXMLBuilder.WriteAttributeString("type", "bill")
            objXMLBuilder.WriteStartElement("Name")

            WriteElement(objXMLBuilder, "First", MakeXmlSafe(CStr(ord.BillAddress.FirstName)))
            WriteElement(objXMLBuilder, "Last", MakeXmlSafe(CStr(ord.BillAddress.LastName)))
            WriteElement(objXMLBuilder, "Full", MakeXmlSafe(CStr(ord.BillAddress.FirstName)) & " " & MakeXmlSafe(CStr(ord.BillAddress.LastName)))
            objXMLBuilder.WriteEndElement()

            WriteElement(objXMLBuilder, "Address1", MakeXmlSafe(CStr(ord.BillAddress.Address1)))
            WriteElement(objXMLBuilder, "Address2", MakeXmlSafe(CStr(ord.BillAddress.Address2)))
            WriteElement(objXMLBuilder, "City", MakeXmlSafe(CStr(ord.BillAddress.City)))
            WriteElement(objXMLBuilder, "State", MakeXmlSafe(CStr(ord.BillAddress.State)))
            WriteElement(objXMLBuilder, "Country", MakeXmlSafe(CStr(ord.BillAddress.Country)))
            WriteElement(objXMLBuilder, "Zip", MakeXmlSafe(CStr(ord.BillAddress.Zip)))
            WriteElement(objXMLBuilder, "Phone", MakeXmlSafe(CStr(ord.BillAddress.Phone)))
            WriteElement(objXMLBuilder, "Email", MakeXmlSafe(CStr(ord.BillAddress.EMail)))

            objXMLBuilder.WriteStartElement("Custom")
            objXMLBuilder.WriteAttributeString("name", "Company")
            objXMLBuilder.WriteString(ord.BillAddress.Company)
            objXMLBuilder.WriteEndElement()
            objXMLBuilder.WriteEndElement()

            objXMLBuilder.WriteStartElement("Shipping")
            If MakeXmlSafe(CStr(ordShippingAddress.Address.ShipCarrierCode)) = "NONE" Then
                objXMLBuilder.WriteString(MakeXmlSafe(CStr(ordShippingAddress.Address.ShipCarrierCode)))
            Else
                objXMLBuilder.WriteString(MakeXmlSafe(CStr(ordShippingAddress.Address.ShipCarrierCode)) & " - " & MakeXmlSafe(CStr(ordShippingAddress.Address.ShipMethod)))
            End If
            objXMLBuilder.WriteEndElement()

            'Get CC Info
            If CInt(ord.PaymentMethod) >= 7 And CInt(ord.PaymentMethod) <= 13 Then
                'TODO: Provide some mechanism to allow the RSA Key to be sent.
                Dim CCNumber As String = String.Empty
                Dim RSAKeyText As String = Nothing
                If StoreFrontConfiguration.ConvertedFrom3DES Then
                    If Not IsNothing(RSAKeyText) And Not (ord.OrderPayment.isDirty) And ord.OrderPayment.CreditCardNumber.Trim.Length > 0 Then
                        Try
                            CCNumber = (New StoreFrontSecurity.StoreFrontRSACrypto).Decrypt(RSAKeyText, ord.OrderPayment.CreditCardNumber)
                        Catch ex As Exception
                            CCNumber = "************" & ord.OrderPayment.LastFourDigits
                        End Try
                    Else
                        CCNumber = "************" & ord.OrderPayment.LastFourDigits
                    End If
                Else
                    CCNumber = GetDecryptedValue(ord.OrderPayment, ord.OrderPayment.CreditCardNumber)
                End If
                Dim CCType As String = ord.OrderPayment.CardType
                Dim CCExpireDate As String = ord.OrderPayment.ExpireMonth & "/" & ord.OrderPayment.ExpireYear
                objXMLBuilder.WriteStartElement("CreditCard")
                objXMLBuilder.WriteAttributeString("type", CCType)
                objXMLBuilder.WriteAttributeString("expiration", CCExpireDate)
                objXMLBuilder.WriteString(CCNumber)
                objXMLBuilder.WriteEndElement()
            End If
            WriteElement(objXMLBuilder, "Comments", MakeXmlSafe(CStr(ordShippingAddress.Address.Instructions)))

            'Get OrderItems
            Dim dsOrderItem As DataSet = objOrder.GetRMSOrderItem(OrderID)
            Dim drOrderItemRow As DataRow
            Dim count As Integer = 0
            For Each drOrderItemRow In dsOrderItem.Tables(0).Rows
                Dim prodID As String = drOrderItemRow.Item("ProductID")
                Dim prodCode As String = MakeXmlSafe(drOrderItemRow.Item("ProductCode"))
                Dim prodDescription As String
                'change
                Dim orderItem As New COrderItem(drOrderItemRow)
                orderItem.LoadOrderAttributes()
                orderItem.LoadOrderHistoryInventory()
                Dim itemSKU As String
                Try
                    itemSKU = orderItem.GetSku()
                Catch ex As Exception
                    itemSKU = String.Empty
                End Try
                'Tee 4/8/2008 deleted prod issue fix
                Dim productManage As New BusinessRule.Management.CProductManagement()
                If productManage.ProductUID(prodCode) = prodID Then
                    productManage = New BusinessRule.Management.CProductManagement(prodID)
                    If productManage.ShortDescription <> "" Then
                        prodDescription = MakeXmlSafe(productManage.ShortDescription)
                    Else
                        prodDescription = "No description available"
                    End If
                Else
                    prodDescription = "No description available"
                End If
                'end Tee
                objXMLBuilder.WriteStartElement("Item")
                objXMLBuilder.WriteAttributeString("num", CStr(count))

                If Not IsNothing(itemSKU) AndAlso itemSKU <> String.Empty Then
                    WriteElement(objXMLBuilder, "Code", MakeXmlSafe(itemSKU))
                Else
                    WriteElement(objXMLBuilder, "Code", MakeXmlSafe(prodCode))
                End If

                WriteElement(objXMLBuilder, "Quantity", CStr(drOrderItemRow.Item("Quantity")))
                WriteElement(objXMLBuilder, "Unit-Price", CStr(drOrderItemRow.Item("ItemPrice")))
                WriteElement(objXMLBuilder, "Description", MakeXmlSafe(prodDescription))

                'Begin Newestech Code: 8/10/2004
                '##SUMMARY - Add the customer defined attributes to the order item info
                Dim itemAtt As New CAttribute
                Dim attValue As String
                For Each itemAtt In orderItem.Attributes
                    If itemAtt.AttributeType = tAttributeType.Custom Then
                        attValue = itemAtt.AttributeDetails(0).Customor_Custom_Description
                    Else
                        attValue = itemAtt.AttributeDetails(0).Name
                    End If
                    'begin: GJV - issue # 758 - commenting, items that are attributes cannot be written to an order in RMS
                    'objXMLBuilder.WriteStartElement("Option")
                    'objXMLBuilder.WriteAttributeString("name", MakeXmlSafe(itemAtt.Name))
                    'objXMLBuilder.WriteAttributeString("type", itemAtt.AttributeType.ToString())
                    'objXMLBuilder.WriteString(MakeXmlSafe(attValue))
                    'objXMLBuilder.WriteEndElement()
                    'end: GJV - issue #758
                Next
                objXMLBuilder.WriteEndElement()
                count = count + 1
            Next
            objXMLBuilder.WriteStartElement("Total")

            objXMLBuilder.WriteStartElement("Line")
            objXMLBuilder.WriteAttributeString("name", "Subtotal")
            objXMLBuilder.WriteAttributeString("type", "Subtotal")
            objXMLBuilder.WriteString(CStr(ord.SubTotal))
            objXMLBuilder.WriteEndElement()

            objXMLBuilder.WriteStartElement("Line")
            objXMLBuilder.WriteAttributeString("name", "Shipping")
            objXMLBuilder.WriteAttributeString("type", "Shipping")
            objXMLBuilder.WriteString(CStr(ord.ShippingTotal + ord.HandlingTotal))
            objXMLBuilder.WriteEndElement()

            objXMLBuilder.WriteStartElement("Line")
            objXMLBuilder.WriteAttributeString("name", "Tax")
            objXMLBuilder.WriteAttributeString("type", "Tax")
            objXMLBuilder.WriteString(CStr(ord.StateTax + ord.LocalTax + ord.CountryTax))
            objXMLBuilder.WriteEndElement()

            objXMLBuilder.WriteStartElement("Line")
            objXMLBuilder.WriteAttributeString("name", "Total")
            objXMLBuilder.WriteAttributeString("type", "Total")
            objXMLBuilder.WriteString(CStr(ord.TotalBilledAmt))
            objXMLBuilder.WriteEndElement()
            objXMLBuilder.WriteEndElement()
            objXMLBuilder.WriteEndElement()
            WriteElement(objXMLBuilder, "WebComment", "Order on " & MakeXmlSafe(CStr(ord.OrderDate)) & " by " & MakeXmlSafe(CStr(ord.BillAddress.FirstName)) & " " & MakeXmlSafe(CStr(ord.BillAddress.LastName)))
            objXMLBuilder.WriteEndElement()
        Next
        objXMLBuilder.WriteEndElement()
        objXMLBuilder.WriteEndDocument()
        objXMLBuilder.Flush()
        objXMLBuilder.Close()
        'Load the XML Document
        objResponseDoc.LoadXml(System.Text.Encoding.UTF8.GetString(responseStream.GetBuffer()))
        Response.Write(objResponseDoc.InnerXml.ToString())
    End Sub
#End Region

#Region "Private Sub WriteElement(ByRef objXMLBuilder As XmlTextWriter, ByVal name As String, ByVal value As String)"
    Private Sub WriteElement(ByRef objXMLBuilder As XmlTextWriter, ByVal name As String, ByVal value As String)
        objXMLBuilder.WriteStartElement(name)
        objXMLBuilder.WriteString(value)
        objXMLBuilder.WriteEndElement()
    End Sub
#End Region

    Private Function MakeXmlSafe(ByVal inStr As String) As String
        If inStr Is Nothing Then Return ""
        inStr = inStr.Replace("<", "&lt;")
        inStr = inStr.Replace(">", "&gt;")
        inStr = inStr.Replace("&", "&amp;")
        inStr = inStr.Replace(Chr(34), "&quot;")
        Return inStr
    End Function

#Region "GetDecryptedValue(ByVal mOrderPayment As COrderPayment, ByVal Value As String) As String"
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
            End If
            Return String.Empty
        Catch ex As Exception
            Return Value
        End Try
    End Function
#End Region

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
