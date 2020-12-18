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
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class OrderAwardVisionDetail : BaseWebFormControl {
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int orderID = 0;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        private OrderData dtsOrder = new OrderData();

        public const string ORDER_ID = "OrderID";

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    SetDisplayButton();
                    BindForm();
                    SetHeaderTitle();
                    SetVisibility();
                }
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
            this.imgBtnConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnConfirm_Click);
        }
        #endregion

        public int OrderID {
            get {
                return orderID;
            }
            set {
                orderID = value;
                ViewState[ORDER_ID] = orderID;
            }
        }

        private void SetHeaderTitle() {
            int FormID = 0;
            if (dtsOrder.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                if (!ordRow.IsNull(OrderHeaderTable.FLD_CAMPAIGN_ID)) {
                    int CampaignID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_CAMPAIGN_ID]);
                    QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                    AccountData dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);
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
                            imgBusinessForm.ImageUrl = "~/" + formRow[FormTable.FLD_IMAGE_URL].ToString();
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

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected void SetFormParameter() {
            if (Request[ORDER_ID] != null) {
                OrderID = Convert.ToInt32(Request[ORDER_ID].ToString());
            }
            else {
                OrderID = 0;
            }
            ViewState[ORDER_ID] = OrderID;
        }

        public override void BindForm() {
            SetFrameControl();
        }

        private void SetFrameControl() {
            //username aet user id
            frPersonalization.Attributes.Add("src", "http://iprint.awardvision.com/QSPStaging/entry.aspx?OrderID=" + OrderID.ToString());
        }

        protected override void LoadData() {
            SetFormParameter();
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            dtsOrder = ordSys.SelectAllDetail(OrderID);
        }

        private void SetDisplayButton() {
            string sWarningMessage = "";
            sWarningMessage = "Are you sure you want to Process Order Now?";

            imgBtnConfirm.Attributes.Add("onclick", "return confirm('" + sWarningMessage + "');");
            imgBtnSaveForLater.Attributes.Add("onclick", "return confirm('Are you sure you want to Save/Hold Order and Process Later?');");

            //Apply Visibility on Saved For Later.
        }

        protected void imgBtnConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Order Status		
            //We do nothing cause the perform Validation Method already set the status
            bool IsSuccess = false;
            if (!dtsOrder.OrderDetail.IsPersonalizeComplete) {
                DeleteAllExceptionRow();
                //Order Status
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.WAIT_FOR_PERSONALIZATION;
            }
            else {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.IN_PROCESS;
            }

            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            IsSuccess = ordSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            if (IsSuccess) {
                GoToReadOnlyPage();
            }
        }

        protected void imgBtnSaveForLater_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            //Clear Exception
            DeleteAllExceptionRow();
            //Order Status
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID] = QSPForm.Common.OrderStatus.SAVED_FOR_LATER;

            bool IsSuccess = false;
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            IsSuccess = ordSys.UpdateAllDetail(dtsOrder, this.Page.UserID);
            if (IsSuccess) {
                GoToReadOnlyPage();
            }
        }

        private void GoToReadOnlyPage() {
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
            string sOrderID = ordRow[OrderHeaderTable.FLD_PKID].ToString();
            // string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetailInfo);
            string url = "~/OrderDetailInfo.aspx?";
            Response.Redirect(url + "&" + BaseOrderDetail.ORDER_ID + "=" + OrderID.ToString());
        }

        private void DeleteAllExceptionRow() {
            QSPForm.Common.DataDef.EntityExceptionTable dTblExc = dtsOrder.OrderException;
            if (dTblExc.Rows.Count > 0) {
                foreach (DataRow row in dTblExc.Rows) {
                    if (row.RowState != DataRowState.Deleted) {
                        row.Delete();
                    }
                }
            }
        }

        private void SetVisibility() {
            int fulfOrderID = 0;
            DataRow ordRow = dtsOrder.OrderHeader.Rows[0];

            if (!ordRow.IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID) && ordRow[OrderHeaderTable.FLD_FULF_ORDER_ID].ToString().Length > 0) {
                fulfOrderID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_FULF_ORDER_ID]);
            }

            tblSave.Visible = (Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]) < QSPForm.Common.OrderStatus.IN_PROCESS && fulfOrderID == 0);
        }
    }
}