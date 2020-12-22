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
Imports StoreFront.BusinessRule.WebRequest

Public Class Oanda
    Inherits CWebPage
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents imgConvert As System.Web.UI.WebControls.Image
    Protected WithEvents imgClose As System.Web.UI.WebControls.Image
    Protected WithEvents cmdConvert As System.Web.UI.WebControls.LinkButton
    Protected WithEvents cmdClose As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList

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

    Private m_objOanda As New COandaFXML()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            TopBanner1.SiteName = m_objMessages.GetXMLMessage("Oanda.aspx", "PageTitle", "Title")

            If (IsNothing(StoreFrontConfiguration.CurrencyISOs) = True) Then
                ' Load it
                StoreFrontConfiguration.CurrencyISOs = m_objOanda.GetISOs()
            End If

            If (IsPostBack = False) Then
                Dropdownlist1.DataTextField = "Name"
                Dropdownlist1.DataValueField = "ISO"
                Dropdownlist1.DataSource = StoreFrontConfiguration.CurrencyISOs
                Dropdownlist1.DataBind()
            End If
            imgConvert.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filename").Value
            imgClose.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Close").Attributes("Filename").Value
            cmdClose.Attributes.Add("onclick", "window.close();")
        Catch ex As Exception
            Session("DetailError") = "Class Oanda Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try

    End Sub

    Public Function MessageAlign()
        Dim objDesign As New CDesign(StoreFrontConfiguration.SiteDesign.Item("Messages"))
        Return objDesign.HorizontalAlignment
    End Function

    Private Sub Convert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConvert.Click
        Dim strISO As String

        strISO = Dropdownlist1.SelectedItem.Value

        '        If (IsNothing(Session("OandaRate")) = True) Then
        Try
            m_objOanda.GetRates(StoreFrontConfiguration.OandaID(), StoreFrontConfiguration.ISOCurrency)
            Dim objRates As COandaCurrencyISO
            For Each objRates In StoreFrontConfiguration.CurrencyISOs
                If (objRates.ISO = strISO) Then
                    Session("OandaRate") = (1 / objRates.Rate)

                    If (objRates.ISO = "BEF" Or objRates.ISO = "DEM" Or _
                        objRates.ISO = "ESP" Or objRates.ISO = "FRF" Or _
                        objRates.ISO = "IEP" Or objRates.ISO = "ITL" Or _
                        objRates.ISO = "LUF" Or objRates.ISO = "NLG" Or _
                        objRates.ISO = "ATS" Or objRates.ISO = "PTE" Or _
                        objRates.ISO = "FIM" Or objRates.ISO = "GRD") Then
                        Session("ConvertISO") = "EUR"
                    Else
                        Session("ConvertISO") = objRates.ISO
                    End If

                    m_objCustomer.UpdateOanda(CStr(1 / objRates.Rate), objRates.ISO)
                    Session("OandaChange") = True
                    Exit For
                End If
            Next
        Catch er As Exception
            Session("OandaRate") = Nothing
            Session("OandaChange") = True
        End Try
        'End If


        RegisterClientScriptBlock("myScript", "<script" _
                    & "  language='JavaScript'>opener.document.forms[0].submit();window.close();</script>")
    End Sub


End Class
