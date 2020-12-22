using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.OrderHeaderTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderSubList.
    /// </summary>
    public partial class OrderSubList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = OrderHeaderTable.FLD_ORDER_DATE + " DESC";
        protected dataDef dTblOrders = new dataDef();
        protected DataView DVOrders;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_CampaignID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            dtgOrder.Columns[2].Visible = (this.Page.Role == AuthSystem.ROLE_FM);
            dtgOrder.Columns[3].Visible = (this.Page.Role > AuthSystem.ROLE_FM);
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
            DVOrders = new DataView(dTblOrders);
            this.DataSource = DVOrders;
            this.MainDataGrid = dtgOrder;
            dtgOrder.DataKeyField = OrderHeaderTable.FLD_PKID;
            base.LabelTotal = lblTotal;

        }

        #endregion

        public int CampaignID {
            get {
                return c_CampaignID;
            }
            set {
                c_CampaignID = value;
            }
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();

            //FM Hierarchy Filter
            string sFMID = "";
            if (Page.Role == AuthSystem.ROLE_FM)
                sFMID = Page.FMID;

            dTblOrders = ordSys.SelectAllByCampaignID(c_CampaignID, sFMID, 1);

            DVOrders = new DataView(dTblOrders);
            DVOrders.Sort = this.dtgOrder.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVOrders;

            lblTotal.Text = "Number of Order(s) : " + DVOrders.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    //sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    sID = dtgOrder.DataKeys[e.Item.ItemIndex].ToString();
                    string sIDName = BaseOrderDetail.ORDER_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.OrderDetailInfo, sIDName, sID, 0,0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "~/V2/Forms/OrderView.aspx", "OrderId", sID, 0, 0);
                    //clsUtil.SetJScriptForOpenDetailNoCMS(e.Item, "NewOrderDetailDisplay.aspx?", sIDName, sID, 0, 0);

                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.OrderDetail, sIDName, sID, 0,0);
                    //					}					
                }
            }
        }
    }
}