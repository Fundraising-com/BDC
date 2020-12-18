using System;
using System.Web;
using QSPForm.Common.DataDef;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>Base page for Web Form pages in QSPForm_Web</summary>
    /// <remarks>
    ///		Inherit from BasePage
    ///		We used this class to manage common functionnality
    ///		for DataGrid by example
    ///	</remarks>
    public class BaseAccountForm : BaseWebForm {
        private AccountData dtsAccountData;
        private const string ACCOUNT_DATA = "AccountData";
        protected BaseAccountFormStep currentStep;
        private BaseWebFormStepCollection accountFormSteps = new BaseWebFormStepCollection();

        override protected void OnLoad(EventArgs e) {

            //Load Data
            if (!IsPostBack) {
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccountData = accSys.InitializeAccount(this.UserID, this.FMID, this.OrganizationID, this.MDRPID, this.FormID);
                DataRow AccRow = dtsAccountData.Account.Rows[0];
                this.OrganizationName = AccRow[AccountTable.FLD_NAME].ToString();
            }
            else {
                //For each postback, the page (the higher in the hierarchy)
                //is in charge to set all children's datasource 
                dtsAccountData = (AccountData)this.ViewState[ACCOUNT_DATA];
            }
            currentStep = (BaseAccountFormStep)this.accountFormSteps.FindByAppItem(this.AppItem);
            InitializeComponent();
            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //InitializeComponent();			
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            if (this.currentStep != null) {
                this.currentStep.GoToPreviousStep += new System.EventHandler(currentStep_GoToPreviousStep);
                this.currentStep.GoToNextStep += new System.EventHandler(currentStep_GoToNextStep);
            }
        }
        #endregion

        public AccountData DataSource {
            get {
                return dtsAccountData;
            }
            set {
                dtsAccountData = value;
            }
        }

        public int AccountID {
            get {
                if (ViewState["AccountID"] != null) {
                    return Convert.ToInt32(ViewState["AccountID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["AccountID"] = value;
            }
        }

        public int OrganizationID {
            get {
                if (ViewState["OrganizationID"] != null) {
                    return Convert.ToInt32(ViewState["OrganizationID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["OrganizationID"] = value;
            }
        }

        public string OrganizationName {
            get {
                if (ViewState["OrganizationName"] != null) {
                    return ViewState["OrganizationName"].ToString();
                }
                else {
                    return "Unspecified";
                }
            }
            set {
                ViewState["OrganizationName"] = value;
            }
        }

        public string MDRPID {
            get {
                if (ViewState["MDRPID"] != null) {
                    return ViewState["MDRPID"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["MDRPID"] = value;
            }
        }

        public int FormID {
            get {
                if (ViewState["FormID"] != null) {
                    return Convert.ToInt32(ViewState["FormID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["FormID"] = value;
            }
        }

        public BaseAccountFormStep CurrentStep {
            get {
                return currentStep;
            }
            set {
                currentStep = value;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            this.ViewState[ACCOUNT_DATA] = dtsAccountData;

            base.OnPreRender(e);
        }

        private void currentStep_GoToPreviousStep(object sender, System.EventArgs e) {
            this.AppItem = currentStep.PreviousAppItem;
            GoToStepByAppItem();
        }

        private void currentStep_GoToNextStep(object sender, System.EventArgs e) {
            if (currentStep.ValidateForm()) {
                currentStep.Update();
                this.AppItem = currentStep.NextAppItem;
                GoToStepByAppItem();
            }
        }

        public void GoToStepByAppItem() {
            this.DisplayManager();
        }

        public void GoToStepByAppItem(QSPForm.Business.AppItem appItem) {
            this.AppItem = appItem;
            GoToStepByAppItem();
        }

        public void DisplayManager() {
            for (int iIndex = 0; iIndex < this.accountFormSteps.Count; iIndex++) {
                this.accountFormSteps[iIndex].Visible = false;
            }
            this.CurrentStep = (BaseAccountFormStep)this.AccountFormSteps.FindByAppItem(this.AppItem);
            CurrentStep.Visible = true;
            if (CurrentStep.IsFirstLoad) {
                CurrentStep.BindForm();
                CurrentStep.IsFirstLoad = false;
            }
            ////To be always at the beginning of the page when going to another step
            this.Page.MaintainScrollPositionOnPostBack = false;
            this.RefreshPageInformation();
        }

        public BaseWebFormStepCollection AccountFormSteps {
            get {
                return accountFormSteps;
            }
            set {
                accountFormSteps = value;
            }
        }
    }
}