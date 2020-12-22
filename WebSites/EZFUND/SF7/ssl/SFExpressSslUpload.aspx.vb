'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.IO
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule.Management
Partial Class SFExpressSslUpload
    Inherits System.Web.UI.Page

    'Enum _FileType
    '    DownLoad = 0
    '    Image = 1
    '    DealTime = 3
    'End Enum

    'Private mFileType As _FileType = _FileType.Image
    'Private _FileName As String

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

        Dim oAdminGeneralManagement As New CAdminGeneralManagement

        If IsNothing(Request.Form("AdminGuid")) OrElse Request.Form("AdminGuid").Trim = String.Empty Then
            Exit Sub
        ElseIf Not Request.Form("AdminGuid").Trim.ToLower.Equals(oAdminGeneralManagement.AdminGuid.ToLower) Then
            Exit Sub
        ElseIf IsNothing(Request.Form("Type")) OrElse Not Request.Form("Type").Trim.Equals("1") Then
            Exit Sub
        End If

        Try

            Dim sFileBytes As String = Request.Form("FileBytes")
            Dim sFileName As String = Request.Form("FileName")

            Dim buffer(sFileBytes.ToCharArray.Length) As Byte

            buffer = System.Convert.FromBase64String(sFileBytes)

            Dim sPath As String = MapPath(sFileName)

            Dim oStreamWriter As New StreamWriter(sPath, False)
            Dim oBinaryWriter As New BinaryWriter(oStreamWriter.BaseStream)

            oBinaryWriter.Write(buffer)
            oBinaryWriter.Close()

        Catch ex As Exception
            StoreFrontConfiguration.Logging.Err = ex
        End Try

        '' begin: JDB - logging for file upload
        'Try
        '    ' end: JDB - logging for file upload
        '    If (IsNothing(Request.Form("Bytes")) = False) Then

        '        Dim sBytes As String = Request.Form("FileBytes")

        '        mFileType = CInt(Request.Form("Bytes"))
        '        _FileName = Request.Form("FileName")

        '        If IsNothing(Request.Form("AdminGuid")) OrElse Request.Form("AdminGuid").Trim = "" Then
        '            Exit Sub
        '        End If
        '        Dim admin As New CAdminGeneralManagement
        '        Dim adminGuid As String = Request.Form("AdminGuid").Trim
        '        If adminGuid <> admin.AdminGuid Then
        '            Exit Sub
        '        End If
        '        Dim oBuffer(sBytes.ToCharArray.Length) As Byte
        '        If mFileType = _FileType.DealTime Then
        '            oBuffer = System.Text.Encoding.UTF8.GetBytes(sBytes)
        '        Else
        '            oBuffer = System.Convert.FromBase64String(sBytes)
        '        End If
        '        SaveFile(oBuffer)
        '    End If
        '    ' begin: JDB - logging for file upload
        'Catch err As Exception
        '    StoreFrontConfiguration.Logging.Err = err
        'End Try
        '' end: JDB - logging for file upload
    End Sub

    'Private Sub SaveFile(ByVal oBuffer() As Byte)
    '    Dim sPath As String = Server.MapPath("")
    '    Try
    '        _FileName = Trim(_FileName.Replace("/", "\"))
    '        Select Case mFileType
    '            Case _FileType.DownLoad
    '                sPath = sPath & "\Download\" & Trim(_FileName)
    '            Case _FileType.Image
    '                'sPath = sPath & "\Images\" & Trim(_FileName)
    '                Dim oFileInfo As New FileInfo(_FileName.Trim)
    '                sPath = _FileName
    '                _FileName = oFileInfo.Name
    '            Case _FileType.DealTime
    '                sPath = sPath & Trim(_FileName)
    '        End Select

    '        If sPath.EndsWith("\") Then
    '            sPath = sPath.Substring(0, sPath.LastIndexOf("\") - 1)
    '        End If

    '        If _FileName.IndexOf("\") > 0 Then
    '            If Not Directory.Exists(sPath.Substring(0, sPath.LastIndexOf("\"))) Then
    '                Directory.CreateDirectory(sPath.Substring(0, sPath.LastIndexOf("\")))
    '            End If
    '        End If


    '        Dim oWriter As New StreamWriter(sPath, False)
    '        Dim sbWriter As New BinaryWriter(oWriter.BaseStream)
    '        sbWriter.Write(oBuffer)
    '        sbWriter.Close()
    '    Catch err As SystemException
    '        ' begin: JDB - logging for file upload
    '        'Response.Write(err.Message)
    '        StoreFrontConfiguration.Logging.Err = err
    '        ' end: JDB - logging for file upload
    '    End Try
    'End Sub
End Class
