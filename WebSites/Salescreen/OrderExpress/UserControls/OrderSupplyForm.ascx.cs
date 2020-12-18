using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderSupplyForm : BaseWebUserControl {
        private CommonUtility util = new CommonUtility();
        private OrderData dtsOrder;
        private CommonUtility clsUtil = new CommonUtility();
        private int c_FormID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here	
            this.SupplyList.FormID = c_FormID;
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
            clsUtil.SetFormBusinessMessage(lblBusinessMessage, QSPForm.Business.AppItem.OrderForm_Step6, c_FormID);
            trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            tblAddressSupply.Visible = (radBtnLstShipTo.SelectedValue != "2");

            //Find the Supply MinDayLeadTime attached to the form			
            BusinessRuleSystem ruleSys = new BusinessRuleSystem();
            int MinDayLeadTime = ruleSys.GetMinNbDayLeadTime_Supply(c_FormID);
            clsUtil.SetJScriptForOpenCalendar(hypLnkDeliveryDate, txtDeliveryDate, lblDayLeadTime, true, lblOrderDate, null, MinDayLeadTime);

            Refresh_NbDayLeadTime();
        }

        public dataRef DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
                SupplyList.DataSource = dtsOrder.OrderSupply;
                c_FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
                SupplyList.FormID = c_FormID;
            }
        }

        public string LabelTotalQuantityClientID {
            get {
                return SupplyList.LabelTotalQuantityClientID;
            }
        }

        public void BindForm() {
            DateTime orderDate = DateTime.Today;
            DateTime ShipTo_NLT;
            DateTime ShipTo_DeliveryDate;

            DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];

            if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_DATE)) {
                orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
            }

            lblOrderDate.Text = orderDate.ToShortDateString() + " " + orderDate.ToShortTimeString(); ;

            if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_ID)) {
                int ShipGrpID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
                int ShipTo = 0;

                if (shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] != DBNull.Value) {
                    ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);
                    radBtnLstShipTo.SelectedIndex = (ShipTo - 1);

                    if (ShipTo != 2) {
                        AddressSupply.Visible = true;
                        AddressSupply.ParentID = ShipGrpID;
                        AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                        AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                        AddressSupply.DataSource = dtsOrder;
                        AddressSupply.BindForm();
                    }
                    else {
                        AddressSupply.Visible = false;
                    }
                }
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE)) {
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE]);
                    txtDeliveryDate.Text = ShipTo_DeliveryDate.ToShortDateString();
                    Refresh_NbDayLeadTime();
                }
            }

            SupplyList.DataSource = dtsOrder.OrderSupply;
            SupplyList.BindForm();

            SetBusinessMessage();
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        public bool UpdateDataSource() {
            bool IsSuccess = false;

            //Supply List
            SupplyList.DataSource = dtsOrder.OrderSupply;




            IsSuccess = SupplyList.UpdateDataSource();

            DataRow row = dtsOrder.ShipmentGroup.Rows[0];
            int ShipTo = Convert.ToInt32(radBtnLstShipTo.SelectedValue);
            int ShipGrpID = 0;

            if (!row.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_ID)) {
                ShipGrpID = Convert.ToInt32(row[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
            }
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_SHIP_SUPPLY_TO, ShipTo.ToString());

            //count the Quantity			
            if (dtsOrder.OrderSupply.TotalQuantity > 0) {
                if (reqFldVal_DeliveryDate.IsValid && compVal_DeliveryDate.IsValid) {
                    clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE, txtDeliveryDate.Text);
                    clsUtil.UpdateRow(row, ShipmentGroupTable.FLD_SUPPLY_DELIVERY_NLT, txtDeliveryDate.Text);
                }
            }

            if (ShipTo != 2) // When this is an other
			{
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsOrder;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                IsSuccess = AddressSupply.UpdateDataSource();
            }
            else {
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsOrder;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                IsSuccess = AddressSupply.DeleteDataSource();
            }
            return IsSuccess;
        }

        protected void radBtnLstShipTo_SelectedIndexChanged(object sender, System.EventArgs e) {
            int ShipTo = Convert.ToInt32(radBtnLstShipTo.SelectedValue);
            DataRow row = dtsOrder.ShipmentGroup.Rows[0];
            row[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] = ShipTo;
            int ShipGrpID = 0;

            if (!row.IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_ID)) {
                ShipGrpID = Convert.ToInt32(row[ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);
            }
            if (radBtnLstShipTo.SelectedValue == "3") {
                AddressSupply.Visible = true;

                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsOrder;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();
                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                ordSys.SetDefaultShippingSupplyPostalAddress(dtsOrder, this.Page.UserID);
                ordSys.SetDefaultShippingSupplyPhoneNumber(dtsOrder, this.Page.UserID);
                ordSys.SetDefaultShippingSupplyEmailAddress(dtsOrder, this.Page.UserID);

                AddressSupply.BindForm();
            }
            else if (radBtnLstShipTo.SelectedValue == "1") //FM
			{
                AddressSupply.Visible = true;

                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsOrder;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();

                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                ordSys.SetFMShippingSupplyPostalAddress(dtsOrder, this.Page.UserID);

                AddressSupply.BindForm();
            }
            else if (radBtnLstShipTo.SelectedValue == "2") //Same than the shipping address
			{
                AddressSupply.Visible = false;
                AddressSupply.ParentID = ShipGrpID;
                AddressSupply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
                AddressSupply.DataSource = dtsOrder;
                AddressSupply.FilterTypeAddress = QSPForm.Common.PostalAddressType.TYPE_SHIPPING;
                AddressSupply.DeleteDataSource();
            }
        }

        private void Refresh_NbDayLeadTime() {
            //Delivery Date Shipping
            
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            compVal_DeliveryDate.Validate();
            if ((compVal_DeliveryDate.IsValid) && (txtDeliveryDate.Text.Trim().Length > 0))
            {
                DateTime deliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                int NbDayLeadTime = 3;
                DateTime orderDate = Convert.ToDateTime(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
                QSPForm.Business.BusinessCalendarSystem calSys = new QSPForm.Business.BusinessCalendarSystem();
                NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate);
                deliveryDate = calSys.GetNextBusinessDay(orderDate, NbDayLeadTime);

                txtDeliveryDate.Text = deliveryDate.ToShortDateString();
                lblDayLeadTime.Text = NbDayLeadTime.ToString();

                if (dtblShipmentGroup.Rows.Count > 0)
                {
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
                SupplyList.DataSource = dtsOrder.OrderSupply;
                SupplyList.UpdateDataSource();
                //count the Quantity
                int quantity = 0;
                foreach (DataRow row in dtsOrder.OrderSupply) {
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