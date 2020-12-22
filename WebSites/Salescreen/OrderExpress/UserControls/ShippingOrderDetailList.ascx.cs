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
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class ShippingOrderDetailList : BaseWebUserControl {
        protected dataDef dTblOrderDetail = new dataDef();

        private int c_ParentID;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected ShipmentGroupTable dTblShipmentGroup = new ShipmentGroupTable();
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();
        protected System.Data.DataTable tblProduct = new DataTable();

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
            this.dtgOrderDetail.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgOrderDetail_DeleteCommand);
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

        public ShipmentGroupTable ShipmentGroup {
            get {
                return dTblShipmentGroup;
            }
            set {
                dTblShipmentGroup = value;
            }
        }

        private void BindGrid() {
            if (!dTblShipmentGroup.Columns.Contains("TitleAddress")) {
                dTblShipmentGroup.Columns.Add("TitleAddress", typeof(System.String));
                for (int iCount = 0; iCount < dTblShipmentGroup.Rows.Count; iCount++) {
                    dTblShipmentGroup.Rows[iCount]["TitleAddress"] = "Shipping Address #" + (iCount + 1);
                }
            }

            FillDataTableForDropDownList();
            this.dtgOrderDetail.DataBind();
        }

        private void dtgOrderDetail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
        }

        private void FillDataTableForDropDownList() {
            QSPForm.Business.ProductSystem prodSys = new QSPForm.Business.ProductSystem();
            //Type Address				
            tblProduct = prodSys.SelectAll();
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    DataView dv = new DataView(dt);
                    dv.Sort = dt.Columns[0].ColumnName;
                    iIndex = dv.Find(sValue);
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return iIndex;
        }

        private void dtgOrderDetail_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            UpdateDataSource();
            // get row to delete in grid
            DataRow row = dTblOrderDetail.Rows[e.Item.ItemIndex];
            if (row.RowState == DataRowState.Added) {
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            }
            row.Delete();
            BindGrid();
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            // get edited row values in grid

            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataRow row;
                row = dTblOrderDetail.Rows[iCount];
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];

                if (row.RowState != DataRowState.Deleted) {
                    row[dataDef.FLD_SHIPMENT_GROUP_ID] = ((DropDownList)dgItem.FindControl("ddlProduct")).SelectedValue;

                    if (row.RowState == DataRowState.Added) {
                        row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                    }
                    else {
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }
                //Operation Sucessful
                IsSuccess = true;
            }

            return IsSuccess;
        }
    }
}