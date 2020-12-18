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

Imports StoreFront.BusinessRule.Management
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Orders
Imports System.Text.RegularExpressions
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class Shipping
    Inherits CWebPage

    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Email_RegularExpressionValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents Regularexpressionvalidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    '#SRS 6.5.3
    Protected WithEvents trnickname As HtmlTableRow

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

        Dim objAddress As Address
        Dim bAdd As Boolean

        SetPageTitle = m_objMessages.GetXMLMessage("Shipping.aspx", "PageTitle", "Title")
        SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, Nothing)
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If (m_objcustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            If (Request.QueryString("Affiliate") <> "") Then
                'Response.Redirect("CustSignInCheckout.aspx?ReturnPage=Shipping.aspx&WebID=" & Request.QueryString("WebID") & "&Affiliate=" & Request.QueryString("Affiliate"))
                Response.Redirect("CustSignInCheckout.aspx?ReturnPage=Shipping.aspx&WebID=" & MyBase.m_objCustomer.GetSessionID & "&Affiliate=" & Request.QueryString("Affiliate"))
            Else
                'Response.Redirect("CustSignInCheckout.aspx?ReturnPage=Shipping.aspx&WebID=" & Request.QueryString("WebID"))
                Response.Redirect("CustSignInCheckout.aspx?ReturnPage=Shipping.aspx&WebID=" & MyBase.m_objCustomer.GetSessionID)
            End If
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

        Try
            If (Not Page.IsPostBack) Then
                m_objXMLCart.CustomerGroup = m_objCustomer.CustomerGroup
                m_objXMLCart.LoadFromDB()
                CheckCart()
                ' If Customer does not have addresses then make drop downs invisible
                m_objAddresses = m_objCustomer.AddressList()

                If (m_objAddresses.Count = 0) Then
                    ShipAddressList.Visible = False
                    Label1.Text = "Shipment Address"
                Else
                    Label1.Text = "Select A Shipping Address From Address Book:"

                    bAdd = True
                    For Each objAddress In m_objAddresses
                        If (objAddress.ID = -1) Then
                            bAdd = False
                            Exit For
                        End If
                    Next
                    If (bAdd) Then
                        objAddress = New Address
                        objAddress.NickName = "New Address"
                        objAddress.ID = -1
                        m_objAddresses.Insert(0, objAddress)
                    End If

                    ShipAddressList.DataSource = m_objAddresses
                    ShipAddressList.DataBind()

                End If
                If Not IsNothing(Session("anonymous")) Then ' new customer
                    trnickname.Visible = False
                    Label1.Text = "Shipment Address"
                    Label1.Visible = True
                    ShipAddressList.Visible = False
                Else 'regular customer
                    trnickname.Visible = True
                    Label1.Visible = True
                End If
            End If
            imgContinue.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
            btnContinue.Attributes.Add("onclick", "return SetValidation();")

        Catch ex As Exception
            Session("DetailError") = "Class Shipping Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Function CheckFields() As Boolean
        If (Ship_NickName.Text = "" Or Ship_FirstName.Text = "" Or _
            Ship_Address1.Text = "" Or Ship_City.Text = "" Or _
            Ship_Phone.Text = "") Then

            ErrorMessage.Text = ""

            If (Ship_NickName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankSaveAs")
            End If
            If (Ship_FirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankFirstName")
            End If
            If (Ship_LastName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankLastName")
            End If
            If (Ship_Address1.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankAddress1")
            End If
            If (Ship_City.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankCity")
            End If
            If (Ship_Phone.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Shipping.aspx", "EditShipping", "BlankPhone")
            End If


            ErrorMessage.Visible = True

            Return False
        Else
            If Not isValidZip(Ship_Zip.Text) Then
                ErrorMessage.Text = "Please enter a valid Zip Code"
                ErrorMessage.Visible = True
                Return False
            End If
            If Not isValidPhone(Ship_Phone.Text) Then
                ErrorMessage.Text = "Please enter a valid Phone Number with area code."
                ErrorMessage.Visible = True
                Return False
            End If
            Return True
        End If
    End Function
    Public Function isValidZip(ByVal inputZip As String) As Boolean
        If Ship_Country.SelectedItem.Value = "US" Or Ship_Country.SelectedItem.Value = "CA" Then
            If inputZip.Trim = "" Then
                Return False
            End If
            Dim strRegex As String = ""
            Select Case Ship_Country.SelectedItem.Value
                Case "US"
                    strRegex = "^(\d{5})(-\d{4})?$"
                Case "CA"
                    strRegex = "^[a-zA-Z]\d[a-zA-Z]\s?\d[a-zA-Z]\d$"
            End Select

            Dim re As New Regex(strRegex)
            If (re.IsMatch(inputZip)) Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Public Function isValidPhone(ByVal strPhone As String) As Boolean
        Dim strRegex As String = "^1?\s*-?\s*(\d{3}|\(\s*\d{3}\s*\))\s*-?\s*\d{3}\s*-?\s*\d{4}$"

        Dim re As New Regex(strRegex)
        If (re.IsMatch(strPhone)) Then
            Return True
        Else
            Return False
        End If
        
    End Function
    Private Sub ShipAddressList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShipAddressList.SelectedIndexChanged
        Dim objList As DropDownList = sender
        Dim objAddress As Address
        Dim i As Integer
        m_objAddresses = m_objcustomer.AddressList()

        ' Filling in Address Block
        If (objList.SelectedItem.Value <> -1) Then
            For Each objAddress In m_objAddresses
                If (objAddress.ID = objList.SelectedItem.Value) Then
                    Ship_NickName.Text = objAddress.NickName
                    Ship_FirstName.Text = objAddress.FirstName
                    Ship_MI.Text = objAddress.MI
                    Ship_LastName.Text = objAddress.LastName
                    Ship_Company.Text = objAddress.Company
                    Ship_Address1.Text = objAddress.Address1
                    Ship_Address2.Text = objAddress.Address2
                    Ship_City.Text = objAddress.City
                    For i = 0 To Ship_State.Items.Count - 1
                        If Ship_State.Items(i).Value = objAddress.State Then
                            Ship_State.SelectedIndex = i
                        End If
                    Next
                    Ship_Zip.Text = objAddress.Zip
                    For i = 0 To Ship_Country.Items.Count - 1
                        If Ship_Country.Items(i).Value = objAddress.Country Then
                            Ship_Country.SelectedIndex = i
                        End If
                    Next
                    Ship_Phone.Text = objAddress.Phone
                    Ship_Fax.Text = objAddress.Fax

                    Exit For
                End If
            Next
        Else
            ' Blank all
            Ship_NickName.Text = ""
            Ship_FirstName.Text = ""
            Ship_MI.Text = ""
            Ship_LastName.Text = ""
            Ship_Company.Text = ""
            Ship_Address1.Text = ""
            Ship_Address2.Text = ""
            Ship_City.Text = ""
            Ship_Zip.Text = ""
            Ship_Phone.Text = ""
            Ship_Fax.Text = ""
        End If
    End Sub

    Public Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        'Tee 4/25/2007 check ensure customer still in valid session
        If (m_objCustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            Response.Redirect("CustSignInCheckout.aspx" & IIf(Request.QueryString("Affiliate") = "", "", "&Affiliate=" & Request.QueryString("Affiliate")))
        End If
        'end Tee
        Dim objShipAddress As AddressOrder
        Dim objAddress As Address
        Dim objitem As CCartItem
        Dim bIsResidential As Boolean = False
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        ' if anonymous checkout the set nickname as shipping address
        If Not IsNothing(Session("anonymous")) Then
            Ship_NickName.Text = "Shipping Address"
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu

        If (CheckFields() = False) Then
            Exit Sub
        End If

        ' Create Order
        Dim objOrderAddress As COrderAddress
        Dim objOrder As New COrder(m_objCustomer)

        objShipAddress = New AddressOrder
        objShipAddress.NickName = Ship_NickName.Text
        objShipAddress.FirstName = Ship_FirstName.Text
        objShipAddress.MI = Ship_MI.Text
        objShipAddress.LastName = Ship_LastName.Text
        objShipAddress.Company = Ship_Company.Text
        objShipAddress.Address1 = Ship_Address1.Text
        objShipAddress.Address2 = Ship_Address2.Text
        objShipAddress.City = Ship_City.Text
        objShipAddress.State = Ship_State.SelectedItem.Value
        objShipAddress.Zip = Ship_Zip.Text
        objShipAddress.Country = Ship_Country.SelectedItem.Value
        objShipAddress.Phone = Ship_Phone.Text
        objShipAddress.Fax = Ship_Fax.Text
        objShipAddress.EMail = m_objCustomer.Email

        m_objAddresses = m_objCustomer.AddressList()
        '#2849-ASH
        For Each objAddress In m_objAddresses
            If objShipAddress.NickName = objAddress.NickName Then
                objShipAddress.ID = objAddress.ID
                Exit For
            End If
        Next

        Try
            m_objCustomer.AddAddress(objShipAddress)
        Catch objErr As AddressException
            m_objCustomer.UpdateAddress(objShipAddress)
        End Try

        '#SRS 6.5.3
        If chk_SameBill.Checked Then
            objOrder.BillAddress = objShipAddress
        Else
            objOrder.BillAddress = Nothing
        End If

        m_objcustomer.ReLoadAddresses()

        objOrderAddress = New COrderAddress(objShipAddress, bIsResidential)

        objOrder.AddOrderAddress(objOrderAddress)

        For Each objitem In m_objXMLCart.GetCartItems()
            objOrderAddress.AddOrderItem(objitem)
        Next

        'objOrder.XMLProductAccess = m_objXMLAccess
        Dim objStoreDiscounts As New CStoreDiscounts
        objOrder.StoreDiscounts = objStoreDiscounts.GetDiscounts()

        objOrder.Coupons = m_objxmlcart.AppliedDiscounts
        'objOrder.GiftCertificates = m_objxmlcart.AppliedGiftCertificates



        Session("Order") = objOrder

        Response.Redirect("ShipSummary.aspx")
    End Sub
End Class
