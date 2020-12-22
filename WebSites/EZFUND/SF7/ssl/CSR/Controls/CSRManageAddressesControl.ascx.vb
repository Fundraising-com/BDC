Imports StoreFront.SystemBase
Imports StoreFront.SystemBase.AppException
Imports StoreFront.UITools
Partial Class CSRManageAddressesControl
    Inherits CSRWebControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents ErrorAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents MessageAlignment As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents CSRAddressBook As CSRAddressBook
    Protected WithEvents SavedAddresses As System.Web.UI.WebControls.DataList

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
        GetOrder()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If MagicAjax.MagicAjaxContext.Current.IsAjaxCall = False Then
                btnSave.Visible = False
                btnCancel.Visible = False
            CSRAddressBook.ReLoadAddresses()
        Else

        End If

        'ReturnPage.Text = "CSRMapShipments.aspx"
        btnContinue.Visible = True
        imgContinue.Visible = True


        'State.ObjXML = dom
        'Country.ObjXML = dom
        'Me.btnContinue.Visible = False
        'If InStr(Request.Url.AbsoluteUri.ToLower, StoreFrontConfiguration.SSLPath.ToLower) > 0 Then
        Me.btnContinue.Visible = True

        'End If
            
        
    End Sub

    Private Sub CSRAddressBook_AddressEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles CSRAddressBook.AddressEdit
        ' Address selected to edit
        Dim objAddress As Address
        Dim i As Integer
        AddressID.Text = CType(sender, LinkButton).CommandArgument

        For Each objAddress In CSRCustomer.AddressList
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
        Dim bReturn As Boolean = False
        Dim Errors As New ArrayList
        Dim ErrorMessage As String = ""
        If (NickName.Text = "") Then
            Errors.Add("Nickname is required.")
        End If
        If (FirstName.Text = "") Then
            Errors.Add("First Name is required.")
        End If
        If (LastName.Text = "") Then
            Errors.Add("Last Name is required.")
        End If
        If (Address1.Text = "") Then
            Errors.Add("Address is required.")
        End If
        If (City.Text = "") Then
            Errors.Add("City is required.")
        End If
        If (Phone.Text = "") Then
            Errors.Add("Phone Number is required.")
        End If
        If (State.SelectedItem.Value = "-1") Then
            Errors.Add("State is required.")
        End If
        If (Country.SelectedItem.Value = "-1") Then
            Errors.Add("Country is required.")
        End If
        If Zip.Text = "" And (Country.SelectedItem.Value = "US" Or Country.SelectedItem.Value = "CA") Then
            Errors.Add("Zip Code is required.")
        End If
        If checkPhoneNumber(Phone.Text) = False Then
            Errors.Add("Phone number is invalid.")
        End If
        If Fax.Text <> "" And checkPhoneNumber(Fax.Text) = False Then
            Errors.Add("Fax number is invalid.")
        End If
        If Errors.Count > 0 Then
            Dim str As String
            ErrorMessage = "Please correct the following:"
            For Each str In Errors
                ErrorMessage = ErrorMessage & vbCrLf & str
            Next
            MagicAjax.AjaxCallHelper.WriteAlert(ErrorMessage)
            bReturn = False
        Else
            bReturn = True
        End If

        Return bReturn
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
        MagicAjax.AjaxCallHelper.WriteAlert("Update Canceled") '& NickName.Text

        ClearFields()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objAddress As New Address

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
                CSRCustomer.AddAddress(objAddress)
                MagicAjax.AjaxCallHelper.WriteAlert("Successfully Added Address.") '& NickName.Text
                ' & objAddress.NickName
            Catch objErr As AddressException
                MagicAjax.AjaxCallHelper.WriteAlert("Successfully Updated Address.") '& NickName.Text
                CSRCustomer.UpdateAddress(objAddress)
            End Try
            CSRAddressBook.ReLoadAddresses()
            ClearFields()


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
            MagicAjax.AjaxCallHelper.WriteAlert(objErr.Message) '& NickName.Text
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objAddress As New Address

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
            CSRCustomer.AddAddress(objAddress)
            CSRAddressBook.ReLoadAddresses()
            ClearFields()

            MagicAjax.AjaxCallHelper.WriteAlert("Successfully Added Address.") '& objAddress.NickName)


            btnClear.Visible = True
            btnAdd.Visible = True
            btnSave.Visible = False
            btnCancel.Visible = False
            imgClear.Visible = True
            imgAdd.Visible = True
            imgSave.Visible = False
            imgCancel.Visible = False
        Catch objErr As AddressException
            MagicAjax.AjaxCallHelper.WriteAlert(objErr.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Response.Redirect("CSRMapShipments.aspx")
    End Sub

    Private Sub CSRAddressBook_AddressDelete(ByVal sender As Object, ByVal e As System.EventArgs) Handles CSRAddressBook.AddressDelete
        btnSave.Visible = False
        btnCancel.Visible = False
        imgSave.Visible = False
        imgCancel.Visible = False
        NewEditLabel.Text = "Add An Address"
        btnClear.Visible = True
        btnAdd.Visible = True
        imgClear.Visible = True
        imgAdd.Visible = True
        MagicAjax.AjaxCallHelper.WriteAlert("Delete Successful.")
        ClearFields()
    End Sub


    
End Class
