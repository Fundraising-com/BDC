using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrganizationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrganizationDetail.
    /// </summary>
    public partial class OrganizationHeaderInfo : BaseWebFormControl {
        private int c_OrgID = 0;
        protected dataDef dtblOrganization = new dataDef();
        protected DataTable dtblOrgType = new DataTable();
        protected System.Web.UI.WebControls.Label txtFMID;
        protected System.Web.UI.WebControls.Label txtComments;
        protected System.Web.UI.WebControls.Label lblLabelTradeClassID;
        protected System.Web.UI.WebControls.Label lblTradeClass;
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

        protected override void LoadData() {
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
                return dtblOrganization;
            }
            set {
                dtblOrganization = value;
            }
        }

        public override void BindForm() {
            LoadData();
            if (dtblOrganization.Rows.Count > 0) {
                DataRow row;
                row = dtblOrganization.Rows[0];
                int OrgID = Convert.ToInt32(row[dataDef.FLD_PKID]);
                //Org ID
                if (OrgID == 0)
                    lblOrgID.Text = "New";
                else
                    lblOrgID.Text = row[dataDef.FLD_PKID].ToString();
                //Org Name
                if (row[dataDef.FLD_NAME] != System.DBNull.Value)
                    lblName.Text = row[dataDef.FLD_NAME].ToString();
                //Org Type
                if (row[dataDef.FLD_ORG_TYPE_ID] != System.DBNull.Value) {
                    lblType.Text = row[dataDef.FLD_ORG_TYPE_NAME].ToString();
                }
                //Org Level
                if (row[dataDef.FLD_ORG_LEVEL_ID] != System.DBNull.Value) {
                    lblLevel.Text = row[dataDef.FLD_ORG_LEVEL_NAME].ToString();
                }

                //Tax Exemption
                if (row[dataDef.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                    lblTaxExemptionNumber.Text = row[dataDef.FLD_TAX_EXEMPTION_NO].ToString();
                //Tax Exemption Date
                if (row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                    lblTaxExemptionNumber.Text = Convert.ToDateTime(row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                //MDR PID
                if (row[dataDef.FLD_MDRPID] != System.DBNull.Value)
                    lblMDRPID.Text = row[dataDef.FLD_MDRPID].ToString();
                //Comments
                if (row[dataDef.FLD_COMMENTS] != System.DBNull.Value)
                    lblComments.Text = row[dataDef.FLD_COMMENTS].ToString();
            }
        }
    }
}