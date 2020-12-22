'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.io
Imports StoreFront.SystemBase

Public Class default_image
    Inherits CWebPage

#Region "Members "

    Protected strHTML As String
    Protected catid As String
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected strObjMain As String
    Protected WithEvents UploadControl1 As UploadControl

#End Region

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

#Region "Properties "

    Public Property HTMLString() As String
        Get
            Return strHTML
        End Get
        Set(ByVal Value As String)
            strHTML = Value
        End Set
    End Property
    Public Property objMainFolder() As String
        Get
            Return strObjMain
        End Get
        Set(ByVal Value As String)
            strObjMain = Value
        End Set
    End Property

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Put user code to initialize the page here
            catid = CStr(Request("catid"))
            UploadControl1.FileType = UploadControl._FileType.Image
            UploadControl1.Reverse = True
            Dim filepath As String
            If Request.QueryString("action") = "del" Then
                filepath = Request.QueryString("file")
                filepath = Replace(filepath, "/", "\")
                If File.Exists(filepath) Then
                    File.SetAttributes(filepath, IO.FileAttributes.Normal)
                    File.Delete(filepath)
                End If
            End If
            If Request.QueryString("action") = "upload" Then
                Response.Expires = 0
                Response.Buffer = True
                Response.Clear()
                Dim sFileName As String
                Dim sPath As String

            End If
            Dim imagepath As String
            'imagepath = StoreFrontConfiguration.ServerPath & "Images"
            imagepath = Me.MapPath("..") & "\Images"
            objMainFolder = imagepath

            If catid = "" Then catid = imagepath
            Dim info As FileInfo
            Dim objTempFiles As String()
            Dim objTempFile As String
            objTempFiles = Directory.GetFiles(catid)
            strHTML = strHTML & "<table border=0 cellpadding=3 cellspacing=0 width=240>"
            For Each objTempFile In objTempFiles
                info = New FileInfo(objTempFile)
                strHTML = strHTML & "<tr bgcolor=Gainsboro>"
                strHTML = strHTML & "<td valign=top>" & info.Name & "</td>"
                strHTML = strHTML & "<td valign=top>" & FormatNumber(info.Length / 1000, 0) & " kb</td>"
                strHTML = strHTML & "<td valign=top style=""cursor:hand;"" onclick=""selectImage('" & StoreFrontConfiguration.SSLPath & ConstructPath(objTempFile) & "')""><u><font color=blue>select</font></u></td>"
                strHTML = strHTML & "<td valign=top style=""cursor:hand;"" onclick=""deleteImage('" & Server.UrlEncode(objTempFile) & "')""><u><font color=blue>del</font></u></td></tr>"
            Next
            strHTML = strHTML & "</table>"
            createCategoryOptions(imagepath)
            DataBind()
        Catch ex As Exception
            Session("DetailError") = "Class Default_Image Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub



    Public Sub createCategoryOptions(ByVal targetDirectory As String)
        Dim objFolder As String
        Dim objFolders As String()
        Dim strOptions As String

        objFolders = Directory.GetDirectories(targetDirectory)
        For Each objFolder In objFolders
            'Recursive programming starts here
            createCategoryOptions(objFolder)
        Next

        If CStr(catid) = CStr(targetDirectory) Then
            strOptions = strOptions & "<option value=" & targetDirectory & " selected>" & ConstructPath(targetDirectory) & "</option>" & vbCrLf
        Else
            strOptions = strOptions & "<option value=" & targetDirectory & ">" & ConstructPath(targetDirectory) & "</option>" & vbCrLf
        End If
        'End If
        objMainFolder = objMainFolder & strOptions & vbCrLf
    End Sub

    Function ConstructPath(ByVal str)
        str = Replace(LCase(str), LCase(Me.MapPath("..") & "\"), "")
        If Left(str, 1) = "\" Then
            str = Right(str, Len(str) - 1)
        End If
        Return Replace(str, "\", "/")
    End Function

    Function constructdeletepath(ByVal str)
        str = Replace(LCase(str), LCase(StoreFrontConfiguration.ServerPath), "")
        If Left(str, 1) = "\" Then
            str = Right(str, Len(str) - 1)
        End If
        Return Replace(str, "\", "/")
    End Function




End Class
