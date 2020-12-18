'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Option Explicit On 
Imports System
Imports System.Xml
Imports System.Threading
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.WebRequest
Imports StoreFront.BusinessRule


Public Class ReloadXML
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable

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

        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
            ReloadXML()
        Else
            ReloadLive()
        End If

    End Sub

    Private Sub ReloadXML()

        If (Request.QueryString("SSL") <> "0" And IsNothing(Application("ThreadXML2")) = True) Then
            Dim objThread2 As New ThreadWork2()
            objThread2.threadImport.Name = "StoreFront 6 ReloadXML2"
            objThread2.UserName = Request.ServerVariables("AUTH_USER")
            objThread2.Password = Request.ServerVariables("AUTH_PASSWORD")
            objThread2.threadImport.Start()
            Application("ThreadXML2") = objThread2
        End If

        If (Request.Url.ToString.ToLower.StartsWith(StoreFrontConfiguration.SSLPath) = True) Then
            Dim objThread As New ThreadWork()

            objThread.threadImport.Name = "StoreFront 6 ReloadXML"

            objThread.threadImport.Start()

            While objthread.threadImport.IsAlive = True
                System.Threading.Thread.Sleep(1000)
            End While
            Exit Sub
        End If

        If (Application("ThreadXML2").threadImport.IsAlive = False) Then
            If (IsNothing(Application("ThreadXML")) = True) Then
                Dim objThread As New ThreadWork()

                objThread.threadImport.Name = "StoreFront 6 ReloadXML"

                objThread.threadImport.Start()

                Application("ThreadXML") = objThread
            Else
                Dim obj As ThreadWork
                obj = Application("ThreadXML")
                If (obj.threadImport.IsAlive = False) Then
                    Application("ThreadXML") = Nothing
                    Application("ThreadXML2") = Nothing
                    Response.Redirect(StoreFrontConfiguration.SiteURL())
                End If
            End If
        End If
    End Sub

    Private Sub ReloadLive()

        If Request.QueryString("SSL") <> "0" Then
            RePopulateCatalog()
            Set_SessionMode()
            Dim objWeb As New CWebRequest
            objWeb.Type = 1
            objWeb.URI = New String(StoreFrontConfiguration.SSLPath() & "ReloadXML.aspx?SSL=0")
            objWeb.SendRequest2(Request.ServerVariables("AUTH_USER"), Request.ServerVariables("AUTH_PASSWORD"))
        End If

        With New CXMLSiteBuilder
            .Load()
            StoreFrontConfiguration.XMLDocument = .XMLDoc
        End With
        Response.Redirect(StoreFrontConfiguration.SiteURL())
    End Sub
    Private Sub Set_SessionMode()
        Dim objServ As New System.ServiceProcess.ServiceController
        Dim xmlDom As XmlDocument
        Dim xmlElem As XmlElement
        Dim xmlAttr As XmlAttribute

        objServ.ServiceName = "aspnet_state"
        'only change if state server is running and the web is inProc
        If objServ.Status = ServiceProcess.ServiceControllerStatus.Running And Session.Mode = SessionState.SessionStateMode.InProc Then
            xmlDom = New XmlDocument
            xmlDom.Load(StoreFrontConfiguration.ServerPath & "\web.config")
            xmlElem = xmlDom.SelectSingleNode("descendant::sessionState")
            xmlAttr = xmlElem.Attributes("mode")
            If xmlAttr.Value = "InProc" Then xmlAttr.Value = "StateServer"
            xmlDom.Save(StoreFrontConfiguration.ServerPath & "\web.config")
        End If

    End Sub
    Private Sub RePopulateCatalog()
        Dim objsearch As CSearchEngine
        objsearch = New CSearchEngine
        objsearch.PopulateSearchCatalog()
    End Sub
    Public Function DoRefresh() As String

        If StoreFrontConfiguration.ProductLoading = SystemBase.StoreFrontConfiguration.LoadType.XML Then
            Return "<meta http-equiv='Refresh' content='10'>"
        Else
            Return ""
        End If

    End Function
    Class ThreadWork
        Private objthreadImport As Thread

        Public Sub New()
            objthreadImport = New Thread(AddressOf Me.DoWork)
        End Sub

        Public Property threadImport() As Thread
            Get
                Return objthreadImport
            End Get
            Set(ByVal Value As Thread)
                objthreadImport = Value
            End Set
        End Property

        Sub DoWork()
            With New CXMLSiteBuilder
                .Load()
                StoreFrontConfiguration.XMLDocument = .XMLDoc
            End With
        End Sub

    End Class

    Class ThreadWork2
        Private objthreadImport As Thread
        Private m_strUser As String = ""
        Private m_strPassword As String = ""

        Public Sub New()
            objthreadImport = New Thread(AddressOf Me.DoWork)
        End Sub

        Public Property UserName() As String
            Get
                Return m_strUser
            End Get
            Set(ByVal Value As String)
                m_strUser = Value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return m_strPassword
            End Get
            Set(ByVal Value As String)
                m_strPassword = Value
            End Set
        End Property

        Public Property threadImport() As Thread
            Get
                Return objthreadImport
            End Get
            Set(ByVal Value As Thread)
                objthreadImport = Value
            End Set
        End Property

        Sub DoWork()
            Dim objWeb As New CWebRequest
            objWeb.Type = 1
            objWeb.URI = New String(StoreFrontConfiguration.SSLPath() & "ReloadXML.aspx?SSL=0")
            objWeb.SendRequest2(m_strUser, m_strPassword)
            objWeb = Nothing
        End Sub
    End Class

End Class
