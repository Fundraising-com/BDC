//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_WarehouseDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'WarehouseDetail.ascx' was also modified to refer to the new class name.
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
using dataDef = QSPForm.Common.DataDef.WarehouseData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>Credit Application Detail form</summary>
    public partial class WarehouseDetail : BaseWarehouseDetail {
        private int c_WarehouseID;
        protected System.Web.UI.WebControls.ImageButton imgBtnSave;
        protected dataDef dtsWarehouse;

        private const string WH_DATA = "WarehouseData";

        protected void Page_Load(object s, System.EventArgs e) {
            try {
                LoadData();
                if (!IsPostBack) {
                    BindForm();
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        #region auto-generated code
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            this.QSPToolBar.DisplayMode = ToolBar.DISPLAY_EDIT;
            this.QSPToolBar.SaveClick += new EventHandler(this.QSPToolBar_SaveClick);
            this.QSPToolBar.DeleteClick += new EventHandler(this.QSPToolBar_DeleteClick);
            this.QSPToolBar.DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this user ?');");

            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }
        #endregion auto-generated code

        private void SetFormParameter() {
            if (Request[WH_ID] != null) {
                c_WarehouseID = Convert.ToInt32(Request[WH_ID].ToString());
            }
            else {
                c_WarehouseID = 0;
            }
            ViewState[WH_ID] = c_WarehouseID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[WH_DATA] = dtsWarehouse;
            //Set the close button
            if (!IsPostBack) {
                if (dtsWarehouse.Warehouse.Rows.Count > 0) {
                    if (dtsWarehouse.Warehouse.Rows[0].RowState != DataRowState.Added) {
                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.WarehouseDetailInfo, WarehouseDetailInfo.WH_ID, c_WarehouseID.ToString());
                        string url = "~/WarehouseDetailInfo.aspx?" + WarehouseDetailInfo.WH_ID + "=" + c_WarehouseID.ToString();
                        this.QSPToolBar.CancelButton.NavigateUrl = url;
                    }
                }
            }
        }

        public override void BindForm() {
            WarehouseForm_Ctrl.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.WarehouseSystem wareSys = new QSPForm.Business.WarehouseSystem();
                dtsWarehouse = wareSys.SelectAllDetail(c_WarehouseID);
                if (dtsWarehouse.Warehouse.Rows.Count == 0) {
                    dtsWarehouse = wareSys.InitializeWarehouse(this.Page.UserID);
                }
                this.ViewState[WH_ID] = c_WarehouseID;
                this.ViewState[WH_DATA] = dtsWarehouse;
            }
            else {
                c_WarehouseID = Convert.ToInt32(this.ViewState[WH_ID]);
                dtsWarehouse = (dataDef)this.ViewState[WH_DATA];
            }
            WarehouseForm_Ctrl.WarehouseID = c_WarehouseID;
            WarehouseForm_Ctrl.DataSource = dtsWarehouse;
            WarehouseBizCal.WarehouseID = c_WarehouseID;
            WarehouseBizCal.DataSource = dtsWarehouse;
        }

        private void QSPToolBar_DeleteClick(object sender, EventArgs e) {
            //			DeleteUser();
        }

        private void QSPToolBar_SaveClick(object sender, EventArgs e) {
            try {
                bool blnValid = true;
                blnValid = WarehouseForm_Ctrl.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = WarehouseBizCal.ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = WarehouseForm_Ctrl.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                blnValid = WarehouseBizCal.UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                QSPForm.Business.WarehouseSystem wareSys = new QSPForm.Business.WarehouseSystem();

                if (dtsWarehouse.Warehouse.Rows[0].RowState == DataRowState.Added) {
                    blnValid = wareSys.InsertAllDetail(dtsWarehouse);
                }
                else {
                    blnValid = wareSys.UpdateAllDetail(dtsWarehouse);
                }

                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.WarehouseDetailInfo, WarehouseDetailInfo.WH_ID, c_WarehouseID.ToString());
                string url = "~/WarehouseDetailInfo.aspx?" + WarehouseDetailInfo.WH_ID + "=" + c_WarehouseID.ToString();
                Response.Redirect(url);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}