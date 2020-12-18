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
    ///		Summary description for OrgStep_Continue.
    /// </summary>
    public partial class OrgStep_Continue : BaseOrganizationFormStep {
        protected System.Web.UI.WebControls.Label lblLabelDeliveryDate;
        protected System.Web.UI.WebControls.Label lblDeliveryDate;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label lblDayLeadTime;
        private dataRef dtsOrganization;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                FillList();
                imgBtnFinish.Attributes.Add("OnClick", "window.close();false;");
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
            this.imgBtnContinue.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnContinue_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step7;
            //			this.ImageButtonBack = imgBtnBack;
            //			this.ImageButtonNext = imgBtnNext;
        }

        public dataRef DataSource {
            get {
                return dtsOrganization;
            }
            set {
                dtsOrganization = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override bool Update() {
            return true;
        }

        public override void BindForm() {
            DataRow orgRow = this.Page.DataSource.Organization.Rows[0];
            int OrgID = Convert.ToInt32(orgRow[OrganizationTable.FLD_PKID]);

            string message = "The organization # " + OrgID.ToString() + " have been saved sucessfully.<br>";

            lblMessageConfirmation.Text = message;
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void FillList() {
        }

        private void imgBtnContinue_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }
    }
}