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
    ///		Summary description for Order Supply List.
    /// </summary>
    public partial class OrderSupplyListInfo : BaseWebUserControl {
        protected dataDef dTblOrderDetail = new dataDef();
        protected DataView dvOrderDetail = new DataView();
        private int c_ParentID;
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();

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
            this.dtgOrderDetail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgOrderDetail_ItemCommand);
            this.dtgOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgOrderDetail_ItemDataBound);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
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

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public int Count {
            get {
                return this.dtgOrderDetail.Items.Count;
            }
        }

        public void LoadDataSource() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            //dTblOrderDetail = orderDetailSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public dataDef DataSource {
            get {
                return dTblOrderDetail;
            }
            set {
                dTblOrderDetail = value;
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

        private void BindGrid() {
            dvOrderDetail.Table = dTblOrderDetail;
            dvOrderDetail.Sort = dataDef.FLD_DISPLAY_ORDER;
            this.dtgOrderDetail.DataBind();
        }

        private void dtgOrderDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
        }

        private void dtgOrderDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                if (((Label)e.Item.FindControl("lblQuantity")).Text == "0") {
                    ((Label)e.Item.FindControl("lblQuantity")).Text = "";
                }
            }
            if (e.Item.ItemType == ListItemType.Footer) {
                //Apply calculation Total
                int totalQty = 0;
                DataView dv = new DataView(dTblOrderDetail);
                foreach (DataRowView dvRow in dv) {
                    int Qty = 0;
                    if (dvRow[OrderDetailTable.FLD_QUANTITY] != System.DBNull.Value) {
                        Qty = Convert.ToInt32(dvRow[OrderDetailTable.FLD_QUANTITY]);
                    }
                    totalQty = totalQty + Qty;

                }
                Label lblTotalQuantity = (Label)e.Item.FindControl("lblTotalQuantity");
                lblTotalQuantity.Text = totalQty.ToString();
            }
        }
    }
}