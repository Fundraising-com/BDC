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
    public partial class OrderSummaryView : System.Web.UI.UserControl
    {
        private OrderSummaryData OrderSummary;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetValueForOrderSummary(OrderSummaryData orderSummary)
        {
            this.OrderSummary = orderSummary;
        }
        public void SetValuesToForm()
        {
            if (this.OrderSummary != null)
            {
                this.lblSubTotal.Text = this.OrderSummary.SubTotal.ToString("C");
                this.lblTaxRate.Text = (this.OrderSummary.TaxRate * 100).ToString() + " %";
                this.lblTaxAmount.Text = this.OrderSummary.TaxAmount.ToString("C");
                this.lblShippingCharges.Text = this.OrderSummary.ShippingCharges.ToString("C");
                this.lblSurcharges.Text = this.OrderSummary.Surcharges.ToString("C");
                this.lblCredits.Text = this.OrderSummary.Credits.ToString("C");
                this.lblGrandTotal.Text = this.OrderSummary.GrandTotal.ToString("C");

                if (this.OrderSummary.Credits > 0)
                {
                    this.trCredits.Visible = true;
                }
                else
                {
                    this.trCredits.Visible = false;
                }
                if (this.OrderSummary.Surcharges > 0)
                {
                    this.trSurcharge.Visible = true;
                }
                else
                {
                    this.trSurcharge.Visible = false;
                }
                if (this.OrderSummary.ShippingCharges > 0)
                {
                    this.trShippingCharges.Visible = true;
                }
                else
                {
                    this.trShippingCharges.Visible = false;
                }
            }
            else
            {
                this.lblSubTotal.Text = "";
                this.lblTaxRate.Text = "";
                this.lblTaxAmount.Text = "";
                this.lblShippingCharges.Text = "";
                this.lblSurcharges.Text = "";
                this.lblCredits.Text = "";
                this.lblGrandTotal.Text = "";

                this.trCredits.Visible = false;
                this.trSurcharge.Visible = false;
                this.trShippingCharges.Visible = false;
            }

        }
    }
}