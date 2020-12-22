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

Imports System.IO

Imports StoreFront.BusinessRule.Orders

Imports StoreFront.SystemBase

Public Class Download
    Inherits CWebPage

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
    Private m_objOrderHistory As COrders
    Private m_objOrder As COrder

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim objOrder As COrder
        Dim objAddress As COrderAddress
        Dim objItem As COrderItem

        If (m_objCustomer.GetCustomerID() = -1) Then
            Response.Redirect("CustSignIn.aspx?ReturnPage=Download.aspx?OrderID=" & Request.QueryString("OrderID") & "%26ItemID=" & Request.QueryString("ItemID"))
        End If

        Try
            If (IsNothing(Session("OrderHistory")) = True) Then
                Session("OrderHistory") = New BusinessRule.Orders.COrders()
                Session("OrderHistory").LoadOrderHistory(m_objCustomer.GetCustomerID())
            End If
            m_objOrderHistory = Session("OrderHistory")

            For Each objOrder In m_objOrderHistory.Orders
                If (objOrder.UID = Request.QueryString("OrderID")) Then
                    m_objOrder = objOrder
                    Exit For
                End If
            Next
        Catch ex As Exception
            Session("DetailError") = "Class Download Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

        For Each objAddress In m_objOrder.OrderAddresses
            For Each objItem In objAddress.OrderItems
                If (objItem.OrderItemID = Request.QueryString("ItemID")) Then
                    Dim objDownload As New CDownload()
                    objDownload.SetDownloadFlag(m_objOrder.UID, objItem.OrderItemID, 1)
                    objItem.Downloaded = True
                    If (objItem.FileName <> "") Then
                        DownloadFile(Server.MapPath("Download") & "\" & objItem.FileName())
                    Else
                        Dim objAttr As CAttribute
                        Dim objdetail As CAttributeDetail
                        For Each objAttr In objItem.Attributes
                            For Each objdetail In objAttr.AttributeDetails
                                If (objdetail.FilePath <> "") Then
                                    DownloadFile(Server.MapPath("Download") & "\" & objdetail.FilePath)
                                    Exit Sub
                                End If
                            Next
                        Next
                    End If

                    Exit For
                End If
            Next
        Next

    End Sub

    Private Sub DownloadFile(ByVal strFile As String)
        Dim obj As File
        Dim rdStream As FileStream
        Dim iCount As Long

        rdStream = obj.OpenRead(strFile)

        Dim ar(rdStream.Length() - 1) As Byte

        iCount = rdStream.Read(ar, 0, rdStream.Length())

        rdStream.Close()

        Response.ContentType = "application/exe"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & strFile.Substring(strFile.LastIndexOf("\") + 1))
        Response.BinaryWrite(ar)
        Response.End()
    End Sub
End Class
