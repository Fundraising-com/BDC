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
    public partial class OrderTermsView : System.Web.UI.UserControl
    {
        private CampaignData OrderTerms;
        private decimal AccountProfit;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForOrderTerms(CampaignData orderTerms)
        {
            this.OrderTerms = orderTerms;
        }
        public void SetValueForAccountProfit(decimal accountProfit)
        {
            this.AccountProfit = accountProfit;
        }
        public void SetValuesToForm()
        {
            if (this.OrderTerms != null)
            {
                if (this.OrderTerms.GoalEstimatedGross.HasValue)
                {
                    this.lblGoalEstimatedGross.Text = this.OrderTerms.GoalEstimatedGross.Value.ToString();
                }
                this.lblEnrollment.Text = this.OrderTerms.Enrollment.ToString();
                this.lblStartDate.Text = this.OrderTerms.StartDate.ToShortDateString();
                this.lblEndDate.Text = this.OrderTerms.EndDate.ToShortDateString();
                this.lblFiscalYear.Text = this.OrderTerms.FiscalYear.ToString();
            }

            if (this.AccountProfit == null)
            {
                this.trAccountProfit.Visible = false;
            }
            else
            {
                this.trAccountProfit.Visible = true;
                this.lblAccountProfit.Text = (this.AccountProfit * 100).ToString() + " %";
            }
        }
    }
}