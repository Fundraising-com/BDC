using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.ProgramAgreementData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProgramAgreementForm_Step1.
    /// </summary>
    public partial class ProgramAgreementStep_PAInformation : BaseProgramAgreementFormStep {
        //protected ProgramAgreementData dtsProgramAgreement;
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        protected System.Web.UI.WebControls.DropDownList ddlProgramAgreementForm;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.UPDATE) {
                    imgBtnBack.Visible = false;
                }
            }
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
            this.Load += new EventHandler(Page_Load);
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.imgBtnNext.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnNext_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            HeaderDetail.AddressHygieneConfirmed += new EventHandler(HeaderDetail_AddressHygieneConfirmed);
            this.PreviousAppItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step2;
            this.StepItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step3;
            this.NextAppItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step4;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        void HeaderDetail_AddressHygieneConfirmed(object sender, EventArgs e) {
            OnGoToNextStep(EventArgs.Empty);
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProgramAgreementForm_Step2);
            //			url = "OrderStep_Selection.aspx?NoMenu=" + Convert.ToInt32(QSPForm.Business.AppItem.OrderForm_Step2).ToString();

            /*string url = "~/ProgramAgreementStep_Selection.aspx?";
            Response.Redirect(url + "&CampID=" + Page.CampaignID.ToString());*/
        }

        private void imgBtnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            HeaderDetail.ResetStatus();
            /*string url = "~/ProgramAgreementStep_Validation.aspx?";
            Response.Redirect(url + "&CampID=" + Page.CampaignID.ToString());*/
        }

        public override string InstructionText {
            get {
                return "<p>Before placing an order, review Account and Program agreement Information below.  In Add New Program Agreement, modifications to Account Information can <u>only</u> be made to the `Ship To' and will <u>only</u> impact <u>this</u> order.  To permanently update the `Bill To' and/or `Ship To' in the system for this Account, please go to Account List, under Directory [Menu Bar] and modify the data accordingly.</p>";
            }
        }

        public override string SectionText {
            get {
                return "Add New Program Agreement :";
            }
        }

        public override string PageText {
            get {
                return "STEP 3 - PA Information";
            }
        }

        public override string IconImage {
            get {
                return "~/images/icon_Account.gif";
            }
        }

        public override bool IconImageVisibility {
            get {
                return true;
            }
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

            }
            //Pass the datasource to the control
            HeaderDetail.DataSource = this.DataSource;
            HeaderDetail.BindForm();
        }

        public override bool ValidateForm() {
            return HeaderDetail.ValidateForm();
        }
    }
}