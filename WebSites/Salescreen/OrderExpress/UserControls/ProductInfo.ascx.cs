using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.ProductTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>User Information - read only</summary>
    public partial class ProductInfo : BaseWebFormControl {
        #region Item Declarations
        private CommonUtility util = new CommonUtility();
        protected dataRef dtblProduct;
        protected System.Web.UI.WebControls.Label Label7;
        protected System.Web.UI.WebControls.Label Label8;
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion auto-generated code

        public dataRef DataSource {
            get {
                return dtblProduct;
            }
            set {
                dtblProduct = value;
            }
        }

        public override void BindForm() {
            if (dtblProduct.Rows.Count > 0) {
                DataRow row;
                row = dtblProduct.Rows[0];

                int productID = Convert.ToInt32(row[dataRef.FLD_PKID]);
                if (productID == 0) {
                    this.lblID.Text = "New";
                }
                else {
                    this.lblID.Text = productID.ToString();
                }

                this.lblProductTypeID.Text = row[dataRef.FLD_PRODUCT_TYPE_ID].ToString();
                //this.lblVendorID.Text = row[dataRef.FLD_VENDOR_ID].ToString();
                this.lblProductCode.Text = row[dataRef.FLD_CODE].ToString();
                this.lblProductName.Text = row[dataRef.FLD_NAME].ToString();
                this.lblDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
                this.lblNbUnit.Text = row[dataRef.FLD_NB_UNITS].ToString();
                this.lblNbDayLeadTime.Text = row[dataRef.FLD_NB_DAY_LEAD_TIME].ToString();
                this.lblVendorItem.Text = row[dataRef.FLD_VENDOR_ITEM_CODE].ToString();
                this.lblOracleCode.Text = row[dataRef.FLD_ORACLE_CODE].ToString();
                this.lblCommission.Text = row[dataRef.FLD_COMMISSION].ToString();
                this.lblIsFreeSample.Text = row[dataRef.FLD_IS_FREE_SAMPLE].ToString();
                this.lblImageURL.Text = row[dataRef.FLD_IMAGE_URL].ToString();
                //this.lblBusinessDivisionID.Text = row[dataRef.FLD_BUSINESS_DIVISION_ID].ToString();
                this.lblBusinesDivisionName.Text = row[dataRef.FLD_BUSINESS_DIVISION_NAME].ToString();
                this.lblProductTypeName.Text = row[dataRef.FLD_PRODUCT_TYPE_NAME].ToString();
                this.lblVendorName.Text = row[dataRef.FLD_VENDOR_NAME].ToString();
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }
    }
}