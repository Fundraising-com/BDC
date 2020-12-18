Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Web.Caching
Imports System.Web
Imports System.Web.UI
Imports System.Web.SessionState

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class Global
    Inherits HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        StoreFrontConfiguration.OnApplicationStart(Context.Server.MapPath(Context.Request.ApplicationPath))
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        'Delete from Shopping Cart if older than 24 hours
        Dim objSchedule As New CScheduleMaintenance(CScheduleMaintenance.MaintenanceTypes.CreditCard)
        CType(Session("XMLShoppingCart"), CCart).DeleteFromDB()
        CType(Session("Customer"), CCustomer).DeleteWalkin()
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class

'Public Class GlobalBase
'    Inherits HttpApplication

'    Private Shared needsCompile As Boolean = True
'    Private Shared applicationPath As String = ""
'    Private Shared physicalPath As String = ""
'    Private Shared applicationURL As String = ""
'    Private Shared strLog As String = "c:\temp\compileLog.txt"

'    Public Overrides Sub Init()
'        If (GlobalBase.needsCompile) Then
'            GlobalBase.needsCompile = False

'            applicationPath = HttpContext.Current.Request.ApplicationPath
'            If (applicationPath.EndsWith("/") = False) Then
'                applicationPath += "/"
'            End If
'            Dim server As String = HttpContext.Current.Request.ServerVariables("SERVER_NAME")
'            Dim https As Boolean
'            https = IIf(HttpContext.Current.Request.ServerVariables("HTTPS") <> "off", True, False)
'            applicationURL = IIf(https, "https://", "http://") & server & applicationPath

'            physicalPath = HttpContext.Current.Request.PhysicalApplicationPath

'            Dim sw As TextWriter = File.CreateText(strLog)

'            GlobalBase.CompileFolder(physicalPath, sw)

'            sw.Close()
'        End If
'    End Sub

'    Private Shared Sub CompileFolder(ByVal Folder As String, ByRef sw As TextWriter)
'        Dim file As String
'        Dim fold As String

'        For Each file In Directory.GetFiles(Folder, "*.as?x")
'            Try
'                Dim path As String = file.Remove(0, physicalPath.Length)
'                If (file.ToLower.EndsWith(".ascx")) Then
'                    Try
'                        Dim virtualPath As String = applicationPath & path.Replace("\", "/")
'                        Dim controlLoader As Page = New Page()
'                        'sw.WriteLine("ControlLoader " & virtualPath)
'                        'controlLoader.LoadControl(virtualPath)
'                    Catch e As Exception
'                        sw.WriteLine("Error on LoadControl " & e.Message)
'                    End Try
'                ElseIf (file.ToLower.EndsWith(".asax") = False) Then
'                    Try
'                        Dim url As String = applicationURL & path.Replace("\", "/")
'                        sw.WriteLine("HttpWebRequest.Create " & url)
'                        HttpWebRequest.Create(url).BeginGetResponse(Nothing, Nothing)
'                    Catch e As Exception
'                        sw.WriteLine("Error on Request " & e.Message)
'                    End Try
'                End If
'            Catch e As Exception
'                sw.WriteLine("Error on file.Remove " & e.Message)
'            End Try
'        Next

'        For Each fold In Directory.GetDirectories(Folder)
'            GlobalBase.CompileFolder(fold, sw)
'        Next
'    End Sub
'End Class