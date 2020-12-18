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
    public class BaseOrganizationForm : BaseWebForm {
        private OrganizationData dtsOrganizationData;
        private const string ORGANIZATION_DATA = "OrganizationData";
        protected BaseOrganizationFormStep currentStep;
        private BaseWebFormStepCollection orgFormSteps = new BaseWebFormStepCollection();
        
        override protected void OnLoad(EventArgs e) {

            //Load Data
            if (!IsPostBack) {
                QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
                dtsOrganizationData = orgSys.InitializeOrganization(this.UserID, this.FMID);
            }
            else {
                //For each postback, the page (the higher in the hierarchy)
                //is in charge to set all children's datasource 
                dtsOrganizationData = (OrganizationData)this.ViewState[ORGANIZATION_DATA];
            }
            currentStep = (BaseOrganizationFormStep)this.orgFormSteps.FindByAppItem(this.AppItem);
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
            this.currentStep.GoToPreviousStep += new System.EventHandler(currentStep_GoToPreviousStep);
            this.currentStep.GoToNextStep += new System.EventHandler(currentStep_GoToNextStep);
        }
        #endregion

        public OrganizationData DataSource {
            get {
                return dtsOrganizationData;
            }
            set {
                dtsOrganizationData = value;
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

        public string MDRSchoolPID {
            get {
                if (ViewState["MDRSchoolPID"] != null) {
                    return ViewState["MDRSchoolPID"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["MDRSchoolPID"] = value;
            }
        }

        public string MDRSchoolName {
            get {
                if (ViewState["MDRSchoolName"] != null) {
                    return ViewState["MDRSchoolName"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["MDRSchoolName"] = value;
            }
        }

        public BaseOrganizationFormStep CurrentStep {
            get {
                return currentStep;
            }
            set {
                currentStep = value;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            this.ViewState[ORGANIZATION_DATA] = dtsOrganizationData;

            base.OnPreRender(e);
        }

        private void currentStep_GoToPreviousStep(object sender, System.EventArgs e) {
            this.AppItem = currentStep.PreviousAppItem;
            GoToStepByAppItem();
        }

        private void currentStep_GoToNextStep(object sender, System.EventArgs e) {
            currentStep.Update();
            this.AppItem = currentStep.NextAppItem;
            GoToStepByAppItem();
        }

        public void GoToStepByAppItem() {
            this.DisplayManager();
        }

        public void GoToStepByAppItem(QSPForm.Business.AppItem appItem) {
            this.AppItem = appItem;
            GoToStepByAppItem();
        }

        public void DisplayManager() {
            for (int iIndex = 0; iIndex < this.orgFormSteps.Count; iIndex++) {
                this.orgFormSteps[iIndex].Visible = false;
            }
            this.CurrentStep = (BaseOrganizationFormStep)this.OrganizationFormSteps.FindByAppItem(this.AppItem);
            CurrentStep.Visible = true;
            if (CurrentStep.IsFirstLoad) {
                CurrentStep.BindForm();
                CurrentStep.IsFirstLoad = false;
            }
            this.RefreshPageInformation();
        }

        public BaseWebFormStepCollection OrganizationFormSteps {
            get {
                return orgFormSteps;
            }
            set {
                orgFormSteps = value;
            }
        }
    }
}