using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using System.Collections;
using System.Text;
using System.Configuration;
using dataDef = QSPForm.Common.DataDef.AccountTransferAccountTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountList.
    /// </summary>
    public partial class AccountTransferControl : BaseWebSubFormControl {

        #region internal variables

        // Constants
        private const string DEFAULT_SORT = AccountTransferAccountTable.FLD_PKID + " DESC";
        private const string FILTER_STATE = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_FSM_DISPLAY_MODE = "Filter_FSM_DisplayMode";
        private const string FILTER_STATUS_CATEGORY = "Filter_Status";
        public const string PARAM_PROG = "ProgType";

        // Variables
        protected DataView DVAccount;
        protected dataDef dTblAccount = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();
        protected string CheckItemIds = String.Empty;
        protected string CheckedFMIds = string.Empty;
        protected string ChkdItems = String.Empty;
        protected string ChkBxIndex = String.Empty;
        protected string FMIDIndex = string.Empty;
        protected bool BxChkd = false;
        protected int AccountCounter = 0;
        protected ArrayList CheckedItems;
        protected ArrayList CheckedItemsCheckAll;
        protected ArrayList CheckedFMs;
        protected string[] AccountTransferResults;
        protected string[] FMIdsResults;
        // private string[] fmAccountTransferUsers;
        // private string ConfigAccountTransferUsers = String.Empty;

        // Properties
        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
        public Label SearchName {
            get {
                return QSPFormSearchModule.LabelSearchByAlpha;
            }
            set {
                QSPFormSearchModule.LabelSearchByAlpha = value;
            }
        }
        /// <summary>
        /// FormID for WFC spring shutdown
        /// </summary>
        public string[] FMAccountTransferUsers {
            get {
                try {
                    string ConfigAccountTransferUsers = String.Empty;
                    if (ConfigurationManager.AppSettings["FMTransferAccountUser"] != null) {
                        ConfigAccountTransferUsers = Convert.ToString(ConfigurationManager.AppSettings["FMTransferAccountUser"]);
                    }

                    if (ConfigAccountTransferUsers.Length > 0) {
                        return ConfigAccountTransferUsers.Split(',');
                    }
                    else {
                        return null;
                    }
                }
                catch (Exception ex) {
                    Page.SetPageError(ex);
                    return null;
                }
            }
        }

        #endregion

        #region Local events

        protected void Page_Load(object sender, System.EventArgs e) {
            AddJavascript();
            btnTransferAccount.OnClientClick = "return AccountNumber();";

            #region Old code

            //  AddJavascript();
            ////  SetJScriptForTransferButton();
            // // btnTransferAccount.OnClientClick = "return AccountNumber();";
            //  btnTransferAccount.OnClientClick = "return AccountNumber();";
            //  // Put user code to initialize the page here
            //  if (!IsPostBack)
            //  {
            //      //tblFilterStatusCategory.Visible = (Page.Role > AuthSystem.ROLE_FM);
            //     // trFieldSupportFilterOption.Visible = (Page.Role > AuthSystem.ROLE_FM);

            //  }

            #endregion
        }
        protected void Page_PreRender(object sender, System.EventArgs e) {
            // SetJScriptForTransferButton();
        }

        protected void btnCheckAll_Click(object sender, ImageClickEventArgs e) {
            // Clears session variables
            ClearSessions();

            base.BindGrid();
            DVAccount = new DataView();
            DVAccount.Table = dTblAccount;
            int i = DVAccount.Count;

            CheckedItemsCheckAll = new ArrayList();
            CheckedFMs = new ArrayList();

            foreach (DataRowView drv in DVAccount) {
                CheckedItemsCheckAll.Add(drv["account_id"].ToString());
                CheckedFMs.Add(drv["fm_id"].ToString() + "+" + drv["account_id"].ToString());
            }

            this.hidbox.Value = CheckedItemsCheckAll.Count.ToString();

            Session["CheckAll"] = true;
            Session["CheckedItemsCheckAll"] = CheckedItemsCheckAll;
            Session["CheckedFMIDs"] = CheckedFMs;

            if (CheckedItemsCheckAll != null) {
                foreach (DataGridItem dgItems in dtgAccount.Items) {
                    ChkBxIndex = dtgAccount.DataKeys[dgItems.ItemIndex].ToString();

                    //Repopulate GridView with items found in Session   
                    if (CheckedItemsCheckAll.Contains(ChkBxIndex)) {
                        CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                        checkBox.Checked = true;
                    }
                }
            }
        }
        protected void btnUnCheckAll_Click(object sender, ImageClickEventArgs e) {
            this.hidbox.Value = "0";

            // Clears session variables
            ClearSessions();

            // Uncheck all checkboxes in the grid
            foreach (DataGridItem dgItems in dtgAccount.Items) {
                CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                if (checkBox.Checked == true) {
                    checkBox.Checked = false;
                }
            }

            // TODO: Check if this is necesary
            // Populate check boxes
            // Since we unchecked all check boxes, this should not be necesary
            RePopulateCheckBoxes();
        }
        protected void btnTransferAccount_Click(object sender, ImageClickEventArgs e) {
            if (Page.IsValid) {
                QSPForm.Business.FMAccountTransferSystem FMAccountSys = new QSPForm.Business.FMAccountTransferSystem();
                GetCheckBoxValues();

                if (BxChkd == true) {
                    RePopulateCheckBoxes();

                    //bool result = FMAccountSys.UpdateAccountsBYFMID(CheckItemIds, CheckedFMIds, txtFMID1.Text, txtFMID2.Text, Convert.ToDateTime(txtDate.Text), txtReason.Text, this.Page.UserID);

                    ClearSessions();

                    //Reset GridView to top                
                    dtgAccount.CurrentPageIndex = 0;
                    this.BindGrid();
                    txtFSMName.Text = "";
                    txtFSMID.Text = "";
                    txtDate.Text = "";
                    txtFMID1.Text = "";
                    txtFMID2.Text = "";
                    txtFMName1.Text = "";
                    txtFMName2.Text = "";
                    txtReason.Text = "";
                    this.hidbox.Value = "0";
                }
            }
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            this.hidbox.Value = "0";

            #region Old code

            //ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;
            //ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;
            //ClearSessions();
            //dtgAccount.CurrentPageIndex = 0;

            #endregion
        }

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;

            DVAccount = new DataView(dTblAccount);
            this.DataSource = DVAccount;
            this.MainDataGrid = dtgAccount;

            dtgAccount.DataKeyField = AccountTransferAccountTable.FLD_PKID;

            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }

        protected void AddJavascript() {
            txtFMName1.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID1.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM1, txtFMID1, txtFMName1, "FMSelector.aspx", "FMSelector", 0, 0, null);
            txtFMName2.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID2.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM2, txtFMID2, txtFMName2, "FMSelector.aspx", "FMSelector", 0, 0, null);

            #region Old code

            //txtFSMID.Attributes.Add("onfocus", "javascript:window.focus();");
            //txtFSMName.Attributes.Add("onfocus", "javascript:window.focus();");
            //clsUtil.SetJScriptForOpenSelector(SelectFMButton, txtFSMID, txtFSMName, "FMSelector.aspx", "FMSelector", 0, 0, null);

            #endregion
        }

        private void SetJScriptForTransferButton() {
            // Set the button script
            // TODO: This should be done in the init control script
            btnTransferAccount.OnClientClick = "return AccountNumber();";

            // Make sure the check box values are populated
            // TODO: Check if this is really necesary
            GetCheckBoxValues();
            RePopulateCheckBoxes();

            // Get total accounts
            int TotalAccounts = AccountCounter;

            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	function AccountNumber() {	\n");
            strBuild.Append("	var ans;	\n");
            strBuild.Append("	        alert('No of Accounts " + AccountCounter.ToString() + " !'); \n");
            strBuild.Append("	ans = window.confirm('Is it Yes ?');	\n");
            strBuild.Append("            return ans;\n");
            strBuild.Append("       }\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            // this.Page.ClientScript.GetPostBackEventReference(this.btnTransferAccount, "OnClick");
            this.Page.RegisterClientScriptBlock("AccountNumber", strBuild.ToString());
        }

        private void CheckUser() {
            bool Allow = false;

            foreach (string userAllowed in FMAccountTransferUsers) {
                if (this.Page.UserID == Convert.ToInt32(userAllowed)) {
                    Allow = true;
                    break;
                }
            }

            if (Allow == false) {
                if (this.Page.Role == AuthSystem.ROLE_SUPER_USER) {

                }
                else {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void ClearSessions() {
            if (Session["CheckedItemsCheckAll"] != null) {
                Session.Remove("CheckedItemsCheckAll");
            }
            if (Session["CheckAll"] != null) {
                Session.Remove("CheckAll");
            }
            if (Session["CheckedItems"] != null) {
                //Clear all Session values    
                Session.Remove("CheckedItems");
            }
            if (Session["CheckedFMIDs"] != null) {
                //Clear all Session values    
                Session.Remove("CheckedFMIDs");
            }
        }

        protected void GetCheckBoxValues() {
            // Create new blank lists
            CheckedItems = new ArrayList();
            CheckedItemsCheckAll = new ArrayList();
            CheckedFMs = new ArrayList();

            // Iterate each grid item
            foreach (DataGridItem dgItems in dtgAccount.Items) {
                ChkBxIndex = dtgAccount.DataKeys[dgItems.ItemIndex].ToString();
                //FMIDIndex =  dgItems.Cells[4].ToString() +"+" + ChkBxIndex;

                CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                Label FMIDlabel = (Label)dgItems.FindControl("lblFMID");

                FMIDIndex = FMIDlabel.Text + "+" + ChkBxIndex;

                #region Get lists from session if possible

                if (Session["CheckedItems"] != null) {
                    CheckedItems = (ArrayList)Session["CheckedItems"];
                }

                if (Session["CheckedItemsCheckAll"] != null) {
                    CheckedItemsCheckAll = (ArrayList)Session["CheckedItemsCheckAll"];
                }

                if (Session["CheckedFMIDs"] != null) {
                    CheckedFMs = (ArrayList)Session["CheckedFMIDs"];
                }

                #endregion

                if (checkBox.Checked == true) {
                    #region Add value to checked list

                    BxChkd = true;

                    if (!CheckedItems.Contains(ChkBxIndex)) {
                        CheckedItems.Add(ChkBxIndex.ToString());
                    }

                    if (!CheckedItemsCheckAll.Contains(ChkBxIndex)) {
                        CheckedItemsCheckAll.Add(ChkBxIndex.ToString());
                        AccountCounter = AccountCounter + 1;
                    }

                    if (!CheckedFMs.Contains(FMIDIndex)) {
                        CheckedFMs.Add(FMIDIndex.ToString());
                    }

                    #endregion
                }
                else {
                    #region Remove value from checked list

                    //Remove value from Session when unchecked    
                    CheckedItems.Remove(ChkBxIndex.ToString());
                    CheckedItemsCheckAll.Remove(ChkBxIndex.ToString());
                    CheckedFMs.Remove(FMIDIndex.ToString());

                    #endregion
                }
            }

            #region Update lists in session

            //Update Session with the list of checked items   
            Session["CheckedItems"] = CheckedItems;

            //Update Session with the list of checked items check All  
            Session["CheckedItemsCheckAll"] = CheckedItemsCheckAll;

            //Update Session with the list of checked items FMIDs 
            Session["CheckedFMIDs"] = CheckedFMs;

            #endregion

        }
        protected void RePopulateCheckBoxes() {
            CheckedItems = new ArrayList();
            CheckedItems = (ArrayList)Session["CheckedItems"];

            CheckedItemsCheckAll = new ArrayList();
            CheckedItemsCheckAll = (ArrayList)Session["CheckedItemsCheckAll"];

            CheckedFMs = new ArrayList();
            CheckedFMs = (ArrayList)Session["CheckedFMIDs"];

            if (CheckedItems != null) {
                foreach (DataGridItem dgItems in dtgAccount.Items) {
                    ChkBxIndex = dtgAccount.DataKeys[dgItems.ItemIndex].ToString();

                    //Repopulate GridView with items found in Session   
                    if (CheckedItems.Contains(ChkBxIndex)) {
                        CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                        checkBox.Checked = true;
                    }
                }
            }

            if ((Session["CheckAll"] != null)) {
                if (CheckedItemsCheckAll != null) {
                    foreach (DataGridItem dgItems in dtgAccount.Items) {
                        ChkBxIndex = dtgAccount.DataKeys[dgItems.ItemIndex].ToString();

                        //Repopulate GridView with items found in Session   
                        if (CheckedItemsCheckAll.Contains(ChkBxIndex)) {
                            CheckBox checkBox = (CheckBox)dgItems.FindControl("AccountCheckBox");
                            checkBox.Checked = true;
                        }
                    }
                }
            }

            if (CheckedItemsCheckAll != null) {
                //Copy ArrayList to a new array       
                AccountTransferResults = (string[])CheckedItemsCheckAll.ToArray(typeof(string)); // ToArray(GetType(String));
                AccountCounter = AccountTransferResults.Length;

                //Concatenate ArrayList with comma to properly send for deletion     
                CheckItemIds = String.Join(",", AccountTransferResults);
            }

            if (CheckedFMs != null) {
                //Copy ArrayList to a new array       
                FMIdsResults = (string[])CheckedFMs.ToArray(typeof(string)); // ToArray(GetType(String));

                //Concatenate ArrayList with comma to properly send for deletion     
                CheckedFMIds = String.Join(",", FMIdsResults);
            }
        }

        #region Methods overrided from base class

        /// <summary>
        /// Fills the options for the grid filter
        /// </summary>
        /// <remarks>
        /// The base implementation does not provide any functionality, so all logic lies within this method.
        /// </remarks>
        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetProgramTypeDropDownList(ddlProgramType, true);

            string sProgTypeID = "";  //Set To ALL as Default
            if (Request[PARAM_PROG] != null) {
                sProgTypeID = Request[PARAM_PROG].ToString();
            }

            ListItem lstItem = ddlProgramType.Items.FindByValue(sProgTypeID);
            if (lstItem != null) {
                ddlProgramType.ClearSelection();
                lstItem.Selected = true;
            }

            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProgramType.SelectedItem.Value);

            clsUtil.SetUSStateDropDownList(ddlState, true);
            ViewState[FILTER_STATE] = ddlState.SelectedItem.Value;

            //clsUtil.SetAccountStatusCategoryDropDownList(ddlStatusCategory, true);
            //ViewState[FILTER_STATUS_CATEGORY] = ddlStatusCategory.SelectedItem.Value;

            // It's 0 by default
            // ViewState[FILTER_FSM_DISPLAY_MODE] = ddlFSMDisplayMode.SelectedItem.Value;

            base.FillFilter();
        }
        /// <summary>
        /// Loads the grid with data
        /// </summary>
        /// <remarks>
        /// The base implementation does not provide any functionality, so all logic lies within this method.
        /// </remarks>
        protected override void LoadDataSourceGrid() {
            QSPForm.Business.FMAccountTransferSystem accSys = new QSPForm.Business.FMAccountTransferSystem();

            #region Get search criteria

            string sCriteria = this.dtgAccount.FilterExpression;

            switch (this.dtgAccount.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            #endregion

            string SubdivisionCode = ViewState[FILTER_STATE].ToString();
            int ProgramType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            int StatusCategoryID = Convert.ToInt32(ViewState[FILTER_STATUS_CATEGORY]);

            string sFMID = "";
            string sFMName = "";
            int FSM_DisplayMode = Convert.ToInt32(ViewState[FILTER_FSM_DISPLAY_MODE]);

            //If the Display mode is not All QSP Accounts
            if ((Page.Role == AuthSystem.ROLE_FM) && (FSM_DisplayMode < 3)) {
                sFMID = Page.FMID;
                //blnAllFMReportedTo = chkReportedTo.Checked;
            }
            if (Page.Role > AuthSystem.ROLE_FM) {
                if (txtFSMID.Text.Trim().Length > 0) {
                    if (txtFSMID.Text.Trim().Length > 4) {
                        sFMID = txtFSMID.Text.Trim().Substring(0, 4);
                    }
                    else {
                        sFMID = txtFSMID.Text.Trim().Substring(0, txtFSMID.Text.Trim().Length);
                    }
                }

                if (txtFSMName.Text.Trim().Length > 0) {
                    sFMName = "%" + txtFSMName.Text.Trim().Replace(" ", "%") + "%";
                }
            }

            dTblAccount = accSys.SelectAll_Search(this.dtgAccount.SearchMode, sCriteria, ProgramType, SubdivisionCode, sFMID, FSM_DisplayMode, StatusCategoryID, sFMName);

            DVAccount = new DataView();
            DVAccount.Table = dTblAccount;

            //Always do a secondary sort on Account_Name			
            DVAccount.Sort = this.dtgAccount.SortExpression + ", " + AccountTransferAccountTable.FLD_PKID;

            lblTotal.Text = "Number of Account(s) : " + DVAccount.Count.ToString();
        }
        /// <summary>
        /// Initializes components for the page
        /// </summary>
        /// <remarks>
        /// This method expands the base implementation.
        /// </remarks>
        /// <param name="e">The initialization event arguments</param>
        protected override void OnInit(EventArgs e) {
            // Check user has a valid session
            CheckUser();

            // Initialize local components
            InitControl();

            // Set up search handler
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);

            // Initialize base components
            base.OnInit(e);
        }
        /// <summary>
        /// Loads the selected grid page
        /// </summary>
        /// <remarks>
        /// This method expands the base implementation by mantaining the check box values in the grid.
        /// </remarks>
        /// <param name="e">The data grid page changed event argunemt</param>
        protected override void OnPageIndexChanged(DataGridPageChangedEventArgs e) {
            // Save the check box values
            GetCheckBoxValues();

            // Load datagrid page
            base.OnPageIndexChanged(e);

            // Populate checkbox values in the new page
            RePopulateCheckBoxes();
        }
        /// <summary>
        /// Sorts the grid's values
        /// </summary>
        /// <remarks>
        /// This method expands the base implementation by mantaining the check box values in the grid.
        /// </remarks>
        /// <param name="e">The data grid sort commant event argument</param>
        protected override void OnSortCommand(DataGridSortCommandEventArgs e) {
            // Save the check box values
            GetCheckBoxValues();

            // Sort datagrid
            base.OnSortCommand(e);

            // Populate checkbox values in the new page
            RePopulateCheckBoxes();
        }

        #endregion

    }
}