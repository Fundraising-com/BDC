using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class AccountInfo : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        protected dataRef dtsAccount;
        protected System.Web.UI.WebControls.CheckBox chkBoxShipSameAddress;
        private int c_AccountID = 0;
        private int c_CampaignID = 0;
        private bool c_ShowOnlyException = false;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            trAccountStatus.Visible = (Page.Role > AuthSystem.ROLE_FM);
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
            this.btnCreditApplication.Click += new System.Web.UI.ImageClickEventHandler(this.btnCreditApplication_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        public dataRef DataSource {
            get {
                return dtsAccount;
            }
            set {
                dtsAccount = value;
                if (dtsAccount != null) {
                    ExceptionList.DataSource = dtsAccount.AccountException;
                    ExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT;
                }
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
            btnCreditApplication.Visible = false;
            bool IsVisible_CreditApp = false;
            DataRow accRow = dtsAccount.Account.Rows[0];

            if (accRow.RowState != DataRowState.Added) {
                //We display the access to the credit app form
                //if a credit app is already entered
                if (dtsAccount.CreditApplication.Rows.Count > 0) {
                    IsVisible_CreditApp = true;
                }
                else {
                    //We display the credit app form
                    //if it's required
                    if (dtsAccount.AccountException.Rows.Count > 0) {
                        DataView dvException = new DataView(dtsAccount.AccountException);
                        dvException.RowFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " = " + Convert.ToInt32(QSPForm.Common.BusinessExceptionType.CreditApplication).ToString();
                        if (dvException.Count > 0) {
                            IsVisible_CreditApp = true;
                        }
                    }
                }
            }
            btnCreditApplication.Visible = IsVisible_CreditApp;

            //Management for the Validation to only display the exception
            if (c_ShowOnlyException) {
                trAccountInfo.Visible = false;
                trAccountAddress.Visible = false;
                trOrderTermsInfo.Visible = false;
            }
            else {
                trAccountInfo.Visible = true;
                trAccountAddress.Visible = true;
                trOrderTermsInfo.Visible = true;
            }
        }

        public int AccountID {
            get {
                return c_AccountID;
            }
            set {
                c_AccountID = value;
            }
        }

        public int CampaignID {
            get {
                return c_CampaignID;
            }
            set {
                c_CampaignID = value;
            }
        }

        public override void BindForm() {
            //AccountTable 
            AccountTable dtblAccount = dtsAccount.Account;
            if (dtblAccount.Rows.Count > 0) {
                DataRow row = dtblAccount.Rows[0];

                if (row.RowState == DataRowState.Added) {
                    lblAccID.Text = "New Account";
                }
                else {
                    lblAccID.Text = row[AccountTable.FLD_PKID].ToString();
                }

                if (row.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID)) {
                    lblEDSAccID.Text = "New Account";
                }
                else {
                    lblEDSAccID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
                }

                lblAccountName.Text = row[AccountTable.FLD_NAME].ToString();

                //Account Status
                if (row.RowState == DataRowState.Added) {
                    lblAccountStatusColor.BackColor = Color.White;
                    lblAccountStatus_ShortDescription.Text = "New Account";
                    lblAccountStatus.Text = "";
                }
                else {
                    //Already Existant Account					
                    lblAccountStatusColor.BackColor = Color.FromName(row[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());
                    lblAccountStatus_ShortDescription.Text = row[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
                    if (row[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString().ToLower() != row[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString().ToLower())
                        lblAccountStatus.Text = row[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString();
                    else {
                        lblAccountStatus.Text = "";
                        lblAccountStatus.Visible = false;
                    }
                }

                lblFMInfo.Text = row[AccountTable.FLD_FM_ID].ToString() + " - " + row[AccountTable.FLD_FM_NAME].ToString();

                int as400Status = InfoStatus.NONE;
                if (!row.IsNull(AccountTable.FLD_INFO_STATUS)) {
                    as400Status = Convert.ToInt32(row[AccountTable.FLD_INFO_STATUS]);
                }
                if (as400Status != InfoStatus.ERROR) {
                    if (!row.IsNull(AccountTable.FLD_LAST_ORDER_DATE)) {
                        DateTime LastOrderDate = Convert.ToDateTime(row[AccountTable.FLD_LAST_ORDER_DATE]);
                        lblLastOrderDate.Text = LastOrderDate.ToShortDateString();

                        CommonSystem comSys = new CommonSystem();
                        int NbOfInactiveMonth = 0;
                        NbOfInactiveMonth = comSys.GetNbOfMonth(LastOrderDate, DateTime.Today);
                        lblNbOfInactiveMonth.Text = NbOfInactiveMonth.ToString();
                    }
                    else {
                        lblLastOrderDate.Text = "";
                    }
                }
                else {
                    lblLastOrderDate.Text = "-----";
                }
                //Tax Exemption information
                if (row[AccountTable.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                    lblTaxExemptionNumber.Text = row[AccountTable.FLD_TAX_EXEMPTION_NO].ToString();
                if (row[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                    lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(row[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                //Account Collection information
                if (!row.IsNull(AccountTable.FLD_ACCOUNT_COLLECTION_DATE))
                    lblAccountCollectionDate.Text = Convert.ToDateTime(row[AccountTable.FLD_ACCOUNT_COLLECTION_DATE]).ToShortDateString();
                if (!row.IsNull(AccountTable.FLD_ACCOUNT_COLLECTION_AMOUNT))
                    lblAccountCollectionAmount.Text = Convert.ToDecimal(row[AccountTable.FLD_ACCOUNT_COLLECTION_AMOUNT]).ToString("C");

                lblComment.Text = row[AccountTable.FLD_COMMENTS].ToString();

                //Query Organization Information
                OrganizationTable dTblOrg = dtsAccount.Organization; ;
                DataRow OrgRow = dTblOrg.Rows[0];
                //Display Organization Information
                lblOrganizationName.Text = OrgRow[OrganizationTable.FLD_NAME].ToString();

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

                //Manage the Detail button to Org
                if (OrgRow.RowState != DataRowState.Added) {
                    string sOrgID = OrgRow[OrganizationTable.FLD_PKID].ToString();
                    CommonUtility clsUtil = new CommonUtility();
                    //	clsUtil.SetJScriptForOpenDetail(imgBtnDetailOrg, QSPForm.Business.AppItem.OrganizationDetailInfo, OrganizationDetailInfo.ORG_ID, sOrgID, 0, 0, "OnClick");
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailOrg, "OrganizationDetailInfo.aspx?", OrganizationDetailInfo.ORG_ID, sOrgID, 0, 0, "OnClick");
                    imgBtnDetailOrg.Visible = true;
                }
                else
                    imgBtnDetailOrg.Visible = false;

                if (dtsAccount.Campaign.Rows.Count > 0) {
                    DataRow campRow = dtsAccount.Campaign.Rows[0];

                    //Campaign Information	
                    if (campRow[CampaignTable.FLD_START_DATE] != DBNull.Value)
                        lblStartDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_START_DATE]).ToShortDateString();
                    if (campRow[CampaignTable.FLD_END_DATE] != DBNull.Value)
                        lblEndDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_END_DATE]).ToShortDateString();
                    //Last FY
                    if (campRow.RowState != DataRowState.Added)
                        lblLastFiscalYear.Text = campRow[CampaignTable.FLD_FISCAL_YEAR].ToString();
                    else
                        lblLastFiscalYear.Text = "";
                    //Init
                    lblEstimatedAmount.Text = "0";
                    lblEnrollment.Text = "0";
                    //Read from DB
                    if (campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] != DBNull.Value)
                        lblEstimatedAmount.Text = Convert.ToDecimal(campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS]).ToString("C");
                    if (campRow[CampaignTable.FLD_ENROLLMENT] != DBNull.Value)
                        lblEnrollment.Text = Convert.ToInt32(campRow[CampaignTable.FLD_ENROLLMENT]).ToString("N0");

                    int ProgTypeID = 0;
                    if (!campRow.IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                        ProgTypeID = Convert.ToInt32(campRow[CampaignTable.FLD_PROG_TYPE_ID]);
                    lblProgramTypeName.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();

                    //Trade Class
                    if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                        lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
                    else
                        lblTradeClass.Text = "None";

                    //Default Warehouse
                    if ((!campRow.IsNull(CampaignTable.FLD_WAREHOUSE_ID)) && (ProgTypeID == 11)) {
                        lblDefaultWarehouse.Text = campRow[CampaignTable.FLD_WAREHOUSE_NAME].ToString();
                        string sID = campRow[CampaignTable.FLD_WAREHOUSE_ID].ToString();
                        CommonUtility clsUtil = new CommonUtility();
                        //clsUtil.SetJScriptForOpenDetail(imgBtnWarehouse, QSPForm.Business.AppItem.WarehouseDetailInfo, WarehouseDetailInfo.WH_ID, sID, 650, 600);
                        clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnWarehouse, "WarehouseDetailInfo.aspx?", WarehouseDetailInfo.WH_ID, sID, 650, 600);
                    }
                    else {
                        lblDefaultWarehouse.Text = "N/A";
                        imgBtnWarehouse.Visible = false;
                    }
                }
            }

            //Bill To Address
            AddressInfo_Billing.ParentID = c_AccountID;
            AddressInfo_Billing.ParentType = EntityType.TYPE_ACCOUNT; //Account
            AddressInfo_Billing.DataSource = dtsAccount;
            AddressInfo_Billing.FilterTypeAddress = PostalAddressType.TYPE_BILLING; //Billing
            AddressInfo_Billing.DataBind();

            //Ship To Address
            AddressInfo_Shipping.ParentID = c_AccountID;
            AddressInfo_Shipping.ParentType = EntityType.TYPE_ACCOUNT; //Account
            AddressInfo_Shipping.DataSource = dtsAccount;
            AddressInfo_Shipping.FilterTypeAddress = PostalAddressType.TYPE_SHIPPING; //Shipping
            AddressInfo_Shipping.DataBind();

            //Exceptions
            tblAccountException.Visible = true;
            ExceptionList.EntityID = c_AccountID;
            ExceptionList.EntityTypeID = EntityType.TYPE_ACCOUNT; //Account
            ExceptionList.DataSource = dtsAccount.AccountException;
            ExceptionList.DataBind();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void btnCreditApplication_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.CreditApplicationDetailInfo);
            string url = "CreditApplicationDetailInfo.aspx?";
            Response.Redirect(url + "&" + CreditApplicationDetailInfo.ACC_ID + "=" + c_AccountID.ToString());
        }
    }
}