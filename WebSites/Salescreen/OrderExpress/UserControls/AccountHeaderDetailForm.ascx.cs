using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using QSPForm.Business;
using QSPForm.Business.com.qsp.ws.AccountFinderService;
using dataRef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountHeaderDetailForm.
    /// </summary>
    public partial class AccountHeaderDetailForm : BaseWebFormControl {
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.CheckBox chkBoxShipSameAddress;
        private int c_AccID = 0;
        private int c_CampID = 0;
        public event System.EventHandler<MatchingAccountsConfirmEventArgs> MatchingAccountsConfirmed;
        public event System.EventHandler AddressHygieneConfirmed;

        protected void Page_Load(object sender, System.EventArgs e) {
            AddJavascript();

            // Put user code to initialize the page here			
            if (!IsPostBack) {
                CampaignTable dTblCamp = dtsAccount.Campaign;
                if (dTblCamp.Rows.Count > 0) {
                    DataRow campRow = dTblCamp.Rows[0];
                    int AccountFY = Convert.ToInt32(campRow[CampaignTable.FLD_FISCAL_YEAR]);

                    BusinessCalendarSystem calSys = new BusinessCalendarSystem();

                    DateTime FirstDateFY = calSys.GetFirstDateOfFiscalYear(AccountFY);
                    //Now account can be entered one fiscal year ahead
                    DateTime LastDateFY = calSys.GetLastDateOfFiscalYear(AccountFY).AddYears(1);

                    rangVal_StartDate.MinimumValue = FirstDateFY.ToShortDateString();
                    rangVal_StartDate.MaximumValue = LastDateFY.ToShortDateString();

                    rangVal_EndDate.MinimumValue = FirstDateFY.ToShortDateString();
                    rangVal_EndDate.MaximumValue = LastDateFY.ToShortDateString();

                    //string errorMsg = "must be include in the FY " + AccountFY.ToString().Substring(2,2) + " (Between " + FirstDateFY.ToShortDateString() + " and " + LastDateFY.ToShortDateString() + ")";
                    string errorMsg = "must be within the Current Fiscal Year" + AccountFY.ToString().Substring(2, 2) + " (Between " + FirstDateFY.ToShortDateString() + " and " + LastDateFY.ToShortDateString() + ")";

                    rangVal_StartDate.ErrorMessage = "The Program Start Date " + errorMsg;
                    rangVal_EndDate.ErrorMessage = "The Program End Date " + errorMsg;
                }
                SetBusinessMessage();
            }

            clsUtil.SetJScriptForOpenCalendar(hypLnkStartDate, txtStartDate);
            clsUtil.SetJScriptForOpenCalendar(hypLnkEndDate, txtEndDate);
            if (this.Page.Role >= AuthSystem.ROLE_ACCOUNTING_MANAGER)
                clsUtil.SetJScriptForOpenCalendar(hypLnkTaxExemptionExpirationDate, txtTaxExemptionExpirationDate);
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                //clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM,txtFMID,txtFMName,QSPForm.Business.AppItem.FMSelector,0,0); FMSelector.aspx?NoMenu=10
                clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
            }
            //Manage when the account name is entered and copied 
            //into the Postal Address Billing Org_name 
            //AddressForm_Billing.TextBoxOrgName.ClientID
            clsUtil.SetJScriptForCopyValueFromCtrlToCtrl(txtAccountName, txtAccountName1, DualAddressFormControl.EntityNameTextBox);

            DualAddressFormControl.EntityNameTextBox.ReadOnly = true;

            if (txtAccountName.Text == "New Organization")
                txtAccountName.Text = "New Account";

            //trAccountStatus.Visible = (Page.Role > AuthSystem.ROLE_FM);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new EventHandler(Page_Load);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.MatchingAccountListControl.MatchingAccountsConfirmed += new EventHandler<MatchingAccountsConfirmEventArgs>(MatchingAccountListControl_MatchingAccountsConfirmed);
            this.DualAddressFormControl.AddressHygieneConfirmed += new EventHandler(DualAddressFormControl_AddressHygieneConfirmed);
        }

        #endregion

        protected void MatchingAccountListControl_MatchingAccountsConfirmed(object sender, MatchingAccountsConfirmEventArgs e) {
            if (MatchingAccountsConfirmed != null) {
                MatchingAccountsConfirmed(MatchingAccountListControl, e);
            }
        }

        protected void DualAddressFormControl_AddressHygieneConfirmed(object sender, EventArgs e) {
            if (AddressHygieneConfirmed != null) {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }

        public int AccountID {
            get {
                return c_AccID;
            }
            set {
                c_AccID = value;
                ViewState["AccID"] = c_AccID;
            }
        }

        public int CampaignID {
            get {
                return c_CampID;
            }
            set {
                c_CampID = value;
            }
        }


        public AccountData DataSource {
            get {
                return dtsAccount;
            }
            set {
                dtsAccount = value;
            }
        }

        public bool ValidateDuplicateAccounts {
            get {
                bool validateDuplicateAccounts = false;

                if (ViewState["ValidateDuplicateAccounts"] != null) {
                    validateDuplicateAccounts = Convert.ToBoolean(ViewState["ValidateDuplicateAccounts"]);
                }

                return validateDuplicateAccounts;
            }
            set {
                ViewState["ValidateDuplicateAccounts"] = value;
            }
        }

        public void InitializeControls() {
            DualAddressFormControl.BillingParentID = c_AccID;
            DualAddressFormControl.ShippingParentID = c_AccID;
            DualAddressFormControl.BillingParentType = EntityType.TYPE_ACCOUNT;
            DualAddressFormControl.ShippingParentType = EntityType.TYPE_ACCOUNT;
            DualAddressFormControl.DataSource = DataSource;
            DualAddressFormControl.BillToAdressNote = false;

            DualAddressFormControl.EntityNameTextBox.Text = txtAccountName.Text + " " + txtAccountName1.Text;
        }

        private void SetBusinessMessage() {
            int FormID = 0;
            if (!dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_FORM_ID))
                FormID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_FORM_ID]);
            //Filter for Section Number
            clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.AccountForm_Step3, FormID);
            trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //Display management of the Org Name, Level and Type
            bool IsNewOrg = false;
            bool IsNewFromMDR = false;
            DataRow orgRow = dtsAccount.Organization.Rows[0];
            IsNewOrg = (orgRow.RowState == DataRowState.Added);
            IsNewFromMDR = (!orgRow.IsNull(OrganizationTable.FLD_MDRPID));

            //Init Visibility to All to False
            trOrgNameInfo.Visible = false;
            trOrgNameEdit.Visible = false;
            trOrgTypeInfo.Visible = false;
            trOrgTypeEdit.Visible = false;
            //Org Info
            if (IsNewOrg && !IsNewFromMDR) {
                trOrgNameEdit.Visible = true;
                trOrgTypeEdit.Visible = true;
            }
            else {
                trOrgNameInfo.Visible = true;
                trOrgTypeInfo.Visible = true;
            }
            //Trade Class is editable for Field Support and Higher
            trTradeClassInfo.Visible = false;
            trTradeClassEdit.Visible = false;
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                trTradeClassEdit.Visible = true;
            }
            else {
                trTradeClassInfo.Visible = true;
            }

            bool IsNewAccount = (dtsAccount.Account.Rows[0].RowState == DataRowState.Added);
            bool IsNullProgramType = (dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_PROG_TYPE_ID));
            //Campaign Program Info
            bool IsProgEditable = IsNullProgramType;
            trProgramTypeInfo.Visible = !IsProgEditable;
            trProgramTypeEdit.Visible = IsProgEditable;

            //Tax Info
            tblTaxInfoEdit.Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);
            tblTaxInfoReadOnly.Visible = !tblTaxInfoEdit.Visible;

            //FM 
            trFmInfo.Visible = true;
            trFmEdit.Visible = false;
            trFmEdit.Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);
            trFmInfo.Visible = !trFmEdit.Visible;

            DualAddressFormControl.EntityNameTextBox.Text = txtAccountName.Text + " " + txtAccountName1.Text;
        }

        public override void BindForm() {
            //AccountTable 
            AccountTable dtblAccount = dtsAccount.Account;
            if (!IsPostBack) {
                FillList();
            }

            if (dtblAccount.Rows.Count > 0) {
                DataRow row = dtblAccount.Rows[0];
                ListItem lstItem;

                if (row.RowState == DataRowState.Added)
                    lblAccID.Text = "New Account";
                else
                    lblAccID.Text = row[AccountTable.FLD_PKID].ToString();

                if (row.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                    lblEDSAccID.Text = "New Account";
                else
                    lblEDSAccID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();

                string sAccountName = row[AccountTable.FLD_NAME].ToString();
                int CountOfChar = 30;
                if (sAccountName.Length > CountOfChar) {
                    txtAccountName.Text = sAccountName.Substring(0, CountOfChar);
                    int MaxLength = 60;
                    if (sAccountName.Length < MaxLength)
                        MaxLength = sAccountName.Length;
                    txtAccountName1.Text = sAccountName.Substring(CountOfChar, (MaxLength - CountOfChar));
                }
                else {
                    //commented for watermark
                    txtAccountName.Text = sAccountName;
                    txtAccountName1.Text = "";
                }

                txtComment.Text = row[AccountTable.FLD_COMMENTS].ToString();

                lblFMInfo.Text = row[AccountTable.FLD_FM_ID].ToString() + " - " + row[AccountTable.FLD_FM_NAME].ToString();
                txtFMID.Text = row[AccountTable.FLD_FM_ID].ToString();
                txtFMName.Text = row[AccountTable.FLD_FM_NAME].ToString();

                //Query Organization Information
                OrganizationTable dTblOrg = dtsAccount.Organization;
                DataRow OrgRow = dTblOrg.Rows[0];
                //Display Organization Information
                //Organization Name
                //commented for watermark
                txtOrganizationName.Text = OrgRow[OrganizationTable.FLD_NAME].ToString();
                //Organization Name
                lblOrganizationName.Text = OrgRow[OrganizationTable.FLD_NAME].ToString();

                if (row.RowState == DataRowState.Added) {
                    txtAccountName.Text = "";
                    if (OrgRow.IsNull(OrganizationTable.FLD_MDRPID))
                        txtOrganizationName.Text = "";
                }

                //Look if the definition of the Organization it's override
                //In the AccountX Table
                if (dtsAccount.AccountX.Rows.Count == 0) {
                    //Organization Type
                    int OrgTypeID = 0;
                    if (!OrgRow.IsNull(OrganizationTable.FLD_ORG_TYPE_ID))
                        OrgTypeID = Convert.ToInt32(OrgRow[OrganizationTable.FLD_ORG_TYPE_ID]);
                    if (OrgTypeID > 0) {
                        lstItem = ddlOrgType.Items.FindByValue(OrgTypeID.ToString());
                        if (lstItem != null) {
                            ddlOrgType.ClearSelection();
                            lstItem.Selected = true;
                        }
                    }
                    //When read-only mode
                    //Organization Type
                    lblOrgType.Text = OrgRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();

                    //Organization Level
                    int OrgLevelID = 0;
                    if (!OrgRow.IsNull(OrganizationTable.FLD_ORG_LEVEL_ID))
                        OrgLevelID = Convert.ToInt32(OrgRow[OrganizationTable.FLD_ORG_LEVEL_ID]);
                    if (OrgLevelID > 0) {
                        lstItem = ddlOrgLevel.Items.FindByValue(OrgLevelID.ToString());
                        if (lstItem != null) {
                            ddlOrgLevel.ClearSelection();
                            lstItem.Selected = true;
                        }
                    }
                    //When read-only mode
                    //Organization Level
                    lblOrgLevel.Text = OrgRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();
                }
                else {
                    DataRow accXRow = dtsAccount.AccountX.Rows[0];
                    //Organization Type
                    int OrgTypeID = 0;
                    if (!accXRow.IsNull(OrganizationTable.FLD_ORG_TYPE_ID))
                        OrgTypeID = Convert.ToInt32(accXRow[OrganizationTable.FLD_ORG_TYPE_ID]);
                    if (OrgTypeID > 0) {
                        lstItem = ddlOrgType.Items.FindByValue(OrgTypeID.ToString());
                        if (lstItem != null) {
                            ddlOrgType.ClearSelection();
                            lstItem.Selected = true;
                        }
                    }
                    //When read-only mode
                    //Organization Type
                    lblOrgType.Text = accXRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();

                    //Organization Level
                    int OrgLevelID = 0;
                    if (!accXRow.IsNull(OrganizationTable.FLD_ORG_LEVEL_ID))
                        OrgLevelID = Convert.ToInt32(accXRow[OrganizationTable.FLD_ORG_LEVEL_ID]);
                    if (OrgLevelID > 0) {
                        lstItem = ddlOrgLevel.Items.FindByValue(OrgLevelID.ToString());
                        if (lstItem != null) {
                            ddlOrgLevel.ClearSelection();
                            lstItem.Selected = true;
                        }
                    }
                    //When read-only mode
                    //Organization Level
                    lblOrgLevel.Text = accXRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();
                }

                //Account Status
                if (row.RowState == DataRowState.Added) {
                    lblAccountStatusColor.BackColor = Color.White;
                    lblAccountStatus_ShortDescription.Text = "New Account";
                    lblAccountStatus_Description.Text = "";
                }
                else {
                    //Already Existant Account					
                    lblAccountStatusColor.BackColor = Color.FromName(row[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());
                    lblAccountStatus_ShortDescription.Text = row[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
                    lblAccountStatus_Description.Text = row[AccountTable.FLD_ACCOUNT_STATUS_DESCRIPTION].ToString();
                }

                //----------------------------------
                //   Tax Information --  from Account
                //----------------------------------
                if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                    if (!row.IsNull(AccountTable.FLD_TAX_EXEMPTION_NO))
                        txtTaxExemptionNumber.Text = row[AccountTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!row.IsNull(AccountTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        txtTaxExemptionExpirationDate.Text = Convert.ToDateTime(row[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                }
                else {
                    if (!row.IsNull(AccountTable.FLD_TAX_EXEMPTION_NO))
                        lblTaxExemptionNumber.Text = row[AccountTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!row.IsNull(AccountTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(row[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                }

                //Account Collection information
                if (!row.IsNull(AccountTable.FLD_ACCOUNT_COLLECTION_DATE))
                    lblAccountCollectionDate.Text = Convert.ToDateTime(row[AccountTable.FLD_ACCOUNT_COLLECTION_DATE]).ToShortDateString();
                if (!row.IsNull(AccountTable.FLD_ACCOUNT_COLLECTION_AMOUNT))
                    lblAccountCollectionAmount.Text = Convert.ToDecimal(row[AccountTable.FLD_ACCOUNT_COLLECTION_AMOUNT]).ToString("C");

                //Campaign Information
                DataRow campRow = dtsAccount.Campaign.Rows[0];

                //Trade Class - Editable
                int TradeClassID = 0;
                if (campRow[CampaignTable.FLD_TRADE_CLASS_ID] != DBNull.Value)
                    TradeClassID = Convert.ToInt32(campRow[CampaignTable.FLD_TRADE_CLASS_ID]);
                if (TradeClassID > 0) {
                    lstItem = ddlTradeClass.Items.FindByValue(TradeClassID.ToString());
                    if (lstItem != null) {
                        ddlTradeClass.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
                //Trade Class - ReadOnly
                if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                    lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
                else
                    lblTradeClass.Text = "None";

                if (campRow[CampaignTable.FLD_PROG_TYPE_ID] != System.DBNull.Value) {
                    lstItem = ddlType.Items.FindByValue(campRow[CampaignTable.FLD_PROG_TYPE_ID].ToString());
                    if (lstItem != null) {
                        ddlType.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
                int ProgTypeID = 0;
                if (!campRow.IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                    ProgTypeID = Convert.ToInt32(campRow[CampaignTable.FLD_PROG_TYPE_ID]);
                lblProgramType.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();
                lblFiscalYear.Text = campRow[CampaignTable.FLD_FISCAL_YEAR].ToString();

                if (campRow[CampaignTable.FLD_START_DATE] != DBNull.Value)
                    txtStartDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_START_DATE]).ToShortDateString();
                if (campRow[CampaignTable.FLD_END_DATE] != DBNull.Value)
                    txtEndDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_END_DATE]).ToShortDateString();

                txtEstimatedAmount.Text = "0";
                txtEnrollment.Text = "0";
                if (campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] != DBNull.Value)
                    txtEstimatedAmount.Text = Convert.ToDecimal(campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS]).ToString("F0");
                if (campRow[CampaignTable.FLD_ENROLLMENT] != DBNull.Value)
                    txtEnrollment.Text = campRow[CampaignTable.FLD_ENROLLMENT].ToString();

                //Default Warehouse
                if (ProgTypeID == 11)
                    lblDefaultWarehouse.Text = campRow[CampaignTable.FLD_WAREHOUSE_NAME].ToString();
                else
                    lblDefaultWarehouse.Text = "N/A";
            }

            DualAddressFormControl.DataBind();
        }


        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        private void FillList() {
            clsUtil.SetOrganizationTypeDropDownList(ddlOrgType, true);
            clsUtil.SetOrganizationLevelDropDownList(ddlOrgLevel, true);
            clsUtil.SetTradeClassDropDownList(ddlTradeClass, true);
            clsUtil.SetProgramTypeDropDownList(ddlType, true);
        }

        public bool ValidateForm() {
            bool isValid = true;
            Account searchAccount;

            trValSumAccountInfo.Visible = false;
            trValSumOrderTermInfo.Visible = false;

            if (isValid && !IsValid(tblAccountInfo.Controls)) {
                trValSumAccountInfo.Visible = true;
                clsUtil.RenderStartUpScroll(lblTitleAccountInfo);
                this.Page.MaintainScrollPositionOnPostBack = false;
                isValid = false;
            }

            isValid = (isValid && DualAddressFormControl.IsValid());

            if (isValid && !IsValid(tblSumOrderTermInfo.Controls)) {
                trValSumOrderTermInfo.Visible = true;
                clsUtil.RenderStartUpScroll(lblTitleOrderTermInfo);
                this.Page.MaintainScrollPositionOnPostBack = false;
                isValid = false;
            }

            if (isValid && ValidateDuplicateAccounts) {
                searchAccount = new Account();
                searchAccount.Name = txtAccountName.Text;
                searchAccount.ProgramType = (ProgramType)Enum.Parse(typeof(ProgramType), ddlType.SelectedItem.Text.Replace(" ", ""), true);
                searchAccount.Address = DualAddressFormControl.GetMatchingAccountsShippingAddress();

                MatchingAccountListControl.DataSource = searchAccount;
                isValid = MatchingAccountListControl.Validate();
            }

            //if everything have been ok
            return isValid;
        }

        public bool UpdateDataSource() {

            //Account
            DataRow row = dtsAccount.Account.Rows[0];
            string sAccountName = txtAccountName.Text.TrimStart();
            if (sAccountName.Length < 30)
                sAccountName = sAccountName.PadRight(30, ' ');
            sAccountName = sAccountName + txtAccountName1.Text.TrimStart();

            clsUtil.UpdateRow(row, AccountTable.FLD_NAME, sAccountName);

            //----------------------------------------------
            //     Tax Information -- At least, a Field Support can change this information
            //----------------------------------------------
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                //if (txtTaxExemptionNumber.Text.Trim().Length >0)
                clsUtil.UpdateRow(row, AccountTable.FLD_TAX_EXEMPTION_NO, txtTaxExemptionNumber.Text);

                compVal_TaxExemptionExpirationDate.Validate();
                //if (compVal_TaxExemptionExpirationDate.IsValid && (txtTaxExemptionExpirationDate.Text.Trim().Length >0))
                if (compVal_TaxExemptionExpirationDate.IsValid)
                    clsUtil.UpdateRow(row, AccountTable.FLD_TAX_EXEMPTION_EXP_DATE, txtTaxExemptionExpirationDate.Text.Trim());
            }
            clsUtil.UpdateRow(row, AccountTable.FLD_COMMENTS, txtComment.Text);

            //Campaign Information
            DataRow campRow = dtsAccount.Campaign.Rows[0];

            compVal_StartDate.Validate();
            if (compVal_StartDate.IsValid)
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_START_DATE, txtStartDate.Text);

            if (compVal_EndDate.IsValid)
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_END_DATE, txtEndDate.Text);

            if (compVal_EstimatedAmount.IsValid)
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_GOAL_ESTIMATED_GROSS, txtEstimatedAmount.Text);

            if (compVal_Enrollment.IsValid)
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_ENROLLMENT, txtEnrollment.Text);

            // set the real fiscal year
            if (compVal_EndDate.IsValid && compVal_StartDate.IsValid && custProgramDateValidator.IsValid) {
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                string[] sStartDate = txtStartDate.Text.Split('/');
                DateTime dStartDate = new DateTime(Convert.ToInt32(sStartDate[2]), Convert.ToInt32(sStartDate[0]), Convert.ToInt32(sStartDate[1]));
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_FISCAL_YEAR, calSys.GetFiscalYear(dStartDate).ToString());
            }

            //------------------------------------------------
            //  Tax Information - //We copy the information to the campaign Level
            //------------------------------------------------			
            clsUtil.UpdateRow(campRow, CampaignTable.FLD_TAX_EXEMPTION_NO, row);
            clsUtil.UpdateRow(campRow, CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE, row);

            clsUtil.UpdateRow(campRow, CampaignTable.FLD_NAME, sAccountName);

            bool IsNewAccount = (row.RowState == DataRowState.Added);
            bool IsNullProgramType = (campRow.IsNull(CampaignTable.FLD_PROG_TYPE_ID));
            //Campaign Program Info
            bool IsProgEditable = IsNullProgramType;
            if (IsProgEditable) {
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_PROG_TYPE_ID, ddlType.SelectedValue);
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_PROG_TYPE_NAME, ddlType.SelectedItem.Text);
            }

            //Trade Class
            if (ddlTradeClass.SelectedIndex > 0) {
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_TRADE_CLASS_ID, ddlTradeClass.SelectedValue);
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_TRADE_CLASS_NAME, ddlTradeClass.SelectedItem.Text);
            }
            else {
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_TRADE_CLASS_ID, "");
                clsUtil.UpdateRow(campRow, CampaignTable.FLD_TRADE_CLASS_NAME, "");
            }

            //We putting the Info of the Account in case is different.
            DualAddressFormControl.EntityNameTextBox.Text = txtAccountName.Text.Trim() + " " + txtAccountName1.Text.Trim();
            DualAddressFormControl.Update();


            //Set Organization Information
            //For the Type and Level, that will depends of many criteria
            //if the account is overrided in the AccountXTable f
            if (dtsAccount.AccountX.Rows.Count == 0) {

            }

            OrganizationTable dTblOrg = dtsAccount.Organization;
            DataRow OrgRow = dTblOrg.Rows[0];
            if (OrgRow.RowState == DataRowState.Added) {
                //Org Type
                OrgRow[OrganizationTable.FLD_ORG_TYPE_ID] = ddlOrgType.SelectedValue;
                OrgRow[OrganizationTable.FLD_ORG_TYPE_NAME] = ddlOrgType.SelectedItem.Text;
                //Org Level
                OrgRow[OrganizationTable.FLD_ORG_LEVEL_ID] = ddlOrgLevel.SelectedValue;
                OrgRow[OrganizationTable.FLD_ORG_LEVEL_NAME] = ddlOrgLevel.SelectedItem.Text;
                //Org Name
                OrgRow[OrganizationTable.FLD_NAME] = txtOrganizationName.Text.Trim();

                //Refresh read-only control
                lblOrgType.Text = ddlOrgType.SelectedItem.Text;
                lblOrgLevel.Text = ddlOrgLevel.SelectedItem.Text;
            }
            //Save the FM Info
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                bool IsFMChanged = clsUtil.UpdateRow(row, AccountTable.FLD_FM_ID, txtFMID.Text);
                if (IsFMChanged) {
                    clsUtil.UpdateRow(campRow, CampaignTable.FLD_FM_ID, txtFMID.Text);
                    //FMName
                    clsUtil.UpdateRow(row, AccountTable.FLD_FM_NAME, txtFMName.Text);
                    clsUtil.UpdateRow(campRow, CampaignTable.FLD_FM_NAME, txtFMName.Text);
                }

            }

            return true;
        }

        public void ValidateProgramDate(object sender, ServerValidateEventArgs e) {
            try {
                e.IsValid = false;

                //mm/dd/yyyy
                string[] sStartDate = txtStartDate.Text.Split('/');
                string[] sEndDate = txtEndDate.Text.Split('/');
                DateTime dStartDate = new DateTime(Convert.ToInt32(sStartDate[2]), Convert.ToInt32(sStartDate[0]), Convert.ToInt32(sStartDate[1]));
                DateTime dEndDate = new DateTime(Convert.ToInt32(sEndDate[2]), Convert.ToInt32(sEndDate[0]), Convert.ToInt32(sEndDate[1]));
                //Verify if in the same fiscal year

                int AccountFY = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_FISCAL_YEAR]);
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                DateTime FirstDateFY = calSys.GetFirstDateOfFiscalYear(AccountFY);
                DateTime LastDateFY = calSys.GetLastDateOfFiscalYear(AccountFY);

                if ((dStartDate >= FirstDateFY) && (dStartDate < LastDateFY) &&
                    (dEndDate > FirstDateFY) && (dEndDate <= LastDateFY)) {
                    e.IsValid = true;
                }
                else {

                    FirstDateFY = FirstDateFY.AddYears(1);
                    LastDateFY = LastDateFY.AddYears(1);

                    if ((dStartDate >= FirstDateFY) && (dStartDate < LastDateFY) &&
                        (dEndDate > FirstDateFY) && (dEndDate <= LastDateFY)) {
                        e.IsValid = true;
                    }
                }
            }
            catch {
                e.IsValid = false;
            }
        }

        private void AddJavascript() {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");
        }

        public void ResetStatus() {
            DualAddressFormControl.ResetStatus();
        }
    }
}