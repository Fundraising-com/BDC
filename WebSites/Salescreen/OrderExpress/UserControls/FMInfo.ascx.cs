using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CUserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>FMInfo: Read only Field Manager information.</summary>
    public partial class FMInfo : BaseWebFormControl {
        private string c_FMID = "";
        protected dataDef dtblFMinfo = new dataDef();
        QSPForm.Business.CUserSystem bizSys = new QSPForm.Business.CUserSystem();
        private CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.WebControls.Label lblLabelProfileInstance;
        protected System.Web.UI.WebControls.Label lblProfileInstance;
        protected System.Web.UI.WebControls.HyperLink Hyperlink1;
        private const string FM_ID = "FMID";

        protected void Page_Load(object s, System.EventArgs e) {

            // Put user code to initialize the page here				
        }

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

        protected override void LoadData() {
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

        public string FMID {
            get {
                return this.c_FMID;
            }
            set {
                this.c_FMID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dtblFMinfo;
            }
            set {
                dtblFMinfo = value;
            }
        }

        public override void BindForm() {
            if (dtblFMinfo.Rows.Count > 0) {
                DataRow row;
                row = dtblFMinfo.Rows[0];
                //this.lblProfileInstance.Text = row[dataDef.FLD_PKID].ToString();
                this.lblFMID.Text = this.c_FMID;
                this.lblFName.Text = row[dataDef.FLD_FIRST_NAME].ToString();
                this.lblLName.Text = row[dataDef.FLD_LAST_NAME].ToString();
                this.lblMGRFMID.Text = row[dataDef.FLD_AREA_MGR].ToString();

                if (row[dataDef.FLD_ADDR1] != System.DBNull.Value) {
                    this.lblAddress.Text = row[dataDef.FLD_ADDR1].ToString();
                    if (row[dataDef.FLD_ADDR2] != System.DBNull.Value) {
                        this.lblAddress.Text += "<br />" + row[dataDef.FLD_ADDR2].ToString();
                    }
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

                if (row[dataDef.FLD_VOICE_MAIL_EXT] != System.DBNull.Value) {
                    this.lbVoiceMailExt.Text = row[dataDef.FLD_VOICE_MAIL_EXT].ToString();
                }
                else {
                    this.lbVoiceMailExt.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_WORK_PHONE] != System.DBNull.Value) {
                    this.lbWorkPH.Text = row[dataDef.FLD_WORK_PHONE].ToString();
                }
                else {
                    this.lbWorkPH.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_MOBILE_PHONE] != System.DBNull.Value) {
                    this.lbMobilePH.Text = row[dataDef.FLD_MOBILE_PHONE].ToString();
                }
                else {
                    this.lbMobilePH.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_FAX_PHONE] != System.DBNull.Value) {
                    this.lbFaxPH.Text = row[dataDef.FLD_FAX_PHONE].ToString();
                }
                else {
                    this.lbFaxPH.Text = "&nbsp;";
                }


                if (row[dataDef.FLD_PAGER_PHONE] != System.DBNull.Value) {
                    this.lbPagerPH.Text = row[dataDef.FLD_PAGER_PHONE].ToString();
                }
                else {
                    this.lbPagerPH.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_TOLL_FREE_PHONE] != System.DBNull.Value) {
                    this.lbTollFreePH.Text = row[dataDef.FLD_TOLL_FREE_PHONE].ToString();
                }
                else {
                    this.lbTollFreePH.Text = "&nbsp;";
                }

                if (row[dataDef.FLD_CORP_EMAIL] != System.DBNull.Value) {
                    this.hlCorporateEM.Text = row[dataDef.FLD_CORP_EMAIL].ToString();
                    this.hlCorporateEM.NavigateUrl = "mailto:" + this.hlCorporateEM.Text;
                    this.hlCorporateEM.Visible = true;
                    this.lbCorporateEM_none.Visible = false;
                }
                else {
                    this.hlCorporateEM.Visible = false;
                    this.lbCorporateEM_none.Visible = true;
                }

                if (row[dataDef.FLD_EMAIL] != System.DBNull.Value) {
                    this.hlPersonalEM.Text = row[dataDef.FLD_EMAIL].ToString();
                    this.hlPersonalEM.NavigateUrl = "mailto:" + this.hlCorporateEM.Text;
                    this.hlPersonalEM.Visible = true;
                    this.lbPersonalEM_none.Visible = false;
                }
                else {
                    this.hlPersonalEM.Visible = false;
                    this.lbPersonalEM_none.Visible = true;
                }
            } //end: if (dtblFMinfo.Rows.Count > 0)
        }
    }
}