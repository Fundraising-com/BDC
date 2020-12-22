using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for BaseAccountForm.
    /// </summary>
    public partial class AccountForm_Step : BaseAccountForm {
        public const string ACCOUNT_ID = "AccountID";
        private const string ACCOUNT_DATA = "AccountData";
        protected dataDef dtsAccount;

        protected void Page_Load(object sender, System.EventArgs e) {
            this.Master.ValSummaryVisibility = false;
        }

        protected void SetHeaderAccountInformation() {
            if (OrganizationID > 0)
                this.lblSectionTitle.Text = "Add New Account:";
            else {
                this.lblSectionTitle.Text = "Add New Account:";
                this.trOrg.Visible = true;
            }

            this.lblInstruction.Text = "Use the Tab key to navigate and complete the required fields [*] below. When all the data is complete, click on Next button below . ";
            this.lblPageTitle.Text = "STEP 3 - Account Information";
        }

        protected void SetHeaderAccountValidation() {
            if (OrganizationID > 0)
                this.lblSectionTitle.Text = "Add New Account:";
            else {
                this.lblSectionTitle.Text = "Add New Account:";
                this.trOrg.Visible = true;
            }

            this.lblInstruction.Text = "Carefully review the Account Information. Use Back button below to review/modify previous pages[s]. When the Account Information is correct, click on Confirm button ";
            this.lblPageTitle.Text = "STEP 4 - Account Validation";
        }

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                GetQueryParam();
            }
            //Load Information Page
            //And InitOrderData (create new row automatically)
            base.OnLoad(e);

            //Step 2
            Information_Step.DataSource = this.DataSource;
            //			//Step 3
            Validation_Step.DataSource = this.DataSource;

            //Load Control
            if (!IsPostBack) {
                DisplayManager();
            }
            //Turn off the Val Sum of the page it's already handled
            //by a web user control behind
            valSum.Visible = false;

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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private new void InitControl() {
            //base.HiddenChange = hidChange;
            //base.LabelInstruction = lblInstruction;
            //base.LabelMessage = lblMessage;		
            //base.LabelPageTitle = lblPageTitle;
            //base.LabelSectionTitle = lblSectionTitle;
            this.Information_Step.MatchingAccountsConfirmed += new EventHandler<MatchingAccountsConfirmEventArgs>(Information_Step_MatchingAccountsConfirmed);
            base.ValSummary = valSum;
            this.AccountFormSteps.Add(Information_Step);
            this.AccountFormSteps.Add(Validation_Step);

        }

        void Information_Step_MatchingAccountsConfirmed(object sender, MatchingAccountsConfirmEventArgs e) {
            Validation_Step.DuplicateAccountOverride = true;
            Validation_Step.MatchingAccounts = e.MatchingAccounts;
        }

        private void GetQueryParam() {
            if (Request.QueryString["NoMenu"] != null) {
                int NoMenu = Convert.ToInt32(Request.QueryString["NoMenu"]);
                this.AppItem = (QSPForm.Business.AppItem)NoMenu;

                //Initialization of the Order
                if (Request["OrgID"] != null) {
                    this.OrganizationID = Convert.ToInt32(Request["OrgID"]);
                }
                else if (Request["MDRPID"] != null) {
                    this.MDRPID = Request["MDRPID"].ToString();
                }

                if (Request["FormID"] != null) {
                    this.FormID = Convert.ToInt32(Request["FormID"]);
                }
            }
        }

        private void ReBindPage() {

        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (OrganizationID > 0) {
                lblOrganizationID.Text = OrganizationID.ToString();
                lblOrganizationName.Text = OrganizationName;
                trCampInfoTitle.Visible = true;
                //	lblSectionTitle_NewOrg.Visible = false;
            }
            else {
                //	lblSectionTitle_NewOrg.Visible = true;
                trCampInfoTitle.Visible = false;
            }

            if (this.currentStep.StepItem == QSPForm.Business.AppItem.AccountForm_Step3) {
                SetHeaderAccountInformation();
            }
            else {
                SetHeaderAccountValidation();
            }
        }
    }
}