Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase
Imports RMSBusinessRule

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


Partial Class TaxTypes
    Inherits CWebPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            'Put user code to initialize the page here
            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True
            Dim objTaxTypes As New TaxTypeManagement
            Dim taxTypeList As New ArrayList
            taxTypeList = objTaxTypes.GetTaxTypes()
            If taxTypeList.Count = 0 Then
                listPanel.Visible = False
            End If
            DLTaxTypes.DataSource = taxTypeList
            DLTaxTypes.DataBind()
        Catch ex As Exception
            Session("DetailError") = "Class General Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim taxType As String = txtTaxType.Text.ToString
        If taxType = "" Then
            ErrorMessage.Text = "You need to enter a value for Tax Type."
            ErrorMessage.Visible = True
        End If
        Dim sfTaxType As String = SFTaxTypes.SelectedItem.ToString()
        Dim obj As New TaxTypeManagement
        Dim success As Boolean = obj.AddTaxType(taxType, sfTaxType)
        If success = False Then
            ErrorMessage.Text = "Duplicate tax type."
            ErrorMessage.Visible = True
            Exit Sub
        End If
        Response.Redirect(StoreFrontConfiguration.SSLPath & "management/TaxTypes.aspx")
    End Sub

    Public Sub DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim olinkBt As LinkButton = CType(sender, LinkButton)
        Dim uid As Long = CLng(olinkBt.CommandArgument)
        Dim objTaxType As New TaxTypeManagement
        objTaxType.DeleteItem(uid)
        Response.Redirect(StoreFrontConfiguration.SSLPath & "management/TaxTypes.aspx")
    End Sub
End Class
