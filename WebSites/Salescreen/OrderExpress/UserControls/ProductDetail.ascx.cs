//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_ProductDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'ProductDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.ProductTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class ProductDetail : BaseProductDetail {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int ProductID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;

        private const string PRODUCT_DATA = "ProductData";
        protected dataDef dtblProduct;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    //this.Page.ValSummary.Visible = false;
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
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
        }
        #endregion

        //		public int AppProductID
        //		{
        override public int AppProductID {
            get {
                return ProductID;
            }
            set {
                ProductID = value;
                ViewState[PRODUCT_ID] = ProductID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[PRODUCT_DATA] = dtblProduct;
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        protected void SetFormParameter() {
            if (Request[PRODUCT_ID] != null) {
                ProductID = Convert.ToInt32(Request[PRODUCT_ID].ToString());
            }
            else {
                ProductID = 0;
            }
            ViewState[PRODUCT_ID] = ProductID;
        }

        public override void BindForm() {
            HeaderDetail.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.ProductSystem prdSys = new QSPForm.Business.ProductSystem();
                //ToDo insert row when c_User_ID=0
                dtblProduct = prdSys.SelectOne(ProductID);

                this.ViewState[PRODUCT_ID] = ProductID;
                this.ViewState[PRODUCT_DATA] = dtblProduct;
            }
            else {
                ProductID = Convert.ToInt32(this.ViewState[PRODUCT_ID]);
                dtblProduct = (dataDef)this.ViewState[PRODUCT_DATA];
            }

            HeaderDetail.ProductID = ProductID;
            HeaderDetail.DataSource = dtblProduct;
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                Boolean blnValid = true;

                blnValid = HeaderDetail.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = HeaderDetail.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.ProductSystem prdSys = new QSPForm.Business.ProductSystem();
                if (ProductID == 0)
                    blnValid = prdSys.Insert(dtblProduct);
                else
                    blnValid = prdSys.Update(dtblProduct);
                if (blnValid) {
                    ProductID = Convert.ToInt32(dtblProduct.Rows[0][dataDef.FLD_PKID]);
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.ProductDetailInfo, ProductDetailInfo.PRODUCT_ID , ProductID.ToString());
                    string url = "~/ProductDetailInfo.aspx?" + ProductDetailInfo.PRODUCT_ID + "=" + ProductID.ToString();
                    Response.Redirect(url, false);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}