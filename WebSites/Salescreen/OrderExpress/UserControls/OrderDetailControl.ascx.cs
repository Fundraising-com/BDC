using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using dataDef = QSPForm.Common.DataDef.OrderData;

using QSP.OrderExpress.Web.Code;
using QSP.WebControl.Reporting;

using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSPForm.Common;
using QSPForm.Common.Entity;


namespace QSP.OrderExpress.Web.UserControls {
    public partial class OrderDetailControl : BaseWebFormControl
    {
        private bool isDateUsed = true;
        private bool isTimeUsed = false;
        private bool isOptionUsed = false;

        public const string ORDER_ID = "OrderID";
        private string IMG_EDIT_ORDER_AND_OR_PERSONALIZATION_URL = "~/images/btnEditOrderAndOrPersonalization.gif";
        private string IMG_EDIT_PERSONALIZATION_URL = "~/images/BtnEditPersonalization.gif";
        private string IMG_VIEW_PERSONALIZATION_URL = "~/images/btnViewPersonalization.gif";
        private const string IMG_SUBMIT_AND_PERSONALIZE_URL = "~/images/btnSubmitOrderAndPersonalize.gif";
        private const string IMG_SAVE_AND_PERSONALIZE_URL = "~/images/btnSaveOrderAndPersonalize.gif";
        private const string GO_TO_PERSONALIZATION_MSG = "Do you want to edit the personalization for this order?";
        protected dataDef dtsOrder;
        protected AccountData dtsAccount;
        private const string ORDER_DATA = "OrderData";
        private const string ACCOUNT_DATA = "AccountData";
        private int c_OrderID;
        private int c_ParentIDAudit = 0;
        private int c_ParentTypeAudit = 0;
        bool c_HideHistoryLink = true;
        protected DataTable dTblAudit;
        private CommonUtility clsUtil = new CommonUtility();
        //private DateTime c_ShutDownStartDate = DateTime.Today;
        //private DateTime c_ShutDownEndDate = DateTime.Today;
        //private int shutdownformID = 0;
        public event System.EventHandler<ReadOnlyStatusChangedEventArgs> ReadOnlyStatusChanged;
        private string[] ShutDownStartDates = null;
        private string[] ShutDownEndDates = null;
        private string ShutdownString = String.Empty;
        private int ShutdownFormID = 0;

        Dictionary<string, string> parameterDictionary = new Dictionary<string, string>();

        #region Properties

        public int OrderID
        {
            get
            {
                return c_OrderID;
            }
            set
            {
                c_OrderID = value;
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
                else if (dtsOrder != null && dtsOrder.ShipmentGroup.Rows.Count > 0)
                {
                    shipmentGroupID = Convert.ToInt32(dtsOrder.ShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);
                    ViewState["ShipmentGroupID"] = shipmentGroupID;
                }

                return shipmentGroupID;
            }
            set
            {
                ViewState["ShipmentGroupID"] = value;
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
        private bool IsPEForm
        {
            get
            {
                int FormID = 0;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

                bool ispe = false;
                foreach (string s in PEForm)
                {
                    if (s == FormID.ToString())
                    {
                        ispe = true;
                        break;
                    }
                }
                return ispe;
            }
        }
        private string[] PEForm
        {
            get
            {
                string peForm = "";
                if (ConfigurationManager.AppSettings["PEForm"] != null)
                    peForm = ConfigurationManager.AppSettings["PEForm"].ToString();

                if (peForm.Length > 0)
                    return peForm.Split(',');
                else
                    return null;
            }
        }
        private bool EditPersonalization
        {
            get
            {
                return Convert.ToBoolean(Convert.ToInt32(hidEditPersonalization.Value));
            }
            set
            {
                hidEditPersonalization.Value = value ? "1" : "0";
            }
        }

        #endregion

        protected override void LoadData()
        {
            if (!IsPostBack)
            {
                // SetFormParameter();
                //Order
                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                dtsOrder = ordSys.SelectAllDetail(c_OrderID, true);
                ordSys.CalculateOrder(dtsOrder);

                //Account
                int CampaignID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

                this.ViewState[ORDER_ID] = c_OrderID;

                c_ParentTypeAudit = QSPForm.Common.EntityType.TYPE_ORDER_BILLING;
                dTblAudit = dtsOrder.OrderHeader;
                c_ParentIDAudit = c_OrderID;
            }
            else
            {
                c_OrderID = Convert.ToInt32(this.ViewState[ORDER_ID]);
                dtsOrder = (dataDef)this.ViewState[ORDER_DATA];
                dtsAccount = (AccountData)this.ViewState[ACCOUNT_DATA];
                //Each Change, is marked as new process. 
                DataRow row = dtsOrder.OrderHeader.Rows[0];
                row[OrderHeaderTable.FLD_ORDER_DATE] = DateTime.Now;
                //Delivery Date Shipping

                DateTime deliveryDate = DateTime.Today;
                DateTime orderDate = Convert.ToDateTime(row[OrderHeaderTable.FLD_ORDER_DATE]);
                lblOrderDate.Text = orderDate.ToShortDateString() + " " + orderDate.ToShortTimeString();

                Refresh_NbDayLeadTime();
            }

            int FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

            #region Load the charge list

            this.ChargeList1.OrderId = c_OrderID;
            this.ChargeList1.LoadData();
            this.ChargeList1.BindList();

            if (ChargeList1.TotalChargeCount > 0)
            {
                this.trChargeList.Visible = true;
            }
            else
            {
                this.trChargeList.Visible = false;
            }

            #endregion

            #region Determine if we use calendar or drop down list

            try
            {
                List<QSP.Business.Fulfillment.FormDeliveryDateType> fddtList = QSP.Business.Fulfillment.FormDeliveryDateType.GetFormDeliveryDateTypeFromFormId(FormID);

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

            //For each load, the page (the higher in the hirarchy)
            //is in charge to set all children's datasource 

            //HeaderDetailForm.DataSource = dtsOrder;
            ////HeaderDetailForm.AccountDataSource = dtsAccount;


            OrderDetailSectionListStep.FormSectionTypeID = FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = dtsOrder;

            if (dtsOrder.OrderDetail.IsContainFormSectionType(FormSectionType.OTHER_PRODUCT))
            {
                OrderDetailSectionListStep_Optional.FormSectionTypeID = FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.DataSource = dtsOrder;
            }
            SupplyForm.DataSource = dtsOrder;

            #region CHR CODE

            int formId = 0;
            if (Request.QueryString["FormId"] == null)
            {
                formId = FormID;
            }
            else
            {
                formId = Convert.ToInt32(Request.QueryString["FormId"]);
            }

            QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(formId);
            if (form.WarehouseTypeId == null)
            {
                if (form.WarehouseTypeId == 1)
                {
                    OrderExceptionList.IsCHR = true;
                }
                else
                {
                    OrderExceptionList.IsCHR = false;
                }
            }

            BusinessRuleSystem ruleSys = new BusinessRuleSystem();
            int requiredLeadTime = ruleSys.SetMinNbDayLeadTime(dtsOrder, FormID, FormSectionType.STANDARD_PRODUCT);
            OrderExceptionList.RequiredLeadTime = requiredLeadTime;

            int selectedLeadTime = 0;

            try
            {
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();
                DataRow shipRow = dtsOrder.ShipmentGroup.Rows[0];
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                DateTime orderDate1 = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE))
                {
                    DateTime ShipTo_DeliveryDate;
                    ShipTo_DeliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE]);
                    selectedLeadTime = calSys.GetNbDayLeadTime(orderDate1, ShipTo_DeliveryDate);
                    OrderExceptionList.SelectedLeadTime = selectedLeadTime;
                }
            }
            catch (Exception exCHR)
            {
            }

            if (selectedLeadTime < requiredLeadTime)
            {
                OrderExceptionList.IsCHRExpeditedFreightNeeded = true;
            }
            else
            {
                OrderExceptionList.IsCHRExpeditedFreightNeeded = false;
            }

            #endregion

            OrderExceptionList.DataSource = dtsOrder.OrderException;
            OrderExceptionList.EntityTypeID = EntityType.TYPE_ORDER_BILLING;
            OrderExceptionList.ShipmentGroup_DataSource = dtsOrder.ShipmentGroup;

            if (Page.Role == 1)
            {
                // If it is an fsm
                //dtsOrder.OrderHeader.Rows[0]
            }
        }
        private void SetHeaderTitle()
        {
            int ProgramTypeID = 0;

            if (dtsAccount.Campaign.Rows.Count > 0)
            {
                DataRow campRow = dtsAccount.Campaign.Rows[0];
                ProgramTypeID = Convert.ToInt32(campRow[CampaignTable.FLD_PROG_TYPE_ID]);
            }
            if (dtsAccount.Account.Rows.Count > 0)
            {
                DataRow accRow = dtsAccount.Account.Rows[0];
                lblAccountNumber.Text = accRow[AccountTable.FLD_PKID].ToString();
                lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
                lblAccountNameInfo.Text = accRow[AccountTable.FLD_NAME].ToString();
                //FM in the Account
                lblAccountFMInfo.Text = accRow[AccountTable.FLD_FM_ID].ToString() + " - " + accRow[AccountTable.FLD_FM_NAME].ToString();

            }
            int FormID = 0;
            if (dtsOrder.OrderHeader.Rows.Count > 0)
            {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_FORM_ID))
                {
                    FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
                    //Fill the Business Order Form Information
                    FormSystem formSys = new QSPForm.Business.FormSystem();
                    QSPForm.Common.DataDef.FormTable dTblForm = formSys.SelectOne(FormID);
                    if (dTblForm.Rows.Count > 0)
                    {
                        DataRow formRow = dTblForm.Rows[0];
                        lblFormID.Text = FormID.ToString();
                        lblFormName.Text = formRow[FormTable.FLD_FORM_NAME].ToString();
                        if (!formRow.IsNull(FormTable.FLD_IMAGE_URL))
                            imgBusinessForm.ImageUrl = "~/" + formRow[FormTable.FLD_IMAGE_URL].ToString();
                        else
                            imgBusinessForm.Visible = false;
                    }
                }
                else
                {
                    lblFormID.Text = "N/A";
                    lblFormName.Text = "Not Specified";
                    imgBusinessForm.Visible = false;
                }

                //Get the status of the order to check if it's possible to modified.
                if (ProgramTypeID == 11)
                {
                    int StatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                    if ((StatusID >= OrderStatus.RELEASED) && (StatusID < OrderStatus.ERROR_UNSPECIFIED))
                    {
                        // this.imgEditOrder.Visible = false;
                        this.Page.SetPageMessage("this order is not editable");
                        Label72.Text = "this order is not editable";
                        //this.trStatusMessage.Visible = true;
                    }
                }
                else
                {
                    // this.imgEditOrder.Visible = false;
                    this.Page.SetPageMessage("this order is not editable");
                    Label72.Text = "this order is not editable";
                    //this.trStatusMessage.Visible = true;
                }
            }
        }

        protected void DualAddressFormControl_AddressHygieneConfirmed(object sender, EventArgs e)
        {
            ConfirmOrder();
        }

        public void ValidateDeliveryDate(object sender, ServerValidateEventArgs e)
        {
            //Set business calendar 
            int MinDayLeadTime = 0;

            DateTime DeadLineDate = DateTime.Today;
            DateTime DeadLineNextFiscal = DateTime.Today;
            int FormID = 0;
            if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

            if (FormID > 0)
            {
                AdjustNbDayLeadTime(FormID);
                MinDayLeadTime = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(FormSectionType.STANDARD_PRODUCT);
            }

          //  DateTime orderDate = Convert.ToDateTime(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
            BusinessCalendarSystem calSys = new BusinessCalendarSystem();

            FormSystem formSystem = new FormSystem();
            FormTable formTable = formSystem.SelectOne(FormID);

            // TODO: Replace the hardcoded Form Group ID with the form type when available
            if ((int)formTable.Rows[0][FormTable.FLD_FORM_GROUP_ID] == 37)
            {
                DeadLineDate = calSys.GetNextBusinessDay(DateTime.Now, MinDayLeadTime);

                e.IsValid = DeliveryDate >= DeadLineDate;
            }

            if (e.IsValid && FormID == ShutdownFormID)
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
            else if (e.IsValid && DeliveryDate > DeadLineNextFiscal)
            {
                e.IsValid = false;
                custDeliveryDateValidator.ErrorMessage = "Delivery dates in the next fiscal year are only accepted within that fiscal year as products are subject to change.";
            }
        }
        private void AdjustNbDayLeadTime(int FormID)
        {
            BusinessRuleSystem ruleSys = new BusinessRuleSystem();
            ruleSys.SetMinNbDayLeadTime(dtsOrder, FormID, FormSectionType.STANDARD_PRODUCT);

        }
        private void Refresh_NbDayLeadTime()
        {
            //Delivery Date Shipping
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            compVal_DeliveryDate.Validate();
            custDeliveryDateValidator.Validate();
            lblDayLeadTime.Text = "";
            int FormID = 0;
            if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            if (compVal_DeliveryDate.IsValid && custDeliveryDateValidator.IsValid && (txtDeliveryDate.Text.Trim().Length > 0))
            {
                DateTime deliveryDate = Convert.ToDateTime(txtDeliveryDate.Text);
                int NbDayLeadTime = 3;
                DateTime orderDate = Convert.ToDateTime(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_DATE]);
                BusinessCalendarSystem calSys = new BusinessCalendarSystem();

                NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate, ShutDownStartDates, ShutDownEndDates, FormID, ShutdownFormID);
                //deliveryDate = calSys.GetNextBusinessDay(orderDate, NbDayLeadTime);

                //lblDayLeadTime.Text = NbDayLeadTime.ToString();
                lblDayLeadTime.Text = (NbDayLeadTime + 1).ToString();
                OrderExceptionList.SelectedLeadTime = NbDayLeadTime + 1;

                //this.lblBusinessMessage.Text = "HELLO!!! ";

                if (dtblShipmentGroup.Rows.Count > 0)
                {
                    dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE] = deliveryDate;
                    dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_DELIVERY_NLT] = deliveryDate;
                }
            }
        }

        protected void AuditBindForm()
        {
            if (dTblAudit != null)
            {
                if (dTblAudit.Rows.Count > 0)
                {
                    DataRow row = dTblAudit.Rows[0];

                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_CREATE_DATE) > -1)
                    {
                        lblCreateDate.Text = row[CommonTable.FLD_CREATE_DATE].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_CREATE_LAST_NAME) > -1)
                    {
                        lblCreateName.Text = row[CommonTable.FLD_CREATE_LAST_NAME].ToString() + " " + row[CommonTable.FLD_CREATE_FIRST_NAME].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_UPDATE_DATE) > -1)
                    {
                        lblUpdateDate.Text = row[CommonTable.FLD_UPDATE_DATE].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_UPDATE_LAST_NAME) > -1)
                    {
                        lblUpdateName.Text = row[CommonTable.FLD_UPDATE_LAST_NAME].ToString() + " " + row[CommonTable.FLD_UPDATE_FIRST_NAME].ToString();
                    }
                }
            }
        }
        protected void OrderInfoBindForm()
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

            int AccID = Convert.ToInt32(row[AccountTable.FLD_PKID].ToString());

            //Account Exceptions
            tblAccountException.Visible = true;
            AccountExceptionList.EntityID = AccID;
            AccountExceptionList.EntityTypeID = QSPForm.Common.EntityType.TYPE_ACCOUNT; //Account
            AccountExceptionList.DataSource = dtsAccount.AccountException;
            AccountExceptionList.DataBind();

            //int OrderID = Convert.ToInt32(dtblOrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);

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
            txtFMID.Text = ordRow[OrderHeaderTable.FLD_FM_ID].ToString();
            txtFMName.Text = ordRow[OrderHeaderTable.FLD_FM_NAME].ToString();


            //Order Type
            lblOrderTypeName.Text = ordRow[OrderHeaderTable.FLD_ORDER_TYPE_NAME].ToString();

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

            //Delivery Date Shipping			
            DateTime deliveryDate = DateTime.Today;

            DateTime orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
            lblOrderDate.Text = orderDate.ToLongDateString() + " " + orderDate.ToShortTimeString();

            int NbDayLeadTime = 3;
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            int ShipGrpID = 0;

            DualAddressFormControl.DataBind();

            if (dtblShipmentGroup.Rows.Count > 0)
            {
                //Delivery Date and Nb Day Lead Time
                DataRow shipRow = dtblShipmentGroup.Rows[0];
                ShipGrpID = Convert.ToInt32(dtblShipmentGroup.Rows[0][ShipmentGroupTable.FLD_PKID]);

                //Bill To Address -- Read Only
                AddressInfo_Billing.ParentID = OrderID;
                AddressInfo_Billing.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_BILLING; //Order Billing
                AddressInfo_Billing.DataSource = dtsOrder;
                AddressInfo_Billing.FilterTypeAddress = PostalAddressType.TYPE_BILLING;
                AddressInfo_Billing.HideTypeAddress = true;
                AddressInfo_Billing.HideTitleAddress = true;
                AddressInfo_Billing.DataBind();

                //Ship To Address -- Read Only
                AddressInfo_Shipping.ParentID = ShipGrpID;
                AddressInfo_Shipping.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_SHIPPING; //Order Shipping
                AddressInfo_Shipping.DataSource = dtsOrder;
                AddressInfo_Shipping.FilterTypeAddress = PostalAddressType.TYPE_SHIPPING;
                AddressInfo_Shipping.HideTypeAddress = true;
                AddressInfo_Shipping.HideTitleAddress = true;
                AddressInfo_Shipping.DataBind();

                int FormID = 0;
                FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE))
                {
                    deliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);
                    NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate, ShutDownStartDates, ShutDownEndDates, FormID, ShutdownFormID);

                    if (this.isDateUsed)
                    {
                        lblDeliveryDate.Text = deliveryDate.ToLongDateString();
                        txtDeliveryDate.Text = deliveryDate.ToShortDateString();

                        if (this.isTimeUsed)
                        {
                            if (shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME))
                            {
                                this.trRequestedDeliveryTime.Visible = false;
                            }
                            else
                            {
                                this.trRequestedDeliveryTime.Visible = true;

                                DateTime deliveryTime = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME]);
                                lblDeliveryTime.Text = deliveryTime.ToString("HH:mm");
                                txtDeliveryTime.Text = deliveryTime.ToString("HH:mm");
                            }
                        }
                        else
                        {
                            this.trRequestedDeliveryTime.Visible = false;
                        }
                    }
                    else
                    {
                        // Set value in text
                        FormSystem formSystem = new FormSystem();
                        WeekOfItem weekOfItem = formSystem.GetWeekOfItem(FormID, deliveryDate);
                        lblDeliveryDate.Text = weekOfItem.Description;

                        // Fill values in drop down list
                        if (this.ddlRequestedDeliveryDateDropDownList.Items.Count > 0)
                        {
                        }
                        else
                        {
                            List<WeekOfItem> weekOfItemList = formSystem.GetWeekOfOptionList(FormID);

                            this.ddlRequestedDeliveryDateDropDownList.Items.Clear();
                            foreach (WeekOfItem weekOfItem2 in weekOfItemList)
                            {
                                ListItem newListItem = new ListItem();

                                newListItem.Value = weekOfItem2.StartDate.Ticks.ToString();
                                newListItem.Text = weekOfItem2.Description;

                                this.ddlRequestedDeliveryDateDropDownList.Items.Add(newListItem);
                            }
                        }

                        // Set value in drop down list
                        foreach (ListItem item in ddlRequestedDeliveryDateDropDownList.Items)
                        {
                            //if (item.Value == deliveryDate.Ticks.ToString())
                            if (item.Value == weekOfItem.StartDate.Ticks.ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }

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
              
                //Delivery Method
                if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_METHOD_ID))
                {
                    ListItem lstItem = ddlDeliveryMethod.Items.FindByValue(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID].ToString());

                    if (lstItem != null)
                    {
                        ddlDeliveryMethod.ClearSelection();
                        lstItem.Selected = true;
                    }
                }

                #endregion

                #region Warehosue

                int deliveryMethodID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);

                if (deliveryMethodID == DeliveryMethod.PICK_UP_AT_WAREHOUSE)
                {
                    trWarehouseSelector.Visible = true;

                    bool isWarehosueSelectable = false;
                    int defaultWarehouseId = 0;

                    #region Get warehouse info from form

                    QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(FormID);

                    isWarehosueSelectable = form.IsWarehouseSelectable;
                    defaultWarehouseId = Convert.ToInt32(form.DefaultWarehouseId);

                    #endregion

                    #region Set read only info

                    if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID))
                    {
                        txtFulfWarehouseID.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID].ToString();
                        txtWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();
                        
                        //lblWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();
                        lblWarehouseName.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID].ToString() + "&nbsp;-&nbsp; " + shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();
                        Label76.Text = shipRow[ShipmentGroupTable.FLD_DELIVERY_FULF_WAREHOUSE_ID].ToString() + "&nbsp;-&nbsp; " + shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_NAME].ToString();

                        string sID = shipRow[ShipmentGroupTable.FLD_DELIVERY_WAREHOUSE_ID].ToString();
                        clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnWarehouse, "WarehouseDetailInfo.aspx?", "WareID", sID, 650, 600);
                    }

                    #endregion

                    if (isWarehosueSelectable)
                    {
                        #region  Enable the select warehouse list

                        clsUtil.SetJScriptForOpenSelector(imgBtnSelectWarehouse, txtFulfWarehouseID, txtWarehouseName, "WarehouseSelector.aspx", "WareHouseSelector", 750, 700, "&FormID=" + FormID.ToString());

                        imgBtnSelectWarehouse.Visible = true;

                        this.txtFulfWarehouseID.ReadOnly = false;
                        this.txtWarehouseName.ReadOnly = false;

                        #endregion
                    }
                    else
                    {
                        #region Set the default warehouse info

                        QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(defaultWarehouseId);

                        imgBtnSelectWarehouse.Visible = false;

                        this.txtFulfWarehouseID.ReadOnly = true;
                        this.txtWarehouseName.ReadOnly = true;

                        this.txtFulfWarehouseID.Text = warehouse.FulfWarehouseId.ToString();
                        this.txtWarehouseName.Text = warehouse.WarehouseName.Trim();

                        #endregion
                    }

                
                }
                else
                {
                    trWarehouseInfo.Visible = false;
                    trWarehouseSelector.Visible = false;
                    trWareHouseReadOnly.Visible = false;
                }




                //Page.RegisterStartupScript("ShowHideWarehouseSelector", "<script type=\"text/javascript\">try { ShowHideWarehouseSelector(); } catch (err) { }</script>");

                #endregion

            
            }



            //Customer PO#
            txtCustomerPO_Number.Text = ordRow[OrderHeaderTable.FLD_CUSTOMER_PO_NUMBER].ToString();
            lblCustomerPO_Number.Text = ordRow[OrderHeaderTable.FLD_CUSTOMER_PO_NUMBER].ToString();
            //Order Comment
            txtComment.Text = ordRow[OrderHeaderTable.FLD_COMMENTS].ToString();
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
                trOrderInfo_Optional.Visible = true;
            }
            else
            {
                trOrderInfo_Optional.Visible = false;
            }

            // Supply Form
            SupplyForm.DataSource = dtsOrder;

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
                    lblShipSupplyNbDayLeadTime.Text = (NbDayLeadTime + 1).ToString();

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
                        AddressControlInfo_Supply.ParentType = EntityType.TYPE_ORDER_SHIPPING;
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
            else
            {
                trOrderException.Visible = false;
                tblOrderException.Visible = false;
            }

            //profit rate
            if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
            {
                Decimal pr = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
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

        
        

        protected void OrderDetailBindForm()
        {
            OrderDetailSectionListStep.FormSectionTypeID = FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = dtsOrder;
            OrderDetailSectionListStep.DisableQtyValidator = this.IsPEForm;
            OrderDetailSectionListStep.BindForm();

            if (!dtsOrder.OrderDetail.IsContainFormSectionType(FormSectionType.OTHER_PRODUCT))
            {
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                lblNonAvailableOptionalSectionType.Visible = true;
                OrderDetailSectionListStep_Optional.Visible = false;
            }
            else
            {
                lblNonAvailableOptionalSectionType.Visible = false;
                OrderDetailSectionListStep_Optional.Visible = true;
                OrderDetailSectionListStep_Optional.FormSectionTypeID = FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.DataSource = dtsOrder;
                OrderDetailSectionListStep_Optional.BindForm();
            }
        }

        /// <summary>
        /// Function to display Read Only Rows Only
        /// </summary>
        protected void OrderInfoVisible()
        {
            trOrderDetail_Standard.Visible = false;
            trOrderDetaillabel.Visible = false;
            trCommentsDetail.Visible = false;
            trCustomerPODetail.Visible = false;
            trDeliveryDetail.Visible = false;
            ddlDeliveryMethod.Enabled = false;
            // ddlOrderType.Enabled = false;
            trFmDetail.Visible = false;
            trShippingDetail.Visible = false;
            trRequiredField.Visible = false;
            trOrderTypeEdit.Visible = false;
            trDeliveryMethodEdit.Visible = false;
            trAccountNameEdit.Visible = false;

            if (trWarehouseSelector.Visible == true)
            {
                trWarehouseSelector.Visible = false;
            }

            trAccountNameInfo.Visible = true;
            trOrderTypeReadOnly.Visible = true;
            trDeliveryMethodReadOnly.Visible = true;
            trAccountException.Visible = true;
            trShippingReadOnly.Visible = true;
            trOrderInfo_Standard.Visible = true;
            trOrderSummary.Visible = true;
            trFmInfo.Visible = true;
            trCustomerPOInfo.Visible = true;
            trCommentsInfo.Visible = true;
            trOrderException.Visible = true;

            #region requested delivery info

            // Disable edit sections
            trDeliveryDetail.Visible = false;               // requested delivery date edit
            trRequestedDeliveryTimeEdit.Visible = false;    // requested delivery time edit

            // Enable read only sections
            trDeliveryInfo.Visible = true;                  // requested delivery date read only
            
            if (this.isTimeUsed)
            {
                trRequestedDeliveryTime.Visible = true;     // requested delivery time read only
            }
            else
            {
                trRequestedDeliveryTime.Visible = false;    // requested delivery time read only
            }

            #endregion

            // FillList();
        }

        /// <summary>
        /// Function to display Edit Mode
        /// </summary>
        protected void OrderDetailVisible()
        {
            // Make Order Detail Visible
            trOrderDetail_Standard.Visible = true;
            trOrderDetaillabel.Visible = true;
            trCommentsDetail.Visible = true;
            trCustomerPODetail.Visible = true;
            trDeliveryDetail.Visible = true;
            ddlDeliveryMethod.Enabled = true;
            trFmDetail.Visible = true;
            trShippingDetail.Visible = true;
            trRequiredField.Visible = true;
            trStatusMessage.Visible = true;
            trAccountNameEdit.Visible = true;

            if (trOrderException.Visible == true)
            {
                trOrderException.Visible = false;
            }

            if (trWareHouseReadOnly.Visible == true)
            {
                trWarehouseInfo.Visible = false;
                trWarehouseSelector.Visible = true;
                trWareHouseReadOnly.Visible = false;
            }

            // Make Order Info Invisible
                        
            trAccountNameInfo.Visible = false;
            trAccountException.Visible = false;
            trAudit.Visible = false;
            trOrderSummary.Visible = false;
            trOrderInfo_Standard.Visible = false;
            trOrderInfo_Optional.Visible = false;
            trFmInfo.Visible = false;
            trDeliveryInfo.Visible = false;
            trCustomerPOInfo.Visible = false;
            trCommentsInfo.Visible = false;
            trShippingReadOnly.Visible = false;
            trReadOnlyMode.Visible = false;
            trChargeList.Visible = false;
            #region requested delivery info

            // Disable read only sections
            trDeliveryInfo.Visible = false;             // requested delivery date read only
            trRequestedDeliveryTime.Visible = false;    // requested delivery time read only

            // Enable edit sections
            trDeliveryDetail.Visible = true;            // requested delivery date edit

            if (this.isDateUsed)
            {
                // Enable calendar
                trRequestedDeliveryDate.Visible = true;
                trRequestedDeliveryOptionList.Visible = false;
            }
            else
            {
                // Enable drop down list
                trRequestedDeliveryDate.Visible = false;
                trRequestedDeliveryOptionList.Visible = true;
            }

            if (this.isTimeUsed)
            {
                trRequestedDeliveryTimeEdit.Visible = true;
            }
            else
            {
                trRequestedDeliveryTimeEdit.Visible = false;
            }

            #endregion

            FillList();
        }

        /// <summary>
        /// Order Summary Data processing
        /// </summary>
        protected void OrderSummaryBindForm()
        {
            //Order Summary
            QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
            orderSys.CalculateOrder(dtsOrder);
            DataRow ordHeader = dtsOrder.OrderHeader.Rows[0];

            lblSubTotal.Text = "0";
            lblTaxRate.Text = "0";
            lblShippingCharges.Text = "0";
            lblSurcharges.Text = "0";
            lblTaxRate.Text = "0";
            lblTaxRate.Text = "0";

            if (ordHeader[OrderHeaderTable.FLD_TOTAL_AMOUNT] != DBNull.Value)
            {
                lblSubTotal.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_AMOUNT]).ToString("C");
            }
            if (ordHeader[OrderHeaderTable.FLD_TAX_RATE] != DBNull.Value)
            {
                lblTaxRate.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TAX_RATE]).ToString("P");
            }
            if (ordHeader[OrderHeaderTable.FLD_TOTAL_TAX_AMOUNT] != DBNull.Value)
            {
                lblTaxAmount.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_TAX_AMOUNT]).ToString("C");
            }
            if (ordHeader[OrderHeaderTable.FLD_TOTAL_SHIP_FEES] != DBNull.Value)
            {
                lblShippingCharges.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_TOTAL_SHIP_FEES]).ToString("C");
            }
            // Setting FLD_TOTAL_CHARGES automatically updates FLD_GRAND_TOTAL.
            lblSurcharges.Text = ChargeList1.TotalChargeAmount.ToString("C");
            ordHeader[OrderHeaderTable.FLD_TOTAL_CHARGES] = ChargeList1.TotalChargeAmount;
            if (ordHeader[OrderHeaderTable.FLD_GRAND_TOTAL] != DBNull.Value)
            {
                lblGrandTotal.Text = Convert.ToDecimal(ordHeader[OrderHeaderTable.FLD_GRAND_TOTAL]).ToString("C");
            }
        }


        private void FillList()
        {
            //By default
            trDeliveryMethodEdit.Visible = true;
            trDeliveryMethodReadOnly.Visible = false;
            trOrderTypeEdit.Visible = true;
            trOrderTypeReadOnly.Visible = false;
            DataRow row = dtsOrder.OrderHeader.Rows[0];
            int FormID = 0;
            if (!row.IsNull(OrderHeaderTable.FLD_FORM_ID))
                FormID = Convert.ToInt32(row[OrderHeaderTable.FLD_FORM_ID]);
            if (FormID > 0)
            {
                clsUtil.SetDeliveryMethodDropDownList(ddlDeliveryMethod, true, FormID);
                if (ddlDeliveryMethod.Items.Count <= 2)
                {
                    string sCommonCarrierName = "";
                    BusinessRuleSystem bizRuleSys = new BusinessRuleSystem();
                    sCommonCarrierName = bizRuleSys.GetCommonCarrierName(FormID);
                    if (sCommonCarrierName.Length == 0)
                    {
                        ddlDeliveryMethod.SelectedIndex = 1;
                        sCommonCarrierName = ddlDeliveryMethod.SelectedItem.Text;
                    }
                    lblDeliveryMethodName.Text = sCommonCarrierName;
                    ddlDeliveryMethod.SelectedIndex = 1;
                    trDeliveryMethodEdit.Visible = false;
                    trDeliveryMethodReadOnly.Visible = true;
                }

                clsUtil.SetOrderTypeDropDownList(ddlOrderType, true, FormID);
                if (ddlOrderType.Items.Count <= 2)
                {
                    trOrderTypeEdit.Visible = false;
                    ddlOrderType.SelectedIndex = 1;
                    lblOrderTypeName.Text = ddlOrderType.SelectedItem.Text;
                    trOrderTypeReadOnly.Visible = true;
                }
            }
            else
            {
                clsUtil.SetDeliveryMethodDropDownList(ddlDeliveryMethod, true);
            }

            SetDeliveryMethod();
        }
        private void SetDeliveryMethod()
        {
            //OrderHeader 
            OrderHeaderTable dtblOrderHeader = dtsOrder.OrderHeader;
            //Shipment Info
            ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
            //Order Information
            DataRow ordRow = dtblOrderHeader.Rows[0];


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

            if (dtblShipmentGroup.Rows.Count > 0)
            {
                //Delivery Date 
                DataRow shipRow = dtblShipmentGroup.Rows[0];
                //Delivery Method
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
                        trWarehouseSelector.Visible = true;
                    }
                }
            }


        }
        private void SetMessageCancelOrder()
        {
            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Cancel this Order";
            if (imgBtnCancelOrder != null)
                imgBtnCancelOrder.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");
        }

        /// <summary>
        /// Setting up PEORder visibility
        /// </summary>
        private void SetImgPEOrder()
        {

            //Set the Initial Mode
            string sMessage = "";
            this.imgEditOrder.Visible = false;
            this.imgEditOrderPE.Visible = false;
            // RNKORDER           
            //if (this.Page.Role == AuthSystem.ROLE_FIELD_SUPPORT || this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR)                // ( this.Page.RightUpdate)
            if (this.Page.Role >= AuthSystem.ROLE_FM)
            {
                //Access Base on Program Type
                if (dtsAccount.Campaign.Rows.Count > 0)
                {
                    int ProgTypeID = 0;
                    if (!dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                        ProgTypeID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PROG_TYPE_ID]);

                    if (ProgTypeID == 11 || ProgTypeID == 7) //if it's a WFC Program Account or a Food Program
                    {
                        this.imgEditOrder.Visible = false;

                        //Access Base on Order Status
                        int OrderStatus = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID]);

                        if ((OrderStatus < QSPForm.Common.OrderStatus.RELEASED) || (OrderStatus >= QSPForm.Common.OrderStatus.ERROR_UNSPECIFIED))
                        {
                            if ((OrderStatus != QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) && (OrderStatus != QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION))
                            {
                                if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT)
                                {
                                    this.imgEditOrder.Visible = true;

                                }
                                else
                                {
                                    //Access Base on FM
                                    if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
                                    {
                                        string fmID = dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FM_ID].ToString();
                                        if (fmID == this.Page.FMID)
                                        {
                                            //this.Page.SetPageMessage("The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.");
                                            this.imgEditOrder.Visible = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sMessage = "The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.";
                                this.imgEditOrder.Visible = true;//The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.
                            }
                        }
                        else
                        {
                            sMessage = "Due to the status of this order, it cannot be edited.";
                        }

                        //Add logic for Order that contains PE Product.
                        //if (this.imgEditOrder.Visible)
                    if (this.Page.Role > QSPForm.Business.AuthSystem.ROLE_FM) 
                        {
                            //if it's still editable based on the logic of the standard Ordfer Form
                            OrderSystem ordSys = new OrderSystem();
                            bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(dtsOrder);
                            //Only Apply this rule when the order contains PE
                            if (IsOrderContainsPE)
                            {
                                //Eric Charest, temporay disable PE Edition for fm
                                if (this.Page.Role > AuthSystem.ROLE_FM)
                                    this.imgEditOrder.ImageUrl = IMG_EDIT_ORDER_AND_OR_PERSONALIZATION_URL;

                                int fulfOrderID = 0;
                                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID))
                                {
                                    if (dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID].ToString().Length > 0)
                                        fulfOrderID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID]);
                                }

                                if ((OrderStatus < QSPForm.Common.OrderStatus.IN_PROCESS) && (fulfOrderID == 0))
                                {
                                    this.imgEditOrder.Visible = true;
                                    this.imgEditOrderPE.Visible = true;
                                    this.imgEditOrderPE.ImageUrl = IMG_EDIT_PERSONALIZATION_URL;
                                }
                                else
                                {
                                    this.imgEditOrder.Visible = false;
                                    this.imgEditOrderPE.Visible = true;
                                    this.imgEditOrderPE.ImageUrl = IMG_VIEW_PERSONALIZATION_URL;
                                }
                            }
                        }
                    }
                    else
                    {
                        sMessage = "Because this order did not originate in Order Express, it cannot be edited.";
                    }
                }
            }

            if (imgEditOrder.Visible && dtsOrder.OrderSupply.TotalQuantity > 0)
                imgEditOrder.Visible = false;

            if (!this.imgEditOrder.Visible)
            {
                if (sMessage.Length == 0)
                {
                    sMessage = "This order is not editable.";
                }
            }

            this.Page.SetPageMessage(sMessage);
            Label72.Text = sMessage;
            // this.trStatusMessage.Visible = true;
        }

        private bool SaveAllDetail()
        {
            bool blnValid = false;

            try
            {
                QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
                blnValid = orderSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            }
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }

            return blnValid;
        }
        private bool ValidateForm()
        {
            Boolean blnValid = true;
            blnValid = HeaderFormValidateForm();
            if (!blnValid)
            {
                return false;
            }
            if (IsPEForm)
                OrderDetailSectionListStep.PreparePEForm();
            blnValid = OrderDetailSectionListStep.ValidateForm();
            if (!blnValid)
            {
                return false;
            }
            if (dtsOrder.OrderDetail.IsContainFormSectionType(FormSectionType.OTHER_PRODUCT))
            {
                blnValid = OrderDetailSectionListStep_Optional.ValidateForm();
                if (!blnValid)
                {
                    return false;
                }
            }
            blnValid = SupplyForm.ValidateForm();
            if (!blnValid)
            {
                return false;
            }

            return blnValid;

        }
        public bool HeaderFormValidateForm()
        {
            bool isValid = true;
            trValSumOrderInfo.Visible = false;

            isValid = (isValid && DualAddressFormControl.IsValid());

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

        public bool HeaderFormUpdateDataSource()
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

            int FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

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

                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, txtDeliveryDate.Text);
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME, txtDeliveryTime.Text);
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

                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE, selectedWeekOfStartDate.ToShortDateString());
                    clsUtil.UpdateRow(dtsOrder.ShipmentGroup.Rows[0], ShipmentGroupTable.FLD_REQUESTED_DELIVERY_TIME, selectedWeekOfStartDate.ToShortTimeString());
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
        private bool UpdateDataSource()
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
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }

            return true;
        }
        private bool OrderDetailUpdateDataSource()
        {
            Boolean blnValid = true;
            try
            {
                blnValid = HeaderFormUpdateDataSource();
                if (!blnValid)
                {
                    return false;
                }
                blnValid = OrderDetailSectionListStep.UpdateDataSource();
                if (!blnValid)
                {
                    return false;
                }

                if (dtsOrder.OrderDetail.IsContainFormSectionType(FormSectionType.OTHER_PRODUCT))
                {
                    blnValid = OrderDetailSectionListStep_Optional.UpdateDataSource();
                    if (!blnValid)
                    {
                        return false;
                    }
                }
                blnValid = SupplyForm.UpdateDataSource();
                if (!blnValid)
                {
                    return false;
                }

                if (dtsOrder.OrderHeader.Rows.Count > 0)
                {
                    DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                    if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID))
                    {
                        int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                        AccountSystem accSys = new QSPForm.Business.AccountSystem();
                        dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                        //Do the Business Validation for the Account
                        accSys.PerformValidation(dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                    }

                    //Do the Business Validation for the Order
                    //We have to change the Status to In Process
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = OrderStatus.IN_PROCESS;
                    OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                    ordSys.CalculateTax(dtsOrder, dtsAccount, this.Page.UserID);
                    ordSys.PrePerformValidation(dtsOrder, dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                    ordSys.SetExpeditedFreightChargeRequirement(dtsOrder, this.Page.Role);
                }
            }
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }

            return blnValid;
        }
        private void ConfirmOrder()
        {
            OrderSystem ordSys;

            Boolean blnValid = true;
            blnValid = ValidateForm();
            if (!blnValid)
            {
                return;
            }

            OrderExceptionList.IsReadOnly = false;
            blnValid = OrderDetailUpdateDataSource();
            if (!blnValid)
            {
                return;
            }

            OrderInfoBindForm();
            OrderSummaryBindForm();
            trButtonConfirm.Visible = true;
            trButtonValidate.Visible = false;
            trChargeList.Visible = false;

            // Remove surcharges as they are not updated until order is saved to the database.
            lblSurcharges.Text = "Calculated on next page";
            dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_TOTAL_CHARGES] = 0;
            lblGrandTotal.Text = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_GRAND_TOTAL]).ToString("C");

            OrderInfoVisible();

            // Ben - 03/26/2007:
            // Button's text should be changed depending on the PE status
            ordSys = new OrderSystem();
            if (ordSys.IsOrderContainsPEProduct(dtsOrder))
            {
                if (dtsOrder.OrderDetail.IsPersonalizeComplete)
                {
                    imgBtnProceed.OnClientClick = "document.getElementById(\"" + hidEditPersonalization.ClientID + "\").value = Number(confirm(\"" + GO_TO_PERSONALIZATION_MSG + "\"));\n";
                    imgBtnSaveForLater.OnClientClick = "document.getElementById(\"" + hidEditPersonalization.ClientID + "\").value = Number(confirm(\"" + GO_TO_PERSONALIZATION_MSG + "\"));\n";
                }
                else
                {
                    //Eric Charest
                    //temporary disable pe edition for fm
                    if (this.Page.Role > AuthSystem.ROLE_FM)
                    {
                        imgBtnProceed.ImageUrl = IMG_SUBMIT_AND_PERSONALIZE_URL;
                        imgBtnSaveForLater.ImageUrl = IMG_SAVE_AND_PERSONALIZE_URL;
                    }
                }
            }
        }

        #region Page events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DualAddressFormControl.AddressHygieneConfirmed += new EventHandler(DualAddressFormControl_AddressHygieneConfirmed);
        }
        public void InitializeControls()
        {
            DualAddressFormControl.BillingParentID = OrderID;
            DualAddressFormControl.ShippingParentID = ShipmentGroupID;
            DualAddressFormControl.BillingParentType = EntityType.TYPE_ORDER_BILLING;
            DualAddressFormControl.ShippingParentType = EntityType.TYPE_ORDER_SHIPPING;
            DualAddressFormControl.DataSource = dtsOrder;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitializeControls();
                if (!IsPostBack)
                {
                    SetHeaderTitle();
                    OrderInfoBindForm();
                    AuditBindForm();
                    OrderSummaryBindForm();
                    OrderDetailBindForm();
                    OrderInfoVisible();

                    if (ReadOnlyStatusChanged != null)
                    {
                        ReadOnlyStatusChanged(this, new ReadOnlyStatusChangedEventArgs(true));
                    }

                    //If no supply is entered we don't show the section by default
                    //trOrderSupplyOrderDetail.Visible = (dtsOrder.OrderSupply.TotalQuantity > 0);
                    trOrderSupplyOrderDetail.Visible = false;
                    trOrderSupply.Visible = false;
                    imgEditOrder.Visible = (dtsOrder.OrderSupply.TotalQuantity == 0);

                    int OrderStatusID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                    int fulfOrderID = 0;

                    if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID))
                    {
                        if (dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID].ToString().Length > 0)
                            fulfOrderID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID]);
                    }

                    imgBtnSaveForLater.Visible = ((OrderStatusID < QSPForm.Common.OrderStatus.IN_PROCESS) && (fulfOrderID == 0));

                    if ((OrderStatusID == QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) || (OrderStatusID == QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION))
                    {
                        imgBtnRollBack.Visible = true;
                        imgBtnCancelOrder.Visible = false;
                        imgBtnValidate.Visible = false;
                        //this.Page.SetPageMessage("The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Rollback to undo changes.");
                        Label72.Text = "The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Rollback to undo changes.";
                        // this.trStatusMessage.Visible = true;
                    }
                    else
                    {
                        imgBtnRollBack.Visible = false;
                        imgBtnCancelOrder.Visible = true;
                        imgBtnValidate.Visible = true;
                    }

                }

                SetImgPEOrder();
                SetMessageCancelOrder();

                if (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT)
                {
                    //clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
                    clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "FMSelector", 0, 0, null);
                }

                int FormID = 0;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

                if (this.Page.Role >= AuthSystem.ROLE_FM)
                {
                    // clsUtil.SetJScriptForOpenSelector(imgBtnSelectWarehouse, txtFulfWarehouseID, txtWarehouseName, QSPForm.Business.AppItem.WarehouseSelector, 750, 700, "&FormID=" + FormID.ToString());
                    clsUtil.SetJScriptForOpenSelector(imgBtnSelectWarehouse, txtFulfWarehouseID, txtWarehouseName, "WarehouseSelector.aspx", "WareHouseSelector", 750, 700, "&FormID=" + FormID.ToString());
                }

                //Set business calendar 
                int MinDayLeadTime = 0;
                if (FormID > 0)
                {
                    BusinessRuleSystem ruleSys = new BusinessRuleSystem();
                    ruleSys.SetMinNbDayLeadTime(dtsOrder, FormID, FormSectionType.STANDARD_PRODUCT);

                    MinDayLeadTime = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(FormSectionType.STANDARD_PRODUCT);
                }
                clsUtil.SetJScriptForOpenCalendar(hypLnkDeliveryDate, txtDeliveryDate, lblDayLeadTime, true, lblOrderDate, null, MinDayLeadTime, FormID);

                if (c_ParentTypeAudit == EntityType.TYPE_ORDER_BILLING)
                {
                    // clsUtil.SetJScriptForOpenDetail(imgBtnViewHistory, AppItem.OrderStatusChangeList, "OrderID", c_ParentID.ToString(), 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnViewHistory, "OrderStatusChangeList.aspx?", "OrderID", c_ParentIDAudit.ToString(), 0, 0);
                }

                //Account Information
                string AccID = dtsAccount.Account.Rows[0][AccountTable.FLD_PKID].ToString();

                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnDetailAccount, "AccountDetailInfo.aspx?", "AccID", AccID, 0, 0, "OnClick");

                FormSystem formSys = new QSPForm.Business.FormSystem();

                QSPForm.Common.DataDef.FormTable dTblForm = formSys.SelectOne(FormID);
                if (dTblForm.Rows.Count > 0)
                {
                    DataRow formRow = dTblForm.Rows[0];

                    //                    PrintFormReport.ReportName = formRow[FormTable.FLD_FORM_NAME].ToString();
                    string formName = formRow[FormTable.FLD_FORM_NAME].ToString();
                    string replaces = ";?:@&=+$/";

                    for (int i = 0; i < replaces.Length; i++)
                    {
                        formName = formName.Replace(replaces[i].ToString(), "");
                    }

                    PrintFormReport.ReportName = formName;
                }

                //Eric Charest, temporary disable PE Edition for FM
                if (this.Page.Role <= AuthSystem.ROLE_FM)
                {
                    this.imgEditOrderPE.Visible = false;
                }

                ShutdownString = clsUtil.GetShutDownForm(FormID, ref ShutdownFormID);
                if (ShutdownString != null)
                {
                    ShutDownStartDates = clsUtil.GetShutDownStartDate(ShutdownString);
                    ShutDownEndDates = clsUtil.GetShutDownEndDate(ShutdownString);
                }

                // CHR CODE
                // FSMs cannot edit orders that are pending approval
                if (Page.Role < AuthSystem.ROLE_FIELD_SUPPORT)
                {
                    int orderStatusId = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][8]);

                    if (orderStatusId == 5)
                    {
                        this.imgEditOrder.Visible = false;
                    }
                }


                //if (Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_WAREHOUSE_TYPE_ID]) == WarehouseType.CHRobinson)
                //{
                //    this.lblBusinessMessage.Text = "CHR!!";
                //}

            }
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }
        }
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            this.ViewState[ORDER_DATA] = dtsOrder;
            this.ViewState[ACCOUNT_DATA] = dtsAccount;

        }

        protected void imgBtnCancelOrder_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                bool IsCancel = false;
                OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                IsCancel = ordSys.SetCancelStatus(dtsOrder, this.Page.UserID);
                if (IsCancel)
                {
                    if (SaveAllDetail())
                    {

                        //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, OrderDetailInfo.ORDER_ID, c_OrderID.ToString());
                        //Response.Redirect(url, false);

                        string url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", c_OrderID);
                        Response.Clear();
                        Response.Redirect(url);
                    }
                }
                else
                {
                    Page.SetPageMessage("This Order cannot be cancelled.");
                    Label72.Text = "This Order cannot be cancelled.";
                    // this.trStatusMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }
        }
        protected void imgBtnRollBack_Click(object sender, ImageClickEventArgs e)
        {
            //Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_STATUS_ID))
            {
                int OrderStatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                if ((OrderStatusID == QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) || (OrderStatusID == QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION))
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.ERROR_WAITING_ROLLBACK;

                if (SaveAllDetail())
                {
                    string url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", c_OrderID);
                    Response.Redirect(url);
                }
            }
        }

        protected void imgBtnShowSupply_Click(object sender, ImageClickEventArgs e)
        {
            trOrderSupplyOrderDetail.Visible = false;
        }
        protected void imgBtnHideSupply_Click(object sender, ImageClickEventArgs e)
        {
            trOrderSupplyOrderDetail.Visible = false;
        }

        protected void imgBtnSaveForLater_Click(object sender, ImageClickEventArgs e)
        {
            // This is the save button
            OrderSystem ordSys;
            string url = string.Empty;

            // Clear Exception
            if (dtsOrder.OrderException.Rows.Count > 0)
            {
                dtsOrder.OrderException.Rows.Clear();
            }

            // Set Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = OrderStatus.SAVED_FOR_LATER;

            if (SaveAllDetail())
            {
                #region Generate applicable charges to the saved order

                QSPForm.Business.Communication.Notifications notifications = new QSPForm.Business.Communication.Notifications();
                QSPForm.Business.Controller.OrderController oc = new QSPForm.Business.Controller.OrderController();
                oc.GenerateCharges(c_OrderID, notifications);

                #endregion

                #region Redirection logic

                // Ben - 03/26/2007:
                // Should redirect to personalization if incomplete
                ordSys = new OrderSystem();
                if (ordSys.IsOrderContainsPEProduct(dtsOrder))
                {
                    if (!dtsOrder.OrderDetail.IsPersonalizeComplete)
                    {
                        //   Response.Redirect(this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString()), false);
                        url = "~/OrderStep_Personalization.aspx?&OrderID=" + c_OrderID.ToString();
                        Response.Redirect(url, false);
                    }
                    else
                    {
                        // Response.Redirect(GetPageToGoToFromConfirm(), false);

                        if (EditPersonalization)
                        {
                            //return this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString());
                            url = "~/OrderStep_Personalization.aspx?&OrderID=" + c_OrderID.ToString();
                            Response.Redirect(url, false);

                        }
                        else
                        {
                            // return this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, ORDER_ID, c_OrderID.ToString());
                            url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", c_OrderID);
                            Response.Redirect(url);
                        }
                    }
                }
                else
                {
                    url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", c_OrderID);
                    Response.Redirect(url);
                }

                #endregion
            }
        }
        protected void imgBtnProceed_Click(object sender, ImageClickEventArgs e)
        {
            // This is the submit button

            // Order Status
            // Check if we pass from a "Saved for later" to "In_process" Status
            // Save the approval of Order Exception if it's the case
            bool IsValid = false;
            bool redirectToPersonalization = false;
            string url = String.Empty;

            OrderExceptionList.IsReadOnly = false;

            //. Update the data source
            IsValid = UpdateDataSource();

            if (IsValid)
            {
                #region Get the desired order status for the order

                int desiredOrderStatusId;

                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                desiredOrderStatusId = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                if (desiredOrderStatusId == OrderStatus.SAVED_FOR_LATER)
                {
                    desiredOrderStatusId = OrderStatus.IN_PROCESS;
                }


                OrderSystem ordSys = new OrderSystem();
                bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(dtsOrder);

                if (IsOrderContainsPE)
                {
                    if (!dtsOrder.OrderDetail.IsPersonalizeComplete || EditPersonalization)
                    {
                        desiredOrderStatusId = QSPForm.Common.OrderStatus.WAIT_FOR_PERSONALIZATION;
                        redirectToPersonalization = true;
                    }
                }

                #endregion

                #region Set a temporary status while we generate charges

                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = OrderStatus.SAVED_FOR_LATER;

                #endregion

                if (SaveAllDetail())
                {
                    #region Generate applicable charges to the saved order

                    QSPForm.Business.Communication.Notifications notifications = new QSPForm.Business.Communication.Notifications();
                    QSPForm.Business.Controller.OrderController oc = new QSPForm.Business.Controller.OrderController();
                    oc.GenerateCharges(c_OrderID, notifications);

                    #endregion

                    #region Set the desired status to the order

                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = desiredOrderStatusId;
                    oc.UpdateOrderStatus(c_OrderID, desiredOrderStatusId, notifications);

                    #endregion

                    #region temporary disable pe edition for fm

                    // Eric Charest 

                    if (!redirectToPersonalization || this.Page.Role <= AuthSystem.ROLE_FM)
                    {
                        url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", c_OrderID);
                    }
                    else
                    {
                        // url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString());
                        url = "~/OrderStep_Personalization.aspx?&OrderID=" + c_OrderID.ToString();
                    }

                    #endregion

                    Response.Redirect(url, false);
                }
            }
        }

        protected void imgEditOrder_Click(object sender, ImageClickEventArgs e)
        {
            trButtonValidate.Visible = true;
            OrderExceptionList.IsReadOnly = false;
            OrderDetailVisible();

            if (ReadOnlyStatusChanged != null)
            {
                ReadOnlyStatusChanged(this, new ReadOnlyStatusChangedEventArgs(false));
            }
        }
        protected void imgEditOrderPE_Click(object sender, ImageClickEventArgs e)
        {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderAwardVisionDetail, BaseOrderDetail.ORDER_ID, c_OrderID.ToString());
            //Response.Redirect(url);

            string url = "~/OrderAwardVisionDetail.aspx?";
            Response.Redirect(url + "&" + BaseOrderDetail.ORDER_ID + "=" + c_OrderID.ToString());
        }
        protected void imgBtnBack_Click(object sender, ImageClickEventArgs e)
        {
            trButtonConfirm.Visible = false;
            trButtonValidate.Visible = true;

            OrderDetailVisible();
        }
        protected void imgBtnValidate_Click(object sender, ImageClickEventArgs e)
        {
            // This is the confirm button
            try
            {
                DualAddressFormControl.ResetStatus();

                ConfirmOrder();
            }
            catch (Exception ex)
            {
                Page.SetPageError(ex);
            }
        }

        protected void ddlDeliveryMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            int FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

            if (ddlDeliveryMethod.SelectedIndex == 2)
            {
                trWarehouseSelector.Visible = true;

                bool isWarehosueSelectable = false;
                int defaultWarehouseId = 0;

                #region Get warehouse info from form

                QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(FormID);

                isWarehosueSelectable = form.IsWarehouseSelectable;
                defaultWarehouseId = Convert.ToInt32(form.DefaultWarehouseId);

                #endregion

                if (isWarehosueSelectable)
                {
                    #region  Enable the select warehouse list

                    clsUtil.SetJScriptForOpenSelector(imgBtnSelectWarehouse, txtFulfWarehouseID, txtWarehouseName, "WarehouseSelector.aspx", "WareHouseSelector", 750, 700, "&FormID=" + FormID.ToString());

                    imgBtnSelectWarehouse.Visible = true;

                    this.txtFulfWarehouseID.ReadOnly = false;
                    this.txtWarehouseName.ReadOnly = false;

                    #endregion
                }
                else
                {
                    #region Set the default warehouse info

                    QSP.Business.Fulfillment.Warehouse warehouse = QSP.Business.Fulfillment.Warehouse.GetWarehouse(defaultWarehouseId);

                    imgBtnSelectWarehouse.Visible = false;

                    this.txtFulfWarehouseID.ReadOnly = true;
                    this.txtWarehouseName.ReadOnly = true;

                    this.txtFulfWarehouseID.Text = warehouse.FulfWarehouseId.ToString();
                    this.txtWarehouseName.Text = warehouse.WarehouseName.Trim();

                    #endregion
                }
            }
            else
            {
                trWarehouseInfo.Visible = false;
                trWarehouseSelector.Visible = false;
                trWareHouseReadOnly.Visible = false;
            }
        }
        protected void PrintFormReport_Click(object sender, ImageClickEventArgs e) 
        {
            QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(c_OrderID);

            if (order.FormId != null)
            {
                QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(Convert.ToInt32(order.FormId));

                PrintFormReport.ReportName = form.ReportName;
            }
            else
            {
                PrintFormReport.ReportName = "OrderForm";
            }

            PrintFormReport.ReportParameterDictionary = new Dictionary<string, string>();
            PrintFormReport.ReportParameterDictionary.Add("order_id", order.OrderId.ToString());

            PrintFormReport.ReportMode = FilePageMode.PopUp;
            PrintFormReport.ReportWebPage = "Common/Reporting/RSPage.aspx";
            PrintFormReport.ReportTimeOut = 60000;
        }

        #endregion

    }
}