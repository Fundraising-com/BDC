Imports StoreFront.BusinessRule.Management
Partial  Class TaxControl
    Inherits System.Web.UI.UserControl

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
    Private objTaxManagement As CTaxManagement

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objTaxManagement = New CTaxManagement()
        If IsPostBack = False Then
            Call loadCountryTax(objTaxManagement.getCountryTaxes)
            Call loadStateTax(objTaxManagement.getStateTaxes)
            Call loadLocalTax(objTaxManagement.getLocalTaxes)
            Call LoadCountries(objTaxManagement.getCountriesWithoutTaxes)
            Call LoadStates(objTaxManagement.getStatesWithoutTaxes)
        End If
        'Put user code to initialize the page here
    End Sub

    Private Sub cmdAddLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLocal.Click

        Dim dt As DataTable
        Dim dr As DataRow
        'Dim _item As ListItem
        dt = getTaxableLocalsFromPage()
        dr = dt.NewRow
        dr.Item("DestinationID") = ""
        dr.Item("Name") = NewLocalCode.Text
        dr.Item("Rate") = NewLocalRate.Text
        dt.Rows.Add(dr)
        Call loadLocalTax(dt)
        NewLocalCode.Text = ""
        NewLocalRate.Text = ""
        Reload()
    End Sub

    Private Sub cmdAddState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddState.Click

        Dim dt As DataTable
        Dim dr As DataRow
        'Dim _item As ListItem
        dt = getTaxableStatesFromPage()
        dr = dt.NewRow
        dr.Item("DestinationID") = States.SelectedItem.Value
        dr.Item("Name") = States.SelectedItem.Text
        dr.Item("Rate") = NewStateRate.Text
        dt.Rows.Add(dr)
        Call loadStateTax(dt)
        dt = getNonTaxableStatesFromPage()
        dt.Rows(States.SelectedIndex).Delete()
        Call loadStates(dt)
        NewStateRate.Text = ""
        Reload()
    End Sub

    Private Sub cmdAddCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCountry.Click
        Dim dt As DataTable
        Dim dr As DataRow
        'Dim _item As ListItem
        dt = getTaxableCountriesFromPage()
        dr = dt.NewRow
        dr.Item("DestinationID") = Countries.SelectedItem.Value
        dr.Item("Name") = Countries.SelectedItem.Text
        dr.Item("Rate") = NewCountryRate.Text
        dt.Rows.Add(dr)
        Call loadCountryTax(dt)
        dt = getNonTaxableCountriesFromPage()
        dt.Rows(Countries.SelectedIndex).Delete()
        Call loadCountries(dt)
        NewCountryRate.Text = ""
        Reload()
    End Sub


    Public Sub DeleteLocal(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim index As Integer
        index = sender.CommandName
        Dim dt As DataTable

        dt = getTaxableLocalsFromPage()
        dt.Rows(index).Delete()
        Call loadLocalTax(dt)
        Reload()
    End Sub

    Public Sub DeleteState(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim index As Integer
        index = sender.CommandName
        Dim dt As DataTable
        Dim dr As DataRow

        dt = getNonTaxableStatesFromPage()
        dr = dt.NewRow
        dr(0) = CType(StateTax.Items(index).FindControl("StateUid"), HtmlControls.HtmlInputHidden).Value
        dr(1) = CType(StateTax.Items(index).FindControl("StateName"), HtmlControls.HtmlInputHidden).Value
        dt.Rows.Add(dr)
        Call loadStates(dt)

        dt = getTaxableStatesFromPage()
        dt.Rows(index).Delete()
        Call loadStateTax(dt)

        Reload()
    End Sub

    Public Sub DeleteCountry(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim index As Integer
        index = sender.CommandName

        Dim dt As DataTable
        Dim dr As DataRow

        dt = getNonTaxableCountriesFromPage()
        dr = dt.NewRow
        dr(0) = CType(CountryTax.Items(index).FindControl("CountryUid"), HtmlControls.HtmlInputHidden).Value
        dr(1) = CType(CountryTax.Items(index).FindControl("CountryName"), HtmlControls.HtmlInputHidden).Value
        dt.Rows.Add(dr)
        Call loadCountries(dt)

        dt = getTaxableCountriesFromPage()
        dt.Rows(index).Delete()
        Call loadCountryTax(dt)
        Reload()

    End Sub

    Private Sub loadCountries(ByVal dt As DataTable)
        If (dt.Rows.Count = 0) Then
            cmdAddCountry.Visible = False
        Else
            cmdAddCountry.Visible = True
        End If
        Countries.DataSource = dt
        Countries.DataTextField = "Name"
        Countries.DataValueField = "uid"
        Countries.DataBind()
    End Sub

    Private Sub loadStates(ByVal dt As DataTable)
        If (dt.Rows.Count = 0) Then
            cmdAddState.Visible = False
        Else
            cmdAddState.Visible = True
        End If
        States.DataSource = dt
        States.DataTextField = "Name"
        States.DataValueField = "uid"
        States.DataBind()
    End Sub

    Private Sub loadCountryTax(ByVal dt As DataTable)
        CountryTax.DataSource = dt
        CountryTax.DataBind()
    End Sub

    Private Sub loadStateTax(ByVal dt As DataTable)
        StateTax.DataSource = dt
        StateTax.DataBind()
    End Sub

    Private Sub loadLocalTax(ByVal dt As DataTable)
        LocalTax.DataSource = dt
        LocalTax.DataBind()
    End Sub

    Private Function getTaxableLocalsFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As RepeaterItem
        Dim uid As String
        Dim name As String
        Dim rate As String

        dt.Columns.Add(New DataColumn("DestinationID", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))
        dt.Columns.Add(New DataColumn("Rate", GetType(String)))

        For Each _item In LocalTax.Items
            uid = CType(_item.FindControl("LocalUid"), HtmlControls.HtmlInputHidden).Value
            name = CType(_item.FindControl("LocalName"), HtmlControls.HtmlInputHidden).Value
            rate = CType(_item.FindControl("LocalRate"), TextBox).Text
            dr = dt.NewRow()
            dr(0) = uid
            dr(1) = name
            dr(2) = rate
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function getTaxableStatesFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As RepeaterItem
        Dim uid As String
        Dim name As String
        Dim rate As String

        dt.Columns.Add(New DataColumn("DestinationID", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))
        dt.Columns.Add(New DataColumn("Rate", GetType(String)))

        For Each _item In StateTax.Items
            uid = CType(_item.FindControl("StateUid"), HtmlControls.HtmlInputHidden).Value
            name = CType(_item.FindControl("StateName"), HtmlControls.HtmlInputHidden).Value
            rate = CType(_item.FindControl("StateRate"), TextBox).Text
            dr = dt.NewRow()
            dr(0) = uid
            dr(1) = name
            dr(2) = rate
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function getTaxableCountriesFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As RepeaterItem
        Dim uid As String
        Dim name As String
        Dim rate As String

        dt.Columns.Add(New DataColumn("DestinationID", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))
        dt.Columns.Add(New DataColumn("Rate", GetType(String)))

        For Each _item In CountryTax.Items
            uid = CType(_item.FindControl("CountryUid"), HtmlControls.HtmlInputHidden).Value
            name = CType(_item.FindControl("CountryName"), HtmlControls.HtmlInputHidden).Value
            rate = CType(_item.FindControl("CountryRate"), TextBox).Text
            dr = dt.NewRow()
            dr(0) = uid
            dr(1) = name
            dr(2) = rate
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function getNonTaxableStatesFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem
        Dim uid As String
        Dim name As String
        'Dim rate As String

        dt.Columns.Add(New DataColumn("uid", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))


        For Each _item In States.Items
            uid = _item.Value
            name = _item.Text
            dr = dt.NewRow()
            dr(0) = uid
            dr(1) = name
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function getNonTaxableCountriesFromPage() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem
        Dim uid As String
        Dim name As String
        'Dim rate As String

        dt.Columns.Add(New DataColumn("uid", GetType(String)))
        dt.Columns.Add(New DataColumn("Name", GetType(String)))


        For Each _item In Countries.Items
            uid = _item.Value
            name = _item.Text
            dr = dt.NewRow()
            dr(0) = uid
            dr(1) = name
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim CountryDT As DataTable
        Dim StateDT As DataTable
        Dim LocalDT As DataTable
        CountryDT = getTaxableCountriesFromPage()
        StateDT = getTaxableStatesFromPage()
        LocalDT = getTaxableLocalsFromPage()
        objTaxManagement = New CTaxManagement()
        objTaxManagement.UpdateCountryTaxes(CountryDT)
        objTaxManagement.UpdateLocalTaxes(LocalDT)
        objTaxManagement.UpdateStateTaxes(StateDT)
        Reload()
    End Sub

    Public Sub Country_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles CountryTax.ItemCreated
        If Not (IsNothing(e.Item.DataItem)) Then
            CType(e.Item.FindControl("cmdDeleteCountry"), LinkButton).Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Delete country tax entry?" & "');")
        End If
    End Sub

    Public Sub State_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles StateTax.ItemCreated
        If Not (IsNothing(e.Item.DataItem)) Then
            CType(e.Item.FindControl("cmdDeleteState"), LinkButton).Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Delete state tax entry?" & "');")
        End If
    End Sub

    Public Sub Local_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles LocalTax.ItemCreated
        If Not (IsNothing(e.Item.DataItem)) Then
            CType(e.Item.FindControl("cmdDeleteLocal"), LinkButton).Attributes.Add("onclick", "javascript:return ConfirmCancel('" & "Delete local tax entry?" & "');")
        End If
    End Sub

    Public Sub Reload()
        loadLocalTax(getTaxableLocalsFromPage())
        loadStateTax(getTaxableStatesFromPage())
        loadCountryTax(getTaxableCountriesFromPage())
    End Sub

End Class
