Imports System.IO
Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Public Class ClientAccess
    Inherits System.Web.UI.Page

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

        Dim objClient As New BusinessRule.CClientConnection()

        If (IsNothing(Request.Form("String")) = False) Then
            objClient.Request(Request.Form("String"), Request.Form("String2"))
        ElseIf (IsNothing(Request.Form("XML")) = False) Then
            objClient.RequestXML(Request.Form("XML"), Request.Form("String3"), Request.Form("String4"))
        ElseIf (IsNothing(Request.InputStream) = False) Then
            ' Start Here
            If (Request.InputStream.Length > 0) Then
                Dim sr As StreamReader
                Dim count As Integer
                Dim read(256) As Char
                Dim strRequest As String = ""
                Dim arForm() As String
                Dim strPair As String
                Dim arNVPair() As String

                Dim strXML As String = ""
                Dim strString3 As String = ""
                Dim strString4 As String = ""

                sr = New StreamReader(Request.InputStream, System.Text.Encoding.UTF8)
                count = sr.Read(read, 0, 256)

                Do While count > 0
                    Dim str1 As String = New String(read, 0, count)
                    strRequest = strRequest & str1
                    count = sr.Read(read, 0, 256)
                Loop

                arForm = strRequest.Split("&")

                For Each strPair In arForm
                    arNVPair = strPair.Split("=")
                    If (arNVPair.GetValue(0) = "XML") Then
                        strXML = HttpUtility.UrlDecode(arNVPair.GetValue(1))
                    ElseIf (arNVPair.GetValue(0) = "String3") Then
                        strString3 = HttpUtility.UrlDecode(arNVPair.GetValue(1))
                    ElseIf (arNVPair.GetValue(0) = "String4") Then
                        strString4 = HttpUtility.UrlDecode(arNVPair.GetValue(1))
                    End If
                Next

                objClient.RequestXML(strXML, strString3, strString4)
            End If
        End If

        Response.ContentType = "text/xml"

        Response.Write(objClient.Response)

        objClient = Nothing

        Response.End()

    End Sub

End Class
