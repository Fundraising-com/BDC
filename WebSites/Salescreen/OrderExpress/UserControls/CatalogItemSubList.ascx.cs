using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CatalogItemTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CatalogItemSubList.
    /// </summary>
    public partial class CatalogItemSubList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME;
        protected dataDef dTblCatalogItem = new dataDef();
        protected DataView DVCatalogItems;
        private int c_CatalogID;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            //Override the method to don't have a query done on load event
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

        #endregion

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVCatalogItems = new DataView(dTblCatalogItem);
            base.DataSource = DVCatalogItems;
            this.MainDataGrid = dtgCatalogItem;
            dtgCatalogItem.DataKeyField = dataDef.FLD_PKID;
            base.LabelTotal = lblTotal;
        }

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            string sCriteria = this.dtgCatalogItem.FilterExpression;

            DVCatalogItems = new DataView(dTblCatalogItem);

            DVCatalogItems.Sort = dtgCatalogItem.SortExpression;

            lblTotal.Text = "Number of Catalog Item(s) : " + DVCatalogItems.Count.ToString();
        }

        public int CatalogID {
            get {
                return c_CatalogID;
            }
            set {
                c_CatalogID = value;
            }
        }

        public new dataDef DataSource {
            get {
                return dTblCatalogItem;
            }
            set {
                dTblCatalogItem = value;
            }
        }

        public override void BindForm() {

            dtgCatalogItem.DataBind();
        }

        //		protected void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e)
        //		{
        //			
        //			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //			{
        //				String sID = "0";
        //				if (e.Item.DataItem != null)
        //				{
        //					//sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
        //					sID = dtgCatalogItem.DataKeys[e.Item.ItemIndex].ToString();
        //					string sIDName = CatalogItemDetail.ORDER_ID;
        //					clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.CatalogItemDetailInfo, sIDName, sID, 0,0);
        //
        ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
        ////					if (imgBtnDetail != null)
        ////					{
        ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.CatalogItemDetail, sIDName, sID, 0,0);
        ////					}					
        //				}		
        //			}			
        //		}
    }
}