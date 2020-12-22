using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QSPForm.Business;

using QSP.OrderExpress.Business;
using QSP.OrderExpress.Business.Entity;
using QSP.OrderExpress.Common.Data;
using QSP.OrderExpress.Common.Enum;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class OrganizationInformationEdit : System.Web.UI.UserControl
    {
        private OrganizationData Organization;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadOrganizationTypeValues();
                this.LoadOrganizationLevelValues();
            }

            this.SetValuesToForm();
        }

        private void LoadOrganizationTypeValues()
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            List<OrganizationType> organizationTypeList = organizationSystem.GetOrganizationTypes();

            this.ddlType.Items.Clear();
            foreach (OrganizationType organizationType in organizationTypeList)
            {
                this.ddlType.Items.Add(new ListItem(organizationType.OrganizationTypeName, organizationType.OrganizationTypeId.ToString()));
            }
        }
        private void LoadOrganizationLevelValues()
        {
            OrganizationSystem organizationSystem = new OrganizationSystem();
            List<OrganizationLevel> organizationLevelList = organizationSystem.GetOrganizationLevels();

            this.ddlLevel.Items.Clear();
            foreach (OrganizationLevel organizationLevel in organizationLevelList)
            {
                this.ddlLevel.Items.Add(new ListItem(organizationLevel.OrganizationLevelName, organizationLevel.OrganizationLevelId.ToString()));
            }
        }
        private void SetValuesToForm()
        {
            if (this.Organization == null)
            {
                // try to get from viewstate
                //this.Organization = (OrganizationData)ViewState["OrganizationData"];
            }

            if (this.Organization != null)
            {
                this.lblQspId.Text = this.Organization.Id.ToString();

                if (this.Organization.Name != null)
                {
                    this.tbName.Text = this.Organization.Name.Trim();
                }

                if (this.Organization.Type != null)
                {
                    ListItem typeItem = this.ddlType.Items.FindByValue(this.Organization.Type.Id.ToString());
                    this.ddlType.SelectedIndex = this.ddlType.Items.IndexOf(typeItem);
                }

                if (this.Organization.Level != null)
                {
                    ListItem levelItem = this.ddlLevel.Items.FindByValue(this.Organization.Level.Id.ToString());
                    this.ddlLevel.SelectedIndex = this.ddlLevel.Items.IndexOf(levelItem);
                }

                if (this.Organization.TaxExemptionNumber != null)
                {
                    this.tbTaxExemption.Text = this.Organization.TaxExemptionNumber.Trim();
                }

                if (this.Organization.TaxExemptionExpirationDate.HasValue)
                {
                    this.tbTaxExeptionExpiry.Text = this.Organization.TaxExemptionExpirationDate.Value.ToShortDateString();
                }
                else
                {
                    this.tbTaxExeptionExpiry.Text = "";
                }

                if (this.Organization.MDRPID != null)
                {
                    this.tbMDRPID.Text = this.Organization.MDRPID.Trim();
                }

                if (this.Organization.Comments != null)
                {
                    this.tbComments.Text = this.Organization.Comments.Trim().Replace(System.Environment.NewLine, "<br />");
                }
            }
        }
        private void GetValuesFromForm()
        {
            this.Organization.Name = this.tbName.Text.Trim();

            if (this.Organization.Type == null)
            {
                this.Organization.Type = new OrganizationTypeData();
            }
            this.Organization.Type.Id = Convert.ToInt32(this.ddlType.SelectedValue);

            if (this.Organization.Level == null)
            {
                this.Organization.Level = new OrganizationLevelData();
            }
            this.Organization.Level.Id = Convert.ToInt32(this.ddlLevel.SelectedValue);
            
            this.Organization.TaxExemptionNumber = this.tbTaxExemption.Text.Trim();

            DateTime expiryDate;
            bool expiryDateExists = DateTime.TryParse(this.tbTaxExeptionExpiry.Text, out expiryDate);
            if (expiryDateExists)
            {
                this.Organization.TaxExemptionExpirationDate = expiryDate;
            }
            else
            {
                this.Organization.TaxExemptionExpirationDate = null;
            }

            this.Organization.MDRPID = this.tbMDRPID.Text.Trim();
            this.Organization.Comments = this.tbComments.Text.Trim();
        }

        public void SetValueForOrganization(OrganizationData organization)
        {
            this.Organization = organization;
            //ViewState["OrganizationData"] = organization;
        }
        public OrganizationData GetValueFromOrganization()
        {
            if (this.Organization == null)
            {
                this.Organization = new OrganizationData();
            }

            this.GetValuesFromForm();
            //ViewState["OrganizationData"] = this.Organization;

            return this.Organization;
        }
    }
}