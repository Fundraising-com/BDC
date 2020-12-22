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
    public partial class OrderSupplyList : BaseOrderSupplyList {
        protected dataDef dTblOrderDetail = new dataDef();
        protected DataView dvOrderDetail = new DataView();
        private int c_ParentID;
        private int c_FormID = 0;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected QSPForm.Common.DataDef.CatalogItemDetailTable tblCatalogItemDetail = new QSPForm.Common.DataDef.CatalogItemDetailTable();
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();
        protected System.Web.UI.WebControls.DropDownList ddlCatalogItemDetail;
        int c_DefaultQuantity = 0;
        private ArrayList lstDetail = new ArrayList();
        private string lblTotalQtyID = "";
        private DataGridItem dtgFooter;

        protected void Page_Load(object sender, System.EventArgs e) {
            //AddJavascript();
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            //this.dtgOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgOrderDetail_ItemDataBound);
            this.dtgOrderDetail.ItemCreated += new DataGridItemEventHandler(dtgOrderDetail_ItemCreated);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public override void BindForm() {
            try {
                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
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

        public string LabelTotalQuantityClientID {
            get {
                if (dtgFooter != null) {
                    Label lblTotal = ((Label)dtgFooter.FindControl("lblTotalQuantity"));
                    if (lblTotal != null) {
                        //lblTotal.Text = qty.ToString();
                        lblTotalQtyID = lblTotal.ClientID; ;
                    }
                }
                return lblTotalQtyID;
            }
        }

        private void BindGrid() {
            dvOrderDetail.Table = dTblOrderDetail;
            dvOrderDetail.Sort = dataDef.FLD_DISPLAY_ORDER;
            this.dtgOrderDetail.DataBind();
        }

        public override bool UpdateDataSource() {
            bool IsSuccess = false;

            // get edited row values in grid
            int ShipmentGroupID = 1;
            DataView dv = new DataView(dTblOrderDetail);
            dv.Sort = dataDef.FLD_PKID;

            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                int iIndex = -1;

                int SupplyDetailID = Convert.ToInt32(dtgOrderDetail.DataKeys[iCount]);
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];

                iIndex = dv.Find(SupplyDetailID);

                if (iIndex != -1) {
                    DataRow row = dv[iIndex].Row;
                    row[dataDef.FLD_ORDER_ID] = c_ParentID;
                    if (((TextBox)dgItem.FindControl("txtQuantity")).Text.Length > 0) {
                        row[dataDef.FLD_QUANTITY] = ((TextBox)dgItem.FindControl("txtQuantity")).Text;
                    }
                    else {
                        row[dataDef.FLD_QUANTITY] = 0;
                    }
                    row[dataDef.FLD_ADJUSTMENT_QUANTITY] = 0;
                    row[dataDef.FLD_SHIPMENT_GROUP_ID] = ShipmentGroupID;

                    if (row.RowState == DataRowState.Added) {
                        row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                    }
                    else {
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }
            }

            //Operation Sucessful
            IsSuccess = true;

            return IsSuccess;
        }

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
            script += "         if(!isNaN(parseInt(ValidateFieldValue(document.getElementById(QtySupplyList[x]).value))))\n";
            script += "            cptList += parseInt(ValidateFieldValue(document.getElementById(QtySupplyList[x]).value));\n";
            script += "     }\n";
            script += "     document.getElementById(\"" + this.lblTotalQtyID + "\").innerHTML = cptList;\n";
            script += " }\n";

            script += "function ValidateFieldValue(fValue)\n";
            script += "{\n";
            script += "var trimValue = fValue.replace(/ /g,'');\n";
            script += "if(trimValue == '')\n";
            script += "	trimValue = '0';\n";
            script += "return trimValue;\n";
            script += "}\n";
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
                    //qty += Convert.ToInt32( ((TextBox)e.FindControl("txtQuantity")).Text );
                    qty += ConvertIntField((TextBox)e.FindControl("txtQuantity"));
                    if (((TextBox)e.FindControl("txtQuantity")).Text == "0") {
                        ((TextBox)e.FindControl("txtQuantity")).Text = "";
                    }
                }

                if (dtgFooter != null) {
                    Label lblTotal = ((Label)dtgFooter.FindControl("lblTotalQuantity"));
                    if (lblTotal != null) {
                        lblTotal.Text = qty.ToString();
                        lblTotalQtyID = lblTotal.ClientID; ;
                    }
                }
            }
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
                blnValid = false;
                clsUtil.RenderStartUpScroll(ValSum);
                Page.MaintainScrollPositionOnPostBack = false;

            }
            else {
                blnValid = true;
            }
            //if everything have been ok
            ValSum.Visible = !blnValid;
            trValSum.Visible = !blnValid;
            return blnValid;

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

        private void dtgOrderDetail_ItemCreated(object sender, DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {

                dtgFooter = e.Item; // used for FillDataGridInfo coz at this point, clientID is invalide
            }
        }

        private int ConvertIntField(TextBox txt) {
            int Result = 0;
            Int32.TryParse(txt.Text.Trim(), out Result);
            return Result;
        }
    }
}