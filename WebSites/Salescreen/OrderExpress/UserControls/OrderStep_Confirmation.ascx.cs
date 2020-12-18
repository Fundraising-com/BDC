using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSP.WebControl.Reporting;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class OrderStep_Confirmation : BaseWebUserControl {
        public const string CAMP_ID = "CampID";
        public const string PROG_TYPE_ID = "ProgTypeID";
        public const string ORDER_ID = "OrderID";
        private int c_OrderID = 0;
        private int c_CampID = 0;
        private int c_ProgTypeID = 0;
        Dictionary<string, string> parameterDictionary = new Dictionary<string,string>();

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
        }
        #endregion

        override protected void OnLoad(EventArgs e)
        {
            GetQueryParam();

            if (!IsPostBack)
            {
                BindForm();
            }
            else
            {
                c_CampID = Convert.ToInt32(ViewState[CAMP_ID]);
                c_ProgTypeID = Convert.ToInt32(ViewState[PROG_TYPE_ID]);
            }

            base.OnLoad(e);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.SetCommandLinks();
        }
        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        private void GetQueryParam()
        {
            if (Request[ORDER_ID] != null)
            {
                c_OrderID = Convert.ToInt32(Request[ORDER_ID]);
                ViewState[ORDER_ID] = c_OrderID;
            }
        }
        public void BindForm() {
            imgBtnCreditApplication.Visible = false;
            lblCreditAppMsg.Visible = false;

            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            OrderHeaderTable dTblOrder = ordSys.SelectOne(c_OrderID);
            DataRow ordRow = dTblOrder.Rows[0];

            c_CampID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            AccountTable dTblAccount = accSys.SelectAllByCampaignID(c_CampID);
            DataRow accRow = dTblAccount.Rows[0];
            int AccountID = Convert.ToInt32(accRow[CampaignTable.FLD_ACCOUNT_ID]);
            lblAccountID.Text = AccountID.ToString();

            lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
            if (!accRow.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                lblAccountName.Text = accRow[AccountTable.FLD_FULF_ACCOUNT_ID].ToString() + " - " + lblAccountName.Text;

            int FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable dTblForm = formSys.SelectOne(FormID);
            DataRow frmRow = dTblForm.Rows[0];
            lblFormID.Text = FormID.ToString();
            lblFormName.Text = frmRow[FormTable.FLD_FORM_NAME].ToString();
            c_ProgTypeID = Convert.ToInt32(frmRow[FormTable.FLD_PROGRAM_TYPE_ID]);

            int StatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
            string sStatus = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_NAME].ToString();
            string sStatusCode = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_COLOR_CODE].ToString();
            string sStatus_ShortDescription = ordRow[OrderHeaderTable.FLD_ORDER_STATUS_SHORT_DESCRIPTION].ToString();

            //			if (StatusID == QSPForm.Common.OrderStatus.WAIT_FOR_APPROVAL)
            //			{
            //				lblCreditAppMsg.Visible = true;
            //				imgBtnCreditApplication.Visible = true;
            //				CommonUtility clsUtil = new CommonUtility();
            //				clsUtil.SetJScriptForOpenDetail(imgBtnCreditApplication,QSPForm.Business.AppItem.CreditApplicationDetail,CreditApplicationDetail.ACC_ID,AccountID.ToString(),0,0);				
            //			}

            lblOrderID.Text = c_OrderID.ToString();
            lblOrderStatus_ShortDescription.Text = sStatus_ShortDescription;
            lblOrderStatusColor.BackColor = Color.FromName(sStatusCode);

            //string message = "The order # " + c_OrderID.ToString() + " have been saved sucessfully.<br>" + 
            //	"The status is '" + sStatus + "'"; 
            //			lblMessageConfirmation.Text = message;
            ViewState[CAMP_ID] = c_CampID;
            ViewState[PROG_TYPE_ID] = c_ProgTypeID;

            //PrintFormReport.ReportName = frmRow[FormTable.FLD_FORM_NAME].ToString();

            string formName = frmRow[FormTable.FLD_FORM_NAME].ToString();
            string replaces = ";?:@&=+$/";

            for (int i = 0; i < replaces.Length; i++) {
                formName = formName.Replace(replaces[i].ToString(), "");
            }

            #region Load the charge list

            this.ChargeList1.OrderId = c_OrderID;
            this.ChargeList1.LoadData();
            this.ChargeList1.BindList();

            if (ChargeList1.TotalChargeCount > 0) {
                this.trChargeList.Visible = true;
            }
            else {
                this.trChargeList.Visible = false;
            }

            #endregion
        }

        private void SetCommandLinks()
        {
            this.hlOrderPrint.NavigateUrl = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}&IsForPrint=true", c_OrderID);
            this.hlOrderCreate.NavigateUrl = string.Format("~/OrderStep_Selection.aspx?CampID={0}", c_CampID);
            this.hlOrderSearch.NavigateUrl = "~/V2/Forms/OrderSearch.aspx";
        }
   }
}