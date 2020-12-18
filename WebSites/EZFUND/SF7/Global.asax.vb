Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Web.Caching
Imports System.Web
Imports System.Web.UI
Imports System.Web.SessionState
Imports System.Threading

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
'Tee 12/5/2007 added class reference
Imports System.Text.RegularExpressions
'end Tee

Public Class [Global]
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

    Public cctimer As Timer

    'Tee 12/5/2007 patterns matching default url with query strings
    Private Shared m_arDefaultUrlPattern() As String = {"(.*)/searchresult.aspx\?categoryid=(\d+)(.*)", "(.*)/detail.aspx\?id=(\d+)(.*)"}
    Private Shared m_arMatchRewrittenUrl() As String = {"$1/cat-INSERTCATNAME-$2.aspx", "$1/prod-INSERTPRODNAME-$2.aspx?$3"}
    'end Tee

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        'update #2383
        StoreFrontConfiguration.OnApplicationStart(Context.Server.MapPath(Context.Request.ApplicationPath))
        Dim Cleaning As Integer = StoreFrontConfiguration.CleanUp
        Dim period As Integer = Cleaning * 60 * 1000 'specifying the periodic time in milliseconds
        Dim objSchedule As New CScheduleMaintenance
        Dim timerdelegate As TimerCallback = AddressOf objSchedule.ScheduledDeletion
        cctimer = New Timer(timerdelegate, Nothing, 0, period)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'Tee 12/5/2007 redirect to preserve search engine ranking
        Dim oRegEx As Regex
        Dim id As Long = 0
        Dim resultPath As String = String.Empty
        For iIndex As Integer = 0 To m_arDefaultUrlPattern.Length - 1
            oRegEx = New Regex(m_arDefaultUrlPattern(iIndex))
            If oRegEx.Match(Request.Url.PathAndQuery.ToLower).Success Then
                resultPath = oRegEx.Replace(Request.Url.PathAndQuery.ToLower, m_arMatchRewrittenUrl(iIndex))
                id = CLng(resultPath.Substring(resultPath.LastIndexOf("-") + 1).Replace(Regex.Match(resultPath, "\.aspx.*").Value, ""))
                If iIndex = 0 Then 'search result page
                    resultPath = resultPath.Replace("INSERTCATNAME", StoreFrontConfiguration.SafeName((New CCategories).GetCategory(id).Name))
                ElseIf iIndex = 1 Then 'detail page
                    resultPath = resultPath.Replace("INSERTPRODNAME", StoreFrontConfiguration.SafeName(New BusinessRule.Management.CProductManagement(id, True).Name))
                End If
                If resultPath.Length > 0 AndAlso Not IsNothing(Request.UrlReferrer) AndAlso Request.UrlReferrer.AbsoluteUri.IndexOf(StoreFrontConfiguration.SiteURL) = -1 Then
                    Response.Status = "301 Moved Permanently"
                    Response.AddHeader("Location", resultPath)
                End If
            End If
        Next
        'end Tee
        ' begin: JDB - 2/20/2007 - UrlRewriter Add-On
        Dim sRewrittenPath As String = StoreFrontConfiguration.GetRewrittenPath(Request.Url.PathAndQuery)
        If sRewrittenPath.Length > 0 Then
            HttpContext.Current.Items.Add("VirtualURL", Request.Url.PathAndQuery)
            HttpContext.Current.RewritePath(sRewrittenPath)
        End If
        ' end: JDB - 2/20/2007 - UrlRewriter Add-On
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)

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