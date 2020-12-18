using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Web.Code;
using QSP.OrderExpress.Web.V2.UserControls;

namespace QSP.OrderExpress.Web.V2.Forms
{
    public partial class OrganizationEdit : System.Web.UI.Page
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

                this.BindOrganization();
                this.BindBillingAddress();
                this.BindShippingAddress();

                this.SetPageTitle();
                this.SetCommandLinks();

                this.trMethodNotification.Visible = false;
            }
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            #region Get data from controls

            this.LoadOrganization();

            #region Organization

            OrganizationData organizationData = this.ucOrganizationInformationEdit.GetValueFromOrganization();
            this.Organization.Name = organizationData.Name;
            this.Organization.Type.Id = organizationData.Type.Id;
            this.Organization.Level.Id = organizationData.Level.Id;
            this.Organization.TaxExemptionNumber = organizationData.TaxExemptionNumber;
            this.Organization.TaxExemptionExpirationDate = organizationData.TaxExemptionExpirationDate;
            this.Organization.MDRPID = organizationData.MDRPID;
            this.Organization.Comments = organizationData.Comments;

            #endregion

            #region Billing address

            AddressData billingAddressData = this.ucBillingAddressEdit.GetValueFromAddress();
            if (billingAddressData != null)
            {
                this.Organization.BillingAddress.Name = billingAddressData.Name;
                this.Organization.BillingAddress.FirstName = billingAddressData.FirstName;
                this.Organization.BillingAddress.LastName = billingAddressData.LastName;
                this.Organization.BillingAddress.Address1 = billingAddressData.Address1;
                this.Organization.BillingAddress.Address2 = billingAddressData.Address2;
                this.Organization.BillingAddress.City = billingAddressData.City;
                this.Organization.BillingAddress.County = billingAddressData.County;
                this.Organization.BillingAddress.Zip = billingAddressData.Zip;
                this.Organization.BillingAddress.Subdivision.Code = billingAddressData.Subdivision.Code;
                this.Organization.BillingAddress.Phone = billingAddressData.Phone;
                this.Organization.BillingAddress.Fax = billingAddressData.Fax;
                this.Organization.BillingAddress.Email = billingAddressData.Email;
                this.Organization.BillingAddress.IsResidentialArea = billingAddressData.IsResidentialArea;
            }

            #endregion

            #region Shipping address

            AddressData shippingAddressData = this.ucShippingAddressEdit.GetValueFromAddress();
            if (shippingAddressData != null)
            {
                this.Organization.ShippingAddress.Name = shippingAddressData.Name;
                this.Organization.ShippingAddress.FirstName = shippingAddressData.FirstName;
                this.Organization.ShippingAddress.LastName = shippingAddressData.LastName;
                this.Organization.ShippingAddress.Address1 = shippingAddressData.Address1;
                this.Organization.ShippingAddress.Address2 = shippingAddressData.Address2;
                this.Organization.ShippingAddress.City = shippingAddressData.City;
                this.Organization.ShippingAddress.County = shippingAddressData.County;
                this.Organization.ShippingAddress.Zip = shippingAddressData.Zip;
                this.Organization.ShippingAddress.Subdivision.Code = shippingAddressData.Subdivision.Code;
                this.Organization.ShippingAddress.Phone = shippingAddressData.Phone;
                this.Organization.ShippingAddress.Fax = shippingAddressData.Fax;
                this.Organization.ShippingAddress.Email = shippingAddressData.Email;
                this.Organization.ShippingAddress.IsResidentialArea = shippingAddressData.IsResidentialArea;
            }

            #endregion

            #endregion

            #region Set update user

            LoggedUser loggedUser = (LoggedUser)Session["LoggedUser"];

            this.Organization.UpdateUserId = loggedUser.UserId;
            this.Organization.BillingAddress.UpdateUserId = loggedUser.UserId; 
            this.Organization.ShippingAddress.UpdateUserId = loggedUser.UserId;

            #endregion

            #region Save changes

            OrganizationSystem organizationSystem = new OrganizationSystem();
            MethodResult result = organizationSystem.SaveOrganization(this.Organization);

            #endregion

            if (result.IsSuccessful)
            {
                Response.Redirect(string.Format("~/V2/Forms/OrganizationView.aspx?OrganizationId={0}", this.Organization.Id));
            }
            else
            {
                this.trMethodNotification.Visible = true;

                this.ucMethodNotificationList.SetValueForList(result.ResultNotifications);
                this.ucMethodNotificationList.SetValuesToForm();
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
                title = String.Format("{0} - Edit organization - Order Express", this.Organization.Name);
            }
            else
            {
                title = "Edit organization - Order Express";
            }

            this.Title = title;
        }
        private void SetCommandLinks()
        {
            this.hlCancel.NavigateUrl = string.Format("~/V2/Forms/OrganizationView.aspx?OrganizationId={0}", this.Organization.Id);
            this.hlClose.NavigateUrl = "javascript:window.close();";
        }

        private void LoadOrganization()
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            this.Organization = organizationSystem.GetOrganization(this.OrganizationId, true);
        }

        private void BindOrganization()
        {
            this.ucOrganizationInformationEdit.SetValueForOrganization(this.Organization);
        }
        private void BindBillingAddress()
        {
            this.ucBillingAddressEdit.SetValueForAddress(this.Organization.BillingAddress);
        }
        private void BindShippingAddress()
        {
            this.ucShippingAddressEdit.SetValueForAddress(this.Organization.ShippingAddress);
        }

    }
}
