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

Public Class Detail1
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            SetPageTitle = m_objMessages.GetXMLMessage("Detail.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)

            If (IsNothing(Session("EmailedAFriend")) = False) Then
                Message.Text = Session("EmailedAFriend")
                Message.Visible = True
                Session("EmailedAFriend") = Nothing
            ElseIf Session("strMessage") <> "" Then
                Message.Visible = True
                Session("strMessage") = ""
            Else
                Message.Text = ""
                Message.Visible = False
            End If

            If (dom.Item("SiteProducts").Item("ProductDetail").Attributes("Type").Value = "1") Then
                ProductDetail11.Visible = True
                ProductDetail21.Visible = False
                If (Not (IsNothing(ProductDetail11.FindControl("btnAddToCart")))) Then
                    CType(ProductDetail11.FindControl("btnAddToCart"), LinkButton).Attributes.Add("onclick", "return SetValidationSearchResults('');")
                End If
                If (Not (IsNothing(ProductDetail11.FindControl("btnAddToSavedCart")))) Then
                    CType(ProductDetail11.FindControl("btnAddToSavedCart"), LinkButton).Attributes.Add("onclick", "return SetValidationSearchResults('');")
                End If
            Else
                ProductDetail11.Visible = False
                ProductDetail21.Visible = True
                If (Not (IsNothing(ProductDetail21.FindControl("btnAddToCart")))) Then
                    CType(ProductDetail21.FindControl("btnAddToCart"), LinkButton).Attributes.Add("onclick", "return SetValidationSearchResults('');")
                End If
                If (Not (IsNothing(ProductDetail21.FindControl("btnAddToSavedCart")))) Then
                    CType(ProductDetail21.FindControl("btnAddToSavedCart"), LinkButton).Attributes.Add("onclick", "return SetValidationSearchResults('');")
                End If
            End If
            
        Catch ex As Exception
            Session("DetailError") = "Class Detail Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
        Else
            Message.Text = ""
            Message.Visible = False
        End If
    End Sub

    Private Sub Page_USER_ERROR(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.USER_ERROR
        Try
            ErrorMessage.Visible = True
            ErrorMessage.Text = sender.ToString
        Catch err As System.Exception
            ErrorMessage.Visible = False
        End Try
    End Sub
End Class
