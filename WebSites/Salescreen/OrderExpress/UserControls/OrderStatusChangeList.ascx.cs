using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.OrderStatusChangeTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderSubList.
    /// </summary>
    public partial class OrderStatusChangeList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_PKID + " DESC";
        protected dataDef dTblOrderStatusChanges = new dataDef();
        protected DataView DVOrderStatusChanges;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_OrderID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here            
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
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
            DVOrderStatusChanges = new DataView(dTblOrderStatusChanges);
            this.DataSource = DVOrderStatusChanges;
            this.MainDataGrid = dtgOrder;
            dtgOrder.DataKeyField = OrderHeaderTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }

        #endregion

        public int OrderID {
            get {
                return c_OrderID;
            }
            set {
                c_OrderID = value;
            }
        }

        private void GetParamQueryStringFilter() {
            if (Request["OrderID"] != null) {
                c_OrderID = Convert.ToInt32(Request["OrderID"]);

            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();

            //Set the Order ID Parameter
            GetParamQueryStringFilter();

            dTblOrderStatusChanges = ordSys.SelectAllOrderStatusChangeByOrderID(c_OrderID);

            DVOrderStatusChanges = new DataView(dTblOrderStatusChanges);
            DVOrderStatusChanges.Sort = this.dtgOrder.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVOrderStatusChanges;

            lblTotal.Text = "Number of Change(s) : " + DVOrderStatusChanges.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if (this.Page.Role > AuthSystem.ROLE_FM) {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                    String sStatusDesc = "";
                    if (e.Item.DataItem != null) {
                        sStatusDesc = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_ORDER_STATUS_DESCRIPTION].ToString();
                        if (sStatusDesc.Trim().Length > 0)
                            e.Item.ToolTip = sStatusDesc;
                    }
                }
            }
        }
    }
}