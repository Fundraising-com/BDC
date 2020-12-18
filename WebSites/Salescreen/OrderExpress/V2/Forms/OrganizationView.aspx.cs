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
    public partial class OrganizationView : System.Web.UI.Page
    {
        private OrganizationData Organization;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidateUserSession();

            if (!Page.IsPostBack)
            {
                this.ValidateFormParameters();
                this.ValidateUserPermissions();

                this.LoadOrganization();
                this.LoadAccountList();

                this.BindOrganization();
                this.BindBillingAddress();
                this.BindShippingAddress();
                this.BindAccountList();

                this.SetPageTitle();
                this.SetCommandLinks();
            }
        }

        private int OrganizationId
        {
            get
            {
                int result = 0;

                bool parseSuccessful = Int32.TryParse(Request.QueryString["OrganizationId"], out result);

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
            if (this.OrganizationId <= 0)
            {
                Response.Redirect("~/V2/Forms/OrganizationSearch.aspx");
            }
        }
        private void ValidateUserPermissions()
        {
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            if (loggedUser.UserTypeId >= (int)UserTypeEnum.User)
            {
                // Access granted by default
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        private void SetPageTitle()
        {
            string title;

            if (this.Organization != null)
            {
                title = String.Format("{0} - View organization - Order Express", this.Organization.Name);
            }
            else
            {
                title = "View organization - Order Express";
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

                this.hlPrint.NavigateUrl = string.Format("~/V2/Forms/OrganizationView.aspx?OrganizationId={0}&IsForPrint=true", this.Organization.Id);
                this.hlEdit.NavigateUrl = string.Format("~/V2/Forms/OrganizationEdit.aspx?OrganizationId={0}", this.Organization.Id);
                this.hlClose.NavigateUrl = "javascript:window.close();";
            }
        }

        private void LoadOrganization()
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            this.Organization = organizationSystem.GetOrganization(this.OrganizationId, true);
        }
        private void LoadAccountList()
        {
            AccountSearchParameters parameters = new AccountSearchParameters();

            Properties.Settings settings = new Properties.Settings();
            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

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


            parameters.LoggedUserId = loggedUser.UserId;
            parameters.LoggedUserType = (UserTypeEnum)loggedUser.UserTypeId;

            parameters.SearchField = AccountSearchFieldEnum.QSPOrganizationId;
            parameters.SearchValue = this.OrganizationId.ToString();
            parameters.ProgramTypeId = null;
            parameters.StatusCategoryId = null;
            parameters.SubdivisionCode = "";

            parameters.IsPagingEnabled = false;
            parameters.ItemsPerPage = settings.GridPageSize;
            parameters.RequestedPage = 1;
            parameters.SortField = settings.AccountSearchDefaultSort;

            this.ucAccountSearchResults.SetSearchParameters(parameters);
            this.ucAccountSearchResults.SetColumnsForOrganizationView();
            this.ucAccountSearchResults.DoSearch();
        }

        private void BindOrganization()
        {
            if (this.IsForPrint)
            {
                this.lblDirections.Visible = false;
            }
            else
            {
                this.lblDirections.Visible = true;
            }

            this.ucOrganizationInformationView.SetValueForOrganization(this.Organization);
            this.ucOrganizationInformationView.SetValuesToForm();
        }
        private void BindBillingAddress()
        {
            this.ucBillingAddressView.SetValueForAddress(this.Organization.BillingAddress);
            this.ucBillingAddressView.SetValuesToForm();
        }
        private void BindShippingAddress()
        {
            this.ucShippingAddressView.SetValueForAddress(this.Organization.ShippingAddress);
            this.ucShippingAddressView.SetValuesToForm();
        }
        private void BindAccountList()
        {
        }

    }
}
