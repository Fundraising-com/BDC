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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class ProgramAgreementForm_Step : BaseProgramAgreementForm {
        private const string UserControlsPath = "~/UserControls/";

        private BaseProgramAgreementFormStep programAgreementFormStep;

        override protected void OnInit(EventArgs e) {
            try {
                if (!IsPostBack) {
                    CurrentStep = ProgramAgreementStep.PAInformation;
                }

                LoadControl(false);

                base.OnInit(e);
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        override protected void OnLoad(EventArgs e) {
            try {
                //Load Information Page
                //And InitProgramAgreementData (create new row automatically)
                //Retreive also the Account Data Stuff and renew the account if needed
                //Load Data
                if (!IsPostBack) {
                    InitializeProgramAgreement();
                    MaintainScrollPositionOnPostBack = false;
                }

                SetHeader();

                base.OnLoad(e);

                programAgreementFormStep.DataSource = this.DataSource;

                if (!IsPostBack) {
                    programAgreementFormStep.BindForm();
                }

            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        protected override void OnPreRender(EventArgs e) {
            try {
                base.OnPreRender(e);
                /*lblAccountNumber.Text = "";
                lblAccountName.Text = "Unspecified";
                lblFormID.Text = "";
                lblFormName.Text = "Unspecified";
                if (CampaignID >0)
                {
                    lblAccountNumber.Text = AccountNumber;	
                    if (AccountName.Length >0)
                        lblAccountName.Text = AccountName;
                    trAccountInfoTitle.Visible = true;
                }	
                else
                {
                    trAccountInfoTitle.Visible = false;
                }
                if (FormID >0)
                {
                    lblFormID.Text = FormID.ToString();
                    if (FormName.Length >0)
                        lblFormName.Text = FormName;
                    if (FormImageURL.Length > 0)
                    {
                        imgBusinessForm.ImageUrl = FormImageURL;
                        imgBusinessForm.Visible = true;
                    }    
                    else
                        imgBusinessForm.Visible = false;

                    trFormInfoTitle.Visible = true;
                }
                else
                {
                    trFormInfoTitle.Visible = false;
                    imgBusinessForm.Visible = false;
                }

                */
                SetHeader();
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        private void programAgreementFormStep_GoToPreviousStep(object sender, System.EventArgs e) {
            try {
                GoToPreviousStep();
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        private void programAgreementFormStep_GoToNextStep(object sender, System.EventArgs e) {
            try {
                GoToNextStep();
            }
            catch (Exception ex) {
                SetPageError(ex);
            }
        }

        void programAgreementFormStep_Saving(object sender, EventArgs e) {
            if (!SaveDataSource()) {
                throw new Exception("Program Agreement Update Failed.");
            }
        }

        protected override object LoadPageStateFromPersistenceMedium() {
            return base.LoadPageStateFromPersistenceMedium();
        }

        protected override void SavePageStateToPersistenceMedium(object state) {
            base.SavePageStateToPersistenceMedium(state);
        }

        public ProgramAgreementData DataSource {
            get {
                ProgramAgreementData programAgreementData;

                if (ViewState["ProgramAgreementData"] != null) {
                    programAgreementData = (ProgramAgreementData)ViewState["ProgramAgreementData"];
                }
                else {
                    programAgreementData = new ProgramAgreementData();
                }

                return programAgreementData;
            }
            set {
                ViewState["ProgramAgreementData"] = value;
            }
        }

        private int InputProgramAgreementID {
            get {
                int inputProgramAgreementID = 0;

                if (Request.QueryString["ProgramAgreementID"] != null) {
                    Int32.TryParse(Request.QueryString["ProgramAgreementID"], out inputProgramAgreementID);
                }

                return inputProgramAgreementID;
            }
        }

        private int InputFormID {
            get {
                int inputFormID = 0;

                if (Request.QueryString["FormID"] != null) {
                    Int32.TryParse(Request.QueryString["FormID"], out inputFormID);
                }

                return inputFormID;
            }
        }

        private int InputProgramID {
            get {
                int inputProgramID = 0;

                if (Request.QueryString["ProgramID"] != null) {
                    Int32.TryParse(Request.QueryString["ProgramID"], out inputProgramID);
                }

                return inputProgramID;
            }
        }

        private int InputCampaignID {
            get {
                int inputCampaignID = 0;

                if (Request.QueryString["CampID"] != null) {
                    Int32.TryParse(Request.QueryString["CampID"], out inputCampaignID);
                }

                return inputCampaignID;
            }
        }

        public int ProgramAgreementID {
            get {
                int programAgreementID = 0;

                if (ViewState["ProgramAgreementID"] != null) {
                    programAgreementID = Convert.ToInt32(ViewState["ProgramAgreementID"]);
                }
                else {
                    programAgreementID = InputProgramAgreementID;
                }

                return programAgreementID;
            }
            set {
                ViewState["ProgramAgreementID"] = value;
            }
        }

        public int CampaignID {
            get {
                int campaignID = 0;

                if (ViewState["CampaignID"] != null) {
                    campaignID = Convert.ToInt32(ViewState["CampaignID"]);
                }
                else {
                    campaignID = InputCampaignID;
                }

                return campaignID;
            }
            set {
                ViewState["CampaignID"] = value;
            }
        }

        public int ProgramID {
            get {
                int programID = 0;

                if (ViewState["ProgramID"] != null) {
                    programID = Convert.ToInt32(ViewState["ProgramID"]);
                }
                else {
                    programID = InputProgramID;
                }

                return programID;
            }
            set {
                ViewState["ProgramID"] = value;
            }
        }

        public string AccountNumber {
            get {
                string accountNumber = String.Empty;

                if (ViewState["AccountNumber"] != null) {
                    accountNumber = ViewState["AccountNumber"].ToString();
                }

                return accountNumber;
            }
            set {
                ViewState["AccountNumber"] = value;
            }
        }

        public string AccountName {
            get {
                string accountName = String.Empty;

                if (ViewState["AccountName"] != null) {
                    AccountName = ViewState["AccountName"].ToString();
                }

                return accountName;
            }
            set {
                ViewState["AccountName"] = value;
            }
        }

        public string FormName {
            get {
                string formName = String.Empty;

                if (ViewState["FormName"] != null) {
                    formName = ViewState["FormName"].ToString();
                }

                return formName;
            }
            set {
                ViewState["FormName"] = value;
            }
        }

        public string FormCode {
            get {
                string formCode = String.Empty;

                if (ViewState["FormCode"] != null) {
                    formCode = ViewState["FormCode"].ToString();
                }

                return formCode;
            }
            set {
                ViewState["FormCode"] = value;
            }
        }

        public int FormID {
            get {
                int formID = 0;

                if (ViewState["FormID"] != null) {
                    formID = Convert.ToInt32(ViewState["FormID"]);
                }
                else {
                    formID = InputFormID;
                }

                return formID;
            }
            set {
                ViewState["FormID"] = value;
            }
        }

        public string FormImageURL {
            get {
                string formImageURL = String.Empty;

                if (ViewState["FormImageURL"] != null) {
                    formImageURL = ViewState["FormImageURL"].ToString();
                }

                return formImageURL;
            }
            set {
                ViewState["FormImageURL"] = value;
            }
        }

        public ProgramAgreementStep CurrentStep {
            get {
                ProgramAgreementStep currentStep = ProgramAgreementStep.PAInformation;

                if (Session["ProgramAgreementForm_Step_CurrentStep"] != null) {
                    currentStep = (ProgramAgreementStep)Session["ProgramAgreementForm_Step_CurrentStep"];
                }

                return currentStep;
            }
            set {
                Session["ProgramAgreementForm_Step_CurrentStep"] = value;
            }
        }

        private void InitializeProgramAgreement() {
            //Fill the Business ProgramAgreement Form Information
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            QSPForm.Common.DataDef.FormTable dTblForm = formSys.SelectOne(FormID);
            if (dTblForm.Rows.Count > 0) {
                DataRow formRow = dTblForm.Rows[0];
                FormCode = formRow[FormTable.FLD_FORM_CODE].ToString();
                FormName = formRow[FormTable.FLD_FORM_NAME].ToString();
                FormImageURL = formRow[FormTable.FLD_IMAGE_URL].ToString();
                if (!formRow.IsNull(FormTable.FLD_PROGRAM_ID))
                    this.ProgramID = Convert.ToInt32(formRow[FormTable.FLD_PROGRAM_ID]);
            }

            if (ProgramAgreementID > 0) {
                LoadDataSource();
            }
            else {
                //Add Mode
                this.DataOperation = QSPForm.Common.DataOperation.INSERT;
                string sFMID = this.FMID;
                QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
                DataSource = prgSys.InitializeProgramAgreement(this.UserID, sFMID, this.CampaignID, this.ProgramID, this.FormID);
            }

            //Account data load
            AccountTable dTblAccount = new AccountTable();
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            dTblAccount = accSys.SelectAllByCampaignID(this.CampaignID);

            if (dTblAccount.Rows.Count > 0) {
                DataRow accRow = dTblAccount.Rows[0];
                AccountNumber = accRow[AccountTable.FLD_PKID].ToString();
                AccountName = accRow[AccountTable.FLD_NAME].ToString();
                DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_CUSTOMER_ID] = accRow[AccountTable.FLD_CUSTOMER_ID];
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = programAgreementFormStep.InstructionText;
            this.Header.SectionText = programAgreementFormStep.SectionText;
            this.Header.PageText = programAgreementFormStep.PageText;
            this.Header.IconImage = programAgreementFormStep.IconImage;
            this.Header.IconImageVisiblilty = programAgreementFormStep.IconImageVisibility;
            this.LabelMessage = this.Master.LabelMessage1;
        }

        private void LoadControl(bool dataBind) {
            programAgreementFormStep = (BaseProgramAgreementFormStep)LoadControl(UserControlsPath + "ProgramAgreementStep_" + CurrentStep.ToString() + ".ascx");
            programAgreementFormStep.ID = "ProgramAgreementFormStep" + CurrentStep;

            programAgreementFormStep.GoToPreviousStep += new System.EventHandler(programAgreementFormStep_GoToPreviousStep);
            programAgreementFormStep.GoToNextStep += new System.EventHandler(programAgreementFormStep_GoToNextStep);
            programAgreementFormStep.Saving += new EventHandler(programAgreementFormStep_Saving);

            programAgreementFormStep.DataSource = this.DataSource;

            plHoldBodyPage.Controls.Clear();
            plHoldBodyPage.Controls.Add(programAgreementFormStep);

            if (dataBind) {
                programAgreementFormStep.BindForm();
            }
        }

        private void GoToNextStep() {
            if (programAgreementFormStep.ValidateForm()) {
                programAgreementFormStep.Update();

                switch (CurrentStep) {
                    case ProgramAgreementStep.PAInformation: {
                            if (DataSource.OrderSupply.Rows.Count > 0)
                                CurrentStep = ProgramAgreementStep.DetailSupplyItem;
                            else
                                CurrentStep = ProgramAgreementStep.Validation;

                            LoadControl(true);

                            break;
                        }
                    case ProgramAgreementStep.DetailSupplyItem: {
                            CurrentStep = ProgramAgreementStep.Validation;
                            LoadControl(true);

                            break;
                        }
                    case ProgramAgreementStep.Validation: {
                            string url = "~/ProgramAgreementStep_Confirmation.aspx?";
                            Response.Redirect(url +
                                "ProgramAgreementID=" + ProgramAgreementID.ToString() +
                                "&ProgTypeID=11" +
                                "&CampID=" + CampaignID.ToString());

                            break;
                        }
                }
            }
        }

        public void GoToPreviousStep() {
            switch (CurrentStep) {
                case ProgramAgreementStep.PAInformation: {
                        string url = "~/ProgramAgreementStep_Selection.aspx?";
                        Response.Redirect(url + "CampID=" + CampaignID.ToString());

                        break;
                    }
                case ProgramAgreementStep.DetailSupplyItem: {
                        CurrentStep = ProgramAgreementStep.PAInformation;
                        LoadControl(true);

                        break;
                    }
                case ProgramAgreementStep.Validation: {
                        if (DataSource.OrderSupply.Rows.Count > 0)
                            CurrentStep = ProgramAgreementStep.DetailSupplyItem;
                        else
                            CurrentStep = ProgramAgreementStep.PAInformation;

                        LoadControl(true);

                        break;
                    }
            }
        }

        public void LoadDataSource() {
            this.DataOperation = QSPForm.Common.DataOperation.UPDATE;
            QSPForm.Business.ProgramAgreementSystem progSys = new QSPForm.Business.ProgramAgreementSystem();

            DataSource = progSys.SelectAllDetail(ProgramAgreementID);

            if (DataSource.ProgramAgreement.Rows.Count > 0) {
                DataRow prgRow = DataSource.ProgramAgreement.Rows[0];
                DataRow prgCampRow = DataSource.ProgramAgreementCampaign.Rows[0];

                CampaignID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
                if (!prgCampRow.IsNull(ProgramAgreementCampaignTable.FLD_PROGRAM_ID))
                    ProgramID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_PROGRAM_ID]);
                FormID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_FORM_ID]);
            }
        }

        public bool SaveDataSource() {
            bool isSuccess = false;

            if (DataSource.ProgramAgreement.Rows.Count > 0) {
                DataRow prgRow = DataSource.ProgramAgreement.Rows[0];
                QSPForm.Business.ProgramAgreementSystem progSys = new QSPForm.Business.ProgramAgreementSystem();
                int progID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);
                if (progID <= 0)
                    isSuccess = progSys.InsertAllDetail(DataSource, this.UserID);
                else
                    isSuccess = progSys.UpdateAllDetail(DataSource, this.UserID);
                progID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);
                this.ProgramAgreementID = progID;
            }
            return isSuccess;
        }
    }
}