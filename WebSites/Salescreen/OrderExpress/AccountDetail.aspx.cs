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

using QSPForm.Business;

using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Common.Search;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web 
{
    public partial class AccountDetail : BaseWebForm 
    {
        protected void Page_Load(object sender, EventArgs e) 
        {
            SetHeader();
            this.ValidateUserSession();
            this.ValidateFormParameters();
            this.ValidateUserPermissions();
        }

        private int AccountId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["AccId"], out result);

                if (!parseSuccessful)
                {
                    result = 0;
                }

                return result;
            }
        }

        private void SetHeader() {
            this.Header.InstructionText = "Edit Sponsor Information, Postal Address, Phone Numbers and/or Email Addresses.  'Bill To' Information can easily be copied over to 'Ship To' Information.";
            this.Header.IconImage = "~/images/icon/icon_account.gif";
            this.Header.SectionText = "Account:";
            this.Header.PageText = "Account Detail";
            this.LabelMessage = this.Master.LabelMessage1;
            this.Master.ValSummaryVisibility = false;
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
    } 
}