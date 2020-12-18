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

namespace QSP.OrderExpress.Web.V2.UserControls
{
    public partial class OrganizationInformationView : System.Web.UI.UserControl
    {
        private OrganizationData Organization;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForOrganization(OrganizationData organization)
        {
            this.Organization = organization;
        }
        public void SetValuesToForm()
        {
            if (this.Organization != null)
            {
                this.lblQspId.Text = this.Organization.Id.ToString();

                if (this.Organization.Name != null)
                {
                    this.lblName.Text = this.Organization.Name.Trim();
                }

                this.lblType.Text = "";
                if (this.Organization.Type != null)
                {
                    if (this.Organization.Type.Name != null)
                    {
                        this.lblType.Text = this.Organization.Type.Name;
                    }
                }

                this.lblLevel.Text = "";
                if (this.Organization.Level != null)
                {
                    if (this.Organization.Level.Name != null)
                    {
                        this.lblLevel.Text = this.Organization.Level.Name;
                    }
                }

                if (this.Organization.TaxExemptionNumber != null)
                {
                    this.lblTaxExemption.Text = this.Organization.TaxExemptionNumber.Trim();
                }

                if (this.Organization.TaxExemptionExpirationDate.HasValue)
                {
                    this.lblTaxExeptionExpiry.Text = this.Organization.TaxExemptionExpirationDate.Value.ToLongDateString();
                }
                else
                {
                    this.lblTaxExeptionExpiry.Text = "";
                }

                if (this.Organization.MDRPID != null)
                {
                    this.lblMDRPID.Text = this.Organization.MDRPID.Trim();
                }

                if (this.Organization.Comments != null)
                {
                    this.lblComments.Text = this.Organization.Comments.Trim().Replace(System.Environment.NewLine, "<br />");
                }
            }

        }
    }
}