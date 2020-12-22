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
    /// Summary description for MainPage.
    /// </summary>
    public partial class ProgramAgreementStep_Confirmation : BaseWebUserControl {
        public const string CAMP_ID = "CampID";
        public const string PROG_TYPE_ID = "ProgTypeID";
        public const string PROGRAM_AGREEMENT_ID = "ProgramAgreementID";
        private int c_ProgramAgreementID = 0;
        private int c_CampID = 0;
        private int c_ProgTypeID = 0;


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
            this.PreRender += new EventHandler(Page_PreRender);
        }
        #endregion

        override protected void OnLoad(EventArgs e)
        {
            GetQueryParam();
            if (!IsPostBack)
            {
                BindForm();
            }
            else
            {
                c_CampID = Convert.ToInt32(ViewState[CAMP_ID]);
                c_ProgTypeID = Convert.ToInt32(ViewState[PROG_TYPE_ID]);
            }

            base.OnLoad(e);
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.SetCommandLinks();
        }

        private void GetQueryParam()
        {
            if (Request[PROGRAM_AGREEMENT_ID] != null)
            {
                c_ProgramAgreementID = Convert.ToInt32(Request[PROGRAM_AGREEMENT_ID]);
                ViewState[PROGRAM_AGREEMENT_ID] = c_ProgramAgreementID;
            }
        }
        public void BindForm() {
            imgBtnCreditApplication.Visible = false;
            lblCreditAppMsg.Visible = false;

            QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
            ProgramAgreementData dtsProgramAgreement = prgSys.SelectAllDetail(c_ProgramAgreementID);
            DataRow prgRow = dtsProgramAgreement.ProgramAgreement.Rows[0];
            DataRow prgCampRow = dtsProgramAgreement.ProgramAgreementCampaign.Rows[0];

            c_CampID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            AccountTable dTblAccount = accSys.SelectAllByCampaignID(c_CampID);
            DataRow accRow = dTblAccount.Rows[0];
            int AccountID = Convert.ToInt32(accRow[CampaignTable.FLD_ACCOUNT_ID]);
            lblAccountID.Text = AccountID.ToString();

            lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
            if (!accRow.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                lblAccountName.Text = accRow[AccountTable.FLD_FULF_ACCOUNT_ID].ToString() + " - " + lblAccountName.Text;

            int FormID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_FORM_ID]);
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable dTblForm = formSys.SelectOne(FormID);
            DataRow frmRow = dTblForm.Rows[0];
            lblFormID.Text = FormID.ToString();
            lblFormName.Text = frmRow[FormTable.FLD_FORM_NAME].ToString();
            c_ProgTypeID = Convert.ToInt32(frmRow[FormTable.FLD_PROGRAM_TYPE_ID]);

            int StatusID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID]);
            string sStatus = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_NAME].ToString();
            string sStatusCode = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_COLOR_CODE].ToString();
            string sStatus_ShortDescription = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_SHORT_DESCRIPTION].ToString();

            //			if (StatusID == Common.ProgramAgreementStatus.WAIT_FOR_APPROVAL)
            //			{
            //				lblCreditAppMsg.Visible = true;
            //				imgBtnCreditApplication.Visible = true;
            //				CommonUtility clsUtil = new CommonUtility();
            //				clsUtil.SetJScriptForOpenDetail(imgBtnCreditApplication,QSPForm.Business.AppItem.CreditApplicationDetail,CreditApplicationDetail.ACC_ID,AccountID.ToString(),0,0);				
            //			}

            lblProgramAgreementID.Text = c_ProgramAgreementID.ToString();
            lblProgramAgreementStatus_ShortDescription.Text = sStatus_ShortDescription;
            lblProgramAgreementStatusColor.BackColor = Color.FromName(sStatusCode);

            //string message = "The order # " + c_ProgramAgreementID.ToString() + " have been saved sucessfully.<br>" + 
            //	"The status is '" + sStatus + "'"; 
            //			lblMessageConfirmation.Text = message;
            ViewState[CAMP_ID] = c_CampID;
            ViewState[PROG_TYPE_ID] = c_ProgTypeID;
        }
        private void SetCommandLinks()
        {
            this.hlPAPrint.NavigateUrl = string.Format("~/V2/Forms/ProgramAgreementView.aspx?ProgramAgreementId={0}&IsForPrint=true", c_ProgramAgreementID);
            this.hlPACreate.NavigateUrl = string.Format("~/ProgramAgreementStep_Selection.aspx?CampID={0}", c_CampID);
            this.hlPASearch.NavigateUrl = "~/V2/Forms/ProgramAgreementSearch.aspx";
            this.hlOrderCreate.NavigateUrl = string.Format("~/OrderStep_Selection.aspx?CampID={0}", c_CampID);
        }

    }
}