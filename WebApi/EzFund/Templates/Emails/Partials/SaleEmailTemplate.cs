using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class SaleEmailTemplate
   {
        private EzFundSale sales;

        public IList<Sale> Sales { get; set; }
        public string Subject { get; set; }

        public SaleEmailTemplate(string subject, EzFundSale sales, string extraMessage = "")
        {

           
            Subject = subject;
            this.sales = sales;

            //#region Review Empty Strings
            sales.Client.Addresses[1].Address2 = sales.Client.Addresses[1].Address2 ?? "No Additional Shipping Address Info";
            sales.Client.Addresses[0].Address2 = sales.Client.Addresses[0].Address2 ?? "No Additional Billing Info";
            sales.DeliveryComments = sales.DeliveryComments ?? "No Delivery Additional Info";
            sales.ReferralCode = sales.ReferralCode ?? "N/A";
            //#endregion
        }
    }
}

