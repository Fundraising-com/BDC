using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.EmailEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrganizationDetail.
    /// </summary>
    public partial class OrgStep_EmailAddress : BaseOrganizationFormStep {
        private int c_OrgID = 0;
        protected dataDef dTblEmailAddress;
        private CommonUtility clsUtil = new CommonUtility();

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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrgForm_Step4;
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step5;
            this.NextAppItem = QSPForm.Business.AppItem.OrgForm_Step6;
            this.ImageButtonNext = imgBtnNext;
            this.ImageButtonBack = imgBtnBack;
        }

        private void LoadData() {
            //			dtblOrganization = orgSys.SelectOne(c_OrgID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db					
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int OrganizationID {
            get {
                return c_OrgID;
            }
            set {
                c_OrgID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dTblEmailAddress;
            }
            set {
                dTblEmailAddress = value;
                EmailAddressList_Org.DataSource = dTblEmailAddress;
            }
        }

        public override void BindForm() {
            EmailAddressList_Org.ParentID = 0;
            EmailAddressList_Org.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION;
            EmailAddressList_Org.DataSource = dTblEmailAddress;
            EmailAddressList_Org.DataBind();
        }

        public override bool Update() {
            bool IsSuccess = false;

            EmailAddressList_Org.ParentID = 0;
            EmailAddressList_Org.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION;
            EmailAddressList_Org.DataSource = dTblEmailAddress;
            IsSuccess = EmailAddressList_Org.UpdateDataSource();

            return IsSuccess;
        }

        protected void Page_PreRender(object sender, EventArgs e) {
        }
    }
}