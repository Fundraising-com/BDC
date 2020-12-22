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
using dataDef = QSPForm.Common.DataDef.ProgramAgreementData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for OrganizationForm.
    /// </summary>
    public partial class ProgramAgreementDetailInfo : BaseWebFormControl {
        private int c_ProgramAgreementID;
        public const string PROGRAM_AGREEMENT_ID = "ProgramAgreementID";
        private const string PROGRAM_AGREEMENT_DATA = "ProgramAgreementData";
        private const string ACCOUNT_DATA = "AccountData";
        protected dataDef dtsProgramAgreement;
        protected AccountData dtsAccount;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	

                LoadData();
                if (!IsPostBack) {
                    BindForm();
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
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_READ;
            QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick);

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int ProgramAgreementID {
            get {
                int progID = 0;
                if (ViewState[PROGRAM_AGREEMENT_ID] != null)
                    progID = Convert.ToInt32(ViewState[PROGRAM_AGREEMENT_ID]);
                return progID;
            }
            set {
                ViewState[PROGRAM_AGREEMENT_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                int ProgTypeID = 0;
                if (dtsAccount.Campaign.Rows.Count > 0) {
                    if (!dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                        ProgTypeID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PROG_TYPE_ID]);
                    if (ProgTypeID == 11 || ProgTypeID == 7) {
                        QSPToolBar.EditButton.Visible = true;
                        string fmID = dtsAccount.Account.Rows[0][AccountTable.FLD_FM_ID].ToString();
                        if (fmID != this.Page.FMID) {
                            QSPToolBar.EditButton.Visible = false;
                        }
                        if (this.Page.Role > QSPForm.Business.AuthSystem.ROLE_FM) {
                            QSPToolBar.EditButton.Visible = true;
                        }
                        if (!QSPToolBar.EditButton.Visible)
                            Page.SetPageMessage("This Program Agreement is not editable");
                    }
                    else {
                        QSPToolBar.EditButton.Visible = false;
                        Page.SetPageMessage("Only Program Agreement running WFC Chocolate Programs or Food Programs can be edited in Order Express at this time");
                    }
                }
                else {
                    QSPToolBar.EditButton.Visible = false;
                    Page.SetPageMessage("The Program Type is missing");
                }
            }
        }

        protected void SetFormParameter() {
            if (Request[PROGRAM_AGREEMENT_ID] != null) {
                c_ProgramAgreementID = Convert.ToInt32(Request[PROGRAM_AGREEMENT_ID].ToString());
            }
            else {
                c_ProgramAgreementID = 0;
            }
            ViewState[PROGRAM_AGREEMENT_ID] = c_ProgramAgreementID;
        }

        protected new void BindForm() {
            ProgramAgreementInfo1.BindForm();
            AuditControlInfo1.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                //ProgramAgreement
                LoadDataSet();
                //Account
                int CampaignID = Convert.ToInt32(dtsProgramAgreement.ProgramAgreementCampaign.Rows[0][ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

                this.ViewState[PROGRAM_AGREEMENT_ID] = c_ProgramAgreementID;
                //this.ViewState[PROGRAM_AGREEMENT_DATA] = dtsProgramAgreement;
                ProgramAgreementInfo1.DataSource = dtsProgramAgreement;
                ProgramAgreementInfo1.AccountDataSource = dtsAccount;
                AuditControlInfo1.ParentType = QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT;
                AuditControlInfo1.DataSource = dtsProgramAgreement.ProgramAgreement;
                AuditControlInfo1.ParentID = c_ProgramAgreementID;
            }
            else {
                c_ProgramAgreementID = Convert.ToInt32(this.ViewState[PROGRAM_AGREEMENT_ID]);
                //dtsProgramAgreement = (dataDef)this.ViewState[PROGRAM_AGREEMENT_DATA];
            }

            //For each load, the page (the higher in the hirarchy)
            //is in charge to set all children's datasource 
        }

        private void LoadDataSet() {
            QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				dtsProgramAgreement = ordSys.SelectAllDetail(c_ProgramAgreementID);
            //			else
            dtsProgramAgreement = prgSys.SelectAllDetail(c_ProgramAgreementID);
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramAgreementDetail, BaseProgramAgreementDetail.PROGRAM_AGREEMENT_ID, c_ProgramAgreementID.ToString());
            string url = "~/ProgramAgreementDetail.aspx?" + BaseProgramAgreementDetail.PROGRAM_AGREEMENT_ID + "=" + c_ProgramAgreementID.ToString();
            Response.Redirect(url);
        }
    }
}