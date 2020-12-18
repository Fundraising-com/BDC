using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CampaignTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AccountDetail.
    /// </summary>
    public partial class CampaignInfo : BaseWebFormControl {
        private int c_CampID = 0;
        protected dataDef dtblCampaign = new dataDef();
        protected DataTable dtblProgType = new DataTable();
        QSPForm.Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();
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
            //			dtblCampaign = campSys.SelectOne(c_CampID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList				
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int CampaignID {
            get {
                return c_CampID;
            }
            set {
                c_CampID = value;
                ViewState["CampID"] = c_CampID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblCampaign;
            }
            set {
                dtblCampaign = value;
            }
        }

        public override void BindForm() {
            if (dtblCampaign.Rows.Count > 0) {
                DataRow row;
                row = dtblCampaign.Rows[0];
                lblCampID.Text = row[dataDef.FLD_PKID].ToString();
                lblName.Text = row[dataDef.FLD_NAME].ToString();
                if (row[dataDef.FLD_START_DATE] != System.DBNull.Value)
                    lblStartDate.Text = Convert.ToDateTime(row[dataDef.FLD_START_DATE]).ToShortDateString();
                if (row[dataDef.FLD_END_DATE] != System.DBNull.Value)
                    lblEndDate.Text = Convert.ToDateTime(row[dataDef.FLD_END_DATE]).ToShortDateString();
                if (row[dataDef.FLD_FISCAL_YEAR] != System.DBNull.Value)
                    lblFiscalYear.Text = row[dataDef.FLD_FISCAL_YEAR].ToString();
                if (row[dataDef.FLD_ACCOUNT_ID] != System.DBNull.Value) {
                    lblAccountID.Text = row[dataDef.FLD_ACCOUNT_ID].ToString();
                    if (row[AccountTable.FLD_FULF_ACCOUNT_ID] != System.DBNull.Value)
                        lblFULFAccountID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
                }

                if (row["program_type_name"] != System.DBNull.Value) {
                    if (row["program_type_name"] != System.DBNull.Value) {
                        lblType.Text = row["program_type_name"].ToString();
                    }
                }
                if (row[dataDef.FLD_FM_ID] != System.DBNull.Value) {
                    lblFMID.Text = row[dataDef.FLD_FM_ID].ToString();
                    if (row["fm_name"] != System.DBNull.Value) {
                        lblFMName.Text = row["fm_name"].ToString();
                    }
                }
                if (row[dataDef.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                    lblTaxExemptionNumber.Text = row[dataDef.FLD_TAX_EXEMPTION_NO].ToString();
                if (row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                    lblTaxExemptionNumber.Text = Convert.ToDateTime(row[dataDef.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();
                if (row[dataDef.FLD_ENROLLMENT] != System.DBNull.Value)
                    lblEnrollment.Text = row[dataDef.FLD_ENROLLMENT].ToString();
                if (row[dataDef.FLD_WAREHOUSE_ID] != System.DBNull.Value)
                    lblWarehouse.Text = row[dataDef.FLD_WAREHOUSE_ID].ToString();
                if (row[dataDef.FLD_COMMENTS] != System.DBNull.Value)
                    lblComments.Text = row[dataDef.FLD_COMMENTS].ToString();
            }
            else {
            }
        }
    }
}