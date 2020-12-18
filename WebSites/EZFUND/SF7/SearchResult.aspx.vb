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

Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

Partial Class SearchResult
    Inherits CWebPage
    Protected WithEvents AddProductPopUp As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected SpeedSearch1 As SpeedSearch
    Private HasMsg As Boolean = False
    Dim categoryname As String
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
            SetPageTitle = m_objMessages.GetXMLMessage("SearchResult.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)

            If (IsNothing(Session("EmailedAFriend")) = False) Then
                Message.Text = Session("EmailedAFriend")
                Message.Visible = True
                Session("EmailedAFriend") = Nothing
            Else
                If HasMsg = False Then
                    Message.Text = ""
                    Message.Visible = False
                Else
                    'reset flag 
                    HasMsg = False
                End If

            End If
            ErrorMessage.Visible = False

            'Begin Custom Code 5/06
            'If (IsNothing(Session("ItemAdded")) = False) Then
            '    SetMessage(Message)
            'Else
            '    Message.Text = ""
            '    Message.Visible = False
            'End If
            'End Custom Code 5/06
            If (dom.Item("SiteProducts").Item("SearchResult").Attributes("Type").Value = "1") Then
                SearchTemplate12.Visible = False
                SearchTemplate13.Visible = False
                SpeedSearch1.Visible = False
                SearchTemplate11.Visible = True
            ElseIf (dom.Item("SiteProducts").Item("SearchResult").Attributes("Type").Value = "2") Then
                SearchTemplate11.Visible = False
                SearchTemplate13.Visible = False
                SpeedSearch1.Visible = False
                SearchTemplate12.Visible = True
            ElseIf (dom.Item("SiteProducts").Item("SearchResult").Attributes("Type").Value = "3") Then
                SearchTemplate11.Visible = False
                SearchTemplate12.Visible = False
                SpeedSearch1.Visible = False
                SearchTemplate13.Visible = True
            ElseIf (dom.Item("SiteProducts").Item("SearchResult").Attributes("Type").Value = "4") Then
                SearchTemplate11.Visible = False
                SearchTemplate12.Visible = False
                SearchTemplate13.Visible = False
                SpeedSearch1.Visible = True
            End If
            InventoryBackOrderConfirm(CType(FindControl("myhiddenfield"), HtmlInputHidden).Value)
        Catch ex As Exception
            If TypeOf ex Is Threading.ThreadAbortException Then Exit Sub
            Session("DetailError") = "Class SearchResult Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub Page_ProductAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.ProductAdded
        If (IsNothing(Session("ItemAdded")) = False) Then
            SetMessage(Message)
            HasMsg = True
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


    'Private Sub Page_EmptySearch(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.EmptySearch
    '    Try
    '        Message.Visible = True
    '        Message.Text = sender.ToString
    '    Catch err As System.Exception
    '        ErrorMessage.Visible = False
    '    End Try
    'End Sub
    Public Sub WriteCategoryName()
        Dim myStorage As CSearchStorage
        Dim myCategoryID As Long = 0
        Dim myPageTitle As String
        If Not Session("Search") Is Nothing Then
            myStorage = CType(Session("Search"), CSearchStorage)
            myCategoryID = myStorage.CategoryID
        End If
        If Not Request.QueryString("CategoryID") Is Nothing AndAlso Request.QueryString("CategoryID") <> "" Then
            myCategoryID = CType(Request.QueryString("CategoryID"), Long)
        End If
        If myCategoryID > 0 Then
            myPageTitle = New CCategories().GetCategory(myCategoryID).Name
        Else
            myPageTitle = m_objMessages.GetXMLMessage("SearchResult.aspx", "PageTitle", "Title")
        End If
        Response.Write(myPageTitle)
    End Sub
End Class
