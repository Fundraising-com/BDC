using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderStep_AccountInformation : BaseOrderFormStep {
        //protected OrderData dtsOrder;
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        protected System.Web.UI.WebControls.DropDownList ddlOrderForm;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.UPDATE) {
                    imgBtnBack.Visible = false;
                }
            }
            bool IsQcapOrder = this.Page.QCAPOrderID != 0;
            imgBtnBack.Visible = !IsQcapOrder;
            trQCAPOrderIntimation.Visible = IsQcapOrder;
            //			if (this.DataSource != null)
            //				HeaderDetail.DataSource = this.DataSource;
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
        }

        #endregion

        void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            OnGoToNextStep(EventArgs.Empty);
        }

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step2;
            this.StepItem = QSPForm.Business.AppItem.OrderForm_Step3;
            this.NextAppItem = QSPForm.Business.AppItem.OrderForm_Step4;
            //this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        protected override void SetInnerControlDataSource() {
            HeaderDetail.DataSource = this.DataSource;
            HeaderDetail.InitializeControls();
        }

        public override bool Update() {
            //Bill To address
            HeaderDetail.UpdateDataSource();

            return true;
        }

        public override void BindForm() {
            if (IsFirstLoad) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.INSERT) {
                    base.SetDefaultBillingInformation();
                    base.SetDefaultShippingInformation();
                }

            }
            //Pass the datasource to the control
            HeaderDetail.DataSource = this.DataSource;
            HeaderDetail.BindForm();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            // RNK
            //			string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step2);
            string url = "OrderStep_Selection.aspx?";

            Response.Redirect(url + "&CampID=" + Page.CampaignID.ToString());
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            HeaderDetail.ResetStatus();
        }

        public override bool ValidateForm() {
            return HeaderDetail.ValidateForm();
        }
    }
}