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
    public partial class ProductHeaderForm : BaseWebFormControl {
        #region Item Declarations
        private QSPForm.Business.ProductSystem productSystem = new QSPForm.Business.ProductSystem();
        private CommonUtility util = new CommonUtility();
        private dataRef dtblProduct = new ProductTable();
        private DataTable dtblProductType;
        private DataTable dtblBusinessDivision;
        private DataTable dtblVendor;
        private int iProductID = 0;

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

        #region accessor

        public dataRef DataSource {
            get {
                return dtblProduct;
            }
            set {
                dtblProduct = value;
            }
        }

        public int ProductID {
            get { return iProductID; }
            set { iProductID = value; }
        }

        #endregion accessor

        public override void BindForm() {
            LoadData();
            FillList();
            if (this.ProductID == 0) {
                this.lblID.Text = "New";
            }
            if (dtblProduct.Rows.Count > 0) {
                DataRow row;
                row = dtblProduct.Rows[0];
                //int productID = Convert.ToInt32(row[dataRef.FLD_PKID]);
                this.lblID.Text = row[dataRef.FLD_PKID].ToString();//productID.ToString(); 

                this.ddlProductType.SelectedValue = row[dataRef.FLD_PRODUCT_TYPE_ID].ToString();
                if ((row[dataRef.FLD_VENDOR_ID] != System.DBNull.Value) || (row[dataRef.FLD_VENDOR_ID].ToString() != String.Empty)) {
                    this.ddlVendor.SelectedValue = row[dataRef.FLD_VENDOR_ID].ToString();
                }
                this.txtProductCode.Text = row[dataRef.FLD_CODE].ToString();
                this.txtProductName.Text = row[dataRef.FLD_NAME].ToString();
                this.txtDescription.Text = row[dataRef.FLD_DESCRIPTION].ToString();
                this.txtNbUnit.Text = row[dataRef.FLD_NB_UNITS].ToString();
                this.txtNbDayLeadTime.Text = row[dataRef.FLD_NB_DAY_LEAD_TIME].ToString();
                this.txtVendorItem.Text = row[dataRef.FLD_VENDOR_ITEM_CODE].ToString();
                this.txtOracleCode.Text = row[dataRef.FLD_ORACLE_CODE].ToString();
                this.txtCommission.Text = row[dataRef.FLD_COMMISSION].ToString();
                if (row[dataRef.FLD_IS_FREE_SAMPLE] != System.DBNull.Value) {
                    this.chkIsFreeSample.Checked = Convert.ToBoolean(row[dataRef.FLD_IS_FREE_SAMPLE].ToString());
                }
                this.txtImageURL.Text = row[dataRef.FLD_IMAGE_URL].ToString();
                if (row[dataRef.FLD_BUSINESS_DIVISION_ID] != System.DBNull.Value) {
                    this.ddlBusinessDivision.SelectedValue = row[dataRef.FLD_BUSINESS_DIVISION_ID].ToString();
                }
            }
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;
            CommonUtility clsUtil = new CommonUtility();
            DataRow row;
            if (this.DataSource.Rows.Count == 0) {
                row = this.DataSource.NewRow();
                this.DataSource.Rows.Add(row);
            }
            else {
                row = this.DataSource.Rows[0];
            }

            clsUtil.UpdateRow(row, dataRef.FLD_PRODUCT_TYPE_ID, this.ddlProductType.SelectedValue);
            if (ddlVendor.SelectedValue != String.Empty) {
                clsUtil.UpdateRow(row, dataRef.FLD_VENDOR_ID, this.ddlVendor.SelectedValue);
            }
            clsUtil.UpdateRow(row, dataRef.FLD_CODE, this.txtProductCode.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_NAME, this.txtProductName.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_DESCRIPTION, this.txtDescription.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_NB_UNITS, this.txtNbUnit.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_NB_DAY_LEAD_TIME, this.txtNbDayLeadTime.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_VENDOR_ITEM_CODE, this.txtVendorItem.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_ORACLE_CODE, this.txtOracleCode.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_COMMISSION, this.txtCommission.Text);
            clsUtil.UpdateRow(row, dataRef.FLD_IS_FREE_SAMPLE, this.chkIsFreeSample.Checked.ToString());
            clsUtil.UpdateRow(row, dataRef.FLD_IMAGE_URL, this.txtImageURL.Text);
            if (ddlBusinessDivision.SelectedValue != String.Empty) {
                clsUtil.UpdateRow(row, dataRef.FLD_BUSINESS_DIVISION_ID, this.ddlBusinessDivision.SelectedValue);
            }

            if (row.RowState != DataRowState.Unchanged) {
                if (row.RowState == DataRowState.Added) {
                    row[dataRef.FLD_CREATE_USER_ID] = Page.UserID;
                }
                else {
                    row[dataRef.FLD_UPDATE_USER_ID] = Page.UserID;
                }
                IsSuccess = true;
            }
            return IsSuccess;
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            try {
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private new void LoadData() {
            this.DataSource = this.productSystem.SelectOne(ProductID);
            base.LoadData();
        }

        private void FillList() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetProductTypeDropDownList(ddlProductType, true);
            clsUtil.SetBusinessDivisionDropDownList(ddlBusinessDivision, true);
            clsUtil.SetVendorDropDownList(ddlVendor, true);

            //			QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            //			//fill productType
            //			dtblProductType = comSys.SelectAllProductType();
            //			ddlProductType.DataSource = dtblProductType;
            //			ddlProductType.DataValueField = "product_type_id";
            //			ddlProductType.DataTextField = "product_type_name";
            //			ddlProductType.DataBind();
            //			//fill BusinessDivision
            //			dtblBusinessDivision = comSys.SelectAllBusinessDivision();
            //			ddlBusinessDivision.DataSource = dtblBusinessDivision;
            //			ddlBusinessDivision.DataValueField = "business_division_id";
            //			ddlBusinessDivision.DataTextField = "business_division_name";
            //			ddlBusinessDivision.DataBind();
            //			ddlBusinessDivision.Items.Add(new ListItem("Not Set",""));
            //			ddlBusinessDivision.SelectedIndex = ddlBusinessDivision.Items.Count-1;
            //			//fill Vendor
            //			QSPForm.Business.VendorSystem vendorSys = new QSPForm.Business.VendorSystem();
            //			dtblVendor = vendorSys.SelectAll();
            //			ddlVendor.DataSource = dtblVendor;
            //			ddlVendor.DataValueField = "vendor_id";
            //			ddlVendor.DataTextField = "vendor_name";
            //			ddlVendor.DataBind();
            //			ddlVendor.Items.Add(new ListItem("Not Set",""));
            //			ddlVendor.SelectedIndex = ddlVendor.Items.Count-1;	
        }

        public bool ValidateForm() {
            this.Page.Validate();
            return this.IsValid();
        }
    }
}