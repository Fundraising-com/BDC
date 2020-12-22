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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml
Imports StoreFront.BusinessRule.Orders

Partial Class PackingSlip
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PackingSlipAddress1 As PackingSlipAddress
    Protected WithEvents PackingSlipProducts1 As PackingSlipProducts

    Private _AddressID As Long
    Private _PendingType As PendingType
    Enum PendingType
        Normal = 0
        BackOrder = 1
    End Enum
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

        Try
            Dim objOrder As COrder = Session("objOrder")
            _AddressID = Request.QueryString("Address")

            ''SetPageTitle = StoreFrontConfiguration.StoreName & m_objMessages.GetXMLMessage("Managementdefault.aspx")
            ''Me.TopBanner1.SiteAdditionalText = m_objMessages.GetXMLMessage("Managementdefault.aspx")
            'SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            Dim objOrderAddress As COrderAddress = Nothing
            If Request.QueryString("Type") = "0" Then
                _PendingType = PendingType.Normal
            Else
                _PendingType = PendingType.BackOrder
            End If
            If IsNothing(objOrder) Then
                Response.Redirect(StoreFrontConfiguration.SSLPath & "Management/OrderFulfillment.aspx")
            Else
                If Not IsPostBack Then
                    lblOrderID.Text = objOrder.OrderNumber
                    lblOrderDate.Text = objOrder.OrderDate

                    For Each objOrderAddress In objOrder.OrderAddresses
                        If objOrderAddress.Address.ID = _AddressID Then
                            Exit For
                        End If
                    Next
                End If
                PackingSlipAddress1.ShipAddress = objOrderAddress.Address
                PackingSlipAddress1.BillAddress = objOrder.BillAddress
                PackingSlipProducts1.Order(_PendingType) = objOrderAddress

            End If
        Catch ex As Exception
            Session("DetailError") = "Class PackingSlip Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
