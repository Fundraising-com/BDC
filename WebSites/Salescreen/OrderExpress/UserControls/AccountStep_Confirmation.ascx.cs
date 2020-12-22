using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountStep_Continue.
    /// </summary>
    public partial class AccountStep_Confirmation : BaseWebUserControl {
        protected System.Web.UI.WebControls.Label lblLabelDeliveryDate;
        protected System.Web.UI.WebControls.Label lblDeliveryDate;
        protected System.Web.UI.WebControls.Label lblDayLeadTime;
        private dataRef dtsAccount;
        public const string ACC_ID = "AccID";
        public const string CAMP_ID = "CampID";
        public const string PROG_TYPE_ID = "ProgTypeID";

        private int c_AccID = 0;
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        protected void SetFormParameter() {
            if (Request[ACC_ID] != null) {
                c_AccID = Convert.ToInt32(Request[ACC_ID].ToString());
            }
            else {
                c_AccID = 0;
            }
            ViewState[ACC_ID] = c_AccID;
        }

        public dataRef DataSource
        {
            get
            {
                return dtsAccount;
            }
            set
            {
                dtsAccount = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here			
            if (!IsPostBack)
            {
                SetFormParameter();
                BindForm();
            }
            else
            {
                c_CampID = Convert.ToInt32(ViewState[CAMP_ID]);
                c_AccID = Convert.ToInt32(ViewState[ACC_ID]);
                c_ProgTypeID = Convert.ToInt32(ViewState[PROG_TYPE_ID]);
                //dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            }

            this.SetCommandLinks();
        }
        protected void Page_PreRender(object sender, System.EventArgs e)
        {

        }

        public void BindForm() {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            dtsAccount = accSys.SelectAllDetailWithLastCampaign(c_AccID);

            DataRow row = dtsAccount.Account.Rows[0];
            string AccountName = row[AccountTable.FLD_NAME].ToString();
            DataRow campRow = dtsAccount.Campaign.Rows[0];
            string ProgName = row[CampaignTable.FLD_PROG_TYPE_NAME].ToString();

            string message = "The following account has been saved:<BR> " +
                             "QSP Account ID #: " + c_AccID.ToString() + " <BR> " +
                             "Account Name : " + AccountName + " <BR> " +
                             "QSP Program : " + ProgName;
            lblMessageConfirmation.Text = message;

            //Look for Credit Application Exception
            EntityExceptionTable dTblEsc = dtsAccount.AccountException;
            DataView dvExc = new DataView(dTblEsc);
            dvExc.RowFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " = " + Convert.ToInt32(QSPForm.Common.BusinessExceptionType.CreditApplication).ToString();
            if (dvExc.Count > 0) {
                tblCreditApp.Visible = true;
                lblCreditAppMsg.Text = dvExc[0][EntityExceptionTable.FLD_MESSAGE].ToString();
                CommonUtility clsUtil = new CommonUtility();
                //	clsUtil.SetJScriptForOpenDetail(imgBtnCreditApplication, QSPForm.Business.AppItem.CreditApplicationDetail,CreditApplicationDetail.ACC_ID,c_AccID.ToString(),0,0);
                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnCreditApplication, "CreditApplicationDetail.aspx?", CreditApplicationDetail.ACC_ID, c_AccID.ToString(), 0, 0);
            }
            else {
                tblCreditApp.Visible = false;
            }

            row = dtsAccount.Campaign.Rows[0];
            c_CampID = Convert.ToInt32(row[CampaignTable.FLD_PKID]);
            c_ProgTypeID = Convert.ToInt32(row[CampaignTable.FLD_PROG_TYPE_ID]);
            ViewState[CAMP_ID] = c_CampID;
            ViewState[PROG_TYPE_ID] = c_ProgTypeID;

            //QSPForm.Business.BusinessCalendarSystem calSys = new QSPForm.Business.BusinessCalendarSystem();
            //if (Convert.ToInt32(row[CampaignTable.FLD_FISCAL_YEAR]) > calSys.GetFiscalYear())
            //    imgBtnEnterNewOrder.Attributes.Add("onclick", "alert('Account Scheduled For Next Fiscal Year - Cannot Accept Order Until Then.');return false;");
        }
        private void SetCommandLinks()
        {
            this.hlAccountPrint.NavigateUrl = string.Format("~/V2/Forms/AccountView.aspx?AccountId={0}&IsForPrint=true", c_AccID);
            this.hlAccountCreate.NavigateUrl = string.Format("~/AccountStep_Selection.aspx?CampID={0}", c_CampID);
            this.hlAccountSearch.NavigateUrl = "~/V2/Forms/AccountSearch.aspx";
            this.hlOrderCreate.NavigateUrl = string.Format("~/OrderStep_Selection.aspx?CampID={0}", c_CampID);
        }
    }
}