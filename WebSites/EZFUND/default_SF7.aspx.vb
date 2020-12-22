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

Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule
Imports System.Xml
Imports System.Runtime.Serialization.Formatters.Binary

Public Class DefaultPage
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents EcomLink As System.Web.UI.WebControls.HyperLink
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
            SetPageTitle = m_objMessages.GetXMLMessage("default.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, Nothing, Nothing)
            If IsNothing(EcomLink) = False Then
                EcomLink.NavigateUrl = StoreFrontConfiguration.SSLPath & "Management"
            End If
        Catch ex As Exception
            Session("DetailError") = "Class Default Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try



        'Dim objcart As CCart
        'Dim objfrmt As New BinaryFormatter
        'Dim fs As New System.IO.FileStream(StoreFrontConfiguration.ServerPath & "data.dat", IO.FileMode.Create)
        'Try
        '    objcart = CType(Session("XMLShoppingCart"), CCart)
        '    objfrmt.Serialize(fs, objcart)
        'Catch ex As Exception
        '    Console.Write(ex.Message)
        '    Throw
        'Finally
        '    fs.Close()
        'End Try

        'fs = New System.IO.FileStream(StoreFrontConfiguration.ServerPath & "data.dat", IO.FileMode.Open)
        'Try
        '    objcart = CType(objfrmt.Deserialize(fs), CCart)
        'Catch ex As Exception
        '    Console.Write(ex.Message)
        '    Throw
        'Finally
        '    fs.Close()
        'End Try






    End Sub
End Class
