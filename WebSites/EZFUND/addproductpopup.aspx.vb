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

Imports System.Xml

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Public Class AddProductPopUp
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblDisplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents imgClose As System.Web.UI.WebControls.Image
    Protected WithEvents imgCheckout As System.Web.UI.WebControls.Image
    Protected WithEvents btnClose As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnCheckout As System.Web.UI.WebControls.LinkButton
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
        'Put user code to initialize the page here
        Dim objItem As CCartItem = Session("ItemAdded")

        Try
            If (IsNothing(objItem) = True) Then
                RegisterClientScriptBlock("myScript", "<script" _
                            & "  language='JavaScript'>window.close();</script>")
                Exit Sub
            End If

            If (IsNothing(Session("HistoryMoved")) = False) Then
                If (Session("HistoryMoved") <> "1") Then
                    Session("HistoryMoved") = Nothing
                    'RegisterClientScriptBlock("myScript", "<script" _
                    '            & "  language='JavaScript'>opener.history.go(-1);</script>")
                    RegisterClientScriptBlock("myScript", "<script" _
                        & "  language='JavaScript'>opener.location = opener.location;</script>")
                    Session("Popup") = 1
                Else
                    Session("HistoryMoved") = Nothing
                End If
            Else
                '    RegisterClientScriptBlock("myScript", "<script" _
                '& "  language='JavaScript'>opener.history.go(-1);</script>")
                RegisterClientScriptBlock("myScript", "<script" _
                    & "  language='JavaScript'>opener.location = opener.location;</script>")
                Session("Popup") = 1
            End If

            Dim strMessage As String = m_objMessages.GetXMLMessage("AddProduct", "AddToCart", "Add")
            Dim objNode As XmlNode = StoreFrontConfiguration.AddProductStyle()

            If (objNode.Attributes("DisplayQuantity").Value = "1") Then
                strMessage = strMessage.Replace("[Quantity]", objItem.Quantity)
            Else
                strMessage = strMessage.Replace("[Quantity] ", "")
            End If
            If (objNode.Attributes("DisplayProductName").Value = "1") Then
                If (objItem.Quantity > 1) Then
                    strMessage = strMessage.Replace("[ProductName]", objItem.PluralName)
                    strMessage = strMessage.Replace("[has]", "have")
                Else
                    strMessage = strMessage.Replace("[ProductName]", objItem.Name)
                    strMessage = strMessage.Replace("[has]", "has")
                End If
            Else
                strMessage = strMessage.Replace("[has] ", "")
                strMessage = strMessage.Replace("[ProductName] ", "")
            End If

            If (objNode.Attributes("DisplayUpSellMessage").Value = "1") Then
                strMessage = strMessage.Replace("[UpSellMessage]", objItem.UpSellMessage)
            Else
                strMessage = strMessage.Replace("[UpSellMessage] ", "")
            End If

            lblDisplay.Text = strMessage
            Session("ItemAdded") = Nothing

            imgCheckout.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("CheckOut").Attributes("Filename").Value
            imgClose.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Close").Attributes("Filename").Value
            btnCheckout.Attributes.Add("onclick", "CheckoutFromPopUp();")
            btnClose.Attributes.Add("onclick", "window.close();")
        Catch ex As Exception
            Session("DetailError") = "Class AddProductPopUp Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub


End Class
