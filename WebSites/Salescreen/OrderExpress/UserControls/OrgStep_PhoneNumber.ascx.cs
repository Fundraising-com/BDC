using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PhoneNumberEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrganizationDetail.
    /// </summary>
    public partial class OrgStep_PhoneNumber : BaseOrganizationFormStep {
        private int c_OrgID = 0;
        protected dataDef dTblPhoneNumber;
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
            this.PreviousAppItem = QSPForm.Business.AppItem.OrgForm_Step3;
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step4;
            this.NextAppItem = QSPForm.Business.AppItem.OrgForm_Step5;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
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
                return dTblPhoneNumber;
            }
            set {
                dTblPhoneNumber = value;
                PhoneNumberList_Org.DataSource = dTblPhoneNumber;
            }
        }

        public override void BindForm() {
            PhoneNumberList_Org.ParentID = 0;
            PhoneNumberList_Org.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION;
            PhoneNumberList_Org.DataSource = dTblPhoneNumber;
            PhoneNumberList_Org.DataBind();
        }

        public override bool Update() {
            bool IsSuccess = false;

            PhoneNumberList_Org.ParentID = 0;
            PhoneNumberList_Org.ParentType = QSPForm.Common.EntityType.TYPE_ORGANIZATION;
            PhoneNumberList_Org.DataSource = dTblPhoneNumber;
            IsSuccess = PhoneNumberList_Org.UpdateDataSource();

            return IsSuccess;
        }

        protected void Page_PreRender(object sender, EventArgs e) {
        }
    }
}