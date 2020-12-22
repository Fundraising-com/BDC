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
Partial Class sfupload
    Inherits System.Web.UI.Page

    Enum m_FileType
        DownLoad = 0
        Image = 1
        DealTime = 3
    End Enum

    Private mFileType As m_FileType = m_FileType.Image
    Private _FileName As String

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
        ' begin: JDB - logging for file upload
        Try
            ' end: JDB - logging for file upload
            If (IsNothing(Request.Form("Bytes")) = False) Then
                'update #1567, 1912
                'Dim mChar() As Char
                'Dim i As Long
                Dim s As String = Request.Form("FileBytes")
                mFileType = CInt(Request.Form("Bytes"))
                _FileName = Request.Form("FileName")
                '2641
                If IsNothing(Request.Form("AdminGuid")) OrElse Request.Form("AdminGuid").Trim = "" Then
                    Exit Sub
                End If
                Dim admin As New CAdminGeneralManagement
                Dim adminGuid As String = Request.Form("AdminGuid").Trim
                If adminGuid <> admin.AdminGuid Then
                    Exit Sub
                End If
                Dim oBuffer(s.ToCharArray.Length) As Byte
                'mChar = s.ToCharArray
                If mFileType = m_FileType.DealTime Then
                    oBuffer = System.Text.Encoding.UTF8.GetBytes(s)
                Else
                    '   For i = 0 To mChar.Length - 1
                    '   oBuffer(i) = Convert.ToByte(mChar(i))
                    '   Next
                    oBuffer = System.Convert.FromBase64String(s)
                End If
                SaveFile(oBuffer)
            End If
            ' begin: JDB - logging for file upload
        Catch err As Exception
            StoreFrontConfiguration.Logging.Err = err
        End Try
        ' end: JDB - logging for file upload
    End Sub

    Private Sub SaveFile(ByVal oBuffer() As Byte)
        Dim sPath As String = StoreFrontConfiguration.ServerPath
        Try

            Select Case mFileType
                Case m_FileType.DownLoad
                    sPath = sPath & "Download\" & Trim(_FileName)
                Case m_FileType.Image
                    sPath = sPath & "Images\" & Trim(_FileName)
                Case m_FileType.DealTime
                    sPath = sPath & Trim(_FileName)
            End Select

            Dim oWriter As New StreamWriter(sPath, False)
            Dim sbWriter As New BinaryWriter(oWriter.BaseStream)
            ' StrReader.Read(sBuffer, 0, sString.Length)
            sbWriter.Write(oBuffer)
            sbWriter.Close()
        Catch err As SystemException
            ' begin: JDB - logging for file upload
            'Response.Write(err.Message)
            StoreFrontConfiguration.Logging.Err = err
            ' end: JDB - logging for file upload
        End Try
    End Sub
End Class
