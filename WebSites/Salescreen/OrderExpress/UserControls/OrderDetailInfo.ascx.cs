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
    /// Summary description for OrganizationForm.
    /// </summary>
    public partial class OrderDetailInfo : BaseWebFormControl {
        private const string IMG_EDIT_ORDER_URL = "images/btnEditOrder.gif";
        private const string IMG_EDIT_ORDER_AND_OR_PERSONALIZATION_URL = "images/btnEditOrderAndOrPersonalization.gif";
        private const string IMG_EDIT_PERSONALIZATION_URL = "images/BtnEditPersonalization.gif";
        private const string IMG_VIEW_PERSONALIZATION_URL = "images/btnViewPersonalization.gif";

        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private int c_OrderID;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidChange;
        protected System.Web.UI.WebControls.Label lblInstruction;
        protected System.Web.UI.WebControls.Image imgTitle;
        public const string ORDER_ID = "OrderID";
        protected System.Web.UI.WebControls.HyperLink HyperLink1;
        private const string ORDER_DATA = "OrderData";
        private const string ACCOUNT_DATA = "AccountData";
        protected dataDef dtsOrder;
        protected AccountData dtsAccount;
        protected System.Web.UI.WebControls.Label lblValidation;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    BindForm();
                    SetHeaderTitle();
                }

                //Set Print Button
                string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.Report_WarehouseStockOrderForm);
                url = url + "&OrderID=" + OrderID;
                imgBtnPrint.Attributes.Add("onclick", "window.open('" + url + "');");

                //Eric Charest, Temporary disable PE edition for fm
                if (this.Page.Role <= QSPForm.Business.AuthSystem.ROLE_FM) {
                    this.imgEditOrderPE.Visible = false;
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
        }
        #endregion

        private void SetDisplayMode() {
        }

        public int OrderID {
            get {
                int orderID = 0;
                if (ViewState[ORDER_ID] != null)
                    orderID = Convert.ToInt32(ViewState[ORDER_ID]);
                return orderID;
            }
            set {
                ViewState[ORDER_ID] = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                //Set the Initial Mode
                string sMessage = "";
                this.imgEditOrder.Visible = false;
                this.imgEditOrderPE.Visible = false;
                if (this.Page.RightUpdate) {
                    //Access Base on Program Type
                    if (dtsAccount.Campaign.Rows.Count > 0) {
                        int ProgTypeID = 0;
                        if (!dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                            ProgTypeID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PROG_TYPE_ID]);

                        if (ProgTypeID == 11 || ProgTypeID == 7) //if it's a WFC Program Account or a Food Program
						{
                            this.imgEditOrder.Visible = false;
                            this.imgEditOrder.ImageUrl = IMG_EDIT_ORDER_URL;
                            //Access Base on Order Status
                            int OrderStatus = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_ORDER_STATUS_ID]);

                            if ((OrderStatus < QSPForm.Common.OrderStatus.RELEASED) || (OrderStatus >= QSPForm.Common.OrderStatus.ERROR_UNSPECIFIED)) {
                                if ((OrderStatus != QSPForm.Common.OrderStatus.ERROR_ALREADY_RELEASED) && (OrderStatus != QSPForm.Common.OrderStatus.ERROR_CONCURENT_MODIFICATION)) {
                                    if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) {
                                        this.imgEditOrder.Visible = true;
                                    }
                                    else {
                                        //Access Base on FM
                                        if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM) {
                                            string fmID = dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FM_ID].ToString();
                                            if (fmID == this.Page.FMID) {
                                                //this.Page.SetPageMessage("The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.");
                                                this.imgEditOrder.Visible = true;
                                            }
                                        }
                                    }
                                }
                                else {
                                    sMessage = "The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.";
                                    this.imgEditOrder.Visible = true;//The modification cannot be processed.  The Order has been released.  <br>Please contact Field Support or press on Edit and Rollback to undo changes.
                                }
                            }
                            else {
                                sMessage = "Due to the status of this order, it cannot be edited.";
                            }

                            //Add logic for Order that contains PE Product.
                            if (this.imgEditOrder.Visible) {
                                //if it's still editable based on the logic of the standard Ordfer Form
                                QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
                                bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(dtsOrder);
                                //Only Apply this rule when the order contains PE
                                if (IsOrderContainsPE) {
                                    this.imgEditOrder.ImageUrl = IMG_EDIT_ORDER_AND_OR_PERSONALIZATION_URL;

                                    int fulfOrderID = 0;
                                    if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FULF_ORDER_ID)) {
                                        if (dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID].ToString().Length > 0)
                                            fulfOrderID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FULF_ORDER_ID]);
                                    }

                                    if ((OrderStatus < QSPForm.Common.OrderStatus.IN_PROCESS) && (fulfOrderID == 0)) {
                                        this.imgEditOrder.Visible = true;
                                        this.imgEditOrderPE.Visible = true;
                                        this.imgEditOrderPE.ImageUrl = IMG_EDIT_PERSONALIZATION_URL;
                                    }
                                    else {
                                        this.imgEditOrder.Visible = false;
                                        this.imgEditOrderPE.Visible = true;
                                        this.imgEditOrderPE.ImageUrl = IMG_VIEW_PERSONALIZATION_URL;
                                    }
                                }
                            }
                        }
                        else {
                            sMessage = "Because this order did not originate in Order Express, it cannot be edited.";
                        }
                    }
                }
                if (!this.imgEditOrder.Visible) {
                    if (sMessage.Length == 0) {
                        sMessage = "This order is not editable.";
                    }
                }
                else
                    sMessage = "";

                this.Page.SetPageMessage(sMessage);
            }

            //Personalize Button
            //imgBtnPersonalize.Visible = false;
            //if (QSPToolBar.EditButton.Visible)
            //{
            //    //QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            //    //bool IsOrderContainsPE = ordSys.IsOrderContainsPEProduct(dtsOrder);
            //    ////imgBtnPersonalize.Visible = IsOrderContainsPE;
            //    //if (IsOrderContainsPE)
            //    //    QSPToolBar.EditButton.Visible = false;
            //}
        }

        protected void SetFormParameter() {
            if (Request[ORDER_ID] != null) {
                c_OrderID = Convert.ToInt32(Request[ORDER_ID].ToString());
            }
            else {
                c_OrderID = 0;
            }
            ViewState[ORDER_ID] = c_OrderID;
        }

        protected new void BindForm() {
            OrderInfo1.BindForm();
            AuditControlInfo1.BindForm();
        }

        private void SetHeaderTitle() {
            int ProgramTypeID = 0;

            if (dtsAccount.Campaign.Rows.Count > 0) {
                DataRow campRow = dtsAccount.Campaign.Rows[0];
                ProgramTypeID = Convert.ToInt32(campRow[CampaignTable.FLD_PROG_TYPE_ID]);
            }
            if (dtsAccount.Account.Rows.Count > 0) {
                DataRow accRow = dtsAccount.Account.Rows[0];
                lblAccountNumber.Text = accRow[AccountTable.FLD_PKID].ToString();
                lblAccountName.Text = accRow[AccountTable.FLD_NAME].ToString();
            }
            int FormID = 0;
            if (dtsOrder.OrderHeader.Rows.Count > 0) {
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
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

                //Get the status of the order to check if it's possible to modified.
                if (ProgramTypeID == 11) {
                    int StatusID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_ORDER_STATUS_ID]);
                    if ((StatusID >= QSPForm.Common.OrderStatus.RELEASED) && (StatusID < QSPForm.Common.OrderStatus.ERROR_UNSPECIFIED)) {
                        this.imgEditOrder.Visible = false;
                        this.Page.SetPageMessage("this order is not editable");
                    }
                }
                else {
                    this.imgEditOrder.Visible = false;
                    this.Page.SetPageMessage("this order is not editable");
                }
            }
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                //Order
                LoadDataSet();
                //Account
                int CampaignID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_CAMPAIGN_ID]);
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailByCampaignID(CampaignID);

                this.ViewState[ORDER_ID] = c_OrderID;
                //this.ViewState[ORDER_DATA] = dtsOrder;
                OrderInfo1.DataSource = dtsOrder;
                OrderInfo1.AccountDataSource = dtsAccount;
                AuditControlInfo1.ParentType = QSPForm.Common.EntityType.TYPE_ORDER_BILLING;
                AuditControlInfo1.DataSource = dtsOrder.OrderHeader;
                AuditControlInfo1.ParentID = c_OrderID;
            }
            else {
                c_OrderID = Convert.ToInt32(this.ViewState[ORDER_ID]);
                //dtsOrder = (dataDef)this.ViewState[ORDER_DATA];
            }

            //For each load, the page (the higher in the hirarchy)
            //is in charge to set all children's datasource 
        }

        private void LoadDataSet() {
            QSPForm.Business.OrderSystem ordSys = new QSPForm.Business.OrderSystem();
            //			if (this.Page.Role == QSPForm.Business.AuthSystem.ROLE_FM)
            //				dtsOrder = ordSys.SelectAllDetail(c_OrderID);
            //			else
            dtsOrder = ordSys.SelectAllDetail(c_OrderID, true);
            ordSys.CalculateOrder(dtsOrder);
        }

        protected void imgEditOrder_Click(object sender, ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderDetail, BaseOrderDetail.ORDER_ID, c_OrderID.ToString());
            string url = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}", OrderID);
            Response.Redirect(url);
        }

        protected void imgEditOrderPE_Click(object sender, ImageClickEventArgs e) {
            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderAwardVisionDetail, BaseOrderDetail.ORDER_ID, c_OrderID.ToString());
            //Response.Redirect(url);

            string url = "~/OrderAwardVisionDetail.aspx?";
            Response.Redirect(url + "&" + BaseOrderDetail.ORDER_ID + "=" + c_OrderID.ToString());
        }
    }
}