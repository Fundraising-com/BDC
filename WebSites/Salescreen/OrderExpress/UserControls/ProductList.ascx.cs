using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.ProductTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProductList.
    /// </summary>
    public partial class ProductList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = ProductTable.FLD_NAME + " ASC";
        protected dataDef dTblProduct = new dataDef();
        protected DataView DVProduct;
        private const string FILTER_VENDOR = "Filter_Vendor";
        protected System.Web.UI.WebControls.Label lblFMID;
        protected System.Web.UI.WebControls.Label lblEndDate;
        private const string FILTER_TYPE = "Filter_Type";
        protected System.Web.UI.WebControls.Label LblState;
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            //Commented by Renuka July 5th,2007. This Functionality has bugs
            // Put user code to initialize the page here
            // clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.ProductDetail, BaseProductDetail.PRODUCT_ID, "0", 0, 0);
            //  clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "ProductDetail.aspx?", BaseProductDetail.PRODUCT_ID, "0", 0, 0);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVProduct = new DataView(dTblProduct);
            this.DataSource = DVProduct;
            this.MainDataGrid = dtgProduct;
            dtgProduct.DataKeyField = ProductTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.ProductSystem prodSys = new QSPForm.Business.ProductSystem();

            string sCriteria = this.dtgProduct.FilterExpression;
            switch (this.dtgProduct.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            int VendorID = Convert.ToInt32(ViewState[FILTER_VENDOR]);
            int ProductType = Convert.ToInt32(ViewState[FILTER_TYPE]);

            dTblProduct = prodSys.SelectAll_Search(this.dtgProduct.SearchMode, sCriteria, ProductType, VendorID);

            DVProduct = new DataView();
            DVProduct.Table = dTblProduct;

            DVProduct.Sort = this.dtgProduct.SortExpression;
            lblTotal.Text = "Number of Product(s) : " + DVProduct.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetProductTypeDropDownList(ddlProductType, true);
            //We assign the product type chocolate (4) by default
            ddlProductType.ClearSelection();
            ListItem lstItem = ddlProductType.Items.FindByValue("4");
            if (lstItem != null)
                lstItem.Selected = true;
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProductType.SelectedItem.Value);

            clsUtil.SetVendorDropDownList(ddlVendor, true);
            ViewState[FILTER_VENDOR] = ddlVendor.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlProductType.SelectedItem.Value);
            ViewState[FILTER_VENDOR] = ddlVendor.SelectedItem.Value;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = ProductDetailInfo.PRODUCT_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.ProductDetailInfo, sIDName, sID, 750,700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "ProductDetailInfo.aspx?", sIDName, sID, 750, 700);
                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.ProductDetailInfo, sIDName, sID, 0,0);
                    //					}
                    //					LinkButton lnkBtn = (LinkButton) e.Item.FindControl("lnkBtnProduct");
                    //					if (lnkBtn != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(lnkBtn, QSPForm.Business.AppItem.ProductDetailInfo, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }

        public Label SearchName {
            get {
                return QSPFormSearchModule.LabelSearchByAlpha;
            }
            set {
                QSPFormSearchModule.LabelSearchByAlpha = value;
            }
        }
    }
}