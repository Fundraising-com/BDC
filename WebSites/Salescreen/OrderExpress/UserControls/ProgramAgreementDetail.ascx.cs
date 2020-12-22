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

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class ProgramAgreementDetail : BaseWebFormControl {
        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here
                ProgramAgreementInfoControl.IsExceptionReadOnly = false;
                LoadData();
                HeaderDetail.InitializeControls();

                if (!IsPostBack) {
                    BindForm();
                    SetVisibility();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new EventHandler(Page_Load);
            this.DeleteButton.Click += new ImageClickEventHandler(DeleteButton_Click);
            this.ValidateButton.Click += new ImageClickEventHandler(ValidateButton_Click);
            this.BackButton.Click += new ImageClickEventHandler(BackButton_Click);
            this.ProceedButton.Click += new ImageClickEventHandler(ProceedButton_Click);
            this.SaveForLaterButton.Click += new ImageClickEventHandler(SaveForLaterButton_Click);
            HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
        }

        #endregion

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e) {
            try {
                CancelPA();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void ValidateButton_Click(object sender, ImageClickEventArgs e) {
            try {
                HeaderDetail.ResetStatus();

                DisplayValidation();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e) {
            try {
                EditionRow.Visible = true;
                EditionButtonRow.Visible = true;
                ValidationRow.Visible = false;
                ValidationButtonRow.Visible = false;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void ProceedButton_Click(object sender, ImageClickEventArgs e) {
            try {
                DataSource.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)QSPForm.Common.ProgramAgreementStatus.InProcess;

                Save();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void SaveForLaterButton_Click(object sender, ImageClickEventArgs e) {
            try {
                DataSource.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = (int)QSPForm.Common.ProgramAgreementStatus.SavedForLater;

                Save();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            DisplayValidation();
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

        public int ProgramAgreementID {
            get {
                int programAgreementID = InputProgramAgreementID;

                if (ViewState["ProgramAgreementID"] != null) {
                    programAgreementID = Convert.ToInt32(ViewState["ProgramAgreementID"]);
                }

                return programAgreementID;
            }
            set {
                ViewState["ProgramAgreementID"] = value;
            }
        }

        private ProgramAgreementData DataSource {
            get {
                ProgramAgreementData dataSource = null;

                if (ViewState["ProgramAgreementData"] != null) {
                    dataSource = (ProgramAgreementData)ViewState["ProgramAgreementData"];
                }

                return dataSource;
            }
            set {
                ViewState["ProgramAgreementData"] = value;
            }
        }

        private AccountData AccountDataSource {
            get {
                AccountData accountDataSource = null;

                if (ViewState["AccountData"] != null) {
                    accountDataSource = (AccountData)ViewState["AccountData"];
                }

                return accountDataSource;
            }
            set {
                ViewState["AccountData"] = value;
            }
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                QSPForm.Business.ProgramAgreementSystem programAgreementSystem = new QSPForm.Business.ProgramAgreementSystem();
                QSPForm.Business.AccountSystem accountSystem = new QSPForm.Business.AccountSystem();

                DataSource = programAgreementSystem.SelectAllDetail(ProgramAgreementID);

                if (DataSource.ProgramAgreementCampaign.Rows.Count > 0) {
                    DataRow programAgreementCampaignRow = DataSource.ProgramAgreementCampaign.Rows[0];
                    if (!programAgreementCampaignRow.IsNull(ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID)) {
                        int campaignID = Convert.ToInt32(programAgreementCampaignRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);

                        AccountDataSource = accountSystem.SelectAllDetailByCampaignID(campaignID);
                    }
                }
            }

            HeaderDetail.DataSource = DataSource;
            ProgramAgreementInfoControl.DataSource = DataSource;
            ProgramAgreementInfoControl.AccountDataSource = AccountDataSource;
        }

        private void SetVisibility() {
            int programAgreementStatusID = 0;
            string fulfProgramAgreementID = String.Empty;

            EditionRow.Visible = true;
            EditionButtonRow.Visible = true;
            ValidationRow.Visible = false;
            ValidationButtonRow.Visible = false;

            programAgreementStatusID = Convert.ToInt32(DataSource.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID]);

            if (!DataSource.ProgramAgreement.Rows[0].IsNull(ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID)) {
                fulfProgramAgreementID = DataSource.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_FULF_PROGRAM_AGREEMENT_ID].ToString();
            }

            SaveForLaterButton.Visible = (programAgreementStatusID < (int)QSPForm.Common.ProgramAgreementStatus.InProcess && fulfProgramAgreementID == String.Empty);
        }

        private void DisplayValidation() {
            if (HeaderDetail.ValidateForm()) {
                if (HeaderDetail.UpdateDataSource()) {
                    ProgramAgreementInfoControl.BindForm();
                    Page.MaintainScrollPositionOnPostBack = false;

                    EditionRow.Visible = false;
                    EditionButtonRow.Visible = false;
                    ValidationRow.Visible = true;
                    ValidationButtonRow.Visible = true;
                }
            }
        }

        private void Save() {
            if (HeaderDetail.UpdateDataSource()) {
                QSPForm.Business.ProgramAgreementSystem programAgreementSystem = new QSPForm.Business.ProgramAgreementSystem();

                if (programAgreementSystem.UpdateAllDetail(DataSource, this.Page.UserID)) 
                {
                    Response.Redirect("~/V2/Forms/ProgramAgreementView.aspx?ProgramAgreementId=" + ProgramAgreementID.ToString(), true);
                }
            }
        }

        private void CancelPA() {
            QSPForm.Business.ProgramAgreementSystem programAgreementSystem = new QSPForm.Business.ProgramAgreementSystem();

            programAgreementSystem.SetCancelStatus(DataSource);

            if (programAgreementSystem.UpdateAllDetail(DataSource, this.Page.UserID)) 
            {
                Response.Redirect("~/V2/Forms/ProgramAgreementSearch.aspx", true);
            }
        }
    }
}