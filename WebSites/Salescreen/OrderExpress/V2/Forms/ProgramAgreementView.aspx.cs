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
    public partial class ProgramAgreementView : System.Web.UI.Page
    {
        private AccountData Account;
        private ProgramAgreementData ProgramAgreement;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                this.ValidateFormParameters();
                this.ValidateUserPermissions();

                this.LoadProgramAgreement();
                this.LoadAccount();
                this.LoadDocumentList();
                this.LoadOrderList();

                this.BindAccount();
                this.BindAccountNoteList();
                this.BindAccountDocumentList();
                this.BindBillingAddress();
                this.BindProgramAgreement();
                this.BindShippingAddress();
                this.BindStatusHistoryList();
                this.BindOrderList();

                this.SetPageTitle();
                this.SetCommandLinks();
            }
        }

        private int ProgramAgreementId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["ProgramAgreementId"], out result);

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
            if (this.ProgramAgreementId <= 0)
            {
                Response.Redirect("~/V2/Forms/ProgramAgreementSearch.aspx");
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

                ProgramAgreementSearchParameters searchParameters = new ProgramAgreementSearchParameters(); ;

                Properties.Settings settings = new Properties.Settings();

                searchParameters.LoggedUserId = loggedUser.UserId;
                searchParameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

                searchParameters.IsPagingEnabled = false;
                searchParameters.ItemsPerPage = settings.GridPageSize;
                searchParameters.RequestedPage = 1;
                searchParameters.SortField = settings.ProgramAgreementSearchDefaultSort;

                searchParameters.SearchField = ProgramAgreementSearchFieldEnum.QSPProgramAgreementId;
                searchParameters.SearchValue = this.ProgramAgreementId.ToString();

                searchParameters.ProgramId = null;
                searchParameters.ProgramTypeId = null;
                searchParameters.SubdivisionCode = "";
                searchParameters.StatusCategoryId = null;
                searchParameters.FormId = null;

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

                ProgramAgreementSystem programAgreementSystem = new ProgramAgreementSystem();
                int count = programAgreementSystem.SearchTotalRowCount(searchParameters);

                if (count > 0)
                {
                    // We have access to the specified account id
                }
                else
                {
                    // No access, redirect to account seearch
                    Response.Redirect("~/V2/Forms/ProgramAgreementSearch.aspx");
                }
            }
        }
        private void SetPageTitle()
        {
            string title;

            if (this.Account != null)
            {
                title = String.Format("{0} - View program agreement - Order Express", this.Account.Name);
            }
            else
            {
                title = "View program agreement - Order Express";
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

                this.hlPrint.NavigateUrl = string.Format("~/V2/Forms/ProgramAgreementView.aspx?ProgramAgreementId={0}&IsForPrint=true", this.ProgramAgreement.Id);
                
                //this.hlEdit.NavigateUrl = string.Format("~/V2/Forms/ProgramAgreementEdit.aspx?ProgramAgreementId={0}", this.ProgramAgreement.Id);
                this.hlEdit.NavigateUrl = string.Format("~/ProgramAgreementDetail.aspx?ProgramAgreementID={0}", this.ProgramAgreement.Id);

                ProgramAgreementSystem programAgreementSystem = new ProgramAgreementSystem();
                if (!programAgreementSystem.IsProgramAgreementEditable(this.ProgramAgreement))
                {
                    this.hlEdit.Visible = false;
                }

                this.hlClose.NavigateUrl = "javascript:window.close();";
            }

        }

        private void LoadAccount()
        {
            if (this.ProgramAgreement != null)
            {
                AccountSystem accountSystem = new AccountSystem();
                this.Account = accountSystem.GetAccount(this.ProgramAgreement.Campaign.AccountId, true);
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
        private void LoadProgramAgreement()
        {
            ProgramAgreementSystem programAgreementSystem = new ProgramAgreementSystem();
            this.ProgramAgreement = programAgreementSystem.GetProgramAgreement(this.ProgramAgreementId, true);
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

            parameters.SearchField = OrderSearchFieldEnum.QSPProgramAgreementId;
            parameters.SearchValue = this.ProgramAgreementId.ToString();
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
        private void BindProgramAgreement()
        {
            if (this.IsForPrint)
            {
                this.lblDirections.Visible = false;
            }
            else
            {
                this.lblDirections.Visible = true;
            }

            #region Program agreement information

            this.ucProgramAgreementInformationView.SetValueForProgramAgreement(this.ProgramAgreement);
            this.ucProgramAgreementInformationView.SetValuesToForm();

            #endregion

            #region Program agreement terms

            this.ucProgramAgreementTermsView.SetValueForProgramAgreement(this.ProgramAgreement);
            this.ucProgramAgreementTermsView.SetValuesToForm();

            #endregion

            #region Program information

            this.ucProgramInformationView.SetValueForProgramAgreement(this.ProgramAgreement);
            this.ucProgramInformationView.SetValuesToForm();

            #endregion

            #region Audit information

            AuditInformationData auditInformationData = new AuditInformationData();
            auditInformationData.EntityId = this.ProgramAgreement.Id;
            auditInformationData.EntityType = EntityTypeEnum.ProgramAgreement;
            auditInformationData.CreateDate = this.ProgramAgreement.CreateDate;
            auditInformationData.CreateUserId = this.ProgramAgreement.CreateUserId;
            auditInformationData.CreateUserFirstName = this.ProgramAgreement.CreateUserFirstName;
            auditInformationData.CreateUserLastName = this.ProgramAgreement.CreateUserLastName;
            auditInformationData.UpdateDate = this.ProgramAgreement.UpdateDate;
            auditInformationData.UpdateUserId = this.ProgramAgreement.UpdateUserId;
            auditInformationData.UpdateUserFirstName = this.ProgramAgreement.UpdateUserFirstName;
            auditInformationData.UpdateUserLastName = this.ProgramAgreement.UpdateUserLastName;

            this.ucAuditInformationView.SetValueForAuditInformation(auditInformationData);
            this.ucAuditInformationView.SetValuesToForm();

            #endregion
        }
        private void BindShippingAddress()
        {
            this.ucShippingAddressView.SetValueForAddress(this.ProgramAgreement.ShippingAddress);
            this.ucShippingAddressView.SetValuesToForm();
        }
        private void BindStatusHistoryList()
        {
            if (this.ProgramAgreement.StatusHistory.Count > 0)
            {
                this.trStatusHistory.Visible = true;
                this.trStatusHistorySpacer.Visible = true;

                this.ucStatusHistoryList.SetValueForList(this.ProgramAgreement.StatusHistory);
                this.ucStatusHistoryList.SetValuesToForm();
            }
            else
            {
                this.trStatusHistory.Visible = false;
                this.trStatusHistorySpacer.Visible = false;
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

    }
}
