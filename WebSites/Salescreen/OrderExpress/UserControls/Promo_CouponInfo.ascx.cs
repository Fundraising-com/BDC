using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class Promo_CouponInfo : BaseWebFormControl {
        #region Item Declarations
        private CommonUtility util = new CommonUtility();
        protected dataRef dtblPromo_Coupon;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.HtmlControls.HtmlTableRow trSubdivision;
        protected System.Web.UI.WebControls.Label Label8;
        protected System.Web.UI.WebControls.Label lblEndDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trStartDate;
        protected System.Web.UI.HtmlControls.HtmlTableRow trEndDate;
        protected System.Web.UI.WebControls.Label lblStartDate;
        //protected System.Web.UI.WebControls.ListBox lbxCurrentSubdivision;
        private QSPForm.Business.CommonSystem comSys;
        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            if (this.imgPortrait.ImageUrl == String.Empty)
                imgPortrait.Visible = false;
            if (this.imgLandscapte.ImageUrl == String.Empty)
                imgLandscapte.Visible = false;
            if (this.imgLogo.ImageUrl == String.Empty)
                imgLogo.Visible = false;
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
                return dtblPromo_Coupon;
            }
            set {
                dtblPromo_Coupon = value;
            }
        }

        public override void DataBind() {
            this.lblTitle.Text = "Coupon Information";
            if (this.DataSource.Rows.Count > 0) {
                DataRow row = this.DataSource.Rows[0];
                this.lblID.Text = row[dataRef.FLD_PKID].ToString();
                this.lblFMID.Text = row[dataRef.FLD_FM_ID].ToString() + " - " + row[dataRef.FLD_FM_NAME].ToString();
                this.lblVendorID.Text = row[dataRef.FLD_VENDOR_ID].ToString() + " - " + row[dataRef.FLD_VENDOR_NAME].ToString();
                this.lblOffer.Text = row[dataRef.FLD_PROMO_TEXT_DESCRIPTION].ToString();
                //this.chkDeleted.Checked = Convert.ToBoolean(row[dataRef.FLD_DELETED].ToString());
                //this.lblText.Text = row[dataRef.FLD_PROMO_TEXT_DESCRIPTION].ToString();
                if (row[dataRef.FLD_PROMO_LOGO_ID].ToString() != "0")
                    this.imgLogo.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                        row[dataRef.FLD_PROMO_LOGO_ID].ToString() + "." +
                                       QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
                if (row[dataRef.FLD_PROMO_LANDSCAPE_ID].ToString() != "0")
                    this.imgLandscapte.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                        row[dataRef.FLD_PROMO_LANDSCAPE_ID].ToString() + "." +
                                       QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;

                if (row[dataRef.FLD_PROMO_PORTRAIT_ID].ToString() != "0")
                    this.imgPortrait.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                        row[dataRef.FLD_PROMO_PORTRAIT_ID].ToString() + "." +
                                       QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;

                //this.chkNational.Checked = Convert.ToBoolean(row[dataRef.FLD_NATIONAL].ToString());
                this.lbCreateBy.Text = row[dataRef.FLD_CREATE_USER_ID].ToString();
                this.lbCreateDT.Text = row[dataRef.FLD_CREATE_DATE].ToString();
                this.lbUpdateBy.Text = row[dataRef.FLD_UPDATE_USER_ID].ToString();
                this.lbUpdateDT.Text = row[dataRef.FLD_UPDATE_DATE].ToString();

                //Fill the list of Subdivision 
                //FillList(Convert.ToInt32(row[dataRef.FLD_PKID].ToString()));
            }
        }

        //private void FillList(int Promo_CouponID)
        //{

        //    QSPForm.Business.PromoCouponSubdivisionSystem regSys = new QSPForm.Business.PromoCouponSubdivisionSystem();
        //    DataTable table = regSys.SelectAllByPromoCouponID(Promo_CouponID);
        //    foreach(DataRow row in table.Rows)
        //    {
        //        lbxCurrentSubdivision.Items.Add(new ListItem(row[QSPForm.Common.DataDef.PromoCouponSubdivisionTable.FLD_SUBDIVISION_CODE]+" - "+row[QSPForm.Common.DataDef.PromoCouponSubdivisionTable.FLD_SUBDIVISION_NAME_1],""));
        //    }
        //}
    }
}