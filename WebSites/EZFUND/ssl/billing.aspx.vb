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

Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders

Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Public Class Billing
    Inherits CWebPage

    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Bill_MI As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_LastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnContinue As System.Web.UI.WebControls.LinkButton
    Protected WithEvents imgContinue As System.Web.UI.WebControls.Image
    Protected WithEvents Bill_NickName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_FirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Company As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Address1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Address2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_City As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Zip As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Phone As System.Web.UI.WebControls.TextBox
    Protected WithEvents Bill_Fax As System.Web.UI.WebControls.TextBox
    Protected WithEvents BillAddressList As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Bill_State As UITools.SelectValControl
    Protected WithEvents Bill_Country As UITools.SelectValControl
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorMessages As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl

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

    Private m_objAddresses As ArrayList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim objAddress As Address
            Dim i As Integer
            Dim bAdd As Boolean
            SetPageTitle = m_objMessages.GetXMLMessage("Billing.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)

            If (Request.QueryString("Edit") = "True") Then
                If (Not IsPostBack) Then
                    m_objAddresses = m_objCustomer.AddressList()

                    objAddress = New Address()
                    objAddress.NickName = "New Address"
                    objAddress.ID = -1
                    m_objAddresses.Insert(0, objAddress)

                    BillAddressList.DataSource = m_objAddresses
                    BillAddressList.DataBind()

                    Dim objOrder As COrder = Session("Order")
                    Dim objItem As ListItem
                    objAddress = objorder.BillAddress

                    For Each objItem In BillAddressList.Items
                        If (objItem.Text = objAddress.NickName) Then
                            objItem.Selected = True
                            Bill_NickName.Text = objAddress.NickName
                            Bill_FirstName.Text = objAddress.FirstName
                            Bill_MI.Text = objAddress.MI
                            Bill_LastName.Text = objAddress.LastName
                            Bill_Company.Text = objAddress.Company
                            Bill_Address1.Text = objAddress.Address1
                            Bill_Address2.Text = objAddress.Address2
                            Bill_City.Text = objAddress.City
                            For i = 0 To Bill_State.Items.Count - 1
                                If Bill_State.Items(i).Value = objAddress.State Then
                                    Bill_State.SelectedIndex = i
                                End If
                            Next

                            Bill_Zip.Text = objAddress.Zip
                            For i = 0 To Bill_Country.Items.Count - 1
                                If Bill_Country.Items(i).Value = objAddress.Country Then
                                    Bill_Country.SelectedIndex = i
                                End If
                            Next
                            Bill_Phone.Text = objAddress.Phone
                            Bill_Fax.Text = objAddress.Fax
                            Exit For
                        End If
                    Next
                End If
            Else
                If (Not Page.IsPostBack) Then
                    ' If Customer does not have addresses then make drop downs invisible
                    m_objAddresses = m_objCustomer.AddressList()

                    bAdd = True
                    For Each objAddress In m_objAddresses
                        If (objAddress.ID = -1) Then
                            bAdd = False
                            Exit For
                        End If
                    Next
                    If (bAdd) Then
                        objAddress = New Address()
                        objAddress.NickName = "New Address"
                        objAddress.ID = -1
                        m_objAddresses.Insert(0, objAddress)
                    End If

                    BillAddressList.DataSource = m_objAddresses
                    BillAddressList.DataBind()

                    Dim objOrder As COrder = Session("Order")
                    Dim objItem As ListItem
                    objAddress = objOrder.OrderAddresses(0).Address

                    For Each objItem In BillAddressList.Items
                        If (objItem.Text = objAddress.NickName) Then
                            objItem.Selected = True
                            Bill_NickName.Text = objAddress.NickName
                            Bill_FirstName.Text = objAddress.FirstName
                            Bill_MI.Text = objAddress.MI
                            Bill_LastName.Text = objAddress.LastName
                            Bill_Company.Text = objAddress.Company
                            Bill_Address1.Text = objAddress.Address1
                            Bill_Address2.Text = objAddress.Address2
                            Bill_City.Text = objAddress.City
                            For i = 0 To Bill_State.Items.Count - 1
                                If Bill_State.Items(i).Value = objAddress.State Then
                                    Bill_State.SelectedIndex = i
                                End If
                            Next

                            Bill_Zip.Text = objAddress.Zip
                            For i = 0 To Bill_Country.Items.Count - 1
                                If Bill_Country.Items(i).Value = objAddress.Country Then
                                    Bill_Country.SelectedIndex = i
                                End If
                            Next
                            Bill_Phone.Text = objAddress.Phone
                            Bill_Fax.Text = objAddress.Fax
                            Exit For
                        End If
                    Next
                End If
            End If
            imgContinue.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filename").Value
            btnContinue.Attributes.Add("onclick", "return SetValidation();")
        Catch ex As Exception
            Session("DetailError") = "Class Billing Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Function CheckFields() As Boolean
        If (Bill_NickName.Text = "" Or Bill_FirstName.Text = "" Or _
            Bill_Address1.Text = "" Or Bill_City.Text = "" Or _
            Bill_Phone.Text = "") Then

            ErrorMessage.Text = ""

            If (Bill_NickName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankSaveAs")
            End If
            If (Bill_FirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankFirstName")
            End If
            If (Bill_Address1.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankAddress1")
            End If
            If (Bill_City.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankCity")
            End If
            If (Bill_Phone.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankPhone")
            End If

            ErrorMessage.Visible = True

            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BillAddressList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillAddressList.SelectedIndexChanged
        Dim objList As DropDownList = sender
        Dim objAddress As Address
        Dim i As Integer
        m_objAddresses = m_objcustomer.AddressList()

        ' Filling in Address Block
        If (objList.SelectedItem.Value <> -1) Then
            For Each objAddress In m_objAddresses
                If (objAddress.ID = objList.SelectedItem.Value) Then
                    Bill_NickName.Text = objAddress.NickName
                    Bill_FirstName.Text = objAddress.FirstName
                    Bill_MI.Text = objAddress.MI
                    Bill_LastName.Text = objAddress.LastName
                    Bill_Company.Text = objAddress.Company
                    Bill_Address1.Text = objAddress.Address1
                    Bill_Address2.Text = objAddress.Address2
                    Bill_City.Text = objAddress.City
                    For i = 0 To Bill_State.Items.Count - 1
                        If Bill_State.Items(i).Value = objAddress.State Then
                            Bill_State.SelectedIndex = i
                        End If
                    Next
                    Bill_Zip.Text = objAddress.Zip
                    For i = 0 To Bill_Country.Items.Count - 1
                        If Bill_Country.Items(i).Value = objAddress.Country Then
                            Bill_Country.SelectedIndex = i
                        End If
                    Next
                    Bill_Phone.Text = objAddress.Phone
                    Bill_Fax.Text = objAddress.Fax
                    Exit For
                End If
            Next
        Else
            ' Blank all
            Bill_NickName.Text = ""
            Bill_FirstName.Text = ""
            Bill_MI.Text = ""
            Bill_LastName.Text = ""
            Bill_Company.Text = ""
            Bill_Address1.Text = ""
            Bill_Address2.Text = ""
            Bill_City.Text = ""
            Bill_Zip.Text = ""
            Bill_Phone.Text = ""
            Bill_Fax.Text = ""
        End If
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim objBillAddress As Address

        If (CheckFields() = False) Then
            Exit Sub
        End If

        Dim objOrder As COrder = Session("Order")

        objBillAddress = New AddressOrder()
        objBillAddress.NickName = Bill_NickName.Text
        objBillAddress.FirstName = Bill_FirstName.Text
        objBillAddress.MI = Bill_MI.Text
        objBillAddress.LastName = Bill_LastName.Text
        objBillAddress.Company = Bill_Company.Text
        objBillAddress.Address1 = Bill_Address1.Text
        objBillAddress.Address2 = Bill_Address2.Text
        objBillAddress.City = Bill_City.Text
        objBillAddress.State = Bill_State.SelectedItem.Value
        objBillAddress.Zip = Bill_Zip.Text
        objBillAddress.Country = Bill_Country.SelectedItem.Value
        objBillAddress.Phone = Bill_Phone.Text
        objBillAddress.Fax = Bill_Fax.Text
        objBillAddress.EMail = m_objCustomer.Email
        objBillAddress.ID = BillAddressList.SelectedItem.Value

        Try
            m_objCustomer.AddAddress(objBillAddress)
        Catch objErr As AddressException
            m_objCustomer.UpdateAddress(objBillAddress)
        End Try

        m_objcustomer.ReLoadAddresses()

        objOrder.BillAddress = objBillAddress

        Session("Order") = objOrder

        Response.Redirect("Payment.aspx")
    End Sub

End Class
