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
using System.Text;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for MainPage.
    /// </summary>
    public partial class OrderStep_Search : BaseWebFormControl {
        protected System.Web.UI.WebControls.ImageButton imgBtnBack;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label lblFormCode;
        protected System.Web.UI.WebControls.Label lblFormName;
        protected System.Web.UI.HtmlControls.HtmlTableRow trCampInfoTitle;
        protected System.Web.UI.HtmlControls.HtmlTableRow trFormInfoTitle;

        protected void Page_Load(object sender, System.EventArgs e) {
        }

        override protected void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.AccountForm_Step1);
                string url = "~/AccountStep_Search.aspx";
                hypLnkNewAccount.NavigateUrl = url;
            }

            //Load Information Page
            //And InitOrderData (create new row automatically)
            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            this.InitControl();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.AccountList_AddOrderStep.SelectedIndexChanged += new EventHandler(AccountList_AddOrderStep_SelectedIndexChanged);

        }
        #endregion

        private void InitControl() {
            AccountList_AddOrderStep.SearchAppItem = QSPForm.Business.AppItem.AccountList;
            //AddOrderListStep.ButtonVisible = false; 
        }

        private void GetQueryParam() {
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public void GoToNextStep() {
            if (AccountList_AddOrderStep.SelectedValue > -1) {
                //if the Account is renewed go to Step 2 (Form Selection)
                //if not we go to extra Step, the Step 1.1 where the user have to
                //to renew his account before ordering			

                int AccID = AccountList_AddOrderStep.SelectedValue;

                QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
                QSPForm.Common.DataDef.AccountTable accTable = new AccountTable();
                accTable = accSys.SelectOne(Convert.ToInt32(AccID));
                if (accTable.Rows.Count > 0) {
                    bool IsInCollection = false;
                    bool IsClosed = false;
                    DataRow row = accTable.Rows[0];
                    int StatusID = 0;
                    if (!row.IsNull(AccountTable.FLD_ACCOUNT_STATUS_ID))
                        StatusID = Convert.ToInt32(row[AccountTable.FLD_ACCOUNT_STATUS_ID]);
                    //Look for In Collection State
                    if (StatusID == QSPForm.Common.AccountStatus.CLOSE ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_PROCESSED ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_IN_PROCESS ||
                                StatusID == QSPForm.Common.AccountStatus.CLOSE_NOT_SUBMITTED) {
                        IsClosed = true;
                        CommonUtility clsUtil = new CommonUtility();
                        SetJScriptForAlertValidation("Account Closed - Please Call QSP Field Support For Assistance");
                        AccountList_AddOrderStep.SelectedIndex = -1;
                    }
                    else if (StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION ||
                             StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION_PROCESSED) {
                        IsInCollection = true;
                        CommonUtility clsUtil = new CommonUtility();
                        SetJScriptForAlertValidation("Account In Collection - Please Call QSP Field Support For Assistance");
                        AccountList_AddOrderStep.SelectedIndex = -1;
                    }
                    if (!IsInCollection && !IsClosed) {
                        //If not do a verification in AS400 
                        int FulfAccountID = 0;
                        if (!row.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                            FulfAccountID = Convert.ToInt32(row[AccountTable.FLD_FULF_ACCOUNT_ID]);
                        if (FulfAccountID > 0) {//Do the verification
                            try {
                                ARMCUSPTable dTblSyncAcc = accSys.SelectOne_FromSynch(FulfAccountID);
                                if (dTblSyncAcc.Rows.Count > 0) {
                                    DataRow syncRow = dTblSyncAcc.Rows[0];
                                    int InColDay = 0;
                                    int InColMonth = 0;
                                    int InColYear = 0;
                                    if (!syncRow.IsNull(ARMCUSPTable.FLD_COLL_DAY))
                                        InColDay = Convert.ToInt32(syncRow[ARMCUSPTable.FLD_COLL_DAY]);
                                    if (!syncRow.IsNull(ARMCUSPTable.FLD_COLL_MONTH))
                                        InColMonth = Convert.ToInt32(syncRow[ARMCUSPTable.FLD_COLL_MONTH]);
                                    if (!syncRow.IsNull(ARMCUSPTable.FLD_COLL_YEAR))
                                        InColYear = Convert.ToInt32(syncRow[ARMCUSPTable.FLD_COLL_YEAR]);

                                    if ((InColDay + InColMonth + InColYear) > 0) {
                                        IsInCollection = true;
                                        CommonUtility clsUtil = new CommonUtility();
                                        SetJScriptForAlertValidation("Account In Collection - Please Call QSP Field Support For Assistance");
                                        AccountList_AddOrderStep.SelectedIndex = -1;
                                    }
                                    else {//Additional Verification for when it's closed 
                                        string sStatus = "";
                                        sStatus = syncRow[ARMCUSPTable.FLD_STATUS].ToString();
                                        if (sStatus == "C") {
                                            IsClosed = true;
                                            CommonUtility clsUtil = new CommonUtility();
                                            SetJScriptForAlertValidation("Account Closed - Please Call QSP Field Support For Assistance");
                                            AccountList_AddOrderStep.SelectedIndex = -1;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex) {
                                //Page.SetPageError(ex);
                            }
                            //We don't stop the process, the AS400 will validate in case of.
                        }
                    }
                    if (!IsInCollection && !IsClosed) {
                        //bool IsRenew = false;
                        //IsRenew = accSys.IsRenew(accTable);

                        int accountId = Convert.ToInt32(row[AccountTable.FLD_PKID]);
                        int programTypeId = Convert.ToInt32(Request.QueryString["ProgType"]);
                        int currentCampaignId = 0;
                        bool IsRenewalRequired = accSys.IsRenewalRequired(accountId, programTypeId, out currentCampaignId);

                        if (!IsRenewalRequired)
                        {
                            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step2);
                            // RNK
                            string url = "OrderStep_Selection.aspx?";
                            Response.Redirect(url + "&CampID=" + currentCampaignId);
                        }
                        else 
                        {
                            //RNK
                            //string url = this.Page.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step1_1);
                            string url = "OrderStep_AccountRenewal.aspx?";
                            Response.Redirect(url + "&AccID=" + AccID.ToString());
                        }
                    }
                }
            }
        }

        private void AccountList_AddOrderStep_SelectedIndexChanged(object sender, EventArgs e) {
            GoToNextStep();
        }

        private void SetJScriptForAlertValidation(string msg) {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	function SetJScriptForAlertValidation() {	\n");
            strBuild.Append("	    alert('" + msg + "'); \n");
            strBuild.Append("   }\n");
            strBuild.Append("   SetJScriptForAlertValidation();\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");
            this.Page.RegisterStartupScript("SetJScriptForAlertValidation", strBuild.ToString());
        }
    }
}