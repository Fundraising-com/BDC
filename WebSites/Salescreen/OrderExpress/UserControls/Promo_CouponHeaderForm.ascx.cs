using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.PromoCouponData;
using tableDataRef = QSPForm.Common.DataDef.PromoCouponTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class Promo_CouponHeaderForm : BaseWebFormControl {
        #region Item Declarations

        private CommonUtility clsUtil = new CommonUtility();
        private QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
        private PromoCouponTable dtblCoupon;
        private QSPForm.Business.PromoCouponSystem promoSys = new QSPForm.Business.PromoCouponSystem();

        #endregion Item Declarations

        protected void Page_Load(object sender, System.EventArgs e) {
            AdjustUI();
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

        #region Property

        public string Title {
            set { this.lblTitle.Text = value; }
        }


        private bool IsNewPromo_Coupon {
            get {
                bool new_promo = true;
                if (this.ViewState["IsNewPromo_Coupon"] != null)
                    new_promo = Convert.ToBoolean(this.ViewState["IsNewPromo_Coupon"].ToString());
                return new_promo;

                //try{ return Convert.ToBoolean(this.ViewState["IsNewPromo_Coupon"].ToString());}
                //catch{throw new Exception("Error in Promo_Coupon : Promo_Coupon is not DataBinded");}
            }
            set { this.ViewState["IsNewPromo_Coupon"] = value; }
        }

        public int Promo_CouponID {
            get {
                int x = 0;
                if (this.ViewState["PromoCouponID"] != null)
                    x = Convert.ToInt32(this.ViewState["PromoCouponID"].ToString());
                return x;
            }
            set {
                this.ViewState["PromoCouponID"] = value;
            }
        }
        #endregion Property

        public override void DataBind() {
            try {
                LoadData();
                SetValue();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void LoadData() {
            dtblCoupon = promoSys.SelectOne(this.Promo_CouponID);
        }

        private void SetValue() {
            DataRow r = dtblCoupon.Rows[0];
            this.lblID.Text = this.Promo_CouponID.ToString();
            this.lblVendorID.Text = r[tableDataRef.FLD_VENDOR_ID].ToString() + " - " +
                               r[tableDataRef.FLD_VENDOR_NAME].ToString();
            this.lblFMID.Text = r[tableDataRef.FLD_FM_ID].ToString() + " - " +
                           r[tableDataRef.FLD_FM_NAME].ToString();
            if (r[tableDataRef.FLD_PROMO_LOGO_ID].ToString() != "0")
                this.imgLogo.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                    r[tableDataRef.FLD_PROMO_LOGO_ID].ToString() + "." +
                                   QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
            if (r[tableDataRef.FLD_PROMO_LANDSCAPE_ID].ToString() != "0")
                this.imgLandscapte.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                    r[tableDataRef.FLD_PROMO_LANDSCAPE_ID].ToString() + "." +
                                   QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
            if (r[tableDataRef.FLD_PROMO_PORTRAIT_ID].ToString() != "0")
                this.imgPortrait.ImageUrl = QSPForm.Common.QSPFormConfiguration.Promo_LogoImagePreviewPath +
                                    r[tableDataRef.FLD_PROMO_PORTRAIT_ID].ToString() + "." +
                                   QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;
            this.lblOffer.Text = r[tableDataRef.FLD_PROMO_TEXT_DESCRIPTION].ToString();
        }

        private string FormatToDate(string s) {
            string[] d = s.Split(' ');
            return d[0];
        }

        private void AdjustUI() {
            if (this.imgPortrait.ImageUrl == String.Empty) {
                imgPortrait.Visible = false;
                btnPortrait.ImageUrl = "images/btnAdd.gif";
            }
            else {
                btnPortrait.ImageUrl = "images/btnEdit.gif";
                btnPortrait.Visible = false;
            }
            if (this.imgLandscapte.ImageUrl == String.Empty) {
                imgLandscapte.Visible = false;
                btnLandscape.ImageUrl = "images/btnAdd.gif";
            }
            else {
                btnLandscape.ImageUrl = "images/btnEdit.gif";
                btnLandscape.Visible = false;
            }
            if (this.imgLogo.ImageUrl == String.Empty)
                imgLogo.Visible = false;
        }

        protected void btnLandscape_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.PromoDetail);
            string url = "~/PromoDetail.aspx?";
            url += "&pcid=" + this.Promo_CouponID + "&f=1";
            Response.Redirect(url);
        }

        protected void btnPortrait_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            // string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.PromoDetail);
            string url = "~/PromoDetail.aspx?";
            url += "&pcid=" + this.Promo_CouponID + "&f=2";
            Response.Redirect(url);
        }
    }
}