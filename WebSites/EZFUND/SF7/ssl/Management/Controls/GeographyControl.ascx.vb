Imports StoreFront.BusinessRule.Management
Partial  Class GeographyControl
    Inherits System.Web.UI.UserControl

    Private objLocalization As CLocalization
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
        'Put user code to initialize the page here
        CErrorMessage.Visible = False
        SErrorMessage.Visible = False
        If IsPostBack = False Then
            objLocalization = New CLocalization()
            Call getActiveCountriesDD(objLocalization.getActiveCountriesDT)
            Call getActiveStatesDD(objLocalization.getActiveStatesDT)
            Call getInActiveCountriesDD(objLocalization.getInActiveCountriesDT)
            Call getInActiveStatesDD(objLocalization.getInActiveStatesDT)
        End If
    End Sub

    Private Sub cmdMoveCountryToInactive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveCountryToInactive.Click
        Dim Activedt As DataTable
        Dim InActivedt As DataTable
        Dim dr As DataRow
        Activedt = createActiveCountryDT()
        InActivedt = createInActiveCountryDT()
        dr = InActivedt.NewRow
        dr("ID") = Activedt.Rows(ActiveCountries.SelectedIndex).Item("ID")
        dr("Display") = Activedt.Rows(ActiveCountries.SelectedIndex).Item("Display")
        InActivedt.Rows.Add(dr)
        Activedt.Rows(ActiveCountries.SelectedIndex).Delete()
        Call getActiveCountriesDD(Activedt)
        Call getInActiveCountriesDD(InActivedt)
    End Sub

    Private Sub cmdAddCountryToActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCountryToActive.Click

        If NewCountryAbbrev.Text <> "" Or NewCountry.Text <> "" Then
            Dim Activedt As DataTable
            Dim InActivedt As DataTable
            Dim dr As DataRow
            Activedt = createActiveCountryDT()
            InActivedt = createInActiveCountryDT()
            Dim bExists As Boolean
            bExists = False

            If Activedt.Select("ID='" & NewCountryAbbrev.Text & "'").Length > 0 Then
                bExists = True
            End If
            If bExists = False Then
                If InActivedt.Select("ID='" & NewCountryAbbrev.Text & "'").Length > 0 Then
                    bExists = True
                End If
            End If
            If bExists = False Then
                dr = Activedt.NewRow
                dr("ID") = NewCountryAbbrev.Text
                dr("Display") = NewCountry.Text
                Activedt.Rows.Add(dr)
                Call getActiveCountriesDD(Activedt)
            Else
                CErrorMessage.Text = "Country abbreviation already exists."
                CErrorMessage.Visible = True
            End If
        Else
            CErrorMessage.Text = "Please enter a country abbreviation and name"
            CErrorMessage.Visible = True
        End If
    End Sub

    Private Sub cmdDeleteCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCountry.Click
        Dim InActivedt As DataTable
        'Dim dr As DataRow
        InActivedt = createInActiveCountryDT()
        InActivedt.Rows(InactiveCountries.SelectedIndex).Delete()
        Call getInActiveCountriesDD(InActivedt)
    End Sub

    Private Sub cmdMoveCountryToActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveCountryToActive.Click
        Dim Activedt As DataTable
        Dim InActivedt As DataTable
        Dim dr As DataRow
        Activedt = createActiveCountryDT()
        InActivedt = createInActiveCountryDT()
        dr = Activedt.NewRow
        dr("ID") = InActivedt.Rows(InactiveCountries.SelectedIndex).Item("ID")
        dr("Display") = InActivedt.Rows(InactiveCountries.SelectedIndex).Item("Display")
        Activedt.Rows.Add(dr)
        InActivedt.Rows(InactiveCountries.SelectedIndex).Delete()
        Call getActiveCountriesDD(Activedt)
        Call getInActiveCountriesDD(InActivedt)
    End Sub

    Private Sub cmdMoveStateToInactive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveStateToInactive.Click
        Dim Activedt As DataTable
        Dim InActivedt As DataTable
        Dim dr As DataRow
        Activedt = createActiveStateDT()
        InActivedt = createInActiveStateDT()
        dr = InActivedt.NewRow
        dr("ID") = Activedt.Rows(ActiveStates.SelectedIndex).Item("ID")
        dr("Display") = Activedt.Rows(ActiveStates.SelectedIndex).Item("Display")
        InActivedt.Rows.Add(dr)
        Activedt.Rows(ActiveStates.SelectedIndex).Delete()
        Call getActiveStatesDD(Activedt)
        Call getInActiveStatesDD(InActivedt)
    End Sub

    Private Sub cmdAddStateToActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddStateToActive.Click

        If NewStateAbbrev.Text <> "" Or NewStateName.Text <> "" Then
            Dim Activedt As DataTable
            Dim InActivedt As DataTable
            Dim dr As DataRow
            Activedt = createActiveStateDT()
            InActivedt = createInActiveStateDT()
            Dim bExists As Boolean
            bExists = False

            If Activedt.Select("ID='" & NewStateAbbrev.Text & "'").Length > 0 Then
                bExists = True
            End If
            If bExists = False Then
                If InActivedt.Select("ID='" & NewStateAbbrev.Text & "'").Length > 0 Then
                    bExists = True
                End If
            End If
            If bExists = False Then
                dr = Activedt.NewRow
                dr("ID") = NewStateAbbrev.Text
                dr("Display") = NewStateName.Text
                Activedt.Rows.Add(dr)
                Call getActiveStatesDD(Activedt)
            Else
                SErrorMessage.Text = "State abbreviation already exists."
                SErrorMessage.Visible = True
            End If
        Else
            SErrorMessage.Text = "Please enter a state abbreviation and name"
            SErrorMessage.Visible = True
        End If
    End Sub

    Private Sub cmdDeleteState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteState.Click
        Dim InActivedt As DataTable
        'Dim dr As DataRow
        InActivedt = createInActiveStateDT()
        InActivedt.Rows(InactiveStates.SelectedIndex).Delete()
        Call getInActiveStatesDD(InActivedt)
    End Sub

    Private Sub cmdMoveStateToActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveStateToActive.Click
        Dim Activedt As DataTable
        Dim InActivedt As DataTable
        Dim dr As DataRow
        Activedt = createActiveStateDT()
        InActivedt = createInActiveStateDT()
        dr = Activedt.NewRow
        dr("ID") = InActivedt.Rows(InactiveStates.SelectedIndex).Item("ID")
        dr("Display") = InActivedt.Rows(InactiveStates.SelectedIndex).Item("Display")
        Activedt.Rows.Add(dr)
        InActivedt.Rows(InactiveStates.SelectedIndex).Delete()
        Call getActiveStatesDD(Activedt)
        Call getInActiveStatesDD(InActivedt)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ActiveCountryDT As DataTable
        Dim InActiveCountryDT As DataTable
        Dim ActiveStateDT As DataTable
        Dim InActiveStateDT As DataTable
        objLocalization = New CLocalization()
        ActiveCountryDT = createActiveCountryDT()
        InActiveCountryDT = createInActiveCountryDT()
        ActiveStateDT = createActiveStateDT()
        InActiveStateDT = createInActiveStateDT()
        objLocalization.UpdateCountries(ActiveCountryDT, InActiveCountryDT)
        objLocalization.UpdateStates(ActiveStateDT, InActiveStateDT)
        objLocalization.updateTaxes()
    End Sub

    Private Function createActiveCountryDT() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem

        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))

        For Each _item In ActiveCountries.Items
            dr = dt.NewRow()
            dr(0) = _item.Value
            dr(1) = _item.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function createInActiveCountryDT() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem

        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))

        For Each _item In InactiveCountries.Items
            dr = dt.NewRow()
            dr(0) = _item.Value
            dr(1) = _item.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function createActiveStateDT() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem

        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))

        For Each _item In ActiveStates.Items
            dr = dt.NewRow()
            dr(0) = _item.Value
            dr(1) = _item.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function createInActiveStateDT() As DataTable
        Dim dt As New DataTable()
        Dim dr As DataRow
        Dim _item As ListItem

        dt.Columns.Add(New DataColumn("ID", GetType(String)))
        dt.Columns.Add(New DataColumn("Display", GetType(String)))

        For Each _item In InactiveStates.Items
            dr = dt.NewRow()
            dr(0) = _item.Value
            dr(1) = _item.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub getActiveCountriesDD(ByVal dt As DataTable)

        'Dim x As Integer

        ActiveCountries.DataSource = dt
        ActiveCountries.DataValueField = "ID"
        ActiveCountries.DataTextField = "Display"
        ActiveCountries.DataBind()

        If ActiveCountries.Items.Count > 0 Then
            ActiveCountries.SelectedIndex = 0
        End If

    End Sub

    Private Sub getInActiveCountriesDD(ByVal dt As DataTable)

        'Dim x As Integer

        InactiveCountries.DataSource = dt
        InactiveCountries.DataValueField = "ID"
        InactiveCountries.DataTextField = "Display"
        InactiveCountries.DataBind()

        If InactiveCountries.Items.Count > 0 Then
            InactiveCountries.SelectedIndex = 0
            'Tee 2/14/2008 bug 1138 fix
            cmdDeleteCountry.Enabled = True
            cmdMoveCountryToActive.Enabled = True
        Else
            cmdDeleteCountry.Enabled = False
            cmdMoveCountryToActive.Enabled = False
            'end Tee
        End If

    End Sub

    Private Sub getActiveStatesDD(ByVal dt As DataTable)

        'Dim x As Integer

        ActiveStates.DataSource = dt
        ActiveStates.DataValueField = "ID"
        ActiveStates.DataTextField = "Display"
        ActiveStates.DataBind()

        If ActiveStates.Items.Count > 0 Then
            ActiveStates.SelectedIndex = 0
        End If

    End Sub

    Private Sub getInActiveStatesDD(ByVal dt As DataTable)

        'Dim x As Integer

        InactiveStates.DataSource = dt
        InactiveStates.DataValueField = "ID"
        InactiveStates.DataTextField = "Display"
        InactiveStates.DataBind()

        If InactiveStates.Items.Count > 0 Then
            InactiveStates.SelectedIndex = 0
            'Tee 2/14/2008 bug 1138 fix
            cmdDeleteState.Enabled = True
            cmdMoveStateToActive.Enabled = True
        Else
            cmdDeleteState.Enabled = False
            cmdMoveStateToActive.Enabled = False
            'end Tee
        End If

    End Sub


End Class
