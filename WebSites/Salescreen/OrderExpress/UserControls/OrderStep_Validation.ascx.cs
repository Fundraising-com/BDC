using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderStep_Validation : BaseOrderFormStep {
        private CommonUtility util = new CommonUtility();

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
            this.imgBtnSaveForLater.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSaveForLater_Click);
            this.imgBtnConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnConfirm_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetMessageButton();
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            //To Be always databind 
            IsFirstLoad = true;
            OrderInfo1.imgBtnDetailAccount_Visible = false; ;
            if (!IsPostBack) {

            }

            trQCAPOrderIntimation.Visible = this.Page.QCAPOrderID != 0;
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step6;
            this.StepItem = QSPForm.Business.AppItem.OrderForm_Step7;
            this.NextAppItem = QSPForm.Business.AppItem.OrderForm_Step8;
            this.ImageButtonBack = imgBtnBack;
            OrderInfo1.IsExceptionReadOnly = false;
        }

        public bool ShowOnlyException {
            get {
                if (ViewState["ShowOnlyException"] != null)
                    return Convert.ToBoolean(ViewState["ShowOnlyException"]);
                else
                    return false;
            }
            set {
                ViewState["ShowOnlyException"] = value;
            }
        }

        public override void BindForm() {
            OrderInfo1.DataSource = this.DataSource;
            AccountData dtsAccount = new AccountData();
            if (this.DataSource.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID)) {
                    int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                    QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                    dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                    OrderInfo1.AccountDataSource = dtsAccount;
                    //Do the Business Validation for the Account
                    accSys.PerformValidation(dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                }
                //Do the Business Validation for the Order
                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                ordSys.CalculateTax(this.DataSource, dtsAccount, this.Page.UserID);
                ordSys.PrePerformValidation(this.DataSource, dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.INSERT);
                ordSys.SetExpeditedFreightChargeRequirement(this.DataSource, this.Page.Role);
                OrderInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
                OrderInfo1.BindForm();
            }
        }

        public override bool Update() {
            bool IsSuccess = false;
            try {
                OrderSystem ordSys = new OrderSystem();
                IsSuccess = this.Page.SaveDataSource();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return IsSuccess;
        }

        protected override void SetInnerControlDataSource() {
            OrderInfo1.DataSource = this.DataSource;
        }

        private void SetMessageButton() {
            // CHR CODE

            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Process Order Now?";
            if (this.DataSource.OrderException.IsContainExceptionType((int)QSPForm.Common.BusinessExceptionType.Expedited_Freight_Charges)) {
                if (Page.Role == AuthSystem.ROLE_FM) {
                    sWarningMessage = "If this order requires Expedited Freight, the cost will be recovered from your 12-pay. If you choose QSP To Pay, the order will require approval from QSP. Are you sure you want to proceed?";

                }
                else if (Page.Role > AuthSystem.ROLE_FIELD_SUPPORT) {
                    sWarningMessage = "If this order requires Expedited Freight, the cost will be paid by QSP or FSM.  Are you sure you want to proceed?";
                }
            }

            if (imgBtnConfirm != null)
                imgBtnConfirm.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");

            if (imgBtnSaveForLater != null)
                imgBtnSaveForLater.Attributes.Add("onclick", "return confirm('Are you sure you want to Save/Hold Order and Process Later?');");
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(this.DataSource);

            trConfirmationButton.Visible = !IsOrderContainsPE;
            trPersonalizationButton.Visible = IsOrderContainsPE;
            trSaveButton.Visible = !IsOrderContainsPE;
        }

        private void GoToConfirmationPage() {
            DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
            string sOrderID = ordRow[OrderHeaderTable.FLD_PKID].ToString();
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step8);
            // RNK
            string url = "~/OrderStep_Confirmation.aspx?&NoMenu=22";
            Response.Redirect(url + "&" + OrderStep_Confirmation.ORDER_ID + "=" + sOrderID);
        }

        private void GoToPersonalizationPage() {
            DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
            string sOrderID = ordRow[OrderHeaderTable.FLD_PKID].ToString();
            // RNK

            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step7_1);
            string url = "~/OrderStep_Personalization.aspx?&NoMenu=22";
            Response.Redirect(url + "&" + OrderStep_Confirmation.ORDER_ID + "=" + sOrderID);
        }

        private void imgBtnSaveForLater_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            // Clear exceptions since we are only saving for later
            // The order will be validated again upon submission
            if (this.DataSource.OrderException.Rows.Count > 0) {
                this.DataSource.OrderException.Rows.Clear();
            }

            // Set order status to saved for later
            DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;

            // Try to save the info
            bool IsSuccess = false;
            IsSuccess = Update();

            // If we are successful, move to the next step
            if (IsSuccess) 
            {
                #region Generate applicable charges to the saved order

                DataRow savedOrderRow = this.DataSource.OrderHeader.Rows[0];
                string newOrderIdString = savedOrderRow[OrderHeaderTable.FLD_PKID].ToString();
                int newOrderId = Convert.ToInt32(newOrderIdString);
                QSPForm.Business.Communication.Notifications notifications = new QSPForm.Business.Communication.Notifications();

                QSPForm.Business.Controller.OrderController oc = new QSPForm.Business.Controller.OrderController();
                oc.GenerateCharges(newOrderId, notifications);

                #endregion

                #region Do program agreement to supply order mapping

                try
                {
                    DataRow orderRow = this.DataSource.OrderHeader.Rows[0];

                    int orderId = 0;
                    bool isOrderIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_PKID].ToString(), out orderId);

                    int userId = 0;
                    bool isUserIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_CREATE_USER_ID].ToString(), out userId);

                    if (isUserIdParseSuccessful && isOrderIdParseSuccessful && orderId != 0)
                    {
                        OrderSystem orderSystem = new OrderSystem();
                        orderSystem.MapOrderToProgramAgreement(orderId, userId);
                    }
                }
                catch (Exception ex)
                {
                }

                #endregion

                GoToConfirmationPage();
            }
        }

        private void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
            int formId = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
            int desiredOrderStatusId = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;

            FormSystem formSystem = new FormSystem();
            if (formSystem.IsOtisForm(formId) || formSystem.IsPineValleyForm(formId)) 
            {
                // Save the status we want to submit the order to
                desiredOrderStatusId = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);

                // Temporarily set the status to saved for later
                // We return to our desired status after we are done creating surcharges for the saved order
                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;
            }

            // Try to submit the info
            bool IsSuccess = false;
            IsSuccess = OrderInfo1.UpdateDataSource();

            if (IsSuccess) 
            {
                // Try to save the info
                IsSuccess = Update();

                // If we are successful, move to the next step
                if (IsSuccess) 
                {
                    DataRow savedOrderRow = this.DataSource.OrderHeader.Rows[0];
                    string newOrderIdString = savedOrderRow[OrderHeaderTable.FLD_PKID].ToString();
                    int newOrderId = Convert.ToInt32(newOrderIdString);

                    QSPForm.Business.Controller.OrderController oc = new QSPForm.Business.Controller.OrderController();

                    if (formSystem.IsOtisForm(formId) || formSystem.IsPineValleyForm(formId)) 
                    {
                        #region Generate applicable charges to the saved order

                        QSPForm.Business.Communication.Notifications notifications = new QSPForm.Business.Communication.Notifications();
                        oc.GenerateCharges(newOrderId, notifications);

                        #endregion

                        #region Do program agreement to supply order mapping

                        try
                        {
                            DataRow orderRow = this.DataSource.OrderHeader.Rows[0];

                            int orderId = 0;
                            bool isOrderIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_PKID].ToString(), out orderId);

                            int userId = 0;
                            bool isUserIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_CREATE_USER_ID].ToString(), out userId);

                            if (isUserIdParseSuccessful && isOrderIdParseSuccessful && orderId != 0)
                            {
                                OrderSystem orderSystem1 = new OrderSystem();
                                orderSystem1.MapOrderToProgramAgreement(orderId, userId);
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        #endregion

                        #region Set the correct campaign

                        try
                        {
                            int currentCampaignId = Convert.ToInt32(savedOrderRow[OrderHeaderTable.FLD_CAMPAIGN_ID].ToString());
                            int correctCampaignId = this.GetCorrectCampaignId(formId, currentCampaignId);

                            if (currentCampaignId != correctCampaignId)
                            {
                                QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(newOrderId);
                                order.CampaignId = correctCampaignId;
                                order.Update();
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        #endregion

                        #region Set the desired status

                        OrderSystem orderSystem2 = new OrderSystem();
                        orderSystem2.SetStatus(newOrderId, desiredOrderStatusId, "set status after generating charges", this.Page.UserID);

                        #endregion
                    }

                    if (this.Page.QCAPOrderID != 0)
                    {
                        OrderSystem ordSys = new OrderSystem();
                        ordSys.DeleteQCAPOrder(this.Page.QCAPOrderID, this.Page.UserID);
                    }

                    GoToConfirmationPage();
                }
            }
        }

        private int GetCorrectCampaignId(int formId, int currentCampaignId)
        {
            int result = 0;

            QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(formId);
            QSP.Business.Fulfillment.Campaign currentCampaign = QSP.Business.Fulfillment.Campaign.GetCampaign(currentCampaignId);

            int fiscalYear = FiscalYearSystem.GetFYFromForm(formId);
            List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(fiscalYear, currentCampaign.AccountId, (int)form.ProgramTypeId);
            

            if (campaignList.Count == 0)
            {
                #region Create new campaign

                QSP.Business.Fulfillment.Account account = QSP.Business.Fulfillment.Account.GetAccount(currentCampaign.AccountId);

                #region Campaign

                QSP.Business.Fulfillment.Campaign newCampaign = new QSP.Business.Fulfillment.Campaign();

                //newCampaign.CampaignId;
                newCampaign.AccountId = currentCampaign.AccountId;
                //newCampaign.FulfCampaignId;
                newCampaign.ProgramTypeId = (int)form.ProgramTypeId;
                //newCampaign.WarehouseId;
                newCampaign.CampaignName = account.AccountName;
                newCampaign.FmId = currentCampaign.FmId;
                //newCampaign.TaxExemptionNumber;
                //newCampaign.TaxExemptionExpirationDate;
                newCampaign.StartDate = (DateTime)form.StartDate;
                newCampaign.EndDate = (DateTime)form.EndDate;
                newCampaign.FiscalYear = fiscalYear;
                newCampaign.Enrollment = currentCampaign.Enrollment;
                newCampaign.GoalEstimatedGross = currentCampaign.GoalEstimatedGross;
                //newCampaign.ARORBL;
                newCampaign.Comments = "Campaign created with new order";
                newCampaign.Deleted = false;
                newCampaign.CreateDate = DateTime.Now;
                newCampaign.CreateUserId = this.Page.UserID;
                newCampaign.UpdateDate = DateTime.Now;
                newCampaign.UpdateUserId = this.Page.UserID;
                //newCampaign.DtsCAccountId;
                //newCampaign.DtsCCAInstance;

                if (currentCampaign.TradeClassId != null)
                {
                    newCampaign.TradeClassId = currentCampaign.TradeClassId;
                }

                List<QSP.Business.Fulfillment.Form> programFormList = QSP.Business.Fulfillment.Form.GetProgramForm((int)form.ProgramTypeId);
                if (programFormList.Count > 0)
                {
                    newCampaign.FormId = programFormList[0].FormId;
                }

                newCampaign.Insert();

                QSPForm.Data.Campaign campaignDataAccess = new QSPForm.Data.Campaign();
                campaignDataAccess.CampaignLinkToCCA(newCampaign.CampaignId);

                #endregion

                #region Postal addresses

                List<QSP.Business.Fulfillment.PostalAddressAccount> postalAddressList = QSP.Business.Fulfillment.PostalAddressAccount.GetAddressesByAccount(account.AccountId);
                foreach (QSP.Business.Fulfillment.PostalAddressAccount postalAddress in postalAddressList)
                {
                    QSP.Business.Fulfillment.PostalAddressCampaign newPostalAddress = new QSP.Business.Fulfillment.PostalAddressCampaign();

                    //newPostalAddress.PostalAddressCampaignId; 
                    newPostalAddress.PostalAddressTypeId = postalAddress.PostalAddressTypeId;
                    newPostalAddress.PostalAddressId = postalAddress.PostalAddressId;
                    newPostalAddress.CampaignId = newCampaign.CampaignId;
                    newPostalAddress.Deleted = false;
                    newPostalAddress.CreateDate = DateTime.Now;
                    newPostalAddress.CreateUserId = this.Page.UserID;
                    newPostalAddress.UpdateDate = DateTime.Now;
                    newPostalAddress.UpdateUserId = this.Page.UserID;

                    newPostalAddress.Insert();
                }

                #endregion

                #region Phone number

                List<QSP.Business.Fulfillment.PhoneNumberAccount> phoneNumberList = QSP.Business.Fulfillment.PhoneNumberAccount.GetPhoneNumberAccountList(account.AccountId);
                foreach (QSP.Business.Fulfillment.PhoneNumberAccount phoneNumber in phoneNumberList)
                {
                    QSP.Business.Fulfillment.PhoneNumberCampaign newPhoneNumber = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                    //newPhoneNumber.PhoneNumberCampaignId;
                    newPhoneNumber.PhoneNumberTypeId = phoneNumber.PhoneNumberTypeId;
                    newPhoneNumber.PhoneNumberId = phoneNumber.PhoneNumberId;
                    newPhoneNumber.CampaignId = newCampaign.CampaignId;
                    newPhoneNumber.Deleted = false;
                    newPhoneNumber.CreateDate = DateTime.Now;
                    newPhoneNumber.CreateUserId = this.Page.UserID;
                    newPhoneNumber.UpdateDate = DateTime.Now;
                    newPhoneNumber.UpdateUserId = this.Page.UserID;

                    newPhoneNumber.Insert();
                }

                #endregion

                #region Email

                List<QSP.Business.Fulfillment.EmailAccount> emailList = QSP.Business.Fulfillment.EmailAccount.GetEmailAccountListByAccount(account.AccountId);
                foreach (QSP.Business.Fulfillment.EmailAccount email in emailList)
                {
                    QSP.Business.Fulfillment.EmailCampaign newEmail = new QSP.Business.Fulfillment.EmailCampaign();

                    //newEmail.EmailCampaignId;                    
                    newEmail.EmailTypeId = email.EmailTypeId;
                    newEmail.EmailId = email.EmailId;
                    newEmail.CampaignId = newCampaign.CampaignId;
                    newEmail.Deleted = false;
                    newEmail.CreateDate = DateTime.Now;
                    newEmail.CreateUserId = this.Page.UserID;
                    newEmail.UpdateDate = DateTime.Now;
                    newEmail.UpdateUserId = this.Page.UserID;

                    newEmail.Insert();
                }

                #endregion

                #endregion

                result = newCampaign.CampaignId;
            }
            else
            {
                result = campaignList[0].CampaignId;
            }

            return result;
        }

        protected void imgBtnPersonalize_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Clear Exception
            if (this.DataSource.OrderException.Rows.Count > 0)
                this.DataSource.OrderException.Rows.Clear();
            //Order Status
            DataRow ordRow = this.DataSource.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.WAIT_FOR_PERSONALIZATION;

            bool IsSuccess = false;
            IsSuccess = Update();
            if (IsSuccess) {
                GoToPersonalizationPage();
            }
        }

        protected override void OnGoToPreviousStep(System.EventArgs e) {
            if (this.Page.IsSupplyStepSkipped) {
                this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step5;
            }

            base.OnGoToPreviousStep(e);
        }

        protected void chkBoxShowOnlyException_CheckedChanged(object sender, System.EventArgs e) {
            OrderInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
            //OrderInfo1.BindForm();
        }
    }
}