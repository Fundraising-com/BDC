using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrgStep_MDRSchool : BaseOrganizationFormStep {
        private CommonUtility util = new CommonUtility();
        protected OrganizationTable dtblOrganization;
        private const string ORDER_HEADER = "OrderHeader";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
            }

            util.SetJScriptForOpenSelector(imgBtnSelect, txtMDRSchoolPID, txtMDRSchoolName, QSPForm.Business.AppItem.MDRSchoolSelector, 0, 0);
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
            this.StepItem = QSPForm.Business.AppItem.OrgForm_Step1;
            this.NextAppItem = QSPForm.Business.AppItem.OrgForm_Step2;
            this.ImageButtonNext = imgBtnNext;
        }

        public OrganizationTable DataSource {
            get {
                return dtblOrganization;
            }
            set {
                dtblOrganization = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override bool Update() {
            if (txtMDRSchoolPID.Text.Length > 0) {
                string MDRSchoolPID = txtMDRSchoolPID.Text.Trim();
                dtblOrganization.Rows[0][OrganizationTable.FLD_MDRPID] = MDRSchoolPID;
                this.Page.MDRSchoolPID = MDRSchoolPID;
                this.Page.MDRSchoolName = txtMDRSchoolName.Text;

                base.SetDefaultInformation();
            }
            return true;
        }

        public override void BindForm() {
            if (dtblOrganization.Rows[0][OrganizationTable.FLD_MDRPID] != DBNull.Value) {
                string MDRSchoolPID = dtblOrganization.Rows[0][OrganizationTable.FLD_MDRPID].ToString();
                if (MDRSchoolPID.Length > 0) {
                    QSPForm.Business.MDRSystem mdrSys = new QSPForm.Business.MDRSystem();
                    CMDRTable dTblMDRSchool;
                    dTblMDRSchool = mdrSys.SelectOne(MDRSchoolPID);
                    DataRow row = dTblMDRSchool.Rows[0];
                    txtMDRSchoolPID.Text = row[CMDRTable.FLD_PKID].ToString();
                    txtMDRSchoolName.Text = row[CMDRTable.FLD_NAME].ToString();
                    this.Page.MDRSchoolName = txtMDRSchoolName.Text;
                }
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void lnkBtnCampaignDetail_Click(object sender, System.EventArgs e) {
            if (txtMDRSchoolPID.Text.Length > 0) {
                Update();
                BindForm();
            }
            else {
                ResetForm();
            }
        }

        private void ResetForm() {
            txtMDRSchoolPID.Text = "";
            txtMDRSchoolName.Text = "";
        }
    }
}