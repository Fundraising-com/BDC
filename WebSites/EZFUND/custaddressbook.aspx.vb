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
Imports StoreFront.SystemBase.AppException

Public Class CustAddressBook
    Inherits CWebPage
    Protected WithEvents Message As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents NickName As System.Web.UI.WebControls.TextBox
    Protected WithEvents FirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents MI As System.Web.UI.WebControls.TextBox
    Protected WithEvents LastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Address1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Address2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents City As System.Web.UI.WebControls.TextBox
    Protected WithEvents Zip As System.Web.UI.WebControls.TextBox
    Protected WithEvents Phone As System.Web.UI.WebControls.TextBox
    Protected WithEvents Fax As System.Web.UI.WebControls.TextBox
    Protected WithEvents CAddressBook1 As CAddressBook
    Protected WithEvents AddressID As System.Web.UI.WebControls.TextBox
    Protected WithEvents Company As System.Web.UI.WebControls.TextBox
    Protected WithEvents NewEditLabel As System.Web.UI.WebControls.Label
    Protected WithEvents State As UITools.SelectValControl
    Protected WithEvents Country As UITools.SelectValControl
    Protected WithEvents ReturnPage As System.Web.UI.WebControls.TextBox
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents imgAdd As System.Web.UI.WebControls.Image
    Protected WithEvents imgCancel As System.Web.UI.WebControls.Image
    Protected WithEvents imgSave As System.Web.UI.WebControls.Image
    Protected WithEvents imgClear As System.Web.UI.WebControls.Image
    Protected WithEvents imgContinue As System.Web.UI.WebControls.Image
    Protected WithEvents btnAdd As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnSave As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnClear As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents btnContinue As System.Web.UI.WebControls.LinkButton

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
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            SetPageTitle = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "PageTitle", "Title")
            SetDesign(PageTable, PageSubTable, PageCell, ErrorAlignment, MessageAlignment)

            If (Not IsPostBack) Then
                btnSave.Visible = False
                btnCancel.Visible = False
            End If

            If (Request.QueryString("ReturnPage") <> "") Then
                ReturnPage.Text = Request.QueryString("ReturnPage")
                btnContinue.Visible = True
                imgContinue.Visible = True
            End If

            If (m_objCustomer.IsSignedIn() = False) Then
                Response.Redirect("CustSignIn.aspx?ReturnPage=CustProfileMain.aspx")
            End If

            State.ObjXML = dom
            Country.ObjXML = dom
            Me.btnContinue.Visible = False
            If InStr(Request.Url.AbsoluteUri.ToLower, StoreFrontConfiguration.SSLPath.ToLower) > 0 Then
                Me.btnContinue.Visible = True
            End If
            imgAdd.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Add").Attributes("Filename").Value
            imgCancel.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Cancel").Attributes("Filename").Value
            imgSave.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Save").Attributes("Filename").Value
            imgClear.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Clear").Attributes("Filename").Value
            imgContinue.ImageUrl = "images/" & dom.Item("SiteProducts").Item("SiteImages").Item("Continue").Attributes("Filename").Value
            btnSave.Attributes.Add("onclick", "return SetValidation();")
            btnAdd.Attributes.Add("onclick", "return SetValidation();")
        Catch ex As Exception
            Session("DetailError") = "Class CustAddressBook Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    Private Sub CAddressBook1_AddressEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles CAddressBook1.AddressEdit
        ' Address selected to edit
        Dim objAddress As Address
        Dim i As Integer
        AddressID.Text = CType(sender, LinkButton).CommandArgument

        For Each objAddress In m_objcustomer.AddressList
            If (objAddress.ID = CLng(AddressID.Text)) Then
                ' Fill in values
                NickName.Text = objAddress.NickName
                FirstName.Text = objAddress.FirstName
                MI.Text = objAddress.MI
                LastName.Text = objAddress.LastName
                Company.Text = objAddress.Company
                Address1.Text = objAddress.Address1
                Address2.Text = objAddress.Address2
                City.Text = objAddress.City
                For i = 0 To State.Items.Count - 1
                    If State.Items(i).Value = objAddress.State Then
                        State.SelectedIndex = i
                    End If
                Next

                Zip.Text = objAddress.Zip
                For i = 0 To Country.Items.Count - 1
                    If Country.Items(i).Value = objAddress.Country Then
                        Country.SelectedIndex = i
                    End If
                Next

                Phone.Text = objAddress.Phone
                Fax.Text = objAddress.Fax

                ' Make save and cancel visible
                NewEditLabel.Text = "Edit Address"
                btnClear.Visible = False
                btnAdd.Visible = False
                btnSave.Visible = True
                btnCancel.Visible = True
                imgClear.Visible = False
                imgAdd.Visible = False
                imgSave.Visible = True
                imgCancel.Visible = True
                Exit For
            End If
        Next
    End Sub

    Private Sub ClearFields()
        NickName.Text = ""
        FirstName.Text = ""
        MI.Text = ""
        LastName.Text = ""
        Company.Text = ""
        Address1.Text = ""
        Address2.Text = ""
        City.Text = ""
        State.SelectedIndex = 0
        Zip.Text = ""
        Country.SelectedIndex = Country.Item_Selected
        Phone.Text = ""
        Fax.Text = ""
    End Sub

    Private Function CheckFields(ByVal strAction As String) As Boolean
        If (NickName.Text = "" Or FirstName.Text = "" Or _
            Address1.Text = "" Or City.Text = "") Then

            If (NickName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", strAction, "BlankNickName")
            ElseIf (FirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", strAction, "BlankFirstName")
            ElseIf (Address1.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", strAction, "BlankAddress1")
            ElseIf (City.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", strAction, "BlankCity")
            End If

            ErrorMessage.Visible = True

            Message.Text = ""
            Message.Visible = False

            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnSave.Visible = False
        btnCancel.Visible = False
        imgSave.Visible = False
        imgCancel.Visible = False
        NewEditLabel.Text = "Add An Address"
        btnClear.Visible = True
        btnAdd.Visible = True
        imgClear.Visible = True
        imgAdd.Visible = True
        Message.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "CancelUpdate", "CancelUpdate") '& NickName.Text
        Message.Visible = True

        ClearFields()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objAddress As New Address()

        If (CheckFields("UpdateAddress") = False) Then
            Exit Sub
        End If

        objAddress.ID = AddressID.Text
        objAddress.NickName = NickName.Text
        objAddress.FirstName = FirstName.Text
        objAddress.MI = MI.Text
        objAddress.LastName = LastName.Text
        objAddress.Company = Company.Text
        objAddress.Address1 = Address1.Text
        objAddress.Address2 = Address2.Text
        objAddress.City = City.Text
        objAddress.State = State.SelectedItem.Value
        objAddress.Zip = Zip.Text
        objAddress.Country = Country.SelectedItem.Value
        objAddress.Phone = Phone.Text
        objAddress.Fax = Fax.Text

        Try
            Try
                m_objCustomer.AddAddress(objAddress)
                Message.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "AddAddress", "Success") ' & objAddress.NickName
            Catch objErr As AddressException
                Message.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "UpdateAddress", "Success") ' & objAddress.NickName
                m_objCustomer.UpdateAddress(objAddress)
            End Try

            CAddressBook1.ReLoadAddresses()

            ClearFields()

            Message.Visible = True

            ErrorMessage.Text = ""
            ErrorMessage.Visible = False

            NewEditLabel.Text = "Add An Address"
            btnClear.Visible = True
            btnAdd.Visible = True
            btnSave.Visible = False
            btnCancel.Visible = False
            imgClear.Visible = True
            imgAdd.Visible = True
            imgSave.Visible = False
            imgCancel.Visible = False
        Catch objErr As AddressException
            Message.Text = ""
            Message.Visible = False
            ErrorMessage.Text = objErr.Message
            ErrorMessage.Visible = True
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objAddress As New Address()

        If (CheckFields("AddAddress") = False) Then
            Exit Sub
        End If

        objAddress.NickName = NickName.Text
        objAddress.FirstName = FirstName.Text
        objAddress.MI = MI.Text
        objAddress.LastName = LastName.Text
        objAddress.Company = Company.Text
        objAddress.Address1 = Address1.Text
        objAddress.Address2 = Address2.Text
        objAddress.City = City.Text
        objAddress.State = State.SelectedItem.Value
        objAddress.Zip = Zip.Text
        objAddress.Country = Country.SelectedItem.Value
        objAddress.Phone = Phone.Text
        objAddress.Fax = Fax.Text

        Try
            m_objCustomer.AddAddress(objAddress)

            CAddressBook1.ReLoadAddresses()

            ClearFields()

            Message.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "AddAddress", "Success") '& objAddress.NickName
            Message.Visible = True

            ErrorMessage.Text = ""
            ErrorMessage.Visible = False

            btnClear.Visible = True
            btnAdd.Visible = True
            btnSave.Visible = False
            btnCancel.Visible = False
            imgClear.Visible = True
            imgAdd.Visible = True
            imgSave.Visible = False
            imgCancel.Visible = False
        Catch objErr As AddressException
            Message.Text = ""
            Message.Visible = False
            ErrorMessage.Text = objErr.Message
            ErrorMessage.Visible = True
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Response.Redirect(StoreFrontConfiguration.SSLPath & "MultiShip.aspx?Return=1")
    End Sub

    Private Sub CAddressBook1_AddressDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles CAddressBook1.AddressDelete
        btnSave.Visible = False
        btnCancel.Visible = False
        imgSave.Visible = False
        imgCancel.Visible = False
        NewEditLabel.Text = "Add An Address"
        btnClear.Visible = True
        btnAdd.Visible = True
        imgClear.Visible = True
        imgAdd.Visible = True
        Message.Text = m_objMessages.GetXMLMessage("CustAddressBook.aspx", "DeleteAddress", "DeleteAddress") '& NickName.Text
        Message.Visible = True
        ClearFields()
    End Sub

End Class
