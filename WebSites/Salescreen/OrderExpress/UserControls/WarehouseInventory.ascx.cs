using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for WarehouseInventory.
    /// </summary>
    public partial class WarehouseInventory : System.Web.UI.UserControl {
        //protected QSP.WebControl.DataGridControl.SortedDataGrid dtgWarehouseInventory;
        private int _fulfwarehouseid = 0;
        private int form_id = 0;
        private QSPForm.Common.DataDef.FINGDSTable source;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (ViewState["FULF_WHSE_ID"] != null)
                _fulfwarehouseid = Convert.ToInt32(ViewState["FULF_WHSE_ID"]);
        }

        public override void DataBind() {
            LoadData();
            BindForm();
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            ViewState["FULF_WHSE_ID"] = _fulfwarehouseid;
        }

        public int FulfWarehouseID {
            get { return _fulfwarehouseid; }
            set { _fulfwarehouseid = value; }
        }

        public int FormID {
            get { return form_id; }
            set { form_id = value; }
        }

        private void LoadData() {
            QSPForm.Business.WarehouseSystem whSys = new QSPForm.Business.WarehouseSystem();
            source = whSys.SelectProductInventory_ByFulfWarehouseId(_fulfwarehouseid);
            if (form_id > 0) {
                QSPForm.Business.CatalogItemDetailSystem catSys = new QSPForm.Business.CatalogItemDetailSystem();
                //Catalog Item Detail (Product and multiple price)			
                QSPForm.Common.DataDef.CatalogItemDetailTable dtblCatalogItemDetail = catSys.SelectAllByFormID(form_id);
                if (dtblCatalogItemDetail.Rows.Count > 0) {
                    DataView dvCat = new DataView(dtblCatalogItemDetail);
                    dvCat.Sort = QSPForm.Common.DataDef.CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE;
                    foreach (DataRow invRow in source.Rows) {
                        string prodCode = invRow[QSPForm.Common.DataDef.FINGDSTable.FLD_FGITEM].ToString();
                        if (invRow[QSPForm.Common.DataDef.FINGDSTable.FLD_FGCOUP].ToString().Trim().Length > 0) {
                            prodCode = prodCode + " " + invRow[QSPForm.Common.DataDef.FINGDSTable.FLD_FGCOUP].ToString();
                        }
                        int iIndex = -1;
                        iIndex = dvCat.Find(prodCode);
                        if (iIndex == -1) {
                            invRow.Delete();
                        }
                    }
                }
            }
        }

        private void BindForm() {
            dtgWarehouseInventory.DataSource = source;
            dtgWarehouseInventory.DataBind();
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
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dtgWarehouseInventory.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgWarehouseInventory_PageIndexChnaged);

        }
        #endregion

        private void dtgWarehouseInventory_PageIndexChnaged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) {
            dtgWarehouseInventory.CurrentPageIndex = e.NewPageIndex;
            DataBind();
        }
    }
}