using System.Collections.Generic;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class OrderCreatedEmailTemplate
   {
        private EzFundSale sales;

        public IList<Sale> Sales { get; set; }
      public string Subject { get; set; }
      //public OrderCreatedEmailTemplate(string subject, IList<Sale> sales)
      //{
      //   Subject = subject;
      //   Sales = sales;
      //}

        public OrderCreatedEmailTemplate(string subject, EzFundSale sales)
        {
            Subject = subject;
            this.sales = sales;
        }
    }
}