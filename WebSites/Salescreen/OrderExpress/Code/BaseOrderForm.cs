using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using QSPForm.Business;
using QSPForm.Common.DataDef;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>Base page for Web Form pages in QSPForm_Web</summary>
    /// <remarks>
    ///		Inherit from BasePage
    ///		We used this class to manage common functionnality
    ///		for DataGrid by example
    ///	</remarks>
    public class BaseOrderForm : BaseWebForm {
        private OrderData dtsOrderData;
        private const string ORDER_DATA = "OrderData";
        protected BaseOrderFormStep currentStep;
        private BaseWebFormStepCollection orderFormSteps = new BaseWebFormStepCollection();
        private CommonUtility clsUtil = new CommonUtility();
        private System.Web.UI.WebControls.PlaceHolder placeHolderBodyPage;
        private NameValueCollection nvcolLoadControlInfo = new NameValueCollection();
        private const string COLLECTION_STEP = "COLLECTION_STEP";

        override protected void OnLoad(EventArgs e) 
        {
            //Load Data
            if (!IsPostBack) 
            {
                //Determin the FM if is not in FM Mode				

                #region Account data load

                AccountData dtsAccountData = new AccountData();
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccountData = accSys.SelectAllDetailByCampaignID(this.CampaignID);

                #endregion

                if (OrderID > 0) 
                {
                    LoadDataSource();
                }
                else 
                {
                    //Add Mode
                    string sFMID = "";
                    string sFMName = "";
                    this.DataOperation = QSPForm.Common.DataOperation.INSERT;

                    if (this.Role == QSPForm.Business.AuthSystem.ROLE_FM) 
                    {
                        sFMID = this.FMID;
                        QSPForm.Business.CUserSystem cuserSys = new QSPForm.Business.CUserSystem();

                        CUserTable dTblFM = cuserSys.SelectOne(sFMID);
                        if (dTblFM.Rows.Count > 0) 
                        {
                            DataRow row = dTblFM.Rows[0];
                            sFMName = row[CUserTable.FLD_LAST_NAME].ToString() + ", " + row[CUserTable.FLD_FIRST_NAME].ToString();
                        }
                    }
                    else 
                    {
                        DataRow accRow = dtsAccountData.Account.Rows[0];
                        string accountFMID = accRow[AccountTable.FLD_FM_ID].ToString();
                        string accountFMNAME = accRow[AccountTable.FLD_FM_NAME].ToString();

                        sFMID = accountFMID;
                        sFMName = accountFMNAME;
                    }

                    QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();

                    dtsOrderData = orderSys.InitializeOrder(this.UserID, sFMID, this.FormID, this.CampaignID);
                    dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_FM_NAME] = sFMName;
                    //dtsAccountData = accSys.VerifyAccountRenewal(this.CampaignID, this.UserID);								
                }

                #region Account data load - set data

                if (dtsAccountData.Account.Rows.Count > 0)
                {
                    DataRow accRow = dtsAccountData.Account.Rows[0];

                    if (accRow[AccountTable.FLD_PKID] != null)
                    {
                        AccountNumber = accRow[AccountTable.FLD_PKID].ToString();
                    }

                    if (accRow[AccountTable.FLD_NAME] != null)
                    {
                        AccountName = accRow[AccountTable.FLD_NAME].ToString();
                    }

                    if (accRow[AccountTable.FLD_CUSTOMER_ID] != null)
                    {
                        dtsOrderData.OrderHeader.Rows[0][OrderHeaderTable.FLD_CUSTOMER_ID] = accRow[AccountTable.FLD_CUSTOMER_ID];
                    }
                }

                #endregion

                //Fill the Business Order Form Information
                QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
                QSPForm.Common.DataDef.FormTable dTblForm = formSys.SelectOne(FormID);

                if (dTblForm.Rows.Count > 0) 
                {
                    DataRow formRow = dTblForm.Rows[0];
                    FormCode = formRow[FormTable.FLD_FORM_CODE].ToString();
                    FormName = formRow[FormTable.FLD_FORM_NAME].ToString();
                    FormImageURL = formRow[FormTable.FLD_IMAGE_URL].ToString();
                }
            }
            else 
            {
                //For each postback, the page (the higher in the hierarchy)
                //is in charge to set all children's datasource 
                //dtsOrderData = (OrderData)this.ViewState[ORDER_DATA];
            }
            
            //currentStep = (BaseOrderFormStep) this.orderFormSteps.FindByAppItem(this.AppItem);

            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {

            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private void InitializeCurrentStepEvent() {
            this.currentStep.GoToPreviousStep += new System.EventHandler(currentStep_GoToPreviousStep);
            this.currentStep.GoToNextStep += new System.EventHandler(currentStep_GoToNextStep);
        }

        protected override void LoadViewState(object savedState) {
            if (savedState != null) {
                // Load State from the array of objects that was saved at ;
                // SavedViewState.
                object[] myState = (object[])savedState;
                string sControlURL = "";
                int NoMenu = 0;

                if (myState[0] != null)
                    base.LoadViewState(myState[0]);
                if (myState[1] != null)
                    sControlURL = (string)myState[1];
                if (myState[2] != null)
                    NoMenu = (int)myState[2];

                this.AppItem = (QSPForm.Business.AppItem)NoMenu;

                RefreshPageInformation();
                dtsOrderData = (OrderData)this.ViewState[ORDER_DATA];
                AssignCurrentStep(sControlURL);

                if (ViewState[COLLECTION_STEP] != null) {
                    nvcolLoadControlInfo = (NameValueCollection)ViewState[COLLECTION_STEP];
                }
                else {
                    nvcolLoadControlInfo = new NameValueCollection();
                }

                //InitializeCurrentStepEvent();
                //currentStep.DataSource = this.DataSource;
            }
        }

        protected override object SaveViewState() {  // Change Text Property of Label when this function is invoked.
            // Save State as a cumulative array of objects.
            object baseState = base.SaveViewState();
            string sControlURL = this.ControlURL;
            object[] allStates = new object[3];
            allStates[0] = baseState;
            allStates[1] = sControlURL;
            allStates[2] = Convert.ToInt32(this.AppItem);
            return allStates;
        }

        public OrderData DataSource {
            get {
                return dtsOrderData;
            }
            set {
                dtsOrderData = value;
            }
        }

        public int OrderID {
            get {
                if (ViewState["OrderID"] != null) {
                    return Convert.ToInt32(ViewState["OrderID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["OrderID"] = value;
            }
        }

        public int CampaignID {
            get {
                if (ViewState["CampaignID"] != null) {
                    return Convert.ToInt32(ViewState["CampaignID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["CampaignID"] = value;
            }
        }

        public int QCAPOrderID
        {
            get
            {
                if (ViewState["QCAPOrderID"] != null)
                {
                    return Convert.ToInt32(ViewState["QCAPOrderID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["QCAPOrderID"] = value;
            }
        }

        public string AccountNumber {
            get {
                if (ViewState["AccountNumber"] != null) {
                    return ViewState["AccountNumber"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["AccountNumber"] = value;
            }
        }

        public string AccountName {
            get {
                if (ViewState["AccountName"] != null) {
                    return ViewState["AccountName"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["AccountName"] = value;
            }
        }

        public string FormName {
            get {
                if (ViewState["FormName"] != null) {
                    return ViewState["FormName"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["FormName"] = value;
            }
        }

        public string FormCode {
            get {
                if (ViewState["FormCode"] != null) {
                    return ViewState["FormCode"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["FormCode"] = value;
            }
        }

        public int FormID {
            get {
                if (ViewState["FormID"] != null) {
                    return Convert.ToInt32(ViewState["FormID"]);
                }
                else {
                    return 0;
                }
            }
            set {
                ViewState["FormID"] = value;
            }
        }

        public string FormImageURL {
            get {
                if (ViewState["FormImageURL"] != null) {
                    return ViewState["FormImageURL"].ToString();
                }
                else {
                    return "";
                }
            }
            set {
                ViewState["FormImageURL"] = value;
            }
        }

        public bool IsSupplyStepSkipped {
            get {
                return !IsControlLoad(QSPForm.Business.AppItem.OrderForm_Step6);
            }
        }

        public BaseOrderFormStep CurrentStep {
            get {
                return currentStep;
            }
            set {
                currentStep = value;
            }
        }

        public PlaceHolder PlaceHolderBodyPage {
            get {
                return placeHolderBodyPage;
            }
            set {
                placeHolderBodyPage = value;
            }
        }

        protected override void OnPreRender(EventArgs e) {
            this.ViewState[ORDER_DATA] = dtsOrderData;
            this.ViewState[COLLECTION_STEP] = nvcolLoadControlInfo;
            base.OnPreRender(e);
        }

        private void currentStep_GoToPreviousStep(object sender, System.EventArgs e) {
            this.AppItem = currentStep.PreviousAppItem;
            GoToStepByAppItem();
        }

        private void currentStep_GoToNextStep(object sender, System.EventArgs e) {
            if (currentStep.ValidateForm()) {
                currentStep.Update();
                this.AppItem = currentStep.NextAppItem;
                GoToStepByAppItem();
            }
        }

        public void GoToStepByAppItem() {
            this.DisplayManager();
        }

        public void GoNextByAppItem(QSPForm.Business.AppItem appItem) {
            if (currentStep.ValidateForm()) {
                currentStep.Update();
                this.AppItem = appItem;
                GoToStepByAppItem();
            }
        }

        public void GoToStepByAppItem(QSPForm.Business.AppItem appItem) {
            this.AppItem = appItem;
            GoToStepByAppItem();
        }

        private void AssignCurrentStep(string sControlURL) {
            sControlURL = "~/UserControls/" + sControlURL;
            if (sControlURL.Length > 0) {
                BaseOrderFormStep ctl = (BaseOrderFormStep)LoadControl(sControlURL);
                ctl.ID = "Step" + this.AppItem.ToString();
                ctl.DataSource = dtsOrderData;
                this.currentStep = ctl;
                InitializeCurrentStepEvent();
                CurrentStep.IsFirstLoad = !IsControlLoad(this.AppItem);
                orderFormSteps.Clear();
                orderFormSteps.Add(ctl);
                placeHolderBodyPage.Controls.Clear();
                placeHolderBodyPage.Controls.Add(ctl);
            }
        }

        public void DisplayManager() {
            this.RefreshPageInformation();
            if (this.ControlURL.Length > 0)
                AssignCurrentStep(this.ControlURL);
            //CurrentStep.DataSource = dtsOrderData;
            if (CurrentStep != null) {
                CurrentStep.BindForm();
                if (CurrentStep.IsFirstLoad) {
                    AssignLoadControlInfo(this.AppItem);
                    //CurrentStep.IsFirstLoad = false;				
                }
            }
            //To be always at the beginning of the page when going to another step
            this.Page.MaintainScrollPositionOnPostBack = false;

        }

        public BaseWebFormStepCollection OrderFormSteps {
            get {
                return orderFormSteps;
            }
            set {
                orderFormSteps = value;
            }
        }

        public bool SaveDataSource() {
            // CHR CODE

            bool IsSuccess = false;

            if (dtsOrderData.OrderHeader.Rows.Count > 0) {

                #region Check if the order needs approval

                try {
                    DataRow shipmentRow = dtsOrderData.ShipmentGroup.Rows[0];

                    // if QSP To Pay and user is an FSM
                    //if ((int)shipmentRow[15] == 2 && this.Role == AuthSystem.ROLE_FM) {
                    if ((int)shipmentRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID] == 2 && this.Role == AuthSystem.ROLE_FM)
                    {
                        // Needs approval
                        dtsOrderData.OrderHeader.Rows[0][8] = 5;
                    }
                }
                catch (Exception ex) {
                }

                #endregion

                DataRow ordRow = dtsOrderData.OrderHeader.Rows[0];

                if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID)) {
                    int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                    //QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();	
                    //AccountData dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
                    QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                    int orderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                    if (orderID <= 0) {
                        IsSuccess = ordSys.InsertAllDetail(dtsOrderData, this.UserID);
                    }
                    else {
                        IsSuccess = ordSys.UpdateAllDetail(dtsOrderData, this.UserID);
                    }
                    orderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                    this.OrderID = orderID;
                }
            }
            return IsSuccess;
        }

        public void LoadDataSource() {
            this.DataOperation = QSPForm.Common.DataOperation.UPDATE;
            QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();

            dtsOrderData = orderSys.SelectAllDetail(OrderID, true);

            if (dtsOrderData.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = dtsOrderData.OrderHeader.Rows[0];
                CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
            }

        }

        private void AssignLoadControlInfo(QSPForm.Business.AppItem appItem) {
            if (nvcolLoadControlInfo[appItem.ToString()] == null) {
                nvcolLoadControlInfo.Add(appItem.ToString(), true.ToString());
            }

        }

        private bool IsControlLoad(QSPForm.Business.AppItem appItem) {
            return (nvcolLoadControlInfo[appItem.ToString()] != null);

        }
    }
}