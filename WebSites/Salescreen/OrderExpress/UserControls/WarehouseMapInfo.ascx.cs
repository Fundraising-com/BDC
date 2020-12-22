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
    public partial class WarehouseMapInfo : BaseWebFormControl {
        private int c_WarehouseID;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                //				LoadData();	
                //				if (!IsPostBack)
                //				{				
                //					BindForm();
                //				}
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

        }
        #endregion

        //		protected void SetFormParameter()
        //		{
        //			if (Request[WH_ID] != null)
        //			{
        //				c_WarehouseID = Convert.ToInt32(Request[WH_ID].ToString());
        //			}
        //			else
        //			{
        //				c_WarehouseID = 0;
        //			}
        //			ViewState[WH_ID] = c_WarehouseID;	
        //
        //		}

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //this.ViewState[WH_DATA] = dtsWarehouse;				
        }

        public override void BindForm() {
        }

        protected override void LoadData() {
            //			if (!IsPostBack)
            //			{
            //				SetFormParameter();
            //				QSPForm.Business.WarehouseSystem wareSys = new QSPForm.Business.WarehouseSystem();
            //				dtsWarehouse = wareSys.SelectAllDetail(c_WarehouseID);
            //
            //				//this.ViewState[WH_DATA] = dtsWarehouse;
            //				this.ViewState[WH_ID] = c_WarehouseID;
            //
            //			}
            //			else
            //			{
            //				c_WarehouseID = Convert.ToInt32(ViewState[WH_ID]);
            //				//dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            //			}
        }
    }
}