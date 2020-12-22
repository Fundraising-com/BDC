using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.ProgramAgreementData;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class ProgramAgreementSupplyForm : BaseWebUserControl {
        private CommonUtility util = new CommonUtility();
        private ProgramAgreementData dtsProgramAgreement;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_FormID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here	
            this.SupplyList.FormID = c_FormID;

            #region Reload dtsProgramAgreement.OrderSupply OrderDetailTable

            int catalogId = 0;
            int catalogItemCategoryId = 0;
            int formSectionNumber = 0;
            int formId = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_FORM_ID]);
            bool priced = Convert.ToBoolean(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PRICED]);

            // Clear supply rows
            dtsProgramAgreement.OrderSupply.Rows.Clear();

            // Look for the correct catalog
            List<QSP.Business.Fulfillment.FormSection> formSectionList = QSP.Business.Fulfillment.FormSection.GetFormSectionListByFormIdAndFormSectionTypeId(formId, 2);
            foreach (QSP.Business.Fulfillment.FormSection formSection in formSectionList)
            {
                QSP.Business.Fulfillment.CatalogItemCategory catalogItemCategory = QSP.Business.Fulfillment.CatalogItemCategory.GetCatalogItemCategory(formSection.CatalogItemCategoryId);
                QSP.Business.Fulfillment.Catalog catalog = QSP.Business.Fulfillment.Catalog.GetCatalog(catalogItemCategory.CatalogId);

                if (catalog.IsPriced == priced)
                {
                    catalogId = catalog.CatalogId;
                    catalogItemCategoryId = formSection.CatalogItemCategoryId;
                    formSectionNumber = (formSection.FormSectionNumber == null) ? 0 : Convert.ToInt32(formSection.FormSectionNumber);
                    break;
                }
            }

            // Get products from catalog
            List<QSP.Business.Fulfillment.CatalogItem> catalogItemList = QSP.Business.Fulfillment.CatalogItem.GetCatalogItemListByCatalogId(catalogId);
            foreach (QSP.Business.Fulfillment.CatalogItem catalogItem in catalogItemList)
            {
                if (catalogItem.CatalogItemStatusId == 101)
                {
                    QSP.Business.Fulfillment.CatalogItemCategoryCatalogItem cicci = QSP.Business.Fulfillment.CatalogItemCategoryCatalogItem.GetCatalogItemCategoryCatalogItemByCatalogItemCategoryIdAndCatalogItemId(catalogItemCategoryId, catalogItem.CatalogItemId);
                    QSP.Business.Fulfillment.CatalogItemDetail catalogItemDetail = QSP.Business.Fulfillment.CatalogItemDetail.GetCatalogItemDetailByCatalogItemIdAndProfitRate(catalogItem.CatalogItemId, 0.0);

                    DataRow newRow = dtsProgramAgreement.OrderSupply.NewRow();

                    newRow.SetField(OrderDetailTable.FLD_PKID, catalogItem.CatalogItemId);
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_CODE, catalogItem.CatalogItemCode);
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_NAME, catalogItem.CatalogItemName);
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_DESC, catalogItem.CatalogItemName + " (" + catalogItem.CatalogItemCode + ")");
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_ID, catalogItemDetail.CatalogItemDetailId);
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PRICE, catalogItemDetail.Price);
                    newRow.SetField(OrderDetailTable.FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE, catalogItemDetail.ProfitRate);
                    newRow.SetField(OrderDetailTable.FLD_PRICE, catalogItem.Price);
                    newRow.SetField(OrderDetailTable.FLD_FORM_SECTION_TYPE_ID, 2);
                    newRow.SetField(OrderDetailTable.FLD_FORM_SECTION_NUMBER, formSectionNumber);
                    newRow.SetField(OrderDetailTable.FLD_DISPLAY_ORDER, cicci.DisplayOrder);
                    newRow.SetField(OrderDetailTable.FLD_DELETED, catalogItem.Deleted ? 1 : 0);

                    dtsProgramAgreement.OrderSupply.Rows.Add(newRow);
                }
            }

            #endregion
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        private void InitControl() {
        }

        private void SetBusinessMessage() {

            clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.ProgramAgreementForm_Step4, c_FormID);
            trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            tblAddressSupply.Visible = (radBtnLstShipTo.SelectedValue != "2");

            //Find the Supply MinDayLeadTime attached to the form
            //			int DeliveryMethodID = 1;
            //			DeliveryMethodID = Convert.ToInt32(dtsOrder.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);				
            BusinessRuleSystem ruleSys = new BusinessRuleSystem();
            int MinDayLeadTime = ruleSys.GetMinNbDayLeadTime_Supply(c_FormID);
            clsUtil.SetJScriptForOpenCalendar(hypLnkDeliveryDate, txtDeliveryDate, lblDayLeadTime, true, lblOrderDate, null, MinDayLeadTime);

            Refresh_NbDayLeadTime();
        }

        public dataRef DataSource {
            get {
                return dtsProgramAgreement;
            }
            set {
                dtsProgramAgreement = value;
                if (dtsProgramAgreement != null && dtsProgramAgreement.ProgramAgreement.Rows.Count > 0) {
                    SupplyList.DataSource = dtsProgramAgreement.OrderSupply;
                    c_FormID = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_FORM_ID]);
                    SupplyList.FormID = c_FormID;
                }
            }
        }

        public string LabelTotalQuantityClientID {
            get {
                return SupplyList.LabelTotalQuantityClientID;
            }
        }

        public void BindForm() {
            DateTime orderDate = DateTime.Today.AddDays(7);
            DateTime ShipTo_DeliveryDate;

            DataRow shipRow = dtsProgramAgreement.ShipmentGroup.Rows[0];
            DataRow ordRow = dtsProgramAgreement.OrderHeader.Rows[0];

            if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_DATE)) {
                orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
            }

            lblOrderDate.Text = orderDate.ToShortDateString() + " " + orderDate.ToShortTimeString(); ;

            if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_ID)) {
                int ShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_PKID]);
                int ShipTo = 0;

                if (shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] != DBNull.Value) {
                    ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);
                    radBtnLstShipTo.SelectedIndex = (ShipTo - 1);

                    if (ShipTo != 2) {
                        AddressSupply.Visible = true;
                        AddressSupply.ParentID = ShipGrpID;
                        AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                        AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                        AddressSupply.DataSource = dtsProgramAgreement;
                        AddressSupply.BindForm();
                    }
                    else {
                        AddressSupply.Visible = false;
                    }
                }
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE)) {
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);
                    txtDeliveryDate.Text = ShipTo_DeliveryDate.ToShortDateString();
                    Refresh_NbDayLeadTime();
                }
            }

            SupplyList.DataSource = dtsProgramAgreement.OrderSupply;
            SupplyList.BindForm();

            SetBusinessMessage();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            //Supply List
            SupplyList.DataSource = dtsProgramAgreement.OrderSupply;
            IsSuccess = SupplyList.UpdateDataSource();

            DataRow row = dtsProgramAgreement.ShipmentGroup.Rows[0];
            int ShipTo = Convert.ToInt32(radBtnLstShipTo.SelectedValue);
            int ShipGrpID = 0;

            if (!row.IsNull(ShipmentGroupTable.FLD_PKID)) {
                ShipGrpID = Convert.ToInt32(row[ShipmentGroupTable.FLD_PKID]);
            }
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_SHIP_SUPPLY_TO, ShipTo.ToString());

            //count the Quantity			
            if (dtsProgramAgreement.OrderSupply.TotalQuantity > 0) {
                if (reqFldVal_DeliveryDate.IsValid && compVal_DeliveryDate.IsValid) {
                    clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, txtDeliveryDate.Text);
                    clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_DELIVERY_NLT, txtDeliveryDate.Text);
                }
            }

            if (ShipTo != 2) // When this is an other
			{
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsProgramAgreement;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                IsSuccess = AddressSupply.UpdateDataSource();
            }
            else {
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsProgramAgreement;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                IsSuccess = AddressSupply.DeleteDataSource();
            }
            return IsSuccess;
        }

        protected void radBtnLstShipTo_SelectedIndexChanged(object sender, System.EventArgs e) {
            int ShipTo = Convert.ToInt32(radBtnLstShipTo.SelectedValue);
            DataRow row = dtsProgramAgreement.ShipmentGroup.Rows[0];
            row[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = ShipTo;
            int ShipGrpID = 0;

            if (!row.IsNull(ShipmentGroupTable.FLD_PKID)) {
                ShipGrpID = Convert.ToInt32(row[ShipmentGroupTable.FLD_PKID]);
            }
            if (radBtnLstShipTo.SelectedValue == "3") {
                AddressSupply.Visible = true;

                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsProgramAgreement;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();
                QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
                prgSys.SetDefaultShippingSupplyPostalAddress(dtsProgramAgreement, this.Page.UserID);
                prgSys.SetDefaultShippingSupplyPhoneNumber(dtsProgramAgreement, this.Page.UserID);
                prgSys.SetDefaultShippingSupplyEmailAddress(dtsProgramAgreement, this.Page.UserID);

                AddressSupply.BindForm();
            }
            else if (radBtnLstShipTo.SelectedValue == "1") //FM
			{
                AddressSupply.Visible = true;

                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsProgramAgreement;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();

                QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
                prgSys.SetFMShippingSupplyPostalAddress(dtsProgramAgreement, this.Page.UserID);

                AddressSupply.BindForm();
            }
            else if (radBtnLstShipTo.SelectedValue == "2") //Same than the shipping address
			{
                AddressSupply.Visible = false;
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsProgramAgreement;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();
            }
        }

        private void Refresh_NbDayLeadTime() {
            //Delivery Date Shipping
            ShipmentGroupTable dtblShipmentGroup = dtsProgramAgreement.ShipmentGroup;
            compVal_DeliveryDate.Validate();
            if ((compVal_DeliveryDate.IsValid) && (txtDeliveryDate.Text.Trim().Length > 0)) {
                DateTime deliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                int NbDayLeadTime = 3;
                DateTime orderDate = Convert.ToDateTime(dtsProgramAgreement.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
                QSPForm.Business.BusinessCalendarSystem calSys = new QSPForm.Business.BusinessCalendarSystem();
                NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate);
                deliveryDate = calSys.GetNextBusinessDay(orderDate, NbDayLeadTime);

                txtDeliveryDate.Text = deliveryDate.ToShortDateString();
                lblDayLeadTime.Text = NbDayLeadTime.ToString();

                if (dtblShipmentGroup.Rows.Count > 0) {
                    dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE] = deliveryDate;
                }
            }
        }

        public bool ValidateForm() {
            bool blnQTyValid = false;
            trValSum.Visible = false;

            if (!SupplyList.ValidateForm()) {
                return false;
            }
            else {
                SupplyList.DataSource = dtsProgramAgreement.OrderSupply;
                SupplyList.UpdateDataSource();
                //count the Quantity
                int quantity = 0;
                foreach (DataRow row in dtsProgramAgreement.OrderSupply) {
                    quantity = quantity + Convert.ToInt32(row[OrderDetailTable.FLD_QUANTITY]);
                }
                blnQTyValid = (quantity == 0);
            }
            //We Validate the Other information only if at least one 
            //Supply product have been entered
            if (!blnQTyValid) {
                reqFldVal_DeliveryDate.Validate();
                if (!reqFldVal_DeliveryDate.IsValid) {
                    trValSum.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitle);
                    Page.MaintainScrollPositionOnPostBack = false;

                    return false;
                }

                compVal_DeliveryDate.Validate();
                if (!compVal_DeliveryDate.IsValid) {
                    trValSum.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitle);
                    Page.MaintainScrollPositionOnPostBack = false;

                    return false;
                }

                if (!AddressSupply.IsValid()) {
                    trValSum.Visible = true;
                    clsUtil.RenderStartUpScroll(lblTitle);
                    Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
            }
            else {
                //We removed the info for supply
            }
            //if everything have been ok

            return true;
        }
    }
}