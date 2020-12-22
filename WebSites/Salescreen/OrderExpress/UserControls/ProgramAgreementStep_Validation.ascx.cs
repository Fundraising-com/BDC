using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataRef = QSPForm.Common.DataDef.ProgramAgreementData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for ProgramAgreementForm_Step1.
    /// </summary>
    public partial class ProgramAgreementStep_Validation : BaseProgramAgreementFormStep {
        private CommonUtility util = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            //To Be always databind 
            IsFirstLoad = true;
            ProgramAgreementInfo1.imgBtnDetailAccount_Visible = false; ;
            if (!IsPostBack) {
            }
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
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(Page_PreRender);
            this.imgBtnSaveForLater.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSaveForLater_Click);
            this.imgBtnConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnConfirm_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.chkBoxShowOnlyException.CheckedChanged += new EventHandler(chkBoxShowOnlyException_CheckedChanged);
        }
        #endregion

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step4;
            this.StepItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step5;
            this.NextAppItem = QSPForm.Business.AppItem.ProgramAgreementForm_Step6;
            this.ImageButtonBack = imgBtnBack;
            ProgramAgreementInfo1.IsExceptionReadOnly = false;
        }

        public override string InstructionText {
            get {
                return "Before completing the program agreement, carefully review ALL the information, especially any notations in the Important Information section. Use Back button to review previous steps and to make corrections, if necessary.";
            }
        }

        public override string SectionText {
            get {
                return "Add New Program Agreement :";
            }
        }

        public override string PageText {
            get {
                return "STEP 4 - PA Validation";
            }
        }

        public override string IconImage {
            get {
                return String.Empty;
            }
        }

        public override bool IconImageVisibility {
            get {
                return false;
            }
        }

        protected override void SetInnerControlDataSource() {
            ProgramAgreementInfo1.DataSource = this.DataSource;
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

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetMessageButton();
        }

        public override bool Update() 
        {
            OnSaving(EventArgs.Empty);

            return true;
        }

        public override void BindForm() {
            ProgramAgreementInfo1.DataSource = this.DataSource;
            AccountData dtsAccount = new AccountData();
            if (this.DataSource.ProgramAgreementCampaign.Rows.Count > 0) {
                DataRow prgCampRow = this.DataSource.ProgramAgreementCampaign.Rows[0];
                if (!prgCampRow.IsNull(ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID)) {
                    int CampaignID = Convert.ToInt32(prgCampRow[ProgramAgreementCampaignTable.FLD_CAMPAIGN_ID]);
                    QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                    dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                    ProgramAgreementInfo1.AccountDataSource = dtsAccount;
                    //Do the Business Validation for the Account
                    accSys.PerformValidation(dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.UPDATE);
                }

                //Do the Business Validation for the ProgramAgreement
                QSPForm.Business.ProgramAgreementSystem prgSys = new QSPForm.Business.ProgramAgreementSystem();
                //prgSys.PrePerformValidation(this.DataSource, dtsAccount, this.Page.UserID, QSPForm.Common.DataOperation.INSERT);
                prgSys.PerformValidation(this.DataSource, this.Page.UserID, QSPForm.Common.DataOperation.INSERT);
                //prgSys.SetExpeditedFreightChargeRequirement(this.DataSource, this.Page.Role);
                ProgramAgreementInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
                ProgramAgreementInfo1.BindForm();
            }
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            BindForm();
        }

        private int GetCorrectCampaignId(DateTime programAgreementStartDate, int accountId, int programTypeId)
        {
            int result = 0;

            int fiscalYear = FiscalYearSystem.GetFYFromDate(programAgreementStartDate);
            List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(fiscalYear, accountId, programTypeId);

            if (campaignList.Count == 0)
            {
                #region Create new campaign

                DataRow programAgreementRow = this.DataSource.ProgramAgreement.Rows[0];
                DataRow campaignRow = this.DataSource.Campaign.Rows[0];
                QSP.Business.Fulfillment.Account account = QSP.Business.Fulfillment.Account.GetAccount(accountId);

                #region Campaign

                QSP.Business.Fulfillment.Campaign newCampaign = new QSP.Business.Fulfillment.Campaign();

                //newCampaign.CampaignId;
                newCampaign.AccountId = accountId;
                //newCampaign.FulfCampaignId;
                newCampaign.ProgramTypeId = programTypeId;
                //newCampaign.WarehouseId;
                newCampaign.CampaignName = account.AccountName;
                newCampaign.FmId = Convert.ToString(campaignRow[CampaignTable.FLD_FM_ID]);
                //newCampaign.TaxExemptionNumber;
                //newCampaign.TaxExemptionExpirationDate;
                newCampaign.StartDate = Convert.ToDateTime(programAgreementRow[ProgramAgreementTable.FLD_START_DATE]);
                newCampaign.EndDate = Convert.ToDateTime(programAgreementRow[ProgramAgreementTable.FLD_END_DATE]);
                newCampaign.FiscalYear = fiscalYear;
                newCampaign.Enrollment = Convert.ToInt32(programAgreementRow[ProgramAgreementTable.FLD_ENROLLMENT]);
                newCampaign.GoalEstimatedGross = Convert.ToDecimal(programAgreementRow[ProgramAgreementTable.FLD_GOAL_ESTIMATED_GROSS]);
                //newCampaign.ARORBL;
                newCampaign.Comments = "Campaign created with new program agreement";
                newCampaign.Deleted = false;
                newCampaign.CreateDate = DateTime.Now;
                newCampaign.CreateUserId = this.Page.UserID;
                newCampaign.UpdateDate = DateTime.Now;
                newCampaign.UpdateUserId = this.Page.UserID;
                //newCampaign.DtsCAccountId;
                //newCampaign.DtsCCAInstance;

                if (!campaignRow.IsNull(CampaignTable.FLD_TRADE_CLASS_ID))
                {
                    newCampaign.TradeClassId = Convert.ToInt32(campaignRow[CampaignTable.FLD_TRADE_CLASS_ID]);
                }

                List<QSP.Business.Fulfillment.Form> programFormList = QSP.Business.Fulfillment.Form.GetProgramForm(programTypeId);
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

        private void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //ProgramAgreement Status		
            //We do nothing cause the perform Validation Method already set the status

            bool IsSuccess = false;

            IsSuccess = ProgramAgreementInfo1.UpdateDataSource();
            
            if (IsSuccess) 
            {
                IsSuccess = Update();
                
                if (IsSuccess)
                {
                    #region Patch for FY

                    try
                    {
                        DataRow programAgreementRow = this.DataSource.ProgramAgreement.Rows[0];
                        DataRow campaignRow = this.DataSource.Campaign.Rows[0];

                        QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(this.DataSource.FormID);
                        DateTime programAgreementStartDate = Convert.ToDateTime(programAgreementRow[ProgramAgreementTable.FLD_START_DATE]);
                        int accountId = Convert.ToInt32(campaignRow[CampaignTable.FLD_ACCOUNT_ID]);
                        int currentCampaignId = Convert.ToInt32(campaignRow[CampaignTable.FLD_PKID]);
                        int correctCampaignId = this.GetCorrectCampaignId(programAgreementStartDate, accountId, (int)form.ProgramTypeId);

                        if (currentCampaignId != correctCampaignId)
                        {
                            int programAgreementCampaignId = Convert.ToInt32(this.DataSource.ProgramAgreementCampaign.Rows[0][ProgramAgreementCampaignTable.FLD_PKID]);
                            QSP.Business.Fulfillment.ProgramAgreementCampaign pac = QSP.Business.Fulfillment.ProgramAgreementCampaign.GetProgramAgreementCampaign(programAgreementCampaignId);
                            pac.CampaignId = correctCampaignId;
                            pac.Update();

                            //Update OrderHeader with new CampaignID
                            int orderId = Convert.ToInt32(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID].ToString());
                            QSP.Business.Fulfillment.Order ord = QSP.Business.Fulfillment.Order.GetOrder(orderId);
                            ord.CampaignId = correctCampaignId;
                            ord.Update();
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    #endregion

                    #region Do program agreement to supply order mapping

                    try
                    {
                        int programAgreementId = 0;
                        DataRow programAgreementRow = this.DataSource.ProgramAgreement.Rows[0];
                        bool isProgramAgreementIdParseSuccessful = Int32.TryParse(programAgreementRow[ProgramAgreementTable.FLD_PKID].ToString(), out programAgreementId);

                        int userId = 0;
                        bool isUserIdParseSuccessful = Int32.TryParse(programAgreementRow[ProgramAgreementTable.FLD_CREATE_USER_ID].ToString(), out userId);

                        int orderId = 0;
                        DataRow orderRow = this.DataSource.OrderHeader.Rows[0];
                        bool isOrderIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_PKID].ToString(), out orderId);

                        if (isProgramAgreementIdParseSuccessful && isUserIdParseSuccessful && isOrderIdParseSuccessful && orderId != 0)
                        {
                            OrderSystem orderSystem = new OrderSystem();
                            orderSystem.MapOrderToProgramAgreement(orderId, programAgreementId, userId);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    #endregion

                    GoToConfirmationPage();
                }
            }
        }
        private void imgBtnSaveForLater_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            //Clear Exception
            if (this.DataSource.ProgramAgreementException.Rows.Count > 0)
            {
                this.DataSource.ProgramAgreementException.Rows.Clear();
            }

            //ProgramAgreement Status
            DataRow prgRow = this.DataSource.ProgramAgreement.Rows[0];
            prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = QSPForm.Common.ProgramAgreementStatus.SavedForLater;

            bool IsSuccess = false;

            IsSuccess = Update();
            
            if (IsSuccess) 
            {
                #region Patch for FY

                try
                {
                    DataRow programAgreementRow = this.DataSource.ProgramAgreement.Rows[0];
                    DataRow campaignRow = this.DataSource.Campaign.Rows[0];

                    QSP.Business.Fulfillment.Form form = QSP.Business.Fulfillment.Form.GetForm(this.DataSource.FormID);
                    DateTime programAgreementStartDate = Convert.ToDateTime(programAgreementRow[ProgramAgreementTable.FLD_START_DATE]);
                    int accountId = Convert.ToInt32(campaignRow[CampaignTable.FLD_ACCOUNT_ID]);
                    int currentCampaignId = Convert.ToInt32(campaignRow[CampaignTable.FLD_PKID]);
                    int correctCampaignId = this.GetCorrectCampaignId(programAgreementStartDate, accountId, (int)form.ProgramTypeId);

                    if (currentCampaignId != correctCampaignId)
                    {
                        int programAgreementCampaignId = Convert.ToInt32(this.DataSource.ProgramAgreementCampaign.Rows[0][ProgramAgreementCampaignTable.FLD_PKID]);
                        QSP.Business.Fulfillment.ProgramAgreementCampaign pac = QSP.Business.Fulfillment.ProgramAgreementCampaign.GetProgramAgreementCampaign(programAgreementCampaignId);
                        pac.CampaignId = correctCampaignId;
                        pac.Update();

                        //Update OrderHeader with new CampaignID
                        int orderId = Convert.ToInt32(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID].ToString());
                        QSP.Business.Fulfillment.Order ord = QSP.Business.Fulfillment.Order.GetOrder(orderId);
                        ord.CampaignId = correctCampaignId;
                        ord.Update();
                    }
                }
                catch (Exception ex)
                {
                }

                #endregion

                #region Do program agreement to supply order mapping

                try
                {
                    int programAgreementId = 0;
                    DataRow programAgreementRow = this.DataSource.ProgramAgreement.Rows[0];
                    bool isProgramAgreementIdParseSuccessful = Int32.TryParse(programAgreementRow[ProgramAgreementTable.FLD_PKID].ToString(), out programAgreementId);

                    int userId = 0;
                    bool isUserIdParseSuccessful = Int32.TryParse(programAgreementRow[ProgramAgreementTable.FLD_CREATE_USER_ID].ToString(), out userId);

                    int orderId = 0;
                    DataRow orderRow = this.DataSource.OrderHeader.Rows[0];
                    bool isOrderIdParseSuccessful = Int32.TryParse(orderRow[OrderHeaderTable.FLD_PKID].ToString(), out orderId);

                    if (isProgramAgreementIdParseSuccessful && isUserIdParseSuccessful && isOrderIdParseSuccessful)
                    {
                        OrderSystem orderSystem = new OrderSystem();
                        orderSystem.MapOrderToProgramAgreement(orderId, programAgreementId, userId);
                    }
                }
                catch (Exception ex)
                {
                }

                #endregion

                GoToConfirmationPage();
            }
        }

        private void GoToConfirmationPage() {
            DataRow ordRow = this.DataSource.ProgramAgreement.Rows[0];
            string sProgramAgreementID = ordRow[ProgramAgreementTable.FLD_PKID].ToString();

            string url = "~/ProgramAgreementStep_Confirmation.aspx?";
            Response.Redirect(url + "&" + ProgramAgreementStep_Confirmation.PROGRAM_AGREEMENT_ID + "=" + sProgramAgreementID);
        }
        protected override void OnGoToPreviousStep(System.EventArgs e) {
            base.OnGoToPreviousStep(e);
        }
        protected void chkBoxShowOnlyException_CheckedChanged(object sender, System.EventArgs e) {
            ProgramAgreementInfo1.ShowOnlyException = chkBoxShowOnlyException.Checked;
            //ProgramAgreementInfo1.BindForm();
        }
        private void SetMessageButton() {
            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Process the PA Now?";
            if (this.DataSource.ProgramAgreementException.IsContainExceptionType((int)QSPForm.Common.BusinessExceptionType.Expedited_Freight_Charges)) {
                if (Page.Role == AuthSystem.ROLE_FM) {
                    sWarningMessage = "This order requires Expedited Freight and the cost will be recovered from your 12-Pay unless the delivery date is changed.  Are you sure you want to proceed?";

                }
                else if (Page.Role > AuthSystem.ROLE_FIELD_SUPPORT) {
                    sWarningMessage = "This order requires Expedited Freight and the cost will be paid by QSP or FSM unless the delivery date is changed.  Are you sure you want to proceed?";
                }
            }

            if (imgBtnConfirm != null)
                imgBtnConfirm.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");

            if (imgBtnSaveForLater != null)
                imgBtnSaveForLater.Attributes.Add("onclick", "return confirm('Are you sure you want to Save/Hold the PA and Process Later?');");
        }
    }
}