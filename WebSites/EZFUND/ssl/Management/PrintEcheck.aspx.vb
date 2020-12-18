Imports StoreFront.SystemBase
Imports StoreFront.BusinessRule

'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.BusinessRule.Orders

Public Class PrintEcheck
    Inherits System.Web.UI.Page
    Protected WithEvents name As System.Web.UI.WebControls.Label
    Protected WithEvents Address As System.Web.UI.WebControls.Label
    Protected WithEvents CityStateZip As System.Web.UI.WebControls.Label
    Protected WithEvents Country As System.Web.UI.WebControls.Label
    Protected WithEvents CheckNumber As System.Web.UI.WebControls.Label
    Protected WithEvents Total As System.Web.UI.WebControls.Label
    Protected WithEvents BankName As System.Web.UI.WebControls.Label
    Protected WithEvents SignedBy As System.Web.UI.WebControls.Label
    Protected WithEvents OrderDate As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents RoutingNumber As System.Web.UI.WebControls.Label

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
            'Put user code to initialize the page here
            Dim objOrder As COrder = Session("objOrder")
            Dim BillName As String
            BillName = objOrder.BillAddress.FirstName & " " & objOrder.BillAddress.MI & " " & objOrder.BillAddress.LastName
            BillName = BillName.Replace("  ", " ")
            name.Text = BillName
            SignedBy.Text = BillName
            Address.Text = objOrder.BillAddress.Address1
            If objOrder.BillAddress.Address2.Length > 0 Then
                Address.Text = Address.Text & "<br>" & objOrder.BillAddress.Address2
            End If
            CityStateZip.Text = objOrder.BillAddress.City & " " & objOrder.BillAddress.State & ", " & objOrder.BillAddress.Zip
            Country.Text = objOrder.BillAddress.Country
            CheckNumber.Text = objOrder.OrderPayment.CheckNumber
            BankName.Text = objOrder.OrderPayment.BankName
            RoutingNumber.Text = "A" & objOrder.OrderPayment.RoutingNumber & "A  " & objOrder.OrderPayment.AccountNumber & "B" & objOrder.OrderPayment.CheckNumber 'to do, fix this field

            Total.Text = Format(objOrder.OrderTotal, "C")
            OrderDate.Text = objOrder.OrderDate
        Catch ex As Exception
            Session("DetailError") = "Class PrintECheck Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
