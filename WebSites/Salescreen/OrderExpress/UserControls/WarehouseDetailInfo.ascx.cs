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
using dataDef = QSPForm.Common.DataDef.WarehouseData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for WarehouseForm.
    /// </summary>
    public partial class WarehouseDetailInfo : BaseWebFormControl {
        private int c_WarehouseID;
        private int c_FormID;
        protected System.Web.UI.WebControls.DataList dtLstProductDetail;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        public const string WH_ID = "WareID";
        public const string FORM_ID = "FormID";
        protected System.Web.UI.WebControls.ImageButton imgBtnDelete;
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected System.Web.UI.WebControls.HyperLink hypLnkCancel;
        private const string WH_DATA = "WareData";
        protected System.Web.UI.WebControls.ImageButton imgBtnEdit;
        protected System.Web.UI.WebControls.HyperLink hypLnkClose;
        protected dataDef dtsWarehouse;
        protected System.Web.UI.WebControls.HyperLink hypLnk_Map;

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
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_CLOSE;
            //this.QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick); 
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        protected void SetFormParameter() {
            if (Request[WH_ID] != null) {
                c_WarehouseID = Convert.ToInt32(Request[WH_ID].ToString());
            }
            else {
                c_WarehouseID = 0;
            }
            ViewState[WH_ID] = c_WarehouseID;

            if (Request[FORM_ID] != null) {
                c_FormID = Convert.ToInt32(Request[FORM_ID].ToString());
            }
            else {
                c_FormID = 0;
            }
            ViewState[FORM_ID] = c_FormID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[WH_DATA] = dtsWarehouse;
        }

        public override void BindForm() {
            WarehouseInfo_Ctrl.BindForm();
            int FulfWarehouseID = 0;
            DataRow wareRow = dtsWarehouse.Warehouse.Rows[0];
            if (!wareRow.IsNull(WarehouseTable.FLD_FULF_WAREHOUSE_ID))
                FulfWarehouseID = Convert.ToInt32(wareRow[WarehouseTable.FLD_FULF_WAREHOUSE_ID]);
            if (FulfWarehouseID > 0) {
                CtrlWarehouseInventory.FulfWarehouseID = FulfWarehouseID;
                CtrlWarehouseInventory.FormID = c_FormID;
                CtrlWarehouseInventory.DataBind();
            }
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.WarehouseSystem wareSys = new QSPForm.Business.WarehouseSystem();
                dtsWarehouse = wareSys.SelectAllDetail(c_WarehouseID);

                //this.ViewState[WH_DATA] = dtsWarehouse;
                this.ViewState[WH_ID] = c_WarehouseID;
            }
            else {
                c_WarehouseID = Convert.ToInt32(ViewState[WH_ID]);
                c_FormID = Convert.ToInt32(ViewState[FORM_ID]);
                //dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            }

            WarehouseInfo_Ctrl.WarehouseID = c_WarehouseID;
            WarehouseInfo_Ctrl.DataSource = dtsWarehouse;
        }

        //private void QSPToolBar_EditClick(object sender, EventArgs e)
        //{
        //    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.WarehouseDetail, WarehouseDetailInfo.WH_ID, c_WarehouseID.ToString());
        //    string url = "WarehouseDetail.aspx?";
        //    Response.Redirect(url + "&" + WarehouseDetailInfo.WH_ID + "=" + c_WarehouseID.ToString());
        //}
    }
}