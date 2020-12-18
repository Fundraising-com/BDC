Imports System.Web.Services
Imports StoreFront.Integration
Imports System.Xml
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Imports System.Xml.Xsl


<System.Web.Services.WebService(Namespace:="http://tempuri.org/StoreFront.Integration/WebService")> _
Public Class WebService
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    <WebMethod()> _
    Public Function ImportCustomers(ByVal xml As String) As String

        Try
            ValidateXML(xml, StoreFrontConfiguration.SSLPath & "schemas/ImportCustomer70.xsd")
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(xml)
            Dim xsltURL As String = StoreFrontConfiguration.SSLPath & "schemas/MapToImportCustomer.xslt"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml(Transform(xmlDoc, xsltURL))
            transformedDoc.RemoveChild(transformedDoc.FirstChild)
            transformedDoc.InnerXml = "<Request><Document Transaction=""ImportCustomer"">" & transformedDoc.InnerXml & _
                "</Document></Request>"
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    'begin: GJV - issue #1145 - remove the ImportOrders web method
    '<WebMethod()> _
    'Public Function ImportOrders(ByVal xml As String) As String

    '    Try
    '        ValidateXML(xml, StoreFrontConfiguration.SSLPath & "schemas/ImportOrder70.xsd")
    '        Dim xmlDoc As New XmlDocument
    '        xmlDoc.LoadXml(xml)
    '        Dim xsltURL As String = StoreFrontConfiguration.SSLPath & "schemas/MapToImportOrder.xslt"

    '        Dim declaration As XmlDeclaration
    '        Dim transformedDoc As New XmlDocument
    '        transformedDoc.LoadXml(Transform(xmlDoc, xsltURL))
    '        transformedDoc.RemoveChild(transformedDoc.FirstChild)
    '        transformedDoc.InnerXml = "<Request><Document Transaction=""ImportOrder"">" & transformedDoc.InnerXml & _
    '            "</Document></Request>"
    '        declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
    '        transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

    '        Dim intor As New Integrator(transformedDoc.OuterXml)

    '        Return intor.GetResponse().OuterXml

    '    Catch ex As Exception
    '        Return BuildExceptionResponse(ex)
    '    End Try

    'End Function
    'end: GJV - issue #1145

    <WebMethod()> _
    Public Function ImportOrderStatus(ByVal xml As String) As String

        Try
            ValidateXML(xml, StoreFrontConfiguration.SSLPath & "schemas/ImportOrderStatus70.xsd")
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(xml)
            Dim xsltURL As String = StoreFrontConfiguration.SSLPath & "schemas/MapToImportOrderStatus.xslt"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml(Transform(xmlDoc, xsltURL))

            For Each node1 As XmlNode In xmlDoc.GetElementsByTagName("SalesOrder")

                If Not node1.Item("TrackingNumbers") Is Nothing Then
                    Dim orderNumber As String = node1.Item("SalesOrderNumber").InnerText
                    Dim trackNumber As String = String.Empty
                    Dim trackMessage As String = String.Empty
                    Dim first As Boolean = True
                    For Each trackElement As XmlElement In node1.Item("TrackingNumbers").GetElementsByTagName("TrackingNumber")
                        If first Then
                            trackNumber = trackElement.InnerText()
                            first = False
                        Else
                            trackMessage &= trackElement.InnerText() & ","
                        End If
                    Next
                    If trackMessage.Length > 0 Then
                        trackMessage = trackMessage.Substring(0, trackMessage.Length - 1)
                    End If
                    If trackNumber <> String.Empty Then
                        For Each xNode As XmlNode In transformedDoc.SelectSingleNode("ImportOrderStatus")
                            If xNode.Item("OrderNumber").InnerText.ToLower() = orderNumber.ToLower() Then
                                Dim etn As XmlElement = transformedDoc.CreateElement("TrackingNumber")
                                etn.InnerText = trackNumber
                                Dim etm As XmlElement = transformedDoc.CreateElement("TrackingMessage")
                                etm.InnerText = trackMessage
                                If xNode.Item("ShippingAddress") Is Nothing Then
                                    xNode.AppendChild(etn)
                                    xNode.AppendChild(etm)
                                Else
                                    xNode.InsertBefore(etn, xNode.Item("ShippingAddress"))
                                    xNode.InsertBefore(etm, xNode.Item("ShippingAddress"))
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            transformedDoc.RemoveChild(transformedDoc.FirstChild)
            transformedDoc.InnerXml = "<Request><Document Transaction=""ImportOrderStatus"">" & transformedDoc.InnerXml & _
                "</Document></Request>"
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
    Public Function ImportProducts(ByVal xml As String) As String

        Try
            'SKC Change - Changed to use the 7.0 schema file for validation
            ValidateXML(xml, StoreFrontConfiguration.SSLPath & "schemas/ImportProduct70.xsd")
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(xml)

            Dim declaration As XmlDeclaration
            xmlDoc.RemoveChild(xmlDoc.FirstChild)
            'SKC Change - Changed the Transaction to be ImportProduct70 so that the right schema would be used later on.
            xmlDoc.InnerXml = "<Request><Document Transaction=""ImportProduct70"">" & xmlDoc.InnerXml & _
                "</Document></Request>"
            declaration = xmldoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            xmldoc.InsertBefore(declaration, xmldoc.DocumentElement)

            Dim intor As New Integrator(xmldoc.OuterXml)

            Dim results As XmlDocument = intor.GetResponse()
            Return results.OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
    Public Function ImportInventory(ByVal xml As String) As String

        Try
            ValidateXML(xml, StoreFrontConfiguration.SSLPath & "schemas/ImportInventory70.xsd")
            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(xml)
            Dim xsltURL As String = StoreFrontConfiguration.SSLPath & "schemas/MapToImportInventory.xslt"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml(Transform(xmlDoc, xsltURL))
            transformedDoc.RemoveChild(transformedDoc.FirstChild)
            transformedDoc.InnerXml = "<Request><Document Transaction=""ImportInventory"">" & transformedDoc.InnerXml & _
                "</Document></Request>"
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
    Public Function RequestOrdersSinceLastRequest() As String

        Try

            Dim xml As String = "<ExportOrder SchemaVersion=""1.0"">" & _
                    "<Request attribute1="""" attribute2=""""><FilterType>Standard</FilterType>" & _
                    "</Request></ExportOrder>"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml("<Request><Document Transaction=""ExportOrder"">" & xml & _
                "</Document></Request>")
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
Public Function RequestOrderByOrderNumber(ByVal orderNumber As String) As String

        Try

            Dim xml As String = "<ExportOrder SchemaVersion=""1.0"">" & _
                    "<Request attribute1=""" & orderNumber & """ attribute2=""""><FilterType>OrderNumber</FilterType>" & _
                    "</Request></ExportOrder>"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml("<Request><Document Transaction=""ExportOrder"">" & xml & _
                "</Document></Request>")
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
Public Function RequestOrdersByDateRange(ByVal fromDate As String, ByVal toDate As String) As String

        Try

            Dim xml As String = "<ExportOrder SchemaVersion=""1.0"">" & _
                    "<Request attribute1=""" & fromDate & """ attribute2=""" & toDate & """><FilterType>DateRange</FilterType>" & _
                    "</Request></ExportOrder>"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml("<Request><Document Transaction=""ExportOrder"">" & xml & _
                "</Document></Request>")
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function

    <WebMethod()> _
Public Function RequestOrdersByCustomer(ByVal customerId As Integer) As String

        Try

            Dim xml As String = "<ExportOrder SchemaVersion=""1.0"">" & _
                    "<Request attribute1=""" & customerId & """ attribute2=""""><FilterType>Customer</FilterType>" & _
                    "</Request></ExportOrder>"

            Dim declaration As XmlDeclaration
            Dim transformedDoc As New XmlDocument
            transformedDoc.LoadXml("<Request><Document Transaction=""ExportOrder"">" & xml & _
                "</Document></Request>")
            declaration = transformedDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            transformedDoc.InsertBefore(declaration, transformedDoc.DocumentElement)

            Dim intor As New Integrator(transformedDoc.OuterXml)

            Return intor.GetResponse().OuterXml

        Catch ex As Exception
            Return BuildExceptionResponse(ex)
        End Try

    End Function


    Private Sub ValidateXML(ByVal strXml As String, ByVal schemaUrl As String)
        'Tee 10/2/2007 commented out, transition from 1.1 to 2.0
        'SKC - Switched back 2/13/08 - Ashish is ok with the 'obsolete' code.
        Dim VReader As New XmlValidatingReader(New XmlTextReader(New IO.StringReader(strXml)))
        VReader.ValidationType = ValidationType.Schema
        Dim SSchema As New Schema.XmlSchema
        VReader.XmlResolver = Nothing
        VReader.Schemas.Add(Nothing, schemaUrl)
        While VReader.Read()
        End While

        'Dim reader As XmlReader
        'Dim settings As New XmlReaderSettings
        'settings.ValidationType = ValidationType.Schema
        'settings.Schemas.Add(Nothing, schemaUrl)
        'reader = XmlReader.Create(New XmlTextReader(New IO.StringReader(strXml)), settings)
        'While reader.Read()
        'End While
        'end Tee
    End Sub
    Private Shared Function BuildExceptionResponse(ByVal ex As Exception) As String

        Dim doc As New XmlDocument
        Dim declaration As XmlDeclaration
        doc.LoadXml("<Response><Document><ErrorDetails><![CDATA[Error:" & ex.Message & " Stack:" & ex.StackTrace & "]]></ErrorDetails></Document></Response>")
        declaration = doc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
        doc.InsertBefore(declaration, doc.DocumentElement)
        Return doc.OuterXml

    End Function
    Private Function Transform(ByVal xmldoc As XmlDocument, ByVal transformationUrl As String) As String
        'Tee 10/2/2007 commented out, 1.1 to 2.0 transition
        'Dim sResult As New System.IO.StringWriter
        'Dim oXSLT As New XslTransform
        'oXSLT.Load(transformationUrl)
        'oXSLT.Transform(xmldoc, Nothing, sResult, Nothing)
        'Return sResult.GetStringBuilder().ToString()
        Dim sResult As New System.IO.StringWriter
        Dim myXmlWriter As XmlWriter = XmlWriter.Create(sResult.GetStringBuilder)
        Dim oXSLT As New XslCompiledTransform
        oXSLT.Load(transformationUrl)
        oXSLT.Transform(xmldoc, myXmlWriter)
        Return sResult.GetStringBuilder().ToString()
        'end Tee
    End Function


End Class
