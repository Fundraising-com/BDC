using System;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class AccountInformationView : System.Web.UI.UserControl
    {
        private AccountData Account;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForAccount(AccountData account)
        {
            this.Account = account;
        }
        public void SetValuesToForm()
        {
            if (this.Account != null)
            {
                this.lblQspId.Text = this.Account.Id.ToString();
                if (this.Account.EdsId.HasValue)
                {
                    this.lblEdsId.Text = this.Account.EdsId.ToString();
                }
                this.lblName.Text = this.Account.Name;

                ColorConverter colConvert = new ColorConverter();
                this.lblStatusBox.BackColor = (Color)colConvert.ConvertFromString(this.Account.StatusColor);
                this.lblStatus.Text = this.Account.StatusName;
                this.lblFSM.Text = string.Format("{0} - {1}, {2}", this.Account.FsmId, this.Account.FsmLastName, this.Account.FsmFirstName);

                this.hlOrganizationView.NavigateUrl = string.Format("~/V2/Forms/OrganizationView.aspx?OrganizationId={0}", this.Account.Organization.Id);
                this.lblOrganizationName.Text = this.Account.Organization.Name;
                this.lblOrganizationType.Text = this.Account.Organization.Type.Name;
                this.lblOrganizationLevel.Text = this.Account.Organization.Level.Name;

                this.lblQSPProgram.Text = this.Account.LastCampaign.ProgramTypeName ?? "";
                this.lblTradeClass.Text = this.Account.LastCampaign.TradeClassName ?? "";
                this.lblLastFiscalYear.Text = this.Account.LastCampaign.FiscalYear.ToString();
                this.lblInactiveMonths.Text = this.Account.LastCampaign.InactiveMonths ?? "";
                if (this.Account.LastCampaign.LastOrderDate.HasValue)
                {
                    this.lblLastOrder.Text = this.Account.LastCampaign.LastOrderDate.Value.ToLongDateString();
                }
                this.lblWarehouse.Text = this.Account.LastCampaign.WarehouseName ?? "";

                this.lblTaxExemption.Text = this.Account.TaxExemptionNumber ?? "";
                if (this.Account.TaxExemptionExpirationDate.HasValue)
                {
                    this.lblTaxExpirationExpire.Text = this.Account.TaxExemptionExpirationDate.Value.ToLongDateString();
                }

                if (this.Account.CollectionAmouunt.HasValue)
                {
                    this.lblCollectionAmount.Text = this.Account.CollectionAmouunt.Value.ToString("C");
                }
                if (this.Account.CollectionDate.HasValue)
                {
                    this.lblCollectionDate.Text = this.Account.CollectionDate.Value.ToLongDateString();
                }

                if (this.Account.Comments != null)
                {
                    this.lblComments.Text = this.Account.Comments.Trim().Replace(System.Environment.NewLine, "<br />");
                }
            }

        }
    }
}