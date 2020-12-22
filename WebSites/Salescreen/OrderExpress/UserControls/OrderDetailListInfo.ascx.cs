using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Order Detail List info.
    /// </summary>
    public partial class OrderDetailListInfo : BaseWebUserControl {
        protected dataDef dTblOrderDetail = new dataDef();
        protected DataView dvOrderDetail = new DataView();
        private int c_OrderID;
        private int c_FormID = 0;
        private int c_FormSectionTypeID = 0;
        private int c_FormSectionNumber = 0;
        private decimal profitRate = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here		
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dtgOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgOrderDetail_ItemDataBound);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            //dtgOrderDetail.Columns[0].Visible =  isPersonalizationMode;
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList						
                BindForm();
            }
            catch (Exception ex) {
                this.Page.SetPageMessage(ex.Message);
            }
        }

        public void BindForm() {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public int OrderID {
            get {
                return c_OrderID;
            }
            set {
                c_OrderID = value;
            }
        }

        public int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
            }
        }

        public string SectionTitle {
            get {
                return lblSectionTitle.Text;
            }
            set {
                lblSectionTitle.Text = value;
            }
        }

        public void LoadDataSource() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            //dTblOrderDetail = orderDetailSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public decimal ProfitRate {
            get { return profitRate; }
            set { profitRate = value; }
        }

        public dataDef DataSource {
            get {
                return dTblOrderDetail;
            }
            set {
                dTblOrderDetail = value;
            }
        }

        public int FormSectionTypeID {
            get {
                return c_FormSectionTypeID;
            }
            set {
                c_FormSectionTypeID = value;
            }
        }

        public int FormSectionNumber {
            get {
                return c_FormSectionNumber;
            }
            set {
                c_FormSectionNumber = value;
            }
        }

        private void BindGrid() {
            dvOrderDetail.Table = dTblOrderDetail;
            string sFilter = "";
            if (c_FormSectionTypeID > 0) {
                sFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + c_FormSectionTypeID.ToString();
                if (FormSectionNumber > 1) {
                    sFilter = sFilter + " AND " + OrderDetailTable.FLD_FORM_SECTION_NUMBER + " = " + c_FormSectionNumber.ToString();
                }
                else {
                    sFilter = sFilter + " AND ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_NUMBER + ",0) <= 1";
                }
            }
            //commented by Renuka Sept 12,2007
            if (this.ProfitRate != 0) {
                sFilter += " AND " + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = " + this.ProfitRate.ToString();
            }
            else {
                sFilter += " AND (" + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = 0";
                sFilter += " OR " + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = 0.5)";
            }

            //if (Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE]) != 0 || this.ProfitRate != 0)
            //{
            //    if (ProfitRate == 0)
            //        this.ProfitRate = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE]);
            //    sFilter += "AND " + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = " + this.ProfitRate.ToString();
            //}

            dvOrderDetail.RowFilter = sFilter;
            dvOrderDetail.Sort = dataDef.FLD_DISPLAY_ORDER;

            this.dtgOrderDetail.DataBind();
        }

        private void dtgOrderDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {
                //Apply calculation Total
                Label lblTotalQuantity = (Label)e.Item.FindControl("lblTotalQuantity");
                lblTotalQuantity.Text = dTblOrderDetail.GetTotalQuantity(c_FormSectionTypeID, c_FormSectionNumber).ToString();
                Label lblTotalAmount = (Label)e.Item.FindControl("lblTotalAmount");
                lblTotalAmount.Text = dTblOrderDetail.GetTotalAmount(c_FormSectionTypeID, c_FormSectionNumber).ToString("C");
            }
        }
    }
}