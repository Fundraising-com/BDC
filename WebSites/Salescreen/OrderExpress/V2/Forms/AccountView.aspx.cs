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
    public partial class AccountView : System.Web.UI.Page
    {
        private AccountData Account;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                this.ValidateFormParameters();
                this.ValidateUserPermissions();

                this.LoadAccount();
                this.LoadDocumentList();
                this.LoadProgramAgreementList();
                this.LoadOrderList();

                this.BindAccount();
                this.BindAccountNoteList();
                this.BindAccountDocumentList();
                this.BindBillingAddress();
                this.BindShippingAddress();
                this.BindProgramAgreementList();
                this.BindOrderList();
                this.BindStatusHistoryList();

                this.SetPageTitle();
                this.SetCommandLinks();
            }
        }

        private int AccountId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["AccountId"], out result);

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
            if (this.AccountId <= 0)
            {
                Response.Redirect("~/V2/Forms/AccountSearch.aspx");
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

                AccountSearchParameters searchParameters = new AccountSearchParameters(); ;

                Properties.Settings settings = new Properties.Settings();


                searchParameters.LoggedUserId = loggedUser.UserId;
                searchParameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

                searchParameters.IsPagingEnabled = false;
                searchParameters.ItemsPerPage = settings.GridPageSize;
                searchParameters.RequestedPage = 1;
                searchParameters.SortField = settings.AccountSearchDefaultSort;

                searchParameters.SearchField = AccountSearchFieldEnum.QSPAccountId;
                searchParameters.SearchValue = this.AccountId.ToString();
                
                searchParameters.ProgramTypeId = null;
                searchParameters.SubdivisionCode = "";
                searchParameters.StatusCategoryId = null;

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

                AccountSystem accountSystem = new AccountSystem();
                int count = accountSystem.SearchTotalRowCount(searchParameters);

                if (count > 0)
                {
                    // We have access to the specified account id
                }
                else
                {
                    // No access, redirect to account seearch
                    Response.Redirect("~/V2/Forms/AccountSearch.aspx");
                }
            }
        }
        private void SetPageTitle()
        {
            string title;

            if (this.Account != null)
            {
                title = String.Format("{0} - View account - Order Express", this.Account.Name);
            }
            else
            {
                title = "View account - Order Express";
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

                this.hlPrint.NavigateUrl = string.Format("~/V2/Forms/AccountView.aspx?AccountId={0}&IsForPrint=true", this.Account.Id);
                
                //this.hlEdit.NavigateUrl = string.Format("~/V2/Forms/AccountEdit.aspx?AccountId={0}", this.Account.Id);
                this.hlEdit.NavigateUrl = string.Format("~/AccountDetail.aspx?AccID={0}", this.Account.Id);

                AccountSystem accountSystem = new AccountSystem();
                if (!accountSystem.IsAccountEditable(this.Account))
                {
                    this.hlEdit.Visible = false;
                }

                this.hlClose.NavigateUrl = "javascript:window.close();";
            }

        }

        private void LoadAccount()
        {
            AccountSystem accountSystem = new AccountSystem();
            this.Account = accountSystem.GetAccount(this.AccountId, true);
        }
        private void LoadDocumentList()
        {
            DocumentSearchParameters parameters = new DocumentSearchParameters();

            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            parameters.LoggedUserId = loggedUser.UserId;
            parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            parameters.SearchField = DocumentSearchFieldEnum.QSPAccountId;
            parameters.SearchValue = this.AccountId.ToString();
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
        private void LoadProgramAgreementList()
        {
            ProgramAgreementSearchParameters parameters = new ProgramAgreementSearchParameters();

            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            parameters.LoggedUserId = loggedUser.UserId;
            parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
            {
                parameters.FSMId = loggedUser.FMId;
                parameters.FSMName = "";
                parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.Own;
            }
            else
            {
                parameters.FSMId = "";
                parameters.FSMName = "";
                parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.All;
            }

            parameters.SearchField = ProgramAgreementSearchFieldEnum.QSPAccountId;
            parameters.SearchValue = this.AccountId.ToString();
            parameters.ProgramTypeId = null;
            parameters.ProgramId = null;
            parameters.FormId = null;
            parameters.StatusCategoryId = null;
            parameters.SubdivisionCode = "";

            parameters.IsPagingEnabled = false;
            parameters.ItemsPerPage = 1000;
            parameters.RequestedPage = 1;
            parameters.SortField = settings.ProgramAgreementSearchDefaultSort;

            this.ucProgramAgreementSearchResults.SetSearchParameters(parameters);
            this.ucProgramAgreementSearchResults.SetColumnsForAccountView();
            this.ucProgramAgreementSearchResults.DoSearch();            
        }
        private void LoadOrderList()
        {
            OrderSearchParameters parameters = new OrderSearchParameters();

            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            parameters.LoggedUserId = loggedUser.UserId;
            parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            if (loggedUser.UserTypeId == (int)UserTypeEnum.User
                || loggedUser.UserTypeId == (int)UserTypeEnum.FieldSaleManager)
            {
                parameters.FSMId = loggedUser.FMId;
                parameters.FSMName = "";
                parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.Own;
            }
            else
            {
                parameters.FSMId = "";
                parameters.FSMName = "";
                parameters.SearchFSMOption = SearchFSMHierarchyOptionEnum.All;
            }

            parameters.SearchField = OrderSearchFieldEnum.QSPAccountId;
            parameters.SearchValue = this.AccountId.ToString();
            parameters.ProgramTypeId = null;
            parameters.SourceId = null;
            parameters.FormId = null;
            parameters.StatusCategoryId = null;
            parameters.SubdivisionCode = "";

            parameters.IsPagingEnabled = false;
            parameters.ItemsPerPage = 1000;
            parameters.RequestedPage = 1;
            parameters.SortField = settings.AccountSearchDefaultSort;

            this.ucOrderSearchResults.SetSearchParameters(parameters);
            this.ucOrderSearchResults.SetColumnsForAccountView();
            this.ucOrderSearchResults.DoSearch();
        }

        private void BindAccount()
        {
            if (this.IsForPrint)
            {
                this.lblDirections.Visible = false;
            }
            else
            {
                this.lblDirections.Visible = true;
            }

            #region Account information

            this.ucAccountInformationView.SetValueForAccount(this.Account);
            this.ucAccountInformationView.SetValuesToForm();

            #endregion

            #region Order terms

            this.ucOrderTermsView.SetValueForOrderTerms(this.Account.LastCampaign);
            this.ucOrderTermsView.SetValuesToForm();

            #endregion

            #region Audit information

            AuditInformationData auditInformationData = new AuditInformationData();
            auditInformationData.EntityId = this.Account.Id;
            auditInformationData.EntityType = EntityTypeEnum.Account;
            auditInformationData.CreateDate = this.Account.CreateDate;
            auditInformationData.CreateUserId = this.Account.CreateUserId;
            auditInformationData.CreateUserFirstName = this.Account.CreateUserFirstName;
            auditInformationData.CreateUserLastName = this.Account.CreateUserLastName;
            auditInformationData.UpdateDate = this.Account.UpdateDate;
            auditInformationData.UpdateUserId = this.Account.UpdateUserId;
            auditInformationData.UpdateUserFirstName = this.Account.UpdateUserFirstName;
            auditInformationData.UpdateUserLastName = this.Account.UpdateUserLastName;

            this.ucAuditInformationView.SetValueForAuditInformation(auditInformationData);
            this.ucAuditInformationView.SetValuesToForm();

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
        private void BindShippingAddress()
        {
            this.ucShippingAddressView.SetValueForAddress(this.Account.ShippingAddress);
            this.ucShippingAddressView.SetValuesToForm();
        }
        private void BindProgramAgreementList()
        {
            int count = this.ucProgramAgreementSearchResults.GetCount();

            if (count > 0)
            {
                this.trProgramAgreementsSpacer.Visible = true;
                this.trProgramAgreements.Visible = true;
            }
            else
            {
                this.trProgramAgreementsSpacer.Visible = false;
                this.trProgramAgreements.Visible = false;
            }
        }
        private void BindOrderList()
        {
            int count = this.ucOrderSearchResults.GetCount();

            if (count > 0)
            {
                this.trOrdersSpacer.Visible = true;
                this.trOrders.Visible = true;
            }
            else
            {
                this.trOrdersSpacer.Visible = false;
                this.trOrders.Visible = false;
            }
        }
        private void BindStatusHistoryList()
        {
            if (this.Account.StatusHistory.Count > 0)
            {
                this.trStatusHistory.Visible = true;
                this.trStatusHistorySpacer.Visible = true;

                this.ucStatusHistoryList.SetValueForList(this.Account.StatusHistory);
                this.ucStatusHistoryList.SetValuesToForm();
            }
            else
            {
                this.trStatusHistory.Visible = false;
                this.trStatusHistorySpacer.Visible = false;
            }
        }
    }
}
