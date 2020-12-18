using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CMDRTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>MDRSchoolInfo: Read only MDR information.</summary>
    public partial class MDRSchoolInfo : BaseWebFormControl {

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion auto-generated code

        #region Item Declarations
        private int c_MDRPID = 0;
        protected dataDef dtblMDR = new dataDef();
        protected DataTable dtblProgType = new DataTable();
        QSPForm.Business.MDRSystem mdrSys = new QSPForm.Business.MDRSystem();
        private CommonUtility clsUtil = new CommonUtility();

        #endregion Item Declarations

        protected void Page_Load(object s, System.EventArgs e) {
            if (!IsPostBack) {

            }
        }

        #region data calls
        protected override void LoadData() {
            //			dtblCampaign = campSys.SelectOne(c_CampID);
            //			base.LoadData ();
        }

        protected void Page_DataBinding(object s, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public int MDRPid {
            get {
                return c_MDRPID;
            }
            set {
                c_MDRPID = value;
                ViewState["c_MDRPID"] = c_MDRPID;
            }
        }

        public dataDef DataSource {
            get {
                return dtblMDR;
            }
            set {
                dtblMDR = value;
            }
        }

        public override void BindForm() {
            if (dtblMDR.Rows.Count > 0) {
                DataRow row;
                row = dtblMDR.Rows[0];
                this.lblMdrPID.Text = row[dataDef.FLD_PKID].ToString();

                this.lblType.Text = "&nbsp;";
                this.lblName.Text = row[dataDef.FLD_NAME].ToString();

                if (row[dataDef.FLD_ADDR] != System.DBNull.Value) {
                    this.lblAddress.Text = row[dataDef.FLD_ADDR].ToString();
                }
                else {
                    this.lblAddress.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_CITY] != System.DBNull.Value) {
                    this.lblCity.Text = row[dataDef.FLD_CITY].ToString();
                }
                else {
                    this.lblCity.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_STATE] != System.DBNull.Value) {
                    this.lblState.Text = row[dataDef.FLD_STATE].ToString();
                }
                else {
                    this.lblState.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_POSTAL_CODE] != System.DBNull.Value) {
                    this.lblPostalCode.Text = row[dataDef.FLD_POSTAL_CODE].ToString();
                }
                else {
                    this.lblPostalCode.Text = "&nbsp;";
                }
            } //end: if (dtblMDR.Rows.Count > 0)
        }
        #endregion data calls
    }
}