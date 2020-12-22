using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using System.Collections;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class OrderDetailGrid : BaseOrderDetailList//BaseWebUserControl
    {
        protected dataDef dTblOrderDetail = new dataDef();
        private OrderData dtsOrder = new OrderData();
        private int c_ParentID;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected CatalogItemDetailTable tblCatalogItemDetail = new CatalogItemDetailTable();
        private const string ARRAY_CODE = "arrProductCode";
        private const string ARRAY_PRICE = "arrProductPrice";
        private const string ARRAY_NB_UNIT = "arrProductNbUnit";
        private const string TABLE_PRODUCT = "ProductTable";
        private const int MINIMUM_QTY = 8;
        private const string IS_PRICE_UPDATABLE = "IsPriceUpdatable";
        private const string TAX_RATE = "TaxRate";
        private decimal taxRate = 0;
        private int c_FormID = 0;
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();
        private bool IsBound = false;
        private bool IsPriceUpdatable = false;

        private ArrayList lstFieldAmount = new ArrayList();
        private ArrayList lstFieldQty = new ArrayList();
        private ArrayList lstMinFieldQty = new ArrayList();
        private DataGridItem dtgFooter;
        private string lblAmountTotalID = "";
        private string lblQTYTotalID = "";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here	
            LoadProductTable();
            InitDataGrid_Validator();
            if (!IsPostBack) {
                QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();
                QSPForm.Common.DataDef.BusinessExceptionTable dTblExc = excSys.SelectAllByNoAppItem(QSPForm.Business.AppItem.OrderForm_Step4, c_FormID);
                if (dTblExc.Rows.Count > 0) {
                    trBusinessMessage.Visible = true;
                    lblBusinessMessage.Text = dTblExc.Rows[0][BusinessExceptionTable.FLD_MESSAGE].ToString();
                }
                if (c_FormID != 0) {
                    QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
                    FormTable dTblForm = frmSys.SelectOne(c_FormID);
                    if (dTblForm.Rows.Count > 0) {
                        DataRow frmRow = dTblForm.Rows[0];
                        if (!frmRow.IsNull(FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE))
                            IsPriceUpdatable = Convert.ToBoolean(frmRow[FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE]);
                    }
                }

                dtgOrderDetail.Columns[7].Visible = IsPriceUpdatable;
                dtgOrderDetail.Columns[8].Visible = !IsPriceUpdatable;
            }
            else {
                if (ViewState[IS_PRICE_UPDATABLE] != null)
                    IsPriceUpdatable = Convert.ToBoolean(ViewState[IS_PRICE_UPDATABLE]);
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtgOrderDetail.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgOrderDetail_DeleteCommand);
            this.dtgOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgOrderDetail_ItemDataBound);
            this.dtgOrderDetail.ItemCreated += new DataGridItemEventHandler(dtgOrderDetail_ItemCreated);
            this.CustVal_MinQty.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_MinQty_ServerValidate);
        }
        #endregion
        private void dtgOrderDetail_ItemCreated(object sender, DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {
                dtgFooter = e.Item; // used in FillDataGridInfo coz at this point, clientID is invalide
            }
        }

        private void InitDataGrid_Validator() {
            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                CustomValidator custVal = (CustomValidator)dgItem.FindControl("CustVal_DuplicateProduct");
                if (custVal != null) {
                    custVal.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_DuplicateProduct_ServerValidate);
                }
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
            ViewState[TABLE_PRODUCT] = tblCatalogItemDetail;
            ViewState[IS_PRICE_UPDATABLE] = IsPriceUpdatable;
            BuildArrayStringForProduct();

            /* Updated Javascript
             * */
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

        public override OrderData DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
                dTblOrderDetail = dtsOrder.OrderDetail;
            }
        }

        private void BindGrid() {
            FillDataTableForDropDownList();
            this.dtgOrderDetail.DataBind();
            IsBound = true;
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Desactivated this requirement.
            CustVal_MinQty.Enabled = false;
            try {
                if (ValidateForm()) {
                    UpdateDataSource();
                    DataRow BlankRow;
                    BlankRow = dTblOrderDetail.NewRow();
                    BlankRow[dataDef.FLD_ORDER_ID] = ParentID;
                    BlankRow[dataDef.FLD_TAX_RATE] = taxRate;

                    dTblOrderDetail.Rows.Add(BlankRow);
                    BindGrid();
                }
                else {
                    RefreshAllGridProductInfo();
                }
            }
            catch (Exception ex) {
            }
            //Desactivated this requirement.
            CustVal_MinQty.Enabled = true;
        }

        private void FillDataTableForDropDownList() {
            try {
                LoadProductTable();
                //BuildArrayStringForProduct();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void LoadProductTable() {
            try {
                if (ViewState[TABLE_PRODUCT] != null) {
                    tblCatalogItemDetail = (CatalogItemDetailTable)ViewState[TABLE_PRODUCT];
                }

                if (tblCatalogItemDetail.Rows.Count == 0) {
                    if (c_FormID != 0) {
                        QSPForm.Business.CatalogItemDetailSystem catSys = new QSPForm.Business.CatalogItemDetailSystem();

                        //Catalog Item Detail (Product and multiple price)	
                        //int FormID  = this.Page.Fo
                        tblCatalogItemDetail = catSys.SelectAllByFormID(c_FormID);

                        //Apply tax Rate to all items
                        foreach (DataRow row in tblCatalogItemDetail.Rows) {
                            row[CatalogItemDetailTable.FLD_TAX_RATE] = taxRate;
                        }
                        DataRow newRow = tblCatalogItemDetail.NewRow();
                        newRow[CatalogItemDetailTable.FLD_PKID] = 0;
                        newRow[CatalogItemDetailTable.FLD_PRICE] = 0;
                        newRow[CatalogItemDetailTable.FLD_NB_UNITS] = 0;
                        newRow[CatalogItemDetailTable.FLD_CATALOG_ITEM_NAME] = "---SELECT A PRODUCT---";
                        tblCatalogItemDetail.Rows.InsertAt(newRow, 0);

                        ViewState[TABLE_PRODUCT] = tblCatalogItemDetail;
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void BuildArrayStringForProduct() {
            String scriptArrayCode = "";
            String scriptArrayPrice = "";
            String scriptArrayNbUnit = "";
            foreach (DataRow row in tblCatalogItemDetail.Rows) {
                if (scriptArrayCode.Length > 0)
                    scriptArrayCode += " ,";
                scriptArrayCode += "new String('" + row[CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE].ToString() + "')";
                if (scriptArrayPrice.Length > 0)
                    scriptArrayPrice += " ,";
                scriptArrayPrice += "new String('" + row[CatalogItemDetailTable.FLD_PRICE].ToString() + "')";
                if (scriptArrayNbUnit.Length > 0)
                    scriptArrayNbUnit += " ,";
                scriptArrayNbUnit += "new String('" + row[CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS].ToString() + "')";
            }

            Page.RegisterArrayDeclaration(ARRAY_CODE, scriptArrayCode);
            Page.RegisterArrayDeclaration(ARRAY_PRICE, scriptArrayPrice);
            Page.RegisterArrayDeclaration(ARRAY_NB_UNIT, scriptArrayNbUnit);
        }

        public override int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
            }
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
            if (row.RowState == DataRowState.Added) {
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            }
            row.Delete();
            BindGrid();
        }

        public override bool UpdateDataSource() {
            bool IsSuccess = false;
            CommonUtility clsUtil = new CommonUtility();
            // get edited row values in grid

            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataRow row;
                row = dTblOrderDetail.Rows[iCount];
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                if (row.RowState != DataRowState.Deleted) {
                    clsUtil.UpdateRow(row, dataDef.FLD_ORDER_ID, c_ParentID.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_CODE, ((Label)dgItem.FindControl("lblCatalogItemCode")).Text);
                    DropDownList ddlCatalogItemDetail = ((DropDownList)dgItem.FindControl("ddlCatalogItemDetail"));
                    clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_DETAIL_ID, ddlCatalogItemDetail.SelectedValue);
                    clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_DESC, ddlCatalogItemDetail.SelectedItem.Text);
                    clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_NAME, ddlCatalogItemDetail.SelectedItem.Text);
                    //Quantity
                    RequiredFieldValidator reqFldVal = ((RequiredFieldValidator)dgItem.FindControl("ReqFldVal_Quantity"));
                    CompareValidator compVal = ((CompareValidator)dgItem.FindControl("compVal_Quantity"));
                    CompareValidator compValZero = ((CompareValidator)dgItem.FindControl("compVal_QuantityZero"));
                    reqFldVal.Validate();
                    compVal.Validate();
                    compValZero.Validate();
                    if (reqFldVal.IsValid && compVal.IsValid && compValZero.IsValid)
                        clsUtil.UpdateRow(row, dataDef.FLD_QUANTITY, ((TextBox)dgItem.FindControl("txtQuantity")).Text);
                    else
                        clsUtil.UpdateRow(row, dataDef.FLD_QUANTITY, "0");

                    //Adjustment Quantity
                    compVal = ((CompareValidator)dgItem.FindControl("compVal_AdjQuantity"));
                    compValZero = ((CompareValidator)dgItem.FindControl("compVal_AdjQuantityZero"));
                    compVal.Validate();
                    compValZero.Validate();
                    if (compVal.IsValid && compValZero.IsValid)
                        clsUtil.UpdateRow(row, dataDef.FLD_ADJUSTMENT_QUANTITY, (-1 * Convert.ToInt32(((TextBox)dgItem.FindControl("txtAdjustmentQuantity")).Text)).ToString());
                    else
                        clsUtil.UpdateRow(row, dataDef.FLD_ADJUSTMENT_QUANTITY, "0");
                    //Product Price
                    if (IsPriceUpdatable) {
                        compVal = ((CompareValidator)dgItem.FindControl("compVal_Price"));
                        compValZero = ((CompareValidator)dgItem.FindControl("compVal_PriceZero"));
                        compVal.Validate();
                        compValZero.Validate();
                        if (compVal.IsValid && compValZero.IsValid)
                            clsUtil.UpdateRow(row, dataDef.FLD_PRICE, ((TextBox)dgItem.FindControl("txtPrice")).Text);
                        else
                            clsUtil.UpdateRow(row, dataDef.FLD_PRICE, "0");
                    }
                    if (dtsOrder.OrderHeader.Rows[0].RowState == DataRowState.Added)
                        row[dataDef.FLD_SHIPMENT_GROUP_ID] = 1;
                    RefreshProductInfo(row, ddlCatalogItemDetail);

                    if (row.RowState != DataRowState.Unchanged) {
                        if (row.RowState == DataRowState.Added) {
                            row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                        }
                        else {
                            clsUtil.UpdateRow(row, dataDef.FLD_UPDATE_USER_ID, Page.UserID.ToString());
                        }
                    }
                }
                //Operation Sucessful
                IsSuccess = true;
            }

            return IsSuccess;
        }

        private void dtgOrderDetail_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                RequiredFieldValidator reqVal = ((RequiredFieldValidator)e.Item.FindControl("RegFldVal_Price"));
                CompareValidator compVal = ((CompareValidator)e.Item.FindControl("compVal_Price"));
                CompareValidator compValZero = ((CompareValidator)e.Item.FindControl("compVal_PriceZero"));
                reqVal.Enabled = IsPriceUpdatable;
                compVal.Enabled = IsPriceUpdatable;
                compValZero.Enabled = IsPriceUpdatable;
            }
        }

        private void RefreshProductInfo(DataRow row, DropDownList ddl) {
            row[dataDef.FLD_CATALOG_ITEM_DETAIL_ID] = ddl.SelectedValue;
            row[dataDef.FLD_CATALOG_ITEM_DESC] = ddl.SelectedItem.Text;
            //Find it in the table to get more info
            int catalogItemDetailID = Convert.ToInt32(ddl.SelectedValue);
            DataView dv = new DataView(tblCatalogItemDetail);
            dv.Sort = CatalogItemDetailTable.FLD_PKID;
            int icatIndex = dv.Find(catalogItemDetailID);
            if (icatIndex > -1) {
                row[dataDef.FLD_CATALOG_ITEM_CODE] = dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE];
                if (dv[icatIndex][CatalogItemDetailTable.FLD_PRICE] != System.DBNull.Value) {
                    if (row.IsNull(dataDef.FLD_PRICE) || Convert.ToDecimal(row[dataDef.FLD_PRICE]) == 0)
                        row[dataDef.FLD_PRICE] = dv[icatIndex][CatalogItemDetailTable.FLD_PRICE];
                }
                else
                    row[dataDef.FLD_PRICE] = 0;

                if (dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS] != System.DBNull.Value)
                    row[dataDef.FLD_CATALOG_ITEM_NB_UNITS] = dv[icatIndex][CatalogItemDetailTable.FLD_PRICE];
            }
        }

        private void RefreshAllGridProductInfo() {
            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                DropDownList ddl = ((DropDownList)dgItem.FindControl("ddlCatalogItemDetail"));

                //Find it in the table to get more info
                int catalogItemDetailID = Convert.ToInt32(ddl.SelectedValue);
                DataView dv = new DataView(tblCatalogItemDetail);
                dv.Sort = CatalogItemDetailTable.FLD_PKID;
                int icatIndex = dv.Find(catalogItemDetailID);
                if (icatIndex > -1) {
                    Label lblCatalogItemCode = (Label)dgItem.FindControl("lblCatalogItemCode");
                    if (lblCatalogItemCode != null) {
                        lblCatalogItemCode.Text = dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_CODE].ToString();
                    }

                    Label lblPrice = (Label)dgItem.FindControl("lblPrice");
                    if (lblPrice != null) {
                        if (dv[icatIndex][CatalogItemDetailTable.FLD_PRICE] != System.DBNull.Value) {
                            if (lblPrice.Text.Length == 0 || lblPrice.Text == "0")
                                lblPrice.Text = dv[icatIndex][CatalogItemDetailTable.FLD_PRICE].ToString();
                        }
                        else {
                            lblPrice.Text = "0";
                        }
                    }

                    Label lblUnit = (Label)dgItem.FindControl("lblUnit");
                    if (lblUnit != null) {
                        if (dv[icatIndex][CatalogItemDetailTable.FLD_CATALOG_ITEM_NB_UNITS] != System.DBNull.Value)
                            lblUnit.Text = dv[icatIndex][CatalogItemDetailTable.FLD_PRICE].ToString();
                        else
                            lblUnit.Text = "0";
                    }
                }
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

        private void CustVal_DuplicateProduct_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) {
            bool IsValid = true;
            string sCatalogItemDetailID = args.Value;
            //Lookup in the DataGrid for duplicate entry
            if (sCatalogItemDetailID.Length > 0) {
                int CountOfProduct = 0;
                for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                    DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                    DropDownList ddlCatalogItemDetail = ((DropDownList)dgItem.FindControl("ddlCatalogItemDetail"));
                    if (ddlCatalogItemDetail != null) {
                        if ((ddlCatalogItemDetail.SelectedValue != "0") && (ddlCatalogItemDetail.SelectedValue.Length > 0)) {
                            if (ddlCatalogItemDetail.SelectedValue == sCatalogItemDetailID) {
                                CountOfProduct += 1;
                            }
                        }
                    }
                    if (CountOfProduct > 1) {
                        IsValid = false;
                        break;
                    }
                }
            }
            args.IsValid = IsValid;
        }

        private void CustVal_MinQty_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) {
            int CountOfQty = 0;
            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                //Quantity
                RequiredFieldValidator reqFldVal = ((RequiredFieldValidator)dgItem.FindControl("ReqFldVal_Quantity"));
                CompareValidator compVal = ((CompareValidator)dgItem.FindControl("compVal_Quantity"));
                CompareValidator compValZero = ((CompareValidator)dgItem.FindControl("compVal_QuantityZero"));
                TextBox txtQty = (TextBox)dgItem.FindControl("txtQuantity");
                reqFldVal.Validate();
                compVal.Validate();
                compValZero.Validate();
                if (reqFldVal.IsValid && compVal.IsValid && compValZero.IsValid) {
                    int qty = Convert.ToInt32(txtQty.Text);
                    CountOfQty += qty;
                }
            }

            args.IsValid = (CountOfQty >= MINIMUM_QTY);
        }

        /** New function added to optimize javascript **/
        private void FillDataGridInfo() {
            int qty = 0;
            decimal amount = 0;

            foreach (DataGridItem e in this.dtgOrderDetail.Items) {
                if ((e.ItemType == ListItemType.Item) || (e.ItemType == ListItemType.AlternatingItem)) {
                    string action = "OrderDetailRefresh(";//adj,qty,price,total //HandleChangeList();
                    action += "\"" + ((TextBox)e.FindControl("txtAdjustmentQuantity")).ClientID + "\",";
                    action += "\"" + ((TextBox)e.FindControl("txtQuantity")).ClientID + "\",";
                    action += "\"" + ((Label)e.FindControl("lblPrice")).ClientID + "\",";
                    action += "\"" + ((Label)e.FindControl("lblAdjAmount")).ClientID + "\");";
                    ((TextBox)e.FindControl("txtQuantity")).Attributes.Add("onKeyUp", action);
                    ((TextBox)e.FindControl("txtAdjustmentQuantity")).Attributes.Add("onKeyUp", action);

                    lstFieldAmount.Add(((Label)e.FindControl("lblAdjAmount")).ClientID);
                    lstFieldQty.Add(((TextBox)e.FindControl("txtQuantity")).ClientID);

                    //Modified to be able to adjust the total at postback
                    qty += Convert.ToInt32(((TextBox)e.FindControl("txtQuantity")).Text);
                    amount += Convert.ToDecimal(((Label)e.FindControl("lblAdjAmount")).Text.Replace("$", ""));

                    //update price when dropdownlist selectedindex change
                    string priceID = "";
                    string codeID = "";
                    string nbProductID = "";
                    string adjID = "";
                    string qtyID = "";
                    string totalAmountID = "";
                    string type = "";
                    if (IsPriceUpdatable) {
                        priceID = ((TextBox)e.FindControl("txtPrice")).ClientID;
                        type = "text";
                    }
                    else {
                        priceID = ((Label)e.FindControl("lblPrice")).ClientID;
                        type = "label";
                    }
                    codeID = ((Label)e.FindControl("lblCatalogItemCode")).ClientID;
                    nbProductID = ((Label)e.FindControl("lblUnit")).ClientID;
                    qtyID = ((TextBox)e.FindControl("txtQuantity")).ClientID;
                    adjID = ((TextBox)e.FindControl("txtAdjustmentQuantity")).ClientID;
                    totalAmountID = ((Label)e.FindControl("lblAdjAmount")).ClientID;
                    ((DropDownList)e.FindControl("ddlCatalogItemDetail")).Attributes["onchange"] = "updatePrice('" + type + "',this,'" + adjID + "','" + qtyID + "','" + priceID + "','" + totalAmountID + "','" + codeID + "','" + nbProductID + "');";
                }
                if (dtgFooter != null) {
                    lblQTYTotalID = ((Label)dtgFooter.FindControl("lblTotalQuantity")).ClientID;
                    lblAmountTotalID = ((Label)dtgFooter.FindControl("lblTotalAdjAmount")).ClientID;

                    //adjust value with viewstate
                    ((Label)dtgFooter.FindControl("lblTotalQuantity")).Text = qty.ToString();
                    ((Label)dtgFooter.FindControl("lblTotalAdjAmount")).Text = amount.ToString("C");
                }
            }
        }

        private void AddJavascript() {
            string script = "";
            script += "	<script> \n";
            script += " var AmountList = new Array (" + ArrayListToString(lstFieldAmount) + ");\n";
            script += " var QtyList = new Array (" + ArrayListToString(lstFieldQty) + ");\n";
            script += " var MinQtyList = new Array (" + ArrayListToString(lstMinFieldQty) + ");\n";
            script += " var lblAmountTotalID = \"" + this.lblAmountTotalID + "\";\n";
            script += " var lblQTYTotalID = \"" + this.lblQTYTotalID + "\";\n";
            script += "</script>\n";

            this.Page.RegisterClientScriptBlock("OrderDetailList_RefreshGrandTotal", script);
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