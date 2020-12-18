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
using dataDef = QSPForm.Common.DataDef.FormData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for BaseAccountForm.
    /// </summary>
    public partial class BusinessForm_Step : BaseBusinessForm {
        protected System.Web.UI.WebControls.HyperLink HyperLink1;

        protected void Page_Load(object sender, System.EventArgs e) {

        }

        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                GetQueryParam();
            }
            //Load Information Page
            //And InitOrderData (create new row automatically)
            base.OnLoad(e);

            //Step 2
            Information_Step.DataSource = this.DataSource;
            //Step 3
            BusinessRule_Step.DataSource = this.DataSource;
            //Step 4
            //BusinessException_Step.DataSource = this.DataSource;
            //Step 5
            BusinessTask_Step.DataSource = this.DataSource;

            //Load Control
            if (!IsPostBack) {
                DisplayManager();
            }
            //Turn off the Val Sum of the page it's already handled
            //by a web user control behind
            //valSum.Visible = false;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitControl();
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

        private new void InitControl() {
            //base.HiddenChange = hidChange;
            //base.LabelInstruction = lblInstruction;
            //base.LabelMessage = lblMessage;		
            //base.LabelPageTitle = lblPageTitle;
            //base.LabelSectionTitle = lblSectionTitle;
            base.ValSummary = valSum;
            this.BusinessFormSteps.Add(Information_Step);
            this.BusinessFormSteps.Add(BusinessRule_Step);
            this.BusinessFormSteps.Add(BusinessException_Step);
            this.BusinessFormSteps.Add(BusinessTask_Step);
        }

        private void GetQueryParam() {
            if (Request.QueryString["NoMenu"] != null) {
                int NoMenu = Convert.ToInt32(Request.QueryString["NoMenu"]);
                this.AppItem = (QSPForm.Business.AppItem)NoMenu;

                //Initialization of the Order
                if (Request["BaseFormID"] != null) {
                    this.BaseFormID = Convert.ToInt32(Request["BaseFormID"]);
                }

                //				if (Request["FormID"] != null)
                //				{
                //					this.FormID = Convert.ToInt32(Request["FormID"]);
                //				}
            }
        }

        private void SetHeaderGeneralInformation() {
            this.lblInstruction.Text = "Edit All General Information of the Form";
            this.lblSectionTitle.Text = "Add a New Form";
            this.lblPageTitle.Text = "STEP 2 - General Information";
        }

        private void SetHeaderBusinessRule() {
            this.lblInstruction.Text = "Click on Add Button to add new Business Rule for this form.";
            this.lblSectionTitle.Text = "Add a New Form";
            this.lblPageTitle.Text = "STEP 3 - Business Rule";
        }

        private void SetHeaderBusinessException() {
            this.lblInstruction.Text = "Click on Add Button to add new Business Exception for this form.";
            this.lblSectionTitle.Text = "Add a New Form";
            this.lblPageTitle.Text = "STEP 4 - Business Exception";
        }

        private void SetHeaderBusinessTask() {
            this.lblInstruction.Text = "Click on Add Button to add new Business Task for this form.";
            this.lblSectionTitle.Text = "Add a New Form";
            this.lblPageTitle.Text = "STEP 5 - Business Task";
        }

        private void ReBindPage() {
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (BaseFormID > 0) {
                lblBaseFormCode.Text = BaseFormCode;
                lblBaseFormName.Text = BaseFormName;
                trFormInfoTitle.Visible = true;
            }
            else {
                trFormInfoTitle.Visible = false;
            }

            if (this.currentStep.StepItem == QSPForm.Business.AppItem.BusinessForm_Step2)
                SetHeaderGeneralInformation();
            if (this.currentStep.StepItem == QSPForm.Business.AppItem.BusinessForm_Step3)
                SetHeaderBusinessRule();
            if (this.currentStep.StepItem == QSPForm.Business.AppItem.BusinessForm_Step4)
                SetHeaderBusinessException();
            if (this.currentStep.StepItem == QSPForm.Business.AppItem.BusinessForm_Step5)
                SetHeaderBusinessTask();
        }
    }
}
