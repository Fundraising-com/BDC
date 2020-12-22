using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using System.Collections;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Order Supply List.
    /// </summary>
    public partial class OrderSupplyGrid : BaseOrderSupplyList {
        protected dataDef dTblOrderDetail = new dataDef();
        private int c_ParentID;
        private int c_FormID = 0;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected QSPForm.Common.DataDef.CatalogItemDetailTable tblCatalogItemDetail = new QSPForm.Common.DataDef.CatalogItemDetailTable();
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();
        private const string ARRAY_CODE = "arrSupplyCode";
        private const string TABLE_SUPPLY = "SupplyTable";
        private bool IsBound = false;
        int c_DefaultQuantity = 0;

        private ArrayList lstDetail = new ArrayList();
        private string lblTotalQtyID = "";
        private DataGridItem dtgFooter;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            LoadSupplyTable();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtgOrderDetail.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgOrderDetail_DeleteCommand);
            this.dtgOrderDetail.ItemCreated += new DataGridItemEventHandler(dtgOrderDetail_ItemCreated);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public override void BindForm() {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (!IsBound) {
                UpdateDataSource();
                BindGrid();
            }
            ViewState[TABLE_SUPPLY] = tblCatalogItemDetail;

            /*updated javascript*/
            FillDataGridInfo();
            AddJavascript();
        }

        public override int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public override int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
            }
        }

        public override dataDef DataSource {
            get {
                return dTblOrderDetail;
            }
            set {
                dTblOrderDetail = value;
            }
        }

        public override int DefaultQuantity {
            get {
                return c_DefaultQuantity;
            }
            set {
                c_DefaultQuantity = value;
            }
        }

        private void BindGrid() {
            FillDataTableForDropDownList();
            this.dtgOrderDetail.DataBind();
            IsBound = true;
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //LoadDataSource();
            UpdateDataSource();
            DataRow BlankRow;
            BlankRow = dTblOrderDetail.NewRow();
            BlankRow[dataDef.FLD_ORDER_ID] = ParentID;
            dTblOrderDetail.Rows.Add(BlankRow);
            BindGrid();
        }

        private void FillDataTableForDropDownList() {
            try {
                LoadSupplyTable();
                BuildArrayStringForSupply();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void LoadSupplyTable() {
            try {

                if (ViewState[TABLE_SUPPLY] != null) {
                    tblCatalogItemDetail = (CatalogItemDetailTable)ViewState[TABLE_SUPPLY];
                }

                if (tblCatalogItemDetail.Rows.Count == 0) {
                    if (c_FormID != 0) {
                        QSPForm.Business.CatalogItemDetailSystem catSys = new QSPForm.Business.CatalogItemDetailSystem();

                        //Catalog Item Detail (Product and multiple price)	
                        //int FormID  = this.Page.Fo
                        tblCatalogItemDetail = catSys.SelectAllSupplyByFormID(c_FormID);
                        DataRow row = tblCatalogItemDetail.NewRow();
                        row[CatalogItemDetailTable.FLD_PKID] = 0;
                        row[CatalogItemDetailTable.FLD_PRICE] = 0;
                        row[CatalogItemDetailTable.FLD_NB_UNITS] = c_DefaultQuantity;
                        row[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME] = "---SELECT A SUPPLY---";
                        tblCatalogItemDetail.Rows.InsertAt(row, 0);
                        ViewState[TABLE_SUPPLY] = tblCatalogItemDetail;
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void BuildArrayStringForSupply() {
            String scriptArrayCode = "";
            foreach (DataRow row in tblCatalogItemDetail.Rows) {
                if (scriptArrayCode.Length > 0)
                    scriptArrayCode += " ,";
                scriptArrayCode += "new String('" + row[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE].ToString() + "')";
            }

            Page.RegisterArrayDeclaration(ARRAY_CODE, scriptArrayCode);
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            int iCounter = 0;
            try {
                if (sValue != "") {
                    foreach (DataRow row in dt.Rows) {
                        if (row[0].ToString() == sValue) {
                            iIndex = iCounter;
                            break;
                        }
                        iCounter = iCounter + 1;
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return iIndex;
        }

        private void dtgOrderDetail_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            UpdateDataSource();
            //c_OrderDetailID = Convert.ToInt32(dtgOrderDetail.DataKeys[e.Item.ItemIndex]);
            // get row to delete in grid
            DataRow row = dTblOrderDetail.Rows[e.Item.ItemIndex];
            if (row.RowState != DataRowState.Added) {
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            }
            row.Delete();
            BindGrid();
        }

        public override bool UpdateDataSource() {
            bool IsSuccess = false;

            // get edited row values in grid
            int ShipmentGroupID = 1;

            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataRow row;
                row = dTblOrderDetail.Rows[iCount];
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                if (row.RowState != DataRowState.Deleted) {
                    row[dataDef.FLD_ORDER_ID] = c_ParentID;
                    row[dataDef.FLD_CATALOG_ITEM_CODE] = ((Label)dgItem.FindControl("lblCatalogItemCode")).Text;
                    DropDownList ddlCatalogItemDetail = ((DropDownList)dgItem.FindControl("ddlCatalogItemDetail"));
                    row[dataDef.FLD_CATALOG_ITEM_DETAIL_ID] = ddlCatalogItemDetail.SelectedValue;
                    row[dataDef.FLD_CATALOG_ITEM_DESC] = ddlCatalogItemDetail.SelectedItem.Text;
                    row[dataDef.FLD_CATALOG_ITEM_NAME] = ddlCatalogItemDetail.SelectedItem.Text;
                    row[dataDef.FLD_QUANTITY] = ((TextBox)dgItem.FindControl("txtQuantity")).Text;
                    row[dataDef.FLD_ADJUSTMENT_QUANTITY] = 0;
                    row[dataDef.FLD_SHIPMENT_GROUP_ID] = ShipmentGroupID;
                    RefreshSupplyInfo(row, ddlCatalogItemDetail);

                    if (row.RowState == DataRowState.Added) {
                        row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                    }
                    else {
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }
            }

            //			bool IsFound = true;
            //			int iIndexFound = -1;
            //			while (IsFound)
            //			{
            //				iIndexFound = -1;
            //				for(int i=0;i<dTblOrderDetail.Rows.Count;i++)
            //				{
            //					DataRow detailRow = dTblOrderDetail.Rows[i];
            //					if (detailRow.RowState == DataRowState.Added)
            //					{
            //						if (detailRow[OrderDetailTable.FLD_QUANTITY].ToString() == "0"  &&
            //							detailRow[OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_ID].ToString() == "0")
            //						{
            //							iIndexFound = i;
            //							break;							
            //						}
            //					}
            //				}
            //				if (iIndexFound == -1)
            //				{
            //					IsFound = false;
            //					break;
            //				}
            //				else
            //				{
            //					dTblOrderDetail.Rows.RemoveAt(iIndexFound);
            //				}
            //			}

            //Operation Sucessful
            IsSuccess = true;

            return IsSuccess;
        }

        //		private void dtgOrderDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        //		{
        //			if (e.Item.ItemType == ListItemType.Footer)
        //			{
        //				//Apply calculation Total
        //				int totalQty = 0;				
        //				DataView dv = new DataView(dTblOrderDetail);
        //				foreach (DataRowView dvRow in dv)
        //				{
        //					int Qty = 0;
        //					if (dvRow[OrderDetailTable.FLD_QUANTITY] != System.DBNull.Value)
        //					{
        //						Qty = Convert.ToInt32(dvRow[OrderDetailTable.FLD_QUANTITY]);
        //					}
        //					totalQty = totalQty + Qty;
        //					
        //				}
        //				Label lblTotalQuantity = (Label)e.Item.FindControl("lblTotalQuantity");
        //				lblTotalQuantity.Text = totalQty.ToString();
        //				
        //			
        //			}
        //		}

        private void AddJavascript() {
            string script = "";
            script += "	<script> \n";
            script += " var QtySupplyList = new Array (" + ArrayListToString(lstDetail) + ");\n";

            script += "	function OrderSupplyRefresh()\n";
            script += "	{\n";
            script += "		OSL_RefreshQuantityTotal();\n";
            script += "	}\n";

            script += "	function OSL_RefreshQuantityTotal()\n";
            script += "	{\n";
            script += "     var cptList = 0;\n";
            script += "     for(var x = 0; x < QtySupplyList.length; x++)\n";
            script += "     {\n";
            script += "         if(!isNaN(parseInt(document.getElementById(QtySupplyList[x]).value)))\n";
            script += "            cptList += parseInt(document.getElementById(QtySupplyList[x]).value);\n";
            script += "     }\n";
            script += "     document.getElementById(\"" + this.lblTotalQtyID + "\").innerHTML = cptList;\n";
            script += " }\n";
            script += "</script>\n";

            this.Page.RegisterClientScriptBlock("OrderSupplyList_RefreshGrandTotal", script);
        }

        private void FillDataGridInfo() {
            int qty = 0;

            foreach (DataGridItem e in this.dtgOrderDetail.Items) {
                if ((e.ItemType == ListItemType.Item) || (e.ItemType == ListItemType.AlternatingItem)) {
                    ((TextBox)e.FindControl("txtQuantity")).Attributes.Add("onKeyUp", "OrderSupplyRefresh();");
                    lstDetail.Add(((TextBox)e.FindControl("txtQuantity")).ClientID);

                    //Modified to be able to adjust the total at postback
                    qty += Convert.ToInt32(((TextBox)e.FindControl("txtQuantity")).Text);

                    ((DropDownList)e.FindControl("ddlCatalogItemDetail")).Attributes["onchange"] = "updateCode(this,'" + ((Label)e.FindControl("lblCatalogItemCode")).ClientID + "');";
                }
            }

            lblTotalQtyID = ((Label)dtgFooter.FindControl("lblTotalQuantity")).ClientID;

            //adjust value with viewstate
            ((Label)dtgFooter.FindControl("lblTotalQuantity")).Text = qty.ToString();
        }

        private void RefreshSupplyInfo(DataRow row, DropDownList ddl) {
            row[dataDef.FLD_CATALOG_ITEM_DETAIL_ID] = ddl.SelectedValue;
            row[dataDef.FLD_CATALOG_ITEM_DESC] = ddl.SelectedItem.Text;
            //Find it in the table to get more info
            int catalogItemDetailID = Convert.ToInt32(ddl.SelectedValue);
            DataView dv = new DataView(tblCatalogItemDetail);
            dv.Sort = CatalogItemDetailTable.FLD_PKID;
            int icatIndex = dv.Find(catalogItemDetailID);
            if (icatIndex > -1) {
                row[dataDef.FLD_CATALOG_ITEM_CODE] = dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];

                if (dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS] != System.DBNull.Value)
                    row[dataDef.FLD_CATALOG_ITEM_NB_UNITS] = dv[icatIndex][CatalogItemDetailTable.FLD_PRICE];
            }
        }

        public override bool ValidateForm() {
            bool blnValid = false;
            if (!this.IsValid()) {
                CommonUtility clsUtil = new CommonUtility();
                //To don't touch to the error
                IsBound = true;
                blnValid = false;
                clsUtil.RenderStartUpScroll(ValSum);
            }
            else {
                blnValid = true;
            }
            //if everything have been ok
            ValSum.Visible = !blnValid;
            trValSum.Visible = !blnValid;
            return blnValid;
        }

        private void dtgOrderDetail_ItemCreated(object sender, DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {
                dtgFooter = e.Item; // used for FillDataGridInfo coz at this point, clientID is invalide
            }
        }

        private string ArrayListToString(ArrayList al) {
            string field = "";
            if (al.Count > 0) {
                for (int i = 0; i < al.Count; i++) {
                    field += '"' + al[i].ToString() + '"';
                    if (i < al.Count - 1) {
                        field += ",";
                    }
                }
            }
            return field;
        }
    }
}