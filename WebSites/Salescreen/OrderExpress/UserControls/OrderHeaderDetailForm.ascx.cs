using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using dataRef = QSPForm.Common.DataDef.OrderData;

using QSP.OrderExpress.Web.Code;

using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSPForm.Common;
using QSPForm.Common.Entity;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderHeaderDetailForm : BaseWebFormControl
    {
        private bool isDateUsed = true;
        private bool isTimeUsed = false;
        private bool isOptionUsed = false;

        protected OrderData dtsOrder;
        protected AccountData dtsAccount;
        private CommonUtility util = new CommonUtility();
        protected System.Web.UI.WebControls.DropDownList ddlOrderForm;
        protected System.Web.UI.WebControls.ImageButton imgBtnDeliveryDate;
        private CommonUtility clsUtil = new CommonUtility();
        private DateTime c_ShutDownStartDate = DateTime.Today;
        private DateTime c_ShutDownEndDate = DateTime.Today;
        public event System.EventHandler AddressHygieneConfirmed;
        private string[] ShutDownStartDates = null;
        private string[] ShutDownEndDates = null;
        private string ShutdownString = String.Empty;
        private int ShutdownFormID = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            AddJavascript();

            // If the logged user is field support of higher, 
            // the fsm can be chosen from the list
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT)
            {
                clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
            }

            trAccountStatus.Visible = false;

            #region Requested delivery date and time

            #region Determine if we use calendar or drop down list

            try
            {
                List<QSP.Business.Fulfillment.FormDeliveryDateType> fddtList = QSP.Business.Fulfillment.FormDeliveryDateType.GetFormDeliveryDateTypeFromFormId(this.FormID);

                if (fddtList.Count > 0)
                {
                    int deliveryDateTypeId = fddtList[0].DeliveryDateTypeId;

                    QSP.Business.Fulfillment.DeliveryDateType deliveryDateType = QSP.Business.Fulfillment.DeliveryDateType.GetDeliveryDateType(deliveryDateTypeId);

                    isDateUsed = deliveryDateType.IsDateUsed;
                    isTimeUsed = deliveryDateType.IsTimeUsed;
                    isOptionUsed = deliveryDateType.IsOptionUsed;
                }
            }
            catch
            {
                isDateUsed = true;
                isTimeUsed = false;
                isOptionUsed = false;
            }

            #endregion

            if (isDateUsed)
            {
                #region We use the delivery date (calenar popup)

                this.trRequestedDeliveryDate.Visible = true;
                this.trRequestedDeliveryTime.Visible = isTimeUsed;
                this.trRequestedLeadTime.Visible = true; 
                this.trRequestedDeliveryOptionList.Visible = false;

                #region Set business calendar

                int MinDayLeadTime = 0;

                if (this.FormID > 0)
                {
                    if (ddlDeliveryMethod.SelectedIndex == 0)
                    {
                        ((ShipmentGroupTable)dtsOrder.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]).Rows[0][21] = 1;
                    }
                    else
                    {
                        ((ShipmentGroupTable)dtsOrder.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]).Rows[0][21] = Convert.ToInt32(ddlDeliveryMethod.SelectedValue);
                    }

                    AdjustNbDayLeadTime(this.FormID);
                    MinDayLeadTime = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(FormSectionType.STANDARD_PRODUCT);
                }

                clsUtil.SetJScriptForOpenCalendar(hypLnkDeliveryDate, txtDeliveryDate, lblDayLeadTime, true, lblOrderDate, null, MinDayLeadTime, this.FormID);

                #endregion

                #endregion
            }
            else if (isOptionUsed)
            {
                #region We use the "week of" delivery date (drop down list)

                this.trRequestedDeliveryDate.Visible = false;
                this.trRequestedDeliveryTime.Visible = false;
                this.trRequestedLeadTime.Visible = false; 
                this.trRequestedDeliveryOptionList.Visible = true;

                #region Set drop down list

                if (this.ddlRequestedDeliveryDateDropDownList.Items.Count > 0)
                {
                }
                else
                {
                    FormSystem formSystem = new FormSystem();
                    List<WeekOfItem> weekOfItemList = formSystem.GetWeekOfOptionList(FormID);

                    this.ddlRequestedDeliveryDateDropDownList.Items.Clear();
                    foreach (WeekOfItem weekOfItem in weekOfItemList)
                    {
                        ListItem newListItem = new ListItem();

                        newListItem.Value = weekOfItem.StartDate.Ticks.ToString();
                        newListItem.Text = weekOfItem.Description;

                        this.ddlRequestedDeliveryDateDropDownList.Items.Add(newListItem);
                    }
                }

                #endregion

                #endregion
            }

            #endregion

            #region Order pickup and default warehouses

            bool isWarehosueSelectable = false;
            int defaultWarehouseId = 0;

            #region Get warehouse info from form

            QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(this.FormID);

            isWarehosueSelectable = form.IsWarehouseSelectable;
            defaultWarehouseId = Convert.ToInt32(form.DefaultWarehouseId);

            #endregion

            // If the logged user is a field sales manager, 
            // the fsm can select the warehouse for pickup (is allowed)
            if (this.Page.Role >= AuthSystem.ROLE_FM)
            {
                if (isWarehosueSelectable)
                {
                    // Enable the select warehouse list
                    clsUtil.SetJScriptForOpenSelector(imgBtnSelectWarehouse, txtFulfWarehouseID, txtWarehouseName, "WarehouseSelector.aspx", "WareHouseSelector", 750, 700, "&FormID=" + this.FormID.ToString());

                    imgBtnSelectWarehouse.Visible = true;

                    this.txtFulfWarehouseID.ReadOnly = false;
                    this.txtWarehouseName.ReadOnly = false;
                }
                else
                {
                    // Set the default warehouse info
                    QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(defaultWarehouseId);

                    imgBtnSelectWarehouse.Visible = false;

                    this.txtFulfWarehouseID.ReadOnly = true;
                    this.txtWarehouseName.ReadOnly = true;

                    this.txtFulfWarehouseID.Text = warehouse.FulfWarehouseId.ToString();
                    this.txtWarehouseName.Text = warehouse.WarehouseName.Trim();
                }
            }

            // Register warehosue selector script
            Page.RegisterStartupScript("ShowHideWarehouseSelector", "<script type=\"text/javascript\">try { ShowHideWarehouseSelector(); } catch (err) { }</script>");

            #endregion

            ShutdownString = clsUtil.GetShutDownForm(this.FormID, ref ShutdownFormID);
            if (ShutdownString != null)
            {
                ShutDownStartDates = clsUtil.GetShutDownStartDate(ShutdownString);
                ShutDownEndDates = clsUtil.GetShutDownEndDate(ShutdownString);
            }
        }
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            trFmInfo.Visible = true;
            trFmEdit.Visible = false;

            trFmEdit.Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);
            trFmInfo.Visible = !trFmEdit.Visible;

            Refresh_NbDayLeadTime();
        }
        protected void Page_DataBinding(object sender, EventArgs e)
        {
            BindForm();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
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
        private void InitializeComponent()
        {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.Load += new EventHandler(this.Page_Load);
            this.DualAddressFormControl.AddressHygieneConfirmed += new EventHandler(DualAddressFormControl_AddressHygieneConfirmed);
        }

        #endregion

        #region Properties

        public OrderData DataSource
        {
            get
            {
                return dtsOrder;
            }
            set
            {
                dtsOrder = value;
            }
        }
        public int FormID
        {
            get
            {
                int result = 0;

                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                {
                    result = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
                }

                return result;
            }
        }
        public int OrderID
        {
            get
            {
                int orderID = 0;

                if (ViewState["OrderID"] != null)
                {
                    orderID = Convert.ToInt32(ViewState["OrderID"]);
                }
                else if (DataSource != null && DataSource.OrderHeader.Rows.Count > 0)
                {
                    orderID = Convert.ToInt32(DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
                    ViewState["OrderID"] = orderID;
                }

                return orderID;
            }
            set
            {
                ViewState["OrderID"] = value;
            }
        }
        public int ShipmentGroupID
        {
            get
            {
                int shipmentGroupID = 0;

                if (ViewState["ShipmentGroupID"] != null)
                {
                    shipmentGroupID = Convert.ToInt32(ViewState["ShipmentGroupID"]);
                }
                else if (DataSource != null && DataSource.ShipmentGroup.Rows.Count > 0)
                {
                    shipmentGroupID = Convert.ToInt32(DataSource.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);
                    ViewState["ShipmentGroupID"] = shipmentGroupID;
                }

                return shipmentGroupID;
            }
            set
            {
                ViewState["ShipmentGroupID"] = value;
            }
        }
        public string DeliveryOption
        {
            get
            {
                return ddlDeliveryMethod.SelectedValue.ToString();
            }
        }

        public bool SectionAccount_Visible
        {
            get
            {
                return tblRowSectionAccount.Visible;
            }
            set
            {
                tblRowSectionAccount.Visible = value;
            }
        }
        public bool SectionOrder_Visible
        {
            get
            {
                return tblRowSectionOrder.Visible;
            }
            set
            {
                tblRowSectionOrder.Visible = value;
            }
        }

        public DateTime DeliveryDate
        {
            get
            {
                try
                {
                    // mm/dd/yyyy
                    string[] date = this.txtDeliveryDate.Text.Split('/');
                    return new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]));
                }
                catch
                {
                    throw new Exception("Delivery Date Is Invalid");
                }
            }
        }
        public DateTime DeliveryTime
        {
            get
            {
                try
                {
                    DateTime result = new DateTime();

                    if (this.trRequestedDeliveryTime.Visible)
                    {
                        // hh:mm
                        string[] time = this.txtDeliveryTime.Text.Split(':');
                        result = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0);
                    }
                    else
                    {
                        result = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 12, 0, 0);
                    }

                    return result;
                }
                catch
                {
                    throw new Exception("Delivery Time Is Invalid");
                }
            }
        }
            
        #endregion

        public void InitializeControls()
        {
            DualAddressFormControl.BillingParentID = OrderID;
            DualAddressFormControl.ShippingParentID = ShipmentGroupID;
            DualAddressFormControl.BillingParentType = EntityType.TYPE_ORDER_BILLING;
            DualAddressFormControl.ShippingParentType = EntityType.TYPE_ORDER_SHIPPING;
            DualAddressFormControl.DataSource = DataSource;
        }
        private void AddJavascript()
        {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");
        }
        public override void BindForm()
        {
            FillList();

            OrderHeaderTable dtblOrderHeader = dtsOrder.OrderHeader;
            DataRow ordRow = dtblOrderHeader.Rows[0];
            int NbDayLeadTime = 0;

            if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID))
            {
                int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                
                AccountSystem accSys = new AccountSystem();
                dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

                if (dtsAccount.Campaign.Rows.Count > 0)
                {
                    #region Campaign Information	

                    DataRow campRow = dtsAccount.Campaign.Rows[0];

                    if (!campRow.IsNull(CampaignTable.FLD_START_DATE))
                    {
                        lblStartDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_START_DATE]).ToShortDateString();
                    }
                    if (!campRow.IsNull(CampaignTable.FLD_END_DATE))
                    {
                        lblEndDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_END_DATE]).ToShortDateString();
                    }

                    //Init
                    lblEstimatedAmount.Text = "0";
                    lblEnrollment.Text = "0";

                    if (!campRow.IsNull(CampaignTable.FLD_GOAL_ESTIMATED_GROSS))
                    {
                        lblEstimatedAmount.Text = Convert.ToDecimal(campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS]).ToString("C");
                    }
                    if (!campRow.IsNull(CampaignTable.FLD_ENROLLMENT))
                    {
                        lblEnrollment.Text = Convert.ToInt32(campRow[CampaignTable.FLD_ENROLLMENT]).ToString("N0");
                    }

                    lblProgramTypeName.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();
                    
                    //Trade Class - ReadOnly
                    if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                    {
                        lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
                    }
                    else
                    {
                        lblTradeClass.Text = "None";
                    }

                    #endregion

                    #region Account Information

                    DataRow accRow = dtsAccount.Account.Rows[0];

                    //----------------------------------
                    //   Tax Information
                    //----------------------------------

                    if (!accRow.IsNull(AccountTable.FLD_TAX_EXEMPTION_NO))
                        lblTaxExemptionNumber.Text = accRow[AccountTable.FLD_TAX_EXEMPTION_NO].ToString();
                    if (!accRow.IsNull(AccountTable.FLD_TAX_EXEMPTION_EXP_DATE))
                        lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(accRow[AccountTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();

                    lblAccID.Text = accRow[AccountTable.FLD_PKID].ToString();

                    if (!accRow.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                        lblEDSAccID.Text = accRow[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
                    else
                        lblEDSAccID.Text = "New Account";

                    lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();

                    //Account Status					
                    lblAccountStatus.Text = accRow[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString();
                    lblAccountStatus_ShortDescription.Text = accRow[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
                    lblAccountStatusColor.BackColor = Color.FromName(accRow[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());

                    //FM in the Account
                    lblAccountFMInfo.Text = accRow[AccountTable.FLD_FM_ID].ToString() + " - " + accRow[AccountTable.FLD_FM_NAME].ToString();


                    lblAccountComment.Text = accRow[AccountTable.FLD_COMMENTS].ToString();

                    //Organization Information
                    DataRow OrgRow = dtsAccount.Organization.Rows[0];
                    //Look if the definition of the Organization it's override
                    //In the AccountX Table
                    if (dtsAccount.AccountX.Rows.Count == 0)
                    {
                        //When read-only mode
                        //Organization Type
                        lblOrgType.Text = OrgRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();
                        //When read-only mode
                        //Organization Level
                        lblOrgLevel.Text = OrgRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();

                    }
                    else
                    {
                        DataRow accXRow = dtsAccount.AccountX.Rows[0];
                        //When read-only mode
                        //Organization Type
                        lblOrgType.Text = accXRow[OrganizationTable.FLD_ORG_TYPE_NAME].ToString();

                        //When read-only mode
                        //Organization Level
                        lblOrgLevel.Text = accXRow[OrganizationTable.FLD_ORG_LEVEL_NAME].ToString();

                    }

                    #endregion
                }
            }


            #region OrderHeader 

            if (ordRow.RowState == DataRowState.Added)
            {
                lblOrderID.Text = "New Order";
            }
            else
            {
                lblOrderID.Text = ordRow[OrderHeaderTable.FLD_PKID].ToString();
            }

            //EDS Order #
            if (!ordRow.IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID))
            {
                lblEDSOrderID.Text = ordRow[OrderHeaderTable.FLD_FULF_ORDER_ID].ToString();
            }
            else
            {
                lblEDSOrderID.Text = "New Order";
            }

            if (ordRow.RowState == DataRowState.Added)
            {
                lblOrderStatus_ShortDescription.Text = "New Order";
                lblOrderStatusColor.BackColor = Color.White;
                lblOrderStatus_Description.Text = "";
            }
            else
            {
                lblOrderStatus_ShortDescription.Text = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_SHORT_DESCRIPTION].ToString();
                lblOrderStatusColor.BackColor = Color.FromName(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_COLOR_CODE].ToString());
                lblOrderStatus_Description.Text = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_DESCRIPTION].ToString();
            }

            lblFMInfo.Text = ordRow[OrderHeaderTable.FLD_FM_ID].ToString() + " - " + ordRow[OrderHeaderTable.FLD_FM_NAME].ToString();
            txtFMID.Text = ordRow[OrderHeaderTable.FLD_FM_ID].ToString();
            txtFMName.Text = ordRow[OrderHeaderTable.FLD_FM_NAME].ToString();
            txtCustomerPO_Number.Text = ordRow[OrderHeaderTable.FLD_CUSTOMER_PO_NUMBER].ToString();

            string sComment = ordRow[OrderHeaderTable.FLD_COMMENTS].ToString();
            txtComment.Text = sComment;

            if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_TYPE_ID))
            {
                int OrderTypeID = Convert.ToInt32(dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_TYPE_ID]);

                if (OrderTypeID > 0)
                {
                    ListItem lstItem = ddlOrderType.Items.FindByValue(OrderTypeID.ToString());
                
                    if (lstItem != null)
                    {
                        ddlOrderType.ClearSelection();
                        lstItem.Selected = true;
                    }
                }
            }

            #endregion


            DualAddressFormControl.DataBind();


            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;

            if (dtblShipmentGroup.Rows.Count > 0)
            {
                DataRow shipRow = dtblShipmentGroup.Rows[0];

                #region Order data

                DateTime orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
                lblOrderDate.Text = orderDate.ToShortDateString() + " " + orderDate.ToShortTimeString();

                #endregion

                #region Delivery Date Shipping

                DateTime deliveryDate = DateTime.Today;

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE))
                {
                    deliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);

                    BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                    NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate, ShutDownStartDates, ShutDownEndDates, this.FormID, ShutdownFormID);
                    
                    if (deliveryDate.Date <= orderDate)
                    {
                        deliveryDate = calSys.GetNextBusinessDay(orderDate, NbDayLeadTime);
                    }

                    txtDeliveryDate.Text = deliveryDate.ToShortDateString();
                    lblDayLeadTime.Text = NbDayLeadTime.ToString();
                }

                #endregion

                #region Delivery Time Shipping

                FormSystem formSystem = new FormSystem();

                if (this.isTimeUsed)
                {
                    DateTime deliveryTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 0, 0);

                    if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME))
                    {
                        deliveryTime = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME]);
                    }

                    this.txtDeliveryTime.Text = deliveryTime.ToString("HH:mm");
                }

                #endregion

                #region Shipping Date

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPMENT_DATE))
                {
                    lblShippingDate.Text = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SHIPMENT_DATE]).ToLongDateString();
                }

                #endregion

                #region Delivery Method 

                trWarehouseInfo.Style["display"] = "none";
                trWarehouseSelector.Style["display"] = "none";

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_METHOD_ID))
                {
                    ListItem lstItem = ddlDeliveryMethod.Items.FindByValue(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID].ToString());
                 
                    if (lstItem != null)
                    {
                        ddlDeliveryMethod.ClearSelection();
                        lstItem.Selected = true;
                    }

                    // Adjust Warehouse Selector if delivery method is pick up at warehouse 
                    int deliveryMethodID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);
                    if (deliveryMethodID == DeliveryMethod.PICK_UP_AT_WAREHOUSE)
                    {
                        trWarehouseSelector.Style["display"] = "block";
                    }
                }

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
                {
                    txtFulfWarehouseID.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID].ToString();
                    txtWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();
                    lblWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();

                    // Get the fulf warehouse id from the database
                    int warehouseId = 0;
                    bool isWarehouseIdAvailable = Int32.TryParse(shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID].ToString(), out warehouseId);

                    if (isWarehouseIdAvailable)
                    {
                        QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(warehouseId);

                        if (warehouse != null)
                        {
                            txtFulfWarehouseID.Text = warehouse.FulfWarehouseId.ToString();
                        }
                    }

                }

                #endregion

                #region Profit rate

                if (!ordRow.IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
                {
                    Decimal pr = Convert.ToDecimal(ordRow[OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                    
                    if (pr != 0)
                    {
                        lblProfitRate.Text = pr.ToString("p");
                    }
                    else
                    {
                        trProfitRate.Visible = false;
                        trProfitRate.Attributes.Add("display", "none");
                        trProfitRate.Style.Add("display", "none");
                    }
                }
                else
                {
                    trProfitRate.Visible = false;
                    trProfitRate.Attributes.Add("display", "none");
                    trProfitRate.Style.Add("display", "none");
                }

                #endregion
            }

            SetBusinessMessage(NbDayLeadTime);
        }
        private void FillList()
        {
            //By default
            trDeliveryMethodEdit.Visible = true;
            trDeliveryMethodInfo.Visible = false;
            trOrderTypeEdit.Visible = true;
            trOrderTypeInfo.Visible = false;
            DataRow row = dtsOrder.OrderHeader.Rows[0];

            if (this.FormID > 0)
            {
                clsUtil.SetDeliveryMethodDropDownList(ddlDeliveryMethod, true, this.FormID);
                if (ddlDeliveryMethod.Items.Count <= 2)
                {
                    string sCommonCarrierName = "";
                    BusinessRuleSystem bizRuleSys = new BusinessRuleSystem();
                    sCommonCarrierName = bizRuleSys.GetCommonCarrierName(this.FormID);
                    if (sCommonCarrierName.Length == 0)
                    {
                        ddlDeliveryMethod.SelectedIndex = 1;
                        sCommonCarrierName = ddlDeliveryMethod.SelectedItem.Text;
                    }
                    lblDeliveryMethod.Text = sCommonCarrierName;
                    ddlDeliveryMethod.SelectedIndex = 1;
                    trDeliveryMethodEdit.Visible = false;
                    trDeliveryMethodInfo.Visible = true;
                }

                clsUtil.SetOrderTypeDropDownList(ddlOrderType, true, this.FormID);
                if (ddlOrderType.Items.Count <= 2)
                {
                    trOrderTypeEdit.Visible = false;
                    ddlOrderType.SelectedIndex = 1;
                    lblOrderType.Text = ddlOrderType.SelectedItem.Text;
                    trOrderTypeInfo.Visible = true;
                }
            }
            else
            {
                clsUtil.SetDeliveryMethodDropDownList(ddlDeliveryMethod, true);
                //clsUtil.SetOrderTypeDropDownList(ddlOrderType, true);
            }
        }
        private void AdjustNbDayLeadTime(int formID)
        {
            BusinessRuleSystem ruleSys = new BusinessRuleSystem();
            ruleSys.SetMinNbDayLeadTime(dtsOrder, formID, FormSectionType.STANDARD_PRODUCT);
        }
        private void SetBusinessMessage(int minimumLeadTime)
        {
            QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(this.FormID);

            if (form.WarehouseTypeId == WarehouseType.CHRobinson)
            {
                trBusinessMessage.Visible = false;

                try
                {
                    int deliveryMethod = Convert.ToInt32(this.ddlDeliveryMethod.SelectedValue);
                }
                catch (Exception ex)
                {
                    //lblBusinessMessage.Text = "" + minimumLeadTime.ToString() + " Business Days Lead-Time Required.<BR><BR>If requested Lead-Time is <u>LESS</u> than " + minimumLeadTime.ToString() + " business Days, change the delivery date to meet this requirement.";

                }
            }
            else
            {    //Filter for Section Number
                int minNbDLT = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(FormSectionType.STANDARD_PRODUCT);
                int SectionNumber = dtsOrder.OrderDetail.GetFormSectionNumber_ForMinNbDayLeadTime(FormSectionType.STANDARD_PRODUCT, minNbDLT);

                clsUtil.SetFormBusinessMessage(lblBusinessMessage, AppItem.OrderForm_Step3, this.FormID, FormSectionType.STANDARD_PRODUCT, SectionNumber);
                trBusinessMessage.Visible = (lblBusinessMessage.Text.Length > 0);
            }
        }
        private void Refresh_NbDayLeadTime()
        {
            //Delivery Date Shipping
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            compVal_DeliveryDate.Validate();
            //custDeliveryDateValidator.Validate();
            lblDayLeadTime.Text = "";
            if (txtDeliveryDate.Text.Trim().Length > 0)
            {
                DateTime deliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                DateTime now = DateTime.Today;
                if (deliveryDate < now)
                {
                    compVal_DeliveryDate.IsValid = false;
                }
            }

            if (compVal_DeliveryDate.IsValid && (txtDeliveryDate.Text.Trim().Length > 0))
            {
                DateTime deliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                int NbDayLeadTime = 3;
                DateTime orderDate = Convert.ToDateTime(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();

                NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate, ShutDownStartDates, ShutDownEndDates, this.FormID, ShutdownFormID);
                // deliveryDate = calSys.GetNextBusinessDay(orderDate, NbDayLeadTime);

                // txtDeliveryDate.Text = deliveryDate.ToShortDateString();
                lblDayLeadTime.Text = NbDayLeadTime.ToString();

                if (dtblShipmentGroup.Rows.Count > 0)
                {
                    dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE] = deliveryDate;
                    dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_DELIVERY_NLT] = deliveryDate;
                }
            }

        }
        public void ResetStatus()
        {
            DualAddressFormControl.ResetStatus();
        }

        public bool ValidateForm()
        {
            bool isValid = true;
            trValSumOrderInfo.Visible = false;

            isValid = (isValid && (!SectionAccount_Visible || DualAddressFormControl.IsValid()));

            //Manage the Delevery method requirement
            string sDeliveryMethodID = ddlDeliveryMethod.SelectedValue;
            //Only validate if this is not "Pick Up At Warehouse"
            if (sDeliveryMethodID != DeliveryMethod.COMMON_CARRIER.ToString())
            {
                //Enabled the validation of the Warehouse				
                ReqFldVal_FulfWarehouseID.Enabled = true;
            }
            else
            {
                //Disabled the validation of the Warehouse
                ReqFldVal_FulfWarehouseID.Enabled = false;
            }

            if (isValid && !IsValid(tblOrderInfo.Controls))
            {
                trValSumOrderInfo.Visible = true;
                Page.MaintainScrollPositionOnPostBack = false;
                clsUtil.RenderStartUpScroll(lblTitleOrderInfo);
                isValid = false;
            }

            return isValid;
        }
        public void ValidateDeliveryDate(object sender, ServerValidateEventArgs e)
        {
            //Set business calendar 
            int MinDayLeadTime = 0;

            DateTime DeadLineDate = DateTime.Today;
            DateTime DeadLineNextFiscal = DateTime.Today;

            if (this.FormID > 0)
            {
                AdjustNbDayLeadTime(this.FormID);
                MinDayLeadTime = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(FormSectionType.STANDARD_PRODUCT);
            }

            DateTime orderDate = Convert.ToDateTime(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
            BusinessCalendarSystem calSys = new BusinessCalendarSystem();

            FormSystem formSystem = new FormSystem();
            FormTable formTable = formSystem.SelectOne(this.FormID);

            // TODO: Replace the hardcoded Form Group ID with the form type when available
            if ((int)formTable.Rows[0][FormTable.FLD_FORM_GROUP_ID] == 37)
            {
                DeadLineDate = calSys.GetNextBusinessDay(orderDate, MinDayLeadTime);

                e.IsValid = DeliveryDate >= DeadLineDate;
            }

            if (e.IsValid && this.FormID == ShutdownFormID)
            {
                for (int i = 0; i < ShutDownEndDates.Length; i++)
                {
                    if (DeliveryDate >= Convert.ToDateTime(ShutDownStartDates[i]) && DeliveryDate <= Convert.ToDateTime(ShutDownEndDates[i]))
                    {
                        e.IsValid = false;
                        custDeliveryDateValidator.ErrorMessage = "Please enter another date. WFC PE Production is closed from " + Convert.ToDateTime(ShutDownStartDates[i]).ToShortDateString() + " to " + Convert.ToDateTime(ShutDownEndDates[i]).ToShortDateString();
                    }
                }
            }

            DeadLineNextFiscal = calSys.GetLastDateOfFiscalYear();

            if (e.IsValid && DeliveryDate < DeadLineDate)
            {
                e.IsValid = false;
                custDeliveryDateValidator.ErrorMessage = "Please enter another date";
            }
            //else if (e.IsValid && DeliveryDate > DeadLineNextFiscal)
            //{
            //    e.IsValid = false;
            //    custDeliveryDateValidator.ErrorMessage = "Delivery dates in the next fiscal year are only accepted within that fiscal year as products are subject to change.";
            //}

        }
        public bool UpdateDataSource()
        {

            DataRow row = dtsOrder.OrderHeader.Rows[0];

            //---ORDER CONTACT INFORMATION ---------------------
            DualAddressFormControl.Update();

            if (ddlOrderType.SelectedIndex > 0)
            {
                int OrderTypeID = Convert.ToInt32(ddlOrderType.SelectedValue);
                clsUtil.UpdateRow(row, OrderHeaderTable.FLD_ORDER_TYPE_ID, ddlOrderType.SelectedValue);
                clsUtil.UpdateRow(row, OrderHeaderTable.FLD_ORDER_TYPE_NAME, ddlOrderType.SelectedItem.Text);
            }

            if (ddlDeliveryMethod.SelectedIndex > 0)
            {
                clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_METHOD_ID, ddlDeliveryMethod.SelectedValue);
                clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_METHOD_NAME, ddlDeliveryMethod.SelectedItem.Text);

            }

            if (txtFulfWarehouseID.Text.Trim().Length > 0)
            {
                bool IsChanged = clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID, txtFulfWarehouseID.Text);
                if (IsChanged)
                {
                    WarehouseSystem wareSys = new WarehouseSystem();
                    WarehouseData dtsWare = new WarehouseData();
                    dtsWare = wareSys.SelectAllDetailByFulfWarehouseID(Convert.ToInt32(txtFulfWarehouseID.Text));
                    WarehouseTable dTblWare = dtsWare.Warehouse;
                    if (dTblWare.Rows.Count > 0)
                    {
                        DataRow wareRow = dTblWare.Rows[0];
                        string sWareID = wareRow[WarehouseTable.FLD_PKID].ToString();
                        if (sWareID.Length > 0)
                            clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID, sWareID);
                    }
                }
                clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME, txtWarehouseName.Text);
            }
            //Recalculate the delivery warehouse
            DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
            //do only if it's null
            if (shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID))
            {
                OrderSystem ordSys = new OrderSystem();
                ordSys.SetDefaultWarehouse(dtsOrder);
            }

            //Customer PO #
            clsUtil.UpdateRow(dtsOrder.OrderHeader.Rows[0], OrderHeaderTable.FLD_CUSTOMER_PO_NUMBER, txtCustomerPO_Number.Text);

            string sComment = "";
            sComment = txtComment.Text.Trim();
            clsUtil.UpdateRow(dtsOrder.OrderHeader.Rows[0], OrderHeaderTable.FLD_COMMENTS, sComment);

            if (isDateUsed)
            {
                compVal_DeliveryDate.Validate();
                if (compVal_DeliveryDate.IsValid)
                {
                    string date = txtDeliveryDate.Text;
                    string time = txtDeliveryDate.Text;
                    if (txtDeliveryTime.Text != null)
                    {
                        if (txtDeliveryTime.Text.Trim().Length > 0)
                        {
                            time = txtDeliveryTime.Text;
                        }
                    }

                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, date);
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME, time);
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_NLT, txtDeliveryDate.Text);
                }
            }
            else if (isOptionUsed)
            {
                try
                {
                    long ticks = Convert.ToInt64(this.ddlRequestedDeliveryDateDropDownList.SelectedItem.Value);
                    DateTime selectedWeekOfStartDate = new DateTime(ticks);

                    string date = selectedWeekOfStartDate.ToShortDateString();
                    string time = selectedWeekOfStartDate.ToShortTimeString();

                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, date);
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME, date + " " + time);
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_DELIVERY_NLT, selectedWeekOfStartDate.ToShortDateString());
                }
                catch { }
            }

            //Save the FM Info
            if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT)
            {
                if (txtFMID.Text.Trim().Length > 0)
                {
                    clsUtil.UpdateRow(row, OrderHeaderTable.FLD_FM_ID, txtFMID.Text.Trim());
                    if (txtFMName.Text.Trim().Length > 0)
                    {
                        clsUtil.UpdateRow(row, OrderHeaderTable.FLD_FM_NAME, txtFMName.Text.Trim());

                    }
                }
            }

            return true;
        }

        protected void DualAddressFormControl_AddressHygieneConfirmed(object sender, EventArgs e)
        {
            if (AddressHygieneConfirmed != null)
            {
                AddressHygieneConfirmed(this, EventArgs.Empty);
            }
        }
        private void imgBtnDeliveryDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Refresh_NbDayLeadTime();
        }


    }
}