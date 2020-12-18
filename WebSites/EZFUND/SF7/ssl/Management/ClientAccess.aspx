<%@ Page Language="vb" AutoEventWireup="false" validateRequest="False"%>

<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="StoreFront.SystemBase"%>
<%@ Import Namespace="StoreFront.BusinessRule"%>
<%@ Import Namespace="System.Xml"%>

<%
	PageLoad()
%>
<script runat="server">
	Sub PageLoad()
		Dim objClient As New StoreFront.BusinessRule.CClientConnection
        If (IsNothing(Request.Form("String")) = False) Then
            objClient.Request(Request.Form("String"), Request.Form("String2"))
            If Request.Form("webConfig") <> "" Then
                WriteWebConfigFile(Request.Form("webConfig"))
            End If
        ElseIf (IsNothing(Request.Form("XML")) = False) Then
            objClient.RequestXML(Request.Form("XML"), Request.Form("String3"), Request.Form("String4"))
        ElseIf (IsNothing(Request.InputStream) = False) Then
            ' Start Here
            If (Request.InputStream.Length > 0) Then
                Dim sr As StreamReader
                Dim count As Integer
                Dim read(256) As Char
                Dim strRequest As String
                Dim arForm() As String
                Dim strPair As String
                Dim arNVPair() As String

                Dim strXML As String
                Dim strString3 As String
                Dim strString4 As String

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
	

    Sub WriteWebConfigFile(ByVal webConfig As String)
        Try
            If webConfig <> "" Then
                Dim xmlDom As New XmlDocument
                Dim xmlNewNode As XmlNode
                xmlDom.Load(StoreFrontConfiguration.ServerPath & "\web.config")
                Dim webConfigDom As New XmlDocument
                webConfigDom.LoadXml(webConfig)
                'if appsettings section does not exist
                If xmlDom.GetElementsByTagName(webConfigDom.FirstChild.Name).Count = 0 Then
                    xmlNewNode = xmlDom.CreateNode(XmlNodeType.Element, webConfigDom.FirstChild.Name, Nothing)
                    xmlNewNode.InnerXml = webConfigDom.FirstChild.InnerXml
                    xmlDom.DocumentElement.AppendChild(xmlNewNode)
                Else
                    'checks if the section to be added exists if not appends to the appsettings section
                    xmlNewNode = xmlDom.GetElementsByTagName(webConfigDom.FirstChild.Name)(0)
                    If webConfigDom.FirstChild.ChildNodes.Count > 0 Then
                        Dim appNode As XmlNode
                        Dim webNode As XmlNode
                        For Each appNode In webConfigDom.FirstChild.ChildNodes
                            Dim flag As Boolean = False
                            For Each webNode In xmlNewNode.ChildNodes
                                If appNode.OuterXml = webNode.OuterXml Then
                                    flag = True
                                End If
                            Next
                            If flag = False Then
                                Dim childNode As XmlNode = xmlDom.ImportNode(appNode, False)
                                xmlNewNode.AppendChild(childNode)
                            End If
                        Next
                    End If
                  End If
                xmlDom.Save(StoreFrontConfiguration.ServerPath & "\web.config")
            End If
        Catch Ex As Exception
            Throw New Exception(Ex.Message)
        End Try
    End Sub

</script>

<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

	