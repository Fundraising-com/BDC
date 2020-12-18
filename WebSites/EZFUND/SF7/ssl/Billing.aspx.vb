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
Imports StoreFront.BusinessRule.Orders
Imports System.text.RegularExpressions
Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException

Partial Class Billing
    Inherits CWebPage
    Protected WithEvents trnickname As HtmlTableRow
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorMessages As System.Web.UI.WebControls.Label
    Protected WithEvents traddressbook As HtmlTableRow

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
        'Tee 4/25/2007 check ensure customer still in valid session
        If (m_objCustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            Response.Redirect("CustSignInCheckout.aspx" & IIf(Request.QueryString("Affiliate") = "", "", "&Affiliate=" & Request.QueryString("Affiliate")))
        End If
        'end Tee
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
                    objAddress = objOrder.BillAddress

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
            imgContinue.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filepath").Value
            btnContinue.Attributes.Add("onclick", "return SetValidation();")
            'hide/display b'date, email and nickname whether user is new or regular cutomer
            If Not IsNothing(Session("anonymous")) Then ' new customer
                trnickname.Visible = False
                traddressbook.Visible = False
                Label1.Visible = False
                BillAddressList.Visible = False
            Else 'regular customer
                trnickname.Visible = True
                traddressbook.Visible = True
                Label1.Visible = True
                BillAddressList.Visible = True
            End If
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
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankSaveAs")
            End If
            If (Bill_FirstName.Text = "") Then
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankFirstName") & "<br>"
            End If
            If (Bill_Address1.Text = "") Then
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankAddress1") & "<br>"
            End If
            If (Bill_City.Text = "") Then
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankCity") & "<br>"
            End If
            If (Bill_Zip.Text = "") Then
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankCity") & "<br>"
            End If
            If (Bill_Phone.Text = "") Then
                ErrorMessage.Text &= m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankPhone")
            End If

            ErrorMessage.Visible = True

            Return False
        Else

            If Not isValidZip(Bill_Zip.Text) Then
                ErrorMessage.Text = "Please enter a valid Zip Code"
                ErrorMessage.Visible = True
                Return False
            End If

            If Not isValidPhone(Bill_Phone.Text) Then
                ErrorMessage.Text = "Please enter a valid Phone Number with area code."
                ErrorMessage.Visible = True
                Return False
            End If
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
        'Tee 4/25/2007 check ensure customer still in valid session
        If (m_objCustomer.IsSignedIn = False) Then
            Response.Expires = 0
            Response.Buffer = True
            Response.Clear()
            Response.Redirect("CustSignInCheckout.aspx" & IIf(Request.QueryString("Affiliate") = "", "", "&Affiliate=" & Request.QueryString("Affiliate")))
        End If
        'end Tee
        Dim objBillAddress As Address

        If (CheckFields() = False) Then
            Exit Sub
        End If

        Dim objOrder As COrder = Session("Order")

        objBillAddress = New AddressOrder
        'begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
        If Not IsNothing(Session("anonymous")) Then
            objBillAddress.NickName = "Billing Address"
        Else
            objBillAddress.NickName = Bill_NickName.Text
        End If
        'end Mod 6.9 - Anonymous Checkout v1.0 - Junu
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

    Public Function isValidZip(ByVal inputZip As String) As Boolean

        If Bill_Country.SelectedItem.Value = "US" Or Bill_Country.SelectedItem.Value = "CA" Then
            If inputZip.Trim = "" Then
                Return False
            End If
            Dim strRegex As String = ""
            Select Case Bill_Country.SelectedItem.Value
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
End Class
