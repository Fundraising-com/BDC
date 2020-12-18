//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_OrderDetail_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'OrderDetail.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for OrderDetailForm.
    /// </summary>
    public partial class OrderDetail : BaseOrderDetail {
        private const string IMG_SUBMIT_ORDER_URL = "~/images/btnSubmitOrder.gif";
        private const string IMG_SAVE_ORDER_URL = "~/images/btnSaveOrder.gif";
        private const string IMG_SUBMIT_AND_PERSONALIZE_URL = "~/images/btnSubmitOrderAndPersonalize.gif";
        private const string IMG_SAVE_AND_PERSONALIZE_URL = "~/images/btnSaveOrderAndPersonalize.gif";

        private const string ORDER_ID = "OrderID";
        private const string GO_TO_PERSONALIZATION_MSG = "Do you want to edit the personalization for this order?";

        private int c_OrderID;
        protected System.Web.UI.WebControls.DataList dtLstProductDetail;
        protected System.Web.UI.WebControls.RadioButtonList radBtnLstDeliveryMethod;
        private const string ORDER_DATA = "OrderData";
        private const string ACCOUNT_DATA = "AccountData";
        protected dataDef dtsOrder;
        protected AccountData dtsAccount;
        protected System.Web.UI.WebControls.Label lblValidation;
        protected System.Web.UI.WebControls.Label Label19;
        protected System.Web.UI.WebControls.RadioButtonList radBtnLstShipTo;
        protected System.Web.UI.WebControls.Label lblLabelDeliveryDate;
        protected System.Web.UI.WebControls.RequiredFieldValidator reqFldVal_DeliveryDate;
        protected System.Web.UI.WebControls.CompareValidator compVal_DeliveryDate;
        protected System.Web.UI.WebControls.Label Label12;
        protected System.Web.UI.WebControls.Label lblSupplyDayLeadTime;
        protected System.Web.UI.WebControls.Label lblSupplyWaitMsg;
        protected System.Web.UI.WebControls.Label Label5;
        protected System.Web.UI.WebControls.Label lblSupplyBusinessMessage;
        protected System.Web.UI.HtmlControls.HtmlTableRow trSupplyBusinessMessage;
        protected System.Web.UI.HtmlControls.HtmlTable tblAddressSupply;
        protected System.Web.UI.WebControls.ImageButton imgBtnSupplyDeliveryDate;
        protected System.Web.UI.WebControls.TextBox txtSupplyDeliveryDate;
        protected System.Web.UI.WebControls.Label lblOrderDetailBusinessMessage;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBusinessMessage;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here
                OrderInfo1.IsExceptionReadOnly = false;
                LoadData();
                HeaderDetailForm.InitializeControls();
                if (!IsPostBack) {
                    if (this.Page.ValSummary != null)
                    {
                        this.Page.ValSummary.Visible = false;
                    }
                    BindForm();

                    trButtonConfirm.Visible = false;
                    trButtonValidate.Visible = true;

                    trOrderInfo.Visible = false;
                    trOrderDetail.Visible = true;

                    int OrderStatusID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID]);

                    int fulfOrderID = 0;
                    if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID)) {
                        if (dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID].ToString().Length > 0)
                            fulfOrderID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID]);
                    }
                    imgBtnSaveForLater.Visible = ((OrderStatusID < QSPForm.Common.OrderStatus.IN_PROCESS) && (fulfOrderID == 0));

                    if ((OrderStatusID == QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) || (OrderStatusID == QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION)) {
                        imgBtnRollBack.Visible = true;
                        imgBtnCancelOrder.Visible = false;
                        imgBtnValidate.Visible = false;
                        this.Page.SetPageMessage("The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Rollback to undo changes.");
                    }
                    else {
                        imgBtnRollBack.Visible = false;
                        imgBtnCancelOrder.Visible = true;
                        imgBtnValidate.Visible = true;
                    }

                    SetHeaderTitle();
                    //If no supply is entered we don't show the section by default
                    trOrderSupply.Visible = (dtsOrder.OrderSupply.TotalQuantity > 0);
                }
                SetMessageCancelOrder();
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnShowSupply.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnShowSupply_Click);
            this.imgBtnHideSupply.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnHideSupply_Click);
            this.imgBtnCancelOrder.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnCancelOrder_Click);
            this.imgBtnRollBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnRollBack_Click);
            this.imgBtnValidate.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnValidate_Click);
            this.imgBtnBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnBack_Click);
            this.imgBtnProceed.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnProceed_Click);
            this.imgBtnSaveForLater.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSaveForLater_Click);

        }
        #endregion

        //		public int OrderID
        //		{
        override public int OrderID {
            get {
                return c_OrderID;
            }
            set {
                c_OrderID = value;
                ViewState[ORDER_ID] = c_OrderID;
            }
        }

        #region Fields

        private bool EditPersonalization {
            get {
                return Convert.ToBoolean(Convert.ToInt32(hidEditPersonalization.Value));
            }
            set {
                hidEditPersonalization.Value = value ? "1" : "0";
            }
        }

        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ORDER_DATA] = dtsOrder;
            imgBtnShowSupply.Visible = false;
        }

        private void SetHeaderTitle() {
            int FormID = 0;
            if (dtsOrder.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID)) {
                    int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                    QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                    dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                    if (dtsAccount.Account.Rows.Count > 0) {
                        DataRow accRow = dtsAccount.Account.Rows[0];
                        lblAccountNumber.Text = accRow[AccountTable.FLD_PKID].ToString();
                        lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
                    }
                }

                if (!ordRow.IsNull(OrderHeaderTable.FLD_FORM_ID)) {
                    FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
                    //Fill the Business Order Form Information
                    QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                    QSPForm.Common.DataDef.FormTable dTblForm = formSys.SelectOne(FormID);
                    if (dTblForm.Rows.Count > 0) {
                        DataRow formRow = dTblForm.Rows[0];
                        lblFormID.Text = FormID.ToString();
                        lblFormName.Text = formRow[FormTable.FLD_FORM_NAME].ToString();
                        if (!formRow.IsNull(FormTable.FLD_IMAGE_URL))
                            imgBusinessForm.ImageUrl = formRow[FormTable.FLD_IMAGE_URL].ToString();
                        else
                            imgBusinessForm.Visible = false;
                    }
                }
                else {
                    lblFormID.Text = "N/A";
                    lblFormName.Text = "Not Specified";
                    imgBusinessForm.Visible = false;
                }
            }
        }

        private void SetFormParameter() {
            if (Request[ORDER_ID] != null) {
                c_OrderID = Convert.ToInt32(Request[ORDER_ID].ToString());

            }
            else {
                c_OrderID = 0;
            }
            ViewState[ORDER_ID] = c_OrderID;
        }

        public override void BindForm() {
            HeaderDetailForm.BindForm();
            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = dtsOrder;
            OrderDetailSectionListStep.BindForm();

            if (!dtsOrder.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                lblNonAvailableOptionalSectionType.Visible = true;
                OrderDetailSectionListStep_Optional.Visible = false;
            }
            else {
                lblNonAvailableOptionalSectionType.Visible = false;
                OrderDetailSectionListStep_Optional.Visible = true;
                OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.DataSource = dtsOrder;
                OrderDetailSectionListStep_Optional.BindForm();
            }
            SupplyForm.Visible = false;
            imgBtnHideSupply.Visible = false;
            //SupplyForm.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                LoadDataSet();
                //Each Change, is marked as new process. 
                DataRow row = dtsOrder.OrderHeader.Rows[0];
                row[OrderHeaderTable.FLD_ORDER_DATE] = DateTime.Now;

                this.ViewState[ORDER_ID] = c_OrderID;
                this.ViewState[ORDER_DATA] = dtsOrder;

                int CampID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);

                //QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                //dtsAccount = accSys.SelectAllDetailByCampaignID(CampID);
                //this.ViewState[ORDER_ID] = c_OrderID;
                //this.ViewState[ACCOUNT_DATA] = dtsAccount;
            }
            else {
                c_OrderID = Convert.ToInt32(this.ViewState[ORDER_ID]);
                dtsOrder = (dataDef)this.ViewState[ORDER_DATA];
                //dtsAccount = (AccountData)this.ViewState[ACCOUNT_DATA];
            }

            int FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            //For each load, the page (the higher in the hirarchy)
            //is in charge to set all children's datasource 
            HeaderDetailForm.DataSource = dtsOrder;
            //HeaderDetailForm.AccountDataSource = dtsAccount;				


            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = dtsOrder;

            if (dtsOrder.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.DataSource = dtsOrder;
            }
            SupplyForm.DataSource = dtsOrder;
            OrderInfo1.DataSource = dtsOrder;
        }

        private void LoadDataSet() {
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				dtsOrder = ordSys.SelectAllDetail(c_OrderID);
            //			else
            dtsOrder = ordSys.SelectAllDetail(c_OrderID, true);
        }

        private bool SaveAllDetail() {
            bool blnValid = false;

            try {
                QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
                blnValid = orderSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private bool ValidateForm() {
            Boolean blnValid = true;
            blnValid = HeaderDetailForm.ValidateForm();
            if (!blnValid) {
                return false;
            }
            blnValid = OrderDetailSectionListStep.ValidateForm();
            if (!blnValid) {
                return false;
            }
            if (dtsOrder.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                blnValid = OrderDetailSectionListStep_Optional.ValidateForm();
                if (!blnValid) {
                    return false;
                }
            }
            blnValid = SupplyForm.ValidateForm();
            if (!blnValid) {
                return false;
            }

            return blnValid;
        }

        private bool UpdateDataSource() {
            Boolean blnValid = true;
            try {
                blnValid = HeaderDetailForm.UpdateDataSource();
                if (!blnValid) {
                    return false;
                }
                blnValid = OrderDetailSectionListStep.UpdateDataSource();
                if (!blnValid) {
                    return false;
                }

                if (dtsOrder.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                    blnValid = OrderDetailSectionListStep_Optional.UpdateDataSource();
                    if (!blnValid) {
                        return false;
                    }
                }
                blnValid = SupplyForm.UpdateDataSource();
                if (!blnValid) {
                    return false;
                }

                OrderInfo1.DataSource = dtsOrder;
                AccountData dtsAccount = new AccountData();
                if (dtsOrder.OrderHeader.Rows.Count > 0) {
                    DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                    if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID)) {
                        int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                        QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                        dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                        OrderInfo1.AccountDataSource = dtsAccount;
                        //Do the Business Validation for the Account
                        accSys.PerformValidation(dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                    }

                    //Do the Business Validation for the Order
                    //We have to change the Status to In Process
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.IN_PROCESS;
                    QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                    ordSys.CalculateTax(dtsOrder, dtsAccount, this.Page.UserID);
                    ordSys.PrePerformValidation(dtsOrder, dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                    ordSys.SetExpeditedFreightChargeRequirement(dtsOrder, this.Page.Role);
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private void imgBtnValidate_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPForm.Business.OrderSystem ordSys;

            try {
                HeaderDetailForm.ResetStatus();

                Boolean blnValid = true;
                blnValid = ValidateForm();
                if (!blnValid) {
                    return;
                }

                blnValid = UpdateDataSource();
                if (!blnValid) {
                    return;
                }

                OrderInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
                OrderInfo1.BindForm();
                Page.MaintainScrollPositionOnPostBack = false;

                trButtonConfirm.Visible = true;
                trButtonValidate.Visible = false;

                trOrderInfo.Visible = true;
                trOrderDetail.Visible = false;

                imgBtnProceed.ImageUrl = IMG_SUBMIT_ORDER_URL;
                imgBtnSaveForLater.ImageUrl = IMG_SAVE_ORDER_URL;

                // Ben - 03/26/2007:
                // Button's text should be changed depending on the PE status
                ordSys = new QSPForm.Business.OrderSystem();
                if (ordSys.IsOrderContainsPEProduct(dtsOrder)) {
                    if (dtsOrder.OrderDetail.IsPersonalizeComplete) {
                        imgBtnProceed.OnClientClick = "document.getElementById(\"" + hidEditPersonalization.ClientID + "\").value = Number(confirm(\"" + GO_TO_PERSONALIZATION_MSG + "\"));\n";
                        imgBtnSaveForLater.OnClientClick = "document.getElementById(\"" + hidEditPersonalization.ClientID + "\").value = Number(confirm(\"" + GO_TO_PERSONALIZATION_MSG + "\"));\n";
                    }
                    else {
                        imgBtnProceed.ImageUrl = IMG_SUBMIT_AND_PERSONALIZE_URL;
                        imgBtnSaveForLater.ImageUrl = IMG_SAVE_AND_PERSONALIZE_URL;
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void imgBtnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            trButtonConfirm.Visible = false;
            trButtonValidate.Visible = true;

            trOrderInfo.Visible = false;
            trOrderDetail.Visible = true;
        }

        private void imgBtnProceed_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Order Status
            //Check if we pass from a "Saved for later" to "In_process" Status
            //Save the approval of Order Exception if it's the case
            bool IsValid = false;
            bool redirectToPersonalization = false;
            string url = String.Empty;

            IsValid = OrderInfo1.UpdateDataSource();
            if (IsValid) {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                int OrderStatusID = QSPForm.Common.OrderStatus.IN_PROCESS;
                OrderStatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                if (OrderStatusID == QSPForm.Common.OrderStatus.SAVED_FOR_LATER)
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.IN_PROCESS;

                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(dtsOrder);
                if (IsOrderContainsPE) {
                    if (!dtsOrder.OrderDetail.IsPersonalizeComplete || EditPersonalization) {
                        //Order Status
                        ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.WAIT_FOR_PERSONALIZATION;
                        redirectToPersonalization = true;
                    }
                    else {
                        ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.IN_PROCESS;
                    }
                }

                if (SaveAllDetail()) {
                    if (!redirectToPersonalization) {
                        // url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, ORDER_ID, c_OrderID.ToString());
                        url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID);
                    }
                    else {
                        // url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString());
                        url = "~/OrderStep_Personalization.aspx?" + ORDER_ID + "=" + c_OrderID.ToString();
                    }

                    Response.Redirect(url, false);
                }
            }
        }

        private void imgBtnSaveForLater_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPForm.Business.OrderSystem ordSys;
            string url = String.Empty;

            //Clear Exception
            if (dtsOrder.OrderException.Rows.Count > 0)
                dtsOrder.OrderException.Rows.Clear();
            //Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;

            if (SaveAllDetail()) {
                // Ben - 03/26/2007:
                // Should redirect to personalization if incomplete
                ordSys = new QSPForm.Business.OrderSystem();
                if (ordSys.IsOrderContainsPEProduct(dtsOrder)) {
                    if (!dtsOrder.OrderDetail.IsPersonalizeComplete) {
                        // Response.Redirect(this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString()), false);
                        Response.Redirect("~/OrderStep_Personalization.aspx?" + "&" + ORDER_ID + "=" + OrderID.ToString());
                    }
                    else {
                        Response.Redirect(GetPageToGoToFromConfirm(), false);
                    }
                }
                else {
                    //Response.Redirect(this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, ORDER_ID, c_OrderID.ToString()), false);
                    Response.Redirect(string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID), false);
                }
            }
        }

        private void imgBtnCancelOrder_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                bool IsCancel = false;
                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                IsCancel = ordSys.SetCancelStatus(dtsOrder, this.Page.UserID);
                if (IsCancel) {
                    if (SaveAllDetail()) {
                        // string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, OrderDetailInfo.ORDER_ID, c_OrderID.ToString());
                        Response.Redirect(string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID), false);
                        //Response.Redirect(url, false);
                    }
                }
                else {
                    Page.SetPageMessage("This Order cannot be cancelled.");
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        private void imgBtnRollBack_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_STATUS_ID)) {
                int OrderStatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                if ((OrderStatusID == QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) || (OrderStatusID == QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION))
                    ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.ERROR_WAITING_ROLLBACK;

                if (SaveAllDetail()) {
                    //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, OrderDetailInfo.ORDER_ID, c_OrderID.ToString());
                    //Response.Redirect(url, false);
                    Response.Redirect(string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID), false);
                }
            }
        }

        protected void chkBoxShowOnlyException_CheckedChanged(object sender, System.EventArgs e) {
            OrderInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
        }

        private void imgBtnHideSupply_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            trOrderSupply.Visible = false;
        }

        private void imgBtnShowSupply_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            trOrderSupply.Visible = true;
        }

        private void SetMessageCancelOrder() {
            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Cancel this Order";
            if (imgBtnCancelOrder != null)
                imgBtnCancelOrder.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");
        }

        private string GetPageToGoToFromConfirm() {
            string url = String.Empty;
            if (EditPersonalization) {
                //return this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1, ORDER_ID, c_OrderID.ToString());
                url = "~/OrderStep_Personalization.aspx?" + "&" + ORDER_ID + "=" + OrderID.ToString();
                return url;
            }
            else {
                // return this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo, ORDER_ID, c_OrderID.ToString());
                return string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID);
            }
        }
    }
}