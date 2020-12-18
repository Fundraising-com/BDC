Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.Management
Imports System.Xml
Imports System.IO
Imports System.Xml.Schema
Imports System.Xml.Serialization
Imports System.Web.Services
Imports RMSBusinessRule
Imports StoreFront.Integration


<System.Web.Services.WebService(Namespace:="http://StoreFront.StoreFront/RMSConfirmWS")> _
Public Class RMSConfirmWS
    Inherits System.Web.Services.WebService

#Region "Members"
    Private MsgList As New ArrayList
    Dim ManageProduct As RMSProductManager
    Private config As RMSConfiguration
#End Region

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

    '##SUMMARY - The function accepts RMS product data and then stores it 
    'in SF database
#Region "<WebMethod()> Public Function ImportRMSProducts(ByVal xmlStr As String) As String"
    <WebMethod()> Public Function ImportRMSProducts(ByVal xmlStr As String) As String

        'begin: GJV - issue #1164 - log the input xml; this was mistakenly removed in revision #16437
        LogInputStream(xmlStr, "Import")
        'end: GJV - issue #1164

        Dim declaration As XmlDeclaration
        Dim doc As New XmlDocument
        doc.LoadXml(xmlStr)
        doc.RemoveChild(doc.FirstChild)
        'RMS description fix. Swapping MatrixDescription and Description
        For Each node As XmlNode In doc.SelectNodes("Products/ProductItem")
            If Not node.SelectSingleNode("MatrixDescription") Is Nothing AndAlso Not node.SelectSingleNode("Description") Is Nothing Then
                Dim de As String = node.SelectSingleNode("Description").InnerText
                node.SelectSingleNode("Description").InnerText = node.SelectSingleNode("MatrixDescription").InnerText
                node.SelectSingleNode("MatrixDescription").InnerText = de
            End If
        Next

        doc.InnerXml = "<Request><Document Transaction=""ImportProduct"">" & doc.InnerXml & _
            "</Document></Request>"

        declaration = doc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
        doc.InsertBefore(declaration, doc.DocumentElement)
        Dim intor As New Integrator(doc.OuterXml)
        Dim rDoc As XmlDocument = intor.GetResponse()

        If CType(rDoc.SelectSingleNode("Response/Document"), XmlElement).GetAttribute("Status").ToUpper() = "OK" Then
            'build response
            rDoc = New XmlDocument
            declaration = rDoc.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
            rDoc.AppendChild(declaration)
            rDoc.AppendChild(rDoc.CreateElement("XMLResponse"))
            Dim node As XmlNode = rDoc.SelectSingleNode("XMLResponse")
            doc.LoadXml(xmlStr)
            For Each n As XmlNode In doc.SelectNodes("Products/ProductItem")
                Dim productElement As XmlElement = rDoc.CreateElement("Product")
                productElement.SetAttribute("Code", n.SelectSingleNode("ItemCode").InnerText)
                Dim errorElement As XmlElement = rDoc.CreateElement("Error")
                errorElement.SetAttribute("Code", "0")
                errorElement.InnerText = "Product was successfully imported"
                productElement.AppendChild(errorElement)
                node.AppendChild(productElement)
            Next
            Return rDoc.OuterXml
        Else
            Throw New Exception(rDoc.SelectSingleNode("Response/Document/ErrorDetails").InnerText)
        End If

    End Function
#End Region

    '##SUMMARY - This function accepts product codes that need to be deleted from the database
#Region "<WebMethod()> Public Function DeleteProducts(ByVal xmlStr As String) As String"
    <WebMethod(Description:="This method would accept product codes of the products that need to be deleted from the database and SKU codes if the inventory on certain items needs to be reset")> _
    Public Function DeleteProducts(ByVal xmlStr As String) As String
        Dim objResponseDoc As New XmlDocument
        Dim objXmlDoc As New XmlDocument
        Dim ms As New MemoryStream
        LogInputStream(xmlStr, "Delete")
        ManageProduct = New RMSProductManager
        Try
            MsgList = New ArrayList
            objXmlDoc.LoadXml(xmlStr)
            objXmlDoc.Save(ms)
            ValidateXML(ms, RMSConfiguration.DeleteSchemaPath)
            If MsgList.Count <> 0 Then
                ManageProduct.GenerateProductCodes("NONE")
                ManageProduct.GenerateErrorCodes(ErrorCode.XMLFileInvalid, "DeleteProducts: XML file could not be validated against schema")
                ManageProduct.ResponseDoc.WriteEndElement()
            Else
                ManageProduct.ParseProductsToBeDeleted(ms)
            End If
        Catch ex As Exception
            ManageProduct.GenerateProductCodes("NONE")
            ManageProduct.GenerateErrorCodes(ErrorCode.XMLFileInvalid, ex.Message & ex.StackTrace)
        End Try
        ManageProduct.ResponseDoc.WriteEndElement() 'end element for XMLResponse
        ManageProduct.ResponseDoc.WriteEndDocument()
        ManageProduct.ResponseDoc.Close()
        objResponseDoc.LoadXml(System.Text.Encoding.UTF8.GetString(ManageProduct.ResponseStream.GetBuffer()))
        Return objResponseDoc.InnerXml.ToString()
    End Function
#End Region

#Region "Private Sub ValidateXML(ByVal ms As MemoryStream, ByVal schemaUrl As String)"
    Private Sub ValidateXML(ByVal ms As MemoryStream, ByVal schemaUrl As String)
        ms.Position = 0
        Dim nr As New XmlTextReader(ms)
        'Dim vr As New XmlValidatingReader(nr)
        'Tee 10/2/2007 transition from 1.1 to 2.0
        Dim settings As New XmlReaderSettings
        settings.ValidationType = ValidationType.Schema
        settings.Schemas.Add(Nothing, schemaUrl)
        AddHandler settings.ValidationEventHandler, AddressOf ValidationHandler
        Dim reader As XmlReader = XmlReader.Create(nr, settings)
        Try
            While reader.Read()
                Dim enc As String = reader.Name
            End While
        Catch ex As Exception
            Throw ex
        End Try
        'Tee 10/2/2007 commented out, transition from 1.1 to 2.0
        'vr.ValidationType = ValidationType.Schema
        'vr.XmlResolver = Nothing
        'vr.Schemas.Add(Nothing, schemaUrl)

        'AddHandler vr.ValidationEventHandler, AddressOf ValidationHandler
        'Try
        '    While vr.Read()
        '        Dim enc As String = vr.Encoding.EncodingName
        '    End While
        'Catch ex As Exception
        '    Throw ex
        'End Try
        'end Tee
    End Sub
#End Region

#Region "Shared Sub ValidationHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)"
    Private Sub ValidationHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
        MsgList.Add(args.Message)
    End Sub
#End Region

#Region "Public Sub LogInputStream(ByVal xmlStr As String)"
    Public Sub LogInputStream(ByVal xmlStr As String, ByVal action As String)
        If RMSConfiguration.Log = True Then
            Dim logger As New ManageLogs(action)
            logger.Log(xmlStr)
        End If
    End Sub
#End Region
End Class
