using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrganizationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrgStep_Confirmation : BaseOrganizationFormStep {
        private CommonUtility util = new CommonUtility();
        protected dataRef dtsOrganization;

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
            InitControl();
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrgForm_Step5;
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step6;
            this.NextAppItem = QSPForm.Business.AppItem.OrgForm_Step7;
            this.ImageButtonNext = imgBtnNext;
            this.ImageButtonBack = imgBtnBack;
        }

        public dataRef DataSource {
            get {
                return dtsOrganization;
            }
            set {
                dtsOrganization = value;
                OrganizationInfo1.DataSource = dtsOrganization;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override bool Update() {
            bool IsSuccess = false;
            try {
                QSPForm.Business.OrganizationSystem orgSys = new QSPForm.Business.OrganizationSystem();
                IsSuccess = orgSys.InsertAllDetail(this.Page.DataSource);
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return IsSuccess;
        }

        public override void BindForm() {
            OrganizationInfo1.DataSource = dtsOrganization;
            OrganizationInfo1.DataBind();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}