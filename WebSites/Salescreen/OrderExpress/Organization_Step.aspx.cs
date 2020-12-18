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
using dataDef = QSPForm.Common.DataDef.OrganizationData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for OrganizationForm.
    /// </summary>
    public partial class Organization_Step : BaseOrganizationForm {
        private int c_OrgID;
        public const string ORG_ID = "OrgID";
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        private const string ORG_DATA = "OrgData";
        protected dataDef dtsOrganization;

        protected void Page_Load(object sender, System.EventArgs e) {

        }

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                GetQueryParam();
            }
            //Load Information Page
            //And InitOrderData (create new row automatically)
            base.OnLoad(e);

            //Step 1
            //MDRSchool_Step.DataSource = this.DataSource.Organization;
            //Step 2
            OrgDetail_Step.DataSource = this.DataSource.Organization;
            //Step 3
            PostalAddress_Step.DataSource = this.DataSource.PostalAddress;
            //Step 4
            PhoneNumber_Step.DataSource = this.DataSource.PhoneNumber;
            //Step 5
            EmailAddress_Step.DataSource = this.DataSource.EmailAddress;
            //Step 6
            Confirmation_Step.DataSource = this.DataSource;
            //Step 7
            Continue_Step.DataSource = this.DataSource;
            //Load Control
            DisplayManager();
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private new void InitControl() {
            base.HiddenChange = hidChange;
            base.LabelInstruction = lblInstruction;
            base.LabelMessage = lblMessage;
            this.OrganizationFormSteps.Add(MDRSchool_Step);
            this.OrganizationFormSteps.Add(OrgDetail_Step);
            this.OrganizationFormSteps.Add(PostalAddress_Step);
            this.OrganizationFormSteps.Add(PhoneNumber_Step);
            this.OrganizationFormSteps.Add(EmailAddress_Step);
            this.OrganizationFormSteps.Add(Confirmation_Step);
            this.OrganizationFormSteps.Add(Continue_Step);
        }

        public int OrgID {
            get {
                return c_OrgID;
            }
            set {
                c_OrgID = value;
                ViewState[ORG_ID] = c_OrgID;
            }
        }

        private void GetQueryParam() {
            if (Request.QueryString["NoMenu"] != null) {
                int NoMenu = Convert.ToInt32(Request.QueryString["NoMenu"]);
                this.AppItem = (QSPForm.Business.AppItem)NoMenu;
            }
        }

        private void ReBindPage() {

        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            imgTitle.ImageUrl = this.ImageTitleURL;
            lblMDRSchoolPID.Text = MDRSchoolPID;
            lblMDRSchoolName.Text = MDRSchoolName;
        }
    }
}