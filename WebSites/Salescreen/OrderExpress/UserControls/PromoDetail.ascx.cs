//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_PromoDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'PromoDetail.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.Promo_ImageTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class PromoDetail : BasePromoDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        //private const string PROMO_DATA = "promo_data";
        //private int _PromoID = 0;
        protected dataDef dtsPromo;

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);

        }
        #endregion

        private int PromoCouponID {
            get {
                try {
                    return Convert.ToInt32(Request.QueryString["pcid"]);
                }
                catch { return 0; }
            }
        }

        private int Format {
            get {
                try {
                    return Convert.ToInt32(Request.QueryString["f"]);
                }
                catch { return 0; }
            }
        }

        public override int PromoID {
            get { return Convert.ToInt32(this.ViewState["PromoID"]); }
            set { this.ViewState["PromoID"] = value; }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //this.ViewState[PROMO_DATA] = dtsPromo;
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            this.Page.SetPageError(new Exception("No delete available at this time"));
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                dtsPromo = new Promo_ImageTable();
                DataRow r = dtsPromo.NewRow();
                dtsPromo.Rows.Add(r);

                ctrlPromoHeaderForm.DataSource = dtsPromo;
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                comSys.UpdateRow(dtsPromo.Rows[0], Promo_ImageTable.FLD_FORMAT_ID, this.Format.ToString());

                Boolean blnValid = true;
                blnValid = ctrlPromoHeaderForm.IsValid();
                if (!blnValid) {
                    return;
                }

                blnValid = ctrlPromoHeaderForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                //QSPForm.Business.PromoSystem prmSys = new QSPForm.Business.PromoSystem();
                QSPForm.Business.PromoImageSystem prmSys = new QSPForm.Business.PromoImageSystem();
                //blnValid = prmSys.UpdateAllDetail(dtsPromo);
                blnValid = prmSys.Update(dtsPromo);

                if ((blnValid) && (PromoID == 0)) {
                    ctrlPromoHeaderForm.CreateImageAndPreview();
                    //PromoID = Convert.ToInt32(this.dtsPromo.Rows[0][PromoTable.FLD_PKID].ToString());
                }

                if (blnValid) {
                    // string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Promo_CouponDetailInfo, BasePromo_CouponDetail.Promo_Coupon_ID, PromoCouponID.ToString());
                    string url = "~/Promo_CouponDetailInfo.aspx?" + BasePromo_CouponDetail.Promo_Coupon_ID + "=" + PromoCouponID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                this.Page.SetPageMessage("Error while saving the image : " + ex.Message);
            }
        }
    }
}