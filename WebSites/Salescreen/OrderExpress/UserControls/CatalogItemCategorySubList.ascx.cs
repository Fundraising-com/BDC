using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.CatalogItemCategoryTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CatalogItemCategorySubList.
    /// </summary>
    public partial class CatalogItemCategorySubList : BaseWebUserControl {
        private const string DEFAULT_SORT = dataDef.FLD_NAME;
        protected dataDef dTblCatalogItemCategory = new dataDef();
        protected DataView DVCatalogItemCategory;
        private int c_CatalogID;
        private string c_CatalogName = "";

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

        public string CatalogName {
            get {
                return c_CatalogName;
            }
            set {
                c_CatalogName = value;
            }
        }

        public dataDef DataSource {
            get {
                return dTblCatalogItemCategory;
            }
            set {
                dTblCatalogItemCategory = value;
            }
        }

        public void BindForm() {
            DVCatalogItemCategory = new DataView(dTblCatalogItemCategory);
            BindTreeViewCategory();
        }

        public void BindTreeViewCategory() {
            //Clear all items in the treeview
            trvwCategory.Nodes.Clear();

            //Verify if All ParentID are in the DataSet
            //If not replace the ParentID by NULL, upgrade to Root
            //			DataView DVNotRoots = new DataView(dTblCatalogItemCategory);
            //			DVNotRoots.RowFilter = "ISNULL(" + dataDef.FLD_PARENT_ID + ",-99999) <> -99999";
            DVCatalogItemCategory.Sort = dataDef.FLD_PKID;
            //			DVNotRoots.Sort = dataDef.FLD_PKID;
            foreach (DataRowView drvw in DVCatalogItemCategory) {
                //Find the parent in collection
                if (!drvw.Row.IsNull(dataDef.FLD_PARENT_ID)) {
                    int ParentID = Convert.ToInt32(drvw[dataDef.FLD_PARENT_ID]);
                    int iIndex = DVCatalogItemCategory.Find(ParentID);
                    if (iIndex == -1) {
                        drvw[dataDef.FLD_PARENT_ID] = DBNull.Value;
                    }
                }
            }

            //Adding The Root Nodes
            DataView DVRoots = new DataView(dTblCatalogItemCategory);
            DVRoots.RowFilter = "ISNULL(" + dataDef.FLD_PARENT_ID + ",-99999) = -99999";

            foreach (DataRowView rootRow in DVRoots) {
                TreeNode Nodx = new TreeNode();
                Nodx.Value = rootRow[dataDef.FLD_PKID].ToString();
                Nodx.Text = rootRow[dataDef.FLD_NAME].ToString();
                //Nodx.Type = "root";
                trvwCategory.Nodes.Add(Nodx);
                AddChildNode(Nodx);
            }
        }

        public void AddChildNode(TreeNode Nodx) {
            //Adding The Root Nodes

            DataView DVChilds = new DataView(dTblCatalogItemCategory);
            DVChilds.RowFilter = dataDef.FLD_PARENT_ID + " = " + Nodx.Value;

            foreach (DataRowView dtrvw in DVChilds) {
                TreeNode Nodc = new TreeNode();
                Nodc.Value = dtrvw[dataDef.FLD_PKID].ToString();
                Nodc.Text = dtrvw[dataDef.FLD_NAME].ToString();
                //Nodc.Type = "Child";
                Nodx.ChildNodes.Add(Nodc);
                AddChildNode(Nodc);
            }
        }

        //		public TreeNode FindSelectedNode(TreeView trvw)
        //		{
        //			String SelectedNodeIndex = trvw.SelectedNodeIndex;
        //			TreeNode Nods  = new TreeNode();
        //			TreeNodeCollection NodsColl  = new TreeNodeCollection();
        //			String[] sarr = SelectedNodeIndex.Split('.');
        //			int iIndex = 0;
        //			NodsColl = trvw.Nodes;
        //			while(iIndex < sarr.Length)
        //			{
        //				int NodIndex = Convert.ToInt32(sarr[iIndex]);
        //				Nods = NodsColl[NodIndex];
        //				
        //				if(Nods.Nodes.Count > 0)
        //				{
        //					NodsColl = Nods.Nodes;
        //				}
        //				iIndex += 1;
        //			}
        //
        //			return Nods;
        //
        //		}

        //		protected void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e)
        //		{
        //			
        //			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        //			{
        //				String sID = "0";
        //				if (e.Item.DataItem != null)
        //				{
        //					//sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
        //					sID = dtgCatalogItemCategory.DataKeys[e.Item.ItemIndex].ToString();
        //					string sIDName = CatalogItemCategoryDetail.ORDER_ID;
        //					clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.CatalogItemCategoryDetailInfo, sIDName, sID, 0,0);
        //
        ////					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
        ////					if (imgBtnDetail != null)
        ////					{
        ////						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, QSPForm.Business.AppItem.CatalogItemCategoryDetail, sIDName, sID, 0,0);
        ////					}					
        //				}		
        //			}			
        //		}
    }
}