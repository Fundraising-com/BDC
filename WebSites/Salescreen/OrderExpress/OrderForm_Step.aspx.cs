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
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class OrderForm_Step : BaseOrderForm {

        protected void Page_Load(object sender, System.EventArgs e) {
            this.Master.ValSummaryVisibility = false;
        }

        private void SetHeaderAccountInformation() {
            this.lblInstruction.Text = "<p>Before placing an order, review Account and Order Information below.  In Add New Order, modifications to Account Information can <u>only</u> be made to the `Ship To' and will <u>only</u> impact <u>this</u> order.  To permanently update the `Bill To' and/or `Ship To' in the system for this Account, please go to Account List, under Directory [Menu Bar] and modify the data accordingly.</p>";
            this.lblSectionTitle.Text = "Add New Order:";
            this.lblPageTitle.Text = "STEP 3 - Account Information";
        }

        private void SetHeaderOrderDetail() {
            this.lblInstruction.Text = "Enter case quantities for Pro Code [Free Cases], if applicable, and/or Products in the text boxes. Use the Tab key to move the cursor from one text box to another. Note: The system will send an error message, if the Pro Code is greater than the Number of Cases, or if the Pro Code is greater than zero and the Number of Cases is blank or zero. ";
            this.lblSectionTitle.Text = "Add New Order:";
            this.lblPageTitle.Text = "STEP 4 - Order Detail";
        }

        private void SetHeaderOrderInformation() {
            this.lblInstruction.Text = "Complete the required Order Information fields below and if necessary, enter a brief Comment, pertaining to this order, in the Comment field.";
            this.lblSectionTitle.Text = "Add New Order:";
            this.lblPageTitle.Text = " STEP 5 - Order Information";
        }

        private void SetHeaderOrderValidation() {
            this.lblInstruction.Text = "Before completing the order, carefully review ALL the information, especially any notations in the Important Information section. Use Back button to review previous steps and to make corrections, if necessary.";
            this.lblSectionTitle.Text = "Add New Order:";
            this.lblPageTitle.Text = " STEP 6 - Order Validation";
        }

        override protected void OnLoad(EventArgs e) {
            valSum.Visible = false;
            //Load Information Page
            //And InitOrderData (create new row automatically)
            //Retreive also the Account Data Stuff and renew the account if needed
            base.OnLoad(e);

            //Step 3 General Info
            //			OrderForm_InformationStep.DataSource = this.DataSource;	
            //			//Step 4 Product
            //			OrderForm_DetailItemStep.DataSource = this.DataSource;
            //			//Step 5 Supply	
            //			  OrderForm_DetailSupplyItemStep.DataSource = this.DataSource;			
            //			//Step 6 Validation
            //			OrderForm_ValidationStep.DataSource = this.DataSource;
            //Load Control
            if (!IsPostBack)
                this.DisplayManager();
            if (currentStep != null)
                currentStep.DataSource = this.DataSource;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            GetQueryParam();

            this.InitControl();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        private new void InitControl() {
            // RNK
            //base.HiddenChange = hidChange;
            //base.LabelInstruction = lblInstruction;
            //base.LabelMessage = lblMessage;		
            //base.LabelPageTitle = lblPageTitle;
            //base.LabelSectionTitle = lblSectionTitle;
            base.ValSummary = valSum;
            base.PlaceHolderBodyPage = plHoldBodyPage;
            //			this.OrderFormSteps.Add(new BaseOrderFormStep);
            //			this.OrderFormSteps.Add(OrderForm_DetailItemStep);
            //			this.OrderFormSteps.Add(OrderForm_DetailSupplyItemStep);
            //			this.OrderFormSteps.Add(OrderForm_ValidationStep);
        }

        private void GetQueryParam() {
            if (!IsPostBack) {
                if (Request.QueryString["NoMenu"] != null) {
                    int NoMenu = Convert.ToInt32(Request.QueryString["NoMenu"]);
                    this.AppItem = (QSPForm.Business.AppItem)NoMenu;
                }
                else {
                    this.AppItem = QSPForm.Business.AppItem.OrderForm_Step3;
                }
            }

            if (Request["OrderID"] != null) {
                //Edit Mode
                OrderID = Convert.ToInt32(Request["OrderID"]);
            }

            //Add Mode
            if (Request["FormID"] != null) {
                FormID = Convert.ToInt32(Request["FormID"]);
            }
            if (Request["CampID"] != null) {
                CampaignID = Convert.ToInt32(Request["CampID"]);
            }
            if (Request["QCAPID"] != null)
            {
                QCAPOrderID = Convert.ToInt32(Request["QCAPID"]);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            lblAccountNumber.Text = "";
            lblAccountName.Text = "Unspecified";
            lblFormID.Text = "";
            lblFormName.Text = "Unspecified";
            if (CampaignID > 0) {
                lblAccountNumber.Text = AccountNumber;
                if (AccountName.Length > 0)
                    lblAccountName.Text = AccountName;
                trAccountInfoTitle.Visible = true;
            }
            else {
                trAccountInfoTitle.Visible = false;
            }
            if (FormID > 0) {
                lblFormID.Text = FormID.ToString();
                if (FormName.Length > 0)
                    lblFormName.Text = FormName;
                if (FormImageURL.Length > 0) {
                    imgBusinessForm.ImageUrl = FormImageURL;
                    imgBusinessForm.Visible = true;
                }
                else
                    imgBusinessForm.Visible = false;

                trFormInfoTitle.Visible = true;
            }
            else {
                trFormInfoTitle.Visible = false;
                imgBusinessForm.Visible = false;
            }

            if (this.CurrentStep.StepItem == QSPForm.Business.AppItem.OrderForm_Step3)
                SetHeaderAccountInformation();

            if (this.CurrentStep.StepItem == QSPForm.Business.AppItem.OrderForm_Step4)
                SetHeaderOrderDetail();

            if (this.CurrentStep.StepItem == QSPForm.Business.AppItem.OrderForm_Step5)
                SetHeaderOrderInformation();

            if (this.currentStep.StepItem == QSPForm.Business.AppItem.OrderForm_Step7)
                SetHeaderOrderValidation();
        }
    }
}