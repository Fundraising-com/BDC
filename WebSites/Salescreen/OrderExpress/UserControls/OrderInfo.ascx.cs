using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using dataRef = QSPForm.Common.DataDef.OrderData;

using QSP.OrderExpress.Web.Code;

using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSPForm.Common;
using QSPForm.Common.Entity;


namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for BaseWebFormControl.
    /// </summary>
	public partial class OrderInfo: BaseWebFormControl
	{
        private bool isDateUsed = true;
        private bool isTimeUsed = false;
        private bool isOptionUsed = false;

        private CommonUtility util = new CommonUtility();
        protected OrderData dtsOrder;
        protected AccountData dtsAccount;
        protected System.Web.UI.WebControls.Label lblShipSullpyDeliveryDate;
        private CommonUtility clsUtil = new CommonUtility();
        protected System.Web.UI.HtmlControls.HtmlTable Table11;
        private bool c_ShowOnlyException = false;
        private string[] ShutDownStartDates = null;
        private string[] ShutDownEndDates = null;
        private string ShutdownString = String.Empty;
        private int ShutdownFormID = 0;


        #region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
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

        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here			
            if (!IsPostBack)
            {
                FillList();
            }

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

            //Always invisble. 
            trAccountStatus.Visible = false;  //(Page.Role > QSPForm.Business.AuthSystem.ROLE_FM);
        }
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            //Management for the Validation to only display the exception
            if (c_ShowOnlyException)
            {
                trAccountInfo.Visible = false;
                trOrderAddress.Visible = false;
                trOrderInfo.Visible = false;
                trOrderTerms.Visible = false;
                trOrderDetail.Visible = false;
                trOrderSupply.Visible = false;
            }
            else
            {
                trAccountInfo.Visible = true;
                trOrderAddress.Visible = true;
                trOrderInfo.Visible = true;
                trOrderTerms.Visible = true;
                trOrderDetail.Visible = true;
                //We use that property cause it's persistent
                //Instead of getting the info by the DataSource
                //When it's load by OrderDetailInfo, the DataSource
                //is not save in ViewState for performance.
                trOrderSupply.Visible = (OrderSupplyListInfoFinal.Count > 0);
            }
        }
        protected void Page_DataBinding(object sender, EventArgs e)
        {
            //BindForm();
        }

		public OrderData DataSource
		{
			get
			{
				return dtsOrder;
			}
			set
			{
                dtsOrder = value;
                OrderExceptionList.DataSource = dtsOrder.OrderException;
                OrderExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ORDER_BILLING;
                OrderExceptionList.ShipmentGroup_DataSource = dtsOrder.ShipmentGroup;

                //int formId = Convert.ToInt32(Request.QueryString["FormId"]);
                int formId = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0].ItemArray[24]);

                QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(formId);
                if (form.WarehouseTypeId == 1)
                {
                    OrderExceptionList.IsCHR = true;
                }
                else
                {
                    OrderExceptionList.IsCHR = false;
                }

                BusinessRuleSystem ruleSys = new BusinessRuleSystem();
                int requiredLeadTime = ruleSys.SetMinNbDayLeadTime(dtsOrder, Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][FormTable.FLD_PKID]), FormSectionType.STANDARD_PRODUCT);
                OrderExceptionList.RequiredLeadTime = requiredLeadTime;

                int selectedLeadTime = 0;

                BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                DateTime orderDate1 = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);

                if (!shipRow.IsNull(10))
                {
                    DateTime ShipTo_DeliveryDate;
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[10]);
                    selectedLeadTime = calSys.GetNbDayLeadTime(orderDate1, ShipTo_DeliveryDate) + 1;
                    OrderExceptionList.SelectedLeadTime = selectedLeadTime;
                }

                if (selectedLeadTime <= requiredLeadTime)
                {
                    OrderExceptionList.IsCHRExpeditedFreightNeeded = true;
                }
                else
                {
                    OrderExceptionList.IsCHRExpeditedFreightNeeded = false;
                }
            }
        }
		public AccountData AccountDataSource
		{
			get
			{
                return dtsAccount;
            }
			set
			{
                dtsAccount = value;
                AccountExceptionList.DataSource = dtsAccount.AccountException;
                AccountExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT;
            }
        }
		public bool IsExceptionReadOnly
		{
			get
			{
                return OrderExceptionList.IsReadOnly;
            }
			set
			{
                OrderExceptionList.IsReadOnly = value;
            }
        }
		public bool imgBtnDetailAccount_Visible
		{
			get
			{
				return imgBtnDetailAccount.Visible;
			}
			set
			{
				imgBtnDetailAccount.Visible = value;				
			}
		}
		public bool ShowOnlyException
		{
			get
			{
				return c_ShowOnlyException;
			}
			set
			{
				c_ShowOnlyException = value;
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

		public override void BindForm()
		{			
			//OrderHeader 
			OrderHeaderTable dtblOrderHeader = dtsOrder.OrderHeader;
			BusinessCalendarSystem calSys = new BusinessCalendarSystem();	

            DataRow campRow = dtsAccount.Campaign.Rows[0];
            //Campaign Information				

            if (campRow[CampaignTable.FLD_START_DATE] != DBNull.Value)
                lblStartDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_START_DATE]).ToShortDateString();
            if (campRow[CampaignTable.FLD_END_DATE] != DBNull.Value)
                lblEndDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_END_DATE]).ToShortDateString();
            //Last FY
            if (campRow.RowState != DataRowState.Added)
                lblLastFiscalYear.Text = campRow[CampaignTable.FLD_FISCAL_YEAR].ToString();
            else
                lblLastFiscalYear.Text = "";
            //Init
            lblEstimatedAmount.Text = "0";
            lblEnrollment.Text = "0";

            if (campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS] != DBNull.Value)
                lblEstimatedAmount.Text = Convert.ToDecimal(campRow[CampaignTable.FLD_GOAL_ESTIMATED_GROSS]).ToString("C");
            if (campRow[CampaignTable.FLD_ENROLLMENT] != DBNull.Value)
                lblEnrollment.Text = Convert.ToInt32(campRow[CampaignTable.FLD_ENROLLMENT]).ToString("N0");

            lblProgramTypeName.Text = campRow[CampaignTable.FLD_PROG_TYPE_NAME].ToString();

            //Trade Class - ReadOnly
            if (!campRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                lblTradeClass.Text = campRow[CampaignTable.FLD_TRADE_CLASS_NAME].ToString();
            else
                lblTradeClass.Text = "None";

            if (campRow[CampaignTable.FLD_TAX_EXEMPTION_NO] != System.DBNull.Value)
                lblTaxExemptionNumber.Text = campRow[CampaignTable.FLD_TAX_EXEMPTION_NO].ToString();
            if (campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE] != System.DBNull.Value)
                lblTaxExemptionExpirationDate.Text = Convert.ToDateTime(campRow[CampaignTable.FLD_TAX_EXEMPTION_EXP_DATE]).ToShortDateString();

            //Account Information
            DataRow row = dtsAccount.Account.Rows[0];

            lblAccID.Text = row[AccountTable.FLD_PKID].ToString();

            if (row[AccountTable.FLD_FULF_ACCOUNT_ID] != System.DBNull.Value)
                lblEDSAccID.Text = row[AccountTable.FLD_FULF_ACCOUNT_ID].ToString();
            else
                lblEDSAccID.Text = "New Account";

            lblAccountName.Text = row[AccountTable.FLD_NAME].ToString();
            //Account Comment
            lblAccountComment.Text = row[AccountTable.FLD_COMMENTS].ToString();
            //Account Last Sales Date
            if (!row.IsNull(AccountTable.FLD_LAST_ORDER_DATE))
                lblLastOrderDate.Text = Convert.ToDateTime(row[AccountTable.FLD_LAST_ORDER_DATE]).ToShortDateString();
            else
                lblLastOrderDate.Text = "";

            //Organization Information
            DataRow OrgRow = dtsAccount.Organization.Rows[0];

            //Display Organization Information
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
            //Account Status					
            lblAccountStatus.Text = row[AccountTable.FLD_ACCOUNT_STATUS_NAME].ToString();
            lblAccountStatus_ShortDescription.Text = row[AccountTable.FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION].ToString();
            lblAccountStatusColor.BackColor = Color.FromName(row[AccountTable.FLD_ACCOUNT_STATUS_COLOR_CODE].ToString());

            //FM in the Account
            lblAccountFMInfo.Text = row[AccountTable.FLD_FM_ID].ToString() + " - " + row[AccountTable.FLD_FM_NAME].ToString();

            int AccID = Convert.ToInt32(row[AccountTable.FLD_PKID].ToString());
            string sIDName = AccountDetailInfo.ACC_ID;
            //clsUtil.SetJScriptForOpenDetail(imgBtnDetailAccount, QSPForm.Business.AppItem.AccountDetailInfo, sIDName, AccID.ToString(), 0, 0, "OnClick");
            clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailAccount, "AccountDetailInfo.aspx?", sIDName, AccID.ToString(), 0, 0, "OnClick");

            //Account Exceptions
            tblAccountException.Visible = true;
            AccountExceptionList.EntityID = AccID;
            AccountExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT; //Account
            AccountExceptionList.DataSource = dtsAccount.AccountException;
            AccountExceptionList.DataBind();

            int OrderID = Convert.ToInt32(dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            int ShipGrpID = 0;
            if (dtblShipmentGroup.Rows.Count > 0)
                ShipGrpID = Convert.ToInt32(dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);

            //Bill To Address
            AddressControlInfo_Billing.ParentID = OrderID;
            AddressControlInfo_Billing.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_BILLING; //Order Billing
            AddressControlInfo_Billing.DataSource = dtsOrder;
			AddressControlInfo_Billing.FilterTypeAddress = PostalAddressType.TYPE_BILLING;
            AddressControlInfo_Billing.HideTypeAddress = true;
            AddressControlInfo_Billing.HideTitleAddress = true;
            AddressControlInfo_Billing.DataBind();

            //Ship To Address
            AddressControlInfo_Shipping.ParentID = ShipGrpID;
            AddressControlInfo_Shipping.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING; //Order Shipping
            AddressControlInfo_Shipping.DataSource = dtsOrder;
			AddressControlInfo_Shipping.FilterTypeAddress = PostalAddressType.TYPE_SHIPPING;
            AddressControlInfo_Shipping.HideTypeAddress = true;
            AddressControlInfo_Shipping.HideTitleAddress = true;
            AddressControlInfo_Shipping.DataBind();

            //Order Information
            DataRow ordRow = dtblOrderHeader.Rows[0];

            //Order ID
            if (dtblOrderHeader.Rows[0].RowState == DataRowState.Added)
                lblOrderID.Text = "New Order";
            else
                lblOrderID.Text = dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_PKID].ToString();
            //EDS Order #
            if (!ordRow.IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID))
                lblEDSOrderID.Text = ordRow[OrderHeaderTable.FLD_FULF_ORDER_ID].ToString();
            else
                lblEDSOrderID.Text = "New Order";

            //Order Status
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

            //Order Type
            lblOrderTypeName.Text = ordRow[OrderHeaderTable.FLD_ORDER_TYPE_NAME].ToString();

            //Delivery Date Shipping			
            DateTime deliveryDate = DateTime.Today;
            DateTime deliveryTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);

            DateTime orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
            lblOrderDate.Text = orderDate.ToLongDateString() + " " + orderDate.ToShortTimeString();
            int NbDayLeadTime = 3;

            ShutdownString = clsUtil.GetShutDownForm(FormID, ref ShutdownFormID);
            if (ShutdownString != null)
            {
                ShutDownStartDates = clsUtil.GetShutDownStartDate(ShutdownString);
                ShutDownEndDates = clsUtil.GetShutDownEndDate(ShutdownString);
            }

			if (dtblShipmentGroup.Rows.Count >0)
			{
                //Delivery Date and Nb Day Lead Time
                DataRow shipRow = dtblShipmentGroup.Rows[0];

				if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE))
				{				
                    deliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);
                    NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate, ShutDownStartDates, ShutDownEndDates, FormID, ShutdownFormID);

                    if (this.isDateUsed)
                    {
                        lblDeliveryDate.Text = deliveryDate.ToLongDateString();

                        if (this.isTimeUsed)
                        {
                            if (shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME))
                            {
                                this.trRequestedDeliveryTime.Visible = false;
                            }
                            else
                            {
                                this.trRequestedDeliveryTime.Visible = true;

                                deliveryTime = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME]);
                                lblDeliveryTime.Text = deliveryTime.ToString("HH:mm");
                            }
                        }
                        else
                        {
                            this.trRequestedDeliveryTime.Visible = false;
                        }
                    }
                    else
                    {
                        FormSystem formSystem = new FormSystem();
                        WeekOfItem weekOfItem = formSystem.GetWeekOfItem(this.FormID, deliveryDate);
                        lblDeliveryDate.Text = weekOfItem.Description;

                        this.trRequestedDeliveryTime.Visible = false;
                    }
                    
                    
					lblDayLeadTime.Text = (NbDayLeadTime + 1).ToString();
				}

				//Shipping Date
				if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPMENT_DATE))
				{
					lblShippingDate.Text = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SHIPMENT_DATE]).ToLongDateString();
				}

                #region Delivery Method

                lblDeliveryMethodName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_NAME].ToString();
				if (shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID].ToString() == "2")
				{
                    lblWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID].ToString() + "&nbsp;-&nbsp; " + shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();
                    trWarehouseInfo.Visible = true;
					if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
					{
                        string sID = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID].ToString();
                        //	clsUtil.SetJScriptForOpenDetail(imgBtnWarehouse, QSPForm.Business.AppItem.WarehouseDetailInfo, WarehouseDetailInfo.WH_ID, sID, 650, 600);
                        clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnWarehouse, "WarehouseDetailInfo.aspx?", WarehouseDetailInfo.WH_ID, sID, 650, 600);
                    }
                }
				else
				{
					trWarehouseInfo.Visible = false;
                }

                #endregion
            }

            //Customer PO#
            lblCustomerPO_Number.Text = ordRow[OrderHeaderTable.FLD_CUSTOMER_PO_NUMBER].ToString();

            //Order Comment
            lblComment.Text = ordRow[OrderHeaderTable.FLD_COMMENTS].ToString();

            //Order Detail List - Standard Product			
            OrderDetailSectionListInfoFinal.DataSource = dtsOrder;
            OrderDetailSectionListInfoFinal.FormSectionTypeID = FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListInfoFinal.BindForm();

            if (dtsOrder.OrderDetail.GetTotalQuantity(FormSectionType.OTHER_PRODUCT, 0) > 0)
            {
                //Order Detail List - Optional Product						
                OrderDetailSectionListInfo_Optional.DataSource = dtsOrder;
                OrderDetailSectionListInfo_Optional.FormSectionTypeID = FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListInfo_Optional.BindForm();
                trOrderDetail_Optional.Visible = true;
            }
            else
            {
                trOrderDetail_Optional.Visible = false;
            }

			int ShipTo = 0;

			if (dtsOrder.OrderSupply.TotalQuantity > 0)
			{
                //Supply
                OrderSupplyListInfoFinal.DataSource = dtsOrder.OrderSupply;
                OrderSupplyListInfoFinal.DataBind();
                trOrderSupply.Visible = true;

                //Ship To 

                DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];

                if (shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO] != DBNull.Value)
                    ShipTo = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIP_SUPPLY_TO]);

				if (!shipRow.IsNull(ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE))
				{
                    DateTime ShipTo_DeliveryDate;
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE]);
                    NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, ShipTo_DeliveryDate);
                    lblShipSupplyDeliveryDate.Text = ShipTo_DeliveryDate.ToLongDateString();
                    lblShipSupplyNbDayLeadTime.Text = NbDayLeadTime.ToString();

                }

                tblAddressSupply.Visible = false;
                if (ShipTo == 1)
                    lblShipTo.Text = "FSM";
                else if (ShipTo == 2)
                    lblShipTo.Text = "Account&nbsp;(Ship&nbsp;To&nbsp;Address)";
                else if (ShipTo == 3)
                    lblShipTo.Text = "Enter&nbsp;new&nbsp;Address";
                else
                    lblShipTo.Text = "";

				if (ShipTo == 1 || ShipTo == 3) 
				{					
                    tblAddressSupply.Visible = true;
                    int ShipGrpSupplyID = -1;
                    //Supply Ship To Address
					if (!dtsOrder.ShipmentGroup.Rows[0].IsNull(ShipmentGroupTable.FLD_SHIP_SUPPLY_ID))
					{
                        ShipGrpSupplyID = Convert.ToInt32(dtsOrder.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_SHIP_SUPPLY_ID]);

                        AddressControlInfo_Supply.ParentID = ShipGrpSupplyID;
                        AddressControlInfo_Supply.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING;
						AddressControlInfo_Supply.FilterTypeAddress = PostalAddressType.TYPE_SHIPPING;
                        AddressControlInfo_Supply.DataSource = dtsOrder;
                        AddressControlInfo_Supply.DataBind();
                    }
                }
            }
			else
			{
                trOrderSupply.Visible = false;
            }

			int OrderStatusID = OrderStatus.SAVED_FOR_LATER;
            if (dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID] != DBNull.Value)
                OrderStatusID = Convert.ToInt32(dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID]);
            //Exceptions
            //((dtblOrderHeader.Rows[0].RowState == DataRowState.Added) ||
			if (OrderStatusID > OrderStatus.SAVED_FOR_LATER) 
			{
                if (OrderExceptionList.IsCHR)
                {
                    #region CHR form

                    if (OrderExceptionList.IsCHRExpeditedFreightNeeded)
                    {
                        tblOrderException.Visible = true;
                        OrderExceptionList.EntityID = OrderID;
                        OrderExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ORDER_BILLING; //Account
                        OrderExceptionList.DataSource = dtsOrder.OrderException;
                        OrderExceptionList.BindForm();
                    }
                    else
                    {
                        if (dtsOrder.OrderException.Rows.Count > 0)
                        {
                            //Order Exceptions
                            tblOrderException.Visible = true;
                            OrderExceptionList.EntityID = OrderID;
                            OrderExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ORDER_BILLING; //Account
                            OrderExceptionList.DataSource = dtsOrder.OrderException;
                            OrderExceptionList.BindForm();
                        }
                        else
                        {
                            trOrderException.Visible = false;
                            tblOrderException.Visible = false;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Normal form

                    if (dtsOrder.OrderException.Rows.Count > 0)
                    {
                        //Order Exceptions
                        tblOrderException.Visible = true;
                        OrderExceptionList.EntityID = OrderID;
                        OrderExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ORDER_BILLING; //Account
                        OrderExceptionList.DataSource = dtsOrder.OrderException;
                        OrderExceptionList.BindForm();
                    }
                    else
                    {
                        trOrderException.Visible = false;
                        tblOrderException.Visible = false;
                    }

                    #endregion
                }
			}
			else
			{
                trOrderException.Visible = false;
                tblOrderException.Visible = false;
            }

            //Order Summary			
            OrderSummary.DataSource = dtsOrder;
            OrderSummary.BindForm();

            //profit rate
            if (!ordRow.IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
            {
                Decimal pr = Convert.ToDecimal(ordRow[OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                if (pr != 0)
                {
                    //Eric Charest, use formated string 
                    //pr = pr * 100;
                    lblProfitRate.Text = pr.ToString("p");// + " %";
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
		}
		private void FillList()
		{
			
		
		}
		public bool UpdateDataSource()
		{
			try
			{
				ValSumExceptionInfo.Visible = false;
				if (!OrderExceptionList.ValidateForm())
				{
                    ValSumExceptionInfo.Visible = true;
                    clsUtil.RenderStartUpScroll(ValSumExceptionInfo);
                    Page.MaintainScrollPositionOnPostBack = false;
                    return false;
                }
                OrderExceptionList.ShipmentGroup_DataSource = dtsOrder.ShipmentGroup;
                OrderExceptionList.UpdateDataSource();

                return true;

            }
			catch(Exception ex)
			{
                Page.SetPageError(ex);
            }

            return true;
        }
    }
}