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
    ///		Summary description for OrderForm_Step1_1.
    /// </summary>
    public partial class OrderStep_AccountRenewal : BaseWebFormControl {
        private CommonUtility util = new CommonUtility();
        CommonUtility clsUtil = new CommonUtility();
        public const string ACC_ID = "AccID";
        public const string CAMP_ID = "CampID";
        private const string ACC_DATA = "AccData";
        protected dataRef dtsAccount;
        int c_AccID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            try {
                // Put user code to initialize the page here	
                LoadData();
                HeaderDetail.InitializeControls();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
                    BindForm();
                    string msg = "To place an order for an account, it must be renewed first.";
                    //Do an alert explaining why they need this extra step
                    System.Text.StringBuilder strBuild = new System.Text.StringBuilder();
                    strBuild.Append("<script language=javascript>\n");
                    strBuild.Append("<!--			\n");
                    strBuild.Append("	\n");
                    strBuild.Append("		alert('" + msg + "');\n");
                    strBuild.Append("	\n");
                    strBuild.Append("//-->\n");
                    strBuild.Append("</script>");

                    this.Page.RegisterStartupScript("ExplainExtraStep", strBuild.ToString());
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
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.imgBtnNext.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnNext_Click);
            this.HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
        }

        #endregion

        private void InitControl() {

        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ACC_DATA] = dtsAccount;
        }

        void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            GoToNextStep();
        }

        protected void SetFormParameter() {
            if (Request[ACC_ID] != null) {
                c_AccID = Convert.ToInt32(Request[ACC_ID].ToString());
            }
            else {
                c_AccID = 0;
            }
            ViewState[ACC_ID] = c_AccID;
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailWithLastCampaign(c_AccID);
                if (!accSys.IsRenew(dtsAccount.Account))
                    dtsAccount = accSys.RenewAccount(c_AccID, this.Page.UserID);

                this.ViewState[ACC_ID] = c_AccID;
                this.ViewState[ACC_DATA] = dtsAccount;
            }
            else {
                c_AccID = Convert.ToInt32(this.ViewState[ACC_ID]);
                dtsAccount = (dataRef)this.ViewState[ACC_DATA];
            }

            HeaderDetail.AccountID = c_AccID;
            HeaderDetail.DataSource = dtsAccount;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step1);	
            string url = "OrderStep_Selection.aspx";
            Response.Redirect(url);
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            HeaderDetail.ResetStatus();

            GoToNextStep();
        }

        public void GoToNextStep() {
            try {
                Boolean blnValid = true;

                blnValid = HeaderDetail.ValidateForm();
                if (!blnValid) {
                    Page.MaintainScrollPositionOnPostBack = false;
                    return;
                }

                blnValid = HeaderDetail.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                blnValid = accSys.UpdateAllDetail(dtsAccount, this.Page.UserID);

                string sCampID = "";
                sCampID = dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PKID].ToString();

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step2);
                string url = "~/OrderStep_Selection.aspx?";
                Response.Redirect(url + "&CampID=" + sCampID);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}