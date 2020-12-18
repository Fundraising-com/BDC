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
using dataDef = QSPForm.Common.DataDef.PromoTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for UserDetail.
    /// </summary>
    public partial class PromoDetailInfo : BaseWebFormControl {
        protected dataDef dtblPromo;
        public const string PROMO_ID = "promo_id";

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack)
                DataBind();
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
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);

        }
        #endregion

        #region Event

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.PromoDetail, BasePromoDetail.PROMO_ID, PromoID.ToString());
            string url = "~/PromoDetail.aspx?" + BasePromoDetail.PROMO_ID + "=" + PromoID.ToString();
            Response.Redirect(url);
        }

        #endregion Event

        #region Property

        private string PromoID {
            get {
                if (Request[PROMO_ID] != null) {
                    return Request[PROMO_ID].ToString();
                }
                else {
                    return "0";
                }
            }
        }

        #endregion Property

        public override void DataBind() {
            try {
                LoadData();
                SetValue();
                this.lblError.Text = "";
            }
            catch (Exception ex) {
                this.lblError.Text = ex.Message;
            }
        }

        protected override void LoadData() {
            QSPForm.Business.PromoSystem promoSys = new QSPForm.Business.PromoSystem();
            this.dtblPromo = promoSys.SelectOne(Convert.ToInt32(this.PromoID));
        }

        private void SetValue() {
            this.ctrlPromoInfo.DataSource = this.dtblPromo;
            this.ctrlPromoInfo.DataBind();
        }
    }
}