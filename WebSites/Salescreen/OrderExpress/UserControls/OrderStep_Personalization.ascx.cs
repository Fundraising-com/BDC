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

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class OrderStep_Personalization : BaseWebUserControl {
        public const string CAMP_ID = "CampID";
        public const string PROG_TYPE_ID = "ProgTypeID";
        public const string ORDER_ID = "OrderID";
        private int c_OrderID = 0;
        private int c_CampID = 0;
        private int c_ProgTypeID = 0;
        private OrderData dtsOrder = new OrderData();
        private QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        override protected void OnLoad(EventArgs e) {
            GetQueryParam();
            //if (!IsPostBack)
            //{				
            BindForm();
            //}
            //else
            //{
            //    c_CampID = Convert.ToInt32(ViewState[CAMP_ID]);
            //    c_ProgTypeID = Convert.ToInt32(ViewState[PROG_TYPE_ID]);
            //}

            base.OnLoad(e);
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

        }
        #endregion

        private void GetQueryParam() {
            if (Request[ORDER_ID] != null) {
                c_OrderID = Convert.ToInt32(Request[ORDER_ID]);
                ViewState[ORDER_ID] = c_OrderID;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public void BindForm() {
            dtsOrder = ordSys.SelectAllDetail(c_OrderID);
            OrderHeaderTable dTblOrder = dtsOrder.OrderHeader;
            DataRow ordRow = dTblOrder.Rows[0];

            c_CampID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            AccountTable dTblAccount = accSys.SelectAllByCampaignID(c_CampID);
            DataRow accRow = dTblAccount.Rows[0];
            int AccountID = Convert.ToInt32(accRow[CampaignTable.FLD_ACCOUNT_ID]);
            lblAccountNumber.Text = AccountID.ToString();
            lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();

            int FormID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FORM_ID]);
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable dTblForm = formSys.SelectOne(FormID);
            DataRow frmRow = dTblForm.Rows[0];
            lblFormID.Text = FormID.ToString();
            lblFormName.Text = frmRow[FormTable.FLD_FORM_NAME].ToString();
            imgBusinessForm.ImageUrl = "~/" + frmRow[FormTable.FLD_IMAGE_URL].ToString();
            SetFrameControl();
            SetMessageButton();
        }

        private void SetFrameControl() {
            //username aet user id
            frPersonalization.Attributes.Add("src", "http://iprint.awardvision.com/QSPStaging/entry.aspx?OrderID=" + c_OrderID.ToString());
        }

        protected void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Order Status		
            //We do nothing cause the perform Validation Method already set the status
            bool IsSuccess = false;
            if (!dtsOrder.OrderDetail.IsPersonalizeComplete) {
                if (dtsOrder.OrderException.Rows.Count > 0)
                    dtsOrder.OrderException.Rows.Clear();
                //Order Status
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.WAIT_FOR_PERSONALIZATION;
            }
            else {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.IN_PROCESS;
            }

            IsSuccess = ordSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            if (IsSuccess) {
                GoToConfirmationPage();
            }
        }

        protected void imgBtnSaveForLater_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Clear Exception
            if (dtsOrder.OrderException.Rows.Count > 0)
                dtsOrder.OrderException.Rows.Clear();
            //Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;

            bool IsSuccess = false;
            IsSuccess = ordSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            if (IsSuccess) {
                GoToConfirmationPage();
            }
        }

        private void GoToConfirmationPage() {
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            string sOrderID = ordRow[OrderHeaderTable.FLD_PKID].ToString();
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step8);
            string url = "~/OrderStep_Confirmation.aspx?";
            Response.Redirect(url + "&" + OrderStep_Confirmation.ORDER_ID + "=" + sOrderID);
        }

        private void SetMessageButton() {
            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Process Order Now?";
            if (dtsOrder.OrderException.IsContainExceptionType((int)QSPForm.Common.BusinessExceptionType.Expedited_Freight_Charges)) {
                if (Page.Role == QSPForm.Business.AuthSystem.ROLE_FM) {
                    sWarningMessage = "This order requires Expedited Freight and the cost will be recovered from your 12-Pay unless the delivery date is changed.  Are you sure you want to proceed?";

                }
                else if (Page.Role > QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) {
                    sWarningMessage = "This order requires Expedited Freight and the cost will be paid by QSP or FSM unless the delivery date is changed.  Are you sure you want to proceed?";
                }
            }

            if (imgBtnConfirm != null)
                imgBtnConfirm.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");

            if (imgBtnSaveForLater != null)
                imgBtnSaveForLater.Attributes.Add("onclick", "return confirm('Are you sure you want to Save/Hold Order and Process Later?');");
        }
    }
}