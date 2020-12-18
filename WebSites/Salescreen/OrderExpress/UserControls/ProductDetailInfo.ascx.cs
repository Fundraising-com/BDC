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
using dataDef = QSPForm.Common.DataDef.ProductTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for UserDetail.
    /// </summary>
    public partial class ProductDetailInfo : BaseWebFormControl {
        private int ProductID = 0;
        public const string PRODUCT_ID = "ProductID";
        private const string PRODUCT_DATA = "ProductData";
        protected dataDef dtblProduct;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	

                LoadData();
                if (!IsPostBack) {
                    BindForm();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEdit_Click);

        }
        #endregion

        protected void SetFormParameter() {
            if (Request[PRODUCT_ID] != null) {
                ProductID = Convert.ToInt32(Request[PRODUCT_ID].ToString());
            }
            else {
                ProductID = 0;
            }
            ViewState[PRODUCT_ID] = ProductID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override void BindForm() {
            ProductInfo.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.ProductSystem prdSys = new QSPForm.Business.ProductSystem();
                dtblProduct = prdSys.SelectOne(ProductID);
            }
            else {
                ProductID = Convert.ToInt32(ViewState[PRODUCT_ID]);
                //dtblUser = (dataDef)this.ViewState[USER_DATA];
            }

            //UserInfo1.uiUserID = c_UserID;
            ProductInfo.DataSource = dtblProduct;
        }

        private void imgBtnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProductDetail, BaseProductDetail.PRODUCT_ID, ProductID.ToString());
            string url = "~/ProductDetail.aspx?" + BaseProductDetail.PRODUCT_ID + "=" + ProductID.ToString();
            Response.Redirect(url);
        }
    }
}