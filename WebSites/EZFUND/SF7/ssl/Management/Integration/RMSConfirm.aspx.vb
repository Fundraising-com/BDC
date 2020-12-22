Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports StoreFront.BusinessRule.Management
Imports System.Xml
Imports System.IO
Imports System.Xml.Schema
Imports RMSBusinessRule

Partial Class RMSConfirm
    Inherits CWebPage

#Region "Members"
    Private MsgList As ArrayList
    Private objXmlDoc As XmlDocument
    Private failedProdList As String

    Private objResponseDoc As XmlDocument
    Private responseStream As MemoryStream
    Private rXML As XmlTextWriter
#End Region

#Region "Enumeration"
    Enum ErrorCode
        ProductImported = 0
        TaxTypeDoesNotExist = 1
        IllegalOperation = 2
        DepartmentNameMissing = 3
        AddExisting = 4
        UpdateUnExisting = 5
        XMLFileInvalid = 6
        DeleteUnExisting = 7
        GeneralError = 8
    End Enum
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        moverridelogin = True
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If Request.HttpMethod = "GET" Then
                Dim orderNumber As String = Request.QueryString("OrderNumber")
                Dim statusCode As String = Request.QueryString("StatusCode")
                UpdateRMS(orderNumber, statusCode)
            End If
        Catch ex As Exception
            Response.Output.Write("Some error occured while processing the XML document")
            lblRMSStatus.Visible = False
            Session("DetailError") = "RMSOutput Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

    End Sub

#Region "Private Sub UpdateRMS(ByVal uid As String , ByVal statusCode as String)"
    '#Summary udpates the RMS field in the Orders table based on the input 
    Private Sub UpdateRMS(ByVal orderNumber As String, ByVal statusCode As String)
        Dim RMSStatus As Long = CLng(statusCode)
        Dim objOrder As New Order
        objOrder.UpdateRMSStatus(CLng(orderNumber), RMSStatus)
        lblRMSStatus.Text = " RMSStatus " & statusCode & " Updated for " & orderNumber
        lblRMSStatus.Visible = True
    End Sub
#End Region

End Class
