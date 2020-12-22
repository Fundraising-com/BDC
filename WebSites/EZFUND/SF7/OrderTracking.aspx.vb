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

Imports StoreFront.SystemBase

Partial Class OrderTracking
    Inherits CWebPage
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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
    Private m_objOrderHistory As BusinessRule.Orders.COrders
    Private m_objOrder As BusinessRule.Orders.COrder

    Public ReadOnly Property Order() As BusinessRule.Orders.COrder
        Get
            Return m_objOrder
        End Get
    End Property

    Public ReadOnly Property AddressID() As String
        Get
            Return Request.QueryString("AddressID")
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objOrder As BusinessRule.Orders.COrder
        Try

            SetPageTitle = m_objMessages.GetXMLMessage("OrderTracking.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, Nothing, MessageAlignment)
        Catch ex As Exception
            Session("DetailError") = "Class OrderTracking Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
        If (m_objCustomer.IsSignedIn = False) Then
            Dim strQueryString As String = "?"
            Dim str As String
            Try

                For Each str In Request.QueryString
                    If (strQueryString.Length > 1) Then
                        strQueryString = strQueryString & "&" & str & "=" & Request.QueryString(str)
                    Else
                        strQueryString = strQueryString & str & "=" & Request.QueryString(str)
                    End If
                Next
                strQueryString = HttpUtility.UrlEncode(strQueryString)
            Catch ex As Exception
                Session("DetailError") = "Class OrderTracking Error=" & ex.Message
                Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
            End Try
            Response.Redirect("CustSignIn.aspx?ReturnPage=OrderTracking.aspx" & strQueryString)
        Else
            Try
                m_objOrderHistory = Session("OrderHistory")
                If m_objOrderHistory Is Nothing Then
                    Session("OrderHistory") = New BusinessRule.Orders.COrders()
                    Session("OrderHistory").LoadOrderHistory(m_objCustomer.GetCustomerID(), False)
                    m_objOrderHistory = Session("OrderHistory")
                End If
                For Each objOrder In m_objOrderHistory.Orders
                    If (objOrder.UID = Request.QueryString("OrderID")) Then
                        m_objOrder = objOrder
                        If m_objOrder.OrderAddresses.Count = 0 Then
                            m_objOrder.LoadOrderAddresses()
                        End If
                        '   lblOrderID.Text = m_objOrder.OrderNumber
                        '  lblOrderDate.Text = m_objOrder.OrderDate
                        Exit For
                    End If
                Next

                DataBind()
            Catch ex As Exception
                Session("DetailError") = "Class OrderTracking Error=" & ex.Message
                Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
            End Try
        End If
        
    End Sub

End Class
