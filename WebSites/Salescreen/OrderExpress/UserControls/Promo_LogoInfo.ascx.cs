using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.Promo_LogoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class Promo_logoInfo : BaseWebFormControl {
        #region Item Declarations
        private CommonUtility util = new CommonUtility();
        protected dataRef dtblPromo_logo;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.HtmlControls.HtmlTableRow trSubdivision;
        protected System.Web.UI.WebControls.Label Label8;
        protected System.Web.UI.WebControls.Label lblEndDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trStartDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trEndDate;
        protected System.Web.UI.WebControls.Label lblStartDate;
        //protected System.Web.UI.WebControls.ListBox lbxCurrentSubdivision;
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            /* Region are temporary removed */
            trRegion.Visible = false;
            imgBtnDetail.Attributes.Add("onclick", "javascript:opendetail();");
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
                return dtblPromo_logo;
            }
            set {
                dtblPromo_logo = value;
            }
        }

        public override void DataBind() {
            this.lblTitle.Text = "Logo Information";
            if (this.DataSource.Rows.Count > 0) {
                DataRow row = this.DataSource.Rows[0];
                this.lblID.Text = row[dataRef.FLD_PKID].ToString();
                this.lblName.Text = row[dataRef.FLD_PROMO_LOGO_NAME].ToString();
                this.lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
                this.lblFMID.Text = row[dataRef.FLD_FM_ID].ToString();
                //this.chkDeleted.Checked = Convert.ToBoolean(row[dataRef.FLD_DELETED].ToString());
                this.chkEnabled.Checked = Convert.ToBoolean(row[dataRef.FLD_ENABLED].ToString());
                this.chkNational.Checked = Convert.ToBoolean(row[dataRef.FLD_NATIONAL].ToString());
                this.imgBtnDetail.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                    row[dataRef.FLD_PKID].ToString() + "." +
                    QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
                this.lbCreateBy.Text = row[dataRef.FLD_CREATE_USER_ID].ToString();
                this.lbCreateDT.Text = row[dataRef.FLD_CREATE_DATE].ToString();
                this.lbUpdateBy.Text = row[dataRef.FLD_UPDATE_USER_ID].ToString();
                this.lbUpdateDT.Text = row[dataRef.FLD_UPDATE_DATE].ToString();

                DataTable tbl = comSys.SelectAllImageCategory();
                foreach (DataRow r in tbl.Rows) {
                    if (r[0].ToString() == row[dataRef.FLD_CATEGORY].ToString()) {
                        this.lblCategory.Text = r[1].ToString();
                        break;
                    }
                }

                //Fill the list of Subdivision 
                FillList(Convert.ToInt32(row[dataRef.FLD_PKID].ToString()));
            }
        }

        private void FillList(int Promo_logoID) {
            QSPForm.Business.Promo_logoSubdivisionSystem regSys = new QSPForm.Business.Promo_logoSubdivisionSystem();
            DataTable table = regSys.SelectAllByPromo_LogoID(Promo_logoID);
            foreach (DataRow row in table.Rows) {
                lbxCurrentSubdivision.Items.Add(new ListItem(row[QSPForm.Common.DataDef.Promo_LogoSubdivisionTable.FLD_SUBDIVISION_CODE] + " - " + row[QSPForm.Common.DataDef.Promo_LogoSubdivisionTable.FLD_SUBDIVISION_NAME_1], ""));
            }
        }
    }
}