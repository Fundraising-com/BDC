using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class BusinessFormStep_Information : BaseBusinessFormStep {
        protected FormData dtsForm;
        private CommonUtility util = new CommonUtility();
        protected System.Web.UI.WebControls.Label Label2;
        CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
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

        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.BusinessForm_Step1;
            this.StepItem = QSPForm.Business.AppItem.BusinessForm_Step2;
            this.NextAppItem = QSPForm.Business.AppItem.BusinessForm_Step3;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        public FormData DataSource {
            get {
                return dtsForm;
            }
            set {
                dtsForm = value;
                HeaderDetail.DataSource = dtsForm.Form;
            }
        }

        public override bool Update() {
            bool IsSuccess = false;
            //Account
            HeaderDetail.DataSource = dtsForm.Form;
            IsSuccess = HeaderDetail.UpdateDataSource();

            return IsSuccess;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.BusinessForm_Step1);
            string url = "~/BusinessFormStep_Selection.aspx";
            Response.Redirect(url);
        }

        public override bool ValidateForm() {
            bool IsValid = false;
            IsValid = HeaderDetail.IsValid();
            if (!IsValid) {
                this.Page.MaintainScrollPositionOnPostBack = false;
            }
            return IsValid;
        }
    }
}