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
    public class BaseBusinessForm : BaseWebForm {
        private FormData dtsFormData;
        private const string FORM_DATA = "FormData";
        protected BaseBusinessFormStep currentStep;
        private BaseWebFormStepCollection businessFormSteps = new BaseWebFormStepCollection();
        private CommonUtility clsUtil = new CommonUtility();
        
        override protected void OnLoad(EventArgs e) {
            //Load Data
            if (!IsPostBack) {
                //Determin the FM if is not in FM Mode				
                QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();

                if (FormID > 0) {

                    this.DataOperation = QSPForm.Common.DataOperation.UPDATE;
                    //Edit Mode
                    dtsFormData = frmSys.SelectAllDetail(FormID);

                    if (dtsFormData.Form.Rows.Count > 0) {
                        DataRow frmRow = dtsFormData.Form.Rows[0];
                        BaseFormID = Convert.ToInt32(frmRow[FormTable.FLD_PARENT_FORM_ID]);
                        FormID = Convert.ToInt32(frmRow[FormTable.FLD_PKID]);
                    }

                }
                else {
                    //Add Mode
                    this.DataOperation = QSPForm.Common.DataOperation.INSERT;

                    FormTable dTblBaseForm = frmSys.SelectOne(this.BaseFormID);
                    if (dTblBaseForm.Rows.Count > 0) {
                        DataRow row = dTblBaseForm.Rows[0];
                        if (!row.IsNull(FormTable.FLD_ENTITY_TYPE_ID))
                            this.EntityTypeID = Convert.ToInt32(row[FormTable.FLD_ENTITY_TYPE_ID]);

                        this.BaseFormCode = row[FormTable.FLD_FORM_CODE].ToString();
                        this.BaseFormName = row[FormTable.FLD_FORM_NAME].ToString();
                    }

                    dtsFormData = frmSys.InitializeForm(this.UserID, this.BaseFormID, this.EntityTypeID);
                }

            }
            else {
                //For each postback, the page (the higher in the hierarchy)
                //is in charge to set all children's datasource 
                dtsFormData = (FormData)this.ViewState[FORM_DATA];


            }
            currentStep = (BaseBusinessFormStep)this.businessFormSteps.FindByAppItem(this.AppItem);
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
        
        public FormData DataSource {
            get {
                return dtsFormData;
            }
            set {
                dtsFormData = value;
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

        public int BaseFormID {
            get {
                if (ViewState["BaseFormID"] != null) {
                    return Convert.ToInt32(ViewState["BaseFormID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["BaseFormID"] = value;
            }
        }

        public int EntityTypeID {
            get {
                if (ViewState["EntityTypeID"] != null) {
                    return Convert.ToInt32(ViewState["EntityTypeID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["EntityTypeID"] = value;
            }
        }

        public string BaseFormCode {
            get {
                if (ViewState["BaseFormCode"] != null) {
                    return ViewState["BaseFormCode"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["BaseFormCode"] = value;
            }
        }

        public string BaseFormName {
            get {
                if (ViewState["BaseFormName"] != null) {
                    return ViewState["BaseFormName"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["BaseFormName"] = value;
            }
        }

        public string FormName {
            get {
                if (ViewState["FormName"] != null) {
                    return ViewState["FormName"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["FormName"] = value;
            }
        }

        public string FormCode {
            get {
                if (ViewState["FormCode"] != null) {
                    return ViewState["FormCode"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["FormCode"] = value;
            }
        }

        public BaseBusinessFormStep CurrentStep {
            get {
                return currentStep;
            }
            set {
                currentStep = value;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            this.ViewState[FORM_DATA] = dtsFormData;

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

        public void GoNextByAppItem(QSPForm.Business.AppItem appItem) {
            if (currentStep.ValidateForm()) {
                currentStep.Update();
                this.AppItem = appItem;
                GoToStepByAppItem();
            }
        }

        public void GoToStepByAppItem(QSPForm.Business.AppItem appItem) {
            this.AppItem = appItem;
            GoToStepByAppItem();
        }

        public void DisplayManager() {
            //Make them all invisible
            for (int iIndex = 0; iIndex < this.businessFormSteps.Count; iIndex++) {
                this.businessFormSteps[iIndex].Visible = false;

            }
            this.CurrentStep = (BaseBusinessFormStep)this.businessFormSteps.FindByAppItem(this.AppItem);
            CurrentStep.Visible = true;
            if (CurrentStep.IsFirstLoad) {
                CurrentStep.BindForm();
                CurrentStep.IsFirstLoad = false;

            }
            //To be always at the beginning of the page when going to another step
            this.Page.MaintainScrollPositionOnPostBack = false;
            this.RefreshPageInformation();
        }

        public BaseWebFormStepCollection BusinessFormSteps {
            get {
                return businessFormSteps;
            }
            set {
                businessFormSteps = value;
            }
        }

        public bool SaveDataSource() {
            bool IsSuccess = false;

            if (dtsFormData.Form.Rows.Count > 0) {
                DataRow ordRow = dtsFormData.Form.Rows[0];
                QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
                IsSuccess = frmSys.UpdateAllDetail(dtsFormData, this.UserID);

            }
            return IsSuccess;
        }
    }
}