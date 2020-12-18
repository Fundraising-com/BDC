using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.WarehouseData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for WarehouseForm.
    /// </summary>
    public partial class WarehouseBusinessCalendarForm : BaseWebFormControl {
        protected dataDef dtsWarehouse;
        private int c_WarehouseID = 0;
        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                //DateTime calDate = DateTime.Today;			
                //BizCalendar_Ctrl.SelectedDate = calDate;
                //BizCalendar_Ctrl.SelectedDate = calDate;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            BizCalendar_Ctrl.IsSelector = false;
            BizCalendar_Ctrl.IsBizCal = true;
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        public int WarehouseID {
            get {
                return c_WarehouseID;
            }
            set {
                c_WarehouseID = value;
                BizCalendar_Ctrl.WarehouseID = c_WarehouseID;
            }
        }

        public dataDef DataSource {
            get {
                return dtsWarehouse;
            }
            set {
                dtsWarehouse = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public new void BindForm() {
        }

        public bool ValidateForm() {
            if (!IsValid()) {
                return false;
            }

            //if everything have been ok
            return true;
        }

        public bool UpdateDataSource() {
            return true;
        }

        protected override void OnDataBinding(EventArgs e) {
            BindForm();
        }

        private void FillDataTableForDropDownList() {
            try {
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}