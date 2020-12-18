using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CatalogGroupCatalogTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CatalogGroupSubList.
    /// </summary>
    public partial class CatalogGroupCatalogSubList : BaseWebUserControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME;
        protected dataDef dTblCatalogGroup = new dataDef();
        protected DataView DVCatalogGroups;
        private int c_CatalogID;

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

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }

        #endregion

        public int CatalogID {
            get {
                return c_CatalogID;
            }
            set {
                c_CatalogID = value;
            }
        }

        public dataDef DataSource {
            get {
                return dTblCatalogGroup;

            }
            set {
                dTblCatalogGroup = value;
            }
        }

        public void BindForm() {
            DVCatalogGroups = new DataView(dTblCatalogGroup);
            dtgCatalogGroup.DataBind();
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
        //					sID = dtgCatalogGroup.DataKeys[e.Item.ItemIndex].ToString();
        //					string sIDName = CatalogGroupDetail.ORDER_ID;
        //					clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.CatalogGroupDetailInfo, sIDName, sID, 0,0);
        //
        ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
        ////					if (imgBtnDetail != null)
        ////					{
        ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.CatalogGroupDetail, sIDName, sID, 0,0);
        ////					}					
        //				}		
        //			}			
        //		}
    }
}