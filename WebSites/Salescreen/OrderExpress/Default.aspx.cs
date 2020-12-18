using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using QSP.OrderExpress.Web;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using System.Drawing;
using QSPForm.Common;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Business.Entity;

namespace QSP.OrderExpress.Web {
    public partial class Default : BasePage {
        List<QCAPOrderData> qcapOrderData;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetHeader();
                SetOtherInfo();
                SetStatusInfo();

                ViewState["SortColumn"] = "Id";
                ViewState["SortDirection"] = SortDirection.Ascending;

                SetQCAPOrderInfo(0);

                if (this.Role == AuthSystem.ROLE_SUPER_USER) {
                    trAccount.Visible = true;
                    trOrganization.Visible = true;
                    trOrder.Visible = true;
                }
            }
        }

        protected void SetHeader() {
            lblInstruction.Text = "This proprietary QSP Sales Tool will help you manage account and order information to successfully support your customers with their QSP Fundraising Programs." +
             "<br><br>To begin, go to the Menu Bar above to access menu options.";
        }

        private void SetOtherInfo() {
            //Setup Status	

            //Last Visit
            QSPForm.Business.AuthSystem authSys = new QSPForm.Business.AuthSystem();
            DateTime lastVisit = authSys.LastVisit(UserID, RegistryID);
            if (lastVisit != DateTime.MinValue)
                lblLstVisitDate.Text = lastVisit.ToString("MM/dd/yyyy hh:mm");

            //Today's Date
            lblTodayName.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            //User Info
            UserTable user = new UserTable();
            UserSystem userSys = new UserSystem();
            user = userSys.SelectOne(this.UserID);
            if (user.Rows.Count > 0) {
                DataRow userRow = user.Rows[0];
                lblUserName.Text = userRow[UserTable.FLD_FIRST_NAME].ToString() + " " + userRow[UserTable.FLD_LAST_NAME].ToString();
            }
        }

        private void SetStatusInfo() {
            //Setup Status
            LnkBtnStatusMsg.Visible = false;
            string msg = "";

            //When Role is FM
            if (this.Role == AuthSystem.ROLE_FM) {
                int orderCount = 0;
                QSPForm.Business.OrderSystem orderSys = new QSPForm.Business.OrderSystem();
                orderCount = orderSys.CountAllByFMID(this.FMID, 5);
                if (orderCount > 0) {
                    msg = "You have " + orderCount.ToString() + " order(s) in Pending Approval Status";
                    LnkBtnStatusMsg.ForeColor = Color.Red;
                }
                if (msg.Length > 0)
                    msg = msg + "<br><br>Click here to access the Order List";
                LnkBtnStatusMsg.Visible = true;

            }
            else if (this.Role > AuthSystem.ROLE_FM) {
                int todoCount = 0;
                int NoteCount = 0;
                QSPForm.Business.BusinessNotificationSystem todoSys = new QSPForm.Business.BusinessNotificationSystem();
                BusinessNotificationTable dTblNote = new BusinessNotificationTable();
                dTblNote = todoSys.SelectAllByAssignedUserID(this.UserID);
                DataView dv = new DataView(dTblNote);
                dv.RowFilter = "[" + BusinessNotificationTable.FLD_IS_COMPLETE + "] = FALSE " +
                                "AND [" + BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID + "] = " + BizNotificationType.TODO;
                todoCount = dv.Count;
                if (todoCount > 0) {
                    LnkBtnStatusMsg.ForeColor = Color.Red;
                    msg = "You have " + todoCount.ToString() + " todo(s) pending for completion";

                }

                dv.RowFilter = "[" + BusinessNotificationTable.FLD_IS_COMPLETE + "] = FALSE " +
                                "AND [" + BusinessNotificationTable.FLD_BUSINESS_NOTIFICATION_TYPE_ID + "] <> " + BizNotificationType.TODO;
                NoteCount = dv.Count;
                if (NoteCount > 0) {
                    if (msg.Length > 0)
                        msg = msg + "<br>";
                    msg = msg + "You have " + NoteCount.ToString() + " Unread Note(s)";
                    if (LnkBtnStatusMsg.ForeColor != Color.Red)
                        LnkBtnStatusMsg.ForeColor = Color.Blue;

                }
                if (Session["HAS_BEEN_ALERTED"] == null) {
                    if ((NoteCount + todoCount) > 0) {
                        System.Text.StringBuilder strBuild = new System.Text.StringBuilder();

                        strBuild.Append("<script language=javascript>\n");
                        strBuild.Append("<!--			\n");
                        strBuild.Append("	\n");
                        strBuild.Append("		alert('" + msg.Replace("<br>", "\\n") + "');\n");
                        strBuild.Append("	\n");
                        strBuild.Append("//-->\n");
                        strBuild.Append("</script>");

                        this.RegisterStartupScript("DisplayToDoResult", strBuild.ToString());
                    }
                }
                Session["HAS_BEEN_ALERTED"] = true;
                if (msg.Length > 0)
                    msg = msg + "<br><br>Click here to access your Note List";
                LnkBtnStatusMsg.Visible = true;
            }

            LnkBtnStatusMsg.Text = msg;
        }

        private void SetQCAPOrderInfo(int pageIndex)
        {
            OrderSystem ordSys = new OrderSystem();
            qcapOrderData = ordSys.GetQCAPOrdersForUser(this.UserID);

            //sort the datasource as required
            if (ViewState["SortColumn"] != null && ViewState["SortDirection"] != null)
            {
                string sortColumn = ViewState["SortColumn"] as string;
                ParameterExpression param = Expression.Parameter(typeof(QCAPOrderData), sortColumn);
                var sortExpr = Expression.Lambda<Func<QCAPOrderData, object>>(Expression.Convert(Expression.Property(param, sortColumn),
                    typeof(object)), param);
                qcapOrderData = ViewState["SortDirection"].Equals(SortDirection.Ascending) ?
                    qcapOrderData.AsQueryable<QCAPOrderData>().OrderBy(sortExpr).ToList() :
                    qcapOrderData.AsQueryable<QCAPOrderData>().OrderByDescending(sortExpr).ToList();
            }

            dtgOrderDetail.DataSource = qcapOrderData;
            dtgOrderDetail.PageIndex = pageIndex;
            dtgOrderDetail.DataBind();

            bool ShowQCAPOrderGrid = (qcapOrderData != null && qcapOrderData.Count > 0);
            dtgOrderDetail.Visible = ShowQCAPOrderGrid;
            lblQCAPOrderDetail.Visible = ShowQCAPOrderGrid;
        }

        protected void LnkBtnStatusMsg_Click(object sender, EventArgs e) {
            if (this.Role == AuthSystem.ROLE_FM) {
                Response.Redirect("~/V2/Forms/OrderSearch.aspx");
            }
            else if (this.Role > AuthSystem.ROLE_FM) {
                Response.Redirect("~/MyNoteList.aspx?AppItem=mynotelist");
            }
        }

        protected void dtgOrderDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtgOrderDetail.SelectedIndex > -1)
            {
                Label lblAccountId = dtgOrderDetail.SelectedRow.FindControl("lblAccountId") as Label;
                Label lblCampaignId = dtgOrderDetail.SelectedRow.FindControl("lblCampaignId") as Label;
                Label lblFormId = dtgOrderDetail.SelectedRow.FindControl("lblFormId") as Label;
                Label lblTempOrderId = dtgOrderDetail.SelectedRow.FindControl("lblTempOrderId") as Label;

                int AccountID = int.Parse(lblAccountId.Text);
                int CampaignId = int.Parse(lblCampaignId.Text);
                int FormId = int.Parse(lblFormId.Text);
                string QCAPOrderId = lblTempOrderId.Text;

                bool IsValid = false;
                IsValid = ValidateAccount(AccountID);
                if (IsValid)
                {
                    IsValid = ValidateOrder(FormId, AccountID);
                    if (IsValid)
                    {
                        IsValid = ValidatePA(FormId, AccountID);
                        if (IsValid)
                        {
                            BasePage basePage = new BasePage();

                            string url = basePage.GetPageToGo(QSPForm.Business.AppItem.OrderForm_Step3);
                            Response.Redirect(url + "&FormID=" + FormId.ToString() + "&CampID=" + CampaignId.ToString()
                                + "&QCAPID=" + QCAPOrderId);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validate if the user has permissions to place the order and if the order is valid for the account's billing region
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        private bool ValidateOrder(int formId, int accountID)
        {
            QSPForm.Business.FormSystem formSystem = new QSPForm.Business.FormSystem();
            List<Form> formList = formSystem.SelectByEntityTypeAndUserPermissions(EntityTypeEnum.Order, this.UserID, accountID);
            bool CanUserPlaceOrder = false;
            CanUserPlaceOrder = (formList.Exists(delegate(Form f) { return f.FormId == formId; }));

            if (!CanUserPlaceOrder)
            {
                SetJScriptForAlertValidation("The order cannot be placed for this account.");
                dtgOrderDetail.SelectedIndex = -1;
            }

            return CanUserPlaceOrder;

        }

        /// <summary>
        /// Validate if the order requires a PA and if the PA is present for the campaign
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private bool ValidatePA(int formId, int accountId)
        {
            bool isPaNeeded = false;
            bool isPaInSystem = true;

            FormSystem formSystem = new FormSystem();
            isPaNeeded = formSystem.FormRequiresProgramAgreement(formId);

            if (isPaNeeded)
            {
                isPaInSystem = formSystem.FormHasProgramAgreement(formId, accountId);
            }

            if (!isPaInSystem)
            
            {
                SetJScriptForAlertValidation("A PA is required to place this order.");
                dtgOrderDetail.SelectedIndex = -1;
            }

            return isPaInSystem;
        }

        /// <summary>
        /// Validate if account is in open state
        /// </summary>
        /// <param name="AccID"></param>
        /// <returns></returns>
        private bool ValidateAccount(int AccID)
        {
            QSPForm.Business.AccountSystem accSys = new QSPForm.Business.AccountSystem();
            QSPForm.Common.DataDef.AccountTable accTable = new AccountTable();
            accTable = accSys.SelectOne(Convert.ToInt32(AccID));
            bool IsInCollection = false;
            bool IsClosed = false;
            if (accTable.Rows.Count > 0)
            {
                DataRow row = accTable.Rows[0];
                int StatusID = 0;
                if (!row.IsNull(AccountTable.FLD_ACCOUNT_STATUS_ID))
                    StatusID = Convert.ToInt32(row[AccountTable.FLD_ACCOUNT_STATUS_ID]);
                //Look for In Collection State
                if (StatusID == QSPForm.Common.AccountStatus.CLOSE ||
                            StatusID == QSPForm.Common.AccountStatus.CLOSE_PROCESSED ||
                            StatusID == QSPForm.Common.AccountStatus.CLOSE_IN_PROCESS ||
                            StatusID == QSPForm.Common.AccountStatus.CLOSE_NOT_SUBMITTED)
                {
                    IsClosed = true;
                    CommonUtility clsUtil = new CommonUtility();
                    SetJScriptForAlertValidation("Account Closed - Please Call QSP Field Support For Assistance");
                    dtgOrderDetail.SelectedIndex = -1;
                }
                else if (StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION ||
                         StatusID == QSPForm.Common.AccountStatus.IN_COLLECTION_PROCESSED)
                {
                    IsInCollection = true;
                    CommonUtility clsUtil = new CommonUtility();
                    SetJScriptForAlertValidation("Account In Collection - Please Call QSP Field Support For Assistance");
                    dtgOrderDetail.SelectedIndex = -1;
                }
                if (!IsInCollection && !IsClosed)
                {
                    //If not do a verification in AS400 
                    int FulfAccountID = 0;
                    if (!row.IsNull(AccountTable.FLD_FULF_ACCOUNT_ID))
                        FulfAccountID = Convert.ToInt32(row[AccountTable.FLD_FULF_ACCOUNT_ID]);
                    if (FulfAccountID > 0)
                    {//Do the verification
                        try
                        {
                            ARMCUSPTable dTblSyncAcc = accSys.SelectOne_FromSynch(FulfAccountID);
                            if (dTblSyncAcc.Rows.Count > 0)
                            {
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

                                if ((InColDay + InColMonth + InColYear) > 0)
                                {
                                    IsInCollection = true;
                                    CommonUtility clsUtil = new CommonUtility();
                                    SetJScriptForAlertValidation("Account In Collection - Please Call QSP Field Support For Assistance");
                                    dtgOrderDetail.SelectedIndex = -1;
                                }
                                else
                                {//Additional Verification for when it's closed 
                                    string sStatus = "";
                                    sStatus = syncRow[ARMCUSPTable.FLD_STATUS].ToString();
                                    if (sStatus == "C")
                                    {
                                        IsClosed = true;
                                        CommonUtility clsUtil = new CommonUtility();
                                        SetJScriptForAlertValidation("Account Closed - Please Call QSP Field Support For Assistance");
                                        dtgOrderDetail.SelectedIndex = -1;
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        //We don't stop the process, the AS400 will validate in case of.
                    }
                }

                //Account Renewal check is not done here since Cookie Dough orders need PAs
            }
            return (!IsInCollection && !IsClosed);
        }

        private void SetJScriptForAlertValidation(string msg)
        {
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

        protected void dtgOrderDetail_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["SortColumn"].Equals(e.SortExpression))
            {
                // Same column, switch direction
                ViewState["SortDirection"] = (ViewState["SortDirection"].Equals(SortDirection.Ascending)) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                // Different column, default to asc direction
                ViewState["SortColumn"] = e.SortExpression;
                ViewState["SortDirection"] = SortDirection.Ascending;
            }

            SetQCAPOrderInfo(dtgOrderDetail.PageIndex);

            e.Cancel = true;
        }

        protected void dtgOrderDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SetQCAPOrderInfo(e.NewPageIndex);
        }

        protected void dtgOrderDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblTempOrderId = dtgOrderDetail.Rows[e.RowIndex].FindControl("lblTempOrderId") as Label;
            int QCAPOrderId = int.Parse(lblTempOrderId.Text);

            OrderSystem ordSys = new OrderSystem();
            ordSys.DeleteQCAPOrder(QCAPOrderId, this.UserID);

            SetQCAPOrderInfo(0);
        }
    } 
}