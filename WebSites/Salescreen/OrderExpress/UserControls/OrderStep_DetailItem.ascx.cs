using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using System.Configuration;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Step1.
    /// </summary>
    public partial class OrderStep_DetailItem : BaseOrderFormStep {
        private CommonUtility util = new CommonUtility();
        protected OrderDetailTable dTblOrderDetail;
        private bool IsPersonalizationMode = false;
        private bool IsOrderContainsPE = false;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here						
            this.OrderDetailSectionListStep.FormID = this.Page.FormID;
            if (ViewState["IsOrderContainsPE"] != null)
                IsOrderContainsPE = Convert.ToBoolean(ViewState["IsOrderContainsPE"]);
            
            trQCAPOrderIntimation.Visible = this.Page.QCAPOrderID != 0;
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
            //this.imgBtnPersonalize.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnPersonalize_Click);
            this.imgBtnSkip.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSkip_Click);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
            this.ddlProfitRate.SelectedIndexChanged += new EventHandler(ddlProfitRate_SelectedIndexChanged);
        }

        protected void ddlProfitRate_SelectedIndexChanged(object sender, EventArgs e) {
            //SavedProfitRate = ddlProfitRate.SelectedValue;
            EmptyQuantity();
            BindOrderDetail();
        }
        #endregion

        public bool hasSetupOrderProfitRate {
            get {
                bool setup = false;
                if (this.ViewState["hasSetupOrderProfitRate"] != null)
                    setup = Convert.ToBoolean(this.ViewState["hasSetupOrderProfitRate"].ToString());
                return setup;
            }
            set { this.ViewState["hasSetupOrderProfitRate"] = value; }
        }

        private decimal SelectedProfitRate {
            get {
                decimal pr = 0;
                if (tblProfitRate.Visible) {
                    if (ddlProfitRate.SelectedIndex > -1)
                        pr = Convert.ToDecimal(ddlProfitRate.SelectedValue);
                }
                return pr;
            }
        }

        private void InitControl() {
            this.PreviousAppItem = QSPForm.Business.AppItem.OrderForm_Step3;
            this.StepItem = QSPForm.Business.AppItem.OrderForm_Step4;
            this.NextAppItem = QSPForm.Business.AppItem.OrderForm_Step5;
            this.ImageButtonBack = imgBtnBack;
            this.ImageButtonNext = imgBtnNext;
        }

        private void ManageDisplayForPersonalization() {
            //Contains Personalization Product
            //			if (IsOrderContainsPE) 
            //			{
            //				imgBtnNext.Visible= false;
            //				imgBtnSkip.Visible= false;
            //				imgBtnPersonalize.Visible = true;
            //				
            //			}
            //			else
            //			{
            //				imgBtnPersonalize.Visible = false;
            //				imgBtnNext.Visible= true;
            //				imgBtnSkip.Visible= true;
            //				
            //			}
            //imgBtnPersonalize.Visible = IsOrderContainsPE;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetJScriptForNextButton();
            //ManageDisplayForPersonalization();
            ViewState["IsOrderContainsPE"] = IsOrderContainsPE;
        }

        protected override void SetInnerControlDataSource() {
            dTblOrderDetail = this.DataSource.OrderDetail;
            //Standard Product
            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = this.DataSource;
            //Optional Product
            OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
            OrderDetailSectionListStep_Optional.DataSource = this.DataSource;
        }

        public override void BindForm() {
            if (IsFirstLoad) {
                if (this.Page.DataOperation == QSPForm.Common.DataOperation.INSERT) {
                    base.SetDefaultFormProduct();
                }

            }
            AdjustProfitRate();

            BindOrderDetail();

            if (!this.DataSource.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                TbStrp_Form.Items.Remove(TbStrp_Form.Items[1]);
                lblNonAvailableOptionalSectionType.Visible = true;
                OrderDetailSectionListStep_Optional.Visible = false;
            }
            //else //moved to bindorderdetail
            //{
            //    lblNonAvailableOptionalSectionType.Visible = false;
            //    OrderDetailSectionListStep_Optional.Visible = true;
            //    OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
            //    OrderDetailSectionListStep_Optional.ProfitRate = this.SelectedProfitRate;
            //    OrderDetailSectionListStep_Optional.DataSource = this.DataSource;
            //    OrderDetailSectionListStep_Optional.BindForm();
            //}
        }

        protected void Page_DataBinding(object sender, EventArgs e) {
            //BindForm();
        }

        public override bool Update() {
            bool IsSucess = false;
            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = this.DataSource;
            IsSucess = OrderDetailSectionListStep.UpdateDataSource();
            if (this.DataSource.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.DataSource = this.DataSource;
                IsSucess = OrderDetailSectionListStep_Optional.UpdateDataSource();
            }

            CommonUtility clsUtil = new CommonUtility();
            if (this.SelectedProfitRate != 0) {
                clsUtil.UpdateRow(this.DataSource.OrderHeader.Rows[0], QSPForm.Common.DataDef.OrderHeaderTable.FLD_PROFIT_RATE, SelectedProfitRate.ToString());
            }

            ////update campaign if the profit rate change. 
            //Business.CampaignSystem campSys = new QSPForm.Business.CampaignSystem();
            //CampaignTable campTbl = campSys.SelectOne(Convert.ToInt32(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID].ToString()));
            //decimal camp_pr = 0;
            //if (!campTbl.Rows[0].IsNull(CampaignTable.FLD_PROFIT_RATE))
            //    camp_pr = Convert.ToDecimal(campTbl.Rows[0][QSPForm.Common.DataDef.CampaignTable.FLD_PROFIT_RATE].ToString());
            //if (camp_pr != this.SelectedProfitRate)
            //{
            //    clsUtil.UpdateRow(campTbl.Rows[0], CampaignTable.FLD_PROFIT_RATE, this.SelectedProfitRate.ToString());                
            //    campSys.Update(campTbl);
            //}

            //CalculateTax();
            return IsSucess;
        }

        public override bool ValidateForm() {
            bool IsValid = false;
            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            if (IsPEForm)
                OrderDetailSectionListStep.PreparePEForm();

            IsValid = OrderDetailSectionListStep.ValidateForm();
            //if(IsValid)
            //    IsValid = OrderDetailSectionListStep.ValidatePEForm();

            if (IsValid) {
                if (this.DataSource.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                    OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                    IsValid = OrderDetailSectionListStep_Optional.ValidateForm();
                }
            }

            return IsValid;
        }

        public bool ValidateBusinessRules() {
            bool IsValid = false;
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();

            //IsValid = ordSys.PerformMandatoryValidation(this.DataSource, this.Page.UserID, QSPForm.Common.DataOperation.INSERT);
            //string sMessage = "";
            //if (!IsValid)
            //{
            //    if (this.DataSource.OrderException.Rows.Count > 0)
            //    {
            //        sMessage = this.DataSource.OrderException.Rows[0][EntityExceptionTable.FLD_MESSAGE].ToString();
            //        Page.SetPageMessage(sMessage);
            //        //SetJScriptForAlertValidation(sMessage);

            //    }
            //}
            return IsValid;
        }

        private void imgBtnSkip_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            this.Page.GoNextByAppItem(QSPForm.Business.AppItem.OrderForm_Step7);
        }

        protected override void OnGoToNextStep(System.EventArgs e) {
            //if (ValidateForm())
            //{
            //    Update();
            //    //Do a second Validation based on business rules aand exceptions
            //    if (ValidateBusinessRules())
            //    {
            //        Page.AppItem = NextAppItem;
            //        Page.GoToStepByAppItem();
            //    }
            //}
            try {
                base.OnGoToNextStep(e);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        protected override void OnGoToPreviousStep(System.EventArgs e) {
            base.OnGoToPreviousStep(e);
        }

        private void SetJScriptForNextButton() {

            imgBtnNext.Attributes.Add("onclick", " return WarnEmptyList();");
            int minTotalQty = 0;
            int maxTotalQty = 0;
            QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
            minTotalQty = bizSys.GetMinTotalQuantity(this.Page.FormID, QSPForm.Common.FormSectionType.STANDARD_PRODUCT);
            maxTotalQty = bizSys.GetMaxTotalAmount(this.Page.FormID, QSPForm.Common.FormSectionType.STANDARD_PRODUCT);
            if (minTotalQty == 0)
                minTotalQty = minTotalQty + 1;
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	function WarnEmptyList() {	\n");
            strBuild.Append("		var ctlTotQty = document.getElementById('" + OrderDetailSectionListStep.HiddenMinTotalQuantityClientID + "');   \n");
            //strBuild.Append("	    alert('Get = " + OrderDetailSectionListStep.HiddenMinTotalQuantityClientID + "'); \n");
            strBuild.Append("		var totQty  = ctlTotQty.value; \n");
            //strBuild.Append("	    alert('totQty=' + totQty); \n");
            strBuild.Append("	    if (totQty < " + minTotalQty.ToString() + ") { \n");
            //if(minTotalQty == 1)
            //    strBuild.Append("	        alert('Please select at least the minimum requested number of case'); \n");
            //else
            //    strBuild.Append("	        alert('The Minimum for an order is " + minTotalQty.ToString() + " cases!'); \n");
            strBuild.Append("	        alert('Quantity Does NOT Meet Minimum Requirement!'); \n");
            strBuild.Append("           return false;\n");
            strBuild.Append("       }\n");
            strBuild.Append("}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");
            this.Page.RegisterClientScriptBlock("WarnEmptyList", strBuild.ToString());
        }

        //private void SetJScriptForAlertValidation(string msg)
        //{
        //    StringBuilder strBuild = new StringBuilder();
        //    strBuild.Append("<script language=javascript>\n");
        //    strBuild.Append("<!--			\n");
        //    strBuild.Append("	function SetJScriptForAlertValidation() {	\n");
        //    strBuild.Append("	    alert('" + msg + "'); \n");
        //    strBuild.Append("   }\n");
        //    strBuild.Append("   SetJScriptForAlertValidation();\n");
        //    strBuild.Append("//-->\n");
        //    strBuild.Append("</script>");
        //    this.Page.RegisterClientScriptBlock("SetJScriptForAlertValidation", strBuild.ToString());
        //}

        private void BindOrderDetail() {
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(this.DataSource);
            OrderDetailSectionListStep.FormSectionTypeID = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
            OrderDetailSectionListStep.DataSource = this.DataSource;
            OrderDetailSectionListStep.ProfitRate = this.SelectedProfitRate;
            OrderDetailSectionListStep.DisableQtyValidator = IsPEForm;
            OrderDetailSectionListStep.BindForm();

            //update optional product - Eric Charest 
            //moved from bind_form to here.
            if (this.DataSource.OrderDetail.IsContainFormSectionType(QSPForm.Common.FormSectionType.OTHER_PRODUCT)) {
                lblNonAvailableOptionalSectionType.Visible = false;
                OrderDetailSectionListStep_Optional.Visible = true;
                OrderDetailSectionListStep_Optional.FormSectionTypeID = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                OrderDetailSectionListStep_Optional.ProfitRate = this.SelectedProfitRate;
                OrderDetailSectionListStep_Optional.DataSource = this.DataSource;
                OrderDetailSectionListStep_Optional.BindForm();
            }
        }

        private void AdjustProfitRate() {
            //if (!IsPostBack)
            FillProfitRate();


            if (ddlProfitRate.Items.Count == 0) {
                tblProfitRate.Visible = false;
                tblProfitRate.Attributes.Add("display", "none");
                tblProfitRate.Style.Add("display", "none");
            }
            else {
                int campaignID = Convert.ToInt32(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID].ToString());
                QSPForm.Business.FormSystem formSystem = new QSPForm.Business.FormSystem();
                FormTable formTable = formSystem.SelectOne(Page.FormID);

                // TODO: Replace the hardcoded Form Group ID with the form type when available
                if ((int)formTable.Rows[0][FormTable.FLD_FORM_GROUP_ID] == 37) {     // Frozen Food Order
                    QSPForm.Business.ProgramAgreementSystem programAgreementSystem = new QSPForm.Business.ProgramAgreementSystem();
                    ProgramAgreementTable programAgreementTable = programAgreementSystem.SelectAllByCampaignID(campaignID);
                    DataRow[] programAgreementRows = programAgreementTable.Select(ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID + " IN (301, 302)");

                    if (programAgreementRows.Length > 0) {
                        ddlProfitRate.Enabled = false;
                        SetSelectedProfitRate(programAgreementRows[0][ProgramAgreementTable.FLD_PROFIT_RATE].ToString());
                    }
                }
                else {
                    QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                    ordSys.FetchByCampaingAndForm(campaignID, this.Page.FormID);
                    if (ordSys.HasOrder) {
                        //if (ordSys.HasProcessedOrder)
                        //{
                        //    ddlProfitRate.Enabled = false;
                        //}
                        //else if (this.Page.Role <= QSPForm.Business.AuthSystem.ROLE_FM) 
                        //{
                        //    ddlProfitRate.Enabled = false;   
                        //}
                        ddlProfitRate.Enabled = false;
                        SetSelectedProfitRate(ordSys.ProfitRate.ToString());
                    }
                    else {
                        if (!hasSetupOrderProfitRate) {
                            if (!DataSource.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_PROFIT_RATE)) {
                                if (DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString() != "0") {
                                    SetSelectedProfitRate(DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                                }
                                else {
                                    SetSelectedProfitRate("0.4");
                                }
                            }
                            hasSetupOrderProfitRate = true;
                        }
                    }
                }
            }
        }

        private void FillProfitRate() {
            ddlProfitRate.Items.Clear();

            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();

            DataTable tbl = comSys.SelectAllProfitRateByForm(this.Page.FormID);
            Decimal rate;
            if (tbl.Rows.Count > 0) {
                foreach (DataRow r in tbl.Rows) {
                    rate = Convert.ToDecimal(r[0].ToString());
                    rate = rate * 100;
                    ddlProfitRate.Items.Add(new ListItem(rate.ToString() + " %", r[0].ToString()));
                }
                if (!this.DataSource.OrderHeader.Rows[0].IsNull(QSPForm.Common.DataDef.OrderHeaderTable.FLD_PROFIT_RATE)) {
                    SetSelectedProfitRate(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                }
            }
        }

        private void SetSelectedProfitRate(string value) {
            if (value == "0" || value == "" || value == "0.50")
                value = "0.5";
            if (value == "0.40")
                value = "0.4";
            for (int i = 0; i < ddlProfitRate.Items.Count; i++) {
                if (ddlProfitRate.Items[i].Value == value.ToString()) {
                    ddlProfitRate.SelectedIndex = i;
                    break;
                }
            }
        }

        private bool IsPEForm {
            get {
                int FormID = 0;
                if (!this.DataSource.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    FormID = Convert.ToInt32(this.DataSource.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);

                bool ispe = false;
                foreach (string s in PEForm) {
                    if (s == FormID.ToString()) {
                        ispe = true;
                        break;
                    }
                }
                return ispe;
            }
        }

        private string[] PEForm {
            get {
                string peForm = "";
                if (ConfigurationManager.AppSettings["PEForm"] != null)
                    peForm = ConfigurationManager.AppSettings["PEForm"].ToString();

                if (peForm.Length > 0)
                    return peForm.Split(',');
                else
                    return null;
            }
        }

        private void EmptyQuantity() {
            foreach (DataRow r in this.DataSource.OrderDetail.Rows) {
                r[OrderDetailTable.FLD_QUANTITY] = 0;
                r[OrderDetailTable.FLD_ADJUSTMENT_QUANTITY] = 0;
            }
        }
    }
}