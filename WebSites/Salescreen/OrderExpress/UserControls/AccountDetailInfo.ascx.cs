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
using dataDef = QSPForm.Common.DataDef.AccountData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for AccountDetail.
    /// </summary>
    public partial class AccountDetailInfo : BaseWebFormControl {
        private int c_AccID = 0;
        public const string CAMP_ID = "CampID";
        private const string ACC_DATA = "AccData";
        protected dataDef dtsAccount;
        public const string ACC_ID = "AccID";

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                LoadData();
                if (!IsPostBack) {
                    BindForm();
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
            QSPToolBar.DisplayMode = ToolBar.DISPLAY_READ;
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.QSPToolBar.EditClick += new EventHandler(QSPToolBar_EditClick);
        }
        #endregion

        protected void SetFormParameter() {
            if (Request[ACC_ID] != null) {
                c_AccID = Convert.ToInt32(Request[ACC_ID].ToString());
            }
            else {
                c_AccID = 0;
            }
            ViewState[ACC_ID] = c_AccID;
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            //Manage Status
            //IF not chocolate or Food program type = 11, 7, the edit right is disabled
            int ProgTypeID = 0;
            if (dtsAccount.Campaign.Rows.Count > 0) {
                if (!dtsAccount.Campaign.Rows[0].IsNull(CampaignTable.FLD_PROG_TYPE_ID))
                    ProgTypeID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PROG_TYPE_ID]);
                if (ProgTypeID == 11 || ProgTypeID == 7) {
                    //Verfifcation on the status of an account
                    int StatusID = 0;
                    StatusID = Convert.ToInt32(dtsAccount.Account.Rows[0][AccountTable.FLD_ACCOUNT_STATUS_ID]);
                    if (StatusID != QSPForm.Common.AccountStatus.CLOSE &&
                        StatusID != QSPForm.Common.AccountStatus.CLOSE_PROCESSED &&
                        StatusID != QSPForm.Common.AccountStatus.CLOSE_IN_PROCESS &&
                        StatusID != QSPForm.Common.AccountStatus.CLOSE_NOT_SUBMITTED) {
                        if (StatusID != QSPForm.Common.AccountStatus.IN_COLLECTION &&
                            StatusID != QSPForm.Common.AccountStatus.IN_COLLECTION_PROCESSED) {
                            QSPToolBar.EditButton.Visible = true;
                            string fmID = dtsAccount.Account.Rows[0][AccountTable.FLD_FM_ID].ToString();
                            if (fmID != this.Page.FMID) {
                                QSPToolBar.EditButton.Visible = false;
                            }
                            if (this.Page.Role > QSPForm.Business.AuthSystem.ROLE_FM) {
                                QSPToolBar.EditButton.Visible = true;
                            }
                            if (!QSPToolBar.EditButton.Visible)
                                Page.SetPageMessage("This Account is not editable");
                        }
                        else {
                            QSPToolBar.EditButton.Visible = false;
                            Page.SetPageMessage("Account In Collection - Please Call QSP Field Support For Assistance");
                        }
                    }
                    else {
                        QSPToolBar.EditButton.Visible = false;
                        Page.SetPageMessage("Account Closed - Please Call QSP Field Support For Assistance");
                    }
                }
                else {
                    QSPToolBar.EditButton.Visible = false;
                    Page.SetPageMessage("Only Accounts running WFC Chocolate Programs or Food Programs can be edited in Order Express at this time");
                }
            }
            else {
                QSPToolBar.EditButton.Visible = false;
                Page.SetPageMessage("The Program Type is missing");
            }
            //Management on edit right
        }

        public override void BindForm() {
            AccountInfo1.BindForm();
            OrderSubList1.BindForm();
            AuditControlInfo1.BindForm();
        }

        protected override void LoadData() {
            if (!IsPostBack) {
                SetFormParameter();
                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                dtsAccount = accSys.SelectAllDetailWithLastCampaign(c_AccID);

                //				if (!accSys.IsRenew(dtsAccount.Account))
                //				{
                //					QSPToolBar.EditButton.ImageUrl = "images/btnRenew.gif";
                //					Page.SetPageMessage("To Renew Account, Click on Renew button below.");
                //					QSPToolBar.EditButton.ToolTip = "Renew account for the current Fiscal Year";
                //				}
                if (dtsAccount.Campaign.Rows.Count > 0) {
                    int CampID = Convert.ToInt32(dtsAccount.Campaign.Rows[0][CampaignTable.FLD_PKID]);
                    OrderSubList1.CampaignID = CampID;
                }

                AccountInfo1.AccountID = c_AccID;
                AccountInfo1.DataSource = dtsAccount;
                AuditControlInfo1.ParentType = QSPForm.Common.EntityType.TYPE_ACCOUNT;
                AuditControlInfo1.ParentID = c_AccID;
                AuditControlInfo1.DataSource = dtsAccount.Account;
            }
            else {
                c_AccID = Convert.ToInt32(ViewState[ACC_ID]);
                //dtsAccount = (dataDef)this.ViewState[ACC_DATA];
            }
        }

        private void QSPToolBar_EditClick(object sender, EventArgs e) {
            // string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountDetail, BaseAccountDetail.ACC_ID, c_AccID.ToString());
            string url = "~/AccountDetail.aspx?AccID=" + c_AccID.ToString();
            Response.Redirect(url);
        }
    }
}