using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class AccountStep_Information : BaseAccountFormStep {
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        CommonUtility clsUtil = new CommonUtility();
        public event EventHandler<ReadOnlyStatusChangedEventArgs> ReadOnlyStatusChanged;
        public event System.EventHandler<MatchingAccountsConfirmEventArgs> MatchingAccountsConfirmed;

        protected void Page_Load(object sender, System.EventArgs e) {

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.imgBtnNext.Click += new System.Web.UI.ImageClickEventHandler(imgBtnNext_Click);
        }

        #endregion

        private void InitControl() {
            this.HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
            this.HeaderDetail.MatchingAccountsConfirmed += new System.EventHandler<MatchingAccountsConfirmEventArgs>(this.HeaderDetail_MatchingAccountsConfirmed);
            this.PreviousAppItem = QSPForm.Business.AppItem.AccountForm_Step2;
            this.StepItem = QSPForm.Business.AppItem.AccountForm_Step3;
            this.NextAppItem = QSPForm.Business.AppItem.AccountForm_Step4;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            OnGoToNextStep(EventArgs.Empty);
        }

        protected void HeaderDetail_MatchingAccountsConfirmed(object sender, MatchingAccountsConfirmEventArgs e) {
            if (MatchingAccountsConfirmed != null) {
                MatchingAccountsConfirmed(sender, e);
            }

            OnGoToNextStep(EventArgs.Empty);
        }

        public AccountData DataSource {
            get {
                return dtsAccount;
            }
            set {
                dtsAccount = value;
                HeaderDetail.DataSource = dtsAccount;
                HeaderDetail.InitializeControls();
            }
        }

        public override bool Update() {
            bool IsSuccess = false;
            //Account
            HeaderDetail.DataSource = dtsAccount;
            IsSuccess = HeaderDetail.UpdateDataSource();

            return IsSuccess;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step2);
            string url = "AccountStep_Selection.aspx?NoMenu=41";
            if (Request["OrgID"] != null) {

                string sOrgId = Request["OrgID"].ToString();
                Response.Redirect(url + "&OrgID=" + sOrgId);
            }
            else if (Request["MDRPID"] != null) {
                //MDR School
                string sMDRPID = Request["MDRPID"].ToString();
                Response.Redirect(url + "&MDRPID=" + sMDRPID);
            }
            else {
                Response.Redirect(url);
            }
        }

        void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            HeaderDetail.ResetStatus();
        }

        public override bool ValidateForm() {
            bool IsValid = false;
            IsValid = HeaderDetail.ValidateForm();
            if (!IsValid) {
                this.Page.MaintainScrollPositionOnPostBack = false;
            }
            return IsValid;
        }
    }
}