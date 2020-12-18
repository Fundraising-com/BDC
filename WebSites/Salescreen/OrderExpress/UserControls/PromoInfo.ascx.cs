using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.PromoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class PromoInfo : BaseWebFormControl {
        #region Item Declarations
        private CommonUtility util = new CommonUtility();
        protected dataRef dtblPromo;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.HtmlControls.HtmlTableRow trSubdivision;
        protected System.Web.UI.WebControls.Label Label8;
        //private QSPForm.CommonSystem comSys;
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {

        }
        #endregion auto-generated code

        public dataRef DataSource {
            get {
                return dtblPromo;
            }
            set {
                dtblPromo = value;
            }
        }

        public override void DataBind() {
            this.lblTitle.Text = "Promotion Information";
            if (this.DataSource.Rows.Count > 0) {
                DataRow row = this.DataSource.Rows[0];
                this.lblID.Text = row[dataRef.FLD_PKID].ToString();
                this.lblName.Text = row[dataRef.FLD_PROMO_NAME].ToString();
                this.lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
                this.lblFMID.Text = row[dataRef.FLD_FM_ID].ToString();
                //this.chkDeleted.Checked = Convert.ToBoolean(row[dataRef.FLD_DELETED].ToString());
                this.chkEnabled.Checked = Convert.ToBoolean(row[dataRef.FLD_ENABLED].ToString());
                this.chkNational.Checked = Convert.ToBoolean(row[dataRef.FLD_NATIONAL].ToString());
                this.lblStartDate.Text = row[dataRef.FLD_START_DATE].ToString();
                this.lblEndDate.Text = row[dataRef.FLD_END_DATE].ToString();
                //this.imgBtnDetail.ImageUrl = row[dataRef.FLD_PROMO_PATH].ToString();
                this.imgBtnDetail.ImageUrl = QSPForm.Common.QSPFormConfiguration.PromoImagePreviewPath +
                    row[dataRef.FLD_PKID].ToString() + "." +
                    QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
                this.lbCreateBy.Text = row[dataRef.FLD_CREATE_USER_ID].ToString();
                this.lbCreateDT.Text = row[dataRef.FLD_CREATE_DATE].ToString();
                this.lbUpdateBy.Text = row[dataRef.FLD_UPDATE_USER_ID].ToString();
                this.lbUpdateDT.Text = row[dataRef.FLD_UPDATE_DATE].ToString();

                //Fill the list of Subdivision 
                FillList(Convert.ToInt32(row[dataRef.FLD_PKID].ToString()));
            }
        }

        private void FillList(int PromoID) {
            QSPForm.Business.PromoSubdivisionSystem regSys = new QSPForm.Business.PromoSubdivisionSystem();
            DataTable table = regSys.SelectAllByPromoID(PromoID);
            foreach (DataRow row in table.Rows) {
                lbxCurrentSubdivision.Items.Add(new ListItem(row[QSPForm.Common.DataDef.PromoSubdivisionTable.FLD_SUBDIVISION_CODE] + " - " + row[QSPForm.Common.DataDef.PromoSubdivisionTable.FLD_SUBDIVISION_NAME_1], ""));
            }
        }
    }
}