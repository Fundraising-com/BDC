using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;
using System.Collections.Generic;
using System.Configuration;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class OrderDetailList : BaseOrderDetailList//BaseWebUserControl
    {
        protected dataDef dTblOrderDetail = new dataDef();
        protected DataView dvOrderDetail = new DataView();
        private OrderData dtsOrder = new OrderData();
        private int c_ParentID;
        private int c_FormSectionTypeID = 0;
        private int c_FormSectionNumber = 0;
        protected CatalogItemDetailTable tblCatalogItemDetail = new CatalogItemDetailTable();
        protected System.Web.UI.WebControls.DropDownList ddlCatalogItemDetail;
        private const string TAX_RATE = "TaxRate";
        private decimal taxRate = 0;
        private int c_FormID = 0;
        private QSPForm.Business.OrderDetailSystem orderDetailSys = new QSPForm.Business.OrderDetailSystem();
        private ArrayList lstFieldAmount = new ArrayList();
        private ArrayList lstFieldQty = new ArrayList();
        private ArrayList lstMinQty = new ArrayList();
        private ArrayList lstFieldAdjQty = new ArrayList();
        private ArrayList lstFieldPrice = new ArrayList();

        private DataGridItem dtgFooter;
        private string lblAmountTotalID = "";
        private string lblQTYTotalID = "";
        private string lblMinQTYTotalID = "";
        private string sSectionCalculationFct = "";
        private decimal profitRate = 0;


        protected void Page_Load(object sender, System.EventArgs e) {
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
            this.CustVal_MinQty.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustVal_MinQty_ServerValidate);
            //this.dtgOrderDetail.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgOrderDetail_ItemDataBound);
            this.dtgOrderDetail.ItemCreated += new DataGridItemEventHandler(dtgOrderDetail_ItemCreated);
            //this.dtgOrderDetail.ItemDataBound += new DataGridItemEventHandler(dtgOrderDetail_ItemDataBound);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }

        #endregion

        private void SetBusinessMessage() {
            //CommonUtility clsUtil = new CommonUtility();
            //clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.OrderForm_Step4, c_FormID, c_FormSectionTypeID, c_FormSectionNumber);
            //trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
            //if (trBusinessMessage.Visible)
            //{
            //    lblBusinessMessage.Text = "<BR>" + lblBusinessMessage.Text + "<BR>";
            //}

        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
        }

        public override void BindForm() {
            try {
                //retreive data detail item for db

                //Init DataList
                FillFilter();

                //SetBusinessMessage();
                this.dtgOrderDetail.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetJScriptForGridCalculation();
            FillDataGridInfo();
            AddJavascript();
        }

        public decimal ProfitRate {
            get {
                if (ViewState["profitRate"] != null)
                    profitRate = Convert.ToDecimal(ViewState["profitRate"].ToString());
                return profitRate;
            }
            set {
                profitRate = value;
                ViewState["profitRate"] = value;
            }
        }

        public override int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public string SectionTitle {
            get {
                return lblSectionTitle.Text;
            }
            set {
                lblSectionTitle.Text = value;
            }
        }

        public string SectionBusinessMessage {
            get {
                return lblBusinessMessage.Text;
            }
            set {
                lblBusinessMessage.Text = value;
            }
        }

        public int FormSectionTypeID {
            get {
                if (this.ViewState["FormSectionTypeID"] != null)
                    c_FormSectionTypeID = Convert.ToInt32(this.ViewState["FormSectionTypeID"].ToString());
                return c_FormSectionTypeID;
            }
            set {
                this.ViewState["FormSectionTypeID"] = value;
                c_FormSectionTypeID = value;
            }
        }

        public int FormSectionNumber {
            get {
                if (this.ViewState["FormSectionNumber"] != null)
                    c_FormSectionNumber = Convert.ToInt32(this.ViewState["FormSectionNumber"].ToString());
                return c_FormSectionNumber;
            }
            set {
                this.ViewState["FormSectionNumber"] = value;
                c_FormSectionNumber = value;
            }
        }

        public override int FormID {
            get {
                if (this.ViewState["FormID"] != null)
                    c_FormID = Convert.ToInt32(this.ViewState["FormID"].ToString());
                return c_FormID;
            }
            set {
                this.ViewState["FormID"] = value;
                c_FormID = value;
            }
        }

        public int MinimumCase {
            get {
                int mCase = 0;
                if (this.ViewState["MinCase"] != null)
                    mCase = Convert.ToInt32(this.ViewState["MinCase"].ToString());
                return mCase;
            }
        }

        private int MinCase {
            get {
                int mCase = 0;
                if (this.ViewState["MinCase"] != null)
                    mCase = Convert.ToInt32(this.ViewState["MinCase"].ToString());
                return mCase;
            }
            set {
                this.ViewState["MinCase"] = value;
            }
        }

        public override OrderData DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
                dTblOrderDetail = dtsOrder.OrderDetail;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    this.FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            }
        }

        private void BindGrid() {
            dvOrderDetail.Table = dTblOrderDetail;
            //dvOrderDetail.Sort = dataDef.FLD_DISPLAY_ORDER;
            string sFilter = "";
            if (FormSectionTypeID > 0) {
                sFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();
                if (FormSectionNumber > 1) {
                    sFilter = sFilter + " AND " + OrderDetailTable.FLD_FORM_SECTION_NUMBER + " = " + c_FormSectionNumber.ToString();
                }
                else {
                    sFilter = sFilter + " AND ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_NUMBER + ",0) <= 1";
                }
            }

            // Commented by Renuka Sept 12,2007 
            if (Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE]) != 0 || this.ProfitRate != 0) {
                if (ProfitRate == 0)
                    this.ProfitRate = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE]);
                sFilter += " AND " + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = " + this.ProfitRate.ToString();
            }
            else {
                sFilter += " AND (" + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = 0";
                sFilter += " OR " + OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + " = 0.5)";
            }

            dvOrderDetail.RowFilter = sFilter;
            dvOrderDetail.Sort = dataDef.FLD_DISPLAY_ORDER;

            this.dtgOrderDetail.DataBind();
        }

        public string LabelTotalQuantityClientID {
            get {
                string lblTotalQtyID = "";
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
        
        public string hdnMinTotalQuantityClientID
        {
            get
            {
                string hdnTotalQtyID = "";
                if (dtgFooter != null)
                {
                    HiddenField hdnTotal = ((HiddenField)dtgFooter.FindControl("hdnMinTotalQuantity"));
                    if (hdnTotal != null)
                    {
                        hdnTotalQtyID = hdnTotal.ClientID; ;
                    }
                }
                return hdnTotalQtyID;
            }
        }

        public string LabelTotalAmountClientID {
            get {
                string lblTotalAmtID = "";
                if (dtgFooter != null) {
                    Label lblTotal = ((Label)dtgFooter.FindControl("lblTotalAmount"));
                    if (lblTotal != null) {
                        //lblTotal.Text = qty.ToString();
                        lblTotalAmtID = lblTotal.ClientID; ;
                    }
                }
                return lblTotalAmtID;
            }
        }

        public bool DisableQtyValidator {
            get {
                bool d = false;
                if (this.ViewState["DisableQtyValidator"] != null)
                    d = Convert.ToBoolean(this.ViewState["DisableQtyValidator"].ToString());
                return d;
            }
            set {
                this.ViewState["DisableQtyValidator"] = value;
            }
        }

        public override bool UpdateDataSource() {
            bool IsSuccess = false;
            CommonUtility clsUtil = new CommonUtility();
            // get edited row values in grid
            DataView dv = new DataView(dTblOrderDetail);
            dv.Sort = dataDef.FLD_PKID;

            for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                int iIndex = -1;

                int OrderDetailID = Convert.ToInt32(dtgOrderDetail.DataKeys[iCount]);
                DataGridItem dgItem = dtgOrderDetail.Items[iCount];

                iIndex = dv.Find(OrderDetailID);

                if (iIndex != -1) {
                    DataRow row = dv[iIndex].Row;
                    TextBox txtQuantity = ((TextBox)dgItem.FindControl("txtQuantity"));
                    TextBox txtAdjustmentQuantity = ((TextBox)dgItem.FindControl("txtAdjustmentQuantity"));

                    clsUtil.UpdateRow(row, dataDef.FLD_ORDER_ID, c_ParentID.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_CODE, ((Label)dgItem.FindControl("lblCatalogItemCode")).Text);
                    //Quantity
                    CompareValidator compVal = ((CompareValidator)dgItem.FindControl("compVal_Quantity"));
                    compVal.Validate();
                    if (compVal.IsValid) {
                        if (txtQuantity.Text.Trim().Length > 0)
                            clsUtil.UpdateRow(row, dataDef.FLD_QUANTITY, ((TextBox)dgItem.FindControl("txtQuantity")).Text);
                        else
                            clsUtil.UpdateRow(row, dataDef.FLD_QUANTITY, "0");
                    }
                    else {
                        clsUtil.UpdateRow(row, dataDef.FLD_QUANTITY, "0");
                    }

                    //Adjustment Quantity
                    compVal = ((CompareValidator)dgItem.FindControl("compVal_AdjQuantity"));
                    compVal.Validate();
                    if (compVal.IsValid) {
                        if (txtAdjustmentQuantity.Text.Trim().Length > 0)
                            clsUtil.UpdateRow(row, dataDef.FLD_ADJUSTMENT_QUANTITY, (-1 * Convert.ToInt32(txtAdjustmentQuantity.Text)).ToString());
                        else
                            clsUtil.UpdateRow(row, dataDef.FLD_ADJUSTMENT_QUANTITY, "0");
                    }
                    else
                        clsUtil.UpdateRow(row, dataDef.FLD_ADJUSTMENT_QUANTITY, "0");

                    //Fill when Shipmentgroup is missing
                    if (dtsOrder.OrderHeader.Rows[0].RowState == DataRowState.Added) {
                        if (dtsOrder.ShipmentGroup.Rows.Count > 0) {
                            row[dataDef.FLD_SHIPMENT_GROUP_ID] = dtsOrder.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID];
                        }
                        else {
                            row[dataDef.FLD_SHIPMENT_GROUP_ID] = -1;
                        }
                    }

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

        public void SetJScriptForGridCalculation() {
            string sPrefix = this.ClientID + this.ClientIDSeparator;
            string sParentPrefix = "";
            if (c_FormSectionTypeID == QSPForm.Common.FormSectionType.STANDARD_PRODUCT)
                sParentPrefix = "StandardSection_";
            else if (c_FormSectionTypeID == QSPForm.Common.FormSectionType.OTHER_PRODUCT)
                sParentPrefix = "OtherSection_";
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<SCRIPT language=javascript>                                               \n");
            sJScript.Append("	                                                                        \n");
            sJScript.Append("	function " + sPrefix + "SetQuantity(adjID,qtyID,priceID,totalID)                       \n");
            sJScript.Append("	{                                                                       \n");
            sJScript.Append("	    var qty = ValidateFieldValue(document.getElementById(qtyID).value);     \n");
            sJScript.Append("	    var price = document.getElementById(priceID).innerHTML.replace('$',''); \n");
            sJScript.Append("	    var adj = 0;                                                            \n");
            sJScript.Append("	    if (document.getElementById(adjID) != null)                             \n");
            sJScript.Append("	        adj = ValidateFieldValue(document.getElementById(adjID).value);     \n");
            sJScript.Append("	    var total = (qty - adj)*price;                                          \n");
            sJScript.Append("	    if(total < 0 ){total = 0.00}                                            \n");
            sJScript.Append("	    if((qty == 0)&&(adj > 0))                                               \n");
            sJScript.Append("	    {                                                                       \n");
            sJScript.Append("		//document.getElementById(qtyID).value = 0;                         \n");
            sJScript.Append("		document.getElementById(totalID).innerHTML = formatCurrency(total); \n");
            sJScript.Append("	    }                                                                       \n");
            sJScript.Append("	}                                                                       \n");
            sJScript.Append("   function " + sPrefix + "OrderDetailRefresh(adjID,qtyID,priceID,totalID) \n");
            sJScript.Append("   { \n");
            sJScript.Append("       " + sPrefix + "ODL_RefreshSubTotal(adjID,qtyID,priceID,totalID); \n");
            sJScript.Append("       " + sPrefix + "ODL_RefreshQuantityTotal(); \n");
            sJScript.Append("       " + sPrefix + "ODL_RefreshMinQuantityTotal(); \n");
            sJScript.Append("       " + sPrefix + "ODL_RefreshGrandTotal(); \n");
            sJScript.Append("       if (" + sParentPrefix + "RefreshSectionGrandTotal != null) \n");
            sJScript.Append("           " + sParentPrefix + "RefreshSectionGrandTotal(); \n");
            sJScript.Append("   } \n");

            sJScript.Append("   function " + sPrefix + "ODL_RefreshSubTotal(adjID,qtyID,priceID,totalID)\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var qty = ValidateFieldValue(document.getElementById(qtyID).value);\n");
            sJScript.Append("       var price = document.getElementById(priceID).innerHTML.replace('$','');\n");
            sJScript.Append("       var adj = 0;\n");
            sJScript.Append("       if (document.getElementById(adjID) != null)\n");
            sJScript.Append("           adj = ValidateFieldValue(document.getElementById(adjID).value);\n");

            sJScript.Append("       var total = (qty - adj)*price;\n");
            sJScript.Append("       if(total < 0 ){total = 0.00}\n");

            sJScript.Append("       //If the sum is 0 do not display anything\n");
            sJScript.Append("       if((qty == 0)&&(adj ==0))\n");
            sJScript.Append("       {\n");
            sJScript.Append("       	document.getElementById(totalID).innerHTML = '';\n");
            sJScript.Append("       	//document.getElementById(qtyID).value = '';\n");
            sJScript.Append("       	//document.getElementById(adjID).value = '';\n");
            sJScript.Append("       }\n");
            sJScript.Append("       else\n");
            sJScript.Append("       {\n");
            sJScript.Append("        	document.getElementById(totalID).innerHTML = formatCurrency(total);\n");
            sJScript.Append("       }\n");
            sJScript.Append("   } \n");
            //New
            sJScript.Append("   function " + sPrefix + "RefreshAll()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       for(var iIndex = 0; iIndex < " + sPrefix + "AmountList.length; iIndex++)\n");
            sJScript.Append("       {\n");
            sJScript.Append("           var qtyID = " + sPrefix + "QtyList[iIndex];\n");
            sJScript.Append("           var qtyID2 = " + sPrefix + "MinQtyList[iIndex];\n");
            sJScript.Append("           var priceID = " + sPrefix + "PriceList[iIndex];\n");
            sJScript.Append("           var adjID = " + sPrefix + "AdjQtyList[iIndex];\n");
            sJScript.Append("           var totalID = " + sPrefix + "AmountList[iIndex];\n");
            sJScript.Append("           " + sPrefix + "ODL_RefreshSubTotal(adjID,qtyID,priceID,totalID); \n");
            sJScript.Append("       }\n");
            sJScript.Append("           " + sPrefix + "ODL_RefreshQuantityTotal(); \n");
            sJScript.Append("           " + sPrefix + "ODL_RefreshMinQuantityTotal(); \n");
            sJScript.Append("           " + sPrefix + "ODL_RefreshGrandTotal(); \n");
            sJScript.Append("           if (" + sParentPrefix + "RefreshSectionGrandTotal != null) \n");
            sJScript.Append("               " + sParentPrefix + "RefreshSectionGrandTotal(); \n");

            sJScript.Append("   } \n");

            sJScript.Append("   function " + sPrefix + "ODL_RefreshGrandTotal()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0.00;\n");
            sJScript.Append("       var sTotal;\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "AmountList.length; x++)\n");
            sJScript.Append("       {\n");
            sJScript.Append("       	sTotal = document.getElementById(" + sPrefix + "AmountList[x]).innerHTML.replace('$','');\n");
            sJScript.Append("       	sTotal = ValidateFieldValue(sTotal);\n");
            sJScript.Append("       	cptList += parseFloat(sTotal.replace(/,/g,''));	\n");
            sJScript.Append("       }\n");
            sJScript.Append("       document.getElementById(" + sPrefix + "lblAmountTotalID).innerHTML = formatCurrency(cptList);\n");
            sJScript.Append("   }\n");

            sJScript.Append("   function " + sPrefix + "ODL_RefreshQuantityTotal()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0;		\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "QtyList.length; x++)\n");
            sJScript.Append("       {\n");
            sJScript.Append("       	if(!isNaN(parseInt(document.getElementById(" + sPrefix + "QtyList[x]).value)))\n");
            sJScript.Append("    	cptList += parseInt(document.getElementById(" + sPrefix + "QtyList[x]).value);\n");
            sJScript.Append("       }\n");
            sJScript.Append("       document.getElementById(" + sPrefix + "lblQTYTotalID).innerHTML = cptList;\n");
            sJScript.Append("   }\n");
            
            sJScript.Append("   function " + sPrefix + "ODL_RefreshMinQuantityTotal()\n");
            sJScript.Append("   {\n");
            sJScript.Append("       var cptList = 0;		\n");
            sJScript.Append("       for(var x = 0; x < " + sPrefix + "MinQtyList.length; x++)\n");
            sJScript.Append("       {\n");
            sJScript.Append("       	if(!isNaN(parseInt(document.getElementById(" + sPrefix + "MinQtyList[x]).value)))\n");
            sJScript.Append("    	cptList += parseInt(document.getElementById(" + sPrefix + "MinQtyList[x]).value);\n");
            sJScript.Append("       }\n");
            sJScript.Append("       document.getElementById(" + sPrefix + "lblMinQTYTotalID).value = cptList;\n");
            sJScript.Append("   }\n");

            sJScript.Append("   " + sPrefix + "RefreshAll(); \n");

            sJScript.Append("</SCRIPT>");

            // Register Client Script
            this.Page.RegisterStartupScript(sPrefix + "SetCalculationGrid", sJScript.ToString());
        }

        private void AddJavascript() {
            string sPrefix = this.ClientID + this.ClientIDSeparator;
            string script = "";
            script += "	<script> \n";
            script += " var " + sPrefix + "AmountList = new Array (" + ArrayListToString(lstFieldAmount) + ");\n";
            script += " var " + sPrefix + "QtyList = new Array (" + ArrayListToString(lstFieldQty) + ");\n";
            script += " var " + sPrefix + "MinQtyList = new Array (" + ArrayListToString(lstMinQty) + ");\n";
            script += " var " + sPrefix + "AdjQtyList = new Array (" + ArrayListToString(lstFieldAdjQty) + ");\n";
            script += " var " + sPrefix + "PriceList = new Array (" + ArrayListToString(lstFieldPrice) + ");\n";
            script += " var " + sPrefix + "lblAmountTotalID = \"" + this.lblAmountTotalID + "\";\n";
            script += " var " + sPrefix + "lblQTYTotalID = \"" + this.lblQTYTotalID + "\";\n";
            script += " var " + sPrefix + "lblMinQTYTotalID = \"" + this.lblMinQTYTotalID + "\";\n";
            script += "</script>\n";

            this.Page.RegisterClientScriptBlock(sPrefix + "OrderDetailList_RefreshGrandTotal", script);
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

        public bool HasSelectedItem {
            get {
                TextBox txtQuantity;
                bool hasSelectedItem = false;
                foreach (DataGridItem e in this.dtgOrderDetail.Items) {
                    txtQuantity = ((TextBox)e.FindControl("txtQuantity"));
                    if (txtQuantity.Text != "") {
                        hasSelectedItem = true;
                        break;
                    }
                }
                return hasSelectedItem;
            }
        }

        public override bool ValidateForm() {
            bool blnValid = false;
            SetValidatorControl();
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

        private void SetValidatorControl() {
            foreach (DataGridItem dgItem in this.dtgOrderDetail.Items) {
                if ((dgItem.ItemType == ListItemType.Item) || (dgItem.ItemType == ListItemType.AlternatingItem)) {
                    CompareValidator compVal_AdjQuantity_Quantity = ((CompareValidator)dgItem.FindControl("compVal_AdjQuantity_Quantity"));
                    //Quantity
                    int qty = 0;
                    TextBox txtQuantity = ((TextBox)dgItem.FindControl("txtQuantity"));
                    TextBox txtAdjustmentQuantity = ((TextBox)dgItem.FindControl("txtAdjustmentQuantity"));
                    if (txtAdjustmentQuantity.Text.Trim().Length > 0) {
                        CompareValidator compVal = ((CompareValidator)dgItem.FindControl("compVal_Quantity"));
                        compVal.Validate();
                        if (compVal.IsValid) {
                            if (txtQuantity.Text.Trim().Length > 0)
                                qty = Convert.ToInt32(txtQuantity.Text.Trim());
                        }
                        compVal_AdjQuantity_Quantity.ValueToCompare = qty.ToString();
                        compVal_AdjQuantity_Quantity.Enabled = true;
                    }
                    else
                        compVal_AdjQuantity_Quantity.Enabled = false;
                }
            }
        }

        private void CustVal_MinQty_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) {
            int minTotalQty = 0;
            int CountOfQty = 0;
            QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
            minTotalQty = bizSys.GetMinTotalQuantity(FormID, FormSectionTypeID, FormSectionNumber);


            //this.MinCase = minTotalQty;
            //Personalized the error message
            string tagToreplace = "[MinTotalQuantity]";
            CustomValidator custVal = (CustomValidator)source;
            custVal.ErrorMessage = custVal.ErrorMessage.Replace(tagToreplace, minTotalQty.ToString());

            if (minTotalQty > 0) {
                for (int iCount = 0; iCount < dtgOrderDetail.Items.Count; iCount++) {
                    DataGridItem dgItem = dtgOrderDetail.Items[iCount];
                    //Quantity
                    CompareValidator compVal = ((CompareValidator)dgItem.FindControl("compVal_Quantity"));
                    TextBox txtQty = (TextBox)dgItem.FindControl("txtQuantity");
                    compVal.Validate();
                    if (compVal.IsValid) {
                        if (txtQty.Text.Trim().Length > 0) {
                            int qty = Convert.ToInt32(txtQty.Text);
                            CountOfQty += qty;
                        }
                    }
                }
                args.IsValid = (CountOfQty >= minTotalQty);
            }
            else
                args.IsValid = true;
        }

        private void FillDataGridInfo() {
            int qty = 0;
            int totalQty = 0;
            decimal amount = 0;
            decimal totalAmount = 0;
            bool IsQtyAdjustmentAllowed = true;
            if (this.FormID > 0) {
                QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
                FormTable dTblForm = frmSys.SelectOne(this.FormID);
                if (dTblForm.Rows.Count > 0) {
                    DataRow row = dTblForm.Rows[0];
                    if (!row.IsNull(FormTable.FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED)) {
                        IsQtyAdjustmentAllowed = Convert.ToBoolean(row[FormTable.FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED]);
                    }
                }
            }
            QSPForm.Business.BusinessRuleSystem ruleSys = new QSPForm.Business.BusinessRuleSystem();
            int minLineItemTotal = ruleSys.GetMinLineItemQuantity(this.FormID, FormSectionTypeID);
            int minSectionLineItemTotal = ruleSys.GetMinLineItemQuantity(this.FormID, FormSectionTypeID, c_FormSectionNumber);
            if (minSectionLineItemTotal > 0)
                minLineItemTotal = minSectionLineItemTotal;
            this.MinCase = minLineItemTotal;

            int formId = 0;
            List<int> cdMinCountFormId = new List<int>();
            QSPForm.Business.ProductSystem prodSys = new QSPForm.Business.ProductSystem();
            //check for product type cookies, poduct_type_id = 6
            QSPForm.Common.DataDef.ProductTable products = prodSys.SelectAllByProductType(6);
            //if the datasource is null or doesn't have Order table.
            try
            {
                //order form ID
                formId = Convert.ToInt32(this.DataSource.Tables["Order"].Rows[0]["form_id"].ToString());
            }
            catch
            {
                //do nothing, formId will be 0
            }

            try
            {
                //cd min count order form id
                foreach (string strFormId in ConfigurationManager.AppSettings["CD_MinCount_form"].ToString().Split(new char[] { ',' }))
                {
                    cdMinCountFormId.Add(Convert.ToInt32(strFormId.Trim()));
                }
            }
            catch
            {

            }

            foreach (DataGridItem e in this.dtgOrderDetail.Items) {
                if ((e.ItemType == ListItemType.Item) || (e.ItemType == ListItemType.AlternatingItem)) {
                    int OrderDetailID = Convert.ToInt32(dtgOrderDetail.DataKeys[e.ItemIndex]);
                    TextBox txtQuantity = ((TextBox)e.FindControl("txtQuantity"));
                    TextBox txtAdjustmentQuantity = ((TextBox)e.FindControl("txtAdjustmentQuantity"));
                    Label lblAdjustmentQuantity = ((Label)e.FindControl("lblAdjustmentQuantity"));
                    Label lblPrice = ((Label)e.FindControl("lblPrice"));
                    Label lblAmount = ((Label)e.FindControl("lblAmount"));

                    string action = this.ClientID + this.ClientIDSeparator +
                        "OrderDetailRefresh(";//adj,qty,price,total //HandleChangeList();
                    action += "'" + txtAdjustmentQuantity.ClientID + "',";
                    action += "'" + txtQuantity.ClientID + "',";
                    action += "'" + lblPrice.ClientID + "',";
                    action += "'" + lblAmount.ClientID + "');";
                    string action2 = action.Replace("OrderDetailRefresh", "SetQuantity");
                    txtQuantity.Attributes.Add("onKeyUp", action);
                    txtQuantity.Attributes.Add("onblur", action2);

                    txtAdjustmentQuantity.Attributes.Add("onKeyUp", action);
                    txtAdjustmentQuantity.Attributes.Add("onblur", action2);

                    lstFieldAmount.Add(lblAmount.ClientID);
                    lstFieldQty.Add(txtQuantity.ClientID);

                    if (cdMinCountFormId.Contains(formId))
                    {

                        string productCode = ((Label)e.FindControl("lblCatalogItemCode")).Text.Trim();
                        if (products.Select(ProductTable.FLD_CODE + "=" + productCode).Length > 0)
                            lstMinQty.Add(txtQuantity.ClientID);
                    }
                    else
                        lstMinQty.Add(txtQuantity.ClientID);

                    lstFieldAdjQty.Add(txtAdjustmentQuantity.ClientID);
                    lstFieldPrice.Add(lblPrice.ClientID);

                    //Modified to be able to adjust the total at postback
                    //Quantity
                    qty = 0;
                    CompareValidator compVal = ((CompareValidator)e.FindControl("compVal_Quantity"));
                    compVal.Validate();
                    if (compVal.IsValid) {
                        if (txtQuantity.Text.Trim().Length > 0)
                            qty = Convert.ToInt32(txtQuantity.Text);
                    }
                    totalQty += qty;

                    //Adjustment Quantity
                    int adjQty = 0;
                    compVal = ((CompareValidator)e.FindControl("compVal_AdjQuantity"));
                    compVal.Validate();
                    if (compVal.IsValid) {
                        if (txtAdjustmentQuantity.Text.Trim().Length > 0)
                            adjQty = Convert.ToInt32(txtAdjustmentQuantity.Text);
                    }

                    //Price and Amount
                    DataView dv = new DataView(dTblOrderDetail);
                    dv.Sort = dataDef.FLD_PKID;
                    int iIndex = -1;
                    iIndex = dv.Find(OrderDetailID);
                    decimal price = 0;
                    if (iIndex > -1) {
                        DataRow row = dv[iIndex].Row;
                        if (!row.IsNull(OrderDetailTable.FLD_PRICE)) {
                            price = Convert.ToDecimal(row[OrderDetailTable.FLD_PRICE]);
                        }
                    }

                    amount = (qty - adjQty) * price;
                    //Set the Line Total
                    if (txtQuantity.Text != "" || txtAdjustmentQuantity.Text != "") {
                        if (qty >= adjQty) //We don't display negative total
                            lblAmount.Text = amount.ToString("C");
                        else
                            lblAmount.Text = (0).ToString("C");
                    }
                    else
                        lblAmount.Text = "";
                    //Summarization for the Total Amount
                    totalAmount += amount;
                    lblAdjustmentQuantity.Visible = !IsQtyAdjustmentAllowed;
                    txtAdjustmentQuantity.Visible = !lblAdjustmentQuantity.Visible;

                    //					//Hide the 0 
                    //					if( ( VerifyFormatField((TextBox)e.FindControl("txtAdjustmentQuantity")) == "0" ) &&
                    //						( qty == 0))
                    //					{
                    //						((TextBox)e.FindControl("txtAdjustmentQuantity")).Text = "";
                    //						((TextBox)e.FindControl("txtQuantity")).Text = "";
                    //						((Label)e.FindControl("lblAdjAmount")).Text = "";
                    //					}
                    //					else if ( (Convert.ToInt32(VerifyFormatField((TextBox)e.FindControl("txtAdjustmentQuantity"))) > 0 ) &&
                    //						( VerifyFormatField((TextBox)e.FindControl("txtQuantity")) == "0"))
                    //					{
                    //							((TextBox)e.FindControl("txtQuantity")).Text = "0";
                    //					}
                    //

                    CompareValidator compVal_MinLineItemQuantity = ((CompareValidator)e.FindControl("compVal_MinLineItemQuantity"));
                    if (minLineItemTotal > 0 && !this.DisableQtyValidator) {
                        compVal_MinLineItemQuantity.Enabled = true;
                        string msg = compVal_MinLineItemQuantity.ErrorMessage.Replace("[value]", minLineItemTotal.ToString());
                        compVal_MinLineItemQuantity.ValueToCompare = minLineItemTotal.ToString();
                        compVal_MinLineItemQuantity.ErrorMessage = msg;
                    }
                    else {
                        compVal_MinLineItemQuantity.Enabled = false;
                    }
                }
            }

            if (dtgFooter != null) {
                Label lblQTYTotal = ((Label)dtgFooter.FindControl("lblTotalQuantity"));
                HiddenField lblMinQTYTotal = ((HiddenField)dtgFooter.FindControl("hdnMinTotalQuantity"));
                Label lblAmountTotal = ((Label)dtgFooter.FindControl("lblTotalAmount"));
                lblQTYTotalID = lblQTYTotal.ClientID;
                lblMinQTYTotalID = lblMinQTYTotal.ClientID;
                lblAmountTotalID = lblAmountTotal.ClientID;

                //adjust value with viewstate
                lblQTYTotal.Text = totalQty.ToString();//qty.ToString();
                lblAmountTotal.Text = totalAmount.ToString("C");//amount.ToString("C");			
            }
        }

        private void dtgOrderDetail_ItemCreated(object sender, DataGridItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Footer) {
                dtgFooter = e.Item; // used for FillDataGridInfo coz at this point, clientID is invalide
            }
        }

        private void FillFilter() {
        }

        public void SetQtyValidator() {
            QSPForm.Business.BusinessRuleSystem ruleSys = new QSPForm.Business.BusinessRuleSystem();
            int minLineItemTotal = 0;
            if (this.MinCase != 0)
                minLineItemTotal = this.MinCase;
            else
                minLineItemTotal = ruleSys.GetMinLineItemQuantity(this.FormID, this.FormSectionTypeID, this.FormSectionNumber);

            CompareValidator compVal_MinLineItemQuantity;

            foreach (DataGridItem e in this.dtgOrderDetail.Items) {
                compVal_MinLineItemQuantity = ((CompareValidator)e.FindControl("compVal_MinLineItemQuantity"));
                if (minLineItemTotal > 0 && !this.DisableQtyValidator) {
                    compVal_MinLineItemQuantity.Enabled = true;
                    string msg = compVal_MinLineItemQuantity.ErrorMessage.Replace("[value]", minLineItemTotal.ToString());
                    compVal_MinLineItemQuantity.ValueToCompare = minLineItemTotal.ToString();
                    compVal_MinLineItemQuantity.ErrorMessage = msg;
                }
                else {
                    compVal_MinLineItemQuantity.Enabled = false;
                }
            }
        }

        //		private string VerifyFormatField(Label lbl)
        //		{
        //			string text = lbl.Text;
        //			if(text.Trim() == "")
        //			{
        //				text = "0.00";
        //			}
        //			return text;
        //		}
    }
}