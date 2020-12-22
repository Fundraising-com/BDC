using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using System.Net;
using QSPForm.Business.com.ses.ws.AddressHygieneService;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    [Serializable]
    public enum AddressHygieneResult {
        ValidUnchanged,
        ValidChanged,
        InvalidWithSuggestionList,
        InvalidWithoutSuggestionList
    }

    /// <summary>
    /// Address Validate Control
    /// </summary>
    /// <remarks>
    /// Created: June 20, 2007
    /// Ben - 08/27/2007: Migrated to .NET and adapted to Order Express
    /// </remarks>
    public partial class AddressHygiene : BaseWebFormControl {
        private const string DefaultErrorMessage = "Unknown Error";

        private static string selection = String.Empty;
        private static string sortExpression = String.Empty;
        private static SortDirection sortDirection = SortDirection.Ascending;

        public event System.EventHandler<AddressHygieneConfirmArgs> AddressHygieneConfirmed;
        public event System.EventHandler AddressHygieneSkipped;
        public event System.EventHandler<AddressHygieneConfirmArgs> SuggestionListItemSelected;
        public event System.EventHandler<AddressHygieneConfirmArgs> AddressHygieneServerConfirmed;

        #region Events

        protected override void OnInit(EventArgs e) {
            this.PreRender += new EventHandler(Page_PreRender);
            this.AddressHygienePanelSkipButton.Click += new System.Web.UI.ImageClickEventHandler(AddressHygienePanelSkipButton_Click);
            this.AddressHygienePanelConfirmButton.Click += new System.Web.UI.ImageClickEventHandler(AddressHygienePanelConfirmButton_Click);
            this.SuggestionListGridView.RowDataBound += new GridViewRowEventHandler(SuggestionListGridView_RowDataBound);
            this.SuggestionListGridView.PageIndexChanging += new GridViewPageEventHandler(SuggestionListGridView_PageIndexChanging);
            this.SuggestionListGridView.Sorting += new GridViewSortEventHandler(SuggestionListGridView_Sorting);
            base.OnInit(e);
        }

        protected void Page_PreRender(object sender, EventArgs e) {
            try {
                InitializePopupPanel();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void AddressHygienePanelSkipButton_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                if (AddressHygieneSkipped != null) {
                    AddressHygieneSkipped(this, EventArgs.Empty);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void AddressHygienePanelConfirmButton_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            DetailedAddress selectedSuggestionListItem = null;

            try {
                if (OutputAddress.SuggestionList.Length == 0) {
                    if (AddressHygieneConfirmed != null) {
                        AddressHygieneConfirmed(this, new AddressHygieneConfirmArgs(OutputAddress));
                    }
                }
                else {
                    selectedSuggestionListItem = GetSelectedSuggestionListItem();

                    if (SuggestionListItemSelected != null) {
                        SuggestionListItemSelected(this, new AddressHygieneConfirmArgs(selectedSuggestionListItem));
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void SuggestionListGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            DataBindSuggestionListItem(e.Row);
        }

        protected void SuggestionListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                SuggestionListGridView.PageIndex = e.NewPageIndex;

                SuggestionListGridView.DataSource = SuggestionListView;
                SuggestionListGridView.DataBind();

                AddressHygieneModalPopupExtender.Show();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void SuggestionListGridView_Sorting(object sender, GridViewSortEventArgs e) {
            try {
                SortSuggestionList(e.SortExpression, e.SortDirection);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Enable the suggestion list feature.
        /// </summary>
        public bool EnableSuggestionList {
            get {
                bool enableSuggestionList = true;

                if (ViewState["EnableSuggestionList"] != null) {
                    enableSuggestionList = Convert.ToBoolean(ViewState["EnableSuggestionList"]);
                }

                return enableSuggestionList;
            }
            set {
                ViewState["EnableSuggestionList"] = value;
            }
        }

        /// <summary>
        /// Address returned by DQXI
        /// </summary>
        protected OutputAddress OutputAddress {
            get {
                return (OutputAddress)ViewState["OutputAddress"];
            }
            set {
                ViewState["OutputAddress"] = value;
            }
        }

        /// <summary>
        /// Sortable view of the suggestion list.
        /// </summary>
        private List<DetailedAddress> SuggestionListView {
            get {
                if (ViewState["SuggestionListView"] == null) {
                    InitializeSuggestionListView();
                }

                return (List<DetailedAddress>)ViewState["SuggestionListView"]; ;
            }
            set {
                ViewState["SuggestionListView"] = value;
            }
        }

        private int ClientWidth {
            get {
                return Convert.ToInt32(WidthHidden.Value);
            }
        }

        private int ClientHeight {
            get {
                return Convert.ToInt32(HeightHidden.Value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the modal pop up panel.
        /// </summary>
        private void InitializePopupPanel() {
            AddressHygieneModalPopupExtender.BehaviorID = this.ClientID + "_AddressHygieneModalBehavior";
            AddressHygieneModalPopupExtender.OnOkScript = this.ClientID + "_AddressHygieneConfirm();";
            AddressHygienePanelSkipButton.OnClientClick = this.ClientID + "_AddressHygieneSkip();";
            AddressHygieneModalPopupExtender.OnCancelScript = this.ClientID + "_AddressHygieneCancel();";

            if ((ClientWidth != 0 && ClientWidth <= 800) ||
                (ClientHeight != 0 && ClientHeight <= 500) ||
                (OutputAddress != null && OutputAddress.SuggestionList.Length >= 6 &&
                ((ClientWidth != 0 && ClientWidth <= 1024) ||
                (ClientHeight != 0 && ClientHeight <= 668)))) {
                AddressHygieneModalPopupExtender.X = 10;
                AddressHygieneModalPopupExtender.Y = 15;
                AddressHygieneModalPopupExtender.RepositionMode = AjaxControlToolkit.ModalPopupRepositionMode.None;
            }
            else {
                AddressHygieneModalPopupExtender.X = -1;
                AddressHygieneModalPopupExtender.Y = -1;
                AddressHygieneModalPopupExtender.RepositionMode = AjaxControlToolkit.ModalPopupRepositionMode.RepositionOnWindowResizeAndScroll;
            }
        }

        /// <summary>
        /// Validates and cleanses an address.
        /// </summary>
        /// <param name="address">Address to be cleansed</param>
        /// <returns>Changes and status of the validation</returns>
        public AddressHygieneResult ValidateAddress(Address address) {
            QSPForm.Business.AddressSystem addressSystem = new QSPForm.Business.AddressSystem();

            try {
                OutputAddress = addressSystem.GetHygienedAddress(address, true);
            }
            catch (Exception ex) {
                QSPForm.SystemFramework.ApplicationError.ManageError(ex);
                OutputAddress = OutputAddress.CreateCleanOutputAddress(address);
            }

            return ParseOutputAddress();
        }

        /// <summary>
        /// Parses the cleansed address and displays the proper information.
        /// </summary>
        /// <returns>Changes and status of the validation</returns>
        private AddressHygieneResult ParseOutputAddress() {
            AddressHygieneResult result = AddressHygieneResult.ValidUnchanged;
            List<Address> addresses = new List<Address>();

            // Suggestion List
            if (OutputAddress.SuggestionList.Length != 0) {
                result = AddressHygieneResult.InvalidWithSuggestionList;

                addresses.Add(OutputAddress.InitialAddress);
                HygienedAddressDetailsView.DataSource = addresses;
                HygienedAddressDetailsView.DataBind();

                InitializeSuggestionListView();

                SuggestionListGridView.DataSource = SuggestionListView;
                SuggestionListGridView.DataBind();

                AddressHygieneInstructionLabel.Text = Resources.AddressHygiene.InvalidWithSuggestionListInstructionLabel;
                SuggestionListGridView.Visible = true;

                AddressHygienePanelConfirmButton.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "none";
            }
            // Invalid Address
            else if (OutputAddress.Fault != Fault.NoError) {
                result = AddressHygieneResult.InvalidWithoutSuggestionList;

                addresses.Add(OutputAddress.InitialAddress);

                HygienedAddressDetailsView.DataSource = addresses;
                HygienedAddressDetailsView.DataBind();

                AddressHygieneInstructionLabel.Text = Resources.AddressHygiene.InvalidWithoutSuggestionListInstructionLabel;
                SuggestionListGridView.Visible = false;

                AddressHygienePanelConfirmButton.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "none";
            }
            // Valid, and only County and/or PostCode2 changed
            else if ((OutputAddress.Status.ChangeStatus == ChangeStatus.County ||
           OutputAddress.Status.ChangeStatus == ChangeStatus.PostCode2 ||
           OutputAddress.Status.ChangeStatus == (ChangeStatus.County | ChangeStatus.PostCode2)) &&
           (OutputAddress.Status.FormatChangeStatus == FormatChangeStatus.None ||
           OutputAddress.Status.FormatChangeStatus == FormatChangeStatus.Country)) {
                if (AddressHygieneServerConfirmed != null) {
                    AddressHygieneServerConfirmed(this, new AddressHygieneConfirmArgs(OutputAddress));
                }
            }
            // Valid but cleansed
            else if (OutputAddress.Status.ChangeStatus != ChangeStatus.None ||
                (OutputAddress.Status.FormatChangeStatus != FormatChangeStatus.None &&
                OutputAddress.Status.FormatChangeStatus != FormatChangeStatus.Country)) {
                result = AddressHygieneResult.ValidChanged;

                List<OutputAddress> outputAddresses = new List<OutputAddress>();
                outputAddresses.Add(OutputAddress);

                HygienedAddressDetailsView.DataSource = outputAddresses;
                HygienedAddressDetailsView.DataBind();

                AddressHygieneInstructionLabel.Text = Resources.AddressHygiene.ValidChangedInstructionLabel;
                SuggestionListGridView.Visible = false;

                AddressHygienePanelConfirmButton.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "block";
            }

            if (result != AddressHygieneResult.ValidUnchanged) {
                AddressHygieneModalPopupExtender.Show();
            }

            return result;
        }

        /// <summary>
        /// Gets appropriate error messages from the resource file based on a Fault object
        /// </summary>
        /// <param name="fault">Fault to get the message for</param>
        /// <returns>Error message</returns>
        private string GetErrorMessageFromFaultCode(Fault fault) {
            string errorMessage = String.Empty;

            try {
                errorMessage = typeof(Resources.AddressHygiene).GetProperty(fault.ToString()).GetValue(null, null).ToString();
            }
            catch {
                errorMessage = DefaultErrorMessage;
            }

            return errorMessage;
        }

        /// <summary>
        /// Databinds a suggestion list item from the grid.
        /// </summary>
        /// <param name="row">Item to databind</param>
        private void DataBindSuggestionListItem(GridViewRow row) {
            if (row.RowType == DataControlRowType.Header) {
                DataBindSuggestionListHeader(row);
            }
            else if (row.RowType == DataControlRowType.DataRow) {
                DataBindSuggestionListDataItem(row);
            }
            else if (row.RowType == DataControlRowType.Pager) {
                DataBindSuggestionListPager(row);
            }
        }

        /// <summary>
        /// Databinds the suggestion list header.
        /// Note that this is a tweak to enable sorting since the
        /// modal pop up disables Javascript to keep the page read only.
        /// </summary>
        /// <param name="row">Header row</param>
        private void DataBindSuggestionListHeader(GridViewRow row) {
            DataControlFieldHeaderCell dataControlFieldHeaderCell;
            LinkButton linkButton;

            foreach (System.Web.UI.Control cell in row.Controls) {
                dataControlFieldHeaderCell = cell as DataControlFieldHeaderCell;

                if (dataControlFieldHeaderCell != null) {
                    foreach (System.Web.UI.Control control in dataControlFieldHeaderCell.Controls) {
                        linkButton = control as LinkButton;

                        if (linkButton != null) {
                            linkButton.OnClientClick = Page.ClientScript.GetPostBackEventReference(SuggestionListGridView, "Sort$" + dataControlFieldHeaderCell.ContainingField.SortExpression);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Databinds a suggestion list data item.
        /// Generates a Javascript that enables selecting an item and copying its values
        /// to the details view control while allowing data entry in the fields that will contain ranges.
        /// </summary>
        /// <param name="row">Item to databind</param>
        private void DataBindSuggestionListDataItem(GridViewRow row) {
            DetailsViewRow detailsViewRow = HygienedAddressDetailsView.Rows[HygienedAddressDetailsView.PageIndex];
            System.Web.UI.HtmlControls.HtmlInputImage selectButton = (System.Web.UI.HtmlControls.HtmlInputImage)row.FindControl("SelectButton");
            System.Web.UI.HtmlControls.HtmlInputImage detailButton = (System.Web.UI.HtmlControls.HtmlInputImage)row.FindControl("DetailButton");
            string script;

            script = "<script language=\"javascript\">\n";
            script += "  function ItemSelected" + row.ClientID + "() {\n";
            script += "    var selectedAddressIDHidden = document.getElementById(\"" + SelectedAddressIDHidden.ClientID + "\");\n";

            script += "    var primaryNumberTextBoxDetails = document.getElementById(\"" + detailsViewRow.FindControl("PrimaryNumberTextBox").ClientID + "\");\n";
            script += "    var address1LabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("Address1Label").ClientID + "\");\n";
            script += "    var address2LabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("Address2Label").ClientID + "\");\n";
            script += "    var unitNumberTextBoxDetails = document.getElementById(\"" + detailsViewRow.FindControl("UnitNumberTextBox").ClientID + "\");\n";
            script += "    var cityLabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("CityLabel").ClientID + "\");\n";
            script += "    var countyLabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("CountyLabel").ClientID + "\");\n";
            script += "    var regionLabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("RegionLabel").ClientID + "\");\n";
            script += "    var zipLabelDetails = document.getElementById(\"" + detailsViewRow.FindControl("ZipLabel").ClientID + "\");\n";

            script += "    var selectionLabelGrid = document.getElementById(\"" + row.FindControl("SelectionLabel").ClientID + "\");\n";
            script += "    var address1LabelGrid = document.getElementById(\"" + row.FindControl("Address1Label").ClientID + "\");\n";
            script += "    var primaryNumberLowLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryNumberLowLabel").ClientID + "\");\n";
            script += "    var primaryNumberHighLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryNumberHighLabel").ClientID + "\");\n";
            script += "    var primaryPrefixLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryPrefixLabel").ClientID + "\");\n";
            script += "    var primaryNameLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryNameLabel").ClientID + "\");\n";
            script += "    var primaryTypeLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryTypeLabel").ClientID + "\");\n";
            script += "    var primaryPostfixLabelGrid = document.getElementById(\"" + row.FindControl("PrimaryPostfixLabel").ClientID + "\");\n";
            script += "    var address2LabelGrid = document.getElementById(\"" + row.FindControl("Address2Label").ClientID + "\");\n";
            script += "    var unitDescriptionLabelGrid = document.getElementById(\"" + row.FindControl("UnitDescriptionLabel").ClientID + "\");\n";
            script += "    var unitNumberLowLabelGrid = document.getElementById(\"" + row.FindControl("UnitNumberLowLabel").ClientID + "\");\n";
            script += "    var unitNumberHighLabelGrid = document.getElementById(\"" + row.FindControl("UnitNumberHighLabel").ClientID + "\");\n";
            script += "    var cityLabelGrid = document.getElementById(\"" + row.FindControl("CityLabel").ClientID + "\");\n";
            script += "    var countyLabelGrid = document.getElementById(\"" + row.FindControl("CountyLabel").ClientID + "\");\n";
            script += "    var regionLabelGrid = document.getElementById(\"" + row.FindControl("RegionLabel").ClientID + "\");\n";
            script += "    var zipLabelGrid = document.getElementById(\"" + row.FindControl("ZipLabel").ClientID + "\");\n";

            script += "    if(primaryNumberHighLabelGrid.innerText != \"\")\n";
            script += "    {\n";
            script += "      primaryNumberTextBoxDetails.style.display = \"block\";\n";
            script += "      primaryNumberInitialValue = primaryNumberLowLabelGrid.innerText + \" - \" + primaryNumberHighLabelGrid.innerText;\n";
            script += "      primaryNumberTextBoxDetails.value = primaryNumberInitialValue;\n";

            script += "      address1LabelDetails.innerText = \"\";\n";

            script += "      if(primaryPrefixLabelGrid.innerText != \"\")\n";
            script += "      {\n";
            script += "        address1LabelDetails.innerText = primaryPrefixLabelGrid.innerText + \" \";\n";
            script += "      }\n";

            script += "      address1LabelDetails.innerText += primaryNameLabelGrid.innerText;\n";

            script += "      if(primaryTypeLabelGrid.innerText != \"\")\n";
            script += "      {\n";
            script += "        if(address1LabelDetails.innerText != \"\")\n";
            script += "        {\n";
            script += "          address1LabelDetails.innerText += \" \";\n";
            script += "        }\n";

            script += "        address1LabelDetails.innerText += primaryTypeLabelGrid.innerText;\n";
            script += "      }\n";

            script += "      if(primaryPostfixLabelGrid.innerText != \"\")\n";
            script += "      {\n";
            script += "        if(address1LabelDetails.innerText != \"\")\n";
            script += "        {\n";
            script += "          address1LabelDetails.innerText += \" \";\n";
            script += "        }\n";

            script += "        address1LabelDetails.innerText += primaryPostfixLabelGrid.innerText;\n";
            script += "      }\n";
            script += "    }\n";
            script += "    else\n";
            script += "    {\n";
            script += "      address1LabelDetails.innerText = address1LabelGrid.innerText;\n";
            script += "    }\n";

            script += "    if(unitNumberHighLabelGrid.innerText != \"\")\n";
            script += "    {\n";
            script += "      unitNumberTextBoxDetails.style.display = \"block\";\n";
            script += "      secondaryNumberInitialValue = unitNumberLowLabelGrid.innerText + \" - \" + unitNumberHighLabelGrid.innerText;\n";
            script += "      unitNumberTextBoxDetails.value = secondaryNumberInitialValue;\n";

            script += "      address2LabelDetails.innerText = unitDescriptionLabelGrid.innerText;\n";
            script += "    }\n";
            script += "    else\n";
            script += "    {\n";
            script += "      address2LabelDetails.innerText = address2LabelGrid.innerText;\n";
            script += "    }\n";

            script += "    cityLabelDetails.innerText = cityLabelGrid.innerText;\n";
            script += "    countyLabelDetails.innerText = countyLabelGrid.innerText;\n";
            script += "    regionLabelDetails.innerText = regionLabelGrid.innerText;\n";
            script += "    zipLabelDetails.innerText = zipLabelGrid.innerText;\n";

            if (OutputAddress.SuggestionListInformation.Status == SuggestionListStatus.LastLineSuggestionListGenerated) {
                script += "    document.getElementById(\"" + AddressHygieneInstructionLabel.ClientID + "\").innerHTML = \"" + Resources.AddressHygiene.SuggestionListItemInstructionLabel + "\";\n";
            }
            else if (OutputAddress.SuggestionListInformation.Status == SuggestionListStatus.AddressLineSuggestionListGenerated ||
                OutputAddress.SuggestionListInformation.Status == SuggestionListStatus.SecondaryAddressLineSuggestionListGenerated) {
                script += "    document.getElementById(\"" + AddressHygieneInstructionLabel.ClientID + "\").innerHTML = \"" + Resources.AddressHygiene.EditableSuggestionListItemInstructionLabel + "\";\n";
            }

            script += "    selectedAddressIDHidden.value = selectionLabelGrid.innerText;\n";
            script += "    document.getElementById(\"" + AddressHygienePanelConfirmButton.ClientID + "\").style[\"display\"] = \"block\";\n";

            script += "  }\n";
            script += "</script>\n";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ItemSelected" + row.ClientID, script);

            selectButton.Attributes["onClick"] = "ItemSelected" + row.ClientID + "();";
            detailButton.Attributes["onClick"] = "ItemSelected" + row.ClientID + "();";
        }

        /// <summary>
        /// Databinds the suggestion list pager.
        /// Note that this is a tweak to enable paging since the
        /// modal pop up disables Javascript to keep the page read only.
        /// </summary>
        /// <param name="row">Pager row</param>
        private void DataBindSuggestionListPager(GridViewRow row) {
            TableRow tableRow;
            TableCell tableCell;
            LinkButton linkButton;

            if (row.Controls.Count > 0 &&
                row.Controls[0].Controls.Count > 0 &&
                row.Controls[0].Controls[0].Controls.Count > 0) {
                tableRow = row.Controls[0].Controls[0].Controls[0] as TableRow;

                if (tableRow != null) {
                    foreach (System.Web.UI.Control cell in tableRow.Controls) {
                        tableCell = cell as TableCell;

                        if (tableCell != null) {
                            foreach (System.Web.UI.Control control in cell.Controls) {
                                linkButton = control as LinkButton;

                                if (linkButton != null) {
                                    linkButton.OnClientClick = Page.ClientScript.GetPostBackEventReference(SuggestionListGridView, "Page$" + linkButton.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the suggestion list sortable view.
        /// </summary>
        private void InitializeSuggestionListView() {
            SuggestionListView = new List<DetailedAddress>();

            foreach (DetailedAddress suggestionListItem in OutputAddress.SuggestionList) {
                SuggestionListView.Add(suggestionListItem);
            }
        }

        /// <summary>
        /// Sorts the suggestion list view.
        /// </summary>
        /// <param name="sortExpression">Field name to sort on</param>
        /// <param name="sortDirection">Direction for the sort</param>
        private void SortSuggestionList(string sortExpression, SortDirection sortDirection) {
            InitializeSuggestionListView();

            AddressHygiene.sortExpression = sortExpression;
            AddressHygiene.sortDirection = sortDirection;
            SuggestionListView.Sort(CompareDetailedAddresses);

            SuggestionListGridView.DataSource = SuggestionListView;
            SuggestionListGridView.DataBind();

            AddressHygieneModalPopupExtender.Show();
        }

        /// <summary>
        /// Compare two addresses for a sort operation.
        /// </summary>
        /// <param name="detailedAddress1">First address to compare</param>
        /// <param name="detailedAddress2">Second address to compare</param>
        /// <returns>Comparison result</returns>
        private static int CompareDetailedAddresses(DetailedAddress detailedAddress1, DetailedAddress detailedAddress2) {
            int compareResult = 0;
            PropertyInfo propertyInfo = typeof(DetailedAddress).GetProperty(sortExpression);

            if (sortDirection == SortDirection.Ascending) {
                compareResult = ((IComparable)propertyInfo.GetValue(detailedAddress1, null)).CompareTo(propertyInfo.GetValue(detailedAddress2, null));
            }
            else if (sortDirection == SortDirection.Descending) {
                compareResult = ((IComparable)propertyInfo.GetValue(detailedAddress2, null)).CompareTo(propertyInfo.GetValue(detailedAddress1, null));
            }

            return compareResult;
        }

        /// <summary>
        /// Gets the suggestion list item selected by the user along with the data
        /// entered to replace a range.
        /// </summary>
        /// <returns>Selected item</returns>
        private DetailedAddress GetSelectedSuggestionListItem() {
            DetailsViewRow detailsViewRow = null;
            DetailedAddress selectedAddress = null;

            if (SelectedAddressIDHidden.Value != String.Empty) {
                detailsViewRow = HygienedAddressDetailsView.Rows[HygienedAddressDetailsView.PageIndex];

                selection = SelectedAddressIDHidden.Value;
                selectedAddress = SuggestionListView.Find(IsSelection);

                if (selectedAddress != null) {
                    if (selectedAddress.PrimaryAddressComponents.PrimaryNumberHigh != String.Empty) {
                        selectedAddress.Address1 = ((TextBox)detailsViewRow.FindControl("PrimaryNumberTextBox")).Text;

                        if (selectedAddress.PrimaryAddressComponents.PrimaryPrefix != String.Empty) {
                            selectedAddress.Address1 += " " + selectedAddress.PrimaryAddressComponents.PrimaryPrefix;
                        }

                        selectedAddress.Address1 += " " + selectedAddress.PrimaryAddressComponents.PrimaryName;

                        if (selectedAddress.PrimaryAddressComponents.PrimaryType != String.Empty) {
                            selectedAddress.Address1 += " " + selectedAddress.PrimaryAddressComponents.PrimaryType;
                        }

                        if (selectedAddress.PrimaryAddressComponents.PrimaryPostfix != String.Empty) {
                            selectedAddress.Address1 += " " + selectedAddress.PrimaryAddressComponents.PrimaryPostfix;
                        }
                    }
                    else if (selectedAddress.SecondaryAddressComponents.UnitNumberHigh != String.Empty) {
                        selectedAddress.Address2 = selectedAddress.SecondaryAddressComponents.UnitDescription + " " +
                            ((TextBox)detailsViewRow.FindControl("UnitNumberTextBox")).Text;
                    }
                }

                SelectedAddressIDHidden.Value = String.Empty;
            }

            return selectedAddress;
        }

        /// <summary>
        /// Returns whether an item is the current selection for a search.
        /// </summary>
        /// <param name="detailedAddress">Address to check</param>
        /// <returns>True if it matches, else false</returns>
        private static bool IsSelection(DetailedAddress detailedAddress) {
            return (detailedAddress.Selection == selection);
        }

        #endregion
    }
}