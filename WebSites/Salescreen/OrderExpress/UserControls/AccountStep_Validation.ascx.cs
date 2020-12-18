using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Business.com.qsp.ws.AccountFinderService;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class AccountStep_Validation : BaseAccountFormStep {
        private CommonUtility util = new CommonUtility();
        protected dataRef dtsAccount;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            //To Be always databind 
            IsFirstLoad = true;
            if (!IsPostBack) {
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitControl();
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnConfirm_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.AccountForm_Step3;
            this.StepItem = QSPForm.Business.AppItem.AccountForm_Step4;
            //this.NextAppItem = QSPForm.Business.AppItem.AccountForm_Step4;
            //this.ImageButtonNext = imgBtnNext;
            this.ImageButtonBack = imgBtnBack;

        }

        public dataRef DataSource {
            get {
                return dtsAccount;
            }
            set {
                dtsAccount = value;
                AccountInfo_Final.DataSource = dtsAccount;
            }
        }

        public bool DuplicateAccountOverride {
            get {
                bool duplicateAccountOverride = false;

                if (ViewState["DuplicateAccountOverride"] != null) {
                    duplicateAccountOverride = Convert.ToBoolean(ViewState["DuplicateAccountOverride"]);
                }

                return duplicateAccountOverride;
            }
            set {
                ViewState["DuplicateAccountOverride"] = value;
            }
        }

        public List<OutputAccount> MatchingAccounts {
            get {
                return (List<OutputAccount>)ViewState["MatchingAccounts"];
            }
            set {
                ViewState["MatchingAccounts"] = value;
            }
        }

        public override bool Update() {
            bool IsSuccess = false;
            try {
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                CopyInformationToEntity(QSPForm.Common.EntityType.TYPE_CAMPAIGN);
                OrganizationTable dTblOrg = dtsAccount.Organization;
                DataRow OrgRow = dTblOrg.Rows[0];
                if ((OrgRow.RowState == DataRowState.Added) && (OrgRow.IsNull(OrganizationTable.FLD_MDRPID)) && (OrgRow[OrganizationTable.FLD_MDRPID].ToString() == String.Empty)) {
                    CopyInformationToEntity(QSPForm.Common.EntityType.TYPE_ORGANIZATION);
                }
                IsSuccess = accSys.InsertAllDetail(this.Page.DataSource, DuplicateAccountOverride, MatchingAccounts, this.Page.UserID);
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return IsSuccess;
        }

        public override void BindForm() {
            AccountInfo_Final.DataSource = dtsAccount;
            //Do the Validation for the Account
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            accSys.PerformValidation(dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.INSERT);
            AccountInfo_Final.ShowOnlyException = chkBoxShowOnlyException.Checked;
            AccountInfo_Final.DataBind();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            bool IsValid = false;

            IsValid = Update();
            
            if (IsValid) 
            {
                DataRow accRow = this.Page.DataSource.Account.Rows[0];
                string sAccID = accRow[AccountTable.FLD_PKID].ToString();

                #region FY Patch

                // Will make sure the account has a valid campaign for 2009 and 2010. 
                // Will create the one that is missing.
                // This is a temporal patch while the campaign management gets an overhaul 
                // and it is able to manage multiple campaigns / FY

                // The patch should go when we create the PAs

                #endregion

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step5, BaseAccountDetail.ACC_ID, sAccID);

                string url = "~/AccountStep_Confirmation.aspx?&NoMenu=44&AccID=" + sAccID;

                Response.Redirect(url);
            }
        }

        protected void chkBoxShowOnlyException_CheckedChanged(object sender, System.EventArgs e) {
            AccountInfo_Final.ShowOnlyException = chkBoxShowOnlyException.Checked;
            //AccountInfo_Final.DataBind();
        }
    }
}