using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web.V2.Forms
{
    public partial class OrderView : System.Web.UI.Page
    {
        private AccountData Account;
        private OrderData Order;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                LoadPage();
            }
        }

        private void LoadPage()
        {
            this.ValidateFormParameters();
            this.ValidateUserPermissions();

            this.LoadOrder();
            this.LoadAccount();
            this.LoadDocumentList();

            this.BindAccount();
            this.BindAccountNoteList();
            this.BindAccountDocumentList();
            this.BindBillingAddress();
            this.BindOrder();
            this.BindShippingAddress();
            this.BindStatusHistoryList();
            this.BindOrderDetailList();
            this.BindOrderChargeList();

            this.SetPageTitle();
            this.SetCommandLinks();
        }

        private int OrderId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["OrderId"], out result);

                if (!parseSuccessful)
                {
                    result = 0;
                }

                return result;
            }
        }
        private bool IsForPrint
        {
            get
            {
                bool result = false;

                bool parseSuccessful = bool.TryParse(Request.QueryString["IsForPrint"], out result);

                if (!parseSuccessful)
                {
                    result = false;
                }

                return result;
            }
        }

        private void ValidateUserSession()
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        private void ValidateFormParameters()
        {
            if (this.OrderId <= 0)
            {
                Response.Redirect("~/V2/Forms/OrderSearch.aspx");
            }
        }
        private void ValidateUserPermissions()
        {
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            if (loggedUser.UserTypeId >= (int)UserTypeEnum.FieldSupport)
            {
                // Access granted by default
            }
            else
            {
                // Check if the user can see the account
                // Use the account search for this

                #region Get search parameters

                OrderSearchParameters searchParameters = new OrderSearchParameters(); ;

                Properties.Settings settings = new Properties.Settings();

                searchParameters.LoggedUserId = loggedUser.UserId;
                searchParameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

                searchParameters.IsPagingEnabled = false;
                searchParameters.ItemsPerPage = settings.GridPageSize;
                searchParameters.RequestedPage = 1;
                searchParameters.SortField = settings.OrderSearchDefaultSort;

                searchParameters.SearchField = OrderSearchFieldEnum.QSPOrderId;
                searchParameters.SearchValue = this.OrderId.ToString();

                searchParameters.StatusCategoryId = null;
                searchParameters.FormId = null;
                searchParameters.SubdivisionCode = ""; 
                searchParameters.ProgramTypeId = null;
                searchParameters.StartDate = null; 
                searchParameters.EndDate = null;
                searchParameters.OrderTypeId = null;
                searchParameters.SourceId = null;

                if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                    || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
                {
                    searchParameters.FSMId = loggedUser.FMId;
                    searchParameters.FSMName = "";
                    searchParameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.OwnAndChildren;
                }
                else
                {
                    searchParameters.FSMId = "";
                    searchParameters.FSMName = "";
                    searchParameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.All;
                }

                #endregion

                OrderSystem orderSystem = new OrderSystem();
                int count = orderSystem.SearchTotalRowCount(searchParameters);

                if (count > 0)
                {
                    // We have access to the specified account id
                }
                else
                {
                    // No access, redirect to account seearch
                    Response.Redirect("~/V2/Forms/OrderSearch.aspx");
                }
            }
        }
        private void SetPageTitle()
        {
            string title;

            if (this.Account != null)
            {
                title = String.Format("{0} - View order - Order Express", this.Order.Id);
            }
            else
            {
                title = "View order - Order Express";
            }

            this.Title = title;
        }
        private void SetCommandLinks()
        {
            if (this.IsForPrint)
            {
                this.trButtons.Visible = false;
                this.trButtonsSpacer.Visible = false;
            }
            else
            {
                this.trButtons.Visible = true;
                this.trButtonsSpacer.Visible = true;

                this.hlPrint.NavigateUrl = string.Format("~/V2/Forms/OrderView.aspx?OrderId={0}&IsForPrint=true", this.Order.Id);

                //this.hlEdit.NavigateUrl = string.Format("~/V2/Forms/OrderEdit.aspx?OrderId={0}", this.Order.Id);
                this.hlEdit.NavigateUrl = string.Format("~/OrderDetail.aspx?OrderID={0}", this.Order.Id);

                OrderSystem orderSystem = new OrderSystem();
                if (!orderSystem.IsOrderEditable(this.Order))
                {
                    this.hlEdit.Visible = false;
                }

                this.hlClose.NavigateUrl = "javascript:window.close();";

                LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];
                if (loggedUser != null && Order != null)
                    hlResetStatus.Visible = loggedUser.UserTypeId == (int)UserTypeEnum.SuperUser && Order.StatusId >= 9000;
            }
            lblConfirmation.Visible = false;
            trConfirmationSpacer.Visible = false;
        }

        private void LoadOrder()
        {
            OrderSystem orderSystem = new OrderSystem();
            this.Order = orderSystem.GetOrder(this.OrderId, true);
        }
        private void LoadAccount()
        {
            if (this.Order != null)
            {
                AccountSystem accountSystem = new AccountSystem();
                this.Account = accountSystem.GetAccount(this.Order.Campaign.AccountId, true);
            }
        }
        private void LoadDocumentList()
        {
            DocumentSearchParameters parameters = new DocumentSearchParameters();

            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            parameters.LoggedUserId = loggedUser.UserId;
            parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            parameters.SearchField = DocumentSearchFieldEnum.QSPAccountId;
            parameters.SearchValue = this.Account.Id.ToString();
            parameters.DocumentStatusId = null;
            parameters.DocumentTypeId = null;

            parameters.IsPagingEnabled = false;
            parameters.ItemsPerPage = 1000;
            parameters.RequestedPage = 1;
            parameters.SortField = settings.DocumentSearchDefaultSort;

            this.ucDocumentList.SetSearchParameters(parameters);
            this.ucDocumentList.SetColumnsForAccountView();
            this.ucDocumentList.DoSearch();
        }

        private void BindAccount()
        {
            #region Account information

            this.ucAccountInformationView.SetValueForAccount(this.Account);
            this.ucAccountInformationView.SetValuesToForm();

            #endregion
        }
        private void BindAccountNoteList()
        {
            if (this.Account.AccountNotes.Count > 0)
            {
                this.trAccountNotes.Visible = true;
                this.trAccountNotesSpacer.Visible = true;

                this.ucBusinessExceptionList.SetValueForList(this.Account.AccountNotes);
                this.ucBusinessExceptionList.SetValuesToForm();
            }
            else
            {
                this.trAccountNotes.Visible = false;
                this.trAccountNotesSpacer.Visible = false;
            }
        }
        private void BindAccountDocumentList()
        {
            int count = this.ucDocumentList.GetCount();

            if (count > 0)
            {
                this.trAccountDocuments.Visible = true;
                this.trAccountDocumentsSpacer.Visible = true;
            }
            else
            {
                this.trAccountDocuments.Visible = false;
                this.trAccountDocumentsSpacer.Visible = false;
            }
        }
        private void BindBillingAddress()
        {
            this.ucBillingAddressView.SetValueForAddress(this.Account.BillingAddress);
            this.ucBillingAddressView.SetValuesToForm();
        }
        private void BindOrder()
        {
            if (this.IsForPrint)
            {
                this.lblDirections.Visible = false;
            }
            else
            {
                this.lblDirections.Visible = true;
            }

            #region Order information

            this.ucOrderInformationView.SetValueForOrder(this.Order);
            this.ucOrderInformationView.SetValuesToForm();

            #endregion

            #region Order terms

            this.ucOrderTermsView.SetValueForOrderTerms(this.Order.Campaign);
            this.ucOrderTermsView.SetValueForAccountProfit(this.Order.ProfitRate);
            this.ucOrderTermsView.SetValuesToForm();

            #endregion

            #region Order details

            this.ucOrderDetailList.SetValueForList(this.Order.OrderDetails);
            this.ucOrderDetailList.SetValuesToForm();

            #endregion

            #region Order charges

            this.ucChargeList.SetValueForList(this.Order.OrderCharges);
            this.ucChargeList.SetValuesToForm();

            #endregion

            #region Order summary

            this.ucOrderSummaryView.SetValueForOrderSummary(this.Order.OrderSummary);
            this.ucOrderSummaryView.SetValuesToForm();

            #endregion

            #region Audit information

            AuditInformationData auditInformationData = new AuditInformationData();
            auditInformationData.EntityId = this.Order.Id;
            auditInformationData.EntityType = EntityTypeEnum.Order;
            auditInformationData.CreateDate = this.Order.CreateDate;
            auditInformationData.CreateUserId = this.Order.CreateUserId;
            auditInformationData.CreateUserFirstName = this.Order.CreateUserFirstName;
            auditInformationData.CreateUserLastName = this.Order.CreateUserLastName;
            auditInformationData.UpdateDate = this.Order.UpdateDate;
            auditInformationData.UpdateUserId = this.Order.UpdateUserId;
            auditInformationData.UpdateUserFirstName = this.Order.UpdateUserFirstName;
            auditInformationData.UpdateUserLastName = this.Order.UpdateUserLastName;

            this.ucAuditInformationView.SetValueForAuditInformation(auditInformationData);
            this.ucAuditInformationView.SetValuesToForm();

            #endregion
        }
        private void BindShippingAddress()
        {
            this.ucShippingAddressView.SetValueForAddress(this.Order.ShippingAddress);
            this.ucShippingAddressView.SetValuesToForm();
        }
        private void BindStatusHistoryList()
        {
            if (this.Order.StatusHistory.Count > 0)
            {
                this.trStatusHistory.Visible = true;
                this.trStatusHistorySpacer.Visible = true;

                this.ucStatusHistoryList.SetValueForList(this.Order.StatusHistory);
                this.ucStatusHistoryList.SetValuesToForm();
            }
            else
            {
                this.trStatusHistory.Visible = false;
                this.trStatusHistorySpacer.Visible = false;
            }
        }
        private void BindOrderDetailList()
        {
        }
        private void BindOrderChargeList()
        {
            int count = this.Order.OrderCharges.Count;

            if (count > 0)
            {
                this.trOrderCharges.Visible = true;
                this.trOrderChargesSpacer.Visible = true;
            }
            else
            {
                this.trOrderCharges.Visible = false;
                this.trOrderChargesSpacer.Visible = false;
            }
        }

        protected void hlResetStatus_Click(object sender, EventArgs e)
        {
            OrderSystem orderSystem = new OrderSystem();
            this.Order = orderSystem.GetOrder(this.OrderId, true);
            if (Order != null && Order.StatusId >= 9000)
            {
                LoggedUser loggedUser = Session["LoggedUser"] as LoggedUser;
                orderSystem.SetStatus(OrderId, QSPForm.Common.OrderStatus.IN_PROCESS, "Status updated from Order Express", loggedUser.UserId);
                LoadPage();
                lblConfirmation.Visible = true;
                trConfirmationSpacer.Visible = true;
            }
            else
            {
                LoadPage();
            }
        }


    }
}
