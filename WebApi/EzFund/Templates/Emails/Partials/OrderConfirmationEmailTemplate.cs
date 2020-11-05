using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
   public partial class OrderConfirmationEmailTemplate
   {
      private IList<Sale> sales;
      public string Subject { get; set; }
      public IList<Sale> Sales { get; set; }
      
        public OrderConfirmationEmailTemplate(string subject, IList<Sale> sales)
        {
            Subject = subject;
            this.sales = sales;
        }
    }
}