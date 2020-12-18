
Imports StoreFront.Systembase
Imports StoreFront.BusinessRule

Partial  Class AffiliateAddressCtrl
    Inherits CWebControl

#Region "Class Enums"

    Private Enum AddressMode
        Add = 0
        Edit = 1
    End Enum


#End Region

#Region "Class Members"

    Private _Mode As AddressMode
    Private lngAffliateID As Long

#End Region

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

#Region "Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"

	 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lngAffliateID = Request.QueryString("Affiliate")
        Me.ErrorMessage.Visible = False
        If lngAffliateID <> 0 Then
            _Mode = AddressMode.Edit
        Else
            _Mode = AddressMode.Add
        End If



        If Not IsPostBack Then
            State.Items.Insert(0, "")
           
            If _Mode = AddressMode.Edit Then
                m_Affiliate = New CAffiliate(lngAffliateID)
               
                PopulateMe()
            Else
                State.ClearSelection()
                State.SelectedIndex = 0
                m_Affiliate = New CAffiliate(1)
            End If



            Session("Affiliate") = m_Affiliate
        Else
            m_Affiliate = Session("Affiliate")
        End If
        'cmdSave.ImageUrl = dom.Item("SiteProducts").Item("SiteImages").Item("MerchantSave").Attributes("Filepath").Value
    End Sub

#End Region    

#Region "Private Sub PopulateMe()"
    '##SUMMARY   Loads interface
    Private Sub PopulateMe()
        Dim i As Integer
        FirstName.Text = m_Affiliate.Address.FirstName
        MI.Text = m_Affiliate.Address.MI
        LastName.Text = m_Affiliate.Address.LastName
        Company.Text = m_Affiliate.Address.Company
        Address1.Text = m_Affiliate.Address.Address1
        Address2.Text = m_Affiliate.Address.Address2
        City.Text = m_Affiliate.Address.City

        For i = 0 To State.Items.Count - 1
            If State.Items(i).Value = m_Affiliate.Address.State Then
                State.SelectedIndex = i
            End If
        Next
        Zip.Text = m_Affiliate.Address.Zip
        For i = 0 To Country.Items.Count - 1
            If Country.Items(i).Value = m_Affiliate.Address.Country Then
                Country.SelectedIndex = i
            End If
        Next
        Phone.Text = m_Affiliate.Address.Phone
        Fax.Text = m_Affiliate.Address.Fax
        EMail.Text = m_Affiliate.Address.EMail
        Me.password.Text = m_Affiliate.Password
        Me.WebSite.Text = m_Affiliate.HomePage
        Me.txtFlatFee.Text = m_Affiliate.MinumimPayOut
        Me.txtPercent.Text = m_Affiliate.PayOut
    End Sub

#End Region     

#Region "Private Sub SaveMe()"
    '##SUMMARY   Saves Affiliates to DB
    Private Sub SaveMe()
        m_Affiliate.Address.FirstName = FirstName.Text
        m_Affiliate.Address.MI = MI.Text
        m_Affiliate.Address.LastName = LastName.Text
        m_Affiliate.Address.Company = Company.Text
        m_Affiliate.Address.Address1 = Address1.Text
        m_Affiliate.Address.Address2 = Address2.Text
        m_Affiliate.Address.City = City.Text
        m_Affiliate.Address.State = State.SelectedItem.Value
        m_Affiliate.Address.Zip = Zip.Text
        m_Affiliate.Address.Country = Country.SelectedItem.Value
        m_Affiliate.Address.Phone = Phone.Text
        m_Affiliate.Address.Fax = Fax.Text
        m_Affiliate.Address.EMail = EMail.Text
        If _Mode = AddressMode.Add Then
            m_Affiliate.Password = Me.password.Text
        ElseIf Me.password.Text <> "" Then
            m_Affiliate.Password = Me.password.Text
        End If
        m_Affiliate.Email = EMail.Text
        m_Affiliate.HomePage = Me.WebSite.Text
        m_Affiliate.PayOut = CDec(txtPercent.Text)
        m_Affiliate.MinumimPayOut = CDec(FormatNumber(txtFlatFee.Text, 2))
    End Sub

#End Region    

#Region "Private Function CheckNEWAffiliateInput() As Boolean"
    '##SUMMARY   Validates New Affilite Info
    '##SUMMARY   return bool
    Private Function CheckNEWAffiliateInput() As Boolean
        If EMail.Text = "" Or password.Text = "" Or _
            Me.WebSite.Text = "" Or Confirmpassword.Text = "" Then
            If (EMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankEMailAddress")
            ElseIf (password.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankPassword")
            ElseIf (WebSite.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankPassword")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankConfirmPassword")
            End If
            ErrorMessage.Visible = True
            Return False
            Exit Function
        ElseIf (password.Text <> Confirmpassword.Text) Then
            ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "PasswordConfirmEqual")
            ErrorMessage.Visible = True
            Return False
            Exit Function
        End If
        SaveMe()
        Return True
    End Function

#End Region     

#Region "Private Function CheckAffiliateInput() As Boolean"
    '##SUMMARY  Validates General input  
    '##SUMMARY   returns bool
    Private Function CheckAffiliateInput() As Boolean
        If Me.WebSite.Text = "" Then
            If (EMail.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankEMailAddress")
            ElseIf (WebSite.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankPassword")
            Else
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "BlankConfirmPassword")
            End If
            ErrorMessage.Visible = True
            Return False
            Exit Function
        ElseIf (password.Text <> "") Then
            If (password.Text <> Confirmpassword.Text) Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("affsignIn.aspx", "CreateAccount", "PasswordConfirmEqual")
                ErrorMessage.Visible = True
                Return False
                Exit Function
            End If
        End If
        SaveMe()
        Return True
    End Function

#End Region     

#Region "Private Function CheckAddressFields() As Boolean"
    '##SUMMARY Validates Address Fields
    '##SUMMARY   
    Private Function CheckAddressFields() As Boolean
        If (FirstName.Text = "" Or _
            Address1.Text = "" Or City.Text = "" Or _
                     Phone.Text = "") Then
            ErrorMessage.Text = ""
            If (FirstName.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankFirstName")
            End If
            If (Address1.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankAddress1")
            End If
            If (City.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankCity")
            End If
            If (Phone.Text = "") Then
                ErrorMessage.Text = m_objMessages.GetXMLMessage("Billing.aspx", "EditBilling", "BlankPhone")
            End If
            ErrorMessage.Visible = True
            Return False
        Else
            SaveMe()
            Return True
        End If
    End Function

#End Region    

#Region "Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) handles cmdSave.Click"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        m_Affiliate = Session("Affiliate")
        If IsNothing(m_Affiliate) = False Then

            If _Mode = AddressMode.Edit Then
                If CheckAddressFields() Then
                    If CheckAffiliateInput() Then
                        m_Affiliate.UpdateAffiliate()
                        Me.ErrorMessage.Visible = True
                        Me.ErrorMessage.Text = "Affiliate Updated"
                    End If

                End If
            ElseIf _Mode = AddressMode.Add Then

                If CheckNEWAffiliateInput() Then
                    m_Affiliate.AddAffiliate()
                    Me.ErrorMessage.Visible = True
                    Me.ErrorMessage.Text = "Affiliate Added"
                End If

            End If
        End If
    End Sub

#End Region

End Class
