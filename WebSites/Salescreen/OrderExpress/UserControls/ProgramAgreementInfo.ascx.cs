using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for BaseWebFormControl.
    /// </summary>
    public partial class ProgramAgreementInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected ProgramAgreementData dtsProgramAgreement;
        protected AccountData dtsAccount;
        private CommonUtility clsUtil = new CommonUtility();
        private bool c_ShowOnlyException = false;
        protected ProgramAgreementCatalogTable ProgramAgreementCatalog;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                FillList();
            }
            //Always invisble. 

            CheckBoxList1.Attributes.Add("OnClick", "return false");
            CheckBoxList1.CssClass = "DescInfoLabel";

            trAccountStatus.Visible = false;  //(Page.Role > QSPForm.Business.AuthSystem.ROLE_FM);			
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        public ProgramAgreementData DataSource {
            get {
                return dtsProgramAgreement;
            }
            set {
                dtsProgramAgreement = value;
                ProgramAgreementExceptionList.DataSource = dtsProgramAgreement.ProgramAgreementException;
                ProgramAgreementExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT;
                ProgramAgreementExceptionList.ShipmentGroup_DataSource = dtsProgramAgreement.ShipmentGroup;
            }
        }

        public AccountData AccountDataSource {
            get {
                return dtsAccount;
            }
            set {
                dtsAccount = value;
                AccountExceptionList.DataSource = dtsAccount.AccountException;
                AccountExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT;
            }
        }

        public bool IsExceptionReadOnly {
            get {
                return ProgramAgreementExceptionList.IsReadOnly;
            }
            set {
                ProgramAgreementExceptionList.IsReadOnly = value;
            }
        }

        public bool imgBtnDetailAccount_Visible {
            get {
                return imgBtnDetailAccount.Visible;
            }
            set {
                imgBtnDetailAccount.Visible = value;
            }
        }

        public bool ShowOnlyException {
            get {
                return c_ShowOnlyException;
            }
            set {
                c_ShowOnlyException = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //Management for the Validation to only display the exception
            if (c_ShowOnlyException) {
                trAccountInfo.Visible = false;
                trProgramAgreementAddress.Visible = false;
                trProgramAgreementInfo.Visible = false;
                trProgramAgreementTerms.Visible = false;
                trOrderSupply.Visible = false;
            }
            else {
                trAccountInfo.Visible = true;
                trProgramAgreementAddress.Visible = true;
                trProgramAgreementInfo.Visible = true;
                trProgramAgreementTerms.Visible = true;
                //trOrderSupply.Visible = true;
            }
        }

        public override void BindForm() {
            //OrderHeader 
            QSPForm.Business.BusinessCalendarSystem calSys = new QSPForm.Business.BusinessCalendarSystem();

            //Account Information
            DataRow row = dtsAccount.Account.Rows[0];

            lblAccID.Text = row[AccountTable.FLD_PKID].ToString();

            if (row[AccountTable.FLD_FULF_ACCOUNT_ID] != System.DBNull.Value)
                lblEDSAccID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
            else
                lblEDSAccID.Text = "New Account";

            lblAccountName.Text = row[AccountTable.FLD_NAME].ToString();
            //Account Comment
            lblAccountComment.Text = row[AccountTable.FLD_COMMENTS].ToString();
            //Account Last Sales Date
            if (!row.IsNull(AccountTable.FLD_LAST_ORDER_DATE) && Convert.ToDateTime(row[AccountTable.FLD_LAST_ORDER_DATE]).ToShortDateString() != "1/1/1995")
                lblLastOrderDate.Text = Convert.ToDateTime(row[AccountTable.FLD_LAST_ORDER_DATE]).ToShortDateString();
            else
                lblLastOrderDate.Text = "";

            //Organization Information
            DataRow OrgRow = dtsAccount.Organization.Rows[0];

            //Display Organization Information
            //Look if the definition of the Organization it's override
            //In the AccountX Table
            if (dtsAccount.AccountX.Rows.Count == 0) {
                //When read-only mode
                //Organization Type
                lblOrgType.Text = OrgRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();
                //When read-only mode
                //Organization Level
                lblOrgLevel.Text = OrgRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();

            }
            else {
                DataRow accXRow = dtsAccount.AccountX.Rows[0];
                //When read-only mode
                //Organization Type
                lblOrgType.Text = accXRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();

                //When read-only mode
                //Organization Level
                lblOrgLevel.Text = accXRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();

            }
            //Account Status					
            lblAccountStatus.Text = row[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString();
            lblAccountStatus_ShortDescription.Text = row[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
            lblAccountStatusColor.BackColor = Color.FromName(row[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());

            //FM in the Account
            lblAccountFMInfo.Text = row[AccountTable.FLD_FM_ID].ToString() + " - " + row[AccountTable.FLD_FM_NAME].ToString();

            int AccID = Convert.ToInt32(row[AccountTable.FLD_PKID].ToString());
            string sIDName = AccountDetailInfo.ACC_ID;
            //clsUtil.SetJScriptForOpenDetail(imgBtnDetailAccount, QSPForm.Business.AppItem.AccountDetailInfo, sIDName, AccID.ToString(), 0, 0, "OnClick");
            clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailAccount, "AccountDetailInfo.aspx?", sIDName, AccID.ToString(), 0, 0, "OnClick");

            //Account Exceptions
            tblAccountException.Visible = true;
            AccountExceptionList.EntityID = AccID;
            AccountExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT; //Account
            AccountExceptionList.DataSource = dtsAccount.AccountException;
            AccountExceptionList.DataBind();

            DataRow campRow = dtsAccount.Campaign.Rows[0];
            //Campaign Information				
            //Last FY
            if (campRow.RowState != DataRowState.Added)
                lblLastFiscalYear.Text = campRow[CampaignTable.FLD_FISCAL_YEAR].ToString();
            else
                lblLastFiscalYear.Text = "";

            lblProgramTypeName.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();

            //Trade Class - ReadOnly
            if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
            else
                lblTradeClass.Text = "None";

            if (campRow[CampaignTable.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                lblTaxExemptionNumber.Text = campRow[CampaignTable.FLD_TAX_EXEMPTION_NO].ToString();
            if (campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();

            //Program Agreement
            ProgramAgreementTable dtblProgramAgreement = dtsProgramAgreement.ProgramAgreement;
            DataRow prgRow = dtblProgramAgreement.Rows[0];

            int formId = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_FORM_ID]);
            DateTime programAgreementDate = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_START_DATE]);

            clsUtil.SetProgramAgreementCatalogs(CheckBoxList1, formId, programAgreementDate);

            ProgramAgreementCatalog = dtsProgramAgreement.ProgramAgreementCatalog;  //catalogSystem.SelectAllByProgramAgreementID(ProgramAgreementID);

            if (ProgramAgreementCatalog.Rows.Count > 0) {
                for (int i = 0; i < ProgramAgreementCatalog.Rows.Count; i++) {
                    DataRow catalogRow = ProgramAgreementCatalog.Rows[i];

                    if (catalogRow.RowState != DataRowState.Deleted) {
                        ListItem item = CheckBoxList1.Items.FindByValue(catalogRow[ProgramAgreementCatalogTable.FLD_CATALOG_ID].ToString());
                        if (item != null)
                            item.Selected = true;
                    }
                }
            }

            // CheckBoxList1.Enabled = false;

            if (prgRow.RowState == DataRowState.Added)
                lblProgramAgreementID.Text = "New Program Agreement";
            else
                lblProgramAgreementID.Text = prgRow[ProgramAgreementTable.FLD_PKID].ToString();

            //EDS ProgramAgreement #
            if (!prgRow.IsNull(ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID))
                lblEDSProgramAgreementID.Text = prgRow[ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID].ToString();
            else
                lblEDSProgramAgreementID.Text = "New Program Agreement";

            if (prgRow.RowState == DataRowState.Added) {
                lblProgramAgreementStatus_ShortDescription.Text = "New Program Agreement";
                lblProgramAgreementStatusColor.BackColor = Color.White;
                lblProgramAgreementStatus_Description.Text = "";
            }
            else {
                lblProgramAgreementStatus_ShortDescription.Text = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_SHORT_DESCRIPTION].ToString();
                lblProgramAgreementStatusColor.BackColor = Color.FromName(prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_COLOR_CODE].ToString());
                lblProgramAgreementStatus_Description.Text = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_DESCRIPTION].ToString();
            }
            //Init
            lblEstimatedAmount.Text = "0";
            lblEnrollment.Text = "0";

            if (prgRow[ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS] != DBNull.Value)
                lblEstimatedAmount.Text = Convert.ToDecimal(prgRow[ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS]).ToString("N0");
            if (prgRow[ProgramAgreementTable.FLD_ENROLLMENT] != DBNull.Value)
                lblEnrollment.Text = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_ENROLLMENT]).ToString("N0");
            //Program Start and End Dates
            if (prgRow[ProgramAgreementTable.FLD_START_DATE] != DBNull.Value)
                lblStartDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_START_DATE]).ToShortDateString();
            if (prgRow[ProgramAgreementTable.FLD_END_DATE] != DBNull.Value)
                lblEndDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_END_DATE]).ToShortDateString();
            //Holiday Start and End Dates
            if (prgRow[ProgramAgreementTable.FLD_HOLIDAY_START_DATE] != DBNull.Value)
                lblHolidayStartDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_HOLIDAY_START_DATE]).ToShortDateString();
            if (prgRow[ProgramAgreementTable.FLD_HOLIDAY_END_DATE] != DBNull.Value)
                lblHolidayEndDate.Text = Convert.ToDateTime(prgRow[ProgramAgreementTable.FLD_HOLIDAY_END_DATE]).ToShortDateString();

            if (!prgRow.IsNull(ProgramAgreementTable.FLD_PROFIT_RATE))
                lblDefaultProfitRate.Text = ((Single)Convert.ToSingle(prgRow[ProgramAgreementTable.FLD_PROFIT_RATE]) * 100).ToString("N0") + "%";

            string priced = string.Empty;

            if (!prgRow.IsNull(ProgramAgreementTable.FLD_PRICED)) {
                priced = prgRow[ProgramAgreementTable.FLD_PRICED].ToString();
                if (priced == "True")
                    lblpriced.Text = "Priced";
                else
                    lblpriced.Text = "Unpriced";
                //  RadioButtonList1.SelectedIndex = RadioButtonList1.Items.IndexOf(RadioButtonList1.Items.FindByValue(priced));
            }

            // RadioButtonList1.Enabled = false;

            int PrgID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);

            //Bill To Address
            AddressControlInfo_Billing.ParentID = (int)dtsProgramAgreement.Campaign.Rows[0][CampaignTable.FLD_ACCOUNT_ID];
            AddressControlInfo_Billing.ParentType = QSPForm.Common.EntityType.TYPE_ACCOUNT; //Order Billing
            AddressControlInfo_Billing.DataSource = dtsProgramAgreement;
            AddressControlInfo_Billing.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_BILLING;
            AddressControlInfo_Billing.HideTypeAddress = true;
            AddressControlInfo_Billing.HideTitleAddress = true;
            AddressControlInfo_Billing.DataBind();

            //Ship To Address
            AddressControlInfo_Shipping.ParentID = PrgID;
            AddressControlInfo_Shipping.ParentType = QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT; //Order Shipping
            AddressControlInfo_Shipping.DataSource = dtsProgramAgreement;
            AddressControlInfo_Shipping.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
            AddressControlInfo_Shipping.HideTypeAddress = true;
            AddressControlInfo_Shipping.HideTitleAddress = true;
            AddressControlInfo_Shipping.DataBind();

            //Order Information
            if (dtsProgramAgreement.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = dtsProgramAgreement.OrderHeader.Rows[0];
                if (ordRow[OrderHeaderTable.FLD_ORDER_DATE] != DBNull.Value)
                    lblProgramAgreementDate.Text = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]).ToShortDateString();
            }

            int ShipTo = 0;

            // SUPPLY ORDER

            if (dtsProgramAgreement.OrderSupply.TotalQuantity > 0) {
                //Supply
                SupplyListInfo.DataSource = dtsProgramAgreement.OrderSupply;
                SupplyListInfo.DataBind();
                trOrderSupply.Visible = true;

                //Ship To 

                DataRow shipRow = dtsProgramAgreement.ShipmentGroup.Rows[0];

                if (shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] != DBNull.Value)
                    ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE)) {
                    DateTime orderDate = DateTime.Now;
                    DateTime ShipTo_DeliveryDate;
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);
                    int NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, ShipTo_DeliveryDate);
                    lblShipSupplyDeliveryDate.Text = ShipTo_DeliveryDate.ToLongDateString();
                    lblShipSupplyNbDayLeadTime.Text = NbDayLeadTime.ToString();
                }

                tblAddressSupply.Visible = false;
                if (ShipTo == 1)
                    lblShipTo.Text = "FSM";
                else if (ShipTo == 2)
                    lblShipTo.Text = "Account&nbsp;(Ship&nbsp;To&nbsp;Address)";
                else if (ShipTo == 3)
                    lblShipTo.Text = "Enter&nbsp;new&nbsp;Address";
                else
                    lblShipTo.Text = "";

                if (ShipTo == 1 || ShipTo == 3) {
                    tblAddressSupply.Visible = true;
                    int ShipGrpID = -1;
                    //Supply Ship To Address
                    if (!dtsProgramAgreement.ShipmentGroup.Rows[0].IsNull(ShipmentGroupTable.FLD_PKID)) {
                        ShipGrpID = Convert.ToInt32(dtsProgramAgreement.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);

                        AddressControlInfo_Supply.ParentID = ShipGrpID;
                        AddressControlInfo_Supply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                        AddressControlInfo_Supply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                        AddressControlInfo_Supply.DataSource = dtsProgramAgreement;
                        AddressControlInfo_Supply.DataBind();
                    }
                }
            }
            else {
                trOrderSupply.Visible = false;
            }

            int ProgStatusID = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;
            if (prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] != DBNull.Value)
                ProgStatusID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID]);
            //Exceptions
            //((dtblOrderHeader.Rows[0].RowState == DataRowState.Added) ||
            if (ProgStatusID > QSPForm.Common.OrderStatus.SAVED_FOR_LATER) {
                //Order Exceptions
                tblProgramAgreementException.Visible = true;
                ProgramAgreementExceptionList.EntityID = PrgID;
                ProgramAgreementExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT; //Account
                ProgramAgreementExceptionList.DataSource = dtsProgramAgreement.ProgramAgreementException;
                ProgramAgreementExceptionList.BindForm();
            }
            else {
                tblProgramAgreementException.Visible = false;
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        private void FillList() {
        }

        public bool UpdateDataSource() {
            try {
                ValSumExceptionInfo.Visible = false;
                if (!ProgramAgreementExceptionList.ValidateForm()) {
                    ValSumExceptionInfo.Visible = true;
                    clsUtil.RenderStartUpScroll(ValSumExceptionInfo);
                    Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
                ProgramAgreementExceptionList.UpdateDataSource();

                return true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return true;
        }
    }
}